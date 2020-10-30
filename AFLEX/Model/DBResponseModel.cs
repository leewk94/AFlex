using AFLEX.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Model
{
    public class DBResponseModel
    {
        public DBStatusCodeEnum Status { get; set; }
        public string Message { get; set; }
    }
}
