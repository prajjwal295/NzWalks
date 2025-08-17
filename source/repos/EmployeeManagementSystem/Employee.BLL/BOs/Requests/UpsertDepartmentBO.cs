using System.ComponentModel.DataAnnotations;

namespace Employee.BLL.BOs.Requests
{
    public class UpsertDepartmentBO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
