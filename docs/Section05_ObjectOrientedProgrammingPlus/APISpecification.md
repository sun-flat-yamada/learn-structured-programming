# Section05_ObjectOrientedProgrammingPlus - API仕様書

## 目次

1. [Core Layer API](#core-layer-api)
2. [Domain Layer API](#domain-layer-api)
3. [Application Layer API](#application-layer-api)
4. [Infrastructure Layer API](#infrastructure-layer-api)
5. [Presentation Layer API](#presentation-layer-api)

---

## Core Layer API

### Position (struct)

2D位置を表す不変な値オブジェクト

#### Position プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| X | int | X座標 |
| Y | int | Y座標 |

#### Position コンストラクタ

```csharp
Position(int x, int y)
```

#### Position メソッド

| メソッド | 戻り値 | 説明 |
| ------- | ----- | ---- |
| Move(int deltaX, int deltaY) | Position | 指定した移動量だけ移動した新しい位置 |
| MoveUp() | Position | 上方向に移動 |
| MoveDown() | Position | 下方向に移動 |
| MoveLeft() | Position | 左方向に移動 |
| MoveRight() | Position | 右方向に移動 |
| MoveTowards(Position target) | Position | ターゲットに向かって1歩移動 |
| MoveAwayFrom(Position target) | Position | ターゲットから逃げる方向に1歩移動 |
| DistanceTo(Position other) | int | マンハッタン距離を計算 |
| Equals(Position other) | bool | 等価性判定 |
| GetHashCode() | int | ハッシュコード取得 |

#### Position 使用例

```csharp
var pos = new Position(10, 20);
var newPos = pos.Move(1, -1);  // (11, 19)
var distance = pos.DistanceTo(new Position(15, 25));  // 10
```

---

### Direction (enum)

移動方向を表す列挙型

#### 値

| 値 | 説明 |
| ---- | ------ |
| None | 移動なし |
| Up | 上 |
| Down | 下 |
| Left | 左 |
| Right | 右 |

#### 拡張メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| ToMovement() | (int DeltaX, int DeltaY) | 移動量に変換 |
| ApplyTo(Position position) | Position | 位置に方向を適用 |
| Opposite() | Direction | 反対方向を取得 |

---

### Bounds (struct)

ゲーム盤の境界を表す不変な値オブジェクト

#### Bounds プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| Width | int | 幅 |
| Height | int | 高さ |
| Center | Position | 中心位置 |

#### Bounds コンストラクタ

```csharp
Bounds(int width, int height)
```

- 例外: width/heightが0以下の場合、ArgumentOutOfRangeException

#### Bounds メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| Contains(Position position) | bool | 位置が境界内にあるか判定 |
| Clamp(Position position) | Position | 位置を境界内に制約 |

---

### GameSettings (class)

ゲーム設定を管理する不変クラス

#### GameSettings プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| BoardBounds | Bounds | 盤面サイズ |
| InitialPlayerPosition | Position | プレイヤー初期位置 |
| InitialEnemyPosition | Position | 敵初期位置 |
| InitialLizardPosition | Position | トカゲ初期位置 |
| UpdateIntervalMs | int | 更新間隔（ミリ秒） |
| EnemyMoveProbability | double | 敵の移動確率 |
| LizardFleeDistance | int | トカゲが逃げ始める距離 |
| LizardTailDropDistance | int | 尻尾を切り離す距離 |

#### GameSettings 静的メソッド

```csharp
static GameSettings Default()
```

デフォルト設定を作成

```csharp
static Builder CreateBuilder()
```

ビルダーを取得

#### GameSettings Builder クラス

```csharp
Builder WithBoardSize(int width, int height)
Builder WithPlayerPosition(Position position)
Builder WithEnemyPosition(Position position)
Builder WithLizardPosition(Position position)
Builder WithUpdateInterval(int milliseconds)
Builder WithEnemyMoveProbability(double probability)
Builder WithLizardFleeDistance(int distance)
Builder WithLizardTailDropDistance(int distance)
GameSettings Build()
```

#### GameSettings 使用例

```csharp
var settings = GameSettings.CreateBuilder()
    .WithBoardSize(40, 40)
    .WithUpdateInterval(100)
    .WithLizardFleeDistance(10)
    .Build();
```

---

## Domain Layer API

### Entity (abstract class)

ゲームエンティティの基底クラス

#### Entity プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| Position | Position | 現在位置 |
| DisplayName | string | 表示名（abstract） |
| Emoji | string | 絵文字表現（abstract） |
| Color | ConsoleColor | 表示色（abstract） |
| IsActive | bool | アクティブ状態（virtual） |

#### Entity コンストラクタ

```csharp
protected Entity(Position initialPosition, Bounds bounds)
```

#### Entity メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| TryMove(Direction direction) | bool | 指定方向に移動を試みる |
| CollidesWith(Entity other) | bool | 別のエンティティと衝突しているか |
| DistanceTo(Entity other) | int | 別のエンティティとの距離 |

---

### Player (class)

プレイヤーキャラクター（カメ）

#### Player プロパティ

| 名前 | 型 | 値 |
| ----- | --- | --- |
| DisplayName | string | "カメ" |
| Emoji | string | "🐢" |
| Color | ConsoleColor | Green |

#### Player コンストラクタ

```csharp
Player(Position initialPosition, Bounds bounds, IMovementBehavior? defaultMovement = null)
```

#### Player メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| MoveUp() | bool | 上に移動 |
| MoveDown() | bool | 下に移動 |
| MoveLeft() | bool | 左に移動 |
| MoveRight() | bool | 右に移動 |
| PerformDefaultMove() | void | デフォルト移動（ランダム） |

---

### Enemy (class)

敵キャラクター（ワニ）

#### Enemy プロパティ

| 名前 | 型 | 値 |
| ----- | --- | --- |
| DisplayName | string | "ワニ" |
| Emoji | string | "🐊" |
| Color | ConsoleColor | Red |

#### Enemy コンストラクタ

```csharp
Enemy(Position initialPosition, Bounds bounds, IMovementBehavior? movementBehavior = null)
```

#### Enemy メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| MoveTowards(Position target) | void | ターゲットに向かって移動 |

---

### Lizard (class)

トカゲキャラクター

#### Lizard プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| DisplayName | string | "トカゲ" |
| Emoji | string | "🦎" |
| Color | ConsoleColor | Yellow |
| HasTail | bool | 尻尾を持っているか |
| State | LizardState | 現在の状態 |
| DroppedTail | Tail? | 切り離した尻尾 |
| IsSpeedBoosted | bool | 倍速モードか |

#### Lizard コンストラクタ

```csharp
Lizard(
    Position initialPosition,
    Bounds bounds,
    int fleeDistance = 8,
    int tailDropDistance = 4,
    IMovementBehavior? randomMovement = null,
    IMovementBehavior? fleeMovement = null)
```

#### Lizard メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| Act(Position enemyPosition) | void | 敵の位置を考慮して行動 |
| NotifyTailEaten() | void | 尻尾が捕食されたことを通知 |

---

### Tail (class)

トカゲの尻尾

#### Tail プロパティ

| 名前 | 型 | 値 |
| --------- | --- | --- |
| DisplayName | string | "尻尾" |
| Emoji | string | "尾 " |
| Color | ConsoleColor | DarkYellow |
| IsActive | bool | アクティブ状態 |

#### Tail コンストラクタ

```csharp
Tail(Position position, Bounds bounds)
```

#### Tail メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| OnEaten() | void | 尻尾が捕食される |

---

### IMovementBehavior (interface)

移動行動を定義するインターフェース

#### IMovementBehavior メソッド

```csharp
Direction DetermineDirection(Position currentPosition, Position targetPosition)
```

#### IMovementBehavior 実装クラス

- ChaseMovementBehavior: 追跡移動
- FleeMovementBehavior: 逃走移動
- RandomMovementBehavior: ランダム移動

---

### GameState (class)

ゲーム状態を管理するドメインモデル

#### GameState イベント

| イベント | 型 | 説明 |
| --------- | --- | ------ |
| StateChanged | EventHandler\<GameStateChangedEventArgs\> | 状態変更 |
| GameEnded | EventHandler\<GameOverEventArgs\> | ゲーム終了 |
| CollisionDetected | EventHandler\<CollisionEventArgs\> | 衝突検出 |
| TailEvent | EventHandler\<TailEventArgs\> | 尻尾イベント |

#### GameState プロパティ

| 名前 | 型 | 説明 |
| ----- | --- | ------ |
| Settings | GameSettings | ゲーム設定 |
| Player | Player | プレイヤー |
| Enemy | Enemy | 敵 |
| Lizard | Lizard | トカゲ |
| Score | int | 現在のスコア |
| TickCount | int | 経過ティック数 |
| Phase | GamePhase | ゲームフェーズ |
| IsActive | bool | ゲームがアクティブか |
| TailsEaten | int | 食べられた尻尾の数 |
| IsPlayerAlive | bool | プレイヤーが生存しているか |
| IsLizardAlive | bool | トカゲが生存しているか |

#### GameState コンストラクタ

```csharp
GameState(GameSettings settings, Player player, Enemy enemy, Lizard lizard)
```

#### GameState メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| Start() | void | ゲームを開始 |
| Tick() | void | ゲームティックを進める |
| CheckCollisions() | bool | 衝突判定を実行 |
| NotifyTailDropped() | void | 尻尾が落とされたことを通知 |
| QuitByPlayer() | void | プレイヤーによるゲーム終了 |
| Pause() | void | ゲームを一時停止 |
| Resume() | void | ゲームを再開 |

---

## Application Layer API

### IGameRenderer (interface)

ゲームレンダリング機能のインターフェース

#### IGameRenderer メソッド

```csharp
void Initialize()
void Render(RenderContext context)
void RenderGameOver(GameOverEventArgs gameOverArgs)
void Cleanup()
```

---

### IInputHandler (interface)

入力処理機能のインターフェース

#### IInputHandler メソッド

```csharp
InputResult ProcessInput(Player player)
```

#### InputResult (enum)

| 値 | 説明 |
| ----- | ------ |
| Continue | 入力なし |
| Moved | 移動した |
| Quit | 終了 |
| Pause | 一時停止 |

---

### IGameClock (interface)

時間管理機能のインターフェース

#### IGameClock メソッド

```csharp
void Wait(int milliseconds)
```

---

### GameLoopService (class)

ゲームループを管理するサービス

#### GameLoopService イベント

| イベント | 型 | 説明 |
| --------- | --- | ------ |
| LoopStarted | EventHandler | ループ開始 |
| TickCompleted | EventHandler | ティック完了 |

#### GameLoopService コンストラクタ

```csharp
GameLoopService(
    GameState gameState,
    IGameRenderer renderer,
    IInputHandler inputHandler,
    IGameClock clock)
```

#### GameLoopService メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| Run() | void | ゲームループを実行 |

---

### GameFactory (class)

ゲームインスタンスを構築するファクトリ

#### GameFactory コンストラクタ

```csharp
GameFactory(IGameRenderer renderer, IInputHandler inputHandler, IGameClock clock)
```

#### GameFactory メソッド

| メソッド | 戻り値 | 説明 |
| --------- | ------- | ------ |
| Create() | GameLoopService | デフォルト設定でゲームを作成 |
| Create(GameSettings settings) | GameLoopService | 指定設定でゲームを作成 |

---

## Infrastructure Layer API

### ConsoleInputHandler (class)

コンソール入力ハンドラー

#### ConsoleInputHandler 実装インターフェース

IInputHandler

#### ConsoleInputHandler サポートキー

| キー | 動作 |
| ----- | ------ |
| W / ↑ | 上に移動 |
| S / ↓ | 下に移動 |
| A / ← | 左に移動 |
| D / → | 右に移動 |
| P | 一時停止 |
| Q / Esc | 終了 |

---

### SystemGameClock (class)

システムクロックを使用したゲームクロック実装

#### SystemGameClock 実装インターフェース

IGameClock

#### SystemGameClock メソッド

```csharp
void Wait(int milliseconds)
```

Thread.Sleepを使用して待機

---

## Presentation Layer API

### ConsoleGameRenderer (class)

コンソール画面へのゲームレンダリング実装

#### ConsoleGameRenderer 実装インターフェース

IGameRenderer

#### ConsoleGameRenderer メソッド

| メソッド | 説明 |
| --------- | ------ |
| Initialize() | コンソール初期化（UTF-8、カーソル非表示） |
| Render(RenderContext context) | ゲーム画面描画 |
| RenderGameOver(GameOverEventArgs args) | ゲームオーバー画面描画 |
| Cleanup() | コンソールクリーンアップ |

---

## Entry Point API

### LifeGame (class)

ゲームのエントリーポイント（Facade）

#### LifeGame コンストラクタ

```csharp
// デフォルト設定
LifeGame()

// カスタム設定
LifeGame(GameSettings settings)

// 完全な依存性注入（テスト用）
LifeGame(
    GameSettings settings,
    IGameRenderer renderer,
    IInputHandler inputHandler,
    IGameClock clock)
```

#### LifeGame メソッド

```csharp
void Run()
```

ゲームを実行

#### LifeGame 使用例

```csharp
// 最もシンプルな使用方法
var game = new LifeGame();
game.Run();

// カスタム設定
var settings = GameSettings.CreateBuilder()
    .WithBoardSize(40, 40)
    .WithUpdateInterval(100)
    .Build();
var game = new LifeGame(settings);
game.Run();

// テスト用（モック注入）
var mockRenderer = new MockRenderer();
var mockInput = new MockInputHandler();
var mockClock = new MockGameClock();
var game = new LifeGame(settings, mockRenderer, mockInput, mockClock);
game.Run();
```

---

## イベント引数API

### GameStateChangedEventArgs

```csharp
class GameStateChangedEventArgs : EventArgs
{
    GameStateChangeType ChangeType { get; }
    object? Data { get; }
}
```

### GameOverEventArgs

```csharp
class GameOverEventArgs : EventArgs
{
    int FinalScore { get; }
    int SurvivalTicks { get; }
    GameOverReason Reason { get; }
    Entity? CaughtEntity { get; }
}
```

### CollisionEventArgs

```csharp
class CollisionEventArgs : EventArgs
{
    Entity Entity1 { get; }
    Entity Entity2 { get; }
    Position CollisionPosition { get; }
    bool IsGameOver { get; }
}
```

### TailEventArgs

```csharp
class TailEventArgs : EventArgs
{
    Tail Tail { get; }
    Lizard Lizard { get; }
    TailEventType EventType { get; }
}
```

---

## 列挙型一覧

### GamePhase

- NotStarted
- Running
- Paused
- Ended

### LizardState

- Wandering（うろうろ歩く）
- Fleeing（逃走中）
- TailDropped（尻尾を切り離して倍速逃走）

### GameStateChangeType

- Initialized
- ScoreChanged
- PlayerMoved
- EnemyMoved
- LizardMoved
- Collision
- PlayerCaught
- LizardCaught
- TailDropped
- TailEaten
- GameOver
- Paused
- Resumed

### GameOverReason

- PlayerCaught
- LizardCaught
- AllCaught
- PlayerQuit
- TimeUp

### TailEventType

- Dropped
- Eaten

### InputResult

- Continue
- Moved
- Quit
- Pause
