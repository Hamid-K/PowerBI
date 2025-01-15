using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build
{
	// Token: 0x020014F8 RID: 5368
	public class GrammarBuilders
	{
		// Token: 0x0600A4CD RID: 42189 RVA: 0x00254477 File Offset: 0x00252677
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x0600A4CE RID: 42190 RVA: 0x002544A3 File Offset: 0x002526A3
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x0600A4CF RID: 42191 RVA: 0x002544B0 File Offset: 0x002526B0
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x0600A4D0 RID: 42192 RVA: 0x002544BD File Offset: 0x002526BD
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x0600A4D1 RID: 42193 RVA: 0x002544CA File Offset: 0x002526CA
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x0600A4D2 RID: 42194 RVA: 0x002544D7 File Offset: 0x002526D7
		// (set) Token: 0x0600A4D3 RID: 42195 RVA: 0x002544DF File Offset: 0x002526DF
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x0600A4D4 RID: 42196 RVA: 0x002544E8 File Offset: 0x002526E8
		// (set) Token: 0x0600A4D5 RID: 42197 RVA: 0x002544F0 File Offset: 0x002526F0
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600A4D6 RID: 42198 RVA: 0x002544FC File Offset: 0x002526FC
		public GrammarBuilders(Grammar grammar)
		{
			GrammarBuilders <>4__this = this;
			this._symbol = new Lazy<GrammarBuilders.GrammarSymbols>(() => new GrammarBuilders.GrammarSymbols(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._rule = new Lazy<GrammarBuilders.GrammarRules>(() => new GrammarBuilders.GrammarRules(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._unnamedConversion = new Lazy<GrammarBuilders.GrammarUnnamedConversions>(() => new GrammarBuilders.GrammarUnnamedConversions(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._hole = new Lazy<GrammarBuilders.GrammarHoles>(() => new GrammarBuilders.GrammarHoles(<>4__this), LazyThreadSafetyMode.ExecutionAndPublication);
			this.Node = new GrammarBuilders.Nodes(this);
			this.Set = new GrammarBuilders.Sets(this);
		}

		// Token: 0x0400432B RID: 17195
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x0400432C RID: 17196
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x0400432D RID: 17197
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x0400432E RID: 17198
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x0400432F RID: 17199
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x020014F9 RID: 5369
		public class GrammarSymbols
		{
			// Token: 0x17001CBE RID: 7358
			// (get) Token: 0x0600A4D8 RID: 42200 RVA: 0x002545A7 File Offset: 0x002527A7
			// (set) Token: 0x0600A4D9 RID: 42201 RVA: 0x002545AF File Offset: 0x002527AF
			public Symbol row { get; private set; }

			// Token: 0x17001CBF RID: 7359
			// (get) Token: 0x0600A4DA RID: 42202 RVA: 0x002545B8 File Offset: 0x002527B8
			// (set) Token: 0x0600A4DB RID: 42203 RVA: 0x002545C0 File Offset: 0x002527C0
			public Symbol result { get; private set; }

			// Token: 0x17001CC0 RID: 7360
			// (get) Token: 0x0600A4DC RID: 42204 RVA: 0x002545C9 File Offset: 0x002527C9
			// (set) Token: 0x0600A4DD RID: 42205 RVA: 0x002545D1 File Offset: 0x002527D1
			public Symbol output { get; private set; }

			// Token: 0x17001CC1 RID: 7361
			// (get) Token: 0x0600A4DE RID: 42206 RVA: 0x002545DA File Offset: 0x002527DA
			// (set) Token: 0x0600A4DF RID: 42207 RVA: 0x002545E2 File Offset: 0x002527E2
			public Symbol outNumber { get; private set; }

			// Token: 0x17001CC2 RID: 7362
			// (get) Token: 0x0600A4E0 RID: 42208 RVA: 0x002545EB File Offset: 0x002527EB
			// (set) Token: 0x0600A4E1 RID: 42209 RVA: 0x002545F3 File Offset: 0x002527F3
			public Symbol outDate { get; private set; }

			// Token: 0x17001CC3 RID: 7363
			// (get) Token: 0x0600A4E2 RID: 42210 RVA: 0x002545FC File Offset: 0x002527FC
			// (set) Token: 0x0600A4E3 RID: 42211 RVA: 0x00254604 File Offset: 0x00252804
			public Symbol outStr { get; private set; }

			// Token: 0x17001CC4 RID: 7364
			// (get) Token: 0x0600A4E4 RID: 42212 RVA: 0x0025460D File Offset: 0x0025280D
			// (set) Token: 0x0600A4E5 RID: 42213 RVA: 0x00254615 File Offset: 0x00252815
			public Symbol outStr1 { get; private set; }

			// Token: 0x17001CC5 RID: 7365
			// (get) Token: 0x0600A4E6 RID: 42214 RVA: 0x0025461E File Offset: 0x0025281E
			// (set) Token: 0x0600A4E7 RID: 42215 RVA: 0x00254626 File Offset: 0x00252826
			public Symbol segmentCase { get; private set; }

			// Token: 0x17001CC6 RID: 7366
			// (get) Token: 0x0600A4E8 RID: 42216 RVA: 0x0025462F File Offset: 0x0025282F
			// (set) Token: 0x0600A4E9 RID: 42217 RVA: 0x00254637 File Offset: 0x00252837
			public Symbol segment { get; private set; }

			// Token: 0x17001CC7 RID: 7367
			// (get) Token: 0x0600A4EA RID: 42218 RVA: 0x00254640 File Offset: 0x00252840
			// (set) Token: 0x0600A4EB RID: 42219 RVA: 0x00254648 File Offset: 0x00252848
			public Symbol formatted { get; private set; }

			// Token: 0x17001CC8 RID: 7368
			// (get) Token: 0x0600A4EC RID: 42220 RVA: 0x00254651 File Offset: 0x00252851
			// (set) Token: 0x0600A4ED RID: 42221 RVA: 0x00254659 File Offset: 0x00252859
			public Symbol concatEntry { get; private set; }

			// Token: 0x17001CC9 RID: 7369
			// (get) Token: 0x0600A4EE RID: 42222 RVA: 0x00254662 File Offset: 0x00252862
			// (set) Token: 0x0600A4EF RID: 42223 RVA: 0x0025466A File Offset: 0x0025286A
			public Symbol concatCase { get; private set; }

			// Token: 0x17001CCA RID: 7370
			// (get) Token: 0x0600A4F0 RID: 42224 RVA: 0x00254673 File Offset: 0x00252873
			// (set) Token: 0x0600A4F1 RID: 42225 RVA: 0x0025467B File Offset: 0x0025287B
			public Symbol concat { get; private set; }

			// Token: 0x17001CCB RID: 7371
			// (get) Token: 0x0600A4F2 RID: 42226 RVA: 0x00254684 File Offset: 0x00252884
			// (set) Token: 0x0600A4F3 RID: 42227 RVA: 0x0025468C File Offset: 0x0025288C
			public Symbol concatPrefix { get; private set; }

			// Token: 0x17001CCC RID: 7372
			// (get) Token: 0x0600A4F4 RID: 42228 RVA: 0x00254695 File Offset: 0x00252895
			// (set) Token: 0x0600A4F5 RID: 42229 RVA: 0x0025469D File Offset: 0x0025289D
			public Symbol concatSegment { get; private set; }

			// Token: 0x17001CCD RID: 7373
			// (get) Token: 0x0600A4F6 RID: 42230 RVA: 0x002546A6 File Offset: 0x002528A6
			// (set) Token: 0x0600A4F7 RID: 42231 RVA: 0x002546AE File Offset: 0x002528AE
			public Symbol concatSuffix { get; private set; }

			// Token: 0x17001CCE RID: 7374
			// (get) Token: 0x0600A4F8 RID: 42232 RVA: 0x002546B7 File Offset: 0x002528B7
			// (set) Token: 0x0600A4F9 RID: 42233 RVA: 0x002546BF File Offset: 0x002528BF
			public Symbol condition { get; private set; }

			// Token: 0x17001CCF RID: 7375
			// (get) Token: 0x0600A4FA RID: 42234 RVA: 0x002546C8 File Offset: 0x002528C8
			// (set) Token: 0x0600A4FB RID: 42235 RVA: 0x002546D0 File Offset: 0x002528D0
			public Symbol or { get; private set; }

			// Token: 0x17001CD0 RID: 7376
			// (get) Token: 0x0600A4FC RID: 42236 RVA: 0x002546D9 File Offset: 0x002528D9
			// (set) Token: 0x0600A4FD RID: 42237 RVA: 0x002546E1 File Offset: 0x002528E1
			public Symbol inull { get; private set; }

			// Token: 0x17001CD1 RID: 7377
			// (get) Token: 0x0600A4FE RID: 42238 RVA: 0x002546EA File Offset: 0x002528EA
			// (set) Token: 0x0600A4FF RID: 42239 RVA: 0x002546F2 File Offset: 0x002528F2
			public Symbol equalsText { get; private set; }

			// Token: 0x17001CD2 RID: 7378
			// (get) Token: 0x0600A500 RID: 42240 RVA: 0x002546FB File Offset: 0x002528FB
			// (set) Token: 0x0600A501 RID: 42241 RVA: 0x00254703 File Offset: 0x00252903
			public Symbol containsFindText { get; private set; }

			// Token: 0x17001CD3 RID: 7379
			// (get) Token: 0x0600A502 RID: 42242 RVA: 0x0025470C File Offset: 0x0025290C
			// (set) Token: 0x0600A503 RID: 42243 RVA: 0x00254714 File Offset: 0x00252914
			public Symbol startsWithFindText { get; private set; }

			// Token: 0x17001CD4 RID: 7380
			// (get) Token: 0x0600A504 RID: 42244 RVA: 0x0025471D File Offset: 0x0025291D
			// (set) Token: 0x0600A505 RID: 42245 RVA: 0x00254725 File Offset: 0x00252925
			public Symbol isMatchRegex { get; private set; }

			// Token: 0x17001CD5 RID: 7381
			// (get) Token: 0x0600A506 RID: 42246 RVA: 0x0025472E File Offset: 0x0025292E
			// (set) Token: 0x0600A507 RID: 42247 RVA: 0x00254736 File Offset: 0x00252936
			public Symbol containsMatchRegex { get; private set; }

			// Token: 0x17001CD6 RID: 7382
			// (get) Token: 0x0600A508 RID: 42248 RVA: 0x0025473F File Offset: 0x0025293F
			// (set) Token: 0x0600A509 RID: 42249 RVA: 0x00254747 File Offset: 0x00252947
			public Symbol containsCount { get; private set; }

			// Token: 0x17001CD7 RID: 7383
			// (get) Token: 0x0600A50A RID: 42250 RVA: 0x00254750 File Offset: 0x00252950
			// (set) Token: 0x0600A50B RID: 42251 RVA: 0x00254758 File Offset: 0x00252958
			public Symbol matchCount { get; private set; }

			// Token: 0x17001CD8 RID: 7384
			// (get) Token: 0x0600A50C RID: 42252 RVA: 0x00254761 File Offset: 0x00252961
			// (set) Token: 0x0600A50D RID: 42253 RVA: 0x00254769 File Offset: 0x00252969
			public Symbol numberEqualsValue { get; private set; }

			// Token: 0x17001CD9 RID: 7385
			// (get) Token: 0x0600A50E RID: 42254 RVA: 0x00254772 File Offset: 0x00252972
			// (set) Token: 0x0600A50F RID: 42255 RVA: 0x0025477A File Offset: 0x0025297A
			public Symbol numberGreaterThanValue { get; private set; }

			// Token: 0x17001CDA RID: 7386
			// (get) Token: 0x0600A510 RID: 42256 RVA: 0x00254783 File Offset: 0x00252983
			// (set) Token: 0x0600A511 RID: 42257 RVA: 0x0025478B File Offset: 0x0025298B
			public Symbol numberLessThanValue { get; private set; }

			// Token: 0x17001CDB RID: 7387
			// (get) Token: 0x0600A512 RID: 42258 RVA: 0x00254794 File Offset: 0x00252994
			// (set) Token: 0x0600A513 RID: 42259 RVA: 0x0025479C File Offset: 0x0025299C
			public Symbol formatNumber { get; private set; }

			// Token: 0x17001CDC RID: 7388
			// (get) Token: 0x0600A514 RID: 42260 RVA: 0x002547A5 File Offset: 0x002529A5
			// (set) Token: 0x0600A515 RID: 42261 RVA: 0x002547AD File Offset: 0x002529AD
			public Symbol number { get; private set; }

			// Token: 0x17001CDD RID: 7389
			// (get) Token: 0x0600A516 RID: 42262 RVA: 0x002547B6 File Offset: 0x002529B6
			// (set) Token: 0x0600A517 RID: 42263 RVA: 0x002547BE File Offset: 0x002529BE
			public Symbol number1 { get; private set; }

			// Token: 0x17001CDE RID: 7390
			// (get) Token: 0x0600A518 RID: 42264 RVA: 0x002547C7 File Offset: 0x002529C7
			// (set) Token: 0x0600A519 RID: 42265 RVA: 0x002547CF File Offset: 0x002529CF
			public Symbol arithmetic { get; private set; }

			// Token: 0x17001CDF RID: 7391
			// (get) Token: 0x0600A51A RID: 42266 RVA: 0x002547D8 File Offset: 0x002529D8
			// (set) Token: 0x0600A51B RID: 42267 RVA: 0x002547E0 File Offset: 0x002529E0
			public Symbol arithmeticLeft { get; private set; }

			// Token: 0x17001CE0 RID: 7392
			// (get) Token: 0x0600A51C RID: 42268 RVA: 0x002547E9 File Offset: 0x002529E9
			// (set) Token: 0x0600A51D RID: 42269 RVA: 0x002547F1 File Offset: 0x002529F1
			public Symbol addRight { get; private set; }

			// Token: 0x17001CE1 RID: 7393
			// (get) Token: 0x0600A51E RID: 42270 RVA: 0x002547FA File Offset: 0x002529FA
			// (set) Token: 0x0600A51F RID: 42271 RVA: 0x00254802 File Offset: 0x00252A02
			public Symbol subtractRight { get; private set; }

			// Token: 0x17001CE2 RID: 7394
			// (get) Token: 0x0600A520 RID: 42272 RVA: 0x0025480B File Offset: 0x00252A0B
			// (set) Token: 0x0600A521 RID: 42273 RVA: 0x00254813 File Offset: 0x00252A13
			public Symbol multiplyRight { get; private set; }

			// Token: 0x17001CE3 RID: 7395
			// (get) Token: 0x0600A522 RID: 42274 RVA: 0x0025481C File Offset: 0x00252A1C
			// (set) Token: 0x0600A523 RID: 42275 RVA: 0x00254824 File Offset: 0x00252A24
			public Symbol divideRight { get; private set; }

			// Token: 0x17001CE4 RID: 7396
			// (get) Token: 0x0600A524 RID: 42276 RVA: 0x0025482D File Offset: 0x00252A2D
			// (set) Token: 0x0600A525 RID: 42277 RVA: 0x00254835 File Offset: 0x00252A35
			public Symbol inumber { get; private set; }

			// Token: 0x17001CE5 RID: 7397
			// (get) Token: 0x0600A526 RID: 42278 RVA: 0x0025483E File Offset: 0x00252A3E
			// (set) Token: 0x0600A527 RID: 42279 RVA: 0x00254846 File Offset: 0x00252A46
			public Symbol rowNumberTransform { get; private set; }

			// Token: 0x17001CE6 RID: 7398
			// (get) Token: 0x0600A528 RID: 42280 RVA: 0x0025484F File Offset: 0x00252A4F
			// (set) Token: 0x0600A529 RID: 42281 RVA: 0x00254857 File Offset: 0x00252A57
			public Symbol formatDateTime { get; private set; }

			// Token: 0x17001CE7 RID: 7399
			// (get) Token: 0x0600A52A RID: 42282 RVA: 0x00254860 File Offset: 0x00252A60
			// (set) Token: 0x0600A52B RID: 42283 RVA: 0x00254868 File Offset: 0x00252A68
			public Symbol date { get; private set; }

			// Token: 0x17001CE8 RID: 7400
			// (get) Token: 0x0600A52C RID: 42284 RVA: 0x00254871 File Offset: 0x00252A71
			// (set) Token: 0x0600A52D RID: 42285 RVA: 0x00254879 File Offset: 0x00252A79
			public Symbol idate { get; private set; }

			// Token: 0x17001CE9 RID: 7401
			// (get) Token: 0x0600A52E RID: 42286 RVA: 0x00254882 File Offset: 0x00252A82
			// (set) Token: 0x0600A52F RID: 42287 RVA: 0x0025488A File Offset: 0x00252A8A
			public Symbol itime { get; private set; }

			// Token: 0x17001CEA RID: 7402
			// (get) Token: 0x0600A530 RID: 42288 RVA: 0x00254893 File Offset: 0x00252A93
			// (set) Token: 0x0600A531 RID: 42289 RVA: 0x0025489B File Offset: 0x00252A9B
			public Symbol parseSubject { get; private set; }

			// Token: 0x17001CEB RID: 7403
			// (get) Token: 0x0600A532 RID: 42290 RVA: 0x002548A4 File Offset: 0x00252AA4
			// (set) Token: 0x0600A533 RID: 42291 RVA: 0x002548AC File Offset: 0x00252AAC
			public Symbol letSubstring { get; private set; }

			// Token: 0x17001CEC RID: 7404
			// (get) Token: 0x0600A534 RID: 42292 RVA: 0x002548B5 File Offset: 0x00252AB5
			// (set) Token: 0x0600A535 RID: 42293 RVA: 0x002548BD File Offset: 0x00252ABD
			public Symbol x { get; private set; }

			// Token: 0x17001CED RID: 7405
			// (get) Token: 0x0600A536 RID: 42294 RVA: 0x002548C6 File Offset: 0x00252AC6
			// (set) Token: 0x0600A537 RID: 42295 RVA: 0x002548CE File Offset: 0x00252ACE
			public Symbol substring { get; private set; }

			// Token: 0x17001CEE RID: 7406
			// (get) Token: 0x0600A538 RID: 42296 RVA: 0x002548D7 File Offset: 0x00252AD7
			// (set) Token: 0x0600A539 RID: 42297 RVA: 0x002548DF File Offset: 0x00252ADF
			public Symbol splitTrim { get; private set; }

			// Token: 0x17001CEF RID: 7407
			// (get) Token: 0x0600A53A RID: 42298 RVA: 0x002548E8 File Offset: 0x00252AE8
			// (set) Token: 0x0600A53B RID: 42299 RVA: 0x002548F0 File Offset: 0x00252AF0
			public Symbol split { get; private set; }

			// Token: 0x17001CF0 RID: 7408
			// (get) Token: 0x0600A53C RID: 42300 RVA: 0x002548F9 File Offset: 0x00252AF9
			// (set) Token: 0x0600A53D RID: 42301 RVA: 0x00254901 File Offset: 0x00252B01
			public Symbol sliceTrim { get; private set; }

			// Token: 0x17001CF1 RID: 7409
			// (get) Token: 0x0600A53E RID: 42302 RVA: 0x0025490A File Offset: 0x00252B0A
			// (set) Token: 0x0600A53F RID: 42303 RVA: 0x00254912 File Offset: 0x00252B12
			public Symbol slice { get; private set; }

			// Token: 0x17001CF2 RID: 7410
			// (get) Token: 0x0600A540 RID: 42304 RVA: 0x0025491B File Offset: 0x00252B1B
			// (set) Token: 0x0600A541 RID: 42305 RVA: 0x00254923 File Offset: 0x00252B23
			public Symbol pos { get; private set; }

			// Token: 0x17001CF3 RID: 7411
			// (get) Token: 0x0600A542 RID: 42306 RVA: 0x0025492C File Offset: 0x00252B2C
			// (set) Token: 0x0600A543 RID: 42307 RVA: 0x00254934 File Offset: 0x00252B34
			public Symbol fromStrTrim { get; private set; }

			// Token: 0x17001CF4 RID: 7412
			// (get) Token: 0x0600A544 RID: 42308 RVA: 0x0025493D File Offset: 0x00252B3D
			// (set) Token: 0x0600A545 RID: 42309 RVA: 0x00254945 File Offset: 0x00252B45
			public Symbol fromStr { get; private set; }

			// Token: 0x17001CF5 RID: 7413
			// (get) Token: 0x0600A546 RID: 42310 RVA: 0x0025494E File Offset: 0x00252B4E
			// (set) Token: 0x0600A547 RID: 42311 RVA: 0x00254956 File Offset: 0x00252B56
			public Symbol fromNumberStr { get; private set; }

			// Token: 0x17001CF6 RID: 7414
			// (get) Token: 0x0600A548 RID: 42312 RVA: 0x0025495F File Offset: 0x00252B5F
			// (set) Token: 0x0600A549 RID: 42313 RVA: 0x00254967 File Offset: 0x00252B67
			public Symbol fromNumber { get; private set; }

			// Token: 0x17001CF7 RID: 7415
			// (get) Token: 0x0600A54A RID: 42314 RVA: 0x00254970 File Offset: 0x00252B70
			// (set) Token: 0x0600A54B RID: 42315 RVA: 0x00254978 File Offset: 0x00252B78
			public Symbol fromNumberCoalesced { get; private set; }

			// Token: 0x17001CF8 RID: 7416
			// (get) Token: 0x0600A54C RID: 42316 RVA: 0x00254981 File Offset: 0x00252B81
			// (set) Token: 0x0600A54D RID: 42317 RVA: 0x00254989 File Offset: 0x00252B89
			public Symbol fromRowNumber { get; private set; }

			// Token: 0x17001CF9 RID: 7417
			// (get) Token: 0x0600A54E RID: 42318 RVA: 0x00254992 File Offset: 0x00252B92
			// (set) Token: 0x0600A54F RID: 42319 RVA: 0x0025499A File Offset: 0x00252B9A
			public Symbol fromNumbers { get; private set; }

			// Token: 0x17001CFA RID: 7418
			// (get) Token: 0x0600A550 RID: 42320 RVA: 0x002549A3 File Offset: 0x00252BA3
			// (set) Token: 0x0600A551 RID: 42321 RVA: 0x002549AB File Offset: 0x00252BAB
			public Symbol fromDateTime { get; private set; }

			// Token: 0x17001CFB RID: 7419
			// (get) Token: 0x0600A552 RID: 42322 RVA: 0x002549B4 File Offset: 0x00252BB4
			// (set) Token: 0x0600A553 RID: 42323 RVA: 0x002549BC File Offset: 0x00252BBC
			public Symbol fromDateTimePart { get; private set; }

			// Token: 0x17001CFC RID: 7420
			// (get) Token: 0x0600A554 RID: 42324 RVA: 0x002549C5 File Offset: 0x00252BC5
			// (set) Token: 0x0600A555 RID: 42325 RVA: 0x002549CD File Offset: 0x00252BCD
			public Symbol fromTime { get; private set; }

			// Token: 0x17001CFD RID: 7421
			// (get) Token: 0x0600A556 RID: 42326 RVA: 0x002549D6 File Offset: 0x00252BD6
			// (set) Token: 0x0600A557 RID: 42327 RVA: 0x002549DE File Offset: 0x00252BDE
			public Symbol constString { get; private set; }

			// Token: 0x17001CFE RID: 7422
			// (get) Token: 0x0600A558 RID: 42328 RVA: 0x002549E7 File Offset: 0x00252BE7
			// (set) Token: 0x0600A559 RID: 42329 RVA: 0x002549EF File Offset: 0x00252BEF
			public Symbol constNumber { get; private set; }

			// Token: 0x17001CFF RID: 7423
			// (get) Token: 0x0600A55A RID: 42330 RVA: 0x002549F8 File Offset: 0x00252BF8
			// (set) Token: 0x0600A55B RID: 42331 RVA: 0x00254A00 File Offset: 0x00252C00
			public Symbol constDate { get; private set; }

			// Token: 0x17001D00 RID: 7424
			// (get) Token: 0x0600A55C RID: 42332 RVA: 0x00254A09 File Offset: 0x00252C09
			// (set) Token: 0x0600A55D RID: 42333 RVA: 0x00254A11 File Offset: 0x00252C11
			public Symbol columnName { get; private set; }

			// Token: 0x17001D01 RID: 7425
			// (get) Token: 0x0600A55E RID: 42334 RVA: 0x00254A1A File Offset: 0x00252C1A
			// (set) Token: 0x0600A55F RID: 42335 RVA: 0x00254A22 File Offset: 0x00252C22
			public Symbol columnNames { get; private set; }

			// Token: 0x17001D02 RID: 7426
			// (get) Token: 0x0600A560 RID: 42336 RVA: 0x00254A2B File Offset: 0x00252C2B
			// (set) Token: 0x0600A561 RID: 42337 RVA: 0x00254A33 File Offset: 0x00252C33
			public Symbol constStr { get; private set; }

			// Token: 0x17001D03 RID: 7427
			// (get) Token: 0x0600A562 RID: 42338 RVA: 0x00254A3C File Offset: 0x00252C3C
			// (set) Token: 0x0600A563 RID: 42339 RVA: 0x00254A44 File Offset: 0x00252C44
			public Symbol constNum { get; private set; }

			// Token: 0x17001D04 RID: 7428
			// (get) Token: 0x0600A564 RID: 42340 RVA: 0x00254A4D File Offset: 0x00252C4D
			// (set) Token: 0x0600A565 RID: 42341 RVA: 0x00254A55 File Offset: 0x00252C55
			public Symbol constDt { get; private set; }

			// Token: 0x17001D05 RID: 7429
			// (get) Token: 0x0600A566 RID: 42342 RVA: 0x00254A5E File Offset: 0x00252C5E
			// (set) Token: 0x0600A567 RID: 42343 RVA: 0x00254A66 File Offset: 0x00252C66
			public Symbol locale { get; private set; }

			// Token: 0x17001D06 RID: 7430
			// (get) Token: 0x0600A568 RID: 42344 RVA: 0x00254A6F File Offset: 0x00252C6F
			// (set) Token: 0x0600A569 RID: 42345 RVA: 0x00254A77 File Offset: 0x00252C77
			public Symbol replaceFindText { get; private set; }

			// Token: 0x17001D07 RID: 7431
			// (get) Token: 0x0600A56A RID: 42346 RVA: 0x00254A80 File Offset: 0x00252C80
			// (set) Token: 0x0600A56B RID: 42347 RVA: 0x00254A88 File Offset: 0x00252C88
			public Symbol replaceText { get; private set; }

			// Token: 0x17001D08 RID: 7432
			// (get) Token: 0x0600A56C RID: 42348 RVA: 0x00254A91 File Offset: 0x00252C91
			// (set) Token: 0x0600A56D RID: 42349 RVA: 0x00254A99 File Offset: 0x00252C99
			public Symbol sliceBetweenStartText { get; private set; }

			// Token: 0x17001D09 RID: 7433
			// (get) Token: 0x0600A56E RID: 42350 RVA: 0x00254AA2 File Offset: 0x00252CA2
			// (set) Token: 0x0600A56F RID: 42351 RVA: 0x00254AAA File Offset: 0x00252CAA
			public Symbol sliceBetweenEndText { get; private set; }

			// Token: 0x17001D0A RID: 7434
			// (get) Token: 0x0600A570 RID: 42352 RVA: 0x00254AB3 File Offset: 0x00252CB3
			// (set) Token: 0x0600A571 RID: 42353 RVA: 0x00254ABB File Offset: 0x00252CBB
			public Symbol numberFormatDesc { get; private set; }

			// Token: 0x17001D0B RID: 7435
			// (get) Token: 0x0600A572 RID: 42354 RVA: 0x00254AC4 File Offset: 0x00252CC4
			// (set) Token: 0x0600A573 RID: 42355 RVA: 0x00254ACC File Offset: 0x00252CCC
			public Symbol numberRoundDesc { get; private set; }

			// Token: 0x17001D0C RID: 7436
			// (get) Token: 0x0600A574 RID: 42356 RVA: 0x00254AD5 File Offset: 0x00252CD5
			// (set) Token: 0x0600A575 RID: 42357 RVA: 0x00254ADD File Offset: 0x00252CDD
			public Symbol dateTimeRoundDesc { get; private set; }

			// Token: 0x17001D0D RID: 7437
			// (get) Token: 0x0600A576 RID: 42358 RVA: 0x00254AE6 File Offset: 0x00252CE6
			// (set) Token: 0x0600A577 RID: 42359 RVA: 0x00254AEE File Offset: 0x00252CEE
			public Symbol dateTimeFormatDesc { get; private set; }

			// Token: 0x17001D0E RID: 7438
			// (get) Token: 0x0600A578 RID: 42360 RVA: 0x00254AF7 File Offset: 0x00252CF7
			// (set) Token: 0x0600A579 RID: 42361 RVA: 0x00254AFF File Offset: 0x00252CFF
			public Symbol dateTimeParseDesc { get; private set; }

			// Token: 0x17001D0F RID: 7439
			// (get) Token: 0x0600A57A RID: 42362 RVA: 0x00254B08 File Offset: 0x00252D08
			// (set) Token: 0x0600A57B RID: 42363 RVA: 0x00254B10 File Offset: 0x00252D10
			public Symbol dateTimePartKind { get; private set; }

			// Token: 0x17001D10 RID: 7440
			// (get) Token: 0x0600A57C RID: 42364 RVA: 0x00254B19 File Offset: 0x00252D19
			// (set) Token: 0x0600A57D RID: 42365 RVA: 0x00254B21 File Offset: 0x00252D21
			public Symbol fromDateTimePartKind { get; private set; }

			// Token: 0x17001D11 RID: 7441
			// (get) Token: 0x0600A57E RID: 42366 RVA: 0x00254B2A File Offset: 0x00252D2A
			// (set) Token: 0x0600A57F RID: 42367 RVA: 0x00254B32 File Offset: 0x00252D32
			public Symbol timePartKind { get; private set; }

			// Token: 0x17001D12 RID: 7442
			// (get) Token: 0x0600A580 RID: 42368 RVA: 0x00254B3B File Offset: 0x00252D3B
			// (set) Token: 0x0600A581 RID: 42369 RVA: 0x00254B43 File Offset: 0x00252D43
			public Symbol rowNumberLinearTransformDesc { get; private set; }

			// Token: 0x17001D13 RID: 7443
			// (get) Token: 0x0600A582 RID: 42370 RVA: 0x00254B4C File Offset: 0x00252D4C
			// (set) Token: 0x0600A583 RID: 42371 RVA: 0x00254B54 File Offset: 0x00252D54
			public Symbol matchDesc { get; private set; }

			// Token: 0x17001D14 RID: 7444
			// (get) Token: 0x0600A584 RID: 42372 RVA: 0x00254B5D File Offset: 0x00252D5D
			// (set) Token: 0x0600A585 RID: 42373 RVA: 0x00254B65 File Offset: 0x00252D65
			public Symbol matchInstance { get; private set; }

			// Token: 0x17001D15 RID: 7445
			// (get) Token: 0x0600A586 RID: 42374 RVA: 0x00254B6E File Offset: 0x00252D6E
			// (set) Token: 0x0600A587 RID: 42375 RVA: 0x00254B76 File Offset: 0x00252D76
			public Symbol splitDelimiter { get; private set; }

			// Token: 0x17001D16 RID: 7446
			// (get) Token: 0x0600A588 RID: 42376 RVA: 0x00254B7F File Offset: 0x00252D7F
			// (set) Token: 0x0600A589 RID: 42377 RVA: 0x00254B87 File Offset: 0x00252D87
			public Symbol splitInstance { get; private set; }

			// Token: 0x17001D17 RID: 7447
			// (get) Token: 0x0600A58A RID: 42378 RVA: 0x00254B90 File Offset: 0x00252D90
			// (set) Token: 0x0600A58B RID: 42379 RVA: 0x00254B98 File Offset: 0x00252D98
			public Symbol findDelimiter { get; private set; }

			// Token: 0x17001D18 RID: 7448
			// (get) Token: 0x0600A58C RID: 42380 RVA: 0x00254BA1 File Offset: 0x00252DA1
			// (set) Token: 0x0600A58D RID: 42381 RVA: 0x00254BA9 File Offset: 0x00252DA9
			public Symbol findInstance { get; private set; }

			// Token: 0x17001D19 RID: 7449
			// (get) Token: 0x0600A58E RID: 42382 RVA: 0x00254BB2 File Offset: 0x00252DB2
			// (set) Token: 0x0600A58F RID: 42383 RVA: 0x00254BBA File Offset: 0x00252DBA
			public Symbol findOffset { get; private set; }

			// Token: 0x17001D1A RID: 7450
			// (get) Token: 0x0600A590 RID: 42384 RVA: 0x00254BC3 File Offset: 0x00252DC3
			// (set) Token: 0x0600A591 RID: 42385 RVA: 0x00254BCB File Offset: 0x00252DCB
			public Symbol slicePrefixAbsPos { get; private set; }

			// Token: 0x17001D1B RID: 7451
			// (get) Token: 0x0600A592 RID: 42386 RVA: 0x00254BD4 File Offset: 0x00252DD4
			// (set) Token: 0x0600A593 RID: 42387 RVA: 0x00254BDC File Offset: 0x00252DDC
			public Symbol scaleNumberFactor { get; private set; }

			// Token: 0x17001D1C RID: 7452
			// (get) Token: 0x0600A594 RID: 42388 RVA: 0x00254BE5 File Offset: 0x00252DE5
			// (set) Token: 0x0600A595 RID: 42389 RVA: 0x00254BED File Offset: 0x00252DED
			public Symbol absPos { get; private set; }

			// Token: 0x0600A596 RID: 42390 RVA: 0x00254BF8 File Offset: 0x00252DF8
			public GrammarSymbols(Grammar grammar)
			{
				this.row = grammar.Symbol("row");
				this.result = grammar.Symbol("result");
				this.output = grammar.Symbol("output");
				this.outNumber = grammar.Symbol("outNumber");
				this.outDate = grammar.Symbol("outDate");
				this.outStr = grammar.Symbol("outStr");
				this.outStr1 = grammar.Symbol("outStr1");
				this.segmentCase = grammar.Symbol("segmentCase");
				this.segment = grammar.Symbol("segment");
				this.formatted = grammar.Symbol("formatted");
				this.concatEntry = grammar.Symbol("concatEntry");
				this.concatCase = grammar.Symbol("concatCase");
				this.concat = grammar.Symbol("concat");
				this.concatPrefix = grammar.Symbol("concatPrefix");
				this.concatSegment = grammar.Symbol("concatSegment");
				this.concatSuffix = grammar.Symbol("concatSuffix");
				this.condition = grammar.Symbol("condition");
				this.or = grammar.Symbol("or");
				this.inull = grammar.Symbol("inull");
				this.equalsText = grammar.Symbol("equalsText");
				this.containsFindText = grammar.Symbol("containsFindText");
				this.startsWithFindText = grammar.Symbol("startsWithFindText");
				this.isMatchRegex = grammar.Symbol("isMatchRegex");
				this.containsMatchRegex = grammar.Symbol("containsMatchRegex");
				this.containsCount = grammar.Symbol("containsCount");
				this.matchCount = grammar.Symbol("matchCount");
				this.numberEqualsValue = grammar.Symbol("numberEqualsValue");
				this.numberGreaterThanValue = grammar.Symbol("numberGreaterThanValue");
				this.numberLessThanValue = grammar.Symbol("numberLessThanValue");
				this.formatNumber = grammar.Symbol("formatNumber");
				this.number = grammar.Symbol("number");
				this.number1 = grammar.Symbol("number1");
				this.arithmetic = grammar.Symbol("arithmetic");
				this.arithmeticLeft = grammar.Symbol("arithmeticLeft");
				this.addRight = grammar.Symbol("addRight");
				this.subtractRight = grammar.Symbol("subtractRight");
				this.multiplyRight = grammar.Symbol("multiplyRight");
				this.divideRight = grammar.Symbol("divideRight");
				this.inumber = grammar.Symbol("inumber");
				this.rowNumberTransform = grammar.Symbol("rowNumberTransform");
				this.formatDateTime = grammar.Symbol("formatDateTime");
				this.date = grammar.Symbol("date");
				this.idate = grammar.Symbol("idate");
				this.itime = grammar.Symbol("itime");
				this.parseSubject = grammar.Symbol("parseSubject");
				this.letSubstring = grammar.Symbol("letSubstring");
				this.x = grammar.Symbol("x");
				this.substring = grammar.Symbol("substring");
				this.splitTrim = grammar.Symbol("splitTrim");
				this.split = grammar.Symbol("split");
				this.sliceTrim = grammar.Symbol("sliceTrim");
				this.slice = grammar.Symbol("slice");
				this.pos = grammar.Symbol("pos");
				this.fromStrTrim = grammar.Symbol("fromStrTrim");
				this.fromStr = grammar.Symbol("fromStr");
				this.fromNumberStr = grammar.Symbol("fromNumberStr");
				this.fromNumber = grammar.Symbol("fromNumber");
				this.fromNumberCoalesced = grammar.Symbol("fromNumberCoalesced");
				this.fromRowNumber = grammar.Symbol("fromRowNumber");
				this.fromNumbers = grammar.Symbol("fromNumbers");
				this.fromDateTime = grammar.Symbol("fromDateTime");
				this.fromDateTimePart = grammar.Symbol("fromDateTimePart");
				this.fromTime = grammar.Symbol("fromTime");
				this.constString = grammar.Symbol("constString");
				this.constNumber = grammar.Symbol("constNumber");
				this.constDate = grammar.Symbol("constDate");
				this.columnName = grammar.Symbol("columnName");
				this.columnNames = grammar.Symbol("columnNames");
				this.constStr = grammar.Symbol("constStr");
				this.constNum = grammar.Symbol("constNum");
				this.constDt = grammar.Symbol("constDt");
				this.locale = grammar.Symbol("locale");
				this.replaceFindText = grammar.Symbol("replaceFindText");
				this.replaceText = grammar.Symbol("replaceText");
				this.sliceBetweenStartText = grammar.Symbol("sliceBetweenStartText");
				this.sliceBetweenEndText = grammar.Symbol("sliceBetweenEndText");
				this.numberFormatDesc = grammar.Symbol("numberFormatDesc");
				this.numberRoundDesc = grammar.Symbol("numberRoundDesc");
				this.dateTimeRoundDesc = grammar.Symbol("dateTimeRoundDesc");
				this.dateTimeFormatDesc = grammar.Symbol("dateTimeFormatDesc");
				this.dateTimeParseDesc = grammar.Symbol("dateTimeParseDesc");
				this.dateTimePartKind = grammar.Symbol("dateTimePartKind");
				this.fromDateTimePartKind = grammar.Symbol("fromDateTimePartKind");
				this.timePartKind = grammar.Symbol("timePartKind");
				this.rowNumberLinearTransformDesc = grammar.Symbol("rowNumberLinearTransformDesc");
				this.matchDesc = grammar.Symbol("matchDesc");
				this.matchInstance = grammar.Symbol("matchInstance");
				this.splitDelimiter = grammar.Symbol("splitDelimiter");
				this.splitInstance = grammar.Symbol("splitInstance");
				this.findDelimiter = grammar.Symbol("findDelimiter");
				this.findInstance = grammar.Symbol("findInstance");
				this.findOffset = grammar.Symbol("findOffset");
				this.slicePrefixAbsPos = grammar.Symbol("slicePrefixAbsPos");
				this.scaleNumberFactor = grammar.Symbol("scaleNumberFactor");
				this.absPos = grammar.Symbol("absPos");
			}
		}

		// Token: 0x020014FA RID: 5370
		public class GrammarRules
		{
			// Token: 0x17001D1D RID: 7453
			// (get) Token: 0x0600A597 RID: 42391 RVA: 0x0025525A File Offset: 0x0025345A
			// (set) Token: 0x0600A598 RID: 42392 RVA: 0x00255262 File Offset: 0x00253462
			public BlackBoxRule If { get; private set; }

			// Token: 0x17001D1E RID: 7454
			// (get) Token: 0x0600A599 RID: 42393 RVA: 0x0025526B File Offset: 0x0025346B
			// (set) Token: 0x0600A59A RID: 42394 RVA: 0x00255273 File Offset: 0x00253473
			public BlackBoxRule ToInt { get; private set; }

			// Token: 0x17001D1F RID: 7455
			// (get) Token: 0x0600A59B RID: 42395 RVA: 0x0025527C File Offset: 0x0025347C
			// (set) Token: 0x0600A59C RID: 42396 RVA: 0x00255284 File Offset: 0x00253484
			public BlackBoxRule ToDouble { get; private set; }

			// Token: 0x17001D20 RID: 7456
			// (get) Token: 0x0600A59D RID: 42397 RVA: 0x0025528D File Offset: 0x0025348D
			// (set) Token: 0x0600A59E RID: 42398 RVA: 0x00255295 File Offset: 0x00253495
			public BlackBoxRule ToDecimal { get; private set; }

			// Token: 0x17001D21 RID: 7457
			// (get) Token: 0x0600A59F RID: 42399 RVA: 0x0025529E File Offset: 0x0025349E
			// (set) Token: 0x0600A5A0 RID: 42400 RVA: 0x002552A6 File Offset: 0x002534A6
			public BlackBoxRule ToDateTime { get; private set; }

			// Token: 0x17001D22 RID: 7458
			// (get) Token: 0x0600A5A1 RID: 42401 RVA: 0x002552AF File Offset: 0x002534AF
			// (set) Token: 0x0600A5A2 RID: 42402 RVA: 0x002552B7 File Offset: 0x002534B7
			public BlackBoxRule ToStr { get; private set; }

			// Token: 0x17001D23 RID: 7459
			// (get) Token: 0x0600A5A3 RID: 42403 RVA: 0x002552C0 File Offset: 0x002534C0
			// (set) Token: 0x0600A5A4 RID: 42404 RVA: 0x002552C8 File Offset: 0x002534C8
			public BlackBoxRule Replace { get; private set; }

			// Token: 0x17001D24 RID: 7460
			// (get) Token: 0x0600A5A5 RID: 42405 RVA: 0x002552D1 File Offset: 0x002534D1
			// (set) Token: 0x0600A5A6 RID: 42406 RVA: 0x002552D9 File Offset: 0x002534D9
			public BlackBoxRule LowerCase { get; private set; }

			// Token: 0x17001D25 RID: 7461
			// (get) Token: 0x0600A5A7 RID: 42407 RVA: 0x002552E2 File Offset: 0x002534E2
			// (set) Token: 0x0600A5A8 RID: 42408 RVA: 0x002552EA File Offset: 0x002534EA
			public BlackBoxRule UpperCase { get; private set; }

			// Token: 0x17001D26 RID: 7462
			// (get) Token: 0x0600A5A9 RID: 42409 RVA: 0x002552F3 File Offset: 0x002534F3
			// (set) Token: 0x0600A5AA RID: 42410 RVA: 0x002552FB File Offset: 0x002534FB
			public BlackBoxRule ProperCase { get; private set; }

			// Token: 0x17001D27 RID: 7463
			// (get) Token: 0x0600A5AB RID: 42411 RVA: 0x00255304 File Offset: 0x00253504
			// (set) Token: 0x0600A5AC RID: 42412 RVA: 0x0025530C File Offset: 0x0025350C
			public BlackBoxRule LowerCaseConcat { get; private set; }

			// Token: 0x17001D28 RID: 7464
			// (get) Token: 0x0600A5AD RID: 42413 RVA: 0x00255315 File Offset: 0x00253515
			// (set) Token: 0x0600A5AE RID: 42414 RVA: 0x0025531D File Offset: 0x0025351D
			public BlackBoxRule UpperCaseConcat { get; private set; }

			// Token: 0x17001D29 RID: 7465
			// (get) Token: 0x0600A5AF RID: 42415 RVA: 0x00255326 File Offset: 0x00253526
			// (set) Token: 0x0600A5B0 RID: 42416 RVA: 0x0025532E File Offset: 0x0025352E
			public BlackBoxRule ProperCaseConcat { get; private set; }

			// Token: 0x17001D2A RID: 7466
			// (get) Token: 0x0600A5B1 RID: 42417 RVA: 0x00255337 File Offset: 0x00253537
			// (set) Token: 0x0600A5B2 RID: 42418 RVA: 0x0025533F File Offset: 0x0025353F
			public BlackBoxRule Concat { get; private set; }

			// Token: 0x17001D2B RID: 7467
			// (get) Token: 0x0600A5B3 RID: 42419 RVA: 0x00255348 File Offset: 0x00253548
			// (set) Token: 0x0600A5B4 RID: 42420 RVA: 0x00255350 File Offset: 0x00253550
			public BlackBoxRule StringEquals { get; private set; }

			// Token: 0x17001D2C RID: 7468
			// (get) Token: 0x0600A5B5 RID: 42421 RVA: 0x00255359 File Offset: 0x00253559
			// (set) Token: 0x0600A5B6 RID: 42422 RVA: 0x00255361 File Offset: 0x00253561
			public BlackBoxRule Contains { get; private set; }

			// Token: 0x17001D2D RID: 7469
			// (get) Token: 0x0600A5B7 RID: 42423 RVA: 0x0025536A File Offset: 0x0025356A
			// (set) Token: 0x0600A5B8 RID: 42424 RVA: 0x00255372 File Offset: 0x00253572
			public BlackBoxRule StartsWithDigit { get; private set; }

			// Token: 0x17001D2E RID: 7470
			// (get) Token: 0x0600A5B9 RID: 42425 RVA: 0x0025537B File Offset: 0x0025357B
			// (set) Token: 0x0600A5BA RID: 42426 RVA: 0x00255383 File Offset: 0x00253583
			public BlackBoxRule EndsWithDigit { get; private set; }

			// Token: 0x17001D2F RID: 7471
			// (get) Token: 0x0600A5BB RID: 42427 RVA: 0x0025538C File Offset: 0x0025358C
			// (set) Token: 0x0600A5BC RID: 42428 RVA: 0x00255394 File Offset: 0x00253594
			public BlackBoxRule StartsWith { get; private set; }

			// Token: 0x17001D30 RID: 7472
			// (get) Token: 0x0600A5BD RID: 42429 RVA: 0x0025539D File Offset: 0x0025359D
			// (set) Token: 0x0600A5BE RID: 42430 RVA: 0x002553A5 File Offset: 0x002535A5
			public BlackBoxRule IsBlank { get; private set; }

			// Token: 0x17001D31 RID: 7473
			// (get) Token: 0x0600A5BF RID: 42431 RVA: 0x002553AE File Offset: 0x002535AE
			// (set) Token: 0x0600A5C0 RID: 42432 RVA: 0x002553B6 File Offset: 0x002535B6
			public BlackBoxRule IsNotBlank { get; private set; }

			// Token: 0x17001D32 RID: 7474
			// (get) Token: 0x0600A5C1 RID: 42433 RVA: 0x002553BF File Offset: 0x002535BF
			// (set) Token: 0x0600A5C2 RID: 42434 RVA: 0x002553C7 File Offset: 0x002535C7
			public BlackBoxRule NumberEquals { get; private set; }

			// Token: 0x17001D33 RID: 7475
			// (get) Token: 0x0600A5C3 RID: 42435 RVA: 0x002553D0 File Offset: 0x002535D0
			// (set) Token: 0x0600A5C4 RID: 42436 RVA: 0x002553D8 File Offset: 0x002535D8
			public BlackBoxRule NumberGreaterThan { get; private set; }

			// Token: 0x17001D34 RID: 7476
			// (get) Token: 0x0600A5C5 RID: 42437 RVA: 0x002553E1 File Offset: 0x002535E1
			// (set) Token: 0x0600A5C6 RID: 42438 RVA: 0x002553E9 File Offset: 0x002535E9
			public BlackBoxRule NumberLessThan { get; private set; }

			// Token: 0x17001D35 RID: 7477
			// (get) Token: 0x0600A5C7 RID: 42439 RVA: 0x002553F2 File Offset: 0x002535F2
			// (set) Token: 0x0600A5C8 RID: 42440 RVA: 0x002553FA File Offset: 0x002535FA
			public BlackBoxRule IsString { get; private set; }

			// Token: 0x17001D36 RID: 7478
			// (get) Token: 0x0600A5C9 RID: 42441 RVA: 0x00255403 File Offset: 0x00253603
			// (set) Token: 0x0600A5CA RID: 42442 RVA: 0x0025540B File Offset: 0x0025360B
			public BlackBoxRule IsNumber { get; private set; }

			// Token: 0x17001D37 RID: 7479
			// (get) Token: 0x0600A5CB RID: 42443 RVA: 0x00255414 File Offset: 0x00253614
			// (set) Token: 0x0600A5CC RID: 42444 RVA: 0x0025541C File Offset: 0x0025361C
			public BlackBoxRule IsMatch { get; private set; }

			// Token: 0x17001D38 RID: 7480
			// (get) Token: 0x0600A5CD RID: 42445 RVA: 0x00255425 File Offset: 0x00253625
			// (set) Token: 0x0600A5CE RID: 42446 RVA: 0x0025542D File Offset: 0x0025362D
			public BlackBoxRule ContainsMatch { get; private set; }

			// Token: 0x17001D39 RID: 7481
			// (get) Token: 0x0600A5CF RID: 42447 RVA: 0x00255436 File Offset: 0x00253636
			// (set) Token: 0x0600A5D0 RID: 42448 RVA: 0x0025543E File Offset: 0x0025363E
			public BlackBoxRule Or { get; private set; }

			// Token: 0x17001D3A RID: 7482
			// (get) Token: 0x0600A5D1 RID: 42449 RVA: 0x00255447 File Offset: 0x00253647
			// (set) Token: 0x0600A5D2 RID: 42450 RVA: 0x0025544F File Offset: 0x0025364F
			public BlackBoxRule Null { get; private set; }

			// Token: 0x17001D3B RID: 7483
			// (get) Token: 0x0600A5D3 RID: 42451 RVA: 0x00255458 File Offset: 0x00253658
			// (set) Token: 0x0600A5D4 RID: 42452 RVA: 0x00255460 File Offset: 0x00253660
			public BlackBoxRule FormatNumber { get; private set; }

			// Token: 0x17001D3C RID: 7484
			// (get) Token: 0x0600A5D5 RID: 42453 RVA: 0x00255469 File Offset: 0x00253669
			// (set) Token: 0x0600A5D6 RID: 42454 RVA: 0x00255471 File Offset: 0x00253671
			public BlackBoxRule Length { get; private set; }

			// Token: 0x17001D3D RID: 7485
			// (get) Token: 0x0600A5D7 RID: 42455 RVA: 0x0025547A File Offset: 0x0025367A
			// (set) Token: 0x0600A5D8 RID: 42456 RVA: 0x00255482 File Offset: 0x00253682
			public BlackBoxRule DateTimePart { get; private set; }

			// Token: 0x17001D3E RID: 7486
			// (get) Token: 0x0600A5D9 RID: 42457 RVA: 0x0025548B File Offset: 0x0025368B
			// (set) Token: 0x0600A5DA RID: 42458 RVA: 0x00255493 File Offset: 0x00253693
			public BlackBoxRule TimePart { get; private set; }

			// Token: 0x17001D3F RID: 7487
			// (get) Token: 0x0600A5DB RID: 42459 RVA: 0x0025549C File Offset: 0x0025369C
			// (set) Token: 0x0600A5DC RID: 42460 RVA: 0x002554A4 File Offset: 0x002536A4
			public BlackBoxRule RoundNumber { get; private set; }

			// Token: 0x17001D40 RID: 7488
			// (get) Token: 0x0600A5DD RID: 42461 RVA: 0x002554AD File Offset: 0x002536AD
			// (set) Token: 0x0600A5DE RID: 42462 RVA: 0x002554B5 File Offset: 0x002536B5
			public BlackBoxRule Add { get; private set; }

			// Token: 0x17001D41 RID: 7489
			// (get) Token: 0x0600A5DF RID: 42463 RVA: 0x002554BE File Offset: 0x002536BE
			// (set) Token: 0x0600A5E0 RID: 42464 RVA: 0x002554C6 File Offset: 0x002536C6
			public BlackBoxRule Subtract { get; private set; }

			// Token: 0x17001D42 RID: 7490
			// (get) Token: 0x0600A5E1 RID: 42465 RVA: 0x002554CF File Offset: 0x002536CF
			// (set) Token: 0x0600A5E2 RID: 42466 RVA: 0x002554D7 File Offset: 0x002536D7
			public BlackBoxRule Multiply { get; private set; }

			// Token: 0x17001D43 RID: 7491
			// (get) Token: 0x0600A5E3 RID: 42467 RVA: 0x002554E0 File Offset: 0x002536E0
			// (set) Token: 0x0600A5E4 RID: 42468 RVA: 0x002554E8 File Offset: 0x002536E8
			public BlackBoxRule Divide { get; private set; }

			// Token: 0x17001D44 RID: 7492
			// (get) Token: 0x0600A5E5 RID: 42469 RVA: 0x002554F1 File Offset: 0x002536F1
			// (set) Token: 0x0600A5E6 RID: 42470 RVA: 0x002554F9 File Offset: 0x002536F9
			public BlackBoxRule Sum { get; private set; }

			// Token: 0x17001D45 RID: 7493
			// (get) Token: 0x0600A5E7 RID: 42471 RVA: 0x00255502 File Offset: 0x00253702
			// (set) Token: 0x0600A5E8 RID: 42472 RVA: 0x0025550A File Offset: 0x0025370A
			public BlackBoxRule Product { get; private set; }

			// Token: 0x17001D46 RID: 7494
			// (get) Token: 0x0600A5E9 RID: 42473 RVA: 0x00255513 File Offset: 0x00253713
			// (set) Token: 0x0600A5EA RID: 42474 RVA: 0x0025551B File Offset: 0x0025371B
			public BlackBoxRule Average { get; private set; }

			// Token: 0x17001D47 RID: 7495
			// (get) Token: 0x0600A5EB RID: 42475 RVA: 0x00255524 File Offset: 0x00253724
			// (set) Token: 0x0600A5EC RID: 42476 RVA: 0x0025552C File Offset: 0x0025372C
			public BlackBoxRule AddRightNumber { get; private set; }

			// Token: 0x17001D48 RID: 7496
			// (get) Token: 0x0600A5ED RID: 42477 RVA: 0x00255535 File Offset: 0x00253735
			// (set) Token: 0x0600A5EE RID: 42478 RVA: 0x0025553D File Offset: 0x0025373D
			public BlackBoxRule SubtractRightNumber { get; private set; }

			// Token: 0x17001D49 RID: 7497
			// (get) Token: 0x0600A5EF RID: 42479 RVA: 0x00255546 File Offset: 0x00253746
			// (set) Token: 0x0600A5F0 RID: 42480 RVA: 0x0025554E File Offset: 0x0025374E
			public BlackBoxRule MultiplyRightNumber { get; private set; }

			// Token: 0x17001D4A RID: 7498
			// (get) Token: 0x0600A5F1 RID: 42481 RVA: 0x00255557 File Offset: 0x00253757
			// (set) Token: 0x0600A5F2 RID: 42482 RVA: 0x0025555F File Offset: 0x0025375F
			public BlackBoxRule DivideRightNumber { get; private set; }

			// Token: 0x17001D4B RID: 7499
			// (get) Token: 0x0600A5F3 RID: 42483 RVA: 0x00255568 File Offset: 0x00253768
			// (set) Token: 0x0600A5F4 RID: 42484 RVA: 0x00255570 File Offset: 0x00253770
			public BlackBoxRule ParseNumber { get; private set; }

			// Token: 0x17001D4C RID: 7500
			// (get) Token: 0x0600A5F5 RID: 42485 RVA: 0x00255579 File Offset: 0x00253779
			// (set) Token: 0x0600A5F6 RID: 42486 RVA: 0x00255581 File Offset: 0x00253781
			public BlackBoxRule RowNumberLinearTransform { get; private set; }

			// Token: 0x17001D4D RID: 7501
			// (get) Token: 0x0600A5F7 RID: 42487 RVA: 0x0025558A File Offset: 0x0025378A
			// (set) Token: 0x0600A5F8 RID: 42488 RVA: 0x00255592 File Offset: 0x00253792
			public BlackBoxRule FormatDateTime { get; private set; }

			// Token: 0x17001D4E RID: 7502
			// (get) Token: 0x0600A5F9 RID: 42489 RVA: 0x0025559B File Offset: 0x0025379B
			// (set) Token: 0x0600A5FA RID: 42490 RVA: 0x002555A3 File Offset: 0x002537A3
			public BlackBoxRule RoundDateTime { get; private set; }

			// Token: 0x17001D4F RID: 7503
			// (get) Token: 0x0600A5FB RID: 42491 RVA: 0x002555AC File Offset: 0x002537AC
			// (set) Token: 0x0600A5FC RID: 42492 RVA: 0x002555B4 File Offset: 0x002537B4
			public BlackBoxRule ParseDateTime { get; private set; }

			// Token: 0x17001D50 RID: 7504
			// (get) Token: 0x0600A5FD RID: 42493 RVA: 0x002555BD File Offset: 0x002537BD
			// (set) Token: 0x0600A5FE RID: 42494 RVA: 0x002555C5 File Offset: 0x002537C5
			public BlackBoxRule SlicePrefixAbs { get; private set; }

			// Token: 0x17001D51 RID: 7505
			// (get) Token: 0x0600A5FF RID: 42495 RVA: 0x002555CE File Offset: 0x002537CE
			// (set) Token: 0x0600A600 RID: 42496 RVA: 0x002555D6 File Offset: 0x002537D6
			public BlackBoxRule SlicePrefix { get; private set; }

			// Token: 0x17001D52 RID: 7506
			// (get) Token: 0x0600A601 RID: 42497 RVA: 0x002555DF File Offset: 0x002537DF
			// (set) Token: 0x0600A602 RID: 42498 RVA: 0x002555E7 File Offset: 0x002537E7
			public BlackBoxRule SliceSuffix { get; private set; }

			// Token: 0x17001D53 RID: 7507
			// (get) Token: 0x0600A603 RID: 42499 RVA: 0x002555F0 File Offset: 0x002537F0
			// (set) Token: 0x0600A604 RID: 42500 RVA: 0x002555F8 File Offset: 0x002537F8
			public BlackBoxRule MatchFull { get; private set; }

			// Token: 0x17001D54 RID: 7508
			// (get) Token: 0x0600A605 RID: 42501 RVA: 0x00255601 File Offset: 0x00253801
			// (set) Token: 0x0600A606 RID: 42502 RVA: 0x00255609 File Offset: 0x00253809
			public BlackBoxRule SliceBetween { get; private set; }

			// Token: 0x17001D55 RID: 7509
			// (get) Token: 0x0600A607 RID: 42503 RVA: 0x00255612 File Offset: 0x00253812
			// (set) Token: 0x0600A608 RID: 42504 RVA: 0x0025561A File Offset: 0x0025381A
			public BlackBoxRule TrimSplit { get; private set; }

			// Token: 0x17001D56 RID: 7510
			// (get) Token: 0x0600A609 RID: 42505 RVA: 0x00255623 File Offset: 0x00253823
			// (set) Token: 0x0600A60A RID: 42506 RVA: 0x0025562B File Offset: 0x0025382B
			public BlackBoxRule TrimFullSplit { get; private set; }

			// Token: 0x17001D57 RID: 7511
			// (get) Token: 0x0600A60B RID: 42507 RVA: 0x00255634 File Offset: 0x00253834
			// (set) Token: 0x0600A60C RID: 42508 RVA: 0x0025563C File Offset: 0x0025383C
			public BlackBoxRule Split { get; private set; }

			// Token: 0x17001D58 RID: 7512
			// (get) Token: 0x0600A60D RID: 42509 RVA: 0x00255645 File Offset: 0x00253845
			// (set) Token: 0x0600A60E RID: 42510 RVA: 0x0025564D File Offset: 0x0025384D
			public BlackBoxRule TrimSlice { get; private set; }

			// Token: 0x17001D59 RID: 7513
			// (get) Token: 0x0600A60F RID: 42511 RVA: 0x00255656 File Offset: 0x00253856
			// (set) Token: 0x0600A610 RID: 42512 RVA: 0x0025565E File Offset: 0x0025385E
			public BlackBoxRule TrimFullSlice { get; private set; }

			// Token: 0x17001D5A RID: 7514
			// (get) Token: 0x0600A611 RID: 42513 RVA: 0x00255667 File Offset: 0x00253867
			// (set) Token: 0x0600A612 RID: 42514 RVA: 0x0025566F File Offset: 0x0025386F
			public BlackBoxRule Slice { get; private set; }

			// Token: 0x17001D5B RID: 7515
			// (get) Token: 0x0600A613 RID: 42515 RVA: 0x00255678 File Offset: 0x00253878
			// (set) Token: 0x0600A614 RID: 42516 RVA: 0x00255680 File Offset: 0x00253880
			public BlackBoxRule Find { get; private set; }

			// Token: 0x17001D5C RID: 7516
			// (get) Token: 0x0600A615 RID: 42517 RVA: 0x00255689 File Offset: 0x00253889
			// (set) Token: 0x0600A616 RID: 42518 RVA: 0x00255691 File Offset: 0x00253891
			public BlackBoxRule Abs { get; private set; }

			// Token: 0x17001D5D RID: 7517
			// (get) Token: 0x0600A617 RID: 42519 RVA: 0x0025569A File Offset: 0x0025389A
			// (set) Token: 0x0600A618 RID: 42520 RVA: 0x002556A2 File Offset: 0x002538A2
			public BlackBoxRule Match { get; private set; }

			// Token: 0x17001D5E RID: 7518
			// (get) Token: 0x0600A619 RID: 42521 RVA: 0x002556AB File Offset: 0x002538AB
			// (set) Token: 0x0600A61A RID: 42522 RVA: 0x002556B3 File Offset: 0x002538B3
			public BlackBoxRule MatchEnd { get; private set; }

			// Token: 0x17001D5F RID: 7519
			// (get) Token: 0x0600A61B RID: 42523 RVA: 0x002556BC File Offset: 0x002538BC
			// (set) Token: 0x0600A61C RID: 42524 RVA: 0x002556C4 File Offset: 0x002538C4
			public BlackBoxRule TrimFull { get; private set; }

			// Token: 0x17001D60 RID: 7520
			// (get) Token: 0x0600A61D RID: 42525 RVA: 0x002556CD File Offset: 0x002538CD
			// (set) Token: 0x0600A61E RID: 42526 RVA: 0x002556D5 File Offset: 0x002538D5
			public BlackBoxRule Trim { get; private set; }

			// Token: 0x17001D61 RID: 7521
			// (get) Token: 0x0600A61F RID: 42527 RVA: 0x002556DE File Offset: 0x002538DE
			// (set) Token: 0x0600A620 RID: 42528 RVA: 0x002556E6 File Offset: 0x002538E6
			public BlackBoxRule FromStr { get; private set; }

			// Token: 0x17001D62 RID: 7522
			// (get) Token: 0x0600A621 RID: 42529 RVA: 0x002556EF File Offset: 0x002538EF
			// (set) Token: 0x0600A622 RID: 42530 RVA: 0x002556F7 File Offset: 0x002538F7
			public BlackBoxRule FromNumberStr { get; private set; }

			// Token: 0x17001D63 RID: 7523
			// (get) Token: 0x0600A623 RID: 42531 RVA: 0x00255700 File Offset: 0x00253900
			// (set) Token: 0x0600A624 RID: 42532 RVA: 0x00255708 File Offset: 0x00253908
			public BlackBoxRule FromNumber { get; private set; }

			// Token: 0x17001D64 RID: 7524
			// (get) Token: 0x0600A625 RID: 42533 RVA: 0x00255711 File Offset: 0x00253911
			// (set) Token: 0x0600A626 RID: 42534 RVA: 0x00255719 File Offset: 0x00253919
			public BlackBoxRule FromNumberCoalesced { get; private set; }

			// Token: 0x17001D65 RID: 7525
			// (get) Token: 0x0600A627 RID: 42535 RVA: 0x00255722 File Offset: 0x00253922
			// (set) Token: 0x0600A628 RID: 42536 RVA: 0x0025572A File Offset: 0x0025392A
			public BlackBoxRule FromRowNumber { get; private set; }

			// Token: 0x17001D66 RID: 7526
			// (get) Token: 0x0600A629 RID: 42537 RVA: 0x00255733 File Offset: 0x00253933
			// (set) Token: 0x0600A62A RID: 42538 RVA: 0x0025573B File Offset: 0x0025393B
			public BlackBoxRule FromNumbers { get; private set; }

			// Token: 0x17001D67 RID: 7527
			// (get) Token: 0x0600A62B RID: 42539 RVA: 0x00255744 File Offset: 0x00253944
			// (set) Token: 0x0600A62C RID: 42540 RVA: 0x0025574C File Offset: 0x0025394C
			public BlackBoxRule FromDateTime { get; private set; }

			// Token: 0x17001D68 RID: 7528
			// (get) Token: 0x0600A62D RID: 42541 RVA: 0x00255755 File Offset: 0x00253955
			// (set) Token: 0x0600A62E RID: 42542 RVA: 0x0025575D File Offset: 0x0025395D
			public BlackBoxRule FromDateTimePart { get; private set; }

			// Token: 0x17001D69 RID: 7529
			// (get) Token: 0x0600A62F RID: 42543 RVA: 0x00255766 File Offset: 0x00253966
			// (set) Token: 0x0600A630 RID: 42544 RVA: 0x0025576E File Offset: 0x0025396E
			public BlackBoxRule FromTime { get; private set; }

			// Token: 0x17001D6A RID: 7530
			// (get) Token: 0x0600A631 RID: 42545 RVA: 0x00255777 File Offset: 0x00253977
			// (set) Token: 0x0600A632 RID: 42546 RVA: 0x0025577F File Offset: 0x0025397F
			public BlackBoxRule Str { get; private set; }

			// Token: 0x17001D6B RID: 7531
			// (get) Token: 0x0600A633 RID: 42547 RVA: 0x00255788 File Offset: 0x00253988
			// (set) Token: 0x0600A634 RID: 42548 RVA: 0x00255790 File Offset: 0x00253990
			public BlackBoxRule Number { get; private set; }

			// Token: 0x17001D6C RID: 7532
			// (get) Token: 0x0600A635 RID: 42549 RVA: 0x00255799 File Offset: 0x00253999
			// (set) Token: 0x0600A636 RID: 42550 RVA: 0x002557A1 File Offset: 0x002539A1
			public BlackBoxRule Date { get; private set; }

			// Token: 0x17001D6D RID: 7533
			// (get) Token: 0x0600A637 RID: 42551 RVA: 0x002557AA File Offset: 0x002539AA
			// (set) Token: 0x0600A638 RID: 42552 RVA: 0x002557B2 File Offset: 0x002539B2
			public LetRule LetX { get; private set; }

			// Token: 0x0600A639 RID: 42553 RVA: 0x002557BC File Offset: 0x002539BC
			public GrammarRules(Grammar grammar)
			{
				this.If = (BlackBoxRule)grammar.Rule("If");
				this.ToInt = (BlackBoxRule)grammar.Rule("ToInt");
				this.ToDouble = (BlackBoxRule)grammar.Rule("ToDouble");
				this.ToDecimal = (BlackBoxRule)grammar.Rule("ToDecimal");
				this.ToDateTime = (BlackBoxRule)grammar.Rule("ToDateTime");
				this.ToStr = (BlackBoxRule)grammar.Rule("ToStr");
				this.Replace = (BlackBoxRule)grammar.Rule("Replace");
				this.LowerCase = (BlackBoxRule)grammar.Rule("LowerCase");
				this.UpperCase = (BlackBoxRule)grammar.Rule("UpperCase");
				this.ProperCase = (BlackBoxRule)grammar.Rule("ProperCase");
				this.LowerCaseConcat = (BlackBoxRule)grammar.Rule("LowerCaseConcat");
				this.UpperCaseConcat = (BlackBoxRule)grammar.Rule("UpperCaseConcat");
				this.ProperCaseConcat = (BlackBoxRule)grammar.Rule("ProperCaseConcat");
				this.Concat = (BlackBoxRule)grammar.Rule("Concat");
				this.StringEquals = (BlackBoxRule)grammar.Rule("StringEquals");
				this.Contains = (BlackBoxRule)grammar.Rule("Contains");
				this.StartsWithDigit = (BlackBoxRule)grammar.Rule("StartsWithDigit");
				this.EndsWithDigit = (BlackBoxRule)grammar.Rule("EndsWithDigit");
				this.StartsWith = (BlackBoxRule)grammar.Rule("StartsWith");
				this.IsBlank = (BlackBoxRule)grammar.Rule("IsBlank");
				this.IsNotBlank = (BlackBoxRule)grammar.Rule("IsNotBlank");
				this.NumberEquals = (BlackBoxRule)grammar.Rule("NumberEquals");
				this.NumberGreaterThan = (BlackBoxRule)grammar.Rule("NumberGreaterThan");
				this.NumberLessThan = (BlackBoxRule)grammar.Rule("NumberLessThan");
				this.IsString = (BlackBoxRule)grammar.Rule("IsString");
				this.IsNumber = (BlackBoxRule)grammar.Rule("IsNumber");
				this.IsMatch = (BlackBoxRule)grammar.Rule("IsMatch");
				this.ContainsMatch = (BlackBoxRule)grammar.Rule("ContainsMatch");
				this.Or = (BlackBoxRule)grammar.Rule("Or");
				this.Null = (BlackBoxRule)grammar.Rule("Null");
				this.FormatNumber = (BlackBoxRule)grammar.Rule("FormatNumber");
				this.Length = (BlackBoxRule)grammar.Rule("Length");
				this.DateTimePart = (BlackBoxRule)grammar.Rule("DateTimePart");
				this.TimePart = (BlackBoxRule)grammar.Rule("TimePart");
				this.RoundNumber = (BlackBoxRule)grammar.Rule("RoundNumber");
				this.Add = (BlackBoxRule)grammar.Rule("Add");
				this.Subtract = (BlackBoxRule)grammar.Rule("Subtract");
				this.Multiply = (BlackBoxRule)grammar.Rule("Multiply");
				this.Divide = (BlackBoxRule)grammar.Rule("Divide");
				this.Sum = (BlackBoxRule)grammar.Rule("Sum");
				this.Product = (BlackBoxRule)grammar.Rule("Product");
				this.Average = (BlackBoxRule)grammar.Rule("Average");
				this.AddRightNumber = (BlackBoxRule)grammar.Rule("AddRightNumber");
				this.SubtractRightNumber = (BlackBoxRule)grammar.Rule("SubtractRightNumber");
				this.MultiplyRightNumber = (BlackBoxRule)grammar.Rule("MultiplyRightNumber");
				this.DivideRightNumber = (BlackBoxRule)grammar.Rule("DivideRightNumber");
				this.ParseNumber = (BlackBoxRule)grammar.Rule("ParseNumber");
				this.RowNumberLinearTransform = (BlackBoxRule)grammar.Rule("RowNumberLinearTransform");
				this.FormatDateTime = (BlackBoxRule)grammar.Rule("FormatDateTime");
				this.RoundDateTime = (BlackBoxRule)grammar.Rule("RoundDateTime");
				this.ParseDateTime = (BlackBoxRule)grammar.Rule("ParseDateTime");
				this.SlicePrefixAbs = (BlackBoxRule)grammar.Rule("SlicePrefixAbs");
				this.SlicePrefix = (BlackBoxRule)grammar.Rule("SlicePrefix");
				this.SliceSuffix = (BlackBoxRule)grammar.Rule("SliceSuffix");
				this.MatchFull = (BlackBoxRule)grammar.Rule("MatchFull");
				this.SliceBetween = (BlackBoxRule)grammar.Rule("SliceBetween");
				this.TrimSplit = (BlackBoxRule)grammar.Rule("TrimSplit");
				this.TrimFullSplit = (BlackBoxRule)grammar.Rule("TrimFullSplit");
				this.Split = (BlackBoxRule)grammar.Rule("Split");
				this.TrimSlice = (BlackBoxRule)grammar.Rule("TrimSlice");
				this.TrimFullSlice = (BlackBoxRule)grammar.Rule("TrimFullSlice");
				this.Slice = (BlackBoxRule)grammar.Rule("Slice");
				this.Find = (BlackBoxRule)grammar.Rule("Find");
				this.Abs = (BlackBoxRule)grammar.Rule("Abs");
				this.Match = (BlackBoxRule)grammar.Rule("Match");
				this.MatchEnd = (BlackBoxRule)grammar.Rule("MatchEnd");
				this.TrimFull = (BlackBoxRule)grammar.Rule("TrimFull");
				this.Trim = (BlackBoxRule)grammar.Rule("Trim");
				this.FromStr = (BlackBoxRule)grammar.Rule("FromStr");
				this.FromNumberStr = (BlackBoxRule)grammar.Rule("FromNumberStr");
				this.FromNumber = (BlackBoxRule)grammar.Rule("FromNumber");
				this.FromNumberCoalesced = (BlackBoxRule)grammar.Rule("FromNumberCoalesced");
				this.FromRowNumber = (BlackBoxRule)grammar.Rule("FromRowNumber");
				this.FromNumbers = (BlackBoxRule)grammar.Rule("FromNumbers");
				this.FromDateTime = (BlackBoxRule)grammar.Rule("FromDateTime");
				this.FromDateTimePart = (BlackBoxRule)grammar.Rule("FromDateTimePart");
				this.FromTime = (BlackBoxRule)grammar.Rule("FromTime");
				this.Str = (BlackBoxRule)grammar.Rule("Str");
				this.Number = (BlackBoxRule)grammar.Rule("Number");
				this.Date = (BlackBoxRule)grammar.Rule("Date");
				this.LetX = (LetRule)grammar.Rule("LetX");
			}
		}

		// Token: 0x020014FB RID: 5371
		public class GrammarUnnamedConversions
		{
			// Token: 0x17001D6E RID: 7534
			// (get) Token: 0x0600A63A RID: 42554 RVA: 0x00255EC5 File Offset: 0x002540C5
			// (set) Token: 0x0600A63B RID: 42555 RVA: 0x00255ECD File Offset: 0x002540CD
			public ConversionRule result_output { get; private set; }

			// Token: 0x17001D6F RID: 7535
			// (get) Token: 0x0600A63C RID: 42556 RVA: 0x00255ED6 File Offset: 0x002540D6
			// (set) Token: 0x0600A63D RID: 42557 RVA: 0x00255EDE File Offset: 0x002540DE
			public ConversionRule result_inull { get; private set; }

			// Token: 0x17001D70 RID: 7536
			// (get) Token: 0x0600A63E RID: 42558 RVA: 0x00255EE7 File Offset: 0x002540E7
			// (set) Token: 0x0600A63F RID: 42559 RVA: 0x00255EEF File Offset: 0x002540EF
			public ConversionRule outNumber_number { get; private set; }

			// Token: 0x17001D71 RID: 7537
			// (get) Token: 0x0600A640 RID: 42560 RVA: 0x00255EF8 File Offset: 0x002540F8
			// (set) Token: 0x0600A641 RID: 42561 RVA: 0x00255F00 File Offset: 0x00254100
			public ConversionRule outNumber_constNumber { get; private set; }

			// Token: 0x17001D72 RID: 7538
			// (get) Token: 0x0600A642 RID: 42562 RVA: 0x00255F09 File Offset: 0x00254109
			// (set) Token: 0x0600A643 RID: 42563 RVA: 0x00255F11 File Offset: 0x00254111
			public ConversionRule outDate_date { get; private set; }

			// Token: 0x17001D73 RID: 7539
			// (get) Token: 0x0600A644 RID: 42564 RVA: 0x00255F1A File Offset: 0x0025411A
			// (set) Token: 0x0600A645 RID: 42565 RVA: 0x00255F22 File Offset: 0x00254122
			public ConversionRule outDate_constDate { get; private set; }

			// Token: 0x17001D74 RID: 7540
			// (get) Token: 0x0600A646 RID: 42566 RVA: 0x00255F2B File Offset: 0x0025412B
			// (set) Token: 0x0600A647 RID: 42567 RVA: 0x00255F33 File Offset: 0x00254133
			public ConversionRule outStr_outStr1 { get; private set; }

			// Token: 0x17001D75 RID: 7541
			// (get) Token: 0x0600A648 RID: 42568 RVA: 0x00255F3C File Offset: 0x0025413C
			// (set) Token: 0x0600A649 RID: 42569 RVA: 0x00255F44 File Offset: 0x00254144
			public ConversionRule outStr1_segmentCase { get; private set; }

			// Token: 0x17001D76 RID: 7542
			// (get) Token: 0x0600A64A RID: 42570 RVA: 0x00255F4D File Offset: 0x0025414D
			// (set) Token: 0x0600A64B RID: 42571 RVA: 0x00255F55 File Offset: 0x00254155
			public ConversionRule outStr1_formatted { get; private set; }

			// Token: 0x17001D77 RID: 7543
			// (get) Token: 0x0600A64C RID: 42572 RVA: 0x00255F5E File Offset: 0x0025415E
			// (set) Token: 0x0600A64D RID: 42573 RVA: 0x00255F66 File Offset: 0x00254166
			public ConversionRule outStr1_concatEntry { get; private set; }

			// Token: 0x17001D78 RID: 7544
			// (get) Token: 0x0600A64E RID: 42574 RVA: 0x00255F6F File Offset: 0x0025416F
			// (set) Token: 0x0600A64F RID: 42575 RVA: 0x00255F77 File Offset: 0x00254177
			public ConversionRule outStr1_constString { get; private set; }

			// Token: 0x17001D79 RID: 7545
			// (get) Token: 0x0600A650 RID: 42576 RVA: 0x00255F80 File Offset: 0x00254180
			// (set) Token: 0x0600A651 RID: 42577 RVA: 0x00255F88 File Offset: 0x00254188
			public ConversionRule segmentCase_segment { get; private set; }

			// Token: 0x17001D7A RID: 7546
			// (get) Token: 0x0600A652 RID: 42578 RVA: 0x00255F91 File Offset: 0x00254191
			// (set) Token: 0x0600A653 RID: 42579 RVA: 0x00255F99 File Offset: 0x00254199
			public ConversionRule segment_fromStrTrim { get; private set; }

			// Token: 0x17001D7B RID: 7547
			// (get) Token: 0x0600A654 RID: 42580 RVA: 0x00255FA2 File Offset: 0x002541A2
			// (set) Token: 0x0600A655 RID: 42581 RVA: 0x00255FAA File Offset: 0x002541AA
			public ConversionRule segment_letSubstring { get; private set; }

			// Token: 0x17001D7C RID: 7548
			// (get) Token: 0x0600A656 RID: 42582 RVA: 0x00255FB3 File Offset: 0x002541B3
			// (set) Token: 0x0600A657 RID: 42583 RVA: 0x00255FBB File Offset: 0x002541BB
			public ConversionRule formatted_formatNumber { get; private set; }

			// Token: 0x17001D7D RID: 7549
			// (get) Token: 0x0600A658 RID: 42584 RVA: 0x00255FC4 File Offset: 0x002541C4
			// (set) Token: 0x0600A659 RID: 42585 RVA: 0x00255FCC File Offset: 0x002541CC
			public ConversionRule formatted_formatDateTime { get; private set; }

			// Token: 0x17001D7E RID: 7550
			// (get) Token: 0x0600A65A RID: 42586 RVA: 0x00255FD5 File Offset: 0x002541D5
			// (set) Token: 0x0600A65B RID: 42587 RVA: 0x00255FDD File Offset: 0x002541DD
			public ConversionRule concatEntry_concatCase { get; private set; }

			// Token: 0x17001D7F RID: 7551
			// (get) Token: 0x0600A65C RID: 42588 RVA: 0x00255FE6 File Offset: 0x002541E6
			// (set) Token: 0x0600A65D RID: 42589 RVA: 0x00255FEE File Offset: 0x002541EE
			public ConversionRule concatEntry_constString { get; private set; }

			// Token: 0x17001D80 RID: 7552
			// (get) Token: 0x0600A65E RID: 42590 RVA: 0x00255FF7 File Offset: 0x002541F7
			// (set) Token: 0x0600A65F RID: 42591 RVA: 0x00255FFF File Offset: 0x002541FF
			public ConversionRule concatCase_concat { get; private set; }

			// Token: 0x17001D81 RID: 7553
			// (get) Token: 0x0600A660 RID: 42592 RVA: 0x00256008 File Offset: 0x00254208
			// (set) Token: 0x0600A661 RID: 42593 RVA: 0x00256010 File Offset: 0x00254210
			public ConversionRule concatPrefix_concatSegment { get; private set; }

			// Token: 0x17001D82 RID: 7554
			// (get) Token: 0x0600A662 RID: 42594 RVA: 0x00256019 File Offset: 0x00254219
			// (set) Token: 0x0600A663 RID: 42595 RVA: 0x00256021 File Offset: 0x00254221
			public ConversionRule concatPrefix_formatted { get; private set; }

			// Token: 0x17001D83 RID: 7555
			// (get) Token: 0x0600A664 RID: 42596 RVA: 0x0025602A File Offset: 0x0025422A
			// (set) Token: 0x0600A665 RID: 42597 RVA: 0x00256032 File Offset: 0x00254232
			public ConversionRule concatPrefix_constString { get; private set; }

			// Token: 0x17001D84 RID: 7556
			// (get) Token: 0x0600A666 RID: 42598 RVA: 0x0025603B File Offset: 0x0025423B
			// (set) Token: 0x0600A667 RID: 42599 RVA: 0x00256043 File Offset: 0x00254243
			public ConversionRule concatSegment_segment { get; private set; }

			// Token: 0x17001D85 RID: 7557
			// (get) Token: 0x0600A668 RID: 42600 RVA: 0x0025604C File Offset: 0x0025424C
			// (set) Token: 0x0600A669 RID: 42601 RVA: 0x00256054 File Offset: 0x00254254
			public ConversionRule concatSegment_segmentCase { get; private set; }

			// Token: 0x17001D86 RID: 7558
			// (get) Token: 0x0600A66A RID: 42602 RVA: 0x0025605D File Offset: 0x0025425D
			// (set) Token: 0x0600A66B RID: 42603 RVA: 0x00256065 File Offset: 0x00254265
			public ConversionRule concatSuffix_concatPrefix { get; private set; }

			// Token: 0x17001D87 RID: 7559
			// (get) Token: 0x0600A66C RID: 42604 RVA: 0x0025606E File Offset: 0x0025426E
			// (set) Token: 0x0600A66D RID: 42605 RVA: 0x00256076 File Offset: 0x00254276
			public ConversionRule concatSuffix_concat { get; private set; }

			// Token: 0x17001D88 RID: 7560
			// (get) Token: 0x0600A66E RID: 42606 RVA: 0x0025607F File Offset: 0x0025427F
			// (set) Token: 0x0600A66F RID: 42607 RVA: 0x00256087 File Offset: 0x00254287
			public ConversionRule condition_or { get; private set; }

			// Token: 0x17001D89 RID: 7561
			// (get) Token: 0x0600A670 RID: 42608 RVA: 0x00256090 File Offset: 0x00254290
			// (set) Token: 0x0600A671 RID: 42609 RVA: 0x00256098 File Offset: 0x00254298
			public ConversionRule number_number1 { get; private set; }

			// Token: 0x17001D8A RID: 7562
			// (get) Token: 0x0600A672 RID: 42610 RVA: 0x002560A1 File Offset: 0x002542A1
			// (set) Token: 0x0600A673 RID: 42611 RVA: 0x002560A9 File Offset: 0x002542A9
			public ConversionRule number_arithmetic { get; private set; }

			// Token: 0x17001D8B RID: 7563
			// (get) Token: 0x0600A674 RID: 42612 RVA: 0x002560B2 File Offset: 0x002542B2
			// (set) Token: 0x0600A675 RID: 42613 RVA: 0x002560BA File Offset: 0x002542BA
			public ConversionRule number_rowNumberTransform { get; private set; }

			// Token: 0x17001D8C RID: 7564
			// (get) Token: 0x0600A676 RID: 42614 RVA: 0x002560C3 File Offset: 0x002542C3
			// (set) Token: 0x0600A677 RID: 42615 RVA: 0x002560CB File Offset: 0x002542CB
			public ConversionRule number1_inumber { get; private set; }

			// Token: 0x17001D8D RID: 7565
			// (get) Token: 0x0600A678 RID: 42616 RVA: 0x002560D4 File Offset: 0x002542D4
			// (set) Token: 0x0600A679 RID: 42617 RVA: 0x002560DC File Offset: 0x002542DC
			public ConversionRule arithmeticLeft_fromNumberCoalesced { get; private set; }

			// Token: 0x17001D8E RID: 7566
			// (get) Token: 0x0600A67A RID: 42618 RVA: 0x002560E5 File Offset: 0x002542E5
			// (set) Token: 0x0600A67B RID: 42619 RVA: 0x002560ED File Offset: 0x002542ED
			public ConversionRule arithmeticLeft_inumber { get; private set; }

			// Token: 0x17001D8F RID: 7567
			// (get) Token: 0x0600A67C RID: 42620 RVA: 0x002560F6 File Offset: 0x002542F6
			// (set) Token: 0x0600A67D RID: 42621 RVA: 0x002560FE File Offset: 0x002542FE
			public ConversionRule addRight_arithmeticLeft { get; private set; }

			// Token: 0x17001D90 RID: 7568
			// (get) Token: 0x0600A67E RID: 42622 RVA: 0x00256107 File Offset: 0x00254307
			// (set) Token: 0x0600A67F RID: 42623 RVA: 0x0025610F File Offset: 0x0025430F
			public ConversionRule subtractRight_arithmeticLeft { get; private set; }

			// Token: 0x17001D91 RID: 7569
			// (get) Token: 0x0600A680 RID: 42624 RVA: 0x00256118 File Offset: 0x00254318
			// (set) Token: 0x0600A681 RID: 42625 RVA: 0x00256120 File Offset: 0x00254320
			public ConversionRule multiplyRight_arithmeticLeft { get; private set; }

			// Token: 0x17001D92 RID: 7570
			// (get) Token: 0x0600A682 RID: 42626 RVA: 0x00256129 File Offset: 0x00254329
			// (set) Token: 0x0600A683 RID: 42627 RVA: 0x00256131 File Offset: 0x00254331
			public ConversionRule divideRight_arithmeticLeft { get; private set; }

			// Token: 0x17001D93 RID: 7571
			// (get) Token: 0x0600A684 RID: 42628 RVA: 0x0025613A File Offset: 0x0025433A
			// (set) Token: 0x0600A685 RID: 42629 RVA: 0x00256142 File Offset: 0x00254342
			public ConversionRule inumber_fromNumber { get; private set; }

			// Token: 0x17001D94 RID: 7572
			// (get) Token: 0x0600A686 RID: 42630 RVA: 0x0025614B File Offset: 0x0025434B
			// (set) Token: 0x0600A687 RID: 42631 RVA: 0x00256153 File Offset: 0x00254353
			public ConversionRule rowNumberTransform_fromRowNumber { get; private set; }

			// Token: 0x17001D95 RID: 7573
			// (get) Token: 0x0600A688 RID: 42632 RVA: 0x0025615C File Offset: 0x0025435C
			// (set) Token: 0x0600A689 RID: 42633 RVA: 0x00256164 File Offset: 0x00254364
			public ConversionRule date_idate { get; private set; }

			// Token: 0x17001D96 RID: 7574
			// (get) Token: 0x0600A68A RID: 42634 RVA: 0x0025616D File Offset: 0x0025436D
			// (set) Token: 0x0600A68B RID: 42635 RVA: 0x00256175 File Offset: 0x00254375
			public ConversionRule idate_fromDateTime { get; private set; }

			// Token: 0x17001D97 RID: 7575
			// (get) Token: 0x0600A68C RID: 42636 RVA: 0x0025617E File Offset: 0x0025437E
			// (set) Token: 0x0600A68D RID: 42637 RVA: 0x00256186 File Offset: 0x00254386
			public ConversionRule idate_fromDateTimePart { get; private set; }

			// Token: 0x17001D98 RID: 7576
			// (get) Token: 0x0600A68E RID: 42638 RVA: 0x0025618F File Offset: 0x0025438F
			// (set) Token: 0x0600A68F RID: 42639 RVA: 0x00256197 File Offset: 0x00254397
			public ConversionRule itime_fromTime { get; private set; }

			// Token: 0x17001D99 RID: 7577
			// (get) Token: 0x0600A690 RID: 42640 RVA: 0x002561A0 File Offset: 0x002543A0
			// (set) Token: 0x0600A691 RID: 42641 RVA: 0x002561A8 File Offset: 0x002543A8
			public ConversionRule parseSubject_fromStr { get; private set; }

			// Token: 0x17001D9A RID: 7578
			// (get) Token: 0x0600A692 RID: 42642 RVA: 0x002561B1 File Offset: 0x002543B1
			// (set) Token: 0x0600A693 RID: 42643 RVA: 0x002561B9 File Offset: 0x002543B9
			public ConversionRule parseSubject_letSubstring { get; private set; }

			// Token: 0x17001D9B RID: 7579
			// (get) Token: 0x0600A694 RID: 42644 RVA: 0x002561C2 File Offset: 0x002543C2
			// (set) Token: 0x0600A695 RID: 42645 RVA: 0x002561CA File Offset: 0x002543CA
			public ConversionRule substring_splitTrim { get; private set; }

			// Token: 0x17001D9C RID: 7580
			// (get) Token: 0x0600A696 RID: 42646 RVA: 0x002561D3 File Offset: 0x002543D3
			// (set) Token: 0x0600A697 RID: 42647 RVA: 0x002561DB File Offset: 0x002543DB
			public ConversionRule substring_sliceTrim { get; private set; }

			// Token: 0x17001D9D RID: 7581
			// (get) Token: 0x0600A698 RID: 42648 RVA: 0x002561E4 File Offset: 0x002543E4
			// (set) Token: 0x0600A699 RID: 42649 RVA: 0x002561EC File Offset: 0x002543EC
			public ConversionRule splitTrim_split { get; private set; }

			// Token: 0x17001D9E RID: 7582
			// (get) Token: 0x0600A69A RID: 42650 RVA: 0x002561F5 File Offset: 0x002543F5
			// (set) Token: 0x0600A69B RID: 42651 RVA: 0x002561FD File Offset: 0x002543FD
			public ConversionRule sliceTrim_slice { get; private set; }

			// Token: 0x17001D9F RID: 7583
			// (get) Token: 0x0600A69C RID: 42652 RVA: 0x00256206 File Offset: 0x00254406
			// (set) Token: 0x0600A69D RID: 42653 RVA: 0x0025620E File Offset: 0x0025440E
			public ConversionRule fromStrTrim_fromStr { get; private set; }

			// Token: 0x17001DA0 RID: 7584
			// (get) Token: 0x0600A69E RID: 42654 RVA: 0x00256217 File Offset: 0x00254417
			// (set) Token: 0x0600A69F RID: 42655 RVA: 0x0025621F File Offset: 0x0025441F
			public ConversionRule fromStrTrim_fromNumberStr { get; private set; }

			// Token: 0x0600A6A0 RID: 42656 RVA: 0x00256228 File Offset: 0x00254428
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.result_output = (ConversionRule)grammar.Rule("~convert_result_output");
				this.result_inull = (ConversionRule)grammar.Rule("~convert_result_inull");
				this.outNumber_number = (ConversionRule)grammar.Rule("~convert_outNumber_number");
				this.outNumber_constNumber = (ConversionRule)grammar.Rule("~convert_outNumber_constNumber");
				this.outDate_date = (ConversionRule)grammar.Rule("~convert_outDate_date");
				this.outDate_constDate = (ConversionRule)grammar.Rule("~convert_outDate_constDate");
				this.outStr_outStr1 = (ConversionRule)grammar.Rule("~convert_outStr_outStr1");
				this.outStr1_segmentCase = (ConversionRule)grammar.Rule("~convert_outStr1_segmentCase");
				this.outStr1_formatted = (ConversionRule)grammar.Rule("~convert_outStr1_formatted");
				this.outStr1_concatEntry = (ConversionRule)grammar.Rule("~convert_outStr1_concatEntry");
				this.outStr1_constString = (ConversionRule)grammar.Rule("~convert_outStr1_constString");
				this.segmentCase_segment = (ConversionRule)grammar.Rule("~convert_segmentCase_segment");
				this.segment_fromStrTrim = (ConversionRule)grammar.Rule("~convert_segment_fromStrTrim");
				this.segment_letSubstring = (ConversionRule)grammar.Rule("~convert_segment_letSubstring");
				this.formatted_formatNumber = (ConversionRule)grammar.Rule("~convert_formatted_formatNumber");
				this.formatted_formatDateTime = (ConversionRule)grammar.Rule("~convert_formatted_formatDateTime");
				this.concatEntry_concatCase = (ConversionRule)grammar.Rule("~convert_concatEntry_concatCase");
				this.concatEntry_constString = (ConversionRule)grammar.Rule("~convert_concatEntry_constString");
				this.concatCase_concat = (ConversionRule)grammar.Rule("~convert_concatCase_concat");
				this.concatPrefix_concatSegment = (ConversionRule)grammar.Rule("~convert_concatPrefix_concatSegment");
				this.concatPrefix_formatted = (ConversionRule)grammar.Rule("~convert_concatPrefix_formatted");
				this.concatPrefix_constString = (ConversionRule)grammar.Rule("~convert_concatPrefix_constString");
				this.concatSegment_segment = (ConversionRule)grammar.Rule("~convert_concatSegment_segment");
				this.concatSegment_segmentCase = (ConversionRule)grammar.Rule("~convert_concatSegment_segmentCase");
				this.concatSuffix_concatPrefix = (ConversionRule)grammar.Rule("~convert_concatSuffix_concatPrefix");
				this.concatSuffix_concat = (ConversionRule)grammar.Rule("~convert_concatSuffix_concat");
				this.condition_or = (ConversionRule)grammar.Rule("~convert_condition_or");
				this.number_number1 = (ConversionRule)grammar.Rule("~convert_number_number1");
				this.number_arithmetic = (ConversionRule)grammar.Rule("~convert_number_arithmetic");
				this.number_rowNumberTransform = (ConversionRule)grammar.Rule("~convert_number_rowNumberTransform");
				this.number1_inumber = (ConversionRule)grammar.Rule("~convert_number1_inumber");
				this.arithmeticLeft_fromNumberCoalesced = (ConversionRule)grammar.Rule("~convert_arithmeticLeft_fromNumberCoalesced");
				this.arithmeticLeft_inumber = (ConversionRule)grammar.Rule("~convert_arithmeticLeft_inumber");
				this.addRight_arithmeticLeft = (ConversionRule)grammar.Rule("~convert_addRight_arithmeticLeft");
				this.subtractRight_arithmeticLeft = (ConversionRule)grammar.Rule("~convert_subtractRight_arithmeticLeft");
				this.multiplyRight_arithmeticLeft = (ConversionRule)grammar.Rule("~convert_multiplyRight_arithmeticLeft");
				this.divideRight_arithmeticLeft = (ConversionRule)grammar.Rule("~convert_divideRight_arithmeticLeft");
				this.inumber_fromNumber = (ConversionRule)grammar.Rule("~convert_inumber_fromNumber");
				this.rowNumberTransform_fromRowNumber = (ConversionRule)grammar.Rule("~convert_rowNumberTransform_fromRowNumber");
				this.date_idate = (ConversionRule)grammar.Rule("~convert_date_idate");
				this.idate_fromDateTime = (ConversionRule)grammar.Rule("~convert_idate_fromDateTime");
				this.idate_fromDateTimePart = (ConversionRule)grammar.Rule("~convert_idate_fromDateTimePart");
				this.itime_fromTime = (ConversionRule)grammar.Rule("~convert_itime_fromTime");
				this.parseSubject_fromStr = (ConversionRule)grammar.Rule("~convert_parseSubject_fromStr");
				this.parseSubject_letSubstring = (ConversionRule)grammar.Rule("~convert_parseSubject_letSubstring");
				this.substring_splitTrim = (ConversionRule)grammar.Rule("~convert_substring_splitTrim");
				this.substring_sliceTrim = (ConversionRule)grammar.Rule("~convert_substring_sliceTrim");
				this.splitTrim_split = (ConversionRule)grammar.Rule("~convert_splitTrim_split");
				this.sliceTrim_slice = (ConversionRule)grammar.Rule("~convert_sliceTrim_slice");
				this.fromStrTrim_fromStr = (ConversionRule)grammar.Rule("~convert_fromStrTrim_fromStr");
				this.fromStrTrim_fromNumberStr = (ConversionRule)grammar.Rule("~convert_fromStrTrim_fromNumberStr");
			}
		}

		// Token: 0x020014FC RID: 5372
		public class GrammarHoles
		{
			// Token: 0x17001DA1 RID: 7585
			// (get) Token: 0x0600A6A1 RID: 42657 RVA: 0x0025669D File Offset: 0x0025489D
			// (set) Token: 0x0600A6A2 RID: 42658 RVA: 0x002566A5 File Offset: 0x002548A5
			public Hole row { get; private set; }

			// Token: 0x17001DA2 RID: 7586
			// (get) Token: 0x0600A6A3 RID: 42659 RVA: 0x002566AE File Offset: 0x002548AE
			// (set) Token: 0x0600A6A4 RID: 42660 RVA: 0x002566B6 File Offset: 0x002548B6
			public Hole result { get; private set; }

			// Token: 0x17001DA3 RID: 7587
			// (get) Token: 0x0600A6A5 RID: 42661 RVA: 0x002566BF File Offset: 0x002548BF
			// (set) Token: 0x0600A6A6 RID: 42662 RVA: 0x002566C7 File Offset: 0x002548C7
			public Hole output { get; private set; }

			// Token: 0x17001DA4 RID: 7588
			// (get) Token: 0x0600A6A7 RID: 42663 RVA: 0x002566D0 File Offset: 0x002548D0
			// (set) Token: 0x0600A6A8 RID: 42664 RVA: 0x002566D8 File Offset: 0x002548D8
			public Hole outNumber { get; private set; }

			// Token: 0x17001DA5 RID: 7589
			// (get) Token: 0x0600A6A9 RID: 42665 RVA: 0x002566E1 File Offset: 0x002548E1
			// (set) Token: 0x0600A6AA RID: 42666 RVA: 0x002566E9 File Offset: 0x002548E9
			public Hole outDate { get; private set; }

			// Token: 0x17001DA6 RID: 7590
			// (get) Token: 0x0600A6AB RID: 42667 RVA: 0x002566F2 File Offset: 0x002548F2
			// (set) Token: 0x0600A6AC RID: 42668 RVA: 0x002566FA File Offset: 0x002548FA
			public Hole outStr { get; private set; }

			// Token: 0x17001DA7 RID: 7591
			// (get) Token: 0x0600A6AD RID: 42669 RVA: 0x00256703 File Offset: 0x00254903
			// (set) Token: 0x0600A6AE RID: 42670 RVA: 0x0025670B File Offset: 0x0025490B
			public Hole outStr1 { get; private set; }

			// Token: 0x17001DA8 RID: 7592
			// (get) Token: 0x0600A6AF RID: 42671 RVA: 0x00256714 File Offset: 0x00254914
			// (set) Token: 0x0600A6B0 RID: 42672 RVA: 0x0025671C File Offset: 0x0025491C
			public Hole segmentCase { get; private set; }

			// Token: 0x17001DA9 RID: 7593
			// (get) Token: 0x0600A6B1 RID: 42673 RVA: 0x00256725 File Offset: 0x00254925
			// (set) Token: 0x0600A6B2 RID: 42674 RVA: 0x0025672D File Offset: 0x0025492D
			public Hole segment { get; private set; }

			// Token: 0x17001DAA RID: 7594
			// (get) Token: 0x0600A6B3 RID: 42675 RVA: 0x00256736 File Offset: 0x00254936
			// (set) Token: 0x0600A6B4 RID: 42676 RVA: 0x0025673E File Offset: 0x0025493E
			public Hole formatted { get; private set; }

			// Token: 0x17001DAB RID: 7595
			// (get) Token: 0x0600A6B5 RID: 42677 RVA: 0x00256747 File Offset: 0x00254947
			// (set) Token: 0x0600A6B6 RID: 42678 RVA: 0x0025674F File Offset: 0x0025494F
			public Hole concatEntry { get; private set; }

			// Token: 0x17001DAC RID: 7596
			// (get) Token: 0x0600A6B7 RID: 42679 RVA: 0x00256758 File Offset: 0x00254958
			// (set) Token: 0x0600A6B8 RID: 42680 RVA: 0x00256760 File Offset: 0x00254960
			public Hole concatCase { get; private set; }

			// Token: 0x17001DAD RID: 7597
			// (get) Token: 0x0600A6B9 RID: 42681 RVA: 0x00256769 File Offset: 0x00254969
			// (set) Token: 0x0600A6BA RID: 42682 RVA: 0x00256771 File Offset: 0x00254971
			public Hole concat { get; private set; }

			// Token: 0x17001DAE RID: 7598
			// (get) Token: 0x0600A6BB RID: 42683 RVA: 0x0025677A File Offset: 0x0025497A
			// (set) Token: 0x0600A6BC RID: 42684 RVA: 0x00256782 File Offset: 0x00254982
			public Hole concatPrefix { get; private set; }

			// Token: 0x17001DAF RID: 7599
			// (get) Token: 0x0600A6BD RID: 42685 RVA: 0x0025678B File Offset: 0x0025498B
			// (set) Token: 0x0600A6BE RID: 42686 RVA: 0x00256793 File Offset: 0x00254993
			public Hole concatSegment { get; private set; }

			// Token: 0x17001DB0 RID: 7600
			// (get) Token: 0x0600A6BF RID: 42687 RVA: 0x0025679C File Offset: 0x0025499C
			// (set) Token: 0x0600A6C0 RID: 42688 RVA: 0x002567A4 File Offset: 0x002549A4
			public Hole concatSuffix { get; private set; }

			// Token: 0x17001DB1 RID: 7601
			// (get) Token: 0x0600A6C1 RID: 42689 RVA: 0x002567AD File Offset: 0x002549AD
			// (set) Token: 0x0600A6C2 RID: 42690 RVA: 0x002567B5 File Offset: 0x002549B5
			public Hole condition { get; private set; }

			// Token: 0x17001DB2 RID: 7602
			// (get) Token: 0x0600A6C3 RID: 42691 RVA: 0x002567BE File Offset: 0x002549BE
			// (set) Token: 0x0600A6C4 RID: 42692 RVA: 0x002567C6 File Offset: 0x002549C6
			public Hole or { get; private set; }

			// Token: 0x17001DB3 RID: 7603
			// (get) Token: 0x0600A6C5 RID: 42693 RVA: 0x002567CF File Offset: 0x002549CF
			// (set) Token: 0x0600A6C6 RID: 42694 RVA: 0x002567D7 File Offset: 0x002549D7
			public Hole inull { get; private set; }

			// Token: 0x17001DB4 RID: 7604
			// (get) Token: 0x0600A6C7 RID: 42695 RVA: 0x002567E0 File Offset: 0x002549E0
			// (set) Token: 0x0600A6C8 RID: 42696 RVA: 0x002567E8 File Offset: 0x002549E8
			public Hole equalsText { get; private set; }

			// Token: 0x17001DB5 RID: 7605
			// (get) Token: 0x0600A6C9 RID: 42697 RVA: 0x002567F1 File Offset: 0x002549F1
			// (set) Token: 0x0600A6CA RID: 42698 RVA: 0x002567F9 File Offset: 0x002549F9
			public Hole containsFindText { get; private set; }

			// Token: 0x17001DB6 RID: 7606
			// (get) Token: 0x0600A6CB RID: 42699 RVA: 0x00256802 File Offset: 0x00254A02
			// (set) Token: 0x0600A6CC RID: 42700 RVA: 0x0025680A File Offset: 0x00254A0A
			public Hole startsWithFindText { get; private set; }

			// Token: 0x17001DB7 RID: 7607
			// (get) Token: 0x0600A6CD RID: 42701 RVA: 0x00256813 File Offset: 0x00254A13
			// (set) Token: 0x0600A6CE RID: 42702 RVA: 0x0025681B File Offset: 0x00254A1B
			public Hole isMatchRegex { get; private set; }

			// Token: 0x17001DB8 RID: 7608
			// (get) Token: 0x0600A6CF RID: 42703 RVA: 0x00256824 File Offset: 0x00254A24
			// (set) Token: 0x0600A6D0 RID: 42704 RVA: 0x0025682C File Offset: 0x00254A2C
			public Hole containsMatchRegex { get; private set; }

			// Token: 0x17001DB9 RID: 7609
			// (get) Token: 0x0600A6D1 RID: 42705 RVA: 0x00256835 File Offset: 0x00254A35
			// (set) Token: 0x0600A6D2 RID: 42706 RVA: 0x0025683D File Offset: 0x00254A3D
			public Hole containsCount { get; private set; }

			// Token: 0x17001DBA RID: 7610
			// (get) Token: 0x0600A6D3 RID: 42707 RVA: 0x00256846 File Offset: 0x00254A46
			// (set) Token: 0x0600A6D4 RID: 42708 RVA: 0x0025684E File Offset: 0x00254A4E
			public Hole matchCount { get; private set; }

			// Token: 0x17001DBB RID: 7611
			// (get) Token: 0x0600A6D5 RID: 42709 RVA: 0x00256857 File Offset: 0x00254A57
			// (set) Token: 0x0600A6D6 RID: 42710 RVA: 0x0025685F File Offset: 0x00254A5F
			public Hole numberEqualsValue { get; private set; }

			// Token: 0x17001DBC RID: 7612
			// (get) Token: 0x0600A6D7 RID: 42711 RVA: 0x00256868 File Offset: 0x00254A68
			// (set) Token: 0x0600A6D8 RID: 42712 RVA: 0x00256870 File Offset: 0x00254A70
			public Hole numberGreaterThanValue { get; private set; }

			// Token: 0x17001DBD RID: 7613
			// (get) Token: 0x0600A6D9 RID: 42713 RVA: 0x00256879 File Offset: 0x00254A79
			// (set) Token: 0x0600A6DA RID: 42714 RVA: 0x00256881 File Offset: 0x00254A81
			public Hole numberLessThanValue { get; private set; }

			// Token: 0x17001DBE RID: 7614
			// (get) Token: 0x0600A6DB RID: 42715 RVA: 0x0025688A File Offset: 0x00254A8A
			// (set) Token: 0x0600A6DC RID: 42716 RVA: 0x00256892 File Offset: 0x00254A92
			public Hole formatNumber { get; private set; }

			// Token: 0x17001DBF RID: 7615
			// (get) Token: 0x0600A6DD RID: 42717 RVA: 0x0025689B File Offset: 0x00254A9B
			// (set) Token: 0x0600A6DE RID: 42718 RVA: 0x002568A3 File Offset: 0x00254AA3
			public Hole number { get; private set; }

			// Token: 0x17001DC0 RID: 7616
			// (get) Token: 0x0600A6DF RID: 42719 RVA: 0x002568AC File Offset: 0x00254AAC
			// (set) Token: 0x0600A6E0 RID: 42720 RVA: 0x002568B4 File Offset: 0x00254AB4
			public Hole number1 { get; private set; }

			// Token: 0x17001DC1 RID: 7617
			// (get) Token: 0x0600A6E1 RID: 42721 RVA: 0x002568BD File Offset: 0x00254ABD
			// (set) Token: 0x0600A6E2 RID: 42722 RVA: 0x002568C5 File Offset: 0x00254AC5
			public Hole arithmetic { get; private set; }

			// Token: 0x17001DC2 RID: 7618
			// (get) Token: 0x0600A6E3 RID: 42723 RVA: 0x002568CE File Offset: 0x00254ACE
			// (set) Token: 0x0600A6E4 RID: 42724 RVA: 0x002568D6 File Offset: 0x00254AD6
			public Hole arithmeticLeft { get; private set; }

			// Token: 0x17001DC3 RID: 7619
			// (get) Token: 0x0600A6E5 RID: 42725 RVA: 0x002568DF File Offset: 0x00254ADF
			// (set) Token: 0x0600A6E6 RID: 42726 RVA: 0x002568E7 File Offset: 0x00254AE7
			public Hole addRight { get; private set; }

			// Token: 0x17001DC4 RID: 7620
			// (get) Token: 0x0600A6E7 RID: 42727 RVA: 0x002568F0 File Offset: 0x00254AF0
			// (set) Token: 0x0600A6E8 RID: 42728 RVA: 0x002568F8 File Offset: 0x00254AF8
			public Hole subtractRight { get; private set; }

			// Token: 0x17001DC5 RID: 7621
			// (get) Token: 0x0600A6E9 RID: 42729 RVA: 0x00256901 File Offset: 0x00254B01
			// (set) Token: 0x0600A6EA RID: 42730 RVA: 0x00256909 File Offset: 0x00254B09
			public Hole multiplyRight { get; private set; }

			// Token: 0x17001DC6 RID: 7622
			// (get) Token: 0x0600A6EB RID: 42731 RVA: 0x00256912 File Offset: 0x00254B12
			// (set) Token: 0x0600A6EC RID: 42732 RVA: 0x0025691A File Offset: 0x00254B1A
			public Hole divideRight { get; private set; }

			// Token: 0x17001DC7 RID: 7623
			// (get) Token: 0x0600A6ED RID: 42733 RVA: 0x00256923 File Offset: 0x00254B23
			// (set) Token: 0x0600A6EE RID: 42734 RVA: 0x0025692B File Offset: 0x00254B2B
			public Hole inumber { get; private set; }

			// Token: 0x17001DC8 RID: 7624
			// (get) Token: 0x0600A6EF RID: 42735 RVA: 0x00256934 File Offset: 0x00254B34
			// (set) Token: 0x0600A6F0 RID: 42736 RVA: 0x0025693C File Offset: 0x00254B3C
			public Hole rowNumberTransform { get; private set; }

			// Token: 0x17001DC9 RID: 7625
			// (get) Token: 0x0600A6F1 RID: 42737 RVA: 0x00256945 File Offset: 0x00254B45
			// (set) Token: 0x0600A6F2 RID: 42738 RVA: 0x0025694D File Offset: 0x00254B4D
			public Hole formatDateTime { get; private set; }

			// Token: 0x17001DCA RID: 7626
			// (get) Token: 0x0600A6F3 RID: 42739 RVA: 0x00256956 File Offset: 0x00254B56
			// (set) Token: 0x0600A6F4 RID: 42740 RVA: 0x0025695E File Offset: 0x00254B5E
			public Hole date { get; private set; }

			// Token: 0x17001DCB RID: 7627
			// (get) Token: 0x0600A6F5 RID: 42741 RVA: 0x00256967 File Offset: 0x00254B67
			// (set) Token: 0x0600A6F6 RID: 42742 RVA: 0x0025696F File Offset: 0x00254B6F
			public Hole idate { get; private set; }

			// Token: 0x17001DCC RID: 7628
			// (get) Token: 0x0600A6F7 RID: 42743 RVA: 0x00256978 File Offset: 0x00254B78
			// (set) Token: 0x0600A6F8 RID: 42744 RVA: 0x00256980 File Offset: 0x00254B80
			public Hole itime { get; private set; }

			// Token: 0x17001DCD RID: 7629
			// (get) Token: 0x0600A6F9 RID: 42745 RVA: 0x00256989 File Offset: 0x00254B89
			// (set) Token: 0x0600A6FA RID: 42746 RVA: 0x00256991 File Offset: 0x00254B91
			public Hole parseSubject { get; private set; }

			// Token: 0x17001DCE RID: 7630
			// (get) Token: 0x0600A6FB RID: 42747 RVA: 0x0025699A File Offset: 0x00254B9A
			// (set) Token: 0x0600A6FC RID: 42748 RVA: 0x002569A2 File Offset: 0x00254BA2
			public Hole letSubstring { get; private set; }

			// Token: 0x17001DCF RID: 7631
			// (get) Token: 0x0600A6FD RID: 42749 RVA: 0x002569AB File Offset: 0x00254BAB
			// (set) Token: 0x0600A6FE RID: 42750 RVA: 0x002569B3 File Offset: 0x00254BB3
			public Hole x { get; private set; }

			// Token: 0x17001DD0 RID: 7632
			// (get) Token: 0x0600A6FF RID: 42751 RVA: 0x002569BC File Offset: 0x00254BBC
			// (set) Token: 0x0600A700 RID: 42752 RVA: 0x002569C4 File Offset: 0x00254BC4
			public Hole substring { get; private set; }

			// Token: 0x17001DD1 RID: 7633
			// (get) Token: 0x0600A701 RID: 42753 RVA: 0x002569CD File Offset: 0x00254BCD
			// (set) Token: 0x0600A702 RID: 42754 RVA: 0x002569D5 File Offset: 0x00254BD5
			public Hole splitTrim { get; private set; }

			// Token: 0x17001DD2 RID: 7634
			// (get) Token: 0x0600A703 RID: 42755 RVA: 0x002569DE File Offset: 0x00254BDE
			// (set) Token: 0x0600A704 RID: 42756 RVA: 0x002569E6 File Offset: 0x00254BE6
			public Hole split { get; private set; }

			// Token: 0x17001DD3 RID: 7635
			// (get) Token: 0x0600A705 RID: 42757 RVA: 0x002569EF File Offset: 0x00254BEF
			// (set) Token: 0x0600A706 RID: 42758 RVA: 0x002569F7 File Offset: 0x00254BF7
			public Hole sliceTrim { get; private set; }

			// Token: 0x17001DD4 RID: 7636
			// (get) Token: 0x0600A707 RID: 42759 RVA: 0x00256A00 File Offset: 0x00254C00
			// (set) Token: 0x0600A708 RID: 42760 RVA: 0x00256A08 File Offset: 0x00254C08
			public Hole slice { get; private set; }

			// Token: 0x17001DD5 RID: 7637
			// (get) Token: 0x0600A709 RID: 42761 RVA: 0x00256A11 File Offset: 0x00254C11
			// (set) Token: 0x0600A70A RID: 42762 RVA: 0x00256A19 File Offset: 0x00254C19
			public Hole pos { get; private set; }

			// Token: 0x17001DD6 RID: 7638
			// (get) Token: 0x0600A70B RID: 42763 RVA: 0x00256A22 File Offset: 0x00254C22
			// (set) Token: 0x0600A70C RID: 42764 RVA: 0x00256A2A File Offset: 0x00254C2A
			public Hole fromStrTrim { get; private set; }

			// Token: 0x17001DD7 RID: 7639
			// (get) Token: 0x0600A70D RID: 42765 RVA: 0x00256A33 File Offset: 0x00254C33
			// (set) Token: 0x0600A70E RID: 42766 RVA: 0x00256A3B File Offset: 0x00254C3B
			public Hole fromStr { get; private set; }

			// Token: 0x17001DD8 RID: 7640
			// (get) Token: 0x0600A70F RID: 42767 RVA: 0x00256A44 File Offset: 0x00254C44
			// (set) Token: 0x0600A710 RID: 42768 RVA: 0x00256A4C File Offset: 0x00254C4C
			public Hole fromNumberStr { get; private set; }

			// Token: 0x17001DD9 RID: 7641
			// (get) Token: 0x0600A711 RID: 42769 RVA: 0x00256A55 File Offset: 0x00254C55
			// (set) Token: 0x0600A712 RID: 42770 RVA: 0x00256A5D File Offset: 0x00254C5D
			public Hole fromNumber { get; private set; }

			// Token: 0x17001DDA RID: 7642
			// (get) Token: 0x0600A713 RID: 42771 RVA: 0x00256A66 File Offset: 0x00254C66
			// (set) Token: 0x0600A714 RID: 42772 RVA: 0x00256A6E File Offset: 0x00254C6E
			public Hole fromNumberCoalesced { get; private set; }

			// Token: 0x17001DDB RID: 7643
			// (get) Token: 0x0600A715 RID: 42773 RVA: 0x00256A77 File Offset: 0x00254C77
			// (set) Token: 0x0600A716 RID: 42774 RVA: 0x00256A7F File Offset: 0x00254C7F
			public Hole fromRowNumber { get; private set; }

			// Token: 0x17001DDC RID: 7644
			// (get) Token: 0x0600A717 RID: 42775 RVA: 0x00256A88 File Offset: 0x00254C88
			// (set) Token: 0x0600A718 RID: 42776 RVA: 0x00256A90 File Offset: 0x00254C90
			public Hole fromNumbers { get; private set; }

			// Token: 0x17001DDD RID: 7645
			// (get) Token: 0x0600A719 RID: 42777 RVA: 0x00256A99 File Offset: 0x00254C99
			// (set) Token: 0x0600A71A RID: 42778 RVA: 0x00256AA1 File Offset: 0x00254CA1
			public Hole fromDateTime { get; private set; }

			// Token: 0x17001DDE RID: 7646
			// (get) Token: 0x0600A71B RID: 42779 RVA: 0x00256AAA File Offset: 0x00254CAA
			// (set) Token: 0x0600A71C RID: 42780 RVA: 0x00256AB2 File Offset: 0x00254CB2
			public Hole fromDateTimePart { get; private set; }

			// Token: 0x17001DDF RID: 7647
			// (get) Token: 0x0600A71D RID: 42781 RVA: 0x00256ABB File Offset: 0x00254CBB
			// (set) Token: 0x0600A71E RID: 42782 RVA: 0x00256AC3 File Offset: 0x00254CC3
			public Hole fromTime { get; private set; }

			// Token: 0x17001DE0 RID: 7648
			// (get) Token: 0x0600A71F RID: 42783 RVA: 0x00256ACC File Offset: 0x00254CCC
			// (set) Token: 0x0600A720 RID: 42784 RVA: 0x00256AD4 File Offset: 0x00254CD4
			public Hole constString { get; private set; }

			// Token: 0x17001DE1 RID: 7649
			// (get) Token: 0x0600A721 RID: 42785 RVA: 0x00256ADD File Offset: 0x00254CDD
			// (set) Token: 0x0600A722 RID: 42786 RVA: 0x00256AE5 File Offset: 0x00254CE5
			public Hole constNumber { get; private set; }

			// Token: 0x17001DE2 RID: 7650
			// (get) Token: 0x0600A723 RID: 42787 RVA: 0x00256AEE File Offset: 0x00254CEE
			// (set) Token: 0x0600A724 RID: 42788 RVA: 0x00256AF6 File Offset: 0x00254CF6
			public Hole constDate { get; private set; }

			// Token: 0x17001DE3 RID: 7651
			// (get) Token: 0x0600A725 RID: 42789 RVA: 0x00256AFF File Offset: 0x00254CFF
			// (set) Token: 0x0600A726 RID: 42790 RVA: 0x00256B07 File Offset: 0x00254D07
			public Hole columnName { get; private set; }

			// Token: 0x17001DE4 RID: 7652
			// (get) Token: 0x0600A727 RID: 42791 RVA: 0x00256B10 File Offset: 0x00254D10
			// (set) Token: 0x0600A728 RID: 42792 RVA: 0x00256B18 File Offset: 0x00254D18
			public Hole columnNames { get; private set; }

			// Token: 0x17001DE5 RID: 7653
			// (get) Token: 0x0600A729 RID: 42793 RVA: 0x00256B21 File Offset: 0x00254D21
			// (set) Token: 0x0600A72A RID: 42794 RVA: 0x00256B29 File Offset: 0x00254D29
			public Hole constStr { get; private set; }

			// Token: 0x17001DE6 RID: 7654
			// (get) Token: 0x0600A72B RID: 42795 RVA: 0x00256B32 File Offset: 0x00254D32
			// (set) Token: 0x0600A72C RID: 42796 RVA: 0x00256B3A File Offset: 0x00254D3A
			public Hole constNum { get; private set; }

			// Token: 0x17001DE7 RID: 7655
			// (get) Token: 0x0600A72D RID: 42797 RVA: 0x00256B43 File Offset: 0x00254D43
			// (set) Token: 0x0600A72E RID: 42798 RVA: 0x00256B4B File Offset: 0x00254D4B
			public Hole constDt { get; private set; }

			// Token: 0x17001DE8 RID: 7656
			// (get) Token: 0x0600A72F RID: 42799 RVA: 0x00256B54 File Offset: 0x00254D54
			// (set) Token: 0x0600A730 RID: 42800 RVA: 0x00256B5C File Offset: 0x00254D5C
			public Hole locale { get; private set; }

			// Token: 0x17001DE9 RID: 7657
			// (get) Token: 0x0600A731 RID: 42801 RVA: 0x00256B65 File Offset: 0x00254D65
			// (set) Token: 0x0600A732 RID: 42802 RVA: 0x00256B6D File Offset: 0x00254D6D
			public Hole replaceFindText { get; private set; }

			// Token: 0x17001DEA RID: 7658
			// (get) Token: 0x0600A733 RID: 42803 RVA: 0x00256B76 File Offset: 0x00254D76
			// (set) Token: 0x0600A734 RID: 42804 RVA: 0x00256B7E File Offset: 0x00254D7E
			public Hole replaceText { get; private set; }

			// Token: 0x17001DEB RID: 7659
			// (get) Token: 0x0600A735 RID: 42805 RVA: 0x00256B87 File Offset: 0x00254D87
			// (set) Token: 0x0600A736 RID: 42806 RVA: 0x00256B8F File Offset: 0x00254D8F
			public Hole sliceBetweenStartText { get; private set; }

			// Token: 0x17001DEC RID: 7660
			// (get) Token: 0x0600A737 RID: 42807 RVA: 0x00256B98 File Offset: 0x00254D98
			// (set) Token: 0x0600A738 RID: 42808 RVA: 0x00256BA0 File Offset: 0x00254DA0
			public Hole sliceBetweenEndText { get; private set; }

			// Token: 0x17001DED RID: 7661
			// (get) Token: 0x0600A739 RID: 42809 RVA: 0x00256BA9 File Offset: 0x00254DA9
			// (set) Token: 0x0600A73A RID: 42810 RVA: 0x00256BB1 File Offset: 0x00254DB1
			public Hole numberFormatDesc { get; private set; }

			// Token: 0x17001DEE RID: 7662
			// (get) Token: 0x0600A73B RID: 42811 RVA: 0x00256BBA File Offset: 0x00254DBA
			// (set) Token: 0x0600A73C RID: 42812 RVA: 0x00256BC2 File Offset: 0x00254DC2
			public Hole numberRoundDesc { get; private set; }

			// Token: 0x17001DEF RID: 7663
			// (get) Token: 0x0600A73D RID: 42813 RVA: 0x00256BCB File Offset: 0x00254DCB
			// (set) Token: 0x0600A73E RID: 42814 RVA: 0x00256BD3 File Offset: 0x00254DD3
			public Hole dateTimeRoundDesc { get; private set; }

			// Token: 0x17001DF0 RID: 7664
			// (get) Token: 0x0600A73F RID: 42815 RVA: 0x00256BDC File Offset: 0x00254DDC
			// (set) Token: 0x0600A740 RID: 42816 RVA: 0x00256BE4 File Offset: 0x00254DE4
			public Hole dateTimeFormatDesc { get; private set; }

			// Token: 0x17001DF1 RID: 7665
			// (get) Token: 0x0600A741 RID: 42817 RVA: 0x00256BED File Offset: 0x00254DED
			// (set) Token: 0x0600A742 RID: 42818 RVA: 0x00256BF5 File Offset: 0x00254DF5
			public Hole dateTimeParseDesc { get; private set; }

			// Token: 0x17001DF2 RID: 7666
			// (get) Token: 0x0600A743 RID: 42819 RVA: 0x00256BFE File Offset: 0x00254DFE
			// (set) Token: 0x0600A744 RID: 42820 RVA: 0x00256C06 File Offset: 0x00254E06
			public Hole dateTimePartKind { get; private set; }

			// Token: 0x17001DF3 RID: 7667
			// (get) Token: 0x0600A745 RID: 42821 RVA: 0x00256C0F File Offset: 0x00254E0F
			// (set) Token: 0x0600A746 RID: 42822 RVA: 0x00256C17 File Offset: 0x00254E17
			public Hole fromDateTimePartKind { get; private set; }

			// Token: 0x17001DF4 RID: 7668
			// (get) Token: 0x0600A747 RID: 42823 RVA: 0x00256C20 File Offset: 0x00254E20
			// (set) Token: 0x0600A748 RID: 42824 RVA: 0x00256C28 File Offset: 0x00254E28
			public Hole timePartKind { get; private set; }

			// Token: 0x17001DF5 RID: 7669
			// (get) Token: 0x0600A749 RID: 42825 RVA: 0x00256C31 File Offset: 0x00254E31
			// (set) Token: 0x0600A74A RID: 42826 RVA: 0x00256C39 File Offset: 0x00254E39
			public Hole rowNumberLinearTransformDesc { get; private set; }

			// Token: 0x17001DF6 RID: 7670
			// (get) Token: 0x0600A74B RID: 42827 RVA: 0x00256C42 File Offset: 0x00254E42
			// (set) Token: 0x0600A74C RID: 42828 RVA: 0x00256C4A File Offset: 0x00254E4A
			public Hole matchDesc { get; private set; }

			// Token: 0x17001DF7 RID: 7671
			// (get) Token: 0x0600A74D RID: 42829 RVA: 0x00256C53 File Offset: 0x00254E53
			// (set) Token: 0x0600A74E RID: 42830 RVA: 0x00256C5B File Offset: 0x00254E5B
			public Hole matchInstance { get; private set; }

			// Token: 0x17001DF8 RID: 7672
			// (get) Token: 0x0600A74F RID: 42831 RVA: 0x00256C64 File Offset: 0x00254E64
			// (set) Token: 0x0600A750 RID: 42832 RVA: 0x00256C6C File Offset: 0x00254E6C
			public Hole splitDelimiter { get; private set; }

			// Token: 0x17001DF9 RID: 7673
			// (get) Token: 0x0600A751 RID: 42833 RVA: 0x00256C75 File Offset: 0x00254E75
			// (set) Token: 0x0600A752 RID: 42834 RVA: 0x00256C7D File Offset: 0x00254E7D
			public Hole splitInstance { get; private set; }

			// Token: 0x17001DFA RID: 7674
			// (get) Token: 0x0600A753 RID: 42835 RVA: 0x00256C86 File Offset: 0x00254E86
			// (set) Token: 0x0600A754 RID: 42836 RVA: 0x00256C8E File Offset: 0x00254E8E
			public Hole findDelimiter { get; private set; }

			// Token: 0x17001DFB RID: 7675
			// (get) Token: 0x0600A755 RID: 42837 RVA: 0x00256C97 File Offset: 0x00254E97
			// (set) Token: 0x0600A756 RID: 42838 RVA: 0x00256C9F File Offset: 0x00254E9F
			public Hole findInstance { get; private set; }

			// Token: 0x17001DFC RID: 7676
			// (get) Token: 0x0600A757 RID: 42839 RVA: 0x00256CA8 File Offset: 0x00254EA8
			// (set) Token: 0x0600A758 RID: 42840 RVA: 0x00256CB0 File Offset: 0x00254EB0
			public Hole findOffset { get; private set; }

			// Token: 0x17001DFD RID: 7677
			// (get) Token: 0x0600A759 RID: 42841 RVA: 0x00256CB9 File Offset: 0x00254EB9
			// (set) Token: 0x0600A75A RID: 42842 RVA: 0x00256CC1 File Offset: 0x00254EC1
			public Hole slicePrefixAbsPos { get; private set; }

			// Token: 0x17001DFE RID: 7678
			// (get) Token: 0x0600A75B RID: 42843 RVA: 0x00256CCA File Offset: 0x00254ECA
			// (set) Token: 0x0600A75C RID: 42844 RVA: 0x00256CD2 File Offset: 0x00254ED2
			public Hole scaleNumberFactor { get; private set; }

			// Token: 0x17001DFF RID: 7679
			// (get) Token: 0x0600A75D RID: 42845 RVA: 0x00256CDB File Offset: 0x00254EDB
			// (set) Token: 0x0600A75E RID: 42846 RVA: 0x00256CE3 File Offset: 0x00254EE3
			public Hole absPos { get; private set; }

			// Token: 0x0600A75F RID: 42847 RVA: 0x00256CEC File Offset: 0x00254EEC
			public GrammarHoles(GrammarBuilders builders)
			{
				this.row = new Hole(builders.Symbol.row, null);
				this.result = new Hole(builders.Symbol.result, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.outNumber = new Hole(builders.Symbol.outNumber, null);
				this.outDate = new Hole(builders.Symbol.outDate, null);
				this.outStr = new Hole(builders.Symbol.outStr, null);
				this.outStr1 = new Hole(builders.Symbol.outStr1, null);
				this.segmentCase = new Hole(builders.Symbol.segmentCase, null);
				this.segment = new Hole(builders.Symbol.segment, null);
				this.formatted = new Hole(builders.Symbol.formatted, null);
				this.concatEntry = new Hole(builders.Symbol.concatEntry, null);
				this.concatCase = new Hole(builders.Symbol.concatCase, null);
				this.concat = new Hole(builders.Symbol.concat, null);
				this.concatPrefix = new Hole(builders.Symbol.concatPrefix, null);
				this.concatSegment = new Hole(builders.Symbol.concatSegment, null);
				this.concatSuffix = new Hole(builders.Symbol.concatSuffix, null);
				this.condition = new Hole(builders.Symbol.condition, null);
				this.or = new Hole(builders.Symbol.or, null);
				this.inull = new Hole(builders.Symbol.inull, null);
				this.equalsText = new Hole(builders.Symbol.equalsText, null);
				this.containsFindText = new Hole(builders.Symbol.containsFindText, null);
				this.startsWithFindText = new Hole(builders.Symbol.startsWithFindText, null);
				this.isMatchRegex = new Hole(builders.Symbol.isMatchRegex, null);
				this.containsMatchRegex = new Hole(builders.Symbol.containsMatchRegex, null);
				this.containsCount = new Hole(builders.Symbol.containsCount, null);
				this.matchCount = new Hole(builders.Symbol.matchCount, null);
				this.numberEqualsValue = new Hole(builders.Symbol.numberEqualsValue, null);
				this.numberGreaterThanValue = new Hole(builders.Symbol.numberGreaterThanValue, null);
				this.numberLessThanValue = new Hole(builders.Symbol.numberLessThanValue, null);
				this.formatNumber = new Hole(builders.Symbol.formatNumber, null);
				this.number = new Hole(builders.Symbol.number, null);
				this.number1 = new Hole(builders.Symbol.number1, null);
				this.arithmetic = new Hole(builders.Symbol.arithmetic, null);
				this.arithmeticLeft = new Hole(builders.Symbol.arithmeticLeft, null);
				this.addRight = new Hole(builders.Symbol.addRight, null);
				this.subtractRight = new Hole(builders.Symbol.subtractRight, null);
				this.multiplyRight = new Hole(builders.Symbol.multiplyRight, null);
				this.divideRight = new Hole(builders.Symbol.divideRight, null);
				this.inumber = new Hole(builders.Symbol.inumber, null);
				this.rowNumberTransform = new Hole(builders.Symbol.rowNumberTransform, null);
				this.formatDateTime = new Hole(builders.Symbol.formatDateTime, null);
				this.date = new Hole(builders.Symbol.date, null);
				this.idate = new Hole(builders.Symbol.idate, null);
				this.itime = new Hole(builders.Symbol.itime, null);
				this.parseSubject = new Hole(builders.Symbol.parseSubject, null);
				this.letSubstring = new Hole(builders.Symbol.letSubstring, null);
				this.x = new Hole(builders.Symbol.x, null);
				this.substring = new Hole(builders.Symbol.substring, null);
				this.splitTrim = new Hole(builders.Symbol.splitTrim, null);
				this.split = new Hole(builders.Symbol.split, null);
				this.sliceTrim = new Hole(builders.Symbol.sliceTrim, null);
				this.slice = new Hole(builders.Symbol.slice, null);
				this.pos = new Hole(builders.Symbol.pos, null);
				this.fromStrTrim = new Hole(builders.Symbol.fromStrTrim, null);
				this.fromStr = new Hole(builders.Symbol.fromStr, null);
				this.fromNumberStr = new Hole(builders.Symbol.fromNumberStr, null);
				this.fromNumber = new Hole(builders.Symbol.fromNumber, null);
				this.fromNumberCoalesced = new Hole(builders.Symbol.fromNumberCoalesced, null);
				this.fromRowNumber = new Hole(builders.Symbol.fromRowNumber, null);
				this.fromNumbers = new Hole(builders.Symbol.fromNumbers, null);
				this.fromDateTime = new Hole(builders.Symbol.fromDateTime, null);
				this.fromDateTimePart = new Hole(builders.Symbol.fromDateTimePart, null);
				this.fromTime = new Hole(builders.Symbol.fromTime, null);
				this.constString = new Hole(builders.Symbol.constString, null);
				this.constNumber = new Hole(builders.Symbol.constNumber, null);
				this.constDate = new Hole(builders.Symbol.constDate, null);
				this.columnName = new Hole(builders.Symbol.columnName, null);
				this.columnNames = new Hole(builders.Symbol.columnNames, null);
				this.constStr = new Hole(builders.Symbol.constStr, null);
				this.constNum = new Hole(builders.Symbol.constNum, null);
				this.constDt = new Hole(builders.Symbol.constDt, null);
				this.locale = new Hole(builders.Symbol.locale, null);
				this.replaceFindText = new Hole(builders.Symbol.replaceFindText, null);
				this.replaceText = new Hole(builders.Symbol.replaceText, null);
				this.sliceBetweenStartText = new Hole(builders.Symbol.sliceBetweenStartText, null);
				this.sliceBetweenEndText = new Hole(builders.Symbol.sliceBetweenEndText, null);
				this.numberFormatDesc = new Hole(builders.Symbol.numberFormatDesc, null);
				this.numberRoundDesc = new Hole(builders.Symbol.numberRoundDesc, null);
				this.dateTimeRoundDesc = new Hole(builders.Symbol.dateTimeRoundDesc, null);
				this.dateTimeFormatDesc = new Hole(builders.Symbol.dateTimeFormatDesc, null);
				this.dateTimeParseDesc = new Hole(builders.Symbol.dateTimeParseDesc, null);
				this.dateTimePartKind = new Hole(builders.Symbol.dateTimePartKind, null);
				this.fromDateTimePartKind = new Hole(builders.Symbol.fromDateTimePartKind, null);
				this.timePartKind = new Hole(builders.Symbol.timePartKind, null);
				this.rowNumberLinearTransformDesc = new Hole(builders.Symbol.rowNumberLinearTransformDesc, null);
				this.matchDesc = new Hole(builders.Symbol.matchDesc, null);
				this.matchInstance = new Hole(builders.Symbol.matchInstance, null);
				this.splitDelimiter = new Hole(builders.Symbol.splitDelimiter, null);
				this.splitInstance = new Hole(builders.Symbol.splitInstance, null);
				this.findDelimiter = new Hole(builders.Symbol.findDelimiter, null);
				this.findInstance = new Hole(builders.Symbol.findInstance, null);
				this.findOffset = new Hole(builders.Symbol.findOffset, null);
				this.slicePrefixAbsPos = new Hole(builders.Symbol.slicePrefixAbsPos, null);
				this.scaleNumberFactor = new Hole(builders.Symbol.scaleNumberFactor, null);
				this.absPos = new Hole(builders.Symbol.absPos, null);
			}
		}

		// Token: 0x020014FD RID: 5373
		public class Nodes
		{
			// Token: 0x0600A760 RID: 42848 RVA: 0x00257588 File Offset: 0x00255788
			public Nodes(GrammarBuilders builders)
			{
				this.Rule = new GrammarBuilders.Nodes.NodeRules(builders);
				this.UnnamedConversion = new GrammarBuilders.Nodes.NodeUnnamedConversionRules(builders);
				this._variable = new Lazy<GrammarBuilders.Nodes.NodeVariables>(() => new GrammarBuilders.Nodes.NodeVariables(builders), LazyThreadSafetyMode.ExecutionAndPublication);
				this._hole = new Lazy<GrammarBuilders.Nodes.NodeHoles>(() => new GrammarBuilders.Nodes.NodeHoles(builders), LazyThreadSafetyMode.ExecutionAndPublication);
				this.Unsafe = new GrammarBuilders.Nodes.NodeUnsafe();
				this.Cast = new GrammarBuilders.Nodes.NodeCast(builders);
				this.CastRule = new GrammarBuilders.Nodes.RuleCast(builders);
				this.Is = new GrammarBuilders.Nodes.NodeIs(builders);
				this.IsRule = new GrammarBuilders.Nodes.RuleIs(builders);
				this.As = new GrammarBuilders.Nodes.NodeAs(builders);
				this.AsRule = new GrammarBuilders.Nodes.RuleAs(builders);
			}

			// Token: 0x17001E00 RID: 7680
			// (get) Token: 0x0600A761 RID: 42849 RVA: 0x0025766B File Offset: 0x0025586B
			// (set) Token: 0x0600A762 RID: 42850 RVA: 0x00257673 File Offset: 0x00255873
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17001E01 RID: 7681
			// (get) Token: 0x0600A763 RID: 42851 RVA: 0x0025767C File Offset: 0x0025587C
			// (set) Token: 0x0600A764 RID: 42852 RVA: 0x00257684 File Offset: 0x00255884
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17001E02 RID: 7682
			// (get) Token: 0x0600A765 RID: 42853 RVA: 0x0025768D File Offset: 0x0025588D
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17001E03 RID: 7683
			// (get) Token: 0x0600A766 RID: 42854 RVA: 0x0025769A File Offset: 0x0025589A
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17001E04 RID: 7684
			// (get) Token: 0x0600A767 RID: 42855 RVA: 0x002576A7 File Offset: 0x002558A7
			// (set) Token: 0x0600A768 RID: 42856 RVA: 0x002576AF File Offset: 0x002558AF
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17001E05 RID: 7685
			// (get) Token: 0x0600A769 RID: 42857 RVA: 0x002576B8 File Offset: 0x002558B8
			// (set) Token: 0x0600A76A RID: 42858 RVA: 0x002576C0 File Offset: 0x002558C0
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17001E06 RID: 7686
			// (get) Token: 0x0600A76B RID: 42859 RVA: 0x002576C9 File Offset: 0x002558C9
			// (set) Token: 0x0600A76C RID: 42860 RVA: 0x002576D1 File Offset: 0x002558D1
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17001E07 RID: 7687
			// (get) Token: 0x0600A76D RID: 42861 RVA: 0x002576DA File Offset: 0x002558DA
			// (set) Token: 0x0600A76E RID: 42862 RVA: 0x002576E2 File Offset: 0x002558E2
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17001E08 RID: 7688
			// (get) Token: 0x0600A76F RID: 42863 RVA: 0x002576EB File Offset: 0x002558EB
			// (set) Token: 0x0600A770 RID: 42864 RVA: 0x002576F3 File Offset: 0x002558F3
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17001E09 RID: 7689
			// (get) Token: 0x0600A771 RID: 42865 RVA: 0x002576FC File Offset: 0x002558FC
			// (set) Token: 0x0600A772 RID: 42866 RVA: 0x00257704 File Offset: 0x00255904
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17001E0A RID: 7690
			// (get) Token: 0x0600A773 RID: 42867 RVA: 0x0025770D File Offset: 0x0025590D
			// (set) Token: 0x0600A774 RID: 42868 RVA: 0x00257715 File Offset: 0x00255915
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04004476 RID: 17526
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04004477 RID: 17527
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x020014FE RID: 5374
			public class NodeRules
			{
				// Token: 0x0600A775 RID: 42869 RVA: 0x0025771E File Offset: 0x0025591E
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600A776 RID: 42870 RVA: 0x0025772D File Offset: 0x0025592D
				public equalsText equalsText(string value)
				{
					return new equalsText(this._builders, value);
				}

				// Token: 0x0600A777 RID: 42871 RVA: 0x0025773B File Offset: 0x0025593B
				public containsFindText containsFindText(string value)
				{
					return new containsFindText(this._builders, value);
				}

				// Token: 0x0600A778 RID: 42872 RVA: 0x00257749 File Offset: 0x00255949
				public startsWithFindText startsWithFindText(string value)
				{
					return new startsWithFindText(this._builders, value);
				}

				// Token: 0x0600A779 RID: 42873 RVA: 0x00257757 File Offset: 0x00255957
				public isMatchRegex isMatchRegex(Regex value)
				{
					return new isMatchRegex(this._builders, value);
				}

				// Token: 0x0600A77A RID: 42874 RVA: 0x00257765 File Offset: 0x00255965
				public containsMatchRegex containsMatchRegex(Regex value)
				{
					return new containsMatchRegex(this._builders, value);
				}

				// Token: 0x0600A77B RID: 42875 RVA: 0x00257773 File Offset: 0x00255973
				public containsCount containsCount(int value)
				{
					return new containsCount(this._builders, value);
				}

				// Token: 0x0600A77C RID: 42876 RVA: 0x00257781 File Offset: 0x00255981
				public matchCount matchCount(int value)
				{
					return new matchCount(this._builders, value);
				}

				// Token: 0x0600A77D RID: 42877 RVA: 0x0025778F File Offset: 0x0025598F
				public numberEqualsValue numberEqualsValue(decimal value)
				{
					return new numberEqualsValue(this._builders, value);
				}

				// Token: 0x0600A77E RID: 42878 RVA: 0x0025779D File Offset: 0x0025599D
				public numberGreaterThanValue numberGreaterThanValue(decimal value)
				{
					return new numberGreaterThanValue(this._builders, value);
				}

				// Token: 0x0600A77F RID: 42879 RVA: 0x002577AB File Offset: 0x002559AB
				public numberLessThanValue numberLessThanValue(decimal value)
				{
					return new numberLessThanValue(this._builders, value);
				}

				// Token: 0x0600A780 RID: 42880 RVA: 0x002577B9 File Offset: 0x002559B9
				public columnName columnName(string value)
				{
					return new columnName(this._builders, value);
				}

				// Token: 0x0600A781 RID: 42881 RVA: 0x002577C7 File Offset: 0x002559C7
				public columnNames columnNames(string[] value)
				{
					return new columnNames(this._builders, value);
				}

				// Token: 0x0600A782 RID: 42882 RVA: 0x002577D5 File Offset: 0x002559D5
				public constStr constStr(string value)
				{
					return new constStr(this._builders, value);
				}

				// Token: 0x0600A783 RID: 42883 RVA: 0x002577E3 File Offset: 0x002559E3
				public constNum constNum(decimal value)
				{
					return new constNum(this._builders, value);
				}

				// Token: 0x0600A784 RID: 42884 RVA: 0x002577F1 File Offset: 0x002559F1
				public constDt constDt(DateTime value)
				{
					return new constDt(this._builders, value);
				}

				// Token: 0x0600A785 RID: 42885 RVA: 0x002577FF File Offset: 0x002559FF
				public locale locale(string value)
				{
					return new locale(this._builders, value);
				}

				// Token: 0x0600A786 RID: 42886 RVA: 0x0025780D File Offset: 0x00255A0D
				public replaceFindText replaceFindText(string value)
				{
					return new replaceFindText(this._builders, value);
				}

				// Token: 0x0600A787 RID: 42887 RVA: 0x0025781B File Offset: 0x00255A1B
				public replaceText replaceText(string value)
				{
					return new replaceText(this._builders, value);
				}

				// Token: 0x0600A788 RID: 42888 RVA: 0x00257829 File Offset: 0x00255A29
				public sliceBetweenStartText sliceBetweenStartText(string value)
				{
					return new sliceBetweenStartText(this._builders, value);
				}

				// Token: 0x0600A789 RID: 42889 RVA: 0x00257837 File Offset: 0x00255A37
				public sliceBetweenEndText sliceBetweenEndText(string value)
				{
					return new sliceBetweenEndText(this._builders, value);
				}

				// Token: 0x0600A78A RID: 42890 RVA: 0x00257845 File Offset: 0x00255A45
				public numberFormatDesc numberFormatDesc(FormatNumberDescriptor value)
				{
					return new numberFormatDesc(this._builders, value);
				}

				// Token: 0x0600A78B RID: 42891 RVA: 0x00257853 File Offset: 0x00255A53
				public numberRoundDesc numberRoundDesc(RoundNumberDescriptor value)
				{
					return new numberRoundDesc(this._builders, value);
				}

				// Token: 0x0600A78C RID: 42892 RVA: 0x00257861 File Offset: 0x00255A61
				public dateTimeRoundDesc dateTimeRoundDesc(RoundDateTimeDescriptor value)
				{
					return new dateTimeRoundDesc(this._builders, value);
				}

				// Token: 0x0600A78D RID: 42893 RVA: 0x0025786F File Offset: 0x00255A6F
				public dateTimeFormatDesc dateTimeFormatDesc(DateTimeDescriptor value)
				{
					return new dateTimeFormatDesc(this._builders, value);
				}

				// Token: 0x0600A78E RID: 42894 RVA: 0x0025787D File Offset: 0x00255A7D
				public dateTimeParseDesc dateTimeParseDesc(DateTimeDescriptor value)
				{
					return new dateTimeParseDesc(this._builders, value);
				}

				// Token: 0x0600A78F RID: 42895 RVA: 0x0025788B File Offset: 0x00255A8B
				public dateTimePartKind dateTimePartKind(DateTimePartKind value)
				{
					return new dateTimePartKind(this._builders, value);
				}

				// Token: 0x0600A790 RID: 42896 RVA: 0x00257899 File Offset: 0x00255A99
				public fromDateTimePartKind fromDateTimePartKind(DateTimePartKind value)
				{
					return new fromDateTimePartKind(this._builders, value);
				}

				// Token: 0x0600A791 RID: 42897 RVA: 0x002578A7 File Offset: 0x00255AA7
				public timePartKind timePartKind(TimePartKind value)
				{
					return new timePartKind(this._builders, value);
				}

				// Token: 0x0600A792 RID: 42898 RVA: 0x002578B5 File Offset: 0x00255AB5
				public rowNumberLinearTransformDesc rowNumberLinearTransformDesc(RowNumberLinearTransformDescriptor value)
				{
					return new rowNumberLinearTransformDesc(this._builders, value);
				}

				// Token: 0x0600A793 RID: 42899 RVA: 0x002578C3 File Offset: 0x00255AC3
				public matchDesc matchDesc(MatchDescriptor value)
				{
					return new matchDesc(this._builders, value);
				}

				// Token: 0x0600A794 RID: 42900 RVA: 0x002578D1 File Offset: 0x00255AD1
				public matchInstance matchInstance(int value)
				{
					return new matchInstance(this._builders, value);
				}

				// Token: 0x0600A795 RID: 42901 RVA: 0x002578DF File Offset: 0x00255ADF
				public splitDelimiter splitDelimiter(string value)
				{
					return new splitDelimiter(this._builders, value);
				}

				// Token: 0x0600A796 RID: 42902 RVA: 0x002578ED File Offset: 0x00255AED
				public splitInstance splitInstance(int value)
				{
					return new splitInstance(this._builders, value);
				}

				// Token: 0x0600A797 RID: 42903 RVA: 0x002578FB File Offset: 0x00255AFB
				public findDelimiter findDelimiter(string value)
				{
					return new findDelimiter(this._builders, value);
				}

				// Token: 0x0600A798 RID: 42904 RVA: 0x00257909 File Offset: 0x00255B09
				public findInstance findInstance(int value)
				{
					return new findInstance(this._builders, value);
				}

				// Token: 0x0600A799 RID: 42905 RVA: 0x00257917 File Offset: 0x00255B17
				public findOffset findOffset(int value)
				{
					return new findOffset(this._builders, value);
				}

				// Token: 0x0600A79A RID: 42906 RVA: 0x00257925 File Offset: 0x00255B25
				public slicePrefixAbsPos slicePrefixAbsPos(int value)
				{
					return new slicePrefixAbsPos(this._builders, value);
				}

				// Token: 0x0600A79B RID: 42907 RVA: 0x00257933 File Offset: 0x00255B33
				public scaleNumberFactor scaleNumberFactor(int value)
				{
					return new scaleNumberFactor(this._builders, value);
				}

				// Token: 0x0600A79C RID: 42908 RVA: 0x00257941 File Offset: 0x00255B41
				public absPos absPos(int value)
				{
					return new absPos(this._builders, value);
				}

				// Token: 0x0600A79D RID: 42909 RVA: 0x0025794F File Offset: 0x00255B4F
				public result If(condition value0, result value1, result value2)
				{
					return new If(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A79E RID: 42910 RVA: 0x00257964 File Offset: 0x00255B64
				public output ToInt(outNumber value0)
				{
					return new ToInt(this._builders, value0);
				}

				// Token: 0x0600A79F RID: 42911 RVA: 0x00257977 File Offset: 0x00255B77
				public output ToDouble(outNumber value0)
				{
					return new ToDouble(this._builders, value0);
				}

				// Token: 0x0600A7A0 RID: 42912 RVA: 0x0025798A File Offset: 0x00255B8A
				public output ToDecimal(outNumber value0)
				{
					return new ToDecimal(this._builders, value0);
				}

				// Token: 0x0600A7A1 RID: 42913 RVA: 0x0025799D File Offset: 0x00255B9D
				public output ToDateTime(outDate value0)
				{
					return new ToDateTime(this._builders, value0);
				}

				// Token: 0x0600A7A2 RID: 42914 RVA: 0x002579B0 File Offset: 0x00255BB0
				public output ToStr(outStr value0)
				{
					return new ToStr(this._builders, value0);
				}

				// Token: 0x0600A7A3 RID: 42915 RVA: 0x002579C3 File Offset: 0x00255BC3
				public outStr Replace(fromStr value0, replaceFindText value1, replaceText value2)
				{
					return new Replace(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7A4 RID: 42916 RVA: 0x002579D8 File Offset: 0x00255BD8
				public segmentCase LowerCase(segment value0)
				{
					return new LowerCase(this._builders, value0);
				}

				// Token: 0x0600A7A5 RID: 42917 RVA: 0x002579EB File Offset: 0x00255BEB
				public segmentCase UpperCase(segment value0)
				{
					return new UpperCase(this._builders, value0);
				}

				// Token: 0x0600A7A6 RID: 42918 RVA: 0x002579FE File Offset: 0x00255BFE
				public segmentCase ProperCase(segment value0)
				{
					return new ProperCase(this._builders, value0);
				}

				// Token: 0x0600A7A7 RID: 42919 RVA: 0x00257A11 File Offset: 0x00255C11
				public concatCase LowerCaseConcat(concat value0)
				{
					return new LowerCaseConcat(this._builders, value0);
				}

				// Token: 0x0600A7A8 RID: 42920 RVA: 0x00257A24 File Offset: 0x00255C24
				public concatCase UpperCaseConcat(concat value0)
				{
					return new UpperCaseConcat(this._builders, value0);
				}

				// Token: 0x0600A7A9 RID: 42921 RVA: 0x00257A37 File Offset: 0x00255C37
				public concatCase ProperCaseConcat(concat value0)
				{
					return new ProperCaseConcat(this._builders, value0);
				}

				// Token: 0x0600A7AA RID: 42922 RVA: 0x00257A4A File Offset: 0x00255C4A
				public concat Concat(concatPrefix value0, concatSuffix value1)
				{
					return new Concat(this._builders, value0, value1);
				}

				// Token: 0x0600A7AB RID: 42923 RVA: 0x00257A5E File Offset: 0x00255C5E
				public condition StringEquals(row value0, columnName value1, equalsText value2)
				{
					return new StringEquals(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7AC RID: 42924 RVA: 0x00257A73 File Offset: 0x00255C73
				public condition Contains(row value0, columnName value1, containsFindText value2, containsCount value3)
				{
					return new Contains(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x0600A7AD RID: 42925 RVA: 0x00257A8A File Offset: 0x00255C8A
				public condition StartsWithDigit(row value0, columnName value1)
				{
					return new StartsWithDigit(this._builders, value0, value1);
				}

				// Token: 0x0600A7AE RID: 42926 RVA: 0x00257A9E File Offset: 0x00255C9E
				public condition EndsWithDigit(row value0, columnName value1)
				{
					return new EndsWithDigit(this._builders, value0, value1);
				}

				// Token: 0x0600A7AF RID: 42927 RVA: 0x00257AB2 File Offset: 0x00255CB2
				public condition StartsWith(row value0, columnName value1, startsWithFindText value2)
				{
					return new StartsWith(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7B0 RID: 42928 RVA: 0x00257AC7 File Offset: 0x00255CC7
				public condition IsBlank(row value0, columnName value1)
				{
					return new IsBlank(this._builders, value0, value1);
				}

				// Token: 0x0600A7B1 RID: 42929 RVA: 0x00257ADB File Offset: 0x00255CDB
				public condition IsNotBlank(row value0, columnName value1)
				{
					return new IsNotBlank(this._builders, value0, value1);
				}

				// Token: 0x0600A7B2 RID: 42930 RVA: 0x00257AEF File Offset: 0x00255CEF
				public condition NumberEquals(row value0, columnName value1, numberEqualsValue value2)
				{
					return new NumberEquals(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7B3 RID: 42931 RVA: 0x00257B04 File Offset: 0x00255D04
				public condition NumberGreaterThan(row value0, columnName value1, numberGreaterThanValue value2)
				{
					return new NumberGreaterThan(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7B4 RID: 42932 RVA: 0x00257B19 File Offset: 0x00255D19
				public condition NumberLessThan(row value0, columnName value1, numberLessThanValue value2)
				{
					return new NumberLessThan(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7B5 RID: 42933 RVA: 0x00257B2E File Offset: 0x00255D2E
				public condition IsString(row value0, columnName value1)
				{
					return new IsString(this._builders, value0, value1);
				}

				// Token: 0x0600A7B6 RID: 42934 RVA: 0x00257B42 File Offset: 0x00255D42
				public condition IsNumber(row value0, columnName value1)
				{
					return new IsNumber(this._builders, value0, value1);
				}

				// Token: 0x0600A7B7 RID: 42935 RVA: 0x00257B56 File Offset: 0x00255D56
				public condition IsMatch(row value0, columnName value1, isMatchRegex value2)
				{
					return new IsMatch(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7B8 RID: 42936 RVA: 0x00257B6B File Offset: 0x00255D6B
				public condition ContainsMatch(row value0, columnName value1, containsMatchRegex value2, matchCount value3)
				{
					return new ContainsMatch(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x0600A7B9 RID: 42937 RVA: 0x00257B82 File Offset: 0x00255D82
				public or Or(condition value0, condition value1)
				{
					return new Or(this._builders, value0, value1);
				}

				// Token: 0x0600A7BA RID: 42938 RVA: 0x00257B96 File Offset: 0x00255D96
				public inull Null()
				{
					return new Null(this._builders);
				}

				// Token: 0x0600A7BB RID: 42939 RVA: 0x00257BA8 File Offset: 0x00255DA8
				public formatNumber FormatNumber(number value0, numberFormatDesc value1)
				{
					return new FormatNumber(this._builders, value0, value1);
				}

				// Token: 0x0600A7BC RID: 42940 RVA: 0x00257BBC File Offset: 0x00255DBC
				public number Length(fromStr value0)
				{
					return new Length(this._builders, value0);
				}

				// Token: 0x0600A7BD RID: 42941 RVA: 0x00257BCF File Offset: 0x00255DCF
				public number1 DateTimePart(idate value0, dateTimePartKind value1)
				{
					return new DateTimePart(this._builders, value0, value1);
				}

				// Token: 0x0600A7BE RID: 42942 RVA: 0x00257BE3 File Offset: 0x00255DE3
				public number1 TimePart(itime value0, timePartKind value1)
				{
					return new TimePart(this._builders, value0, value1);
				}

				// Token: 0x0600A7BF RID: 42943 RVA: 0x00257BF7 File Offset: 0x00255DF7
				public number1 RoundNumber(inumber value0, numberRoundDesc value1)
				{
					return new RoundNumber(this._builders, value0, value1);
				}

				// Token: 0x0600A7C0 RID: 42944 RVA: 0x00257C0B File Offset: 0x00255E0B
				public arithmetic Add(arithmeticLeft value0, addRight value1)
				{
					return new Add(this._builders, value0, value1);
				}

				// Token: 0x0600A7C1 RID: 42945 RVA: 0x00257C1F File Offset: 0x00255E1F
				public arithmetic Subtract(arithmeticLeft value0, subtractRight value1)
				{
					return new Subtract(this._builders, value0, value1);
				}

				// Token: 0x0600A7C2 RID: 42946 RVA: 0x00257C33 File Offset: 0x00255E33
				public arithmetic Multiply(arithmeticLeft value0, multiplyRight value1)
				{
					return new Multiply(this._builders, value0, value1);
				}

				// Token: 0x0600A7C3 RID: 42947 RVA: 0x00257C47 File Offset: 0x00255E47
				public arithmetic Divide(arithmeticLeft value0, divideRight value1)
				{
					return new Divide(this._builders, value0, value1);
				}

				// Token: 0x0600A7C4 RID: 42948 RVA: 0x00257C5B File Offset: 0x00255E5B
				public arithmetic Sum(fromNumbers value0)
				{
					return new Sum(this._builders, value0);
				}

				// Token: 0x0600A7C5 RID: 42949 RVA: 0x00257C6E File Offset: 0x00255E6E
				public arithmetic Product(fromNumbers value0)
				{
					return new Product(this._builders, value0);
				}

				// Token: 0x0600A7C6 RID: 42950 RVA: 0x00257C81 File Offset: 0x00255E81
				public arithmetic Average(fromNumbers value0)
				{
					return new Average(this._builders, value0);
				}

				// Token: 0x0600A7C7 RID: 42951 RVA: 0x00257C94 File Offset: 0x00255E94
				public addRight AddRightNumber(constNum value0)
				{
					return new AddRightNumber(this._builders, value0);
				}

				// Token: 0x0600A7C8 RID: 42952 RVA: 0x00257CA7 File Offset: 0x00255EA7
				public subtractRight SubtractRightNumber(constNum value0)
				{
					return new SubtractRightNumber(this._builders, value0);
				}

				// Token: 0x0600A7C9 RID: 42953 RVA: 0x00257CBA File Offset: 0x00255EBA
				public multiplyRight MultiplyRightNumber(constNum value0)
				{
					return new MultiplyRightNumber(this._builders, value0);
				}

				// Token: 0x0600A7CA RID: 42954 RVA: 0x00257CCD File Offset: 0x00255ECD
				public divideRight DivideRightNumber(constNum value0)
				{
					return new DivideRightNumber(this._builders, value0);
				}

				// Token: 0x0600A7CB RID: 42955 RVA: 0x00257CE0 File Offset: 0x00255EE0
				public inumber ParseNumber(parseSubject value0, locale value1)
				{
					return new ParseNumber(this._builders, value0, value1);
				}

				// Token: 0x0600A7CC RID: 42956 RVA: 0x00257CF4 File Offset: 0x00255EF4
				public rowNumberTransform RowNumberLinearTransform(fromRowNumber value0, rowNumberLinearTransformDesc value1)
				{
					return new RowNumberLinearTransform(this._builders, value0, value1);
				}

				// Token: 0x0600A7CD RID: 42957 RVA: 0x00257D08 File Offset: 0x00255F08
				public formatDateTime FormatDateTime(date value0, dateTimeFormatDesc value1)
				{
					return new FormatDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600A7CE RID: 42958 RVA: 0x00257D1C File Offset: 0x00255F1C
				public date RoundDateTime(idate value0, dateTimeRoundDesc value1)
				{
					return new RoundDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600A7CF RID: 42959 RVA: 0x00257D30 File Offset: 0x00255F30
				public idate ParseDateTime(parseSubject value0, dateTimeParseDesc value1)
				{
					return new ParseDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600A7D0 RID: 42960 RVA: 0x00257D44 File Offset: 0x00255F44
				public substring SlicePrefixAbs(x value0, slicePrefixAbsPos value1)
				{
					return new SlicePrefixAbs(this._builders, value0, value1);
				}

				// Token: 0x0600A7D1 RID: 42961 RVA: 0x00257D58 File Offset: 0x00255F58
				public substring SlicePrefix(x value0, pos value1)
				{
					return new SlicePrefix(this._builders, value0, value1);
				}

				// Token: 0x0600A7D2 RID: 42962 RVA: 0x00257D6C File Offset: 0x00255F6C
				public substring SliceSuffix(x value0, pos value1)
				{
					return new SliceSuffix(this._builders, value0, value1);
				}

				// Token: 0x0600A7D3 RID: 42963 RVA: 0x00257D80 File Offset: 0x00255F80
				public substring MatchFull(x value0, matchDesc value1, matchInstance value2)
				{
					return new MatchFull(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7D4 RID: 42964 RVA: 0x00257D95 File Offset: 0x00255F95
				public substring SliceBetween(x value0, sliceBetweenStartText value1, sliceBetweenEndText value2)
				{
					return new SliceBetween(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7D5 RID: 42965 RVA: 0x00257DAA File Offset: 0x00255FAA
				public splitTrim TrimSplit(split value0)
				{
					return new TrimSplit(this._builders, value0);
				}

				// Token: 0x0600A7D6 RID: 42966 RVA: 0x00257DBD File Offset: 0x00255FBD
				public splitTrim TrimFullSplit(split value0)
				{
					return new TrimFullSplit(this._builders, value0);
				}

				// Token: 0x0600A7D7 RID: 42967 RVA: 0x00257DD0 File Offset: 0x00255FD0
				public split Split(x value0, splitDelimiter value1, splitInstance value2)
				{
					return new Split(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7D8 RID: 42968 RVA: 0x00257DE5 File Offset: 0x00255FE5
				public sliceTrim TrimSlice(slice value0)
				{
					return new TrimSlice(this._builders, value0);
				}

				// Token: 0x0600A7D9 RID: 42969 RVA: 0x00257DF8 File Offset: 0x00255FF8
				public sliceTrim TrimFullSlice(slice value0)
				{
					return new TrimFullSlice(this._builders, value0);
				}

				// Token: 0x0600A7DA RID: 42970 RVA: 0x00257E0B File Offset: 0x0025600B
				public slice Slice(x value0, pos value1, pos value2)
				{
					return new Slice(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7DB RID: 42971 RVA: 0x00257E20 File Offset: 0x00256020
				public pos Find(x value0, findDelimiter value1, findInstance value2, findOffset value3)
				{
					return new Find(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x0600A7DC RID: 42972 RVA: 0x00257E37 File Offset: 0x00256037
				public pos Abs(x value0, absPos value1)
				{
					return new Abs(this._builders, value0, value1);
				}

				// Token: 0x0600A7DD RID: 42973 RVA: 0x00257E4B File Offset: 0x0025604B
				public pos Match(x value0, matchDesc value1, matchInstance value2)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7DE RID: 42974 RVA: 0x00257E60 File Offset: 0x00256060
				public pos MatchEnd(x value0, matchDesc value1, matchInstance value2)
				{
					return new MatchEnd(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7DF RID: 42975 RVA: 0x00257E75 File Offset: 0x00256075
				public fromStrTrim TrimFull(fromStr value0)
				{
					return new TrimFull(this._builders, value0);
				}

				// Token: 0x0600A7E0 RID: 42976 RVA: 0x00257E88 File Offset: 0x00256088
				public fromStrTrim Trim(fromStr value0)
				{
					return new Trim(this._builders, value0);
				}

				// Token: 0x0600A7E1 RID: 42977 RVA: 0x00257E9B File Offset: 0x0025609B
				public fromStr FromStr(row value0, columnName value1)
				{
					return new FromStr(this._builders, value0, value1);
				}

				// Token: 0x0600A7E2 RID: 42978 RVA: 0x00257EAF File Offset: 0x002560AF
				public fromNumberStr FromNumberStr(row value0, columnName value1)
				{
					return new FromNumberStr(this._builders, value0, value1);
				}

				// Token: 0x0600A7E3 RID: 42979 RVA: 0x00257EC3 File Offset: 0x002560C3
				public fromNumber FromNumber(row value0, columnName value1)
				{
					return new FromNumber(this._builders, value0, value1);
				}

				// Token: 0x0600A7E4 RID: 42980 RVA: 0x00257ED7 File Offset: 0x002560D7
				public fromNumberCoalesced FromNumberCoalesced(row value0, columnName value1)
				{
					return new FromNumberCoalesced(this._builders, value0, value1);
				}

				// Token: 0x0600A7E5 RID: 42981 RVA: 0x00257EEB File Offset: 0x002560EB
				public fromRowNumber FromRowNumber(row value0)
				{
					return new FromRowNumber(this._builders, value0);
				}

				// Token: 0x0600A7E6 RID: 42982 RVA: 0x00257EFE File Offset: 0x002560FE
				public fromNumbers FromNumbers(row value0, columnNames value1)
				{
					return new FromNumbers(this._builders, value0, value1);
				}

				// Token: 0x0600A7E7 RID: 42983 RVA: 0x00257F12 File Offset: 0x00256112
				public fromDateTime FromDateTime(row value0, columnName value1)
				{
					return new FromDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600A7E8 RID: 42984 RVA: 0x00257F26 File Offset: 0x00256126
				public fromDateTimePart FromDateTimePart(row value0, columnName value1, fromDateTimePartKind value2)
				{
					return new FromDateTimePart(this._builders, value0, value1, value2);
				}

				// Token: 0x0600A7E9 RID: 42985 RVA: 0x00257F3B File Offset: 0x0025613B
				public fromTime FromTime(row value0, columnName value1)
				{
					return new FromTime(this._builders, value0, value1);
				}

				// Token: 0x0600A7EA RID: 42986 RVA: 0x00257F4F File Offset: 0x0025614F
				public constString Str(constStr value0)
				{
					return new Str(this._builders, value0);
				}

				// Token: 0x0600A7EB RID: 42987 RVA: 0x00257F62 File Offset: 0x00256162
				public constNumber Number(constNum value0)
				{
					return new Number(this._builders, value0);
				}

				// Token: 0x0600A7EC RID: 42988 RVA: 0x00257F75 File Offset: 0x00256175
				public constDate Date(constDt value0)
				{
					return new Date(this._builders, value0);
				}

				// Token: 0x0600A7ED RID: 42989 RVA: 0x00257F88 File Offset: 0x00256188
				public letSubstring LetX(fromStrTrim value0, substring value1)
				{
					return new LetX(this._builders, value0, value1);
				}

				// Token: 0x0400447F RID: 17535
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020014FF RID: 5375
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0600A7EE RID: 42990 RVA: 0x00257F9C File Offset: 0x0025619C
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600A7EF RID: 42991 RVA: 0x00257FAB File Offset: 0x002561AB
				public result result_output(output value0)
				{
					return new result_output(this._builders, value0);
				}

				// Token: 0x0600A7F0 RID: 42992 RVA: 0x00257FBE File Offset: 0x002561BE
				public result result_inull(inull value0)
				{
					return new result_inull(this._builders, value0);
				}

				// Token: 0x0600A7F1 RID: 42993 RVA: 0x00257FD1 File Offset: 0x002561D1
				public outNumber outNumber_number(number value0)
				{
					return new outNumber_number(this._builders, value0);
				}

				// Token: 0x0600A7F2 RID: 42994 RVA: 0x00257FE4 File Offset: 0x002561E4
				public outNumber outNumber_constNumber(constNumber value0)
				{
					return new outNumber_constNumber(this._builders, value0);
				}

				// Token: 0x0600A7F3 RID: 42995 RVA: 0x00257FF7 File Offset: 0x002561F7
				public outDate outDate_date(date value0)
				{
					return new outDate_date(this._builders, value0);
				}

				// Token: 0x0600A7F4 RID: 42996 RVA: 0x0025800A File Offset: 0x0025620A
				public outDate outDate_constDate(constDate value0)
				{
					return new outDate_constDate(this._builders, value0);
				}

				// Token: 0x0600A7F5 RID: 42997 RVA: 0x0025801D File Offset: 0x0025621D
				public outStr outStr_outStr1(outStr1 value0)
				{
					return new outStr_outStr1(this._builders, value0);
				}

				// Token: 0x0600A7F6 RID: 42998 RVA: 0x00258030 File Offset: 0x00256230
				public outStr1 outStr1_segmentCase(segmentCase value0)
				{
					return new outStr1_segmentCase(this._builders, value0);
				}

				// Token: 0x0600A7F7 RID: 42999 RVA: 0x00258043 File Offset: 0x00256243
				public outStr1 outStr1_formatted(formatted value0)
				{
					return new outStr1_formatted(this._builders, value0);
				}

				// Token: 0x0600A7F8 RID: 43000 RVA: 0x00258056 File Offset: 0x00256256
				public outStr1 outStr1_concatEntry(concatEntry value0)
				{
					return new outStr1_concatEntry(this._builders, value0);
				}

				// Token: 0x0600A7F9 RID: 43001 RVA: 0x00258069 File Offset: 0x00256269
				public outStr1 outStr1_constString(constString value0)
				{
					return new outStr1_constString(this._builders, value0);
				}

				// Token: 0x0600A7FA RID: 43002 RVA: 0x0025807C File Offset: 0x0025627C
				public segmentCase segmentCase_segment(segment value0)
				{
					return new segmentCase_segment(this._builders, value0);
				}

				// Token: 0x0600A7FB RID: 43003 RVA: 0x0025808F File Offset: 0x0025628F
				public segment segment_fromStrTrim(fromStrTrim value0)
				{
					return new segment_fromStrTrim(this._builders, value0);
				}

				// Token: 0x0600A7FC RID: 43004 RVA: 0x002580A2 File Offset: 0x002562A2
				public segment segment_letSubstring(letSubstring value0)
				{
					return new segment_letSubstring(this._builders, value0);
				}

				// Token: 0x0600A7FD RID: 43005 RVA: 0x002580B5 File Offset: 0x002562B5
				public formatted formatted_formatNumber(formatNumber value0)
				{
					return new formatted_formatNumber(this._builders, value0);
				}

				// Token: 0x0600A7FE RID: 43006 RVA: 0x002580C8 File Offset: 0x002562C8
				public formatted formatted_formatDateTime(formatDateTime value0)
				{
					return new formatted_formatDateTime(this._builders, value0);
				}

				// Token: 0x0600A7FF RID: 43007 RVA: 0x002580DB File Offset: 0x002562DB
				public concatEntry concatEntry_concatCase(concatCase value0)
				{
					return new concatEntry_concatCase(this._builders, value0);
				}

				// Token: 0x0600A800 RID: 43008 RVA: 0x002580EE File Offset: 0x002562EE
				public concatEntry concatEntry_constString(constString value0)
				{
					return new concatEntry_constString(this._builders, value0);
				}

				// Token: 0x0600A801 RID: 43009 RVA: 0x00258101 File Offset: 0x00256301
				public concatCase concatCase_concat(concat value0)
				{
					return new concatCase_concat(this._builders, value0);
				}

				// Token: 0x0600A802 RID: 43010 RVA: 0x00258114 File Offset: 0x00256314
				public concatPrefix concatPrefix_concatSegment(concatSegment value0)
				{
					return new concatPrefix_concatSegment(this._builders, value0);
				}

				// Token: 0x0600A803 RID: 43011 RVA: 0x00258127 File Offset: 0x00256327
				public concatPrefix concatPrefix_formatted(formatted value0)
				{
					return new concatPrefix_formatted(this._builders, value0);
				}

				// Token: 0x0600A804 RID: 43012 RVA: 0x0025813A File Offset: 0x0025633A
				public concatPrefix concatPrefix_constString(constString value0)
				{
					return new concatPrefix_constString(this._builders, value0);
				}

				// Token: 0x0600A805 RID: 43013 RVA: 0x0025814D File Offset: 0x0025634D
				public concatSegment concatSegment_segment(segment value0)
				{
					return new concatSegment_segment(this._builders, value0);
				}

				// Token: 0x0600A806 RID: 43014 RVA: 0x00258160 File Offset: 0x00256360
				public concatSegment concatSegment_segmentCase(segmentCase value0)
				{
					return new concatSegment_segmentCase(this._builders, value0);
				}

				// Token: 0x0600A807 RID: 43015 RVA: 0x00258173 File Offset: 0x00256373
				public concatSuffix concatSuffix_concatPrefix(concatPrefix value0)
				{
					return new concatSuffix_concatPrefix(this._builders, value0);
				}

				// Token: 0x0600A808 RID: 43016 RVA: 0x00258186 File Offset: 0x00256386
				public concatSuffix concatSuffix_concat(concat value0)
				{
					return new concatSuffix_concat(this._builders, value0);
				}

				// Token: 0x0600A809 RID: 43017 RVA: 0x00258199 File Offset: 0x00256399
				public condition condition_or(or value0)
				{
					return new condition_or(this._builders, value0);
				}

				// Token: 0x0600A80A RID: 43018 RVA: 0x002581AC File Offset: 0x002563AC
				public number number_number1(number1 value0)
				{
					return new number_number1(this._builders, value0);
				}

				// Token: 0x0600A80B RID: 43019 RVA: 0x002581BF File Offset: 0x002563BF
				public number number_arithmetic(arithmetic value0)
				{
					return new number_arithmetic(this._builders, value0);
				}

				// Token: 0x0600A80C RID: 43020 RVA: 0x002581D2 File Offset: 0x002563D2
				public number number_rowNumberTransform(rowNumberTransform value0)
				{
					return new number_rowNumberTransform(this._builders, value0);
				}

				// Token: 0x0600A80D RID: 43021 RVA: 0x002581E5 File Offset: 0x002563E5
				public number1 number1_inumber(inumber value0)
				{
					return new number1_inumber(this._builders, value0);
				}

				// Token: 0x0600A80E RID: 43022 RVA: 0x002581F8 File Offset: 0x002563F8
				public arithmeticLeft arithmeticLeft_fromNumberCoalesced(fromNumberCoalesced value0)
				{
					return new arithmeticLeft_fromNumberCoalesced(this._builders, value0);
				}

				// Token: 0x0600A80F RID: 43023 RVA: 0x0025820B File Offset: 0x0025640B
				public arithmeticLeft arithmeticLeft_inumber(inumber value0)
				{
					return new arithmeticLeft_inumber(this._builders, value0);
				}

				// Token: 0x0600A810 RID: 43024 RVA: 0x0025821E File Offset: 0x0025641E
				public addRight addRight_arithmeticLeft(arithmeticLeft value0)
				{
					return new addRight_arithmeticLeft(this._builders, value0);
				}

				// Token: 0x0600A811 RID: 43025 RVA: 0x00258231 File Offset: 0x00256431
				public subtractRight subtractRight_arithmeticLeft(arithmeticLeft value0)
				{
					return new subtractRight_arithmeticLeft(this._builders, value0);
				}

				// Token: 0x0600A812 RID: 43026 RVA: 0x00258244 File Offset: 0x00256444
				public multiplyRight multiplyRight_arithmeticLeft(arithmeticLeft value0)
				{
					return new multiplyRight_arithmeticLeft(this._builders, value0);
				}

				// Token: 0x0600A813 RID: 43027 RVA: 0x00258257 File Offset: 0x00256457
				public divideRight divideRight_arithmeticLeft(arithmeticLeft value0)
				{
					return new divideRight_arithmeticLeft(this._builders, value0);
				}

				// Token: 0x0600A814 RID: 43028 RVA: 0x0025826A File Offset: 0x0025646A
				public inumber inumber_fromNumber(fromNumber value0)
				{
					return new inumber_fromNumber(this._builders, value0);
				}

				// Token: 0x0600A815 RID: 43029 RVA: 0x0025827D File Offset: 0x0025647D
				public rowNumberTransform rowNumberTransform_fromRowNumber(fromRowNumber value0)
				{
					return new rowNumberTransform_fromRowNumber(this._builders, value0);
				}

				// Token: 0x0600A816 RID: 43030 RVA: 0x00258290 File Offset: 0x00256490
				public date date_idate(idate value0)
				{
					return new date_idate(this._builders, value0);
				}

				// Token: 0x0600A817 RID: 43031 RVA: 0x002582A3 File Offset: 0x002564A3
				public idate idate_fromDateTime(fromDateTime value0)
				{
					return new idate_fromDateTime(this._builders, value0);
				}

				// Token: 0x0600A818 RID: 43032 RVA: 0x002582B6 File Offset: 0x002564B6
				public idate idate_fromDateTimePart(fromDateTimePart value0)
				{
					return new idate_fromDateTimePart(this._builders, value0);
				}

				// Token: 0x0600A819 RID: 43033 RVA: 0x002582C9 File Offset: 0x002564C9
				public itime itime_fromTime(fromTime value0)
				{
					return new itime_fromTime(this._builders, value0);
				}

				// Token: 0x0600A81A RID: 43034 RVA: 0x002582DC File Offset: 0x002564DC
				public parseSubject parseSubject_fromStr(fromStr value0)
				{
					return new parseSubject_fromStr(this._builders, value0);
				}

				// Token: 0x0600A81B RID: 43035 RVA: 0x002582EF File Offset: 0x002564EF
				public parseSubject parseSubject_letSubstring(letSubstring value0)
				{
					return new parseSubject_letSubstring(this._builders, value0);
				}

				// Token: 0x0600A81C RID: 43036 RVA: 0x00258302 File Offset: 0x00256502
				public substring substring_splitTrim(splitTrim value0)
				{
					return new substring_splitTrim(this._builders, value0);
				}

				// Token: 0x0600A81D RID: 43037 RVA: 0x00258315 File Offset: 0x00256515
				public substring substring_sliceTrim(sliceTrim value0)
				{
					return new substring_sliceTrim(this._builders, value0);
				}

				// Token: 0x0600A81E RID: 43038 RVA: 0x00258328 File Offset: 0x00256528
				public splitTrim splitTrim_split(split value0)
				{
					return new splitTrim_split(this._builders, value0);
				}

				// Token: 0x0600A81F RID: 43039 RVA: 0x0025833B File Offset: 0x0025653B
				public sliceTrim sliceTrim_slice(slice value0)
				{
					return new sliceTrim_slice(this._builders, value0);
				}

				// Token: 0x0600A820 RID: 43040 RVA: 0x0025834E File Offset: 0x0025654E
				public fromStrTrim fromStrTrim_fromStr(fromStr value0)
				{
					return new fromStrTrim_fromStr(this._builders, value0);
				}

				// Token: 0x0600A821 RID: 43041 RVA: 0x00258361 File Offset: 0x00256561
				public fromStrTrim fromStrTrim_fromNumberStr(fromNumberStr value0)
				{
					return new fromStrTrim_fromNumberStr(this._builders, value0);
				}

				// Token: 0x04004480 RID: 17536
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001500 RID: 5376
			public class NodeVariables
			{
				// Token: 0x17001E0B RID: 7691
				// (get) Token: 0x0600A822 RID: 43042 RVA: 0x00258374 File Offset: 0x00256574
				// (set) Token: 0x0600A823 RID: 43043 RVA: 0x0025837C File Offset: 0x0025657C
				public row row { get; private set; }

				// Token: 0x17001E0C RID: 7692
				// (get) Token: 0x0600A824 RID: 43044 RVA: 0x00258385 File Offset: 0x00256585
				// (set) Token: 0x0600A825 RID: 43045 RVA: 0x0025838D File Offset: 0x0025658D
				public x x { get; private set; }

				// Token: 0x0600A826 RID: 43046 RVA: 0x00258396 File Offset: 0x00256596
				public NodeVariables(GrammarBuilders builders)
				{
					this.row = new row(builders);
					this.x = new x(builders);
				}
			}

			// Token: 0x02001501 RID: 5377
			public class NodeHoles
			{
				// Token: 0x17001E0D RID: 7693
				// (get) Token: 0x0600A827 RID: 43047 RVA: 0x002583B6 File Offset: 0x002565B6
				// (set) Token: 0x0600A828 RID: 43048 RVA: 0x002583BE File Offset: 0x002565BE
				public result result { get; private set; }

				// Token: 0x17001E0E RID: 7694
				// (get) Token: 0x0600A829 RID: 43049 RVA: 0x002583C7 File Offset: 0x002565C7
				// (set) Token: 0x0600A82A RID: 43050 RVA: 0x002583CF File Offset: 0x002565CF
				public output output { get; private set; }

				// Token: 0x17001E0F RID: 7695
				// (get) Token: 0x0600A82B RID: 43051 RVA: 0x002583D8 File Offset: 0x002565D8
				// (set) Token: 0x0600A82C RID: 43052 RVA: 0x002583E0 File Offset: 0x002565E0
				public outNumber outNumber { get; private set; }

				// Token: 0x17001E10 RID: 7696
				// (get) Token: 0x0600A82D RID: 43053 RVA: 0x002583E9 File Offset: 0x002565E9
				// (set) Token: 0x0600A82E RID: 43054 RVA: 0x002583F1 File Offset: 0x002565F1
				public outDate outDate { get; private set; }

				// Token: 0x17001E11 RID: 7697
				// (get) Token: 0x0600A82F RID: 43055 RVA: 0x002583FA File Offset: 0x002565FA
				// (set) Token: 0x0600A830 RID: 43056 RVA: 0x00258402 File Offset: 0x00256602
				public outStr outStr { get; private set; }

				// Token: 0x17001E12 RID: 7698
				// (get) Token: 0x0600A831 RID: 43057 RVA: 0x0025840B File Offset: 0x0025660B
				// (set) Token: 0x0600A832 RID: 43058 RVA: 0x00258413 File Offset: 0x00256613
				public outStr1 outStr1 { get; private set; }

				// Token: 0x17001E13 RID: 7699
				// (get) Token: 0x0600A833 RID: 43059 RVA: 0x0025841C File Offset: 0x0025661C
				// (set) Token: 0x0600A834 RID: 43060 RVA: 0x00258424 File Offset: 0x00256624
				public segmentCase segmentCase { get; private set; }

				// Token: 0x17001E14 RID: 7700
				// (get) Token: 0x0600A835 RID: 43061 RVA: 0x0025842D File Offset: 0x0025662D
				// (set) Token: 0x0600A836 RID: 43062 RVA: 0x00258435 File Offset: 0x00256635
				public segment segment { get; private set; }

				// Token: 0x17001E15 RID: 7701
				// (get) Token: 0x0600A837 RID: 43063 RVA: 0x0025843E File Offset: 0x0025663E
				// (set) Token: 0x0600A838 RID: 43064 RVA: 0x00258446 File Offset: 0x00256646
				public formatted formatted { get; private set; }

				// Token: 0x17001E16 RID: 7702
				// (get) Token: 0x0600A839 RID: 43065 RVA: 0x0025844F File Offset: 0x0025664F
				// (set) Token: 0x0600A83A RID: 43066 RVA: 0x00258457 File Offset: 0x00256657
				public concatEntry concatEntry { get; private set; }

				// Token: 0x17001E17 RID: 7703
				// (get) Token: 0x0600A83B RID: 43067 RVA: 0x00258460 File Offset: 0x00256660
				// (set) Token: 0x0600A83C RID: 43068 RVA: 0x00258468 File Offset: 0x00256668
				public concatCase concatCase { get; private set; }

				// Token: 0x17001E18 RID: 7704
				// (get) Token: 0x0600A83D RID: 43069 RVA: 0x00258471 File Offset: 0x00256671
				// (set) Token: 0x0600A83E RID: 43070 RVA: 0x00258479 File Offset: 0x00256679
				public concat concat { get; private set; }

				// Token: 0x17001E19 RID: 7705
				// (get) Token: 0x0600A83F RID: 43071 RVA: 0x00258482 File Offset: 0x00256682
				// (set) Token: 0x0600A840 RID: 43072 RVA: 0x0025848A File Offset: 0x0025668A
				public concatPrefix concatPrefix { get; private set; }

				// Token: 0x17001E1A RID: 7706
				// (get) Token: 0x0600A841 RID: 43073 RVA: 0x00258493 File Offset: 0x00256693
				// (set) Token: 0x0600A842 RID: 43074 RVA: 0x0025849B File Offset: 0x0025669B
				public concatSegment concatSegment { get; private set; }

				// Token: 0x17001E1B RID: 7707
				// (get) Token: 0x0600A843 RID: 43075 RVA: 0x002584A4 File Offset: 0x002566A4
				// (set) Token: 0x0600A844 RID: 43076 RVA: 0x002584AC File Offset: 0x002566AC
				public concatSuffix concatSuffix { get; private set; }

				// Token: 0x17001E1C RID: 7708
				// (get) Token: 0x0600A845 RID: 43077 RVA: 0x002584B5 File Offset: 0x002566B5
				// (set) Token: 0x0600A846 RID: 43078 RVA: 0x002584BD File Offset: 0x002566BD
				public condition condition { get; private set; }

				// Token: 0x17001E1D RID: 7709
				// (get) Token: 0x0600A847 RID: 43079 RVA: 0x002584C6 File Offset: 0x002566C6
				// (set) Token: 0x0600A848 RID: 43080 RVA: 0x002584CE File Offset: 0x002566CE
				public or or { get; private set; }

				// Token: 0x17001E1E RID: 7710
				// (get) Token: 0x0600A849 RID: 43081 RVA: 0x002584D7 File Offset: 0x002566D7
				// (set) Token: 0x0600A84A RID: 43082 RVA: 0x002584DF File Offset: 0x002566DF
				public inull inull { get; private set; }

				// Token: 0x17001E1F RID: 7711
				// (get) Token: 0x0600A84B RID: 43083 RVA: 0x002584E8 File Offset: 0x002566E8
				// (set) Token: 0x0600A84C RID: 43084 RVA: 0x002584F0 File Offset: 0x002566F0
				public equalsText equalsText { get; private set; }

				// Token: 0x17001E20 RID: 7712
				// (get) Token: 0x0600A84D RID: 43085 RVA: 0x002584F9 File Offset: 0x002566F9
				// (set) Token: 0x0600A84E RID: 43086 RVA: 0x00258501 File Offset: 0x00256701
				public containsFindText containsFindText { get; private set; }

				// Token: 0x17001E21 RID: 7713
				// (get) Token: 0x0600A84F RID: 43087 RVA: 0x0025850A File Offset: 0x0025670A
				// (set) Token: 0x0600A850 RID: 43088 RVA: 0x00258512 File Offset: 0x00256712
				public startsWithFindText startsWithFindText { get; private set; }

				// Token: 0x17001E22 RID: 7714
				// (get) Token: 0x0600A851 RID: 43089 RVA: 0x0025851B File Offset: 0x0025671B
				// (set) Token: 0x0600A852 RID: 43090 RVA: 0x00258523 File Offset: 0x00256723
				public isMatchRegex isMatchRegex { get; private set; }

				// Token: 0x17001E23 RID: 7715
				// (get) Token: 0x0600A853 RID: 43091 RVA: 0x0025852C File Offset: 0x0025672C
				// (set) Token: 0x0600A854 RID: 43092 RVA: 0x00258534 File Offset: 0x00256734
				public containsMatchRegex containsMatchRegex { get; private set; }

				// Token: 0x17001E24 RID: 7716
				// (get) Token: 0x0600A855 RID: 43093 RVA: 0x0025853D File Offset: 0x0025673D
				// (set) Token: 0x0600A856 RID: 43094 RVA: 0x00258545 File Offset: 0x00256745
				public containsCount containsCount { get; private set; }

				// Token: 0x17001E25 RID: 7717
				// (get) Token: 0x0600A857 RID: 43095 RVA: 0x0025854E File Offset: 0x0025674E
				// (set) Token: 0x0600A858 RID: 43096 RVA: 0x00258556 File Offset: 0x00256756
				public matchCount matchCount { get; private set; }

				// Token: 0x17001E26 RID: 7718
				// (get) Token: 0x0600A859 RID: 43097 RVA: 0x0025855F File Offset: 0x0025675F
				// (set) Token: 0x0600A85A RID: 43098 RVA: 0x00258567 File Offset: 0x00256767
				public numberEqualsValue numberEqualsValue { get; private set; }

				// Token: 0x17001E27 RID: 7719
				// (get) Token: 0x0600A85B RID: 43099 RVA: 0x00258570 File Offset: 0x00256770
				// (set) Token: 0x0600A85C RID: 43100 RVA: 0x00258578 File Offset: 0x00256778
				public numberGreaterThanValue numberGreaterThanValue { get; private set; }

				// Token: 0x17001E28 RID: 7720
				// (get) Token: 0x0600A85D RID: 43101 RVA: 0x00258581 File Offset: 0x00256781
				// (set) Token: 0x0600A85E RID: 43102 RVA: 0x00258589 File Offset: 0x00256789
				public numberLessThanValue numberLessThanValue { get; private set; }

				// Token: 0x17001E29 RID: 7721
				// (get) Token: 0x0600A85F RID: 43103 RVA: 0x00258592 File Offset: 0x00256792
				// (set) Token: 0x0600A860 RID: 43104 RVA: 0x0025859A File Offset: 0x0025679A
				public formatNumber formatNumber { get; private set; }

				// Token: 0x17001E2A RID: 7722
				// (get) Token: 0x0600A861 RID: 43105 RVA: 0x002585A3 File Offset: 0x002567A3
				// (set) Token: 0x0600A862 RID: 43106 RVA: 0x002585AB File Offset: 0x002567AB
				public number number { get; private set; }

				// Token: 0x17001E2B RID: 7723
				// (get) Token: 0x0600A863 RID: 43107 RVA: 0x002585B4 File Offset: 0x002567B4
				// (set) Token: 0x0600A864 RID: 43108 RVA: 0x002585BC File Offset: 0x002567BC
				public number1 number1 { get; private set; }

				// Token: 0x17001E2C RID: 7724
				// (get) Token: 0x0600A865 RID: 43109 RVA: 0x002585C5 File Offset: 0x002567C5
				// (set) Token: 0x0600A866 RID: 43110 RVA: 0x002585CD File Offset: 0x002567CD
				public arithmetic arithmetic { get; private set; }

				// Token: 0x17001E2D RID: 7725
				// (get) Token: 0x0600A867 RID: 43111 RVA: 0x002585D6 File Offset: 0x002567D6
				// (set) Token: 0x0600A868 RID: 43112 RVA: 0x002585DE File Offset: 0x002567DE
				public arithmeticLeft arithmeticLeft { get; private set; }

				// Token: 0x17001E2E RID: 7726
				// (get) Token: 0x0600A869 RID: 43113 RVA: 0x002585E7 File Offset: 0x002567E7
				// (set) Token: 0x0600A86A RID: 43114 RVA: 0x002585EF File Offset: 0x002567EF
				public addRight addRight { get; private set; }

				// Token: 0x17001E2F RID: 7727
				// (get) Token: 0x0600A86B RID: 43115 RVA: 0x002585F8 File Offset: 0x002567F8
				// (set) Token: 0x0600A86C RID: 43116 RVA: 0x00258600 File Offset: 0x00256800
				public subtractRight subtractRight { get; private set; }

				// Token: 0x17001E30 RID: 7728
				// (get) Token: 0x0600A86D RID: 43117 RVA: 0x00258609 File Offset: 0x00256809
				// (set) Token: 0x0600A86E RID: 43118 RVA: 0x00258611 File Offset: 0x00256811
				public multiplyRight multiplyRight { get; private set; }

				// Token: 0x17001E31 RID: 7729
				// (get) Token: 0x0600A86F RID: 43119 RVA: 0x0025861A File Offset: 0x0025681A
				// (set) Token: 0x0600A870 RID: 43120 RVA: 0x00258622 File Offset: 0x00256822
				public divideRight divideRight { get; private set; }

				// Token: 0x17001E32 RID: 7730
				// (get) Token: 0x0600A871 RID: 43121 RVA: 0x0025862B File Offset: 0x0025682B
				// (set) Token: 0x0600A872 RID: 43122 RVA: 0x00258633 File Offset: 0x00256833
				public inumber inumber { get; private set; }

				// Token: 0x17001E33 RID: 7731
				// (get) Token: 0x0600A873 RID: 43123 RVA: 0x0025863C File Offset: 0x0025683C
				// (set) Token: 0x0600A874 RID: 43124 RVA: 0x00258644 File Offset: 0x00256844
				public rowNumberTransform rowNumberTransform { get; private set; }

				// Token: 0x17001E34 RID: 7732
				// (get) Token: 0x0600A875 RID: 43125 RVA: 0x0025864D File Offset: 0x0025684D
				// (set) Token: 0x0600A876 RID: 43126 RVA: 0x00258655 File Offset: 0x00256855
				public formatDateTime formatDateTime { get; private set; }

				// Token: 0x17001E35 RID: 7733
				// (get) Token: 0x0600A877 RID: 43127 RVA: 0x0025865E File Offset: 0x0025685E
				// (set) Token: 0x0600A878 RID: 43128 RVA: 0x00258666 File Offset: 0x00256866
				public date date { get; private set; }

				// Token: 0x17001E36 RID: 7734
				// (get) Token: 0x0600A879 RID: 43129 RVA: 0x0025866F File Offset: 0x0025686F
				// (set) Token: 0x0600A87A RID: 43130 RVA: 0x00258677 File Offset: 0x00256877
				public idate idate { get; private set; }

				// Token: 0x17001E37 RID: 7735
				// (get) Token: 0x0600A87B RID: 43131 RVA: 0x00258680 File Offset: 0x00256880
				// (set) Token: 0x0600A87C RID: 43132 RVA: 0x00258688 File Offset: 0x00256888
				public itime itime { get; private set; }

				// Token: 0x17001E38 RID: 7736
				// (get) Token: 0x0600A87D RID: 43133 RVA: 0x00258691 File Offset: 0x00256891
				// (set) Token: 0x0600A87E RID: 43134 RVA: 0x00258699 File Offset: 0x00256899
				public parseSubject parseSubject { get; private set; }

				// Token: 0x17001E39 RID: 7737
				// (get) Token: 0x0600A87F RID: 43135 RVA: 0x002586A2 File Offset: 0x002568A2
				// (set) Token: 0x0600A880 RID: 43136 RVA: 0x002586AA File Offset: 0x002568AA
				public letSubstring letSubstring { get; private set; }

				// Token: 0x17001E3A RID: 7738
				// (get) Token: 0x0600A881 RID: 43137 RVA: 0x002586B3 File Offset: 0x002568B3
				// (set) Token: 0x0600A882 RID: 43138 RVA: 0x002586BB File Offset: 0x002568BB
				public x x { get; private set; }

				// Token: 0x17001E3B RID: 7739
				// (get) Token: 0x0600A883 RID: 43139 RVA: 0x002586C4 File Offset: 0x002568C4
				// (set) Token: 0x0600A884 RID: 43140 RVA: 0x002586CC File Offset: 0x002568CC
				public substring substring { get; private set; }

				// Token: 0x17001E3C RID: 7740
				// (get) Token: 0x0600A885 RID: 43141 RVA: 0x002586D5 File Offset: 0x002568D5
				// (set) Token: 0x0600A886 RID: 43142 RVA: 0x002586DD File Offset: 0x002568DD
				public splitTrim splitTrim { get; private set; }

				// Token: 0x17001E3D RID: 7741
				// (get) Token: 0x0600A887 RID: 43143 RVA: 0x002586E6 File Offset: 0x002568E6
				// (set) Token: 0x0600A888 RID: 43144 RVA: 0x002586EE File Offset: 0x002568EE
				public split split { get; private set; }

				// Token: 0x17001E3E RID: 7742
				// (get) Token: 0x0600A889 RID: 43145 RVA: 0x002586F7 File Offset: 0x002568F7
				// (set) Token: 0x0600A88A RID: 43146 RVA: 0x002586FF File Offset: 0x002568FF
				public sliceTrim sliceTrim { get; private set; }

				// Token: 0x17001E3F RID: 7743
				// (get) Token: 0x0600A88B RID: 43147 RVA: 0x00258708 File Offset: 0x00256908
				// (set) Token: 0x0600A88C RID: 43148 RVA: 0x00258710 File Offset: 0x00256910
				public slice slice { get; private set; }

				// Token: 0x17001E40 RID: 7744
				// (get) Token: 0x0600A88D RID: 43149 RVA: 0x00258719 File Offset: 0x00256919
				// (set) Token: 0x0600A88E RID: 43150 RVA: 0x00258721 File Offset: 0x00256921
				public pos pos { get; private set; }

				// Token: 0x17001E41 RID: 7745
				// (get) Token: 0x0600A88F RID: 43151 RVA: 0x0025872A File Offset: 0x0025692A
				// (set) Token: 0x0600A890 RID: 43152 RVA: 0x00258732 File Offset: 0x00256932
				public fromStrTrim fromStrTrim { get; private set; }

				// Token: 0x17001E42 RID: 7746
				// (get) Token: 0x0600A891 RID: 43153 RVA: 0x0025873B File Offset: 0x0025693B
				// (set) Token: 0x0600A892 RID: 43154 RVA: 0x00258743 File Offset: 0x00256943
				public fromStr fromStr { get; private set; }

				// Token: 0x17001E43 RID: 7747
				// (get) Token: 0x0600A893 RID: 43155 RVA: 0x0025874C File Offset: 0x0025694C
				// (set) Token: 0x0600A894 RID: 43156 RVA: 0x00258754 File Offset: 0x00256954
				public fromNumberStr fromNumberStr { get; private set; }

				// Token: 0x17001E44 RID: 7748
				// (get) Token: 0x0600A895 RID: 43157 RVA: 0x0025875D File Offset: 0x0025695D
				// (set) Token: 0x0600A896 RID: 43158 RVA: 0x00258765 File Offset: 0x00256965
				public fromNumber fromNumber { get; private set; }

				// Token: 0x17001E45 RID: 7749
				// (get) Token: 0x0600A897 RID: 43159 RVA: 0x0025876E File Offset: 0x0025696E
				// (set) Token: 0x0600A898 RID: 43160 RVA: 0x00258776 File Offset: 0x00256976
				public fromNumberCoalesced fromNumberCoalesced { get; private set; }

				// Token: 0x17001E46 RID: 7750
				// (get) Token: 0x0600A899 RID: 43161 RVA: 0x0025877F File Offset: 0x0025697F
				// (set) Token: 0x0600A89A RID: 43162 RVA: 0x00258787 File Offset: 0x00256987
				public fromRowNumber fromRowNumber { get; private set; }

				// Token: 0x17001E47 RID: 7751
				// (get) Token: 0x0600A89B RID: 43163 RVA: 0x00258790 File Offset: 0x00256990
				// (set) Token: 0x0600A89C RID: 43164 RVA: 0x00258798 File Offset: 0x00256998
				public fromNumbers fromNumbers { get; private set; }

				// Token: 0x17001E48 RID: 7752
				// (get) Token: 0x0600A89D RID: 43165 RVA: 0x002587A1 File Offset: 0x002569A1
				// (set) Token: 0x0600A89E RID: 43166 RVA: 0x002587A9 File Offset: 0x002569A9
				public fromDateTime fromDateTime { get; private set; }

				// Token: 0x17001E49 RID: 7753
				// (get) Token: 0x0600A89F RID: 43167 RVA: 0x002587B2 File Offset: 0x002569B2
				// (set) Token: 0x0600A8A0 RID: 43168 RVA: 0x002587BA File Offset: 0x002569BA
				public fromDateTimePart fromDateTimePart { get; private set; }

				// Token: 0x17001E4A RID: 7754
				// (get) Token: 0x0600A8A1 RID: 43169 RVA: 0x002587C3 File Offset: 0x002569C3
				// (set) Token: 0x0600A8A2 RID: 43170 RVA: 0x002587CB File Offset: 0x002569CB
				public fromTime fromTime { get; private set; }

				// Token: 0x17001E4B RID: 7755
				// (get) Token: 0x0600A8A3 RID: 43171 RVA: 0x002587D4 File Offset: 0x002569D4
				// (set) Token: 0x0600A8A4 RID: 43172 RVA: 0x002587DC File Offset: 0x002569DC
				public constString constString { get; private set; }

				// Token: 0x17001E4C RID: 7756
				// (get) Token: 0x0600A8A5 RID: 43173 RVA: 0x002587E5 File Offset: 0x002569E5
				// (set) Token: 0x0600A8A6 RID: 43174 RVA: 0x002587ED File Offset: 0x002569ED
				public constNumber constNumber { get; private set; }

				// Token: 0x17001E4D RID: 7757
				// (get) Token: 0x0600A8A7 RID: 43175 RVA: 0x002587F6 File Offset: 0x002569F6
				// (set) Token: 0x0600A8A8 RID: 43176 RVA: 0x002587FE File Offset: 0x002569FE
				public constDate constDate { get; private set; }

				// Token: 0x17001E4E RID: 7758
				// (get) Token: 0x0600A8A9 RID: 43177 RVA: 0x00258807 File Offset: 0x00256A07
				// (set) Token: 0x0600A8AA RID: 43178 RVA: 0x0025880F File Offset: 0x00256A0F
				public columnName columnName { get; private set; }

				// Token: 0x17001E4F RID: 7759
				// (get) Token: 0x0600A8AB RID: 43179 RVA: 0x00258818 File Offset: 0x00256A18
				// (set) Token: 0x0600A8AC RID: 43180 RVA: 0x00258820 File Offset: 0x00256A20
				public columnNames columnNames { get; private set; }

				// Token: 0x17001E50 RID: 7760
				// (get) Token: 0x0600A8AD RID: 43181 RVA: 0x00258829 File Offset: 0x00256A29
				// (set) Token: 0x0600A8AE RID: 43182 RVA: 0x00258831 File Offset: 0x00256A31
				public constStr constStr { get; private set; }

				// Token: 0x17001E51 RID: 7761
				// (get) Token: 0x0600A8AF RID: 43183 RVA: 0x0025883A File Offset: 0x00256A3A
				// (set) Token: 0x0600A8B0 RID: 43184 RVA: 0x00258842 File Offset: 0x00256A42
				public constNum constNum { get; private set; }

				// Token: 0x17001E52 RID: 7762
				// (get) Token: 0x0600A8B1 RID: 43185 RVA: 0x0025884B File Offset: 0x00256A4B
				// (set) Token: 0x0600A8B2 RID: 43186 RVA: 0x00258853 File Offset: 0x00256A53
				public constDt constDt { get; private set; }

				// Token: 0x17001E53 RID: 7763
				// (get) Token: 0x0600A8B3 RID: 43187 RVA: 0x0025885C File Offset: 0x00256A5C
				// (set) Token: 0x0600A8B4 RID: 43188 RVA: 0x00258864 File Offset: 0x00256A64
				public locale locale { get; private set; }

				// Token: 0x17001E54 RID: 7764
				// (get) Token: 0x0600A8B5 RID: 43189 RVA: 0x0025886D File Offset: 0x00256A6D
				// (set) Token: 0x0600A8B6 RID: 43190 RVA: 0x00258875 File Offset: 0x00256A75
				public replaceFindText replaceFindText { get; private set; }

				// Token: 0x17001E55 RID: 7765
				// (get) Token: 0x0600A8B7 RID: 43191 RVA: 0x0025887E File Offset: 0x00256A7E
				// (set) Token: 0x0600A8B8 RID: 43192 RVA: 0x00258886 File Offset: 0x00256A86
				public replaceText replaceText { get; private set; }

				// Token: 0x17001E56 RID: 7766
				// (get) Token: 0x0600A8B9 RID: 43193 RVA: 0x0025888F File Offset: 0x00256A8F
				// (set) Token: 0x0600A8BA RID: 43194 RVA: 0x00258897 File Offset: 0x00256A97
				public sliceBetweenStartText sliceBetweenStartText { get; private set; }

				// Token: 0x17001E57 RID: 7767
				// (get) Token: 0x0600A8BB RID: 43195 RVA: 0x002588A0 File Offset: 0x00256AA0
				// (set) Token: 0x0600A8BC RID: 43196 RVA: 0x002588A8 File Offset: 0x00256AA8
				public sliceBetweenEndText sliceBetweenEndText { get; private set; }

				// Token: 0x17001E58 RID: 7768
				// (get) Token: 0x0600A8BD RID: 43197 RVA: 0x002588B1 File Offset: 0x00256AB1
				// (set) Token: 0x0600A8BE RID: 43198 RVA: 0x002588B9 File Offset: 0x00256AB9
				public numberFormatDesc numberFormatDesc { get; private set; }

				// Token: 0x17001E59 RID: 7769
				// (get) Token: 0x0600A8BF RID: 43199 RVA: 0x002588C2 File Offset: 0x00256AC2
				// (set) Token: 0x0600A8C0 RID: 43200 RVA: 0x002588CA File Offset: 0x00256ACA
				public numberRoundDesc numberRoundDesc { get; private set; }

				// Token: 0x17001E5A RID: 7770
				// (get) Token: 0x0600A8C1 RID: 43201 RVA: 0x002588D3 File Offset: 0x00256AD3
				// (set) Token: 0x0600A8C2 RID: 43202 RVA: 0x002588DB File Offset: 0x00256ADB
				public dateTimeRoundDesc dateTimeRoundDesc { get; private set; }

				// Token: 0x17001E5B RID: 7771
				// (get) Token: 0x0600A8C3 RID: 43203 RVA: 0x002588E4 File Offset: 0x00256AE4
				// (set) Token: 0x0600A8C4 RID: 43204 RVA: 0x002588EC File Offset: 0x00256AEC
				public dateTimeFormatDesc dateTimeFormatDesc { get; private set; }

				// Token: 0x17001E5C RID: 7772
				// (get) Token: 0x0600A8C5 RID: 43205 RVA: 0x002588F5 File Offset: 0x00256AF5
				// (set) Token: 0x0600A8C6 RID: 43206 RVA: 0x002588FD File Offset: 0x00256AFD
				public dateTimeParseDesc dateTimeParseDesc { get; private set; }

				// Token: 0x17001E5D RID: 7773
				// (get) Token: 0x0600A8C7 RID: 43207 RVA: 0x00258906 File Offset: 0x00256B06
				// (set) Token: 0x0600A8C8 RID: 43208 RVA: 0x0025890E File Offset: 0x00256B0E
				public dateTimePartKind dateTimePartKind { get; private set; }

				// Token: 0x17001E5E RID: 7774
				// (get) Token: 0x0600A8C9 RID: 43209 RVA: 0x00258917 File Offset: 0x00256B17
				// (set) Token: 0x0600A8CA RID: 43210 RVA: 0x0025891F File Offset: 0x00256B1F
				public fromDateTimePartKind fromDateTimePartKind { get; private set; }

				// Token: 0x17001E5F RID: 7775
				// (get) Token: 0x0600A8CB RID: 43211 RVA: 0x00258928 File Offset: 0x00256B28
				// (set) Token: 0x0600A8CC RID: 43212 RVA: 0x00258930 File Offset: 0x00256B30
				public timePartKind timePartKind { get; private set; }

				// Token: 0x17001E60 RID: 7776
				// (get) Token: 0x0600A8CD RID: 43213 RVA: 0x00258939 File Offset: 0x00256B39
				// (set) Token: 0x0600A8CE RID: 43214 RVA: 0x00258941 File Offset: 0x00256B41
				public rowNumberLinearTransformDesc rowNumberLinearTransformDesc { get; private set; }

				// Token: 0x17001E61 RID: 7777
				// (get) Token: 0x0600A8CF RID: 43215 RVA: 0x0025894A File Offset: 0x00256B4A
				// (set) Token: 0x0600A8D0 RID: 43216 RVA: 0x00258952 File Offset: 0x00256B52
				public matchDesc matchDesc { get; private set; }

				// Token: 0x17001E62 RID: 7778
				// (get) Token: 0x0600A8D1 RID: 43217 RVA: 0x0025895B File Offset: 0x00256B5B
				// (set) Token: 0x0600A8D2 RID: 43218 RVA: 0x00258963 File Offset: 0x00256B63
				public matchInstance matchInstance { get; private set; }

				// Token: 0x17001E63 RID: 7779
				// (get) Token: 0x0600A8D3 RID: 43219 RVA: 0x0025896C File Offset: 0x00256B6C
				// (set) Token: 0x0600A8D4 RID: 43220 RVA: 0x00258974 File Offset: 0x00256B74
				public splitDelimiter splitDelimiter { get; private set; }

				// Token: 0x17001E64 RID: 7780
				// (get) Token: 0x0600A8D5 RID: 43221 RVA: 0x0025897D File Offset: 0x00256B7D
				// (set) Token: 0x0600A8D6 RID: 43222 RVA: 0x00258985 File Offset: 0x00256B85
				public splitInstance splitInstance { get; private set; }

				// Token: 0x17001E65 RID: 7781
				// (get) Token: 0x0600A8D7 RID: 43223 RVA: 0x0025898E File Offset: 0x00256B8E
				// (set) Token: 0x0600A8D8 RID: 43224 RVA: 0x00258996 File Offset: 0x00256B96
				public findDelimiter findDelimiter { get; private set; }

				// Token: 0x17001E66 RID: 7782
				// (get) Token: 0x0600A8D9 RID: 43225 RVA: 0x0025899F File Offset: 0x00256B9F
				// (set) Token: 0x0600A8DA RID: 43226 RVA: 0x002589A7 File Offset: 0x00256BA7
				public findInstance findInstance { get; private set; }

				// Token: 0x17001E67 RID: 7783
				// (get) Token: 0x0600A8DB RID: 43227 RVA: 0x002589B0 File Offset: 0x00256BB0
				// (set) Token: 0x0600A8DC RID: 43228 RVA: 0x002589B8 File Offset: 0x00256BB8
				public findOffset findOffset { get; private set; }

				// Token: 0x17001E68 RID: 7784
				// (get) Token: 0x0600A8DD RID: 43229 RVA: 0x002589C1 File Offset: 0x00256BC1
				// (set) Token: 0x0600A8DE RID: 43230 RVA: 0x002589C9 File Offset: 0x00256BC9
				public slicePrefixAbsPos slicePrefixAbsPos { get; private set; }

				// Token: 0x17001E69 RID: 7785
				// (get) Token: 0x0600A8DF RID: 43231 RVA: 0x002589D2 File Offset: 0x00256BD2
				// (set) Token: 0x0600A8E0 RID: 43232 RVA: 0x002589DA File Offset: 0x00256BDA
				public scaleNumberFactor scaleNumberFactor { get; private set; }

				// Token: 0x17001E6A RID: 7786
				// (get) Token: 0x0600A8E1 RID: 43233 RVA: 0x002589E3 File Offset: 0x00256BE3
				// (set) Token: 0x0600A8E2 RID: 43234 RVA: 0x002589EB File Offset: 0x00256BEB
				public absPos absPos { get; private set; }

				// Token: 0x0600A8E3 RID: 43235 RVA: 0x002589F4 File Offset: 0x00256BF4
				public NodeHoles(GrammarBuilders builders)
				{
					this.result = result.CreateHole(builders, null);
					this.output = output.CreateHole(builders, null);
					this.outNumber = outNumber.CreateHole(builders, null);
					this.outDate = outDate.CreateHole(builders, null);
					this.outStr = outStr.CreateHole(builders, null);
					this.outStr1 = outStr1.CreateHole(builders, null);
					this.segmentCase = segmentCase.CreateHole(builders, null);
					this.segment = segment.CreateHole(builders, null);
					this.formatted = formatted.CreateHole(builders, null);
					this.concatEntry = concatEntry.CreateHole(builders, null);
					this.concatCase = concatCase.CreateHole(builders, null);
					this.concat = concat.CreateHole(builders, null);
					this.concatPrefix = concatPrefix.CreateHole(builders, null);
					this.concatSegment = concatSegment.CreateHole(builders, null);
					this.concatSuffix = concatSuffix.CreateHole(builders, null);
					this.condition = condition.CreateHole(builders, null);
					this.or = or.CreateHole(builders, null);
					this.inull = inull.CreateHole(builders, null);
					this.equalsText = equalsText.CreateHole(builders, null);
					this.containsFindText = containsFindText.CreateHole(builders, null);
					this.startsWithFindText = startsWithFindText.CreateHole(builders, null);
					this.isMatchRegex = isMatchRegex.CreateHole(builders, null);
					this.containsMatchRegex = containsMatchRegex.CreateHole(builders, null);
					this.containsCount = containsCount.CreateHole(builders, null);
					this.matchCount = matchCount.CreateHole(builders, null);
					this.numberEqualsValue = numberEqualsValue.CreateHole(builders, null);
					this.numberGreaterThanValue = numberGreaterThanValue.CreateHole(builders, null);
					this.numberLessThanValue = numberLessThanValue.CreateHole(builders, null);
					this.formatNumber = formatNumber.CreateHole(builders, null);
					this.number = number.CreateHole(builders, null);
					this.number1 = number1.CreateHole(builders, null);
					this.arithmetic = arithmetic.CreateHole(builders, null);
					this.arithmeticLeft = arithmeticLeft.CreateHole(builders, null);
					this.addRight = addRight.CreateHole(builders, null);
					this.subtractRight = subtractRight.CreateHole(builders, null);
					this.multiplyRight = multiplyRight.CreateHole(builders, null);
					this.divideRight = divideRight.CreateHole(builders, null);
					this.inumber = inumber.CreateHole(builders, null);
					this.rowNumberTransform = rowNumberTransform.CreateHole(builders, null);
					this.formatDateTime = formatDateTime.CreateHole(builders, null);
					this.date = date.CreateHole(builders, null);
					this.idate = idate.CreateHole(builders, null);
					this.itime = itime.CreateHole(builders, null);
					this.parseSubject = parseSubject.CreateHole(builders, null);
					this.letSubstring = letSubstring.CreateHole(builders, null);
					this.x = x.CreateHole(builders, null);
					this.substring = substring.CreateHole(builders, null);
					this.splitTrim = splitTrim.CreateHole(builders, null);
					this.split = split.CreateHole(builders, null);
					this.sliceTrim = sliceTrim.CreateHole(builders, null);
					this.slice = slice.CreateHole(builders, null);
					this.pos = pos.CreateHole(builders, null);
					this.fromStrTrim = fromStrTrim.CreateHole(builders, null);
					this.fromStr = fromStr.CreateHole(builders, null);
					this.fromNumberStr = fromNumberStr.CreateHole(builders, null);
					this.fromNumber = fromNumber.CreateHole(builders, null);
					this.fromNumberCoalesced = fromNumberCoalesced.CreateHole(builders, null);
					this.fromRowNumber = fromRowNumber.CreateHole(builders, null);
					this.fromNumbers = fromNumbers.CreateHole(builders, null);
					this.fromDateTime = fromDateTime.CreateHole(builders, null);
					this.fromDateTimePart = fromDateTimePart.CreateHole(builders, null);
					this.fromTime = fromTime.CreateHole(builders, null);
					this.constString = constString.CreateHole(builders, null);
					this.constNumber = constNumber.CreateHole(builders, null);
					this.constDate = constDate.CreateHole(builders, null);
					this.columnName = columnName.CreateHole(builders, null);
					this.columnNames = columnNames.CreateHole(builders, null);
					this.constStr = constStr.CreateHole(builders, null);
					this.constNum = constNum.CreateHole(builders, null);
					this.constDt = constDt.CreateHole(builders, null);
					this.locale = locale.CreateHole(builders, null);
					this.replaceFindText = replaceFindText.CreateHole(builders, null);
					this.replaceText = replaceText.CreateHole(builders, null);
					this.sliceBetweenStartText = sliceBetweenStartText.CreateHole(builders, null);
					this.sliceBetweenEndText = sliceBetweenEndText.CreateHole(builders, null);
					this.numberFormatDesc = numberFormatDesc.CreateHole(builders, null);
					this.numberRoundDesc = numberRoundDesc.CreateHole(builders, null);
					this.dateTimeRoundDesc = dateTimeRoundDesc.CreateHole(builders, null);
					this.dateTimeFormatDesc = dateTimeFormatDesc.CreateHole(builders, null);
					this.dateTimeParseDesc = dateTimeParseDesc.CreateHole(builders, null);
					this.dateTimePartKind = dateTimePartKind.CreateHole(builders, null);
					this.fromDateTimePartKind = fromDateTimePartKind.CreateHole(builders, null);
					this.timePartKind = timePartKind.CreateHole(builders, null);
					this.rowNumberLinearTransformDesc = rowNumberLinearTransformDesc.CreateHole(builders, null);
					this.matchDesc = matchDesc.CreateHole(builders, null);
					this.matchInstance = matchInstance.CreateHole(builders, null);
					this.splitDelimiter = splitDelimiter.CreateHole(builders, null);
					this.splitInstance = splitInstance.CreateHole(builders, null);
					this.findDelimiter = findDelimiter.CreateHole(builders, null);
					this.findInstance = findInstance.CreateHole(builders, null);
					this.findOffset = findOffset.CreateHole(builders, null);
					this.slicePrefixAbsPos = slicePrefixAbsPos.CreateHole(builders, null);
					this.scaleNumberFactor = scaleNumberFactor.CreateHole(builders, null);
					this.absPos = absPos.CreateHole(builders, null);
				}
			}

			// Token: 0x02001502 RID: 5378
			public class NodeUnsafe
			{
				// Token: 0x0600A8E4 RID: 43236 RVA: 0x00258ECD File Offset: 0x002570CD
				public result result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result.CreateUnsafe(node);
				}

				// Token: 0x0600A8E5 RID: 43237 RVA: 0x00258ED5 File Offset: 0x002570D5
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x0600A8E6 RID: 43238 RVA: 0x00258EDD File Offset: 0x002570DD
				public outNumber outNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber.CreateUnsafe(node);
				}

				// Token: 0x0600A8E7 RID: 43239 RVA: 0x00258EE5 File Offset: 0x002570E5
				public outDate outDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate.CreateUnsafe(node);
				}

				// Token: 0x0600A8E8 RID: 43240 RVA: 0x00258EED File Offset: 0x002570ED
				public outStr outStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr.CreateUnsafe(node);
				}

				// Token: 0x0600A8E9 RID: 43241 RVA: 0x00258EF5 File Offset: 0x002570F5
				public outStr1 outStr1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1.CreateUnsafe(node);
				}

				// Token: 0x0600A8EA RID: 43242 RVA: 0x00258EFD File Offset: 0x002570FD
				public segmentCase segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase.CreateUnsafe(node);
				}

				// Token: 0x0600A8EB RID: 43243 RVA: 0x00258F05 File Offset: 0x00257105
				public segment segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment.CreateUnsafe(node);
				}

				// Token: 0x0600A8EC RID: 43244 RVA: 0x00258F0D File Offset: 0x0025710D
				public formatted formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted.CreateUnsafe(node);
				}

				// Token: 0x0600A8ED RID: 43245 RVA: 0x00258F15 File Offset: 0x00257115
				public concatEntry concatEntry(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry.CreateUnsafe(node);
				}

				// Token: 0x0600A8EE RID: 43246 RVA: 0x00258F1D File Offset: 0x0025711D
				public concatCase concatCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase.CreateUnsafe(node);
				}

				// Token: 0x0600A8EF RID: 43247 RVA: 0x00258F25 File Offset: 0x00257125
				public concat concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat.CreateUnsafe(node);
				}

				// Token: 0x0600A8F0 RID: 43248 RVA: 0x00258F2D File Offset: 0x0025712D
				public concatPrefix concatPrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix.CreateUnsafe(node);
				}

				// Token: 0x0600A8F1 RID: 43249 RVA: 0x00258F35 File Offset: 0x00257135
				public concatSegment concatSegment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment.CreateUnsafe(node);
				}

				// Token: 0x0600A8F2 RID: 43250 RVA: 0x00258F3D File Offset: 0x0025713D
				public concatSuffix concatSuffix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix.CreateUnsafe(node);
				}

				// Token: 0x0600A8F3 RID: 43251 RVA: 0x00258F45 File Offset: 0x00257145
				public condition condition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition.CreateUnsafe(node);
				}

				// Token: 0x0600A8F4 RID: 43252 RVA: 0x00258F4D File Offset: 0x0025714D
				public or or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or.CreateUnsafe(node);
				}

				// Token: 0x0600A8F5 RID: 43253 RVA: 0x00258F55 File Offset: 0x00257155
				public inull inull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull.CreateUnsafe(node);
				}

				// Token: 0x0600A8F6 RID: 43254 RVA: 0x00258F5D File Offset: 0x0025715D
				public equalsText equalsText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText.CreateUnsafe(node);
				}

				// Token: 0x0600A8F7 RID: 43255 RVA: 0x00258F65 File Offset: 0x00257165
				public containsFindText containsFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText.CreateUnsafe(node);
				}

				// Token: 0x0600A8F8 RID: 43256 RVA: 0x00258F6D File Offset: 0x0025716D
				public startsWithFindText startsWithFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText.CreateUnsafe(node);
				}

				// Token: 0x0600A8F9 RID: 43257 RVA: 0x00258F75 File Offset: 0x00257175
				public isMatchRegex isMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex.CreateUnsafe(node);
				}

				// Token: 0x0600A8FA RID: 43258 RVA: 0x00258F7D File Offset: 0x0025717D
				public containsMatchRegex containsMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex.CreateUnsafe(node);
				}

				// Token: 0x0600A8FB RID: 43259 RVA: 0x00258F85 File Offset: 0x00257185
				public containsCount containsCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount.CreateUnsafe(node);
				}

				// Token: 0x0600A8FC RID: 43260 RVA: 0x00258F8D File Offset: 0x0025718D
				public matchCount matchCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount.CreateUnsafe(node);
				}

				// Token: 0x0600A8FD RID: 43261 RVA: 0x00258F95 File Offset: 0x00257195
				public numberEqualsValue numberEqualsValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue.CreateUnsafe(node);
				}

				// Token: 0x0600A8FE RID: 43262 RVA: 0x00258F9D File Offset: 0x0025719D
				public numberGreaterThanValue numberGreaterThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue.CreateUnsafe(node);
				}

				// Token: 0x0600A8FF RID: 43263 RVA: 0x00258FA5 File Offset: 0x002571A5
				public numberLessThanValue numberLessThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue.CreateUnsafe(node);
				}

				// Token: 0x0600A900 RID: 43264 RVA: 0x00258FAD File Offset: 0x002571AD
				public formatNumber formatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber.CreateUnsafe(node);
				}

				// Token: 0x0600A901 RID: 43265 RVA: 0x00258FB5 File Offset: 0x002571B5
				public number number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number.CreateUnsafe(node);
				}

				// Token: 0x0600A902 RID: 43266 RVA: 0x00258FBD File Offset: 0x002571BD
				public number1 number1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1.CreateUnsafe(node);
				}

				// Token: 0x0600A903 RID: 43267 RVA: 0x00258FC5 File Offset: 0x002571C5
				public arithmetic arithmetic(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic.CreateUnsafe(node);
				}

				// Token: 0x0600A904 RID: 43268 RVA: 0x00258FCD File Offset: 0x002571CD
				public arithmeticLeft arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft.CreateUnsafe(node);
				}

				// Token: 0x0600A905 RID: 43269 RVA: 0x00258FD5 File Offset: 0x002571D5
				public addRight addRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight.CreateUnsafe(node);
				}

				// Token: 0x0600A906 RID: 43270 RVA: 0x00258FDD File Offset: 0x002571DD
				public subtractRight subtractRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight.CreateUnsafe(node);
				}

				// Token: 0x0600A907 RID: 43271 RVA: 0x00258FE5 File Offset: 0x002571E5
				public multiplyRight multiplyRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight.CreateUnsafe(node);
				}

				// Token: 0x0600A908 RID: 43272 RVA: 0x00258FED File Offset: 0x002571ED
				public divideRight divideRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight.CreateUnsafe(node);
				}

				// Token: 0x0600A909 RID: 43273 RVA: 0x00258FF5 File Offset: 0x002571F5
				public inumber inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber.CreateUnsafe(node);
				}

				// Token: 0x0600A90A RID: 43274 RVA: 0x00258FFD File Offset: 0x002571FD
				public rowNumberTransform rowNumberTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform.CreateUnsafe(node);
				}

				// Token: 0x0600A90B RID: 43275 RVA: 0x00259005 File Offset: 0x00257205
				public formatDateTime formatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime.CreateUnsafe(node);
				}

				// Token: 0x0600A90C RID: 43276 RVA: 0x0025900D File Offset: 0x0025720D
				public date date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date.CreateUnsafe(node);
				}

				// Token: 0x0600A90D RID: 43277 RVA: 0x00259015 File Offset: 0x00257215
				public idate idate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate.CreateUnsafe(node);
				}

				// Token: 0x0600A90E RID: 43278 RVA: 0x0025901D File Offset: 0x0025721D
				public itime itime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime.CreateUnsafe(node);
				}

				// Token: 0x0600A90F RID: 43279 RVA: 0x00259025 File Offset: 0x00257225
				public parseSubject parseSubject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject.CreateUnsafe(node);
				}

				// Token: 0x0600A910 RID: 43280 RVA: 0x0025902D File Offset: 0x0025722D
				public letSubstring letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring.CreateUnsafe(node);
				}

				// Token: 0x0600A911 RID: 43281 RVA: 0x00259035 File Offset: 0x00257235
				public x x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x.CreateUnsafe(node);
				}

				// Token: 0x0600A912 RID: 43282 RVA: 0x0025903D File Offset: 0x0025723D
				public substring substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring.CreateUnsafe(node);
				}

				// Token: 0x0600A913 RID: 43283 RVA: 0x00259045 File Offset: 0x00257245
				public splitTrim splitTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim.CreateUnsafe(node);
				}

				// Token: 0x0600A914 RID: 43284 RVA: 0x0025904D File Offset: 0x0025724D
				public split split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split.CreateUnsafe(node);
				}

				// Token: 0x0600A915 RID: 43285 RVA: 0x00259055 File Offset: 0x00257255
				public sliceTrim sliceTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim.CreateUnsafe(node);
				}

				// Token: 0x0600A916 RID: 43286 RVA: 0x0025905D File Offset: 0x0025725D
				public slice slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice.CreateUnsafe(node);
				}

				// Token: 0x0600A917 RID: 43287 RVA: 0x00259065 File Offset: 0x00257265
				public pos pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos.CreateUnsafe(node);
				}

				// Token: 0x0600A918 RID: 43288 RVA: 0x0025906D File Offset: 0x0025726D
				public fromStrTrim fromStrTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim.CreateUnsafe(node);
				}

				// Token: 0x0600A919 RID: 43289 RVA: 0x00259075 File Offset: 0x00257275
				public fromStr fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr.CreateUnsafe(node);
				}

				// Token: 0x0600A91A RID: 43290 RVA: 0x0025907D File Offset: 0x0025727D
				public fromNumberStr fromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr.CreateUnsafe(node);
				}

				// Token: 0x0600A91B RID: 43291 RVA: 0x00259085 File Offset: 0x00257285
				public fromNumber fromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber.CreateUnsafe(node);
				}

				// Token: 0x0600A91C RID: 43292 RVA: 0x0025908D File Offset: 0x0025728D
				public fromNumberCoalesced fromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced.CreateUnsafe(node);
				}

				// Token: 0x0600A91D RID: 43293 RVA: 0x00259095 File Offset: 0x00257295
				public fromRowNumber fromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber.CreateUnsafe(node);
				}

				// Token: 0x0600A91E RID: 43294 RVA: 0x0025909D File Offset: 0x0025729D
				public fromNumbers fromNumbers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers.CreateUnsafe(node);
				}

				// Token: 0x0600A91F RID: 43295 RVA: 0x002590A5 File Offset: 0x002572A5
				public fromDateTime fromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime.CreateUnsafe(node);
				}

				// Token: 0x0600A920 RID: 43296 RVA: 0x002590AD File Offset: 0x002572AD
				public fromDateTimePart fromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart.CreateUnsafe(node);
				}

				// Token: 0x0600A921 RID: 43297 RVA: 0x002590B5 File Offset: 0x002572B5
				public fromTime fromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime.CreateUnsafe(node);
				}

				// Token: 0x0600A922 RID: 43298 RVA: 0x002590BD File Offset: 0x002572BD
				public constString constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString.CreateUnsafe(node);
				}

				// Token: 0x0600A923 RID: 43299 RVA: 0x002590C5 File Offset: 0x002572C5
				public constNumber constNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber.CreateUnsafe(node);
				}

				// Token: 0x0600A924 RID: 43300 RVA: 0x002590CD File Offset: 0x002572CD
				public constDate constDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate.CreateUnsafe(node);
				}

				// Token: 0x0600A925 RID: 43301 RVA: 0x002590D5 File Offset: 0x002572D5
				public columnName columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName.CreateUnsafe(node);
				}

				// Token: 0x0600A926 RID: 43302 RVA: 0x002590DD File Offset: 0x002572DD
				public columnNames columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames.CreateUnsafe(node);
				}

				// Token: 0x0600A927 RID: 43303 RVA: 0x002590E5 File Offset: 0x002572E5
				public constStr constStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr.CreateUnsafe(node);
				}

				// Token: 0x0600A928 RID: 43304 RVA: 0x002590ED File Offset: 0x002572ED
				public constNum constNum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum.CreateUnsafe(node);
				}

				// Token: 0x0600A929 RID: 43305 RVA: 0x002590F5 File Offset: 0x002572F5
				public constDt constDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt.CreateUnsafe(node);
				}

				// Token: 0x0600A92A RID: 43306 RVA: 0x002590FD File Offset: 0x002572FD
				public locale locale(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale.CreateUnsafe(node);
				}

				// Token: 0x0600A92B RID: 43307 RVA: 0x00259105 File Offset: 0x00257305
				public replaceFindText replaceFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText.CreateUnsafe(node);
				}

				// Token: 0x0600A92C RID: 43308 RVA: 0x0025910D File Offset: 0x0025730D
				public replaceText replaceText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText.CreateUnsafe(node);
				}

				// Token: 0x0600A92D RID: 43309 RVA: 0x00259115 File Offset: 0x00257315
				public sliceBetweenStartText sliceBetweenStartText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText.CreateUnsafe(node);
				}

				// Token: 0x0600A92E RID: 43310 RVA: 0x0025911D File Offset: 0x0025731D
				public sliceBetweenEndText sliceBetweenEndText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText.CreateUnsafe(node);
				}

				// Token: 0x0600A92F RID: 43311 RVA: 0x00259125 File Offset: 0x00257325
				public numberFormatDesc numberFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A930 RID: 43312 RVA: 0x0025912D File Offset: 0x0025732D
				public numberRoundDesc numberRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A931 RID: 43313 RVA: 0x00259135 File Offset: 0x00257335
				public dateTimeRoundDesc dateTimeRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A932 RID: 43314 RVA: 0x0025913D File Offset: 0x0025733D
				public dateTimeFormatDesc dateTimeFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A933 RID: 43315 RVA: 0x00259145 File Offset: 0x00257345
				public dateTimeParseDesc dateTimeParseDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A934 RID: 43316 RVA: 0x0025914D File Offset: 0x0025734D
				public dateTimePartKind dateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind.CreateUnsafe(node);
				}

				// Token: 0x0600A935 RID: 43317 RVA: 0x00259155 File Offset: 0x00257355
				public fromDateTimePartKind fromDateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind.CreateUnsafe(node);
				}

				// Token: 0x0600A936 RID: 43318 RVA: 0x0025915D File Offset: 0x0025735D
				public timePartKind timePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind.CreateUnsafe(node);
				}

				// Token: 0x0600A937 RID: 43319 RVA: 0x00259165 File Offset: 0x00257365
				public rowNumberLinearTransformDesc rowNumberLinearTransformDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A938 RID: 43320 RVA: 0x0025916D File Offset: 0x0025736D
				public matchDesc matchDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc.CreateUnsafe(node);
				}

				// Token: 0x0600A939 RID: 43321 RVA: 0x00259175 File Offset: 0x00257375
				public matchInstance matchInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance.CreateUnsafe(node);
				}

				// Token: 0x0600A93A RID: 43322 RVA: 0x0025917D File Offset: 0x0025737D
				public splitDelimiter splitDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter.CreateUnsafe(node);
				}

				// Token: 0x0600A93B RID: 43323 RVA: 0x00259185 File Offset: 0x00257385
				public splitInstance splitInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance.CreateUnsafe(node);
				}

				// Token: 0x0600A93C RID: 43324 RVA: 0x0025918D File Offset: 0x0025738D
				public findDelimiter findDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter.CreateUnsafe(node);
				}

				// Token: 0x0600A93D RID: 43325 RVA: 0x00259195 File Offset: 0x00257395
				public findInstance findInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance.CreateUnsafe(node);
				}

				// Token: 0x0600A93E RID: 43326 RVA: 0x0025919D File Offset: 0x0025739D
				public findOffset findOffset(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset.CreateUnsafe(node);
				}

				// Token: 0x0600A93F RID: 43327 RVA: 0x002591A5 File Offset: 0x002573A5
				public slicePrefixAbsPos slicePrefixAbsPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos.CreateUnsafe(node);
				}

				// Token: 0x0600A940 RID: 43328 RVA: 0x002591AD File Offset: 0x002573AD
				public scaleNumberFactor scaleNumberFactor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor.CreateUnsafe(node);
				}

				// Token: 0x0600A941 RID: 43329 RVA: 0x002591B5 File Offset: 0x002573B5
				public absPos absPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos.CreateUnsafe(node);
				}
			}

			// Token: 0x02001503 RID: 5379
			public class NodeCast
			{
				// Token: 0x0600A943 RID: 43331 RVA: 0x002591BD File Offset: 0x002573BD
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600A944 RID: 43332 RVA: 0x002591CC File Offset: 0x002573CC
				public result result(ProgramNode node)
				{
					result? result = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result.CreateSafe(this._builders, node);
					if (result == null)
					{
						string text = "node";
						string text2 = "expected node for symbol result but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return result.Value;
				}

				// Token: 0x0600A945 RID: 43333 RVA: 0x00259220 File Offset: 0x00257420
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x0600A946 RID: 43334 RVA: 0x00259274 File Offset: 0x00257474
				public outNumber outNumber(ProgramNode node)
				{
					outNumber? outNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber.CreateSafe(this._builders, node);
					if (outNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outNumber.Value;
				}

				// Token: 0x0600A947 RID: 43335 RVA: 0x002592C8 File Offset: 0x002574C8
				public outDate outDate(ProgramNode node)
				{
					outDate? outDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate.CreateSafe(this._builders, node);
					if (outDate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outDate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outDate.Value;
				}

				// Token: 0x0600A948 RID: 43336 RVA: 0x0025931C File Offset: 0x0025751C
				public outStr outStr(ProgramNode node)
				{
					outStr? outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr.CreateSafe(this._builders, node);
					if (outStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr.Value;
				}

				// Token: 0x0600A949 RID: 43337 RVA: 0x00259370 File Offset: 0x00257570
				public outStr1 outStr1(ProgramNode node)
				{
					outStr1? outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1.CreateSafe(this._builders, node);
					if (outStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr.Value;
				}

				// Token: 0x0600A94A RID: 43338 RVA: 0x002593C4 File Offset: 0x002575C4
				public segmentCase segmentCase(ProgramNode node)
				{
					segmentCase? segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase.CreateSafe(this._builders, node);
					if (segmentCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol segmentCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return segmentCase.Value;
				}

				// Token: 0x0600A94B RID: 43339 RVA: 0x00259418 File Offset: 0x00257618
				public segment segment(ProgramNode node)
				{
					segment? segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment.CreateSafe(this._builders, node);
					if (segment == null)
					{
						string text = "node";
						string text2 = "expected node for symbol segment but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return segment.Value;
				}

				// Token: 0x0600A94C RID: 43340 RVA: 0x0025946C File Offset: 0x0025766C
				public formatted formatted(ProgramNode node)
				{
					formatted? formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted.CreateSafe(this._builders, node);
					if (formatted == null)
					{
						string text = "node";
						string text2 = "expected node for symbol formatted but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatted.Value;
				}

				// Token: 0x0600A94D RID: 43341 RVA: 0x002594C0 File Offset: 0x002576C0
				public concatEntry concatEntry(ProgramNode node)
				{
					concatEntry? concatEntry = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry.CreateSafe(this._builders, node);
					if (concatEntry == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatEntry but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatEntry.Value;
				}

				// Token: 0x0600A94E RID: 43342 RVA: 0x00259514 File Offset: 0x00257714
				public concatCase concatCase(ProgramNode node)
				{
					concatCase? concatCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase.CreateSafe(this._builders, node);
					if (concatCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatCase.Value;
				}

				// Token: 0x0600A94F RID: 43343 RVA: 0x00259568 File Offset: 0x00257768
				public concat concat(ProgramNode node)
				{
					concat? concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concat.Value;
				}

				// Token: 0x0600A950 RID: 43344 RVA: 0x002595BC File Offset: 0x002577BC
				public concatPrefix concatPrefix(ProgramNode node)
				{
					concatPrefix? concatPrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix.CreateSafe(this._builders, node);
					if (concatPrefix == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatPrefix but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatPrefix.Value;
				}

				// Token: 0x0600A951 RID: 43345 RVA: 0x00259610 File Offset: 0x00257810
				public concatSegment concatSegment(ProgramNode node)
				{
					concatSegment? concatSegment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment.CreateSafe(this._builders, node);
					if (concatSegment == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSegment but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSegment.Value;
				}

				// Token: 0x0600A952 RID: 43346 RVA: 0x00259664 File Offset: 0x00257864
				public concatSuffix concatSuffix(ProgramNode node)
				{
					concatSuffix? concatSuffix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix.CreateSafe(this._builders, node);
					if (concatSuffix == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSuffix but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSuffix.Value;
				}

				// Token: 0x0600A953 RID: 43347 RVA: 0x002596B8 File Offset: 0x002578B8
				public condition condition(ProgramNode node)
				{
					condition? condition = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition.CreateSafe(this._builders, node);
					if (condition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol condition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return condition.Value;
				}

				// Token: 0x0600A954 RID: 43348 RVA: 0x0025970C File Offset: 0x0025790C
				public or or(ProgramNode node)
				{
					or? or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or.CreateSafe(this._builders, node);
					if (or == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @or but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return or.Value;
				}

				// Token: 0x0600A955 RID: 43349 RVA: 0x00259760 File Offset: 0x00257960
				public inull inull(ProgramNode node)
				{
					inull? inull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull.CreateSafe(this._builders, node);
					if (inull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inull.Value;
				}

				// Token: 0x0600A956 RID: 43350 RVA: 0x002597B4 File Offset: 0x002579B4
				public equalsText equalsText(ProgramNode node)
				{
					equalsText? equalsText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText.CreateSafe(this._builders, node);
					if (equalsText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol equalsText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return equalsText.Value;
				}

				// Token: 0x0600A957 RID: 43351 RVA: 0x00259808 File Offset: 0x00257A08
				public containsFindText containsFindText(ProgramNode node)
				{
					containsFindText? containsFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText.CreateSafe(this._builders, node);
					if (containsFindText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol containsFindText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsFindText.Value;
				}

				// Token: 0x0600A958 RID: 43352 RVA: 0x0025985C File Offset: 0x00257A5C
				public startsWithFindText startsWithFindText(ProgramNode node)
				{
					startsWithFindText? startsWithFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText.CreateSafe(this._builders, node);
					if (startsWithFindText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol startsWithFindText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWithFindText.Value;
				}

				// Token: 0x0600A959 RID: 43353 RVA: 0x002598B0 File Offset: 0x00257AB0
				public isMatchRegex isMatchRegex(ProgramNode node)
				{
					isMatchRegex? isMatchRegex = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex.CreateSafe(this._builders, node);
					if (isMatchRegex == null)
					{
						string text = "node";
						string text2 = "expected node for symbol isMatchRegex but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isMatchRegex.Value;
				}

				// Token: 0x0600A95A RID: 43354 RVA: 0x00259904 File Offset: 0x00257B04
				public containsMatchRegex containsMatchRegex(ProgramNode node)
				{
					containsMatchRegex? containsMatchRegex = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex.CreateSafe(this._builders, node);
					if (containsMatchRegex == null)
					{
						string text = "node";
						string text2 = "expected node for symbol containsMatchRegex but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsMatchRegex.Value;
				}

				// Token: 0x0600A95B RID: 43355 RVA: 0x00259958 File Offset: 0x00257B58
				public containsCount containsCount(ProgramNode node)
				{
					containsCount? containsCount = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount.CreateSafe(this._builders, node);
					if (containsCount == null)
					{
						string text = "node";
						string text2 = "expected node for symbol containsCount but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsCount.Value;
				}

				// Token: 0x0600A95C RID: 43356 RVA: 0x002599AC File Offset: 0x00257BAC
				public matchCount matchCount(ProgramNode node)
				{
					matchCount? matchCount = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount.CreateSafe(this._builders, node);
					if (matchCount == null)
					{
						string text = "node";
						string text2 = "expected node for symbol matchCount but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchCount.Value;
				}

				// Token: 0x0600A95D RID: 43357 RVA: 0x00259A00 File Offset: 0x00257C00
				public numberEqualsValue numberEqualsValue(ProgramNode node)
				{
					numberEqualsValue? numberEqualsValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue.CreateSafe(this._builders, node);
					if (numberEqualsValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberEqualsValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberEqualsValue.Value;
				}

				// Token: 0x0600A95E RID: 43358 RVA: 0x00259A54 File Offset: 0x00257C54
				public numberGreaterThanValue numberGreaterThanValue(ProgramNode node)
				{
					numberGreaterThanValue? numberGreaterThanValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue.CreateSafe(this._builders, node);
					if (numberGreaterThanValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberGreaterThanValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberGreaterThanValue.Value;
				}

				// Token: 0x0600A95F RID: 43359 RVA: 0x00259AA8 File Offset: 0x00257CA8
				public numberLessThanValue numberLessThanValue(ProgramNode node)
				{
					numberLessThanValue? numberLessThanValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue.CreateSafe(this._builders, node);
					if (numberLessThanValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberLessThanValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberLessThanValue.Value;
				}

				// Token: 0x0600A960 RID: 43360 RVA: 0x00259AFC File Offset: 0x00257CFC
				public formatNumber formatNumber(ProgramNode node)
				{
					formatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol formatNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatNumber.Value;
				}

				// Token: 0x0600A961 RID: 43361 RVA: 0x00259B50 File Offset: 0x00257D50
				public number number(ProgramNode node)
				{
					number? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number.CreateSafe(this._builders, node);
					if (number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number.Value;
				}

				// Token: 0x0600A962 RID: 43362 RVA: 0x00259BA4 File Offset: 0x00257DA4
				public number1 number1(ProgramNode node)
				{
					number1? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1.CreateSafe(this._builders, node);
					if (number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number.Value;
				}

				// Token: 0x0600A963 RID: 43363 RVA: 0x00259BF8 File Offset: 0x00257DF8
				public arithmetic arithmetic(ProgramNode node)
				{
					arithmetic? arithmetic = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic.CreateSafe(this._builders, node);
					if (arithmetic == null)
					{
						string text = "node";
						string text2 = "expected node for symbol arithmetic but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return arithmetic.Value;
				}

				// Token: 0x0600A964 RID: 43364 RVA: 0x00259C4C File Offset: 0x00257E4C
				public arithmeticLeft arithmeticLeft(ProgramNode node)
				{
					arithmeticLeft? arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft.CreateSafe(this._builders, node);
					if (arithmeticLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol arithmeticLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return arithmeticLeft.Value;
				}

				// Token: 0x0600A965 RID: 43365 RVA: 0x00259CA0 File Offset: 0x00257EA0
				public addRight addRight(ProgramNode node)
				{
					addRight? addRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight.CreateSafe(this._builders, node);
					if (addRight == null)
					{
						string text = "node";
						string text2 = "expected node for symbol addRight but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return addRight.Value;
				}

				// Token: 0x0600A966 RID: 43366 RVA: 0x00259CF4 File Offset: 0x00257EF4
				public subtractRight subtractRight(ProgramNode node)
				{
					subtractRight? subtractRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight.CreateSafe(this._builders, node);
					if (subtractRight == null)
					{
						string text = "node";
						string text2 = "expected node for symbol subtractRight but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subtractRight.Value;
				}

				// Token: 0x0600A967 RID: 43367 RVA: 0x00259D48 File Offset: 0x00257F48
				public multiplyRight multiplyRight(ProgramNode node)
				{
					multiplyRight? multiplyRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight.CreateSafe(this._builders, node);
					if (multiplyRight == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multiplyRight but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiplyRight.Value;
				}

				// Token: 0x0600A968 RID: 43368 RVA: 0x00259D9C File Offset: 0x00257F9C
				public divideRight divideRight(ProgramNode node)
				{
					divideRight? divideRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight.CreateSafe(this._builders, node);
					if (divideRight == null)
					{
						string text = "node";
						string text2 = "expected node for symbol divideRight but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return divideRight.Value;
				}

				// Token: 0x0600A969 RID: 43369 RVA: 0x00259DF0 File Offset: 0x00257FF0
				public inumber inumber(ProgramNode node)
				{
					inumber? inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber.CreateSafe(this._builders, node);
					if (inumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inumber.Value;
				}

				// Token: 0x0600A96A RID: 43370 RVA: 0x00259E44 File Offset: 0x00258044
				public rowNumberTransform rowNumberTransform(ProgramNode node)
				{
					rowNumberTransform? rowNumberTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform.CreateSafe(this._builders, node);
					if (rowNumberTransform == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rowNumberTransform but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowNumberTransform.Value;
				}

				// Token: 0x0600A96B RID: 43371 RVA: 0x00259E98 File Offset: 0x00258098
				public formatDateTime formatDateTime(ProgramNode node)
				{
					formatDateTime? formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime.CreateSafe(this._builders, node);
					if (formatDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol formatDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatDateTime.Value;
				}

				// Token: 0x0600A96C RID: 43372 RVA: 0x00259EEC File Offset: 0x002580EC
				public date date(ProgramNode node)
				{
					date? date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date.CreateSafe(this._builders, node);
					if (date == null)
					{
						string text = "node";
						string text2 = "expected node for symbol date but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return date.Value;
				}

				// Token: 0x0600A96D RID: 43373 RVA: 0x00259F40 File Offset: 0x00258140
				public idate idate(ProgramNode node)
				{
					idate? idate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate.CreateSafe(this._builders, node);
					if (idate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idate.Value;
				}

				// Token: 0x0600A96E RID: 43374 RVA: 0x00259F94 File Offset: 0x00258194
				public itime itime(ProgramNode node)
				{
					itime? itime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime.CreateSafe(this._builders, node);
					if (itime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol itime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return itime.Value;
				}

				// Token: 0x0600A96F RID: 43375 RVA: 0x00259FE8 File Offset: 0x002581E8
				public parseSubject parseSubject(ProgramNode node)
				{
					parseSubject? parseSubject = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject.CreateSafe(this._builders, node);
					if (parseSubject == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parseSubject but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseSubject.Value;
				}

				// Token: 0x0600A970 RID: 43376 RVA: 0x0025A03C File Offset: 0x0025823C
				public letSubstring letSubstring(ProgramNode node)
				{
					letSubstring? letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring.CreateSafe(this._builders, node);
					if (letSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol letSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSubstring.Value;
				}

				// Token: 0x0600A971 RID: 43377 RVA: 0x0025A090 File Offset: 0x00258290
				public x x(ProgramNode node)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						string text = "node";
						string text2 = "expected node for symbol x but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return x.Value;
				}

				// Token: 0x0600A972 RID: 43378 RVA: 0x0025A0E4 File Offset: 0x002582E4
				public substring substring(ProgramNode node)
				{
					substring? substring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring.Value;
				}

				// Token: 0x0600A973 RID: 43379 RVA: 0x0025A138 File Offset: 0x00258338
				public splitTrim splitTrim(ProgramNode node)
				{
					splitTrim? splitTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim.CreateSafe(this._builders, node);
					if (splitTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitTrim.Value;
				}

				// Token: 0x0600A974 RID: 43380 RVA: 0x0025A18C File Offset: 0x0025838C
				public split split(ProgramNode node)
				{
					split? split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split.CreateSafe(this._builders, node);
					if (split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return split.Value;
				}

				// Token: 0x0600A975 RID: 43381 RVA: 0x0025A1E0 File Offset: 0x002583E0
				public sliceTrim sliceTrim(ProgramNode node)
				{
					sliceTrim? sliceTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim.CreateSafe(this._builders, node);
					if (sliceTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sliceTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceTrim.Value;
				}

				// Token: 0x0600A976 RID: 43382 RVA: 0x0025A234 File Offset: 0x00258434
				public slice slice(ProgramNode node)
				{
					slice? slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol slice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slice.Value;
				}

				// Token: 0x0600A977 RID: 43383 RVA: 0x0025A288 File Offset: 0x00258488
				public pos pos(ProgramNode node)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pos.Value;
				}

				// Token: 0x0600A978 RID: 43384 RVA: 0x0025A2DC File Offset: 0x002584DC
				public fromStrTrim fromStrTrim(ProgramNode node)
				{
					fromStrTrim? fromStrTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim.CreateSafe(this._builders, node);
					if (fromStrTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromStrTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromStrTrim.Value;
				}

				// Token: 0x0600A979 RID: 43385 RVA: 0x0025A330 File Offset: 0x00258530
				public fromStr fromStr(ProgramNode node)
				{
					fromStr? fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr.CreateSafe(this._builders, node);
					if (fromStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromStr.Value;
				}

				// Token: 0x0600A97A RID: 43386 RVA: 0x0025A384 File Offset: 0x00258584
				public fromNumberStr fromNumberStr(ProgramNode node)
				{
					fromNumberStr? fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr.CreateSafe(this._builders, node);
					if (fromNumberStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromNumberStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumberStr.Value;
				}

				// Token: 0x0600A97B RID: 43387 RVA: 0x0025A3D8 File Offset: 0x002585D8
				public fromNumber fromNumber(ProgramNode node)
				{
					fromNumber? fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber.CreateSafe(this._builders, node);
					if (fromNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumber.Value;
				}

				// Token: 0x0600A97C RID: 43388 RVA: 0x0025A42C File Offset: 0x0025862C
				public fromNumberCoalesced fromNumberCoalesced(ProgramNode node)
				{
					fromNumberCoalesced? fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced.CreateSafe(this._builders, node);
					if (fromNumberCoalesced == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromNumberCoalesced but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumberCoalesced.Value;
				}

				// Token: 0x0600A97D RID: 43389 RVA: 0x0025A480 File Offset: 0x00258680
				public fromRowNumber fromRowNumber(ProgramNode node)
				{
					fromRowNumber? fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber.CreateSafe(this._builders, node);
					if (fromRowNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromRowNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromRowNumber.Value;
				}

				// Token: 0x0600A97E RID: 43390 RVA: 0x0025A4D4 File Offset: 0x002586D4
				public fromNumbers fromNumbers(ProgramNode node)
				{
					fromNumbers? fromNumbers = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers.CreateSafe(this._builders, node);
					if (fromNumbers == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromNumbers but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumbers.Value;
				}

				// Token: 0x0600A97F RID: 43391 RVA: 0x0025A528 File Offset: 0x00258728
				public fromDateTime fromDateTime(ProgramNode node)
				{
					fromDateTime? fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime.CreateSafe(this._builders, node);
					if (fromDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromDateTime.Value;
				}

				// Token: 0x0600A980 RID: 43392 RVA: 0x0025A57C File Offset: 0x0025877C
				public fromDateTimePart fromDateTimePart(ProgramNode node)
				{
					fromDateTimePart? fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart.CreateSafe(this._builders, node);
					if (fromDateTimePart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromDateTimePart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromDateTimePart.Value;
				}

				// Token: 0x0600A981 RID: 43393 RVA: 0x0025A5D0 File Offset: 0x002587D0
				public fromTime fromTime(ProgramNode node)
				{
					fromTime? fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime.CreateSafe(this._builders, node);
					if (fromTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromTime.Value;
				}

				// Token: 0x0600A982 RID: 43394 RVA: 0x0025A624 File Offset: 0x00258824
				public constString constString(ProgramNode node)
				{
					constString? constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString.CreateSafe(this._builders, node);
					if (constString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constString.Value;
				}

				// Token: 0x0600A983 RID: 43395 RVA: 0x0025A678 File Offset: 0x00258878
				public constNumber constNumber(ProgramNode node)
				{
					constNumber? constNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber.CreateSafe(this._builders, node);
					if (constNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constNumber.Value;
				}

				// Token: 0x0600A984 RID: 43396 RVA: 0x0025A6CC File Offset: 0x002588CC
				public constDate constDate(ProgramNode node)
				{
					constDate? constDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate.CreateSafe(this._builders, node);
					if (constDate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constDate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constDate.Value;
				}

				// Token: 0x0600A985 RID: 43397 RVA: 0x0025A720 File Offset: 0x00258920
				public columnName columnName(ProgramNode node)
				{
					columnName? columnName = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
					if (columnName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnName.Value;
				}

				// Token: 0x0600A986 RID: 43398 RVA: 0x0025A774 File Offset: 0x00258974
				public columnNames columnNames(ProgramNode node)
				{
					columnNames? columnNames = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnNames but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnNames.Value;
				}

				// Token: 0x0600A987 RID: 43399 RVA: 0x0025A7C8 File Offset: 0x002589C8
				public constStr constStr(ProgramNode node)
				{
					constStr? constStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constStr.Value;
				}

				// Token: 0x0600A988 RID: 43400 RVA: 0x0025A81C File Offset: 0x00258A1C
				public constNum constNum(ProgramNode node)
				{
					constNum? constNum = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum.CreateSafe(this._builders, node);
					if (constNum == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constNum but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constNum.Value;
				}

				// Token: 0x0600A989 RID: 43401 RVA: 0x0025A870 File Offset: 0x00258A70
				public constDt constDt(ProgramNode node)
				{
					constDt? constDt = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt.CreateSafe(this._builders, node);
					if (constDt == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constDt but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constDt.Value;
				}

				// Token: 0x0600A98A RID: 43402 RVA: 0x0025A8C4 File Offset: 0x00258AC4
				public locale locale(ProgramNode node)
				{
					locale? locale = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale.CreateSafe(this._builders, node);
					if (locale == null)
					{
						string text = "node";
						string text2 = "expected node for symbol locale but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return locale.Value;
				}

				// Token: 0x0600A98B RID: 43403 RVA: 0x0025A918 File Offset: 0x00258B18
				public replaceFindText replaceFindText(ProgramNode node)
				{
					replaceFindText? replaceFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText.CreateSafe(this._builders, node);
					if (replaceFindText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol replaceFindText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return replaceFindText.Value;
				}

				// Token: 0x0600A98C RID: 43404 RVA: 0x0025A96C File Offset: 0x00258B6C
				public replaceText replaceText(ProgramNode node)
				{
					replaceText? replaceText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText.CreateSafe(this._builders, node);
					if (replaceText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol replaceText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return replaceText.Value;
				}

				// Token: 0x0600A98D RID: 43405 RVA: 0x0025A9C0 File Offset: 0x00258BC0
				public sliceBetweenStartText sliceBetweenStartText(ProgramNode node)
				{
					sliceBetweenStartText? sliceBetweenStartText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText.CreateSafe(this._builders, node);
					if (sliceBetweenStartText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sliceBetweenStartText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceBetweenStartText.Value;
				}

				// Token: 0x0600A98E RID: 43406 RVA: 0x0025AA14 File Offset: 0x00258C14
				public sliceBetweenEndText sliceBetweenEndText(ProgramNode node)
				{
					sliceBetweenEndText? sliceBetweenEndText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText.CreateSafe(this._builders, node);
					if (sliceBetweenEndText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sliceBetweenEndText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceBetweenEndText.Value;
				}

				// Token: 0x0600A98F RID: 43407 RVA: 0x0025AA68 File Offset: 0x00258C68
				public numberFormatDesc numberFormatDesc(ProgramNode node)
				{
					numberFormatDesc? numberFormatDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc.CreateSafe(this._builders, node);
					if (numberFormatDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormatDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormatDesc.Value;
				}

				// Token: 0x0600A990 RID: 43408 RVA: 0x0025AABC File Offset: 0x00258CBC
				public numberRoundDesc numberRoundDesc(ProgramNode node)
				{
					numberRoundDesc? numberRoundDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc.CreateSafe(this._builders, node);
					if (numberRoundDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberRoundDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberRoundDesc.Value;
				}

				// Token: 0x0600A991 RID: 43409 RVA: 0x0025AB10 File Offset: 0x00258D10
				public dateTimeRoundDesc dateTimeRoundDesc(ProgramNode node)
				{
					dateTimeRoundDesc? dateTimeRoundDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc.CreateSafe(this._builders, node);
					if (dateTimeRoundDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dateTimeRoundDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dateTimeRoundDesc.Value;
				}

				// Token: 0x0600A992 RID: 43410 RVA: 0x0025AB64 File Offset: 0x00258D64
				public dateTimeFormatDesc dateTimeFormatDesc(ProgramNode node)
				{
					dateTimeFormatDesc? dateTimeFormatDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc.CreateSafe(this._builders, node);
					if (dateTimeFormatDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dateTimeFormatDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dateTimeFormatDesc.Value;
				}

				// Token: 0x0600A993 RID: 43411 RVA: 0x0025ABB8 File Offset: 0x00258DB8
				public dateTimeParseDesc dateTimeParseDesc(ProgramNode node)
				{
					dateTimeParseDesc? dateTimeParseDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc.CreateSafe(this._builders, node);
					if (dateTimeParseDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dateTimeParseDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dateTimeParseDesc.Value;
				}

				// Token: 0x0600A994 RID: 43412 RVA: 0x0025AC0C File Offset: 0x00258E0C
				public dateTimePartKind dateTimePartKind(ProgramNode node)
				{
					dateTimePartKind? dateTimePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind.CreateSafe(this._builders, node);
					if (dateTimePartKind == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dateTimePartKind but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dateTimePartKind.Value;
				}

				// Token: 0x0600A995 RID: 43413 RVA: 0x0025AC60 File Offset: 0x00258E60
				public fromDateTimePartKind fromDateTimePartKind(ProgramNode node)
				{
					fromDateTimePartKind? fromDateTimePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind.CreateSafe(this._builders, node);
					if (fromDateTimePartKind == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromDateTimePartKind but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromDateTimePartKind.Value;
				}

				// Token: 0x0600A996 RID: 43414 RVA: 0x0025ACB4 File Offset: 0x00258EB4
				public timePartKind timePartKind(ProgramNode node)
				{
					timePartKind? timePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind.CreateSafe(this._builders, node);
					if (timePartKind == null)
					{
						string text = "node";
						string text2 = "expected node for symbol timePartKind but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return timePartKind.Value;
				}

				// Token: 0x0600A997 RID: 43415 RVA: 0x0025AD08 File Offset: 0x00258F08
				public rowNumberLinearTransformDesc rowNumberLinearTransformDesc(ProgramNode node)
				{
					rowNumberLinearTransformDesc? rowNumberLinearTransformDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc.CreateSafe(this._builders, node);
					if (rowNumberLinearTransformDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rowNumberLinearTransformDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowNumberLinearTransformDesc.Value;
				}

				// Token: 0x0600A998 RID: 43416 RVA: 0x0025AD5C File Offset: 0x00258F5C
				public matchDesc matchDesc(ProgramNode node)
				{
					matchDesc? matchDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc.CreateSafe(this._builders, node);
					if (matchDesc == null)
					{
						string text = "node";
						string text2 = "expected node for symbol matchDesc but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchDesc.Value;
				}

				// Token: 0x0600A999 RID: 43417 RVA: 0x0025ADB0 File Offset: 0x00258FB0
				public matchInstance matchInstance(ProgramNode node)
				{
					matchInstance? matchInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance.CreateSafe(this._builders, node);
					if (matchInstance == null)
					{
						string text = "node";
						string text2 = "expected node for symbol matchInstance but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchInstance.Value;
				}

				// Token: 0x0600A99A RID: 43418 RVA: 0x0025AE04 File Offset: 0x00259004
				public splitDelimiter splitDelimiter(ProgramNode node)
				{
					splitDelimiter? splitDelimiter = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter.CreateSafe(this._builders, node);
					if (splitDelimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitDelimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitDelimiter.Value;
				}

				// Token: 0x0600A99B RID: 43419 RVA: 0x0025AE58 File Offset: 0x00259058
				public splitInstance splitInstance(ProgramNode node)
				{
					splitInstance? splitInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance.CreateSafe(this._builders, node);
					if (splitInstance == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitInstance but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitInstance.Value;
				}

				// Token: 0x0600A99C RID: 43420 RVA: 0x0025AEAC File Offset: 0x002590AC
				public findDelimiter findDelimiter(ProgramNode node)
				{
					findDelimiter? findDelimiter = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter.CreateSafe(this._builders, node);
					if (findDelimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol findDelimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return findDelimiter.Value;
				}

				// Token: 0x0600A99D RID: 43421 RVA: 0x0025AF00 File Offset: 0x00259100
				public findInstance findInstance(ProgramNode node)
				{
					findInstance? findInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance.CreateSafe(this._builders, node);
					if (findInstance == null)
					{
						string text = "node";
						string text2 = "expected node for symbol findInstance but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return findInstance.Value;
				}

				// Token: 0x0600A99E RID: 43422 RVA: 0x0025AF54 File Offset: 0x00259154
				public findOffset findOffset(ProgramNode node)
				{
					findOffset? findOffset = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset.CreateSafe(this._builders, node);
					if (findOffset == null)
					{
						string text = "node";
						string text2 = "expected node for symbol findOffset but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return findOffset.Value;
				}

				// Token: 0x0600A99F RID: 43423 RVA: 0x0025AFA8 File Offset: 0x002591A8
				public slicePrefixAbsPos slicePrefixAbsPos(ProgramNode node)
				{
					slicePrefixAbsPos? slicePrefixAbsPos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos.CreateSafe(this._builders, node);
					if (slicePrefixAbsPos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol slicePrefixAbsPos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slicePrefixAbsPos.Value;
				}

				// Token: 0x0600A9A0 RID: 43424 RVA: 0x0025AFFC File Offset: 0x002591FC
				public scaleNumberFactor scaleNumberFactor(ProgramNode node)
				{
					scaleNumberFactor? scaleNumberFactor = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor.CreateSafe(this._builders, node);
					if (scaleNumberFactor == null)
					{
						string text = "node";
						string text2 = "expected node for symbol scaleNumberFactor but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return scaleNumberFactor.Value;
				}

				// Token: 0x0600A9A1 RID: 43425 RVA: 0x0025B050 File Offset: 0x00259250
				public absPos absPos(ProgramNode node)
				{
					absPos? absPos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos.CreateSafe(this._builders, node);
					if (absPos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol absPos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return absPos.Value;
				}

				// Token: 0x040044E1 RID: 17633
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001504 RID: 5380
			public class RuleCast
			{
				// Token: 0x0600A9A2 RID: 43426 RVA: 0x0025B0A1 File Offset: 0x002592A1
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600A9A3 RID: 43427 RVA: 0x0025B0B0 File Offset: 0x002592B0
				public result_output result_output(ProgramNode node)
				{
					result_output? result_output = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_output.CreateSafe(this._builders, node);
					if (result_output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol result_output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return result_output.Value;
				}

				// Token: 0x0600A9A4 RID: 43428 RVA: 0x0025B104 File Offset: 0x00259304
				public result_inull result_inull(ProgramNode node)
				{
					result_inull? result_inull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_inull.CreateSafe(this._builders, node);
					if (result_inull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol result_inull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return result_inull.Value;
				}

				// Token: 0x0600A9A5 RID: 43429 RVA: 0x0025B158 File Offset: 0x00259358
				public If If(ProgramNode node)
				{
					If? @if = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.If.CreateSafe(this._builders, node);
					if (@if == null)
					{
						string text = "node";
						string text2 = "expected node for symbol If but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @if.Value;
				}

				// Token: 0x0600A9A6 RID: 43430 RVA: 0x0025B1AC File Offset: 0x002593AC
				public ToInt ToInt(ProgramNode node)
				{
					ToInt? toInt = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToInt.CreateSafe(this._builders, node);
					if (toInt == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToInt but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toInt.Value;
				}

				// Token: 0x0600A9A7 RID: 43431 RVA: 0x0025B200 File Offset: 0x00259400
				public ToDouble ToDouble(ProgramNode node)
				{
					ToDouble? toDouble = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDouble.CreateSafe(this._builders, node);
					if (toDouble == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToDouble but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toDouble.Value;
				}

				// Token: 0x0600A9A8 RID: 43432 RVA: 0x0025B254 File Offset: 0x00259454
				public ToDecimal ToDecimal(ProgramNode node)
				{
					ToDecimal? toDecimal = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDecimal.CreateSafe(this._builders, node);
					if (toDecimal == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToDecimal but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toDecimal.Value;
				}

				// Token: 0x0600A9A9 RID: 43433 RVA: 0x0025B2A8 File Offset: 0x002594A8
				public ToDateTime ToDateTime(ProgramNode node)
				{
					ToDateTime? toDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDateTime.CreateSafe(this._builders, node);
					if (toDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toDateTime.Value;
				}

				// Token: 0x0600A9AA RID: 43434 RVA: 0x0025B2FC File Offset: 0x002594FC
				public ToStr ToStr(ProgramNode node)
				{
					ToStr? toStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToStr.CreateSafe(this._builders, node);
					if (toStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toStr.Value;
				}

				// Token: 0x0600A9AB RID: 43435 RVA: 0x0025B350 File Offset: 0x00259550
				public outNumber_number outNumber_number(ProgramNode node)
				{
					outNumber_number? outNumber_number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_number.CreateSafe(this._builders, node);
					if (outNumber_number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outNumber_number but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outNumber_number.Value;
				}

				// Token: 0x0600A9AC RID: 43436 RVA: 0x0025B3A4 File Offset: 0x002595A4
				public outNumber_constNumber outNumber_constNumber(ProgramNode node)
				{
					outNumber_constNumber? outNumber_constNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_constNumber.CreateSafe(this._builders, node);
					if (outNumber_constNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outNumber_constNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outNumber_constNumber.Value;
				}

				// Token: 0x0600A9AD RID: 43437 RVA: 0x0025B3F8 File Offset: 0x002595F8
				public outDate_date outDate_date(ProgramNode node)
				{
					outDate_date? outDate_date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_date.CreateSafe(this._builders, node);
					if (outDate_date == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outDate_date but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outDate_date.Value;
				}

				// Token: 0x0600A9AE RID: 43438 RVA: 0x0025B44C File Offset: 0x0025964C
				public outDate_constDate outDate_constDate(ProgramNode node)
				{
					outDate_constDate? outDate_constDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_constDate.CreateSafe(this._builders, node);
					if (outDate_constDate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outDate_constDate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outDate_constDate.Value;
				}

				// Token: 0x0600A9AF RID: 43439 RVA: 0x0025B4A0 File Offset: 0x002596A0
				public outStr_outStr1 outStr_outStr1(ProgramNode node)
				{
					outStr_outStr1? outStr_outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr_outStr1.CreateSafe(this._builders, node);
					if (outStr_outStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr_outStr1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr_outStr.Value;
				}

				// Token: 0x0600A9B0 RID: 43440 RVA: 0x0025B4F4 File Offset: 0x002596F4
				public Replace Replace(ProgramNode node)
				{
					Replace? replace = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Replace.CreateSafe(this._builders, node);
					if (replace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Replace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return replace.Value;
				}

				// Token: 0x0600A9B1 RID: 43441 RVA: 0x0025B548 File Offset: 0x00259748
				public outStr1_segmentCase outStr1_segmentCase(ProgramNode node)
				{
					outStr1_segmentCase? outStr1_segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_segmentCase.CreateSafe(this._builders, node);
					if (outStr1_segmentCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr1_segmentCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr1_segmentCase.Value;
				}

				// Token: 0x0600A9B2 RID: 43442 RVA: 0x0025B59C File Offset: 0x0025979C
				public outStr1_formatted outStr1_formatted(ProgramNode node)
				{
					outStr1_formatted? outStr1_formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_formatted.CreateSafe(this._builders, node);
					if (outStr1_formatted == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr1_formatted but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr1_formatted.Value;
				}

				// Token: 0x0600A9B3 RID: 43443 RVA: 0x0025B5F0 File Offset: 0x002597F0
				public outStr1_concatEntry outStr1_concatEntry(ProgramNode node)
				{
					outStr1_concatEntry? outStr1_concatEntry = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_concatEntry.CreateSafe(this._builders, node);
					if (outStr1_concatEntry == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr1_concatEntry but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr1_concatEntry.Value;
				}

				// Token: 0x0600A9B4 RID: 43444 RVA: 0x0025B644 File Offset: 0x00259844
				public outStr1_constString outStr1_constString(ProgramNode node)
				{
					outStr1_constString? outStr1_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_constString.CreateSafe(this._builders, node);
					if (outStr1_constString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outStr1_constString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outStr1_constString.Value;
				}

				// Token: 0x0600A9B5 RID: 43445 RVA: 0x0025B698 File Offset: 0x00259898
				public segmentCase_segment segmentCase_segment(ProgramNode node)
				{
					segmentCase_segment? segmentCase_segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segmentCase_segment.CreateSafe(this._builders, node);
					if (segmentCase_segment == null)
					{
						string text = "node";
						string text2 = "expected node for symbol segmentCase_segment but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return segmentCase_segment.Value;
				}

				// Token: 0x0600A9B6 RID: 43446 RVA: 0x0025B6EC File Offset: 0x002598EC
				public LowerCase LowerCase(ProgramNode node)
				{
					LowerCase? lowerCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCase.CreateSafe(this._builders, node);
					if (lowerCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LowerCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lowerCase.Value;
				}

				// Token: 0x0600A9B7 RID: 43447 RVA: 0x0025B740 File Offset: 0x00259940
				public UpperCase UpperCase(ProgramNode node)
				{
					UpperCase? upperCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCase.CreateSafe(this._builders, node);
					if (upperCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol UpperCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return upperCase.Value;
				}

				// Token: 0x0600A9B8 RID: 43448 RVA: 0x0025B794 File Offset: 0x00259994
				public ProperCase ProperCase(ProgramNode node)
				{
					ProperCase? properCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCase.CreateSafe(this._builders, node);
					if (properCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ProperCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return properCase.Value;
				}

				// Token: 0x0600A9B9 RID: 43449 RVA: 0x0025B7E8 File Offset: 0x002599E8
				public segment_fromStrTrim segment_fromStrTrim(ProgramNode node)
				{
					segment_fromStrTrim? segment_fromStrTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_fromStrTrim.CreateSafe(this._builders, node);
					if (segment_fromStrTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol segment_fromStrTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return segment_fromStrTrim.Value;
				}

				// Token: 0x0600A9BA RID: 43450 RVA: 0x0025B83C File Offset: 0x00259A3C
				public segment_letSubstring segment_letSubstring(ProgramNode node)
				{
					segment_letSubstring? segment_letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_letSubstring.CreateSafe(this._builders, node);
					if (segment_letSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol segment_letSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return segment_letSubstring.Value;
				}

				// Token: 0x0600A9BB RID: 43451 RVA: 0x0025B890 File Offset: 0x00259A90
				public formatted_formatNumber formatted_formatNumber(ProgramNode node)
				{
					formatted_formatNumber? formatted_formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatNumber.CreateSafe(this._builders, node);
					if (formatted_formatNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol formatted_formatNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatted_formatNumber.Value;
				}

				// Token: 0x0600A9BC RID: 43452 RVA: 0x0025B8E4 File Offset: 0x00259AE4
				public formatted_formatDateTime formatted_formatDateTime(ProgramNode node)
				{
					formatted_formatDateTime? formatted_formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatDateTime.CreateSafe(this._builders, node);
					if (formatted_formatDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol formatted_formatDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatted_formatDateTime.Value;
				}

				// Token: 0x0600A9BD RID: 43453 RVA: 0x0025B938 File Offset: 0x00259B38
				public concatEntry_concatCase concatEntry_concatCase(ProgramNode node)
				{
					concatEntry_concatCase? concatEntry_concatCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_concatCase.CreateSafe(this._builders, node);
					if (concatEntry_concatCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatEntry_concatCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatEntry_concatCase.Value;
				}

				// Token: 0x0600A9BE RID: 43454 RVA: 0x0025B98C File Offset: 0x00259B8C
				public concatEntry_constString concatEntry_constString(ProgramNode node)
				{
					concatEntry_constString? concatEntry_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_constString.CreateSafe(this._builders, node);
					if (concatEntry_constString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatEntry_constString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatEntry_constString.Value;
				}

				// Token: 0x0600A9BF RID: 43455 RVA: 0x0025B9E0 File Offset: 0x00259BE0
				public concatCase_concat concatCase_concat(ProgramNode node)
				{
					concatCase_concat? concatCase_concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatCase_concat.CreateSafe(this._builders, node);
					if (concatCase_concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatCase_concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatCase_concat.Value;
				}

				// Token: 0x0600A9C0 RID: 43456 RVA: 0x0025BA34 File Offset: 0x00259C34
				public LowerCaseConcat LowerCaseConcat(ProgramNode node)
				{
					LowerCaseConcat? lowerCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCaseConcat.CreateSafe(this._builders, node);
					if (lowerCaseConcat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LowerCaseConcat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lowerCaseConcat.Value;
				}

				// Token: 0x0600A9C1 RID: 43457 RVA: 0x0025BA88 File Offset: 0x00259C88
				public UpperCaseConcat UpperCaseConcat(ProgramNode node)
				{
					UpperCaseConcat? upperCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCaseConcat.CreateSafe(this._builders, node);
					if (upperCaseConcat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol UpperCaseConcat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return upperCaseConcat.Value;
				}

				// Token: 0x0600A9C2 RID: 43458 RVA: 0x0025BADC File Offset: 0x00259CDC
				public ProperCaseConcat ProperCaseConcat(ProgramNode node)
				{
					ProperCaseConcat? properCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCaseConcat.CreateSafe(this._builders, node);
					if (properCaseConcat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ProperCaseConcat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return properCaseConcat.Value;
				}

				// Token: 0x0600A9C3 RID: 43459 RVA: 0x0025BB30 File Offset: 0x00259D30
				public Concat Concat(ProgramNode node)
				{
					Concat? concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concat.Value;
				}

				// Token: 0x0600A9C4 RID: 43460 RVA: 0x0025BB84 File Offset: 0x00259D84
				public concatPrefix_concatSegment concatPrefix_concatSegment(ProgramNode node)
				{
					concatPrefix_concatSegment? concatPrefix_concatSegment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_concatSegment.CreateSafe(this._builders, node);
					if (concatPrefix_concatSegment == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatPrefix_concatSegment but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatPrefix_concatSegment.Value;
				}

				// Token: 0x0600A9C5 RID: 43461 RVA: 0x0025BBD8 File Offset: 0x00259DD8
				public concatPrefix_formatted concatPrefix_formatted(ProgramNode node)
				{
					concatPrefix_formatted? concatPrefix_formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_formatted.CreateSafe(this._builders, node);
					if (concatPrefix_formatted == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatPrefix_formatted but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatPrefix_formatted.Value;
				}

				// Token: 0x0600A9C6 RID: 43462 RVA: 0x0025BC2C File Offset: 0x00259E2C
				public concatPrefix_constString concatPrefix_constString(ProgramNode node)
				{
					concatPrefix_constString? concatPrefix_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_constString.CreateSafe(this._builders, node);
					if (concatPrefix_constString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatPrefix_constString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatPrefix_constString.Value;
				}

				// Token: 0x0600A9C7 RID: 43463 RVA: 0x0025BC80 File Offset: 0x00259E80
				public concatSegment_segment concatSegment_segment(ProgramNode node)
				{
					concatSegment_segment? concatSegment_segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segment.CreateSafe(this._builders, node);
					if (concatSegment_segment == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSegment_segment but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSegment_segment.Value;
				}

				// Token: 0x0600A9C8 RID: 43464 RVA: 0x0025BCD4 File Offset: 0x00259ED4
				public concatSegment_segmentCase concatSegment_segmentCase(ProgramNode node)
				{
					concatSegment_segmentCase? concatSegment_segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segmentCase.CreateSafe(this._builders, node);
					if (concatSegment_segmentCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSegment_segmentCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSegment_segmentCase.Value;
				}

				// Token: 0x0600A9C9 RID: 43465 RVA: 0x0025BD28 File Offset: 0x00259F28
				public concatSuffix_concatPrefix concatSuffix_concatPrefix(ProgramNode node)
				{
					concatSuffix_concatPrefix? concatSuffix_concatPrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concatPrefix.CreateSafe(this._builders, node);
					if (concatSuffix_concatPrefix == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSuffix_concatPrefix but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSuffix_concatPrefix.Value;
				}

				// Token: 0x0600A9CA RID: 43466 RVA: 0x0025BD7C File Offset: 0x00259F7C
				public concatSuffix_concat concatSuffix_concat(ProgramNode node)
				{
					concatSuffix_concat? concatSuffix_concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concat.CreateSafe(this._builders, node);
					if (concatSuffix_concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol concatSuffix_concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatSuffix_concat.Value;
				}

				// Token: 0x0600A9CB RID: 43467 RVA: 0x0025BDD0 File Offset: 0x00259FD0
				public StringEquals StringEquals(ProgramNode node)
				{
					StringEquals? stringEquals = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StringEquals.CreateSafe(this._builders, node);
					if (stringEquals == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StringEquals but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return stringEquals.Value;
				}

				// Token: 0x0600A9CC RID: 43468 RVA: 0x0025BE24 File Offset: 0x0025A024
				public Contains Contains(ProgramNode node)
				{
					Contains? contains = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
					if (contains == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Contains but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return contains.Value;
				}

				// Token: 0x0600A9CD RID: 43469 RVA: 0x0025BE78 File Offset: 0x0025A078
				public StartsWithDigit StartsWithDigit(ProgramNode node)
				{
					StartsWithDigit? startsWithDigit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
					if (startsWithDigit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWithDigit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWithDigit.Value;
				}

				// Token: 0x0600A9CE RID: 43470 RVA: 0x0025BECC File Offset: 0x0025A0CC
				public EndsWithDigit EndsWithDigit(ProgramNode node)
				{
					EndsWithDigit? endsWithDigit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
					if (endsWithDigit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndsWithDigit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endsWithDigit.Value;
				}

				// Token: 0x0600A9CF RID: 43471 RVA: 0x0025BF20 File Offset: 0x0025A120
				public StartsWith StartsWith(ProgramNode node)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWith but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWith.Value;
				}

				// Token: 0x0600A9D0 RID: 43472 RVA: 0x0025BF74 File Offset: 0x0025A174
				public IsBlank IsBlank(ProgramNode node)
				{
					IsBlank? isBlank = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsBlank.CreateSafe(this._builders, node);
					if (isBlank == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsBlank but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isBlank.Value;
				}

				// Token: 0x0600A9D1 RID: 43473 RVA: 0x0025BFC8 File Offset: 0x0025A1C8
				public IsNotBlank IsNotBlank(ProgramNode node)
				{
					IsNotBlank? isNotBlank = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNotBlank.CreateSafe(this._builders, node);
					if (isNotBlank == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNotBlank but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNotBlank.Value;
				}

				// Token: 0x0600A9D2 RID: 43474 RVA: 0x0025C01C File Offset: 0x0025A21C
				public NumberEquals NumberEquals(ProgramNode node)
				{
					NumberEquals? numberEquals = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberEquals.CreateSafe(this._builders, node);
					if (numberEquals == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NumberEquals but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberEquals.Value;
				}

				// Token: 0x0600A9D3 RID: 43475 RVA: 0x0025C070 File Offset: 0x0025A270
				public NumberGreaterThan NumberGreaterThan(ProgramNode node)
				{
					NumberGreaterThan? numberGreaterThan = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberGreaterThan.CreateSafe(this._builders, node);
					if (numberGreaterThan == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NumberGreaterThan but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberGreaterThan.Value;
				}

				// Token: 0x0600A9D4 RID: 43476 RVA: 0x0025C0C4 File Offset: 0x0025A2C4
				public NumberLessThan NumberLessThan(ProgramNode node)
				{
					NumberLessThan? numberLessThan = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberLessThan.CreateSafe(this._builders, node);
					if (numberLessThan == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NumberLessThan but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberLessThan.Value;
				}

				// Token: 0x0600A9D5 RID: 43477 RVA: 0x0025C118 File Offset: 0x0025A318
				public IsString IsString(ProgramNode node)
				{
					IsString? isString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsString.CreateSafe(this._builders, node);
					if (isString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isString.Value;
				}

				// Token: 0x0600A9D6 RID: 43478 RVA: 0x0025C16C File Offset: 0x0025A36C
				public IsNumber IsNumber(ProgramNode node)
				{
					IsNumber? isNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNumber.CreateSafe(this._builders, node);
					if (isNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNumber.Value;
				}

				// Token: 0x0600A9D7 RID: 43479 RVA: 0x0025C1C0 File Offset: 0x0025A3C0
				public IsMatch IsMatch(ProgramNode node)
				{
					IsMatch? isMatch = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsMatch.CreateSafe(this._builders, node);
					if (isMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isMatch.Value;
				}

				// Token: 0x0600A9D8 RID: 43480 RVA: 0x0025C214 File Offset: 0x0025A414
				public ContainsMatch ContainsMatch(ProgramNode node)
				{
					ContainsMatch? containsMatch = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ContainsMatch.CreateSafe(this._builders, node);
					if (containsMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ContainsMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsMatch.Value;
				}

				// Token: 0x0600A9D9 RID: 43481 RVA: 0x0025C268 File Offset: 0x0025A468
				public condition_or condition_or(ProgramNode node)
				{
					condition_or? condition_or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.condition_or.CreateSafe(this._builders, node);
					if (condition_or == null)
					{
						string text = "node";
						string text2 = "expected node for symbol condition_or but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return condition_or.Value;
				}

				// Token: 0x0600A9DA RID: 43482 RVA: 0x0025C2BC File Offset: 0x0025A4BC
				public Or Or(ProgramNode node)
				{
					Or? or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Or.CreateSafe(this._builders, node);
					if (or == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Or but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return or.Value;
				}

				// Token: 0x0600A9DB RID: 43483 RVA: 0x0025C310 File Offset: 0x0025A510
				public Null Null(ProgramNode node)
				{
					Null? @null = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Null.CreateSafe(this._builders, node);
					if (@null == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Null but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @null.Value;
				}

				// Token: 0x0600A9DC RID: 43484 RVA: 0x0025C364 File Offset: 0x0025A564
				public FormatNumber FormatNumber(ProgramNode node)
				{
					FormatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatNumber.Value;
				}

				// Token: 0x0600A9DD RID: 43485 RVA: 0x0025C3B8 File Offset: 0x0025A5B8
				public number_number1 number_number1(ProgramNode node)
				{
					number_number1? number_number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_number1.CreateSafe(this._builders, node);
					if (number_number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number_number1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number_number.Value;
				}

				// Token: 0x0600A9DE RID: 43486 RVA: 0x0025C40C File Offset: 0x0025A60C
				public number_arithmetic number_arithmetic(ProgramNode node)
				{
					number_arithmetic? number_arithmetic = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_arithmetic.CreateSafe(this._builders, node);
					if (number_arithmetic == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number_arithmetic but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number_arithmetic.Value;
				}

				// Token: 0x0600A9DF RID: 43487 RVA: 0x0025C460 File Offset: 0x0025A660
				public number_rowNumberTransform number_rowNumberTransform(ProgramNode node)
				{
					number_rowNumberTransform? number_rowNumberTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_rowNumberTransform.CreateSafe(this._builders, node);
					if (number_rowNumberTransform == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number_rowNumberTransform but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number_rowNumberTransform.Value;
				}

				// Token: 0x0600A9E0 RID: 43488 RVA: 0x0025C4B4 File Offset: 0x0025A6B4
				public Length Length(ProgramNode node)
				{
					Length? length = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Length.CreateSafe(this._builders, node);
					if (length == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Length but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return length.Value;
				}

				// Token: 0x0600A9E1 RID: 43489 RVA: 0x0025C508 File Offset: 0x0025A708
				public number1_inumber number1_inumber(ProgramNode node)
				{
					number1_inumber? number1_inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number1_inumber.CreateSafe(this._builders, node);
					if (number1_inumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number1_inumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number1_inumber.Value;
				}

				// Token: 0x0600A9E2 RID: 43490 RVA: 0x0025C55C File Offset: 0x0025A75C
				public DateTimePart DateTimePart(ProgramNode node)
				{
					DateTimePart? dateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DateTimePart.CreateSafe(this._builders, node);
					if (dateTimePart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DateTimePart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dateTimePart.Value;
				}

				// Token: 0x0600A9E3 RID: 43491 RVA: 0x0025C5B0 File Offset: 0x0025A7B0
				public TimePart TimePart(ProgramNode node)
				{
					TimePart? timePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TimePart.CreateSafe(this._builders, node);
					if (timePart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TimePart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return timePart.Value;
				}

				// Token: 0x0600A9E4 RID: 43492 RVA: 0x0025C604 File Offset: 0x0025A804
				public RoundNumber RoundNumber(ProgramNode node)
				{
					RoundNumber? roundNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
					if (roundNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RoundNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return roundNumber.Value;
				}

				// Token: 0x0600A9E5 RID: 43493 RVA: 0x0025C658 File Offset: 0x0025A858
				public Add Add(ProgramNode node)
				{
					Add? add = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
					if (add == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Add but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return add.Value;
				}

				// Token: 0x0600A9E6 RID: 43494 RVA: 0x0025C6AC File Offset: 0x0025A8AC
				public Subtract Subtract(ProgramNode node)
				{
					Subtract? subtract = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Subtract.CreateSafe(this._builders, node);
					if (subtract == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Subtract but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subtract.Value;
				}

				// Token: 0x0600A9E7 RID: 43495 RVA: 0x0025C700 File Offset: 0x0025A900
				public Multiply Multiply(ProgramNode node)
				{
					Multiply? multiply = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Multiply.CreateSafe(this._builders, node);
					if (multiply == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Multiply but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiply.Value;
				}

				// Token: 0x0600A9E8 RID: 43496 RVA: 0x0025C754 File Offset: 0x0025A954
				public Divide Divide(ProgramNode node)
				{
					Divide? divide = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Divide.CreateSafe(this._builders, node);
					if (divide == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Divide but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return divide.Value;
				}

				// Token: 0x0600A9E9 RID: 43497 RVA: 0x0025C7A8 File Offset: 0x0025A9A8
				public Sum Sum(ProgramNode node)
				{
					Sum? sum = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Sum.CreateSafe(this._builders, node);
					if (sum == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Sum but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sum.Value;
				}

				// Token: 0x0600A9EA RID: 43498 RVA: 0x0025C7FC File Offset: 0x0025A9FC
				public Product Product(ProgramNode node)
				{
					Product? product = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Product.CreateSafe(this._builders, node);
					if (product == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Product but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return product.Value;
				}

				// Token: 0x0600A9EB RID: 43499 RVA: 0x0025C850 File Offset: 0x0025AA50
				public Average Average(ProgramNode node)
				{
					Average? average = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Average.CreateSafe(this._builders, node);
					if (average == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Average but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return average.Value;
				}

				// Token: 0x0600A9EC RID: 43500 RVA: 0x0025C8A4 File Offset: 0x0025AAA4
				public arithmeticLeft_fromNumberCoalesced arithmeticLeft_fromNumberCoalesced(ProgramNode node)
				{
					arithmeticLeft_fromNumberCoalesced? arithmeticLeft_fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_fromNumberCoalesced.CreateSafe(this._builders, node);
					if (arithmeticLeft_fromNumberCoalesced == null)
					{
						string text = "node";
						string text2 = "expected node for symbol arithmeticLeft_fromNumberCoalesced but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return arithmeticLeft_fromNumberCoalesced.Value;
				}

				// Token: 0x0600A9ED RID: 43501 RVA: 0x0025C8F8 File Offset: 0x0025AAF8
				public arithmeticLeft_inumber arithmeticLeft_inumber(ProgramNode node)
				{
					arithmeticLeft_inumber? arithmeticLeft_inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_inumber.CreateSafe(this._builders, node);
					if (arithmeticLeft_inumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol arithmeticLeft_inumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return arithmeticLeft_inumber.Value;
				}

				// Token: 0x0600A9EE RID: 43502 RVA: 0x0025C94C File Offset: 0x0025AB4C
				public addRight_arithmeticLeft addRight_arithmeticLeft(ProgramNode node)
				{
					addRight_arithmeticLeft? addRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.addRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (addRight_arithmeticLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol addRight_arithmeticLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return addRight_arithmeticLeft.Value;
				}

				// Token: 0x0600A9EF RID: 43503 RVA: 0x0025C9A0 File Offset: 0x0025ABA0
				public AddRightNumber AddRightNumber(ProgramNode node)
				{
					AddRightNumber? addRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.AddRightNumber.CreateSafe(this._builders, node);
					if (addRightNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AddRightNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return addRightNumber.Value;
				}

				// Token: 0x0600A9F0 RID: 43504 RVA: 0x0025C9F4 File Offset: 0x0025ABF4
				public subtractRight_arithmeticLeft subtractRight_arithmeticLeft(ProgramNode node)
				{
					subtractRight_arithmeticLeft? subtractRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.subtractRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (subtractRight_arithmeticLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol subtractRight_arithmeticLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subtractRight_arithmeticLeft.Value;
				}

				// Token: 0x0600A9F1 RID: 43505 RVA: 0x0025CA48 File Offset: 0x0025AC48
				public SubtractRightNumber SubtractRightNumber(ProgramNode node)
				{
					SubtractRightNumber? subtractRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SubtractRightNumber.CreateSafe(this._builders, node);
					if (subtractRightNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SubtractRightNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subtractRightNumber.Value;
				}

				// Token: 0x0600A9F2 RID: 43506 RVA: 0x0025CA9C File Offset: 0x0025AC9C
				public multiplyRight_arithmeticLeft multiplyRight_arithmeticLeft(ProgramNode node)
				{
					multiplyRight_arithmeticLeft? multiplyRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.multiplyRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (multiplyRight_arithmeticLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multiplyRight_arithmeticLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiplyRight_arithmeticLeft.Value;
				}

				// Token: 0x0600A9F3 RID: 43507 RVA: 0x0025CAF0 File Offset: 0x0025ACF0
				public MultiplyRightNumber MultiplyRightNumber(ProgramNode node)
				{
					MultiplyRightNumber? multiplyRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MultiplyRightNumber.CreateSafe(this._builders, node);
					if (multiplyRightNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MultiplyRightNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiplyRightNumber.Value;
				}

				// Token: 0x0600A9F4 RID: 43508 RVA: 0x0025CB44 File Offset: 0x0025AD44
				public divideRight_arithmeticLeft divideRight_arithmeticLeft(ProgramNode node)
				{
					divideRight_arithmeticLeft? divideRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.divideRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (divideRight_arithmeticLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol divideRight_arithmeticLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return divideRight_arithmeticLeft.Value;
				}

				// Token: 0x0600A9F5 RID: 43509 RVA: 0x0025CB98 File Offset: 0x0025AD98
				public DivideRightNumber DivideRightNumber(ProgramNode node)
				{
					DivideRightNumber? divideRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DivideRightNumber.CreateSafe(this._builders, node);
					if (divideRightNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DivideRightNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return divideRightNumber.Value;
				}

				// Token: 0x0600A9F6 RID: 43510 RVA: 0x0025CBEC File Offset: 0x0025ADEC
				public inumber_fromNumber inumber_fromNumber(ProgramNode node)
				{
					inumber_fromNumber? inumber_fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.inumber_fromNumber.CreateSafe(this._builders, node);
					if (inumber_fromNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inumber_fromNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inumber_fromNumber.Value;
				}

				// Token: 0x0600A9F7 RID: 43511 RVA: 0x0025CC40 File Offset: 0x0025AE40
				public ParseNumber ParseNumber(ProgramNode node)
				{
					ParseNumber? parseNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
					if (parseNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ParseNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseNumber.Value;
				}

				// Token: 0x0600A9F8 RID: 43512 RVA: 0x0025CC94 File Offset: 0x0025AE94
				public rowNumberTransform_fromRowNumber rowNumberTransform_fromRowNumber(ProgramNode node)
				{
					rowNumberTransform_fromRowNumber? rowNumberTransform_fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.rowNumberTransform_fromRowNumber.CreateSafe(this._builders, node);
					if (rowNumberTransform_fromRowNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rowNumberTransform_fromRowNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowNumberTransform_fromRowNumber.Value;
				}

				// Token: 0x0600A9F9 RID: 43513 RVA: 0x0025CCE8 File Offset: 0x0025AEE8
				public RowNumberLinearTransform RowNumberLinearTransform(ProgramNode node)
				{
					RowNumberLinearTransform? rowNumberLinearTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RowNumberLinearTransform.CreateSafe(this._builders, node);
					if (rowNumberLinearTransform == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RowNumberLinearTransform but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowNumberLinearTransform.Value;
				}

				// Token: 0x0600A9FA RID: 43514 RVA: 0x0025CD3C File Offset: 0x0025AF3C
				public FormatDateTime FormatDateTime(ProgramNode node)
				{
					FormatDateTime? formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatDateTime.CreateSafe(this._builders, node);
					if (formatDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatDateTime.Value;
				}

				// Token: 0x0600A9FB RID: 43515 RVA: 0x0025CD90 File Offset: 0x0025AF90
				public date_idate date_idate(ProgramNode node)
				{
					date_idate? date_idate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.date_idate.CreateSafe(this._builders, node);
					if (date_idate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol date_idate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return date_idate.Value;
				}

				// Token: 0x0600A9FC RID: 43516 RVA: 0x0025CDE4 File Offset: 0x0025AFE4
				public RoundDateTime RoundDateTime(ProgramNode node)
				{
					RoundDateTime? roundDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundDateTime.CreateSafe(this._builders, node);
					if (roundDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RoundDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return roundDateTime.Value;
				}

				// Token: 0x0600A9FD RID: 43517 RVA: 0x0025CE38 File Offset: 0x0025B038
				public idate_fromDateTime idate_fromDateTime(ProgramNode node)
				{
					idate_fromDateTime? idate_fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTime.CreateSafe(this._builders, node);
					if (idate_fromDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idate_fromDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idate_fromDateTime.Value;
				}

				// Token: 0x0600A9FE RID: 43518 RVA: 0x0025CE8C File Offset: 0x0025B08C
				public idate_fromDateTimePart idate_fromDateTimePart(ProgramNode node)
				{
					idate_fromDateTimePart? idate_fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTimePart.CreateSafe(this._builders, node);
					if (idate_fromDateTimePart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idate_fromDateTimePart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idate_fromDateTimePart.Value;
				}

				// Token: 0x0600A9FF RID: 43519 RVA: 0x0025CEE0 File Offset: 0x0025B0E0
				public ParseDateTime ParseDateTime(ProgramNode node)
				{
					ParseDateTime? parseDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseDateTime.CreateSafe(this._builders, node);
					if (parseDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ParseDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseDateTime.Value;
				}

				// Token: 0x0600AA00 RID: 43520 RVA: 0x0025CF34 File Offset: 0x0025B134
				public itime_fromTime itime_fromTime(ProgramNode node)
				{
					itime_fromTime? itime_fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.itime_fromTime.CreateSafe(this._builders, node);
					if (itime_fromTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol itime_fromTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return itime_fromTime.Value;
				}

				// Token: 0x0600AA01 RID: 43521 RVA: 0x0025CF88 File Offset: 0x0025B188
				public parseSubject_fromStr parseSubject_fromStr(ProgramNode node)
				{
					parseSubject_fromStr? parseSubject_fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_fromStr.CreateSafe(this._builders, node);
					if (parseSubject_fromStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parseSubject_fromStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseSubject_fromStr.Value;
				}

				// Token: 0x0600AA02 RID: 43522 RVA: 0x0025CFDC File Offset: 0x0025B1DC
				public parseSubject_letSubstring parseSubject_letSubstring(ProgramNode node)
				{
					parseSubject_letSubstring? parseSubject_letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_letSubstring.CreateSafe(this._builders, node);
					if (parseSubject_letSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parseSubject_letSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseSubject_letSubstring.Value;
				}

				// Token: 0x0600AA03 RID: 43523 RVA: 0x0025D030 File Offset: 0x0025B230
				public LetX LetX(ProgramNode node)
				{
					LetX? letX = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
					if (letX == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetX but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letX.Value;
				}

				// Token: 0x0600AA04 RID: 43524 RVA: 0x0025D084 File Offset: 0x0025B284
				public substring_splitTrim substring_splitTrim(ProgramNode node)
				{
					substring_splitTrim? substring_splitTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_splitTrim.CreateSafe(this._builders, node);
					if (substring_splitTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substring_splitTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring_splitTrim.Value;
				}

				// Token: 0x0600AA05 RID: 43525 RVA: 0x0025D0D8 File Offset: 0x0025B2D8
				public SlicePrefixAbs SlicePrefixAbs(ProgramNode node)
				{
					SlicePrefixAbs? slicePrefixAbs = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefixAbs.CreateSafe(this._builders, node);
					if (slicePrefixAbs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SlicePrefixAbs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slicePrefixAbs.Value;
				}

				// Token: 0x0600AA06 RID: 43526 RVA: 0x0025D12C File Offset: 0x0025B32C
				public SlicePrefix SlicePrefix(ProgramNode node)
				{
					SlicePrefix? slicePrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefix.CreateSafe(this._builders, node);
					if (slicePrefix == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SlicePrefix but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slicePrefix.Value;
				}

				// Token: 0x0600AA07 RID: 43527 RVA: 0x0025D180 File Offset: 0x0025B380
				public SliceSuffix SliceSuffix(ProgramNode node)
				{
					SliceSuffix? sliceSuffix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceSuffix.CreateSafe(this._builders, node);
					if (sliceSuffix == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SliceSuffix but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceSuffix.Value;
				}

				// Token: 0x0600AA08 RID: 43528 RVA: 0x0025D1D4 File Offset: 0x0025B3D4
				public substring_sliceTrim substring_sliceTrim(ProgramNode node)
				{
					substring_sliceTrim? substring_sliceTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_sliceTrim.CreateSafe(this._builders, node);
					if (substring_sliceTrim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substring_sliceTrim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring_sliceTrim.Value;
				}

				// Token: 0x0600AA09 RID: 43529 RVA: 0x0025D228 File Offset: 0x0025B428
				public MatchFull MatchFull(ProgramNode node)
				{
					MatchFull? matchFull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchFull.CreateSafe(this._builders, node);
					if (matchFull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MatchFull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchFull.Value;
				}

				// Token: 0x0600AA0A RID: 43530 RVA: 0x0025D27C File Offset: 0x0025B47C
				public SliceBetween SliceBetween(ProgramNode node)
				{
					SliceBetween? sliceBetween = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceBetween.CreateSafe(this._builders, node);
					if (sliceBetween == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SliceBetween but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceBetween.Value;
				}

				// Token: 0x0600AA0B RID: 43531 RVA: 0x0025D2D0 File Offset: 0x0025B4D0
				public splitTrim_split splitTrim_split(ProgramNode node)
				{
					splitTrim_split? splitTrim_split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.splitTrim_split.CreateSafe(this._builders, node);
					if (splitTrim_split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitTrim_split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitTrim_split.Value;
				}

				// Token: 0x0600AA0C RID: 43532 RVA: 0x0025D324 File Offset: 0x0025B524
				public TrimSplit TrimSplit(ProgramNode node)
				{
					TrimSplit? trimSplit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSplit.CreateSafe(this._builders, node);
					if (trimSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimSplit.Value;
				}

				// Token: 0x0600AA0D RID: 43533 RVA: 0x0025D378 File Offset: 0x0025B578
				public TrimFullSplit TrimFullSplit(ProgramNode node)
				{
					TrimFullSplit? trimFullSplit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSplit.CreateSafe(this._builders, node);
					if (trimFullSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimFullSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimFullSplit.Value;
				}

				// Token: 0x0600AA0E RID: 43534 RVA: 0x0025D3CC File Offset: 0x0025B5CC
				public Split Split(ProgramNode node)
				{
					Split? split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return split.Value;
				}

				// Token: 0x0600AA0F RID: 43535 RVA: 0x0025D420 File Offset: 0x0025B620
				public sliceTrim_slice sliceTrim_slice(ProgramNode node)
				{
					sliceTrim_slice? sliceTrim_slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.sliceTrim_slice.CreateSafe(this._builders, node);
					if (sliceTrim_slice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sliceTrim_slice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sliceTrim_slice.Value;
				}

				// Token: 0x0600AA10 RID: 43536 RVA: 0x0025D474 File Offset: 0x0025B674
				public TrimSlice TrimSlice(ProgramNode node)
				{
					TrimSlice? trimSlice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSlice.CreateSafe(this._builders, node);
					if (trimSlice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimSlice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimSlice.Value;
				}

				// Token: 0x0600AA11 RID: 43537 RVA: 0x0025D4C8 File Offset: 0x0025B6C8
				public TrimFullSlice TrimFullSlice(ProgramNode node)
				{
					TrimFullSlice? trimFullSlice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSlice.CreateSafe(this._builders, node);
					if (trimFullSlice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimFullSlice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimFullSlice.Value;
				}

				// Token: 0x0600AA12 RID: 43538 RVA: 0x0025D51C File Offset: 0x0025B71C
				public Slice Slice(ProgramNode node)
				{
					Slice? slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Slice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slice.Value;
				}

				// Token: 0x0600AA13 RID: 43539 RVA: 0x0025D570 File Offset: 0x0025B770
				public Find Find(ProgramNode node)
				{
					Find? find = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Find.CreateSafe(this._builders, node);
					if (find == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Find but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return find.Value;
				}

				// Token: 0x0600AA14 RID: 43540 RVA: 0x0025D5C4 File Offset: 0x0025B7C4
				public Abs Abs(ProgramNode node)
				{
					Abs? abs = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Abs.CreateSafe(this._builders, node);
					if (abs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Abs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return abs.Value;
				}

				// Token: 0x0600AA15 RID: 43541 RVA: 0x0025D618 File Offset: 0x0025B818
				public Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match Match(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match? match = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match.CreateSafe(this._builders, node);
					if (match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return match.Value;
				}

				// Token: 0x0600AA16 RID: 43542 RVA: 0x0025D66C File Offset: 0x0025B86C
				public MatchEnd MatchEnd(ProgramNode node)
				{
					MatchEnd? matchEnd = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchEnd.CreateSafe(this._builders, node);
					if (matchEnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MatchEnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchEnd.Value;
				}

				// Token: 0x0600AA17 RID: 43543 RVA: 0x0025D6C0 File Offset: 0x0025B8C0
				public fromStrTrim_fromStr fromStrTrim_fromStr(ProgramNode node)
				{
					fromStrTrim_fromStr? fromStrTrim_fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromStr.CreateSafe(this._builders, node);
					if (fromStrTrim_fromStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromStrTrim_fromStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromStrTrim_fromStr.Value;
				}

				// Token: 0x0600AA18 RID: 43544 RVA: 0x0025D714 File Offset: 0x0025B914
				public fromStrTrim_fromNumberStr fromStrTrim_fromNumberStr(ProgramNode node)
				{
					fromStrTrim_fromNumberStr? fromStrTrim_fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromNumberStr.CreateSafe(this._builders, node);
					if (fromStrTrim_fromNumberStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fromStrTrim_fromNumberStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromStrTrim_fromNumberStr.Value;
				}

				// Token: 0x0600AA19 RID: 43545 RVA: 0x0025D768 File Offset: 0x0025B968
				public TrimFull TrimFull(ProgramNode node)
				{
					TrimFull? trimFull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFull.CreateSafe(this._builders, node);
					if (trimFull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimFull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimFull.Value;
				}

				// Token: 0x0600AA1A RID: 43546 RVA: 0x0025D7BC File Offset: 0x0025B9BC
				public Trim Trim(ProgramNode node)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Trim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trim.Value;
				}

				// Token: 0x0600AA1B RID: 43547 RVA: 0x0025D810 File Offset: 0x0025BA10
				public FromStr FromStr(ProgramNode node)
				{
					FromStr? fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromStr.CreateSafe(this._builders, node);
					if (fromStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromStr.Value;
				}

				// Token: 0x0600AA1C RID: 43548 RVA: 0x0025D864 File Offset: 0x0025BA64
				public FromNumberStr FromNumberStr(ProgramNode node)
				{
					FromNumberStr? fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberStr.CreateSafe(this._builders, node);
					if (fromNumberStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromNumberStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumberStr.Value;
				}

				// Token: 0x0600AA1D RID: 43549 RVA: 0x0025D8B8 File Offset: 0x0025BAB8
				public FromNumber FromNumber(ProgramNode node)
				{
					FromNumber? fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumber.CreateSafe(this._builders, node);
					if (fromNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumber.Value;
				}

				// Token: 0x0600AA1E RID: 43550 RVA: 0x0025D90C File Offset: 0x0025BB0C
				public FromNumberCoalesced FromNumberCoalesced(ProgramNode node)
				{
					FromNumberCoalesced? fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberCoalesced.CreateSafe(this._builders, node);
					if (fromNumberCoalesced == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromNumberCoalesced but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumberCoalesced.Value;
				}

				// Token: 0x0600AA1F RID: 43551 RVA: 0x0025D960 File Offset: 0x0025BB60
				public FromRowNumber FromRowNumber(ProgramNode node)
				{
					FromRowNumber? fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromRowNumber.CreateSafe(this._builders, node);
					if (fromRowNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromRowNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromRowNumber.Value;
				}

				// Token: 0x0600AA20 RID: 43552 RVA: 0x0025D9B4 File Offset: 0x0025BBB4
				public FromNumbers FromNumbers(ProgramNode node)
				{
					FromNumbers? fromNumbers = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumbers.CreateSafe(this._builders, node);
					if (fromNumbers == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromNumbers but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromNumbers.Value;
				}

				// Token: 0x0600AA21 RID: 43553 RVA: 0x0025DA08 File Offset: 0x0025BC08
				public FromDateTime FromDateTime(ProgramNode node)
				{
					FromDateTime? fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTime.CreateSafe(this._builders, node);
					if (fromDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromDateTime.Value;
				}

				// Token: 0x0600AA22 RID: 43554 RVA: 0x0025DA5C File Offset: 0x0025BC5C
				public FromDateTimePart FromDateTimePart(ProgramNode node)
				{
					FromDateTimePart? fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTimePart.CreateSafe(this._builders, node);
					if (fromDateTimePart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromDateTimePart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromDateTimePart.Value;
				}

				// Token: 0x0600AA23 RID: 43555 RVA: 0x0025DAB0 File Offset: 0x0025BCB0
				public FromTime FromTime(ProgramNode node)
				{
					FromTime? fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromTime.CreateSafe(this._builders, node);
					if (fromTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FromTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fromTime.Value;
				}

				// Token: 0x0600AA24 RID: 43556 RVA: 0x0025DB04 File Offset: 0x0025BD04
				public Str Str(ProgramNode node)
				{
					Str? str = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Str.CreateSafe(this._builders, node);
					if (str == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Str but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return str.Value;
				}

				// Token: 0x0600AA25 RID: 43557 RVA: 0x0025DB58 File Offset: 0x0025BD58
				public Number Number(ProgramNode node)
				{
					Number? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Number.CreateSafe(this._builders, node);
					if (number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Number but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number.Value;
				}

				// Token: 0x0600AA26 RID: 43558 RVA: 0x0025DBAC File Offset: 0x0025BDAC
				public Date Date(ProgramNode node)
				{
					Date? date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Date.CreateSafe(this._builders, node);
					if (date == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Date but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return date.Value;
				}

				// Token: 0x040044E2 RID: 17634
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001505 RID: 5381
			public class NodeIs
			{
				// Token: 0x0600AA27 RID: 43559 RVA: 0x0025DBFD File Offset: 0x0025BDFD
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600AA28 RID: 43560 RVA: 0x0025DC0C File Offset: 0x0025BE0C
				public bool result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA29 RID: 43561 RVA: 0x0025DC30 File Offset: 0x0025BE30
				public bool result(ProgramNode node, out result value)
				{
					result? result = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result.CreateSafe(this._builders, node);
					if (result == null)
					{
						value = default(result);
						return false;
					}
					value = result.Value;
					return true;
				}

				// Token: 0x0600AA2A RID: 43562 RVA: 0x0025DC6C File Offset: 0x0025BE6C
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA2B RID: 43563 RVA: 0x0025DC90 File Offset: 0x0025BE90
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x0600AA2C RID: 43564 RVA: 0x0025DCCC File Offset: 0x0025BECC
				public bool outNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA2D RID: 43565 RVA: 0x0025DCF0 File Offset: 0x0025BEF0
				public bool outNumber(ProgramNode node, out outNumber value)
				{
					outNumber? outNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber.CreateSafe(this._builders, node);
					if (outNumber == null)
					{
						value = default(outNumber);
						return false;
					}
					value = outNumber.Value;
					return true;
				}

				// Token: 0x0600AA2E RID: 43566 RVA: 0x0025DD2C File Offset: 0x0025BF2C
				public bool outDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA2F RID: 43567 RVA: 0x0025DD50 File Offset: 0x0025BF50
				public bool outDate(ProgramNode node, out outDate value)
				{
					outDate? outDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate.CreateSafe(this._builders, node);
					if (outDate == null)
					{
						value = default(outDate);
						return false;
					}
					value = outDate.Value;
					return true;
				}

				// Token: 0x0600AA30 RID: 43568 RVA: 0x0025DD8C File Offset: 0x0025BF8C
				public bool outStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA31 RID: 43569 RVA: 0x0025DDB0 File Offset: 0x0025BFB0
				public bool outStr(ProgramNode node, out outStr value)
				{
					outStr? outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr.CreateSafe(this._builders, node);
					if (outStr == null)
					{
						value = default(outStr);
						return false;
					}
					value = outStr.Value;
					return true;
				}

				// Token: 0x0600AA32 RID: 43570 RVA: 0x0025DDEC File Offset: 0x0025BFEC
				public bool outStr1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA33 RID: 43571 RVA: 0x0025DE10 File Offset: 0x0025C010
				public bool outStr1(ProgramNode node, out outStr1 value)
				{
					outStr1? outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1.CreateSafe(this._builders, node);
					if (outStr == null)
					{
						value = default(outStr1);
						return false;
					}
					value = outStr.Value;
					return true;
				}

				// Token: 0x0600AA34 RID: 43572 RVA: 0x0025DE4C File Offset: 0x0025C04C
				public bool segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA35 RID: 43573 RVA: 0x0025DE70 File Offset: 0x0025C070
				public bool segmentCase(ProgramNode node, out segmentCase value)
				{
					segmentCase? segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase.CreateSafe(this._builders, node);
					if (segmentCase == null)
					{
						value = default(segmentCase);
						return false;
					}
					value = segmentCase.Value;
					return true;
				}

				// Token: 0x0600AA36 RID: 43574 RVA: 0x0025DEAC File Offset: 0x0025C0AC
				public bool segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA37 RID: 43575 RVA: 0x0025DED0 File Offset: 0x0025C0D0
				public bool segment(ProgramNode node, out segment value)
				{
					segment? segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment.CreateSafe(this._builders, node);
					if (segment == null)
					{
						value = default(segment);
						return false;
					}
					value = segment.Value;
					return true;
				}

				// Token: 0x0600AA38 RID: 43576 RVA: 0x0025DF0C File Offset: 0x0025C10C
				public bool formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA39 RID: 43577 RVA: 0x0025DF30 File Offset: 0x0025C130
				public bool formatted(ProgramNode node, out formatted value)
				{
					formatted? formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted.CreateSafe(this._builders, node);
					if (formatted == null)
					{
						value = default(formatted);
						return false;
					}
					value = formatted.Value;
					return true;
				}

				// Token: 0x0600AA3A RID: 43578 RVA: 0x0025DF6C File Offset: 0x0025C16C
				public bool concatEntry(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA3B RID: 43579 RVA: 0x0025DF90 File Offset: 0x0025C190
				public bool concatEntry(ProgramNode node, out concatEntry value)
				{
					concatEntry? concatEntry = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry.CreateSafe(this._builders, node);
					if (concatEntry == null)
					{
						value = default(concatEntry);
						return false;
					}
					value = concatEntry.Value;
					return true;
				}

				// Token: 0x0600AA3C RID: 43580 RVA: 0x0025DFCC File Offset: 0x0025C1CC
				public bool concatCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA3D RID: 43581 RVA: 0x0025DFF0 File Offset: 0x0025C1F0
				public bool concatCase(ProgramNode node, out concatCase value)
				{
					concatCase? concatCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase.CreateSafe(this._builders, node);
					if (concatCase == null)
					{
						value = default(concatCase);
						return false;
					}
					value = concatCase.Value;
					return true;
				}

				// Token: 0x0600AA3E RID: 43582 RVA: 0x0025E02C File Offset: 0x0025C22C
				public bool concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA3F RID: 43583 RVA: 0x0025E050 File Offset: 0x0025C250
				public bool concat(ProgramNode node, out concat value)
				{
					concat? concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						value = default(concat);
						return false;
					}
					value = concat.Value;
					return true;
				}

				// Token: 0x0600AA40 RID: 43584 RVA: 0x0025E08C File Offset: 0x0025C28C
				public bool concatPrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA41 RID: 43585 RVA: 0x0025E0B0 File Offset: 0x0025C2B0
				public bool concatPrefix(ProgramNode node, out concatPrefix value)
				{
					concatPrefix? concatPrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix.CreateSafe(this._builders, node);
					if (concatPrefix == null)
					{
						value = default(concatPrefix);
						return false;
					}
					value = concatPrefix.Value;
					return true;
				}

				// Token: 0x0600AA42 RID: 43586 RVA: 0x0025E0EC File Offset: 0x0025C2EC
				public bool concatSegment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA43 RID: 43587 RVA: 0x0025E110 File Offset: 0x0025C310
				public bool concatSegment(ProgramNode node, out concatSegment value)
				{
					concatSegment? concatSegment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment.CreateSafe(this._builders, node);
					if (concatSegment == null)
					{
						value = default(concatSegment);
						return false;
					}
					value = concatSegment.Value;
					return true;
				}

				// Token: 0x0600AA44 RID: 43588 RVA: 0x0025E14C File Offset: 0x0025C34C
				public bool concatSuffix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA45 RID: 43589 RVA: 0x0025E170 File Offset: 0x0025C370
				public bool concatSuffix(ProgramNode node, out concatSuffix value)
				{
					concatSuffix? concatSuffix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix.CreateSafe(this._builders, node);
					if (concatSuffix == null)
					{
						value = default(concatSuffix);
						return false;
					}
					value = concatSuffix.Value;
					return true;
				}

				// Token: 0x0600AA46 RID: 43590 RVA: 0x0025E1AC File Offset: 0x0025C3AC
				public bool condition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA47 RID: 43591 RVA: 0x0025E1D0 File Offset: 0x0025C3D0
				public bool condition(ProgramNode node, out condition value)
				{
					condition? condition = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition.CreateSafe(this._builders, node);
					if (condition == null)
					{
						value = default(condition);
						return false;
					}
					value = condition.Value;
					return true;
				}

				// Token: 0x0600AA48 RID: 43592 RVA: 0x0025E20C File Offset: 0x0025C40C
				public bool or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA49 RID: 43593 RVA: 0x0025E230 File Offset: 0x0025C430
				public bool or(ProgramNode node, out or value)
				{
					or? or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or.CreateSafe(this._builders, node);
					if (or == null)
					{
						value = default(or);
						return false;
					}
					value = or.Value;
					return true;
				}

				// Token: 0x0600AA4A RID: 43594 RVA: 0x0025E26C File Offset: 0x0025C46C
				public bool inull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA4B RID: 43595 RVA: 0x0025E290 File Offset: 0x0025C490
				public bool inull(ProgramNode node, out inull value)
				{
					inull? inull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull.CreateSafe(this._builders, node);
					if (inull == null)
					{
						value = default(inull);
						return false;
					}
					value = inull.Value;
					return true;
				}

				// Token: 0x0600AA4C RID: 43596 RVA: 0x0025E2CC File Offset: 0x0025C4CC
				public bool equalsText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA4D RID: 43597 RVA: 0x0025E2F0 File Offset: 0x0025C4F0
				public bool equalsText(ProgramNode node, out equalsText value)
				{
					equalsText? equalsText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText.CreateSafe(this._builders, node);
					if (equalsText == null)
					{
						value = default(equalsText);
						return false;
					}
					value = equalsText.Value;
					return true;
				}

				// Token: 0x0600AA4E RID: 43598 RVA: 0x0025E32C File Offset: 0x0025C52C
				public bool containsFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA4F RID: 43599 RVA: 0x0025E350 File Offset: 0x0025C550
				public bool containsFindText(ProgramNode node, out containsFindText value)
				{
					containsFindText? containsFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText.CreateSafe(this._builders, node);
					if (containsFindText == null)
					{
						value = default(containsFindText);
						return false;
					}
					value = containsFindText.Value;
					return true;
				}

				// Token: 0x0600AA50 RID: 43600 RVA: 0x0025E38C File Offset: 0x0025C58C
				public bool startsWithFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA51 RID: 43601 RVA: 0x0025E3B0 File Offset: 0x0025C5B0
				public bool startsWithFindText(ProgramNode node, out startsWithFindText value)
				{
					startsWithFindText? startsWithFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText.CreateSafe(this._builders, node);
					if (startsWithFindText == null)
					{
						value = default(startsWithFindText);
						return false;
					}
					value = startsWithFindText.Value;
					return true;
				}

				// Token: 0x0600AA52 RID: 43602 RVA: 0x0025E3EC File Offset: 0x0025C5EC
				public bool isMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA53 RID: 43603 RVA: 0x0025E410 File Offset: 0x0025C610
				public bool isMatchRegex(ProgramNode node, out isMatchRegex value)
				{
					isMatchRegex? isMatchRegex = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex.CreateSafe(this._builders, node);
					if (isMatchRegex == null)
					{
						value = default(isMatchRegex);
						return false;
					}
					value = isMatchRegex.Value;
					return true;
				}

				// Token: 0x0600AA54 RID: 43604 RVA: 0x0025E44C File Offset: 0x0025C64C
				public bool containsMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA55 RID: 43605 RVA: 0x0025E470 File Offset: 0x0025C670
				public bool containsMatchRegex(ProgramNode node, out containsMatchRegex value)
				{
					containsMatchRegex? containsMatchRegex = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex.CreateSafe(this._builders, node);
					if (containsMatchRegex == null)
					{
						value = default(containsMatchRegex);
						return false;
					}
					value = containsMatchRegex.Value;
					return true;
				}

				// Token: 0x0600AA56 RID: 43606 RVA: 0x0025E4AC File Offset: 0x0025C6AC
				public bool containsCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA57 RID: 43607 RVA: 0x0025E4D0 File Offset: 0x0025C6D0
				public bool containsCount(ProgramNode node, out containsCount value)
				{
					containsCount? containsCount = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount.CreateSafe(this._builders, node);
					if (containsCount == null)
					{
						value = default(containsCount);
						return false;
					}
					value = containsCount.Value;
					return true;
				}

				// Token: 0x0600AA58 RID: 43608 RVA: 0x0025E50C File Offset: 0x0025C70C
				public bool matchCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA59 RID: 43609 RVA: 0x0025E530 File Offset: 0x0025C730
				public bool matchCount(ProgramNode node, out matchCount value)
				{
					matchCount? matchCount = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount.CreateSafe(this._builders, node);
					if (matchCount == null)
					{
						value = default(matchCount);
						return false;
					}
					value = matchCount.Value;
					return true;
				}

				// Token: 0x0600AA5A RID: 43610 RVA: 0x0025E56C File Offset: 0x0025C76C
				public bool numberEqualsValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA5B RID: 43611 RVA: 0x0025E590 File Offset: 0x0025C790
				public bool numberEqualsValue(ProgramNode node, out numberEqualsValue value)
				{
					numberEqualsValue? numberEqualsValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue.CreateSafe(this._builders, node);
					if (numberEqualsValue == null)
					{
						value = default(numberEqualsValue);
						return false;
					}
					value = numberEqualsValue.Value;
					return true;
				}

				// Token: 0x0600AA5C RID: 43612 RVA: 0x0025E5CC File Offset: 0x0025C7CC
				public bool numberGreaterThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA5D RID: 43613 RVA: 0x0025E5F0 File Offset: 0x0025C7F0
				public bool numberGreaterThanValue(ProgramNode node, out numberGreaterThanValue value)
				{
					numberGreaterThanValue? numberGreaterThanValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue.CreateSafe(this._builders, node);
					if (numberGreaterThanValue == null)
					{
						value = default(numberGreaterThanValue);
						return false;
					}
					value = numberGreaterThanValue.Value;
					return true;
				}

				// Token: 0x0600AA5E RID: 43614 RVA: 0x0025E62C File Offset: 0x0025C82C
				public bool numberLessThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA5F RID: 43615 RVA: 0x0025E650 File Offset: 0x0025C850
				public bool numberLessThanValue(ProgramNode node, out numberLessThanValue value)
				{
					numberLessThanValue? numberLessThanValue = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue.CreateSafe(this._builders, node);
					if (numberLessThanValue == null)
					{
						value = default(numberLessThanValue);
						return false;
					}
					value = numberLessThanValue.Value;
					return true;
				}

				// Token: 0x0600AA60 RID: 43616 RVA: 0x0025E68C File Offset: 0x0025C88C
				public bool formatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA61 RID: 43617 RVA: 0x0025E6B0 File Offset: 0x0025C8B0
				public bool formatNumber(ProgramNode node, out formatNumber value)
				{
					formatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						value = default(formatNumber);
						return false;
					}
					value = formatNumber.Value;
					return true;
				}

				// Token: 0x0600AA62 RID: 43618 RVA: 0x0025E6EC File Offset: 0x0025C8EC
				public bool number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA63 RID: 43619 RVA: 0x0025E710 File Offset: 0x0025C910
				public bool number(ProgramNode node, out number value)
				{
					number? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number.CreateSafe(this._builders, node);
					if (number == null)
					{
						value = default(number);
						return false;
					}
					value = number.Value;
					return true;
				}

				// Token: 0x0600AA64 RID: 43620 RVA: 0x0025E74C File Offset: 0x0025C94C
				public bool number1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA65 RID: 43621 RVA: 0x0025E770 File Offset: 0x0025C970
				public bool number1(ProgramNode node, out number1 value)
				{
					number1? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1.CreateSafe(this._builders, node);
					if (number == null)
					{
						value = default(number1);
						return false;
					}
					value = number.Value;
					return true;
				}

				// Token: 0x0600AA66 RID: 43622 RVA: 0x0025E7AC File Offset: 0x0025C9AC
				public bool arithmetic(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA67 RID: 43623 RVA: 0x0025E7D0 File Offset: 0x0025C9D0
				public bool arithmetic(ProgramNode node, out arithmetic value)
				{
					arithmetic? arithmetic = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic.CreateSafe(this._builders, node);
					if (arithmetic == null)
					{
						value = default(arithmetic);
						return false;
					}
					value = arithmetic.Value;
					return true;
				}

				// Token: 0x0600AA68 RID: 43624 RVA: 0x0025E80C File Offset: 0x0025CA0C
				public bool arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA69 RID: 43625 RVA: 0x0025E830 File Offset: 0x0025CA30
				public bool arithmeticLeft(ProgramNode node, out arithmeticLeft value)
				{
					arithmeticLeft? arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft.CreateSafe(this._builders, node);
					if (arithmeticLeft == null)
					{
						value = default(arithmeticLeft);
						return false;
					}
					value = arithmeticLeft.Value;
					return true;
				}

				// Token: 0x0600AA6A RID: 43626 RVA: 0x0025E86C File Offset: 0x0025CA6C
				public bool addRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA6B RID: 43627 RVA: 0x0025E890 File Offset: 0x0025CA90
				public bool addRight(ProgramNode node, out addRight value)
				{
					addRight? addRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight.CreateSafe(this._builders, node);
					if (addRight == null)
					{
						value = default(addRight);
						return false;
					}
					value = addRight.Value;
					return true;
				}

				// Token: 0x0600AA6C RID: 43628 RVA: 0x0025E8CC File Offset: 0x0025CACC
				public bool subtractRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA6D RID: 43629 RVA: 0x0025E8F0 File Offset: 0x0025CAF0
				public bool subtractRight(ProgramNode node, out subtractRight value)
				{
					subtractRight? subtractRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight.CreateSafe(this._builders, node);
					if (subtractRight == null)
					{
						value = default(subtractRight);
						return false;
					}
					value = subtractRight.Value;
					return true;
				}

				// Token: 0x0600AA6E RID: 43630 RVA: 0x0025E92C File Offset: 0x0025CB2C
				public bool multiplyRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA6F RID: 43631 RVA: 0x0025E950 File Offset: 0x0025CB50
				public bool multiplyRight(ProgramNode node, out multiplyRight value)
				{
					multiplyRight? multiplyRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight.CreateSafe(this._builders, node);
					if (multiplyRight == null)
					{
						value = default(multiplyRight);
						return false;
					}
					value = multiplyRight.Value;
					return true;
				}

				// Token: 0x0600AA70 RID: 43632 RVA: 0x0025E98C File Offset: 0x0025CB8C
				public bool divideRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA71 RID: 43633 RVA: 0x0025E9B0 File Offset: 0x0025CBB0
				public bool divideRight(ProgramNode node, out divideRight value)
				{
					divideRight? divideRight = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight.CreateSafe(this._builders, node);
					if (divideRight == null)
					{
						value = default(divideRight);
						return false;
					}
					value = divideRight.Value;
					return true;
				}

				// Token: 0x0600AA72 RID: 43634 RVA: 0x0025E9EC File Offset: 0x0025CBEC
				public bool inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA73 RID: 43635 RVA: 0x0025EA10 File Offset: 0x0025CC10
				public bool inumber(ProgramNode node, out inumber value)
				{
					inumber? inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber.CreateSafe(this._builders, node);
					if (inumber == null)
					{
						value = default(inumber);
						return false;
					}
					value = inumber.Value;
					return true;
				}

				// Token: 0x0600AA74 RID: 43636 RVA: 0x0025EA4C File Offset: 0x0025CC4C
				public bool rowNumberTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA75 RID: 43637 RVA: 0x0025EA70 File Offset: 0x0025CC70
				public bool rowNumberTransform(ProgramNode node, out rowNumberTransform value)
				{
					rowNumberTransform? rowNumberTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform.CreateSafe(this._builders, node);
					if (rowNumberTransform == null)
					{
						value = default(rowNumberTransform);
						return false;
					}
					value = rowNumberTransform.Value;
					return true;
				}

				// Token: 0x0600AA76 RID: 43638 RVA: 0x0025EAAC File Offset: 0x0025CCAC
				public bool formatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA77 RID: 43639 RVA: 0x0025EAD0 File Offset: 0x0025CCD0
				public bool formatDateTime(ProgramNode node, out formatDateTime value)
				{
					formatDateTime? formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime.CreateSafe(this._builders, node);
					if (formatDateTime == null)
					{
						value = default(formatDateTime);
						return false;
					}
					value = formatDateTime.Value;
					return true;
				}

				// Token: 0x0600AA78 RID: 43640 RVA: 0x0025EB0C File Offset: 0x0025CD0C
				public bool date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA79 RID: 43641 RVA: 0x0025EB30 File Offset: 0x0025CD30
				public bool date(ProgramNode node, out date value)
				{
					date? date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date.CreateSafe(this._builders, node);
					if (date == null)
					{
						value = default(date);
						return false;
					}
					value = date.Value;
					return true;
				}

				// Token: 0x0600AA7A RID: 43642 RVA: 0x0025EB6C File Offset: 0x0025CD6C
				public bool idate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA7B RID: 43643 RVA: 0x0025EB90 File Offset: 0x0025CD90
				public bool idate(ProgramNode node, out idate value)
				{
					idate? idate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate.CreateSafe(this._builders, node);
					if (idate == null)
					{
						value = default(idate);
						return false;
					}
					value = idate.Value;
					return true;
				}

				// Token: 0x0600AA7C RID: 43644 RVA: 0x0025EBCC File Offset: 0x0025CDCC
				public bool itime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA7D RID: 43645 RVA: 0x0025EBF0 File Offset: 0x0025CDF0
				public bool itime(ProgramNode node, out itime value)
				{
					itime? itime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime.CreateSafe(this._builders, node);
					if (itime == null)
					{
						value = default(itime);
						return false;
					}
					value = itime.Value;
					return true;
				}

				// Token: 0x0600AA7E RID: 43646 RVA: 0x0025EC2C File Offset: 0x0025CE2C
				public bool parseSubject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA7F RID: 43647 RVA: 0x0025EC50 File Offset: 0x0025CE50
				public bool parseSubject(ProgramNode node, out parseSubject value)
				{
					parseSubject? parseSubject = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject.CreateSafe(this._builders, node);
					if (parseSubject == null)
					{
						value = default(parseSubject);
						return false;
					}
					value = parseSubject.Value;
					return true;
				}

				// Token: 0x0600AA80 RID: 43648 RVA: 0x0025EC8C File Offset: 0x0025CE8C
				public bool letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA81 RID: 43649 RVA: 0x0025ECB0 File Offset: 0x0025CEB0
				public bool letSubstring(ProgramNode node, out letSubstring value)
				{
					letSubstring? letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring.CreateSafe(this._builders, node);
					if (letSubstring == null)
					{
						value = default(letSubstring);
						return false;
					}
					value = letSubstring.Value;
					return true;
				}

				// Token: 0x0600AA82 RID: 43650 RVA: 0x0025ECEC File Offset: 0x0025CEEC
				public bool x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA83 RID: 43651 RVA: 0x0025ED10 File Offset: 0x0025CF10
				public bool x(ProgramNode node, out x value)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						value = default(x);
						return false;
					}
					value = x.Value;
					return true;
				}

				// Token: 0x0600AA84 RID: 43652 RVA: 0x0025ED4C File Offset: 0x0025CF4C
				public bool substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA85 RID: 43653 RVA: 0x0025ED70 File Offset: 0x0025CF70
				public bool substring(ProgramNode node, out substring value)
				{
					substring? substring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						value = default(substring);
						return false;
					}
					value = substring.Value;
					return true;
				}

				// Token: 0x0600AA86 RID: 43654 RVA: 0x0025EDAC File Offset: 0x0025CFAC
				public bool splitTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA87 RID: 43655 RVA: 0x0025EDD0 File Offset: 0x0025CFD0
				public bool splitTrim(ProgramNode node, out splitTrim value)
				{
					splitTrim? splitTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim.CreateSafe(this._builders, node);
					if (splitTrim == null)
					{
						value = default(splitTrim);
						return false;
					}
					value = splitTrim.Value;
					return true;
				}

				// Token: 0x0600AA88 RID: 43656 RVA: 0x0025EE0C File Offset: 0x0025D00C
				public bool split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA89 RID: 43657 RVA: 0x0025EE30 File Offset: 0x0025D030
				public bool split(ProgramNode node, out split value)
				{
					split? split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split.CreateSafe(this._builders, node);
					if (split == null)
					{
						value = default(split);
						return false;
					}
					value = split.Value;
					return true;
				}

				// Token: 0x0600AA8A RID: 43658 RVA: 0x0025EE6C File Offset: 0x0025D06C
				public bool sliceTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA8B RID: 43659 RVA: 0x0025EE90 File Offset: 0x0025D090
				public bool sliceTrim(ProgramNode node, out sliceTrim value)
				{
					sliceTrim? sliceTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim.CreateSafe(this._builders, node);
					if (sliceTrim == null)
					{
						value = default(sliceTrim);
						return false;
					}
					value = sliceTrim.Value;
					return true;
				}

				// Token: 0x0600AA8C RID: 43660 RVA: 0x0025EECC File Offset: 0x0025D0CC
				public bool slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA8D RID: 43661 RVA: 0x0025EEF0 File Offset: 0x0025D0F0
				public bool slice(ProgramNode node, out slice value)
				{
					slice? slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						value = default(slice);
						return false;
					}
					value = slice.Value;
					return true;
				}

				// Token: 0x0600AA8E RID: 43662 RVA: 0x0025EF2C File Offset: 0x0025D12C
				public bool pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA8F RID: 43663 RVA: 0x0025EF50 File Offset: 0x0025D150
				public bool pos(ProgramNode node, out pos value)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						value = default(pos);
						return false;
					}
					value = pos.Value;
					return true;
				}

				// Token: 0x0600AA90 RID: 43664 RVA: 0x0025EF8C File Offset: 0x0025D18C
				public bool fromStrTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA91 RID: 43665 RVA: 0x0025EFB0 File Offset: 0x0025D1B0
				public bool fromStrTrim(ProgramNode node, out fromStrTrim value)
				{
					fromStrTrim? fromStrTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim.CreateSafe(this._builders, node);
					if (fromStrTrim == null)
					{
						value = default(fromStrTrim);
						return false;
					}
					value = fromStrTrim.Value;
					return true;
				}

				// Token: 0x0600AA92 RID: 43666 RVA: 0x0025EFEC File Offset: 0x0025D1EC
				public bool fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA93 RID: 43667 RVA: 0x0025F010 File Offset: 0x0025D210
				public bool fromStr(ProgramNode node, out fromStr value)
				{
					fromStr? fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr.CreateSafe(this._builders, node);
					if (fromStr == null)
					{
						value = default(fromStr);
						return false;
					}
					value = fromStr.Value;
					return true;
				}

				// Token: 0x0600AA94 RID: 43668 RVA: 0x0025F04C File Offset: 0x0025D24C
				public bool fromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA95 RID: 43669 RVA: 0x0025F070 File Offset: 0x0025D270
				public bool fromNumberStr(ProgramNode node, out fromNumberStr value)
				{
					fromNumberStr? fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr.CreateSafe(this._builders, node);
					if (fromNumberStr == null)
					{
						value = default(fromNumberStr);
						return false;
					}
					value = fromNumberStr.Value;
					return true;
				}

				// Token: 0x0600AA96 RID: 43670 RVA: 0x0025F0AC File Offset: 0x0025D2AC
				public bool fromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA97 RID: 43671 RVA: 0x0025F0D0 File Offset: 0x0025D2D0
				public bool fromNumber(ProgramNode node, out fromNumber value)
				{
					fromNumber? fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber.CreateSafe(this._builders, node);
					if (fromNumber == null)
					{
						value = default(fromNumber);
						return false;
					}
					value = fromNumber.Value;
					return true;
				}

				// Token: 0x0600AA98 RID: 43672 RVA: 0x0025F10C File Offset: 0x0025D30C
				public bool fromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA99 RID: 43673 RVA: 0x0025F130 File Offset: 0x0025D330
				public bool fromNumberCoalesced(ProgramNode node, out fromNumberCoalesced value)
				{
					fromNumberCoalesced? fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced.CreateSafe(this._builders, node);
					if (fromNumberCoalesced == null)
					{
						value = default(fromNumberCoalesced);
						return false;
					}
					value = fromNumberCoalesced.Value;
					return true;
				}

				// Token: 0x0600AA9A RID: 43674 RVA: 0x0025F16C File Offset: 0x0025D36C
				public bool fromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA9B RID: 43675 RVA: 0x0025F190 File Offset: 0x0025D390
				public bool fromRowNumber(ProgramNode node, out fromRowNumber value)
				{
					fromRowNumber? fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber.CreateSafe(this._builders, node);
					if (fromRowNumber == null)
					{
						value = default(fromRowNumber);
						return false;
					}
					value = fromRowNumber.Value;
					return true;
				}

				// Token: 0x0600AA9C RID: 43676 RVA: 0x0025F1CC File Offset: 0x0025D3CC
				public bool fromNumbers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA9D RID: 43677 RVA: 0x0025F1F0 File Offset: 0x0025D3F0
				public bool fromNumbers(ProgramNode node, out fromNumbers value)
				{
					fromNumbers? fromNumbers = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers.CreateSafe(this._builders, node);
					if (fromNumbers == null)
					{
						value = default(fromNumbers);
						return false;
					}
					value = fromNumbers.Value;
					return true;
				}

				// Token: 0x0600AA9E RID: 43678 RVA: 0x0025F22C File Offset: 0x0025D42C
				public bool fromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AA9F RID: 43679 RVA: 0x0025F250 File Offset: 0x0025D450
				public bool fromDateTime(ProgramNode node, out fromDateTime value)
				{
					fromDateTime? fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime.CreateSafe(this._builders, node);
					if (fromDateTime == null)
					{
						value = default(fromDateTime);
						return false;
					}
					value = fromDateTime.Value;
					return true;
				}

				// Token: 0x0600AAA0 RID: 43680 RVA: 0x0025F28C File Offset: 0x0025D48C
				public bool fromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAA1 RID: 43681 RVA: 0x0025F2B0 File Offset: 0x0025D4B0
				public bool fromDateTimePart(ProgramNode node, out fromDateTimePart value)
				{
					fromDateTimePart? fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart.CreateSafe(this._builders, node);
					if (fromDateTimePart == null)
					{
						value = default(fromDateTimePart);
						return false;
					}
					value = fromDateTimePart.Value;
					return true;
				}

				// Token: 0x0600AAA2 RID: 43682 RVA: 0x0025F2EC File Offset: 0x0025D4EC
				public bool fromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAA3 RID: 43683 RVA: 0x0025F310 File Offset: 0x0025D510
				public bool fromTime(ProgramNode node, out fromTime value)
				{
					fromTime? fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime.CreateSafe(this._builders, node);
					if (fromTime == null)
					{
						value = default(fromTime);
						return false;
					}
					value = fromTime.Value;
					return true;
				}

				// Token: 0x0600AAA4 RID: 43684 RVA: 0x0025F34C File Offset: 0x0025D54C
				public bool constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAA5 RID: 43685 RVA: 0x0025F370 File Offset: 0x0025D570
				public bool constString(ProgramNode node, out constString value)
				{
					constString? constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString.CreateSafe(this._builders, node);
					if (constString == null)
					{
						value = default(constString);
						return false;
					}
					value = constString.Value;
					return true;
				}

				// Token: 0x0600AAA6 RID: 43686 RVA: 0x0025F3AC File Offset: 0x0025D5AC
				public bool constNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAA7 RID: 43687 RVA: 0x0025F3D0 File Offset: 0x0025D5D0
				public bool constNumber(ProgramNode node, out constNumber value)
				{
					constNumber? constNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber.CreateSafe(this._builders, node);
					if (constNumber == null)
					{
						value = default(constNumber);
						return false;
					}
					value = constNumber.Value;
					return true;
				}

				// Token: 0x0600AAA8 RID: 43688 RVA: 0x0025F40C File Offset: 0x0025D60C
				public bool constDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAA9 RID: 43689 RVA: 0x0025F430 File Offset: 0x0025D630
				public bool constDate(ProgramNode node, out constDate value)
				{
					constDate? constDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate.CreateSafe(this._builders, node);
					if (constDate == null)
					{
						value = default(constDate);
						return false;
					}
					value = constDate.Value;
					return true;
				}

				// Token: 0x0600AAAA RID: 43690 RVA: 0x0025F46C File Offset: 0x0025D66C
				public bool columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAAB RID: 43691 RVA: 0x0025F490 File Offset: 0x0025D690
				public bool columnName(ProgramNode node, out columnName value)
				{
					columnName? columnName = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
					if (columnName == null)
					{
						value = default(columnName);
						return false;
					}
					value = columnName.Value;
					return true;
				}

				// Token: 0x0600AAAC RID: 43692 RVA: 0x0025F4CC File Offset: 0x0025D6CC
				public bool columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAAD RID: 43693 RVA: 0x0025F4F0 File Offset: 0x0025D6F0
				public bool columnNames(ProgramNode node, out columnNames value)
				{
					columnNames? columnNames = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						value = default(columnNames);
						return false;
					}
					value = columnNames.Value;
					return true;
				}

				// Token: 0x0600AAAE RID: 43694 RVA: 0x0025F52C File Offset: 0x0025D72C
				public bool constStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAAF RID: 43695 RVA: 0x0025F550 File Offset: 0x0025D750
				public bool constStr(ProgramNode node, out constStr value)
				{
					constStr? constStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						value = default(constStr);
						return false;
					}
					value = constStr.Value;
					return true;
				}

				// Token: 0x0600AAB0 RID: 43696 RVA: 0x0025F58C File Offset: 0x0025D78C
				public bool constNum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAB1 RID: 43697 RVA: 0x0025F5B0 File Offset: 0x0025D7B0
				public bool constNum(ProgramNode node, out constNum value)
				{
					constNum? constNum = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum.CreateSafe(this._builders, node);
					if (constNum == null)
					{
						value = default(constNum);
						return false;
					}
					value = constNum.Value;
					return true;
				}

				// Token: 0x0600AAB2 RID: 43698 RVA: 0x0025F5EC File Offset: 0x0025D7EC
				public bool constDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAB3 RID: 43699 RVA: 0x0025F610 File Offset: 0x0025D810
				public bool constDt(ProgramNode node, out constDt value)
				{
					constDt? constDt = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt.CreateSafe(this._builders, node);
					if (constDt == null)
					{
						value = default(constDt);
						return false;
					}
					value = constDt.Value;
					return true;
				}

				// Token: 0x0600AAB4 RID: 43700 RVA: 0x0025F64C File Offset: 0x0025D84C
				public bool locale(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAB5 RID: 43701 RVA: 0x0025F670 File Offset: 0x0025D870
				public bool locale(ProgramNode node, out locale value)
				{
					locale? locale = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale.CreateSafe(this._builders, node);
					if (locale == null)
					{
						value = default(locale);
						return false;
					}
					value = locale.Value;
					return true;
				}

				// Token: 0x0600AAB6 RID: 43702 RVA: 0x0025F6AC File Offset: 0x0025D8AC
				public bool replaceFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAB7 RID: 43703 RVA: 0x0025F6D0 File Offset: 0x0025D8D0
				public bool replaceFindText(ProgramNode node, out replaceFindText value)
				{
					replaceFindText? replaceFindText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText.CreateSafe(this._builders, node);
					if (replaceFindText == null)
					{
						value = default(replaceFindText);
						return false;
					}
					value = replaceFindText.Value;
					return true;
				}

				// Token: 0x0600AAB8 RID: 43704 RVA: 0x0025F70C File Offset: 0x0025D90C
				public bool replaceText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAB9 RID: 43705 RVA: 0x0025F730 File Offset: 0x0025D930
				public bool replaceText(ProgramNode node, out replaceText value)
				{
					replaceText? replaceText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText.CreateSafe(this._builders, node);
					if (replaceText == null)
					{
						value = default(replaceText);
						return false;
					}
					value = replaceText.Value;
					return true;
				}

				// Token: 0x0600AABA RID: 43706 RVA: 0x0025F76C File Offset: 0x0025D96C
				public bool sliceBetweenStartText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AABB RID: 43707 RVA: 0x0025F790 File Offset: 0x0025D990
				public bool sliceBetweenStartText(ProgramNode node, out sliceBetweenStartText value)
				{
					sliceBetweenStartText? sliceBetweenStartText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText.CreateSafe(this._builders, node);
					if (sliceBetweenStartText == null)
					{
						value = default(sliceBetweenStartText);
						return false;
					}
					value = sliceBetweenStartText.Value;
					return true;
				}

				// Token: 0x0600AABC RID: 43708 RVA: 0x0025F7CC File Offset: 0x0025D9CC
				public bool sliceBetweenEndText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AABD RID: 43709 RVA: 0x0025F7F0 File Offset: 0x0025D9F0
				public bool sliceBetweenEndText(ProgramNode node, out sliceBetweenEndText value)
				{
					sliceBetweenEndText? sliceBetweenEndText = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText.CreateSafe(this._builders, node);
					if (sliceBetweenEndText == null)
					{
						value = default(sliceBetweenEndText);
						return false;
					}
					value = sliceBetweenEndText.Value;
					return true;
				}

				// Token: 0x0600AABE RID: 43710 RVA: 0x0025F82C File Offset: 0x0025DA2C
				public bool numberFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AABF RID: 43711 RVA: 0x0025F850 File Offset: 0x0025DA50
				public bool numberFormatDesc(ProgramNode node, out numberFormatDesc value)
				{
					numberFormatDesc? numberFormatDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc.CreateSafe(this._builders, node);
					if (numberFormatDesc == null)
					{
						value = default(numberFormatDesc);
						return false;
					}
					value = numberFormatDesc.Value;
					return true;
				}

				// Token: 0x0600AAC0 RID: 43712 RVA: 0x0025F88C File Offset: 0x0025DA8C
				public bool numberRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAC1 RID: 43713 RVA: 0x0025F8B0 File Offset: 0x0025DAB0
				public bool numberRoundDesc(ProgramNode node, out numberRoundDesc value)
				{
					numberRoundDesc? numberRoundDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc.CreateSafe(this._builders, node);
					if (numberRoundDesc == null)
					{
						value = default(numberRoundDesc);
						return false;
					}
					value = numberRoundDesc.Value;
					return true;
				}

				// Token: 0x0600AAC2 RID: 43714 RVA: 0x0025F8EC File Offset: 0x0025DAEC
				public bool dateTimeRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAC3 RID: 43715 RVA: 0x0025F910 File Offset: 0x0025DB10
				public bool dateTimeRoundDesc(ProgramNode node, out dateTimeRoundDesc value)
				{
					dateTimeRoundDesc? dateTimeRoundDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc.CreateSafe(this._builders, node);
					if (dateTimeRoundDesc == null)
					{
						value = default(dateTimeRoundDesc);
						return false;
					}
					value = dateTimeRoundDesc.Value;
					return true;
				}

				// Token: 0x0600AAC4 RID: 43716 RVA: 0x0025F94C File Offset: 0x0025DB4C
				public bool dateTimeFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAC5 RID: 43717 RVA: 0x0025F970 File Offset: 0x0025DB70
				public bool dateTimeFormatDesc(ProgramNode node, out dateTimeFormatDesc value)
				{
					dateTimeFormatDesc? dateTimeFormatDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc.CreateSafe(this._builders, node);
					if (dateTimeFormatDesc == null)
					{
						value = default(dateTimeFormatDesc);
						return false;
					}
					value = dateTimeFormatDesc.Value;
					return true;
				}

				// Token: 0x0600AAC6 RID: 43718 RVA: 0x0025F9AC File Offset: 0x0025DBAC
				public bool dateTimeParseDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAC7 RID: 43719 RVA: 0x0025F9D0 File Offset: 0x0025DBD0
				public bool dateTimeParseDesc(ProgramNode node, out dateTimeParseDesc value)
				{
					dateTimeParseDesc? dateTimeParseDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc.CreateSafe(this._builders, node);
					if (dateTimeParseDesc == null)
					{
						value = default(dateTimeParseDesc);
						return false;
					}
					value = dateTimeParseDesc.Value;
					return true;
				}

				// Token: 0x0600AAC8 RID: 43720 RVA: 0x0025FA0C File Offset: 0x0025DC0C
				public bool dateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAC9 RID: 43721 RVA: 0x0025FA30 File Offset: 0x0025DC30
				public bool dateTimePartKind(ProgramNode node, out dateTimePartKind value)
				{
					dateTimePartKind? dateTimePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind.CreateSafe(this._builders, node);
					if (dateTimePartKind == null)
					{
						value = default(dateTimePartKind);
						return false;
					}
					value = dateTimePartKind.Value;
					return true;
				}

				// Token: 0x0600AACA RID: 43722 RVA: 0x0025FA6C File Offset: 0x0025DC6C
				public bool fromDateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AACB RID: 43723 RVA: 0x0025FA90 File Offset: 0x0025DC90
				public bool fromDateTimePartKind(ProgramNode node, out fromDateTimePartKind value)
				{
					fromDateTimePartKind? fromDateTimePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind.CreateSafe(this._builders, node);
					if (fromDateTimePartKind == null)
					{
						value = default(fromDateTimePartKind);
						return false;
					}
					value = fromDateTimePartKind.Value;
					return true;
				}

				// Token: 0x0600AACC RID: 43724 RVA: 0x0025FACC File Offset: 0x0025DCCC
				public bool timePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AACD RID: 43725 RVA: 0x0025FAF0 File Offset: 0x0025DCF0
				public bool timePartKind(ProgramNode node, out timePartKind value)
				{
					timePartKind? timePartKind = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind.CreateSafe(this._builders, node);
					if (timePartKind == null)
					{
						value = default(timePartKind);
						return false;
					}
					value = timePartKind.Value;
					return true;
				}

				// Token: 0x0600AACE RID: 43726 RVA: 0x0025FB2C File Offset: 0x0025DD2C
				public bool rowNumberLinearTransformDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AACF RID: 43727 RVA: 0x0025FB50 File Offset: 0x0025DD50
				public bool rowNumberLinearTransformDesc(ProgramNode node, out rowNumberLinearTransformDesc value)
				{
					rowNumberLinearTransformDesc? rowNumberLinearTransformDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc.CreateSafe(this._builders, node);
					if (rowNumberLinearTransformDesc == null)
					{
						value = default(rowNumberLinearTransformDesc);
						return false;
					}
					value = rowNumberLinearTransformDesc.Value;
					return true;
				}

				// Token: 0x0600AAD0 RID: 43728 RVA: 0x0025FB8C File Offset: 0x0025DD8C
				public bool matchDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAD1 RID: 43729 RVA: 0x0025FBB0 File Offset: 0x0025DDB0
				public bool matchDesc(ProgramNode node, out matchDesc value)
				{
					matchDesc? matchDesc = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc.CreateSafe(this._builders, node);
					if (matchDesc == null)
					{
						value = default(matchDesc);
						return false;
					}
					value = matchDesc.Value;
					return true;
				}

				// Token: 0x0600AAD2 RID: 43730 RVA: 0x0025FBEC File Offset: 0x0025DDEC
				public bool matchInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAD3 RID: 43731 RVA: 0x0025FC10 File Offset: 0x0025DE10
				public bool matchInstance(ProgramNode node, out matchInstance value)
				{
					matchInstance? matchInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance.CreateSafe(this._builders, node);
					if (matchInstance == null)
					{
						value = default(matchInstance);
						return false;
					}
					value = matchInstance.Value;
					return true;
				}

				// Token: 0x0600AAD4 RID: 43732 RVA: 0x0025FC4C File Offset: 0x0025DE4C
				public bool splitDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAD5 RID: 43733 RVA: 0x0025FC70 File Offset: 0x0025DE70
				public bool splitDelimiter(ProgramNode node, out splitDelimiter value)
				{
					splitDelimiter? splitDelimiter = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter.CreateSafe(this._builders, node);
					if (splitDelimiter == null)
					{
						value = default(splitDelimiter);
						return false;
					}
					value = splitDelimiter.Value;
					return true;
				}

				// Token: 0x0600AAD6 RID: 43734 RVA: 0x0025FCAC File Offset: 0x0025DEAC
				public bool splitInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAD7 RID: 43735 RVA: 0x0025FCD0 File Offset: 0x0025DED0
				public bool splitInstance(ProgramNode node, out splitInstance value)
				{
					splitInstance? splitInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance.CreateSafe(this._builders, node);
					if (splitInstance == null)
					{
						value = default(splitInstance);
						return false;
					}
					value = splitInstance.Value;
					return true;
				}

				// Token: 0x0600AAD8 RID: 43736 RVA: 0x0025FD0C File Offset: 0x0025DF0C
				public bool findDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAD9 RID: 43737 RVA: 0x0025FD30 File Offset: 0x0025DF30
				public bool findDelimiter(ProgramNode node, out findDelimiter value)
				{
					findDelimiter? findDelimiter = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter.CreateSafe(this._builders, node);
					if (findDelimiter == null)
					{
						value = default(findDelimiter);
						return false;
					}
					value = findDelimiter.Value;
					return true;
				}

				// Token: 0x0600AADA RID: 43738 RVA: 0x0025FD6C File Offset: 0x0025DF6C
				public bool findInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AADB RID: 43739 RVA: 0x0025FD90 File Offset: 0x0025DF90
				public bool findInstance(ProgramNode node, out findInstance value)
				{
					findInstance? findInstance = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance.CreateSafe(this._builders, node);
					if (findInstance == null)
					{
						value = default(findInstance);
						return false;
					}
					value = findInstance.Value;
					return true;
				}

				// Token: 0x0600AADC RID: 43740 RVA: 0x0025FDCC File Offset: 0x0025DFCC
				public bool findOffset(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AADD RID: 43741 RVA: 0x0025FDF0 File Offset: 0x0025DFF0
				public bool findOffset(ProgramNode node, out findOffset value)
				{
					findOffset? findOffset = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset.CreateSafe(this._builders, node);
					if (findOffset == null)
					{
						value = default(findOffset);
						return false;
					}
					value = findOffset.Value;
					return true;
				}

				// Token: 0x0600AADE RID: 43742 RVA: 0x0025FE2C File Offset: 0x0025E02C
				public bool slicePrefixAbsPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AADF RID: 43743 RVA: 0x0025FE50 File Offset: 0x0025E050
				public bool slicePrefixAbsPos(ProgramNode node, out slicePrefixAbsPos value)
				{
					slicePrefixAbsPos? slicePrefixAbsPos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos.CreateSafe(this._builders, node);
					if (slicePrefixAbsPos == null)
					{
						value = default(slicePrefixAbsPos);
						return false;
					}
					value = slicePrefixAbsPos.Value;
					return true;
				}

				// Token: 0x0600AAE0 RID: 43744 RVA: 0x0025FE8C File Offset: 0x0025E08C
				public bool scaleNumberFactor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAE1 RID: 43745 RVA: 0x0025FEB0 File Offset: 0x0025E0B0
				public bool scaleNumberFactor(ProgramNode node, out scaleNumberFactor value)
				{
					scaleNumberFactor? scaleNumberFactor = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor.CreateSafe(this._builders, node);
					if (scaleNumberFactor == null)
					{
						value = default(scaleNumberFactor);
						return false;
					}
					value = scaleNumberFactor.Value;
					return true;
				}

				// Token: 0x0600AAE2 RID: 43746 RVA: 0x0025FEEC File Offset: 0x0025E0EC
				public bool absPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAE3 RID: 43747 RVA: 0x0025FF10 File Offset: 0x0025E110
				public bool absPos(ProgramNode node, out absPos value)
				{
					absPos? absPos = Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos.CreateSafe(this._builders, node);
					if (absPos == null)
					{
						value = default(absPos);
						return false;
					}
					value = absPos.Value;
					return true;
				}

				// Token: 0x040044E3 RID: 17635
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001506 RID: 5382
			public class RuleIs
			{
				// Token: 0x0600AAE4 RID: 43748 RVA: 0x0025FF4A File Offset: 0x0025E14A
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600AAE5 RID: 43749 RVA: 0x0025FF5C File Offset: 0x0025E15C
				public bool result_output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAE6 RID: 43750 RVA: 0x0025FF80 File Offset: 0x0025E180
				public bool result_output(ProgramNode node, out result_output value)
				{
					result_output? result_output = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_output.CreateSafe(this._builders, node);
					if (result_output == null)
					{
						value = default(result_output);
						return false;
					}
					value = result_output.Value;
					return true;
				}

				// Token: 0x0600AAE7 RID: 43751 RVA: 0x0025FFBC File Offset: 0x0025E1BC
				public bool result_inull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_inull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAE8 RID: 43752 RVA: 0x0025FFE0 File Offset: 0x0025E1E0
				public bool result_inull(ProgramNode node, out result_inull value)
				{
					result_inull? result_inull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_inull.CreateSafe(this._builders, node);
					if (result_inull == null)
					{
						value = default(result_inull);
						return false;
					}
					value = result_inull.Value;
					return true;
				}

				// Token: 0x0600AAE9 RID: 43753 RVA: 0x0026001C File Offset: 0x0025E21C
				public bool If(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.If.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAEA RID: 43754 RVA: 0x00260040 File Offset: 0x0025E240
				public bool If(ProgramNode node, out If value)
				{
					If? @if = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.If.CreateSafe(this._builders, node);
					if (@if == null)
					{
						value = default(If);
						return false;
					}
					value = @if.Value;
					return true;
				}

				// Token: 0x0600AAEB RID: 43755 RVA: 0x0026007C File Offset: 0x0025E27C
				public bool ToInt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToInt.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAEC RID: 43756 RVA: 0x002600A0 File Offset: 0x0025E2A0
				public bool ToInt(ProgramNode node, out ToInt value)
				{
					ToInt? toInt = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToInt.CreateSafe(this._builders, node);
					if (toInt == null)
					{
						value = default(ToInt);
						return false;
					}
					value = toInt.Value;
					return true;
				}

				// Token: 0x0600AAED RID: 43757 RVA: 0x002600DC File Offset: 0x0025E2DC
				public bool ToDouble(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDouble.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAEE RID: 43758 RVA: 0x00260100 File Offset: 0x0025E300
				public bool ToDouble(ProgramNode node, out ToDouble value)
				{
					ToDouble? toDouble = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDouble.CreateSafe(this._builders, node);
					if (toDouble == null)
					{
						value = default(ToDouble);
						return false;
					}
					value = toDouble.Value;
					return true;
				}

				// Token: 0x0600AAEF RID: 43759 RVA: 0x0026013C File Offset: 0x0025E33C
				public bool ToDecimal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDecimal.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAF0 RID: 43760 RVA: 0x00260160 File Offset: 0x0025E360
				public bool ToDecimal(ProgramNode node, out ToDecimal value)
				{
					ToDecimal? toDecimal = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDecimal.CreateSafe(this._builders, node);
					if (toDecimal == null)
					{
						value = default(ToDecimal);
						return false;
					}
					value = toDecimal.Value;
					return true;
				}

				// Token: 0x0600AAF1 RID: 43761 RVA: 0x0026019C File Offset: 0x0025E39C
				public bool ToDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAF2 RID: 43762 RVA: 0x002601C0 File Offset: 0x0025E3C0
				public bool ToDateTime(ProgramNode node, out ToDateTime value)
				{
					ToDateTime? toDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDateTime.CreateSafe(this._builders, node);
					if (toDateTime == null)
					{
						value = default(ToDateTime);
						return false;
					}
					value = toDateTime.Value;
					return true;
				}

				// Token: 0x0600AAF3 RID: 43763 RVA: 0x002601FC File Offset: 0x0025E3FC
				public bool ToStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAF4 RID: 43764 RVA: 0x00260220 File Offset: 0x0025E420
				public bool ToStr(ProgramNode node, out ToStr value)
				{
					ToStr? toStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToStr.CreateSafe(this._builders, node);
					if (toStr == null)
					{
						value = default(ToStr);
						return false;
					}
					value = toStr.Value;
					return true;
				}

				// Token: 0x0600AAF5 RID: 43765 RVA: 0x0026025C File Offset: 0x0025E45C
				public bool outNumber_number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_number.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAF6 RID: 43766 RVA: 0x00260280 File Offset: 0x0025E480
				public bool outNumber_number(ProgramNode node, out outNumber_number value)
				{
					outNumber_number? outNumber_number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_number.CreateSafe(this._builders, node);
					if (outNumber_number == null)
					{
						value = default(outNumber_number);
						return false;
					}
					value = outNumber_number.Value;
					return true;
				}

				// Token: 0x0600AAF7 RID: 43767 RVA: 0x002602BC File Offset: 0x0025E4BC
				public bool outNumber_constNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_constNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAF8 RID: 43768 RVA: 0x002602E0 File Offset: 0x0025E4E0
				public bool outNumber_constNumber(ProgramNode node, out outNumber_constNumber value)
				{
					outNumber_constNumber? outNumber_constNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_constNumber.CreateSafe(this._builders, node);
					if (outNumber_constNumber == null)
					{
						value = default(outNumber_constNumber);
						return false;
					}
					value = outNumber_constNumber.Value;
					return true;
				}

				// Token: 0x0600AAF9 RID: 43769 RVA: 0x0026031C File Offset: 0x0025E51C
				public bool outDate_date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_date.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAFA RID: 43770 RVA: 0x00260340 File Offset: 0x0025E540
				public bool outDate_date(ProgramNode node, out outDate_date value)
				{
					outDate_date? outDate_date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_date.CreateSafe(this._builders, node);
					if (outDate_date == null)
					{
						value = default(outDate_date);
						return false;
					}
					value = outDate_date.Value;
					return true;
				}

				// Token: 0x0600AAFB RID: 43771 RVA: 0x0026037C File Offset: 0x0025E57C
				public bool outDate_constDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_constDate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAFC RID: 43772 RVA: 0x002603A0 File Offset: 0x0025E5A0
				public bool outDate_constDate(ProgramNode node, out outDate_constDate value)
				{
					outDate_constDate? outDate_constDate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_constDate.CreateSafe(this._builders, node);
					if (outDate_constDate == null)
					{
						value = default(outDate_constDate);
						return false;
					}
					value = outDate_constDate.Value;
					return true;
				}

				// Token: 0x0600AAFD RID: 43773 RVA: 0x002603DC File Offset: 0x0025E5DC
				public bool outStr_outStr1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr_outStr1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AAFE RID: 43774 RVA: 0x00260400 File Offset: 0x0025E600
				public bool outStr_outStr1(ProgramNode node, out outStr_outStr1 value)
				{
					outStr_outStr1? outStr_outStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr_outStr1.CreateSafe(this._builders, node);
					if (outStr_outStr == null)
					{
						value = default(outStr_outStr1);
						return false;
					}
					value = outStr_outStr.Value;
					return true;
				}

				// Token: 0x0600AAFF RID: 43775 RVA: 0x0026043C File Offset: 0x0025E63C
				public bool Replace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Replace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB00 RID: 43776 RVA: 0x00260460 File Offset: 0x0025E660
				public bool Replace(ProgramNode node, out Replace value)
				{
					Replace? replace = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Replace.CreateSafe(this._builders, node);
					if (replace == null)
					{
						value = default(Replace);
						return false;
					}
					value = replace.Value;
					return true;
				}

				// Token: 0x0600AB01 RID: 43777 RVA: 0x0026049C File Offset: 0x0025E69C
				public bool outStr1_segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_segmentCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB02 RID: 43778 RVA: 0x002604C0 File Offset: 0x0025E6C0
				public bool outStr1_segmentCase(ProgramNode node, out outStr1_segmentCase value)
				{
					outStr1_segmentCase? outStr1_segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_segmentCase.CreateSafe(this._builders, node);
					if (outStr1_segmentCase == null)
					{
						value = default(outStr1_segmentCase);
						return false;
					}
					value = outStr1_segmentCase.Value;
					return true;
				}

				// Token: 0x0600AB03 RID: 43779 RVA: 0x002604FC File Offset: 0x0025E6FC
				public bool outStr1_formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_formatted.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB04 RID: 43780 RVA: 0x00260520 File Offset: 0x0025E720
				public bool outStr1_formatted(ProgramNode node, out outStr1_formatted value)
				{
					outStr1_formatted? outStr1_formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_formatted.CreateSafe(this._builders, node);
					if (outStr1_formatted == null)
					{
						value = default(outStr1_formatted);
						return false;
					}
					value = outStr1_formatted.Value;
					return true;
				}

				// Token: 0x0600AB05 RID: 43781 RVA: 0x0026055C File Offset: 0x0025E75C
				public bool outStr1_concatEntry(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_concatEntry.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB06 RID: 43782 RVA: 0x00260580 File Offset: 0x0025E780
				public bool outStr1_concatEntry(ProgramNode node, out outStr1_concatEntry value)
				{
					outStr1_concatEntry? outStr1_concatEntry = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_concatEntry.CreateSafe(this._builders, node);
					if (outStr1_concatEntry == null)
					{
						value = default(outStr1_concatEntry);
						return false;
					}
					value = outStr1_concatEntry.Value;
					return true;
				}

				// Token: 0x0600AB07 RID: 43783 RVA: 0x002605BC File Offset: 0x0025E7BC
				public bool outStr1_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_constString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB08 RID: 43784 RVA: 0x002605E0 File Offset: 0x0025E7E0
				public bool outStr1_constString(ProgramNode node, out outStr1_constString value)
				{
					outStr1_constString? outStr1_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_constString.CreateSafe(this._builders, node);
					if (outStr1_constString == null)
					{
						value = default(outStr1_constString);
						return false;
					}
					value = outStr1_constString.Value;
					return true;
				}

				// Token: 0x0600AB09 RID: 43785 RVA: 0x0026061C File Offset: 0x0025E81C
				public bool segmentCase_segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segmentCase_segment.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB0A RID: 43786 RVA: 0x00260640 File Offset: 0x0025E840
				public bool segmentCase_segment(ProgramNode node, out segmentCase_segment value)
				{
					segmentCase_segment? segmentCase_segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segmentCase_segment.CreateSafe(this._builders, node);
					if (segmentCase_segment == null)
					{
						value = default(segmentCase_segment);
						return false;
					}
					value = segmentCase_segment.Value;
					return true;
				}

				// Token: 0x0600AB0B RID: 43787 RVA: 0x0026067C File Offset: 0x0025E87C
				public bool LowerCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB0C RID: 43788 RVA: 0x002606A0 File Offset: 0x0025E8A0
				public bool LowerCase(ProgramNode node, out LowerCase value)
				{
					LowerCase? lowerCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCase.CreateSafe(this._builders, node);
					if (lowerCase == null)
					{
						value = default(LowerCase);
						return false;
					}
					value = lowerCase.Value;
					return true;
				}

				// Token: 0x0600AB0D RID: 43789 RVA: 0x002606DC File Offset: 0x0025E8DC
				public bool UpperCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB0E RID: 43790 RVA: 0x00260700 File Offset: 0x0025E900
				public bool UpperCase(ProgramNode node, out UpperCase value)
				{
					UpperCase? upperCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCase.CreateSafe(this._builders, node);
					if (upperCase == null)
					{
						value = default(UpperCase);
						return false;
					}
					value = upperCase.Value;
					return true;
				}

				// Token: 0x0600AB0F RID: 43791 RVA: 0x0026073C File Offset: 0x0025E93C
				public bool ProperCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB10 RID: 43792 RVA: 0x00260760 File Offset: 0x0025E960
				public bool ProperCase(ProgramNode node, out ProperCase value)
				{
					ProperCase? properCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCase.CreateSafe(this._builders, node);
					if (properCase == null)
					{
						value = default(ProperCase);
						return false;
					}
					value = properCase.Value;
					return true;
				}

				// Token: 0x0600AB11 RID: 43793 RVA: 0x0026079C File Offset: 0x0025E99C
				public bool segment_fromStrTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_fromStrTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB12 RID: 43794 RVA: 0x002607C0 File Offset: 0x0025E9C0
				public bool segment_fromStrTrim(ProgramNode node, out segment_fromStrTrim value)
				{
					segment_fromStrTrim? segment_fromStrTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_fromStrTrim.CreateSafe(this._builders, node);
					if (segment_fromStrTrim == null)
					{
						value = default(segment_fromStrTrim);
						return false;
					}
					value = segment_fromStrTrim.Value;
					return true;
				}

				// Token: 0x0600AB13 RID: 43795 RVA: 0x002607FC File Offset: 0x0025E9FC
				public bool segment_letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_letSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB14 RID: 43796 RVA: 0x00260820 File Offset: 0x0025EA20
				public bool segment_letSubstring(ProgramNode node, out segment_letSubstring value)
				{
					segment_letSubstring? segment_letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_letSubstring.CreateSafe(this._builders, node);
					if (segment_letSubstring == null)
					{
						value = default(segment_letSubstring);
						return false;
					}
					value = segment_letSubstring.Value;
					return true;
				}

				// Token: 0x0600AB15 RID: 43797 RVA: 0x0026085C File Offset: 0x0025EA5C
				public bool formatted_formatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB16 RID: 43798 RVA: 0x00260880 File Offset: 0x0025EA80
				public bool formatted_formatNumber(ProgramNode node, out formatted_formatNumber value)
				{
					formatted_formatNumber? formatted_formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatNumber.CreateSafe(this._builders, node);
					if (formatted_formatNumber == null)
					{
						value = default(formatted_formatNumber);
						return false;
					}
					value = formatted_formatNumber.Value;
					return true;
				}

				// Token: 0x0600AB17 RID: 43799 RVA: 0x002608BC File Offset: 0x0025EABC
				public bool formatted_formatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB18 RID: 43800 RVA: 0x002608E0 File Offset: 0x0025EAE0
				public bool formatted_formatDateTime(ProgramNode node, out formatted_formatDateTime value)
				{
					formatted_formatDateTime? formatted_formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatDateTime.CreateSafe(this._builders, node);
					if (formatted_formatDateTime == null)
					{
						value = default(formatted_formatDateTime);
						return false;
					}
					value = formatted_formatDateTime.Value;
					return true;
				}

				// Token: 0x0600AB19 RID: 43801 RVA: 0x0026091C File Offset: 0x0025EB1C
				public bool concatEntry_concatCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_concatCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB1A RID: 43802 RVA: 0x00260940 File Offset: 0x0025EB40
				public bool concatEntry_concatCase(ProgramNode node, out concatEntry_concatCase value)
				{
					concatEntry_concatCase? concatEntry_concatCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_concatCase.CreateSafe(this._builders, node);
					if (concatEntry_concatCase == null)
					{
						value = default(concatEntry_concatCase);
						return false;
					}
					value = concatEntry_concatCase.Value;
					return true;
				}

				// Token: 0x0600AB1B RID: 43803 RVA: 0x0026097C File Offset: 0x0025EB7C
				public bool concatEntry_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_constString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB1C RID: 43804 RVA: 0x002609A0 File Offset: 0x0025EBA0
				public bool concatEntry_constString(ProgramNode node, out concatEntry_constString value)
				{
					concatEntry_constString? concatEntry_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_constString.CreateSafe(this._builders, node);
					if (concatEntry_constString == null)
					{
						value = default(concatEntry_constString);
						return false;
					}
					value = concatEntry_constString.Value;
					return true;
				}

				// Token: 0x0600AB1D RID: 43805 RVA: 0x002609DC File Offset: 0x0025EBDC
				public bool concatCase_concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatCase_concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB1E RID: 43806 RVA: 0x00260A00 File Offset: 0x0025EC00
				public bool concatCase_concat(ProgramNode node, out concatCase_concat value)
				{
					concatCase_concat? concatCase_concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatCase_concat.CreateSafe(this._builders, node);
					if (concatCase_concat == null)
					{
						value = default(concatCase_concat);
						return false;
					}
					value = concatCase_concat.Value;
					return true;
				}

				// Token: 0x0600AB1F RID: 43807 RVA: 0x00260A3C File Offset: 0x0025EC3C
				public bool LowerCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCaseConcat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB20 RID: 43808 RVA: 0x00260A60 File Offset: 0x0025EC60
				public bool LowerCaseConcat(ProgramNode node, out LowerCaseConcat value)
				{
					LowerCaseConcat? lowerCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCaseConcat.CreateSafe(this._builders, node);
					if (lowerCaseConcat == null)
					{
						value = default(LowerCaseConcat);
						return false;
					}
					value = lowerCaseConcat.Value;
					return true;
				}

				// Token: 0x0600AB21 RID: 43809 RVA: 0x00260A9C File Offset: 0x0025EC9C
				public bool UpperCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCaseConcat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB22 RID: 43810 RVA: 0x00260AC0 File Offset: 0x0025ECC0
				public bool UpperCaseConcat(ProgramNode node, out UpperCaseConcat value)
				{
					UpperCaseConcat? upperCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCaseConcat.CreateSafe(this._builders, node);
					if (upperCaseConcat == null)
					{
						value = default(UpperCaseConcat);
						return false;
					}
					value = upperCaseConcat.Value;
					return true;
				}

				// Token: 0x0600AB23 RID: 43811 RVA: 0x00260AFC File Offset: 0x0025ECFC
				public bool ProperCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCaseConcat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB24 RID: 43812 RVA: 0x00260B20 File Offset: 0x0025ED20
				public bool ProperCaseConcat(ProgramNode node, out ProperCaseConcat value)
				{
					ProperCaseConcat? properCaseConcat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCaseConcat.CreateSafe(this._builders, node);
					if (properCaseConcat == null)
					{
						value = default(ProperCaseConcat);
						return false;
					}
					value = properCaseConcat.Value;
					return true;
				}

				// Token: 0x0600AB25 RID: 43813 RVA: 0x00260B5C File Offset: 0x0025ED5C
				public bool Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB26 RID: 43814 RVA: 0x00260B80 File Offset: 0x0025ED80
				public bool Concat(ProgramNode node, out Concat value)
				{
					Concat? concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						value = default(Concat);
						return false;
					}
					value = concat.Value;
					return true;
				}

				// Token: 0x0600AB27 RID: 43815 RVA: 0x00260BBC File Offset: 0x0025EDBC
				public bool concatPrefix_concatSegment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_concatSegment.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB28 RID: 43816 RVA: 0x00260BE0 File Offset: 0x0025EDE0
				public bool concatPrefix_concatSegment(ProgramNode node, out concatPrefix_concatSegment value)
				{
					concatPrefix_concatSegment? concatPrefix_concatSegment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_concatSegment.CreateSafe(this._builders, node);
					if (concatPrefix_concatSegment == null)
					{
						value = default(concatPrefix_concatSegment);
						return false;
					}
					value = concatPrefix_concatSegment.Value;
					return true;
				}

				// Token: 0x0600AB29 RID: 43817 RVA: 0x00260C1C File Offset: 0x0025EE1C
				public bool concatPrefix_formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_formatted.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB2A RID: 43818 RVA: 0x00260C40 File Offset: 0x0025EE40
				public bool concatPrefix_formatted(ProgramNode node, out concatPrefix_formatted value)
				{
					concatPrefix_formatted? concatPrefix_formatted = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_formatted.CreateSafe(this._builders, node);
					if (concatPrefix_formatted == null)
					{
						value = default(concatPrefix_formatted);
						return false;
					}
					value = concatPrefix_formatted.Value;
					return true;
				}

				// Token: 0x0600AB2B RID: 43819 RVA: 0x00260C7C File Offset: 0x0025EE7C
				public bool concatPrefix_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_constString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB2C RID: 43820 RVA: 0x00260CA0 File Offset: 0x0025EEA0
				public bool concatPrefix_constString(ProgramNode node, out concatPrefix_constString value)
				{
					concatPrefix_constString? concatPrefix_constString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_constString.CreateSafe(this._builders, node);
					if (concatPrefix_constString == null)
					{
						value = default(concatPrefix_constString);
						return false;
					}
					value = concatPrefix_constString.Value;
					return true;
				}

				// Token: 0x0600AB2D RID: 43821 RVA: 0x00260CDC File Offset: 0x0025EEDC
				public bool concatSegment_segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segment.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB2E RID: 43822 RVA: 0x00260D00 File Offset: 0x0025EF00
				public bool concatSegment_segment(ProgramNode node, out concatSegment_segment value)
				{
					concatSegment_segment? concatSegment_segment = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segment.CreateSafe(this._builders, node);
					if (concatSegment_segment == null)
					{
						value = default(concatSegment_segment);
						return false;
					}
					value = concatSegment_segment.Value;
					return true;
				}

				// Token: 0x0600AB2F RID: 43823 RVA: 0x00260D3C File Offset: 0x0025EF3C
				public bool concatSegment_segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segmentCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB30 RID: 43824 RVA: 0x00260D60 File Offset: 0x0025EF60
				public bool concatSegment_segmentCase(ProgramNode node, out concatSegment_segmentCase value)
				{
					concatSegment_segmentCase? concatSegment_segmentCase = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segmentCase.CreateSafe(this._builders, node);
					if (concatSegment_segmentCase == null)
					{
						value = default(concatSegment_segmentCase);
						return false;
					}
					value = concatSegment_segmentCase.Value;
					return true;
				}

				// Token: 0x0600AB31 RID: 43825 RVA: 0x00260D9C File Offset: 0x0025EF9C
				public bool concatSuffix_concatPrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concatPrefix.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB32 RID: 43826 RVA: 0x00260DC0 File Offset: 0x0025EFC0
				public bool concatSuffix_concatPrefix(ProgramNode node, out concatSuffix_concatPrefix value)
				{
					concatSuffix_concatPrefix? concatSuffix_concatPrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concatPrefix.CreateSafe(this._builders, node);
					if (concatSuffix_concatPrefix == null)
					{
						value = default(concatSuffix_concatPrefix);
						return false;
					}
					value = concatSuffix_concatPrefix.Value;
					return true;
				}

				// Token: 0x0600AB33 RID: 43827 RVA: 0x00260DFC File Offset: 0x0025EFFC
				public bool concatSuffix_concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB34 RID: 43828 RVA: 0x00260E20 File Offset: 0x0025F020
				public bool concatSuffix_concat(ProgramNode node, out concatSuffix_concat value)
				{
					concatSuffix_concat? concatSuffix_concat = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concat.CreateSafe(this._builders, node);
					if (concatSuffix_concat == null)
					{
						value = default(concatSuffix_concat);
						return false;
					}
					value = concatSuffix_concat.Value;
					return true;
				}

				// Token: 0x0600AB35 RID: 43829 RVA: 0x00260E5C File Offset: 0x0025F05C
				public bool StringEquals(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StringEquals.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB36 RID: 43830 RVA: 0x00260E80 File Offset: 0x0025F080
				public bool StringEquals(ProgramNode node, out StringEquals value)
				{
					StringEquals? stringEquals = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StringEquals.CreateSafe(this._builders, node);
					if (stringEquals == null)
					{
						value = default(StringEquals);
						return false;
					}
					value = stringEquals.Value;
					return true;
				}

				// Token: 0x0600AB37 RID: 43831 RVA: 0x00260EBC File Offset: 0x0025F0BC
				public bool Contains(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB38 RID: 43832 RVA: 0x00260EE0 File Offset: 0x0025F0E0
				public bool Contains(ProgramNode node, out Contains value)
				{
					Contains? contains = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
					if (contains == null)
					{
						value = default(Contains);
						return false;
					}
					value = contains.Value;
					return true;
				}

				// Token: 0x0600AB39 RID: 43833 RVA: 0x00260F1C File Offset: 0x0025F11C
				public bool StartsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB3A RID: 43834 RVA: 0x00260F40 File Offset: 0x0025F140
				public bool StartsWithDigit(ProgramNode node, out StartsWithDigit value)
				{
					StartsWithDigit? startsWithDigit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
					if (startsWithDigit == null)
					{
						value = default(StartsWithDigit);
						return false;
					}
					value = startsWithDigit.Value;
					return true;
				}

				// Token: 0x0600AB3B RID: 43835 RVA: 0x00260F7C File Offset: 0x0025F17C
				public bool EndsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB3C RID: 43836 RVA: 0x00260FA0 File Offset: 0x0025F1A0
				public bool EndsWithDigit(ProgramNode node, out EndsWithDigit value)
				{
					EndsWithDigit? endsWithDigit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
					if (endsWithDigit == null)
					{
						value = default(EndsWithDigit);
						return false;
					}
					value = endsWithDigit.Value;
					return true;
				}

				// Token: 0x0600AB3D RID: 43837 RVA: 0x00260FDC File Offset: 0x0025F1DC
				public bool StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB3E RID: 43838 RVA: 0x00261000 File Offset: 0x0025F200
				public bool StartsWith(ProgramNode node, out StartsWith value)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						value = default(StartsWith);
						return false;
					}
					value = startsWith.Value;
					return true;
				}

				// Token: 0x0600AB3F RID: 43839 RVA: 0x0026103C File Offset: 0x0025F23C
				public bool IsBlank(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsBlank.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB40 RID: 43840 RVA: 0x00261060 File Offset: 0x0025F260
				public bool IsBlank(ProgramNode node, out IsBlank value)
				{
					IsBlank? isBlank = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsBlank.CreateSafe(this._builders, node);
					if (isBlank == null)
					{
						value = default(IsBlank);
						return false;
					}
					value = isBlank.Value;
					return true;
				}

				// Token: 0x0600AB41 RID: 43841 RVA: 0x0026109C File Offset: 0x0025F29C
				public bool IsNotBlank(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNotBlank.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB42 RID: 43842 RVA: 0x002610C0 File Offset: 0x0025F2C0
				public bool IsNotBlank(ProgramNode node, out IsNotBlank value)
				{
					IsNotBlank? isNotBlank = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNotBlank.CreateSafe(this._builders, node);
					if (isNotBlank == null)
					{
						value = default(IsNotBlank);
						return false;
					}
					value = isNotBlank.Value;
					return true;
				}

				// Token: 0x0600AB43 RID: 43843 RVA: 0x002610FC File Offset: 0x0025F2FC
				public bool NumberEquals(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberEquals.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB44 RID: 43844 RVA: 0x00261120 File Offset: 0x0025F320
				public bool NumberEquals(ProgramNode node, out NumberEquals value)
				{
					NumberEquals? numberEquals = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberEquals.CreateSafe(this._builders, node);
					if (numberEquals == null)
					{
						value = default(NumberEquals);
						return false;
					}
					value = numberEquals.Value;
					return true;
				}

				// Token: 0x0600AB45 RID: 43845 RVA: 0x0026115C File Offset: 0x0025F35C
				public bool NumberGreaterThan(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberGreaterThan.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB46 RID: 43846 RVA: 0x00261180 File Offset: 0x0025F380
				public bool NumberGreaterThan(ProgramNode node, out NumberGreaterThan value)
				{
					NumberGreaterThan? numberGreaterThan = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberGreaterThan.CreateSafe(this._builders, node);
					if (numberGreaterThan == null)
					{
						value = default(NumberGreaterThan);
						return false;
					}
					value = numberGreaterThan.Value;
					return true;
				}

				// Token: 0x0600AB47 RID: 43847 RVA: 0x002611BC File Offset: 0x0025F3BC
				public bool NumberLessThan(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberLessThan.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB48 RID: 43848 RVA: 0x002611E0 File Offset: 0x0025F3E0
				public bool NumberLessThan(ProgramNode node, out NumberLessThan value)
				{
					NumberLessThan? numberLessThan = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberLessThan.CreateSafe(this._builders, node);
					if (numberLessThan == null)
					{
						value = default(NumberLessThan);
						return false;
					}
					value = numberLessThan.Value;
					return true;
				}

				// Token: 0x0600AB49 RID: 43849 RVA: 0x0026121C File Offset: 0x0025F41C
				public bool IsString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB4A RID: 43850 RVA: 0x00261240 File Offset: 0x0025F440
				public bool IsString(ProgramNode node, out IsString value)
				{
					IsString? isString = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsString.CreateSafe(this._builders, node);
					if (isString == null)
					{
						value = default(IsString);
						return false;
					}
					value = isString.Value;
					return true;
				}

				// Token: 0x0600AB4B RID: 43851 RVA: 0x0026127C File Offset: 0x0025F47C
				public bool IsNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB4C RID: 43852 RVA: 0x002612A0 File Offset: 0x0025F4A0
				public bool IsNumber(ProgramNode node, out IsNumber value)
				{
					IsNumber? isNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNumber.CreateSafe(this._builders, node);
					if (isNumber == null)
					{
						value = default(IsNumber);
						return false;
					}
					value = isNumber.Value;
					return true;
				}

				// Token: 0x0600AB4D RID: 43853 RVA: 0x002612DC File Offset: 0x0025F4DC
				public bool IsMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB4E RID: 43854 RVA: 0x00261300 File Offset: 0x0025F500
				public bool IsMatch(ProgramNode node, out IsMatch value)
				{
					IsMatch? isMatch = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsMatch.CreateSafe(this._builders, node);
					if (isMatch == null)
					{
						value = default(IsMatch);
						return false;
					}
					value = isMatch.Value;
					return true;
				}

				// Token: 0x0600AB4F RID: 43855 RVA: 0x0026133C File Offset: 0x0025F53C
				public bool ContainsMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ContainsMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB50 RID: 43856 RVA: 0x00261360 File Offset: 0x0025F560
				public bool ContainsMatch(ProgramNode node, out ContainsMatch value)
				{
					ContainsMatch? containsMatch = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ContainsMatch.CreateSafe(this._builders, node);
					if (containsMatch == null)
					{
						value = default(ContainsMatch);
						return false;
					}
					value = containsMatch.Value;
					return true;
				}

				// Token: 0x0600AB51 RID: 43857 RVA: 0x0026139C File Offset: 0x0025F59C
				public bool condition_or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.condition_or.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB52 RID: 43858 RVA: 0x002613C0 File Offset: 0x0025F5C0
				public bool condition_or(ProgramNode node, out condition_or value)
				{
					condition_or? condition_or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.condition_or.CreateSafe(this._builders, node);
					if (condition_or == null)
					{
						value = default(condition_or);
						return false;
					}
					value = condition_or.Value;
					return true;
				}

				// Token: 0x0600AB53 RID: 43859 RVA: 0x002613FC File Offset: 0x0025F5FC
				public bool Or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Or.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB54 RID: 43860 RVA: 0x00261420 File Offset: 0x0025F620
				public bool Or(ProgramNode node, out Or value)
				{
					Or? or = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Or.CreateSafe(this._builders, node);
					if (or == null)
					{
						value = default(Or);
						return false;
					}
					value = or.Value;
					return true;
				}

				// Token: 0x0600AB55 RID: 43861 RVA: 0x0026145C File Offset: 0x0025F65C
				public bool Null(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Null.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB56 RID: 43862 RVA: 0x00261480 File Offset: 0x0025F680
				public bool Null(ProgramNode node, out Null value)
				{
					Null? @null = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Null.CreateSafe(this._builders, node);
					if (@null == null)
					{
						value = default(Null);
						return false;
					}
					value = @null.Value;
					return true;
				}

				// Token: 0x0600AB57 RID: 43863 RVA: 0x002614BC File Offset: 0x0025F6BC
				public bool FormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB58 RID: 43864 RVA: 0x002614E0 File Offset: 0x0025F6E0
				public bool FormatNumber(ProgramNode node, out FormatNumber value)
				{
					FormatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						value = default(FormatNumber);
						return false;
					}
					value = formatNumber.Value;
					return true;
				}

				// Token: 0x0600AB59 RID: 43865 RVA: 0x0026151C File Offset: 0x0025F71C
				public bool number_number1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_number1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB5A RID: 43866 RVA: 0x00261540 File Offset: 0x0025F740
				public bool number_number1(ProgramNode node, out number_number1 value)
				{
					number_number1? number_number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_number1.CreateSafe(this._builders, node);
					if (number_number == null)
					{
						value = default(number_number1);
						return false;
					}
					value = number_number.Value;
					return true;
				}

				// Token: 0x0600AB5B RID: 43867 RVA: 0x0026157C File Offset: 0x0025F77C
				public bool number_arithmetic(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_arithmetic.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB5C RID: 43868 RVA: 0x002615A0 File Offset: 0x0025F7A0
				public bool number_arithmetic(ProgramNode node, out number_arithmetic value)
				{
					number_arithmetic? number_arithmetic = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_arithmetic.CreateSafe(this._builders, node);
					if (number_arithmetic == null)
					{
						value = default(number_arithmetic);
						return false;
					}
					value = number_arithmetic.Value;
					return true;
				}

				// Token: 0x0600AB5D RID: 43869 RVA: 0x002615DC File Offset: 0x0025F7DC
				public bool number_rowNumberTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_rowNumberTransform.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB5E RID: 43870 RVA: 0x00261600 File Offset: 0x0025F800
				public bool number_rowNumberTransform(ProgramNode node, out number_rowNumberTransform value)
				{
					number_rowNumberTransform? number_rowNumberTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_rowNumberTransform.CreateSafe(this._builders, node);
					if (number_rowNumberTransform == null)
					{
						value = default(number_rowNumberTransform);
						return false;
					}
					value = number_rowNumberTransform.Value;
					return true;
				}

				// Token: 0x0600AB5F RID: 43871 RVA: 0x0026163C File Offset: 0x0025F83C
				public bool Length(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Length.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB60 RID: 43872 RVA: 0x00261660 File Offset: 0x0025F860
				public bool Length(ProgramNode node, out Length value)
				{
					Length? length = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Length.CreateSafe(this._builders, node);
					if (length == null)
					{
						value = default(Length);
						return false;
					}
					value = length.Value;
					return true;
				}

				// Token: 0x0600AB61 RID: 43873 RVA: 0x0026169C File Offset: 0x0025F89C
				public bool number1_inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number1_inumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB62 RID: 43874 RVA: 0x002616C0 File Offset: 0x0025F8C0
				public bool number1_inumber(ProgramNode node, out number1_inumber value)
				{
					number1_inumber? number1_inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number1_inumber.CreateSafe(this._builders, node);
					if (number1_inumber == null)
					{
						value = default(number1_inumber);
						return false;
					}
					value = number1_inumber.Value;
					return true;
				}

				// Token: 0x0600AB63 RID: 43875 RVA: 0x002616FC File Offset: 0x0025F8FC
				public bool DateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DateTimePart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB64 RID: 43876 RVA: 0x00261720 File Offset: 0x0025F920
				public bool DateTimePart(ProgramNode node, out DateTimePart value)
				{
					DateTimePart? dateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DateTimePart.CreateSafe(this._builders, node);
					if (dateTimePart == null)
					{
						value = default(DateTimePart);
						return false;
					}
					value = dateTimePart.Value;
					return true;
				}

				// Token: 0x0600AB65 RID: 43877 RVA: 0x0026175C File Offset: 0x0025F95C
				public bool TimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TimePart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB66 RID: 43878 RVA: 0x00261780 File Offset: 0x0025F980
				public bool TimePart(ProgramNode node, out TimePart value)
				{
					TimePart? timePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TimePart.CreateSafe(this._builders, node);
					if (timePart == null)
					{
						value = default(TimePart);
						return false;
					}
					value = timePart.Value;
					return true;
				}

				// Token: 0x0600AB67 RID: 43879 RVA: 0x002617BC File Offset: 0x0025F9BC
				public bool RoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB68 RID: 43880 RVA: 0x002617E0 File Offset: 0x0025F9E0
				public bool RoundNumber(ProgramNode node, out RoundNumber value)
				{
					RoundNumber? roundNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
					if (roundNumber == null)
					{
						value = default(RoundNumber);
						return false;
					}
					value = roundNumber.Value;
					return true;
				}

				// Token: 0x0600AB69 RID: 43881 RVA: 0x0026181C File Offset: 0x0025FA1C
				public bool Add(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB6A RID: 43882 RVA: 0x00261840 File Offset: 0x0025FA40
				public bool Add(ProgramNode node, out Add value)
				{
					Add? add = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
					if (add == null)
					{
						value = default(Add);
						return false;
					}
					value = add.Value;
					return true;
				}

				// Token: 0x0600AB6B RID: 43883 RVA: 0x0026187C File Offset: 0x0025FA7C
				public bool Subtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Subtract.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB6C RID: 43884 RVA: 0x002618A0 File Offset: 0x0025FAA0
				public bool Subtract(ProgramNode node, out Subtract value)
				{
					Subtract? subtract = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Subtract.CreateSafe(this._builders, node);
					if (subtract == null)
					{
						value = default(Subtract);
						return false;
					}
					value = subtract.Value;
					return true;
				}

				// Token: 0x0600AB6D RID: 43885 RVA: 0x002618DC File Offset: 0x0025FADC
				public bool Multiply(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Multiply.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB6E RID: 43886 RVA: 0x00261900 File Offset: 0x0025FB00
				public bool Multiply(ProgramNode node, out Multiply value)
				{
					Multiply? multiply = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Multiply.CreateSafe(this._builders, node);
					if (multiply == null)
					{
						value = default(Multiply);
						return false;
					}
					value = multiply.Value;
					return true;
				}

				// Token: 0x0600AB6F RID: 43887 RVA: 0x0026193C File Offset: 0x0025FB3C
				public bool Divide(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Divide.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB70 RID: 43888 RVA: 0x00261960 File Offset: 0x0025FB60
				public bool Divide(ProgramNode node, out Divide value)
				{
					Divide? divide = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Divide.CreateSafe(this._builders, node);
					if (divide == null)
					{
						value = default(Divide);
						return false;
					}
					value = divide.Value;
					return true;
				}

				// Token: 0x0600AB71 RID: 43889 RVA: 0x0026199C File Offset: 0x0025FB9C
				public bool Sum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Sum.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB72 RID: 43890 RVA: 0x002619C0 File Offset: 0x0025FBC0
				public bool Sum(ProgramNode node, out Sum value)
				{
					Sum? sum = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Sum.CreateSafe(this._builders, node);
					if (sum == null)
					{
						value = default(Sum);
						return false;
					}
					value = sum.Value;
					return true;
				}

				// Token: 0x0600AB73 RID: 43891 RVA: 0x002619FC File Offset: 0x0025FBFC
				public bool Product(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Product.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB74 RID: 43892 RVA: 0x00261A20 File Offset: 0x0025FC20
				public bool Product(ProgramNode node, out Product value)
				{
					Product? product = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Product.CreateSafe(this._builders, node);
					if (product == null)
					{
						value = default(Product);
						return false;
					}
					value = product.Value;
					return true;
				}

				// Token: 0x0600AB75 RID: 43893 RVA: 0x00261A5C File Offset: 0x0025FC5C
				public bool Average(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Average.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB76 RID: 43894 RVA: 0x00261A80 File Offset: 0x0025FC80
				public bool Average(ProgramNode node, out Average value)
				{
					Average? average = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Average.CreateSafe(this._builders, node);
					if (average == null)
					{
						value = default(Average);
						return false;
					}
					value = average.Value;
					return true;
				}

				// Token: 0x0600AB77 RID: 43895 RVA: 0x00261ABC File Offset: 0x0025FCBC
				public bool arithmeticLeft_fromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_fromNumberCoalesced.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB78 RID: 43896 RVA: 0x00261AE0 File Offset: 0x0025FCE0
				public bool arithmeticLeft_fromNumberCoalesced(ProgramNode node, out arithmeticLeft_fromNumberCoalesced value)
				{
					arithmeticLeft_fromNumberCoalesced? arithmeticLeft_fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_fromNumberCoalesced.CreateSafe(this._builders, node);
					if (arithmeticLeft_fromNumberCoalesced == null)
					{
						value = default(arithmeticLeft_fromNumberCoalesced);
						return false;
					}
					value = arithmeticLeft_fromNumberCoalesced.Value;
					return true;
				}

				// Token: 0x0600AB79 RID: 43897 RVA: 0x00261B1C File Offset: 0x0025FD1C
				public bool arithmeticLeft_inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_inumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB7A RID: 43898 RVA: 0x00261B40 File Offset: 0x0025FD40
				public bool arithmeticLeft_inumber(ProgramNode node, out arithmeticLeft_inumber value)
				{
					arithmeticLeft_inumber? arithmeticLeft_inumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_inumber.CreateSafe(this._builders, node);
					if (arithmeticLeft_inumber == null)
					{
						value = default(arithmeticLeft_inumber);
						return false;
					}
					value = arithmeticLeft_inumber.Value;
					return true;
				}

				// Token: 0x0600AB7B RID: 43899 RVA: 0x00261B7C File Offset: 0x0025FD7C
				public bool addRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.addRight_arithmeticLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB7C RID: 43900 RVA: 0x00261BA0 File Offset: 0x0025FDA0
				public bool addRight_arithmeticLeft(ProgramNode node, out addRight_arithmeticLeft value)
				{
					addRight_arithmeticLeft? addRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.addRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (addRight_arithmeticLeft == null)
					{
						value = default(addRight_arithmeticLeft);
						return false;
					}
					value = addRight_arithmeticLeft.Value;
					return true;
				}

				// Token: 0x0600AB7D RID: 43901 RVA: 0x00261BDC File Offset: 0x0025FDDC
				public bool AddRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.AddRightNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB7E RID: 43902 RVA: 0x00261C00 File Offset: 0x0025FE00
				public bool AddRightNumber(ProgramNode node, out AddRightNumber value)
				{
					AddRightNumber? addRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.AddRightNumber.CreateSafe(this._builders, node);
					if (addRightNumber == null)
					{
						value = default(AddRightNumber);
						return false;
					}
					value = addRightNumber.Value;
					return true;
				}

				// Token: 0x0600AB7F RID: 43903 RVA: 0x00261C3C File Offset: 0x0025FE3C
				public bool subtractRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.subtractRight_arithmeticLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB80 RID: 43904 RVA: 0x00261C60 File Offset: 0x0025FE60
				public bool subtractRight_arithmeticLeft(ProgramNode node, out subtractRight_arithmeticLeft value)
				{
					subtractRight_arithmeticLeft? subtractRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.subtractRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (subtractRight_arithmeticLeft == null)
					{
						value = default(subtractRight_arithmeticLeft);
						return false;
					}
					value = subtractRight_arithmeticLeft.Value;
					return true;
				}

				// Token: 0x0600AB81 RID: 43905 RVA: 0x00261C9C File Offset: 0x0025FE9C
				public bool SubtractRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SubtractRightNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB82 RID: 43906 RVA: 0x00261CC0 File Offset: 0x0025FEC0
				public bool SubtractRightNumber(ProgramNode node, out SubtractRightNumber value)
				{
					SubtractRightNumber? subtractRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SubtractRightNumber.CreateSafe(this._builders, node);
					if (subtractRightNumber == null)
					{
						value = default(SubtractRightNumber);
						return false;
					}
					value = subtractRightNumber.Value;
					return true;
				}

				// Token: 0x0600AB83 RID: 43907 RVA: 0x00261CFC File Offset: 0x0025FEFC
				public bool multiplyRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.multiplyRight_arithmeticLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB84 RID: 43908 RVA: 0x00261D20 File Offset: 0x0025FF20
				public bool multiplyRight_arithmeticLeft(ProgramNode node, out multiplyRight_arithmeticLeft value)
				{
					multiplyRight_arithmeticLeft? multiplyRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.multiplyRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (multiplyRight_arithmeticLeft == null)
					{
						value = default(multiplyRight_arithmeticLeft);
						return false;
					}
					value = multiplyRight_arithmeticLeft.Value;
					return true;
				}

				// Token: 0x0600AB85 RID: 43909 RVA: 0x00261D5C File Offset: 0x0025FF5C
				public bool MultiplyRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MultiplyRightNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB86 RID: 43910 RVA: 0x00261D80 File Offset: 0x0025FF80
				public bool MultiplyRightNumber(ProgramNode node, out MultiplyRightNumber value)
				{
					MultiplyRightNumber? multiplyRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MultiplyRightNumber.CreateSafe(this._builders, node);
					if (multiplyRightNumber == null)
					{
						value = default(MultiplyRightNumber);
						return false;
					}
					value = multiplyRightNumber.Value;
					return true;
				}

				// Token: 0x0600AB87 RID: 43911 RVA: 0x00261DBC File Offset: 0x0025FFBC
				public bool divideRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.divideRight_arithmeticLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB88 RID: 43912 RVA: 0x00261DE0 File Offset: 0x0025FFE0
				public bool divideRight_arithmeticLeft(ProgramNode node, out divideRight_arithmeticLeft value)
				{
					divideRight_arithmeticLeft? divideRight_arithmeticLeft = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.divideRight_arithmeticLeft.CreateSafe(this._builders, node);
					if (divideRight_arithmeticLeft == null)
					{
						value = default(divideRight_arithmeticLeft);
						return false;
					}
					value = divideRight_arithmeticLeft.Value;
					return true;
				}

				// Token: 0x0600AB89 RID: 43913 RVA: 0x00261E1C File Offset: 0x0026001C
				public bool DivideRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DivideRightNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB8A RID: 43914 RVA: 0x00261E40 File Offset: 0x00260040
				public bool DivideRightNumber(ProgramNode node, out DivideRightNumber value)
				{
					DivideRightNumber? divideRightNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DivideRightNumber.CreateSafe(this._builders, node);
					if (divideRightNumber == null)
					{
						value = default(DivideRightNumber);
						return false;
					}
					value = divideRightNumber.Value;
					return true;
				}

				// Token: 0x0600AB8B RID: 43915 RVA: 0x00261E7C File Offset: 0x0026007C
				public bool inumber_fromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.inumber_fromNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB8C RID: 43916 RVA: 0x00261EA0 File Offset: 0x002600A0
				public bool inumber_fromNumber(ProgramNode node, out inumber_fromNumber value)
				{
					inumber_fromNumber? inumber_fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.inumber_fromNumber.CreateSafe(this._builders, node);
					if (inumber_fromNumber == null)
					{
						value = default(inumber_fromNumber);
						return false;
					}
					value = inumber_fromNumber.Value;
					return true;
				}

				// Token: 0x0600AB8D RID: 43917 RVA: 0x00261EDC File Offset: 0x002600DC
				public bool ParseNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB8E RID: 43918 RVA: 0x00261F00 File Offset: 0x00260100
				public bool ParseNumber(ProgramNode node, out ParseNumber value)
				{
					ParseNumber? parseNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
					if (parseNumber == null)
					{
						value = default(ParseNumber);
						return false;
					}
					value = parseNumber.Value;
					return true;
				}

				// Token: 0x0600AB8F RID: 43919 RVA: 0x00261F3C File Offset: 0x0026013C
				public bool rowNumberTransform_fromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.rowNumberTransform_fromRowNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB90 RID: 43920 RVA: 0x00261F60 File Offset: 0x00260160
				public bool rowNumberTransform_fromRowNumber(ProgramNode node, out rowNumberTransform_fromRowNumber value)
				{
					rowNumberTransform_fromRowNumber? rowNumberTransform_fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.rowNumberTransform_fromRowNumber.CreateSafe(this._builders, node);
					if (rowNumberTransform_fromRowNumber == null)
					{
						value = default(rowNumberTransform_fromRowNumber);
						return false;
					}
					value = rowNumberTransform_fromRowNumber.Value;
					return true;
				}

				// Token: 0x0600AB91 RID: 43921 RVA: 0x00261F9C File Offset: 0x0026019C
				public bool RowNumberLinearTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RowNumberLinearTransform.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB92 RID: 43922 RVA: 0x00261FC0 File Offset: 0x002601C0
				public bool RowNumberLinearTransform(ProgramNode node, out RowNumberLinearTransform value)
				{
					RowNumberLinearTransform? rowNumberLinearTransform = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RowNumberLinearTransform.CreateSafe(this._builders, node);
					if (rowNumberLinearTransform == null)
					{
						value = default(RowNumberLinearTransform);
						return false;
					}
					value = rowNumberLinearTransform.Value;
					return true;
				}

				// Token: 0x0600AB93 RID: 43923 RVA: 0x00261FFC File Offset: 0x002601FC
				public bool FormatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB94 RID: 43924 RVA: 0x00262020 File Offset: 0x00260220
				public bool FormatDateTime(ProgramNode node, out FormatDateTime value)
				{
					FormatDateTime? formatDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatDateTime.CreateSafe(this._builders, node);
					if (formatDateTime == null)
					{
						value = default(FormatDateTime);
						return false;
					}
					value = formatDateTime.Value;
					return true;
				}

				// Token: 0x0600AB95 RID: 43925 RVA: 0x0026205C File Offset: 0x0026025C
				public bool date_idate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.date_idate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB96 RID: 43926 RVA: 0x00262080 File Offset: 0x00260280
				public bool date_idate(ProgramNode node, out date_idate value)
				{
					date_idate? date_idate = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.date_idate.CreateSafe(this._builders, node);
					if (date_idate == null)
					{
						value = default(date_idate);
						return false;
					}
					value = date_idate.Value;
					return true;
				}

				// Token: 0x0600AB97 RID: 43927 RVA: 0x002620BC File Offset: 0x002602BC
				public bool RoundDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB98 RID: 43928 RVA: 0x002620E0 File Offset: 0x002602E0
				public bool RoundDateTime(ProgramNode node, out RoundDateTime value)
				{
					RoundDateTime? roundDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundDateTime.CreateSafe(this._builders, node);
					if (roundDateTime == null)
					{
						value = default(RoundDateTime);
						return false;
					}
					value = roundDateTime.Value;
					return true;
				}

				// Token: 0x0600AB99 RID: 43929 RVA: 0x0026211C File Offset: 0x0026031C
				public bool idate_fromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB9A RID: 43930 RVA: 0x00262140 File Offset: 0x00260340
				public bool idate_fromDateTime(ProgramNode node, out idate_fromDateTime value)
				{
					idate_fromDateTime? idate_fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTime.CreateSafe(this._builders, node);
					if (idate_fromDateTime == null)
					{
						value = default(idate_fromDateTime);
						return false;
					}
					value = idate_fromDateTime.Value;
					return true;
				}

				// Token: 0x0600AB9B RID: 43931 RVA: 0x0026217C File Offset: 0x0026037C
				public bool idate_fromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTimePart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB9C RID: 43932 RVA: 0x002621A0 File Offset: 0x002603A0
				public bool idate_fromDateTimePart(ProgramNode node, out idate_fromDateTimePart value)
				{
					idate_fromDateTimePart? idate_fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTimePart.CreateSafe(this._builders, node);
					if (idate_fromDateTimePart == null)
					{
						value = default(idate_fromDateTimePart);
						return false;
					}
					value = idate_fromDateTimePart.Value;
					return true;
				}

				// Token: 0x0600AB9D RID: 43933 RVA: 0x002621DC File Offset: 0x002603DC
				public bool ParseDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600AB9E RID: 43934 RVA: 0x00262200 File Offset: 0x00260400
				public bool ParseDateTime(ProgramNode node, out ParseDateTime value)
				{
					ParseDateTime? parseDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseDateTime.CreateSafe(this._builders, node);
					if (parseDateTime == null)
					{
						value = default(ParseDateTime);
						return false;
					}
					value = parseDateTime.Value;
					return true;
				}

				// Token: 0x0600AB9F RID: 43935 RVA: 0x0026223C File Offset: 0x0026043C
				public bool itime_fromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.itime_fromTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABA0 RID: 43936 RVA: 0x00262260 File Offset: 0x00260460
				public bool itime_fromTime(ProgramNode node, out itime_fromTime value)
				{
					itime_fromTime? itime_fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.itime_fromTime.CreateSafe(this._builders, node);
					if (itime_fromTime == null)
					{
						value = default(itime_fromTime);
						return false;
					}
					value = itime_fromTime.Value;
					return true;
				}

				// Token: 0x0600ABA1 RID: 43937 RVA: 0x0026229C File Offset: 0x0026049C
				public bool parseSubject_fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_fromStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABA2 RID: 43938 RVA: 0x002622C0 File Offset: 0x002604C0
				public bool parseSubject_fromStr(ProgramNode node, out parseSubject_fromStr value)
				{
					parseSubject_fromStr? parseSubject_fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_fromStr.CreateSafe(this._builders, node);
					if (parseSubject_fromStr == null)
					{
						value = default(parseSubject_fromStr);
						return false;
					}
					value = parseSubject_fromStr.Value;
					return true;
				}

				// Token: 0x0600ABA3 RID: 43939 RVA: 0x002622FC File Offset: 0x002604FC
				public bool parseSubject_letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_letSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABA4 RID: 43940 RVA: 0x00262320 File Offset: 0x00260520
				public bool parseSubject_letSubstring(ProgramNode node, out parseSubject_letSubstring value)
				{
					parseSubject_letSubstring? parseSubject_letSubstring = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_letSubstring.CreateSafe(this._builders, node);
					if (parseSubject_letSubstring == null)
					{
						value = default(parseSubject_letSubstring);
						return false;
					}
					value = parseSubject_letSubstring.Value;
					return true;
				}

				// Token: 0x0600ABA5 RID: 43941 RVA: 0x0026235C File Offset: 0x0026055C
				public bool LetX(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABA6 RID: 43942 RVA: 0x00262380 File Offset: 0x00260580
				public bool LetX(ProgramNode node, out LetX value)
				{
					LetX? letX = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
					if (letX == null)
					{
						value = default(LetX);
						return false;
					}
					value = letX.Value;
					return true;
				}

				// Token: 0x0600ABA7 RID: 43943 RVA: 0x002623BC File Offset: 0x002605BC
				public bool substring_splitTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_splitTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABA8 RID: 43944 RVA: 0x002623E0 File Offset: 0x002605E0
				public bool substring_splitTrim(ProgramNode node, out substring_splitTrim value)
				{
					substring_splitTrim? substring_splitTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_splitTrim.CreateSafe(this._builders, node);
					if (substring_splitTrim == null)
					{
						value = default(substring_splitTrim);
						return false;
					}
					value = substring_splitTrim.Value;
					return true;
				}

				// Token: 0x0600ABA9 RID: 43945 RVA: 0x0026241C File Offset: 0x0026061C
				public bool SlicePrefixAbs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefixAbs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABAA RID: 43946 RVA: 0x00262440 File Offset: 0x00260640
				public bool SlicePrefixAbs(ProgramNode node, out SlicePrefixAbs value)
				{
					SlicePrefixAbs? slicePrefixAbs = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefixAbs.CreateSafe(this._builders, node);
					if (slicePrefixAbs == null)
					{
						value = default(SlicePrefixAbs);
						return false;
					}
					value = slicePrefixAbs.Value;
					return true;
				}

				// Token: 0x0600ABAB RID: 43947 RVA: 0x0026247C File Offset: 0x0026067C
				public bool SlicePrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefix.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABAC RID: 43948 RVA: 0x002624A0 File Offset: 0x002606A0
				public bool SlicePrefix(ProgramNode node, out SlicePrefix value)
				{
					SlicePrefix? slicePrefix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefix.CreateSafe(this._builders, node);
					if (slicePrefix == null)
					{
						value = default(SlicePrefix);
						return false;
					}
					value = slicePrefix.Value;
					return true;
				}

				// Token: 0x0600ABAD RID: 43949 RVA: 0x002624DC File Offset: 0x002606DC
				public bool SliceSuffix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceSuffix.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABAE RID: 43950 RVA: 0x00262500 File Offset: 0x00260700
				public bool SliceSuffix(ProgramNode node, out SliceSuffix value)
				{
					SliceSuffix? sliceSuffix = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceSuffix.CreateSafe(this._builders, node);
					if (sliceSuffix == null)
					{
						value = default(SliceSuffix);
						return false;
					}
					value = sliceSuffix.Value;
					return true;
				}

				// Token: 0x0600ABAF RID: 43951 RVA: 0x0026253C File Offset: 0x0026073C
				public bool substring_sliceTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_sliceTrim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABB0 RID: 43952 RVA: 0x00262560 File Offset: 0x00260760
				public bool substring_sliceTrim(ProgramNode node, out substring_sliceTrim value)
				{
					substring_sliceTrim? substring_sliceTrim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_sliceTrim.CreateSafe(this._builders, node);
					if (substring_sliceTrim == null)
					{
						value = default(substring_sliceTrim);
						return false;
					}
					value = substring_sliceTrim.Value;
					return true;
				}

				// Token: 0x0600ABB1 RID: 43953 RVA: 0x0026259C File Offset: 0x0026079C
				public bool MatchFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchFull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABB2 RID: 43954 RVA: 0x002625C0 File Offset: 0x002607C0
				public bool MatchFull(ProgramNode node, out MatchFull value)
				{
					MatchFull? matchFull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchFull.CreateSafe(this._builders, node);
					if (matchFull == null)
					{
						value = default(MatchFull);
						return false;
					}
					value = matchFull.Value;
					return true;
				}

				// Token: 0x0600ABB3 RID: 43955 RVA: 0x002625FC File Offset: 0x002607FC
				public bool SliceBetween(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceBetween.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABB4 RID: 43956 RVA: 0x00262620 File Offset: 0x00260820
				public bool SliceBetween(ProgramNode node, out SliceBetween value)
				{
					SliceBetween? sliceBetween = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceBetween.CreateSafe(this._builders, node);
					if (sliceBetween == null)
					{
						value = default(SliceBetween);
						return false;
					}
					value = sliceBetween.Value;
					return true;
				}

				// Token: 0x0600ABB5 RID: 43957 RVA: 0x0026265C File Offset: 0x0026085C
				public bool splitTrim_split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.splitTrim_split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABB6 RID: 43958 RVA: 0x00262680 File Offset: 0x00260880
				public bool splitTrim_split(ProgramNode node, out splitTrim_split value)
				{
					splitTrim_split? splitTrim_split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.splitTrim_split.CreateSafe(this._builders, node);
					if (splitTrim_split == null)
					{
						value = default(splitTrim_split);
						return false;
					}
					value = splitTrim_split.Value;
					return true;
				}

				// Token: 0x0600ABB7 RID: 43959 RVA: 0x002626BC File Offset: 0x002608BC
				public bool TrimSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABB8 RID: 43960 RVA: 0x002626E0 File Offset: 0x002608E0
				public bool TrimSplit(ProgramNode node, out TrimSplit value)
				{
					TrimSplit? trimSplit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSplit.CreateSafe(this._builders, node);
					if (trimSplit == null)
					{
						value = default(TrimSplit);
						return false;
					}
					value = trimSplit.Value;
					return true;
				}

				// Token: 0x0600ABB9 RID: 43961 RVA: 0x0026271C File Offset: 0x0026091C
				public bool TrimFullSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABBA RID: 43962 RVA: 0x00262740 File Offset: 0x00260940
				public bool TrimFullSplit(ProgramNode node, out TrimFullSplit value)
				{
					TrimFullSplit? trimFullSplit = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSplit.CreateSafe(this._builders, node);
					if (trimFullSplit == null)
					{
						value = default(TrimFullSplit);
						return false;
					}
					value = trimFullSplit.Value;
					return true;
				}

				// Token: 0x0600ABBB RID: 43963 RVA: 0x0026277C File Offset: 0x0026097C
				public bool Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABBC RID: 43964 RVA: 0x002627A0 File Offset: 0x002609A0
				public bool Split(ProgramNode node, out Split value)
				{
					Split? split = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						value = default(Split);
						return false;
					}
					value = split.Value;
					return true;
				}

				// Token: 0x0600ABBD RID: 43965 RVA: 0x002627DC File Offset: 0x002609DC
				public bool sliceTrim_slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.sliceTrim_slice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABBE RID: 43966 RVA: 0x00262800 File Offset: 0x00260A00
				public bool sliceTrim_slice(ProgramNode node, out sliceTrim_slice value)
				{
					sliceTrim_slice? sliceTrim_slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.sliceTrim_slice.CreateSafe(this._builders, node);
					if (sliceTrim_slice == null)
					{
						value = default(sliceTrim_slice);
						return false;
					}
					value = sliceTrim_slice.Value;
					return true;
				}

				// Token: 0x0600ABBF RID: 43967 RVA: 0x0026283C File Offset: 0x00260A3C
				public bool TrimSlice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSlice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABC0 RID: 43968 RVA: 0x00262860 File Offset: 0x00260A60
				public bool TrimSlice(ProgramNode node, out TrimSlice value)
				{
					TrimSlice? trimSlice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSlice.CreateSafe(this._builders, node);
					if (trimSlice == null)
					{
						value = default(TrimSlice);
						return false;
					}
					value = trimSlice.Value;
					return true;
				}

				// Token: 0x0600ABC1 RID: 43969 RVA: 0x0026289C File Offset: 0x00260A9C
				public bool TrimFullSlice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSlice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABC2 RID: 43970 RVA: 0x002628C0 File Offset: 0x00260AC0
				public bool TrimFullSlice(ProgramNode node, out TrimFullSlice value)
				{
					TrimFullSlice? trimFullSlice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSlice.CreateSafe(this._builders, node);
					if (trimFullSlice == null)
					{
						value = default(TrimFullSlice);
						return false;
					}
					value = trimFullSlice.Value;
					return true;
				}

				// Token: 0x0600ABC3 RID: 43971 RVA: 0x002628FC File Offset: 0x00260AFC
				public bool Slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABC4 RID: 43972 RVA: 0x00262920 File Offset: 0x00260B20
				public bool Slice(ProgramNode node, out Slice value)
				{
					Slice? slice = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						value = default(Slice);
						return false;
					}
					value = slice.Value;
					return true;
				}

				// Token: 0x0600ABC5 RID: 43973 RVA: 0x0026295C File Offset: 0x00260B5C
				public bool Find(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Find.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABC6 RID: 43974 RVA: 0x00262980 File Offset: 0x00260B80
				public bool Find(ProgramNode node, out Find value)
				{
					Find? find = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Find.CreateSafe(this._builders, node);
					if (find == null)
					{
						value = default(Find);
						return false;
					}
					value = find.Value;
					return true;
				}

				// Token: 0x0600ABC7 RID: 43975 RVA: 0x002629BC File Offset: 0x00260BBC
				public bool Abs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Abs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABC8 RID: 43976 RVA: 0x002629E0 File Offset: 0x00260BE0
				public bool Abs(ProgramNode node, out Abs value)
				{
					Abs? abs = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Abs.CreateSafe(this._builders, node);
					if (abs == null)
					{
						value = default(Abs);
						return false;
					}
					value = abs.Value;
					return true;
				}

				// Token: 0x0600ABC9 RID: 43977 RVA: 0x00262A1C File Offset: 0x00260C1C
				public bool Match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABCA RID: 43978 RVA: 0x00262A40 File Offset: 0x00260C40
				public bool Match(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match value)
				{
					Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match? match = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match.CreateSafe(this._builders, node);
					if (match == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match);
						return false;
					}
					value = match.Value;
					return true;
				}

				// Token: 0x0600ABCB RID: 43979 RVA: 0x00262A7C File Offset: 0x00260C7C
				public bool MatchEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchEnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABCC RID: 43980 RVA: 0x00262AA0 File Offset: 0x00260CA0
				public bool MatchEnd(ProgramNode node, out MatchEnd value)
				{
					MatchEnd? matchEnd = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchEnd.CreateSafe(this._builders, node);
					if (matchEnd == null)
					{
						value = default(MatchEnd);
						return false;
					}
					value = matchEnd.Value;
					return true;
				}

				// Token: 0x0600ABCD RID: 43981 RVA: 0x00262ADC File Offset: 0x00260CDC
				public bool fromStrTrim_fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABCE RID: 43982 RVA: 0x00262B00 File Offset: 0x00260D00
				public bool fromStrTrim_fromStr(ProgramNode node, out fromStrTrim_fromStr value)
				{
					fromStrTrim_fromStr? fromStrTrim_fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromStr.CreateSafe(this._builders, node);
					if (fromStrTrim_fromStr == null)
					{
						value = default(fromStrTrim_fromStr);
						return false;
					}
					value = fromStrTrim_fromStr.Value;
					return true;
				}

				// Token: 0x0600ABCF RID: 43983 RVA: 0x00262B3C File Offset: 0x00260D3C
				public bool fromStrTrim_fromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromNumberStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABD0 RID: 43984 RVA: 0x00262B60 File Offset: 0x00260D60
				public bool fromStrTrim_fromNumberStr(ProgramNode node, out fromStrTrim_fromNumberStr value)
				{
					fromStrTrim_fromNumberStr? fromStrTrim_fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromNumberStr.CreateSafe(this._builders, node);
					if (fromStrTrim_fromNumberStr == null)
					{
						value = default(fromStrTrim_fromNumberStr);
						return false;
					}
					value = fromStrTrim_fromNumberStr.Value;
					return true;
				}

				// Token: 0x0600ABD1 RID: 43985 RVA: 0x00262B9C File Offset: 0x00260D9C
				public bool TrimFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABD2 RID: 43986 RVA: 0x00262BC0 File Offset: 0x00260DC0
				public bool TrimFull(ProgramNode node, out TrimFull value)
				{
					TrimFull? trimFull = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFull.CreateSafe(this._builders, node);
					if (trimFull == null)
					{
						value = default(TrimFull);
						return false;
					}
					value = trimFull.Value;
					return true;
				}

				// Token: 0x0600ABD3 RID: 43987 RVA: 0x00262BFC File Offset: 0x00260DFC
				public bool Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABD4 RID: 43988 RVA: 0x00262C20 File Offset: 0x00260E20
				public bool Trim(ProgramNode node, out Trim value)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						value = default(Trim);
						return false;
					}
					value = trim.Value;
					return true;
				}

				// Token: 0x0600ABD5 RID: 43989 RVA: 0x00262C5C File Offset: 0x00260E5C
				public bool FromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABD6 RID: 43990 RVA: 0x00262C80 File Offset: 0x00260E80
				public bool FromStr(ProgramNode node, out FromStr value)
				{
					FromStr? fromStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromStr.CreateSafe(this._builders, node);
					if (fromStr == null)
					{
						value = default(FromStr);
						return false;
					}
					value = fromStr.Value;
					return true;
				}

				// Token: 0x0600ABD7 RID: 43991 RVA: 0x00262CBC File Offset: 0x00260EBC
				public bool FromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABD8 RID: 43992 RVA: 0x00262CE0 File Offset: 0x00260EE0
				public bool FromNumberStr(ProgramNode node, out FromNumberStr value)
				{
					FromNumberStr? fromNumberStr = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberStr.CreateSafe(this._builders, node);
					if (fromNumberStr == null)
					{
						value = default(FromNumberStr);
						return false;
					}
					value = fromNumberStr.Value;
					return true;
				}

				// Token: 0x0600ABD9 RID: 43993 RVA: 0x00262D1C File Offset: 0x00260F1C
				public bool FromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABDA RID: 43994 RVA: 0x00262D40 File Offset: 0x00260F40
				public bool FromNumber(ProgramNode node, out FromNumber value)
				{
					FromNumber? fromNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumber.CreateSafe(this._builders, node);
					if (fromNumber == null)
					{
						value = default(FromNumber);
						return false;
					}
					value = fromNumber.Value;
					return true;
				}

				// Token: 0x0600ABDB RID: 43995 RVA: 0x00262D7C File Offset: 0x00260F7C
				public bool FromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberCoalesced.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABDC RID: 43996 RVA: 0x00262DA0 File Offset: 0x00260FA0
				public bool FromNumberCoalesced(ProgramNode node, out FromNumberCoalesced value)
				{
					FromNumberCoalesced? fromNumberCoalesced = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberCoalesced.CreateSafe(this._builders, node);
					if (fromNumberCoalesced == null)
					{
						value = default(FromNumberCoalesced);
						return false;
					}
					value = fromNumberCoalesced.Value;
					return true;
				}

				// Token: 0x0600ABDD RID: 43997 RVA: 0x00262DDC File Offset: 0x00260FDC
				public bool FromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromRowNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABDE RID: 43998 RVA: 0x00262E00 File Offset: 0x00261000
				public bool FromRowNumber(ProgramNode node, out FromRowNumber value)
				{
					FromRowNumber? fromRowNumber = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromRowNumber.CreateSafe(this._builders, node);
					if (fromRowNumber == null)
					{
						value = default(FromRowNumber);
						return false;
					}
					value = fromRowNumber.Value;
					return true;
				}

				// Token: 0x0600ABDF RID: 43999 RVA: 0x00262E3C File Offset: 0x0026103C
				public bool FromNumbers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumbers.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABE0 RID: 44000 RVA: 0x00262E60 File Offset: 0x00261060
				public bool FromNumbers(ProgramNode node, out FromNumbers value)
				{
					FromNumbers? fromNumbers = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumbers.CreateSafe(this._builders, node);
					if (fromNumbers == null)
					{
						value = default(FromNumbers);
						return false;
					}
					value = fromNumbers.Value;
					return true;
				}

				// Token: 0x0600ABE1 RID: 44001 RVA: 0x00262E9C File Offset: 0x0026109C
				public bool FromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABE2 RID: 44002 RVA: 0x00262EC0 File Offset: 0x002610C0
				public bool FromDateTime(ProgramNode node, out FromDateTime value)
				{
					FromDateTime? fromDateTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTime.CreateSafe(this._builders, node);
					if (fromDateTime == null)
					{
						value = default(FromDateTime);
						return false;
					}
					value = fromDateTime.Value;
					return true;
				}

				// Token: 0x0600ABE3 RID: 44003 RVA: 0x00262EFC File Offset: 0x002610FC
				public bool FromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTimePart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABE4 RID: 44004 RVA: 0x00262F20 File Offset: 0x00261120
				public bool FromDateTimePart(ProgramNode node, out FromDateTimePart value)
				{
					FromDateTimePart? fromDateTimePart = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTimePart.CreateSafe(this._builders, node);
					if (fromDateTimePart == null)
					{
						value = default(FromDateTimePart);
						return false;
					}
					value = fromDateTimePart.Value;
					return true;
				}

				// Token: 0x0600ABE5 RID: 44005 RVA: 0x00262F5C File Offset: 0x0026115C
				public bool FromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABE6 RID: 44006 RVA: 0x00262F80 File Offset: 0x00261180
				public bool FromTime(ProgramNode node, out FromTime value)
				{
					FromTime? fromTime = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromTime.CreateSafe(this._builders, node);
					if (fromTime == null)
					{
						value = default(FromTime);
						return false;
					}
					value = fromTime.Value;
					return true;
				}

				// Token: 0x0600ABE7 RID: 44007 RVA: 0x00262FBC File Offset: 0x002611BC
				public bool Str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Str.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABE8 RID: 44008 RVA: 0x00262FE0 File Offset: 0x002611E0
				public bool Str(ProgramNode node, out Str value)
				{
					Str? str = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Str.CreateSafe(this._builders, node);
					if (str == null)
					{
						value = default(Str);
						return false;
					}
					value = str.Value;
					return true;
				}

				// Token: 0x0600ABE9 RID: 44009 RVA: 0x0026301C File Offset: 0x0026121C
				public bool Number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Number.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABEA RID: 44010 RVA: 0x00263040 File Offset: 0x00261240
				public bool Number(ProgramNode node, out Number value)
				{
					Number? number = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Number.CreateSafe(this._builders, node);
					if (number == null)
					{
						value = default(Number);
						return false;
					}
					value = number.Value;
					return true;
				}

				// Token: 0x0600ABEB RID: 44011 RVA: 0x0026307C File Offset: 0x0026127C
				public bool Date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Date.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ABEC RID: 44012 RVA: 0x002630A0 File Offset: 0x002612A0
				public bool Date(ProgramNode node, out Date value)
				{
					Date? date = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Date.CreateSafe(this._builders, node);
					if (date == null)
					{
						value = default(Date);
						return false;
					}
					value = date.Value;
					return true;
				}

				// Token: 0x040044E4 RID: 17636
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001507 RID: 5383
			public class NodeAs
			{
				// Token: 0x0600ABED RID: 44013 RVA: 0x002630DA File Offset: 0x002612DA
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ABEE RID: 44014 RVA: 0x002630E9 File Offset: 0x002612E9
				public result? result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABEF RID: 44015 RVA: 0x002630F7 File Offset: 0x002612F7
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF0 RID: 44016 RVA: 0x00263105 File Offset: 0x00261305
				public outNumber? outNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF1 RID: 44017 RVA: 0x00263113 File Offset: 0x00261313
				public outDate? outDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF2 RID: 44018 RVA: 0x00263121 File Offset: 0x00261321
				public outStr? outStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF3 RID: 44019 RVA: 0x0026312F File Offset: 0x0026132F
				public outStr1? outStr1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF4 RID: 44020 RVA: 0x0026313D File Offset: 0x0026133D
				public segmentCase? segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF5 RID: 44021 RVA: 0x0026314B File Offset: 0x0026134B
				public segment? segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF6 RID: 44022 RVA: 0x00263159 File Offset: 0x00261359
				public formatted? formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF7 RID: 44023 RVA: 0x00263167 File Offset: 0x00261367
				public concatEntry? concatEntry(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF8 RID: 44024 RVA: 0x00263175 File Offset: 0x00261375
				public concatCase? concatCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABF9 RID: 44025 RVA: 0x00263183 File Offset: 0x00261383
				public concat? concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFA RID: 44026 RVA: 0x00263191 File Offset: 0x00261391
				public concatPrefix? concatPrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFB RID: 44027 RVA: 0x0026319F File Offset: 0x0026139F
				public concatSegment? concatSegment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFC RID: 44028 RVA: 0x002631AD File Offset: 0x002613AD
				public concatSuffix? concatSuffix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFD RID: 44029 RVA: 0x002631BB File Offset: 0x002613BB
				public condition? condition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFE RID: 44030 RVA: 0x002631C9 File Offset: 0x002613C9
				public or? or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ABFF RID: 44031 RVA: 0x002631D7 File Offset: 0x002613D7
				public inull? inull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC00 RID: 44032 RVA: 0x002631E5 File Offset: 0x002613E5
				public equalsText? equalsText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC01 RID: 44033 RVA: 0x002631F3 File Offset: 0x002613F3
				public containsFindText? containsFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC02 RID: 44034 RVA: 0x00263201 File Offset: 0x00261401
				public startsWithFindText? startsWithFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC03 RID: 44035 RVA: 0x0026320F File Offset: 0x0026140F
				public isMatchRegex? isMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC04 RID: 44036 RVA: 0x0026321D File Offset: 0x0026141D
				public containsMatchRegex? containsMatchRegex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC05 RID: 44037 RVA: 0x0026322B File Offset: 0x0026142B
				public containsCount? containsCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC06 RID: 44038 RVA: 0x00263239 File Offset: 0x00261439
				public matchCount? matchCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC07 RID: 44039 RVA: 0x00263247 File Offset: 0x00261447
				public numberEqualsValue? numberEqualsValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC08 RID: 44040 RVA: 0x00263255 File Offset: 0x00261455
				public numberGreaterThanValue? numberGreaterThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC09 RID: 44041 RVA: 0x00263263 File Offset: 0x00261463
				public numberLessThanValue? numberLessThanValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0A RID: 44042 RVA: 0x00263271 File Offset: 0x00261471
				public formatNumber? formatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0B RID: 44043 RVA: 0x0026327F File Offset: 0x0026147F
				public number? number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0C RID: 44044 RVA: 0x0026328D File Offset: 0x0026148D
				public number1? number1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0D RID: 44045 RVA: 0x0026329B File Offset: 0x0026149B
				public arithmetic? arithmetic(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0E RID: 44046 RVA: 0x002632A9 File Offset: 0x002614A9
				public arithmeticLeft? arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC0F RID: 44047 RVA: 0x002632B7 File Offset: 0x002614B7
				public addRight? addRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC10 RID: 44048 RVA: 0x002632C5 File Offset: 0x002614C5
				public subtractRight? subtractRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC11 RID: 44049 RVA: 0x002632D3 File Offset: 0x002614D3
				public multiplyRight? multiplyRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC12 RID: 44050 RVA: 0x002632E1 File Offset: 0x002614E1
				public divideRight? divideRight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC13 RID: 44051 RVA: 0x002632EF File Offset: 0x002614EF
				public inumber? inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC14 RID: 44052 RVA: 0x002632FD File Offset: 0x002614FD
				public rowNumberTransform? rowNumberTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC15 RID: 44053 RVA: 0x0026330B File Offset: 0x0026150B
				public formatDateTime? formatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC16 RID: 44054 RVA: 0x00263319 File Offset: 0x00261519
				public date? date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC17 RID: 44055 RVA: 0x00263327 File Offset: 0x00261527
				public idate? idate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC18 RID: 44056 RVA: 0x00263335 File Offset: 0x00261535
				public itime? itime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC19 RID: 44057 RVA: 0x00263343 File Offset: 0x00261543
				public parseSubject? parseSubject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1A RID: 44058 RVA: 0x00263351 File Offset: 0x00261551
				public letSubstring? letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1B RID: 44059 RVA: 0x0026335F File Offset: 0x0026155F
				public x? x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1C RID: 44060 RVA: 0x0026336D File Offset: 0x0026156D
				public substring? substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1D RID: 44061 RVA: 0x0026337B File Offset: 0x0026157B
				public splitTrim? splitTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1E RID: 44062 RVA: 0x00263389 File Offset: 0x00261589
				public split? split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC1F RID: 44063 RVA: 0x00263397 File Offset: 0x00261597
				public sliceTrim? sliceTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC20 RID: 44064 RVA: 0x002633A5 File Offset: 0x002615A5
				public slice? slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC21 RID: 44065 RVA: 0x002633B3 File Offset: 0x002615B3
				public pos? pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC22 RID: 44066 RVA: 0x002633C1 File Offset: 0x002615C1
				public fromStrTrim? fromStrTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC23 RID: 44067 RVA: 0x002633CF File Offset: 0x002615CF
				public fromStr? fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC24 RID: 44068 RVA: 0x002633DD File Offset: 0x002615DD
				public fromNumberStr? fromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC25 RID: 44069 RVA: 0x002633EB File Offset: 0x002615EB
				public fromNumber? fromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC26 RID: 44070 RVA: 0x002633F9 File Offset: 0x002615F9
				public fromNumberCoalesced? fromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC27 RID: 44071 RVA: 0x00263407 File Offset: 0x00261607
				public fromRowNumber? fromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC28 RID: 44072 RVA: 0x00263415 File Offset: 0x00261615
				public fromNumbers? fromNumbers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC29 RID: 44073 RVA: 0x00263423 File Offset: 0x00261623
				public fromDateTime? fromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2A RID: 44074 RVA: 0x00263431 File Offset: 0x00261631
				public fromDateTimePart? fromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2B RID: 44075 RVA: 0x0026343F File Offset: 0x0026163F
				public fromTime? fromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2C RID: 44076 RVA: 0x0026344D File Offset: 0x0026164D
				public constString? constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2D RID: 44077 RVA: 0x0026345B File Offset: 0x0026165B
				public constNumber? constNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2E RID: 44078 RVA: 0x00263469 File Offset: 0x00261669
				public constDate? constDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC2F RID: 44079 RVA: 0x00263477 File Offset: 0x00261677
				public columnName? columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC30 RID: 44080 RVA: 0x00263485 File Offset: 0x00261685
				public columnNames? columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC31 RID: 44081 RVA: 0x00263493 File Offset: 0x00261693
				public constStr? constStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC32 RID: 44082 RVA: 0x002634A1 File Offset: 0x002616A1
				public constNum? constNum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC33 RID: 44083 RVA: 0x002634AF File Offset: 0x002616AF
				public constDt? constDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC34 RID: 44084 RVA: 0x002634BD File Offset: 0x002616BD
				public locale? locale(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC35 RID: 44085 RVA: 0x002634CB File Offset: 0x002616CB
				public replaceFindText? replaceFindText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC36 RID: 44086 RVA: 0x002634D9 File Offset: 0x002616D9
				public replaceText? replaceText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC37 RID: 44087 RVA: 0x002634E7 File Offset: 0x002616E7
				public sliceBetweenStartText? sliceBetweenStartText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC38 RID: 44088 RVA: 0x002634F5 File Offset: 0x002616F5
				public sliceBetweenEndText? sliceBetweenEndText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC39 RID: 44089 RVA: 0x00263503 File Offset: 0x00261703
				public numberFormatDesc? numberFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3A RID: 44090 RVA: 0x00263511 File Offset: 0x00261711
				public numberRoundDesc? numberRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3B RID: 44091 RVA: 0x0026351F File Offset: 0x0026171F
				public dateTimeRoundDesc? dateTimeRoundDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3C RID: 44092 RVA: 0x0026352D File Offset: 0x0026172D
				public dateTimeFormatDesc? dateTimeFormatDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3D RID: 44093 RVA: 0x0026353B File Offset: 0x0026173B
				public dateTimeParseDesc? dateTimeParseDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3E RID: 44094 RVA: 0x00263549 File Offset: 0x00261749
				public dateTimePartKind? dateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC3F RID: 44095 RVA: 0x00263557 File Offset: 0x00261757
				public fromDateTimePartKind? fromDateTimePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC40 RID: 44096 RVA: 0x00263565 File Offset: 0x00261765
				public timePartKind? timePartKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC41 RID: 44097 RVA: 0x00263573 File Offset: 0x00261773
				public rowNumberLinearTransformDesc? rowNumberLinearTransformDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC42 RID: 44098 RVA: 0x00263581 File Offset: 0x00261781
				public matchDesc? matchDesc(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC43 RID: 44099 RVA: 0x0026358F File Offset: 0x0026178F
				public matchInstance? matchInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC44 RID: 44100 RVA: 0x0026359D File Offset: 0x0026179D
				public splitDelimiter? splitDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC45 RID: 44101 RVA: 0x002635AB File Offset: 0x002617AB
				public splitInstance? splitInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC46 RID: 44102 RVA: 0x002635B9 File Offset: 0x002617B9
				public findDelimiter? findDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC47 RID: 44103 RVA: 0x002635C7 File Offset: 0x002617C7
				public findInstance? findInstance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC48 RID: 44104 RVA: 0x002635D5 File Offset: 0x002617D5
				public findOffset? findOffset(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC49 RID: 44105 RVA: 0x002635E3 File Offset: 0x002617E3
				public slicePrefixAbsPos? slicePrefixAbsPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC4A RID: 44106 RVA: 0x002635F1 File Offset: 0x002617F1
				public scaleNumberFactor? scaleNumberFactor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC4B RID: 44107 RVA: 0x002635FF File Offset: 0x002617FF
				public absPos? absPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos.CreateSafe(this._builders, node);
				}

				// Token: 0x040044E5 RID: 17637
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001508 RID: 5384
			public class RuleAs
			{
				// Token: 0x0600AC4C RID: 44108 RVA: 0x0026360D File Offset: 0x0026180D
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600AC4D RID: 44109 RVA: 0x0026361C File Offset: 0x0026181C
				public result_output? result_output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_output.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC4E RID: 44110 RVA: 0x0026362A File Offset: 0x0026182A
				public result_inull? result_inull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.result_inull.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC4F RID: 44111 RVA: 0x00263638 File Offset: 0x00261838
				public If? If(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.If.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC50 RID: 44112 RVA: 0x00263646 File Offset: 0x00261846
				public ToInt? ToInt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToInt.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC51 RID: 44113 RVA: 0x00263654 File Offset: 0x00261854
				public ToDouble? ToDouble(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDouble.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC52 RID: 44114 RVA: 0x00263662 File Offset: 0x00261862
				public ToDecimal? ToDecimal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDecimal.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC53 RID: 44115 RVA: 0x00263670 File Offset: 0x00261870
				public ToDateTime? ToDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC54 RID: 44116 RVA: 0x0026367E File Offset: 0x0026187E
				public ToStr? ToStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ToStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC55 RID: 44117 RVA: 0x0026368C File Offset: 0x0026188C
				public outNumber_number? outNumber_number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_number.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC56 RID: 44118 RVA: 0x0026369A File Offset: 0x0026189A
				public outNumber_constNumber? outNumber_constNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outNumber_constNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC57 RID: 44119 RVA: 0x002636A8 File Offset: 0x002618A8
				public outDate_date? outDate_date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_date.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC58 RID: 44120 RVA: 0x002636B6 File Offset: 0x002618B6
				public outDate_constDate? outDate_constDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outDate_constDate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC59 RID: 44121 RVA: 0x002636C4 File Offset: 0x002618C4
				public outStr_outStr1? outStr_outStr1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr_outStr1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5A RID: 44122 RVA: 0x002636D2 File Offset: 0x002618D2
				public Replace? Replace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Replace.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5B RID: 44123 RVA: 0x002636E0 File Offset: 0x002618E0
				public outStr1_segmentCase? outStr1_segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_segmentCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5C RID: 44124 RVA: 0x002636EE File Offset: 0x002618EE
				public outStr1_formatted? outStr1_formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_formatted.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5D RID: 44125 RVA: 0x002636FC File Offset: 0x002618FC
				public outStr1_concatEntry? outStr1_concatEntry(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_concatEntry.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5E RID: 44126 RVA: 0x0026370A File Offset: 0x0026190A
				public outStr1_constString? outStr1_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.outStr1_constString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC5F RID: 44127 RVA: 0x00263718 File Offset: 0x00261918
				public segmentCase_segment? segmentCase_segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segmentCase_segment.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC60 RID: 44128 RVA: 0x00263726 File Offset: 0x00261926
				public LowerCase? LowerCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC61 RID: 44129 RVA: 0x00263734 File Offset: 0x00261934
				public UpperCase? UpperCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC62 RID: 44130 RVA: 0x00263742 File Offset: 0x00261942
				public ProperCase? ProperCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC63 RID: 44131 RVA: 0x00263750 File Offset: 0x00261950
				public segment_fromStrTrim? segment_fromStrTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_fromStrTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC64 RID: 44132 RVA: 0x0026375E File Offset: 0x0026195E
				public segment_letSubstring? segment_letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.segment_letSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC65 RID: 44133 RVA: 0x0026376C File Offset: 0x0026196C
				public formatted_formatNumber? formatted_formatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC66 RID: 44134 RVA: 0x0026377A File Offset: 0x0026197A
				public formatted_formatDateTime? formatted_formatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.formatted_formatDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC67 RID: 44135 RVA: 0x00263788 File Offset: 0x00261988
				public concatEntry_concatCase? concatEntry_concatCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_concatCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC68 RID: 44136 RVA: 0x00263796 File Offset: 0x00261996
				public concatEntry_constString? concatEntry_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatEntry_constString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC69 RID: 44137 RVA: 0x002637A4 File Offset: 0x002619A4
				public concatCase_concat? concatCase_concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatCase_concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6A RID: 44138 RVA: 0x002637B2 File Offset: 0x002619B2
				public LowerCaseConcat? LowerCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LowerCaseConcat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6B RID: 44139 RVA: 0x002637C0 File Offset: 0x002619C0
				public UpperCaseConcat? UpperCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.UpperCaseConcat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6C RID: 44140 RVA: 0x002637CE File Offset: 0x002619CE
				public ProperCaseConcat? ProperCaseConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ProperCaseConcat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6D RID: 44141 RVA: 0x002637DC File Offset: 0x002619DC
				public Concat? Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6E RID: 44142 RVA: 0x002637EA File Offset: 0x002619EA
				public concatPrefix_concatSegment? concatPrefix_concatSegment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_concatSegment.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC6F RID: 44143 RVA: 0x002637F8 File Offset: 0x002619F8
				public concatPrefix_formatted? concatPrefix_formatted(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_formatted.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC70 RID: 44144 RVA: 0x00263806 File Offset: 0x00261A06
				public concatPrefix_constString? concatPrefix_constString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatPrefix_constString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC71 RID: 44145 RVA: 0x00263814 File Offset: 0x00261A14
				public concatSegment_segment? concatSegment_segment(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segment.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC72 RID: 44146 RVA: 0x00263822 File Offset: 0x00261A22
				public concatSegment_segmentCase? concatSegment_segmentCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSegment_segmentCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC73 RID: 44147 RVA: 0x00263830 File Offset: 0x00261A30
				public concatSuffix_concatPrefix? concatSuffix_concatPrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concatPrefix.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC74 RID: 44148 RVA: 0x0026383E File Offset: 0x00261A3E
				public concatSuffix_concat? concatSuffix_concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.concatSuffix_concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC75 RID: 44149 RVA: 0x0026384C File Offset: 0x00261A4C
				public StringEquals? StringEquals(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StringEquals.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC76 RID: 44150 RVA: 0x0026385A File Offset: 0x00261A5A
				public Contains? Contains(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC77 RID: 44151 RVA: 0x00263868 File Offset: 0x00261A68
				public StartsWithDigit? StartsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC78 RID: 44152 RVA: 0x00263876 File Offset: 0x00261A76
				public EndsWithDigit? EndsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC79 RID: 44153 RVA: 0x00263884 File Offset: 0x00261A84
				public StartsWith? StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7A RID: 44154 RVA: 0x00263892 File Offset: 0x00261A92
				public IsBlank? IsBlank(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsBlank.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7B RID: 44155 RVA: 0x002638A0 File Offset: 0x00261AA0
				public IsNotBlank? IsNotBlank(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNotBlank.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7C RID: 44156 RVA: 0x002638AE File Offset: 0x00261AAE
				public NumberEquals? NumberEquals(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberEquals.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7D RID: 44157 RVA: 0x002638BC File Offset: 0x00261ABC
				public NumberGreaterThan? NumberGreaterThan(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberGreaterThan.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7E RID: 44158 RVA: 0x002638CA File Offset: 0x00261ACA
				public NumberLessThan? NumberLessThan(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.NumberLessThan.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC7F RID: 44159 RVA: 0x002638D8 File Offset: 0x00261AD8
				public IsString? IsString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC80 RID: 44160 RVA: 0x002638E6 File Offset: 0x00261AE6
				public IsNumber? IsNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC81 RID: 44161 RVA: 0x002638F4 File Offset: 0x00261AF4
				public IsMatch? IsMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC82 RID: 44162 RVA: 0x00263902 File Offset: 0x00261B02
				public ContainsMatch? ContainsMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ContainsMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC83 RID: 44163 RVA: 0x00263910 File Offset: 0x00261B10
				public condition_or? condition_or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.condition_or.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC84 RID: 44164 RVA: 0x0026391E File Offset: 0x00261B1E
				public Or? Or(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Or.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC85 RID: 44165 RVA: 0x0026392C File Offset: 0x00261B2C
				public Null? Null(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Null.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC86 RID: 44166 RVA: 0x0026393A File Offset: 0x00261B3A
				public FormatNumber? FormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC87 RID: 44167 RVA: 0x00263948 File Offset: 0x00261B48
				public number_number1? number_number1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_number1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC88 RID: 44168 RVA: 0x00263956 File Offset: 0x00261B56
				public number_arithmetic? number_arithmetic(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_arithmetic.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC89 RID: 44169 RVA: 0x00263964 File Offset: 0x00261B64
				public number_rowNumberTransform? number_rowNumberTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number_rowNumberTransform.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8A RID: 44170 RVA: 0x00263972 File Offset: 0x00261B72
				public Length? Length(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Length.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8B RID: 44171 RVA: 0x00263980 File Offset: 0x00261B80
				public number1_inumber? number1_inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.number1_inumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8C RID: 44172 RVA: 0x0026398E File Offset: 0x00261B8E
				public DateTimePart? DateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DateTimePart.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8D RID: 44173 RVA: 0x0026399C File Offset: 0x00261B9C
				public TimePart? TimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TimePart.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8E RID: 44174 RVA: 0x002639AA File Offset: 0x00261BAA
				public RoundNumber? RoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC8F RID: 44175 RVA: 0x002639B8 File Offset: 0x00261BB8
				public Add? Add(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC90 RID: 44176 RVA: 0x002639C6 File Offset: 0x00261BC6
				public Subtract? Subtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Subtract.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC91 RID: 44177 RVA: 0x002639D4 File Offset: 0x00261BD4
				public Multiply? Multiply(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Multiply.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC92 RID: 44178 RVA: 0x002639E2 File Offset: 0x00261BE2
				public Divide? Divide(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Divide.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC93 RID: 44179 RVA: 0x002639F0 File Offset: 0x00261BF0
				public Sum? Sum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Sum.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC94 RID: 44180 RVA: 0x002639FE File Offset: 0x00261BFE
				public Product? Product(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Product.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC95 RID: 44181 RVA: 0x00263A0C File Offset: 0x00261C0C
				public Average? Average(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Average.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC96 RID: 44182 RVA: 0x00263A1A File Offset: 0x00261C1A
				public arithmeticLeft_fromNumberCoalesced? arithmeticLeft_fromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_fromNumberCoalesced.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC97 RID: 44183 RVA: 0x00263A28 File Offset: 0x00261C28
				public arithmeticLeft_inumber? arithmeticLeft_inumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.arithmeticLeft_inumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC98 RID: 44184 RVA: 0x00263A36 File Offset: 0x00261C36
				public addRight_arithmeticLeft? addRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.addRight_arithmeticLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC99 RID: 44185 RVA: 0x00263A44 File Offset: 0x00261C44
				public AddRightNumber? AddRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.AddRightNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9A RID: 44186 RVA: 0x00263A52 File Offset: 0x00261C52
				public subtractRight_arithmeticLeft? subtractRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.subtractRight_arithmeticLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9B RID: 44187 RVA: 0x00263A60 File Offset: 0x00261C60
				public SubtractRightNumber? SubtractRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SubtractRightNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9C RID: 44188 RVA: 0x00263A6E File Offset: 0x00261C6E
				public multiplyRight_arithmeticLeft? multiplyRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.multiplyRight_arithmeticLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9D RID: 44189 RVA: 0x00263A7C File Offset: 0x00261C7C
				public MultiplyRightNumber? MultiplyRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MultiplyRightNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9E RID: 44190 RVA: 0x00263A8A File Offset: 0x00261C8A
				public divideRight_arithmeticLeft? divideRight_arithmeticLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.divideRight_arithmeticLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x0600AC9F RID: 44191 RVA: 0x00263A98 File Offset: 0x00261C98
				public DivideRightNumber? DivideRightNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.DivideRightNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA0 RID: 44192 RVA: 0x00263AA6 File Offset: 0x00261CA6
				public inumber_fromNumber? inumber_fromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.inumber_fromNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA1 RID: 44193 RVA: 0x00263AB4 File Offset: 0x00261CB4
				public ParseNumber? ParseNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA2 RID: 44194 RVA: 0x00263AC2 File Offset: 0x00261CC2
				public rowNumberTransform_fromRowNumber? rowNumberTransform_fromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.rowNumberTransform_fromRowNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA3 RID: 44195 RVA: 0x00263AD0 File Offset: 0x00261CD0
				public RowNumberLinearTransform? RowNumberLinearTransform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RowNumberLinearTransform.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA4 RID: 44196 RVA: 0x00263ADE File Offset: 0x00261CDE
				public FormatDateTime? FormatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FormatDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA5 RID: 44197 RVA: 0x00263AEC File Offset: 0x00261CEC
				public date_idate? date_idate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.date_idate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA6 RID: 44198 RVA: 0x00263AFA File Offset: 0x00261CFA
				public RoundDateTime? RoundDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.RoundDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA7 RID: 44199 RVA: 0x00263B08 File Offset: 0x00261D08
				public idate_fromDateTime? idate_fromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA8 RID: 44200 RVA: 0x00263B16 File Offset: 0x00261D16
				public idate_fromDateTimePart? idate_fromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.idate_fromDateTimePart.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACA9 RID: 44201 RVA: 0x00263B24 File Offset: 0x00261D24
				public ParseDateTime? ParseDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.ParseDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAA RID: 44202 RVA: 0x00263B32 File Offset: 0x00261D32
				public itime_fromTime? itime_fromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.itime_fromTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAB RID: 44203 RVA: 0x00263B40 File Offset: 0x00261D40
				public parseSubject_fromStr? parseSubject_fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_fromStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAC RID: 44204 RVA: 0x00263B4E File Offset: 0x00261D4E
				public parseSubject_letSubstring? parseSubject_letSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.parseSubject_letSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAD RID: 44205 RVA: 0x00263B5C File Offset: 0x00261D5C
				public LetX? LetX(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAE RID: 44206 RVA: 0x00263B6A File Offset: 0x00261D6A
				public substring_splitTrim? substring_splitTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_splitTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACAF RID: 44207 RVA: 0x00263B78 File Offset: 0x00261D78
				public SlicePrefixAbs? SlicePrefixAbs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefixAbs.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB0 RID: 44208 RVA: 0x00263B86 File Offset: 0x00261D86
				public SlicePrefix? SlicePrefix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SlicePrefix.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB1 RID: 44209 RVA: 0x00263B94 File Offset: 0x00261D94
				public SliceSuffix? SliceSuffix(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceSuffix.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB2 RID: 44210 RVA: 0x00263BA2 File Offset: 0x00261DA2
				public substring_sliceTrim? substring_sliceTrim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.substring_sliceTrim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB3 RID: 44211 RVA: 0x00263BB0 File Offset: 0x00261DB0
				public MatchFull? MatchFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchFull.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB4 RID: 44212 RVA: 0x00263BBE File Offset: 0x00261DBE
				public SliceBetween? SliceBetween(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.SliceBetween.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB5 RID: 44213 RVA: 0x00263BCC File Offset: 0x00261DCC
				public splitTrim_split? splitTrim_split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.splitTrim_split.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB6 RID: 44214 RVA: 0x00263BDA File Offset: 0x00261DDA
				public TrimSplit? TrimSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB7 RID: 44215 RVA: 0x00263BE8 File Offset: 0x00261DE8
				public TrimFullSplit? TrimFullSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB8 RID: 44216 RVA: 0x00263BF6 File Offset: 0x00261DF6
				public Split? Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACB9 RID: 44217 RVA: 0x00263C04 File Offset: 0x00261E04
				public sliceTrim_slice? sliceTrim_slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.sliceTrim_slice.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBA RID: 44218 RVA: 0x00263C12 File Offset: 0x00261E12
				public TrimSlice? TrimSlice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimSlice.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBB RID: 44219 RVA: 0x00263C20 File Offset: 0x00261E20
				public TrimFullSlice? TrimFullSlice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFullSlice.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBC RID: 44220 RVA: 0x00263C2E File Offset: 0x00261E2E
				public Slice? Slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBD RID: 44221 RVA: 0x00263C3C File Offset: 0x00261E3C
				public Find? Find(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Find.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBE RID: 44222 RVA: 0x00263C4A File Offset: 0x00261E4A
				public Abs? Abs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Abs.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACBF RID: 44223 RVA: 0x00263C58 File Offset: 0x00261E58
				public Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match? Match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC0 RID: 44224 RVA: 0x00263C66 File Offset: 0x00261E66
				public MatchEnd? MatchEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.MatchEnd.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC1 RID: 44225 RVA: 0x00263C74 File Offset: 0x00261E74
				public fromStrTrim_fromStr? fromStrTrim_fromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC2 RID: 44226 RVA: 0x00263C82 File Offset: 0x00261E82
				public fromStrTrim_fromNumberStr? fromStrTrim_fromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes.fromStrTrim_fromNumberStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC3 RID: 44227 RVA: 0x00263C90 File Offset: 0x00261E90
				public TrimFull? TrimFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.TrimFull.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC4 RID: 44228 RVA: 0x00263C9E File Offset: 0x00261E9E
				public Trim? Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC5 RID: 44229 RVA: 0x00263CAC File Offset: 0x00261EAC
				public FromStr? FromStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC6 RID: 44230 RVA: 0x00263CBA File Offset: 0x00261EBA
				public FromNumberStr? FromNumberStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC7 RID: 44231 RVA: 0x00263CC8 File Offset: 0x00261EC8
				public FromNumber? FromNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC8 RID: 44232 RVA: 0x00263CD6 File Offset: 0x00261ED6
				public FromNumberCoalesced? FromNumberCoalesced(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumberCoalesced.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACC9 RID: 44233 RVA: 0x00263CE4 File Offset: 0x00261EE4
				public FromRowNumber? FromRowNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromRowNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCA RID: 44234 RVA: 0x00263CF2 File Offset: 0x00261EF2
				public FromNumbers? FromNumbers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromNumbers.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCB RID: 44235 RVA: 0x00263D00 File Offset: 0x00261F00
				public FromDateTime? FromDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCC RID: 44236 RVA: 0x00263D0E File Offset: 0x00261F0E
				public FromDateTimePart? FromDateTimePart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromDateTimePart.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCD RID: 44237 RVA: 0x00263D1C File Offset: 0x00261F1C
				public FromTime? FromTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.FromTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCE RID: 44238 RVA: 0x00263D2A File Offset: 0x00261F2A
				public Str? Str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Str.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACCF RID: 44239 RVA: 0x00263D38 File Offset: 0x00261F38
				public Number? Number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Number.CreateSafe(this._builders, node);
				}

				// Token: 0x0600ACD0 RID: 44240 RVA: 0x00263D46 File Offset: 0x00261F46
				public Date? Date(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Date.CreateSafe(this._builders, node);
				}

				// Token: 0x040044E6 RID: 17638
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x0200150A RID: 5386
		public class Sets
		{
			// Token: 0x0600ACD4 RID: 44244 RVA: 0x00263D70 File Offset: 0x00261F70
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17001E6B RID: 7787
			// (get) Token: 0x0600ACD5 RID: 44245 RVA: 0x00263DBF File Offset: 0x00261FBF
			// (set) Token: 0x0600ACD6 RID: 44246 RVA: 0x00263DC7 File Offset: 0x00261FC7
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17001E6C RID: 7788
			// (get) Token: 0x0600ACD7 RID: 44247 RVA: 0x00263DD0 File Offset: 0x00261FD0
			// (set) Token: 0x0600ACD8 RID: 44248 RVA: 0x00263DD8 File Offset: 0x00261FD8
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17001E6D RID: 7789
			// (get) Token: 0x0600ACD9 RID: 44249 RVA: 0x00263DE1 File Offset: 0x00261FE1
			// (set) Token: 0x0600ACDA RID: 44250 RVA: 0x00263DE9 File Offset: 0x00261FE9
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17001E6E RID: 7790
			// (get) Token: 0x0600ACDB RID: 44251 RVA: 0x00263DF2 File Offset: 0x00261FF2
			// (set) Token: 0x0600ACDC RID: 44252 RVA: 0x00263DFA File Offset: 0x00261FFA
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17001E6F RID: 7791
			// (get) Token: 0x0600ACDD RID: 44253 RVA: 0x00263E03 File Offset: 0x00262003
			// (set) Token: 0x0600ACDE RID: 44254 RVA: 0x00263E0B File Offset: 0x0026200B
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x0200150B RID: 5387
			public class Joins
			{
				// Token: 0x0600ACDF RID: 44255 RVA: 0x00263E14 File Offset: 0x00262014
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ACE0 RID: 44256 RVA: 0x00263E24 File Offset: 0x00262024
				public ProgramSetBuilder<result> If(ProgramSetBuilder<condition> value0, ProgramSetBuilder<result> value1, ProgramSetBuilder<result> value2)
				{
					return ProgramSetBuilder<result>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.If, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACE1 RID: 44257 RVA: 0x00263E7E File Offset: 0x0026207E
				public ProgramSetBuilder<output> ToInt(ProgramSetBuilder<outNumber> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToInt, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE2 RID: 44258 RVA: 0x00263EAF File Offset: 0x002620AF
				public ProgramSetBuilder<output> ToDouble(ProgramSetBuilder<outNumber> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToDouble, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE3 RID: 44259 RVA: 0x00263EE0 File Offset: 0x002620E0
				public ProgramSetBuilder<output> ToDecimal(ProgramSetBuilder<outNumber> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToDecimal, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE4 RID: 44260 RVA: 0x00263F11 File Offset: 0x00262111
				public ProgramSetBuilder<output> ToDateTime(ProgramSetBuilder<outDate> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE5 RID: 44261 RVA: 0x00263F42 File Offset: 0x00262142
				public ProgramSetBuilder<output> ToStr(ProgramSetBuilder<outStr> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE6 RID: 44262 RVA: 0x00263F74 File Offset: 0x00262174
				public ProgramSetBuilder<outStr> Replace(ProgramSetBuilder<fromStr> value0, ProgramSetBuilder<replaceFindText> value1, ProgramSetBuilder<replaceText> value2)
				{
					return ProgramSetBuilder<outStr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Replace, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACE7 RID: 44263 RVA: 0x00263FCE File Offset: 0x002621CE
				public ProgramSetBuilder<segmentCase> LowerCase(ProgramSetBuilder<segment> value0)
				{
					return ProgramSetBuilder<segmentCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LowerCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE8 RID: 44264 RVA: 0x00263FFF File Offset: 0x002621FF
				public ProgramSetBuilder<segmentCase> UpperCase(ProgramSetBuilder<segment> value0)
				{
					return ProgramSetBuilder<segmentCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.UpperCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACE9 RID: 44265 RVA: 0x00264030 File Offset: 0x00262230
				public ProgramSetBuilder<segmentCase> ProperCase(ProgramSetBuilder<segment> value0)
				{
					return ProgramSetBuilder<segmentCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ProperCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACEA RID: 44266 RVA: 0x00264061 File Offset: 0x00262261
				public ProgramSetBuilder<concatCase> LowerCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return ProgramSetBuilder<concatCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LowerCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACEB RID: 44267 RVA: 0x00264092 File Offset: 0x00262292
				public ProgramSetBuilder<concatCase> UpperCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return ProgramSetBuilder<concatCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.UpperCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACEC RID: 44268 RVA: 0x002640C3 File Offset: 0x002622C3
				public ProgramSetBuilder<concatCase> ProperCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return ProgramSetBuilder<concatCase>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ProperCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ACED RID: 44269 RVA: 0x002640F4 File Offset: 0x002622F4
				public ProgramSetBuilder<concat> Concat(ProgramSetBuilder<concatPrefix> value0, ProgramSetBuilder<concatSuffix> value1)
				{
					return ProgramSetBuilder<concat>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACEE RID: 44270 RVA: 0x00264134 File Offset: 0x00262334
				public ProgramSetBuilder<condition> StringEquals(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<equalsText> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StringEquals, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACEF RID: 44271 RVA: 0x00264190 File Offset: 0x00262390
				public ProgramSetBuilder<condition> Contains(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<containsFindText> value2, ProgramSetBuilder<containsCount> value3)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Contains, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600ACF0 RID: 44272 RVA: 0x002641FB File Offset: 0x002623FB
				public ProgramSetBuilder<condition> StartsWithDigit(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWithDigit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACF1 RID: 44273 RVA: 0x0026423B File Offset: 0x0026243B
				public ProgramSetBuilder<condition> EndsWithDigit(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndsWithDigit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACF2 RID: 44274 RVA: 0x0026427C File Offset: 0x0026247C
				public ProgramSetBuilder<condition> StartsWith(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<startsWithFindText> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACF3 RID: 44275 RVA: 0x002642D6 File Offset: 0x002624D6
				public ProgramSetBuilder<condition> IsBlank(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsBlank, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACF4 RID: 44276 RVA: 0x00264316 File Offset: 0x00262516
				public ProgramSetBuilder<condition> IsNotBlank(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNotBlank, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACF5 RID: 44277 RVA: 0x00264358 File Offset: 0x00262558
				public ProgramSetBuilder<condition> NumberEquals(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberEqualsValue> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NumberEquals, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACF6 RID: 44278 RVA: 0x002643B4 File Offset: 0x002625B4
				public ProgramSetBuilder<condition> NumberGreaterThan(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberGreaterThanValue> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NumberGreaterThan, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACF7 RID: 44279 RVA: 0x00264410 File Offset: 0x00262610
				public ProgramSetBuilder<condition> NumberLessThan(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberLessThanValue> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NumberLessThan, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACF8 RID: 44280 RVA: 0x0026446A File Offset: 0x0026266A
				public ProgramSetBuilder<condition> IsString(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACF9 RID: 44281 RVA: 0x002644AA File Offset: 0x002626AA
				public ProgramSetBuilder<condition> IsNumber(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACFA RID: 44282 RVA: 0x002644EC File Offset: 0x002626EC
				public ProgramSetBuilder<condition> IsMatch(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<isMatchRegex> value2)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600ACFB RID: 44283 RVA: 0x00264548 File Offset: 0x00262748
				public ProgramSetBuilder<condition> ContainsMatch(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<containsMatchRegex> value2, ProgramSetBuilder<matchCount> value3)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ContainsMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600ACFC RID: 44284 RVA: 0x002645B3 File Offset: 0x002627B3
				public ProgramSetBuilder<or> Or(ProgramSetBuilder<condition> value0, ProgramSetBuilder<condition> value1)
				{
					return ProgramSetBuilder<or>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Or, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACFD RID: 44285 RVA: 0x002645F3 File Offset: 0x002627F3
				public ProgramSetBuilder<inull> Null()
				{
					return ProgramSetBuilder<inull>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Null, Array.Empty<ProgramSet>()));
				}

				// Token: 0x0600ACFE RID: 44286 RVA: 0x00264614 File Offset: 0x00262814
				public ProgramSetBuilder<formatNumber> FormatNumber(ProgramSetBuilder<number> value0, ProgramSetBuilder<numberFormatDesc> value1)
				{
					return ProgramSetBuilder<formatNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600ACFF RID: 44287 RVA: 0x00264654 File Offset: 0x00262854
				public ProgramSetBuilder<number> Length(ProgramSetBuilder<fromStr> value0)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Length, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD00 RID: 44288 RVA: 0x00264685 File Offset: 0x00262885
				public ProgramSetBuilder<number1> DateTimePart(ProgramSetBuilder<idate> value0, ProgramSetBuilder<dateTimePartKind> value1)
				{
					return ProgramSetBuilder<number1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DateTimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD01 RID: 44289 RVA: 0x002646C5 File Offset: 0x002628C5
				public ProgramSetBuilder<number1> TimePart(ProgramSetBuilder<itime> value0, ProgramSetBuilder<timePartKind> value1)
				{
					return ProgramSetBuilder<number1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD02 RID: 44290 RVA: 0x00264705 File Offset: 0x00262905
				public ProgramSetBuilder<number1> RoundNumber(ProgramSetBuilder<inumber> value0, ProgramSetBuilder<numberRoundDesc> value1)
				{
					return ProgramSetBuilder<number1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD03 RID: 44291 RVA: 0x00264745 File Offset: 0x00262945
				public ProgramSetBuilder<arithmetic> Add(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<addRight> value1)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Add, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD04 RID: 44292 RVA: 0x00264785 File Offset: 0x00262985
				public ProgramSetBuilder<arithmetic> Subtract(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<subtractRight> value1)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Subtract, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD05 RID: 44293 RVA: 0x002647C5 File Offset: 0x002629C5
				public ProgramSetBuilder<arithmetic> Multiply(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<multiplyRight> value1)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Multiply, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD06 RID: 44294 RVA: 0x00264805 File Offset: 0x00262A05
				public ProgramSetBuilder<arithmetic> Divide(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<divideRight> value1)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Divide, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD07 RID: 44295 RVA: 0x00264845 File Offset: 0x00262A45
				public ProgramSetBuilder<arithmetic> Sum(ProgramSetBuilder<fromNumbers> value0)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Sum, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD08 RID: 44296 RVA: 0x00264876 File Offset: 0x00262A76
				public ProgramSetBuilder<arithmetic> Product(ProgramSetBuilder<fromNumbers> value0)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Product, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD09 RID: 44297 RVA: 0x002648A7 File Offset: 0x00262AA7
				public ProgramSetBuilder<arithmetic> Average(ProgramSetBuilder<fromNumbers> value0)
				{
					return ProgramSetBuilder<arithmetic>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Average, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD0A RID: 44298 RVA: 0x002648D8 File Offset: 0x00262AD8
				public ProgramSetBuilder<addRight> AddRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return ProgramSetBuilder<addRight>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AddRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD0B RID: 44299 RVA: 0x00264909 File Offset: 0x00262B09
				public ProgramSetBuilder<subtractRight> SubtractRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return ProgramSetBuilder<subtractRight>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SubtractRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD0C RID: 44300 RVA: 0x0026493A File Offset: 0x00262B3A
				public ProgramSetBuilder<multiplyRight> MultiplyRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return ProgramSetBuilder<multiplyRight>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MultiplyRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD0D RID: 44301 RVA: 0x0026496B File Offset: 0x00262B6B
				public ProgramSetBuilder<divideRight> DivideRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return ProgramSetBuilder<divideRight>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DivideRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD0E RID: 44302 RVA: 0x0026499C File Offset: 0x00262B9C
				public ProgramSetBuilder<inumber> ParseNumber(ProgramSetBuilder<parseSubject> value0, ProgramSetBuilder<locale> value1)
				{
					return ProgramSetBuilder<inumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ParseNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD0F RID: 44303 RVA: 0x002649DC File Offset: 0x00262BDC
				public ProgramSetBuilder<rowNumberTransform> RowNumberLinearTransform(ProgramSetBuilder<fromRowNumber> value0, ProgramSetBuilder<rowNumberLinearTransformDesc> value1)
				{
					return ProgramSetBuilder<rowNumberTransform>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RowNumberLinearTransform, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD10 RID: 44304 RVA: 0x00264A1C File Offset: 0x00262C1C
				public ProgramSetBuilder<formatDateTime> FormatDateTime(ProgramSetBuilder<date> value0, ProgramSetBuilder<dateTimeFormatDesc> value1)
				{
					return ProgramSetBuilder<formatDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD11 RID: 44305 RVA: 0x00264A5C File Offset: 0x00262C5C
				public ProgramSetBuilder<date> RoundDateTime(ProgramSetBuilder<idate> value0, ProgramSetBuilder<dateTimeRoundDesc> value1)
				{
					return ProgramSetBuilder<date>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RoundDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD12 RID: 44306 RVA: 0x00264A9C File Offset: 0x00262C9C
				public ProgramSetBuilder<idate> ParseDateTime(ProgramSetBuilder<parseSubject> value0, ProgramSetBuilder<dateTimeParseDesc> value1)
				{
					return ProgramSetBuilder<idate>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ParseDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD13 RID: 44307 RVA: 0x00264ADC File Offset: 0x00262CDC
				public ProgramSetBuilder<substring> SlicePrefixAbs(ProgramSetBuilder<x> value0, ProgramSetBuilder<slicePrefixAbsPos> value1)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SlicePrefixAbs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD14 RID: 44308 RVA: 0x00264B1C File Offset: 0x00262D1C
				public ProgramSetBuilder<substring> SlicePrefix(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SlicePrefix, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD15 RID: 44309 RVA: 0x00264B5C File Offset: 0x00262D5C
				public ProgramSetBuilder<substring> SliceSuffix(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SliceSuffix, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD16 RID: 44310 RVA: 0x00264B9C File Offset: 0x00262D9C
				public ProgramSetBuilder<substring> MatchFull(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MatchFull, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD17 RID: 44311 RVA: 0x00264BF8 File Offset: 0x00262DF8
				public ProgramSetBuilder<substring> SliceBetween(ProgramSetBuilder<x> value0, ProgramSetBuilder<sliceBetweenStartText> value1, ProgramSetBuilder<sliceBetweenEndText> value2)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SliceBetween, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD18 RID: 44312 RVA: 0x00264C52 File Offset: 0x00262E52
				public ProgramSetBuilder<splitTrim> TrimSplit(ProgramSetBuilder<split> value0)
				{
					return ProgramSetBuilder<splitTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD19 RID: 44313 RVA: 0x00264C83 File Offset: 0x00262E83
				public ProgramSetBuilder<splitTrim> TrimFullSplit(ProgramSetBuilder<split> value0)
				{
					return ProgramSetBuilder<splitTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimFullSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD1A RID: 44314 RVA: 0x00264CB4 File Offset: 0x00262EB4
				public ProgramSetBuilder<split> Split(ProgramSetBuilder<x> value0, ProgramSetBuilder<splitDelimiter> value1, ProgramSetBuilder<splitInstance> value2)
				{
					return ProgramSetBuilder<split>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Split, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD1B RID: 44315 RVA: 0x00264D0E File Offset: 0x00262F0E
				public ProgramSetBuilder<sliceTrim> TrimSlice(ProgramSetBuilder<slice> value0)
				{
					return ProgramSetBuilder<sliceTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimSlice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD1C RID: 44316 RVA: 0x00264D3F File Offset: 0x00262F3F
				public ProgramSetBuilder<sliceTrim> TrimFullSlice(ProgramSetBuilder<slice> value0)
				{
					return ProgramSetBuilder<sliceTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimFullSlice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD1D RID: 44317 RVA: 0x00264D70 File Offset: 0x00262F70
				public ProgramSetBuilder<slice> Slice(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1, ProgramSetBuilder<pos> value2)
				{
					return ProgramSetBuilder<slice>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Slice, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD1E RID: 44318 RVA: 0x00264DCC File Offset: 0x00262FCC
				public ProgramSetBuilder<pos> Find(ProgramSetBuilder<x> value0, ProgramSetBuilder<findDelimiter> value1, ProgramSetBuilder<findInstance> value2, ProgramSetBuilder<findOffset> value3)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Find, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600AD1F RID: 44319 RVA: 0x00264E37 File Offset: 0x00263037
				public ProgramSetBuilder<pos> Abs(ProgramSetBuilder<x> value0, ProgramSetBuilder<absPos> value1)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Abs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD20 RID: 44320 RVA: 0x00264E78 File Offset: 0x00263078
				public ProgramSetBuilder<pos> Match(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Match, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD21 RID: 44321 RVA: 0x00264ED4 File Offset: 0x002630D4
				public ProgramSetBuilder<pos> MatchEnd(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MatchEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD22 RID: 44322 RVA: 0x00264F2E File Offset: 0x0026312E
				public ProgramSetBuilder<fromStrTrim> TrimFull(ProgramSetBuilder<fromStr> value0)
				{
					return ProgramSetBuilder<fromStrTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimFull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD23 RID: 44323 RVA: 0x00264F5F File Offset: 0x0026315F
				public ProgramSetBuilder<fromStrTrim> Trim(ProgramSetBuilder<fromStr> value0)
				{
					return ProgramSetBuilder<fromStrTrim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD24 RID: 44324 RVA: 0x00264F90 File Offset: 0x00263190
				public ProgramSetBuilder<fromStr> FromStr(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromStr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD25 RID: 44325 RVA: 0x00264FD0 File Offset: 0x002631D0
				public ProgramSetBuilder<fromNumberStr> FromNumberStr(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromNumberStr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromNumberStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD26 RID: 44326 RVA: 0x00265010 File Offset: 0x00263210
				public ProgramSetBuilder<fromNumber> FromNumber(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD27 RID: 44327 RVA: 0x00265050 File Offset: 0x00263250
				public ProgramSetBuilder<fromNumberCoalesced> FromNumberCoalesced(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromNumberCoalesced>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromNumberCoalesced, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD28 RID: 44328 RVA: 0x00265090 File Offset: 0x00263290
				public ProgramSetBuilder<fromRowNumber> FromRowNumber(ProgramSetBuilder<row> value0)
				{
					return ProgramSetBuilder<fromRowNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromRowNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD29 RID: 44329 RVA: 0x002650C1 File Offset: 0x002632C1
				public ProgramSetBuilder<fromNumbers> FromNumbers(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnNames> value1)
				{
					return ProgramSetBuilder<fromNumbers>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromNumbers, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD2A RID: 44330 RVA: 0x00265101 File Offset: 0x00263301
				public ProgramSetBuilder<fromDateTime> FromDateTime(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD2B RID: 44331 RVA: 0x00265144 File Offset: 0x00263344
				public ProgramSetBuilder<fromDateTimePart> FromDateTimePart(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<fromDateTimePartKind> value2)
				{
					return ProgramSetBuilder<fromDateTimePart>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromDateTimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD2C RID: 44332 RVA: 0x0026519E File Offset: 0x0026339E
				public ProgramSetBuilder<fromTime> FromTime(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<fromTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FromTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD2D RID: 44333 RVA: 0x002651DE File Offset: 0x002633DE
				public ProgramSetBuilder<constString> Str(ProgramSetBuilder<constStr> value0)
				{
					return ProgramSetBuilder<constString>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Str, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD2E RID: 44334 RVA: 0x0026520F File Offset: 0x0026340F
				public ProgramSetBuilder<constNumber> Number(ProgramSetBuilder<constNum> value0)
				{
					return ProgramSetBuilder<constNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Number, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD2F RID: 44335 RVA: 0x00265240 File Offset: 0x00263440
				public ProgramSetBuilder<constDate> Date(ProgramSetBuilder<constDt> value0)
				{
					return ProgramSetBuilder<constDate>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Date, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD30 RID: 44336 RVA: 0x00265271 File Offset: 0x00263471
				public ProgramSetBuilder<letSubstring> LetX(ProgramSetBuilder<fromStrTrim> value0, ProgramSetBuilder<substring> value1)
				{
					return ProgramSetBuilder<letSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetX, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040044ED RID: 17645
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200150C RID: 5388
			public class ExplicitJoins
			{
				// Token: 0x0600AD31 RID: 44337 RVA: 0x002652B1 File Offset: 0x002634B1
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600AD32 RID: 44338 RVA: 0x002652C0 File Offset: 0x002634C0
				public JoinProgramSetBuilder<result> If(ProgramSetBuilder<condition> value0, ProgramSetBuilder<result> value1, ProgramSetBuilder<result> value2)
				{
					return JoinProgramSetBuilder<result>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.If, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD33 RID: 44339 RVA: 0x0026531A File Offset: 0x0026351A
				public JoinProgramSetBuilder<output> ToInt(ProgramSetBuilder<outNumber> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToInt, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD34 RID: 44340 RVA: 0x0026534B File Offset: 0x0026354B
				public JoinProgramSetBuilder<output> ToDouble(ProgramSetBuilder<outNumber> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToDouble, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD35 RID: 44341 RVA: 0x0026537C File Offset: 0x0026357C
				public JoinProgramSetBuilder<output> ToDecimal(ProgramSetBuilder<outNumber> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToDecimal, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD36 RID: 44342 RVA: 0x002653AD File Offset: 0x002635AD
				public JoinProgramSetBuilder<output> ToDateTime(ProgramSetBuilder<outDate> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD37 RID: 44343 RVA: 0x002653DE File Offset: 0x002635DE
				public JoinProgramSetBuilder<output> ToStr(ProgramSetBuilder<outStr> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD38 RID: 44344 RVA: 0x00265410 File Offset: 0x00263610
				public JoinProgramSetBuilder<outStr> Replace(ProgramSetBuilder<fromStr> value0, ProgramSetBuilder<replaceFindText> value1, ProgramSetBuilder<replaceText> value2)
				{
					return JoinProgramSetBuilder<outStr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Replace, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD39 RID: 44345 RVA: 0x0026546A File Offset: 0x0026366A
				public JoinProgramSetBuilder<segmentCase> LowerCase(ProgramSetBuilder<segment> value0)
				{
					return JoinProgramSetBuilder<segmentCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LowerCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3A RID: 44346 RVA: 0x0026549B File Offset: 0x0026369B
				public JoinProgramSetBuilder<segmentCase> UpperCase(ProgramSetBuilder<segment> value0)
				{
					return JoinProgramSetBuilder<segmentCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.UpperCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3B RID: 44347 RVA: 0x002654CC File Offset: 0x002636CC
				public JoinProgramSetBuilder<segmentCase> ProperCase(ProgramSetBuilder<segment> value0)
				{
					return JoinProgramSetBuilder<segmentCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ProperCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3C RID: 44348 RVA: 0x002654FD File Offset: 0x002636FD
				public JoinProgramSetBuilder<concatCase> LowerCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return JoinProgramSetBuilder<concatCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LowerCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3D RID: 44349 RVA: 0x0026552E File Offset: 0x0026372E
				public JoinProgramSetBuilder<concatCase> UpperCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return JoinProgramSetBuilder<concatCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.UpperCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3E RID: 44350 RVA: 0x0026555F File Offset: 0x0026375F
				public JoinProgramSetBuilder<concatCase> ProperCaseConcat(ProgramSetBuilder<concat> value0)
				{
					return JoinProgramSetBuilder<concatCase>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ProperCaseConcat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD3F RID: 44351 RVA: 0x00265590 File Offset: 0x00263790
				public JoinProgramSetBuilder<concat> Concat(ProgramSetBuilder<concatPrefix> value0, ProgramSetBuilder<concatSuffix> value1)
				{
					return JoinProgramSetBuilder<concat>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD40 RID: 44352 RVA: 0x002655D0 File Offset: 0x002637D0
				public JoinProgramSetBuilder<condition> StringEquals(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<equalsText> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StringEquals, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD41 RID: 44353 RVA: 0x0026562C File Offset: 0x0026382C
				public JoinProgramSetBuilder<condition> Contains(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<containsFindText> value2, ProgramSetBuilder<containsCount> value3)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Contains, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600AD42 RID: 44354 RVA: 0x00265697 File Offset: 0x00263897
				public JoinProgramSetBuilder<condition> StartsWithDigit(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWithDigit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD43 RID: 44355 RVA: 0x002656D7 File Offset: 0x002638D7
				public JoinProgramSetBuilder<condition> EndsWithDigit(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndsWithDigit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD44 RID: 44356 RVA: 0x00265718 File Offset: 0x00263918
				public JoinProgramSetBuilder<condition> StartsWith(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<startsWithFindText> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD45 RID: 44357 RVA: 0x00265772 File Offset: 0x00263972
				public JoinProgramSetBuilder<condition> IsBlank(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsBlank, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD46 RID: 44358 RVA: 0x002657B2 File Offset: 0x002639B2
				public JoinProgramSetBuilder<condition> IsNotBlank(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNotBlank, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD47 RID: 44359 RVA: 0x002657F4 File Offset: 0x002639F4
				public JoinProgramSetBuilder<condition> NumberEquals(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberEqualsValue> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NumberEquals, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD48 RID: 44360 RVA: 0x00265850 File Offset: 0x00263A50
				public JoinProgramSetBuilder<condition> NumberGreaterThan(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberGreaterThanValue> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NumberGreaterThan, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD49 RID: 44361 RVA: 0x002658AC File Offset: 0x00263AAC
				public JoinProgramSetBuilder<condition> NumberLessThan(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<numberLessThanValue> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NumberLessThan, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD4A RID: 44362 RVA: 0x00265906 File Offset: 0x00263B06
				public JoinProgramSetBuilder<condition> IsString(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD4B RID: 44363 RVA: 0x00265946 File Offset: 0x00263B46
				public JoinProgramSetBuilder<condition> IsNumber(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD4C RID: 44364 RVA: 0x00265988 File Offset: 0x00263B88
				public JoinProgramSetBuilder<condition> IsMatch(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<isMatchRegex> value2)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD4D RID: 44365 RVA: 0x002659E4 File Offset: 0x00263BE4
				public JoinProgramSetBuilder<condition> ContainsMatch(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<containsMatchRegex> value2, ProgramSetBuilder<matchCount> value3)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ContainsMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600AD4E RID: 44366 RVA: 0x00265A4F File Offset: 0x00263C4F
				public JoinProgramSetBuilder<or> Or(ProgramSetBuilder<condition> value0, ProgramSetBuilder<condition> value1)
				{
					return JoinProgramSetBuilder<or>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Or, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD4F RID: 44367 RVA: 0x00265A8F File Offset: 0x00263C8F
				public JoinProgramSetBuilder<inull> Null()
				{
					return JoinProgramSetBuilder<inull>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Null, Array.Empty<ProgramSet>()));
				}

				// Token: 0x0600AD50 RID: 44368 RVA: 0x00265AB0 File Offset: 0x00263CB0
				public JoinProgramSetBuilder<formatNumber> FormatNumber(ProgramSetBuilder<number> value0, ProgramSetBuilder<numberFormatDesc> value1)
				{
					return JoinProgramSetBuilder<formatNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD51 RID: 44369 RVA: 0x00265AF0 File Offset: 0x00263CF0
				public JoinProgramSetBuilder<number> Length(ProgramSetBuilder<fromStr> value0)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Length, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD52 RID: 44370 RVA: 0x00265B21 File Offset: 0x00263D21
				public JoinProgramSetBuilder<number1> DateTimePart(ProgramSetBuilder<idate> value0, ProgramSetBuilder<dateTimePartKind> value1)
				{
					return JoinProgramSetBuilder<number1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DateTimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD53 RID: 44371 RVA: 0x00265B61 File Offset: 0x00263D61
				public JoinProgramSetBuilder<number1> TimePart(ProgramSetBuilder<itime> value0, ProgramSetBuilder<timePartKind> value1)
				{
					return JoinProgramSetBuilder<number1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD54 RID: 44372 RVA: 0x00265BA1 File Offset: 0x00263DA1
				public JoinProgramSetBuilder<number1> RoundNumber(ProgramSetBuilder<inumber> value0, ProgramSetBuilder<numberRoundDesc> value1)
				{
					return JoinProgramSetBuilder<number1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD55 RID: 44373 RVA: 0x00265BE1 File Offset: 0x00263DE1
				public JoinProgramSetBuilder<arithmetic> Add(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<addRight> value1)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Add, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD56 RID: 44374 RVA: 0x00265C21 File Offset: 0x00263E21
				public JoinProgramSetBuilder<arithmetic> Subtract(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<subtractRight> value1)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Subtract, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD57 RID: 44375 RVA: 0x00265C61 File Offset: 0x00263E61
				public JoinProgramSetBuilder<arithmetic> Multiply(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<multiplyRight> value1)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Multiply, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD58 RID: 44376 RVA: 0x00265CA1 File Offset: 0x00263EA1
				public JoinProgramSetBuilder<arithmetic> Divide(ProgramSetBuilder<arithmeticLeft> value0, ProgramSetBuilder<divideRight> value1)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Divide, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD59 RID: 44377 RVA: 0x00265CE1 File Offset: 0x00263EE1
				public JoinProgramSetBuilder<arithmetic> Sum(ProgramSetBuilder<fromNumbers> value0)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Sum, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5A RID: 44378 RVA: 0x00265D12 File Offset: 0x00263F12
				public JoinProgramSetBuilder<arithmetic> Product(ProgramSetBuilder<fromNumbers> value0)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Product, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5B RID: 44379 RVA: 0x00265D43 File Offset: 0x00263F43
				public JoinProgramSetBuilder<arithmetic> Average(ProgramSetBuilder<fromNumbers> value0)
				{
					return JoinProgramSetBuilder<arithmetic>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Average, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5C RID: 44380 RVA: 0x00265D74 File Offset: 0x00263F74
				public JoinProgramSetBuilder<addRight> AddRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return JoinProgramSetBuilder<addRight>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AddRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5D RID: 44381 RVA: 0x00265DA5 File Offset: 0x00263FA5
				public JoinProgramSetBuilder<subtractRight> SubtractRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return JoinProgramSetBuilder<subtractRight>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SubtractRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5E RID: 44382 RVA: 0x00265DD6 File Offset: 0x00263FD6
				public JoinProgramSetBuilder<multiplyRight> MultiplyRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return JoinProgramSetBuilder<multiplyRight>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MultiplyRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD5F RID: 44383 RVA: 0x00265E07 File Offset: 0x00264007
				public JoinProgramSetBuilder<divideRight> DivideRightNumber(ProgramSetBuilder<constNum> value0)
				{
					return JoinProgramSetBuilder<divideRight>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DivideRightNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD60 RID: 44384 RVA: 0x00265E38 File Offset: 0x00264038
				public JoinProgramSetBuilder<inumber> ParseNumber(ProgramSetBuilder<parseSubject> value0, ProgramSetBuilder<locale> value1)
				{
					return JoinProgramSetBuilder<inumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ParseNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD61 RID: 44385 RVA: 0x00265E78 File Offset: 0x00264078
				public JoinProgramSetBuilder<rowNumberTransform> RowNumberLinearTransform(ProgramSetBuilder<fromRowNumber> value0, ProgramSetBuilder<rowNumberLinearTransformDesc> value1)
				{
					return JoinProgramSetBuilder<rowNumberTransform>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RowNumberLinearTransform, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD62 RID: 44386 RVA: 0x00265EB8 File Offset: 0x002640B8
				public JoinProgramSetBuilder<formatDateTime> FormatDateTime(ProgramSetBuilder<date> value0, ProgramSetBuilder<dateTimeFormatDesc> value1)
				{
					return JoinProgramSetBuilder<formatDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD63 RID: 44387 RVA: 0x00265EF8 File Offset: 0x002640F8
				public JoinProgramSetBuilder<date> RoundDateTime(ProgramSetBuilder<idate> value0, ProgramSetBuilder<dateTimeRoundDesc> value1)
				{
					return JoinProgramSetBuilder<date>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RoundDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD64 RID: 44388 RVA: 0x00265F38 File Offset: 0x00264138
				public JoinProgramSetBuilder<idate> ParseDateTime(ProgramSetBuilder<parseSubject> value0, ProgramSetBuilder<dateTimeParseDesc> value1)
				{
					return JoinProgramSetBuilder<idate>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ParseDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD65 RID: 44389 RVA: 0x00265F78 File Offset: 0x00264178
				public JoinProgramSetBuilder<substring> SlicePrefixAbs(ProgramSetBuilder<x> value0, ProgramSetBuilder<slicePrefixAbsPos> value1)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SlicePrefixAbs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD66 RID: 44390 RVA: 0x00265FB8 File Offset: 0x002641B8
				public JoinProgramSetBuilder<substring> SlicePrefix(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SlicePrefix, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD67 RID: 44391 RVA: 0x00265FF8 File Offset: 0x002641F8
				public JoinProgramSetBuilder<substring> SliceSuffix(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SliceSuffix, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD68 RID: 44392 RVA: 0x00266038 File Offset: 0x00264238
				public JoinProgramSetBuilder<substring> MatchFull(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MatchFull, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD69 RID: 44393 RVA: 0x00266094 File Offset: 0x00264294
				public JoinProgramSetBuilder<substring> SliceBetween(ProgramSetBuilder<x> value0, ProgramSetBuilder<sliceBetweenStartText> value1, ProgramSetBuilder<sliceBetweenEndText> value2)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SliceBetween, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD6A RID: 44394 RVA: 0x002660EE File Offset: 0x002642EE
				public JoinProgramSetBuilder<splitTrim> TrimSplit(ProgramSetBuilder<split> value0)
				{
					return JoinProgramSetBuilder<splitTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD6B RID: 44395 RVA: 0x0026611F File Offset: 0x0026431F
				public JoinProgramSetBuilder<splitTrim> TrimFullSplit(ProgramSetBuilder<split> value0)
				{
					return JoinProgramSetBuilder<splitTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimFullSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD6C RID: 44396 RVA: 0x00266150 File Offset: 0x00264350
				public JoinProgramSetBuilder<split> Split(ProgramSetBuilder<x> value0, ProgramSetBuilder<splitDelimiter> value1, ProgramSetBuilder<splitInstance> value2)
				{
					return JoinProgramSetBuilder<split>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Split, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD6D RID: 44397 RVA: 0x002661AA File Offset: 0x002643AA
				public JoinProgramSetBuilder<sliceTrim> TrimSlice(ProgramSetBuilder<slice> value0)
				{
					return JoinProgramSetBuilder<sliceTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimSlice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD6E RID: 44398 RVA: 0x002661DB File Offset: 0x002643DB
				public JoinProgramSetBuilder<sliceTrim> TrimFullSlice(ProgramSetBuilder<slice> value0)
				{
					return JoinProgramSetBuilder<sliceTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimFullSlice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD6F RID: 44399 RVA: 0x0026620C File Offset: 0x0026440C
				public JoinProgramSetBuilder<slice> Slice(ProgramSetBuilder<x> value0, ProgramSetBuilder<pos> value1, ProgramSetBuilder<pos> value2)
				{
					return JoinProgramSetBuilder<slice>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Slice, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD70 RID: 44400 RVA: 0x00266268 File Offset: 0x00264468
				public JoinProgramSetBuilder<pos> Find(ProgramSetBuilder<x> value0, ProgramSetBuilder<findDelimiter> value1, ProgramSetBuilder<findInstance> value2, ProgramSetBuilder<findOffset> value3)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Find, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600AD71 RID: 44401 RVA: 0x002662D3 File Offset: 0x002644D3
				public JoinProgramSetBuilder<pos> Abs(ProgramSetBuilder<x> value0, ProgramSetBuilder<absPos> value1)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Abs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD72 RID: 44402 RVA: 0x00266314 File Offset: 0x00264514
				public JoinProgramSetBuilder<pos> Match(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Match, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD73 RID: 44403 RVA: 0x00266370 File Offset: 0x00264570
				public JoinProgramSetBuilder<pos> MatchEnd(ProgramSetBuilder<x> value0, ProgramSetBuilder<matchDesc> value1, ProgramSetBuilder<matchInstance> value2)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MatchEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD74 RID: 44404 RVA: 0x002663CA File Offset: 0x002645CA
				public JoinProgramSetBuilder<fromStrTrim> TrimFull(ProgramSetBuilder<fromStr> value0)
				{
					return JoinProgramSetBuilder<fromStrTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimFull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD75 RID: 44405 RVA: 0x002663FB File Offset: 0x002645FB
				public JoinProgramSetBuilder<fromStrTrim> Trim(ProgramSetBuilder<fromStr> value0)
				{
					return JoinProgramSetBuilder<fromStrTrim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD76 RID: 44406 RVA: 0x0026642C File Offset: 0x0026462C
				public JoinProgramSetBuilder<fromStr> FromStr(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromStr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD77 RID: 44407 RVA: 0x0026646C File Offset: 0x0026466C
				public JoinProgramSetBuilder<fromNumberStr> FromNumberStr(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromNumberStr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromNumberStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD78 RID: 44408 RVA: 0x002664AC File Offset: 0x002646AC
				public JoinProgramSetBuilder<fromNumber> FromNumber(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD79 RID: 44409 RVA: 0x002664EC File Offset: 0x002646EC
				public JoinProgramSetBuilder<fromNumberCoalesced> FromNumberCoalesced(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromNumberCoalesced>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromNumberCoalesced, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD7A RID: 44410 RVA: 0x0026652C File Offset: 0x0026472C
				public JoinProgramSetBuilder<fromRowNumber> FromRowNumber(ProgramSetBuilder<row> value0)
				{
					return JoinProgramSetBuilder<fromRowNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromRowNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD7B RID: 44411 RVA: 0x0026655D File Offset: 0x0026475D
				public JoinProgramSetBuilder<fromNumbers> FromNumbers(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnNames> value1)
				{
					return JoinProgramSetBuilder<fromNumbers>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromNumbers, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD7C RID: 44412 RVA: 0x0026659D File Offset: 0x0026479D
				public JoinProgramSetBuilder<fromDateTime> FromDateTime(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD7D RID: 44413 RVA: 0x002665E0 File Offset: 0x002647E0
				public JoinProgramSetBuilder<fromDateTimePart> FromDateTimePart(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1, ProgramSetBuilder<fromDateTimePartKind> value2)
				{
					return JoinProgramSetBuilder<fromDateTimePart>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromDateTimePart, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600AD7E RID: 44414 RVA: 0x0026663A File Offset: 0x0026483A
				public JoinProgramSetBuilder<fromTime> FromTime(ProgramSetBuilder<row> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<fromTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FromTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600AD7F RID: 44415 RVA: 0x0026667A File Offset: 0x0026487A
				public JoinProgramSetBuilder<constString> Str(ProgramSetBuilder<constStr> value0)
				{
					return JoinProgramSetBuilder<constString>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Str, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD80 RID: 44416 RVA: 0x002666AB File Offset: 0x002648AB
				public JoinProgramSetBuilder<constNumber> Number(ProgramSetBuilder<constNum> value0)
				{
					return JoinProgramSetBuilder<constNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Number, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD81 RID: 44417 RVA: 0x002666DC File Offset: 0x002648DC
				public JoinProgramSetBuilder<constDate> Date(ProgramSetBuilder<constDt> value0)
				{
					return JoinProgramSetBuilder<constDate>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Date, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD82 RID: 44418 RVA: 0x0026670D File Offset: 0x0026490D
				public JoinProgramSetBuilder<letSubstring> LetX(ProgramSetBuilder<fromStrTrim> value0, ProgramSetBuilder<substring> value1)
				{
					return JoinProgramSetBuilder<letSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetX, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040044EE RID: 17646
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200150D RID: 5389
			public class JoinUnnamedConversions
			{
				// Token: 0x0600AD83 RID: 44419 RVA: 0x0026674D File Offset: 0x0026494D
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600AD84 RID: 44420 RVA: 0x0026675C File Offset: 0x0026495C
				public ProgramSetBuilder<result> result_output(ProgramSetBuilder<output> value0)
				{
					return ProgramSetBuilder<result>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.result_output, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD85 RID: 44421 RVA: 0x0026678D File Offset: 0x0026498D
				public ProgramSetBuilder<result> result_inull(ProgramSetBuilder<inull> value0)
				{
					return ProgramSetBuilder<result>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.result_inull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD86 RID: 44422 RVA: 0x002667BE File Offset: 0x002649BE
				public ProgramSetBuilder<outNumber> outNumber_number(ProgramSetBuilder<number> value0)
				{
					return ProgramSetBuilder<outNumber>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outNumber_number, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD87 RID: 44423 RVA: 0x002667EF File Offset: 0x002649EF
				public ProgramSetBuilder<outNumber> outNumber_constNumber(ProgramSetBuilder<constNumber> value0)
				{
					return ProgramSetBuilder<outNumber>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outNumber_constNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD88 RID: 44424 RVA: 0x00266820 File Offset: 0x00264A20
				public ProgramSetBuilder<outDate> outDate_date(ProgramSetBuilder<date> value0)
				{
					return ProgramSetBuilder<outDate>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outDate_date, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD89 RID: 44425 RVA: 0x00266851 File Offset: 0x00264A51
				public ProgramSetBuilder<outDate> outDate_constDate(ProgramSetBuilder<constDate> value0)
				{
					return ProgramSetBuilder<outDate>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outDate_constDate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8A RID: 44426 RVA: 0x00266882 File Offset: 0x00264A82
				public ProgramSetBuilder<outStr> outStr_outStr1(ProgramSetBuilder<outStr1> value0)
				{
					return ProgramSetBuilder<outStr>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outStr_outStr1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8B RID: 44427 RVA: 0x002668B3 File Offset: 0x00264AB3
				public ProgramSetBuilder<outStr1> outStr1_segmentCase(ProgramSetBuilder<segmentCase> value0)
				{
					return ProgramSetBuilder<outStr1>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outStr1_segmentCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8C RID: 44428 RVA: 0x002668E4 File Offset: 0x00264AE4
				public ProgramSetBuilder<outStr1> outStr1_formatted(ProgramSetBuilder<formatted> value0)
				{
					return ProgramSetBuilder<outStr1>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outStr1_formatted, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8D RID: 44429 RVA: 0x00266915 File Offset: 0x00264B15
				public ProgramSetBuilder<outStr1> outStr1_concatEntry(ProgramSetBuilder<concatEntry> value0)
				{
					return ProgramSetBuilder<outStr1>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outStr1_concatEntry, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8E RID: 44430 RVA: 0x00266946 File Offset: 0x00264B46
				public ProgramSetBuilder<outStr1> outStr1_constString(ProgramSetBuilder<constString> value0)
				{
					return ProgramSetBuilder<outStr1>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.outStr1_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD8F RID: 44431 RVA: 0x00266977 File Offset: 0x00264B77
				public ProgramSetBuilder<segmentCase> segmentCase_segment(ProgramSetBuilder<segment> value0)
				{
					return ProgramSetBuilder<segmentCase>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.segmentCase_segment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD90 RID: 44432 RVA: 0x002669A8 File Offset: 0x00264BA8
				public ProgramSetBuilder<segment> segment_fromStrTrim(ProgramSetBuilder<fromStrTrim> value0)
				{
					return ProgramSetBuilder<segment>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.segment_fromStrTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD91 RID: 44433 RVA: 0x002669D9 File Offset: 0x00264BD9
				public ProgramSetBuilder<segment> segment_letSubstring(ProgramSetBuilder<letSubstring> value0)
				{
					return ProgramSetBuilder<segment>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.segment_letSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD92 RID: 44434 RVA: 0x00266A0A File Offset: 0x00264C0A
				public ProgramSetBuilder<formatted> formatted_formatNumber(ProgramSetBuilder<formatNumber> value0)
				{
					return ProgramSetBuilder<formatted>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.formatted_formatNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD93 RID: 44435 RVA: 0x00266A3B File Offset: 0x00264C3B
				public ProgramSetBuilder<formatted> formatted_formatDateTime(ProgramSetBuilder<formatDateTime> value0)
				{
					return ProgramSetBuilder<formatted>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.formatted_formatDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD94 RID: 44436 RVA: 0x00266A6C File Offset: 0x00264C6C
				public ProgramSetBuilder<concatEntry> concatEntry_concatCase(ProgramSetBuilder<concatCase> value0)
				{
					return ProgramSetBuilder<concatEntry>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatEntry_concatCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD95 RID: 44437 RVA: 0x00266A9D File Offset: 0x00264C9D
				public ProgramSetBuilder<concatEntry> concatEntry_constString(ProgramSetBuilder<constString> value0)
				{
					return ProgramSetBuilder<concatEntry>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatEntry_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD96 RID: 44438 RVA: 0x00266ACE File Offset: 0x00264CCE
				public ProgramSetBuilder<concatCase> concatCase_concat(ProgramSetBuilder<concat> value0)
				{
					return ProgramSetBuilder<concatCase>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatCase_concat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD97 RID: 44439 RVA: 0x00266AFF File Offset: 0x00264CFF
				public ProgramSetBuilder<concatPrefix> concatPrefix_concatSegment(ProgramSetBuilder<concatSegment> value0)
				{
					return ProgramSetBuilder<concatPrefix>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatPrefix_concatSegment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD98 RID: 44440 RVA: 0x00266B30 File Offset: 0x00264D30
				public ProgramSetBuilder<concatPrefix> concatPrefix_formatted(ProgramSetBuilder<formatted> value0)
				{
					return ProgramSetBuilder<concatPrefix>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatPrefix_formatted, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD99 RID: 44441 RVA: 0x00266B61 File Offset: 0x00264D61
				public ProgramSetBuilder<concatPrefix> concatPrefix_constString(ProgramSetBuilder<constString> value0)
				{
					return ProgramSetBuilder<concatPrefix>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatPrefix_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9A RID: 44442 RVA: 0x00266B92 File Offset: 0x00264D92
				public ProgramSetBuilder<concatSegment> concatSegment_segment(ProgramSetBuilder<segment> value0)
				{
					return ProgramSetBuilder<concatSegment>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatSegment_segment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9B RID: 44443 RVA: 0x00266BC3 File Offset: 0x00264DC3
				public ProgramSetBuilder<concatSegment> concatSegment_segmentCase(ProgramSetBuilder<segmentCase> value0)
				{
					return ProgramSetBuilder<concatSegment>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatSegment_segmentCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9C RID: 44444 RVA: 0x00266BF4 File Offset: 0x00264DF4
				public ProgramSetBuilder<concatSuffix> concatSuffix_concatPrefix(ProgramSetBuilder<concatPrefix> value0)
				{
					return ProgramSetBuilder<concatSuffix>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatSuffix_concatPrefix, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9D RID: 44445 RVA: 0x00266C25 File Offset: 0x00264E25
				public ProgramSetBuilder<concatSuffix> concatSuffix_concat(ProgramSetBuilder<concat> value0)
				{
					return ProgramSetBuilder<concatSuffix>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.concatSuffix_concat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9E RID: 44446 RVA: 0x00266C56 File Offset: 0x00264E56
				public ProgramSetBuilder<condition> condition_or(ProgramSetBuilder<or> value0)
				{
					return ProgramSetBuilder<condition>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.condition_or, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600AD9F RID: 44447 RVA: 0x00266C87 File Offset: 0x00264E87
				public ProgramSetBuilder<number> number_number1(ProgramSetBuilder<number1> value0)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.number_number1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA0 RID: 44448 RVA: 0x00266CB8 File Offset: 0x00264EB8
				public ProgramSetBuilder<number> number_arithmetic(ProgramSetBuilder<arithmetic> value0)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.number_arithmetic, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA1 RID: 44449 RVA: 0x00266CE9 File Offset: 0x00264EE9
				public ProgramSetBuilder<number> number_rowNumberTransform(ProgramSetBuilder<rowNumberTransform> value0)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.number_rowNumberTransform, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA2 RID: 44450 RVA: 0x00266D1A File Offset: 0x00264F1A
				public ProgramSetBuilder<number1> number1_inumber(ProgramSetBuilder<inumber> value0)
				{
					return ProgramSetBuilder<number1>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.number1_inumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA3 RID: 44451 RVA: 0x00266D4B File Offset: 0x00264F4B
				public ProgramSetBuilder<arithmeticLeft> arithmeticLeft_fromNumberCoalesced(ProgramSetBuilder<fromNumberCoalesced> value0)
				{
					return ProgramSetBuilder<arithmeticLeft>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.arithmeticLeft_fromNumberCoalesced, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA4 RID: 44452 RVA: 0x00266D7C File Offset: 0x00264F7C
				public ProgramSetBuilder<arithmeticLeft> arithmeticLeft_inumber(ProgramSetBuilder<inumber> value0)
				{
					return ProgramSetBuilder<arithmeticLeft>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.arithmeticLeft_inumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA5 RID: 44453 RVA: 0x00266DAD File Offset: 0x00264FAD
				public ProgramSetBuilder<addRight> addRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return ProgramSetBuilder<addRight>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.addRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA6 RID: 44454 RVA: 0x00266DDE File Offset: 0x00264FDE
				public ProgramSetBuilder<subtractRight> subtractRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return ProgramSetBuilder<subtractRight>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.subtractRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA7 RID: 44455 RVA: 0x00266E0F File Offset: 0x0026500F
				public ProgramSetBuilder<multiplyRight> multiplyRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return ProgramSetBuilder<multiplyRight>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.multiplyRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA8 RID: 44456 RVA: 0x00266E40 File Offset: 0x00265040
				public ProgramSetBuilder<divideRight> divideRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return ProgramSetBuilder<divideRight>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.divideRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADA9 RID: 44457 RVA: 0x00266E71 File Offset: 0x00265071
				public ProgramSetBuilder<inumber> inumber_fromNumber(ProgramSetBuilder<fromNumber> value0)
				{
					return ProgramSetBuilder<inumber>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.inumber_fromNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAA RID: 44458 RVA: 0x00266EA2 File Offset: 0x002650A2
				public ProgramSetBuilder<rowNumberTransform> rowNumberTransform_fromRowNumber(ProgramSetBuilder<fromRowNumber> value0)
				{
					return ProgramSetBuilder<rowNumberTransform>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.rowNumberTransform_fromRowNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAB RID: 44459 RVA: 0x00266ED3 File Offset: 0x002650D3
				public ProgramSetBuilder<date> date_idate(ProgramSetBuilder<idate> value0)
				{
					return ProgramSetBuilder<date>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.date_idate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAC RID: 44460 RVA: 0x00266F04 File Offset: 0x00265104
				public ProgramSetBuilder<idate> idate_fromDateTime(ProgramSetBuilder<fromDateTime> value0)
				{
					return ProgramSetBuilder<idate>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.idate_fromDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAD RID: 44461 RVA: 0x00266F35 File Offset: 0x00265135
				public ProgramSetBuilder<idate> idate_fromDateTimePart(ProgramSetBuilder<fromDateTimePart> value0)
				{
					return ProgramSetBuilder<idate>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.idate_fromDateTimePart, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAE RID: 44462 RVA: 0x00266F66 File Offset: 0x00265166
				public ProgramSetBuilder<itime> itime_fromTime(ProgramSetBuilder<fromTime> value0)
				{
					return ProgramSetBuilder<itime>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.itime_fromTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADAF RID: 44463 RVA: 0x00266F97 File Offset: 0x00265197
				public ProgramSetBuilder<parseSubject> parseSubject_fromStr(ProgramSetBuilder<fromStr> value0)
				{
					return ProgramSetBuilder<parseSubject>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.parseSubject_fromStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB0 RID: 44464 RVA: 0x00266FC8 File Offset: 0x002651C8
				public ProgramSetBuilder<parseSubject> parseSubject_letSubstring(ProgramSetBuilder<letSubstring> value0)
				{
					return ProgramSetBuilder<parseSubject>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.parseSubject_letSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB1 RID: 44465 RVA: 0x00266FF9 File Offset: 0x002651F9
				public ProgramSetBuilder<substring> substring_splitTrim(ProgramSetBuilder<splitTrim> value0)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.substring_splitTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB2 RID: 44466 RVA: 0x0026702A File Offset: 0x0026522A
				public ProgramSetBuilder<substring> substring_sliceTrim(ProgramSetBuilder<sliceTrim> value0)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.substring_sliceTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB3 RID: 44467 RVA: 0x0026705B File Offset: 0x0026525B
				public ProgramSetBuilder<splitTrim> splitTrim_split(ProgramSetBuilder<split> value0)
				{
					return ProgramSetBuilder<splitTrim>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.splitTrim_split, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB4 RID: 44468 RVA: 0x0026708C File Offset: 0x0026528C
				public ProgramSetBuilder<sliceTrim> sliceTrim_slice(ProgramSetBuilder<slice> value0)
				{
					return ProgramSetBuilder<sliceTrim>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.sliceTrim_slice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB5 RID: 44469 RVA: 0x002670BD File Offset: 0x002652BD
				public ProgramSetBuilder<fromStrTrim> fromStrTrim_fromStr(ProgramSetBuilder<fromStr> value0)
				{
					return ProgramSetBuilder<fromStrTrim>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.fromStrTrim_fromStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB6 RID: 44470 RVA: 0x002670EE File Offset: 0x002652EE
				public ProgramSetBuilder<fromStrTrim> fromStrTrim_fromNumberStr(ProgramSetBuilder<fromNumberStr> value0)
				{
					return ProgramSetBuilder<fromStrTrim>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.fromStrTrim_fromNumberStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040044EF RID: 17647
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200150E RID: 5390
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x0600ADB7 RID: 44471 RVA: 0x0026711F File Offset: 0x0026531F
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ADB8 RID: 44472 RVA: 0x0026712E File Offset: 0x0026532E
				public JoinProgramSetBuilder<result> result_output(ProgramSetBuilder<output> value0)
				{
					return JoinProgramSetBuilder<result>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.result_output, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADB9 RID: 44473 RVA: 0x0026715F File Offset: 0x0026535F
				public JoinProgramSetBuilder<result> result_inull(ProgramSetBuilder<inull> value0)
				{
					return JoinProgramSetBuilder<result>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.result_inull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBA RID: 44474 RVA: 0x00267190 File Offset: 0x00265390
				public JoinProgramSetBuilder<outNumber> outNumber_number(ProgramSetBuilder<number> value0)
				{
					return JoinProgramSetBuilder<outNumber>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outNumber_number, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBB RID: 44475 RVA: 0x002671C1 File Offset: 0x002653C1
				public JoinProgramSetBuilder<outNumber> outNumber_constNumber(ProgramSetBuilder<constNumber> value0)
				{
					return JoinProgramSetBuilder<outNumber>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outNumber_constNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBC RID: 44476 RVA: 0x002671F2 File Offset: 0x002653F2
				public JoinProgramSetBuilder<outDate> outDate_date(ProgramSetBuilder<date> value0)
				{
					return JoinProgramSetBuilder<outDate>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outDate_date, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBD RID: 44477 RVA: 0x00267223 File Offset: 0x00265423
				public JoinProgramSetBuilder<outDate> outDate_constDate(ProgramSetBuilder<constDate> value0)
				{
					return JoinProgramSetBuilder<outDate>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outDate_constDate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBE RID: 44478 RVA: 0x00267254 File Offset: 0x00265454
				public JoinProgramSetBuilder<outStr> outStr_outStr1(ProgramSetBuilder<outStr1> value0)
				{
					return JoinProgramSetBuilder<outStr>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outStr_outStr1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADBF RID: 44479 RVA: 0x00267285 File Offset: 0x00265485
				public JoinProgramSetBuilder<outStr1> outStr1_segmentCase(ProgramSetBuilder<segmentCase> value0)
				{
					return JoinProgramSetBuilder<outStr1>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outStr1_segmentCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC0 RID: 44480 RVA: 0x002672B6 File Offset: 0x002654B6
				public JoinProgramSetBuilder<outStr1> outStr1_formatted(ProgramSetBuilder<formatted> value0)
				{
					return JoinProgramSetBuilder<outStr1>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outStr1_formatted, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC1 RID: 44481 RVA: 0x002672E7 File Offset: 0x002654E7
				public JoinProgramSetBuilder<outStr1> outStr1_concatEntry(ProgramSetBuilder<concatEntry> value0)
				{
					return JoinProgramSetBuilder<outStr1>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outStr1_concatEntry, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC2 RID: 44482 RVA: 0x00267318 File Offset: 0x00265518
				public JoinProgramSetBuilder<outStr1> outStr1_constString(ProgramSetBuilder<constString> value0)
				{
					return JoinProgramSetBuilder<outStr1>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.outStr1_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC3 RID: 44483 RVA: 0x00267349 File Offset: 0x00265549
				public JoinProgramSetBuilder<segmentCase> segmentCase_segment(ProgramSetBuilder<segment> value0)
				{
					return JoinProgramSetBuilder<segmentCase>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.segmentCase_segment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC4 RID: 44484 RVA: 0x0026737A File Offset: 0x0026557A
				public JoinProgramSetBuilder<segment> segment_fromStrTrim(ProgramSetBuilder<fromStrTrim> value0)
				{
					return JoinProgramSetBuilder<segment>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.segment_fromStrTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC5 RID: 44485 RVA: 0x002673AB File Offset: 0x002655AB
				public JoinProgramSetBuilder<segment> segment_letSubstring(ProgramSetBuilder<letSubstring> value0)
				{
					return JoinProgramSetBuilder<segment>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.segment_letSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC6 RID: 44486 RVA: 0x002673DC File Offset: 0x002655DC
				public JoinProgramSetBuilder<formatted> formatted_formatNumber(ProgramSetBuilder<formatNumber> value0)
				{
					return JoinProgramSetBuilder<formatted>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.formatted_formatNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC7 RID: 44487 RVA: 0x0026740D File Offset: 0x0026560D
				public JoinProgramSetBuilder<formatted> formatted_formatDateTime(ProgramSetBuilder<formatDateTime> value0)
				{
					return JoinProgramSetBuilder<formatted>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.formatted_formatDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC8 RID: 44488 RVA: 0x0026743E File Offset: 0x0026563E
				public JoinProgramSetBuilder<concatEntry> concatEntry_concatCase(ProgramSetBuilder<concatCase> value0)
				{
					return JoinProgramSetBuilder<concatEntry>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatEntry_concatCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADC9 RID: 44489 RVA: 0x0026746F File Offset: 0x0026566F
				public JoinProgramSetBuilder<concatEntry> concatEntry_constString(ProgramSetBuilder<constString> value0)
				{
					return JoinProgramSetBuilder<concatEntry>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatEntry_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCA RID: 44490 RVA: 0x002674A0 File Offset: 0x002656A0
				public JoinProgramSetBuilder<concatCase> concatCase_concat(ProgramSetBuilder<concat> value0)
				{
					return JoinProgramSetBuilder<concatCase>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatCase_concat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCB RID: 44491 RVA: 0x002674D1 File Offset: 0x002656D1
				public JoinProgramSetBuilder<concatPrefix> concatPrefix_concatSegment(ProgramSetBuilder<concatSegment> value0)
				{
					return JoinProgramSetBuilder<concatPrefix>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatPrefix_concatSegment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCC RID: 44492 RVA: 0x00267502 File Offset: 0x00265702
				public JoinProgramSetBuilder<concatPrefix> concatPrefix_formatted(ProgramSetBuilder<formatted> value0)
				{
					return JoinProgramSetBuilder<concatPrefix>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatPrefix_formatted, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCD RID: 44493 RVA: 0x00267533 File Offset: 0x00265733
				public JoinProgramSetBuilder<concatPrefix> concatPrefix_constString(ProgramSetBuilder<constString> value0)
				{
					return JoinProgramSetBuilder<concatPrefix>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatPrefix_constString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCE RID: 44494 RVA: 0x00267564 File Offset: 0x00265764
				public JoinProgramSetBuilder<concatSegment> concatSegment_segment(ProgramSetBuilder<segment> value0)
				{
					return JoinProgramSetBuilder<concatSegment>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatSegment_segment, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADCF RID: 44495 RVA: 0x00267595 File Offset: 0x00265795
				public JoinProgramSetBuilder<concatSegment> concatSegment_segmentCase(ProgramSetBuilder<segmentCase> value0)
				{
					return JoinProgramSetBuilder<concatSegment>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatSegment_segmentCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD0 RID: 44496 RVA: 0x002675C6 File Offset: 0x002657C6
				public JoinProgramSetBuilder<concatSuffix> concatSuffix_concatPrefix(ProgramSetBuilder<concatPrefix> value0)
				{
					return JoinProgramSetBuilder<concatSuffix>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatSuffix_concatPrefix, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD1 RID: 44497 RVA: 0x002675F7 File Offset: 0x002657F7
				public JoinProgramSetBuilder<concatSuffix> concatSuffix_concat(ProgramSetBuilder<concat> value0)
				{
					return JoinProgramSetBuilder<concatSuffix>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.concatSuffix_concat, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD2 RID: 44498 RVA: 0x00267628 File Offset: 0x00265828
				public JoinProgramSetBuilder<condition> condition_or(ProgramSetBuilder<or> value0)
				{
					return JoinProgramSetBuilder<condition>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.condition_or, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD3 RID: 44499 RVA: 0x00267659 File Offset: 0x00265859
				public JoinProgramSetBuilder<number> number_number1(ProgramSetBuilder<number1> value0)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.number_number1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD4 RID: 44500 RVA: 0x0026768A File Offset: 0x0026588A
				public JoinProgramSetBuilder<number> number_arithmetic(ProgramSetBuilder<arithmetic> value0)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.number_arithmetic, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD5 RID: 44501 RVA: 0x002676BB File Offset: 0x002658BB
				public JoinProgramSetBuilder<number> number_rowNumberTransform(ProgramSetBuilder<rowNumberTransform> value0)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.number_rowNumberTransform, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD6 RID: 44502 RVA: 0x002676EC File Offset: 0x002658EC
				public JoinProgramSetBuilder<number1> number1_inumber(ProgramSetBuilder<inumber> value0)
				{
					return JoinProgramSetBuilder<number1>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.number1_inumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD7 RID: 44503 RVA: 0x0026771D File Offset: 0x0026591D
				public JoinProgramSetBuilder<arithmeticLeft> arithmeticLeft_fromNumberCoalesced(ProgramSetBuilder<fromNumberCoalesced> value0)
				{
					return JoinProgramSetBuilder<arithmeticLeft>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.arithmeticLeft_fromNumberCoalesced, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD8 RID: 44504 RVA: 0x0026774E File Offset: 0x0026594E
				public JoinProgramSetBuilder<arithmeticLeft> arithmeticLeft_inumber(ProgramSetBuilder<inumber> value0)
				{
					return JoinProgramSetBuilder<arithmeticLeft>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.arithmeticLeft_inumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADD9 RID: 44505 RVA: 0x0026777F File Offset: 0x0026597F
				public JoinProgramSetBuilder<addRight> addRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return JoinProgramSetBuilder<addRight>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.addRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDA RID: 44506 RVA: 0x002677B0 File Offset: 0x002659B0
				public JoinProgramSetBuilder<subtractRight> subtractRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return JoinProgramSetBuilder<subtractRight>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.subtractRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDB RID: 44507 RVA: 0x002677E1 File Offset: 0x002659E1
				public JoinProgramSetBuilder<multiplyRight> multiplyRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return JoinProgramSetBuilder<multiplyRight>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.multiplyRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDC RID: 44508 RVA: 0x00267812 File Offset: 0x00265A12
				public JoinProgramSetBuilder<divideRight> divideRight_arithmeticLeft(ProgramSetBuilder<arithmeticLeft> value0)
				{
					return JoinProgramSetBuilder<divideRight>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.divideRight_arithmeticLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDD RID: 44509 RVA: 0x00267843 File Offset: 0x00265A43
				public JoinProgramSetBuilder<inumber> inumber_fromNumber(ProgramSetBuilder<fromNumber> value0)
				{
					return JoinProgramSetBuilder<inumber>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.inumber_fromNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDE RID: 44510 RVA: 0x00267874 File Offset: 0x00265A74
				public JoinProgramSetBuilder<rowNumberTransform> rowNumberTransform_fromRowNumber(ProgramSetBuilder<fromRowNumber> value0)
				{
					return JoinProgramSetBuilder<rowNumberTransform>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.rowNumberTransform_fromRowNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADDF RID: 44511 RVA: 0x002678A5 File Offset: 0x00265AA5
				public JoinProgramSetBuilder<date> date_idate(ProgramSetBuilder<idate> value0)
				{
					return JoinProgramSetBuilder<date>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.date_idate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE0 RID: 44512 RVA: 0x002678D6 File Offset: 0x00265AD6
				public JoinProgramSetBuilder<idate> idate_fromDateTime(ProgramSetBuilder<fromDateTime> value0)
				{
					return JoinProgramSetBuilder<idate>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.idate_fromDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE1 RID: 44513 RVA: 0x00267907 File Offset: 0x00265B07
				public JoinProgramSetBuilder<idate> idate_fromDateTimePart(ProgramSetBuilder<fromDateTimePart> value0)
				{
					return JoinProgramSetBuilder<idate>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.idate_fromDateTimePart, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE2 RID: 44514 RVA: 0x00267938 File Offset: 0x00265B38
				public JoinProgramSetBuilder<itime> itime_fromTime(ProgramSetBuilder<fromTime> value0)
				{
					return JoinProgramSetBuilder<itime>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.itime_fromTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE3 RID: 44515 RVA: 0x00267969 File Offset: 0x00265B69
				public JoinProgramSetBuilder<parseSubject> parseSubject_fromStr(ProgramSetBuilder<fromStr> value0)
				{
					return JoinProgramSetBuilder<parseSubject>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.parseSubject_fromStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE4 RID: 44516 RVA: 0x0026799A File Offset: 0x00265B9A
				public JoinProgramSetBuilder<parseSubject> parseSubject_letSubstring(ProgramSetBuilder<letSubstring> value0)
				{
					return JoinProgramSetBuilder<parseSubject>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.parseSubject_letSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE5 RID: 44517 RVA: 0x002679CB File Offset: 0x00265BCB
				public JoinProgramSetBuilder<substring> substring_splitTrim(ProgramSetBuilder<splitTrim> value0)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.substring_splitTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE6 RID: 44518 RVA: 0x002679FC File Offset: 0x00265BFC
				public JoinProgramSetBuilder<substring> substring_sliceTrim(ProgramSetBuilder<sliceTrim> value0)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.substring_sliceTrim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE7 RID: 44519 RVA: 0x00267A2D File Offset: 0x00265C2D
				public JoinProgramSetBuilder<splitTrim> splitTrim_split(ProgramSetBuilder<split> value0)
				{
					return JoinProgramSetBuilder<splitTrim>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.splitTrim_split, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE8 RID: 44520 RVA: 0x00267A5E File Offset: 0x00265C5E
				public JoinProgramSetBuilder<sliceTrim> sliceTrim_slice(ProgramSetBuilder<slice> value0)
				{
					return JoinProgramSetBuilder<sliceTrim>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.sliceTrim_slice, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADE9 RID: 44521 RVA: 0x00267A8F File Offset: 0x00265C8F
				public JoinProgramSetBuilder<fromStrTrim> fromStrTrim_fromStr(ProgramSetBuilder<fromStr> value0)
				{
					return JoinProgramSetBuilder<fromStrTrim>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.fromStrTrim_fromStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600ADEA RID: 44522 RVA: 0x00267AC0 File Offset: 0x00265CC0
				public JoinProgramSetBuilder<fromStrTrim> fromStrTrim_fromNumberStr(ProgramSetBuilder<fromNumberStr> value0)
				{
					return JoinProgramSetBuilder<fromStrTrim>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.fromStrTrim_fromNumberStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040044F0 RID: 17648
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200150F RID: 5391
			public class Casts
			{
				// Token: 0x0600ADEB RID: 44523 RVA: 0x00267AF1 File Offset: 0x00265CF1
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ADEC RID: 44524 RVA: 0x00267B00 File Offset: 0x00265D00
				public ProgramSetBuilder<result> result(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.result)
					{
						string text = "set";
						string text2 = "expected program set for symbol result but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.result>.CreateUnsafe(set);
				}

				// Token: 0x0600ADED RID: 44525 RVA: 0x00267B58 File Offset: 0x00265D58
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x0600ADEE RID: 44526 RVA: 0x00267BB0 File Offset: 0x00265DB0
				public ProgramSetBuilder<outNumber> outNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.outNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol outNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600ADEF RID: 44527 RVA: 0x00267C08 File Offset: 0x00265E08
				public ProgramSetBuilder<outDate> outDate(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.outDate)
					{
						string text = "set";
						string text2 = "expected program set for symbol outDate but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outDate>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF0 RID: 44528 RVA: 0x00267C60 File Offset: 0x00265E60
				public ProgramSetBuilder<outStr> outStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.outStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol outStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF1 RID: 44529 RVA: 0x00267CB8 File Offset: 0x00265EB8
				public ProgramSetBuilder<outStr1> outStr1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.outStr1)
					{
						string text = "set";
						string text2 = "expected program set for symbol outStr1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.outStr1>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF2 RID: 44530 RVA: 0x00267D10 File Offset: 0x00265F10
				public ProgramSetBuilder<segmentCase> segmentCase(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.segmentCase)
					{
						string text = "set";
						string text2 = "expected program set for symbol segmentCase but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segmentCase>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF3 RID: 44531 RVA: 0x00267D68 File Offset: 0x00265F68
				public ProgramSetBuilder<segment> segment(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.segment)
					{
						string text = "set";
						string text2 = "expected program set for symbol segment but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.segment>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF4 RID: 44532 RVA: 0x00267DC0 File Offset: 0x00265FC0
				public ProgramSetBuilder<formatted> formatted(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.formatted)
					{
						string text = "set";
						string text2 = "expected program set for symbol formatted but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatted>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF5 RID: 44533 RVA: 0x00267E18 File Offset: 0x00266018
				public ProgramSetBuilder<concatEntry> concatEntry(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concatEntry)
					{
						string text = "set";
						string text2 = "expected program set for symbol concatEntry but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatEntry>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF6 RID: 44534 RVA: 0x00267E70 File Offset: 0x00266070
				public ProgramSetBuilder<concatCase> concatCase(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concatCase)
					{
						string text = "set";
						string text2 = "expected program set for symbol concatCase but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatCase>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF7 RID: 44535 RVA: 0x00267EC8 File Offset: 0x002660C8
				public ProgramSetBuilder<concat> concat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concat)
					{
						string text = "set";
						string text2 = "expected program set for symbol concat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concat>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF8 RID: 44536 RVA: 0x00267F20 File Offset: 0x00266120
				public ProgramSetBuilder<concatPrefix> concatPrefix(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concatPrefix)
					{
						string text = "set";
						string text2 = "expected program set for symbol concatPrefix but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatPrefix>.CreateUnsafe(set);
				}

				// Token: 0x0600ADF9 RID: 44537 RVA: 0x00267F78 File Offset: 0x00266178
				public ProgramSetBuilder<concatSegment> concatSegment(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concatSegment)
					{
						string text = "set";
						string text2 = "expected program set for symbol concatSegment but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSegment>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFA RID: 44538 RVA: 0x00267FD0 File Offset: 0x002661D0
				public ProgramSetBuilder<concatSuffix> concatSuffix(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.concatSuffix)
					{
						string text = "set";
						string text2 = "expected program set for symbol concatSuffix but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.concatSuffix>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFB RID: 44539 RVA: 0x00268028 File Offset: 0x00266228
				public ProgramSetBuilder<condition> condition(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.condition)
					{
						string text = "set";
						string text2 = "expected program set for symbol condition but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.condition>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFC RID: 44540 RVA: 0x00268080 File Offset: 0x00266280
				public ProgramSetBuilder<or> or(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.or)
					{
						string text = "set";
						string text2 = "expected program set for symbol @or but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.or>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFD RID: 44541 RVA: 0x002680D8 File Offset: 0x002662D8
				public ProgramSetBuilder<inull> inull(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inull)
					{
						string text = "set";
						string text2 = "expected program set for symbol inull but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inull>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFE RID: 44542 RVA: 0x00268130 File Offset: 0x00266330
				public ProgramSetBuilder<equalsText> equalsText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.equalsText)
					{
						string text = "set";
						string text2 = "expected program set for symbol equalsText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.equalsText>.CreateUnsafe(set);
				}

				// Token: 0x0600ADFF RID: 44543 RVA: 0x00268188 File Offset: 0x00266388
				public ProgramSetBuilder<containsFindText> containsFindText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.containsFindText)
					{
						string text = "set";
						string text2 = "expected program set for symbol containsFindText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsFindText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE00 RID: 44544 RVA: 0x002681E0 File Offset: 0x002663E0
				public ProgramSetBuilder<startsWithFindText> startsWithFindText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.startsWithFindText)
					{
						string text = "set";
						string text2 = "expected program set for symbol startsWithFindText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.startsWithFindText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE01 RID: 44545 RVA: 0x00268238 File Offset: 0x00266438
				public ProgramSetBuilder<isMatchRegex> isMatchRegex(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.isMatchRegex)
					{
						string text = "set";
						string text2 = "expected program set for symbol isMatchRegex but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.isMatchRegex>.CreateUnsafe(set);
				}

				// Token: 0x0600AE02 RID: 44546 RVA: 0x00268290 File Offset: 0x00266490
				public ProgramSetBuilder<containsMatchRegex> containsMatchRegex(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.containsMatchRegex)
					{
						string text = "set";
						string text2 = "expected program set for symbol containsMatchRegex but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsMatchRegex>.CreateUnsafe(set);
				}

				// Token: 0x0600AE03 RID: 44547 RVA: 0x002682E8 File Offset: 0x002664E8
				public ProgramSetBuilder<containsCount> containsCount(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.containsCount)
					{
						string text = "set";
						string text2 = "expected program set for symbol containsCount but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.containsCount>.CreateUnsafe(set);
				}

				// Token: 0x0600AE04 RID: 44548 RVA: 0x00268340 File Offset: 0x00266540
				public ProgramSetBuilder<matchCount> matchCount(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.matchCount)
					{
						string text = "set";
						string text2 = "expected program set for symbol matchCount but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchCount>.CreateUnsafe(set);
				}

				// Token: 0x0600AE05 RID: 44549 RVA: 0x00268398 File Offset: 0x00266598
				public ProgramSetBuilder<numberEqualsValue> numberEqualsValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberEqualsValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberEqualsValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberEqualsValue>.CreateUnsafe(set);
				}

				// Token: 0x0600AE06 RID: 44550 RVA: 0x002683F0 File Offset: 0x002665F0
				public ProgramSetBuilder<numberGreaterThanValue> numberGreaterThanValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberGreaterThanValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberGreaterThanValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberGreaterThanValue>.CreateUnsafe(set);
				}

				// Token: 0x0600AE07 RID: 44551 RVA: 0x00268448 File Offset: 0x00266648
				public ProgramSetBuilder<numberLessThanValue> numberLessThanValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberLessThanValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberLessThanValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberLessThanValue>.CreateUnsafe(set);
				}

				// Token: 0x0600AE08 RID: 44552 RVA: 0x002684A0 File Offset: 0x002666A0
				public ProgramSetBuilder<formatNumber> formatNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.formatNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol formatNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600AE09 RID: 44553 RVA: 0x002684F8 File Offset: 0x002666F8
				public ProgramSetBuilder<number> number(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.number)
					{
						string text = "set";
						string text2 = "expected program set for symbol number but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0A RID: 44554 RVA: 0x00268550 File Offset: 0x00266750
				public ProgramSetBuilder<number1> number1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.number1)
					{
						string text = "set";
						string text2 = "expected program set for symbol number1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.number1>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0B RID: 44555 RVA: 0x002685A8 File Offset: 0x002667A8
				public ProgramSetBuilder<arithmetic> arithmetic(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.arithmetic)
					{
						string text = "set";
						string text2 = "expected program set for symbol arithmetic but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmetic>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0C RID: 44556 RVA: 0x00268600 File Offset: 0x00266800
				public ProgramSetBuilder<arithmeticLeft> arithmeticLeft(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.arithmeticLeft)
					{
						string text = "set";
						string text2 = "expected program set for symbol arithmeticLeft but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.arithmeticLeft>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0D RID: 44557 RVA: 0x00268658 File Offset: 0x00266858
				public ProgramSetBuilder<addRight> addRight(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.addRight)
					{
						string text = "set";
						string text2 = "expected program set for symbol addRight but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.addRight>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0E RID: 44558 RVA: 0x002686B0 File Offset: 0x002668B0
				public ProgramSetBuilder<subtractRight> subtractRight(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.subtractRight)
					{
						string text = "set";
						string text2 = "expected program set for symbol subtractRight but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.subtractRight>.CreateUnsafe(set);
				}

				// Token: 0x0600AE0F RID: 44559 RVA: 0x00268708 File Offset: 0x00266908
				public ProgramSetBuilder<multiplyRight> multiplyRight(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.multiplyRight)
					{
						string text = "set";
						string text2 = "expected program set for symbol multiplyRight but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.multiplyRight>.CreateUnsafe(set);
				}

				// Token: 0x0600AE10 RID: 44560 RVA: 0x00268760 File Offset: 0x00266960
				public ProgramSetBuilder<divideRight> divideRight(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.divideRight)
					{
						string text = "set";
						string text2 = "expected program set for symbol divideRight but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.divideRight>.CreateUnsafe(set);
				}

				// Token: 0x0600AE11 RID: 44561 RVA: 0x002687B8 File Offset: 0x002669B8
				public ProgramSetBuilder<inumber> inumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol inumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.inumber>.CreateUnsafe(set);
				}

				// Token: 0x0600AE12 RID: 44562 RVA: 0x00268810 File Offset: 0x00266A10
				public ProgramSetBuilder<rowNumberTransform> rowNumberTransform(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rowNumberTransform)
					{
						string text = "set";
						string text2 = "expected program set for symbol rowNumberTransform but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberTransform>.CreateUnsafe(set);
				}

				// Token: 0x0600AE13 RID: 44563 RVA: 0x00268868 File Offset: 0x00266A68
				public ProgramSetBuilder<formatDateTime> formatDateTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.formatDateTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol formatDateTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.formatDateTime>.CreateUnsafe(set);
				}

				// Token: 0x0600AE14 RID: 44564 RVA: 0x002688C0 File Offset: 0x00266AC0
				public ProgramSetBuilder<date> date(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.date)
					{
						string text = "set";
						string text2 = "expected program set for symbol date but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.date>.CreateUnsafe(set);
				}

				// Token: 0x0600AE15 RID: 44565 RVA: 0x00268918 File Offset: 0x00266B18
				public ProgramSetBuilder<idate> idate(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.idate)
					{
						string text = "set";
						string text2 = "expected program set for symbol idate but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.idate>.CreateUnsafe(set);
				}

				// Token: 0x0600AE16 RID: 44566 RVA: 0x00268970 File Offset: 0x00266B70
				public ProgramSetBuilder<itime> itime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.itime)
					{
						string text = "set";
						string text2 = "expected program set for symbol itime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.itime>.CreateUnsafe(set);
				}

				// Token: 0x0600AE17 RID: 44567 RVA: 0x002689C8 File Offset: 0x00266BC8
				public ProgramSetBuilder<parseSubject> parseSubject(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.parseSubject)
					{
						string text = "set";
						string text2 = "expected program set for symbol parseSubject but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.parseSubject>.CreateUnsafe(set);
				}

				// Token: 0x0600AE18 RID: 44568 RVA: 0x00268A20 File Offset: 0x00266C20
				public ProgramSetBuilder<letSubstring> letSubstring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.letSubstring)
					{
						string text = "set";
						string text2 = "expected program set for symbol letSubstring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.letSubstring>.CreateUnsafe(set);
				}

				// Token: 0x0600AE19 RID: 44569 RVA: 0x00268A78 File Offset: 0x00266C78
				public ProgramSetBuilder<x> x(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.x)
					{
						string text = "set";
						string text2 = "expected program set for symbol x but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.x>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1A RID: 44570 RVA: 0x00268AD0 File Offset: 0x00266CD0
				public ProgramSetBuilder<substring> substring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.substring)
					{
						string text = "set";
						string text2 = "expected program set for symbol substring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.substring>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1B RID: 44571 RVA: 0x00268B28 File Offset: 0x00266D28
				public ProgramSetBuilder<splitTrim> splitTrim(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitTrim)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitTrim but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitTrim>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1C RID: 44572 RVA: 0x00268B80 File Offset: 0x00266D80
				public ProgramSetBuilder<split> split(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.split)
					{
						string text = "set";
						string text2 = "expected program set for symbol split but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.split>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1D RID: 44573 RVA: 0x00268BD8 File Offset: 0x00266DD8
				public ProgramSetBuilder<sliceTrim> sliceTrim(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sliceTrim)
					{
						string text = "set";
						string text2 = "expected program set for symbol sliceTrim but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceTrim>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1E RID: 44574 RVA: 0x00268C30 File Offset: 0x00266E30
				public ProgramSetBuilder<slice> slice(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.slice)
					{
						string text = "set";
						string text2 = "expected program set for symbol slice but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slice>.CreateUnsafe(set);
				}

				// Token: 0x0600AE1F RID: 44575 RVA: 0x00268C88 File Offset: 0x00266E88
				public ProgramSetBuilder<pos> pos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pos)
					{
						string text = "set";
						string text2 = "expected program set for symbol pos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.pos>.CreateUnsafe(set);
				}

				// Token: 0x0600AE20 RID: 44576 RVA: 0x00268CE0 File Offset: 0x00266EE0
				public ProgramSetBuilder<fromStrTrim> fromStrTrim(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromStrTrim)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromStrTrim but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStrTrim>.CreateUnsafe(set);
				}

				// Token: 0x0600AE21 RID: 44577 RVA: 0x00268D38 File Offset: 0x00266F38
				public ProgramSetBuilder<fromStr> fromStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromStr>.CreateUnsafe(set);
				}

				// Token: 0x0600AE22 RID: 44578 RVA: 0x00268D90 File Offset: 0x00266F90
				public ProgramSetBuilder<fromNumberStr> fromNumberStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromNumberStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromNumberStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberStr>.CreateUnsafe(set);
				}

				// Token: 0x0600AE23 RID: 44579 RVA: 0x00268DE8 File Offset: 0x00266FE8
				public ProgramSetBuilder<fromNumber> fromNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600AE24 RID: 44580 RVA: 0x00268E40 File Offset: 0x00267040
				public ProgramSetBuilder<fromNumberCoalesced> fromNumberCoalesced(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromNumberCoalesced)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromNumberCoalesced but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumberCoalesced>.CreateUnsafe(set);
				}

				// Token: 0x0600AE25 RID: 44581 RVA: 0x00268E98 File Offset: 0x00267098
				public ProgramSetBuilder<fromRowNumber> fromRowNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromRowNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromRowNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromRowNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600AE26 RID: 44582 RVA: 0x00268EF0 File Offset: 0x002670F0
				public ProgramSetBuilder<fromNumbers> fromNumbers(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromNumbers)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromNumbers but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromNumbers>.CreateUnsafe(set);
				}

				// Token: 0x0600AE27 RID: 44583 RVA: 0x00268F48 File Offset: 0x00267148
				public ProgramSetBuilder<fromDateTime> fromDateTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromDateTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromDateTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTime>.CreateUnsafe(set);
				}

				// Token: 0x0600AE28 RID: 44584 RVA: 0x00268FA0 File Offset: 0x002671A0
				public ProgramSetBuilder<fromDateTimePart> fromDateTimePart(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromDateTimePart)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromDateTimePart but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePart>.CreateUnsafe(set);
				}

				// Token: 0x0600AE29 RID: 44585 RVA: 0x00268FF8 File Offset: 0x002671F8
				public ProgramSetBuilder<fromTime> fromTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromTime>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2A RID: 44586 RVA: 0x00269050 File Offset: 0x00267250
				public ProgramSetBuilder<constString> constString(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constString)
					{
						string text = "set";
						string text2 = "expected program set for symbol constString but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constString>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2B RID: 44587 RVA: 0x002690A8 File Offset: 0x002672A8
				public ProgramSetBuilder<constNumber> constNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol constNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2C RID: 44588 RVA: 0x00269100 File Offset: 0x00267300
				public ProgramSetBuilder<constDate> constDate(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constDate)
					{
						string text = "set";
						string text2 = "expected program set for symbol constDate but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDate>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2D RID: 44589 RVA: 0x00269158 File Offset: 0x00267358
				public ProgramSetBuilder<columnName> columnName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnName)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnName>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2E RID: 44590 RVA: 0x002691B0 File Offset: 0x002673B0
				public ProgramSetBuilder<columnNames> columnNames(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnNames)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnNames but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.columnNames>.CreateUnsafe(set);
				}

				// Token: 0x0600AE2F RID: 44591 RVA: 0x00269208 File Offset: 0x00267408
				public ProgramSetBuilder<constStr> constStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol constStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constStr>.CreateUnsafe(set);
				}

				// Token: 0x0600AE30 RID: 44592 RVA: 0x00269260 File Offset: 0x00267460
				public ProgramSetBuilder<constNum> constNum(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constNum)
					{
						string text = "set";
						string text2 = "expected program set for symbol constNum but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constNum>.CreateUnsafe(set);
				}

				// Token: 0x0600AE31 RID: 44593 RVA: 0x002692B8 File Offset: 0x002674B8
				public ProgramSetBuilder<constDt> constDt(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constDt)
					{
						string text = "set";
						string text2 = "expected program set for symbol constDt but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.constDt>.CreateUnsafe(set);
				}

				// Token: 0x0600AE32 RID: 44594 RVA: 0x00269310 File Offset: 0x00267510
				public ProgramSetBuilder<locale> locale(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.locale)
					{
						string text = "set";
						string text2 = "expected program set for symbol locale but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.locale>.CreateUnsafe(set);
				}

				// Token: 0x0600AE33 RID: 44595 RVA: 0x00269368 File Offset: 0x00267568
				public ProgramSetBuilder<replaceFindText> replaceFindText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.replaceFindText)
					{
						string text = "set";
						string text2 = "expected program set for symbol replaceFindText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceFindText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE34 RID: 44596 RVA: 0x002693C0 File Offset: 0x002675C0
				public ProgramSetBuilder<replaceText> replaceText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.replaceText)
					{
						string text = "set";
						string text2 = "expected program set for symbol replaceText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.replaceText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE35 RID: 44597 RVA: 0x00269418 File Offset: 0x00267618
				public ProgramSetBuilder<sliceBetweenStartText> sliceBetweenStartText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sliceBetweenStartText)
					{
						string text = "set";
						string text2 = "expected program set for symbol sliceBetweenStartText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenStartText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE36 RID: 44598 RVA: 0x00269470 File Offset: 0x00267670
				public ProgramSetBuilder<sliceBetweenEndText> sliceBetweenEndText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sliceBetweenEndText)
					{
						string text = "set";
						string text2 = "expected program set for symbol sliceBetweenEndText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.sliceBetweenEndText>.CreateUnsafe(set);
				}

				// Token: 0x0600AE37 RID: 44599 RVA: 0x002694C8 File Offset: 0x002676C8
				public ProgramSetBuilder<numberFormatDesc> numberFormatDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberFormatDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberFormatDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberFormatDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE38 RID: 44600 RVA: 0x00269520 File Offset: 0x00267720
				public ProgramSetBuilder<numberRoundDesc> numberRoundDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberRoundDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberRoundDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.numberRoundDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE39 RID: 44601 RVA: 0x00269578 File Offset: 0x00267778
				public ProgramSetBuilder<dateTimeRoundDesc> dateTimeRoundDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dateTimeRoundDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol dateTimeRoundDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeRoundDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3A RID: 44602 RVA: 0x002695D0 File Offset: 0x002677D0
				public ProgramSetBuilder<dateTimeFormatDesc> dateTimeFormatDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dateTimeFormatDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol dateTimeFormatDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeFormatDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3B RID: 44603 RVA: 0x00269628 File Offset: 0x00267828
				public ProgramSetBuilder<dateTimeParseDesc> dateTimeParseDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dateTimeParseDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol dateTimeParseDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimeParseDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3C RID: 44604 RVA: 0x00269680 File Offset: 0x00267880
				public ProgramSetBuilder<dateTimePartKind> dateTimePartKind(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dateTimePartKind)
					{
						string text = "set";
						string text2 = "expected program set for symbol dateTimePartKind but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.dateTimePartKind>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3D RID: 44605 RVA: 0x002696D8 File Offset: 0x002678D8
				public ProgramSetBuilder<fromDateTimePartKind> fromDateTimePartKind(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fromDateTimePartKind)
					{
						string text = "set";
						string text2 = "expected program set for symbol fromDateTimePartKind but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.fromDateTimePartKind>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3E RID: 44606 RVA: 0x00269730 File Offset: 0x00267930
				public ProgramSetBuilder<timePartKind> timePartKind(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.timePartKind)
					{
						string text = "set";
						string text2 = "expected program set for symbol timePartKind but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.timePartKind>.CreateUnsafe(set);
				}

				// Token: 0x0600AE3F RID: 44607 RVA: 0x00269788 File Offset: 0x00267988
				public ProgramSetBuilder<rowNumberLinearTransformDesc> rowNumberLinearTransformDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rowNumberLinearTransformDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol rowNumberLinearTransformDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.rowNumberLinearTransformDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE40 RID: 44608 RVA: 0x002697E0 File Offset: 0x002679E0
				public ProgramSetBuilder<matchDesc> matchDesc(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.matchDesc)
					{
						string text = "set";
						string text2 = "expected program set for symbol matchDesc but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchDesc>.CreateUnsafe(set);
				}

				// Token: 0x0600AE41 RID: 44609 RVA: 0x00269838 File Offset: 0x00267A38
				public ProgramSetBuilder<matchInstance> matchInstance(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.matchInstance)
					{
						string text = "set";
						string text2 = "expected program set for symbol matchInstance but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.matchInstance>.CreateUnsafe(set);
				}

				// Token: 0x0600AE42 RID: 44610 RVA: 0x00269890 File Offset: 0x00267A90
				public ProgramSetBuilder<splitDelimiter> splitDelimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitDelimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitDelimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitDelimiter>.CreateUnsafe(set);
				}

				// Token: 0x0600AE43 RID: 44611 RVA: 0x002698E8 File Offset: 0x00267AE8
				public ProgramSetBuilder<splitInstance> splitInstance(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitInstance)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitInstance but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.splitInstance>.CreateUnsafe(set);
				}

				// Token: 0x0600AE44 RID: 44612 RVA: 0x00269940 File Offset: 0x00267B40
				public ProgramSetBuilder<findDelimiter> findDelimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.findDelimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol findDelimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findDelimiter>.CreateUnsafe(set);
				}

				// Token: 0x0600AE45 RID: 44613 RVA: 0x00269998 File Offset: 0x00267B98
				public ProgramSetBuilder<findInstance> findInstance(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.findInstance)
					{
						string text = "set";
						string text2 = "expected program set for symbol findInstance but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findInstance>.CreateUnsafe(set);
				}

				// Token: 0x0600AE46 RID: 44614 RVA: 0x002699F0 File Offset: 0x00267BF0
				public ProgramSetBuilder<findOffset> findOffset(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.findOffset)
					{
						string text = "set";
						string text2 = "expected program set for symbol findOffset but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.findOffset>.CreateUnsafe(set);
				}

				// Token: 0x0600AE47 RID: 44615 RVA: 0x00269A48 File Offset: 0x00267C48
				public ProgramSetBuilder<slicePrefixAbsPos> slicePrefixAbsPos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.slicePrefixAbsPos)
					{
						string text = "set";
						string text2 = "expected program set for symbol slicePrefixAbsPos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.slicePrefixAbsPos>.CreateUnsafe(set);
				}

				// Token: 0x0600AE48 RID: 44616 RVA: 0x00269AA0 File Offset: 0x00267CA0
				public ProgramSetBuilder<scaleNumberFactor> scaleNumberFactor(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.scaleNumberFactor)
					{
						string text = "set";
						string text2 = "expected program set for symbol scaleNumberFactor but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.scaleNumberFactor>.CreateUnsafe(set);
				}

				// Token: 0x0600AE49 RID: 44617 RVA: 0x00269AF8 File Offset: 0x00267CF8
				public ProgramSetBuilder<absPos> absPos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.absPos)
					{
						string text = "set";
						string text2 = "expected program set for symbol absPos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes.absPos>.CreateUnsafe(set);
				}

				// Token: 0x040044F1 RID: 17649
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
