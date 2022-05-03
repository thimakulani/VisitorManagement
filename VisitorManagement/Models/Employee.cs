using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), DisplayName("Persal#")]
        public int Persal { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email"), EmailAddress]
        public string Email { get; set; }
        [DisplayName("Phone#"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DisplayName("Stuff Status")]
        public string Staff_status { get; set; }
        public string Qr_image { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        public DateTime? LastCheckIn { get; set; }
        public ICollection<EmployeeRegister> EmployeeRegisters { get; set; }
    }
}
