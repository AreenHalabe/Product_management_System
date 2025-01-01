using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class midicen
    {

        public int SupplierId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Contact { get; set; } = string.Empty;
    }
}
