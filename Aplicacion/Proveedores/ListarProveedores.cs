using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Proveedores
{
    public class ListarProveedores
    {
        public class ListarVistaProveedores : IRequest<List<VistaProveedoresDto>>{
            
        }
        public class Manager : IRequestHandler<ListarVistaProveedores, List<VistaProveedoresDto>>
        {
            private readonly AplicacionAlmacenContext _context;
            public Manager(AplicacionAlmacenContext context)
            {
                _context = context;
            }
            public async Task<List<VistaProveedoresDto>> Handle(ListarVistaProveedores request, CancellationToken cancellationToken)
            {
                var listaProveedor = await _context.Proveedor.Select(
                    x => new VistaProveedoresDto {
                        ProveedorId = x.ProveedorId,
                        Descripcion = x.Descripcion
                    }).ToListAsync();
                return listaProveedor;
            }
        }
    }
}