using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.WebAPI.Services.Interfaces;
public interface IInvoiceService
{
  IEnumerable<Invoice> GetAllInvoices();
}