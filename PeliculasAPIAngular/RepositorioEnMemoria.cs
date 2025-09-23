using PeliculasAPIAngular.Entidades;

namespace PeliculasAPIAngular
{
    public class RepositorioEnMemoria
    {
        private List<Genero> _generos;

        public RepositorioEnMemoria()
        {
            _generos = new List<Genero>
            {
                new Genero { Id = 1, Nombre = "Acción" },
                new Genero { Id = 2, Nombre = "Comedia" },
                new Genero { Id = 3, Nombre = "Drama" },
                new Genero { Id = 4, Nombre = "Terror" },
                new Genero { Id = 5, Nombre = "Ciencia Ficción" }
            };
        }

        public List<Genero> ObtenerTodosLosGeneros()
        {
            return _generos;
        }

        public Genero? ObtenerGeneroPorId(int id)
        {
            return _generos.FirstOrDefault(g => g.Id == id);
        }

        public async Task<Genero?> ObtenerPorId(int id)
        {
            await Task.Delay(5000);
            return _generos.FirstOrDefault(g => g.Id == id);
        }

        public bool Existe(string nombre)
        {
            return _generos.Any(g => g.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
