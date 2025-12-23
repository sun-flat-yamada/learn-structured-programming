using System;
using System.Threading;

namespace LearnStructuredProgramming.Section04_ObjectOrientedProgramming
{
  /// <summary>
  /// オブジェクト指向設計に基づいたカメVSワニゲーム（2D版）
  ///
  /// オブジェクト指向設計のベストプラクティス:
  /// - 依存性の注入 (DI): コンストラクタを通じた依存性の注入
  /// - 依存性逆転の原則 (DIP): インターフェースを通じて具体実装に依存しない
  /// - ファサードパターン: ゲーム全体の複雑さを隠蔽し、シンプルなAPIを提供
  /// - 構成より継承: インターフェースとコンポジションを使用
  /// - 単一責任の原則 (SRP): ゲームループの制御のみを担当
  ///
  /// 特徴:
  /// - 32x32のマス目内を縦横自由に移動可能
  /// - カメはランダムに4方向（上下左右）に移動
  /// - ワニはカメを追いかける
  /// </summary>
  public class TurtleVsCrocodileGame
  {
    private readonly GameConfig _config;
    private readonly GameState _gameState;
    private readonly Turtle _turtle;
    private readonly Crocodile _crocodile;
    private readonly IGameRenderer _renderer;
    private readonly IInputHandler _inputHandler;

    /// <summary>
    /// ゲームインスタンスを作成（依存性注入対応）
    /// </summary>
    public TurtleVsCrocodileGame(
      GameConfig? config = null,
      IGameRenderer? renderer = null,
      IInputHandler? inputHandler = null)
    {
      _config = config ?? new GameConfig();
      _turtle = new Turtle(_config, _config.GetInitialTurtlePosition());
      _crocodile = new Crocodile(_config, _config.GetInitialCrocodilePosition());
      _gameState = new GameState(_config, _turtle, _crocodile);
      _renderer = renderer ?? new ConsoleGameRenderer(_config);
      _inputHandler = inputHandler ?? new ConsoleInputHandler();

      SubscribeToGameEvents();
    }

    /// <summary>
    /// ゲームを実行する
    /// </summary>
    public void Run()
    {
      _renderer.SetupDisplay();
      _gameState.Initialize();

      RunGameLoop();

      _renderer.RenderGameOverScreen(_gameState.Score);
      _renderer.RestoreDisplay();
    }

    /// <summary>
    /// メインゲームループ
    /// </summary>
    private void RunGameLoop()
    {
      while (_gameState.IsActive)
      {
        ExecuteGameTick();
      }
    }

    /// <summary>
    /// 1ティックのゲーム処理を実行
    /// </summary>
    private void ExecuteGameTick()
    {
      // 画面を描画
      _renderer.RenderGameScreen(_gameState);

      if (!_gameState.IsActive)
      {
        return;
      }

      // ユーザー入力を処理
      InputResult inputResult = _inputHandler.ProcessInput(_turtle);
      if (inputResult == InputResult.Quit)
      {
        _gameState.End();
        return;
      }

      // ワニをカメに向かって移動
      _crocodile.MoveTowards(_turtle.Position);

      // 衝突判定
      if (_gameState.IsCollisionDetected())
      {
        _gameState.End();
        return;
      }

      // スコア加算
      _gameState.IncrementScore();

      // ゲーム更新速度制御
      Thread.Sleep(_config.GameUpdateDelayMs);
    }

    /// <summary>
    /// ゲームイベントを購読する
    /// </summary>
    private void SubscribeToGameEvents()
    {
      _gameState.StateChanged += OnGameStateChanged;
      _gameState.GameEnded += OnGameEnded;
      _gameState.GameInitialized += OnGameInitialized;
    }

    private void OnGameStateChanged(object? sender, GameStateChangedEventArgs e)
    {
      // 状態変更時の処理（ログ出力、アニメーション等に拡張可能）
    }

    private void OnGameEnded(object? sender, GameOverEventArgs e)
    {
      // ゲーム終了時の処理（統計記録、ハイスコア更新等に拡張可能）
    }

    private void OnGameInitialized(object? sender, EventArgs e)
    {
      // ゲーム初期化時の処理（ウェルカムメッセージ等に拡張可能）
    }
  }

  /// <summary>
  /// 後方互換性のためのエイリアス
  /// </summary>
  public class FrogVsSnakeGame : TurtleVsCrocodileGame
  {
    public FrogVsSnakeGame(
      GameConfig? config = null,
      IGameRenderer? renderer = null,
      IInputHandler? inputHandler = null)
      : base(config, renderer, inputHandler)
    {
    }
  }
}
