using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build
{
	// Token: 0x0200091A RID: 2330
	public class GrammarBuilders
	{
		// Token: 0x06003256 RID: 12886 RVA: 0x000A2F83 File Offset: 0x000A1183
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000A2FAF File Offset: 0x000A11AF
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000A2FBC File Offset: 0x000A11BC
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000A2FC9 File Offset: 0x000A11C9
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000A2FD6 File Offset: 0x000A11D6
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000A2FE3 File Offset: 0x000A11E3
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000A2FEB File Offset: 0x000A11EB
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000A2FF4 File Offset: 0x000A11F4
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000A2FFC File Offset: 0x000A11FC
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600325F RID: 12895 RVA: 0x000A3008 File Offset: 0x000A1208
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

		// Token: 0x04001925 RID: 6437
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04001926 RID: 6438
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04001927 RID: 6439
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04001928 RID: 6440
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04001929 RID: 6441
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x0200091B RID: 2331
		public class GrammarSymbols
		{
			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x06003261 RID: 12897 RVA: 0x000A30B3 File Offset: 0x000A12B3
			// (set) Token: 0x06003262 RID: 12898 RVA: 0x000A30BB File Offset: 0x000A12BB
			public Symbol file { get; private set; }

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06003263 RID: 12899 RVA: 0x000A30C4 File Offset: 0x000A12C4
			// (set) Token: 0x06003264 RID: 12900 RVA: 0x000A30CC File Offset: 0x000A12CC
			public Symbol hasHeader { get; private set; }

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x06003265 RID: 12901 RVA: 0x000A30D5 File Offset: 0x000A12D5
			// (set) Token: 0x06003266 RID: 12902 RVA: 0x000A30DD File Offset: 0x000A12DD
			public Symbol columnList { get; private set; }

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06003267 RID: 12903 RVA: 0x000A30E6 File Offset: 0x000A12E6
			// (set) Token: 0x06003268 RID: 12904 RVA: 0x000A30EE File Offset: 0x000A12EE
			public Symbol topSplit { get; private set; }

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06003269 RID: 12905 RVA: 0x000A30F7 File Offset: 0x000A12F7
			// (set) Token: 0x0600326A RID: 12906 RVA: 0x000A30FF File Offset: 0x000A12FF
			public Symbol records { get; private set; }

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x0600326B RID: 12907 RVA: 0x000A3108 File Offset: 0x000A1308
			// (set) Token: 0x0600326C RID: 12908 RVA: 0x000A3110 File Offset: 0x000A1310
			public Symbol splitRecordsSelect { get; private set; }

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x0600326D RID: 12909 RVA: 0x000A3119 File Offset: 0x000A1319
			// (set) Token: 0x0600326E RID: 12910 RVA: 0x000A3121 File Offset: 0x000A1321
			public Symbol splitRecords { get; private set; }

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x0600326F RID: 12911 RVA: 0x000A312A File Offset: 0x000A132A
			// (set) Token: 0x06003270 RID: 12912 RVA: 0x000A3132 File Offset: 0x000A1332
			public Symbol key { get; private set; }

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06003271 RID: 12913 RVA: 0x000A313B File Offset: 0x000A133B
			// (set) Token: 0x06003272 RID: 12914 RVA: 0x000A3143 File Offset: 0x000A1343
			public Symbol sep { get; private set; }

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06003273 RID: 12915 RVA: 0x000A314C File Offset: 0x000A134C
			// (set) Token: 0x06003274 RID: 12916 RVA: 0x000A3154 File Offset: 0x000A1354
			public Symbol newLineSep { get; private set; }

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06003275 RID: 12917 RVA: 0x000A315D File Offset: 0x000A135D
			// (set) Token: 0x06003276 RID: 12918 RVA: 0x000A3165 File Offset: 0x000A1365
			public Symbol fwPos { get; private set; }

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x06003277 RID: 12919 RVA: 0x000A316E File Offset: 0x000A136E
			// (set) Token: 0x06003278 RID: 12920 RVA: 0x000A3176 File Offset: 0x000A1376
			public Symbol multiRecordSplit { get; private set; }

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06003279 RID: 12921 RVA: 0x000A317F File Offset: 0x000A137F
			// (set) Token: 0x0600327A RID: 12922 RVA: 0x000A3187 File Offset: 0x000A1387
			public Symbol rowRecords { get; private set; }

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x0600327B RID: 12923 RVA: 0x000A3190 File Offset: 0x000A1390
			// (set) Token: 0x0600327C RID: 12924 RVA: 0x000A3198 File Offset: 0x000A1398
			public Symbol mapColumnSelectors { get; private set; }

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x0600327D RID: 12925 RVA: 0x000A31A1 File Offset: 0x000A13A1
			// (set) Token: 0x0600327E RID: 12926 RVA: 0x000A31A9 File Offset: 0x000A13A9
			public Symbol rowRecord { get; private set; }

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x0600327F RID: 12927 RVA: 0x000A31B2 File Offset: 0x000A13B2
			// (set) Token: 0x06003280 RID: 12928 RVA: 0x000A31BA File Offset: 0x000A13BA
			public Symbol columnSelectorList { get; private set; }

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x06003281 RID: 12929 RVA: 0x000A31C3 File Offset: 0x000A13C3
			// (set) Token: 0x06003282 RID: 12930 RVA: 0x000A31CB File Offset: 0x000A13CB
			public Symbol columnSelector { get; private set; }

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x06003283 RID: 12931 RVA: 0x000A31D4 File Offset: 0x000A13D4
			// (set) Token: 0x06003284 RID: 12932 RVA: 0x000A31DC File Offset: 0x000A13DC
			public Symbol primarySelector { get; private set; }

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x06003285 RID: 12933 RVA: 0x000A31E5 File Offset: 0x000A13E5
			// (set) Token: 0x06003286 RID: 12934 RVA: 0x000A31ED File Offset: 0x000A13ED
			public Symbol delimiterSplit { get; private set; }

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x06003287 RID: 12935 RVA: 0x000A31F6 File Offset: 0x000A13F6
			// (set) Token: 0x06003288 RID: 12936 RVA: 0x000A31FE File Offset: 0x000A13FE
			public Symbol record { get; private set; }

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x06003289 RID: 12937 RVA: 0x000A3207 File Offset: 0x000A1407
			// (set) Token: 0x0600328A RID: 12938 RVA: 0x000A320F File Offset: 0x000A140F
			public Symbol splitTextProg { get; private set; }

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x0600328B RID: 12939 RVA: 0x000A3218 File Offset: 0x000A1418
			// (set) Token: 0x0600328C RID: 12940 RVA: 0x000A3220 File Offset: 0x000A1420
			public Symbol splitFile { get; private set; }

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x0600328D RID: 12941 RVA: 0x000A3229 File Offset: 0x000A1429
			// (set) Token: 0x0600328E RID: 12942 RVA: 0x000A3231 File Offset: 0x000A1431
			public Symbol allLines { get; private set; }

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x0600328F RID: 12943 RVA: 0x000A323A File Offset: 0x000A143A
			// (set) Token: 0x06003290 RID: 12944 RVA: 0x000A3242 File Offset: 0x000A1442
			public Symbol r { get; private set; }

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06003291 RID: 12945 RVA: 0x000A324B File Offset: 0x000A144B
			// (set) Token: 0x06003292 RID: 12946 RVA: 0x000A3253 File Offset: 0x000A1453
			public Symbol k { get; private set; }

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x06003293 RID: 12947 RVA: 0x000A325C File Offset: 0x000A145C
			// (set) Token: 0x06003294 RID: 12948 RVA: 0x000A3264 File Offset: 0x000A1464
			public Symbol quotingConfig { get; private set; }

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x06003295 RID: 12949 RVA: 0x000A326D File Offset: 0x000A146D
			// (set) Token: 0x06003296 RID: 12950 RVA: 0x000A3275 File Offset: 0x000A1475
			public Symbol delimiter { get; private set; }

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x06003297 RID: 12951 RVA: 0x000A327E File Offset: 0x000A147E
			// (set) Token: 0x06003298 RID: 12952 RVA: 0x000A3286 File Offset: 0x000A1486
			public Symbol headerIndex { get; private set; }

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x06003299 RID: 12953 RVA: 0x000A328F File Offset: 0x000A148F
			// (set) Token: 0x0600329A RID: 12954 RVA: 0x000A3297 File Offset: 0x000A1497
			public Symbol commentStr { get; private set; }

			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x0600329B RID: 12955 RVA: 0x000A32A0 File Offset: 0x000A14A0
			// (set) Token: 0x0600329C RID: 12956 RVA: 0x000A32A8 File Offset: 0x000A14A8
			public Symbol skipEmpty { get; private set; }

			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x0600329D RID: 12957 RVA: 0x000A32B1 File Offset: 0x000A14B1
			// (set) Token: 0x0600329E RID: 12958 RVA: 0x000A32B9 File Offset: 0x000A14B9
			public Symbol hasCommentHeader { get; private set; }

			// Token: 0x17000916 RID: 2326
			// (get) Token: 0x0600329F RID: 12959 RVA: 0x000A32C2 File Offset: 0x000A14C2
			// (set) Token: 0x060032A0 RID: 12960 RVA: 0x000A32CA File Offset: 0x000A14CA
			public Symbol splitLines { get; private set; }

			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000A32D3 File Offset: 0x000A14D3
			// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000A32DB File Offset: 0x000A14DB
			public Symbol ls { get; private set; }

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000A32E4 File Offset: 0x000A14E4
			// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000A32EC File Offset: 0x000A14EC
			public Symbol dataLines { get; private set; }

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000A32F5 File Offset: 0x000A14F5
			// (set) Token: 0x060032A6 RID: 12966 RVA: 0x000A32FD File Offset: 0x000A14FD
			public Symbol s { get; private set; }

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000A3306 File Offset: 0x000A1506
			// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000A330E File Offset: 0x000A150E
			public Symbol skippedRecords { get; private set; }

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000A3317 File Offset: 0x000A1517
			// (set) Token: 0x060032AA RID: 12970 RVA: 0x000A331F File Offset: 0x000A151F
			public Symbol skippedFooter { get; private set; }

			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x060032AB RID: 12971 RVA: 0x000A3328 File Offset: 0x000A1528
			// (set) Token: 0x060032AC RID: 12972 RVA: 0x000A3330 File Offset: 0x000A1530
			public Symbol allRecords { get; private set; }

			// Token: 0x1700091D RID: 2333
			// (get) Token: 0x060032AD RID: 12973 RVA: 0x000A3339 File Offset: 0x000A1539
			// (set) Token: 0x060032AE RID: 12974 RVA: 0x000A3341 File Offset: 0x000A1541
			public Symbol basicLinePredicate { get; private set; }

			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x060032AF RID: 12975 RVA: 0x000A334A File Offset: 0x000A154A
			// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000A3352 File Offset: 0x000A1552
			public Symbol splitSequence { get; private set; }

			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000A335B File Offset: 0x000A155B
			// (set) Token: 0x060032B2 RID: 12978 RVA: 0x000A3363 File Offset: 0x000A1563
			public Symbol _LFun0 { get; private set; }

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000A336C File Offset: 0x000A156C
			// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000A3374 File Offset: 0x000A1574
			public Symbol _LFun1 { get; private set; }

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000A337D File Offset: 0x000A157D
			// (set) Token: 0x060032B6 RID: 12982 RVA: 0x000A3385 File Offset: 0x000A1585
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000A338E File Offset: 0x000A158E
			// (set) Token: 0x060032B8 RID: 12984 RVA: 0x000A3396 File Offset: 0x000A1596
			public Symbol _LetB1 { get; private set; }

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000A339F File Offset: 0x000A159F
			// (set) Token: 0x060032BA RID: 12986 RVA: 0x000A33A7 File Offset: 0x000A15A7
			public Symbol _LFun2 { get; private set; }

			// Token: 0x17000924 RID: 2340
			// (get) Token: 0x060032BB RID: 12987 RVA: 0x000A33B0 File Offset: 0x000A15B0
			// (set) Token: 0x060032BC RID: 12988 RVA: 0x000A33B8 File Offset: 0x000A15B8
			public Symbol _LFun3 { get; private set; }

			// Token: 0x060032BD RID: 12989 RVA: 0x000A33C4 File Offset: 0x000A15C4
			public GrammarSymbols(Grammar grammar)
			{
				this.file = grammar.Symbol("file");
				this.hasHeader = grammar.Symbol("hasHeader");
				this.columnList = grammar.Symbol("columnList");
				this.topSplit = grammar.Symbol("topSplit");
				this.records = grammar.Symbol("records");
				this.splitRecordsSelect = grammar.Symbol("splitRecordsSelect");
				this.splitRecords = grammar.Symbol("splitRecords");
				this.key = grammar.Symbol("key");
				this.sep = grammar.Symbol("sep");
				this.newLineSep = grammar.Symbol("newLineSep");
				this.fwPos = grammar.Symbol("fwPos");
				this.multiRecordSplit = grammar.Symbol("multiRecordSplit");
				this.rowRecords = grammar.Symbol("rowRecords");
				this.mapColumnSelectors = grammar.Symbol("mapColumnSelectors");
				this.rowRecord = grammar.Symbol("rowRecord");
				this.columnSelectorList = grammar.Symbol("columnSelectorList");
				this.columnSelector = grammar.Symbol("columnSelector");
				this.primarySelector = grammar.Symbol("primarySelector");
				this.delimiterSplit = grammar.Symbol("delimiterSplit");
				this.record = grammar.Symbol("record");
				this.splitTextProg = grammar.Symbol("splitTextProg");
				this.splitFile = grammar.Symbol("splitFile");
				this.allLines = grammar.Symbol("allLines");
				this.r = grammar.Symbol("r");
				this.k = grammar.Symbol("k");
				this.quotingConfig = grammar.Symbol("quotingConfig");
				this.delimiter = grammar.Symbol("delimiter");
				this.headerIndex = grammar.Symbol("headerIndex");
				this.commentStr = grammar.Symbol("commentStr");
				this.skipEmpty = grammar.Symbol("skipEmpty");
				this.hasCommentHeader = grammar.Symbol("hasCommentHeader");
				this.splitLines = grammar.Symbol("splitLines");
				this.ls = grammar.Symbol("ls");
				this.dataLines = grammar.Symbol("dataLines");
				this.s = grammar.Symbol("s");
				this.skippedRecords = grammar.Symbol("skippedRecords");
				this.skippedFooter = grammar.Symbol("skippedFooter");
				this.allRecords = grammar.Symbol("allRecords");
				this.basicLinePredicate = grammar.Symbol("basicLinePredicate");
				this.splitSequence = grammar.Symbol("splitSequence");
				this._LFun0 = grammar.Symbol("_LFun0");
				this._LFun1 = grammar.Symbol("_LFun1");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
				this._LFun2 = grammar.Symbol("_LFun2");
				this._LFun3 = grammar.Symbol("_LFun3");
			}
		}

		// Token: 0x0200091C RID: 2332
		public class GrammarRules
		{
			// Token: 0x17000925 RID: 2341
			// (get) Token: 0x060032BE RID: 12990 RVA: 0x000A36E5 File Offset: 0x000A18E5
			// (set) Token: 0x060032BF RID: 12991 RVA: 0x000A36ED File Offset: 0x000A18ED
			public BlackBoxRule SelectColumns { get; private set; }

			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000A36F6 File Offset: 0x000A18F6
			// (set) Token: 0x060032C1 RID: 12993 RVA: 0x000A36FE File Offset: 0x000A18FE
			public BlackBoxRule NoSplit { get; private set; }

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000A3707 File Offset: 0x000A1907
			// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000A370F File Offset: 0x000A190F
			public BlackBoxRule TableFromCells { get; private set; }

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000A3718 File Offset: 0x000A1918
			// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000A3720 File Offset: 0x000A1920
			public BlackBoxRule MultiRecordSplit { get; private set; }

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000A3729 File Offset: 0x000A1929
			// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000A3731 File Offset: 0x000A1931
			public BlackBoxRule Empty { get; private set; }

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000A373A File Offset: 0x000A193A
			// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000A3742 File Offset: 0x000A1942
			public BlackBoxRule SelectorList { get; private set; }

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x060032CA RID: 13002 RVA: 0x000A374B File Offset: 0x000A194B
			// (set) Token: 0x060032CB RID: 13003 RVA: 0x000A3753 File Offset: 0x000A1953
			public BlackBoxRule KthLine { get; private set; }

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x060032CC RID: 13004 RVA: 0x000A375C File Offset: 0x000A195C
			// (set) Token: 0x060032CD RID: 13005 RVA: 0x000A3764 File Offset: 0x000A1964
			public BlackBoxRule KthKeyValue { get; private set; }

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x060032CE RID: 13006 RVA: 0x000A376D File Offset: 0x000A196D
			// (set) Token: 0x060032CF RID: 13007 RVA: 0x000A3775 File Offset: 0x000A1975
			public BlackBoxRule KthTwoLineKeyValue { get; private set; }

			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000A377E File Offset: 0x000A197E
			// (set) Token: 0x060032D1 RID: 13009 RVA: 0x000A3786 File Offset: 0x000A1986
			public BlackBoxRule KthKeyQuote { get; private set; }

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000A378F File Offset: 0x000A198F
			// (set) Token: 0x060032D3 RID: 13011 RVA: 0x000A3797 File Offset: 0x000A1997
			public BlackBoxRule KthKeyValueFw { get; private set; }

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000A37A0 File Offset: 0x000A19A0
			// (set) Token: 0x060032D5 RID: 13013 RVA: 0x000A37A8 File Offset: 0x000A19A8
			public BlackBoxRule BreakLine { get; private set; }

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000A37B1 File Offset: 0x000A19B1
			// (set) Token: 0x060032D7 RID: 13015 RVA: 0x000A37B9 File Offset: 0x000A19B9
			public BlackBoxRule TwoLineKeyValue { get; private set; }

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000A37C2 File Offset: 0x000A19C2
			// (set) Token: 0x060032D9 RID: 13017 RVA: 0x000A37CA File Offset: 0x000A19CA
			public BlackBoxRule KeyValue { get; private set; }

			// Token: 0x17000933 RID: 2355
			// (get) Token: 0x060032DA RID: 13018 RVA: 0x000A37D3 File Offset: 0x000A19D3
			// (set) Token: 0x060032DB RID: 13019 RVA: 0x000A37DB File Offset: 0x000A19DB
			public BlackBoxRule KeyQuote { get; private set; }

			// Token: 0x17000934 RID: 2356
			// (get) Token: 0x060032DC RID: 13020 RVA: 0x000A37E4 File Offset: 0x000A19E4
			// (set) Token: 0x060032DD RID: 13021 RVA: 0x000A37EC File Offset: 0x000A19EC
			public BlackBoxRule SplitFile { get; private set; }

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x060032DE RID: 13022 RVA: 0x000A37F5 File Offset: 0x000A19F5
			// (set) Token: 0x060032DF RID: 13023 RVA: 0x000A37FD File Offset: 0x000A19FD
			public BlackBoxRule MergeRecordLines { get; private set; }

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000A3806 File Offset: 0x000A1A06
			// (set) Token: 0x060032E1 RID: 13025 RVA: 0x000A380E File Offset: 0x000A1A0E
			public BlackBoxRule FilterRecords { get; private set; }

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000A3817 File Offset: 0x000A1A17
			// (set) Token: 0x060032E3 RID: 13027 RVA: 0x000A381F File Offset: 0x000A1A1F
			public BlackBoxRule Skip { get; private set; }

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000A3828 File Offset: 0x000A1A28
			// (set) Token: 0x060032E5 RID: 13029 RVA: 0x000A3830 File Offset: 0x000A1A30
			public BlackBoxRule SkipFooter { get; private set; }

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000A3839 File Offset: 0x000A1A39
			// (set) Token: 0x060032E7 RID: 13031 RVA: 0x000A3841 File Offset: 0x000A1A41
			public BlackBoxRule QuoteRecords { get; private set; }

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000A384A File Offset: 0x000A1A4A
			// (set) Token: 0x060032E9 RID: 13033 RVA: 0x000A3852 File Offset: 0x000A1A52
			public BlackBoxRule StartsWith { get; private set; }

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x060032EA RID: 13034 RVA: 0x000A385B File Offset: 0x000A1A5B
			// (set) Token: 0x060032EB RID: 13035 RVA: 0x000A3863 File Offset: 0x000A1A63
			public BlackBoxRule SplitSequence { get; private set; }

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x060032EC RID: 13036 RVA: 0x000A386C File Offset: 0x000A1A6C
			// (set) Token: 0x060032ED RID: 13037 RVA: 0x000A3874 File Offset: 0x000A1A74
			public BlackBoxRule Sequence { get; private set; }

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x060032EE RID: 13038 RVA: 0x000A387D File Offset: 0x000A1A7D
			// (set) Token: 0x060032EF RID: 13039 RVA: 0x000A3885 File Offset: 0x000A1A85
			public ConceptRule MapColumnSelector { get; private set; }

			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000A388E File Offset: 0x000A1A8E
			// (set) Token: 0x060032F1 RID: 13041 RVA: 0x000A3896 File Offset: 0x000A1A96
			public ConceptRule SplitToCells { get; private set; }

			// Token: 0x1700093F RID: 2367
			// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000A389F File Offset: 0x000A1A9F
			// (set) Token: 0x060032F3 RID: 13043 RVA: 0x000A38A7 File Offset: 0x000A1AA7
			public ConceptRule FilterHeader { get; private set; }

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000A38B0 File Offset: 0x000A1AB0
			// (set) Token: 0x060032F5 RID: 13045 RVA: 0x000A38B8 File Offset: 0x000A1AB8
			public ConceptRule SelectData { get; private set; }

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000A38C1 File Offset: 0x000A1AC1
			// (set) Token: 0x060032F7 RID: 13047 RVA: 0x000A38C9 File Offset: 0x000A1AC9
			public ConversionRule SplitTextProg { get; private set; }

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000A38D2 File Offset: 0x000A1AD2
			// (set) Token: 0x060032F9 RID: 13049 RVA: 0x000A38DA File Offset: 0x000A1ADA
			public LetRule LetFileRecordSplit { get; private set; }

			// Token: 0x17000943 RID: 2371
			// (get) Token: 0x060032FA RID: 13050 RVA: 0x000A38E3 File Offset: 0x000A1AE3
			// (set) Token: 0x060032FB RID: 13051 RVA: 0x000A38EB File Offset: 0x000A1AEB
			public LetRule LetMultiRecordSplit { get; private set; }

			// Token: 0x17000944 RID: 2372
			// (get) Token: 0x060032FC RID: 13052 RVA: 0x000A38F4 File Offset: 0x000A1AF4
			// (set) Token: 0x060032FD RID: 13053 RVA: 0x000A38FC File Offset: 0x000A1AFC
			public LetRule LetSplitFile { get; private set; }

			// Token: 0x17000945 RID: 2373
			// (get) Token: 0x060032FE RID: 13054 RVA: 0x000A3905 File Offset: 0x000A1B05
			// (set) Token: 0x060032FF RID: 13055 RVA: 0x000A390D File Offset: 0x000A1B0D
			public LetRule SplitSequenceLet { get; private set; }

			// Token: 0x06003300 RID: 13056 RVA: 0x000A3918 File Offset: 0x000A1B18
			public GrammarRules(Grammar grammar)
			{
				this.SelectColumns = (BlackBoxRule)grammar.Rule("SelectColumns");
				this.NoSplit = (BlackBoxRule)grammar.Rule("NoSplit");
				this.TableFromCells = (BlackBoxRule)grammar.Rule("TableFromCells");
				this.MultiRecordSplit = (BlackBoxRule)grammar.Rule("MultiRecordSplit");
				this.Empty = (BlackBoxRule)grammar.Rule("Empty");
				this.SelectorList = (BlackBoxRule)grammar.Rule("SelectorList");
				this.KthLine = (BlackBoxRule)grammar.Rule("KthLine");
				this.KthKeyValue = (BlackBoxRule)grammar.Rule("KthKeyValue");
				this.KthTwoLineKeyValue = (BlackBoxRule)grammar.Rule("KthTwoLineKeyValue");
				this.KthKeyQuote = (BlackBoxRule)grammar.Rule("KthKeyQuote");
				this.KthKeyValueFw = (BlackBoxRule)grammar.Rule("KthKeyValueFw");
				this.BreakLine = (BlackBoxRule)grammar.Rule("BreakLine");
				this.TwoLineKeyValue = (BlackBoxRule)grammar.Rule("TwoLineKeyValue");
				this.KeyValue = (BlackBoxRule)grammar.Rule("KeyValue");
				this.KeyQuote = (BlackBoxRule)grammar.Rule("KeyQuote");
				this.SplitFile = (BlackBoxRule)grammar.Rule("SplitFile");
				this.MergeRecordLines = (BlackBoxRule)grammar.Rule("MergeRecordLines");
				this.FilterRecords = (BlackBoxRule)grammar.Rule("FilterRecords");
				this.Skip = (BlackBoxRule)grammar.Rule("Skip");
				this.SkipFooter = (BlackBoxRule)grammar.Rule("SkipFooter");
				this.QuoteRecords = (BlackBoxRule)grammar.Rule("QuoteRecords");
				this.StartsWith = (BlackBoxRule)grammar.Rule("StartsWith");
				this.SplitSequence = (BlackBoxRule)grammar.Rule("SplitSequence");
				this.Sequence = (BlackBoxRule)grammar.Rule("Sequence");
				this.MapColumnSelector = (ConceptRule)grammar.Rule("MapColumnSelector");
				this.SplitToCells = (ConceptRule)grammar.Rule("SplitToCells");
				this.FilterHeader = (ConceptRule)grammar.Rule("FilterHeader");
				this.SelectData = (ConceptRule)grammar.Rule("SelectData");
				this.SplitTextProg = (ConversionRule)grammar.Rule("SplitTextProg");
				this.LetFileRecordSplit = (LetRule)grammar.Rule("LetFileRecordSplit");
				this.LetMultiRecordSplit = (LetRule)grammar.Rule("LetMultiRecordSplit");
				this.LetSplitFile = (LetRule)grammar.Rule("LetSplitFile");
				this.SplitSequenceLet = (LetRule)grammar.Rule("SplitSequenceLet");
			}
		}

		// Token: 0x0200091D RID: 2333
		public class GrammarUnnamedConversions
		{
			// Token: 0x17000946 RID: 2374
			// (get) Token: 0x06003301 RID: 13057 RVA: 0x000A3C01 File Offset: 0x000A1E01
			// (set) Token: 0x06003302 RID: 13058 RVA: 0x000A3C09 File Offset: 0x000A1E09
			public ConversionRule splitRecordsSelect_splitRecords { get; private set; }

			// Token: 0x17000947 RID: 2375
			// (get) Token: 0x06003303 RID: 13059 RVA: 0x000A3C12 File Offset: 0x000A1E12
			// (set) Token: 0x06003304 RID: 13060 RVA: 0x000A3C1A File Offset: 0x000A1E1A
			public ConversionRule dataLines_skippedRecords { get; private set; }

			// Token: 0x17000948 RID: 2376
			// (get) Token: 0x06003305 RID: 13061 RVA: 0x000A3C23 File Offset: 0x000A1E23
			// (set) Token: 0x06003306 RID: 13062 RVA: 0x000A3C2B File Offset: 0x000A1E2B
			public ConversionRule skippedRecords_skippedFooter { get; private set; }

			// Token: 0x17000949 RID: 2377
			// (get) Token: 0x06003307 RID: 13063 RVA: 0x000A3C34 File Offset: 0x000A1E34
			// (set) Token: 0x06003308 RID: 13064 RVA: 0x000A3C3C File Offset: 0x000A1E3C
			public ConversionRule skippedFooter_allRecords { get; private set; }

			// Token: 0x1700094A RID: 2378
			// (get) Token: 0x06003309 RID: 13065 RVA: 0x000A3C45 File Offset: 0x000A1E45
			// (set) Token: 0x0600330A RID: 13066 RVA: 0x000A3C4D File Offset: 0x000A1E4D
			public ConversionRule allRecords_allLines { get; private set; }

			// Token: 0x0600330B RID: 13067 RVA: 0x000A3C58 File Offset: 0x000A1E58
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.splitRecordsSelect_splitRecords = (ConversionRule)grammar.Rule("~convert_splitRecordsSelect_splitRecords");
				this.dataLines_skippedRecords = (ConversionRule)grammar.Rule("~convert_dataLines_skippedRecords");
				this.skippedRecords_skippedFooter = (ConversionRule)grammar.Rule("~convert_skippedRecords_skippedFooter");
				this.skippedFooter_allRecords = (ConversionRule)grammar.Rule("~convert_skippedFooter_allRecords");
				this.allRecords_allLines = (ConversionRule)grammar.Rule("~convert_allRecords_allLines");
			}
		}

		// Token: 0x0200091E RID: 2334
		public class GrammarHoles
		{
			// Token: 0x1700094B RID: 2379
			// (get) Token: 0x0600330C RID: 13068 RVA: 0x000A3CD9 File Offset: 0x000A1ED9
			// (set) Token: 0x0600330D RID: 13069 RVA: 0x000A3CE1 File Offset: 0x000A1EE1
			public Hole file { get; private set; }

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x0600330E RID: 13070 RVA: 0x000A3CEA File Offset: 0x000A1EEA
			// (set) Token: 0x0600330F RID: 13071 RVA: 0x000A3CF2 File Offset: 0x000A1EF2
			public Hole hasHeader { get; private set; }

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x06003310 RID: 13072 RVA: 0x000A3CFB File Offset: 0x000A1EFB
			// (set) Token: 0x06003311 RID: 13073 RVA: 0x000A3D03 File Offset: 0x000A1F03
			public Hole columnList { get; private set; }

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x06003312 RID: 13074 RVA: 0x000A3D0C File Offset: 0x000A1F0C
			// (set) Token: 0x06003313 RID: 13075 RVA: 0x000A3D14 File Offset: 0x000A1F14
			public Hole topSplit { get; private set; }

			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x06003314 RID: 13076 RVA: 0x000A3D1D File Offset: 0x000A1F1D
			// (set) Token: 0x06003315 RID: 13077 RVA: 0x000A3D25 File Offset: 0x000A1F25
			public Hole records { get; private set; }

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x06003316 RID: 13078 RVA: 0x000A3D2E File Offset: 0x000A1F2E
			// (set) Token: 0x06003317 RID: 13079 RVA: 0x000A3D36 File Offset: 0x000A1F36
			public Hole splitRecordsSelect { get; private set; }

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x06003318 RID: 13080 RVA: 0x000A3D3F File Offset: 0x000A1F3F
			// (set) Token: 0x06003319 RID: 13081 RVA: 0x000A3D47 File Offset: 0x000A1F47
			public Hole splitRecords { get; private set; }

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x0600331A RID: 13082 RVA: 0x000A3D50 File Offset: 0x000A1F50
			// (set) Token: 0x0600331B RID: 13083 RVA: 0x000A3D58 File Offset: 0x000A1F58
			public Hole key { get; private set; }

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x0600331C RID: 13084 RVA: 0x000A3D61 File Offset: 0x000A1F61
			// (set) Token: 0x0600331D RID: 13085 RVA: 0x000A3D69 File Offset: 0x000A1F69
			public Hole sep { get; private set; }

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x0600331E RID: 13086 RVA: 0x000A3D72 File Offset: 0x000A1F72
			// (set) Token: 0x0600331F RID: 13087 RVA: 0x000A3D7A File Offset: 0x000A1F7A
			public Hole newLineSep { get; private set; }

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06003320 RID: 13088 RVA: 0x000A3D83 File Offset: 0x000A1F83
			// (set) Token: 0x06003321 RID: 13089 RVA: 0x000A3D8B File Offset: 0x000A1F8B
			public Hole fwPos { get; private set; }

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06003322 RID: 13090 RVA: 0x000A3D94 File Offset: 0x000A1F94
			// (set) Token: 0x06003323 RID: 13091 RVA: 0x000A3D9C File Offset: 0x000A1F9C
			public Hole multiRecordSplit { get; private set; }

			// Token: 0x17000957 RID: 2391
			// (get) Token: 0x06003324 RID: 13092 RVA: 0x000A3DA5 File Offset: 0x000A1FA5
			// (set) Token: 0x06003325 RID: 13093 RVA: 0x000A3DAD File Offset: 0x000A1FAD
			public Hole rowRecords { get; private set; }

			// Token: 0x17000958 RID: 2392
			// (get) Token: 0x06003326 RID: 13094 RVA: 0x000A3DB6 File Offset: 0x000A1FB6
			// (set) Token: 0x06003327 RID: 13095 RVA: 0x000A3DBE File Offset: 0x000A1FBE
			public Hole mapColumnSelectors { get; private set; }

			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06003328 RID: 13096 RVA: 0x000A3DC7 File Offset: 0x000A1FC7
			// (set) Token: 0x06003329 RID: 13097 RVA: 0x000A3DCF File Offset: 0x000A1FCF
			public Hole rowRecord { get; private set; }

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x0600332A RID: 13098 RVA: 0x000A3DD8 File Offset: 0x000A1FD8
			// (set) Token: 0x0600332B RID: 13099 RVA: 0x000A3DE0 File Offset: 0x000A1FE0
			public Hole columnSelectorList { get; private set; }

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x0600332C RID: 13100 RVA: 0x000A3DE9 File Offset: 0x000A1FE9
			// (set) Token: 0x0600332D RID: 13101 RVA: 0x000A3DF1 File Offset: 0x000A1FF1
			public Hole columnSelector { get; private set; }

			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x0600332E RID: 13102 RVA: 0x000A3DFA File Offset: 0x000A1FFA
			// (set) Token: 0x0600332F RID: 13103 RVA: 0x000A3E02 File Offset: 0x000A2002
			public Hole primarySelector { get; private set; }

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06003330 RID: 13104 RVA: 0x000A3E0B File Offset: 0x000A200B
			// (set) Token: 0x06003331 RID: 13105 RVA: 0x000A3E13 File Offset: 0x000A2013
			public Hole delimiterSplit { get; private set; }

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x06003332 RID: 13106 RVA: 0x000A3E1C File Offset: 0x000A201C
			// (set) Token: 0x06003333 RID: 13107 RVA: 0x000A3E24 File Offset: 0x000A2024
			public Hole record { get; private set; }

			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x06003334 RID: 13108 RVA: 0x000A3E2D File Offset: 0x000A202D
			// (set) Token: 0x06003335 RID: 13109 RVA: 0x000A3E35 File Offset: 0x000A2035
			public Hole splitTextProg { get; private set; }

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x06003336 RID: 13110 RVA: 0x000A3E3E File Offset: 0x000A203E
			// (set) Token: 0x06003337 RID: 13111 RVA: 0x000A3E46 File Offset: 0x000A2046
			public Hole splitFile { get; private set; }

			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x06003338 RID: 13112 RVA: 0x000A3E4F File Offset: 0x000A204F
			// (set) Token: 0x06003339 RID: 13113 RVA: 0x000A3E57 File Offset: 0x000A2057
			public Hole allLines { get; private set; }

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x0600333A RID: 13114 RVA: 0x000A3E60 File Offset: 0x000A2060
			// (set) Token: 0x0600333B RID: 13115 RVA: 0x000A3E68 File Offset: 0x000A2068
			public Hole r { get; private set; }

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x0600333C RID: 13116 RVA: 0x000A3E71 File Offset: 0x000A2071
			// (set) Token: 0x0600333D RID: 13117 RVA: 0x000A3E79 File Offset: 0x000A2079
			public Hole k { get; private set; }

			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x0600333E RID: 13118 RVA: 0x000A3E82 File Offset: 0x000A2082
			// (set) Token: 0x0600333F RID: 13119 RVA: 0x000A3E8A File Offset: 0x000A208A
			public Hole quotingConfig { get; private set; }

			// Token: 0x17000965 RID: 2405
			// (get) Token: 0x06003340 RID: 13120 RVA: 0x000A3E93 File Offset: 0x000A2093
			// (set) Token: 0x06003341 RID: 13121 RVA: 0x000A3E9B File Offset: 0x000A209B
			public Hole delimiter { get; private set; }

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x06003342 RID: 13122 RVA: 0x000A3EA4 File Offset: 0x000A20A4
			// (set) Token: 0x06003343 RID: 13123 RVA: 0x000A3EAC File Offset: 0x000A20AC
			public Hole headerIndex { get; private set; }

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x06003344 RID: 13124 RVA: 0x000A3EB5 File Offset: 0x000A20B5
			// (set) Token: 0x06003345 RID: 13125 RVA: 0x000A3EBD File Offset: 0x000A20BD
			public Hole commentStr { get; private set; }

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06003346 RID: 13126 RVA: 0x000A3EC6 File Offset: 0x000A20C6
			// (set) Token: 0x06003347 RID: 13127 RVA: 0x000A3ECE File Offset: 0x000A20CE
			public Hole skipEmpty { get; private set; }

			// Token: 0x17000969 RID: 2409
			// (get) Token: 0x06003348 RID: 13128 RVA: 0x000A3ED7 File Offset: 0x000A20D7
			// (set) Token: 0x06003349 RID: 13129 RVA: 0x000A3EDF File Offset: 0x000A20DF
			public Hole hasCommentHeader { get; private set; }

			// Token: 0x1700096A RID: 2410
			// (get) Token: 0x0600334A RID: 13130 RVA: 0x000A3EE8 File Offset: 0x000A20E8
			// (set) Token: 0x0600334B RID: 13131 RVA: 0x000A3EF0 File Offset: 0x000A20F0
			public Hole splitLines { get; private set; }

			// Token: 0x1700096B RID: 2411
			// (get) Token: 0x0600334C RID: 13132 RVA: 0x000A3EF9 File Offset: 0x000A20F9
			// (set) Token: 0x0600334D RID: 13133 RVA: 0x000A3F01 File Offset: 0x000A2101
			public Hole ls { get; private set; }

			// Token: 0x1700096C RID: 2412
			// (get) Token: 0x0600334E RID: 13134 RVA: 0x000A3F0A File Offset: 0x000A210A
			// (set) Token: 0x0600334F RID: 13135 RVA: 0x000A3F12 File Offset: 0x000A2112
			public Hole dataLines { get; private set; }

			// Token: 0x1700096D RID: 2413
			// (get) Token: 0x06003350 RID: 13136 RVA: 0x000A3F1B File Offset: 0x000A211B
			// (set) Token: 0x06003351 RID: 13137 RVA: 0x000A3F23 File Offset: 0x000A2123
			public Hole s { get; private set; }

			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x06003352 RID: 13138 RVA: 0x000A3F2C File Offset: 0x000A212C
			// (set) Token: 0x06003353 RID: 13139 RVA: 0x000A3F34 File Offset: 0x000A2134
			public Hole skippedRecords { get; private set; }

			// Token: 0x1700096F RID: 2415
			// (get) Token: 0x06003354 RID: 13140 RVA: 0x000A3F3D File Offset: 0x000A213D
			// (set) Token: 0x06003355 RID: 13141 RVA: 0x000A3F45 File Offset: 0x000A2145
			public Hole skippedFooter { get; private set; }

			// Token: 0x17000970 RID: 2416
			// (get) Token: 0x06003356 RID: 13142 RVA: 0x000A3F4E File Offset: 0x000A214E
			// (set) Token: 0x06003357 RID: 13143 RVA: 0x000A3F56 File Offset: 0x000A2156
			public Hole allRecords { get; private set; }

			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x06003358 RID: 13144 RVA: 0x000A3F5F File Offset: 0x000A215F
			// (set) Token: 0x06003359 RID: 13145 RVA: 0x000A3F67 File Offset: 0x000A2167
			public Hole basicLinePredicate { get; private set; }

			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x0600335A RID: 13146 RVA: 0x000A3F70 File Offset: 0x000A2170
			// (set) Token: 0x0600335B RID: 13147 RVA: 0x000A3F78 File Offset: 0x000A2178
			public Hole splitSequence { get; private set; }

			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x0600335C RID: 13148 RVA: 0x000A3F81 File Offset: 0x000A2181
			// (set) Token: 0x0600335D RID: 13149 RVA: 0x000A3F89 File Offset: 0x000A2189
			public Hole _LFun0 { get; private set; }

			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x0600335E RID: 13150 RVA: 0x000A3F92 File Offset: 0x000A2192
			// (set) Token: 0x0600335F RID: 13151 RVA: 0x000A3F9A File Offset: 0x000A219A
			public Hole _LFun1 { get; private set; }

			// Token: 0x17000975 RID: 2421
			// (get) Token: 0x06003360 RID: 13152 RVA: 0x000A3FA3 File Offset: 0x000A21A3
			// (set) Token: 0x06003361 RID: 13153 RVA: 0x000A3FAB File Offset: 0x000A21AB
			public Hole _LetB0 { get; private set; }

			// Token: 0x17000976 RID: 2422
			// (get) Token: 0x06003362 RID: 13154 RVA: 0x000A3FB4 File Offset: 0x000A21B4
			// (set) Token: 0x06003363 RID: 13155 RVA: 0x000A3FBC File Offset: 0x000A21BC
			public Hole _LetB1 { get; private set; }

			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x06003364 RID: 13156 RVA: 0x000A3FC5 File Offset: 0x000A21C5
			// (set) Token: 0x06003365 RID: 13157 RVA: 0x000A3FCD File Offset: 0x000A21CD
			public Hole _LFun2 { get; private set; }

			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x06003366 RID: 13158 RVA: 0x000A3FD6 File Offset: 0x000A21D6
			// (set) Token: 0x06003367 RID: 13159 RVA: 0x000A3FDE File Offset: 0x000A21DE
			public Hole _LFun3 { get; private set; }

			// Token: 0x06003368 RID: 13160 RVA: 0x000A3FE8 File Offset: 0x000A21E8
			public GrammarHoles(GrammarBuilders builders)
			{
				this.file = new Hole(builders.Symbol.file, null);
				this.hasHeader = new Hole(builders.Symbol.hasHeader, null);
				this.columnList = new Hole(builders.Symbol.columnList, null);
				this.topSplit = new Hole(builders.Symbol.topSplit, null);
				this.records = new Hole(builders.Symbol.records, null);
				this.splitRecordsSelect = new Hole(builders.Symbol.splitRecordsSelect, null);
				this.splitRecords = new Hole(builders.Symbol.splitRecords, null);
				this.key = new Hole(builders.Symbol.key, null);
				this.sep = new Hole(builders.Symbol.sep, null);
				this.newLineSep = new Hole(builders.Symbol.newLineSep, null);
				this.fwPos = new Hole(builders.Symbol.fwPos, null);
				this.multiRecordSplit = new Hole(builders.Symbol.multiRecordSplit, null);
				this.rowRecords = new Hole(builders.Symbol.rowRecords, null);
				this.mapColumnSelectors = new Hole(builders.Symbol.mapColumnSelectors, null);
				this.rowRecord = new Hole(builders.Symbol.rowRecord, null);
				this.columnSelectorList = new Hole(builders.Symbol.columnSelectorList, null);
				this.columnSelector = new Hole(builders.Symbol.columnSelector, null);
				this.primarySelector = new Hole(builders.Symbol.primarySelector, null);
				this.delimiterSplit = new Hole(builders.Symbol.delimiterSplit, null);
				this.record = new Hole(builders.Symbol.record, null);
				this.splitTextProg = new Hole(builders.Symbol.splitTextProg, null);
				this.splitFile = new Hole(builders.Symbol.splitFile, null);
				this.allLines = new Hole(builders.Symbol.allLines, null);
				this.r = new Hole(builders.Symbol.r, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.quotingConfig = new Hole(builders.Symbol.quotingConfig, null);
				this.delimiter = new Hole(builders.Symbol.delimiter, null);
				this.headerIndex = new Hole(builders.Symbol.headerIndex, null);
				this.commentStr = new Hole(builders.Symbol.commentStr, null);
				this.skipEmpty = new Hole(builders.Symbol.skipEmpty, null);
				this.hasCommentHeader = new Hole(builders.Symbol.hasCommentHeader, null);
				this.splitLines = new Hole(builders.Symbol.splitLines, null);
				this.ls = new Hole(builders.Symbol.ls, null);
				this.dataLines = new Hole(builders.Symbol.dataLines, null);
				this.s = new Hole(builders.Symbol.s, null);
				this.skippedRecords = new Hole(builders.Symbol.skippedRecords, null);
				this.skippedFooter = new Hole(builders.Symbol.skippedFooter, null);
				this.allRecords = new Hole(builders.Symbol.allRecords, null);
				this.basicLinePredicate = new Hole(builders.Symbol.basicLinePredicate, null);
				this.splitSequence = new Hole(builders.Symbol.splitSequence, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
				this._LFun1 = new Hole(builders.Symbol._LFun1, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
				this._LFun2 = new Hole(builders.Symbol._LFun2, null);
				this._LFun3 = new Hole(builders.Symbol._LFun3, null);
			}
		}

		// Token: 0x0200091F RID: 2335
		public class Nodes
		{
			// Token: 0x06003369 RID: 13161 RVA: 0x000A4420 File Offset: 0x000A2620
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

			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x0600336A RID: 13162 RVA: 0x000A4503 File Offset: 0x000A2703
			// (set) Token: 0x0600336B RID: 13163 RVA: 0x000A450B File Offset: 0x000A270B
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x0600336C RID: 13164 RVA: 0x000A4514 File Offset: 0x000A2714
			// (set) Token: 0x0600336D RID: 13165 RVA: 0x000A451C File Offset: 0x000A271C
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x0600336E RID: 13166 RVA: 0x000A4525 File Offset: 0x000A2725
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x0600336F RID: 13167 RVA: 0x000A4532 File Offset: 0x000A2732
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x06003370 RID: 13168 RVA: 0x000A453F File Offset: 0x000A273F
			// (set) Token: 0x06003371 RID: 13169 RVA: 0x000A4547 File Offset: 0x000A2747
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x1700097E RID: 2430
			// (get) Token: 0x06003372 RID: 13170 RVA: 0x000A4550 File Offset: 0x000A2750
			// (set) Token: 0x06003373 RID: 13171 RVA: 0x000A4558 File Offset: 0x000A2758
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x06003374 RID: 13172 RVA: 0x000A4561 File Offset: 0x000A2761
			// (set) Token: 0x06003375 RID: 13173 RVA: 0x000A4569 File Offset: 0x000A2769
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x06003376 RID: 13174 RVA: 0x000A4572 File Offset: 0x000A2772
			// (set) Token: 0x06003377 RID: 13175 RVA: 0x000A457A File Offset: 0x000A277A
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x06003378 RID: 13176 RVA: 0x000A4583 File Offset: 0x000A2783
			// (set) Token: 0x06003379 RID: 13177 RVA: 0x000A458B File Offset: 0x000A278B
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x0600337A RID: 13178 RVA: 0x000A4594 File Offset: 0x000A2794
			// (set) Token: 0x0600337B RID: 13179 RVA: 0x000A459C File Offset: 0x000A279C
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x0600337C RID: 13180 RVA: 0x000A45A5 File Offset: 0x000A27A5
			// (set) Token: 0x0600337D RID: 13181 RVA: 0x000A45AD File Offset: 0x000A27AD
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x040019B0 RID: 6576
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x040019B1 RID: 6577
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000920 RID: 2336
			public class NodeRules
			{
				// Token: 0x0600337E RID: 13182 RVA: 0x000A45B6 File Offset: 0x000A27B6
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600337F RID: 13183 RVA: 0x000A45C5 File Offset: 0x000A27C5
				public hasHeader hasHeader(bool value)
				{
					return new hasHeader(this._builders, value);
				}

				// Token: 0x06003380 RID: 13184 RVA: 0x000A45D3 File Offset: 0x000A27D3
				public columnList columnList(int[] value)
				{
					return new columnList(this._builders, value);
				}

				// Token: 0x06003381 RID: 13185 RVA: 0x000A45E1 File Offset: 0x000A27E1
				public key key(string value)
				{
					return new key(this._builders, value);
				}

				// Token: 0x06003382 RID: 13186 RVA: 0x000A45EF File Offset: 0x000A27EF
				public sep sep(string value)
				{
					return new sep(this._builders, value);
				}

				// Token: 0x06003383 RID: 13187 RVA: 0x000A45FD File Offset: 0x000A27FD
				public newLineSep newLineSep(string value)
				{
					return new newLineSep(this._builders, value);
				}

				// Token: 0x06003384 RID: 13188 RVA: 0x000A460B File Offset: 0x000A280B
				public fwPos fwPos(int value)
				{
					return new fwPos(this._builders, value);
				}

				// Token: 0x06003385 RID: 13189 RVA: 0x000A4619 File Offset: 0x000A2819
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r r(RegularExpression value)
				{
					return new Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r(this._builders, value);
				}

				// Token: 0x06003386 RID: 13190 RVA: 0x000A4627 File Offset: 0x000A2827
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x06003387 RID: 13191 RVA: 0x000A4635 File Offset: 0x000A2835
				public quotingConfig quotingConfig(QuotingConfiguration value)
				{
					return new quotingConfig(this._builders, value);
				}

				// Token: 0x06003388 RID: 13192 RVA: 0x000A4643 File Offset: 0x000A2843
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter delimiter(Optional<string> value)
				{
					return new Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter(this._builders, value);
				}

				// Token: 0x06003389 RID: 13193 RVA: 0x000A4651 File Offset: 0x000A2851
				public headerIndex headerIndex(Optional<int> value)
				{
					return new headerIndex(this._builders, value);
				}

				// Token: 0x0600338A RID: 13194 RVA: 0x000A465F File Offset: 0x000A285F
				public commentStr commentStr(Optional<string> value)
				{
					return new commentStr(this._builders, value);
				}

				// Token: 0x0600338B RID: 13195 RVA: 0x000A466D File Offset: 0x000A286D
				public skipEmpty skipEmpty(bool value)
				{
					return new skipEmpty(this._builders, value);
				}

				// Token: 0x0600338C RID: 13196 RVA: 0x000A467B File Offset: 0x000A287B
				public hasCommentHeader hasCommentHeader(bool value)
				{
					return new hasCommentHeader(this._builders, value);
				}

				// Token: 0x0600338D RID: 13197 RVA: 0x000A4689 File Offset: 0x000A2889
				public splitRecordsSelect SelectColumns(columnList value0, splitRecords value1)
				{
					return new SelectColumns(this._builders, value0, value1);
				}

				// Token: 0x0600338E RID: 13198 RVA: 0x000A469D File Offset: 0x000A289D
				public splitRecords NoSplit(records value0, hasHeader value1)
				{
					return new NoSplit(this._builders, value0, value1);
				}

				// Token: 0x0600338F RID: 13199 RVA: 0x000A46B1 File Offset: 0x000A28B1
				public splitRecords TableFromCells(delimiterSplit value0, hasHeader value1)
				{
					return new TableFromCells(this._builders, value0, value1);
				}

				// Token: 0x06003390 RID: 13200 RVA: 0x000A46C5 File Offset: 0x000A28C5
				public splitRecords MultiRecordSplit(multiRecordSplit value0)
				{
					return new MultiRecordSplit(this._builders, value0);
				}

				// Token: 0x06003391 RID: 13201 RVA: 0x000A46D8 File Offset: 0x000A28D8
				public columnSelectorList Empty()
				{
					return new Empty(this._builders);
				}

				// Token: 0x06003392 RID: 13202 RVA: 0x000A46EA File Offset: 0x000A28EA
				public columnSelectorList SelectorList(columnSelector value0, columnSelectorList value1)
				{
					return new SelectorList(this._builders, value0, value1);
				}

				// Token: 0x06003393 RID: 13203 RVA: 0x000A46FE File Offset: 0x000A28FE
				public columnSelector KthLine(k value0, rowRecord value1)
				{
					return new KthLine(this._builders, value0, value1);
				}

				// Token: 0x06003394 RID: 13204 RVA: 0x000A4712 File Offset: 0x000A2912
				public columnSelector KthKeyValue(key value0, sep value1, k value2, rowRecord value3)
				{
					return new KthKeyValue(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06003395 RID: 13205 RVA: 0x000A4729 File Offset: 0x000A2929
				public columnSelector KthTwoLineKeyValue(key value0, sep value1, k value2, rowRecord value3)
				{
					return new KthTwoLineKeyValue(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06003396 RID: 13206 RVA: 0x000A4740 File Offset: 0x000A2940
				public columnSelector KthKeyQuote(key value0, k value1, newLineSep value2, rowRecord value3)
				{
					return new KthKeyQuote(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06003397 RID: 13207 RVA: 0x000A4757 File Offset: 0x000A2957
				public columnSelector KthKeyValueFw(key value0, fwPos value1, k value2, newLineSep value3, rowRecord value4)
				{
					return new KthKeyValueFw(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x06003398 RID: 13208 RVA: 0x000A4770 File Offset: 0x000A2970
				public primarySelector BreakLine(records value0)
				{
					return new BreakLine(this._builders, value0);
				}

				// Token: 0x06003399 RID: 13209 RVA: 0x000A4783 File Offset: 0x000A2983
				public primarySelector TwoLineKeyValue(key value0, sep value1, records value2)
				{
					return new TwoLineKeyValue(this._builders, value0, value1, value2);
				}

				// Token: 0x0600339A RID: 13210 RVA: 0x000A4798 File Offset: 0x000A2998
				public primarySelector KeyValue(key value0, sep value1, records value2)
				{
					return new KeyValue(this._builders, value0, value1, value2);
				}

				// Token: 0x0600339B RID: 13211 RVA: 0x000A47AD File Offset: 0x000A29AD
				public primarySelector KeyQuote(key value0, records value1)
				{
					return new KeyQuote(this._builders, value0, value1);
				}

				// Token: 0x0600339C RID: 13212 RVA: 0x000A47C1 File Offset: 0x000A29C1
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 SplitFile(file value0)
				{
					return new SplitFile(this._builders, value0);
				}

				// Token: 0x0600339D RID: 13213 RVA: 0x000A47D4 File Offset: 0x000A29D4
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 MergeRecordLines(splitLines value0)
				{
					return new MergeRecordLines(this._builders, value0);
				}

				// Token: 0x0600339E RID: 13214 RVA: 0x000A47E7 File Offset: 0x000A29E7
				public dataLines FilterRecords(skipEmpty value0, Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter value1, commentStr value2, hasCommentHeader value3, skippedRecords value4)
				{
					return new FilterRecords(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x0600339F RID: 13215 RVA: 0x000A4800 File Offset: 0x000A2A00
				public skippedRecords Skip(k value0, headerIndex value1, skippedFooter value2)
				{
					return new Skip(this._builders, value0, value1, value2);
				}

				// Token: 0x060033A0 RID: 13216 RVA: 0x000A4815 File Offset: 0x000A2A15
				public skippedFooter SkipFooter(k value0, allRecords value1)
				{
					return new SkipFooter(this._builders, value0, value1);
				}

				// Token: 0x060033A1 RID: 13217 RVA: 0x000A4829 File Offset: 0x000A2A29
				public allRecords QuoteRecords(quotingConfig value0, Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter value1, allLines value2)
				{
					return new QuoteRecords(this._builders, value0, value1, value2);
				}

				// Token: 0x060033A2 RID: 13218 RVA: 0x000A483E File Offset: 0x000A2A3E
				public basicLinePredicate StartsWith(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s value0, Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r value1)
				{
					return new StartsWith(this._builders, value0, value1);
				}

				// Token: 0x060033A3 RID: 13219 RVA: 0x000A4852 File Offset: 0x000A2A52
				public splitSequence SplitSequence(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r value0, ls value1)
				{
					return new SplitSequence(this._builders, value0, value1);
				}

				// Token: 0x060033A4 RID: 13220 RVA: 0x000A4866 File Offset: 0x000A2A66
				public splitSequence Sequence(ls value0)
				{
					return new Sequence(this._builders, value0);
				}

				// Token: 0x060033A5 RID: 13221 RVA: 0x000A4879 File Offset: 0x000A2A79
				public mapColumnSelectors MapColumnSelector(columnSelectorList value0, rowRecords value1)
				{
					return new MapColumnSelector(this._builders, value0, value1);
				}

				// Token: 0x060033A6 RID: 13222 RVA: 0x000A488D File Offset: 0x000A2A8D
				public delimiterSplit SplitToCells(splitTextProg value0, records value1)
				{
					return new SplitToCells(this._builders, value0, value1);
				}

				// Token: 0x060033A7 RID: 13223 RVA: 0x000A48A1 File Offset: 0x000A2AA1
				public dataLines FilterHeader(basicLinePredicate value0, skippedRecords value1)
				{
					return new FilterHeader(this._builders, value0, value1);
				}

				// Token: 0x060033A8 RID: 13224 RVA: 0x000A48B5 File Offset: 0x000A2AB5
				public dataLines SelectData(basicLinePredicate value0, skippedRecords value1)
				{
					return new SelectData(this._builders, value0, value1);
				}

				// Token: 0x060033A9 RID: 13225 RVA: 0x000A48C9 File Offset: 0x000A2AC9
				public splitTextProg SplitTextProg(regionSplit value0)
				{
					return new SplitTextProg(this._builders, value0);
				}

				// Token: 0x060033AA RID: 13226 RVA: 0x000A48DC File Offset: 0x000A2ADC
				public topSplit LetFileRecordSplit(splitFile value0, splitRecordsSelect value1)
				{
					return new LetFileRecordSplit(this._builders, value0, value1);
				}

				// Token: 0x060033AB RID: 13227 RVA: 0x000A48F0 File Offset: 0x000A2AF0
				public multiRecordSplit LetMultiRecordSplit(primarySelector value0, mapColumnSelectors value1)
				{
					return new LetMultiRecordSplit(this._builders, value0, value1);
				}

				// Token: 0x060033AC RID: 13228 RVA: 0x000A4904 File Offset: 0x000A2B04
				public splitFile LetSplitFile(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 value0, Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 value1)
				{
					return new LetSplitFile(this._builders, value0, value1);
				}

				// Token: 0x060033AD RID: 13229 RVA: 0x000A4918 File Offset: 0x000A2B18
				public splitLines SplitSequenceLet(dataLines value0, splitSequence value1)
				{
					return new SplitSequenceLet(this._builders, value0, value1);
				}

				// Token: 0x040019B9 RID: 6585
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000921 RID: 2337
			public class NodeUnnamedConversionRules
			{
				// Token: 0x060033AE RID: 13230 RVA: 0x000A492C File Offset: 0x000A2B2C
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060033AF RID: 13231 RVA: 0x000A493B File Offset: 0x000A2B3B
				public splitRecordsSelect splitRecordsSelect_splitRecords(splitRecords value0)
				{
					return new splitRecordsSelect_splitRecords(this._builders, value0);
				}

				// Token: 0x060033B0 RID: 13232 RVA: 0x000A494E File Offset: 0x000A2B4E
				public dataLines dataLines_skippedRecords(skippedRecords value0)
				{
					return new dataLines_skippedRecords(this._builders, value0);
				}

				// Token: 0x060033B1 RID: 13233 RVA: 0x000A4961 File Offset: 0x000A2B61
				public skippedRecords skippedRecords_skippedFooter(skippedFooter value0)
				{
					return new skippedRecords_skippedFooter(this._builders, value0);
				}

				// Token: 0x060033B2 RID: 13234 RVA: 0x000A4974 File Offset: 0x000A2B74
				public skippedFooter skippedFooter_allRecords(allRecords value0)
				{
					return new skippedFooter_allRecords(this._builders, value0);
				}

				// Token: 0x060033B3 RID: 13235 RVA: 0x000A4987 File Offset: 0x000A2B87
				public allRecords allRecords_allLines(allLines value0)
				{
					return new allRecords_allLines(this._builders, value0);
				}

				// Token: 0x040019BA RID: 6586
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000922 RID: 2338
			public class NodeVariables
			{
				// Token: 0x17000984 RID: 2436
				// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000A499A File Offset: 0x000A2B9A
				// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000A49A2 File Offset: 0x000A2BA2
				public file file { get; private set; }

				// Token: 0x17000985 RID: 2437
				// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000A49AB File Offset: 0x000A2BAB
				// (set) Token: 0x060033B7 RID: 13239 RVA: 0x000A49B3 File Offset: 0x000A2BB3
				public records records { get; private set; }

				// Token: 0x17000986 RID: 2438
				// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000A49BC File Offset: 0x000A2BBC
				// (set) Token: 0x060033B9 RID: 13241 RVA: 0x000A49C4 File Offset: 0x000A2BC4
				public rowRecords rowRecords { get; private set; }

				// Token: 0x17000987 RID: 2439
				// (get) Token: 0x060033BA RID: 13242 RVA: 0x000A49CD File Offset: 0x000A2BCD
				// (set) Token: 0x060033BB RID: 13243 RVA: 0x000A49D5 File Offset: 0x000A2BD5
				public rowRecord rowRecord { get; private set; }

				// Token: 0x17000988 RID: 2440
				// (get) Token: 0x060033BC RID: 13244 RVA: 0x000A49DE File Offset: 0x000A2BDE
				// (set) Token: 0x060033BD RID: 13245 RVA: 0x000A49E6 File Offset: 0x000A2BE6
				public record record { get; private set; }

				// Token: 0x17000989 RID: 2441
				// (get) Token: 0x060033BE RID: 13246 RVA: 0x000A49EF File Offset: 0x000A2BEF
				// (set) Token: 0x060033BF RID: 13247 RVA: 0x000A49F7 File Offset: 0x000A2BF7
				public allLines allLines { get; private set; }

				// Token: 0x1700098A RID: 2442
				// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000A4A00 File Offset: 0x000A2C00
				// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000A4A08 File Offset: 0x000A2C08
				public ls ls { get; private set; }

				// Token: 0x1700098B RID: 2443
				// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000A4A11 File Offset: 0x000A2C11
				// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000A4A19 File Offset: 0x000A2C19
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s s { get; private set; }

				// Token: 0x060033C4 RID: 13252 RVA: 0x000A4A24 File Offset: 0x000A2C24
				public NodeVariables(GrammarBuilders builders)
				{
					this.file = new file(builders);
					this.records = new records(builders);
					this.rowRecords = new rowRecords(builders);
					this.rowRecord = new rowRecord(builders);
					this.record = new record(builders);
					this.allLines = new allLines(builders);
					this.ls = new ls(builders);
					this.s = new Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s(builders);
				}
			}

			// Token: 0x02000923 RID: 2339
			public class NodeHoles
			{
				// Token: 0x1700098C RID: 2444
				// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000A4A97 File Offset: 0x000A2C97
				// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000A4A9F File Offset: 0x000A2C9F
				public hasHeader hasHeader { get; private set; }

				// Token: 0x1700098D RID: 2445
				// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000A4AA8 File Offset: 0x000A2CA8
				// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000A4AB0 File Offset: 0x000A2CB0
				public columnList columnList { get; private set; }

				// Token: 0x1700098E RID: 2446
				// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000A4AB9 File Offset: 0x000A2CB9
				// (set) Token: 0x060033CA RID: 13258 RVA: 0x000A4AC1 File Offset: 0x000A2CC1
				public topSplit topSplit { get; private set; }

				// Token: 0x1700098F RID: 2447
				// (get) Token: 0x060033CB RID: 13259 RVA: 0x000A4ACA File Offset: 0x000A2CCA
				// (set) Token: 0x060033CC RID: 13260 RVA: 0x000A4AD2 File Offset: 0x000A2CD2
				public records records { get; private set; }

				// Token: 0x17000990 RID: 2448
				// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A4ADB File Offset: 0x000A2CDB
				// (set) Token: 0x060033CE RID: 13262 RVA: 0x000A4AE3 File Offset: 0x000A2CE3
				public splitRecordsSelect splitRecordsSelect { get; private set; }

				// Token: 0x17000991 RID: 2449
				// (get) Token: 0x060033CF RID: 13263 RVA: 0x000A4AEC File Offset: 0x000A2CEC
				// (set) Token: 0x060033D0 RID: 13264 RVA: 0x000A4AF4 File Offset: 0x000A2CF4
				public splitRecords splitRecords { get; private set; }

				// Token: 0x17000992 RID: 2450
				// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000A4AFD File Offset: 0x000A2CFD
				// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000A4B05 File Offset: 0x000A2D05
				public key key { get; private set; }

				// Token: 0x17000993 RID: 2451
				// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000A4B0E File Offset: 0x000A2D0E
				// (set) Token: 0x060033D4 RID: 13268 RVA: 0x000A4B16 File Offset: 0x000A2D16
				public sep sep { get; private set; }

				// Token: 0x17000994 RID: 2452
				// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000A4B1F File Offset: 0x000A2D1F
				// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000A4B27 File Offset: 0x000A2D27
				public newLineSep newLineSep { get; private set; }

				// Token: 0x17000995 RID: 2453
				// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000A4B30 File Offset: 0x000A2D30
				// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000A4B38 File Offset: 0x000A2D38
				public fwPos fwPos { get; private set; }

				// Token: 0x17000996 RID: 2454
				// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000A4B41 File Offset: 0x000A2D41
				// (set) Token: 0x060033DA RID: 13274 RVA: 0x000A4B49 File Offset: 0x000A2D49
				public multiRecordSplit multiRecordSplit { get; private set; }

				// Token: 0x17000997 RID: 2455
				// (get) Token: 0x060033DB RID: 13275 RVA: 0x000A4B52 File Offset: 0x000A2D52
				// (set) Token: 0x060033DC RID: 13276 RVA: 0x000A4B5A File Offset: 0x000A2D5A
				public rowRecords rowRecords { get; private set; }

				// Token: 0x17000998 RID: 2456
				// (get) Token: 0x060033DD RID: 13277 RVA: 0x000A4B63 File Offset: 0x000A2D63
				// (set) Token: 0x060033DE RID: 13278 RVA: 0x000A4B6B File Offset: 0x000A2D6B
				public mapColumnSelectors mapColumnSelectors { get; private set; }

				// Token: 0x17000999 RID: 2457
				// (get) Token: 0x060033DF RID: 13279 RVA: 0x000A4B74 File Offset: 0x000A2D74
				// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000A4B7C File Offset: 0x000A2D7C
				public rowRecord rowRecord { get; private set; }

				// Token: 0x1700099A RID: 2458
				// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000A4B85 File Offset: 0x000A2D85
				// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000A4B8D File Offset: 0x000A2D8D
				public columnSelectorList columnSelectorList { get; private set; }

				// Token: 0x1700099B RID: 2459
				// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000A4B96 File Offset: 0x000A2D96
				// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000A4B9E File Offset: 0x000A2D9E
				public columnSelector columnSelector { get; private set; }

				// Token: 0x1700099C RID: 2460
				// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000A4BA7 File Offset: 0x000A2DA7
				// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000A4BAF File Offset: 0x000A2DAF
				public primarySelector primarySelector { get; private set; }

				// Token: 0x1700099D RID: 2461
				// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000A4BB8 File Offset: 0x000A2DB8
				// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000A4BC0 File Offset: 0x000A2DC0
				public delimiterSplit delimiterSplit { get; private set; }

				// Token: 0x1700099E RID: 2462
				// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000A4BC9 File Offset: 0x000A2DC9
				// (set) Token: 0x060033EA RID: 13290 RVA: 0x000A4BD1 File Offset: 0x000A2DD1
				public record record { get; private set; }

				// Token: 0x1700099F RID: 2463
				// (get) Token: 0x060033EB RID: 13291 RVA: 0x000A4BDA File Offset: 0x000A2DDA
				// (set) Token: 0x060033EC RID: 13292 RVA: 0x000A4BE2 File Offset: 0x000A2DE2
				public splitTextProg splitTextProg { get; private set; }

				// Token: 0x170009A0 RID: 2464
				// (get) Token: 0x060033ED RID: 13293 RVA: 0x000A4BEB File Offset: 0x000A2DEB
				// (set) Token: 0x060033EE RID: 13294 RVA: 0x000A4BF3 File Offset: 0x000A2DF3
				public splitFile splitFile { get; private set; }

				// Token: 0x170009A1 RID: 2465
				// (get) Token: 0x060033EF RID: 13295 RVA: 0x000A4BFC File Offset: 0x000A2DFC
				// (set) Token: 0x060033F0 RID: 13296 RVA: 0x000A4C04 File Offset: 0x000A2E04
				public allLines allLines { get; private set; }

				// Token: 0x170009A2 RID: 2466
				// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000A4C0D File Offset: 0x000A2E0D
				// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000A4C15 File Offset: 0x000A2E15
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r r { get; private set; }

				// Token: 0x170009A3 RID: 2467
				// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000A4C1E File Offset: 0x000A2E1E
				// (set) Token: 0x060033F4 RID: 13300 RVA: 0x000A4C26 File Offset: 0x000A2E26
				public k k { get; private set; }

				// Token: 0x170009A4 RID: 2468
				// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000A4C2F File Offset: 0x000A2E2F
				// (set) Token: 0x060033F6 RID: 13302 RVA: 0x000A4C37 File Offset: 0x000A2E37
				public quotingConfig quotingConfig { get; private set; }

				// Token: 0x170009A5 RID: 2469
				// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000A4C40 File Offset: 0x000A2E40
				// (set) Token: 0x060033F8 RID: 13304 RVA: 0x000A4C48 File Offset: 0x000A2E48
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter delimiter { get; private set; }

				// Token: 0x170009A6 RID: 2470
				// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000A4C51 File Offset: 0x000A2E51
				// (set) Token: 0x060033FA RID: 13306 RVA: 0x000A4C59 File Offset: 0x000A2E59
				public headerIndex headerIndex { get; private set; }

				// Token: 0x170009A7 RID: 2471
				// (get) Token: 0x060033FB RID: 13307 RVA: 0x000A4C62 File Offset: 0x000A2E62
				// (set) Token: 0x060033FC RID: 13308 RVA: 0x000A4C6A File Offset: 0x000A2E6A
				public commentStr commentStr { get; private set; }

				// Token: 0x170009A8 RID: 2472
				// (get) Token: 0x060033FD RID: 13309 RVA: 0x000A4C73 File Offset: 0x000A2E73
				// (set) Token: 0x060033FE RID: 13310 RVA: 0x000A4C7B File Offset: 0x000A2E7B
				public skipEmpty skipEmpty { get; private set; }

				// Token: 0x170009A9 RID: 2473
				// (get) Token: 0x060033FF RID: 13311 RVA: 0x000A4C84 File Offset: 0x000A2E84
				// (set) Token: 0x06003400 RID: 13312 RVA: 0x000A4C8C File Offset: 0x000A2E8C
				public hasCommentHeader hasCommentHeader { get; private set; }

				// Token: 0x170009AA RID: 2474
				// (get) Token: 0x06003401 RID: 13313 RVA: 0x000A4C95 File Offset: 0x000A2E95
				// (set) Token: 0x06003402 RID: 13314 RVA: 0x000A4C9D File Offset: 0x000A2E9D
				public splitLines splitLines { get; private set; }

				// Token: 0x170009AB RID: 2475
				// (get) Token: 0x06003403 RID: 13315 RVA: 0x000A4CA6 File Offset: 0x000A2EA6
				// (set) Token: 0x06003404 RID: 13316 RVA: 0x000A4CAE File Offset: 0x000A2EAE
				public ls ls { get; private set; }

				// Token: 0x170009AC RID: 2476
				// (get) Token: 0x06003405 RID: 13317 RVA: 0x000A4CB7 File Offset: 0x000A2EB7
				// (set) Token: 0x06003406 RID: 13318 RVA: 0x000A4CBF File Offset: 0x000A2EBF
				public dataLines dataLines { get; private set; }

				// Token: 0x170009AD RID: 2477
				// (get) Token: 0x06003407 RID: 13319 RVA: 0x000A4CC8 File Offset: 0x000A2EC8
				// (set) Token: 0x06003408 RID: 13320 RVA: 0x000A4CD0 File Offset: 0x000A2ED0
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s s { get; private set; }

				// Token: 0x170009AE RID: 2478
				// (get) Token: 0x06003409 RID: 13321 RVA: 0x000A4CD9 File Offset: 0x000A2ED9
				// (set) Token: 0x0600340A RID: 13322 RVA: 0x000A4CE1 File Offset: 0x000A2EE1
				public skippedRecords skippedRecords { get; private set; }

				// Token: 0x170009AF RID: 2479
				// (get) Token: 0x0600340B RID: 13323 RVA: 0x000A4CEA File Offset: 0x000A2EEA
				// (set) Token: 0x0600340C RID: 13324 RVA: 0x000A4CF2 File Offset: 0x000A2EF2
				public skippedFooter skippedFooter { get; private set; }

				// Token: 0x170009B0 RID: 2480
				// (get) Token: 0x0600340D RID: 13325 RVA: 0x000A4CFB File Offset: 0x000A2EFB
				// (set) Token: 0x0600340E RID: 13326 RVA: 0x000A4D03 File Offset: 0x000A2F03
				public allRecords allRecords { get; private set; }

				// Token: 0x170009B1 RID: 2481
				// (get) Token: 0x0600340F RID: 13327 RVA: 0x000A4D0C File Offset: 0x000A2F0C
				// (set) Token: 0x06003410 RID: 13328 RVA: 0x000A4D14 File Offset: 0x000A2F14
				public basicLinePredicate basicLinePredicate { get; private set; }

				// Token: 0x170009B2 RID: 2482
				// (get) Token: 0x06003411 RID: 13329 RVA: 0x000A4D1D File Offset: 0x000A2F1D
				// (set) Token: 0x06003412 RID: 13330 RVA: 0x000A4D25 File Offset: 0x000A2F25
				public splitSequence splitSequence { get; private set; }

				// Token: 0x170009B3 RID: 2483
				// (get) Token: 0x06003413 RID: 13331 RVA: 0x000A4D2E File Offset: 0x000A2F2E
				// (set) Token: 0x06003414 RID: 13332 RVA: 0x000A4D36 File Offset: 0x000A2F36
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 _LetB0 { get; private set; }

				// Token: 0x170009B4 RID: 2484
				// (get) Token: 0x06003415 RID: 13333 RVA: 0x000A4D3F File Offset: 0x000A2F3F
				// (set) Token: 0x06003416 RID: 13334 RVA: 0x000A4D47 File Offset: 0x000A2F47
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 _LetB1 { get; private set; }

				// Token: 0x06003417 RID: 13335 RVA: 0x000A4D50 File Offset: 0x000A2F50
				public NodeHoles(GrammarBuilders builders)
				{
					this.hasHeader = hasHeader.CreateHole(builders, null);
					this.columnList = columnList.CreateHole(builders, null);
					this.topSplit = topSplit.CreateHole(builders, null);
					this.records = records.CreateHole(builders, null);
					this.splitRecordsSelect = splitRecordsSelect.CreateHole(builders, null);
					this.splitRecords = splitRecords.CreateHole(builders, null);
					this.key = key.CreateHole(builders, null);
					this.sep = sep.CreateHole(builders, null);
					this.newLineSep = newLineSep.CreateHole(builders, null);
					this.fwPos = fwPos.CreateHole(builders, null);
					this.multiRecordSplit = multiRecordSplit.CreateHole(builders, null);
					this.rowRecords = rowRecords.CreateHole(builders, null);
					this.mapColumnSelectors = mapColumnSelectors.CreateHole(builders, null);
					this.rowRecord = rowRecord.CreateHole(builders, null);
					this.columnSelectorList = columnSelectorList.CreateHole(builders, null);
					this.columnSelector = columnSelector.CreateHole(builders, null);
					this.primarySelector = primarySelector.CreateHole(builders, null);
					this.delimiterSplit = delimiterSplit.CreateHole(builders, null);
					this.record = record.CreateHole(builders, null);
					this.splitTextProg = splitTextProg.CreateHole(builders, null);
					this.splitFile = splitFile.CreateHole(builders, null);
					this.allLines = allLines.CreateHole(builders, null);
					this.r = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateHole(builders, null);
					this.k = k.CreateHole(builders, null);
					this.quotingConfig = quotingConfig.CreateHole(builders, null);
					this.delimiter = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateHole(builders, null);
					this.headerIndex = headerIndex.CreateHole(builders, null);
					this.commentStr = commentStr.CreateHole(builders, null);
					this.skipEmpty = skipEmpty.CreateHole(builders, null);
					this.hasCommentHeader = hasCommentHeader.CreateHole(builders, null);
					this.splitLines = splitLines.CreateHole(builders, null);
					this.ls = ls.CreateHole(builders, null);
					this.dataLines = dataLines.CreateHole(builders, null);
					this.s = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateHole(builders, null);
					this.skippedRecords = skippedRecords.CreateHole(builders, null);
					this.skippedFooter = skippedFooter.CreateHole(builders, null);
					this.allRecords = allRecords.CreateHole(builders, null);
					this.basicLinePredicate = basicLinePredicate.CreateHole(builders, null);
					this.splitSequence = splitSequence.CreateHole(builders, null);
					this._LetB0 = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateHole(builders, null);
					this._LetB1 = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateHole(builders, null);
				}
			}

			// Token: 0x02000924 RID: 2340
			public class NodeUnsafe
			{
				// Token: 0x06003418 RID: 13336 RVA: 0x000A4F78 File Offset: 0x000A3178
				public hasHeader hasHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader.CreateUnsafe(node);
				}

				// Token: 0x06003419 RID: 13337 RVA: 0x000A4F80 File Offset: 0x000A3180
				public columnList columnList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList.CreateUnsafe(node);
				}

				// Token: 0x0600341A RID: 13338 RVA: 0x000A4F88 File Offset: 0x000A3188
				public topSplit topSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit.CreateUnsafe(node);
				}

				// Token: 0x0600341B RID: 13339 RVA: 0x000A4F90 File Offset: 0x000A3190
				public records records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records.CreateUnsafe(node);
				}

				// Token: 0x0600341C RID: 13340 RVA: 0x000A4F98 File Offset: 0x000A3198
				public splitRecordsSelect splitRecordsSelect(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect.CreateUnsafe(node);
				}

				// Token: 0x0600341D RID: 13341 RVA: 0x000A4FA0 File Offset: 0x000A31A0
				public splitRecords splitRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords.CreateUnsafe(node);
				}

				// Token: 0x0600341E RID: 13342 RVA: 0x000A4FA8 File Offset: 0x000A31A8
				public key key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key.CreateUnsafe(node);
				}

				// Token: 0x0600341F RID: 13343 RVA: 0x000A4FB0 File Offset: 0x000A31B0
				public sep sep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep.CreateUnsafe(node);
				}

				// Token: 0x06003420 RID: 13344 RVA: 0x000A4FB8 File Offset: 0x000A31B8
				public newLineSep newLineSep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep.CreateUnsafe(node);
				}

				// Token: 0x06003421 RID: 13345 RVA: 0x000A4FC0 File Offset: 0x000A31C0
				public fwPos fwPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos.CreateUnsafe(node);
				}

				// Token: 0x06003422 RID: 13346 RVA: 0x000A4FC8 File Offset: 0x000A31C8
				public multiRecordSplit multiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit.CreateUnsafe(node);
				}

				// Token: 0x06003423 RID: 13347 RVA: 0x000A4FD0 File Offset: 0x000A31D0
				public rowRecords rowRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords.CreateUnsafe(node);
				}

				// Token: 0x06003424 RID: 13348 RVA: 0x000A4FD8 File Offset: 0x000A31D8
				public mapColumnSelectors mapColumnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors.CreateUnsafe(node);
				}

				// Token: 0x06003425 RID: 13349 RVA: 0x000A4FE0 File Offset: 0x000A31E0
				public rowRecord rowRecord(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord.CreateUnsafe(node);
				}

				// Token: 0x06003426 RID: 13350 RVA: 0x000A4FE8 File Offset: 0x000A31E8
				public columnSelectorList columnSelectorList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList.CreateUnsafe(node);
				}

				// Token: 0x06003427 RID: 13351 RVA: 0x000A4FF0 File Offset: 0x000A31F0
				public columnSelector columnSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector.CreateUnsafe(node);
				}

				// Token: 0x06003428 RID: 13352 RVA: 0x000A4FF8 File Offset: 0x000A31F8
				public primarySelector primarySelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector.CreateUnsafe(node);
				}

				// Token: 0x06003429 RID: 13353 RVA: 0x000A5000 File Offset: 0x000A3200
				public delimiterSplit delimiterSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit.CreateUnsafe(node);
				}

				// Token: 0x0600342A RID: 13354 RVA: 0x000A5008 File Offset: 0x000A3208
				public record record(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record.CreateUnsafe(node);
				}

				// Token: 0x0600342B RID: 13355 RVA: 0x000A5010 File Offset: 0x000A3210
				public splitTextProg splitTextProg(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg.CreateUnsafe(node);
				}

				// Token: 0x0600342C RID: 13356 RVA: 0x000A5018 File Offset: 0x000A3218
				public splitFile splitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile.CreateUnsafe(node);
				}

				// Token: 0x0600342D RID: 13357 RVA: 0x000A5020 File Offset: 0x000A3220
				public allLines allLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines.CreateUnsafe(node);
				}

				// Token: 0x0600342E RID: 13358 RVA: 0x000A5028 File Offset: 0x000A3228
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateUnsafe(node);
				}

				// Token: 0x0600342F RID: 13359 RVA: 0x000A5030 File Offset: 0x000A3230
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x06003430 RID: 13360 RVA: 0x000A5038 File Offset: 0x000A3238
				public quotingConfig quotingConfig(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig.CreateUnsafe(node);
				}

				// Token: 0x06003431 RID: 13361 RVA: 0x000A5040 File Offset: 0x000A3240
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateUnsafe(node);
				}

				// Token: 0x06003432 RID: 13362 RVA: 0x000A5048 File Offset: 0x000A3248
				public headerIndex headerIndex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex.CreateUnsafe(node);
				}

				// Token: 0x06003433 RID: 13363 RVA: 0x000A5050 File Offset: 0x000A3250
				public commentStr commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr.CreateUnsafe(node);
				}

				// Token: 0x06003434 RID: 13364 RVA: 0x000A5058 File Offset: 0x000A3258
				public skipEmpty skipEmpty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty.CreateUnsafe(node);
				}

				// Token: 0x06003435 RID: 13365 RVA: 0x000A5060 File Offset: 0x000A3260
				public hasCommentHeader hasCommentHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader.CreateUnsafe(node);
				}

				// Token: 0x06003436 RID: 13366 RVA: 0x000A5068 File Offset: 0x000A3268
				public splitLines splitLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines.CreateUnsafe(node);
				}

				// Token: 0x06003437 RID: 13367 RVA: 0x000A5070 File Offset: 0x000A3270
				public ls ls(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls.CreateUnsafe(node);
				}

				// Token: 0x06003438 RID: 13368 RVA: 0x000A5078 File Offset: 0x000A3278
				public dataLines dataLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines.CreateUnsafe(node);
				}

				// Token: 0x06003439 RID: 13369 RVA: 0x000A5080 File Offset: 0x000A3280
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateUnsafe(node);
				}

				// Token: 0x0600343A RID: 13370 RVA: 0x000A5088 File Offset: 0x000A3288
				public skippedRecords skippedRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords.CreateUnsafe(node);
				}

				// Token: 0x0600343B RID: 13371 RVA: 0x000A5090 File Offset: 0x000A3290
				public skippedFooter skippedFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter.CreateUnsafe(node);
				}

				// Token: 0x0600343C RID: 13372 RVA: 0x000A5098 File Offset: 0x000A3298
				public allRecords allRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords.CreateUnsafe(node);
				}

				// Token: 0x0600343D RID: 13373 RVA: 0x000A50A0 File Offset: 0x000A32A0
				public basicLinePredicate basicLinePredicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate.CreateUnsafe(node);
				}

				// Token: 0x0600343E RID: 13374 RVA: 0x000A50A8 File Offset: 0x000A32A8
				public splitSequence splitSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence.CreateUnsafe(node);
				}

				// Token: 0x0600343F RID: 13375 RVA: 0x000A50B0 File Offset: 0x000A32B0
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x06003440 RID: 13376 RVA: 0x000A50B8 File Offset: 0x000A32B8
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}
			}

			// Token: 0x02000925 RID: 2341
			public class NodeCast
			{
				// Token: 0x06003442 RID: 13378 RVA: 0x000A50C0 File Offset: 0x000A32C0
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003443 RID: 13379 RVA: 0x000A50D0 File Offset: 0x000A32D0
				public hasHeader hasHeader(ProgramNode node)
				{
					hasHeader? hasHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader.CreateSafe(this._builders, node);
					if (hasHeader == null)
					{
						string text = "node";
						string text2 = "expected node for symbol hasHeader but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasHeader.Value;
				}

				// Token: 0x06003444 RID: 13380 RVA: 0x000A5124 File Offset: 0x000A3324
				public columnList columnList(ProgramNode node)
				{
					columnList? columnList = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList.CreateSafe(this._builders, node);
					if (columnList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnList.Value;
				}

				// Token: 0x06003445 RID: 13381 RVA: 0x000A5178 File Offset: 0x000A3378
				public topSplit topSplit(ProgramNode node)
				{
					topSplit? topSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit.CreateSafe(this._builders, node);
					if (topSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol topSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return topSplit.Value;
				}

				// Token: 0x06003446 RID: 13382 RVA: 0x000A51CC File Offset: 0x000A33CC
				public records records(ProgramNode node)
				{
					records? records = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records.CreateSafe(this._builders, node);
					if (records == null)
					{
						string text = "node";
						string text2 = "expected node for symbol records but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return records.Value;
				}

				// Token: 0x06003447 RID: 13383 RVA: 0x000A5220 File Offset: 0x000A3420
				public splitRecordsSelect splitRecordsSelect(ProgramNode node)
				{
					splitRecordsSelect? splitRecordsSelect = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect.CreateSafe(this._builders, node);
					if (splitRecordsSelect == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitRecordsSelect but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitRecordsSelect.Value;
				}

				// Token: 0x06003448 RID: 13384 RVA: 0x000A5274 File Offset: 0x000A3474
				public splitRecords splitRecords(ProgramNode node)
				{
					splitRecords? splitRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords.CreateSafe(this._builders, node);
					if (splitRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitRecords.Value;
				}

				// Token: 0x06003449 RID: 13385 RVA: 0x000A52C8 File Offset: 0x000A34C8
				public key key(ProgramNode node)
				{
					key? key = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key.CreateSafe(this._builders, node);
					if (key == null)
					{
						string text = "node";
						string text2 = "expected node for symbol key but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return key.Value;
				}

				// Token: 0x0600344A RID: 13386 RVA: 0x000A531C File Offset: 0x000A351C
				public sep sep(ProgramNode node)
				{
					sep? sep = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep.CreateSafe(this._builders, node);
					if (sep == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sep but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sep.Value;
				}

				// Token: 0x0600344B RID: 13387 RVA: 0x000A5370 File Offset: 0x000A3570
				public newLineSep newLineSep(ProgramNode node)
				{
					newLineSep? newLineSep = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep.CreateSafe(this._builders, node);
					if (newLineSep == null)
					{
						string text = "node";
						string text2 = "expected node for symbol newLineSep but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return newLineSep.Value;
				}

				// Token: 0x0600344C RID: 13388 RVA: 0x000A53C4 File Offset: 0x000A35C4
				public fwPos fwPos(ProgramNode node)
				{
					fwPos? fwPos = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos.CreateSafe(this._builders, node);
					if (fwPos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fwPos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fwPos.Value;
				}

				// Token: 0x0600344D RID: 13389 RVA: 0x000A5418 File Offset: 0x000A3618
				public multiRecordSplit multiRecordSplit(ProgramNode node)
				{
					multiRecordSplit? multiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit.CreateSafe(this._builders, node);
					if (multiRecordSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multiRecordSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiRecordSplit.Value;
				}

				// Token: 0x0600344E RID: 13390 RVA: 0x000A546C File Offset: 0x000A366C
				public rowRecords rowRecords(ProgramNode node)
				{
					rowRecords? rowRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords.CreateSafe(this._builders, node);
					if (rowRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rowRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowRecords.Value;
				}

				// Token: 0x0600344F RID: 13391 RVA: 0x000A54C0 File Offset: 0x000A36C0
				public mapColumnSelectors mapColumnSelectors(ProgramNode node)
				{
					mapColumnSelectors? mapColumnSelectors = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors.CreateSafe(this._builders, node);
					if (mapColumnSelectors == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mapColumnSelectors but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mapColumnSelectors.Value;
				}

				// Token: 0x06003450 RID: 13392 RVA: 0x000A5514 File Offset: 0x000A3714
				public rowRecord rowRecord(ProgramNode node)
				{
					rowRecord? rowRecord = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord.CreateSafe(this._builders, node);
					if (rowRecord == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rowRecord but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowRecord.Value;
				}

				// Token: 0x06003451 RID: 13393 RVA: 0x000A5568 File Offset: 0x000A3768
				public columnSelectorList columnSelectorList(ProgramNode node)
				{
					columnSelectorList? columnSelectorList = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList.CreateSafe(this._builders, node);
					if (columnSelectorList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnSelectorList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnSelectorList.Value;
				}

				// Token: 0x06003452 RID: 13394 RVA: 0x000A55BC File Offset: 0x000A37BC
				public columnSelector columnSelector(ProgramNode node)
				{
					columnSelector? columnSelector = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector.CreateSafe(this._builders, node);
					if (columnSelector == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnSelector but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnSelector.Value;
				}

				// Token: 0x06003453 RID: 13395 RVA: 0x000A5610 File Offset: 0x000A3810
				public primarySelector primarySelector(ProgramNode node)
				{
					primarySelector? primarySelector = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector.CreateSafe(this._builders, node);
					if (primarySelector == null)
					{
						string text = "node";
						string text2 = "expected node for symbol primarySelector but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return primarySelector.Value;
				}

				// Token: 0x06003454 RID: 13396 RVA: 0x000A5664 File Offset: 0x000A3864
				public delimiterSplit delimiterSplit(ProgramNode node)
				{
					delimiterSplit? delimiterSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit.CreateSafe(this._builders, node);
					if (delimiterSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiterSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiterSplit.Value;
				}

				// Token: 0x06003455 RID: 13397 RVA: 0x000A56B8 File Offset: 0x000A38B8
				public record record(ProgramNode node)
				{
					record? record = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record.CreateSafe(this._builders, node);
					if (record == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @record but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return record.Value;
				}

				// Token: 0x06003456 RID: 13398 RVA: 0x000A570C File Offset: 0x000A390C
				public splitTextProg splitTextProg(ProgramNode node)
				{
					splitTextProg? splitTextProg = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg.CreateSafe(this._builders, node);
					if (splitTextProg == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitTextProg but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitTextProg.Value;
				}

				// Token: 0x06003457 RID: 13399 RVA: 0x000A5760 File Offset: 0x000A3960
				public splitFile splitFile(ProgramNode node)
				{
					splitFile? splitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile.CreateSafe(this._builders, node);
					if (splitFile == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitFile but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitFile.Value;
				}

				// Token: 0x06003458 RID: 13400 RVA: 0x000A57B4 File Offset: 0x000A39B4
				public allLines allLines(ProgramNode node)
				{
					allLines? allLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines.CreateSafe(this._builders, node);
					if (allLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol allLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return allLines.Value;
				}

				// Token: 0x06003459 RID: 13401 RVA: 0x000A5808 File Offset: 0x000A3A08
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r r(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r? r = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						string text = "node";
						string text2 = "expected node for symbol r but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return r.Value;
				}

				// Token: 0x0600345A RID: 13402 RVA: 0x000A585C File Offset: 0x000A3A5C
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x0600345B RID: 13403 RVA: 0x000A58B0 File Offset: 0x000A3AB0
				public quotingConfig quotingConfig(ProgramNode node)
				{
					quotingConfig? quotingConfig = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig.CreateSafe(this._builders, node);
					if (quotingConfig == null)
					{
						string text = "node";
						string text2 = "expected node for symbol quotingConfig but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return quotingConfig.Value;
				}

				// Token: 0x0600345C RID: 13404 RVA: 0x000A5904 File Offset: 0x000A3B04
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter delimiter(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter? delimiter = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiter.Value;
				}

				// Token: 0x0600345D RID: 13405 RVA: 0x000A5958 File Offset: 0x000A3B58
				public headerIndex headerIndex(ProgramNode node)
				{
					headerIndex? headerIndex = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex.CreateSafe(this._builders, node);
					if (headerIndex == null)
					{
						string text = "node";
						string text2 = "expected node for symbol headerIndex but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return headerIndex.Value;
				}

				// Token: 0x0600345E RID: 13406 RVA: 0x000A59AC File Offset: 0x000A3BAC
				public commentStr commentStr(ProgramNode node)
				{
					commentStr? commentStr = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
					if (commentStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol commentStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return commentStr.Value;
				}

				// Token: 0x0600345F RID: 13407 RVA: 0x000A5A00 File Offset: 0x000A3C00
				public skipEmpty skipEmpty(ProgramNode node)
				{
					skipEmpty? skipEmpty = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty.CreateSafe(this._builders, node);
					if (skipEmpty == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skipEmpty but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skipEmpty.Value;
				}

				// Token: 0x06003460 RID: 13408 RVA: 0x000A5A54 File Offset: 0x000A3C54
				public hasCommentHeader hasCommentHeader(ProgramNode node)
				{
					hasCommentHeader? hasCommentHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader.CreateSafe(this._builders, node);
					if (hasCommentHeader == null)
					{
						string text = "node";
						string text2 = "expected node for symbol hasCommentHeader but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasCommentHeader.Value;
				}

				// Token: 0x06003461 RID: 13409 RVA: 0x000A5AA8 File Offset: 0x000A3CA8
				public splitLines splitLines(ProgramNode node)
				{
					splitLines? splitLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines.CreateSafe(this._builders, node);
					if (splitLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitLines.Value;
				}

				// Token: 0x06003462 RID: 13410 RVA: 0x000A5AFC File Offset: 0x000A3CFC
				public ls ls(ProgramNode node)
				{
					ls? ls = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls.CreateSafe(this._builders, node);
					if (ls == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ls but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ls.Value;
				}

				// Token: 0x06003463 RID: 13411 RVA: 0x000A5B50 File Offset: 0x000A3D50
				public dataLines dataLines(ProgramNode node)
				{
					dataLines? dataLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines.CreateSafe(this._builders, node);
					if (dataLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dataLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dataLines.Value;
				}

				// Token: 0x06003464 RID: 13412 RVA: 0x000A5BA4 File Offset: 0x000A3DA4
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s s(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s? s = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						string text = "node";
						string text2 = "expected node for symbol s but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return s.Value;
				}

				// Token: 0x06003465 RID: 13413 RVA: 0x000A5BF8 File Offset: 0x000A3DF8
				public skippedRecords skippedRecords(ProgramNode node)
				{
					skippedRecords? skippedRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords.CreateSafe(this._builders, node);
					if (skippedRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skippedRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skippedRecords.Value;
				}

				// Token: 0x06003466 RID: 13414 RVA: 0x000A5C4C File Offset: 0x000A3E4C
				public skippedFooter skippedFooter(ProgramNode node)
				{
					skippedFooter? skippedFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter.CreateSafe(this._builders, node);
					if (skippedFooter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skippedFooter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skippedFooter.Value;
				}

				// Token: 0x06003467 RID: 13415 RVA: 0x000A5CA0 File Offset: 0x000A3EA0
				public allRecords allRecords(ProgramNode node)
				{
					allRecords? allRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords.CreateSafe(this._builders, node);
					if (allRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol allRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return allRecords.Value;
				}

				// Token: 0x06003468 RID: 13416 RVA: 0x000A5CF4 File Offset: 0x000A3EF4
				public basicLinePredicate basicLinePredicate(ProgramNode node)
				{
					basicLinePredicate? basicLinePredicate = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate.CreateSafe(this._builders, node);
					if (basicLinePredicate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol basicLinePredicate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return basicLinePredicate.Value;
				}

				// Token: 0x06003469 RID: 13417 RVA: 0x000A5D48 File Offset: 0x000A3F48
				public splitSequence splitSequence(ProgramNode node)
				{
					splitSequence? splitSequence = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence.CreateSafe(this._builders, node);
					if (splitSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitSequence.Value;
				}

				// Token: 0x0600346A RID: 13418 RVA: 0x000A5D9C File Offset: 0x000A3F9C
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600346B RID: 13419 RVA: 0x000A5DF0 File Offset: 0x000A3FF0
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 _LetB1(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1? letB = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x040019EC RID: 6636
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000926 RID: 2342
			public class RuleCast
			{
				// Token: 0x0600346C RID: 13420 RVA: 0x000A5E41 File Offset: 0x000A4041
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600346D RID: 13421 RVA: 0x000A5E50 File Offset: 0x000A4050
				public LetFileRecordSplit LetFileRecordSplit(ProgramNode node)
				{
					LetFileRecordSplit? letFileRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetFileRecordSplit.CreateSafe(this._builders, node);
					if (letFileRecordSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetFileRecordSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letFileRecordSplit.Value;
				}

				// Token: 0x0600346E RID: 13422 RVA: 0x000A5EA4 File Offset: 0x000A40A4
				public SelectColumns SelectColumns(ProgramNode node)
				{
					SelectColumns? selectColumns = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectColumns.CreateSafe(this._builders, node);
					if (selectColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectColumns.Value;
				}

				// Token: 0x0600346F RID: 13423 RVA: 0x000A5EF8 File Offset: 0x000A40F8
				public splitRecordsSelect_splitRecords splitRecordsSelect_splitRecords(ProgramNode node)
				{
					splitRecordsSelect_splitRecords? splitRecordsSelect_splitRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.splitRecordsSelect_splitRecords.CreateSafe(this._builders, node);
					if (splitRecordsSelect_splitRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitRecordsSelect_splitRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitRecordsSelect_splitRecords.Value;
				}

				// Token: 0x06003470 RID: 13424 RVA: 0x000A5F4C File Offset: 0x000A414C
				public NoSplit NoSplit(ProgramNode node)
				{
					NoSplit? noSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.NoSplit.CreateSafe(this._builders, node);
					if (noSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NoSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return noSplit.Value;
				}

				// Token: 0x06003471 RID: 13425 RVA: 0x000A5FA0 File Offset: 0x000A41A0
				public TableFromCells TableFromCells(ProgramNode node)
				{
					TableFromCells? tableFromCells = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TableFromCells.CreateSafe(this._builders, node);
					if (tableFromCells == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TableFromCells but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tableFromCells.Value;
				}

				// Token: 0x06003472 RID: 13426 RVA: 0x000A5FF4 File Offset: 0x000A41F4
				public MultiRecordSplit MultiRecordSplit(ProgramNode node)
				{
					MultiRecordSplit? multiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MultiRecordSplit.CreateSafe(this._builders, node);
					if (multiRecordSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MultiRecordSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiRecordSplit.Value;
				}

				// Token: 0x06003473 RID: 13427 RVA: 0x000A6048 File Offset: 0x000A4248
				public LetMultiRecordSplit LetMultiRecordSplit(ProgramNode node)
				{
					LetMultiRecordSplit? letMultiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetMultiRecordSplit.CreateSafe(this._builders, node);
					if (letMultiRecordSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetMultiRecordSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letMultiRecordSplit.Value;
				}

				// Token: 0x06003474 RID: 13428 RVA: 0x000A609C File Offset: 0x000A429C
				public MapColumnSelector MapColumnSelector(ProgramNode node)
				{
					MapColumnSelector? mapColumnSelector = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MapColumnSelector.CreateSafe(this._builders, node);
					if (mapColumnSelector == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MapColumnSelector but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mapColumnSelector.Value;
				}

				// Token: 0x06003475 RID: 13429 RVA: 0x000A60F0 File Offset: 0x000A42F0
				public Empty Empty(ProgramNode node)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Empty but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return empty.Value;
				}

				// Token: 0x06003476 RID: 13430 RVA: 0x000A6144 File Offset: 0x000A4344
				public SelectorList SelectorList(ProgramNode node)
				{
					SelectorList? selectorList = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectorList.CreateSafe(this._builders, node);
					if (selectorList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectorList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectorList.Value;
				}

				// Token: 0x06003477 RID: 13431 RVA: 0x000A6198 File Offset: 0x000A4398
				public KthLine KthLine(ProgramNode node)
				{
					KthLine? kthLine = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthLine.CreateSafe(this._builders, node);
					if (kthLine == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthLine but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthLine.Value;
				}

				// Token: 0x06003478 RID: 13432 RVA: 0x000A61EC File Offset: 0x000A43EC
				public KthKeyValue KthKeyValue(ProgramNode node)
				{
					KthKeyValue? kthKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValue.CreateSafe(this._builders, node);
					if (kthKeyValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthKeyValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthKeyValue.Value;
				}

				// Token: 0x06003479 RID: 13433 RVA: 0x000A6240 File Offset: 0x000A4440
				public KthTwoLineKeyValue KthTwoLineKeyValue(ProgramNode node)
				{
					KthTwoLineKeyValue? kthTwoLineKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthTwoLineKeyValue.CreateSafe(this._builders, node);
					if (kthTwoLineKeyValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthTwoLineKeyValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthTwoLineKeyValue.Value;
				}

				// Token: 0x0600347A RID: 13434 RVA: 0x000A6294 File Offset: 0x000A4494
				public KthKeyQuote KthKeyQuote(ProgramNode node)
				{
					KthKeyQuote? kthKeyQuote = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyQuote.CreateSafe(this._builders, node);
					if (kthKeyQuote == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthKeyQuote but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthKeyQuote.Value;
				}

				// Token: 0x0600347B RID: 13435 RVA: 0x000A62E8 File Offset: 0x000A44E8
				public KthKeyValueFw KthKeyValueFw(ProgramNode node)
				{
					KthKeyValueFw? kthKeyValueFw = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValueFw.CreateSafe(this._builders, node);
					if (kthKeyValueFw == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthKeyValueFw but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthKeyValueFw.Value;
				}

				// Token: 0x0600347C RID: 13436 RVA: 0x000A633C File Offset: 0x000A453C
				public BreakLine BreakLine(ProgramNode node)
				{
					BreakLine? breakLine = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.BreakLine.CreateSafe(this._builders, node);
					if (breakLine == null)
					{
						string text = "node";
						string text2 = "expected node for symbol BreakLine but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return breakLine.Value;
				}

				// Token: 0x0600347D RID: 13437 RVA: 0x000A6390 File Offset: 0x000A4590
				public TwoLineKeyValue TwoLineKeyValue(ProgramNode node)
				{
					TwoLineKeyValue? twoLineKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TwoLineKeyValue.CreateSafe(this._builders, node);
					if (twoLineKeyValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TwoLineKeyValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return twoLineKeyValue.Value;
				}

				// Token: 0x0600347E RID: 13438 RVA: 0x000A63E4 File Offset: 0x000A45E4
				public KeyValue KeyValue(ProgramNode node)
				{
					KeyValue? keyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyValue.CreateSafe(this._builders, node);
					if (keyValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KeyValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return keyValue.Value;
				}

				// Token: 0x0600347F RID: 13439 RVA: 0x000A6438 File Offset: 0x000A4638
				public KeyQuote KeyQuote(ProgramNode node)
				{
					KeyQuote? keyQuote = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyQuote.CreateSafe(this._builders, node);
					if (keyQuote == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KeyQuote but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return keyQuote.Value;
				}

				// Token: 0x06003480 RID: 13440 RVA: 0x000A648C File Offset: 0x000A468C
				public SplitToCells SplitToCells(ProgramNode node)
				{
					SplitToCells? splitToCells = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitToCells.CreateSafe(this._builders, node);
					if (splitToCells == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitToCells but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitToCells.Value;
				}

				// Token: 0x06003481 RID: 13441 RVA: 0x000A64E0 File Offset: 0x000A46E0
				public SplitTextProg SplitTextProg(ProgramNode node)
				{
					SplitTextProg? splitTextProg = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitTextProg.CreateSafe(this._builders, node);
					if (splitTextProg == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitTextProg but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitTextProg.Value;
				}

				// Token: 0x06003482 RID: 13442 RVA: 0x000A6534 File Offset: 0x000A4734
				public SplitFile SplitFile(ProgramNode node)
				{
					SplitFile? splitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitFile.CreateSafe(this._builders, node);
					if (splitFile == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitFile but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitFile.Value;
				}

				// Token: 0x06003483 RID: 13443 RVA: 0x000A6588 File Offset: 0x000A4788
				public MergeRecordLines MergeRecordLines(ProgramNode node)
				{
					MergeRecordLines? mergeRecordLines = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MergeRecordLines.CreateSafe(this._builders, node);
					if (mergeRecordLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MergeRecordLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mergeRecordLines.Value;
				}

				// Token: 0x06003484 RID: 13444 RVA: 0x000A65DC File Offset: 0x000A47DC
				public LetSplitFile LetSplitFile(ProgramNode node)
				{
					LetSplitFile? letSplitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetSplitFile.CreateSafe(this._builders, node);
					if (letSplitFile == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSplitFile but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSplitFile.Value;
				}

				// Token: 0x06003485 RID: 13445 RVA: 0x000A6630 File Offset: 0x000A4830
				public SplitSequenceLet SplitSequenceLet(ProgramNode node)
				{
					SplitSequenceLet? splitSequenceLet = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequenceLet.CreateSafe(this._builders, node);
					if (splitSequenceLet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitSequenceLet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitSequenceLet.Value;
				}

				// Token: 0x06003486 RID: 13446 RVA: 0x000A6684 File Offset: 0x000A4884
				public FilterHeader FilterHeader(ProgramNode node)
				{
					FilterHeader? filterHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterHeader.CreateSafe(this._builders, node);
					if (filterHeader == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FilterHeader but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterHeader.Value;
				}

				// Token: 0x06003487 RID: 13447 RVA: 0x000A66D8 File Offset: 0x000A48D8
				public SelectData SelectData(ProgramNode node)
				{
					SelectData? selectData = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectData.CreateSafe(this._builders, node);
					if (selectData == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectData but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectData.Value;
				}

				// Token: 0x06003488 RID: 13448 RVA: 0x000A672C File Offset: 0x000A492C
				public FilterRecords FilterRecords(ProgramNode node)
				{
					FilterRecords? filterRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterRecords.CreateSafe(this._builders, node);
					if (filterRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FilterRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterRecords.Value;
				}

				// Token: 0x06003489 RID: 13449 RVA: 0x000A6780 File Offset: 0x000A4980
				public dataLines_skippedRecords dataLines_skippedRecords(ProgramNode node)
				{
					dataLines_skippedRecords? dataLines_skippedRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.dataLines_skippedRecords.CreateSafe(this._builders, node);
					if (dataLines_skippedRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dataLines_skippedRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dataLines_skippedRecords.Value;
				}

				// Token: 0x0600348A RID: 13450 RVA: 0x000A67D4 File Offset: 0x000A49D4
				public Skip Skip(ProgramNode node)
				{
					Skip? skip = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Skip but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skip.Value;
				}

				// Token: 0x0600348B RID: 13451 RVA: 0x000A6828 File Offset: 0x000A4A28
				public skippedRecords_skippedFooter skippedRecords_skippedFooter(ProgramNode node)
				{
					skippedRecords_skippedFooter? skippedRecords_skippedFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedRecords_skippedFooter.CreateSafe(this._builders, node);
					if (skippedRecords_skippedFooter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skippedRecords_skippedFooter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skippedRecords_skippedFooter.Value;
				}

				// Token: 0x0600348C RID: 13452 RVA: 0x000A687C File Offset: 0x000A4A7C
				public SkipFooter SkipFooter(ProgramNode node)
				{
					SkipFooter? skipFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SkipFooter.CreateSafe(this._builders, node);
					if (skipFooter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SkipFooter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skipFooter.Value;
				}

				// Token: 0x0600348D RID: 13453 RVA: 0x000A68D0 File Offset: 0x000A4AD0
				public skippedFooter_allRecords skippedFooter_allRecords(ProgramNode node)
				{
					skippedFooter_allRecords? skippedFooter_allRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedFooter_allRecords.CreateSafe(this._builders, node);
					if (skippedFooter_allRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skippedFooter_allRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skippedFooter_allRecords.Value;
				}

				// Token: 0x0600348E RID: 13454 RVA: 0x000A6924 File Offset: 0x000A4B24
				public allRecords_allLines allRecords_allLines(ProgramNode node)
				{
					allRecords_allLines? allRecords_allLines = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.allRecords_allLines.CreateSafe(this._builders, node);
					if (allRecords_allLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol allRecords_allLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return allRecords_allLines.Value;
				}

				// Token: 0x0600348F RID: 13455 RVA: 0x000A6978 File Offset: 0x000A4B78
				public QuoteRecords QuoteRecords(ProgramNode node)
				{
					QuoteRecords? quoteRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.QuoteRecords.CreateSafe(this._builders, node);
					if (quoteRecords == null)
					{
						string text = "node";
						string text2 = "expected node for symbol QuoteRecords but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return quoteRecords.Value;
				}

				// Token: 0x06003490 RID: 13456 RVA: 0x000A69CC File Offset: 0x000A4BCC
				public StartsWith StartsWith(ProgramNode node)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWith but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWith.Value;
				}

				// Token: 0x06003491 RID: 13457 RVA: 0x000A6A20 File Offset: 0x000A4C20
				public SplitSequence SplitSequence(ProgramNode node)
				{
					SplitSequence? splitSequence = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequence.CreateSafe(this._builders, node);
					if (splitSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitSequence.Value;
				}

				// Token: 0x06003492 RID: 13458 RVA: 0x000A6A74 File Offset: 0x000A4C74
				public Sequence Sequence(ProgramNode node)
				{
					Sequence? sequence = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Sequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequence.Value;
				}

				// Token: 0x040019ED RID: 6637
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000927 RID: 2343
			public class NodeIs
			{
				// Token: 0x06003493 RID: 13459 RVA: 0x000A6AC5 File Offset: 0x000A4CC5
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003494 RID: 13460 RVA: 0x000A6AD4 File Offset: 0x000A4CD4
				public bool hasHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003495 RID: 13461 RVA: 0x000A6AF8 File Offset: 0x000A4CF8
				public bool hasHeader(ProgramNode node, out hasHeader value)
				{
					hasHeader? hasHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader.CreateSafe(this._builders, node);
					if (hasHeader == null)
					{
						value = default(hasHeader);
						return false;
					}
					value = hasHeader.Value;
					return true;
				}

				// Token: 0x06003496 RID: 13462 RVA: 0x000A6B34 File Offset: 0x000A4D34
				public bool columnList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003497 RID: 13463 RVA: 0x000A6B58 File Offset: 0x000A4D58
				public bool columnList(ProgramNode node, out columnList value)
				{
					columnList? columnList = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList.CreateSafe(this._builders, node);
					if (columnList == null)
					{
						value = default(columnList);
						return false;
					}
					value = columnList.Value;
					return true;
				}

				// Token: 0x06003498 RID: 13464 RVA: 0x000A6B94 File Offset: 0x000A4D94
				public bool topSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003499 RID: 13465 RVA: 0x000A6BB8 File Offset: 0x000A4DB8
				public bool topSplit(ProgramNode node, out topSplit value)
				{
					topSplit? topSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit.CreateSafe(this._builders, node);
					if (topSplit == null)
					{
						value = default(topSplit);
						return false;
					}
					value = topSplit.Value;
					return true;
				}

				// Token: 0x0600349A RID: 13466 RVA: 0x000A6BF4 File Offset: 0x000A4DF4
				public bool records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600349B RID: 13467 RVA: 0x000A6C18 File Offset: 0x000A4E18
				public bool records(ProgramNode node, out records value)
				{
					records? records = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records.CreateSafe(this._builders, node);
					if (records == null)
					{
						value = default(records);
						return false;
					}
					value = records.Value;
					return true;
				}

				// Token: 0x0600349C RID: 13468 RVA: 0x000A6C54 File Offset: 0x000A4E54
				public bool splitRecordsSelect(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600349D RID: 13469 RVA: 0x000A6C78 File Offset: 0x000A4E78
				public bool splitRecordsSelect(ProgramNode node, out splitRecordsSelect value)
				{
					splitRecordsSelect? splitRecordsSelect = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect.CreateSafe(this._builders, node);
					if (splitRecordsSelect == null)
					{
						value = default(splitRecordsSelect);
						return false;
					}
					value = splitRecordsSelect.Value;
					return true;
				}

				// Token: 0x0600349E RID: 13470 RVA: 0x000A6CB4 File Offset: 0x000A4EB4
				public bool splitRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600349F RID: 13471 RVA: 0x000A6CD8 File Offset: 0x000A4ED8
				public bool splitRecords(ProgramNode node, out splitRecords value)
				{
					splitRecords? splitRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords.CreateSafe(this._builders, node);
					if (splitRecords == null)
					{
						value = default(splitRecords);
						return false;
					}
					value = splitRecords.Value;
					return true;
				}

				// Token: 0x060034A0 RID: 13472 RVA: 0x000A6D14 File Offset: 0x000A4F14
				public bool key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034A1 RID: 13473 RVA: 0x000A6D38 File Offset: 0x000A4F38
				public bool key(ProgramNode node, out key value)
				{
					key? key = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key.CreateSafe(this._builders, node);
					if (key == null)
					{
						value = default(key);
						return false;
					}
					value = key.Value;
					return true;
				}

				// Token: 0x060034A2 RID: 13474 RVA: 0x000A6D74 File Offset: 0x000A4F74
				public bool sep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034A3 RID: 13475 RVA: 0x000A6D98 File Offset: 0x000A4F98
				public bool sep(ProgramNode node, out sep value)
				{
					sep? sep = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep.CreateSafe(this._builders, node);
					if (sep == null)
					{
						value = default(sep);
						return false;
					}
					value = sep.Value;
					return true;
				}

				// Token: 0x060034A4 RID: 13476 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
				public bool newLineSep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034A5 RID: 13477 RVA: 0x000A6DF8 File Offset: 0x000A4FF8
				public bool newLineSep(ProgramNode node, out newLineSep value)
				{
					newLineSep? newLineSep = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep.CreateSafe(this._builders, node);
					if (newLineSep == null)
					{
						value = default(newLineSep);
						return false;
					}
					value = newLineSep.Value;
					return true;
				}

				// Token: 0x060034A6 RID: 13478 RVA: 0x000A6E34 File Offset: 0x000A5034
				public bool fwPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034A7 RID: 13479 RVA: 0x000A6E58 File Offset: 0x000A5058
				public bool fwPos(ProgramNode node, out fwPos value)
				{
					fwPos? fwPos = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos.CreateSafe(this._builders, node);
					if (fwPos == null)
					{
						value = default(fwPos);
						return false;
					}
					value = fwPos.Value;
					return true;
				}

				// Token: 0x060034A8 RID: 13480 RVA: 0x000A6E94 File Offset: 0x000A5094
				public bool multiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034A9 RID: 13481 RVA: 0x000A6EB8 File Offset: 0x000A50B8
				public bool multiRecordSplit(ProgramNode node, out multiRecordSplit value)
				{
					multiRecordSplit? multiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit.CreateSafe(this._builders, node);
					if (multiRecordSplit == null)
					{
						value = default(multiRecordSplit);
						return false;
					}
					value = multiRecordSplit.Value;
					return true;
				}

				// Token: 0x060034AA RID: 13482 RVA: 0x000A6EF4 File Offset: 0x000A50F4
				public bool rowRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034AB RID: 13483 RVA: 0x000A6F18 File Offset: 0x000A5118
				public bool rowRecords(ProgramNode node, out rowRecords value)
				{
					rowRecords? rowRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords.CreateSafe(this._builders, node);
					if (rowRecords == null)
					{
						value = default(rowRecords);
						return false;
					}
					value = rowRecords.Value;
					return true;
				}

				// Token: 0x060034AC RID: 13484 RVA: 0x000A6F54 File Offset: 0x000A5154
				public bool mapColumnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034AD RID: 13485 RVA: 0x000A6F78 File Offset: 0x000A5178
				public bool mapColumnSelectors(ProgramNode node, out mapColumnSelectors value)
				{
					mapColumnSelectors? mapColumnSelectors = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors.CreateSafe(this._builders, node);
					if (mapColumnSelectors == null)
					{
						value = default(mapColumnSelectors);
						return false;
					}
					value = mapColumnSelectors.Value;
					return true;
				}

				// Token: 0x060034AE RID: 13486 RVA: 0x000A6FB4 File Offset: 0x000A51B4
				public bool rowRecord(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034AF RID: 13487 RVA: 0x000A6FD8 File Offset: 0x000A51D8
				public bool rowRecord(ProgramNode node, out rowRecord value)
				{
					rowRecord? rowRecord = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord.CreateSafe(this._builders, node);
					if (rowRecord == null)
					{
						value = default(rowRecord);
						return false;
					}
					value = rowRecord.Value;
					return true;
				}

				// Token: 0x060034B0 RID: 13488 RVA: 0x000A7014 File Offset: 0x000A5214
				public bool columnSelectorList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034B1 RID: 13489 RVA: 0x000A7038 File Offset: 0x000A5238
				public bool columnSelectorList(ProgramNode node, out columnSelectorList value)
				{
					columnSelectorList? columnSelectorList = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList.CreateSafe(this._builders, node);
					if (columnSelectorList == null)
					{
						value = default(columnSelectorList);
						return false;
					}
					value = columnSelectorList.Value;
					return true;
				}

				// Token: 0x060034B2 RID: 13490 RVA: 0x000A7074 File Offset: 0x000A5274
				public bool columnSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034B3 RID: 13491 RVA: 0x000A7098 File Offset: 0x000A5298
				public bool columnSelector(ProgramNode node, out columnSelector value)
				{
					columnSelector? columnSelector = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector.CreateSafe(this._builders, node);
					if (columnSelector == null)
					{
						value = default(columnSelector);
						return false;
					}
					value = columnSelector.Value;
					return true;
				}

				// Token: 0x060034B4 RID: 13492 RVA: 0x000A70D4 File Offset: 0x000A52D4
				public bool primarySelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034B5 RID: 13493 RVA: 0x000A70F8 File Offset: 0x000A52F8
				public bool primarySelector(ProgramNode node, out primarySelector value)
				{
					primarySelector? primarySelector = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector.CreateSafe(this._builders, node);
					if (primarySelector == null)
					{
						value = default(primarySelector);
						return false;
					}
					value = primarySelector.Value;
					return true;
				}

				// Token: 0x060034B6 RID: 13494 RVA: 0x000A7134 File Offset: 0x000A5334
				public bool delimiterSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034B7 RID: 13495 RVA: 0x000A7158 File Offset: 0x000A5358
				public bool delimiterSplit(ProgramNode node, out delimiterSplit value)
				{
					delimiterSplit? delimiterSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit.CreateSafe(this._builders, node);
					if (delimiterSplit == null)
					{
						value = default(delimiterSplit);
						return false;
					}
					value = delimiterSplit.Value;
					return true;
				}

				// Token: 0x060034B8 RID: 13496 RVA: 0x000A7194 File Offset: 0x000A5394
				public bool record(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034B9 RID: 13497 RVA: 0x000A71B8 File Offset: 0x000A53B8
				public bool record(ProgramNode node, out record value)
				{
					record? record = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record.CreateSafe(this._builders, node);
					if (record == null)
					{
						value = default(record);
						return false;
					}
					value = record.Value;
					return true;
				}

				// Token: 0x060034BA RID: 13498 RVA: 0x000A71F4 File Offset: 0x000A53F4
				public bool splitTextProg(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034BB RID: 13499 RVA: 0x000A7218 File Offset: 0x000A5418
				public bool splitTextProg(ProgramNode node, out splitTextProg value)
				{
					splitTextProg? splitTextProg = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg.CreateSafe(this._builders, node);
					if (splitTextProg == null)
					{
						value = default(splitTextProg);
						return false;
					}
					value = splitTextProg.Value;
					return true;
				}

				// Token: 0x060034BC RID: 13500 RVA: 0x000A7254 File Offset: 0x000A5454
				public bool splitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034BD RID: 13501 RVA: 0x000A7278 File Offset: 0x000A5478
				public bool splitFile(ProgramNode node, out splitFile value)
				{
					splitFile? splitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile.CreateSafe(this._builders, node);
					if (splitFile == null)
					{
						value = default(splitFile);
						return false;
					}
					value = splitFile.Value;
					return true;
				}

				// Token: 0x060034BE RID: 13502 RVA: 0x000A72B4 File Offset: 0x000A54B4
				public bool allLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034BF RID: 13503 RVA: 0x000A72D8 File Offset: 0x000A54D8
				public bool allLines(ProgramNode node, out allLines value)
				{
					allLines? allLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines.CreateSafe(this._builders, node);
					if (allLines == null)
					{
						value = default(allLines);
						return false;
					}
					value = allLines.Value;
					return true;
				}

				// Token: 0x060034C0 RID: 13504 RVA: 0x000A7314 File Offset: 0x000A5514
				public bool r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034C1 RID: 13505 RVA: 0x000A7338 File Offset: 0x000A5538
				public bool r(ProgramNode node, out Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r value)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r? r = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						value = default(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r);
						return false;
					}
					value = r.Value;
					return true;
				}

				// Token: 0x060034C2 RID: 13506 RVA: 0x000A7374 File Offset: 0x000A5574
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034C3 RID: 13507 RVA: 0x000A7398 File Offset: 0x000A5598
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x060034C4 RID: 13508 RVA: 0x000A73D4 File Offset: 0x000A55D4
				public bool quotingConfig(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034C5 RID: 13509 RVA: 0x000A73F8 File Offset: 0x000A55F8
				public bool quotingConfig(ProgramNode node, out quotingConfig value)
				{
					quotingConfig? quotingConfig = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig.CreateSafe(this._builders, node);
					if (quotingConfig == null)
					{
						value = default(quotingConfig);
						return false;
					}
					value = quotingConfig.Value;
					return true;
				}

				// Token: 0x060034C6 RID: 13510 RVA: 0x000A7434 File Offset: 0x000A5634
				public bool delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034C7 RID: 13511 RVA: 0x000A7458 File Offset: 0x000A5658
				public bool delimiter(ProgramNode node, out Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter value)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter? delimiter = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						value = default(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter);
						return false;
					}
					value = delimiter.Value;
					return true;
				}

				// Token: 0x060034C8 RID: 13512 RVA: 0x000A7494 File Offset: 0x000A5694
				public bool headerIndex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034C9 RID: 13513 RVA: 0x000A74B8 File Offset: 0x000A56B8
				public bool headerIndex(ProgramNode node, out headerIndex value)
				{
					headerIndex? headerIndex = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex.CreateSafe(this._builders, node);
					if (headerIndex == null)
					{
						value = default(headerIndex);
						return false;
					}
					value = headerIndex.Value;
					return true;
				}

				// Token: 0x060034CA RID: 13514 RVA: 0x000A74F4 File Offset: 0x000A56F4
				public bool commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034CB RID: 13515 RVA: 0x000A7518 File Offset: 0x000A5718
				public bool commentStr(ProgramNode node, out commentStr value)
				{
					commentStr? commentStr = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
					if (commentStr == null)
					{
						value = default(commentStr);
						return false;
					}
					value = commentStr.Value;
					return true;
				}

				// Token: 0x060034CC RID: 13516 RVA: 0x000A7554 File Offset: 0x000A5754
				public bool skipEmpty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034CD RID: 13517 RVA: 0x000A7578 File Offset: 0x000A5778
				public bool skipEmpty(ProgramNode node, out skipEmpty value)
				{
					skipEmpty? skipEmpty = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty.CreateSafe(this._builders, node);
					if (skipEmpty == null)
					{
						value = default(skipEmpty);
						return false;
					}
					value = skipEmpty.Value;
					return true;
				}

				// Token: 0x060034CE RID: 13518 RVA: 0x000A75B4 File Offset: 0x000A57B4
				public bool hasCommentHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034CF RID: 13519 RVA: 0x000A75D8 File Offset: 0x000A57D8
				public bool hasCommentHeader(ProgramNode node, out hasCommentHeader value)
				{
					hasCommentHeader? hasCommentHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader.CreateSafe(this._builders, node);
					if (hasCommentHeader == null)
					{
						value = default(hasCommentHeader);
						return false;
					}
					value = hasCommentHeader.Value;
					return true;
				}

				// Token: 0x060034D0 RID: 13520 RVA: 0x000A7614 File Offset: 0x000A5814
				public bool splitLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034D1 RID: 13521 RVA: 0x000A7638 File Offset: 0x000A5838
				public bool splitLines(ProgramNode node, out splitLines value)
				{
					splitLines? splitLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines.CreateSafe(this._builders, node);
					if (splitLines == null)
					{
						value = default(splitLines);
						return false;
					}
					value = splitLines.Value;
					return true;
				}

				// Token: 0x060034D2 RID: 13522 RVA: 0x000A7674 File Offset: 0x000A5874
				public bool ls(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034D3 RID: 13523 RVA: 0x000A7698 File Offset: 0x000A5898
				public bool ls(ProgramNode node, out ls value)
				{
					ls? ls = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls.CreateSafe(this._builders, node);
					if (ls == null)
					{
						value = default(ls);
						return false;
					}
					value = ls.Value;
					return true;
				}

				// Token: 0x060034D4 RID: 13524 RVA: 0x000A76D4 File Offset: 0x000A58D4
				public bool dataLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034D5 RID: 13525 RVA: 0x000A76F8 File Offset: 0x000A58F8
				public bool dataLines(ProgramNode node, out dataLines value)
				{
					dataLines? dataLines = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines.CreateSafe(this._builders, node);
					if (dataLines == null)
					{
						value = default(dataLines);
						return false;
					}
					value = dataLines.Value;
					return true;
				}

				// Token: 0x060034D6 RID: 13526 RVA: 0x000A7734 File Offset: 0x000A5934
				public bool s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034D7 RID: 13527 RVA: 0x000A7758 File Offset: 0x000A5958
				public bool s(ProgramNode node, out Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s value)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s? s = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						value = default(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s);
						return false;
					}
					value = s.Value;
					return true;
				}

				// Token: 0x060034D8 RID: 13528 RVA: 0x000A7794 File Offset: 0x000A5994
				public bool skippedRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034D9 RID: 13529 RVA: 0x000A77B8 File Offset: 0x000A59B8
				public bool skippedRecords(ProgramNode node, out skippedRecords value)
				{
					skippedRecords? skippedRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords.CreateSafe(this._builders, node);
					if (skippedRecords == null)
					{
						value = default(skippedRecords);
						return false;
					}
					value = skippedRecords.Value;
					return true;
				}

				// Token: 0x060034DA RID: 13530 RVA: 0x000A77F4 File Offset: 0x000A59F4
				public bool skippedFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034DB RID: 13531 RVA: 0x000A7818 File Offset: 0x000A5A18
				public bool skippedFooter(ProgramNode node, out skippedFooter value)
				{
					skippedFooter? skippedFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter.CreateSafe(this._builders, node);
					if (skippedFooter == null)
					{
						value = default(skippedFooter);
						return false;
					}
					value = skippedFooter.Value;
					return true;
				}

				// Token: 0x060034DC RID: 13532 RVA: 0x000A7854 File Offset: 0x000A5A54
				public bool allRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034DD RID: 13533 RVA: 0x000A7878 File Offset: 0x000A5A78
				public bool allRecords(ProgramNode node, out allRecords value)
				{
					allRecords? allRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords.CreateSafe(this._builders, node);
					if (allRecords == null)
					{
						value = default(allRecords);
						return false;
					}
					value = allRecords.Value;
					return true;
				}

				// Token: 0x060034DE RID: 13534 RVA: 0x000A78B4 File Offset: 0x000A5AB4
				public bool basicLinePredicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034DF RID: 13535 RVA: 0x000A78D8 File Offset: 0x000A5AD8
				public bool basicLinePredicate(ProgramNode node, out basicLinePredicate value)
				{
					basicLinePredicate? basicLinePredicate = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate.CreateSafe(this._builders, node);
					if (basicLinePredicate == null)
					{
						value = default(basicLinePredicate);
						return false;
					}
					value = basicLinePredicate.Value;
					return true;
				}

				// Token: 0x060034E0 RID: 13536 RVA: 0x000A7914 File Offset: 0x000A5B14
				public bool splitSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034E1 RID: 13537 RVA: 0x000A7938 File Offset: 0x000A5B38
				public bool splitSequence(ProgramNode node, out splitSequence value)
				{
					splitSequence? splitSequence = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence.CreateSafe(this._builders, node);
					if (splitSequence == null)
					{
						value = default(splitSequence);
						return false;
					}
					value = splitSequence.Value;
					return true;
				}

				// Token: 0x060034E2 RID: 13538 RVA: 0x000A7974 File Offset: 0x000A5B74
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034E3 RID: 13539 RVA: 0x000A7998 File Offset: 0x000A5B98
				public bool _LetB0(ProgramNode node, out Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0 value)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x060034E4 RID: 13540 RVA: 0x000A79D4 File Offset: 0x000A5BD4
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034E5 RID: 13541 RVA: 0x000A79F8 File Offset: 0x000A5BF8
				public bool _LetB1(ProgramNode node, out Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1 value)
				{
					Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1? letB = Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x040019EE RID: 6638
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000928 RID: 2344
			public class RuleIs
			{
				// Token: 0x060034E6 RID: 13542 RVA: 0x000A7A32 File Offset: 0x000A5C32
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060034E7 RID: 13543 RVA: 0x000A7A44 File Offset: 0x000A5C44
				public bool LetFileRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetFileRecordSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034E8 RID: 13544 RVA: 0x000A7A68 File Offset: 0x000A5C68
				public bool LetFileRecordSplit(ProgramNode node, out LetFileRecordSplit value)
				{
					LetFileRecordSplit? letFileRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetFileRecordSplit.CreateSafe(this._builders, node);
					if (letFileRecordSplit == null)
					{
						value = default(LetFileRecordSplit);
						return false;
					}
					value = letFileRecordSplit.Value;
					return true;
				}

				// Token: 0x060034E9 RID: 13545 RVA: 0x000A7AA4 File Offset: 0x000A5CA4
				public bool SelectColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034EA RID: 13546 RVA: 0x000A7AC8 File Offset: 0x000A5CC8
				public bool SelectColumns(ProgramNode node, out SelectColumns value)
				{
					SelectColumns? selectColumns = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectColumns.CreateSafe(this._builders, node);
					if (selectColumns == null)
					{
						value = default(SelectColumns);
						return false;
					}
					value = selectColumns.Value;
					return true;
				}

				// Token: 0x060034EB RID: 13547 RVA: 0x000A7B04 File Offset: 0x000A5D04
				public bool splitRecordsSelect_splitRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.splitRecordsSelect_splitRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034EC RID: 13548 RVA: 0x000A7B28 File Offset: 0x000A5D28
				public bool splitRecordsSelect_splitRecords(ProgramNode node, out splitRecordsSelect_splitRecords value)
				{
					splitRecordsSelect_splitRecords? splitRecordsSelect_splitRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.splitRecordsSelect_splitRecords.CreateSafe(this._builders, node);
					if (splitRecordsSelect_splitRecords == null)
					{
						value = default(splitRecordsSelect_splitRecords);
						return false;
					}
					value = splitRecordsSelect_splitRecords.Value;
					return true;
				}

				// Token: 0x060034ED RID: 13549 RVA: 0x000A7B64 File Offset: 0x000A5D64
				public bool NoSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.NoSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034EE RID: 13550 RVA: 0x000A7B88 File Offset: 0x000A5D88
				public bool NoSplit(ProgramNode node, out NoSplit value)
				{
					NoSplit? noSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.NoSplit.CreateSafe(this._builders, node);
					if (noSplit == null)
					{
						value = default(NoSplit);
						return false;
					}
					value = noSplit.Value;
					return true;
				}

				// Token: 0x060034EF RID: 13551 RVA: 0x000A7BC4 File Offset: 0x000A5DC4
				public bool TableFromCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TableFromCells.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034F0 RID: 13552 RVA: 0x000A7BE8 File Offset: 0x000A5DE8
				public bool TableFromCells(ProgramNode node, out TableFromCells value)
				{
					TableFromCells? tableFromCells = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TableFromCells.CreateSafe(this._builders, node);
					if (tableFromCells == null)
					{
						value = default(TableFromCells);
						return false;
					}
					value = tableFromCells.Value;
					return true;
				}

				// Token: 0x060034F1 RID: 13553 RVA: 0x000A7C24 File Offset: 0x000A5E24
				public bool MultiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MultiRecordSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034F2 RID: 13554 RVA: 0x000A7C48 File Offset: 0x000A5E48
				public bool MultiRecordSplit(ProgramNode node, out MultiRecordSplit value)
				{
					MultiRecordSplit? multiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MultiRecordSplit.CreateSafe(this._builders, node);
					if (multiRecordSplit == null)
					{
						value = default(MultiRecordSplit);
						return false;
					}
					value = multiRecordSplit.Value;
					return true;
				}

				// Token: 0x060034F3 RID: 13555 RVA: 0x000A7C84 File Offset: 0x000A5E84
				public bool LetMultiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetMultiRecordSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034F4 RID: 13556 RVA: 0x000A7CA8 File Offset: 0x000A5EA8
				public bool LetMultiRecordSplit(ProgramNode node, out LetMultiRecordSplit value)
				{
					LetMultiRecordSplit? letMultiRecordSplit = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetMultiRecordSplit.CreateSafe(this._builders, node);
					if (letMultiRecordSplit == null)
					{
						value = default(LetMultiRecordSplit);
						return false;
					}
					value = letMultiRecordSplit.Value;
					return true;
				}

				// Token: 0x060034F5 RID: 13557 RVA: 0x000A7CE4 File Offset: 0x000A5EE4
				public bool MapColumnSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MapColumnSelector.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034F6 RID: 13558 RVA: 0x000A7D08 File Offset: 0x000A5F08
				public bool MapColumnSelector(ProgramNode node, out MapColumnSelector value)
				{
					MapColumnSelector? mapColumnSelector = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MapColumnSelector.CreateSafe(this._builders, node);
					if (mapColumnSelector == null)
					{
						value = default(MapColumnSelector);
						return false;
					}
					value = mapColumnSelector.Value;
					return true;
				}

				// Token: 0x060034F7 RID: 13559 RVA: 0x000A7D44 File Offset: 0x000A5F44
				public bool Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034F8 RID: 13560 RVA: 0x000A7D68 File Offset: 0x000A5F68
				public bool Empty(ProgramNode node, out Empty value)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						value = default(Empty);
						return false;
					}
					value = empty.Value;
					return true;
				}

				// Token: 0x060034F9 RID: 13561 RVA: 0x000A7DA4 File Offset: 0x000A5FA4
				public bool SelectorList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectorList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034FA RID: 13562 RVA: 0x000A7DC8 File Offset: 0x000A5FC8
				public bool SelectorList(ProgramNode node, out SelectorList value)
				{
					SelectorList? selectorList = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectorList.CreateSafe(this._builders, node);
					if (selectorList == null)
					{
						value = default(SelectorList);
						return false;
					}
					value = selectorList.Value;
					return true;
				}

				// Token: 0x060034FB RID: 13563 RVA: 0x000A7E04 File Offset: 0x000A6004
				public bool KthLine(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthLine.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034FC RID: 13564 RVA: 0x000A7E28 File Offset: 0x000A6028
				public bool KthLine(ProgramNode node, out KthLine value)
				{
					KthLine? kthLine = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthLine.CreateSafe(this._builders, node);
					if (kthLine == null)
					{
						value = default(KthLine);
						return false;
					}
					value = kthLine.Value;
					return true;
				}

				// Token: 0x060034FD RID: 13565 RVA: 0x000A7E64 File Offset: 0x000A6064
				public bool KthKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060034FE RID: 13566 RVA: 0x000A7E88 File Offset: 0x000A6088
				public bool KthKeyValue(ProgramNode node, out KthKeyValue value)
				{
					KthKeyValue? kthKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValue.CreateSafe(this._builders, node);
					if (kthKeyValue == null)
					{
						value = default(KthKeyValue);
						return false;
					}
					value = kthKeyValue.Value;
					return true;
				}

				// Token: 0x060034FF RID: 13567 RVA: 0x000A7EC4 File Offset: 0x000A60C4
				public bool KthTwoLineKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthTwoLineKeyValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003500 RID: 13568 RVA: 0x000A7EE8 File Offset: 0x000A60E8
				public bool KthTwoLineKeyValue(ProgramNode node, out KthTwoLineKeyValue value)
				{
					KthTwoLineKeyValue? kthTwoLineKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthTwoLineKeyValue.CreateSafe(this._builders, node);
					if (kthTwoLineKeyValue == null)
					{
						value = default(KthTwoLineKeyValue);
						return false;
					}
					value = kthTwoLineKeyValue.Value;
					return true;
				}

				// Token: 0x06003501 RID: 13569 RVA: 0x000A7F24 File Offset: 0x000A6124
				public bool KthKeyQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyQuote.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003502 RID: 13570 RVA: 0x000A7F48 File Offset: 0x000A6148
				public bool KthKeyQuote(ProgramNode node, out KthKeyQuote value)
				{
					KthKeyQuote? kthKeyQuote = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyQuote.CreateSafe(this._builders, node);
					if (kthKeyQuote == null)
					{
						value = default(KthKeyQuote);
						return false;
					}
					value = kthKeyQuote.Value;
					return true;
				}

				// Token: 0x06003503 RID: 13571 RVA: 0x000A7F84 File Offset: 0x000A6184
				public bool KthKeyValueFw(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValueFw.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003504 RID: 13572 RVA: 0x000A7FA8 File Offset: 0x000A61A8
				public bool KthKeyValueFw(ProgramNode node, out KthKeyValueFw value)
				{
					KthKeyValueFw? kthKeyValueFw = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValueFw.CreateSafe(this._builders, node);
					if (kthKeyValueFw == null)
					{
						value = default(KthKeyValueFw);
						return false;
					}
					value = kthKeyValueFw.Value;
					return true;
				}

				// Token: 0x06003505 RID: 13573 RVA: 0x000A7FE4 File Offset: 0x000A61E4
				public bool BreakLine(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.BreakLine.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003506 RID: 13574 RVA: 0x000A8008 File Offset: 0x000A6208
				public bool BreakLine(ProgramNode node, out BreakLine value)
				{
					BreakLine? breakLine = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.BreakLine.CreateSafe(this._builders, node);
					if (breakLine == null)
					{
						value = default(BreakLine);
						return false;
					}
					value = breakLine.Value;
					return true;
				}

				// Token: 0x06003507 RID: 13575 RVA: 0x000A8044 File Offset: 0x000A6244
				public bool TwoLineKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TwoLineKeyValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003508 RID: 13576 RVA: 0x000A8068 File Offset: 0x000A6268
				public bool TwoLineKeyValue(ProgramNode node, out TwoLineKeyValue value)
				{
					TwoLineKeyValue? twoLineKeyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TwoLineKeyValue.CreateSafe(this._builders, node);
					if (twoLineKeyValue == null)
					{
						value = default(TwoLineKeyValue);
						return false;
					}
					value = twoLineKeyValue.Value;
					return true;
				}

				// Token: 0x06003509 RID: 13577 RVA: 0x000A80A4 File Offset: 0x000A62A4
				public bool KeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600350A RID: 13578 RVA: 0x000A80C8 File Offset: 0x000A62C8
				public bool KeyValue(ProgramNode node, out KeyValue value)
				{
					KeyValue? keyValue = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyValue.CreateSafe(this._builders, node);
					if (keyValue == null)
					{
						value = default(KeyValue);
						return false;
					}
					value = keyValue.Value;
					return true;
				}

				// Token: 0x0600350B RID: 13579 RVA: 0x000A8104 File Offset: 0x000A6304
				public bool KeyQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyQuote.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600350C RID: 13580 RVA: 0x000A8128 File Offset: 0x000A6328
				public bool KeyQuote(ProgramNode node, out KeyQuote value)
				{
					KeyQuote? keyQuote = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyQuote.CreateSafe(this._builders, node);
					if (keyQuote == null)
					{
						value = default(KeyQuote);
						return false;
					}
					value = keyQuote.Value;
					return true;
				}

				// Token: 0x0600350D RID: 13581 RVA: 0x000A8164 File Offset: 0x000A6364
				public bool SplitToCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitToCells.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600350E RID: 13582 RVA: 0x000A8188 File Offset: 0x000A6388
				public bool SplitToCells(ProgramNode node, out SplitToCells value)
				{
					SplitToCells? splitToCells = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitToCells.CreateSafe(this._builders, node);
					if (splitToCells == null)
					{
						value = default(SplitToCells);
						return false;
					}
					value = splitToCells.Value;
					return true;
				}

				// Token: 0x0600350F RID: 13583 RVA: 0x000A81C4 File Offset: 0x000A63C4
				public bool SplitTextProg(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitTextProg.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003510 RID: 13584 RVA: 0x000A81E8 File Offset: 0x000A63E8
				public bool SplitTextProg(ProgramNode node, out SplitTextProg value)
				{
					SplitTextProg? splitTextProg = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitTextProg.CreateSafe(this._builders, node);
					if (splitTextProg == null)
					{
						value = default(SplitTextProg);
						return false;
					}
					value = splitTextProg.Value;
					return true;
				}

				// Token: 0x06003511 RID: 13585 RVA: 0x000A8224 File Offset: 0x000A6424
				public bool SplitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitFile.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003512 RID: 13586 RVA: 0x000A8248 File Offset: 0x000A6448
				public bool SplitFile(ProgramNode node, out SplitFile value)
				{
					SplitFile? splitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitFile.CreateSafe(this._builders, node);
					if (splitFile == null)
					{
						value = default(SplitFile);
						return false;
					}
					value = splitFile.Value;
					return true;
				}

				// Token: 0x06003513 RID: 13587 RVA: 0x000A8284 File Offset: 0x000A6484
				public bool MergeRecordLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MergeRecordLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003514 RID: 13588 RVA: 0x000A82A8 File Offset: 0x000A64A8
				public bool MergeRecordLines(ProgramNode node, out MergeRecordLines value)
				{
					MergeRecordLines? mergeRecordLines = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MergeRecordLines.CreateSafe(this._builders, node);
					if (mergeRecordLines == null)
					{
						value = default(MergeRecordLines);
						return false;
					}
					value = mergeRecordLines.Value;
					return true;
				}

				// Token: 0x06003515 RID: 13589 RVA: 0x000A82E4 File Offset: 0x000A64E4
				public bool LetSplitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetSplitFile.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003516 RID: 13590 RVA: 0x000A8308 File Offset: 0x000A6508
				public bool LetSplitFile(ProgramNode node, out LetSplitFile value)
				{
					LetSplitFile? letSplitFile = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetSplitFile.CreateSafe(this._builders, node);
					if (letSplitFile == null)
					{
						value = default(LetSplitFile);
						return false;
					}
					value = letSplitFile.Value;
					return true;
				}

				// Token: 0x06003517 RID: 13591 RVA: 0x000A8344 File Offset: 0x000A6544
				public bool SplitSequenceLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequenceLet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003518 RID: 13592 RVA: 0x000A8368 File Offset: 0x000A6568
				public bool SplitSequenceLet(ProgramNode node, out SplitSequenceLet value)
				{
					SplitSequenceLet? splitSequenceLet = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequenceLet.CreateSafe(this._builders, node);
					if (splitSequenceLet == null)
					{
						value = default(SplitSequenceLet);
						return false;
					}
					value = splitSequenceLet.Value;
					return true;
				}

				// Token: 0x06003519 RID: 13593 RVA: 0x000A83A4 File Offset: 0x000A65A4
				public bool FilterHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterHeader.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600351A RID: 13594 RVA: 0x000A83C8 File Offset: 0x000A65C8
				public bool FilterHeader(ProgramNode node, out FilterHeader value)
				{
					FilterHeader? filterHeader = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterHeader.CreateSafe(this._builders, node);
					if (filterHeader == null)
					{
						value = default(FilterHeader);
						return false;
					}
					value = filterHeader.Value;
					return true;
				}

				// Token: 0x0600351B RID: 13595 RVA: 0x000A8404 File Offset: 0x000A6604
				public bool SelectData(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectData.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600351C RID: 13596 RVA: 0x000A8428 File Offset: 0x000A6628
				public bool SelectData(ProgramNode node, out SelectData value)
				{
					SelectData? selectData = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectData.CreateSafe(this._builders, node);
					if (selectData == null)
					{
						value = default(SelectData);
						return false;
					}
					value = selectData.Value;
					return true;
				}

				// Token: 0x0600351D RID: 13597 RVA: 0x000A8464 File Offset: 0x000A6664
				public bool FilterRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600351E RID: 13598 RVA: 0x000A8488 File Offset: 0x000A6688
				public bool FilterRecords(ProgramNode node, out FilterRecords value)
				{
					FilterRecords? filterRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterRecords.CreateSafe(this._builders, node);
					if (filterRecords == null)
					{
						value = default(FilterRecords);
						return false;
					}
					value = filterRecords.Value;
					return true;
				}

				// Token: 0x0600351F RID: 13599 RVA: 0x000A84C4 File Offset: 0x000A66C4
				public bool dataLines_skippedRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.dataLines_skippedRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003520 RID: 13600 RVA: 0x000A84E8 File Offset: 0x000A66E8
				public bool dataLines_skippedRecords(ProgramNode node, out dataLines_skippedRecords value)
				{
					dataLines_skippedRecords? dataLines_skippedRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.dataLines_skippedRecords.CreateSafe(this._builders, node);
					if (dataLines_skippedRecords == null)
					{
						value = default(dataLines_skippedRecords);
						return false;
					}
					value = dataLines_skippedRecords.Value;
					return true;
				}

				// Token: 0x06003521 RID: 13601 RVA: 0x000A8524 File Offset: 0x000A6724
				public bool Skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003522 RID: 13602 RVA: 0x000A8548 File Offset: 0x000A6748
				public bool Skip(ProgramNode node, out Skip value)
				{
					Skip? skip = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						value = default(Skip);
						return false;
					}
					value = skip.Value;
					return true;
				}

				// Token: 0x06003523 RID: 13603 RVA: 0x000A8584 File Offset: 0x000A6784
				public bool skippedRecords_skippedFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedRecords_skippedFooter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003524 RID: 13604 RVA: 0x000A85A8 File Offset: 0x000A67A8
				public bool skippedRecords_skippedFooter(ProgramNode node, out skippedRecords_skippedFooter value)
				{
					skippedRecords_skippedFooter? skippedRecords_skippedFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedRecords_skippedFooter.CreateSafe(this._builders, node);
					if (skippedRecords_skippedFooter == null)
					{
						value = default(skippedRecords_skippedFooter);
						return false;
					}
					value = skippedRecords_skippedFooter.Value;
					return true;
				}

				// Token: 0x06003525 RID: 13605 RVA: 0x000A85E4 File Offset: 0x000A67E4
				public bool SkipFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SkipFooter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003526 RID: 13606 RVA: 0x000A8608 File Offset: 0x000A6808
				public bool SkipFooter(ProgramNode node, out SkipFooter value)
				{
					SkipFooter? skipFooter = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SkipFooter.CreateSafe(this._builders, node);
					if (skipFooter == null)
					{
						value = default(SkipFooter);
						return false;
					}
					value = skipFooter.Value;
					return true;
				}

				// Token: 0x06003527 RID: 13607 RVA: 0x000A8644 File Offset: 0x000A6844
				public bool skippedFooter_allRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedFooter_allRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003528 RID: 13608 RVA: 0x000A8668 File Offset: 0x000A6868
				public bool skippedFooter_allRecords(ProgramNode node, out skippedFooter_allRecords value)
				{
					skippedFooter_allRecords? skippedFooter_allRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedFooter_allRecords.CreateSafe(this._builders, node);
					if (skippedFooter_allRecords == null)
					{
						value = default(skippedFooter_allRecords);
						return false;
					}
					value = skippedFooter_allRecords.Value;
					return true;
				}

				// Token: 0x06003529 RID: 13609 RVA: 0x000A86A4 File Offset: 0x000A68A4
				public bool allRecords_allLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.allRecords_allLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600352A RID: 13610 RVA: 0x000A86C8 File Offset: 0x000A68C8
				public bool allRecords_allLines(ProgramNode node, out allRecords_allLines value)
				{
					allRecords_allLines? allRecords_allLines = Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.allRecords_allLines.CreateSafe(this._builders, node);
					if (allRecords_allLines == null)
					{
						value = default(allRecords_allLines);
						return false;
					}
					value = allRecords_allLines.Value;
					return true;
				}

				// Token: 0x0600352B RID: 13611 RVA: 0x000A8704 File Offset: 0x000A6904
				public bool QuoteRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.QuoteRecords.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600352C RID: 13612 RVA: 0x000A8728 File Offset: 0x000A6928
				public bool QuoteRecords(ProgramNode node, out QuoteRecords value)
				{
					QuoteRecords? quoteRecords = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.QuoteRecords.CreateSafe(this._builders, node);
					if (quoteRecords == null)
					{
						value = default(QuoteRecords);
						return false;
					}
					value = quoteRecords.Value;
					return true;
				}

				// Token: 0x0600352D RID: 13613 RVA: 0x000A8764 File Offset: 0x000A6964
				public bool StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600352E RID: 13614 RVA: 0x000A8788 File Offset: 0x000A6988
				public bool StartsWith(ProgramNode node, out StartsWith value)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						value = default(StartsWith);
						return false;
					}
					value = startsWith.Value;
					return true;
				}

				// Token: 0x0600352F RID: 13615 RVA: 0x000A87C4 File Offset: 0x000A69C4
				public bool SplitSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003530 RID: 13616 RVA: 0x000A87E8 File Offset: 0x000A69E8
				public bool SplitSequence(ProgramNode node, out SplitSequence value)
				{
					SplitSequence? splitSequence = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequence.CreateSafe(this._builders, node);
					if (splitSequence == null)
					{
						value = default(SplitSequence);
						return false;
					}
					value = splitSequence.Value;
					return true;
				}

				// Token: 0x06003531 RID: 13617 RVA: 0x000A8824 File Offset: 0x000A6A24
				public bool Sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003532 RID: 13618 RVA: 0x000A8848 File Offset: 0x000A6A48
				public bool Sequence(ProgramNode node, out Sequence value)
				{
					Sequence? sequence = Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						value = default(Sequence);
						return false;
					}
					value = sequence.Value;
					return true;
				}

				// Token: 0x040019EF RID: 6639
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000929 RID: 2345
			public class NodeAs
			{
				// Token: 0x06003533 RID: 13619 RVA: 0x000A8882 File Offset: 0x000A6A82
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003534 RID: 13620 RVA: 0x000A8891 File Offset: 0x000A6A91
				public hasHeader? hasHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader.CreateSafe(this._builders, node);
				}

				// Token: 0x06003535 RID: 13621 RVA: 0x000A889F File Offset: 0x000A6A9F
				public columnList? columnList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList.CreateSafe(this._builders, node);
				}

				// Token: 0x06003536 RID: 13622 RVA: 0x000A88AD File Offset: 0x000A6AAD
				public topSplit? topSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003537 RID: 13623 RVA: 0x000A88BB File Offset: 0x000A6ABB
				public records? records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records.CreateSafe(this._builders, node);
				}

				// Token: 0x06003538 RID: 13624 RVA: 0x000A88C9 File Offset: 0x000A6AC9
				public splitRecordsSelect? splitRecordsSelect(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect.CreateSafe(this._builders, node);
				}

				// Token: 0x06003539 RID: 13625 RVA: 0x000A88D7 File Offset: 0x000A6AD7
				public splitRecords? splitRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353A RID: 13626 RVA: 0x000A88E5 File Offset: 0x000A6AE5
				public key? key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353B RID: 13627 RVA: 0x000A88F3 File Offset: 0x000A6AF3
				public sep? sep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353C RID: 13628 RVA: 0x000A8901 File Offset: 0x000A6B01
				public newLineSep? newLineSep(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353D RID: 13629 RVA: 0x000A890F File Offset: 0x000A6B0F
				public fwPos? fwPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353E RID: 13630 RVA: 0x000A891D File Offset: 0x000A6B1D
				public multiRecordSplit? multiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600353F RID: 13631 RVA: 0x000A892B File Offset: 0x000A6B2B
				public rowRecords? rowRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x06003540 RID: 13632 RVA: 0x000A8939 File Offset: 0x000A6B39
				public mapColumnSelectors? mapColumnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors.CreateSafe(this._builders, node);
				}

				// Token: 0x06003541 RID: 13633 RVA: 0x000A8947 File Offset: 0x000A6B47
				public rowRecord? rowRecord(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord.CreateSafe(this._builders, node);
				}

				// Token: 0x06003542 RID: 13634 RVA: 0x000A8955 File Offset: 0x000A6B55
				public columnSelectorList? columnSelectorList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList.CreateSafe(this._builders, node);
				}

				// Token: 0x06003543 RID: 13635 RVA: 0x000A8963 File Offset: 0x000A6B63
				public columnSelector? columnSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector.CreateSafe(this._builders, node);
				}

				// Token: 0x06003544 RID: 13636 RVA: 0x000A8971 File Offset: 0x000A6B71
				public primarySelector? primarySelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector.CreateSafe(this._builders, node);
				}

				// Token: 0x06003545 RID: 13637 RVA: 0x000A897F File Offset: 0x000A6B7F
				public delimiterSplit? delimiterSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003546 RID: 13638 RVA: 0x000A898D File Offset: 0x000A6B8D
				public record? record(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record.CreateSafe(this._builders, node);
				}

				// Token: 0x06003547 RID: 13639 RVA: 0x000A899B File Offset: 0x000A6B9B
				public splitTextProg? splitTextProg(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg.CreateSafe(this._builders, node);
				}

				// Token: 0x06003548 RID: 13640 RVA: 0x000A89A9 File Offset: 0x000A6BA9
				public splitFile? splitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile.CreateSafe(this._builders, node);
				}

				// Token: 0x06003549 RID: 13641 RVA: 0x000A89B7 File Offset: 0x000A6BB7
				public allLines? allLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354A RID: 13642 RVA: 0x000A89C5 File Offset: 0x000A6BC5
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r? r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354B RID: 13643 RVA: 0x000A89D3 File Offset: 0x000A6BD3
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354C RID: 13644 RVA: 0x000A89E1 File Offset: 0x000A6BE1
				public quotingConfig? quotingConfig(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354D RID: 13645 RVA: 0x000A89EF File Offset: 0x000A6BEF
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter? delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354E RID: 13646 RVA: 0x000A89FD File Offset: 0x000A6BFD
				public headerIndex? headerIndex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex.CreateSafe(this._builders, node);
				}

				// Token: 0x0600354F RID: 13647 RVA: 0x000A8A0B File Offset: 0x000A6C0B
				public commentStr? commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
				}

				// Token: 0x06003550 RID: 13648 RVA: 0x000A8A19 File Offset: 0x000A6C19
				public skipEmpty? skipEmpty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty.CreateSafe(this._builders, node);
				}

				// Token: 0x06003551 RID: 13649 RVA: 0x000A8A27 File Offset: 0x000A6C27
				public hasCommentHeader? hasCommentHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader.CreateSafe(this._builders, node);
				}

				// Token: 0x06003552 RID: 13650 RVA: 0x000A8A35 File Offset: 0x000A6C35
				public splitLines? splitLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines.CreateSafe(this._builders, node);
				}

				// Token: 0x06003553 RID: 13651 RVA: 0x000A8A43 File Offset: 0x000A6C43
				public ls? ls(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls.CreateSafe(this._builders, node);
				}

				// Token: 0x06003554 RID: 13652 RVA: 0x000A8A51 File Offset: 0x000A6C51
				public dataLines? dataLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines.CreateSafe(this._builders, node);
				}

				// Token: 0x06003555 RID: 13653 RVA: 0x000A8A5F File Offset: 0x000A6C5F
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s? s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s.CreateSafe(this._builders, node);
				}

				// Token: 0x06003556 RID: 13654 RVA: 0x000A8A6D File Offset: 0x000A6C6D
				public skippedRecords? skippedRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x06003557 RID: 13655 RVA: 0x000A8A7B File Offset: 0x000A6C7B
				public skippedFooter? skippedFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter.CreateSafe(this._builders, node);
				}

				// Token: 0x06003558 RID: 13656 RVA: 0x000A8A89 File Offset: 0x000A6C89
				public allRecords? allRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x06003559 RID: 13657 RVA: 0x000A8A97 File Offset: 0x000A6C97
				public basicLinePredicate? basicLinePredicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600355A RID: 13658 RVA: 0x000A8AA5 File Offset: 0x000A6CA5
				public splitSequence? splitSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x0600355B RID: 13659 RVA: 0x000A8AB3 File Offset: 0x000A6CB3
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x0600355C RID: 13660 RVA: 0x000A8AC1 File Offset: 0x000A6CC1
				public Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x040019F0 RID: 6640
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200092A RID: 2346
			public class RuleAs
			{
				// Token: 0x0600355D RID: 13661 RVA: 0x000A8ACF File Offset: 0x000A6CCF
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600355E RID: 13662 RVA: 0x000A8ADE File Offset: 0x000A6CDE
				public LetFileRecordSplit? LetFileRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetFileRecordSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600355F RID: 13663 RVA: 0x000A8AEC File Offset: 0x000A6CEC
				public SelectColumns? SelectColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06003560 RID: 13664 RVA: 0x000A8AFA File Offset: 0x000A6CFA
				public splitRecordsSelect_splitRecords? splitRecordsSelect_splitRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.splitRecordsSelect_splitRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x06003561 RID: 13665 RVA: 0x000A8B08 File Offset: 0x000A6D08
				public NoSplit? NoSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.NoSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003562 RID: 13666 RVA: 0x000A8B16 File Offset: 0x000A6D16
				public TableFromCells? TableFromCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TableFromCells.CreateSafe(this._builders, node);
				}

				// Token: 0x06003563 RID: 13667 RVA: 0x000A8B24 File Offset: 0x000A6D24
				public MultiRecordSplit? MultiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MultiRecordSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003564 RID: 13668 RVA: 0x000A8B32 File Offset: 0x000A6D32
				public LetMultiRecordSplit? LetMultiRecordSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetMultiRecordSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003565 RID: 13669 RVA: 0x000A8B40 File Offset: 0x000A6D40
				public MapColumnSelector? MapColumnSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MapColumnSelector.CreateSafe(this._builders, node);
				}

				// Token: 0x06003566 RID: 13670 RVA: 0x000A8B4E File Offset: 0x000A6D4E
				public Empty? Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
				}

				// Token: 0x06003567 RID: 13671 RVA: 0x000A8B5C File Offset: 0x000A6D5C
				public SelectorList? SelectorList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectorList.CreateSafe(this._builders, node);
				}

				// Token: 0x06003568 RID: 13672 RVA: 0x000A8B6A File Offset: 0x000A6D6A
				public KthLine? KthLine(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthLine.CreateSafe(this._builders, node);
				}

				// Token: 0x06003569 RID: 13673 RVA: 0x000A8B78 File Offset: 0x000A6D78
				public KthKeyValue? KthKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356A RID: 13674 RVA: 0x000A8B86 File Offset: 0x000A6D86
				public KthTwoLineKeyValue? KthTwoLineKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthTwoLineKeyValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356B RID: 13675 RVA: 0x000A8B94 File Offset: 0x000A6D94
				public KthKeyQuote? KthKeyQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyQuote.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356C RID: 13676 RVA: 0x000A8BA2 File Offset: 0x000A6DA2
				public KthKeyValueFw? KthKeyValueFw(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KthKeyValueFw.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356D RID: 13677 RVA: 0x000A8BB0 File Offset: 0x000A6DB0
				public BreakLine? BreakLine(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.BreakLine.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356E RID: 13678 RVA: 0x000A8BBE File Offset: 0x000A6DBE
				public TwoLineKeyValue? TwoLineKeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.TwoLineKeyValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600356F RID: 13679 RVA: 0x000A8BCC File Offset: 0x000A6DCC
				public KeyValue? KeyValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyValue.CreateSafe(this._builders, node);
				}

				// Token: 0x06003570 RID: 13680 RVA: 0x000A8BDA File Offset: 0x000A6DDA
				public KeyQuote? KeyQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.KeyQuote.CreateSafe(this._builders, node);
				}

				// Token: 0x06003571 RID: 13681 RVA: 0x000A8BE8 File Offset: 0x000A6DE8
				public SplitToCells? SplitToCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitToCells.CreateSafe(this._builders, node);
				}

				// Token: 0x06003572 RID: 13682 RVA: 0x000A8BF6 File Offset: 0x000A6DF6
				public SplitTextProg? SplitTextProg(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitTextProg.CreateSafe(this._builders, node);
				}

				// Token: 0x06003573 RID: 13683 RVA: 0x000A8C04 File Offset: 0x000A6E04
				public SplitFile? SplitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitFile.CreateSafe(this._builders, node);
				}

				// Token: 0x06003574 RID: 13684 RVA: 0x000A8C12 File Offset: 0x000A6E12
				public MergeRecordLines? MergeRecordLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.MergeRecordLines.CreateSafe(this._builders, node);
				}

				// Token: 0x06003575 RID: 13685 RVA: 0x000A8C20 File Offset: 0x000A6E20
				public LetSplitFile? LetSplitFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.LetSplitFile.CreateSafe(this._builders, node);
				}

				// Token: 0x06003576 RID: 13686 RVA: 0x000A8C2E File Offset: 0x000A6E2E
				public SplitSequenceLet? SplitSequenceLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequenceLet.CreateSafe(this._builders, node);
				}

				// Token: 0x06003577 RID: 13687 RVA: 0x000A8C3C File Offset: 0x000A6E3C
				public FilterHeader? FilterHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterHeader.CreateSafe(this._builders, node);
				}

				// Token: 0x06003578 RID: 13688 RVA: 0x000A8C4A File Offset: 0x000A6E4A
				public SelectData? SelectData(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SelectData.CreateSafe(this._builders, node);
				}

				// Token: 0x06003579 RID: 13689 RVA: 0x000A8C58 File Offset: 0x000A6E58
				public FilterRecords? FilterRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.FilterRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357A RID: 13690 RVA: 0x000A8C66 File Offset: 0x000A6E66
				public dataLines_skippedRecords? dataLines_skippedRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.dataLines_skippedRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357B RID: 13691 RVA: 0x000A8C74 File Offset: 0x000A6E74
				public Skip? Skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357C RID: 13692 RVA: 0x000A8C82 File Offset: 0x000A6E82
				public skippedRecords_skippedFooter? skippedRecords_skippedFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedRecords_skippedFooter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357D RID: 13693 RVA: 0x000A8C90 File Offset: 0x000A6E90
				public SkipFooter? SkipFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SkipFooter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357E RID: 13694 RVA: 0x000A8C9E File Offset: 0x000A6E9E
				public skippedFooter_allRecords? skippedFooter_allRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.skippedFooter_allRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x0600357F RID: 13695 RVA: 0x000A8CAC File Offset: 0x000A6EAC
				public allRecords_allLines? allRecords_allLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes.allRecords_allLines.CreateSafe(this._builders, node);
				}

				// Token: 0x06003580 RID: 13696 RVA: 0x000A8CBA File Offset: 0x000A6EBA
				public QuoteRecords? QuoteRecords(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.QuoteRecords.CreateSafe(this._builders, node);
				}

				// Token: 0x06003581 RID: 13697 RVA: 0x000A8CC8 File Offset: 0x000A6EC8
				public StartsWith? StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
				}

				// Token: 0x06003582 RID: 13698 RVA: 0x000A8CD6 File Offset: 0x000A6ED6
				public SplitSequence? SplitSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.SplitSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x06003583 RID: 13699 RVA: 0x000A8CE4 File Offset: 0x000A6EE4
				public Sequence? Sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
				}

				// Token: 0x040019F1 RID: 6641
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x0200092C RID: 2348
		public class Sets
		{
			// Token: 0x06003587 RID: 13703 RVA: 0x000A8D0C File Offset: 0x000A6F0C
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x170009B5 RID: 2485
			// (get) Token: 0x06003588 RID: 13704 RVA: 0x000A8D5B File Offset: 0x000A6F5B
			// (set) Token: 0x06003589 RID: 13705 RVA: 0x000A8D63 File Offset: 0x000A6F63
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x170009B6 RID: 2486
			// (get) Token: 0x0600358A RID: 13706 RVA: 0x000A8D6C File Offset: 0x000A6F6C
			// (set) Token: 0x0600358B RID: 13707 RVA: 0x000A8D74 File Offset: 0x000A6F74
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x170009B7 RID: 2487
			// (get) Token: 0x0600358C RID: 13708 RVA: 0x000A8D7D File Offset: 0x000A6F7D
			// (set) Token: 0x0600358D RID: 13709 RVA: 0x000A8D85 File Offset: 0x000A6F85
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x170009B8 RID: 2488
			// (get) Token: 0x0600358E RID: 13710 RVA: 0x000A8D8E File Offset: 0x000A6F8E
			// (set) Token: 0x0600358F RID: 13711 RVA: 0x000A8D96 File Offset: 0x000A6F96
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x170009B9 RID: 2489
			// (get) Token: 0x06003590 RID: 13712 RVA: 0x000A8D9F File Offset: 0x000A6F9F
			// (set) Token: 0x06003591 RID: 13713 RVA: 0x000A8DA7 File Offset: 0x000A6FA7
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x0200092D RID: 2349
			public class Joins
			{
				// Token: 0x06003592 RID: 13714 RVA: 0x000A8DB0 File Offset: 0x000A6FB0
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003593 RID: 13715 RVA: 0x000A8DBF File Offset: 0x000A6FBF
				public ProgramSetBuilder<splitRecordsSelect> SelectColumns(ProgramSetBuilder<columnList> value0, ProgramSetBuilder<splitRecords> value1)
				{
					return ProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003594 RID: 13716 RVA: 0x000A8DFF File Offset: 0x000A6FFF
				public ProgramSetBuilder<splitRecords> NoSplit(ProgramSetBuilder<records> value0, ProgramSetBuilder<hasHeader> value1)
				{
					return ProgramSetBuilder<splitRecords>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NoSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003595 RID: 13717 RVA: 0x000A8E3F File Offset: 0x000A703F
				public ProgramSetBuilder<splitRecords> TableFromCells(ProgramSetBuilder<delimiterSplit> value0, ProgramSetBuilder<hasHeader> value1)
				{
					return ProgramSetBuilder<splitRecords>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TableFromCells, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003596 RID: 13718 RVA: 0x000A8E7F File Offset: 0x000A707F
				public ProgramSetBuilder<splitRecords> MultiRecordSplit(ProgramSetBuilder<multiRecordSplit> value0)
				{
					return ProgramSetBuilder<splitRecords>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MultiRecordSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003597 RID: 13719 RVA: 0x000A8EB0 File Offset: 0x000A70B0
				public ProgramSetBuilder<columnSelectorList> Empty()
				{
					return ProgramSetBuilder<columnSelectorList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Empty, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06003598 RID: 13720 RVA: 0x000A8ED1 File Offset: 0x000A70D1
				public ProgramSetBuilder<columnSelectorList> SelectorList(ProgramSetBuilder<columnSelector> value0, ProgramSetBuilder<columnSelectorList> value1)
				{
					return ProgramSetBuilder<columnSelectorList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectorList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003599 RID: 13721 RVA: 0x000A8F11 File Offset: 0x000A7111
				public ProgramSetBuilder<columnSelector> KthLine(ProgramSetBuilder<k> value0, ProgramSetBuilder<rowRecord> value1)
				{
					return ProgramSetBuilder<columnSelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthLine, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600359A RID: 13722 RVA: 0x000A8F54 File Offset: 0x000A7154
				public ProgramSetBuilder<columnSelector> KthKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return ProgramSetBuilder<columnSelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600359B RID: 13723 RVA: 0x000A8FC0 File Offset: 0x000A71C0
				public ProgramSetBuilder<columnSelector> KthTwoLineKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return ProgramSetBuilder<columnSelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthTwoLineKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600359C RID: 13724 RVA: 0x000A902C File Offset: 0x000A722C
				public ProgramSetBuilder<columnSelector> KthKeyQuote(ProgramSetBuilder<key> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<newLineSep> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return ProgramSetBuilder<columnSelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthKeyQuote, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600359D RID: 13725 RVA: 0x000A9098 File Offset: 0x000A7298
				public ProgramSetBuilder<columnSelector> KthKeyValueFw(ProgramSetBuilder<key> value0, ProgramSetBuilder<fwPos> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<newLineSep> value3, ProgramSetBuilder<rowRecord> value4)
				{
					return ProgramSetBuilder<columnSelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthKeyValueFw, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600359E RID: 13726 RVA: 0x000A9114 File Offset: 0x000A7314
				public ProgramSetBuilder<primarySelector> BreakLine(ProgramSetBuilder<records> value0)
				{
					return ProgramSetBuilder<primarySelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.BreakLine, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600359F RID: 13727 RVA: 0x000A9148 File Offset: 0x000A7348
				public ProgramSetBuilder<primarySelector> TwoLineKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<records> value2)
				{
					return ProgramSetBuilder<primarySelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TwoLineKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035A0 RID: 13728 RVA: 0x000A91A4 File Offset: 0x000A73A4
				public ProgramSetBuilder<primarySelector> KeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<records> value2)
				{
					return ProgramSetBuilder<primarySelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035A1 RID: 13729 RVA: 0x000A91FE File Offset: 0x000A73FE
				public ProgramSetBuilder<primarySelector> KeyQuote(ProgramSetBuilder<key> value0, ProgramSetBuilder<records> value1)
				{
					return ProgramSetBuilder<primarySelector>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KeyQuote, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035A2 RID: 13730 RVA: 0x000A923E File Offset: 0x000A743E
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0> SplitFile(ProgramSetBuilder<file> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitFile, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035A3 RID: 13731 RVA: 0x000A926F File Offset: 0x000A746F
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1> MergeRecordLines(ProgramSetBuilder<splitLines> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MergeRecordLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035A4 RID: 13732 RVA: 0x000A92A0 File Offset: 0x000A74A0
				public ProgramSetBuilder<dataLines> FilterRecords(ProgramSetBuilder<skipEmpty> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter> value1, ProgramSetBuilder<commentStr> value2, ProgramSetBuilder<hasCommentHeader> value3, ProgramSetBuilder<skippedRecords> value4)
				{
					return ProgramSetBuilder<dataLines>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FilterRecords, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x060035A5 RID: 13733 RVA: 0x000A931C File Offset: 0x000A751C
				public ProgramSetBuilder<skippedRecords> Skip(ProgramSetBuilder<k> value0, ProgramSetBuilder<headerIndex> value1, ProgramSetBuilder<skippedFooter> value2)
				{
					return ProgramSetBuilder<skippedRecords>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Skip, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035A6 RID: 13734 RVA: 0x000A9376 File Offset: 0x000A7576
				public ProgramSetBuilder<skippedFooter> SkipFooter(ProgramSetBuilder<k> value0, ProgramSetBuilder<allRecords> value1)
				{
					return ProgramSetBuilder<skippedFooter>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SkipFooter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035A7 RID: 13735 RVA: 0x000A93B8 File Offset: 0x000A75B8
				public ProgramSetBuilder<allRecords> QuoteRecords(ProgramSetBuilder<quotingConfig> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter> value1, ProgramSetBuilder<allLines> value2)
				{
					return ProgramSetBuilder<allRecords>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.QuoteRecords, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035A8 RID: 13736 RVA: 0x000A9412 File Offset: 0x000A7612
				public ProgramSetBuilder<basicLinePredicate> StartsWith(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r> value1)
				{
					return ProgramSetBuilder<basicLinePredicate>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035A9 RID: 13737 RVA: 0x000A9452 File Offset: 0x000A7652
				public ProgramSetBuilder<splitSequence> SplitSequence(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r> value0, ProgramSetBuilder<ls> value1)
				{
					return ProgramSetBuilder<splitSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035AA RID: 13738 RVA: 0x000A9492 File Offset: 0x000A7692
				public ProgramSetBuilder<splitSequence> Sequence(ProgramSetBuilder<ls> value0)
				{
					return ProgramSetBuilder<splitSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Sequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035AB RID: 13739 RVA: 0x000A94C3 File Offset: 0x000A76C3
				public ProgramSetBuilder<mapColumnSelectors> MapColumnSelector(ProgramSetBuilder<columnSelectorList> value0, ProgramSetBuilder<rowRecords> value1)
				{
					return ProgramSetBuilder<mapColumnSelectors>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MapColumnSelector, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035AC RID: 13740 RVA: 0x000A9503 File Offset: 0x000A7703
				public ProgramSetBuilder<delimiterSplit> SplitToCells(ProgramSetBuilder<splitTextProg> value0, ProgramSetBuilder<records> value1)
				{
					return ProgramSetBuilder<delimiterSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitToCells, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035AD RID: 13741 RVA: 0x000A9543 File Offset: 0x000A7743
				public ProgramSetBuilder<dataLines> FilterHeader(ProgramSetBuilder<basicLinePredicate> value0, ProgramSetBuilder<skippedRecords> value1)
				{
					return ProgramSetBuilder<dataLines>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FilterHeader, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035AE RID: 13742 RVA: 0x000A9583 File Offset: 0x000A7783
				public ProgramSetBuilder<dataLines> SelectData(ProgramSetBuilder<basicLinePredicate> value0, ProgramSetBuilder<skippedRecords> value1)
				{
					return ProgramSetBuilder<dataLines>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectData, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035AF RID: 13743 RVA: 0x000A95C3 File Offset: 0x000A77C3
				public ProgramSetBuilder<splitTextProg> SplitTextProg(ProgramSetBuilder<regionSplit> value0)
				{
					return ProgramSetBuilder<splitTextProg>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitTextProg, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035B0 RID: 13744 RVA: 0x000A95F4 File Offset: 0x000A77F4
				public ProgramSetBuilder<topSplit> LetFileRecordSplit(ProgramSetBuilder<splitFile> value0, ProgramSetBuilder<splitRecordsSelect> value1)
				{
					return ProgramSetBuilder<topSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetFileRecordSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B1 RID: 13745 RVA: 0x000A9634 File Offset: 0x000A7834
				public ProgramSetBuilder<multiRecordSplit> LetMultiRecordSplit(ProgramSetBuilder<primarySelector> value0, ProgramSetBuilder<mapColumnSelectors> value1)
				{
					return ProgramSetBuilder<multiRecordSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetMultiRecordSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B2 RID: 13746 RVA: 0x000A9674 File Offset: 0x000A7874
				public ProgramSetBuilder<splitFile> LetSplitFile(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1> value1)
				{
					return ProgramSetBuilder<splitFile>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSplitFile, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B3 RID: 13747 RVA: 0x000A96B4 File Offset: 0x000A78B4
				public ProgramSetBuilder<splitLines> SplitSequenceLet(ProgramSetBuilder<dataLines> value0, ProgramSetBuilder<splitSequence> value1)
				{
					return ProgramSetBuilder<splitLines>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitSequenceLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040019F8 RID: 6648
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200092E RID: 2350
			public class ExplicitJoins
			{
				// Token: 0x060035B4 RID: 13748 RVA: 0x000A96F4 File Offset: 0x000A78F4
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060035B5 RID: 13749 RVA: 0x000A9703 File Offset: 0x000A7903
				public JoinProgramSetBuilder<splitRecordsSelect> SelectColumns(ProgramSetBuilder<columnList> value0, ProgramSetBuilder<splitRecords> value1)
				{
					return JoinProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B6 RID: 13750 RVA: 0x000A9743 File Offset: 0x000A7943
				public JoinProgramSetBuilder<splitRecords> NoSplit(ProgramSetBuilder<records> value0, ProgramSetBuilder<hasHeader> value1)
				{
					return JoinProgramSetBuilder<splitRecords>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NoSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B7 RID: 13751 RVA: 0x000A9783 File Offset: 0x000A7983
				public JoinProgramSetBuilder<splitRecords> TableFromCells(ProgramSetBuilder<delimiterSplit> value0, ProgramSetBuilder<hasHeader> value1)
				{
					return JoinProgramSetBuilder<splitRecords>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TableFromCells, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035B8 RID: 13752 RVA: 0x000A97C3 File Offset: 0x000A79C3
				public JoinProgramSetBuilder<splitRecords> MultiRecordSplit(ProgramSetBuilder<multiRecordSplit> value0)
				{
					return JoinProgramSetBuilder<splitRecords>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MultiRecordSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035B9 RID: 13753 RVA: 0x000A97F4 File Offset: 0x000A79F4
				public JoinProgramSetBuilder<columnSelectorList> Empty()
				{
					return JoinProgramSetBuilder<columnSelectorList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Empty, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060035BA RID: 13754 RVA: 0x000A9815 File Offset: 0x000A7A15
				public JoinProgramSetBuilder<columnSelectorList> SelectorList(ProgramSetBuilder<columnSelector> value0, ProgramSetBuilder<columnSelectorList> value1)
				{
					return JoinProgramSetBuilder<columnSelectorList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectorList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035BB RID: 13755 RVA: 0x000A9855 File Offset: 0x000A7A55
				public JoinProgramSetBuilder<columnSelector> KthLine(ProgramSetBuilder<k> value0, ProgramSetBuilder<rowRecord> value1)
				{
					return JoinProgramSetBuilder<columnSelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthLine, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035BC RID: 13756 RVA: 0x000A9898 File Offset: 0x000A7A98
				public JoinProgramSetBuilder<columnSelector> KthKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return JoinProgramSetBuilder<columnSelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x060035BD RID: 13757 RVA: 0x000A9904 File Offset: 0x000A7B04
				public JoinProgramSetBuilder<columnSelector> KthTwoLineKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return JoinProgramSetBuilder<columnSelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthTwoLineKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x060035BE RID: 13758 RVA: 0x000A9970 File Offset: 0x000A7B70
				public JoinProgramSetBuilder<columnSelector> KthKeyQuote(ProgramSetBuilder<key> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<newLineSep> value2, ProgramSetBuilder<rowRecord> value3)
				{
					return JoinProgramSetBuilder<columnSelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthKeyQuote, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x060035BF RID: 13759 RVA: 0x000A99DC File Offset: 0x000A7BDC
				public JoinProgramSetBuilder<columnSelector> KthKeyValueFw(ProgramSetBuilder<key> value0, ProgramSetBuilder<fwPos> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<newLineSep> value3, ProgramSetBuilder<rowRecord> value4)
				{
					return JoinProgramSetBuilder<columnSelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthKeyValueFw, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x060035C0 RID: 13760 RVA: 0x000A9A58 File Offset: 0x000A7C58
				public JoinProgramSetBuilder<primarySelector> BreakLine(ProgramSetBuilder<records> value0)
				{
					return JoinProgramSetBuilder<primarySelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.BreakLine, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035C1 RID: 13761 RVA: 0x000A9A8C File Offset: 0x000A7C8C
				public JoinProgramSetBuilder<primarySelector> TwoLineKeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<records> value2)
				{
					return JoinProgramSetBuilder<primarySelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TwoLineKeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035C2 RID: 13762 RVA: 0x000A9AE8 File Offset: 0x000A7CE8
				public JoinProgramSetBuilder<primarySelector> KeyValue(ProgramSetBuilder<key> value0, ProgramSetBuilder<sep> value1, ProgramSetBuilder<records> value2)
				{
					return JoinProgramSetBuilder<primarySelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KeyValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035C3 RID: 13763 RVA: 0x000A9B42 File Offset: 0x000A7D42
				public JoinProgramSetBuilder<primarySelector> KeyQuote(ProgramSetBuilder<key> value0, ProgramSetBuilder<records> value1)
				{
					return JoinProgramSetBuilder<primarySelector>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KeyQuote, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035C4 RID: 13764 RVA: 0x000A9B82 File Offset: 0x000A7D82
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0> SplitFile(ProgramSetBuilder<file> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitFile, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035C5 RID: 13765 RVA: 0x000A9BB3 File Offset: 0x000A7DB3
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1> MergeRecordLines(ProgramSetBuilder<splitLines> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MergeRecordLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035C6 RID: 13766 RVA: 0x000A9BE4 File Offset: 0x000A7DE4
				public JoinProgramSetBuilder<dataLines> FilterRecords(ProgramSetBuilder<skipEmpty> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter> value1, ProgramSetBuilder<commentStr> value2, ProgramSetBuilder<hasCommentHeader> value3, ProgramSetBuilder<skippedRecords> value4)
				{
					return JoinProgramSetBuilder<dataLines>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FilterRecords, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x060035C7 RID: 13767 RVA: 0x000A9C60 File Offset: 0x000A7E60
				public JoinProgramSetBuilder<skippedRecords> Skip(ProgramSetBuilder<k> value0, ProgramSetBuilder<headerIndex> value1, ProgramSetBuilder<skippedFooter> value2)
				{
					return JoinProgramSetBuilder<skippedRecords>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Skip, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035C8 RID: 13768 RVA: 0x000A9CBA File Offset: 0x000A7EBA
				public JoinProgramSetBuilder<skippedFooter> SkipFooter(ProgramSetBuilder<k> value0, ProgramSetBuilder<allRecords> value1)
				{
					return JoinProgramSetBuilder<skippedFooter>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SkipFooter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035C9 RID: 13769 RVA: 0x000A9CFC File Offset: 0x000A7EFC
				public JoinProgramSetBuilder<allRecords> QuoteRecords(ProgramSetBuilder<quotingConfig> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter> value1, ProgramSetBuilder<allLines> value2)
				{
					return JoinProgramSetBuilder<allRecords>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.QuoteRecords, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060035CA RID: 13770 RVA: 0x000A9D56 File Offset: 0x000A7F56
				public JoinProgramSetBuilder<basicLinePredicate> StartsWith(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r> value1)
				{
					return JoinProgramSetBuilder<basicLinePredicate>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035CB RID: 13771 RVA: 0x000A9D96 File Offset: 0x000A7F96
				public JoinProgramSetBuilder<splitSequence> SplitSequence(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r> value0, ProgramSetBuilder<ls> value1)
				{
					return JoinProgramSetBuilder<splitSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035CC RID: 13772 RVA: 0x000A9DD6 File Offset: 0x000A7FD6
				public JoinProgramSetBuilder<splitSequence> Sequence(ProgramSetBuilder<ls> value0)
				{
					return JoinProgramSetBuilder<splitSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Sequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035CD RID: 13773 RVA: 0x000A9E07 File Offset: 0x000A8007
				public JoinProgramSetBuilder<mapColumnSelectors> MapColumnSelector(ProgramSetBuilder<columnSelectorList> value0, ProgramSetBuilder<rowRecords> value1)
				{
					return JoinProgramSetBuilder<mapColumnSelectors>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MapColumnSelector, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035CE RID: 13774 RVA: 0x000A9E47 File Offset: 0x000A8047
				public JoinProgramSetBuilder<delimiterSplit> SplitToCells(ProgramSetBuilder<splitTextProg> value0, ProgramSetBuilder<records> value1)
				{
					return JoinProgramSetBuilder<delimiterSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitToCells, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035CF RID: 13775 RVA: 0x000A9E87 File Offset: 0x000A8087
				public JoinProgramSetBuilder<dataLines> FilterHeader(ProgramSetBuilder<basicLinePredicate> value0, ProgramSetBuilder<skippedRecords> value1)
				{
					return JoinProgramSetBuilder<dataLines>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FilterHeader, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035D0 RID: 13776 RVA: 0x000A9EC7 File Offset: 0x000A80C7
				public JoinProgramSetBuilder<dataLines> SelectData(ProgramSetBuilder<basicLinePredicate> value0, ProgramSetBuilder<skippedRecords> value1)
				{
					return JoinProgramSetBuilder<dataLines>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectData, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035D1 RID: 13777 RVA: 0x000A9F07 File Offset: 0x000A8107
				public JoinProgramSetBuilder<splitTextProg> SplitTextProg(ProgramSetBuilder<regionSplit> value0)
				{
					return JoinProgramSetBuilder<splitTextProg>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitTextProg, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035D2 RID: 13778 RVA: 0x000A9F38 File Offset: 0x000A8138
				public JoinProgramSetBuilder<topSplit> LetFileRecordSplit(ProgramSetBuilder<splitFile> value0, ProgramSetBuilder<splitRecordsSelect> value1)
				{
					return JoinProgramSetBuilder<topSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetFileRecordSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035D3 RID: 13779 RVA: 0x000A9F78 File Offset: 0x000A8178
				public JoinProgramSetBuilder<multiRecordSplit> LetMultiRecordSplit(ProgramSetBuilder<primarySelector> value0, ProgramSetBuilder<mapColumnSelectors> value1)
				{
					return JoinProgramSetBuilder<multiRecordSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetMultiRecordSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035D4 RID: 13780 RVA: 0x000A9FB8 File Offset: 0x000A81B8
				public JoinProgramSetBuilder<splitFile> LetSplitFile(ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1> value1)
				{
					return JoinProgramSetBuilder<splitFile>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSplitFile, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060035D5 RID: 13781 RVA: 0x000A9FF8 File Offset: 0x000A81F8
				public JoinProgramSetBuilder<splitLines> SplitSequenceLet(ProgramSetBuilder<dataLines> value0, ProgramSetBuilder<splitSequence> value1)
				{
					return JoinProgramSetBuilder<splitLines>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitSequenceLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040019F9 RID: 6649
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200092F RID: 2351
			public class JoinUnnamedConversions
			{
				// Token: 0x060035D6 RID: 13782 RVA: 0x000AA038 File Offset: 0x000A8238
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060035D7 RID: 13783 RVA: 0x000AA047 File Offset: 0x000A8247
				public ProgramSetBuilder<splitRecordsSelect> splitRecordsSelect_splitRecords(ProgramSetBuilder<splitRecords> value0)
				{
					return ProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.splitRecordsSelect_splitRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035D8 RID: 13784 RVA: 0x000AA078 File Offset: 0x000A8278
				public ProgramSetBuilder<dataLines> dataLines_skippedRecords(ProgramSetBuilder<skippedRecords> value0)
				{
					return ProgramSetBuilder<dataLines>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.dataLines_skippedRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035D9 RID: 13785 RVA: 0x000AA0A9 File Offset: 0x000A82A9
				public ProgramSetBuilder<skippedRecords> skippedRecords_skippedFooter(ProgramSetBuilder<skippedFooter> value0)
				{
					return ProgramSetBuilder<skippedRecords>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.skippedRecords_skippedFooter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035DA RID: 13786 RVA: 0x000AA0DA File Offset: 0x000A82DA
				public ProgramSetBuilder<skippedFooter> skippedFooter_allRecords(ProgramSetBuilder<allRecords> value0)
				{
					return ProgramSetBuilder<skippedFooter>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.skippedFooter_allRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035DB RID: 13787 RVA: 0x000AA10B File Offset: 0x000A830B
				public ProgramSetBuilder<allRecords> allRecords_allLines(ProgramSetBuilder<allLines> value0)
				{
					return ProgramSetBuilder<allRecords>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.allRecords_allLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040019FA RID: 6650
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000930 RID: 2352
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x060035DC RID: 13788 RVA: 0x000AA13C File Offset: 0x000A833C
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060035DD RID: 13789 RVA: 0x000AA14B File Offset: 0x000A834B
				public JoinProgramSetBuilder<splitRecordsSelect> splitRecordsSelect_splitRecords(ProgramSetBuilder<splitRecords> value0)
				{
					return JoinProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.splitRecordsSelect_splitRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035DE RID: 13790 RVA: 0x000AA17C File Offset: 0x000A837C
				public JoinProgramSetBuilder<dataLines> dataLines_skippedRecords(ProgramSetBuilder<skippedRecords> value0)
				{
					return JoinProgramSetBuilder<dataLines>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.dataLines_skippedRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035DF RID: 13791 RVA: 0x000AA1AD File Offset: 0x000A83AD
				public JoinProgramSetBuilder<skippedRecords> skippedRecords_skippedFooter(ProgramSetBuilder<skippedFooter> value0)
				{
					return JoinProgramSetBuilder<skippedRecords>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.skippedRecords_skippedFooter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035E0 RID: 13792 RVA: 0x000AA1DE File Offset: 0x000A83DE
				public JoinProgramSetBuilder<skippedFooter> skippedFooter_allRecords(ProgramSetBuilder<allRecords> value0)
				{
					return JoinProgramSetBuilder<skippedFooter>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.skippedFooter_allRecords, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060035E1 RID: 13793 RVA: 0x000AA20F File Offset: 0x000A840F
				public JoinProgramSetBuilder<allRecords> allRecords_allLines(ProgramSetBuilder<allLines> value0)
				{
					return JoinProgramSetBuilder<allRecords>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.allRecords_allLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040019FB RID: 6651
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000931 RID: 2353
			public class Casts
			{
				// Token: 0x060035E2 RID: 13794 RVA: 0x000AA240 File Offset: 0x000A8440
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060035E3 RID: 13795 RVA: 0x000AA250 File Offset: 0x000A8450
				public ProgramSetBuilder<hasHeader> hasHeader(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.hasHeader)
					{
						string text = "set";
						string text2 = "expected program set for symbol hasHeader but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasHeader>.CreateUnsafe(set);
				}

				// Token: 0x060035E4 RID: 13796 RVA: 0x000AA2A8 File Offset: 0x000A84A8
				public ProgramSetBuilder<columnList> columnList(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnList)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnList but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnList>.CreateUnsafe(set);
				}

				// Token: 0x060035E5 RID: 13797 RVA: 0x000AA300 File Offset: 0x000A8500
				public ProgramSetBuilder<topSplit> topSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.topSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol topSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.topSplit>.CreateUnsafe(set);
				}

				// Token: 0x060035E6 RID: 13798 RVA: 0x000AA358 File Offset: 0x000A8558
				public ProgramSetBuilder<records> records(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.records)
					{
						string text = "set";
						string text2 = "expected program set for symbol records but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.records>.CreateUnsafe(set);
				}

				// Token: 0x060035E7 RID: 13799 RVA: 0x000AA3B0 File Offset: 0x000A85B0
				public ProgramSetBuilder<splitRecordsSelect> splitRecordsSelect(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitRecordsSelect)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitRecordsSelect but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecordsSelect>.CreateUnsafe(set);
				}

				// Token: 0x060035E8 RID: 13800 RVA: 0x000AA408 File Offset: 0x000A8608
				public ProgramSetBuilder<splitRecords> splitRecords(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitRecords)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitRecords but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitRecords>.CreateUnsafe(set);
				}

				// Token: 0x060035E9 RID: 13801 RVA: 0x000AA460 File Offset: 0x000A8660
				public ProgramSetBuilder<key> key(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.key)
					{
						string text = "set";
						string text2 = "expected program set for symbol key but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.key>.CreateUnsafe(set);
				}

				// Token: 0x060035EA RID: 13802 RVA: 0x000AA4B8 File Offset: 0x000A86B8
				public ProgramSetBuilder<sep> sep(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sep)
					{
						string text = "set";
						string text2 = "expected program set for symbol sep but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.sep>.CreateUnsafe(set);
				}

				// Token: 0x060035EB RID: 13803 RVA: 0x000AA510 File Offset: 0x000A8710
				public ProgramSetBuilder<newLineSep> newLineSep(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.newLineSep)
					{
						string text = "set";
						string text2 = "expected program set for symbol newLineSep but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.newLineSep>.CreateUnsafe(set);
				}

				// Token: 0x060035EC RID: 13804 RVA: 0x000AA568 File Offset: 0x000A8768
				public ProgramSetBuilder<fwPos> fwPos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fwPos)
					{
						string text = "set";
						string text2 = "expected program set for symbol fwPos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.fwPos>.CreateUnsafe(set);
				}

				// Token: 0x060035ED RID: 13805 RVA: 0x000AA5C0 File Offset: 0x000A87C0
				public ProgramSetBuilder<multiRecordSplit> multiRecordSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.multiRecordSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol multiRecordSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.multiRecordSplit>.CreateUnsafe(set);
				}

				// Token: 0x060035EE RID: 13806 RVA: 0x000AA618 File Offset: 0x000A8818
				public ProgramSetBuilder<rowRecords> rowRecords(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rowRecords)
					{
						string text = "set";
						string text2 = "expected program set for symbol rowRecords but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecords>.CreateUnsafe(set);
				}

				// Token: 0x060035EF RID: 13807 RVA: 0x000AA670 File Offset: 0x000A8870
				public ProgramSetBuilder<mapColumnSelectors> mapColumnSelectors(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mapColumnSelectors)
					{
						string text = "set";
						string text2 = "expected program set for symbol mapColumnSelectors but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.mapColumnSelectors>.CreateUnsafe(set);
				}

				// Token: 0x060035F0 RID: 13808 RVA: 0x000AA6C8 File Offset: 0x000A88C8
				public ProgramSetBuilder<rowRecord> rowRecord(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rowRecord)
					{
						string text = "set";
						string text2 = "expected program set for symbol rowRecord but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.rowRecord>.CreateUnsafe(set);
				}

				// Token: 0x060035F1 RID: 13809 RVA: 0x000AA720 File Offset: 0x000A8920
				public ProgramSetBuilder<columnSelectorList> columnSelectorList(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnSelectorList)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnSelectorList but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelectorList>.CreateUnsafe(set);
				}

				// Token: 0x060035F2 RID: 13810 RVA: 0x000AA778 File Offset: 0x000A8978
				public ProgramSetBuilder<columnSelector> columnSelector(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnSelector)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnSelector but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.columnSelector>.CreateUnsafe(set);
				}

				// Token: 0x060035F3 RID: 13811 RVA: 0x000AA7D0 File Offset: 0x000A89D0
				public ProgramSetBuilder<primarySelector> primarySelector(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.primarySelector)
					{
						string text = "set";
						string text2 = "expected program set for symbol primarySelector but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.primarySelector>.CreateUnsafe(set);
				}

				// Token: 0x060035F4 RID: 13812 RVA: 0x000AA828 File Offset: 0x000A8A28
				public ProgramSetBuilder<delimiterSplit> delimiterSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiterSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiterSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiterSplit>.CreateUnsafe(set);
				}

				// Token: 0x060035F5 RID: 13813 RVA: 0x000AA880 File Offset: 0x000A8A80
				public ProgramSetBuilder<record> record(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.record)
					{
						string text = "set";
						string text2 = "expected program set for symbol @record but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.record>.CreateUnsafe(set);
				}

				// Token: 0x060035F6 RID: 13814 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
				public ProgramSetBuilder<splitTextProg> splitTextProg(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitTextProg)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitTextProg but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitTextProg>.CreateUnsafe(set);
				}

				// Token: 0x060035F7 RID: 13815 RVA: 0x000AA930 File Offset: 0x000A8B30
				public ProgramSetBuilder<splitFile> splitFile(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitFile)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitFile but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitFile>.CreateUnsafe(set);
				}

				// Token: 0x060035F8 RID: 13816 RVA: 0x000AA988 File Offset: 0x000A8B88
				public ProgramSetBuilder<allLines> allLines(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.allLines)
					{
						string text = "set";
						string text2 = "expected program set for symbol allLines but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allLines>.CreateUnsafe(set);
				}

				// Token: 0x060035F9 RID: 13817 RVA: 0x000AA9E0 File Offset: 0x000A8BE0
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r> r(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.r)
					{
						string text = "set";
						string text2 = "expected program set for symbol r but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.r>.CreateUnsafe(set);
				}

				// Token: 0x060035FA RID: 13818 RVA: 0x000AAA38 File Offset: 0x000A8C38
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x060035FB RID: 13819 RVA: 0x000AAA90 File Offset: 0x000A8C90
				public ProgramSetBuilder<quotingConfig> quotingConfig(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.quotingConfig)
					{
						string text = "set";
						string text2 = "expected program set for symbol quotingConfig but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.quotingConfig>.CreateUnsafe(set);
				}

				// Token: 0x060035FC RID: 13820 RVA: 0x000AAAE8 File Offset: 0x000A8CE8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter> delimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.delimiter>.CreateUnsafe(set);
				}

				// Token: 0x060035FD RID: 13821 RVA: 0x000AAB40 File Offset: 0x000A8D40
				public ProgramSetBuilder<headerIndex> headerIndex(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.headerIndex)
					{
						string text = "set";
						string text2 = "expected program set for symbol headerIndex but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.headerIndex>.CreateUnsafe(set);
				}

				// Token: 0x060035FE RID: 13822 RVA: 0x000AAB98 File Offset: 0x000A8D98
				public ProgramSetBuilder<commentStr> commentStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.commentStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol commentStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.commentStr>.CreateUnsafe(set);
				}

				// Token: 0x060035FF RID: 13823 RVA: 0x000AABF0 File Offset: 0x000A8DF0
				public ProgramSetBuilder<skipEmpty> skipEmpty(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skipEmpty)
					{
						string text = "set";
						string text2 = "expected program set for symbol skipEmpty but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skipEmpty>.CreateUnsafe(set);
				}

				// Token: 0x06003600 RID: 13824 RVA: 0x000AAC48 File Offset: 0x000A8E48
				public ProgramSetBuilder<hasCommentHeader> hasCommentHeader(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.hasCommentHeader)
					{
						string text = "set";
						string text2 = "expected program set for symbol hasCommentHeader but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.hasCommentHeader>.CreateUnsafe(set);
				}

				// Token: 0x06003601 RID: 13825 RVA: 0x000AACA0 File Offset: 0x000A8EA0
				public ProgramSetBuilder<splitLines> splitLines(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitLines)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitLines but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitLines>.CreateUnsafe(set);
				}

				// Token: 0x06003602 RID: 13826 RVA: 0x000AACF8 File Offset: 0x000A8EF8
				public ProgramSetBuilder<ls> ls(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.ls)
					{
						string text = "set";
						string text2 = "expected program set for symbol ls but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.ls>.CreateUnsafe(set);
				}

				// Token: 0x06003603 RID: 13827 RVA: 0x000AAD50 File Offset: 0x000A8F50
				public ProgramSetBuilder<dataLines> dataLines(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dataLines)
					{
						string text = "set";
						string text2 = "expected program set for symbol dataLines but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.dataLines>.CreateUnsafe(set);
				}

				// Token: 0x06003604 RID: 13828 RVA: 0x000AADA8 File Offset: 0x000A8FA8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s> s(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.s)
					{
						string text = "set";
						string text2 = "expected program set for symbol s but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.s>.CreateUnsafe(set);
				}

				// Token: 0x06003605 RID: 13829 RVA: 0x000AAE00 File Offset: 0x000A9000
				public ProgramSetBuilder<skippedRecords> skippedRecords(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skippedRecords)
					{
						string text = "set";
						string text2 = "expected program set for symbol skippedRecords but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedRecords>.CreateUnsafe(set);
				}

				// Token: 0x06003606 RID: 13830 RVA: 0x000AAE58 File Offset: 0x000A9058
				public ProgramSetBuilder<skippedFooter> skippedFooter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skippedFooter)
					{
						string text = "set";
						string text2 = "expected program set for symbol skippedFooter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.skippedFooter>.CreateUnsafe(set);
				}

				// Token: 0x06003607 RID: 13831 RVA: 0x000AAEB0 File Offset: 0x000A90B0
				public ProgramSetBuilder<allRecords> allRecords(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.allRecords)
					{
						string text = "set";
						string text2 = "expected program set for symbol allRecords but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.allRecords>.CreateUnsafe(set);
				}

				// Token: 0x06003608 RID: 13832 RVA: 0x000AAF08 File Offset: 0x000A9108
				public ProgramSetBuilder<basicLinePredicate> basicLinePredicate(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.basicLinePredicate)
					{
						string text = "set";
						string text2 = "expected program set for symbol basicLinePredicate but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.basicLinePredicate>.CreateUnsafe(set);
				}

				// Token: 0x06003609 RID: 13833 RVA: 0x000AAF60 File Offset: 0x000A9160
				public ProgramSetBuilder<splitSequence> splitSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes.splitSequence>.CreateUnsafe(set);
				}

				// Token: 0x0600360A RID: 13834 RVA: 0x000AAFB8 File Offset: 0x000A91B8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x0600360B RID: 13835 RVA: 0x000AB010 File Offset: 0x000A9210
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x040019FC RID: 6652
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
