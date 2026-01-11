using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.CategoriaService
{
    /// <summary>
    /// Implementação do serviço de Categorias.
    /// Responsável pela lógica de negócio relacionada a operações com categorias.
    /// </summary>
    public class CategoriaService : ICategoriaInterface
    {
        // Contexto do banco de dados para acesso às tabelas
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="context">Instância do ApplicationDbContext configurada no Startup</param>
        public CategoriaService(ApplicationDbContext context)
        {
            _context = context; // Armazena o contexto para uso nos métodos
        }

        /// <summary>
        /// Implementação do método para obter todas as categorias.
        /// Acessa o banco de dados de forma assíncrona para listar as categorias.
        /// </summary>
        public async Task<ServiceResponse<List<CategoriaModel>>> GetCategorias()
        {
            // Cria a resposta padrão do serviço
            var response = new ServiceResponse<List<CategoriaModel>>();

            try
            {
                // LÓGICA PRINCIPAL: Consulta todas as categorias no banco de dados
                // ToListAsync() é usado para execução assíncrona e melhor performance
                response.Dados = await _context.Categorias.ToListAsync();

                // Se chegou aqui, a operação foi bem-sucedida
                // As propriedades Sucesso=true e Mensagem=null são padrão do ServiceResponse
            }
            catch (Exception ex)
            {
                // TRATAMENTO DE ERRO: Captura qualquer exceção durante a operação
                response.Mensagem = ex.Message; // Propaga a mensagem de erro
                response.Sucesso = false;       // Indica que a operação falhou

                // IMPORTANTE: Em produção, considerar logging mais detalhado aqui
                // Console.Error.WriteLine($"Erro ao buscar categorias: {ex}");
            }

            return response; // Retorna a resposta (sucesso ou erro)
        }

        /// <summary>
        /// Implementação do método para criar uma nova categoria.
        /// Valida e persiste a nova categoria no banco de dados.
        /// </summary>
        public async Task<ServiceResponse<List<CategoriaModel>>> CreateCategoria(CategoriaModel novaCategoria)
        {
            var response = new ServiceResponse<List<CategoriaModel>>();

            try
            {
                // LÓGICA PRINCIPAL: Adiciona a nova categoria ao contexto
                _context.Categorias.Add(novaCategoria);

                // Persiste as alterações no banco de dados de forma assíncrona
                await _context.SaveChangesAsync();

                // Após criar, retorna a lista atualizada de categorias
                response.Dados = await _context.Categorias.ToListAsync();

                // OBSERVAÇÃO: Retornar a lista atualizada permite ao cliente
                // atualizar sua interface sem necessidade de nova requisição
            }
            catch (Exception ex)
            {
                // TRATAMENTO DE ERRO: Captura exceções (ex: violação de constraints, conexão)
                response.Mensagem = ex.Message;
                response.Sucesso = false;

                // PONTO DE MELHORIA: Poderiamos adicionar validações específicas aqui:
                // - Verificar se a descrição já existe (unicidade)
                // - Validar o campo Finalidade (Despesa/Receita/Ambas)
                // - Verificar se a categoria é usada em transações (se houver exclusão)
            }

            return response;
        }
    }
}