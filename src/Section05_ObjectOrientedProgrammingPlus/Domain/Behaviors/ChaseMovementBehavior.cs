using System;

using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Behaviors;

/// <summary>
/// 追跡移動行動
///
/// ■ 責務
/// ターゲットに向かって直線的に追跡する戦略を実装します。
/// 距離差が大きい軸を優先して移動します。
/// </summary>
public sealed class ChaseMovementBehavior : IMovementBehavior
{
  public Direction DetermineDirection(Position currentPosition, Position targetPosition)
  {
    var deltaX = targetPosition.X - currentPosition.X;
    var deltaY = targetPosition.Y - currentPosition.Y;

    // 距離差が大きい軸を優先して移動（絶対距離を効率的に詰める）
    if (Math.Abs(deltaX) >= Math.Abs(deltaY))
    {
      // X軸の差が大きいか同じ場合はX軸を優先
      if (deltaX > 0)
        return Direction.Right;
      if (deltaX < 0)
        return Direction.Left;

      // X軸の差がない場合はY軸
      if (deltaY > 0)
        return Direction.Down;
      if (deltaY < 0)
        return Direction.Up;
    }
    else
    {
      // Y軸の差が大きい場合はY軸を優先
      if (deltaY > 0)
        return Direction.Down;
      if (deltaY < 0)
        return Direction.Up;

      // Y軸の差がない場合はX軸
      if (deltaX > 0)
        return Direction.Right;
      if (deltaX < 0)
        return Direction.Left;
    }

    return Direction.None;
  }
}
