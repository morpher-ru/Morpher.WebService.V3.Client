namespace Morpher.WebService.V3.Client.UnitTests
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using V3.Ukrainian;

    [TestClass]
    public class Ukrainian
    {
        public string Result { get; } = @"
{
    ""Р"": ""теста"",
    ""Д"": ""тесту"",
    ""З"": ""теста"",
    ""О"": ""тестом"",
    ""М"": ""тесті"",
    ""К"": ""тесте"",
    ""рід"": ""Чоловічий""
}";

        public string ExceptionText { get; } = @"
{
  ""code"": 4,
  ""message"": ""Склонение числительных в declension не поддерживается. Используйте метод spell.""
}";

        [TestMethod]
        public void Parse_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(Result);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            DeclensionResult declensionResult = morpherClient.Ukrainian.Parse("тест");
            Assert.IsNotNull(declensionResult);
            Assert.AreEqual("тест", declensionResult.Nominative);
            Assert.AreEqual("теста", declensionResult.Genitive);
            Assert.AreEqual("тесту", declensionResult.Dative);
            Assert.AreEqual("теста", declensionResult.Accusative);
            Assert.AreEqual("тестом", declensionResult.Instrumental);
            Assert.AreEqual("тесті", declensionResult.Prepositional);
            Assert.AreEqual("тесте", declensionResult.Vocative);
            Assert.AreEqual(Gender.Masculine, declensionResult.Gender);
        }

        [TestMethod]
        [ExpectedException(typeof(MorpherWebServiceException),
            "Склонение числительных в declension не поддерживается. Используйте метод spell.")]
        public void Parse_MorpherWebServiceException()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());

            WebException exception = new WebException("Exception", null, WebExceptionStatus.ReceiveFailure,
                WebResponseMock.CreateWebResponse(HttpStatusCode.InternalServerError,
                    new MemoryStream(Encoding.UTF8.GetBytes(ExceptionText))));

            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Throws(exception);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            morpherClient.Ukrainian.Parse("exception here");
        }
    }
}