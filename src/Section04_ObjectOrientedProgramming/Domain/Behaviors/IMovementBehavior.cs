using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Behaviors
{
  /// <summary>
  /// 移動行動を定義するインターフェース
  ///
  /// ■ ストラテジーパターン
  /// 移動アルゴリズムをカプセル化し、
  /// 実行時に移動戦略を切り替え可能にします。
  ///
  /// ■ 実装例
  /// - RandomMovementBehavior: ランダムに4方向のいずれかへ移動
  /// - ChaseMovementBehavior: ターゲットを追跡
  /// </summary>
  public interface IMovementBehavior
  {
    /// <summary>
    /// 現在位置とターゲット位置から移動方向を決定
    /// </summary>
    Direction DetermineDirection(Position currentPosition, Position targetPosition);
  }
}
