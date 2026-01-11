// Importa a biblioteca axios, que é usada para fazer requisições HTTP
// (GET, POST, PUT, DELETE, etc) para a API backend
import axios from "axios";

// Cria uma instância configurada do axios
// Isso permite que todo o sistema use a mesma configuração de API
export const api = axios.create({
  // URL base da sua API backend (.NET)
  // Todas as requisições feitas usando "api" irão começar com esse endereço
  // Exemplo: api.get("/Pessoa") -> https://localhost:7071/api/Pessoa
  baseURL: "https://localhost:7071/api",
});
