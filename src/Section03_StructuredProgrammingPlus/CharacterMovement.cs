using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のキャラクター移動処理関数（完全対応版）
  /// </summary>
  /// <remarks>
  /// 【Section03_StructuredProgrammingPlus について】
  /// このセクションは、Section02 のコメント内の「パターン2」を完全実装したバージョンです。
  ///
  /// 【改修内容】カエルを行ごとにランダム化する完全対応版
  ///
  /// - 移動確率の3段階分岐：30%左移動、30%右移動、40%移動なし
  /// - 毎フレーム（各行描画のたびに）新しいランダム値を生成
  /// - 各行で独立した確率判定により、異なるランダムなパターンが実現される
  /// </remarks>
  public static class CharacterMovement
  {
    private static Random _random = new Random();

    /// <summary>
    /// カエルをランダムに移動させる（ユーザー入力がない場合のデフォルト動作）
    /// 完全対応版：3段階確率分岐（左30%、右30%、移動なし40%）
    /// </summary>
    public static void MoveFrogRandomly()
    {
      // 毎フレームで新しいランダム値を生成
      // これにより、各行の描画時にカエルの移動パターンが異なるようになる
      int randomValue = _random.Next(100);

      // 3段階の確率分岐：
      // - 0～29: 左に移動（30%の確率）
      // - 30～59: 右に移動（30%の確率）
      // - 60～99: 移動なし（40%の確率）

      int newPosition;

      if (randomValue < 30)
      {
        // 左方向に1マス移動
        newPosition = GameState.FrogPosition - 1;
      }
      else if (randomValue < 60)
      {
        // 右方向に1マス移動
        newPosition = GameState.FrogPosition + 1;
      }
      else
      {
        // 移動なし
        return;
      }

      // 移動後の位置がゲーム領域内であることを確認
      if (GameRules.IsWithinBounds(newPosition))
      {
        GameState.FrogPosition = newPosition;
      }
    }

    /// <summary>
    /// ヘビをカエルに向かって移動させる
    /// </summary>
    public static void MoveSnakeTowardsFrog()
    {
      if (GameState.SnakePosition < GameState.FrogPosition)
      {
        GameState.SnakePosition++;
      }
      else if (GameState.SnakePosition > GameState.FrogPosition)
      {
        GameState.SnakePosition--;
      }
    }

    /// <summary>
    /// カエルをユーザー入力に基づいて移動させる
    /// </summary>
    public static void MoveFrog(int direction)
    {
      int newPosition = GameState.FrogPosition + direction;
      if (GameRules.IsWithinBounds(newPosition))
      {
        GameState.FrogPosition = newPosition;
      }
    }
  }
}
