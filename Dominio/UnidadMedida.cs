using System;

namespace Dominio
{
    public class UnidadMedida
    {
        public Guid UnidadMedidaId { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Material Material { get; set; }
    }
}