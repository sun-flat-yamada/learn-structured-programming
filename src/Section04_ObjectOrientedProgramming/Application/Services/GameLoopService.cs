using System;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Interfaces;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain;
using LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Domain.Events;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming.Application.Services
{
  /// <summary>
  /// ゲームループを管理するサービス
  ///
  /// ■ 責務
  /// ゲームループの実行制御と各コンポーネントのオーケストレーション。
  ///
  /// ■ ファサードパターン
  /// ゲーム全体の複雑さをこのクラスで隠蔽し、
  /// 外部からはRun()のみを呼び出せばよいシンプルなAPIを提供します。
  /// </summary>
  public sealed class GameLoopService
  {
    private readonly GameState _gameState;
    private readonly IGameRenderer _renderer;
    private readonly IInputHandler _inputHandler;
    private readonly IGameClock _clock;

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
      _gameState.Start();
      LoopStarted?.Invoke(this, EventArgs.Empty);
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
        Score = _gameState.Score,
        TickCount = _gameState.TickCount,
        BoardWidth = _gameState.Settings.BoardBounds.Width,
        BoardHeight = _gameState.Settings.BoardBounds.Height
      };

      _renderer.Render(context);
    }

    private void ProcessInput()
    {
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

    private void UpdateGameWorld()
    {
      if (!_gameState.IsActive) return;

      // 敵をプレイヤーに向かって移動
      _gameState.Enemy.MoveTowards(_gameState.Player.Position);

      // ゲームティックを進める
      _gameState.Tick();

      TickCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void CheckGameConditions()
    {
      if (!_gameState.IsActive) return;

      _gameState.CheckCollision();
    }

    private void WaitForNextTick()
    {
      _clock.Wait(_gameState.Settings.UpdateIntervalMs);
    }

    private void Cleanup()
    {
      _renderer.RenderGameOver(_gameState.Score, _gameState.TickCount);
      _renderer.Cleanup();
    }
  }
}
