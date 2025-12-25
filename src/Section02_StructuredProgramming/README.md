# Section02_StructuredProgramming - 構造化プログラミングのみを使用した実装

## 概要

Section01_UnstructuredProgrammingの非構造化プログラムを、構造化プログラミングの原則に従って書き直したバージョンです。

**重要**: このセクションは **分かりやすくオブジェクト指向設計と分離した構造化プログラミングのみ** を使用しています。機能分割にクラスを使用していますが、インスタンス化は行わず、すべてのメンバーは静的に定義して用いており、オブジェクト指向設計は一切取り入れていません。これは、オブジェクト指向設計と明確に区別するための措置であり、構造化プログラミング自体がこのような制限を持っていたわけではありません。構造化プログラミングをダイクストラが提言した時点で、データに対する分離と抽象化も重要なテーマでしたが、このセクションではあえてそれらを排除しています。

## 主な特徴

### 1. グローバル変数によるゲーム状態管理
```csharp
public static class GameState
{
    public static int TurtlePosition;
    public static int CrocodilePosition;
    public static int Score;
    public static bool IsActive;
}
```

### 2. 静的関数による手続き型プログラミング
すべての処理は静的クラスの静的関数で実装：
```csharp
public static class CharacterMovement
{
    public static void MoveTurtleAwayFromCrocodile()
    {
        // ワニが近づいてきたら反対方向に逃げる
        int distance = Math.Abs(GameState.TurtlePosition - GameState.CrocodilePosition);

        // ワニが近くにいる場合（5マス以内）
        if (distance <= 5)
        {
            int newPosition;
            if (GameState.CrocodilePosition < GameState.TurtlePosition)
            {
                // ワニが左にいるので右に逃げる
                newPosition = GameState.TurtlePosition + 1;
            }
            else
            {
                // ワニが右にいるので左に逃げる
                newPosition = GameState.TurtlePosition - 1;
            }

            if (GameRules.IsWithinBounds(newPosition))
            {
                GameState.TurtlePosition = newPosition;
            }
        }
    }
}
```

### 3. goto文の廃止
- **非構造化版**: goto文を使用した複雑な制御フロー
- **本実装**: while、if-else、for のみで制御

### 4. クラスは名前空間の整理のみ
クラスはインスタンス化されず、静的メンバーのコンテナとして機能

## 構造化プログラミングの3つの基本構造

```
1. 逐次処理（Sequence）
   実行順序に従って上から下へ実行

2. 選択処理（Selection）
   if-else, switchで条件分岐

3. 繰り返し処理（Iteration）
   while, for で繰り返し
```

すべての処理がこの3つで構成されています。

## ファイル構成

| ファイル              | 責任                                        |
| ------------------- | ------------------------------------------ |
| `GameState.cs`      | グローバル変数と状態操作関数               |
| `GameRules.cs`      | ゲームルール定義（定数と判定関数）         |
| `CharacterMovement.cs` | キャラクター移動処理（カメの逃走、ワニの追跡） |
| `GameRenderer.cs`   | 画面描画処理                               |
| `InputHandler.cs`   | ユーザー入力処理                           |
| `GameLogic.cs`      | ゲームロジック（衝突判定など）             |
| `TurtleVsCrocodileGame.cs` | メインゲームループ                         |
| `README.md`        | このファイル                               |

## 実行フロー

```csharp
public class TurtleVsCrocodileGame
{
    public void Run()
    {
        GameState.Initialize(...);  // グローバル変数を初期化
        
        while (GameState.IsActive)  // 繰り返し処理
        {
            GameRenderer.RenderGameScreen();       // 画面を描画
            InputHandler.ProcessInput();           // 入力を処理（カメがワニから逃げる）
            CharacterMovement.MoveCrocodileTowardsTurtle(); // ワニを移動
            
            if (GameLogic.IsCollisionDetected())   // 選択処理
            {
                GameState.End();
            }
            
            GameState.IncrementScore();
            Thread.Sleep(...);
        }
        
        GameRenderer.RenderGameOverScreen();
    }
}
```

## オブジェクト指向設計との違い

### 非構造化版（Before）
```
goto 文による複雑な制御フロー
```

### 本実装（Structured）
```
✅ グローバル変数で状態を一元管理
✅ 静的関数で処理を組織化
✅ ループと条件分岐のみで制御
✅ クラスはオブジェクトではなく機能の集約
```

## 使用例

```csharp
var game = new TurtleVsCrocodileGame();
game.Run();
```

## 学習ポイント

1. **goto文からの脱却**: ループと条件分岐で制御する方法
2. **グローバル変数の使用**: 状態を共有する最小限の方法
3. **関数分割**: 処理を管理可能なサイズに分割
4. **構造化プログラミングの本質**: 3つの基本構造の組み合わせ

## 参考資料

- Dijkstra, "Go To Statement Considered Harmful" (1968)
- 構造化プログラミングの歴史と原理
- C言語など初期のプログラミング言語での実装パターン
