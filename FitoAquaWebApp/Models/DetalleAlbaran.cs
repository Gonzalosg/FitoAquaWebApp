namespace FitoAquaWebApp.Models
{
    public class DetalleAlbaran
    {
        public int AlbaranId { get; set; }
        public Albaran Albaran { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int CantidadUsada { get; set; }
    }
}
