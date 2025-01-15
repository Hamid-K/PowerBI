using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Split.Text.Build
{
	// Token: 0x0200131B RID: 4891
	public class GrammarBuilders
	{
		// Token: 0x06009327 RID: 37671 RVA: 0x001FB735 File Offset: 0x001F9935
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06009328 RID: 37672 RVA: 0x001FB761 File Offset: 0x001F9961
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06009329 RID: 37673 RVA: 0x001FB76E File Offset: 0x001F996E
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x0600932A RID: 37674 RVA: 0x001FB77B File Offset: 0x001F997B
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x0600932B RID: 37675 RVA: 0x001FB788 File Offset: 0x001F9988
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x0600932C RID: 37676 RVA: 0x001FB795 File Offset: 0x001F9995
		// (set) Token: 0x0600932D RID: 37677 RVA: 0x001FB79D File Offset: 0x001F999D
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x0600932E RID: 37678 RVA: 0x001FB7A6 File Offset: 0x001F99A6
		// (set) Token: 0x0600932F RID: 37679 RVA: 0x001FB7AE File Offset: 0x001F99AE
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06009330 RID: 37680 RVA: 0x001FB7B8 File Offset: 0x001F99B8
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

		// Token: 0x04003C7D RID: 15485
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04003C7E RID: 15486
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04003C7F RID: 15487
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04003C80 RID: 15488
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04003C81 RID: 15489
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x0200131C RID: 4892
		public class GrammarSymbols
		{
			// Token: 0x1700193F RID: 6463
			// (get) Token: 0x06009332 RID: 37682 RVA: 0x001FB863 File Offset: 0x001F9A63
			// (set) Token: 0x06009333 RID: 37683 RVA: 0x001FB86B File Offset: 0x001F9A6B
			public Symbol v { get; private set; }

			// Token: 0x17001940 RID: 6464
			// (get) Token: 0x06009334 RID: 37684 RVA: 0x001FB874 File Offset: 0x001F9A74
			// (set) Token: 0x06009335 RID: 37685 RVA: 0x001FB87C File Offset: 0x001F9A7C
			public Symbol regionSplit { get; private set; }

			// Token: 0x17001941 RID: 6465
			// (get) Token: 0x06009336 RID: 37686 RVA: 0x001FB885 File Offset: 0x001F9A85
			// (set) Token: 0x06009337 RID: 37687 RVA: 0x001FB88D File Offset: 0x001F9A8D
			public Symbol splitMatches { get; private set; }

			// Token: 0x17001942 RID: 6466
			// (get) Token: 0x06009338 RID: 37688 RVA: 0x001FB896 File Offset: 0x001F9A96
			// (set) Token: 0x06009339 RID: 37689 RVA: 0x001FB89E File Offset: 0x001F9A9E
			public Symbol multipleMatches { get; private set; }

			// Token: 0x17001943 RID: 6467
			// (get) Token: 0x0600933A RID: 37690 RVA: 0x001FB8A7 File Offset: 0x001F9AA7
			// (set) Token: 0x0600933B RID: 37691 RVA: 0x001FB8AF File Offset: 0x001F9AAF
			public Symbol delimiterList { get; private set; }

			// Token: 0x17001944 RID: 6468
			// (get) Token: 0x0600933C RID: 37692 RVA: 0x001FB8B8 File Offset: 0x001F9AB8
			// (set) Token: 0x0600933D RID: 37693 RVA: 0x001FB8C0 File Offset: 0x001F9AC0
			public Symbol extractionPoints { get; private set; }

			// Token: 0x17001945 RID: 6469
			// (get) Token: 0x0600933E RID: 37694 RVA: 0x001FB8C9 File Offset: 0x001F9AC9
			// (set) Token: 0x0600933F RID: 37695 RVA: 0x001FB8D1 File Offset: 0x001F9AD1
			public Symbol cndExtPoint { get; private set; }

			// Token: 0x17001946 RID: 6470
			// (get) Token: 0x06009340 RID: 37696 RVA: 0x001FB8DA File Offset: 0x001F9ADA
			// (set) Token: 0x06009341 RID: 37697 RVA: 0x001FB8E2 File Offset: 0x001F9AE2
			public Symbol extPoint { get; private set; }

			// Token: 0x17001947 RID: 6471
			// (get) Token: 0x06009342 RID: 37698 RVA: 0x001FB8EB File Offset: 0x001F9AEB
			// (set) Token: 0x06009343 RID: 37699 RVA: 0x001FB8F3 File Offset: 0x001F9AF3
			public Symbol pred { get; private set; }

			// Token: 0x17001948 RID: 6472
			// (get) Token: 0x06009344 RID: 37700 RVA: 0x001FB8FC File Offset: 0x001F9AFC
			// (set) Token: 0x06009345 RID: 37701 RVA: 0x001FB904 File Offset: 0x001F9B04
			public Symbol pattern { get; private set; }

			// Token: 0x17001949 RID: 6473
			// (get) Token: 0x06009346 RID: 37702 RVA: 0x001FB90D File Offset: 0x001F9B0D
			// (set) Token: 0x06009347 RID: 37703 RVA: 0x001FB915 File Offset: 0x001F9B15
			public Symbol d { get; private set; }

			// Token: 0x1700194A RID: 6474
			// (get) Token: 0x06009348 RID: 37704 RVA: 0x001FB91E File Offset: 0x001F9B1E
			// (set) Token: 0x06009349 RID: 37705 RVA: 0x001FB926 File Offset: 0x001F9B26
			public Symbol c { get; private set; }

			// Token: 0x1700194B RID: 6475
			// (get) Token: 0x0600934A RID: 37706 RVA: 0x001FB92F File Offset: 0x001F9B2F
			// (set) Token: 0x0600934B RID: 37707 RVA: 0x001FB937 File Offset: 0x001F9B37
			public Symbol quotingConf { get; private set; }

			// Token: 0x1700194C RID: 6476
			// (get) Token: 0x0600934C RID: 37708 RVA: 0x001FB940 File Offset: 0x001F9B40
			// (set) Token: 0x0600934D RID: 37709 RVA: 0x001FB948 File Offset: 0x001F9B48
			public Symbol constantDelimiterMatches { get; private set; }

			// Token: 0x1700194D RID: 6477
			// (get) Token: 0x0600934E RID: 37710 RVA: 0x001FB951 File Offset: 0x001F9B51
			// (set) Token: 0x0600934F RID: 37711 RVA: 0x001FB959 File Offset: 0x001F9B59
			public Symbol r { get; private set; }

			// Token: 0x1700194E RID: 6478
			// (get) Token: 0x06009350 RID: 37712 RVA: 0x001FB962 File Offset: 0x001F9B62
			// (set) Token: 0x06009351 RID: 37713 RVA: 0x001FB96A File Offset: 0x001F9B6A
			public Symbol regexMatch { get; private set; }

			// Token: 0x1700194F RID: 6479
			// (get) Token: 0x06009352 RID: 37714 RVA: 0x001FB973 File Offset: 0x001F9B73
			// (set) Token: 0x06009353 RID: 37715 RVA: 0x001FB97B File Offset: 0x001F9B7B
			public Symbol fieldMatch { get; private set; }

			// Token: 0x17001950 RID: 6480
			// (get) Token: 0x06009354 RID: 37716 RVA: 0x001FB984 File Offset: 0x001F9B84
			// (set) Token: 0x06009355 RID: 37717 RVA: 0x001FB98C File Offset: 0x001F9B8C
			public Symbol fixedWidthMatches { get; private set; }

			// Token: 0x17001951 RID: 6481
			// (get) Token: 0x06009356 RID: 37718 RVA: 0x001FB995 File Offset: 0x001F9B95
			// (set) Token: 0x06009357 RID: 37719 RVA: 0x001FB99D File Offset: 0x001F9B9D
			public Symbol gen_Concat { get; private set; }

			// Token: 0x17001952 RID: 6482
			// (get) Token: 0x06009358 RID: 37720 RVA: 0x001FB9A6 File Offset: 0x001F9BA6
			// (set) Token: 0x06009359 RID: 37721 RVA: 0x001FB9AE File Offset: 0x001F9BAE
			public Symbol gen_LookAround { get; private set; }

			// Token: 0x17001953 RID: 6483
			// (get) Token: 0x0600935A RID: 37722 RVA: 0x001FB9B7 File Offset: 0x001F9BB7
			// (set) Token: 0x0600935B RID: 37723 RVA: 0x001FB9BF File Offset: 0x001F9BBF
			public Symbol gen_LookAroundField { get; private set; }

			// Token: 0x17001954 RID: 6484
			// (get) Token: 0x0600935C RID: 37724 RVA: 0x001FB9C8 File Offset: 0x001F9BC8
			// (set) Token: 0x0600935D RID: 37725 RVA: 0x001FB9D0 File Offset: 0x001F9BD0
			public Symbol delimiterStart { get; private set; }

			// Token: 0x17001955 RID: 6485
			// (get) Token: 0x0600935E RID: 37726 RVA: 0x001FB9D9 File Offset: 0x001F9BD9
			// (set) Token: 0x0600935F RID: 37727 RVA: 0x001FB9E1 File Offset: 0x001F9BE1
			public Symbol delimiterEnd { get; private set; }

			// Token: 0x17001956 RID: 6486
			// (get) Token: 0x06009360 RID: 37728 RVA: 0x001FB9EA File Offset: 0x001F9BEA
			// (set) Token: 0x06009361 RID: 37729 RVA: 0x001FB9F2 File Offset: 0x001F9BF2
			public Symbol includeDelimiters { get; private set; }

			// Token: 0x17001957 RID: 6487
			// (get) Token: 0x06009362 RID: 37730 RVA: 0x001FB9FB File Offset: 0x001F9BFB
			// (set) Token: 0x06009363 RID: 37731 RVA: 0x001FBA03 File Offset: 0x001F9C03
			public Symbol fillStrategy { get; private set; }

			// Token: 0x17001958 RID: 6488
			// (get) Token: 0x06009364 RID: 37732 RVA: 0x001FBA0C File Offset: 0x001F9C0C
			// (set) Token: 0x06009365 RID: 37733 RVA: 0x001FBA14 File Offset: 0x001F9C14
			public Symbol ignoreIndexes { get; private set; }

			// Token: 0x17001959 RID: 6489
			// (get) Token: 0x06009366 RID: 37734 RVA: 0x001FBA1D File Offset: 0x001F9C1D
			// (set) Token: 0x06009367 RID: 37735 RVA: 0x001FBA25 File Offset: 0x001F9C25
			public Symbol fieldStartPositions { get; private set; }

			// Token: 0x1700195A RID: 6490
			// (get) Token: 0x06009368 RID: 37736 RVA: 0x001FBA2E File Offset: 0x001F9C2E
			// (set) Token: 0x06009369 RID: 37737 RVA: 0x001FBA36 File Offset: 0x001F9C36
			public Symbol delimiterPositions { get; private set; }

			// Token: 0x1700195B RID: 6491
			// (get) Token: 0x0600936A RID: 37738 RVA: 0x001FBA3F File Offset: 0x001F9C3F
			// (set) Token: 0x0600936B RID: 37739 RVA: 0x001FBA47 File Offset: 0x001F9C47
			public Symbol fregex { get; private set; }

			// Token: 0x1700195C RID: 6492
			// (get) Token: 0x0600936C RID: 37740 RVA: 0x001FBA50 File Offset: 0x001F9C50
			// (set) Token: 0x0600936D RID: 37741 RVA: 0x001FBA58 File Offset: 0x001F9C58
			public Symbol s { get; private set; }

			// Token: 0x1700195D RID: 6493
			// (get) Token: 0x0600936E RID: 37742 RVA: 0x001FBA61 File Offset: 0x001F9C61
			// (set) Token: 0x0600936F RID: 37743 RVA: 0x001FBA69 File Offset: 0x001F9C69
			public Symbol a { get; private set; }

			// Token: 0x1700195E RID: 6494
			// (get) Token: 0x06009370 RID: 37744 RVA: 0x001FBA72 File Offset: 0x001F9C72
			// (set) Token: 0x06009371 RID: 37745 RVA: 0x001FBA7A File Offset: 0x001F9C7A
			public Symbol numSplits { get; private set; }

			// Token: 0x1700195F RID: 6495
			// (get) Token: 0x06009372 RID: 37746 RVA: 0x001FBA83 File Offset: 0x001F9C83
			// (set) Token: 0x06009373 RID: 37747 RVA: 0x001FBA8B File Offset: 0x001F9C8B
			public Symbol regex { get; private set; }

			// Token: 0x17001960 RID: 6496
			// (get) Token: 0x06009374 RID: 37748 RVA: 0x001FBA94 File Offset: 0x001F9C94
			// (set) Token: 0x06009375 RID: 37749 RVA: 0x001FBA9C File Offset: 0x001F9C9C
			public Symbol obj { get; private set; }

			// Token: 0x17001961 RID: 6497
			// (get) Token: 0x06009376 RID: 37750 RVA: 0x001FBAA5 File Offset: 0x001F9CA5
			// (set) Token: 0x06009377 RID: 37751 RVA: 0x001FBAAD File Offset: 0x001F9CAD
			public Symbol delimiter { get; private set; }

			// Token: 0x17001962 RID: 6498
			// (get) Token: 0x06009378 RID: 37752 RVA: 0x001FBAB6 File Offset: 0x001F9CB6
			// (set) Token: 0x06009379 RID: 37753 RVA: 0x001FBABE File Offset: 0x001F9CBE
			public Symbol output { get; private set; }

			// Token: 0x17001963 RID: 6499
			// (get) Token: 0x0600937A RID: 37754 RVA: 0x001FBAC7 File Offset: 0x001F9CC7
			// (set) Token: 0x0600937B RID: 37755 RVA: 0x001FBACF File Offset: 0x001F9CCF
			public Symbol pair { get; private set; }

			// Token: 0x17001964 RID: 6500
			// (get) Token: 0x0600937C RID: 37756 RVA: 0x001FBAD8 File Offset: 0x001F9CD8
			// (set) Token: 0x0600937D RID: 37757 RVA: 0x001FBAE0 File Offset: 0x001F9CE0
			public Symbol item1 { get; private set; }

			// Token: 0x17001965 RID: 6501
			// (get) Token: 0x0600937E RID: 37758 RVA: 0x001FBAE9 File Offset: 0x001F9CE9
			// (set) Token: 0x0600937F RID: 37759 RVA: 0x001FBAF1 File Offset: 0x001F9CF1
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17001966 RID: 6502
			// (get) Token: 0x06009380 RID: 37760 RVA: 0x001FBAFA File Offset: 0x001F9CFA
			// (set) Token: 0x06009381 RID: 37761 RVA: 0x001FBB02 File Offset: 0x001F9D02
			public Symbol _LetB1 { get; private set; }

			// Token: 0x17001967 RID: 6503
			// (get) Token: 0x06009382 RID: 37762 RVA: 0x001FBB0B File Offset: 0x001F9D0B
			// (set) Token: 0x06009383 RID: 37763 RVA: 0x001FBB13 File Offset: 0x001F9D13
			public Symbol _LetB2 { get; private set; }

			// Token: 0x17001968 RID: 6504
			// (get) Token: 0x06009384 RID: 37764 RVA: 0x001FBB1C File Offset: 0x001F9D1C
			// (set) Token: 0x06009385 RID: 37765 RVA: 0x001FBB24 File Offset: 0x001F9D24
			public Symbol _LetB3 { get; private set; }

			// Token: 0x06009386 RID: 37766 RVA: 0x001FBB30 File Offset: 0x001F9D30
			public GrammarSymbols(Grammar grammar)
			{
				this.v = grammar.Symbol("v");
				this.regionSplit = grammar.Symbol("regionSplit");
				this.splitMatches = grammar.Symbol("splitMatches");
				this.multipleMatches = grammar.Symbol("multipleMatches");
				this.delimiterList = grammar.Symbol("delimiterList");
				this.extractionPoints = grammar.Symbol("extractionPoints");
				this.cndExtPoint = grammar.Symbol("cndExtPoint");
				this.extPoint = grammar.Symbol("extPoint");
				this.pred = grammar.Symbol("pred");
				this.pattern = grammar.Symbol("pattern");
				this.d = grammar.Symbol("d");
				this.c = grammar.Symbol("c");
				this.quotingConf = grammar.Symbol("quotingConf");
				this.constantDelimiterMatches = grammar.Symbol("constantDelimiterMatches");
				this.r = grammar.Symbol("r");
				this.regexMatch = grammar.Symbol("regexMatch");
				this.fieldMatch = grammar.Symbol("fieldMatch");
				this.fixedWidthMatches = grammar.Symbol("fixedWidthMatches");
				this.gen_Concat = grammar.Symbol("gen_Concat");
				this.gen_LookAround = grammar.Symbol("gen_LookAround");
				this.gen_LookAroundField = grammar.Symbol("gen_LookAroundField");
				this.delimiterStart = grammar.Symbol("delimiterStart");
				this.delimiterEnd = grammar.Symbol("delimiterEnd");
				this.includeDelimiters = grammar.Symbol("includeDelimiters");
				this.fillStrategy = grammar.Symbol("fillStrategy");
				this.ignoreIndexes = grammar.Symbol("ignoreIndexes");
				this.fieldStartPositions = grammar.Symbol("fieldStartPositions");
				this.delimiterPositions = grammar.Symbol("delimiterPositions");
				this.fregex = grammar.Symbol("fregex");
				this.s = grammar.Symbol("s");
				this.a = grammar.Symbol("a");
				this.numSplits = grammar.Symbol("numSplits");
				this.regex = grammar.Symbol("regex");
				this.obj = grammar.Symbol("obj");
				this.delimiter = grammar.Symbol("delimiter");
				this.output = grammar.Symbol("output");
				this.pair = grammar.Symbol("pair");
				this.item1 = grammar.Symbol("item1");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
				this._LetB2 = grammar.Symbol("_LetB2");
				this._LetB3 = grammar.Symbol("_LetB3");
			}
		}

		// Token: 0x0200131D RID: 4893
		public class GrammarRules
		{
			// Token: 0x17001969 RID: 6505
			// (get) Token: 0x06009387 RID: 37767 RVA: 0x001FBE0D File Offset: 0x001FA00D
			// (set) Token: 0x06009388 RID: 37768 RVA: 0x001FBE15 File Offset: 0x001FA015
			public BlackBoxRule ExtractionSplit { get; private set; }

			// Token: 0x1700196A RID: 6506
			// (get) Token: 0x06009389 RID: 37769 RVA: 0x001FBE1E File Offset: 0x001FA01E
			// (set) Token: 0x0600938A RID: 37770 RVA: 0x001FBE26 File Offset: 0x001FA026
			public BlackBoxRule SplitRegion { get; private set; }

			// Token: 0x1700196B RID: 6507
			// (get) Token: 0x0600938B RID: 37771 RVA: 0x001FBE2F File Offset: 0x001FA02F
			// (set) Token: 0x0600938C RID: 37772 RVA: 0x001FBE37 File Offset: 0x001FA037
			public BlackBoxRule SplitMultiple { get; private set; }

			// Token: 0x1700196C RID: 6508
			// (get) Token: 0x0600938D RID: 37773 RVA: 0x001FBE40 File Offset: 0x001FA040
			// (set) Token: 0x0600938E RID: 37774 RVA: 0x001FBE48 File Offset: 0x001FA048
			public BlackBoxRule DelimitersList { get; private set; }

			// Token: 0x1700196D RID: 6509
			// (get) Token: 0x0600938F RID: 37775 RVA: 0x001FBE51 File Offset: 0x001FA051
			// (set) Token: 0x06009390 RID: 37776 RVA: 0x001FBE59 File Offset: 0x001FA059
			public BlackBoxRule EmptyDelimitersList { get; private set; }

			// Token: 0x1700196E RID: 6510
			// (get) Token: 0x06009391 RID: 37777 RVA: 0x001FBE62 File Offset: 0x001FA062
			// (set) Token: 0x06009392 RID: 37778 RVA: 0x001FBE6A File Offset: 0x001FA06A
			public BlackBoxRule ExtPointsList { get; private set; }

			// Token: 0x1700196F RID: 6511
			// (get) Token: 0x06009393 RID: 37779 RVA: 0x001FBE73 File Offset: 0x001FA073
			// (set) Token: 0x06009394 RID: 37780 RVA: 0x001FBE7B File Offset: 0x001FA07B
			public BlackBoxRule EmptyExtPointsList { get; private set; }

			// Token: 0x17001970 RID: 6512
			// (get) Token: 0x06009395 RID: 37781 RVA: 0x001FBE84 File Offset: 0x001FA084
			// (set) Token: 0x06009396 RID: 37782 RVA: 0x001FBE8C File Offset: 0x001FA08C
			public BlackBoxRule ConditionalExtract { get; private set; }

			// Token: 0x17001971 RID: 6513
			// (get) Token: 0x06009397 RID: 37783 RVA: 0x001FBE95 File Offset: 0x001FA095
			// (set) Token: 0x06009398 RID: 37784 RVA: 0x001FBE9D File Offset: 0x001FA09D
			public BlackBoxRule SpecialCharPattern { get; private set; }

			// Token: 0x17001972 RID: 6514
			// (get) Token: 0x06009399 RID: 37785 RVA: 0x001FBEA6 File Offset: 0x001FA0A6
			// (set) Token: 0x0600939A RID: 37786 RVA: 0x001FBEAE File Offset: 0x001FA0AE
			public BlackBoxRule LookAround { get; private set; }

			// Token: 0x17001973 RID: 6515
			// (get) Token: 0x0600939B RID: 37787 RVA: 0x001FBEB7 File Offset: 0x001FA0B7
			// (set) Token: 0x0600939C RID: 37788 RVA: 0x001FBEBF File Offset: 0x001FA0BF
			public BlackBoxRule FieldEndPoints { get; private set; }

			// Token: 0x17001974 RID: 6516
			// (get) Token: 0x0600939D RID: 37789 RVA: 0x001FBEC8 File Offset: 0x001FA0C8
			// (set) Token: 0x0600939E RID: 37790 RVA: 0x001FBED0 File Offset: 0x001FA0D0
			public BlackBoxRule FieldLookAroundEndPoints { get; private set; }

			// Token: 0x17001975 RID: 6517
			// (get) Token: 0x0600939F RID: 37791 RVA: 0x001FBED9 File Offset: 0x001FA0D9
			// (set) Token: 0x060093A0 RID: 37792 RVA: 0x001FBEE1 File Offset: 0x001FA0E1
			public BlackBoxRule ConstStr { get; private set; }

			// Token: 0x17001976 RID: 6518
			// (get) Token: 0x060093A1 RID: 37793 RVA: 0x001FBEEA File Offset: 0x001FA0EA
			// (set) Token: 0x060093A2 RID: 37794 RVA: 0x001FBEF2 File Offset: 0x001FA0F2
			public BlackBoxRule ConstStrWithWhitespace { get; private set; }

			// Token: 0x17001977 RID: 6519
			// (get) Token: 0x060093A3 RID: 37795 RVA: 0x001FBEFB File Offset: 0x001FA0FB
			// (set) Token: 0x060093A4 RID: 37796 RVA: 0x001FBF03 File Offset: 0x001FA103
			public BlackBoxRule ConstAlphStr { get; private set; }

			// Token: 0x17001978 RID: 6520
			// (get) Token: 0x060093A5 RID: 37797 RVA: 0x001FBF0C File Offset: 0x001FA10C
			// (set) Token: 0x060093A6 RID: 37798 RVA: 0x001FBF14 File Offset: 0x001FA114
			public BlackBoxRule ConstantDelimiterWithQuoting { get; private set; }

			// Token: 0x17001979 RID: 6521
			// (get) Token: 0x060093A7 RID: 37799 RVA: 0x001FBF1D File Offset: 0x001FA11D
			// (set) Token: 0x060093A8 RID: 37800 RVA: 0x001FBF25 File Offset: 0x001FA125
			public BlackBoxRule ConstantDelimiter { get; private set; }

			// Token: 0x1700197A RID: 6522
			// (get) Token: 0x060093A9 RID: 37801 RVA: 0x001FBF2E File Offset: 0x001FA12E
			// (set) Token: 0x060093AA RID: 37802 RVA: 0x001FBF36 File Offset: 0x001FA136
			public BlackBoxRule Empty { get; private set; }

			// Token: 0x1700197B RID: 6523
			// (get) Token: 0x060093AB RID: 37803 RVA: 0x001FBF3F File Offset: 0x001FA13F
			// (set) Token: 0x060093AC RID: 37804 RVA: 0x001FBF47 File Offset: 0x001FA147
			public BlackBoxRule Concat { get; private set; }

			// Token: 0x1700197C RID: 6524
			// (get) Token: 0x060093AD RID: 37805 RVA: 0x001FBF50 File Offset: 0x001FA150
			// (set) Token: 0x060093AE RID: 37806 RVA: 0x001FBF58 File Offset: 0x001FA158
			public BlackBoxRule RegexMatch { get; private set; }

			// Token: 0x1700197D RID: 6525
			// (get) Token: 0x060093AF RID: 37807 RVA: 0x001FBF61 File Offset: 0x001FA161
			// (set) Token: 0x060093B0 RID: 37808 RVA: 0x001FBF69 File Offset: 0x001FA169
			public BlackBoxRule FieldMatch { get; private set; }

			// Token: 0x1700197E RID: 6526
			// (get) Token: 0x060093B1 RID: 37809 RVA: 0x001FBF72 File Offset: 0x001FA172
			// (set) Token: 0x060093B2 RID: 37810 RVA: 0x001FBF7A File Offset: 0x001FA17A
			public BlackBoxRule FixedWidth { get; private set; }

			// Token: 0x1700197F RID: 6527
			// (get) Token: 0x060093B3 RID: 37811 RVA: 0x001FBF83 File Offset: 0x001FA183
			// (set) Token: 0x060093B4 RID: 37812 RVA: 0x001FBF8B File Offset: 0x001FA18B
			public BlackBoxRule FixedWidthDelimiters { get; private set; }

			// Token: 0x17001980 RID: 6528
			// (get) Token: 0x060093B5 RID: 37813 RVA: 0x001FBF94 File Offset: 0x001FA194
			// (set) Token: 0x060093B6 RID: 37814 RVA: 0x001FBF9C File Offset: 0x001FA19C
			public BlackBoxRule GEN_Concat { get; private set; }

			// Token: 0x17001981 RID: 6529
			// (get) Token: 0x060093B7 RID: 37815 RVA: 0x001FBFA5 File Offset: 0x001FA1A5
			// (set) Token: 0x060093B8 RID: 37816 RVA: 0x001FBFAD File Offset: 0x001FA1AD
			public BlackBoxRule GEN_LookAround { get; private set; }

			// Token: 0x17001982 RID: 6530
			// (get) Token: 0x060093B9 RID: 37817 RVA: 0x001FBFB6 File Offset: 0x001FA1B6
			// (set) Token: 0x060093BA RID: 37818 RVA: 0x001FBFBE File Offset: 0x001FA1BE
			public BlackBoxRule GEN_FieldLookAroundEndPoints { get; private set; }

			// Token: 0x17001983 RID: 6531
			// (get) Token: 0x060093BB RID: 37819 RVA: 0x001FBFC7 File Offset: 0x001FA1C7
			// (set) Token: 0x060093BC RID: 37820 RVA: 0x001FBFCF File Offset: 0x001FA1CF
			public BlackBoxRule Item2 { get; private set; }

			// Token: 0x17001984 RID: 6532
			// (get) Token: 0x060093BD RID: 37821 RVA: 0x001FBFD8 File Offset: 0x001FA1D8
			// (set) Token: 0x060093BE RID: 37822 RVA: 0x001FBFE0 File Offset: 0x001FA1E0
			public BlackBoxRule Append { get; private set; }

			// Token: 0x17001985 RID: 6533
			// (get) Token: 0x060093BF RID: 37823 RVA: 0x001FBFE9 File Offset: 0x001FA1E9
			// (set) Token: 0x060093C0 RID: 37824 RVA: 0x001FBFF1 File Offset: 0x001FA1F1
			public BlackBoxRule Split { get; private set; }

			// Token: 0x17001986 RID: 6534
			// (get) Token: 0x060093C1 RID: 37825 RVA: 0x001FBFFA File Offset: 0x001FA1FA
			// (set) Token: 0x060093C2 RID: 37826 RVA: 0x001FC002 File Offset: 0x001FA202
			public BlackBoxRule List { get; private set; }

			// Token: 0x17001987 RID: 6535
			// (get) Token: 0x060093C3 RID: 37827 RVA: 0x001FC00B File Offset: 0x001FA20B
			// (set) Token: 0x060093C4 RID: 37828 RVA: 0x001FC013 File Offset: 0x001FA213
			public BlackBoxRule Item1 { get; private set; }

			// Token: 0x17001988 RID: 6536
			// (get) Token: 0x060093C5 RID: 37829 RVA: 0x001FC01C File Offset: 0x001FA21C
			// (set) Token: 0x060093C6 RID: 37830 RVA: 0x001FC024 File Offset: 0x001FA224
			public LetRule InnerLetWitness { get; private set; }

			// Token: 0x17001989 RID: 6537
			// (get) Token: 0x060093C7 RID: 37831 RVA: 0x001FC02D File Offset: 0x001FA22D
			// (set) Token: 0x060093C8 RID: 37832 RVA: 0x001FC035 File Offset: 0x001FA235
			public LetRule OuterLetWitness { get; private set; }

			// Token: 0x060093C9 RID: 37833 RVA: 0x001FC040 File Offset: 0x001FA240
			public GrammarRules(Grammar grammar)
			{
				this.ExtractionSplit = (BlackBoxRule)grammar.Rule("ExtractionSplit");
				this.SplitRegion = (BlackBoxRule)grammar.Rule("SplitRegion");
				this.SplitMultiple = (BlackBoxRule)grammar.Rule("SplitMultiple");
				this.DelimitersList = (BlackBoxRule)grammar.Rule("DelimitersList");
				this.EmptyDelimitersList = (BlackBoxRule)grammar.Rule("EmptyDelimitersList");
				this.ExtPointsList = (BlackBoxRule)grammar.Rule("ExtPointsList");
				this.EmptyExtPointsList = (BlackBoxRule)grammar.Rule("EmptyExtPointsList");
				this.ConditionalExtract = (BlackBoxRule)grammar.Rule("ConditionalExtract");
				this.SpecialCharPattern = (BlackBoxRule)grammar.Rule("SpecialCharPattern");
				this.LookAround = (BlackBoxRule)grammar.Rule("LookAround");
				this.FieldEndPoints = (BlackBoxRule)grammar.Rule("FieldEndPoints");
				this.FieldLookAroundEndPoints = (BlackBoxRule)grammar.Rule("FieldLookAroundEndPoints");
				this.ConstStr = (BlackBoxRule)grammar.Rule("ConstStr");
				this.ConstStrWithWhitespace = (BlackBoxRule)grammar.Rule("ConstStrWithWhitespace");
				this.ConstAlphStr = (BlackBoxRule)grammar.Rule("ConstAlphStr");
				this.ConstantDelimiterWithQuoting = (BlackBoxRule)grammar.Rule("ConstantDelimiterWithQuoting");
				this.ConstantDelimiter = (BlackBoxRule)grammar.Rule("ConstantDelimiter");
				this.Empty = (BlackBoxRule)grammar.Rule("Empty");
				this.Concat = (BlackBoxRule)grammar.Rule("Concat");
				this.RegexMatch = (BlackBoxRule)grammar.Rule("RegexMatch");
				this.FieldMatch = (BlackBoxRule)grammar.Rule("FieldMatch");
				this.FixedWidth = (BlackBoxRule)grammar.Rule("FixedWidth");
				this.FixedWidthDelimiters = (BlackBoxRule)grammar.Rule("FixedWidthDelimiters");
				this.GEN_Concat = (BlackBoxRule)grammar.Rule("GEN_Concat");
				this.GEN_LookAround = (BlackBoxRule)grammar.Rule("GEN_LookAround");
				this.GEN_FieldLookAroundEndPoints = (BlackBoxRule)grammar.Rule("GEN_FieldLookAroundEndPoints");
				this.Item2 = (BlackBoxRule)grammar.Rule("Item2");
				this.Append = (BlackBoxRule)grammar.Rule("Append");
				this.Split = (BlackBoxRule)grammar.Rule("Split");
				this.List = (BlackBoxRule)grammar.Rule("List");
				this.Item1 = (BlackBoxRule)grammar.Rule("Item1");
				this.InnerLetWitness = (LetRule)grammar.Rule("InnerLetWitness");
				this.OuterLetWitness = (LetRule)grammar.Rule("OuterLetWitness");
			}
		}

		// Token: 0x0200131E RID: 4894
		public class GrammarUnnamedConversions
		{
			// Token: 0x1700198A RID: 6538
			// (get) Token: 0x060093CA RID: 37834 RVA: 0x001FC329 File Offset: 0x001FA529
			// (set) Token: 0x060093CB RID: 37835 RVA: 0x001FC331 File Offset: 0x001FA531
			public ConversionRule splitMatches_multipleMatches { get; private set; }

			// Token: 0x1700198B RID: 6539
			// (get) Token: 0x060093CC RID: 37836 RVA: 0x001FC33A File Offset: 0x001FA53A
			// (set) Token: 0x060093CD RID: 37837 RVA: 0x001FC342 File Offset: 0x001FA542
			public ConversionRule splitMatches_constantDelimiterMatches { get; private set; }

			// Token: 0x1700198C RID: 6540
			// (get) Token: 0x060093CE RID: 37838 RVA: 0x001FC34B File Offset: 0x001FA54B
			// (set) Token: 0x060093CF RID: 37839 RVA: 0x001FC353 File Offset: 0x001FA553
			public ConversionRule splitMatches_fixedWidthMatches { get; private set; }

			// Token: 0x1700198D RID: 6541
			// (get) Token: 0x060093D0 RID: 37840 RVA: 0x001FC35C File Offset: 0x001FA55C
			// (set) Token: 0x060093D1 RID: 37841 RVA: 0x001FC364 File Offset: 0x001FA564
			public ConversionRule multipleMatches_d { get; private set; }

			// Token: 0x1700198E RID: 6542
			// (get) Token: 0x060093D2 RID: 37842 RVA: 0x001FC36D File Offset: 0x001FA56D
			// (set) Token: 0x060093D3 RID: 37843 RVA: 0x001FC375 File Offset: 0x001FA575
			public ConversionRule cndExtPoint_extPoint { get; private set; }

			// Token: 0x1700198F RID: 6543
			// (get) Token: 0x060093D4 RID: 37844 RVA: 0x001FC37E File Offset: 0x001FA57E
			// (set) Token: 0x060093D5 RID: 37845 RVA: 0x001FC386 File Offset: 0x001FA586
			public ConversionRule r_regexMatch { get; private set; }

			// Token: 0x060093D6 RID: 37846 RVA: 0x001FC390 File Offset: 0x001FA590
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.splitMatches_multipleMatches = (ConversionRule)grammar.Rule("~convert_splitMatches_multipleMatches");
				this.splitMatches_constantDelimiterMatches = (ConversionRule)grammar.Rule("~convert_splitMatches_constantDelimiterMatches");
				this.splitMatches_fixedWidthMatches = (ConversionRule)grammar.Rule("~convert_splitMatches_fixedWidthMatches");
				this.multipleMatches_d = (ConversionRule)grammar.Rule("~convert_multipleMatches_d");
				this.cndExtPoint_extPoint = (ConversionRule)grammar.Rule("~convert_cndExtPoint_extPoint");
				this.r_regexMatch = (ConversionRule)grammar.Rule("~convert_r_regexMatch");
			}
		}

		// Token: 0x0200131F RID: 4895
		public class GrammarHoles
		{
			// Token: 0x17001990 RID: 6544
			// (get) Token: 0x060093D7 RID: 37847 RVA: 0x001FC427 File Offset: 0x001FA627
			// (set) Token: 0x060093D8 RID: 37848 RVA: 0x001FC42F File Offset: 0x001FA62F
			public Hole v { get; private set; }

			// Token: 0x17001991 RID: 6545
			// (get) Token: 0x060093D9 RID: 37849 RVA: 0x001FC438 File Offset: 0x001FA638
			// (set) Token: 0x060093DA RID: 37850 RVA: 0x001FC440 File Offset: 0x001FA640
			public Hole regionSplit { get; private set; }

			// Token: 0x17001992 RID: 6546
			// (get) Token: 0x060093DB RID: 37851 RVA: 0x001FC449 File Offset: 0x001FA649
			// (set) Token: 0x060093DC RID: 37852 RVA: 0x001FC451 File Offset: 0x001FA651
			public Hole splitMatches { get; private set; }

			// Token: 0x17001993 RID: 6547
			// (get) Token: 0x060093DD RID: 37853 RVA: 0x001FC45A File Offset: 0x001FA65A
			// (set) Token: 0x060093DE RID: 37854 RVA: 0x001FC462 File Offset: 0x001FA662
			public Hole multipleMatches { get; private set; }

			// Token: 0x17001994 RID: 6548
			// (get) Token: 0x060093DF RID: 37855 RVA: 0x001FC46B File Offset: 0x001FA66B
			// (set) Token: 0x060093E0 RID: 37856 RVA: 0x001FC473 File Offset: 0x001FA673
			public Hole delimiterList { get; private set; }

			// Token: 0x17001995 RID: 6549
			// (get) Token: 0x060093E1 RID: 37857 RVA: 0x001FC47C File Offset: 0x001FA67C
			// (set) Token: 0x060093E2 RID: 37858 RVA: 0x001FC484 File Offset: 0x001FA684
			public Hole extractionPoints { get; private set; }

			// Token: 0x17001996 RID: 6550
			// (get) Token: 0x060093E3 RID: 37859 RVA: 0x001FC48D File Offset: 0x001FA68D
			// (set) Token: 0x060093E4 RID: 37860 RVA: 0x001FC495 File Offset: 0x001FA695
			public Hole cndExtPoint { get; private set; }

			// Token: 0x17001997 RID: 6551
			// (get) Token: 0x060093E5 RID: 37861 RVA: 0x001FC49E File Offset: 0x001FA69E
			// (set) Token: 0x060093E6 RID: 37862 RVA: 0x001FC4A6 File Offset: 0x001FA6A6
			public Hole extPoint { get; private set; }

			// Token: 0x17001998 RID: 6552
			// (get) Token: 0x060093E7 RID: 37863 RVA: 0x001FC4AF File Offset: 0x001FA6AF
			// (set) Token: 0x060093E8 RID: 37864 RVA: 0x001FC4B7 File Offset: 0x001FA6B7
			public Hole pred { get; private set; }

			// Token: 0x17001999 RID: 6553
			// (get) Token: 0x060093E9 RID: 37865 RVA: 0x001FC4C0 File Offset: 0x001FA6C0
			// (set) Token: 0x060093EA RID: 37866 RVA: 0x001FC4C8 File Offset: 0x001FA6C8
			public Hole pattern { get; private set; }

			// Token: 0x1700199A RID: 6554
			// (get) Token: 0x060093EB RID: 37867 RVA: 0x001FC4D1 File Offset: 0x001FA6D1
			// (set) Token: 0x060093EC RID: 37868 RVA: 0x001FC4D9 File Offset: 0x001FA6D9
			public Hole d { get; private set; }

			// Token: 0x1700199B RID: 6555
			// (get) Token: 0x060093ED RID: 37869 RVA: 0x001FC4E2 File Offset: 0x001FA6E2
			// (set) Token: 0x060093EE RID: 37870 RVA: 0x001FC4EA File Offset: 0x001FA6EA
			public Hole c { get; private set; }

			// Token: 0x1700199C RID: 6556
			// (get) Token: 0x060093EF RID: 37871 RVA: 0x001FC4F3 File Offset: 0x001FA6F3
			// (set) Token: 0x060093F0 RID: 37872 RVA: 0x001FC4FB File Offset: 0x001FA6FB
			public Hole quotingConf { get; private set; }

			// Token: 0x1700199D RID: 6557
			// (get) Token: 0x060093F1 RID: 37873 RVA: 0x001FC504 File Offset: 0x001FA704
			// (set) Token: 0x060093F2 RID: 37874 RVA: 0x001FC50C File Offset: 0x001FA70C
			public Hole constantDelimiterMatches { get; private set; }

			// Token: 0x1700199E RID: 6558
			// (get) Token: 0x060093F3 RID: 37875 RVA: 0x001FC515 File Offset: 0x001FA715
			// (set) Token: 0x060093F4 RID: 37876 RVA: 0x001FC51D File Offset: 0x001FA71D
			public Hole r { get; private set; }

			// Token: 0x1700199F RID: 6559
			// (get) Token: 0x060093F5 RID: 37877 RVA: 0x001FC526 File Offset: 0x001FA726
			// (set) Token: 0x060093F6 RID: 37878 RVA: 0x001FC52E File Offset: 0x001FA72E
			public Hole regexMatch { get; private set; }

			// Token: 0x170019A0 RID: 6560
			// (get) Token: 0x060093F7 RID: 37879 RVA: 0x001FC537 File Offset: 0x001FA737
			// (set) Token: 0x060093F8 RID: 37880 RVA: 0x001FC53F File Offset: 0x001FA73F
			public Hole fieldMatch { get; private set; }

			// Token: 0x170019A1 RID: 6561
			// (get) Token: 0x060093F9 RID: 37881 RVA: 0x001FC548 File Offset: 0x001FA748
			// (set) Token: 0x060093FA RID: 37882 RVA: 0x001FC550 File Offset: 0x001FA750
			public Hole fixedWidthMatches { get; private set; }

			// Token: 0x170019A2 RID: 6562
			// (get) Token: 0x060093FB RID: 37883 RVA: 0x001FC559 File Offset: 0x001FA759
			// (set) Token: 0x060093FC RID: 37884 RVA: 0x001FC561 File Offset: 0x001FA761
			public Hole gen_Concat { get; private set; }

			// Token: 0x170019A3 RID: 6563
			// (get) Token: 0x060093FD RID: 37885 RVA: 0x001FC56A File Offset: 0x001FA76A
			// (set) Token: 0x060093FE RID: 37886 RVA: 0x001FC572 File Offset: 0x001FA772
			public Hole gen_LookAround { get; private set; }

			// Token: 0x170019A4 RID: 6564
			// (get) Token: 0x060093FF RID: 37887 RVA: 0x001FC57B File Offset: 0x001FA77B
			// (set) Token: 0x06009400 RID: 37888 RVA: 0x001FC583 File Offset: 0x001FA783
			public Hole gen_LookAroundField { get; private set; }

			// Token: 0x170019A5 RID: 6565
			// (get) Token: 0x06009401 RID: 37889 RVA: 0x001FC58C File Offset: 0x001FA78C
			// (set) Token: 0x06009402 RID: 37890 RVA: 0x001FC594 File Offset: 0x001FA794
			public Hole delimiterStart { get; private set; }

			// Token: 0x170019A6 RID: 6566
			// (get) Token: 0x06009403 RID: 37891 RVA: 0x001FC59D File Offset: 0x001FA79D
			// (set) Token: 0x06009404 RID: 37892 RVA: 0x001FC5A5 File Offset: 0x001FA7A5
			public Hole delimiterEnd { get; private set; }

			// Token: 0x170019A7 RID: 6567
			// (get) Token: 0x06009405 RID: 37893 RVA: 0x001FC5AE File Offset: 0x001FA7AE
			// (set) Token: 0x06009406 RID: 37894 RVA: 0x001FC5B6 File Offset: 0x001FA7B6
			public Hole includeDelimiters { get; private set; }

			// Token: 0x170019A8 RID: 6568
			// (get) Token: 0x06009407 RID: 37895 RVA: 0x001FC5BF File Offset: 0x001FA7BF
			// (set) Token: 0x06009408 RID: 37896 RVA: 0x001FC5C7 File Offset: 0x001FA7C7
			public Hole fillStrategy { get; private set; }

			// Token: 0x170019A9 RID: 6569
			// (get) Token: 0x06009409 RID: 37897 RVA: 0x001FC5D0 File Offset: 0x001FA7D0
			// (set) Token: 0x0600940A RID: 37898 RVA: 0x001FC5D8 File Offset: 0x001FA7D8
			public Hole ignoreIndexes { get; private set; }

			// Token: 0x170019AA RID: 6570
			// (get) Token: 0x0600940B RID: 37899 RVA: 0x001FC5E1 File Offset: 0x001FA7E1
			// (set) Token: 0x0600940C RID: 37900 RVA: 0x001FC5E9 File Offset: 0x001FA7E9
			public Hole fieldStartPositions { get; private set; }

			// Token: 0x170019AB RID: 6571
			// (get) Token: 0x0600940D RID: 37901 RVA: 0x001FC5F2 File Offset: 0x001FA7F2
			// (set) Token: 0x0600940E RID: 37902 RVA: 0x001FC5FA File Offset: 0x001FA7FA
			public Hole delimiterPositions { get; private set; }

			// Token: 0x170019AC RID: 6572
			// (get) Token: 0x0600940F RID: 37903 RVA: 0x001FC603 File Offset: 0x001FA803
			// (set) Token: 0x06009410 RID: 37904 RVA: 0x001FC60B File Offset: 0x001FA80B
			public Hole fregex { get; private set; }

			// Token: 0x170019AD RID: 6573
			// (get) Token: 0x06009411 RID: 37905 RVA: 0x001FC614 File Offset: 0x001FA814
			// (set) Token: 0x06009412 RID: 37906 RVA: 0x001FC61C File Offset: 0x001FA81C
			public Hole s { get; private set; }

			// Token: 0x170019AE RID: 6574
			// (get) Token: 0x06009413 RID: 37907 RVA: 0x001FC625 File Offset: 0x001FA825
			// (set) Token: 0x06009414 RID: 37908 RVA: 0x001FC62D File Offset: 0x001FA82D
			public Hole a { get; private set; }

			// Token: 0x170019AF RID: 6575
			// (get) Token: 0x06009415 RID: 37909 RVA: 0x001FC636 File Offset: 0x001FA836
			// (set) Token: 0x06009416 RID: 37910 RVA: 0x001FC63E File Offset: 0x001FA83E
			public Hole numSplits { get; private set; }

			// Token: 0x170019B0 RID: 6576
			// (get) Token: 0x06009417 RID: 37911 RVA: 0x001FC647 File Offset: 0x001FA847
			// (set) Token: 0x06009418 RID: 37912 RVA: 0x001FC64F File Offset: 0x001FA84F
			public Hole regex { get; private set; }

			// Token: 0x170019B1 RID: 6577
			// (get) Token: 0x06009419 RID: 37913 RVA: 0x001FC658 File Offset: 0x001FA858
			// (set) Token: 0x0600941A RID: 37914 RVA: 0x001FC660 File Offset: 0x001FA860
			public Hole obj { get; private set; }

			// Token: 0x170019B2 RID: 6578
			// (get) Token: 0x0600941B RID: 37915 RVA: 0x001FC669 File Offset: 0x001FA869
			// (set) Token: 0x0600941C RID: 37916 RVA: 0x001FC671 File Offset: 0x001FA871
			public Hole delimiter { get; private set; }

			// Token: 0x170019B3 RID: 6579
			// (get) Token: 0x0600941D RID: 37917 RVA: 0x001FC67A File Offset: 0x001FA87A
			// (set) Token: 0x0600941E RID: 37918 RVA: 0x001FC682 File Offset: 0x001FA882
			public Hole output { get; private set; }

			// Token: 0x170019B4 RID: 6580
			// (get) Token: 0x0600941F RID: 37919 RVA: 0x001FC68B File Offset: 0x001FA88B
			// (set) Token: 0x06009420 RID: 37920 RVA: 0x001FC693 File Offset: 0x001FA893
			public Hole pair { get; private set; }

			// Token: 0x170019B5 RID: 6581
			// (get) Token: 0x06009421 RID: 37921 RVA: 0x001FC69C File Offset: 0x001FA89C
			// (set) Token: 0x06009422 RID: 37922 RVA: 0x001FC6A4 File Offset: 0x001FA8A4
			public Hole item1 { get; private set; }

			// Token: 0x170019B6 RID: 6582
			// (get) Token: 0x06009423 RID: 37923 RVA: 0x001FC6AD File Offset: 0x001FA8AD
			// (set) Token: 0x06009424 RID: 37924 RVA: 0x001FC6B5 File Offset: 0x001FA8B5
			public Hole _LetB0 { get; private set; }

			// Token: 0x170019B7 RID: 6583
			// (get) Token: 0x06009425 RID: 37925 RVA: 0x001FC6BE File Offset: 0x001FA8BE
			// (set) Token: 0x06009426 RID: 37926 RVA: 0x001FC6C6 File Offset: 0x001FA8C6
			public Hole _LetB1 { get; private set; }

			// Token: 0x170019B8 RID: 6584
			// (get) Token: 0x06009427 RID: 37927 RVA: 0x001FC6CF File Offset: 0x001FA8CF
			// (set) Token: 0x06009428 RID: 37928 RVA: 0x001FC6D7 File Offset: 0x001FA8D7
			public Hole _LetB2 { get; private set; }

			// Token: 0x170019B9 RID: 6585
			// (get) Token: 0x06009429 RID: 37929 RVA: 0x001FC6E0 File Offset: 0x001FA8E0
			// (set) Token: 0x0600942A RID: 37930 RVA: 0x001FC6E8 File Offset: 0x001FA8E8
			public Hole _LetB3 { get; private set; }

			// Token: 0x0600942B RID: 37931 RVA: 0x001FC6F4 File Offset: 0x001FA8F4
			public GrammarHoles(GrammarBuilders builders)
			{
				this.v = new Hole(builders.Symbol.v, null);
				this.regionSplit = new Hole(builders.Symbol.regionSplit, null);
				this.splitMatches = new Hole(builders.Symbol.splitMatches, null);
				this.multipleMatches = new Hole(builders.Symbol.multipleMatches, null);
				this.delimiterList = new Hole(builders.Symbol.delimiterList, null);
				this.extractionPoints = new Hole(builders.Symbol.extractionPoints, null);
				this.cndExtPoint = new Hole(builders.Symbol.cndExtPoint, null);
				this.extPoint = new Hole(builders.Symbol.extPoint, null);
				this.pred = new Hole(builders.Symbol.pred, null);
				this.pattern = new Hole(builders.Symbol.pattern, null);
				this.d = new Hole(builders.Symbol.d, null);
				this.c = new Hole(builders.Symbol.c, null);
				this.quotingConf = new Hole(builders.Symbol.quotingConf, null);
				this.constantDelimiterMatches = new Hole(builders.Symbol.constantDelimiterMatches, null);
				this.r = new Hole(builders.Symbol.r, null);
				this.regexMatch = new Hole(builders.Symbol.regexMatch, null);
				this.fieldMatch = new Hole(builders.Symbol.fieldMatch, null);
				this.fixedWidthMatches = new Hole(builders.Symbol.fixedWidthMatches, null);
				this.gen_Concat = new Hole(builders.Symbol.gen_Concat, null);
				this.gen_LookAround = new Hole(builders.Symbol.gen_LookAround, null);
				this.gen_LookAroundField = new Hole(builders.Symbol.gen_LookAroundField, null);
				this.delimiterStart = new Hole(builders.Symbol.delimiterStart, null);
				this.delimiterEnd = new Hole(builders.Symbol.delimiterEnd, null);
				this.includeDelimiters = new Hole(builders.Symbol.includeDelimiters, null);
				this.fillStrategy = new Hole(builders.Symbol.fillStrategy, null);
				this.ignoreIndexes = new Hole(builders.Symbol.ignoreIndexes, null);
				this.fieldStartPositions = new Hole(builders.Symbol.fieldStartPositions, null);
				this.delimiterPositions = new Hole(builders.Symbol.delimiterPositions, null);
				this.fregex = new Hole(builders.Symbol.fregex, null);
				this.s = new Hole(builders.Symbol.s, null);
				this.a = new Hole(builders.Symbol.a, null);
				this.numSplits = new Hole(builders.Symbol.numSplits, null);
				this.regex = new Hole(builders.Symbol.regex, null);
				this.obj = new Hole(builders.Symbol.obj, null);
				this.delimiter = new Hole(builders.Symbol.delimiter, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.pair = new Hole(builders.Symbol.pair, null);
				this.item1 = new Hole(builders.Symbol.item1, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
				this._LetB2 = new Hole(builders.Symbol._LetB2, null);
				this._LetB3 = new Hole(builders.Symbol._LetB3, null);
			}
		}

		// Token: 0x02001320 RID: 4896
		public class Nodes
		{
			// Token: 0x0600942C RID: 37932 RVA: 0x001FCAD0 File Offset: 0x001FACD0
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

			// Token: 0x170019BA RID: 6586
			// (get) Token: 0x0600942D RID: 37933 RVA: 0x001FCBB3 File Offset: 0x001FADB3
			// (set) Token: 0x0600942E RID: 37934 RVA: 0x001FCBBB File Offset: 0x001FADBB
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x170019BB RID: 6587
			// (get) Token: 0x0600942F RID: 37935 RVA: 0x001FCBC4 File Offset: 0x001FADC4
			// (set) Token: 0x06009430 RID: 37936 RVA: 0x001FCBCC File Offset: 0x001FADCC
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x170019BC RID: 6588
			// (get) Token: 0x06009431 RID: 37937 RVA: 0x001FCBD5 File Offset: 0x001FADD5
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x170019BD RID: 6589
			// (get) Token: 0x06009432 RID: 37938 RVA: 0x001FCBE2 File Offset: 0x001FADE2
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x170019BE RID: 6590
			// (get) Token: 0x06009433 RID: 37939 RVA: 0x001FCBEF File Offset: 0x001FADEF
			// (set) Token: 0x06009434 RID: 37940 RVA: 0x001FCBF7 File Offset: 0x001FADF7
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x170019BF RID: 6591
			// (get) Token: 0x06009435 RID: 37941 RVA: 0x001FCC00 File Offset: 0x001FAE00
			// (set) Token: 0x06009436 RID: 37942 RVA: 0x001FCC08 File Offset: 0x001FAE08
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x170019C0 RID: 6592
			// (get) Token: 0x06009437 RID: 37943 RVA: 0x001FCC11 File Offset: 0x001FAE11
			// (set) Token: 0x06009438 RID: 37944 RVA: 0x001FCC19 File Offset: 0x001FAE19
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x170019C1 RID: 6593
			// (get) Token: 0x06009439 RID: 37945 RVA: 0x001FCC22 File Offset: 0x001FAE22
			// (set) Token: 0x0600943A RID: 37946 RVA: 0x001FCC2A File Offset: 0x001FAE2A
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x170019C2 RID: 6594
			// (get) Token: 0x0600943B RID: 37947 RVA: 0x001FCC33 File Offset: 0x001FAE33
			// (set) Token: 0x0600943C RID: 37948 RVA: 0x001FCC3B File Offset: 0x001FAE3B
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x170019C3 RID: 6595
			// (get) Token: 0x0600943D RID: 37949 RVA: 0x001FCC44 File Offset: 0x001FAE44
			// (set) Token: 0x0600943E RID: 37950 RVA: 0x001FCC4C File Offset: 0x001FAE4C
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x170019C4 RID: 6596
			// (get) Token: 0x0600943F RID: 37951 RVA: 0x001FCC55 File Offset: 0x001FAE55
			// (set) Token: 0x06009440 RID: 37952 RVA: 0x001FCC5D File Offset: 0x001FAE5D
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04003D01 RID: 15617
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04003D02 RID: 15618
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001321 RID: 4897
			public class NodeRules
			{
				// Token: 0x06009441 RID: 37953 RVA: 0x001FCC66 File Offset: 0x001FAE66
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009442 RID: 37954 RVA: 0x001FCC75 File Offset: 0x001FAE75
				public extPoint extPoint(Record<int, int, int, int>? value)
				{
					return new extPoint(this._builders, value);
				}

				// Token: 0x06009443 RID: 37955 RVA: 0x001FCC83 File Offset: 0x001FAE83
				public pattern pattern(string value)
				{
					return new pattern(this._builders, value);
				}

				// Token: 0x06009444 RID: 37956 RVA: 0x001FCC91 File Offset: 0x001FAE91
				public quotingConf quotingConf(QuotingConfiguration value)
				{
					return new quotingConf(this._builders, value);
				}

				// Token: 0x06009445 RID: 37957 RVA: 0x001FCC9F File Offset: 0x001FAE9F
				public delimiterStart delimiterStart(bool value)
				{
					return new delimiterStart(this._builders, value);
				}

				// Token: 0x06009446 RID: 37958 RVA: 0x001FCCAD File Offset: 0x001FAEAD
				public delimiterEnd delimiterEnd(bool value)
				{
					return new delimiterEnd(this._builders, value);
				}

				// Token: 0x06009447 RID: 37959 RVA: 0x001FCCBB File Offset: 0x001FAEBB
				public includeDelimiters includeDelimiters(bool value)
				{
					return new includeDelimiters(this._builders, value);
				}

				// Token: 0x06009448 RID: 37960 RVA: 0x001FCCC9 File Offset: 0x001FAEC9
				public fillStrategy fillStrategy(FillStrategy value)
				{
					return new fillStrategy(this._builders, value);
				}

				// Token: 0x06009449 RID: 37961 RVA: 0x001FCCD7 File Offset: 0x001FAED7
				public ignoreIndexes ignoreIndexes(int[] value)
				{
					return new ignoreIndexes(this._builders, value);
				}

				// Token: 0x0600944A RID: 37962 RVA: 0x001FCCE5 File Offset: 0x001FAEE5
				public fieldStartPositions fieldStartPositions(int[] value)
				{
					return new fieldStartPositions(this._builders, value);
				}

				// Token: 0x0600944B RID: 37963 RVA: 0x001FCCF3 File Offset: 0x001FAEF3
				public delimiterPositions delimiterPositions(Record<int, int>[] value)
				{
					return new delimiterPositions(this._builders, value);
				}

				// Token: 0x0600944C RID: 37964 RVA: 0x001FCD01 File Offset: 0x001FAF01
				public fregex fregex(RegularExpression value)
				{
					return new fregex(this._builders, value);
				}

				// Token: 0x0600944D RID: 37965 RVA: 0x001FCD0F File Offset: 0x001FAF0F
				public s s(string value)
				{
					return new s(this._builders, value);
				}

				// Token: 0x0600944E RID: 37966 RVA: 0x001FCD1D File Offset: 0x001FAF1D
				public a a(string value)
				{
					return new a(this._builders, value);
				}

				// Token: 0x0600944F RID: 37967 RVA: 0x001FCD2B File Offset: 0x001FAF2B
				public numSplits numSplits(int value)
				{
					return new numSplits(this._builders, value);
				}

				// Token: 0x06009450 RID: 37968 RVA: 0x001FCD39 File Offset: 0x001FAF39
				public regex regex(RegularExpression value)
				{
					return new regex(this._builders, value);
				}

				// Token: 0x06009451 RID: 37969 RVA: 0x001FCD47 File Offset: 0x001FAF47
				public obj obj(object value)
				{
					return new obj(this._builders, value);
				}

				// Token: 0x06009452 RID: 37970 RVA: 0x001FCD55 File Offset: 0x001FAF55
				public delimiter delimiter(Record<RegularExpression, RegularExpression, RegularExpression> value)
				{
					return new delimiter(this._builders, value);
				}

				// Token: 0x06009453 RID: 37971 RVA: 0x001FCD63 File Offset: 0x001FAF63
				public regionSplit ExtractionSplit(v value0, delimiterList value1, extractionPoints value2)
				{
					return new ExtractionSplit(this._builders, value0, value1, value2);
				}

				// Token: 0x06009454 RID: 37972 RVA: 0x001FCD78 File Offset: 0x001FAF78
				public regionSplit SplitRegion(v value0, splitMatches value1, ignoreIndexes value2, numSplits value3, delimiterStart value4, delimiterEnd value5, includeDelimiters value6, fillStrategy value7)
				{
					return new SplitRegion(this._builders, value0, value1, value2, value3, value4, value5, value6, value7);
				}

				// Token: 0x06009455 RID: 37973 RVA: 0x001FCDA2 File Offset: 0x001FAFA2
				public multipleMatches SplitMultiple(multipleMatches value0, d value1)
				{
					return new SplitMultiple(this._builders, value0, value1);
				}

				// Token: 0x06009456 RID: 37974 RVA: 0x001FCDB6 File Offset: 0x001FAFB6
				public delimiterList DelimitersList(delimiterList value0, d value1)
				{
					return new DelimitersList(this._builders, value0, value1);
				}

				// Token: 0x06009457 RID: 37975 RVA: 0x001FCDCA File Offset: 0x001FAFCA
				public delimiterList EmptyDelimitersList()
				{
					return new EmptyDelimitersList(this._builders);
				}

				// Token: 0x06009458 RID: 37976 RVA: 0x001FCDDC File Offset: 0x001FAFDC
				public extractionPoints ExtPointsList(extractionPoints value0, cndExtPoint value1)
				{
					return new ExtPointsList(this._builders, value0, value1);
				}

				// Token: 0x06009459 RID: 37977 RVA: 0x001FCDF0 File Offset: 0x001FAFF0
				public extractionPoints EmptyExtPointsList()
				{
					return new EmptyExtPointsList(this._builders);
				}

				// Token: 0x0600945A RID: 37978 RVA: 0x001FCE02 File Offset: 0x001FB002
				public cndExtPoint ConditionalExtract(pred value0, extPoint value1, cndExtPoint value2)
				{
					return new ConditionalExtract(this._builders, value0, value1, value2);
				}

				// Token: 0x0600945B RID: 37979 RVA: 0x001FCE17 File Offset: 0x001FB017
				public pred SpecialCharPattern(v value0, pattern value1)
				{
					return new SpecialCharPattern(this._builders, value0, value1);
				}

				// Token: 0x0600945C RID: 37980 RVA: 0x001FCE2B File Offset: 0x001FB02B
				public d LookAround(r value0, c value1, r value2)
				{
					return new LookAround(this._builders, value0, value1, value2);
				}

				// Token: 0x0600945D RID: 37981 RVA: 0x001FCE40 File Offset: 0x001FB040
				public d FieldEndPoints(fieldMatch value0)
				{
					return new FieldEndPoints(this._builders, value0);
				}

				// Token: 0x0600945E RID: 37982 RVA: 0x001FCE53 File Offset: 0x001FB053
				public d FieldLookAroundEndPoints(regexMatch value0, fieldMatch value1, regexMatch value2)
				{
					return new FieldLookAroundEndPoints(this._builders, value0, value1, value2);
				}

				// Token: 0x0600945F RID: 37983 RVA: 0x001FCE68 File Offset: 0x001FB068
				public c ConstStr(v value0, s value1)
				{
					return new ConstStr(this._builders, value0, value1);
				}

				// Token: 0x06009460 RID: 37984 RVA: 0x001FCE7C File Offset: 0x001FB07C
				public c ConstStrWithWhitespace(v value0, s value1)
				{
					return new ConstStrWithWhitespace(this._builders, value0, value1);
				}

				// Token: 0x06009461 RID: 37985 RVA: 0x001FCE90 File Offset: 0x001FB090
				public c ConstAlphStr(v value0, a value1)
				{
					return new ConstAlphStr(this._builders, value0, value1);
				}

				// Token: 0x06009462 RID: 37986 RVA: 0x001FCEA4 File Offset: 0x001FB0A4
				public constantDelimiterMatches ConstantDelimiterWithQuoting(v value0, s value1, quotingConf value2)
				{
					return new ConstantDelimiterWithQuoting(this._builders, value0, value1, value2);
				}

				// Token: 0x06009463 RID: 37987 RVA: 0x001FCEB9 File Offset: 0x001FB0B9
				public constantDelimiterMatches ConstantDelimiter(v value0, s value1)
				{
					return new ConstantDelimiter(this._builders, value0, value1);
				}

				// Token: 0x06009464 RID: 37988 RVA: 0x001FCECD File Offset: 0x001FB0CD
				public r Empty(v value0)
				{
					return new Empty(this._builders, value0);
				}

				// Token: 0x06009465 RID: 37989 RVA: 0x001FCEE0 File Offset: 0x001FB0E0
				public r Concat(r value0, regexMatch value1)
				{
					return new Concat(this._builders, value0, value1);
				}

				// Token: 0x06009466 RID: 37990 RVA: 0x001FCEF4 File Offset: 0x001FB0F4
				public regexMatch RegexMatch(v value0, regex value1)
				{
					return new RegexMatch(this._builders, value0, value1);
				}

				// Token: 0x06009467 RID: 37991 RVA: 0x001FCF08 File Offset: 0x001FB108
				public fieldMatch FieldMatch(v value0, fregex value1)
				{
					return new FieldMatch(this._builders, value0, value1);
				}

				// Token: 0x06009468 RID: 37992 RVA: 0x001FCF1C File Offset: 0x001FB11C
				public fixedWidthMatches FixedWidth(v value0, fieldStartPositions value1)
				{
					return new FixedWidth(this._builders, value0, value1);
				}

				// Token: 0x06009469 RID: 37993 RVA: 0x001FCF30 File Offset: 0x001FB130
				public fixedWidthMatches FixedWidthDelimiters(v value0, delimiterPositions value1)
				{
					return new FixedWidthDelimiters(this._builders, value0, value1);
				}

				// Token: 0x0600946A RID: 37994 RVA: 0x001FCF44 File Offset: 0x001FB144
				public gen_Concat GEN_Concat(obj value0, obj value1)
				{
					return new GEN_Concat(this._builders, value0, value1);
				}

				// Token: 0x0600946B RID: 37995 RVA: 0x001FCF58 File Offset: 0x001FB158
				public gen_LookAround GEN_LookAround(obj value0, obj value1, obj value2)
				{
					return new GEN_LookAround(this._builders, value0, value1, value2);
				}

				// Token: 0x0600946C RID: 37996 RVA: 0x001FCF6D File Offset: 0x001FB16D
				public gen_LookAroundField GEN_FieldLookAroundEndPoints(obj value0, obj value1, obj value2)
				{
					return new GEN_FieldLookAroundEndPoints(this._builders, value0, value1, value2);
				}

				// Token: 0x0600946D RID: 37997 RVA: 0x001FCF82 File Offset: 0x001FB182
				public _LetB0 Item2(pair value0)
				{
					return new Item2(this._builders, value0);
				}

				// Token: 0x0600946E RID: 37998 RVA: 0x001FCF95 File Offset: 0x001FB195
				public _LetB1 Append(item1 value0, output value1)
				{
					return new Append(this._builders, value0, value1);
				}

				// Token: 0x0600946F RID: 37999 RVA: 0x001FCFA9 File Offset: 0x001FB1A9
				public _LetB2 Split(v value0, delimiter value1)
				{
					return new Split(this._builders, value0, value1);
				}

				// Token: 0x06009470 RID: 38000 RVA: 0x001FCFBD File Offset: 0x001FB1BD
				public output List(v value0)
				{
					return new List(this._builders, value0);
				}

				// Token: 0x06009471 RID: 38001 RVA: 0x001FCFD0 File Offset: 0x001FB1D0
				public item1 Item1(pair value0)
				{
					return new Item1(this._builders, value0);
				}

				// Token: 0x06009472 RID: 38002 RVA: 0x001FCFE3 File Offset: 0x001FB1E3
				public _LetB3 InnerLetWitness(_LetB0 value0, _LetB1 value1)
				{
					return new InnerLetWitness(this._builders, value0, value1);
				}

				// Token: 0x06009473 RID: 38003 RVA: 0x001FCFF7 File Offset: 0x001FB1F7
				public output OuterLetWitness(_LetB2 value0, _LetB3 value1)
				{
					return new OuterLetWitness(this._builders, value0, value1);
				}

				// Token: 0x04003D0A RID: 15626
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001322 RID: 4898
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06009474 RID: 38004 RVA: 0x001FD00B File Offset: 0x001FB20B
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009475 RID: 38005 RVA: 0x001FD01A File Offset: 0x001FB21A
				public splitMatches splitMatches_multipleMatches(multipleMatches value0)
				{
					return new splitMatches_multipleMatches(this._builders, value0);
				}

				// Token: 0x06009476 RID: 38006 RVA: 0x001FD02D File Offset: 0x001FB22D
				public splitMatches splitMatches_constantDelimiterMatches(constantDelimiterMatches value0)
				{
					return new splitMatches_constantDelimiterMatches(this._builders, value0);
				}

				// Token: 0x06009477 RID: 38007 RVA: 0x001FD040 File Offset: 0x001FB240
				public splitMatches splitMatches_fixedWidthMatches(fixedWidthMatches value0)
				{
					return new splitMatches_fixedWidthMatches(this._builders, value0);
				}

				// Token: 0x06009478 RID: 38008 RVA: 0x001FD053 File Offset: 0x001FB253
				public multipleMatches multipleMatches_d(d value0)
				{
					return new multipleMatches_d(this._builders, value0);
				}

				// Token: 0x06009479 RID: 38009 RVA: 0x001FD066 File Offset: 0x001FB266
				public cndExtPoint cndExtPoint_extPoint(extPoint value0)
				{
					return new cndExtPoint_extPoint(this._builders, value0);
				}

				// Token: 0x0600947A RID: 38010 RVA: 0x001FD079 File Offset: 0x001FB279
				public r r_regexMatch(regexMatch value0)
				{
					return new r_regexMatch(this._builders, value0);
				}

				// Token: 0x04003D0B RID: 15627
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001323 RID: 4899
			public class NodeVariables
			{
				// Token: 0x170019C5 RID: 6597
				// (get) Token: 0x0600947B RID: 38011 RVA: 0x001FD08C File Offset: 0x001FB28C
				// (set) Token: 0x0600947C RID: 38012 RVA: 0x001FD094 File Offset: 0x001FB294
				public v v { get; private set; }

				// Token: 0x170019C6 RID: 6598
				// (get) Token: 0x0600947D RID: 38013 RVA: 0x001FD09D File Offset: 0x001FB29D
				// (set) Token: 0x0600947E RID: 38014 RVA: 0x001FD0A5 File Offset: 0x001FB2A5
				public pair pair { get; private set; }

				// Token: 0x0600947F RID: 38015 RVA: 0x001FD0AE File Offset: 0x001FB2AE
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new v(builders);
					this.pair = new pair(builders);
				}
			}

			// Token: 0x02001324 RID: 4900
			public class NodeHoles
			{
				// Token: 0x170019C7 RID: 6599
				// (get) Token: 0x06009480 RID: 38016 RVA: 0x001FD0CE File Offset: 0x001FB2CE
				// (set) Token: 0x06009481 RID: 38017 RVA: 0x001FD0D6 File Offset: 0x001FB2D6
				public regionSplit regionSplit { get; private set; }

				// Token: 0x170019C8 RID: 6600
				// (get) Token: 0x06009482 RID: 38018 RVA: 0x001FD0DF File Offset: 0x001FB2DF
				// (set) Token: 0x06009483 RID: 38019 RVA: 0x001FD0E7 File Offset: 0x001FB2E7
				public splitMatches splitMatches { get; private set; }

				// Token: 0x170019C9 RID: 6601
				// (get) Token: 0x06009484 RID: 38020 RVA: 0x001FD0F0 File Offset: 0x001FB2F0
				// (set) Token: 0x06009485 RID: 38021 RVA: 0x001FD0F8 File Offset: 0x001FB2F8
				public multipleMatches multipleMatches { get; private set; }

				// Token: 0x170019CA RID: 6602
				// (get) Token: 0x06009486 RID: 38022 RVA: 0x001FD101 File Offset: 0x001FB301
				// (set) Token: 0x06009487 RID: 38023 RVA: 0x001FD109 File Offset: 0x001FB309
				public delimiterList delimiterList { get; private set; }

				// Token: 0x170019CB RID: 6603
				// (get) Token: 0x06009488 RID: 38024 RVA: 0x001FD112 File Offset: 0x001FB312
				// (set) Token: 0x06009489 RID: 38025 RVA: 0x001FD11A File Offset: 0x001FB31A
				public extractionPoints extractionPoints { get; private set; }

				// Token: 0x170019CC RID: 6604
				// (get) Token: 0x0600948A RID: 38026 RVA: 0x001FD123 File Offset: 0x001FB323
				// (set) Token: 0x0600948B RID: 38027 RVA: 0x001FD12B File Offset: 0x001FB32B
				public cndExtPoint cndExtPoint { get; private set; }

				// Token: 0x170019CD RID: 6605
				// (get) Token: 0x0600948C RID: 38028 RVA: 0x001FD134 File Offset: 0x001FB334
				// (set) Token: 0x0600948D RID: 38029 RVA: 0x001FD13C File Offset: 0x001FB33C
				public extPoint extPoint { get; private set; }

				// Token: 0x170019CE RID: 6606
				// (get) Token: 0x0600948E RID: 38030 RVA: 0x001FD145 File Offset: 0x001FB345
				// (set) Token: 0x0600948F RID: 38031 RVA: 0x001FD14D File Offset: 0x001FB34D
				public pred pred { get; private set; }

				// Token: 0x170019CF RID: 6607
				// (get) Token: 0x06009490 RID: 38032 RVA: 0x001FD156 File Offset: 0x001FB356
				// (set) Token: 0x06009491 RID: 38033 RVA: 0x001FD15E File Offset: 0x001FB35E
				public pattern pattern { get; private set; }

				// Token: 0x170019D0 RID: 6608
				// (get) Token: 0x06009492 RID: 38034 RVA: 0x001FD167 File Offset: 0x001FB367
				// (set) Token: 0x06009493 RID: 38035 RVA: 0x001FD16F File Offset: 0x001FB36F
				public d d { get; private set; }

				// Token: 0x170019D1 RID: 6609
				// (get) Token: 0x06009494 RID: 38036 RVA: 0x001FD178 File Offset: 0x001FB378
				// (set) Token: 0x06009495 RID: 38037 RVA: 0x001FD180 File Offset: 0x001FB380
				public c c { get; private set; }

				// Token: 0x170019D2 RID: 6610
				// (get) Token: 0x06009496 RID: 38038 RVA: 0x001FD189 File Offset: 0x001FB389
				// (set) Token: 0x06009497 RID: 38039 RVA: 0x001FD191 File Offset: 0x001FB391
				public quotingConf quotingConf { get; private set; }

				// Token: 0x170019D3 RID: 6611
				// (get) Token: 0x06009498 RID: 38040 RVA: 0x001FD19A File Offset: 0x001FB39A
				// (set) Token: 0x06009499 RID: 38041 RVA: 0x001FD1A2 File Offset: 0x001FB3A2
				public constantDelimiterMatches constantDelimiterMatches { get; private set; }

				// Token: 0x170019D4 RID: 6612
				// (get) Token: 0x0600949A RID: 38042 RVA: 0x001FD1AB File Offset: 0x001FB3AB
				// (set) Token: 0x0600949B RID: 38043 RVA: 0x001FD1B3 File Offset: 0x001FB3B3
				public r r { get; private set; }

				// Token: 0x170019D5 RID: 6613
				// (get) Token: 0x0600949C RID: 38044 RVA: 0x001FD1BC File Offset: 0x001FB3BC
				// (set) Token: 0x0600949D RID: 38045 RVA: 0x001FD1C4 File Offset: 0x001FB3C4
				public regexMatch regexMatch { get; private set; }

				// Token: 0x170019D6 RID: 6614
				// (get) Token: 0x0600949E RID: 38046 RVA: 0x001FD1CD File Offset: 0x001FB3CD
				// (set) Token: 0x0600949F RID: 38047 RVA: 0x001FD1D5 File Offset: 0x001FB3D5
				public fieldMatch fieldMatch { get; private set; }

				// Token: 0x170019D7 RID: 6615
				// (get) Token: 0x060094A0 RID: 38048 RVA: 0x001FD1DE File Offset: 0x001FB3DE
				// (set) Token: 0x060094A1 RID: 38049 RVA: 0x001FD1E6 File Offset: 0x001FB3E6
				public fixedWidthMatches fixedWidthMatches { get; private set; }

				// Token: 0x170019D8 RID: 6616
				// (get) Token: 0x060094A2 RID: 38050 RVA: 0x001FD1EF File Offset: 0x001FB3EF
				// (set) Token: 0x060094A3 RID: 38051 RVA: 0x001FD1F7 File Offset: 0x001FB3F7
				public gen_Concat gen_Concat { get; private set; }

				// Token: 0x170019D9 RID: 6617
				// (get) Token: 0x060094A4 RID: 38052 RVA: 0x001FD200 File Offset: 0x001FB400
				// (set) Token: 0x060094A5 RID: 38053 RVA: 0x001FD208 File Offset: 0x001FB408
				public gen_LookAround gen_LookAround { get; private set; }

				// Token: 0x170019DA RID: 6618
				// (get) Token: 0x060094A6 RID: 38054 RVA: 0x001FD211 File Offset: 0x001FB411
				// (set) Token: 0x060094A7 RID: 38055 RVA: 0x001FD219 File Offset: 0x001FB419
				public gen_LookAroundField gen_LookAroundField { get; private set; }

				// Token: 0x170019DB RID: 6619
				// (get) Token: 0x060094A8 RID: 38056 RVA: 0x001FD222 File Offset: 0x001FB422
				// (set) Token: 0x060094A9 RID: 38057 RVA: 0x001FD22A File Offset: 0x001FB42A
				public delimiterStart delimiterStart { get; private set; }

				// Token: 0x170019DC RID: 6620
				// (get) Token: 0x060094AA RID: 38058 RVA: 0x001FD233 File Offset: 0x001FB433
				// (set) Token: 0x060094AB RID: 38059 RVA: 0x001FD23B File Offset: 0x001FB43B
				public delimiterEnd delimiterEnd { get; private set; }

				// Token: 0x170019DD RID: 6621
				// (get) Token: 0x060094AC RID: 38060 RVA: 0x001FD244 File Offset: 0x001FB444
				// (set) Token: 0x060094AD RID: 38061 RVA: 0x001FD24C File Offset: 0x001FB44C
				public includeDelimiters includeDelimiters { get; private set; }

				// Token: 0x170019DE RID: 6622
				// (get) Token: 0x060094AE RID: 38062 RVA: 0x001FD255 File Offset: 0x001FB455
				// (set) Token: 0x060094AF RID: 38063 RVA: 0x001FD25D File Offset: 0x001FB45D
				public fillStrategy fillStrategy { get; private set; }

				// Token: 0x170019DF RID: 6623
				// (get) Token: 0x060094B0 RID: 38064 RVA: 0x001FD266 File Offset: 0x001FB466
				// (set) Token: 0x060094B1 RID: 38065 RVA: 0x001FD26E File Offset: 0x001FB46E
				public ignoreIndexes ignoreIndexes { get; private set; }

				// Token: 0x170019E0 RID: 6624
				// (get) Token: 0x060094B2 RID: 38066 RVA: 0x001FD277 File Offset: 0x001FB477
				// (set) Token: 0x060094B3 RID: 38067 RVA: 0x001FD27F File Offset: 0x001FB47F
				public fieldStartPositions fieldStartPositions { get; private set; }

				// Token: 0x170019E1 RID: 6625
				// (get) Token: 0x060094B4 RID: 38068 RVA: 0x001FD288 File Offset: 0x001FB488
				// (set) Token: 0x060094B5 RID: 38069 RVA: 0x001FD290 File Offset: 0x001FB490
				public delimiterPositions delimiterPositions { get; private set; }

				// Token: 0x170019E2 RID: 6626
				// (get) Token: 0x060094B6 RID: 38070 RVA: 0x001FD299 File Offset: 0x001FB499
				// (set) Token: 0x060094B7 RID: 38071 RVA: 0x001FD2A1 File Offset: 0x001FB4A1
				public fregex fregex { get; private set; }

				// Token: 0x170019E3 RID: 6627
				// (get) Token: 0x060094B8 RID: 38072 RVA: 0x001FD2AA File Offset: 0x001FB4AA
				// (set) Token: 0x060094B9 RID: 38073 RVA: 0x001FD2B2 File Offset: 0x001FB4B2
				public s s { get; private set; }

				// Token: 0x170019E4 RID: 6628
				// (get) Token: 0x060094BA RID: 38074 RVA: 0x001FD2BB File Offset: 0x001FB4BB
				// (set) Token: 0x060094BB RID: 38075 RVA: 0x001FD2C3 File Offset: 0x001FB4C3
				public a a { get; private set; }

				// Token: 0x170019E5 RID: 6629
				// (get) Token: 0x060094BC RID: 38076 RVA: 0x001FD2CC File Offset: 0x001FB4CC
				// (set) Token: 0x060094BD RID: 38077 RVA: 0x001FD2D4 File Offset: 0x001FB4D4
				public numSplits numSplits { get; private set; }

				// Token: 0x170019E6 RID: 6630
				// (get) Token: 0x060094BE RID: 38078 RVA: 0x001FD2DD File Offset: 0x001FB4DD
				// (set) Token: 0x060094BF RID: 38079 RVA: 0x001FD2E5 File Offset: 0x001FB4E5
				public regex regex { get; private set; }

				// Token: 0x170019E7 RID: 6631
				// (get) Token: 0x060094C0 RID: 38080 RVA: 0x001FD2EE File Offset: 0x001FB4EE
				// (set) Token: 0x060094C1 RID: 38081 RVA: 0x001FD2F6 File Offset: 0x001FB4F6
				public obj obj { get; private set; }

				// Token: 0x170019E8 RID: 6632
				// (get) Token: 0x060094C2 RID: 38082 RVA: 0x001FD2FF File Offset: 0x001FB4FF
				// (set) Token: 0x060094C3 RID: 38083 RVA: 0x001FD307 File Offset: 0x001FB507
				public delimiter delimiter { get; private set; }

				// Token: 0x170019E9 RID: 6633
				// (get) Token: 0x060094C4 RID: 38084 RVA: 0x001FD310 File Offset: 0x001FB510
				// (set) Token: 0x060094C5 RID: 38085 RVA: 0x001FD318 File Offset: 0x001FB518
				public output output { get; private set; }

				// Token: 0x170019EA RID: 6634
				// (get) Token: 0x060094C6 RID: 38086 RVA: 0x001FD321 File Offset: 0x001FB521
				// (set) Token: 0x060094C7 RID: 38087 RVA: 0x001FD329 File Offset: 0x001FB529
				public pair pair { get; private set; }

				// Token: 0x170019EB RID: 6635
				// (get) Token: 0x060094C8 RID: 38088 RVA: 0x001FD332 File Offset: 0x001FB532
				// (set) Token: 0x060094C9 RID: 38089 RVA: 0x001FD33A File Offset: 0x001FB53A
				public item1 item1 { get; private set; }

				// Token: 0x170019EC RID: 6636
				// (get) Token: 0x060094CA RID: 38090 RVA: 0x001FD343 File Offset: 0x001FB543
				// (set) Token: 0x060094CB RID: 38091 RVA: 0x001FD34B File Offset: 0x001FB54B
				public _LetB0 _LetB0 { get; private set; }

				// Token: 0x170019ED RID: 6637
				// (get) Token: 0x060094CC RID: 38092 RVA: 0x001FD354 File Offset: 0x001FB554
				// (set) Token: 0x060094CD RID: 38093 RVA: 0x001FD35C File Offset: 0x001FB55C
				public _LetB1 _LetB1 { get; private set; }

				// Token: 0x170019EE RID: 6638
				// (get) Token: 0x060094CE RID: 38094 RVA: 0x001FD365 File Offset: 0x001FB565
				// (set) Token: 0x060094CF RID: 38095 RVA: 0x001FD36D File Offset: 0x001FB56D
				public _LetB2 _LetB2 { get; private set; }

				// Token: 0x170019EF RID: 6639
				// (get) Token: 0x060094D0 RID: 38096 RVA: 0x001FD376 File Offset: 0x001FB576
				// (set) Token: 0x060094D1 RID: 38097 RVA: 0x001FD37E File Offset: 0x001FB57E
				public _LetB3 _LetB3 { get; private set; }

				// Token: 0x060094D2 RID: 38098 RVA: 0x001FD388 File Offset: 0x001FB588
				public NodeHoles(GrammarBuilders builders)
				{
					this.regionSplit = regionSplit.CreateHole(builders, null);
					this.splitMatches = splitMatches.CreateHole(builders, null);
					this.multipleMatches = multipleMatches.CreateHole(builders, null);
					this.delimiterList = delimiterList.CreateHole(builders, null);
					this.extractionPoints = extractionPoints.CreateHole(builders, null);
					this.cndExtPoint = cndExtPoint.CreateHole(builders, null);
					this.extPoint = extPoint.CreateHole(builders, null);
					this.pred = pred.CreateHole(builders, null);
					this.pattern = pattern.CreateHole(builders, null);
					this.d = d.CreateHole(builders, null);
					this.c = c.CreateHole(builders, null);
					this.quotingConf = quotingConf.CreateHole(builders, null);
					this.constantDelimiterMatches = constantDelimiterMatches.CreateHole(builders, null);
					this.r = r.CreateHole(builders, null);
					this.regexMatch = regexMatch.CreateHole(builders, null);
					this.fieldMatch = fieldMatch.CreateHole(builders, null);
					this.fixedWidthMatches = fixedWidthMatches.CreateHole(builders, null);
					this.gen_Concat = gen_Concat.CreateHole(builders, null);
					this.gen_LookAround = gen_LookAround.CreateHole(builders, null);
					this.gen_LookAroundField = gen_LookAroundField.CreateHole(builders, null);
					this.delimiterStart = delimiterStart.CreateHole(builders, null);
					this.delimiterEnd = delimiterEnd.CreateHole(builders, null);
					this.includeDelimiters = includeDelimiters.CreateHole(builders, null);
					this.fillStrategy = fillStrategy.CreateHole(builders, null);
					this.ignoreIndexes = ignoreIndexes.CreateHole(builders, null);
					this.fieldStartPositions = fieldStartPositions.CreateHole(builders, null);
					this.delimiterPositions = delimiterPositions.CreateHole(builders, null);
					this.fregex = fregex.CreateHole(builders, null);
					this.s = s.CreateHole(builders, null);
					this.a = a.CreateHole(builders, null);
					this.numSplits = numSplits.CreateHole(builders, null);
					this.regex = regex.CreateHole(builders, null);
					this.obj = obj.CreateHole(builders, null);
					this.delimiter = delimiter.CreateHole(builders, null);
					this.output = output.CreateHole(builders, null);
					this.pair = pair.CreateHole(builders, null);
					this.item1 = item1.CreateHole(builders, null);
					this._LetB0 = _LetB0.CreateHole(builders, null);
					this._LetB1 = _LetB1.CreateHole(builders, null);
					this._LetB2 = _LetB2.CreateHole(builders, null);
					this._LetB3 = _LetB3.CreateHole(builders, null);
				}
			}

			// Token: 0x02001325 RID: 4901
			public class NodeUnsafe
			{
				// Token: 0x060094D3 RID: 38099 RVA: 0x001FD5B0 File Offset: 0x001FB7B0
				public regionSplit regionSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit.CreateUnsafe(node);
				}

				// Token: 0x060094D4 RID: 38100 RVA: 0x001FD5B8 File Offset: 0x001FB7B8
				public splitMatches splitMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches.CreateUnsafe(node);
				}

				// Token: 0x060094D5 RID: 38101 RVA: 0x001FD5C0 File Offset: 0x001FB7C0
				public multipleMatches multipleMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches.CreateUnsafe(node);
				}

				// Token: 0x060094D6 RID: 38102 RVA: 0x001FD5C8 File Offset: 0x001FB7C8
				public delimiterList delimiterList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList.CreateUnsafe(node);
				}

				// Token: 0x060094D7 RID: 38103 RVA: 0x001FD5D0 File Offset: 0x001FB7D0
				public extractionPoints extractionPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints.CreateUnsafe(node);
				}

				// Token: 0x060094D8 RID: 38104 RVA: 0x001FD5D8 File Offset: 0x001FB7D8
				public cndExtPoint cndExtPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint.CreateUnsafe(node);
				}

				// Token: 0x060094D9 RID: 38105 RVA: 0x001FD5E0 File Offset: 0x001FB7E0
				public extPoint extPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint.CreateUnsafe(node);
				}

				// Token: 0x060094DA RID: 38106 RVA: 0x001FD5E8 File Offset: 0x001FB7E8
				public pred pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred.CreateUnsafe(node);
				}

				// Token: 0x060094DB RID: 38107 RVA: 0x001FD5F0 File Offset: 0x001FB7F0
				public pattern pattern(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern.CreateUnsafe(node);
				}

				// Token: 0x060094DC RID: 38108 RVA: 0x001FD5F8 File Offset: 0x001FB7F8
				public d d(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d.CreateUnsafe(node);
				}

				// Token: 0x060094DD RID: 38109 RVA: 0x001FD600 File Offset: 0x001FB800
				public c c(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c.CreateUnsafe(node);
				}

				// Token: 0x060094DE RID: 38110 RVA: 0x001FD608 File Offset: 0x001FB808
				public quotingConf quotingConf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf.CreateUnsafe(node);
				}

				// Token: 0x060094DF RID: 38111 RVA: 0x001FD610 File Offset: 0x001FB810
				public constantDelimiterMatches constantDelimiterMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches.CreateUnsafe(node);
				}

				// Token: 0x060094E0 RID: 38112 RVA: 0x001FD618 File Offset: 0x001FB818
				public r r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r.CreateUnsafe(node);
				}

				// Token: 0x060094E1 RID: 38113 RVA: 0x001FD620 File Offset: 0x001FB820
				public regexMatch regexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch.CreateUnsafe(node);
				}

				// Token: 0x060094E2 RID: 38114 RVA: 0x001FD628 File Offset: 0x001FB828
				public fieldMatch fieldMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch.CreateUnsafe(node);
				}

				// Token: 0x060094E3 RID: 38115 RVA: 0x001FD630 File Offset: 0x001FB830
				public fixedWidthMatches fixedWidthMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches.CreateUnsafe(node);
				}

				// Token: 0x060094E4 RID: 38116 RVA: 0x001FD638 File Offset: 0x001FB838
				public gen_Concat gen_Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat.CreateUnsafe(node);
				}

				// Token: 0x060094E5 RID: 38117 RVA: 0x001FD640 File Offset: 0x001FB840
				public gen_LookAround gen_LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround.CreateUnsafe(node);
				}

				// Token: 0x060094E6 RID: 38118 RVA: 0x001FD648 File Offset: 0x001FB848
				public gen_LookAroundField gen_LookAroundField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField.CreateUnsafe(node);
				}

				// Token: 0x060094E7 RID: 38119 RVA: 0x001FD650 File Offset: 0x001FB850
				public delimiterStart delimiterStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart.CreateUnsafe(node);
				}

				// Token: 0x060094E8 RID: 38120 RVA: 0x001FD658 File Offset: 0x001FB858
				public delimiterEnd delimiterEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd.CreateUnsafe(node);
				}

				// Token: 0x060094E9 RID: 38121 RVA: 0x001FD660 File Offset: 0x001FB860
				public includeDelimiters includeDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters.CreateUnsafe(node);
				}

				// Token: 0x060094EA RID: 38122 RVA: 0x001FD668 File Offset: 0x001FB868
				public fillStrategy fillStrategy(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy.CreateUnsafe(node);
				}

				// Token: 0x060094EB RID: 38123 RVA: 0x001FD670 File Offset: 0x001FB870
				public ignoreIndexes ignoreIndexes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes.CreateUnsafe(node);
				}

				// Token: 0x060094EC RID: 38124 RVA: 0x001FD678 File Offset: 0x001FB878
				public fieldStartPositions fieldStartPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions.CreateUnsafe(node);
				}

				// Token: 0x060094ED RID: 38125 RVA: 0x001FD680 File Offset: 0x001FB880
				public delimiterPositions delimiterPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions.CreateUnsafe(node);
				}

				// Token: 0x060094EE RID: 38126 RVA: 0x001FD688 File Offset: 0x001FB888
				public fregex fregex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex.CreateUnsafe(node);
				}

				// Token: 0x060094EF RID: 38127 RVA: 0x001FD690 File Offset: 0x001FB890
				public s s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s.CreateUnsafe(node);
				}

				// Token: 0x060094F0 RID: 38128 RVA: 0x001FD698 File Offset: 0x001FB898
				public a a(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a.CreateUnsafe(node);
				}

				// Token: 0x060094F1 RID: 38129 RVA: 0x001FD6A0 File Offset: 0x001FB8A0
				public numSplits numSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits.CreateUnsafe(node);
				}

				// Token: 0x060094F2 RID: 38130 RVA: 0x001FD6A8 File Offset: 0x001FB8A8
				public regex regex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex.CreateUnsafe(node);
				}

				// Token: 0x060094F3 RID: 38131 RVA: 0x001FD6B0 File Offset: 0x001FB8B0
				public obj obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj.CreateUnsafe(node);
				}

				// Token: 0x060094F4 RID: 38132 RVA: 0x001FD6B8 File Offset: 0x001FB8B8
				public delimiter delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter.CreateUnsafe(node);
				}

				// Token: 0x060094F5 RID: 38133 RVA: 0x001FD6C0 File Offset: 0x001FB8C0
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x060094F6 RID: 38134 RVA: 0x001FD6C8 File Offset: 0x001FB8C8
				public pair pair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair.CreateUnsafe(node);
				}

				// Token: 0x060094F7 RID: 38135 RVA: 0x001FD6D0 File Offset: 0x001FB8D0
				public item1 item1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1.CreateUnsafe(node);
				}

				// Token: 0x060094F8 RID: 38136 RVA: 0x001FD6D8 File Offset: 0x001FB8D8
				public _LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x060094F9 RID: 38137 RVA: 0x001FD6E0 File Offset: 0x001FB8E0
				public _LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}

				// Token: 0x060094FA RID: 38138 RVA: 0x001FD6E8 File Offset: 0x001FB8E8
				public _LetB2 _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2.CreateUnsafe(node);
				}

				// Token: 0x060094FB RID: 38139 RVA: 0x001FD6F0 File Offset: 0x001FB8F0
				public _LetB3 _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3.CreateUnsafe(node);
				}
			}

			// Token: 0x02001326 RID: 4902
			public class NodeCast
			{
				// Token: 0x060094FD RID: 38141 RVA: 0x001FD6F8 File Offset: 0x001FB8F8
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060094FE RID: 38142 RVA: 0x001FD708 File Offset: 0x001FB908
				public regionSplit regionSplit(ProgramNode node)
				{
					regionSplit? regionSplit = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit.CreateSafe(this._builders, node);
					if (regionSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regionSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regionSplit.Value;
				}

				// Token: 0x060094FF RID: 38143 RVA: 0x001FD75C File Offset: 0x001FB95C
				public splitMatches splitMatches(ProgramNode node)
				{
					splitMatches? splitMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches.CreateSafe(this._builders, node);
					if (splitMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMatches.Value;
				}

				// Token: 0x06009500 RID: 38144 RVA: 0x001FD7B0 File Offset: 0x001FB9B0
				public multipleMatches multipleMatches(ProgramNode node)
				{
					multipleMatches? multipleMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches.CreateSafe(this._builders, node);
					if (multipleMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multipleMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multipleMatches.Value;
				}

				// Token: 0x06009501 RID: 38145 RVA: 0x001FD804 File Offset: 0x001FBA04
				public delimiterList delimiterList(ProgramNode node)
				{
					delimiterList? delimiterList = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList.CreateSafe(this._builders, node);
					if (delimiterList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiterList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiterList.Value;
				}

				// Token: 0x06009502 RID: 38146 RVA: 0x001FD858 File Offset: 0x001FBA58
				public extractionPoints extractionPoints(ProgramNode node)
				{
					extractionPoints? extractionPoints = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints.CreateSafe(this._builders, node);
					if (extractionPoints == null)
					{
						string text = "node";
						string text2 = "expected node for symbol extractionPoints but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extractionPoints.Value;
				}

				// Token: 0x06009503 RID: 38147 RVA: 0x001FD8AC File Offset: 0x001FBAAC
				public cndExtPoint cndExtPoint(ProgramNode node)
				{
					cndExtPoint? cndExtPoint = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint.CreateSafe(this._builders, node);
					if (cndExtPoint == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cndExtPoint but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cndExtPoint.Value;
				}

				// Token: 0x06009504 RID: 38148 RVA: 0x001FD900 File Offset: 0x001FBB00
				public extPoint extPoint(ProgramNode node)
				{
					extPoint? extPoint = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint.CreateSafe(this._builders, node);
					if (extPoint == null)
					{
						string text = "node";
						string text2 = "expected node for symbol extPoint but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extPoint.Value;
				}

				// Token: 0x06009505 RID: 38149 RVA: 0x001FD954 File Offset: 0x001FBB54
				public pred pred(ProgramNode node)
				{
					pred? pred = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pred.Value;
				}

				// Token: 0x06009506 RID: 38150 RVA: 0x001FD9A8 File Offset: 0x001FBBA8
				public pattern pattern(ProgramNode node)
				{
					pattern? pattern = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern.CreateSafe(this._builders, node);
					if (pattern == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pattern but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pattern.Value;
				}

				// Token: 0x06009507 RID: 38151 RVA: 0x001FD9FC File Offset: 0x001FBBFC
				public d d(ProgramNode node)
				{
					d? d = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d.CreateSafe(this._builders, node);
					if (d == null)
					{
						string text = "node";
						string text2 = "expected node for symbol d but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return d.Value;
				}

				// Token: 0x06009508 RID: 38152 RVA: 0x001FDA50 File Offset: 0x001FBC50
				public c c(ProgramNode node)
				{
					c? c = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c.CreateSafe(this._builders, node);
					if (c == null)
					{
						string text = "node";
						string text2 = "expected node for symbol c but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return c.Value;
				}

				// Token: 0x06009509 RID: 38153 RVA: 0x001FDAA4 File Offset: 0x001FBCA4
				public quotingConf quotingConf(ProgramNode node)
				{
					quotingConf? quotingConf = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf.CreateSafe(this._builders, node);
					if (quotingConf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol quotingConf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return quotingConf.Value;
				}

				// Token: 0x0600950A RID: 38154 RVA: 0x001FDAF8 File Offset: 0x001FBCF8
				public constantDelimiterMatches constantDelimiterMatches(ProgramNode node)
				{
					constantDelimiterMatches? constantDelimiterMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches.CreateSafe(this._builders, node);
					if (constantDelimiterMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol constantDelimiterMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constantDelimiterMatches.Value;
				}

				// Token: 0x0600950B RID: 38155 RVA: 0x001FDB4C File Offset: 0x001FBD4C
				public r r(ProgramNode node)
				{
					r? r = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						string text = "node";
						string text2 = "expected node for symbol r but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return r.Value;
				}

				// Token: 0x0600950C RID: 38156 RVA: 0x001FDBA0 File Offset: 0x001FBDA0
				public regexMatch regexMatch(ProgramNode node)
				{
					regexMatch? regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch.CreateSafe(this._builders, node);
					if (regexMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regexMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexMatch.Value;
				}

				// Token: 0x0600950D RID: 38157 RVA: 0x001FDBF4 File Offset: 0x001FBDF4
				public fieldMatch fieldMatch(ProgramNode node)
				{
					fieldMatch? fieldMatch = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch.CreateSafe(this._builders, node);
					if (fieldMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fieldMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldMatch.Value;
				}

				// Token: 0x0600950E RID: 38158 RVA: 0x001FDC48 File Offset: 0x001FBE48
				public fixedWidthMatches fixedWidthMatches(ProgramNode node)
				{
					fixedWidthMatches? fixedWidthMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches.CreateSafe(this._builders, node);
					if (fixedWidthMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fixedWidthMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fixedWidthMatches.Value;
				}

				// Token: 0x0600950F RID: 38159 RVA: 0x001FDC9C File Offset: 0x001FBE9C
				public gen_Concat gen_Concat(ProgramNode node)
				{
					gen_Concat? gen_Concat = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat.CreateSafe(this._builders, node);
					if (gen_Concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_Concat.Value;
				}

				// Token: 0x06009510 RID: 38160 RVA: 0x001FDCF0 File Offset: 0x001FBEF0
				public gen_LookAround gen_LookAround(ProgramNode node)
				{
					gen_LookAround? gen_LookAround = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround.CreateSafe(this._builders, node);
					if (gen_LookAround == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_LookAround but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_LookAround.Value;
				}

				// Token: 0x06009511 RID: 38161 RVA: 0x001FDD44 File Offset: 0x001FBF44
				public gen_LookAroundField gen_LookAroundField(ProgramNode node)
				{
					gen_LookAroundField? gen_LookAroundField = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField.CreateSafe(this._builders, node);
					if (gen_LookAroundField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_LookAroundField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_LookAroundField.Value;
				}

				// Token: 0x06009512 RID: 38162 RVA: 0x001FDD98 File Offset: 0x001FBF98
				public delimiterStart delimiterStart(ProgramNode node)
				{
					delimiterStart? delimiterStart = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart.CreateSafe(this._builders, node);
					if (delimiterStart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiterStart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiterStart.Value;
				}

				// Token: 0x06009513 RID: 38163 RVA: 0x001FDDEC File Offset: 0x001FBFEC
				public delimiterEnd delimiterEnd(ProgramNode node)
				{
					delimiterEnd? delimiterEnd = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd.CreateSafe(this._builders, node);
					if (delimiterEnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiterEnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiterEnd.Value;
				}

				// Token: 0x06009514 RID: 38164 RVA: 0x001FDE40 File Offset: 0x001FC040
				public includeDelimiters includeDelimiters(ProgramNode node)
				{
					includeDelimiters? includeDelimiters = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters.CreateSafe(this._builders, node);
					if (includeDelimiters == null)
					{
						string text = "node";
						string text2 = "expected node for symbol includeDelimiters but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return includeDelimiters.Value;
				}

				// Token: 0x06009515 RID: 38165 RVA: 0x001FDE94 File Offset: 0x001FC094
				public fillStrategy fillStrategy(ProgramNode node)
				{
					fillStrategy? fillStrategy = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy.CreateSafe(this._builders, node);
					if (fillStrategy == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fillStrategy but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fillStrategy.Value;
				}

				// Token: 0x06009516 RID: 38166 RVA: 0x001FDEE8 File Offset: 0x001FC0E8
				public ignoreIndexes ignoreIndexes(ProgramNode node)
				{
					ignoreIndexes? ignoreIndexes = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes.CreateSafe(this._builders, node);
					if (ignoreIndexes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ignoreIndexes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ignoreIndexes.Value;
				}

				// Token: 0x06009517 RID: 38167 RVA: 0x001FDF3C File Offset: 0x001FC13C
				public fieldStartPositions fieldStartPositions(ProgramNode node)
				{
					fieldStartPositions? fieldStartPositions = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions.CreateSafe(this._builders, node);
					if (fieldStartPositions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fieldStartPositions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldStartPositions.Value;
				}

				// Token: 0x06009518 RID: 38168 RVA: 0x001FDF90 File Offset: 0x001FC190
				public delimiterPositions delimiterPositions(ProgramNode node)
				{
					delimiterPositions? delimiterPositions = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions.CreateSafe(this._builders, node);
					if (delimiterPositions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiterPositions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiterPositions.Value;
				}

				// Token: 0x06009519 RID: 38169 RVA: 0x001FDFE4 File Offset: 0x001FC1E4
				public fregex fregex(ProgramNode node)
				{
					fregex? fregex = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex.CreateSafe(this._builders, node);
					if (fregex == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fregex but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fregex.Value;
				}

				// Token: 0x0600951A RID: 38170 RVA: 0x001FE038 File Offset: 0x001FC238
				public s s(ProgramNode node)
				{
					s? s = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						string text = "node";
						string text2 = "expected node for symbol s but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return s.Value;
				}

				// Token: 0x0600951B RID: 38171 RVA: 0x001FE08C File Offset: 0x001FC28C
				public a a(ProgramNode node)
				{
					a? a = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a.CreateSafe(this._builders, node);
					if (a == null)
					{
						string text = "node";
						string text2 = "expected node for symbol a but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return a.Value;
				}

				// Token: 0x0600951C RID: 38172 RVA: 0x001FE0E0 File Offset: 0x001FC2E0
				public numSplits numSplits(ProgramNode node)
				{
					numSplits? numSplits = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits.CreateSafe(this._builders, node);
					if (numSplits == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numSplits but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numSplits.Value;
				}

				// Token: 0x0600951D RID: 38173 RVA: 0x001FE134 File Offset: 0x001FC334
				public regex regex(ProgramNode node)
				{
					regex? regex = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex.CreateSafe(this._builders, node);
					if (regex == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regex but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regex.Value;
				}

				// Token: 0x0600951E RID: 38174 RVA: 0x001FE188 File Offset: 0x001FC388
				public obj obj(ProgramNode node)
				{
					obj? obj = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj.CreateSafe(this._builders, node);
					if (obj == null)
					{
						string text = "node";
						string text2 = "expected node for symbol obj but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return obj.Value;
				}

				// Token: 0x0600951F RID: 38175 RVA: 0x001FE1DC File Offset: 0x001FC3DC
				public delimiter delimiter(ProgramNode node)
				{
					delimiter? delimiter = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiter.Value;
				}

				// Token: 0x06009520 RID: 38176 RVA: 0x001FE230 File Offset: 0x001FC430
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06009521 RID: 38177 RVA: 0x001FE284 File Offset: 0x001FC484
				public pair pair(ProgramNode node)
				{
					pair? pair = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair.CreateSafe(this._builders, node);
					if (pair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pair.Value;
				}

				// Token: 0x06009522 RID: 38178 RVA: 0x001FE2D8 File Offset: 0x001FC4D8
				public item1 item1(ProgramNode node)
				{
					item1? item = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1.CreateSafe(this._builders, node);
					if (item == null)
					{
						string text = "node";
						string text2 = "expected node for symbol item1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return item.Value;
				}

				// Token: 0x06009523 RID: 38179 RVA: 0x001FE32C File Offset: 0x001FC52C
				public _LetB0 _LetB0(ProgramNode node)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06009524 RID: 38180 RVA: 0x001FE380 File Offset: 0x001FC580
				public _LetB1 _LetB1(ProgramNode node)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06009525 RID: 38181 RVA: 0x001FE3D4 File Offset: 0x001FC5D4
				public _LetB2 _LetB2(ProgramNode node)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06009526 RID: 38182 RVA: 0x001FE428 File Offset: 0x001FC628
				public _LetB3 _LetB3(ProgramNode node)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x04003D37 RID: 15671
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001327 RID: 4903
			public class RuleCast
			{
				// Token: 0x06009527 RID: 38183 RVA: 0x001FE479 File Offset: 0x001FC679
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009528 RID: 38184 RVA: 0x001FE488 File Offset: 0x001FC688
				public ExtractionSplit ExtractionSplit(ProgramNode node)
				{
					ExtractionSplit? extractionSplit = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtractionSplit.CreateSafe(this._builders, node);
					if (extractionSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ExtractionSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extractionSplit.Value;
				}

				// Token: 0x06009529 RID: 38185 RVA: 0x001FE4DC File Offset: 0x001FC6DC
				public SplitRegion SplitRegion(ProgramNode node)
				{
					SplitRegion? splitRegion = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitRegion.CreateSafe(this._builders, node);
					if (splitRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitRegion.Value;
				}

				// Token: 0x0600952A RID: 38186 RVA: 0x001FE530 File Offset: 0x001FC730
				public splitMatches_multipleMatches splitMatches_multipleMatches(ProgramNode node)
				{
					splitMatches_multipleMatches? splitMatches_multipleMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_multipleMatches.CreateSafe(this._builders, node);
					if (splitMatches_multipleMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitMatches_multipleMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMatches_multipleMatches.Value;
				}

				// Token: 0x0600952B RID: 38187 RVA: 0x001FE584 File Offset: 0x001FC784
				public splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches(ProgramNode node)
				{
					splitMatches_constantDelimiterMatches? splitMatches_constantDelimiterMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_constantDelimiterMatches.CreateSafe(this._builders, node);
					if (splitMatches_constantDelimiterMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitMatches_constantDelimiterMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMatches_constantDelimiterMatches.Value;
				}

				// Token: 0x0600952C RID: 38188 RVA: 0x001FE5D8 File Offset: 0x001FC7D8
				public splitMatches_fixedWidthMatches splitMatches_fixedWidthMatches(ProgramNode node)
				{
					splitMatches_fixedWidthMatches? splitMatches_fixedWidthMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_fixedWidthMatches.CreateSafe(this._builders, node);
					if (splitMatches_fixedWidthMatches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitMatches_fixedWidthMatches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMatches_fixedWidthMatches.Value;
				}

				// Token: 0x0600952D RID: 38189 RVA: 0x001FE62C File Offset: 0x001FC82C
				public SplitMultiple SplitMultiple(ProgramNode node)
				{
					SplitMultiple? splitMultiple = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitMultiple.CreateSafe(this._builders, node);
					if (splitMultiple == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitMultiple but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMultiple.Value;
				}

				// Token: 0x0600952E RID: 38190 RVA: 0x001FE680 File Offset: 0x001FC880
				public multipleMatches_d multipleMatches_d(ProgramNode node)
				{
					multipleMatches_d? multipleMatches_d = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.multipleMatches_d.CreateSafe(this._builders, node);
					if (multipleMatches_d == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multipleMatches_d but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multipleMatches_d.Value;
				}

				// Token: 0x0600952F RID: 38191 RVA: 0x001FE6D4 File Offset: 0x001FC8D4
				public DelimitersList DelimitersList(ProgramNode node)
				{
					DelimitersList? delimitersList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.DelimitersList.CreateSafe(this._builders, node);
					if (delimitersList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DelimitersList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimitersList.Value;
				}

				// Token: 0x06009530 RID: 38192 RVA: 0x001FE728 File Offset: 0x001FC928
				public EmptyDelimitersList EmptyDelimitersList(ProgramNode node)
				{
					EmptyDelimitersList? emptyDelimitersList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyDelimitersList.CreateSafe(this._builders, node);
					if (emptyDelimitersList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EmptyDelimitersList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return emptyDelimitersList.Value;
				}

				// Token: 0x06009531 RID: 38193 RVA: 0x001FE77C File Offset: 0x001FC97C
				public ExtPointsList ExtPointsList(ProgramNode node)
				{
					ExtPointsList? extPointsList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtPointsList.CreateSafe(this._builders, node);
					if (extPointsList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ExtPointsList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extPointsList.Value;
				}

				// Token: 0x06009532 RID: 38194 RVA: 0x001FE7D0 File Offset: 0x001FC9D0
				public EmptyExtPointsList EmptyExtPointsList(ProgramNode node)
				{
					EmptyExtPointsList? emptyExtPointsList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyExtPointsList.CreateSafe(this._builders, node);
					if (emptyExtPointsList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EmptyExtPointsList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return emptyExtPointsList.Value;
				}

				// Token: 0x06009533 RID: 38195 RVA: 0x001FE824 File Offset: 0x001FCA24
				public cndExtPoint_extPoint cndExtPoint_extPoint(ProgramNode node)
				{
					cndExtPoint_extPoint? cndExtPoint_extPoint = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.cndExtPoint_extPoint.CreateSafe(this._builders, node);
					if (cndExtPoint_extPoint == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cndExtPoint_extPoint but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cndExtPoint_extPoint.Value;
				}

				// Token: 0x06009534 RID: 38196 RVA: 0x001FE878 File Offset: 0x001FCA78
				public ConditionalExtract ConditionalExtract(ProgramNode node)
				{
					ConditionalExtract? conditionalExtract = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConditionalExtract.CreateSafe(this._builders, node);
					if (conditionalExtract == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConditionalExtract but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conditionalExtract.Value;
				}

				// Token: 0x06009535 RID: 38197 RVA: 0x001FE8CC File Offset: 0x001FCACC
				public SpecialCharPattern SpecialCharPattern(ProgramNode node)
				{
					SpecialCharPattern? specialCharPattern = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SpecialCharPattern.CreateSafe(this._builders, node);
					if (specialCharPattern == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SpecialCharPattern but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return specialCharPattern.Value;
				}

				// Token: 0x06009536 RID: 38198 RVA: 0x001FE920 File Offset: 0x001FCB20
				public LookAround LookAround(ProgramNode node)
				{
					LookAround? lookAround = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.LookAround.CreateSafe(this._builders, node);
					if (lookAround == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LookAround but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookAround.Value;
				}

				// Token: 0x06009537 RID: 38199 RVA: 0x001FE974 File Offset: 0x001FCB74
				public FieldEndPoints FieldEndPoints(ProgramNode node)
				{
					FieldEndPoints? fieldEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldEndPoints.CreateSafe(this._builders, node);
					if (fieldEndPoints == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FieldEndPoints but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldEndPoints.Value;
				}

				// Token: 0x06009538 RID: 38200 RVA: 0x001FE9C8 File Offset: 0x001FCBC8
				public FieldLookAroundEndPoints FieldLookAroundEndPoints(ProgramNode node)
				{
					FieldLookAroundEndPoints? fieldLookAroundEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldLookAroundEndPoints.CreateSafe(this._builders, node);
					if (fieldLookAroundEndPoints == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FieldLookAroundEndPoints but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldLookAroundEndPoints.Value;
				}

				// Token: 0x06009539 RID: 38201 RVA: 0x001FEA1C File Offset: 0x001FCC1C
				public ConstStr ConstStr(ProgramNode node)
				{
					ConstStr? constStr = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constStr.Value;
				}

				// Token: 0x0600953A RID: 38202 RVA: 0x001FEA70 File Offset: 0x001FCC70
				public ConstStrWithWhitespace ConstStrWithWhitespace(ProgramNode node)
				{
					ConstStrWithWhitespace? constStrWithWhitespace = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStrWithWhitespace.CreateSafe(this._builders, node);
					if (constStrWithWhitespace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstStrWithWhitespace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constStrWithWhitespace.Value;
				}

				// Token: 0x0600953B RID: 38203 RVA: 0x001FEAC4 File Offset: 0x001FCCC4
				public ConstAlphStr ConstAlphStr(ProgramNode node)
				{
					ConstAlphStr? constAlphStr = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstAlphStr.CreateSafe(this._builders, node);
					if (constAlphStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstAlphStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constAlphStr.Value;
				}

				// Token: 0x0600953C RID: 38204 RVA: 0x001FEB18 File Offset: 0x001FCD18
				public ConstantDelimiterWithQuoting ConstantDelimiterWithQuoting(ProgramNode node)
				{
					ConstantDelimiterWithQuoting? constantDelimiterWithQuoting = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiterWithQuoting.CreateSafe(this._builders, node);
					if (constantDelimiterWithQuoting == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstantDelimiterWithQuoting but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constantDelimiterWithQuoting.Value;
				}

				// Token: 0x0600953D RID: 38205 RVA: 0x001FEB6C File Offset: 0x001FCD6C
				public ConstantDelimiter ConstantDelimiter(ProgramNode node)
				{
					ConstantDelimiter? constantDelimiter = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiter.CreateSafe(this._builders, node);
					if (constantDelimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstantDelimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constantDelimiter.Value;
				}

				// Token: 0x0600953E RID: 38206 RVA: 0x001FEBC0 File Offset: 0x001FCDC0
				public Empty Empty(ProgramNode node)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Empty but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return empty.Value;
				}

				// Token: 0x0600953F RID: 38207 RVA: 0x001FEC14 File Offset: 0x001FCE14
				public r_regexMatch r_regexMatch(ProgramNode node)
				{
					r_regexMatch? r_regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.r_regexMatch.CreateSafe(this._builders, node);
					if (r_regexMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol r_regexMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return r_regexMatch.Value;
				}

				// Token: 0x06009540 RID: 38208 RVA: 0x001FEC68 File Offset: 0x001FCE68
				public Concat Concat(ProgramNode node)
				{
					Concat? concat = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concat.Value;
				}

				// Token: 0x06009541 RID: 38209 RVA: 0x001FECBC File Offset: 0x001FCEBC
				public RegexMatch RegexMatch(ProgramNode node)
				{
					RegexMatch? regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.RegexMatch.CreateSafe(this._builders, node);
					if (regexMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RegexMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexMatch.Value;
				}

				// Token: 0x06009542 RID: 38210 RVA: 0x001FED10 File Offset: 0x001FCF10
				public FieldMatch FieldMatch(ProgramNode node)
				{
					FieldMatch? fieldMatch = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldMatch.CreateSafe(this._builders, node);
					if (fieldMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FieldMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldMatch.Value;
				}

				// Token: 0x06009543 RID: 38211 RVA: 0x001FED64 File Offset: 0x001FCF64
				public FixedWidth FixedWidth(ProgramNode node)
				{
					FixedWidth? fixedWidth = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidth.CreateSafe(this._builders, node);
					if (fixedWidth == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FixedWidth but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fixedWidth.Value;
				}

				// Token: 0x06009544 RID: 38212 RVA: 0x001FEDB8 File Offset: 0x001FCFB8
				public FixedWidthDelimiters FixedWidthDelimiters(ProgramNode node)
				{
					FixedWidthDelimiters? fixedWidthDelimiters = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidthDelimiters.CreateSafe(this._builders, node);
					if (fixedWidthDelimiters == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FixedWidthDelimiters but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fixedWidthDelimiters.Value;
				}

				// Token: 0x06009545 RID: 38213 RVA: 0x001FEE0C File Offset: 0x001FD00C
				public GEN_Concat GEN_Concat(ProgramNode node)
				{
					GEN_Concat? gen_Concat = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_Concat.CreateSafe(this._builders, node);
					if (gen_Concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_Concat.Value;
				}

				// Token: 0x06009546 RID: 38214 RVA: 0x001FEE60 File Offset: 0x001FD060
				public GEN_LookAround GEN_LookAround(ProgramNode node)
				{
					GEN_LookAround? gen_LookAround = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_LookAround.CreateSafe(this._builders, node);
					if (gen_LookAround == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_LookAround but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_LookAround.Value;
				}

				// Token: 0x06009547 RID: 38215 RVA: 0x001FEEB4 File Offset: 0x001FD0B4
				public GEN_FieldLookAroundEndPoints GEN_FieldLookAroundEndPoints(ProgramNode node)
				{
					GEN_FieldLookAroundEndPoints? gen_FieldLookAroundEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_FieldLookAroundEndPoints.CreateSafe(this._builders, node);
					if (gen_FieldLookAroundEndPoints == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_FieldLookAroundEndPoints but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_FieldLookAroundEndPoints.Value;
				}

				// Token: 0x06009548 RID: 38216 RVA: 0x001FEF08 File Offset: 0x001FD108
				public Item2 Item2(ProgramNode node)
				{
					Item2? item = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item2.CreateSafe(this._builders, node);
					if (item == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Item2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return item.Value;
				}

				// Token: 0x06009549 RID: 38217 RVA: 0x001FEF5C File Offset: 0x001FD15C
				public Append Append(ProgramNode node)
				{
					Append? append = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
					if (append == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Append but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return append.Value;
				}

				// Token: 0x0600954A RID: 38218 RVA: 0x001FEFB0 File Offset: 0x001FD1B0
				public Split Split(ProgramNode node)
				{
					Split? split = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return split.Value;
				}

				// Token: 0x0600954B RID: 38219 RVA: 0x001FF004 File Offset: 0x001FD204
				public InnerLetWitness InnerLetWitness(ProgramNode node)
				{
					InnerLetWitness? innerLetWitness = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.InnerLetWitness.CreateSafe(this._builders, node);
					if (innerLetWitness == null)
					{
						string text = "node";
						string text2 = "expected node for symbol InnerLetWitness but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return innerLetWitness.Value;
				}

				// Token: 0x0600954C RID: 38220 RVA: 0x001FF058 File Offset: 0x001FD258
				public List List(ProgramNode node)
				{
					List? list = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
					if (list == null)
					{
						string text = "node";
						string text2 = "expected node for symbol List but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return list.Value;
				}

				// Token: 0x0600954D RID: 38221 RVA: 0x001FF0AC File Offset: 0x001FD2AC
				public OuterLetWitness OuterLetWitness(ProgramNode node)
				{
					OuterLetWitness? outerLetWitness = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.OuterLetWitness.CreateSafe(this._builders, node);
					if (outerLetWitness == null)
					{
						string text = "node";
						string text2 = "expected node for symbol OuterLetWitness but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outerLetWitness.Value;
				}

				// Token: 0x0600954E RID: 38222 RVA: 0x001FF100 File Offset: 0x001FD300
				public Item1 Item1(ProgramNode node)
				{
					Item1? item = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item1.CreateSafe(this._builders, node);
					if (item == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Item1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return item.Value;
				}

				// Token: 0x04003D38 RID: 15672
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001328 RID: 4904
			public class NodeIs
			{
				// Token: 0x0600954F RID: 38223 RVA: 0x001FF151 File Offset: 0x001FD351
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009550 RID: 38224 RVA: 0x001FF160 File Offset: 0x001FD360
				public bool regionSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009551 RID: 38225 RVA: 0x001FF184 File Offset: 0x001FD384
				public bool regionSplit(ProgramNode node, out regionSplit value)
				{
					regionSplit? regionSplit = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit.CreateSafe(this._builders, node);
					if (regionSplit == null)
					{
						value = default(regionSplit);
						return false;
					}
					value = regionSplit.Value;
					return true;
				}

				// Token: 0x06009552 RID: 38226 RVA: 0x001FF1C0 File Offset: 0x001FD3C0
				public bool splitMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009553 RID: 38227 RVA: 0x001FF1E4 File Offset: 0x001FD3E4
				public bool splitMatches(ProgramNode node, out splitMatches value)
				{
					splitMatches? splitMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches.CreateSafe(this._builders, node);
					if (splitMatches == null)
					{
						value = default(splitMatches);
						return false;
					}
					value = splitMatches.Value;
					return true;
				}

				// Token: 0x06009554 RID: 38228 RVA: 0x001FF220 File Offset: 0x001FD420
				public bool multipleMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009555 RID: 38229 RVA: 0x001FF244 File Offset: 0x001FD444
				public bool multipleMatches(ProgramNode node, out multipleMatches value)
				{
					multipleMatches? multipleMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches.CreateSafe(this._builders, node);
					if (multipleMatches == null)
					{
						value = default(multipleMatches);
						return false;
					}
					value = multipleMatches.Value;
					return true;
				}

				// Token: 0x06009556 RID: 38230 RVA: 0x001FF280 File Offset: 0x001FD480
				public bool delimiterList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009557 RID: 38231 RVA: 0x001FF2A4 File Offset: 0x001FD4A4
				public bool delimiterList(ProgramNode node, out delimiterList value)
				{
					delimiterList? delimiterList = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList.CreateSafe(this._builders, node);
					if (delimiterList == null)
					{
						value = default(delimiterList);
						return false;
					}
					value = delimiterList.Value;
					return true;
				}

				// Token: 0x06009558 RID: 38232 RVA: 0x001FF2E0 File Offset: 0x001FD4E0
				public bool extractionPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009559 RID: 38233 RVA: 0x001FF304 File Offset: 0x001FD504
				public bool extractionPoints(ProgramNode node, out extractionPoints value)
				{
					extractionPoints? extractionPoints = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints.CreateSafe(this._builders, node);
					if (extractionPoints == null)
					{
						value = default(extractionPoints);
						return false;
					}
					value = extractionPoints.Value;
					return true;
				}

				// Token: 0x0600955A RID: 38234 RVA: 0x001FF340 File Offset: 0x001FD540
				public bool cndExtPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600955B RID: 38235 RVA: 0x001FF364 File Offset: 0x001FD564
				public bool cndExtPoint(ProgramNode node, out cndExtPoint value)
				{
					cndExtPoint? cndExtPoint = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint.CreateSafe(this._builders, node);
					if (cndExtPoint == null)
					{
						value = default(cndExtPoint);
						return false;
					}
					value = cndExtPoint.Value;
					return true;
				}

				// Token: 0x0600955C RID: 38236 RVA: 0x001FF3A0 File Offset: 0x001FD5A0
				public bool extPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600955D RID: 38237 RVA: 0x001FF3C4 File Offset: 0x001FD5C4
				public bool extPoint(ProgramNode node, out extPoint value)
				{
					extPoint? extPoint = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint.CreateSafe(this._builders, node);
					if (extPoint == null)
					{
						value = default(extPoint);
						return false;
					}
					value = extPoint.Value;
					return true;
				}

				// Token: 0x0600955E RID: 38238 RVA: 0x001FF400 File Offset: 0x001FD600
				public bool pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600955F RID: 38239 RVA: 0x001FF424 File Offset: 0x001FD624
				public bool pred(ProgramNode node, out pred value)
				{
					pred? pred = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						value = default(pred);
						return false;
					}
					value = pred.Value;
					return true;
				}

				// Token: 0x06009560 RID: 38240 RVA: 0x001FF460 File Offset: 0x001FD660
				public bool pattern(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009561 RID: 38241 RVA: 0x001FF484 File Offset: 0x001FD684
				public bool pattern(ProgramNode node, out pattern value)
				{
					pattern? pattern = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern.CreateSafe(this._builders, node);
					if (pattern == null)
					{
						value = default(pattern);
						return false;
					}
					value = pattern.Value;
					return true;
				}

				// Token: 0x06009562 RID: 38242 RVA: 0x001FF4C0 File Offset: 0x001FD6C0
				public bool d(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009563 RID: 38243 RVA: 0x001FF4E4 File Offset: 0x001FD6E4
				public bool d(ProgramNode node, out d value)
				{
					d? d = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d.CreateSafe(this._builders, node);
					if (d == null)
					{
						value = default(d);
						return false;
					}
					value = d.Value;
					return true;
				}

				// Token: 0x06009564 RID: 38244 RVA: 0x001FF520 File Offset: 0x001FD720
				public bool c(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009565 RID: 38245 RVA: 0x001FF544 File Offset: 0x001FD744
				public bool c(ProgramNode node, out c value)
				{
					c? c = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c.CreateSafe(this._builders, node);
					if (c == null)
					{
						value = default(c);
						return false;
					}
					value = c.Value;
					return true;
				}

				// Token: 0x06009566 RID: 38246 RVA: 0x001FF580 File Offset: 0x001FD780
				public bool quotingConf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009567 RID: 38247 RVA: 0x001FF5A4 File Offset: 0x001FD7A4
				public bool quotingConf(ProgramNode node, out quotingConf value)
				{
					quotingConf? quotingConf = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf.CreateSafe(this._builders, node);
					if (quotingConf == null)
					{
						value = default(quotingConf);
						return false;
					}
					value = quotingConf.Value;
					return true;
				}

				// Token: 0x06009568 RID: 38248 RVA: 0x001FF5E0 File Offset: 0x001FD7E0
				public bool constantDelimiterMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009569 RID: 38249 RVA: 0x001FF604 File Offset: 0x001FD804
				public bool constantDelimiterMatches(ProgramNode node, out constantDelimiterMatches value)
				{
					constantDelimiterMatches? constantDelimiterMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches.CreateSafe(this._builders, node);
					if (constantDelimiterMatches == null)
					{
						value = default(constantDelimiterMatches);
						return false;
					}
					value = constantDelimiterMatches.Value;
					return true;
				}

				// Token: 0x0600956A RID: 38250 RVA: 0x001FF640 File Offset: 0x001FD840
				public bool r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600956B RID: 38251 RVA: 0x001FF664 File Offset: 0x001FD864
				public bool r(ProgramNode node, out r value)
				{
					r? r = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						value = default(r);
						return false;
					}
					value = r.Value;
					return true;
				}

				// Token: 0x0600956C RID: 38252 RVA: 0x001FF6A0 File Offset: 0x001FD8A0
				public bool regexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600956D RID: 38253 RVA: 0x001FF6C4 File Offset: 0x001FD8C4
				public bool regexMatch(ProgramNode node, out regexMatch value)
				{
					regexMatch? regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch.CreateSafe(this._builders, node);
					if (regexMatch == null)
					{
						value = default(regexMatch);
						return false;
					}
					value = regexMatch.Value;
					return true;
				}

				// Token: 0x0600956E RID: 38254 RVA: 0x001FF700 File Offset: 0x001FD900
				public bool fieldMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600956F RID: 38255 RVA: 0x001FF724 File Offset: 0x001FD924
				public bool fieldMatch(ProgramNode node, out fieldMatch value)
				{
					fieldMatch? fieldMatch = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch.CreateSafe(this._builders, node);
					if (fieldMatch == null)
					{
						value = default(fieldMatch);
						return false;
					}
					value = fieldMatch.Value;
					return true;
				}

				// Token: 0x06009570 RID: 38256 RVA: 0x001FF760 File Offset: 0x001FD960
				public bool fixedWidthMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009571 RID: 38257 RVA: 0x001FF784 File Offset: 0x001FD984
				public bool fixedWidthMatches(ProgramNode node, out fixedWidthMatches value)
				{
					fixedWidthMatches? fixedWidthMatches = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches.CreateSafe(this._builders, node);
					if (fixedWidthMatches == null)
					{
						value = default(fixedWidthMatches);
						return false;
					}
					value = fixedWidthMatches.Value;
					return true;
				}

				// Token: 0x06009572 RID: 38258 RVA: 0x001FF7C0 File Offset: 0x001FD9C0
				public bool gen_Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009573 RID: 38259 RVA: 0x001FF7E4 File Offset: 0x001FD9E4
				public bool gen_Concat(ProgramNode node, out gen_Concat value)
				{
					gen_Concat? gen_Concat = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat.CreateSafe(this._builders, node);
					if (gen_Concat == null)
					{
						value = default(gen_Concat);
						return false;
					}
					value = gen_Concat.Value;
					return true;
				}

				// Token: 0x06009574 RID: 38260 RVA: 0x001FF820 File Offset: 0x001FDA20
				public bool gen_LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009575 RID: 38261 RVA: 0x001FF844 File Offset: 0x001FDA44
				public bool gen_LookAround(ProgramNode node, out gen_LookAround value)
				{
					gen_LookAround? gen_LookAround = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround.CreateSafe(this._builders, node);
					if (gen_LookAround == null)
					{
						value = default(gen_LookAround);
						return false;
					}
					value = gen_LookAround.Value;
					return true;
				}

				// Token: 0x06009576 RID: 38262 RVA: 0x001FF880 File Offset: 0x001FDA80
				public bool gen_LookAroundField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009577 RID: 38263 RVA: 0x001FF8A4 File Offset: 0x001FDAA4
				public bool gen_LookAroundField(ProgramNode node, out gen_LookAroundField value)
				{
					gen_LookAroundField? gen_LookAroundField = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField.CreateSafe(this._builders, node);
					if (gen_LookAroundField == null)
					{
						value = default(gen_LookAroundField);
						return false;
					}
					value = gen_LookAroundField.Value;
					return true;
				}

				// Token: 0x06009578 RID: 38264 RVA: 0x001FF8E0 File Offset: 0x001FDAE0
				public bool delimiterStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009579 RID: 38265 RVA: 0x001FF904 File Offset: 0x001FDB04
				public bool delimiterStart(ProgramNode node, out delimiterStart value)
				{
					delimiterStart? delimiterStart = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart.CreateSafe(this._builders, node);
					if (delimiterStart == null)
					{
						value = default(delimiterStart);
						return false;
					}
					value = delimiterStart.Value;
					return true;
				}

				// Token: 0x0600957A RID: 38266 RVA: 0x001FF940 File Offset: 0x001FDB40
				public bool delimiterEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600957B RID: 38267 RVA: 0x001FF964 File Offset: 0x001FDB64
				public bool delimiterEnd(ProgramNode node, out delimiterEnd value)
				{
					delimiterEnd? delimiterEnd = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd.CreateSafe(this._builders, node);
					if (delimiterEnd == null)
					{
						value = default(delimiterEnd);
						return false;
					}
					value = delimiterEnd.Value;
					return true;
				}

				// Token: 0x0600957C RID: 38268 RVA: 0x001FF9A0 File Offset: 0x001FDBA0
				public bool includeDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600957D RID: 38269 RVA: 0x001FF9C4 File Offset: 0x001FDBC4
				public bool includeDelimiters(ProgramNode node, out includeDelimiters value)
				{
					includeDelimiters? includeDelimiters = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters.CreateSafe(this._builders, node);
					if (includeDelimiters == null)
					{
						value = default(includeDelimiters);
						return false;
					}
					value = includeDelimiters.Value;
					return true;
				}

				// Token: 0x0600957E RID: 38270 RVA: 0x001FFA00 File Offset: 0x001FDC00
				public bool fillStrategy(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600957F RID: 38271 RVA: 0x001FFA24 File Offset: 0x001FDC24
				public bool fillStrategy(ProgramNode node, out fillStrategy value)
				{
					fillStrategy? fillStrategy = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy.CreateSafe(this._builders, node);
					if (fillStrategy == null)
					{
						value = default(fillStrategy);
						return false;
					}
					value = fillStrategy.Value;
					return true;
				}

				// Token: 0x06009580 RID: 38272 RVA: 0x001FFA60 File Offset: 0x001FDC60
				public bool ignoreIndexes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009581 RID: 38273 RVA: 0x001FFA84 File Offset: 0x001FDC84
				public bool ignoreIndexes(ProgramNode node, out ignoreIndexes value)
				{
					ignoreIndexes? ignoreIndexes = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes.CreateSafe(this._builders, node);
					if (ignoreIndexes == null)
					{
						value = default(ignoreIndexes);
						return false;
					}
					value = ignoreIndexes.Value;
					return true;
				}

				// Token: 0x06009582 RID: 38274 RVA: 0x001FFAC0 File Offset: 0x001FDCC0
				public bool fieldStartPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009583 RID: 38275 RVA: 0x001FFAE4 File Offset: 0x001FDCE4
				public bool fieldStartPositions(ProgramNode node, out fieldStartPositions value)
				{
					fieldStartPositions? fieldStartPositions = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions.CreateSafe(this._builders, node);
					if (fieldStartPositions == null)
					{
						value = default(fieldStartPositions);
						return false;
					}
					value = fieldStartPositions.Value;
					return true;
				}

				// Token: 0x06009584 RID: 38276 RVA: 0x001FFB20 File Offset: 0x001FDD20
				public bool delimiterPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009585 RID: 38277 RVA: 0x001FFB44 File Offset: 0x001FDD44
				public bool delimiterPositions(ProgramNode node, out delimiterPositions value)
				{
					delimiterPositions? delimiterPositions = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions.CreateSafe(this._builders, node);
					if (delimiterPositions == null)
					{
						value = default(delimiterPositions);
						return false;
					}
					value = delimiterPositions.Value;
					return true;
				}

				// Token: 0x06009586 RID: 38278 RVA: 0x001FFB80 File Offset: 0x001FDD80
				public bool fregex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009587 RID: 38279 RVA: 0x001FFBA4 File Offset: 0x001FDDA4
				public bool fregex(ProgramNode node, out fregex value)
				{
					fregex? fregex = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex.CreateSafe(this._builders, node);
					if (fregex == null)
					{
						value = default(fregex);
						return false;
					}
					value = fregex.Value;
					return true;
				}

				// Token: 0x06009588 RID: 38280 RVA: 0x001FFBE0 File Offset: 0x001FDDE0
				public bool s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009589 RID: 38281 RVA: 0x001FFC04 File Offset: 0x001FDE04
				public bool s(ProgramNode node, out s value)
				{
					s? s = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						value = default(s);
						return false;
					}
					value = s.Value;
					return true;
				}

				// Token: 0x0600958A RID: 38282 RVA: 0x001FFC40 File Offset: 0x001FDE40
				public bool a(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600958B RID: 38283 RVA: 0x001FFC64 File Offset: 0x001FDE64
				public bool a(ProgramNode node, out a value)
				{
					a? a = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a.CreateSafe(this._builders, node);
					if (a == null)
					{
						value = default(a);
						return false;
					}
					value = a.Value;
					return true;
				}

				// Token: 0x0600958C RID: 38284 RVA: 0x001FFCA0 File Offset: 0x001FDEA0
				public bool numSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600958D RID: 38285 RVA: 0x001FFCC4 File Offset: 0x001FDEC4
				public bool numSplits(ProgramNode node, out numSplits value)
				{
					numSplits? numSplits = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits.CreateSafe(this._builders, node);
					if (numSplits == null)
					{
						value = default(numSplits);
						return false;
					}
					value = numSplits.Value;
					return true;
				}

				// Token: 0x0600958E RID: 38286 RVA: 0x001FFD00 File Offset: 0x001FDF00
				public bool regex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600958F RID: 38287 RVA: 0x001FFD24 File Offset: 0x001FDF24
				public bool regex(ProgramNode node, out regex value)
				{
					regex? regex = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex.CreateSafe(this._builders, node);
					if (regex == null)
					{
						value = default(regex);
						return false;
					}
					value = regex.Value;
					return true;
				}

				// Token: 0x06009590 RID: 38288 RVA: 0x001FFD60 File Offset: 0x001FDF60
				public bool obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009591 RID: 38289 RVA: 0x001FFD84 File Offset: 0x001FDF84
				public bool obj(ProgramNode node, out obj value)
				{
					obj? obj = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj.CreateSafe(this._builders, node);
					if (obj == null)
					{
						value = default(obj);
						return false;
					}
					value = obj.Value;
					return true;
				}

				// Token: 0x06009592 RID: 38290 RVA: 0x001FFDC0 File Offset: 0x001FDFC0
				public bool delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009593 RID: 38291 RVA: 0x001FFDE4 File Offset: 0x001FDFE4
				public bool delimiter(ProgramNode node, out delimiter value)
				{
					delimiter? delimiter = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						value = default(delimiter);
						return false;
					}
					value = delimiter.Value;
					return true;
				}

				// Token: 0x06009594 RID: 38292 RVA: 0x001FFE20 File Offset: 0x001FE020
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009595 RID: 38293 RVA: 0x001FFE44 File Offset: 0x001FE044
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x06009596 RID: 38294 RVA: 0x001FFE80 File Offset: 0x001FE080
				public bool pair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009597 RID: 38295 RVA: 0x001FFEA4 File Offset: 0x001FE0A4
				public bool pair(ProgramNode node, out pair value)
				{
					pair? pair = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair.CreateSafe(this._builders, node);
					if (pair == null)
					{
						value = default(pair);
						return false;
					}
					value = pair.Value;
					return true;
				}

				// Token: 0x06009598 RID: 38296 RVA: 0x001FFEE0 File Offset: 0x001FE0E0
				public bool item1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06009599 RID: 38297 RVA: 0x001FFF04 File Offset: 0x001FE104
				public bool item1(ProgramNode node, out item1 value)
				{
					item1? item = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1.CreateSafe(this._builders, node);
					if (item == null)
					{
						value = default(item1);
						return false;
					}
					value = item.Value;
					return true;
				}

				// Token: 0x0600959A RID: 38298 RVA: 0x001FFF40 File Offset: 0x001FE140
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600959B RID: 38299 RVA: 0x001FFF64 File Offset: 0x001FE164
				public bool _LetB0(ProgramNode node, out _LetB0 value)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600959C RID: 38300 RVA: 0x001FFFA0 File Offset: 0x001FE1A0
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600959D RID: 38301 RVA: 0x001FFFC4 File Offset: 0x001FE1C4
				public bool _LetB1(ProgramNode node, out _LetB1 value)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600959E RID: 38302 RVA: 0x00200000 File Offset: 0x001FE200
				public bool _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600959F RID: 38303 RVA: 0x00200024 File Offset: 0x001FE224
				public bool _LetB2(ProgramNode node, out _LetB2 value)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB2);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x060095A0 RID: 38304 RVA: 0x00200060 File Offset: 0x001FE260
				public bool _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095A1 RID: 38305 RVA: 0x00200084 File Offset: 0x001FE284
				public bool _LetB3(ProgramNode node, out _LetB3 value)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB3);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x04003D39 RID: 15673
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001329 RID: 4905
			public class RuleIs
			{
				// Token: 0x060095A2 RID: 38306 RVA: 0x002000BE File Offset: 0x001FE2BE
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060095A3 RID: 38307 RVA: 0x002000D0 File Offset: 0x001FE2D0
				public bool ExtractionSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtractionSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095A4 RID: 38308 RVA: 0x002000F4 File Offset: 0x001FE2F4
				public bool ExtractionSplit(ProgramNode node, out ExtractionSplit value)
				{
					ExtractionSplit? extractionSplit = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtractionSplit.CreateSafe(this._builders, node);
					if (extractionSplit == null)
					{
						value = default(ExtractionSplit);
						return false;
					}
					value = extractionSplit.Value;
					return true;
				}

				// Token: 0x060095A5 RID: 38309 RVA: 0x00200130 File Offset: 0x001FE330
				public bool SplitRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095A6 RID: 38310 RVA: 0x00200154 File Offset: 0x001FE354
				public bool SplitRegion(ProgramNode node, out SplitRegion value)
				{
					SplitRegion? splitRegion = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitRegion.CreateSafe(this._builders, node);
					if (splitRegion == null)
					{
						value = default(SplitRegion);
						return false;
					}
					value = splitRegion.Value;
					return true;
				}

				// Token: 0x060095A7 RID: 38311 RVA: 0x00200190 File Offset: 0x001FE390
				public bool splitMatches_multipleMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_multipleMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095A8 RID: 38312 RVA: 0x002001B4 File Offset: 0x001FE3B4
				public bool splitMatches_multipleMatches(ProgramNode node, out splitMatches_multipleMatches value)
				{
					splitMatches_multipleMatches? splitMatches_multipleMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_multipleMatches.CreateSafe(this._builders, node);
					if (splitMatches_multipleMatches == null)
					{
						value = default(splitMatches_multipleMatches);
						return false;
					}
					value = splitMatches_multipleMatches.Value;
					return true;
				}

				// Token: 0x060095A9 RID: 38313 RVA: 0x002001F0 File Offset: 0x001FE3F0
				public bool splitMatches_constantDelimiterMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_constantDelimiterMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095AA RID: 38314 RVA: 0x00200214 File Offset: 0x001FE414
				public bool splitMatches_constantDelimiterMatches(ProgramNode node, out splitMatches_constantDelimiterMatches value)
				{
					splitMatches_constantDelimiterMatches? splitMatches_constantDelimiterMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_constantDelimiterMatches.CreateSafe(this._builders, node);
					if (splitMatches_constantDelimiterMatches == null)
					{
						value = default(splitMatches_constantDelimiterMatches);
						return false;
					}
					value = splitMatches_constantDelimiterMatches.Value;
					return true;
				}

				// Token: 0x060095AB RID: 38315 RVA: 0x00200250 File Offset: 0x001FE450
				public bool splitMatches_fixedWidthMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_fixedWidthMatches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095AC RID: 38316 RVA: 0x00200274 File Offset: 0x001FE474
				public bool splitMatches_fixedWidthMatches(ProgramNode node, out splitMatches_fixedWidthMatches value)
				{
					splitMatches_fixedWidthMatches? splitMatches_fixedWidthMatches = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_fixedWidthMatches.CreateSafe(this._builders, node);
					if (splitMatches_fixedWidthMatches == null)
					{
						value = default(splitMatches_fixedWidthMatches);
						return false;
					}
					value = splitMatches_fixedWidthMatches.Value;
					return true;
				}

				// Token: 0x060095AD RID: 38317 RVA: 0x002002B0 File Offset: 0x001FE4B0
				public bool SplitMultiple(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitMultiple.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095AE RID: 38318 RVA: 0x002002D4 File Offset: 0x001FE4D4
				public bool SplitMultiple(ProgramNode node, out SplitMultiple value)
				{
					SplitMultiple? splitMultiple = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitMultiple.CreateSafe(this._builders, node);
					if (splitMultiple == null)
					{
						value = default(SplitMultiple);
						return false;
					}
					value = splitMultiple.Value;
					return true;
				}

				// Token: 0x060095AF RID: 38319 RVA: 0x00200310 File Offset: 0x001FE510
				public bool multipleMatches_d(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.multipleMatches_d.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095B0 RID: 38320 RVA: 0x00200334 File Offset: 0x001FE534
				public bool multipleMatches_d(ProgramNode node, out multipleMatches_d value)
				{
					multipleMatches_d? multipleMatches_d = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.multipleMatches_d.CreateSafe(this._builders, node);
					if (multipleMatches_d == null)
					{
						value = default(multipleMatches_d);
						return false;
					}
					value = multipleMatches_d.Value;
					return true;
				}

				// Token: 0x060095B1 RID: 38321 RVA: 0x00200370 File Offset: 0x001FE570
				public bool DelimitersList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.DelimitersList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095B2 RID: 38322 RVA: 0x00200394 File Offset: 0x001FE594
				public bool DelimitersList(ProgramNode node, out DelimitersList value)
				{
					DelimitersList? delimitersList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.DelimitersList.CreateSafe(this._builders, node);
					if (delimitersList == null)
					{
						value = default(DelimitersList);
						return false;
					}
					value = delimitersList.Value;
					return true;
				}

				// Token: 0x060095B3 RID: 38323 RVA: 0x002003D0 File Offset: 0x001FE5D0
				public bool EmptyDelimitersList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyDelimitersList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095B4 RID: 38324 RVA: 0x002003F4 File Offset: 0x001FE5F4
				public bool EmptyDelimitersList(ProgramNode node, out EmptyDelimitersList value)
				{
					EmptyDelimitersList? emptyDelimitersList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyDelimitersList.CreateSafe(this._builders, node);
					if (emptyDelimitersList == null)
					{
						value = default(EmptyDelimitersList);
						return false;
					}
					value = emptyDelimitersList.Value;
					return true;
				}

				// Token: 0x060095B5 RID: 38325 RVA: 0x00200430 File Offset: 0x001FE630
				public bool ExtPointsList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtPointsList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095B6 RID: 38326 RVA: 0x00200454 File Offset: 0x001FE654
				public bool ExtPointsList(ProgramNode node, out ExtPointsList value)
				{
					ExtPointsList? extPointsList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtPointsList.CreateSafe(this._builders, node);
					if (extPointsList == null)
					{
						value = default(ExtPointsList);
						return false;
					}
					value = extPointsList.Value;
					return true;
				}

				// Token: 0x060095B7 RID: 38327 RVA: 0x00200490 File Offset: 0x001FE690
				public bool EmptyExtPointsList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyExtPointsList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095B8 RID: 38328 RVA: 0x002004B4 File Offset: 0x001FE6B4
				public bool EmptyExtPointsList(ProgramNode node, out EmptyExtPointsList value)
				{
					EmptyExtPointsList? emptyExtPointsList = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyExtPointsList.CreateSafe(this._builders, node);
					if (emptyExtPointsList == null)
					{
						value = default(EmptyExtPointsList);
						return false;
					}
					value = emptyExtPointsList.Value;
					return true;
				}

				// Token: 0x060095B9 RID: 38329 RVA: 0x002004F0 File Offset: 0x001FE6F0
				public bool cndExtPoint_extPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.cndExtPoint_extPoint.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095BA RID: 38330 RVA: 0x00200514 File Offset: 0x001FE714
				public bool cndExtPoint_extPoint(ProgramNode node, out cndExtPoint_extPoint value)
				{
					cndExtPoint_extPoint? cndExtPoint_extPoint = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.cndExtPoint_extPoint.CreateSafe(this._builders, node);
					if (cndExtPoint_extPoint == null)
					{
						value = default(cndExtPoint_extPoint);
						return false;
					}
					value = cndExtPoint_extPoint.Value;
					return true;
				}

				// Token: 0x060095BB RID: 38331 RVA: 0x00200550 File Offset: 0x001FE750
				public bool ConditionalExtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConditionalExtract.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095BC RID: 38332 RVA: 0x00200574 File Offset: 0x001FE774
				public bool ConditionalExtract(ProgramNode node, out ConditionalExtract value)
				{
					ConditionalExtract? conditionalExtract = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConditionalExtract.CreateSafe(this._builders, node);
					if (conditionalExtract == null)
					{
						value = default(ConditionalExtract);
						return false;
					}
					value = conditionalExtract.Value;
					return true;
				}

				// Token: 0x060095BD RID: 38333 RVA: 0x002005B0 File Offset: 0x001FE7B0
				public bool SpecialCharPattern(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SpecialCharPattern.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095BE RID: 38334 RVA: 0x002005D4 File Offset: 0x001FE7D4
				public bool SpecialCharPattern(ProgramNode node, out SpecialCharPattern value)
				{
					SpecialCharPattern? specialCharPattern = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SpecialCharPattern.CreateSafe(this._builders, node);
					if (specialCharPattern == null)
					{
						value = default(SpecialCharPattern);
						return false;
					}
					value = specialCharPattern.Value;
					return true;
				}

				// Token: 0x060095BF RID: 38335 RVA: 0x00200610 File Offset: 0x001FE810
				public bool LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.LookAround.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095C0 RID: 38336 RVA: 0x00200634 File Offset: 0x001FE834
				public bool LookAround(ProgramNode node, out LookAround value)
				{
					LookAround? lookAround = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.LookAround.CreateSafe(this._builders, node);
					if (lookAround == null)
					{
						value = default(LookAround);
						return false;
					}
					value = lookAround.Value;
					return true;
				}

				// Token: 0x060095C1 RID: 38337 RVA: 0x00200670 File Offset: 0x001FE870
				public bool FieldEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldEndPoints.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095C2 RID: 38338 RVA: 0x00200694 File Offset: 0x001FE894
				public bool FieldEndPoints(ProgramNode node, out FieldEndPoints value)
				{
					FieldEndPoints? fieldEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldEndPoints.CreateSafe(this._builders, node);
					if (fieldEndPoints == null)
					{
						value = default(FieldEndPoints);
						return false;
					}
					value = fieldEndPoints.Value;
					return true;
				}

				// Token: 0x060095C3 RID: 38339 RVA: 0x002006D0 File Offset: 0x001FE8D0
				public bool FieldLookAroundEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldLookAroundEndPoints.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095C4 RID: 38340 RVA: 0x002006F4 File Offset: 0x001FE8F4
				public bool FieldLookAroundEndPoints(ProgramNode node, out FieldLookAroundEndPoints value)
				{
					FieldLookAroundEndPoints? fieldLookAroundEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldLookAroundEndPoints.CreateSafe(this._builders, node);
					if (fieldLookAroundEndPoints == null)
					{
						value = default(FieldLookAroundEndPoints);
						return false;
					}
					value = fieldLookAroundEndPoints.Value;
					return true;
				}

				// Token: 0x060095C5 RID: 38341 RVA: 0x00200730 File Offset: 0x001FE930
				public bool ConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095C6 RID: 38342 RVA: 0x00200754 File Offset: 0x001FE954
				public bool ConstStr(ProgramNode node, out ConstStr value)
				{
					ConstStr? constStr = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						value = default(ConstStr);
						return false;
					}
					value = constStr.Value;
					return true;
				}

				// Token: 0x060095C7 RID: 38343 RVA: 0x00200790 File Offset: 0x001FE990
				public bool ConstStrWithWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStrWithWhitespace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095C8 RID: 38344 RVA: 0x002007B4 File Offset: 0x001FE9B4
				public bool ConstStrWithWhitespace(ProgramNode node, out ConstStrWithWhitespace value)
				{
					ConstStrWithWhitespace? constStrWithWhitespace = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStrWithWhitespace.CreateSafe(this._builders, node);
					if (constStrWithWhitespace == null)
					{
						value = default(ConstStrWithWhitespace);
						return false;
					}
					value = constStrWithWhitespace.Value;
					return true;
				}

				// Token: 0x060095C9 RID: 38345 RVA: 0x002007F0 File Offset: 0x001FE9F0
				public bool ConstAlphStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstAlphStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095CA RID: 38346 RVA: 0x00200814 File Offset: 0x001FEA14
				public bool ConstAlphStr(ProgramNode node, out ConstAlphStr value)
				{
					ConstAlphStr? constAlphStr = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstAlphStr.CreateSafe(this._builders, node);
					if (constAlphStr == null)
					{
						value = default(ConstAlphStr);
						return false;
					}
					value = constAlphStr.Value;
					return true;
				}

				// Token: 0x060095CB RID: 38347 RVA: 0x00200850 File Offset: 0x001FEA50
				public bool ConstantDelimiterWithQuoting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiterWithQuoting.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095CC RID: 38348 RVA: 0x00200874 File Offset: 0x001FEA74
				public bool ConstantDelimiterWithQuoting(ProgramNode node, out ConstantDelimiterWithQuoting value)
				{
					ConstantDelimiterWithQuoting? constantDelimiterWithQuoting = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiterWithQuoting.CreateSafe(this._builders, node);
					if (constantDelimiterWithQuoting == null)
					{
						value = default(ConstantDelimiterWithQuoting);
						return false;
					}
					value = constantDelimiterWithQuoting.Value;
					return true;
				}

				// Token: 0x060095CD RID: 38349 RVA: 0x002008B0 File Offset: 0x001FEAB0
				public bool ConstantDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095CE RID: 38350 RVA: 0x002008D4 File Offset: 0x001FEAD4
				public bool ConstantDelimiter(ProgramNode node, out ConstantDelimiter value)
				{
					ConstantDelimiter? constantDelimiter = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiter.CreateSafe(this._builders, node);
					if (constantDelimiter == null)
					{
						value = default(ConstantDelimiter);
						return false;
					}
					value = constantDelimiter.Value;
					return true;
				}

				// Token: 0x060095CF RID: 38351 RVA: 0x00200910 File Offset: 0x001FEB10
				public bool Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095D0 RID: 38352 RVA: 0x00200934 File Offset: 0x001FEB34
				public bool Empty(ProgramNode node, out Empty value)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						value = default(Empty);
						return false;
					}
					value = empty.Value;
					return true;
				}

				// Token: 0x060095D1 RID: 38353 RVA: 0x00200970 File Offset: 0x001FEB70
				public bool r_regexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.r_regexMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095D2 RID: 38354 RVA: 0x00200994 File Offset: 0x001FEB94
				public bool r_regexMatch(ProgramNode node, out r_regexMatch value)
				{
					r_regexMatch? r_regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.r_regexMatch.CreateSafe(this._builders, node);
					if (r_regexMatch == null)
					{
						value = default(r_regexMatch);
						return false;
					}
					value = r_regexMatch.Value;
					return true;
				}

				// Token: 0x060095D3 RID: 38355 RVA: 0x002009D0 File Offset: 0x001FEBD0
				public bool Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095D4 RID: 38356 RVA: 0x002009F4 File Offset: 0x001FEBF4
				public bool Concat(ProgramNode node, out Concat value)
				{
					Concat? concat = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						value = default(Concat);
						return false;
					}
					value = concat.Value;
					return true;
				}

				// Token: 0x060095D5 RID: 38357 RVA: 0x00200A30 File Offset: 0x001FEC30
				public bool RegexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.RegexMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095D6 RID: 38358 RVA: 0x00200A54 File Offset: 0x001FEC54
				public bool RegexMatch(ProgramNode node, out RegexMatch value)
				{
					RegexMatch? regexMatch = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.RegexMatch.CreateSafe(this._builders, node);
					if (regexMatch == null)
					{
						value = default(RegexMatch);
						return false;
					}
					value = regexMatch.Value;
					return true;
				}

				// Token: 0x060095D7 RID: 38359 RVA: 0x00200A90 File Offset: 0x001FEC90
				public bool FieldMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095D8 RID: 38360 RVA: 0x00200AB4 File Offset: 0x001FECB4
				public bool FieldMatch(ProgramNode node, out FieldMatch value)
				{
					FieldMatch? fieldMatch = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldMatch.CreateSafe(this._builders, node);
					if (fieldMatch == null)
					{
						value = default(FieldMatch);
						return false;
					}
					value = fieldMatch.Value;
					return true;
				}

				// Token: 0x060095D9 RID: 38361 RVA: 0x00200AF0 File Offset: 0x001FECF0
				public bool FixedWidth(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidth.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095DA RID: 38362 RVA: 0x00200B14 File Offset: 0x001FED14
				public bool FixedWidth(ProgramNode node, out FixedWidth value)
				{
					FixedWidth? fixedWidth = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidth.CreateSafe(this._builders, node);
					if (fixedWidth == null)
					{
						value = default(FixedWidth);
						return false;
					}
					value = fixedWidth.Value;
					return true;
				}

				// Token: 0x060095DB RID: 38363 RVA: 0x00200B50 File Offset: 0x001FED50
				public bool FixedWidthDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidthDelimiters.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095DC RID: 38364 RVA: 0x00200B74 File Offset: 0x001FED74
				public bool FixedWidthDelimiters(ProgramNode node, out FixedWidthDelimiters value)
				{
					FixedWidthDelimiters? fixedWidthDelimiters = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidthDelimiters.CreateSafe(this._builders, node);
					if (fixedWidthDelimiters == null)
					{
						value = default(FixedWidthDelimiters);
						return false;
					}
					value = fixedWidthDelimiters.Value;
					return true;
				}

				// Token: 0x060095DD RID: 38365 RVA: 0x00200BB0 File Offset: 0x001FEDB0
				public bool GEN_Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095DE RID: 38366 RVA: 0x00200BD4 File Offset: 0x001FEDD4
				public bool GEN_Concat(ProgramNode node, out GEN_Concat value)
				{
					GEN_Concat? gen_Concat = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_Concat.CreateSafe(this._builders, node);
					if (gen_Concat == null)
					{
						value = default(GEN_Concat);
						return false;
					}
					value = gen_Concat.Value;
					return true;
				}

				// Token: 0x060095DF RID: 38367 RVA: 0x00200C10 File Offset: 0x001FEE10
				public bool GEN_LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_LookAround.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095E0 RID: 38368 RVA: 0x00200C34 File Offset: 0x001FEE34
				public bool GEN_LookAround(ProgramNode node, out GEN_LookAround value)
				{
					GEN_LookAround? gen_LookAround = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_LookAround.CreateSafe(this._builders, node);
					if (gen_LookAround == null)
					{
						value = default(GEN_LookAround);
						return false;
					}
					value = gen_LookAround.Value;
					return true;
				}

				// Token: 0x060095E1 RID: 38369 RVA: 0x00200C70 File Offset: 0x001FEE70
				public bool GEN_FieldLookAroundEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_FieldLookAroundEndPoints.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095E2 RID: 38370 RVA: 0x00200C94 File Offset: 0x001FEE94
				public bool GEN_FieldLookAroundEndPoints(ProgramNode node, out GEN_FieldLookAroundEndPoints value)
				{
					GEN_FieldLookAroundEndPoints? gen_FieldLookAroundEndPoints = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_FieldLookAroundEndPoints.CreateSafe(this._builders, node);
					if (gen_FieldLookAroundEndPoints == null)
					{
						value = default(GEN_FieldLookAroundEndPoints);
						return false;
					}
					value = gen_FieldLookAroundEndPoints.Value;
					return true;
				}

				// Token: 0x060095E3 RID: 38371 RVA: 0x00200CD0 File Offset: 0x001FEED0
				public bool Item2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095E4 RID: 38372 RVA: 0x00200CF4 File Offset: 0x001FEEF4
				public bool Item2(ProgramNode node, out Item2 value)
				{
					Item2? item = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item2.CreateSafe(this._builders, node);
					if (item == null)
					{
						value = default(Item2);
						return false;
					}
					value = item.Value;
					return true;
				}

				// Token: 0x060095E5 RID: 38373 RVA: 0x00200D30 File Offset: 0x001FEF30
				public bool Append(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095E6 RID: 38374 RVA: 0x00200D54 File Offset: 0x001FEF54
				public bool Append(ProgramNode node, out Append value)
				{
					Append? append = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
					if (append == null)
					{
						value = default(Append);
						return false;
					}
					value = append.Value;
					return true;
				}

				// Token: 0x060095E7 RID: 38375 RVA: 0x00200D90 File Offset: 0x001FEF90
				public bool Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095E8 RID: 38376 RVA: 0x00200DB4 File Offset: 0x001FEFB4
				public bool Split(ProgramNode node, out Split value)
				{
					Split? split = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						value = default(Split);
						return false;
					}
					value = split.Value;
					return true;
				}

				// Token: 0x060095E9 RID: 38377 RVA: 0x00200DF0 File Offset: 0x001FEFF0
				public bool InnerLetWitness(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.InnerLetWitness.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095EA RID: 38378 RVA: 0x00200E14 File Offset: 0x001FF014
				public bool InnerLetWitness(ProgramNode node, out InnerLetWitness value)
				{
					InnerLetWitness? innerLetWitness = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.InnerLetWitness.CreateSafe(this._builders, node);
					if (innerLetWitness == null)
					{
						value = default(InnerLetWitness);
						return false;
					}
					value = innerLetWitness.Value;
					return true;
				}

				// Token: 0x060095EB RID: 38379 RVA: 0x00200E50 File Offset: 0x001FF050
				public bool List(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095EC RID: 38380 RVA: 0x00200E74 File Offset: 0x001FF074
				public bool List(ProgramNode node, out List value)
				{
					List? list = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
					if (list == null)
					{
						value = default(List);
						return false;
					}
					value = list.Value;
					return true;
				}

				// Token: 0x060095ED RID: 38381 RVA: 0x00200EB0 File Offset: 0x001FF0B0
				public bool OuterLetWitness(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.OuterLetWitness.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095EE RID: 38382 RVA: 0x00200ED4 File Offset: 0x001FF0D4
				public bool OuterLetWitness(ProgramNode node, out OuterLetWitness value)
				{
					OuterLetWitness? outerLetWitness = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.OuterLetWitness.CreateSafe(this._builders, node);
					if (outerLetWitness == null)
					{
						value = default(OuterLetWitness);
						return false;
					}
					value = outerLetWitness.Value;
					return true;
				}

				// Token: 0x060095EF RID: 38383 RVA: 0x00200F10 File Offset: 0x001FF110
				public bool Item1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060095F0 RID: 38384 RVA: 0x00200F34 File Offset: 0x001FF134
				public bool Item1(ProgramNode node, out Item1 value)
				{
					Item1? item = Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item1.CreateSafe(this._builders, node);
					if (item == null)
					{
						value = default(Item1);
						return false;
					}
					value = item.Value;
					return true;
				}

				// Token: 0x04003D3A RID: 15674
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200132A RID: 4906
			public class NodeAs
			{
				// Token: 0x060095F1 RID: 38385 RVA: 0x00200F6E File Offset: 0x001FF16E
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060095F2 RID: 38386 RVA: 0x00200F7D File Offset: 0x001FF17D
				public regionSplit? regionSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F3 RID: 38387 RVA: 0x00200F8B File Offset: 0x001FF18B
				public splitMatches? splitMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F4 RID: 38388 RVA: 0x00200F99 File Offset: 0x001FF199
				public multipleMatches? multipleMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F5 RID: 38389 RVA: 0x00200FA7 File Offset: 0x001FF1A7
				public delimiterList? delimiterList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F6 RID: 38390 RVA: 0x00200FB5 File Offset: 0x001FF1B5
				public extractionPoints? extractionPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F7 RID: 38391 RVA: 0x00200FC3 File Offset: 0x001FF1C3
				public cndExtPoint? cndExtPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F8 RID: 38392 RVA: 0x00200FD1 File Offset: 0x001FF1D1
				public extPoint? extPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint.CreateSafe(this._builders, node);
				}

				// Token: 0x060095F9 RID: 38393 RVA: 0x00200FDF File Offset: 0x001FF1DF
				public pred? pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FA RID: 38394 RVA: 0x00200FED File Offset: 0x001FF1ED
				public pattern? pattern(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FB RID: 38395 RVA: 0x00200FFB File Offset: 0x001FF1FB
				public d? d(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FC RID: 38396 RVA: 0x00201009 File Offset: 0x001FF209
				public c? c(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FD RID: 38397 RVA: 0x00201017 File Offset: 0x001FF217
				public quotingConf? quotingConf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FE RID: 38398 RVA: 0x00201025 File Offset: 0x001FF225
				public constantDelimiterMatches? constantDelimiterMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x060095FF RID: 38399 RVA: 0x00201033 File Offset: 0x001FF233
				public r? r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
				}

				// Token: 0x06009600 RID: 38400 RVA: 0x00201041 File Offset: 0x001FF241
				public regexMatch? regexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x06009601 RID: 38401 RVA: 0x0020104F File Offset: 0x001FF24F
				public fieldMatch? fieldMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x06009602 RID: 38402 RVA: 0x0020105D File Offset: 0x001FF25D
				public fixedWidthMatches? fixedWidthMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x06009603 RID: 38403 RVA: 0x0020106B File Offset: 0x001FF26B
				public gen_Concat? gen_Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x06009604 RID: 38404 RVA: 0x00201079 File Offset: 0x001FF279
				public gen_LookAround? gen_LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround.CreateSafe(this._builders, node);
				}

				// Token: 0x06009605 RID: 38405 RVA: 0x00201087 File Offset: 0x001FF287
				public gen_LookAroundField? gen_LookAroundField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField.CreateSafe(this._builders, node);
				}

				// Token: 0x06009606 RID: 38406 RVA: 0x00201095 File Offset: 0x001FF295
				public delimiterStart? delimiterStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart.CreateSafe(this._builders, node);
				}

				// Token: 0x06009607 RID: 38407 RVA: 0x002010A3 File Offset: 0x001FF2A3
				public delimiterEnd? delimiterEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd.CreateSafe(this._builders, node);
				}

				// Token: 0x06009608 RID: 38408 RVA: 0x002010B1 File Offset: 0x001FF2B1
				public includeDelimiters? includeDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters.CreateSafe(this._builders, node);
				}

				// Token: 0x06009609 RID: 38409 RVA: 0x002010BF File Offset: 0x001FF2BF
				public fillStrategy? fillStrategy(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960A RID: 38410 RVA: 0x002010CD File Offset: 0x001FF2CD
				public ignoreIndexes? ignoreIndexes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960B RID: 38411 RVA: 0x002010DB File Offset: 0x001FF2DB
				public fieldStartPositions? fieldStartPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960C RID: 38412 RVA: 0x002010E9 File Offset: 0x001FF2E9
				public delimiterPositions? delimiterPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960D RID: 38413 RVA: 0x002010F7 File Offset: 0x001FF2F7
				public fregex? fregex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960E RID: 38414 RVA: 0x00201105 File Offset: 0x001FF305
				public s? s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
				}

				// Token: 0x0600960F RID: 38415 RVA: 0x00201113 File Offset: 0x001FF313
				public a? a(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a.CreateSafe(this._builders, node);
				}

				// Token: 0x06009610 RID: 38416 RVA: 0x00201121 File Offset: 0x001FF321
				public numSplits? numSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits.CreateSafe(this._builders, node);
				}

				// Token: 0x06009611 RID: 38417 RVA: 0x0020112F File Offset: 0x001FF32F
				public regex? regex(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex.CreateSafe(this._builders, node);
				}

				// Token: 0x06009612 RID: 38418 RVA: 0x0020113D File Offset: 0x001FF33D
				public obj? obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj.CreateSafe(this._builders, node);
				}

				// Token: 0x06009613 RID: 38419 RVA: 0x0020114B File Offset: 0x001FF34B
				public delimiter? delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x06009614 RID: 38420 RVA: 0x00201159 File Offset: 0x001FF359
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x06009615 RID: 38421 RVA: 0x00201167 File Offset: 0x001FF367
				public pair? pair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair.CreateSafe(this._builders, node);
				}

				// Token: 0x06009616 RID: 38422 RVA: 0x00201175 File Offset: 0x001FF375
				public item1? item1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1.CreateSafe(this._builders, node);
				}

				// Token: 0x06009617 RID: 38423 RVA: 0x00201183 File Offset: 0x001FF383
				public _LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x06009618 RID: 38424 RVA: 0x00201191 File Offset: 0x001FF391
				public _LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x06009619 RID: 38425 RVA: 0x0020119F File Offset: 0x001FF39F
				public _LetB2? _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600961A RID: 38426 RVA: 0x002011AD File Offset: 0x001FF3AD
				public _LetB3? _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
				}

				// Token: 0x04003D3B RID: 15675
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200132B RID: 4907
			public class RuleAs
			{
				// Token: 0x0600961B RID: 38427 RVA: 0x002011BB File Offset: 0x001FF3BB
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600961C RID: 38428 RVA: 0x002011CA File Offset: 0x001FF3CA
				public ExtractionSplit? ExtractionSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtractionSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600961D RID: 38429 RVA: 0x002011D8 File Offset: 0x001FF3D8
				public SplitRegion? SplitRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x0600961E RID: 38430 RVA: 0x002011E6 File Offset: 0x001FF3E6
				public splitMatches_multipleMatches? splitMatches_multipleMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_multipleMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x0600961F RID: 38431 RVA: 0x002011F4 File Offset: 0x001FF3F4
				public splitMatches_constantDelimiterMatches? splitMatches_constantDelimiterMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_constantDelimiterMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x06009620 RID: 38432 RVA: 0x00201202 File Offset: 0x001FF402
				public splitMatches_fixedWidthMatches? splitMatches_fixedWidthMatches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.splitMatches_fixedWidthMatches.CreateSafe(this._builders, node);
				}

				// Token: 0x06009621 RID: 38433 RVA: 0x00201210 File Offset: 0x001FF410
				public SplitMultiple? SplitMultiple(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SplitMultiple.CreateSafe(this._builders, node);
				}

				// Token: 0x06009622 RID: 38434 RVA: 0x0020121E File Offset: 0x001FF41E
				public multipleMatches_d? multipleMatches_d(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.multipleMatches_d.CreateSafe(this._builders, node);
				}

				// Token: 0x06009623 RID: 38435 RVA: 0x0020122C File Offset: 0x001FF42C
				public DelimitersList? DelimitersList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.DelimitersList.CreateSafe(this._builders, node);
				}

				// Token: 0x06009624 RID: 38436 RVA: 0x0020123A File Offset: 0x001FF43A
				public EmptyDelimitersList? EmptyDelimitersList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyDelimitersList.CreateSafe(this._builders, node);
				}

				// Token: 0x06009625 RID: 38437 RVA: 0x00201248 File Offset: 0x001FF448
				public ExtPointsList? ExtPointsList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ExtPointsList.CreateSafe(this._builders, node);
				}

				// Token: 0x06009626 RID: 38438 RVA: 0x00201256 File Offset: 0x001FF456
				public EmptyExtPointsList? EmptyExtPointsList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.EmptyExtPointsList.CreateSafe(this._builders, node);
				}

				// Token: 0x06009627 RID: 38439 RVA: 0x00201264 File Offset: 0x001FF464
				public cndExtPoint_extPoint? cndExtPoint_extPoint(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.cndExtPoint_extPoint.CreateSafe(this._builders, node);
				}

				// Token: 0x06009628 RID: 38440 RVA: 0x00201272 File Offset: 0x001FF472
				public ConditionalExtract? ConditionalExtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConditionalExtract.CreateSafe(this._builders, node);
				}

				// Token: 0x06009629 RID: 38441 RVA: 0x00201280 File Offset: 0x001FF480
				public SpecialCharPattern? SpecialCharPattern(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.SpecialCharPattern.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962A RID: 38442 RVA: 0x0020128E File Offset: 0x001FF48E
				public LookAround? LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.LookAround.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962B RID: 38443 RVA: 0x0020129C File Offset: 0x001FF49C
				public FieldEndPoints? FieldEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldEndPoints.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962C RID: 38444 RVA: 0x002012AA File Offset: 0x001FF4AA
				public FieldLookAroundEndPoints? FieldLookAroundEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldLookAroundEndPoints.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962D RID: 38445 RVA: 0x002012B8 File Offset: 0x001FF4B8
				public ConstStr? ConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962E RID: 38446 RVA: 0x002012C6 File Offset: 0x001FF4C6
				public ConstStrWithWhitespace? ConstStrWithWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstStrWithWhitespace.CreateSafe(this._builders, node);
				}

				// Token: 0x0600962F RID: 38447 RVA: 0x002012D4 File Offset: 0x001FF4D4
				public ConstAlphStr? ConstAlphStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstAlphStr.CreateSafe(this._builders, node);
				}

				// Token: 0x06009630 RID: 38448 RVA: 0x002012E2 File Offset: 0x001FF4E2
				public ConstantDelimiterWithQuoting? ConstantDelimiterWithQuoting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiterWithQuoting.CreateSafe(this._builders, node);
				}

				// Token: 0x06009631 RID: 38449 RVA: 0x002012F0 File Offset: 0x001FF4F0
				public ConstantDelimiter? ConstantDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.ConstantDelimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x06009632 RID: 38450 RVA: 0x002012FE File Offset: 0x001FF4FE
				public Empty? Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
				}

				// Token: 0x06009633 RID: 38451 RVA: 0x0020130C File Offset: 0x001FF50C
				public r_regexMatch? r_regexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes.r_regexMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x06009634 RID: 38452 RVA: 0x0020131A File Offset: 0x001FF51A
				public Concat? Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x06009635 RID: 38453 RVA: 0x00201328 File Offset: 0x001FF528
				public RegexMatch? RegexMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.RegexMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x06009636 RID: 38454 RVA: 0x00201336 File Offset: 0x001FF536
				public FieldMatch? FieldMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FieldMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x06009637 RID: 38455 RVA: 0x00201344 File Offset: 0x001FF544
				public FixedWidth? FixedWidth(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidth.CreateSafe(this._builders, node);
				}

				// Token: 0x06009638 RID: 38456 RVA: 0x00201352 File Offset: 0x001FF552
				public FixedWidthDelimiters? FixedWidthDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.FixedWidthDelimiters.CreateSafe(this._builders, node);
				}

				// Token: 0x06009639 RID: 38457 RVA: 0x00201360 File Offset: 0x001FF560
				public GEN_Concat? GEN_Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963A RID: 38458 RVA: 0x0020136E File Offset: 0x001FF56E
				public GEN_LookAround? GEN_LookAround(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_LookAround.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963B RID: 38459 RVA: 0x0020137C File Offset: 0x001FF57C
				public GEN_FieldLookAroundEndPoints? GEN_FieldLookAroundEndPoints(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.GEN_FieldLookAroundEndPoints.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963C RID: 38460 RVA: 0x0020138A File Offset: 0x001FF58A
				public Item2? Item2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963D RID: 38461 RVA: 0x00201398 File Offset: 0x001FF598
				public Append? Append(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963E RID: 38462 RVA: 0x002013A6 File Offset: 0x001FF5A6
				public Split? Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
				}

				// Token: 0x0600963F RID: 38463 RVA: 0x002013B4 File Offset: 0x001FF5B4
				public InnerLetWitness? InnerLetWitness(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.InnerLetWitness.CreateSafe(this._builders, node);
				}

				// Token: 0x06009640 RID: 38464 RVA: 0x002013C2 File Offset: 0x001FF5C2
				public List? List(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
				}

				// Token: 0x06009641 RID: 38465 RVA: 0x002013D0 File Offset: 0x001FF5D0
				public OuterLetWitness? OuterLetWitness(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.OuterLetWitness.CreateSafe(this._builders, node);
				}

				// Token: 0x06009642 RID: 38466 RVA: 0x002013DE File Offset: 0x001FF5DE
				public Item1? Item1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes.Item1.CreateSafe(this._builders, node);
				}

				// Token: 0x04003D3C RID: 15676
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x0200132D RID: 4909
		public class Sets
		{
			// Token: 0x06009646 RID: 38470 RVA: 0x00201408 File Offset: 0x001FF608
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x170019F0 RID: 6640
			// (get) Token: 0x06009647 RID: 38471 RVA: 0x00201457 File Offset: 0x001FF657
			// (set) Token: 0x06009648 RID: 38472 RVA: 0x0020145F File Offset: 0x001FF65F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x170019F1 RID: 6641
			// (get) Token: 0x06009649 RID: 38473 RVA: 0x00201468 File Offset: 0x001FF668
			// (set) Token: 0x0600964A RID: 38474 RVA: 0x00201470 File Offset: 0x001FF670
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x170019F2 RID: 6642
			// (get) Token: 0x0600964B RID: 38475 RVA: 0x00201479 File Offset: 0x001FF679
			// (set) Token: 0x0600964C RID: 38476 RVA: 0x00201481 File Offset: 0x001FF681
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x170019F3 RID: 6643
			// (get) Token: 0x0600964D RID: 38477 RVA: 0x0020148A File Offset: 0x001FF68A
			// (set) Token: 0x0600964E RID: 38478 RVA: 0x00201492 File Offset: 0x001FF692
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x170019F4 RID: 6644
			// (get) Token: 0x0600964F RID: 38479 RVA: 0x0020149B File Offset: 0x001FF69B
			// (set) Token: 0x06009650 RID: 38480 RVA: 0x002014A3 File Offset: 0x001FF6A3
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x0200132E RID: 4910
			public class Joins
			{
				// Token: 0x06009651 RID: 38481 RVA: 0x002014AC File Offset: 0x001FF6AC
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009652 RID: 38482 RVA: 0x002014BC File Offset: 0x001FF6BC
				public ProgramSetBuilder<regionSplit> ExtractionSplit(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiterList> value1, ProgramSetBuilder<extractionPoints> value2)
				{
					return ProgramSetBuilder<regionSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ExtractionSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06009653 RID: 38483 RVA: 0x00201518 File Offset: 0x001FF718
				public ProgramSetBuilder<regionSplit> SplitRegion(ProgramSetBuilder<v> value0, ProgramSetBuilder<splitMatches> value1, ProgramSetBuilder<ignoreIndexes> value2, ProgramSetBuilder<numSplits> value3, ProgramSetBuilder<delimiterStart> value4, ProgramSetBuilder<delimiterEnd> value5, ProgramSetBuilder<includeDelimiters> value6, ProgramSetBuilder<fillStrategy> value7)
				{
					return ProgramSetBuilder<regionSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null,
						(value7 != null) ? value7.Set : null
					}));
				}

				// Token: 0x06009654 RID: 38484 RVA: 0x002015C7 File Offset: 0x001FF7C7
				public ProgramSetBuilder<multipleMatches> SplitMultiple(ProgramSetBuilder<multipleMatches> value0, ProgramSetBuilder<d> value1)
				{
					return ProgramSetBuilder<multipleMatches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitMultiple, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009655 RID: 38485 RVA: 0x00201607 File Offset: 0x001FF807
				public ProgramSetBuilder<delimiterList> DelimitersList(ProgramSetBuilder<delimiterList> value0, ProgramSetBuilder<d> value1)
				{
					return ProgramSetBuilder<delimiterList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DelimitersList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009656 RID: 38486 RVA: 0x00201647 File Offset: 0x001FF847
				public ProgramSetBuilder<delimiterList> EmptyDelimitersList()
				{
					return ProgramSetBuilder<delimiterList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EmptyDelimitersList, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06009657 RID: 38487 RVA: 0x00201668 File Offset: 0x001FF868
				public ProgramSetBuilder<extractionPoints> ExtPointsList(ProgramSetBuilder<extractionPoints> value0, ProgramSetBuilder<cndExtPoint> value1)
				{
					return ProgramSetBuilder<extractionPoints>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ExtPointsList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009658 RID: 38488 RVA: 0x002016A8 File Offset: 0x001FF8A8
				public ProgramSetBuilder<extractionPoints> EmptyExtPointsList()
				{
					return ProgramSetBuilder<extractionPoints>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EmptyExtPointsList, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06009659 RID: 38489 RVA: 0x002016CC File Offset: 0x001FF8CC
				public ProgramSetBuilder<cndExtPoint> ConditionalExtract(ProgramSetBuilder<pred> value0, ProgramSetBuilder<extPoint> value1, ProgramSetBuilder<cndExtPoint> value2)
				{
					return ProgramSetBuilder<cndExtPoint>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConditionalExtract, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600965A RID: 38490 RVA: 0x00201726 File Offset: 0x001FF926
				public ProgramSetBuilder<pred> SpecialCharPattern(ProgramSetBuilder<v> value0, ProgramSetBuilder<pattern> value1)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SpecialCharPattern, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600965B RID: 38491 RVA: 0x00201768 File Offset: 0x001FF968
				public ProgramSetBuilder<d> LookAround(ProgramSetBuilder<r> value0, ProgramSetBuilder<c> value1, ProgramSetBuilder<r> value2)
				{
					return ProgramSetBuilder<d>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LookAround, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600965C RID: 38492 RVA: 0x002017C2 File Offset: 0x001FF9C2
				public ProgramSetBuilder<d> FieldEndPoints(ProgramSetBuilder<fieldMatch> value0)
				{
					return ProgramSetBuilder<d>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FieldEndPoints, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600965D RID: 38493 RVA: 0x002017F4 File Offset: 0x001FF9F4
				public ProgramSetBuilder<d> FieldLookAroundEndPoints(ProgramSetBuilder<regexMatch> value0, ProgramSetBuilder<fieldMatch> value1, ProgramSetBuilder<regexMatch> value2)
				{
					return ProgramSetBuilder<d>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FieldLookAroundEndPoints, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600965E RID: 38494 RVA: 0x0020184E File Offset: 0x001FFA4E
				public ProgramSetBuilder<c> ConstStr(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return ProgramSetBuilder<c>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600965F RID: 38495 RVA: 0x0020188E File Offset: 0x001FFA8E
				public ProgramSetBuilder<c> ConstStrWithWhitespace(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return ProgramSetBuilder<c>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstStrWithWhitespace, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009660 RID: 38496 RVA: 0x002018CE File Offset: 0x001FFACE
				public ProgramSetBuilder<c> ConstAlphStr(ProgramSetBuilder<v> value0, ProgramSetBuilder<a> value1)
				{
					return ProgramSetBuilder<c>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstAlphStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009661 RID: 38497 RVA: 0x00201910 File Offset: 0x001FFB10
				public ProgramSetBuilder<constantDelimiterMatches> ConstantDelimiterWithQuoting(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1, ProgramSetBuilder<quotingConf> value2)
				{
					return ProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstantDelimiterWithQuoting, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06009662 RID: 38498 RVA: 0x0020196A File Offset: 0x001FFB6A
				public ProgramSetBuilder<constantDelimiterMatches> ConstantDelimiter(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return ProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstantDelimiter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009663 RID: 38499 RVA: 0x002019AA File Offset: 0x001FFBAA
				public ProgramSetBuilder<r> Empty(ProgramSetBuilder<v> value0)
				{
					return ProgramSetBuilder<r>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Empty, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009664 RID: 38500 RVA: 0x002019DB File Offset: 0x001FFBDB
				public ProgramSetBuilder<r> Concat(ProgramSetBuilder<r> value0, ProgramSetBuilder<regexMatch> value1)
				{
					return ProgramSetBuilder<r>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009665 RID: 38501 RVA: 0x00201A1B File Offset: 0x001FFC1B
				public ProgramSetBuilder<regexMatch> RegexMatch(ProgramSetBuilder<v> value0, ProgramSetBuilder<regex> value1)
				{
					return ProgramSetBuilder<regexMatch>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RegexMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009666 RID: 38502 RVA: 0x00201A5B File Offset: 0x001FFC5B
				public ProgramSetBuilder<fieldMatch> FieldMatch(ProgramSetBuilder<v> value0, ProgramSetBuilder<fregex> value1)
				{
					return ProgramSetBuilder<fieldMatch>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FieldMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009667 RID: 38503 RVA: 0x00201A9B File Offset: 0x001FFC9B
				public ProgramSetBuilder<fixedWidthMatches> FixedWidth(ProgramSetBuilder<v> value0, ProgramSetBuilder<fieldStartPositions> value1)
				{
					return ProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FixedWidth, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009668 RID: 38504 RVA: 0x00201ADB File Offset: 0x001FFCDB
				public ProgramSetBuilder<fixedWidthMatches> FixedWidthDelimiters(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiterPositions> value1)
				{
					return ProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FixedWidthDelimiters, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009669 RID: 38505 RVA: 0x00201B1B File Offset: 0x001FFD1B
				public ProgramSetBuilder<gen_Concat> GEN_Concat(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_Concat>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600966A RID: 38506 RVA: 0x00201B5C File Offset: 0x001FFD5C
				public ProgramSetBuilder<gen_LookAround> GEN_LookAround(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1, ProgramSetBuilder<obj> value2)
				{
					return ProgramSetBuilder<gen_LookAround>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_LookAround, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600966B RID: 38507 RVA: 0x00201BB8 File Offset: 0x001FFDB8
				public ProgramSetBuilder<gen_LookAroundField> GEN_FieldLookAroundEndPoints(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1, ProgramSetBuilder<obj> value2)
				{
					return ProgramSetBuilder<gen_LookAroundField>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_FieldLookAroundEndPoints, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600966C RID: 38508 RVA: 0x00201C12 File Offset: 0x001FFE12
				public ProgramSetBuilder<_LetB0> Item2(ProgramSetBuilder<pair> value0)
				{
					return ProgramSetBuilder<_LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Item2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600966D RID: 38509 RVA: 0x00201C43 File Offset: 0x001FFE43
				public ProgramSetBuilder<_LetB1> Append(ProgramSetBuilder<item1> value0, ProgramSetBuilder<output> value1)
				{
					return ProgramSetBuilder<_LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Append, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600966E RID: 38510 RVA: 0x00201C83 File Offset: 0x001FFE83
				public ProgramSetBuilder<_LetB2> Split(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiter> value1)
				{
					return ProgramSetBuilder<_LetB2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Split, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600966F RID: 38511 RVA: 0x00201CC3 File Offset: 0x001FFEC3
				public ProgramSetBuilder<output> List(ProgramSetBuilder<v> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.List, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009670 RID: 38512 RVA: 0x00201CF4 File Offset: 0x001FFEF4
				public ProgramSetBuilder<item1> Item1(ProgramSetBuilder<pair> value0)
				{
					return ProgramSetBuilder<item1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Item1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009671 RID: 38513 RVA: 0x00201D25 File Offset: 0x001FFF25
				public ProgramSetBuilder<_LetB3> InnerLetWitness(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return ProgramSetBuilder<_LetB3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.InnerLetWitness, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009672 RID: 38514 RVA: 0x00201D65 File Offset: 0x001FFF65
				public ProgramSetBuilder<output> OuterLetWitness(ProgramSetBuilder<_LetB2> value0, ProgramSetBuilder<_LetB3> value1)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.OuterLetWitness, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003D43 RID: 15683
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200132F RID: 4911
			public class ExplicitJoins
			{
				// Token: 0x06009673 RID: 38515 RVA: 0x00201DA5 File Offset: 0x001FFFA5
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009674 RID: 38516 RVA: 0x00201DB4 File Offset: 0x001FFFB4
				public JoinProgramSetBuilder<regionSplit> ExtractionSplit(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiterList> value1, ProgramSetBuilder<extractionPoints> value2)
				{
					return JoinProgramSetBuilder<regionSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ExtractionSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06009675 RID: 38517 RVA: 0x00201E10 File Offset: 0x00200010
				public JoinProgramSetBuilder<regionSplit> SplitRegion(ProgramSetBuilder<v> value0, ProgramSetBuilder<splitMatches> value1, ProgramSetBuilder<ignoreIndexes> value2, ProgramSetBuilder<numSplits> value3, ProgramSetBuilder<delimiterStart> value4, ProgramSetBuilder<delimiterEnd> value5, ProgramSetBuilder<includeDelimiters> value6, ProgramSetBuilder<fillStrategy> value7)
				{
					return JoinProgramSetBuilder<regionSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null,
						(value7 != null) ? value7.Set : null
					}));
				}

				// Token: 0x06009676 RID: 38518 RVA: 0x00201EBF File Offset: 0x002000BF
				public JoinProgramSetBuilder<multipleMatches> SplitMultiple(ProgramSetBuilder<multipleMatches> value0, ProgramSetBuilder<d> value1)
				{
					return JoinProgramSetBuilder<multipleMatches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitMultiple, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009677 RID: 38519 RVA: 0x00201EFF File Offset: 0x002000FF
				public JoinProgramSetBuilder<delimiterList> DelimitersList(ProgramSetBuilder<delimiterList> value0, ProgramSetBuilder<d> value1)
				{
					return JoinProgramSetBuilder<delimiterList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DelimitersList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009678 RID: 38520 RVA: 0x00201F3F File Offset: 0x0020013F
				public JoinProgramSetBuilder<delimiterList> EmptyDelimitersList()
				{
					return JoinProgramSetBuilder<delimiterList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EmptyDelimitersList, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06009679 RID: 38521 RVA: 0x00201F60 File Offset: 0x00200160
				public JoinProgramSetBuilder<extractionPoints> ExtPointsList(ProgramSetBuilder<extractionPoints> value0, ProgramSetBuilder<cndExtPoint> value1)
				{
					return JoinProgramSetBuilder<extractionPoints>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ExtPointsList, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600967A RID: 38522 RVA: 0x00201FA0 File Offset: 0x002001A0
				public JoinProgramSetBuilder<extractionPoints> EmptyExtPointsList()
				{
					return JoinProgramSetBuilder<extractionPoints>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EmptyExtPointsList, Array.Empty<ProgramSet>()));
				}

				// Token: 0x0600967B RID: 38523 RVA: 0x00201FC4 File Offset: 0x002001C4
				public JoinProgramSetBuilder<cndExtPoint> ConditionalExtract(ProgramSetBuilder<pred> value0, ProgramSetBuilder<extPoint> value1, ProgramSetBuilder<cndExtPoint> value2)
				{
					return JoinProgramSetBuilder<cndExtPoint>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConditionalExtract, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600967C RID: 38524 RVA: 0x0020201E File Offset: 0x0020021E
				public JoinProgramSetBuilder<pred> SpecialCharPattern(ProgramSetBuilder<v> value0, ProgramSetBuilder<pattern> value1)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SpecialCharPattern, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600967D RID: 38525 RVA: 0x00202060 File Offset: 0x00200260
				public JoinProgramSetBuilder<d> LookAround(ProgramSetBuilder<r> value0, ProgramSetBuilder<c> value1, ProgramSetBuilder<r> value2)
				{
					return JoinProgramSetBuilder<d>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LookAround, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600967E RID: 38526 RVA: 0x002020BA File Offset: 0x002002BA
				public JoinProgramSetBuilder<d> FieldEndPoints(ProgramSetBuilder<fieldMatch> value0)
				{
					return JoinProgramSetBuilder<d>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FieldEndPoints, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600967F RID: 38527 RVA: 0x002020EC File Offset: 0x002002EC
				public JoinProgramSetBuilder<d> FieldLookAroundEndPoints(ProgramSetBuilder<regexMatch> value0, ProgramSetBuilder<fieldMatch> value1, ProgramSetBuilder<regexMatch> value2)
				{
					return JoinProgramSetBuilder<d>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FieldLookAroundEndPoints, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06009680 RID: 38528 RVA: 0x00202146 File Offset: 0x00200346
				public JoinProgramSetBuilder<c> ConstStr(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return JoinProgramSetBuilder<c>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009681 RID: 38529 RVA: 0x00202186 File Offset: 0x00200386
				public JoinProgramSetBuilder<c> ConstStrWithWhitespace(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return JoinProgramSetBuilder<c>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstStrWithWhitespace, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009682 RID: 38530 RVA: 0x002021C6 File Offset: 0x002003C6
				public JoinProgramSetBuilder<c> ConstAlphStr(ProgramSetBuilder<v> value0, ProgramSetBuilder<a> value1)
				{
					return JoinProgramSetBuilder<c>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstAlphStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009683 RID: 38531 RVA: 0x00202208 File Offset: 0x00200408
				public JoinProgramSetBuilder<constantDelimiterMatches> ConstantDelimiterWithQuoting(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1, ProgramSetBuilder<quotingConf> value2)
				{
					return JoinProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstantDelimiterWithQuoting, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06009684 RID: 38532 RVA: 0x00202262 File Offset: 0x00200462
				public JoinProgramSetBuilder<constantDelimiterMatches> ConstantDelimiter(ProgramSetBuilder<v> value0, ProgramSetBuilder<s> value1)
				{
					return JoinProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstantDelimiter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009685 RID: 38533 RVA: 0x002022A2 File Offset: 0x002004A2
				public JoinProgramSetBuilder<r> Empty(ProgramSetBuilder<v> value0)
				{
					return JoinProgramSetBuilder<r>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Empty, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009686 RID: 38534 RVA: 0x002022D3 File Offset: 0x002004D3
				public JoinProgramSetBuilder<r> Concat(ProgramSetBuilder<r> value0, ProgramSetBuilder<regexMatch> value1)
				{
					return JoinProgramSetBuilder<r>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009687 RID: 38535 RVA: 0x00202313 File Offset: 0x00200513
				public JoinProgramSetBuilder<regexMatch> RegexMatch(ProgramSetBuilder<v> value0, ProgramSetBuilder<regex> value1)
				{
					return JoinProgramSetBuilder<regexMatch>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RegexMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009688 RID: 38536 RVA: 0x00202353 File Offset: 0x00200553
				public JoinProgramSetBuilder<fieldMatch> FieldMatch(ProgramSetBuilder<v> value0, ProgramSetBuilder<fregex> value1)
				{
					return JoinProgramSetBuilder<fieldMatch>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FieldMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009689 RID: 38537 RVA: 0x00202393 File Offset: 0x00200593
				public JoinProgramSetBuilder<fixedWidthMatches> FixedWidth(ProgramSetBuilder<v> value0, ProgramSetBuilder<fieldStartPositions> value1)
				{
					return JoinProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FixedWidth, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600968A RID: 38538 RVA: 0x002023D3 File Offset: 0x002005D3
				public JoinProgramSetBuilder<fixedWidthMatches> FixedWidthDelimiters(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiterPositions> value1)
				{
					return JoinProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FixedWidthDelimiters, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600968B RID: 38539 RVA: 0x00202413 File Offset: 0x00200613
				public JoinProgramSetBuilder<gen_Concat> GEN_Concat(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_Concat>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600968C RID: 38540 RVA: 0x00202454 File Offset: 0x00200654
				public JoinProgramSetBuilder<gen_LookAround> GEN_LookAround(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1, ProgramSetBuilder<obj> value2)
				{
					return JoinProgramSetBuilder<gen_LookAround>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_LookAround, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600968D RID: 38541 RVA: 0x002024B0 File Offset: 0x002006B0
				public JoinProgramSetBuilder<gen_LookAroundField> GEN_FieldLookAroundEndPoints(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1, ProgramSetBuilder<obj> value2)
				{
					return JoinProgramSetBuilder<gen_LookAroundField>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_FieldLookAroundEndPoints, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600968E RID: 38542 RVA: 0x0020250A File Offset: 0x0020070A
				public JoinProgramSetBuilder<_LetB0> Item2(ProgramSetBuilder<pair> value0)
				{
					return JoinProgramSetBuilder<_LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Item2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600968F RID: 38543 RVA: 0x0020253B File Offset: 0x0020073B
				public JoinProgramSetBuilder<_LetB1> Append(ProgramSetBuilder<item1> value0, ProgramSetBuilder<output> value1)
				{
					return JoinProgramSetBuilder<_LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Append, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009690 RID: 38544 RVA: 0x0020257B File Offset: 0x0020077B
				public JoinProgramSetBuilder<_LetB2> Split(ProgramSetBuilder<v> value0, ProgramSetBuilder<delimiter> value1)
				{
					return JoinProgramSetBuilder<_LetB2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Split, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009691 RID: 38545 RVA: 0x002025BB File Offset: 0x002007BB
				public JoinProgramSetBuilder<output> List(ProgramSetBuilder<v> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.List, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009692 RID: 38546 RVA: 0x002025EC File Offset: 0x002007EC
				public JoinProgramSetBuilder<item1> Item1(ProgramSetBuilder<pair> value0)
				{
					return JoinProgramSetBuilder<item1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Item1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009693 RID: 38547 RVA: 0x0020261D File Offset: 0x0020081D
				public JoinProgramSetBuilder<_LetB3> InnerLetWitness(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return JoinProgramSetBuilder<_LetB3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.InnerLetWitness, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06009694 RID: 38548 RVA: 0x0020265D File Offset: 0x0020085D
				public JoinProgramSetBuilder<output> OuterLetWitness(ProgramSetBuilder<_LetB2> value0, ProgramSetBuilder<_LetB3> value1)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.OuterLetWitness, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003D44 RID: 15684
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001330 RID: 4912
			public class JoinUnnamedConversions
			{
				// Token: 0x06009695 RID: 38549 RVA: 0x0020269D File Offset: 0x0020089D
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06009696 RID: 38550 RVA: 0x002026AC File Offset: 0x002008AC
				public ProgramSetBuilder<splitMatches> splitMatches_multipleMatches(ProgramSetBuilder<multipleMatches> value0)
				{
					return ProgramSetBuilder<splitMatches>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.splitMatches_multipleMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009697 RID: 38551 RVA: 0x002026DD File Offset: 0x002008DD
				public ProgramSetBuilder<splitMatches> splitMatches_constantDelimiterMatches(ProgramSetBuilder<constantDelimiterMatches> value0)
				{
					return ProgramSetBuilder<splitMatches>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.splitMatches_constantDelimiterMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009698 RID: 38552 RVA: 0x0020270E File Offset: 0x0020090E
				public ProgramSetBuilder<splitMatches> splitMatches_fixedWidthMatches(ProgramSetBuilder<fixedWidthMatches> value0)
				{
					return ProgramSetBuilder<splitMatches>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.splitMatches_fixedWidthMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06009699 RID: 38553 RVA: 0x0020273F File Offset: 0x0020093F
				public ProgramSetBuilder<multipleMatches> multipleMatches_d(ProgramSetBuilder<d> value0)
				{
					return ProgramSetBuilder<multipleMatches>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.multipleMatches_d, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600969A RID: 38554 RVA: 0x00202770 File Offset: 0x00200970
				public ProgramSetBuilder<cndExtPoint> cndExtPoint_extPoint(ProgramSetBuilder<extPoint> value0)
				{
					return ProgramSetBuilder<cndExtPoint>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.cndExtPoint_extPoint, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600969B RID: 38555 RVA: 0x002027A1 File Offset: 0x002009A1
				public ProgramSetBuilder<r> r_regexMatch(ProgramSetBuilder<regexMatch> value0)
				{
					return ProgramSetBuilder<r>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.r_regexMatch, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04003D45 RID: 15685
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001331 RID: 4913
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x0600969C RID: 38556 RVA: 0x002027D2 File Offset: 0x002009D2
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600969D RID: 38557 RVA: 0x002027E1 File Offset: 0x002009E1
				public JoinProgramSetBuilder<splitMatches> splitMatches_multipleMatches(ProgramSetBuilder<multipleMatches> value0)
				{
					return JoinProgramSetBuilder<splitMatches>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.splitMatches_multipleMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600969E RID: 38558 RVA: 0x00202812 File Offset: 0x00200A12
				public JoinProgramSetBuilder<splitMatches> splitMatches_constantDelimiterMatches(ProgramSetBuilder<constantDelimiterMatches> value0)
				{
					return JoinProgramSetBuilder<splitMatches>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.splitMatches_constantDelimiterMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600969F RID: 38559 RVA: 0x00202843 File Offset: 0x00200A43
				public JoinProgramSetBuilder<splitMatches> splitMatches_fixedWidthMatches(ProgramSetBuilder<fixedWidthMatches> value0)
				{
					return JoinProgramSetBuilder<splitMatches>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.splitMatches_fixedWidthMatches, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060096A0 RID: 38560 RVA: 0x00202874 File Offset: 0x00200A74
				public JoinProgramSetBuilder<multipleMatches> multipleMatches_d(ProgramSetBuilder<d> value0)
				{
					return JoinProgramSetBuilder<multipleMatches>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.multipleMatches_d, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060096A1 RID: 38561 RVA: 0x002028A5 File Offset: 0x00200AA5
				public JoinProgramSetBuilder<cndExtPoint> cndExtPoint_extPoint(ProgramSetBuilder<extPoint> value0)
				{
					return JoinProgramSetBuilder<cndExtPoint>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.cndExtPoint_extPoint, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060096A2 RID: 38562 RVA: 0x002028D6 File Offset: 0x00200AD6
				public JoinProgramSetBuilder<r> r_regexMatch(ProgramSetBuilder<regexMatch> value0)
				{
					return JoinProgramSetBuilder<r>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.r_regexMatch, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04003D46 RID: 15686
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001332 RID: 4914
			public class Casts
			{
				// Token: 0x060096A3 RID: 38563 RVA: 0x00202907 File Offset: 0x00200B07
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060096A4 RID: 38564 RVA: 0x00202918 File Offset: 0x00200B18
				public ProgramSetBuilder<regionSplit> regionSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regionSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol regionSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regionSplit>.CreateUnsafe(set);
				}

				// Token: 0x060096A5 RID: 38565 RVA: 0x00202970 File Offset: 0x00200B70
				public ProgramSetBuilder<splitMatches> splitMatches(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitMatches)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitMatches but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.splitMatches>.CreateUnsafe(set);
				}

				// Token: 0x060096A6 RID: 38566 RVA: 0x002029C8 File Offset: 0x00200BC8
				public ProgramSetBuilder<multipleMatches> multipleMatches(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.multipleMatches)
					{
						string text = "set";
						string text2 = "expected program set for symbol multipleMatches but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.multipleMatches>.CreateUnsafe(set);
				}

				// Token: 0x060096A7 RID: 38567 RVA: 0x00202A20 File Offset: 0x00200C20
				public ProgramSetBuilder<delimiterList> delimiterList(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiterList)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiterList but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterList>.CreateUnsafe(set);
				}

				// Token: 0x060096A8 RID: 38568 RVA: 0x00202A78 File Offset: 0x00200C78
				public ProgramSetBuilder<extractionPoints> extractionPoints(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.extractionPoints)
					{
						string text = "set";
						string text2 = "expected program set for symbol extractionPoints but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extractionPoints>.CreateUnsafe(set);
				}

				// Token: 0x060096A9 RID: 38569 RVA: 0x00202AD0 File Offset: 0x00200CD0
				public ProgramSetBuilder<cndExtPoint> cndExtPoint(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.cndExtPoint)
					{
						string text = "set";
						string text2 = "expected program set for symbol cndExtPoint but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.cndExtPoint>.CreateUnsafe(set);
				}

				// Token: 0x060096AA RID: 38570 RVA: 0x00202B28 File Offset: 0x00200D28
				public ProgramSetBuilder<extPoint> extPoint(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.extPoint)
					{
						string text = "set";
						string text2 = "expected program set for symbol extPoint but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.extPoint>.CreateUnsafe(set);
				}

				// Token: 0x060096AB RID: 38571 RVA: 0x00202B80 File Offset: 0x00200D80
				public ProgramSetBuilder<pred> pred(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pred)
					{
						string text = "set";
						string text2 = "expected program set for symbol pred but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pred>.CreateUnsafe(set);
				}

				// Token: 0x060096AC RID: 38572 RVA: 0x00202BD8 File Offset: 0x00200DD8
				public ProgramSetBuilder<pattern> pattern(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pattern)
					{
						string text = "set";
						string text2 = "expected program set for symbol pattern but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pattern>.CreateUnsafe(set);
				}

				// Token: 0x060096AD RID: 38573 RVA: 0x00202C30 File Offset: 0x00200E30
				public ProgramSetBuilder<d> d(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.d)
					{
						string text = "set";
						string text2 = "expected program set for symbol d but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.d>.CreateUnsafe(set);
				}

				// Token: 0x060096AE RID: 38574 RVA: 0x00202C88 File Offset: 0x00200E88
				public ProgramSetBuilder<c> c(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.c)
					{
						string text = "set";
						string text2 = "expected program set for symbol c but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.c>.CreateUnsafe(set);
				}

				// Token: 0x060096AF RID: 38575 RVA: 0x00202CE0 File Offset: 0x00200EE0
				public ProgramSetBuilder<quotingConf> quotingConf(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.quotingConf)
					{
						string text = "set";
						string text2 = "expected program set for symbol quotingConf but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.quotingConf>.CreateUnsafe(set);
				}

				// Token: 0x060096B0 RID: 38576 RVA: 0x00202D38 File Offset: 0x00200F38
				public ProgramSetBuilder<constantDelimiterMatches> constantDelimiterMatches(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.constantDelimiterMatches)
					{
						string text = "set";
						string text2 = "expected program set for symbol constantDelimiterMatches but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.constantDelimiterMatches>.CreateUnsafe(set);
				}

				// Token: 0x060096B1 RID: 38577 RVA: 0x00202D90 File Offset: 0x00200F90
				public ProgramSetBuilder<r> r(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.r)
					{
						string text = "set";
						string text2 = "expected program set for symbol r but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.r>.CreateUnsafe(set);
				}

				// Token: 0x060096B2 RID: 38578 RVA: 0x00202DE8 File Offset: 0x00200FE8
				public ProgramSetBuilder<regexMatch> regexMatch(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regexMatch)
					{
						string text = "set";
						string text2 = "expected program set for symbol regexMatch but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regexMatch>.CreateUnsafe(set);
				}

				// Token: 0x060096B3 RID: 38579 RVA: 0x00202E40 File Offset: 0x00201040
				public ProgramSetBuilder<fieldMatch> fieldMatch(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fieldMatch)
					{
						string text = "set";
						string text2 = "expected program set for symbol fieldMatch but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldMatch>.CreateUnsafe(set);
				}

				// Token: 0x060096B4 RID: 38580 RVA: 0x00202E98 File Offset: 0x00201098
				public ProgramSetBuilder<fixedWidthMatches> fixedWidthMatches(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fixedWidthMatches)
					{
						string text = "set";
						string text2 = "expected program set for symbol fixedWidthMatches but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fixedWidthMatches>.CreateUnsafe(set);
				}

				// Token: 0x060096B5 RID: 38581 RVA: 0x00202EF0 File Offset: 0x002010F0
				public ProgramSetBuilder<gen_Concat> gen_Concat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_Concat)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_Concat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_Concat>.CreateUnsafe(set);
				}

				// Token: 0x060096B6 RID: 38582 RVA: 0x00202F48 File Offset: 0x00201148
				public ProgramSetBuilder<gen_LookAround> gen_LookAround(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_LookAround)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_LookAround but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAround>.CreateUnsafe(set);
				}

				// Token: 0x060096B7 RID: 38583 RVA: 0x00202FA0 File Offset: 0x002011A0
				public ProgramSetBuilder<gen_LookAroundField> gen_LookAroundField(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_LookAroundField)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_LookAroundField but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.gen_LookAroundField>.CreateUnsafe(set);
				}

				// Token: 0x060096B8 RID: 38584 RVA: 0x00202FF8 File Offset: 0x002011F8
				public ProgramSetBuilder<delimiterStart> delimiterStart(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiterStart)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiterStart but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterStart>.CreateUnsafe(set);
				}

				// Token: 0x060096B9 RID: 38585 RVA: 0x00203050 File Offset: 0x00201250
				public ProgramSetBuilder<delimiterEnd> delimiterEnd(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiterEnd)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiterEnd but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterEnd>.CreateUnsafe(set);
				}

				// Token: 0x060096BA RID: 38586 RVA: 0x002030A8 File Offset: 0x002012A8
				public ProgramSetBuilder<includeDelimiters> includeDelimiters(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.includeDelimiters)
					{
						string text = "set";
						string text2 = "expected program set for symbol includeDelimiters but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.includeDelimiters>.CreateUnsafe(set);
				}

				// Token: 0x060096BB RID: 38587 RVA: 0x00203100 File Offset: 0x00201300
				public ProgramSetBuilder<fillStrategy> fillStrategy(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fillStrategy)
					{
						string text = "set";
						string text2 = "expected program set for symbol fillStrategy but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fillStrategy>.CreateUnsafe(set);
				}

				// Token: 0x060096BC RID: 38588 RVA: 0x00203158 File Offset: 0x00201358
				public ProgramSetBuilder<ignoreIndexes> ignoreIndexes(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.ignoreIndexes)
					{
						string text = "set";
						string text2 = "expected program set for symbol ignoreIndexes but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.ignoreIndexes>.CreateUnsafe(set);
				}

				// Token: 0x060096BD RID: 38589 RVA: 0x002031B0 File Offset: 0x002013B0
				public ProgramSetBuilder<fieldStartPositions> fieldStartPositions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fieldStartPositions)
					{
						string text = "set";
						string text2 = "expected program set for symbol fieldStartPositions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fieldStartPositions>.CreateUnsafe(set);
				}

				// Token: 0x060096BE RID: 38590 RVA: 0x00203208 File Offset: 0x00201408
				public ProgramSetBuilder<delimiterPositions> delimiterPositions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiterPositions)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiterPositions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiterPositions>.CreateUnsafe(set);
				}

				// Token: 0x060096BF RID: 38591 RVA: 0x00203260 File Offset: 0x00201460
				public ProgramSetBuilder<fregex> fregex(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fregex)
					{
						string text = "set";
						string text2 = "expected program set for symbol fregex but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.fregex>.CreateUnsafe(set);
				}

				// Token: 0x060096C0 RID: 38592 RVA: 0x002032B8 File Offset: 0x002014B8
				public ProgramSetBuilder<s> s(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.s)
					{
						string text = "set";
						string text2 = "expected program set for symbol s but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.s>.CreateUnsafe(set);
				}

				// Token: 0x060096C1 RID: 38593 RVA: 0x00203310 File Offset: 0x00201510
				public ProgramSetBuilder<a> a(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.a)
					{
						string text = "set";
						string text2 = "expected program set for symbol a but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.a>.CreateUnsafe(set);
				}

				// Token: 0x060096C2 RID: 38594 RVA: 0x00203368 File Offset: 0x00201568
				public ProgramSetBuilder<numSplits> numSplits(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numSplits)
					{
						string text = "set";
						string text2 = "expected program set for symbol numSplits but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.numSplits>.CreateUnsafe(set);
				}

				// Token: 0x060096C3 RID: 38595 RVA: 0x002033C0 File Offset: 0x002015C0
				public ProgramSetBuilder<regex> regex(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regex)
					{
						string text = "set";
						string text2 = "expected program set for symbol regex but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.regex>.CreateUnsafe(set);
				}

				// Token: 0x060096C4 RID: 38596 RVA: 0x00203418 File Offset: 0x00201618
				public ProgramSetBuilder<obj> obj(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.obj)
					{
						string text = "set";
						string text2 = "expected program set for symbol obj but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.obj>.CreateUnsafe(set);
				}

				// Token: 0x060096C5 RID: 38597 RVA: 0x00203470 File Offset: 0x00201670
				public ProgramSetBuilder<delimiter> delimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.delimiter>.CreateUnsafe(set);
				}

				// Token: 0x060096C6 RID: 38598 RVA: 0x002034C8 File Offset: 0x002016C8
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x060096C7 RID: 38599 RVA: 0x00203520 File Offset: 0x00201720
				public ProgramSetBuilder<pair> pair(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pair)
					{
						string text = "set";
						string text2 = "expected program set for symbol pair but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.pair>.CreateUnsafe(set);
				}

				// Token: 0x060096C8 RID: 38600 RVA: 0x00203578 File Offset: 0x00201778
				public ProgramSetBuilder<item1> item1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.item1)
					{
						string text = "set";
						string text2 = "expected program set for symbol item1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes.item1>.CreateUnsafe(set);
				}

				// Token: 0x060096C9 RID: 38601 RVA: 0x002035D0 File Offset: 0x002017D0
				public ProgramSetBuilder<_LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x060096CA RID: 38602 RVA: 0x00203628 File Offset: 0x00201828
				public ProgramSetBuilder<_LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x060096CB RID: 38603 RVA: 0x00203680 File Offset: 0x00201880
				public ProgramSetBuilder<_LetB2> _LetB2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB2)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB2>.CreateUnsafe(set);
				}

				// Token: 0x060096CC RID: 38604 RVA: 0x002036D8 File Offset: 0x002018D8
				public ProgramSetBuilder<_LetB3> _LetB3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB3)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes._LetB3>.CreateUnsafe(set);
				}

				// Token: 0x04003D47 RID: 15687
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
