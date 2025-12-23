using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のゲームロジック処理関数
  /// </summary>
  public static class GameLogic
  {
    /// <summary>
    /// 衝突判定を行う（2D座標対応）
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
