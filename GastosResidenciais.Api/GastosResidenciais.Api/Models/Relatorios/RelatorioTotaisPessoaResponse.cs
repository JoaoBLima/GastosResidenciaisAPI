namespace GastosResidenciais.Api.Models.Relatorios
{
    /// <summary>
    /// Modelo de resposta completo para o relatório "Totais por Pessoa".
    /// Atende ao requisito obrigatório da especificação.
    /// 
    /// REQUISITO ATENDIDO (da especificação):
    /// "Deverá listar todas as pessoas cadastradas, exibindo o total de receitas,
    /// despesas e o saldo (receita – despesa) de cada uma.
    /// Ao final da listagem anterior, deverá exibir o total geral de todas as
    /// pessoas incluindo o total de receitas, total de despesas e o saldo líquido."
    /// </summary>
    public class RelatorioTotaisPessoaResponse
    {
        /// <summary>
        /// Lista detalhada com os totais financeiros de cada pessoa individualmente.
        /// Cada item representa uma pessoa com seus respectivos cálculos.
        /// 
        /// ESTRUTURA ESPERADA NO FRONT-END:
        /// [
        ///   { PessoaId: 1, Nome: "João", TotalReceitas: 5000, TotalDespesas: 3000, Saldo: 2000 },
        ///   { PessoaId: 2, Nome: "Maria", TotalReceitas: 4000, TotalDespesas: 3500, Saldo: 500 },
        ///   ...
        /// ]
        /// </summary>
        public List<TotaisPorPessoaModel> TotaisPorPessoa { get; set; } = new();

        /// <summary>
        /// Totais consolidados de todas as pessoas juntas.
        /// Fornece uma visão macro da situação financeira do residencial.
        /// 
        /// USO TÍPICO: Exibido após a lista individual, como um resumo final.
        /// Exemplo: "Total Geral: Receitas R$ 9.000,00 | Despesas R$ 6.500,00 | Saldo R$ 2.500,00"
        /// </summary>
        public TotalGeralModel TotalGeral { get; set; } = new();
    }
}