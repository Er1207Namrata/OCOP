using System.Data.SqlClient;
using System.Data;
using System.Net;
using System;

namespace OCOO.Models
{
    public class PaidBillReport:Menu
    {
        public string? Pk_YearId { get; set; }  
        public string? BillStatus { get; set; }  
        public DataSet GetZoneWisePaidBills()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_ZoneId",Pk_ZoneId=="0"?null:Pk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@Month",Fk_MonthId=="0"?null:Fk_MonthId),
                    new SqlParameter("@Year",Pk_YearId=="0"?null:Pk_YearId),
                    new SqlParameter("@BillStatus",BillStatus),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@Pk_EmpId", Pk_EmployeeId=="0"?null:Pk_EmployeeId),
                    new SqlParameter("@IsBilled", IsBilled),
                    new SqlParameter("@Months", Months),
                    new SqlParameter("@Years", Years),
                };
                ds = Connection.ExecuteQuery(ProcedureName.ZoneWisePaidBills, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet FirmwisePaidBills()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                   new SqlParameter("@Fk_FirmId",Fk_FirmId=="0"?null:Fk_FirmId),
                    new SqlParameter("@BillStatus",BillStatus),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@IsBilled", IsBilled),
                    new SqlParameter("@Months", Months),
                    new SqlParameter("@Years", Years),


                };
                ds = Connection.ExecuteQuery(ProcedureName.FirmwisePaidBills, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet ReadNotification()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                   new SqlParameter("@Fk_userTypeId",Fk_UsertypeId),
                    new SqlParameter("@Fk_EmpId", Fk_FirmId),


                };
                ds = Connection.ExecuteQuery(ProcedureName.ReadNotification, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
