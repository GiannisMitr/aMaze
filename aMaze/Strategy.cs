using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMaze
{
    static class Strategy
    {
       public static int numberOfStrategies=2;/// the numbers of strategy options
         

        public static Boolean[] strategy(string strategyNumber, Maze maze, Actor actor, int yPosition, int xPosition, int count) ///strategy public static interface, takes the arguments for the selected strategy 
            ///and calls the apropriate private mathod of the given strategy 
        {
            Boolean[] Result = new bool[2] { false, false };///stores the results of the chosen strategy

            if (strategyNumber=="1")///runs the given method with correct arguments and returns a boolean array with its results
            { Result[1]=strategy1(maze, actor, yPosition, xPosition); }
            else if(strategyNumber == "2")
            { Result=strategy2(maze, actor, yPosition, xPosition, count); }
            return Result;
        }

            

        /*The strategy method takes as parameters the Maze object the Actor object, and the present position, and implements 
         a recursive algorithm that traverses the Maze, updates Actors map of the Maze, and when reaches the Exit,
         it Bubbles up to the surface...to the first caller instance of the method, marking the Exit path.
         
         The Algorithm was heavily inspired from "http://interactivepython.org/runestone/static/pythonds/Recursion/ExploringaMaze.html" 
         
         The Algorithm's efficiency depends largely, on the order that the various branches (up,down.left.right)
         are executed and prioritized, The direction that executes first marks first it's path as visited, and blocks the 
         execution of other, possible, sortest branches*/

        private static bool strategy1(Maze maze, Actor actor, int yPosition, int xPosition)

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

        /*This strategy method is similar to the above with the addition of a counter that marks the depth of the recursion path, in every visited square, so it gives 
         a chance to revisit squares if the new path is shorter in depth, and also when it calls itself in  neighbouring squares it uses a non shotcircuited OR " | " that 
         makes sure that all possible branches execute. finally, when a branch reaches the exit point, if it is shortest, than another branch that reached it first , then it clears the
         exit path stack (kept in actor object), and it marks the new shortest exit path as it bubbles up as true
         
         The aforementioned "improvements" were mine contributions, the algorithm finds the shortest path, but the search is tremendously slow in
         bigger mazes,"don't try maze 4!!!"*/

        private static bool[] strategy2(Maze maze, Actor actor, int yPosition, int xPosition, int count)
        {
            count += 1;///counter to know the recursive depth

            if (maze.ask(yPosition, xPosition) == "X")///ask maze if the current square is blocked 
            {

                return new Boolean[] { false, false };///then if it is blocked return with false to the caller method
            }
            else if (maze.ask(yPosition, xPosition) == "OOM")///Out Of Maze square
            {

                return new Boolean[] { false, false };///return no exit path mark
            }
            else if (actor.readMap(yPosition, xPosition) == "DEADEND")///DeadEnd
            {

                return new Boolean[] { false, false };
            }

            else if (maze.ask(yPosition, xPosition) == "EXIT")///if it is the exit!!!!!
            {

                if (actor.mapWithCounts[yPosition, xPosition] <= count && actor.mapWithCounts[yPosition, xPosition] != 0)///if there is another exitpath but it is shortert that current branch depth (count)
                
                {
                    return new Boolean[] { true, false };///then return [true false], the first boolean that we got an exit path, but the second one states that the current is not the shortest.
                }
                else///we found the exit square and in fewer steps than the previous path
                {
                    actor.exitPath.Clear();///we clear the exit path stack "the previous shortest path"
                    actor.writeMap(0, 0, "CLEARPATH");///we update the actors map "notes", so to clear from there too, the exit path
                    actor.mapWithCounts[yPosition, xPosition] = count;///updates the depth of the current(shortest) path to the exit square at the actors note
                    actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!
                    actor.writeMap(yPosition, xPosition, "EXIT");///  update actors map
                    return new Boolean[] { true, true };///returns 2 booleans first true that we have an exit, second true that is currently the shoertest one 
                }///  bubble up a true boolean to the caller and mark the exit path
            }

            else if (actor.mapWithCounts[yPosition, xPosition] != 0) ///if was visited before
            {
                if (count > actor.mapWithCounts[yPosition, xPosition])
                    return new Boolean[] { false, false };
            }
            else
            { actor.mapWithCounts[yPosition, xPosition] = count; }///mark the square as visited, 
                                                         ///cause if not no memory of past actions, and infinity!

            ///Check recursively using the same method for exit in paths that start from the square 
            ///that lies down or right or up or left of current position, and return true if an Exit path is found

            bool[] resultUp = strategy2(maze, actor, yPosition - 1, xPosition, count);
            bool[] resultRight = strategy2(maze, actor, yPosition, xPosition + 1, count);
            bool[] resultDown = strategy2(maze, actor, yPosition + 1, xPosition, count);
            bool[] resultLeft = strategy2(maze, actor, yPosition, xPosition - 1, count);

            if (resultUp[1] || resultRight[1] || resultDown[1] || resultLeft[1])///if a shortest exit path was found to the branches started from here
            {
                actor.writeMap(yPosition, xPosition, "EXITPATH");/// mark the current square as part of the exit path
                actor.exitPath.Push((yPosition + 1).ToString() + "," + (xPosition + 1).ToString());///push it to the Exit Path stack!

                return new Boolean[] { true, true };///return to the caller function with a sign of a shortest exit path found
            }
            else if (resultUp[0] || resultRight[0] || resultDown[0] || resultLeft[0])///if it there is an exit path in one direction, but it is'not the shortest
            {
                return new Boolean[] { true, false };

            }
            else {///if there is no exit path all the way around(the square that we came from gives false because of depth counter)
                
                actor.writeMap(yPosition, xPosition, "DEADEND");///then mark this square as dead end
                return new Boolean[] { false, false };///and return no exit path mark
            }



        }
    }
}
