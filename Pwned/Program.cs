using System;

namespace Pwned
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            CheckPassword();
        }

        static void CheckPassword()
        {
            Console.Write("Enter password to check: ");
            var password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                Environment.Exit(0);
            }
            var response = PwnedService.GetCount(password);
            if (response.Status == Status.OK)
            {
                    Console.WriteLine(Snarkifier.Snarkify(response.Value));
            }
            else
            {
                Console.WriteLine("Could not check password");
                if (!string.IsNullOrEmpty(response.Message))
                {
                    Console.WriteLine(" - " + response.Message);
                }
            }
            Console.WriteLine();
            CheckPassword();
        }
    }
}
