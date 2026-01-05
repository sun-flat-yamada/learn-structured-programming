using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming;

/// <summary>
/// コンソール画面への描画を担当する静的クラス
///
/// ■ 責務
/// ゲーム画面（ヘッダー、ボード、操作説明）およびゲームオーバー画面の
/// 描画処理を提供します。
///
/// ■ 設計意図
/// 描画処理を1つのクラスに集約することで、
/// 表示変更時の影響範囲を限定しています。
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
      renderGameRow();
      Console.WriteLine("║");
    }
  }

  private static void renderGameRow()
  {
    for (int x = 0; x < GameRules.GAME_WIDTH; x++)
    {
      if (x == GameState.CrocodilePosition)
      {
        renderCharacter("🐊", ConsoleColor.Red);
        x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
      }
      else if (x == GameState.TurtlePosition)
      {
        renderCharacter("🐢", ConsoleColor.Green);
        x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
      }
      else
      {
        Console.Write(" ");
      }
    }

    // 行の残り部分を埋める
    int filledWidth = 0;
    if (GameState.CrocodilePosition < GameRules.GAME_WIDTH)
      filledWidth += 2;
    if (GameState.TurtlePosition < GameRules.GAME_WIDTH)
      filledWidth += 2;

    for (int i = filledWidth; i < GameRules.GAME_WIDTH; i++)
    {
      Console.Write(" ");
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
    Console.WriteLine("操作: [A]左 [D]右 [Q]終了");
    Console.WriteLine();
    Console.WriteLine("┌────────────────────────────────────────┐");
    Console.WriteLine("│ カメ🐢: 左右矢印で移動                │");
    Console.WriteLine("│ ワニ🐊: カメを追いかけます             │");
    Console.WriteLine("│ ワニに捕まったらゲームオーバー          │");
    Console.WriteLine("└────────────────────────────────────────┘");
  }
}
