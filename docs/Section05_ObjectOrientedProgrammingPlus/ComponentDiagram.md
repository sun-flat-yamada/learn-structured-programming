# Section05_ObjectOrientedProgrammingPlus - コンポーネント図

## PlantUML図

コンポーネント図はPlantUML形式で `diagrams/` ディレクトリに格納されています。

### 図一覧

| 図 | ファイル | 説明 |
| ---- | ---- | ---- |
| システム全体 | [14_Component_System.puml](./diagrams/14_Component_System.puml) | システム全体コンポーネント図 |

---

## コンポーネント構成概要

### レイヤー別コンポーネント

#### Presentation Layer (緑)

- **ConsoleGameRenderer** - コンソール描画実装

#### Application Layer (黄緑)

- **GameLoopService** - ゲームループ管理
- **GameFactory** - オブジェクト生成

#### Domain Layer (黄)

- **GameState** - ゲーム状態管理
- **Entities** - Player, Enemy, Lizard, Tail
- **Behaviors** - 移動戦略実装
- **Events** - ゲームイベント

#### Infrastructure Layer (ピンク)

- **ConsoleInputHandler** - コンソール入力実装
- **SystemGameClock** - システムクロック実装

#### Core Layer (水色)

- **Value Objects** - Position, Bounds, Direction
- **GameSettings** - ゲーム設定

### インターフェース

- **IGameRenderer** - 描画インターフェース
- **IInputHandler** - 入力処理インターフェース
- **IGameClock** - 時間管理インターフェース
- **IMovementBehavior** - 移動戦略インターフェース

---

## 依存関係

### 実装関係

```text
ConsoleGameRenderer ..|> IGameRenderer
ConsoleInputHandler ..|> IInputHandler
SystemGameClock ..|> IGameClock
ChaseMovementBehavior ..|> IMovementBehavior
FleeMovementBehavior ..|> IMovementBehavior
RandomMovementBehavior ..|> IMovementBehavior
```

### コンポーネント間の依存

```text
GameLoopService --> IGameRenderer
GameLoopService --> IInputHandler
GameLoopService --> IGameClock
GameLoopService --> GameState

GameFactory --> GameLoopService
GameFactory --> GameState
GameFactory --> Entities

GameState --> Entities
GameState --> GameSettings
GameState --> Events

Entities --> Behaviors
Entities --> Value Objects

GameSettings --> Value Objects
```

---

## レイヤー間の依存ルール

1. **内側のレイヤーは外側のレイヤーを知らない**
2. **依存の方向は常に内側（Core）に向かう**
3. **インターフェースによる依存性の逆転**

```text
Presentation → Application → Domain → Core
                    ↑
Infrastructure ─────┘
```

---

## 外部システム

- **System.Console** - コンソールI/O
- **System.Threading** - スレッド管理

---

## 関連ドキュメント

- [アーキテクチャ](./Architecture.md)
- [クラス図](./ClassDiagram.md)
- [SOLID原則](./SOLIDPrinciples.md) - Dependency Inversion
