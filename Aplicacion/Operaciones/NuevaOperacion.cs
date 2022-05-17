using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Aplicacion.Operaciones
{
    public class NuevaOperacion
    {
        public class Execute : IRequest<int>{
            public string Descripcion { get; set; }
        }
        public class ExecuteValidation :  AbstractValidator<Execute>{
             public ExecuteValidation(){
                RuleFor( x => x.Descripcion).NotEmpty();
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
                Guid _OperacionId = Guid.NewGuid();
                var operacion = new OperacionesAlmacen{
                    OperacionesAlmacenId = _OperacionId,
                    Descripcion = request.Descripcion.ToUpper(),
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario.UserName.ToUpper()
                };
                _context.OperacionesAlmacen.Add(operacion);
                var valor = await _context.SaveChangesAsync();
                if(valor > 0){
                    return 1;
                }
                throw new Exception("No se pudo insertar la operación");
            }
        }
    }
}