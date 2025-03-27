namespace TheatricalPlayersRefactoringKata.Application.Services.Interfaces;

public interface IInvoiceCalculator
{
  (decimal TotalAmount, int VolumeCredits) CalculateStatement(Invoice invoice);
}