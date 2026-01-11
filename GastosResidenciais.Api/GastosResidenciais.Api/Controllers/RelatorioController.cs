using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;
using GastosResidenciais.Api.Service.RelatorioService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerar relatórios analíticos do sistema.
    /// Expõe endpoints para consultas agregadas e análises financeiras.
    /// </summary>
    [Route("api/[controller]")] // Rota base: api/Relatorio
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioInterface _relatorioService;

        public RelatorioController(IRelatorioInterface relatorioService)
        {
            _relatorioService = relatorioService;
        }

        /// <summary>
        /// Endpoint GET: api/Relatorio/totais-por-pessoa
        /// Gera relatório de totais financeiros agrupados por pessoa.
        /// ATENDE REQUISITO OBRIGATÓRIO: "Consulta de totais por pessoa"
        /// </summary>
        /// <returns>
        /// Relatório contendo:
        /// - Lista de pessoas com totais de receitas, despesas e saldo
        /// - Totais gerais consolidados
        /// </returns>
        [HttpGet("totais-por-pessoa")]
        public async Task<ActionResult<ServiceResponse<RelatorioTotaisPessoaResponse>>> GetTotaisPorPessoa()
        {
            return Ok(await _relatorioService.GetTotaisPorPessoa());
        }
    }
}