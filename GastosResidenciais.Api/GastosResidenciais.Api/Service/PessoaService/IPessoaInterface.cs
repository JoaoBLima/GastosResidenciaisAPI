using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.PessoaService
{
    /// <summary>
    /// Interface que define o contrato para operações relacionadas a Pessoas.
    /// Segue o padrão de injeção de dependência para desacoplar a implementação.
    /// </summary>
    public interface IPessoaInterface
    {
        /// <summary>
        /// Obtém a lista de todas as pessoas cadastradas no sistema.
        /// </summary>
        /// <returns>
        /// ServiceResponse contendo a lista de pessoas ou informações de erro.
        /// </returns>
        Task<ServiceResponse<List<PessoaModel>>> GetPessoas();

        /// <summary>
        /// Obtém uma pessoa específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da pessoa a ser buscada</param>
        /// <returns>
        /// ServiceResponse contendo a pessoa encontrada ou mensagem de erro se não encontrada.
        /// </returns>
        Task<ServiceResponse<PessoaModel>> GetPessoaById(int id);

        /// <summary>
        /// Cria uma nova pessoa no sistema.
        /// </summary>
        /// <param name="novaPessoa">Objeto PessoaModel com os dados da nova pessoa</param>
        /// <returns>
        /// ServiceResponse contendo a lista atualizada de pessoas ou informações de erro.
        /// </returns>
        Task<ServiceResponse<List<PessoaModel>>> CreatePessoa(PessoaModel novaPessoa);

        /// <summary>
        /// Exclui uma pessoa do sistema.
        /// IMPORTANTE: Ao excluir uma pessoa, todas as suas transações também são excluídas (cascata).
        /// </summary>
        /// <param name="id">ID da pessoa a ser excluída</param>
        /// <returns>
        /// ServiceResponse contendo a lista atualizada de pessoas após exclusão.
        /// </returns>
        Task<ServiceResponse<List<PessoaModel>>> DeletePessoa(int id);
    }
}