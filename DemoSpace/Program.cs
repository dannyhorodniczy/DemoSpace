using System;

namespace DemoSpace;

internal class Program
{
    static void Main(string[] args)
    {
        int[] arr = [0, 1, 2, 3, 4, 5];
        Console.WriteLine(string.Join(", ", arr[0..(2 + 1)]));
        Console.WriteLine(string.Join(", ", arr[3..]));

        /* The Roomba algorithm in 2D (DFS)
         * 1. Pick an order for the set of cardinal directions (i.e., CW or CCW)
         * --> you can think of the [row, column] jagged array representation
         * 2. Create a hashset (hashmap in some languages) that will contain the list
         * --> of all visited spaces
         * 3. The algorithm begins.
         * Arguments: robot, current position, visited spaces, direction (or orientation)
         * 4. Clean the current space.
         * 5. Add it to the list of visited spaces
         * 6. Enter a loop of the 4 directions defined above
         * 7. Compute the next for the robot (compute direction, then position)
         * Direction needs to maintain it's orientation on the 1st loop
         * For example: if you were facing left when you 1st enter the loop,
         * you should continue facing left, and the loop should go: left, up, right, down
         * (if you have selected a CW direction that is)
         * basically: newDir = (dir + i) % 4 (in the case of: for (int i=0; I<4; i++))
         * (dir is the integer representation direction that you entered the loop with)
         * 8. Check to see if the new position is valid:
         * --> has not been visited
         * --> is not a wall
         * if valid: reenter the algorithm with the new direction
         * --> backtrack: once the algo reentry has completed
         * --> it means we're cornered by walls or visited spaces
         * --> so we need to backtrack: turn around, try to move 1 space, turn back around
         * else (invalid):
         * turn in your chosen direction CW or CCW
         * 
         */
    }
}
