using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Qazaq
{
    [DataContract(Name = "qazaqXml", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : SameNumberForms
    {
        [DataMember(Name = "көпше")]
        public SameNumberForms Plural { get; set; }

        [DataMember(Name = "IsPlural")]
        public bool IsPlural { get; set; }
    }
}
