# Section05_ObjectOrientedProgrammingPlus - Object-Oriented Programming Plus 完全ドキュメント

## 目次

1. [概要](#概要)
2. [アーキテクチャ](./Architecture.md)
3. [クラス図](./ClassDiagram.md)
4. [シーケンス図](./SequenceDiagrams.md)
5. [ステートマシン図](./StateMachineDiagrams.md)
6. [コンポーネント図](./ComponentDiagram.md)
7. [デザインパターン](./DesignPatterns.md)
8. [SOLID原則](./SOLIDPrinciples.md)
9. [API仕様](./APISpecification.md)

## 概要

### プロジェクト名

LifeGame Plus - 生命の逃避行

### 目的

オブジェクト指向設計のベストプラクティスを適用した教育用ゲームプロジェクト。
Clean Architecture、SOLID原則、デザインパターンの実践的な実装例を提供します。

### 新機能（Section04からの拡張）

#### トカゲ（🦎）キャラクター

- **安全時**: ランダムに歩き回る
- **危険時**: ワニから反対方向に逃げる
- **緊急時**: 尻尾を切り離して倍速で逃走
- **尻尾**: ワニの囮として機能（食べられてもゲームオーバーにならない）

#### ゲームルール

- プレイヤー（カメ🐢）とトカゲ（🦎）の両方が生存
- ワニ（🐊）は最も近いターゲットを追跡
- 尻尾がある場合は尻尾を優先的に追跡
- カメとトカゲの両方が捕食されたらゲームオーバー

### 技術スタック

- **.NET 9.0** / **C# 13.0**
- **Clean Architecture** (5層構造)
- **SOLID原則** 完全準拠
- **7つのデザインパターン** 適用

### アーキテクチャ概要

```text
┌──────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│  (ConsoleGameRenderer)                                   │
├──────────────────────────────────────────────────────────┤
│                    Application Layer                     │
│  (GameLoopService, GameFactory, Interfaces)              │
├──────────────────────────────────────────────────────────┤
│                      Domain Layer                        │
│  (Entities, Behaviors, Events, GameState)                │
├──────────────────────────────────────────────────────────┤
│                    Infrastructure Layer                  │
│  (ConsoleInputHandler, SystemGameClock)                  │
├──────────────────────────────────────────────────────────┤
│                       Core Layer                         │
│  (Position, Direction, Bounds, GameSettings)             │
└──────────────────────────────────────────────────────────┘
```

### 主要コンポーネント

| レイヤー | コンポーネント | 責務 |
| --------- | -------------- | ------ |
| **Core** | Position, Bounds, Direction, GameSettings | 値オブジェクトと基本型 |
| **Domain** | Entity, Player, Enemy, Lizard, Tail | ビジネスロジック |
| **Domain** | IMovementBehavior, GameState | 移動戦略とゲーム状態 |
| **Application** | GameLoopService, GameFactory | ユースケースとサービス |
| **Application** | IGameRenderer, IInputHandler, IGameClock | インターフェース定義 |
| **Infrastructure** | ConsoleInputHandler, SystemGameClock | 外部システム接続 |
| **Presentation** | ConsoleGameRenderer | UI表示 |

### ファイル構成

```text
Section05_ObjectOrientedProgrammingPlus/
├── LifeGame.cs                          # エントリーポイント
├── Core/
│   ├── Position.cs                      # 位置の値オブジェクト
│   ├── Direction.cs                     # 方向の列挙型
│   ├── Bounds.cs                        # 境界の値オブジェクト
│   └── GameSettings.cs                  # ゲーム設定（Builder付き）
├── Domain/
│   ├── GameState.cs                     # ゲーム状態管理
│   ├── Entities/
│   │   ├── Entity.cs                    # エンティティ基底クラス
│   │   ├── Player.cs                    # プレイヤー（カメ）
│   │   ├── Enemy.cs                     # 敵（ワニ）
│   │   ├── Lizard.cs                    # トカゲ（新規）
│   │   └── Tail.cs                      # 尻尾（新規）
│   ├── Behaviors/
│   │   ├── IMovementBehavior.cs         # 移動戦略インターフェース
│   │   ├── ChaseMovementBehavior.cs     # 追跡移動
│   │   ├── FleeMovementBehavior.cs      # 逃走移動（新規）
│   │   └── RandomMovementBehavior.cs    # ランダム移動
│   └── Events/
│       └── GameEvents.cs                # ゲームイベント定義
├── Application/
│   ├── Services/
│   │   ├── GameLoopService.cs           # ゲームループ管理
│   │   └── GameFactory.cs               # ファクトリ
│   └── Interfaces/
│       ├── IGameRenderer.cs             # レンダラーインターフェース
│       ├── IInputHandler.cs             # 入力ハンドラーインターフェース
│       └── IGameClock.cs                # クロックインターフェース
├── Infrastructure/
│   ├── Input/
│   │   └── ConsoleInputHandler.cs       # コンソール入力実装
│   └── Timing/
│       └── SystemGameClock.cs           # システムクロック実装
└── Presentation/
    └── Console/
        └── ConsoleGameRenderer.cs       # コンソール描画実装
```

### 統計情報

- **総クラス数**: 24
- **インターフェース数**: 4
- **列挙型数**: 7
- **値オブジェクト数**: 3
- **デザインパターン数**: 7
- **総行数**: 約2,500行

### 学習ポイント

1. **Clean Architecture**: 依存関係の方向性とレイヤー分離
2. **SOLID原則**: 各原則の実践的な適用方法
3. **デザインパターン**: 実際の問題解決への適用
4. **値オブジェクト**: イミュータブルな設計
5. **依存性注入**: テスタビリティの向上
6. **イベント駆動**: 疎結合なコンポーネント間通信
7. **ステートパターン**: 複雑な状態管理の簡潔化
