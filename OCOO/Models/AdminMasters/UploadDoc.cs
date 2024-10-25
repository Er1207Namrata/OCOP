using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models.AdminMasters
{
    public class UploadDoc: Menu
    {

        public string? PK_UploadDocId { get; set; }
        public string? UploadDocName { get; set; }
        public DataSet ManageUPloadDoc()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@PK_UploadDocId",PK_UploadDocId),
                    new SqlParameter("@UploadDocName", UploadDocName),
                };
                ds = Connection.ExecuteQuery(ProcedureName.UPloadDocMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
