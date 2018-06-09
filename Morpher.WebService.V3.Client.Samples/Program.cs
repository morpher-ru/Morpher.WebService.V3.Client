using System;
using System.Collections.Generic;
using static System.Console;

namespace Morpher.WebService.V3.Client.Samples
{
    public static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Адрес веб-сервиса. 
            // Пользователи "Морфер.Сервера" (http://morpher.ru/webservice/local/), 
            // могут указать адрес своего локального сервера:
            string url = "http://ws3.morpher.ru";

            // Это token от демо-аккаунта, он нужен для демонстрации функций работы с пользовательским словарем.
            // Зарегистрируйтесь и получите свой token: http://morpher.ru/Register.aspx
            Guid token = new Guid("a8dab5fe-7a47-4c17-84ea-46facb7d19fe");

            var morpherClient = new MorpherClient(); // оба параметра необязательные

            Russian.Client russian = morpherClient.Russian;
            Ukrainian.Client ukrainian = morpherClient.Ukrainian;

            try
            {
                RussianDemo(russian);
                UkrainianDemo(ukrainian);

                WriteLine("Остаток запросов на сегодня: " + morpherClient.QueriesLeftForToday());
                WriteLine();

                WriteLine("Провоцируем ошибку:");
                russian.Parse("wuf");
            }
            catch (InvalidArgumentException ex)
            {
                WriteLine("Ошибка: {0}", ex.Message);
            }
            catch (AccessDeniedException ex)
            {
                WriteLine("Ошибка: {0}", ex.Message);
            }
            catch (System.Net.WebException ex)
            {
                WriteLine("Ошибка: {0}", ex.Message);
            }
            // На этом список исключений, которые стоит обрабатывать, исчерпывается.
            // InvalidArgumentException имеет подтипы:
            // - ArgumentEmptyException
            // - Russian.ArgumentNotRussianException
            // - Russian.NumeralsDeclensionNotSupportedException
            // - InvalidFlagsException
            // AccessDeniedException имеет подтипы:
            // - DailyLimitExceededException
            // - IpBlockedException
            // - TokenNotFoundException

            WriteLine();
            WriteLine(premium + " означает, что функция доступна только на платных тарифах. Подробнее http://morpher.ru/ws3#premium");
            ReadLine();
        }

        const decimal number = 2513;
        const string premium = "*****";

        static void RussianDemo(Russian.Client russian)
        {
            WriteLine("Склонение на русском языке:");
            Russian.DeclensionResult declensionResult = russian.Parse("Соединенное королевство");
            WriteLine("Именительный падеж: {0}", declensionResult.Nominative);
            WriteLine(" Родительный падеж: {0}", declensionResult.Genitive);
            WriteLine("   Дательный падеж: {0}", declensionResult.Dative);
            WriteLine(" Винительный падеж: {0}", declensionResult.Accusative);
            WriteLine("Творительный падеж: {0}", declensionResult.Instrumental);
            WriteLine("  Предложный падеж: {0}", declensionResult.Prepositional);
            WriteLine("Предложный с предлогом: {0}", declensionResult.PrepositionalWithO ?? premium);
            WriteLine("               Где? {0}", declensionResult.Where ?? premium);
            WriteLine("              Куда? {0}", declensionResult.To ?? premium);
            WriteLine("            Откуда? {0}", declensionResult.From ?? premium);
            if (declensionResult.Plural != null)
            {
                WriteLine("Именительный падеж: {0}", declensionResult.Plural.Nominative);
                WriteLine(" Родительный падеж: {0}", declensionResult.Plural.Genitive);
                WriteLine("   Дательный падеж: {0}", declensionResult.Plural.Dative);
                WriteLine(" Винительный падеж: {0}", declensionResult.Plural.Accusative);
                WriteLine("Творительный падеж: {0}", declensionResult.Plural.Instrumental);
                WriteLine("  Предложный падеж: {0}", declensionResult.Plural.Prepositional);
                WriteLine("Предложный с предлогом: {0}", declensionResult.Plural.PrepositionalWithO ?? premium);
            }
            WriteLine();

            WriteLine("Определение рода на русском языке:");
            WriteLine("Род: {0}", declensionResult.Gender?.ToString() ?? premium);
            WriteLine();

            WriteLine("Разделение ФИО на части:");
            Russian.DeclensionResult nameDeclensionResult = russian.Parse("Полад Бюльбюль-оглы Мамедов");
            WriteLine("Ф: " + nameDeclensionResult.FullName.Surname);
            WriteLine("И: " + nameDeclensionResult.FullName.Name);
            WriteLine("О: " + nameDeclensionResult.FullName.Pantronymic);
            WriteLine();

            WriteLine("Склонение прилагательных по родам:");
            Russian.AdjectiveGenders adjectiveGenders = russian.AdjectiveGenders("уважаемый");
            WriteLine("Женский род:         " + adjectiveGenders.Feminie);
            WriteLine("Средний род:         " + adjectiveGenders.Neuter);
            WriteLine("Множественное число: " + adjectiveGenders.Plural);
            WriteLine();

            WriteLine("Образование прилагательных:");
            List<string> adjectives = russian.Adjectivize("Мытищи");
            adjectives.ForEach(WriteLine);
            WriteLine();

            // Функции пользовательского словаря для ws3.morpher.ru работают только при наличии токена.
            // Для local сервиса токен не нужен.
            // Русский язык
            // Добавляем новое пользовательское исправление
            var entry = new Russian.CorrectionEntry()
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

            WriteLine("Склонение с исправлением:");
            Russian.DeclensionResult spellWithCorrection = russian.Parse("Кошка");
            WriteLine("            Именительный падеж: {0}", spellWithCorrection.Nominative);
            WriteLine("               Дательный падеж: {0}", spellWithCorrection.Dative);
            WriteLine("Дательный падеж множественного: {0}", spellWithCorrection.Plural.Dative);
            WriteLine();

            WriteLine("Получаем список всех исправлений:");
            IEnumerable<Russian.CorrectionEntry> corrections = russian.UserDict.GetAll();

            foreach (Russian.CorrectionEntry correctionEntry in corrections)
            {
                WriteLine(correctionEntry.Singular.Nominative);
            }

            WriteLine();

            // Удаляем исправление.
            // True если исправление было удалено успешно, false если исправление не найдено.
            bool found = russian.UserDict.Remove("Кошка");
            WriteLine("Исправление найдено: {0}", found ? "Да" : "Нет");

            WriteLine("Склонение после удаления исправления:");
            Russian.DeclensionResult spellWithoutCorrection = russian.Parse("Кошка");
            WriteLine("            Именительный падеж: {0}", spellWithoutCorrection.Nominative);
            WriteLine("               Дательный падеж: {0}", spellWithoutCorrection.Dative);
            WriteLine("Дательный падеж множественного: {0}", spellWithoutCorrection.Plural.Dative);
            WriteLine();

            WriteLine("Сумма прописью:");
            Russian.NumberSpellingResult russianNumberSpellingResult = russian.Spell(number, "рубль");
            WriteLine("В размере {0} ({1}) {2}", number,
                russianNumberSpellingResult.NumberDeclension.Genitive,
                russianNumberSpellingResult.UnitDeclension.Genitive);
        }

        static void UkrainianDemo(Ukrainian.Client ukrainian)
        {
            WriteLine("Склонение ФИО на украинском языке:");
            Ukrainian.DeclensionResult ukrainianDeclensionResult = ukrainian.Parse("Тест");
            WriteLine(" Називний вiдмiнок: " + ukrainianDeclensionResult.Nominative);
            WriteLine("  Родовий вiдмiнок: " + ukrainianDeclensionResult.Genitive);
            WriteLine("Давальний вiдмiнок: " + ukrainianDeclensionResult.Dative);
            WriteLine("Знахідний вiдмiнок: " + ukrainianDeclensionResult.Accusative);
            WriteLine("  Орудний вiдмiнок: " + ukrainianDeclensionResult.Instrumental);
            WriteLine(" Місцевий вiдмiнок: " + ukrainianDeclensionResult.Prepositional);
            WriteLine("  Кличний вiдмiнок: " + ukrainianDeclensionResult.Vocative);
            WriteLine();

            WriteLine("Определение рода на украинском языке:");
            WriteLine("Род: {0}", ukrainianDeclensionResult.Gender?.ToString() ?? premium);
            WriteLine();

            WriteLine("Сумма прописью на укранинском:");
            Ukrainian.NumberSpellingResult ukrainianNumberSpellingResult = ukrainian.Spell(number, "рубль");
            WriteLine("У розмірі {0} ({1}) {2}", number,
                ukrainianNumberSpellingResult.NumberDeclension.Genitive,
                ukrainianNumberSpellingResult.UnitDeclension.Genitive);
            WriteLine();

            // Функции пользовательского словаря для ws3.morpher.ru работают только при наличии токена.
            // Для local сервиса токен не нужен.
            // Украинский язык
            // Добавляем новое пользовательское исправление
            var entry = new Ukrainian.CorrectionEntry()
            {
                Singular = new Ukrainian.CorrectionForms()
                {
                    Nominative = "Сергій",
                    Prepositional = "Сергієві"
                }
            };
            ukrainian.UserDict.AddOrUpdate(entry);

            WriteLine("Склонение с исправлением:");
            Ukrainian.DeclensionResult spellWithCorrection = ukrainian.Parse("Сергій");
            WriteLine("Називний вiдмiнок: {0}", spellWithCorrection.Nominative);
            WriteLine("Мiсцевий вiдмiнок: {0}", spellWithCorrection.Prepositional);
            WriteLine();

            WriteLine("Получаем список всех исправлений:");
            IEnumerable<Ukrainian.CorrectionEntry> corrections = ukrainian.UserDict.GetAll();

            foreach (Ukrainian.CorrectionEntry correctionEntry in corrections)
            {
                WriteLine(correctionEntry.Singular.Nominative);
            }

            WriteLine();

            // Удаляем исправление.
            // True если исправление было удалено успешно, false если исправление не найдено.
            bool found = ukrainian.UserDict.Remove("Сергій");
            WriteLine("Исправление найдено: {0}", found ? "Да" : "Нет");

            WriteLine("Склонение после удаления исправления:");
            Ukrainian.DeclensionResult spellWithoutCorrection = ukrainian.Parse("Сергій");
            WriteLine("Називний вiдмiнок: {0}", spellWithoutCorrection.Nominative);
            WriteLine("Мiсцевий вiдмiнок: {0}", spellWithoutCorrection.Prepositional);
            WriteLine();
        }
    }
}
