using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.PessoaService
{
    /// <summary>
    /// Implementação do serviço de Pessoas.
    /// Responsável pela lógica de negócio relacionada a operações com pessoas.
    /// Contém regras específicas como validação de idade e exclusão em cascata.
    /// </summary>
    public class PessoaService : IPessoaInterface
    {
        // Contexto do banco de dados para acesso às tabelas
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="context">Instância do ApplicationDbContext configurada no Startup</param>
        public PessoaService(ApplicationDbContext context)
        {
            _context = context; // Armazena o contexto para uso nos métodos
        }

        /// <summary>
        /// Obtém todas as pessoas cadastradas no sistema.
        /// </summary>
        public async Task<ServiceResponse<List<PessoaModel>>> GetPessoas()
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                // LÓGICA PRINCIPAL: Consulta todas as pessoas no banco de dados
                response.Dados = await _context.Pessoas.ToListAsync();

                // Mensagem informativa quando não há pessoas cadastradas
                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhuma pessoa cadastrada.";
            }
            catch (Exception ex)
            {
                // TRATAMENTO DE ERRO: Captura exceções durante a consulta
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        /// <summary>
        /// Obtém uma pessoa específica pelo seu ID.
        /// </summary>
        public async Task<ServiceResponse<PessoaModel>> GetPessoaById(int id)
        {
            var response = new ServiceResponse<PessoaModel>();

            try
            {
                // LÓGICA PRINCIPAL: Busca pessoa pelo ID usando FirstOrDefaultAsync
                var pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

                // VALIDAÇÃO: Verifica se a pessoa foi encontrada
                if (pessoa == null)
                {
                    response.Mensagem = "Pessoa não encontrada.";
                    response.Sucesso = false;
                    return response; // Retorna early em caso de erro
                }

                response.Dados = pessoa; // Retorna a pessoa encontrada
            }
            catch (Exception ex)
            {
                // TRATAMENTO DE ERRO: Captura exceções durante a busca
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        /// <summary>
        /// Cria uma nova pessoa no sistema com validação de idade.
        /// </summary>
        public async Task<ServiceResponse<List<PessoaModel>>> CreatePessoa(PessoaModel novaPessoa)
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                // VALIDAÇÃO DE REGRA DE NEGÓCIO: Idade não pode ser negativa
                if (novaPessoa.Idade < 0)
                {
                    response.Mensagem = "Idade inválida.";
                    response.Sucesso = false;
                    return response; // Retorna early em caso de validação falhar
                }

                // LÓGICA PRINCIPAL: Adiciona a nova pessoa ao contexto
                _context.Pessoas.Add(novaPessoa);

                // Persiste as alterações no banco de dados
                await _context.SaveChangesAsync();

                // Retorna a lista atualizada de pessoas
                response.Dados = await _context.Pessoas.ToListAsync();

                // PONTO DE MELHORIA: Poderia adicionar mais validações:
                // - Nome não pode ser vazio
                // - Idade máxima razoável
                // - Nome não pode conter caracteres especiais
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        /// <summary>
        /// Exclui uma pessoa do sistema.
        /// IMPLEMENTA REGRA DE NEGÓCIO: Ao excluir pessoa, exclui também todas as suas transações.
        /// </summary>
        public async Task<ServiceResponse<List<PessoaModel>>> DeletePessoa(int id)
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                // LÓGICA PRINCIPAL: Busca pessoa incluindo suas transações (Include)
                // O Include é necessário para carregar as transações relacionadas
                var pessoa = await _context.Pessoas
                    .Include(p => p.Transacoes) // Carrega transações relacionadas
                    .FirstOrDefaultAsync(p => p.Id == id);

                // VALIDAÇÃO: Verifica se a pessoa existe
                if (pessoa == null)
                {
                    response.Mensagem = "Pessoa não encontrada.";
                    response.Sucesso = false;
                    return response;
                }

                // REGRA DE NEGÓCIO IMPLEMENTADA: Exclui todas as transações da pessoa primeiro
                // Isso evita violação de chave estrangeira e mantém a consistência do banco
                _context.Transacoes.RemoveRange(pessoa.Transacoes);

                // Exclui a pessoa após remover suas transações
                _context.Pessoas.Remove(pessoa);

                // Persiste as alterações (transações removidas + pessoa removida)
                await _context.SaveChangesAsync();

                // Retorna a lista atualizada de pessoas
                response.Dados = await _context.Pessoas.ToListAsync();

                // DECISÃO TÉCNICA: A exclusão em cascata foi implementada manualmente
                // em vez de usar Cascade Delete do EF para maior controle e clareza
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