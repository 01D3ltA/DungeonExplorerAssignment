using DungeonExplorer.DungeonExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class GameTest
    {
        public static void RunTests()
        {
            Console.WriteLine("Starting Game Tests...");

            // Test 1: Initialize Game
            Game game = new Game();
            Console.WriteLine("Test 1: Game initialized successfully.");

            // Test 2: Create Player and Inventory
            Player player = new Player("TestHero", 100, 15, 1, 0);
            Inventory inventory = new Inventory();
            Console.WriteLine("Test 2: Player and Inventory created successfully.");

            // Test 3: Start Game
            try
            {
                game.Start(player, inventory);
                Console.WriteLine("Test 3: Game started successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test 3 Failed: {ex.Message}");
            }

            // Test 4: Move Between Rooms
            try
            {
                Console.WriteLine("Test 4: Testing room movement...");
                game.Start(player, inventory); // Start the game to initialize rooms
                Console.WriteLine("Move to a valid direction (if available):");
                game.Move(); // Simulate a move
                Console.WriteLine("Test 4: Room movement tested successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test 4 Failed: {ex.Message}");
            }

            // Test 5: Search for Items
            try
            {
                Console.WriteLine("Test 5: Testing item search...");
                game.SearchForItems(inventory);
                Console.WriteLine("Test 5: Item search tested successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test 5 Failed: {ex.Message}");
            }

            // Test 6: Fight Monsters
            try
            {
                Console.WriteLine("Test 6: Testing monster combat...");
                game.FightMonsters(player, inventory);
                Console.WriteLine("Test 6: Monster combat tested successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test 6 Failed: {ex.Message}");
            }

            // Test 7: Inventory Management
            try
            {
                Console.WriteLine("Test 7: Testing inventory management...");
                Item testItem = new Weapon("Test Sword", "A test weapon.", 10, 50, 20);
                inventory.AddItem(testItem);
                inventory.DisplayInventory();
                inventory.UseItem("Test Sword");
                inventory.DisplayInventory();
                Console.WriteLine("Test 7: Inventory management tested successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test 7 Failed: {ex.Message}");
            }

            Console.WriteLine("Game Tests Completed.");
        }
    }
}
