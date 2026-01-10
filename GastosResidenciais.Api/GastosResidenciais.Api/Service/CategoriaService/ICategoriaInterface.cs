using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.CategoriaService
{
    public interface ICategoriaInterface
    {
        Task<ServiceResponse<List<CategoriaModel>>> GetCategorias();
        Task<ServiceResponse<List<CategoriaModel>>> CreateCategoria(CategoriaModel novaCategoria);
    }
}
