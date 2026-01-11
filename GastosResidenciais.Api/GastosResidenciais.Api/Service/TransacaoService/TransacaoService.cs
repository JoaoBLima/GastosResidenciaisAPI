using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Enums;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.TransacaoService
{
    /// <summary>
    /// Implementação do serviço de Transações.
    /// Contém regras de negócio complexas relacionadas a validações de transações.
    /// Responsável por garantir a integridade das transações financeiras.
    /// </summary>
    public class TransacaoService : ITransacaoInterface
    {
        // Contexto do banco de dados para acesso às tabelas
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="context">Instância do ApplicationDbContext configurada no Startup</param>
        public TransacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as transações cadastradas, incluindo dados relacionados.
        /// Utiliza o método Include do Entity Framework para carregar relacionamentos.
        /// </summary>
        public async Task<ServiceResponse<List<TransacaoModel>>> GetTransacoes()
        {
            var response = new ServiceResponse<List<TransacaoModel>>();

            try
            {
                // LÓGICA PRINCIPAL: Consulta transações com relacionamentos (JOIN)
                // Include() carrega os objetos relacionados para evitar consultas adicionais (N+1)
                response.Dados = await _context.Transacoes
                    .Include(t => t.Pessoa)    // Carrega dados da pessoa relacionada
                    .Include(t => t.Categoria) // Carrega dados da categoria relacionada
                    .ToListAsync();

                // BENEFÍCIO: Esta abordagem permite que o front-end tenha acesso imediato a:
                // - Nome da pessoa (t.Pessoa.Nome) sem consulta adicional
                // - Descrição da categoria (t.Categoria.Descricao) sem consulta adicional
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        /// <summary>
        /// Cria uma nova transação com validações de regras de negócio.
        /// Implementa duas regras principais conforme especificação do projeto.
        /// </summary>
        public async Task<ServiceResponse<List<TransacaoModel>>> CreateTransacao(TransacaoModel nova)
        {
            var response = new ServiceResponse<List<TransacaoModel>>();

            try
            {
                // ETAPA 1: Verificar existência das entidades relacionadas
                var pessoa = await _context.Pessoas.FindAsync(nova.PessoaId);
                var categoria = await _context.Categorias.FindAsync(nova.CategoriaId);

                // VALIDAÇÃO BÁSICA: Pessoa e Categoria devem existir
                if (pessoa == null || categoria == null)
                {
                    response.Mensagem = "Pessoa ou Categoria não encontrada.";
                    response.Sucesso = false;
                    return response; // Early return em caso de erro
                }

                // ============================================================
                // REGRAS DE NEGÓCIO IMPLEMENTADAS:
                // ============================================================

                // REGRA 1: Menor de idade só pode ter transações do tipo DESPESA
                // Lógica: Se pessoa tem menos de 18 anos E transação é Receita → ERRO
                if (pessoa.Idade < 18 && nova.Tipo == TipoTransacaoEnum.Receita)
                {
                    response.Mensagem = "Menor de idade não pode ter receita.";
                    response.Sucesso = false;
                    return response;
                }
                // JUSTIFICATIVA: Esta regra impede que menores de idade tenham receitas
                // no sistema, conforme especificado nos requisitos.

                // REGRA 2: Categoria precisa ser compatível com o tipo da transação
                // Lógica: Se categoria não é "Ambas" E tipo não corresponde → ERRO
                if (categoria.Finalidade != FinalidadeCategoriaEnum.Ambas &&
                    (int)categoria.Finalidade != (int)nova.Tipo)
                {
                    response.Mensagem = "Categoria incompatível com o tipo da transação.";
                    response.Sucesso = false;
                    return response;
                }
                // EXPLICAÇÃO DA LÓGICA:
                // - FinalidadeCategoriaEnum: 0=Despesa, 1=Receita, 2=Ambas
                // - TipoTransacaoEnum: 0=Despesa, 1=Receita
                // - (int) converte enum para inteiro para comparação
                // - Se categoria é só para Despesa (0) e transação é Receita (1) → ERRO
                // - Se categoria é só para Receita (1) e transação é Despesa (0) → ERRO
                // - Se categoria é Ambas (2) → SEMPRE VÁLIDO

                // ============================================================
                // PERSISTÊNCIA DOS DADOS (após todas as validações)
                // ============================================================

                _context.Transacoes.Add(nova);
                await _context.SaveChangesAsync();

                // Retorna lista atualizada (sem Include para performance)
                response.Dados = await _context.Transacoes.ToListAsync();

                
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }
    }
}