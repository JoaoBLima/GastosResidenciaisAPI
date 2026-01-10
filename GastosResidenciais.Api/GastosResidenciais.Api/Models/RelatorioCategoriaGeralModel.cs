namespace GastosResidenciais.Api.Models
{
    public class RelatorioCategoriaGeralModel
    {
        public List<RelatorioCategoriaModel> Categorias { get; set; }

        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }

        public decimal SaldoGeral => TotalReceitasGeral - TotalDespesasGeral;
    }
}
