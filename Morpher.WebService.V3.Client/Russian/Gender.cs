namespace Morpher.WebService.V3.Russian
{
    using System.Runtime.Serialization;

    [DataContract]
    public enum Gender
    {
        [EnumMember(Value = "Мужской")] Masculine,
        [EnumMember(Value = "Женский")] Feminine,
        [EnumMember(Value = "Средний")] Neuter,
        [EnumMember(Value = "Множественное")] Plural
    }
}