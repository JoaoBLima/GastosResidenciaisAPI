// Importa hooks do React para controle de estado e ciclo de vida do componente
import { useEffect, useState } from "react";

// Importa a instância do Axios já configurada para acessar a Web API
import { api } from "../api/api";

// Importa os tipos que representam os contratos de dados com o backend
import type { Transacao } from "../models/Transacao";
import type { Pessoa } from "../models/Pessoa";
import type { Categoria } from "../models/Categoria";

// Componente responsável pela tela de cadastro e listagem de transações financeiras
export function TransacoesPage() {

  // ============================
  // Estados da aplicação
  // ============================

  // Lista de transações já cadastradas
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

  // Listas auxiliares usadas nos selects
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  // Estados do formulário de cadastro
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState(0);
  const [tipo, setTipo] = useState("Despesa"); // "Despesa" ou "Receita"
  const [pessoaId, setPessoaId] = useState(0);
  const [categoriaId, setCategoriaId] = useState(0);

  // Estados para feedback ao usuário
  const [mensagem, setMensagem] = useState<string | null>(null);
  const [erro, setErro] = useState<string | null>(null);

  /**
   * Função responsável por carregar:
   * - As transações existentes
   * - As pessoas cadastradas
   * - As categorias cadastradas
   * Tudo isso é necessário para preencher a tela e os selects do formulário.
   */
  async function carregar() {
    const t = await api.get("/Transacao");
    const p = await api.get("/Pessoa");
    const c = await api.get("/Categoria");

    setTransacoes(t.data.dados);
    setPessoas(p.data.dados);
    setCategorias(c.data.dados);
  }

  /**
   * Função responsável por salvar uma nova transação.
   * Aqui ficam concentradas TODAS as regras de negócio exigidas no trabalho.
   */
  async function salvar() {
    // Limpa mensagens anteriores
    setMensagem(null);
    setErro(null);

    // ============================
    // Validações básicas de formulário
    // ============================

    if (!descricao.trim()) {
      setErro("Informe a descrição.");
      return;
    }

    if (valor <= 0) {
      setErro("O valor deve ser maior que zero.");
      return;
    }

    if (pessoaId === 0) {
      setErro("Selecione uma pessoa.");
      return;
    }

    if (categoriaId === 0) {
      setErro("Selecione uma categoria.");
      return;
    }

    // ============================
    // 🔴 REGRAS DE NEGÓCIO
    // ============================

    // Busca a pessoa selecionada
    const pessoa = pessoas.find(p => p.id === pessoaId);
    if (!pessoa) {
      setErro("Pessoa inválida.");
      return;
    }

    // Busca a categoria selecionada
    const categoria = categorias.find(c => c.id === categoriaId);
    if (!categoria) {
      setErro("Categoria inválida.");
      return;
    }

    /**
     * ✅ Regra 1:
     * Se a pessoa for menor de idade (< 18),
     * ela só pode cadastrar transações do tipo DESPESA.
     */
    if (pessoa.idade < 18 && tipo === "Receita") {
      setErro("Menor de idade não pode cadastrar receita.");
      return;
    }

    /**
     * ✅ Regra 2:
     * O tipo da transação deve ser compatível com a finalidade da categoria.
     * Exemplo:
     * - Se a transação é "Despesa", não pode usar categoria de "Receita".
     */
    if (categoria.finalidade !== tipo && categoria.finalidade !== "Ambas") {
      setErro("O tipo da transação não é compatível com a categoria selecionada.");
      return;
    }

    // ============================

    try {
      // Envia os dados para a API
      await api.post("/Transacao", {
        descricao,
        valor,
        tipo,
        pessoaId,
        categoriaId,
      });

      // Limpa o formulário após salvar
      setDescricao("");
      setValor(0);
      setPessoaId(0);
      setCategoriaId(0);

      // Exibe mensagem de sucesso
      setMensagem("Transação cadastrada com sucesso!");

      // Recarrega os dados da tela
      carregar();
    } catch (e) {
      console.error(e);
      setErro("Erro ao salvar transação.");
    }
  }

  /**
   * Hook executado quando a página é carregada.
   * Ele busca todos os dados necessários para a tela.
   */
  useEffect(() => {
    const executar = async () => {
      await carregar();
    };
    executar();
  }, []);

  // ============================
  // Renderização da interface
  // ============================
  return (
    <div style={styles.page}>
      <div style={styles.card}>
        <h2 style={styles.title}>Cadastro de Transações</h2>

        {/* Exibição de mensagens */}
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

          <input
            style={styles.input}
            type="number"
            placeholder="Valor"
            value={valor}
            onChange={e => setValor(Number(e.target.value))}
          />

          <select
            style={styles.input}
            value={tipo}
            onChange={e => setTipo(e.target.value)}
          >
            <option value="Despesa">Despesa</option>
            <option value="Receita">Receita</option>
          </select>

          <select
            style={styles.input}
            value={pessoaId}
            onChange={e => setPessoaId(Number(e.target.value))}
          >
            <option value={0}>Selecione a pessoa</option>
            {pessoas.map(p => (
              <option key={p.id} value={p.id}>
                {p.nome}
              </option>
            ))}
          </select>

          <select
            style={styles.input}
            value={categoriaId}
            onChange={e => setCategoriaId(Number(e.target.value))}
          >
            <option value={0}>Selecione a categoria</option>
            {categorias.map(c => (
              <option key={c.id} value={c.id}>
                {c.descricao}
              </option>
            ))}
          </select>

          <button style={styles.button} onClick={salvar}>
            Salvar
          </button>
        </div>
      </div>

      {/* Lista de transações */}
      <div style={styles.listCard}>
        <h3>Lista de Transações</h3>

        {transacoes.length === 0 && <p>Nenhuma transação cadastrada.</p>}

        {transacoes.map(t => (
          <div key={t.id} style={styles.listItem}>
            <div>
              <strong>{t.descricao}</strong>
              <div style={{ fontSize: 12, color: "#666" }}>
                {t.tipo} — R$ {t.valor}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

/**
 * Estilos da página utilizando CSS-in-JS
 */
const styles: { [key: string]: React.CSSProperties } = {
  page: {
    maxWidth: 900,
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
