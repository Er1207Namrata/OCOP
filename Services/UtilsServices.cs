using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class UtilsServices
    {
        public static List<string> GetDatesQuarterWise(List<string> datesQuarterWise)
        {
            List<string> _Dates = new List<string>();
            foreach (string date in datesQuarterWise)
            {
                string[] Dates = date.Split("/");
                int quarter = Convert.ToInt32(Dates[0].Replace("Q", ""));
                int year = Convert.ToInt32(Dates[1]);
                _Dates.AddRange(GetMonthsForQuarter(quarter, year).ToList());
            }
            return _Dates;
        }
        public static string[] GetMonthsForQuarter(int quarter, int year)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentException("Quarter must be between 1 and 4");
            }
            string[] months = new string[3];
            switch (quarter)
            {
                case 1:
                    months[0] = "01/" + year;
                    months[1] = "02/" + year;
                    months[2] = "03/" + year;
                    break;
                case 2:
                    months[0] = "04/" + year;
                    months[1] = "05/" + year;
                    months[2] = "06/" + year;
                    break;
                case 3:
                    months[0] = "07/" + year;
                    months[1] = "08/" + year;
                    months[2] = "09/" + year;
                    break;
                case 4:
                    months[0] = "10/" + year;
                    months[1] = "11/" + year;
                    months[2] = "12/" + year;
                    break;
            }
            return months;
        }
    }
}
