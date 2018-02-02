using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FrHello.TencentYunCosSDK.Authorization;
using FrHello.TencentYunCosSDK.Common;

namespace FrHello.TencentYunCosSDK.Service
{
    public class ServiceInvoker
    {
        /// <summary>
        /// 获取请求者名下的所有存储空间列表（Bucket list）。
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetService(CancellationToken cancellationToken = default (CancellationToken))
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(ConstData.DefaultHttpRequestTimeOut),
                BaseAddress = new Uri("http://service.cos.myqcloud.com")
            };

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Sign.Signature());

            using (var response = await client.GetAsync(string.Empty, cancellationToken))
            {
                var s = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }
    }
}
