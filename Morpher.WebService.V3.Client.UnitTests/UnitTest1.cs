using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Morpher.WebService.V3.Client.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void InvalidUrlThrows()
        {
            new MorpherClient(null, "http://dns-error-fjeqweWe3cu.com").Russian.Parse("кошка");
        }
    }
}
