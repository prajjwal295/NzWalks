namespace Employee.BLL.BOs.Response
{
    public class DepartmentResponseBO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<EmployeeResponsebo> Employees { get; set; } = new List<EmployeeResponsebo>();
    }
}
