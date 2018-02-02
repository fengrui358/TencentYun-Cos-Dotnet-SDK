using FrHello.TencentYunCosSDK.Service;
using Xunit;

namespace FrHello.TencentYunCosSDK.Test.Service
{
    public class ServiceInvokerTest
    {
        [Fact]
        public void GetServiceTest()
        {
            var r = ServiceInvoker.GetService().ConfigureAwait(false).GetAwaiter().GetResult();

        }
    }
}
