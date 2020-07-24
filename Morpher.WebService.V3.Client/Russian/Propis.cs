using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Russian
{
    [DataContract]
    public class Propis
    {
        [DataMember]
        public string propis1 { get; set; }

        [DataMember]
        public string propis2 { get; set; }

        [DataMember]
        public string propis3 { get; set; }

        [DataMember]
        public string amount { get; set; }

        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string cents { get; set; }
    }
}