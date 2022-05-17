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
    public class ListaMovimientos
    {
        public class ListaDeIngresos : IRequest<List<NewDetalleIngresoDTO>>{

        }
        public class Manager : IRequestHandler<ListaDeIngresos, List<NewDetalleIngresoDTO>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<List<NewDetalleIngresoDTO>> Handle(ListaDeIngresos request, CancellationToken cancellationToken)
            {
                try
                {
                    var lista = await _context.DMovimientoAlmacen.Select(
                    x => new NewDetalleIngresoDTO
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
                    //_context.Database.SetCommandTimeout(400);
                    return lista;
                    
                }
                catch (Exception e)
                {

                    throw;
                }
                
            }
        }
    }
}