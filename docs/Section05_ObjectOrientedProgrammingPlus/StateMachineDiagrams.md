# Section05_ObjectOrientedProgrammingPlus - ステートマシン図

## PlantUML図

すべてのステートマシン図はPlantUML形式で `diagrams/` ディレクトリに格納されています。

### 図一覧

| 図 | ファイル | 説明 |
| ---- | --------- | ------ |
| ゲームフェーズ | [12_StateMachine_GamePhase.puml](./diagrams/12_StateMachine_GamePhase.puml) | ゲームフェーズ状態遷移 |
| トカゲ状態 | [13_StateMachine_LizardState.puml](./diagrams/13_StateMachine_LizardState.puml) | トカゲの状態遷移 (State Pattern) |

---

## 状態遷移概要

### 1. ゲームフェーズ状態遷移

```text
[*] → NotStarted → Running → Ended → [*]
                      ↓↑
                    Paused
```

| 状態 | 説明 | 遷移条件 |
| ----- | ------ | --------- |
| **NotStarted** | 初期状態（Score=0, TickCount=0） | ゲーム作成時 |
| **Running** | ゲーム実行中（入力受付、移動、衝突判定） | `Start()` |
| **Paused** | 一時停止（未実装） | `Pause()` |
| **Ended** | 終了状態（スコア表示） | ゲームオーバー or `QuitByPlayer()` |

### 2. トカゲの状態遷移（State Pattern）

```text
[*] → Wandering ⇄ Fleeing → TailDropped
         ↓                      ↓
    TailDropped ←───────────────┘
```

| 状態 | 説明 | 行動 | 遷移条件 |
| ----- | ------ | ------ | --------- |
| **Wandering** | 🚶 うろうろ歩く | `wander()` - ランダム移動 | 初期状態、distance > fleeDistance |
| **Fleeing** | 🏃 逃走中 | `flee()` - ワニから逃げる | distance <= fleeDistance |
| **TailDropped** | ⚡ 倍速逃走 | `fleeWithSpeedBoost()` - 倍速移動 | distance <= tailDropDistance |

#### 状態遷移の詳細

Wandering → Fleeing

- 条件: `distance <= fleeDistance` (デフォルト: 8)
- 動作: 逃走モードに切り替え

Wandering → TailDropped

- 条件: `distance <= tailDropDistance` (デフォルト: 4)
- 動作: 尻尾を切り離し、倍速逃走開始

Fleeing → Wandering

- 条件: `distance > fleeDistance`
- 動作: 安全になったのでうろうろモードに戻る

Fleeing → TailDropped

- 条件: `distance <= tailDropDistance` かつ `HasTail`
- 動作: 尻尾を切り離し、倍速逃走開始

TailDropped → TailDropped

- 条件: 継続（尻尾は再生しない）
- 動作: 倍速は10ティック後に終了、逃走は継続

### 3. 入力処理状態遷移

```text
WaitingInput → CheckKeyAvailable → ReadKey → ProcessKey → Return
                     ↓
                  NoInput → Return
```

| 入力 | 結果 |
| ----- | ------ |
| W / ↑ | `InputResult.Moved` (上移動) |
| S / ↓ | `InputResult.Moved` (下移動) |
| A / ← | `InputResult.Moved` (左移動) |
| D / → | `InputResult.Moved` (右移動) |
| P | `InputResult.Pause` |
| Q / Esc | `InputResult.Quit` |
| その他 / なし | `InputResult.Continue` |

### 4. エンティティ移動状態遷移

```text
TryMove → CalculateNewPosition → CheckBounds → UpdatePosition → Success
                                      ↓
                                   Failure
```

| 状態 | 説明 |
| ----- | ------ |
| **TryMove** | 移動方向を受け取る |
| **CalculateNewPosition** | `direction.ApplyTo(Position)` |
| **CheckBounds** | `Bounds.Contains(newPosition)` |
| **UpdatePosition** | 境界内なら位置を更新 |
| **Success** | `return true` |
| **Failure** | `return false` (位置は変更されない) |

### 5. 尻尾のライフサイクル

```text
[*] → NotExist → Active → Eaten → Inactive
```

| 状態 | 説明 |
| ----- | ------ |
| **NotExist** | 尻尾なし（`DroppedTail = null`） |
| **Active** | 尻尾アクティブ（`IsActive = true`）、ワニの囮として機能 |
| **Eaten** | ワニに捕食される（`TailsEaten++`） |
| **Inactive** | 非アクティブ（`IsActive = false`）、表示されない |

### 6. ゲームループ状態遷移

```text
Initialize → SetupRenderer → StartGame → LoopCheck → Render → ProcessInput
                                            ↓           ↑
                                         Cleanup    UpdateWorld → CheckCollisions → Wait
```

| フェーズ | 処理 |
| --------- | ------ |
| **Initialize** | レンダラー初期化、ゲーム状態初期化 |
| **Render** | ゲーム盤描画、ステータス表示 |
| **ProcessInput** | キー入力処理、プレイヤー移動 |
| **UpdateWorld** | トカゲ行動、ワニ追跡、スコア加算 |
| **CheckCollisions** | 衝突判定、ゲームオーバー判定 |
| **Wait** | 次のティックまで待機（デフォルト200ms） |
| **Cleanup** | ゲームオーバー画面表示、リソース解放 |

---

## 関連ドキュメント

- [シーケンス図](./SequenceDiagrams.md)
- [デザインパターン](./DesignPatterns.md) - State Pattern
- [API仕様](./APISpecification.md)
