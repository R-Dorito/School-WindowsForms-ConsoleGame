using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class GameManager
    {
        Player player;
        MapLoader load;
        Dictionary<string, dynamic> GameList;
        string playerName;
        string playerDescription;

        public void GameLoop()
        {
            GameOpening();
            InitializePlayer();

            System.Threading.Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine($"Welcome to the game");
            Console.WriteLine($"Your story begins in the {player.MyLocation.Name}");

            Command myCommand = new Command_Processor();
            do
            {
                string command = getInput("Command ->");

                if(command == "quit")
                {
                    Console.WriteLine("Quititng game. Goodbye my friend");
                    Environment.Exit(0);
                }
                else if(command == "help")
                {
                    Console.WriteLine(DisplayHelp());
                }
                else if (command == "about")
                {
                    Console.WriteLine(DisplayAbout());
                }
                else
                {
                    Console.WriteLine(myCommand.Execute(player, command.Split(" ")));
                }
                Console.WriteLine();

            } while (true);
        }

        private string DisplayHelp()
        {
            string message;
            message = "Welcome to SwinGame\n" +
                "This is a console game that now comes in Windows Form style.\n" +
                "Things you can type:\n" +
                "look -> look at the room\n" +
                "look at (item id) -> look at an item\n" +
                "move (direction) -> travel to another room\n" +
                "pickup (item id) -> take an item from a room\n" +
                "drop (item id) -> drop a held item into a room.\n\n" +
                "I believe that is all that is needed";

            return message;
        }
        private string DisplayAbout()
        {
            string message;
            message = "Made with love\n" +
                "By Rita Uy\n" +
                "Date: 20/05/2021\n" +
                "Version: 1\n";

            return message;
        }

        private void InitializePlayer()
        {
            playerName = getInput("What is your name?");
            playerDescription = getInput("How would you describe yourself?");

            player = new Player(playerName, playerDescription);
            load = new MapLoader(player);
            try
            {
                GameList = load.GameList;
                player.MyLocation = GameList["Front Porch"];
            }
            catch
            {
                Console.WriteLine("Error with file reading, type any key to close program.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private void GameOpening()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("| -------------------------------- |");
            Console.WriteLine("| Hello, welcome to SwinAdventure! |");
            Console.WriteLine("| -------------------------------- |");
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(250);
                Console.WriteLine("*");
            }
        }
        private string getInput(string message)
        {
            string input;
            do
            {
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                input = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input is empty, please try again");
                    Console.ForegroundColor = ConsoleColor.Black;

                }
                Console.ForegroundColor = ConsoleColor.Black;

            } while (String.IsNullOrEmpty(input));

            return input;
        }
    }
}
