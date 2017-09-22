namespace Morpher.WebService.V3.Russian
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CorrectionEntry
    {
        [DataMember(Name = "singular")]
        public CorrectionForms Singular { get; set; }
        
        [DataMember(Name = "plural")]
        public CorrectionForms Plural { get; set; }

        [DataMember(Name = "gender")]
        public Gender? Gender { get; set; }
    }
}
