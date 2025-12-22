using System;
using System.Threading;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// オブジェクト指向設計に基づいたカエルVSヘビゲーム
  ///
  /// 特徴：
  /// - カプセル化：各クラスが関連するデータと動作を保持
  /// - 抽象化：IGameRenderer、IInputHandlerインターフェースで依存性を抽象化
  /// - ポリモーフィズム：異なるレンダラーと入力ハンドラーの交換可能性
  /// - 単一責任の原則：各クラスが単一の責任を持つ
  /// - 依存性の注入：コンストラクタを通じた依存性の注入
  /// - イベント駆動：ゲーム状態変更をイベントで通知
  /// </summary>
  public class FrogVsSnakeGame
  {
    private readonly GameConfig _config;
    private readonly GameState _gameState;
    private readonly Frog _frog;
    private readonly Snake _snake;
    private readonly IGameRenderer _renderer;
    private readonly IInputHandler _inputHandler;

    public FrogVsSnakeGame(
      GameConfig? config = null,
      IGameRenderer? renderer = null,
      IInputHandler? inputHandler = null)
    {
      _config = config ?? new GameConfig();
      _frog = new Frog(_config, _config.InitialFrogPosition);
      _snake = new Snake(_config, _config.InitialSnakePosition);
      _gameState = new GameState(_config);
      _renderer = renderer ?? new ConsoleGameRenderer(_config, _frog, _snake);
      _inputHandler = inputHandler ?? new ConsoleInputHandler();

      SubscribeToGameEvents();
    }

    public void Run()
    {
      _renderer.SetupConsole();
      _gameState.Initialize();

      while (_gameState.IsActive)
      {
        ExecuteGameTick();
      }

      _renderer.RenderGameOverScreen(_gameState.Score);
      _renderer.RestoreConsole();
    }

    private void ExecuteGameTick()
    {
      _renderer.RenderGameScreen(_gameState);

      if (!_gameState.IsActive)
      {
        return;
      }

      bool shouldContinue = _inputHandler.ProcessInput(_frog);
      if (!shouldContinue)
      {
        _gameState.End();
        return;
      }

      _gameState.FrogPosition = _frog.Position;

      _snake.MoveTowardsFrog(_frog.Position);
      _gameState.SnakePosition = _snake.Position;

      if (_gameState.IsCollisionDetected())
      {
        _gameState.End();
        return;
      }

      _gameState.IncrementScore();

      Thread.Sleep(_config.GameUpdateDelayMs);
    }

    private void SubscribeToGameEvents()
    {
      _gameState.StateChanged += (sender, e) => { };
      _gameState.GameEnded += (sender, e) =>
      {
      };
    }
  }
}
