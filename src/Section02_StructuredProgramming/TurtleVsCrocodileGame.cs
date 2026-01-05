using System;
using System.Threading;

namespace LearnStructuredProgramming.Section02_StructuredProgramming;

/// <summary>
/// カメ vs ワニゲーム - 構造化プログラミング版
///
/// ■ 教育目的
/// Section01の非構造化プログラミング(goto文)を、構造化された制御構文
/// （ループ・条件分岐・関数呼び出し）で書き直した改善版です。
///
/// ■ 構造化プログラミングの特徴
/// - 順次・選択・反復の3構造のみで制御
/// - 関数による処理の分割（責務の分離）
/// - static関数とグローバル状態（まだOOP化していない）
/// - goto文を排除し、可読性と保守性を向上
///
/// ■ ファイル構成
/// - GameState: ゲーム状態の保持
/// - GameRules: ルール定数と境界判定
/// - GameRenderer: 画面描画
/// - InputHandler: 入力処理
/// - CharacterMovement: キャラ移動ロジック
/// - GameLogic: 衝突判定等
///
/// ■ 制限事項
/// オブジェクト指向設計は適用していません。
/// カプセル化やポリモーフィズムはSection04以降で学びます。
/// </summary>
public class TurtleVsCrocodileGame
{
  /// <summary>
  /// ゲームを実行（メインループ）
  /// </summary>
  public void Run()
  {
    setupConsole();

    GameState.Initialize(GameRules.INITIAL_TURTLE_POSITION, GameRules.INITIAL_CROCODILE_POSITION);

    // 構造化されたゲームループ（whileによる繰り返し）
    while (GameState.IsActive)
    {
      executeGameTick();
    }

    displayGameOver();
    restoreConsole();
  }

  /// <summary>
  /// コンソール初期設定
  /// </summary>
  private static void setupConsole()
  {
    Console.Clear();
    Console.CursorVisible = false;
  }

  /// <summary>
  /// コンソール設定を復元
  /// </summary>
  private static void restoreConsole()
  {
    Console.CursorVisible = true;
  }

  /// <summary>
  /// 1フレーム分のゲーム処理を実行
  /// </summary>
  private static void executeGameTick()
  {
    GameRenderer.RenderGameScreen();

    if (!GameState.IsActive)
    {
      return;
    }

    // 入力処理（入力なしなら自動逃走）
    InputHandler.ProcessInput();

    // 敵移動
    CharacterMovement.MoveCrocodileTowardsTurtle();

    // 衝突判定
    if (GameLogic.IsCollisionDetected())
    {
      GameState.End();
      return;
    }

    GameState.IncrementScore();

    // フレーム間隔制御
    Thread.Sleep(GameRules.GAME_UPDATE_DELAY_MS);
  }

  /// <summary>
  /// ゲームオーバー画面表示
  /// </summary>
  private static void displayGameOver()
  {
    GameRenderer.RenderGameOverScreen();
  }
}
