using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;

/// <summary>
/// 入力処理機能のインターフェース
///
/// ■ 責務
/// ユーザー入力の受付とプレイヤー操作への変換を抽象化します。
/// </summary>
public interface IInputHandler
{
  InputResult ProcessInput(Player player);
}

/// <summary>
/// 入力処理の結果
/// </summary>
public enum InputResult
{
  Continue,
  Moved,
  Quit,
  Pause
}
