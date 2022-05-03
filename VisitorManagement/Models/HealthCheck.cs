using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class HealthCheck
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), DisplayName("Id#")]
        public int Id { get; set; }
        public double? Temperature { get; set; }
        public string Hc_fevor { get; set; }
        public string Hc_shortness_breath { get; set; }
        public string Hc_sore_throat { get; set; }
        public string Hc_loss_taste { get; set; }
        public string Hc_cough { get; set; }
        public string Hc_muscle_pain { get; set; }
        public string Hc_other { get; set; }
        public string Hc_visit_gethering { get; set; }
        public string Hc_gethering_place { get; set; }
        public string Hc_gething_dates { get; set; }
        public string Hc_visit_health_facility { get; set; }
        public string Hc_health_facility_name { get; set; }
        public string Hc_health_facility_dates { get; set; }
        public DateTime? Last_check_dates { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
