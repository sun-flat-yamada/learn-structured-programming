using System;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core
{
  /// <summary>
  /// ゲーム盤の境界を表す値オブジェクト
  ///
  /// ■ 責務
  /// ゲーム盤の幅・高さと境界判定ロジックをカプセル化します。
  ///
  /// ■ 値オブジェクト
  /// Positionと同様にイミュータブルな値オブジェクトです。
  /// </summary>
  public readonly struct Bounds : IEquatable<Bounds>
  {
    public int Width { get; }
    public int Height { get; }

    public Bounds(int width, int height)
    {
      if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "幅は0より大きい必要があります");
      if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "高さは0より大きい必要があります");

      Width = width;
      Height = height;
    }

    /// <summary>
    /// 位置が境界内にあるか判定
    /// </summary>
    public bool Contains(Position position) =>
      position.X >= 0 && position.X < Width &&
      position.Y >= 0 && position.Y < Height;

    /// <summary>
    /// 位置を境界内に制約（クランプ）
    /// </summary>
    public Position Clamp(Position position) =>
      new(
        Math.Clamp(position.X, 0, Width - 1),
        Math.Clamp(position.Y, 0, Height - 1)
      );

    /// <summary>
    /// 中心位置を取得
    /// </summary>
    public Position Center => new(Width / 2, Height / 2);

    #region Equality Members

    public bool Equals(Bounds other) => Width == other.Width && Height == other.Height;

    public override bool Equals(object? obj) => obj is Bounds other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Width, Height);

    public static bool operator ==(Bounds left, Bounds right) => left.Equals(right);

    public static bool operator !=(Bounds left, Bounds right) => !left.Equals(right);

    #endregion

    public override string ToString() => $"{Width}x{Height}";
  }
}
