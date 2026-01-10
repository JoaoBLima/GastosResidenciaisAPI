using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.CategoriaService
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CategoriaModel>>> GetCategorias()
        {
            var response = new ServiceResponse<List<CategoriaModel>>();

            try
            {
                response.Dados = await _context.Categorias.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<CategoriaModel>>> CreateCategoria(CategoriaModel novaCategoria)
        {
            var response = new ServiceResponse<List<CategoriaModel>>();

            try
            {
                _context.Categorias.Add(novaCategoria);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Categorias.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }
    }
}