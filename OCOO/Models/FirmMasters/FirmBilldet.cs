using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class FirmBilldet
    {

        public string? BillingDate { get; set; }
        public string? WaterDischarge { get; set; }
        public string? ComplaintReceived { get; set; }
        public string? ComplaintResolved { get; set; }
        public string? Pk_BillingId { get; set; }
        public string? InHouseBOD { get; set; }
        public string? InHouseCOD { get; set; }
        public string? InHouseFC { get; set; }
        public string? InHouseTSS { get; set; }
        public string? ThirdPartyBOD { get; set; }
        public string? ThirdPartyCOD { get; set; }
        public string? ThirdPartyFC { get; set; }
        public string? ThirdPartyTSS { get; set; }
        public string? UPPCBBOD { get; set; }
        public string? UPPCBCOD { get; set; }
        public string? UPPCBFC { get; set; }
        public string? UPPCBTSS { get; set; }
        public bool IsInhouse { get; set; }
        public bool IsUPPCB { get; set; }
        public bool IsTPI { get; set; }
        public string? STPName { get; set; }
        public string? InHouseBODColor { get; set; }
        public string? InHouseCODColor { get; set; }
        public string? InHouseFCColor { get; set; }
        public string? InHouseTSSColor { get; set; }
        public string? TPIBODColor { get; set; }
        public string? TPICODColor { get; set; }
        public string? TPIFCColor { get; set; }
        public string? TPITSSColor { get; set; }
        public string? UPPCBBODColor { get; set; }
        public string? UPPCBCODColor { get; set; }
        public string? UPPCBFCColor { get; set; }
        public string? UPPCBTSSColor { get; set; }
        public DataSet GetBilling()
        {
            try
            {
                SqlParameter[] para =
                {

                    new SqlParameter("@Pk_BillingId", Pk_BillingId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_GetBilling, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
