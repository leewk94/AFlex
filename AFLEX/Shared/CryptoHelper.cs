using AFLEX.Constants;
using AFLEX.Enumerations;
using AFLEX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Shared
{
    public class CryptoHelper
    {
        public static CryptoResponseModel Encrypt(string input)
        {
            CryptoResponseModel responseModel = new CryptoResponseModel();

            try
            {
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(MasterConstant.MASTER_CRYPTOKEY);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();

                responseModel = new CryptoResponseModel
                {
                    Status = CryptoStatusCodeEnum.Success,
                    Value = System.Convert.ToBase64String(resultArray, 0, resultArray.Length)
                };
            }
            catch(Exception ex)
            {
                responseModel = new CryptoResponseModel
                {
                    Status = CryptoStatusCodeEnum.Fail,
                    Message = ex.Message.ToString()
                };
            }

            return responseModel;
        }
        public static CryptoResponseModel Decrypt(string input)
        {
            CryptoResponseModel responseModel = new CryptoResponseModel();
            try
            {
                byte[] inputArray = System.Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(MasterConstant.MASTER_CRYPTOKEY);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return

                responseModel = new CryptoResponseModel
                {
                    Status = CryptoStatusCodeEnum.Success,
                    Value = UTF8Encoding.UTF8.GetString(resultArray)
                };
            
            }
            catch(Exception ex)
            {
                responseModel = new CryptoResponseModel
                    {
                        Status = CryptoStatusCodeEnum.Fail,
                        Message = ex.Message.ToString()
                };
            }
            return responseModel;
        }
    }
}
