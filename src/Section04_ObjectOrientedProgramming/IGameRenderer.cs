using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム画面レンダリング機能のインターフェース
  /// 異なるレンダラー実装の交換可能性を提供する
  /// </summary>
  public interface IGameRenderer
  {
    /// <summary>
    /// ゲーム画面を描画する
    /// </summary>
    void RenderGameScreen(GameState gameState);

    /// <summary>
    /// ゲームオーバー画面を描画する
    /// </summary>
    void RenderGameOverScreen(int finalScore);

    /// <summary>
    /// コンソールを初期化する
    /// </summary>
    void SetupConsole();

    /// <summary>
    /// コンソールの設定を復元する
    /// </summary>
    void RestoreConsole();
  }
}
