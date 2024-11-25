using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/snakes-and-ladders/

public class SnakesAndLaddersProblem
{
    /*
     * so this is a graph BFS question
     * BFS implies we use a queue
     * the queue is used to store each level of the graph or tree
     * BFS is used when we are trying to find the shortest path
     * which is exactly what we are trying to do here
     */

    [Fact]
    public void Given_WhenSnakesAndLadders_Then()
    {
        int[][] board = [
            [-1, -1, -1, -1, -1, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, 35, -1, -1, 13, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, 15, -1, -1, -1, -1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(4);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then2()
    {
        int[][] board = [
            [-1, -1],
            [-1, 3]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then3()
    {
        int[][] board = [
            [-1,-1,-1],
            [-1,9,8],
            [-1,8,9]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then4()
    {
        int[][] board = [
            [-1,4,-1],
            [6,2,6],
            [-1,3,-1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then5()
    {
        int[][] board = [
            [1,1,-1],
            [1,1,1],
            [-1,1,1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(-1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then6()
    {
        int[][] board = [
            [-1,1,2,-1],
            [2,13,15,-1],
            [-1,10,-1,-1],
            [-1,6,2,8]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then7()
    {
        int[][] board = [
            [-1,-1,19,10,-1],
            [2,-1,-1,6,-1],
            [-1,17,-1,19,-1],
            [25,-1,20,-1,-1],
            [-1,-1,-1,-1,15]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    private static int SnakesAndLadders(int[][] board)
    {
        if (board.Length < 3)
        {
            return 1;
        }

        // let's create the dictionary to store the snakes and ladders
        var snakesAndLadders = BuildSnakesAndLaddersDictionary(board);

        int boardSize = board.Length * board[0].Length;
        var bfsQueue = new Queue<int>();
        bfsQueue.Enqueue(1);
        var visited = new HashSet<int> { 1 };
        int rollCount = 0;

        while (bfsQueue.Count > 0)
        {
            // need to check each item in the Queue
            // this is why it's BFS: we are checking this level
            // we need to set queueCount before entering the loop
            // because the queue.Count will be modified inside the loop
            int queueCount = bfsQueue.Count;
            for (int i = 0; i < queueCount; i++)
            {
                int current = bfsQueue.Dequeue();
                if (current == boardSize)
                {
                    return rollCount;
                }

                int[] possibleMoves = GetPossibleMoves(current, boardSize);

                for (int j = 0; j < possibleMoves.Length; j++)
                {
                    // add snakes & ladders if they exist
                    if (snakesAndLadders.TryGetValue(possibleMoves[j], out int next))
                    {
                        possibleMoves[j] = next;
                    }

                    if (!visited.Contains(possibleMoves[j]))
                    {
                        visited.Add(possibleMoves[j]);
                        bfsQueue.Enqueue(possibleMoves[j]);
                    }
                }
            }

            rollCount++;
        }

        return -1;
    }

    private static Dictionary<int, int> BuildSnakesAndLaddersDictionary(int[][] board)
    {
        var snakesAndLadders = new Dictionary<int, int>();
        int counter = 0;
        bool leftToRight = true;
        for (int row = board.Length - 1; row > -1; row--)
        {
            if (leftToRight)
            {
                for (int column = 0; column < board[row].Length; column++)
                {
                    counter++;
                    if (board[row][column] != -1)
                    {
                        snakesAndLadders[counter] = board[row][column];
                    }
                }
            }
            else
            {
                for (int column = board[row].Length - 1; column > -1; column--)
                {
                    counter++;
                    if (board[row][column] != -1)
                    {
                        snakesAndLadders[counter] = board[row][column];
                    }
                }
            }

            leftToRight = !leftToRight;
        }

        return snakesAndLadders;
    }

    private static int[] GetPossibleMoves(int current, int boardLength)
    {
        int maxEndJump = Math.Min(current + 6, boardLength);

        var range = Enumerable.Range(
            current + 1,
            maxEndJump - current);

        return range.ToArray();
    }
}
