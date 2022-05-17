using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion.DTO
{
    public class DetalleIngresoDTO
    {
        public Guid MovimientosAlmacenId { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialDes { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public int Activo { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Lote { get; set; }
    }
}