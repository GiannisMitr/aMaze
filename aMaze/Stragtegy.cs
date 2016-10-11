using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMaze
{
    class Strategy
    {
       public int numberOfStrategies=2;/// the numbers of strategy options
        public Boolean[] Result=new bool [2]{ false,false};///stores the results of the chosen strategy

        public Strategy(string strategyNumber, Maze maze, Actor actor, int yPosition, int xPosition, int count) ///Strategy Constructor, initiallizes an object 
                                            ///with a method strategy selected from strategyNumber
        {
            if (strategyNumber=="1")
            { Result[1]=strategy1(maze, actor, yPosition, xPosition); }
            else if(strategyNumber == "2")
            { Result=strategy2(maze, actor, yPosition, xPosition, count); }
        }

            private bool[] strategy2(Maze maze, Actor actor, int yPosition, int xPosition, int count)
          {
            count += 1;

            if (maze.ask(yPosition, xPosition) == "X")///ask maze if the current square is blocked 
            {

                return new Boolean[] { false, false };///then if it is blocked return with false to the caller method
            }
            else if (maze.ask(yPosition, xPosition) == "OOM")///Out Of Maze square
            {

                return new Boolean[] { false, false };
            }
            else if (actor.readMap(yPosition, xPosition) == "DEADEND")///DeadEnd
            {

                return new Boolean[] { false, false };
            }

            else if (maze.ask(yPosition, xPosition) == "EXIT")///if it is the exit!!!!!
            {

                if (actor.map2[yPosition, xPosition] <= count && actor.map2[yPosition, xPosition] != 0)
                {
                    return new Boolean[] { true, false };
                }
                else
                {
                    actor.exitPath.Clear();
                    actor.writeMap(0, 0, "CLEARPATH");
                    actor.map2[yPosition, xPosition] = count;
                    actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!
                    actor.writeMap(yPosition, xPosition, "EXIT");///  update actors map
                    return new Boolean[] { true, true };
                }///  bubble up a true boolean to the caller and mark the exit path
            }

            else if (actor.map2[yPosition, xPosition] != 0) ///if was visited before
            {
                if (count > actor.map2[yPosition, xPosition])
                    return new Boolean[] { false, false };
            }
            else
            { actor.map2[yPosition, xPosition] = count; }///mark the square as visited, 
                                                         ///cause if not no memory of past actions, and infinity!

            ///Check recursively using the same method for exit in paths that start from the square 
            ///that lies down or right or up or left of current position, and return true if an Exit path is found

            bool[] resultUp = strategy2(maze, actor, yPosition - 1, xPosition, count);
            bool[] resultRight = strategy2(maze, actor, yPosition, xPosition + 1, count);
            bool[] resultDown = strategy2(maze, actor, yPosition + 1, xPosition, count);
            bool[] resultLeft = strategy2(maze, actor, yPosition, xPosition - 1, count);

            if (resultUp[1] || resultRight[1] || resultDown[1] || resultLeft[1])
            {
                actor.writeMap(yPosition, xPosition, "EXITPATH");///if the path started from here, leads to an exit then mark the exit path
                actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!

                return new Boolean[] { true, true };
            }
            else if (resultUp[0] && resultRight[0] && resultDown[0] && resultLeft[0] && resultUp[1] && resultRight[1] && resultDown[1] && resultLeft[1])
            {
                actor.writeMap(yPosition, xPosition, "DEADEND");
                return new Boolean[] { false, false };
            }
            else { return new Boolean[] { true, false }; }



        }
        /*The strategy method (can become and object and a collection of more strategies in the future)
         takes as parameters the Maze object the Actor object, and the present position, and implements 
         a recursive algorithm that traverses the Maze, updates Actors map of the Maze, and when reaches the Exit,
         it Bubbles up to the surface...to the first caller instance of the method, marking the Exit path.
         
         The Algorithm was heavily inspired from "http://interactivepython.org/runestone/static/pythonds/Recursion/ExploringaMaze.html" 
         
         The Algorithm's efficiency depends largely, on the order that the various branches (up,down.left.right)
         are executed and prioritized, The direction that executes first marks first it's path as visited, and blocks the 
         execution of other, possible, sortest branches*/

        static bool strategy1(Maze maze, Actor actor, int yPosition, int xPosition)

        {
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
                actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!
                actor.writeMap(yPosition, xPosition, "EXIT");///  update actors map
                return true;  ///  bubble up a true boolean to the caller and mark the exit path
            }
            else if (actor.readMap(yPosition, xPosition) == "VISITED")///if was visited before
            {
                return false;
            }
            else
            { actor.writeMap(yPosition, xPosition, "VISITED"); }///mark the square as visited, 
                                                                ///cause if not no memory of past actions, and infinity!

            ///Check recursively using the same method for exit in paths that start from the square 
            ///that lies down or right or up or left of current position, and return true if an Exit path is found

            bool Exit = strategy1(maze, actor, yPosition + 1, xPosition)
                          || strategy1(maze, actor, yPosition, xPosition + 1)
                          || strategy1(maze, actor, yPosition - 1, xPosition)
                          || strategy1(maze, actor, yPosition, xPosition - 1);

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
