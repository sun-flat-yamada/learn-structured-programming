using System;
using System.Text;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Presentation.Console
{
  /// <summary>
  /// コンソール画面へのゲームレンダリング実装
  ///
  /// ■ 責務
  /// ゲーム盤、スコア、ゲームオーバー画面等を
  /// コンソール上に描画します。絵文字や罫線で
  /// 視覚的にわかりやすいUIを提供します。
  /// </summary>
  public sealed class ConsoleGameRenderer : IGameRenderer
  {
    private const string GameTitle = "LifeGame - 生命の逃避行";
    private const int BorderWidth = 2;

    public void Initialize()
    {
      System.Console.OutputEncoding = Encoding.UTF8;
      System.Console.CursorVisible = false;
      System.Console.Clear();
    }

    public void Render(RenderContext context)
    {
      System.Console.SetCursorPosition(0, 0);

      RenderHeader();
      RenderGameBoard(context);
      RenderStatus(context);
      RenderInstructions();
    }

    public void RenderGameOver(int finalScore, int survivalTicks)
    {
      System.Console.Clear();

      RenderBox("ゲーム終了", ConsoleColor.Red);
      System.Console.WriteLine();

      System.Console.ForegroundColor = ConsoleColor.Yellow;
      System.Console.WriteLine("  🐊 ワニがカメを捕食しました！");
      System.Console.ResetColor();
      System.Console.WriteLine();

      System.Console.WriteLine($"  📊 最終スコア: {finalScore}");
      System.Console.WriteLine($"  ⏱️  生存時間: {survivalTicks} ティック");
      System.Console.WriteLine();

      RenderStatistics(finalScore, survivalTicks);

      System.Console.WriteLine();
      System.Console.ForegroundColor = ConsoleColor.DarkGray;
      System.Console.WriteLine("  Enterキーを押して終了...");
      System.Console.ResetColor();
      System.Console.ReadLine();
    }

    public void Cleanup()
    {
      System.Console.CursorVisible = true;
      System.Console.ResetColor();
    }

    private static void RenderHeader()
    {
      RenderBox(GameTitle, ConsoleColor.Cyan);
      System.Console.WriteLine();
    }

    private static void RenderBox(string title, ConsoleColor color)
    {
      var width = Math.Max(title.Length + 4, 40);
      var border = new string('═', width - 2);
      var padding = (width - 2 - title.Length) / 2;
      var paddedTitle = title.PadLeft(padding + title.Length).PadRight(width - 2);

      System.Console.ForegroundColor = color;
      System.Console.WriteLine($"╔{border}╗");
      System.Console.WriteLine($"║{paddedTitle}║");
      System.Console.WriteLine($"╚{border}╝");
      System.Console.ResetColor();
    }

    private void RenderGameBoard(RenderContext context)
    {
      RenderBoardBorder(context.BoardWidth, '┌', '─', '┐');

      for (int y = 0; y < context.BoardHeight; y++)
      {
        System.Console.Write("│");
        RenderRow(y, context);
        System.Console.WriteLine("│");
      }

      RenderBoardBorder(context.BoardWidth, '└', '─', '┘');
      System.Console.WriteLine();
    }

    private static void RenderBoardBorder(int width, char left, char middle, char right)
    {
      System.Console.Write(left);
      System.Console.Write(new string(middle, width));
      System.Console.WriteLine(right);
    }

    private void RenderRow(int y, RenderContext context)
    {
      var playerPos = context.Player.Position;
      var enemyPos = context.Enemy.Position;

      for (int x = 0; x < context.BoardWidth; x++)
      {
        if (x == enemyPos.X && y == enemyPos.Y)
        {
          RenderEntity(context.Enemy);
          x++; // 絵文字は2文字幅
        }
        else if (x == playerPos.X && y == playerPos.Y)
        {
          RenderEntity(context.Player);
          x++; // 絵文字は2文字幅
        }
        else
        {
          System.Console.Write(' ');
        }
      }
    }

    private static void RenderEntity(Entity entity)
    {
      System.Console.ForegroundColor = entity.Color;
      System.Console.Write(entity.Emoji);
      System.Console.ResetColor();
    }

    private static void RenderStatus(RenderContext context)
    {
      System.Console.ForegroundColor = ConsoleColor.White;
      System.Console.Write("  スコア: ");
      System.Console.ForegroundColor = ConsoleColor.Yellow;
      System.Console.Write($"{context.Score,6}");
      System.Console.ResetColor();

      System.Console.Write("  │  ");

      System.Console.ForegroundColor = ConsoleColor.White;
      System.Console.Write("距離: ");
      var distance = context.Player.DistanceTo(context.Enemy);
      System.Console.ForegroundColor = distance < 5 ? ConsoleColor.Red :
                                        distance < 10 ? ConsoleColor.Yellow :
                                        ConsoleColor.Green;
      System.Console.Write($"{distance,3}");
      System.Console.ResetColor();

      System.Console.WriteLine();
      System.Console.WriteLine();
    }

    private static void RenderInstructions()
    {
      System.Console.ForegroundColor = ConsoleColor.DarkGray;
      System.Console.WriteLine("┌─────────────────────────────────────────┐");
      System.Console.WriteLine("│  操作: [W/↑]上  [S/↓]下  [A/←]左  [D/→]右  │");
      System.Console.WriteLine("│        [Q/Esc]終了                       │");
      System.Console.WriteLine("├─────────────────────────────────────────┤");
      System.Console.WriteLine("│  🐢 カメ: あなたが操作                   │");
      System.Console.WriteLine("│  🐊 ワニ: カメを追跡中                   │");
      System.Console.WriteLine("└─────────────────────────────────────────┘");
      System.Console.ResetColor();
    }

    private static void RenderStatistics(int score, int ticks)
    {
      System.Console.ForegroundColor = ConsoleColor.Cyan;
      System.Console.WriteLine("  ┌─────────────────────────────┐");
      System.Console.WriteLine("  │        統計情報             │");
      System.Console.WriteLine("  ├─────────────────────────────┤");

      var rating = score switch
      {
        >= 500 => ("🏆 マスター", ConsoleColor.Yellow),
        >= 200 => ("⭐ エキスパート", ConsoleColor.Cyan),
        >= 100 => ("🎯 上級者", ConsoleColor.Green),
        >= 50 => ("📈 中級者", ConsoleColor.White),
        _ => ("🌱 初心者", ConsoleColor.DarkGray)
      };

      System.Console.Write("  │  評価: ");
      System.Console.ForegroundColor = rating.Item2;
      System.Console.Write($"{rating.Item1,-15}");
      System.Console.ForegroundColor = ConsoleColor.Cyan;
      System.Console.WriteLine("│");
      System.Console.WriteLine("  └─────────────────────────────┘");
      System.Console.ResetColor();
    }
  }
}
