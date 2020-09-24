using System;
using System.Collections.Generic;

namespace MapListTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random random = new Random();
            Console.Title = "";
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            List<Map> maps = new List<Map>
            {
                new Map(0, "Town", new int[20, 40])
            };

            while (keyInfo.Key != ConsoleKey.X)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        maps.Clear();
                        maps.Add(new Map(0, "Town", new int[20, 40]));
                        AddBorders(maps);
                        AddBuilding1(maps, random);
                        AddBuilding2(maps, random);
                        AddBuilding3(maps, random);
                        AddMine(maps, random);
                        DrawMap(maps);
                        break;
                }
            }
        }
        
        private static void AddBuilding1(List<Map> maps, Random random)
        {
            int[,] building1 = new int[5, 4]
            {
               { 35,35,35,35 },
               { 35,35,35,35 },
               { 35,35,35,68 },
               { 35,35,35,35 },
               { 35,35,35,35 }
            };
            bool canFit = false;
            int yDim = 0;
            int xDim = 0;
            CheckPlacement(building1, maps, random, ref canFit, ref yDim, ref xDim);
            PlaceBuilding(building1, maps, canFit, yDim, xDim);
        }
        
        private static void AddBuilding2(List<Map> maps, Random random)
        {
            int[,] building2 = new int[4, 7]
            {
                { 35,35,35,35,35,35,35 },
                { 35,35,35,35,35,35,35 },
                { 35,35,35,35,35,35,35 },
                { 35,35,35,68,35,35,35 },
            };
            bool canFit = false;
            int yDim = 0;
            int xDim = 0;
            CheckPlacement(building2, maps, random, ref canFit, ref yDim, ref xDim);
            PlaceBuilding(building2, maps, canFit, yDim, xDim);
        }

        private static void AddBuilding3(List<Map> maps, Random random)
        {
            int[,] building3 = new int[5, 5]
            {
                { 32, 32, 35, 32, 32 },
                { 32, 68, 35, 35, 32 },
                { 35, 35, 35, 35, 35 },
                { 32, 35, 35, 35, 32 },
                { 32, 32, 35, 32, 32 }
            };
            bool canFit = false;
            int yDim = 0;
            int xDim = 0;
            CheckPlacement(building3, maps, random, ref canFit, ref yDim, ref xDim);
            PlaceBuilding(building3, maps, canFit, yDim, xDim);
        }

        private static void AddBorders(List<Map> maps)
        {
            for (int i = 0; i < maps[0].Dimensions.GetLength(0); i++)
            {
                for (int j = 0; j < maps[0].Dimensions.GetLength(1); j++)
                {
                    maps[0].Dimensions[i, j] = 35;
                    if ((i > 0 && i < 39) && (j > 0 && j < 39))
                    {
                        maps[0].Dimensions[i, j] = 32;
                        if (i == 19)
                        {
                            maps[0].Dimensions[19, j] = 35;
                        }
                    }
                }
            }
        }
        private static void AddMine(List<Map> maps, Random random)
        {
            int[,] mine = new int[4, 7]
            {
                { 32, 35, 35, 35, 35, 35, 32 },
                { 35, 35, 35, 35, 35, 35, 35 },
                { 35, 35, 35, 35, 35, 35, 35 },
                { 32, 35, 35, 68, 35, 35, 32 },
            };
            bool canFit = false;
            int yDim = 0;
            int xDim = 0;
            CheckPlacement(mine, maps, random, ref canFit, ref yDim, ref xDim);
            PlaceBuilding(mine, maps, canFit, yDim, xDim);
        }

        private static void CheckPlacement(int[,] building, List<Map> maps, Random random, ref bool canFit, ref int yStart, ref int xStart)
        {
            while (!canFit)
            {
                yStart = random.Next(2, maps[0].Dimensions.GetLength(0));
                xStart = random.Next(2, maps[0].Dimensions.GetLength(1));
                Console.Title = $"Y:{yStart} X:{xStart}";
                if (yStart + building.GetLength(0) < maps[0].Dimensions.GetLength(0) - 1 && xStart + building.GetLength(1) < maps[0].Dimensions.GetLength(1) - 1)
                {
                    int collisionCount = 0;
                    for (int y = yStart - 2; y < yStart + building.GetLength(0) + 2; y++)
                    {
                        for (int x = xStart - 2; x < xStart + building.GetLength(1) + 2; x++)
                        {
                            if (maps[0].Dimensions[y, x] != 32)
                            {
                                collisionCount++;
                            }
                        }
                    }
                    if (collisionCount > 0)
                    {
                        canFit = false;
                    }
                    else
                    {
                        canFit = true;
                    }
                }
            }
        }

        private static void DrawMap(List<Map> maps)
        {
            Console.Clear();
            for (int y = 0; y < maps[0].Dimensions.GetLength(0); y++)
            {
                for (int x = 0; x < maps[0].Dimensions.GetLength(1); x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write((char)maps[0].Dimensions[y, x]);
                }
            }
        }
        
        private static void PlaceBuilding(int[,] building, List<Map> maps, bool canFit, int yDim, int xDim)
        {
            if (canFit)
            {
                for (int y = 0; y < building.GetLength(0); y++)
                {
                    for (int x = 0; x < building.GetLength(1); x++)
                    {
                        maps[0].Dimensions[yDim + y, xDim + x] = building[y, x];
                    }
                }
            }
        }
    }

    class Map
    {
        public Map(int iD, string label, int[,] dimensions)
        {
            Id = iD;
            Label = label;
            Dimensions = dimensions;
            
        }
        public int Id { get; set; }
        public string Label { get; set; }
        public int[,] Dimensions { get; set; }
    }
}
