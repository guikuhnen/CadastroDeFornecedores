using CadastroDeFornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeFornecedores.Data.Context
{
    public class CadastroDeFornecedoresContext : DbContext
    {
        public CadastroDeFornecedoresContext(DbContextOptions<CadastroDeFornecedoresContext> options)
            : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<FornecedorTelefones> FornecedoresTelefones { get; set; }
    }
}