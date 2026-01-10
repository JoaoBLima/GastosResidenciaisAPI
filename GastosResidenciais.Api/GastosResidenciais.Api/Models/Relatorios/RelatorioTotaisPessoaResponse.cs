namespace GastosResidenciais.Api.Models.Relatorios
{
    public class RelatorioTotaisPessoaResponse
    {
        public List<TotaisPorPessoaModel> TotaisPorPessoa { get; set; } = new();
        public TotalGeralModel TotalGeral { get; set; } = new();
    }
}
