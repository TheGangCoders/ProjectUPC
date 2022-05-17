using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Grupo
{
    public class Consulta
    {
        public class ListaGrupos : IRequest<List<GrupoMateriales>>{

        }
        public class Manejador : IRequestHandler<ListaGrupos, List<GrupoMateriales>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manejador(AplicacionAlmacenContext context)
            {
                _context = context;
            }
            public async Task<List<GrupoMateriales>> Handle(ListaGrupos request, CancellationToken cancellationToken)
            {
                var grupos = await _context.GrupoMateriales.ToListAsync();
                return grupos;
            }
        }
    }
}