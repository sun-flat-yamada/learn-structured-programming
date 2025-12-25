using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Behaviors
{
  /// <summary>
  /// 移動行動を定義するインターフェース
  ///
  /// ■ ストラテジーパターン
  /// 移動アルゴリズムをカプセル化し、
  /// 実行時に移動戦略を切り替え可能にします。
  ///
  /// ■ 実装例（Section05）
  /// - RandomMovementBehavior: ランダム移動
  /// - ChaseMovementBehavior: 追跡移動
  /// - FleeMovementBehavior: 逃走移動（追加）
  /// </summary>
  public interface IMovementBehavior
  {
    Direction DetermineDirection(Position currentPosition, Position targetPosition);
  }
}
