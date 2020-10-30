using AFLEX.Constants.Database;
using AFLEX.Enumerations;
using AFLEX.Model;
using BCE.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Shared
{
    class DBHelper
    {
        private static readonly Lazy<DBHelper> lazy = new Lazy<DBHelper>(() => new DBHelper());
        public static DBHelper Instance { get { return lazy.Value; } }

        public DBResponseModel Execute(DBSetting dbSetting, string query, Dictionary<string, object> _params = null)
        {
            DBResponseModel response = new DBResponseModel();
            using (SqlConnection conn = new SqlConnection(dbSetting.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Connection.Open();

                    if(_params.Count != 0)
                    {
                        foreach(var param in _params)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    cmd.ExecuteNonQuery();
                    response = new DBResponseModel
                    {
                        Status = DBStatusCodeEnum.Success
                    };
                }
                catch (Exception ex)
                {
                    response = new DBResponseModel
                    {
                        Status = DBStatusCodeEnum.Fail,
                        Message = ex.Message.ToString(),
                    };
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return response;
        }

        public string QueryScript(string tableName, string query)
        {
            return string.Concat(string.Format(DBScripts.MASTER_CHECKEXISTENCE,tableName), query);
        }
    }
}
