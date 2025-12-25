using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities
{
  /// <summary>
  /// トカゲの尻尾
  ///
  /// ■ 責務
  /// トカゲがワニから逃げる際に切り離す囮です。
  /// 切り離された場所から動かず、ワニに被視されますが、
  /// 食べられてもゲームオーバーにはなりません。
  /// </summary>
  public sealed class Tail : Entity
  {
    private bool _isActive = true;

    public override string DisplayName => "尻尾";
    public override string Emoji => "尾 ";  // 2文字幅を維持するため半角スペース追加
    public override ConsoleColor Color => ConsoleColor.DarkYellow;
    public override bool IsActive => _isActive;

    public Tail(Position position, Bounds bounds)
      : base(position, bounds)
    {
    }

    /// <summary>
    /// 尻尾が捕食される
    /// </summary>
    public void OnEaten()
    {
      _isActive = false;
    }
  }
}
