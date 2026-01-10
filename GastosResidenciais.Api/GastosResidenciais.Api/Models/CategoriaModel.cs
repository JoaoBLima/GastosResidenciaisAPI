using GastosResidenciais.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        [JsonIgnore]
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
