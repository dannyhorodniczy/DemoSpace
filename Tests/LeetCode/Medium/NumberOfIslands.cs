using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/number-of-islands/

public class NumberOfIslands
{
    [Fact]
    public void Given_WhenNumIslands_Then()
    {
        char[][] grid = [
  ['1','1','1','1','0'],
  ['1','1','0','1','0'],
  ['1','1','0','0','0'],
  ['0','0','0','0','0']
];
        int result = NumIslands(grid);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenNumIslands_Then2()
    {
        char[][] grid = [
  ['1','1','0','0','0'],
  ['1','1','0','0','0'],
  ['0','0','1','0','0'],
  ['0','0','0','1','1']
];
        int result = NumIslands(grid);
        result.Should().Be(3);
    }

    [Fact]
    public void Given_WhenNumIslands_Then3()
    {
        char[][] grid = [
  ['1','1','1'],
  ['0','1','0'],
  ['0','1','0']
];
        int result = NumIslands(grid);
        result.Should().Be(1);
    }

    private int NumIslands(char[][] grid)
    {
        int[][] directions = [
        [-1,0], // up
        [0,1], // right
        [1,0], // down
        [0,-1], // up
        ];

        int islandCount = 0;
        var visited = new HashSet<(int, int)>();
        for (int row = 0; row < grid.Length; row++)
        {
            for (int column = 0; column < grid[row].Length; column++)
            {
                Console.WriteLine($"Checking row {row}, column {column}");

                if (visited.Contains((row, column)))
                {
                    continue;
                }

                if (grid[row][column] == '1')
                {
                    // DFS
                    Dfs(directions, grid, visited, 0, row, column, grid.Length - 1, grid[0].Length - 1);
                    islandCount++;
                    continue;
                }

                if (grid[row][column] == '0')
                {
                    visited.Add((row, column));
                    continue;
                }
            }
        }

        return islandCount;
    }

    private static void Dfs(int[][] directions, char[][] grid, HashSet<(int, int)> visited, int direction, int row, int column, int maxRow, int maxColumn)
    {
        visited.Add((row, column));
        grid[row][column] = '2';
        Console.WriteLine($"Current direction: {(Dir) direction}");
        Print(grid, visited, row, column);

        for (int i = 0; i < 4; i++)
        {
            int newDirection = (direction + i) % 4;
            Console.WriteLine($"New direction: {(Dir) newDirection}");

            int newRow = row + directions[newDirection][0];
            if (newRow < 0 || newRow > maxRow)
            {
                continue;
            }

            int newColumn = column + directions[newDirection][1];
            if (newColumn < 0 || newColumn > maxColumn)
            {
                continue;
            }

            if (grid[newRow][newColumn] == '0')
            {
                continue;
            }

            if (!visited.Contains((newRow, newColumn)))
            {
                Dfs(directions, grid, visited, newDirection, newRow, newColumn, maxRow, maxColumn);

                // Backtracking
                // need to move back one space

                // this does not appear to be necessary in the alg
                // I believe because we don't have to move the "robot" around manually
                // after we exit one call, we are automatically moved back 1 space
                // in the robot algorithm, we have to explicitly move the robot
                // the robot does not return to the position it was in when the call was made
                // in this alg, the "robot" doesn't have a position, so your effective position
                // is the one when the call was made
                // so you backtrack and check all 4 directions for all spaces by default :)

                //if (newDirection == 0) // up
                //{
                //    row++;
                //    continue;
                //}
                //if (newDirection == 1) // right
                //{
                //    column--;
                //    continue;
                //}
                //if (newDirection == 2) // down
                //{
                //    row--;
                //    continue;
                //}
                //if (newDirection == 3) // left
                //{
                //    column++;
                //    continue;
                //}
            }
        }

    }
    private enum Dir
    {
        Up, Right, Down, Left
    }

    private static void Print(char[][] grid, HashSet<(int, int)> visited, int row, int column)
    {
        //Console.WriteLine();
        //Console.WriteLine($'Cleaned Row: {row}, Column: {column}');
        Console.WriteLine();
        for (int i = 0; i < grid.Length; i++)
        {
            Console.Write('[');
            for (int j = 0; j < grid[0].Length; j++)
            {
                // robot location
                if (i == row && j == column)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(grid[i][j]);
                }
                // visited place
                else if (visited.Contains((i, j)))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(grid[i][j]);
                }
                // water
                else if (grid[i][j] == '0')
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(grid[i][j]);
                }
                // unvisited place
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(grid[i][j]);
                }

                if (j != grid[0].Length - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(',');
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(']');
        }
    }

    /*
     * ok this seems like a problem where we want to use DFS with backtracking
     * because you need to identify a full contiguous island
     * 
     * I believe the algorithm should be:
     * create a hashset to define visited spaces
     * 
     * do a double for loop
     * if you find a piece of land, you do DFS w/backtracking until complete, increment the island count
     * if you find water, you don't care
     * if you find a 
     * *make sure that you mark visited spaces (both water and DFS, water might not be necessary actually)*
     * 
     * continue the loop to the end
     */
}
