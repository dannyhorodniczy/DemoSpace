using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.ca/all/489.html
/* The Roomba algorithm
 * This algorithm will use the IRobot API to clean a 2D room
 *
 * To start, we need a list of visited locations (hashset in this case for fast lookup)
 * and a list of directions (can be CW or CCW)
 *
 * This will be a recursive algorithm performing a DFS algorithm
 * The function will accept the robot, the current position, the visited spaces, and the direction
 * Upon entering the function, the robot will:
 * 1. Clean the current space
 * 2. Add it to the list of visited spaces
 * 3. Enter a loop of the 4 directions (CW or CCW) that you have defined
 * 4. Compute the next direction for the robot nextDir = (dir + i) % 4 (so we maintain orientation)
 * 5. Compute the next position with the new direction
 * 6. Check to see if the new position is valid: (not a wall, not visited)
 * 7. If valid: call the function again with the new direction
 * --> Backtrack while maintaining orientation
 * 8. If invalid: turn in the chosen direction (CW (right) or CCW (left))
 */

public class RobotRoomCleaner
{
    private static readonly int[][] Room =
    [
        [1, 1, 1, 1, 1, 0, 1, 1],
        [1, 1, 1, 1, 1, 0, 1, 1],
        [1, 0, 1, 1, 1, 1, 1, 1],
        [0, 0, 0, 1, 0, 0, 0, 0],
        [1, 1, 1, 1, 1, 1, 1, 1]
    ];

    private static readonly int[][] Directions =
    [
        [-1, 0], // up 0
        [0, 1], // right 1
        [1, 0], // down 2
        [0, -1] // left 3
    ];

    [Fact]
    public void GivenARoomaAndARoomba_WhenCleanRoom_ThenTheRoomIsCleaned()
    {
        // Given
        var roomba = new Robot(Room);
        int dirtyCountBefore = roomba.RoomState.SelectMany(x => x).Where(x => x == 1).Count();
        int wallCountBefore = roomba.RoomState.SelectMany(x => x).Where(x => x == 0).Count();

        // When
        CleanRoom(roomba);

        // Then
        roomba.RoomState.Any(x => x.Any(y => y == 1)).Should().BeFalse();

        int cleanCountAfter = roomba.RoomState.SelectMany(x => x).Where(x => x == 2).Count();
        int wallCountAfter = roomba.RoomState.SelectMany(x => x).Where(x => x == 0).Count();

        dirtyCountBefore.Should().Be(cleanCountAfter);
        wallCountBefore.Should().Be(wallCountAfter);
    }

    [Fact]
    public void GivenAnEdgeCase_WhenCleanRoom_ThenTheRoomIsCleaned()
    {
        // Given
        var roomba = new Robot([[1]], 0, 0);
        int dirtyCountBefore = roomba.RoomState.SelectMany(x => x).Where(x => x == 1).Count();
        int wallCountBefore = roomba.RoomState.SelectMany(x => x).Where(x => x == 0).Count();

        // When
        CleanRoom(roomba);

        // Then
        roomba.RoomState.Any(x => x.Any(y => y == 1)).Should().BeFalse();

        int cleanCountAfter = roomba.RoomState.SelectMany(x => x).Where(x => x == 2).Count();
        int wallCountAfter = roomba.RoomState.SelectMany(x => x).Where(x => x == 0).Count();

        dirtyCountBefore.Should().Be(cleanCountAfter);
        wallCountBefore.Should().Be(wallCountAfter);
    }

    private static void CleanRoom(IRobot roomba)
    {
        RoombaDfs(roomba, new HashSet<(int, int)>(), 0, 0, 0);
    }

    private static void RoombaDfs(IRobot roomba, HashSet<(int, int)> visited, int direction, int row, int column)
    {
        roomba.Clean();
        visited.Add((row, column));

        for (var i = 0; i < Directions.Length; i++)
        {
            int newDirection = (direction + i) % Directions.Length;
            int newRow = row + Directions[newDirection][0];
            int newColumn = column + Directions[newDirection][1];

            if (!visited.Contains((newRow, newColumn)) && roomba.Move())
            {
                RoombaDfs(roomba, visited, newDirection, newRow, newColumn);

                // if we reach this point, it means we have been cornered
                // try to move back one space and maintain orientation
                roomba.TurnLeft();
                roomba.TurnLeft();
                roomba.Move();
                roomba.TurnLeft();
                roomba.TurnLeft();
            }

            roomba.TurnRight();
        }
    }
}