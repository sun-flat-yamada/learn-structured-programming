# エドガー・W. ダイクストラ チューリング賞講演
# "The Humble Programmer"（謙虚なるプログラマ）

- **講演者**: Edsger W. Dijkstra
- **受賞年**: 1972年（ACM チューリング賞 受賞講演）
- **原典**: EWD340
- **原文URL**: https://www.cs.utexas.edu/~EWD/transcriptions/EWD03xx/EWD340.html
- **翻訳URL**: https://www.unixuser.org/~euske/doc/dijkstra-ja/thehumbleprogrammer.html

---

## 目次

1. [講演の概要](#1-講演の概要)
2. [第1部：プログラミング職業の黎明期](#2-第1部プログラミング職業の黎明期)
3. [第2部：ソフトウェア危機の到来](#3-第2部ソフトウェア危機の到来)
4. [第3部：第三世代コンピュータへの批判](#4-第3部第三世代コンピュータへの批判)
5. [第4部：プログラミング言語の歴史的評価](#5-第4部プログラミング言語の歴史的評価)
6. [第5部：革命的変化のビジョン](#6-第5部革命的変化のビジョン)
7. [第6部：技術的実現可能性を支える6つの論拠](#7-第6部技術的実現可能性を支える6つの論拠)
8. [第7部：階層構造とコンピュータの知的挑戦](#8-第7部階層構造とコンピュータの知的挑戦)
9. [結論：謙虚なプログラマとしての姿勢](#9-結論謙虚なプログラマとしての姿勢)
10. [講演の主要メッセージ一覧](#10-講演の主要メッセージ一覧)

---

## 1. 講演の概要

ダイクストラは本講演で、自身のプログラミング職業への参入から始まり、ソフトウェア危機の原因分析、プログラミング言語の批判的評価、そして将来への展望を語った。講演全体を貫くテーマは **「謙虚さ」** であり、人間の知的能力の限界を認識し、それに適した方法論とツールを用いるべきという主張である。

### 講演の構成（時系列）

| セクション | 内容 |
|-----------|------|
| 導入 | プログラミング職業の誕生と社会的認知の遅れ |
| 歴史的分析 | 初期コンピュータ時代の問題と誤解 |
| 危機の分析 | ソフトウェア危機の原因と背景 |
| 言語評価 | FORTRAN, LISP, ALGOL 60, PL/1 の批判的検討 |
| 将来展望 | 革命的変化の可能性と条件 |
| 6つの論拠 | 技術的実現可能性を支える根拠 |
| 結論 | 謙虚なプログラマとしての心構え |

---

## 2. 第1部：プログラミング職業の黎明期

### 2.1 ダイクストラ自身の経験

- **1952年春**: オランダで最初のプログラマとして職業的にプログラミングを開始
- 当時、プログラミングは職業として認知されていなかった
- ライデン大学で理論物理学を学びながら、プログラミングとの両立に苦悩

### 2.2 ファン・ワインハールデンとの対話

ダイクストラの上司であったファン・ワインハールデン（A. van Wijngaarden）との対話が転機となった：

> **原文**: "he went on to explain quietly that automatic computers were here to stay, that we were just at the beginning and could not I be one of the persons called to make programming a respectable discipline in the years to come?"
>
> **日本語訳**: 「彼は静かに説明を続けた。自動計算機は定着するものであり、我々はまだ始まったばかりだ。君はこれからの年月でプログラミングを立派な学問分野にする使命を担う人物の一人になれるのではないか？」

この言葉がダイクストラの人生の転換点となった。

### 2.3 社会的認知の欠如

- **1957年**: 結婚届で「プログラマ」と職業を記載しようとしたが、アムステルダム市当局に拒否された
- 結婚証明書には「理論物理学者」と記載されることになった
- これはプログラミング職業の社会的認知の遅れを象徴するエピソード

### 2.4 初期の誤解

初期のプログラマについて2つの誤った見解が生まれた：

> **原文**: "The one opinion was that a really competent programmer should be puzzle-minded and very fond of clever tricks; the other opinion was that programming was nothing more than optimizing the efficiency of the computational process, in one direction or the other."
>
> **日本語訳**: 「一つの見解は、本当に有能なプログラマはパズル指向で巧妙なトリックを非常に好むべきだというものだった。もう一つの見解は、プログラミングは計算プロセスの効率を何らかの方向に最適化することに過ぎないというものだった。」

1. **パズル指向の誤解**: 優秀なプログラマはパズル好きで巧妙なトリックを使うべき
2. **最適化至上主義**: プログラミングは計算効率の最適化に過ぎない

---

## 3. 第2部：ソフトウェア危機の到来

### 3.1 期待と現実のギャップ

当初の期待：
> **原文**: "once more powerful machines were available, programming would no longer be a problem"
>
> **日本語訳**: 「より強力なマシンが利用可能になれば、プログラミングはもはや問題ではなくなる」

現実：
> **原文**: "But instead of finding ourselves in the state of eternal bliss of all programming problems solved, we found ourselves up to our necks in the software crisis!"
>
> **日本語訳**: 「しかし、すべてのプログラミング問題が解決された永遠の至福の状態に到達する代わりに、我々はソフトウェア危機の首まで浸かっていることに気づいた！」

### 3.2 ソフトウェア危機の原因

#### 副次的原因（Minor Causes）

1. **I/O割り込み**: 予測不可能で再現不可能なタイミングで発生
   - 従来の決定論的オートマトンからの劇的な変化
   - システムプログラマの白髪の原因

2. **多層記憶装置**: 管理戦略の問題が依然として解決困難

#### 主要原因（Major Cause）

> **原文**: "To put it quite bluntly: as long as there were no machines, programming was no problem at all; when we had a few weak computers, programming became a mild problem, and now we have gigantic computers, programming had become an equally gigantic problem."
>
> **日本語訳**: 「率直に言えば：マシンが存在しなかったとき、プログラミングは全く問題ではなかった。弱いコンピュータが少しあったとき、プログラミングは軽い問題になった。そして巨大なコンピュータを持つ今、プログラミングは同様に巨大な問題になった。」

> **原文**: "In this sense the electronic industry has not solved a single problem, it has only created them, it has created the problem of using its products."
>
> **日本語訳**: 「この意味で、電子産業は一つの問題も解決しておらず、問題を創出しただけだ。自社製品を使用するという問題を創出したのだ。」

- 電子産業は問題を解決したのではなく、**問題を創出した**
- ハードウェアの能力が1000倍以上向上すると、社会の野心も同様に拡大
- プログラマは目的と手段の間で板挟みになった

### 3.3 1968年ガルミッシュ会議

- ソフトウェア危機が初めて公式に認められた転換点
- ソフトウェアエンジニアリングに関する会議で、危機の存在が公然と認められた

---

## 4. 第3部：第三世代コンピュータへの批判

### 4.1 設計思想への批判

1960年代中頃に登場した第三世代コンピュータに対する厳しい批判：

> **原文**: "The official literature tells us that their price/performance ratio has been one of the major design objectives. But if you take as 'performance' the duty cycle of the machine's various components, little will prevent you from ending up with a design in which the major part of your performance goal is reached by internal housekeeping activities of doubtful necessity."
>
> **日本語訳**: 「公式文献は、価格/性能比が主要な設計目標の一つだったと告げている。しかし『性能』をマシン各部品の稼働率と定義すれば、疑わしい必要性の内部管理活動で性能目標の大部分を達成する設計に行き着くことを止めるものはほとんどない。」

### 4.2 具体的問題点

- プログラミングが非常に困難な設計
- 早期束縛決定を強制する命令コード
- 解決不可能な競合を引き起こす設計

### 4.3 ダイクストラの嘆き

> **原文**: "I felt that with a single stroke the progress of computing science had been retarded by at least ten years: it was then that I had the blackest week in the whole of my professional life."
>
> **日本語訳**: 「一撃で計算機科学の進歩が少なくとも10年は後退させられたと感じた。それは私の職業人生で最も暗い一週間だった。」

### 4.4 誤った正当化への反論

多くの人々が「これだけ多く売れているのだから、設計がそれほど悪いはずがない」と主張することへの反論：

> **原文**: "But upon closer inspection, that line of defense has the same convincing strength as the argument that cigarette smoking must be healthy because so many people do it."
>
> **日本語訳**: 「しかしより近くで検討すれば、その防御線は、喫煙は多くの人がやっているから健康に良いに違いないという議論と同程度の説得力しかない。」

### 4.5 コンピュータレビューの必要性

- 科学雑誌が論文をレビューするように、新しいコンピュータもレビューされるべき
- ダイクストラ自身、1960年代初頭にそのようなレビューを書いたが、発表する勇気がなかった
- 一般的に受け入れられた基準の欠如が問題

---

## 5. 第4部：プログラミング言語の歴史的評価

### 5.1 EDSAC とサブルーチンライブラリ

- ケンブリッジのEDSACでは、最初から**サブルーチンライブラリ**が中心的役割

> **原文**: "We should recognise the closed subroutines as one of the greatest software inventions; it has survived three generations of computers and it will survive a few more, because it caters for the implementation of one of our basic patterns of abstraction."
>
> **日本語訳**: 「クローズドサブルーチンをソフトウェア最大の発明の一つとして認識すべきだ。それは3世代のコンピュータを生き延び、さらに数世代を生き延びるだろう。なぜなら、それは我々の基本的な抽象化パターンの一つの実装を提供するからだ。」

- クローズドサブルーチンは**ソフトウェア最大の発明の一つ**
- 抽象化の基本パターンを実装する手段として、3世代のコンピュータを生き延びた

### 5.2 FORTRAN（1950年代後半〜）

**評価**:
- 当時としては大胆なプロジェクトであり、責任者たちは賞賛に値する
- 10年以上の使用後に明らかになった欠点を責めるのは不公平

**批判**:

> **原文**: "In retrospect we must rate FORTRAN as a successful coding technique, but with very few effective aids to conception"
>
> **日本語訳**: 「振り返れば、FORTRANは成功したコーディング技法と評価しなければならないが、概念化のための効果的な補助はほとんどない」

> **原文**: "The sooner we can forget that FORTRAN has ever existed, the better, for as a vehicle of thought it is no longer adequate: it wastes our brainpower, is too risky and therefore too expensive to use."
>
> **日本語訳**: 「FORTRANが存在したことを忘れれば忘れるほど良い。思考の手段としてもはや適切ではない：脳力を浪費し、リスクが高すぎ、そのため使用するには高価すぎる。」

> **原文**: "FORTRAN's tragic fate has been its wide acceptance, mentally chaining thousands and thousands of programmers to our past mistakes."
>
> **日本語訳**: 「FORTRANの悲劇的な運命は、その広範な受容により、何千ものプログラマを過去の過ちに精神的に縛り付けたことだ。」

### 5.3 LISP

**評価**:
- 完全に異なる性質の魅力的な試み
- 非常に基本的な原則に基づき、驚くべき安定性を示した
- 最も洗練されたコンピュータ応用の多くを担ってきた

**名言**:

> **原文**: "LISP has jokingly been described as 'the most intelligent way to misuse a computer'. I think that description a great compliment because it transmits the full flavour of liberation: it has assisted a number of our most gifted fellow humans in thinking previously impossible thoughts."
>
> **日本語訳**: 「LISPは『コンピュータを誤用する最も知的な方法』と冗談で言われてきた。私はこれを大きな褒め言葉だと思う。なぜなら、それは解放の完全な風味を伝えているからだ：それは我々の最も才能ある同胞たちの多くが、以前は不可能だった思考をするのを助けてきた。」

### 5.4 ALGOL 60

**評価**:
- **実装非依存**でプログラミング言語を定義しようとした真摯な努力の成果
- **BNF（Backus-Naur Form）** の力を見事に実証
- ピーター・ナウアによる注意深い英語表現の力を示した

**影響**:

> **原文**: "I think that it is fair to say that only very few documents as short as this have had an equally profound influence on the computing community."
>
> **日本語訳**: 「これほど短い文書でこれほど深い影響を計算機界に与えたものは非常に少ないと言って良いと思う。」

**批判**:
- BNFの強力さゆえに、過度に複雑で体系的でない構文が数ページに詰め込まれた
- パラメータ機構は組み合わせの自由度が高すぎ、使用には強い規律が必要
- 実装が高価で使用が危険

### 5.5 PL/1

**厳しい批判**:

> **原文**: "Finally, although the subject is not a pleasant one, I must mention PL/1, a programming language for which the defining documentation is of a frightening size and complexity."
>
> **日本語訳**: 「最後に、快い話題ではないが、PL/1について言及しなければならない。その定義文書は恐ろしいほどの大きさと複雑さを持つプログラミング言語だ。」

> **原文**: "Using PL/1 must be like flying a plane with 7000 buttons, switches and handles to manipulate in the cockpit."
>
> **日本語訳**: 「PL/1を使うのは、コックピットに7000個のボタン、スイッチ、レバーがある飛行機を操縦するようなものに違いない。」

> **原文**: "And if I have to describe the influence PL/1 can have on its users, the closest metaphor that comes to my mind is that of a drug."
>
> **日本語訳**: 「PL/1がユーザーに与える影響を説明しなければならないとすれば、私の心に浮かぶ最も近い比喩は麻薬だ。」

あるPL/1ユーザーの講演について：

> **原文**: "But within a one-hour lecture in praise of PL/1, he managed to ask for the addition of about fifty new 'features', little supposing that the main source of his problems could very well be that it contained already far too many 'features'."
>
> **日本語訳**: 「PL/1を称賛する1時間の講演の中で、彼は約50の新しい『機能』の追加を求めた。問題の主な原因がすでに機能が多すぎることかもしれないとは思いもしなかった。」

> **原文**: "The speaker displayed all the depressing symptoms of addiction, reduced as he was to the state of mental stagnation in which he could only ask for more, more, more..."
>
> **日本語訳**: 「講演者は依存症のすべての抑うつ的な症状を示し、もっと、もっと、もっとと求めることしかできない精神的停滞の状態に陥っていた。」

最終的評価：

> **原文**: "When FORTRAN has been called an infantile disorder, full PL/1, with its growth characteristics of a dangerous tumor, could turn out to be a fatal disease."
>
> **日本語訳**: 「FORTRANが幼児期の病気と呼ばれるなら、PL/1はその危険な腫瘍のような成長特性を持ち、致命的な病気になりうる。」

---

## 6. 第5部：革命的変化のビジョン

### 6.1 将来への展望

> **原文**: "The vision is that, well before the seventies have run to completion, we shall be able to design and implement the kind of systems that are now straining our programming ability, at the expense of only a few percent in man-years of what they cost us now, and that besides that, these systems will be virtually free of bugs."
>
> **日本語訳**: 「ビジョンはこうだ：1970年代が終わるかなり前に、現在プログラミング能力を限界まで酷使しているようなシステムを、現在の数パーセントの人月で設計・実装でき、さらにそれらのシステムは事実上バグがなくなるだろう。」

### 6.2 品質とコストの関係

> **原文**: "Those who want really reliable software will discover that they must find means of avoiding the majority of bugs to start with, and as a result the programming process will become cheaper."
>
> **日本語訳**: 「本当に信頼性の高いソフトウェアを望む者は、最初から大部分のバグを避ける手段を見つけなければならないことを発見するだろう。その結果、プログラミングプロセスはより安くなる。」

> **原文**: "If you want more effective programmers, you will discover that they should not waste their time debugging, they should not introduce the bugs to start with."
>
> **日本語訳**: 「より効果的なプログラマが欲しければ、彼らはデバッグに時間を浪費すべきではなく、最初からバグを導入すべきではないことを発見するだろう。」

### 6.3 革命の3条件

革命が起こるための3つの必要条件：

| 条件 | 内容 | 状況 |
|------|------|------|
| 1. 認識 | 変化の必要性が広く認識されること | ✓ 1968年ガルミッシュ会議で認められた |
| 2. 経済的必要性 | 十分に強い経済的動機があること | ✓ ハードウェア価格が10分の1になる見込み |
| 3. 技術的実現可能性 | 技術的に可能であること | 次節で6つの論拠を提示 |

---

## 7. 第6部：技術的実現可能性を支える6つの論拠

### 論拠1：知的管理可能性による選択肢の削減

> **原文**: "Argument one is that, as the programmer only needs to consider intellectually manageable programs, the alternatives he is choosing between are much, much easier to cope with."
>
> **日本語訳**: 「論拠1は、プログラマが知的に管理可能なプログラムのみを考慮すればよいので、選択肢は遥かに扱いやすくなるということだ。」

**発見されたルール**:
- **第1種**: 機械的に強制可能（例：goto文の排除、複数出力パラメータを持つ手続きの排除）
- **第2種**: プログラマの規律として要求される（例：ループの終了証明、不変量の明示）

### 論拠2：解空間の劇的削減

> **原文**: "Argument two is that, as soon as we have decided to restrict ourselves to the subset of the intellectually manageable programs, we have achieved, once and for all, a drastic reduction of the solution space to be considered."
>
> **日本語訳**: 「論拠2は、知的に管理可能なプログラムの部分集合に制限することを決めた時点で、考慮すべき解空間の劇的な削減を一度で達成したということだ。」

### 論拠3：正当性の構成的アプローチ

従来のアプローチへの批判：

> **原文**: "Today a usual technique is to make a program and then to test it. But: program testing can be a very effective way to show the presence of bugs, but is hopelessly inadequate for showing their absence."
>
> **日本語訳**: 「今日の一般的な技法は、プログラムを作成してからテストすることだ。しかし：プログラムテストはバグの存在を示すには非常に効果的だが、バグの不在を示すには絶望的に不十分だ。」

提案するアプローチ：

> **原文**: "But one should not first make the program and then prove its correctness, because then the requirement of providing the proof would only increase the poor programmer's burden. On the contrary: the programmer should let correctness proof and program grow hand in hand."
>
> **日本語訳**: 「しかし、プログラムを先に作って正当性を証明するべきではない。そうすると証明を提供する要件が不幸なプログラマの負担を増やすだけだからだ。そうではなく：プログラマは正当性証明とプログラムを手を取り合って成長させるべきだ。」

> **原文**: "If one first asks oneself what the structure of a convincing proof would be and, having found this, then constructs a program satisfying this proof's requirements, then these correctness concerns turn out to be a very effective heuristic guidance."
>
> **日本語訳**: 「まず説得力のある証明の構造がどうあるべきかを自問し、それを見つけてから、その証明の要件を満たすプログラムを構築すれば、正当性への関心は非常に効果的な発見的指針となる。」

### 論拠4：抽象化のパターン

**抽象化の目的**:

> **原文**: "In this connection it might be worth-while to point out that the purpose of abstracting is not to be vague, but to create a new semantic level in which one can be absolutely precise."
>
> **日本語訳**: 「この文脈で、抽象化の目的は曖昧になることではなく、絶対的に正確でありうる新しい意味レベルを創造することだと指摘する価値があるかもしれない。」

**発見**:

> **原文**: "Enough is now known about these patterns of abstraction that you could devote a lecture to about each of them."
>
> **日本語訳**: 「これらの抽象化パターンについては、それぞれに一つの講義を捨てられるほど十分に知られている。」

**具体例**:

> **原文**: "What the familiarity and conscious knowledge of these patterns of abstraction imply dawned upon me when I realized that, had they been common knowledge fifteen years ago, the step from BNF to syntax-directed compilers, for instance, could have taken a few minutes instead of a few years."
>
> **日本語訳**: 「これらの抽象化パターンの愉しみと意識的な知識が何を意味するかは、もしそれらが15年前に共通知識だったら、例えばBNFから構文指向コンパイラへのステップは数年ではなく数分で済んだだろうと気づいたときに明らかになった。」

### 論拠5：ツールが思考に与える影響

**重要な洞察**:

> **原文**: "the tools we are trying to use and the language or notation we are using to express or record our thoughts, are the major factors determining what we can think or express at all!"
>
> **日本語訳**: 「使おうとしているツールや、思考を表現・記録するために使う言語や記法は、何を考え表現できるかを決定する主要な要因である！」

**プログラマの資質**:

> **原文**: "The competent programmer is fully aware of the strictly limited size of his own skull; therefore he approaches the programming task in full humility, and among other things he avoids clever tricks like the plague."
>
> **日本語訳**: 「有能なプログラマは自分の頭蓋骨の厳しく限られた大きさを十分に認識している。したがって、完全な謙虚さをもってプログラミング課題に取り組み、とりわけ巧妙なトリックをペストのように避ける。」

**one-liners現象への批判**:
ある会話型プログラミング言語のコミュニティで観察される現象：

> **原文**: "one programmer places a one-line program on the desk of another and either he proudly tells what it does and adds the question 'Can you code this in less symbols?' —as if this were of any conceptual relevance!— or he just asks 'Guess what it does!'."
>
> **日本語訳**: 「あるプログラマが別のプログラマの机に1行のプログラムを置き、誇らしそうにそれが何をするかを話し、『これより少ない記号でコーディングできるか？』と問う——まるでこれに概念的な意味があるかのように！——あるいは『何をするか当ててみろ！』と言う。」

> **原文**: "From this observation we must conclude that this language as a tool is an open invitation for clever tricks; and while exactly this may be the explanation for some of its appeal, viz. to those who like to show how clever they are, I am sorry, but I must regard this as one of the most damning things that can be said about a programming language."
>
> **日本語訳**: 「この観察から、この言語はツールとして巧妙なトリックへの公然たる招待状だと結論しなければならない。まさにそれがその魅力の一部の説明かもしれないが——自分がいかに賢いかを見せたい人々には——申し訳ないが、これはプログラミング言語について言える最も批判的なことの一つだと見なさなければならない。」

**将来の言語への期待**:

> **原文**: "I see a great future for very systematic and very modest programming languages."
>
> **日本語訳**: 「非常に体系的で非常に控えめなプログラミング言語に大きな将来を見ている。」

> **原文**: "When I say 'modest', I mean that, for instance, not only ALGOL 60's 'for clause', but even FORTRAN's 'DO loop' may find themselves thrown out as being too baroque."
>
> **日本語訳**: 「『控えめ』と言うとき、例えばALGOL 60の『for節』だけでなく、FORTRANの『DOループ』さえも、バロック過ぎるとして捨てられるかもしれないという意味だ。」

### 論拠6：階層構造の適用可能性

**信念の表明**:

> **原文**: "I could even go one step further and make an article of faith out of it, viz. that the only problems we can really solve in a satisfactory manner are those that finally admit a nicely factored solution."
>
> **日本語訳**: 「さらに一歩進めてこれを信条にすることもできる。すなわち、本当に満足のいく形で解決できる問題は、最終的にうまく分解された解決策を認める問題だけだということだ。」

**実践的含意**:

> **原文**: "By the time that we are sufficiently modest to try factored solutions only, because the other efforts escape our intellectual grip, we shall do our utmost best to avoid all those interfaces impairing our ability to factor the system in a helpful way."
>
> **日本語訳**: 「他の努力が我々の知的把握を逃れるため、分解された解決策のみを試みるほど謙虚になれば、システムを有用な方法で分解する能力を損なうすべてのインターフェースを避けるために最善を尽くすだろう。」

---

## 8. 第7部：階層構造とコンピュータの知的挑戦

### 8.1 階層システムの性質

> **原文**: "Hierarchical systems seem to have the property that something considered as an undivided entity on one level, is considered as a composite object on the next lower level of greater detail"
>
> **日本語訳**: 「階層システムは、あるレベルで分割不可能な実体と見なされるものが、より詳細な次の下位レベルでは複合オブジェクトと見なされるという性質を持つようだ」

具体例：
- 壁 → レンガ → 結晶 → 分子 ...

### 8.2 コンピュータの特異性

> **原文**: "In computer programming our basic building block has an associated time grain of less than a microsecond, but our program may take hours of computation time."
>
> **日本語訳**: 「コンピュータプログラミングでは、基本構成要素の時間粒度はマイクロ秒未満だが、プログラムの実行には何時間もかかることがある。」

> **原文**: "I do not know of any other technology covering a ratio of 10^10 or more: the computer, by virtue of its fantastic speed, seems to be the first to provide us with an environment where highly hierarchical artefacts are both possible and necessary."
>
> **日本語訳**: 「10^10以上の比率をカバーする他の技術を知らない。コンピュータはその驚異的な速度のおかげで、高度に階層的な人工物が可能でありかつ必要である環境を初めて提供しているようだ。」

### 8.3 知的挑戦としてのプログラミング

> **原文**: "This challenge, viz. the confrontation with the programming task, is so unique that this novel experience can teach us a lot about ourselves."
>
> **日本語訳**: 「プログラミング課題との対峰というこの挑戦は非常にユニークであり、この新しい経験は我々自身について多くを教えてくれる。」

期待される効果：
- 設計と創造のプロセスへの理解を深める
- 思考を組織化する作業に対するより良い制御を与える

---

## 9. 結論：謙虚なプログラマとしての姿勢

### 9.1 革命への障害

予想される保守的勢力：
- 大企業よりも、**教育機関**や**保守的なユーザーグループ**
- 古いプログラムを書き直す価値がないと考える人々
- 大規模アプリケーション（例：高エネルギー物理学）による中央計算施設の選択への影響

### 9.2 政治的障壁

> **原文**: "The first effect of teaching a methodology —rather than disseminating knowledge— is that of enhancing the capacities of the already capable, thus magnifying the difference in intelligence. In a society in which the educational system is used as an instrument for the establishment of a homogenized culture, in which the cream is prevented from rising to the top, the education of competent programmers could be politically impalatable."
>
> **日本語訳**: 「知識を広めるのではなく方法論を教えることの最初の効果は、すでに能力のある人の能力を高め、知能の差を拡大することだ。均質化された文化を確立するための道具として教育システムが使われ、クリームが上に浮くのを阻止する社会では、有能なプログラマの教育は政治的に不快なものかもしれない。」

### 9.3 最終メッセージ

> **原文**: "Automatic computers have now been with us for a quarter of a century. They have had a great impact on our society in their capacity of tools, but in that capacity their influence will be but a ripple on the surface of our culture, compared with the much more profound influence they will have in their capacity of intellectual challenge without precedent in the cultural history of mankind."
>
> **日本語訳**: 「自動計算機は今や四半世紀の歴史を持つ。道具としての能力において社会に大きな影響を与えてきたが、その能力における影響は、人類の文化史において前例のない知的挑戦としての、はるかに深い影響に比べれば、文化の表面に立つさざ波に過ぎない。」

> **原文**: "We shall do a much better programming job, provided that we approach the task with a full appreciation of its tremendous difficulty, provided that we stick to modest and elegant programming languages, provided that we respect the intrinsic limitations of the human mind and approach the task as Very Humble Programmers."
>
> **日本語訳**: 「我々はプログラミング課題の途方もない困難さを十分に認識し、控えめで優雅なプログラミング言語にこだわり、人間の心の本質的な限界を尊重し、**非常に謙虚なプログラマ（Very Humble Programmers）** として課題に取り組むなら、はるかに良いプログラミングの仕事ができるだろう。」

---

## 10. 講演の主要メッセージ一覧

### 歴史的教訓

| # | メッセージ |
|---|-----------|
| 1 | プログラミングは職業として認知されるのに長い時間がかかった |
| 2 | 初期の「パズル指向」「最適化至上主義」は誤りだった |
| 3 | より強力なマシンは問題を解決せず、より大きな問題を生み出した |

### ソフトウェア危機について

| # | メッセージ |
|---|-----------|
| 4 | ソフトウェア危機は1968年のガルミッシュ会議で公式に認められた |
| 5 | 危機の主因はマシンの能力向上に伴う野心の拡大 |
| 6 | 第三世代コンピュータの設計は計算機科学の進歩を10年後退させた |

### プログラミング言語について

| # | メッセージ |
|---|-----------|
| 7 | クローズドサブルーチンはソフトウェア最大の発明の一つ |
| 8 | FORTRANは忘れられるべき過去の遺物 |
| 9 | LISPは解放をもたらす知的ツール |
| 10 | ALGOL 60はBNFの力を示したが、複雑すぎる構文を許した |
| 11 | PL/1は麻薬のように危険で致命的になりうる |

### プログラミング方法論について

| # | メッセージ |
|---|-----------|
| 12 | テストはバグの存在を示せるが、不在は示せない |
| 13 | 正当性証明とプログラムを同時に成長させるべき |
| 14 | 抽象化の目的は曖昧さではなく、新しい精密さの創造 |
| 15 | ツールと言語は思考を決定的に形作る |

### プログラマの姿勢について

| # | メッセージ |
|---|-----------|
| 16 | 有能なプログラマは自分の知的限界を認識している |
| 17 | 巧妙なトリックはペストのように避けるべき |
| 18 | 謙虚さをもってプログラミングに取り組むべき |
| 19 | 控えめで優雅な言語を使うべき |

### 将来への展望

| # | メッセージ |
|---|-----------|
| 20 | より良いツールができても、プログラミングは困難であり続ける |
| 21 | 階層的分解が複雑なシステムへの唯一の道 |
| 22 | コンピュータは人類史上前例のない知的挑戦を提供している |

---

## 付録：重要な引用（原文と日本語訳）

### プログラマの謙虚さについて

> **原文**: "The competent programmer is fully aware of the strictly limited size of his own skull; therefore he approaches the programming task in full humility, and among other things he avoids clever tricks like the plague."
>
> **日本語訳**: 「有能なプログラマは自分の頭蓋骨の厳しく限られた大きさを十分に認識している。したがって、完全な謙虚さをもってプログラミング課題に取り組み、とりわけ巧妙なトリックをペストのように避ける。」

### テストの限界について

> **原文**: "Program testing can be a very effective way to show the presence of bugs, but is hopelessly inadequate for showing their absence."
>
> **日本語訳**: 「プログラムテストはバグの存在を示すには非常に効果的だが、バグの不在を示すには絶望的に不十分だ。」

### 抽象化について

> **原文**: "The purpose of abstracting is not to be vague, but to create a new semantic level in which one can be absolutely precise."
>
> **日本語訳**: 「抽象化の目的は曖昧になることではなく、絶対的に正確でありうる新しい意味レベルを創造することだ。」

### 最終結論

> **原文**: "We shall do a much better programming job, provided that we approach the task with a full appreciation of its tremendous difficulty, provided that we stick to modest and elegant programming languages, provided that we respect the intrinsic limitations of the human mind and approach the task as Very Humble Programmers."
>
> **日本語訳**: 「我々はプログラミング課題の途方もない困難さを十分に認識し、控えめで優雅なプログラミング言語にこだわり、人間の心の本質的な限界を尊重し、**非常に謙虚なプログラマ（Very Humble Programmers）** として課題に取り組むなら、はるかに良いプログラミングの仕事ができるだろう。」

---

## 付録2：現代プログラミングパラダイムへの対応づけ

ダイクストラの1972年チューリング賞講演 "The Humble Programmer" には、後に発展した現代プログラミングパラダイムの萌芽が数多く含まれている。以下に4つの主要パラダイムと講演内容の対応関係を整理する。

---

### 1. モデリング（Modeling）

**パラダイムの本質**: 私たち人間の抽象化能力をふさわしい部分で使えば、プログラムの作成、あるいは理解に必要な知的労力は激減する。

#### 講演における関連箇所

**抽象化の目的**:
> **原文**: "the purpose of abstracting is not to be vague, but to create a new semantic level in which one can be absolutely precise."
>
> **日本語訳**: 「抽象化の目的は曖昧になることではなく、絶対的に正確でありうる新しい意味レベルを創造することだ。」

**知的労力の削減**:
> **原文**: "the effective exploitation of his powers of abstraction must be regarded as one of the most vital activities of a competent programmer."
>
> **日本語訳**: 「抽象化能力の効果的な活用は、有能なプログラマの最も重要な活動の一つと見なされなければならない。」

**階層的思考**:
> **原文**: "Hierarchical systems seem to have the property that something considered as an undivided entity on one level, is considered as a composite object on the next lower level of greater detail"
>
> **日本語訳**: 「階層システムは、あるレベルで分割不可能な実体と見なされるものが、より詳細な次の下位レベルでは複合オブジェクトと見なされるという性質を持つようだ」

#### 現代への示唆

ダイクストラは、抽象化を「曖昧さ」ではなく「新しい精密さの創造」と定義した。これは現代のモデリング（ドメインモデリング、UML、DDD等）において、業務概念を正確に捉えるモデルを構築することの理論的基盤となっている。適切な抽象レベルでモデルを構築すれば、複雑なシステムも知的に管理可能になるという主張は、現代のモデリング手法の核心と一致する。

---

### 2. デザインパターン（Design Patterns）

**パラダイムの本質**: 人の持つ抽象化という能力で複数の構造パターンを見いだせる。

#### 講演における関連箇所

**抽象化パターンの発見**:
> **原文**: "Enough is now known about these patterns of abstraction that you could devote a lecture to about each of them."
>
> **日本語訳**: 「これらの抽象化パターンについては、それぞれに一つの講義を捧げられるほど十分に知られている。」

**パターン知識の効果**:
> **原文**: "had they been common knowledge fifteen years ago, the step from BNF to syntax-directed compilers, for instance, could have taken a few minutes instead of a few years."
>
> **日本語訳**: 「もしそれらが15年前に共通知識だったら、例えばBNFから構文指向コンパイラへのステップは数年ではなく数分で済んだだろう。」

**クローズドサブルーチンというパターン**:
> **原文**: "We should recognise the closed subroutines as one of the greatest software inventions; it has survived three generations of computers and it will survive a few more, because it caters for the implementation of one of our basic patterns of abstraction."
>
> **日本語訳**: 「クローズドサブルーチンをソフトウェア最大の発明の一つとして認識すべきだ。それは3世代のコンピュータを生き延び、さらに数世代を生き延びるだろう。なぜなら、それは我々の基本的な抽象化パターンの一つの実装を提供するからだ。」

#### 現代への示唆

ダイクストラは「抽象化パターン」という概念を明確に述べ、それらが共通知識として普及すれば開発効率が劇的に向上すると予見した。この考えは、1994年のGoF（Gang of Four）による「デザインパターン」の体系化へと直接つながる。パターンを「発明」ではなく「発見」として捉え、共通言語として普及させるという発想は、まさにダイクストラが示唆した方向性である。

---

### 3. テスト駆動開発（Test-Driven Development）

**パラダイムの本質**: プログラムを作ってからテストでバグがないことを証明するのは悪魔の証明であり、テストを作りながらプログラミングすべき。

#### 講演における関連箇所

**テストの限界（悪魔の証明）**:
> **原文**: "program testing can be a very effective way to show the presence of bugs, but is hopelessly inadequate for showing their absence."
>
> **日本語訳**: 「プログラムテストはバグの存在を示すには非常に効果的だが、バグの不在を示すには絶望的に不十分だ。」

**事後証明の問題**:
> **原文**: "But one should not first make the program and then prove its correctness, because then the requirement of providing the proof would only increase the poor programmer's burden."
>
> **日本語訳**: 「しかし、プログラムを先に作って正当性を証明するべきではない。そうすると証明を提供する要件が不幸なプログラマの負担を増やすだけだからだ。」

**証明とプログラムの同時成長**:
> **原文**: "the programmer should let correctness proof and program grow hand in hand."
>
> **日本語訳**: 「プログラマは正当性証明とプログラムを手を取り合って成長させるべきだ。」

**証明先行のアプローチ**:
> **原文**: "If one first asks oneself what the structure of a convincing proof would be and, having found this, then constructs a program satisfying this proof's requirements, then these correctness concerns turn out to be a very effective heuristic guidance."
>
> **日本語訳**: 「まず説得力のある証明の構造がどうあるべきかを自問し、それを見つけてから、その証明の要件を満たすプログラムを構築すれば、正当性への関心は非常に効果的な発見的指針となる。」

#### 現代への示唆

ダイクストラの「正当性証明とプログラムを手を取り合って成長させる」という主張は、ケント・ベックが1990年代後半に体系化したTDD（テスト駆動開発）の理論的先駆けである。「まず証明（テスト）の構造を考え、それを満たすプログラムを書く」というアプローチは、TDDの「Red-Green-Refactor」サイクルそのものである。事後的なテストではなく、テストを設計の指針として先行させる発想が、すでに1972年に明確に述べられていた。

---

### 4. アジャイル開発（Agile Development）

**パラダイムの本質**: テストを作る過程で業務の構造を見つけ、仕様として定義していくのが良い。

#### 講演における関連箇所

**発見的指針としての正当性**:
> **原文**: "these correctness concerns turn out to be a very effective heuristic guidance."
>
> **日本語訳**: 「正当性への関心は非常に効果的な発見的指針となる。」

**分解された解決策の発見**:
> **原文**: "the only problems we can really solve in a satisfactory manner are those that finally admit a nicely factored solution."
>
> **日本語訳**: 「本当に満足のいく形で解決できる問題は、最終的にうまく分解された解決策を認める問題だけだ。」

**謙虚さと反復的アプローチ**:
> **原文**: "By the time that we are sufficiently modest to try factored solutions only, because the other efforts escape our intellectual grip, we shall do our utmost best to avoid all those interfaces impairing our ability to factor the system in a helpful way."
>
> **日本語訳**: 「他の努力が我々の知的把握を逃れるため、分解された解決策のみを試みるほど謙虚になれば、システムを有用な方法で分解する能力を損なうすべてのインターフェースを避けるために最善を尽くすだろう。」

**バグを最初から避ける**:
> **原文**: "If you want more effective programmers, you will discover that they should not waste their time debugging, they should not introduce the bugs to start with."
>
> **日本語訳**: 「より効果的なプログラマが欲しければ、彼らはデバッグに時間を浪費すべきではなく、最初からバグを導入すべきではないことを発見するだろう。」

#### 現代への示唆

ダイクストラの「正当性への関心が発見的指針となる」という主張は、アジャイル開発における「テストを通じて仕様を発見する」プラクティスの先駆けである。テストを書く過程で業務の本質的な構造が明らかになり、それが仕様として定義されていくという考え方は、TDDとBDD（振る舞い駆動開発）を通じてアジャイル手法の中核となった。

また、「謙虚さ」という講演の主題は、アジャイル宣言の精神——変化への適応、継続的な学習、顧客との協調——と深く共鳴する。完璧な事前設計を諦め、反復的に解決策を発見していく姿勢は、まさに「謙虚なプログラマ」の態度そのものである。

---

### 5. ドメイン駆動設計（Domain-Driven Design）

**パラダイムの本質**: 業務ドメインの専門家と開発者が共通言語（ユビキタス言語）を構築し、ドメインモデルを中心にソフトウェアを設計する。モデルはドメインの本質を捉え、コードと一体化する。

#### 講演における関連箇所

**意味レベルの創造**:
> **原文**: "the purpose of abstracting is not to be vague, but to create a new semantic level in which one can be absolutely precise."
>
> **日本語訳**: 「抽象化の目的は曖昧になることではなく、絶対的に正確でありうる新しい意味レベルを創造することだ。」

**ツールと言語が思考を形作る**:
> **原文**: "the tools we are trying to use and the language or notation we are using to express or record our thoughts, are the major factors determining what we can think or express at all!"
>
> **日本語訳**: 「使おうとしているツールや、思考を表現・記録するために使う言語や記法は、何を考え表現できるかを決定する主要な要因である！」

**階層的分解と境界の重要性**:
> **原文**: "we shall do our utmost best to avoid all those interfaces impairing our ability to factor the system in a helpful way."
>
> **日本語訳**: 「システムを有用な方法で分解する能力を損なうすべてのインターフェースを避けるために最善を尽くすだろう。」

**うまく分解された解決策**:
> **原文**: "the only problems we can really solve in a satisfactory manner are those that finally admit a nicely factored solution."
>
> **日本語訳**: 「本当に満足のいく形で解決できる問題は、最終的にうまく分解された解決策を認める問題だけだ。」

**階層システムの性質**:
> **原文**: "Hierarchical systems seem to have the property that something considered as an undivided entity on one level, is considered as a composite object on the next lower level of greater detail"
>
> **日本語訳**: 「階層システムは、あるレベルで分割不可能な実体と見なされるものが、より詳細な次の下位レベルでは複合オブジェクトと見なされるという性質を持つようだ」

#### 現代への示唆

ダイクストラの「新しい意味レベル（semantic level）の創造」という主張は、DDDにおける**ユビキタス言語**と**ドメインモデル**の概念を先取りしている。エリック・エヴァンスが2003年に体系化したDDDは、業務ドメインの専門家と開発者が共通の語彙（言語）を構築し、その言語でモデルを表現することを核心とする。

ダイクストラが述べた「言語や記法が思考を決定する」という洞察は、DDDの根本原理と完全に一致する。適切なドメイン言語を持たなければ、ドメインの本質を正確に捉えることができない。逆に、ドメインに適した言語を構築すれば、そこで「絶対的に正確」な表現が可能になる。

また、「うまく分解された解決策」「インターフェースの適切な設計」という主張は、DDDにおける**境界づけられたコンテキスト（Bounded Context）**の概念と響き合う。複雑なドメインを適切な境界で分割し、各コンテキスト内では一貫したモデルを維持する——この設計原則は、ダイクストラの階層的分解の思想の延長線上にある。

---

### パラダイム対応表（サマリ）

| パラダイム | ダイクストラの主張 | 講演での核心的引用 |
|-----------|-------------------|-------------------|
| **モデリング** | 抽象化は新しい精密さの創造 | "the purpose of abstracting is not to be vague, but to create a new semantic level in which one can be absolutely precise" |
| **デザインパターン** | 抽象化パターンの共有で効率劇的向上 | "these patterns of abstraction... could have taken a few minutes instead of a few years" |
| **TDD** | 証明とプログラムの同時成長 | "the programmer should let correctness proof and program grow hand in hand" |
| **アジャイル** | 正当性への関心が発見的指針 | "correctness concerns turn out to be a very effective heuristic guidance" |
| **DDD** | 言語が思考を決定し、意味レベルを創造 | "the language or notation we are using to express or record our thoughts, are the major factors determining what we can think or express at all" |

---

### 結論：1972年の予見

ダイクストラは1972年の時点で、後の数十年間に発展する主要なプログラミングパラダイムの本質を見抜いていた。彼の主張を現代的に再解釈すれば：

1. **モデリング**: 適切な抽象化レベルで問題を捉えれば、知的労力は激減する
2. **デザインパターン**: 抽象化パターンを共通知識として普及させれば、開発効率は劇的に向上する
3. **TDD**: テスト（証明）を先行させ、プログラムと同時に成長させるべき
4. **アジャイル**: テストを書く過程で問題の構造を発見し、仕様として定義していく
5. **DDD**: 適切な言語（ユビキタス言語）を構築すれば、ドメインを正確に捉え、適切な境界で分解できる

これらすべてを貫くのが「**謙虚さ（Humility）**」である。人間の知的能力の限界を認識し、それに適した方法論とツールを選ぶ——この姿勢こそが、50年以上経った現在でも色褪せない、ダイクストラの最も重要なメッセージである。

---

## 参考情報

- **原典**: EWD340
- **発表**: 1972年 ACM チューリング賞講演
- **出版**: Communications of the ACM, Vol. 15, No. 10, October 1972
- **アーカイブ**: https://www.cs.utexas.edu/~EWD/
