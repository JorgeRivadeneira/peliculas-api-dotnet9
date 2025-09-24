using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPIAngular.Contratos;
using PeliculasAPIAngular.Entidades;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PeliculasAPIAngular.Controllers
{
    //[Route("api/generos")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly ServicioTransient transient1;
        private readonly ServicioTransient transient2;
        private readonly ServicioScoped scoped1;
        private readonly ServicioScoped scoped2;
        private readonly ServicioSingleton singleton;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IConfiguration configuration;
        private const string cacheTag = "generos";

        public GenerosController(IRepositorio repositorio,
            ServicioTransient transient1,
            ServicioTransient transient2,
            ServicioScoped scoped1,
            ServicioScoped scoped2,
            ServicioSingleton singleton, IOutputCacheStore outputCacheStore,
            IConfiguration configuration)
        {
            this.repositorio = repositorio;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton = singleton;
            this.outputCacheStore = outputCacheStore;
            this.configuration = configuration;
        }

        [HttpGet("ejemplo-proveedor-config")]
        public ActionResult<string> EjemploProveedorConfiguracion()
        {
            //"ConnectionString": "prod conn string",
            return configuration.GetValue<string>("ConnectionString")!;
        }


        [HttpGet("servicios-lifetime")]
        public IActionResult GetServicioTiempoDeVida()
        {
            return Ok(new
            {
                Transients = new { transient1 = transient1.ObtenerId(), transient2 = transient2.ObtenerId() },
                Scopeds = new { scoped1 = scoped1.ObtenerId(), scoped2 = scoped2.ObtenerId() },
                Singleton = singleton.ObtenerId()
            });
        }
            

        [HttpGet] //tenemos 2 rutas GET
        [HttpGet("obtenerTodos")] //api/generos/obtenerTodos
        [HttpGet("/todosLosGeneros")] //todosLosGeneros)]
        [OutputCache(Tags = [cacheTag])]
        public List<Genero> Get()
        {
            //var repositorio = new RepositorioEnMemoria();
            var generos = repositorio.ObtenerTodosLosGeneros();
            return generos;
        }

        //[HttpGet("{id}/{nombre?}")] //api/generos/1 || api/generos/1/accion
        [HttpGet("{id:int}")] //api/generos/1        
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            //var repositorio = new RepositorioEnMemoria();
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
            //var repositorio = new RepositorioEnMemoria();
            var genero = await repositorio.ObtenerPorId(1);
            return genero;
        }


        //[HttpPost("{id:int}")]
        //public void Post(int id, [FromBody]Genero genero, [FromQuery] string apellido)
        //{
        //    //return new OkResult();
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Genero genero)
        {
            //var repositorio = new RepositorioEnMemoria();
            var yaExisteUnGeneroConDichoNombre = repositorio.Existe(genero.Nombre);
            if (yaExisteUnGeneroConDichoNombre)
            {
                return BadRequest($"Ya existe un género con el nombre {genero.Nombre}");
            }

            repositorio.Crear(genero);
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return Ok(genero);
        }
    }
}
