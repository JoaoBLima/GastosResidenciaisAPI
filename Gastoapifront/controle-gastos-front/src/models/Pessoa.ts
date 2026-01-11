// Interface que representa uma Pessoa no sistema
export interface Pessoa {
  // Identificador único da pessoa (gerado pelo backend)
  id: number;

  // Nome da pessoa
  nome: string;

  // Idade da pessoa (número inteiro positivo)
  idade: number;
}