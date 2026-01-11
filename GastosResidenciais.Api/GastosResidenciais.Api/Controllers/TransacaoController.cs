using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.TransacaoService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar operações relacionadas a Transações.
    /// Expõe endpoints para criação e consulta de transações financeiras.
    /// Implementa validações de regras de negócio via serviço.
    /// </summary>
    [Route("api/[controller]")] // Rota base: api/Transacao
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoInterface _service;

        public TransacaoController(ITransacaoInterface service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint GET: api/Transacao
        /// Obtém a lista de todas as transações cadastradas.
        /// Inclui dados relacionados de Pessoa e Categoria.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<TransacaoModel>>>> GetTransacoes()
        {
            return Ok(await _service.GetTransacoes());
        }

        /// <summary>
        /// Endpoint POST: api/Transacao
        /// Cria uma nova transação no sistema.
        /// VALIDAÇÕES APLICADAS NO SERVIÇO:
        /// 1. Pessoa e Categoria devem existir
        /// 2. Menor de idade só pode ter transações do tipo Despesa
        /// 3. Categoria deve ser compatível com o tipo da transação
        /// </summary>
        /// <param name="nova">Dados da transação a ser criada</param>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<TransacaoModel>>>> CreateTransacao(TransacaoModel nova)
        {
            return Ok(await _service.CreateTransacao(nova));
        }
    }
}