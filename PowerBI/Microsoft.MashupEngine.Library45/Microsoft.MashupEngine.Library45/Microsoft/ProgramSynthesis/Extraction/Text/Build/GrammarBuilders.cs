using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build
{
	// Token: 0x02000F01 RID: 3841
	public class GrammarBuilders
	{
		// Token: 0x06006872 RID: 26738 RVA: 0x0015AD59 File Offset: 0x00158F59
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x0015AD85 File Offset: 0x00158F85
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x0015AD92 File Offset: 0x00158F92
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x0015AD9F File Offset: 0x00158F9F
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x0015ADAC File Offset: 0x00158FAC
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06006877 RID: 26743 RVA: 0x0015ADB9 File Offset: 0x00158FB9
		// (set) Token: 0x06006878 RID: 26744 RVA: 0x0015ADC1 File Offset: 0x00158FC1
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06006879 RID: 26745 RVA: 0x0015ADCA File Offset: 0x00158FCA
		// (set) Token: 0x0600687A RID: 26746 RVA: 0x0015ADD2 File Offset: 0x00158FD2
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600687B RID: 26747 RVA: 0x0015ADDC File Offset: 0x00158FDC
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

		// Token: 0x04002E51 RID: 11857
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04002E52 RID: 11858
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04002E53 RID: 11859
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04002E54 RID: 11860
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04002E55 RID: 11861
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000F02 RID: 3842
		public class GrammarSymbols
		{
			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x0600687D RID: 26749 RVA: 0x0015AE87 File Offset: 0x00159087
			// (set) Token: 0x0600687E RID: 26750 RVA: 0x0015AE8F File Offset: 0x0015908F
			public Symbol v { get; private set; }

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x0600687F RID: 26751 RVA: 0x0015AE98 File Offset: 0x00159098
			// (set) Token: 0x06006880 RID: 26752 RVA: 0x0015AEA0 File Offset: 0x001590A0
			public Symbol k { get; private set; }

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06006881 RID: 26753 RVA: 0x0015AEA9 File Offset: 0x001590A9
			// (set) Token: 0x06006882 RID: 26754 RVA: 0x0015AEB1 File Offset: 0x001590B1
			public Symbol str { get; private set; }

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06006883 RID: 26755 RVA: 0x0015AEBA File Offset: 0x001590BA
			// (set) Token: 0x06006884 RID: 26756 RVA: 0x0015AEC2 File Offset: 0x001590C2
			public Symbol del { get; private set; }

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x06006885 RID: 26757 RVA: 0x0015AECB File Offset: 0x001590CB
			// (set) Token: 0x06006886 RID: 26758 RVA: 0x0015AED3 File Offset: 0x001590D3
			public Symbol re { get; private set; }

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x06006887 RID: 26759 RVA: 0x0015AEDC File Offset: 0x001590DC
			// (set) Token: 0x06006888 RID: 26760 RVA: 0x0015AEE4 File Offset: 0x001590E4
			public Symbol columnNames { get; private set; }

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x06006889 RID: 26761 RVA: 0x0015AEED File Offset: 0x001590ED
			// (set) Token: 0x0600688A RID: 26762 RVA: 0x0015AEF5 File Offset: 0x001590F5
			public Symbol output { get; private set; }

			// Token: 0x170012AD RID: 4781
			// (get) Token: 0x0600688B RID: 26763 RVA: 0x0015AEFE File Offset: 0x001590FE
			// (set) Token: 0x0600688C RID: 26764 RVA: 0x0015AF06 File Offset: 0x00159106
			public Symbol table { get; private set; }

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x0600688D RID: 26765 RVA: 0x0015AF0F File Offset: 0x0015910F
			// (set) Token: 0x0600688E RID: 26766 RVA: 0x0015AF17 File Offset: 0x00159117
			public Symbol row { get; private set; }

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x0600688F RID: 26767 RVA: 0x0015AF20 File Offset: 0x00159120
			// (set) Token: 0x06006890 RID: 26768 RVA: 0x0015AF28 File Offset: 0x00159128
			public Symbol colSplit { get; private set; }

			// Token: 0x170012B0 RID: 4784
			// (get) Token: 0x06006891 RID: 26769 RVA: 0x0015AF31 File Offset: 0x00159131
			// (set) Token: 0x06006892 RID: 26770 RVA: 0x0015AF39 File Offset: 0x00159139
			public Symbol tup { get; private set; }

			// Token: 0x170012B1 RID: 4785
			// (get) Token: 0x06006893 RID: 26771 RVA: 0x0015AF42 File Offset: 0x00159142
			// (set) Token: 0x06006894 RID: 26772 RVA: 0x0015AF4A File Offset: 0x0015914A
			public Symbol extractTup { get; private set; }

			// Token: 0x170012B2 RID: 4786
			// (get) Token: 0x06006895 RID: 26773 RVA: 0x0015AF53 File Offset: 0x00159153
			// (set) Token: 0x06006896 RID: 26774 RVA: 0x0015AF5B File Offset: 0x0015915B
			public Symbol trimExtract { get; private set; }

			// Token: 0x170012B3 RID: 4787
			// (get) Token: 0x06006897 RID: 26775 RVA: 0x0015AF64 File Offset: 0x00159164
			// (set) Token: 0x06006898 RID: 26776 RVA: 0x0015AF6C File Offset: 0x0015916C
			public Symbol extract { get; private set; }

			// Token: 0x170012B4 RID: 4788
			// (get) Token: 0x06006899 RID: 26777 RVA: 0x0015AF75 File Offset: 0x00159175
			// (set) Token: 0x0600689A RID: 26778 RVA: 0x0015AF7D File Offset: 0x0015917D
			public Symbol split { get; private set; }

			// Token: 0x170012B5 RID: 4789
			// (get) Token: 0x0600689B RID: 26779 RVA: 0x0015AF86 File Offset: 0x00159186
			// (set) Token: 0x0600689C RID: 26780 RVA: 0x0015AF8E File Offset: 0x0015918E
			public Symbol records { get; private set; }

			// Token: 0x170012B6 RID: 4790
			// (get) Token: 0x0600689D RID: 26781 RVA: 0x0015AF97 File Offset: 0x00159197
			// (set) Token: 0x0600689E RID: 26782 RVA: 0x0015AF9F File Offset: 0x0015919F
			public Symbol skip { get; private set; }

			// Token: 0x170012B7 RID: 4791
			// (get) Token: 0x0600689F RID: 26783 RVA: 0x0015AFA8 File Offset: 0x001591A8
			// (set) Token: 0x060068A0 RID: 26784 RVA: 0x0015AFB0 File Offset: 0x001591B0
			public Symbol lines { get; private set; }

			// Token: 0x170012B8 RID: 4792
			// (get) Token: 0x060068A1 RID: 26785 RVA: 0x0015AFB9 File Offset: 0x001591B9
			// (set) Token: 0x060068A2 RID: 26786 RVA: 0x0015AFC1 File Offset: 0x001591C1
			public Symbol _LFun0 { get; private set; }

			// Token: 0x170012B9 RID: 4793
			// (get) Token: 0x060068A3 RID: 26787 RVA: 0x0015AFCA File Offset: 0x001591CA
			// (set) Token: 0x060068A4 RID: 26788 RVA: 0x0015AFD2 File Offset: 0x001591D2
			public Symbol _LetB0 { get; private set; }

			// Token: 0x170012BA RID: 4794
			// (get) Token: 0x060068A5 RID: 26789 RVA: 0x0015AFDB File Offset: 0x001591DB
			// (set) Token: 0x060068A6 RID: 26790 RVA: 0x0015AFE3 File Offset: 0x001591E3
			public Symbol _LetB1 { get; private set; }

			// Token: 0x170012BB RID: 4795
			// (get) Token: 0x060068A7 RID: 26791 RVA: 0x0015AFEC File Offset: 0x001591EC
			// (set) Token: 0x060068A8 RID: 26792 RVA: 0x0015AFF4 File Offset: 0x001591F4
			public Symbol _LetB2 { get; private set; }

			// Token: 0x170012BC RID: 4796
			// (get) Token: 0x060068A9 RID: 26793 RVA: 0x0015AFFD File Offset: 0x001591FD
			// (set) Token: 0x060068AA RID: 26794 RVA: 0x0015B005 File Offset: 0x00159205
			public Symbol _LetB3 { get; private set; }

			// Token: 0x060068AB RID: 26795 RVA: 0x0015B010 File Offset: 0x00159210
			public GrammarSymbols(Grammar grammar)
			{
				this.v = grammar.Symbol("v");
				this.k = grammar.Symbol("k");
				this.str = grammar.Symbol("str");
				this.del = grammar.Symbol("del");
				this.re = grammar.Symbol("re");
				this.columnNames = grammar.Symbol("columnNames");
				this.output = grammar.Symbol("output");
				this.table = grammar.Symbol("table");
				this.row = grammar.Symbol("row");
				this.colSplit = grammar.Symbol("colSplit");
				this.tup = grammar.Symbol("tup");
				this.extractTup = grammar.Symbol("extractTup");
				this.trimExtract = grammar.Symbol("trimExtract");
				this.extract = grammar.Symbol("extract");
				this.split = grammar.Symbol("split");
				this.records = grammar.Symbol("records");
				this.skip = grammar.Symbol("skip");
				this.lines = grammar.Symbol("lines");
				this._LFun0 = grammar.Symbol("_LFun0");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
				this._LetB2 = grammar.Symbol("_LetB2");
				this._LetB3 = grammar.Symbol("_LetB3");
			}
		}

		// Token: 0x02000F03 RID: 3843
		public class GrammarRules
		{
			// Token: 0x170012BD RID: 4797
			// (get) Token: 0x060068AC RID: 26796 RVA: 0x0015B1AA File Offset: 0x001593AA
			// (set) Token: 0x060068AD RID: 26797 RVA: 0x0015B1B2 File Offset: 0x001593B2
			public BlackBoxRule Table { get; private set; }

			// Token: 0x170012BE RID: 4798
			// (get) Token: 0x060068AE RID: 26798 RVA: 0x0015B1BB File Offset: 0x001593BB
			// (set) Token: 0x060068AF RID: 26799 RVA: 0x0015B1C3 File Offset: 0x001593C3
			public BlackBoxRule Second { get; private set; }

			// Token: 0x170012BF RID: 4799
			// (get) Token: 0x060068B0 RID: 26800 RVA: 0x0015B1CC File Offset: 0x001593CC
			// (set) Token: 0x060068B1 RID: 26801 RVA: 0x0015B1D4 File Offset: 0x001593D4
			public BlackBoxRule Prepend { get; private set; }

			// Token: 0x170012C0 RID: 4800
			// (get) Token: 0x060068B2 RID: 26802 RVA: 0x0015B1DD File Offset: 0x001593DD
			// (set) Token: 0x060068B3 RID: 26803 RVA: 0x0015B1E5 File Offset: 0x001593E5
			public BlackBoxRule List { get; private set; }

			// Token: 0x170012C1 RID: 4801
			// (get) Token: 0x060068B4 RID: 26804 RVA: 0x0015B1EE File Offset: 0x001593EE
			// (set) Token: 0x060068B5 RID: 26805 RVA: 0x0015B1F6 File Offset: 0x001593F6
			public BlackBoxRule First { get; private set; }

			// Token: 0x170012C2 RID: 4802
			// (get) Token: 0x060068B6 RID: 26806 RVA: 0x0015B1FF File Offset: 0x001593FF
			// (set) Token: 0x060068B7 RID: 26807 RVA: 0x0015B207 File Offset: 0x00159407
			public BlackBoxRule Trim { get; private set; }

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x060068B8 RID: 26808 RVA: 0x0015B210 File Offset: 0x00159410
			// (set) Token: 0x060068B9 RID: 26809 RVA: 0x0015B218 File Offset: 0x00159418
			public BlackBoxRule BetweenDelimiters { get; private set; }

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x060068BA RID: 26810 RVA: 0x0015B221 File Offset: 0x00159421
			// (set) Token: 0x060068BB RID: 26811 RVA: 0x0015B229 File Offset: 0x00159429
			public BlackBoxRule Substring { get; private set; }

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x060068BC RID: 26812 RVA: 0x0015B232 File Offset: 0x00159432
			// (set) Token: 0x060068BD RID: 26813 RVA: 0x0015B23A File Offset: 0x0015943A
			public BlackBoxRule Slice { get; private set; }

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x060068BE RID: 26814 RVA: 0x0015B243 File Offset: 0x00159443
			// (set) Token: 0x060068BF RID: 26815 RVA: 0x0015B24B File Offset: 0x0015944B
			public BlackBoxRule SplitPosition { get; private set; }

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x060068C0 RID: 26816 RVA: 0x0015B254 File Offset: 0x00159454
			// (set) Token: 0x060068C1 RID: 26817 RVA: 0x0015B25C File Offset: 0x0015945C
			public BlackBoxRule SplitDelimiter { get; private set; }

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x060068C2 RID: 26818 RVA: 0x0015B265 File Offset: 0x00159465
			// (set) Token: 0x060068C3 RID: 26819 RVA: 0x0015B26D File Offset: 0x0015946D
			public BlackBoxRule Select { get; private set; }

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x060068C4 RID: 26820 RVA: 0x0015B276 File Offset: 0x00159476
			// (set) Token: 0x060068C5 RID: 26821 RVA: 0x0015B27E File Offset: 0x0015947E
			public BlackBoxRule Group { get; private set; }

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x060068C6 RID: 26822 RVA: 0x0015B287 File Offset: 0x00159487
			// (set) Token: 0x060068C7 RID: 26823 RVA: 0x0015B28F File Offset: 0x0015948F
			public BlackBoxRule MergeEvery { get; private set; }

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x060068C8 RID: 26824 RVA: 0x0015B298 File Offset: 0x00159498
			// (set) Token: 0x060068C9 RID: 26825 RVA: 0x0015B2A0 File Offset: 0x001594A0
			public BlackBoxRule Skip { get; private set; }

			// Token: 0x170012CC RID: 4812
			// (get) Token: 0x060068CA RID: 26826 RVA: 0x0015B2A9 File Offset: 0x001594A9
			// (set) Token: 0x060068CB RID: 26827 RVA: 0x0015B2B1 File Offset: 0x001594B1
			public BlackBoxRule SplitLines { get; private set; }

			// Token: 0x170012CD RID: 4813
			// (get) Token: 0x060068CC RID: 26828 RVA: 0x0015B2BA File Offset: 0x001594BA
			// (set) Token: 0x060068CD RID: 26829 RVA: 0x0015B2C2 File Offset: 0x001594C2
			public ConceptRule RowMap { get; private set; }

			// Token: 0x170012CE RID: 4814
			// (get) Token: 0x060068CE RID: 26830 RVA: 0x0015B2CB File Offset: 0x001594CB
			// (set) Token: 0x060068CF RID: 26831 RVA: 0x0015B2D3 File Offset: 0x001594D3
			public LetRule LetPrepend { get; private set; }

			// Token: 0x170012CF RID: 4815
			// (get) Token: 0x060068D0 RID: 26832 RVA: 0x0015B2DC File Offset: 0x001594DC
			// (set) Token: 0x060068D1 RID: 26833 RVA: 0x0015B2E4 File Offset: 0x001594E4
			public LetRule LetSplit { get; private set; }

			// Token: 0x170012D0 RID: 4816
			// (get) Token: 0x060068D2 RID: 26834 RVA: 0x0015B2ED File Offset: 0x001594ED
			// (set) Token: 0x060068D3 RID: 26835 RVA: 0x0015B2F5 File Offset: 0x001594F5
			public LetRule LetExtractTup { get; private set; }

			// Token: 0x060068D4 RID: 26836 RVA: 0x0015B300 File Offset: 0x00159500
			public GrammarRules(Grammar grammar)
			{
				this.Table = (BlackBoxRule)grammar.Rule("Table");
				this.Second = (BlackBoxRule)grammar.Rule("Second");
				this.Prepend = (BlackBoxRule)grammar.Rule("Prepend");
				this.List = (BlackBoxRule)grammar.Rule("List");
				this.First = (BlackBoxRule)grammar.Rule("First");
				this.Trim = (BlackBoxRule)grammar.Rule("Trim");
				this.BetweenDelimiters = (BlackBoxRule)grammar.Rule("BetweenDelimiters");
				this.Substring = (BlackBoxRule)grammar.Rule("Substring");
				this.Slice = (BlackBoxRule)grammar.Rule("Slice");
				this.SplitPosition = (BlackBoxRule)grammar.Rule("SplitPosition");
				this.SplitDelimiter = (BlackBoxRule)grammar.Rule("SplitDelimiter");
				this.Select = (BlackBoxRule)grammar.Rule("Select");
				this.Group = (BlackBoxRule)grammar.Rule("Group");
				this.MergeEvery = (BlackBoxRule)grammar.Rule("MergeEvery");
				this.Skip = (BlackBoxRule)grammar.Rule("Skip");
				this.SplitLines = (BlackBoxRule)grammar.Rule("SplitLines");
				this.RowMap = (ConceptRule)grammar.Rule("RowMap");
				this.LetPrepend = (LetRule)grammar.Rule("LetPrepend");
				this.LetSplit = (LetRule)grammar.Rule("LetSplit");
				this.LetExtractTup = (LetRule)grammar.Rule("LetExtractTup");
			}
		}

		// Token: 0x02000F04 RID: 3844
		public class GrammarUnnamedConversions
		{
			// Token: 0x170012D1 RID: 4817
			// (get) Token: 0x060068D5 RID: 26837 RVA: 0x0015B4CB File Offset: 0x001596CB
			// (set) Token: 0x060068D6 RID: 26838 RVA: 0x0015B4D3 File Offset: 0x001596D3
			public ConversionRule trimExtract_extract { get; private set; }

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x060068D7 RID: 26839 RVA: 0x0015B4DC File Offset: 0x001596DC
			// (set) Token: 0x060068D8 RID: 26840 RVA: 0x0015B4E4 File Offset: 0x001596E4
			public ConversionRule extract_row { get; private set; }

			// Token: 0x170012D3 RID: 4819
			// (get) Token: 0x060068D9 RID: 26841 RVA: 0x0015B4ED File Offset: 0x001596ED
			// (set) Token: 0x060068DA RID: 26842 RVA: 0x0015B4F5 File Offset: 0x001596F5
			public ConversionRule records_skip { get; private set; }

			// Token: 0x170012D4 RID: 4820
			// (get) Token: 0x060068DB RID: 26843 RVA: 0x0015B4FE File Offset: 0x001596FE
			// (set) Token: 0x060068DC RID: 26844 RVA: 0x0015B506 File Offset: 0x00159706
			public ConversionRule skip_lines { get; private set; }

			// Token: 0x060068DD RID: 26845 RVA: 0x0015B510 File Offset: 0x00159710
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.trimExtract_extract = (ConversionRule)grammar.Rule("~convert_trimExtract_extract");
				this.extract_row = (ConversionRule)grammar.Rule("~convert_extract_row");
				this.records_skip = (ConversionRule)grammar.Rule("~convert_records_skip");
				this.skip_lines = (ConversionRule)grammar.Rule("~convert_skip_lines");
			}
		}

		// Token: 0x02000F05 RID: 3845
		public class GrammarHoles
		{
			// Token: 0x170012D5 RID: 4821
			// (get) Token: 0x060068DE RID: 26846 RVA: 0x0015B57B File Offset: 0x0015977B
			// (set) Token: 0x060068DF RID: 26847 RVA: 0x0015B583 File Offset: 0x00159783
			public Hole v { get; private set; }

			// Token: 0x170012D6 RID: 4822
			// (get) Token: 0x060068E0 RID: 26848 RVA: 0x0015B58C File Offset: 0x0015978C
			// (set) Token: 0x060068E1 RID: 26849 RVA: 0x0015B594 File Offset: 0x00159794
			public Hole k { get; private set; }

			// Token: 0x170012D7 RID: 4823
			// (get) Token: 0x060068E2 RID: 26850 RVA: 0x0015B59D File Offset: 0x0015979D
			// (set) Token: 0x060068E3 RID: 26851 RVA: 0x0015B5A5 File Offset: 0x001597A5
			public Hole str { get; private set; }

			// Token: 0x170012D8 RID: 4824
			// (get) Token: 0x060068E4 RID: 26852 RVA: 0x0015B5AE File Offset: 0x001597AE
			// (set) Token: 0x060068E5 RID: 26853 RVA: 0x0015B5B6 File Offset: 0x001597B6
			public Hole del { get; private set; }

			// Token: 0x170012D9 RID: 4825
			// (get) Token: 0x060068E6 RID: 26854 RVA: 0x0015B5BF File Offset: 0x001597BF
			// (set) Token: 0x060068E7 RID: 26855 RVA: 0x0015B5C7 File Offset: 0x001597C7
			public Hole re { get; private set; }

			// Token: 0x170012DA RID: 4826
			// (get) Token: 0x060068E8 RID: 26856 RVA: 0x0015B5D0 File Offset: 0x001597D0
			// (set) Token: 0x060068E9 RID: 26857 RVA: 0x0015B5D8 File Offset: 0x001597D8
			public Hole columnNames { get; private set; }

			// Token: 0x170012DB RID: 4827
			// (get) Token: 0x060068EA RID: 26858 RVA: 0x0015B5E1 File Offset: 0x001597E1
			// (set) Token: 0x060068EB RID: 26859 RVA: 0x0015B5E9 File Offset: 0x001597E9
			public Hole output { get; private set; }

			// Token: 0x170012DC RID: 4828
			// (get) Token: 0x060068EC RID: 26860 RVA: 0x0015B5F2 File Offset: 0x001597F2
			// (set) Token: 0x060068ED RID: 26861 RVA: 0x0015B5FA File Offset: 0x001597FA
			public Hole table { get; private set; }

			// Token: 0x170012DD RID: 4829
			// (get) Token: 0x060068EE RID: 26862 RVA: 0x0015B603 File Offset: 0x00159803
			// (set) Token: 0x060068EF RID: 26863 RVA: 0x0015B60B File Offset: 0x0015980B
			public Hole row { get; private set; }

			// Token: 0x170012DE RID: 4830
			// (get) Token: 0x060068F0 RID: 26864 RVA: 0x0015B614 File Offset: 0x00159814
			// (set) Token: 0x060068F1 RID: 26865 RVA: 0x0015B61C File Offset: 0x0015981C
			public Hole colSplit { get; private set; }

			// Token: 0x170012DF RID: 4831
			// (get) Token: 0x060068F2 RID: 26866 RVA: 0x0015B625 File Offset: 0x00159825
			// (set) Token: 0x060068F3 RID: 26867 RVA: 0x0015B62D File Offset: 0x0015982D
			public Hole tup { get; private set; }

			// Token: 0x170012E0 RID: 4832
			// (get) Token: 0x060068F4 RID: 26868 RVA: 0x0015B636 File Offset: 0x00159836
			// (set) Token: 0x060068F5 RID: 26869 RVA: 0x0015B63E File Offset: 0x0015983E
			public Hole extractTup { get; private set; }

			// Token: 0x170012E1 RID: 4833
			// (get) Token: 0x060068F6 RID: 26870 RVA: 0x0015B647 File Offset: 0x00159847
			// (set) Token: 0x060068F7 RID: 26871 RVA: 0x0015B64F File Offset: 0x0015984F
			public Hole trimExtract { get; private set; }

			// Token: 0x170012E2 RID: 4834
			// (get) Token: 0x060068F8 RID: 26872 RVA: 0x0015B658 File Offset: 0x00159858
			// (set) Token: 0x060068F9 RID: 26873 RVA: 0x0015B660 File Offset: 0x00159860
			public Hole extract { get; private set; }

			// Token: 0x170012E3 RID: 4835
			// (get) Token: 0x060068FA RID: 26874 RVA: 0x0015B669 File Offset: 0x00159869
			// (set) Token: 0x060068FB RID: 26875 RVA: 0x0015B671 File Offset: 0x00159871
			public Hole split { get; private set; }

			// Token: 0x170012E4 RID: 4836
			// (get) Token: 0x060068FC RID: 26876 RVA: 0x0015B67A File Offset: 0x0015987A
			// (set) Token: 0x060068FD RID: 26877 RVA: 0x0015B682 File Offset: 0x00159882
			public Hole records { get; private set; }

			// Token: 0x170012E5 RID: 4837
			// (get) Token: 0x060068FE RID: 26878 RVA: 0x0015B68B File Offset: 0x0015988B
			// (set) Token: 0x060068FF RID: 26879 RVA: 0x0015B693 File Offset: 0x00159893
			public Hole skip { get; private set; }

			// Token: 0x170012E6 RID: 4838
			// (get) Token: 0x06006900 RID: 26880 RVA: 0x0015B69C File Offset: 0x0015989C
			// (set) Token: 0x06006901 RID: 26881 RVA: 0x0015B6A4 File Offset: 0x001598A4
			public Hole lines { get; private set; }

			// Token: 0x170012E7 RID: 4839
			// (get) Token: 0x06006902 RID: 26882 RVA: 0x0015B6AD File Offset: 0x001598AD
			// (set) Token: 0x06006903 RID: 26883 RVA: 0x0015B6B5 File Offset: 0x001598B5
			public Hole _LFun0 { get; private set; }

			// Token: 0x170012E8 RID: 4840
			// (get) Token: 0x06006904 RID: 26884 RVA: 0x0015B6BE File Offset: 0x001598BE
			// (set) Token: 0x06006905 RID: 26885 RVA: 0x0015B6C6 File Offset: 0x001598C6
			public Hole _LetB0 { get; private set; }

			// Token: 0x170012E9 RID: 4841
			// (get) Token: 0x06006906 RID: 26886 RVA: 0x0015B6CF File Offset: 0x001598CF
			// (set) Token: 0x06006907 RID: 26887 RVA: 0x0015B6D7 File Offset: 0x001598D7
			public Hole _LetB1 { get; private set; }

			// Token: 0x170012EA RID: 4842
			// (get) Token: 0x06006908 RID: 26888 RVA: 0x0015B6E0 File Offset: 0x001598E0
			// (set) Token: 0x06006909 RID: 26889 RVA: 0x0015B6E8 File Offset: 0x001598E8
			public Hole _LetB2 { get; private set; }

			// Token: 0x170012EB RID: 4843
			// (get) Token: 0x0600690A RID: 26890 RVA: 0x0015B6F1 File Offset: 0x001598F1
			// (set) Token: 0x0600690B RID: 26891 RVA: 0x0015B6F9 File Offset: 0x001598F9
			public Hole _LetB3 { get; private set; }

			// Token: 0x0600690C RID: 26892 RVA: 0x0015B704 File Offset: 0x00159904
			public GrammarHoles(GrammarBuilders builders)
			{
				this.v = new Hole(builders.Symbol.v, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.str = new Hole(builders.Symbol.str, null);
				this.del = new Hole(builders.Symbol.del, null);
				this.re = new Hole(builders.Symbol.re, null);
				this.columnNames = new Hole(builders.Symbol.columnNames, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.table = new Hole(builders.Symbol.table, null);
				this.row = new Hole(builders.Symbol.row, null);
				this.colSplit = new Hole(builders.Symbol.colSplit, null);
				this.tup = new Hole(builders.Symbol.tup, null);
				this.extractTup = new Hole(builders.Symbol.extractTup, null);
				this.trimExtract = new Hole(builders.Symbol.trimExtract, null);
				this.extract = new Hole(builders.Symbol.extract, null);
				this.split = new Hole(builders.Symbol.split, null);
				this.records = new Hole(builders.Symbol.records, null);
				this.skip = new Hole(builders.Symbol.skip, null);
				this.lines = new Hole(builders.Symbol.lines, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
				this._LetB2 = new Hole(builders.Symbol._LetB2, null);
				this._LetB3 = new Hole(builders.Symbol._LetB3, null);
			}
		}

		// Token: 0x02000F06 RID: 3846
		public class Nodes
		{
			// Token: 0x0600690D RID: 26893 RVA: 0x0015B928 File Offset: 0x00159B28
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

			// Token: 0x170012EC RID: 4844
			// (get) Token: 0x0600690E RID: 26894 RVA: 0x0015BA0B File Offset: 0x00159C0B
			// (set) Token: 0x0600690F RID: 26895 RVA: 0x0015BA13 File Offset: 0x00159C13
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x170012ED RID: 4845
			// (get) Token: 0x06006910 RID: 26896 RVA: 0x0015BA1C File Offset: 0x00159C1C
			// (set) Token: 0x06006911 RID: 26897 RVA: 0x0015BA24 File Offset: 0x00159C24
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x170012EE RID: 4846
			// (get) Token: 0x06006912 RID: 26898 RVA: 0x0015BA2D File Offset: 0x00159C2D
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x170012EF RID: 4847
			// (get) Token: 0x06006913 RID: 26899 RVA: 0x0015BA3A File Offset: 0x00159C3A
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x170012F0 RID: 4848
			// (get) Token: 0x06006914 RID: 26900 RVA: 0x0015BA47 File Offset: 0x00159C47
			// (set) Token: 0x06006915 RID: 26901 RVA: 0x0015BA4F File Offset: 0x00159C4F
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x170012F1 RID: 4849
			// (get) Token: 0x06006916 RID: 26902 RVA: 0x0015BA58 File Offset: 0x00159C58
			// (set) Token: 0x06006917 RID: 26903 RVA: 0x0015BA60 File Offset: 0x00159C60
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x170012F2 RID: 4850
			// (get) Token: 0x06006918 RID: 26904 RVA: 0x0015BA69 File Offset: 0x00159C69
			// (set) Token: 0x06006919 RID: 26905 RVA: 0x0015BA71 File Offset: 0x00159C71
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x170012F3 RID: 4851
			// (get) Token: 0x0600691A RID: 26906 RVA: 0x0015BA7A File Offset: 0x00159C7A
			// (set) Token: 0x0600691B RID: 26907 RVA: 0x0015BA82 File Offset: 0x00159C82
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x170012F4 RID: 4852
			// (get) Token: 0x0600691C RID: 26908 RVA: 0x0015BA8B File Offset: 0x00159C8B
			// (set) Token: 0x0600691D RID: 26909 RVA: 0x0015BA93 File Offset: 0x00159C93
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x170012F5 RID: 4853
			// (get) Token: 0x0600691E RID: 26910 RVA: 0x0015BA9C File Offset: 0x00159C9C
			// (set) Token: 0x0600691F RID: 26911 RVA: 0x0015BAA4 File Offset: 0x00159CA4
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x170012F6 RID: 4854
			// (get) Token: 0x06006920 RID: 26912 RVA: 0x0015BAAD File Offset: 0x00159CAD
			// (set) Token: 0x06006921 RID: 26913 RVA: 0x0015BAB5 File Offset: 0x00159CB5
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04002EA0 RID: 11936
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04002EA1 RID: 11937
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000F07 RID: 3847
			public class NodeRules
			{
				// Token: 0x06006922 RID: 26914 RVA: 0x0015BABE File Offset: 0x00159CBE
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006923 RID: 26915 RVA: 0x0015BACD File Offset: 0x00159CCD
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x06006924 RID: 26916 RVA: 0x0015BADB File Offset: 0x00159CDB
				public str str(string value)
				{
					return new str(this._builders, value);
				}

				// Token: 0x06006925 RID: 26917 RVA: 0x0015BAE9 File Offset: 0x00159CE9
				public del del(Optional<string> value)
				{
					return new del(this._builders, value);
				}

				// Token: 0x06006926 RID: 26918 RVA: 0x0015BAF7 File Offset: 0x00159CF7
				public re re(Regex value)
				{
					return new re(this._builders, value);
				}

				// Token: 0x06006927 RID: 26919 RVA: 0x0015BB05 File Offset: 0x00159D05
				public columnNames columnNames(IReadOnlyList<string> value)
				{
					return new columnNames(this._builders, value);
				}

				// Token: 0x06006928 RID: 26920 RVA: 0x0015BB13 File Offset: 0x00159D13
				public output Table(columnNames value0, table value1)
				{
					return new Table(this._builders, value0, value1);
				}

				// Token: 0x06006929 RID: 26921 RVA: 0x0015BB27 File Offset: 0x00159D27
				public _LetB0 Second(tup value0)
				{
					return new Second(this._builders, value0);
				}

				// Token: 0x0600692A RID: 26922 RVA: 0x0015BB3A File Offset: 0x00159D3A
				public _LetB1 Prepend(extractTup value0, colSplit value1)
				{
					return new Prepend(this._builders, value0, value1);
				}

				// Token: 0x0600692B RID: 26923 RVA: 0x0015BB4E File Offset: 0x00159D4E
				public colSplit List(trimExtract value0)
				{
					return new List(this._builders, value0);
				}

				// Token: 0x0600692C RID: 26924 RVA: 0x0015BB61 File Offset: 0x00159D61
				public _LetB3 First(tup value0)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First(this._builders, value0);
				}

				// Token: 0x0600692D RID: 26925 RVA: 0x0015BB74 File Offset: 0x00159D74
				public trimExtract Trim(extract value0)
				{
					return new Trim(this._builders, value0);
				}

				// Token: 0x0600692E RID: 26926 RVA: 0x0015BB87 File Offset: 0x00159D87
				public extract BetweenDelimiters(row value0, del value1, del value2)
				{
					return new BetweenDelimiters(this._builders, value0, value1, value2);
				}

				// Token: 0x0600692F RID: 26927 RVA: 0x0015BB9C File Offset: 0x00159D9C
				public extract Substring(row value0, k value1, k value2)
				{
					return new Substring(this._builders, value0, value1, value2);
				}

				// Token: 0x06006930 RID: 26928 RVA: 0x0015BBB1 File Offset: 0x00159DB1
				public extract Slice(row value0, k value1, k value2)
				{
					return new Slice(this._builders, value0, value1, value2);
				}

				// Token: 0x06006931 RID: 26929 RVA: 0x0015BBC6 File Offset: 0x00159DC6
				public split SplitPosition(row value0, k value1)
				{
					return new SplitPosition(this._builders, value0, value1);
				}

				// Token: 0x06006932 RID: 26930 RVA: 0x0015BBDA File Offset: 0x00159DDA
				public split SplitDelimiter(row value0, str value1, k value2)
				{
					return new SplitDelimiter(this._builders, value0, value1, value2);
				}

				// Token: 0x06006933 RID: 26931 RVA: 0x0015BBEF File Offset: 0x00159DEF
				public records Select(re value0, skip value1)
				{
					return new Select(this._builders, value0, value1);
				}

				// Token: 0x06006934 RID: 26932 RVA: 0x0015BC03 File Offset: 0x00159E03
				public records Group(re value0, skip value1)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group(this._builders, value0, value1);
				}

				// Token: 0x06006935 RID: 26933 RVA: 0x0015BC17 File Offset: 0x00159E17
				public records MergeEvery(k value0, skip value1)
				{
					return new MergeEvery(this._builders, value0, value1);
				}

				// Token: 0x06006936 RID: 26934 RVA: 0x0015BC2B File Offset: 0x00159E2B
				public skip Skip(k value0, lines value1)
				{
					return new Skip(this._builders, value0, value1);
				}

				// Token: 0x06006937 RID: 26935 RVA: 0x0015BC3F File Offset: 0x00159E3F
				public lines SplitLines(v value0)
				{
					return new SplitLines(this._builders, value0);
				}

				// Token: 0x06006938 RID: 26936 RVA: 0x0015BC52 File Offset: 0x00159E52
				public table RowMap(colSplit value0, records value1)
				{
					return new RowMap(this._builders, value0, value1);
				}

				// Token: 0x06006939 RID: 26937 RVA: 0x0015BC66 File Offset: 0x00159E66
				public _LetB2 LetPrepend(_LetB0 value0, _LetB1 value1)
				{
					return new LetPrepend(this._builders, value0, value1);
				}

				// Token: 0x0600693A RID: 26938 RVA: 0x0015BC7A File Offset: 0x00159E7A
				public colSplit LetSplit(split value0, _LetB2 value1)
				{
					return new LetSplit(this._builders, value0, value1);
				}

				// Token: 0x0600693B RID: 26939 RVA: 0x0015BC8E File Offset: 0x00159E8E
				public extractTup LetExtractTup(_LetB3 value0, trimExtract value1)
				{
					return new LetExtractTup(this._builders, value0, value1);
				}

				// Token: 0x04002EA9 RID: 11945
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F08 RID: 3848
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0600693C RID: 26940 RVA: 0x0015BCA2 File Offset: 0x00159EA2
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600693D RID: 26941 RVA: 0x0015BCB1 File Offset: 0x00159EB1
				public trimExtract trimExtract_extract(extract value0)
				{
					return new trimExtract_extract(this._builders, value0);
				}

				// Token: 0x0600693E RID: 26942 RVA: 0x0015BCC4 File Offset: 0x00159EC4
				public extract extract_row(row value0)
				{
					return new extract_row(this._builders, value0);
				}

				// Token: 0x0600693F RID: 26943 RVA: 0x0015BCD7 File Offset: 0x00159ED7
				public records records_skip(skip value0)
				{
					return new records_skip(this._builders, value0);
				}

				// Token: 0x06006940 RID: 26944 RVA: 0x0015BCEA File Offset: 0x00159EEA
				public skip skip_lines(lines value0)
				{
					return new skip_lines(this._builders, value0);
				}

				// Token: 0x04002EAA RID: 11946
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F09 RID: 3849
			public class NodeVariables
			{
				// Token: 0x170012F7 RID: 4855
				// (get) Token: 0x06006941 RID: 26945 RVA: 0x0015BCFD File Offset: 0x00159EFD
				// (set) Token: 0x06006942 RID: 26946 RVA: 0x0015BD05 File Offset: 0x00159F05
				public v v { get; private set; }

				// Token: 0x170012F8 RID: 4856
				// (get) Token: 0x06006943 RID: 26947 RVA: 0x0015BD0E File Offset: 0x00159F0E
				// (set) Token: 0x06006944 RID: 26948 RVA: 0x0015BD16 File Offset: 0x00159F16
				public row row { get; private set; }

				// Token: 0x170012F9 RID: 4857
				// (get) Token: 0x06006945 RID: 26949 RVA: 0x0015BD1F File Offset: 0x00159F1F
				// (set) Token: 0x06006946 RID: 26950 RVA: 0x0015BD27 File Offset: 0x00159F27
				public tup tup { get; private set; }

				// Token: 0x06006947 RID: 26951 RVA: 0x0015BD30 File Offset: 0x00159F30
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new v(builders);
					this.row = new row(builders);
					this.tup = new tup(builders);
				}
			}

			// Token: 0x02000F0A RID: 3850
			public class NodeHoles
			{
				// Token: 0x170012FA RID: 4858
				// (get) Token: 0x06006948 RID: 26952 RVA: 0x0015BD5C File Offset: 0x00159F5C
				// (set) Token: 0x06006949 RID: 26953 RVA: 0x0015BD64 File Offset: 0x00159F64
				public k k { get; private set; }

				// Token: 0x170012FB RID: 4859
				// (get) Token: 0x0600694A RID: 26954 RVA: 0x0015BD6D File Offset: 0x00159F6D
				// (set) Token: 0x0600694B RID: 26955 RVA: 0x0015BD75 File Offset: 0x00159F75
				public str str { get; private set; }

				// Token: 0x170012FC RID: 4860
				// (get) Token: 0x0600694C RID: 26956 RVA: 0x0015BD7E File Offset: 0x00159F7E
				// (set) Token: 0x0600694D RID: 26957 RVA: 0x0015BD86 File Offset: 0x00159F86
				public del del { get; private set; }

				// Token: 0x170012FD RID: 4861
				// (get) Token: 0x0600694E RID: 26958 RVA: 0x0015BD8F File Offset: 0x00159F8F
				// (set) Token: 0x0600694F RID: 26959 RVA: 0x0015BD97 File Offset: 0x00159F97
				public re re { get; private set; }

				// Token: 0x170012FE RID: 4862
				// (get) Token: 0x06006950 RID: 26960 RVA: 0x0015BDA0 File Offset: 0x00159FA0
				// (set) Token: 0x06006951 RID: 26961 RVA: 0x0015BDA8 File Offset: 0x00159FA8
				public columnNames columnNames { get; private set; }

				// Token: 0x170012FF RID: 4863
				// (get) Token: 0x06006952 RID: 26962 RVA: 0x0015BDB1 File Offset: 0x00159FB1
				// (set) Token: 0x06006953 RID: 26963 RVA: 0x0015BDB9 File Offset: 0x00159FB9
				public output output { get; private set; }

				// Token: 0x17001300 RID: 4864
				// (get) Token: 0x06006954 RID: 26964 RVA: 0x0015BDC2 File Offset: 0x00159FC2
				// (set) Token: 0x06006955 RID: 26965 RVA: 0x0015BDCA File Offset: 0x00159FCA
				public table table { get; private set; }

				// Token: 0x17001301 RID: 4865
				// (get) Token: 0x06006956 RID: 26966 RVA: 0x0015BDD3 File Offset: 0x00159FD3
				// (set) Token: 0x06006957 RID: 26967 RVA: 0x0015BDDB File Offset: 0x00159FDB
				public row row { get; private set; }

				// Token: 0x17001302 RID: 4866
				// (get) Token: 0x06006958 RID: 26968 RVA: 0x0015BDE4 File Offset: 0x00159FE4
				// (set) Token: 0x06006959 RID: 26969 RVA: 0x0015BDEC File Offset: 0x00159FEC
				public colSplit colSplit { get; private set; }

				// Token: 0x17001303 RID: 4867
				// (get) Token: 0x0600695A RID: 26970 RVA: 0x0015BDF5 File Offset: 0x00159FF5
				// (set) Token: 0x0600695B RID: 26971 RVA: 0x0015BDFD File Offset: 0x00159FFD
				public tup tup { get; private set; }

				// Token: 0x17001304 RID: 4868
				// (get) Token: 0x0600695C RID: 26972 RVA: 0x0015BE06 File Offset: 0x0015A006
				// (set) Token: 0x0600695D RID: 26973 RVA: 0x0015BE0E File Offset: 0x0015A00E
				public extractTup extractTup { get; private set; }

				// Token: 0x17001305 RID: 4869
				// (get) Token: 0x0600695E RID: 26974 RVA: 0x0015BE17 File Offset: 0x0015A017
				// (set) Token: 0x0600695F RID: 26975 RVA: 0x0015BE1F File Offset: 0x0015A01F
				public trimExtract trimExtract { get; private set; }

				// Token: 0x17001306 RID: 4870
				// (get) Token: 0x06006960 RID: 26976 RVA: 0x0015BE28 File Offset: 0x0015A028
				// (set) Token: 0x06006961 RID: 26977 RVA: 0x0015BE30 File Offset: 0x0015A030
				public extract extract { get; private set; }

				// Token: 0x17001307 RID: 4871
				// (get) Token: 0x06006962 RID: 26978 RVA: 0x0015BE39 File Offset: 0x0015A039
				// (set) Token: 0x06006963 RID: 26979 RVA: 0x0015BE41 File Offset: 0x0015A041
				public split split { get; private set; }

				// Token: 0x17001308 RID: 4872
				// (get) Token: 0x06006964 RID: 26980 RVA: 0x0015BE4A File Offset: 0x0015A04A
				// (set) Token: 0x06006965 RID: 26981 RVA: 0x0015BE52 File Offset: 0x0015A052
				public records records { get; private set; }

				// Token: 0x17001309 RID: 4873
				// (get) Token: 0x06006966 RID: 26982 RVA: 0x0015BE5B File Offset: 0x0015A05B
				// (set) Token: 0x06006967 RID: 26983 RVA: 0x0015BE63 File Offset: 0x0015A063
				public skip skip { get; private set; }

				// Token: 0x1700130A RID: 4874
				// (get) Token: 0x06006968 RID: 26984 RVA: 0x0015BE6C File Offset: 0x0015A06C
				// (set) Token: 0x06006969 RID: 26985 RVA: 0x0015BE74 File Offset: 0x0015A074
				public lines lines { get; private set; }

				// Token: 0x1700130B RID: 4875
				// (get) Token: 0x0600696A RID: 26986 RVA: 0x0015BE7D File Offset: 0x0015A07D
				// (set) Token: 0x0600696B RID: 26987 RVA: 0x0015BE85 File Offset: 0x0015A085
				public _LetB0 _LetB0 { get; private set; }

				// Token: 0x1700130C RID: 4876
				// (get) Token: 0x0600696C RID: 26988 RVA: 0x0015BE8E File Offset: 0x0015A08E
				// (set) Token: 0x0600696D RID: 26989 RVA: 0x0015BE96 File Offset: 0x0015A096
				public _LetB1 _LetB1 { get; private set; }

				// Token: 0x1700130D RID: 4877
				// (get) Token: 0x0600696E RID: 26990 RVA: 0x0015BE9F File Offset: 0x0015A09F
				// (set) Token: 0x0600696F RID: 26991 RVA: 0x0015BEA7 File Offset: 0x0015A0A7
				public _LetB2 _LetB2 { get; private set; }

				// Token: 0x1700130E RID: 4878
				// (get) Token: 0x06006970 RID: 26992 RVA: 0x0015BEB0 File Offset: 0x0015A0B0
				// (set) Token: 0x06006971 RID: 26993 RVA: 0x0015BEB8 File Offset: 0x0015A0B8
				public _LetB3 _LetB3 { get; private set; }

				// Token: 0x06006972 RID: 26994 RVA: 0x0015BEC4 File Offset: 0x0015A0C4
				public NodeHoles(GrammarBuilders builders)
				{
					this.k = k.CreateHole(builders, null);
					this.str = str.CreateHole(builders, null);
					this.del = del.CreateHole(builders, null);
					this.re = re.CreateHole(builders, null);
					this.columnNames = columnNames.CreateHole(builders, null);
					this.output = output.CreateHole(builders, null);
					this.table = table.CreateHole(builders, null);
					this.row = row.CreateHole(builders, null);
					this.colSplit = colSplit.CreateHole(builders, null);
					this.tup = tup.CreateHole(builders, null);
					this.extractTup = extractTup.CreateHole(builders, null);
					this.trimExtract = trimExtract.CreateHole(builders, null);
					this.extract = extract.CreateHole(builders, null);
					this.split = split.CreateHole(builders, null);
					this.records = records.CreateHole(builders, null);
					this.skip = skip.CreateHole(builders, null);
					this.lines = lines.CreateHole(builders, null);
					this._LetB0 = _LetB0.CreateHole(builders, null);
					this._LetB1 = _LetB1.CreateHole(builders, null);
					this._LetB2 = _LetB2.CreateHole(builders, null);
					this._LetB3 = _LetB3.CreateHole(builders, null);
				}
			}

			// Token: 0x02000F0B RID: 3851
			public class NodeUnsafe
			{
				// Token: 0x06006973 RID: 26995 RVA: 0x0015BFE8 File Offset: 0x0015A1E8
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x06006974 RID: 26996 RVA: 0x0015BFF0 File Offset: 0x0015A1F0
				public str str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str.CreateUnsafe(node);
				}

				// Token: 0x06006975 RID: 26997 RVA: 0x0015BFF8 File Offset: 0x0015A1F8
				public del del(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del.CreateUnsafe(node);
				}

				// Token: 0x06006976 RID: 26998 RVA: 0x0015C000 File Offset: 0x0015A200
				public re re(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re.CreateUnsafe(node);
				}

				// Token: 0x06006977 RID: 26999 RVA: 0x0015C008 File Offset: 0x0015A208
				public columnNames columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames.CreateUnsafe(node);
				}

				// Token: 0x06006978 RID: 27000 RVA: 0x0015C010 File Offset: 0x0015A210
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x06006979 RID: 27001 RVA: 0x0015C018 File Offset: 0x0015A218
				public table table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table.CreateUnsafe(node);
				}

				// Token: 0x0600697A RID: 27002 RVA: 0x0015C020 File Offset: 0x0015A220
				public row row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row.CreateUnsafe(node);
				}

				// Token: 0x0600697B RID: 27003 RVA: 0x0015C028 File Offset: 0x0015A228
				public colSplit colSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit.CreateUnsafe(node);
				}

				// Token: 0x0600697C RID: 27004 RVA: 0x0015C030 File Offset: 0x0015A230
				public tup tup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup.CreateUnsafe(node);
				}

				// Token: 0x0600697D RID: 27005 RVA: 0x0015C038 File Offset: 0x0015A238
				public extractTup extractTup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup.CreateUnsafe(node);
				}

				// Token: 0x0600697E RID: 27006 RVA: 0x0015C040 File Offset: 0x0015A240
				public trimExtract trimExtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract.CreateUnsafe(node);
				}

				// Token: 0x0600697F RID: 27007 RVA: 0x0015C048 File Offset: 0x0015A248
				public extract extract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract.CreateUnsafe(node);
				}

				// Token: 0x06006980 RID: 27008 RVA: 0x0015C050 File Offset: 0x0015A250
				public split split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split.CreateUnsafe(node);
				}

				// Token: 0x06006981 RID: 27009 RVA: 0x0015C058 File Offset: 0x0015A258
				public records records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records.CreateUnsafe(node);
				}

				// Token: 0x06006982 RID: 27010 RVA: 0x0015C060 File Offset: 0x0015A260
				public skip skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip.CreateUnsafe(node);
				}

				// Token: 0x06006983 RID: 27011 RVA: 0x0015C068 File Offset: 0x0015A268
				public lines lines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines.CreateUnsafe(node);
				}

				// Token: 0x06006984 RID: 27012 RVA: 0x0015C070 File Offset: 0x0015A270
				public _LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x06006985 RID: 27013 RVA: 0x0015C078 File Offset: 0x0015A278
				public _LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}

				// Token: 0x06006986 RID: 27014 RVA: 0x0015C080 File Offset: 0x0015A280
				public _LetB2 _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2.CreateUnsafe(node);
				}

				// Token: 0x06006987 RID: 27015 RVA: 0x0015C088 File Offset: 0x0015A288
				public _LetB3 _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3.CreateUnsafe(node);
				}
			}

			// Token: 0x02000F0C RID: 3852
			public class NodeCast
			{
				// Token: 0x06006989 RID: 27017 RVA: 0x0015C090 File Offset: 0x0015A290
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600698A RID: 27018 RVA: 0x0015C0A0 File Offset: 0x0015A2A0
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x0600698B RID: 27019 RVA: 0x0015C0F4 File Offset: 0x0015A2F4
				public str str(ProgramNode node)
				{
					str? str = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						string text = "node";
						string text2 = "expected node for symbol str but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return str.Value;
				}

				// Token: 0x0600698C RID: 27020 RVA: 0x0015C148 File Offset: 0x0015A348
				public del del(ProgramNode node)
				{
					del? del = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del.CreateSafe(this._builders, node);
					if (del == null)
					{
						string text = "node";
						string text2 = "expected node for symbol del but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return del.Value;
				}

				// Token: 0x0600698D RID: 27021 RVA: 0x0015C19C File Offset: 0x0015A39C
				public re re(ProgramNode node)
				{
					re? re = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re.CreateSafe(this._builders, node);
					if (re == null)
					{
						string text = "node";
						string text2 = "expected node for symbol re but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return re.Value;
				}

				// Token: 0x0600698E RID: 27022 RVA: 0x0015C1F0 File Offset: 0x0015A3F0
				public columnNames columnNames(ProgramNode node)
				{
					columnNames? columnNames = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnNames but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnNames.Value;
				}

				// Token: 0x0600698F RID: 27023 RVA: 0x0015C244 File Offset: 0x0015A444
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06006990 RID: 27024 RVA: 0x0015C298 File Offset: 0x0015A498
				public table table(ProgramNode node)
				{
					table? table = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table.CreateSafe(this._builders, node);
					if (table == null)
					{
						string text = "node";
						string text2 = "expected node for symbol table but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return table.Value;
				}

				// Token: 0x06006991 RID: 27025 RVA: 0x0015C2EC File Offset: 0x0015A4EC
				public row row(ProgramNode node)
				{
					row? row = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row.CreateSafe(this._builders, node);
					if (row == null)
					{
						string text = "node";
						string text2 = "expected node for symbol row but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return row.Value;
				}

				// Token: 0x06006992 RID: 27026 RVA: 0x0015C340 File Offset: 0x0015A540
				public colSplit colSplit(ProgramNode node)
				{
					colSplit? colSplit = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit.CreateSafe(this._builders, node);
					if (colSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol colSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return colSplit.Value;
				}

				// Token: 0x06006993 RID: 27027 RVA: 0x0015C394 File Offset: 0x0015A594
				public tup tup(ProgramNode node)
				{
					tup? tup = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup.CreateSafe(this._builders, node);
					if (tup == null)
					{
						string text = "node";
						string text2 = "expected node for symbol tup but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tup.Value;
				}

				// Token: 0x06006994 RID: 27028 RVA: 0x0015C3E8 File Offset: 0x0015A5E8
				public extractTup extractTup(ProgramNode node)
				{
					extractTup? extractTup = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup.CreateSafe(this._builders, node);
					if (extractTup == null)
					{
						string text = "node";
						string text2 = "expected node for symbol extractTup but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extractTup.Value;
				}

				// Token: 0x06006995 RID: 27029 RVA: 0x0015C43C File Offset: 0x0015A63C
				public trimExtract trimExtract(ProgramNode node)
				{
					trimExtract? trimExtract = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract.CreateSafe(this._builders, node);
					if (trimExtract == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimExtract but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimExtract.Value;
				}

				// Token: 0x06006996 RID: 27030 RVA: 0x0015C490 File Offset: 0x0015A690
				public extract extract(ProgramNode node)
				{
					extract? extract = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract.CreateSafe(this._builders, node);
					if (extract == null)
					{
						string text = "node";
						string text2 = "expected node for symbol extract but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extract.Value;
				}

				// Token: 0x06006997 RID: 27031 RVA: 0x0015C4E4 File Offset: 0x0015A6E4
				public split split(ProgramNode node)
				{
					split? split = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split.CreateSafe(this._builders, node);
					if (split == null)
					{
						string text = "node";
						string text2 = "expected node for symbol split but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return split.Value;
				}

				// Token: 0x06006998 RID: 27032 RVA: 0x0015C538 File Offset: 0x0015A738
				public records records(ProgramNode node)
				{
					records? records = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records.CreateSafe(this._builders, node);
					if (records == null)
					{
						string text = "node";
						string text2 = "expected node for symbol records but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return records.Value;
				}

				// Token: 0x06006999 RID: 27033 RVA: 0x0015C58C File Offset: 0x0015A78C
				public skip skip(ProgramNode node)
				{
					skip? skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skip but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skip.Value;
				}

				// Token: 0x0600699A RID: 27034 RVA: 0x0015C5E0 File Offset: 0x0015A7E0
				public lines lines(ProgramNode node)
				{
					lines? lines = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines.CreateSafe(this._builders, node);
					if (lines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol lines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lines.Value;
				}

				// Token: 0x0600699B RID: 27035 RVA: 0x0015C634 File Offset: 0x0015A834
				public _LetB0 _LetB0(ProgramNode node)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600699C RID: 27036 RVA: 0x0015C688 File Offset: 0x0015A888
				public _LetB1 _LetB1(ProgramNode node)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600699D RID: 27037 RVA: 0x0015C6DC File Offset: 0x0015A8DC
				public _LetB2 _LetB2(ProgramNode node)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600699E RID: 27038 RVA: 0x0015C730 File Offset: 0x0015A930
				public _LetB3 _LetB3(ProgramNode node)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x04002EC3 RID: 11971
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F0D RID: 3853
			public class RuleCast
			{
				// Token: 0x0600699F RID: 27039 RVA: 0x0015C781 File Offset: 0x0015A981
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060069A0 RID: 27040 RVA: 0x0015C790 File Offset: 0x0015A990
				public Table Table(ProgramNode node)
				{
					Table? table = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Table.CreateSafe(this._builders, node);
					if (table == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Table but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return table.Value;
				}

				// Token: 0x060069A1 RID: 27041 RVA: 0x0015C7E4 File Offset: 0x0015A9E4
				public RowMap RowMap(ProgramNode node)
				{
					RowMap? rowMap = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.RowMap.CreateSafe(this._builders, node);
					if (rowMap == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RowMap but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rowMap.Value;
				}

				// Token: 0x060069A2 RID: 27042 RVA: 0x0015C838 File Offset: 0x0015AA38
				public Second Second(ProgramNode node)
				{
					Second? second = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Second.CreateSafe(this._builders, node);
					if (second == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Second but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return second.Value;
				}

				// Token: 0x060069A3 RID: 27043 RVA: 0x0015C88C File Offset: 0x0015AA8C
				public Prepend Prepend(ProgramNode node)
				{
					Prepend? prepend = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
					if (prepend == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Prepend but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return prepend.Value;
				}

				// Token: 0x060069A4 RID: 27044 RVA: 0x0015C8E0 File Offset: 0x0015AAE0
				public LetPrepend LetPrepend(ProgramNode node)
				{
					LetPrepend? letPrepend = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetPrepend.CreateSafe(this._builders, node);
					if (letPrepend == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetPrepend but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letPrepend.Value;
				}

				// Token: 0x060069A5 RID: 27045 RVA: 0x0015C934 File Offset: 0x0015AB34
				public List List(ProgramNode node)
				{
					List? list = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
					if (list == null)
					{
						string text = "node";
						string text2 = "expected node for symbol List but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return list.Value;
				}

				// Token: 0x060069A6 RID: 27046 RVA: 0x0015C988 File Offset: 0x0015AB88
				public LetSplit LetSplit(ProgramNode node)
				{
					LetSplit? letSplit = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
					if (letSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSplit.Value;
				}

				// Token: 0x060069A7 RID: 27047 RVA: 0x0015C9DC File Offset: 0x0015ABDC
				public Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First First(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First? first = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First.CreateSafe(this._builders, node);
					if (first == null)
					{
						string text = "node";
						string text2 = "expected node for symbol First but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return first.Value;
				}

				// Token: 0x060069A8 RID: 27048 RVA: 0x0015CA30 File Offset: 0x0015AC30
				public LetExtractTup LetExtractTup(ProgramNode node)
				{
					LetExtractTup? letExtractTup = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetExtractTup.CreateSafe(this._builders, node);
					if (letExtractTup == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetExtractTup but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letExtractTup.Value;
				}

				// Token: 0x060069A9 RID: 27049 RVA: 0x0015CA84 File Offset: 0x0015AC84
				public trimExtract_extract trimExtract_extract(ProgramNode node)
				{
					trimExtract_extract? trimExtract_extract = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.trimExtract_extract.CreateSafe(this._builders, node);
					if (trimExtract_extract == null)
					{
						string text = "node";
						string text2 = "expected node for symbol trimExtract_extract but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimExtract_extract.Value;
				}

				// Token: 0x060069AA RID: 27050 RVA: 0x0015CAD8 File Offset: 0x0015ACD8
				public Trim Trim(ProgramNode node)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Trim but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trim.Value;
				}

				// Token: 0x060069AB RID: 27051 RVA: 0x0015CB2C File Offset: 0x0015AD2C
				public extract_row extract_row(ProgramNode node)
				{
					extract_row? extract_row = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.extract_row.CreateSafe(this._builders, node);
					if (extract_row == null)
					{
						string text = "node";
						string text2 = "expected node for symbol extract_row but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extract_row.Value;
				}

				// Token: 0x060069AC RID: 27052 RVA: 0x0015CB80 File Offset: 0x0015AD80
				public BetweenDelimiters BetweenDelimiters(ProgramNode node)
				{
					BetweenDelimiters? betweenDelimiters = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.BetweenDelimiters.CreateSafe(this._builders, node);
					if (betweenDelimiters == null)
					{
						string text = "node";
						string text2 = "expected node for symbol BetweenDelimiters but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return betweenDelimiters.Value;
				}

				// Token: 0x060069AD RID: 27053 RVA: 0x0015CBD4 File Offset: 0x0015ADD4
				public Substring Substring(ProgramNode node)
				{
					Substring? substring = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Substring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring.Value;
				}

				// Token: 0x060069AE RID: 27054 RVA: 0x0015CC28 File Offset: 0x0015AE28
				public Slice Slice(ProgramNode node)
				{
					Slice? slice = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Slice but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return slice.Value;
				}

				// Token: 0x060069AF RID: 27055 RVA: 0x0015CC7C File Offset: 0x0015AE7C
				public SplitPosition SplitPosition(ProgramNode node)
				{
					SplitPosition? splitPosition = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitPosition.CreateSafe(this._builders, node);
					if (splitPosition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitPosition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitPosition.Value;
				}

				// Token: 0x060069B0 RID: 27056 RVA: 0x0015CCD0 File Offset: 0x0015AED0
				public SplitDelimiter SplitDelimiter(ProgramNode node)
				{
					SplitDelimiter? splitDelimiter = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitDelimiter.CreateSafe(this._builders, node);
					if (splitDelimiter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitDelimiter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitDelimiter.Value;
				}

				// Token: 0x060069B1 RID: 27057 RVA: 0x0015CD24 File Offset: 0x0015AF24
				public records_skip records_skip(ProgramNode node)
				{
					records_skip? records_skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.records_skip.CreateSafe(this._builders, node);
					if (records_skip == null)
					{
						string text = "node";
						string text2 = "expected node for symbol records_skip but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return records_skip.Value;
				}

				// Token: 0x060069B2 RID: 27058 RVA: 0x0015CD78 File Offset: 0x0015AF78
				public Select Select(ProgramNode node)
				{
					Select? select = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
					if (select == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Select but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return select.Value;
				}

				// Token: 0x060069B3 RID: 27059 RVA: 0x0015CDCC File Offset: 0x0015AFCC
				public Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group Group(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group? group = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group.CreateSafe(this._builders, node);
					if (group == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Group but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return group.Value;
				}

				// Token: 0x060069B4 RID: 27060 RVA: 0x0015CE20 File Offset: 0x0015B020
				public MergeEvery MergeEvery(ProgramNode node)
				{
					MergeEvery? mergeEvery = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.MergeEvery.CreateSafe(this._builders, node);
					if (mergeEvery == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MergeEvery but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mergeEvery.Value;
				}

				// Token: 0x060069B5 RID: 27061 RVA: 0x0015CE74 File Offset: 0x0015B074
				public skip_lines skip_lines(ProgramNode node)
				{
					skip_lines? skip_lines = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.skip_lines.CreateSafe(this._builders, node);
					if (skip_lines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol skip_lines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skip_lines.Value;
				}

				// Token: 0x060069B6 RID: 27062 RVA: 0x0015CEC8 File Offset: 0x0015B0C8
				public Skip Skip(ProgramNode node)
				{
					Skip? skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Skip but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return skip.Value;
				}

				// Token: 0x060069B7 RID: 27063 RVA: 0x0015CF1C File Offset: 0x0015B11C
				public SplitLines SplitLines(ProgramNode node)
				{
					SplitLines? splitLines = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitLines.CreateSafe(this._builders, node);
					if (splitLines == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SplitLines but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return splitLines.Value;
				}

				// Token: 0x04002EC4 RID: 11972
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F0E RID: 3854
			public class NodeIs
			{
				// Token: 0x060069B8 RID: 27064 RVA: 0x0015CF6D File Offset: 0x0015B16D
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060069B9 RID: 27065 RVA: 0x0015CF7C File Offset: 0x0015B17C
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069BA RID: 27066 RVA: 0x0015CFA0 File Offset: 0x0015B1A0
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x060069BB RID: 27067 RVA: 0x0015CFDC File Offset: 0x0015B1DC
				public bool str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069BC RID: 27068 RVA: 0x0015D000 File Offset: 0x0015B200
				public bool str(ProgramNode node, out str value)
				{
					str? str = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						value = default(str);
						return false;
					}
					value = str.Value;
					return true;
				}

				// Token: 0x060069BD RID: 27069 RVA: 0x0015D03C File Offset: 0x0015B23C
				public bool del(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069BE RID: 27070 RVA: 0x0015D060 File Offset: 0x0015B260
				public bool del(ProgramNode node, out del value)
				{
					del? del = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del.CreateSafe(this._builders, node);
					if (del == null)
					{
						value = default(del);
						return false;
					}
					value = del.Value;
					return true;
				}

				// Token: 0x060069BF RID: 27071 RVA: 0x0015D09C File Offset: 0x0015B29C
				public bool re(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069C0 RID: 27072 RVA: 0x0015D0C0 File Offset: 0x0015B2C0
				public bool re(ProgramNode node, out re value)
				{
					re? re = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re.CreateSafe(this._builders, node);
					if (re == null)
					{
						value = default(re);
						return false;
					}
					value = re.Value;
					return true;
				}

				// Token: 0x060069C1 RID: 27073 RVA: 0x0015D0FC File Offset: 0x0015B2FC
				public bool columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069C2 RID: 27074 RVA: 0x0015D120 File Offset: 0x0015B320
				public bool columnNames(ProgramNode node, out columnNames value)
				{
					columnNames? columnNames = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
					if (columnNames == null)
					{
						value = default(columnNames);
						return false;
					}
					value = columnNames.Value;
					return true;
				}

				// Token: 0x060069C3 RID: 27075 RVA: 0x0015D15C File Offset: 0x0015B35C
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069C4 RID: 27076 RVA: 0x0015D180 File Offset: 0x0015B380
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x060069C5 RID: 27077 RVA: 0x0015D1BC File Offset: 0x0015B3BC
				public bool table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069C6 RID: 27078 RVA: 0x0015D1E0 File Offset: 0x0015B3E0
				public bool table(ProgramNode node, out table value)
				{
					table? table = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table.CreateSafe(this._builders, node);
					if (table == null)
					{
						value = default(table);
						return false;
					}
					value = table.Value;
					return true;
				}

				// Token: 0x060069C7 RID: 27079 RVA: 0x0015D21C File Offset: 0x0015B41C
				public bool row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069C8 RID: 27080 RVA: 0x0015D240 File Offset: 0x0015B440
				public bool row(ProgramNode node, out row value)
				{
					row? row = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row.CreateSafe(this._builders, node);
					if (row == null)
					{
						value = default(row);
						return false;
					}
					value = row.Value;
					return true;
				}

				// Token: 0x060069C9 RID: 27081 RVA: 0x0015D27C File Offset: 0x0015B47C
				public bool colSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069CA RID: 27082 RVA: 0x0015D2A0 File Offset: 0x0015B4A0
				public bool colSplit(ProgramNode node, out colSplit value)
				{
					colSplit? colSplit = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit.CreateSafe(this._builders, node);
					if (colSplit == null)
					{
						value = default(colSplit);
						return false;
					}
					value = colSplit.Value;
					return true;
				}

				// Token: 0x060069CB RID: 27083 RVA: 0x0015D2DC File Offset: 0x0015B4DC
				public bool tup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069CC RID: 27084 RVA: 0x0015D300 File Offset: 0x0015B500
				public bool tup(ProgramNode node, out tup value)
				{
					tup? tup = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup.CreateSafe(this._builders, node);
					if (tup == null)
					{
						value = default(tup);
						return false;
					}
					value = tup.Value;
					return true;
				}

				// Token: 0x060069CD RID: 27085 RVA: 0x0015D33C File Offset: 0x0015B53C
				public bool extractTup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069CE RID: 27086 RVA: 0x0015D360 File Offset: 0x0015B560
				public bool extractTup(ProgramNode node, out extractTup value)
				{
					extractTup? extractTup = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup.CreateSafe(this._builders, node);
					if (extractTup == null)
					{
						value = default(extractTup);
						return false;
					}
					value = extractTup.Value;
					return true;
				}

				// Token: 0x060069CF RID: 27087 RVA: 0x0015D39C File Offset: 0x0015B59C
				public bool trimExtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069D0 RID: 27088 RVA: 0x0015D3C0 File Offset: 0x0015B5C0
				public bool trimExtract(ProgramNode node, out trimExtract value)
				{
					trimExtract? trimExtract = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract.CreateSafe(this._builders, node);
					if (trimExtract == null)
					{
						value = default(trimExtract);
						return false;
					}
					value = trimExtract.Value;
					return true;
				}

				// Token: 0x060069D1 RID: 27089 RVA: 0x0015D3FC File Offset: 0x0015B5FC
				public bool extract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069D2 RID: 27090 RVA: 0x0015D420 File Offset: 0x0015B620
				public bool extract(ProgramNode node, out extract value)
				{
					extract? extract = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract.CreateSafe(this._builders, node);
					if (extract == null)
					{
						value = default(extract);
						return false;
					}
					value = extract.Value;
					return true;
				}

				// Token: 0x060069D3 RID: 27091 RVA: 0x0015D45C File Offset: 0x0015B65C
				public bool split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069D4 RID: 27092 RVA: 0x0015D480 File Offset: 0x0015B680
				public bool split(ProgramNode node, out split value)
				{
					split? split = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split.CreateSafe(this._builders, node);
					if (split == null)
					{
						value = default(split);
						return false;
					}
					value = split.Value;
					return true;
				}

				// Token: 0x060069D5 RID: 27093 RVA: 0x0015D4BC File Offset: 0x0015B6BC
				public bool records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069D6 RID: 27094 RVA: 0x0015D4E0 File Offset: 0x0015B6E0
				public bool records(ProgramNode node, out records value)
				{
					records? records = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records.CreateSafe(this._builders, node);
					if (records == null)
					{
						value = default(records);
						return false;
					}
					value = records.Value;
					return true;
				}

				// Token: 0x060069D7 RID: 27095 RVA: 0x0015D51C File Offset: 0x0015B71C
				public bool skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069D8 RID: 27096 RVA: 0x0015D540 File Offset: 0x0015B740
				public bool skip(ProgramNode node, out skip value)
				{
					skip? skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						value = default(skip);
						return false;
					}
					value = skip.Value;
					return true;
				}

				// Token: 0x060069D9 RID: 27097 RVA: 0x0015D57C File Offset: 0x0015B77C
				public bool lines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069DA RID: 27098 RVA: 0x0015D5A0 File Offset: 0x0015B7A0
				public bool lines(ProgramNode node, out lines value)
				{
					lines? lines = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines.CreateSafe(this._builders, node);
					if (lines == null)
					{
						value = default(lines);
						return false;
					}
					value = lines.Value;
					return true;
				}

				// Token: 0x060069DB RID: 27099 RVA: 0x0015D5DC File Offset: 0x0015B7DC
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069DC RID: 27100 RVA: 0x0015D600 File Offset: 0x0015B800
				public bool _LetB0(ProgramNode node, out _LetB0 value)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x060069DD RID: 27101 RVA: 0x0015D63C File Offset: 0x0015B83C
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069DE RID: 27102 RVA: 0x0015D660 File Offset: 0x0015B860
				public bool _LetB1(ProgramNode node, out _LetB1 value)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x060069DF RID: 27103 RVA: 0x0015D69C File Offset: 0x0015B89C
				public bool _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069E0 RID: 27104 RVA: 0x0015D6C0 File Offset: 0x0015B8C0
				public bool _LetB2(ProgramNode node, out _LetB2 value)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB2);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x060069E1 RID: 27105 RVA: 0x0015D6FC File Offset: 0x0015B8FC
				public bool _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069E2 RID: 27106 RVA: 0x0015D720 File Offset: 0x0015B920
				public bool _LetB3(ProgramNode node, out _LetB3 value)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB3);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x04002EC5 RID: 11973
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F0F RID: 3855
			public class RuleIs
			{
				// Token: 0x060069E3 RID: 27107 RVA: 0x0015D75A File Offset: 0x0015B95A
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060069E4 RID: 27108 RVA: 0x0015D76C File Offset: 0x0015B96C
				public bool Table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Table.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069E5 RID: 27109 RVA: 0x0015D790 File Offset: 0x0015B990
				public bool Table(ProgramNode node, out Table value)
				{
					Table? table = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Table.CreateSafe(this._builders, node);
					if (table == null)
					{
						value = default(Table);
						return false;
					}
					value = table.Value;
					return true;
				}

				// Token: 0x060069E6 RID: 27110 RVA: 0x0015D7CC File Offset: 0x0015B9CC
				public bool RowMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.RowMap.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069E7 RID: 27111 RVA: 0x0015D7F0 File Offset: 0x0015B9F0
				public bool RowMap(ProgramNode node, out RowMap value)
				{
					RowMap? rowMap = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.RowMap.CreateSafe(this._builders, node);
					if (rowMap == null)
					{
						value = default(RowMap);
						return false;
					}
					value = rowMap.Value;
					return true;
				}

				// Token: 0x060069E8 RID: 27112 RVA: 0x0015D82C File Offset: 0x0015BA2C
				public bool Second(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Second.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069E9 RID: 27113 RVA: 0x0015D850 File Offset: 0x0015BA50
				public bool Second(ProgramNode node, out Second value)
				{
					Second? second = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Second.CreateSafe(this._builders, node);
					if (second == null)
					{
						value = default(Second);
						return false;
					}
					value = second.Value;
					return true;
				}

				// Token: 0x060069EA RID: 27114 RVA: 0x0015D88C File Offset: 0x0015BA8C
				public bool Prepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069EB RID: 27115 RVA: 0x0015D8B0 File Offset: 0x0015BAB0
				public bool Prepend(ProgramNode node, out Prepend value)
				{
					Prepend? prepend = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
					if (prepend == null)
					{
						value = default(Prepend);
						return false;
					}
					value = prepend.Value;
					return true;
				}

				// Token: 0x060069EC RID: 27116 RVA: 0x0015D8EC File Offset: 0x0015BAEC
				public bool LetPrepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetPrepend.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069ED RID: 27117 RVA: 0x0015D910 File Offset: 0x0015BB10
				public bool LetPrepend(ProgramNode node, out LetPrepend value)
				{
					LetPrepend? letPrepend = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetPrepend.CreateSafe(this._builders, node);
					if (letPrepend == null)
					{
						value = default(LetPrepend);
						return false;
					}
					value = letPrepend.Value;
					return true;
				}

				// Token: 0x060069EE RID: 27118 RVA: 0x0015D94C File Offset: 0x0015BB4C
				public bool List(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069EF RID: 27119 RVA: 0x0015D970 File Offset: 0x0015BB70
				public bool List(ProgramNode node, out List value)
				{
					List? list = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
					if (list == null)
					{
						value = default(List);
						return false;
					}
					value = list.Value;
					return true;
				}

				// Token: 0x060069F0 RID: 27120 RVA: 0x0015D9AC File Offset: 0x0015BBAC
				public bool LetSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069F1 RID: 27121 RVA: 0x0015D9D0 File Offset: 0x0015BBD0
				public bool LetSplit(ProgramNode node, out LetSplit value)
				{
					LetSplit? letSplit = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
					if (letSplit == null)
					{
						value = default(LetSplit);
						return false;
					}
					value = letSplit.Value;
					return true;
				}

				// Token: 0x060069F2 RID: 27122 RVA: 0x0015DA0C File Offset: 0x0015BC0C
				public bool First(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069F3 RID: 27123 RVA: 0x0015DA30 File Offset: 0x0015BC30
				public bool First(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First value)
				{
					Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First? first = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First.CreateSafe(this._builders, node);
					if (first == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First);
						return false;
					}
					value = first.Value;
					return true;
				}

				// Token: 0x060069F4 RID: 27124 RVA: 0x0015DA6C File Offset: 0x0015BC6C
				public bool LetExtractTup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetExtractTup.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069F5 RID: 27125 RVA: 0x0015DA90 File Offset: 0x0015BC90
				public bool LetExtractTup(ProgramNode node, out LetExtractTup value)
				{
					LetExtractTup? letExtractTup = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetExtractTup.CreateSafe(this._builders, node);
					if (letExtractTup == null)
					{
						value = default(LetExtractTup);
						return false;
					}
					value = letExtractTup.Value;
					return true;
				}

				// Token: 0x060069F6 RID: 27126 RVA: 0x0015DACC File Offset: 0x0015BCCC
				public bool trimExtract_extract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.trimExtract_extract.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069F7 RID: 27127 RVA: 0x0015DAF0 File Offset: 0x0015BCF0
				public bool trimExtract_extract(ProgramNode node, out trimExtract_extract value)
				{
					trimExtract_extract? trimExtract_extract = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.trimExtract_extract.CreateSafe(this._builders, node);
					if (trimExtract_extract == null)
					{
						value = default(trimExtract_extract);
						return false;
					}
					value = trimExtract_extract.Value;
					return true;
				}

				// Token: 0x060069F8 RID: 27128 RVA: 0x0015DB2C File Offset: 0x0015BD2C
				public bool Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069F9 RID: 27129 RVA: 0x0015DB50 File Offset: 0x0015BD50
				public bool Trim(ProgramNode node, out Trim value)
				{
					Trim? trim = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
					if (trim == null)
					{
						value = default(Trim);
						return false;
					}
					value = trim.Value;
					return true;
				}

				// Token: 0x060069FA RID: 27130 RVA: 0x0015DB8C File Offset: 0x0015BD8C
				public bool extract_row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.extract_row.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069FB RID: 27131 RVA: 0x0015DBB0 File Offset: 0x0015BDB0
				public bool extract_row(ProgramNode node, out extract_row value)
				{
					extract_row? extract_row = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.extract_row.CreateSafe(this._builders, node);
					if (extract_row == null)
					{
						value = default(extract_row);
						return false;
					}
					value = extract_row.Value;
					return true;
				}

				// Token: 0x060069FC RID: 27132 RVA: 0x0015DBEC File Offset: 0x0015BDEC
				public bool BetweenDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.BetweenDelimiters.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069FD RID: 27133 RVA: 0x0015DC10 File Offset: 0x0015BE10
				public bool BetweenDelimiters(ProgramNode node, out BetweenDelimiters value)
				{
					BetweenDelimiters? betweenDelimiters = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.BetweenDelimiters.CreateSafe(this._builders, node);
					if (betweenDelimiters == null)
					{
						value = default(BetweenDelimiters);
						return false;
					}
					value = betweenDelimiters.Value;
					return true;
				}

				// Token: 0x060069FE RID: 27134 RVA: 0x0015DC4C File Offset: 0x0015BE4C
				public bool Substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060069FF RID: 27135 RVA: 0x0015DC70 File Offset: 0x0015BE70
				public bool Substring(ProgramNode node, out Substring value)
				{
					Substring? substring = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						value = default(Substring);
						return false;
					}
					value = substring.Value;
					return true;
				}

				// Token: 0x06006A00 RID: 27136 RVA: 0x0015DCAC File Offset: 0x0015BEAC
				public bool Slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A01 RID: 27137 RVA: 0x0015DCD0 File Offset: 0x0015BED0
				public bool Slice(ProgramNode node, out Slice value)
				{
					Slice? slice = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
					if (slice == null)
					{
						value = default(Slice);
						return false;
					}
					value = slice.Value;
					return true;
				}

				// Token: 0x06006A02 RID: 27138 RVA: 0x0015DD0C File Offset: 0x0015BF0C
				public bool SplitPosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitPosition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A03 RID: 27139 RVA: 0x0015DD30 File Offset: 0x0015BF30
				public bool SplitPosition(ProgramNode node, out SplitPosition value)
				{
					SplitPosition? splitPosition = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitPosition.CreateSafe(this._builders, node);
					if (splitPosition == null)
					{
						value = default(SplitPosition);
						return false;
					}
					value = splitPosition.Value;
					return true;
				}

				// Token: 0x06006A04 RID: 27140 RVA: 0x0015DD6C File Offset: 0x0015BF6C
				public bool SplitDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitDelimiter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A05 RID: 27141 RVA: 0x0015DD90 File Offset: 0x0015BF90
				public bool SplitDelimiter(ProgramNode node, out SplitDelimiter value)
				{
					SplitDelimiter? splitDelimiter = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitDelimiter.CreateSafe(this._builders, node);
					if (splitDelimiter == null)
					{
						value = default(SplitDelimiter);
						return false;
					}
					value = splitDelimiter.Value;
					return true;
				}

				// Token: 0x06006A06 RID: 27142 RVA: 0x0015DDCC File Offset: 0x0015BFCC
				public bool records_skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.records_skip.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A07 RID: 27143 RVA: 0x0015DDF0 File Offset: 0x0015BFF0
				public bool records_skip(ProgramNode node, out records_skip value)
				{
					records_skip? records_skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.records_skip.CreateSafe(this._builders, node);
					if (records_skip == null)
					{
						value = default(records_skip);
						return false;
					}
					value = records_skip.Value;
					return true;
				}

				// Token: 0x06006A08 RID: 27144 RVA: 0x0015DE2C File Offset: 0x0015C02C
				public bool Select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A09 RID: 27145 RVA: 0x0015DE50 File Offset: 0x0015C050
				public bool Select(ProgramNode node, out Select value)
				{
					Select? select = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
					if (select == null)
					{
						value = default(Select);
						return false;
					}
					value = select.Value;
					return true;
				}

				// Token: 0x06006A0A RID: 27146 RVA: 0x0015DE8C File Offset: 0x0015C08C
				public bool Group(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A0B RID: 27147 RVA: 0x0015DEB0 File Offset: 0x0015C0B0
				public bool Group(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group value)
				{
					Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group? group = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group.CreateSafe(this._builders, node);
					if (group == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group);
						return false;
					}
					value = group.Value;
					return true;
				}

				// Token: 0x06006A0C RID: 27148 RVA: 0x0015DEEC File Offset: 0x0015C0EC
				public bool MergeEvery(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.MergeEvery.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A0D RID: 27149 RVA: 0x0015DF10 File Offset: 0x0015C110
				public bool MergeEvery(ProgramNode node, out MergeEvery value)
				{
					MergeEvery? mergeEvery = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.MergeEvery.CreateSafe(this._builders, node);
					if (mergeEvery == null)
					{
						value = default(MergeEvery);
						return false;
					}
					value = mergeEvery.Value;
					return true;
				}

				// Token: 0x06006A0E RID: 27150 RVA: 0x0015DF4C File Offset: 0x0015C14C
				public bool skip_lines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.skip_lines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A0F RID: 27151 RVA: 0x0015DF70 File Offset: 0x0015C170
				public bool skip_lines(ProgramNode node, out skip_lines value)
				{
					skip_lines? skip_lines = Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.skip_lines.CreateSafe(this._builders, node);
					if (skip_lines == null)
					{
						value = default(skip_lines);
						return false;
					}
					value = skip_lines.Value;
					return true;
				}

				// Token: 0x06006A10 RID: 27152 RVA: 0x0015DFAC File Offset: 0x0015C1AC
				public bool Skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A11 RID: 27153 RVA: 0x0015DFD0 File Offset: 0x0015C1D0
				public bool Skip(ProgramNode node, out Skip value)
				{
					Skip? skip = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
					if (skip == null)
					{
						value = default(Skip);
						return false;
					}
					value = skip.Value;
					return true;
				}

				// Token: 0x06006A12 RID: 27154 RVA: 0x0015E00C File Offset: 0x0015C20C
				public bool SplitLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitLines.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06006A13 RID: 27155 RVA: 0x0015E030 File Offset: 0x0015C230
				public bool SplitLines(ProgramNode node, out SplitLines value)
				{
					SplitLines? splitLines = Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitLines.CreateSafe(this._builders, node);
					if (splitLines == null)
					{
						value = default(SplitLines);
						return false;
					}
					value = splitLines.Value;
					return true;
				}

				// Token: 0x04002EC6 RID: 11974
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F10 RID: 3856
			public class NodeAs
			{
				// Token: 0x06006A14 RID: 27156 RVA: 0x0015E06A File Offset: 0x0015C26A
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A15 RID: 27157 RVA: 0x0015E079 File Offset: 0x0015C279
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A16 RID: 27158 RVA: 0x0015E087 File Offset: 0x0015C287
				public str? str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A17 RID: 27159 RVA: 0x0015E095 File Offset: 0x0015C295
				public del? del(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A18 RID: 27160 RVA: 0x0015E0A3 File Offset: 0x0015C2A3
				public re? re(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A19 RID: 27161 RVA: 0x0015E0B1 File Offset: 0x0015C2B1
				public columnNames? columnNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1A RID: 27162 RVA: 0x0015E0BF File Offset: 0x0015C2BF
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1B RID: 27163 RVA: 0x0015E0CD File Offset: 0x0015C2CD
				public table? table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1C RID: 27164 RVA: 0x0015E0DB File Offset: 0x0015C2DB
				public row? row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1D RID: 27165 RVA: 0x0015E0E9 File Offset: 0x0015C2E9
				public colSplit? colSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1E RID: 27166 RVA: 0x0015E0F7 File Offset: 0x0015C2F7
				public tup? tup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A1F RID: 27167 RVA: 0x0015E105 File Offset: 0x0015C305
				public extractTup? extractTup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A20 RID: 27168 RVA: 0x0015E113 File Offset: 0x0015C313
				public trimExtract? trimExtract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A21 RID: 27169 RVA: 0x0015E121 File Offset: 0x0015C321
				public extract? extract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A22 RID: 27170 RVA: 0x0015E12F File Offset: 0x0015C32F
				public split? split(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A23 RID: 27171 RVA: 0x0015E13D File Offset: 0x0015C33D
				public records? records(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A24 RID: 27172 RVA: 0x0015E14B File Offset: 0x0015C34B
				public skip? skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A25 RID: 27173 RVA: 0x0015E159 File Offset: 0x0015C359
				public lines? lines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A26 RID: 27174 RVA: 0x0015E167 File Offset: 0x0015C367
				public _LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A27 RID: 27175 RVA: 0x0015E175 File Offset: 0x0015C375
				public _LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A28 RID: 27176 RVA: 0x0015E183 File Offset: 0x0015C383
				public _LetB2? _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A29 RID: 27177 RVA: 0x0015E191 File Offset: 0x0015C391
				public _LetB3? _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
				}

				// Token: 0x04002EC7 RID: 11975
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F11 RID: 3857
			public class RuleAs
			{
				// Token: 0x06006A2A RID: 27178 RVA: 0x0015E19F File Offset: 0x0015C39F
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A2B RID: 27179 RVA: 0x0015E1AE File Offset: 0x0015C3AE
				public Table? Table(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Table.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A2C RID: 27180 RVA: 0x0015E1BC File Offset: 0x0015C3BC
				public RowMap? RowMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.RowMap.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A2D RID: 27181 RVA: 0x0015E1CA File Offset: 0x0015C3CA
				public Second? Second(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Second.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A2E RID: 27182 RVA: 0x0015E1D8 File Offset: 0x0015C3D8
				public Prepend? Prepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A2F RID: 27183 RVA: 0x0015E1E6 File Offset: 0x0015C3E6
				public LetPrepend? LetPrepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetPrepend.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A30 RID: 27184 RVA: 0x0015E1F4 File Offset: 0x0015C3F4
				public List? List(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.List.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A31 RID: 27185 RVA: 0x0015E202 File Offset: 0x0015C402
				public LetSplit? LetSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A32 RID: 27186 RVA: 0x0015E210 File Offset: 0x0015C410
				public Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First? First(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.First.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A33 RID: 27187 RVA: 0x0015E21E File Offset: 0x0015C41E
				public LetExtractTup? LetExtractTup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.LetExtractTup.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A34 RID: 27188 RVA: 0x0015E22C File Offset: 0x0015C42C
				public trimExtract_extract? trimExtract_extract(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.trimExtract_extract.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A35 RID: 27189 RVA: 0x0015E23A File Offset: 0x0015C43A
				public Trim? Trim(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Trim.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A36 RID: 27190 RVA: 0x0015E248 File Offset: 0x0015C448
				public extract_row? extract_row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.extract_row.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A37 RID: 27191 RVA: 0x0015E256 File Offset: 0x0015C456
				public BetweenDelimiters? BetweenDelimiters(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.BetweenDelimiters.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A38 RID: 27192 RVA: 0x0015E264 File Offset: 0x0015C464
				public Substring? Substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A39 RID: 27193 RVA: 0x0015E272 File Offset: 0x0015C472
				public Slice? Slice(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Slice.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3A RID: 27194 RVA: 0x0015E280 File Offset: 0x0015C480
				public SplitPosition? SplitPosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitPosition.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3B RID: 27195 RVA: 0x0015E28E File Offset: 0x0015C48E
				public SplitDelimiter? SplitDelimiter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitDelimiter.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3C RID: 27196 RVA: 0x0015E29C File Offset: 0x0015C49C
				public records_skip? records_skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.records_skip.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3D RID: 27197 RVA: 0x0015E2AA File Offset: 0x0015C4AA
				public Select? Select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3E RID: 27198 RVA: 0x0015E2B8 File Offset: 0x0015C4B8
				public Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group? Group(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A3F RID: 27199 RVA: 0x0015E2C6 File Offset: 0x0015C4C6
				public MergeEvery? MergeEvery(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.MergeEvery.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A40 RID: 27200 RVA: 0x0015E2D4 File Offset: 0x0015C4D4
				public skip_lines? skip_lines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes.skip_lines.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A41 RID: 27201 RVA: 0x0015E2E2 File Offset: 0x0015C4E2
				public Skip? Skip(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Skip.CreateSafe(this._builders, node);
				}

				// Token: 0x06006A42 RID: 27202 RVA: 0x0015E2F0 File Offset: 0x0015C4F0
				public SplitLines? SplitLines(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.SplitLines.CreateSafe(this._builders, node);
				}

				// Token: 0x04002EC8 RID: 11976
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000F13 RID: 3859
		public class Sets
		{
			// Token: 0x06006A46 RID: 27206 RVA: 0x0015E318 File Offset: 0x0015C518
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x1700130F RID: 4879
			// (get) Token: 0x06006A47 RID: 27207 RVA: 0x0015E367 File Offset: 0x0015C567
			// (set) Token: 0x06006A48 RID: 27208 RVA: 0x0015E36F File Offset: 0x0015C56F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17001310 RID: 4880
			// (get) Token: 0x06006A49 RID: 27209 RVA: 0x0015E378 File Offset: 0x0015C578
			// (set) Token: 0x06006A4A RID: 27210 RVA: 0x0015E380 File Offset: 0x0015C580
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17001311 RID: 4881
			// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0015E389 File Offset: 0x0015C589
			// (set) Token: 0x06006A4C RID: 27212 RVA: 0x0015E391 File Offset: 0x0015C591
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17001312 RID: 4882
			// (get) Token: 0x06006A4D RID: 27213 RVA: 0x0015E39A File Offset: 0x0015C59A
			// (set) Token: 0x06006A4E RID: 27214 RVA: 0x0015E3A2 File Offset: 0x0015C5A2
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17001313 RID: 4883
			// (get) Token: 0x06006A4F RID: 27215 RVA: 0x0015E3AB File Offset: 0x0015C5AB
			// (set) Token: 0x06006A50 RID: 27216 RVA: 0x0015E3B3 File Offset: 0x0015C5B3
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000F14 RID: 3860
			public class Joins
			{
				// Token: 0x06006A51 RID: 27217 RVA: 0x0015E3BC File Offset: 0x0015C5BC
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A52 RID: 27218 RVA: 0x0015E3CB File Offset: 0x0015C5CB
				public ProgramSetBuilder<output> Table(ProgramSetBuilder<columnNames> value0, ProgramSetBuilder<table> value1)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Table, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A53 RID: 27219 RVA: 0x0015E40B File Offset: 0x0015C60B
				public ProgramSetBuilder<_LetB0> Second(ProgramSetBuilder<tup> value0)
				{
					return ProgramSetBuilder<_LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Second, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A54 RID: 27220 RVA: 0x0015E43C File Offset: 0x0015C63C
				public ProgramSetBuilder<_LetB1> Prepend(ProgramSetBuilder<extractTup> value0, ProgramSetBuilder<colSplit> value1)
				{
					return ProgramSetBuilder<_LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Prepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A55 RID: 27221 RVA: 0x0015E47C File Offset: 0x0015C67C
				public ProgramSetBuilder<colSplit> List(ProgramSetBuilder<trimExtract> value0)
				{
					return ProgramSetBuilder<colSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.List, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A56 RID: 27222 RVA: 0x0015E4AD File Offset: 0x0015C6AD
				public ProgramSetBuilder<_LetB3> First(ProgramSetBuilder<tup> value0)
				{
					return ProgramSetBuilder<_LetB3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.First, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A57 RID: 27223 RVA: 0x0015E4DE File Offset: 0x0015C6DE
				public ProgramSetBuilder<trimExtract> Trim(ProgramSetBuilder<extract> value0)
				{
					return ProgramSetBuilder<trimExtract>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A58 RID: 27224 RVA: 0x0015E510 File Offset: 0x0015C710
				public ProgramSetBuilder<extract> BetweenDelimiters(ProgramSetBuilder<row> value0, ProgramSetBuilder<del> value1, ProgramSetBuilder<del> value2)
				{
					return ProgramSetBuilder<extract>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.BetweenDelimiters, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A59 RID: 27225 RVA: 0x0015E56C File Offset: 0x0015C76C
				public ProgramSetBuilder<extract> Substring(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<extract>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Substring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A5A RID: 27226 RVA: 0x0015E5C8 File Offset: 0x0015C7C8
				public ProgramSetBuilder<extract> Slice(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<extract>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Slice, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A5B RID: 27227 RVA: 0x0015E622 File Offset: 0x0015C822
				public ProgramSetBuilder<split> SplitPosition(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<split>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitPosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A5C RID: 27228 RVA: 0x0015E664 File Offset: 0x0015C864
				public ProgramSetBuilder<split> SplitDelimiter(ProgramSetBuilder<row> value0, ProgramSetBuilder<str> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<split>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitDelimiter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A5D RID: 27229 RVA: 0x0015E6BE File Offset: 0x0015C8BE
				public ProgramSetBuilder<records> Select(ProgramSetBuilder<re> value0, ProgramSetBuilder<skip> value1)
				{
					return ProgramSetBuilder<records>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Select, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A5E RID: 27230 RVA: 0x0015E6FE File Offset: 0x0015C8FE
				public ProgramSetBuilder<records> Group(ProgramSetBuilder<re> value0, ProgramSetBuilder<skip> value1)
				{
					return ProgramSetBuilder<records>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Group, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A5F RID: 27231 RVA: 0x0015E73E File Offset: 0x0015C93E
				public ProgramSetBuilder<records> MergeEvery(ProgramSetBuilder<k> value0, ProgramSetBuilder<skip> value1)
				{
					return ProgramSetBuilder<records>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MergeEvery, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A60 RID: 27232 RVA: 0x0015E77E File Offset: 0x0015C97E
				public ProgramSetBuilder<skip> Skip(ProgramSetBuilder<k> value0, ProgramSetBuilder<lines> value1)
				{
					return ProgramSetBuilder<skip>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Skip, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A61 RID: 27233 RVA: 0x0015E7BE File Offset: 0x0015C9BE
				public ProgramSetBuilder<lines> SplitLines(ProgramSetBuilder<v> value0)
				{
					return ProgramSetBuilder<lines>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SplitLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A62 RID: 27234 RVA: 0x0015E7EF File Offset: 0x0015C9EF
				public ProgramSetBuilder<table> RowMap(ProgramSetBuilder<colSplit> value0, ProgramSetBuilder<records> value1)
				{
					return ProgramSetBuilder<table>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RowMap, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A63 RID: 27235 RVA: 0x0015E82F File Offset: 0x0015CA2F
				public ProgramSetBuilder<_LetB2> LetPrepend(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return ProgramSetBuilder<_LetB2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetPrepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A64 RID: 27236 RVA: 0x0015E86F File Offset: 0x0015CA6F
				public ProgramSetBuilder<colSplit> LetSplit(ProgramSetBuilder<split> value0, ProgramSetBuilder<_LetB2> value1)
				{
					return ProgramSetBuilder<colSplit>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A65 RID: 27237 RVA: 0x0015E8AF File Offset: 0x0015CAAF
				public ProgramSetBuilder<extractTup> LetExtractTup(ProgramSetBuilder<_LetB3> value0, ProgramSetBuilder<trimExtract> value1)
				{
					return ProgramSetBuilder<extractTup>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetExtractTup, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04002ECF RID: 11983
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F15 RID: 3861
			public class ExplicitJoins
			{
				// Token: 0x06006A66 RID: 27238 RVA: 0x0015E8EF File Offset: 0x0015CAEF
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A67 RID: 27239 RVA: 0x0015E8FE File Offset: 0x0015CAFE
				public JoinProgramSetBuilder<output> Table(ProgramSetBuilder<columnNames> value0, ProgramSetBuilder<table> value1)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Table, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A68 RID: 27240 RVA: 0x0015E93E File Offset: 0x0015CB3E
				public JoinProgramSetBuilder<_LetB0> Second(ProgramSetBuilder<tup> value0)
				{
					return JoinProgramSetBuilder<_LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Second, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A69 RID: 27241 RVA: 0x0015E96F File Offset: 0x0015CB6F
				public JoinProgramSetBuilder<_LetB1> Prepend(ProgramSetBuilder<extractTup> value0, ProgramSetBuilder<colSplit> value1)
				{
					return JoinProgramSetBuilder<_LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Prepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A6A RID: 27242 RVA: 0x0015E9AF File Offset: 0x0015CBAF
				public JoinProgramSetBuilder<colSplit> List(ProgramSetBuilder<trimExtract> value0)
				{
					return JoinProgramSetBuilder<colSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.List, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A6B RID: 27243 RVA: 0x0015E9E0 File Offset: 0x0015CBE0
				public JoinProgramSetBuilder<_LetB3> First(ProgramSetBuilder<tup> value0)
				{
					return JoinProgramSetBuilder<_LetB3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.First, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A6C RID: 27244 RVA: 0x0015EA11 File Offset: 0x0015CC11
				public JoinProgramSetBuilder<trimExtract> Trim(ProgramSetBuilder<extract> value0)
				{
					return JoinProgramSetBuilder<trimExtract>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Trim, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A6D RID: 27245 RVA: 0x0015EA44 File Offset: 0x0015CC44
				public JoinProgramSetBuilder<extract> BetweenDelimiters(ProgramSetBuilder<row> value0, ProgramSetBuilder<del> value1, ProgramSetBuilder<del> value2)
				{
					return JoinProgramSetBuilder<extract>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.BetweenDelimiters, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A6E RID: 27246 RVA: 0x0015EAA0 File Offset: 0x0015CCA0
				public JoinProgramSetBuilder<extract> Substring(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<extract>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Substring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A6F RID: 27247 RVA: 0x0015EAFC File Offset: 0x0015CCFC
				public JoinProgramSetBuilder<extract> Slice(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<extract>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Slice, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A70 RID: 27248 RVA: 0x0015EB56 File Offset: 0x0015CD56
				public JoinProgramSetBuilder<split> SplitPosition(ProgramSetBuilder<row> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<split>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitPosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A71 RID: 27249 RVA: 0x0015EB98 File Offset: 0x0015CD98
				public JoinProgramSetBuilder<split> SplitDelimiter(ProgramSetBuilder<row> value0, ProgramSetBuilder<str> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<split>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitDelimiter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06006A72 RID: 27250 RVA: 0x0015EBF2 File Offset: 0x0015CDF2
				public JoinProgramSetBuilder<records> Select(ProgramSetBuilder<re> value0, ProgramSetBuilder<skip> value1)
				{
					return JoinProgramSetBuilder<records>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Select, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A73 RID: 27251 RVA: 0x0015EC32 File Offset: 0x0015CE32
				public JoinProgramSetBuilder<records> Group(ProgramSetBuilder<re> value0, ProgramSetBuilder<skip> value1)
				{
					return JoinProgramSetBuilder<records>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Group, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A74 RID: 27252 RVA: 0x0015EC72 File Offset: 0x0015CE72
				public JoinProgramSetBuilder<records> MergeEvery(ProgramSetBuilder<k> value0, ProgramSetBuilder<skip> value1)
				{
					return JoinProgramSetBuilder<records>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MergeEvery, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A75 RID: 27253 RVA: 0x0015ECB2 File Offset: 0x0015CEB2
				public JoinProgramSetBuilder<skip> Skip(ProgramSetBuilder<k> value0, ProgramSetBuilder<lines> value1)
				{
					return JoinProgramSetBuilder<skip>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Skip, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A76 RID: 27254 RVA: 0x0015ECF2 File Offset: 0x0015CEF2
				public JoinProgramSetBuilder<lines> SplitLines(ProgramSetBuilder<v> value0)
				{
					return JoinProgramSetBuilder<lines>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SplitLines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A77 RID: 27255 RVA: 0x0015ED23 File Offset: 0x0015CF23
				public JoinProgramSetBuilder<table> RowMap(ProgramSetBuilder<colSplit> value0, ProgramSetBuilder<records> value1)
				{
					return JoinProgramSetBuilder<table>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RowMap, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A78 RID: 27256 RVA: 0x0015ED63 File Offset: 0x0015CF63
				public JoinProgramSetBuilder<_LetB2> LetPrepend(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return JoinProgramSetBuilder<_LetB2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetPrepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A79 RID: 27257 RVA: 0x0015EDA3 File Offset: 0x0015CFA3
				public JoinProgramSetBuilder<colSplit> LetSplit(ProgramSetBuilder<split> value0, ProgramSetBuilder<_LetB2> value1)
				{
					return JoinProgramSetBuilder<colSplit>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06006A7A RID: 27258 RVA: 0x0015EDE3 File Offset: 0x0015CFE3
				public JoinProgramSetBuilder<extractTup> LetExtractTup(ProgramSetBuilder<_LetB3> value0, ProgramSetBuilder<trimExtract> value1)
				{
					return JoinProgramSetBuilder<extractTup>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetExtractTup, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04002ED0 RID: 11984
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F16 RID: 3862
			public class JoinUnnamedConversions
			{
				// Token: 0x06006A7B RID: 27259 RVA: 0x0015EE23 File Offset: 0x0015D023
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A7C RID: 27260 RVA: 0x0015EE32 File Offset: 0x0015D032
				public ProgramSetBuilder<trimExtract> trimExtract_extract(ProgramSetBuilder<extract> value0)
				{
					return ProgramSetBuilder<trimExtract>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.trimExtract_extract, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A7D RID: 27261 RVA: 0x0015EE63 File Offset: 0x0015D063
				public ProgramSetBuilder<extract> extract_row(ProgramSetBuilder<row> value0)
				{
					return ProgramSetBuilder<extract>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.extract_row, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A7E RID: 27262 RVA: 0x0015EE94 File Offset: 0x0015D094
				public ProgramSetBuilder<records> records_skip(ProgramSetBuilder<skip> value0)
				{
					return ProgramSetBuilder<records>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.records_skip, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A7F RID: 27263 RVA: 0x0015EEC5 File Offset: 0x0015D0C5
				public ProgramSetBuilder<skip> skip_lines(ProgramSetBuilder<lines> value0)
				{
					return ProgramSetBuilder<skip>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.skip_lines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002ED1 RID: 11985
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F17 RID: 3863
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06006A80 RID: 27264 RVA: 0x0015EEF6 File Offset: 0x0015D0F6
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A81 RID: 27265 RVA: 0x0015EF05 File Offset: 0x0015D105
				public JoinProgramSetBuilder<trimExtract> trimExtract_extract(ProgramSetBuilder<extract> value0)
				{
					return JoinProgramSetBuilder<trimExtract>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.trimExtract_extract, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A82 RID: 27266 RVA: 0x0015EF36 File Offset: 0x0015D136
				public JoinProgramSetBuilder<extract> extract_row(ProgramSetBuilder<row> value0)
				{
					return JoinProgramSetBuilder<extract>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.extract_row, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A83 RID: 27267 RVA: 0x0015EF67 File Offset: 0x0015D167
				public JoinProgramSetBuilder<records> records_skip(ProgramSetBuilder<skip> value0)
				{
					return JoinProgramSetBuilder<records>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.records_skip, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06006A84 RID: 27268 RVA: 0x0015EF98 File Offset: 0x0015D198
				public JoinProgramSetBuilder<skip> skip_lines(ProgramSetBuilder<lines> value0)
				{
					return JoinProgramSetBuilder<skip>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.skip_lines, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04002ED2 RID: 11986
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000F18 RID: 3864
			public class Casts
			{
				// Token: 0x06006A85 RID: 27269 RVA: 0x0015EFC9 File Offset: 0x0015D1C9
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06006A86 RID: 27270 RVA: 0x0015EFD8 File Offset: 0x0015D1D8
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06006A87 RID: 27271 RVA: 0x0015F030 File Offset: 0x0015D230
				public ProgramSetBuilder<str> str(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.str)
					{
						string text = "set";
						string text2 = "expected program set for symbol str but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.str>.CreateUnsafe(set);
				}

				// Token: 0x06006A88 RID: 27272 RVA: 0x0015F088 File Offset: 0x0015D288
				public ProgramSetBuilder<del> del(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.del)
					{
						string text = "set";
						string text2 = "expected program set for symbol del but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.del>.CreateUnsafe(set);
				}

				// Token: 0x06006A89 RID: 27273 RVA: 0x0015F0E0 File Offset: 0x0015D2E0
				public ProgramSetBuilder<re> re(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.re)
					{
						string text = "set";
						string text2 = "expected program set for symbol re but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.re>.CreateUnsafe(set);
				}

				// Token: 0x06006A8A RID: 27274 RVA: 0x0015F138 File Offset: 0x0015D338
				public ProgramSetBuilder<columnNames> columnNames(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnNames)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnNames but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.columnNames>.CreateUnsafe(set);
				}

				// Token: 0x06006A8B RID: 27275 RVA: 0x0015F190 File Offset: 0x0015D390
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x06006A8C RID: 27276 RVA: 0x0015F1E8 File Offset: 0x0015D3E8
				public ProgramSetBuilder<table> table(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.table)
					{
						string text = "set";
						string text2 = "expected program set for symbol table but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.table>.CreateUnsafe(set);
				}

				// Token: 0x06006A8D RID: 27277 RVA: 0x0015F240 File Offset: 0x0015D440
				public ProgramSetBuilder<row> row(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.row)
					{
						string text = "set";
						string text2 = "expected program set for symbol row but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.row>.CreateUnsafe(set);
				}

				// Token: 0x06006A8E RID: 27278 RVA: 0x0015F298 File Offset: 0x0015D498
				public ProgramSetBuilder<colSplit> colSplit(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.colSplit)
					{
						string text = "set";
						string text2 = "expected program set for symbol colSplit but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.colSplit>.CreateUnsafe(set);
				}

				// Token: 0x06006A8F RID: 27279 RVA: 0x0015F2F0 File Offset: 0x0015D4F0
				public ProgramSetBuilder<tup> tup(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.tup)
					{
						string text = "set";
						string text2 = "expected program set for symbol tup but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.tup>.CreateUnsafe(set);
				}

				// Token: 0x06006A90 RID: 27280 RVA: 0x0015F348 File Offset: 0x0015D548
				public ProgramSetBuilder<extractTup> extractTup(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.extractTup)
					{
						string text = "set";
						string text2 = "expected program set for symbol extractTup but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extractTup>.CreateUnsafe(set);
				}

				// Token: 0x06006A91 RID: 27281 RVA: 0x0015F3A0 File Offset: 0x0015D5A0
				public ProgramSetBuilder<trimExtract> trimExtract(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.trimExtract)
					{
						string text = "set";
						string text2 = "expected program set for symbol trimExtract but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.trimExtract>.CreateUnsafe(set);
				}

				// Token: 0x06006A92 RID: 27282 RVA: 0x0015F3F8 File Offset: 0x0015D5F8
				public ProgramSetBuilder<extract> extract(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.extract)
					{
						string text = "set";
						string text2 = "expected program set for symbol extract but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.extract>.CreateUnsafe(set);
				}

				// Token: 0x06006A93 RID: 27283 RVA: 0x0015F450 File Offset: 0x0015D650
				public ProgramSetBuilder<split> split(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.split)
					{
						string text = "set";
						string text2 = "expected program set for symbol split but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.split>.CreateUnsafe(set);
				}

				// Token: 0x06006A94 RID: 27284 RVA: 0x0015F4A8 File Offset: 0x0015D6A8
				public ProgramSetBuilder<records> records(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.records)
					{
						string text = "set";
						string text2 = "expected program set for symbol records but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.records>.CreateUnsafe(set);
				}

				// Token: 0x06006A95 RID: 27285 RVA: 0x0015F500 File Offset: 0x0015D700
				public ProgramSetBuilder<skip> skip(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.skip)
					{
						string text = "set";
						string text2 = "expected program set for symbol skip but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.skip>.CreateUnsafe(set);
				}

				// Token: 0x06006A96 RID: 27286 RVA: 0x0015F558 File Offset: 0x0015D758
				public ProgramSetBuilder<lines> lines(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.lines)
					{
						string text = "set";
						string text2 = "expected program set for symbol lines but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes.lines>.CreateUnsafe(set);
				}

				// Token: 0x06006A97 RID: 27287 RVA: 0x0015F5B0 File Offset: 0x0015D7B0
				public ProgramSetBuilder<_LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x06006A98 RID: 27288 RVA: 0x0015F608 File Offset: 0x0015D808
				public ProgramSetBuilder<_LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x06006A99 RID: 27289 RVA: 0x0015F660 File Offset: 0x0015D860
				public ProgramSetBuilder<_LetB2> _LetB2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB2)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB2>.CreateUnsafe(set);
				}

				// Token: 0x06006A9A RID: 27290 RVA: 0x0015F6B8 File Offset: 0x0015D8B8
				public ProgramSetBuilder<_LetB3> _LetB3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB3)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes._LetB3>.CreateUnsafe(set);
				}

				// Token: 0x04002ED3 RID: 11987
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
