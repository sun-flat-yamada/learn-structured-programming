namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;

/// <summary>
/// 時間管理機能のインターフェース
///
/// ■ 責務
/// ゲームループのタイミング制御を抽象化します。
/// テスト時はモックに置き換え可能です。
/// </summary>
public interface IGameClock
{
  void Wait(int milliseconds);
}
