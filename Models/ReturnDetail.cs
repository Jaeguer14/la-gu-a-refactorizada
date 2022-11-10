using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class ReturnDetail
    {
        [Key]
        public int ReturnDetailID { get; set; }
        
        public int ReturnID { get; set;}
        public virtual Return? Return { get; set; }

        public int MovieID { get; set; }
        public virtual Movie? Movie { get; set; }

        [Display(Name = "Nombre de la Pelicula")]        
        public string? MovieName { get; set; }
       
    }
}