using BCE.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Repo
{
    public class DebtorRepo
    {
        private static readonly Lazy<DebtorRepo> lazy = new Lazy<DebtorRepo>(() => new DebtorRepo());
        public static DebtorRepo Instance { get { return lazy.Value; } }


        
    }
}
