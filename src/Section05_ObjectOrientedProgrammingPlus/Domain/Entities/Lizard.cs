using System;
using System.Collections.Generic;
using System.Linq;

using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Behaviors;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

/// <summary>
/// トカゲキャラクター
///
/// ■ 責務
/// ワニから逃走するNPCキャラクターの行動を提供します。
///
/// ■ 行動パターン（ステートパターン）
/// - Wandering: 安全な時はランダムに歩く
/// - Fleeing: ワニが近づいたら反対方向に逃げる
/// - TailDropped: 尻尾を切り離して倍速で逃げる
/// </summary>
public sealed class Lizard : Entity
{
  private readonly IMovementBehavior _randomMovement;
  private readonly IMovementBehavior _fleeMovement;
  private readonly int _fleeDistance;
  private readonly int _tailDropDistance;
  private int _speedBoostTicks;
  private const int SPEED_BOOST_DURATION = 10; // 尻尾を落とした後の倍速継続ティック数

  public override string DisplayName => "トカゲ";
  public override string Emoji => "🦎";  // 常に🦎で表示
  public override ConsoleColor Color => ConsoleColor.Yellow;

  /// <summary>尻尾を持っているか</summary>
  public bool HasTail => State == LizardState.Wandering || State == LizardState.Fleeing;

  /// <summary>現在の状態</summary>
  public LizardState State { get; private set; } = LizardState.Wandering;

  /// <summary>切り離した尻尾</summary>
  public Tail? DroppedTail { get; private set; }

  /// <summary>倍速モードか</summary>
  public bool IsSpeedBoosted => _speedBoostTicks > 0;

  public Lizard(
    Position initialPosition,
    Bounds bounds,
    int fleeDistance = 8,
    int tailDropDistance = 4,
    IMovementBehavior? randomMovement = null,
    IMovementBehavior? fleeMovement = null)
    : base(initialPosition, bounds)
  {
    _fleeDistance = fleeDistance;
    _tailDropDistance = tailDropDistance;
    _randomMovement = randomMovement ?? new RandomMovementBehavior();
    _fleeMovement = fleeMovement ?? new FleeMovementBehavior();
  }

  /// <summary>
  /// 敵（ワニ）の位置を考慮して行動する
  /// </summary>
  public void Act(Position enemyPosition)
  {
    var distanceToEnemy = Position.DistanceTo(enemyPosition);

    // 状態遷移の判定
    updateState(distanceToEnemy);

    // 状態に応じた行動
    switch (State)
    {
      case LizardState.Wandering:
        wander();
        break;

      case LizardState.Fleeing:
        flee(enemyPosition);
        break;

      case LizardState.TailDropped:
        fleeWithSpeedBoost(enemyPosition);
        break;
    }
  }

  private void updateState(int distanceToEnemy)
  {
    switch (State)
    {
      case LizardState.Wandering:
        if (distanceToEnemy <= _tailDropDistance)
        {
          dropTail();
        }
        else if (distanceToEnemy <= _fleeDistance)
        {
          State = LizardState.Fleeing;
        }
        break;

      case LizardState.Fleeing:
        if (distanceToEnemy <= _tailDropDistance && HasTail)
        {
          dropTail();
        }
        else if (distanceToEnemy > _fleeDistance)
        {
          State = LizardState.Wandering;
        }
        break;

      case LizardState.TailDropped:
        // 尻尾を落とした状態は維持（尻尾は再生しない）
        if (_speedBoostTicks > 0)
        {
          _speedBoostTicks--;
        }
        // 倍速終了後も逃走モードは継続
        break;
    }
  }

  private void wander()
  {
    var direction = _randomMovement.DetermineDirection(Position, Position);
    tryMoveWithFallback(direction);
  }

  private void flee(Position enemyPosition)
  {
    var direction = _fleeMovement.DetermineDirection(Position, enemyPosition);
    fleeWithFallback(direction, enemyPosition);
  }

  private void fleeWithSpeedBoost(Position enemyPosition)
  {
    var direction = _fleeMovement.DetermineDirection(Position, enemyPosition);
    fleeWithFallback(direction, enemyPosition);

    // 倍速移動（同じ方向にもう一度移動）
    if (IsSpeedBoosted)
    {
      var nextDirection = _fleeMovement.DetermineDirection(Position, enemyPosition);
      fleeWithFallback(nextDirection, enemyPosition);
    }
  }

  /// <summary>
  /// 移動を試み、失敗したら代替方向を試す
  /// </summary>
  private void tryMoveWithFallback(Direction preferredDirection)
  {
    if (TryMove(preferredDirection))
      return;

    // 代替方向を試す
    var alternatives = getAlternativeDirections(preferredDirection);
    foreach (var alt in alternatives)
    {
      if (TryMove(alt))
        return;
    }
  }

  /// <summary>
  /// 逃走時に移動を試み、失敗したら広いスペース方向に逃げる
  /// </summary>
  private void fleeWithFallback(Direction preferredDirection, Position enemyPosition)
  {
    if (TryMove(preferredDirection))
      return;

    // 境界に到達した場合、敵から遠ざかる代替方向を試す
    var alternatives = getFleeAlternativeDirections(enemyPosition);
    foreach (var alt in alternatives)
    {
      if (TryMove(alt))
        return;
    }
  }

  /// <summary>
  /// 代替方向のリストを取得
  /// </summary>
  private static Direction[] getAlternativeDirections(Direction preferred)
  {
    return preferred switch
    {
      Direction.Left or Direction.Right => new[] { Direction.Up, Direction.Down },
      Direction.Up or Direction.Down => new[] { Direction.Left, Direction.Right },
      _ => new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right }
    };
  }

  /// <summary>
  /// 逃走時の代替方向を敵から遠ざかる順で取得
  /// </summary>
  private Direction[] getFleeAlternativeDirections(Position enemyPosition)
  {
    var bounds = GetBounds();
    var directions = new List<(Direction dir, int score)>();

    // 各方向の「広さスコア」を計算（敵から遠く、かつ移動可能なスペースが多い）
    foreach (var dir in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
    {
      var newPos = dir.ApplyTo(Position);
      if (!bounds.Contains(newPos))
        continue;

      // 敵からの距離をスコアとする
      var distanceFromEnemy = newPos.DistanceTo(enemyPosition);

      // 境界までの余裕も考慮（中央寄りを優先）
      var spaceScore = calculateSpaceScore(newPos, bounds);

      directions.Add((dir, distanceFromEnemy + spaceScore));
    }

    // スコアの高い順（敵から遠い＆広いスペースがある方向）
    return directions
      .OrderByDescending(d => d.score)
      .Select(d => d.dir)
      .ToArray();
  }

  /// <summary>
  /// 位置の周囲の広さスコアを計算
  /// </summary>
  private static int calculateSpaceScore(Position pos, Bounds bounds)
  {
    // 各方向への余裕を計算
    var leftSpace = pos.X;
    var rightSpace = bounds.Width - 1 - pos.X;
    var topSpace = pos.Y;
    var bottomSpace = bounds.Height - 1 - pos.Y;

    // 最小の余裕が大きいほど良い（角に追い詰められにくい）
    return Math.Min(Math.Min(leftSpace, rightSpace), Math.Min(topSpace, bottomSpace));
  }

  private void dropTail()
  {
    if (!HasTail)
      return;

    // トカゲの位置から5座標離れたランダムな場所に尻尾を生成
    // ビチビチ動いて気を引くため、遠くに飛ばす
    var tailPosition = generateRandomPositionAtDistance(Position, 5);
    DroppedTail = new Tail(tailPosition, GetBounds());
    State = LizardState.TailDropped;
    _speedBoostTicks = SPEED_BOOST_DURATION;
  }

  /// <summary>
  /// 指定位置から指定距離離れたランダムな位置を生成
  /// </summary>
  private Position generateRandomPositionAtDistance(Position origin, int distance)
  {
    var random = new Random();
    var bounds = GetBounds();

    // ランダムな角度（8方向）を選択
    var directions = new[]
    {
      (dx: distance, dy: 0),        // 右
      (dx: -distance, dy: 0),       // 左
      (dx: 0, dy: distance),        // 下
      (dx: 0, dy: -distance),       // 上
      (dx: distance, dy: distance), // 右下
      (dx: -distance, dy: distance),// 左下
      (dx: distance, dy: -distance),// 右上
      (dx: -distance, dy: -distance)// 左上
    };

    // ランダムに方向を選び、境界内に収まるまで試行
    var shuffledDirections = directions.OrderBy(_ => random.Next()).ToArray();

    foreach (var (dx, dy) in shuffledDirections)
    {
      var newPosition = new Position(origin.X + dx, origin.Y + dy);
      if (bounds.Contains(newPosition))
      {
        return newPosition;
      }
    }

    // 全方向が境界外の場合は、境界内にクランプ
    var fallbackPosition = new Position(
      origin.X + shuffledDirections[0].dx,
      origin.Y + shuffledDirections[0].dy
    );
    return bounds.Clamp(fallbackPosition);
  }

  /// <summary>
  /// 尻尾が捕食されたことを通知
  /// </summary>
  public void NotifyTailEaten()
  {
    DroppedTail?.OnEaten();
  }
}

/// <summary>
/// トカゲの状態
/// </summary>
public enum LizardState
{
  /// <summary>うろうろ歩いている（安全）</summary>
  Wandering,

  /// <summary>逃げている</summary>
  Fleeing,

  /// <summary>尻尾を落として逃げている</summary>
  TailDropped
}
