using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class ComplaintsRecevied:Menu
    {
        public int PK_FbillId { get; set; } 
        public string? UserId { get; set; } 
       
        public DataSet GetComplaintsRecevied()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsRecevied, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetComplaintsReceviedForFirm()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsReceviedForFirm, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetComplaintsResolved()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsResolved, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetComplaintsResolvedForFirm()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsResolvedForFirm, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetComplaintsPending()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsPending, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetComplaintsPendingForFirm()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_ZoneId",Fk_ZoneId == "0" ? null : Fk_ZoneId),
                    new SqlParameter("@Fk_FirmId",Fk_FirmId == "0" ? null : Fk_FirmId),
                    new SqlParameter("@Fk_STPId",Fk_STPId == "0" ? null : Fk_STPId),
                    new SqlParameter("@Fk_MonthId",Fk_MonthId == "0" ? null : Fk_MonthId),
                    new SqlParameter("@UserId",UserId == "0" ? null : UserId),
                    new SqlParameter("@FromDate",FromDate == "0" ? null : FromDate),
                    new SqlParameter("@ToDate",ToDate == "0" ? null : ToDate)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ComplaintsPendingForFirm, para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
