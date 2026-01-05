namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces;

/// <summary>
/// 時間管理の抽象インターフェース
///
/// ■ 責務
/// ゲームループのタイミング制御を抽象化します。
///
/// ■ 設計意図
/// テスト時にモックに差し替えて、待機時間をスキップ可能にします。
/// </summary>
public interface IGameClock
{
  /// <summary>
  /// 指定ミリ秒待機
  /// </summary>
  void Wait(int milliseconds);
}
