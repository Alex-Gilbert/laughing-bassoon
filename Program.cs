namespace GameOfLife
{
    class Program
    {
        public static void Main(string[] args)
        {
            string boardName = "RANDOM";
            int generations = 4;
            int sleepInterval = 0;
            bool clear = false;
            char aliveSymbol = 'X';
            char deadSymbol = '.';

            if (!ParseCommandLineArgs(args, ref boardName, ref generations, ref clear, ref sleepInterval, ref aliveSymbol, ref deadSymbol))
            {
                return;
            }

            Simulator sim = new Simulator(BoardFactory.GetPremadeBoard(boardName));
            Renderer renderer = new Renderer(deadSymbol, aliveSymbol);

            Console.WriteLine(boardName);

            for (int i = 0; i < generations; i++)
            {
                if (clear)
                {
                    Console.Clear();
                    Console.WriteLine(boardName);
                }

                renderer.Render(sim.GetCurrentBoard());
                Console.WriteLine();

                sim.Update();

                if (sleepInterval > 0)
                {
                    System.Threading.Thread.Sleep(sleepInterval);
                }
            }
        }

        private static bool ParseCommandLineArgs(string[] args,
                ref string board,
                ref int generations,
                ref bool clear,
                ref int sleepInterval,
                ref char aliveSymbol,
                ref char deadSymbol)
        {
            // Parse command line arguments
            int currentArg = 0;
            while (currentArg < args.Length)
            {
                if (currentArg == 0 && !args[currentArg].StartsWith("--"))
                {
                    board = args[currentArg];
                    currentArg++;

                    if (BoardFactory.DoesBoardExist(board))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Board {board} does not exist");
                        return false;
                    }
                }

                if (args[currentArg] == "--help")
                {
                    PrintHelp();
                    return false;
                }

                if (args[currentArg] == "--generations")
                {
                    if (currentArg + 1 >= args.Length)
                    {
                        Console.WriteLine("Error: --generations requires an argument");
                        return false;
                    }

                    if (!int.TryParse(args[currentArg + 1], out generations))
                    {
                        Console.WriteLine("Error: --generations requires an integer argument");
                        return false;
                    }

                    currentArg += 2;
                    continue;
                }

                if (args[currentArg] == "--clear")
                {
                    clear = true;
                    currentArg++;
                    continue;
                }

                if (args[currentArg] == "--sleep")
                {
                    if (currentArg + 1 >= args.Length)
                    {
                        Console.WriteLine("Error: --sleep requires an argument");
                        return false;
                    }

                    if (!int.TryParse(args[currentArg + 1], out sleepInterval))
                    {
                        Console.WriteLine("Error: --sleep requires an integer argument");
                        return false;
                    }

                    currentArg += 2;
                    continue;
                }

                if (args[currentArg] == "--alive-symbol")
                {
                    if (currentArg + 1 >= args.Length)
                    {
                        Console.WriteLine("Error: --alive-symbol requires an argument");
                        return false;
                    }

                    if (args[currentArg + 1].ToUpper() == "SPACE")
                    {
                        aliveSymbol = ' ';
                        currentArg += 2;
                        continue;
                    }
                    else
                    {
                        aliveSymbol = args[currentArg + 1][0];
                        currentArg += 2;
                        continue;
                    }
                }

                if (args[currentArg] == "--dead-symbol")
                {
                    if (currentArg + 1 >= args.Length)
                    {
                        Console.WriteLine("Error: --dead-symbol requires an argument");
                        return false;
                    }

                    if (args[currentArg + 1].ToUpper() == "SPACE")
                    {
                        deadSymbol = ' ';
                        currentArg += 2;
                        continue;
                    }
                    else
                    {
                        deadSymbol = args[currentArg + 1][0];
                        currentArg += 2;
                        continue;
                    }
                }

                Console.WriteLine($"Error: Unknown argument {args[currentArg]}");
                return false;
            }

            return true;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Usage: gol [board] [--generations n] [--clear] [--sleep ms] [--alive-symbol s] [--dead-symbol s]");

            string[] availableBoards = BoardFactory.GetAvailableBoards();
            Console.WriteLine("Available boards: RANDOM, " + string.Join(", ", availableBoards) + ". Default is RANDOM");

            Console.WriteLine("--generations n: Run for n generations. Default is 4");
            Console.WriteLine("--sleep ms: Sleep for ms milliseconds between generations. Default is 0");
            Console.WriteLine("--clear: Clear the console between generations");
            Console.WriteLine("--alive-symbol s: Use s as the symbol for alive cells. Use SPACE for a blank space. Default is X");
            Console.WriteLine("--dead-symbol s: Use s as the symbol for dead cells. Use SPACE for a blank space. Default is .");
        }
    }
}
