using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.PessoaService
{
    public class PessoaService : IPessoaInterface
    {
        private readonly ApplicationDbContext _context;

        public PessoaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<PessoaModel>>> GetPessoas()
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                response.Dados = await _context.Pessoas.ToListAsync();

                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhuma pessoa cadastrada.";
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        public async Task<ServiceResponse<PessoaModel>> GetPessoaById(int id)
        {
            var response = new ServiceResponse<PessoaModel>();

            try
            {
                var pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

                if (pessoa == null)
                {
                    response.Mensagem = "Pessoa não encontrada.";
                    response.Sucesso = false;
                    return response;
                }

                response.Dados = pessoa;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<PessoaModel>>> CreatePessoa(PessoaModel novaPessoa)
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                if (novaPessoa.Idade < 0)
                {
                    response.Mensagem = "Idade inválida.";
                    response.Sucesso = false;
                    return response;
                }

                _context.Pessoas.Add(novaPessoa);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Pessoas.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<PessoaModel>>> DeletePessoa(int id)
        {
            var response = new ServiceResponse<List<PessoaModel>>();

            try
            {
                var pessoa = await _context.Pessoas
                    .Include(p => p.Transacoes)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pessoa == null)
                {
                    response.Mensagem = "Pessoa não encontrada.";
                    response.Sucesso = false;
                    return response;
                }

                // REGRA: ao deletar pessoa, apaga todas as transações dela
                _context.Transacoes.RemoveRange(pessoa.Transacoes);
                _context.Pessoas.Remove(pessoa);

                await _context.SaveChangesAsync();

                response.Dados = await _context.Pessoas.ToListAsync();
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
