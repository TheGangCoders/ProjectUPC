using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio;

namespace Aplicacion.DTO
{
    public class MateriaDto
    {
        public Guid MaterialId { get; set; }
        public string MaterialDescripcion { get; set; }
        public string CodProveedor { get; set; }
        public Guid UnidadMedidaId { get; set; }
        public string UnidadMedida { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        public Guid GrupoMaterialesId { get; set; }
        public string GrupoMaterial { get; set; }
        public int Activo { get; set; }
        
    }
}