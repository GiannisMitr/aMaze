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
    /*The Maze classs constructor, loads the maze from the file, finds it's starting point and its dimensions, 
     also has a method "draw" that prints the maze, and a method "ask" to give data about what a square is */
    public class Maze
    {
        public int length;
        public int width;
        public int[] start = new int[2];
        private string[] maze;

        public Maze(string fileName) ///constructor 
                                     ///inputs existing txt file and converts it to an array of strings
                                     ///and seaches for starting point
                                     ///and initiallizes the map of the actor
        {
            String input = File.ReadAllText(fileName + ".txt");
            maze = Regex.Split(input, "\r\n");
            length = maze.Length; ///computes length
            width = maze[0].Length; ///computes width


            ///searches for start point in the maze 
            ///and saves it in the objects property during construction
            for (int i = 0; i < length; i++)
            {
                for (int k = 0; k < width; k++)
                {

                    if (String.Equals(maze[i][k].ToString(), "s", StringComparison.OrdinalIgnoreCase))
                    {
                        start[0] = i;
                        start[1] = k;
                    }
                }
            }

        }

        public void draw()///prints the maze
        {
            Console.Clear();

            for (int i = 0; i < length; i++)

            {
                Console.WriteLine(maze[i]);

            }
        }

        public string ask(int y, int x) ///ask the maze object info about a position
        {
            if ((y >= 0 && y < length) && (x >= 0 && x < width))   ///checks if the position is inside the maze
            {
                if (String.Equals(maze[y][x].ToString(), "x", StringComparison.OrdinalIgnoreCase))///checks if the position is blocked
                { return "X"; }
                else if (String.Equals(maze[y][x].ToString(), "g", StringComparison.OrdinalIgnoreCase))
                { return "EXIT"; }
                else { return "OPEN"; }
            }
            else
            { return "OOM"; } ///Out Of Maze exception!}

        }
    }
}
