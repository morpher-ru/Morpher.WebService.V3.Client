namespace Morpher.WebSerivce.V3
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
