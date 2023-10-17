using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Entidades;

namespace MyBlog.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BlogContext _context;
        public UsuarioService(BlogContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
                 .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
