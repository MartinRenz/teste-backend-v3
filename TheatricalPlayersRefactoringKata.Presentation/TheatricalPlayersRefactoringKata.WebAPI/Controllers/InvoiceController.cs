using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.WebAPI.Services.Interfaces;

namespace TheatricalPlayersRefactoringKata.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Invoice>> GetAllInvoices()
    {
        var invoices = _invoiceService.GetAllInvoices();
        return Ok(invoices);
    }
}