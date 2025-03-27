using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices;

/// <summary>
/// Print the invoice totals for theatrical performances.
/// </summary>
public class InvoicePrinterText : IInvoicePrinter
{
    private readonly IInvoiceCalculator _invoiceCalculator;
    private readonly CultureInfo _cultureInfo;

    public InvoicePrinterText(IInvoiceCalculator invoiceCalculator, CultureInfo cultureInfo = null)
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

        var invoiceResult = _invoiceCalculator.CalculateInvoice(invoice);

        AppendPerformanceLines(invoice, invoiceResult, result);
        AppendFooter(invoiceResult, result);

        return result.ToString();
    }

    /// <summary>
    /// Appends formatted performance lines amount into the StringBuilder.
    /// </summary>
    /// <param name="invoice"></param>
    /// <param name="invoiceResult"></param>
    /// <param name="result"></param>
    private void AppendPerformanceLines(
        Invoice invoice,
        InvoiceResult invoiceResult,
        StringBuilder result
    )
    {
        if (invoiceResult.Amounts.Count != invoice.Performances.Count)
            throw new InvalidOperationException("Mismatch between performances and calculated amounts.");

        for (int i = 0; i < invoice.Performances.Count; i++)
        {
            var perf = invoice.Performances[i];
            var play = perf.Play;
            var thisAmount = invoiceResult.Amounts[i];

            result.AppendFormat(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
        }
    }

    /// <summary>
    /// Appends formatted total amount into the StringBuilder.
    /// </summary>
    /// <param name="invoiceResult"></param>
    /// <param name="result"></param>
    private void AppendFooter(InvoiceResult invoiceResult, StringBuilder result)
    {
        result.AppendFormat(_cultureInfo, "Amount owed is {0:C}\n", invoiceResult.TotalAmount / 100);
        result.Append($"You earned {invoiceResult.VolumeCredits} credits\n");
    }
}