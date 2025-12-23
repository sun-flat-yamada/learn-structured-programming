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
  /// - カメが左右に移動します
  /// - ワニがカメを追いかけます
  /// - ワニがカメに追いついたらゲームオーバーになります
  /// - カメはワニが近づいてきたら反対方向に逃げます
  /// </summary>
  public class FrogVsSnakeGame
  {
    // ゲームパラメータ
    private const int GameWidth = 32;
    private const int GameHeight = 1;
    private const int InitialTurtlePosition = 20;
    private const int InitialCrocodilePosition = 5;

    private int turtlePosition;
    private int crocodilePosition;
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

      // ワニの移動
      MoveCrocodile();

      // 衝突判定
      if (turtlePosition == crocodilePosition)
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
      turtlePosition = InitialTurtlePosition;
      crocodilePosition = InitialCrocodilePosition;
      score = 0;
      gameActive = true;
      random = new Random();
    }

    private void DisplayGame()
    {
      Console.Clear();
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カメVSワニゲーム                ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
      Console.WriteLine($"スコア: {score} | 操作: [A]左 [D]右 [Q]終了");
      Console.WriteLine();

      // ゲーム画面の描画
      DrawGameBoard();

      Console.WriteLine();
      Console.WriteLine("┌────────────────────────────────────────┐");
      Console.WriteLine("│ カメ🐢: 左右矢印で移動                │");
      Console.WriteLine("│ ワニ🐊: カメを追いかけます             │");
      Console.WriteLine("│ ワニに捕まったらゲームオーバー          │");
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

      // ゲーム領域（1行目のみ）
      for (int y = 0; y < GameHeight; y++)
      {
        Console.Write("║");

        for (int x = 0; x < GameWidth; x++)
        {
          if (x == crocodilePosition)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("🐊");
            Console.ResetColor();
            x++; // Unicodeキャラクタは幅が2なので、カウンタを進める
          }
          else if (x == turtlePosition)
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("🐢");
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
        if (crocodilePosition < GameWidth)
          filledWidth += 2;
        if (turtlePosition < GameWidth)
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
      // カメのデフォルト動作（ワニが近づいたら反対方向に逃げる）
      MoveTurtleAwayFromCrocodile();

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
          if (turtlePosition > 0)
          {
            turtlePosition--;
          }
          break;

        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
          if (turtlePosition < GameWidth - 2)
          {
            turtlePosition++;
          }
          break;

        case ConsoleKey.Q:
          gameActive = false;
          break;

        default:
          break;
      }
    }

    private void MoveTurtleAwayFromCrocodile()
    {
      // ワニが近づいてきたら反対方向に逃げる
      int distance = Math.Abs(turtlePosition - crocodilePosition);

      // ワニが近くにいる場合（5マス以内）
      if (distance <= 5)
      {
        if (crocodilePosition < turtlePosition)
        {
          // ワニが左にいるので右に逃げる
          if (turtlePosition < GameWidth - 2)
          {
            turtlePosition++;
          }
        }
        else if (crocodilePosition > turtlePosition)
        {
          // ワニが右にいるので左に逃げる
          if (turtlePosition > 0)
          {
            turtlePosition--;
          }
        }
      }
    }

    private void MoveCrocodile()
    {
      // ワニの簡単なAI: カメに向かって移動
      if (crocodilePosition < turtlePosition)
      {
        crocodilePosition++;
      }
      else if (crocodilePosition > turtlePosition)
      {
        crocodilePosition--;
      }
      // crocodilePosition == turtlePosition の場合は移動しない（衝突判定で処理）
    }

    private void DisplayGameOver()
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
      Console.WriteLine($"最終スコア: {score}");
      Console.WriteLine();
      Console.WriteLine("Enterキーを押して終了...");
      Console.ReadLine();
    }
  }
}
