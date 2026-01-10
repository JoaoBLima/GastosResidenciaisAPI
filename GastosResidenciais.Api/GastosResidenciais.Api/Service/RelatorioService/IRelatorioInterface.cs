using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;

namespace GastosResidenciais.Api.Service.RelatorioService
{
    public interface IRelatorioInterface
    {
        Task<ServiceResponse<RelatorioTotaisPessoaResponse>> GetTotaisPorPessoa();
    }
}
