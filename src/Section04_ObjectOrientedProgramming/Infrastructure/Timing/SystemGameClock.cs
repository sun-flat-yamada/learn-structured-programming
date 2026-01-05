using System.Threading;

using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Infrastructure.Timing;

/// <summary>
/// システムクロックを使用したゲームクロック実装
///
/// ■ 責務
/// Thread.Sleepを使用して実際の待機を行います。
/// テスト時はモックに置き換え可能です。
/// </summary>
public sealed class SystemGameClock : IGameClock
{
  public void Wait(int milliseconds)
  {
    if (milliseconds > 0)
    {
      Thread.Sleep(milliseconds);
    }
  }
}
