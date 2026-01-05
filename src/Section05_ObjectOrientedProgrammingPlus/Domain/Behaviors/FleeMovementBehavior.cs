using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Behaviors;

/// <summary>
/// 逃走移動行動
///
/// ■ 責務
/// ターゲットから逃げる方向に移動する戦略を実装します。
/// 追跡の反対方向へX軸優先で移動します。
/// </summary>
public sealed class FleeMovementBehavior : IMovementBehavior
{
  public Direction DetermineDirection(Position currentPosition, Position targetPosition)
  {
    // X軸方向を優先（ターゲットの反対方向へ）
    if (currentPosition.X < targetPosition.X)
      return Direction.Left;
    if (currentPosition.X > targetPosition.X)
      return Direction.Right;

    // Y軸方向（ターゲットの反対方向へ）
    if (currentPosition.Y < targetPosition.Y)
      return Direction.Up;
    if (currentPosition.Y > targetPosition.Y)
      return Direction.Down;

    return Direction.None;
  }
}
