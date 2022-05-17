using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Aplicacion.Movimientos
{
    public class NuevaSalida
    {
        public class GenerarSalida : IRequest<int>{
            public Guid OperacionesAlmacenId { get; set; }
            public Guid MonedaId { get; set; }
        public List<DMovimientoAlmacen> DetalleMovimientoAlmacen { get; set; }
        }
        public class ExecuteValidation : AbstractValidator<GenerarSalida>{
            public ExecuteValidation(){
                RuleFor( x => x.OperacionesAlmacenId).NotEmpty();
                RuleFor( x => x.MonedaId).NotEmpty();
            }
        }
        public class Manager : IRequestHandler<GenerarSalida, int>
        {
            private readonly AplicacionAlmacenContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manager(AplicacionAlmacenContext context, UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion){
                _context = context;
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<int> Handle(GenerarSalida request, CancellationToken cancellationToken)
            {
                decimal total = 0;
                var docCompra = "A0243AA2-2584-48C4-AFAE-FD5F5316817E";
                var proveedor = "4210668C-2758-43B1-8441-2CE93D211597";
                foreach (var totales in request.DetalleMovimientoAlmacen)
                {
                    total = total + (totales.Cantidad * totales.PrecioUnitario);
                }
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                var correlativo = _context.CorrelativoMovimientos.Where(x => x.OperacionesAlmacenId == request.OperacionesAlmacenId).FirstOrDefault();
                Guid _MovimientosAlmacenId = Guid.NewGuid();
                var salida = new MovimientosAlmacen {
                    MovimientosAlmacenId = _MovimientosAlmacenId,
                    OperacionesAlmacenId = request.OperacionesAlmacenId,
                    DocumentoCompraId = Guid.Parse(docCompra),
                    SerieDoc = correlativo.Serie,
                    Numero= correlativo.Numero,
                    MonedaId = request.MonedaId,
                    ProveedorId = Guid.Parse(proveedor),
                    Total= Math.Round(total,2),
                    Valor= Math.Round(total/1.18m,2),
                    Igv=Math.Round((total/1.18m)*0.18m,2),
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario.UserName.ToUpper()
                };
                _context.MovimientosAlmacen.Add(salida);
                if(request.DetalleMovimientoAlmacen != null){
                    foreach (var detallesM in request.DetalleMovimientoAlmacen)
                    {
                        var detalle = new DMovimientoAlmacen{
                            DMovimientoAlmacenId = Guid.NewGuid(),
                            MovimientosAlmacenId = _MovimientosAlmacenId,
                            MaterialId = detallesM.MaterialId,
                            Cantidad = detallesM.Cantidad,
                            PrecioUnitario = detallesM.PrecioUnitario,
                            ValorUnitario = Math.Round(detallesM.PrecioUnitario/1.18m,2),
                            Igv = Math.Round(((detallesM.PrecioUnitario*detallesM.Cantidad)/1.18m)*0.18m,2),
                            Subtotal = Math.Round(detallesM.PrecioUnitario*detallesM.Cantidad,2),
                            Observacion = detallesM.Observacion,
                            Activo = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = usuario.UserName.ToUpper()
                        };
                        _context.DMovimientoAlmacen.Add(detalle);
                    }
                }
                if(request.DetalleMovimientoAlmacen != null){
                    foreach (var stock in request.DetalleMovimientoAlmacen)
                    {
                        var detallestock = new MaterialStock{
                            MaterialId = stock.MaterialId,
                            Cantidad = stock.Cantidad
                        };

                        var stockBD = _context.MaterialStock.Where(x => x.MaterialId == detallestock.MaterialId).FirstOrDefault();
                        if(stockBD != null)
                        {
                            if(stockBD.Cantidad-stock.Cantidad<-1){
                                return -2;
                            }
                            else {
                                stockBD.Cantidad = stockBD.Cantidad - stock.Cantidad;
                                stockBD.UsuarioModificacion = usuario.UserName.ToUpper();
                                stockBD.FechaModificacion = DateTime.Now;
                                _context.MaterialStock.Update(stockBD);
                            }
                            
                        }
                    }
                }
                if (correlativo != null)
                {
                    correlativo.Numero = correlativo.Numero + 1;
                    correlativo.FechaModificacion = DateTime.Now;
                    correlativo.UsuarioModificacion = usuario.UserName.ToUpper();
                    _context.CorrelativoMovimientos.Update(correlativo);
                }

                var valor = await _context.SaveChangesAsync();
                if(valor > 0){
                    return 1;
                }
                throw new Exception("No se pudo dar salida al material materiales");
            }
        }
    }
}