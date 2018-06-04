using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Qazaq
{
    [DataContract(Name = "qazaqXml", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : DeclensionForms
    {
        [DataMember(Name = "көпше")]
        public DeclensionForms Plural { get; set; }
    }
}
