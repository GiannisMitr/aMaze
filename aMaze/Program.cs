using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*The app consists of three classes, the program class that has the Main method  that initiallizes the other objects
 the Maze class, and the Actor class, also there is a method called strategy that implements a recursive path finding algorithm.*/

namespace aMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Greets and asks for a filename         
            Console.WriteLine("Hello, Welcome to aMaze!!");
            Console.WriteLine("Please type the file name of the Maze (excluding .txt):");
            string fileName = Console.ReadLine();
            string strategyOption;
            while (!File.Exists(fileName + ".txt"))/// it makes sure the file exists
            {
                Console.WriteLine("Please type an existant's file name:");
                fileName = Console.ReadLine();
            }

            Maze maze1 = new Maze(fileName);/// initializes the maze object loading the maze
            Actor actor1 = new Actor(maze1, "john");/// initializes the actor object
            Console.WriteLine("Please Choose a strategy,\r\n Type 1 for simple recursive aglorithm\r\n or type 2 for an advanced recursive (finds the shortest path) but Slow:");
            strategyOption = Console.ReadLine();
            while (strategyOption!="1"&& strategyOption != "2")/// it makes sure the file exists
            {
                Console.WriteLine("Please type a correct strategy number/r/n Type 1 for simple recursive aglorithm\r\n or type 2 for an advanced recursive (finds the shortest path) but Slow: ");
                strategyOption = Console.ReadLine();
                

            }
            Strategy strategy1 = new Strategy(strategyOption, maze1, actor1, maze1.start[0], maze1.start[1], 0);
            ///Boolean []exit = strategy(maze1, actor1, maze1.start[0], maze1.start[1], 0);/// runs the recursive algorithm
           maze1.draw();///draws the maze
           actor1.draw();///draws on top of the maze, the Actors path to Exit
            actor1.print(strategy1.Result[1]);///prints the coordinates of the Actors Path (if the algorithm found one exit=true)
        }

        /*The strategy method (can become an object and a collection of more strategies in the future)
         takes as parameters the Maze object the Actor object, and the present position, and implements 
         a recursive algorithm that traverses the Maze, updates Actors map of the Maze, and when reaches the Exit,
         it Bubbles up to the surface...to the first caller instance of the method, marking the Exit path.
         
         The Algorithm was heavily inspired from "http://interactivepython.org/runestone/static/pythonds/Recursion/ExploringaMaze.html" 
         
         The Algorithm's efficiency depends largely, on the order that the various branches (up,down.left.right)
         are executed and prioritized, The direction that executes first marks first it's path as visited, and blocks the 
         execution of other, possible, sortest branches*/   

    }
}
