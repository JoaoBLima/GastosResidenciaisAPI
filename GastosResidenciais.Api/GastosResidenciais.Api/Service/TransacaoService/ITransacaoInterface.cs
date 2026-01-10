using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.TransacaoService
{
    public interface ITransacaoInterface
    {
        Task<ServiceResponse<List<TransacaoModel>>> GetTransacoes();
        Task<ServiceResponse<List<TransacaoModel>>> CreateTransacao(TransacaoModel novaTransacao);
    }
}
