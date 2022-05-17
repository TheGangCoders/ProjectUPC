using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.Movimientos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class SalidaMaterialesController : MyControllerBase
    {
        [Authorize]
        [HttpPost("Salidas")]
        public async Task<ActionResult<int>> Salida(NuevaSalida.GenerarSalida data){
            return await Mediator.Send(data);
        }
        [HttpGet("ListaSalidas")]
        public async Task<ActionResult<List<DetalleSalidaDTO>>> Lista(){
            return await Mediator.Send( new ListaSalidas.ListaSalidasView());            
        }
    }
}