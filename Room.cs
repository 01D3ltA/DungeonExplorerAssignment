using System;
using System.Collections.Generic;
using System.Threading;

namespace DungeonExplorer
{
    public class Room
    {
        /// Properties of the Room class 
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, Room> Neighbors { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Monster> Monsters { get; set; }
        public bool HasBeenSearched { get; set; } // New property to track if the room has been searched

        public Room(string name, string description, int x, int y)
        {
            // Initialize properties 
            Name = name;
            Description = description;
            X = x;
            Y = y;
            Neighbors = new Dictionary<string, Room>();
            Monsters = new List<Monster>();
            HasBeenSearched = false; // Initialize as not searched
        }

        public void AddNeighbor(string direction, Room neighbor)
        {
            Neighbors[direction] = neighbor; // Add a neighbor in the specified direction
        }

        public void AddMonster(Monster monster)
        {
            Monsters.Add(monster); // Add a monster to the room
        }
    }

}