using Employee.Dal.Entities;

namespace Employee.BLL.BOs.Response
{
    public class EmployeeResponsebo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int? DepartmentId { get; set; }

        public DepartmentNavigationBO Department { get; set; }
    }
}
