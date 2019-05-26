namespace Morpher.WebService.V3.Client.UnitTests
{    
    using System.Collections.Specialized;
    using System.Net;
    using Moq;
    using NUnit.Framework;
    using V3.Qazaq;

    [TestFixture]
    class Qazaq
    {
        string DeclensionResultText { get; } = @"
{
    ""І"": ""тесттің"",
    ""Б"": ""тестке"",
    ""Т"": ""тестті"",
    ""Ш"": ""тесттен"",
    ""Ж"": ""тестте"",
    ""К"": ""тестпен"",    
    ""көпше"": {
        ""A"": ""тесттер"",
        ""І"": ""тесттертің"",
        ""Б"": ""тесттерке"",
        ""Т"": ""тесттерті"",
        ""Ш"": ""тесттертен"",
        ""Ж"": ""тесттерте"",
        ""К"": ""тесттерпен""
    }
}";

        [Test]
        public void Parse_Success()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionResultText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            DeclensionResult declensionResult = morpherClient.Qazaq.Parse("тест");
            Assert.IsNotNull(declensionResult);
            Assert.IsNotNull(declensionResult.Plural);
            Assert.AreEqual("тест", declensionResult.Nominative);
            Assert.AreEqual("тесттің", declensionResult.Genitive);
            Assert.AreEqual("тестке", declensionResult.Dative);
            Assert.AreEqual("тестті", declensionResult.Accusative);
            Assert.AreEqual("тесттен", declensionResult.Ablative);
            Assert.AreEqual("тестте", declensionResult.Locative);
            Assert.AreEqual("тестпен", declensionResult.Instrumental);

            Assert.AreEqual("тесттер", declensionResult.Plural.Nominative);
            Assert.AreEqual("тесттертің", declensionResult.Plural.Genitive);
            Assert.AreEqual("тесттерке", declensionResult.Plural.Dative);
            Assert.AreEqual("тесттерті", declensionResult.Plural.Accusative);
            Assert.AreEqual("тесттертен", declensionResult.Plural.Ablative);
            Assert.AreEqual("тесттерте", declensionResult.Plural.Locative);
            Assert.AreEqual("тесттерпен", declensionResult.Plural.Instrumental);
        }

        public const string ArgumentNotQazaqError = @"
        {
          ""code"": 5,
          ""message"": ""Не найдено казахских слов.""
        }";

        [Test]
        public void ArgumentNotQazaq_Exception()
        {
            MorpherClient client = MockClientHelpers.ExceptionClient(ArgumentNotQazaqError, (HttpStatusCode)496);
            Assert.Throws<ArgumentNotQazaqException>(() => client.Qazaq.Parse("NotQazaq"));
        }
    }
}
