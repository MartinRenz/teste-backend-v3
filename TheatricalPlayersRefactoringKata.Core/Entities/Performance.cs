namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Represents a theater performance.
/// </summary>
public class Performance
{
    private Play _play;
    private int _audience;

    /// <summary>
    /// Theatrical play.
    /// </summary>
    public Play Play { get => _play; set => _play = value; }

    /// <summary>
    /// Audience number at the performance.
    /// </summary>
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        this._play = play;
        this._audience = audience;
    }
}