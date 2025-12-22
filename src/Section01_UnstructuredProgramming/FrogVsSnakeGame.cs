using System;
using System.Threading;

namespace LearnStructuredProgramming.Section01_UnstructuredProgramming
{
  /// <summary>
  /// ジャンプコード（goto文）を使用した非構造化プログラムの例
  ///
  /// このプログラムは構造化プログラミングの手法を意図的に適用せず、
  /// goto文を使用してプログラムフローを制御しています。
  ///
  /// ゲーム内容：
  /// - カエルが左右に移動します
  /// - ヘビがカエルを追いかけます
  /// - ヘビがカエルに追いついたらゲームオーバーになります
  /// </summary>
  public class FrogVsSnakeGame
  {
    // ゲームパラメータ
    private const int GameWidth = 40;
    private const int GameHeight = 10;
    private const int InitialFrogPosition = 20;
    private const int InitialSnakePosition = 5;

    private int frogPosition;
    private int snakePosition;
    private int score;
    private bool gameActive;
    private Random random;

    public void Run()
    {
      Console.Clear();
      Console.CursorVisible = false;

      // ゲーム初期化開始地点
      InitializeGame();

      // メインループ開始地点
    MainLoop:

      DisplayGame();

      // ゲームオーバー判定
      if (!gameActive)
      {
        goto GameOver;
      }

      // ユーザー入力処理
      ProcessInput();

      // ヘビの移動
      MoveSnake();

      // 衝突判定
      if (frogPosition == snakePosition)
      {
        gameActive = false;
        goto GameOver;
      }

      // スコア加算
      score++;

      // スリープ（ゲーム速度調整）
      Thread.Sleep(200);

      // メインループへ戻る
      goto MainLoop;

      // ゲームオーバー処理
    GameOver:
      DisplayGameOver();

      // プログラム終了
      Console.CursorVisible = true;
    }

    private void InitializeGame()
    {
      frogPosition = InitialFrogPosition;
      snakePosition = InitialSnakePosition;
      score = 0;
      gameActive = true;
      random = new Random();
    }

    private void DisplayGame()
    {
      Console.Clear();
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カエルVSヘビゲーム              ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
      Console.WriteLine($"スコア: {score} | 操作: [A]左 [D]右 [Q]終了");
      Console.WriteLine();

      // ゲーム画面の描画
      DrawGameBoard();

      Console.WriteLine();
      Console.WriteLine("┌────────────────────────────────────────┐");
      Console.WriteLine("│ カエル🐸: 左右矢印で移動              │");
      Console.WriteLine("│ ヘビ🐍: カエルを追いかけます           │");
      Console.WriteLine("│ ヘビに捕まったらゲームオーバー          │");
      Console.WriteLine("└────────────────────────────────────────┘");
    }

    private void DrawGameBoard()
    {
      // ゲームボード上部枠線
      Console.Write("║");
      for (int i = 0; i < GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");

      // ゲーム領域
      for (int y = 0; y < GameHeight; y++)
      {
        Console.Write("║");

        for (int x = 0; x < GameWidth; x++)
        {
          if (x == snakePosition)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("🐍");
            Console.ResetColor();
            x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
          }
          else if (x == frogPosition)
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("🐸");
            Console.ResetColor();
            x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
          }
          else
          {
            Console.Write(" ");
          }
        }

        // 行の残り部分を埋める
        int filledWidth = 0;
        if (snakePosition < GameWidth)
          filledWidth += 2;
        if (frogPosition < GameWidth)
          filledWidth += 2;

        for (int i = filledWidth; i < GameWidth; i++)
        {
          Console.Write(" ");
        }

        Console.WriteLine("║");
      }

      // ゲームボード下部枠線
      Console.Write("║");
      for (int i = 0; i < GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    private void ProcessInput()
    {
      // カエルのデフォルト動作（ランダムに左右に移動）
      MoveFrogRandomly();

      // ユーザーのキー入力処理
      try
      {
        if (!Console.KeyAvailable)
        {
          return;
        }
      }
      catch (InvalidOperationException)
      {
        // コンソール入力がリダイレクトされている環境では KeyAvailable は使用不可
        return;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);

      // キー入力判定（goto文のない単純な処理）
      switch (keyInfo.Key)
      {
        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
          if (frogPosition > 0)
          {
            frogPosition--;
          }
          break;

        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
          if (frogPosition < GameWidth - 2)
          {
            frogPosition++;
          }
          break;

        case ConsoleKey.Q:
          gameActive = false;
          break;

        default:
          break;
      }
    }

    private void MoveFrogRandomly()
    {
      // 60%の確率でランダムに左右に移動
      if (random.Next(100) < 60)
      {
        if (random.Next(2) == 0)
        {
          // 左に移動
          if (frogPosition > 0)
          {
            frogPosition--;
          }
        }
        else
        {
          // 右に移動
          if (frogPosition < GameWidth - 2)
          {
            frogPosition++;
          }
        }
      }
    }

    private void MoveSnake()
    {
      // ヘビの簡単なAI: カエルに向かって移動
      if (snakePosition < frogPosition)
      {
        snakePosition++;
      }
      else if (snakePosition > frogPosition)
      {
        snakePosition--;
      }
      // snakePosition == frogPosition の場合は移動しない（衝突判定で処理）
    }

    private void DisplayGameOver()
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
      Console.WriteLine($"最終スコア: {score}");
      Console.WriteLine();
      Console.WriteLine("Enterキーを押して終了...");
      Console.ReadLine();
    }
  }
}
