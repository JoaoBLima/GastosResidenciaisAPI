namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Modelo que representa o relatório completo de totais por categoria.
    /// Contém análise individual por categoria e totais gerais consolidados.
    /// </summary>
    public class RelatorioCategoriaGeralModel
    {
        /// <summary>
        /// Lista de totais calculados para cada categoria.
        /// </summary>
        public List<RelatorioCategoriaModel> Categorias { get; set; } = new();

        /// <summary>
        /// Total geral de receitas de todas as categorias.
        /// </summary>
        public decimal TotalReceitasGeral { get; set; }

        /// <summary>
        /// Total geral de despesas de todas as categorias.
        /// </summary>
        public decimal TotalDespesasGeral { get; set; }

        /// <summary>
        /// Saldo geral consolidado (TotalReceitasGeral - TotalDespesasGeral).
        /// Propriedade calculada automaticamente.
        /// </summary>
        public decimal SaldoGeral => TotalReceitasGeral - TotalDespesasGeral;
    }
}