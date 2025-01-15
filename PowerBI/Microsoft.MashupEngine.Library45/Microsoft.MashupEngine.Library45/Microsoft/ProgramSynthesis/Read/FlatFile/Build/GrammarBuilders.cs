using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build
{
	// Token: 0x0200125C RID: 4700
	public class GrammarBuilders
	{
		// Token: 0x06008D4D RID: 36173 RVA: 0x001DE6B4 File Offset: 0x001DC8B4
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06008D4E RID: 36174 RVA: 0x001DE6E0 File Offset: 0x001DC8E0
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06008D4F RID: 36175 RVA: 0x001DE6ED File Offset: 0x001DC8ED
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06008D50 RID: 36176 RVA: 0x001DE6FA File Offset: 0x001DC8FA
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06008D51 RID: 36177 RVA: 0x001DE707 File Offset: 0x001DC907
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06008D52 RID: 36178 RVA: 0x001DE714 File Offset: 0x001DC914
		// (set) Token: 0x06008D53 RID: 36179 RVA: 0x001DE71C File Offset: 0x001DC91C
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06008D54 RID: 36180 RVA: 0x001DE725 File Offset: 0x001DC925
		// (set) Token: 0x06008D55 RID: 36181 RVA: 0x001DE72D File Offset: 0x001DC92D
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06008D56 RID: 36182 RVA: 0x001DE738 File Offset: 0x001DC938
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

		// Token: 0x040039E3 RID: 14819
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x040039E4 RID: 14820
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x040039E5 RID: 14821
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x040039E6 RID: 14822
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x040039E7 RID: 14823
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x0200125D RID: 4701
		public class GrammarSymbols
		{
			// Token: 0x17001841 RID: 6209
			// (get) Token: 0x06008D58 RID: 36184 RVA: 0x001DE7E3 File Offset: 0x001DC9E3
			// (set) Token: 0x06008D59 RID: 36185 RVA: 0x001DE7EB File Offset: 0x001DC9EB
			public Symbol file { get; private set; }

			// Token: 0x17001842 RID: 6210
			// (get) Token: 0x06008D5A RID: 36186 RVA: 0x001DE7F4 File Offset: 0x001DC9F4
			// (set) Token: 0x06008D5B RID: 36187 RVA: 0x001DE7FC File Offset: 0x001DC9FC
			public Symbol columnNames { get; private set; }

			// Token: 0x17001843 RID: 6211
			// (get) Token: 0x06008D5C RID: 36188 RVA: 0x001DE805 File Offset: 0x001DCA05
			// (set) Token: 0x06008D5D RID: 36189 RVA: 0x001DE80D File Offset: 0x001DCA0D
			public Symbol skip { get; private set; }

			// Token: 0x17001844 RID: 6212
			// (get) Token: 0x06008D5E RID: 36190 RVA: 0x001DE816 File Offset: 0x001DCA16
			// (set) Token: 0x06008D5F RID: 36191 RVA: 0x001DE81E File Offset: 0x001DCA1E
			public Symbol skipFooter { get; private set; }

			// Token: 0x17001845 RID: 6213
			// (get) Token: 0x06008D60 RID: 36192 RVA: 0x001DE827 File Offset: 0x001DCA27
			// (set) Token: 0x06008D61 RID: 36193 RVA: 0x001DE82F File Offset: 0x001DCA2F
			public Symbol delimiter { get; private set; }

			// Token: 0x17001846 RID: 6214
			// (get) Token: 0x06008D62 RID: 36194 RVA: 0x001DE838 File Offset: 0x001DCA38
			// (set) Token: 0x06008D63 RID: 36195 RVA: 0x001DE840 File Offset: 0x001DCA40
			public Symbol fieldPositions { get; private set; }

			// Token: 0x17001847 RID: 6215
			// (get) Token: 0x06008D64 RID: 36196 RVA: 0x001DE849 File Offset: 0x001DCA49
			// (set) Token: 0x06008D65 RID: 36197 RVA: 0x001DE851 File Offset: 0x001DCA51
			public Symbol filterEmptyLines { get; private set; }

			// Token: 0x17001848 RID: 6216
			// (get) Token: 0x06008D66 RID: 36198 RVA: 0x001DE85A File Offset: 0x001DCA5A
			// (set) Token: 0x06008D67 RID: 36199 RVA: 0x001DE862 File Offset: 0x001DCA62
			public Symbol commentStr { get; private set; }

			// Token: 0x17001849 RID: 6217
			// (get) Token: 0x06008D68 RID: 36200 RVA: 0x001DE86B File Offset: 0x001DCA6B
			// (set) Token: 0x06008D69 RID: 36201 RVA: 0x001DE873 File Offset: 0x001DCA73
			public Symbol quoteChar { get; private set; }

			// Token: 0x1700184A RID: 6218
			// (get) Token: 0x06008D6A RID: 36202 RVA: 0x001DE87C File Offset: 0x001DCA7C
			// (set) Token: 0x06008D6B RID: 36203 RVA: 0x001DE884 File Offset: 0x001DCA84
			public Symbol escapeChar { get; private set; }

			// Token: 0x1700184B RID: 6219
			// (get) Token: 0x06008D6C RID: 36204 RVA: 0x001DE88D File Offset: 0x001DCA8D
			// (set) Token: 0x06008D6D RID: 36205 RVA: 0x001DE895 File Offset: 0x001DCA95
			public Symbol doubleQuote { get; private set; }

			// Token: 0x1700184C RID: 6220
			// (get) Token: 0x06008D6E RID: 36206 RVA: 0x001DE89E File Offset: 0x001DCA9E
			// (set) Token: 0x06008D6F RID: 36207 RVA: 0x001DE8A6 File Offset: 0x001DCAA6
			public Symbol readFlatFile { get; private set; }

			// Token: 0x1700184D RID: 6221
			// (get) Token: 0x06008D70 RID: 36208 RVA: 0x001DE8AF File Offset: 0x001DCAAF
			// (set) Token: 0x06008D71 RID: 36209 RVA: 0x001DE8B7 File Offset: 0x001DCAB7
			public Symbol eText { get; private set; }

			// Token: 0x1700184E RID: 6222
			// (get) Token: 0x06008D72 RID: 36210 RVA: 0x001DE8C0 File Offset: 0x001DCAC0
			// (set) Token: 0x06008D73 RID: 36211 RVA: 0x001DE8C8 File Offset: 0x001DCAC8
			public Symbol fileRegion { get; private set; }

			// Token: 0x1700184F RID: 6223
			// (get) Token: 0x06008D74 RID: 36212 RVA: 0x001DE8D1 File Offset: 0x001DCAD1
			// (set) Token: 0x06008D75 RID: 36213 RVA: 0x001DE8D9 File Offset: 0x001DCAD9
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17001850 RID: 6224
			// (get) Token: 0x06008D76 RID: 36214 RVA: 0x001DE8E2 File Offset: 0x001DCAE2
			// (set) Token: 0x06008D77 RID: 36215 RVA: 0x001DE8EA File Offset: 0x001DCAEA
			public Symbol _LetB1 { get; private set; }

			// Token: 0x06008D78 RID: 36216 RVA: 0x001DE8F4 File Offset: 0x001DCAF4
			public GrammarSymbols(Grammar grammar)
			{
				this.file = grammar.Symbol("file");
				this.columnNames = grammar.Symbol("columnNames");
				this.skip = grammar.Symbol("skip");
				this.skipFooter = grammar.Symbol("skipFooter");
				this.delimiter = grammar.Symbol("delimiter");
				this.fieldPositions = grammar.Symbol("fieldPositions");
				this.filterEmptyLines = grammar.Symbol("filterEmptyLines");
				this.commentStr = grammar.Symbol("commentStr");
				this.quoteChar = grammar.Symbol("quoteChar");
				this.escapeChar = grammar.Symbol("escapeChar");
				this.doubleQuote = grammar.Symbol("doubleQuote");
				this.readFlatFile = grammar.Symbol("readFlatFile");
				this.eText = grammar.Symbol("eText");
				this.fileRegion = grammar.Symbol("fileRegion");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
			}
		}

		// Token: 0x0200125E RID: 4702
		public class GrammarRules
		{
			// Token: 0x17001851 RID: 6225
			// (get) Token: 0x06008D79 RID: 36217 RVA: 0x001DEA17 File Offset: 0x001DCC17
			// (set) Token: 0x06008D7A RID: 36218 RVA: 0x001DEA1F File Offset: 0x001DCC1F
			public BlackBoxRule Csv { get; private set; }

			// Token: 0x17001852 RID: 6226
			// (get) Token: 0x06008D7B RID: 36219 RVA: 0x001DEA28 File Offset: 0x001DCC28
			// (set) Token: 0x06008D7C RID: 36220 RVA: 0x001DEA30 File Offset: 0x001DCC30
			public BlackBoxRule Fw { get; private set; }

			// Token: 0x17001853 RID: 6227
			// (get) Token: 0x06008D7D RID: 36221 RVA: 0x001DEA39 File Offset: 0x001DCC39
			// (set) Token: 0x06008D7E RID: 36222 RVA: 0x001DEA41 File Offset: 0x001DCC41
			public BlackBoxRule StringRegionToStringTable { get; private set; }

			// Token: 0x17001854 RID: 6228
			// (get) Token: 0x06008D7F RID: 36223 RVA: 0x001DEA4A File Offset: 0x001DCC4A
			// (set) Token: 0x06008D80 RID: 36224 RVA: 0x001DEA52 File Offset: 0x001DCC52
			public BlackBoxRule CreateStringRegion { get; private set; }

			// Token: 0x17001855 RID: 6229
			// (get) Token: 0x06008D81 RID: 36225 RVA: 0x001DEA5B File Offset: 0x001DCC5B
			// (set) Token: 0x06008D82 RID: 36226 RVA: 0x001DEA63 File Offset: 0x001DCC63
			public ConversionRule ETextOutput { get; private set; }

			// Token: 0x17001856 RID: 6230
			// (get) Token: 0x06008D83 RID: 36227 RVA: 0x001DEA6C File Offset: 0x001DCC6C
			// (set) Token: 0x06008D84 RID: 36228 RVA: 0x001DEA74 File Offset: 0x001DCC74
			public LetRule LetEText { get; private set; }

			// Token: 0x06008D85 RID: 36229 RVA: 0x001DEA80 File Offset: 0x001DCC80
			public GrammarRules(Grammar grammar)
			{
				this.Csv = (BlackBoxRule)grammar.Rule("Csv");
				this.Fw = (BlackBoxRule)grammar.Rule("Fw");
				this.StringRegionToStringTable = (BlackBoxRule)grammar.Rule("StringRegionToStringTable");
				this.CreateStringRegion = (BlackBoxRule)grammar.Rule("CreateStringRegion");
				this.ETextOutput = (ConversionRule)grammar.Rule("ETextOutput");
				this.LetEText = (LetRule)grammar.Rule("LetEText");
			}
		}

		// Token: 0x0200125F RID: 4703
		public class GrammarUnnamedConversions
		{
			// Token: 0x06008D86 RID: 36230 RVA: 0x00002130 File Offset: 0x00000330
			public GrammarUnnamedConversions(Grammar grammar)
			{
			}
		}

		// Token: 0x02001260 RID: 4704
		public class GrammarHoles
		{
			// Token: 0x17001857 RID: 6231
			// (get) Token: 0x06008D87 RID: 36231 RVA: 0x001DEB17 File Offset: 0x001DCD17
			// (set) Token: 0x06008D88 RID: 36232 RVA: 0x001DEB1F File Offset: 0x001DCD1F
			public Hole file { get; private set; }

			// Token: 0x17001858 RID: 6232
			// (get) Token: 0x06008D89 RID: 36233 RVA: 0x001DEB28 File Offset: 0x001DCD28
			// (set) Token: 0x06008D8A RID: 36234 RVA: 0x001DEB30 File Offset: 0x001DCD30
			public Hole columnNames { get; private set; }

			// Token: 0x17001859 RID: 6233
			// (get) Token: 0x06008D8B RID: 36235 RVA: 0x001DEB39 File Offset: 0x001DCD39
			// (set) Token: 0x06008D8C RID: 36236 RVA: 0x001DEB41 File Offset: 0x001DCD41
			public Hole skip { get; private set; }

			// Token: 0x1700185A RID: 6234
			// (get) Token: 0x06008D8D RID: 36237 RVA: 0x001DEB4A File Offset: 0x001DCD4A
			// (set) Token: 0x06008D8E RID: 36238 RVA: 0x001DEB52 File Offset: 0x001DCD52
			public Hole skipFooter { get; private set; }

			// Token: 0x1700185B RID: 6235
			// (get) Token: 0x06008D8F RID: 36239 RVA: 0x001DEB5B File Offset: 0x001DCD5B
			// (set) Token: 0x06008D90 RID: 36240 RVA: 0x001DEB63 File Offset: 0x001DCD63
			public Hole delimiter { get; private set; }

			// Token: 0x1700185C RID: 6236
			// (get) Token: 0x06008D91 RID: 36241 RVA: 0x001DEB6C File Offset: 0x001DCD6C
			// (set) Token: 0x06008D92 RID: 36242 RVA: 0x001DEB74 File Offset: 0x001DCD74
			public Hole fieldPositions { get; private set; }

			// Token: 0x1700185D RID: 6237
			// (get) Token: 0x06008D93 RID: 36243 RVA: 0x001DEB7D File Offset: 0x001DCD7D
			// (set) Token: 0x06008D94 RID: 36244 RVA: 0x001DEB85 File Offset: 0x001DCD85
			public Hole filterEmptyLines { get; private set; }

			// Token: 0x1700185E RID: 6238
			// (get) Token: 0x06008D95 RID: 36245 RVA: 0x001DEB8E File Offset: 0x001DCD8E
			// (set) Token: 0x06008D96 RID: 36246 RVA: 0x001DEB96 File Offset: 0x001DCD96
			public Hole commentStr { get; private set; }

			// Token: 0x1700185F RID: 6239
			// (get) Token: 0x06008D97 RID: 36247 RVA: 0x001DEB9F File Offset: 0x001DCD9F
			// (set) Token: 0x06008D98 RID: 36248 RVA: 0x001DEBA7 File Offset: 0x001DCDA7
			public Hole quoteChar { get; private set; }

			// Token: 0x17001860 RID: 6240
			// (get) Token: 0x06008D99 RID: 36249 RVA: 0x001DEBB0 File Offset: 0x001DCDB0
			// (set) Token: 0x06008D9A RID: 36250 RVA: 0x001DEBB8 File Offset: 0x001DCDB8
			public Hole escapeChar { get; private set; }

			// Token: 0x17001861 RID: 6241
			// (get) Token: 0x06008D9B RID: 36251 RVA: 0x001DEBC1 File Offset: 0x001DCDC1
			// (set) Token: 0x06008D9C RID: 36252 RVA: 0x001DEBC9 File Offset: 0x001DCDC9
			public Hole doubleQuote { get; private set; }

			// Token: 0x17001862 RID: 6242
			// (get) Token: 0x06008D9D RID: 36253 RVA: 0x001DEBD2 File Offset: 0x001DCDD2
			// (set) Token: 0x06008D9E RID: 36254 RVA: 0x001DEBDA File Offset: 0x001DCDDA
			public Hole readFlatFile { get; private set; }

			// Token: 0x17001863 RID: 6243
			// (get) Token: 0x06008D9F RID: 36255 RVA: 0x001DEBE3 File Offset: 0x001DCDE3
			// (set) Token: 0x06008DA0 RID: 36256 RVA: 0x001DEBEB File Offset: 0x001DCDEB
			public Hole eText { get; private set; }

			// Token: 0x17001864 RID: 6244
			// (get) Token: 0x06008DA1 RID: 36257 RVA: 0x001DEBF4 File Offset: 0x001DCDF4
			// (set) Token: 0x06008DA2 RID: 36258 RVA: 0x001DEBFC File Offset: 0x001DCDFC
			public Hole fileRegion { get; private set; }

			// Token: 0x17001865 RID: 6245
			// (get) Token: 0x06008DA3 RID: 36259 RVA: 0x001DEC05 File Offset: 0x001DCE05
			// (set) Token: 0x06008DA4 RID: 36260 RVA: 0x001DEC0D File Offset: 0x001DCE0D
			public Hole _LetB0 { get; private set; }

			// Token: 0x17001866 RID: 6246
			// (get) Token: 0x06008DA5 RID: 36261 RVA: 0x001DEC16 File Offset: 0x001DCE16
			// (set) Token: 0x06008DA6 RID: 36262 RVA: 0x001DEC1E File Offset: 0x001DCE1E
			public Hole _LetB1 { get; private set; }

			// Token: 0x06008DA7 RID: 36263 RVA: 0x001DEC28 File Offset: 0x001DCE28
			public GrammarHoles(GrammarBuilders builders)
			{
				this.file = new Hole(builders.Symbol.file, null);
				this.columnNames = new Hole(builders.Symbol.columnNames, null);
				this.skip = new Hole(builders.Symbol.skip, null);
				this.skipFooter = new Hole(builders.Symbol.skipFooter, null);
				this.delimiter = new Hole(builders.Symbol.delimiter, null);
				this.fieldPositions = new Hole(builders.Symbol.fieldPositions, null);
				this.filterEmptyLines = new Hole(builders.Symbol.filterEmptyLines, null);
				this.commentStr = new Hole(builders.Symbol.commentStr, null);
				this.quoteChar = new Hole(builders.Symbol.quoteChar, null);
				this.escapeChar = new Hole(builders.Symbol.escapeChar, null);
				this.doubleQuote = new Hole(builders.Symbol.doubleQuote, null);
				this.readFlatFile = new Hole(builders.Symbol.readFlatFile, null);
				this.eText = new Hole(builders.Symbol.eText, null);
				this.fileRegion = new Hole(builders.Symbol.fileRegion, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
			}
		}

		// Token: 0x02001261 RID: 4705
		public class Nodes
		{
			// Token: 0x06008DA8 RID: 36264 RVA: 0x001DEDAC File Offset: 0x001DCFAC
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

			// Token: 0x17001867 RID: 6247
			// (get) Token: 0x06008DA9 RID: 36265 RVA: 0x001DEE8F File Offset: 0x001DD08F
			// (set) Token: 0x06008DAA RID: 36266 RVA: 0x001DEE97 File Offset: 0x001DD097
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17001868 RID: 6248
			// (get) Token: 0x06008DAB RID: 36267 RVA: 0x001DEEA0 File Offset: 0x001DD0A0
			// (set) Token: 0x06008DAC RID: 36268 RVA: 0x001DEEA8 File Offset: 0x001DD0A8
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17001869 RID: 6249
			// (get) Token: 0x06008DAD RID: 36269 RVA: 0x001DEEB1 File Offset: 0x001DD0B1
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x1700186A RID: 6250
			// (get) Token: 0x06008DAE RID: 36270 RVA: 0x001DEEBE File Offset: 0x001DD0BE
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x1700186B RID: 6251
			// (get) Token: 0x06008DAF RID: 36271 RVA: 0x001DEECB File Offset: 0x001DD0CB
			// (set) Token: 0x06008DB0 RID: 36272 RVA: 0x001DEED3 File Offset: 0x001DD0D3
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x1700186C RID: 6252
			// (get) Token: 0x06008DB1 RID: 36273 RVA: 0x001DEEDC File Offset: 0x001DD0DC
			// (set) Token: 0x06008DB2 RID: 36274 RVA: 0x001DEEE4 File Offset: 0x001DD0E4
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x1700186D RID: 6253
			// (get) Token: 0x06008DB3 RID: 36275 RVA: 0x001DEEED File Offset: 0x001DD0ED
			// (set) Token: 0x06008DB4 RID: 36276 RVA: 0x001DEEF5 File Offset: 0x001DD0F5
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x1700186E RID: 6254
			// (get) Token: 0x06008DB5 RID: 36277 RVA: 0x001DEEFE File Offset: 0x001DD0FE
			// (set) Token: 0x06008DB6 RID: 36278 RVA: 0x001DEF06 File Offset: 0x001DD106
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x1700186F RID: 6255
			// (get) Token: 0x06008DB7 RID: 36279 RVA: 0x001DEF0F File Offset: 0x001DD10F
			// (set) Token: 0x06008DB8 RID: 36280 RVA: 0x001DEF17 File Offset: 0x001DD117
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17001870 RID: 6256
			// (get) Token: 0x06008DB9 RID: 36281 RVA: 0x001DEF20 File Offset: 0x001DD120
			// (set) Token: 0x06008DBA RID: 36282 RVA: 0x001DEF28 File Offset: 0x001DD128
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17001871 RID: 6257
			// (get) Token: 0x06008DBB RID: 36283 RVA: 0x001DEF31 File Offset: 0x001DD131
			// (set) Token: 0x06008DBC RID: 36284 RVA: 0x001DEF39 File Offset: 0x001DD139
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04003A12 RID: 14866
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04003A13 RID: 14867
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001262 RID: 4706
			public class NodeRules
			{
				// Token: 0x06008DBD RID: 36285 RVA: 0x001DEF42 File Offset: 0x001DD142
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008DBE RID: 36286 RVA: 0x001DEF51 File Offset: 0x001DD151
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames columnNames(IReadOnlyList<string> value)
				{
					return new Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames(this._builders, value);
				}

				// Token: 0x06008DBF RID: 36287 RVA: 0x001DEF5F File Offset: 0x001DD15F
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip skip(int value)
				{
					return new Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip(this._builders, value);
				}

				// Token: 0x06008DC0 RID: 36288 RVA: 0x001DEF6D File Offset: 0x001DD16D
				public skipFooter skipFooter(int value)
				{
					return new skipFooter(this._builders, value);
				}

				// Token: 0x06008DC1 RID: 36289 RVA: 0x001DEF7B File Offset: 0x001DD17B
				public delimiter delimiter(string value)
				{
					return new delimiter(this._builders, value);
				}

				// Token: 0x06008DC2 RID: 36290 RVA: 0x001DEF89 File Offset: 0x001DD189
				public fieldPositions fieldPositions(IReadOnlyList<Record<int, int?>> value)
				{
					return new fieldPositions(this._builders, value);
				}

				// Token: 0x06008DC3 RID: 36291 RVA: 0x001DEF97 File Offset: 0x001DD197
				public filterEmptyLines filterEmptyLines(bool value)
				{
					return new filterEmptyLines(this._builders, value);
				}

				// Token: 0x06008DC4 RID: 36292 RVA: 0x001DEFA5 File Offset: 0x001DD1A5
				public commentStr commentStr(Optional<string> value)
				{
					return new commentStr(this._builders, value);
				}

				// Token: 0x06008DC5 RID: 36293 RVA: 0x001DEFB3 File Offset: 0x001DD1B3
				public quoteChar quoteChar(Optional<char> value)
				{
					return new quoteChar(this._builders, value);
				}

				// Token: 0x06008DC6 RID: 36294 RVA: 0x001DEFC1 File Offset: 0x001DD1C1
				public escapeChar escapeChar(Optional<char> value)
				{
					return new escapeChar(this._builders, value);
				}

				// Token: 0x06008DC7 RID: 36295 RVA: 0x001DEFCF File Offset: 0x001DD1CF
				public doubleQuote doubleQuote(bool value)
				{
					return new doubleQuote(this._builders, value);
				}

				// Token: 0x06008DC8 RID: 36296 RVA: 0x001DEFE0 File Offset: 0x001DD1E0
				public readFlatFile Csv(file value0, Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames value1, Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip value2, skipFooter value3, delimiter value4, filterEmptyLines value5, commentStr value6, quoteChar value7, escapeChar value8, doubleQuote value9)
				{
					return new Csv(this._builders, value0, value1, value2, value3, value4, value5, value6, value7, value8, value9);
				}

				// Token: 0x06008DC9 RID: 36297 RVA: 0x001DF00E File Offset: 0x001DD20E
				public readFlatFile Fw(file value0, Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames value1, Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip value2, skipFooter value3, fieldPositions value4, filterEmptyLines value5, commentStr value6)
				{
					return new Fw(this._builders, value0, value1, value2, value3, value4, value5, value6);
				}

				// Token: 0x06008DCA RID: 36298 RVA: 0x001DF02B File Offset: 0x001DD22B
				public readFlatFile StringRegionToStringTable(eText value0)
				{
					return new StringRegionToStringTable(this._builders, value0);
				}

				// Token: 0x06008DCB RID: 36299 RVA: 0x001DF03E File Offset: 0x001DD23E
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 CreateStringRegion(file value0)
				{
					return new CreateStringRegion(this._builders, value0);
				}

				// Token: 0x06008DCC RID: 36300 RVA: 0x001DF051 File Offset: 0x001DD251
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 ETextOutput(output value0)
				{
					return new ETextOutput(this._builders, value0);
				}

				// Token: 0x06008DCD RID: 36301 RVA: 0x001DF064 File Offset: 0x001DD264
				public eText LetEText(Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 value0, Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 value1)
				{
					return new LetEText(this._builders, value0, value1);
				}

				// Token: 0x04003A1B RID: 14875
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001263 RID: 4707
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06008DCE RID: 36302 RVA: 0x001DF078 File Offset: 0x001DD278
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x04003A1C RID: 14876
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001264 RID: 4708
			public class NodeVariables
			{
				// Token: 0x17001872 RID: 6258
				// (get) Token: 0x06008DCF RID: 36303 RVA: 0x001DF087 File Offset: 0x001DD287
				// (set) Token: 0x06008DD0 RID: 36304 RVA: 0x001DF08F File Offset: 0x001DD28F
				public file file { get; private set; }

				// Token: 0x17001873 RID: 6259
				// (get) Token: 0x06008DD1 RID: 36305 RVA: 0x001DF098 File Offset: 0x001DD298
				// (set) Token: 0x06008DD2 RID: 36306 RVA: 0x001DF0A0 File Offset: 0x001DD2A0
				public fileRegion fileRegion { get; private set; }

				// Token: 0x06008DD3 RID: 36307 RVA: 0x001DF0A9 File Offset: 0x001DD2A9
				public NodeVariables(GrammarBuilders builders)
				{
					this.file = new file(builders);
					this.fileRegion = new fileRegion(builders);
				}
			}

			// Token: 0x02001265 RID: 4709
			public class NodeHoles
			{
				// Token: 0x17001874 RID: 6260
				// (get) Token: 0x06008DD4 RID: 36308 RVA: 0x001DF0C9 File Offset: 0x001DD2C9
				// (set) Token: 0x06008DD5 RID: 36309 RVA: 0x001DF0D1 File Offset: 0x001DD2D1
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames columnNames { get; private set; }

				// Token: 0x17001875 RID: 6261
				// (get) Token: 0x06008DD6 RID: 36310 RVA: 0x001DF0DA File Offset: 0x001DD2DA
				// (set) Token: 0x06008DD7 RID: 36311 RVA: 0x001DF0E2 File Offset: 0x001DD2E2
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip skip { get; private set; }

				// Token: 0x17001876 RID: 6262
				// (get) Token: 0x06008DD8 RID: 36312 RVA: 0x001DF0EB File Offset: 0x001DD2EB
				// (set) Token: 0x06008DD9 RID: 36313 RVA: 0x001DF0F3 File Offset: 0x001DD2F3
				public skipFooter skipFooter { get; private set; }

				// Token: 0x17001877 RID: 6263
				// (get) Token: 0x06008DDA RID: 36314 RVA: 0x001DF0FC File Offset: 0x001DD2FC
				// (set) Token: 0x06008DDB RID: 36315 RVA: 0x001DF104 File Offset: 0x001DD304
				public delimiter delimiter { get; private set; }

				// Token: 0x17001878 RID: 6264
				// (get) Token: 0x06008DDC RID: 36316 RVA: 0x001DF10D File Offset: 0x001DD30D
				// (set) Token: 0x06008DDD RID: 36317 RVA: 0x001DF115 File Offset: 0x001DD315
				public fieldPositions fieldPositions { get; private set; }

				// Token: 0x17001879 RID: 6265
				// (get) Token: 0x06008DDE RID: 36318 RVA: 0x001DF11E File Offset: 0x001DD31E
				// (set) Token: 0x06008DDF RID: 36319 RVA: 0x001DF126 File Offset: 0x001DD326
				public filterEmptyLines filterEmptyLines { get; private set; }

				// Token: 0x1700187A RID: 6266
				// (get) Token: 0x06008DE0 RID: 36320 RVA: 0x001DF12F File Offset: 0x001DD32F
				// (set) Token: 0x06008DE1 RID: 36321 RVA: 0x001DF137 File Offset: 0x001DD337
				public commentStr commentStr { get; private set; }

				// Token: 0x1700187B RID: 6267
				// (get) Token: 0x06008DE2 RID: 36322 RVA: 0x001DF140 File Offset: 0x001DD340
				// (set) Token: 0x06008DE3 RID: 36323 RVA: 0x001DF148 File Offset: 0x001DD348
				public quoteChar quoteChar { get; private set; }

				// Token: 0x1700187C RID: 6268
				// (get) Token: 0x06008DE4 RID: 36324 RVA: 0x001DF151 File Offset: 0x001DD351
				// (set) Token: 0x06008DE5 RID: 36325 RVA: 0x001DF159 File Offset: 0x001DD359
				public escapeChar escapeChar { get; private set; }

				// Token: 0x1700187D RID: 6269
				// (get) Token: 0x06008DE6 RID: 36326 RVA: 0x001DF162 File Offset: 0x001DD362
				// (set) Token: 0x06008DE7 RID: 36327 RVA: 0x001DF16A File Offset: 0x001DD36A
				public doubleQuote doubleQuote { get; private set; }

				// Token: 0x1700187E RID: 6270
				// (get) Token: 0x06008DE8 RID: 36328 RVA: 0x001DF173 File Offset: 0x001DD373
				// (set) Token: 0x06008DE9 RID: 36329 RVA: 0x001DF17B File Offset: 0x001DD37B
				public readFlatFile readFlatFile { get; private set; }

				// Token: 0x1700187F RID: 6271
				// (get) Token: 0x06008DEA RID: 36330 RVA: 0x001DF184 File Offset: 0x001DD384
				// (set) Token: 0x06008DEB RID: 36331 RVA: 0x001DF18C File Offset: 0x001DD38C
				public eText eText { get; private set; }

				// Token: 0x17001880 RID: 6272
				// (get) Token: 0x06008DEC RID: 36332 RVA: 0x001DF195 File Offset: 0x001DD395
				// (set) Token: 0x06008DED RID: 36333 RVA: 0x001DF19D File Offset: 0x001DD39D
				public fileRegion fileRegion { get; private set; }

				// Token: 0x17001881 RID: 6273
				// (get) Token: 0x06008DEE RID: 36334 RVA: 0x001DF1A6 File Offset: 0x001DD3A6
				// (set) Token: 0x06008DEF RID: 36335 RVA: 0x001DF1AE File Offset: 0x001DD3AE
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 _LetB0 { get; private set; }

				// Token: 0x17001882 RID: 6274
				// (get) Token: 0x06008DF0 RID: 36336 RVA: 0x001DF1B7 File Offset: 0x001DD3B7
				// (set) Token: 0x06008DF1 RID: 36337 RVA: 0x001DF1BF File Offset: 0x001DD3BF
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 _LetB1 { get; private set; }

				// Token: 0x06008DF2 RID: 36338 RVA: 0x001DF1C8 File Offset: 0x001DD3C8
				public NodeHoles(GrammarBuilders builders)
				{
					this.columnNames = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateHole(builders, null);
					this.skip = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateHole(builders, null);
					this.skipFooter = skipFooter.CreateHole(builders, null);
					this.delimiter = delimiter.CreateHole(builders, null);
					this.fieldPositions = fieldPositions.CreateHole(builders, null);
					this.filterEmptyLines = filterEmptyLines.CreateHole(builders, null);
					this.commentStr = commentStr.CreateHole(builders, null);
					this.quoteChar = quoteChar.CreateHole(builders, null);
					this.escapeChar = escapeChar.CreateHole(builders, null);
					this.doubleQuote = doubleQuote.CreateHole(builders, null);
					this.readFlatFile = readFlatFile.CreateHole(builders, null);
					this.eText = eText.CreateHole(builders, null);
					this.fileRegion = fileRegion.CreateHole(builders, null);
					this._LetB0 = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateHole(builders, null);
					this._LetB1 = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateHole(builders, null);
				}
			}

			// Token: 0x02001266 RID: 4710
			public class NodeUnsafe
			{
				// Token: 0x06008DF3 RID: 36339 RVA: 0x001DF29E File Offset: 0x001DD49E
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateUnsafe(node);
				}

				// Token: 0x06008DF4 RID: 36340 RVA: 0x001DF2A6 File Offset: 0x001DD4A6
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateUnsafe(node);
				}

				// Token: 0x06008DF5 RID: 36341 RVA: 0x001DF2AE File Offset: 0x001DD4AE
				public skipFooter skipFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter.CreateUnsafe(node);
				}

				// Token: 0x06008DF6 RID: 36342 RVA: 0x001DF2B6 File Offset: 0x001DD4B6
				public delimiter delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter.CreateUnsafe(node);
				}

				// Token: 0x06008DF7 RID: 36343 RVA: 0x001DF2BE File Offset: 0x001DD4BE
				public fieldPositions fieldPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions.CreateUnsafe(node);
				}

				// Token: 0x06008DF8 RID: 36344 RVA: 0x001DF2C6 File Offset: 0x001DD4C6
				public filterEmptyLines filterEmptyLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines.CreateUnsafe(node);
				}

				// Token: 0x06008DF9 RID: 36345 RVA: 0x001DF2CE File Offset: 0x001DD4CE
				public commentStr commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr.CreateUnsafe(node);
				}

				// Token: 0x06008DFA RID: 36346 RVA: 0x001DF2D6 File Offset: 0x001DD4D6
				public quoteChar quoteChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar.CreateUnsafe(node);
				}

				// Token: 0x06008DFB RID: 36347 RVA: 0x001DF2DE File Offset: 0x001DD4DE
				public escapeChar escapeChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar.CreateUnsafe(node);
				}

				// Token: 0x06008DFC RID: 36348 RVA: 0x001DF2E6 File Offset: 0x001DD4E6
				public doubleQuote doubleQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote.CreateUnsafe(node);
				}

				// Token: 0x06008DFD RID: 36349 RVA: 0x001DF2EE File Offset: 0x001DD4EE
				public readFlatFile readFlatFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile.CreateUnsafe(node);
				}

				// Token: 0x06008DFE RID: 36350 RVA: 0x001DF2F6 File Offset: 0x001DD4F6
				public eText eText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText.CreateUnsafe(node);
				}

				// Token: 0x06008DFF RID: 36351 RVA: 0x001DF2FE File Offset: 0x001DD4FE
				public fileRegion fileRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion.CreateUnsafe(node);
				}

				// Token: 0x06008E00 RID: 36352 RVA: 0x001DF306 File Offset: 0x001DD506
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x06008E01 RID: 36353 RVA: 0x001DF30E File Offset: 0x001DD50E
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}
			}

			// Token: 0x02001267 RID: 4711
			public class NodeCast
			{
				// Token: 0x06008E03 RID: 36355 RVA: 0x001DF316 File Offset: 0x001DD516
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E04 RID: 36356 RVA: 0x001DF328 File Offset: 0x001DD528
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames columnNames(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames? columnNames = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnNames but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnNames.Value;
				}

				// Token: 0x06008E05 RID: 36357 RVA: 0x001DF37C File Offset: 0x001DD57C
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip skip(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip? skip = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skip but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skip.Value;
				}

				// Token: 0x06008E06 RID: 36358 RVA: 0x001DF3D0 File Offset: 0x001DD5D0
				public skipFooter skipFooter(ProgramNode node)
				{
					skipFooter? skipFooter = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter.CreateSafe(this._builders, node);
					if (skipFooter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skipFooter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skipFooter.Value;
				}

				// Token: 0x06008E07 RID: 36359 RVA: 0x001DF424 File Offset: 0x001DD624
				public delimiter delimiter(ProgramNode node)
				{
					delimiter? delimiter = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol delimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return delimiter.Value;
				}

				// Token: 0x06008E08 RID: 36360 RVA: 0x001DF478 File Offset: 0x001DD678
				public fieldPositions fieldPositions(ProgramNode node)
				{
					fieldPositions? fieldPositions = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions.CreateSafe(this._builders, node);
					if (fieldPositions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fieldPositions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldPositions.Value;
				}

				// Token: 0x06008E09 RID: 36361 RVA: 0x001DF4CC File Offset: 0x001DD6CC
				public filterEmptyLines filterEmptyLines(ProgramNode node)
				{
					filterEmptyLines? filterEmptyLines = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines.CreateSafe(this._builders, node);
					if (filterEmptyLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterEmptyLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterEmptyLines.Value;
				}

				// Token: 0x06008E0A RID: 36362 RVA: 0x001DF520 File Offset: 0x001DD720
				public commentStr commentStr(ProgramNode node)
				{
					commentStr? commentStr = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
					if (commentStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol commentStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return commentStr.Value;
				}

				// Token: 0x06008E0B RID: 36363 RVA: 0x001DF574 File Offset: 0x001DD774
				public quoteChar quoteChar(ProgramNode node)
				{
					quoteChar? quoteChar = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar.CreateSafe(this._builders, node);
					if (quoteChar == null)
					{
						string text = "node";
						string text2 = "expected node for symbol quoteChar but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return quoteChar.Value;
				}

				// Token: 0x06008E0C RID: 36364 RVA: 0x001DF5C8 File Offset: 0x001DD7C8
				public escapeChar escapeChar(ProgramNode node)
				{
					escapeChar? escapeChar = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar.CreateSafe(this._builders, node);
					if (escapeChar == null)
					{
						string text = "node";
						string text2 = "expected node for symbol escapeChar but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return escapeChar.Value;
				}

				// Token: 0x06008E0D RID: 36365 RVA: 0x001DF61C File Offset: 0x001DD81C
				public doubleQuote doubleQuote(ProgramNode node)
				{
					doubleQuote? doubleQuote = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote.CreateSafe(this._builders, node);
					if (doubleQuote == null)
					{
						string text = "node";
						string text2 = "expected node for symbol doubleQuote but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return doubleQuote.Value;
				}

				// Token: 0x06008E0E RID: 36366 RVA: 0x001DF670 File Offset: 0x001DD870
				public readFlatFile readFlatFile(ProgramNode node)
				{
					readFlatFile? readFlatFile = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile.CreateSafe(this._builders, node);
					if (readFlatFile == null)
					{
						string text = "node";
						string text2 = "expected node for symbol readFlatFile but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return readFlatFile.Value;
				}

				// Token: 0x06008E0F RID: 36367 RVA: 0x001DF6C4 File Offset: 0x001DD8C4
				public eText eText(ProgramNode node)
				{
					eText? eText = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText.CreateSafe(this._builders, node);
					if (eText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol eText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return eText.Value;
				}

				// Token: 0x06008E10 RID: 36368 RVA: 0x001DF718 File Offset: 0x001DD918
				public fileRegion fileRegion(ProgramNode node)
				{
					fileRegion? fileRegion = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion.CreateSafe(this._builders, node);
					if (fileRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fileRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fileRegion.Value;
				}

				// Token: 0x06008E11 RID: 36369 RVA: 0x001DF76C File Offset: 0x001DD96C
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06008E12 RID: 36370 RVA: 0x001DF7C0 File Offset: 0x001DD9C0
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 _LetB1(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1? letB = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x04003A2E RID: 14894
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001268 RID: 4712
			public class RuleCast
			{
				// Token: 0x06008E13 RID: 36371 RVA: 0x001DF811 File Offset: 0x001DDA11
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E14 RID: 36372 RVA: 0x001DF820 File Offset: 0x001DDA20
				public Csv Csv(ProgramNode node)
				{
					Csv? csv = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Csv.CreateSafe(this._builders, node);
					if (csv == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Csv but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return csv.Value;
				}

				// Token: 0x06008E15 RID: 36373 RVA: 0x001DF874 File Offset: 0x001DDA74
				public Fw Fw(ProgramNode node)
				{
					Fw? fw = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Fw.CreateSafe(this._builders, node);
					if (fw == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Fw but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fw.Value;
				}

				// Token: 0x06008E16 RID: 36374 RVA: 0x001DF8C8 File Offset: 0x001DDAC8
				public StringRegionToStringTable StringRegionToStringTable(ProgramNode node)
				{
					StringRegionToStringTable? stringRegionToStringTable = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.StringRegionToStringTable.CreateSafe(this._builders, node);
					if (stringRegionToStringTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StringRegionToStringTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return stringRegionToStringTable.Value;
				}

				// Token: 0x06008E17 RID: 36375 RVA: 0x001DF91C File Offset: 0x001DDB1C
				public CreateStringRegion CreateStringRegion(ProgramNode node)
				{
					CreateStringRegion? createStringRegion = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.CreateStringRegion.CreateSafe(this._builders, node);
					if (createStringRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol CreateStringRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return createStringRegion.Value;
				}

				// Token: 0x06008E18 RID: 36376 RVA: 0x001DF970 File Offset: 0x001DDB70
				public ETextOutput ETextOutput(ProgramNode node)
				{
					ETextOutput? etextOutput = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.ETextOutput.CreateSafe(this._builders, node);
					if (etextOutput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ETextOutput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return etextOutput.Value;
				}

				// Token: 0x06008E19 RID: 36377 RVA: 0x001DF9C4 File Offset: 0x001DDBC4
				public LetEText LetEText(ProgramNode node)
				{
					LetEText? letEText = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.LetEText.CreateSafe(this._builders, node);
					if (letEText == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetEText but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letEText.Value;
				}

				// Token: 0x04003A2F RID: 14895
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001269 RID: 4713
			public class NodeIs
			{
				// Token: 0x06008E1A RID: 36378 RVA: 0x001DFA15 File Offset: 0x001DDC15
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E1B RID: 36379 RVA: 0x001DFA24 File Offset: 0x001DDC24
				public bool columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E1C RID: 36380 RVA: 0x001DFA48 File Offset: 0x001DDC48
				public bool columnNames(ProgramNode node, out Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames value)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames? columnNames = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						value = default(Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames);
						return false;
					}
					value = columnNames.Value;
					return true;
				}

				// Token: 0x06008E1D RID: 36381 RVA: 0x001DFA84 File Offset: 0x001DDC84
				public bool skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E1E RID: 36382 RVA: 0x001DFAA8 File Offset: 0x001DDCA8
				public bool skip(ProgramNode node, out Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip value)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip? skip = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						value = default(Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip);
						return false;
					}
					value = skip.Value;
					return true;
				}

				// Token: 0x06008E1F RID: 36383 RVA: 0x001DFAE4 File Offset: 0x001DDCE4
				public bool skipFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E20 RID: 36384 RVA: 0x001DFB08 File Offset: 0x001DDD08
				public bool skipFooter(ProgramNode node, out skipFooter value)
				{
					skipFooter? skipFooter = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter.CreateSafe(this._builders, node);
					if (skipFooter == null)
					{
						value = default(skipFooter);
						return false;
					}
					value = skipFooter.Value;
					return true;
				}

				// Token: 0x06008E21 RID: 36385 RVA: 0x001DFB44 File Offset: 0x001DDD44
				public bool delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E22 RID: 36386 RVA: 0x001DFB68 File Offset: 0x001DDD68
				public bool delimiter(ProgramNode node, out delimiter value)
				{
					delimiter? delimiter = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
					if (delimiter == null)
					{
						value = default(delimiter);
						return false;
					}
					value = delimiter.Value;
					return true;
				}

				// Token: 0x06008E23 RID: 36387 RVA: 0x001DFBA4 File Offset: 0x001DDDA4
				public bool fieldPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E24 RID: 36388 RVA: 0x001DFBC8 File Offset: 0x001DDDC8
				public bool fieldPositions(ProgramNode node, out fieldPositions value)
				{
					fieldPositions? fieldPositions = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions.CreateSafe(this._builders, node);
					if (fieldPositions == null)
					{
						value = default(fieldPositions);
						return false;
					}
					value = fieldPositions.Value;
					return true;
				}

				// Token: 0x06008E25 RID: 36389 RVA: 0x001DFC04 File Offset: 0x001DDE04
				public bool filterEmptyLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E26 RID: 36390 RVA: 0x001DFC28 File Offset: 0x001DDE28
				public bool filterEmptyLines(ProgramNode node, out filterEmptyLines value)
				{
					filterEmptyLines? filterEmptyLines = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines.CreateSafe(this._builders, node);
					if (filterEmptyLines == null)
					{
						value = default(filterEmptyLines);
						return false;
					}
					value = filterEmptyLines.Value;
					return true;
				}

				// Token: 0x06008E27 RID: 36391 RVA: 0x001DFC64 File Offset: 0x001DDE64
				public bool commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E28 RID: 36392 RVA: 0x001DFC88 File Offset: 0x001DDE88
				public bool commentStr(ProgramNode node, out commentStr value)
				{
					commentStr? commentStr = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
					if (commentStr == null)
					{
						value = default(commentStr);
						return false;
					}
					value = commentStr.Value;
					return true;
				}

				// Token: 0x06008E29 RID: 36393 RVA: 0x001DFCC4 File Offset: 0x001DDEC4
				public bool quoteChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E2A RID: 36394 RVA: 0x001DFCE8 File Offset: 0x001DDEE8
				public bool quoteChar(ProgramNode node, out quoteChar value)
				{
					quoteChar? quoteChar = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar.CreateSafe(this._builders, node);
					if (quoteChar == null)
					{
						value = default(quoteChar);
						return false;
					}
					value = quoteChar.Value;
					return true;
				}

				// Token: 0x06008E2B RID: 36395 RVA: 0x001DFD24 File Offset: 0x001DDF24
				public bool escapeChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E2C RID: 36396 RVA: 0x001DFD48 File Offset: 0x001DDF48
				public bool escapeChar(ProgramNode node, out escapeChar value)
				{
					escapeChar? escapeChar = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar.CreateSafe(this._builders, node);
					if (escapeChar == null)
					{
						value = default(escapeChar);
						return false;
					}
					value = escapeChar.Value;
					return true;
				}

				// Token: 0x06008E2D RID: 36397 RVA: 0x001DFD84 File Offset: 0x001DDF84
				public bool doubleQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E2E RID: 36398 RVA: 0x001DFDA8 File Offset: 0x001DDFA8
				public bool doubleQuote(ProgramNode node, out doubleQuote value)
				{
					doubleQuote? doubleQuote = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote.CreateSafe(this._builders, node);
					if (doubleQuote == null)
					{
						value = default(doubleQuote);
						return false;
					}
					value = doubleQuote.Value;
					return true;
				}

				// Token: 0x06008E2F RID: 36399 RVA: 0x001DFDE4 File Offset: 0x001DDFE4
				public bool readFlatFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E30 RID: 36400 RVA: 0x001DFE08 File Offset: 0x001DE008
				public bool readFlatFile(ProgramNode node, out readFlatFile value)
				{
					readFlatFile? readFlatFile = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile.CreateSafe(this._builders, node);
					if (readFlatFile == null)
					{
						value = default(readFlatFile);
						return false;
					}
					value = readFlatFile.Value;
					return true;
				}

				// Token: 0x06008E31 RID: 36401 RVA: 0x001DFE44 File Offset: 0x001DE044
				public bool eText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E32 RID: 36402 RVA: 0x001DFE68 File Offset: 0x001DE068
				public bool eText(ProgramNode node, out eText value)
				{
					eText? eText = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText.CreateSafe(this._builders, node);
					if (eText == null)
					{
						value = default(eText);
						return false;
					}
					value = eText.Value;
					return true;
				}

				// Token: 0x06008E33 RID: 36403 RVA: 0x001DFEA4 File Offset: 0x001DE0A4
				public bool fileRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E34 RID: 36404 RVA: 0x001DFEC8 File Offset: 0x001DE0C8
				public bool fileRegion(ProgramNode node, out fileRegion value)
				{
					fileRegion? fileRegion = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion.CreateSafe(this._builders, node);
					if (fileRegion == null)
					{
						value = default(fileRegion);
						return false;
					}
					value = fileRegion.Value;
					return true;
				}

				// Token: 0x06008E35 RID: 36405 RVA: 0x001DFF04 File Offset: 0x001DE104
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E36 RID: 36406 RVA: 0x001DFF28 File Offset: 0x001DE128
				public bool _LetB0(ProgramNode node, out Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0 value)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06008E37 RID: 36407 RVA: 0x001DFF64 File Offset: 0x001DE164
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E38 RID: 36408 RVA: 0x001DFF88 File Offset: 0x001DE188
				public bool _LetB1(ProgramNode node, out Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1 value)
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1? letB = Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x04003A30 RID: 14896
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200126A RID: 4714
			public class RuleIs
			{
				// Token: 0x06008E39 RID: 36409 RVA: 0x001DFFC2 File Offset: 0x001DE1C2
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E3A RID: 36410 RVA: 0x001DFFD4 File Offset: 0x001DE1D4
				public bool Csv(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Csv.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E3B RID: 36411 RVA: 0x001DFFF8 File Offset: 0x001DE1F8
				public bool Csv(ProgramNode node, out Csv value)
				{
					Csv? csv = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Csv.CreateSafe(this._builders, node);
					if (csv == null)
					{
						value = default(Csv);
						return false;
					}
					value = csv.Value;
					return true;
				}

				// Token: 0x06008E3C RID: 36412 RVA: 0x001E0034 File Offset: 0x001DE234
				public bool Fw(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Fw.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E3D RID: 36413 RVA: 0x001E0058 File Offset: 0x001DE258
				public bool Fw(ProgramNode node, out Fw value)
				{
					Fw? fw = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Fw.CreateSafe(this._builders, node);
					if (fw == null)
					{
						value = default(Fw);
						return false;
					}
					value = fw.Value;
					return true;
				}

				// Token: 0x06008E3E RID: 36414 RVA: 0x001E0094 File Offset: 0x001DE294
				public bool StringRegionToStringTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.StringRegionToStringTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E3F RID: 36415 RVA: 0x001E00B8 File Offset: 0x001DE2B8
				public bool StringRegionToStringTable(ProgramNode node, out StringRegionToStringTable value)
				{
					StringRegionToStringTable? stringRegionToStringTable = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.StringRegionToStringTable.CreateSafe(this._builders, node);
					if (stringRegionToStringTable == null)
					{
						value = default(StringRegionToStringTable);
						return false;
					}
					value = stringRegionToStringTable.Value;
					return true;
				}

				// Token: 0x06008E40 RID: 36416 RVA: 0x001E00F4 File Offset: 0x001DE2F4
				public bool CreateStringRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.CreateStringRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E41 RID: 36417 RVA: 0x001E0118 File Offset: 0x001DE318
				public bool CreateStringRegion(ProgramNode node, out CreateStringRegion value)
				{
					CreateStringRegion? createStringRegion = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.CreateStringRegion.CreateSafe(this._builders, node);
					if (createStringRegion == null)
					{
						value = default(CreateStringRegion);
						return false;
					}
					value = createStringRegion.Value;
					return true;
				}

				// Token: 0x06008E42 RID: 36418 RVA: 0x001E0154 File Offset: 0x001DE354
				public bool ETextOutput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.ETextOutput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E43 RID: 36419 RVA: 0x001E0178 File Offset: 0x001DE378
				public bool ETextOutput(ProgramNode node, out ETextOutput value)
				{
					ETextOutput? etextOutput = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.ETextOutput.CreateSafe(this._builders, node);
					if (etextOutput == null)
					{
						value = default(ETextOutput);
						return false;
					}
					value = etextOutput.Value;
					return true;
				}

				// Token: 0x06008E44 RID: 36420 RVA: 0x001E01B4 File Offset: 0x001DE3B4
				public bool LetEText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.LetEText.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008E45 RID: 36421 RVA: 0x001E01D8 File Offset: 0x001DE3D8
				public bool LetEText(ProgramNode node, out LetEText value)
				{
					LetEText? letEText = Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.LetEText.CreateSafe(this._builders, node);
					if (letEText == null)
					{
						value = default(LetEText);
						return false;
					}
					value = letEText.Value;
					return true;
				}

				// Token: 0x04003A31 RID: 14897
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200126B RID: 4715
			public class NodeAs
			{
				// Token: 0x06008E46 RID: 36422 RVA: 0x001E0212 File Offset: 0x001DE412
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E47 RID: 36423 RVA: 0x001E0221 File Offset: 0x001DE421
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames? columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E48 RID: 36424 RVA: 0x001E022F File Offset: 0x001DE42F
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip? skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E49 RID: 36425 RVA: 0x001E023D File Offset: 0x001DE43D
				public skipFooter? skipFooter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4A RID: 36426 RVA: 0x001E024B File Offset: 0x001DE44B
				public delimiter? delimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4B RID: 36427 RVA: 0x001E0259 File Offset: 0x001DE459
				public fieldPositions? fieldPositions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4C RID: 36428 RVA: 0x001E0267 File Offset: 0x001DE467
				public filterEmptyLines? filterEmptyLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4D RID: 36429 RVA: 0x001E0275 File Offset: 0x001DE475
				public commentStr? commentStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4E RID: 36430 RVA: 0x001E0283 File Offset: 0x001DE483
				public quoteChar? quoteChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E4F RID: 36431 RVA: 0x001E0291 File Offset: 0x001DE491
				public escapeChar? escapeChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E50 RID: 36432 RVA: 0x001E029F File Offset: 0x001DE49F
				public doubleQuote? doubleQuote(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E51 RID: 36433 RVA: 0x001E02AD File Offset: 0x001DE4AD
				public readFlatFile? readFlatFile(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E52 RID: 36434 RVA: 0x001E02BB File Offset: 0x001DE4BB
				public eText? eText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E53 RID: 36435 RVA: 0x001E02C9 File Offset: 0x001DE4C9
				public fileRegion? fileRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E54 RID: 36436 RVA: 0x001E02D7 File Offset: 0x001DE4D7
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E55 RID: 36437 RVA: 0x001E02E5 File Offset: 0x001DE4E5
				public Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x04003A32 RID: 14898
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x0200126C RID: 4716
			public class RuleAs
			{
				// Token: 0x06008E56 RID: 36438 RVA: 0x001E02F3 File Offset: 0x001DE4F3
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E57 RID: 36439 RVA: 0x001E0302 File Offset: 0x001DE502
				public Csv? Csv(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Csv.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E58 RID: 36440 RVA: 0x001E0310 File Offset: 0x001DE510
				public Fw? Fw(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.Fw.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E59 RID: 36441 RVA: 0x001E031E File Offset: 0x001DE51E
				public StringRegionToStringTable? StringRegionToStringTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.StringRegionToStringTable.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E5A RID: 36442 RVA: 0x001E032C File Offset: 0x001DE52C
				public CreateStringRegion? CreateStringRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.CreateStringRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E5B RID: 36443 RVA: 0x001E033A File Offset: 0x001DE53A
				public ETextOutput? ETextOutput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.ETextOutput.CreateSafe(this._builders, node);
				}

				// Token: 0x06008E5C RID: 36444 RVA: 0x001E0348 File Offset: 0x001DE548
				public LetEText? LetEText(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes.LetEText.CreateSafe(this._builders, node);
				}

				// Token: 0x04003A33 RID: 14899
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x0200126E RID: 4718
		public class Sets
		{
			// Token: 0x06008E60 RID: 36448 RVA: 0x001E0370 File Offset: 0x001DE570
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17001883 RID: 6275
			// (get) Token: 0x06008E61 RID: 36449 RVA: 0x001E03BF File Offset: 0x001DE5BF
			// (set) Token: 0x06008E62 RID: 36450 RVA: 0x001E03C7 File Offset: 0x001DE5C7
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17001884 RID: 6276
			// (get) Token: 0x06008E63 RID: 36451 RVA: 0x001E03D0 File Offset: 0x001DE5D0
			// (set) Token: 0x06008E64 RID: 36452 RVA: 0x001E03D8 File Offset: 0x001DE5D8
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17001885 RID: 6277
			// (get) Token: 0x06008E65 RID: 36453 RVA: 0x001E03E1 File Offset: 0x001DE5E1
			// (set) Token: 0x06008E66 RID: 36454 RVA: 0x001E03E9 File Offset: 0x001DE5E9
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17001886 RID: 6278
			// (get) Token: 0x06008E67 RID: 36455 RVA: 0x001E03F2 File Offset: 0x001DE5F2
			// (set) Token: 0x06008E68 RID: 36456 RVA: 0x001E03FA File Offset: 0x001DE5FA
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17001887 RID: 6279
			// (get) Token: 0x06008E69 RID: 36457 RVA: 0x001E0403 File Offset: 0x001DE603
			// (set) Token: 0x06008E6A RID: 36458 RVA: 0x001E040B File Offset: 0x001DE60B
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x0200126F RID: 4719
			public class Joins
			{
				// Token: 0x06008E6B RID: 36459 RVA: 0x001E0414 File Offset: 0x001DE614
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E6C RID: 36460 RVA: 0x001E0424 File Offset: 0x001DE624
				public ProgramSetBuilder<readFlatFile> Csv(ProgramSetBuilder<file> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip> value2, ProgramSetBuilder<skipFooter> value3, ProgramSetBuilder<delimiter> value4, ProgramSetBuilder<filterEmptyLines> value5, ProgramSetBuilder<commentStr> value6, ProgramSetBuilder<quoteChar> value7, ProgramSetBuilder<escapeChar> value8, ProgramSetBuilder<doubleQuote> value9)
				{
					return ProgramSetBuilder<readFlatFile>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Csv, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null,
						(value7 != null) ? value7.Set : null,
						(value8 != null) ? value8.Set : null,
						(value9 != null) ? value9.Set : null
					}));
				}

				// Token: 0x06008E6D RID: 36461 RVA: 0x001E04F8 File Offset: 0x001DE6F8
				public ProgramSetBuilder<readFlatFile> Fw(ProgramSetBuilder<file> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip> value2, ProgramSetBuilder<skipFooter> value3, ProgramSetBuilder<fieldPositions> value4, ProgramSetBuilder<filterEmptyLines> value5, ProgramSetBuilder<commentStr> value6)
				{
					return ProgramSetBuilder<readFlatFile>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Fw, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null
					}));
				}

				// Token: 0x06008E6E RID: 36462 RVA: 0x001E0596 File Offset: 0x001DE796
				public ProgramSetBuilder<readFlatFile> StringRegionToStringTable(ProgramSetBuilder<eText> value0)
				{
					return ProgramSetBuilder<readFlatFile>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StringRegionToStringTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E6F RID: 36463 RVA: 0x001E05C7 File Offset: 0x001DE7C7
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0> CreateStringRegion(ProgramSetBuilder<file> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.CreateStringRegion, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E70 RID: 36464 RVA: 0x001E05F8 File Offset: 0x001DE7F8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1> ETextOutput(ProgramSetBuilder<output> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ETextOutput, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E71 RID: 36465 RVA: 0x001E0629 File Offset: 0x001DE829
				public ProgramSetBuilder<eText> LetEText(ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1> value1)
				{
					return ProgramSetBuilder<eText>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetEText, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003A3A RID: 14906
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001270 RID: 4720
			public class ExplicitJoins
			{
				// Token: 0x06008E72 RID: 36466 RVA: 0x001E0669 File Offset: 0x001DE869
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E73 RID: 36467 RVA: 0x001E0678 File Offset: 0x001DE878
				public JoinProgramSetBuilder<readFlatFile> Csv(ProgramSetBuilder<file> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip> value2, ProgramSetBuilder<skipFooter> value3, ProgramSetBuilder<delimiter> value4, ProgramSetBuilder<filterEmptyLines> value5, ProgramSetBuilder<commentStr> value6, ProgramSetBuilder<quoteChar> value7, ProgramSetBuilder<escapeChar> value8, ProgramSetBuilder<doubleQuote> value9)
				{
					return JoinProgramSetBuilder<readFlatFile>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Csv, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null,
						(value7 != null) ? value7.Set : null,
						(value8 != null) ? value8.Set : null,
						(value9 != null) ? value9.Set : null
					}));
				}

				// Token: 0x06008E74 RID: 36468 RVA: 0x001E074C File Offset: 0x001DE94C
				public JoinProgramSetBuilder<readFlatFile> Fw(ProgramSetBuilder<file> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip> value2, ProgramSetBuilder<skipFooter> value3, ProgramSetBuilder<fieldPositions> value4, ProgramSetBuilder<filterEmptyLines> value5, ProgramSetBuilder<commentStr> value6)
				{
					return JoinProgramSetBuilder<readFlatFile>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Fw, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null,
						(value6 != null) ? value6.Set : null
					}));
				}

				// Token: 0x06008E75 RID: 36469 RVA: 0x001E07EA File Offset: 0x001DE9EA
				public JoinProgramSetBuilder<readFlatFile> StringRegionToStringTable(ProgramSetBuilder<eText> value0)
				{
					return JoinProgramSetBuilder<readFlatFile>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StringRegionToStringTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E76 RID: 36470 RVA: 0x001E081B File Offset: 0x001DEA1B
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0> CreateStringRegion(ProgramSetBuilder<file> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.CreateStringRegion, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E77 RID: 36471 RVA: 0x001E084C File Offset: 0x001DEA4C
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1> ETextOutput(ProgramSetBuilder<output> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ETextOutput, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06008E78 RID: 36472 RVA: 0x001E087D File Offset: 0x001DEA7D
				public JoinProgramSetBuilder<eText> LetEText(ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1> value1)
				{
					return JoinProgramSetBuilder<eText>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetEText, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003A3B RID: 14907
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001271 RID: 4721
			public class JoinUnnamedConversions
			{
				// Token: 0x06008E79 RID: 36473 RVA: 0x001E08BD File Offset: 0x001DEABD
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x04003A3C RID: 14908
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001272 RID: 4722
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06008E7A RID: 36474 RVA: 0x001E08CC File Offset: 0x001DEACC
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x04003A3D RID: 14909
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001273 RID: 4723
			public class Casts
			{
				// Token: 0x06008E7B RID: 36475 RVA: 0x001E08DB File Offset: 0x001DEADB
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008E7C RID: 36476 RVA: 0x001E08EC File Offset: 0x001DEAEC
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames> columnNames(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnNames)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnNames but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.columnNames>.CreateUnsafe(set);
				}

				// Token: 0x06008E7D RID: 36477 RVA: 0x001E0944 File Offset: 0x001DEB44
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip> skip(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skip)
					{
						string text = "set";
						string text2 = "expected program set for symbol skip but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skip>.CreateUnsafe(set);
				}

				// Token: 0x06008E7E RID: 36478 RVA: 0x001E099C File Offset: 0x001DEB9C
				public ProgramSetBuilder<skipFooter> skipFooter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skipFooter)
					{
						string text = "set";
						string text2 = "expected program set for symbol skipFooter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.skipFooter>.CreateUnsafe(set);
				}

				// Token: 0x06008E7F RID: 36479 RVA: 0x001E09F4 File Offset: 0x001DEBF4
				public ProgramSetBuilder<delimiter> delimiter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.delimiter)
					{
						string text = "set";
						string text2 = "expected program set for symbol delimiter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.delimiter>.CreateUnsafe(set);
				}

				// Token: 0x06008E80 RID: 36480 RVA: 0x001E0A4C File Offset: 0x001DEC4C
				public ProgramSetBuilder<fieldPositions> fieldPositions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fieldPositions)
					{
						string text = "set";
						string text2 = "expected program set for symbol fieldPositions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fieldPositions>.CreateUnsafe(set);
				}

				// Token: 0x06008E81 RID: 36481 RVA: 0x001E0AA4 File Offset: 0x001DECA4
				public ProgramSetBuilder<filterEmptyLines> filterEmptyLines(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterEmptyLines)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterEmptyLines but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.filterEmptyLines>.CreateUnsafe(set);
				}

				// Token: 0x06008E82 RID: 36482 RVA: 0x001E0AFC File Offset: 0x001DECFC
				public ProgramSetBuilder<commentStr> commentStr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.commentStr)
					{
						string text = "set";
						string text2 = "expected program set for symbol commentStr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.commentStr>.CreateUnsafe(set);
				}

				// Token: 0x06008E83 RID: 36483 RVA: 0x001E0B54 File Offset: 0x001DED54
				public ProgramSetBuilder<quoteChar> quoteChar(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.quoteChar)
					{
						string text = "set";
						string text2 = "expected program set for symbol quoteChar but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.quoteChar>.CreateUnsafe(set);
				}

				// Token: 0x06008E84 RID: 36484 RVA: 0x001E0BAC File Offset: 0x001DEDAC
				public ProgramSetBuilder<escapeChar> escapeChar(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.escapeChar)
					{
						string text = "set";
						string text2 = "expected program set for symbol escapeChar but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.escapeChar>.CreateUnsafe(set);
				}

				// Token: 0x06008E85 RID: 36485 RVA: 0x001E0C04 File Offset: 0x001DEE04
				public ProgramSetBuilder<doubleQuote> doubleQuote(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.doubleQuote)
					{
						string text = "set";
						string text2 = "expected program set for symbol doubleQuote but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.doubleQuote>.CreateUnsafe(set);
				}

				// Token: 0x06008E86 RID: 36486 RVA: 0x001E0C5C File Offset: 0x001DEE5C
				public ProgramSetBuilder<readFlatFile> readFlatFile(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.readFlatFile)
					{
						string text = "set";
						string text2 = "expected program set for symbol readFlatFile but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.readFlatFile>.CreateUnsafe(set);
				}

				// Token: 0x06008E87 RID: 36487 RVA: 0x001E0CB4 File Offset: 0x001DEEB4
				public ProgramSetBuilder<eText> eText(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.eText)
					{
						string text = "set";
						string text2 = "expected program set for symbol eText but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.eText>.CreateUnsafe(set);
				}

				// Token: 0x06008E88 RID: 36488 RVA: 0x001E0D0C File Offset: 0x001DEF0C
				public ProgramSetBuilder<fileRegion> fileRegion(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fileRegion)
					{
						string text = "set";
						string text2 = "expected program set for symbol fileRegion but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes.fileRegion>.CreateUnsafe(set);
				}

				// Token: 0x06008E89 RID: 36489 RVA: 0x001E0D64 File Offset: 0x001DEF64
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x06008E8A RID: 36490 RVA: 0x001E0DBC File Offset: 0x001DEFBC
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x04003A3E RID: 14910
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
