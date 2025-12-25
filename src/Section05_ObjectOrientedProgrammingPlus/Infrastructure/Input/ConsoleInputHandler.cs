using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Infrastructure.Input
{
  /// <summary>
  /// コンソール入力ハンドラー
  ///
  /// ■ 責務
  /// コンソールからのキー入力を受け取り、
  /// プレイヤー操作またはシステムコマンドに変換します。
  /// WASDおよび矢印キーによる移動をサポートします。
  /// </summary>
  public sealed class ConsoleInputHandler : IInputHandler
  {
    public InputResult ProcessInput(Player player)
    {
      if (!TryGetKey(out var keyInfo))
      {
        return InputResult.Continue;
      }

      return ProcessKeyInput(keyInfo, player);
    }

    private static bool TryGetKey(out ConsoleKeyInfo keyInfo)
    {
      keyInfo = default;

      try
      {
        if (!Console.KeyAvailable)
          return false;

        keyInfo = Console.ReadKey(intercept: true);
        return true;
      }
      catch (InvalidOperationException)
      {
        // コンソール入力がリダイレクトされている環境
        return false;
      }
    }

    private static InputResult ProcessKeyInput(ConsoleKeyInfo keyInfo, Player player)
    {
      switch (keyInfo.Key)
      {
        case ConsoleKey.W:
        case ConsoleKey.UpArrow:
          player.MoveUp();
          return InputResult.Moved;

        case ConsoleKey.S:
        case ConsoleKey.DownArrow:
          player.MoveDown();
          return InputResult.Moved;

        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
          player.MoveLeft();
          return InputResult.Moved;

        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
          player.MoveRight();
          return InputResult.Moved;

        case ConsoleKey.P:
          return InputResult.Pause;

        case ConsoleKey.Q:
        case ConsoleKey.Escape:
          return InputResult.Quit;

        default:
          return InputResult.Continue;
      }
    }
  }
}
