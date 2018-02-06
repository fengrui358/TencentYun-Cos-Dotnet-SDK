using System;
using System.IO;
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

        private static string HMACSHAString(string key, string msg)
        {
            using (var mac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(msg));
                var strHashData = BitConverter.ToString(hash);
                //替换-
                strHashData = strHashData.Replace("-", "");
                var strResult = strHashData.ToLower();

                return strResult;

                //var pText = Encoding.UTF8.GetString(hash);

                //return Convert.ToBase64String(hash); ;

                //var all = new byte[hash.Length + pText.Length];
                //Array.Copy(hash, 0, all, 0, hash.Length);
                //Array.Copy(pText, 0, all, hash.Length, pText.Length);
                //return Convert.ToBase64String(all);
            }
        }

        public static string Signature()
        {
            var httpMethod = "get";

            var algorithm = "sha1";
            var secretId = "AKIDQjz3ltompVjBni5LitkWHFlFpwkn9U5q";
            var secretKey = "BQYIM75p8x0iWVFSIgqEKwFprpRSVHlz";
            var now = DateTime.Now.ToUnixTime() / 1000;
            var endTime = DateTime.Now.AddDays(1).ToUnixTime() / 1000;
            //var keyTime = $"{now};{endTime}";
            var keyTime = "1417773892;1417853898";

            var signKey = HMACSHAString(keyTime, secretKey);

            //var httpString = $"{httpMethod}\n/test123\nhost=tigo-private-1251827262.cos.ap-chengdu.myqcloud.com\n";
            var httpString = @"get\n/testfile\n\nhost=bucket1-1254000000.cos.ap-beijing.myqcloud.com&range=bytes%3D0-3\n";

            var stringToSign = $@"{algorithm}\n{keyTime}\n{GetSHA1(httpString)}\n";

            var signature = HMACSHAString(signKey, stringToSign);

            var authorization = $"q-sign-algorithm={algorithm}&q-ak={secretId}&q-sign-time={keyTime}&q-key-time={keyTime}&q-header-list=host&q-url-param-list=&q-signature={signature}";
            var authorization2 =
                "q-sign-algorithm=sha1&q-ak=AKIDQjz3ltompVjBni5LitkWHFlFpwkn9U5q&q-sign-time=1417773892;1417853898&q-key-time=1417773892;1417853898&q-header-list=host;range&q-url-param-list=&q-signature=4b6cbab14ce01381c29032423481ebffd514e8be";

            return authorization;

            return
                "q-sign-algorithm=sha1&q-ak=&q-sign-time=1480932292;1481012298&q-key-time=1480932292;1481012298&q-header-list=host&q-url-param-list=versioning&q-signature=438023e4a4207299d87bb75d1c739c06cc9406bb";
        }

        public static string GetSHA1(string httpString)
        {
            SHA1CryptoServiceProvider osha1 = new SHA1CryptoServiceProvider();
            try
            {
                var arrbytHashValue = osha1.ComputeHash(Encoding.UTF8.GetBytes(httpString)); //计算指定Stream 对象的哈希值

                var strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                var strResult = strHashData.ToLower();

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
