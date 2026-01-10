using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Service.CategoriaService;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaInterface _service;

        public CategoriaController(ICategoriaInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CategoriaModel>>>> GetCategorias()
        {
            return Ok(await _service.GetCategorias());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CategoriaModel>>>> CreateCategoria(CategoriaModel novaCategoria)
        {
            return Ok(await _service.CreateCategoria(novaCategoria));
        }
    }
}
