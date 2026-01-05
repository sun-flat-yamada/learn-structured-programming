using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Services;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Infrastructure.Input;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Infrastructure.Timing;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Presentation.Console;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus;

/// <summary>
/// LifeGame Plus - 生命の逃避行
///
/// オブジェクト指向設計のベストプラクティスを適用した版（拡張版）
///
/// 新機能:
/// - トカゲ（🦎）: ワニから逃げる仲間キャラクター
///   - 安全な時はランダムに歩く
///   - ワニが近づくと反対方向に逃げる
///   - さらに危険が迫ると尻尾を切り離して倍速で逃げる
///   - 尻尾はワニの囮になる
///
/// アーキテクチャ:
/// ┌──────────────────────────────────────────────────────────┐
/// │                    Presentation Layer                    │
/// │  (ConsoleGameRenderer)                                   │
/// ├──────────────────────────────────────────────────────────┤
/// │                    Application Layer                     │
/// │  (GameLoopService, GameFactory, Interfaces)              │
/// ├──────────────────────────────────────────────────────────┤
/// │                      Domain Layer                        │
/// │  (Entities, Behaviors, Events, GameState)                │
/// ├──────────────────────────────────────────────────────────┤
/// │                    Infrastructure Layer                  │
/// │  (ConsoleInputHandler, SystemGameClock)                  │
/// ├──────────────────────────────────────────────────────────┤
/// │                       Core Layer                         │
/// │  (Position, Direction, Bounds, GameSettings)             │
/// └──────────────────────────────────────────────────────────┘
///
/// 適用されたデザインパターン:
/// - 依存性注入 (DI): コンストラクタを通じた依存性の注入
/// - ストラテジーパターン: 移動行動の交換可能な実装
/// - ステートパターン: トカゲの状態管理
/// - ファクトリパターン: ゲームインスタンスの生成
/// - オブザーバーパターン: イベントによる状態変更通知
/// - ビルダーパターン: GameSettingsの構築
/// - 値オブジェクトパターン: Position, Bounds
/// - ファサードパターン: GameLoopServiceによる複雑さの隠蔽
///
/// SOLID原則:
/// - 単一責任の原則: 各クラスが一つの責任のみを持つ
/// - 開放/閉鎖原則: 新しい移動行動の追加が容易
/// - リスコフの置換原則: Entityの派生クラスが基底クラスとして扱える
/// - インターフェース分離の原則: 最小限のインターフェース定義
/// - 依存性逆転の原則: 高レベルモジュールが抽象に依存
/// </summary>
public sealed class LifeGame
{
  private readonly GameLoopService _gameLoop;

  /// <summary>
  /// デフォルト設定でLifeGameを作成
  /// </summary>
  public LifeGame() : this(GameSettings.Default())
  {
  }

  /// <summary>
  /// カスタム設定でLifeGameを作成
  /// </summary>
  public LifeGame(GameSettings settings)
    : this(
        settings,
        new ConsoleGameRenderer(),
        new ConsoleInputHandler(),
        new SystemGameClock())
  {
  }

  /// <summary>
  /// 完全な依存性注入でLifeGameを作成
  /// </summary>
  public LifeGame(
    GameSettings settings,
    IGameRenderer renderer,
    IInputHandler inputHandler,
    IGameClock clock)
  {
    var factory = new GameFactory(renderer, inputHandler, clock);
    _gameLoop = factory.Create(settings);
  }

  /// <summary>
  /// ゲームを実行
  /// </summary>
  public void Run()
  {
    _gameLoop.Run();
  }
}
