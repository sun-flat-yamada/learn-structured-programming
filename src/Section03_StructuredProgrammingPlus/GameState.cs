using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// ゲーム状態を保持する静的クラス（2D座標対応）
  ///
  /// ■ 責務
  /// ゲーム全体で共有される状態（X/Y位置、スコア、実行中フラグ）を
  /// グローバル変数として管理します。
  ///
  /// ■ Section02からの変更点
  /// TurtlePosition/CrocodilePosition → TurtlePositionX/Y, CrocodilePositionX/Y
  /// </summary>
  public static class GameState
  {
    // カメの位置（X, Y座標）
    public static int TurtlePositionX;
    public static int TurtlePositionY;

    // ワニの位置（X, Y座標）
    public static int CrocodilePositionX;
    public static int CrocodilePositionY;

    // ゲーム進行状態
    public static int Score;
    public static bool IsActive;

    /// <summary>
    /// ゲーム状態を初期値に設定
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
    /// ゲーム状態をリセット（再プレイ用）
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
