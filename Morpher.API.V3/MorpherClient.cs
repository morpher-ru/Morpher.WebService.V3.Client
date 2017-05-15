namespace Morpher.API.V3
{
    using System;

    using Morpher.API.V3.Interfaces;

    public class MorpherClient : IMorpherClient
    {
        public MorpherClient(Guid? token = null, string url = null)
        {
            string apiUrl = url ?? "http://api3.morpher.ru";
            this.Russian = new Russian(apiUrl, token);
            this.Ukrainian = new Ukrainian(apiUrl, token);
        }

        public IRussian Russian { get; }
        
        public IUkrainian Ukrainian { get; }     
    }
}
