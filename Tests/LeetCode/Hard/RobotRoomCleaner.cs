using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.ca/all/489.html
public class RobotRoomCleaner
{
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

        for (int i = 0; i < Directions.Length; i++)
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

    private static int[][] Room = [
                                [1,1,1,1,1,0,1,1],
                                    [1,1,1,1,1,0,1,1],
                                    [1,0,1,1,1,1,1,1],
                                    [0,0,0,1,0,0,0,0],
                                    [1,1,1,1,1,1,1,1]
                              ];

    private static readonly int[][] Directions = [
                                                  [-1, 0], // up 0
                                                  [0, 1],  // right 1
                                                  [1, 0],  // down 2
                                                  [0, -1]  // left 3
                                                 ];
}
