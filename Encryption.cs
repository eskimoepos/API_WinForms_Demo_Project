using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Win_Forms_Client
{
    static class Encryption

    {
        public static string EncryptString(string strKey, string strValue)
        {
            byte[] intputVector = {
                65,
                110,
                68,
                26,
                69,
                178,
                200,
                219
            };

            //Dim MS As New MemoryStream
            //Dim DES As New DESCryptoServiceProvider
            //Dim CS As CryptoStream = Nothing

            try
            {
                if (strKey.Length != 8)
                    throw new Exception("Key length must equal 8.");
                if ((strValue.Length <= 0))
                    return "";
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strValue);
                byte[] keyByteArray = Encoding.UTF8.GetBytes(strKey);

                using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider())
                {
                    using (MemoryStream MS = new MemoryStream())
                    {
                        using (CryptoStream CS = new CryptoStream(MS, DES.CreateEncryptor(keyByteArray, intputVector), CryptoStreamMode.Write))
                        {
                            CS.Write(inputByteArray, 0, inputByteArray.Length);
                            CS.FlushFinalBlock();
                            return Convert.ToBase64String(MS.ToArray());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string DecryptString(string strKey, string strValue)
        {
            byte[] intputVector = {
                65,
                110,
                68,
                26,
                69,
                178,
                200,
                219
            };
            //Dim MS As New MemoryStream
            //Dim DES As New DESCryptoServiceProvider
            //Dim CS As CryptoStream = Nothing


            try
            {
                if (strKey.Length != 8)
                    throw new Exception("Key length must equal 8.");
                if ((strValue.Length <= 0))
                    return "";
                byte[] inputByteArray = Convert.FromBase64String(strValue);
                byte[] keyByteArray = Encoding.UTF8.GetBytes(strKey);

                using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider())
                {
                    using (MemoryStream MS = new MemoryStream())
                    {
                        using (CryptoStream CS = new CryptoStream(MS, DES.CreateDecryptor(keyByteArray, intputVector), CryptoStreamMode.Write))
                        {
                            CS.Write(inputByteArray, 0, inputByteArray.Length);
                            CS.FlushFinalBlock();
                            return Encoding.UTF8.GetString(MS.ToArray());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
