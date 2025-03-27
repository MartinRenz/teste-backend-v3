using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices;

/// <summary>
/// Calculates invoice totals for theatrical performances.
/// </summary>
public class InvoiceCalculator : IInvoiceCalculator
{
    private readonly Dictionary<PlayType, IPlayTypeCalculator> _calculators;

    public InvoiceCalculator(Dictionary<PlayType, IPlayTypeCalculator> calculators)
    {
        _calculators = calculators ?? throw new ArgumentNullException("Calculators dictionary cannot be null.");
    }

    /// <summary>
    /// Calculates the total amount and volume credits for an invoice.
    /// </summary>
    /// <param name="invoice"></param>
    public InvoiceResult CalculateInvoice(Invoice invoice)
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var amounts = new List<decimal>();

        if (invoice == null)
            throw new ArgumentNullException("Invoice parameter cannot be null.");

        if (invoice.Performances == null)
            throw new ArgumentException("Performances collection cannot be null.");

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;
            if (!_calculators.TryGetValue(play.Type, out var calculator))
                throw new InvalidOperationException($"Unknown play type: {play.Type}");

            var thisAmount = calculator.CalculateAmount(perf);
            amounts.Add(thisAmount);
            totalAmount += thisAmount;
            volumeCredits += calculator.CalculateVolumeCredits(perf);
        }

        return new InvoiceResult(totalAmount, volumeCredits, amounts);
    }
}