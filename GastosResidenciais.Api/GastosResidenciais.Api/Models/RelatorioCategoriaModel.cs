namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Modelo que representa os totais financeiros de uma categoria específica.
    /// Usado no relatório opcional "Consulta de totais por categoria".
    /// </summary>
    public class RelatorioCategoriaModel
    {
        /// <summary>
        /// ID da categoria analisada.
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Descrição da categoria.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Total de receitas (transações do tipo Receita) nesta categoria.
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Total de despesas (transações do tipo Despesa) nesta categoria.
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo da categoria (Receitas - Despesas).
        /// Propriedade calculada automaticamente.
        /// </summary>
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}