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

public class InvoiceCalculatorTests
{
  [Fact]
  public void ShouldCalculateCorrectInvoiceResult()
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

    var invoiceCalculator = new InvoiceCalculator();

    var result = invoiceCalculator.CalculateInvoice(invoice);

    Assert.Equal(165300m, result.TotalAmount);
    Assert.Equal(47, result.VolumeCredits);
  }

  [Fact]
  public void ShouldThrowArgumentNullException_WhenInvoiceIsNull()
  {
    var invoiceCalculator = new InvoiceCalculator();
    Assert.Throws<ArgumentNullException>(() => invoiceCalculator.CalculateInvoice(null));
  }

  [Fact]
  public void ShouldThrowArgumentException_WhenPerformancesIsNull()
  {
    var invoice = new Invoice("BigCo", null);
    var invoiceCalculator = new InvoiceCalculator();
    Assert.Throws<ArgumentException>(() => invoiceCalculator.CalculateInvoice(invoice));
  }

  [Fact]
  public void ShouldThrowArgumentException_WhenCustomerNameIsNullOrEmpty()
  {
    var invoiceWithNullName = new Invoice(null, new List<Performance>());
    var invoiceWithEmptyName = new Invoice(string.Empty, new List<Performance>());

    var invoiceCalculator = new InvoiceCalculator();

    Assert.Throws<ArgumentException>(() => invoiceCalculator.CalculateInvoice(invoiceWithNullName));
    Assert.Throws<ArgumentException>(() => invoiceCalculator.CalculateInvoice(invoiceWithEmptyName));
  }
}