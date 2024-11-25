using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/valid-sudoku/

public class ValidSudoku
{
    [Fact]
    public void Given_WhenIsValidSudoku_Then()
    {
        char[][] board =
[['5','3','.','.','7','.','.','.','.']
,['6','.','.','1','9','5','.','.','.']
,['.','9','8','.','.','.','.','6','.']
,['8','.','.','.','6','.','.','.','3']
,['4','.','.','8','.','3','.','.','1']
,['7','.','.','.','2','.','.','.','6']
,['.','6','.','.','.','.','2','8','.']
,['.','.','.','4','1','9','.','.','5']
,['.','.','.','.','8','.','.','7','9']];

        var result = IsValidSudoku(board);
        result.Should().BeTrue();
    }

    [Fact]
    public void Given_WhenIsValidSudoku_Then2()
    {
        char[][] board =
[['8','3','.','.','7','.','.','.','.']
,['6','.','.','1','9','5','.','.','.']
,['.','9','8','.','.','.','.','6','.']
,['8','.','.','.','6','.','.','.','3']
,['4','.','.','8','.','3','.','.','1']
,['7','.','.','.','2','.','.','.','6']
,['.','6','.','.','.','.','2','8','.']
,['.','.','.','4','1','9','.','.','5']
,['.','.','.','.','8','.','.','7','9']];

        var result = IsValidSudoku(board);
        result.Should().BeFalse();
    }

    [Fact]
    public void Given_WhenIsValidSudoku_Then3()
    {
        char[][] board =
[['.','.','.','.','5','.','.','1','.']
,['.','4','.','3','.','.','.','.','.']
,['.','.','.','.','.','3','.','.','1']
,['8','.','.','.','.','.','.','2','.']
,['.','.','2','.','7','.','.','.','.']
,['.','1','5','.','.','.','.','.','.']
,['.','.','.','.','.','2','.','.','.']
,['.','2','.','9','.','.','.','.','.']
,['.','.','4','.','.','.','.','.','.']];

        var result = IsValidSudoku(board);
        result.Should().BeFalse();
    }

    private static bool IsValidSudoku(char[][] board)
    {
        /*
         * 3 checks
         * 1. row check
         * 2. column check
         * 3. 3x3 check
         * 
         * foreach check, use a fresh hashset, ignore periods
         */

        for (int row = 0; row < board.Length; row++)
        {
            var hashset = new HashSet<char>();
            foreach (var columnValue in board[row])
            {
                if (columnValue == '.') continue;
                if (!hashset.Add(columnValue)) return false;
            }
        }

        for (int column = 0; column < board[0].Length; column++)
        {
            var hashset = new HashSet<char>();
            for (int row = 0; row < board.Length; row++)
            {
                if (board[row][column] == '.') continue;
                if (!hashset.Add(board[row][column])) return false;
            }
        }

        for (int outerRow = 0; outerRow < 7; outerRow += 3)
        {
            for (int outerColumn = 0; outerColumn < 7; outerColumn += 3)
            {
                var hashset = new HashSet<char>();
                for (int row = outerRow; row < outerRow + 3; row++)
                {
                    for (int column = outerColumn; column < outerColumn + 3; column++)
                    {
                        if (board[row][column] == '.') continue;
                        if (!hashset.Add(board[row][column])) return false;
                    }
                }
            }
        }

        return true;
    }
}
