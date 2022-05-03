using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class VisitReason
    {
        [Required, DisplayName("REASEON")]
        public string Name { get; set; }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), DisplayName("ID#")]
        public int Id { get; set; }
    }
}
