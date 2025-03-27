using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Presentation;

public class StatementPrinter
{
    private readonly Dictionary<PlayType, IPlayTypeCalculator> _calculators;

    public StatementPrinter()
    {
        _calculators = new Dictionary<PlayType, IPlayTypeCalculator>
        {
            { PlayType.Tragedy, new TragedyTypeCalculator() },
            { PlayType.Comedy, new ComedyTypeCalculator() }
        };
    }

    public string Print(Invoice invoice)
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;
            var calculator = _calculators[play.Type];

            // Performs the calculations.
            var thisAmount = calculator.CalculateAmount(perf);
            volumeCredits += calculator.CalculateVolumeCredits(perf);

            // Print line for this order.
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
