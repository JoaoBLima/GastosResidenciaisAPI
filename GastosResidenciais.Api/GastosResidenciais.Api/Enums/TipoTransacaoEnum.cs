using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Enums
{
    /// <summary>
    /// Enumeração que define os tipos possíveis de uma transação financeira.
    /// Utilizado para classificar transações como entrada (receita) ou saída (despesa).
    /// </summary>
    /// <remarks>
    /// ATRIBUTO JsonConverter: Configura a serialização/deserialização como strings
    /// em vez de números, melhorando a legibilidade nas APIs JSON.
    /// </remarks>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoTransacaoEnum
    {
        /// <summary>
        /// Representa uma transação de saída de recursos (gasto).
        /// Valor numérico: 1
        /// </summary>
        /// <example>
        /// Pagamento de conta de luz, compra de supermercado
        /// </example>
        Despesa = 1,

        /// <summary>
        /// Representa uma transação de entrada de recursos (ganho).
        /// Valor numérico: 2
        /// </summary>
        /// <example>
        /// Salário, rendimentos de investimentos
        /// </example>
        Receita = 2
    }
}