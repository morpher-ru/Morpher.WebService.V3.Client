using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Russian
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [DataContract(Name = "xml", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : DeclensionForms
    {
        [DataMember(Name = "род")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? Gender { get; set; }

        [DataMember(Name = "множественное")]
        public DeclensionForms Plural { get; set; }

        [DataMember(Name = "ФИО")]
        public FullName FullName { get; set; }

        [DataMember(Name = "где")]
        public string Where { get; set; }

        [DataMember(Name = "куда")]
        public string To { get; set; }

        [DataMember(Name = "откуда")]
        public string From { get; set; }
        
        [DataMember(Name = "одушевленное")]
        public bool IsAnimate { get; set; }

        [DataMember(Name = "счетнаяформа")]
        public string Paucal { get; set; }

        [DataMember(Name = "М")]
        public string Locative { get; set; }
    }
}