namespace Morpher.WebService.V3.Ukrainian
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CorrectionForms
    {
        [DataMember(Name = "Н")] public string Nominative { get; set; }
        [DataMember(Name = "Р")] public string Genitive { get; set; }
        [DataMember(Name = "Д")] public string Dative { get; set; }
        [DataMember(Name = "З")] public string Accusative { get; set; }
        [DataMember(Name = "О")] public string Instrumental { get; set; }
        [DataMember(Name = "М")] public string Prepositional { get; set; }
        [DataMember(Name = "К")] public string Vocative { get; set; }
    }
}
