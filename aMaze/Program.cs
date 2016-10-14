using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*The app consists of three classes, the program class that has the Main method  that initiallizes the other objects
 the Maze class, and the Actor class, also there is a class called strategy that implements as static methods the two alternative recursive path finding algorithms.*/

namespace aMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean[] strategyResult;///stores strategy results
            string fileName;
            string strategyOption;
            string runAgain = "y";

            while (runAgain == "y" || runAgain == "Y" || runAgain == "yes" || runAgain == "YES")///outer loop for program repetition
            {
                /// Greets and asks for a filename   
                Console.Clear();
                Console.WriteLine("Hello, Welcome to aMaze!!");
                Console.Write("Please type the file name of the Maze (excluding .txt):");
                fileName = Console.ReadLine();
                while (!File.Exists(fileName + ".txt"))/// it makes sure the file exists
                {
                    Console.Write("Please type an existant's file name:");
                    fileName = Console.ReadLine();
                }

                Maze maze1 = new Maze(fileName);/// initializes the maze object loading the maze from the filename passed as parameter to the constructor
                Actor actor1 = new Actor(maze1, "john");/// initializes the actor object
                Console.Clear();
                Console.Write("Please Choose a strategy \r\nType 1 for simple recursive aglorithm\r\nType 2 for an advanced recursive (finds the shortest path) but Slow \r\nYour Option (1 or 2):");
                strategyOption = Console.ReadLine();
                while (strategyOption != "1" && strategyOption != "2")/// it makes sure the file exists
                {
                    Console.Write("Please type a correct strategy number\r\nType 1 for simple recursive aglorithm\r\nType 2 for an advanced recursive (finds the shortest path) but Slow \r\nYour Option (1 or 2):");
                    strategyOption = Console.ReadLine();


                }
                strategyResult = Strategy.strategy(strategyOption, maze1, actor1, maze1.start[0], maze1.start[1], 0);///calls the static method strategy of the class Strategy that returns the result of the chosen algorithm

                maze1.draw();///draws the maze
                actor1.draw();///draws on top of the maze, the Actors path to Exit
                actor1.print(strategyResult[1]);///prints the coordinates of the Actors Path (if the algorithm found that exit path exists)

                Console.Write("\r\n\r\nDo you want to run again?? (press (y)es and hit enter):");
                runAgain = Console.ReadLine();
            }
        }

    }
}
