using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces
{
  /// <summary>
  /// 入力処理の抽象インターフェース
  ///
  /// ■ 責務
  /// ユーザー入力の取得と処理を抽象化します。
  ///
  /// ■ 設計意図
  /// コンソール以外の入力ソース（ネットワーク等）にも差し替え可能にします。
  /// </summary>
  public interface IInputHandler
  {
    /// <summary>
    /// 入力を処理し結果を返す
    /// </summary>
    InputResult ProcessInput(Player player);
  }

  /// <summary>
  /// 入力処理の結果
  /// </summary>
  public enum InputResult
  {
    /// <summary>入力なし、ゲーム継続</summary>
    Continue,

    /// <summary>プレイヤーが移動した</summary>
    Moved,

    /// <summary>ゲーム終了要求</summary>
    Quit,

    /// <summary>一時停止要求</summary>
    Pause
  }
}
