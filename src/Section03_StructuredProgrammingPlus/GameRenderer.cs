using System;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus;

/// <summary>
/// コンソール画面への描画を担当する静的クラス（2D対応）
///
/// ■ 責務
/// 32x32の2Dゲームボードをコンソールに描画します。
///
/// ■ Section02からの変更点
/// RenderGameRowがY座標を受け取り、2D描画に対応。
/// </summary>
public static class GameRenderer
{
  /// <summary>
  /// ゲーム画面全体を再描画
  /// </summary>
  public static void RenderGameScreen()
  {
    Console.Clear();
    renderHeader();
    renderGameBoard();
    renderInstructions();
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
    Console.WriteLine("ワニがカメを捕食しました！");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine($"最終スコア: {GameState.Score}");
    Console.WriteLine();
    Console.WriteLine("Enterキーを押して終了...");
    Console.ReadLine();
  }

  private static void renderHeader()
  {
    Console.WriteLine("╔════════════════════════════════════════╗");
    Console.WriteLine("║      カメVSワニゲーム                ║");
    Console.WriteLine("╚════════════════════════════════════════╝");
    Console.WriteLine();
  }

  private static void renderGameBoard()
  {
    renderBoardTop();
    renderGameArea();
    renderBoardBottom();
    Console.WriteLine();
  }

  private static void renderBoardTop()
  {
    Console.Write("║");
    for (int i = 0; i < GameRules.GAME_WIDTH; i++)
    {
      Console.Write("─");
    }
    Console.WriteLine("║");
  }

  private static void renderBoardBottom()
  {
    Console.Write("║");
    for (int i = 0; i < GameRules.GAME_WIDTH; i++)
    {
      Console.Write("─");
    }
    Console.WriteLine("║");
  }

  private static void renderGameArea()
  {
    for (int y = 0; y < GameRules.GAME_HEIGHT; y++)
    {
      Console.Write("║");
      renderGameRow(y);
      Console.WriteLine("║");
    }
  }

  private static void renderGameRow(int y)
  {
    for (int x = 0; x < GameRules.GAME_WIDTH; x++)
    {
      if (x == GameState.CrocodilePositionX && y == GameState.CrocodilePositionY)
      {
        renderCharacter("🐊", ConsoleColor.Red);
        x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
      }
      else if (x == GameState.TurtlePositionX && y == GameState.TurtlePositionY)
      {
        renderCharacter("🐢", ConsoleColor.Green);
        x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
      }
      else
      {
        Console.Write(" ");
      }
    }
  }

  private static void renderCharacter(string character, ConsoleColor color)
  {
    Console.ForegroundColor = color;
    Console.Write(character);
    Console.ResetColor();
  }

  private static void renderInstructions()
  {
    Console.WriteLine("操作: [W]上 [S]下 [A]左 [D]右 [Q]終了");
    Console.WriteLine();
    Console.WriteLine("┌────────────────────────────────────────┐");
    Console.WriteLine("│ カメ🐢: 矢印キー/WASDで移動            │");
    Console.WriteLine("│ ワニ🐊: カメを追いかけます             │");
    Console.WriteLine("│ ワニに捕まったらゲームオーバー          │");
    Console.WriteLine("└────────────────────────────────────────┘");
  }
}
