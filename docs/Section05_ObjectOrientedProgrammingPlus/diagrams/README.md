# Section05_ObjectOrientedProgrammingPlus - PlantUML図一覧

このディレクトリには、Section05のすべてのUML図がPlantUML形式で含まれています。

## 📊 図の種類

### アーキテクチャ図 (Architecture)

| ファイル | 説明 |
|---------|------|
| [01_LayerArchitecture.puml](./01_LayerArchitecture.puml) | レイヤーアーキテクチャ全体図 |

### クラス図 (Class Diagrams)

| ファイル | 説明 |
|---------|------|
| [02_CoreLayer.puml](./02_CoreLayer.puml) | Core Layer詳細クラス図 |
| [03_DomainEntities.puml](./03_DomainEntities.puml) | Domain Layer: Entities |
| [04_DomainBehaviors.puml](./04_DomainBehaviors.puml) | Domain Layer: Behaviors (Strategy Pattern) |
| [05_DomainGameState.puml](./05_DomainGameState.puml) | Domain Layer: GameState |
| [06_ApplicationLayer.puml](./06_ApplicationLayer.puml) | Application Layer |
| [07_InfrastructureLayer.puml](./07_InfrastructureLayer.puml) | Infrastructure Layer |
| [08_PresentationLayer.puml](./08_PresentationLayer.puml) | Presentation Layer |
| [09_EntryPoint.puml](./09_EntryPoint.puml) | Entry Point (Facade Pattern) |

### シーケンス図 (Sequence Diagrams)

| ファイル | 説明 |
|---------|------|
| [10_Sequence_GameStartup.puml](./10_Sequence_GameStartup.puml) | ゲーム起動シーケンス |
| [11_Sequence_GameLoop.puml](./11_Sequence_GameLoop.puml) | ゲームループシーケンス |

### ステートマシン図 (State Machine Diagrams)

| ファイル | 説明 |
|---------|------|
| [12_StateMachine_GamePhase.puml](./12_StateMachine_GamePhase.puml) | ゲームフェーズ状態遷移 |
| [13_StateMachine_LizardState.puml](./13_StateMachine_LizardState.puml) | トカゲの状態遷移 (State Pattern) |

### コンポーネント図 (Component Diagrams)

| ファイル | 説明 |
|---------|------|
| [14_Component_System.puml](./14_Component_System.puml) | システム全体コンポーネント図 |

### デザインパターン図 (Design Patterns)

| ファイル | 説明 |
|---------|------|
| [15_Pattern_Strategy.puml](./15_Pattern_Strategy.puml) | Strategy Pattern (戦略パターン) |
| [16_Pattern_State.puml](./16_Pattern_State.puml) | State Pattern (ステートパターン) |
| [17_Pattern_Factory.puml](./17_Pattern_Factory.puml) | Factory Pattern (ファクトリパターン) |
| [18_Pattern_Observer.puml](./18_Pattern_Observer.puml) | Observer Pattern (オブザーバーパターン) |
| [19_Pattern_Builder.puml](./19_Pattern_Builder.puml) | Builder Pattern (ビルダーパターン) |
| [20_Pattern_ValueObject.puml](./20_Pattern_ValueObject.puml) | Value Object Pattern (値オブジェクトパターン) |
| [21_Pattern_Facade.puml](./21_Pattern_Facade.puml) | Facade Pattern (ファサードパターン) |

### SOLID原則図 (SOLID Principles)

| ファイル | 説明 |
|---------|------|
| [22_SOLID_DependencyInversion.puml](./22_SOLID_DependencyInversion.puml) | Dependency Inversion Principle |

---

## 🔧 PlantUMLの使用方法

### VS Codeでの表示

1. **PlantUML拡張機能をインストール**
   - 拡張機能: `PlantUML` by jebbs

2. **プレビュー表示**
   - `.puml`ファイルを開く
   - `Alt+D` でプレビュー表示

3. **画像エクスポート**
   - コマンドパレット: `PlantUML: Export Current Diagram`
   - PNG, SVG, PDF形式でエクスポート可能

### オンラインビューア

PlantUMLファイルをオンラインで表示:
- [PlantUML Online Server](http://www.plantuml.com/plantuml/uml/)
- [PlantText](https://www.planttext.com/)

### コマンドライン

```bash
# PlantUMLのインストール (Java必要)
# Homebrewの場合
brew install plantuml

# 画像生成
plantuml diagram.puml

# SVG生成
plantuml -tsvg diagram.puml

# すべての図を生成
plantuml *.puml
```

---

## 📁 ディレクトリ構造

```
diagrams/
├── README.md                              # このファイル
├── 01_LayerArchitecture.puml              # アーキテクチャ
├── 02_CoreLayer.puml                      # Core Layer
├── 03_DomainEntities.puml                 # Domain Entities
├── 04_DomainBehaviors.puml                # Domain Behaviors
├── 05_DomainGameState.puml                # Domain GameState
├── 06_ApplicationLayer.puml               # Application Layer
├── 07_InfrastructureLayer.puml            # Infrastructure Layer
├── 08_PresentationLayer.puml              # Presentation Layer
├── 09_EntryPoint.puml                     # Entry Point
├── 10_Sequence_GameStartup.puml           # シーケンス: 起動
├── 11_Sequence_GameLoop.puml              # シーケンス: ループ
├── 12_StateMachine_GamePhase.puml         # ステート: ゲームフェーズ
├── 13_StateMachine_LizardState.puml       # ステート: トカゲ
├── 14_Component_System.puml               # コンポーネント
├── 15_Pattern_Strategy.puml               # パターン: Strategy
├── 16_Pattern_State.puml                  # パターン: State
├── 17_Pattern_Factory.puml                # パターン: Factory
├── 18_Pattern_Observer.puml               # パターン: Observer
├── 19_Pattern_Builder.puml                # パターン: Builder
├── 20_Pattern_ValueObject.puml            # パターン: Value Object
├── 21_Pattern_Facade.puml                 # パターン: Facade
└── 22_SOLID_DependencyInversion.puml      # SOLID: DIP
```

---

## 📊 統計情報

- **総図数**: 22
- **クラス図**: 8
- **シーケンス図**: 2
- **ステートマシン図**: 2
- **コンポーネント図**: 1
- **デザインパターン図**: 7
- **SOLID原則図**: 1
- **アーキテクチャ図**: 1

---

## 🎨 図の凡例

### ステレオタイプ

- `<<interface>>` - インターフェース
- `<<abstract>>` - 抽象クラス
- `<<struct>>` - 構造体
- `<<enumeration>>` - 列挙型
- `<<external>>` - 外部システム

### デザインパターン用ステレオタイプ

- `<<Strategy>>` - 戦略パターンの戦略
- `<<Context>>` - 戦略パターンのコンテキスト
- `<<State>>` - ステートパターンの状態
- `<<Factory>>` - ファクトリパターンのファクトリ
- `<<Product>>` - ファクトリパターンの生成物
- `<<Subject>>` - オブザーバーパターンのサブジェクト
- `<<Observer>>` - オブザーバーパターンのオブザーバー
- `<<Builder>>` - ビルダーパターンのビルダー
- `<<ValueObject>>` - 値オブジェクトパターン
- `<<Facade>>` - ファサードパターンのファサード

### 関係性

- `-->` - 依存 (Dependency)
- `--|>` - 継承 (Inheritance)
- `..|>` - 実装 (Implementation)
- `o-->` - 集約 (Aggregation)
- `*-->` - コンポジション (Composition)
- `..>` - 使用 (Usage)

---

## 📖 関連ドキュメント

- [Section05 Overview](../Overview.md)
- [Architecture](../Architecture.md)
- [Class Diagram](../ClassDiagram.md)
- [Design Patterns](../DesignPatterns.md)
- [SOLID Principles](../SOLIDPrinciples.md)

---

**Happy Diagramming! 📊**
