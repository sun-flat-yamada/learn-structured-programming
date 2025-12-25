using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities
{
  /// <summary>
  /// ゲームエンティティの基底クラス
  ///
  /// ■ 責務
  /// すべてのゲームオブジェクト（プレイヤー、敵、トカゲ等）の
  /// 共通機能（位置管理、移動、表示属性）を定義します。
  ///
  /// ■ 追加機能（Section05）
  /// - IsActive: エンティティの有効状態を示すプロパティ
  /// </summary>
  public abstract class Entity
  {
    private Position _position;
    private readonly Bounds _bounds;

    /// <summary>現在位置</summary>
    public Position Position => _position;

    /// <summary>表示名</summary>
    public abstract string DisplayName { get; }

    /// <summary>絵文字表現</summary>
    public abstract string Emoji { get; }

    /// <summary>表示色</summary>
    public abstract ConsoleColor Color { get; }

    /// <summary>エンティティがアクティブか（ゲーム上で有効か）</summary>
    public virtual bool IsActive => true;

    protected Entity(Position initialPosition, Bounds bounds)
    {
      _bounds = bounds;
      _position = bounds.Contains(initialPosition)
        ? initialPosition
        : throw new ArgumentOutOfRangeException(nameof(initialPosition));
    }

    /// <summary>
    /// 指定方向に移動を試みる
    /// </summary>
    public bool TryMove(Direction direction)
    {
      var newPosition = direction.ApplyTo(_position);
      return TryMoveTo(newPosition);
    }

    /// <summary>
    /// 指定位置への移動を試みる
    /// </summary>
    protected bool TryMoveTo(Position newPosition)
    {
      if (!_bounds.Contains(newPosition))
        return false;

      _position = newPosition;
      return true;
    }

    /// <summary>
    /// 別のエンティティと衝突しているか判定
    /// </summary>
    public bool CollidesWith(Entity other) =>
      _position == other._position;

    /// <summary>
    /// 別の位置との距離を計算
    /// </summary>
    public int DistanceTo(Entity other) =>
      _position.DistanceTo(other._position);

    /// <summary>
    /// 境界情報を取得
    /// </summary>
    protected Bounds GetBounds() => _bounds;
  }
}
