using System;

namespace Dominio
{
    public class CorrelativoMovimientos
    {
        public Guid CorrelativoMovimientosId { get; set; }
        public int Numero { get; set; }
        public string Serie { get; set; }
        public bool Activo {get; set;}
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Guid OperacionesAlmacenId { get; set; }
        public OperacionesAlmacen OperacionesAlmacen { get; set; }
    }
}