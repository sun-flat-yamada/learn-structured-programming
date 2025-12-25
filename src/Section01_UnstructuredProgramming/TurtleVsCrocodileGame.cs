using System;
using System.Threading;

namespace LearnStructuredProgramming.Section01_UnstructuredProgramming
{
  /// <summary>
  /// カメ vs ワニゲーム - 非構造化プログラミング版
  ///
  /// ■ 教育目的
  /// このクラスは「非構造化プログラミング」の問題点を示すための反面教師です。
  /// goto文を使用したスパゲッティコードがいかに読みにくく、保守困難かを体験できます。
  ///
  /// ■ 非構造化プログラミングの特徴
  /// - goto文によるフロー制御（ラベルジャンプ）
  /// - 処理の流れが追いにくい
  /// - 変更時の影響範囲が予測困難
  /// - ダイクストラ氏が1968年に「goto文は有害」と提唱
  ///
  /// ■ ゲームルール
  /// - カメ🐢: 左右に移動、ワニが近づくと自動で逃げる
  /// - ワニ🐊: カメを追跡
  /// - ワニがカメに追いつくとゲームオーバー
  ///
  /// ■ 注意
  /// 実際の開発ではgoto文を避け、構造化された制御構文を使用してください。
  /// </summary>
  public class TurtleVsCrocodileGame
  {
    // ゲームボードの定数
    private const int GameWidth = 32;
    private const int GameHeight = 1;
    private const int InitialTurtlePosition = 20;
    private const int InitialCrocodilePosition = 5;

    // キャラクター位置
    private int turtlePosition;
    private int crocodilePosition;

    // ゲーム状態
    private int score;
    private bool gameActive;
    private Random random = new Random();

    /// <summary>
    /// ゲームを実行（goto文によるメインループ）
    /// </summary>
    public void Run()
    {
      Console.Clear();
      Console.CursorVisible = false;

      // ゲーム初期化
      InitializeGame();

      // メインループ（gotoラベル）
    MainLoop:

      DisplayGame();

      // 終了判定
      if (!gameActive)
      {
        goto GameOver;
      }

      // 入力処理
      ProcessInput();

      // 敵の移動
      MoveCrocodile();

      // 衝突判定
      if (turtlePosition == crocodilePosition)
      {
        gameActive = false;
        goto GameOver;
      }

      score++;

      // フレーム間隔
      Thread.Sleep(200);

      // ループ継続（gotoによるジャンプ）
      goto MainLoop;

      // 終了処理（gotoラベル）
    GameOver:
      DisplayGameOver();
      Console.CursorVisible = true;
    }

    /// <summary>
    /// ゲーム状態を初期値にリセット
    /// </summary>
    private void InitializeGame()
    {
      turtlePosition = InitialTurtlePosition;
      crocodilePosition = InitialCrocodilePosition;
      score = 0;
      gameActive = true;
      random = new Random();
    }

    /// <summary>
    /// 現在のゲーム画面を描画
    /// </summary>
    private void DisplayGame()
    {
      Console.Clear();
      Console.WriteLine("╔════════════════════════════════════════╗");
      Console.WriteLine("║      カメVSワニゲーム                ║");
      Console.WriteLine("╚════════════════════════════════════════╝");
      Console.WriteLine();
      Console.WriteLine($"スコア: {score} | 操作: [A]左 [D]右 [Q]終了");
      Console.WriteLine();

      DrawGameBoard();

      Console.WriteLine();
      Console.WriteLine("┌────────────────────────────────────────┐");
      Console.WriteLine("│ カメ🐢: 左右矢印で移動                │");
      Console.WriteLine("│ ワニ🐊: カメを追いかけます             │");
      Console.WriteLine("│ ワニに捕まったらゲームオーバー          │");
      Console.WriteLine("└────────────────────────────────────────┘");
    }

    /// <summary>
    /// ゲームボード（キャラクター配置）を描画
    /// </summary>
    private void DrawGameBoard()
    {
      // 上枠
      Console.Write("║");
      for (int i = 0; i < GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");

      // ゲームエリア
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
            x++; // 絵文字は2文字幅
          }
          else if (x == turtlePosition)
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("🐢");
            Console.ResetColor();
            x++; // 絵文字は2文字幅
          }
          else
          {
            Console.Write(" ");
          }
        }

        // 残りスペースを埋める
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

      // 下枠
      Console.Write("║");
      for (int i = 0; i < GameWidth; i++)
      {
        Console.Write("─");
      }
      Console.WriteLine("║");
    }

    /// <summary>
    /// ユーザー入力とカメの自動逃走を処理
    /// </summary>
    private void ProcessInput()
    {
      // 入力がなければ自動で逃走行動
      MoveTurtleAwayFromCrocodile();

      try
      {
        if (!Console.KeyAvailable)
        {
          return;
        }
      }
      catch (InvalidOperationException)
      {
        // リダイレクト環境ではKeyAvailable不可
        return;
      }

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);

      // キー入力に応じた移動
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

    /// <summary>
    /// カメをワニから遠ざける（ワニが5マス以内のとき）
    /// </summary>
    private void MoveTurtleAwayFromCrocodile()
    {
      int distance = Math.Abs(turtlePosition - crocodilePosition);

      // 危険距離内なら逃走
      if (distance <= 5)
      {
        if (crocodilePosition < turtlePosition)
        {
          // ワニが左にいるので右へ
          if (turtlePosition < GameWidth - 2)
          {
            turtlePosition++;
          }
        }
        else if (crocodilePosition > turtlePosition)
        {
          // ワニが右にいるので左へ
          if (turtlePosition > 0)
          {
            turtlePosition--;
          }
        }
      }
    }

    /// <summary>
    /// ワニをカメに向かって移動
    /// </summary>
    private void MoveCrocodile()
    {
      // 単純追跡AI
      if (crocodilePosition < turtlePosition)
      {
        crocodilePosition++;
      }
      else if (crocodilePosition > turtlePosition)
      {
        crocodilePosition--;
      }
    }

    /// <summary>
    /// ゲームオーバー画面を表示
    /// </summary>
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
