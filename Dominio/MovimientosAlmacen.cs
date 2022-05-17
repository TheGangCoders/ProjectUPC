using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class MovimientosAlmacen
    {
        public Guid MovimientosAlmacenId { get; set; }
        public string SerieGuia { get; set; }
        public string NroGuia { get; set; }
        public Guid OperacionesAlmacenId { get; set; }
        public OperacionesAlmacen OperacionesAlmacen { get; set; }
        public Guid DocumentoCompraId { get; set; }
        public DocumentoCompra DocumentoCompra { get; set; }
        public Guid ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public string SerieDoc { get; set; } //correlativo
        public int Numero { get; set; } //correlativo
        public Guid MonedaId { get; set; }
        public Moneda Moneda { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Igv { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public ICollection<DMovimientoAlmacen> DetalleMovimientoAlmacen { get; set; }
    }
}