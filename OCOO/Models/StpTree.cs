using System.Data;
using System.Data.SqlClient;

namespace OCOO.Models
{
    public class StpTree : Menu
    {

        public List<ParentStpTree>? lstParent { get; set; }
        public List<ChildStpTree>? lstChild { get; set; }
        public List<LevelTree>? lstLevelTree { get; set; }
        public List<Drain>? lstDrain { get; set; }
        public List<SeverLength>? lstSeverLength { get; set; }

        public DataSet GetStpTree()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_StpId",Fk_STPId),

                };
                ds = Connection.ExecuteQuery(ProcedureName.Sp_STPTree, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


    }
    public class ParentStpTree
    {
        public string? Pk_Id { get; set; }
        public string? ParentName { get; set; }
        public string? Name { get; set; }
        public List<ChildStpTree> Children { get; set; } = new List<ChildStpTree>();

    }


    public class ChildStpTree
    {
        public string? Pk_Id { get; set; }
        public string? Fk_ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? Name { get; set; }

        public int LevelNo { get; set; }
        public List<ChildStpTree> Children { get; set; } = new List<ChildStpTree>();

   
      
      public List<ChildStpTree> Parent { get; set; } = new List<ChildStpTree>();
        public List<Drain> Drain { get; set; } = new List<Drain>();
        public List<SeverLength> SeverLength { get; set; } = new List<SeverLength>();

    }

    public class LevelTree
    {
        public long LevelNo { get; set; }

    }
    public class Drain
    {

        public string? ParentName { get; set; }
        public string? FlowFromDrain { get; set; }

        public string? Fk_ParentId { get; set; }
      
        public string? Name { get; set; }
        public string? dName { get; set; }

    }
    public class SeverLength
    {

        public string? ParentName { get; set; }
        public string? FlowFromSewer { get; set; }

        public string? Fk_ParentId { get; set; }
    
        public string? Name { get; set; }
        public string? sName { get; set; }

    }
}
