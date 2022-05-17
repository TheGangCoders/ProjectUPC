using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.Proveedores;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : MyControllerBase
    {
        [HttpGet("VistaProveedores")]
        public async Task<ActionResult<List<VistaProveedoresDto>>> Lista(){
            return await Mediator.Send(new ListarProveedores.ListarVistaProveedores());
        }
        
    }
}