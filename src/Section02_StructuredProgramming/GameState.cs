using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming
{
  /// <summary>
  /// ゲーム状態を保持する静的クラス
  ///
  /// ■ 責務
  /// ゲーム全体で共有される状態（位置、スコア、実行中フラグ）を
  /// グローバル変数として管理します。
  ///
  /// ■ 構造化プログラミングにおける位置づけ
  /// OOPではインスタンス変数として隠蔽しますが、
  /// 構造化プログラミングでは静的変数で状態を共有します。
  ///
  /// ■ 注意
  /// グローバル状態は副作用の原因となるため、
  /// 大規模開発では避けるべきパターンです。
  /// </summary>
  public static class GameState
  {
    // キャラクター位置（X座標）
    public static int TurtlePosition;
    public static int CrocodilePosition;

    // ゲーム進行状態
    public static int Score;
    public static bool IsActive;

    /// <summary>
    /// ゲーム状態を初期値に設定
    /// </summary>
    public static void Initialize(int initialTurtlePosition, int initialCrocodilePosition)
    {
      TurtlePosition = initialTurtlePosition;
      CrocodilePosition = initialCrocodilePosition;
      Score = 0;
      IsActive = true;
    }

    /// <summary>
    /// ゲーム状態をリセット（再プレイ用）
    /// </summary>
    public static void Reset(int initialTurtlePosition, int initialCrocodilePosition)
    {
      TurtlePosition = initialTurtlePosition;
      CrocodilePosition = initialCrocodilePosition;
      Score = 0;
      IsActive = true;
    }

    /// <summary>
    /// ゲームを終了状態に
    /// </summary>
    public static void End()
    {
      IsActive = false;
    }

    /// <summary>
    /// スコアを加算
    /// </summary>
    public static void IncrementScore()
    {
      Score++;
    }
  }
}
