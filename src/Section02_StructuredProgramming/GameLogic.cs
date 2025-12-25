using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// ゲームロジック（判定系）を提供する静的クラス
  ///
  /// ■ 責務
  /// 衝突判定などのゲームルールに関するロジックを提供します。
  /// GameRulesの判定関数を呼び出すファサード的役割です。
  /// </summary>
  public static class GameLogic
  {
    /// <summary>
    /// カメとワニの衝突判定
    /// </summary>
    public static bool IsCollisionDetected()
    {
      return GameRules.IsCollisionDetected(GameState.TurtlePosition, GameState.CrocodilePosition);
    }
  }
}
