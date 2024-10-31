using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Hard;

public class RRC2
{
    private static int[][] Room = [
                                    [1,1,1,1,1,0,1,1],
                                    [1,1,1,1,1,0,1,1],
                                    [1,0,1,1,1,1,1,1],
                                    [0,0,0,1,0,0,0,0],
                                    [1,1,1,1,1,1,1,1]
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
        /*
         * The idea is to do a DFS algo
         * I imagine this is more efficient for a rectangular space with weird walls and corners
         * perhaps it is more efficient in general, even for an open space
         * anyways, you should google that later
         * 
         * 1. Define a set of directions CW or CCW
         * 2. Define a set of visited spaces
         * 3. Being the recursive algorithm, args: robot, visitedSpaces, position, direction
         * 4. Clean the current space.
         * 5. Save the current space to visited spaces.
         * 6. Enter a loop of the directions defined in step 1
         * 7. Compute a new direction such that the 1st time you enter the loop, the current direction is maintained: (dir + i) % 4
         * 8. Compute the next position
         * 9. if the next position is valid (not a wall, not visited before, reenter the algo)
         * --> after that, it means that we have reached a corner, so need to back track (move one spot in opposite direction, maintain orientation)
         * 10. if not: turn in the next direction of your predefined directions (right for CW, left for CCW)
         * 
         * 
         */

        var visited = new HashSet<(int, int)>();

        DfsRoomba(roomba, visited, 0, 0, 0);
    }

    private static void DfsRoomba(IRobot roomba, HashSet<(int, int)> visited, int direction, int row, int column)
    {
        roomba.Clean();
        visited.Add((row, column));

        for (int i = 0; i < 4; i++)
        {
            int newDirection = (direction + i) % 4;
            int newRow = row + Directions[newDirection][0];
            int newColumn = column + Directions[newDirection][1];

            if (!visited.Contains((newRow, newColumn)) && roomba.Move())
            {
                DfsRoomba(roomba, visited, newDirection, newRow, newColumn);

                // if we reach here, we need to back track
                roomba.TurnRight();
                roomba.TurnRight();
                roomba.Move();
                roomba.TurnRight();
                roomba.TurnRight();
            }

            roomba.TurnRight();
        }
    }

    private static int[][] Directions = [
            [-1, 0], // up = 0
            [0, 1],  // right = 1
            [1, 0],  // down = 2
            [0, -1]  // left = 3
            ];
}
