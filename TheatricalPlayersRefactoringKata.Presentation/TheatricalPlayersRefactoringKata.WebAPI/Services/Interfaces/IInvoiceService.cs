using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.WebAPI.Services.Interfaces;
public interface IInvoiceService
{
  IEnumerable<Invoice> GetAllInvoices();
  Invoice GetInvoiceByCustomerName(string customerName);
  Invoice CreateInvoice(Invoice newInvoice);
  bool DeleteInvoice(int invoiceId);
}