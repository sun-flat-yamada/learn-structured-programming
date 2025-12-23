# Section04_ObjectOrientedProgramming

## 概要

**オブジェクト指向設計のベストプラクティスを適用したカメVSワニゲーム（2D版）**

このセクションは、Section03_StructuredProgrammingPlus のコードをオブジェクト指向設計の原則に基づいて完全に作り直したバージョンです。

## 適用したオブジェクト指向設計のベストプラクティス

### 1. SOLID原則

#### 単一責任の原則 (SRP: Single Responsibility Principle)
各クラスは単一の責任のみを持ちます：
- `GameConfig`: ゲーム設定の管理
- `GameState`: ゲーム状態の管理
- `Turtle`/`Crocodile`: キャラクター固有のロジック
- `ConsoleGameRenderer`: 画面描画
- `ConsoleInputHandler`: 入力処理

#### 開放/閉鎖原則 (OCP: Open/Closed Principle)
- インターフェース（`IGameRenderer`, `IInputHandler`）により、既存コードを変更せずに拡張可能
- 新しいレンダラーや入力ハンドラーの追加が容易

#### リスコフの置換原則 (LSP: Liskov Substitution Principle)
- `Turtle` と `Crocodile` は `Character` 基底クラスを継承
- 派生クラスは基底クラスとして扱える

#### インターフェース分離の原則 (ISP: Interface Segregation Principle)
- `IGameRenderer` と `IInputHandler` は必要最小限のメソッドのみを定義
- クライアントは使用しないメソッドに依存しない

#### 依存性逆転の原則 (DIP: Dependency Inversion Principle)
- `TurtleVsCrocodileGame` はインターフェースに依存
- 具体実装への依存を排除

### 2. デザインパターン

#### Template Method パターン
- `Character` 基底クラスで共通アルゴリズムを定義
- 派生クラスで特定の処理をオーバーライド

#### Strategy パターン
- `IGameRenderer` と `IInputHandler` で異なる実装を交換可能

#### Observer パターン
- `GameState` のイベント（`StateChanged`, `GameEnded`, `GameInitialized`）で状態変更を通知

#### Facade パターン
- `TurtleVsCrocodileGame` がゲーム全体の複雑さを隠蔽

### 3. 値オブジェクト (Value Object)
- `Position` 構造体: イミュータブルで等価性に基づく比較を実装

### 4. 依存性の注入 (Dependency Injection)
- コンストラクタを通じて依存性を注入
- テスタビリティと疎結合を実現

### 5. イミュータブル設計
- `GameConfig`: 作成後の変更不可
- `Position`: 値オブジェクトとして不変

## ファイル一覧

| ファイル | 責任 |
|---------|------|
| `Position.cs` | 2D位置を表す値オブジェクト |
| `GameConfig.cs` | ゲーム設定（イミュータブル） |
| `Character.cs` | キャラクター基底クラスとカメ/ワニ派生クラス |
| `GameState.cs` | ゲーム状態管理とイベント通知 |
| `IGameRenderer.cs` | レンダリングインターフェース |
| `IInputHandler.cs` | 入力処理インターフェース |
| `ConsoleGameRenderer.cs` | コンソールレンダリング実装 |
| `ConsoleInputHandler.cs` | コンソール入力処理実装 |
| `FrogVsSnakeGame.cs` | メインゲームクラス（ファサード） |

## クラス図

```
┌─────────────────┐
│ TurtleVsCrocodile│
│     Game        │ ←── ファサード
└────────┬────────┘
         │ 依存
    ┌────┴────┬────────────┬────────────┐
    ▼         ▼            ▼            ▼
┌────────┐ ┌──────────┐ ┌───────────┐ ┌───────────┐
│GameState│ │GameConfig│ │IGameRender│ │IInputHandl│
│        │ │ (不変)   │ │    er     │ │    er     │
└────┬───┘ └──────────┘ └─────┬─────┘ └─────┬─────┘
     │                        │             │
     │ 参照                   │ 実装        │ 実装
     ▼                        ▼             ▼
┌─────────┐            ┌───────────┐ ┌───────────┐
│Character│            │Console    │ │Console    │
│(abstract)│            │GameRender│ │InputHandlr│
└────┬────┘            └───────────┘ └───────────┘
     │ 継承
  ┌──┴──┐
  ▼     ▼
┌────┐ ┌────────┐
│Turtl│ │Crocodile│
└────┘ └────────┘
```

## 使用方法

```csharp
// デフォルト設定でゲームを開始
var game = new TurtleVsCrocodileGame();
game.Run();

// カスタム設定でゲームを開始
var config = new GameConfig(
    gameWidth: 40,
    gameHeight: 20,
    gameUpdateDelayMs: 150
);
var game = new TurtleVsCrocodileGame(config);
game.Run();

// テスト用にモックを注入
var mockRenderer = new MockGameRenderer();
var mockInputHandler = new MockInputHandler();
var game = new TurtleVsCrocodileGame(
    config: new GameConfig(),
    renderer: mockRenderer,
    inputHandler: mockInputHandler
);
game.Run();
```

## Section03からの主な変更点

| 観点 | Section03 (手続き型) | Section04 (OOP) |
|------|---------------------|-----------------|
| 状態管理 | 静的グローバル変数 | インスタンスフィールド |
| 位置表現 | 個別のint変数 | Position値オブジェクト |
| キャラクター | 静的メソッド | Character継承階層 |
| 描画 | 静的メソッド | IGameRendererインターフェース |
| 入力 | 静的メソッド | IInputHandlerインターフェース |
| 設定 | 定数クラス | イミュータブルGameConfig |
| テスト容易性 | 困難 | DIにより容易 |
| 拡張性 | 困難 | インターフェースで容易 |
