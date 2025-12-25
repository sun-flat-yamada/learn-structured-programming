using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events
{
  /// <summary>
  /// ゲーム状態変更の種類
  /// </summary>
  public enum GameStateChangeType
  {
    Initialized,
    ScoreChanged,
    PlayerMoved,
    EnemyMoved,
    LizardMoved,
    Collision,
    PlayerCaught,
    LizardCaught,
    TailDropped,
    TailEaten,
    GameOver,
    Paused,
    Resumed
  }

  /// <summary>
  /// ゲーム状態変更イベントの引数
  /// </summary>
  public class GameStateChangedEventArgs : EventArgs
  {
    public GameStateChangeType ChangeType { get; }
    public object? Data { get; }

    public GameStateChangedEventArgs(GameStateChangeType changeType, object? data = null)
    {
      ChangeType = changeType;
      Data = data;
    }
  }

  /// <summary>
  /// ゲームオーバーイベントの引数
  /// </summary>
  public class GameOverEventArgs : EventArgs
  {
    /// <summary>最終スコア</summary>
    public int FinalScore { get; }

    /// <summary>生存時間（ティック数）</summary>
    public int SurvivalTicks { get; }

    /// <summary>ゲームオーバーの原因</summary>
    public GameOverReason Reason { get; }

    /// <summary>捕食されたエンティティ</summary>
    public Entity? CaughtEntity { get; }

    public GameOverEventArgs(int finalScore, int survivalTicks, GameOverReason reason, Entity? caughtEntity = null)
    {
      FinalScore = finalScore;
      SurvivalTicks = survivalTicks;
      Reason = reason;
      CaughtEntity = caughtEntity;
    }
  }

  /// <summary>
  /// ゲームオーバーの原因
  /// </summary>
  public enum GameOverReason
  {
    PlayerCaught,
    LizardCaught,
    AllCaught,
    PlayerQuit,
    TimeUp
  }

  /// <summary>
  /// 衝突イベントの引数
  /// </summary>
  public class CollisionEventArgs : EventArgs
  {
    public Entity Entity1 { get; }
    public Entity Entity2 { get; }
    public Position CollisionPosition { get; }
    public bool IsGameOver { get; }

    public CollisionEventArgs(Entity entity1, Entity entity2, Position collisionPosition, bool isGameOver = true)
    {
      Entity1 = entity1;
      Entity2 = entity2;
      CollisionPosition = collisionPosition;
      IsGameOver = isGameOver;
    }
  }

  /// <summary>
  /// 尻尾関連イベントの引数
  /// </summary>
  public class TailEventArgs : EventArgs
  {
    public Tail Tail { get; }
    public Lizard Lizard { get; }
    public TailEventType EventType { get; }

    public TailEventArgs(Tail tail, Lizard lizard, TailEventType eventType)
    {
      Tail = tail;
      Lizard = lizard;
      EventType = eventType;
    }
  }

  /// <summary>
  /// 尻尾イベントの種類
  /// </summary>
  public enum TailEventType
  {
    Dropped,
    Eaten
  }
}
