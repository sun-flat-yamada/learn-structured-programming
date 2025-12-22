using System;
using System.Threading;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミング用のユーザー入力処理関数
  /// </summary>
  public static class InputHandler
  {
    /// <summary>
    /// ユーザー入力を処理してゲーム状態を更新する
    /// </summary>
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
