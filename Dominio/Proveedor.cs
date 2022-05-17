using System;

namespace Dominio
{
    public class Proveedor
    {
        public Guid ProveedorId { get; set; }
        public string Descripcion { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public MovimientosAlmacen MovimientosAlmacen { get; set; }
    }
}