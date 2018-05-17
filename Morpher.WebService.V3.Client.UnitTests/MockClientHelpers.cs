namespace Morpher.WebService.V3.Client.UnitTests
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using Moq;

    public static class MockClientHelpers
    {
        public static MorpherClient ExceptionClient(string exceptionText = ExceptionText.MissedParameter, HttpStatusCode statusCode = (HttpStatusCode)400)
        {
            var exception = new WebException("Exception", null, WebExceptionStatus.ReceiveFailure,
                WebResponseMock.CreateWebResponse(statusCode,
                    new MemoryStream(Encoding.UTF8.GetBytes(exceptionText))));
            return ExceptionClient(exception);
        }


        public static MorpherClient ExceptionClient(Exception exception)
        {
            IWebClient webClient = BuildWebClientThatThrows(exception);
            return NewMorpherClientInject(webClient);
        }

        static IWebClient BuildWebClientThatThrows(Exception exception)
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Throws(exception);
            return webClient.Object;
        }

        public static MorpherClient NewMorpherClientInject(IWebClient webClient)
        {
            return new MorpherClient(null, null, webClient);
        }
    }
}
