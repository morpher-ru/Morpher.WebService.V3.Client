using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Qazaq
{
    [DataContract]
    public class DeclensionForms
    {
        [DataMember(Name = "А")] public string Nominative { get; set; }
        [DataMember(Name = "І")] public string Genitive { get; set; }
        [DataMember(Name = "Б")] public string Dative { get; set; }
        [DataMember(Name = "Т")] public string Accusative { get; set; }
        [DataMember(Name = "Ш")] public string Instrumental { get; set; }
        [DataMember(Name = "Ж")] public string Prepositional { get; set; }
        [DataMember(Name = "К")] public string Vocative { get; set; }
    }
}