// Importa hooks do React para controle de estado e ciclo de vida do componente
import { useEffect, useState } from "react";

// Importa a instância do Axios já configurada para acessar a Web API
import { api } from "../api/api";

/**
 * Representa cada linha do relatório por categoria retornado pela API.
 * Cada categoria possui seus próprios totais de receitas, despesas e saldo.
 */
interface CategoriaRelatorio {
  categoriaId: number;
  descricao: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

/**
 * Representa o resumo geral considerando TODAS as categorias.
 */
interface TotaisGerais {
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
}

/**
 * Tela responsável por exibir o relatório financeiro agrupado por categoria.
 * 
 * Para cada categoria, o sistema exibe:
 * - Total de receitas
 * - Total de despesas
 * - Saldo (receitas - despesas)
 * 
 * Ao final, também exibe o TOTAL GERAL considerando todas as categorias.
 */
export function RelatorioCategoriaPage() {

  // ============================
  // Estados da aplicação
  // ============================

  // Lista de categorias com seus totais calculados
  const [categorias, setCategorias] = useState<CategoriaRelatorio[]>([]);

  // Totais gerais do sistema (somatório de todas as categorias)
  const [totais, setTotais] = useState<TotaisGerais | null>(null);

  // Mensagem de erro, caso ocorra algum problema na API
  const [erro, setErro] = useState<string | null>(null);

  /**
   * Função responsável por buscar os dados do relatório na API.
   * 
   * Endpoint esperado:
   * GET /RelatorioCategoria
   * 
   * Estrutura esperada da resposta:
   * {
   *   dados: {
   *     categorias: [...],
   *     totalReceitasGeral: number,
   *     totalDespesasGeral: number,
   *     saldoGeral: number
   *   }
   * }
   */
  async function carregar() {
    try {
      const res = await api.get("/RelatorioCategoria");
      const dados = res.data?.dados;

      // Validação básica para garantir que a API retornou o formato esperado
      if (!dados || !Array.isArray(dados.categorias)) {
        setErro("Erro ao carregar relatório.");
        return;
      }

      // Atualiza a lista de categorias com seus totais
      setCategorias(dados.categorias);

      // Atualiza os totais gerais
      setTotais({
        totalReceitasGeral: dados.totalReceitasGeral,
        totalDespesasGeral: dados.totalDespesasGeral,
        saldoGeral: dados.saldoGeral,
      });

    } catch (e) {
      console.error(e);
      setErro("Erro ao carregar relatório.");
    }
  }

  /**
   * Hook executado quando a página é carregada.
   * Ele busca os dados do relatório diretamente da API.
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
        <h2 style={styles.title}>Relatório por Categoria</h2>

        {/* Exibe mensagem de erro, se existir */}
        {erro && <div style={styles.error}>{erro}</div>}

        {/* Caso não exista nenhum dado e não haja erro */}
        {categorias.length === 0 && !erro && (
          <p>Nenhum dado para exibir.</p>
        )}

        {/* Lista de categorias com seus totais */}
        {categorias.map((c) => (
          <div key={c.categoriaId} style={styles.listItem}>
            <div>
              <strong>{c.descricao}</strong>
              <div style={{ fontSize: 12, color: "#666" }}>
                Receitas: R$ {c.totalReceitas} | Despesas: R$ {c.totalDespesas}
              </div>
            </div>

            {/* Saldo com cor dinâmica:
                Verde = positivo
                Vermelho = negativo */}
            <div
              style={{
                fontWeight: "bold",
                color: c.saldo >= 0 ? "#166534" : "#991b1b",
              }}
            >
              R$ {c.saldo}
            </div>
          </div>
        ))}
      </div>

      {/* Card de totais gerais */}
      {totais && (
        <div style={styles.card}>
          <h3>Total Geral</h3>

          <div style={styles.totalBox}>
            <div>
              <strong>Receitas</strong>
              <div>R$ {totais.totalReceitasGeral}</div>
            </div>

            <div>
              <strong>Despesas</strong>
              <div>R$ {totais.totalDespesasGeral}</div>
            </div>

            <div>
              <strong>Saldo</strong>
              <div
                style={{
                  fontWeight: "bold",
                  color: totais.saldoGeral >= 0 ? "#166534" : "#991b1b",
                }}
              >
                R$ {totais.saldoGeral}
              </div>
            </div>
          </div>
        </div>
      )}
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
