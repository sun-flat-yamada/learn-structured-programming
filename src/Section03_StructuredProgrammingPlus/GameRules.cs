using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のゲームルール定義と関連する関数
  /// </summary>
  public static class GameRules
  {
    // ゲームルール設定（グローバル定数）
    public const int GameWidth = 40;
    public const int GameHeight = 10;
    public const int InitialFrogPosition = 20;
    public const int InitialSnakePosition = 5;
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
    public static bool IsCollisionDetected(int frogPosition, int snakePosition)
    {
      return frogPosition == snakePosition;
    }
  }
}
