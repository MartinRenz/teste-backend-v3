using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Represents an invoice for a theatrical play.
/// </summary>
public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    /// <summary>
    /// Customer name associated with invoice.
    /// </summary>
    public string Customer { get => _customer; set => _customer = value; }

    /// <summary>
    /// List of performances associated with the customer.
    /// </summary>
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }
}