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

    /// <summary>
    /// Search for all the invoices.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Invoice>> GetAllInvoices()
    {
        var invoices = _invoiceService.GetAllInvoices();

        return Ok(invoices);
    }

    /// <summary>
    /// Search for invoice by customer name.
    /// </summary>
    /// <param name="customerName"></param>
    [HttpGet("{customerName}")]
    public ActionResult<Invoice> GetInvoiceByCustomerName(string customerName)
    {
        var invoice = _invoiceService.GetInvoiceByCustomerName(customerName);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    /// <summary>
    /// Create an invoice.
    /// </summary>
    /// <param name="newInvoice"></param>
    [HttpPost]
    public ActionResult<Invoice> CreateInvoice([FromBody] Invoice newInvoice)
    {
        if (newInvoice == null)
        {
            return BadRequest("Invoice data is required.");
        }

        var createdInvoice = _invoiceService.CreateInvoice(newInvoice);

        return CreatedAtAction(nameof(GetInvoiceByCustomerName), new { customerName = createdInvoice.Customer }, createdInvoice);
    }

    /// <summary>
    /// Delete an invoice.
    /// </summary>
    /// <param name="invoiceId"></param>
    [HttpDelete("{invoiceId}")]
    public ActionResult DeleteInvoice(int invoiceId)
    {
        var success = _invoiceService.DeleteInvoice(invoiceId);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}