using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPIAngular.Entidades;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PeliculasAPIAngular.Controllers
{
    //[Route("api/generos")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController: ControllerBase
    {
        [HttpGet] //tenemos 2 rutas GET
        [HttpGet("obtenerTodos")] //api/generos/obtenerTodos
        [HttpGet("/todosLosGeneros")] //todosLosGeneros)]
        public List<Genero> Get()
        {
            var repositorio = new RepositorioEnMemoria();
            var generos = repositorio.ObtenerTodosLosGeneros();
            return generos;
        }

        //[HttpGet("{id}/{nombre?}")] //api/generos/1 || api/generos/1/accion
        [HttpGet("{id:int}")] //api/generos/1
        [OutputCache]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var repositorio = new RepositorioEnMemoria();
            var genero = await repositorio.ObtenerPorId(id);
            if (genero is null)
            {
                return NotFound();
            }
            return genero;
        }

        [HttpGet("{nombre}")] //api/generos/Jorge
        public async Task<Genero?> Get(string nombre)
        {
            var repositorio = new RepositorioEnMemoria();
            var genero = await repositorio.ObtenerPorId(1);
            return genero;
        }


        //[HttpPost("{id:int}")]
        //public void Post(int id, [FromBody]Genero genero, [FromQuery] string apellido)
        //{
        //    //return new OkResult();
        //}

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {
            var repositorio = new RepositorioEnMemoria();
            var yaExisteUnGeneroConDichoNombre = repositorio.Existe(genero.Nombre);
            if (yaExisteUnGeneroConDichoNombre)
            {
                return BadRequest($"Ya existe un género con el nombre {genero.Nombre}");
            }
            return Ok(genero);
        }
    }
}
