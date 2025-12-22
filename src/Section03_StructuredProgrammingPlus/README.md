# Section03_StructuredProgrammingPlus

## 概要

**完全対応版：カエルの行ごとランダム化実装**

このセクションは、Section02 のコメント内で説明されていた「パターン2（3段階確率分岐）」を完全に実装したバージョンです。

## 主な改修内容

### CharacterMovement.cs の改修

`MoveFrogRandomly()` メソッドを以下のように改修しました：

#### 変更前（Section02）
```csharp
public static void MoveFrogRandomly()
{
  // 60%の確率で移動
  if (_random.Next(100) >= 60)
  {
    return;
  }

  // 方向をランダムに決定（左または右）
  int newPosition = _random.Next(2) == 0
    ? GameState.FrogPosition - 1
    : GameState.FrogPosition + 1;

  if (GameRules.IsWithinBounds(newPosition))
  {
    GameState.FrogPosition = newPosition;
  }
}
```

#### 変更後（Section03_Plus）
```csharp
public static void MoveFrogRandomly()
{
  // 毎フレームで新しいランダム値を生成
  int randomValue = _random.Next(100);

  // 3段階の確率分岐：
  // - 0～29: 左に移動（30%の確率）
  // - 30～59: 右に移動（30%の確率）
  // - 60～99: 移動なし（40%の確率）

  int newPosition;

  if (randomValue < 30)
  {
    // 左方向に1マス移動
    newPosition = GameState.FrogPosition - 1;
  }
  else if (randomValue < 60)
  {
    // 右方向に1マス移動
    newPosition = GameState.FrogPosition + 1;
  }
  else
  {
    // 移動なし
    return;
  }

  if (GameRules.IsWithinBounds(newPosition))
  {
    GameState.FrogPosition = newPosition;
  }
}
```

## 重要なポイント

### 1. 3段階確率分岐の実装
- **左移動（0～29）**: 30%の確率
- **右移動（30～59）**: 30%の確率
- **移動なし（60～99）**: 40%の確率

このような明示的な確率分岐により、ゲームの難易度調整がしやすくなります。

### 2. 行ごとのランダム化
毎フレーム（ゲームループの各イテレーション）で新しいランダム値を生成するため：
- 各行の描画のたびに独立したランダム判定が実行される
- 行ごとに異なる移動パターンが実現される
- より自然でアニメーション的な動きになる

### 3. コード構造の改善
- ランダム値の生成を1回に統一（効率化）
- 確率判定とアクションが明確に分離されている
- 今後の保守性・拡張性が向上

## ファイル一覧

- `CharacterMovement.cs` - キャラクター移動処理（改修版）
- `GameState.cs` - ゲーム状態管理
- `GameRules.cs` - ゲームルール定義
- `GameRenderer.cs` - 画面描画処理
- `GameLogic.cs` - ゲームロジック処理
- `InputHandler.cs` - ユーザー入力処理
- `FrogVsSnakeGame.cs` - メインゲームクラス

## 実行方法

```bash
dotnet run
```

## 操作方法

- **[A] または [←]**: 左に移動
- **[D] または [→]**: 右に移動
- **[Q]**: ゲーム終了

## 与える影響

このセクションの実装により、以下の学習効果が期待できます：

1. **確率制御の実装方法** - Random.Next()を使った条件分岐の実装パターン
2. **ランダム性の導入** - ゲームに予測不可能性を加える方法
3. **構造化プログラミングの応用** - グローバル変数と関数の組み合わせ方
4. **フレームベースの更新ループ** - ゲームループでの毎フレーム処理の重要性

---

**参考**: このセクションは「学習用の構造化プログラミング」の実装例です。
本来はオブジェクト指向プログラミングを推奨します。
