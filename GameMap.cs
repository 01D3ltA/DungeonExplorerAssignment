using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        public static void Display(List<Room> rooms, Room currentRoom)
        {
            // Determine the grid size
            int minX = rooms.Min(r => r.X);
            int maxX = rooms.Max(r => r.X);
            int minY = rooms.Min(r => r.Y);
            int maxY = rooms.Max(r => r.Y);

            for (int y = maxY; y >= minY; y--) // Iterate rows (top to bottom)
            {
                for (int x = minX; x <= maxX; x++) // Iterate columns (left to right)
                {
                    Room room = rooms.FirstOrDefault(r => r.X == x && r.Y == y);
                    if (room != null)
                    {
                        if (room == currentRoom)
                            Console.Write("[X]"); // Current room
                        else
                            Console.Write("[ ]"); // Other room
                    }
                    else
                    {
                        Console.Write("   "); // Empty space
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
