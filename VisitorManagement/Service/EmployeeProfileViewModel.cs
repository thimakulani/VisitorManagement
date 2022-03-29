using VisitorManagement.Models;

namespace VisitorManagement.Service
{
    public class EmployeeProfileViewModel
    {
        public Employee Employee { get; set; }
        public List<EmployeeRegister> EmployeeRegister { get; set; }
    }
}
