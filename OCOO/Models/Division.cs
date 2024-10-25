using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;

namespace OCOO.Models
{
    public class Division :Menu
    {
        public string? Pk_DivisinId { get; set; }
        public string? DivisionName { get; set; }

        public string? Pk_UserId { get; set; }
        public DataSet GetMasterData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Value", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ManageDivision()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_DivisionId",Pk_DivisinId),
                    new SqlParameter("@Pk_UserId",Pk_UserId),
                    new SqlParameter("@DivisionName",DivisionName),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),


                };
                ds = Connection.ExecuteQuery(ProcedureName.DivisionMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
