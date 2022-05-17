using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Materiales
{
    public class UpdateMaterial
    {
        public class UpdateMat : IRequest<int>
        {
            public Guid MaterialId { get; set; }
            public string Descripcion { get; set; }
            public Guid GrupoMaterialesId { get; set; }
            public Guid UnidadMedidaId { get; set; }
            public decimal PrecioActual { get; set; }
            public string CodProveedor { get; set; }
            
        }
        public class ExecuteValidation : AbstractValidator<UpdateMat>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.GrupoMaterialesId).NotEmpty();
                RuleFor(x => x.UnidadMedidaId).NotEmpty();
                RuleFor(x => x.PrecioActual).NotEmpty();
                RuleFor( x => x.CodProveedor).NotEmpty();
            }
        }
        public class Manager : IRequestHandler<UpdateMat, int>
        {
            private readonly AplicacionAlmacenContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manager(AplicacionAlmacenContext context, UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion)
            {
                _context = context;
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<int> Handle(UpdateMat request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                var materiales = await _context.Material.Where(x => x.MaterialId == request.MaterialId).FirstOrDefaultAsync();
                if(materiales != null)
                {
                    var price = await _context.Precio.Where(x => x.MaterialId == request.MaterialId).FirstOrDefaultAsync();

                    materiales.GrupoMaterialesId = request.GrupoMaterialesId;
                    materiales.UnidadMedidaId = request.UnidadMedidaId;
                    materiales.Descripcion = request.Descripcion.ToUpper();
                    materiales.CodProveedor = request.CodProveedor.ToUpper();
                    materiales.FechaModificacion = DateTime.Now;
                    materiales.UsuarioModificacion = usuario.UserName.ToUpper();
                    _context.Update(materiales);

                    price.PrecioActual = request.PrecioActual;
                    price.FechaModificacion = DateTime.Now;
                    price.UsuarioModificacion = usuario.UserName.ToUpper();
                    _context.Update(price);
                    var valor = await _context.SaveChangesAsync();
                    if (valor > 0)
                    {
                        return 1;
                    }
                    throw new Exception("No se pudo ingresar materiales");
                }
                else
                {
                    return -2;
                }
            }
        }
    }
    }
