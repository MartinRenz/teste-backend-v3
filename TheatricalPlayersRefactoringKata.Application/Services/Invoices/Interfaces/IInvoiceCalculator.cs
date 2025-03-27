using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;

public interface IInvoiceCalculator
{
  InvoiceResult CalculateInvoice(Invoice invoice);
}