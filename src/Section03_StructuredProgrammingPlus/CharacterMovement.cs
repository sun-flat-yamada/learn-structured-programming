using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のキャラクター移動処理関数（完全対応版）
  /// </summary>
  /// <remarks>
  /// 【Section03_StructuredProgrammingPlus について】
  /// このセクションは、32x32のマス目内を縦横自由に動くバージョンです。
  ///
  /// 【改修内容】カメを縦横自由にランダム移動させる
  ///
  /// - 移動方向の4方向分岐：上、下、左、右
  /// - 毎フレーム（各行描画のたびに）新しいランダム値を生成
  /// </remarks>
  public static class CharacterMovement
  {
    private static Random _random = new Random();

    /// <summary>
    /// カメをランダムに移動させる（ユーザー入力がない場合のデフォルト動作）
    /// 縦横自由に移動可能
    /// </summary>
    public static void MoveTurtleRandomly()
    {
      // 毎フレームで新しいランダム値を生成
      int randomValue = _random.Next(100);

      // 4方向の確率分岐：
      // - 0～24: 上に移動（25%の確率）
      // - 25～49: 下に移動（25%の確率）
      // - 50～74: 左に移動（25%の確率）
      // - 75～99: 右に移動（25%の確率）

      int newX = GameState.TurtlePositionX;
      int newY = GameState.TurtlePositionY;

      if (randomValue < 25)
      {
        // 上方向に1マス移動
        newY = GameState.TurtlePositionY - 1;
      }
      else if (randomValue < 50)
      {
        // 下方向に1マス移動
        newY = GameState.TurtlePositionY + 1;
      }
      else if (randomValue < 75)
      {
        // 左方向に1マス移動
        newX = GameState.TurtlePositionX - 1;
      }
      else
      {
        // 右方向に1マス移動
        newX = GameState.TurtlePositionX + 1;
      }

      // 移動後の位置がゲーム領域内であることを確認
      if (GameRules.IsWithinBoundsX(newX) && GameRules.IsWithinBoundsY(newY))
      {
        GameState.TurtlePositionX = newX;
        GameState.TurtlePositionY = newY;
      }
    }

    /// <summary>
    /// ワニをカメに向かって移動させる（縦横自由）
    /// </summary>
    public static void MoveCrocodileTowardsTurtle()
    {
      // X方向の移動
      if (GameState.CrocodilePositionX < GameState.TurtlePositionX)
      {
        GameState.CrocodilePositionX++;
      }
      else if (GameState.CrocodilePositionX > GameState.TurtlePositionX)
      {
        GameState.CrocodilePositionX--;
      }
      // Y方向の移動
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
    /// カメをユーザー入力に基づいて移動させる（縦横対応）
    /// </summary>
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
}
