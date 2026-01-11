# 💰 Controle de Gastos Residenciais

Aplicação web para controle de gastos residenciais, permitindo o cadastro de pessoas, categorias e transações (receitas e despesas), além da visualização de relatórios consolidados por pessoa e por categoria.


---

## 🧠 Funcionalidades

### 👤 Pessoas
- Cadastro de pessoas
- Validação de nome e idade
- Exclusão de pessoas
- Listagem de pessoas cadastradas

### 🗂️ Categorias
- Cadastro de categorias com finalidade:
  - Despesa
  - Receita
  - Ambas
- Validação de descrição
- Listagem de categorias

### 💳 Transações
- Cadastro de transações financeiras
- Campos:
  - Descrição
  - Valor
  - Tipo (Despesa ou Receita)
  - Pessoa
  - Categoria
- Regras de negócio:
  - ✅ Menores de idade **não podem cadastrar receitas**
  - ✅ O tipo da transação deve ser **compatível com a finalidade da categoria**
- Validações completas no front-end
- Listagem de transações

### 📊 Relatórios

#### 📄 Relatório por Pessoa
- Total de receitas por pessoa
- Total de despesas por pessoa
- Saldo por pessoa
- Total geral consolidado

#### 📄 Relatório por Categoria
- Total de receitas por categoria
- Total de despesas por categoria
- Saldo por categoria
- Total geral consolidado

---

## 🛠️ Tecnologias Utilizadas

### Front-end
- React
- TypeScript
- Axios
- React Router DOM

### Back-end
- API REST em .NET 
- Sql Server para persistência de dados.
---

## 🧱 Arquitetura do Front-end

- Organização por páginas
- Separação de responsabilidades
- Models tipados com TypeScript
- Serviço centralizado de API (`api.ts`)
- Validações feitas no front-end antes de enviar para o back-end
- Regras de negócio aplicadas no front-end

---

## 🚀 Como executar o projeto

### Pré-requisitos
- Node.js instalado
- API back-end rodando em:
