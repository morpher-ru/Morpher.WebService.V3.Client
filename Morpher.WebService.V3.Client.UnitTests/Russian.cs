namespace Morpher.WebService.V3.Client.UnitTests
{
    using System.Collections.Specialized;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using V3.Russian;

    [TestClass]
    public class Russian
    {
        public string DeclensionResultText { get; } = @"
{
    ""Р"": ""теста"",
    ""Д"": ""тесту"",
    ""В"": ""тест"",
    ""Т"": ""тестом"",
    ""П"": ""тесте"",
    ""П_о"": ""о тесте"",
    ""род"": ""Мужской"",
    ""множественное"": {
        ""И"": ""тесты"",
        ""Р"": ""тестов"",
        ""Д"": ""тестам"",
        ""В"": ""тесты"",
        ""Т"": ""тестами"",
        ""П"": ""тестах"",
        ""П_о"": ""о тестах""
    },
    ""где"": ""в тесте"",
    ""куда"": ""в тест"",
    ""откуда"": ""из теста""
}";

        public string SpellResultText { get; } = @"
{
    ""n"": {
        ""И"": ""десять"",
        ""Р"": ""десяти"",
        ""Д"": ""десяти"",
        ""В"": ""десять"",
        ""Т"": ""десятью"",
        ""П"": ""десяти"",
        ""П_о"": ""десяти""
    },
    ""unit"": {
        ""И"": ""рублей"",
        ""Р"": ""рублей"",
        ""Д"": ""рублям"",
        ""В"": ""рублей"",
        ""Т"": ""рублями"",
        ""П"": ""рублях"",
        ""П_о"": ""рублях""
    }
}";

        [TestMethod]
        public void Parse_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionResultText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            DeclensionResult declensionResult = morpherClient.Russian.Parse("тест");
            Assert.IsNotNull(declensionResult);
            Assert.IsNotNull(declensionResult.Plural);
            Assert.AreEqual("тест", declensionResult.Nominative);
            Assert.AreEqual("теста", declensionResult.Genitive);
            Assert.AreEqual("тесту", declensionResult.Dative);
            Assert.AreEqual("тест", declensionResult.Accusative);
            Assert.AreEqual("тестом", declensionResult.Instrumental);
            Assert.AreEqual("тесте", declensionResult.Prepositional);
            Assert.AreEqual("о тесте", declensionResult.PrepositionalWithO);

            Assert.AreEqual("в тесте", declensionResult.Where);
            Assert.AreEqual("в тест", declensionResult.To);
            Assert.AreEqual("из теста", declensionResult.From);

            Assert.AreEqual("тесты", declensionResult.Plural.Nominative);
            Assert.AreEqual("тестов", declensionResult.Plural.Genitive);
            Assert.AreEqual("тестам", declensionResult.Plural.Dative);
            Assert.AreEqual("тесты", declensionResult.Plural.Accusative);
            Assert.AreEqual("тестами", declensionResult.Plural.Instrumental);
            Assert.AreEqual("тестах", declensionResult.Plural.Prepositional);
            Assert.AreEqual("о тестах", declensionResult.Plural.PrepositionalWithO);

            Assert.AreEqual(Gender.Masculine, declensionResult.Gender);
        }
    }
}