using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.CategoriaService
{
    /// <summary>
    /// Interface que define o contrato para operações relacionadas a Categorias.
    /// Segue o padrão de injeção de dependência para desacoplar a implementação.
    /// </summary>
    public interface ICategoriaInterface
    {
        /// <summary>
        /// Obtém a lista de todas as categorias cadastradas.
        /// </summary>
        /// <returns>
        /// ServiceResponse contendo a lista de categorias ou informações de erro.
        /// </returns>
        Task<ServiceResponse<List<CategoriaModel>>> GetCategorias();

        /// <summary>
        /// Cria uma nova categoria no sistema.
        /// </summary>
        /// <param name="novaCategoria">Objeto CategoriaModel com os dados da nova categoria</param>
        /// <returns>
        /// ServiceResponse contendo a lista atualizada de categorias ou informações de erro.
        /// </returns>
        Task<ServiceResponse<List<CategoriaModel>>> CreateCategoria(CategoriaModel novaCategoria);
    }
}