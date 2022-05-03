using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class Temperature
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
    }
}
