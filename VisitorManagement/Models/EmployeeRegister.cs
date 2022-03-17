using System.ComponentModel.DataAnnotations;

namespace VisitorManagement.Models
{
    public class EmployeeRegister
    {
        [Key]
        public int Id { get; set; }
        public string Persal { get; set; }
        public string Temp { get; set; }
        public DateTime Last_login { get; set; }
        public DateTime Last_logout { get; set; }
        public bool Suspend_status { get; set; }
        public string Asset_num { get; set; }
        public string Asset_type { get; set; }
        public DateTime Last_login_date { get; set; }
        public DateTime Last_logout_date { get; set; }
        public int HealthCheckId { get; set; }
        public HealthCheck HealthCheck { get; set; }

    }
}
