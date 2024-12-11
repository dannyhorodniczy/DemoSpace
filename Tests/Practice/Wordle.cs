using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Practice;

public class WordleTests
{
    [Theory]
    [InlineData("words", "wordd", new S[] { S.Correct, S.Correct, S.Correct, S.Correct, S.Incorrect })]
    [InlineData("words", "words", new S[] { S.Correct, S.Correct, S.Correct, S.Correct, S.Correct })]
    [InlineData("words", "sdrow", new S[] { S.LetterExists, S.LetterExists, S.Correct, S.LetterExists, S.LetterExists })]
    [InlineData("words", "wrxyz", new S[] { S.Correct, S.LetterExists, S.Incorrect, S.Incorrect, S.Incorrect })]
    public void GivenAWordAndAGuess_WhenGuess_ThenReturnsExpectedOutput(string word, string guess, S[] statuses)
    {
        // Given
        var wordle = new Wordle(word);

        // When
        var result = wordle.Guess(guess);

        // Then
        result.Should().BeEquivalentTo(statuses, o => o.WithStrictOrdering());
    }

    [Theory]
    [InlineData("words", "guess")]
    public void Given6Guesses_WhenGuess_ThenThrows(string word, string guess)
    {
        // Given
        var wordle = new Wordle(word);

        // When
        wordle.Guess(guess);
        wordle.Guess(guess);
        wordle.Guess(guess);
        wordle.Guess(guess);
        wordle.Guess(guess);
        Func<S[]> func = () => wordle.Guess(guess);

        // Then
        func.Should().Throw<Exception>();
    }
}

public enum S
{
    Incorrect = 0,
    LetterExists = 1,
    Correct = 2
}

public class Wordle
{
    private readonly string theWord;
    private int _guessCount = 0;
    private bool _gameWon = false;

    public Wordle(string wordToGuess)
    {
        if (string.IsNullOrWhiteSpace(wordToGuess))
        {
            throw new ArgumentNullException();
        }

        wordToGuess = wordToGuess.Trim().ToLower();

        if (wordToGuess.Length != 5)
        {
            throw new ArgumentException("Improper length");
        }

        theWord = wordToGuess;
    }

    public S[] Guess(string input)
    {
        if (_guessCount >= 5 || _gameWon)
        {
            throw new Exception("The game is over!");
        }

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input is null or whitespace");
            return [];
        }

        input = input.Trim().ToLower();

        if (input.Length != 5)
        {
            Console.WriteLine("Input is incorrect length");
            return [];
        }

        S[] printStatus = new S[5];
        var remainingWordChars = new List<char>();
        var remainingInputChars = new List<char>();
        for (int j = 0; j < 5; j++)
        {
            if (input[j] == theWord[j])
            {
                printStatus[j] = S.Correct;
                remainingWordChars.Add('!');
                remainingInputChars.Add('?');
            }
            else
            {
                remainingWordChars.Add(theWord[j]);
                remainingInputChars.Add(input[j]);
            }
        }

        var arr = remainingInputChars.ToArray();
        for (int j = 0; j < 5; j++)
        {
            if (printStatus[j] == S.Correct)
            {
                continue;
            }

            if (remainingWordChars.Contains(arr[j]))
            {
                remainingInputChars.Remove(arr[j]);
                printStatus[j] = S.LetterExists;
            }
        }

        for (int j = 0; j < 5; j++)
        {
            if (printStatus[j] == S.LetterExists)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write(input[j]);
                Console.BackgroundColor = ConsoleColor.Black;
                continue;
            }

            if (printStatus[j] == S.Correct)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(input[j]);
                Console.BackgroundColor = ConsoleColor.Black;
                continue;
            }

            Console.Write(input[j]);
        }

        if (printStatus.Length == 5 && printStatus.All(x => x == S.Correct))
        {
            Console.WriteLine();
            Console.WriteLine("You win!");
            _gameWon = true;
        }

        _guessCount++;
        Console.WriteLine();

        return printStatus;
    }
}