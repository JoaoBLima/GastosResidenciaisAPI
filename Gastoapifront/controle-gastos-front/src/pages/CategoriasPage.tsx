// Importa os hooks do React para controle de estado e ciclo de vida do componente
import { useEffect, useState } from "react";

// Importa a instância do Axios configurada para acessar a Web API
import { api } from "../api/api";

// Importa o tipo Categoria, que representa o contrato de dados com o backend
import type { Categoria } from "../models/Categoria";

// Componente responsável pela tela de cadastro e listagem de categorias
export function CategoriasPage() {

  // Estado que armazena a lista de categorias vindas da API
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  // Estados do formulário de cadastro
  const [descricao, setDescricao] = useState("");
  const [finalidade, setFinalidade] = useState("Despesa");

  // Estados para exibição de mensagens ao usuário
  const [mensagem, setMensagem] = useState<string | null>(null);
  const [erro, setErro] = useState<string | null>(null);

  /**
   * Função responsável por buscar todas as categorias cadastradas na API
   * e atualizar o estado da tela.
   */
  async function carregar() {
    const res = await api.get("/Categoria");
    // A API retorna os dados dentro da propriedade "dados"
    setCategorias(res.data.dados);
  }

  /**
   * Função responsável por salvar uma nova categoria.
   * Também executa validações básicas antes de enviar para o backend.
   */
  async function salvar() {
    // Limpa mensagens anteriores
    setMensagem(null);
    setErro(null);

    // Validação: não permite descrição vazia
    if (!descricao.trim()) {
      setErro("Informe a descrição.");
      return;
    }

    try {
      // Envia os dados para a API
      await api.post("/Categoria", { descricao, finalidade });

      // Limpa o formulário após salvar
      setDescricao("");
      setFinalidade("Despesa");

      // Exibe mensagem de sucesso
      setMensagem("Categoria cadastrada com sucesso!");

      // Recarrega a lista de categorias
      carregar();
    } catch (e) {
      console.error(e);
      setErro("Erro ao cadastrar categoria.");
    }
  }

  /**
   * Hook executado quando a página é carregada.
   * Ele busca a lista de categorias automaticamente ao abrir a tela.
   */
  useEffect(() => {
    const executar = async () => {
      await carregar();
    };
    executar();
  }, []);

  // Renderização da tela
  return (
    <div style={styles.page}>

      {/* Card de cadastro */}
      <div style={styles.card}>
        <h2 style={styles.title}>Cadastro de Categorias</h2>

        {/* Exibição de mensagens de sucesso ou erro */}
        {mensagem && <div style={styles.success}>{mensagem}</div>}
        {erro && <div style={styles.error}>{erro}</div>}

        {/* Formulário */}
        <div style={styles.form}>
          <input
            style={styles.input}
            placeholder="Descrição"
            value={descricao}
            onChange={e => setDescricao(e.target.value)}
          />

          <select
            style={styles.input}
            value={finalidade}
            onChange={e => setFinalidade(e.target.value)}
          >
            <option value="Despesa">Despesa</option>
            <option value="Receita">Receita</option>
            <option value="Ambas">Ambas</option>
          </select>

          <button style={styles.button} onClick={salvar}>
            Salvar
          </button>
        </div>
      </div>

      {/* Card de listagem */}
      <div style={styles.listCard}>
        <h3>Lista de Categorias</h3>

        {/* Caso não exista nenhuma categoria */}
        {categorias.length === 0 && <p>Nenhuma categoria cadastrada.</p>}

        {/* Renderização da lista de categorias */}
        {categorias.map(c => (
          <div key={c.id} style={styles.listItem}>
            <div>
              <strong>{c.descricao}</strong>
              <div style={{ fontSize: 12, color: "#666" }}>
                Finalidade: {c.finalidade}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

/**
 * Objeto de estilos da página.
 * Foi utilizado CSS-in-JS para simplificar e evitar dependência de arquivos CSS externos.
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
    background: "#fff",
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
