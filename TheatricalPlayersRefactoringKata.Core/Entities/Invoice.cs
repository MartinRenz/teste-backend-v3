using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Representa uma fatura de uma peça teatral.
/// </summary>
public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    /// <summary>
    /// Nome do cliente associado a fatura.
    /// </summary>
    public string Customer { get => _customer; set => _customer = value; }

    /// <summary>
    /// Lista de performances associadas ao cliente.
    /// </summary>
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }
}