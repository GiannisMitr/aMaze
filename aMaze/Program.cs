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
            while (!File.Exists(fileName + ".txt"))/// it makes sure the file exists
            {
                Console.WriteLine("Please type an existant's file name:");
                fileName = Console.ReadLine();
            }

            Maze maze1 = new Maze(fileName);/// initializes the maze object loading the maze
            Actor actor1 = new Actor(maze1, "john");/// initializes the actor object
            Boolean exit = strategy(maze1, actor1, maze1.start[0], maze1.start[1], 0);/// runs the recursive algorithm
            maze1.draw();///draws the maze
            actor1.draw();///draws on top of the maze, the Actors path to Exit
            actor1.print(exit);///prints the coordinates of the Actors Path (if the algorithm found one exit=true)
        }

        /*The strategy method (can become an object and a collection of more strategies in the future)
         takes as parameters the Maze object the Actor object, and the present position, and implements 
         a recursive algorithm that traverses the Maze, updates Actors map of the Maze, and when reaches the Exit,
         it Bubbles up to the surface...to the first caller instance of the method, marking the Exit path.
         
         The Algorithm was heavily inspired from "http://interactivepython.org/runestone/static/pythonds/Recursion/ExploringaMaze.html" 
         
         The Algorithm's efficiency depends largely, on the order that the various branches (up,down.left.right)
         are executed and prioritized, The direction that executes first marks first it's path as visited, and blocks the 
         execution of other, possible, sortest branches*/

        static bool strategy(Maze maze, Actor actor, int yPosition, int xPosition, int count)

        {

            count += 1;
              
            if (maze.ask(yPosition, xPosition) == "X")///ask maze if the current square is blocked 
            {
                return false;///then if it is blocked return with false to the caller method
            }
            else if (maze.ask(yPosition, xPosition) == "OOM")///Out Of Maze square
            {

                return false;
            }
            else if (maze.ask(yPosition, xPosition) == "EXIT")///if it is the exit!!!!!
            {
                
                if (actor.map2[yPosition, xPosition] <= count && actor.map2[yPosition, xPosition] !=0)
                   {
                     return false;
                   }
                else
                {                   
                    actor.exitPath.Clear();
                    /// actor.writeMap(0,0,"CLEARPATH");
                    actor.map2[yPosition, xPosition] = count;
                    actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!
                    actor.writeMap(yPosition, xPosition, "EXIT");///  update actors map
                    return true;
                }///  bubble up a true boolean to the caller and mark the exit path
            }
            else if (actor.readMap(yPosition, xPosition) == "DEADEND")
            {

                return false;
            }
            else if (actor.map2[yPosition, xPosition] != 0) ///if was visited before
            {
                if (count > actor.map2[yPosition, xPosition])
                    return false;
            }
            else
            { actor.map2[yPosition, xPosition] = count; }///mark the square as visited, 
                                                         ///cause if not no memory of past actions, and infinity!

            ///Check recursively using the same method for exit in paths that start from the square 
            ///that lies down or right or up or left of current position, and return true if an Exit path is found

            bool Exit = strategy(maze, actor, yPosition - 1, xPosition, count)
                          | strategy(maze, actor, yPosition, xPosition + 1, count)
                          | strategy(maze, actor, yPosition + 1, xPosition, count)
                          | strategy(maze, actor, yPosition, xPosition - 1, count);

            if (Exit)
            {
                actor.writeMap(yPosition, xPosition, "EXITPATH");///if the path started from here, leads to an exit then mark the exit path
                actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!

            }
            else
            {
                actor.writeMap(yPosition, xPosition, "DEADEND"); ///if not, mark the square as dead end path
            }

            return Exit;/// return to the caller function if this is an EXIT path.
        }

    }
}
