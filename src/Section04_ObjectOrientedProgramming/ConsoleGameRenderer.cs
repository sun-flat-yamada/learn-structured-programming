using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// コンソール画面へのゲームレンダリング実装
  /// IGameRendererインターフェースを実装
  /// </summary>
  public class ConsoleGameRenderer : IGameRenderer
  {
    private readonly GameConfig _config;
    private readonly Frog _frog;
    private readonly Snake _snake;

    public ConsoleGameRenderer(GameConfig config, Frog frog, Snake snake)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
      _frog = frog ?? throw new ArgumentNullException(nameof(frog));
      _snake = snake ?? throw new ArgumentNullException(nameof(snake));
    }

    public void SetupConsole()
    {
      Console.Clear();
      Console.CursorVisible = false;
    }

    public void RestoreConsole()
    {
      Console.CursorVisible = true;
    }

    public void RenderGameScreen(GameState gameState)
    {
      Console.Clear();
      RenderHeader();
      RenderGameBoard(gameState);
      RenderInstructions();
    }

    public void RenderGameOverScreen(int finalScore)
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
      Console.WriteLine($"最終スコア: {finalScore}");
      Console.WriteLine();
      Console.WriteLine("Enterキーを押して終了...");
      Console.ReadLine();
    }

    private void RenderHeader()
    {
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カエルVSヘビゲーム              ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
    }

    private void RenderGameBoard(GameState gameState)
    {
      RenderBoardTop();
      RenderGameArea(gameState);
      RenderBoardBottom();
      Console.WriteLine();
      RenderScore(gameState);
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
        RenderGameRow(gameState);
        Console.WriteLine("║");
      }
    }

    private void RenderGameRow(GameState gameState)
    {
      for (int x = 0; x < _config.GameWidth; x++)
      {
        if (x == _snake.Position)
        {
          RenderCharacter(_snake.GetEmoji(), _snake.GetColor());
          x++;
        }
        else if (x == _frog.Position)
        {
          RenderCharacter(_frog.GetEmoji(), _frog.GetColor());
          x++;
        }
        else
        {
          Console.Write(" ");
        }
      }

      int filledWidth = 0;
      if (_snake.Position < _config.GameWidth)
        filledWidth += 2;
      if (_frog.Position < _config.GameWidth)
        filledWidth += 2;

      for (int i = filledWidth; i < _config.GameWidth; i++)
      {
        Console.Write(" ");
      }
    }

    private void RenderCharacter(string emoji, ConsoleColor color)
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

    private void RenderInstructions()
    {
      Console.WriteLine("操作: [A]左 [D]右 [Q]終了");
      Console.WriteLine();
      Console.WriteLine("┌────────────────────────────────────────┐");
      Console.WriteLine("│ カエル🐸: 左右キーで移動              │");
      Console.WriteLine("│ ヘビ🐍: カエルを追いかけます           │");
      Console.WriteLine("│ ヘビに捕まったらゲームオーバー          │");
      Console.WriteLine("└────────────────────────────────────────┘");
    }
  }
}
