namespace Morpher.API.V3
{
    using System.Runtime.Serialization;

    [DataContract(Name = "PropisUkrResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class UkrainianNumberSpellingResult
    {
        [DataMember(Name = "n")]
        public UkrainianDeclensionForms NumberDeclension { get; set; }

        [DataMember(Name = "unit")]
        public UkrainianDeclensionForms UnitDeclension { get; set; }
    }
}