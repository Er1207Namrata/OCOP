using System.ComponentModel.DataAnnotations;

namespace OCOO.Models
{
    public class ComplaintDTO
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
        // [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number")]
        [Phone(ErrorMessage = "Invalid contact number.")]
        public string ContactNo { get; set; }
        [EmailAddress(ErrorMessage = "Email is required")]
        [Required(ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }

        public string Flat_No { get; set; }
        public string Block_No { get; set; }
        public string Colony_Name { get; set; }
        public string City_State { get; set; }
        public string? City { get; set; }
        public string? Ward_Name { get; set; }
        public string? WardNo { get; set; }
        public string? Ward_Locality { get; set; }
        public string? Zone { get; set; }
        public string? Parsad_Name { get; set; }
        public string? Zonal_Incharge_Number { get; set; }
        public string? ZonalIncharge_Name { get; set; }
        public string? Type_Of_Complaint { get; set; }
        public string? Issue { get; set; }
        public string? tat { get; set; }
        public string? comment { get; set; }
        public string? file_base64 { get; set; }
        public string? file_path {  get; set; }  
    }
}
