using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Materiales
{
    public class ListarMaterial
    {
        public class ListaMateriales : IRequest<List<MateriaDto>>{
        }
        public class Manager : IRequestHandler<ListaMateriales, List<MateriaDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            private readonly IMapper _mapper;
            public Manager(AplicacionAlmacenContext context, IMapper mapper){
                 _context = context;
                 _mapper = mapper;
            }
            public async Task<List<MateriaDto>> Handle(ListaMateriales request, CancellationToken cancellationToken)
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
                }).OrderBy( x => x.MaterialDescripcion)
                .ToListAsync();
                return materiales;
            }
        }
    }
}