using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace OCOO.Models.AdminMasters
{
    public class ZoneModel : Menu
    {
        public string?  Pk_ContractId { get; set; }
        public string?  ContractAmt { get; set; }
        public int?  ContractYears { get; set; }
        public string? ContractFrom { get; set; }
        public string? ContractTo { get; set; }
        public string? MinLDCharge { get; set; }
        public string? TotalCapacity { get; set; }
        public string? NoOfStp { get; set; }
        public string? YearWiseIncreament { get; set; }
        public string? AdditionalContractYear { get; set; }
        public string? AdditionalContractYearDate { get; set; }


        public DataSet GetZoneContract()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Pk_ContractId", Pk_ContractId),
                    new SqlParameter("@Fk_ZoneId", Fk_ZoneId),
                    new SqlParameter("@ContractAmt", ContractAmt),
                    new SqlParameter("@ContractYear", ContractYears),
                    new SqlParameter("@ContractTo", ContractTo),
                    new SqlParameter("@ContractFrom", ContractFrom),
                    new SqlParameter("@MinLDCharge", MinLDCharge),
                    new SqlParameter("@AddedBy", AddedBy),
                    new SqlParameter("@TotalCapacity", TotalCapacity),
                    new SqlParameter("@NoOfStp", NoOfStp),
                    new SqlParameter("@OpCode", OpCode),
                    new SqlParameter("@YearWiseIncreament", YearWiseIncreament),
                    new SqlParameter("@AdditionalContractYear", AdditionalContractYear),
                    new SqlParameter("@AdditionalContractYearDate", AdditionalContractYearDate)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SP_ZoneMaster, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
