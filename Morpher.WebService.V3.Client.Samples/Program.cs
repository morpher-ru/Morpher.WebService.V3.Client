namespace Morpher.WebService.V3.Client.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Exceptions;

    public class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Вы можете передать токен в качестве аргумента конструктора.
            // Guid token = Guid.Parse("17ce56c3-934f-453a-9ef7-cc1feec4e344");
            // Если вы используете "Морфер.Сервер" (http://morpher.ru/webservice/local/), 
            // то вы можете указать в качестве url адрес вашего локального сервера:
            // string url = "http://ws3.morpher.ru"
            // MorpherClient morpherClient = new MorpherClient(token, url);
            // !!! Не используйте этот токен в production !!!
            Guid token = Guid.Parse("a8dab5fe-7a47-4c17-84ea-46facb7d19fe");
            var morpherClient = new MorpherClient(token);

            Russian.Client russian = morpherClient.Russian;
            Ukrainian.Client ukrainian = morpherClient.Ukrainian;

            try
            {
                RussianDemo(russian);
                UkrainianDemo(ukrainian);

                Console.WriteLine("Остаток запросов на сегодня: " + morpherClient.QueriesLeftForToday());
                Console.WriteLine();

                Console.WriteLine("Провоцируем ошибку:");
                russian.Parse("wuf");
            }
            catch (ArgumentNotRussianException exc)
            {
                Console.WriteLine("Ошибка: {0}", exc.Message);
            }
            catch (AccessDeniedException exc)
            {
                Console.WriteLine("Ошибка: {0}", exc.Message);
            }

            Console.WriteLine();
            Console.WriteLine(premium + " означает, что функция доступна на платных тарифах. Подробнее http://morpher.ru/ws3#premium");
        }

        static readonly decimal number = 2513;
        const string premium = "*****";

        static void RussianDemo(Russian.Client russian)
        {
            Console.WriteLine("Склонение на русском языке:");
            Russian.DeclensionResult declensionResult = russian.Parse("Соединенное королевство");
            Console.WriteLine("Именительный падеж: {0}", declensionResult.Nominative);
            Console.WriteLine(" Родительный падеж: {0}", declensionResult.Genitive);
            Console.WriteLine("   Дательный падеж: {0}", declensionResult.Dative);
            Console.WriteLine(" Винительный падеж: {0}", declensionResult.Accusative);
            Console.WriteLine("Творительный падеж: {0}", declensionResult.Instrumental);
            Console.WriteLine("  Предложный падеж: {0}", declensionResult.Prepositional);
            Console.WriteLine("     Местный падеж: {0}", declensionResult.PrepositionalWithO ?? premium);
            Console.WriteLine("               Где? {0}", declensionResult.Where ?? premium);
            Console.WriteLine("              Куда? {0}", declensionResult.To ?? premium);
            Console.WriteLine("            Откуда? {0}", declensionResult.From ?? premium);
            if (declensionResult.Plural != null)
            {
                Console.WriteLine("Именительный падеж: {0}", declensionResult.Plural.Nominative);
                Console.WriteLine(" Родительный падеж: {0}", declensionResult.Plural.Genitive);
                Console.WriteLine("   Дательный падеж: {0}", declensionResult.Plural.Dative);
                Console.WriteLine(" Винительный падеж: {0}", declensionResult.Plural.Accusative);
                Console.WriteLine("Творительный падеж: {0}", declensionResult.Plural.Instrumental);
                Console.WriteLine("  Предложный падеж: {0}", declensionResult.Plural.Prepositional);
                Console.WriteLine("     Местный падеж: {0}", declensionResult.Plural.PrepositionalWithO ?? premium);
            }
            Console.WriteLine();

            Console.WriteLine("Определение рода на русском языке:");
            Console.WriteLine("Род: {0}", declensionResult.Gender?.ToString() ?? premium);
            Console.WriteLine();

            Console.WriteLine("Разделение ФИО на части:");
            Russian.DeclensionResult nameDeclensionResult = russian.Parse("Полад Бюльбюль-оглы Мамедов");
            Console.WriteLine("Ф: " + nameDeclensionResult.FullName.Surname);
            Console.WriteLine("И: " + nameDeclensionResult.FullName.Name);
            Console.WriteLine("О: " + nameDeclensionResult.FullName.Pantronymic);
            Console.WriteLine();

            Console.WriteLine("Склонение прилагательных по родам:");
            Russian.AdjectiveGenders adjectiveGenders = russian.AdjectiveGenders("уважаемый");
            Console.WriteLine("Женский род:         " + adjectiveGenders.Feminie);
            Console.WriteLine("Средний род:         " + adjectiveGenders.Neuter);
            Console.WriteLine("Множественное число: " + adjectiveGenders.Plural);
            Console.WriteLine();

            Console.WriteLine("Образование прилагательных:");
            List<string> adjectives = russian.Adjectivize("Мытищи");
            adjectives.ForEach(Console.WriteLine);
            Console.WriteLine();

            // Функции пользовательского словаря для ws3.morpher.ru работают только при наличии токена.
            // Для local сервиса токен не нужен.
            // Русский язык
            // Добавляем новое пользовательское исправление
            Russian.CorrectionEntry entry = new Russian.CorrectionEntry()
            {
                Singular = new Russian.CorrectionForms()
                {
                    Nominative = "Кошка",
                    Dative = "Пантере"
                },
                Plural = new Russian.CorrectionForms()
                {
                    Dative = "Пантерам"
                }
            };
            russian.UserDict.AddOrUpdate(entry);

            Console.WriteLine("Склонение с исправлением:");
            Russian.DeclensionResult spellWithCorrection = russian.Parse("Кошка");
            Console.WriteLine("            Именительный падеж: {0}", spellWithCorrection.Nominative);
            Console.WriteLine("               Дательный падеж: {0}", spellWithCorrection.Dative);
            Console.WriteLine("Дательный падеж множественного: {0}", spellWithCorrection.Plural.Dative);
            Console.WriteLine();

            Console.WriteLine("Получаем список всех исправлений:");
            IEnumerable<Russian.CorrectionEntry> corrections = russian.UserDict.GetAll();

            foreach (var correctionEntry in corrections)
            {
                Console.WriteLine(correctionEntry.Singular.Nominative);
            }

            Console.WriteLine();

            // Удаляем исправление.
            // True если исправление было удалено успешно, false если исправление не найдено.
            bool found = russian.UserDict.Remove("Кошка");
            Console.WriteLine("Исправление найдено: {0}", found ? "Да" : "Нет");

            Console.WriteLine("Склонение после удаления исправления:");
            Russian.DeclensionResult spellWithoutCorrection = russian.Parse("Кошка");
            Console.WriteLine("            Именительный падеж: {0}", spellWithoutCorrection.Nominative);
            Console.WriteLine("               Дательный падеж: {0}", spellWithoutCorrection.Dative);
            Console.WriteLine("Дательный падеж множественного: {0}", spellWithoutCorrection.Plural.Dative);
            Console.WriteLine();

            Console.WriteLine("Сумма прописью:");
            Russian.NumberSpellingResult russianNumberSpellingResult = russian.Spell(number, "рубль");
            Console.WriteLine("В размере {0} ({1}) {2}", number,
                russianNumberSpellingResult.NumberDeclension.Genitive,
                russianNumberSpellingResult.UnitDeclension.Genitive);
        }

        static void UkrainianDemo(Ukrainian.Client ukrainian)
        {
            Console.WriteLine("Склонение ФИО на украинском языке:");
            Ukrainian.DeclensionResult ukrainianDeclensionResult = ukrainian.Parse("Тест");
            Console.WriteLine(" Називний вiдмiнок: " + ukrainianDeclensionResult.Nominative);
            Console.WriteLine("  Родовий вiдмiнок: " + ukrainianDeclensionResult.Genitive);
            Console.WriteLine("Давальний вiдмiнок: " + ukrainianDeclensionResult.Dative);
            Console.WriteLine("Знахідний вiдмiнок: " + ukrainianDeclensionResult.Accusative);
            Console.WriteLine("  Орудний вiдмiнок: " + ukrainianDeclensionResult.Instrumental);
            Console.WriteLine(" Місцевий вiдмiнок: " + ukrainianDeclensionResult.Prepositional);
            Console.WriteLine("  Кличний вiдмiнок: " + ukrainianDeclensionResult.Vocative);
            Console.WriteLine();

            Console.WriteLine("Определение рода на украинском языке:");
            Console.WriteLine("Род: {0}", ukrainianDeclensionResult.Gender?.ToString() ?? premium);
            Console.WriteLine();

            Console.WriteLine("Сумма прописью на укранинском:");
            Ukrainian.NumberSpellingResult ukrainianNumberSpellingResult = ukrainian.Spell(number, "рубль");
            Console.WriteLine("У розмірі {0} ({1}) {2}", number,
                ukrainianNumberSpellingResult.NumberDeclension.Genitive,
                ukrainianNumberSpellingResult.UnitDeclension.Genitive);
            Console.WriteLine();

            // Функции пользовательского словаря для ws3.morpher.ru работают только при наличии токена.
            // Для local сервиса токен не нужен.
            // Украинский язык
            // Добавляем новое пользовательское исправление
            Ukrainian.CorrectionEntry entry = new Ukrainian.CorrectionEntry()
            {
                Singular = new Ukrainian.CorrectionForms()
                {
                    Nominative = "Сергій",
                    Prepositional = "Сергієві"
                }
            };
            ukrainian.UserDict.AddOrUpdate(entry);

            Console.WriteLine("Склонение с исправлением:");
            Ukrainian.DeclensionResult spellWithCorrection = ukrainian.Parse("Сергій");
            Console.WriteLine("Називний вiдмiнок: {0}", spellWithCorrection.Nominative);
            Console.WriteLine("Мiсцевий вiдмiнок: {0}", spellWithCorrection.Prepositional);
            Console.WriteLine();

            Console.WriteLine("Получаем список всех исправлений:");
            IEnumerable<Ukrainian.CorrectionEntry> corrections = ukrainian.UserDict.GetAll();

            foreach (var correctionEntry in corrections)
            {
                Console.WriteLine(correctionEntry.Singular.Nominative);
            }

            Console.WriteLine();

            // Удаляем исправление.
            // True если исправление было удалено успешно, false если исправление не найдено.
            bool found = ukrainian.UserDict.Remove("Сергій");
            Console.WriteLine("Исправление найдено: {0}", found ? "Да" : "Нет");

            Console.WriteLine("Склонение после удаления исправления:");
            Ukrainian.DeclensionResult spellWithoutCorrection = ukrainian.Parse("Сергій");
            Console.WriteLine("Називний вiдмiнок: {0}", spellWithoutCorrection.Nominative);
            Console.WriteLine("Мiсцевий вiдмiнок: {0}", spellWithoutCorrection.Prepositional);
            Console.WriteLine();
        }
    }
}
