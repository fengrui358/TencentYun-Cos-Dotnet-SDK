using System;
using FrHello.TencentYunCosSDK.Common;
using Xunit;

namespace FrHello.TencentYunCosSDK.Test.Common
{
    public class ExtensionTest
    {
        [Fact]
        public void ToUnixTimeTest()
        {
            var d = new DateTime(1975, 1, 3, 5, 3, 2, DateTimeKind.Utc).ToUnixTime();
            Assert.Equal(157957382000L, d);
        }
    }
}
