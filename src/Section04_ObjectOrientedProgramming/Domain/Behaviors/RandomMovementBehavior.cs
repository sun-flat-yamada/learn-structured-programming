using System;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Behaviors
{
  /// <summary>
  /// ランダム移動行動
  ///
  /// ■ 責務
  /// 4方向のいずれかにランダムに移動する戦略を実装します。
  /// </summary>
  public sealed class RandomMovementBehavior : IMovementBehavior
  {
    private static readonly Direction[] Directions =
    [
      Direction.Up,
      Direction.Down,
      Direction.Left,
      Direction.Right
    ];

    private readonly Random _random;

    public RandomMovementBehavior(Random? random = null)
    {
      _random = random ?? Random.Shared;
    }

    public Direction DetermineDirection(Position currentPosition, Position targetPosition)
    {
      return Directions[_random.Next(Directions.Length)];
    }
  }
}
