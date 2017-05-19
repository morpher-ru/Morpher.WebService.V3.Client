using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Russian
{
    [DataContract]
    public class DeclensionForms
    {
        [DataMember(Name = "И")] public string Nominative { get; set; }
        [DataMember(Name = "Р")] public string Genitive { get; set; }
        [DataMember(Name = "Д")] public string Dative { get; set; }
        [DataMember(Name = "В")] public string Accusative { get; set; }
        [DataMember(Name = "Т")] public string Instrumental { get; set; }
        [DataMember(Name = "П")] public string Prepositional { get; set; }
        [DataMember(Name = "П-о")] public string PrepositionalWithO { get; set; }
   }
}