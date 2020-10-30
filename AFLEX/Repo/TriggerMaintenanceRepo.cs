using AFLEX.Constants;
using AFLEX.Constants.Database;
using AFLEX.Enumerations;
using AFLEX.Model;
using AFLEX.Shared;
using BCE.AutoCount.Invoicing;
using BCE.AutoCount.Invoicing.Sales.Quotation;
using BCE.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Repo
{
    class TriggerMaintenanceRepo
    {
        private static readonly Lazy<TriggerMaintenanceRepo> lazy = new Lazy<TriggerMaintenanceRepo>(() => new TriggerMaintenanceRepo());
        public static TriggerMaintenanceRepo Instance { get { return lazy.Value; } }


        public DataTable GetTriggerMaintenance_ByMaintenanceValue(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            string sQuery = string.Format(DBScripts.TRIGGERMAINTENANCE_GETLIST_BYMAINTENANCEVALUE, category.ToString().ToUpper(), maintenanceValue.ToString(),actionType.ToString().ToUpperInvariant());

            sQuery = DBHelper.Instance.QueryScript(DBTables.TABLE_TRIGGERMAINTENANCE, sQuery);

            DataTable dt = dbSetting.GetDataTable(sQuery, false);

            return dt;
        }

        public DBResponseModel UpdateTriggerMaintenance(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            string sQuery = string.Format(DBScripts.TRIGGERMAINTENANCE_UPDATE, DBParams.PARAM_MODIFIEDDATE, DBParams.PARAM_DOCTYPE, DBParams.PARAM_VALUE, DBParams.PARAM_STATUS);

            sQuery = DBHelper.Instance.QueryScript(DBTables.TABLE_TRIGGERMAINTENANCE, sQuery);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBParams.PARAM_MODIFIEDDATE, DateTime.Now);
            param.Add(DBParams.PARAM_DOCTYPE, category.ToString().ToUpper());
            param.Add(DBParams.PARAM_VALUE, maintenanceValue);
            param.Add(DBParams.PARAM_STATUS, actionType.ToString().ToUpper());

            var response = DBHelper.Instance.Execute(dbSetting, sQuery, param);
            return response;
        }

        public DBResponseModel CreateTriggerMaintenance(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            string sQuery = string.Format(DBScripts.TRIGGERMAINTENANCE_INSERT, DBParams.PARAM_DOCTYPE, DBParams.PARAM_VALUE, DBParams.PARAM_STATUS, DBParams.PARAM_CREATIONDATE, DBParams.PARAM_MODIFIEDDATE);

            sQuery = DBHelper.Instance.QueryScript(DBTables.TABLE_TRIGGERMAINTENANCE, sQuery);

            DateTime dateNow = DateTime.Now;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBParams.PARAM_DOCTYPE, category.ToString().ToUpper());
            param.Add(DBParams.PARAM_VALUE, maintenanceValue);
            param.Add(DBParams.PARAM_STATUS, actionType.ToString().ToUpper());
            param.Add(DBParams.PARAM_CREATIONDATE, dateNow);
            param.Add(DBParams.PARAM_MODIFIEDDATE, dateNow);

            var response = DBHelper.Instance.Execute(dbSetting, sQuery, param);
            return response;
        }

        public DBResponseModel DeleteTriggerMaintenance_ByMaintenanceValue(DBSetting dbSetting, CategoryEnum category, string maintenanceValue)
        {
            string sQuery = string.Format(DBScripts.TRIGGERMAINTENANCE_DELETE_BYMAINTENANCEVALUE, DBParams.PARAM_DOCTYPE, DBParams.PARAM_VALUE);

            sQuery = DBHelper.Instance.QueryScript(DBTables.TABLE_TRIGGERMAINTENANCE, sQuery);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBParams.PARAM_DOCTYPE, category.ToString().ToUpper());
            param.Add(DBParams.PARAM_VALUE, maintenanceValue);

            var response = DBHelper.Instance.Execute(dbSetting, sQuery, param);
            return response;
        }
    }
}
