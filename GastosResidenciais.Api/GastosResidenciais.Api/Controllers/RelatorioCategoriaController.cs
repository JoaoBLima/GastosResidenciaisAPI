using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.RelatorioCategoriaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerar relatórios por categoria (funcionalidade opcional).
    /// Expõe endpoint para análise financeira agrupada por categoria.
    /// </summary>
    [Route("api/[controller]")] // Rota base: api/RelatorioCategoria
    [ApiController]
    public class RelatorioCategoriaController : ControllerBase
    {
        private readonly IRelatorioCategoriaInterface _service;

        public RelatorioCategoriaController(IRelatorioCategoriaInterface service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint GET: api/RelatorioCategoria
        /// Gera relatório de totais financeiros agrupados por categoria.
        /// ATENDE REQUISITO OPCIONAL: "Consulta de totais por categoria"
        /// </summary>
        /// <returns>
        /// Relatório contendo análise financeira por categoria.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<RelatorioCategoriaGeralModel>>> GetRelatorioCategoria()
        {
            return Ok(await _service.GetRelatorioPorCategoria());
        }
    }
}