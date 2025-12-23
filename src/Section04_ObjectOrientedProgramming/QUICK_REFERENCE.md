# Section04 クイックリファレンス

## オブジェクト指向設計のベストプラクティス一覧

### SOLID原則

| 原則 | 説明 | 適用箇所 |
|------|------|---------|
| **S**RP | 単一責任: 1クラス1責任 | 全クラス |
| **O**CP | 開放閉鎖: 拡張に開き、修正に閉じる | IGameRenderer, IInputHandler |
| **L**SP | リスコフ置換: 派生型は基底型と交換可能 | Character → Turtle, Crocodile |
| **I**SP | インターフェース分離: 必要最小限のインターフェース | IGameRenderer, IInputHandler |
| **D**IP | 依存性逆転: 抽象に依存 | TurtleVsCrocodileGame |

### デザインパターン

| パターン | 説明 | 適用箇所 |
|----------|------|---------|
| Template Method | 骨格をスーパークラスで定義 | Character |
| Strategy | アルゴリズムをカプセル化 | IGameRenderer, IInputHandler |
| Observer | 状態変更を通知 | GameState.StateChanged |
| Facade | 複雑さを隠蔽 | TurtleVsCrocodileGame |
| Value Object | イミュータブルな値 | Position |

### クラス責務マップ

```
TurtleVsCrocodileGame ─┬─ GameConfig (設定)
                       ├─ GameState (状態)
                       │    ├─ Turtle (プレイヤー)
                       │    └─ Crocodile (敵)
                       ├─ IGameRenderer (描画)
                       │    └─ ConsoleGameRenderer
                       └─ IInputHandler (入力)
                            └─ ConsoleInputHandler
```

### 拡張ポイント

1. **新しいレンダラー**: `IGameRenderer` を実装
2. **新しい入力**: `IInputHandler` を実装  
3. **新しいキャラクター**: `Character` を継承
4. **新しいイベント**: `GameState` にイベント追加

### コード例

```csharp
// ゲーム開始
var game = new TurtleVsCrocodileGame();
game.Run();

// DI付きゲーム開始
var game = new TurtleVsCrocodileGame(
    config: new GameConfig(gameWidth: 40),
    renderer: new ConsoleGameRenderer(config),
    inputHandler: new ConsoleInputHandler()
);
game.Run();
```
