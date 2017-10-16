namespace Morpher.WebService.V3.Client.UnitTests
{
    using System.Collections.Specialized;
    using System.Net;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GarbageInResponseBody()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns("< !DOCTYPE html>");
            var morpherClient = MockClientHelpers.NewMorpherClientInject(webClient.Object);
            Assert.Throws<InvalidServerResponseException>(() => morpherClient.Russian.Parse("кошка"));
        }

        [Test]
        public void InvalidServerResponseException()
        {
            Assert.Throws<InvalidServerResponseException>(() =>
                MockClientHelpers.ExceptionClient("Any", (HttpStatusCode)401).Russian.UserDict.GetAll());
        }
    }
}
