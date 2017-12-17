namespace Morpher.WebService.V3.Client.UnitTests
{
    internal static class ExceptionText
    {
        public const string UseSpell = @"
{
  ""code"": 4,
  ""message"": ""Склонение числительных в declension не поддерживается. Используйте метод spell.""
}";

        public const string MissedParameter = @"
{
  ""code"": 6,
  ""message"": ""Не указан обязательный параметр:""
}";

        public const string ServerError = @"
{
  ""code"": 11,
  ""message"": ""Ошибка сервера.""
}";
    }
}
