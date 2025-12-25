using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Services
{
  /// <summary>
  /// ゲームインスタンスを生成するファクトリ
  ///
  /// ■ 責務
  /// ゲームループに必要なオブジェクト群を生成・組み立てます。
  ///
  /// ■ ファクトリパターンの利点
  /// - オブジェクト生成の複雑さを隠蔽
  /// - 依存関係の組み立てを一元化
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
    /// デフォルト設定でゲームを生成
    /// </summary>
    public GameLoopService Create() => Create(GameSettings.Default());

    /// <summary>
    /// 指定設定でゲームを生成
    /// </summary>
    public GameLoopService Create(GameSettings settings)
    {
      var player = new Player(settings.InitialPlayerPosition, settings.BoardBounds);
      var enemy = new Enemy(settings.InitialEnemyPosition, settings.BoardBounds);
      var gameState = new GameState(settings, player, enemy);

      return new GameLoopService(gameState, _renderer, _inputHandler, _clock);
    }
  }
}
