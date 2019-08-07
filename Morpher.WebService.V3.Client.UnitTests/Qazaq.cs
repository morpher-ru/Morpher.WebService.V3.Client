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

        string DeclensionPersonalResultText { get; } = @"
{
	""A"": ""бала"",
	""І"": ""баланың"",
	""Б"": ""балаға"",
	""Т"": ""баланы"",
	""Ш"": ""баладан"",
	""Ж"": ""балада"",
	""К"": ""баламен"",
	""менің"": {
		""A"": ""балам"",
		""І"": ""баламның"",
		""Б"": ""балама"",
		""Т"": ""баламды"",
		""Ш"": ""баламнан"",
		""Ж"": ""баламда"",
		""К"": ""баламмен""
	},
	""cенің"": {
		""A"": ""балаң"",
		""І"": ""балаңның"",
		""Б"": ""балаңа"",
		""Т"": ""балаңды"",
		""Ш"": ""балаңнан"",
		""Ж"": ""балаңда"",
		""К"": ""балаңмен""
	},
	""сіздің"": {
		""A"": ""балаңыз"",
		""І"": ""балаңыздың"",
		""Б"": ""балаңызға"",
		""Т"": ""балаңызды"",
		""Ш"": ""балаңыздан"",
		""Ж"": ""балаңызда"",
		""К"": ""балаңызбен""
	},
	""оның"": {
		""A"": ""баласы"",
		""І"": ""баласының"",
		""Б"": ""баласына"",
		""Т"": ""баласын"",
		""Ш"": ""баласынан"",
		""Ж"": ""баласында"",
		""К"": ""баласымен""
	},
	""біздің"": {
		""A"": ""баламыз"",
		""І"": ""баламыздың"",
		""Б"": ""баламызға"",
		""Т"": ""баламызды"",
		""Ш"": ""баламыздан"",
		""Ж"": ""баламызда"",
		""К"": ""баламызбен""
	},
	""сендердің"": {
		""A"": ""балаларың"",
		""І"": ""балаларыңның"",
		""Б"": ""балаларыңа"",
		""Т"": ""балаларыңды"",
		""Ш"": ""балаларыңнан"",
		""Ж"": ""балаларыңда"",
		""К"": ""балаларыңмен""
	},
	""сіздердің"": {
		""A"": ""балаларыңыз"",
		""І"": ""балаларыңыздың"",
		""Б"": ""балаларыңызға"",
		""Т"": ""балаларыңызды"",
		""Ш"": ""балаларыңыздан"",
		""Ж"": ""балаларыңызда"",
		""К"": ""балаларыңызбен""
	},
	""олардың"": {
		""A"": ""балалары"",
		""І"": ""балаларының"",
		""Б"": ""балаларына"",
		""Т"": ""балаларын"",
		""Ш"": ""балаларынан"",
		""Ж"": ""балаларында"",
		""К"": ""балаларымен""
	},
	""көпше"": {
		""A"": ""балалар"",
		""І"": ""балалардың"",
		""Б"": ""балаларға"",
		""Т"": ""балаларды"",
		""Ш"": ""балалардан"",
		""Ж"": ""балаларда"",
		""К"": ""балалармен"",
		""менің"": {
			""A"": ""балаларым"",
			""І"": ""балаларымның"",
			""Б"": ""балаларыма"",
			""Т"": ""балаларымды"",
			""Ш"": ""балаларымнан"",
			""Ж"": ""балаларымда"",
			""К"": ""балаларыммен""
		},
		""cенің"": {
			""A"": ""балаларың"",
			""І"": ""балаларыңның"",
			""Б"": ""балаларыңа"",
			""Т"": ""балаларыңды"",
			""Ш"": ""балаларыңнан"",
			""Ж"": ""балаларыңда"",
			""К"": ""балаларыңмен""
		},
		""сіздің"": {
			""A"": ""балаларыңыз"",
			""І"": ""балаларыңыздың"",
			""Б"": ""балаларыңызға"",
			""Т"": ""балаларыңызды"",
			""Ш"": ""балаларыңыздан"",
			""Ж"": ""балаларыңызда"",
			""К"": ""балаларыңызбен""
		},
		""оның"": {
			""A"": ""балалары"",
			""І"": ""балаларының"",
			""Б"": ""балаларына"",
			""Т"": ""балаларын"",
			""Ш"": ""балаларынан"",
			""Ж"": ""балаларында"",
			""К"": ""балаларымен""
		},
		""біздің"": {
			""A"": ""балаларымыз"",
			""І"": ""балаларымыздың"",
			""Б"": ""балаларымызға"",
			""Т"": ""балаларымызды"",
			""Ш"": ""балаларымыздан"",
			""Ж"": ""балаларымызда"",
			""К"": ""балаларымызбен""
		},
		""сендердің"": {
			""A"": ""балаларың"",
			""І"": ""балаларыңның"",
			""Б"": ""балаларыңа"",
			""Т"": ""балаларыңды"",
			""Ш"": ""балаларыңнан"",
			""Ж"": ""балаларыңда"",
			""К"": ""балаларыңмен""
		},
		""сіздердің"": {
			""A"": ""балаларыңыз"",
			""І"": ""балаларыңыздың"",
			""Б"": ""балаларыңызға"",
			""Т"": ""балаларыңызды"",
			""Ш"": ""балаларыңыздан"",
			""Ж"": ""балаларыңызда"",
			""К"": ""балаларыңызбен""
		},
		""олардың"": {
			""A"": ""балалары"",
			""І"": ""балаларының"",
			""Б"": ""балаларына"",
			""Т"": ""балаларын"",
			""Ш"": ""балаларынан"",
			""Ж"": ""балаларында"",
			""К"": ""балаларымен""
		}
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

        [Test]
        public void Parse_Personal_Success()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionPersonalResultText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            DeclensionResult declensionResult = morpherClient.Qazaq.Parse("бала");
            Assert.IsNotNull(declensionResult);
            Assert.AreEqual("бала", declensionResult.Nominative);
            Assert.AreEqual("баланың", declensionResult.Genitive);
            Assert.AreEqual("балаға", declensionResult.Dative);
            Assert.AreEqual("баланы", declensionResult.Accusative);
            Assert.AreEqual("баладан", declensionResult.Ablative);
            Assert.AreEqual("балада", declensionResult.Locative);
            Assert.AreEqual("баламен", declensionResult.Instrumental);

            Assert.IsNotNull(declensionResult.FirstPerson);
            Assert.AreEqual("балам", declensionResult.FirstPerson.Nominative);
            Assert.AreEqual("баламның", declensionResult.FirstPerson.Genitive);
            Assert.AreEqual("балама", declensionResult.FirstPerson.Dative);
            Assert.AreEqual("баламды", declensionResult.FirstPerson.Accusative);
            Assert.AreEqual("баламнан", declensionResult.FirstPerson.Ablative);
            Assert.AreEqual("баламда", declensionResult.FirstPerson.Locative);
            Assert.AreEqual("баламмен", declensionResult.FirstPerson.Instrumental);

            Assert.IsNotNull(declensionResult.SecondPerson);
            Assert.AreEqual("балаң", declensionResult.SecondPerson.Nominative);
            Assert.AreEqual("балаңның", declensionResult.SecondPerson.Genitive);
            Assert.AreEqual("балаңа", declensionResult.SecondPerson.Dative);
            Assert.AreEqual("балаңды", declensionResult.SecondPerson.Accusative);
            Assert.AreEqual("балаңнан", declensionResult.SecondPerson.Ablative);
            Assert.AreEqual("балаңда", declensionResult.SecondPerson.Locative);
            Assert.AreEqual("балаңмен", declensionResult.SecondPerson.Instrumental);

            Assert.IsNotNull(declensionResult.SecondPersonRespectful);
            Assert.AreEqual("балаңыз", declensionResult.SecondPersonRespectful.Nominative);
            Assert.AreEqual("балаңыздың", declensionResult.SecondPersonRespectful.Genitive);
            Assert.AreEqual("балаңызға", declensionResult.SecondPersonRespectful.Dative);
            Assert.AreEqual("балаңызды", declensionResult.SecondPersonRespectful.Accusative);
            Assert.AreEqual("балаңыздан", declensionResult.SecondPersonRespectful.Ablative);
            Assert.AreEqual("балаңызда", declensionResult.SecondPersonRespectful.Locative);
            Assert.AreEqual("балаңызбен", declensionResult.SecondPersonRespectful.Instrumental);

            Assert.IsNotNull(declensionResult.ThirdPerson);
            Assert.AreEqual("баласы", declensionResult.ThirdPerson.Nominative);
            Assert.AreEqual("баласының", declensionResult.ThirdPerson.Genitive);
            Assert.AreEqual("баласына", declensionResult.ThirdPerson.Dative);
            Assert.AreEqual("баласын", declensionResult.ThirdPerson.Accusative);
            Assert.AreEqual("баласынан", declensionResult.ThirdPerson.Ablative);
            Assert.AreEqual("баласында", declensionResult.ThirdPerson.Locative);
            Assert.AreEqual("баласымен", declensionResult.ThirdPerson.Instrumental);

            Assert.IsNotNull(declensionResult.FirstPersonPlural);
            Assert.AreEqual("баламыз", declensionResult.FirstPersonPlural.Nominative);
            Assert.AreEqual("баламыздың", declensionResult.FirstPersonPlural.Genitive);
            Assert.AreEqual("баламызға", declensionResult.FirstPersonPlural.Dative);
            Assert.AreEqual("баламызды", declensionResult.FirstPersonPlural.Accusative);
            Assert.AreEqual("баламыздан", declensionResult.FirstPersonPlural.Ablative);
            Assert.AreEqual("баламызда", declensionResult.FirstPersonPlural.Locative);
            Assert.AreEqual("баламызбен", declensionResult.FirstPersonPlural.Instrumental);

            Assert.IsNotNull(declensionResult.SecondPerson);
            Assert.AreEqual("балаң", declensionResult.SecondPerson.Nominative);
            Assert.AreEqual("балаңның", declensionResult.SecondPerson.Genitive);
            Assert.AreEqual("балаңа", declensionResult.SecondPerson.Dative);
            Assert.AreEqual("балаңды", declensionResult.SecondPerson.Accusative);
            Assert.AreEqual("балаңнан", declensionResult.SecondPerson.Ablative);
            Assert.AreEqual("балаңда", declensionResult.SecondPerson.Locative);
            Assert.AreEqual("балаңмен", declensionResult.SecondPerson.Instrumental);

            Assert.IsNotNull(declensionResult.SecondPersonRespectful);
            Assert.AreEqual("балаңыз", declensionResult.SecondPersonRespectful.Nominative);
            Assert.AreEqual("балаңыздың", declensionResult.SecondPersonRespectful.Genitive);
            Assert.AreEqual("балаңызға", declensionResult.SecondPersonRespectful.Dative);
            Assert.AreEqual("балаңызды", declensionResult.SecondPersonRespectful.Accusative);
            Assert.AreEqual("балаңыздан", declensionResult.SecondPersonRespectful.Ablative);
            Assert.AreEqual("балаңызда", declensionResult.SecondPersonRespectful.Locative);
            Assert.AreEqual("балаңызбен", declensionResult.SecondPersonRespectful.Instrumental);

            Assert.IsNotNull(declensionResult.ThirdPersonPlural);
            Assert.AreEqual("балалары", declensionResult.ThirdPersonPlural.Nominative);
            Assert.AreEqual("балаларының", declensionResult.ThirdPersonPlural.Genitive);
            Assert.AreEqual("балаларына", declensionResult.ThirdPersonPlural.Dative);
            Assert.AreEqual("балаларын", declensionResult.ThirdPersonPlural.Accusative);
            Assert.AreEqual("балаларынан", declensionResult.ThirdPersonPlural.Ablative);
            Assert.AreEqual("балаларында", declensionResult.ThirdPersonPlural.Locative);
            Assert.AreEqual("балаларымен", declensionResult.ThirdPersonPlural.Instrumental);

            Assert.IsNotNull(declensionResult.Plural);
            Assert.AreEqual("балалар", declensionResult.Plural.Nominative);
            Assert.AreEqual("балалардың", declensionResult.Plural.Genitive);
            Assert.AreEqual("балаларға", declensionResult.Plural.Dative);
            Assert.AreEqual("балаларды", declensionResult.Plural.Accusative);
            Assert.AreEqual("балалардан", declensionResult.Plural.Ablative);
            Assert.AreEqual("балаларда", declensionResult.Plural.Locative);
            Assert.AreEqual("балалармен", declensionResult.Plural.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.FirstPerson);
            Assert.AreEqual("балаларым", declensionResult.Plural.FirstPerson.Nominative);
            Assert.AreEqual("балаларымның", declensionResult.Plural.FirstPerson.Genitive);
            Assert.AreEqual("балаларыма", declensionResult.Plural.FirstPerson.Dative);
            Assert.AreEqual("балаларымды", declensionResult.Plural.FirstPerson.Accusative);
            Assert.AreEqual("балаларымнан", declensionResult.Plural.FirstPerson.Ablative);
            Assert.AreEqual("балаларымда", declensionResult.Plural.FirstPerson.Locative);
            Assert.AreEqual("балаларыммен", declensionResult.Plural.FirstPerson.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.SecondPerson);
            Assert.AreEqual("балаларың", declensionResult.Plural.SecondPerson.Nominative);
            Assert.AreEqual("балаларыңның", declensionResult.Plural.SecondPerson.Genitive);
            Assert.AreEqual("балаларыңа", declensionResult.Plural.SecondPerson.Dative);
            Assert.AreEqual("балаларыңды", declensionResult.Plural.SecondPerson.Accusative);
            Assert.AreEqual("балаларыңнан", declensionResult.Plural.SecondPerson.Ablative);
            Assert.AreEqual("балаларыңда", declensionResult.Plural.SecondPerson.Locative);
            Assert.AreEqual("балаларыңмен", declensionResult.Plural.SecondPerson.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.SecondPersonRespectful);
            Assert.AreEqual("балаларыңыз", declensionResult.Plural.SecondPersonRespectful.Nominative);
            Assert.AreEqual("балаларыңыздың", declensionResult.Plural.SecondPersonRespectful.Genitive);
            Assert.AreEqual("балаларыңызға", declensionResult.Plural.SecondPersonRespectful.Dative);
            Assert.AreEqual("балаларыңызды", declensionResult.Plural.SecondPersonRespectful.Accusative);
            Assert.AreEqual("балаларыңыздан", declensionResult.Plural.SecondPersonRespectful.Ablative);
            Assert.AreEqual("балаларыңызда", declensionResult.Plural.SecondPersonRespectful.Locative);
            Assert.AreEqual("балаларыңызбен", declensionResult.Plural.SecondPersonRespectful.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.ThirdPerson);
            Assert.AreEqual("балалары", declensionResult.Plural.ThirdPerson.Nominative);
            Assert.AreEqual("балаларының", declensionResult.Plural.ThirdPerson.Genitive);
            Assert.AreEqual("балаларына", declensionResult.Plural.ThirdPerson.Dative);
            Assert.AreEqual("балаларын", declensionResult.Plural.ThirdPerson.Accusative);
            Assert.AreEqual("балаларынан", declensionResult.Plural.ThirdPerson.Ablative);
            Assert.AreEqual("балаларында", declensionResult.Plural.ThirdPerson.Locative);
            Assert.AreEqual("балаларымен", declensionResult.Plural.ThirdPerson.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.FirstPersonPlural);
            Assert.AreEqual("балаларымыз", declensionResult.Plural.FirstPersonPlural.Nominative);
            Assert.AreEqual("балаларымыздың", declensionResult.Plural.FirstPersonPlural.Genitive);
            Assert.AreEqual("балаларымызға", declensionResult.Plural.FirstPersonPlural.Dative);
            Assert.AreEqual("балаларымызды", declensionResult.Plural.FirstPersonPlural.Accusative);
            Assert.AreEqual("балаларымыздан", declensionResult.Plural.FirstPersonPlural.Ablative);
            Assert.AreEqual("балаларымызда", declensionResult.Plural.FirstPersonPlural.Locative);
            Assert.AreEqual("балаларымызбен", declensionResult.Plural.FirstPersonPlural.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.SecondPerson);
            Assert.AreEqual("балаларың", declensionResult.Plural.SecondPerson.Nominative);
            Assert.AreEqual("балаларыңның", declensionResult.Plural.SecondPerson.Genitive);
            Assert.AreEqual("балаларыңа", declensionResult.Plural.SecondPerson.Dative);
            Assert.AreEqual("балаларыңды", declensionResult.Plural.SecondPerson.Accusative);
            Assert.AreEqual("балаларыңнан", declensionResult.Plural.SecondPerson.Ablative);
            Assert.AreEqual("балаларыңда", declensionResult.Plural.SecondPerson.Locative);
            Assert.AreEqual("балаларыңмен", declensionResult.Plural.SecondPerson.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.SecondPersonRespectful);
            Assert.AreEqual("балаларыңыз", declensionResult.Plural.SecondPersonRespectful.Nominative);
            Assert.AreEqual("балаларыңыздың", declensionResult.Plural.SecondPersonRespectful.Genitive);
            Assert.AreEqual("балаларыңызға", declensionResult.Plural.SecondPersonRespectful.Dative);
            Assert.AreEqual("балаларыңызды", declensionResult.Plural.SecondPersonRespectful.Accusative);
            Assert.AreEqual("балаларыңыздан", declensionResult.Plural.SecondPersonRespectful.Ablative);
            Assert.AreEqual("балаларыңызда", declensionResult.Plural.SecondPersonRespectful.Locative);
            Assert.AreEqual("балаларыңызбен", declensionResult.Plural.SecondPersonRespectful.Instrumental);

            Assert.IsNotNull(declensionResult.Plural.ThirdPersonPlural);
            Assert.AreEqual("балалары", declensionResult.Plural.ThirdPersonPlural.Nominative);
            Assert.AreEqual("балаларының", declensionResult.Plural.ThirdPersonPlural.Genitive);
            Assert.AreEqual("балаларына", declensionResult.Plural.ThirdPersonPlural.Dative);
            Assert.AreEqual("балаларын", declensionResult.Plural.ThirdPersonPlural.Accusative);
            Assert.AreEqual("балаларынан", declensionResult.Plural.ThirdPersonPlural.Ablative);
            Assert.AreEqual("балаларында", declensionResult.Plural.ThirdPersonPlural.Locative);
            Assert.AreEqual("балаларымен", declensionResult.Plural.ThirdPersonPlural.Instrumental);
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
