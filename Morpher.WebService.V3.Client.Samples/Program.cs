namespace Morpher.WebService.V3.Client.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Russian;

    public class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Вы можете передать токен в качестве аргумента конструктора.
            // Guid token = Guid.Parse("17ce56c3-934f-453a-9ef7-cc1feec4e344");
            // Если вы используете "Морфер.Сервер" (http://morpher.ru/webservice/local/), 
            // то вы можете указать в качестве url адрес вашего локального сервера:
            // string url = "http://ws3.morpher.ru"
            // IMorpherClient morpherClient = new MorpherClient(token, url);
            var morpherClient = new MorpherClient();
            const string premium = "*****";

            Console.WriteLine("Склонение на русском языке:");
            Russian.DeclensionResult russianDeclensionResult = morpherClient.Russian.Parse("Соединенное королевство");
            Console.WriteLine("Именительный падеж: {0}", russianDeclensionResult.Nominative);
            Console.WriteLine(" Родительный падеж: {0}", russianDeclensionResult.Genitive);
            Console.WriteLine("   Дательный падеж: {0}", russianDeclensionResult.Dative);
            Console.WriteLine(" Винительный падеж: {0}", russianDeclensionResult.Accusative);
            Console.WriteLine("Творительный падеж: {0}", russianDeclensionResult.Instrumental);
            Console.WriteLine("  Предложный падеж: {0}", russianDeclensionResult.Prepositional);
            Console.WriteLine("     Местный падеж: {0}", russianDeclensionResult.PrepositionalWithO ?? premium);
            Console.WriteLine("               Где? {0}", russianDeclensionResult.Where    ?? premium);
            Console.WriteLine("              Куда? {0}", russianDeclensionResult.To       ?? premium);
            Console.WriteLine("            Откуда? {0}", russianDeclensionResult.From     ?? premium);
            if (russianDeclensionResult.Plural != null)
            {
                Console.WriteLine("Именительный падеж: {0}", russianDeclensionResult.Plural.Nominative);
                Console.WriteLine(" Родительный падеж: {0}", russianDeclensionResult.Plural.Genitive);
                Console.WriteLine("   Дательный падеж: {0}", russianDeclensionResult.Plural.Dative);
                Console.WriteLine(" Винительный падеж: {0}", russianDeclensionResult.Plural.Accusative);
                Console.WriteLine("Творительный падеж: {0}", russianDeclensionResult.Plural.Instrumental);
                Console.WriteLine("  Предложный падеж: {0}", russianDeclensionResult.Plural.Prepositional);
                Console.WriteLine("     Местный падеж: {0}", russianDeclensionResult.Plural.PrepositionalWithO ?? premium);
            }
            Console.WriteLine();

            Console.WriteLine("Определение рода на русском языке:");
            Console.WriteLine("Род: {0}", russianDeclensionResult.Gender ?? premium);
            Console.WriteLine();

            Console.WriteLine("Разделение ФИО на части:");
            Russian.DeclensionResult nameDeclensionResult = morpherClient.Russian.Parse("Полад Бюльбюль-оглы Мамедов");
            Console.WriteLine("Ф: " + nameDeclensionResult.FullName.Surname);
            Console.WriteLine("И: " + nameDeclensionResult.FullName.Name);
            Console.WriteLine("О: " + nameDeclensionResult.FullName.Pantronymic);
            Console.WriteLine();

            Console.WriteLine("Склонение ФИО на украинском языке:");
            Ukrainian.DeclensionResult ukrainianDeclensionResult = morpherClient.Ukrainian.Parse("Крутько Катерина Володимирiвна");
            Console.WriteLine(" Називний вiдмiнок: " + ukrainianDeclensionResult.Nominative);
            Console.WriteLine("  Родовий вiдмiнок: " + ukrainianDeclensionResult.Genitive);
            Console.WriteLine("Давальний вiдмiнок: " + ukrainianDeclensionResult.Dative);
            Console.WriteLine("Знахідний вiдмiнок: " + ukrainianDeclensionResult.Accusative);
            Console.WriteLine("  Орудний вiдмiнок: " + ukrainianDeclensionResult.Instrumental);
            Console.WriteLine(" Місцевий вiдмiнок: " + ukrainianDeclensionResult.Prepositional);
            Console.WriteLine("  Кличний вiдмiнок: " + ukrainianDeclensionResult.Vocative);
            Console.WriteLine();

            Console.WriteLine("Определение рода на украинском языке:");
            Console.WriteLine("Род: {0}", ukrainianDeclensionResult.Gender ?? premium);
            Console.WriteLine();

            Console.WriteLine("Сумма прописью на двух языках:");
            uint number = 2513;
            Russian.NumberSpellingResult russianNumberSpellingResult = morpherClient.Russian.Spell(number, "рубль");
            Console.WriteLine("В размере {0} ({1}) {2}", number,
                    russianNumberSpellingResult.NumberDeclension.Genitive,
                    russianNumberSpellingResult.UnitDeclension.Genitive);
            Ukrainian.NumberSpellingResult ukrainianNumberSpellingResult = morpherClient.Ukrainian.Spell(number, "рубль");
            Console.WriteLine("У розмірі {0} ({1}) {2}", number,
                    ukrainianNumberSpellingResult.NumberDeclension.Genitive,
                    ukrainianNumberSpellingResult.UnitDeclension.Genitive);
            Console.WriteLine();

            Console.WriteLine("Склонение прилагательных по родам:");
            Russian.AdjectiveGenders adjectiveGenders = morpherClient.Russian.AdjectiveGenders("уважаемый");
            Console.WriteLine("Женский род:         " + adjectiveGenders.Feminie);
            Console.WriteLine("Средний род:         " + adjectiveGenders.Neuter);
            Console.WriteLine("Множественное число: " + adjectiveGenders.Plural);
            Console.WriteLine();

            Console.WriteLine("Образование прилагательных:");
            List<string> adjectives = morpherClient.Russian.Adjectivize("Мытищи");
            adjectives.ForEach(Console.WriteLine);
            Console.WriteLine();


            //Работа с пользовательским словарем для ws3.morpher.ru работает только при наличии токена
            // Для local сервиса токен не нужен.
            // Добавляем новое пользоватеслькое исправление
            CorrectionEntry entry = new CorrectionEntry()
            {
                Singular = new CorrectionForms()
                {
                    Nominative = "Кошка",
                    Dative = "Пантере"
                },
                Plural = new CorrectionForms()
                {
                    Dative = "Пантерам"
                }
            };
            morpherClient.Russian.UserDict.AddOrUpdate(entry);

            // Получаем список всех исправлений
            IEnumerable<CorrectionEntry> corrections = morpherClient.Russian.UserDict.GetAll();

            // Удаляем исправление
            // True если исправление было удалено успешно, false если исправление не найдено в бд.
            bool success = morpherClient.Russian.UserDict.Remove("Кошка");

            Console.WriteLine("Обработка ошибок сервиса:");
            try
            {
                morpherClient.Russian.Parse("wuf");
            }
            catch (MorpherWebServiceException exc)
            {
                Console.WriteLine("Code: {0} Message: {1}", exc.Code, exc.Message);
            }
            Console.WriteLine();

            Console.WriteLine("Остаток запросов на сегодня: " + morpherClient.QueriesLeftForToday());
            Console.WriteLine();

            Console.WriteLine(premium + " означает, что функция доступна на платных тарифах. Подробнее http://morpher.ru/WebServiceV3.aspx#premium");
        }
    }
}
