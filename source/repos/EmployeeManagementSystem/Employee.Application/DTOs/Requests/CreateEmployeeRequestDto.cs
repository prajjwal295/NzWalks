namespace Employee.Application.DTOs.Requests
{
    public class CreateEmployeeRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
