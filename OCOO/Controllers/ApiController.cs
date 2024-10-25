using DTOs.APIDTO;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using OCOO.Models;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OCOO.Controllers
{

    [ApiController]
    [Route("api/v1.0/IT/001/")]
    public class ApiController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ApiController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost]
        public IActionResult UPJNPostData([FromBody] UPJNAPIDTO _req)
        {
            try
            {
                UPJNDataService _UPJNDataService = new UPJNDataService();
                DataSet data0 = _UPJNDataService.GetKeys(1);
                string ApiKey = "";
                string SecretKey = "";
                if (data0 != null && data0.Tables.Count > 0 && data0.Tables[0].Rows.Count > 0)
                {
                    ApiKey = data0.Tables[0].Rows[0]["ApiKey"].ToString();
                    SecretKey = data0.Tables[0].Rows[0]["SecretKey"].ToString();
                }
                if (!Request.Headers.TryGetValue("ApiKey", out var ApiKeyheaderValue))
                    return BadRequest("Invalid api key or api key not found.");
                if (!Request.Headers.TryGetValue("SecretKey", out var SecretKeyheaderValue))
                    return BadRequest("Invalid secret key or  secret key not found.");
                if (ApiKeyheaderValue == ApiKey && SecretKeyheaderValue == SecretKey)
                {

                    List<STPsOCEMSDTO> _ObjectOfOCEMS = new List<STPsOCEMSDTO>();
                    DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (var x in _req.data)
                    {
                        STPsOCEMSDTO sTPsOCEMSDTO = new STPsOCEMSDTO();
                        sTPsOCEMSDTO.StationId = Convert.ToInt32(_req.stationId);
                        sTPsOCEMSDTO.BOD = (float)x.parameters.Where(y => y.parameter.ToLower() == "bod").FirstOrDefault().value;
                        sTPsOCEMSDTO.COD = (float)x.parameters.Where(y => y.parameter.ToLower() == "cod").FirstOrDefault().value;
                        sTPsOCEMSDTO.TSS = (float)x.parameters.Where(y => y.parameter.ToLower() == "tss").FirstOrDefault().value;
                        sTPsOCEMSDTO.PH = (float)x.parameters.Where(y => y.parameter.ToLower() == "ph").FirstOrDefault().value;
                        sTPsOCEMSDTO.Flow = (float)x.parameters.Where(y => y.parameter.ToLower() == "flow").FirstOrDefault().value;
                        sTPsOCEMSDTO.ReportedDate = x.parameters.FirstOrDefault().timestamp;
                        _ObjectOfOCEMS.Add(sTPsOCEMSDTO);
                    }

                    string SingleXMLdata = _UPJNDataService.SerializeToXml(_ObjectOfOCEMS);
                    string XMLdata = _UPJNDataService.SerializeToXml(_req);
                    DataSet data = _UPJNDataService.SaveUPJNData(XMLdata, SingleXMLdata);
                    if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                        if (data.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            return Ok("Data received successfully.");
                        else
                            return BadRequest("Something went wrong please try again.");
                    else
                        return BadRequest("Something went wrong please try again.");
                }
                else
                    return Unauthorized("Invalid API Key or Secret Key provided.");
            }
            catch (Exception ex)
            {
                return BadRequest("We are facing some technical issues please try after some time.");
            }
        }
        [HttpPost]
        [Route("Complaints")]
        public IActionResult Complaints([FromForm] ComplaintDTO complainDTO)
        {
            try
            {
                UPJNDataService _UPJNDataService = new UPJNDataService();
                DataSet data0 = _UPJNDataService.GetKeys(2);
                string ApiKey = "";
                string SecretKey = "";
                if (data0 != null && data0.Tables.Count > 0 && data0.Tables[0].Rows.Count > 0)
                {
                    ApiKey = data0.Tables[0].Rows[0]["ApiKey"].ToString();
                    SecretKey = data0.Tables[0].Rows[0]["SecretKey"].ToString();
                }
                if (!Request.Headers.TryGetValue("ApiKey", out var ApiKeyheaderValue))
                    return BadRequest("Invalid api key or api key not found.");
                if (!Request.Headers.TryGetValue("SecretKey", out var SecretKeyheaderValue))
                    return BadRequest("Invalid secret key or  secret key not found.");

                if (Request.Form.Files != null)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var file = Request.Form.Files[0];
                        if (file.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "ComplaintFiles");
                            if (!Directory.Exists(uploadsFolder))
                                Directory.CreateDirectory(uploadsFolder);
                            string _fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(uploadsFolder, _fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyToAsync(fileStream);
                                complainDTO.file_path = "/ComplaintFiles/" + _fileName;
                            }
                        }
                    }
                }
                DataSet data = _UPJNDataService.GetComplaint(complainDTO);
                if (ApiKeyheaderValue == ApiKey && SecretKeyheaderValue == SecretKey)
                {
                    if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        if (data.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            return Ok(new { Status = "success", Message = "Data Save Successfully." });
                        else
                            return BadRequest(new { Status = "Failed", Message = "Something went wrong please try again." });
                    }
                    else
                    {
                        return BadRequest(new { Status = "Failed", Message = "Something went wrong please try again." });
                    }
                }
                else
                    return Unauthorized(new { Status = "Failed", Message = "Invalid API Key or Secret Key provided." });
            }
            catch
            {
                return BadRequest(new { Status = "Failed", Message = "We are facing some technical issues please try after some time." });
            }
        }
    }
}
