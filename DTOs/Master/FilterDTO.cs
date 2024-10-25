using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Master
{
    public class FilterDTO
    {
        public int? billId { get; set; }
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? LoginId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int ZoneId { get; set; }
    }
}
