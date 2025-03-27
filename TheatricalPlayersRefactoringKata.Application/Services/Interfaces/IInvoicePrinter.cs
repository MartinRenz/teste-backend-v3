namespace TheatricalPlayersRefactoringKata.Application.Services.Interfaces;

public interface IInvoicePrinter
{
    string Print(Invoice invoice);
}