using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// キャラクター移動ロジックを提供する静的クラス
  ///
  /// ■ 責務
  /// カメとワニの移動処理を担当します。
  /// 移動に関するロジックをここに集約することで、責務を明確化しています。
  ///
  /// ■ 移動パターン
  /// - カメ: ワニ接近時に自動逃走、またはユーザー入力で移動
  /// - ワニ: カメを追跡（単純AI）
  /// </summary>
  public static class CharacterMovement
  {
    /// <summary>
    /// カメをワニから逃げる方向に自動移動（危険距離5マス以内）
    /// </summary>
    public static void MoveTurtleAwayFromCrocodile()
    {
      int distance = Math.Abs(GameState.TurtlePosition - GameState.CrocodilePosition);

      // 危険距離内なら逃走
      if (distance <= 5)
      {
        int newPosition;
        if (GameState.CrocodilePosition < GameState.TurtlePosition)
        {
          // ワニが左にいるので右へ
          newPosition = GameState.TurtlePosition + 1;
        }
        else
        {
          // ワニが右にいるので左へ
          newPosition = GameState.TurtlePosition - 1;
        }

        if (GameRules.IsWithinBounds(newPosition))
        {
          GameState.TurtlePosition = newPosition;
        }
      }
    }

    /// <summary>
    /// ワニをカメに向かって追跡移動
    /// </summary>
    public static void MoveCrocodileTowardsTurtle()
    {
      if (GameState.CrocodilePosition < GameState.TurtlePosition)
      {
        GameState.CrocodilePosition++;
      }
      else if (GameState.CrocodilePosition > GameState.TurtlePosition)
      {
        GameState.CrocodilePosition--;
      }
    }

    /// <summary>
    /// カメを指定方向に移動（ユーザー入力用）
    /// </summary>
    /// <param name="direction">移動量（-1:左, 1:右）</param>
    public static void MoveTurtle(int direction)
    {
      int newPosition = GameState.TurtlePosition + direction;
      if (GameRules.IsWithinBounds(newPosition))
      {
        GameState.TurtlePosition = newPosition;
      }
    }
  }
}
