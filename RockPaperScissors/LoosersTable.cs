namespace RockPaperScissors
{
    class LoosersTable
    {
        private string[] arguments;

        public LoosersTable(string[] args) { arguments = args; }
        public int[] calculateLoosers(int element)
        {
            int[] loosers = new int[arguments.Length / 2];
            for (int i = 1; i <= loosers.Length; i++)
            {
                if (element - i < 0)
                    loosers[i - 1] = element - i + arguments.Length;
                else
                    loosers[i - 1] = element - i;
            }
            return loosers;
        }
    }
}
