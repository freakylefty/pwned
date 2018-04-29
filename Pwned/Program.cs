using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (response.Value == 0)
                {
                    Console.WriteLine("This password was not found in Pwned Passwords.  Good for it.");
                }
                else if (response.Value == 1)
                {
                    Console.WriteLine("This password was found once in Pwned Passwords. Not too shabby.");
                }
                else
                {
                    var num = StringUtil.FormatNumber(response.Value);
                    var snark = "";
                    if (response.Value < 10)
                    {
                        snark = "Not too shabby.";
                    }
                    else if (response.Value < 100)
                    {
                        snark = "*raises eyebrow*";
                    }
                    else if (response.Value < 1000)
                    {
                        snark = "Naughty.";
                    }
                    else if (response.Value < 5000)
                    {
                        snark = "Wait, you're not actually using it, are you?";
                    }
                    else
                    {
                        snark = "*screaming intensifies*";
                    }
                    Console.WriteLine(string.Format("This password was found {0} times in Pwned Passwords. {1}", num, snark));
                }
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
