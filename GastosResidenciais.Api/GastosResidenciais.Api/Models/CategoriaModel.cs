using GastosResidenciais.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Modelo que representa uma Categoria no sistema de controle de gastos.
    /// Define como as transações são classificadas (ex: Alimentação, Transporte, Salário).
    /// </summary>
    public class CategoriaModel
    {
        /// <summary>
        /// Chave primária da categoria. Gerada automaticamente pelo banco de dados.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Descrição da categoria. Ex: "Alimentação", "Transporte", "Lazer".
        /// Campo obrigatório (non-nullable).
        /// </summary>
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Finalidade da categoria, determina quais tipos de transação pode classificar.
        /// Valores: Despesa (1), Receita (2), Ambas (3).
        /// </summary>
        public FinalidadeCategoriaEnum Finalidade { get; set; }

        /// <summary>
        /// Data de criação do registro. Preenchida automaticamente com a data/hora atual.
        /// Usa hora local do servidor.
        /// </summary>
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// Data da última alteração no registro. Atualizada automaticamente em modificações.
        /// </summary>
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();

        /// <summary>
        /// Lista de transações associadas a esta categoria (relacionamento 1:N).
        /// JsonIgnore: Não serializada nas respostas da API para evitar referências circulares.
        /// </summary>
        [JsonIgnore]
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}