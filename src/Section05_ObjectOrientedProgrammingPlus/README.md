# LifeGame Plus - 生命の逃避行 🐢💨🐊🦎

オブジェクト指向設計のベストプラクティスを適用したコンソールゲーム（拡張版）

## ゲーム概要

キャラクター (オブジェクト) 毎に特性を定義し、それぞれの特性に従って行動します。
カメ(🐢)はユーザー操作で動きを外から操作することもできます。
ワニ(🐊)に捕まったら捕食されてゲームオーバーです。

### 新キャラクター: トカゲ 🦎

Section04から追加された新しいNPCキャラクターです。

**行動パターン:**
- 🚶 **うろうろモード**: ワニが遠くにいる安全な時は、ランダムに歩き回る
- 🏃 **逃走モード**: ワニが近づいてきたら（距離8以内）、反対方向に逃げる
- ⚡ **倍速逃走モード**: ワニがさらに近づいたら（距離4以内）、尻尾を切り離して倍速で逃げる

**尻尾について:**
- 尻尾は切り離された場所から動かない
- ワニは尻尾を優先的に追跡する（囮として機能）
- 尻尾がワニに捕食されてもゲームオーバーにはならない

### 操作方法

| キー | アクション |
|------|----------|
| W / ↑ | 上に移動 |
| S / ↓ | 下に移動 |
| A / ← | 左に移動 |
| D / → | 右に移動 |
| Q / Esc | ゲーム終了 |

## アーキテクチャ

クリーンアーキテクチャに基づいた5層構造を採用しています。

```
┌──────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│  UI/描画を担当                                            │
│  └── Console/ConsoleGameRenderer.cs                      │
├──────────────────────────────────────────────────────────┤
│                    Application Layer                     │
│  ユースケース・ゲームロジックを担当                         │
│  ├── Interfaces/                                         │
│  │   ├── IGameRenderer.cs                                │
│  │   ├── IInputHandler.cs                                │
│  │   └── IGameClock.cs                                   │
│  └── Services/                                           │
│      ├── GameLoopService.cs                              │
│      └── GameFactory.cs                                  │
├──────────────────────────────────────────────────────────┤
│                      Domain Layer                        │
│  ビジネスロジック・エンティティを担当                       │
│  ├── Entities/                                           │
│  │   ├── Entity.cs (基底クラス)                           │
│  │   ├── Player.cs (カメ)                                │
│  │   ├── Enemy.cs (ワニ)                                 │
│  │   ├── Lizard.cs (トカゲ) ★新規                        │
│  │   └── Tail.cs (尻尾) ★新規                           │
│  ├── Behaviors/                                          │
│  │   ├── IMovementBehavior.cs                            │
│  │   ├── RandomMovementBehavior.cs                       │
│  │   ├── ChaseMovementBehavior.cs                        │
│  │   └── FleeMovementBehavior.cs ★新規                   │
│  ├── Events/GameEvents.cs                                │
│  └── GameState.cs                                        │
├──────────────────────────────────────────────────────────┤
│                    Infrastructure Layer                  │
│  外部システムとの接続を担当                                │
│  ├── Input/ConsoleInputHandler.cs                        │
│  └── Timing/SystemGameClock.cs                           │
├──────────────────────────────────────────────────────────┤
│                       Core Layer                         │
│  基盤となる値オブジェクト・設定を定義                       │
│  ├── Position.cs                                         │
│  ├── Direction.cs                                        │
│  ├── Bounds.cs                                           │
│  └── GameSettings.cs                                     │
└──────────────────────────────────────────────────────────┘
```

## 適用されたデザインパターン

| パターン | 適用箇所 | 説明 |
|---------|---------|------|
| **依存性注入 (DI)** | 全体 | コンストラクタを通じた依存性の注入 |
| **ストラテジー** | IMovementBehavior | 移動アルゴリズムの交換可能な実装 |
| **ステート** | Lizard | トカゲの状態（うろうろ/逃走/尻尾切り離し）管理 |
| **ファクトリ** | GameFactory | ゲームインスタンスの生成 |
| **オブザーバー** | GameState | イベントによる状態変更通知 |
| **ビルダー** | GameSettings.Builder | 複雑な設定オブジェクトの段階的構築 |
| **値オブジェクト** | Position, Bounds | イミュータブルな値の表現 |
| **ファサード** | GameLoopService | ゲーム全体の複雑さを隠蔽 |
| **テンプレートメソッド** | Entity | 共通アルゴリズムの骨格定義 |

## SOLID原則の適用

### 単一責任の原則 (SRP)
各クラスが一つの責任のみを持つ:
- `Lizard`: トカゲの行動ロジックのみ
- `Tail`: 尻尾の状態管理のみ
- `FleeMovementBehavior`: 逃走方向の決定のみ

### 開放/閉鎖原則 (OCP)
新しい機能の追加が容易:
- `FleeMovementBehavior` を `IMovementBehavior` として追加
- 既存コードの修正なしで新しい移動パターンを追加可能

### リスコフの置換原則 (LSP)
派生クラスは基底クラスとして扱える:
- `Lizard`, `Tail` も `Entity` として扱える
- レンダリング時に統一的に処理可能

### インターフェース分離の原則 (ISP)
最小限のインターフェース定義:
- `IMovementBehavior` は `DetermineDirection` メソッドのみ

### 依存性逆転の原則 (DIP)
高レベルモジュールが抽象に依存:
- `GameLoopService` は `IGameRenderer` に依存（`ConsoleGameRenderer` ではなく）

## Section04からの変更点

1. **新しいエンティティ**
   - `Lizard.cs`: トカゲキャラクター（ステートパターンで状態管理）
   - `Tail.cs`: 切り離された尻尾

2. **新しい移動行動**
   - `FleeMovementBehavior.cs`: 逃走移動（追跡の反対方向へ）

3. **GameSettingsの拡張**
   - `InitialLizardPosition`: トカゲの初期位置
   - `LizardFleeDistance`: 逃走開始距離
   - `LizardTailDropDistance`: 尻尾切り離し距離

4. **GameStateの拡張**
   - トカゲの管理
   - 尻尾イベント（`TailDropped`, `TailEaten`）
   - ゲームオーバー理由の拡張

5. **レンダリングの拡張**
   - トカゲと尻尾の描画
   - トカゲ状態の表示
   - ゲームオーバーメッセージの拡張
