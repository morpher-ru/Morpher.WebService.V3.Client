namespace Morpher.WebSerivce.V3
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

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