using System;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Behaviors;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities
{
  /// <summary>
  /// プレイヤーキャラクター（カメ）
  ///
  /// ■ 責務
  /// プレイヤーの移動操作を提供します。
  /// ユーザー入力に応じた4方向移動および
  /// 入力がない場合のデフォルト移動をサポートします。
  /// </summary>
  public sealed class Player : Entity
  {
    private readonly IMovementBehavior _defaultMovement;

    public override string DisplayName => "カメ";
    public override string Emoji => "🐢";
    public override ConsoleColor Color => ConsoleColor.Green;

    public Player(Position initialPosition, Bounds bounds, IMovementBehavior? defaultMovement = null)
      : base(initialPosition, bounds)
    {
      _defaultMovement = defaultMovement ?? new RandomMovementBehavior();
    }

    /// <summary>
    /// プレイヤーを上に移動
    /// </summary>
    public bool MoveUp() => TryMove(Direction.Up);

    /// <summary>
    /// プレイヤーを下に移動
    /// </summary>
    public bool MoveDown() => TryMove(Direction.Down);

    /// <summary>
    /// プレイヤーを左に移動
    /// </summary>
    public bool MoveLeft() => TryMove(Direction.Left);

    /// <summary>
    /// プレイヤーを右に移動
    /// </summary>
    public bool MoveRight() => TryMove(Direction.Right);

    /// <summary>
    /// デフォルトの移動（入力がない場合のランダム移動）
    /// </summary>
    public void PerformDefaultMove()
    {
      var direction = _defaultMovement.DetermineDirection(Position, Position);
      TryMove(direction);
    }
  }
}
