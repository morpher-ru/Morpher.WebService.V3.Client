namespace Morpher.WebService.V3.Client.UnitTests
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;
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
            MorpherClient morpherClient = MockClientHelpers.NewMorpherClientInject(webClient.Object);
            Assert.Throws<InvalidServerResponseException>(() => morpherClient.Russian.Parse("кошка"));
        }

        [Test]
        public void GarbageInExceptionBody()
        {
            MorpherClient morpherClient = MockClientHelpers.ExceptionClient("< !DOCTYPE html>", (HttpStatusCode) 402);
            Assert.Throws<DailyLimitExceededException>(() => morpherClient.Russian.Parse("exception here"));
        }

        [Test]
        public void InvalidServerResponseException()
        {
            MorpherClient client = MockClientHelpers.ExceptionClient("Any", (HttpStatusCode)401);
            Assert.That(() => client.Russian.UserDict.GetAll(), Throws.Exception);
        }

        [Test]
        public void UrlEncodeRoutines()
        {            
            Assert.AreEqual("%D0%9A%D0%BE%D0%BD%D1%81%D1%83%D0%BB%D1%8C%D1%82%D0%B0%D0%BD%D1%82%2B", HttpUtility.UrlEncode("Консультант+").ToUpper());
            Assert.AreEqual("%D0%9A%D0%BE%D0%BD%D1%81%D1%83%D0%BB%D1%8C%D1%82%D0%B0%D0%BD%D1%82%2B", Uri.EscapeDataString("Консультант+").ToUpper());
        }

    }
}
