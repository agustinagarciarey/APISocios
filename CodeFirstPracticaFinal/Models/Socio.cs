using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Models
{
    [Table("socios")]
    public class Socio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //para que sea identity (que aumenta de a 1)
        [Key] //así especifico que id será mi primary key 
              //using System.ComponentModel.DataAnnotations
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int Edad { get; set; }
        public int DNI { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

        public bool Premium { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdDeporte { get; set; }
        //tiene un...   
        [ForeignKey("IdDeporte")]
        //especificamos a dónde apunta
        public Deporte Deporte { set; get; }
        //estamos diciendo: este atributo será fk de la siguiente tabla

        public Socio()
        {

        }

        public Socio(int Id, string Nombre, string Apellido, int IdDeporte, int DNI, string Direccion, string Email, int Edad, bool Premium, DateTime FechaAlta)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.IdDeporte = IdDeporte;
            this.DNI = DNI;
            this.Direccion = Direccion;
            this.Email = Email;
            this.Edad = Edad;
            this.Premium = Premium;
            this.FechaAlta = FechaAlta;

        }


    }
}