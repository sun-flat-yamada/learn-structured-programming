using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム設定を管理する不変クラス
  /// ゲームルールおよび定数をカプセル化し、整合性を保証する
  /// </summary>
  public class GameConfig
  {
    /// <summary>
    /// ゲーム盤の幅
    /// </summary>
    public int GameWidth { get; }

    /// <summary>
    /// ゲーム盤の高さ
    /// </summary>
    public int GameHeight { get; }

    /// <summary>
    /// カエルの初期位置
    /// </summary>
    public int InitialFrogPosition { get; }

    /// <summary>
    /// ヘビの初期位置
    /// </summary>
    public int InitialSnakePosition { get; }

    /// <summary>
    /// ゲーム更新の遅延時間（ミリ秒）
    /// </summary>
    public int GameUpdateDelayMs { get; }

    /// <summary>
    /// カエルが左に移動する確率（パーセンテージ）
    /// </summary>
    public int FrogLeftMoveProbability { get; }

    /// <summary>
    /// カエルが右に移動する確率（パーセンテージ）
    /// </summary>
    public int FrogRightMoveProbability { get; }

    public GameConfig(
      int gameWidth = 40,
      int gameHeight = 10,
      int initialFrogPosition = 20,
      int initialSnakePosition = 5,
      int gameUpdateDelayMs = 200,
      int frogLeftMoveProbability = 30,
      int frogRightMoveProbability = 30)
    {
      if (gameWidth <= 0 || gameHeight <= 0)
        throw new ArgumentException("ゲーム盤のサイズは0より大きい値である必要があります");

      if (gameUpdateDelayMs < 0)
        throw new ArgumentException("ゲーム更新の遅延時間は0以上である必要があります");

      if (frogLeftMoveProbability < 0 || frogLeftMoveProbability > 100)
        throw new ArgumentException("移動確率は0～100の範囲である必要があります");

      if (frogRightMoveProbability < 0 || frogRightMoveProbability > 100)
        throw new ArgumentException("移動確率は0～100の範囲である必要があります");

      if (frogLeftMoveProbability + frogRightMoveProbability > 100)
        throw new ArgumentException("移動確率の合計が100を超えることはできません");

      GameWidth = gameWidth;
      GameHeight = gameHeight;
      InitialFrogPosition = initialFrogPosition;
      InitialSnakePosition = initialSnakePosition;
      GameUpdateDelayMs = gameUpdateDelayMs;
      FrogLeftMoveProbability = frogLeftMoveProbability;
      FrogRightMoveProbability = frogRightMoveProbability;
    }

    /// <summary>
    /// 指定位置がゲーム盤内か判定する
    /// </summary>
    public bool IsWithinBounds(int position)
    {
      return position >= 0 && position < GameWidth - 2;
    }

    /// <summary>
    /// 移動不可の確率を算出する
    /// </summary>
    public int GetNoMoveProbability()
    {
      return 100 - FrogLeftMoveProbability - FrogRightMoveProbability;
    }
  }
}
