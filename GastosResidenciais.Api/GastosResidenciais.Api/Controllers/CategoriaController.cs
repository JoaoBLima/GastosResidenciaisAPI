using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.CategoriaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar operações relacionadas a Categorias.
    /// Expõe endpoints RESTful para criação e consulta de categorias.
    /// </summary>
    [Route("api/[controller]")] // Define a rota base: api/Categoria
    [ApiController]            // Ativa comportamentos padrão do API Controller
    public class CategoriaController : ControllerBase
    {
        // Serviço de categorias injetado via construtor
        private readonly ICategoriaInterface _service;

        /// <summary>
        /// Construtor que recebe o serviço de categorias por injeção de dependência.
        /// </summary>
        public CategoriaController(ICategoriaInterface service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint GET: api/Categoria
        /// Obtém a lista de todas as categorias cadastradas.
        /// </summary>
        /// <returns>
        /// 200 OK com lista de categorias ou informações de erro no ServiceResponse.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CategoriaModel>>>> GetCategorias()
        {
            // Chama o serviço e retorna resposta HTTP 200 (OK) com os dados
            return Ok(await _service.GetCategorias());
        }

        /// <summary>
        /// Endpoint POST: api/Categoria
        /// Cria uma nova categoria no sistema.
        /// </summary>
        /// <param name="novaCategoria">Dados da categoria a ser criada (via body da requisição)</param>
        /// <returns>
        /// 200 OK com lista atualizada de categorias após criação.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CategoriaModel>>>> CreateCategoria(CategoriaModel novaCategoria)
        {
            // Chama o serviço para criar categoria e retorna resposta
            return Ok(await _service.CreateCategoria(novaCategoria));
        }
    }
}