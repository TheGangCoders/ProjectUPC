using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.UnidadesMedida;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesMedidaController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Crear(NuevaUnidad.Ejecuta data){
            return await Mediator.Send(data);
        }
        [HttpGet("VistaUnidad")]
        public async Task<ActionResult<List<VistaUnidadDto>>> Lista(){
            return await Mediator.Send(new ListaVistaUnidad.ListarVistaUnidad());
        }
    }
}