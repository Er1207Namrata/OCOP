using System.Data.SqlClient;
using System.Data;

namespace OCOO.Models.AdminMasters
{
    public class MoM:Menu
    {
        public string? LetterNo { get; set; }
        public string? MeetingDate { get; set; }
        public string? Pk_MomId { get; set; }
        public string? DocUrl { get; set; }
        public IFormFile? MoMDoc { get; set; }
        public DataSet saveMom()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@Pk_MomId", Pk_MomId),
                new SqlParameter("@MeetingDate", MeetingDate),
                new SqlParameter("@DocUrl", DocUrl),
                new SqlParameter("@LetterNo", LetterNo),
                new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@OpCode", OpCode)
            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.MoM, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
