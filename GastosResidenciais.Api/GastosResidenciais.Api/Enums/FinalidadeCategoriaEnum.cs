using System.Text.Json.Serialization;

namespace GastosResidenciais.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FinalidadeCategoriaEnum
    {
        Despesa = 1,
        Receita = 2,
        Ambas = 3
    }
}
