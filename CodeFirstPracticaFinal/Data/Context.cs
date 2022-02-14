using System.Linq;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace Data
{
    public class Context : DbContext //using Microsoft.EntityFramework
    {
        private string cadenaConexion;

        //escribimos en forma de atributos las tablas que tendremos en la db
        //tengo una tabla que estará mapeada por la clase persona
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //ctor, llama al padre y le manda las options
        public Context(DbContextOptions options) : base(options)
        {

        }

        //ctor que no recibe nada
        public Context() : base()
        {
            //guardamos lo que nos devuelve el IConfigurationRoot (que es el puntero a mi archivo de config)
            var configuration = GetConfiguration();
            //obtenemos la cadea de conex
            cadenaConexion = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        //método para obtener los datos del appsettings
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(cadenaConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //using System.Linq;
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            }
            base.OnModelCreating(builder);
        }
    }
}