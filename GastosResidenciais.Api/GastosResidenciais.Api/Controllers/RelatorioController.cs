using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;
using GastosResidenciais.Api.Service.RelatorioService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioInterface _relatorioService;

        public RelatorioController(IRelatorioInterface relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("totais-por-pessoa")]
        public async Task<ActionResult<ServiceResponse<RelatorioTotaisPessoaResponse>>> GetTotaisPorPessoa()
        {
            return Ok(await _relatorioService.GetTotaisPorPessoa());
        }
    }
}
