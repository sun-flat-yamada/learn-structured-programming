using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング用のゲーム画面描画処理関数
  /// </summary>
  public static class GameRenderer
  {
    /// <summary>
    /// ゲーム画面全体を描画
    /// </summary>
    public static void RenderGameScreen()
    {
      Console.Clear();
      RenderHeader();
      RenderGameBoard();
      RenderInstructions();
    }

    /// <summary>
    /// ゲームオーバー画面を描画
    /// </summary>
    public static void RenderGameOverScreen()
    {
      Console.Clear();
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║        ゲーム オーバー                 ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("ヘビがカエルを捕食しました！");
      Console.ResetColor();
      Console.WriteLine();
      Console.WriteLine($"最終スコア: {GameState.Score}");
      Console.WriteLine();
      Console.WriteLine("Enterキーを押して終了...");
      Console.ReadLine();
    }

    private static void RenderHeader()
    {
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カエルVSヘビゲーム              ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
    }

    private static void RenderGameBoard()
    {
      RenderBoardTop();
      RenderGameArea();
      RenderBoardBottom();
      Console.WriteLine();
    }

    private static void RenderBoardTop()
    {
      Console.Write("║");
      for (int i = 0; i < GameRules.GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    private static void RenderBoardBottom()
    {
      Console.Write("║");
      for (int i = 0; i < GameRules.GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    private static void RenderGameArea()
    {
      for (int y = 0; y < GameRules.GameHeight; y++)
      {
        Console.Write("║");
        RenderGameRow();
        Console.WriteLine("║");
      }
    }

    private static void RenderGameRow()
    {
      for (int x = 0; x < GameRules.GameWidth; x++)
      {
        if (x == GameState.SnakePosition)
        {
          RenderCharacter("🐍", ConsoleColor.Red);
          x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
        }
        else if (x == GameState.FrogPosition)
        {
          RenderCharacter("🐸", ConsoleColor.Green);
          x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
        }
        else
        {
          Console.Write(" ");
        }
      }

      // 行の残り部分を埋める
      int filledWidth = 0;
      if (GameState.SnakePosition < GameRules.GameWidth)
        filledWidth += 2;
      if (GameState.FrogPosition < GameRules.GameWidth)
        filledWidth += 2;

      for (int i = filledWidth; i < GameRules.GameWidth; i++)
      {
        Console.Write(" ");
      }
    }

    private static void RenderCharacter(string character, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      Console.Write(character);
      Console.ResetColor();
    }

    private static void RenderInstructions()
    {
      Console.WriteLine("操作: [A]左 [D]右 [Q]終了");
      Console.WriteLine();
      Console.WriteLine("┌────────────────────────────────────────┐");
      Console.WriteLine("│ カエル🐸: 左右矢印で移動              │");
      Console.WriteLine("│ ヘビ🐍: カエルを追いかけます           │");
      Console.WriteLine("│ ヘビに捕まったらゲームオーバー          │");
      Console.WriteLine("└────────────────────────────────────────┘");
    }
  }
}
