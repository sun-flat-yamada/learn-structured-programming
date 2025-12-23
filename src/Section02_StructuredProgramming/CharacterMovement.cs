using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のキャラクター移動処理関数
  /// </summary>
  public static class CharacterMovement
  {
    /// <summary>
    /// カメをワニから逃げる方向に移動させる（ワニが近づいてきたら反対方向に逃げる）
    /// </summary>
    public static void MoveTurtleAwayFromCrocodile()
    {
      // ワニが近づいてきたら反対方向に逃げる
      int distance = Math.Abs(GameState.TurtlePosition - GameState.CrocodilePosition);

      // ワニが近くにいる場合（5マス以内）
      if (distance <= 5)
      {
        int newPosition;
        if (GameState.CrocodilePosition < GameState.TurtlePosition)
        {
          // ワニが左にいるので右に逃げる
          newPosition = GameState.TurtlePosition + 1;
        }
        else
        {
          // ワニが右にいるので左に逃げる
          newPosition = GameState.TurtlePosition - 1;
        }

        if (GameRules.IsWithinBounds(newPosition))
        {
          GameState.TurtlePosition = newPosition;
        }
      }
    }

    /// <summary>
    /// ワニをカメに向かって移動させる
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
    /// カメをユーザー入力に基づいて移動させる
    /// </summary>
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
