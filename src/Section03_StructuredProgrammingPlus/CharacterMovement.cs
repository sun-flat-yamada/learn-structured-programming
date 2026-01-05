using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus;

/// <summary>
/// キャラクター移動ロジックを提供する静的クラス（2D対応）
///
/// ■ 責務
/// カメとワニの2D移動処理を担当します。
///
/// ■ 移動パターン
/// - カメ: 上下左右ランダム移動（25%ずつ）
/// - ワニ: X軸優先で2D追跡
/// </summary>
public static class CharacterMovement
{
  private static readonly Random _random = new();

  /// <summary>
  /// カメを4方向にランダム移動（入力がない場合）
  /// </summary>
  public static void MoveTurtleRandomly()
  {
    // 毎フレーム新しいランダム値を生成
    int randomValue = _random.Next(100);

    // 4方向に25%ずつの確率で移動
    int newX = GameState.TurtlePositionX;
    int newY = GameState.TurtlePositionY;

    if (randomValue < 25)
    {
      newY = GameState.TurtlePositionY - 1;  // 上
    }
    else if (randomValue < 50)
    {
      newY = GameState.TurtlePositionY + 1;  // 下
    }
    else if (randomValue < 75)
    {
      newX = GameState.TurtlePositionX - 1;  // 左
    }
    else
    {
      newX = GameState.TurtlePositionX + 1;  // 右
    }

    // 境界チェック後に移動
    if (GameRules.IsWithinBoundsX(newX) && GameRules.IsWithinBoundsY(newY))
    {
      GameState.TurtlePositionX = newX;
      GameState.TurtlePositionY = newY;
    }
  }

  /// <summary>
  /// ワニをカメに向かって2D追跡（X軸優先）
  /// </summary>
  public static void MoveCrocodileTowardsTurtle()
  {
    // X軸の差を先に詰める
    if (GameState.CrocodilePositionX < GameState.TurtlePositionX)
    {
      GameState.CrocodilePositionX++;
    }
    else if (GameState.CrocodilePositionX > GameState.TurtlePositionX)
    {
      GameState.CrocodilePositionX--;
    }
    // Xが同じならYを詰める
    else if (GameState.CrocodilePositionY < GameState.TurtlePositionY)
    {
      GameState.CrocodilePositionY++;
    }
    else if (GameState.CrocodilePositionY > GameState.TurtlePositionY)
    {
      GameState.CrocodilePositionY--;
    }
  }

  /// <summary>
  /// カメを指定方向に移動（ユーザー入力用）
  /// </summary>
  /// <param name="directionX">X方向移動量</param>
  /// <param name="directionY">Y方向移動量</param>
  public static void MoveTurtle(int directionX, int directionY)
  {
    int newX = GameState.TurtlePositionX + directionX;
    int newY = GameState.TurtlePositionY + directionY;
    if (GameRules.IsWithinBoundsX(newX) && GameRules.IsWithinBoundsY(newY))
    {
      GameState.TurtlePositionX = newX;
      GameState.TurtlePositionY = newY;
    }
  }
}
