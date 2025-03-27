using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Invoices.Interfaces;

public interface IInvoicePrinter
{
    string Print(Invoice invoice);
}