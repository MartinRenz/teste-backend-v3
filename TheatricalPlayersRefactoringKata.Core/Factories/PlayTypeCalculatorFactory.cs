using System;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Core.Factories;

/// <summary>
/// Factory to provide instances of calculators based on the type of theatrical play.
/// </summary>
public static class PlayTypeCalculatorFactory
{
  /// <summary>
  /// Method that provides instance of calculator.
  /// </summary>
  /// <param name="playType"></param>
  public static IPlayTypeCalculator GetCalculator(PlayType playType)
  {
    return playType switch
    {
      PlayType.Comedy => new ComedyTypeCalculator(),
      PlayType.Tragedy => new TragedyTypeCalculator(),
      PlayType.History => new HistoryTypeCalculator(),
      _ => throw new ArgumentException($"Unknown play type: {playType}")
    };
  }
}