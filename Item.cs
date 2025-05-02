using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Base Item class
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }

        public Item(string name, string description, int weight, int value)
        {
            Name = name;
            Description = description;
            Weight = weight;
            Value = value;
        }

        public virtual void Use()
        {
            Console.WriteLine($"You use the {Name}.");
        }
    }
    // Weapon class derived from Item
    public class Weapon : Item
    {
        public int Damage { get; set; }

        public Weapon(string name, string description, int weight, int value, int damage)
            : base(name, description, weight, value)
        {
            Damage = damage;
        }

        public override void Use()
        {
            Console.WriteLine($"You equip the {Name}, ready to deal {Damage} damage.");
        }
    }
    // Consumable class derived from Item
    public class Consumable : Item
    {
        public int HealAmount { get; set; }

        public Consumable(string name, string description, int weight, int value, int healAmount)
            : base(name, description, weight, value)
        {
            HealAmount = healAmount;
        }

        public override void Use()
        {
            Console.WriteLine($"You consume the {Name} and heal for {HealAmount} health.");
        }
    }

}
