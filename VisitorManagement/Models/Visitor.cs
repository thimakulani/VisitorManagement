using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class Visitor
    {
        [Key, Required, DataType(DataType.Text)]
        [Display(Name = "IDENTIFICATION")]
        public string Identification { get; set; }
        [DisplayName("FullNames")]
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Purpose { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }

}
