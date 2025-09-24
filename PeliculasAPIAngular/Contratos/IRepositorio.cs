using PeliculasAPIAngular.Entidades;

namespace PeliculasAPIAngular.Contratos
{
    public interface IRepositorio
    {
        public List<Genero> ObtenerTodosLosGeneros();
        public Genero? ObtenerGeneroPorId(int id);
        public Task<Genero?> ObtenerPorId(int id);
        public bool Existe(string nombre);
        void Crear(Genero genero);
    }
}
