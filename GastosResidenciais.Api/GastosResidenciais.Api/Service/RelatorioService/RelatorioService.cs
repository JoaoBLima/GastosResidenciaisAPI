using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Enums;
using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.Service.RelatorioService
{
    /// <summary>
    /// Implementação do serviço de Relatórios.
    /// Responsável por processar dados agregados e gerar relatórios analíticos.
    /// Contém lógica de agregação e cálculo de totais financeiros.
    /// </summary>
    public class RelatorioService : IRelatorioInterface
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public RelatorioService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Implementação do método para gerar relatório de totais por pessoa.
        /// Processa todas as pessoas e suas transações para calcular totais financeiros.
        /// </summary>
        public async Task<ServiceResponse<RelatorioTotaisPessoaResponse>> GetTotaisPorPessoa()
        {
            ServiceResponse<RelatorioTotaisPessoaResponse> serviceResponse = new();

            try
            {
                // ETAPA 1: CARREGAR DADOS
                // Busca todas as pessoas incluindo suas transações relacionadas
                // Include() é essencial para carregar transações sem consultas adicionais
                var pessoas = await _context.Pessoas
                    .Include(p => p.Transacoes) // Carrega transações de cada pessoa
                    .ToListAsync();

                // ETAPA 2: INICIALIZAR ESTRUTURA DO RELATÓRIO
                RelatorioTotaisPessoaResponse resultado = new();

                // ETAPA 3: PROCESSAR CADA PESSOA E CALCULAR TOTAIS
                foreach (var pessoa in pessoas)
                {
                    // CÁLCULO DE RECEITAS: Soma valores de transações do tipo Receita
                    var totalReceitas = pessoa.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Receita) // Filtra apenas receitas
                        .Sum(t => t.Valor); // Soma os valores

                    // CÁLCULO DE DESPESAS: Soma valores de transações do tipo Despesa
                    var totalDespesas = pessoa.Transacoes
                        .Where(t => t.Tipo == TipoTransacaoEnum.Despesa) // Filtra apenas despesas
                        .Sum(t => t.Valor); // Soma os valores

                    // CÁLCULO DO SALDO: Receitas - Despesas
                    // O saldo é calculado automaticamente no setter da propriedade
                    // ou pode ser calculado aqui se necessário

                    // ADICIONA RESULTADO DA PESSOA À LISTA
                    resultado.TotaisPorPessoa.Add(new TotaisPorPessoaModel
                    {
                        PessoaId = pessoa.Id,
                        Nome = pessoa.Nome,
                        TotalReceitas = totalReceitas,
                        TotalDespesas = totalDespesas
                        // OBSERVAÇÃO: A propriedade Saldo é calculada automaticamente
                        // no modelo TotaisPorPessoaModel (TotalReceitas - TotalDespesas)
                    });
                }

                // ETAPA 4: CALCULAR TOTAIS GERAIS
                // Soma todos os valores de receitas de todas as pessoas
                resultado.TotalGeral.TotalReceitas = resultado.TotaisPorPessoa.Sum(x => x.TotalReceitas);

                // Soma todos os valores de despesas de todas as pessoas
                resultado.TotalGeral.TotalDespesas = resultado.TotaisPorPessoa.Sum(x => x.TotalDespesas);

                // OBSERVAÇÃO: TotalGeral.Saldo é calculado automaticamente no modelo
                // como TotalReceitas - TotalDespesas

                // ETAPA 5: PREPARAR RESPOSTA
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