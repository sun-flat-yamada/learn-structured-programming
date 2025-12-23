using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム状態を管理するクラス
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - オブザーバーパターン: イベントによる状態変更通知
  /// - カプセル化: 状態変更のロジックを内部で管理
  /// - 単一責任の原則: ゲーム状態管理のみを担当
  /// - 依存性の注入: GameConfigを外部から受け取る
  /// </summary>
  public class GameState
  {
    private readonly GameConfig _config;
    private readonly Turtle _turtle;
    private readonly Crocodile _crocodile;
    private int _score;
    private bool _isActive;

    /// <summary>
    /// ゲーム状態が変更された時に発生するイベント
    /// </summary>
    public event EventHandler<GameStateChangedEventArgs>? StateChanged;

    /// <summary>
    /// ゲームが終了した時に発生するイベント
    /// </summary>
    public event EventHandler<GameOverEventArgs>? GameEnded;

    /// <summary>
    /// ゲームが初期化された時に発生するイベント
    /// </summary>
    public event EventHandler<EventArgs>? GameInitialized;

    /// <summary>
    /// カメの現在位置
    /// </summary>
    public Position TurtlePosition => _turtle.Position;

    /// <summary>
    /// ワニの現在位置
    /// </summary>
    public Position CrocodilePosition => _crocodile.Position;

    /// <summary>
    /// 現在のスコア
    /// </summary>
    public int Score
    {
      get => _score;
      private set
      {
        if (_score != value)
        {
          int oldScore = _score;
          _score = value;
          OnStateChanged(GameStateChangeType.ScoreChanged, oldScore, value);
        }
      }
    }

    /// <summary>
    /// ゲームがアクティブかどうか
    /// </summary>
    public bool IsActive
    {
      get => _isActive;
      private set => _isActive = value;
    }

    /// <summary>
    /// ゲーム設定への参照
    /// </summary>
    public GameConfig Config => _config;

    /// <summary>
    /// カメへの参照
    /// </summary>
    public Turtle Turtle => _turtle;

    /// <summary>
    /// ワニへの参照
    /// </summary>
    public Crocodile Crocodile => _crocodile;

    public GameState(GameConfig config, Turtle turtle, Crocodile crocodile)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
      _turtle = turtle ?? throw new ArgumentNullException(nameof(turtle));
      _crocodile = crocodile ?? throw new ArgumentNullException(nameof(crocodile));
      _score = 0;
      _isActive = false;
    }

    /// <summary>
    /// ゲーム状態を初期化する
    /// </summary>
    public void Initialize()
    {
      _score = 0;
      _isActive = true;
      OnGameInitialized();
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    public void IncrementScore()
    {
      Score++;
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void End()
    {
      _isActive = false;
      OnGameEnded();
    }

    /// <summary>
    /// 衝突が発生しているか判定する
    /// </summary>
    public bool IsCollisionDetected()
    {
      return _turtle.CollidesWith(_crocodile);
    }

    protected virtual void OnStateChanged(GameStateChangeType changeType, object? oldValue = null, object? newValue = null)
    {
      StateChanged?.Invoke(this, new GameStateChangedEventArgs(changeType, oldValue, newValue));
    }

    protected virtual void OnGameEnded()
    {
      GameEnded?.Invoke(this, new GameOverEventArgs(_score));
    }

    protected virtual void OnGameInitialized()
    {
      GameInitialized?.Invoke(this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// ゲーム状態変更の種類
  /// </summary>
  public enum GameStateChangeType
  {
    ScoreChanged,
    TurtleMoved,
    CrocodileMoved,
    GameStarted,
    GameEnded
  }

  /// <summary>
  /// ゲーム状態変更イベントの引数
  /// </summary>
  public class GameStateChangedEventArgs : EventArgs
  {
    public GameStateChangeType ChangeType { get; }
    public object? OldValue { get; }
    public object? NewValue { get; }

    public GameStateChangedEventArgs(GameStateChangeType changeType, object? oldValue = null, object? newValue = null)
    {
      ChangeType = changeType;
      OldValue = oldValue;
      NewValue = newValue;
    }
  }

  /// <summary>
  /// ゲームオーバーイベントの引数
  /// </summary>
  public class GameOverEventArgs : EventArgs
  {
    public int FinalScore { get; }

    public GameOverEventArgs(int finalScore)
    {
      FinalScore = finalScore;
    }
  }
}
