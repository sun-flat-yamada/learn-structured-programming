using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces
{
  /// <summary>
  /// ゲームレンダリング機能のインターフェース
  ///
  /// ■ 責務
  /// ゲーム画面の初期化・描画・後処理を抽象化します。
  /// コンソール以外の出力先（GUI等）への切り替えも可能です。
  /// </summary>
  public interface IGameRenderer
  {
    void Initialize();
    void Render(RenderContext context);
    void RenderGameOver(GameOverEventArgs gameOverArgs);
    void Cleanup();
  }

  /// <summary>
  /// レンダリングコンテキスト
  ///
  /// ■ 責務
  /// 描画に必要な情報をまとめた値オブジェクトです。
  /// </summary>
  public readonly struct RenderContext
  {
    public Player Player { get; init; }
    public Enemy Enemy { get; init; }
    public Lizard Lizard { get; init; }
    public int Score { get; init; }
    public int TickCount { get; init; }
    public int BoardWidth { get; init; }
    public int BoardHeight { get; init; }
    public int TailsEaten { get; init; }
    public bool IsPlayerAlive { get; init; }
    public bool IsLizardAlive { get; init; }
  }
}
