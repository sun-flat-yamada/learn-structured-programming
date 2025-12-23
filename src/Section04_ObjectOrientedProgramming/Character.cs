using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲームキャラクターの基底クラス
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - Template Methodパターン: 共通の骨格をもつアルゴリズムを基底クラスで定義
  /// - カプセル化: 位置情報と境界チェックを内部で管理
  /// - 抽象化: GetEmoji/GetColor/GetDisplayNameを派生クラスで実装
  /// - リスコフの置換原則: 派生クラスは基底クラスとして扱える
  /// </summary>
  public abstract class Character
  {
    protected Position _position;
    protected readonly GameConfig _config;

    /// <summary>
    /// キャラクターの現在位置
    /// </summary>
    public Position Position
    {
      get => _position;
      protected set
      {
        if (_config.IsWithinBounds(value))
        {
          _position = value;
        }
      }
    }

    /// <summary>
    /// キャラクターの表示名
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// キャラクターの絵文字表現
    /// </summary>
    public abstract string Emoji { get; }

    /// <summary>
    /// キャラクターの表示色
    /// </summary>
    public abstract ConsoleColor Color { get; }

    protected Character(GameConfig config, Position initialPosition)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
      _position = initialPosition;
    }

    /// <summary>
    /// 指定した方向に移動を試みる
    /// </summary>
    public virtual void TryMove(int deltaX, int deltaY)
    {
      Position newPosition = _position.Move(deltaX, deltaY);
      Position = newPosition;
    }

    /// <summary>
    /// 別のキャラクターと衝突しているか判定
    /// </summary>
    public bool CollidesWith(Character other)
    {
      return _position.CollidesWith(other._position);
    }
  }

  /// <summary>
  /// カメクラス
  /// プレイヤーが操作するキャラクター
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 単一責任の原則: カメ固有の移動ロジックのみを担当
  /// - 継承: Character基底クラスの機能を拡張
  /// </summary>
  public class Turtle : Character
  {
    private readonly Random _random = new();

    public override string DisplayName => "カメ";
    public override string Emoji => "🐢";
    public override ConsoleColor Color => ConsoleColor.Green;

    public Turtle(GameConfig config, Position initialPosition)
      : base(config, initialPosition)
    {
    }

    /// <summary>
    /// カメをランダムに4方向のいずれかに移動させる
    /// 確率: 上25%、下25%、左25%、右25%
    /// </summary>
    public void MoveRandomly()
    {
      int randomValue = _random.Next(100);
      Position newPosition = randomValue switch
      {
        < 25 => _position.MoveUp(),
        < 50 => _position.MoveDown(),
        < 75 => _position.MoveLeft(),
        _ => _position.MoveRight()
      };

      Position = newPosition;
    }

    /// <summary>
    /// 上方向に移動
    /// </summary>
    public void MoveUp() => TryMove(0, -1);

    /// <summary>
    /// 下方向に移動
    /// </summary>
    public void MoveDown() => TryMove(0, 1);

    /// <summary>
    /// 左方向に移動
    /// </summary>
    public void MoveLeft() => TryMove(-1, 0);

    /// <summary>
    /// 右方向に移動
    /// </summary>
    public void MoveRight() => TryMove(1, 0);
  }

  /// <summary>
  /// ワニクラス
  /// カメを追いかける敵キャラクター
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 単一責任の原則: ワニ固有の追跡ロジックのみを担当
  /// - 継承: Character基底クラスの機能を拡張
  /// </summary>
  public class Crocodile : Character
  {
    public override string DisplayName => "ワニ";
    public override string Emoji => "🐊";
    public override ConsoleColor Color => ConsoleColor.Red;

    public Crocodile(GameConfig config, Position initialPosition)
      : base(config, initialPosition)
    {
    }

    /// <summary>
    /// カメに向かってワニを移動させる
    /// X方向を優先し、同じX座標ならY方向に移動
    /// </summary>
    public void MoveTowards(Position targetPosition)
    {
      Position newPosition = _position.MoveTowards(targetPosition);
      Position = newPosition;
    }
  }
}
