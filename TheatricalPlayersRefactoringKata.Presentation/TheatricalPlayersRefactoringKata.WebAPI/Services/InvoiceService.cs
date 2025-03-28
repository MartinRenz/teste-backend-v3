using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.WebAPI.Services.Interfaces;

namespace TheatricalPlayersRefactoringKata.WebAPI.Services
{
  public class InvoiceService : IInvoiceService
  {
    public IEnumerable<Invoice> GetAllInvoices()
    {
      throw new NotImplementedException();
    }

    public Invoice GetInvoiceByCustomerName(string customerName)
    {
      throw new NotImplementedException();
    }

    public Invoice CreateInvoice(Invoice newInvoice)
    {
      throw new NotImplementedException();
    }

    public bool DeleteInvoice(int invoiceId)
    {
      throw new NotImplementedException();
    }
  }
}