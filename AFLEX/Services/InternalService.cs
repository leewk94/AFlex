using AFLEX.Domain;
using AFLEX.Enumerations;
using AFLEX.Model;
using BCE.AutoCount;
using BCE.AutoCount.ARAP.APCNAnalysis;
using BCE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX
{
    public class InternalService
    {
        private static readonly Lazy<InternalService> lazy = new Lazy<InternalService>(() => new InternalService());
        public static InternalService Instance { get { return lazy.Value; } }


        #region Master
        public string GetAPITokenFilePath()
        {
            return MasterDomain.Instance.GetAPITokenFilePath();
        }

        public bool CheckAPITokenFile(DBSetting dbSetting)
        {
            return MasterDomain.Instance.CheckAPITokenFile(dbSetting);
        }

        public void InsertDBDetailsToTokenFile(DBSetting dbSetting, string serverType, string serverName, string dbName, string filePath)
        {
            MasterDomain.Instance.InsertDBDetailsToTokenFile(dbSetting, serverType, serverName, dbName, filePath);
        }

        public CryptoResponseModel EncryptTokenFile(string text)
        {
            return MasterDomain.Instance.Encryption(text);
        }
        public CryptoResponseModel DecryptTokenFile(string text)
        {
            return MasterDomain.Instance.Decryption(text);
        }

        public bool CheckLoginAccess(DBSetting dbSetting)
        {
            return MasterDomain.Instance.GetLoginAcessCheck(dbSetting);
        }

        public bool CheckModulePermission(DBSetting dbSetting, CategoryEnum category)
        {
            return MasterDomain.Instance.GetModulePermission(dbSetting, category);
        }

        public bool GetRealTimeSyncStatus(DBSetting dbSetting, SyncDataTypeEnum type)
        {
            return MasterDomain.Instance.GetRealTimeSyncStatus(dbSetting, type);
        }
        public string GetServiceConfigureDetail(DBSetting dbSetting, SyncDataTypeEnum type)
        {
            return MasterDomain.Instance.GetServiceConfigureDetail(dbSetting, type);
        }

        public bool InsertTriggerMaintenance(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            return TriggerMaintenanceDomain.Instance.InsertTriggerMaintenance(dbSetting, category, actionType, maintenanceValue);
        }

        public bool DeleteTriggerMaintenance_ByMaintenanceValue(DBSetting dbSetting, CategoryEnum category, ActionTypeEnum actionType, string maintenanceValue)
        {
            return TriggerMaintenanceDomain.Instance.DeleteTriggerMaintenance_ByMaintenanceValue(dbSetting, category, actionType, maintenanceValue);
        }

        #endregion
    }
}
