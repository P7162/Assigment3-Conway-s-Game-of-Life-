
Game-of-Life-


The game should use a 25x25 grid of cells with "dead" borders - i.e., live cells should never be allowed on the edges of the grid. 

The program should also log the number of cells that are alive after each generation in a log file (and NOT to the console).

While the program is running in Interactive mode, it should react appropriately if the user presses any of the following keys:

Q - quits the program
F - regenerates a random grid filled with 20% live cells (or whatever percentage the user specified on the command line (see below))
R - clears the grid and then adds the R-Pentomino pattern in the middle of the grid
Whenever the user presses a valid key, you should log that fact using the Info log level.  Invalid user keypresses should be ignored (but you can log them if you want).

Command Line Parameters:

The program can run in one of two different modes - "Interactive Mode" or "Silent Mode" - based on the first command-line argument.  If the first argument is "interactive" (the default) - or just the letter "i" - then every generation is displayed one-after-another AND the user can press F, R, or Q while the program is running to either quit or restart the game (see above for details).  If the first argument is "silent" - or just the letter "s" - only the very last generation is displayed; all other generations do not appear in the output.

The second command-line argument indicates how the grid should be initialized before the simulation starts.  If the second argument is an "R" (the default), then a R-Pentamino pattern (see below) is placed in the middle of the grid.  If the second argument is a number between 0 and 100 (inclusive), then the grid is randomly filled using the argument as the "random percentage" (see RandomStars for an example).

The third command-line argument is an integer that determines how many generations to calculation before the simulation automatically stops. If the user enters "-1", the simulation runs 'forever' (assuming that it is in Interactive mode).  Otherwise, the simulation stops after the generation specified by the third argument is displayed.  The default for interactive mode is -1 ("forever").   The default for silent mode is 50.

<img width="116" alt="RPentomino" src="https://user-images.githubusercontent.com/71481139/179337230-b2458066-7350-45e6-bd8a-4fbe60903a22.png">

So, for example, if you entered "dotnet run silent r 0" your program should display the following output (on the left side).  (The log.txt file should contain the information shown on the right side.)

LifeSilentR0.png

If the user entered "dotnet run silent r 1", your program would display:

LifeSilentR1.png

and if the user entered "dotnet run silent r 10", your program would display:

LifeSilentR10.png

Because it is in silent mode, it should NOT display any of the other generations.

On the other hand, if you entered "dotnet run" (with no arguments), your program should run in interactive mode starting with the R-Pentomino pattern and run until the user presses "Q".   When it starts running, it should look something like this:

The R-Pentomino pattern running

Again, remember, if Silent mode is specified on the command line, you should only display the final generation!
