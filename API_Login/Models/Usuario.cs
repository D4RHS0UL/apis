namespace API_Login.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; } // Nota: Mantén las contraseñas seguras y no muestres este campo en las respuestas
        public string? Rol { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public int IntentosFallidos { get; set; } = 0;
        public bool Estado { get; set; } = false;
        public DateTime? FechaBloqueo { get; set; } = null;
    }

    public class LoginUsuario
    {
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
    }
}
