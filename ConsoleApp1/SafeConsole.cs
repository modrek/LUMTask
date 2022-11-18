using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace LUMTask.Helpers
{
    public static class SafeConsole
    {
        private const string UnsafeMessage = "Warning, the parameter is suspicious of XSS attack!!";
        /// <summary>
        /// Write input text in the console if it's XSS-safe.
        /// </summary>
        /// <param name="input"></param>
        public static void WriteLine(object input)
        {
            Console.WriteLine(ValidateAntiXss(input) ? input : UnsafeMessage);
        }

        /// <summary>
        /// Write input text in the console if it's XSS-safe.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public static void WriteLine(string format, params object[] arg)
        {
            if (arg.Any(a => !ValidateAntiXss(a)))
            {
                Console.WriteLine(UnsafeMessage);
                return;
            }

            Console.WriteLine(format ?? string.Empty, arg);
        }

        /// <summary>
        /// Check the input to prevent XSS vulnerability.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ValidateAntiXss(object input)
        {
            if (input == null)
                return true;

            var pattern = new StringBuilder();

            //Checks any js events i.e. onKeyUp(), onBlur(), alerts and custom js functions etc.
            pattern.Append(@"((alert|on\w+|function\s+\w+)\s*\(\s*(['+\d\w](,?\s*['+\d\w]*)*)*\s*\))");

            //Checks any html tags i.e. <script, <embed, <object etc.
            pattern.Append(
                @"|(<(script|iframe|embed|frame|frameset|object|img|applet|body|html|style|layer|link|ilayer|meta|bgsound))");

            return !Regex.IsMatch(input.ToString() ?? string.Empty, pattern.ToString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
