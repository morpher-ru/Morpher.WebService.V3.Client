namespace Morpher.WebService.V3.Client.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Morpher.WebService.V3;

    public class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Вы можете передать токен в качестве аргумента конструктора.
            // Guid token = Guid.Parse("17ce56c3-934f-453a-9ef7-cc1feec4e344");
            // Если вы используете Морфер.Сервер, вы можете указать в качестве url адрес вашего локального сервера.
            // string url = "http://api3.morpher.ru"
            // IMorpherClient morpherClient = new MorpherClient(token, url);
            IMorpherClient morpherClient = new MorpherClient();

            // Склонение русских слов.
            Console.WriteLine("Склонение русских слов:");
            RussianDeclensionResult russianDeclensionResult = morpherClient.Russian.Parse("Соединенное королевство");
            Console.WriteLine($"Именительный падеж: {russianDeclensionResult.Nominative},"
                + $" Родительный падеж: {russianDeclensionResult.Genitive}\n");

            // Поле род доступно только на платных тарифах. Подробнее http://morpher.ru/WebServiceV3.aspx
            Console.WriteLine("Род (только на платных тарифах):");
            Console.WriteLine($"Род: {russianDeclensionResult.Gender}");

            // Передача признаков для улучшения качества склонения, а так же разделение ФИО на части
            Console.WriteLine("Разделение на ФИО:");
            RussianDeclensionResult nameDeclensionResult =
                morpherClient.Russian.Parse("Крутько Екатерина Володимировна", DeclensionFlags.Name);
            Console.WriteLine($"Ф: {nameDeclensionResult.FullName.Surname} "
                + $"И: {nameDeclensionResult.FullName.Name} " +
                $"О: {nameDeclensionResult.FullName.Pantronymic}\n");

            // Склонение украинских ФИО
            Console.WriteLine("Склонения украинских ФИО:");
            UkrainianDeclensionResult ukrainianDeclensionResult = morpherClient.Ukrainian.Parse("Крутько Катерина Володимирiвна");
            Console.WriteLine($"Називний вiдмiнок: {ukrainianDeclensionResult.Nominative}," +
                $" Родовий вiдмiнок: {ukrainianDeclensionResult.Genitive}\n");

            // Сумма прописью
            Console.WriteLine("Сумма прописью на двух языках:");
            uint number = 2513;
            RussianNumberSpellingResult russianNumberSpellingResult =
                morpherClient.Russian.Spell(number, "рубль");
            Console.WriteLine($"В размере {number} ({russianNumberSpellingResult.NumberDeclension.Genitive}) " 
                + $"{russianNumberSpellingResult.UnitDeclension.Genitive}");
            UkrainianNumberSpellingResult ukrainianNumberSpellingResult = morpherClient.Ukrainian.Spell(number, "рубль");
            Console.WriteLine($"У розмірі {number} ({ukrainianNumberSpellingResult.NumberDeclension.Genitive}) " 
                + $"{ukrainianNumberSpellingResult.UnitDeclension.Genitive}\n");

            // Склонение прилагательных по родам
            Console.WriteLine("Склонение прилагательных по родам:");
            AdjectiveGenders adjectiveGenders = morpherClient.Russian.AdjectiveGenders("уважаемый");
            Console.WriteLine($"Женский: {adjectiveGenders.Feminie}\n" 
                + $"Средний: {adjectiveGenders.Neuter}\n" + 
                $"Множественное число: {adjectiveGenders.Plural}\n");

            // Образования прилагательных
            Console.WriteLine("Образование прилагательных:");
            List<string> adjectives = morpherClient.Russian.Adjectivize("Мытищи");
            adjectives.ForEach(Console.WriteLine);

            // Остаток запросов
            Console.WriteLine($"\nОстаток запросов: {morpherClient.QueriesLeftForToday()}\n");

            // Обработка ошибок сервиса
            Console.WriteLine("Обработка ошибок сервиса:");
            try
            {
                morpherClient.Russian.Parse("wuf");
            }
            catch (MorpherWebServiceException exc)
            {
                Console.WriteLine($"Code: {exc.Code} Message: {exc.Message}");
            }
        }
    }
}
