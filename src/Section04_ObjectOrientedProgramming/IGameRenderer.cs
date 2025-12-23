using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム画面レンダリング機能のインターフェース
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - インターフェース分離の原則 (ISP): 必要最小限のメソッドのみを定義
  /// - 依存性逆転の原則 (DIP): 高レベルモジュールは抽象に依存
  /// - ストラテジーパターン: 異なるレンダリング実装の交換可能性
  /// </summary>
  public interface IGameRenderer
  {
    /// <summary>
    /// ゲーム画面を描画する
    /// </summary>
    /// <param name="gameState">現在のゲーム状態</param>
    void RenderGameScreen(GameState gameState);

    /// <summary>
    /// ゲームオーバー画面を描画する
    /// </summary>
    /// <param name="finalScore">最終スコア</param>
    void RenderGameOverScreen(int finalScore);

    /// <summary>
    /// コンソール/画面を初期化する
    /// </summary>
    void SetupDisplay();

    /// <summary>
    /// コンソール/画面の設定を復元する
    /// </summary>
    void RestoreDisplay();
  }
}
