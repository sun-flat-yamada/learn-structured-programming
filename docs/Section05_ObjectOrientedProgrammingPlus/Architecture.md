# Section05_ObjectOrientedProgrammingPlus - アーキテクチャ詳細

## Clean Architecture 5層構造

### レイヤー依存関係図

```mermaid
graph TB
    Presentation[Presentation Layer]
    Application[Application Layer]
    Domain[Domain Layer]
    Infrastructure[Infrastructure Layer]
    Core[Core Layer]

    Presentation --> Application
    Presentation --> Domain
    Presentation --> Core

    Application --> Domain
    Application --> Core

    Infrastructure --> Application
    Infrastructure --> Domain
    Infrastructure --> Core

    Domain --> Core

    style Core fill:#e1f5ff
    style Domain fill:#fff4e1
    style Application fill:#f0ffe1
    style Infrastructure fill:#ffe1f5
    style Presentation fill:#e1ffe8
```

### 依存関係ルール

1. **内側のレイヤーは外側のレイヤーを知らない**
2. **依存の方向は常に内側（Core）に向かう**
3. **インターフェースによる依存性の逆転**

## レイヤー詳細

### 1. Core Layer（最内層）

**責務**: ビジネスルールの基礎となる値オブジェクトと基本型

**特徴**:

- 他のレイヤーに依存しない
- イミュータブル（不変）
- ビジネスロジックを含まない

**コンポーネント**:

```mermaid
classDiagram
    class Position {
        <<struct>>
        +int X
        +int Y
        +Position Move(int deltaX, int deltaY)
        +Position MoveTowards(Position target)
        +Position MoveAwayFrom(Position target)
        +int DistanceTo(Position other)
    }

    class Direction {
        <<enumeration>>
        None
        Up
        Down
        Left
        Right
    }

    class Bounds {
        <<struct>>
        +int Width
        +int Height
        +bool Contains(Position position)
        +Position Clamp(Position position)
        +Position Center
    }

    class GameSettings {
        +Bounds BoardBounds
        +Position InitialPlayerPosition
        +Position InitialEnemyPosition
        +Position InitialLizardPosition
        +int UpdateIntervalMs
        +double EnemyMoveProbability
        +int LizardFleeDistance
        +int LizardTailDropDistance
    }

    GameSettings --> Position
    GameSettings --> Bounds
```

### 2. Domain Layer（ビジネスロジック層）

**責務**: ゲームのビジネスルールとドメインモデル

**特徴**:

- Coreレイヤーのみに依存
- 外部システムを知らない
- ビジネスロジックの中核

**サブレイヤー**:

#### Entities（エンティティ）

```mermaid
classDiagram
    class Entity {
        <<abstract>>
        +Position Position
        +abstract string DisplayName
        +abstract string Emoji
        +abstract ConsoleColor Color
        +virtual bool IsActive
        +bool TryMove(Direction direction)
        +bool CollidesWith(Entity other)
        +int DistanceTo(Entity other)
    }

    class Player {
        +bool MoveUp()
        +bool MoveDown()
        +bool MoveLeft()
        +bool MoveRight()
        +void PerformDefaultMove()
    }

    class Enemy {
        +void MoveTowards(Position target)
    }

    class Lizard {
        +bool HasTail
        +LizardState State
        +Tail? DroppedTail
        +bool IsSpeedBoosted
        +void Act(Position enemyPosition)
        +void NotifyTailEaten()
    }

    class Tail {
        +bool IsActive
        +void OnEaten()
    }

    Entity <|-- Player
    Entity <|-- Enemy
    Entity <|-- Lizard
    Entity <|-- Tail
```

#### Behaviors（振る舞い）

```mermaid
classDiagram
    class IMovementBehavior {
        <<interface>>
        +Direction DetermineDirection(Position current, Position target)
    }

    class ChaseMovementBehavior {
        +Direction DetermineDirection(Position current, Position target)
    }

    class FleeMovementBehavior {
        +Direction DetermineDirection(Position current, Position target)
    }

    class RandomMovementBehavior {
        +Direction DetermineDirection(Position current, Position target)
    }

    IMovementBehavior <|.. ChaseMovementBehavior
    IMovementBehavior <|.. FleeMovementBehavior
    IMovementBehavior <|.. RandomMovementBehavior
```

#### GameState（ゲーム状態）

```mermaid
classDiagram
    class GameState {
        +GameSettings Settings
        +Player Player
        +Enemy Enemy
        +Lizard Lizard
        +int Score
        +int TickCount
        +GamePhase Phase
        +bool IsActive
        +int TailsEaten
        +bool IsPlayerAlive
        +bool IsLizardAlive
        +void Start()
        +void Tick()
        +bool CheckCollisions()
        +void NotifyTailDropped()
        +void QuitByPlayer()
    }

    class GamePhase {
        <<enumeration>>
        NotStarted
        Running
        Paused
        Ended
    }

    GameState --> GamePhase
```

### 3. Application Layer（アプリケーション層）

**責務**: ユースケースの実装とオーケストレーション

**特徴**:

- Domain/Coreレイヤーに依存
- インターフェースで外部システムを抽象化
- ビジネスロジックの調整役

**コンポーネント**:

#### Services

```mermaid
classDiagram
    class GameLoopService {
        -GameState _gameState
        -IGameRenderer _renderer
        -IInputHandler _inputHandler
        -IGameClock _clock
        +void Run()
    }

    class GameFactory {
        -IGameRenderer _renderer
        -IInputHandler _inputHandler
        -IGameClock _clock
        +GameLoopService Create()
        +GameLoopService Create(GameSettings settings)
    }

    GameFactory --> GameLoopService
```

#### Interfaces

```mermaid
classDiagram
    class IGameRenderer {
        <<interface>>
        +void Initialize()
        +void Render(RenderContext context)
        +void RenderGameOver(GameOverEventArgs args)
        +void Cleanup()
    }

    class IInputHandler {
        <<interface>>
        +InputResult ProcessInput(Player player)
    }

    class IGameClock {
        <<interface>>
        +void Wait(int milliseconds)
    }
```

### 4. Infrastructure Layer（インフラストラクチャ層）

**責務**: 外部システムとの接続実装

**特徴**:

- Application/Domain/Coreレイヤーに依存
- インターフェースの具体的な実装
- 外部依存を隠蔽

**コンポーネント**:

```mermaid
classDiagram
    class ConsoleInputHandler {
        +InputResult ProcessInput(Player player)
    }

    class SystemGameClock {
        +void Wait(int milliseconds)
    }

    class IInputHandler {
        <<interface>>
    }

    class IGameClock {
        <<interface>>
    }

    IInputHandler <|.. ConsoleInputHandler
    IGameClock <|.. SystemGameClock
```

### 5. Presentation Layer（プレゼンテーション層）

**責務**: ユーザーインターフェースの実装

**特徴**:

- すべてのレイヤーに依存可能
- UI固有のロジック
- 表示の詳細を担当

**コンポーネント**:

```mermaid
classDiagram
    class ConsoleGameRenderer {
        +void Initialize()
        +void Render(RenderContext context)
        +void RenderGameOver(GameOverEventArgs args)
        +void Cleanup()
    }

    class IGameRenderer {
        <<interface>>
    }

    IGameRenderer <|.. ConsoleGameRenderer
```

## データフロー図

```mermaid
flowchart TB
    User[ユーザー入力]
    Console[ConsoleInputHandler]
    GameLoop[GameLoopService]
    GameState[GameState]
    Player[Player]
    Enemy[Enemy]
    Lizard[Lizard]
    Renderer[ConsoleGameRenderer]
    Display[画面表示]

    User -->|キー入力| Console
    Console -->|InputResult| GameLoop
    GameLoop -->|ProcessInput| Player
    GameLoop -->|UpdateWorld| GameState
    GameState -->|Tick| Player
    GameState -->|Tick| Enemy
    GameState -->|Tick| Lizard
    GameState -->|CheckCollisions| GameState
    GameLoop -->|Render| Renderer
    Renderer -->|描画| Display
    Display -->|視覚フィードバック| User
```

## パッケージ図

```mermaid
graph TB
    subgraph "LifeGame.exe"
        subgraph "Presentation"
            ConsoleRenderer[ConsoleGameRenderer]
        end

        subgraph "Application"
            GameLoop[GameLoopService]
            Factory[GameFactory]
            IRenderer[IGameRenderer]
            IInput[IInputHandler]
            IClock[IGameClock]
        end

        subgraph "Domain"
            Entities[Entities/*]
            Behaviors[Behaviors/*]
            Events[Events/*]
            State[GameState]
        end

        subgraph "Infrastructure"
            ConsoleInput[ConsoleInputHandler]
            SystemClock[SystemGameClock]
        end

        subgraph "Core"
            ValueObjects[Position, Bounds, Direction]
            Settings[GameSettings]
        end
    end

    ConsoleRenderer -.implements.-> IRenderer
    ConsoleInput -.implements.-> IInput
    SystemClock -.implements.-> IClock

    GameLoop --> IRenderer
    GameLoop --> IInput
    GameLoop --> IClock
    GameLoop --> State

    Factory --> GameLoop

    State --> Entities
    State --> Settings

    Entities --> Behaviors
    Entities --> ValueObjects
```

## 依存性注入フロー

```mermaid
sequenceDiagram
    participant Main as Program.Main
    participant Life as LifeGame
    participant Factory as GameFactory
    participant Loop as GameLoopService
    participant State as GameState

    Main->>Life: new LifeGame()
    Life->>Life: new ConsoleGameRenderer()
    Life->>Life: new ConsoleInputHandler()
    Life->>Life: new SystemGameClock()
    Life->>Factory: new GameFactory(renderer, input, clock)
    Factory->>State: new GameState(settings, player, enemy, lizard)
    Factory->>Loop: new GameLoopService(state, renderer, input, clock)
    Factory-->>Life: return GameLoopService
    Life->>Loop: Run()
```
