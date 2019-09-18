using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Qazaq
{
    /// <summary>
    /// Падежно-личные формы одного числа.
    /// </summary>
    public class SameNumberForms : DeclensionForms
    {
        [DataMember(Name = "менің")]
        public DeclensionForms FirstPerson { get; set; }

        [DataMember(Name = "сенің")]
        public DeclensionForms SecondPerson { get; set; }

        [DataMember(Name = "сіздің")]
        public DeclensionForms SecondPersonRespectful { get; set; }

        [DataMember(Name = "оның")]
        public DeclensionForms ThirdPerson { get; set; }

        [DataMember(Name = "біздің")]
        public DeclensionForms FirstPersonPlural { get; set; }

        [DataMember(Name = "сендердің")]
        public DeclensionForms SecondPersonPlural { get; set; }

        [DataMember(Name = "сіздердің")]
        public DeclensionForms SecondPersonRespectfulPlural { get; set; }

        [DataMember(Name = "олардың")]
        public DeclensionForms ThirdPersonPlural { get; set; }
    }
}