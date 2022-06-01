using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Movimientos
{
    public class ListaSalidas
    {
        public class ListaSalidasView : IRequest<List<Detallesalidadto>>{

        }
        public class Manager : IRequestHandler<ListaSalidasView, List<Detallesalidadto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<List<Detallesalidadto>> Handle(ListaSalidasView request, CancellationToken cancellationToken)
            {
                var lista = await _context.DMovimientoAlmacen.Select(
                    x => new Detallesalidadto
                    {
                        MovimientosAlmacenId = x.MovimientosAlmacenId,
                        Descripcion = x.Material.Descripcion,
                        CodProveedor = x.Material.CodProveedor,
                        DescripcionOperacion = x.MovimientosAlmacen.OperacionesAlmacen.Descripcion,
                        UnidadMedida = x.Material.UnidadMedida.Abreviatura,
                        Cantidad = x.Cantidad,
                        PrecioActual = x.PrecioUnitario,
                        Total = x.Subtotal,
                        Valor = x.ValorUnitario,
                        Igv = x.Igv,
                        FechaRegistro = x.FechaCreacion,
                        Observacion = x.Observacion
                    }
                ).OrderBy(x => x.FechaRegistro).Where(x => x.DescripcionOperacion == "SALIDAS").ToListAsync();
                return lista;
            }
        }
    }
}