using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoTransacaoEnum
    {
        Despesa = 1,
        Receita = 2
    }
}
