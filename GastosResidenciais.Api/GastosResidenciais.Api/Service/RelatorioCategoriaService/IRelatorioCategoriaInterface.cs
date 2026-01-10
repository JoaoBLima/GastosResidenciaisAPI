using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.RelatorioCategoriaService
{
    public interface IRelatorioCategoriaInterface
    {
        Task<ServiceResponse<RelatorioCategoriaGeralModel>> GetRelatorioPorCategoria();
    }
}
