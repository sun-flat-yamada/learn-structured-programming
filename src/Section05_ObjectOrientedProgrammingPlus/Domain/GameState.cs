using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain
{
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
    private int _score;
    private int _tickCount;
    private GamePhase _phase;
    private int _tailsEaten;
    private bool _isPlayerAlive = true;
    private bool _isLizardAlive = true;

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
    public int Score => _score;

    /// <summary>経過ティック数</summary>
    public int TickCount => _tickCount;

    /// <summary>ゲームフェーズ</summary>
    public GamePhase Phase => _phase;

    /// <summary>ゲームがアクティブか</summary>
    public bool IsActive => _phase == GamePhase.Running;

    /// <summary>食べられた尻尾の数</summary>
    public int TailsEaten => _tailsEaten;

    /// <summary>プレイヤー（カメ）が生存しているか</summary>
    public bool IsPlayerAlive => _isPlayerAlive;

    /// <summary>トカゲが生存しているか</summary>
    public bool IsLizardAlive => _isLizardAlive;

    public GameState(GameSettings settings, Player player, Enemy enemy, Lizard lizard)
    {
      Settings = settings ?? throw new ArgumentNullException(nameof(settings));
      Player = player ?? throw new ArgumentNullException(nameof(player));
      Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
      Lizard = lizard ?? throw new ArgumentNullException(nameof(lizard));
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
      _tailsEaten = 0;
      _isPlayerAlive = true;
      _isLizardAlive = true;
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
    public bool CheckCollisions()
    {
      if (!IsActive) return false;

      bool collisionOccurred = false;

      // プレイヤーとワニの衝突判定
      if (_isPlayerAlive && Player.CollidesWith(Enemy))
      {
        _isPlayerAlive = false;
        OnCollisionDetected(Player, Enemy, false);
        OnStateChanged(GameStateChangeType.PlayerCaught, Player);
        collisionOccurred = true;
      }

      // トカゲとワニの衝突判定
      if (_isLizardAlive && Lizard.CollidesWith(Enemy))
      {
        _isLizardAlive = false;
        OnCollisionDetected(Lizard, Enemy, false);
        OnStateChanged(GameStateChangeType.LizardCaught, Lizard);
        collisionOccurred = true;
      }

      // 両方が捕食されたらゲームオーバー
      if (!_isPlayerAlive && !_isLizardAlive)
      {
        EndGame(GameOverReason.AllCaught);
        return true;
      }

      // 尻尾とワニの衝突判定（ゲームオーバーにはならない）
      var tail = Lizard.DroppedTail;
      if (tail != null && tail.IsActive && tail.CollidesWith(Enemy))
      {
        OnTailEaten(tail);
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
        OnStateChanged(GameStateChangeType.TailDropped, tail);
      }
    }

    private void OnTailEaten(Tail tail)
    {
      Lizard.NotifyTailEaten();
      _tailsEaten++;
      TailEvent?.Invoke(this, new TailEventArgs(tail, Lizard, TailEventType.Eaten));
      OnStateChanged(GameStateChangeType.TailEaten, tail);
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

    private void EndGame(GameOverReason reason, Entity? caughtEntity = null)
    {
      if (_phase == GamePhase.Ended) return;

      _phase = GamePhase.Ended;
      OnStateChanged(GameStateChangeType.GameOver);
      GameEnded?.Invoke(this, new GameOverEventArgs(_score, _tickCount, reason, caughtEntity));
    }

    private void OnStateChanged(GameStateChangeType changeType, object? data = null)
    {
      StateChanged?.Invoke(this, new GameStateChangedEventArgs(changeType, data));
    }

    private void OnCollisionDetected(Entity entity1, Entity entity2, bool isGameOver)
    {
      CollisionDetected?.Invoke(this, new CollisionEventArgs(entity1, entity2, entity1.Position, isGameOver));
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
