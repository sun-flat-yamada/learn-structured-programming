using System;
using System.Threading;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus;

/// <summary>
/// ユーザー入力を処理する静的クラス（2D対応）
///
/// ■ 責務
/// コンソールからのキー入力を検出し、2D移動を実行します。
/// 入力がない場合はランダム移動を発動します。
///
/// ■ 対応キー
/// - W/↑: 上移動
/// - S/↓: 下移動
/// - A/←: 左移動
/// - D/→: 右移動
/// - Q: ゲーム終了
/// </summary>
public static class InputHandler
{
  /// <summary>
  /// 毎フレーム呼び出される入力処理（入力なしならランダム移動）
  /// </summary>
  public static bool ProcessInput()
  {
    if (!isKeyAvailable())
    {
      // 入力がない場合は、カメがランダムに移動
      CharacterMovement.MoveTurtleRandomly();
      return true;
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    return processKeyInput(keyInfo);
  }

  /// <summary>
  /// キー入力可能か確認（リダイレクト環境考慮）
  /// </summary>
  private static bool isKeyAvailable()
  {
    try
    {
      return Console.KeyAvailable;
    }
    catch (InvalidOperationException)
    {
      return false;
    }
  }

  /// <summary>
  /// キー入力に応じた2D移動を実行
  /// </summary>
  private static bool processKeyInput(ConsoleKeyInfo keyInfo)
  {
    return keyInfo.Key switch
    {
      ConsoleKey.A or ConsoleKey.LeftArrow =>
        handleLeftInput(),

      ConsoleKey.D or ConsoleKey.RightArrow =>
        handleRightInput(),

      ConsoleKey.W or ConsoleKey.UpArrow =>
        handleUpInput(),

      ConsoleKey.S or ConsoleKey.DownArrow =>
        handleDownInput(),

      ConsoleKey.Q =>
        handleQuitInput(),

      _ =>
        // 無効なキーは無視してランダム移動を続ける
        handleRandomMovement()
    };
  }

  private static bool handleLeftInput()
  {
    CharacterMovement.MoveTurtle(-1, 0);
    return true;
  }

  private static bool handleRightInput()
  {
    CharacterMovement.MoveTurtle(1, 0);
    return true;
  }

  private static bool handleUpInput()
  {
    CharacterMovement.MoveTurtle(0, -1);
    return true;
  }

  private static bool handleDownInput()
  {
    CharacterMovement.MoveTurtle(0, 1);
    return true;
  }

  private static bool handleQuitInput()
  {
    GameState.End();
    return false;
  }

  private static bool handleRandomMovement()
  {
    CharacterMovement.MoveTurtleRandomly();
    return true;
  }
}
