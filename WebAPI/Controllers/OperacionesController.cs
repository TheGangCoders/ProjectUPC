using System.Threading.Tasks;
using Aplicacion.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionesController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Crear(NuevaOperacion.Execute data){
            return await Mediator.Send(data);
        }
    }
}