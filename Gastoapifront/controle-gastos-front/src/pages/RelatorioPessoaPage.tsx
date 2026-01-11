import { useEffect, useState } from "react";
import { api } from "../api/api";

/**
 * Representa uma linha do relatório por pessoa
 * Cada pessoa terá o total de receitas, despesas e o saldo
 */
type RelatorioPessoaItem = {
  pessoaId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
};

/**
 * Representa o total geral somando todas as pessoas
 */
type TotalGeral = {
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
};

/**
 * Página de Relatório de Totais por Pessoa
 * Mostra:
 * - Quanto cada pessoa teve de receitas e despesas
 * - O saldo de cada pessoa
 * - O total geral do sistema
 */
export function RelatorioPessoaPage() {
  // Lista com os dados do relatório por pessoa
  const [lista, setLista] = useState<RelatorioPessoaItem[]>([]);

  // Totais gerais (somando todas as pessoas)
  const [totalGeral, setTotalGeral] = useState<TotalGeral | null>(null);

  // Mensagem de erro caso a API falhe
  const [erro, setErro] = useState<string | null>(null);

  /**
   * Busca os dados do relatório na API
   */
  async function carregar() {
    try {
      // Chama a API
      const res = await api.get("/Relatorio/totais-por-pessoa");

      // Pega somente o "dados" da resposta
      const dados = res.data?.dados;

      // Se não vier nada, mostra erro
      if (!dados) {
        setErro("Erro ao carregar relatório.");
        return;
      }

      // Preenche a lista com os totais por pessoa
      setLista(dados.totaisPorPessoa || []);

      // Preenche o total geral
      setTotalGeral(dados.totalGeral || null);
    } catch (e) {
      console.error(e);
      setErro("Erro ao carregar relatório.");
    }
  }

  /**
   * useEffect executa automaticamente quando a tela abre
   * Aqui ele chama o carregar() uma única vez
   */
  useEffect(() => {
    const executar = async () => {
      await carregar();
    };
    executar();
  }, []);

  return (
    <div style={styles.page}>
      <div style={styles.card}>
        <h2 style={styles.title}>Relatório por Pessoa</h2>

        {/* Mostra erro, se existir */}
        {erro && <div style={styles.error}>{erro}</div>}

        {/* Se não houver dados e nem erro, mostra mensagem */}
        {lista.length === 0 && !erro && (
          <p>Nenhum dado para exibir.</p>
        )}

        {/* Lista os totais por pessoa */}
        {lista.map((p) => (
          <div key={p.pessoaId} style={styles.listItem}>
            <div>
              <strong>{p.nome}</strong>
              <div style={{ fontSize: 12, color: "#666" }}>
                Receitas: R$ {p.totalReceitas} | Despesas: R$ {p.totalDespesas}
              </div>
            </div>

            {/* Saldo com cor verde se positivo e vermelho se negativo */}
            <div
              style={{
                fontWeight: "bold",
                color: p.saldo >= 0 ? "#166534" : "#991b1b",
              }}
            >
              R$ {p.saldo}
            </div>
          </div>
        ))}
      </div>

      {/* Card do total geral (só aparece se existir) */}
      {totalGeral && (
        <div style={styles.card}>
          <h3>Total Geral</h3>

          <div style={styles.totalBox}>
            <div>
              <strong>Receitas</strong>
              <div>R$ {totalGeral.totalReceitas}</div>
            </div>

            <div>
              <strong>Despesas</strong>
              <div>R$ {totalGeral.totalDespesas}</div>
            </div>

            <div>
              <strong>Saldo</strong>
              <div
                style={{
                  fontWeight: "bold",
                  color: totalGeral.saldo >= 0 ? "#166534" : "#991b1b",
                }}
              >
                R$ {totalGeral.saldo}
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

/**
 * Estilos da página (CSS em JS)
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
  title: {
    marginBottom: 15,
  },
  listItem: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    padding: "12px 0",
    borderBottom: "1px solid #eee",
  },
  error: {
    background: "#fee2e2",
    color: "#991b1b",
    padding: 10,
    borderRadius: 6,
    marginBottom: 10,
  },
  totalBox: {
    display: "flex",
    gap: 40,
    flexWrap: "wrap",
  },
};
