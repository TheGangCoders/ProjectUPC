using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.Grupo;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoMaterialController : MyControllerBase
    {
    
        [HttpPost]
        public async Task<ActionResult<int>> Crear(NuevoGrupo.Ejecuta data){
           return await Mediator.Send(data);
        }
        [HttpGet("VistaGrupo")]
        public async Task<ActionResult<List<VistaGrupoDto>>> Lista(){
            return await Mediator.Send(new ConsultaVistaGrupo.ListarVistaGrupo());
        }
    }
}