using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Materiales
{
    public class ListarMaterialesStock
    {
        public class ListaMaterialesSt : IRequest<List<MateriaDto>>{

        }
        public class Manager : IRequestHandler<ListaMaterialesSt, List<MateriaDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<List<MateriaDto>> Handle(ListaMaterialesSt request, CancellationToken cancellationToken)
            {
                var materiales = await _context.Material
                .Select( x => new MateriaDto{
                    MaterialId = x.MaterialId,
                    MaterialDescripcion = x.Descripcion,
                    CodProveedor = x.CodProveedor,
                    PrecioActual = x.PrecioActual.PrecioActual,
                    GrupoMaterialesId = x.GrupoMaterialesId,
                    GrupoMaterial = x.GrupoMaterial.Descripcion,
                    Cantidad = x.MaterialStock.Cantidad,
                    UnidadMedida = x.UnidadMedida.Abreviatura,
                    UnidadMedidaId = x.UnidadMedida.UnidadMedidaId,
                    Activo = 1
                }).Where(x => x.Cantidad >0).OrderBy( x => x.MaterialDescripcion)
                .ToListAsync();
                return materiales;
            }
        }
    }
}