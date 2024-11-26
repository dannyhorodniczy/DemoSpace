using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/robot-bounded-in-circle/

public class RobotBoundedInACircle
{
    [Theory]
    [InlineData("GGLLGG", true)]
    [InlineData("GG", false)]
    [InlineData("GL", true)]
    public void Given_WhenIsRobotBounded_Then(string instructions, bool expected)
    {
        bool result = IsRobotBounded(instructions);
        result.Should().Be(expected);
    }

    private static bool IsRobotBounded(string instructions)
    {
        // basically we need to detect cycles
        // but at what point do we know it's not a cycle

        // hint1: Calculate the final vector of how the robot travels after executing all instructions once - it consists of a change in position plus a change in direction.
        // hint2: The robot stays in the circle if and only if (looking at the final vector) it changes direction (ie. doesn't stay pointing north), or it moves 0.
        int dir = 0;
        var current = (0, 0);

        int[][] directions = [
            [0,1], // up
            [1,0], // right
            [0,-1], // down
            [-1,0]  // left
            ];


        foreach (char c in instructions)
        {
            switch (c)
            {
                case 'G':
                    current.Item1 += directions[dir][0];
                    current.Item2 += directions[dir][1];
                    break;
                case 'L':
                    if (dir == 0) dir = 3;
                    else dir--;
                    break;
                case 'R':
                    if (dir == 3) dir = 0;
                    else dir++;
                    break;
                default:
                    break;
            }
        }

        return dir != 0 || (current.Item1 == 0 && current.Item2 == 0);
    }
}
