using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム入力処理機能のインターフェース
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - インターフェース分離の原則 (ISP): 入力処理に必要な最小限のメソッドのみ定義
  /// - 依存性逆転の原則 (DIP): 具体的な入力ソースに依存しない
  /// - ストラテジーパターン: 異なる入力ハンドラー実装の交換可能性
  /// </summary>
  public interface IInputHandler
  {
    /// <summary>
    /// ユーザー入力を処理する
    /// </summary>
    /// <param name="turtle">操作対象のカメ</param>
    /// <returns>ゲームを継続する場合はtrue、終了する場合はfalse</returns>
    InputResult ProcessInput(Turtle turtle);
  }

  /// <summary>
  /// 入力処理の結果を表す列挙型
  /// </summary>
  public enum InputResult
  {
    /// <summary>
    /// ゲームを継続
    /// </summary>
    Continue,

    /// <summary>
    /// ゲームを終了
    /// </summary>
    Quit
  }
}
