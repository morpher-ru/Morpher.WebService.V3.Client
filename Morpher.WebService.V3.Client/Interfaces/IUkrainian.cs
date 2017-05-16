namespace Morpher.WebSerivce.V3
{
    using Morpher.WebSerivce.V3;

    public interface IUkrainian
    {
        UkrainianDeclensionResult Parse(string lemma, DeclensionFlags? flags = default(DeclensionFlags?));

        UkrainianNumberSpellingResult Spell(uint number, string unit);
    }
}