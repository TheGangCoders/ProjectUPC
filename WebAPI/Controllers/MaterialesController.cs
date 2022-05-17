using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.DTO;
using Aplicacion.Materiales;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialesController : MyControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Crear(NuevoMaterial.Execute data){
            return await Mediator.Send(data);
        }
        [HttpGet]
        public async Task<ActionResult<List<MateriaDto>>> Get(){
            return await Mediator.Send(new ListarMaterial.ListaMateriales());
        }
        [HttpGet("ListarStock")]
        public async Task<ActionResult<List<MateriaDto>>> GetSt(){
            return await Mediator.Send(new ListarMaterialesStock.ListaMaterialesSt());
        }
        [Authorize]
        [HttpGet("MaterialById/{id}", Name= "MaterialById")]
        public async Task<ActionResult<MateriaDto>> Unique(Guid id){
            return await Mediator.Send(new ListMaterialById.MaterialUnique{Id = id});
        }
        [Authorize]
        [HttpPut("UpdateMat/{id}", Name = "UpdateMat")]
        public async Task<ActionResult<int>> Update(Guid id, UpdateMaterial.UpdateMat data) {
            data.MaterialId = id;
            return await Mediator.Send(data);
        }

    }
}