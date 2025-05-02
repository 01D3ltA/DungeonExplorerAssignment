using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DungeonExplorer
    {
        internal class Inventory
        {
            private const int MaxWeight = 100; // Maximum weight limit
            private List<Item> items; // List to store items
            private int currentWeight; // Tracks the current total weight

            public Inventory()
            {
                items = new List<Item>();
                currentWeight = 0;
                
            }

            public bool AddItem(Item item)
            {
                // Check if adding the item exceeds the weight limit
                if (currentWeight + item.Weight > MaxWeight)
                {
                    Console.WriteLine($"You cannot pick up the {item.Name}. It would exceed your weight limit of {MaxWeight}.");
                    return false;
                }

                // Add the item and update the current weight
                items.Add(item);
                currentWeight += item.Weight;
                Console.WriteLine($"You added the {item.Name} to your inventory. Current weight: {currentWeight}/{MaxWeight}.");
                return true;
            }

            public void RemoveItem(Item item)
            {
                if (items.Remove(item))
                {
                    currentWeight -= item.Weight;
                    Console.WriteLine($"You removed the {item.Name} from your inventory. Current weight: {currentWeight}/{MaxWeight}.");
                }
                else
                {
                    Console.WriteLine($"The {item.Name} is not in your inventory.");
                }
            }

            public void DisplayInventory()
            {
                Console.WriteLine("Your inventory contains:");
                foreach (var item in items)
                {
                    Console.WriteLine($"- {item.Name} (Weight: {item.Weight})");
                }
                Console.WriteLine($"Total weight: {currentWeight}/{MaxWeight}");
            }
            public void UseItem(string itemName)
            {
                var item = items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    item.Use();
                    RemoveItem(item); // Remove the item after use
                }
                else
                {
                    Console.WriteLine($"You do not have a {itemName} in your inventory.");
                }
            }
            public Item GetItemByName(string itemName)
            {
                return items.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            }

    }



}

