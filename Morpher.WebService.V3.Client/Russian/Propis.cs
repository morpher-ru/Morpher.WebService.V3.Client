using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Russian
{
    [DataContract]
    public class Propis
    {
        [DataMember(Name = "propis1")]
        public string Propis1 { get; set; }

        [DataMember(Name = "propis2")]
        public string Propis2 { get; set; }

        [DataMember(Name = "propis3")]
        public string Propis3 { get; set; }

        [DataMember(Name = "amount")]
        public string Amount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "cents")]
        public string Cents { get; set; }
    }
}