namespace Morpher.API.Samples
{
    using System;
    using System.Text;

    using Morpher.API.V3;

    public class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IMorpherClient morpherClient = new MorpherClient();

            Console.WriteLine(morpherClient.ParseRussian("пес").Genitive);
        }
    }
}
