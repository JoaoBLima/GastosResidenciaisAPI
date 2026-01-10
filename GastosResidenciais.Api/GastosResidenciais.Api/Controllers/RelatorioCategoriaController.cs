using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.RelatorioCategoriaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioCategoriaController : ControllerBase
    {
        private readonly IRelatorioCategoriaInterface _service;

        public RelatorioCategoriaController(IRelatorioCategoriaInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<RelatorioCategoriaGeralModel>>> GetRelatorioCategoria()
        {
            return Ok(await _service.GetRelatorioPorCategoria());
        }
    }
}
