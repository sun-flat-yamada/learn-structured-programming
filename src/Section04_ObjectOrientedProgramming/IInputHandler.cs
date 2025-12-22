using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム入力処理機能のインターフェース
  /// 異なる入力ハンドラー実装の交換可能性を提供する
  /// </summary>
  public interface IInputHandler
  {
    /// <summary>
    /// ユーザー入力を処理し、ゲーム状態を更新する
    /// </summary>
    bool ProcessInput(Frog frog);
  }
}
