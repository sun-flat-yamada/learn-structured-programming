using System;
using System.Threading;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// ユーザー入力を処理する静的クラス
  ///
  /// ■ 責務
  /// コンソールからのキー入力を検出し、対応するアクションを実行します。
  /// 入力がない場合は、カメの自動逃走行動を発動させます。
  ///
  /// ■ 対応キー
  /// - A/←: 左移動
  /// - D/→: 右移動
  /// - Q: ゲーム終了
  /// </summary>
  public static class InputHandler
  {
    /// <summary>
    /// 毎フレーム呼び出される入力処理（入力なしなら自動逃走）
    /// </summary>
    /// <returns>ゲーム継続ならtrue</returns>
    public static bool ProcessInput()
    {
      if (!IsKeyAvailable())
      {
        // 入力なし: 自動逃走行動
        CharacterMovement.MoveTurtleAwayFromCrocodile();
        return true;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      return ProcessKeyInput(keyInfo);
    }

    /// <summary>
    /// キー入力可能か確認（リダイレクト環境考慮）
    /// </summary>
    private static bool IsKeyAvailable()
    {
      try
      {
        return Console.KeyAvailable;
      }
      catch (InvalidOperationException)
      {
        // リダイレクト環境ではKeyAvailable不可
        return false;
      }
    }

    /// <summary>
    /// キー入力に応じたアクションを実行
    /// </summary>
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
