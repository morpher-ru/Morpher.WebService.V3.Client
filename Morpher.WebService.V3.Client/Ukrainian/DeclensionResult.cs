using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Ukrainian
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [DataContract(Name = "GetXmlUkrResult", Namespace = "http://schemas.datacontract.org/2004/07/Morpher.WebApi.Models")]
    public class DeclensionResult : DeclensionForms
    {
        [DataMember(Name = "рід")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? Gender { get; set; }
    }
}