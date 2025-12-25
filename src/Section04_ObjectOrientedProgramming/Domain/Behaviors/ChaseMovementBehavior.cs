using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Behaviors
{
  /// <summary>
  /// 追跡移動行動
  ///
  /// ■ 責務
  /// ターゲットに向かって直線的に追跡する戦略を実装します。
  /// X軸方向を優先して移動します。
  /// </summary>
  public sealed class ChaseMovementBehavior : IMovementBehavior
  {
    public Direction DetermineDirection(Position currentPosition, Position targetPosition)
    {
      // X軸方向を優先
      if (currentPosition.X < targetPosition.X)
        return Direction.Right;
      if (currentPosition.X > targetPosition.X)
        return Direction.Left;

      // Y軸方向
      if (currentPosition.Y < targetPosition.Y)
        return Direction.Down;
      if (currentPosition.Y > targetPosition.Y)
        return Direction.Up;

      return Direction.None;
    }
  }
}
