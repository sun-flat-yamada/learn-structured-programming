using System;

using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain;

/// <summary>
/// ゲーム状態を管理するドメインモデル
///
/// ■ 責務
/// - プレイヤー・敵・トカゲ・スコア等のゲーム状態を保持
/// - ゲームフェーズ（未開始・実行中・終了）の管理
/// - 状態変更のイベント通知（オブザーバーパターン）
///
/// ■ 追加機能（Section05）
/// - Lizardの管理と尻尾イベント
/// - プレイヤー・トカゲそれぞれの生存状態追跡
/// </summary>
public sealed class GameState
{

  /// <summary>ゲーム状態変更イベント</summary>
  public event EventHandler<GameStateChangedEventArgs>? StateChanged;

  /// <summary>ゲーム終了イベント</summary>
  public event EventHandler<GameOverEventArgs>? GameEnded;

  /// <summary>衝突検出イベント</summary>
  public event EventHandler<CollisionEventArgs>? CollisionDetected;

  /// <summary>尻尾イベント</summary>
  public event EventHandler<TailEventArgs>? TailEvent;

  /// <summary>ゲーム設定</summary>
  public GameSettings Settings { get; }

  /// <summary>プレイヤー</summary>
  public Player Player { get; }

  /// <summary>敵</summary>
  public Enemy Enemy { get; }

  /// <summary>トカゲ</summary>
  public Lizard Lizard { get; }

  /// <summary>現在のスコア</summary>
  public int Score { get; private set; }

  /// <summary>経過ティック数</summary>
  public int TickCount { get; private set; }

  /// <summary>ゲームフェーズ</summary>
  public GamePhase Phase { get; private set; }

  /// <summary>ゲームがアクティブか</summary>
  public bool IsActive => Phase == GamePhase.Running;

  /// <summary>食べられた尻尾の数</summary>
  public int TailsEaten { get; private set; }

  /// <summary>プレイヤー（カメ）が生存しているか</summary>
  public bool IsPlayerAlive { get; private set; } = true;

  /// <summary>トカゲが生存しているか</summary>
  public bool IsLizardAlive { get; private set; } = true;

  public GameState(GameSettings settings, Player player, Enemy enemy, Lizard lizard)
  {
    Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    Player = player ?? throw new ArgumentNullException(nameof(player));
    Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
    Lizard = lizard ?? throw new ArgumentNullException(nameof(lizard));
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
    TailsEaten = 0;
    IsPlayerAlive = true;
    IsLizardAlive = true;
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
  public bool CheckCollisions()
  {
    if (!IsActive)
      return false;

    bool collisionOccurred = false;

    // プレイヤーとワニの衝突判定
    if (IsPlayerAlive && Player.CollidesWith(Enemy))
    {
      IsPlayerAlive = false;
      onCollisionDetected(Player, Enemy, false);
      onStateChanged(GameStateChangeType.PlayerCaught, Player);
      collisionOccurred = true;
    }

    // トカゲとワニの衝突判定
    if (IsLizardAlive && Lizard.CollidesWith(Enemy))
    {
      IsLizardAlive = false;
      onCollisionDetected(Lizard, Enemy, false);
      onStateChanged(GameStateChangeType.LizardCaught, Lizard);
      collisionOccurred = true;
    }

    // 両方が捕食されたらゲームオーバー
    if (!IsPlayerAlive && !IsLizardAlive)
    {
      endGame(GameOverReason.AllCaught);
      return true;
    }

    // 尻尾とワニの衝突判定（ゲームオーバーにはならない）
    var tail = Lizard.DroppedTail;
    if (tail != null && tail.IsActive && tail.CollidesWith(Enemy))
    {
      onTailEaten(tail);
    }

    return collisionOccurred;
  }

  /// <summary>
  /// 尻尾が落とされたことを通知
  /// </summary>
  public void NotifyTailDropped()
  {
    var tail = Lizard.DroppedTail;
    if (tail != null)
    {
      TailEvent?.Invoke(this, new TailEventArgs(tail, Lizard, TailEventType.Dropped));
      onStateChanged(GameStateChangeType.TailDropped, tail);
    }
  }

  private void onTailEaten(Tail tail)
  {
    Lizard.NotifyTailEaten();
    TailsEaten++;
    TailEvent?.Invoke(this, new TailEventArgs(tail, Lizard, TailEventType.Eaten));
    onStateChanged(GameStateChangeType.TailEaten, tail);
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

  private void endGame(GameOverReason reason, Entity? caughtEntity = null)
  {
    if (Phase == GamePhase.Ended)
      return;

    Phase = GamePhase.Ended;
    onStateChanged(GameStateChangeType.GameOver);
    GameEnded?.Invoke(this, new GameOverEventArgs(Score, TickCount, reason, caughtEntity));
  }

  private void onStateChanged(GameStateChangeType changeType, object? data = null)
  {
    StateChanged?.Invoke(this, new GameStateChangedEventArgs(changeType, data));
  }

  private void onCollisionDetected(Entity entity1, Entity entity2, bool isGameOver)
  {
    CollisionDetected?.Invoke(this, new CollisionEventArgs(entity1, entity2, entity1.Position, isGameOver));
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
