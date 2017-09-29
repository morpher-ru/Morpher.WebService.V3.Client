namespace Morpher.WebService.V3.Russian
{
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [DataContract]
    public class CorrectionEntry
    {
        [DataMember(Name = "singular")]
        public CorrectionForms Singular { get; set; }
        
        [DataMember(Name = "plural")]
        public CorrectionForms Plural { get; set; }

        [DataMember(Name = "gender")]
        public Gender? Gender { get; set; }

        public NameValueCollection ToNameValueCollection()
        {
            NameValueCollection collection = new NameValueCollection();

            // Singular
            AddToCollection("И", Singular.Nominative, collection);
            AddToCollection("Р", Singular.Genitive, collection);
            AddToCollection("Д", Singular.Dative, collection);
            AddToCollection("В", Singular.Accusative, collection);
            AddToCollection("Т", Singular.Instrumental, collection);
            AddToCollection("П", Singular.Prepositional, collection);
            AddToCollection("М", Singular.Locative, collection);

            // Plural
            if (Plural != null)
            {
                AddToCollection("М_И", Plural.Nominative, collection);
                AddToCollection("М_Р", Plural.Genitive, collection);
                AddToCollection("М_Д", Plural.Dative, collection);
                AddToCollection("М_В", Plural.Accusative, collection);
                AddToCollection("М_Т", Plural.Instrumental, collection);
                AddToCollection("М_П", Plural.Prepositional, collection);
                AddToCollection("М_М", Plural.Locative, collection);
            }

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
