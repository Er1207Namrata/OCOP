using System.Data.SqlClient;
using System.Data;
using DTOs.APIDTO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace OCOO.Models
{
    public class UPJNDataService
    {
        public DataSet GetKeys(int id)
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@LoginId", id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetKeys, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SaveUPJNData(string XmlData, string SingleXMLdata)
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@XmlData", XmlData),
                     new SqlParameter("@SingleXMLdata", SingleXMLdata),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.InsertUPJNData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SaveUPJN_Data(string XmlData, string SingleXMLdata)
        {
            try
            {
                SqlParameter[] para =
                {
                     new SqlParameter("@XmlData", XmlData),
                     new SqlParameter("@SingleXMLdata", SingleXMLdata),
                };
                DataSet ds = Connection.ExecuteQuery("ManualInsertUPJNData", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string SerializeToXml(UPJNAPIDTO obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UPJNAPIDTO));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public string SerializeToXml(List<STPsOCEMSDTO> obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<STPsOCEMSDTO>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public string SerializeToXml(_UPJNAPIDTO obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(_UPJNAPIDTO));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public DataSet GetComplaint(ComplaintDTO dTO)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@ticketid", dTO.ticketid),
                    new SqlParameter("@createddate", dTO.createddate),
                    new SqlParameter("@modifydate", dTO.modifydate),
                    new SqlParameter("@status", dTO.status),
                    new SqlParameter("@assigned", dTO.assigned),
                    new SqlParameter("@ComplainerName", dTO.ComplainerName),
                    new SqlParameter("@ContactNo", dTO.ContactNo),
                    new SqlParameter("@EmailId", dTO.EmailId),
                    new SqlParameter("@Flat_No", dTO.Flat_No),
                    new SqlParameter("@Block_No", dTO.Block_No),
                    new SqlParameter("@Colony_Name", dTO.Colony_Name),
                    new SqlParameter("@City_State", dTO.City_State),
                    new SqlParameter("@City", dTO.City),
                    new SqlParameter("@Ward_Name", dTO.Ward_Name),
                    new SqlParameter("@WardNo", dTO.WardNo),
                    new SqlParameter("@Ward_Locality", dTO.Ward_Locality),
                    new SqlParameter("@Zone", dTO.Zone),
                    new SqlParameter("@Parsad_Name", dTO.Parsad_Name),
                    new SqlParameter("@ZonalIncharge_Name", dTO.ZonalIncharge_Name),
                    new SqlParameter("@Zonal_Incharge_Number", dTO.Zonal_Incharge_Number),
                    new SqlParameter("@Type_Of_Complaint", dTO.Type_Of_Complaint),
                    new SqlParameter("@Issue", dTO.Issue),
                    new SqlParameter("@tat", dTO.tat),
                    new SqlParameter("@comment", dTO.comment),
                    new SqlParameter("@file_path", dTO.file_path)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Sp_Complaint, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
