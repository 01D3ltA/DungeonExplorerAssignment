using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameTest.RunTests(); // Run the tests before starting the game
                DungeonExplorer.Game game = new DungeonExplorer.Game(); // creates a game object

                // Create a Player and Inventory object
                Console.WriteLine("Enter your name Hero:");
                string playerName = Console.ReadLine();
                Player player = new Player(playerName, 25, 10, 1, 0); // Removed redundant namespace
                Inventory inventory = new Inventory(); // Removed redundant namespace

                // Pass the Player and Inventory objects to the Start method
                game.Start(player, inventory);
            }
            catch (Exception ex) // error check, if an exception is thrown, a message is displayed
            {
                Console.WriteLine($"An error occurred {ex.Message}");
            }
            finally // Once the game is finished running, we await for a user key input and end the program
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
