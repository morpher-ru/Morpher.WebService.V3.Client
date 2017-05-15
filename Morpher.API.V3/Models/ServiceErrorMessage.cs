namespace Morpher.API.V3.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ServiceErrorMessage
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
