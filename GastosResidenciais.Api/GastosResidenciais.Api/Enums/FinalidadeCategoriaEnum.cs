using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Enums
{
    /// <summary>
    /// Enumeração que define a finalidade de uma categoria de transação.
    /// Determina quais tipos de transações podem usar cada categoria.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FinalidadeCategoriaEnum
    {
        /// <summary>
        /// Categoria exclusiva para transações do tipo Despesa.
        /// Valor numérico: 1
        /// </summary>
        /// <example>
        /// Categoria "Aluguel" só pode ser usada para despesas
        /// </example>
        Despesa = 1,

        /// <summary>
        /// Categoria exclusiva para transações do tipo Receita.
        /// Valor numérico: 2
        /// </summary>
        /// <example>
        /// Categoria "Salário" só pode ser usada para receitas
        /// </example>
        Receita = 2,

        /// <summary>
        /// Categoria que pode ser usada para ambos os tipos de transação.
        /// Valor numérico: 3
        /// </summary>
        /// <example>
        /// Categoria "Diversos" pode ser usada para despesas e receitas
        /// </example>
        Ambas = 3
    }
}