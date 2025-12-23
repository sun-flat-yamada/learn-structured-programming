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
    /// </summary>
    /// <remarks>
    /// 【改修内容】カメの動きをワニから逃げる動作に変更
    ///
    /// このメソッドは毎フレーム（ExecuteGameTick()の各呼び出し）で実行される。
    /// カメはワニが近づいてきたら反対方向に逃げる。
    /// </remarks>
    public static bool ProcessInput()
    {
      if (!IsKeyAvailable())
      {
        // 入力がない場合は、カメがワニから逃げる
        CharacterMovement.MoveTurtleAwayFromCrocodile();
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
          // 無効なキーは無視してワニから逃げる動作を続ける
          HandleEscapeMovement()
      };
    }

    private static bool HandleLeftInput()
    {
      CharacterMovement.MoveTurtle(-1);
      return true;
    }

    private static bool HandleRightInput()
    {
      CharacterMovement.MoveTurtle(1);
      return true;
    }

    private static bool HandleQuitInput()
    {
      GameState.End();
      return false;
    }

    private static bool HandleEscapeMovement()
    {
      CharacterMovement.MoveTurtleAwayFromCrocodile();
      return true;
    }
  }
}
