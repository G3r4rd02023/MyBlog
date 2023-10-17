using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Entidades;
using MyBlog.Services;
using System.Security.Claims;

namespace MyBlog.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        private readonly IFilesService _filesService;
        public LoginController(IUsuarioService usuarioServicio,IFilesService filesService )
        {
            _usuarioServicio = usuarioServicio;
            _filesService = filesService;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario, IFormFile Imagen)
        {
            Stream image = Imagen.OpenReadStream();
            string urlimagen = await _filesService.SubirArchivo(image, Imagen.FileName);

            usuario.Clave = Utilidades.EncriptarClave(usuario.Clave);
            usuario.URLFotoPerfil = urlimagen;

            Usuario usuarioCreado = await _usuarioServicio.SaveUsuario(usuario);

            if (usuarioCreado.Id > 0)
                return RedirectToAction("IniciarSesion", "Login");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {

            Usuario usuarioEncontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarClave(clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario),
                new Claim("FotoPerfil", usuarioEncontrado.URLFotoPerfil),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
