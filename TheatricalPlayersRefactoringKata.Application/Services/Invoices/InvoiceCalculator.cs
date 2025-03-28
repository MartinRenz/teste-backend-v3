using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;
using TheatricalPlayersRefactoringKata.Core.Factories;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices;

/// <summary>
/// Calculates invoice totals for theatrical performances.
/// </summary>
public class InvoiceCalculator : IInvoiceCalculator
{
    /// <summary>
    /// Calculates the total amount and volume credits for an invoice.
    /// </summary>
    /// <param name="invoice"></param>
    public InvoiceResult CalculateInvoice(Invoice invoice)
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var amounts = new List<decimal>();
        var credits = new List<int>();

        if (invoice == null)
            throw new ArgumentNullException("Invoice parameter cannot be null.");

        if (invoice.Performances == null)
            throw new ArgumentException("Performances collection cannot be null.");

        if (string.IsNullOrEmpty(invoice.Customer))
            throw new ArgumentException("Customer name cannot be null or empty.");

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;

            var calculator = PlayTypeCalculatorFactory.GetCalculator(play.Type);
            var thisAmount = calculator.CalculateAmount(perf);
            amounts.Add(thisAmount);
            totalAmount += thisAmount;

            var thisCredit = calculator.CalculateVolumeCredits(perf);
            credits.Add(thisCredit);
            volumeCredits += thisCredit;
        }

        return new InvoiceResult(totalAmount, volumeCredits, amounts, credits);
    }
}