using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Ukrainian
{
    [DataContract(Name = "GetXmlUkrResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : DeclensionForms
    {
        [DataMember(Name = "рід")]
        public string Gender { get; set; }
    }
}