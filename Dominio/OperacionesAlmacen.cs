using System;

namespace Dominio
{
    public class OperacionesAlmacen
    {
        public Guid OperacionesAlmacenId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public MovimientosAlmacen MovimientosAlmacen { get; set; }
        public CorrelativoMovimientos CorrelativoMovimientos { get; set; }
    }
}