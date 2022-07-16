
using System;
using static System.Console;
using System.Threading;
using NLog;
using Microsoft.Extensions.Logging;

namespace Conway_s_Game_of_Life
{
    class Program
    {
        const int Dead = 0;             // Using a grid of 0's and 1's will help us count
        const int Alive = 1;            //   count neighbors efficiently in the Life program.
        static int GridSizeX = 25;
        static int GridSizeY = 25;
        static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("=== Starting Program ===");
            int gridCount = 0;
        int[,] grid = new int[GridSizeX, GridSizeY];
        bool inSilentMode = false;
        bool useRPentomino = false;
        int fillPercentage = 20;
        int finalGeneration = 50;
            // Parse the commend-Line argumnets and change our config variables as approprite
            if (args.Length >= 1)
            {
                if (args[0].ToLower() == "s" || args[0].ToLower() == "silent")
                {
                    inSilentMode = true;
                }
                else
                {
                    if (args[0].ToLower() == "i" || args[0].ToLower() == "Interactive")
                    {
                        inSilentMode = false;
                    }
                    else
{
    WriteLine("[Error] Invilide number. Please enter the valid number between 0 to 100");
    return;
}
                }
                if (args.Length >= 2)
{
    if (args[1].ToLower() == "r")
    {
        useRPentomino = true;
    }
    else if (int.TryParse(args[1], out int percent))
    {
        fillPercentage = percent;
        if (fillPercentage > 100 || fillPercentage < 0)
        {
            WriteLine("Error!!] Please enter the valid number between 0 to 100");
            return;
        }
    }
    else
    {
        WriteLine("[Error!!] Please enter the valid number between 0 to 100 ");
        return;
    }
}
else
{
    WriteLine("[Error!!] Please enter the valid number between 0 to 100 ");
    return;
}
if (args.Length == 3)
{
    if (int.TryParse(args[2], out int percent))
    {
        finalGeneration = percent;
    }
    else
    {
        WriteLine("[Error] Please enter the valid number between o to 100");
        return;
    }
}
else
{
    WriteLine("[Error] Please enter the valid  number between o to 100");
    return;
}
            }
            else
{
    if (args.Length == 0)
    {
        useRPentomino = true;
        inSilentMode = false;
    }
}
if (inSilentMode && finalGeneration < 0)
{
    WriteLine("[Error]: Silent mode cannot run forever.  Please specify a positive number for the final generation.");
    System.Environment.Exit(0);
            }
//Instillized the grid Randomally
Initialize(grid);
RPentomino(grid);
if (useRPentomino)
{
    grid = RPentomino(grid);
}
bool done = false;
while (!done)
{
    logger.Info("=== Starting Program ===");
    if (!inSilentMode)
    {
        // Display the Current grid (and log its statistics)
        WriteLine($"Generation #{gridCount}");
        PrintGrid(grid);
    }
    logger.Debug($"Generation #{gridCount}  aliveCount: {CountLiveCells(grid)}");
    if (gridCount == finalGeneration)
    {
        done = true;
        continue;
    }
    grid = CalculateNextGeneration(grid);
    if (!inSilentMode)
    {
        // Check to see if the user pressed a key
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            logger.Debug($"{key} pressed...");
            if (key == ConsoleKey.Q)
                done = true;
            if (key == ConsoleKey.F)
                FillGridRandomly(grid, fillPercentage);
            if (key == ConsoleKey.R)
                grid = RPentomino(grid);
            RPentomino(grid);
        }
        Thread.Sleep(500);
    }
    gridCount++;            // Increment at bottom of loop so that first grid displayed is Grid #0    
}
if (inSilentMode)
{
    WriteLine("Conway's Game of Life");
    WriteLine("==============================");
    WriteLine("SiletMode");
    WriteLine($"Running for #{gridCount}");
    WriteLine($"Generation #{gridCount}");
    PrintGrid(grid);
}
logger.Info("=== Ending Program ===");
        }
        static int[,] CalculateNextGeneration(int[,] currentGrid)
{
    int[,] nextGrid = new int[GridSizeX, GridSizeY];
    // loopthroug current grid
    for (int x = 1; x < GridSizeX - 1; x++)
    {
        for (int y = 1; y < GridSizeY - 1; y++)
        {
            int countNighbour = currentGrid[x - 1, y - 1] + currentGrid[x, y - 1] + currentGrid[x + 1, y - 1] +
                                currentGrid[x - 1, y] + +currentGrid[x + 1, y] +
                                currentGrid[x - 1, y + 1] + currentGrid[x, y + 1] + currentGrid[x + 1, y + 1];
            // Determine if grid[x,y] will lives or die in next grid;
            if (currentGrid[x, y] == Dead)
            {
                if (countNighbour == 3)
                    nextGrid[x, y] = Alive;
                else
                    nextGrid[x, y] = Dead;
            }
            else
            {
                if (countNighbour == 2 || countNighbour == 3)
                    nextGrid[x, y] = Alive;
                else
                    nextGrid[x, y] = Dead;
            }
        }
    }
    return nextGrid;
}
static int[,] RPentomino(int[,] currentGrid)
{
    int[,] RPattren = new int[GridSizeX, GridSizeY];
    int x = GridSizeX / 2;
    int y = GridSizeY / 2;
    RPattren[x, y] = Alive;
    RPattren[x, y + 1] = Alive;
    RPattren[x - 1, y] = Alive;
    RPattren[x + 1, y - 1] = Alive;
    RPattren[x, y - 1] = Alive;
    return RPattren;
}
static void Initialize(int[,] grid)
{
    Random ran = new Random();
    for (int i = 0; i < GridSizeX; i++)
    {
        for (int j = 0; j < GridSizeY; j++)
        {
            int rng = ran.Next(0, 100);
            if (rng < 20)
                grid[i, j] = 1;
            else
                grid[i, j] = 0;
        }
    }
}
static void FillGridRandomly(int[,] grid, int fillPercentage)
{
    for (int x = 0; x < GridSizeX; x++)
    {
        for (int y = 0; y < GridSizeY; y++)
        {
            if (RandomBool(fillPercentage) == true)
                grid[x, y] = Alive;
            else
                grid[x, y] = Dead;
        }
    }
}
static bool RandomBool(int percent)
{
    Random rng = new Random();
    return (rng.Next() % 100 < percent);
}
static void PrintGrid(int[,] grid)
{
    WriteLine($"+{Dashes(GridSizeX * 3)}+");
    for (int y = 0; y < GridSizeY; y++)
    {
        string s = "|";
        for (int x = 0; x < GridSizeX; x++)
        {
            string cell = (grid[x, y] == Alive) ? " * " : "   ";
            s += cell;
        }
        s += "|";
        WriteLine(s);
    }
    WriteLine($"+{Dashes(GridSizeX * 3)}+");
}
static string Dashes(int number)
{
    return new string('-', number);
}
static int CountLiveCells(int[,] grid)
{
    int count = 0;
    for (int x = 0; x < GridSizeX; x++)
        for (int y = 0; y < GridSizeY; y++)
            if (grid[x, y] == Alive)
                count++;
    return count;
}
    }
}
