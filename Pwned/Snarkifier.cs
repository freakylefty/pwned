namespace Pwned
{
    class Snarkifier
    {
        public static string Snarkify(int count)
        {
            if (count == 0)
            {
                return "This password was not found in Pwned Passwords.  Good for it.";
            }
            if (count == 1)
            {
                return "This password was found once in Pwned Passwords. So close.";
            }
            var num = StringUtil.FormatNumber(count);
            var snark = "";
            if (count < 10)
            {
                snark = "Not too shabby.";
            }
            else if (count < 100)
            {
                snark = "*raises eyebrow*";
            }
            else if (count < 1000)
            {
                snark = "Naughty.";
            }
            else if (count < 5000)
            {
                snark = "Wait, you're not actually using it, are you?";
            }
            else
            {
                snark = "*screaming intensifies*";
            }
            return string.Format("This password was found {0} times in Pwned Passwords. {1}", num, snark);
        }
    }
}
