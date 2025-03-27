using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Calculators.Interfaces;

public interface IPlayTypeCalculator
{
  decimal CalculateAmount(Performance perf);
  int CalculateVolumeCredits(Performance perf);
}