namespace Morpher.WebService.V3.Ukrainian
{
    using System.Runtime.Serialization;

    [DataContract]
    public enum Gender
    {
        [EnumMember(Value = "Чоловічий")] Masculine,
        [EnumMember(Value = "Жіночий")] Feminine,
        [EnumMember(Value = "Середній")] Neuter,
        [EnumMember(Value = "Множина")] Plural
    }
}
