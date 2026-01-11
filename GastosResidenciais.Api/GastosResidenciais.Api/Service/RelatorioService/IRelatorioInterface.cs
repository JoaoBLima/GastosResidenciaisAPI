using GastosResidenciais.Api.Models;
using GastosResidenciais.Api.Models.Relatorios;

namespace GastosResidenciais.Api.Service.RelatorioService
{
    /// <summary>
    /// Interface que define o contrato para operações de relatórios.
    /// Foca em consultas complexas e agregações de dados para análise.
    /// </summary>
    public interface IRelatorioInterface
    {
        /// <summary>
        /// Gera relatório de totais financeiros agrupados por pessoa.
        /// Calcula receitas, despesas e saldo para cada pessoa e totais gerais.
        /// </summary>
        /// <returns>
        /// ServiceResponse contendo o relatório completo com:
        /// - Lista de totais por pessoa
        /// - Totais gerais consolidados
        /// </returns>
        /// <remarks>
        /// Este relatório atende ao requisito obrigatório do sistema:
        /// "Consulta de totais por pessoa" que deve listar todas as pessoas
        /// com total de receitas, despesas e saldo, além do total geral.
        /// </remarks>
        Task<ServiceResponse<RelatorioTotaisPessoaResponse>> GetTotaisPorPessoa();
    }
}