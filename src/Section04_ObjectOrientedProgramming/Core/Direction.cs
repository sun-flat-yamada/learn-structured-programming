namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core
{
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
  /// 方向から移動量への変換、位置への適用を提供します。
  /// </summary>
  public static class DirectionExtensions
  {
    /// <summary>
    /// 方向に対応する移動量を取得
    /// </summary>
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
  }
}
