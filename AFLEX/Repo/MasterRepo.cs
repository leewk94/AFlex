using AFLEX.Constants;
using AFLEX.Constants.Database;
using AFLEX.Enumerations;
using AFLEX.Shared;
using BCE.AutoCount.GL.LastYearBalance;
using BCE.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Repo
{
    class MasterRepo
    {
        private static readonly Lazy<MasterRepo> lazy = new Lazy<MasterRepo>(() => new MasterRepo());
        public static MasterRepo Instance { get { return lazy.Value; } }

        public DataTable GetServiceSyncDataValue(DBSetting dbSetting, SyncDataTypeEnum dataType)
        {
            string sQuery = string.Format(DBScripts.MASTER_GETSERVICESYNCDATA_BYDATATYPE, dataType.ToString());
            sQuery = DBHelper.Instance.QueryScript(DBTables.TABLE_FLEX_SERVICESYNCDATA, sQuery);
            DataTable dt = dbSetting.GetDataTable(sQuery, false);

            return dt;
        }

    }
}
