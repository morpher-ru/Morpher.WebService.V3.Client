using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Morpher.WebService.V3.Russian
{
    [DataContract]
    public class AdjectiveGenders
    {
        [DataMember(Name = "feminine")]
        public string Feminie { get; set; }

        [DataMember(Name = "neuter")]
        public string Neuter { get; set; }

        [XmlElement("plural")]
        [DataMember(Name = "plural")]
        public string Plural { get; set; }
    }
}