using AFLEX.Enumerations;
using AFLEX.Model;
using AFLEX.Repo;
using BCE.AutoCount.ARAP.APCNAnalysis;
using BCE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Domain
{
    class TriggerMaintenanceDomain
    {
        private static readonly Lazy<TriggerMaintenanceDomain> lazy = new Lazy<TriggerMaintenanceDomain>(() => new TriggerMaintenanceDomain());
        public static TriggerMaintenanceDomain Instance { get { return lazy.Value; } }


        public bool InsertTriggerMaintenance(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            var TriggerMaintenanceList = TriggerMaintenanceRepo.Instance.GetTriggerMaintenance_ByMaintenanceValue(dbSetting, category, actionType, maintenanceValue.Replace("'", "''"));

            var responseModel = new DBResponseModel();

            if (TriggerMaintenanceList.Rows.Count != 0)
            {
                // update
                responseModel = TriggerMaintenanceRepo.Instance.UpdateTriggerMaintenance(dbSetting, category, actionType, maintenanceValue);

                if(responseModel.Status == DBStatusCodeEnum.Success)
                    return true;
                else
                {
                    // fail, insert into log
                    return false;
                }
            }
            else
            {
                // insert
                responseModel = TriggerMaintenanceRepo.Instance.CreateTriggerMaintenance(dbSetting, category, actionType, maintenanceValue);

                if (responseModel.Status == DBStatusCodeEnum.Success)
                    return true;
                else
                {
                    // fail, insert into log
                    return false;
                }
            }
        }
        
        public bool DeleteTriggerMaintenance_ByMaintenanceValue(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            var TriggerMaintenanceList = TriggerMaintenanceRepo.Instance.GetTriggerMaintenance_ByMaintenanceValue(dbSetting, category, actionType, maintenanceValue.Replace("'", "''"));

            var responseModel = new DBResponseModel();

            if (TriggerMaintenanceList.Rows.Count != 0)
            {
                //Delete the record
                responseModel = TriggerMaintenanceRepo.Instance.DeleteTriggerMaintenance_ByMaintenanceValue(dbSetting, category, maintenanceValue);

                if (responseModel.Status == DBStatusCodeEnum.Success)
                {
                    return true;
                }
                else
                {
                    // fail, insert into log
                    return false;
                }
            }
            else
                return true;
            
        }

    }
}
