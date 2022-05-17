using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Materiales
{
    public class ListMaterialById
    {
        public class MaterialUnique : IRequest<MateriaDto>{
            public Guid Id {get; set;}
        }
        public class Handler : IRequestHandler<MaterialUnique, MateriaDto>
        {
            private readonly AplicacionAlmacenContext _context;
            public Handler(AplicacionAlmacenContext context){
                 _context = context;
            }
            public async Task<MateriaDto> Handle(MaterialUnique request, CancellationToken cancellationToken)
            {
                var material = await _context.Material
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
                })
                .FirstOrDefaultAsync(x => x.MaterialId == request.Id);
                return material;
            }
        }
    }
}