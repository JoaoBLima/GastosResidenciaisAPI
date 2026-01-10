using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Enums;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.TransacaoService
{
    public class TransacaoService : ITransacaoInterface
    {
        private readonly ApplicationDbContext _context;

        public TransacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<TransacaoModel>>> GetTransacoes()
        {
            var response = new ServiceResponse<List<TransacaoModel>>();

            try
            {
                response.Dados = await _context.Transacoes
                    .Include(t => t.Pessoa)
                    .Include(t => t.Categoria)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<TransacaoModel>>> CreateTransacao(TransacaoModel nova)
        {
            var response = new ServiceResponse<List<TransacaoModel>>();

            try
            {
                var pessoa = await _context.Pessoas.FindAsync(nova.PessoaId);
                var categoria = await _context.Categorias.FindAsync(nova.CategoriaId);

                if (pessoa == null || categoria == null)
                {
                    response.Mensagem = "Pessoa ou Categoria não encontrada.";
                    response.Sucesso = false;
                    return response;
                }

                // REGRA 1: Menor de idade só pode ter DESPESA
                if (pessoa.Idade < 18 && nova.Tipo == TipoTransacaoEnum.Receita)
                {
                    response.Mensagem = "Menor de idade não pode ter receita.";
                    response.Sucesso = false;
                    return response;
                }

                // REGRA 2: Categoria precisa ser compatível com tipo
                if (categoria.Finalidade != FinalidadeCategoriaEnum.Ambas &&
                    (int)categoria.Finalidade != (int)nova.Tipo)
                {
                    response.Mensagem = "Categoria incompatível com o tipo da transação.";
                    response.Sucesso = false;
                    return response;
                }

                _context.Transacoes.Add(nova);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Transacoes.ToListAsync();
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