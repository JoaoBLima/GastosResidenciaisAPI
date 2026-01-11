using GastosResidenciais.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Modelo que representa uma Transação financeira no sistema.
    /// Registra entradas (receitas) e saídas (despesas) de recursos.
    /// </summary>
    public class TransacaoModel
    {
        /// <summary>
        /// Chave primária da transação. Gerada automaticamente pelo banco de dados.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Descrição da transação. Ex: "Pagamento de conta de luz", "Salário mensal".
        /// Campo obrigatório (non-nullable).
        /// </summary>
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Valor monetário da transação. Sempre positivo.
        /// PRECISÃO: Configurada no DbContext como decimal(18,2).
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação: Despesa (1) ou Receita (2).
        /// Determina se o valor é uma saída ou entrada de recursos.
        /// </summary>
        public TipoTransacaoEnum Tipo { get; set; }

        /// <summary>
        /// ID da pessoa associada a esta transação (chave estrangeira).
        /// </summary>
        public int PessoaId { get; set; }

        /// <summary>
        /// Objeto Pessoa associada (navegação).
        /// JsonIgnore: Geralmente não serializado, mas pode ser carregado via Include() quando necessário.
        /// </summary>
        [JsonIgnore]
        public PessoaModel? Pessoa { get; set; }

        /// <summary>
        /// Data de criação do registro da transação.
        /// </summary>
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// Data da última alteração no registro.
        /// </summary>
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// ID da categoria associada a esta transação (chave estrangeira).
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Objeto Categoria associada (navegação).
        /// JsonIgnore: Geralmente não serializado, mas pode ser carregado via Include() quando necessário.
        /// </summary>
        [JsonIgnore]
        public CategoriaModel? Categoria { get; set; }
    }
}