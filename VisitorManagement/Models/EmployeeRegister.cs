﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class EmployeeRegister
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), DisplayName("Id#")]
        public int Id { get; set; }


        public string? Temp { get; set; }
        public DateTime? Last_login { get; set; }
        public DateTime? Last_logout { get; set; }
        public bool Suspend_status { get; set; }
        public string? Asset_num { get; set; }
        public string? Asset_type { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
