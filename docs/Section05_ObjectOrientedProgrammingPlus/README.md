# Section05_ObjectOrientedProgrammingPlus - Object-Oriented Programming Plus

## LifeGame Plus - 生命の逃避行

オブジェクト指向設計のベストプラクティスを適用した教育用ゲームプロジェクト

---

## 🚀 クイックスタート

### ドキュメントナビゲーション

**📑 [完全ドキュメント索引](./Index.md)** - すべてのドキュメントへのナビゲーション

### 学習パス別ガイド

#### 🌱 初級者向け

まずはプロジェクトの全体像を把握しましょう

1. **[概要](./Overview.md)** - プロジェクトの目的と構成
2. **[クラス図](./ClassDiagram.md)** - クラス構造の理解
3. **[API仕様](./APISpecification.md)** - 各クラスの使い方

#### 🌿 中級者向け

アーキテクチャとデザインパターンを学習

1. **[アーキテクチャ](./Architecture.md)** - Clean Architecture 5層構造
2. **[シーケンス図](./SequenceDiagrams.md)** - 処理フローの追跡
3. **[デザインパターン](./DesignPatterns.md)** - 7つのパターン適用例

#### 🌳 上級者向け

設計原則と複雑な状態管理を深掘り

1. **[SOLID原則](./SOLIDPrinciples.md)** - 5つの設計原則の実践
2. **[ステートマシン図](./StateMachineDiagrams.md)** - 状態遷移の詳細
3. **[コンポーネント図](./ComponentDiagram.md)** - システム全体の俯瞰

---

## 📚 ドキュメント一覧

| ドキュメント | 説明 | UML図数 |
| ------------ | ------ | -------- |
| [概要](./Overview.md) | プロジェクト全体像、技術スタック | 1 |
| [アーキテクチャ](./Architecture.md) | Clean Architecture 5層構造 | 8 |
| [クラス図](./ClassDiagram.md) | 全24クラスの構造と関係性 | 2 |
| [シーケンス図](./SequenceDiagrams.md) | 主要処理フロー | 8 |
| [ステートマシン図](./StateMachineDiagrams.md) | 状態遷移の詳細 | 9 |
| [コンポーネント図](./ComponentDiagram.md) | コンポーネント構成 | 9 |
| [デザインパターン](./DesignPatterns.md) | 7つのパターン詳細 | 8 |
| [SOLID原則](./SOLIDPrinciples.md) | 5つの原則の適用例 | 6 |
| [API仕様](./APISpecification.md) | 全クラス・インターフェースAPI | - |

**総UML図数**: 50以上

---

## 🎯 プロジェクト概要

### 新機能（Section04からの拡張）

#### トカゲ（🦎）キャラクター

- **安全時**: ランダムに歩き回る
- **危険時**: ワニから反対方向に逃げる
- **緊急時**: 尻尾を切り離して倍速で逃走
- **尻尾**: ワニの囮として機能

### 技術スタック

- **.NET 9.0** / **C# 13.0**
- **Clean Architecture** (5層構造)
- **SOLID原則** 完全準拠
- **7つのデザインパターン** 適用

### アーキテクチャ

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

---

## 🎨 適用されているデザインパターン

1. **Strategy Pattern** - 移動行動の戦略化
2. **State Pattern** - トカゲの状態管理
3. **Factory Pattern** - オブジェクト生成の集約
4. **Observer Pattern** - イベント駆動通信
5. **Builder Pattern** - 複雑な設定の構築
6. **Value Object Pattern** - 不変な値の表現
7. **Facade Pattern** - 複雑さの隠蔽

詳細は [デザインパターン](./DesignPatterns.md) を参照

---

## 🎯 SOLID原則の適用

- **S**ingle Responsibility - 各クラスが単一の責任
- **O**pen/Closed - 拡張に開き、修正に閉じる
- **L**iskov Substitution - 派生クラスの置換可能性
- **I**nterface Segregation - 小さなインターフェース
- **D**ependency Inversion - 抽象への依存

詳細は [SOLID原則](./SOLIDPrinciples.md) を参照

---

## 📊 統計情報

- **総クラス数**: 24
- **インターフェース数**: 4
- **列挙型数**: 7
- **値オブジェクト数**: 3
- **デザインパターン数**: 7
- **総行数**: 約2,500行
- **ドキュメントページ数**: 約100ページ相当

---

## 🔍 よくある質問

### Q: どのドキュメントから読めばいい？

A: [概要](./Overview.md) から始めて、[クラス図](./ClassDiagram.md) で構造を把握するのがおすすめです。

### Q: UML図はどこで見られる？

A: すべてのドキュメントにMermaid形式のUML図が含まれています。GitHub、VS Code、その他のMermaid対応ツールで表示できます。

### Q: コードの使い方を知りたい

A: [API仕様](./APISpecification.md) に全クラスの使用例があります。

### Q: 設計の理由を知りたい

A: [アーキテクチャ](./Architecture.md)、[デザインパターン](./DesignPatterns.md)、[SOLID原則](./SOLIDPrinciples.md) を参照してください。

---

## 📖 関連ドキュメント

- **Section04** - Object-Oriented Programming（基本版）
- **プロジェクトルート** - [README.md](../../README.md)
- **ソースコード** - [src/Section05_ObjectOrientedProgrammingPlus](../../src/Section05_ObjectOrientedProgrammingPlus/)

---

## 📝 ドキュメント更新履歴

| 日付 | バージョン | 更新内容 |
| ---------- | ---------- | --------- |
| 2026-01-06 | 1.0.0 | 初版作成 - 完全ドキュメント一式 |

---

## 🤝 貢献

ドキュメントの改善提案やフィードバックは、プロジェクトのIssueトラッカーまでお願いします。

---

Happy Learning! 🎓
