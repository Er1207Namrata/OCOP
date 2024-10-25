using System.Data.SqlClient;
using System.Data;
using OCOO.Models;

namespace OCOO.Models
{
    public class Permission : Menu
    {
        public long[]? itemId { get; set; }
        public ItemList1? itemList { get; set; }

        public DataSet GetEmployee()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetlMenuDdl()
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Value", MenuId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }

    public class ItemList1
    {
        public long[]? itemId { get; set; }
    }
}
