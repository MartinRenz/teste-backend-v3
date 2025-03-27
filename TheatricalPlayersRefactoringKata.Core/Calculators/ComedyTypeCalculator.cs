using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class ComedyTypeCalculator : IPlayTypeCalculator
{
    public decimal CalculateAmount(Performance perf)
    {
        var lines = perf.Play.Lines;
        var thisAmount = lines * 10;

        // Lógica específica do Comedy
        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }

        thisAmount += 300 * perf.Audience;

        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance perf)
    {
        var volumeCredits = Math.Max(perf.Audience - 30, 0);
        volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        return volumeCredits;
    }
}