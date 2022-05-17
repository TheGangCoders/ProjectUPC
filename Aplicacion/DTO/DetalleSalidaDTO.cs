using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion.DTO
{
    public class DetalleSalidaDTO
    {
        public Guid MovimientosAlmacenId { get; set; }
        public string Descripcion { get; set; }
        public string CodProveedor { get; set; }
        public string DescripcionOperacion { get; set; }
        public string UnidadMedida { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Igv { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observacion { get; set; }
    }
}