using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Grupo
{
    public class ConsultaVistaGrupo
    {
        public class ListarVistaGrupo : IRequest<List<VistaGrupoDto>>{
            
        }
        public class Manager : IRequestHandler<ListarVistaGrupo, List<VistaGrupoDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context)
            {
                _context = context;
            }
            public async Task<List<VistaGrupoDto>> Handle(ListarVistaGrupo request, CancellationToken cancellationToken)
            {
                var listaGrupo = await _context.GrupoMateriales.Select(x => new VistaGrupoDto{
                    GrupoMaterialesId = x.GrupoMaterialesId,
                    Descripcion = x.Descripcion
                }).ToListAsync();
                return listaGrupo;
            }
        }
    }
}