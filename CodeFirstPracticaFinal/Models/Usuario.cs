using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //para que sea identity (que aumenta de a 1)
        [Key] //así especifico que id será mi primary key 
              //using System.ComponentModel.DataAnnotations
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public Usuario()
        {

        }

    }
}