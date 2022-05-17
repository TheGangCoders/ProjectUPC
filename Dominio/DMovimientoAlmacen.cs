using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class DMovimientoAlmacen
    {
        public Guid DMovimientoAlmacenId { get; set; }
        public Guid MovimientosAlmacenId { get; set; }
        public MovimientosAlmacen MovimientosAlmacen { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ValorUnitario { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Igv { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string Observacion { get; set; }
    }
}