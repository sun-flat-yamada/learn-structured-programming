using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Entities;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces
{
  /// <summary>
  /// ゲーム描画の抽象インターフェース
  ///
  /// ■ 責務
  /// ゲーム画面の描画処理を抽象化します。
  ///
  /// ■ 設計意図
  /// コンソール以外の描画先（GUI等）にも差し替え可能にします。
  /// </summary>
  public interface IGameRenderer
  {
    /// <summary>
    /// 描画環境を初期化
    /// </summary>
    void Initialize();

    /// <summary>
    /// ゲーム画面を描画
    /// </summary>
    void Render(RenderContext context);

    /// <summary>
    /// ゲームオーバー画面を描画
    /// </summary>
    void RenderGameOver(int finalScore, int survivalTicks);

    /// <summary>
    /// 描画環境を復元
    /// </summary>
    void Cleanup();
  }

  /// <summary>
  /// 描画に必要な情報をまとめた値オブジェクト
  /// </summary>
  public readonly struct RenderContext
  {
    public Player Player { get; init; }
    public Enemy Enemy { get; init; }
    public int Score { get; init; }
    public int TickCount { get; init; }
    public int BoardWidth { get; init; }
    public int BoardHeight { get; init; }
  }
}
