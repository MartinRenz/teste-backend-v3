using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Represents the total calculation of an invoice.
/// </summary>
public class InvoiceResult
{
    private decimal _totalAmount;
    private int _volumeCredits;
    private List<decimal> _amounts;
    private List<int> _credits;

    /// <summary>
    /// Total amount of the invoice.
    /// </summary>
    public decimal TotalAmount { get => _totalAmount; set => _totalAmount = value; }

    /// <summary>
    /// Credits of the invoice.
    /// </summary>
    public int VolumeCredits { get => _volumeCredits; set => _volumeCredits = value; }

    /// <summary>
    /// List of all amounts calculated.
    /// </summary>
    public List<decimal> Amounts { get => _amounts; set => _amounts = value; }

    /// <summary>
    /// List of all credits calculated.
    /// </summary>
    public List<int> Credits { get => _credits; set => _credits = value; }

    public InvoiceResult
    (
        decimal totalAmount,
        int volumeCredits,
        List<decimal> amounts,
        List<int> credits
    )
    {
        this._totalAmount = totalAmount;
        this._volumeCredits = volumeCredits;
        this._amounts = amounts;
        this._credits = credits;
    }
}