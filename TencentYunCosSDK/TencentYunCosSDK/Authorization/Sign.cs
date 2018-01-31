using System;
using System.Security.Cryptography;
using System.Text;
using FrHello.TencentYunCosSDK.Common;

namespace FrHello.TencentYunCosSDK.Authorization
{
    public class Sign
    {
        public static string Signature(int appId, string secretId, string secretKey, long expired, string fileId, string bucketName)
        {
            if (secretId == "" || secretKey == "")
            {
                return "-1";
            }
            var now = DateTime.Now.ToUnixTime() / 1000;
            var rand = new Random();
            var rdm = rand.Next(Int32.MaxValue);
            var plainText = "a=" + appId + "&k=" + secretId + "&e=" + expired + "&t=" + now + "&r=" + rdm + "&f=" + fileId + "&b=" + bucketName;

            using (var mac = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var pText = Encoding.UTF8.GetBytes(plainText);
                var all = new byte[hash.Length + pText.Length];
                Array.Copy(hash, 0, all, 0, hash.Length);
                Array.Copy(pText, 0, all, hash.Length, pText.Length);
                return Convert.ToBase64String(all);
            }
        }

        public static string Signature(int appId, string secretId, string secretKey, long expired, string bucketName)
        {
            return Signature(appId, secretId, secretKey, expired, "", bucketName);
        }

        public static string SignatureOnce(int appId, string secretId, string secretKey, string remotePath, string bucketName)
        {
            var fileId = "/" + appId + "/" + bucketName + remotePath;
            return Signature(appId, secretId, secretKey, 0, fileId, bucketName);
        }

        public static string Signature()
        {
            return
                "q6556-erew=a34e";

            return
                "q-sign-algorithm=sha1&q-ak=AKIDQjz3ltompVjBni5LitkWHFlFpwkn9U5q&q-sign-time=1480932292;1481012298&q-key-time=1480932292;1481012298&q-header-list=host&q-url-param-list=versioning&q-signature=438023e4a4207299d87bb75d1c739c06cc9406bb";
        }
    }
}
