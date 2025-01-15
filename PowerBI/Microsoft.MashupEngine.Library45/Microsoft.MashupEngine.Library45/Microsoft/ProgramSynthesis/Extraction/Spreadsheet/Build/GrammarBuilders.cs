using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build
{
	// Token: 0x02000DEB RID: 3563
	public class GrammarBuilders
	{
		// Token: 0x06005A76 RID: 23158 RVA: 0x00130AA5 File Offset: 0x0012ECA5
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x00130AD1 File Offset: 0x0012ECD1
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005A78 RID: 23160 RVA: 0x00130ADE File Offset: 0x0012ECDE
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x00130AEB File Offset: 0x0012ECEB
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x00130AF8 File Offset: 0x0012ECF8
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x00130B05 File Offset: 0x0012ED05
		// (set) Token: 0x06005A7C RID: 23164 RVA: 0x00130B0D File Offset: 0x0012ED0D
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x00130B16 File Offset: 0x0012ED16
		// (set) Token: 0x06005A7E RID: 23166 RVA: 0x00130B1E File Offset: 0x0012ED1E
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06005A7F RID: 23167 RVA: 0x00130B28 File Offset: 0x0012ED28
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

		// Token: 0x04002A85 RID: 10885
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04002A86 RID: 10886
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04002A87 RID: 10887
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04002A88 RID: 10888
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04002A89 RID: 10889
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000DEC RID: 3564
		public class GrammarSymbols
		{
			// Token: 0x17001081 RID: 4225
			// (get) Token: 0x06005A81 RID: 23169 RVA: 0x00130BD3 File Offset: 0x0012EDD3
			// (set) Token: 0x06005A82 RID: 23170 RVA: 0x00130BDB File Offset: 0x0012EDDB
			public Symbol sheetPair { get; private set; }

			// Token: 0x17001082 RID: 4226
			// (get) Token: 0x06005A83 RID: 23171 RVA: 0x00130BE4 File Offset: 0x0012EDE4
			// (set) Token: 0x06005A84 RID: 23172 RVA: 0x00130BEC File Offset: 0x0012EDEC
			public Symbol output { get; private set; }

			// Token: 0x17001083 RID: 4227
			// (get) Token: 0x06005A85 RID: 23173 RVA: 0x00130BF5 File Offset: 0x0012EDF5
			// (set) Token: 0x06005A86 RID: 23174 RVA: 0x00130BFD File Offset: 0x0012EDFD
			public Symbol trim { get; private set; }

			// Token: 0x17001084 RID: 4228
			// (get) Token: 0x06005A87 RID: 23175 RVA: 0x00130C06 File Offset: 0x0012EE06
			// (set) Token: 0x06005A88 RID: 23176 RVA: 0x00130C0E File Offset: 0x0012EE0E
			public Symbol area { get; private set; }

			// Token: 0x17001085 RID: 4229
			// (get) Token: 0x06005A89 RID: 23177 RVA: 0x00130C17 File Offset: 0x0012EE17
			// (set) Token: 0x06005A8A RID: 23178 RVA: 0x00130C1F File Offset: 0x0012EE1F
			public Symbol trimLeft { get; private set; }

			// Token: 0x17001086 RID: 4230
			// (get) Token: 0x06005A8B RID: 23179 RVA: 0x00130C28 File Offset: 0x0012EE28
			// (set) Token: 0x06005A8C RID: 23180 RVA: 0x00130C30 File Offset: 0x0012EE30
			public Symbol trimBottom { get; private set; }

			// Token: 0x17001087 RID: 4231
			// (get) Token: 0x06005A8D RID: 23181 RVA: 0x00130C39 File Offset: 0x0012EE39
			// (set) Token: 0x06005A8E RID: 23182 RVA: 0x00130C41 File Offset: 0x0012EE41
			public Symbol trimTop { get; private set; }

			// Token: 0x17001088 RID: 4232
			// (get) Token: 0x06005A8F RID: 23183 RVA: 0x00130C4A File Offset: 0x0012EE4A
			// (set) Token: 0x06005A90 RID: 23184 RVA: 0x00130C52 File Offset: 0x0012EE52
			public Symbol sheetSection { get; private set; }

			// Token: 0x17001089 RID: 4233
			// (get) Token: 0x06005A91 RID: 23185 RVA: 0x00130C5B File Offset: 0x0012EE5B
			// (set) Token: 0x06005A92 RID: 23186 RVA: 0x00130C63 File Offset: 0x0012EE63
			public Symbol horizontalSheetSection { get; private set; }

			// Token: 0x1700108A RID: 4234
			// (get) Token: 0x06005A93 RID: 23187 RVA: 0x00130C6C File Offset: 0x0012EE6C
			// (set) Token: 0x06005A94 RID: 23188 RVA: 0x00130C74 File Offset: 0x0012EE74
			public Symbol verticalSheetSection { get; private set; }

			// Token: 0x1700108B RID: 4235
			// (get) Token: 0x06005A95 RID: 23189 RVA: 0x00130C7D File Offset: 0x0012EE7D
			// (set) Token: 0x06005A96 RID: 23190 RVA: 0x00130C85 File Offset: 0x0012EE85
			public Symbol uncleanedSheetSection { get; private set; }

			// Token: 0x1700108C RID: 4236
			// (get) Token: 0x06005A97 RID: 23191 RVA: 0x00130C8E File Offset: 0x0012EE8E
			// (set) Token: 0x06005A98 RID: 23192 RVA: 0x00130C96 File Offset: 0x0012EE96
			public Symbol wholeSheet { get; private set; }

			// Token: 0x1700108D RID: 4237
			// (get) Token: 0x06005A99 RID: 23193 RVA: 0x00130C9F File Offset: 0x0012EE9F
			// (set) Token: 0x06005A9A RID: 23194 RVA: 0x00130CA7 File Offset: 0x0012EEA7
			public Symbol wholeSheetFull { get; private set; }

			// Token: 0x1700108E RID: 4238
			// (get) Token: 0x06005A9B RID: 23195 RVA: 0x00130CB0 File Offset: 0x0012EEB0
			// (set) Token: 0x06005A9C RID: 23196 RVA: 0x00130CB8 File Offset: 0x0012EEB8
			public Symbol sheet { get; private set; }

			// Token: 0x1700108F RID: 4239
			// (get) Token: 0x06005A9D RID: 23197 RVA: 0x00130CC1 File Offset: 0x0012EEC1
			// (set) Token: 0x06005A9E RID: 23198 RVA: 0x00130CC9 File Offset: 0x0012EEC9
			public Symbol horizontalSheetSplits { get; private set; }

			// Token: 0x17001090 RID: 4240
			// (get) Token: 0x06005A9F RID: 23199 RVA: 0x00130CD2 File Offset: 0x0012EED2
			// (set) Token: 0x06005AA0 RID: 23200 RVA: 0x00130CDA File Offset: 0x0012EEDA
			public Symbol verticalSheetSplits { get; private set; }

			// Token: 0x17001091 RID: 4241
			// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x00130CE3 File Offset: 0x0012EEE3
			// (set) Token: 0x06005AA2 RID: 23202 RVA: 0x00130CEB File Offset: 0x0012EEEB
			public Symbol sheetSplits { get; private set; }

			// Token: 0x17001092 RID: 4242
			// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x00130CF4 File Offset: 0x0012EEF4
			// (set) Token: 0x06005AA4 RID: 23204 RVA: 0x00130CFC File Offset: 0x0012EEFC
			public Symbol index { get; private set; }

			// Token: 0x17001093 RID: 4243
			// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x00130D05 File Offset: 0x0012EF05
			// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x00130D0D File Offset: 0x0012EF0D
			public Symbol rangeName { get; private set; }

			// Token: 0x17001094 RID: 4244
			// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x00130D16 File Offset: 0x0012EF16
			// (set) Token: 0x06005AA8 RID: 23208 RVA: 0x00130D1E File Offset: 0x0012EF1E
			public Symbol k { get; private set; }

			// Token: 0x17001095 RID: 4245
			// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x00130D27 File Offset: 0x0012EF27
			// (set) Token: 0x06005AAA RID: 23210 RVA: 0x00130D2F File Offset: 0x0012EF2F
			public Symbol splitMode { get; private set; }

			// Token: 0x17001096 RID: 4246
			// (get) Token: 0x06005AAB RID: 23211 RVA: 0x00130D38 File Offset: 0x0012EF38
			// (set) Token: 0x06005AAC RID: 23212 RVA: 0x00130D40 File Offset: 0x0012EF40
			public Symbol styleFilter { get; private set; }

			// Token: 0x17001097 RID: 4247
			// (get) Token: 0x06005AAD RID: 23213 RVA: 0x00130D49 File Offset: 0x0012EF49
			// (set) Token: 0x06005AAE RID: 23214 RVA: 0x00130D51 File Offset: 0x0012EF51
			public Symbol mProgram { get; private set; }

			// Token: 0x17001098 RID: 4248
			// (get) Token: 0x06005AAF RID: 23215 RVA: 0x00130D5A File Offset: 0x0012EF5A
			// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x00130D62 File Offset: 0x0012EF62
			public Symbol mTable { get; private set; }

			// Token: 0x17001099 RID: 4249
			// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x00130D6B File Offset: 0x0012EF6B
			// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x00130D73 File Offset: 0x0012EF73
			public Symbol mSection { get; private set; }

			// Token: 0x1700109A RID: 4250
			// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x00130D7C File Offset: 0x0012EF7C
			// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x00130D84 File Offset: 0x0012EF84
			public Symbol withoutFormatting { get; private set; }

			// Token: 0x1700109B RID: 4251
			// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x00130D8D File Offset: 0x0012EF8D
			// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x00130D95 File Offset: 0x0012EF95
			public Symbol startTitle { get; private set; }

			// Token: 0x1700109C RID: 4252
			// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x00130D9E File Offset: 0x0012EF9E
			// (set) Token: 0x06005AB8 RID: 23224 RVA: 0x00130DA6 File Offset: 0x0012EFA6
			public Symbol title { get; private set; }

			// Token: 0x1700109D RID: 4253
			// (get) Token: 0x06005AB9 RID: 23225 RVA: 0x00130DAF File Offset: 0x0012EFAF
			// (set) Token: 0x06005ABA RID: 23226 RVA: 0x00130DB7 File Offset: 0x0012EFB7
			public Symbol aboveOrLeftmost { get; private set; }

			// Token: 0x1700109E RID: 4254
			// (get) Token: 0x06005ABB RID: 23227 RVA: 0x00130DC0 File Offset: 0x0012EFC0
			// (set) Token: 0x06005ABC RID: 23228 RVA: 0x00130DC8 File Offset: 0x0012EFC8
			public Symbol aboveOrOutput { get; private set; }

			// Token: 0x1700109F RID: 4255
			// (get) Token: 0x06005ABD RID: 23229 RVA: 0x00130DD1 File Offset: 0x0012EFD1
			// (set) Token: 0x06005ABE RID: 23230 RVA: 0x00130DD9 File Offset: 0x0012EFD9
			public Symbol aboveOrHeader { get; private set; }

			// Token: 0x170010A0 RID: 4256
			// (get) Token: 0x06005ABF RID: 23231 RVA: 0x00130DE2 File Offset: 0x0012EFE2
			// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x00130DEA File Offset: 0x0012EFEA
			public Symbol headerSection { get; private set; }

			// Token: 0x170010A1 RID: 4257
			// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x00130DF3 File Offset: 0x0012EFF3
			// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x00130DFB File Offset: 0x0012EFFB
			public Symbol splitForTitle { get; private set; }

			// Token: 0x170010A2 RID: 4258
			// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x00130E04 File Offset: 0x0012F004
			// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x00130E0C File Offset: 0x0012F00C
			public Symbol above { get; private set; }

			// Token: 0x170010A3 RID: 4259
			// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x00130E15 File Offset: 0x0012F015
			// (set) Token: 0x06005AC6 RID: 23238 RVA: 0x00130E1D File Offset: 0x0012F01D
			public Symbol titleOf { get; private set; }

			// Token: 0x170010A4 RID: 4260
			// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x00130E26 File Offset: 0x0012F026
			// (set) Token: 0x06005AC8 RID: 23240 RVA: 0x00130E2E File Offset: 0x0012F02E
			public Symbol titleAboveMode { get; private set; }

			// Token: 0x06005AC9 RID: 23241 RVA: 0x00130E38 File Offset: 0x0012F038
			public GrammarSymbols(Grammar grammar)
			{
				this.sheetPair = grammar.Symbol("sheetPair");
				this.output = grammar.Symbol("output");
				this.trim = grammar.Symbol("trim");
				this.area = grammar.Symbol("area");
				this.trimLeft = grammar.Symbol("trimLeft");
				this.trimBottom = grammar.Symbol("trimBottom");
				this.trimTop = grammar.Symbol("trimTop");
				this.sheetSection = grammar.Symbol("sheetSection");
				this.horizontalSheetSection = grammar.Symbol("horizontalSheetSection");
				this.verticalSheetSection = grammar.Symbol("verticalSheetSection");
				this.uncleanedSheetSection = grammar.Symbol("uncleanedSheetSection");
				this.wholeSheet = grammar.Symbol("wholeSheet");
				this.wholeSheetFull = grammar.Symbol("wholeSheetFull");
				this.sheet = grammar.Symbol("sheet");
				this.horizontalSheetSplits = grammar.Symbol("horizontalSheetSplits");
				this.verticalSheetSplits = grammar.Symbol("verticalSheetSplits");
				this.sheetSplits = grammar.Symbol("sheetSplits");
				this.index = grammar.Symbol("index");
				this.rangeName = grammar.Symbol("rangeName");
				this.k = grammar.Symbol("k");
				this.splitMode = grammar.Symbol("splitMode");
				this.styleFilter = grammar.Symbol("styleFilter");
				this.mProgram = grammar.Symbol("mProgram");
				this.mTable = grammar.Symbol("mTable");
				this.mSection = grammar.Symbol("mSection");
				this.withoutFormatting = grammar.Symbol("withoutFormatting");
				this.startTitle = grammar.Symbol("startTitle");
				this.title = grammar.Symbol("title");
				this.aboveOrLeftmost = grammar.Symbol("aboveOrLeftmost");
				this.aboveOrOutput = grammar.Symbol("aboveOrOutput");
				this.aboveOrHeader = grammar.Symbol("aboveOrHeader");
				this.headerSection = grammar.Symbol("headerSection");
				this.splitForTitle = grammar.Symbol("splitForTitle");
				this.above = grammar.Symbol("above");
				this.titleOf = grammar.Symbol("titleOf");
				this.titleAboveMode = grammar.Symbol("titleAboveMode");
			}
		}

		// Token: 0x02000DED RID: 3565
		public class GrammarRules
		{
			// Token: 0x170010A5 RID: 4261
			// (get) Token: 0x06005ACA RID: 23242 RVA: 0x001310AF File Offset: 0x0012F2AF
			// (set) Token: 0x06005ACB RID: 23243 RVA: 0x001310B7 File Offset: 0x0012F2B7
			public BlackBoxRule Trim { get; private set; }

			// Token: 0x170010A6 RID: 4262
			// (get) Token: 0x06005ACC RID: 23244 RVA: 0x001310C0 File Offset: 0x0012F2C0
			// (set) Token: 0x06005ACD RID: 23245 RVA: 0x001310C8 File Offset: 0x0012F2C8
			public BlackBoxRule TrimHidden { get; private set; }

			// Token: 0x170010A7 RID: 4263
			// (get) Token: 0x06005ACE RID: 23246 RVA: 0x001310D1 File Offset: 0x0012F2D1
			// (set) Token: 0x06005ACF RID: 23247 RVA: 0x001310D9 File Offset: 0x0012F2D9
			public BlackBoxRule DefinedRange { get; private set; }

			// Token: 0x170010A8 RID: 4264
			// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x001310E2 File Offset: 0x0012F2E2
			// (set) Token: 0x06005AD1 RID: 23249 RVA: 0x001310EA File Offset: 0x0012F2EA
			public BlackBoxRule TrimLeftSingleCellColumns { get; private set; }

			// Token: 0x170010A9 RID: 4265
			// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x001310F3 File Offset: 0x0012F2F3
			// (set) Token: 0x06005AD3 RID: 23251 RVA: 0x001310FB File Offset: 0x0012F2FB
			public BlackBoxRule TrimBottomSingleCellRows { get; private set; }

			// Token: 0x170010AA RID: 4266
			// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x00131104 File Offset: 0x0012F304
			// (set) Token: 0x06005AD5 RID: 23253 RVA: 0x0013110C File Offset: 0x0012F30C
			public BlackBoxRule TakeUntilEmptyRow { get; private set; }

			// Token: 0x170010AB RID: 4267
			// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x00131115 File Offset: 0x0012F315
			// (set) Token: 0x06005AD7 RID: 23255 RVA: 0x0013111D File Offset: 0x0012F31D
			public BlackBoxRule TrimAboveBottomBorder { get; private set; }

			// Token: 0x170010AC RID: 4268
			// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x00131126 File Offset: 0x0012F326
			// (set) Token: 0x06005AD9 RID: 23257 RVA: 0x0013112E File Offset: 0x0012F32E
			public BlackBoxRule FreezePaneTight { get; private set; }

			// Token: 0x170010AD RID: 4269
			// (get) Token: 0x06005ADA RID: 23258 RVA: 0x00131137 File Offset: 0x0012F337
			// (set) Token: 0x06005ADB RID: 23259 RVA: 0x0013113F File Offset: 0x0012F33F
			public BlackBoxRule FreezePaneToBlanks { get; private set; }

			// Token: 0x170010AE RID: 4270
			// (get) Token: 0x06005ADC RID: 23260 RVA: 0x00131148 File Offset: 0x0012F348
			// (set) Token: 0x06005ADD RID: 23261 RVA: 0x00131150 File Offset: 0x0012F350
			public BlackBoxRule FreezePaneToMultipleBlanks { get; private set; }

			// Token: 0x170010AF RID: 4271
			// (get) Token: 0x06005ADE RID: 23262 RVA: 0x00131159 File Offset: 0x0012F359
			// (set) Token: 0x06005ADF RID: 23263 RVA: 0x00131161 File Offset: 0x0012F361
			public BlackBoxRule TrimTopMergedCellRows { get; private set; }

			// Token: 0x170010B0 RID: 4272
			// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x0013116A File Offset: 0x0012F36A
			// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x00131172 File Offset: 0x0012F372
			public BlackBoxRule TrimTopFullWidthMergedCellRows { get; private set; }

			// Token: 0x170010B1 RID: 4273
			// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x0013117B File Offset: 0x0012F37B
			// (set) Token: 0x06005AE3 RID: 23267 RVA: 0x00131183 File Offset: 0x0012F383
			public BlackBoxRule TrimTopSingleCellRows { get; private set; }

			// Token: 0x170010B2 RID: 4274
			// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x0013118C File Offset: 0x0012F38C
			// (set) Token: 0x06005AE5 RID: 23269 RVA: 0x00131194 File Offset: 0x0012F394
			public BlackBoxRule TrimBelowTopBorder { get; private set; }

			// Token: 0x170010B3 RID: 4275
			// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x0013119D File Offset: 0x0012F39D
			// (set) Token: 0x06005AE7 RID: 23271 RVA: 0x001311A5 File Offset: 0x0012F3A5
			public BlackBoxRule TakeAfterEmptyRow { get; private set; }

			// Token: 0x170010B4 RID: 4276
			// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x001311AE File Offset: 0x0012F3AE
			// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x001311B6 File Offset: 0x0012F3B6
			public BlackBoxRule TakeUntilEmptyColumn { get; private set; }

			// Token: 0x170010B5 RID: 4277
			// (get) Token: 0x06005AEA RID: 23274 RVA: 0x001311BF File Offset: 0x0012F3BF
			// (set) Token: 0x06005AEB RID: 23275 RVA: 0x001311C7 File Offset: 0x0012F3C7
			public BlackBoxRule TrimRightSingleCellColumns { get; private set; }

			// Token: 0x170010B6 RID: 4278
			// (get) Token: 0x06005AEC RID: 23276 RVA: 0x001311D0 File Offset: 0x0012F3D0
			// (set) Token: 0x06005AED RID: 23277 RVA: 0x001311D8 File Offset: 0x0012F3D8
			public BlackBoxRule Area { get; private set; }

			// Token: 0x170010B7 RID: 4279
			// (get) Token: 0x06005AEE RID: 23278 RVA: 0x001311E1 File Offset: 0x0012F3E1
			// (set) Token: 0x06005AEF RID: 23279 RVA: 0x001311E9 File Offset: 0x0012F3E9
			public BlackBoxRule TrimHiddenWholeSheet { get; private set; }

			// Token: 0x170010B8 RID: 4280
			// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x001311F2 File Offset: 0x0012F3F2
			// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x001311FA File Offset: 0x0012F3FA
			public BlackBoxRule WholeSheet { get; private set; }

			// Token: 0x170010B9 RID: 4281
			// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x00131203 File Offset: 0x0012F403
			// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x0013120B File Offset: 0x0012F40B
			public BlackBoxRule WithFormatting { get; private set; }

			// Token: 0x170010BA RID: 4282
			// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x00131214 File Offset: 0x0012F414
			// (set) Token: 0x06005AF5 RID: 23285 RVA: 0x0013121C File Offset: 0x0012F41C
			public BlackBoxRule SplitOnEmptyRows { get; private set; }

			// Token: 0x170010BB RID: 4283
			// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x00131225 File Offset: 0x0012F425
			// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x0013122D File Offset: 0x0012F42D
			public BlackBoxRule SplitOnMatchingRows { get; private set; }

			// Token: 0x170010BC RID: 4284
			// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x00131236 File Offset: 0x0012F436
			// (set) Token: 0x06005AF9 RID: 23289 RVA: 0x0013123E File Offset: 0x0012F43E
			public BlackBoxRule SplitOnEmptyColumns { get; private set; }

			// Token: 0x170010BD RID: 4285
			// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00131247 File Offset: 0x0012F447
			// (set) Token: 0x06005AFB RID: 23291 RVA: 0x0013124F File Offset: 0x0012F44F
			public BlackBoxRule BorderedAreas { get; private set; }

			// Token: 0x170010BE RID: 4286
			// (get) Token: 0x06005AFC RID: 23292 RVA: 0x00131258 File Offset: 0x0012F458
			// (set) Token: 0x06005AFD RID: 23293 RVA: 0x00131260 File Offset: 0x0012F460
			public BlackBoxRule RemoveEmptyRows { get; private set; }

			// Token: 0x170010BF RID: 4287
			// (get) Token: 0x06005AFE RID: 23294 RVA: 0x00131269 File Offset: 0x0012F469
			// (set) Token: 0x06005AFF RID: 23295 RVA: 0x00131271 File Offset: 0x0012F471
			public BlackBoxRule RemoveEmptyColumns { get; private set; }

			// Token: 0x170010C0 RID: 4288
			// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0013127A File Offset: 0x0012F47A
			// (set) Token: 0x06005B01 RID: 23297 RVA: 0x00131282 File Offset: 0x0012F482
			public BlackBoxRule MWholeSheet { get; private set; }

			// Token: 0x170010C1 RID: 4289
			// (get) Token: 0x06005B02 RID: 23298 RVA: 0x0013128B File Offset: 0x0012F48B
			// (set) Token: 0x06005B03 RID: 23299 RVA: 0x00131293 File Offset: 0x0012F493
			public BlackBoxRule KthAndNextMSection { get; private set; }

			// Token: 0x170010C2 RID: 4290
			// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0013129C File Offset: 0x0012F49C
			// (set) Token: 0x06005B05 RID: 23301 RVA: 0x001312A4 File Offset: 0x0012F4A4
			public BlackBoxRule MTrimTopSingleCellRows { get; private set; }

			// Token: 0x170010C3 RID: 4291
			// (get) Token: 0x06005B06 RID: 23302 RVA: 0x001312AD File Offset: 0x0012F4AD
			// (set) Token: 0x06005B07 RID: 23303 RVA: 0x001312B5 File Offset: 0x0012F4B5
			public BlackBoxRule MTrimTopSingleLeftCellRows { get; private set; }

			// Token: 0x170010C4 RID: 4292
			// (get) Token: 0x06005B08 RID: 23304 RVA: 0x001312BE File Offset: 0x0012F4BE
			// (set) Token: 0x06005B09 RID: 23305 RVA: 0x001312C6 File Offset: 0x0012F4C6
			public BlackBoxRule MTrimBottomSingleCellRows { get; private set; }

			// Token: 0x170010C5 RID: 4293
			// (get) Token: 0x06005B0A RID: 23306 RVA: 0x001312CF File Offset: 0x0012F4CF
			// (set) Token: 0x06005B0B RID: 23307 RVA: 0x001312D7 File Offset: 0x0012F4D7
			public BlackBoxRule MTrimLeftSingleCellColumns { get; private set; }

			// Token: 0x170010C6 RID: 4294
			// (get) Token: 0x06005B0C RID: 23308 RVA: 0x001312E0 File Offset: 0x0012F4E0
			// (set) Token: 0x06005B0D RID: 23309 RVA: 0x001312E8 File Offset: 0x0012F4E8
			public BlackBoxRule MTrimRightSingleCellColumns { get; private set; }

			// Token: 0x170010C7 RID: 4295
			// (get) Token: 0x06005B0E RID: 23310 RVA: 0x001312F1 File Offset: 0x0012F4F1
			// (set) Token: 0x06005B0F RID: 23311 RVA: 0x001312F9 File Offset: 0x0012F4F9
			public BlackBoxRule MTrimTopDoubleCellRows { get; private set; }

			// Token: 0x170010C8 RID: 4296
			// (get) Token: 0x06005B10 RID: 23312 RVA: 0x00131302 File Offset: 0x0012F502
			// (set) Token: 0x06005B11 RID: 23313 RVA: 0x0013130A File Offset: 0x0012F50A
			public BlackBoxRule MTrimBottomDoubleCellRows { get; private set; }

			// Token: 0x170010C9 RID: 4297
			// (get) Token: 0x06005B12 RID: 23314 RVA: 0x00131313 File Offset: 0x0012F513
			// (set) Token: 0x06005B13 RID: 23315 RVA: 0x0013131B File Offset: 0x0012F51B
			public BlackBoxRule MSplitOnEmptyRows { get; private set; }

			// Token: 0x170010CA RID: 4298
			// (get) Token: 0x06005B14 RID: 23316 RVA: 0x00131324 File Offset: 0x0012F524
			// (set) Token: 0x06005B15 RID: 23317 RVA: 0x0013132C File Offset: 0x0012F52C
			public BlackBoxRule MSplitOnEmptyColumns { get; private set; }

			// Token: 0x170010CB RID: 4299
			// (get) Token: 0x06005B16 RID: 23318 RVA: 0x00131335 File Offset: 0x0012F535
			// (set) Token: 0x06005B17 RID: 23319 RVA: 0x0013133D File Offset: 0x0012F53D
			public BlackBoxRule WithoutFormatting { get; private set; }

			// Token: 0x170010CC RID: 4300
			// (get) Token: 0x06005B18 RID: 23320 RVA: 0x00131346 File Offset: 0x0012F546
			// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0013134E File Offset: 0x0012F54E
			public BlackBoxRule TopLeftCell { get; private set; }

			// Token: 0x170010CD RID: 4301
			// (get) Token: 0x06005B1A RID: 23322 RVA: 0x00131357 File Offset: 0x0012F557
			// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0013135F File Offset: 0x0012F55F
			public BlackBoxRule TopSameFontCells { get; private set; }

			// Token: 0x170010CE RID: 4302
			// (get) Token: 0x06005B1C RID: 23324 RVA: 0x00131368 File Offset: 0x0012F568
			// (set) Token: 0x06005B1D RID: 23325 RVA: 0x00131370 File Offset: 0x0012F570
			public BlackBoxRule BottomLeftSameFontCells { get; private set; }

			// Token: 0x170010CF RID: 4303
			// (get) Token: 0x06005B1E RID: 23326 RVA: 0x00131379 File Offset: 0x0012F579
			// (set) Token: 0x06005B1F RID: 23327 RVA: 0x00131381 File Offset: 0x0012F581
			public BlackBoxRule LeftmostColumn { get; private set; }

			// Token: 0x170010D0 RID: 4304
			// (get) Token: 0x06005B20 RID: 23328 RVA: 0x0013138A File Offset: 0x0012F58A
			// (set) Token: 0x06005B21 RID: 23329 RVA: 0x00131392 File Offset: 0x0012F592
			public BlackBoxRule LeftOf { get; private set; }

			// Token: 0x170010D1 RID: 4305
			// (get) Token: 0x06005B22 RID: 23330 RVA: 0x0013139B File Offset: 0x0012F59B
			// (set) Token: 0x06005B23 RID: 23331 RVA: 0x001313A3 File Offset: 0x0012F5A3
			public BlackBoxRule FirstSplit { get; private set; }

			// Token: 0x170010D2 RID: 4306
			// (get) Token: 0x06005B24 RID: 23332 RVA: 0x001313AC File Offset: 0x0012F5AC
			// (set) Token: 0x06005B25 RID: 23333 RVA: 0x001313B4 File Offset: 0x0012F5B4
			public BlackBoxRule TitleSplitOnEmptyRows { get; private set; }

			// Token: 0x170010D3 RID: 4307
			// (get) Token: 0x06005B26 RID: 23334 RVA: 0x001313BD File Offset: 0x0012F5BD
			// (set) Token: 0x06005B27 RID: 23335 RVA: 0x001313C5 File Offset: 0x0012F5C5
			public BlackBoxRule TitleSplitOnMatchingRows { get; private set; }

			// Token: 0x170010D4 RID: 4308
			// (get) Token: 0x06005B28 RID: 23336 RVA: 0x001313CE File Offset: 0x0012F5CE
			// (set) Token: 0x06005B29 RID: 23337 RVA: 0x001313D6 File Offset: 0x0012F5D6
			public BlackBoxRule TitleCellsAbove { get; private set; }

			// Token: 0x170010D5 RID: 4309
			// (get) Token: 0x06005B2A RID: 23338 RVA: 0x001313DF File Offset: 0x0012F5DF
			// (set) Token: 0x06005B2B RID: 23339 RVA: 0x001313E7 File Offset: 0x0012F5E7
			public BlackBoxRule TitleCellsAboveMatching { get; private set; }

			// Token: 0x170010D6 RID: 4310
			// (get) Token: 0x06005B2C RID: 23340 RVA: 0x001313F0 File Offset: 0x0012F5F0
			// (set) Token: 0x06005B2D RID: 23341 RVA: 0x001313F8 File Offset: 0x0012F5F8
			public BlackBoxRule IncludeEmptyToLeft { get; private set; }

			// Token: 0x170010D7 RID: 4311
			// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00131401 File Offset: 0x0012F601
			// (set) Token: 0x06005B2F RID: 23343 RVA: 0x00131409 File Offset: 0x0012F609
			public ConceptRule KthHorizontal { get; private set; }

			// Token: 0x170010D8 RID: 4312
			// (get) Token: 0x06005B30 RID: 23344 RVA: 0x00131412 File Offset: 0x0012F612
			// (set) Token: 0x06005B31 RID: 23345 RVA: 0x0013141A File Offset: 0x0012F61A
			public ConceptRule KthVertical { get; private set; }

			// Token: 0x170010D9 RID: 4313
			// (get) Token: 0x06005B32 RID: 23346 RVA: 0x00131423 File Offset: 0x0012F623
			// (set) Token: 0x06005B33 RID: 23347 RVA: 0x0013142B File Offset: 0x0012F62B
			public ConceptRule KthSplit { get; private set; }

			// Token: 0x170010DA RID: 4314
			// (get) Token: 0x06005B34 RID: 23348 RVA: 0x00131434 File Offset: 0x0012F634
			// (set) Token: 0x06005B35 RID: 23349 RVA: 0x0013143C File Offset: 0x0012F63C
			public ConceptRule KthMSection { get; private set; }

			// Token: 0x170010DB RID: 4315
			// (get) Token: 0x06005B36 RID: 23350 RVA: 0x00131445 File Offset: 0x0012F645
			// (set) Token: 0x06005B37 RID: 23351 RVA: 0x0013144D File Offset: 0x0012F64D
			public ConversionRule Output { get; private set; }

			// Token: 0x170010DC RID: 4316
			// (get) Token: 0x06005B38 RID: 23352 RVA: 0x00131456 File Offset: 0x0012F656
			// (set) Token: 0x06005B39 RID: 23353 RVA: 0x0013145E File Offset: 0x0012F65E
			public ConversionRule StartTitle { get; private set; }

			// Token: 0x170010DD RID: 4317
			// (get) Token: 0x06005B3A RID: 23354 RVA: 0x00131467 File Offset: 0x0012F667
			// (set) Token: 0x06005B3B RID: 23355 RVA: 0x0013146F File Offset: 0x0012F66F
			public ConversionRule WrapOutputForTitle { get; private set; }

			// Token: 0x06005B3C RID: 23356 RVA: 0x00131478 File Offset: 0x0012F678
			public GrammarRules(Grammar grammar)
			{
				this.Trim = (BlackBoxRule)grammar.Rule("Trim");
				this.TrimHidden = (BlackBoxRule)grammar.Rule("TrimHidden");
				this.DefinedRange = (BlackBoxRule)grammar.Rule("DefinedRange");
				this.TrimLeftSingleCellColumns = (BlackBoxRule)grammar.Rule("TrimLeftSingleCellColumns");
				this.TrimBottomSingleCellRows = (BlackBoxRule)grammar.Rule("TrimBottomSingleCellRows");
				this.TakeUntilEmptyRow = (BlackBoxRule)grammar.Rule("TakeUntilEmptyRow");
				this.TrimAboveBottomBorder = (BlackBoxRule)grammar.Rule("TrimAboveBottomBorder");
				this.FreezePaneTight = (BlackBoxRule)grammar.Rule("FreezePaneTight");
				this.FreezePaneToBlanks = (BlackBoxRule)grammar.Rule("FreezePaneToBlanks");
				this.FreezePaneToMultipleBlanks = (BlackBoxRule)grammar.Rule("FreezePaneToMultipleBlanks");
				this.TrimTopMergedCellRows = (BlackBoxRule)grammar.Rule("TrimTopMergedCellRows");
				this.TrimTopFullWidthMergedCellRows = (BlackBoxRule)grammar.Rule("TrimTopFullWidthMergedCellRows");
				this.TrimTopSingleCellRows = (BlackBoxRule)grammar.Rule("TrimTopSingleCellRows");
				this.TrimBelowTopBorder = (BlackBoxRule)grammar.Rule("TrimBelowTopBorder");
				this.TakeAfterEmptyRow = (BlackBoxRule)grammar.Rule("TakeAfterEmptyRow");
				this.TakeUntilEmptyColumn = (BlackBoxRule)grammar.Rule("TakeUntilEmptyColumn");
				this.TrimRightSingleCellColumns = (BlackBoxRule)grammar.Rule("TrimRightSingleCellColumns");
				this.Area = (BlackBoxRule)grammar.Rule("Area");
				this.TrimHiddenWholeSheet = (BlackBoxRule)grammar.Rule("TrimHiddenWholeSheet");
				this.WholeSheet = (BlackBoxRule)grammar.Rule("WholeSheet");
				this.WithFormatting = (BlackBoxRule)grammar.Rule("WithFormatting");
				this.SplitOnEmptyRows = (BlackBoxRule)grammar.Rule("SplitOnEmptyRows");
				this.SplitOnMatchingRows = (BlackBoxRule)grammar.Rule("SplitOnMatchingRows");
				this.SplitOnEmptyColumns = (BlackBoxRule)grammar.Rule("SplitOnEmptyColumns");
				this.BorderedAreas = (BlackBoxRule)grammar.Rule("BorderedAreas");
				this.RemoveEmptyRows = (BlackBoxRule)grammar.Rule("RemoveEmptyRows");
				this.RemoveEmptyColumns = (BlackBoxRule)grammar.Rule("RemoveEmptyColumns");
				this.MWholeSheet = (BlackBoxRule)grammar.Rule("MWholeSheet");
				this.KthAndNextMSection = (BlackBoxRule)grammar.Rule("KthAndNextMSection");
				this.MTrimTopSingleCellRows = (BlackBoxRule)grammar.Rule("MTrimTopSingleCellRows");
				this.MTrimTopSingleLeftCellRows = (BlackBoxRule)grammar.Rule("MTrimTopSingleLeftCellRows");
				this.MTrimBottomSingleCellRows = (BlackBoxRule)grammar.Rule("MTrimBottomSingleCellRows");
				this.MTrimLeftSingleCellColumns = (BlackBoxRule)grammar.Rule("MTrimLeftSingleCellColumns");
				this.MTrimRightSingleCellColumns = (BlackBoxRule)grammar.Rule("MTrimRightSingleCellColumns");
				this.MTrimTopDoubleCellRows = (BlackBoxRule)grammar.Rule("MTrimTopDoubleCellRows");
				this.MTrimBottomDoubleCellRows = (BlackBoxRule)grammar.Rule("MTrimBottomDoubleCellRows");
				this.MSplitOnEmptyRows = (BlackBoxRule)grammar.Rule("MSplitOnEmptyRows");
				this.MSplitOnEmptyColumns = (BlackBoxRule)grammar.Rule("MSplitOnEmptyColumns");
				this.WithoutFormatting = (BlackBoxRule)grammar.Rule("WithoutFormatting");
				this.TopLeftCell = (BlackBoxRule)grammar.Rule("TopLeftCell");
				this.TopSameFontCells = (BlackBoxRule)grammar.Rule("TopSameFontCells");
				this.BottomLeftSameFontCells = (BlackBoxRule)grammar.Rule("BottomLeftSameFontCells");
				this.LeftmostColumn = (BlackBoxRule)grammar.Rule("LeftmostColumn");
				this.LeftOf = (BlackBoxRule)grammar.Rule("LeftOf");
				this.FirstSplit = (BlackBoxRule)grammar.Rule("FirstSplit");
				this.TitleSplitOnEmptyRows = (BlackBoxRule)grammar.Rule("TitleSplitOnEmptyRows");
				this.TitleSplitOnMatchingRows = (BlackBoxRule)grammar.Rule("TitleSplitOnMatchingRows");
				this.TitleCellsAbove = (BlackBoxRule)grammar.Rule("TitleCellsAbove");
				this.TitleCellsAboveMatching = (BlackBoxRule)grammar.Rule("TitleCellsAboveMatching");
				this.IncludeEmptyToLeft = (BlackBoxRule)grammar.Rule("IncludeEmptyToLeft");
				this.KthHorizontal = (ConceptRule)grammar.Rule("KthHorizontal");
				this.KthVertical = (ConceptRule)grammar.Rule("KthVertical");
				this.KthSplit = (ConceptRule)grammar.Rule("KthSplit");
				this.KthMSection = (ConceptRule)grammar.Rule("KthMSection");
				this.Output = (ConversionRule)grammar.Rule("Output");
				this.StartTitle = (ConversionRule)grammar.Rule("StartTitle");
				this.WrapOutputForTitle = (ConversionRule)grammar.Rule("WrapOutputForTitle");
			}
		}

		// Token: 0x02000DEE RID: 3566
		public class GrammarUnnamedConversions
		{
			// Token: 0x170010DE RID: 4318
			// (get) Token: 0x06005B3D RID: 23357 RVA: 0x00131971 File Offset: 0x0012FB71
			// (set) Token: 0x06005B3E RID: 23358 RVA: 0x00131979 File Offset: 0x0012FB79
			public ConversionRule area_trimLeft { get; private set; }

			// Token: 0x170010DF RID: 4319
			// (get) Token: 0x06005B3F RID: 23359 RVA: 0x00131982 File Offset: 0x0012FB82
			// (set) Token: 0x06005B40 RID: 23360 RVA: 0x0013198A File Offset: 0x0012FB8A
			public ConversionRule trimLeft_trimBottom { get; private set; }

			// Token: 0x170010E0 RID: 4320
			// (get) Token: 0x06005B41 RID: 23361 RVA: 0x00131993 File Offset: 0x0012FB93
			// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0013199B File Offset: 0x0012FB9B
			public ConversionRule trimBottom_trimTop { get; private set; }

			// Token: 0x170010E1 RID: 4321
			// (get) Token: 0x06005B43 RID: 23363 RVA: 0x001319A4 File Offset: 0x0012FBA4
			// (set) Token: 0x06005B44 RID: 23364 RVA: 0x001319AC File Offset: 0x0012FBAC
			public ConversionRule trimTop_sheetSection { get; private set; }

			// Token: 0x170010E2 RID: 4322
			// (get) Token: 0x06005B45 RID: 23365 RVA: 0x001319B5 File Offset: 0x0012FBB5
			// (set) Token: 0x06005B46 RID: 23366 RVA: 0x001319BD File Offset: 0x0012FBBD
			public ConversionRule sheetSection_horizontalSheetSection { get; private set; }

			// Token: 0x170010E3 RID: 4323
			// (get) Token: 0x06005B47 RID: 23367 RVA: 0x001319C6 File Offset: 0x0012FBC6
			// (set) Token: 0x06005B48 RID: 23368 RVA: 0x001319CE File Offset: 0x0012FBCE
			public ConversionRule horizontalSheetSection_verticalSheetSection { get; private set; }

			// Token: 0x170010E4 RID: 4324
			// (get) Token: 0x06005B49 RID: 23369 RVA: 0x001319D7 File Offset: 0x0012FBD7
			// (set) Token: 0x06005B4A RID: 23370 RVA: 0x001319DF File Offset: 0x0012FBDF
			public ConversionRule verticalSheetSection_uncleanedSheetSection { get; private set; }

			// Token: 0x170010E5 RID: 4325
			// (get) Token: 0x06005B4B RID: 23371 RVA: 0x001319E8 File Offset: 0x0012FBE8
			// (set) Token: 0x06005B4C RID: 23372 RVA: 0x001319F0 File Offset: 0x0012FBF0
			public ConversionRule uncleanedSheetSection_wholeSheet { get; private set; }

			// Token: 0x170010E6 RID: 4326
			// (get) Token: 0x06005B4D RID: 23373 RVA: 0x001319F9 File Offset: 0x0012FBF9
			// (set) Token: 0x06005B4E RID: 23374 RVA: 0x00131A01 File Offset: 0x0012FC01
			public ConversionRule wholeSheet_wholeSheetFull { get; private set; }

			// Token: 0x170010E7 RID: 4327
			// (get) Token: 0x06005B4F RID: 23375 RVA: 0x00131A0A File Offset: 0x0012FC0A
			// (set) Token: 0x06005B50 RID: 23376 RVA: 0x00131A12 File Offset: 0x0012FC12
			public ConversionRule mProgram_mTable { get; private set; }

			// Token: 0x170010E8 RID: 4328
			// (get) Token: 0x06005B51 RID: 23377 RVA: 0x00131A1B File Offset: 0x0012FC1B
			// (set) Token: 0x06005B52 RID: 23378 RVA: 0x00131A23 File Offset: 0x0012FC23
			public ConversionRule title_above { get; private set; }

			// Token: 0x170010E9 RID: 4329
			// (get) Token: 0x06005B53 RID: 23379 RVA: 0x00131A2C File Offset: 0x0012FC2C
			// (set) Token: 0x06005B54 RID: 23380 RVA: 0x00131A34 File Offset: 0x0012FC34
			public ConversionRule aboveOrLeftmost_above { get; private set; }

			// Token: 0x170010EA RID: 4330
			// (get) Token: 0x06005B55 RID: 23381 RVA: 0x00131A3D File Offset: 0x0012FC3D
			// (set) Token: 0x06005B56 RID: 23382 RVA: 0x00131A45 File Offset: 0x0012FC45
			public ConversionRule aboveOrOutput_aboveOrHeader { get; private set; }

			// Token: 0x170010EB RID: 4331
			// (get) Token: 0x06005B57 RID: 23383 RVA: 0x00131A4E File Offset: 0x0012FC4E
			// (set) Token: 0x06005B58 RID: 23384 RVA: 0x00131A56 File Offset: 0x0012FC56
			public ConversionRule aboveOrOutput_titleOf { get; private set; }

			// Token: 0x170010EC RID: 4332
			// (get) Token: 0x06005B59 RID: 23385 RVA: 0x00131A5F File Offset: 0x0012FC5F
			// (set) Token: 0x06005B5A RID: 23386 RVA: 0x00131A67 File Offset: 0x0012FC67
			public ConversionRule aboveOrHeader_above { get; private set; }

			// Token: 0x170010ED RID: 4333
			// (get) Token: 0x06005B5B RID: 23387 RVA: 0x00131A70 File Offset: 0x0012FC70
			// (set) Token: 0x06005B5C RID: 23388 RVA: 0x00131A78 File Offset: 0x0012FC78
			public ConversionRule aboveOrHeader_headerSection { get; private set; }

			// Token: 0x06005B5D RID: 23389 RVA: 0x00131A84 File Offset: 0x0012FC84
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.area_trimLeft = (ConversionRule)grammar.Rule("~convert_area_trimLeft");
				this.trimLeft_trimBottom = (ConversionRule)grammar.Rule("~convert_trimLeft_trimBottom");
				this.trimBottom_trimTop = (ConversionRule)grammar.Rule("~convert_trimBottom_trimTop");
				this.trimTop_sheetSection = (ConversionRule)grammar.Rule("~convert_trimTop_sheetSection");
				this.sheetSection_horizontalSheetSection = (ConversionRule)grammar.Rule("~convert_sheetSection_horizontalSheetSection");
				this.horizontalSheetSection_verticalSheetSection = (ConversionRule)grammar.Rule("~convert_horizontalSheetSection_verticalSheetSection");
				this.verticalSheetSection_uncleanedSheetSection = (ConversionRule)grammar.Rule("~convert_verticalSheetSection_uncleanedSheetSection");
				this.uncleanedSheetSection_wholeSheet = (ConversionRule)grammar.Rule("~convert_uncleanedSheetSection_wholeSheet");
				this.wholeSheet_wholeSheetFull = (ConversionRule)grammar.Rule("~convert_wholeSheet_wholeSheetFull");
				this.mProgram_mTable = (ConversionRule)grammar.Rule("~convert_mProgram_mTable");
				this.title_above = (ConversionRule)grammar.Rule("~convert_title_above");
				this.aboveOrLeftmost_above = (ConversionRule)grammar.Rule("~convert_aboveOrLeftmost_above");
				this.aboveOrOutput_aboveOrHeader = (ConversionRule)grammar.Rule("~convert_aboveOrOutput_aboveOrHeader");
				this.aboveOrOutput_titleOf = (ConversionRule)grammar.Rule("~convert_aboveOrOutput_titleOf");
				this.aboveOrHeader_above = (ConversionRule)grammar.Rule("~convert_aboveOrHeader_above");
				this.aboveOrHeader_headerSection = (ConversionRule)grammar.Rule("~convert_aboveOrHeader_headerSection");
			}
		}

		// Token: 0x02000DEF RID: 3567
		public class GrammarHoles
		{
			// Token: 0x170010EE RID: 4334
			// (get) Token: 0x06005B5E RID: 23390 RVA: 0x00131BF7 File Offset: 0x0012FDF7
			// (set) Token: 0x06005B5F RID: 23391 RVA: 0x00131BFF File Offset: 0x0012FDFF
			public Hole sheetPair { get; private set; }

			// Token: 0x170010EF RID: 4335
			// (get) Token: 0x06005B60 RID: 23392 RVA: 0x00131C08 File Offset: 0x0012FE08
			// (set) Token: 0x06005B61 RID: 23393 RVA: 0x00131C10 File Offset: 0x0012FE10
			public Hole output { get; private set; }

			// Token: 0x170010F0 RID: 4336
			// (get) Token: 0x06005B62 RID: 23394 RVA: 0x00131C19 File Offset: 0x0012FE19
			// (set) Token: 0x06005B63 RID: 23395 RVA: 0x00131C21 File Offset: 0x0012FE21
			public Hole trim { get; private set; }

			// Token: 0x170010F1 RID: 4337
			// (get) Token: 0x06005B64 RID: 23396 RVA: 0x00131C2A File Offset: 0x0012FE2A
			// (set) Token: 0x06005B65 RID: 23397 RVA: 0x00131C32 File Offset: 0x0012FE32
			public Hole area { get; private set; }

			// Token: 0x170010F2 RID: 4338
			// (get) Token: 0x06005B66 RID: 23398 RVA: 0x00131C3B File Offset: 0x0012FE3B
			// (set) Token: 0x06005B67 RID: 23399 RVA: 0x00131C43 File Offset: 0x0012FE43
			public Hole trimLeft { get; private set; }

			// Token: 0x170010F3 RID: 4339
			// (get) Token: 0x06005B68 RID: 23400 RVA: 0x00131C4C File Offset: 0x0012FE4C
			// (set) Token: 0x06005B69 RID: 23401 RVA: 0x00131C54 File Offset: 0x0012FE54
			public Hole trimBottom { get; private set; }

			// Token: 0x170010F4 RID: 4340
			// (get) Token: 0x06005B6A RID: 23402 RVA: 0x00131C5D File Offset: 0x0012FE5D
			// (set) Token: 0x06005B6B RID: 23403 RVA: 0x00131C65 File Offset: 0x0012FE65
			public Hole trimTop { get; private set; }

			// Token: 0x170010F5 RID: 4341
			// (get) Token: 0x06005B6C RID: 23404 RVA: 0x00131C6E File Offset: 0x0012FE6E
			// (set) Token: 0x06005B6D RID: 23405 RVA: 0x00131C76 File Offset: 0x0012FE76
			public Hole sheetSection { get; private set; }

			// Token: 0x170010F6 RID: 4342
			// (get) Token: 0x06005B6E RID: 23406 RVA: 0x00131C7F File Offset: 0x0012FE7F
			// (set) Token: 0x06005B6F RID: 23407 RVA: 0x00131C87 File Offset: 0x0012FE87
			public Hole horizontalSheetSection { get; private set; }

			// Token: 0x170010F7 RID: 4343
			// (get) Token: 0x06005B70 RID: 23408 RVA: 0x00131C90 File Offset: 0x0012FE90
			// (set) Token: 0x06005B71 RID: 23409 RVA: 0x00131C98 File Offset: 0x0012FE98
			public Hole verticalSheetSection { get; private set; }

			// Token: 0x170010F8 RID: 4344
			// (get) Token: 0x06005B72 RID: 23410 RVA: 0x00131CA1 File Offset: 0x0012FEA1
			// (set) Token: 0x06005B73 RID: 23411 RVA: 0x00131CA9 File Offset: 0x0012FEA9
			public Hole uncleanedSheetSection { get; private set; }

			// Token: 0x170010F9 RID: 4345
			// (get) Token: 0x06005B74 RID: 23412 RVA: 0x00131CB2 File Offset: 0x0012FEB2
			// (set) Token: 0x06005B75 RID: 23413 RVA: 0x00131CBA File Offset: 0x0012FEBA
			public Hole wholeSheet { get; private set; }

			// Token: 0x170010FA RID: 4346
			// (get) Token: 0x06005B76 RID: 23414 RVA: 0x00131CC3 File Offset: 0x0012FEC3
			// (set) Token: 0x06005B77 RID: 23415 RVA: 0x00131CCB File Offset: 0x0012FECB
			public Hole wholeSheetFull { get; private set; }

			// Token: 0x170010FB RID: 4347
			// (get) Token: 0x06005B78 RID: 23416 RVA: 0x00131CD4 File Offset: 0x0012FED4
			// (set) Token: 0x06005B79 RID: 23417 RVA: 0x00131CDC File Offset: 0x0012FEDC
			public Hole sheet { get; private set; }

			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x06005B7A RID: 23418 RVA: 0x00131CE5 File Offset: 0x0012FEE5
			// (set) Token: 0x06005B7B RID: 23419 RVA: 0x00131CED File Offset: 0x0012FEED
			public Hole horizontalSheetSplits { get; private set; }

			// Token: 0x170010FD RID: 4349
			// (get) Token: 0x06005B7C RID: 23420 RVA: 0x00131CF6 File Offset: 0x0012FEF6
			// (set) Token: 0x06005B7D RID: 23421 RVA: 0x00131CFE File Offset: 0x0012FEFE
			public Hole verticalSheetSplits { get; private set; }

			// Token: 0x170010FE RID: 4350
			// (get) Token: 0x06005B7E RID: 23422 RVA: 0x00131D07 File Offset: 0x0012FF07
			// (set) Token: 0x06005B7F RID: 23423 RVA: 0x00131D0F File Offset: 0x0012FF0F
			public Hole sheetSplits { get; private set; }

			// Token: 0x170010FF RID: 4351
			// (get) Token: 0x06005B80 RID: 23424 RVA: 0x00131D18 File Offset: 0x0012FF18
			// (set) Token: 0x06005B81 RID: 23425 RVA: 0x00131D20 File Offset: 0x0012FF20
			public Hole index { get; private set; }

			// Token: 0x17001100 RID: 4352
			// (get) Token: 0x06005B82 RID: 23426 RVA: 0x00131D29 File Offset: 0x0012FF29
			// (set) Token: 0x06005B83 RID: 23427 RVA: 0x00131D31 File Offset: 0x0012FF31
			public Hole rangeName { get; private set; }

			// Token: 0x17001101 RID: 4353
			// (get) Token: 0x06005B84 RID: 23428 RVA: 0x00131D3A File Offset: 0x0012FF3A
			// (set) Token: 0x06005B85 RID: 23429 RVA: 0x00131D42 File Offset: 0x0012FF42
			public Hole k { get; private set; }

			// Token: 0x17001102 RID: 4354
			// (get) Token: 0x06005B86 RID: 23430 RVA: 0x00131D4B File Offset: 0x0012FF4B
			// (set) Token: 0x06005B87 RID: 23431 RVA: 0x00131D53 File Offset: 0x0012FF53
			public Hole splitMode { get; private set; }

			// Token: 0x17001103 RID: 4355
			// (get) Token: 0x06005B88 RID: 23432 RVA: 0x00131D5C File Offset: 0x0012FF5C
			// (set) Token: 0x06005B89 RID: 23433 RVA: 0x00131D64 File Offset: 0x0012FF64
			public Hole styleFilter { get; private set; }

			// Token: 0x17001104 RID: 4356
			// (get) Token: 0x06005B8A RID: 23434 RVA: 0x00131D6D File Offset: 0x0012FF6D
			// (set) Token: 0x06005B8B RID: 23435 RVA: 0x00131D75 File Offset: 0x0012FF75
			public Hole mProgram { get; private set; }

			// Token: 0x17001105 RID: 4357
			// (get) Token: 0x06005B8C RID: 23436 RVA: 0x00131D7E File Offset: 0x0012FF7E
			// (set) Token: 0x06005B8D RID: 23437 RVA: 0x00131D86 File Offset: 0x0012FF86
			public Hole mTable { get; private set; }

			// Token: 0x17001106 RID: 4358
			// (get) Token: 0x06005B8E RID: 23438 RVA: 0x00131D8F File Offset: 0x0012FF8F
			// (set) Token: 0x06005B8F RID: 23439 RVA: 0x00131D97 File Offset: 0x0012FF97
			public Hole mSection { get; private set; }

			// Token: 0x17001107 RID: 4359
			// (get) Token: 0x06005B90 RID: 23440 RVA: 0x00131DA0 File Offset: 0x0012FFA0
			// (set) Token: 0x06005B91 RID: 23441 RVA: 0x00131DA8 File Offset: 0x0012FFA8
			public Hole withoutFormatting { get; private set; }

			// Token: 0x17001108 RID: 4360
			// (get) Token: 0x06005B92 RID: 23442 RVA: 0x00131DB1 File Offset: 0x0012FFB1
			// (set) Token: 0x06005B93 RID: 23443 RVA: 0x00131DB9 File Offset: 0x0012FFB9
			public Hole startTitle { get; private set; }

			// Token: 0x17001109 RID: 4361
			// (get) Token: 0x06005B94 RID: 23444 RVA: 0x00131DC2 File Offset: 0x0012FFC2
			// (set) Token: 0x06005B95 RID: 23445 RVA: 0x00131DCA File Offset: 0x0012FFCA
			public Hole title { get; private set; }

			// Token: 0x1700110A RID: 4362
			// (get) Token: 0x06005B96 RID: 23446 RVA: 0x00131DD3 File Offset: 0x0012FFD3
			// (set) Token: 0x06005B97 RID: 23447 RVA: 0x00131DDB File Offset: 0x0012FFDB
			public Hole aboveOrLeftmost { get; private set; }

			// Token: 0x1700110B RID: 4363
			// (get) Token: 0x06005B98 RID: 23448 RVA: 0x00131DE4 File Offset: 0x0012FFE4
			// (set) Token: 0x06005B99 RID: 23449 RVA: 0x00131DEC File Offset: 0x0012FFEC
			public Hole aboveOrOutput { get; private set; }

			// Token: 0x1700110C RID: 4364
			// (get) Token: 0x06005B9A RID: 23450 RVA: 0x00131DF5 File Offset: 0x0012FFF5
			// (set) Token: 0x06005B9B RID: 23451 RVA: 0x00131DFD File Offset: 0x0012FFFD
			public Hole aboveOrHeader { get; private set; }

			// Token: 0x1700110D RID: 4365
			// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00131E06 File Offset: 0x00130006
			// (set) Token: 0x06005B9D RID: 23453 RVA: 0x00131E0E File Offset: 0x0013000E
			public Hole headerSection { get; private set; }

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x06005B9E RID: 23454 RVA: 0x00131E17 File Offset: 0x00130017
			// (set) Token: 0x06005B9F RID: 23455 RVA: 0x00131E1F File Offset: 0x0013001F
			public Hole splitForTitle { get; private set; }

			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x06005BA0 RID: 23456 RVA: 0x00131E28 File Offset: 0x00130028
			// (set) Token: 0x06005BA1 RID: 23457 RVA: 0x00131E30 File Offset: 0x00130030
			public Hole above { get; private set; }

			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x06005BA2 RID: 23458 RVA: 0x00131E39 File Offset: 0x00130039
			// (set) Token: 0x06005BA3 RID: 23459 RVA: 0x00131E41 File Offset: 0x00130041
			public Hole titleOf { get; private set; }

			// Token: 0x17001111 RID: 4369
			// (get) Token: 0x06005BA4 RID: 23460 RVA: 0x00131E4A File Offset: 0x0013004A
			// (set) Token: 0x06005BA5 RID: 23461 RVA: 0x00131E52 File Offset: 0x00130052
			public Hole titleAboveMode { get; private set; }

			// Token: 0x06005BA6 RID: 23462 RVA: 0x00131E5C File Offset: 0x0013005C
			public GrammarHoles(GrammarBuilders builders)
			{
				this.sheetPair = new Hole(builders.Symbol.sheetPair, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.trim = new Hole(builders.Symbol.trim, null);
				this.area = new Hole(builders.Symbol.area, null);
				this.trimLeft = new Hole(builders.Symbol.trimLeft, null);
				this.trimBottom = new Hole(builders.Symbol.trimBottom, null);
				this.trimTop = new Hole(builders.Symbol.trimTop, null);
				this.sheetSection = new Hole(builders.Symbol.sheetSection, null);
				this.horizontalSheetSection = new Hole(builders.Symbol.horizontalSheetSection, null);
				this.verticalSheetSection = new Hole(builders.Symbol.verticalSheetSection, null);
				this.uncleanedSheetSection = new Hole(builders.Symbol.uncleanedSheetSection, null);
				this.wholeSheet = new Hole(builders.Symbol.wholeSheet, null);
				this.wholeSheetFull = new Hole(builders.Symbol.wholeSheetFull, null);
				this.sheet = new Hole(builders.Symbol.sheet, null);
				this.horizontalSheetSplits = new Hole(builders.Symbol.horizontalSheetSplits, null);
				this.verticalSheetSplits = new Hole(builders.Symbol.verticalSheetSplits, null);
				this.sheetSplits = new Hole(builders.Symbol.sheetSplits, null);
				this.index = new Hole(builders.Symbol.index, null);
				this.rangeName = new Hole(builders.Symbol.rangeName, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.splitMode = new Hole(builders.Symbol.splitMode, null);
				this.styleFilter = new Hole(builders.Symbol.styleFilter, null);
				this.mProgram = new Hole(builders.Symbol.mProgram, null);
				this.mTable = new Hole(builders.Symbol.mTable, null);
				this.mSection = new Hole(builders.Symbol.mSection, null);
				this.withoutFormatting = new Hole(builders.Symbol.withoutFormatting, null);
				this.startTitle = new Hole(builders.Symbol.startTitle, null);
				this.title = new Hole(builders.Symbol.title, null);
				this.aboveOrLeftmost = new Hole(builders.Symbol.aboveOrLeftmost, null);
				this.aboveOrOutput = new Hole(builders.Symbol.aboveOrOutput, null);
				this.aboveOrHeader = new Hole(builders.Symbol.aboveOrHeader, null);
				this.headerSection = new Hole(builders.Symbol.headerSection, null);
				this.splitForTitle = new Hole(builders.Symbol.splitForTitle, null);
				this.above = new Hole(builders.Symbol.above, null);
				this.titleOf = new Hole(builders.Symbol.titleOf, null);
				this.titleAboveMode = new Hole(builders.Symbol.titleAboveMode, null);
			}
		}

		// Token: 0x02000DF0 RID: 3568
		public class Nodes
		{
			// Token: 0x06005BA7 RID: 23463 RVA: 0x001321AC File Offset: 0x001303AC
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

			// Token: 0x17001112 RID: 4370
			// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x0013228F File Offset: 0x0013048F
			// (set) Token: 0x06005BA9 RID: 23465 RVA: 0x00132297 File Offset: 0x00130497
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x06005BAA RID: 23466 RVA: 0x001322A0 File Offset: 0x001304A0
			// (set) Token: 0x06005BAB RID: 23467 RVA: 0x001322A8 File Offset: 0x001304A8
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x06005BAC RID: 23468 RVA: 0x001322B1 File Offset: 0x001304B1
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x06005BAD RID: 23469 RVA: 0x001322BE File Offset: 0x001304BE
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x06005BAE RID: 23470 RVA: 0x001322CB File Offset: 0x001304CB
			// (set) Token: 0x06005BAF RID: 23471 RVA: 0x001322D3 File Offset: 0x001304D3
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x001322DC File Offset: 0x001304DC
			// (set) Token: 0x06005BB1 RID: 23473 RVA: 0x001322E4 File Offset: 0x001304E4
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x001322ED File Offset: 0x001304ED
			// (set) Token: 0x06005BB3 RID: 23475 RVA: 0x001322F5 File Offset: 0x001304F5
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17001119 RID: 4377
			// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x001322FE File Offset: 0x001304FE
			// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x00132306 File Offset: 0x00130506
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x1700111A RID: 4378
			// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x0013230F File Offset: 0x0013050F
			// (set) Token: 0x06005BB7 RID: 23479 RVA: 0x00132317 File Offset: 0x00130517
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x06005BB8 RID: 23480 RVA: 0x00132320 File Offset: 0x00130520
			// (set) Token: 0x06005BB9 RID: 23481 RVA: 0x00132328 File Offset: 0x00130528
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x1700111C RID: 4380
			// (get) Token: 0x06005BBA RID: 23482 RVA: 0x00132331 File Offset: 0x00130531
			// (set) Token: 0x06005BBB RID: 23483 RVA: 0x00132339 File Offset: 0x00130539
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04002B1F RID: 11039
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04002B20 RID: 11040
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000DF1 RID: 3569
			public class NodeRules
			{
				// Token: 0x06005BBC RID: 23484 RVA: 0x00132342 File Offset: 0x00130542
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005BBD RID: 23485 RVA: 0x00132351 File Offset: 0x00130551
				public index index(int value)
				{
					return new index(this._builders, value);
				}

				// Token: 0x06005BBE RID: 23486 RVA: 0x0013235F File Offset: 0x0013055F
				public rangeName rangeName(string value)
				{
					return new rangeName(this._builders, value);
				}

				// Token: 0x06005BBF RID: 23487 RVA: 0x0013236D File Offset: 0x0013056D
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x06005BC0 RID: 23488 RVA: 0x0013237B File Offset: 0x0013057B
				public splitMode splitMode(SplitMode value)
				{
					return new splitMode(this._builders, value);
				}

				// Token: 0x06005BC1 RID: 23489 RVA: 0x00132389 File Offset: 0x00130589
				public styleFilter styleFilter(StyleFilter value)
				{
					return new styleFilter(this._builders, value);
				}

				// Token: 0x06005BC2 RID: 23490 RVA: 0x00132397 File Offset: 0x00130597
				public titleAboveMode titleAboveMode(TitleAboveMode value)
				{
					return new titleAboveMode(this._builders, value);
				}

				// Token: 0x06005BC3 RID: 23491 RVA: 0x001323A5 File Offset: 0x001305A5
				public trim Trim(area value0)
				{
					return new Trim(this._builders, value0);
				}

				// Token: 0x06005BC4 RID: 23492 RVA: 0x001323B8 File Offset: 0x001305B8
				public trim TrimHidden(area value0)
				{
					return new TrimHidden(this._builders, value0);
				}

				// Token: 0x06005BC5 RID: 23493 RVA: 0x001323CB File Offset: 0x001305CB
				public area DefinedRange(sheet value0, rangeName value1)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange(this._builders, value0, value1);
				}

				// Token: 0x06005BC6 RID: 23494 RVA: 0x001323DF File Offset: 0x001305DF
				public trimLeft TrimLeftSingleCellColumns(trimBottom value0)
				{
					return new TrimLeftSingleCellColumns(this._builders, value0);
				}

				// Token: 0x06005BC7 RID: 23495 RVA: 0x001323F2 File Offset: 0x001305F2
				public trimBottom TrimBottomSingleCellRows(trimTop value0)
				{
					return new TrimBottomSingleCellRows(this._builders, value0);
				}

				// Token: 0x06005BC8 RID: 23496 RVA: 0x00132405 File Offset: 0x00130605
				public trimBottom TakeUntilEmptyRow(trimTop value0)
				{
					return new TakeUntilEmptyRow(this._builders, value0);
				}

				// Token: 0x06005BC9 RID: 23497 RVA: 0x00132418 File Offset: 0x00130618
				public trimBottom TrimAboveBottomBorder(trimTop value0)
				{
					return new TrimAboveBottomBorder(this._builders, value0);
				}

				// Token: 0x06005BCA RID: 23498 RVA: 0x0013242B File Offset: 0x0013062B
				public trimTop FreezePaneTight(sheet value0)
				{
					return new FreezePaneTight(this._builders, value0);
				}

				// Token: 0x06005BCB RID: 23499 RVA: 0x0013243E File Offset: 0x0013063E
				public trimTop FreezePaneToBlanks(sheet value0)
				{
					return new FreezePaneToBlanks(this._builders, value0);
				}

				// Token: 0x06005BCC RID: 23500 RVA: 0x00132451 File Offset: 0x00130651
				public trimTop FreezePaneToMultipleBlanks(sheet value0)
				{
					return new FreezePaneToMultipleBlanks(this._builders, value0);
				}

				// Token: 0x06005BCD RID: 23501 RVA: 0x00132464 File Offset: 0x00130664
				public trimTop TrimTopMergedCellRows(sheetSection value0)
				{
					return new TrimTopMergedCellRows(this._builders, value0);
				}

				// Token: 0x06005BCE RID: 23502 RVA: 0x00132477 File Offset: 0x00130677
				public trimTop TrimTopFullWidthMergedCellRows(sheetSection value0)
				{
					return new TrimTopFullWidthMergedCellRows(this._builders, value0);
				}

				// Token: 0x06005BCF RID: 23503 RVA: 0x0013248A File Offset: 0x0013068A
				public trimTop TrimTopSingleCellRows(sheetSection value0)
				{
					return new TrimTopSingleCellRows(this._builders, value0);
				}

				// Token: 0x06005BD0 RID: 23504 RVA: 0x0013249D File Offset: 0x0013069D
				public trimTop TrimBelowTopBorder(sheetSection value0)
				{
					return new TrimBelowTopBorder(this._builders, value0);
				}

				// Token: 0x06005BD1 RID: 23505 RVA: 0x001324B0 File Offset: 0x001306B0
				public trimTop TakeAfterEmptyRow(sheetSection value0)
				{
					return new TakeAfterEmptyRow(this._builders, value0);
				}

				// Token: 0x06005BD2 RID: 23506 RVA: 0x001324C3 File Offset: 0x001306C3
				public sheetSection TakeUntilEmptyColumn(horizontalSheetSection value0)
				{
					return new TakeUntilEmptyColumn(this._builders, value0);
				}

				// Token: 0x06005BD3 RID: 23507 RVA: 0x001324D6 File Offset: 0x001306D6
				public sheetSection TrimRightSingleCellColumns(horizontalSheetSection value0)
				{
					return new TrimRightSingleCellColumns(this._builders, value0);
				}

				// Token: 0x06005BD4 RID: 23508 RVA: 0x001324E9 File Offset: 0x001306E9
				public uncleanedSheetSection Area(sheet value0, index value1, index value2, index value3, index value4)
				{
					return new Area(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x06005BD5 RID: 23509 RVA: 0x00132502 File Offset: 0x00130702
				public wholeSheet TrimHiddenWholeSheet(wholeSheetFull value0)
				{
					return new TrimHiddenWholeSheet(this._builders, value0);
				}

				// Token: 0x06005BD6 RID: 23510 RVA: 0x00132515 File Offset: 0x00130715
				public wholeSheetFull WholeSheet(sheet value0)
				{
					return new WholeSheet(this._builders, value0);
				}

				// Token: 0x06005BD7 RID: 23511 RVA: 0x00132528 File Offset: 0x00130728
				public sheet WithFormatting(sheetPair value0)
				{
					return new WithFormatting(this._builders, value0);
				}

				// Token: 0x06005BD8 RID: 23512 RVA: 0x0013253B File Offset: 0x0013073B
				public horizontalSheetSplits SplitOnEmptyRows(verticalSheetSection value0)
				{
					return new SplitOnEmptyRows(this._builders, value0);
				}

				// Token: 0x06005BD9 RID: 23513 RVA: 0x0013254E File Offset: 0x0013074E
				public horizontalSheetSplits SplitOnMatchingRows(verticalSheetSection value0, styleFilter value1, splitMode value2)
				{
					return new SplitOnMatchingRows(this._builders, value0, value1, value2);
				}

				// Token: 0x06005BDA RID: 23514 RVA: 0x00132563 File Offset: 0x00130763
				public verticalSheetSplits SplitOnEmptyColumns(uncleanedSheetSection value0)
				{
					return new SplitOnEmptyColumns(this._builders, value0);
				}

				// Token: 0x06005BDB RID: 23515 RVA: 0x00132576 File Offset: 0x00130776
				public sheetSplits BorderedAreas(wholeSheet value0)
				{
					return new BorderedAreas(this._builders, value0);
				}

				// Token: 0x06005BDC RID: 23516 RVA: 0x00132589 File Offset: 0x00130789
				public mProgram RemoveEmptyRows(mProgram value0)
				{
					return new RemoveEmptyRows(this._builders, value0);
				}

				// Token: 0x06005BDD RID: 23517 RVA: 0x0013259C File Offset: 0x0013079C
				public mProgram RemoveEmptyColumns(mProgram value0)
				{
					return new RemoveEmptyColumns(this._builders, value0);
				}

				// Token: 0x06005BDE RID: 23518 RVA: 0x001325AF File Offset: 0x001307AF
				public mTable MWholeSheet(withoutFormatting value0)
				{
					return new MWholeSheet(this._builders, value0);
				}

				// Token: 0x06005BDF RID: 23519 RVA: 0x001325C2 File Offset: 0x001307C2
				public mTable KthAndNextMSection(mSection value0, k value1)
				{
					return new KthAndNextMSection(this._builders, value0, value1);
				}

				// Token: 0x06005BE0 RID: 23520 RVA: 0x001325D6 File Offset: 0x001307D6
				public mTable MTrimTopSingleCellRows(mTable value0)
				{
					return new MTrimTopSingleCellRows(this._builders, value0);
				}

				// Token: 0x06005BE1 RID: 23521 RVA: 0x001325E9 File Offset: 0x001307E9
				public mTable MTrimTopSingleLeftCellRows(mTable value0)
				{
					return new MTrimTopSingleLeftCellRows(this._builders, value0);
				}

				// Token: 0x06005BE2 RID: 23522 RVA: 0x001325FC File Offset: 0x001307FC
				public mTable MTrimBottomSingleCellRows(mTable value0)
				{
					return new MTrimBottomSingleCellRows(this._builders, value0);
				}

				// Token: 0x06005BE3 RID: 23523 RVA: 0x0013260F File Offset: 0x0013080F
				public mTable MTrimLeftSingleCellColumns(mTable value0)
				{
					return new MTrimLeftSingleCellColumns(this._builders, value0);
				}

				// Token: 0x06005BE4 RID: 23524 RVA: 0x00132622 File Offset: 0x00130822
				public mTable MTrimRightSingleCellColumns(mTable value0)
				{
					return new MTrimRightSingleCellColumns(this._builders, value0);
				}

				// Token: 0x06005BE5 RID: 23525 RVA: 0x00132635 File Offset: 0x00130835
				public mTable MTrimTopDoubleCellRows(mTable value0)
				{
					return new MTrimTopDoubleCellRows(this._builders, value0);
				}

				// Token: 0x06005BE6 RID: 23526 RVA: 0x00132648 File Offset: 0x00130848
				public mTable MTrimBottomDoubleCellRows(mTable value0)
				{
					return new MTrimBottomDoubleCellRows(this._builders, value0);
				}

				// Token: 0x06005BE7 RID: 23527 RVA: 0x0013265B File Offset: 0x0013085B
				public mSection MSplitOnEmptyRows(mTable value0)
				{
					return new MSplitOnEmptyRows(this._builders, value0);
				}

				// Token: 0x06005BE8 RID: 23528 RVA: 0x0013266E File Offset: 0x0013086E
				public mSection MSplitOnEmptyColumns(mTable value0)
				{
					return new MSplitOnEmptyColumns(this._builders, value0);
				}

				// Token: 0x06005BE9 RID: 23529 RVA: 0x00132681 File Offset: 0x00130881
				public withoutFormatting WithoutFormatting(sheetPair value0)
				{
					return new WithoutFormatting(this._builders, value0);
				}

				// Token: 0x06005BEA RID: 23530 RVA: 0x00132694 File Offset: 0x00130894
				public title TopLeftCell(aboveOrLeftmost value0)
				{
					return new TopLeftCell(this._builders, value0);
				}

				// Token: 0x06005BEB RID: 23531 RVA: 0x001326A7 File Offset: 0x001308A7
				public title TopSameFontCells(aboveOrLeftmost value0)
				{
					return new TopSameFontCells(this._builders, value0);
				}

				// Token: 0x06005BEC RID: 23532 RVA: 0x001326BA File Offset: 0x001308BA
				public title BottomLeftSameFontCells(aboveOrHeader value0)
				{
					return new BottomLeftSameFontCells(this._builders, value0);
				}

				// Token: 0x06005BED RID: 23533 RVA: 0x001326CD File Offset: 0x001308CD
				public aboveOrLeftmost LeftmostColumn(aboveOrOutput value0)
				{
					return new LeftmostColumn(this._builders, value0);
				}

				// Token: 0x06005BEE RID: 23534 RVA: 0x001326E0 File Offset: 0x001308E0
				public aboveOrLeftmost LeftOf(titleOf value0)
				{
					return new LeftOf(this._builders, value0);
				}

				// Token: 0x06005BEF RID: 23535 RVA: 0x001326F3 File Offset: 0x001308F3
				public headerSection FirstSplit(splitForTitle value0)
				{
					return new FirstSplit(this._builders, value0);
				}

				// Token: 0x06005BF0 RID: 23536 RVA: 0x00132706 File Offset: 0x00130906
				public splitForTitle TitleSplitOnEmptyRows(titleOf value0)
				{
					return new TitleSplitOnEmptyRows(this._builders, value0);
				}

				// Token: 0x06005BF1 RID: 23537 RVA: 0x00132719 File Offset: 0x00130919
				public splitForTitle TitleSplitOnMatchingRows(titleOf value0, styleFilter value1, splitMode value2)
				{
					return new TitleSplitOnMatchingRows(this._builders, value0, value1, value2);
				}

				// Token: 0x06005BF2 RID: 23538 RVA: 0x0013272E File Offset: 0x0013092E
				public above TitleCellsAbove(titleOf value0, titleAboveMode value1)
				{
					return new TitleCellsAbove(this._builders, value0, value1);
				}

				// Token: 0x06005BF3 RID: 23539 RVA: 0x00132742 File Offset: 0x00130942
				public above TitleCellsAboveMatching(titleOf value0, titleAboveMode value1, styleFilter value2)
				{
					return new TitleCellsAboveMatching(this._builders, value0, value1, value2);
				}

				// Token: 0x06005BF4 RID: 23540 RVA: 0x00132757 File Offset: 0x00130957
				public titleOf IncludeEmptyToLeft(output value0)
				{
					return new IncludeEmptyToLeft(this._builders, value0);
				}

				// Token: 0x06005BF5 RID: 23541 RVA: 0x0013276A File Offset: 0x0013096A
				public horizontalSheetSection KthHorizontal(horizontalSheetSplits value0, k value1)
				{
					return new KthHorizontal(this._builders, value0, value1);
				}

				// Token: 0x06005BF6 RID: 23542 RVA: 0x0013277E File Offset: 0x0013097E
				public verticalSheetSection KthVertical(verticalSheetSplits value0, k value1)
				{
					return new KthVertical(this._builders, value0, value1);
				}

				// Token: 0x06005BF7 RID: 23543 RVA: 0x00132792 File Offset: 0x00130992
				public uncleanedSheetSection KthSplit(sheetSplits value0, k value1)
				{
					return new KthSplit(this._builders, value0, value1);
				}

				// Token: 0x06005BF8 RID: 23544 RVA: 0x001327A6 File Offset: 0x001309A6
				public mTable KthMSection(mSection value0, k value1)
				{
					return new KthMSection(this._builders, value0, value1);
				}

				// Token: 0x06005BF9 RID: 23545 RVA: 0x001327BA File Offset: 0x001309BA
				public output Output(trim value0)
				{
					return new Output(this._builders, value0);
				}

				// Token: 0x06005BFA RID: 23546 RVA: 0x001327CD File Offset: 0x001309CD
				public startTitle StartTitle(title value0)
				{
					return new StartTitle(this._builders, value0);
				}

				// Token: 0x06005BFB RID: 23547 RVA: 0x001327E0 File Offset: 0x001309E0
				public titleOf WrapOutputForTitle(output value0)
				{
					return new WrapOutputForTitle(this._builders, value0);
				}

				// Token: 0x04002B28 RID: 11048
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DF2 RID: 3570
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06005BFC RID: 23548 RVA: 0x001327F3 File Offset: 0x001309F3
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005BFD RID: 23549 RVA: 0x00132802 File Offset: 0x00130A02
				public area area_trimLeft(trimLeft value0)
				{
					return new area_trimLeft(this._builders, value0);
				}

				// Token: 0x06005BFE RID: 23550 RVA: 0x00132815 File Offset: 0x00130A15
				public trimLeft trimLeft_trimBottom(trimBottom value0)
				{
					return new trimLeft_trimBottom(this._builders, value0);
				}

				// Token: 0x06005BFF RID: 23551 RVA: 0x00132828 File Offset: 0x00130A28
				public trimBottom trimBottom_trimTop(trimTop value0)
				{
					return new trimBottom_trimTop(this._builders, value0);
				}

				// Token: 0x06005C00 RID: 23552 RVA: 0x0013283B File Offset: 0x00130A3B
				public trimTop trimTop_sheetSection(sheetSection value0)
				{
					return new trimTop_sheetSection(this._builders, value0);
				}

				// Token: 0x06005C01 RID: 23553 RVA: 0x0013284E File Offset: 0x00130A4E
				public sheetSection sheetSection_horizontalSheetSection(horizontalSheetSection value0)
				{
					return new sheetSection_horizontalSheetSection(this._builders, value0);
				}

				// Token: 0x06005C02 RID: 23554 RVA: 0x00132861 File Offset: 0x00130A61
				public horizontalSheetSection horizontalSheetSection_verticalSheetSection(verticalSheetSection value0)
				{
					return new horizontalSheetSection_verticalSheetSection(this._builders, value0);
				}

				// Token: 0x06005C03 RID: 23555 RVA: 0x00132874 File Offset: 0x00130A74
				public verticalSheetSection verticalSheetSection_uncleanedSheetSection(uncleanedSheetSection value0)
				{
					return new verticalSheetSection_uncleanedSheetSection(this._builders, value0);
				}

				// Token: 0x06005C04 RID: 23556 RVA: 0x00132887 File Offset: 0x00130A87
				public uncleanedSheetSection uncleanedSheetSection_wholeSheet(wholeSheet value0)
				{
					return new uncleanedSheetSection_wholeSheet(this._builders, value0);
				}

				// Token: 0x06005C05 RID: 23557 RVA: 0x0013289A File Offset: 0x00130A9A
				public wholeSheet wholeSheet_wholeSheetFull(wholeSheetFull value0)
				{
					return new wholeSheet_wholeSheetFull(this._builders, value0);
				}

				// Token: 0x06005C06 RID: 23558 RVA: 0x001328AD File Offset: 0x00130AAD
				public mProgram mProgram_mTable(mTable value0)
				{
					return new mProgram_mTable(this._builders, value0);
				}

				// Token: 0x06005C07 RID: 23559 RVA: 0x001328C0 File Offset: 0x00130AC0
				public title title_above(above value0)
				{
					return new title_above(this._builders, value0);
				}

				// Token: 0x06005C08 RID: 23560 RVA: 0x001328D3 File Offset: 0x00130AD3
				public aboveOrLeftmost aboveOrLeftmost_above(above value0)
				{
					return new aboveOrLeftmost_above(this._builders, value0);
				}

				// Token: 0x06005C09 RID: 23561 RVA: 0x001328E6 File Offset: 0x00130AE6
				public aboveOrOutput aboveOrOutput_aboveOrHeader(aboveOrHeader value0)
				{
					return new aboveOrOutput_aboveOrHeader(this._builders, value0);
				}

				// Token: 0x06005C0A RID: 23562 RVA: 0x001328F9 File Offset: 0x00130AF9
				public aboveOrOutput aboveOrOutput_titleOf(titleOf value0)
				{
					return new aboveOrOutput_titleOf(this._builders, value0);
				}

				// Token: 0x06005C0B RID: 23563 RVA: 0x0013290C File Offset: 0x00130B0C
				public aboveOrHeader aboveOrHeader_above(above value0)
				{
					return new aboveOrHeader_above(this._builders, value0);
				}

				// Token: 0x06005C0C RID: 23564 RVA: 0x0013291F File Offset: 0x00130B1F
				public aboveOrHeader aboveOrHeader_headerSection(headerSection value0)
				{
					return new aboveOrHeader_headerSection(this._builders, value0);
				}

				// Token: 0x04002B29 RID: 11049
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DF3 RID: 3571
			public class NodeVariables
			{
				// Token: 0x1700111D RID: 4381
				// (get) Token: 0x06005C0D RID: 23565 RVA: 0x00132932 File Offset: 0x00130B32
				// (set) Token: 0x06005C0E RID: 23566 RVA: 0x0013293A File Offset: 0x00130B3A
				public sheetPair sheetPair { get; private set; }

				// Token: 0x06005C0F RID: 23567 RVA: 0x00132943 File Offset: 0x00130B43
				public NodeVariables(GrammarBuilders builders)
				{
					this.sheetPair = new sheetPair(builders);
				}
			}

			// Token: 0x02000DF4 RID: 3572
			public class NodeHoles
			{
				// Token: 0x1700111E RID: 4382
				// (get) Token: 0x06005C10 RID: 23568 RVA: 0x00132957 File Offset: 0x00130B57
				// (set) Token: 0x06005C11 RID: 23569 RVA: 0x0013295F File Offset: 0x00130B5F
				public output output { get; private set; }

				// Token: 0x1700111F RID: 4383
				// (get) Token: 0x06005C12 RID: 23570 RVA: 0x00132968 File Offset: 0x00130B68
				// (set) Token: 0x06005C13 RID: 23571 RVA: 0x00132970 File Offset: 0x00130B70
				public trim trim { get; private set; }

				// Token: 0x17001120 RID: 4384
				// (get) Token: 0x06005C14 RID: 23572 RVA: 0x00132979 File Offset: 0x00130B79
				// (set) Token: 0x06005C15 RID: 23573 RVA: 0x00132981 File Offset: 0x00130B81
				public area area { get; private set; }

				// Token: 0x17001121 RID: 4385
				// (get) Token: 0x06005C16 RID: 23574 RVA: 0x0013298A File Offset: 0x00130B8A
				// (set) Token: 0x06005C17 RID: 23575 RVA: 0x00132992 File Offset: 0x00130B92
				public trimLeft trimLeft { get; private set; }

				// Token: 0x17001122 RID: 4386
				// (get) Token: 0x06005C18 RID: 23576 RVA: 0x0013299B File Offset: 0x00130B9B
				// (set) Token: 0x06005C19 RID: 23577 RVA: 0x001329A3 File Offset: 0x00130BA3
				public trimBottom trimBottom { get; private set; }

				// Token: 0x17001123 RID: 4387
				// (get) Token: 0x06005C1A RID: 23578 RVA: 0x001329AC File Offset: 0x00130BAC
				// (set) Token: 0x06005C1B RID: 23579 RVA: 0x001329B4 File Offset: 0x00130BB4
				public trimTop trimTop { get; private set; }

				// Token: 0x17001124 RID: 4388
				// (get) Token: 0x06005C1C RID: 23580 RVA: 0x001329BD File Offset: 0x00130BBD
				// (set) Token: 0x06005C1D RID: 23581 RVA: 0x001329C5 File Offset: 0x00130BC5
				public sheetSection sheetSection { get; private set; }

				// Token: 0x17001125 RID: 4389
				// (get) Token: 0x06005C1E RID: 23582 RVA: 0x001329CE File Offset: 0x00130BCE
				// (set) Token: 0x06005C1F RID: 23583 RVA: 0x001329D6 File Offset: 0x00130BD6
				public horizontalSheetSection horizontalSheetSection { get; private set; }

				// Token: 0x17001126 RID: 4390
				// (get) Token: 0x06005C20 RID: 23584 RVA: 0x001329DF File Offset: 0x00130BDF
				// (set) Token: 0x06005C21 RID: 23585 RVA: 0x001329E7 File Offset: 0x00130BE7
				public verticalSheetSection verticalSheetSection { get; private set; }

				// Token: 0x17001127 RID: 4391
				// (get) Token: 0x06005C22 RID: 23586 RVA: 0x001329F0 File Offset: 0x00130BF0
				// (set) Token: 0x06005C23 RID: 23587 RVA: 0x001329F8 File Offset: 0x00130BF8
				public uncleanedSheetSection uncleanedSheetSection { get; private set; }

				// Token: 0x17001128 RID: 4392
				// (get) Token: 0x06005C24 RID: 23588 RVA: 0x00132A01 File Offset: 0x00130C01
				// (set) Token: 0x06005C25 RID: 23589 RVA: 0x00132A09 File Offset: 0x00130C09
				public wholeSheet wholeSheet { get; private set; }

				// Token: 0x17001129 RID: 4393
				// (get) Token: 0x06005C26 RID: 23590 RVA: 0x00132A12 File Offset: 0x00130C12
				// (set) Token: 0x06005C27 RID: 23591 RVA: 0x00132A1A File Offset: 0x00130C1A
				public wholeSheetFull wholeSheetFull { get; private set; }

				// Token: 0x1700112A RID: 4394
				// (get) Token: 0x06005C28 RID: 23592 RVA: 0x00132A23 File Offset: 0x00130C23
				// (set) Token: 0x06005C29 RID: 23593 RVA: 0x00132A2B File Offset: 0x00130C2B
				public sheet sheet { get; private set; }

				// Token: 0x1700112B RID: 4395
				// (get) Token: 0x06005C2A RID: 23594 RVA: 0x00132A34 File Offset: 0x00130C34
				// (set) Token: 0x06005C2B RID: 23595 RVA: 0x00132A3C File Offset: 0x00130C3C
				public horizontalSheetSplits horizontalSheetSplits { get; private set; }

				// Token: 0x1700112C RID: 4396
				// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00132A45 File Offset: 0x00130C45
				// (set) Token: 0x06005C2D RID: 23597 RVA: 0x00132A4D File Offset: 0x00130C4D
				public verticalSheetSplits verticalSheetSplits { get; private set; }

				// Token: 0x1700112D RID: 4397
				// (get) Token: 0x06005C2E RID: 23598 RVA: 0x00132A56 File Offset: 0x00130C56
				// (set) Token: 0x06005C2F RID: 23599 RVA: 0x00132A5E File Offset: 0x00130C5E
				public sheetSplits sheetSplits { get; private set; }

				// Token: 0x1700112E RID: 4398
				// (get) Token: 0x06005C30 RID: 23600 RVA: 0x00132A67 File Offset: 0x00130C67
				// (set) Token: 0x06005C31 RID: 23601 RVA: 0x00132A6F File Offset: 0x00130C6F
				public index index { get; private set; }

				// Token: 0x1700112F RID: 4399
				// (get) Token: 0x06005C32 RID: 23602 RVA: 0x00132A78 File Offset: 0x00130C78
				// (set) Token: 0x06005C33 RID: 23603 RVA: 0x00132A80 File Offset: 0x00130C80
				public rangeName rangeName { get; private set; }

				// Token: 0x17001130 RID: 4400
				// (get) Token: 0x06005C34 RID: 23604 RVA: 0x00132A89 File Offset: 0x00130C89
				// (set) Token: 0x06005C35 RID: 23605 RVA: 0x00132A91 File Offset: 0x00130C91
				public k k { get; private set; }

				// Token: 0x17001131 RID: 4401
				// (get) Token: 0x06005C36 RID: 23606 RVA: 0x00132A9A File Offset: 0x00130C9A
				// (set) Token: 0x06005C37 RID: 23607 RVA: 0x00132AA2 File Offset: 0x00130CA2
				public splitMode splitMode { get; private set; }

				// Token: 0x17001132 RID: 4402
				// (get) Token: 0x06005C38 RID: 23608 RVA: 0x00132AAB File Offset: 0x00130CAB
				// (set) Token: 0x06005C39 RID: 23609 RVA: 0x00132AB3 File Offset: 0x00130CB3
				public styleFilter styleFilter { get; private set; }

				// Token: 0x17001133 RID: 4403
				// (get) Token: 0x06005C3A RID: 23610 RVA: 0x00132ABC File Offset: 0x00130CBC
				// (set) Token: 0x06005C3B RID: 23611 RVA: 0x00132AC4 File Offset: 0x00130CC4
				public mProgram mProgram { get; private set; }

				// Token: 0x17001134 RID: 4404
				// (get) Token: 0x06005C3C RID: 23612 RVA: 0x00132ACD File Offset: 0x00130CCD
				// (set) Token: 0x06005C3D RID: 23613 RVA: 0x00132AD5 File Offset: 0x00130CD5
				public mTable mTable { get; private set; }

				// Token: 0x17001135 RID: 4405
				// (get) Token: 0x06005C3E RID: 23614 RVA: 0x00132ADE File Offset: 0x00130CDE
				// (set) Token: 0x06005C3F RID: 23615 RVA: 0x00132AE6 File Offset: 0x00130CE6
				public mSection mSection { get; private set; }

				// Token: 0x17001136 RID: 4406
				// (get) Token: 0x06005C40 RID: 23616 RVA: 0x00132AEF File Offset: 0x00130CEF
				// (set) Token: 0x06005C41 RID: 23617 RVA: 0x00132AF7 File Offset: 0x00130CF7
				public withoutFormatting withoutFormatting { get; private set; }

				// Token: 0x17001137 RID: 4407
				// (get) Token: 0x06005C42 RID: 23618 RVA: 0x00132B00 File Offset: 0x00130D00
				// (set) Token: 0x06005C43 RID: 23619 RVA: 0x00132B08 File Offset: 0x00130D08
				public startTitle startTitle { get; private set; }

				// Token: 0x17001138 RID: 4408
				// (get) Token: 0x06005C44 RID: 23620 RVA: 0x00132B11 File Offset: 0x00130D11
				// (set) Token: 0x06005C45 RID: 23621 RVA: 0x00132B19 File Offset: 0x00130D19
				public title title { get; private set; }

				// Token: 0x17001139 RID: 4409
				// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00132B22 File Offset: 0x00130D22
				// (set) Token: 0x06005C47 RID: 23623 RVA: 0x00132B2A File Offset: 0x00130D2A
				public aboveOrLeftmost aboveOrLeftmost { get; private set; }

				// Token: 0x1700113A RID: 4410
				// (get) Token: 0x06005C48 RID: 23624 RVA: 0x00132B33 File Offset: 0x00130D33
				// (set) Token: 0x06005C49 RID: 23625 RVA: 0x00132B3B File Offset: 0x00130D3B
				public aboveOrOutput aboveOrOutput { get; private set; }

				// Token: 0x1700113B RID: 4411
				// (get) Token: 0x06005C4A RID: 23626 RVA: 0x00132B44 File Offset: 0x00130D44
				// (set) Token: 0x06005C4B RID: 23627 RVA: 0x00132B4C File Offset: 0x00130D4C
				public aboveOrHeader aboveOrHeader { get; private set; }

				// Token: 0x1700113C RID: 4412
				// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00132B55 File Offset: 0x00130D55
				// (set) Token: 0x06005C4D RID: 23629 RVA: 0x00132B5D File Offset: 0x00130D5D
				public headerSection headerSection { get; private set; }

				// Token: 0x1700113D RID: 4413
				// (get) Token: 0x06005C4E RID: 23630 RVA: 0x00132B66 File Offset: 0x00130D66
				// (set) Token: 0x06005C4F RID: 23631 RVA: 0x00132B6E File Offset: 0x00130D6E
				public splitForTitle splitForTitle { get; private set; }

				// Token: 0x1700113E RID: 4414
				// (get) Token: 0x06005C50 RID: 23632 RVA: 0x00132B77 File Offset: 0x00130D77
				// (set) Token: 0x06005C51 RID: 23633 RVA: 0x00132B7F File Offset: 0x00130D7F
				public above above { get; private set; }

				// Token: 0x1700113F RID: 4415
				// (get) Token: 0x06005C52 RID: 23634 RVA: 0x00132B88 File Offset: 0x00130D88
				// (set) Token: 0x06005C53 RID: 23635 RVA: 0x00132B90 File Offset: 0x00130D90
				public titleOf titleOf { get; private set; }

				// Token: 0x17001140 RID: 4416
				// (get) Token: 0x06005C54 RID: 23636 RVA: 0x00132B99 File Offset: 0x00130D99
				// (set) Token: 0x06005C55 RID: 23637 RVA: 0x00132BA1 File Offset: 0x00130DA1
				public titleAboveMode titleAboveMode { get; private set; }

				// Token: 0x06005C56 RID: 23638 RVA: 0x00132BAC File Offset: 0x00130DAC
				public NodeHoles(GrammarBuilders builders)
				{
					this.output = output.CreateHole(builders, null);
					this.trim = trim.CreateHole(builders, null);
					this.area = area.CreateHole(builders, null);
					this.trimLeft = trimLeft.CreateHole(builders, null);
					this.trimBottom = trimBottom.CreateHole(builders, null);
					this.trimTop = trimTop.CreateHole(builders, null);
					this.sheetSection = sheetSection.CreateHole(builders, null);
					this.horizontalSheetSection = horizontalSheetSection.CreateHole(builders, null);
					this.verticalSheetSection = verticalSheetSection.CreateHole(builders, null);
					this.uncleanedSheetSection = uncleanedSheetSection.CreateHole(builders, null);
					this.wholeSheet = wholeSheet.CreateHole(builders, null);
					this.wholeSheetFull = wholeSheetFull.CreateHole(builders, null);
					this.sheet = sheet.CreateHole(builders, null);
					this.horizontalSheetSplits = horizontalSheetSplits.CreateHole(builders, null);
					this.verticalSheetSplits = verticalSheetSplits.CreateHole(builders, null);
					this.sheetSplits = sheetSplits.CreateHole(builders, null);
					this.index = index.CreateHole(builders, null);
					this.rangeName = rangeName.CreateHole(builders, null);
					this.k = k.CreateHole(builders, null);
					this.splitMode = splitMode.CreateHole(builders, null);
					this.styleFilter = styleFilter.CreateHole(builders, null);
					this.mProgram = mProgram.CreateHole(builders, null);
					this.mTable = mTable.CreateHole(builders, null);
					this.mSection = mSection.CreateHole(builders, null);
					this.withoutFormatting = withoutFormatting.CreateHole(builders, null);
					this.startTitle = startTitle.CreateHole(builders, null);
					this.title = title.CreateHole(builders, null);
					this.aboveOrLeftmost = aboveOrLeftmost.CreateHole(builders, null);
					this.aboveOrOutput = aboveOrOutput.CreateHole(builders, null);
					this.aboveOrHeader = aboveOrHeader.CreateHole(builders, null);
					this.headerSection = headerSection.CreateHole(builders, null);
					this.splitForTitle = splitForTitle.CreateHole(builders, null);
					this.above = above.CreateHole(builders, null);
					this.titleOf = titleOf.CreateHole(builders, null);
					this.titleAboveMode = titleAboveMode.CreateHole(builders, null);
				}
			}

			// Token: 0x02000DF5 RID: 3573
			public class NodeUnsafe
			{
				// Token: 0x06005C57 RID: 23639 RVA: 0x00132D86 File Offset: 0x00130F86
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x06005C58 RID: 23640 RVA: 0x00132D8E File Offset: 0x00130F8E
				public trim trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim.CreateUnsafe(node);
				}

				// Token: 0x06005C59 RID: 23641 RVA: 0x00132D96 File Offset: 0x00130F96
				public area area(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area.CreateUnsafe(node);
				}

				// Token: 0x06005C5A RID: 23642 RVA: 0x00132D9E File Offset: 0x00130F9E
				public trimLeft trimLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft.CreateUnsafe(node);
				}

				// Token: 0x06005C5B RID: 23643 RVA: 0x00132DA6 File Offset: 0x00130FA6
				public trimBottom trimBottom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom.CreateUnsafe(node);
				}

				// Token: 0x06005C5C RID: 23644 RVA: 0x00132DAE File Offset: 0x00130FAE
				public trimTop trimTop(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop.CreateUnsafe(node);
				}

				// Token: 0x06005C5D RID: 23645 RVA: 0x00132DB6 File Offset: 0x00130FB6
				public sheetSection sheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection.CreateUnsafe(node);
				}

				// Token: 0x06005C5E RID: 23646 RVA: 0x00132DBE File Offset: 0x00130FBE
				public horizontalSheetSection horizontalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection.CreateUnsafe(node);
				}

				// Token: 0x06005C5F RID: 23647 RVA: 0x00132DC6 File Offset: 0x00130FC6
				public verticalSheetSection verticalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection.CreateUnsafe(node);
				}

				// Token: 0x06005C60 RID: 23648 RVA: 0x00132DCE File Offset: 0x00130FCE
				public uncleanedSheetSection uncleanedSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection.CreateUnsafe(node);
				}

				// Token: 0x06005C61 RID: 23649 RVA: 0x00132DD6 File Offset: 0x00130FD6
				public wholeSheet wholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet.CreateUnsafe(node);
				}

				// Token: 0x06005C62 RID: 23650 RVA: 0x00132DDE File Offset: 0x00130FDE
				public wholeSheetFull wholeSheetFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull.CreateUnsafe(node);
				}

				// Token: 0x06005C63 RID: 23651 RVA: 0x00132DE6 File Offset: 0x00130FE6
				public sheet sheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet.CreateUnsafe(node);
				}

				// Token: 0x06005C64 RID: 23652 RVA: 0x00132DEE File Offset: 0x00130FEE
				public horizontalSheetSplits horizontalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits.CreateUnsafe(node);
				}

				// Token: 0x06005C65 RID: 23653 RVA: 0x00132DF6 File Offset: 0x00130FF6
				public verticalSheetSplits verticalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits.CreateUnsafe(node);
				}

				// Token: 0x06005C66 RID: 23654 RVA: 0x00132DFE File Offset: 0x00130FFE
				public sheetSplits sheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits.CreateUnsafe(node);
				}

				// Token: 0x06005C67 RID: 23655 RVA: 0x00132E06 File Offset: 0x00131006
				public index index(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index.CreateUnsafe(node);
				}

				// Token: 0x06005C68 RID: 23656 RVA: 0x00132E0E File Offset: 0x0013100E
				public rangeName rangeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName.CreateUnsafe(node);
				}

				// Token: 0x06005C69 RID: 23657 RVA: 0x00132E16 File Offset: 0x00131016
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x06005C6A RID: 23658 RVA: 0x00132E1E File Offset: 0x0013101E
				public splitMode splitMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode.CreateUnsafe(node);
				}

				// Token: 0x06005C6B RID: 23659 RVA: 0x00132E26 File Offset: 0x00131026
				public styleFilter styleFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter.CreateUnsafe(node);
				}

				// Token: 0x06005C6C RID: 23660 RVA: 0x00132E2E File Offset: 0x0013102E
				public mProgram mProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram.CreateUnsafe(node);
				}

				// Token: 0x06005C6D RID: 23661 RVA: 0x00132E36 File Offset: 0x00131036
				public mTable mTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable.CreateUnsafe(node);
				}

				// Token: 0x06005C6E RID: 23662 RVA: 0x00132E3E File Offset: 0x0013103E
				public mSection mSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection.CreateUnsafe(node);
				}

				// Token: 0x06005C6F RID: 23663 RVA: 0x00132E46 File Offset: 0x00131046
				public withoutFormatting withoutFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting.CreateUnsafe(node);
				}

				// Token: 0x06005C70 RID: 23664 RVA: 0x00132E4E File Offset: 0x0013104E
				public startTitle startTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle.CreateUnsafe(node);
				}

				// Token: 0x06005C71 RID: 23665 RVA: 0x00132E56 File Offset: 0x00131056
				public title title(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title.CreateUnsafe(node);
				}

				// Token: 0x06005C72 RID: 23666 RVA: 0x00132E5E File Offset: 0x0013105E
				public aboveOrLeftmost aboveOrLeftmost(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost.CreateUnsafe(node);
				}

				// Token: 0x06005C73 RID: 23667 RVA: 0x00132E66 File Offset: 0x00131066
				public aboveOrOutput aboveOrOutput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput.CreateUnsafe(node);
				}

				// Token: 0x06005C74 RID: 23668 RVA: 0x00132E6E File Offset: 0x0013106E
				public aboveOrHeader aboveOrHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader.CreateUnsafe(node);
				}

				// Token: 0x06005C75 RID: 23669 RVA: 0x00132E76 File Offset: 0x00131076
				public headerSection headerSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection.CreateUnsafe(node);
				}

				// Token: 0x06005C76 RID: 23670 RVA: 0x00132E7E File Offset: 0x0013107E
				public splitForTitle splitForTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle.CreateUnsafe(node);
				}

				// Token: 0x06005C77 RID: 23671 RVA: 0x00132E86 File Offset: 0x00131086
				public above above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above.CreateUnsafe(node);
				}

				// Token: 0x06005C78 RID: 23672 RVA: 0x00132E8E File Offset: 0x0013108E
				public titleOf titleOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf.CreateUnsafe(node);
				}

				// Token: 0x06005C79 RID: 23673 RVA: 0x00132E96 File Offset: 0x00131096
				public titleAboveMode titleAboveMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode.CreateUnsafe(node);
				}
			}

			// Token: 0x02000DF6 RID: 3574
			public class NodeCast
			{
				// Token: 0x06005C7B RID: 23675 RVA: 0x00132E9E File Offset: 0x0013109E
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005C7C RID: 23676 RVA: 0x00132EB0 File Offset: 0x001310B0
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06005C7D RID: 23677 RVA: 0x00132F04 File Offset: 0x00131104
				public trim trim(ProgramNode node)
				{
					trim? trim = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trim.Value;
				}

				// Token: 0x06005C7E RID: 23678 RVA: 0x00132F58 File Offset: 0x00131158
				public area area(ProgramNode node)
				{
					area? area = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area.CreateSafe(this._builders, node);
					if (area == null)
					{
						string text = "node";
						string text2 = "expected node for symbol area but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return area.Value;
				}

				// Token: 0x06005C7F RID: 23679 RVA: 0x00132FAC File Offset: 0x001311AC
				public trimLeft trimLeft(ProgramNode node)
				{
					trimLeft? trimLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft.CreateSafe(this._builders, node);
					if (trimLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimLeft.Value;
				}

				// Token: 0x06005C80 RID: 23680 RVA: 0x00133000 File Offset: 0x00131200
				public trimBottom trimBottom(ProgramNode node)
				{
					trimBottom? trimBottom = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom.CreateSafe(this._builders, node);
					if (trimBottom == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimBottom but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimBottom.Value;
				}

				// Token: 0x06005C81 RID: 23681 RVA: 0x00133054 File Offset: 0x00131254
				public trimTop trimTop(ProgramNode node)
				{
					trimTop? trimTop = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop.CreateSafe(this._builders, node);
					if (trimTop == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimTop but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimTop.Value;
				}

				// Token: 0x06005C82 RID: 23682 RVA: 0x001330A8 File Offset: 0x001312A8
				public sheetSection sheetSection(ProgramNode node)
				{
					sheetSection? sheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection.CreateSafe(this._builders, node);
					if (sheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sheetSection.Value;
				}

				// Token: 0x06005C83 RID: 23683 RVA: 0x001330FC File Offset: 0x001312FC
				public horizontalSheetSection horizontalSheetSection(ProgramNode node)
				{
					horizontalSheetSection? horizontalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection.CreateSafe(this._builders, node);
					if (horizontalSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol horizontalSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return horizontalSheetSection.Value;
				}

				// Token: 0x06005C84 RID: 23684 RVA: 0x00133150 File Offset: 0x00131350
				public verticalSheetSection verticalSheetSection(ProgramNode node)
				{
					verticalSheetSection? verticalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection.CreateSafe(this._builders, node);
					if (verticalSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol verticalSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return verticalSheetSection.Value;
				}

				// Token: 0x06005C85 RID: 23685 RVA: 0x001331A4 File Offset: 0x001313A4
				public uncleanedSheetSection uncleanedSheetSection(ProgramNode node)
				{
					uncleanedSheetSection? uncleanedSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection.CreateSafe(this._builders, node);
					if (uncleanedSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol uncleanedSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return uncleanedSheetSection.Value;
				}

				// Token: 0x06005C86 RID: 23686 RVA: 0x001331F8 File Offset: 0x001313F8
				public wholeSheet wholeSheet(ProgramNode node)
				{
					wholeSheet? wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet.CreateSafe(this._builders, node);
					if (wholeSheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol wholeSheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wholeSheet.Value;
				}

				// Token: 0x06005C87 RID: 23687 RVA: 0x0013324C File Offset: 0x0013144C
				public wholeSheetFull wholeSheetFull(ProgramNode node)
				{
					wholeSheetFull? wholeSheetFull = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull.CreateSafe(this._builders, node);
					if (wholeSheetFull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol wholeSheetFull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wholeSheetFull.Value;
				}

				// Token: 0x06005C88 RID: 23688 RVA: 0x001332A0 File Offset: 0x001314A0
				public sheet sheet(ProgramNode node)
				{
					sheet? sheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet.CreateSafe(this._builders, node);
					if (sheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sheet.Value;
				}

				// Token: 0x06005C89 RID: 23689 RVA: 0x001332F4 File Offset: 0x001314F4
				public horizontalSheetSplits horizontalSheetSplits(ProgramNode node)
				{
					horizontalSheetSplits? horizontalSheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits.CreateSafe(this._builders, node);
					if (horizontalSheetSplits == null)
					{
						string text = "node";
						string text2 = "expected node for symbol horizontalSheetSplits but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return horizontalSheetSplits.Value;
				}

				// Token: 0x06005C8A RID: 23690 RVA: 0x00133348 File Offset: 0x00131548
				public verticalSheetSplits verticalSheetSplits(ProgramNode node)
				{
					verticalSheetSplits? verticalSheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits.CreateSafe(this._builders, node);
					if (verticalSheetSplits == null)
					{
						string text = "node";
						string text2 = "expected node for symbol verticalSheetSplits but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return verticalSheetSplits.Value;
				}

				// Token: 0x06005C8B RID: 23691 RVA: 0x0013339C File Offset: 0x0013159C
				public sheetSplits sheetSplits(ProgramNode node)
				{
					sheetSplits? sheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits.CreateSafe(this._builders, node);
					if (sheetSplits == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sheetSplits but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sheetSplits.Value;
				}

				// Token: 0x06005C8C RID: 23692 RVA: 0x001333F0 File Offset: 0x001315F0
				public index index(ProgramNode node)
				{
					index? index = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index.CreateSafe(this._builders, node);
					if (index == null)
					{
						string text = "node";
						string text2 = "expected node for symbol index but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return index.Value;
				}

				// Token: 0x06005C8D RID: 23693 RVA: 0x00133444 File Offset: 0x00131644
				public rangeName rangeName(ProgramNode node)
				{
					rangeName? rangeName = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName.CreateSafe(this._builders, node);
					if (rangeName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeName.Value;
				}

				// Token: 0x06005C8E RID: 23694 RVA: 0x00133498 File Offset: 0x00131698
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x06005C8F RID: 23695 RVA: 0x001334EC File Offset: 0x001316EC
				public splitMode splitMode(ProgramNode node)
				{
					splitMode? splitMode = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode.CreateSafe(this._builders, node);
					if (splitMode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitMode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitMode.Value;
				}

				// Token: 0x06005C90 RID: 23696 RVA: 0x00133540 File Offset: 0x00131740
				public styleFilter styleFilter(ProgramNode node)
				{
					styleFilter? styleFilter = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter.CreateSafe(this._builders, node);
					if (styleFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol styleFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return styleFilter.Value;
				}

				// Token: 0x06005C91 RID: 23697 RVA: 0x00133594 File Offset: 0x00131794
				public mProgram mProgram(ProgramNode node)
				{
					mProgram? mProgram = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram.CreateSafe(this._builders, node);
					if (mProgram == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mProgram but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mProgram.Value;
				}

				// Token: 0x06005C92 RID: 23698 RVA: 0x001335E8 File Offset: 0x001317E8
				public mTable mTable(ProgramNode node)
				{
					mTable? mTable = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable.CreateSafe(this._builders, node);
					if (mTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mTable.Value;
				}

				// Token: 0x06005C93 RID: 23699 RVA: 0x0013363C File Offset: 0x0013183C
				public mSection mSection(ProgramNode node)
				{
					mSection? mSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection.CreateSafe(this._builders, node);
					if (mSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mSection.Value;
				}

				// Token: 0x06005C94 RID: 23700 RVA: 0x00133690 File Offset: 0x00131890
				public withoutFormatting withoutFormatting(ProgramNode node)
				{
					withoutFormatting? withoutFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting.CreateSafe(this._builders, node);
					if (withoutFormatting == null)
					{
						string text = "node";
						string text2 = "expected node for symbol withoutFormatting but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return withoutFormatting.Value;
				}

				// Token: 0x06005C95 RID: 23701 RVA: 0x001336E4 File Offset: 0x001318E4
				public startTitle startTitle(ProgramNode node)
				{
					startTitle? startTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle.CreateSafe(this._builders, node);
					if (startTitle == null)
					{
						string text = "node";
						string text2 = "expected node for symbol startTitle but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startTitle.Value;
				}

				// Token: 0x06005C96 RID: 23702 RVA: 0x00133738 File Offset: 0x00131938
				public title title(ProgramNode node)
				{
					title? title = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title.CreateSafe(this._builders, node);
					if (title == null)
					{
						string text = "node";
						string text2 = "expected node for symbol title but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return title.Value;
				}

				// Token: 0x06005C97 RID: 23703 RVA: 0x0013378C File Offset: 0x0013198C
				public aboveOrLeftmost aboveOrLeftmost(ProgramNode node)
				{
					aboveOrLeftmost? aboveOrLeftmost = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost.CreateSafe(this._builders, node);
					if (aboveOrLeftmost == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrLeftmost but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrLeftmost.Value;
				}

				// Token: 0x06005C98 RID: 23704 RVA: 0x001337E0 File Offset: 0x001319E0
				public aboveOrOutput aboveOrOutput(ProgramNode node)
				{
					aboveOrOutput? aboveOrOutput = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput.CreateSafe(this._builders, node);
					if (aboveOrOutput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrOutput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrOutput.Value;
				}

				// Token: 0x06005C99 RID: 23705 RVA: 0x00133834 File Offset: 0x00131A34
				public aboveOrHeader aboveOrHeader(ProgramNode node)
				{
					aboveOrHeader? aboveOrHeader = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader.CreateSafe(this._builders, node);
					if (aboveOrHeader == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrHeader but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrHeader.Value;
				}

				// Token: 0x06005C9A RID: 23706 RVA: 0x00133888 File Offset: 0x00131A88
				public headerSection headerSection(ProgramNode node)
				{
					headerSection? headerSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection.CreateSafe(this._builders, node);
					if (headerSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol headerSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return headerSection.Value;
				}

				// Token: 0x06005C9B RID: 23707 RVA: 0x001338DC File Offset: 0x00131ADC
				public splitForTitle splitForTitle(ProgramNode node)
				{
					splitForTitle? splitForTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle.CreateSafe(this._builders, node);
					if (splitForTitle == null)
					{
						string text = "node";
						string text2 = "expected node for symbol splitForTitle but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitForTitle.Value;
				}

				// Token: 0x06005C9C RID: 23708 RVA: 0x00133930 File Offset: 0x00131B30
				public above above(ProgramNode node)
				{
					above? above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above.CreateSafe(this._builders, node);
					if (above == null)
					{
						string text = "node";
						string text2 = "expected node for symbol above but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return above.Value;
				}

				// Token: 0x06005C9D RID: 23709 RVA: 0x00133984 File Offset: 0x00131B84
				public titleOf titleOf(ProgramNode node)
				{
					titleOf? titleOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf.CreateSafe(this._builders, node);
					if (titleOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol titleOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleOf.Value;
				}

				// Token: 0x06005C9E RID: 23710 RVA: 0x001339D8 File Offset: 0x00131BD8
				public titleAboveMode titleAboveMode(ProgramNode node)
				{
					titleAboveMode? titleAboveMode = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode.CreateSafe(this._builders, node);
					if (titleAboveMode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol titleAboveMode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleAboveMode.Value;
				}

				// Token: 0x04002B4E RID: 11086
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DF7 RID: 3575
			public class RuleCast
			{
				// Token: 0x06005C9F RID: 23711 RVA: 0x00133A29 File Offset: 0x00131C29
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005CA0 RID: 23712 RVA: 0x00133A38 File Offset: 0x00131C38
				public Output Output(ProgramNode node)
				{
					Output? output = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06005CA1 RID: 23713 RVA: 0x00133A8C File Offset: 0x00131C8C
				public Trim Trim(ProgramNode node)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Trim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trim.Value;
				}

				// Token: 0x06005CA2 RID: 23714 RVA: 0x00133AE0 File Offset: 0x00131CE0
				public TrimHidden TrimHidden(ProgramNode node)
				{
					TrimHidden? trimHidden = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHidden.CreateSafe(this._builders, node);
					if (trimHidden == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimHidden but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimHidden.Value;
				}

				// Token: 0x06005CA3 RID: 23715 RVA: 0x00133B34 File Offset: 0x00131D34
				public area_trimLeft area_trimLeft(ProgramNode node)
				{
					area_trimLeft? area_trimLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.area_trimLeft.CreateSafe(this._builders, node);
					if (area_trimLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol area_trimLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return area_trimLeft.Value;
				}

				// Token: 0x06005CA4 RID: 23716 RVA: 0x00133B88 File Offset: 0x00131D88
				public Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange DefinedRange(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange? definedRange = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange.CreateSafe(this._builders, node);
					if (definedRange == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DefinedRange but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return definedRange.Value;
				}

				// Token: 0x06005CA5 RID: 23717 RVA: 0x00133BDC File Offset: 0x00131DDC
				public trimLeft_trimBottom trimLeft_trimBottom(ProgramNode node)
				{
					trimLeft_trimBottom? trimLeft_trimBottom = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimLeft_trimBottom.CreateSafe(this._builders, node);
					if (trimLeft_trimBottom == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimLeft_trimBottom but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimLeft_trimBottom.Value;
				}

				// Token: 0x06005CA6 RID: 23718 RVA: 0x00133C30 File Offset: 0x00131E30
				public TrimLeftSingleCellColumns TrimLeftSingleCellColumns(ProgramNode node)
				{
					TrimLeftSingleCellColumns? trimLeftSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimLeftSingleCellColumns.CreateSafe(this._builders, node);
					if (trimLeftSingleCellColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimLeftSingleCellColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimLeftSingleCellColumns.Value;
				}

				// Token: 0x06005CA7 RID: 23719 RVA: 0x00133C84 File Offset: 0x00131E84
				public trimBottom_trimTop trimBottom_trimTop(ProgramNode node)
				{
					trimBottom_trimTop? trimBottom_trimTop = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimBottom_trimTop.CreateSafe(this._builders, node);
					if (trimBottom_trimTop == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimBottom_trimTop but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimBottom_trimTop.Value;
				}

				// Token: 0x06005CA8 RID: 23720 RVA: 0x00133CD8 File Offset: 0x00131ED8
				public TrimBottomSingleCellRows TrimBottomSingleCellRows(ProgramNode node)
				{
					TrimBottomSingleCellRows? trimBottomSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBottomSingleCellRows.CreateSafe(this._builders, node);
					if (trimBottomSingleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimBottomSingleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimBottomSingleCellRows.Value;
				}

				// Token: 0x06005CA9 RID: 23721 RVA: 0x00133D2C File Offset: 0x00131F2C
				public TakeUntilEmptyRow TakeUntilEmptyRow(ProgramNode node)
				{
					TakeUntilEmptyRow? takeUntilEmptyRow = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyRow.CreateSafe(this._builders, node);
					if (takeUntilEmptyRow == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TakeUntilEmptyRow but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return takeUntilEmptyRow.Value;
				}

				// Token: 0x06005CAA RID: 23722 RVA: 0x00133D80 File Offset: 0x00131F80
				public TrimAboveBottomBorder TrimAboveBottomBorder(ProgramNode node)
				{
					TrimAboveBottomBorder? trimAboveBottomBorder = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimAboveBottomBorder.CreateSafe(this._builders, node);
					if (trimAboveBottomBorder == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimAboveBottomBorder but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimAboveBottomBorder.Value;
				}

				// Token: 0x06005CAB RID: 23723 RVA: 0x00133DD4 File Offset: 0x00131FD4
				public trimTop_sheetSection trimTop_sheetSection(ProgramNode node)
				{
					trimTop_sheetSection? trimTop_sheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimTop_sheetSection.CreateSafe(this._builders, node);
					if (trimTop_sheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimTop_sheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimTop_sheetSection.Value;
				}

				// Token: 0x06005CAC RID: 23724 RVA: 0x00133E28 File Offset: 0x00132028
				public FreezePaneTight FreezePaneTight(ProgramNode node)
				{
					FreezePaneTight? freezePaneTight = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneTight.CreateSafe(this._builders, node);
					if (freezePaneTight == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FreezePaneTight but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return freezePaneTight.Value;
				}

				// Token: 0x06005CAD RID: 23725 RVA: 0x00133E7C File Offset: 0x0013207C
				public FreezePaneToBlanks FreezePaneToBlanks(ProgramNode node)
				{
					FreezePaneToBlanks? freezePaneToBlanks = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToBlanks.CreateSafe(this._builders, node);
					if (freezePaneToBlanks == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FreezePaneToBlanks but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return freezePaneToBlanks.Value;
				}

				// Token: 0x06005CAE RID: 23726 RVA: 0x00133ED0 File Offset: 0x001320D0
				public FreezePaneToMultipleBlanks FreezePaneToMultipleBlanks(ProgramNode node)
				{
					FreezePaneToMultipleBlanks? freezePaneToMultipleBlanks = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToMultipleBlanks.CreateSafe(this._builders, node);
					if (freezePaneToMultipleBlanks == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FreezePaneToMultipleBlanks but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return freezePaneToMultipleBlanks.Value;
				}

				// Token: 0x06005CAF RID: 23727 RVA: 0x00133F24 File Offset: 0x00132124
				public TrimTopMergedCellRows TrimTopMergedCellRows(ProgramNode node)
				{
					TrimTopMergedCellRows? trimTopMergedCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopMergedCellRows.CreateSafe(this._builders, node);
					if (trimTopMergedCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimTopMergedCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimTopMergedCellRows.Value;
				}

				// Token: 0x06005CB0 RID: 23728 RVA: 0x00133F78 File Offset: 0x00132178
				public TrimTopFullWidthMergedCellRows TrimTopFullWidthMergedCellRows(ProgramNode node)
				{
					TrimTopFullWidthMergedCellRows? trimTopFullWidthMergedCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopFullWidthMergedCellRows.CreateSafe(this._builders, node);
					if (trimTopFullWidthMergedCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimTopFullWidthMergedCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimTopFullWidthMergedCellRows.Value;
				}

				// Token: 0x06005CB1 RID: 23729 RVA: 0x00133FCC File Offset: 0x001321CC
				public TrimTopSingleCellRows TrimTopSingleCellRows(ProgramNode node)
				{
					TrimTopSingleCellRows? trimTopSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopSingleCellRows.CreateSafe(this._builders, node);
					if (trimTopSingleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimTopSingleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimTopSingleCellRows.Value;
				}

				// Token: 0x06005CB2 RID: 23730 RVA: 0x00134020 File Offset: 0x00132220
				public TrimBelowTopBorder TrimBelowTopBorder(ProgramNode node)
				{
					TrimBelowTopBorder? trimBelowTopBorder = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBelowTopBorder.CreateSafe(this._builders, node);
					if (trimBelowTopBorder == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimBelowTopBorder but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimBelowTopBorder.Value;
				}

				// Token: 0x06005CB3 RID: 23731 RVA: 0x00134074 File Offset: 0x00132274
				public TakeAfterEmptyRow TakeAfterEmptyRow(ProgramNode node)
				{
					TakeAfterEmptyRow? takeAfterEmptyRow = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeAfterEmptyRow.CreateSafe(this._builders, node);
					if (takeAfterEmptyRow == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TakeAfterEmptyRow but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return takeAfterEmptyRow.Value;
				}

				// Token: 0x06005CB4 RID: 23732 RVA: 0x001340C8 File Offset: 0x001322C8
				public sheetSection_horizontalSheetSection sheetSection_horizontalSheetSection(ProgramNode node)
				{
					sheetSection_horizontalSheetSection? sheetSection_horizontalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.sheetSection_horizontalSheetSection.CreateSafe(this._builders, node);
					if (sheetSection_horizontalSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sheetSection_horizontalSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sheetSection_horizontalSheetSection.Value;
				}

				// Token: 0x06005CB5 RID: 23733 RVA: 0x0013411C File Offset: 0x0013231C
				public TakeUntilEmptyColumn TakeUntilEmptyColumn(ProgramNode node)
				{
					TakeUntilEmptyColumn? takeUntilEmptyColumn = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyColumn.CreateSafe(this._builders, node);
					if (takeUntilEmptyColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TakeUntilEmptyColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return takeUntilEmptyColumn.Value;
				}

				// Token: 0x06005CB6 RID: 23734 RVA: 0x00134170 File Offset: 0x00132370
				public TrimRightSingleCellColumns TrimRightSingleCellColumns(ProgramNode node)
				{
					TrimRightSingleCellColumns? trimRightSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimRightSingleCellColumns.CreateSafe(this._builders, node);
					if (trimRightSingleCellColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimRightSingleCellColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimRightSingleCellColumns.Value;
				}

				// Token: 0x06005CB7 RID: 23735 RVA: 0x001341C4 File Offset: 0x001323C4
				public horizontalSheetSection_verticalSheetSection horizontalSheetSection_verticalSheetSection(ProgramNode node)
				{
					horizontalSheetSection_verticalSheetSection? horizontalSheetSection_verticalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.horizontalSheetSection_verticalSheetSection.CreateSafe(this._builders, node);
					if (horizontalSheetSection_verticalSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol horizontalSheetSection_verticalSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return horizontalSheetSection_verticalSheetSection.Value;
				}

				// Token: 0x06005CB8 RID: 23736 RVA: 0x00134218 File Offset: 0x00132418
				public KthHorizontal KthHorizontal(ProgramNode node)
				{
					KthHorizontal? kthHorizontal = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthHorizontal.CreateSafe(this._builders, node);
					if (kthHorizontal == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthHorizontal but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthHorizontal.Value;
				}

				// Token: 0x06005CB9 RID: 23737 RVA: 0x0013426C File Offset: 0x0013246C
				public verticalSheetSection_uncleanedSheetSection verticalSheetSection_uncleanedSheetSection(ProgramNode node)
				{
					verticalSheetSection_uncleanedSheetSection? verticalSheetSection_uncleanedSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.verticalSheetSection_uncleanedSheetSection.CreateSafe(this._builders, node);
					if (verticalSheetSection_uncleanedSheetSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol verticalSheetSection_uncleanedSheetSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return verticalSheetSection_uncleanedSheetSection.Value;
				}

				// Token: 0x06005CBA RID: 23738 RVA: 0x001342C0 File Offset: 0x001324C0
				public KthVertical KthVertical(ProgramNode node)
				{
					KthVertical? kthVertical = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthVertical.CreateSafe(this._builders, node);
					if (kthVertical == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthVertical but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthVertical.Value;
				}

				// Token: 0x06005CBB RID: 23739 RVA: 0x00134314 File Offset: 0x00132514
				public uncleanedSheetSection_wholeSheet uncleanedSheetSection_wholeSheet(ProgramNode node)
				{
					uncleanedSheetSection_wholeSheet? uncleanedSheetSection_wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.uncleanedSheetSection_wholeSheet.CreateSafe(this._builders, node);
					if (uncleanedSheetSection_wholeSheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol uncleanedSheetSection_wholeSheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return uncleanedSheetSection_wholeSheet.Value;
				}

				// Token: 0x06005CBC RID: 23740 RVA: 0x00134368 File Offset: 0x00132568
				public KthSplit KthSplit(ProgramNode node)
				{
					KthSplit? kthSplit = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthSplit.CreateSafe(this._builders, node);
					if (kthSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthSplit.Value;
				}

				// Token: 0x06005CBD RID: 23741 RVA: 0x001343BC File Offset: 0x001325BC
				public Area Area(ProgramNode node)
				{
					Area? area = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Area.CreateSafe(this._builders, node);
					if (area == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Area but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return area.Value;
				}

				// Token: 0x06005CBE RID: 23742 RVA: 0x00134410 File Offset: 0x00132610
				public wholeSheet_wholeSheetFull wholeSheet_wholeSheetFull(ProgramNode node)
				{
					wholeSheet_wholeSheetFull? wholeSheet_wholeSheetFull = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.wholeSheet_wholeSheetFull.CreateSafe(this._builders, node);
					if (wholeSheet_wholeSheetFull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol wholeSheet_wholeSheetFull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wholeSheet_wholeSheetFull.Value;
				}

				// Token: 0x06005CBF RID: 23743 RVA: 0x00134464 File Offset: 0x00132664
				public TrimHiddenWholeSheet TrimHiddenWholeSheet(ProgramNode node)
				{
					TrimHiddenWholeSheet? trimHiddenWholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHiddenWholeSheet.CreateSafe(this._builders, node);
					if (trimHiddenWholeSheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimHiddenWholeSheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimHiddenWholeSheet.Value;
				}

				// Token: 0x06005CC0 RID: 23744 RVA: 0x001344B8 File Offset: 0x001326B8
				public WholeSheet WholeSheet(ProgramNode node)
				{
					WholeSheet? wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WholeSheet.CreateSafe(this._builders, node);
					if (wholeSheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WholeSheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wholeSheet.Value;
				}

				// Token: 0x06005CC1 RID: 23745 RVA: 0x0013450C File Offset: 0x0013270C
				public WithFormatting WithFormatting(ProgramNode node)
				{
					WithFormatting? withFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithFormatting.CreateSafe(this._builders, node);
					if (withFormatting == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WithFormatting but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return withFormatting.Value;
				}

				// Token: 0x06005CC2 RID: 23746 RVA: 0x00134560 File Offset: 0x00132760
				public SplitOnEmptyRows SplitOnEmptyRows(ProgramNode node)
				{
					SplitOnEmptyRows? splitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyRows.CreateSafe(this._builders, node);
					if (splitOnEmptyRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitOnEmptyRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitOnEmptyRows.Value;
				}

				// Token: 0x06005CC3 RID: 23747 RVA: 0x001345B4 File Offset: 0x001327B4
				public SplitOnMatchingRows SplitOnMatchingRows(ProgramNode node)
				{
					SplitOnMatchingRows? splitOnMatchingRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnMatchingRows.CreateSafe(this._builders, node);
					if (splitOnMatchingRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitOnMatchingRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitOnMatchingRows.Value;
				}

				// Token: 0x06005CC4 RID: 23748 RVA: 0x00134608 File Offset: 0x00132808
				public SplitOnEmptyColumns SplitOnEmptyColumns(ProgramNode node)
				{
					SplitOnEmptyColumns? splitOnEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyColumns.CreateSafe(this._builders, node);
					if (splitOnEmptyColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitOnEmptyColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitOnEmptyColumns.Value;
				}

				// Token: 0x06005CC5 RID: 23749 RVA: 0x0013465C File Offset: 0x0013285C
				public BorderedAreas BorderedAreas(ProgramNode node)
				{
					BorderedAreas? borderedAreas = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BorderedAreas.CreateSafe(this._builders, node);
					if (borderedAreas == null)
					{
						string text = "node";
						string text2 = "expected node for symbol BorderedAreas but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return borderedAreas.Value;
				}

				// Token: 0x06005CC6 RID: 23750 RVA: 0x001346B0 File Offset: 0x001328B0
				public mProgram_mTable mProgram_mTable(ProgramNode node)
				{
					mProgram_mTable? mProgram_mTable = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.mProgram_mTable.CreateSafe(this._builders, node);
					if (mProgram_mTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mProgram_mTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mProgram_mTable.Value;
				}

				// Token: 0x06005CC7 RID: 23751 RVA: 0x00134704 File Offset: 0x00132904
				public RemoveEmptyRows RemoveEmptyRows(ProgramNode node)
				{
					RemoveEmptyRows? removeEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyRows.CreateSafe(this._builders, node);
					if (removeEmptyRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RemoveEmptyRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return removeEmptyRows.Value;
				}

				// Token: 0x06005CC8 RID: 23752 RVA: 0x00134758 File Offset: 0x00132958
				public RemoveEmptyColumns RemoveEmptyColumns(ProgramNode node)
				{
					RemoveEmptyColumns? removeEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyColumns.CreateSafe(this._builders, node);
					if (removeEmptyColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RemoveEmptyColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return removeEmptyColumns.Value;
				}

				// Token: 0x06005CC9 RID: 23753 RVA: 0x001347AC File Offset: 0x001329AC
				public MWholeSheet MWholeSheet(ProgramNode node)
				{
					MWholeSheet? mwholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MWholeSheet.CreateSafe(this._builders, node);
					if (mwholeSheet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MWholeSheet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mwholeSheet.Value;
				}

				// Token: 0x06005CCA RID: 23754 RVA: 0x00134800 File Offset: 0x00132A00
				public KthMSection KthMSection(ProgramNode node)
				{
					KthMSection? kthMSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthMSection.CreateSafe(this._builders, node);
					if (kthMSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthMSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthMSection.Value;
				}

				// Token: 0x06005CCB RID: 23755 RVA: 0x00134854 File Offset: 0x00132A54
				public KthAndNextMSection KthAndNextMSection(ProgramNode node)
				{
					KthAndNextMSection? kthAndNextMSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthAndNextMSection.CreateSafe(this._builders, node);
					if (kthAndNextMSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthAndNextMSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthAndNextMSection.Value;
				}

				// Token: 0x06005CCC RID: 23756 RVA: 0x001348A8 File Offset: 0x00132AA8
				public MTrimTopSingleCellRows MTrimTopSingleCellRows(ProgramNode node)
				{
					MTrimTopSingleCellRows? mtrimTopSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleCellRows.CreateSafe(this._builders, node);
					if (mtrimTopSingleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimTopSingleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimTopSingleCellRows.Value;
				}

				// Token: 0x06005CCD RID: 23757 RVA: 0x001348FC File Offset: 0x00132AFC
				public MTrimTopSingleLeftCellRows MTrimTopSingleLeftCellRows(ProgramNode node)
				{
					MTrimTopSingleLeftCellRows? mtrimTopSingleLeftCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleLeftCellRows.CreateSafe(this._builders, node);
					if (mtrimTopSingleLeftCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimTopSingleLeftCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimTopSingleLeftCellRows.Value;
				}

				// Token: 0x06005CCE RID: 23758 RVA: 0x00134950 File Offset: 0x00132B50
				public MTrimBottomSingleCellRows MTrimBottomSingleCellRows(ProgramNode node)
				{
					MTrimBottomSingleCellRows? mtrimBottomSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomSingleCellRows.CreateSafe(this._builders, node);
					if (mtrimBottomSingleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimBottomSingleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimBottomSingleCellRows.Value;
				}

				// Token: 0x06005CCF RID: 23759 RVA: 0x001349A4 File Offset: 0x00132BA4
				public MTrimLeftSingleCellColumns MTrimLeftSingleCellColumns(ProgramNode node)
				{
					MTrimLeftSingleCellColumns? mtrimLeftSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimLeftSingleCellColumns.CreateSafe(this._builders, node);
					if (mtrimLeftSingleCellColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimLeftSingleCellColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimLeftSingleCellColumns.Value;
				}

				// Token: 0x06005CD0 RID: 23760 RVA: 0x001349F8 File Offset: 0x00132BF8
				public MTrimRightSingleCellColumns MTrimRightSingleCellColumns(ProgramNode node)
				{
					MTrimRightSingleCellColumns? mtrimRightSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimRightSingleCellColumns.CreateSafe(this._builders, node);
					if (mtrimRightSingleCellColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimRightSingleCellColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimRightSingleCellColumns.Value;
				}

				// Token: 0x06005CD1 RID: 23761 RVA: 0x00134A4C File Offset: 0x00132C4C
				public MTrimTopDoubleCellRows MTrimTopDoubleCellRows(ProgramNode node)
				{
					MTrimTopDoubleCellRows? mtrimTopDoubleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopDoubleCellRows.CreateSafe(this._builders, node);
					if (mtrimTopDoubleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimTopDoubleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimTopDoubleCellRows.Value;
				}

				// Token: 0x06005CD2 RID: 23762 RVA: 0x00134AA0 File Offset: 0x00132CA0
				public MTrimBottomDoubleCellRows MTrimBottomDoubleCellRows(ProgramNode node)
				{
					MTrimBottomDoubleCellRows? mtrimBottomDoubleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomDoubleCellRows.CreateSafe(this._builders, node);
					if (mtrimBottomDoubleCellRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MTrimBottomDoubleCellRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mtrimBottomDoubleCellRows.Value;
				}

				// Token: 0x06005CD3 RID: 23763 RVA: 0x00134AF4 File Offset: 0x00132CF4
				public MSplitOnEmptyRows MSplitOnEmptyRows(ProgramNode node)
				{
					MSplitOnEmptyRows? msplitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyRows.CreateSafe(this._builders, node);
					if (msplitOnEmptyRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MSplitOnEmptyRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return msplitOnEmptyRows.Value;
				}

				// Token: 0x06005CD4 RID: 23764 RVA: 0x00134B48 File Offset: 0x00132D48
				public MSplitOnEmptyColumns MSplitOnEmptyColumns(ProgramNode node)
				{
					MSplitOnEmptyColumns? msplitOnEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyColumns.CreateSafe(this._builders, node);
					if (msplitOnEmptyColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MSplitOnEmptyColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return msplitOnEmptyColumns.Value;
				}

				// Token: 0x06005CD5 RID: 23765 RVA: 0x00134B9C File Offset: 0x00132D9C
				public WithoutFormatting WithoutFormatting(ProgramNode node)
				{
					WithoutFormatting? withoutFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithoutFormatting.CreateSafe(this._builders, node);
					if (withoutFormatting == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WithoutFormatting but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return withoutFormatting.Value;
				}

				// Token: 0x06005CD6 RID: 23766 RVA: 0x00134BF0 File Offset: 0x00132DF0
				public StartTitle StartTitle(ProgramNode node)
				{
					StartTitle? startTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.StartTitle.CreateSafe(this._builders, node);
					if (startTitle == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartTitle but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startTitle.Value;
				}

				// Token: 0x06005CD7 RID: 23767 RVA: 0x00134C44 File Offset: 0x00132E44
				public title_above title_above(ProgramNode node)
				{
					title_above? title_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.title_above.CreateSafe(this._builders, node);
					if (title_above == null)
					{
						string text = "node";
						string text2 = "expected node for symbol title_above but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return title_above.Value;
				}

				// Token: 0x06005CD8 RID: 23768 RVA: 0x00134C98 File Offset: 0x00132E98
				public TopLeftCell TopLeftCell(ProgramNode node)
				{
					TopLeftCell? topLeftCell = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopLeftCell.CreateSafe(this._builders, node);
					if (topLeftCell == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TopLeftCell but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return topLeftCell.Value;
				}

				// Token: 0x06005CD9 RID: 23769 RVA: 0x00134CEC File Offset: 0x00132EEC
				public TopSameFontCells TopSameFontCells(ProgramNode node)
				{
					TopSameFontCells? topSameFontCells = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopSameFontCells.CreateSafe(this._builders, node);
					if (topSameFontCells == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TopSameFontCells but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return topSameFontCells.Value;
				}

				// Token: 0x06005CDA RID: 23770 RVA: 0x00134D40 File Offset: 0x00132F40
				public BottomLeftSameFontCells BottomLeftSameFontCells(ProgramNode node)
				{
					BottomLeftSameFontCells? bottomLeftSameFontCells = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BottomLeftSameFontCells.CreateSafe(this._builders, node);
					if (bottomLeftSameFontCells == null)
					{
						string text = "node";
						string text2 = "expected node for symbol BottomLeftSameFontCells but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return bottomLeftSameFontCells.Value;
				}

				// Token: 0x06005CDB RID: 23771 RVA: 0x00134D94 File Offset: 0x00132F94
				public aboveOrLeftmost_above aboveOrLeftmost_above(ProgramNode node)
				{
					aboveOrLeftmost_above? aboveOrLeftmost_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrLeftmost_above.CreateSafe(this._builders, node);
					if (aboveOrLeftmost_above == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrLeftmost_above but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrLeftmost_above.Value;
				}

				// Token: 0x06005CDC RID: 23772 RVA: 0x00134DE8 File Offset: 0x00132FE8
				public LeftmostColumn LeftmostColumn(ProgramNode node)
				{
					LeftmostColumn? leftmostColumn = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftmostColumn.CreateSafe(this._builders, node);
					if (leftmostColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeftmostColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leftmostColumn.Value;
				}

				// Token: 0x06005CDD RID: 23773 RVA: 0x00134E3C File Offset: 0x0013303C
				public LeftOf LeftOf(ProgramNode node)
				{
					LeftOf? leftOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftOf.CreateSafe(this._builders, node);
					if (leftOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeftOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leftOf.Value;
				}

				// Token: 0x06005CDE RID: 23774 RVA: 0x00134E90 File Offset: 0x00133090
				public aboveOrOutput_aboveOrHeader aboveOrOutput_aboveOrHeader(ProgramNode node)
				{
					aboveOrOutput_aboveOrHeader? aboveOrOutput_aboveOrHeader = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_aboveOrHeader.CreateSafe(this._builders, node);
					if (aboveOrOutput_aboveOrHeader == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrOutput_aboveOrHeader but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrOutput_aboveOrHeader.Value;
				}

				// Token: 0x06005CDF RID: 23775 RVA: 0x00134EE4 File Offset: 0x001330E4
				public aboveOrOutput_titleOf aboveOrOutput_titleOf(ProgramNode node)
				{
					aboveOrOutput_titleOf? aboveOrOutput_titleOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_titleOf.CreateSafe(this._builders, node);
					if (aboveOrOutput_titleOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrOutput_titleOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrOutput_titleOf.Value;
				}

				// Token: 0x06005CE0 RID: 23776 RVA: 0x00134F38 File Offset: 0x00133138
				public aboveOrHeader_above aboveOrHeader_above(ProgramNode node)
				{
					aboveOrHeader_above? aboveOrHeader_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_above.CreateSafe(this._builders, node);
					if (aboveOrHeader_above == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrHeader_above but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrHeader_above.Value;
				}

				// Token: 0x06005CE1 RID: 23777 RVA: 0x00134F8C File Offset: 0x0013318C
				public aboveOrHeader_headerSection aboveOrHeader_headerSection(ProgramNode node)
				{
					aboveOrHeader_headerSection? aboveOrHeader_headerSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_headerSection.CreateSafe(this._builders, node);
					if (aboveOrHeader_headerSection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol aboveOrHeader_headerSection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return aboveOrHeader_headerSection.Value;
				}

				// Token: 0x06005CE2 RID: 23778 RVA: 0x00134FE0 File Offset: 0x001331E0
				public FirstSplit FirstSplit(ProgramNode node)
				{
					FirstSplit? firstSplit = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FirstSplit.CreateSafe(this._builders, node);
					if (firstSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FirstSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return firstSplit.Value;
				}

				// Token: 0x06005CE3 RID: 23779 RVA: 0x00135034 File Offset: 0x00133234
				public TitleSplitOnEmptyRows TitleSplitOnEmptyRows(ProgramNode node)
				{
					TitleSplitOnEmptyRows? titleSplitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnEmptyRows.CreateSafe(this._builders, node);
					if (titleSplitOnEmptyRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TitleSplitOnEmptyRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleSplitOnEmptyRows.Value;
				}

				// Token: 0x06005CE4 RID: 23780 RVA: 0x00135088 File Offset: 0x00133288
				public TitleSplitOnMatchingRows TitleSplitOnMatchingRows(ProgramNode node)
				{
					TitleSplitOnMatchingRows? titleSplitOnMatchingRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnMatchingRows.CreateSafe(this._builders, node);
					if (titleSplitOnMatchingRows == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TitleSplitOnMatchingRows but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleSplitOnMatchingRows.Value;
				}

				// Token: 0x06005CE5 RID: 23781 RVA: 0x001350DC File Offset: 0x001332DC
				public TitleCellsAbove TitleCellsAbove(ProgramNode node)
				{
					TitleCellsAbove? titleCellsAbove = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAbove.CreateSafe(this._builders, node);
					if (titleCellsAbove == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TitleCellsAbove but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleCellsAbove.Value;
				}

				// Token: 0x06005CE6 RID: 23782 RVA: 0x00135130 File Offset: 0x00133330
				public TitleCellsAboveMatching TitleCellsAboveMatching(ProgramNode node)
				{
					TitleCellsAboveMatching? titleCellsAboveMatching = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAboveMatching.CreateSafe(this._builders, node);
					if (titleCellsAboveMatching == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TitleCellsAboveMatching but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleCellsAboveMatching.Value;
				}

				// Token: 0x06005CE7 RID: 23783 RVA: 0x00135184 File Offset: 0x00133384
				public WrapOutputForTitle WrapOutputForTitle(ProgramNode node)
				{
					WrapOutputForTitle? wrapOutputForTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WrapOutputForTitle.CreateSafe(this._builders, node);
					if (wrapOutputForTitle == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WrapOutputForTitle but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wrapOutputForTitle.Value;
				}

				// Token: 0x06005CE8 RID: 23784 RVA: 0x001351D8 File Offset: 0x001333D8
				public IncludeEmptyToLeft IncludeEmptyToLeft(ProgramNode node)
				{
					IncludeEmptyToLeft? includeEmptyToLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.IncludeEmptyToLeft.CreateSafe(this._builders, node);
					if (includeEmptyToLeft == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IncludeEmptyToLeft but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return includeEmptyToLeft.Value;
				}

				// Token: 0x04002B4F RID: 11087
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DF8 RID: 3576
			public class NodeIs
			{
				// Token: 0x06005CE9 RID: 23785 RVA: 0x00135229 File Offset: 0x00133429
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005CEA RID: 23786 RVA: 0x00135238 File Offset: 0x00133438
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CEB RID: 23787 RVA: 0x0013525C File Offset: 0x0013345C
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x06005CEC RID: 23788 RVA: 0x00135298 File Offset: 0x00133498
				public bool trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CED RID: 23789 RVA: 0x001352BC File Offset: 0x001334BC
				public bool trim(ProgramNode node, out trim value)
				{
					trim? trim = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						value = default(trim);
						return false;
					}
					value = trim.Value;
					return true;
				}

				// Token: 0x06005CEE RID: 23790 RVA: 0x001352F8 File Offset: 0x001334F8
				public bool area(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CEF RID: 23791 RVA: 0x0013531C File Offset: 0x0013351C
				public bool area(ProgramNode node, out area value)
				{
					area? area = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area.CreateSafe(this._builders, node);
					if (area == null)
					{
						value = default(area);
						return false;
					}
					value = area.Value;
					return true;
				}

				// Token: 0x06005CF0 RID: 23792 RVA: 0x00135358 File Offset: 0x00133558
				public bool trimLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CF1 RID: 23793 RVA: 0x0013537C File Offset: 0x0013357C
				public bool trimLeft(ProgramNode node, out trimLeft value)
				{
					trimLeft? trimLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft.CreateSafe(this._builders, node);
					if (trimLeft == null)
					{
						value = default(trimLeft);
						return false;
					}
					value = trimLeft.Value;
					return true;
				}

				// Token: 0x06005CF2 RID: 23794 RVA: 0x001353B8 File Offset: 0x001335B8
				public bool trimBottom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CF3 RID: 23795 RVA: 0x001353DC File Offset: 0x001335DC
				public bool trimBottom(ProgramNode node, out trimBottom value)
				{
					trimBottom? trimBottom = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom.CreateSafe(this._builders, node);
					if (trimBottom == null)
					{
						value = default(trimBottom);
						return false;
					}
					value = trimBottom.Value;
					return true;
				}

				// Token: 0x06005CF4 RID: 23796 RVA: 0x00135418 File Offset: 0x00133618
				public bool trimTop(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CF5 RID: 23797 RVA: 0x0013543C File Offset: 0x0013363C
				public bool trimTop(ProgramNode node, out trimTop value)
				{
					trimTop? trimTop = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop.CreateSafe(this._builders, node);
					if (trimTop == null)
					{
						value = default(trimTop);
						return false;
					}
					value = trimTop.Value;
					return true;
				}

				// Token: 0x06005CF6 RID: 23798 RVA: 0x00135478 File Offset: 0x00133678
				public bool sheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CF7 RID: 23799 RVA: 0x0013549C File Offset: 0x0013369C
				public bool sheetSection(ProgramNode node, out sheetSection value)
				{
					sheetSection? sheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection.CreateSafe(this._builders, node);
					if (sheetSection == null)
					{
						value = default(sheetSection);
						return false;
					}
					value = sheetSection.Value;
					return true;
				}

				// Token: 0x06005CF8 RID: 23800 RVA: 0x001354D8 File Offset: 0x001336D8
				public bool horizontalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CF9 RID: 23801 RVA: 0x001354FC File Offset: 0x001336FC
				public bool horizontalSheetSection(ProgramNode node, out horizontalSheetSection value)
				{
					horizontalSheetSection? horizontalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection.CreateSafe(this._builders, node);
					if (horizontalSheetSection == null)
					{
						value = default(horizontalSheetSection);
						return false;
					}
					value = horizontalSheetSection.Value;
					return true;
				}

				// Token: 0x06005CFA RID: 23802 RVA: 0x00135538 File Offset: 0x00133738
				public bool verticalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CFB RID: 23803 RVA: 0x0013555C File Offset: 0x0013375C
				public bool verticalSheetSection(ProgramNode node, out verticalSheetSection value)
				{
					verticalSheetSection? verticalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection.CreateSafe(this._builders, node);
					if (verticalSheetSection == null)
					{
						value = default(verticalSheetSection);
						return false;
					}
					value = verticalSheetSection.Value;
					return true;
				}

				// Token: 0x06005CFC RID: 23804 RVA: 0x00135598 File Offset: 0x00133798
				public bool uncleanedSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CFD RID: 23805 RVA: 0x001355BC File Offset: 0x001337BC
				public bool uncleanedSheetSection(ProgramNode node, out uncleanedSheetSection value)
				{
					uncleanedSheetSection? uncleanedSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection.CreateSafe(this._builders, node);
					if (uncleanedSheetSection == null)
					{
						value = default(uncleanedSheetSection);
						return false;
					}
					value = uncleanedSheetSection.Value;
					return true;
				}

				// Token: 0x06005CFE RID: 23806 RVA: 0x001355F8 File Offset: 0x001337F8
				public bool wholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005CFF RID: 23807 RVA: 0x0013561C File Offset: 0x0013381C
				public bool wholeSheet(ProgramNode node, out wholeSheet value)
				{
					wholeSheet? wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet.CreateSafe(this._builders, node);
					if (wholeSheet == null)
					{
						value = default(wholeSheet);
						return false;
					}
					value = wholeSheet.Value;
					return true;
				}

				// Token: 0x06005D00 RID: 23808 RVA: 0x00135658 File Offset: 0x00133858
				public bool wholeSheetFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D01 RID: 23809 RVA: 0x0013567C File Offset: 0x0013387C
				public bool wholeSheetFull(ProgramNode node, out wholeSheetFull value)
				{
					wholeSheetFull? wholeSheetFull = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull.CreateSafe(this._builders, node);
					if (wholeSheetFull == null)
					{
						value = default(wholeSheetFull);
						return false;
					}
					value = wholeSheetFull.Value;
					return true;
				}

				// Token: 0x06005D02 RID: 23810 RVA: 0x001356B8 File Offset: 0x001338B8
				public bool sheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D03 RID: 23811 RVA: 0x001356DC File Offset: 0x001338DC
				public bool sheet(ProgramNode node, out sheet value)
				{
					sheet? sheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet.CreateSafe(this._builders, node);
					if (sheet == null)
					{
						value = default(sheet);
						return false;
					}
					value = sheet.Value;
					return true;
				}

				// Token: 0x06005D04 RID: 23812 RVA: 0x00135718 File Offset: 0x00133918
				public bool horizontalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D05 RID: 23813 RVA: 0x0013573C File Offset: 0x0013393C
				public bool horizontalSheetSplits(ProgramNode node, out horizontalSheetSplits value)
				{
					horizontalSheetSplits? horizontalSheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits.CreateSafe(this._builders, node);
					if (horizontalSheetSplits == null)
					{
						value = default(horizontalSheetSplits);
						return false;
					}
					value = horizontalSheetSplits.Value;
					return true;
				}

				// Token: 0x06005D06 RID: 23814 RVA: 0x00135778 File Offset: 0x00133978
				public bool verticalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D07 RID: 23815 RVA: 0x0013579C File Offset: 0x0013399C
				public bool verticalSheetSplits(ProgramNode node, out verticalSheetSplits value)
				{
					verticalSheetSplits? verticalSheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits.CreateSafe(this._builders, node);
					if (verticalSheetSplits == null)
					{
						value = default(verticalSheetSplits);
						return false;
					}
					value = verticalSheetSplits.Value;
					return true;
				}

				// Token: 0x06005D08 RID: 23816 RVA: 0x001357D8 File Offset: 0x001339D8
				public bool sheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D09 RID: 23817 RVA: 0x001357FC File Offset: 0x001339FC
				public bool sheetSplits(ProgramNode node, out sheetSplits value)
				{
					sheetSplits? sheetSplits = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits.CreateSafe(this._builders, node);
					if (sheetSplits == null)
					{
						value = default(sheetSplits);
						return false;
					}
					value = sheetSplits.Value;
					return true;
				}

				// Token: 0x06005D0A RID: 23818 RVA: 0x00135838 File Offset: 0x00133A38
				public bool index(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D0B RID: 23819 RVA: 0x0013585C File Offset: 0x00133A5C
				public bool index(ProgramNode node, out index value)
				{
					index? index = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index.CreateSafe(this._builders, node);
					if (index == null)
					{
						value = default(index);
						return false;
					}
					value = index.Value;
					return true;
				}

				// Token: 0x06005D0C RID: 23820 RVA: 0x00135898 File Offset: 0x00133A98
				public bool rangeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D0D RID: 23821 RVA: 0x001358BC File Offset: 0x00133ABC
				public bool rangeName(ProgramNode node, out rangeName value)
				{
					rangeName? rangeName = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName.CreateSafe(this._builders, node);
					if (rangeName == null)
					{
						value = default(rangeName);
						return false;
					}
					value = rangeName.Value;
					return true;
				}

				// Token: 0x06005D0E RID: 23822 RVA: 0x001358F8 File Offset: 0x00133AF8
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D0F RID: 23823 RVA: 0x0013591C File Offset: 0x00133B1C
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x06005D10 RID: 23824 RVA: 0x00135958 File Offset: 0x00133B58
				public bool splitMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D11 RID: 23825 RVA: 0x0013597C File Offset: 0x00133B7C
				public bool splitMode(ProgramNode node, out splitMode value)
				{
					splitMode? splitMode = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode.CreateSafe(this._builders, node);
					if (splitMode == null)
					{
						value = default(splitMode);
						return false;
					}
					value = splitMode.Value;
					return true;
				}

				// Token: 0x06005D12 RID: 23826 RVA: 0x001359B8 File Offset: 0x00133BB8
				public bool styleFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D13 RID: 23827 RVA: 0x001359DC File Offset: 0x00133BDC
				public bool styleFilter(ProgramNode node, out styleFilter value)
				{
					styleFilter? styleFilter = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter.CreateSafe(this._builders, node);
					if (styleFilter == null)
					{
						value = default(styleFilter);
						return false;
					}
					value = styleFilter.Value;
					return true;
				}

				// Token: 0x06005D14 RID: 23828 RVA: 0x00135A18 File Offset: 0x00133C18
				public bool mProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D15 RID: 23829 RVA: 0x00135A3C File Offset: 0x00133C3C
				public bool mProgram(ProgramNode node, out mProgram value)
				{
					mProgram? mProgram = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram.CreateSafe(this._builders, node);
					if (mProgram == null)
					{
						value = default(mProgram);
						return false;
					}
					value = mProgram.Value;
					return true;
				}

				// Token: 0x06005D16 RID: 23830 RVA: 0x00135A78 File Offset: 0x00133C78
				public bool mTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D17 RID: 23831 RVA: 0x00135A9C File Offset: 0x00133C9C
				public bool mTable(ProgramNode node, out mTable value)
				{
					mTable? mTable = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable.CreateSafe(this._builders, node);
					if (mTable == null)
					{
						value = default(mTable);
						return false;
					}
					value = mTable.Value;
					return true;
				}

				// Token: 0x06005D18 RID: 23832 RVA: 0x00135AD8 File Offset: 0x00133CD8
				public bool mSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D19 RID: 23833 RVA: 0x00135AFC File Offset: 0x00133CFC
				public bool mSection(ProgramNode node, out mSection value)
				{
					mSection? mSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection.CreateSafe(this._builders, node);
					if (mSection == null)
					{
						value = default(mSection);
						return false;
					}
					value = mSection.Value;
					return true;
				}

				// Token: 0x06005D1A RID: 23834 RVA: 0x00135B38 File Offset: 0x00133D38
				public bool withoutFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D1B RID: 23835 RVA: 0x00135B5C File Offset: 0x00133D5C
				public bool withoutFormatting(ProgramNode node, out withoutFormatting value)
				{
					withoutFormatting? withoutFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting.CreateSafe(this._builders, node);
					if (withoutFormatting == null)
					{
						value = default(withoutFormatting);
						return false;
					}
					value = withoutFormatting.Value;
					return true;
				}

				// Token: 0x06005D1C RID: 23836 RVA: 0x00135B98 File Offset: 0x00133D98
				public bool startTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D1D RID: 23837 RVA: 0x00135BBC File Offset: 0x00133DBC
				public bool startTitle(ProgramNode node, out startTitle value)
				{
					startTitle? startTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle.CreateSafe(this._builders, node);
					if (startTitle == null)
					{
						value = default(startTitle);
						return false;
					}
					value = startTitle.Value;
					return true;
				}

				// Token: 0x06005D1E RID: 23838 RVA: 0x00135BF8 File Offset: 0x00133DF8
				public bool title(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D1F RID: 23839 RVA: 0x00135C1C File Offset: 0x00133E1C
				public bool title(ProgramNode node, out title value)
				{
					title? title = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title.CreateSafe(this._builders, node);
					if (title == null)
					{
						value = default(title);
						return false;
					}
					value = title.Value;
					return true;
				}

				// Token: 0x06005D20 RID: 23840 RVA: 0x00135C58 File Offset: 0x00133E58
				public bool aboveOrLeftmost(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D21 RID: 23841 RVA: 0x00135C7C File Offset: 0x00133E7C
				public bool aboveOrLeftmost(ProgramNode node, out aboveOrLeftmost value)
				{
					aboveOrLeftmost? aboveOrLeftmost = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost.CreateSafe(this._builders, node);
					if (aboveOrLeftmost == null)
					{
						value = default(aboveOrLeftmost);
						return false;
					}
					value = aboveOrLeftmost.Value;
					return true;
				}

				// Token: 0x06005D22 RID: 23842 RVA: 0x00135CB8 File Offset: 0x00133EB8
				public bool aboveOrOutput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D23 RID: 23843 RVA: 0x00135CDC File Offset: 0x00133EDC
				public bool aboveOrOutput(ProgramNode node, out aboveOrOutput value)
				{
					aboveOrOutput? aboveOrOutput = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput.CreateSafe(this._builders, node);
					if (aboveOrOutput == null)
					{
						value = default(aboveOrOutput);
						return false;
					}
					value = aboveOrOutput.Value;
					return true;
				}

				// Token: 0x06005D24 RID: 23844 RVA: 0x00135D18 File Offset: 0x00133F18
				public bool aboveOrHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D25 RID: 23845 RVA: 0x00135D3C File Offset: 0x00133F3C
				public bool aboveOrHeader(ProgramNode node, out aboveOrHeader value)
				{
					aboveOrHeader? aboveOrHeader = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader.CreateSafe(this._builders, node);
					if (aboveOrHeader == null)
					{
						value = default(aboveOrHeader);
						return false;
					}
					value = aboveOrHeader.Value;
					return true;
				}

				// Token: 0x06005D26 RID: 23846 RVA: 0x00135D78 File Offset: 0x00133F78
				public bool headerSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D27 RID: 23847 RVA: 0x00135D9C File Offset: 0x00133F9C
				public bool headerSection(ProgramNode node, out headerSection value)
				{
					headerSection? headerSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection.CreateSafe(this._builders, node);
					if (headerSection == null)
					{
						value = default(headerSection);
						return false;
					}
					value = headerSection.Value;
					return true;
				}

				// Token: 0x06005D28 RID: 23848 RVA: 0x00135DD8 File Offset: 0x00133FD8
				public bool splitForTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D29 RID: 23849 RVA: 0x00135DFC File Offset: 0x00133FFC
				public bool splitForTitle(ProgramNode node, out splitForTitle value)
				{
					splitForTitle? splitForTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle.CreateSafe(this._builders, node);
					if (splitForTitle == null)
					{
						value = default(splitForTitle);
						return false;
					}
					value = splitForTitle.Value;
					return true;
				}

				// Token: 0x06005D2A RID: 23850 RVA: 0x00135E38 File Offset: 0x00134038
				public bool above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D2B RID: 23851 RVA: 0x00135E5C File Offset: 0x0013405C
				public bool above(ProgramNode node, out above value)
				{
					above? above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above.CreateSafe(this._builders, node);
					if (above == null)
					{
						value = default(above);
						return false;
					}
					value = above.Value;
					return true;
				}

				// Token: 0x06005D2C RID: 23852 RVA: 0x00135E98 File Offset: 0x00134098
				public bool titleOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D2D RID: 23853 RVA: 0x00135EBC File Offset: 0x001340BC
				public bool titleOf(ProgramNode node, out titleOf value)
				{
					titleOf? titleOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf.CreateSafe(this._builders, node);
					if (titleOf == null)
					{
						value = default(titleOf);
						return false;
					}
					value = titleOf.Value;
					return true;
				}

				// Token: 0x06005D2E RID: 23854 RVA: 0x00135EF8 File Offset: 0x001340F8
				public bool titleAboveMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D2F RID: 23855 RVA: 0x00135F1C File Offset: 0x0013411C
				public bool titleAboveMode(ProgramNode node, out titleAboveMode value)
				{
					titleAboveMode? titleAboveMode = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode.CreateSafe(this._builders, node);
					if (titleAboveMode == null)
					{
						value = default(titleAboveMode);
						return false;
					}
					value = titleAboveMode.Value;
					return true;
				}

				// Token: 0x04002B50 RID: 11088
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DF9 RID: 3577
			public class RuleIs
			{
				// Token: 0x06005D30 RID: 23856 RVA: 0x00135F56 File Offset: 0x00134156
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005D31 RID: 23857 RVA: 0x00135F68 File Offset: 0x00134168
				public bool Output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D32 RID: 23858 RVA: 0x00135F8C File Offset: 0x0013418C
				public bool Output(ProgramNode node, out Output value)
				{
					Output? output = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(Output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x06005D33 RID: 23859 RVA: 0x00135FC8 File Offset: 0x001341C8
				public bool Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D34 RID: 23860 RVA: 0x00135FEC File Offset: 0x001341EC
				public bool Trim(ProgramNode node, out Trim value)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						value = default(Trim);
						return false;
					}
					value = trim.Value;
					return true;
				}

				// Token: 0x06005D35 RID: 23861 RVA: 0x00136028 File Offset: 0x00134228
				public bool TrimHidden(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHidden.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D36 RID: 23862 RVA: 0x0013604C File Offset: 0x0013424C
				public bool TrimHidden(ProgramNode node, out TrimHidden value)
				{
					TrimHidden? trimHidden = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHidden.CreateSafe(this._builders, node);
					if (trimHidden == null)
					{
						value = default(TrimHidden);
						return false;
					}
					value = trimHidden.Value;
					return true;
				}

				// Token: 0x06005D37 RID: 23863 RVA: 0x00136088 File Offset: 0x00134288
				public bool area_trimLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.area_trimLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D38 RID: 23864 RVA: 0x001360AC File Offset: 0x001342AC
				public bool area_trimLeft(ProgramNode node, out area_trimLeft value)
				{
					area_trimLeft? area_trimLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.area_trimLeft.CreateSafe(this._builders, node);
					if (area_trimLeft == null)
					{
						value = default(area_trimLeft);
						return false;
					}
					value = area_trimLeft.Value;
					return true;
				}

				// Token: 0x06005D39 RID: 23865 RVA: 0x001360E8 File Offset: 0x001342E8
				public bool DefinedRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D3A RID: 23866 RVA: 0x0013610C File Offset: 0x0013430C
				public bool DefinedRange(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange value)
				{
					Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange? definedRange = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange.CreateSafe(this._builders, node);
					if (definedRange == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange);
						return false;
					}
					value = definedRange.Value;
					return true;
				}

				// Token: 0x06005D3B RID: 23867 RVA: 0x00136148 File Offset: 0x00134348
				public bool trimLeft_trimBottom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimLeft_trimBottom.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D3C RID: 23868 RVA: 0x0013616C File Offset: 0x0013436C
				public bool trimLeft_trimBottom(ProgramNode node, out trimLeft_trimBottom value)
				{
					trimLeft_trimBottom? trimLeft_trimBottom = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimLeft_trimBottom.CreateSafe(this._builders, node);
					if (trimLeft_trimBottom == null)
					{
						value = default(trimLeft_trimBottom);
						return false;
					}
					value = trimLeft_trimBottom.Value;
					return true;
				}

				// Token: 0x06005D3D RID: 23869 RVA: 0x001361A8 File Offset: 0x001343A8
				public bool TrimLeftSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimLeftSingleCellColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D3E RID: 23870 RVA: 0x001361CC File Offset: 0x001343CC
				public bool TrimLeftSingleCellColumns(ProgramNode node, out TrimLeftSingleCellColumns value)
				{
					TrimLeftSingleCellColumns? trimLeftSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimLeftSingleCellColumns.CreateSafe(this._builders, node);
					if (trimLeftSingleCellColumns == null)
					{
						value = default(TrimLeftSingleCellColumns);
						return false;
					}
					value = trimLeftSingleCellColumns.Value;
					return true;
				}

				// Token: 0x06005D3F RID: 23871 RVA: 0x00136208 File Offset: 0x00134408
				public bool trimBottom_trimTop(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimBottom_trimTop.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D40 RID: 23872 RVA: 0x0013622C File Offset: 0x0013442C
				public bool trimBottom_trimTop(ProgramNode node, out trimBottom_trimTop value)
				{
					trimBottom_trimTop? trimBottom_trimTop = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimBottom_trimTop.CreateSafe(this._builders, node);
					if (trimBottom_trimTop == null)
					{
						value = default(trimBottom_trimTop);
						return false;
					}
					value = trimBottom_trimTop.Value;
					return true;
				}

				// Token: 0x06005D41 RID: 23873 RVA: 0x00136268 File Offset: 0x00134468
				public bool TrimBottomSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBottomSingleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D42 RID: 23874 RVA: 0x0013628C File Offset: 0x0013448C
				public bool TrimBottomSingleCellRows(ProgramNode node, out TrimBottomSingleCellRows value)
				{
					TrimBottomSingleCellRows? trimBottomSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBottomSingleCellRows.CreateSafe(this._builders, node);
					if (trimBottomSingleCellRows == null)
					{
						value = default(TrimBottomSingleCellRows);
						return false;
					}
					value = trimBottomSingleCellRows.Value;
					return true;
				}

				// Token: 0x06005D43 RID: 23875 RVA: 0x001362C8 File Offset: 0x001344C8
				public bool TakeUntilEmptyRow(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyRow.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D44 RID: 23876 RVA: 0x001362EC File Offset: 0x001344EC
				public bool TakeUntilEmptyRow(ProgramNode node, out TakeUntilEmptyRow value)
				{
					TakeUntilEmptyRow? takeUntilEmptyRow = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyRow.CreateSafe(this._builders, node);
					if (takeUntilEmptyRow == null)
					{
						value = default(TakeUntilEmptyRow);
						return false;
					}
					value = takeUntilEmptyRow.Value;
					return true;
				}

				// Token: 0x06005D45 RID: 23877 RVA: 0x00136328 File Offset: 0x00134528
				public bool TrimAboveBottomBorder(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimAboveBottomBorder.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D46 RID: 23878 RVA: 0x0013634C File Offset: 0x0013454C
				public bool TrimAboveBottomBorder(ProgramNode node, out TrimAboveBottomBorder value)
				{
					TrimAboveBottomBorder? trimAboveBottomBorder = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimAboveBottomBorder.CreateSafe(this._builders, node);
					if (trimAboveBottomBorder == null)
					{
						value = default(TrimAboveBottomBorder);
						return false;
					}
					value = trimAboveBottomBorder.Value;
					return true;
				}

				// Token: 0x06005D47 RID: 23879 RVA: 0x00136388 File Offset: 0x00134588
				public bool trimTop_sheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimTop_sheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D48 RID: 23880 RVA: 0x001363AC File Offset: 0x001345AC
				public bool trimTop_sheetSection(ProgramNode node, out trimTop_sheetSection value)
				{
					trimTop_sheetSection? trimTop_sheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimTop_sheetSection.CreateSafe(this._builders, node);
					if (trimTop_sheetSection == null)
					{
						value = default(trimTop_sheetSection);
						return false;
					}
					value = trimTop_sheetSection.Value;
					return true;
				}

				// Token: 0x06005D49 RID: 23881 RVA: 0x001363E8 File Offset: 0x001345E8
				public bool FreezePaneTight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneTight.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D4A RID: 23882 RVA: 0x0013640C File Offset: 0x0013460C
				public bool FreezePaneTight(ProgramNode node, out FreezePaneTight value)
				{
					FreezePaneTight? freezePaneTight = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneTight.CreateSafe(this._builders, node);
					if (freezePaneTight == null)
					{
						value = default(FreezePaneTight);
						return false;
					}
					value = freezePaneTight.Value;
					return true;
				}

				// Token: 0x06005D4B RID: 23883 RVA: 0x00136448 File Offset: 0x00134648
				public bool FreezePaneToBlanks(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToBlanks.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D4C RID: 23884 RVA: 0x0013646C File Offset: 0x0013466C
				public bool FreezePaneToBlanks(ProgramNode node, out FreezePaneToBlanks value)
				{
					FreezePaneToBlanks? freezePaneToBlanks = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToBlanks.CreateSafe(this._builders, node);
					if (freezePaneToBlanks == null)
					{
						value = default(FreezePaneToBlanks);
						return false;
					}
					value = freezePaneToBlanks.Value;
					return true;
				}

				// Token: 0x06005D4D RID: 23885 RVA: 0x001364A8 File Offset: 0x001346A8
				public bool FreezePaneToMultipleBlanks(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToMultipleBlanks.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D4E RID: 23886 RVA: 0x001364CC File Offset: 0x001346CC
				public bool FreezePaneToMultipleBlanks(ProgramNode node, out FreezePaneToMultipleBlanks value)
				{
					FreezePaneToMultipleBlanks? freezePaneToMultipleBlanks = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToMultipleBlanks.CreateSafe(this._builders, node);
					if (freezePaneToMultipleBlanks == null)
					{
						value = default(FreezePaneToMultipleBlanks);
						return false;
					}
					value = freezePaneToMultipleBlanks.Value;
					return true;
				}

				// Token: 0x06005D4F RID: 23887 RVA: 0x00136508 File Offset: 0x00134708
				public bool TrimTopMergedCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopMergedCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D50 RID: 23888 RVA: 0x0013652C File Offset: 0x0013472C
				public bool TrimTopMergedCellRows(ProgramNode node, out TrimTopMergedCellRows value)
				{
					TrimTopMergedCellRows? trimTopMergedCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopMergedCellRows.CreateSafe(this._builders, node);
					if (trimTopMergedCellRows == null)
					{
						value = default(TrimTopMergedCellRows);
						return false;
					}
					value = trimTopMergedCellRows.Value;
					return true;
				}

				// Token: 0x06005D51 RID: 23889 RVA: 0x00136568 File Offset: 0x00134768
				public bool TrimTopFullWidthMergedCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopFullWidthMergedCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D52 RID: 23890 RVA: 0x0013658C File Offset: 0x0013478C
				public bool TrimTopFullWidthMergedCellRows(ProgramNode node, out TrimTopFullWidthMergedCellRows value)
				{
					TrimTopFullWidthMergedCellRows? trimTopFullWidthMergedCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopFullWidthMergedCellRows.CreateSafe(this._builders, node);
					if (trimTopFullWidthMergedCellRows == null)
					{
						value = default(TrimTopFullWidthMergedCellRows);
						return false;
					}
					value = trimTopFullWidthMergedCellRows.Value;
					return true;
				}

				// Token: 0x06005D53 RID: 23891 RVA: 0x001365C8 File Offset: 0x001347C8
				public bool TrimTopSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopSingleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D54 RID: 23892 RVA: 0x001365EC File Offset: 0x001347EC
				public bool TrimTopSingleCellRows(ProgramNode node, out TrimTopSingleCellRows value)
				{
					TrimTopSingleCellRows? trimTopSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopSingleCellRows.CreateSafe(this._builders, node);
					if (trimTopSingleCellRows == null)
					{
						value = default(TrimTopSingleCellRows);
						return false;
					}
					value = trimTopSingleCellRows.Value;
					return true;
				}

				// Token: 0x06005D55 RID: 23893 RVA: 0x00136628 File Offset: 0x00134828
				public bool TrimBelowTopBorder(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBelowTopBorder.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D56 RID: 23894 RVA: 0x0013664C File Offset: 0x0013484C
				public bool TrimBelowTopBorder(ProgramNode node, out TrimBelowTopBorder value)
				{
					TrimBelowTopBorder? trimBelowTopBorder = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBelowTopBorder.CreateSafe(this._builders, node);
					if (trimBelowTopBorder == null)
					{
						value = default(TrimBelowTopBorder);
						return false;
					}
					value = trimBelowTopBorder.Value;
					return true;
				}

				// Token: 0x06005D57 RID: 23895 RVA: 0x00136688 File Offset: 0x00134888
				public bool TakeAfterEmptyRow(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeAfterEmptyRow.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D58 RID: 23896 RVA: 0x001366AC File Offset: 0x001348AC
				public bool TakeAfterEmptyRow(ProgramNode node, out TakeAfterEmptyRow value)
				{
					TakeAfterEmptyRow? takeAfterEmptyRow = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeAfterEmptyRow.CreateSafe(this._builders, node);
					if (takeAfterEmptyRow == null)
					{
						value = default(TakeAfterEmptyRow);
						return false;
					}
					value = takeAfterEmptyRow.Value;
					return true;
				}

				// Token: 0x06005D59 RID: 23897 RVA: 0x001366E8 File Offset: 0x001348E8
				public bool sheetSection_horizontalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.sheetSection_horizontalSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D5A RID: 23898 RVA: 0x0013670C File Offset: 0x0013490C
				public bool sheetSection_horizontalSheetSection(ProgramNode node, out sheetSection_horizontalSheetSection value)
				{
					sheetSection_horizontalSheetSection? sheetSection_horizontalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.sheetSection_horizontalSheetSection.CreateSafe(this._builders, node);
					if (sheetSection_horizontalSheetSection == null)
					{
						value = default(sheetSection_horizontalSheetSection);
						return false;
					}
					value = sheetSection_horizontalSheetSection.Value;
					return true;
				}

				// Token: 0x06005D5B RID: 23899 RVA: 0x00136748 File Offset: 0x00134948
				public bool TakeUntilEmptyColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D5C RID: 23900 RVA: 0x0013676C File Offset: 0x0013496C
				public bool TakeUntilEmptyColumn(ProgramNode node, out TakeUntilEmptyColumn value)
				{
					TakeUntilEmptyColumn? takeUntilEmptyColumn = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyColumn.CreateSafe(this._builders, node);
					if (takeUntilEmptyColumn == null)
					{
						value = default(TakeUntilEmptyColumn);
						return false;
					}
					value = takeUntilEmptyColumn.Value;
					return true;
				}

				// Token: 0x06005D5D RID: 23901 RVA: 0x001367A8 File Offset: 0x001349A8
				public bool TrimRightSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimRightSingleCellColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D5E RID: 23902 RVA: 0x001367CC File Offset: 0x001349CC
				public bool TrimRightSingleCellColumns(ProgramNode node, out TrimRightSingleCellColumns value)
				{
					TrimRightSingleCellColumns? trimRightSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimRightSingleCellColumns.CreateSafe(this._builders, node);
					if (trimRightSingleCellColumns == null)
					{
						value = default(TrimRightSingleCellColumns);
						return false;
					}
					value = trimRightSingleCellColumns.Value;
					return true;
				}

				// Token: 0x06005D5F RID: 23903 RVA: 0x00136808 File Offset: 0x00134A08
				public bool horizontalSheetSection_verticalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.horizontalSheetSection_verticalSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D60 RID: 23904 RVA: 0x0013682C File Offset: 0x00134A2C
				public bool horizontalSheetSection_verticalSheetSection(ProgramNode node, out horizontalSheetSection_verticalSheetSection value)
				{
					horizontalSheetSection_verticalSheetSection? horizontalSheetSection_verticalSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.horizontalSheetSection_verticalSheetSection.CreateSafe(this._builders, node);
					if (horizontalSheetSection_verticalSheetSection == null)
					{
						value = default(horizontalSheetSection_verticalSheetSection);
						return false;
					}
					value = horizontalSheetSection_verticalSheetSection.Value;
					return true;
				}

				// Token: 0x06005D61 RID: 23905 RVA: 0x00136868 File Offset: 0x00134A68
				public bool KthHorizontal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthHorizontal.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D62 RID: 23906 RVA: 0x0013688C File Offset: 0x00134A8C
				public bool KthHorizontal(ProgramNode node, out KthHorizontal value)
				{
					KthHorizontal? kthHorizontal = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthHorizontal.CreateSafe(this._builders, node);
					if (kthHorizontal == null)
					{
						value = default(KthHorizontal);
						return false;
					}
					value = kthHorizontal.Value;
					return true;
				}

				// Token: 0x06005D63 RID: 23907 RVA: 0x001368C8 File Offset: 0x00134AC8
				public bool verticalSheetSection_uncleanedSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.verticalSheetSection_uncleanedSheetSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D64 RID: 23908 RVA: 0x001368EC File Offset: 0x00134AEC
				public bool verticalSheetSection_uncleanedSheetSection(ProgramNode node, out verticalSheetSection_uncleanedSheetSection value)
				{
					verticalSheetSection_uncleanedSheetSection? verticalSheetSection_uncleanedSheetSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.verticalSheetSection_uncleanedSheetSection.CreateSafe(this._builders, node);
					if (verticalSheetSection_uncleanedSheetSection == null)
					{
						value = default(verticalSheetSection_uncleanedSheetSection);
						return false;
					}
					value = verticalSheetSection_uncleanedSheetSection.Value;
					return true;
				}

				// Token: 0x06005D65 RID: 23909 RVA: 0x00136928 File Offset: 0x00134B28
				public bool KthVertical(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthVertical.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D66 RID: 23910 RVA: 0x0013694C File Offset: 0x00134B4C
				public bool KthVertical(ProgramNode node, out KthVertical value)
				{
					KthVertical? kthVertical = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthVertical.CreateSafe(this._builders, node);
					if (kthVertical == null)
					{
						value = default(KthVertical);
						return false;
					}
					value = kthVertical.Value;
					return true;
				}

				// Token: 0x06005D67 RID: 23911 RVA: 0x00136988 File Offset: 0x00134B88
				public bool uncleanedSheetSection_wholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.uncleanedSheetSection_wholeSheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D68 RID: 23912 RVA: 0x001369AC File Offset: 0x00134BAC
				public bool uncleanedSheetSection_wholeSheet(ProgramNode node, out uncleanedSheetSection_wholeSheet value)
				{
					uncleanedSheetSection_wholeSheet? uncleanedSheetSection_wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.uncleanedSheetSection_wholeSheet.CreateSafe(this._builders, node);
					if (uncleanedSheetSection_wholeSheet == null)
					{
						value = default(uncleanedSheetSection_wholeSheet);
						return false;
					}
					value = uncleanedSheetSection_wholeSheet.Value;
					return true;
				}

				// Token: 0x06005D69 RID: 23913 RVA: 0x001369E8 File Offset: 0x00134BE8
				public bool KthSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D6A RID: 23914 RVA: 0x00136A0C File Offset: 0x00134C0C
				public bool KthSplit(ProgramNode node, out KthSplit value)
				{
					KthSplit? kthSplit = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthSplit.CreateSafe(this._builders, node);
					if (kthSplit == null)
					{
						value = default(KthSplit);
						return false;
					}
					value = kthSplit.Value;
					return true;
				}

				// Token: 0x06005D6B RID: 23915 RVA: 0x00136A48 File Offset: 0x00134C48
				public bool Area(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Area.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D6C RID: 23916 RVA: 0x00136A6C File Offset: 0x00134C6C
				public bool Area(ProgramNode node, out Area value)
				{
					Area? area = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Area.CreateSafe(this._builders, node);
					if (area == null)
					{
						value = default(Area);
						return false;
					}
					value = area.Value;
					return true;
				}

				// Token: 0x06005D6D RID: 23917 RVA: 0x00136AA8 File Offset: 0x00134CA8
				public bool wholeSheet_wholeSheetFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.wholeSheet_wholeSheetFull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D6E RID: 23918 RVA: 0x00136ACC File Offset: 0x00134CCC
				public bool wholeSheet_wholeSheetFull(ProgramNode node, out wholeSheet_wholeSheetFull value)
				{
					wholeSheet_wholeSheetFull? wholeSheet_wholeSheetFull = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.wholeSheet_wholeSheetFull.CreateSafe(this._builders, node);
					if (wholeSheet_wholeSheetFull == null)
					{
						value = default(wholeSheet_wholeSheetFull);
						return false;
					}
					value = wholeSheet_wholeSheetFull.Value;
					return true;
				}

				// Token: 0x06005D6F RID: 23919 RVA: 0x00136B08 File Offset: 0x00134D08
				public bool TrimHiddenWholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHiddenWholeSheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D70 RID: 23920 RVA: 0x00136B2C File Offset: 0x00134D2C
				public bool TrimHiddenWholeSheet(ProgramNode node, out TrimHiddenWholeSheet value)
				{
					TrimHiddenWholeSheet? trimHiddenWholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHiddenWholeSheet.CreateSafe(this._builders, node);
					if (trimHiddenWholeSheet == null)
					{
						value = default(TrimHiddenWholeSheet);
						return false;
					}
					value = trimHiddenWholeSheet.Value;
					return true;
				}

				// Token: 0x06005D71 RID: 23921 RVA: 0x00136B68 File Offset: 0x00134D68
				public bool WholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WholeSheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D72 RID: 23922 RVA: 0x00136B8C File Offset: 0x00134D8C
				public bool WholeSheet(ProgramNode node, out WholeSheet value)
				{
					WholeSheet? wholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WholeSheet.CreateSafe(this._builders, node);
					if (wholeSheet == null)
					{
						value = default(WholeSheet);
						return false;
					}
					value = wholeSheet.Value;
					return true;
				}

				// Token: 0x06005D73 RID: 23923 RVA: 0x00136BC8 File Offset: 0x00134DC8
				public bool WithFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithFormatting.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D74 RID: 23924 RVA: 0x00136BEC File Offset: 0x00134DEC
				public bool WithFormatting(ProgramNode node, out WithFormatting value)
				{
					WithFormatting? withFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithFormatting.CreateSafe(this._builders, node);
					if (withFormatting == null)
					{
						value = default(WithFormatting);
						return false;
					}
					value = withFormatting.Value;
					return true;
				}

				// Token: 0x06005D75 RID: 23925 RVA: 0x00136C28 File Offset: 0x00134E28
				public bool SplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D76 RID: 23926 RVA: 0x00136C4C File Offset: 0x00134E4C
				public bool SplitOnEmptyRows(ProgramNode node, out SplitOnEmptyRows value)
				{
					SplitOnEmptyRows? splitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyRows.CreateSafe(this._builders, node);
					if (splitOnEmptyRows == null)
					{
						value = default(SplitOnEmptyRows);
						return false;
					}
					value = splitOnEmptyRows.Value;
					return true;
				}

				// Token: 0x06005D77 RID: 23927 RVA: 0x00136C88 File Offset: 0x00134E88
				public bool SplitOnMatchingRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnMatchingRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D78 RID: 23928 RVA: 0x00136CAC File Offset: 0x00134EAC
				public bool SplitOnMatchingRows(ProgramNode node, out SplitOnMatchingRows value)
				{
					SplitOnMatchingRows? splitOnMatchingRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnMatchingRows.CreateSafe(this._builders, node);
					if (splitOnMatchingRows == null)
					{
						value = default(SplitOnMatchingRows);
						return false;
					}
					value = splitOnMatchingRows.Value;
					return true;
				}

				// Token: 0x06005D79 RID: 23929 RVA: 0x00136CE8 File Offset: 0x00134EE8
				public bool SplitOnEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D7A RID: 23930 RVA: 0x00136D0C File Offset: 0x00134F0C
				public bool SplitOnEmptyColumns(ProgramNode node, out SplitOnEmptyColumns value)
				{
					SplitOnEmptyColumns? splitOnEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyColumns.CreateSafe(this._builders, node);
					if (splitOnEmptyColumns == null)
					{
						value = default(SplitOnEmptyColumns);
						return false;
					}
					value = splitOnEmptyColumns.Value;
					return true;
				}

				// Token: 0x06005D7B RID: 23931 RVA: 0x00136D48 File Offset: 0x00134F48
				public bool BorderedAreas(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BorderedAreas.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D7C RID: 23932 RVA: 0x00136D6C File Offset: 0x00134F6C
				public bool BorderedAreas(ProgramNode node, out BorderedAreas value)
				{
					BorderedAreas? borderedAreas = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BorderedAreas.CreateSafe(this._builders, node);
					if (borderedAreas == null)
					{
						value = default(BorderedAreas);
						return false;
					}
					value = borderedAreas.Value;
					return true;
				}

				// Token: 0x06005D7D RID: 23933 RVA: 0x00136DA8 File Offset: 0x00134FA8
				public bool mProgram_mTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.mProgram_mTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D7E RID: 23934 RVA: 0x00136DCC File Offset: 0x00134FCC
				public bool mProgram_mTable(ProgramNode node, out mProgram_mTable value)
				{
					mProgram_mTable? mProgram_mTable = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.mProgram_mTable.CreateSafe(this._builders, node);
					if (mProgram_mTable == null)
					{
						value = default(mProgram_mTable);
						return false;
					}
					value = mProgram_mTable.Value;
					return true;
				}

				// Token: 0x06005D7F RID: 23935 RVA: 0x00136E08 File Offset: 0x00135008
				public bool RemoveEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D80 RID: 23936 RVA: 0x00136E2C File Offset: 0x0013502C
				public bool RemoveEmptyRows(ProgramNode node, out RemoveEmptyRows value)
				{
					RemoveEmptyRows? removeEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyRows.CreateSafe(this._builders, node);
					if (removeEmptyRows == null)
					{
						value = default(RemoveEmptyRows);
						return false;
					}
					value = removeEmptyRows.Value;
					return true;
				}

				// Token: 0x06005D81 RID: 23937 RVA: 0x00136E68 File Offset: 0x00135068
				public bool RemoveEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D82 RID: 23938 RVA: 0x00136E8C File Offset: 0x0013508C
				public bool RemoveEmptyColumns(ProgramNode node, out RemoveEmptyColumns value)
				{
					RemoveEmptyColumns? removeEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyColumns.CreateSafe(this._builders, node);
					if (removeEmptyColumns == null)
					{
						value = default(RemoveEmptyColumns);
						return false;
					}
					value = removeEmptyColumns.Value;
					return true;
				}

				// Token: 0x06005D83 RID: 23939 RVA: 0x00136EC8 File Offset: 0x001350C8
				public bool MWholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MWholeSheet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D84 RID: 23940 RVA: 0x00136EEC File Offset: 0x001350EC
				public bool MWholeSheet(ProgramNode node, out MWholeSheet value)
				{
					MWholeSheet? mwholeSheet = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MWholeSheet.CreateSafe(this._builders, node);
					if (mwholeSheet == null)
					{
						value = default(MWholeSheet);
						return false;
					}
					value = mwholeSheet.Value;
					return true;
				}

				// Token: 0x06005D85 RID: 23941 RVA: 0x00136F28 File Offset: 0x00135128
				public bool KthMSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthMSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D86 RID: 23942 RVA: 0x00136F4C File Offset: 0x0013514C
				public bool KthMSection(ProgramNode node, out KthMSection value)
				{
					KthMSection? kthMSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthMSection.CreateSafe(this._builders, node);
					if (kthMSection == null)
					{
						value = default(KthMSection);
						return false;
					}
					value = kthMSection.Value;
					return true;
				}

				// Token: 0x06005D87 RID: 23943 RVA: 0x00136F88 File Offset: 0x00135188
				public bool KthAndNextMSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthAndNextMSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D88 RID: 23944 RVA: 0x00136FAC File Offset: 0x001351AC
				public bool KthAndNextMSection(ProgramNode node, out KthAndNextMSection value)
				{
					KthAndNextMSection? kthAndNextMSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthAndNextMSection.CreateSafe(this._builders, node);
					if (kthAndNextMSection == null)
					{
						value = default(KthAndNextMSection);
						return false;
					}
					value = kthAndNextMSection.Value;
					return true;
				}

				// Token: 0x06005D89 RID: 23945 RVA: 0x00136FE8 File Offset: 0x001351E8
				public bool MTrimTopSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D8A RID: 23946 RVA: 0x0013700C File Offset: 0x0013520C
				public bool MTrimTopSingleCellRows(ProgramNode node, out MTrimTopSingleCellRows value)
				{
					MTrimTopSingleCellRows? mtrimTopSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleCellRows.CreateSafe(this._builders, node);
					if (mtrimTopSingleCellRows == null)
					{
						value = default(MTrimTopSingleCellRows);
						return false;
					}
					value = mtrimTopSingleCellRows.Value;
					return true;
				}

				// Token: 0x06005D8B RID: 23947 RVA: 0x00137048 File Offset: 0x00135248
				public bool MTrimTopSingleLeftCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleLeftCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D8C RID: 23948 RVA: 0x0013706C File Offset: 0x0013526C
				public bool MTrimTopSingleLeftCellRows(ProgramNode node, out MTrimTopSingleLeftCellRows value)
				{
					MTrimTopSingleLeftCellRows? mtrimTopSingleLeftCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleLeftCellRows.CreateSafe(this._builders, node);
					if (mtrimTopSingleLeftCellRows == null)
					{
						value = default(MTrimTopSingleLeftCellRows);
						return false;
					}
					value = mtrimTopSingleLeftCellRows.Value;
					return true;
				}

				// Token: 0x06005D8D RID: 23949 RVA: 0x001370A8 File Offset: 0x001352A8
				public bool MTrimBottomSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomSingleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D8E RID: 23950 RVA: 0x001370CC File Offset: 0x001352CC
				public bool MTrimBottomSingleCellRows(ProgramNode node, out MTrimBottomSingleCellRows value)
				{
					MTrimBottomSingleCellRows? mtrimBottomSingleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomSingleCellRows.CreateSafe(this._builders, node);
					if (mtrimBottomSingleCellRows == null)
					{
						value = default(MTrimBottomSingleCellRows);
						return false;
					}
					value = mtrimBottomSingleCellRows.Value;
					return true;
				}

				// Token: 0x06005D8F RID: 23951 RVA: 0x00137108 File Offset: 0x00135308
				public bool MTrimLeftSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimLeftSingleCellColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D90 RID: 23952 RVA: 0x0013712C File Offset: 0x0013532C
				public bool MTrimLeftSingleCellColumns(ProgramNode node, out MTrimLeftSingleCellColumns value)
				{
					MTrimLeftSingleCellColumns? mtrimLeftSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimLeftSingleCellColumns.CreateSafe(this._builders, node);
					if (mtrimLeftSingleCellColumns == null)
					{
						value = default(MTrimLeftSingleCellColumns);
						return false;
					}
					value = mtrimLeftSingleCellColumns.Value;
					return true;
				}

				// Token: 0x06005D91 RID: 23953 RVA: 0x00137168 File Offset: 0x00135368
				public bool MTrimRightSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimRightSingleCellColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D92 RID: 23954 RVA: 0x0013718C File Offset: 0x0013538C
				public bool MTrimRightSingleCellColumns(ProgramNode node, out MTrimRightSingleCellColumns value)
				{
					MTrimRightSingleCellColumns? mtrimRightSingleCellColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimRightSingleCellColumns.CreateSafe(this._builders, node);
					if (mtrimRightSingleCellColumns == null)
					{
						value = default(MTrimRightSingleCellColumns);
						return false;
					}
					value = mtrimRightSingleCellColumns.Value;
					return true;
				}

				// Token: 0x06005D93 RID: 23955 RVA: 0x001371C8 File Offset: 0x001353C8
				public bool MTrimTopDoubleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopDoubleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D94 RID: 23956 RVA: 0x001371EC File Offset: 0x001353EC
				public bool MTrimTopDoubleCellRows(ProgramNode node, out MTrimTopDoubleCellRows value)
				{
					MTrimTopDoubleCellRows? mtrimTopDoubleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopDoubleCellRows.CreateSafe(this._builders, node);
					if (mtrimTopDoubleCellRows == null)
					{
						value = default(MTrimTopDoubleCellRows);
						return false;
					}
					value = mtrimTopDoubleCellRows.Value;
					return true;
				}

				// Token: 0x06005D95 RID: 23957 RVA: 0x00137228 File Offset: 0x00135428
				public bool MTrimBottomDoubleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomDoubleCellRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D96 RID: 23958 RVA: 0x0013724C File Offset: 0x0013544C
				public bool MTrimBottomDoubleCellRows(ProgramNode node, out MTrimBottomDoubleCellRows value)
				{
					MTrimBottomDoubleCellRows? mtrimBottomDoubleCellRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomDoubleCellRows.CreateSafe(this._builders, node);
					if (mtrimBottomDoubleCellRows == null)
					{
						value = default(MTrimBottomDoubleCellRows);
						return false;
					}
					value = mtrimBottomDoubleCellRows.Value;
					return true;
				}

				// Token: 0x06005D97 RID: 23959 RVA: 0x00137288 File Offset: 0x00135488
				public bool MSplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D98 RID: 23960 RVA: 0x001372AC File Offset: 0x001354AC
				public bool MSplitOnEmptyRows(ProgramNode node, out MSplitOnEmptyRows value)
				{
					MSplitOnEmptyRows? msplitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyRows.CreateSafe(this._builders, node);
					if (msplitOnEmptyRows == null)
					{
						value = default(MSplitOnEmptyRows);
						return false;
					}
					value = msplitOnEmptyRows.Value;
					return true;
				}

				// Token: 0x06005D99 RID: 23961 RVA: 0x001372E8 File Offset: 0x001354E8
				public bool MSplitOnEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D9A RID: 23962 RVA: 0x0013730C File Offset: 0x0013550C
				public bool MSplitOnEmptyColumns(ProgramNode node, out MSplitOnEmptyColumns value)
				{
					MSplitOnEmptyColumns? msplitOnEmptyColumns = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyColumns.CreateSafe(this._builders, node);
					if (msplitOnEmptyColumns == null)
					{
						value = default(MSplitOnEmptyColumns);
						return false;
					}
					value = msplitOnEmptyColumns.Value;
					return true;
				}

				// Token: 0x06005D9B RID: 23963 RVA: 0x00137348 File Offset: 0x00135548
				public bool WithoutFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithoutFormatting.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D9C RID: 23964 RVA: 0x0013736C File Offset: 0x0013556C
				public bool WithoutFormatting(ProgramNode node, out WithoutFormatting value)
				{
					WithoutFormatting? withoutFormatting = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithoutFormatting.CreateSafe(this._builders, node);
					if (withoutFormatting == null)
					{
						value = default(WithoutFormatting);
						return false;
					}
					value = withoutFormatting.Value;
					return true;
				}

				// Token: 0x06005D9D RID: 23965 RVA: 0x001373A8 File Offset: 0x001355A8
				public bool StartTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.StartTitle.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005D9E RID: 23966 RVA: 0x001373CC File Offset: 0x001355CC
				public bool StartTitle(ProgramNode node, out StartTitle value)
				{
					StartTitle? startTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.StartTitle.CreateSafe(this._builders, node);
					if (startTitle == null)
					{
						value = default(StartTitle);
						return false;
					}
					value = startTitle.Value;
					return true;
				}

				// Token: 0x06005D9F RID: 23967 RVA: 0x00137408 File Offset: 0x00135608
				public bool title_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.title_above.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DA0 RID: 23968 RVA: 0x0013742C File Offset: 0x0013562C
				public bool title_above(ProgramNode node, out title_above value)
				{
					title_above? title_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.title_above.CreateSafe(this._builders, node);
					if (title_above == null)
					{
						value = default(title_above);
						return false;
					}
					value = title_above.Value;
					return true;
				}

				// Token: 0x06005DA1 RID: 23969 RVA: 0x00137468 File Offset: 0x00135668
				public bool TopLeftCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopLeftCell.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DA2 RID: 23970 RVA: 0x0013748C File Offset: 0x0013568C
				public bool TopLeftCell(ProgramNode node, out TopLeftCell value)
				{
					TopLeftCell? topLeftCell = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopLeftCell.CreateSafe(this._builders, node);
					if (topLeftCell == null)
					{
						value = default(TopLeftCell);
						return false;
					}
					value = topLeftCell.Value;
					return true;
				}

				// Token: 0x06005DA3 RID: 23971 RVA: 0x001374C8 File Offset: 0x001356C8
				public bool TopSameFontCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopSameFontCells.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DA4 RID: 23972 RVA: 0x001374EC File Offset: 0x001356EC
				public bool TopSameFontCells(ProgramNode node, out TopSameFontCells value)
				{
					TopSameFontCells? topSameFontCells = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopSameFontCells.CreateSafe(this._builders, node);
					if (topSameFontCells == null)
					{
						value = default(TopSameFontCells);
						return false;
					}
					value = topSameFontCells.Value;
					return true;
				}

				// Token: 0x06005DA5 RID: 23973 RVA: 0x00137528 File Offset: 0x00135728
				public bool BottomLeftSameFontCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BottomLeftSameFontCells.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DA6 RID: 23974 RVA: 0x0013754C File Offset: 0x0013574C
				public bool BottomLeftSameFontCells(ProgramNode node, out BottomLeftSameFontCells value)
				{
					BottomLeftSameFontCells? bottomLeftSameFontCells = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BottomLeftSameFontCells.CreateSafe(this._builders, node);
					if (bottomLeftSameFontCells == null)
					{
						value = default(BottomLeftSameFontCells);
						return false;
					}
					value = bottomLeftSameFontCells.Value;
					return true;
				}

				// Token: 0x06005DA7 RID: 23975 RVA: 0x00137588 File Offset: 0x00135788
				public bool aboveOrLeftmost_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrLeftmost_above.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DA8 RID: 23976 RVA: 0x001375AC File Offset: 0x001357AC
				public bool aboveOrLeftmost_above(ProgramNode node, out aboveOrLeftmost_above value)
				{
					aboveOrLeftmost_above? aboveOrLeftmost_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrLeftmost_above.CreateSafe(this._builders, node);
					if (aboveOrLeftmost_above == null)
					{
						value = default(aboveOrLeftmost_above);
						return false;
					}
					value = aboveOrLeftmost_above.Value;
					return true;
				}

				// Token: 0x06005DA9 RID: 23977 RVA: 0x001375E8 File Offset: 0x001357E8
				public bool LeftmostColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftmostColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DAA RID: 23978 RVA: 0x0013760C File Offset: 0x0013580C
				public bool LeftmostColumn(ProgramNode node, out LeftmostColumn value)
				{
					LeftmostColumn? leftmostColumn = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftmostColumn.CreateSafe(this._builders, node);
					if (leftmostColumn == null)
					{
						value = default(LeftmostColumn);
						return false;
					}
					value = leftmostColumn.Value;
					return true;
				}

				// Token: 0x06005DAB RID: 23979 RVA: 0x00137648 File Offset: 0x00135848
				public bool LeftOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DAC RID: 23980 RVA: 0x0013766C File Offset: 0x0013586C
				public bool LeftOf(ProgramNode node, out LeftOf value)
				{
					LeftOf? leftOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftOf.CreateSafe(this._builders, node);
					if (leftOf == null)
					{
						value = default(LeftOf);
						return false;
					}
					value = leftOf.Value;
					return true;
				}

				// Token: 0x06005DAD RID: 23981 RVA: 0x001376A8 File Offset: 0x001358A8
				public bool aboveOrOutput_aboveOrHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_aboveOrHeader.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DAE RID: 23982 RVA: 0x001376CC File Offset: 0x001358CC
				public bool aboveOrOutput_aboveOrHeader(ProgramNode node, out aboveOrOutput_aboveOrHeader value)
				{
					aboveOrOutput_aboveOrHeader? aboveOrOutput_aboveOrHeader = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_aboveOrHeader.CreateSafe(this._builders, node);
					if (aboveOrOutput_aboveOrHeader == null)
					{
						value = default(aboveOrOutput_aboveOrHeader);
						return false;
					}
					value = aboveOrOutput_aboveOrHeader.Value;
					return true;
				}

				// Token: 0x06005DAF RID: 23983 RVA: 0x00137708 File Offset: 0x00135908
				public bool aboveOrOutput_titleOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_titleOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DB0 RID: 23984 RVA: 0x0013772C File Offset: 0x0013592C
				public bool aboveOrOutput_titleOf(ProgramNode node, out aboveOrOutput_titleOf value)
				{
					aboveOrOutput_titleOf? aboveOrOutput_titleOf = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_titleOf.CreateSafe(this._builders, node);
					if (aboveOrOutput_titleOf == null)
					{
						value = default(aboveOrOutput_titleOf);
						return false;
					}
					value = aboveOrOutput_titleOf.Value;
					return true;
				}

				// Token: 0x06005DB1 RID: 23985 RVA: 0x00137768 File Offset: 0x00135968
				public bool aboveOrHeader_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_above.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DB2 RID: 23986 RVA: 0x0013778C File Offset: 0x0013598C
				public bool aboveOrHeader_above(ProgramNode node, out aboveOrHeader_above value)
				{
					aboveOrHeader_above? aboveOrHeader_above = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_above.CreateSafe(this._builders, node);
					if (aboveOrHeader_above == null)
					{
						value = default(aboveOrHeader_above);
						return false;
					}
					value = aboveOrHeader_above.Value;
					return true;
				}

				// Token: 0x06005DB3 RID: 23987 RVA: 0x001377C8 File Offset: 0x001359C8
				public bool aboveOrHeader_headerSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_headerSection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DB4 RID: 23988 RVA: 0x001377EC File Offset: 0x001359EC
				public bool aboveOrHeader_headerSection(ProgramNode node, out aboveOrHeader_headerSection value)
				{
					aboveOrHeader_headerSection? aboveOrHeader_headerSection = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_headerSection.CreateSafe(this._builders, node);
					if (aboveOrHeader_headerSection == null)
					{
						value = default(aboveOrHeader_headerSection);
						return false;
					}
					value = aboveOrHeader_headerSection.Value;
					return true;
				}

				// Token: 0x06005DB5 RID: 23989 RVA: 0x00137828 File Offset: 0x00135A28
				public bool FirstSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FirstSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DB6 RID: 23990 RVA: 0x0013784C File Offset: 0x00135A4C
				public bool FirstSplit(ProgramNode node, out FirstSplit value)
				{
					FirstSplit? firstSplit = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FirstSplit.CreateSafe(this._builders, node);
					if (firstSplit == null)
					{
						value = default(FirstSplit);
						return false;
					}
					value = firstSplit.Value;
					return true;
				}

				// Token: 0x06005DB7 RID: 23991 RVA: 0x00137888 File Offset: 0x00135A88
				public bool TitleSplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnEmptyRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DB8 RID: 23992 RVA: 0x001378AC File Offset: 0x00135AAC
				public bool TitleSplitOnEmptyRows(ProgramNode node, out TitleSplitOnEmptyRows value)
				{
					TitleSplitOnEmptyRows? titleSplitOnEmptyRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnEmptyRows.CreateSafe(this._builders, node);
					if (titleSplitOnEmptyRows == null)
					{
						value = default(TitleSplitOnEmptyRows);
						return false;
					}
					value = titleSplitOnEmptyRows.Value;
					return true;
				}

				// Token: 0x06005DB9 RID: 23993 RVA: 0x001378E8 File Offset: 0x00135AE8
				public bool TitleSplitOnMatchingRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnMatchingRows.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DBA RID: 23994 RVA: 0x0013790C File Offset: 0x00135B0C
				public bool TitleSplitOnMatchingRows(ProgramNode node, out TitleSplitOnMatchingRows value)
				{
					TitleSplitOnMatchingRows? titleSplitOnMatchingRows = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnMatchingRows.CreateSafe(this._builders, node);
					if (titleSplitOnMatchingRows == null)
					{
						value = default(TitleSplitOnMatchingRows);
						return false;
					}
					value = titleSplitOnMatchingRows.Value;
					return true;
				}

				// Token: 0x06005DBB RID: 23995 RVA: 0x00137948 File Offset: 0x00135B48
				public bool TitleCellsAbove(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAbove.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DBC RID: 23996 RVA: 0x0013796C File Offset: 0x00135B6C
				public bool TitleCellsAbove(ProgramNode node, out TitleCellsAbove value)
				{
					TitleCellsAbove? titleCellsAbove = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAbove.CreateSafe(this._builders, node);
					if (titleCellsAbove == null)
					{
						value = default(TitleCellsAbove);
						return false;
					}
					value = titleCellsAbove.Value;
					return true;
				}

				// Token: 0x06005DBD RID: 23997 RVA: 0x001379A8 File Offset: 0x00135BA8
				public bool TitleCellsAboveMatching(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAboveMatching.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DBE RID: 23998 RVA: 0x001379CC File Offset: 0x00135BCC
				public bool TitleCellsAboveMatching(ProgramNode node, out TitleCellsAboveMatching value)
				{
					TitleCellsAboveMatching? titleCellsAboveMatching = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAboveMatching.CreateSafe(this._builders, node);
					if (titleCellsAboveMatching == null)
					{
						value = default(TitleCellsAboveMatching);
						return false;
					}
					value = titleCellsAboveMatching.Value;
					return true;
				}

				// Token: 0x06005DBF RID: 23999 RVA: 0x00137A08 File Offset: 0x00135C08
				public bool WrapOutputForTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WrapOutputForTitle.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DC0 RID: 24000 RVA: 0x00137A2C File Offset: 0x00135C2C
				public bool WrapOutputForTitle(ProgramNode node, out WrapOutputForTitle value)
				{
					WrapOutputForTitle? wrapOutputForTitle = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WrapOutputForTitle.CreateSafe(this._builders, node);
					if (wrapOutputForTitle == null)
					{
						value = default(WrapOutputForTitle);
						return false;
					}
					value = wrapOutputForTitle.Value;
					return true;
				}

				// Token: 0x06005DC1 RID: 24001 RVA: 0x00137A68 File Offset: 0x00135C68
				public bool IncludeEmptyToLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.IncludeEmptyToLeft.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06005DC2 RID: 24002 RVA: 0x00137A8C File Offset: 0x00135C8C
				public bool IncludeEmptyToLeft(ProgramNode node, out IncludeEmptyToLeft value)
				{
					IncludeEmptyToLeft? includeEmptyToLeft = Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.IncludeEmptyToLeft.CreateSafe(this._builders, node);
					if (includeEmptyToLeft == null)
					{
						value = default(IncludeEmptyToLeft);
						return false;
					}
					value = includeEmptyToLeft.Value;
					return true;
				}

				// Token: 0x04002B51 RID: 11089
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DFA RID: 3578
			public class NodeAs
			{
				// Token: 0x06005DC3 RID: 24003 RVA: 0x00137AC6 File Offset: 0x00135CC6
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005DC4 RID: 24004 RVA: 0x00137AD5 File Offset: 0x00135CD5
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DC5 RID: 24005 RVA: 0x00137AE3 File Offset: 0x00135CE3
				public trim? trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DC6 RID: 24006 RVA: 0x00137AF1 File Offset: 0x00135CF1
				public area? area(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DC7 RID: 24007 RVA: 0x00137AFF File Offset: 0x00135CFF
				public trimLeft? trimLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DC8 RID: 24008 RVA: 0x00137B0D File Offset: 0x00135D0D
				public trimBottom? trimBottom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DC9 RID: 24009 RVA: 0x00137B1B File Offset: 0x00135D1B
				public trimTop? trimTop(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCA RID: 24010 RVA: 0x00137B29 File Offset: 0x00135D29
				public sheetSection? sheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCB RID: 24011 RVA: 0x00137B37 File Offset: 0x00135D37
				public horizontalSheetSection? horizontalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCC RID: 24012 RVA: 0x00137B45 File Offset: 0x00135D45
				public verticalSheetSection? verticalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCD RID: 24013 RVA: 0x00137B53 File Offset: 0x00135D53
				public uncleanedSheetSection? uncleanedSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCE RID: 24014 RVA: 0x00137B61 File Offset: 0x00135D61
				public wholeSheet? wholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DCF RID: 24015 RVA: 0x00137B6F File Offset: 0x00135D6F
				public wholeSheetFull? wholeSheetFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD0 RID: 24016 RVA: 0x00137B7D File Offset: 0x00135D7D
				public sheet? sheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD1 RID: 24017 RVA: 0x00137B8B File Offset: 0x00135D8B
				public horizontalSheetSplits? horizontalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD2 RID: 24018 RVA: 0x00137B99 File Offset: 0x00135D99
				public verticalSheetSplits? verticalSheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD3 RID: 24019 RVA: 0x00137BA7 File Offset: 0x00135DA7
				public sheetSplits? sheetSplits(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD4 RID: 24020 RVA: 0x00137BB5 File Offset: 0x00135DB5
				public index? index(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD5 RID: 24021 RVA: 0x00137BC3 File Offset: 0x00135DC3
				public rangeName? rangeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD6 RID: 24022 RVA: 0x00137BD1 File Offset: 0x00135DD1
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD7 RID: 24023 RVA: 0x00137BDF File Offset: 0x00135DDF
				public splitMode? splitMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD8 RID: 24024 RVA: 0x00137BED File Offset: 0x00135DED
				public styleFilter? styleFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DD9 RID: 24025 RVA: 0x00137BFB File Offset: 0x00135DFB
				public mProgram? mProgram(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDA RID: 24026 RVA: 0x00137C09 File Offset: 0x00135E09
				public mTable? mTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDB RID: 24027 RVA: 0x00137C17 File Offset: 0x00135E17
				public mSection? mSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDC RID: 24028 RVA: 0x00137C25 File Offset: 0x00135E25
				public withoutFormatting? withoutFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDD RID: 24029 RVA: 0x00137C33 File Offset: 0x00135E33
				public startTitle? startTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDE RID: 24030 RVA: 0x00137C41 File Offset: 0x00135E41
				public title? title(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DDF RID: 24031 RVA: 0x00137C4F File Offset: 0x00135E4F
				public aboveOrLeftmost? aboveOrLeftmost(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE0 RID: 24032 RVA: 0x00137C5D File Offset: 0x00135E5D
				public aboveOrOutput? aboveOrOutput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE1 RID: 24033 RVA: 0x00137C6B File Offset: 0x00135E6B
				public aboveOrHeader? aboveOrHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE2 RID: 24034 RVA: 0x00137C79 File Offset: 0x00135E79
				public headerSection? headerSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE3 RID: 24035 RVA: 0x00137C87 File Offset: 0x00135E87
				public splitForTitle? splitForTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE4 RID: 24036 RVA: 0x00137C95 File Offset: 0x00135E95
				public above? above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE5 RID: 24037 RVA: 0x00137CA3 File Offset: 0x00135EA3
				public titleOf? titleOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE6 RID: 24038 RVA: 0x00137CB1 File Offset: 0x00135EB1
				public titleAboveMode? titleAboveMode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode.CreateSafe(this._builders, node);
				}

				// Token: 0x04002B52 RID: 11090
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DFB RID: 3579
			public class RuleAs
			{
				// Token: 0x06005DE7 RID: 24039 RVA: 0x00137CBF File Offset: 0x00135EBF
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005DE8 RID: 24040 RVA: 0x00137CCE File Offset: 0x00135ECE
				public Output? Output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Output.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DE9 RID: 24041 RVA: 0x00137CDC File Offset: 0x00135EDC
				public Trim? Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DEA RID: 24042 RVA: 0x00137CEA File Offset: 0x00135EEA
				public TrimHidden? TrimHidden(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHidden.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DEB RID: 24043 RVA: 0x00137CF8 File Offset: 0x00135EF8
				public area_trimLeft? area_trimLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.area_trimLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DEC RID: 24044 RVA: 0x00137D06 File Offset: 0x00135F06
				public Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange? DefinedRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.DefinedRange.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DED RID: 24045 RVA: 0x00137D14 File Offset: 0x00135F14
				public trimLeft_trimBottom? trimLeft_trimBottom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimLeft_trimBottom.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DEE RID: 24046 RVA: 0x00137D22 File Offset: 0x00135F22
				public TrimLeftSingleCellColumns? TrimLeftSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimLeftSingleCellColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DEF RID: 24047 RVA: 0x00137D30 File Offset: 0x00135F30
				public trimBottom_trimTop? trimBottom_trimTop(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimBottom_trimTop.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF0 RID: 24048 RVA: 0x00137D3E File Offset: 0x00135F3E
				public TrimBottomSingleCellRows? TrimBottomSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBottomSingleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF1 RID: 24049 RVA: 0x00137D4C File Offset: 0x00135F4C
				public TakeUntilEmptyRow? TakeUntilEmptyRow(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyRow.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF2 RID: 24050 RVA: 0x00137D5A File Offset: 0x00135F5A
				public TrimAboveBottomBorder? TrimAboveBottomBorder(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimAboveBottomBorder.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF3 RID: 24051 RVA: 0x00137D68 File Offset: 0x00135F68
				public trimTop_sheetSection? trimTop_sheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.trimTop_sheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF4 RID: 24052 RVA: 0x00137D76 File Offset: 0x00135F76
				public FreezePaneTight? FreezePaneTight(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneTight.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF5 RID: 24053 RVA: 0x00137D84 File Offset: 0x00135F84
				public FreezePaneToBlanks? FreezePaneToBlanks(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToBlanks.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF6 RID: 24054 RVA: 0x00137D92 File Offset: 0x00135F92
				public FreezePaneToMultipleBlanks? FreezePaneToMultipleBlanks(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FreezePaneToMultipleBlanks.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF7 RID: 24055 RVA: 0x00137DA0 File Offset: 0x00135FA0
				public TrimTopMergedCellRows? TrimTopMergedCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopMergedCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF8 RID: 24056 RVA: 0x00137DAE File Offset: 0x00135FAE
				public TrimTopFullWidthMergedCellRows? TrimTopFullWidthMergedCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopFullWidthMergedCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DF9 RID: 24057 RVA: 0x00137DBC File Offset: 0x00135FBC
				public TrimTopSingleCellRows? TrimTopSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimTopSingleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFA RID: 24058 RVA: 0x00137DCA File Offset: 0x00135FCA
				public TrimBelowTopBorder? TrimBelowTopBorder(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimBelowTopBorder.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFB RID: 24059 RVA: 0x00137DD8 File Offset: 0x00135FD8
				public TakeAfterEmptyRow? TakeAfterEmptyRow(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeAfterEmptyRow.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFC RID: 24060 RVA: 0x00137DE6 File Offset: 0x00135FE6
				public sheetSection_horizontalSheetSection? sheetSection_horizontalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.sheetSection_horizontalSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFD RID: 24061 RVA: 0x00137DF4 File Offset: 0x00135FF4
				public TakeUntilEmptyColumn? TakeUntilEmptyColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TakeUntilEmptyColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFE RID: 24062 RVA: 0x00137E02 File Offset: 0x00136002
				public TrimRightSingleCellColumns? TrimRightSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimRightSingleCellColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005DFF RID: 24063 RVA: 0x00137E10 File Offset: 0x00136010
				public horizontalSheetSection_verticalSheetSection? horizontalSheetSection_verticalSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.horizontalSheetSection_verticalSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E00 RID: 24064 RVA: 0x00137E1E File Offset: 0x0013601E
				public KthHorizontal? KthHorizontal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthHorizontal.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E01 RID: 24065 RVA: 0x00137E2C File Offset: 0x0013602C
				public verticalSheetSection_uncleanedSheetSection? verticalSheetSection_uncleanedSheetSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.verticalSheetSection_uncleanedSheetSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E02 RID: 24066 RVA: 0x00137E3A File Offset: 0x0013603A
				public KthVertical? KthVertical(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthVertical.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E03 RID: 24067 RVA: 0x00137E48 File Offset: 0x00136048
				public uncleanedSheetSection_wholeSheet? uncleanedSheetSection_wholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.uncleanedSheetSection_wholeSheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E04 RID: 24068 RVA: 0x00137E56 File Offset: 0x00136056
				public KthSplit? KthSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E05 RID: 24069 RVA: 0x00137E64 File Offset: 0x00136064
				public Area? Area(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.Area.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E06 RID: 24070 RVA: 0x00137E72 File Offset: 0x00136072
				public wholeSheet_wholeSheetFull? wholeSheet_wholeSheetFull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.wholeSheet_wholeSheetFull.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E07 RID: 24071 RVA: 0x00137E80 File Offset: 0x00136080
				public TrimHiddenWholeSheet? TrimHiddenWholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TrimHiddenWholeSheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E08 RID: 24072 RVA: 0x00137E8E File Offset: 0x0013608E
				public WholeSheet? WholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WholeSheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E09 RID: 24073 RVA: 0x00137E9C File Offset: 0x0013609C
				public WithFormatting? WithFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithFormatting.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0A RID: 24074 RVA: 0x00137EAA File Offset: 0x001360AA
				public SplitOnEmptyRows? SplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0B RID: 24075 RVA: 0x00137EB8 File Offset: 0x001360B8
				public SplitOnMatchingRows? SplitOnMatchingRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnMatchingRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0C RID: 24076 RVA: 0x00137EC6 File Offset: 0x001360C6
				public SplitOnEmptyColumns? SplitOnEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.SplitOnEmptyColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0D RID: 24077 RVA: 0x00137ED4 File Offset: 0x001360D4
				public BorderedAreas? BorderedAreas(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BorderedAreas.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0E RID: 24078 RVA: 0x00137EE2 File Offset: 0x001360E2
				public mProgram_mTable? mProgram_mTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.mProgram_mTable.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E0F RID: 24079 RVA: 0x00137EF0 File Offset: 0x001360F0
				public RemoveEmptyRows? RemoveEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E10 RID: 24080 RVA: 0x00137EFE File Offset: 0x001360FE
				public RemoveEmptyColumns? RemoveEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.RemoveEmptyColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E11 RID: 24081 RVA: 0x00137F0C File Offset: 0x0013610C
				public MWholeSheet? MWholeSheet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MWholeSheet.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E12 RID: 24082 RVA: 0x00137F1A File Offset: 0x0013611A
				public KthMSection? KthMSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthMSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E13 RID: 24083 RVA: 0x00137F28 File Offset: 0x00136128
				public KthAndNextMSection? KthAndNextMSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.KthAndNextMSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E14 RID: 24084 RVA: 0x00137F36 File Offset: 0x00136136
				public MTrimTopSingleCellRows? MTrimTopSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E15 RID: 24085 RVA: 0x00137F44 File Offset: 0x00136144
				public MTrimTopSingleLeftCellRows? MTrimTopSingleLeftCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopSingleLeftCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E16 RID: 24086 RVA: 0x00137F52 File Offset: 0x00136152
				public MTrimBottomSingleCellRows? MTrimBottomSingleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomSingleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E17 RID: 24087 RVA: 0x00137F60 File Offset: 0x00136160
				public MTrimLeftSingleCellColumns? MTrimLeftSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimLeftSingleCellColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E18 RID: 24088 RVA: 0x00137F6E File Offset: 0x0013616E
				public MTrimRightSingleCellColumns? MTrimRightSingleCellColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimRightSingleCellColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E19 RID: 24089 RVA: 0x00137F7C File Offset: 0x0013617C
				public MTrimTopDoubleCellRows? MTrimTopDoubleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimTopDoubleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1A RID: 24090 RVA: 0x00137F8A File Offset: 0x0013618A
				public MTrimBottomDoubleCellRows? MTrimBottomDoubleCellRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MTrimBottomDoubleCellRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1B RID: 24091 RVA: 0x00137F98 File Offset: 0x00136198
				public MSplitOnEmptyRows? MSplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1C RID: 24092 RVA: 0x00137FA6 File Offset: 0x001361A6
				public MSplitOnEmptyColumns? MSplitOnEmptyColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.MSplitOnEmptyColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1D RID: 24093 RVA: 0x00137FB4 File Offset: 0x001361B4
				public WithoutFormatting? WithoutFormatting(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WithoutFormatting.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1E RID: 24094 RVA: 0x00137FC2 File Offset: 0x001361C2
				public StartTitle? StartTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.StartTitle.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E1F RID: 24095 RVA: 0x00137FD0 File Offset: 0x001361D0
				public title_above? title_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.title_above.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E20 RID: 24096 RVA: 0x00137FDE File Offset: 0x001361DE
				public TopLeftCell? TopLeftCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopLeftCell.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E21 RID: 24097 RVA: 0x00137FEC File Offset: 0x001361EC
				public TopSameFontCells? TopSameFontCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TopSameFontCells.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E22 RID: 24098 RVA: 0x00137FFA File Offset: 0x001361FA
				public BottomLeftSameFontCells? BottomLeftSameFontCells(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.BottomLeftSameFontCells.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E23 RID: 24099 RVA: 0x00138008 File Offset: 0x00136208
				public aboveOrLeftmost_above? aboveOrLeftmost_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrLeftmost_above.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E24 RID: 24100 RVA: 0x00138016 File Offset: 0x00136216
				public LeftmostColumn? LeftmostColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftmostColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E25 RID: 24101 RVA: 0x00138024 File Offset: 0x00136224
				public LeftOf? LeftOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.LeftOf.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E26 RID: 24102 RVA: 0x00138032 File Offset: 0x00136232
				public aboveOrOutput_aboveOrHeader? aboveOrOutput_aboveOrHeader(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_aboveOrHeader.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E27 RID: 24103 RVA: 0x00138040 File Offset: 0x00136240
				public aboveOrOutput_titleOf? aboveOrOutput_titleOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrOutput_titleOf.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E28 RID: 24104 RVA: 0x0013804E File Offset: 0x0013624E
				public aboveOrHeader_above? aboveOrHeader_above(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_above.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E29 RID: 24105 RVA: 0x0013805C File Offset: 0x0013625C
				public aboveOrHeader_headerSection? aboveOrHeader_headerSection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes.aboveOrHeader_headerSection.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2A RID: 24106 RVA: 0x0013806A File Offset: 0x0013626A
				public FirstSplit? FirstSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.FirstSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2B RID: 24107 RVA: 0x00138078 File Offset: 0x00136278
				public TitleSplitOnEmptyRows? TitleSplitOnEmptyRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnEmptyRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2C RID: 24108 RVA: 0x00138086 File Offset: 0x00136286
				public TitleSplitOnMatchingRows? TitleSplitOnMatchingRows(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleSplitOnMatchingRows.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2D RID: 24109 RVA: 0x00138094 File Offset: 0x00136294
				public TitleCellsAbove? TitleCellsAbove(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAbove.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2E RID: 24110 RVA: 0x001380A2 File Offset: 0x001362A2
				public TitleCellsAboveMatching? TitleCellsAboveMatching(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.TitleCellsAboveMatching.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E2F RID: 24111 RVA: 0x001380B0 File Offset: 0x001362B0
				public WrapOutputForTitle? WrapOutputForTitle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.WrapOutputForTitle.CreateSafe(this._builders, node);
				}

				// Token: 0x06005E30 RID: 24112 RVA: 0x001380BE File Offset: 0x001362BE
				public IncludeEmptyToLeft? IncludeEmptyToLeft(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes.IncludeEmptyToLeft.CreateSafe(this._builders, node);
				}

				// Token: 0x04002B53 RID: 11091
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000DFD RID: 3581
		public class Sets
		{
			// Token: 0x06005E34 RID: 24116 RVA: 0x001380E8 File Offset: 0x001362E8
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17001141 RID: 4417
			// (get) Token: 0x06005E35 RID: 24117 RVA: 0x00138137 File Offset: 0x00136337
			// (set) Token: 0x06005E36 RID: 24118 RVA: 0x0013813F File Offset: 0x0013633F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17001142 RID: 4418
			// (get) Token: 0x06005E37 RID: 24119 RVA: 0x00138148 File Offset: 0x00136348
			// (set) Token: 0x06005E38 RID: 24120 RVA: 0x00138150 File Offset: 0x00136350
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17001143 RID: 4419
			// (get) Token: 0x06005E39 RID: 24121 RVA: 0x00138159 File Offset: 0x00136359
			// (set) Token: 0x06005E3A RID: 24122 RVA: 0x00138161 File Offset: 0x00136361
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17001144 RID: 4420
			// (get) Token: 0x06005E3B RID: 24123 RVA: 0x0013816A File Offset: 0x0013636A
			// (set) Token: 0x06005E3C RID: 24124 RVA: 0x00138172 File Offset: 0x00136372
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17001145 RID: 4421
			// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0013817B File Offset: 0x0013637B
			// (set) Token: 0x06005E3E RID: 24126 RVA: 0x00138183 File Offset: 0x00136383
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000DFE RID: 3582
			public class Joins
			{
				// Token: 0x06005E3F RID: 24127 RVA: 0x0013818C File Offset: 0x0013638C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005E40 RID: 24128 RVA: 0x0013819B File Offset: 0x0013639B
				public ProgramSetBuilder<trim> Trim(ProgramSetBuilder<area> value0)
				{
					return ProgramSetBuilder<trim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E41 RID: 24129 RVA: 0x001381CC File Offset: 0x001363CC
				public ProgramSetBuilder<trim> TrimHidden(ProgramSetBuilder<area> value0)
				{
					return ProgramSetBuilder<trim>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimHidden, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E42 RID: 24130 RVA: 0x001381FD File Offset: 0x001363FD
				public ProgramSetBuilder<area> DefinedRange(ProgramSetBuilder<sheet> value0, ProgramSetBuilder<rangeName> value1)
				{
					return ProgramSetBuilder<area>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DefinedRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E43 RID: 24131 RVA: 0x0013823D File Offset: 0x0013643D
				public ProgramSetBuilder<trimLeft> TrimLeftSingleCellColumns(ProgramSetBuilder<trimBottom> value0)
				{
					return ProgramSetBuilder<trimLeft>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimLeftSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E44 RID: 24132 RVA: 0x0013826E File Offset: 0x0013646E
				public ProgramSetBuilder<trimBottom> TrimBottomSingleCellRows(ProgramSetBuilder<trimTop> value0)
				{
					return ProgramSetBuilder<trimBottom>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimBottomSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E45 RID: 24133 RVA: 0x0013829F File Offset: 0x0013649F
				public ProgramSetBuilder<trimBottom> TakeUntilEmptyRow(ProgramSetBuilder<trimTop> value0)
				{
					return ProgramSetBuilder<trimBottom>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TakeUntilEmptyRow, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E46 RID: 24134 RVA: 0x001382D0 File Offset: 0x001364D0
				public ProgramSetBuilder<trimBottom> TrimAboveBottomBorder(ProgramSetBuilder<trimTop> value0)
				{
					return ProgramSetBuilder<trimBottom>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimAboveBottomBorder, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E47 RID: 24135 RVA: 0x00138301 File Offset: 0x00136501
				public ProgramSetBuilder<trimTop> FreezePaneTight(ProgramSetBuilder<sheet> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FreezePaneTight, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E48 RID: 24136 RVA: 0x00138332 File Offset: 0x00136532
				public ProgramSetBuilder<trimTop> FreezePaneToBlanks(ProgramSetBuilder<sheet> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FreezePaneToBlanks, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E49 RID: 24137 RVA: 0x00138363 File Offset: 0x00136563
				public ProgramSetBuilder<trimTop> FreezePaneToMultipleBlanks(ProgramSetBuilder<sheet> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FreezePaneToMultipleBlanks, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4A RID: 24138 RVA: 0x00138394 File Offset: 0x00136594
				public ProgramSetBuilder<trimTop> TrimTopMergedCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimTopMergedCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4B RID: 24139 RVA: 0x001383C5 File Offset: 0x001365C5
				public ProgramSetBuilder<trimTop> TrimTopFullWidthMergedCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimTopFullWidthMergedCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4C RID: 24140 RVA: 0x001383F6 File Offset: 0x001365F6
				public ProgramSetBuilder<trimTop> TrimTopSingleCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimTopSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4D RID: 24141 RVA: 0x00138427 File Offset: 0x00136627
				public ProgramSetBuilder<trimTop> TrimBelowTopBorder(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimBelowTopBorder, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4E RID: 24142 RVA: 0x00138458 File Offset: 0x00136658
				public ProgramSetBuilder<trimTop> TakeAfterEmptyRow(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TakeAfterEmptyRow, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E4F RID: 24143 RVA: 0x00138489 File Offset: 0x00136689
				public ProgramSetBuilder<sheetSection> TakeUntilEmptyColumn(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return ProgramSetBuilder<sheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TakeUntilEmptyColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E50 RID: 24144 RVA: 0x001384BA File Offset: 0x001366BA
				public ProgramSetBuilder<sheetSection> TrimRightSingleCellColumns(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return ProgramSetBuilder<sheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimRightSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E51 RID: 24145 RVA: 0x001384EC File Offset: 0x001366EC
				public ProgramSetBuilder<uncleanedSheetSection> Area(ProgramSetBuilder<sheet> value0, ProgramSetBuilder<index> value1, ProgramSetBuilder<index> value2, ProgramSetBuilder<index> value3, ProgramSetBuilder<index> value4)
				{
					return ProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Area, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x06005E52 RID: 24146 RVA: 0x00138568 File Offset: 0x00136768
				public ProgramSetBuilder<wholeSheet> TrimHiddenWholeSheet(ProgramSetBuilder<wholeSheetFull> value0)
				{
					return ProgramSetBuilder<wholeSheet>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimHiddenWholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E53 RID: 24147 RVA: 0x00138599 File Offset: 0x00136799
				public ProgramSetBuilder<wholeSheetFull> WholeSheet(ProgramSetBuilder<sheet> value0)
				{
					return ProgramSetBuilder<wholeSheetFull>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E54 RID: 24148 RVA: 0x001385CA File Offset: 0x001367CA
				public ProgramSetBuilder<sheet> WithFormatting(ProgramSetBuilder<sheetPair> value0)
				{
					return ProgramSetBuilder<sheet>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WithFormatting, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E55 RID: 24149 RVA: 0x001385FB File Offset: 0x001367FB
				public ProgramSetBuilder<horizontalSheetSplits> SplitOnEmptyRows(ProgramSetBuilder<verticalSheetSection> value0)
				{
					return ProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E56 RID: 24150 RVA: 0x0013862C File Offset: 0x0013682C
				public ProgramSetBuilder<horizontalSheetSplits> SplitOnMatchingRows(ProgramSetBuilder<verticalSheetSection> value0, ProgramSetBuilder<styleFilter> value1, ProgramSetBuilder<splitMode> value2)
				{
					return ProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitOnMatchingRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005E57 RID: 24151 RVA: 0x00138686 File Offset: 0x00136886
				public ProgramSetBuilder<verticalSheetSplits> SplitOnEmptyColumns(ProgramSetBuilder<uncleanedSheetSection> value0)
				{
					return ProgramSetBuilder<verticalSheetSplits>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitOnEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E58 RID: 24152 RVA: 0x001386B7 File Offset: 0x001368B7
				public ProgramSetBuilder<sheetSplits> BorderedAreas(ProgramSetBuilder<wholeSheet> value0)
				{
					return ProgramSetBuilder<sheetSplits>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.BorderedAreas, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E59 RID: 24153 RVA: 0x001386E8 File Offset: 0x001368E8
				public ProgramSetBuilder<mProgram> RemoveEmptyRows(ProgramSetBuilder<mProgram> value0)
				{
					return ProgramSetBuilder<mProgram>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RemoveEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E5A RID: 24154 RVA: 0x00138719 File Offset: 0x00136919
				public ProgramSetBuilder<mProgram> RemoveEmptyColumns(ProgramSetBuilder<mProgram> value0)
				{
					return ProgramSetBuilder<mProgram>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RemoveEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E5B RID: 24155 RVA: 0x0013874A File Offset: 0x0013694A
				public ProgramSetBuilder<mTable> MWholeSheet(ProgramSetBuilder<withoutFormatting> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MWholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E5C RID: 24156 RVA: 0x0013877B File Offset: 0x0013697B
				public ProgramSetBuilder<mTable> KthAndNextMSection(ProgramSetBuilder<mSection> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthAndNextMSection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E5D RID: 24157 RVA: 0x001387BB File Offset: 0x001369BB
				public ProgramSetBuilder<mTable> MTrimTopSingleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimTopSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E5E RID: 24158 RVA: 0x001387EC File Offset: 0x001369EC
				public ProgramSetBuilder<mTable> MTrimTopSingleLeftCellRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimTopSingleLeftCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E5F RID: 24159 RVA: 0x0013881D File Offset: 0x00136A1D
				public ProgramSetBuilder<mTable> MTrimBottomSingleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimBottomSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E60 RID: 24160 RVA: 0x0013884E File Offset: 0x00136A4E
				public ProgramSetBuilder<mTable> MTrimLeftSingleCellColumns(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimLeftSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E61 RID: 24161 RVA: 0x0013887F File Offset: 0x00136A7F
				public ProgramSetBuilder<mTable> MTrimRightSingleCellColumns(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimRightSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E62 RID: 24162 RVA: 0x001388B0 File Offset: 0x00136AB0
				public ProgramSetBuilder<mTable> MTrimTopDoubleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimTopDoubleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E63 RID: 24163 RVA: 0x001388E1 File Offset: 0x00136AE1
				public ProgramSetBuilder<mTable> MTrimBottomDoubleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MTrimBottomDoubleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E64 RID: 24164 RVA: 0x00138912 File Offset: 0x00136B12
				public ProgramSetBuilder<mSection> MSplitOnEmptyRows(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MSplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E65 RID: 24165 RVA: 0x00138943 File Offset: 0x00136B43
				public ProgramSetBuilder<mSection> MSplitOnEmptyColumns(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MSplitOnEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E66 RID: 24166 RVA: 0x00138974 File Offset: 0x00136B74
				public ProgramSetBuilder<withoutFormatting> WithoutFormatting(ProgramSetBuilder<sheetPair> value0)
				{
					return ProgramSetBuilder<withoutFormatting>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WithoutFormatting, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E67 RID: 24167 RVA: 0x001389A5 File Offset: 0x00136BA5
				public ProgramSetBuilder<title> TopLeftCell(ProgramSetBuilder<aboveOrLeftmost> value0)
				{
					return ProgramSetBuilder<title>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TopLeftCell, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E68 RID: 24168 RVA: 0x001389D6 File Offset: 0x00136BD6
				public ProgramSetBuilder<title> TopSameFontCells(ProgramSetBuilder<aboveOrLeftmost> value0)
				{
					return ProgramSetBuilder<title>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TopSameFontCells, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E69 RID: 24169 RVA: 0x00138A07 File Offset: 0x00136C07
				public ProgramSetBuilder<title> BottomLeftSameFontCells(ProgramSetBuilder<aboveOrHeader> value0)
				{
					return ProgramSetBuilder<title>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.BottomLeftSameFontCells, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E6A RID: 24170 RVA: 0x00138A38 File Offset: 0x00136C38
				public ProgramSetBuilder<aboveOrLeftmost> LeftmostColumn(ProgramSetBuilder<aboveOrOutput> value0)
				{
					return ProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeftmostColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E6B RID: 24171 RVA: 0x00138A69 File Offset: 0x00136C69
				public ProgramSetBuilder<aboveOrLeftmost> LeftOf(ProgramSetBuilder<titleOf> value0)
				{
					return ProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeftOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E6C RID: 24172 RVA: 0x00138A9A File Offset: 0x00136C9A
				public ProgramSetBuilder<headerSection> FirstSplit(ProgramSetBuilder<splitForTitle> value0)
				{
					return ProgramSetBuilder<headerSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FirstSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E6D RID: 24173 RVA: 0x00138ACB File Offset: 0x00136CCB
				public ProgramSetBuilder<splitForTitle> TitleSplitOnEmptyRows(ProgramSetBuilder<titleOf> value0)
				{
					return ProgramSetBuilder<splitForTitle>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TitleSplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E6E RID: 24174 RVA: 0x00138AFC File Offset: 0x00136CFC
				public ProgramSetBuilder<splitForTitle> TitleSplitOnMatchingRows(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<styleFilter> value1, ProgramSetBuilder<splitMode> value2)
				{
					return ProgramSetBuilder<splitForTitle>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TitleSplitOnMatchingRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005E6F RID: 24175 RVA: 0x00138B56 File Offset: 0x00136D56
				public ProgramSetBuilder<above> TitleCellsAbove(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<titleAboveMode> value1)
				{
					return ProgramSetBuilder<above>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TitleCellsAbove, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E70 RID: 24176 RVA: 0x00138B98 File Offset: 0x00136D98
				public ProgramSetBuilder<above> TitleCellsAboveMatching(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<titleAboveMode> value1, ProgramSetBuilder<styleFilter> value2)
				{
					return ProgramSetBuilder<above>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TitleCellsAboveMatching, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005E71 RID: 24177 RVA: 0x00138BF2 File Offset: 0x00136DF2
				public ProgramSetBuilder<titleOf> IncludeEmptyToLeft(ProgramSetBuilder<output> value0)
				{
					return ProgramSetBuilder<titleOf>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IncludeEmptyToLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E72 RID: 24178 RVA: 0x00138C23 File Offset: 0x00136E23
				public ProgramSetBuilder<horizontalSheetSection> KthHorizontal(ProgramSetBuilder<horizontalSheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthHorizontal, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E73 RID: 24179 RVA: 0x00138C63 File Offset: 0x00136E63
				public ProgramSetBuilder<verticalSheetSection> KthVertical(ProgramSetBuilder<verticalSheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<verticalSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthVertical, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E74 RID: 24180 RVA: 0x00138CA3 File Offset: 0x00136EA3
				public ProgramSetBuilder<uncleanedSheetSection> KthSplit(ProgramSetBuilder<sheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E75 RID: 24181 RVA: 0x00138CE3 File Offset: 0x00136EE3
				public ProgramSetBuilder<mTable> KthMSection(ProgramSetBuilder<mSection> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<mTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthMSection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E76 RID: 24182 RVA: 0x00138D23 File Offset: 0x00136F23
				public ProgramSetBuilder<output> Output(ProgramSetBuilder<trim> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Output, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E77 RID: 24183 RVA: 0x00138D54 File Offset: 0x00136F54
				public ProgramSetBuilder<startTitle> StartTitle(ProgramSetBuilder<title> value0)
				{
					return ProgramSetBuilder<startTitle>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartTitle, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E78 RID: 24184 RVA: 0x00138D85 File Offset: 0x00136F85
				public ProgramSetBuilder<titleOf> WrapOutputForTitle(ProgramSetBuilder<output> value0)
				{
					return ProgramSetBuilder<titleOf>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WrapOutputForTitle, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002B5A RID: 11098
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000DFF RID: 3583
			public class ExplicitJoins
			{
				// Token: 0x06005E79 RID: 24185 RVA: 0x00138DB6 File Offset: 0x00136FB6
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005E7A RID: 24186 RVA: 0x00138DC5 File Offset: 0x00136FC5
				public JoinProgramSetBuilder<trim> Trim(ProgramSetBuilder<area> value0)
				{
					return JoinProgramSetBuilder<trim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E7B RID: 24187 RVA: 0x00138DF6 File Offset: 0x00136FF6
				public JoinProgramSetBuilder<trim> TrimHidden(ProgramSetBuilder<area> value0)
				{
					return JoinProgramSetBuilder<trim>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimHidden, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E7C RID: 24188 RVA: 0x00138E27 File Offset: 0x00137027
				public JoinProgramSetBuilder<area> DefinedRange(ProgramSetBuilder<sheet> value0, ProgramSetBuilder<rangeName> value1)
				{
					return JoinProgramSetBuilder<area>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DefinedRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E7D RID: 24189 RVA: 0x00138E67 File Offset: 0x00137067
				public JoinProgramSetBuilder<trimLeft> TrimLeftSingleCellColumns(ProgramSetBuilder<trimBottom> value0)
				{
					return JoinProgramSetBuilder<trimLeft>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimLeftSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E7E RID: 24190 RVA: 0x00138E98 File Offset: 0x00137098
				public JoinProgramSetBuilder<trimBottom> TrimBottomSingleCellRows(ProgramSetBuilder<trimTop> value0)
				{
					return JoinProgramSetBuilder<trimBottom>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimBottomSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E7F RID: 24191 RVA: 0x00138EC9 File Offset: 0x001370C9
				public JoinProgramSetBuilder<trimBottom> TakeUntilEmptyRow(ProgramSetBuilder<trimTop> value0)
				{
					return JoinProgramSetBuilder<trimBottom>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TakeUntilEmptyRow, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E80 RID: 24192 RVA: 0x00138EFA File Offset: 0x001370FA
				public JoinProgramSetBuilder<trimBottom> TrimAboveBottomBorder(ProgramSetBuilder<trimTop> value0)
				{
					return JoinProgramSetBuilder<trimBottom>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimAboveBottomBorder, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E81 RID: 24193 RVA: 0x00138F2B File Offset: 0x0013712B
				public JoinProgramSetBuilder<trimTop> FreezePaneTight(ProgramSetBuilder<sheet> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FreezePaneTight, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E82 RID: 24194 RVA: 0x00138F5C File Offset: 0x0013715C
				public JoinProgramSetBuilder<trimTop> FreezePaneToBlanks(ProgramSetBuilder<sheet> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FreezePaneToBlanks, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E83 RID: 24195 RVA: 0x00138F8D File Offset: 0x0013718D
				public JoinProgramSetBuilder<trimTop> FreezePaneToMultipleBlanks(ProgramSetBuilder<sheet> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FreezePaneToMultipleBlanks, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E84 RID: 24196 RVA: 0x00138FBE File Offset: 0x001371BE
				public JoinProgramSetBuilder<trimTop> TrimTopMergedCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimTopMergedCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E85 RID: 24197 RVA: 0x00138FEF File Offset: 0x001371EF
				public JoinProgramSetBuilder<trimTop> TrimTopFullWidthMergedCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimTopFullWidthMergedCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E86 RID: 24198 RVA: 0x00139020 File Offset: 0x00137220
				public JoinProgramSetBuilder<trimTop> TrimTopSingleCellRows(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimTopSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E87 RID: 24199 RVA: 0x00139051 File Offset: 0x00137251
				public JoinProgramSetBuilder<trimTop> TrimBelowTopBorder(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimBelowTopBorder, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E88 RID: 24200 RVA: 0x00139082 File Offset: 0x00137282
				public JoinProgramSetBuilder<trimTop> TakeAfterEmptyRow(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TakeAfterEmptyRow, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E89 RID: 24201 RVA: 0x001390B3 File Offset: 0x001372B3
				public JoinProgramSetBuilder<sheetSection> TakeUntilEmptyColumn(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return JoinProgramSetBuilder<sheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TakeUntilEmptyColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E8A RID: 24202 RVA: 0x001390E4 File Offset: 0x001372E4
				public JoinProgramSetBuilder<sheetSection> TrimRightSingleCellColumns(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return JoinProgramSetBuilder<sheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimRightSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E8B RID: 24203 RVA: 0x00139118 File Offset: 0x00137318
				public JoinProgramSetBuilder<uncleanedSheetSection> Area(ProgramSetBuilder<sheet> value0, ProgramSetBuilder<index> value1, ProgramSetBuilder<index> value2, ProgramSetBuilder<index> value3, ProgramSetBuilder<index> value4)
				{
					return JoinProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Area, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x06005E8C RID: 24204 RVA: 0x00139194 File Offset: 0x00137394
				public JoinProgramSetBuilder<wholeSheet> TrimHiddenWholeSheet(ProgramSetBuilder<wholeSheetFull> value0)
				{
					return JoinProgramSetBuilder<wholeSheet>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimHiddenWholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E8D RID: 24205 RVA: 0x001391C5 File Offset: 0x001373C5
				public JoinProgramSetBuilder<wholeSheetFull> WholeSheet(ProgramSetBuilder<sheet> value0)
				{
					return JoinProgramSetBuilder<wholeSheetFull>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E8E RID: 24206 RVA: 0x001391F6 File Offset: 0x001373F6
				public JoinProgramSetBuilder<sheet> WithFormatting(ProgramSetBuilder<sheetPair> value0)
				{
					return JoinProgramSetBuilder<sheet>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WithFormatting, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E8F RID: 24207 RVA: 0x00139227 File Offset: 0x00137427
				public JoinProgramSetBuilder<horizontalSheetSplits> SplitOnEmptyRows(ProgramSetBuilder<verticalSheetSection> value0)
				{
					return JoinProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E90 RID: 24208 RVA: 0x00139258 File Offset: 0x00137458
				public JoinProgramSetBuilder<horizontalSheetSplits> SplitOnMatchingRows(ProgramSetBuilder<verticalSheetSection> value0, ProgramSetBuilder<styleFilter> value1, ProgramSetBuilder<splitMode> value2)
				{
					return JoinProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitOnMatchingRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005E91 RID: 24209 RVA: 0x001392B2 File Offset: 0x001374B2
				public JoinProgramSetBuilder<verticalSheetSplits> SplitOnEmptyColumns(ProgramSetBuilder<uncleanedSheetSection> value0)
				{
					return JoinProgramSetBuilder<verticalSheetSplits>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitOnEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E92 RID: 24210 RVA: 0x001392E3 File Offset: 0x001374E3
				public JoinProgramSetBuilder<sheetSplits> BorderedAreas(ProgramSetBuilder<wholeSheet> value0)
				{
					return JoinProgramSetBuilder<sheetSplits>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.BorderedAreas, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E93 RID: 24211 RVA: 0x00139314 File Offset: 0x00137514
				public JoinProgramSetBuilder<mProgram> RemoveEmptyRows(ProgramSetBuilder<mProgram> value0)
				{
					return JoinProgramSetBuilder<mProgram>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RemoveEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E94 RID: 24212 RVA: 0x00139345 File Offset: 0x00137545
				public JoinProgramSetBuilder<mProgram> RemoveEmptyColumns(ProgramSetBuilder<mProgram> value0)
				{
					return JoinProgramSetBuilder<mProgram>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RemoveEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E95 RID: 24213 RVA: 0x00139376 File Offset: 0x00137576
				public JoinProgramSetBuilder<mTable> MWholeSheet(ProgramSetBuilder<withoutFormatting> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MWholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E96 RID: 24214 RVA: 0x001393A7 File Offset: 0x001375A7
				public JoinProgramSetBuilder<mTable> KthAndNextMSection(ProgramSetBuilder<mSection> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthAndNextMSection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005E97 RID: 24215 RVA: 0x001393E7 File Offset: 0x001375E7
				public JoinProgramSetBuilder<mTable> MTrimTopSingleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimTopSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E98 RID: 24216 RVA: 0x00139418 File Offset: 0x00137618
				public JoinProgramSetBuilder<mTable> MTrimTopSingleLeftCellRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimTopSingleLeftCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E99 RID: 24217 RVA: 0x00139449 File Offset: 0x00137649
				public JoinProgramSetBuilder<mTable> MTrimBottomSingleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimBottomSingleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9A RID: 24218 RVA: 0x0013947A File Offset: 0x0013767A
				public JoinProgramSetBuilder<mTable> MTrimLeftSingleCellColumns(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimLeftSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9B RID: 24219 RVA: 0x001394AB File Offset: 0x001376AB
				public JoinProgramSetBuilder<mTable> MTrimRightSingleCellColumns(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimRightSingleCellColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9C RID: 24220 RVA: 0x001394DC File Offset: 0x001376DC
				public JoinProgramSetBuilder<mTable> MTrimTopDoubleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimTopDoubleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9D RID: 24221 RVA: 0x0013950D File Offset: 0x0013770D
				public JoinProgramSetBuilder<mTable> MTrimBottomDoubleCellRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MTrimBottomDoubleCellRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9E RID: 24222 RVA: 0x0013953E File Offset: 0x0013773E
				public JoinProgramSetBuilder<mSection> MSplitOnEmptyRows(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MSplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005E9F RID: 24223 RVA: 0x0013956F File Offset: 0x0013776F
				public JoinProgramSetBuilder<mSection> MSplitOnEmptyColumns(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MSplitOnEmptyColumns, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA0 RID: 24224 RVA: 0x001395A0 File Offset: 0x001377A0
				public JoinProgramSetBuilder<withoutFormatting> WithoutFormatting(ProgramSetBuilder<sheetPair> value0)
				{
					return JoinProgramSetBuilder<withoutFormatting>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WithoutFormatting, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA1 RID: 24225 RVA: 0x001395D1 File Offset: 0x001377D1
				public JoinProgramSetBuilder<title> TopLeftCell(ProgramSetBuilder<aboveOrLeftmost> value0)
				{
					return JoinProgramSetBuilder<title>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TopLeftCell, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA2 RID: 24226 RVA: 0x00139602 File Offset: 0x00137802
				public JoinProgramSetBuilder<title> TopSameFontCells(ProgramSetBuilder<aboveOrLeftmost> value0)
				{
					return JoinProgramSetBuilder<title>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TopSameFontCells, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA3 RID: 24227 RVA: 0x00139633 File Offset: 0x00137833
				public JoinProgramSetBuilder<title> BottomLeftSameFontCells(ProgramSetBuilder<aboveOrHeader> value0)
				{
					return JoinProgramSetBuilder<title>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.BottomLeftSameFontCells, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA4 RID: 24228 RVA: 0x00139664 File Offset: 0x00137864
				public JoinProgramSetBuilder<aboveOrLeftmost> LeftmostColumn(ProgramSetBuilder<aboveOrOutput> value0)
				{
					return JoinProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeftmostColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA5 RID: 24229 RVA: 0x00139695 File Offset: 0x00137895
				public JoinProgramSetBuilder<aboveOrLeftmost> LeftOf(ProgramSetBuilder<titleOf> value0)
				{
					return JoinProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeftOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA6 RID: 24230 RVA: 0x001396C6 File Offset: 0x001378C6
				public JoinProgramSetBuilder<headerSection> FirstSplit(ProgramSetBuilder<splitForTitle> value0)
				{
					return JoinProgramSetBuilder<headerSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FirstSplit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA7 RID: 24231 RVA: 0x001396F7 File Offset: 0x001378F7
				public JoinProgramSetBuilder<splitForTitle> TitleSplitOnEmptyRows(ProgramSetBuilder<titleOf> value0)
				{
					return JoinProgramSetBuilder<splitForTitle>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TitleSplitOnEmptyRows, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EA8 RID: 24232 RVA: 0x00139728 File Offset: 0x00137928
				public JoinProgramSetBuilder<splitForTitle> TitleSplitOnMatchingRows(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<styleFilter> value1, ProgramSetBuilder<splitMode> value2)
				{
					return JoinProgramSetBuilder<splitForTitle>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TitleSplitOnMatchingRows, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005EA9 RID: 24233 RVA: 0x00139782 File Offset: 0x00137982
				public JoinProgramSetBuilder<above> TitleCellsAbove(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<titleAboveMode> value1)
				{
					return JoinProgramSetBuilder<above>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TitleCellsAbove, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005EAA RID: 24234 RVA: 0x001397C4 File Offset: 0x001379C4
				public JoinProgramSetBuilder<above> TitleCellsAboveMatching(ProgramSetBuilder<titleOf> value0, ProgramSetBuilder<titleAboveMode> value1, ProgramSetBuilder<styleFilter> value2)
				{
					return JoinProgramSetBuilder<above>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TitleCellsAboveMatching, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06005EAB RID: 24235 RVA: 0x0013981E File Offset: 0x00137A1E
				public JoinProgramSetBuilder<titleOf> IncludeEmptyToLeft(ProgramSetBuilder<output> value0)
				{
					return JoinProgramSetBuilder<titleOf>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IncludeEmptyToLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EAC RID: 24236 RVA: 0x0013984F File Offset: 0x00137A4F
				public JoinProgramSetBuilder<horizontalSheetSection> KthHorizontal(ProgramSetBuilder<horizontalSheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthHorizontal, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005EAD RID: 24237 RVA: 0x0013988F File Offset: 0x00137A8F
				public JoinProgramSetBuilder<verticalSheetSection> KthVertical(ProgramSetBuilder<verticalSheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<verticalSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthVertical, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005EAE RID: 24238 RVA: 0x001398CF File Offset: 0x00137ACF
				public JoinProgramSetBuilder<uncleanedSheetSection> KthSplit(ProgramSetBuilder<sheetSplits> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005EAF RID: 24239 RVA: 0x0013990F File Offset: 0x00137B0F
				public JoinProgramSetBuilder<mTable> KthMSection(ProgramSetBuilder<mSection> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<mTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthMSection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06005EB0 RID: 24240 RVA: 0x0013994F File Offset: 0x00137B4F
				public JoinProgramSetBuilder<output> Output(ProgramSetBuilder<trim> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Output, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB1 RID: 24241 RVA: 0x00139980 File Offset: 0x00137B80
				public JoinProgramSetBuilder<startTitle> StartTitle(ProgramSetBuilder<title> value0)
				{
					return JoinProgramSetBuilder<startTitle>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartTitle, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB2 RID: 24242 RVA: 0x001399B1 File Offset: 0x00137BB1
				public JoinProgramSetBuilder<titleOf> WrapOutputForTitle(ProgramSetBuilder<output> value0)
				{
					return JoinProgramSetBuilder<titleOf>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WrapOutputForTitle, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002B5B RID: 11099
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000E00 RID: 3584
			public class JoinUnnamedConversions
			{
				// Token: 0x06005EB3 RID: 24243 RVA: 0x001399E2 File Offset: 0x00137BE2
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005EB4 RID: 24244 RVA: 0x001399F1 File Offset: 0x00137BF1
				public ProgramSetBuilder<area> area_trimLeft(ProgramSetBuilder<trimLeft> value0)
				{
					return ProgramSetBuilder<area>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.area_trimLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB5 RID: 24245 RVA: 0x00139A22 File Offset: 0x00137C22
				public ProgramSetBuilder<trimLeft> trimLeft_trimBottom(ProgramSetBuilder<trimBottom> value0)
				{
					return ProgramSetBuilder<trimLeft>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.trimLeft_trimBottom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB6 RID: 24246 RVA: 0x00139A53 File Offset: 0x00137C53
				public ProgramSetBuilder<trimBottom> trimBottom_trimTop(ProgramSetBuilder<trimTop> value0)
				{
					return ProgramSetBuilder<trimBottom>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.trimBottom_trimTop, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB7 RID: 24247 RVA: 0x00139A84 File Offset: 0x00137C84
				public ProgramSetBuilder<trimTop> trimTop_sheetSection(ProgramSetBuilder<sheetSection> value0)
				{
					return ProgramSetBuilder<trimTop>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.trimTop_sheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB8 RID: 24248 RVA: 0x00139AB5 File Offset: 0x00137CB5
				public ProgramSetBuilder<sheetSection> sheetSection_horizontalSheetSection(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return ProgramSetBuilder<sheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.sheetSection_horizontalSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EB9 RID: 24249 RVA: 0x00139AE6 File Offset: 0x00137CE6
				public ProgramSetBuilder<horizontalSheetSection> horizontalSheetSection_verticalSheetSection(ProgramSetBuilder<verticalSheetSection> value0)
				{
					return ProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.horizontalSheetSection_verticalSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBA RID: 24250 RVA: 0x00139B17 File Offset: 0x00137D17
				public ProgramSetBuilder<verticalSheetSection> verticalSheetSection_uncleanedSheetSection(ProgramSetBuilder<uncleanedSheetSection> value0)
				{
					return ProgramSetBuilder<verticalSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.verticalSheetSection_uncleanedSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBB RID: 24251 RVA: 0x00139B48 File Offset: 0x00137D48
				public ProgramSetBuilder<uncleanedSheetSection> uncleanedSheetSection_wholeSheet(ProgramSetBuilder<wholeSheet> value0)
				{
					return ProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.uncleanedSheetSection_wholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBC RID: 24252 RVA: 0x00139B79 File Offset: 0x00137D79
				public ProgramSetBuilder<wholeSheet> wholeSheet_wholeSheetFull(ProgramSetBuilder<wholeSheetFull> value0)
				{
					return ProgramSetBuilder<wholeSheet>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.wholeSheet_wholeSheetFull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBD RID: 24253 RVA: 0x00139BAA File Offset: 0x00137DAA
				public ProgramSetBuilder<mProgram> mProgram_mTable(ProgramSetBuilder<mTable> value0)
				{
					return ProgramSetBuilder<mProgram>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.mProgram_mTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBE RID: 24254 RVA: 0x00139BDB File Offset: 0x00137DDB
				public ProgramSetBuilder<title> title_above(ProgramSetBuilder<above> value0)
				{
					return ProgramSetBuilder<title>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.title_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EBF RID: 24255 RVA: 0x00139C0C File Offset: 0x00137E0C
				public ProgramSetBuilder<aboveOrLeftmost> aboveOrLeftmost_above(ProgramSetBuilder<above> value0)
				{
					return ProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.aboveOrLeftmost_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC0 RID: 24256 RVA: 0x00139C3D File Offset: 0x00137E3D
				public ProgramSetBuilder<aboveOrOutput> aboveOrOutput_aboveOrHeader(ProgramSetBuilder<aboveOrHeader> value0)
				{
					return ProgramSetBuilder<aboveOrOutput>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.aboveOrOutput_aboveOrHeader, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC1 RID: 24257 RVA: 0x00139C6E File Offset: 0x00137E6E
				public ProgramSetBuilder<aboveOrOutput> aboveOrOutput_titleOf(ProgramSetBuilder<titleOf> value0)
				{
					return ProgramSetBuilder<aboveOrOutput>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.aboveOrOutput_titleOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC2 RID: 24258 RVA: 0x00139C9F File Offset: 0x00137E9F
				public ProgramSetBuilder<aboveOrHeader> aboveOrHeader_above(ProgramSetBuilder<above> value0)
				{
					return ProgramSetBuilder<aboveOrHeader>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.aboveOrHeader_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC3 RID: 24259 RVA: 0x00139CD0 File Offset: 0x00137ED0
				public ProgramSetBuilder<aboveOrHeader> aboveOrHeader_headerSection(ProgramSetBuilder<headerSection> value0)
				{
					return ProgramSetBuilder<aboveOrHeader>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.aboveOrHeader_headerSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002B5C RID: 11100
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000E01 RID: 3585
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06005EC4 RID: 24260 RVA: 0x00139D01 File Offset: 0x00137F01
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005EC5 RID: 24261 RVA: 0x00139D10 File Offset: 0x00137F10
				public JoinProgramSetBuilder<area> area_trimLeft(ProgramSetBuilder<trimLeft> value0)
				{
					return JoinProgramSetBuilder<area>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.area_trimLeft, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC6 RID: 24262 RVA: 0x00139D41 File Offset: 0x00137F41
				public JoinProgramSetBuilder<trimLeft> trimLeft_trimBottom(ProgramSetBuilder<trimBottom> value0)
				{
					return JoinProgramSetBuilder<trimLeft>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.trimLeft_trimBottom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC7 RID: 24263 RVA: 0x00139D72 File Offset: 0x00137F72
				public JoinProgramSetBuilder<trimBottom> trimBottom_trimTop(ProgramSetBuilder<trimTop> value0)
				{
					return JoinProgramSetBuilder<trimBottom>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.trimBottom_trimTop, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC8 RID: 24264 RVA: 0x00139DA3 File Offset: 0x00137FA3
				public JoinProgramSetBuilder<trimTop> trimTop_sheetSection(ProgramSetBuilder<sheetSection> value0)
				{
					return JoinProgramSetBuilder<trimTop>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.trimTop_sheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005EC9 RID: 24265 RVA: 0x00139DD4 File Offset: 0x00137FD4
				public JoinProgramSetBuilder<sheetSection> sheetSection_horizontalSheetSection(ProgramSetBuilder<horizontalSheetSection> value0)
				{
					return JoinProgramSetBuilder<sheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.sheetSection_horizontalSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECA RID: 24266 RVA: 0x00139E05 File Offset: 0x00138005
				public JoinProgramSetBuilder<horizontalSheetSection> horizontalSheetSection_verticalSheetSection(ProgramSetBuilder<verticalSheetSection> value0)
				{
					return JoinProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.horizontalSheetSection_verticalSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECB RID: 24267 RVA: 0x00139E36 File Offset: 0x00138036
				public JoinProgramSetBuilder<verticalSheetSection> verticalSheetSection_uncleanedSheetSection(ProgramSetBuilder<uncleanedSheetSection> value0)
				{
					return JoinProgramSetBuilder<verticalSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.verticalSheetSection_uncleanedSheetSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECC RID: 24268 RVA: 0x00139E67 File Offset: 0x00138067
				public JoinProgramSetBuilder<uncleanedSheetSection> uncleanedSheetSection_wholeSheet(ProgramSetBuilder<wholeSheet> value0)
				{
					return JoinProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.uncleanedSheetSection_wholeSheet, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECD RID: 24269 RVA: 0x00139E98 File Offset: 0x00138098
				public JoinProgramSetBuilder<wholeSheet> wholeSheet_wholeSheetFull(ProgramSetBuilder<wholeSheetFull> value0)
				{
					return JoinProgramSetBuilder<wholeSheet>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.wholeSheet_wholeSheetFull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECE RID: 24270 RVA: 0x00139EC9 File Offset: 0x001380C9
				public JoinProgramSetBuilder<mProgram> mProgram_mTable(ProgramSetBuilder<mTable> value0)
				{
					return JoinProgramSetBuilder<mProgram>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.mProgram_mTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ECF RID: 24271 RVA: 0x00139EFA File Offset: 0x001380FA
				public JoinProgramSetBuilder<title> title_above(ProgramSetBuilder<above> value0)
				{
					return JoinProgramSetBuilder<title>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.title_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ED0 RID: 24272 RVA: 0x00139F2B File Offset: 0x0013812B
				public JoinProgramSetBuilder<aboveOrLeftmost> aboveOrLeftmost_above(ProgramSetBuilder<above> value0)
				{
					return JoinProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.aboveOrLeftmost_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ED1 RID: 24273 RVA: 0x00139F5C File Offset: 0x0013815C
				public JoinProgramSetBuilder<aboveOrOutput> aboveOrOutput_aboveOrHeader(ProgramSetBuilder<aboveOrHeader> value0)
				{
					return JoinProgramSetBuilder<aboveOrOutput>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.aboveOrOutput_aboveOrHeader, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ED2 RID: 24274 RVA: 0x00139F8D File Offset: 0x0013818D
				public JoinProgramSetBuilder<aboveOrOutput> aboveOrOutput_titleOf(ProgramSetBuilder<titleOf> value0)
				{
					return JoinProgramSetBuilder<aboveOrOutput>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.aboveOrOutput_titleOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ED3 RID: 24275 RVA: 0x00139FBE File Offset: 0x001381BE
				public JoinProgramSetBuilder<aboveOrHeader> aboveOrHeader_above(ProgramSetBuilder<above> value0)
				{
					return JoinProgramSetBuilder<aboveOrHeader>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.aboveOrHeader_above, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06005ED4 RID: 24276 RVA: 0x00139FEF File Offset: 0x001381EF
				public JoinProgramSetBuilder<aboveOrHeader> aboveOrHeader_headerSection(ProgramSetBuilder<headerSection> value0)
				{
					return JoinProgramSetBuilder<aboveOrHeader>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.aboveOrHeader_headerSection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002B5D RID: 11101
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000E02 RID: 3586
			public class Casts
			{
				// Token: 0x06005ED5 RID: 24277 RVA: 0x0013A020 File Offset: 0x00138220
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06005ED6 RID: 24278 RVA: 0x0013A030 File Offset: 0x00138230
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x06005ED7 RID: 24279 RVA: 0x0013A088 File Offset: 0x00138288
				public ProgramSetBuilder<trim> trim(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.trim)
					{
						string text = "set";
						string text2 = "expected program set for symbol trim but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trim>.CreateUnsafe(set);
				}

				// Token: 0x06005ED8 RID: 24280 RVA: 0x0013A0E0 File Offset: 0x001382E0
				public ProgramSetBuilder<area> area(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.area)
					{
						string text = "set";
						string text2 = "expected program set for symbol area but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.area>.CreateUnsafe(set);
				}

				// Token: 0x06005ED9 RID: 24281 RVA: 0x0013A138 File Offset: 0x00138338
				public ProgramSetBuilder<trimLeft> trimLeft(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.trimLeft)
					{
						string text = "set";
						string text2 = "expected program set for symbol trimLeft but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimLeft>.CreateUnsafe(set);
				}

				// Token: 0x06005EDA RID: 24282 RVA: 0x0013A190 File Offset: 0x00138390
				public ProgramSetBuilder<trimBottom> trimBottom(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.trimBottom)
					{
						string text = "set";
						string text2 = "expected program set for symbol trimBottom but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimBottom>.CreateUnsafe(set);
				}

				// Token: 0x06005EDB RID: 24283 RVA: 0x0013A1E8 File Offset: 0x001383E8
				public ProgramSetBuilder<trimTop> trimTop(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.trimTop)
					{
						string text = "set";
						string text2 = "expected program set for symbol trimTop but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.trimTop>.CreateUnsafe(set);
				}

				// Token: 0x06005EDC RID: 24284 RVA: 0x0013A240 File Offset: 0x00138440
				public ProgramSetBuilder<sheetSection> sheetSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sheetSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol sheetSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EDD RID: 24285 RVA: 0x0013A298 File Offset: 0x00138498
				public ProgramSetBuilder<horizontalSheetSection> horizontalSheetSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.horizontalSheetSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol horizontalSheetSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EDE RID: 24286 RVA: 0x0013A2F0 File Offset: 0x001384F0
				public ProgramSetBuilder<verticalSheetSection> verticalSheetSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.verticalSheetSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol verticalSheetSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EDF RID: 24287 RVA: 0x0013A348 File Offset: 0x00138548
				public ProgramSetBuilder<uncleanedSheetSection> uncleanedSheetSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.uncleanedSheetSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol uncleanedSheetSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.uncleanedSheetSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EE0 RID: 24288 RVA: 0x0013A3A0 File Offset: 0x001385A0
				public ProgramSetBuilder<wholeSheet> wholeSheet(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.wholeSheet)
					{
						string text = "set";
						string text2 = "expected program set for symbol wholeSheet but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheet>.CreateUnsafe(set);
				}

				// Token: 0x06005EE1 RID: 24289 RVA: 0x0013A3F8 File Offset: 0x001385F8
				public ProgramSetBuilder<wholeSheetFull> wholeSheetFull(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.wholeSheetFull)
					{
						string text = "set";
						string text2 = "expected program set for symbol wholeSheetFull but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.wholeSheetFull>.CreateUnsafe(set);
				}

				// Token: 0x06005EE2 RID: 24290 RVA: 0x0013A450 File Offset: 0x00138650
				public ProgramSetBuilder<sheet> sheet(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sheet)
					{
						string text = "set";
						string text2 = "expected program set for symbol sheet but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheet>.CreateUnsafe(set);
				}

				// Token: 0x06005EE3 RID: 24291 RVA: 0x0013A4A8 File Offset: 0x001386A8
				public ProgramSetBuilder<horizontalSheetSplits> horizontalSheetSplits(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.horizontalSheetSplits)
					{
						string text = "set";
						string text2 = "expected program set for symbol horizontalSheetSplits but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.horizontalSheetSplits>.CreateUnsafe(set);
				}

				// Token: 0x06005EE4 RID: 24292 RVA: 0x0013A500 File Offset: 0x00138700
				public ProgramSetBuilder<verticalSheetSplits> verticalSheetSplits(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.verticalSheetSplits)
					{
						string text = "set";
						string text2 = "expected program set for symbol verticalSheetSplits but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.verticalSheetSplits>.CreateUnsafe(set);
				}

				// Token: 0x06005EE5 RID: 24293 RVA: 0x0013A558 File Offset: 0x00138758
				public ProgramSetBuilder<sheetSplits> sheetSplits(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sheetSplits)
					{
						string text = "set";
						string text2 = "expected program set for symbol sheetSplits but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.sheetSplits>.CreateUnsafe(set);
				}

				// Token: 0x06005EE6 RID: 24294 RVA: 0x0013A5B0 File Offset: 0x001387B0
				public ProgramSetBuilder<index> index(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.index)
					{
						string text = "set";
						string text2 = "expected program set for symbol index but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.index>.CreateUnsafe(set);
				}

				// Token: 0x06005EE7 RID: 24295 RVA: 0x0013A608 File Offset: 0x00138808
				public ProgramSetBuilder<rangeName> rangeName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rangeName)
					{
						string text = "set";
						string text2 = "expected program set for symbol rangeName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.rangeName>.CreateUnsafe(set);
				}

				// Token: 0x06005EE8 RID: 24296 RVA: 0x0013A660 File Offset: 0x00138860
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06005EE9 RID: 24297 RVA: 0x0013A6B8 File Offset: 0x001388B8
				public ProgramSetBuilder<splitMode> splitMode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitMode)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitMode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitMode>.CreateUnsafe(set);
				}

				// Token: 0x06005EEA RID: 24298 RVA: 0x0013A710 File Offset: 0x00138910
				public ProgramSetBuilder<styleFilter> styleFilter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.styleFilter)
					{
						string text = "set";
						string text2 = "expected program set for symbol styleFilter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.styleFilter>.CreateUnsafe(set);
				}

				// Token: 0x06005EEB RID: 24299 RVA: 0x0013A768 File Offset: 0x00138968
				public ProgramSetBuilder<mProgram> mProgram(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mProgram)
					{
						string text = "set";
						string text2 = "expected program set for symbol mProgram but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mProgram>.CreateUnsafe(set);
				}

				// Token: 0x06005EEC RID: 24300 RVA: 0x0013A7C0 File Offset: 0x001389C0
				public ProgramSetBuilder<mTable> mTable(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mTable)
					{
						string text = "set";
						string text2 = "expected program set for symbol mTable but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mTable>.CreateUnsafe(set);
				}

				// Token: 0x06005EED RID: 24301 RVA: 0x0013A818 File Offset: 0x00138A18
				public ProgramSetBuilder<mSection> mSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol mSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.mSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EEE RID: 24302 RVA: 0x0013A870 File Offset: 0x00138A70
				public ProgramSetBuilder<withoutFormatting> withoutFormatting(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.withoutFormatting)
					{
						string text = "set";
						string text2 = "expected program set for symbol withoutFormatting but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.withoutFormatting>.CreateUnsafe(set);
				}

				// Token: 0x06005EEF RID: 24303 RVA: 0x0013A8C8 File Offset: 0x00138AC8
				public ProgramSetBuilder<startTitle> startTitle(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.startTitle)
					{
						string text = "set";
						string text2 = "expected program set for symbol startTitle but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.startTitle>.CreateUnsafe(set);
				}

				// Token: 0x06005EF0 RID: 24304 RVA: 0x0013A920 File Offset: 0x00138B20
				public ProgramSetBuilder<title> title(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.title)
					{
						string text = "set";
						string text2 = "expected program set for symbol title but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.title>.CreateUnsafe(set);
				}

				// Token: 0x06005EF1 RID: 24305 RVA: 0x0013A978 File Offset: 0x00138B78
				public ProgramSetBuilder<aboveOrLeftmost> aboveOrLeftmost(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.aboveOrLeftmost)
					{
						string text = "set";
						string text2 = "expected program set for symbol aboveOrLeftmost but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrLeftmost>.CreateUnsafe(set);
				}

				// Token: 0x06005EF2 RID: 24306 RVA: 0x0013A9D0 File Offset: 0x00138BD0
				public ProgramSetBuilder<aboveOrOutput> aboveOrOutput(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.aboveOrOutput)
					{
						string text = "set";
						string text2 = "expected program set for symbol aboveOrOutput but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrOutput>.CreateUnsafe(set);
				}

				// Token: 0x06005EF3 RID: 24307 RVA: 0x0013AA28 File Offset: 0x00138C28
				public ProgramSetBuilder<aboveOrHeader> aboveOrHeader(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.aboveOrHeader)
					{
						string text = "set";
						string text2 = "expected program set for symbol aboveOrHeader but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.aboveOrHeader>.CreateUnsafe(set);
				}

				// Token: 0x06005EF4 RID: 24308 RVA: 0x0013AA80 File Offset: 0x00138C80
				public ProgramSetBuilder<headerSection> headerSection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.headerSection)
					{
						string text = "set";
						string text2 = "expected program set for symbol headerSection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.headerSection>.CreateUnsafe(set);
				}

				// Token: 0x06005EF5 RID: 24309 RVA: 0x0013AAD8 File Offset: 0x00138CD8
				public ProgramSetBuilder<splitForTitle> splitForTitle(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.splitForTitle)
					{
						string text = "set";
						string text2 = "expected program set for symbol splitForTitle but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.splitForTitle>.CreateUnsafe(set);
				}

				// Token: 0x06005EF6 RID: 24310 RVA: 0x0013AB30 File Offset: 0x00138D30
				public ProgramSetBuilder<above> above(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.above)
					{
						string text = "set";
						string text2 = "expected program set for symbol above but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.above>.CreateUnsafe(set);
				}

				// Token: 0x06005EF7 RID: 24311 RVA: 0x0013AB88 File Offset: 0x00138D88
				public ProgramSetBuilder<titleOf> titleOf(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.titleOf)
					{
						string text = "set";
						string text2 = "expected program set for symbol titleOf but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleOf>.CreateUnsafe(set);
				}

				// Token: 0x06005EF8 RID: 24312 RVA: 0x0013ABE0 File Offset: 0x00138DE0
				public ProgramSetBuilder<titleAboveMode> titleAboveMode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.titleAboveMode)
					{
						string text = "set";
						string text2 = "expected program set for symbol titleAboveMode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes.titleAboveMode>.CreateUnsafe(set);
				}

				// Token: 0x04002B5E RID: 11102
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
