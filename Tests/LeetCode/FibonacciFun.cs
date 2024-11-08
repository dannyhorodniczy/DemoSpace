using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Tests.LeetCode;
public class FibonacciFun
{
    [Theory]
    [InlineData(5)]
    public static void Fun(int n)
    {
        var sw = new Stopwatch();

        // 1. Brute recursion
        sw.Restart();
        int a1 = FibonacciBruteForceRecursive(n);
        sw.Stop();
        Console.WriteLine($"A1: {a1}");
        Console.WriteLine($"ms: {sw.ElapsedTicks}");

        // 2. Brute recursion w/memory
        sw.Restart();
        int[] fibs = BuildArray(n);
        int a2 = FibonacciBruteForceRecursiveWithMemory(n, fibs);
        sw.Stop();
        Console.WriteLine($"A2: {a2}");
        Console.WriteLine($"ms: {sw.ElapsedTicks}");

        a1.Should().Be(a2);

        // 3. Loop
        sw.Restart();
        int[] fibs2 = BuildArray(n);
        int a3 = FibonacciBruteForceLoop(n, fibs2);
        sw.Stop();
        Console.WriteLine($"A3: {a3}");
        Console.WriteLine($"ms: {sw.ElapsedTicks}");

        a2.Should().Be(a3);

        // 4. Loop with best space complexity
        sw.Restart();
        int a4 = FibonacciOptimizedLoop(n);
        sw.Stop();
        Console.WriteLine($"A4: {a4}");
        Console.WriteLine($"ms: {sw.ElapsedTicks}");

        a3.Should().Be(a4);
    }

    private static int[] BuildArray(int n)
    {
        int[] fibs = new int[n + 1].Select(x => x = -1).ToArray();
        fibs[0] = 0;
        fibs[1] = 1;
        return fibs;
    }

    private static int FibonacciBruteForceRecursive(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        int x = FibonacciBruteForceRecursive(n - 1);
        int y = FibonacciBruteForceRecursive(n - 2);

        return x + y;
    }

    private static int FibonacciBruteForceRecursiveWithMemory(int n, int[] fibs)
    {
        if (fibs[n] != -1)
        {
            return fibs[n];
        }

        int x = FibonacciBruteForceRecursiveWithMemory(n - 1, fibs);
        fibs[n - 1] = x;
        int y = FibonacciBruteForceRecursiveWithMemory(n - 2, fibs);
        fibs[n - 2] = y;

        //fibs[n] = x + y;

        return x + y;
    }

    private static int FibonacciBruteForceLoop(int n, int[] fibs)
    {
        for (int i = 2; i < fibs.Length; i++)
        {
            fibs[i] = fibs[i - 1] + fibs[i - 2];
        }

        return fibs[n];
    }

    private static int FibonacciOptimizedLoop(int n)
    {
        if (n < 2)
        {
            return n;
        }

        int previous2 = 0;
        int previous1 = 1;
        int current = 0;

        for (int i = 2; i < n + 1; i++)
        {
            current = previous1 + previous2;
            previous2 = previous1;
            previous1 = current;
        }

        return current;
    }
}
