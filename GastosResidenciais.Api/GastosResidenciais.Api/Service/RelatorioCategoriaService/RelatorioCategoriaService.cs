using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Enums;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.RelatorioCategoriaService
{
    public class RelatorioCategoriaService : IRelatorioCategoriaInterface
    {
        private readonly ApplicationDbContext _context;

        public RelatorioCategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<RelatorioCategoriaGeralModel>> GetRelatorioPorCategoria()
        {
            ServiceResponse<RelatorioCategoriaGeralModel> serviceResponse = new ServiceResponse<RelatorioCategoriaGeralModel>();

            try
            {
                var categorias = await _context.Categorias
                    .Include(c => c.Transacoes)
                    .ToListAsync();

                List<RelatorioCategoriaModel> lista = new List<RelatorioCategoriaModel>();

                decimal totalReceitasGeral = 0;
                decimal totalDespesasGeral = 0;

                foreach (var categoria in categorias)
                {
                    decimal receitas = categoria.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Receita)
                        .Sum(t => t.Valor);

                    decimal despesas = categoria.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Despesa)
                        .Sum(t => t.Valor);

                    lista.Add(new RelatorioCategoriaModel
                    {
                        CategoriaId = categoria.Id,
                        Descricao = categoria.Descricao,
                        TotalReceitas = receitas,
                        TotalDespesas = despesas
                    });

                    totalReceitasGeral += receitas;
                    totalDespesasGeral += despesas;
                }

                serviceResponse.Dados = new RelatorioCategoriaGeralModel
                {
                    Categorias = lista,
                    TotalReceitasGeral = totalReceitasGeral,
                    TotalDespesasGeral = totalDespesasGeral
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }
    }
}
