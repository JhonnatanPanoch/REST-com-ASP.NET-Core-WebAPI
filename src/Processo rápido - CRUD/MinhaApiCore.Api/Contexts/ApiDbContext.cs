using Microsoft.EntityFrameworkCore;
using MinhaApiCore.Api.Model;

namespace MinhaApiCore.Api.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}
