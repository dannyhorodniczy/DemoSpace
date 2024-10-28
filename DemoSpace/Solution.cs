using System;
using System.Collections.Generic;
namespace DemoSpace;

internal class Solution
{
    // Directions: up, right, down, left
    private static int[][] _directions = new int[][]
        {
            new int[] {-1, 0}, // up
            new int[] {0, 1}, // right
            new int[] {1, 0}, // down 
            new int[] {0, -1} // up
        };

    public static void CleanRoom(IRobot robot)
    {
        var visited = new HashSet<(int, int)>();
        DFS(robot, 0, 0, 0, visited);
    }

    public static void DFS(IRobot robot, int row, int col, int direction, HashSet<(int, int)> visited)
    {
        robot.Clean();
        visited.Add((row, col));

        for (int i = 0; i < 4; i++)
        {
            int newDirection = (direction + i) % 4;
            int newRow = row + _directions[newDirection][0];
            int newCol = col + _directions[newDirection][1];

            if (!visited.Contains((newRow, newCol)) && robot.Move())
            {
                DFS(robot, newRow, newCol, newDirection, visited);
                // Backtrack
                Console.WriteLine("Backtrack start.");
                robot.TurnLeft();
                robot.TurnLeft();
                robot.Move();
                robot.TurnLeft();
                robot.TurnLeft();
                Console.WriteLine("Backtrack end.");
            }
            robot.TurnRight();
        }
    }
}
