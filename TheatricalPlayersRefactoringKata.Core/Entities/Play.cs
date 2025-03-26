using System;
using TheatricalPlayersRefactoringKata.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Representa uma peça de teatro.
/// </summary>
public class Play
{
    private string _name;
    private int _lines;
    private PlayType _type;

    /// <summary>
    /// Nome da peça de teatro.
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Número de linhas no texto da peça de teatro.
    /// </summary>
    public int Lines
    {
        get => _lines;
        set => _lines = Math.Clamp(value, 1000, 4000);
    }

    /// <summary>
    /// Tipo de peça de teatro.
    /// </summary>
    public PlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, PlayType type)
    {
        this._name = name;
        this._lines = Math.Clamp(lines, 1000, 4000);
        this._type = type;
    }
}