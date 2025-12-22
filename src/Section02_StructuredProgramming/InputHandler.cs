using System;
using System.Threading;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のユーザー入力処理関数
  /// </summary>
  public static class InputHandler
  {
    /// <summary>
    /// ユーザー入力を処理してゲーム状態を更新する
    /// </summary>    /// <remarks>
    /// 【改修内容】カエルの動きを行ごとに違うランダム化する場合の実装ポイント
    ///
    /// このメソッドは毎フレーム（ExecuteGameTick()の各呼び出し）で実行される。
    /// ランダム化を行ごとに異なるパターンにするには、以下の流れが重要：
    ///
    /// 【処理フロー】
    /// 1. IsKeyAvailable() でユーザー入力があるかチェック
    /// 2a. 入力がない場合：
    ///     - CharacterMovement.MoveFrogRandomly() を呼び出す
    ///     - この呼び出しのたびに新しい Random.Next() が実行される
    ///     - 毎フレーム（行ごと）に新しい乱数判定が行われる
    /// 2b. 入力がある場合：
    ///     - ProcessKeyInput() で入力内容を判定
    ///     - 方向キー(A/D/←/→)なら CharacterMovement.MoveFrog() で移動
    ///     - Q キーなら GameState.End() でゲーム終了
    ///
    /// 【実装例】より複雑なランダムパターンが必要な場合
    ///
    /// パターン1: ランダム移動の確率を調整する場合
    /// if (!IsKeyAvailable())
    /// {
    ///   int randomCheck = new Random().Next(100);
    ///   if (randomCheck less than 45)  // 45%の確率でランダム移動
    ///   {
    ///     CharacterMovement.MoveFrogRandomly();
    ///   }
    ///   return true;
    /// }
    ///
    /// パターン2: 毎フレーム異なるランダムルールを適用する場合
    /// if (!IsKeyAvailable())
    /// {
    ///   int frameRandomPattern = new Random().Next(3);
    ///   if (frameRandomPattern == 0)
    ///   {
    ///     CharacterMovement.MoveFrogRandomly();  // ランダム移動
    ///   }
    ///   else if (frameRandomPattern == 1)
    ///   {
    ///     CharacterMovement.MoveFrog(-1);  // 左に移動
    ///   }
    ///   // frameRandomPattern == 2 の場合は移動しない
    ///   return true;
    /// }
    ///
    /// パターン3: 複合的なランダム化を実装する場合
    /// if (!IsKeyAvailable())
    /// {
    ///   int complexRandom = new Random().Next(1000);
    ///   if (complexRandom less than 333)
    ///   {
    ///     CharacterMovement.MoveFrog(-1);  // 33.3%で左移動
    ///   }
    ///   else if (complexRandom less than 666)
    ///   {
    ///     CharacterMovement.MoveFrog(1);   // 33.3%で右移動
    ///   }
    ///   // else: 33.3%で移動なし
    ///   return true;
    /// }
    /// </remarks>
    public static bool ProcessInput()
    {
      if (!IsKeyAvailable())
      {
        // 入力がない場合は、カエルがランダムに移動
        CharacterMovement.MoveFrogRandomly();
        return true;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      return ProcessKeyInput(keyInfo);
    }

    private static bool IsKeyAvailable()
    {
      try
      {
        return Console.KeyAvailable;
      }
      catch (InvalidOperationException)
      {
        // コンソール入力がリダイレクトされている環境では KeyAvailable は使用不可
        return false;
      }
    }

    private static bool ProcessKeyInput(ConsoleKeyInfo keyInfo)
    {
      return keyInfo.Key switch
      {
        ConsoleKey.A or ConsoleKey.LeftArrow =>
          HandleLeftInput(),

        ConsoleKey.D or ConsoleKey.RightArrow =>
          HandleRightInput(),

        ConsoleKey.Q =>
          HandleQuitInput(),

        _ =>
          // 無効なキーは無視してランダム移動を続ける
          HandleRandomMovement()
      };
    }

    private static bool HandleLeftInput()
    {
      CharacterMovement.MoveFrog(-1);
      return true;
    }

    private static bool HandleRightInput()
    {
      CharacterMovement.MoveFrog(1);
      return true;
    }

    private static bool HandleQuitInput()
    {
      GameState.End();
      return false;
    }

    private static bool HandleRandomMovement()
    {
      CharacterMovement.MoveFrogRandomly();
      return true;
    }
  }
}
