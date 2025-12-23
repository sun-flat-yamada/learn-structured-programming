using System;
using System.Threading;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミングのみを使用したカメVSワニゲーム
  ///
  /// 特徴：
  /// - グローバル変数を使用したゲーム状態管理
  /// - 手続き型プログラミング（関数の線形実行）
  /// - 副作用に基づくデータ操作
  /// - クラスやオブジェクト指向設計なし
  /// - goto文を使わず、ループと条件分岐で制御
  /// - カメはワニが近づいてきたら反対方向に逃げる
  /// </summary>
  public class FrogVsSnakeGame
  {
    public void Run()
    {
      SetupConsole();

      // ゲーム状態を初期化
      GameState.Initialize(GameRules.InitialTurtlePosition, GameRules.InitialCrocodilePosition);

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
      // 改修内容：カメの動きを行ごとに違うランダム化する場合の実装ポイント
      // 重要：このメソッドは毎フレーム（ゲームループの各イテレーション）で呼び出される
      //      行ごとに異なるランダムな動きを実現するには、以下の点が重要である：
      //      1. InputHandler.ProcessInput()が毎フレーム呼び出され、
      //         その内部でCharacterMovement.MoveTurtleAwayFromCrocodile()が実行される
      //      2. カメはワニが近づいてきたら反対方向に逃げる

      // 画面を描画
      GameRenderer.RenderGameScreen();

      if (!GameState.IsActive)
      {
        return;
      }

      // ユーザー入力を処理
      // カメはワニが近づいてきたら反対方向に逃げる
      InputHandler.ProcessInput();

      // ワニを移動
      CharacterMovement.MoveCrocodileTowardsTurtle();

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
