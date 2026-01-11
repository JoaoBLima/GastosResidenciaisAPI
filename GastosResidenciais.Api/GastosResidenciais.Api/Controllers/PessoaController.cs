using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.PessoaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar operações relacionadas a Pessoas.
    /// Expõe endpoints RESTful para CRUD completo de pessoas.
    /// </summary>
    [Route("api/[controller]")] // Rota base: api/Pessoa
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaInterface _service;

        public PessoaController(IPessoaInterface service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint GET: api/Pessoa
        /// Obtém a lista de todas as pessoas cadastradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> GetPessoas()
        {
            return Ok(await _service.GetPessoas());
        }

        /// <summary>
        /// Endpoint GET: api/Pessoa/{id}
        /// Obtém uma pessoa específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da pessoa (parâmetro de rota)</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<PessoaModel>>> GetPessoaById(int id)
        {
            return Ok(await _service.GetPessoaById(id));
        }

        /// <summary>
        /// Endpoint POST: api/Pessoa
        /// Cria uma nova pessoa no sistema.
        /// </summary>
        /// <param name="novaPessoa">Dados da pessoa a ser criada</param>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> CreatePessoa(PessoaModel novaPessoa)
        {
            return Ok(await _service.CreatePessoa(novaPessoa));
        }

        /// <summary>
        /// Endpoint DELETE: api/Pessoa/{id}
        /// Exclui uma pessoa do sistema.
        /// IMPORTANTE: Ao excluir uma pessoa, todas as suas transações também são excluídas.
        /// </summary>
        /// <param name="id">ID da pessoa a ser excluída</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<PessoaModel>>>> DeletePessoa(int id)
        {
            return Ok(await _service.DeletePessoa(id));
        }
    }
}