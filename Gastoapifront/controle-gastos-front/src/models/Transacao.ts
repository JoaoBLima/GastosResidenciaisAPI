// Define os possíveis tipos de uma transação financeira
// - "Despesa": valor que diminui o saldo
// - "Receita": valor que aumenta o saldo
export type TipoTransacao = "Despesa" | "Receita";

// Interface que representa uma Transação financeira
export interface Transacao {
  // Identificador único da transação (gerado pelo backend)
  id: number;

  // Descrição da transação (ex: "Compra no mercado", "Salário", etc)
  descricao: string;

  // Valor monetário da transação (número decimal positivo)
  valor: number;

  // Tipo da transação: Despesa ou Receita
  tipo: TipoTransacao;

  // ID da pessoa associada a essa transação
  // Relaciona com o cadastro de pessoas
  pessoaId: number;

  // ID da categoria associada a essa transação
  // Relaciona com o cadastro de categorias
  categoriaId: number;
}