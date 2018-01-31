using System;
using System.Net.Http;
using System.Threading.Tasks;
using FrHello.TencentYunCosSDK.Common;

namespace FrHello.TencentYunCosSDK.Service
{
    public class ServiceInvoker
    {
        private static readonly HttpClient _client;

        static ServiceInvoker()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromMilliseconds(ConstData.DefaultHttpRequestTimeOut);
            _client.BaseAddress = new Uri("service.cos.myqcloud.com");
        }

        /// <summary>
        /// 获取请求者名下的所有存储空间列表（Bucket list）。
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetService()
        {
            using (var response = await _client.GetAsync(string.Empty))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }
    }
}
