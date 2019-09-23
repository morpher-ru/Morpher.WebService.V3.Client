using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Russian
{
    [DataContract(Name = "PropisResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DateSpellingResult : DeclensionForms
    {
        public DateSpellingResult()
        {
        }
    }
}