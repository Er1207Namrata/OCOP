using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Employee
{
    public class EmployeeDropdownDTO
    {
        public int EmployeeId { get;set; }
        public string Name { get;set; }
        public string LoginId { get;set; }
        public string Office { get;set; }
        public string? Department { get;set; }   
        public int DepartmentId { get;set; }
        public string? Designation { get;set; }
        public int DesignationId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string STPs { get; set; }
        public string City { get; set; }
    }
}
