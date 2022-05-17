using System;

namespace Dominio
{
    public class Material
    {
        public Guid MaterialId { get; set; }
        public string Descripcion { get; set; }
        public string CodProveedor { get; set; }
        public Guid GrupoMaterialesId { get; set; }
        public GrupoMateriales GrupoMaterial { get; set; }
        public Guid UnidadMedidaId { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Precio PrecioActual { get; set; }
        public MaterialStock MaterialStock { get; set; }
    }
}