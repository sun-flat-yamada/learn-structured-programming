using System;

using LearnStructuredProgramming.Section01_UnstructuredProgramming;
using LearnStructuredProgramming.Section02_StructuredProgramming;
using LearnStructuredProgramming.Section03_StructuredProgrammingPlus;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming;

namespace LearnStructuredProgramming
{
  /// <summary>
  /// 構造化プログラミング学習用サンプルプログラム
  ///
  /// このプログラムは、異なるプログラミング手法の比較学習のために設計されています。
  /// </summary>
  public static class Program
  {
    public static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;

      DisplayMenu();
    }

    private static void DisplayMenu()
    {
      Console.Clear();
      Console.WriteLine("╔════════════════════════════════════════════════════════╗");
      Console.WriteLine("║    構造化プログラミング学習用サンプルプログラム          ║");
      Console.WriteLine("╚════════════════════════════════════════════════════════╝");
      Console.WriteLine();

      Console.WriteLine("実行するサンプルを選択してください:");
      Console.WriteLine();
      Console.WriteLine("  [1] セクション1: 非構造化プログラミング (goto文使用)");
      Console.WriteLine("      - FrogVsSnakeGame: カエルVSヘビゲーム");
      Console.WriteLine();
      Console.WriteLine("  [2] セクション2: 構造化プログラミング (改善版)");
      Console.WriteLine("      - FrogVsSnakeGame: カエルVSヘビゲーム");
      Console.WriteLine();
      Console.WriteLine("  [3] セクション3: 構造化プログラミング+(完全対応版)");
      Console.WriteLine("      - FrogVsSnakeGame: カエルVSヘビゲーム");
      Console.WriteLine();
      Console.WriteLine("  [4] セクション4: オブジェクト指向プログラミング (ベストプラクティス)");
      Console.WriteLine("      - FrogVsSnakeGame: カエルVSヘビゲーム");
      Console.WriteLine();
      Console.WriteLine("  [0] 終了");
      Console.WriteLine();

      Console.Write("選択 (0-4): ");
      string? input = Console.ReadLine();

      if (input == "1")
      {
        var game = new Section01_UnstructuredProgramming.FrogVsSnakeGame();
        game.Run();
        // ゲーム終了後、メニューに戻る
        DisplayMenu();
      }
      else if (input == "2")
      {
        var game = new Section02_StructuredProgramming.FrogVsSnakeGame();
        game.Run();
        // ゲーム終了後、メニューに戻る
        DisplayMenu();
      }
      else if (input == "3")
      {
        var game = new Section03_StructuredProgrammingPlus.FrogVsSnakeGame();
        game.Run();
        // ゲーム終了後、メニューに戻る
        DisplayMenu();
      }
      else if (input == "4")
      {
        var game = new Section04_ObjectOrientedProgramming.FrogVsSnakeGame();
        game.Run();
        // ゲーム終了後、メニューに戻る
        DisplayMenu();
      }
      else if (input == "0")
      {
        Console.WriteLine("終了します。");
      }
      else
      {
        Console.WriteLine("無効な入力です。もう一度選択してください。");
        Console.ReadLine();
        DisplayMenu();
      }
    }
  }
}
