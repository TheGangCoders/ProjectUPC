using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Movimientos
{
    public class ListaMovimientos
    {
        public class ListaDeIngresos : IRequest<List<NewDetalleIngresoDto>>{

        }
        public class Manager : IRequestHandler<ListaDeIngresos, List<NewDetalleIngresoDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<List<NewDetalleIngresoDto>> Handle(ListaDeIngresos request, CancellationToken cancellationToken)
            {
                try
                {
                    var lista = await _context.DMovimientoAlmacen.Select(
                    x => new NewDetalleIngresoDto
                    {
                        MovimientosAlmacenId = x.MovimientosAlmacenId,
                        SerieGuia = x.MovimientosAlmacen.SerieGuia,
                        NroGuia = x.MovimientosAlmacen.NroGuia,
                        DescripcionOperacion = x.MovimientosAlmacen.OperacionesAlmacen.Descripcion,
                        DocumentoCompra = x.MovimientosAlmacen.DocumentoCompra.Descripcion,
                        Proveedor = x.MovimientosAlmacen.Proveedor.Descripcion,
                        Descripcion = x.Material.Descripcion,
                        UnidadMedida = x.Material.UnidadMedida.Abreviatura,
                        Cantidad = x.Cantidad,
                        CodProveedor = x.Material.CodProveedor,
                        PrecioActual = x.PrecioUnitario,
                        Total = x.Subtotal,
                        Valor = x.ValorUnitario,
                        Igv = x.Igv,
                        FechaRegistro = x.FechaCreacion,
                    }
                ).OrderBy(x => x.FechaRegistro).Where(x => x.DescripcionOperacion == "INGRESOS").ToListAsync();
                    return lista;
                    
                }
                catch (Exception e)
                {
                    throw new InstanceNotFoundException(e.ToString());
                }
                
            }
        }
    }
}