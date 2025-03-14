using API_Login.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Login.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }

        // GET: api/usuario
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListaUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioId(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<Usuario>> CrearUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            // Adicionalmente, puedes validar las propiedades del usuario antes de guardar
            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Email)
                || string.IsNullOrWhiteSpace(usuario.Contrasena) || string.IsNullOrWhiteSpace(usuario.Rol))
            {
                return BadRequest("El nombre, correo, contraseña y rol son obligatorios.");
            }
            usuario.IntentosFallidos = 0;
            usuario.Estado = false;
            usuario.FechaBloqueo = null;

            _context.Usuario.Add(usuario);

            await _context.SaveChangesAsync();

            // Devuelve el usuario creado con el código 201 Created
            return CreatedAtAction(nameof(ObtenerUsuarioId), new { id = usuario.Id }, usuario);
        }

        // POST: api/Usuario/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginUsuario input)
        {
            if (input == null || string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Contrasena))
            {
                return BadRequest("Email y contraseña son obligatorios.");
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(u => u.Email == input.Email && u.Contrasena == input.Contrasena);

            if (usuario == null)
            {
                return Unauthorized();
            }

            return usuario; // Devuelve el usuario si la validación es correcta
        }

    }
}
