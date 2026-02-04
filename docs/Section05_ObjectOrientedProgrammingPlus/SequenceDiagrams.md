# Section05_ObjectOrientedProgrammingPlus - シーケンス図

## PlantUML図

すべてのシーケンス図はPlantUML形式で `diagrams/` ディレクトリに格納されています。

### 図一覧

| 図 | ファイル | 説明 |
| ---- | --------- | ------ |
| ゲーム起動 | [10_Sequence_GameStartup.puml](./diagrams/10_Sequence_GameStartup.puml) | ゲーム起動シーケンス |
| ゲームループ | [11_Sequence_GameLoop.puml](./diagrams/11_Sequence_GameLoop.puml) | メインループシーケンス |

---

## シーケンス概要

### 1. ゲーム起動シーケンス

**参加者**: User → Program.Main → LifeGame → GameFactory → GameLoopService → GameState → Renderer

**フロー**:

1. ユーザーがプログラムを実行
2. `LifeGame`インスタンスを作成
3. `GameSettings.Default()`でデフォルト設定を取得
4. `ConsoleGameRenderer`, `ConsoleInputHandler`, `SystemGameClock`を生成
5. `GameFactory`でゲームオブジェクトを構築
   - Player, Enemy, Lizardを生成
   - GameStateを生成
   - GameLoopServiceを生成
6. `Run()`でゲームを開始
7. Rendererを初期化
8. GameStateを開始（Phase = Running）
9. ゲームループを実行

### 2. ゲームループシーケンス

**参加者**: GameLoopService → Renderer → InputHandler → GameState → Player → Enemy → Lizard → Clock

**フロー（1ティック）**:

1. **描画**: `Renderer.Render(context)`
2. **入力処理**: `InputHandler.ProcessInput(player)`
   - キー入力あり → プレイヤー移動
   - キー入力なし → デフォルト移動
3. **トカゲ行動**: `Lizard.Act(enemyPosition)`
   - 状態更新（Wandering/Fleeing/TailDropped）
   - 状態に応じた移動
4. **ターゲット決定**: `determineEnemyTarget()`
   - 尻尾がアクティブ → 尻尾を追跡
   - それ以外 → 最も近いターゲット
5. **敵移動**: `Enemy.MoveTowards(target)`
6. **ティック更新**: `GameState.Tick()`
7. **衝突判定**: `GameState.CheckCollisions()`
8. **待機**: `Clock.Wait(200ms)`
9. ループ継続または終了

### 3. プレイヤー移動シーケンス

**フロー**:

1. ユーザーがキー入力（W/A/S/D または矢印キー）
2. `InputHandler`がキーを検出
3. `Player.MoveUp/Down/Left/Right()`を呼び出し
4. `Entity.TryMove(direction)`で移動を試行
5. `Position.Move(deltaX, deltaY)`で新しい位置を計算
6. `Bounds.Contains(newPosition)`で境界チェック
7. 境界内なら位置を更新、境界外なら移動失敗

### 4. トカゲの状態遷移シーケンス

**フロー**:

1. `Lizard.Act(enemyPosition)`が呼ばれる
2. 敵との距離を計算
3. 状態を更新（`updateState`）
   - 距離 > fleeDistance → Wandering
   - 距離 <= fleeDistance → Fleeing
   - 距離 <= tailDropDistance → TailDropped（尻尾を切り離し）
4. 状態に応じた行動を実行
   - Wandering: `wander()` - ランダム移動
   - Fleeing: `flee()` - 逃走
   - TailDropped: `fleeWithSpeedBoost()` - 倍速逃走

### 5. 衝突判定シーケンス

**フロー**:

1. `GameState.CheckCollisions()`が呼ばれる
2. プレイヤーと敵の衝突チェック
   - 衝突 → `IsPlayerAlive = false`
3. トカゲと敵の衝突チェック
   - 衝突 → `IsLizardAlive = false`
4. 両方捕食されたかチェック
   - 両方捕食 → `endGame(AllCaught)`
5. 尻尾と敵の衝突チェック
   - 衝突 → `TailsEaten++`、尻尾を非アクティブ化

### 6. ワニのターゲット選択シーケンス

**優先順位**:

1. アクティブな尻尾があれば尻尾を追跡
2. 両方生存 → 最も近いターゲットを追跡
3. プレイヤーのみ生存 → プレイヤーを追跡
4. トカゲのみ生存 → トカゲを追跡

### 7. ゲーム終了シーケンス

**フロー**:

1. 終了条件の発生
   - プレイヤーがQ/Escキー押下 → `QuitByPlayer()`
   - 両方捕食 → `endGame(AllCaught)`
2. `Phase = Ended`に設定
3. `GameEnded`イベントを発行
4. ゲームループ終了
5. `Renderer.RenderGameOver(args)`でゲームオーバー画面表示
6. `Renderer.Cleanup()`でクリーンアップ

---

## 関連ドキュメント

- [アーキテクチャ](./Architecture.md)
- [ステートマシン図](./StateMachineDiagrams.md)
- [API仕様](./APISpecification.md)
