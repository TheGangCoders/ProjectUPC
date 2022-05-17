using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Aplicacion.Materiales
{
    public class NuevoMaterial
    {
        public class Execute : IRequest<int>{
            public string Descripcion { get; set; }
            public Guid GrupoMaterialesId { get; set; }
            public Guid UnidadMedidaId { get; set; }
            public decimal PrecioActual { get; set; }
            public string CodProveedor { get; set; }
            
        }
        public class ExecuteValidation : AbstractValidator<Execute>{
            public ExecuteValidation(){
                RuleFor( x => x.Descripcion).NotEmpty();
                RuleFor( x => x.GrupoMaterialesId).NotEmpty();
                RuleFor( x => x.UnidadMedidaId).NotEmpty();
                RuleFor( x => x.PrecioActual).NotEmpty();
                RuleFor( x => x.CodProveedor).NotEmpty();
            }
        }
        public class Manager : IRequestHandler<Execute, int>
        {
            private readonly AplicacionAlmacenContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manager(AplicacionAlmacenContext context, UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion){
                _context = context;
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<int> Handle(Execute request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                Guid _MaterialId = Guid.NewGuid();
                var material = new Material{
                    MaterialId = _MaterialId,
                    Descripcion = request.Descripcion.ToUpper(),
                    CodProveedor = request.CodProveedor.ToUpper(),
                    GrupoMaterialesId = request.GrupoMaterialesId,
                    UnidadMedidaId = request.UnidadMedidaId,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario.UserName.ToUpper()
                };
                _context.Material.Add(material);
                var precio = new Precio{
                    PrecioId = Guid.NewGuid(),
                    PrecioActual = request.PrecioActual,
                    MaterialId = _MaterialId,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario.UserName.ToUpper()
                };
                _context.Precio.Add(precio);
                var valor = await _context.SaveChangesAsync();
                if(valor > 0){
                    return 1;
                }
                throw new Exception("No se pudo insertar el Material");
            }
        }
    }
}