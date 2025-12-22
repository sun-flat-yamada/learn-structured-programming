using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のゲームロジック処理関数
  /// </summary>
  public static class GameLogic
  {
    /// <summary>
    /// 衝突判定を行う
    /// </summary>
    public static bool IsCollisionDetected()
    {
      return GameRules.IsCollisionDetected(GameState.FrogPosition, GameState.SnakePosition);
    }
  }
}
