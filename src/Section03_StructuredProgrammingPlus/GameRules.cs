using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// ゲームルールと定数を定義する静的クラス（2D対応）
  ///
  /// ■ 責務
  /// ゲームの不変パラメータおよびルール判定ロジックを提供します。
  ///
  /// ■ Section02からの変更点
  /// - GameHeightが1→32に拡張
  /// - 座標判定がX/Y別に分離
  /// </summary>
  public static class GameRules
  {
    // 盤面サイズ（32x32の2Dフィールド）
    public const int GameWidth = 32;
    public const int GameHeight = 32;

    // カメの初期位置
    public const int InitialTurtlePositionX = 20;
    public const int InitialTurtlePositionY = 16;

    // ワニの初期位置
    public const int InitialCrocodilePositionX = 5;
    public const int InitialCrocodilePositionY = 16;

    // ゲーム速度
    public const int GameUpdateDelayMs = 200;

    /// <summary>
    /// X座標がゲーム領域内か判定
    /// </summary>
    public static bool IsWithinBoundsX(int position)
    {
      return position >= 0 && position < GameWidth - 2;
    }

    /// <summary>
    /// Y座標がゲーム領域内か判定
    /// </summary>
    public static bool IsWithinBoundsY(int position)
    {
      return position >= 0 && position < GameHeight;
    }

    /// <summary>
    /// 2D座標での衝突判定
    /// </summary>
    public static bool IsCollisionDetected(int turtleX, int turtleY, int crocodileX, int crocodileY)
    {
      return turtleX == crocodileX && turtleY == crocodileY;
    }
  }
}
