using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のゲームルール定義と関連する関数
  /// </summary>
  public static class GameRules
  {
    // ゲームルール設定（グローバル定数）
    public const int GameWidth = 32;
    public const int GameHeight = 1;
    public const int InitialTurtlePosition = 20;
    public const int InitialCrocodilePosition = 5;
    public const int GameUpdateDelayMs = 200;

    /// <summary>
    /// 指定位置がゲーム領域内か判定する
    /// </summary>
    public static bool IsWithinBounds(int position)
    {
      return position >= 0 && position < GameWidth - 2;
    }

    /// <summary>
    /// 衝突が発生しているか判定する
    /// </summary>
    public static bool IsCollisionDetected(int turtlePosition, int crocodilePosition)
    {
      return turtlePosition == crocodilePosition;
    }
  }
}
