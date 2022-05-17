using System;
using System.Collections.Generic;

namespace Aplicacion.DTO
{
    public class IngresoDTO
    {
        public Guid MovimientosAlmacenId { get; set; }
        public Guid DocumentoCompraId { get; set; }
        public Guid ProveedorId { get; set; }
        public string SerieGuia { get; set; }
        public string NroGuia { get; set; }
        public Guid MonedaId { get; set; }
        public ICollection<DetalleIngresoDTO> DetalleMovimientoAlmacen { get; set; }
    }
}