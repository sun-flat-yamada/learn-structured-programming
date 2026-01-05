using System;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Core;

/// <summary>
/// ゲーム設定を管理する不変クラス
///
/// ■ 責務
/// ゲームの各種パラメータ（盤面サイズ、初期位置、更新間隔等）
/// をカプセル化し、作成後の変更を禁止します。
///
/// ■ 追加設定（Section05）
/// - InitialLizardPosition: トカゲの初期位置
/// - LizardFleeDistance: トカゲが逃げ始める距離
/// - LizardTailDropDistance: 尻尾を切り離す距離
/// </summary>
public sealed class GameSettings
{
  public Bounds BoardBounds { get; }
  public Position InitialPlayerPosition { get; }
  public Position InitialEnemyPosition { get; }
  public Position InitialLizardPosition { get; }
  public int UpdateIntervalMs { get; }
  public double EnemyMoveProbability { get; }
  public int LizardFleeDistance { get; }
  public int LizardTailDropDistance { get; }

  private GameSettings(
    Bounds boardBounds,
    Position initialPlayerPosition,
    Position initialEnemyPosition,
    Position initialLizardPosition,
    int updateIntervalMs,
    double enemyMoveProbability,
    int lizardFleeDistance,
    int lizardTailDropDistance)
  {
    BoardBounds = boardBounds;
    InitialPlayerPosition = initialPlayerPosition;
    InitialEnemyPosition = initialEnemyPosition;
    InitialLizardPosition = initialLizardPosition;
    UpdateIntervalMs = updateIntervalMs;
    EnemyMoveProbability = enemyMoveProbability;
    LizardFleeDistance = lizardFleeDistance;
    LizardTailDropDistance = lizardTailDropDistance;
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
    private Position? _lizardPosition;
    private int _updateIntervalMs = 200;
    private double _enemyMoveProbability = 1.0;
    private int _lizardFleeDistance = 8;
    private int _lizardTailDropDistance = 2;

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

    public Builder WithLizardPosition(Position position)
    {
      _lizardPosition = position;
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

    public Builder WithLizardFleeDistance(int distance)
    {
      if (distance <= 0)
        throw new ArgumentOutOfRangeException(nameof(distance));
      _lizardFleeDistance = distance;
      return this;
    }

    public Builder WithLizardTailDropDistance(int distance)
    {
      if (distance <= 0)
        throw new ArgumentOutOfRangeException(nameof(distance));
      _lizardTailDropDistance = distance;
      return this;
    }

    public GameSettings Build()
    {
      var bounds = new Bounds(_boardWidth, _boardHeight);
      var playerPos = _playerPosition ?? new Position(_boardWidth * 3 / 4, _boardHeight / 2);
      var enemyPos = _enemyPosition ?? new Position(_boardWidth / 4, _boardHeight / 2);
      var lizardPos = _lizardPosition ?? new Position(_boardWidth / 2, _boardHeight * 3 / 4);

      validatePosition(playerPos, bounds, "プレイヤー");
      validatePosition(enemyPos, bounds, "敵");
      validatePosition(lizardPos, bounds, "トカゲ");

      return new GameSettings(
        bounds,
        playerPos,
        enemyPos,
        lizardPos,
        _updateIntervalMs,
        _enemyMoveProbability,
        _lizardFleeDistance,
        _lizardTailDropDistance);
    }

    private static void validatePosition(Position position, Bounds bounds, string entityName)
    {
      if (!bounds.Contains(position))
        throw new ArgumentOutOfRangeException(
          $"{entityName}の位置({position})が境界({bounds})の外にあります");
    }
  }
}
