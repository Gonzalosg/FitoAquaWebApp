﻿namespace FitoAquaWebApp.Models
{
    public class UsuarioObra
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
