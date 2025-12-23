using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// コンソール入力のハンドリング実装
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 単一責任の原則: コンソール入力処理のみを担当
  /// - 開放/閉鎖原則: 新しいキーバインドの追加が容易
  /// - ストラテジーパターン: IInputHandlerインターフェースを実装
  /// </summary>
  public class ConsoleInputHandler : IInputHandler
  {
    /// <summary>
    /// ユーザー入力を処理し、カメを移動させる
    /// </summary>
    public InputResult ProcessInput(Turtle turtle)
    {
      if (!IsKeyAvailable())
      {
        turtle.MoveRandomly();
        return InputResult.Continue;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      return ProcessKeyInput(keyInfo, turtle);
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

    private static InputResult ProcessKeyInput(ConsoleKeyInfo keyInfo, Turtle turtle)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.W:
        case ConsoleKey.UpArrow:
          turtle.MoveUp();
          return InputResult.Continue;

        case ConsoleKey.S:
        case ConsoleKey.DownArrow:
          turtle.MoveDown();
          return InputResult.Continue;

        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
          turtle.MoveLeft();
          return InputResult.Continue;

        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
          turtle.MoveRight();
          return InputResult.Continue;

        case ConsoleKey.Q:
          return InputResult.Quit;

        default:
          // 無効なキーは無視してランダム移動
          turtle.MoveRandomly();
          return InputResult.Continue;
      }
    }
  }
}
