using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Morpher.WebService.V3.Ukrainian
{
    [DataContract]
    public class DeclensionForms : IEquatable<DeclensionForms>
    {
        [DataMember(Name = "Н")]
        public string Nominative { get; set; }

        [DataMember(Name = "Р")]
        public string Genitive { get; set; }

        [DataMember(Name = "Д")]
        public string Dative { get; set; }

        [DataMember(Name = "З")]
        public string Accusative { get; set; }

        [DataMember(Name = "О")]
        public string Instrumental { get; set; }

        [DataMember(Name = "М")]
        public string Prepositional { get; set; }

        [DataMember(Name = "К")]
        public string Vocative { get; set; }

        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        public static bool operator ==(DeclensionForms left, DeclensionForms right)
        {
            return Equals(left, right);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        public static bool operator !=(DeclensionForms left, DeclensionForms right)
        {
            return !Equals(left, right);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1503")]
        public bool Equals(DeclensionForms other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.Nominative, other.Nominative) &&
                string.Equals(this.Genitive, other.Genitive) && 
                string.Equals(this.Dative, other.Dative) && 
                string.Equals(this.Accusative, other.Accusative) &&
                string.Equals(this.Instrumental, other.Instrumental) && 
                string.Equals(this.Prepositional, other.Prepositional) && 
                string.Equals(this.Vocative, other.Vocative);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1503")]
        [SuppressMessage("ReSharper", "StyleCop.SA1126")]
        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((DeclensionForms)obj);
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1119")]
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (this.Nominative != null ? this.Nominative.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Genitive != null ? this.Genitive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Dative != null ? this.Dative.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Accusative != null ? this.Accusative.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Instrumental != null ? this.Instrumental.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Prepositional != null ? this.Prepositional.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Vocative != null ? this.Vocative.GetHashCode() : 0);
                return hashCode;
            }
        }

    }
}