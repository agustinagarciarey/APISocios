using System;

namespace Comando.Socios
{
    public class ComandoCreateSocio
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public int DNI { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int Deporte { get; set; }
        public bool Premium { get; set; }

        public DateTime FechaAlta { get; set; }
    }
}