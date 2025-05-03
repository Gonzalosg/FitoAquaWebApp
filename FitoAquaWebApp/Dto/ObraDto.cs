namespace FitoAquaWebApp.DTOs
{
    public class ObraDto
    {
        public int Id { get; set; }  // Requerido para PUT
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNombre { get; set; }


    }
}
