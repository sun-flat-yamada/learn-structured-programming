using System;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Events;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain
{
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
    private int _score;
    private int _tickCount;
    private GamePhase _phase;

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
    public int Score => _score;

    /// <summary>経過ティック数</summary>
    public int TickCount => _tickCount;

    /// <summary>ゲームフェーズ</summary>
    public GamePhase Phase => _phase;

    /// <summary>ゲームがアクティブか</summary>
    public bool IsActive => _phase == GamePhase.Running;

    public GameState(GameSettings settings, Player player, Enemy enemy)
    {
      Settings = settings ?? throw new ArgumentNullException(nameof(settings));
      Player = player ?? throw new ArgumentNullException(nameof(player));
      Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
      _phase = GamePhase.NotStarted;
    }

    /// <summary>
    /// ゲームを開始する
    /// </summary>
    public void Start()
    {
      if (_phase != GamePhase.NotStarted)
        throw new InvalidOperationException("ゲームはすでに開始されています");

      _score = 0;
      _tickCount = 0;
      _phase = GamePhase.Running;

      OnStateChanged(GameStateChangeType.Initialized);
    }

    /// <summary>
    /// ゲームティックを進める
    /// </summary>
    public void Tick()
    {
      if (!IsActive) return;

      _tickCount++;
      _score++;

      OnStateChanged(GameStateChangeType.ScoreChanged, _score);
    }

    /// <summary>
    /// 衝突判定を実行
    /// </summary>
    public bool CheckCollision()
    {
      if (Player.CollidesWith(Enemy))
      {
        OnCollisionDetected(Player, Enemy);
        EndGame(GameOverReason.Collision);
        return true;
      }
      return false;
    }

    /// <summary>
    /// プレイヤーによるゲーム終了
    /// </summary>
    public void QuitByPlayer()
    {
      EndGame(GameOverReason.PlayerQuit);
    }

    /// <summary>
    /// ゲームを一時停止
    /// </summary>
    public void Pause()
    {
      if (_phase != GamePhase.Running) return;
      _phase = GamePhase.Paused;
      OnStateChanged(GameStateChangeType.Paused);
    }

    /// <summary>
    /// ゲームを再開
    /// </summary>
    public void Resume()
    {
      if (_phase != GamePhase.Paused) return;
      _phase = GamePhase.Running;
      OnStateChanged(GameStateChangeType.Resumed);
    }

    private void EndGame(GameOverReason reason)
    {
      if (_phase == GamePhase.Ended) return;

      _phase = GamePhase.Ended;
      OnStateChanged(GameStateChangeType.GameOver);
      GameEnded?.Invoke(this, new GameOverEventArgs(_score, _tickCount, reason));
    }

    private void OnStateChanged(GameStateChangeType changeType, object? data = null)
    {
      StateChanged?.Invoke(this, new GameStateChangedEventArgs(changeType, data));
    }

    private void OnCollisionDetected(Entity entity1, Entity entity2)
    {
      CollisionDetected?.Invoke(this, new CollisionEventArgs(entity1, entity2, entity1.Position));
      OnStateChanged(GameStateChangeType.Collision);
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
}
