using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Qazaq
{
    [DataContract(Name = "QazaqDeclensionResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : DeclensionForms
    {
        [DataMember(Name = "көпше")]
        public DeclensionResult Plural { get; set; }

        [DataMember(Name = "менің")]
        public DeclensionForms FirstPerson { get; set; }

        [DataMember(Name = "cенің")]
        public DeclensionForms SecondPerson { get; set; }

        [DataMember(Name = "сіздің")]
        public DeclensionForms SecondPersonRespectful { get; set; }

        [DataMember(Name = "оның")]
        public DeclensionForms ThirdPerson { get; set; }

        [DataMember(Name = "біздің")]
        public DeclensionForms FirstPersonPlural { get; set; }

        [DataMember(Name = "cендердің")]
        public DeclensionForms SecondPersonPlural { get; set; }

        [DataMember(Name = "сіздердің")]
        public DeclensionForms SecondPersonRespectfulPlural { get; set; }

        [DataMember(Name = "олардың")]
        public DeclensionForms ThirdPersonPlural { get; set; }
    }
}
