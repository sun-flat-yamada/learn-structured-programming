# ソフトウエア設計パラダイムの歴史

| **年** | **マイルストーン (カテゴリー)** | **詳細** |
| ----- | ------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| 1968年 | ソフトウェアの危機 (**重要な出来事**) | 西ドイツ・ガルミッシュで開催されたNATOソフトウェア工学会議にて、開発プロジェクトの失敗続出状態が「**ソフトウェア危機**」と命名されました [\[en.wikipedia.org/wiki/Software_crisis\]](https://en.wikipedia.org/wiki/Software_crisis)。この出来事を契機に、計画的手法によるソフトウェア開発管理（ソフトウェア工学）の必要性が強く認識されました。 |
| 1968年 | 構造化プログラミング (**技術革新**) | オランダの計算機科学者エドガー・ダイクストラが1968年に公開した手紙「Go To文に関する考察」により、無秩序なジャンプが生む**スパゲッティコード**を痛烈に批判、**構造化プログラミング**という概念を広めました [\[en.wikipedia.org/wiki/Structured_programming\]](https://en.wikipedia.org/wiki/Structured_programming)。制御フローをブロック化し、`goto`を使わない構造化手法は、コードの可読性・保守性を飛躍的に高めるものとして受け入れられました。 |
| 1970年 | ウォーターフォールモデル (**標準化**) | 米国の技術者ウィンストン・ロイスが1970年の論文「大規模ソフトウェアシステム開発の管理」において、要件定義から運用まで段階的に進める開発プロセスモデルを提案しました [\[en.wikipedia.org/wiki/Winston_W._Royce\]](https://en.wikipedia.org/wiki/Winston_W._Royce), [\[en.wikipedia.org\]](https://en.wikipedia.org/wiki/Winston_W._Royce)。後に**ウォーターフォールモデル**と呼ばれたこの手法は、ソフトウェア開発における標準的手順として広く認知され、以降の手法の基礎となりました。 |
| 1972年 | オブジェクト指向プログラミング (**技術革新**) | 1972年、ゼロックスPARCでAlan Kayらが開発した **Smalltalk** 言語が公開され、オブジェクト間のメッセージ交換による**オブジェクト指向プログラミング**が実践されました [\[en.wikipedia.org\]](https://en.wikipedia.org/wiki/Smalltalk), [\[en.wikipedia.org/wiki/Smalltalk\]](https://en.wikipedia.org/wiki/Smalltalk)。Smalltalk（および先行するSimula言語）の成功により、「オブジェクト」という概念にもとづくプログラミングパラダイムが確立し、後の多くのプログラミング言語（JavaやC++など）に影響を与えました。 |
| 1986年 | スパイラルモデル (**技術革新**) | 1986年、米国のバリー・ボームがリスク駆動型の開発プロセスである**スパイラルモデル**を提唱しました [\[en.wikipedia.org/wiki/Spiral_model\]](https://en.wikipedia.org/wiki/Spiral_model)。ウォーターフォール型の各工程を繰り返し実行する**反復型開発**にリスク分析を組み合わせたこの手法では、計画→リスク評価→プロトタイプ実装→評価のサイクルを何度も回すことで、不確実性を制御しながら漸進的にシステムを完成させていきます。 |
| 1994年 | デザインパターン集 (**標準化**) | 1994年、エリック・ガンマら**4人組**（GoF）によりソフトウェア設計書『**Design Patterns: Elements of Reusable Object-Oriented Software**』が出版され、23の汎用的なオブジェクト指向デザインパターンが整理・命名されました [\[en.wikipedia.org/wiki/Design_Patterns\]](https://en.wikipedia.org/wiki/Design_Patterns)。これにより再利用可能な設計ソリューションのカタログと共通用語が開発者間で共有され、設計ノウハウの**標準化**が進みました。 |
| 1997年 | 単体テストフレームワーク JUnit (**技術革新**) | 1997年ごろ、Kent BeckとErich GammaがJava向けの単体テストフレームワーク**JUnit**を開発・公開しました [\[en.wikipedia.org/wiki/Kent_Beck\]](https://en.wikipedia.org/wiki/Kent_Beck)。SmalltalkのSUnitを元にしたJUnitの登場により、プログラマはコードに対する自動化テストを容易に書けるようになり、ソフトウェア開発において**単体テストとテスト駆動開発**（TDD）の実践が急速に普及しました。 |
| 1999年 | リファクタリング手法 (**標準化**) | 1999年、マーティン・ファウラーが著書『**リファクタリング**』を発表し、既存コードの設計改善手法を体系化しました [\[en.wikipedia.org/wiki/Martin_Fowler_%28software_engineer%29\]](https://en.wikipedia.org/wiki/Martin_Fowler_%28software_engineer%29)。動作を変えずにコードの内部構造を改善する具体的なリファクタリング手順とカタログが提示され、これらはコード品質を向上させる標準的プラクティスとして定着しました。 |
| 1999年 | エクストリームプログラミング (XP) (**技術革新**) | 1999年、Kent Beckの著書『**エクストリームプログラミング入門** (Extreme Programming Explained)』の出版によって**エクストリーム・プログラミング** (XP) が広く知られるようになりました [\[en.wikipedia.org/wiki/Kent_Beck\]](https://en.wikipedia.org/wiki/Kent_Beck)。XPは短い開発イテレーションと継続的な顧客フィードバックを重視し、ペアプログラミングや継続的インテグレーション、自動テストなど革新的プラクティスを組み合わせたアジャイル手法として注目されました。 |
| 2001年 | アジャイル宣言 (**重要な出来事**) | 2001年2月、ソフトウェア開発の有識者17名が米国ユタ州スノーバードで集まり\*\*「アジャイルソフトウェア開発宣言」\*\* (Agile Manifesto) に合意しました [\[en.wikipedia.org/wiki/Kent_Beck\]](https://en.wikipedia.org/wiki/Kent_Beck)。この**アジャイル宣言**は「 individuals and interactions over processes and tools（プロセスやツールより**個人と対話**を重視）」等の原則を掲げ、従来の重量級開発プロセスに代わる軽量で適応性の高い開発アプローチの指針となりました。 |
| 2002年 | テスト駆動開発 (TDD) **(技術革新)** | 2002年、Kent Beckが著書『**テスト駆動開発** (Test-Driven Development by Example)』でテストファーストの開発手法を体系化しました [\[en.wikipedia.org/wiki/Kent_Beck\]](https://en.wikipedia.org/wiki/Kent_Beck)。**テスト駆動開発 (TDD)** では、まず失敗するテストを書き、それを通すコードを実装するサイクルを繰り返します。この手法によりコードの品質と設計の改善が継続的に促進されるようになりました。 |
| 2003年 | ドメイン駆動設計 (DDD) **(技術革新)** | 2003年、エリック・エバンスが著書『**ドメイン駆動設計**』(Domain-Driven Design) を出版し [\[fabiofumar....github.io\]](https://fabiofumarola.github.io/nosql/readingMaterial/Evans03.pdf)、ソフトウェア設計において**ドメインモデル**を重視するアプローチを提唱しました。DDDでは、開発者とドメイン専門家が共有する**ユビキタス言語**を用いてビジネス領域の知識をモデル化し、複雑な要件に対応することを目指します。この手法は大規模システムの開発で設計とビジネスの橋渡しとして広く採用されました。 |
| 2005年 | ヘキサゴナルアーキテクチャ (**技術革新**) | 2005年、アリスター・コーバーンが**ヘキサゴナルアーキテクチャ**（別名：ポートとアダプタ構造）を提唱しました [\[alistair.cockburn.us\]](https://alistair.cockburn.us/hexagonal-architecture)。UIやデータベースなど外部システムとの入出力を**ポート**として抽象化し、対応する**アダプタ**経由でやり取りすることで、アプリケーションの中核ビジネスロジックを外部要素から独立させます。このアーキテクチャパターンによりテスト容易性が向上し、UIやDBを差し替えても安定して動作する柔軟なシステム設計が可能になりました。 |
| 2009年 | DevOps の台頭 (**重要な出来事**) | 2009年、開発(Dev)と運用(Ops)の連携を強化する文化として**DevOps**が誕生しました。ベルギーで開催された最初の**DevOps Days** (2009年) を皮切りに [\[en.wikipedia.org/wiki/DevOps\]](https://en.wikipedia.org/wiki/DevOps)、開発プロセスの自動化や継続的デリバリーを通じて開発・運用チームの協働を促進するプラクティスが急速に広まりました。DevOpsは従来分業されていた開発と運用の壁を崩し、ソフトウェアのリリースサイクルを大幅に短縮するムーブメントとなりました。 |
| 2011年 | マイクロサービスアーキテクチャ (**技術革新**) | 2011年前後から、大規模アプリケーションを細かなサービスに分割する**マイクロサービスアーキテクチャ**が注目を集め始めました [\[leanix.net\]](https://www.leanix.net/en/blog/a-brief-history-of-microservices)。NetflixやAmazonなどが先駆者となり、アプリケーションを独立してデプロイ可能な小さなサービス群に機能分割することで、サービスごとにスケールやデプロイを行いやすくし、開発チームの並行開発も容易にする手法です。このアプローチはクラウドの普及と相まって2010年代に急速に広まりました。 |
| 2012年 | クリーンアーキテクチャ (**標準化**) | 2012年、Robert C. Martin (通称Uncle Bob) がブログ記事「The Clean Architecture」で複数のアーキテクチャ原則を統合した**クリーンアーキテクチャ**を提唱しました [\[blog.cleancoder.com\]](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)。システムを層状に分割し、内側のビジネスルールが外側のUIやデータベースに依存しないよう**依存関係の方向性を制御する**（「**依存性のルール**」）ことを強調する構造です [\[blog.cleancoder.com\]](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)。この原則により、高い保守性・テスト容易性を備えたアーキテクチャ設計が体系化され、2017年には書籍『Clean Architecture』としても刊行されました。 |
| 2015年 | イベント駆動アーキテクチャ / CQRS (**技術革新**) | 2010年代を通じて、**イベント駆動アーキテクチャ**と**CQRS (Command Query Responsibility Segregation)** パターンが広く普及しました [\[en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation\]](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation)。Greg Youngらが提唱したCQRSは、読み取りと書き込みのモデルを分離し、**イベントソーシング**と組み合わせることで、複雑なドメインにおけるスケーラビリティと監査可能性を向上させます。DDDとの親和性が高く、分散システム設計の重要なパターンとして定着しました。 |
| 2019年 | モジュラーモノリス (**技術革新**) | マイクロサービスの複雑さへの反省から、**モジュラーモノリス**というアーキテクチャパターンが再評価されています [\[en.wikipedia.org/wiki/Monolithic_application\]](https://en.wikipedia.org/wiki/Monolithic_application)。単一のデプロイ単位を維持しながら、内部を明確な境界を持つモジュールに分割することで、モノリスの単純さとマイクロサービスの疎結合性を両立させます。DDDの境界づけられたコンテキスト (Bounded Context) の概念を活用し、将来的な分割への移行パスも確保できる設計アプローチです。 |
| 2023年 | AI駆動設計 / Vibe Coding (**技術革新**) | 2023年以降、大規模言語モデル (LLM) の急速な進化により、**AI駆動設計**という新たなパラダイムが台頭しています。自然言語による要件記述からコードを生成し、設計意図を対話的に洗練させる開発スタイルは、従来の分析・設計プロセスを根本から変革しつつあります。2025年にはAndrej Karpathyが提唱した**Vibe Coding**（AIとの対話を通じて直感的にソフトウェアを構築する手法）が注目を集め、ソフトウェア開発における人間とAIの協働のあり方が問われています [\[x.com/karpathy\]](https://x.com/karpathy/status/1886192184808149383)。 |

---

## 参考文献

### 一次資料（原著論文・書籍・公式発表）

| 年 | 資料名 | 著者 | URL |
| --- | --- | --- | --- |
| 1968 | *Go To Statement Considered Harmful* | Edsger W. Dijkstra | [dl.acm.org](https://dl.acm.org/doi/10.1145/362929.362947) |
| 1968 | NATO Software Engineering Conference Report | Peter Naur, Brian Randell (Eds.) | [homepages.cs.ncl.ac.uk](http://homepages.cs.ncl.ac.uk/brian.randell/NATO/nato1968.PDF) |
| 1970 | *Managing the Development of Large Software Systems* | Winston W. Royce | [leadinganswers.typepad.com](https://leadinganswers.typepad.com/leading_answers/files/original_waterfall_paper_winston_royce.pdf) |
| 1986 | *A Spiral Model of Software Development and Enhancement* | Barry W. Boehm | [csse.usc.edu](https://csse.usc.edu/TECHRPTS/1988/usccse88-500/usccse88-500.pdf) |
| 1994 | *Design Patterns: Elements of Reusable Object-Oriented Software* | Gamma, Helm, Johnson, Vlissides (GoF) | ISBN: 978-0201633610 |
| 1999 | *Refactoring: Improving the Design of Existing Code* | Martin Fowler | ISBN: 978-0201485677 |
| 1999 | *Extreme Programming Explained* | Kent Beck | ISBN: 978-0201616415 |
| 2001 | Manifesto for Agile Software Development | Beck, Fowler, Martin et al. | [agilemanifesto.org](https://agilemanifesto.org/) |
| 2002 | *Test-Driven Development: By Example* | Kent Beck | ISBN: 978-0321146533 |
| 2003 | *Domain-Driven Design: Tackling Complexity in the Heart of Software* | Eric Evans | ISBN: 978-0321125217 |
| 2005 | *Hexagonal Architecture (Ports and Adapters)* | Alistair Cockburn | [alistair.cockburn.us](https://alistair.cockburn.us/hexagonal-architecture/) |
| 2010 | *CQRS Documents* | Greg Young | [cqrs.files.wordpress.com](https://cqrs.files.wordpress.com/2010/11/cqrs_documents.pdf) |
| 2012 | *The Clean Architecture* (blog post) | Robert C. Martin | [blog.cleancoder.com](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) |
| 2017 | *Clean Architecture: A Craftsman's Guide to Software Structure and Design* | Robert C. Martin | ISBN: 978-0134494166 |
| 2025 | Vibe Coding (concept introduction) | Andrej Karpathy | [x.com/karpathy](https://x.com/karpathy/status/1886192184808149383) |

### 二次資料（Wikipedia・解説記事）

| トピック | URL |
| --- | --- |
| Software crisis | [en.wikipedia.org/wiki/Software_crisis](https://en.wikipedia.org/wiki/Software_crisis) |
| Structured programming | [en.wikipedia.org/wiki/Structured_programming](https://en.wikipedia.org/wiki/Structured_programming) |
| Waterfall model | [en.wikipedia.org/wiki/Waterfall_model](https://en.wikipedia.org/wiki/Waterfall_model) |
| Winston W. Royce | [en.wikipedia.org/wiki/Winston_W._Royce](https://en.wikipedia.org/wiki/Winston_W._Royce) |
| Smalltalk | [en.wikipedia.org/wiki/Smalltalk](https://en.wikipedia.org/wiki/Smalltalk) |
| Spiral model | [en.wikipedia.org/wiki/Spiral_model](https://en.wikipedia.org/wiki/Spiral_model) |
| Design Patterns (book) | [en.wikipedia.org/wiki/Design_Patterns](https://en.wikipedia.org/wiki/Design_Patterns) |
| Kent Beck | [en.wikipedia.org/wiki/Kent_Beck](https://en.wikipedia.org/wiki/Kent_Beck) |
| Martin Fowler | [en.wikipedia.org/wiki/Martin_Fowler_(software_engineer)](https://en.wikipedia.org/wiki/Martin_Fowler_(software_engineer)) |
| Test-driven development | [en.wikipedia.org/wiki/Test-driven_development](https://en.wikipedia.org/wiki/Test-driven_development) |
| Domain-driven design | [en.wikipedia.org/wiki/Domain-driven_design](https://en.wikipedia.org/wiki/Domain-driven_design) |
| CQRS | [en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation) |
| DevOps | [en.wikipedia.org/wiki/DevOps](https://en.wikipedia.org/wiki/DevOps) |
| Microservices | [en.wikipedia.org/wiki/Microservices](https://en.wikipedia.org/wiki/Microservices) |
| History of Microservices | [leanix.net](https://www.leanix.net/en/blog/a-brief-history-of-microservices) |
| Monolithic application | [en.wikipedia.org/wiki/Monolithic_application](https://en.wikipedia.org/wiki/Monolithic_application) |
