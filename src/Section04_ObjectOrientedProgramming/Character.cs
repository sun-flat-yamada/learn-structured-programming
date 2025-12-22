using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲームキャラクターの基底クラス
  /// 位置管理と移動ロジックの共通部分をカプセル化する
  /// </summary>
  public abstract class Character
  {
    protected int _position;
    protected readonly GameConfig _config;

    public int Position
    {
      get => _position;
      set
      {
        if (_config.IsWithinBounds(value))
        {
          _position = value;
        }
      }
    }

    public string DisplayName { get; protected set; } = "";

    protected Character(GameConfig config, int initialPosition)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
      _position = initialPosition;
    }

    /// <summary>
    /// キャラクターの絵文字表現を取得する
    /// </summary>
    public abstract string GetEmoji();

    /// <summary>
    /// キャラクターの表示色を取得する
    /// </summary>
    public abstract ConsoleColor GetColor();
  }

  /// <summary>
  /// カエルクラス
  /// ランダム移動とユーザー操作による移動の両方に対応
  /// </summary>
  public class Frog : Character
  {
    private readonly Random _random = new Random();

    public Frog(GameConfig config, int initialPosition)
      : base(config, initialPosition)
    {
      DisplayName = "カエル";
    }

    public override string GetEmoji() => "🐸";

    public override ConsoleColor GetColor() => ConsoleColor.Green;

    /// <summary>
    /// ユーザー入力に基づいてカエルを移動させる
    /// </summary>
    public void MoveByDirection(int direction)
    {
      int newPosition = _position + direction;
      Position = newPosition;
    }

    /// <summary>
    /// カエルをランダムに移動させる
    /// 3段階確率分岐：左30%、右30%、移動なし40%
    /// </summary>
    public void MoveRandomly()
    {
      int randomValue = _random.Next(100);
      int newPosition;

      if (randomValue < _config.FrogLeftMoveProbability)
      {
        newPosition = _position - 1;
      }
      else if (randomValue < _config.FrogLeftMoveProbability + _config.FrogRightMoveProbability)
      {
        newPosition = _position + 1;
      }
      else
      {
        return;
      }

      Position = newPosition;
    }
  }

  /// <summary>
  /// ヘビクラス
  /// カエルを追いかける自動移動ロジックを実装
  /// </summary>
  public class Snake : Character
  {
    public Snake(GameConfig config, int initialPosition)
      : base(config, initialPosition)
    {
      DisplayName = "ヘビ";
    }

    public override string GetEmoji() => "🐍";

    public override ConsoleColor GetColor() => ConsoleColor.Red;

    /// <summary>
    /// カエルに向かってヘビを移動させる
    /// </summary>
    public void MoveTowardsFrog(int frogPosition)
    {
      if (_position < frogPosition)
      {
        Position = _position + 1;
      }
      else if (_position > frogPosition)
      {
        Position = _position - 1;
      }
    }
  }
}
