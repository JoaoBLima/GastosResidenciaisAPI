namespace GastosResidenciais.Api.Models
{
    /// <summary>
    /// Classe genérica para padronização de respostas da API.
    /// Segue o padrão de resposta consistente para todas as operações da API.
    /// </summary>
    /// <typeparam name="T">Tipo dos dados que serão retornados na resposta</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Dados retornados pela operação. Pode ser qualquer tipo (lista, objeto único, etc.).
        /// É nullable para permitir respostas sem dados em caso de erro.
        /// </summary>
        /// <example>
        /// Para GetCategorias(): List<CategoriaModel>
        /// Para GetPessoaById(): PessoaModel
        /// </example>
        public T? Dados { get; set; }

        /// <summary>
        /// Mensagem descritiva sobre o resultado da operação.
        /// Em caso de sucesso: pode conter mensagem informativa ou ficar vazia.
        /// Em caso de erro: deve conter a descrição do problema para debug.
        /// </summary>
        /// <example>
        /// Sucesso: "Categoria criada com sucesso"
        /// Erro: "Erro ao conectar com o banco de dados"
        /// </example>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Indica se a operação foi concluída com sucesso.
        /// Padrão: true (otimista) - assume sucesso até que um erro ocorra.
        /// O front-end pode usar este flag para decidir como tratar a resposta.
        /// </summary>
        public bool Sucesso { get; set; } = true;
    }
}