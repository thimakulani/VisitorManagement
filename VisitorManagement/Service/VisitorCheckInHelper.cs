using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Service
{
    public class VisitorCheckInHelper
    {
        [Required]
        public string Id { get; set; }
    }
}
