using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using System.Xml;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoicePrinterXmlTests
{
    private readonly InvoicePrinterXml _invoicePrinterXml;

    public InvoicePrinterXmlTests()
    {
        _invoicePrinterXml = new InvoicePrinterXml(new InvoiceCalculator());
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void ShouldGenerateCorrectXmlStatement()
    {
        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(new Play("Hamlet", 4024, PlayType.Tragedy), 55),
                new Performance(new Play("As You Like It", 2670, PlayType.Comedy), 35),
                new Performance(new Play("Othello", 3560, PlayType.Tragedy), 40),
                new Performance(new Play("Henry V", 3227, PlayType.History), 20),
                new Performance(new Play("King John", 2648, PlayType.History), 39),
                new Performance(new Play("Henry V", 3227, PlayType.History), 20)
            }
        );

        var result = _invoicePrinterXml.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    public void ShouldGenerateValidXmlStructure()
    {
        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(new Play("Hamlet", 4024, PlayType.Tragedy), 55),
                new Performance(new Play("As You Like It", 2670, PlayType.Comedy), 35),
                new Performance(new Play("Othello", 3560, PlayType.Tragedy), 40)
            }
        );

        var result = _invoicePrinterXml.Print(invoice);

        // Load the XML into XmlDocument
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(result);

        Assert.NotNull(xmlDoc.DocumentElement);

        // Assert the structure of the XML
        Assert.Equal("Statement", xmlDoc.DocumentElement.Name);

        // Validate the presence of the Customer element
        var customerElement = xmlDoc.SelectSingleNode("//Customer");
        Assert.NotNull(customerElement);
        Assert.Equal("BigCo", customerElement.InnerText);

        // Validate presence of Item elements inside the Items element
        var itemElements = xmlDoc.SelectNodes("//Item");
        Assert.NotNull(itemElements);
        Assert.True(itemElements.Count > 0);

        // Validate specific structure of the first Item
        var firstItem = itemElements[0];
        Assert.NotNull(firstItem.SelectSingleNode("AmountOwed"));
        Assert.NotNull(firstItem.SelectSingleNode("EarnedCredits"));
        Assert.NotNull(firstItem.SelectSingleNode("Seats"));
    }


    [Fact]
    public void ShouldThrowArgumentNullException_WhenInvoiceIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _invoicePrinterXml.Print(null));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenPerformancesIsNull()
    {
        var invoice = new Invoice("BigCo", null);
        Assert.Throws<ArgumentException>(() => _invoicePrinterXml.Print(invoice));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCustomerNameIsNullOrEmpty()
    {
        // Testing with null company name
        var invoiceWithNullName = new Invoice(null, new List<Performance>());
        Assert.Throws<ArgumentException>(() => _invoicePrinterXml.Print(invoiceWithNullName));

        // Testing with empty company name
        var invoiceWithEmptyName = new Invoice(string.Empty, new List<Performance>());
        Assert.Throws<ArgumentException>(() => _invoicePrinterXml.Print(invoiceWithEmptyName));
    }
}