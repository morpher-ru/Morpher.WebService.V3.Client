namespace Morpher.WebService.V3.Client.UnitTests
{
    using System.Net;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void InvalidUrlThrows()
        {
            Assert.Throws<WebException>(() => new MorpherClient(null, "http://dns-error-fjeqweWe3cu.com").Russian.Parse("кошка"));
        }


    }
}
