using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のゲームルール定義と関連する関数
  /// </summary>
  public static class GameRules
  {
    // ゲームルール設定（グローバル定数）
    public const int GameWidth = 32;
    public const int GameHeight = 32;
    public const int InitialTurtlePositionX = 20;
    public const int InitialTurtlePositionY = 16;
    public const int InitialCrocodilePositionX = 5;
    public const int InitialCrocodilePositionY = 16;
    public const int GameUpdateDelayMs = 200;

    /// <summary>
    /// 指定位置がゲーム領域内か判定する（X座標）
    /// </summary>
    public static bool IsWithinBoundsX(int position)
    {
      return position >= 0 && position < GameWidth - 2;
    }

    /// <summary>
    /// 指定位置がゲーム領域内か判定する（Y座標）
    /// </summary>
    public static bool IsWithinBoundsY(int position)
    {
      return position >= 0 && position < GameHeight;
    }

    /// <summary>
    /// 衝突が発生しているか判定する
    /// </summary>
    public static bool IsCollisionDetected(int turtleX, int turtleY, int crocodileX, int crocodileY)
    {
      return turtleX == crocodileX && turtleY == crocodileY;
    }
  }
}
