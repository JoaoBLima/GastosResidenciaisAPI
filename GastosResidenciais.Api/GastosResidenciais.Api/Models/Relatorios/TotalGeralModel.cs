namespace GastosResidenciais.Api.Models.Relatorios
{
    public class TotalGeralModel
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}
