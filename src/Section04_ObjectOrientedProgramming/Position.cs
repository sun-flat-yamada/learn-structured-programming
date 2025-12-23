using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// 2D位置を表す値オブジェクト（Value Object）
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 値オブジェクトパターン: イミュータブルで等価性に基づく比較
  /// - 構造体: スタック割り当てによるメモリ効率
  /// - 不変性: 作成後の変更不可で副作用を防止
  /// </summary>
  public readonly struct Position : IEquatable<Position>
  {
    /// <summary>
    /// X座標
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Y座標
    /// </summary>
    public int Y { get; }

    public Position(int x, int y)
    {
      X = x;
      Y = y;
    }

    /// <summary>
    /// 新しい位置を返す（現在の位置に移動量を加算）
    /// </summary>
    public Position Move(int deltaX, int deltaY)
    {
      return new Position(X + deltaX, Y + deltaY);
    }

    /// <summary>
    /// 上方向に移動した新しい位置を返す
    /// </summary>
    public Position MoveUp() => Move(0, -1);

    /// <summary>
    /// 下方向に移動した新しい位置を返す
    /// </summary>
    public Position MoveDown() => Move(0, 1);

    /// <summary>
    /// 左方向に移動した新しい位置を返す
    /// </summary>
    public Position MoveLeft() => Move(-1, 0);

    /// <summary>
    /// 右方向に移動した新しい位置を返す
    /// </summary>
    public Position MoveRight() => Move(1, 0);

    /// <summary>
    /// 別の位置との衝突判定
    /// </summary>
    public bool CollidesWith(Position other)
    {
      return X == other.X && Y == other.Y;
    }

    /// <summary>
    /// ターゲット位置に向かう方向を計算
    /// </summary>
    public Position MoveTowards(Position target)
    {
      if (X < target.X)
      {
        return MoveRight();
      }
      else if (X > target.X)
      {
        return MoveLeft();
      }
      else if (Y < target.Y)
      {
        return MoveDown();
      }
      else if (Y > target.Y)
      {
        return MoveUp();
      }
      return this;
    }

    #region Equality Members

    public bool Equals(Position other)
    {
      return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
      return obj is Position other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Position left, Position right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Position left, Position right)
    {
      return !left.Equals(right);
    }

    #endregion

    public override string ToString()
    {
      return $"({X}, {Y})";
    }
  }
}

