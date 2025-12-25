using System;
using System.Threading;

namespace LearnStructuredProgramming.Section03_StructuredProgrammingPlus
{
  /// <summary>
  /// カメ vs ワニゲーム - 構造化プログラミング拡張版
  ///
  /// ■ 教育目的
  /// Section02の構造化プログラミングを拡張し、2D座標系に対応したバージョンです。
  /// 構造化プログラミングの範囲で機能拡張を行う方法を学べます。
  ///
  /// ■ Section02からの改良点
  /// - 1D→2D: 32x32のマス目内を縦横自由に移動可能
  /// - カメ: 上下左右ランダムに移動（WASD対応）
  /// - ワニ: カメに向かって2D追跡
  ///
  /// ■ ファイル構成
  /// Section02と同じモジュール分割を維持しつつ2D対応を追加。
  ///
  /// ■ 制限事項
  /// オブジェクト指向設計は適用していません。
  /// </summary>
  public class TurtleVsCrocodileGame
  {
    /// <summary>
    /// ゲームを実行
    /// </summary>
    public void Run()
    {
      SetupConsole();

      GameState.Initialize(
        GameRules.InitialTurtlePositionX,
        GameRules.InitialTurtlePositionY,
        GameRules.InitialCrocodilePositionX,
        GameRules.InitialCrocodilePositionY);

      // 構造化されたゲームループ
      while (GameState.IsActive)
      {
        ExecuteGameTick();
      }

      DisplayGameOver();
      RestoreConsole();
    }

    /// <summary>
    /// コンソール初期設定
    /// </summary>
    private static void SetupConsole()
    {
      Console.Clear();
      Console.CursorVisible = false;
    }

    /// <summary>
    /// コンソール設定を復元
    /// </summary>
    private static void RestoreConsole()
    {
      Console.CursorVisible = true;
    }

    /// <summary>
    /// 1フレーム分のゲーム処理を実行
    /// </summary>
    private static void ExecuteGameTick()
    {
      GameRenderer.RenderGameScreen();

      if (!GameState.IsActive)
      {
        return;
      }

      // 入力処理（入力なしならランダム移動）
      InputHandler.ProcessInput();

      // ワニの2D追跡
      CharacterMovement.MoveCrocodileTowardsTurtle();

      // 衝突判定
      if (GameLogic.IsCollisionDetected())
      {
        GameState.End();
        return;
      }

      GameState.IncrementScore();

      // フレーム間隔制御
      Thread.Sleep(GameRules.GameUpdateDelayMs);
    }

    /// <summary>
    /// ゲームオーバー画面表示
    /// </summary>
    private static void DisplayGameOver()
    {
      GameRenderer.RenderGameOverScreen();
    }
  }
}
