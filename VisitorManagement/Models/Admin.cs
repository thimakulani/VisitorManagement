using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class Admin : IdentityUser
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), DisplayName("Persal#")]
        public int Persal { get; set; }

        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Lastname { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        [Required]
        public string? Level { get; set; }
    }
}