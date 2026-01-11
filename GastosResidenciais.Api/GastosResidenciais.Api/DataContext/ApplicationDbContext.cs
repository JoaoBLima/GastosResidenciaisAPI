using GastosResidenciais.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Api.DataContext
{
    /// <summary>
    /// Contexto do Entity Framework para acesso ao banco de dados.
    /// Define as entidades (tabelas) e suas configurações.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor que recebe as opções de configuração do DbContext.
        /// </summary>
        /// <param name="options">Opções de configuração do DbContext</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // O construtor base já inicializa o DbContext com as opções fornecidas
        }

        // ============================================================
        // DEFINIÇÃO DAS ENTIDADES (TABELAS) DO BANCO DE DADOS
        // ============================================================

        /// <summary>
        /// DbSet para a entidade PessoaModel.
        /// Representa a tabela "Pessoas" no banco de dados.
        /// Permite operações CRUD (Create, Read, Update, Delete) para pessoas.
        /// </summary>
        public DbSet<PessoaModel> Pessoas { get; set; }

        /// <summary>
        /// DbSet para a entidade CategoriaModel.
        /// Representa a tabela "Categorias" no banco de dados.
        /// Permite operações CRUD para categorias de transações.
        /// </summary>
        public DbSet<CategoriaModel> Categorias { get; set; }

        /// <summary>
        /// DbSet para a entidade TransacaoModel.
        /// Representa a tabela "Transacoes" no banco de dados.
        /// Permite operações CRUD para transações financeiras.
        /// </summary>
        public DbSet<TransacaoModel> Transacoes { get; set; }

        /// <summary>
        /// Método chamado durante a criação do modelo do banco de dados.
        /// Configurações específicas das entidades são definidas aqui.
        /// </summary>
        /// <param name="modelBuilder">Construtor do modelo do Entity Framework</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ============================================================
            // CONFIGURAÇÃO ESPECÍFICA DA ENTIDADE TransacaoModel
            // ============================================================

            // Configuração da propriedade Valor da entidade TransacaoModel
            modelBuilder.Entity<TransacaoModel>()
                .Property(t => t.Valor)
                .HasPrecision(18, 2); // Define a precisão decimal: 18 dígitos totais, 2 casas decimais

            // JUSTIFICATIVA: Esta configuração é necessária para:
            // 1. Garantir que valores monetários sejam armazenados com precisão adequada (2 casas decimais)
            // 2. Evitar problemas de arredondamento em cálculos financeiros
            // 3. Padronizar o formato de armazenamento de valores monetários no banco de dados
        }
    }
}