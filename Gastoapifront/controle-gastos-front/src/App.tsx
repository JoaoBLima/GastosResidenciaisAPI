import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import { PessoasPage } from "./pages/PessoasPage";
import { CategoriasPage } from "./pages/CategoriasPage";
import { TransacoesPage } from "./pages/TransacoesPage";
import { RelatorioPessoaPage } from "./pages/RelatorioPessoaPage";
import { RelatorioCategoriaPage } from "./pages/RelatorioCategoriaPage";

/**
 * Componente principal da aplicação
 * Aqui ficam:
 * - O roteador (BrowserRouter)
 * - O menu de navegação
 * - O mapeamento das rotas para cada página
 */
export default function App() {
  return (
    /**
     * BrowserRouter:
     * Habilita navegação por rotas (URLs) no React
     * Ex: /pessoas, /categorias, /transacoes, etc
     */
    <BrowserRouter>
      <div style={{ padding: 20 }}>
        <h1>Controle de Gastos Residenciais</h1>

        {/*
          Menu de navegação da aplicação
          O componente Link troca de página SEM recarregar o site
        */}
        <nav style={{ marginBottom: 20 }}>
          <Link to="/pessoas">Pessoas</Link> |{" "}
          <Link to="/categorias">Categorias</Link> |{" "}
          <Link to="/transacoes">Transações</Link> |{" "}
          <Link to="/relatorio-pessoa">Relatório por Pessoa</Link> |{" "}
          <Link to="/relatorio-categoria">Relatório por Categoria</Link>
        </nav>

        {/*
          Routes:
          Define quais componentes serão exibidos
          dependendo da URL acessada
        */}
        <Routes>
          {/* Página inicial do sistema */}
          <Route path="/" element={<PessoasPage />} />

          {/* Cadastro de pessoas */}
          <Route path="/pessoas" element={<PessoasPage />} />

          {/* Cadastro de categorias */}
          <Route path="/categorias" element={<CategoriasPage />} />

          {/* Cadastro de transações */}
          <Route path="/transacoes" element={<TransacoesPage />} />

          {/* Relatório por pessoa */}
          <Route path="/relatorio-pessoa" element={<RelatorioPessoaPage />} />

          {/* Relatório por categoria */}
          <Route path="/relatorio-categoria" element={<RelatorioCategoriaPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}
