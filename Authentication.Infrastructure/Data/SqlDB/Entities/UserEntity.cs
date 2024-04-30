namespace Authentication.Infrastructure.Data.SqlDB.Entities
{
    public class UserEntity
    {
        public int IdUsuario { get; set; }
        public string IdentificacionUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public bool EstadoUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
