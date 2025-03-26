namespace TheatricalPlayersRefactoringKata.Core.Entities;

/// <summary>
/// Representa uma performance de teatro.
/// </summary>
public class Performance
{
    private Play _play;
    private int _audience;

    /// <summary>
    /// Peça de teatro.
    /// </summary>
    public Play Play { get => _play; set => _play = value; }

    /// <summary>
    /// Número de audiência na performance.
    /// </summary>
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        this._play = play;
        this._audience = audience;
    }
}