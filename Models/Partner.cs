using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models {
    public class Partner
    {
        [Key]
        public int PartnerID { get; set; }


        [Display(Name = "Nombre del Socio")]
        [Required(ErrorMessage = "Este valor es Obligatorio.")]
        [MaxLength(150, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? PartnerName { get; set; }


        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? PartnerDirection { get; set; }


        [Display(Name = "Telefono")]
        [MaxLength(100, ErrorMessage = "El largo máximo es de {0} caracteres.")]
        public string? PartnerPhone { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime PartnerBirthDate { get; set; }


        [NotMapped]
        public int PartnerAge
        {
            get
            {
                return DateTime.Now.Year - PartnerBirthDate.Year;
            }
        }

        [Display(Name = "Localidad")]
        public int LocationID { get; set; }
        public virtual Location? Locations { get; set; }
    }
}