using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.Movimientos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoMaterialesController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Crear(NuevoIngreso.Execute data){
            return await Mediator.Send(data);
        }
        [HttpGet("ListaIngresos")]
        public async Task<ActionResult<List<NewDetalleIngresoDTO>>> Lista(){
            return await Mediator.Send( new ListaMovimientos.ListaDeIngresos());            
        }
        [Authorize]
        [HttpGet("IngresoById/{id}", Name= "IngresoById")]
        public async Task<ActionResult<IngresoDTO>> Unique(Guid id){
            return await Mediator.Send(new ListaIngresosVista.ListaIngreso{Id = id});
        }
    }
}