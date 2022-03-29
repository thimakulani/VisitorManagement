using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), DisplayName("Persal#")]
        public int Persal { get; set; }
        [DisplayName("Firs tName")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [DisplayName("Email")]
        public string? Email { get; set; }
        [DisplayName("Phone#")]
        public string? Phone { get; set; }
        //[DisplayName("Job Title")]
        //public string? Job_title { get; set; }
        //[DisplayName("Address")]
        //public string? Address { get; set; }
        //[DataType(DataType.ImageUrl)]
        //public string? Image { get; set; }
        //[DisplayName("Directorate Name")]
        //public string? DirectorateName { get; set; }

        [DisplayName("Stuff Status")]
        public string? Staff_status { get; set; }
        public string? Qr_image { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; }
    }
}
