using System;

using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Behaviors;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

/// <summary>
/// 敵キャラクター（ワニ）
///
/// ■ 責務
/// プレイヤーやトカゲを追跡する敵の移動ロジックを提供します。
/// IMovementBehaviorにより移動戦略の切り替えが可能です。
/// </summary>
public sealed class Enemy : Entity
{
  private readonly IMovementBehavior _movementBehavior;

  public override string DisplayName => "ワニ";
  public override string Emoji => "🐊";
  public override ConsoleColor Color => ConsoleColor.Red;

  public Enemy(Position initialPosition, Bounds bounds, IMovementBehavior? movementBehavior = null)
    : base(initialPosition, bounds)
  {
    _movementBehavior = movementBehavior ?? new ChaseMovementBehavior();
  }

  /// <summary>
  /// ターゲットに向かって移動
  /// </summary>
  public void MoveTowards(Position target)
  {
    var direction = _movementBehavior.DetermineDirection(Position, target);
    TryMove(direction);
  }
}
