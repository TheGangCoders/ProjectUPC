using System;
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
    public class ListaIngresosVista
    {
        public class ListaIngreso : IRequest<IngresoDTO>{
            public Guid Id { get; set; }
        }
        public class Manager : IRequestHandler<ListaIngreso, IngresoDTO>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<IngresoDTO> Handle(ListaIngreso request, CancellationToken cancellationToken)
            {
                var dlista = await _context.DMovimientoAlmacen.Select(
                    x => new DetalleIngresoDTO {
                        MovimientosAlmacenId = x.MovimientosAlmacenId,
                        MaterialId = x.MaterialId,
                        MaterialDes = x.Material.Descripcion,
                        Cantidad = x.Cantidad,
                        Precio = x.PrecioUnitario,
                        Activo = 1,
                        Lote = x.Lote == null? null : x.Lote,
                        FechaVencimiento = x.FechaVencimiento == null? (DateTime?)null : x.FechaVencimiento
                    }
                ).Where(x => x.MovimientosAlmacenId == request.Id).ToListAsync();

                var lista = await _context.MovimientosAlmacen.Select(
                    x => new IngresoDTO {
                        MovimientosAlmacenId = x.MovimientosAlmacenId,
                        DocumentoCompraId = x.DocumentoCompraId,
                        ProveedorId = x.ProveedorId,
                        SerieGuia = x.SerieGuia,
                        NroGuia = x.NroGuia,
                        MonedaId = x.MonedaId,
                        DetalleMovimientoAlmacen = dlista
                    }
                ).FirstOrDefaultAsync(x => x.MovimientosAlmacenId == request.Id);
                return lista;
            }
        }
    }
}