namespace Morpher.WebService.V3.Russian
{
    using System.Runtime.Serialization;

    [DataContract]
    public struct ResultOrError
    {
        [DataMember(Name = "DeclensionResult")] public DeclensionResult Result { get; set; }
        [DataMember(Name = "Error")] public ServiceErrorMessage Error { get; set; }
    }
}
