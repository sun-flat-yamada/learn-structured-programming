using System;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Interfaces;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Entities;
using LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Domain.Events;

namespace LearnStructuredProgramming.Section05_ObjectOrientedProgrammingPlus.Application.Services
{
  /// <summary>
  /// ゲームループを管理するサービス
  ///
  /// ■ 責務
  /// ゲームループの実行制御と各コンポーネントのオーケストレーション。
  /// ファサードパターンでゲーム全体の複雑さを隠蔽し、
  /// Run()のみでゲームを開始できるシンプルなAPIを提供します。
  /// </summary>
  public sealed class GameLoopService
  {
    private readonly GameState _gameState;
    private readonly IGameRenderer _renderer;
    private readonly IInputHandler _inputHandler;
    private readonly IGameClock _clock;

    private bool _hadTailLastTick;
    private GameOverEventArgs? _gameOverArgs;

    /// <summary>
    /// ゲームループ開始イベント
    /// </summary>
    public event EventHandler? LoopStarted;

    /// <summary>
    /// ティック処理完了イベント
    /// </summary>
    public event EventHandler? TickCompleted;

    public GameLoopService(
      GameState gameState,
      IGameRenderer renderer,
      IInputHandler inputHandler,
      IGameClock clock)
    {
      _gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
      _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
      _inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));
      _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    /// <summary>
    /// ゲームループを実行
    /// </summary>
    public void Run()
    {
      Initialize();
      ExecuteLoop();
      Cleanup();
    }

    private void Initialize()
    {
      _renderer.Initialize();
      _gameState.GameEnded += OnGameEnded;
      _gameState.Start();
      _hadTailLastTick = _gameState.Lizard.HasTail;
      LoopStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnGameEnded(object? sender, GameOverEventArgs e)
    {
      _gameOverArgs = e;
    }

    private void ExecuteLoop()
    {
      while (_gameState.IsActive)
      {
        RenderCurrentState();
        ProcessInput();
        UpdateGameWorld();
        CheckGameConditions();
        WaitForNextTick();
      }
    }

    private void RenderCurrentState()
    {
      var context = new RenderContext
      {
        Player = _gameState.Player,
        Enemy = _gameState.Enemy,
        Lizard = _gameState.Lizard,
        Score = _gameState.Score,
        TickCount = _gameState.TickCount,
        BoardWidth = _gameState.Settings.BoardBounds.Width,
        BoardHeight = _gameState.Settings.BoardBounds.Height,
        TailsEaten = _gameState.TailsEaten,
        IsPlayerAlive = _gameState.IsPlayerAlive,
        IsLizardAlive = _gameState.IsLizardAlive
      };

      _renderer.Render(context);
    }

    private void ProcessInput()
    {
      // プレイヤーが捕食されていたら入力を無視
      if (!_gameState.IsPlayerAlive)
      {
        // 終了キーのみ受付
        if (TryCheckQuitInput())
        {
          _gameState.QuitByPlayer();
        }
        return;
      }

      var result = _inputHandler.ProcessInput(_gameState.Player);

      switch (result)
      {
        case InputResult.Quit:
          _gameState.QuitByPlayer();
          break;
        case InputResult.Pause:
          _gameState.Pause();
          break;
        case InputResult.Continue:
          // 入力がなかった場合、デフォルト移動
          _gameState.Player.PerformDefaultMove();
          break;
        case InputResult.Moved:
          // プレイヤーが移動した場合、何もしない
          break;
      }
    }

    private bool TryCheckQuitInput()
    {
      try
      {
        if (System.Console.KeyAvailable)
        {
          var key = System.Console.ReadKey(intercept: true);
          return key.Key == ConsoleKey.Q || key.Key == ConsoleKey.Escape;
        }
      }
      catch (InvalidOperationException)
      {
        // コンソール入力がリダイレクトされている環境
      }
      return false;
    }

    private void UpdateGameWorld()
    {
      if (!_gameState.IsActive) return;

      // トカゲが生存している場合のみ行動させる
      if (_gameState.IsLizardAlive)
      {
        _gameState.Lizard.Act(_gameState.Enemy.Position);

        // 尻尾が落とされたか確認
        if (_hadTailLastTick && !_gameState.Lizard.HasTail)
        {
          _gameState.NotifyTailDropped();
        }
        _hadTailLastTick = _gameState.Lizard.HasTail;
      }

      // ワニの追跡対象を決定
      var target = DetermineEnemyTarget();

      // 敵をターゲットに向かって移動
      _gameState.Enemy.MoveTowards(target);

      // ゲームティックを進める
      _gameState.Tick();

      TickCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ワニの追跡対象を決定
    /// 尻尾があれば尻尾を優先、なければ生存しているターゲットを追跡
    /// </summary>
    private Core.Position DetermineEnemyTarget()
    {
      var tail = _gameState.Lizard.DroppedTail;

      // アクティブな尻尾があればそれを優先
      if (tail != null && tail.IsActive)
      {
        return tail.Position;
      }

      // 両方生存している場合は最も近いターゲットを追跡
      if (_gameState.IsPlayerAlive && _gameState.IsLizardAlive)
      {
        var distanceToPlayer = _gameState.Enemy.DistanceTo(_gameState.Player);
        var distanceToLizard = _gameState.Enemy.DistanceTo(_gameState.Lizard);

        return distanceToPlayer <= distanceToLizard
          ? _gameState.Player.Position
          : _gameState.Lizard.Position;
      }

      // プレイヤーのみ生存
      if (_gameState.IsPlayerAlive)
      {
        return _gameState.Player.Position;
      }

      // トカゲのみ生存
      if (_gameState.IsLizardAlive)
      {
        return _gameState.Lizard.Position;
      }

      // 両方捕食されている場合（通常はここには来ない）
      return _gameState.Enemy.Position;
    }

    private void CheckGameConditions()
    {
      if (!_gameState.IsActive) return;

      _gameState.CheckCollisions();
    }

    private void WaitForNextTick()
    {
      _clock.Wait(_gameState.Settings.UpdateIntervalMs);
    }

    private void Cleanup()
    {
      _gameState.GameEnded -= OnGameEnded;

      if (_gameOverArgs != null)
      {
        _renderer.RenderGameOver(_gameOverArgs);
      }
      _renderer.Cleanup();
    }
  }
}
