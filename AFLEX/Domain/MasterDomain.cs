using AFLEX.Constants;
using AFLEX.Constants.Database;
using AFLEX.Enumerations;
using AFLEX.Model;
using AFLEX.Repo;
using AFLEX.Shared;
using BCE.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Domain
{
    public class MasterDomain
    {
        private static readonly Lazy<MasterDomain> lazy = new Lazy<MasterDomain>(() => new MasterDomain());
        public static MasterDomain Instance { get { return lazy.Value; } }


        public string GetAPITokenFilePath()
        {
            string sFilePath = string.Empty;

            string temppath = Environment.GetEnvironmentVariable(MasterConstant.MASTER_TEMP, EnvironmentVariableTarget.Machine);
            
            string sOriFilePath = string.Concat(temppath, "\\", MasterConstant.MASTER_TOKENFILENAME);

            if (Directory.Exists(MasterConstant.MASTER_EDJOBSERVICE_PATH_X86))
            {
                sFilePath = string.Concat(MasterConstant.MASTER_EDJOBSERVICE_PATH_X86, MasterConstant.MASTER_TOKENFILENAME);
            }
            else
            {
                sFilePath = string.Concat(MasterConstant.MASTER_EDJOBSERVICE_PATH_X64, MasterConstant.MASTER_TOKENFILENAME);
            }

            if (!File.Exists(sFilePath))
            {
                sFilePath = sOriFilePath;
            }

            return sFilePath;
        }

        public bool CheckAPITokenFile(DBSetting dbSetting)
        {
            string sFilePath = GetAPITokenFilePath();

            if (File.Exists(sFilePath))
            {
                FileInfo fInfo = new FileInfo(sFilePath);
                var size = fInfo.Length;

                if (System.Convert.ToInt32(size) > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                var response = MasterRepo.Instance.GetServiceSyncDataValue(dbSetting, SyncDataTypeEnum.API_TOKEN_VAL);
                
                if (response.Rows.Count != 0)
                    return true;
                else
                    return false;
            }
        }

        public void InsertDBDetailsToTokenFile(DBSetting dbSetting, string serverType, string serverName, string dbName, string filePath)
        {
            List<CustomerTokenModel> acc = new List<CustomerTokenModel>();

            if (CheckAPITokenFile(dbSetting))
            {
                var sTokenPath = GetAPITokenFilePath();

                if (File.Exists(sTokenPath))
                {
                    var text = File.ReadAllText(sTokenPath);

                    CryptoResponseModel descryptResponseModel = Decryption(text);

                    if (descryptResponseModel.Status == CryptoStatusCodeEnum.Success)
                    {
                        acc = JsonConvert.DeserializeObject<List<CustomerTokenModel>>(descryptResponseModel.Value);

                        CustomerTokenModel cust = new CustomerTokenModel
                        {
                            ServerType = serverType,
                            ServerName = serverName,
                            DBName = dbName,
                        };
                        acc.Add(cust);
                    }
                    else
                    {
                        // insert into log
                    }
                }
                else
                {
                    CustomerTokenModel cust = new CustomerTokenModel
                    {
                        ServerType = serverType,
                        ServerName = serverName,
                        DBName = dbName,
                    };
                    acc.Add(cust);
                }
            }
            else
            {
                CustomerTokenModel cust = new CustomerTokenModel
                {
                    ServerType = serverType,
                    ServerName = serverName,
                    DBName = dbName,
                };
                acc.Add(cust);
            }

            string jsontext = JsonConvert.SerializeObject(acc);

            var encryptResponseModel = Encryption(jsontext);

            if (encryptResponseModel.Status == CryptoStatusCodeEnum.Success)
            {
                if (File.Exists(filePath))
                    File.WriteAllText(filePath, encryptResponseModel.Value);
                else
                {
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(encryptResponseModel.Value);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                // fail insert into log
            }
        }

        public CryptoResponseModel Encryption(string text)
        {
            return CryptoHelper.Encrypt(text);
        }

        public CryptoResponseModel Decryption(string text)
        {
            return CryptoHelper.Decrypt(text);
        }


        public bool GetLoginAcessCheck(DBSetting dbSetting)
        {
            var response = MasterRepo.Instance.GetServiceSyncDataValue(dbSetting, SyncDataTypeEnum.ACCESSCHECK);

            if (response.Rows.Count != 0)
            {
                if (response.Rows[0][DBVariables.VAR_SERVICEVALUE].ToString().ToUpperInvariant().Equals(MasterConstant.MASTER_LOGINACCESSCHECK_SUCCESS))
                    return true;
            }

            return false;
        }

        public bool GetModulePermission(DBSetting dbSetting, CategoryEnum category)
        {
            var response = MasterRepo.Instance.GetServiceSyncDataValue(dbSetting, SyncDataTypeEnum.PERMISSION);

            if (response.Rows.Count != 0)
            {
                string sReadPermission = category.ToString() + PermissionEnum.Read.ToString();
                string sWritePermission = category.ToString() + PermissionEnum.Write.ToString();

                List<string> sPermissions = new List<string>();
                sPermissions = response.Rows[0][DBVariables.VAR_SERVICEVALUE].ToString().Split(',').ToList();

                if (sPermissions.Contains(sReadPermission) && sPermissions.Contains(sWritePermission))
                    return true;
            }
            
            return false; 
        }

        public bool GetRealTimeSyncStatus(DBSetting dbSetting, SyncDataTypeEnum type)
        {
            var response = MasterRepo.Instance.GetServiceSyncDataValue(dbSetting, type);

            if(response.Rows.Count != 0)
            {
                if (System.Convert.ToInt32(response.Rows[0][DBVariables.VAR_SERVICEVALUE2].ToString()).Equals(MasterConstant.MASTER_REALTIMESTATUSVALUE))
                    return true;
            }
            return false;
        }

        public string GetServiceConfigureDetail(DBSetting dbSetting, SyncDataTypeEnum type)
        {
            var response = MasterRepo.Instance.GetServiceSyncDataValue(dbSetting, type);
            var result = string.Empty;
            if (response.Rows.Count != 0)
                result = CryptoHelper.Decrypt(response.Rows[0][DBVariables.VAR_SERVICEVALUE].ToString());
            
            return result; 
        }
    }
}
