using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Services;

/// <summary>
/// Print the invoice totals for theatrical performances.
/// </summary>
public class InvoicePrinter : IInvoicePrinter
{
    private readonly IInvoiceCalculator _invoiceCalculator;
    private readonly CultureInfo _cultureInfo;

    public InvoicePrinter(IInvoiceCalculator invoiceCalculator, CultureInfo cultureInfo = null)
    {
        _invoiceCalculator = invoiceCalculator ?? throw new ArgumentNullException("Invoice calculator cannot be null.");
        _cultureInfo = cultureInfo ?? CultureInfo.GetCultureInfo("en-US");
    }

    /// <summary>
    /// Print the total amount and volume credits for an invoice.
    /// </summary>
    /// <param name="invoice"></param>
    public string Print(Invoice invoice)
    {
        if (invoice == null)
            throw new ArgumentNullException("Invoice parameter cannot be null.");

        if (invoice.Performances == null)
            throw new ArgumentException("Performances collection cannot be null.");

        var result = new StringBuilder();
        result.AppendLine($"Statement for {invoice.Customer}");

        var (totalAmount, volumeCredits) = _invoiceCalculator.CalculateInvoiceTotals(invoice);

        AppendPerformanceLines(invoice, result);
        AppendFooter(result, totalAmount, volumeCredits);

        return result.ToString();
    }

    /// <summary>
    /// Appends formatted performance lines amount into the StringBuilder.
    /// </summary>
    /// <param name="invoice"></param>
    /// <param name="result"></param>
    private void AppendPerformanceLines(Invoice invoice, StringBuilder result)
    {
        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;
            var calculator = _invoiceCalculator.GetCalculatorForPlay(play.Type);
            var thisAmount = calculator.CalculateAmount(perf);
            result.AppendFormat(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
        }
    }

    /// <summary>
    /// Appends formatted total amount into the StringBuilder.
    /// </summary>
    /// <param name="invoice"></param>
    /// <param name="result"></param>
    private void AppendFooter(StringBuilder result, decimal totalAmount, int volumeCredits)
    {
        result.AppendFormat(_cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result.Append($"You earned {volumeCredits} credits\n");
    }
}