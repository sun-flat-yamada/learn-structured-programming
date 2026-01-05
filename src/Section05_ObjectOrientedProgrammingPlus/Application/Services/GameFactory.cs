using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Services;

/// <summary>
/// ゲームインスタンスを構築するファクトリ
///
/// ■ 責務
/// プレイヤー・敵・トカゲ・ゲーム状態等の
/// オブジェクト生成と組み立てを隠蔽します。
/// </summary>
public sealed class GameFactory
{
  private readonly IGameRenderer _renderer;
  private readonly IInputHandler _inputHandler;
  private readonly IGameClock _clock;

  public GameFactory(IGameRenderer renderer, IInputHandler inputHandler, IGameClock clock)
  {
    _renderer = renderer;
    _inputHandler = inputHandler;
    _clock = clock;
  }

  /// <summary>
  /// デフォルト設定でゲームを作成
  /// </summary>
  public GameLoopService Create() => Create(GameSettings.Default());

  /// <summary>
  /// 指定設定でゲームを作成
  /// </summary>
  public GameLoopService Create(GameSettings settings)
  {
    var player = new Player(settings.InitialPlayerPosition, settings.BoardBounds);
    var enemy = new Enemy(settings.InitialEnemyPosition, settings.BoardBounds);
    var lizard = new Lizard(
      settings.InitialLizardPosition,
      settings.BoardBounds,
      settings.LizardFleeDistance,
      settings.LizardTailDropDistance);
    var gameState = new GameState(settings, player, enemy, lizard);

    return new GameLoopService(gameState, _renderer, _inputHandler, _clock);
  }
}
