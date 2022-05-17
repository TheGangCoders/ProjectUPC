using System;

namespace Dominio
{
    public class GrupoMateriales
    {
        public Guid GrupoMaterialesId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Material Material { get; set; }
    }
}