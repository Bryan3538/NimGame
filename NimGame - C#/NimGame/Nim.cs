using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class Nim
    {
        private enum players { User, Computer };
        private const ConsoleColor SYSTEM_MESSAGE_COLOR = ConsoleColor.Green;
        private const ConsoleColor INPUT_PROMPT_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor ERROR_MESSAGE_COLOR = ConsoleColor.Red;
        private const ConsoleColor GAME_BOARD_COLOR = ConsoleColor.Cyan;

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);

            StartNim();
        }

        private static void StartNim()
        {
            string input;
            bool exit = false;

            PrintIntroduction();

            do
            {
                PlayGame();

                Console.WriteLine("Do you wish to play another game of Nim? Enter \'y\' to continue.");
                input = Console.ReadLine();

                if (!input.Equals("y", StringComparison.OrdinalIgnoreCase))
                    exit = true;

            } while (!exit);
            

        }

        private static void PrintIntroduction()
        {
            Console.ForegroundColor = SYSTEM_MESSAGE_COLOR;
            Console.WriteLine("Welcome to the game of Nim!\n"
                + "This game contains several piles of sticks, each containing a various number of them.\n"
                + "You and your opponent, the computer AI, will each take turns removing a specified number of sticks\n"
                + "from a chosen pile until no piles contain any sticks.\n"
                + "The player to remove the last sticks from the last pile loses.\n\n");
            Console.ResetColor();
        }

        private static void PlayGame()
        {
            players currentPlayer = players.User;
            NimAi computer = new NimAi();
            int pileNumber = 0, sticksToRemove = 0;
            bool winner = false;
            Pile[] piles = new Pile[GetIntInRange(0, 10, "How many piles do you wish to have?")];
            
            InitializePileSizes(piles);
            currentPlayer = GetFirstPlayer();

            while(!winner)
            {
                PrintPiles(piles);
                Console.WriteLine();
                if (currentPlayer == players.User)
                {
                    GetUserMove(piles, out pileNumber, out sticksToRemove);
                    piles[pileNumber].Count -= sticksToRemove;
                    AnnounceMove(currentPlayer, pileNumber, sticksToRemove);
                }
                else //player is computer
                {
                    computer.GetMove(piles, out pileNumber, out sticksToRemove);
                    piles[pileNumber].Count -= sticksToRemove;
                    AnnounceMove(currentPlayer, pileNumber, sticksToRemove);
                }

                winner = CheckForWinner(piles);
                
                if (winner)
                    AnnounceWinner(currentPlayer);
                else
                    if (currentPlayer == players.User)
                        currentPlayer = players.Computer;
                    else
                        currentPlayer = players.User;
            }

        }

        private static void AnnounceMove(players currentPlayer, int pileNumber, int sticksToRemove)
        {
            string playerDescription = "";

            if(currentPlayer == players.User)
            {
                playerDescription = "You have";
            }
            else
            {
                playerDescription = "The Computer has";
            }
            Console.ForegroundColor = SYSTEM_MESSAGE_COLOR;
            Console.WriteLine(playerDescription + " removed {0} sticks from pile {1}.", sticksToRemove, pileNumber + 1);
            Console.ResetColor();
        }

        private static void AnnounceWinner(players currentPlayer)
        {
            if(currentPlayer == players.User)
            {
                Console.ForegroundColor = SYSTEM_MESSAGE_COLOR;
                Console.WriteLine("Congratulations, player, you have won the game of Nim!");
            }
            else
            {
                Console.ForegroundColor = ERROR_MESSAGE_COLOR;
                Console.WriteLine("Sorry, the computer has beaten you. Better luck next time!");
            }

            Console.ResetColor();
        }

        private static bool CheckForWinner(Pile[] piles)
        {
            foreach(Pile pile in piles)
            {
                if (pile.Count > 0)
                    return false;
            }

            return true;
        }

        private static void GetUserMove( Pile[] piles, out int pileNumber, out int sticksToRemove)
        {
            bool validPile = false;

            do
            {
                pileNumber = GetIntInRange(1, piles.Length, "Please enter the pile you wish to remove sticks from.") - 1;

                if(piles[pileNumber].Count == 0)
                {
                    Console.ForegroundColor = ERROR_MESSAGE_COLOR;
                    Console.WriteLine("You may only select a pile that still has sticks in it!");
                }
                else
                {
                    validPile = true;
                }
            } while (!validPile);

            sticksToRemove = GetIntInRange(1, piles[pileNumber].Count, 
                String.Format("Please enter how many sticks you wish to remove from pile {0}.", pileNumber + 1));

            Console.WriteLine();
        }

        private static players GetFirstPlayer()
        {
            string input;
            players firstPlayer;
            Console.ForegroundColor = INPUT_PROMPT_COLOR;
            Console.WriteLine("Do you wish to play first?");
            Console.WriteLine("Enter \'y\' to play first, otherwise you will go second.");
            input = Console.ReadLine();

            Console.ForegroundColor = SYSTEM_MESSAGE_COLOR;
            if(input.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You, the user, will go first.");
                firstPlayer = players.User;
            }
            else
            {
                Console.WriteLine("The computer will go first.");
                firstPlayer = players.Computer;
            }

            Console.ResetColor();
            Console.WriteLine();
            return firstPlayer;
        }

        private static void PrintPiles(Pile[] piles)
        {
            Console.ForegroundColor = GAME_BOARD_COLOR;
            for(int i = 0; i < piles.Length; i++)
            {
                Console.WriteLine("Pile {0}: {1}\t {2,10}", i + 1, piles[i].Count, piles[i].ToString());
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void InitializePileSizes(Pile[] piles)
        {
            for(int i = 0; i < piles.Length; i++)
            {
                piles[i] = new Pile(GetIntInRange(0, int.MaxValue, 
                    String.Format("Please enter how many sticks you wish there to be in pile {0}.", i + 1)));
            }
            Console.WriteLine();
        }

        private static int GetIntInRange(int min, int max, string message)
        {
            
            int number = 0;
            bool validInput = false;

            if (min > max)
                throw new ArgumentException("The minimum value must be less than the maximum value!");

            while(!validInput)
            {
                Console.ForegroundColor = INPUT_PROMPT_COLOR;
                Console.WriteLine(message);
                

                if(!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.ForegroundColor = ERROR_MESSAGE_COLOR;
                    Console.WriteLine("You may only enter whole numbers.");
                }
                else if (number < min || number > max)
                {
                    Console.ForegroundColor = ERROR_MESSAGE_COLOR;
                    Console.WriteLine("You may only enter numbers between {0} and {1}.", min, max);
                }
                else
                {
                    validInput = true;
                }
            }
            Console.ResetColor();
            return number;
        }
    }
}
