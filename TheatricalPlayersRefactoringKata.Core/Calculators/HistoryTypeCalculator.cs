using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class HistoryTypeCalculator : IPlayTypeCalculator
{
    private readonly IPlayTypeCalculator _tragedyCalculator = new TragedyTypeCalculator();
    private readonly IPlayTypeCalculator _comedyCalculator = new ComedyTypeCalculator();

    public decimal CalculateAmount(Performance perf)
    {
        var tragedyAmount = _tragedyCalculator.CalculateAmount(perf);
        var comedyAmount = _comedyCalculator.CalculateAmount(perf);

        return tragedyAmount + comedyAmount;
    }

    public int CalculateVolumeCredits(Performance perf)
    {
        return _tragedyCalculator.CalculateVolumeCredits(perf);
    }
}