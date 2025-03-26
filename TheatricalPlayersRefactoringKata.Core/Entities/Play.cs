using System;
using TheatricalPlayersRefactoringKata.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Represents a theatrical play.
/// </summary>
public class Play
{
    private string _name;
    private int _lines;
    private PlayType _type;

    /// <summary>
    /// Name of the play.
    /// </summary>
    public string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Number of lines in the plays text.
    /// </summary>
    public int Lines
    {
        get => _lines;
        set => _lines = Math.Clamp(value, 1000, 4000);
    }

    /// <summary>
    /// Type of play.
    /// </summary>
    public PlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, PlayType type)
    {
        this._name = name;
        this._lines = Math.Clamp(lines, 1000, 4000);
        this._type = type;
    }
}