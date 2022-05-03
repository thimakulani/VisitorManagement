using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Persal { get; set; }

        [Required]
        public string Firstname { get; set; }


        [Required]
        public string Lastname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
