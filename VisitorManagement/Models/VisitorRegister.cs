using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class VisitorRegister
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), DisplayName("Id#")]
        public int Id { get; set; }
        [Display(Name = "CHECKED-IN"), DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Last_login { get; set; }
        [DataType(DataType.Time), Display(Name = "CHECKED-OUT")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Last_logout { get; set; }
        public string Type { get; set; }
        public string AppointmentWith { get; set; }
        public double Temperature { get; set; }
        public bool Suspend_status { get; set; }
        public string Asset_type { get; set; }
        public string Asset_num { get; set; }
        public bool Hc_fevor { get; set; }
        public bool Hc_shortness_breath { get; set; }
        public bool Hc_sore_throat { get; set; }
        public bool Hc_loss_taste { get; set; }
        public bool Hc_cough { get; set; }
        public bool Hc_muscle_pain { get; set; }
        public bool Hc_other { get; set; }
        public bool Hc_visit_gethering { get; set; }
        public string Hc_gethering_place { get; set; }
        public DateTime? Hc_gething_dates { get; set; }
        public bool Hc_visit_health_facility { get; set; }
        public string Hc_health_facility_name { get; set; }
        public DateTime? Hc_health_facility_dates { get; set; }

        [Required]
        public string VisitorId { get; set; }
        [Required]
        public Visitor Visitor { get; set; }
    }
}
