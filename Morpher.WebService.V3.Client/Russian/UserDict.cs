namespace Morpher.WebService.V3.Russian
{
    using System;
    using System.Collections.Generic;

    public class UserDict
    {
        private readonly Func<MyWebClient> _newClient;

        internal UserDict(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public void AddOrUpdate(CorrectionEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Singular.Nominative))
            {
                throw new ArgumentException("Нужно указать именительную форму единственного числа.", nameof(entry.Singular.Nominative));
            }

            var collection = entry.ToNameValueCollection();
            if (collection.Count <= 1)
            {
                throw new ArgumentException("Нужно указать как минимум одну форму кроме именительного падежа.", nameof(entry));

            }

            using (var client = _newClient())
            {
                client.UploadValues("/russian/userdict", collection);
            }
        }

        public bool Remove(string nominativeForm)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", nominativeForm);
                return client.DeleteRequest<bool>("/russian/userdict");
            }
        }

        public IEnumerable<CorrectionEntry> GetAll()
        {
            using (var client = _newClient())
            {
                return client.GetObject<IEnumerable<CorrectionEntry>>("/russian/userdict");
            }
        }
    }
}
