using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;
using TheatricalPlayersRefactoringKata.Application.Services.Invoices;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoicePrinterTextTests
{
    private readonly InvoicePrinterText _invoicePrinterText;

    public InvoicePrinterTextTests()
    {
        _invoicePrinterText = new InvoicePrinterText(new InvoiceCalculator());
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void ShouldGenerateCorrectStatement()
    {
        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(new Play("Hamlet", 4024, PlayType.Tragedy), 55),
                new Performance(new Play("As You Like It", 2670, PlayType.Comedy), 35),
                new Performance(new Play("Othello", 3560, PlayType.Tragedy), 40),
            }
        );

        var result = _invoicePrinterText.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void ShouldGenerateCorrectStatement_WithMorePlays()
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

        var result = _invoicePrinterText.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_WhenInvoiceIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _invoicePrinterText.Print(null));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenPerformancesIsNull()
    {
        var invoice = new Invoice("BigCo", null);
        Assert.Throws<ArgumentException>(() => _invoicePrinterText.Print(invoice));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCustomerNameIsNullOrEmpty()
    {
        // Testing with null company name
        var invoiceWithNullName = new Invoice(null, new List<Performance>());
        Assert.Throws<ArgumentException>(() => _invoicePrinterText.Print(invoiceWithNullName));

        // Testing with empty company name
        var invoiceWithEmptyName = new Invoice(string.Empty, new List<Performance>());
        Assert.Throws<ArgumentException>(() => _invoicePrinterText.Print(invoiceWithEmptyName));
    }
}