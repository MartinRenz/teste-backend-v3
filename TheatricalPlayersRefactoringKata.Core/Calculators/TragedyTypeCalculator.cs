using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class TragedyTypeCalculator : IPlayTypeCalculator
{
    public decimal CalculateAmount(Performance perf)
    {
        var lines = perf.Play.Lines;
        var thisAmount = lines * 10;

        if (perf.Audience > 30)
        {
            thisAmount += 1000 * (perf.Audience - 30);
        }

        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance perf)
    {
        return Math.Max(perf.Audience - 30, 0);
    }
}