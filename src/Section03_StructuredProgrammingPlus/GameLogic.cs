using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// ゲームロジック（判定系）を提供する静的クラス（2D対応）
  ///
  /// ■ 責務
  /// 2D座標での衝突判定を提供します。
  /// </summary>
  public static class GameLogic
  {
    /// <summary>
    /// カメとワニの2D衝突判定
    /// </summary>
    public static bool IsCollisionDetected()
    {
      return GameRules.IsCollisionDetected(
        GameState.TurtlePositionX,
        GameState.TurtlePositionY,
        GameState.CrocodilePositionX,
        GameState.CrocodilePositionY);
    }
  }
}
