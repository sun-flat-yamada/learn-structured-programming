using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のゲーム状態定義と操作関数
  /// </summary>
  public static class GameState
  {
    // グローバル変数：ゲーム状態（2D座標対応）
    public static int TurtlePositionX;
    public static int TurtlePositionY;
    public static int CrocodilePositionX;
    public static int CrocodilePositionY;
    public static int Score;
    public static bool IsActive;

    /// <summary>
    /// ゲーム状態を初期化する
    /// </summary>
    public static void Initialize(int initialTurtleX, int initialTurtleY, int initialCrocodileX, int initialCrocodileY)
    {
      TurtlePositionX = initialTurtleX;
      TurtlePositionY = initialTurtleY;
      CrocodilePositionX = initialCrocodileX;
      CrocodilePositionY = initialCrocodileY;
      Score = 0;
      IsActive = true;
    }

    /// <summary>
    /// ゲーム状態をリセットする
    /// </summary>
    public static void Reset(int initialTurtleX, int initialTurtleY, int initialCrocodileX, int initialCrocodileY)
    {
      TurtlePositionX = initialTurtleX;
      TurtlePositionY = initialTurtleY;
      CrocodilePositionX = initialCrocodileX;
      CrocodilePositionY = initialCrocodileY;
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
