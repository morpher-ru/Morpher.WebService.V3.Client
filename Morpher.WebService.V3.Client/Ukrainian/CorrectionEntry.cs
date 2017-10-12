namespace Morpher.WebService.V3.Ukrainian
{
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [DataContract]
    public class CorrectionEntry
    {
        [DataMember(Name = "singular")]
        public CorrectionForms Singular { get; set; }

        [DataMember(Name = "gender")]
        public Gender? Gender { get; set; }

        public NameValueCollection ToNameValueCollection()
        {
            NameValueCollection collection = new NameValueCollection();

            // Singular
            AddToCollection("Н", Singular.Nominative, collection);
            AddToCollection("Р", Singular.Genitive, collection);
            AddToCollection("Д", Singular.Dative, collection);
            AddToCollection("З", Singular.Accusative, collection);
            AddToCollection("О", Singular.Instrumental, collection);
            AddToCollection("М", Singular.Prepositional, collection);
            AddToCollection("К", Singular.Vocative, collection);

            return collection;
        }

        private void AddToCollection(string form, string value, NameValueCollection collection)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                collection.Add(form, value);
            }
        }
    }
}