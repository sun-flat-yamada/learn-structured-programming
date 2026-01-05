using System;
using System.Text;

using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Presentation.Console;

/// <summary>
/// コンソール画面へのゲームレンダリング実装
///
/// ■ 責務
/// ゲーム盤、スコア、トカゲステータス、ゲームオーバー画面等を
/// コンソール上に描画します。絵文字や罫線で
/// 視覚的にわかりやすいUIを提供します。
/// </summary>
public sealed class ConsoleGameRenderer : IGameRenderer
{
  private const string GAME_TITLE = "LifeGame Plus - 生命の逃避行";
  private const int BORDER_WIDTH = 2;

  public void Initialize()
  {
    System.Console.OutputEncoding = Encoding.UTF8;
    System.Console.CursorVisible = false;
    System.Console.Clear();
  }

  public void Render(RenderContext context)
  {
    System.Console.SetCursorPosition(0, 0);

    renderHeader();
    renderGameBoard(context);
    renderStatus(context);
    renderLizardStatus(context);
    renderInstructions();
  }

  public void RenderGameOver(GameOverEventArgs gameOverArgs)
  {
    System.Console.Clear();

    renderBox("ゲーム終了", ConsoleColor.Red);
    System.Console.WriteLine();

    renderGameOverMessage(gameOverArgs);
    System.Console.WriteLine();

    System.Console.WriteLine($"  📊 最終スコア: {gameOverArgs.FinalScore}");
    System.Console.WriteLine($"  ⏱️  生存時間: {gameOverArgs.SurvivalTicks} ティック");
    System.Console.WriteLine();

    renderStatistics(gameOverArgs.FinalScore, gameOverArgs.SurvivalTicks);

    System.Console.WriteLine();
    System.Console.ForegroundColor = ConsoleColor.DarkGray;
    System.Console.WriteLine("  Enterキーを押して終了...");
    System.Console.ResetColor();
    System.Console.ReadLine();
  }

  private static void renderGameOverMessage(GameOverEventArgs args)
  {
    System.Console.ForegroundColor = ConsoleColor.Yellow;

    switch (args.Reason)
    {
      case GameOverReason.AllCaught:
        System.Console.WriteLine("  🐊 ワニがカメとトカゲを両方捕食しました！");
        break;
      case GameOverReason.PlayerCaught:
        System.Console.WriteLine("  🐊 ワニがカメを捕食しました！");
        break;
      case GameOverReason.LizardCaught:
        System.Console.WriteLine("  🐊 ワニがトカゲを捕食しました！");
        break;
      case GameOverReason.PlayerQuit:
        System.Console.WriteLine("  👋 ゲームを終了しました");
        break;
      case GameOverReason.TimeUp:
        System.Console.WriteLine("  ⏰ 時間切れです");
        break;
    }

    System.Console.ResetColor();
  }

  public void Cleanup()
  {
    System.Console.CursorVisible = true;
    System.Console.ResetColor();
  }

  private static void renderHeader()
  {
    renderBox(GAME_TITLE, ConsoleColor.Cyan);
    System.Console.WriteLine();
  }

  private static void renderBox(string title, ConsoleColor color)
  {
    var width = Math.Max(title.Length + 4, 44);
    var border = new string('═', width - 2);
    var padding = (width - 2 - title.Length) / 2;
    var paddedTitle = title.PadLeft(padding + title.Length).PadRight(width - 2);

    System.Console.ForegroundColor = color;
    System.Console.WriteLine($"╔{border}╗");
    System.Console.WriteLine($"║{paddedTitle}║");
    System.Console.WriteLine($"╚{border}╝");
    System.Console.ResetColor();
  }

  private void renderGameBoard(RenderContext context)
  {
    renderBoardBorder(context.BoardWidth, '┌', '─', '┐');

    for (int y = 0; y < context.BoardHeight; y++)
    {
      System.Console.Write("│");
      renderRow(y, context);
      System.Console.WriteLine("│");
    }

    renderBoardBorder(context.BoardWidth, '└', '─', '┘');
    System.Console.WriteLine();
  }

  private static void renderBoardBorder(int width, char left, char middle, char right)
  {
    System.Console.Write(left);
    System.Console.Write(new string(middle, width));
    System.Console.WriteLine(right);
  }

  private void renderRow(int y, RenderContext context)
  {
    var playerPos = context.Player.Position;
    var enemyPos = context.Enemy.Position;
    var lizardPos = context.Lizard.Position;
    var tail = context.Lizard.DroppedTail;
    var tailPos = tail?.Position;

    for (int x = 0; x < context.BoardWidth; x++)
    {
      var currentPos = new Position(x, y);

      if (x == enemyPos.X && y == enemyPos.Y)
      {
        renderEntity(context.Enemy);
        x++; // 絵文字は2文字幅
      }
      else if (context.IsPlayerAlive && x == playerPos.X && y == playerPos.Y)
      {
        renderEntity(context.Player);
        x++; // 絵文字は2文字幅
      }
      else if (context.IsLizardAlive && x == lizardPos.X && y == lizardPos.Y)
      {
        renderLizard(context.Lizard);
        x++; // 絵文字は2文字幅
      }
      else if (tail != null && tail.IsActive && tailPos.HasValue && x == tailPos.Value.X && y == tailPos.Value.Y)
      {
        renderEntity(tail);
        x++; // 絵文字は2文字幅
      }
      else
      {
        System.Console.Write(' ');
      }
    }
  }

  private static void renderEntity(Entity entity)
  {
    System.Console.ForegroundColor = entity.Color;
    System.Console.Write(entity.Emoji);
    System.Console.ResetColor();
  }

  private static void renderLizard(Lizard lizard)
  {
    // 尻尾を切り離した後は背景を黄色にする
    if (lizard.State == LizardState.TailDropped)
    {
      System.Console.BackgroundColor = ConsoleColor.Yellow;
    }

    // 倍速モード中は点滅風の表現
    if (lizard.IsSpeedBoosted)
    {
      System.Console.ForegroundColor = ConsoleColor.Magenta;
    }
    else
    {
      System.Console.ForegroundColor = lizard.Color;
    }
    System.Console.Write(lizard.Emoji);
    System.Console.ResetColor();
  }

  private static void renderStatus(RenderContext context)
  {
    System.Console.ForegroundColor = ConsoleColor.White;
    System.Console.Write("  スコア: ");
    System.Console.ForegroundColor = ConsoleColor.Yellow;
    System.Console.Write($"{context.Score,6}");
    System.Console.ResetColor();

    System.Console.Write("  │  ");

    System.Console.ForegroundColor = ConsoleColor.White;
    System.Console.Write("カメ: ");
    if (context.IsPlayerAlive)
    {
      var playerDistance = context.Player.DistanceTo(context.Enemy);
      System.Console.ForegroundColor = playerDistance < 5 ? ConsoleColor.Red :
                                        playerDistance < 10 ? ConsoleColor.Yellow :
                                        ConsoleColor.Green;
      System.Console.Write($"距離{playerDistance,3}");
    }
    else
    {
      System.Console.ForegroundColor = ConsoleColor.DarkGray;
      System.Console.Write("💀捕食済");
    }
    System.Console.ResetColor();

    System.Console.WriteLine();
  }

  private static void renderLizardStatus(RenderContext context)
  {
    var lizard = context.Lizard;

    System.Console.Write("  トカゲ: ");

    if (context.IsLizardAlive)
    {
      var lizardDistance = lizard.DistanceTo(context.Enemy);

      switch (lizard.State)
      {
        case LizardState.Wandering:
          System.Console.ForegroundColor = ConsoleColor.Green;
          System.Console.Write("🚶 うろうろ");
          break;
        case LizardState.Fleeing:
          System.Console.ForegroundColor = ConsoleColor.Yellow;
          System.Console.Write("🏃 逃走中");
          break;
        case LizardState.TailDropped:
          System.Console.ForegroundColor = ConsoleColor.Magenta;
          System.Console.Write(lizard.IsSpeedBoosted ? "⚡ 倍速逃走!" : "💨 尻尾なし");
          break;
      }
      System.Console.ResetColor();

      System.Console.Write("  │  ");

      System.Console.ForegroundColor = ConsoleColor.White;
      System.Console.Write("距離: ");
      System.Console.ForegroundColor = lizardDistance < 5 ? ConsoleColor.Red :
                                        lizardDistance < 10 ? ConsoleColor.Yellow :
                                        ConsoleColor.Green;
      System.Console.Write($"{lizardDistance,3}");
      System.Console.ResetColor();
    }
    else
    {
      System.Console.ForegroundColor = ConsoleColor.DarkGray;
      System.Console.Write("💀 捕食済");
      System.Console.ResetColor();
    }

    if (context.TailsEaten > 0)
    {
      System.Console.Write("  │  ");
      System.Console.ForegroundColor = ConsoleColor.DarkYellow;
      System.Console.Write($"食べられた尻尾: {context.TailsEaten}");
      System.Console.ResetColor();
    }

    System.Console.WriteLine();
    System.Console.WriteLine();
  }

  private static void renderInstructions()
  {
    System.Console.ForegroundColor = ConsoleColor.DarkGray;
    System.Console.WriteLine("┌───────────────────────────────────────────┐");
    System.Console.WriteLine("│  操作: [W/↑]上  [S/↓]下  [A/←]左  [D/→]右    │");
    System.Console.WriteLine("│        [Q/Esc]終了                         │");
    System.Console.WriteLine("├───────────────────────────────────────────┤");
    System.Console.WriteLine("│  🐢 カメ: あなたが操作                     │");
    System.Console.WriteLine("│  🐊 ワニ: カメ・トカゲを追跡中             │");
    System.Console.WriteLine("│  🦎 トカゲ: ワニから逃げる仲間              │");
    System.Console.WriteLine("│     └─ 危険時に尻尾を切って倍速逃走！       │");
    System.Console.WriteLine("└───────────────────────────────────────────┘");
    System.Console.ResetColor();
  }

  private static void renderStatistics(int score, int ticks)
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
