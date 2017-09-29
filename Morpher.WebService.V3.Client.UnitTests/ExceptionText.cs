namespace Morpher.WebService.V3.Client.UnitTests
{
    internal class ExceptionText
    {
        public static string UseSpell { get; } = @"
{
  ""code"": 4,
  ""message"": ""Склонение числительных в declension не поддерживается. Используйте метод spell.""
}";

        public static string MissedParameter { get; } = @"
{
  ""code"": 6,
  ""message"": ""Не указан обязательный параметр: unit""
}";

        public static string ServerError { get; } = @"
{
  ""code"": 11,
  ""message"": ""Ошибка сервера.""
}";
    }
}
