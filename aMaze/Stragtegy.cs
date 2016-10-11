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
        public Boolean[] Result;///stores the results of the chosen strategy

        public Strategy(string strategyNumber, Maze maze, Actor actor, int yPosition, int xPosition, int count) ///Strategy Constructor, initiallizes an object 
                                            ///with a method strategy selected from strategyNumber
        {
            if (strategyNumber=="1")
            { Result=strategy1(maze, actor, yPosition, xPosition, count); }
            else if(strategyNumber == "2")
            { Result=strategy1(maze, actor, yPosition, xPosition, count); }
        }

            private bool[] strategy1(Maze maze, Actor actor, int yPosition, int xPosition, int count)
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

            bool[] resultUp = strategy1(maze, actor, yPosition - 1, xPosition, count);
            bool[] resultRight = strategy1(maze, actor, yPosition, xPosition + 1, count);
            bool[] resultDown = strategy1(maze, actor, yPosition + 1, xPosition, count);
            bool[] resultLeft = strategy1(maze, actor, yPosition, xPosition - 1, count);

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
    }
}
