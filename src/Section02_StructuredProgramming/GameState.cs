using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のゲーム状態定義と操作関数
  /// </summary>
  public static class GameState
  {
    // グローバル変数：ゲーム状態
    public static int TurtlePosition;
    public static int CrocodilePosition;
    public static int Score;
    public static bool IsActive;

    /// <summary>
    /// ゲーム状態を初期化する
    /// </summary>
    public static void Initialize(int initialTurtlePosition, int initialCrocodilePosition)
    {
      TurtlePosition = initialTurtlePosition;
      CrocodilePosition = initialCrocodilePosition;
      Score = 0;
      IsActive = true;
    }

    /// <summary>
    /// ゲーム状態をリセットする
    /// </summary>
    public static void Reset(int initialTurtlePosition, int initialCrocodilePosition)
    {
      TurtlePosition = initialTurtlePosition;
      CrocodilePosition = initialCrocodilePosition;
      Score = 0;
      IsActive = true;
    }

    /// <summary>
    /// ゲームを終了状態にする
    /// </summary>
    public static void End()
    {
      IsActive = false;
    }

    /// <summary>
    /// スコアを加算
    /// </summary>
    public static void IncrementScore()
    {
      Score++;
    }
  }
}
