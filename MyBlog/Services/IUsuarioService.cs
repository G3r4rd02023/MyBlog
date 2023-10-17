using MyBlog.Data.Entidades;

namespace MyBlog.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario usuario);

    }
}
