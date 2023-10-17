using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Entidades;

namespace MyBlog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
