using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Aplicacion.Grupo
{
    public class NuevoGrupo
    {
        public class Ejecuta : IRequest<int>
        {
            public string Descripcion { get; set; }
        }
        public class Validation : AbstractValidator<Ejecuta>{
            public Validation(){
                RuleFor( x => x.Descripcion).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, int>
        {
            private readonly AplicacionAlmacenContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(AplicacionAlmacenContext context, UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion){
                _context = context;
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
            } 
            public async Task<int> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                Guid _GrupoMaterialesId = Guid.NewGuid();
                var grupo = new GrupoMateriales{
                    GrupoMaterialesId = _GrupoMaterialesId,
                    Descripcion = request.Descripcion.ToUpper(),
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario.UserName.ToUpper()
                };
                _context.GrupoMateriales.Add(grupo);
                var valor = await _context.SaveChangesAsync();
                if(valor > 0){
                    return 1;
                }
                throw new Exception("No se pudo insertar el Grupo de Material");
            }
        }
    }
}