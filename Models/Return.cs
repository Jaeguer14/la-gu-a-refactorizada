using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Return
    {
        [Key]
        public int ReturnID { get; set; }

        [Display(Name = "Fecha de Devolucion")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Socio")]
        public int PartnerID { get; set; }
        
        [Display(Name = "Socio")]
        public virtual Partner? Partner { get; set; }

        public virtual ICollection<ReturnDetail>? ReturnDetail { get; set; }

    }
}