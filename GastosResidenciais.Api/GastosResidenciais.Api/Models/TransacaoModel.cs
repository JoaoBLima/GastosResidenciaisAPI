using GastosResidenciais.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    public class TransacaoModel
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacaoEnum Tipo { get; set; }

        public int PessoaId { get; set; }
        [JsonIgnore]
        public PessoaModel? Pessoa { get; set; } 
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public CategoriaModel? Categoria { get; set; } 
    }
}
