using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DTOs.APIDTO
{
    public class ParameterDTO
    {
        public string parameter { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public long timestamp { get; set; }
        public string flag { get; set; }
    }

    public class UPJNDataDTO
    {
        public string deviceId { get; set; }
        public List<ParameterDTO> parameters { get; set; }
    }

    public class UPJNAPIDTO
    {
        public string stationId { get; set; }
        public List<UPJNDataDTO> data { get; set; }
    }


    public class ApiResponseDTO
    {
        public string status {  get; set; }
        public string message { get; set; }
    }
    public class STPsOCEMSDTO
    {
        public long Id { get; set; } = 0;
        public int StationId { get; set; }
        public float BOD { get; set; }
        public float COD { get; set; }
        public float TSS { get; set; }
        public float PH { get; set; }
        public float Flow { get; set; }
        public long ReportedDate { get; set; }
        public DateTime AddedOn { get; set; }

    }

    [XmlRoot("UPJNAPIDTO")]
    public class _UPJNAPIDTO
    {
        public int stationId { get; set; }
        public UPJNData data { get; set; }
    }

    public class UPJNData
    {
        public UPJNDataDTO UPJNDataDTO { get; set; }
    }
   
   
}
