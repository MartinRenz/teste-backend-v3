using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
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
            }
        );

        var result = _invoicePrinterXml.Print(invoice);

        Approvals.Verify(result);
    }
}