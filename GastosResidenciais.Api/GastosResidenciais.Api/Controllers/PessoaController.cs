using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.PessoaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaInterface _service;

        public PessoaController(IPessoaInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> GetPessoas()
        {
            return Ok(await _service.GetPessoas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> GetPessoaById(int id)
        {
            return Ok(await _service.GetPessoaById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> CreatePessoa(PessoaModel novaPessoa)
        {
            return Ok(await _service.CreatePessoa(novaPessoa));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> DeletePessoa(int id)
        {
            return Ok(await _service.DeletePessoa(id));
        }
    }
}
