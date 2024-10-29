using System;

namespace Tests.LeetCode.Hard;
public interface IRobot
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

internal class Robot : IRobot
{
    private int[][] _roomState;
    private int _row;
    private int _column;
    private int _height;
    private int _width;
    private Direction _direction;

    public Robot(
        int[][] initialRoomState,
        int startingRow = 1,
        int startingColumn = 3)
    {
        _roomState = initialRoomState;
        _row = startingRow;
        _column = startingColumn;
        _height = initialRoomState.Length;
        _width = initialRoomState[0].Length;
        _direction = Direction.Up;

        Console.WriteLine($"height: {_height}, width: {_width}");
        Print(_roomState, _row, _column);
    }

    public int[][] RoomState => _roomState;

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
                Console.WriteLine($"Moved up. row: {_row}, column: {_column}");
                Print(_roomState, _row, _column);
                return true;

            case Direction.Down:
                if (_row == _height - 1)
                {
                    return false;
                }

                if (_roomState[_row + 1][_column] == 0)
                {
                    return false;
                }

                _row++;
                Console.WriteLine($"Moved down. row: {_row}, column: {_column}");
                Print(_roomState, _row, _column);
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
                Console.WriteLine($"Moved left. row: {_row}, column: {_column}");
                Print(_roomState, _row, _column);
                return true;

            case Direction.Right:
                if (_column == _width - 1)
                {
                    return false;
                }

                if (_roomState[_row][_column + 1] == 0)
                {
                    return false;
                }

                _column++;
                Console.WriteLine($"Moved right. row: {_row}, column: {_column}");
                Print(_roomState, _row, _column);
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

        Console.WriteLine($"Turned Left, direction is now: {_direction}");
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

        Console.WriteLine($"Turned Right, direction is now: {_direction}");
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }

    private static void Print(int[][] room, int row, int column)
    {
        //Console.WriteLine();
        //Console.WriteLine($"Cleaned Row: {row}, Column: {column}");
        Console.WriteLine();
        for (int i = 0; i < room.Length; i++)
        {
            Console.Write("[");
            for (int j = 0; j < room[0].Length; j++)
            {
                // robot location
                if (i == row && j == column)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(room[i][j]);
                }
                // clean place
                else if (room[i][j] == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(room[i][j]);
                }
                // wall
                else if (room[i][j] == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(room[i][j]);
                }
                //dirty place
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(room[i][j]);
                }

                if (j != room[0].Length - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(',');
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("]");
        }
    }
}
