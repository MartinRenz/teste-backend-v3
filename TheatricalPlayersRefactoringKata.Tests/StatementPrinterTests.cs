using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enums;
using TheatricalPlayersRefactoringKata.Presentation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
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

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
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


        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}