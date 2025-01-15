using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build
{
	// Token: 0x02000B3B RID: 2875
	public class GrammarBuilders
	{
		// Token: 0x060047B5 RID: 18357 RVA: 0x000E4722 File Offset: 0x000E2922
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x000E474E File Offset: 0x000E294E
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x000E475B File Offset: 0x000E295B
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x000E4768 File Offset: 0x000E2968
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x000E4775 File Offset: 0x000E2975
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x000E4782 File Offset: 0x000E2982
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x000E478A File Offset: 0x000E298A
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x000E4793 File Offset: 0x000E2993
		// (set) Token: 0x060047BD RID: 18365 RVA: 0x000E479B File Offset: 0x000E299B
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x060047BE RID: 18366 RVA: 0x000E47A4 File Offset: 0x000E29A4
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

		// Token: 0x040020DB RID: 8411
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x040020DC RID: 8412
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x040020DD RID: 8413
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x040020DE RID: 8414
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x040020DF RID: 8415
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000B3C RID: 2876
		public class GrammarSymbols
		{
			// Token: 0x17000CE6 RID: 3302
			// (get) Token: 0x060047C0 RID: 18368 RVA: 0x000E484F File Offset: 0x000E2A4F
			// (set) Token: 0x060047C1 RID: 18369 RVA: 0x000E4857 File Offset: 0x000E2A57
			public Symbol v { get; private set; }

			// Token: 0x17000CE7 RID: 3303
			// (get) Token: 0x060047C2 RID: 18370 RVA: 0x000E4860 File Offset: 0x000E2A60
			// (set) Token: 0x060047C3 RID: 18371 RVA: 0x000E4868 File Offset: 0x000E2A68
			public Symbol path { get; private set; }

			// Token: 0x17000CE8 RID: 3304
			// (get) Token: 0x060047C4 RID: 18372 RVA: 0x000E4871 File Offset: 0x000E2A71
			// (set) Token: 0x060047C5 RID: 18373 RVA: 0x000E4879 File Offset: 0x000E2A79
			public Symbol id { get; private set; }

			// Token: 0x17000CE9 RID: 3305
			// (get) Token: 0x060047C6 RID: 18374 RVA: 0x000E4882 File Offset: 0x000E2A82
			// (set) Token: 0x060047C7 RID: 18375 RVA: 0x000E488A File Offset: 0x000E2A8A
			public Symbol output { get; private set; }

			// Token: 0x17000CEA RID: 3306
			// (get) Token: 0x060047C8 RID: 18376 RVA: 0x000E4893 File Offset: 0x000E2A93
			// (set) Token: 0x060047C9 RID: 18377 RVA: 0x000E489B File Offset: 0x000E2A9B
			public Symbol @struct { get; private set; }

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x060047CA RID: 18378 RVA: 0x000E48A4 File Offset: 0x000E2AA4
			// (set) Token: 0x060047CB RID: 18379 RVA: 0x000E48AC File Offset: 0x000E2AAC
			public Symbol structBodyRec { get; private set; }

			// Token: 0x17000CEC RID: 3308
			// (get) Token: 0x060047CC RID: 18380 RVA: 0x000E48B5 File Offset: 0x000E2AB5
			// (set) Token: 0x060047CD RID: 18381 RVA: 0x000E48BD File Offset: 0x000E2ABD
			public Symbol sequence { get; private set; }

			// Token: 0x17000CED RID: 3309
			// (get) Token: 0x060047CE RID: 18382 RVA: 0x000E48C6 File Offset: 0x000E2AC6
			// (set) Token: 0x060047CF RID: 18383 RVA: 0x000E48CE File Offset: 0x000E2ACE
			public Symbol sequenceBody { get; private set; }

			// Token: 0x17000CEE RID: 3310
			// (get) Token: 0x060047D0 RID: 18384 RVA: 0x000E48D7 File Offset: 0x000E2AD7
			// (set) Token: 0x060047D1 RID: 18385 RVA: 0x000E48DF File Offset: 0x000E2ADF
			public Symbol x { get; private set; }

			// Token: 0x17000CEF RID: 3311
			// (get) Token: 0x060047D2 RID: 18386 RVA: 0x000E48E8 File Offset: 0x000E2AE8
			// (set) Token: 0x060047D3 RID: 18387 RVA: 0x000E48F0 File Offset: 0x000E2AF0
			public Symbol wrapStruct { get; private set; }

			// Token: 0x17000CF0 RID: 3312
			// (get) Token: 0x060047D4 RID: 18388 RVA: 0x000E48F9 File Offset: 0x000E2AF9
			// (set) Token: 0x060047D5 RID: 18389 RVA: 0x000E4901 File Offset: 0x000E2B01
			public Symbol selectSequence { get; private set; }

			// Token: 0x17000CF1 RID: 3313
			// (get) Token: 0x060047D6 RID: 18390 RVA: 0x000E490A File Offset: 0x000E2B0A
			// (set) Token: 0x060047D7 RID: 18391 RVA: 0x000E4912 File Offset: 0x000E2B12
			public Symbol selectRegion { get; private set; }

			// Token: 0x17000CF2 RID: 3314
			// (get) Token: 0x060047D8 RID: 18392 RVA: 0x000E491B File Offset: 0x000E2B1B
			// (set) Token: 0x060047D9 RID: 18393 RVA: 0x000E4923 File Offset: 0x000E2B23
			public Symbol _LFun0 { get; private set; }

			// Token: 0x060047DA RID: 18394 RVA: 0x000E492C File Offset: 0x000E2B2C
			public GrammarSymbols(Grammar grammar)
			{
				this.v = grammar.Symbol("v");
				this.path = grammar.Symbol("path");
				this.id = grammar.Symbol("id");
				this.output = grammar.Symbol("output");
				this.@struct = grammar.Symbol("struct");
				this.structBodyRec = grammar.Symbol("structBodyRec");
				this.sequence = grammar.Symbol("sequence");
				this.sequenceBody = grammar.Symbol("sequenceBody");
				this.x = grammar.Symbol("x");
				this.wrapStruct = grammar.Symbol("wrapStruct");
				this.selectSequence = grammar.Symbol("selectSequence");
				this.selectRegion = grammar.Symbol("selectRegion");
				this._LFun0 = grammar.Symbol("_LFun0");
			}
		}

		// Token: 0x02000B3D RID: 2877
		public class GrammarRules
		{
			// Token: 0x17000CF3 RID: 3315
			// (get) Token: 0x060047DB RID: 18395 RVA: 0x000E4A1C File Offset: 0x000E2C1C
			// (set) Token: 0x060047DC RID: 18396 RVA: 0x000E4A24 File Offset: 0x000E2C24
			public BlackBoxRule Struct { get; private set; }

			// Token: 0x17000CF4 RID: 3316
			// (get) Token: 0x060047DD RID: 18397 RVA: 0x000E4A2D File Offset: 0x000E2C2D
			// (set) Token: 0x060047DE RID: 18398 RVA: 0x000E4A35 File Offset: 0x000E2C35
			public BlackBoxRule Field { get; private set; }

			// Token: 0x17000CF5 RID: 3317
			// (get) Token: 0x060047DF RID: 18399 RVA: 0x000E4A3E File Offset: 0x000E2C3E
			// (set) Token: 0x060047E0 RID: 18400 RVA: 0x000E4A46 File Offset: 0x000E2C46
			public BlackBoxRule Concat { get; private set; }

			// Token: 0x17000CF6 RID: 3318
			// (get) Token: 0x060047E1 RID: 18401 RVA: 0x000E4A4F File Offset: 0x000E2C4F
			// (set) Token: 0x060047E2 RID: 18402 RVA: 0x000E4A57 File Offset: 0x000E2C57
			public BlackBoxRule ToList { get; private set; }

			// Token: 0x17000CF7 RID: 3319
			// (get) Token: 0x060047E3 RID: 18403 RVA: 0x000E4A60 File Offset: 0x000E2C60
			// (set) Token: 0x060047E4 RID: 18404 RVA: 0x000E4A68 File Offset: 0x000E2C68
			public BlackBoxRule Empty { get; private set; }

			// Token: 0x17000CF8 RID: 3320
			// (get) Token: 0x060047E5 RID: 18405 RVA: 0x000E4A71 File Offset: 0x000E2C71
			// (set) Token: 0x060047E6 RID: 18406 RVA: 0x000E4A79 File Offset: 0x000E2C79
			public BlackBoxRule Sequence { get; private set; }

			// Token: 0x17000CF9 RID: 3321
			// (get) Token: 0x060047E7 RID: 18407 RVA: 0x000E4A82 File Offset: 0x000E2C82
			// (set) Token: 0x060047E8 RID: 18408 RVA: 0x000E4A8A File Offset: 0x000E2C8A
			public BlackBoxRule DummySequence { get; private set; }

			// Token: 0x17000CFA RID: 3322
			// (get) Token: 0x060047E9 RID: 18409 RVA: 0x000E4A93 File Offset: 0x000E2C93
			// (set) Token: 0x060047EA RID: 18410 RVA: 0x000E4A9B File Offset: 0x000E2C9B
			public BlackBoxRule SelectSequence { get; private set; }

			// Token: 0x17000CFB RID: 3323
			// (get) Token: 0x060047EB RID: 18411 RVA: 0x000E4AA4 File Offset: 0x000E2CA4
			// (set) Token: 0x060047EC RID: 18412 RVA: 0x000E4AAC File Offset: 0x000E2CAC
			public BlackBoxRule SelectRegion { get; private set; }

			// Token: 0x17000CFC RID: 3324
			// (get) Token: 0x060047ED RID: 18413 RVA: 0x000E4AB5 File Offset: 0x000E2CB5
			// (set) Token: 0x060047EE RID: 18414 RVA: 0x000E4ABD File Offset: 0x000E2CBD
			public ConceptRule SequenceBody { get; private set; }

			// Token: 0x17000CFD RID: 3325
			// (get) Token: 0x060047EF RID: 18415 RVA: 0x000E4AC6 File Offset: 0x000E2CC6
			// (set) Token: 0x060047F0 RID: 18416 RVA: 0x000E4ACE File Offset: 0x000E2CCE
			public LetRule WrapStructLet { get; private set; }

			// Token: 0x060047F1 RID: 18417 RVA: 0x000E4AD8 File Offset: 0x000E2CD8
			public GrammarRules(Grammar grammar)
			{
				this.Struct = (BlackBoxRule)grammar.Rule("Struct");
				this.Field = (BlackBoxRule)grammar.Rule("Field");
				this.Concat = (BlackBoxRule)grammar.Rule("Concat");
				this.ToList = (BlackBoxRule)grammar.Rule("ToList");
				this.Empty = (BlackBoxRule)grammar.Rule("Empty");
				this.Sequence = (BlackBoxRule)grammar.Rule("Sequence");
				this.DummySequence = (BlackBoxRule)grammar.Rule("DummySequence");
				this.SelectSequence = (BlackBoxRule)grammar.Rule("SelectSequence");
				this.SelectRegion = (BlackBoxRule)grammar.Rule("SelectRegion");
				this.SequenceBody = (ConceptRule)grammar.Rule("SequenceBody");
				this.WrapStructLet = (LetRule)grammar.Rule("WrapStructLet");
			}
		}

		// Token: 0x02000B3E RID: 2878
		public class GrammarUnnamedConversions
		{
			// Token: 0x17000CFE RID: 3326
			// (get) Token: 0x060047F2 RID: 18418 RVA: 0x000E4BDD File Offset: 0x000E2DDD
			// (set) Token: 0x060047F3 RID: 18419 RVA: 0x000E4BE5 File Offset: 0x000E2DE5
			public ConversionRule output_struct { get; private set; }

			// Token: 0x17000CFF RID: 3327
			// (get) Token: 0x060047F4 RID: 18420 RVA: 0x000E4BEE File Offset: 0x000E2DEE
			// (set) Token: 0x060047F5 RID: 18421 RVA: 0x000E4BF6 File Offset: 0x000E2DF6
			public ConversionRule output_sequence { get; private set; }

			// Token: 0x060047F6 RID: 18422 RVA: 0x000E4BFF File Offset: 0x000E2DFF
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.output_struct = (ConversionRule)grammar.Rule("~convert_output_struct");
				this.output_sequence = (ConversionRule)grammar.Rule("~convert_output_sequence");
			}
		}

		// Token: 0x02000B3F RID: 2879
		public class GrammarHoles
		{
			// Token: 0x17000D00 RID: 3328
			// (get) Token: 0x060047F7 RID: 18423 RVA: 0x000E4C33 File Offset: 0x000E2E33
			// (set) Token: 0x060047F8 RID: 18424 RVA: 0x000E4C3B File Offset: 0x000E2E3B
			public Hole v { get; private set; }

			// Token: 0x17000D01 RID: 3329
			// (get) Token: 0x060047F9 RID: 18425 RVA: 0x000E4C44 File Offset: 0x000E2E44
			// (set) Token: 0x060047FA RID: 18426 RVA: 0x000E4C4C File Offset: 0x000E2E4C
			public Hole path { get; private set; }

			// Token: 0x17000D02 RID: 3330
			// (get) Token: 0x060047FB RID: 18427 RVA: 0x000E4C55 File Offset: 0x000E2E55
			// (set) Token: 0x060047FC RID: 18428 RVA: 0x000E4C5D File Offset: 0x000E2E5D
			public Hole id { get; private set; }

			// Token: 0x17000D03 RID: 3331
			// (get) Token: 0x060047FD RID: 18429 RVA: 0x000E4C66 File Offset: 0x000E2E66
			// (set) Token: 0x060047FE RID: 18430 RVA: 0x000E4C6E File Offset: 0x000E2E6E
			public Hole output { get; private set; }

			// Token: 0x17000D04 RID: 3332
			// (get) Token: 0x060047FF RID: 18431 RVA: 0x000E4C77 File Offset: 0x000E2E77
			// (set) Token: 0x06004800 RID: 18432 RVA: 0x000E4C7F File Offset: 0x000E2E7F
			public Hole @struct { get; private set; }

			// Token: 0x17000D05 RID: 3333
			// (get) Token: 0x06004801 RID: 18433 RVA: 0x000E4C88 File Offset: 0x000E2E88
			// (set) Token: 0x06004802 RID: 18434 RVA: 0x000E4C90 File Offset: 0x000E2E90
			public Hole structBodyRec { get; private set; }

			// Token: 0x17000D06 RID: 3334
			// (get) Token: 0x06004803 RID: 18435 RVA: 0x000E4C99 File Offset: 0x000E2E99
			// (set) Token: 0x06004804 RID: 18436 RVA: 0x000E4CA1 File Offset: 0x000E2EA1
			public Hole sequence { get; private set; }

			// Token: 0x17000D07 RID: 3335
			// (get) Token: 0x06004805 RID: 18437 RVA: 0x000E4CAA File Offset: 0x000E2EAA
			// (set) Token: 0x06004806 RID: 18438 RVA: 0x000E4CB2 File Offset: 0x000E2EB2
			public Hole sequenceBody { get; private set; }

			// Token: 0x17000D08 RID: 3336
			// (get) Token: 0x06004807 RID: 18439 RVA: 0x000E4CBB File Offset: 0x000E2EBB
			// (set) Token: 0x06004808 RID: 18440 RVA: 0x000E4CC3 File Offset: 0x000E2EC3
			public Hole x { get; private set; }

			// Token: 0x17000D09 RID: 3337
			// (get) Token: 0x06004809 RID: 18441 RVA: 0x000E4CCC File Offset: 0x000E2ECC
			// (set) Token: 0x0600480A RID: 18442 RVA: 0x000E4CD4 File Offset: 0x000E2ED4
			public Hole wrapStruct { get; private set; }

			// Token: 0x17000D0A RID: 3338
			// (get) Token: 0x0600480B RID: 18443 RVA: 0x000E4CDD File Offset: 0x000E2EDD
			// (set) Token: 0x0600480C RID: 18444 RVA: 0x000E4CE5 File Offset: 0x000E2EE5
			public Hole selectSequence { get; private set; }

			// Token: 0x17000D0B RID: 3339
			// (get) Token: 0x0600480D RID: 18445 RVA: 0x000E4CEE File Offset: 0x000E2EEE
			// (set) Token: 0x0600480E RID: 18446 RVA: 0x000E4CF6 File Offset: 0x000E2EF6
			public Hole selectRegion { get; private set; }

			// Token: 0x17000D0C RID: 3340
			// (get) Token: 0x0600480F RID: 18447 RVA: 0x000E4CFF File Offset: 0x000E2EFF
			// (set) Token: 0x06004810 RID: 18448 RVA: 0x000E4D07 File Offset: 0x000E2F07
			public Hole _LFun0 { get; private set; }

			// Token: 0x06004811 RID: 18449 RVA: 0x000E4D10 File Offset: 0x000E2F10
			public GrammarHoles(GrammarBuilders builders)
			{
				this.v = new Hole(builders.Symbol.v, null);
				this.path = new Hole(builders.Symbol.path, null);
				this.id = new Hole(builders.Symbol.id, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.@struct = new Hole(builders.Symbol.@struct, null);
				this.structBodyRec = new Hole(builders.Symbol.structBodyRec, null);
				this.sequence = new Hole(builders.Symbol.sequence, null);
				this.sequenceBody = new Hole(builders.Symbol.sequenceBody, null);
				this.x = new Hole(builders.Symbol.x, null);
				this.wrapStruct = new Hole(builders.Symbol.wrapStruct, null);
				this.selectSequence = new Hole(builders.Symbol.selectSequence, null);
				this.selectRegion = new Hole(builders.Symbol.selectRegion, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
			}
		}

		// Token: 0x02000B40 RID: 2880
		public class Nodes
		{
			// Token: 0x06004812 RID: 18450 RVA: 0x000E4E50 File Offset: 0x000E3050
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

			// Token: 0x17000D0D RID: 3341
			// (get) Token: 0x06004813 RID: 18451 RVA: 0x000E4F33 File Offset: 0x000E3133
			// (set) Token: 0x06004814 RID: 18452 RVA: 0x000E4F3B File Offset: 0x000E313B
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17000D0E RID: 3342
			// (get) Token: 0x06004815 RID: 18453 RVA: 0x000E4F44 File Offset: 0x000E3144
			// (set) Token: 0x06004816 RID: 18454 RVA: 0x000E4F4C File Offset: 0x000E314C
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17000D0F RID: 3343
			// (get) Token: 0x06004817 RID: 18455 RVA: 0x000E4F55 File Offset: 0x000E3155
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17000D10 RID: 3344
			// (get) Token: 0x06004818 RID: 18456 RVA: 0x000E4F62 File Offset: 0x000E3162
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17000D11 RID: 3345
			// (get) Token: 0x06004819 RID: 18457 RVA: 0x000E4F6F File Offset: 0x000E316F
			// (set) Token: 0x0600481A RID: 18458 RVA: 0x000E4F77 File Offset: 0x000E3177
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17000D12 RID: 3346
			// (get) Token: 0x0600481B RID: 18459 RVA: 0x000E4F80 File Offset: 0x000E3180
			// (set) Token: 0x0600481C RID: 18460 RVA: 0x000E4F88 File Offset: 0x000E3188
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17000D13 RID: 3347
			// (get) Token: 0x0600481D RID: 18461 RVA: 0x000E4F91 File Offset: 0x000E3191
			// (set) Token: 0x0600481E RID: 18462 RVA: 0x000E4F99 File Offset: 0x000E3199
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17000D14 RID: 3348
			// (get) Token: 0x0600481F RID: 18463 RVA: 0x000E4FA2 File Offset: 0x000E31A2
			// (set) Token: 0x06004820 RID: 18464 RVA: 0x000E4FAA File Offset: 0x000E31AA
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17000D15 RID: 3349
			// (get) Token: 0x06004821 RID: 18465 RVA: 0x000E4FB3 File Offset: 0x000E31B3
			// (set) Token: 0x06004822 RID: 18466 RVA: 0x000E4FBB File Offset: 0x000E31BB
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17000D16 RID: 3350
			// (get) Token: 0x06004823 RID: 18467 RVA: 0x000E4FC4 File Offset: 0x000E31C4
			// (set) Token: 0x06004824 RID: 18468 RVA: 0x000E4FCC File Offset: 0x000E31CC
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17000D17 RID: 3351
			// (get) Token: 0x06004825 RID: 18469 RVA: 0x000E4FD5 File Offset: 0x000E31D5
			// (set) Token: 0x06004826 RID: 18470 RVA: 0x000E4FDD File Offset: 0x000E31DD
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x0400210B RID: 8459
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x0400210C RID: 8460
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000B41 RID: 2881
			public class NodeRules
			{
				// Token: 0x06004827 RID: 18471 RVA: 0x000E4FE6 File Offset: 0x000E31E6
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004828 RID: 18472 RVA: 0x000E4FF5 File Offset: 0x000E31F5
				public path path(JPath value)
				{
					return new path(this._builders, value);
				}

				// Token: 0x06004829 RID: 18473 RVA: 0x000E5003 File Offset: 0x000E3203
				public id id(string value)
				{
					return new id(this._builders, value);
				}

				// Token: 0x0600482A RID: 18474 RVA: 0x000E5011 File Offset: 0x000E3211
				public @struct Struct(v value0, structBodyRec value1)
				{
					return new Struct(this._builders, value0, value1);
				}

				// Token: 0x0600482B RID: 18475 RVA: 0x000E5025 File Offset: 0x000E3225
				public @struct Field(v value0, id value1, selectRegion value2)
				{
					return new Field(this._builders, value0, value1, value2);
				}

				// Token: 0x0600482C RID: 18476 RVA: 0x000E503A File Offset: 0x000E323A
				public structBodyRec Concat(output value0, structBodyRec value1)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat(this._builders, value0, value1);
				}

				// Token: 0x0600482D RID: 18477 RVA: 0x000E504E File Offset: 0x000E324E
				public structBodyRec ToList(output value0)
				{
					return new ToList(this._builders, value0);
				}

				// Token: 0x0600482E RID: 18478 RVA: 0x000E5061 File Offset: 0x000E3261
				public structBodyRec Empty()
				{
					return new Empty(this._builders);
				}

				// Token: 0x0600482F RID: 18479 RVA: 0x000E5073 File Offset: 0x000E3273
				public sequence Sequence(id value0, selectSequence value1)
				{
					return new Sequence(this._builders, value0, value1);
				}

				// Token: 0x06004830 RID: 18480 RVA: 0x000E5087 File Offset: 0x000E3287
				public sequence DummySequence(sequenceBody value0)
				{
					return new DummySequence(this._builders, value0);
				}

				// Token: 0x06004831 RID: 18481 RVA: 0x000E509A File Offset: 0x000E329A
				public selectSequence SelectSequence(v value0, path value1)
				{
					return new SelectSequence(this._builders, value0, value1);
				}

				// Token: 0x06004832 RID: 18482 RVA: 0x000E50AE File Offset: 0x000E32AE
				public selectRegion SelectRegion(v value0, path value1)
				{
					return new SelectRegion(this._builders, value0, value1);
				}

				// Token: 0x06004833 RID: 18483 RVA: 0x000E50C2 File Offset: 0x000E32C2
				public sequenceBody SequenceBody(wrapStruct value0, selectSequence value1)
				{
					return new SequenceBody(this._builders, value0, value1);
				}

				// Token: 0x06004834 RID: 18484 RVA: 0x000E50D6 File Offset: 0x000E32D6
				public wrapStruct WrapStructLet(x value0, output value1)
				{
					return new WrapStructLet(this._builders, value0, value1);
				}

				// Token: 0x04002114 RID: 8468
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B42 RID: 2882
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06004835 RID: 18485 RVA: 0x000E50EA File Offset: 0x000E32EA
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004836 RID: 18486 RVA: 0x000E50F9 File Offset: 0x000E32F9
				public output output_struct(@struct value0)
				{
					return new output_struct(this._builders, value0);
				}

				// Token: 0x06004837 RID: 18487 RVA: 0x000E510C File Offset: 0x000E330C
				public output output_sequence(sequence value0)
				{
					return new output_sequence(this._builders, value0);
				}

				// Token: 0x04002115 RID: 8469
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B43 RID: 2883
			public class NodeVariables
			{
				// Token: 0x17000D18 RID: 3352
				// (get) Token: 0x06004838 RID: 18488 RVA: 0x000E511F File Offset: 0x000E331F
				// (set) Token: 0x06004839 RID: 18489 RVA: 0x000E5127 File Offset: 0x000E3327
				public v v { get; private set; }

				// Token: 0x17000D19 RID: 3353
				// (get) Token: 0x0600483A RID: 18490 RVA: 0x000E5130 File Offset: 0x000E3330
				// (set) Token: 0x0600483B RID: 18491 RVA: 0x000E5138 File Offset: 0x000E3338
				public x x { get; private set; }

				// Token: 0x0600483C RID: 18492 RVA: 0x000E5141 File Offset: 0x000E3341
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new v(builders);
					this.x = new x(builders);
				}
			}

			// Token: 0x02000B44 RID: 2884
			public class NodeHoles
			{
				// Token: 0x17000D1A RID: 3354
				// (get) Token: 0x0600483D RID: 18493 RVA: 0x000E5161 File Offset: 0x000E3361
				// (set) Token: 0x0600483E RID: 18494 RVA: 0x000E5169 File Offset: 0x000E3369
				public path path { get; private set; }

				// Token: 0x17000D1B RID: 3355
				// (get) Token: 0x0600483F RID: 18495 RVA: 0x000E5172 File Offset: 0x000E3372
				// (set) Token: 0x06004840 RID: 18496 RVA: 0x000E517A File Offset: 0x000E337A
				public id id { get; private set; }

				// Token: 0x17000D1C RID: 3356
				// (get) Token: 0x06004841 RID: 18497 RVA: 0x000E5183 File Offset: 0x000E3383
				// (set) Token: 0x06004842 RID: 18498 RVA: 0x000E518B File Offset: 0x000E338B
				public output output { get; private set; }

				// Token: 0x17000D1D RID: 3357
				// (get) Token: 0x06004843 RID: 18499 RVA: 0x000E5194 File Offset: 0x000E3394
				// (set) Token: 0x06004844 RID: 18500 RVA: 0x000E519C File Offset: 0x000E339C
				public @struct @struct { get; private set; }

				// Token: 0x17000D1E RID: 3358
				// (get) Token: 0x06004845 RID: 18501 RVA: 0x000E51A5 File Offset: 0x000E33A5
				// (set) Token: 0x06004846 RID: 18502 RVA: 0x000E51AD File Offset: 0x000E33AD
				public structBodyRec structBodyRec { get; private set; }

				// Token: 0x17000D1F RID: 3359
				// (get) Token: 0x06004847 RID: 18503 RVA: 0x000E51B6 File Offset: 0x000E33B6
				// (set) Token: 0x06004848 RID: 18504 RVA: 0x000E51BE File Offset: 0x000E33BE
				public sequence sequence { get; private set; }

				// Token: 0x17000D20 RID: 3360
				// (get) Token: 0x06004849 RID: 18505 RVA: 0x000E51C7 File Offset: 0x000E33C7
				// (set) Token: 0x0600484A RID: 18506 RVA: 0x000E51CF File Offset: 0x000E33CF
				public sequenceBody sequenceBody { get; private set; }

				// Token: 0x17000D21 RID: 3361
				// (get) Token: 0x0600484B RID: 18507 RVA: 0x000E51D8 File Offset: 0x000E33D8
				// (set) Token: 0x0600484C RID: 18508 RVA: 0x000E51E0 File Offset: 0x000E33E0
				public x x { get; private set; }

				// Token: 0x17000D22 RID: 3362
				// (get) Token: 0x0600484D RID: 18509 RVA: 0x000E51E9 File Offset: 0x000E33E9
				// (set) Token: 0x0600484E RID: 18510 RVA: 0x000E51F1 File Offset: 0x000E33F1
				public wrapStruct wrapStruct { get; private set; }

				// Token: 0x17000D23 RID: 3363
				// (get) Token: 0x0600484F RID: 18511 RVA: 0x000E51FA File Offset: 0x000E33FA
				// (set) Token: 0x06004850 RID: 18512 RVA: 0x000E5202 File Offset: 0x000E3402
				public selectSequence selectSequence { get; private set; }

				// Token: 0x17000D24 RID: 3364
				// (get) Token: 0x06004851 RID: 18513 RVA: 0x000E520B File Offset: 0x000E340B
				// (set) Token: 0x06004852 RID: 18514 RVA: 0x000E5213 File Offset: 0x000E3413
				public selectRegion selectRegion { get; private set; }

				// Token: 0x06004853 RID: 18515 RVA: 0x000E521C File Offset: 0x000E341C
				public NodeHoles(GrammarBuilders builders)
				{
					this.path = path.CreateHole(builders, null);
					this.id = id.CreateHole(builders, null);
					this.output = output.CreateHole(builders, null);
					this.@struct = @struct.CreateHole(builders, null);
					this.structBodyRec = structBodyRec.CreateHole(builders, null);
					this.sequence = sequence.CreateHole(builders, null);
					this.sequenceBody = sequenceBody.CreateHole(builders, null);
					this.x = x.CreateHole(builders, null);
					this.wrapStruct = wrapStruct.CreateHole(builders, null);
					this.selectSequence = selectSequence.CreateHole(builders, null);
					this.selectRegion = selectRegion.CreateHole(builders, null);
				}
			}

			// Token: 0x02000B45 RID: 2885
			public class NodeUnsafe
			{
				// Token: 0x06004854 RID: 18516 RVA: 0x000E52BE File Offset: 0x000E34BE
				public path path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path.CreateUnsafe(node);
				}

				// Token: 0x06004855 RID: 18517 RVA: 0x000E52C6 File Offset: 0x000E34C6
				public id id(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id.CreateUnsafe(node);
				}

				// Token: 0x06004856 RID: 18518 RVA: 0x000E52CE File Offset: 0x000E34CE
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x06004857 RID: 18519 RVA: 0x000E52D6 File Offset: 0x000E34D6
				public @struct @struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct.CreateUnsafe(node);
				}

				// Token: 0x06004858 RID: 18520 RVA: 0x000E52DE File Offset: 0x000E34DE
				public structBodyRec structBodyRec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec.CreateUnsafe(node);
				}

				// Token: 0x06004859 RID: 18521 RVA: 0x000E52E6 File Offset: 0x000E34E6
				public sequence sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence.CreateUnsafe(node);
				}

				// Token: 0x0600485A RID: 18522 RVA: 0x000E52EE File Offset: 0x000E34EE
				public sequenceBody sequenceBody(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody.CreateUnsafe(node);
				}

				// Token: 0x0600485B RID: 18523 RVA: 0x000E52F6 File Offset: 0x000E34F6
				public x x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x.CreateUnsafe(node);
				}

				// Token: 0x0600485C RID: 18524 RVA: 0x000E52FE File Offset: 0x000E34FE
				public wrapStruct wrapStruct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct.CreateUnsafe(node);
				}

				// Token: 0x0600485D RID: 18525 RVA: 0x000E5306 File Offset: 0x000E3506
				public selectSequence selectSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence.CreateUnsafe(node);
				}

				// Token: 0x0600485E RID: 18526 RVA: 0x000E530E File Offset: 0x000E350E
				public selectRegion selectRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion.CreateUnsafe(node);
				}
			}

			// Token: 0x02000B46 RID: 2886
			public class NodeCast
			{
				// Token: 0x06004860 RID: 18528 RVA: 0x000E5316 File Offset: 0x000E3516
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004861 RID: 18529 RVA: 0x000E5328 File Offset: 0x000E3528
				public path path(ProgramNode node)
				{
					path? path = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						string text = "node";
						string text2 = "expected node for symbol path but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return path.Value;
				}

				// Token: 0x06004862 RID: 18530 RVA: 0x000E537C File Offset: 0x000E357C
				public id id(ProgramNode node)
				{
					id? id = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id.CreateSafe(this._builders, node);
					if (id == null)
					{
						string text = "node";
						string text2 = "expected node for symbol id but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return id.Value;
				}

				// Token: 0x06004863 RID: 18531 RVA: 0x000E53D0 File Offset: 0x000E35D0
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06004864 RID: 18532 RVA: 0x000E5424 File Offset: 0x000E3624
				public @struct @struct(ProgramNode node)
				{
					@struct? @struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct.CreateSafe(this._builders, node);
					if (@struct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @struct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @struct.Value;
				}

				// Token: 0x06004865 RID: 18533 RVA: 0x000E5478 File Offset: 0x000E3678
				public structBodyRec structBodyRec(ProgramNode node)
				{
					structBodyRec? structBodyRec = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec.CreateSafe(this._builders, node);
					if (structBodyRec == null)
					{
						string text = "node";
						string text2 = "expected node for symbol structBodyRec but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return structBodyRec.Value;
				}

				// Token: 0x06004866 RID: 18534 RVA: 0x000E54CC File Offset: 0x000E36CC
				public sequence sequence(ProgramNode node)
				{
					sequence? sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequence.Value;
				}

				// Token: 0x06004867 RID: 18535 RVA: 0x000E5520 File Offset: 0x000E3720
				public sequenceBody sequenceBody(ProgramNode node)
				{
					sequenceBody? sequenceBody = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody.CreateSafe(this._builders, node);
					if (sequenceBody == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequenceBody but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceBody.Value;
				}

				// Token: 0x06004868 RID: 18536 RVA: 0x000E5574 File Offset: 0x000E3774
				public x x(ProgramNode node)
				{
					x? x = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						string text = "node";
						string text2 = "expected node for symbol x but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return x.Value;
				}

				// Token: 0x06004869 RID: 18537 RVA: 0x000E55C8 File Offset: 0x000E37C8
				public wrapStruct wrapStruct(ProgramNode node)
				{
					wrapStruct? wrapStruct = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct.CreateSafe(this._builders, node);
					if (wrapStruct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol wrapStruct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wrapStruct.Value;
				}

				// Token: 0x0600486A RID: 18538 RVA: 0x000E561C File Offset: 0x000E381C
				public selectSequence selectSequence(ProgramNode node)
				{
					selectSequence? selectSequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence.CreateSafe(this._builders, node);
					if (selectSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectSequence.Value;
				}

				// Token: 0x0600486B RID: 18539 RVA: 0x000E5670 File Offset: 0x000E3870
				public selectRegion selectRegion(ProgramNode node)
				{
					selectRegion? selectRegion = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion.CreateSafe(this._builders, node);
					if (selectRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectRegion.Value;
				}

				// Token: 0x04002123 RID: 8483
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B47 RID: 2887
			public class RuleCast
			{
				// Token: 0x0600486C RID: 18540 RVA: 0x000E56C1 File Offset: 0x000E38C1
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600486D RID: 18541 RVA: 0x000E56D0 File Offset: 0x000E38D0
				public output_struct output_struct(ProgramNode node)
				{
					output_struct? output_struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_struct.CreateSafe(this._builders, node);
					if (output_struct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output_struct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output_struct.Value;
				}

				// Token: 0x0600486E RID: 18542 RVA: 0x000E5724 File Offset: 0x000E3924
				public output_sequence output_sequence(ProgramNode node)
				{
					output_sequence? output_sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_sequence.CreateSafe(this._builders, node);
					if (output_sequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output_sequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output_sequence.Value;
				}

				// Token: 0x0600486F RID: 18543 RVA: 0x000E5778 File Offset: 0x000E3978
				public Struct Struct(ProgramNode node)
				{
					Struct? @struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Struct.CreateSafe(this._builders, node);
					if (@struct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Struct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @struct.Value;
				}

				// Token: 0x06004870 RID: 18544 RVA: 0x000E57CC File Offset: 0x000E39CC
				public Field Field(ProgramNode node)
				{
					Field? field = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Field.CreateSafe(this._builders, node);
					if (field == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Field but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return field.Value;
				}

				// Token: 0x06004871 RID: 18545 RVA: 0x000E5820 File Offset: 0x000E3A20
				public Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat Concat(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat? concat = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concat.Value;
				}

				// Token: 0x06004872 RID: 18546 RVA: 0x000E5874 File Offset: 0x000E3A74
				public ToList ToList(ProgramNode node)
				{
					ToList? toList = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.ToList.CreateSafe(this._builders, node);
					if (toList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toList.Value;
				}

				// Token: 0x06004873 RID: 18547 RVA: 0x000E58C8 File Offset: 0x000E3AC8
				public Empty Empty(ProgramNode node)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Empty but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return empty.Value;
				}

				// Token: 0x06004874 RID: 18548 RVA: 0x000E591C File Offset: 0x000E3B1C
				public Sequence Sequence(ProgramNode node)
				{
					Sequence? sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Sequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequence.Value;
				}

				// Token: 0x06004875 RID: 18549 RVA: 0x000E5970 File Offset: 0x000E3B70
				public DummySequence DummySequence(ProgramNode node)
				{
					DummySequence? dummySequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.DummySequence.CreateSafe(this._builders, node);
					if (dummySequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DummySequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dummySequence.Value;
				}

				// Token: 0x06004876 RID: 18550 RVA: 0x000E59C4 File Offset: 0x000E3BC4
				public SequenceBody SequenceBody(ProgramNode node)
				{
					SequenceBody? sequenceBody = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SequenceBody.CreateSafe(this._builders, node);
					if (sequenceBody == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SequenceBody but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceBody.Value;
				}

				// Token: 0x06004877 RID: 18551 RVA: 0x000E5A18 File Offset: 0x000E3C18
				public WrapStructLet WrapStructLet(ProgramNode node)
				{
					WrapStructLet? wrapStructLet = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.WrapStructLet.CreateSafe(this._builders, node);
					if (wrapStructLet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WrapStructLet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wrapStructLet.Value;
				}

				// Token: 0x06004878 RID: 18552 RVA: 0x000E5A6C File Offset: 0x000E3C6C
				public SelectSequence SelectSequence(ProgramNode node)
				{
					SelectSequence? selectSequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectSequence.CreateSafe(this._builders, node);
					if (selectSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectSequence.Value;
				}

				// Token: 0x06004879 RID: 18553 RVA: 0x000E5AC0 File Offset: 0x000E3CC0
				public SelectRegion SelectRegion(ProgramNode node)
				{
					SelectRegion? selectRegion = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectRegion.CreateSafe(this._builders, node);
					if (selectRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectRegion.Value;
				}

				// Token: 0x04002124 RID: 8484
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B48 RID: 2888
			public class NodeIs
			{
				// Token: 0x0600487A RID: 18554 RVA: 0x000E5B11 File Offset: 0x000E3D11
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600487B RID: 18555 RVA: 0x000E5B20 File Offset: 0x000E3D20
				public bool path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600487C RID: 18556 RVA: 0x000E5B44 File Offset: 0x000E3D44
				public bool path(ProgramNode node, out path value)
				{
					path? path = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						value = default(path);
						return false;
					}
					value = path.Value;
					return true;
				}

				// Token: 0x0600487D RID: 18557 RVA: 0x000E5B80 File Offset: 0x000E3D80
				public bool id(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600487E RID: 18558 RVA: 0x000E5BA4 File Offset: 0x000E3DA4
				public bool id(ProgramNode node, out id value)
				{
					id? id = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id.CreateSafe(this._builders, node);
					if (id == null)
					{
						value = default(id);
						return false;
					}
					value = id.Value;
					return true;
				}

				// Token: 0x0600487F RID: 18559 RVA: 0x000E5BE0 File Offset: 0x000E3DE0
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004880 RID: 18560 RVA: 0x000E5C04 File Offset: 0x000E3E04
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x06004881 RID: 18561 RVA: 0x000E5C40 File Offset: 0x000E3E40
				public bool @struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004882 RID: 18562 RVA: 0x000E5C64 File Offset: 0x000E3E64
				public bool @struct(ProgramNode node, out @struct value)
				{
					@struct? @struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct.CreateSafe(this._builders, node);
					if (@struct == null)
					{
						value = default(@struct);
						return false;
					}
					value = @struct.Value;
					return true;
				}

				// Token: 0x06004883 RID: 18563 RVA: 0x000E5CA0 File Offset: 0x000E3EA0
				public bool structBodyRec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004884 RID: 18564 RVA: 0x000E5CC4 File Offset: 0x000E3EC4
				public bool structBodyRec(ProgramNode node, out structBodyRec value)
				{
					structBodyRec? structBodyRec = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec.CreateSafe(this._builders, node);
					if (structBodyRec == null)
					{
						value = default(structBodyRec);
						return false;
					}
					value = structBodyRec.Value;
					return true;
				}

				// Token: 0x06004885 RID: 18565 RVA: 0x000E5D00 File Offset: 0x000E3F00
				public bool sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004886 RID: 18566 RVA: 0x000E5D24 File Offset: 0x000E3F24
				public bool sequence(ProgramNode node, out sequence value)
				{
					sequence? sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						value = default(sequence);
						return false;
					}
					value = sequence.Value;
					return true;
				}

				// Token: 0x06004887 RID: 18567 RVA: 0x000E5D60 File Offset: 0x000E3F60
				public bool sequenceBody(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004888 RID: 18568 RVA: 0x000E5D84 File Offset: 0x000E3F84
				public bool sequenceBody(ProgramNode node, out sequenceBody value)
				{
					sequenceBody? sequenceBody = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody.CreateSafe(this._builders, node);
					if (sequenceBody == null)
					{
						value = default(sequenceBody);
						return false;
					}
					value = sequenceBody.Value;
					return true;
				}

				// Token: 0x06004889 RID: 18569 RVA: 0x000E5DC0 File Offset: 0x000E3FC0
				public bool x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600488A RID: 18570 RVA: 0x000E5DE4 File Offset: 0x000E3FE4
				public bool x(ProgramNode node, out x value)
				{
					x? x = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						value = default(x);
						return false;
					}
					value = x.Value;
					return true;
				}

				// Token: 0x0600488B RID: 18571 RVA: 0x000E5E20 File Offset: 0x000E4020
				public bool wrapStruct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600488C RID: 18572 RVA: 0x000E5E44 File Offset: 0x000E4044
				public bool wrapStruct(ProgramNode node, out wrapStruct value)
				{
					wrapStruct? wrapStruct = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct.CreateSafe(this._builders, node);
					if (wrapStruct == null)
					{
						value = default(wrapStruct);
						return false;
					}
					value = wrapStruct.Value;
					return true;
				}

				// Token: 0x0600488D RID: 18573 RVA: 0x000E5E80 File Offset: 0x000E4080
				public bool selectSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600488E RID: 18574 RVA: 0x000E5EA4 File Offset: 0x000E40A4
				public bool selectSequence(ProgramNode node, out selectSequence value)
				{
					selectSequence? selectSequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence.CreateSafe(this._builders, node);
					if (selectSequence == null)
					{
						value = default(selectSequence);
						return false;
					}
					value = selectSequence.Value;
					return true;
				}

				// Token: 0x0600488F RID: 18575 RVA: 0x000E5EE0 File Offset: 0x000E40E0
				public bool selectRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004890 RID: 18576 RVA: 0x000E5F04 File Offset: 0x000E4104
				public bool selectRegion(ProgramNode node, out selectRegion value)
				{
					selectRegion? selectRegion = Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion.CreateSafe(this._builders, node);
					if (selectRegion == null)
					{
						value = default(selectRegion);
						return false;
					}
					value = selectRegion.Value;
					return true;
				}

				// Token: 0x04002125 RID: 8485
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B49 RID: 2889
			public class RuleIs
			{
				// Token: 0x06004891 RID: 18577 RVA: 0x000E5F3E File Offset: 0x000E413E
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004892 RID: 18578 RVA: 0x000E5F50 File Offset: 0x000E4150
				public bool output_struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_struct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004893 RID: 18579 RVA: 0x000E5F74 File Offset: 0x000E4174
				public bool output_struct(ProgramNode node, out output_struct value)
				{
					output_struct? output_struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_struct.CreateSafe(this._builders, node);
					if (output_struct == null)
					{
						value = default(output_struct);
						return false;
					}
					value = output_struct.Value;
					return true;
				}

				// Token: 0x06004894 RID: 18580 RVA: 0x000E5FB0 File Offset: 0x000E41B0
				public bool output_sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_sequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004895 RID: 18581 RVA: 0x000E5FD4 File Offset: 0x000E41D4
				public bool output_sequence(ProgramNode node, out output_sequence value)
				{
					output_sequence? output_sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_sequence.CreateSafe(this._builders, node);
					if (output_sequence == null)
					{
						value = default(output_sequence);
						return false;
					}
					value = output_sequence.Value;
					return true;
				}

				// Token: 0x06004896 RID: 18582 RVA: 0x000E6010 File Offset: 0x000E4210
				public bool Struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Struct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004897 RID: 18583 RVA: 0x000E6034 File Offset: 0x000E4234
				public bool Struct(ProgramNode node, out Struct value)
				{
					Struct? @struct = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Struct.CreateSafe(this._builders, node);
					if (@struct == null)
					{
						value = default(Struct);
						return false;
					}
					value = @struct.Value;
					return true;
				}

				// Token: 0x06004898 RID: 18584 RVA: 0x000E6070 File Offset: 0x000E4270
				public bool Field(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Field.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004899 RID: 18585 RVA: 0x000E6094 File Offset: 0x000E4294
				public bool Field(ProgramNode node, out Field value)
				{
					Field? field = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Field.CreateSafe(this._builders, node);
					if (field == null)
					{
						value = default(Field);
						return false;
					}
					value = field.Value;
					return true;
				}

				// Token: 0x0600489A RID: 18586 RVA: 0x000E60D0 File Offset: 0x000E42D0
				public bool Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600489B RID: 18587 RVA: 0x000E60F4 File Offset: 0x000E42F4
				public bool Concat(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat value)
				{
					Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat? concat = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat);
						return false;
					}
					value = concat.Value;
					return true;
				}

				// Token: 0x0600489C RID: 18588 RVA: 0x000E6130 File Offset: 0x000E4330
				public bool ToList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.ToList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600489D RID: 18589 RVA: 0x000E6154 File Offset: 0x000E4354
				public bool ToList(ProgramNode node, out ToList value)
				{
					ToList? toList = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.ToList.CreateSafe(this._builders, node);
					if (toList == null)
					{
						value = default(ToList);
						return false;
					}
					value = toList.Value;
					return true;
				}

				// Token: 0x0600489E RID: 18590 RVA: 0x000E6190 File Offset: 0x000E4390
				public bool Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600489F RID: 18591 RVA: 0x000E61B4 File Offset: 0x000E43B4
				public bool Empty(ProgramNode node, out Empty value)
				{
					Empty? empty = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
					if (empty == null)
					{
						value = default(Empty);
						return false;
					}
					value = empty.Value;
					return true;
				}

				// Token: 0x060048A0 RID: 18592 RVA: 0x000E61F0 File Offset: 0x000E43F0
				public bool Sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048A1 RID: 18593 RVA: 0x000E6214 File Offset: 0x000E4414
				public bool Sequence(ProgramNode node, out Sequence value)
				{
					Sequence? sequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
					if (sequence == null)
					{
						value = default(Sequence);
						return false;
					}
					value = sequence.Value;
					return true;
				}

				// Token: 0x060048A2 RID: 18594 RVA: 0x000E6250 File Offset: 0x000E4450
				public bool DummySequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.DummySequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048A3 RID: 18595 RVA: 0x000E6274 File Offset: 0x000E4474
				public bool DummySequence(ProgramNode node, out DummySequence value)
				{
					DummySequence? dummySequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.DummySequence.CreateSafe(this._builders, node);
					if (dummySequence == null)
					{
						value = default(DummySequence);
						return false;
					}
					value = dummySequence.Value;
					return true;
				}

				// Token: 0x060048A4 RID: 18596 RVA: 0x000E62B0 File Offset: 0x000E44B0
				public bool SequenceBody(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SequenceBody.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048A5 RID: 18597 RVA: 0x000E62D4 File Offset: 0x000E44D4
				public bool SequenceBody(ProgramNode node, out SequenceBody value)
				{
					SequenceBody? sequenceBody = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SequenceBody.CreateSafe(this._builders, node);
					if (sequenceBody == null)
					{
						value = default(SequenceBody);
						return false;
					}
					value = sequenceBody.Value;
					return true;
				}

				// Token: 0x060048A6 RID: 18598 RVA: 0x000E6310 File Offset: 0x000E4510
				public bool WrapStructLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.WrapStructLet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048A7 RID: 18599 RVA: 0x000E6334 File Offset: 0x000E4534
				public bool WrapStructLet(ProgramNode node, out WrapStructLet value)
				{
					WrapStructLet? wrapStructLet = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.WrapStructLet.CreateSafe(this._builders, node);
					if (wrapStructLet == null)
					{
						value = default(WrapStructLet);
						return false;
					}
					value = wrapStructLet.Value;
					return true;
				}

				// Token: 0x060048A8 RID: 18600 RVA: 0x000E6370 File Offset: 0x000E4570
				public bool SelectSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048A9 RID: 18601 RVA: 0x000E6394 File Offset: 0x000E4594
				public bool SelectSequence(ProgramNode node, out SelectSequence value)
				{
					SelectSequence? selectSequence = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectSequence.CreateSafe(this._builders, node);
					if (selectSequence == null)
					{
						value = default(SelectSequence);
						return false;
					}
					value = selectSequence.Value;
					return true;
				}

				// Token: 0x060048AA RID: 18602 RVA: 0x000E63D0 File Offset: 0x000E45D0
				public bool SelectRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060048AB RID: 18603 RVA: 0x000E63F4 File Offset: 0x000E45F4
				public bool SelectRegion(ProgramNode node, out SelectRegion value)
				{
					SelectRegion? selectRegion = Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectRegion.CreateSafe(this._builders, node);
					if (selectRegion == null)
					{
						value = default(SelectRegion);
						return false;
					}
					value = selectRegion.Value;
					return true;
				}

				// Token: 0x04002126 RID: 8486
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B4A RID: 2890
			public class NodeAs
			{
				// Token: 0x060048AC RID: 18604 RVA: 0x000E642E File Offset: 0x000E462E
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048AD RID: 18605 RVA: 0x000E643D File Offset: 0x000E463D
				public path? path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
				}

				// Token: 0x060048AE RID: 18606 RVA: 0x000E644B File Offset: 0x000E464B
				public id? id(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id.CreateSafe(this._builders, node);
				}

				// Token: 0x060048AF RID: 18607 RVA: 0x000E6459 File Offset: 0x000E4659
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B0 RID: 18608 RVA: 0x000E6467 File Offset: 0x000E4667
				public @struct? @struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B1 RID: 18609 RVA: 0x000E6475 File Offset: 0x000E4675
				public structBodyRec? structBodyRec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B2 RID: 18610 RVA: 0x000E6483 File Offset: 0x000E4683
				public sequence? sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B3 RID: 18611 RVA: 0x000E6491 File Offset: 0x000E4691
				public sequenceBody? sequenceBody(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B4 RID: 18612 RVA: 0x000E649F File Offset: 0x000E469F
				public x? x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B5 RID: 18613 RVA: 0x000E64AD File Offset: 0x000E46AD
				public wrapStruct? wrapStruct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B6 RID: 18614 RVA: 0x000E64BB File Offset: 0x000E46BB
				public selectSequence? selectSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048B7 RID: 18615 RVA: 0x000E64C9 File Offset: 0x000E46C9
				public selectRegion? selectRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x04002127 RID: 8487
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B4B RID: 2891
			public class RuleAs
			{
				// Token: 0x060048B8 RID: 18616 RVA: 0x000E64D7 File Offset: 0x000E46D7
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048B9 RID: 18617 RVA: 0x000E64E6 File Offset: 0x000E46E6
				public output_struct? output_struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_struct.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BA RID: 18618 RVA: 0x000E64F4 File Offset: 0x000E46F4
				public output_sequence? output_sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes.output_sequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BB RID: 18619 RVA: 0x000E6502 File Offset: 0x000E4702
				public Struct? Struct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Struct.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BC RID: 18620 RVA: 0x000E6510 File Offset: 0x000E4710
				public Field? Field(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Field.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BD RID: 18621 RVA: 0x000E651E File Offset: 0x000E471E
				public Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat? Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BE RID: 18622 RVA: 0x000E652C File Offset: 0x000E472C
				public ToList? ToList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.ToList.CreateSafe(this._builders, node);
				}

				// Token: 0x060048BF RID: 18623 RVA: 0x000E653A File Offset: 0x000E473A
				public Empty? Empty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Empty.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C0 RID: 18624 RVA: 0x000E6548 File Offset: 0x000E4748
				public Sequence? Sequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.Sequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C1 RID: 18625 RVA: 0x000E6556 File Offset: 0x000E4756
				public DummySequence? DummySequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.DummySequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C2 RID: 18626 RVA: 0x000E6564 File Offset: 0x000E4764
				public SequenceBody? SequenceBody(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SequenceBody.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C3 RID: 18627 RVA: 0x000E6572 File Offset: 0x000E4772
				public WrapStructLet? WrapStructLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.WrapStructLet.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C4 RID: 18628 RVA: 0x000E6580 File Offset: 0x000E4780
				public SelectSequence? SelectSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060048C5 RID: 18629 RVA: 0x000E658E File Offset: 0x000E478E
				public SelectRegion? SelectRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes.SelectRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x04002128 RID: 8488
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000B4D RID: 2893
		public class Sets
		{
			// Token: 0x060048C9 RID: 18633 RVA: 0x000E65B8 File Offset: 0x000E47B8
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17000D25 RID: 3365
			// (get) Token: 0x060048CA RID: 18634 RVA: 0x000E6607 File Offset: 0x000E4807
			// (set) Token: 0x060048CB RID: 18635 RVA: 0x000E660F File Offset: 0x000E480F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17000D26 RID: 3366
			// (get) Token: 0x060048CC RID: 18636 RVA: 0x000E6618 File Offset: 0x000E4818
			// (set) Token: 0x060048CD RID: 18637 RVA: 0x000E6620 File Offset: 0x000E4820
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17000D27 RID: 3367
			// (get) Token: 0x060048CE RID: 18638 RVA: 0x000E6629 File Offset: 0x000E4829
			// (set) Token: 0x060048CF RID: 18639 RVA: 0x000E6631 File Offset: 0x000E4831
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17000D28 RID: 3368
			// (get) Token: 0x060048D0 RID: 18640 RVA: 0x000E663A File Offset: 0x000E483A
			// (set) Token: 0x060048D1 RID: 18641 RVA: 0x000E6642 File Offset: 0x000E4842
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17000D29 RID: 3369
			// (get) Token: 0x060048D2 RID: 18642 RVA: 0x000E664B File Offset: 0x000E484B
			// (set) Token: 0x060048D3 RID: 18643 RVA: 0x000E6653 File Offset: 0x000E4853
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000B4E RID: 2894
			public class Joins
			{
				// Token: 0x060048D4 RID: 18644 RVA: 0x000E665C File Offset: 0x000E485C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048D5 RID: 18645 RVA: 0x000E666B File Offset: 0x000E486B
				public ProgramSetBuilder<@struct> Struct(ProgramSetBuilder<v> value0, ProgramSetBuilder<structBodyRec> value1)
				{
					return ProgramSetBuilder<@struct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Struct, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048D6 RID: 18646 RVA: 0x000E66AC File Offset: 0x000E48AC
				public ProgramSetBuilder<@struct> Field(ProgramSetBuilder<v> value0, ProgramSetBuilder<id> value1, ProgramSetBuilder<selectRegion> value2)
				{
					return ProgramSetBuilder<@struct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Field, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060048D7 RID: 18647 RVA: 0x000E6706 File Offset: 0x000E4906
				public ProgramSetBuilder<structBodyRec> Concat(ProgramSetBuilder<output> value0, ProgramSetBuilder<structBodyRec> value1)
				{
					return ProgramSetBuilder<structBodyRec>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048D8 RID: 18648 RVA: 0x000E6746 File Offset: 0x000E4946
				public ProgramSetBuilder<structBodyRec> ToList(ProgramSetBuilder<output> value0)
				{
					return ProgramSetBuilder<structBodyRec>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048D9 RID: 18649 RVA: 0x000E6777 File Offset: 0x000E4977
				public ProgramSetBuilder<structBodyRec> Empty()
				{
					return ProgramSetBuilder<structBodyRec>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Empty, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060048DA RID: 18650 RVA: 0x000E6798 File Offset: 0x000E4998
				public ProgramSetBuilder<sequence> Sequence(ProgramSetBuilder<id> value0, ProgramSetBuilder<selectSequence> value1)
				{
					return ProgramSetBuilder<sequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Sequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048DB RID: 18651 RVA: 0x000E67D8 File Offset: 0x000E49D8
				public ProgramSetBuilder<sequence> DummySequence(ProgramSetBuilder<sequenceBody> value0)
				{
					return ProgramSetBuilder<sequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DummySequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048DC RID: 18652 RVA: 0x000E6809 File Offset: 0x000E4A09
				public ProgramSetBuilder<selectSequence> SelectSequence(ProgramSetBuilder<v> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048DD RID: 18653 RVA: 0x000E6849 File Offset: 0x000E4A49
				public ProgramSetBuilder<selectRegion> SelectRegion(ProgramSetBuilder<v> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectRegion>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048DE RID: 18654 RVA: 0x000E6889 File Offset: 0x000E4A89
				public ProgramSetBuilder<sequenceBody> SequenceBody(ProgramSetBuilder<wrapStruct> value0, ProgramSetBuilder<selectSequence> value1)
				{
					return ProgramSetBuilder<sequenceBody>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SequenceBody, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048DF RID: 18655 RVA: 0x000E68C9 File Offset: 0x000E4AC9
				public ProgramSetBuilder<wrapStruct> WrapStructLet(ProgramSetBuilder<x> value0, ProgramSetBuilder<output> value1)
				{
					return ProgramSetBuilder<wrapStruct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WrapStructLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0400212F RID: 8495
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B4F RID: 2895
			public class ExplicitJoins
			{
				// Token: 0x060048E0 RID: 18656 RVA: 0x000E6909 File Offset: 0x000E4B09
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048E1 RID: 18657 RVA: 0x000E6918 File Offset: 0x000E4B18
				public JoinProgramSetBuilder<@struct> Struct(ProgramSetBuilder<v> value0, ProgramSetBuilder<structBodyRec> value1)
				{
					return JoinProgramSetBuilder<@struct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Struct, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048E2 RID: 18658 RVA: 0x000E6958 File Offset: 0x000E4B58
				public JoinProgramSetBuilder<@struct> Field(ProgramSetBuilder<v> value0, ProgramSetBuilder<id> value1, ProgramSetBuilder<selectRegion> value2)
				{
					return JoinProgramSetBuilder<@struct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Field, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060048E3 RID: 18659 RVA: 0x000E69B2 File Offset: 0x000E4BB2
				public JoinProgramSetBuilder<structBodyRec> Concat(ProgramSetBuilder<output> value0, ProgramSetBuilder<structBodyRec> value1)
				{
					return JoinProgramSetBuilder<structBodyRec>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048E4 RID: 18660 RVA: 0x000E69F2 File Offset: 0x000E4BF2
				public JoinProgramSetBuilder<structBodyRec> ToList(ProgramSetBuilder<output> value0)
				{
					return JoinProgramSetBuilder<structBodyRec>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048E5 RID: 18661 RVA: 0x000E6A23 File Offset: 0x000E4C23
				public JoinProgramSetBuilder<structBodyRec> Empty()
				{
					return JoinProgramSetBuilder<structBodyRec>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Empty, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060048E6 RID: 18662 RVA: 0x000E6A44 File Offset: 0x000E4C44
				public JoinProgramSetBuilder<sequence> Sequence(ProgramSetBuilder<id> value0, ProgramSetBuilder<selectSequence> value1)
				{
					return JoinProgramSetBuilder<sequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Sequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048E7 RID: 18663 RVA: 0x000E6A84 File Offset: 0x000E4C84
				public JoinProgramSetBuilder<sequence> DummySequence(ProgramSetBuilder<sequenceBody> value0)
				{
					return JoinProgramSetBuilder<sequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DummySequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048E8 RID: 18664 RVA: 0x000E6AB5 File Offset: 0x000E4CB5
				public JoinProgramSetBuilder<selectSequence> SelectSequence(ProgramSetBuilder<v> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048E9 RID: 18665 RVA: 0x000E6AF5 File Offset: 0x000E4CF5
				public JoinProgramSetBuilder<selectRegion> SelectRegion(ProgramSetBuilder<v> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectRegion>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048EA RID: 18666 RVA: 0x000E6B35 File Offset: 0x000E4D35
				public JoinProgramSetBuilder<sequenceBody> SequenceBody(ProgramSetBuilder<wrapStruct> value0, ProgramSetBuilder<selectSequence> value1)
				{
					return JoinProgramSetBuilder<sequenceBody>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SequenceBody, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060048EB RID: 18667 RVA: 0x000E6B75 File Offset: 0x000E4D75
				public JoinProgramSetBuilder<wrapStruct> WrapStructLet(ProgramSetBuilder<x> value0, ProgramSetBuilder<output> value1)
				{
					return JoinProgramSetBuilder<wrapStruct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WrapStructLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04002130 RID: 8496
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B50 RID: 2896
			public class JoinUnnamedConversions
			{
				// Token: 0x060048EC RID: 18668 RVA: 0x000E6BB5 File Offset: 0x000E4DB5
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048ED RID: 18669 RVA: 0x000E6BC4 File Offset: 0x000E4DC4
				public ProgramSetBuilder<output> output_struct(ProgramSetBuilder<@struct> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.output_struct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048EE RID: 18670 RVA: 0x000E6BF5 File Offset: 0x000E4DF5
				public ProgramSetBuilder<output> output_sequence(ProgramSetBuilder<sequence> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.output_sequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002131 RID: 8497
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B51 RID: 2897
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x060048EF RID: 18671 RVA: 0x000E6C26 File Offset: 0x000E4E26
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048F0 RID: 18672 RVA: 0x000E6C35 File Offset: 0x000E4E35
				public JoinProgramSetBuilder<output> output_struct(ProgramSetBuilder<@struct> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.output_struct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060048F1 RID: 18673 RVA: 0x000E6C66 File Offset: 0x000E4E66
				public JoinProgramSetBuilder<output> output_sequence(ProgramSetBuilder<sequence> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.output_sequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002132 RID: 8498
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000B52 RID: 2898
			public class Casts
			{
				// Token: 0x060048F2 RID: 18674 RVA: 0x000E6C97 File Offset: 0x000E4E97
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060048F3 RID: 18675 RVA: 0x000E6CA8 File Offset: 0x000E4EA8
				public ProgramSetBuilder<path> path(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.path)
					{
						string text = "set";
						string text2 = "expected program set for symbol path but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.path>.CreateUnsafe(set);
				}

				// Token: 0x060048F4 RID: 18676 RVA: 0x000E6D00 File Offset: 0x000E4F00
				public ProgramSetBuilder<id> id(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.id)
					{
						string text = "set";
						string text2 = "expected program set for symbol id but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.id>.CreateUnsafe(set);
				}

				// Token: 0x060048F5 RID: 18677 RVA: 0x000E6D58 File Offset: 0x000E4F58
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x060048F6 RID: 18678 RVA: 0x000E6DB0 File Offset: 0x000E4FB0
				public ProgramSetBuilder<@struct> @struct(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.@struct)
					{
						string text = "set";
						string text2 = "expected program set for symbol @struct but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.@struct>.CreateUnsafe(set);
				}

				// Token: 0x060048F7 RID: 18679 RVA: 0x000E6E08 File Offset: 0x000E5008
				public ProgramSetBuilder<structBodyRec> structBodyRec(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.structBodyRec)
					{
						string text = "set";
						string text2 = "expected program set for symbol structBodyRec but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.structBodyRec>.CreateUnsafe(set);
				}

				// Token: 0x060048F8 RID: 18680 RVA: 0x000E6E60 File Offset: 0x000E5060
				public ProgramSetBuilder<sequence> sequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol sequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequence>.CreateUnsafe(set);
				}

				// Token: 0x060048F9 RID: 18681 RVA: 0x000E6EB8 File Offset: 0x000E50B8
				public ProgramSetBuilder<sequenceBody> sequenceBody(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sequenceBody)
					{
						string text = "set";
						string text2 = "expected program set for symbol sequenceBody but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.sequenceBody>.CreateUnsafe(set);
				}

				// Token: 0x060048FA RID: 18682 RVA: 0x000E6F10 File Offset: 0x000E5110
				public ProgramSetBuilder<x> x(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.x)
					{
						string text = "set";
						string text2 = "expected program set for symbol x but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.x>.CreateUnsafe(set);
				}

				// Token: 0x060048FB RID: 18683 RVA: 0x000E6F68 File Offset: 0x000E5168
				public ProgramSetBuilder<wrapStruct> wrapStruct(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.wrapStruct)
					{
						string text = "set";
						string text2 = "expected program set for symbol wrapStruct but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.wrapStruct>.CreateUnsafe(set);
				}

				// Token: 0x060048FC RID: 18684 RVA: 0x000E6FC0 File Offset: 0x000E51C0
				public ProgramSetBuilder<selectSequence> selectSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectSequence>.CreateUnsafe(set);
				}

				// Token: 0x060048FD RID: 18685 RVA: 0x000E7018 File Offset: 0x000E5218
				public ProgramSetBuilder<selectRegion> selectRegion(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectRegion)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectRegion but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes.selectRegion>.CreateUnsafe(set);
				}

				// Token: 0x04002133 RID: 8499
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
