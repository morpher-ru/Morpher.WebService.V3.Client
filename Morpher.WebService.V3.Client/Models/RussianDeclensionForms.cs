namespace Morpher.WebService.V3
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [DataContract]
    public class RussianDeclensionForms : IEquatable<RussianDeclensionForms>
    {
        [DataMember(Name = "И")]
        public string Nominative { get; set; }

        [DataMember(Name = "Р")]
        public string Genitive { get; set; }

        [DataMember(Name = "Д")]
        public string Dative { get; set; }

        [DataMember(Name = "В")]
        public string Accusative { get; set; }

        [DataMember(Name = "Т")]
        public string Instrumental { get; set; }

        [DataMember(Name = "П")]
        public string Prepositional { get; set; }

        [DataMember(Name = "П-о")]
        public string Locative { get; set; }

        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        public static bool operator ==(RussianDeclensionForms left, RussianDeclensionForms right)
        {
            return Equals(left, right);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        public static bool operator !=(RussianDeclensionForms left, RussianDeclensionForms right)
        {
            return !Equals(left, right);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1503")]
        public bool Equals(RussianDeclensionForms other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.Nominative, other.Nominative) && string.Equals(this.Genitive, other.Genitive)
                   && string.Equals(this.Dative, other.Dative) && string.Equals(this.Accusative, other.Accusative)
                   && string.Equals(this.Instrumental, other.Instrumental)
                   && string.Equals(this.Prepositional, other.Prepositional)
                   && string.Equals(this.Locative, other.Locative);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1503")]
        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((RussianDeclensionForms)obj);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1119")]
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Nominative?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Genitive?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Dative?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Accusative?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Instrumental?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Prepositional?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Locative?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}