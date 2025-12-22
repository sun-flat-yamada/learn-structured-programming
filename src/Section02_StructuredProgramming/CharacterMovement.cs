using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のキャラクター移動処理関数
  /// </summary>
  public static class CharacterMovement
  {
    private static Random _random = new Random();

    /// <summary>
    /// カエルをランダムに移動させる（ユーザー入力がない場合のデフォルト動作）
    /// </summary>
    /// <remarks>
    /// 【改修内容】カエルの動きを行ごとに違うランダム化するには、以下の処理を実装する
    ///
    /// ステップ1: 毎ゲームティック（ExecuteGameTick呼び出しのたびに）に新しいランダム値を生成する
    ///           これにより、各行の描画時にカエルの移動パターンが異なるようになる
    ///
    /// ステップ2: Random.Next()の呼び出し位置を確認し、各ティックで複数回の乱数生成が行われないようにする
    ///
    /// ステップ3: 移動確率の設定値（現在60%）と、方向選択のロジックは行ごとに独立して評価される
    ///           すなわち1行目では右に移動、2行目では移動せず、3行目では左に移動、という具合に
    ///           ゲームループの各イテレーション時点で新しいランダム判定が行われる
    ///
    /// 【改修実装例】以下のコードで行ごとに異なるランダム化を実現
    ///
    /// 【パターン1】移動確率を変数化して柔軟に調整する場合の実装方法：
    /// int moveThreshold = 60;
    /// if (_random.Next(100) >= moveThreshold) { return; }
    /// ランダム値の閾値を変数化することで、ゲーム設定から移動確率を動的に変更可能にする。
    ///
    /// 【パターン2】より細かく3段階の確率分岐を増やす場合の実装方法：
    /// int randomValue = _random.Next(100);
    /// int adjustedPosition = (randomValue less than 30) ? GameState.FrogPosition - 1 :
    ///                        (randomValue less than 60) ? GameState.FrogPosition + 1 :
    ///                        GameState.FrogPosition;
    /// if (randomValue less than 60 AND IsWithinBounds(adjustedPosition))
    /// { GameState.FrogPosition = adjustedPosition; }
    /// この方法で、30%左移動、30%右移動、40%移動なしの3パターンを実装可能にする。
    ///
    /// 【パターン3】ランダム化を強化するため複数の乱数を組み合わせる場合の実装方法：
    /// int moveProbability = _random.Next(100);
    /// int directionWeight = _random.Next(3);
    /// if (moveProbability less than 50)
    /// {
    ///   int randomDirection = (directionWeight == 0) ? -1 : (directionWeight == 1) ? 1 : 0;
    ///   int calculatedPosition = GameState.FrogPosition + randomDirection;
    ///   if (IsWithinBounds(calculatedPosition))
    ///   { GameState.FrogPosition = calculatedPosition; }
    /// }
    /// 複数の乱数判定を組み合わせることで、より複雑なランダムなパターンが生成される。
    /// </remarks>
    public static void MoveFrogRandomly()
    {
      // 60%の確率で移動
      if (_random.Next(100) >= 60)
      {
        return;
      }

      // 改修内容：移動方向をランダムに決定する
      // 実装：Random.Next(2)が0なら左（-1）、1なら右（+1）という二者択一で方向を決定
      //      この判定も毎行のティック時に独立して実行されるため、
      //      行ごとに異なる方向選択が行われることを保証している
      int newPosition = _random.Next(2) == 0
        ? GameState.FrogPosition - 1  // 左方向に1マス移動
        : GameState.FrogPosition + 1; // 右方向に1マス移動

      // 改修内容：移動後の位置がゲーム領域内であることを確認
      // 実装：GameRules.IsWithinBounds()でカエルの新しい位置がゲーム領域内かチェック
      //      この確認により、画面の端を超えてカエルが移動することを防ぎ、
      //      ランダムな動きの中でも正当性を保つ
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
