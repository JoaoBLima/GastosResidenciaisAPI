// Define os possíveis valores para a finalidade de uma categoria
// - "Despesa": categoria usada apenas para despesas
// - "Receita": categoria usada apenas para receitas
// - "Ambas": categoria que pode ser usada para os dois tipos
export type Finalidade = "Despesa" | "Receita" | "Ambas";

// Interface que representa uma Categoria no front-end
// Esse formato deve espelhar exatamente o DTO retornado pela API
export interface Categoria {
  // Identificador único da categoria (gerado pelo backend)
  id: number;

  // Descrição/nome da categoria (ex: "Alimentação", "Salário", etc)
  descricao: string;

  // Define se a categoria pode ser usada para despesa, receita ou ambas
  finalidade: Finalidade;
}