# Section03_StructuredProgrammingPlus

## 概要

**2D対応版：32x32マス目内を縦横自由に移動するカメVSワニゲーム**

このセクションは、Section02 の1D（左右移動のみ）を拡張し、32x32のマス目内を縦横自由に動くバージョンです。

## 主な改修内容

### GameState.cs の改修

1D座標から2D座標への変更：

```csharp
public static class GameState
{
    // 2D座標対応
    public static int TurtlePositionX;
    public static int TurtlePositionY;
    public static int CrocodilePositionX;
    public static int CrocodilePositionY;
    public static int Score;
    public static bool IsActive;
}
```

### CharacterMovement.cs の改修

`MoveTurtleRandomly()` メソッドを4方向対応に改修しました：

```csharp
public static void MoveTurtleRandomly()
{
  // 毎フレームで新しいランダム値を生成
  int randomValue = _random.Next(100);

  // 4方向の確率分岐：
  // - 0～24: 上に移動（25%の確率）
  // - 25～49: 下に移動（25%の確率）
  // - 50～74: 左に移動（25%の確率）
  // - 75～99: 右に移動（25%の確率）

  int newX = GameState.TurtlePositionX;
  int newY = GameState.TurtlePositionY;

  if (randomValue < 25)
  {
    // 上方向に1マス移動
    newY = GameState.TurtlePositionY - 1;
  }
  else if (randomValue < 50)
  {
    // 下方向に1マス移動
    newY = GameState.TurtlePositionY + 1;
  }
  else if (randomValue < 75)
  {
    // 左方向に1マス移動
    newX = GameState.TurtlePositionX - 1;
  }
  else
  {
    // 右方向に1マス移動
    newX = GameState.TurtlePositionX + 1;
  }

  if (GameRules.IsWithinBoundsX(newX) && GameRules.IsWithinBoundsY(newY))
  {
    GameState.TurtlePositionX = newX;
    GameState.TurtlePositionY = newY;
  }
}
```

## 重要なポイント

### 1. 4方向確率分岐の実装
- **上移動（0～24）**: 25%の確率
- **下移動（25～49）**: 25%の確率
- **左移動（50～74）**: 25%の確率
- **右移動（75～99）**: 25%の確率

各方向への移動確率が均等になるよう設計されています。

### 2. 2D座標システム
- X座標とY座標を独立して管理
- 境界チェックも `IsWithinBoundsX()` と `IsWithinBoundsY()` に分離
- ワニもX方向→Y方向の順で追跡

### 3. ワニの追跡アルゴリズム
ワニはまずX座標をカメに合わせ、その後Y座標を合わせる戦略で追跡します。

## ファイル一覧

| ファイル | 責任 |
|---------|------|
| `CharacterMovement.cs` | キャラクター移動処理（2D対応版） |
| `GameState.cs` | ゲーム状態管理（2D座標） |
| `GameRules.cs` | ゲームルール定義（2D境界チェック） |
| `GameRenderer.cs` | 画面描画処理 |
| `GameLogic.cs` | ゲームロジック処理 |
| `InputHandler.cs` | ユーザー入力処理（上下左右対応） |
| `TurtleVsCrocodileGame.cs` | メインゲームクラス |
| `README.md` | このファイル |

## 実行方法

```bash
dotnet run
```

## 操作方法

- **[W] または [↑]**: 上に移動
- **[S] または [↓]**: 下に移動
- **[A] または [←]**: 左に移動
- **[D] または [→]**: 右に移動
- **[Q]**: ゲーム終了

## 与える影響

このセクションの実装により、以下の学習効果が期待できます：

1. **2D座標システムの実装** - X/Y座標を使った位置管理
2. **確率制御の実装方法** - Random.Next()を使った条件分岐の実装パターン
3. **ランダム性の導入** - ゲームに予測不可能性を加える方法
4. **構造化プログラミングの応用** - グローバル変数と関数の組み合わせ方
5. **追跡アルゴリズム** - 2D空間での追跡ロジックの実装

---

**参考**: このセクションは「学習用の構造化プログラミング」の実装例です。
本来はオブジェクト指向プログラミングを推奨します。
