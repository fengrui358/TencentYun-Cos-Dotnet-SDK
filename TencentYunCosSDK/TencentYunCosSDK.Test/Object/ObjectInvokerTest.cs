using FrHello.TencentYunCosSDK.Object;
using Xunit;

namespace FrHello.TencentYunCosSDK.Test.Object
{
    public class ObjectInvokerTest
    {
        [Fact]
        public void GetObjectTest()
        {
            var r = ObjectInvoker.GetObject().ConfigureAwait(false).GetAwaiter().GetResult();

        }
    }
}
