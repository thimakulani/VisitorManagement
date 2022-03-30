using System.ComponentModel.DataAnnotations;
using VisitorManagement.Models;

namespace VisitorManagement.Service
{
    public class VisitorCheckInHelper
    {
        public string Id { get; set; }
        public VisitorRegister V_REG { get; set; }
    }
}
