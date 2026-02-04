# Section05_ObjectOrientedProgrammingPlus - Object-Oriented Programming Plus クラス図

## PlantUML図

すべてのクラス図はPlantUML形式で `diagrams/` ディレクトリに格納されています。

### 図一覧

| 図 | ファイル | 説明 |
| ---- | --------- | ------ |
| レイヤーアーキテクチャ | [01_LayerArchitecture.puml](./diagrams/01_LayerArchitecture.puml) | 5層構造の全体図 |
| Core Layer | [02_CoreLayer.puml](./diagrams/02_CoreLayer.puml) | Position, Bounds, GameSettings |
| Domain Entities | [03_DomainEntities.puml](./diagrams/03_DomainEntities.puml) | Entity, Player, Enemy, Lizard, Tail |
| Domain Behaviors | [04_DomainBehaviors.puml](./diagrams/04_DomainBehaviors.puml) | IMovementBehavior実装 |
| Domain GameState | [05_DomainGameState.puml](./diagrams/05_DomainGameState.puml) | GameState, Events |
| Application Layer | [06_ApplicationLayer.puml](./diagrams/06_ApplicationLayer.puml) | Services, Interfaces |
| Infrastructure Layer | [07_InfrastructureLayer.puml](./diagrams/07_InfrastructureLayer.puml) | Input, Timing |
| Presentation Layer | [08_PresentationLayer.puml](./diagrams/08_PresentationLayer.puml) | ConsoleGameRenderer |
| Entry Point | [09_EntryPoint.puml](./diagrams/09_EntryPoint.puml) | LifeGame (Facade) |

---

## クラス構成概要

### Core Layer

値オブジェクトと基本型（他のレイヤーに依存しない）

- **Position** - 2D位置を表す不変な値オブジェクト
- **Direction** - 移動方向を表す列挙型
- **Bounds** - ゲーム盤の境界を表す値オブジェクト
- **GameSettings** - ゲーム設定（Builder付き）

### Domain Layer

ビジネスロジックとドメインモデル

#### Entities

- **Entity** - ゲームエンティティの基底クラス（abstract）
- **Player** - プレイヤーキャラクター（カメ🐢）
- **Enemy** - 敵キャラクター（ワニ🐊）
- **Lizard** - トカゲキャラクター（🦎）- State Pattern適用
- **Tail** - トカゲの尻尾（囮）

#### Behaviors (Strategy Pattern)

- **IMovementBehavior** - 移動戦略インターフェース
- **ChaseMovementBehavior** - 追跡移動
- **FleeMovementBehavior** - 逃走移動
- **RandomMovementBehavior** - ランダム移動

#### State & Events

- **GameState** - ゲーム状態管理（Observer Pattern）
- **GamePhase** - ゲームフェーズ列挙型
- **LizardState** - トカゲ状態列挙型
- **GameStateChangedEventArgs** - 状態変更イベント
- **GameOverEventArgs** - ゲーム終了イベント
- **CollisionEventArgs** - 衝突イベント
- **TailEventArgs** - 尻尾イベント

### Application Layer

ユースケースとサービス

#### Services

- **GameLoopService** - ゲームループ管理（Facade Pattern）
- **GameFactory** - ゲームインスタンス生成（Factory Pattern）

#### Interfaces

- **IGameRenderer** - 描画インターフェース
- **IInputHandler** - 入力処理インターフェース
- **IGameClock** - 時間管理インターフェース
- **RenderContext** - 描画コンテキスト（struct）
- **InputResult** - 入力結果列挙型

### Infrastructure Layer

外部システムとの接続

- **ConsoleInputHandler** - コンソール入力実装
- **SystemGameClock** - システムクロック実装

### Presentation Layer

UI表示

- **ConsoleGameRenderer** - コンソール描画実装

### Entry Point

- **LifeGame** - ゲームのエントリーポイント（Facade Pattern）

---

## デザインパターン適用箇所

| パターン | 適用クラス | 説明 |
| --------- | ----------- | ------ |
| Strategy | IMovementBehavior | 移動アルゴリズムのカプセル化 |
| State | LizardState | トカゲの状態管理 |
| Factory | GameFactory | オブジェクト生成の集約 |
| Observer | GameState Events | イベント駆動通信 |
| Builder | GameSettings.Builder | 複雑なオブジェクト構築 |
| Value Object | Position, Bounds | 不変な値の表現 |
| Facade | LifeGame, GameLoopService | 複雑さの隠蔽 |

---

## SOLID原則の適用

| 原則 | 適用箇所 |
| ----- | --------- |
| **S**ingle Responsibility | 各クラスが単一の責任を持つ |
| **O**pen/Closed | IMovementBehaviorで拡張可能 |
| **L**iskov Substitution | Entity派生クラスが置換可能 |
| **I**nterface Segregation | 小さなインターフェース定義 |
| **D**ependency Inversion | 抽象への依存（DI） |

---

## 統計情報

- **総クラス数**: 24
- **インターフェース数**: 4
- **列挙型数**: 7
- **値オブジェクト数**: 3

---

## 関連ドキュメント

- [アーキテクチャ](./Architecture.md)
- [デザインパターン](./DesignPatterns.md)
- [SOLID原則](./SOLIDPrinciples.md)
- [API仕様](./APISpecification.md)
