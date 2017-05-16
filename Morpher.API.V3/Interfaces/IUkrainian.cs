namespace Morpher.API.V3
{
    using Morpher.API.V3;

    public interface IUkrainian
    {
        UkrainianDeclensionResult Parse(string lemma, DeclensionFlags? flags = default(DeclensionFlags?));

        UkrainianNumberSpellingResult Spell(uint number, string unit);
    }
}