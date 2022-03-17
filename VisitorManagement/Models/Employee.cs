using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class Employee
    {
        [Key]
        public int Persal { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Job_title { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string Image { get; set; }
        public string Name_directorate { get; set; }
        public string Staff_status { get; set; }
        public string Qr_image { get; set; }
        public string Status { get; set; }
    }
}
