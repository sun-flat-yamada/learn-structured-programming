using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Core;

/// <summary>
/// ゲーム設定を管理する不変クラス
///
/// ■ 責務
/// ゲームの各種パラメータ（盤面サイズ、初期位置、更新間隔等）
/// をカプセル化し、作成後の変更を禁止します。
///
/// ■ ビルダーパターン
/// 複雑な設定の構築を段階的に行えるよう、
/// ネストされたBuilderクラスを提供します。
/// </summary>
public sealed class GameSettings
{
  public Bounds BoardBounds { get; }
  public Position InitialPlayerPosition { get; }
  public Position InitialEnemyPosition { get; }
  public int UpdateIntervalMs { get; }
  public double EnemyMoveProbability { get; }

  private GameSettings(
    Bounds boardBounds,
    Position initialPlayerPosition,
    Position initialEnemyPosition,
    int updateIntervalMs,
    double enemyMoveProbability)
  {
    BoardBounds = boardBounds;
    InitialPlayerPosition = initialPlayerPosition;
    InitialEnemyPosition = initialEnemyPosition;
    UpdateIntervalMs = updateIntervalMs;
    EnemyMoveProbability = enemyMoveProbability;
  }

  /// <summary>
  /// デフォルト設定を作成
  /// </summary>
  public static GameSettings Default() => new Builder().Build();

  /// <summary>
  /// ビルダーを取得
  /// </summary>
  public static Builder CreateBuilder() => new();

  /// <summary>
  /// GameSettingsのビルダークラス
  ///
  /// 設計原則:
  /// - ビルダーパターン: 複雑なオブジェクト生成をステップバイステップで実行
  /// - 流暢なインターフェース: メソッドチェーンによる可読性向上
  /// </summary>
  public class Builder
  {
    private int _boardWidth = 32;
    private int _boardHeight = 32;
    private Position? _playerPosition;
    private Position? _enemyPosition;
    private int _updateIntervalMs = 200;
    private double _enemyMoveProbability = 1.0;

    public Builder WithBoardSize(int width, int height)
    {
      if (width <= 0)
        throw new ArgumentOutOfRangeException(nameof(width));
      if (height <= 0)
        throw new ArgumentOutOfRangeException(nameof(height));
      _boardWidth = width;
      _boardHeight = height;
      return this;
    }

    public Builder WithPlayerPosition(Position position)
    {
      _playerPosition = position;
      return this;
    }

    public Builder WithEnemyPosition(Position position)
    {
      _enemyPosition = position;
      return this;
    }

    public Builder WithUpdateInterval(int milliseconds)
    {
      if (milliseconds < 0)
        throw new ArgumentOutOfRangeException(nameof(milliseconds));
      _updateIntervalMs = milliseconds;
      return this;
    }

    public Builder WithEnemyMoveProbability(double probability)
    {
      if (probability < 0 || probability > 1)
        throw new ArgumentOutOfRangeException(nameof(probability), "確率は0.0から1.0の範囲である必要があります");
      _enemyMoveProbability = probability;
      return this;
    }

    public GameSettings Build()
    {
      var bounds = new Bounds(_boardWidth, _boardHeight);
      var playerPos = _playerPosition ?? new Position(_boardWidth * 3 / 4, _boardHeight / 2);
      var enemyPos = _enemyPosition ?? new Position(_boardWidth / 4, _boardHeight / 2);

      validatePosition(playerPos, bounds, "プレイヤー");
      validatePosition(enemyPos, bounds, "敵");

      return new GameSettings(bounds, playerPos, enemyPos, _updateIntervalMs, _enemyMoveProbability);
    }

    private static void validatePosition(Position position, Bounds bounds, string entityName)
    {
      if (!bounds.Contains(position))
        throw new ArgumentOutOfRangeException(
          $"{entityName}の位置({position})が境界({bounds})の外にあります");
    }
  }
}
