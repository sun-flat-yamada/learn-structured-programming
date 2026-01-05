using System;
using System.Threading;

namespace LearnStructuredProgramming.Section01_UnstructuredProgramming;

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
  private const int GAME_WIDTH = 32;
  private const int GAME_HEIGHT = 1;
  private const int INITIAL_TURTLE_POSITION = 20;
  private const int INITIAL_CROCODILE_POSITION = 5;

  // キャラクター位置
  private int _turtlePosition;
  private int _crocodilePosition;

  // ゲーム状態
  private int _score;
  private bool _gameActive;
  private Random _random = new();

  /// <summary>
  /// ゲームを実行（goto文によるメインループ）
  /// </summary>
  public void Run()
  {
    Console.Clear();
    Console.CursorVisible = false;

    // ゲーム初期化
    initializeGame();

  // メインループ（gotoラベル）
  MainLoop:

    displayGame();

    // 終了判定
    if (!_gameActive)
    {
      goto GameOver;
    }

    // 入力処理
    processInput();

    // 敵の移動
    moveCrocodile();

    // 衝突判定
    if (_turtlePosition == _crocodilePosition)
    {
      _gameActive = false;
      goto GameOver;
    }

    _score++;

    // フレーム間隔
    Thread.Sleep(200);

    // ループ継続（gotoによるジャンプ）
    goto MainLoop;

  // 終了処理（gotoラベル）
  GameOver:
    displayGameOver();
    Console.CursorVisible = true;
  }

  /// <summary>
  /// ゲーム状態を初期値にリセット
  /// </summary>
  private void initializeGame()
  {
    _turtlePosition = INITIAL_TURTLE_POSITION;
    _crocodilePosition = INITIAL_CROCODILE_POSITION;
    _score = 0;
    _gameActive = true;
    _random = new Random();
  }

  /// <summary>
  /// 現在のゲーム画面を描画
  /// </summary>
  private void displayGame()
  {
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════╗");
    Console.WriteLine("║      カメVSワニゲーム                ║");
    Console.WriteLine("╚════════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine($"スコア: {_score} | 操作: [A]左 [D]右 [Q]終了");
    Console.WriteLine();

    drawGameBoard();

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
  private void drawGameBoard()
  {
    // 上枠
    Console.Write("║");
    for (int i = 0; i < GAME_WIDTH; i++)
    {
      Console.Write("─");
    }
    Console.WriteLine("║");

    // ゲームエリア
    for (int y = 0; y < GAME_HEIGHT; y++)
    {
      Console.Write("║");

      for (int x = 0; x < GAME_WIDTH; x++)
      {
        if (x == _crocodilePosition)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("🐊");
          Console.ResetColor();
          x++; // 絵文字は2文字幅
        }
        else if (x == _turtlePosition)
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
      if (_crocodilePosition < GAME_WIDTH)
        filledWidth += 2;
      if (_turtlePosition < GAME_WIDTH)
        filledWidth += 2;

      for (int i = filledWidth; i < GAME_WIDTH; i++)
      {
        Console.Write(" ");
      }

      Console.WriteLine("║");
    }

    // 下枠
    Console.Write("║");
    for (int i = 0; i < GAME_WIDTH; i++)
    {
      Console.Write("─");
    }
    Console.WriteLine("║");
  }

  /// <summary>
  /// ユーザー入力とカメの自動逃走を処理
  /// </summary>
  private void processInput()
  {
    // 入力がなければ自動で逃走行動
    moveTurtleAwayFromCrocodile();

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
        if (_turtlePosition > 0)
        {
          _turtlePosition--;
        }
        break;

      case ConsoleKey.D:
      case ConsoleKey.RightArrow:
        if (_turtlePosition < GAME_WIDTH - 2)
        {
          _turtlePosition++;
        }
        break;

      case ConsoleKey.Q:
        _gameActive = false;
        break;

      default:
        break;
    }
  }

  /// <summary>
  /// カメをワニから遠ざける（ワニが5マス以内のとき）
  /// </summary>
  private void moveTurtleAwayFromCrocodile()
  {
    int distance = Math.Abs(_turtlePosition - _crocodilePosition);

    // 危険距離内なら逃走
    if (distance <= 5)
    {
      if (_crocodilePosition < _turtlePosition)
      {
        // ワニが左にいるので右へ
        if (_turtlePosition < GAME_WIDTH - 2)
        {
          _turtlePosition++;
        }
      }
      else if (_crocodilePosition > _turtlePosition)
      {
        // ワニが右にいるので左へ
        if (_turtlePosition > 0)
        {
          _turtlePosition--;
        }
      }
    }
  }

  /// <summary>
  /// ワニをカメに向かって移動
  /// </summary>
  private void moveCrocodile()
  {
    // 単純追跡AI
    if (_crocodilePosition < _turtlePosition)
    {
      _crocodilePosition++;
    }
    else if (_crocodilePosition > _turtlePosition)
    {
      _crocodilePosition--;
    }
  }

  /// <summary>
  /// ゲームオーバー画面を表示
  /// </summary>
  private void displayGameOver()
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
    Console.WriteLine($"最終スコア: {_score}");
    Console.WriteLine();
    Console.WriteLine("Enterキーを押して終了...");
    Console.ReadLine();
  }
}
