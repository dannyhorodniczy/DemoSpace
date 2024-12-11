using System;
using System.Linq;
using Xunit;

namespace Tests.Practice;

// given a 2d array of prices of stocks of different company over 7 days. Find the 3 top company with the highest averages.

public class Given2dArrayOfStocksOfCompaniesOver7Days
{
    [Fact]
    public void Given_When_Then()
    {
        var stocks = BuildStocksArray();

        var companyAverages = new (int company, double avg)[stocks.Length];

        for (int i = 0; i < stocks.Length; i++)
        {
            companyAverages[i] = (i, stocks[i].Average());
            Console.WriteLine($"Company: {companyAverages[i].company}, Avg: {companyAverages[i].avg}");
        }

        Console.WriteLine();

        var ordered = companyAverages.OrderByDescending(x => x.avg).ToArray();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Company: {ordered[i].company}, Avg: {ordered[i].avg}");
        }
    }

    private static int[][] BuildStocksArray(int companyCount = 20)
    {
        var rnd = new Random();
        int[][] stocks = new int[companyCount][]; // [company, day_of_week]
        for (int i = 0; i < stocks.Length; i++)
        {
            stocks[i] = new int[7];
            for (int j = 0; j < stocks[0].Length; j++)
                stocks[i][j] = rnd.Next(1, 100);
        }
        return stocks;
    }
}
