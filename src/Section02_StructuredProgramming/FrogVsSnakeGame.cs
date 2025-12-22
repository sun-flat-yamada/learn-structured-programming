using System;
using System.Threading;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// 構造化プログラミングのみを使用したカエルVSヘビゲーム
  ///
  /// 特徴：
  /// - グローバル変数を使用したゲーム状態管理
  /// - 手続き型プログラミング（関数の線形実行）
  /// - 副作用に基づくデータ操作
  /// - クラスやオブジェクト指向設計なし
  /// - goto文を使わず、ループと条件分岐で制御
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
      // 改修内容：カエルの動きを行ごとに違うランダム化する場合の実装ポイント
      // 重要：このメソッドは毎フレーム（ゲームループの各イテレーション）で呼び出される
      //      行ごとに異なるランダムな動きを実現するには、以下の点が重要である：
      //      1. InputHandler.ProcessInput()が毎フレーム呼び出され、
      //         その内部でCharacterMovement.MoveFrogRandomly()が実行される
      //      2. ProcessInput()内でMoveFrogRandomly()が実行されるたびに、
      //         新しいランダム値がRandom.Next()で生成される
      //      3. 各フレーム（行）でMoveFrogRandomly()が実行されるため、
      //         新しい乱数判定が行われ、カエルの動きが行ごとに異なることが保証される
      //      4. このような仕組みにより、各行の描画時に異なるランダムなパターンが実現される

      // 実装例：カエルの動きを行ごとに違うランダム化する場合のコード
      // 以下のような流れで処理が進行する：
      // (1) while (GameState.IsActive) ループの各イテレーションで ExecuteGameTick() が呼ばれる
      // (2) GameRenderer.RenderGameScreen() が画面を描画
      // (3) InputHandler.ProcessInput() が呼ばれ、内部で以下が実行される：
      //     - IsKeyAvailable() でユーザー入力をチェック
      //     - 入力がない場合：CharacterMovement.MoveFrogRandomly() を呼び出す
      //       ここで、毎フレーム新しい Random.Next(100) が実行される
      //       その結果、各行で異なる確率判定が行われ、カエルが異なる方向に移動する
      //     - 入力がある場合：入力に応じてカエルを移動
      // (4) CharacterMovement.MoveSnakeTowardsFrog() でヘビを移動
      // (5) 衝突判定、スコア加算など、その他の処理を実行
      // (6) Thread.Sleep() でゲーム速度を制御
      // (7) 次のイテレーションで、新たに (2) から開始
      //
      // このような構造により、毎フレーム新しい乱数判定が行われるため、
      // カエルの動きが行ごとに異なるランダム化が実現される。

      // 画面を描画
      GameRenderer.RenderGameScreen();

      if (!GameState.IsActive)
      {
        return;
      }

      // ユーザー入力を処理
      // 改修内容：InputHandler.ProcessInput()の役割と、ランダム化との関係
      // 実装：ProcessInput()メソッドは以下の処理を実行する
      // 1. ユーザー入力があるかチェック（IsKeyAvailable()）
      // 2. 入力がない場合：CharacterMovement.MoveFrogRandomly()を呼び出す
      //    ここで、毎フレーム新しいランダム判定が行われ、
      //    カエルが行ごとに異なる確率で、異なる方向に移動する
      // 3. 入力がある場合：入力に応じて移動方向が決定され、
      //    CharacterMovement.MoveFrog()が呼び出される
      // このように、入力がない場合は毎行ランダム移動が行われ、
      // 各行で新しい乱数判定により異なるパターンの動きが実現される
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
