using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        [JsonIgnore]
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
