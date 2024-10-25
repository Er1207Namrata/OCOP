using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models.FirmMasters
{
    public class DeclineDoc:Menu
    {
        public IFormFile? Files { get; set; }
        public string? Doc_url { get; set; }
        public string? Fk_DocId { get; set; }
        public string? Remark { get; set; }
        public string? FK_FbillId { get; set; }
        public string? Fk_FBDocId { get; set; }

        public DataSet GetDeclineDoc()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                   
                    new SqlParameter("@Fk_FirmId",Fk_FirmId),
                };
                ds = Connection.ExecuteQuery(ProcedureName.DeclineDoc, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet UploadDeclineDoc()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                   
                    new SqlParameter("@FK_FbillId",FK_FbillId),
                    new SqlParameter("@Fk_UploadDocId",Fk_DocId),
                    new SqlParameter("@Doc_Url",Doc_url),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Fk_FBDocId",Fk_FBDocId),
                    new SqlParameter("@Remarks",Remark),
                };
                ds = Connection.ExecuteQuery(ProcedureName.UploadDecDoc, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
