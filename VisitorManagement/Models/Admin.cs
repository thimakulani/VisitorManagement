using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? DateCreated { get; set; }
        public string? Level { get; set; }
    }
}