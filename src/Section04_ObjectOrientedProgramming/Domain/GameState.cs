using System;

using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Events;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain;

/// <summary>
/// ゲーム状態を管理するドメインモデル
///
/// ■ 責務
/// - プレイヤー・敵・スコア等のゲーム状態を保持
/// - ゲームフェーズ（未開始・実行中・終了）の管理
/// - 状態変更のイベント通知（オブザーバーパターン）
/// </summary>
public sealed class GameState
{

  /// <summary>ゲーム状態変更イベント</summary>
  public event EventHandler<GameStateChangedEventArgs>? StateChanged;

  /// <summary>ゲーム終了イベント</summary>
  public event EventHandler<GameOverEventArgs>? GameEnded;

  /// <summary>衝突検出イベント</summary>
  public event EventHandler<CollisionEventArgs>? CollisionDetected;

  /// <summary>ゲーム設定</summary>
  public GameSettings Settings { get; }

  /// <summary>プレイヤー</summary>
  public Player Player { get; }

  /// <summary>敵</summary>
  public Enemy Enemy { get; }

  /// <summary>現在のスコア</summary>
  public int Score { get; private set; }

  /// <summary>経過ティック数</summary>
  public int TickCount { get; private set; }

  /// <summary>ゲームフェーズ</summary>
  public GamePhase Phase { get; private set; }

  /// <summary>ゲームがアクティブか</summary>
  public bool IsActive => Phase == GamePhase.Running;

  public GameState(GameSettings settings, Player player, Enemy enemy)
  {
    Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    Player = player ?? throw new ArgumentNullException(nameof(player));
    Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
    Phase = GamePhase.NotStarted;
  }

  /// <summary>
  /// ゲームを開始する
  /// </summary>
  public void Start()
  {
    if (Phase != GamePhase.NotStarted)
      throw new InvalidOperationException("ゲームはすでに開始されています");

    Score = 0;
    TickCount = 0;
    Phase = GamePhase.Running;

    onStateChanged(GameStateChangeType.Initialized);
  }

  /// <summary>
  /// ゲームティックを進める
  /// </summary>
  public void Tick()
  {
    if (!IsActive)
      return;

    TickCount++;
    Score++;

    onStateChanged(GameStateChangeType.ScoreChanged, Score);
  }

  /// <summary>
  /// 衝突判定を実行
  /// </summary>
  public bool CheckCollision()
  {
    if (Player.CollidesWith(Enemy))
    {
      onCollisionDetected(Player, Enemy);
      endGame(GameOverReason.Collision);
      return true;
    }
    return false;
  }

  /// <summary>
  /// プレイヤーによるゲーム終了
  /// </summary>
  public void QuitByPlayer()
  {
    endGame(GameOverReason.PlayerQuit);
  }

  /// <summary>
  /// ゲームを一時停止
  /// </summary>
  public void Pause()
  {
    if (Phase != GamePhase.Running)
      return;
    Phase = GamePhase.Paused;
    onStateChanged(GameStateChangeType.Paused);
  }

  /// <summary>
  /// ゲームを再開
  /// </summary>
  public void Resume()
  {
    if (Phase != GamePhase.Paused)
      return;
    Phase = GamePhase.Running;
    onStateChanged(GameStateChangeType.Resumed);
  }

  private void endGame(GameOverReason reason)
  {
    if (Phase == GamePhase.Ended)
      return;

    Phase = GamePhase.Ended;
    onStateChanged(GameStateChangeType.GameOver);
    GameEnded?.Invoke(this, new GameOverEventArgs(Score, TickCount, reason));
  }

  private void onStateChanged(GameStateChangeType changeType, object? data = null)
  {
    StateChanged?.Invoke(this, new GameStateChangedEventArgs(changeType, data));
  }

  private void onCollisionDetected(Entity entity1, Entity entity2)
  {
    CollisionDetected?.Invoke(this, new CollisionEventArgs(entity1, entity2, entity1.Position));
    onStateChanged(GameStateChangeType.Collision);
  }
}

/// <summary>
/// ゲームフェーズ
/// </summary>
public enum GamePhase
{
  NotStarted,
  Running,
  Paused,
  Ended
}
