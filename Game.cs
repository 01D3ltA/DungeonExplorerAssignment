using System;
using System.IO;
using System.Media;

namespace DungeonExplorer
{
    using System;
    using System.Collections.Generic;

    namespace DungeonExplorer
    {
        internal class Game
        {
            private List<Room> rooms;
            private Room currentRoom;
            private Inventory inventory; // Fixed typo and made it private

            public Game()
            {
                rooms = new List<Room>();
                inventory = new Inventory(); // Initialize the inventory
                GenerateRandomRooms(100);
            }

            private void GenerateRandomRooms(int numberOfRooms)
            {
                Random random = new Random();
                List<Room> rooms = new List<Room>();
                HashSet<(int, int)> occupiedCoordinates = new HashSet<(int, int)>();

                // Create the starting room
                Room startingRoom = new Room("Starting Room", "The beginning of your adventure.", 0, 0);
                rooms.Add(startingRoom);
                currentRoom = startingRoom;
                occupiedCoordinates.Add((0, 0)); // Mark the starting room's coordinates as occupied

                // Define basic monster ideas
                List<(string Name, int Health, int AttackPower, int ExpDrop)> monsterTypes = new List<(string, int, int, int)>
                {
                    ("Goblin", 20, 5, 8),
                    ("Skeleton", 15, 7, 8),
                    ("Orc", 30, 10, 25),
                    ("Spider", 10, 3, 5),
                    ("Zombie", 25, 6, 10),
                    ("Vampire", 40, 12, 30),
                    ("Dragon", 100, 20, 100)

                };

                // Generate additional rooms
                for (int i = 1; i < numberOfRooms; i++)
                {
                    string roomName = $"Room {i}";
                    string roomDescription = $"This is a mysterious room numbered {i}.";

                    // Generate unique coordinates
                    int x, y;
                    do
                    {
                        x = random.Next(-10, 10); // Random X coordinate
                        y = random.Next(-10, 10); // Random Y coordinate
                    } while (occupiedCoordinates.Contains((x, y))); // Ensure coordinates are unique

                    occupiedCoordinates.Add((x, y)); // Mark the coordinates as occupied

                    Room newRoom = new Room(roomName, roomDescription, x, y);
                    rooms.Add(newRoom);

                    // Randomly connect the new room to an existing room
                    Room existingRoom = rooms[random.Next(rooms.Count)];
                    string[] directions = { "north", "south", "east", "west" };
                    string randomDirection = directions[random.Next(directions.Length)];
                    string oppositeDirection = GetOppositeDirection(randomDirection);

                    existingRoom.AddNeighbor(randomDirection, newRoom);
                    newRoom.AddNeighbor(oppositeDirection, existingRoom);

                    // Add a random monster to the room (50% chance)
                    if (random.Next(0, 2) == 1)
                    {
                        var monsterType = monsterTypes[random.Next(monsterTypes.Count)];
                        Monster monster = new Monster(monsterType.Name, monsterType.Health, monsterType.AttackPower, monsterType.ExpDrop);
                        newRoom.AddMonster(monster);
                    }
                }
            }
            
            private string GetOppositeDirection(string direction)
            {
                switch (direction)
                {
                    case "north":
                        return "south";
                    case "south":
                        return "north";
                    case "east":
                        return "west";
                    case "west":
                        return "east";
                    default:
                        throw new ArgumentException("Invalid direction");
                }
            }

            public void Start(Player player, Inventory inventory)
            {
                Console.WriteLine("Welcome to Dungeon Explorer!");
                bool isPlaying = true;

                while (isPlaying)
                {
                    Console.WriteLine($"\nYou are in the {currentRoom.Name}.");
                    Console.WriteLine(currentRoom.Description);

                    // Check for monsters in the current room
                    if (currentRoom.Monsters.Count > 0)
                    {
                        Console.WriteLine("There are monsters in the room!");
                    }

                    // Display available directions
                    Console.WriteLine("Exits:");
                    foreach (var neighbor in currentRoom.Neighbors)
                    {
                        Console.WriteLine($"- {neighbor.Key}");
                    }

                    // Player action
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("1. Move");
                    if (currentRoom.Monsters.Count == 0)
                    {
                        Console.WriteLine("2. Search for items");
                    }
                    else
                    {
                        Console.WriteLine("2. (Locked: Defeat all monsters to search for items)");
                    }
                    Console.WriteLine("3. View inventory");
                    Console.WriteLine("4. View player status");
                    Console.WriteLine("5. Exit game");
                    if (currentRoom.Monsters.Count > 0)
                    {
                        Console.WriteLine("6. Fight the monsters");
                    }

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Move();
                            break;
                        case "2":
                            if (currentRoom.Monsters.Count == 0)
                            {
                                SearchForItems(inventory);
                            }
                            else
                            {
                                Console.WriteLine("You cannot search for items while monsters are in the room!");
                            }
                            break;
                        case "3":
                            inventory.DisplayInventory();
                            break;
                        case "4":
                            ShowPlayerInfo(player);
                            break;
                        case "5":
                            isPlaying = false;
                            Console.WriteLine("Thanks for playing!");
                            break;
                        case "6":
                            if (currentRoom.Monsters.Count > 0)
                            {
                                FightMonsters(player, inventory);
                            }
                            else
                            {
                                Console.WriteLine("There are no monsters to fight.");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
            }

            private void ShowPlayerInfo(Player player)
            {
                Console.WriteLine($"Player Name: {player.Name}");
                Console.WriteLine($"Health: {player.GetHealth()}");
                Console.WriteLine($"Attack Power: {player.GetAttackPower()}");
                Console.WriteLine($"Level: {player.Level}");
                Console.WriteLine($"Experience: {player.CurrentExp}");
            }


            internal void FightMonsters(Player player, Inventory inventory)
            {
                while (currentRoom.Monsters.Count > 0)
                {
                    Monster monster = currentRoom.Monsters[0]; // Get the first monster in the room
                    Console.WriteLine($"A {monster.Name} appears! Health: {monster.Health}");

                    // Combat loop
                    while (monster.Health > 0)
                    {
                        Console.WriteLine("\nWhat would you like to do?");
                        Console.WriteLine("1. Attack");
                        Console.WriteLine("2. Run");
                        Console.WriteLine("3. Use Item");

                        string combatChoice = Console.ReadLine();
                        switch (combatChoice)
                        {
                            case "1":
                                // Player attacks the monster
                                int playerDamage = player.GetAttackPower();
                                monster.Health -= playerDamage;
                                Console.WriteLine($"You attack the {monster.Name} for {playerDamage} damage!");

                                if (monster.Health <= 0)
                                {
                                    Console.WriteLine($"You defeated the {monster.Name}!");
                                    currentRoom.Monsters.Remove(monster);
                                    player.GainExperience(monster.ExpDrop); // Gain experience from defeating the monster
                                    break;
                                }

                                // Monster attacks back
                                monster.Attack(player);
                                break;

                            case "2":
                                Console.WriteLine("You run away!");
                                return; // Exit the combat loop and return to the main game loop

                            case "3":
                                // Player uses an item
                                inventory.DisplayInventory(); // Assuming this method displays the player's inventory
                                Console.WriteLine("Choose an item to use:");
                                string itemChoice = Console.ReadLine(); // Declare and initialize itemChoice here
                                Item selectedItem = inventory.GetItemByName(itemChoice); // Assuming GetItemByName retrieves an Item object by name
                                if (selectedItem != null) // Pass the Item object to UseItem 
                                {
                                    Console.WriteLine($"You used {selectedItem.Name}!");
                                    // Remove the item from inventory after use
                                    inventory.RemoveItem(selectedItem);
                                    // Check if the item is a consumable and apply its effects
                                    if (selectedItem is Consumable consumable)
                                    {
                                        player.Health += consumable.HealAmount; // Heal the player
                                        Console.WriteLine($"You healed for {consumable.HealAmount} health.");
                                    }
                                    else if (selectedItem is Weapon weapon)
                                    {
                                        playerDamage = weapon.Damage; // Equip the weapon
                                        Console.WriteLine($"You equipped the {weapon.Name}.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid item or unable to use the item.");
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Try again.");
                                break;
                        }
                    }
                }
            }

            internal void Move()
            {
                Console.WriteLine("Which direction would you like to go?");
                string direction = Console.ReadLine();

                if (currentRoom.Neighbors.ContainsKey(direction))
                {
                    currentRoom = currentRoom.Neighbors[direction];
                    Console.WriteLine($"You move {direction} to the {currentRoom.Name}.");
                }
                else
                {
                    Console.WriteLine("You can't go that way.");
                }
            }

            internal void SearchForItems(Inventory inventory)
            {
                // Check if the current room has already been searched
                if (currentRoom.HasBeenSearched)
                {
                    Console.WriteLine("You have already searched this room. There's nothing more to find here.");
                    return;
                }

                Console.WriteLine("You search the room for items...");

                // Define a list of possible items with their drop rates
                List<(Item item, double dropRate)> possibleItems = new List<(Item, double)>
                {
                    (new Weapon("Sword", "A sharp blade, useful for combat.", 35, 75, 25), 0.3), // 30% chance
                    (new Weapon("Blunt Sword", "A rusty blade, somewhat useful for combat.", 35, 35, 15), 0.5), // 50% chance
                    (new Item("Reinforced Shield", "A sturdy shield to block attacks.", 45, 65), 0.4), // 40% chance
                    (new Item("Wood Shield", "A mediocre shield to block attacks.", 45, 30), 0.6), // 60% chance
                    (new Consumable("Small Health Potion", "Restores 25 HP.", 8, 15, 25), 0.8), // 80% chance
                    (new Consumable("Health Potion", "Restores 50 HP.", 12, 25, 50), 0.7), // 70% chance
                    (new Consumable("Big Health Potion", "Restores 75 HP.", 16, 35, 75), 0.4), // 40% chance
                    (new Item("Key", "Might unlock a door.", 5, 10), 0.2), // 20% chance
                    (new Weapon("Battle Axe", "A heavy axe that deals significant damage.", 50, 88, 40), 0.3), // 30% chance
                    (new Item("Torch", "Lights up dark areas.", 3, 5), 0.6), // 60% chance
                    (new Item("Ancient Coin", "A mysterious coin with unknown value.", 1, 1), 0.1) // 10% chance
                };

                // Randomly determine how many items will appear (between 1 and 3)
                Random random = new Random();
                int numberOfItems = random.Next(1, 4);

                // Track found items
                List<Item> foundItems = new List<Item>();

                for (int i = 0; i < numberOfItems; i++)
                {
                    foreach (var (item, dropRate) in possibleItems)
                    {
                        // Roll for each item based on its drop rate
                        if (random.NextDouble() <= dropRate)
                        {
                            foundItems.Add(item);
                            break; // Stop after finding one item in this iteration
                        }
                    }
                }

                // Allow the player to choose whether to pick up each item
                if (foundItems.Count > 0)
                {
                    foreach (var foundItem in foundItems)
                    {
                        Console.WriteLine($"You found a {foundItem.Name}: {foundItem.Description}");
                        Console.WriteLine("Do you want to pick it up? (yes/no)");

                        string choice = Console.ReadLine()?.Trim().ToLower();
                        if (choice == "yes")
                        {
                            inventory.AddItem(foundItem);
                            Console.WriteLine($"You picked up the {foundItem.Name}.");
                        }
                        else
                        {
                            Console.WriteLine($"You left the {foundItem.Name} behind.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You found nothing this time.");
                }
                // Mark the room as searched
                currentRoom.HasBeenSearched = true;
            }
        }

        }
    }

