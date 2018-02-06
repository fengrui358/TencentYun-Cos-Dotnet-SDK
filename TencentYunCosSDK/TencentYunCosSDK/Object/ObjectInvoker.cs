using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FrHello.TencentYunCosSDK.Authorization;
using FrHello.TencentYunCosSDK.Common;

namespace FrHello.TencentYunCosSDK.Object
{
    public class ObjectInvoker
    {
        /// <summary>
        /// Get Object 接口请求可以在 COS 的 Bucket 中将一个文件（Object）下载至本地。该操作需要请求者对目标 Object 具有读权限或目标 Object 对所有人都开放了读权限（公有读）。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> GetObject(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(ConstData.DefaultHttpRequestTimeOut),
                BaseAddress = new Uri("http://tigo-private-1251827262.cos.ap-chengdu.myqcloud.com")
            })
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Sign.Signature());

                using (var response = await client.GetAsync("/test123", cancellationToken))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var s = await response.Content.ReadAsStreamAsync();
                        var bytes = new byte[s.Length];
                        await s.ReadAsync(bytes, 0, bytes.Length);

                        File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test123.txt"), bytes);
                    }
                }
            }

            return null;
        }
    }
}
