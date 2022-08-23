using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Producer //copiamos el codigo de section y cambiamos los 'section' por producer
    {
        //public dice que nosotros desde cualquier parte de la aplicacion podemos acceder al codigo de esta clase.
        // si usaramos private en lugar de public, hacemos que sea visible solo dentro de este archivo.
        //protected solo visible para la carpeta que lo contiene.
        [Key]
        public int ProducerID { get; set; }
        //sectionID: identifica un objeto unico. con esto nos damos cuenta rapido cuando cometemos un error


        [Display(Name = "Nombre de la Productora")] //Cambiamos 'seccion' por productora
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? ProducerName { get; set; }



         public virtual ICollection<Movie>? Movies { get; set; }
    }
}