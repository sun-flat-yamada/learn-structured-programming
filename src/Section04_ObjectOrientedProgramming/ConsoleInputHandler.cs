using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// コンソール入力のハンドリング実装
  /// IInputHandlerインターフェースを実装
  /// </summary>
  public class ConsoleInputHandler : IInputHandler
  {
    public bool ProcessInput(Frog frog)
    {
      if (!IsKeyAvailable())
      {
        frog.MoveRandomly();
        return true;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      return ProcessKeyInput(keyInfo, frog);
    }

    private static bool IsKeyAvailable()
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

    private static bool ProcessKeyInput(ConsoleKeyInfo keyInfo, Frog frog)
    {
      return keyInfo.Key switch
      {
        ConsoleKey.A or ConsoleKey.LeftArrow => HandleLeftInput(frog),
        ConsoleKey.D or ConsoleKey.RightArrow => HandleRightInput(frog),
        ConsoleKey.Q => false,
        _ => HandleRandomMovement(frog)
      };
    }

    private static bool HandleLeftInput(Frog frog)
    {
      frog.MoveByDirection(-1);
      return true;
    }

    private static bool HandleRightInput(Frog frog)
    {
      frog.MoveByDirection(1);
      return true;
    }

    private static bool HandleRandomMovement(Frog frog)
    {
      frog.MoveRandomly();
      return true;
    }
  }
}
