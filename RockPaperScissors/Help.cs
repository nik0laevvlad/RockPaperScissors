using System;

namespace RockPaperScissors
{
    class Help
    {
        private string[] arguments;
        private LoosersTable victimsTable;

        public Help(string[] args, LoosersTable table) { arguments = args; victimsTable = table; }
        public string[] GenerateHelp()
        {
            const int reservedStringsForHelp = 12;
            const int reservedStringsForHeader = 2;
            const int reservedStrings = reservedStringsForHelp + reservedStringsForHeader;
            string[] helpText = new string[arguments.Length * 2 + reservedStrings];


            helpText[reservedStringsForHelp] = string.Format("{0}", " \\User".PadRight(CalculateLongestElement(), ' ')) + " |";
            helpText[reservedStringsForHelp + 1] = string.Format("{0}", "PC\\  ".PadRight(CalculateLongestElement(), ' ')) + " |";


            for (int i = 0; i < arguments.Length; i++)
            {
                helpText[reservedStringsForHelp] += string.Format("{0}", "".PadRight(CalculateLongestElement(i), ' ')) + "|";
                helpText[reservedStringsForHelp + 1] += string.Format("{0}", (" " + arguments[i] + " ").PadRight(6, ' ')) + "|";
                helpText[i * 2 + reservedStrings] = CreateBarier(-1);
                helpText[i * 2 + 1 + reservedStrings] = string.Format("{0}", arguments[i].PadRight(CalculateLongestElement(), ' ')) + " |";
                for (int g = 0; g < arguments.Length; g++)
                {
                    helpText[i * 2 + reservedStrings] += CreateBarier(g);
                    helpText[i * 2 + 1 + reservedStrings] += string.Format("{0}", CalculateResult(i, g).PadRight(CalculateLongestElement(g), ' ')) + "|";
                }
            }

            return helpText;
        }

        private string CreateBarier(int mod)
        {
            string result = "";
            if (mod == -1)
            {
                result = string.Format("{0}", result.PadRight(CalculateLongestElement(), '-'));
                result += "-+";
                return result;
            }
            result = string.Format("{0}", result.PadRight(CalculateLongestElement(mod), '-'));
            result += "+";
            return result;
        }

        private int CalculateLongestElement()
        {
            int maxLength = 6;
            foreach (string element in arguments)
                if (element.Length > maxLength)
                    maxLength = element.Length;
            return maxLength;
        }

        private int CalculateLongestElement(int element)
        {
            int maxLength = 6;
            if (arguments[element].Length + 2 > maxLength)
                maxLength = arguments[element].Length + 2;
            return maxLength;
        }

        private string CalculateResult(int element, int target)
        {
            if (element == target)
                return " DRAW";
            if (Array.IndexOf(victimsTable.calculateLoosers(target), element) != -1)
                return " WIN";
            return " LOSE";
        }
    }
}
