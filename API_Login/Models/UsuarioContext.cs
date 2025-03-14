using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Login.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
