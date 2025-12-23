using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム設定を管理する不変クラス
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - イミュータブル設計: プロパティはget-onlyで変更不可
  /// - バリデーションの集中管理: コンストラクタで整合性を保証
  /// - ビルダーパターンの代替: 名前付き引数でデフォルト値を提供
  /// </summary>
  public sealed class GameConfig
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
    /// カメの初期X座標
    /// </summary>
    public int InitialTurtlePositionX { get; }

    /// <summary>
    /// カメの初期Y座標
    /// </summary>
    public int InitialTurtlePositionY { get; }

    /// <summary>
    /// ワニの初期X座標
    /// </summary>
    public int InitialCrocodilePositionX { get; }

    /// <summary>
    /// ワニの初期Y座標
    /// </summary>
    public int InitialCrocodilePositionY { get; }

    /// <summary>
    /// ゲーム更新の遅延時間（ミリ秒）
    /// </summary>
    public int GameUpdateDelayMs { get; }

    public GameConfig(
      int gameWidth = 32,
      int gameHeight = 32,
      int initialTurtlePositionX = 20,
      int initialTurtlePositionY = 16,
      int initialCrocodilePositionX = 5,
      int initialCrocodilePositionY = 16,
      int gameUpdateDelayMs = 200)
    {
      ValidatePositiveValue(gameWidth, nameof(gameWidth));
      ValidatePositiveValue(gameHeight, nameof(gameHeight));
      ValidateNonNegativeValue(gameUpdateDelayMs, nameof(gameUpdateDelayMs));
      ValidatePositionInBounds(initialTurtlePositionX, gameWidth, "カメのX座標");
      ValidatePositionInBounds(initialTurtlePositionY, gameHeight, "カメのY座標");
      ValidatePositionInBounds(initialCrocodilePositionX, gameWidth, "ワニのX座標");
      ValidatePositionInBounds(initialCrocodilePositionY, gameHeight, "ワニのY座標");

      GameWidth = gameWidth;
      GameHeight = gameHeight;
      InitialTurtlePositionX = initialTurtlePositionX;
      InitialTurtlePositionY = initialTurtlePositionY;
      InitialCrocodilePositionX = initialCrocodilePositionX;
      InitialCrocodilePositionY = initialCrocodilePositionY;
      GameUpdateDelayMs = gameUpdateDelayMs;
    }

    /// <summary>
    /// 指定位置がゲーム盤内か判定する
    /// </summary>
    public bool IsWithinBounds(Position position)
    {
      return IsWithinBoundsX(position.X) && IsWithinBoundsY(position.Y);
    }

    /// <summary>
    /// X座標がゲーム盤内か判定する
    /// </summary>
    public bool IsWithinBoundsX(int x)
    {
      return x >= 0 && x < GameWidth - 2;
    }

    /// <summary>
    /// Y座標がゲーム盤内か判定する
    /// </summary>
    public bool IsWithinBoundsY(int y)
    {
      return y >= 0 && y < GameHeight;
    }

    /// <summary>
    /// カメの初期位置を取得
    /// </summary>
    public Position GetInitialTurtlePosition()
    {
      return new Position(InitialTurtlePositionX, InitialTurtlePositionY);
    }

    /// <summary>
    /// ワニの初期位置を取得
    /// </summary>
    public Position GetInitialCrocodilePosition()
    {
      return new Position(InitialCrocodilePositionX, InitialCrocodilePositionY);
    }

    private static void ValidatePositiveValue(int value, string paramName)
    {
      if (value <= 0)
      {
        throw new ArgumentException($"{paramName}は0より大きい値である必要があります", paramName);
      }
    }

    private static void ValidateNonNegativeValue(int value, string paramName)
    {
      if (value < 0)
      {
        throw new ArgumentException($"{paramName}は0以上である必要があります", paramName);
      }
    }

    private static void ValidatePositionInBounds(int position, int max, string description)
    {
      if (position < 0 || position >= max)
      {
        throw new ArgumentOutOfRangeException(description, $"{description}は0から{max - 1}の範囲である必要があります");
      }
    }
  }
}
