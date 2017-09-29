namespace Morpher.WebService.V3.Client.UnitTests
{
    using System;
    using System.Collections.Specialized;
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
            };;

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
    }
}