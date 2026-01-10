using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Enums;
using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.RelatorioService
{
    public class RelatorioService : IRelatorioInterface
    {
        private readonly ApplicationDbContext _context;

        public RelatorioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<RelatorioTotaisPessoaResponse>> GetTotaisPorPessoa()
        {
            ServiceResponse<RelatorioTotaisPessoaResponse> serviceResponse = new();

            try
            {
                var pessoas = await _context.Pessoas
                    .Include(p => p.Transacoes)
                    .ToListAsync();

                RelatorioTotaisPessoaResponse resultado = new();

                foreach (var pessoa in pessoas)
                {
                    var totalReceitas = pessoa.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Receita)
                        .Sum(t => t.Valor);

                    var totalDespesas = pessoa.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Despesa)
                        .Sum(t => t.Valor);

                    resultado.TotaisPorPessoa.Add(new TotaisPorPessoaModel
                    {
                        PessoaId = pessoa.Id,
                        Nome = pessoa.Nome,
                        TotalReceitas = totalReceitas,
                        TotalDespesas = totalDespesas
                    });
                }

                resultado.TotalGeral.TotalReceitas = resultado.TotaisPorPessoa.Sum(x => x.TotalReceitas);
                resultado.TotalGeral.TotalDespesas = resultado.TotaisPorPessoa.Sum(x => x.TotalDespesas);

                serviceResponse.Dados = resultado;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
