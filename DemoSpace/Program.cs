using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoSpace;

internal class Program
{
    // https://leetcode.ca/all/489.html
    private static int[][] Room = [
                            [1,1,1,1,1,0,1,1],
                            [1,1,1,1,1,0,1,1],
                            [1,0,1,1,1,1,1,1],
                            [0,0,0,1,0,0,0,0],
                            [1,1,1,1,1,1,1,1]
                           ];

    static void Main(string[] args)
    {
        Console.WriteLine(Room.Length); // row count
        Console.WriteLine(Room[0].Length); // column count
        Print(Room);
        IRobot api = new Robot(Room);

        // begin alg

        /*
         * ok let's think of how to do this
         * we know that the room will be a square, but that's about it
         * maybe something you can think about, but probably better to just google it
         * let's give it an hour of try, and then google
         * 
         * so you know your starting position, in a rectangular room with obstacles
         * but otherwise no other information
         * 
         */

        // end alg
        CleanRoom(api);
        Print(Room);
    }

    private static void CleanRoom(IRobot robot)
    {
        var visited = new HashSet<(int, int)>();
        DFS(robot, 0, 0, 0, visited);
    }

    private static void DFS(IRobot robot, int row, int col, int direction, HashSet<(int, int)> visited)
    {
        robot.Clean();
        visited.Add((row, col));

        // Directions: up, right, down, left
        int[][] directions = new int[][]
        {
            new int[] {-1, 0},
            new int[] {0, 1},
            new int[] {1, 0},
            new int[] {0, -1}
        };

        for (int i = 0; i < 4; i++)
        {
            int newDirection = (direction + i) % 4;
            int newRow = row + directions[newDirection][0];
            int newCol = col + directions[newDirection][1];

            if (!visited.Contains((newRow, newCol)) && robot.Move())
            {
                DFS(robot, newRow, newCol, newDirection, visited);
                // Backtrack
                robot.TurnLeft();
                robot.TurnLeft();
                robot.Move();
                robot.TurnLeft();
                robot.TurnLeft();
            }
            robot.TurnRight();
        }
    }

    private static void Print(int[][] room, int row, int column)
    {
        Console.WriteLine();
        Console.WriteLine($"Row: {row}, Column: {column}");
        Print(room);
    }

    private static void Print(int[][] room)
    {
        Console.WriteLine();
        foreach (int[] row in room)
        {
            Console.WriteLine($"[{string.Join(',', row)}]");
        }
    }

    private static bool VerifyIsClean(int[][] room)
    {
        return room.Any(x => x.Any(y => y == 1));
    }

    internal class Robot : IRobot
    {
        private int[][] _roomState;
        private int _row;
        private int _column;
        private Direction _direction;

        public Robot(
            int[][] initialRoomState,
            int startingRow = 1,
            int startingColumn = 3)
        {
            _roomState = initialRoomState;
            _row = startingRow;
            _column = startingColumn;
            _direction = Direction.Up;
        }
        public void Clean()
        {
            if (_roomState[_row][_column] == 0)
            {
                throw new Exception("You are inside of a wall, your movement algorithm is incorrect");
            }

            _roomState[_row][_column] = 2;
            Print(_roomState, _row, _column);
        }

        public bool Move()
        {
            switch (_direction)
            {
                case Direction.Up:
                    if (_row == 0)
                    {
                        return false;
                    }

                    if (_roomState[_row - 1][_column] == 0)
                    {
                        return false;
                    }

                    _row--;
                    return true;

                case Direction.Down:
                    if (_row == Room.Length - 1)
                    {
                        return false;
                    }

                    if (_roomState[_row + 1][_column] == 0)
                    {
                        return false;
                    }

                    _row++;
                    return true;

                case Direction.Left:
                    if (_column == 0)
                    {
                        return false;
                    }

                    if (_roomState[_row][_column - 1] == 0)
                    {
                        return false;
                    }

                    _column--;
                    return true;

                case Direction.Right:
                    if (_column == Room[0].Length - 1)
                    {
                        return false;
                    }

                    if (_roomState[_row][_column + 1] == 0)
                    {
                        return false;
                    }

                    _column++;
                    return true;

                default:
                    return false;
            }
        }

        public void TurnLeft()
        {
            _direction = _direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Right => Direction.Up,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                _ => throw new ArgumentOutOfRangeException(nameof(_direction), $"Not expected direction value: {_direction}"),
            };
        }

        public void TurnRight()
        {
            _direction = _direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(_direction), $"Not expected direction value: {_direction}"),
            };
        }

        private enum Direction
        {
            Up, Down, Left, Right
        }
    }

    interface IRobot
    {
        // returns true if next cell is open and robot moves into the cell.
        // returns false if next cell is obstacle and robot stays on the current cell.
        bool Move();

        // Robot will stay on the same cell after calling turnLeft/turnRight.
        // Each turn will be 90 degrees.
        void TurnLeft();
        void TurnRight();

        // Clean the current cell.
        void Clean();
    }
}
