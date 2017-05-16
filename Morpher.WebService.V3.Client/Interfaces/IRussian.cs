namespace Morpher.WebService.V3
{
    using System.Collections.Generic;

    public interface IRussian
    {
        RussianDeclensionResult Parse(string lemma, DeclensionFlags? flags = default(DeclensionFlags?));

        RussianNumberSpellingResult Spell(uint number, string unit);

        AdjectiveGenders AdjectiveGenders(string lemma);

        List<string> Adjectivize(string lemma);
    }
}