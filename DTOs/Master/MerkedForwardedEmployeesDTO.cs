using DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Master
{
    public class MerkedForwardedEmployeesDTO
    {
        public List<EmployeeDropdownDTO> MarkedEmployees { get; set; }
        public List<EmployeeDropdownDTO> ForwardedEmployees { get; set; }
        public List<EmployeeDropdownDTO> BillForwardedEmployees { get; set; }

    }
}
