# Section04_ObjectOrientedProgramming

## 概要

**オブジェクト指向設計に基づくカエルVSヘビゲーム**

このセクションは、Section03_StructuredProgrammingPlus の構造化プログラミング版を、オブジェクト指向設計のベストプラクティスを適用してリファクタリングしたバージョンです。

## 主な特徴

### 1. カプセル化（Encapsulation）

各クラスが関連するデータと動作を一つにまとめ、内部の実装詳細を隠蔽します。

#### GameConfig クラス
- ゲーム設定を不変オブジェクト（Immutable Object）として管理
- コンストラクタでの値検証により、常に有効な状態を保証
- ゲームルール全体の整合性を保証

```csharp
public class GameConfig
{
  public int GameWidth { get; }
  public int GameHeight { get; }
  public int InitialFrogPosition { get; }
  // ... その他のプロパティ
  
  public bool IsWithinBounds(int position) { ... }
}
```

#### Character クラスヒエラルキー
- 抽象基底クラスで共通の位置管理機能を実装
- Frog と Snake が特有の動作を実装

```csharp
public abstract class Character
{
  public int Position { get; set; }
  public abstract string GetEmoji();
  public abstract ConsoleColor GetColor();
}

public class Frog : Character { ... }
public class Snake : Character { ... }
```

#### GameState クラス
- ゲーム状態の一元管理
- スコアや位置の直接操作を防止し、専用メソッドを通じた操作を強制

```csharp
public class GameState
{
  public int FrogPosition { get; set; }
  public int SnakePosition { get; set; }
  public int Score { get; private set; }
  
  public void IncrementScore() { ... }
  public void End() { ... }
}
```

### 2. 抽象化（Abstraction）

インターフェースを使用して、実装の詳細から切り離された抽象層を提供します。

#### IGameRenderer インターフェース
```csharp
public interface IGameRenderer
{
  void RenderGameScreen(GameState gameState);
  void RenderGameOverScreen(int finalScore);
  void SetupConsole();
  void RestoreConsole();
}
```

複数のレンダラー実装が可能：
- ConsoleGameRenderer（コンソール描画）
- WebGameRenderer（Web画面への出力）など

#### IInputHandler インターフェース
```csharp
public interface IInputHandler
{
  bool ProcessInput(Frog frog);
}
```

複数の入力ハンドラー実装が可能：
- ConsoleInputHandler（コンソール入力）
- NetworkInputHandler（ネットワーク経由の入力）など

### 3. ポリモーフィズム（Polymorphism）

インターフェースと継承を使用して、同じメソッド呼び出しで異なる動作を実現します。

```csharp
public class FrogVsSnakeGame
{
  private readonly IGameRenderer _renderer;
  private readonly IInputHandler _inputHandler;

  public FrogVsSnakeGame(
    IGameRenderer? renderer = null,
    IInputHandler? inputHandler = null)
  {
    _renderer = renderer ?? new ConsoleGameRenderer(...);
    _inputHandler = inputHandler ?? new ConsoleInputHandler();
  }

  private void ExecuteGameTick()
  {
    // レンダラーの実装の詳細を知らずに使用可能
    _renderer.RenderGameScreen(_gameState);
    
    // 入力ハンドラーの実装の詳細を知らずに使用可能
    _inputHandler.ProcessInput(_frog);
  }
}
```

### 4. 単一責任の原則（Single Responsibility Principle）

各クラスが単一の責任を持ち、その責任の理由だけで変更されます。

| クラス | 責任 |
|--------|------|
| GameConfig | ゲーム設定管理と検証 |
| GameState | ゲーム状態管理とイベント通知 |
| Frog | カエルの位置管理と移動ロジック |
| Snake | ヘビの位置管理と移動ロジック |
| ConsoleGameRenderer | コンソール画面描画 |
| ConsoleInputHandler | コンソール入力処理 |
| FrogVsSnakeGame | ゲームループと各コンポーネントの統合 |

### 5. 依存性の注入（Dependency Injection）

コンストラクタを通じて依存性を注入することで、テストしやすく、疎結合な設計を実現します。

```csharp
public FrogVsSnakeGame(
  GameConfig? config = null,
  IGameRenderer? renderer = null,
  IInputHandler? inputHandler = null)
{
  _config = config ?? new GameConfig();
  _renderer = renderer ?? new ConsoleGameRenderer(_config, _frog, _snake);
  _inputHandler = inputHandler ?? new ConsoleInputHandler();
}
```

### 6. イベント駆動設計（Event-Driven Design）

ゲーム状態の変更を購読可能なイベントとして提供します。

```csharp
public class GameState
{
  public event EventHandler<EventArgs>? StateChanged;
  public event EventHandler<GameOverEventArgs>? GameEnded;

  protected virtual void OnStateChanged()
  {
    StateChanged?.Invoke(this, EventArgs.Empty);
  }

  protected virtual void OnGameEnded()
  {
    GameEnded?.Invoke(this, new GameOverEventArgs(_score));
  }
}
```

## ファイル構成

```
Section04_ObjectOrientedProgramming/
├── GameConfig.cs              # ゲーム設定管理（不変オブジェクト）
├── GameState.cs               # ゲーム状態管理とイベント定義
├── Character.cs               # キャラクター基底クラスと実装クラス
│                              # (Frog, Snake)
├── IGameRenderer.cs           # ゲーム描画インターフェース
├── ConsoleGameRenderer.cs     # コンソール画面描画実装
├── IInputHandler.cs           # 入力処理インターフェース
├── ConsoleInputHandler.cs     # コンソール入力処理実装
├── FrogVsSnakeGame.cs         # ゲームエンジンメインクラス
└── README.md                  # このファイル
```

## 設計パターン

### 1. Strategy パターン
- `IGameRenderer` と `IInputHandler` がStrategy パターンの例
- 異なるレンダリング戦略と入力処理戦略を切り替え可能

### 2. Template Method パターン
- `Character` 抽象クラスが基本的な動作をテンプレートとして定義

### 3. Factory パターン
- `FrogVsSnakeGame` コンストラクタでデフォルト実装を生成

### 4. Observer パターン
- `GameState` のイベント機構がObserver パターンの例
- 状態変更を購読可能

## Section02・03 との比較

### 構造化プログラミング（Section02・03）
```csharp
// グローバル変数に直接アクセス
GameState.FrogPosition = newPosition;
GameState.Score++;

// 静的メソッドを通じた操作
CharacterMovement.MoveFrogRandomly();
GameLogic.IsCollisionDetected();
```

### オブジェクト指向プログラミング（Section04）
```csharp
// オブジェクトのメソッドを通じた操作
_frog.MoveRandomly();
_frog.MoveByDirection(-1);
_gameState.IncrementScore();

// インターフェースを通じた依存性の抽象化
_renderer.RenderGameScreen(_gameState);
_inputHandler.ProcessInput(_frog);

// イベント駆動
_gameState.StateChanged += (sender, e) => { ... };
_gameState.GameEnded += (sender, e) => { ... };
```

## 拡張のしやすさ

オブジェクト指向設計により、以下のような拡張が容易になります：

### 1. 新しいレンダラーの追加
```csharp
public class WebGameRenderer : IGameRenderer { ... }

var game = new FrogVsSnakeGame(
  renderer: new WebGameRenderer()
);
```

### 2. 新しい入力ハンドラーの追加
```csharp
public class NetworkInputHandler : IInputHandler { ... }

var game = new FrogVsSnakeGame(
  inputHandler: new NetworkInputHandler()
);
```

### 3. 新しいキャラクターの追加
```csharp
public class Bird : Character { ... }

var bird = new Bird(_config, initialPosition);
```

### 4. ゲーム設定のカスタマイズ
```csharp
var customConfig = new GameConfig(
  gameWidth: 50,
  gameHeight: 15,
  frogLeftMoveProbability: 25,
  frogRightMoveProbability: 25
);

var game = new FrogVsSnakeGame(config: customConfig);
```

## テストの容易さ

依存性の注入により、ユニットテストが簡単になります：

```csharp
// テスト用のダミーレンダラーとハンドラーを注入
var mockRenderer = new MockGameRenderer();
var mockInputHandler = new MockInputHandler();

var game = new FrogVsSnakeGame(
  renderer: mockRenderer,
  inputHandler: mockInputHandler
);

game.Run();

// レンダラーやハンドラーの呼び出しを検証
Assert.AreEqual(expectedCallCount, mockRenderer.RenderCallCount);
```

## オブジェクト指向設計のメリット

### 保守性の向上
- 各クラスの責任が明確
- コードの変更範囲が限定される
- 関連するデータと動作が一箇所にまとまっている

### 再利用性の向上
- `Character` クラスの継承により、新しいキャラクターを簡単に追加可能
- インターフェースの実装により、新しいレンダラーや入力ハンドラーを追加可能

### テスト容易性
- 依存性の注入により、モック/スタブの使用が容易
- インターフェースにより、テスト対象の分離が簡単

### スケーラビリティ
- 新機能を既存コードへの影響を最小限に抑えて追加可能
- クラスの分離により、チーム開発が効率的

## ベストプラクティス

このセクションで実装されたベストプラクティス：

1. **不変オブジェクト（Immutable Objects）**: GameConfig
2. **エラーハンドリング**: コンストラクタでの入力値検証
3. **プロパティカプセル化**: 自動プロパティと get-only プロパティ
4. **イベント駆動**: GameState の StateChanged, GameEnded イベント
5. **インターフェース分離**: IGameRenderer, IInputHandler
6. **スマートデフォルト**: 依存性の注入でのnull-coalescing オペレータ使用
7. **抽象化の活用**: Character 基底クラスとポリモーフィズム
8. **関心の分離**: 各クラスが単一の責任を持つ

## 学習ポイント

このセクションを通じて学べること：

- **カプセル化**：データ隠蔽と制御
- **抽象化**：インターフェースと基底クラスの活用
- **ポリモーフィズム**：同じインターフェースで異なる実装
- **依存性の注入**：テストしやすい疎結合な設計
- **イベント駆動**：非同期的な状態通知
- **SOLID原則**：特に単一責任と依存性逆転
- **デザインパターン**：Strategy, Template Method, Factory, Observer
