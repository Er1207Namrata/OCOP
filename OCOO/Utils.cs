using System.Data;

namespace OCOO
{
    public static class Utils
    {
        public static string GetFileName(string filename) => System.IO.Path.GetFileName(filename);

        public static double GetSum(DataTable dataTable, string fieldName)
        {
            double sum = 0.0;
            if (dataTable != null && !string.IsNullOrEmpty(fieldName) && dataTable.Columns.Contains(fieldName))
                foreach (DataRow row in dataTable.Rows)
                {
                    object value = row[fieldName];
                    if (value != DBNull.Value)
                    {
                        double fieldValue;
                        if (double.TryParse(value.ToString(), out fieldValue))
                            sum += fieldValue;
                        int _fieldValue;
                        if (int.TryParse(value.ToString(), out _fieldValue))
                            sum += _fieldValue;
                    }
                }
            else
                sum = -1;

            return Math.Round(sum,2);
        }
    }

}
