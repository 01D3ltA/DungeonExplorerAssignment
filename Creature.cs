using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Abstract base class
    public abstract class Creature
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public Creature(string name, int health, int attackpower)
        {
            Name = name;
            Health = health;
            AttackPower = attackpower;
        }

        public abstract void Attack(Creature target);
    }

    // Derived class for Player
    public class Player : Creature
    {
        public int Level { get; set; }
        public int CurrentExp { get; set; } // Current experience points of the player

        public Player(string name, int health, int attackpower, int level, int currentExp) : base(name, health, attackpower)
        {
            Level = level;
            CurrentExp = currentExp;
        }

        public override void Attack(Creature target)
        {
            // Player-specific attack logic
            Console.WriteLine($"{Name} attacks {target.Name} for {AttackPower} damage");
        }
        public void GainExperience(int exp)
        {
            CurrentExp += exp;
            Console.WriteLine($"{Name} gained {exp} experience points. Total: {CurrentExp}");
            if (CurrentExp >= 100) // Example level-up condition
            {
                LevelUp();
                CurrentExp -= 100; // Reset experience after leveling up
            }
        }
        public void LevelUp()
        {
            Level++;
            Health += 10; // Increase health on level up
            AttackPower += 2; // Increase attack power on level up
            Console.WriteLine($"{Name} leveled up to level {Level}! Health: {Health}, Attack Power: {AttackPower}");
        }
        public void UseItem(Item item)
        {
            
            item.Use();
        }
        public int GetAttackPower()
        {
            return AttackPower;
        }
        public int GetHealth()
        {
            return Health;
        }
    }

    // Derived class for Monster
    public class Monster : Creature
    {
        
        public int ExpDrop { get; set; } // Experience points dropped by the monster
        public Monster(string name, int health, int attackpower, int expDrop) : base(name, health, attackpower)
        {
            ExpDrop = expDrop;
        }


        public override void Attack(Creature target)
        {
            // Monster-specific attack logic
            Console.WriteLine($"{Name} attacks {target.Name} for {AttackPower} damage");
            target.Health -= AttackPower; // Reduce target's health
        }
        
    }
}
