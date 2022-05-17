using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.UnidadesMedida
{
    public class ListaVistaUnidad
    {
        public class ListarVistaUnidad : IRequest<List<VistaUnidadDto>>{
            
        }
        public class Manager : IRequestHandler<ListarVistaUnidad, List<VistaUnidadDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context)
            {
                _context = context;
            }
            public async Task<List<VistaUnidadDto>> Handle(ListarVistaUnidad request, CancellationToken cancellationToken)
            {
                var listaUnidad = await _context.UnidadMedida.Select(x => new VistaUnidadDto{
                    UnidadMedidaId = x.UnidadMedidaId,
                    Abreviatura = x.Abreviatura
                }).ToListAsync();
                return listaUnidad;
            }
        }
    }
}