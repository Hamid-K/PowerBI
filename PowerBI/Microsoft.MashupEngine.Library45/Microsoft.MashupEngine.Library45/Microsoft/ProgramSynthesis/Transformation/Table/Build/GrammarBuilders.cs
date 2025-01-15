using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Extraction.Json;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build
{
	// Token: 0x02001A8A RID: 6794
	public class GrammarBuilders
	{
		// Token: 0x0600DF7D RID: 57213 RVA: 0x002FBAEA File Offset: 0x002F9CEA
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x0600DF7E RID: 57214 RVA: 0x002FBB16 File Offset: 0x002F9D16
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x0600DF7F RID: 57215 RVA: 0x002FBB23 File Offset: 0x002F9D23
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x0600DF80 RID: 57216 RVA: 0x002FBB30 File Offset: 0x002F9D30
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x0600DF81 RID: 57217 RVA: 0x002FBB3D File Offset: 0x002F9D3D
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x0600DF82 RID: 57218 RVA: 0x002FBB4A File Offset: 0x002F9D4A
		// (set) Token: 0x0600DF83 RID: 57219 RVA: 0x002FBB52 File Offset: 0x002F9D52
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x0600DF84 RID: 57220 RVA: 0x002FBB5B File Offset: 0x002F9D5B
		// (set) Token: 0x0600DF85 RID: 57221 RVA: 0x002FBB63 File Offset: 0x002F9D63
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600DF86 RID: 57222 RVA: 0x002FBB6C File Offset: 0x002F9D6C
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

		// Token: 0x040054D3 RID: 21715
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x040054D4 RID: 21716
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x040054D5 RID: 21717
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x040054D6 RID: 21718
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x040054D7 RID: 21719
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02001A8B RID: 6795
		public class GrammarSymbols
		{
			// Token: 0x17002553 RID: 9555
			// (get) Token: 0x0600DF88 RID: 57224 RVA: 0x002FBC17 File Offset: 0x002F9E17
			// (set) Token: 0x0600DF89 RID: 57225 RVA: 0x002FBC1F File Offset: 0x002F9E1F
			public Symbol @out { get; private set; }

			// Token: 0x17002554 RID: 9556
			// (get) Token: 0x0600DF8A RID: 57226 RVA: 0x002FBC28 File Offset: 0x002F9E28
			// (set) Token: 0x0600DF8B RID: 57227 RVA: 0x002FBC30 File Offset: 0x002F9E30
			public Symbol table { get; private set; }

			// Token: 0x17002555 RID: 9557
			// (get) Token: 0x0600DF8C RID: 57228 RVA: 0x002FBC39 File Offset: 0x002F9E39
			// (set) Token: 0x0600DF8D RID: 57229 RVA: 0x002FBC41 File Offset: 0x002F9E41
			public Symbol newColumns { get; private set; }

			// Token: 0x17002556 RID: 9558
			// (get) Token: 0x0600DF8E RID: 57230 RVA: 0x002FBC4A File Offset: 0x002F9E4A
			// (set) Token: 0x0600DF8F RID: 57231 RVA: 0x002FBC52 File Offset: 0x002F9E52
			public Symbol v { get; private set; }

			// Token: 0x17002557 RID: 9559
			// (get) Token: 0x0600DF90 RID: 57232 RVA: 0x002FBC5B File Offset: 0x002F9E5B
			// (set) Token: 0x0600DF91 RID: 57233 RVA: 0x002FBC63 File Offset: 0x002F9E63
			public Symbol columnToSplit { get; private set; }

			// Token: 0x17002558 RID: 9560
			// (get) Token: 0x0600DF92 RID: 57234 RVA: 0x002FBC6C File Offset: 0x002F9E6C
			// (set) Token: 0x0600DF93 RID: 57235 RVA: 0x002FBC74 File Offset: 0x002F9E74
			public Symbol splitCell { get; private set; }

			// Token: 0x17002559 RID: 9561
			// (get) Token: 0x0600DF94 RID: 57236 RVA: 0x002FBC7D File Offset: 0x002F9E7D
			// (set) Token: 0x0600DF95 RID: 57237 RVA: 0x002FBC85 File Offset: 0x002F9E85
			public Symbol inputTable { get; private set; }

			// Token: 0x1700255A RID: 9562
			// (get) Token: 0x0600DF96 RID: 57238 RVA: 0x002FBC8E File Offset: 0x002F9E8E
			// (set) Token: 0x0600DF97 RID: 57239 RVA: 0x002FBC96 File Offset: 0x002F9E96
			public Symbol sourceColumnName { get; private set; }

			// Token: 0x1700255B RID: 9563
			// (get) Token: 0x0600DF98 RID: 57240 RVA: 0x002FBC9F File Offset: 0x002F9E9F
			// (set) Token: 0x0600DF99 RID: 57241 RVA: 0x002FBCA7 File Offset: 0x002F9EA7
			public Symbol richDataType { get; private set; }

			// Token: 0x1700255C RID: 9564
			// (get) Token: 0x0600DF9A RID: 57242 RVA: 0x002FBCB0 File Offset: 0x002F9EB0
			// (set) Token: 0x0600DF9B RID: 57243 RVA: 0x002FBCB8 File Offset: 0x002F9EB8
			public Symbol fillMethod { get; private set; }

			// Token: 0x1700255D RID: 9565
			// (get) Token: 0x0600DF9C RID: 57244 RVA: 0x002FBCC1 File Offset: 0x002F9EC1
			// (set) Token: 0x0600DF9D RID: 57245 RVA: 0x002FBCC9 File Offset: 0x002F9EC9
			public Symbol dropCondition { get; private set; }

			// Token: 0x1700255E RID: 9566
			// (get) Token: 0x0600DF9E RID: 57246 RVA: 0x002FBCD2 File Offset: 0x002F9ED2
			// (set) Token: 0x0600DF9F RID: 57247 RVA: 0x002FBCDA File Offset: 0x002F9EDA
			public Symbol fillValue { get; private set; }

			// Token: 0x1700255F RID: 9567
			// (get) Token: 0x0600DFA0 RID: 57248 RVA: 0x002FBCE3 File Offset: 0x002F9EE3
			// (set) Token: 0x0600DFA1 RID: 57249 RVA: 0x002FBCEB File Offset: 0x002F9EEB
			public Symbol missingValueMarkers { get; private set; }

			// Token: 0x17002560 RID: 9568
			// (get) Token: 0x0600DFA2 RID: 57250 RVA: 0x002FBCF4 File Offset: 0x002F9EF4
			// (set) Token: 0x0600DFA3 RID: 57251 RVA: 0x002FBCFC File Offset: 0x002F9EFC
			public Symbol isMixedColumn { get; private set; }

			// Token: 0x17002561 RID: 9569
			// (get) Token: 0x0600DFA4 RID: 57252 RVA: 0x002FBD05 File Offset: 0x002F9F05
			// (set) Token: 0x0600DFA5 RID: 57253 RVA: 0x002FBD0D File Offset: 0x002F9F0D
			public Symbol delimiter { get; private set; }

			// Token: 0x17002562 RID: 9570
			// (get) Token: 0x0600DFA6 RID: 57254 RVA: 0x002FBD16 File Offset: 0x002F9F16
			// (set) Token: 0x0600DFA7 RID: 57255 RVA: 0x002FBD1E File Offset: 0x002F9F1E
			public Symbol ejsonProgram { get; private set; }

			// Token: 0x17002563 RID: 9571
			// (get) Token: 0x0600DFA8 RID: 57256 RVA: 0x002FBD27 File Offset: 0x002F9F27
			// (set) Token: 0x0600DFA9 RID: 57257 RVA: 0x002FBD2F File Offset: 0x002F9F2F
			public Symbol _LFun0 { get; private set; }

			// Token: 0x0600DFAA RID: 57258 RVA: 0x002FBD38 File Offset: 0x002F9F38
			public GrammarSymbols(Grammar grammar)
			{
				this.@out = grammar.Symbol("out");
				this.table = grammar.Symbol("table");
				this.newColumns = grammar.Symbol("newColumns");
				this.v = grammar.Symbol("v");
				this.columnToSplit = grammar.Symbol("columnToSplit");
				this.splitCell = grammar.Symbol("splitCell");
				this.inputTable = grammar.Symbol("inputTable");
				this.sourceColumnName = grammar.Symbol("sourceColumnName");
				this.richDataType = grammar.Symbol("richDataType");
				this.fillMethod = grammar.Symbol("fillMethod");
				this.dropCondition = grammar.Symbol("dropCondition");
				this.fillValue = grammar.Symbol("fillValue");
				this.missingValueMarkers = grammar.Symbol("missingValueMarkers");
				this.isMixedColumn = grammar.Symbol("isMixedColumn");
				this.delimiter = grammar.Symbol("delimiter");
				this.ejsonProgram = grammar.Symbol("ejsonProgram");
				this._LFun0 = grammar.Symbol("_LFun0");
			}
		}

		// Token: 0x02001A8C RID: 6796
		public class GrammarRules
		{
			// Token: 0x17002564 RID: 9572
			// (get) Token: 0x0600DFAB RID: 57259 RVA: 0x002FBE6C File Offset: 0x002FA06C
			// (set) Token: 0x0600DFAC RID: 57260 RVA: 0x002FBE74 File Offset: 0x002FA074
			public BlackBoxRule LabelEncode { get; private set; }

			// Token: 0x17002565 RID: 9573
			// (get) Token: 0x0600DFAD RID: 57261 RVA: 0x002FBE7D File Offset: 0x002FA07D
			// (set) Token: 0x0600DFAE RID: 57262 RVA: 0x002FBE85 File Offset: 0x002FA085
			public BlackBoxRule OneHotEncode { get; private set; }

			// Token: 0x17002566 RID: 9574
			// (get) Token: 0x0600DFAF RID: 57263 RVA: 0x002FBE8E File Offset: 0x002FA08E
			// (set) Token: 0x0600DFB0 RID: 57264 RVA: 0x002FBE96 File Offset: 0x002FA096
			public BlackBoxRule MultiLabelBinarizer { get; private set; }

			// Token: 0x17002567 RID: 9575
			// (get) Token: 0x0600DFB1 RID: 57265 RVA: 0x002FBE9F File Offset: 0x002FA09F
			// (set) Token: 0x0600DFB2 RID: 57266 RVA: 0x002FBEA7 File Offset: 0x002FA0A7
			public BlackBoxRule CastColumn { get; private set; }

			// Token: 0x17002568 RID: 9576
			// (get) Token: 0x0600DFB3 RID: 57267 RVA: 0x002FBEB0 File Offset: 0x002FA0B0
			// (set) Token: 0x0600DFB4 RID: 57268 RVA: 0x002FBEB8 File Offset: 0x002FA0B8
			public BlackBoxRule FillMissingValues { get; private set; }

			// Token: 0x17002569 RID: 9577
			// (get) Token: 0x0600DFB5 RID: 57269 RVA: 0x002FBEC1 File Offset: 0x002FA0C1
			// (set) Token: 0x0600DFB6 RID: 57270 RVA: 0x002FBEC9 File Offset: 0x002FA0C9
			public BlackBoxRule DropColumn { get; private set; }

			// Token: 0x1700256A RID: 9578
			// (get) Token: 0x0600DFB7 RID: 57271 RVA: 0x002FBED2 File Offset: 0x002FA0D2
			// (set) Token: 0x0600DFB8 RID: 57272 RVA: 0x002FBEDA File Offset: 0x002FA0DA
			public BlackBoxRule DropRows { get; private set; }

			// Token: 0x1700256B RID: 9579
			// (get) Token: 0x0600DFB9 RID: 57273 RVA: 0x002FBEE3 File Offset: 0x002FA0E3
			// (set) Token: 0x0600DFBA RID: 57274 RVA: 0x002FBEEB File Offset: 0x002FA0EB
			public BlackBoxRule AddSplitColumns { get; private set; }

			// Token: 0x1700256C RID: 9580
			// (get) Token: 0x0600DFBB RID: 57275 RVA: 0x002FBEF4 File Offset: 0x002FA0F4
			// (set) Token: 0x0600DFBC RID: 57276 RVA: 0x002FBEFC File Offset: 0x002FA0FC
			public BlackBoxRule AddColumnsFromJson { get; private set; }

			// Token: 0x1700256D RID: 9581
			// (get) Token: 0x0600DFBD RID: 57277 RVA: 0x002FBF05 File Offset: 0x002FA105
			// (set) Token: 0x0600DFBE RID: 57278 RVA: 0x002FBF0D File Offset: 0x002FA10D
			public BlackBoxRule SelectColumnToSplit { get; private set; }

			// Token: 0x1700256E RID: 9582
			// (get) Token: 0x0600DFBF RID: 57279 RVA: 0x002FBF16 File Offset: 0x002FA116
			// (set) Token: 0x0600DFC0 RID: 57280 RVA: 0x002FBF1E File Offset: 0x002FA11E
			public ConceptRule SplitColumn { get; private set; }

			// Token: 0x1700256F RID: 9583
			// (get) Token: 0x0600DFC1 RID: 57281 RVA: 0x002FBF27 File Offset: 0x002FA127
			// (set) Token: 0x0600DFC2 RID: 57282 RVA: 0x002FBF2F File Offset: 0x002FA12F
			public ConversionRule TTableProgram { get; private set; }

			// Token: 0x17002570 RID: 9584
			// (get) Token: 0x0600DFC3 RID: 57283 RVA: 0x002FBF38 File Offset: 0x002FA138
			// (set) Token: 0x0600DFC4 RID: 57284 RVA: 0x002FBF40 File Offset: 0x002FA140
			public ConversionRule Split { get; private set; }

			// Token: 0x0600DFC5 RID: 57285 RVA: 0x002FBF4C File Offset: 0x002FA14C
			public GrammarRules(Grammar grammar)
			{
				this.LabelEncode = (BlackBoxRule)grammar.Rule("LabelEncode");
				this.OneHotEncode = (BlackBoxRule)grammar.Rule("OneHotEncode");
				this.MultiLabelBinarizer = (BlackBoxRule)grammar.Rule("MultiLabelBinarizer");
				this.CastColumn = (BlackBoxRule)grammar.Rule("CastColumn");
				this.FillMissingValues = (BlackBoxRule)grammar.Rule("FillMissingValues");
				this.DropColumn = (BlackBoxRule)grammar.Rule("DropColumn");
				this.DropRows = (BlackBoxRule)grammar.Rule("DropRows");
				this.AddSplitColumns = (BlackBoxRule)grammar.Rule("AddSplitColumns");
				this.AddColumnsFromJson = (BlackBoxRule)grammar.Rule("AddColumnsFromJson");
				this.SelectColumnToSplit = (BlackBoxRule)grammar.Rule("SelectColumnToSplit");
				this.SplitColumn = (ConceptRule)grammar.Rule("SplitColumn");
				this.TTableProgram = (ConversionRule)grammar.Rule("TTableProgram");
				this.Split = (ConversionRule)grammar.Rule("Split");
			}
		}

		// Token: 0x02001A8D RID: 6797
		public class GrammarUnnamedConversions
		{
			// Token: 0x17002571 RID: 9585
			// (get) Token: 0x0600DFC6 RID: 57286 RVA: 0x002FC07D File Offset: 0x002FA27D
			// (set) Token: 0x0600DFC7 RID: 57287 RVA: 0x002FC085 File Offset: 0x002FA285
			public ConversionRule table_inputTable { get; private set; }

			// Token: 0x0600DFC8 RID: 57288 RVA: 0x002FC08E File Offset: 0x002FA28E
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.table_inputTable = (ConversionRule)grammar.Rule("~convert_table_inputTable");
			}
		}

		// Token: 0x02001A8E RID: 6798
		public class GrammarHoles
		{
			// Token: 0x17002572 RID: 9586
			// (get) Token: 0x0600DFC9 RID: 57289 RVA: 0x002FC0AC File Offset: 0x002FA2AC
			// (set) Token: 0x0600DFCA RID: 57290 RVA: 0x002FC0B4 File Offset: 0x002FA2B4
			public Hole @out { get; private set; }

			// Token: 0x17002573 RID: 9587
			// (get) Token: 0x0600DFCB RID: 57291 RVA: 0x002FC0BD File Offset: 0x002FA2BD
			// (set) Token: 0x0600DFCC RID: 57292 RVA: 0x002FC0C5 File Offset: 0x002FA2C5
			public Hole table { get; private set; }

			// Token: 0x17002574 RID: 9588
			// (get) Token: 0x0600DFCD RID: 57293 RVA: 0x002FC0CE File Offset: 0x002FA2CE
			// (set) Token: 0x0600DFCE RID: 57294 RVA: 0x002FC0D6 File Offset: 0x002FA2D6
			public Hole newColumns { get; private set; }

			// Token: 0x17002575 RID: 9589
			// (get) Token: 0x0600DFCF RID: 57295 RVA: 0x002FC0DF File Offset: 0x002FA2DF
			// (set) Token: 0x0600DFD0 RID: 57296 RVA: 0x002FC0E7 File Offset: 0x002FA2E7
			public Hole v { get; private set; }

			// Token: 0x17002576 RID: 9590
			// (get) Token: 0x0600DFD1 RID: 57297 RVA: 0x002FC0F0 File Offset: 0x002FA2F0
			// (set) Token: 0x0600DFD2 RID: 57298 RVA: 0x002FC0F8 File Offset: 0x002FA2F8
			public Hole columnToSplit { get; private set; }

			// Token: 0x17002577 RID: 9591
			// (get) Token: 0x0600DFD3 RID: 57299 RVA: 0x002FC101 File Offset: 0x002FA301
			// (set) Token: 0x0600DFD4 RID: 57300 RVA: 0x002FC109 File Offset: 0x002FA309
			public Hole splitCell { get; private set; }

			// Token: 0x17002578 RID: 9592
			// (get) Token: 0x0600DFD5 RID: 57301 RVA: 0x002FC112 File Offset: 0x002FA312
			// (set) Token: 0x0600DFD6 RID: 57302 RVA: 0x002FC11A File Offset: 0x002FA31A
			public Hole inputTable { get; private set; }

			// Token: 0x17002579 RID: 9593
			// (get) Token: 0x0600DFD7 RID: 57303 RVA: 0x002FC123 File Offset: 0x002FA323
			// (set) Token: 0x0600DFD8 RID: 57304 RVA: 0x002FC12B File Offset: 0x002FA32B
			public Hole sourceColumnName { get; private set; }

			// Token: 0x1700257A RID: 9594
			// (get) Token: 0x0600DFD9 RID: 57305 RVA: 0x002FC134 File Offset: 0x002FA334
			// (set) Token: 0x0600DFDA RID: 57306 RVA: 0x002FC13C File Offset: 0x002FA33C
			public Hole richDataType { get; private set; }

			// Token: 0x1700257B RID: 9595
			// (get) Token: 0x0600DFDB RID: 57307 RVA: 0x002FC145 File Offset: 0x002FA345
			// (set) Token: 0x0600DFDC RID: 57308 RVA: 0x002FC14D File Offset: 0x002FA34D
			public Hole fillMethod { get; private set; }

			// Token: 0x1700257C RID: 9596
			// (get) Token: 0x0600DFDD RID: 57309 RVA: 0x002FC156 File Offset: 0x002FA356
			// (set) Token: 0x0600DFDE RID: 57310 RVA: 0x002FC15E File Offset: 0x002FA35E
			public Hole dropCondition { get; private set; }

			// Token: 0x1700257D RID: 9597
			// (get) Token: 0x0600DFDF RID: 57311 RVA: 0x002FC167 File Offset: 0x002FA367
			// (set) Token: 0x0600DFE0 RID: 57312 RVA: 0x002FC16F File Offset: 0x002FA36F
			public Hole fillValue { get; private set; }

			// Token: 0x1700257E RID: 9598
			// (get) Token: 0x0600DFE1 RID: 57313 RVA: 0x002FC178 File Offset: 0x002FA378
			// (set) Token: 0x0600DFE2 RID: 57314 RVA: 0x002FC180 File Offset: 0x002FA380
			public Hole missingValueMarkers { get; private set; }

			// Token: 0x1700257F RID: 9599
			// (get) Token: 0x0600DFE3 RID: 57315 RVA: 0x002FC189 File Offset: 0x002FA389
			// (set) Token: 0x0600DFE4 RID: 57316 RVA: 0x002FC191 File Offset: 0x002FA391
			public Hole isMixedColumn { get; private set; }

			// Token: 0x17002580 RID: 9600
			// (get) Token: 0x0600DFE5 RID: 57317 RVA: 0x002FC19A File Offset: 0x002FA39A
			// (set) Token: 0x0600DFE6 RID: 57318 RVA: 0x002FC1A2 File Offset: 0x002FA3A2
			public Hole delimiter { get; private set; }

			// Token: 0x17002581 RID: 9601
			// (get) Token: 0x0600DFE7 RID: 57319 RVA: 0x002FC1AB File Offset: 0x002FA3AB
			// (set) Token: 0x0600DFE8 RID: 57320 RVA: 0x002FC1B3 File Offset: 0x002FA3B3
			public Hole ejsonProgram { get; private set; }

			// Token: 0x17002582 RID: 9602
			// (get) Token: 0x0600DFE9 RID: 57321 RVA: 0x002FC1BC File Offset: 0x002FA3BC
			// (set) Token: 0x0600DFEA RID: 57322 RVA: 0x002FC1C4 File Offset: 0x002FA3C4
			public Hole _LFun0 { get; private set; }

			// Token: 0x0600DFEB RID: 57323 RVA: 0x002FC1D0 File Offset: 0x002FA3D0
			public GrammarHoles(GrammarBuilders builders)
			{
				this.@out = new Hole(builders.Symbol.@out, null);
				this.table = new Hole(builders.Symbol.table, null);
				this.newColumns = new Hole(builders.Symbol.newColumns, null);
				this.v = new Hole(builders.Symbol.v, null);
				this.columnToSplit = new Hole(builders.Symbol.columnToSplit, null);
				this.splitCell = new Hole(builders.Symbol.splitCell, null);
				this.inputTable = new Hole(builders.Symbol.inputTable, null);
				this.sourceColumnName = new Hole(builders.Symbol.sourceColumnName, null);
				this.richDataType = new Hole(builders.Symbol.richDataType, null);
				this.fillMethod = new Hole(builders.Symbol.fillMethod, null);
				this.dropCondition = new Hole(builders.Symbol.dropCondition, null);
				this.fillValue = new Hole(builders.Symbol.fillValue, null);
				this.missingValueMarkers = new Hole(builders.Symbol.missingValueMarkers, null);
				this.isMixedColumn = new Hole(builders.Symbol.isMixedColumn, null);
				this.delimiter = new Hole(builders.Symbol.delimiter, null);
				this.ejsonProgram = new Hole(builders.Symbol.ejsonProgram, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
			}
		}

		// Token: 0x02001A8F RID: 6799
		public class Nodes
		{
			// Token: 0x0600DFEC RID: 57324 RVA: 0x002FC36C File Offset: 0x002FA56C
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

			// Token: 0x17002583 RID: 9603
			// (get) Token: 0x0600DFED RID: 57325 RVA: 0x002FC44F File Offset: 0x002FA64F
			// (set) Token: 0x0600DFEE RID: 57326 RVA: 0x002FC457 File Offset: 0x002FA657
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17002584 RID: 9604
			// (get) Token: 0x0600DFEF RID: 57327 RVA: 0x002FC460 File Offset: 0x002FA660
			// (set) Token: 0x0600DFF0 RID: 57328 RVA: 0x002FC468 File Offset: 0x002FA668
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17002585 RID: 9605
			// (get) Token: 0x0600DFF1 RID: 57329 RVA: 0x002FC471 File Offset: 0x002FA671
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17002586 RID: 9606
			// (get) Token: 0x0600DFF2 RID: 57330 RVA: 0x002FC47E File Offset: 0x002FA67E
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17002587 RID: 9607
			// (get) Token: 0x0600DFF3 RID: 57331 RVA: 0x002FC48B File Offset: 0x002FA68B
			// (set) Token: 0x0600DFF4 RID: 57332 RVA: 0x002FC493 File Offset: 0x002FA693
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17002588 RID: 9608
			// (get) Token: 0x0600DFF5 RID: 57333 RVA: 0x002FC49C File Offset: 0x002FA69C
			// (set) Token: 0x0600DFF6 RID: 57334 RVA: 0x002FC4A4 File Offset: 0x002FA6A4
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17002589 RID: 9609
			// (get) Token: 0x0600DFF7 RID: 57335 RVA: 0x002FC4AD File Offset: 0x002FA6AD
			// (set) Token: 0x0600DFF8 RID: 57336 RVA: 0x002FC4B5 File Offset: 0x002FA6B5
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x1700258A RID: 9610
			// (get) Token: 0x0600DFF9 RID: 57337 RVA: 0x002FC4BE File Offset: 0x002FA6BE
			// (set) Token: 0x0600DFFA RID: 57338 RVA: 0x002FC4C6 File Offset: 0x002FA6C6
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x1700258B RID: 9611
			// (get) Token: 0x0600DFFB RID: 57339 RVA: 0x002FC4CF File Offset: 0x002FA6CF
			// (set) Token: 0x0600DFFC RID: 57340 RVA: 0x002FC4D7 File Offset: 0x002FA6D7
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x1700258C RID: 9612
			// (get) Token: 0x0600DFFD RID: 57341 RVA: 0x002FC4E0 File Offset: 0x002FA6E0
			// (set) Token: 0x0600DFFE RID: 57342 RVA: 0x002FC4E8 File Offset: 0x002FA6E8
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x1700258D RID: 9613
			// (get) Token: 0x0600DFFF RID: 57343 RVA: 0x002FC4F1 File Offset: 0x002FA6F1
			// (set) Token: 0x0600E000 RID: 57344 RVA: 0x002FC4F9 File Offset: 0x002FA6F9
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x0400550C RID: 21772
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x0400550D RID: 21773
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001A90 RID: 6800
			public class NodeRules
			{
				// Token: 0x0600E001 RID: 57345 RVA: 0x002FC502 File Offset: 0x002FA702
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E002 RID: 57346 RVA: 0x002FC511 File Offset: 0x002FA711
				public sourceColumnName sourceColumnName(string value)
				{
					return new sourceColumnName(this._builders, value);
				}

				// Token: 0x0600E003 RID: 57347 RVA: 0x002FC51F File Offset: 0x002FA71F
				public richDataType richDataType(IRichDataType value)
				{
					return new richDataType(this._builders, value);
				}

				// Token: 0x0600E004 RID: 57348 RVA: 0x002FC52D File Offset: 0x002FA72D
				public fillMethod fillMethod(FillMethod value)
				{
					return new fillMethod(this._builders, value);
				}

				// Token: 0x0600E005 RID: 57349 RVA: 0x002FC53B File Offset: 0x002FA73B
				public dropCondition dropCondition(DropCondition value)
				{
					return new dropCondition(this._builders, value);
				}

				// Token: 0x0600E006 RID: 57350 RVA: 0x002FC549 File Offset: 0x002FA749
				public fillValue fillValue(object value)
				{
					return new fillValue(this._builders, value);
				}

				// Token: 0x0600E007 RID: 57351 RVA: 0x002FC557 File Offset: 0x002FA757
				public missingValueMarkers missingValueMarkers(IEnumerable<object> value)
				{
					return new missingValueMarkers(this._builders, value);
				}

				// Token: 0x0600E008 RID: 57352 RVA: 0x002FC565 File Offset: 0x002FA765
				public isMixedColumn isMixedColumn(bool value)
				{
					return new isMixedColumn(this._builders, value);
				}

				// Token: 0x0600E009 RID: 57353 RVA: 0x002FC573 File Offset: 0x002FA773
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter delimiter(string value)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter(this._builders, value);
				}

				// Token: 0x0600E00A RID: 57354 RVA: 0x002FC581 File Offset: 0x002FA781
				public ejsonProgram ejsonProgram(Program value)
				{
					return new ejsonProgram(this._builders, value);
				}

				// Token: 0x0600E00B RID: 57355 RVA: 0x002FC58F File Offset: 0x002FA78F
				public table LabelEncode(table value0, sourceColumnName value1)
				{
					return new LabelEncode(this._builders, value0, value1);
				}

				// Token: 0x0600E00C RID: 57356 RVA: 0x002FC5A3 File Offset: 0x002FA7A3
				public table OneHotEncode(table value0, sourceColumnName value1)
				{
					return new OneHotEncode(this._builders, value0, value1);
				}

				// Token: 0x0600E00D RID: 57357 RVA: 0x002FC5B7 File Offset: 0x002FA7B7
				public table MultiLabelBinarizer(table value0, sourceColumnName value1, Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter value2)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer(this._builders, value0, value1, value2);
				}

				// Token: 0x0600E00E RID: 57358 RVA: 0x002FC5CC File Offset: 0x002FA7CC
				public table CastColumn(table value0, sourceColumnName value1, richDataType value2, isMixedColumn value3)
				{
					return new CastColumn(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x0600E00F RID: 57359 RVA: 0x002FC5E3 File Offset: 0x002FA7E3
				public table FillMissingValues(table value0, sourceColumnName value1, fillValue value2, missingValueMarkers value3, fillMethod value4)
				{
					return new FillMissingValues(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x0600E010 RID: 57360 RVA: 0x002FC5FC File Offset: 0x002FA7FC
				public table DropColumn(table value0, sourceColumnName value1, dropCondition value2)
				{
					return new DropColumn(this._builders, value0, value1, value2);
				}

				// Token: 0x0600E011 RID: 57361 RVA: 0x002FC611 File Offset: 0x002FA811
				public table DropRows(table value0, dropCondition value1)
				{
					return new DropRows(this._builders, value0, value1);
				}

				// Token: 0x0600E012 RID: 57362 RVA: 0x002FC625 File Offset: 0x002FA825
				public table AddSplitColumns(table value0, sourceColumnName value1, newColumns value2)
				{
					return new AddSplitColumns(this._builders, value0, value1, value2);
				}

				// Token: 0x0600E013 RID: 57363 RVA: 0x002FC63A File Offset: 0x002FA83A
				public table AddColumnsFromJson(table value0, sourceColumnName value1, ejsonProgram value2)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson(this._builders, value0, value1, value2);
				}

				// Token: 0x0600E014 RID: 57364 RVA: 0x002FC64F File Offset: 0x002FA84F
				public columnToSplit SelectColumnToSplit(inputTable value0, sourceColumnName value1)
				{
					return new SelectColumnToSplit(this._builders, value0, value1);
				}

				// Token: 0x0600E015 RID: 57365 RVA: 0x002FC663 File Offset: 0x002FA863
				public newColumns SplitColumn(splitCell value0, columnToSplit value1)
				{
					return new SplitColumn(this._builders, value0, value1);
				}

				// Token: 0x0600E016 RID: 57366 RVA: 0x002FC677 File Offset: 0x002FA877
				public @out TTableProgram(table value0)
				{
					return new TTableProgram(this._builders, value0);
				}

				// Token: 0x0600E017 RID: 57367 RVA: 0x002FC68A File Offset: 0x002FA88A
				public splitCell Split(regionSplit value0)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split(this._builders, value0);
				}

				// Token: 0x04005515 RID: 21781
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A91 RID: 6801
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0600E018 RID: 57368 RVA: 0x002FC69D File Offset: 0x002FA89D
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E019 RID: 57369 RVA: 0x002FC6AC File Offset: 0x002FA8AC
				public table table_inputTable(inputTable value0)
				{
					return new table_inputTable(this._builders, value0);
				}

				// Token: 0x04005516 RID: 21782
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A92 RID: 6802
			public class NodeVariables
			{
				// Token: 0x1700258E RID: 9614
				// (get) Token: 0x0600E01A RID: 57370 RVA: 0x002FC6BF File Offset: 0x002FA8BF
				// (set) Token: 0x0600E01B RID: 57371 RVA: 0x002FC6C7 File Offset: 0x002FA8C7
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v v { get; private set; }

				// Token: 0x1700258F RID: 9615
				// (get) Token: 0x0600E01C RID: 57372 RVA: 0x002FC6D0 File Offset: 0x002FA8D0
				// (set) Token: 0x0600E01D RID: 57373 RVA: 0x002FC6D8 File Offset: 0x002FA8D8
				public inputTable inputTable { get; private set; }

				// Token: 0x0600E01E RID: 57374 RVA: 0x002FC6E1 File Offset: 0x002FA8E1
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v(builders);
					this.inputTable = new inputTable(builders);
				}
			}

			// Token: 0x02001A93 RID: 6803
			public class NodeHoles
			{
				// Token: 0x17002590 RID: 9616
				// (get) Token: 0x0600E01F RID: 57375 RVA: 0x002FC701 File Offset: 0x002FA901
				// (set) Token: 0x0600E020 RID: 57376 RVA: 0x002FC709 File Offset: 0x002FA909
				public @out @out { get; private set; }

				// Token: 0x17002591 RID: 9617
				// (get) Token: 0x0600E021 RID: 57377 RVA: 0x002FC712 File Offset: 0x002FA912
				// (set) Token: 0x0600E022 RID: 57378 RVA: 0x002FC71A File Offset: 0x002FA91A
				public table table { get; private set; }

				// Token: 0x17002592 RID: 9618
				// (get) Token: 0x0600E023 RID: 57379 RVA: 0x002FC723 File Offset: 0x002FA923
				// (set) Token: 0x0600E024 RID: 57380 RVA: 0x002FC72B File Offset: 0x002FA92B
				public newColumns newColumns { get; private set; }

				// Token: 0x17002593 RID: 9619
				// (get) Token: 0x0600E025 RID: 57381 RVA: 0x002FC734 File Offset: 0x002FA934
				// (set) Token: 0x0600E026 RID: 57382 RVA: 0x002FC73C File Offset: 0x002FA93C
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v v { get; private set; }

				// Token: 0x17002594 RID: 9620
				// (get) Token: 0x0600E027 RID: 57383 RVA: 0x002FC745 File Offset: 0x002FA945
				// (set) Token: 0x0600E028 RID: 57384 RVA: 0x002FC74D File Offset: 0x002FA94D
				public columnToSplit columnToSplit { get; private set; }

				// Token: 0x17002595 RID: 9621
				// (get) Token: 0x0600E029 RID: 57385 RVA: 0x002FC756 File Offset: 0x002FA956
				// (set) Token: 0x0600E02A RID: 57386 RVA: 0x002FC75E File Offset: 0x002FA95E
				public splitCell splitCell { get; private set; }

				// Token: 0x17002596 RID: 9622
				// (get) Token: 0x0600E02B RID: 57387 RVA: 0x002FC767 File Offset: 0x002FA967
				// (set) Token: 0x0600E02C RID: 57388 RVA: 0x002FC76F File Offset: 0x002FA96F
				public sourceColumnName sourceColumnName { get; private set; }

				// Token: 0x17002597 RID: 9623
				// (get) Token: 0x0600E02D RID: 57389 RVA: 0x002FC778 File Offset: 0x002FA978
				// (set) Token: 0x0600E02E RID: 57390 RVA: 0x002FC780 File Offset: 0x002FA980
				public richDataType richDataType { get; private set; }

				// Token: 0x17002598 RID: 9624
				// (get) Token: 0x0600E02F RID: 57391 RVA: 0x002FC789 File Offset: 0x002FA989
				// (set) Token: 0x0600E030 RID: 57392 RVA: 0x002FC791 File Offset: 0x002FA991
				public fillMethod fillMethod { get; private set; }

				// Token: 0x17002599 RID: 9625
				// (get) Token: 0x0600E031 RID: 57393 RVA: 0x002FC79A File Offset: 0x002FA99A
				// (set) Token: 0x0600E032 RID: 57394 RVA: 0x002FC7A2 File Offset: 0x002FA9A2
				public dropCondition dropCondition { get; private set; }

				// Token: 0x1700259A RID: 9626
				// (get) Token: 0x0600E033 RID: 57395 RVA: 0x002FC7AB File Offset: 0x002FA9AB
				// (set) Token: 0x0600E034 RID: 57396 RVA: 0x002FC7B3 File Offset: 0x002FA9B3
				public fillValue fillValue { get; private set; }

				// Token: 0x1700259B RID: 9627
				// (get) Token: 0x0600E035 RID: 57397 RVA: 0x002FC7BC File Offset: 0x002FA9BC
				// (set) Token: 0x0600E036 RID: 57398 RVA: 0x002FC7C4 File Offset: 0x002FA9C4
				public missingValueMarkers missingValueMarkers { get; private set; }

				// Token: 0x1700259C RID: 9628
				// (get) Token: 0x0600E037 RID: 57399 RVA: 0x002FC7CD File Offset: 0x002FA9CD
				// (set) Token: 0x0600E038 RID: 57400 RVA: 0x002FC7D5 File Offset: 0x002FA9D5
				public isMixedColumn isMixedColumn { get; private set; }

				// Token: 0x1700259D RID: 9629
				// (get) Token: 0x0600E039 RID: 57401 RVA: 0x002FC7DE File Offset: 0x002FA9DE
				// (set) Token: 0x0600E03A RID: 57402 RVA: 0x002FC7E6 File Offset: 0x002FA9E6
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter delimiter { get; private set; }

				// Token: 0x1700259E RID: 9630
				// (get) Token: 0x0600E03B RID: 57403 RVA: 0x002FC7EF File Offset: 0x002FA9EF
				// (set) Token: 0x0600E03C RID: 57404 RVA: 0x002FC7F7 File Offset: 0x002FA9F7
				public ejsonProgram ejsonProgram { get; private set; }

				// Token: 0x0600E03D RID: 57405 RVA: 0x002FC800 File Offset: 0x002FAA00
				public NodeHoles(GrammarBuilders builders)
				{
					this.@out = @out.CreateHole(builders, null);
					this.table = table.CreateHole(builders, null);
					this.newColumns = newColumns.CreateHole(builders, null);
					this.v = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateHole(builders, null);
					this.columnToSplit = columnToSplit.CreateHole(builders, null);
					this.splitCell = splitCell.CreateHole(builders, null);
					this.sourceColumnName = sourceColumnName.CreateHole(builders, null);
					this.richDataType = richDataType.CreateHole(builders, null);
					this.fillMethod = fillMethod.CreateHole(builders, null);
					this.dropCondition = dropCondition.CreateHole(builders, null);
					this.fillValue = fillValue.CreateHole(builders, null);
					this.missingValueMarkers = missingValueMarkers.CreateHole(builders, null);
					this.isMixedColumn = isMixedColumn.CreateHole(builders, null);
					this.delimiter = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateHole(builders, null);
					this.ejsonProgram = ejsonProgram.CreateHole(builders, null);
				}
			}

			// Token: 0x02001A94 RID: 6804
			public class NodeUnsafe
			{
				// Token: 0x0600E03E RID: 57406 RVA: 0x002FC8D6 File Offset: 0x002FAAD6
				public @out @out(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out.CreateUnsafe(node);
				}

				// Token: 0x0600E03F RID: 57407 RVA: 0x002FC8DE File Offset: 0x002FAADE
				public table table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table.CreateUnsafe(node);
				}

				// Token: 0x0600E040 RID: 57408 RVA: 0x002FC8E6 File Offset: 0x002FAAE6
				public newColumns newColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns.CreateUnsafe(node);
				}

				// Token: 0x0600E041 RID: 57409 RVA: 0x002FC8EE File Offset: 0x002FAAEE
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateUnsafe(node);
				}

				// Token: 0x0600E042 RID: 57410 RVA: 0x002FC8F6 File Offset: 0x002FAAF6
				public columnToSplit columnToSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit.CreateUnsafe(node);
				}

				// Token: 0x0600E043 RID: 57411 RVA: 0x002FC8FE File Offset: 0x002FAAFE
				public splitCell splitCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell.CreateUnsafe(node);
				}

				// Token: 0x0600E044 RID: 57412 RVA: 0x002FC906 File Offset: 0x002FAB06
				public sourceColumnName sourceColumnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName.CreateUnsafe(node);
				}

				// Token: 0x0600E045 RID: 57413 RVA: 0x002FC90E File Offset: 0x002FAB0E
				public richDataType richDataType(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType.CreateUnsafe(node);
				}

				// Token: 0x0600E046 RID: 57414 RVA: 0x002FC916 File Offset: 0x002FAB16
				public fillMethod fillMethod(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod.CreateUnsafe(node);
				}

				// Token: 0x0600E047 RID: 57415 RVA: 0x002FC91E File Offset: 0x002FAB1E
				public dropCondition dropCondition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition.CreateUnsafe(node);
				}

				// Token: 0x0600E048 RID: 57416 RVA: 0x002FC926 File Offset: 0x002FAB26
				public fillValue fillValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue.CreateUnsafe(node);
				}

				// Token: 0x0600E049 RID: 57417 RVA: 0x002FC92E File Offset: 0x002FAB2E
				public missingValueMarkers missingValueMarkers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers.CreateUnsafe(node);
				}

				// Token: 0x0600E04A RID: 57418 RVA: 0x002FC936 File Offset: 0x002FAB36
				public isMixedColumn isMixedColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn.CreateUnsafe(node);
				}

				// Token: 0x0600E04B RID: 57419 RVA: 0x002FC93E File Offset: 0x002FAB3E
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateUnsafe(node);
				}

				// Token: 0x0600E04C RID: 57420 RVA: 0x002FC946 File Offset: 0x002FAB46
				public ejsonProgram ejsonProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram.CreateUnsafe(node);
				}
			}

			// Token: 0x02001A95 RID: 6805
			public class NodeCast
			{
				// Token: 0x0600E04E RID: 57422 RVA: 0x002FC94E File Offset: 0x002FAB4E
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E04F RID: 57423 RVA: 0x002FC960 File Offset: 0x002FAB60
				public @out @out(ProgramNode node)
				{
					@out? @out = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out.CreateSafe(this._builders, node);
					if (@out == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @out but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @out.Value;
				}

				// Token: 0x0600E050 RID: 57424 RVA: 0x002FC9B4 File Offset: 0x002FABB4
				public table table(ProgramNode node)
				{
					table? table = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table.CreateSafe(this._builders, node);
					if (table == null)
					{
						string text = "node";
						string text2 = "expected node for symbol table but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return table.Value;
				}

				// Token: 0x0600E051 RID: 57425 RVA: 0x002FCA08 File Offset: 0x002FAC08
				public newColumns newColumns(ProgramNode node)
				{
					newColumns? newColumns = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns.CreateSafe(this._builders, node);
					if (newColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol newColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return newColumns.Value;
				}

				// Token: 0x0600E052 RID: 57426 RVA: 0x002FCA5C File Offset: 0x002FAC5C
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v v(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v? v = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateSafe(this._builders, node);
					if (v == null)
					{
						string text = "node";
						string text2 = "expected node for symbol v but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return v.Value;
				}

				// Token: 0x0600E053 RID: 57427 RVA: 0x002FCAB0 File Offset: 0x002FACB0
				public columnToSplit columnToSplit(ProgramNode node)
				{
					columnToSplit? columnToSplit = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit.CreateSafe(this._builders, node);
					if (columnToSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnToSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnToSplit.Value;
				}

				// Token: 0x0600E054 RID: 57428 RVA: 0x002FCB04 File Offset: 0x002FAD04
				public splitCell splitCell(ProgramNode node)
				{
					splitCell? splitCell = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell.CreateSafe(this._builders, node);
					if (splitCell == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitCell but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitCell.Value;
				}

				// Token: 0x0600E055 RID: 57429 RVA: 0x002FCB58 File Offset: 0x002FAD58
				public sourceColumnName sourceColumnName(ProgramNode node)
				{
					sourceColumnName? sourceColumnName = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName.CreateSafe(this._builders, node);
					if (sourceColumnName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sourceColumnName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sourceColumnName.Value;
				}

				// Token: 0x0600E056 RID: 57430 RVA: 0x002FCBAC File Offset: 0x002FADAC
				public richDataType richDataType(ProgramNode node)
				{
					richDataType? richDataType = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType.CreateSafe(this._builders, node);
					if (richDataType == null)
					{
						string text = "node";
						string text2 = "expected node for symbol richDataType but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return richDataType.Value;
				}

				// Token: 0x0600E057 RID: 57431 RVA: 0x002FCC00 File Offset: 0x002FAE00
				public fillMethod fillMethod(ProgramNode node)
				{
					fillMethod? fillMethod = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod.CreateSafe(this._builders, node);
					if (fillMethod == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fillMethod but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fillMethod.Value;
				}

				// Token: 0x0600E058 RID: 57432 RVA: 0x002FCC54 File Offset: 0x002FAE54
				public dropCondition dropCondition(ProgramNode node)
				{
					dropCondition? dropCondition = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition.CreateSafe(this._builders, node);
					if (dropCondition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dropCondition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dropCondition.Value;
				}

				// Token: 0x0600E059 RID: 57433 RVA: 0x002FCCA8 File Offset: 0x002FAEA8
				public fillValue fillValue(ProgramNode node)
				{
					fillValue? fillValue = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue.CreateSafe(this._builders, node);
					if (fillValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fillValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fillValue.Value;
				}

				// Token: 0x0600E05A RID: 57434 RVA: 0x002FCCFC File Offset: 0x002FAEFC
				public missingValueMarkers missingValueMarkers(ProgramNode node)
				{
					missingValueMarkers? missingValueMarkers = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers.CreateSafe(this._builders, node);
					if (missingValueMarkers == null)
					{
						string text = "node";
						string text2 = "expected node for symbol missingValueMarkers but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return missingValueMarkers.Value;
				}

				// Token: 0x0600E05B RID: 57435 RVA: 0x002FCD50 File Offset: 0x002FAF50
				public isMixedColumn isMixedColumn(ProgramNode node)
				{
					isMixedColumn? isMixedColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn.CreateSafe(this._builders, node);
					if (isMixedColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol isMixedColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isMixedColumn.Value;
				}

				// Token: 0x0600E05C RID: 57436 RVA: 0x002FCDA4 File Offset: 0x002FAFA4
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter delimiter(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter? delimiter = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiter.Value;
				}

				// Token: 0x0600E05D RID: 57437 RVA: 0x002FCDF8 File Offset: 0x002FAFF8
				public ejsonProgram ejsonProgram(ProgramNode node)
				{
					ejsonProgram? ejsonProgram = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram.CreateSafe(this._builders, node);
					if (ejsonProgram == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ejsonProgram but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ejsonProgram.Value;
				}

				// Token: 0x04005528 RID: 21800
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A96 RID: 6806
			public class RuleCast
			{
				// Token: 0x0600E05E RID: 57438 RVA: 0x002FCE49 File Offset: 0x002FB049
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E05F RID: 57439 RVA: 0x002FCE58 File Offset: 0x002FB058
				public TTableProgram TTableProgram(ProgramNode node)
				{
					TTableProgram? ttableProgram = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.TTableProgram.CreateSafe(this._builders, node);
					if (ttableProgram == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TTableProgram but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ttableProgram.Value;
				}

				// Token: 0x0600E060 RID: 57440 RVA: 0x002FCEAC File Offset: 0x002FB0AC
				public table_inputTable table_inputTable(ProgramNode node)
				{
					table_inputTable? table_inputTable = Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes.table_inputTable.CreateSafe(this._builders, node);
					if (table_inputTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol table_inputTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return table_inputTable.Value;
				}

				// Token: 0x0600E061 RID: 57441 RVA: 0x002FCF00 File Offset: 0x002FB100
				public LabelEncode LabelEncode(ProgramNode node)
				{
					LabelEncode? labelEncode = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.LabelEncode.CreateSafe(this._builders, node);
					if (labelEncode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LabelEncode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelEncode.Value;
				}

				// Token: 0x0600E062 RID: 57442 RVA: 0x002FCF54 File Offset: 0x002FB154
				public OneHotEncode OneHotEncode(ProgramNode node)
				{
					OneHotEncode? oneHotEncode = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.OneHotEncode.CreateSafe(this._builders, node);
					if (oneHotEncode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol OneHotEncode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return oneHotEncode.Value;
				}

				// Token: 0x0600E063 RID: 57443 RVA: 0x002FCFA8 File Offset: 0x002FB1A8
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer MultiLabelBinarizer(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer? multiLabelBinarizer = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer.CreateSafe(this._builders, node);
					if (multiLabelBinarizer == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MultiLabelBinarizer but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multiLabelBinarizer.Value;
				}

				// Token: 0x0600E064 RID: 57444 RVA: 0x002FCFFC File Offset: 0x002FB1FC
				public CastColumn CastColumn(ProgramNode node)
				{
					CastColumn? castColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.CastColumn.CreateSafe(this._builders, node);
					if (castColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol CastColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return castColumn.Value;
				}

				// Token: 0x0600E065 RID: 57445 RVA: 0x002FD050 File Offset: 0x002FB250
				public FillMissingValues FillMissingValues(ProgramNode node)
				{
					FillMissingValues? fillMissingValues = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.FillMissingValues.CreateSafe(this._builders, node);
					if (fillMissingValues == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FillMissingValues but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fillMissingValues.Value;
				}

				// Token: 0x0600E066 RID: 57446 RVA: 0x002FD0A4 File Offset: 0x002FB2A4
				public DropColumn DropColumn(ProgramNode node)
				{
					DropColumn? dropColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropColumn.CreateSafe(this._builders, node);
					if (dropColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DropColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dropColumn.Value;
				}

				// Token: 0x0600E067 RID: 57447 RVA: 0x002FD0F8 File Offset: 0x002FB2F8
				public DropRows DropRows(ProgramNode node)
				{
					DropRows? dropRows = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropRows.CreateSafe(this._builders, node);
					if (dropRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DropRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dropRows.Value;
				}

				// Token: 0x0600E068 RID: 57448 RVA: 0x002FD14C File Offset: 0x002FB34C
				public AddSplitColumns AddSplitColumns(ProgramNode node)
				{
					AddSplitColumns? addSplitColumns = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddSplitColumns.CreateSafe(this._builders, node);
					if (addSplitColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AddSplitColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return addSplitColumns.Value;
				}

				// Token: 0x0600E069 RID: 57449 RVA: 0x002FD1A0 File Offset: 0x002FB3A0
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson AddColumnsFromJson(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson? addColumnsFromJson = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson.CreateSafe(this._builders, node);
					if (addColumnsFromJson == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AddColumnsFromJson but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return addColumnsFromJson.Value;
				}

				// Token: 0x0600E06A RID: 57450 RVA: 0x002FD1F4 File Offset: 0x002FB3F4
				public SplitColumn SplitColumn(ProgramNode node)
				{
					SplitColumn? splitColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SplitColumn.CreateSafe(this._builders, node);
					if (splitColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitColumn.Value;
				}

				// Token: 0x0600E06B RID: 57451 RVA: 0x002FD248 File Offset: 0x002FB448
				public SelectColumnToSplit SelectColumnToSplit(ProgramNode node)
				{
					SelectColumnToSplit? selectColumnToSplit = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SelectColumnToSplit.CreateSafe(this._builders, node);
					if (selectColumnToSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectColumnToSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectColumnToSplit.Value;
				}

				// Token: 0x0600E06C RID: 57452 RVA: 0x002FD29C File Offset: 0x002FB49C
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split Split(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split? split = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return split.Value;
				}

				// Token: 0x04005529 RID: 21801
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A97 RID: 6807
			public class NodeIs
			{
				// Token: 0x0600E06D RID: 57453 RVA: 0x002FD2ED File Offset: 0x002FB4ED
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E06E RID: 57454 RVA: 0x002FD2FC File Offset: 0x002FB4FC
				public bool @out(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E06F RID: 57455 RVA: 0x002FD320 File Offset: 0x002FB520
				public bool @out(ProgramNode node, out @out value)
				{
					@out? @out = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out.CreateSafe(this._builders, node);
					if (@out == null)
					{
						value = default(@out);
						return false;
					}
					value = @out.Value;
					return true;
				}

				// Token: 0x0600E070 RID: 57456 RVA: 0x002FD35C File Offset: 0x002FB55C
				public bool table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E071 RID: 57457 RVA: 0x002FD380 File Offset: 0x002FB580
				public bool table(ProgramNode node, out table value)
				{
					table? table = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table.CreateSafe(this._builders, node);
					if (table == null)
					{
						value = default(table);
						return false;
					}
					value = table.Value;
					return true;
				}

				// Token: 0x0600E072 RID: 57458 RVA: 0x002FD3BC File Offset: 0x002FB5BC
				public bool newColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E073 RID: 57459 RVA: 0x002FD3E0 File Offset: 0x002FB5E0
				public bool newColumns(ProgramNode node, out newColumns value)
				{
					newColumns? newColumns = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns.CreateSafe(this._builders, node);
					if (newColumns == null)
					{
						value = default(newColumns);
						return false;
					}
					value = newColumns.Value;
					return true;
				}

				// Token: 0x0600E074 RID: 57460 RVA: 0x002FD41C File Offset: 0x002FB61C
				public bool v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E075 RID: 57461 RVA: 0x002FD440 File Offset: 0x002FB640
				public bool v(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v value)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v? v = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateSafe(this._builders, node);
					if (v == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v);
						return false;
					}
					value = v.Value;
					return true;
				}

				// Token: 0x0600E076 RID: 57462 RVA: 0x002FD47C File Offset: 0x002FB67C
				public bool columnToSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E077 RID: 57463 RVA: 0x002FD4A0 File Offset: 0x002FB6A0
				public bool columnToSplit(ProgramNode node, out columnToSplit value)
				{
					columnToSplit? columnToSplit = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit.CreateSafe(this._builders, node);
					if (columnToSplit == null)
					{
						value = default(columnToSplit);
						return false;
					}
					value = columnToSplit.Value;
					return true;
				}

				// Token: 0x0600E078 RID: 57464 RVA: 0x002FD4DC File Offset: 0x002FB6DC
				public bool splitCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E079 RID: 57465 RVA: 0x002FD500 File Offset: 0x002FB700
				public bool splitCell(ProgramNode node, out splitCell value)
				{
					splitCell? splitCell = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell.CreateSafe(this._builders, node);
					if (splitCell == null)
					{
						value = default(splitCell);
						return false;
					}
					value = splitCell.Value;
					return true;
				}

				// Token: 0x0600E07A RID: 57466 RVA: 0x002FD53C File Offset: 0x002FB73C
				public bool sourceColumnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E07B RID: 57467 RVA: 0x002FD560 File Offset: 0x002FB760
				public bool sourceColumnName(ProgramNode node, out sourceColumnName value)
				{
					sourceColumnName? sourceColumnName = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName.CreateSafe(this._builders, node);
					if (sourceColumnName == null)
					{
						value = default(sourceColumnName);
						return false;
					}
					value = sourceColumnName.Value;
					return true;
				}

				// Token: 0x0600E07C RID: 57468 RVA: 0x002FD59C File Offset: 0x002FB79C
				public bool richDataType(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E07D RID: 57469 RVA: 0x002FD5C0 File Offset: 0x002FB7C0
				public bool richDataType(ProgramNode node, out richDataType value)
				{
					richDataType? richDataType = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType.CreateSafe(this._builders, node);
					if (richDataType == null)
					{
						value = default(richDataType);
						return false;
					}
					value = richDataType.Value;
					return true;
				}

				// Token: 0x0600E07E RID: 57470 RVA: 0x002FD5FC File Offset: 0x002FB7FC
				public bool fillMethod(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E07F RID: 57471 RVA: 0x002FD620 File Offset: 0x002FB820
				public bool fillMethod(ProgramNode node, out fillMethod value)
				{
					fillMethod? fillMethod = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod.CreateSafe(this._builders, node);
					if (fillMethod == null)
					{
						value = default(fillMethod);
						return false;
					}
					value = fillMethod.Value;
					return true;
				}

				// Token: 0x0600E080 RID: 57472 RVA: 0x002FD65C File Offset: 0x002FB85C
				public bool dropCondition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E081 RID: 57473 RVA: 0x002FD680 File Offset: 0x002FB880
				public bool dropCondition(ProgramNode node, out dropCondition value)
				{
					dropCondition? dropCondition = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition.CreateSafe(this._builders, node);
					if (dropCondition == null)
					{
						value = default(dropCondition);
						return false;
					}
					value = dropCondition.Value;
					return true;
				}

				// Token: 0x0600E082 RID: 57474 RVA: 0x002FD6BC File Offset: 0x002FB8BC
				public bool fillValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E083 RID: 57475 RVA: 0x002FD6E0 File Offset: 0x002FB8E0
				public bool fillValue(ProgramNode node, out fillValue value)
				{
					fillValue? fillValue = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue.CreateSafe(this._builders, node);
					if (fillValue == null)
					{
						value = default(fillValue);
						return false;
					}
					value = fillValue.Value;
					return true;
				}

				// Token: 0x0600E084 RID: 57476 RVA: 0x002FD71C File Offset: 0x002FB91C
				public bool missingValueMarkers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E085 RID: 57477 RVA: 0x002FD740 File Offset: 0x002FB940
				public bool missingValueMarkers(ProgramNode node, out missingValueMarkers value)
				{
					missingValueMarkers? missingValueMarkers = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers.CreateSafe(this._builders, node);
					if (missingValueMarkers == null)
					{
						value = default(missingValueMarkers);
						return false;
					}
					value = missingValueMarkers.Value;
					return true;
				}

				// Token: 0x0600E086 RID: 57478 RVA: 0x002FD77C File Offset: 0x002FB97C
				public bool isMixedColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E087 RID: 57479 RVA: 0x002FD7A0 File Offset: 0x002FB9A0
				public bool isMixedColumn(ProgramNode node, out isMixedColumn value)
				{
					isMixedColumn? isMixedColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn.CreateSafe(this._builders, node);
					if (isMixedColumn == null)
					{
						value = default(isMixedColumn);
						return false;
					}
					value = isMixedColumn.Value;
					return true;
				}

				// Token: 0x0600E088 RID: 57480 RVA: 0x002FD7DC File Offset: 0x002FB9DC
				public bool delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E089 RID: 57481 RVA: 0x002FD800 File Offset: 0x002FBA00
				public bool delimiter(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter value)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter? delimiter = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter);
						return false;
					}
					value = delimiter.Value;
					return true;
				}

				// Token: 0x0600E08A RID: 57482 RVA: 0x002FD83C File Offset: 0x002FBA3C
				public bool ejsonProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E08B RID: 57483 RVA: 0x002FD860 File Offset: 0x002FBA60
				public bool ejsonProgram(ProgramNode node, out ejsonProgram value)
				{
					ejsonProgram? ejsonProgram = Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram.CreateSafe(this._builders, node);
					if (ejsonProgram == null)
					{
						value = default(ejsonProgram);
						return false;
					}
					value = ejsonProgram.Value;
					return true;
				}

				// Token: 0x0400552A RID: 21802
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A98 RID: 6808
			public class RuleIs
			{
				// Token: 0x0600E08C RID: 57484 RVA: 0x002FD89A File Offset: 0x002FBA9A
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E08D RID: 57485 RVA: 0x002FD8AC File Offset: 0x002FBAAC
				public bool TTableProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.TTableProgram.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E08E RID: 57486 RVA: 0x002FD8D0 File Offset: 0x002FBAD0
				public bool TTableProgram(ProgramNode node, out TTableProgram value)
				{
					TTableProgram? ttableProgram = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.TTableProgram.CreateSafe(this._builders, node);
					if (ttableProgram == null)
					{
						value = default(TTableProgram);
						return false;
					}
					value = ttableProgram.Value;
					return true;
				}

				// Token: 0x0600E08F RID: 57487 RVA: 0x002FD90C File Offset: 0x002FBB0C
				public bool table_inputTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes.table_inputTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E090 RID: 57488 RVA: 0x002FD930 File Offset: 0x002FBB30
				public bool table_inputTable(ProgramNode node, out table_inputTable value)
				{
					table_inputTable? table_inputTable = Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes.table_inputTable.CreateSafe(this._builders, node);
					if (table_inputTable == null)
					{
						value = default(table_inputTable);
						return false;
					}
					value = table_inputTable.Value;
					return true;
				}

				// Token: 0x0600E091 RID: 57489 RVA: 0x002FD96C File Offset: 0x002FBB6C
				public bool LabelEncode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.LabelEncode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E092 RID: 57490 RVA: 0x002FD990 File Offset: 0x002FBB90
				public bool LabelEncode(ProgramNode node, out LabelEncode value)
				{
					LabelEncode? labelEncode = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.LabelEncode.CreateSafe(this._builders, node);
					if (labelEncode == null)
					{
						value = default(LabelEncode);
						return false;
					}
					value = labelEncode.Value;
					return true;
				}

				// Token: 0x0600E093 RID: 57491 RVA: 0x002FD9CC File Offset: 0x002FBBCC
				public bool OneHotEncode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.OneHotEncode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E094 RID: 57492 RVA: 0x002FD9F0 File Offset: 0x002FBBF0
				public bool OneHotEncode(ProgramNode node, out OneHotEncode value)
				{
					OneHotEncode? oneHotEncode = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.OneHotEncode.CreateSafe(this._builders, node);
					if (oneHotEncode == null)
					{
						value = default(OneHotEncode);
						return false;
					}
					value = oneHotEncode.Value;
					return true;
				}

				// Token: 0x0600E095 RID: 57493 RVA: 0x002FDA2C File Offset: 0x002FBC2C
				public bool MultiLabelBinarizer(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E096 RID: 57494 RVA: 0x002FDA50 File Offset: 0x002FBC50
				public bool MultiLabelBinarizer(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer value)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer? multiLabelBinarizer = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer.CreateSafe(this._builders, node);
					if (multiLabelBinarizer == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer);
						return false;
					}
					value = multiLabelBinarizer.Value;
					return true;
				}

				// Token: 0x0600E097 RID: 57495 RVA: 0x002FDA8C File Offset: 0x002FBC8C
				public bool CastColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.CastColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E098 RID: 57496 RVA: 0x002FDAB0 File Offset: 0x002FBCB0
				public bool CastColumn(ProgramNode node, out CastColumn value)
				{
					CastColumn? castColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.CastColumn.CreateSafe(this._builders, node);
					if (castColumn == null)
					{
						value = default(CastColumn);
						return false;
					}
					value = castColumn.Value;
					return true;
				}

				// Token: 0x0600E099 RID: 57497 RVA: 0x002FDAEC File Offset: 0x002FBCEC
				public bool FillMissingValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.FillMissingValues.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E09A RID: 57498 RVA: 0x002FDB10 File Offset: 0x002FBD10
				public bool FillMissingValues(ProgramNode node, out FillMissingValues value)
				{
					FillMissingValues? fillMissingValues = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.FillMissingValues.CreateSafe(this._builders, node);
					if (fillMissingValues == null)
					{
						value = default(FillMissingValues);
						return false;
					}
					value = fillMissingValues.Value;
					return true;
				}

				// Token: 0x0600E09B RID: 57499 RVA: 0x002FDB4C File Offset: 0x002FBD4C
				public bool DropColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E09C RID: 57500 RVA: 0x002FDB70 File Offset: 0x002FBD70
				public bool DropColumn(ProgramNode node, out DropColumn value)
				{
					DropColumn? dropColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropColumn.CreateSafe(this._builders, node);
					if (dropColumn == null)
					{
						value = default(DropColumn);
						return false;
					}
					value = dropColumn.Value;
					return true;
				}

				// Token: 0x0600E09D RID: 57501 RVA: 0x002FDBAC File Offset: 0x002FBDAC
				public bool DropRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E09E RID: 57502 RVA: 0x002FDBD0 File Offset: 0x002FBDD0
				public bool DropRows(ProgramNode node, out DropRows value)
				{
					DropRows? dropRows = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropRows.CreateSafe(this._builders, node);
					if (dropRows == null)
					{
						value = default(DropRows);
						return false;
					}
					value = dropRows.Value;
					return true;
				}

				// Token: 0x0600E09F RID: 57503 RVA: 0x002FDC0C File Offset: 0x002FBE0C
				public bool AddSplitColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddSplitColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E0A0 RID: 57504 RVA: 0x002FDC30 File Offset: 0x002FBE30
				public bool AddSplitColumns(ProgramNode node, out AddSplitColumns value)
				{
					AddSplitColumns? addSplitColumns = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddSplitColumns.CreateSafe(this._builders, node);
					if (addSplitColumns == null)
					{
						value = default(AddSplitColumns);
						return false;
					}
					value = addSplitColumns.Value;
					return true;
				}

				// Token: 0x0600E0A1 RID: 57505 RVA: 0x002FDC6C File Offset: 0x002FBE6C
				public bool AddColumnsFromJson(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E0A2 RID: 57506 RVA: 0x002FDC90 File Offset: 0x002FBE90
				public bool AddColumnsFromJson(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson value)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson? addColumnsFromJson = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson.CreateSafe(this._builders, node);
					if (addColumnsFromJson == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson);
						return false;
					}
					value = addColumnsFromJson.Value;
					return true;
				}

				// Token: 0x0600E0A3 RID: 57507 RVA: 0x002FDCCC File Offset: 0x002FBECC
				public bool SplitColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SplitColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E0A4 RID: 57508 RVA: 0x002FDCF0 File Offset: 0x002FBEF0
				public bool SplitColumn(ProgramNode node, out SplitColumn value)
				{
					SplitColumn? splitColumn = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SplitColumn.CreateSafe(this._builders, node);
					if (splitColumn == null)
					{
						value = default(SplitColumn);
						return false;
					}
					value = splitColumn.Value;
					return true;
				}

				// Token: 0x0600E0A5 RID: 57509 RVA: 0x002FDD2C File Offset: 0x002FBF2C
				public bool SelectColumnToSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SelectColumnToSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E0A6 RID: 57510 RVA: 0x002FDD50 File Offset: 0x002FBF50
				public bool SelectColumnToSplit(ProgramNode node, out SelectColumnToSplit value)
				{
					SelectColumnToSplit? selectColumnToSplit = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SelectColumnToSplit.CreateSafe(this._builders, node);
					if (selectColumnToSplit == null)
					{
						value = default(SelectColumnToSplit);
						return false;
					}
					value = selectColumnToSplit.Value;
					return true;
				}

				// Token: 0x0600E0A7 RID: 57511 RVA: 0x002FDD8C File Offset: 0x002FBF8C
				public bool Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600E0A8 RID: 57512 RVA: 0x002FDDB0 File Offset: 0x002FBFB0
				public bool Split(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split value)
				{
					Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split? split = Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
					if (split == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split);
						return false;
					}
					value = split.Value;
					return true;
				}

				// Token: 0x0400552B RID: 21803
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A99 RID: 6809
			public class NodeAs
			{
				// Token: 0x0600E0A9 RID: 57513 RVA: 0x002FDDEA File Offset: 0x002FBFEA
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0AA RID: 57514 RVA: 0x002FDDF9 File Offset: 0x002FBFF9
				public @out? @out(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0AB RID: 57515 RVA: 0x002FDE07 File Offset: 0x002FC007
				public table? table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0AC RID: 57516 RVA: 0x002FDE15 File Offset: 0x002FC015
				public newColumns? newColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0AD RID: 57517 RVA: 0x002FDE23 File Offset: 0x002FC023
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v? v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0AE RID: 57518 RVA: 0x002FDE31 File Offset: 0x002FC031
				public columnToSplit? columnToSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0AF RID: 57519 RVA: 0x002FDE3F File Offset: 0x002FC03F
				public splitCell? splitCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B0 RID: 57520 RVA: 0x002FDE4D File Offset: 0x002FC04D
				public sourceColumnName? sourceColumnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B1 RID: 57521 RVA: 0x002FDE5B File Offset: 0x002FC05B
				public richDataType? richDataType(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B2 RID: 57522 RVA: 0x002FDE69 File Offset: 0x002FC069
				public fillMethod? fillMethod(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B3 RID: 57523 RVA: 0x002FDE77 File Offset: 0x002FC077
				public dropCondition? dropCondition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B4 RID: 57524 RVA: 0x002FDE85 File Offset: 0x002FC085
				public fillValue? fillValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B5 RID: 57525 RVA: 0x002FDE93 File Offset: 0x002FC093
				public missingValueMarkers? missingValueMarkers(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B6 RID: 57526 RVA: 0x002FDEA1 File Offset: 0x002FC0A1
				public isMixedColumn? isMixedColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B7 RID: 57527 RVA: 0x002FDEAF File Offset: 0x002FC0AF
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter? delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0B8 RID: 57528 RVA: 0x002FDEBD File Offset: 0x002FC0BD
				public ejsonProgram? ejsonProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram.CreateSafe(this._builders, node);
				}

				// Token: 0x0400552C RID: 21804
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A9A RID: 6810
			public class RuleAs
			{
				// Token: 0x0600E0B9 RID: 57529 RVA: 0x002FDECB File Offset: 0x002FC0CB
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0BA RID: 57530 RVA: 0x002FDEDA File Offset: 0x002FC0DA
				public TTableProgram? TTableProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.TTableProgram.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0BB RID: 57531 RVA: 0x002FDEE8 File Offset: 0x002FC0E8
				public table_inputTable? table_inputTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes.table_inputTable.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0BC RID: 57532 RVA: 0x002FDEF6 File Offset: 0x002FC0F6
				public LabelEncode? LabelEncode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.LabelEncode.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0BD RID: 57533 RVA: 0x002FDF04 File Offset: 0x002FC104
				public OneHotEncode? OneHotEncode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.OneHotEncode.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0BE RID: 57534 RVA: 0x002FDF12 File Offset: 0x002FC112
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer? MultiLabelBinarizer(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.MultiLabelBinarizer.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0BF RID: 57535 RVA: 0x002FDF20 File Offset: 0x002FC120
				public CastColumn? CastColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.CastColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C0 RID: 57536 RVA: 0x002FDF2E File Offset: 0x002FC12E
				public FillMissingValues? FillMissingValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.FillMissingValues.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C1 RID: 57537 RVA: 0x002FDF3C File Offset: 0x002FC13C
				public DropColumn? DropColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C2 RID: 57538 RVA: 0x002FDF4A File Offset: 0x002FC14A
				public DropRows? DropRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.DropRows.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C3 RID: 57539 RVA: 0x002FDF58 File Offset: 0x002FC158
				public AddSplitColumns? AddSplitColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddSplitColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C4 RID: 57540 RVA: 0x002FDF66 File Offset: 0x002FC166
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson? AddColumnsFromJson(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.AddColumnsFromJson.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C5 RID: 57541 RVA: 0x002FDF74 File Offset: 0x002FC174
				public SplitColumn? SplitColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SplitColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C6 RID: 57542 RVA: 0x002FDF82 File Offset: 0x002FC182
				public SelectColumnToSplit? SelectColumnToSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.SelectColumnToSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x0600E0C7 RID: 57543 RVA: 0x002FDF90 File Offset: 0x002FC190
				public Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split? Split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split.CreateSafe(this._builders, node);
				}

				// Token: 0x0400552D RID: 21805
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02001A9C RID: 6812
		public class Sets
		{
			// Token: 0x0600E0CB RID: 57547 RVA: 0x002FDFB8 File Offset: 0x002FC1B8
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x1700259F RID: 9631
			// (get) Token: 0x0600E0CC RID: 57548 RVA: 0x002FE007 File Offset: 0x002FC207
			// (set) Token: 0x0600E0CD RID: 57549 RVA: 0x002FE00F File Offset: 0x002FC20F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x170025A0 RID: 9632
			// (get) Token: 0x0600E0CE RID: 57550 RVA: 0x002FE018 File Offset: 0x002FC218
			// (set) Token: 0x0600E0CF RID: 57551 RVA: 0x002FE020 File Offset: 0x002FC220
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x170025A1 RID: 9633
			// (get) Token: 0x0600E0D0 RID: 57552 RVA: 0x002FE029 File Offset: 0x002FC229
			// (set) Token: 0x0600E0D1 RID: 57553 RVA: 0x002FE031 File Offset: 0x002FC231
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x170025A2 RID: 9634
			// (get) Token: 0x0600E0D2 RID: 57554 RVA: 0x002FE03A File Offset: 0x002FC23A
			// (set) Token: 0x0600E0D3 RID: 57555 RVA: 0x002FE042 File Offset: 0x002FC242
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x170025A3 RID: 9635
			// (get) Token: 0x0600E0D4 RID: 57556 RVA: 0x002FE04B File Offset: 0x002FC24B
			// (set) Token: 0x0600E0D5 RID: 57557 RVA: 0x002FE053 File Offset: 0x002FC253
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02001A9D RID: 6813
			public class Joins
			{
				// Token: 0x0600E0D6 RID: 57558 RVA: 0x002FE05C File Offset: 0x002FC25C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0D7 RID: 57559 RVA: 0x002FE06B File Offset: 0x002FC26B
				public ProgramSetBuilder<table> LabelEncode(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LabelEncode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0D8 RID: 57560 RVA: 0x002FE0AB File Offset: 0x002FC2AB
				public ProgramSetBuilder<table> OneHotEncode(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.OneHotEncode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0D9 RID: 57561 RVA: 0x002FE0EC File Offset: 0x002FC2EC
				public ProgramSetBuilder<table> MultiLabelBinarizer(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter> value2)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MultiLabelBinarizer, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0DA RID: 57562 RVA: 0x002FE148 File Offset: 0x002FC348
				public ProgramSetBuilder<table> CastColumn(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<richDataType> value2, ProgramSetBuilder<isMixedColumn> value3)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.CastColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600E0DB RID: 57563 RVA: 0x002FE1B4 File Offset: 0x002FC3B4
				public ProgramSetBuilder<table> FillMissingValues(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<fillValue> value2, ProgramSetBuilder<missingValueMarkers> value3, ProgramSetBuilder<fillMethod> value4)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FillMissingValues, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600E0DC RID: 57564 RVA: 0x002FE230 File Offset: 0x002FC430
				public ProgramSetBuilder<table> DropColumn(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<dropCondition> value2)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DropColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0DD RID: 57565 RVA: 0x002FE28A File Offset: 0x002FC48A
				public ProgramSetBuilder<table> DropRows(ProgramSetBuilder<table> value0, ProgramSetBuilder<dropCondition> value1)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DropRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0DE RID: 57566 RVA: 0x002FE2CC File Offset: 0x002FC4CC
				public ProgramSetBuilder<table> AddSplitColumns(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<newColumns> value2)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AddSplitColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0DF RID: 57567 RVA: 0x002FE328 File Offset: 0x002FC528
				public ProgramSetBuilder<table> AddColumnsFromJson(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<ejsonProgram> value2)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AddColumnsFromJson, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0E0 RID: 57568 RVA: 0x002FE382 File Offset: 0x002FC582
				public ProgramSetBuilder<columnToSplit> SelectColumnToSplit(ProgramSetBuilder<inputTable> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return ProgramSetBuilder<columnToSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectColumnToSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0E1 RID: 57569 RVA: 0x002FE3C2 File Offset: 0x002FC5C2
				public ProgramSetBuilder<newColumns> SplitColumn(ProgramSetBuilder<splitCell> value0, ProgramSetBuilder<columnToSplit> value1)
				{
					return ProgramSetBuilder<newColumns>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0E2 RID: 57570 RVA: 0x002FE402 File Offset: 0x002FC602
				public ProgramSetBuilder<@out> TTableProgram(ProgramSetBuilder<table> value0)
				{
					return ProgramSetBuilder<@out>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TTableProgram, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600E0E3 RID: 57571 RVA: 0x002FE433 File Offset: 0x002FC633
				public ProgramSetBuilder<splitCell> Split(ProgramSetBuilder<regionSplit> value0)
				{
					return ProgramSetBuilder<splitCell>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Split, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005534 RID: 21812
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A9E RID: 6814
			public class ExplicitJoins
			{
				// Token: 0x0600E0E4 RID: 57572 RVA: 0x002FE464 File Offset: 0x002FC664
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0E5 RID: 57573 RVA: 0x002FE473 File Offset: 0x002FC673
				public JoinProgramSetBuilder<table> LabelEncode(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LabelEncode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0E6 RID: 57574 RVA: 0x002FE4B3 File Offset: 0x002FC6B3
				public JoinProgramSetBuilder<table> OneHotEncode(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.OneHotEncode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0E7 RID: 57575 RVA: 0x002FE4F4 File Offset: 0x002FC6F4
				public JoinProgramSetBuilder<table> MultiLabelBinarizer(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter> value2)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MultiLabelBinarizer, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0E8 RID: 57576 RVA: 0x002FE550 File Offset: 0x002FC750
				public JoinProgramSetBuilder<table> CastColumn(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<richDataType> value2, ProgramSetBuilder<isMixedColumn> value3)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.CastColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0600E0E9 RID: 57577 RVA: 0x002FE5BC File Offset: 0x002FC7BC
				public JoinProgramSetBuilder<table> FillMissingValues(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<fillValue> value2, ProgramSetBuilder<missingValueMarkers> value3, ProgramSetBuilder<fillMethod> value4)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FillMissingValues, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600E0EA RID: 57578 RVA: 0x002FE638 File Offset: 0x002FC838
				public JoinProgramSetBuilder<table> DropColumn(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<dropCondition> value2)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DropColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0EB RID: 57579 RVA: 0x002FE692 File Offset: 0x002FC892
				public JoinProgramSetBuilder<table> DropRows(ProgramSetBuilder<table> value0, ProgramSetBuilder<dropCondition> value1)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DropRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0EC RID: 57580 RVA: 0x002FE6D4 File Offset: 0x002FC8D4
				public JoinProgramSetBuilder<table> AddSplitColumns(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<newColumns> value2)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AddSplitColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0ED RID: 57581 RVA: 0x002FE730 File Offset: 0x002FC930
				public JoinProgramSetBuilder<table> AddColumnsFromJson(ProgramSetBuilder<table> value0, ProgramSetBuilder<sourceColumnName> value1, ProgramSetBuilder<ejsonProgram> value2)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AddColumnsFromJson, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600E0EE RID: 57582 RVA: 0x002FE78A File Offset: 0x002FC98A
				public JoinProgramSetBuilder<columnToSplit> SelectColumnToSplit(ProgramSetBuilder<inputTable> value0, ProgramSetBuilder<sourceColumnName> value1)
				{
					return JoinProgramSetBuilder<columnToSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectColumnToSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0EF RID: 57583 RVA: 0x002FE7CA File Offset: 0x002FC9CA
				public JoinProgramSetBuilder<newColumns> SplitColumn(ProgramSetBuilder<splitCell> value0, ProgramSetBuilder<columnToSplit> value1)
				{
					return JoinProgramSetBuilder<newColumns>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitColumn, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600E0F0 RID: 57584 RVA: 0x002FE80A File Offset: 0x002FCA0A
				public JoinProgramSetBuilder<@out> TTableProgram(ProgramSetBuilder<table> value0)
				{
					return JoinProgramSetBuilder<@out>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TTableProgram, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600E0F1 RID: 57585 RVA: 0x002FE83B File Offset: 0x002FCA3B
				public JoinProgramSetBuilder<splitCell> Split(ProgramSetBuilder<regionSplit> value0)
				{
					return JoinProgramSetBuilder<splitCell>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Split, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005535 RID: 21813
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A9F RID: 6815
			public class JoinUnnamedConversions
			{
				// Token: 0x0600E0F2 RID: 57586 RVA: 0x002FE86C File Offset: 0x002FCA6C
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0F3 RID: 57587 RVA: 0x002FE87B File Offset: 0x002FCA7B
				public ProgramSetBuilder<table> table_inputTable(ProgramSetBuilder<inputTable> value0)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.table_inputTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005536 RID: 21814
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001AA0 RID: 6816
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x0600E0F4 RID: 57588 RVA: 0x002FE8AC File Offset: 0x002FCAAC
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0F5 RID: 57589 RVA: 0x002FE8BB File Offset: 0x002FCABB
				public JoinProgramSetBuilder<table> table_inputTable(ProgramSetBuilder<inputTable> value0)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.table_inputTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005537 RID: 21815
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001AA1 RID: 6817
			public class Casts
			{
				// Token: 0x0600E0F6 RID: 57590 RVA: 0x002FE8EC File Offset: 0x002FCAEC
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600E0F7 RID: 57591 RVA: 0x002FE8FC File Offset: 0x002FCAFC
				public ProgramSetBuilder<@out> @out(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.@out)
					{
						string text = "set";
						string text2 = "expected program set for symbol @out but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.@out>.CreateUnsafe(set);
				}

				// Token: 0x0600E0F8 RID: 57592 RVA: 0x002FE954 File Offset: 0x002FCB54
				public ProgramSetBuilder<table> table(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.table)
					{
						string text = "set";
						string text2 = "expected program set for symbol table but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.table>.CreateUnsafe(set);
				}

				// Token: 0x0600E0F9 RID: 57593 RVA: 0x002FE9AC File Offset: 0x002FCBAC
				public ProgramSetBuilder<newColumns> newColumns(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.newColumns)
					{
						string text = "set";
						string text2 = "expected program set for symbol newColumns but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.newColumns>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FA RID: 57594 RVA: 0x002FEA04 File Offset: 0x002FCC04
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v> v(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.v)
					{
						string text = "set";
						string text2 = "expected program set for symbol v but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.v>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FB RID: 57595 RVA: 0x002FEA5C File Offset: 0x002FCC5C
				public ProgramSetBuilder<columnToSplit> columnToSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnToSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnToSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.columnToSplit>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FC RID: 57596 RVA: 0x002FEAB4 File Offset: 0x002FCCB4
				public ProgramSetBuilder<splitCell> splitCell(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitCell)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitCell but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.splitCell>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FD RID: 57597 RVA: 0x002FEB0C File Offset: 0x002FCD0C
				public ProgramSetBuilder<sourceColumnName> sourceColumnName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sourceColumnName)
					{
						string text = "set";
						string text2 = "expected program set for symbol sourceColumnName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.sourceColumnName>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FE RID: 57598 RVA: 0x002FEB64 File Offset: 0x002FCD64
				public ProgramSetBuilder<richDataType> richDataType(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.richDataType)
					{
						string text = "set";
						string text2 = "expected program set for symbol richDataType but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.richDataType>.CreateUnsafe(set);
				}

				// Token: 0x0600E0FF RID: 57599 RVA: 0x002FEBBC File Offset: 0x002FCDBC
				public ProgramSetBuilder<fillMethod> fillMethod(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fillMethod)
					{
						string text = "set";
						string text2 = "expected program set for symbol fillMethod but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillMethod>.CreateUnsafe(set);
				}

				// Token: 0x0600E100 RID: 57600 RVA: 0x002FEC14 File Offset: 0x002FCE14
				public ProgramSetBuilder<dropCondition> dropCondition(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dropCondition)
					{
						string text = "set";
						string text2 = "expected program set for symbol dropCondition but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.dropCondition>.CreateUnsafe(set);
				}

				// Token: 0x0600E101 RID: 57601 RVA: 0x002FEC6C File Offset: 0x002FCE6C
				public ProgramSetBuilder<fillValue> fillValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fillValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol fillValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.fillValue>.CreateUnsafe(set);
				}

				// Token: 0x0600E102 RID: 57602 RVA: 0x002FECC4 File Offset: 0x002FCEC4
				public ProgramSetBuilder<missingValueMarkers> missingValueMarkers(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.missingValueMarkers)
					{
						string text = "set";
						string text2 = "expected program set for symbol missingValueMarkers but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.missingValueMarkers>.CreateUnsafe(set);
				}

				// Token: 0x0600E103 RID: 57603 RVA: 0x002FED1C File Offset: 0x002FCF1C
				public ProgramSetBuilder<isMixedColumn> isMixedColumn(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.isMixedColumn)
					{
						string text = "set";
						string text2 = "expected program set for symbol isMixedColumn but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.isMixedColumn>.CreateUnsafe(set);
				}

				// Token: 0x0600E104 RID: 57604 RVA: 0x002FED74 File Offset: 0x002FCF74
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter> delimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.delimiter>.CreateUnsafe(set);
				}

				// Token: 0x0600E105 RID: 57605 RVA: 0x002FEDCC File Offset: 0x002FCFCC
				public ProgramSetBuilder<ejsonProgram> ejsonProgram(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.ejsonProgram)
					{
						string text = "set";
						string text2 = "expected program set for symbol ejsonProgram but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes.ejsonProgram>.CreateUnsafe(set);
				}

				// Token: 0x04005538 RID: 21816
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
