using DTOs.BillFlowMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class IntListExtensions
    {
        public static int SumAll(this List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum;
        }
        public static string ConvertToXml(this List<int> list, string field)
        {
            if (list == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            int sum = 0;
            foreach (var item in list)
            {
                sb.Append("<" + field + ">");
                sb.Append(item);
                sb.Append("</" + field + ">");
                sum += item;
            }
            sb.Append("</table>");
            return sb.ToString();
        }
        public static string ConvertToXml(this List<string> list, string field)
        {
            if (list == null)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");           
            foreach (var item in list)
            {
                sb.Append("<" + field + ">");
                sb.Append(item);
                sb.Append("</" + field + ">");                
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }

    public static class BillFlowMappedEmployeesDTOListExtensions
    {
        public static string ToXML(this List<BillFlowMappedEmployeesDTO> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var x in data)
            {
                sb.Append("<row>");
                sb.Append("<empid>" + x.EmployeeId + "</empid>");
                sb.Append("<fw>" + x.Forword + "</fw>");
                sb.Append("<dv>" + x.DocumentVerification + "</dv>");
                sb.Append("<fa>" + x.FinalApproval + "</fa>");
                sb.Append("</row>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
