namespace PeliculasAPIAngular
{
    public class ServicioTransient
    {
        private readonly Guid _id = Guid.NewGuid();
        public ServicioTransient()
        {
            _id = Guid.NewGuid();
        }

        public Guid ObtenerId() => _id;
    }

    public class ServicioScoped
    {
        private readonly Guid _id = Guid.NewGuid();
        public ServicioScoped()
        {
            _id = Guid.NewGuid();
        }

        public Guid ObtenerId() => _id;
    }

    public class ServicioSingleton
    {
        private readonly Guid _id = Guid.NewGuid();
        public ServicioSingleton()
        {
            _id = Guid.NewGuid();
        }

        public Guid ObtenerId() => _id;
    }
}
