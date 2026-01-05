using System;

namespace LearnStructuredProgramming.Section02_StructuredProgramming;

/// <summary>
/// ゲームルールと定数を定義する静的クラス
///
/// ■ 責務
/// ゲームの不変パラメータ（盤面サイズ、初期位置、更新間隔）と
/// ルール判定ロジック（境界チェック、衝突判定）を提供します。
///
/// ■ 設計意図
/// マジックナンバーを排除し、定数に名前を付けることで
/// コードの可読性と変更容易性を向上させています。
/// </summary>
public static class GameRules
{
  // 盤面サイズ
  public const int GAME_WIDTH = 32;
  public const int GAME_HEIGHT = 1;

  // 初期配置
  public const int INITIAL_TURTLE_POSITION = 20;
  public const int INITIAL_CROCODILE_POSITION = 5;

  // ゲーム速度（ミリ秒）
  public const int GAME_UPDATE_DELAY_MS = 200;

  /// <summary>
  /// 位置がゲーム領域内か判定
  /// </summary>
  public static bool IsWithinBounds(int position)
  {
    return position >= 0 && position < GAME_WIDTH - 2;
  }

  /// <summary>
  /// カメとワニが同じ位置か判定
  /// </summary>
  public static bool IsCollisionDetected(int turtlePosition, int crocodilePosition)
  {
    return turtlePosition == crocodilePosition;
  }
}
