# Section04_ObjectOrientedProgramming - クイックレファレンス

## ファイル構成

```
src/Section04_ObjectOrientedProgramming/
├── GameConfig.cs              # ゲーム設定（不変オブジェクト）
├── GameState.cs               # ゲーム状態（イベント駆動）
├── Character.cs               # キャラクター（基底・派生クラス）
├── IGameRenderer.cs           # 描画インターフェース
├── ConsoleGameRenderer.cs     # コンソール描画実装
├── IInputHandler.cs           # 入力処理インターフェース
├── ConsoleInputHandler.cs     # コンソール入力実装
├── FrogVsSnakeGame.cs         # ゲームエンジン
└── README.md                  # 詳細ドキュメント
```

## 主要クラス関係図

```
┌─────────────────────────────────────────┐
│       FrogVsSnakeGame                   │
│  (ゲームエンジン・依存性注入)           │
└──────────────┬──────────────────────────┘
               │
        ┌──────┼──────┬──────────┬─────────┐
        ▼      ▼      ▼          ▼         ▼
    GameConfig GameState Character IGameRenderer IInputHandler
         ▲      ▲      ▲ ▲         ▲               ▲
         │      │      │ │         │               │
         │      │  ┌───┘ └─┬──┐    │               │
         │      │  │       │  │    │               │
         │      └──┤   Frog   Snake                │
         │         │               │               │
         └─────────┴─ ┌─────────────┴──────────────┘
                      │
             ConsoleGameRenderer
             ConsoleInputHandler
```

## クイックスタート

### 1. デフォルト設定で実行

```csharp
var game = new FrogVsSnakeGame();
game.Run();
```

### 2. カスタム設定で実行

```csharp
var config = new GameConfig(
  gameWidth: 50,
  gameHeight: 15,
  frogLeftMoveProbability: 25,
  frogRightMoveProbability: 25,
  gameUpdateDelayMs: 150
);

var game = new FrogVsSnakeGame(config: config);
game.Run();
```

### 3. カスタムレンダラーで実行

```csharp
var renderer = new CustomGameRenderer();
var game = new FrogVsSnakeGame(renderer: renderer);
game.Run();
```

### 4. カスタム入力ハンドラーで実行

```csharp
var inputHandler = new CustomInputHandler();
var game = new FrogVsSnakeGame(inputHandler: inputHandler);
game.Run();
```

## キークラス解説

### GameConfig（ゲーム設定）

**特徴**: 不変オブジェクト、値検証済み

```csharp
public class GameConfig
{
  public int GameWidth { get; }           // 40（デフォルト）
  public int GameHeight { get; }          // 10（デフォルト）
  public int InitialFrogPosition { get; } // 20（デフォルト）
  public int InitialSnakePosition { get; } // 5（デフォルト）
  public int GameUpdateDelayMs { get; }   // 200（デフォルト）
  public int FrogLeftMoveProbability { get; }  // 30%（デフォルト）
  public int FrogRightMoveProbability { get; } // 30%（デフォルト）
  
  public bool IsWithinBounds(int position) { ... }
  public int GetNoMoveProbability() { ... }
}
```

### GameState（ゲーム状態）

**特徴**: イベント駆動、状態保護

```csharp
public class GameState
{
  // プロパティ
  public int FrogPosition { get; set; }
  public int SnakePosition { get; set; }
  public int Score { get; } // 読み取り専用
  public bool IsActive { get; }
  
  // メソッド
  public void Initialize() { ... }
  public void IncrementScore() { ... }
  public void End() { ... }
  public bool IsCollisionDetected() { ... }
  
  // イベント
  public event EventHandler<EventArgs>? StateChanged;
  public event EventHandler<GameOverEventArgs>? GameEnded;
}
```

### Character（キャラクター基底クラス）

**特徴**: 抽象クラス、位置管理、ポリモーフィズム

```csharp
public abstract class Character
{
  public int Position { get; set; }
  public string DisplayName { get; protected set; }
  
  public abstract string GetEmoji();
  public abstract ConsoleColor GetColor();
}

// 派生クラス
public class Frog : Character
{
  public void MoveByDirection(int direction) { ... }
  public void MoveRandomly() { ... }
}

public class Snake : Character
{
  public void MoveTowardsFrog(int frogPosition) { ... }
}
```

### インターフェース

```csharp
// 画面描画の抽象化
public interface IGameRenderer
{
  void RenderGameScreen(GameState gameState);
  void RenderGameOverScreen(int finalScore);
  void SetupConsole();
  void RestoreConsole();
}

// 入力処理の抽象化
public interface IInputHandler
{
  bool ProcessInput(Frog frog);
}
```

## デザインパターン一覧

| パターン | 実装場所 | 説明 |
|----------|----------|------|
| **Strategy** | IGameRenderer, IInputHandler | 異なるアルゴリズムの交換可能性 |
| **Template Method** | Character | 基本的な動作の型定義 |
| **Factory** | FrogVsSnakeGame コンストラクタ | デフォルト実装の生成 |
| **Observer** | GameState イベント | 状態変更の通知 |
| **Dependency Injection** | FrogVsSnakeGame コンストラクタ | 依存性の注入 |

## SOLID原則チェックリスト

| 原則 | 実装 | 例 |
|------|------|-----|
| **S** (Single Responsibility) | ✅ | GameConfig = 設定、GameState = 状態、Character = 位置 |
| **O** (Open/Closed) | ✅ | インターフェース（IGameRenderer）で拡張に開放 |
| **L** (Liskov Substitution) | ✅ | Frog/Snake は Character の代わりに使用可能 |
| **I** (Interface Segregation) | ✅ | 細粒度インターフェース（IGameRenderer, IInputHandler） |
| **D** (Dependency Inversion) | ✅ | インターフェース経由での依存 |

## 拡張ポイント

### 新しいレンダラーの追加

```csharp
public class WebGameRenderer : IGameRenderer
{
  public void RenderGameScreen(GameState gameState) 
  { 
    // Web画面への出力 
  }
  
  public void RenderGameOverScreen(int finalScore) 
  { 
    // Webゲームオーバー画面 
  }
  
  public void SetupConsole() { }
  public void RestoreConsole() { }
}
```

### 新しい入力ハンドラーの追加

```csharp
public class GamepadInputHandler : IInputHandler
{
  public bool ProcessInput(Frog frog)
  {
    // ゲームパッド入力の処理
    return true;
  }
}
```

### 新しいキャラクターの追加

```csharp
public class Bird : Character
{
  public Bird(GameConfig config, int initialPosition)
    : base(config, initialPosition)
  {
    DisplayName = "鳥";
  }

  public override string GetEmoji() => "🦅";
  public override ConsoleColor GetColor() => ConsoleColor.Yellow;
  
  public void FlyRandomly() { ... }
  public void FlyTowardsFrog(int frogPosition) { ... }
}
```

## ユニットテスト例

```csharp
[TestClass]
public class FrogTests
{
  [TestMethod]
  public void MoveByDirection_Positive_ShouldMoveRight()
  {
    var config = new GameConfig();
    var frog = new Frog(config, 10);
    
    frog.MoveByDirection(1);
    
    Assert.AreEqual(11, frog.Position);
  }
  
  [TestMethod]
  public void MoveByDirection_Negative_ShouldMoveLeft()
  {
    var config = new GameConfig();
    var frog = new Frog(config, 10);
    
    frog.MoveByDirection(-1);
    
    Assert.AreEqual(9, frog.Position);
  }
}
```

## デバッグのコツ

### 1. GameState のイベント購読

```csharp
var gameState = new GameState(config);
gameState.StateChanged += (sender, e) => 
{
  Console.WriteLine($"State changed at {DateTime.Now}");
};
```

### 2. Character の位置追跡

```csharp
var frog = new Frog(config, 20);
Console.WriteLine($"Frog position: {frog.Position}");
frog.MoveRandomly();
Console.WriteLine($"Frog position after random move: {frog.Position}");
```

### 3. GameConfig の検証

```csharp
try
{
  var badConfig = new GameConfig(
    frogLeftMoveProbability: 150 // エラー！
  );
}
catch (ArgumentException ex)
{
  Console.WriteLine($"設定エラー: {ex.Message}");
}
```

## パフォーマンス最適化

### 1. Random のプーリング

Frog クラスで Random を再利用（既実装）

### 2. 不要なイベント購読の削除

```csharp
gameState.StateChanged -= OnStateChanged;
```

### 3. ゲーム更新の遅延調整

```csharp
var fastConfig = new GameConfig(gameUpdateDelayMs: 50);
var slowConfig = new GameConfig(gameUpdateDelayMs: 500);
```

## トラブルシューティング

| 問題 | 原因 | 解決方法 |
|------|------|---------|
| `ArgumentException` in GameConfig | 無効な設定値 | コンストラクタのパラメータを確認 |
| Character が盤外に出る | IsWithinBounds チェックなし | Position プロパティの内部検証を確認 |
| イベントが発火しない | 購読していない | StateChanged/GameEnded に購読を追加 |
| 描画がちらつく | Console.Clear() が多い | RenderGameScreen の効率を改善 |

## 実行方法

### メニューから選択

```
選択 (0-4): 4
```

### プログラムから直接実行

```csharp
static void Main()
{
  var game = new FrogVsSnakeGame();
  game.Run();
}
```

---

詳細は [README.md](README.md) を参照してください。
