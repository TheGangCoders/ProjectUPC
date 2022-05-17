using System;

namespace Dominio
{
    public class Moneda
    {
        public Guid MonedaId { get; set; }
        public string Descripcion { get; set; }
        public string Divisa { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public MovimientosAlmacen MovimientosAlmacen { get; set; }
    }
}