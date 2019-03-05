namespace Morpher.WebService.V3.Client.UnitTests
{    
    using System.Collections.Specialized;
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
            Assert.AreEqual("тесттен", declensionResult.Instrumental);
            Assert.AreEqual("тестте", declensionResult.Prepositional);
            Assert.AreEqual("тестпен", declensionResult.Vocative);

            Assert.AreEqual("тесттер", declensionResult.Plural.Nominative);
            Assert.AreEqual("тесттертің", declensionResult.Plural.Genitive);
            Assert.AreEqual("тесттерке", declensionResult.Plural.Dative);
            Assert.AreEqual("тесттерті", declensionResult.Plural.Accusative);
            Assert.AreEqual("тесттертен", declensionResult.Plural.Instrumental);
            Assert.AreEqual("тесттерте", declensionResult.Plural.Prepositional);
            Assert.AreEqual("тесттерпен", declensionResult.Plural.Vocative);
        }

        [Test]
        public void ArgumentNotQazaqException()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Throws(new Morpher.WebService.V3.Qazaq.ArgumentNotQazaqException());
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            Assert.Throws<Morpher.WebService.V3.Qazaq.ArgumentNotQazaqException>(() =>
                morpherClient.Qazaq.Parse("NotQazaq"));            
        }
        
    }
}
