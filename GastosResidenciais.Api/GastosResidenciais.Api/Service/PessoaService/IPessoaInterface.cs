using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Service.PessoaService
{
    public interface IPessoaInterface
    {
        Task<ServiceResponse<List<PessoaModel>>> GetPessoas();
        Task<ServiceResponse<PessoaModel>> GetPessoaById(int id);
        Task<ServiceResponse<List<PessoaModel>>> CreatePessoa(PessoaModel novaPessoa);
        Task<ServiceResponse<List<PessoaModel>>> DeletePessoa(int id);
    }
}
