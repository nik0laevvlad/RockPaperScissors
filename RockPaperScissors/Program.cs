using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            int rounds = args.Length;
            if (args.Length < 3 || args.Length % 2 == 0 || SameArgs(args))
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                StartGame(args);
            }
        }

        static void DrawMenu(string[] args)
        {
            Console.WriteLine("Avaliable moves: ");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {args[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        static void StartGame(string[] args)
        {
            LoosersTable loosersTable = new LoosersTable(args);
            Cryptography cryptography = new Cryptography();
            cryptography.CalculateSecretKey();
            int computerChoise = cryptography.GetComputerChoise(args);
            ShowHMAC(cryptography.GetHMAC(args[computerChoise]));
            DrawMenu(args);
            int userMove = GetUserMove(args.Length);
            switch (userMove)
            {
                default:
                    Game game = new Game(loosersTable);
                    string result = game.CalculateResult(computerChoise, userMove - 1);
                    ShowResult(result, args[userMove - 1], args[computerChoise], cryptography.GetSecretKey());
                    break;
                case -1:
                    Help help = new Help(args, loosersTable);
                    ShowHelp(help.GenerateHelp());
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
            StartGame(args);
        }

        static private void ShowHMAC(string hmac)
        {
            Console.WriteLine("HMAC:");
            Console.WriteLine(hmac);
            Console.WriteLine();
        }

        static private int GetUserMove(int availableMoves)
        {
            string move = Console.ReadLine();
            if (move == "?") return -1;
            if (int.TryParse(move, out int result))
                if (result < 0 || result > availableMoves)
                {
                    Console.WriteLine("Incorrect value!");
                    GetUserMove(availableMoves);
                }
            return result;
        }

        static private void ShowResult(string result, string userMove, string computerChoise, string secretKey)
        {
            Console.WriteLine("Your move: " + userMove);
            Console.WriteLine("Computer move: " + computerChoise);
            Console.WriteLine("Result: " + result);
            Console.WriteLine("HMAC key: " + secretKey);
            Console.WriteLine();
        }

        static private void ShowHelp(string[] helpText)
        {
            foreach (string element in helpText)
                Console.WriteLine(element);
            Console.WriteLine();
        }

        static private bool SameArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                for (int g = i + 1; g < args.Length; g++)
                {
                    if (args[i] == args[g])
                        return true;
                }
            }
            return false;
        }
    }
}
