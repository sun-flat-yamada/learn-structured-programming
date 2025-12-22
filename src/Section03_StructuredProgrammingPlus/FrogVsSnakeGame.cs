using System;
using System.Threading;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// 構造化プログラミングのみを使用したカエルVSヘビゲーム（完全対応版）
  ///
  /// 特徴：
  /// - グローバル変数を使用したゲーム状態管理
  /// - 手続き型プログラミング（関数の線形実行）
  /// - 副作用に基づくデータ操作
  /// - クラスやオブジェクト指向設計なし
  /// - goto文を使わず、ループと条件分岐で制御
  ///
  /// 【改修内容】
  /// - カエルの移動を行ごとにランダム化：3段階確率分岐（左30%、右30%、移動なし40%）
  /// - 毎フレーム新しいランダム値を生成し、各行で独立した確率判定を実行
  /// </summary>
  public class FrogVsSnakeGame
  {
    public void Run()
    {
      SetupConsole();

      // ゲーム状態を初期化
      GameState.Initialize(GameRules.InitialFrogPosition, GameRules.InitialSnakePosition);

      // メインゲームループ
      while (GameState.IsActive)
      {
        ExecuteGameTick();
      }

      DisplayGameOver();
      RestoreConsole();
    }

    private static void SetupConsole()
    {
      Console.Clear();
      Console.CursorVisible = false;
    }

    private static void RestoreConsole()
    {
      Console.CursorVisible = true;
    }

    private static void ExecuteGameTick()
    {
      // 画面を描画
      GameRenderer.RenderGameScreen();

      if (!GameState.IsActive)
      {
        return;
      }

      // ユーザー入力を処理
      // 入力がない場合：CharacterMovement.MoveFrogRandomly()を呼び出す
      // 毎フレーム新しいランダム判定が行われ、カエルが行ごとに異なる確率で移動する
      InputHandler.ProcessInput();

      // ヘビを移動
      CharacterMovement.MoveSnakeTowardsFrog();

      // 衝突判定
      if (GameLogic.IsCollisionDetected())
      {
        GameState.End();
        return;
      }

      // スコア加算
      GameState.IncrementScore();

      // ゲーム更新速度制御
      Thread.Sleep(GameRules.GameUpdateDelayMs);
    }

    private static void DisplayGameOver()
    {
      GameRenderer.RenderGameOverScreen();
    }
  }
}
