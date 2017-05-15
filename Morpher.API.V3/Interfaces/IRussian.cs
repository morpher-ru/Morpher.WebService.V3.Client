namespace Morpher.API.V3.Interfaces
{
    using System.Collections.Generic;

    using Morpher.API.V3.Models;

    public interface IRussian
    {
        RussianDeclensionResult Parse(string lemma, DeclensionFlags? flags = default(DeclensionFlags?));

        RussianNumberSpellingResult Spell(uint number, string unit);

        AdjectiveGenders AdjectiveGenders(string lemma);

        List<string> Adjectivize(string lemma);
    }
}