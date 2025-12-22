using System;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// ゲーム状態を管理するクラス
  /// ゲームの状態変更を集中管理し、イベント通知機能を提供する
  /// </summary>
  public class GameState
  {
    /// <summary>
    /// ゲーム状態が変更された時に発生するイベント
    /// </summary>
    public event EventHandler<EventArgs>? StateChanged;

    /// <summary>
    /// ゲームが終了した時に発生するイベント
    /// </summary>
    public event EventHandler<GameOverEventArgs>? GameEnded;

    private int _frogPosition;
    private int _snakePosition;
    private int _score;
    private bool _isActive;
    private readonly GameConfig _config;

    public int FrogPosition
    {
      get => _frogPosition;
      set
      {
        if (_frogPosition != value)
        {
          _frogPosition = value;
          OnStateChanged();
        }
      }
    }

    public int SnakePosition
    {
      get => _snakePosition;
      set
      {
        if (_snakePosition != value)
        {
          _snakePosition = value;
          OnStateChanged();
        }
      }
    }

    public int Score
    {
      get => _score;
      private set
      {
        if (_score != value)
        {
          _score = value;
          OnStateChanged();
        }
      }
    }

    public bool IsActive
    {
      get => _isActive;
      private set => _isActive = value;
    }

    public GameState(GameConfig config)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
      Initialize();
    }

    /// <summary>
    /// ゲーム状態を初期化する
    /// </summary>
    public void Initialize()
    {
      _frogPosition = _config.InitialFrogPosition;
      _snakePosition = _config.InitialSnakePosition;
      _score = 0;
      _isActive = true;
      OnStateChanged();
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
      return FrogPosition == SnakePosition;
    }

    protected virtual void OnStateChanged()
    {
      StateChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnGameEnded()
    {
      GameEnded?.Invoke(this, new GameOverEventArgs(_score));
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
