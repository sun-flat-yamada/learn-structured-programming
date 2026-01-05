using System;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

/// <summary>
/// 2D位置を表す値オブジェクト
///
/// ■ 値オブジェクトパターン
/// - イミュータブル（不変）: 作成後の変更不可
/// - 等価性で比較: 同じ座標なら同じオブジェクト
/// - struct: スタック割り当てでメモリ効率化
///
/// ■ 追加機能（Section05）
/// - MoveAwayFrom: ターゲットから逃げる方向へ移動
/// - DistanceTo: マンハッタン距離の計算
/// </summary>
public readonly struct Position : IEquatable<Position>
{
  public int X { get; }
  public int Y { get; }

  public Position(int x, int y)
  {
    X = x;
    Y = y;
  }

  /// <summary>
  /// 指定した移動量だけ移動した新しい位置を返す
  /// </summary>
  public Position Move(int deltaX, int deltaY) => new(X + deltaX, Y + deltaY);

  /// <summary>上方向に移動した新しい位置を返す</summary>
  public Position MoveUp() => Move(0, -1);

  /// <summary>下方向に移動した新しい位置を返す</summary>
  public Position MoveDown() => Move(0, 1);

  /// <summary>左方向に移動した新しい位置を返す</summary>
  public Position MoveLeft() => Move(-1, 0);

  /// <summary>右方向に移動した新しい位置を返す</summary>
  public Position MoveRight() => Move(1, 0);

  /// <summary>
  /// ターゲット位置に向かって1歩移動した新しい位置を返す
  /// </summary>
  public Position MoveTowards(Position target)
  {
    if (X < target.X)
      return MoveRight();
    if (X > target.X)
      return MoveLeft();
    if (Y < target.Y)
      return MoveDown();
    if (Y > target.Y)
      return MoveUp();
    return this;
  }

  /// <summary>
  /// ターゲット位置から逃げる方向に1歩移動した新しい位置を返す
  /// </summary>
  public Position MoveAwayFrom(Position target)
  {
    if (X < target.X)
      return MoveLeft();
    if (X > target.X)
      return MoveRight();
    if (Y < target.Y)
      return MoveUp();
    if (Y > target.Y)
      return MoveDown();
    return this;
  }

  /// <summary>
  /// マンハッタン距離を計算
  /// </summary>
  public int DistanceTo(Position other) =>
    Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

  #region Equality Members

  public bool Equals(Position other) => X == other.X && Y == other.Y;

  public override bool Equals(object? obj) => obj is Position other && Equals(other);

  public override int GetHashCode() => HashCode.Combine(X, Y);

  public static bool operator ==(Position left, Position right) => left.Equals(right);

  public static bool operator !=(Position left, Position right) => !left.Equals(right);

  #endregion

  public override string ToString() => $"({X}, {Y})";
}
