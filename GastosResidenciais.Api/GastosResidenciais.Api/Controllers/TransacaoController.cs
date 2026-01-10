using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.TransacaoService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoInterface _service;

        public TransacaoController(ITransacaoInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<TransacaoModel>>>> GetTransacoes()
        {
            return Ok(await _service.GetTransacoes());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<TransacaoModel>>>> CreateTransacao(TransacaoModel nova)
        {
            return Ok(await _service.CreateTransacao(nova));
        }
    }
}
