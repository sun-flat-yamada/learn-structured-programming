using System;

using LearnStructuredProgramming.Section00_UnstructuredProgramming;
using LearnStructuredProgramming.Section01_UnstructuredProgramming;
using LearnStructuredProgramming.Section02_StructuredProgramming;
using LearnStructuredProgramming.Section03_StructuredProgrammingPlus;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus;

namespace LearnStructuredProgramming;

/// <summary>
/// 構造化プログラミング学習用サンプルプログラム - エントリーポイント
///
/// ■ 概要
/// このプログラムは、プログラミングパラダイムの進化を体験的に学ぶための
/// 教育用サンプルです。同じ「カメ vs ワニ」ゲームを、異なる設計手法で
/// 実装することで、各パラダイムの特徴と利点を比較できます。
///
/// ■ 学習できる内容
/// - Section00: 非構造化プログラミング入門（グローバル変数、手続き的プログラミング）
/// - Section01: 非構造化プログラミング（グラフィック化、goto文によるフロー制御）
/// - Section02: 構造化プログラミング（関数分割、ループ、条件分岐）
/// - Section03: 構造化プログラミング拡張（2D移動、モジュール分割）
/// - Section04: オブジェクト指向プログラミング（クラス設計、SOLID原則）
/// - Section05: オブジェクト指向プログラミング拡張（ステートパターン、複数エンティティ）
///
/// ■ 実行方法
/// `dotnet run` でメニューが表示され、セクションを選択できます。
/// </summary>
public static class Program
{
  /// <summary>
  /// アプリケーションのエントリーポイント
  /// </summary>
  public static void Main(string[] args)
  {
    // 日本語絵文字を正しく表示するためUTF-8を設定
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    displayMenu();
  }

  /// <summary>
  /// メニュー画面を表示し、ユーザーの選択に応じてゲームを起動
  /// </summary>
  private static void displayMenu()
  {
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║    構造化プログラミング学習用サンプルプログラム          ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.WriteLine();

    Console.WriteLine("実行するサンプルを選択してください:");
    Console.WriteLine();
    Console.WriteLine("  [0] セクション0: 非構造化プログラミング (グローバル変数、手続き的プログラミング)");
    Console.WriteLine("      - TurtleVsCrocodileGame: カメ(🐢) vs ワニ(🐊) ゲーム");
    Console.WriteLine();
    Console.WriteLine("  [1] セクション1: 非構造化プログラミング (グラフィック化、goto文によるフロー制御)");
    Console.WriteLine("      - TurtleVsCrocodileGame: カメ(🐢) vs ワニ(🐊) ゲーム");
    Console.WriteLine();
    Console.WriteLine("  [2] セクション2: 構造化プログラミング (改善版)");
    Console.WriteLine("      - TurtleVsCrocodileGame: カメ(🐢) vs ワニ(🐊) ゲーム");
    Console.WriteLine();
    Console.WriteLine("  [3] セクション3: 構造化プログラミング (機能拡張版)");
    Console.WriteLine("      - TurtleVsCrocodileGame: カメ(🐢) vs ワニ(🐊) ゲーム");
    Console.WriteLine();
    Console.WriteLine("  [4] セクション4: オブジェクト指向プログラミング");
    Console.WriteLine("      - LifeGame: 生命の逃避行 🐢💨🐊");
    Console.WriteLine();
    Console.WriteLine("  [5] セクション5: オブジェクト指向プログラミング (拡張版)");
    Console.WriteLine("      - LifeGame Plus: 生命の逃避行 🐢 💨 🦎 💨 🐊");
    Console.WriteLine();
    Console.WriteLine("  [q] 終了");
    Console.WriteLine();

    Console.Write("選択 (0-5, q): ");
    string? input = Console.ReadLine();

    // 入力に応じたゲームを起動（再帰的にメニュー表示）
    if (input == "0")
    {
      var game = new Section00_UnstructuredProgramming.TurtleVsCrocodileGame();
      game.Run();
      displayMenu();
    }
    else if (input == "1")
    {
      var game = new Section01_UnstructuredProgramming.TurtleVsCrocodileGame();
      game.Run();
      displayMenu();
    }
    else if (input == "2")
    {
      var game = new Section02_StructuredProgramming.TurtleVsCrocodileGame();
      game.Run();
      displayMenu();
    }
    else if (input == "3")
    {
      var game = new Section03_StructuredProgrammingPlus.TurtleVsCrocodileGame();
      game.Run();
      displayMenu();
    }
    else if (input == "4")
    {
      var game = new Section04_ObjectOrientedProgramming.LifeGame();
      game.Run();
      displayMenu();
    }
    else if (input == "5")
    {
      var game = new Section05_ObjectOrientedProgrammingPlus.LifeGame();
      game.Run();
      displayMenu();
    }
    else if (input == "q" || input == "Q")
    {
      Console.WriteLine("終了します。");
    }
    else
    {
      // 不正入力時は再表示
      Console.WriteLine("無効な入力です。もう一度選択してください。");
      Console.ReadLine();
      displayMenu();
    }
  }
}
