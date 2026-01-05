namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

/// <summary>
/// 移動方向を表す列挙型
/// </summary>
public enum Direction
{
  None,
  Up,
  Down,
  Left,
  Right
}

/// <summary>
/// Direction用の拡張メソッド
///
/// ■ 責務
/// 方向から移動量への変換、位置への適用、反対方向取得を提供します。
/// </summary>
public static class DirectionExtensions
{
  public static (int DeltaX, int DeltaY) ToMovement(this Direction direction) =>
    direction switch
    {
      Direction.Up => (0, -1),
      Direction.Down => (0, 1),
      Direction.Left => (-1, 0),
      Direction.Right => (1, 0),
      _ => (0, 0)
    };

  /// <summary>
  /// 位置を指定方向に移動
  /// </summary>
  public static Position ApplyTo(this Direction direction, Position position)
  {
    var (deltaX, deltaY) = direction.ToMovement();
    return position.Move(deltaX, deltaY);
  }

  /// <summary>
  /// 反対方向を取得
  /// </summary>
  public static Direction Opposite(this Direction direction) =>
    direction switch
    {
      Direction.Up => Direction.Down,
      Direction.Down => Direction.Up,
      Direction.Left => Direction.Right,
      Direction.Right => Direction.Left,
      _ => Direction.None
    };
}
