namespace Morpher.API.V3
{
    using Morpher.API.V3.Models;

    public interface IMorpherClient
    {
        RussianDeclensionResult ParseRussian(string lemma, DeclensionFlags? flags = null);

        UkrainianDeclensionResult ParseUkrainian(string lemma, DeclensionFlags? flags = null);

        RussianNumberSpellingResult SpellRussian(uint number, string unit);

        UkrainianNumberSpellingResult SpellUkrainian(uint number, string unit);
    }
}
