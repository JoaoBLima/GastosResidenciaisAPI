using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Modelo que representa uma Pessoa no sistema de controle de gastos.
    /// Cada pessoa pode ter múltiplas transações associadas.
    /// </summary>
    public class PessoaModel
    {
        /// <summary>
        /// Chave primária da pessoa. Gerada automaticamente pelo banco de dados.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome completo da pessoa. Ex: "João Silva".
        /// Campo obrigatório (non-nullable).
        /// </summary>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Idade da pessoa em anos. Usada para validação de regras de negócio.
        /// REGRA: Menores de idade (idade < 18) só podem ter transações do tipo Despesa.
        /// </summary>
        public int Idade { get; set; }

        /// <summary>
        /// Data de criação do registro. Preenchida automaticamente.
        /// </summary>
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// Data da última alteração no registro.
        /// </summary>
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// Lista de transações associadas a esta pessoa (relacionamento 1:N).
        /// JsonIgnore: Não serializada nas respostas da API para evitar referências circulares.
        /// </summary>
        [JsonIgnore]
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}