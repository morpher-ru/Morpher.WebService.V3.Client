namespace Morpher.API.V3
{
    using System.Runtime.Serialization;

    [DataContract(Name = "PropisResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class RussianNumberSpellingResult
    {
        [DataMember(Name = "n")]
        public RussianDeclensionForms NumberDeclension { get; set; }

        [DataMember(Name = "unit")]
        public RussianDeclensionForms UnitDeclension { get; set; }
    }
}