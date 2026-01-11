using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.TransacaoService
{
    /// <summary>
    /// Interface que define o contrato para operações relacionadas a Transações.
    /// Segue o padrão de injeção de dependência para desacoplar a implementação.
    /// </summary>
    public interface ITransacaoInterface
    {
        /// <summary>
        /// Obtém a lista de todas as transações cadastradas.
        /// Inclui dados relacionados de Pessoa e Categoria para exibição completa.
        /// </summary>
        /// <returns>
        /// ServiceResponse contendo a lista de transações com seus relacionamentos.
        /// </returns>
        Task<ServiceResponse<List<TransacaoModel>>> GetTransacoes();

        /// <summary>
        /// Cria uma nova transação no sistema com validações de regras de negócio.
        /// </summary>
        /// <param name="novaTransacao">Objeto TransacaoModel com os dados da nova transação</param>
        /// <returns>
        /// ServiceResponse contendo a lista atualizada de transações.
        /// </returns>
        /// <remarks>
        /// Validações implementadas:
        /// 1. Menor de idade só pode ter transações do tipo Despesa
        /// 2. Categoria deve ser compatível com o tipo da transação
        /// </remarks>
        Task<ServiceResponse<List<TransacaoModel>>> CreateTransacao(TransacaoModel novaTransacao);
    }
}