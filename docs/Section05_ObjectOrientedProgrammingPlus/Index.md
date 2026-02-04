# Section05_ObjectOrientedProgrammingPlus - Object-Oriented Programming Plus ドキュメント索引

## 📚 完全ドキュメント一覧

### 1. 📖 [概要 (Overview)](./Overview.md)

プロジェクトの全体像、目的、技術スタック、ファイル構成

**内容**:

- プロジェクト概要
- 新機能（トカゲキャラクター）
- アーキテクチャ概要
- ファイル構成
- 統計情報
- 学習ポイント

---

### 2. 🏗️ [アーキテクチャ (Architecture)](./Architecture.md)

Clean Architecture 5層構造の詳細説明

**内容**:

- レイヤー依存関係図
- 各レイヤーの詳細
  - Core Layer
  - Domain Layer
  - Application Layer
  - Infrastructure Layer
  - Presentation Layer
- データフロー図
- パッケージ図
- 依存性注入フロー

**UML図**:

- レイヤー依存関係図
- 各レイヤーのクラス図
- データフロー図
- パッケージ図
- 依存性注入シーケンス図

---

### 3. 📊 [クラス図 (Class Diagram)](./ClassDiagram.md)

全クラスの構造と関係性

**内容**:

- 全体アーキテクチャ図
- 詳細クラス図（24クラス）
- デザインパターン適用箇所
- SOLID原則の適用
- レイヤー構成

**UML図**:

- システム全体クラス図
- レイヤー別詳細クラス図
- 関係性図（継承、実装、依存）

---

### 4. 🔄 [シーケンス図 (Sequence Diagrams)](./SequenceDiagrams.md)

主要な処理フローの時系列表現

**内容**:

1. ゲーム起動シーケンス
2. ゲームループシーケンス
3. プレイヤー移動シーケンス
4. トカゲの状態遷移シーケンス
5. 衝突判定シーケンス
6. ワニのターゲット選択シーケンス
7. ゲーム終了シーケンス
8. 依存性注入シーケンス（テスト用）

**UML図**:

- 8つの主要シーケンス図

---

### 5. 🔀 [ステートマシン図 (State Machine Diagrams)](./StateMachineDiagrams.md)

状態遷移の詳細表現

**内容**:

1. ゲームフェーズ状態遷移図
2. トカゲの状態遷移図（詳細版）
3. トカゲの行動決定フローチャート
4. 入力処理状態遷移図
5. 衝突判定状態遷移図
6. エンティティ移動状態遷移図
7. 尻尾のライフサイクル状態遷移図
8. ゲームループ状態遷移図
9. 移動行動戦略選択状態遷移図

**UML図**:

- 9つのステートマシン図

---

### 6. 🧩 [コンポーネント図 (Component Diagram)](./ComponentDiagram.md)

システムのコンポーネント構成

**内容**:

1. システム全体コンポーネント図
2. レイヤー別コンポーネント詳細図
   - Core Layer
   - Domain Layer
   - Application Layer
   - Infrastructure Layer
   - Presentation Layer
3. 依存関係コンポーネント図
4. デプロイメント図
5. パッケージ依存関係図
6. インターフェース実装コンポーネント図
7. イベント駆動コンポーネント図
8. テスト可能性コンポーネント図
9. ファクトリパターンコンポーネント図

**UML図**:

- 9つのコンポーネント図

---

### 7. 🎨 [デザインパターン (Design Patterns)](./DesignPatterns.md)

適用された7つのデザインパターンの詳細

**内容**:

1. **Strategy Pattern** (戦略パターン)
   - IMovementBehavior
2. **State Pattern** (ステートパターン)
   - LizardState
3. **Factory Pattern** (ファクトリパターン)
   - GameFactory
4. **Observer Pattern** (オブザーバーパターン)
   - GameState Events
5. **Builder Pattern** (ビルダーパターン)
   - GameSettings.Builder
6. **Value Object Pattern** (値オブジェクトパターン)
   - Position, Bounds
7. **Facade Pattern** (ファサードパターン)
   - LifeGame, GameLoopService

各パターンについて:

- 目的
- クラス図
- コード例
- メリット

**UML図**:

- 各パターンのクラス図
- パターン適用マトリクス
- パターン間の相互作用図

---

### 8. 🎯 [SOLID原則 (SOLID Principles)](./SOLIDPrinciples.md)

5つのSOLID原則の適用例

**内容**:

1. **Single Responsibility Principle** (単一責任の原則)
2. **Open/Closed Principle** (開放/閉鎖原則)
3. **Liskov Substitution Principle** (リスコフの置換原則)
4. **Interface Segregation Principle** (インターフェース分離の原則)
5. **Dependency Inversion Principle** (依存性逆転の原則)

各原則について:

- 定義
- 良い例 vs 悪い例
- Section05での適用箇所
- メリット

**UML図**:

- 各原則の適用例クラス図
- SOLID原則の相互関係図
- 適用マトリクス

---

### 9. 📘 [API仕様 (API Specification)](./APISpecification.md)

全クラス・インターフェースのAPI詳細

**内容**:

- **Core Layer API**
  - Position, Direction, Bounds, GameSettings
- **Domain Layer API**
  - Entity, Player, Enemy, Lizard, Tail
  - IMovementBehavior, GameState
- **Application Layer API**
  - IGameRenderer, IInputHandler, IGameClock
  - GameLoopService, GameFactory
- **Infrastructure Layer API**
  - ConsoleInputHandler, SystemGameClock
- **Presentation Layer API**
  - ConsoleGameRenderer
- **Entry Point API**
  - LifeGame
- **イベント引数API**
- **列挙型一覧**

各APIについて:

- プロパティ一覧
- メソッドシグネチャ
- 使用例

---

## 📑 ドキュメント構成マップ

```text
Section05 Documentation
│
├── 📖 Overview (概要)
│   └── プロジェクト全体像
│
├── 🏗️ Architecture (アーキテクチャ)
│   ├── Clean Architecture 5層
│   └── 依存関係
│
├── 📊 Class Diagram (クラス図)
│   ├── 全体構造
│   └── 詳細クラス図
│
├── 🔄 Sequence Diagrams (シーケンス図)
│   ├── ゲーム起動
│   ├── ゲームループ
│   ├── プレイヤー移動
│   ├── トカゲ状態遷移
│   ├── 衝突判定
│   ├── ターゲット選択
│   ├── ゲーム終了
│   └── 依存性注入
│
├── 🔀 State Machine Diagrams (ステートマシン図)
│   ├── ゲームフェーズ
│   ├── トカゲ状態
│   ├── 入力処理
│   ├── 衝突判定
│   ├── エンティティ移動
│   ├── 尻尾ライフサイクル
│   ├── ゲームループ
│   └── 移動戦略選択
│
├── 🧩 Component Diagram (コンポーネント図)
│   ├── システム全体
│   ├── レイヤー別詳細
│   ├── 依存関係
│   ├── デプロイメント
│   ├── パッケージ依存
│   ├── インターフェース実装
│   ├── イベント駆動
│   ├── テスト可能性
│   └── ファクトリパターン
│
├── 🎨 Design Patterns (デザインパターン)
│   ├── Strategy Pattern
│   ├── State Pattern
│   ├── Factory Pattern
│   ├── Observer Pattern
│   ├── Builder Pattern
│   ├── Value Object Pattern
│   └── Facade Pattern
│
├── 🎯 SOLID Principles (SOLID原則)
│   ├── Single Responsibility
│   ├── Open/Closed
│   ├── Liskov Substitution
│   ├── Interface Segregation
│   └── Dependency Inversion
│
└── 📘 API Specification (API仕様)
    ├── Core Layer
    ├── Domain Layer
    ├── Application Layer
    ├── Infrastructure Layer
    ├── Presentation Layer
    └── Entry Point
```

---

## 🎓 学習パス

### 初級者向け

1. [概要](./Overview.md) - プロジェクト全体を理解
2. [クラス図](./ClassDiagram.md) - クラス構造を把握
3. [API仕様](./APISpecification.md) - 各クラスの使い方を学習

### 中級者向け

1. [アーキテクチャ](./Architecture.md) - Clean Architectureを理解
2. [シーケンス図](./SequenceDiagrams.md) - 処理フローを追跡
3. [デザインパターン](./DesignPatterns.md) - パターンの適用方法を学習

### 上級者向け

1. [SOLID原則](./SOLIDPrinciples.md) - 設計原則の実践を理解
2. [ステートマシン図](./StateMachineDiagrams.md) - 複雑な状態管理を分析
3. [コンポーネント図](./ComponentDiagram.md) - システム全体の構成を俯瞰

---

## 📐 PlantUML図

すべてのUML図はPlantUML形式で `diagrams/` ディレクトリに格納されています。

**[📁 PlantUML図一覧](./diagrams/README.md)**

| カテゴリ | 図数 |
| --------- | ----- |
| クラス図 | 9 |
| シーケンス図 | 2 |
| ステートマシン図 | 2 |
| コンポーネント図 | 1 |
| デザインパターン図 | 7 |
| SOLID原則図 | 1 |
| **合計** | **22** |

---

## 📊 統計情報

- **総ドキュメント数**: 10ファイル
- **総PlantUML図数**: 22
- **カバー範囲**:
  - クラス: 24
  - インターフェース: 4
  - 列挙型: 7
  - デザインパターン: 7
  - SOLID原則: 5

---

## 🔍 クイックリファレンス

### よく参照されるセクション

| 目的 | ドキュメント | セクション |
| ----- | ------------ | ----------- |
| クラスの使い方を知りたい | [API仕様](./APISpecification.md) | 該当レイヤー |
| 処理の流れを知りたい | [シーケンス図](./SequenceDiagrams.md) | 該当シーケンス |
| 状態遷移を理解したい | [ステートマシン図](./StateMachineDiagrams.md) | 該当ステート |
| パターンの実装を見たい | [デザインパターン](./DesignPatterns.md) | 該当パターン |
| 設計原則を学びたい | [SOLID原則](./SOLIDPrinciples.md) | 該当原則 |
| アーキテクチャを理解したい | [アーキテクチャ](./Architecture.md) | レイヤー構成 |

---

## 📝 ドキュメント更新履歴

| 日付 | バージョン | 更新内容 |
| ---------- | ---------- | --------- |
| 2026-01-06 | 1.0.0 | 初版作成 - 完全ドキュメント一式 |

---

## 📧 フィードバック

ドキュメントに関するフィードバックや改善提案は、プロジェクトのIssueトラッカーまでお願いします。
