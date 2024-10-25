using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models
{
    public class Distric : Menu
    {
        public string? Pk_DistricId { get; set; }
        public string? DistricName { get; set; }
        public string? Fk_StateId { get; set; }
        public string? Fk_DivisionId { get; set; }

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
        public DataSet ManageDistric()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_DistrictId",Pk_DistricId),
                    new SqlParameter("@Fk_StateId",Fk_StateId),
                    new SqlParameter("@Fk_DivisionId",Fk_DivisionId),
                    new SqlParameter("@Pk_UserId",Pk_UserId),
                    new SqlParameter("@DistrictName",DistricName),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),


                };
                ds = Connection.ExecuteQuery(ProcedureName.DistrictMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
