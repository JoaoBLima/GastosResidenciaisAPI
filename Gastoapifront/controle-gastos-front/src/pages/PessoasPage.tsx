// Importa os hooks do React para controle de estado e ciclo de vida do componente
import { useEffect, useState } from "react";

// Importa a instância do Axios configurada para acessar a Web API
import { api } from "../api/api";

// Importa o tipo Pessoa, que representa o contrato de dados com o backend
import type { Pessoa } from "../models/Pessoa";

// Componente responsável pela tela de cadastro e gerenciamento de pessoas
export function PessoasPage() {

  // Estado que armazena a lista de pessoas vindas da API
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  // Estados do formulário de cadastro
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState(0);

  // Estados para exibição de mensagens ao usuário
  const [mensagem, setMensagem] = useState<string | null>(null);
  const [erro, setErro] = useState<string | null>(null);

  /**
   * Função responsável por buscar todas as pessoas cadastradas na API
   * e atualizar o estado da tela.
   */
  async function carregar() {
    const res = await api.get("/Pessoa");
    // A API retorna os dados dentro da propriedade "dados"
    setPessoas(res.data.dados);
  }

  /**
   * Função responsável por cadastrar uma nova pessoa.
   * Também executa validações básicas antes de enviar para o backend.
   */
  async function salvar() {
    // Limpa mensagens anteriores
    setMensagem(null);
    setErro(null);

    // Validação: não permite nome vazio
    if (!nome.trim()) {
      setErro("Informe o nome.");
      return;
    }

    // Validação: idade deve ser um número inteiro positivo
    if (idade <= 0 || !Number.isInteger(idade)) {
      setErro("Informe uma idade válida.");
      return;
    }

    try {
      // Envia os dados para a API
      await api.post("/Pessoa", { nome, idade });

      // Limpa o formulário após salvar
      setNome("");
      setIdade(0);

      // Exibe mensagem de sucesso
      setMensagem("Pessoa cadastrada com sucesso!");

      // Recarrega a lista de pessoas
      carregar();
    } catch (e) {
      console.error(e);
      setErro("Erro ao cadastrar pessoa.");
    }
  }

  /**
   * Função responsável por excluir uma pessoa.
   * Regra de negócio importante:
   * Ao excluir uma pessoa, o backend também remove todas as transações associadas a ela.
   */
  async function excluir(id: number) {
    try {
      setMensagem(null);
      setErro(null);

      // Chamada para a API para excluir a pessoa pelo ID
      await api.delete(`/Pessoa/${id}`);

      setMensagem("Pessoa excluída com sucesso!");

      // Recarrega a lista após exclusão
      carregar();
    } catch (e) {
      console.error(e);
      setErro("Erro ao excluir pessoa.");
    }
  }

  /**
   * Hook executado quando a página é carregada.
   * Ele busca automaticamente a lista de pessoas cadastradas.
   */
  useEffect(() => {
    const executar = async () => {
      await carregar();
    };
    executar();
  }, []);

  // Renderização da interface
  return (
    <div style={styles.page}>

      {/* Card de cadastro */}
      <div style={styles.card}>
        <h2 style={styles.title}>Cadastro de Pessoas</h2>

        {/* Exibição de mensagens de sucesso ou erro */}
        {mensagem && <div style={styles.success}>{mensagem}</div>}
        {erro && <div style={styles.error}>{erro}</div>}

        {/* Formulário de cadastro */}
        <div style={styles.form}>
          <input
            style={styles.input}
            placeholder="Nome"
            value={nome}
            onChange={e => setNome(e.target.value)}
          />

          <input
            style={styles.input}
            type="number"
            placeholder="Idade"
            value={idade}
            onChange={e => setIdade(Number(e.target.value))}
          />

          <button style={styles.button} onClick={salvar}>
            Salvar
          </button>
        </div>
      </div>

      {/* Card de listagem */}
      <div style={styles.listCard}>
        <h3>Lista de Pessoas</h3>

        {/* Caso não exista nenhuma pessoa cadastrada */}
        {pessoas.length === 0 && <p>Nenhuma pessoa cadastrada.</p>}

        {/* Renderização da lista de pessoas */}
        {pessoas.map(p => (
          <div key={p.id} style={styles.listItem}>
            <div>
              <strong>{p.nome}</strong>
              <div style={{ fontSize: 12, color: "#666" }}>
                {p.idade} anos
              </div>
            </div>

            <button
              style={styles.deleteButton}
              onClick={() => excluir(p.id)}
            >
              Excluir
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

/**
 * Estilos da página utilizando CSS-in-JS.
 * Essa abordagem simplifica a organização do projeto e evita dependência de arquivos CSS externos.
 */
const styles: { [key: string]: React.CSSProperties } = {
  page: {
    maxWidth: 800,
    margin: "0 auto",
    padding: 20,
    fontFamily: "Arial, sans-serif",
  },
  card: {
    background: "#fff",
    padding: 20,
    borderRadius: 8,
    boxShadow: "0 2px 8px rgba(0,0,0,0.1)",
    marginBottom: 20,
  },
  listCard: {
    background: "#fff",
    padding: 20,
    borderRadius: 8,
    boxShadow: "0 2px 8px rgba(0,0,0,0.1)",
  },
  title: {
    marginBottom: 15,
  },
  form: {
    display: "flex",
    gap: 10,
    flexWrap: "wrap",
  },
  input: {
    flex: "1 1 200px",
    padding: "10px",
    borderRadius: 6,
    border: "1px solid #ccc",
    fontSize: 14,
  },
  button: {
    padding: "10px 20px",
    borderRadius: 6,
    border: "none",
    background: "#16a34a",
    color: "#fff",
    cursor: "pointer",
    fontWeight: "bold",
  },
  deleteButton: {
    padding: "6px 12px",
    borderRadius: 6,
    border: "none",
    background: "#dc2626",
    color: "#fff",
    cursor: "pointer",
  },
  listItem: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    padding: "10px 0",
    borderBottom: "1px solid #eee",
  },
  success: {
    background: "#dcfce7",
    color: "#166534",
    padding: 10,
    borderRadius: 6,
    marginBottom: 10,
  },
  error: {
    background: "#fee2e2",
    color: "#991b1b",
    padding: 10,
    borderRadius: 6,
    marginBottom: 10,
  },
};
