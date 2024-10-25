using System.Data;

namespace OCOO.Models
{
    public class Dashboard:Menu
    {
       public  List<CompliantList>? BarChartList { get; set; }

        public DataSet GetAdminDashbord()
        {
            try
            {
                DataSet dataSet = new DataSet();
                dataSet = Connection.ExecuteQuery(ProcedureName.AdminDashboardData);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
