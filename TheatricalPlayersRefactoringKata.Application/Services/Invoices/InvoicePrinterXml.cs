using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices;

/// <summary>
/// Print in XML the invoice totals for theatrical performances.
/// </summary>
public class InvoicePrinterXml : IInvoicePrinter
{
    private readonly IInvoiceCalculator _invoiceCalculator;
    private readonly CultureInfo _cultureInfo;

    public InvoicePrinterXml(IInvoiceCalculator invoiceCalculator, CultureInfo cultureInfo = null)
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

        var invoiceResult = _invoiceCalculator.CalculateInvoice(invoice);

        var xmlDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                CreateItemsElement(invoice, invoiceResult),
                new XElement("AmountOwed", FormatAmount(invoiceResult.TotalAmount)),
                new XElement("EarnedCredits", invoiceResult.VolumeCredits)
            )
        );

        return xmlDocument.ToString();
    }

    /// <summary>
    /// Creates the Items element with the performance items
    /// </summary>
    /// <param name="invoice"></param>
    /// <param name="invoiceResult"></param>
    private XElement CreateItemsElement(Invoice invoice, InvoiceResult invoiceResult)
    {
        if (invoiceResult.Amounts.Count != invoice.Performances.Count)
            throw new InvalidOperationException("Mismatch between performances and calculated amounts.");

        var itemsElement = new XElement("Items");

        for (int i = 0; i < invoice.Performances.Count; i++)
        {
            var perf = invoice.Performances[i];
            var thisAmount = invoiceResult.Amounts[i];
            var thisCredit = invoiceResult.Credits[i];

            itemsElement.Add(
                new XElement("Item",
                    new XElement("AmountOwed", FormatAmount(thisAmount)),
                    new XElement("EarnedCredits", thisCredit),
                    new XElement("Seats", perf.Audience)
                )
            );
        }

        return itemsElement;
    }

    private string FormatAmount(decimal amount)
    {
        var thisAmount = amount / 100m;
        return thisAmount.ToString("0.00", _cultureInfo);
    }
}