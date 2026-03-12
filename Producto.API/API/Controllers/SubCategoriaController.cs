using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase, ISubCategoriaController
    {
        private ISubCategoriaFlujo _subcatFlujo;
        private ILogger<SubCategoriaController> _modeloLogger;

        public SubCategoriaController(ISubCategoriaFlujo modeloFlujo, ILogger<SubCategoriaController> modeloLogger)
        {
            _subcatFlujo = modeloFlujo;
            _modeloLogger = modeloLogger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _subcatFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _subcatFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
