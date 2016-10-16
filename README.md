# aMaze
###Maze Solving Console App (C# - .NET)

This program was developed as part of the coding camp ([AFDEmp]) courses.

Loads a maze from a file, computes and prints the exit path.

Supports two different recursive maze solving algorithms.

There is **NO** gun-powder invention *(Greek joke!)*.

The basic recursive maze-solving algorithm was heavily inspired from [here].

Additionally, an alternate modified edition of the first algorithm was developed. It computes always the shortest path (but is computationally inefficient).

###USAGE

Run the executable **aMaze.exe** and make sure you provide on the same folder a "*.txt*" file with the maze in the following format

``____G__X``

``___XXX__``

``X______X``

``__XXXX__``

``___X____``

``__S__X__``

Start and Finish squares are denoted by S and G respectively, and blocked squares with X.

Free spaces can be **_** ,  **spaces** or other symbols too.

Make sure you hit **enter** between lines. 

Once Run, the program :
- depicts the maze on the command line 
- computes and highlights by color the Exit Path
- prints an array with the coordinates of the path's squares.

##ENJOY!!


###LICENSE
GNU GENERAL PUBLIC LICENSE (Ver. 3)


[AFDEmp]: <http://www.afdemp.org/>
[here]: <http://interactivepython.org/runestone/static/pythonds/Recursion/ExploringaMaze.html>