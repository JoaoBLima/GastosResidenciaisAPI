namespace GastosResidenciais.Api.Models.Relatorios
{
    /// <summary>
    /// Modelo que representa os totais financeiros consolidados de TODO o sistema.
    /// Agrega os valores de todas as pessoas para uma visão geral do residencial.
    /// </summary>
    public class TotalGeralModel
    {
        /// <summary>
        /// Soma total de TODAS as receitas de TODAS as pessoas do sistema.
        /// Calculado como: Σ(pessoa.TotalReceitas) para todas as pessoas
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Soma total de TODAS as despesas de TODAS as pessoas do sistema.
        /// Calculado como: Σ(pessoa.TotalDespesas) para todas as pessoas
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo financeiro geral do residencial.
        /// Representa a saúde financeira coletiva de todas as pessoas.
        /// 
        /// USO NO FRONT-END: Pode ser exibido como:
        /// - "Saldo do Residencial: R$ X,XX"
        /// - Com cor verde (positivo) ou vermelho (negativo)
        /// - Como indicador de performance financeira geral
        /// </summary>
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}