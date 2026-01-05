using System;

using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Events;

/// <summary>
/// ゲーム状態変更の種類
/// </summary>
public enum GameStateChangeType
{
  Initialized,
  ScoreChanged,
  PlayerMoved,
  EnemyMoved,
  Collision,
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

  public GameOverEventArgs(int finalScore, int survivalTicks, GameOverReason reason)
  {
    FinalScore = finalScore;
    SurvivalTicks = survivalTicks;
    Reason = reason;
  }
}

/// <summary>
/// ゲームオーバーの原因
/// </summary>
public enum GameOverReason
{
  Collision,
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

  public CollisionEventArgs(Entity entity1, Entity entity2, Position collisionPosition)
  {
    Entity1 = entity1;
    Entity2 = entity2;
    CollisionPosition = collisionPosition;
  }
}
