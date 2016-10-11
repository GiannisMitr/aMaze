using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aMaze
{
    /*The Actor class constructor, names the actor objects and gets data from the Maze object (dimensions and starting point)
     to create a map matrix, that gets updated with the actor's knowledge of the maze, as the algorithm progresses.
     Also it has a stack, that keeps the coordinates of the exit path with LIFO because of the recursive algorithm.
     And has a "print" method that prints the coordinates of the exit path, and a "draw" method that draws them relative to 
     the maze drawing*/
    public class Actor
    {
        public Stack exitPath = new Stack();
        public string name;
        public int[] startPoint;
        public string[,] map;///creates a map, of what the actor discovers, so it knows the visited squares
        public int[,] map2;

        public Actor(Maze maze, string names) ///Actor constructor, it sets the maps starting position and size, 
                                              ///according to the maze object passed
        {
            map2 = new int[maze.length, maze.width];
            name = names;///gives name to an Actor object
            map = new string[maze.length, maze.width];///initiallizes the notes with correct size
            startPoint = maze.start;///reads the startpoint from the maze object
            map[startPoint[0], startPoint[1]] = "S";///sets the start point to the map, 
        }

        public void print(Boolean exit)///prints the exit path on the screen, (as coordinates of the squares)
        {
            if (exit)
            {
                Console.WriteLine("\r\n");
                foreach (string c in exitPath)
                {

                    Console.Write(" {" + c + "} .");
                }
            }
            else { Console.WriteLine("Exit is Blocked!...Maze is not Solvable!...\r\n Please Try Again with Another Maze"); }

            
        }

        public void draw()///prints the exit path on the screen, (as drawing on the maze)
        {

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int k = 0; k < map.GetLength(1); k++)
                {
                    Console.SetCursorPosition(k, i);
                    if (String.Equals(map[i, k] ?? "".ToString(), "+", StringComparison.OrdinalIgnoreCase))
                    {
                        if (i == startPoint[0] && k == startPoint[1])///we want to maintain the S symbol on the starting point of the map
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;///and paint it blue
                            Console.Write("S");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Green;///all the exit path is painted green
                            Console.Write(map[i, k]);
                        }
                    }
                    else if (map[i, k] == "G")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;///and paint red the finish point
                        Console.Write("G");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public string readMap(int y, int x) ///look the actor's notes (map) info about a position
        {

            if (String.Equals(map[y, x] ?? "".ToString(), "$", StringComparison.OrdinalIgnoreCase))///checks if the position is blocked
            { return "VISITED"; }
            else { return ""; }

        }


        public void writeMap(int y, int x, string info) ///UPDATES the actor's notes (map) info about a position
        {
            if (info == "VISITED")
                map[y, x] = "$";
            else if (info == "EXITPATH")
                map[y, x] = "+";
            else if (info == "EXIT")
                map[y, x] = "G";
            else if (info == "DEADEND")
                map[y, x] = "D";
            else if (info == "CLEARPATH")
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int k = 0; k < map.GetLength(1); k++)
                    {
                        if (map[i, k] == "+")
                        {
                            map[i, k] = "";
                        }
                    }
                }
            }
            
        }

    }
}
