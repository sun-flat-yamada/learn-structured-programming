using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// コンソール画面へのゲームレンダリング実装
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 単一責任の原則: コンソールへの描画のみを担当
  /// - インターフェース実装: IGameRendererを実装し交換可能性を提供
  /// - コンポジション: GameConfigを依存性として受け取る
  /// </summary>
  public class ConsoleGameRenderer : IGameRenderer
  {
    private readonly GameConfig _config;

    public ConsoleGameRenderer(GameConfig config)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public void SetupDisplay()
    {
      Console.Clear();
      Console.CursorVisible = false;
    }

    public void RestoreDisplay()
    {
      Console.CursorVisible = true;
    }

    public void RenderGameScreen(GameState gameState)
    {
      Console.Clear();
      RenderHeader();
      RenderGameBoard(gameState);
      RenderInstructions();
      RenderScore(gameState);
    }

    public void RenderGameOverScreen(int finalScore)
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
      Console.WriteLine($"最終スコア: {finalScore}");
      Console.WriteLine();
      Console.WriteLine("Enterキーを押して終了...");
      Console.ReadLine();
    }

    private void RenderHeader()
    {
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カメVSワニゲーム                ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
    }

    private void RenderGameBoard(GameState gameState)
    {
      RenderBoardTop();
      RenderGameArea(gameState);
      RenderBoardBottom();
      Console.WriteLine();
    }

    private void RenderBoardTop()
    {
      Console.Write("║");
      for (int i = 0; i < _config.GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    private void RenderBoardBottom()
    {
      Console.Write("║");
      for (int i = 0; i < _config.GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    private void RenderGameArea(GameState gameState)
    {
      for (int y = 0; y < _config.GameHeight; y++)
      {
        Console.Write("║");
        RenderGameRow(y, gameState);
        Console.WriteLine("║");
      }
    }

    private void RenderGameRow(int y, GameState gameState)
    {
      Position turtlePos = gameState.TurtlePosition;
      Position crocodilePos = gameState.CrocodilePosition;
      Turtle turtle = gameState.Turtle;
      Crocodile crocodile = gameState.Crocodile;

      for (int x = 0; x < _config.GameWidth; x++)
      {
        if (x == crocodilePos.X && y == crocodilePos.Y)
        {
          RenderCharacter(crocodile.Emoji, crocodile.Color);
          x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
        }
        else if (x == turtlePos.X && y == turtlePos.Y)
        {
          RenderCharacter(turtle.Emoji, turtle.Color);
          x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
        }
        else
        {
          Console.Write(" ");
        }
      }
    }

    private static void RenderCharacter(string emoji, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      Console.Write(emoji);
      Console.ResetColor();
    }

    private void RenderScore(GameState gameState)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"スコア: {gameState.Score}");
      Console.ResetColor();
    }

    private static void RenderInstructions()
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
}
