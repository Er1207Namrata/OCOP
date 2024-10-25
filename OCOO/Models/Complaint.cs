using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    
    public class ComplaintReport : Common
    {
        public int ticketid { get; set; }

        [Required(ErrorMessage = "Create date is required")]
        public string createddate { get; set; }
        [Required(ErrorMessage = "modify date is required")]
        public string modifydate { get; set; }
        public string status { get; set; }
        public string assigned { get; set; }

        [Required(ErrorMessage = "Complainer name is required")]
        public string ComplainerName { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number.")]
        public string ContactNo { get; set; }
        [EmailAddress(ErrorMessage = "Email is required")]
        [Required(ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }

        public string Flat_No { get; set; }
        public string Block_No { get; set; }
        public string Colony_Name { get; set; }
        public string City_State { get; set; }
        public string City { get; set; }
        public string Ward_Name { get; set; }
        public string WardNo { get; set; }
        public string Ward_Locality { get; set; }
        public string Zone { get; set; }
        public string Parsad_Name { get; set; }
        public string Zonal_Incharge_Number { get; set; }
        public string ZonalIncharge_Name { get; set; }
        public string Type_Of_Complaint { get; set; }
        public string Issue { get; set; }
        public string tat { get; set; }
        public string comment { get; set; }
        public string file_path { get; set; }
        public string FormDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetComplaintData()
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@FormDate ",string.IsNullOrEmpty(FormDate)  ? null : FormDate),
                     new SqlParameter("@ToDate ",string.IsNullOrEmpty(ToDate)  ? null : ToDate),
                     new SqlParameter("@ComplainerName",string.IsNullOrEmpty(ComplainerName)  ? null : ComplainerName ),
                     new SqlParameter("@ContactNo",string.IsNullOrEmpty(ContactNo)? null : ContactNo ),
                     new SqlParameter("@Zone",string.IsNullOrEmpty(Zone)? null : Zone ),
                     new SqlParameter("@EmailId",string.IsNullOrEmpty(EmailId)? null :EmailId ),
                     new SqlParameter("@Page",Page),
                     new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.sp_ComplianceReport, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
