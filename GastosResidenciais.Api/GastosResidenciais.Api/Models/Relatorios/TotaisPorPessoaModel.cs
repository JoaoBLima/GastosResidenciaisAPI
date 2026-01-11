namespace GastosResidenciais.Api.Models.Relatorios
{
    /// <summary>
    /// Modelo que representa os totais financeiros consolidados de uma pessoa específica.
    /// Utilizado no relatório obrigatório "Consulta de totais por pessoa".
    /// </summary>
    public class TotaisPorPessoaModel
    {
        /// <summary>
        /// ID da pessoa analisada. Permite vincular os totais à pessoa original.
        /// </summary>
        public int PessoaId { get; set; }

        /// <summary>
        /// Nome da pessoa para fácil identificação no relatório.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Soma total de todas as transações do tipo Receita associadas a esta pessoa.
        /// Calculado como: Σ(transacao.Valor) onde transacao.Tipo = Receita
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Soma total de todas as transações do tipo Despesa associadas a esta pessoa.
        /// Calculado como: Σ(transacao.Valor) onde transacao.Tipo = Despesa
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo financeiro da pessoa (Receitas - Despesas).
        /// Propriedade calculada automaticamente para garantir consistência.
        /// 
        /// INTERPRETAÇÃO DO SALDO:
        /// - Positivo: Pessoa tem mais receitas que despesas (superávit)
        /// - Negativo: Pessoa tem mais despesas que receitas (déficit)
        /// - Zero: Equilíbrio perfeito entre receitas e despesas
        /// </summary>
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}