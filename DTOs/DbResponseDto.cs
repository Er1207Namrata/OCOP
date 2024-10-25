using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DbResponseDto<T>
    {
        public List<T>? Table { get; set; }
    }
}
