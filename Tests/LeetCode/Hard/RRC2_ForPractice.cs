using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.ca/all/489.html

public class RRC2_ForPractice
{
    private static readonly int[][] Room =
    [
        [1, 1, 1, 1, 1, 0, 1, 1],
        [1, 1, 1, 1, 1, 0, 1, 1],
        [1, 0, 1, 1, 1, 1, 1, 1],
        [0, 0, 0, 1, 0, 0, 0, 0],
        [1, 1, 1, 1, 1, 1, 1, 1]
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
        roomba.RoomState.SelectMany(x => x).Any(y => y == 1).Should().BeFalse();

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

    private static void CleanRoom(IRobot robot)
    { }
}