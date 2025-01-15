using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build
{
	// Token: 0x02000FE0 RID: 4064
	public class GrammarBuilders
	{
		// Token: 0x06007051 RID: 28753 RVA: 0x0018C29A File Offset: 0x0018A49A
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06007052 RID: 28754 RVA: 0x0018C2C6 File Offset: 0x0018A4C6
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06007053 RID: 28755 RVA: 0x0018C2D3 File Offset: 0x0018A4D3
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06007054 RID: 28756 RVA: 0x0018C2E0 File Offset: 0x0018A4E0
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06007055 RID: 28757 RVA: 0x0018C2ED File Offset: 0x0018A4ED
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06007056 RID: 28758 RVA: 0x0018C2FA File Offset: 0x0018A4FA
		// (set) Token: 0x06007057 RID: 28759 RVA: 0x0018C302 File Offset: 0x0018A502
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06007058 RID: 28760 RVA: 0x0018C30B File Offset: 0x0018A50B
		// (set) Token: 0x06007059 RID: 28761 RVA: 0x0018C313 File Offset: 0x0018A513
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600705A RID: 28762 RVA: 0x0018C31C File Offset: 0x0018A51C
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

		// Token: 0x04003113 RID: 12563
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04003114 RID: 12564
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04003115 RID: 12565
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04003116 RID: 12566
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04003117 RID: 12567
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000FE1 RID: 4065
		public class GrammarSymbols
		{
			// Token: 0x170013FB RID: 5115
			// (get) Token: 0x0600705C RID: 28764 RVA: 0x0018C3C7 File Offset: 0x0018A5C7
			// (set) Token: 0x0600705D RID: 28765 RVA: 0x0018C3CF File Offset: 0x0018A5CF
			public Symbol resultSequence { get; private set; }

			// Token: 0x170013FC RID: 5116
			// (get) Token: 0x0600705E RID: 28766 RVA: 0x0018C3D8 File Offset: 0x0018A5D8
			// (set) Token: 0x0600705F RID: 28767 RVA: 0x0018C3E0 File Offset: 0x0018A5E0
			public Symbol resultRegion { get; private set; }

			// Token: 0x170013FD RID: 5117
			// (get) Token: 0x06007060 RID: 28768 RVA: 0x0018C3E9 File Offset: 0x0018A5E9
			// (set) Token: 0x06007061 RID: 28769 RVA: 0x0018C3F1 File Offset: 0x0018A5F1
			public Symbol subNodeSequence { get; private set; }

			// Token: 0x170013FE RID: 5118
			// (get) Token: 0x06007062 RID: 28770 RVA: 0x0018C3FA File Offset: 0x0018A5FA
			// (set) Token: 0x06007063 RID: 28771 RVA: 0x0018C402 File Offset: 0x0018A602
			public Symbol node { get; private set; }

			// Token: 0x170013FF RID: 5119
			// (get) Token: 0x06007064 RID: 28772 RVA: 0x0018C40B File Offset: 0x0018A60B
			// (set) Token: 0x06007065 RID: 28773 RVA: 0x0018C413 File Offset: 0x0018A613
			public Symbol subNode { get; private set; }

			// Token: 0x17001400 RID: 5120
			// (get) Token: 0x06007066 RID: 28774 RVA: 0x0018C41C File Offset: 0x0018A61C
			// (set) Token: 0x06007067 RID: 28775 RVA: 0x0018C424 File Offset: 0x0018A624
			public Symbol mapNodeInSequence { get; private set; }

			// Token: 0x17001401 RID: 5121
			// (get) Token: 0x06007068 RID: 28776 RVA: 0x0018C42D File Offset: 0x0018A62D
			// (set) Token: 0x06007069 RID: 28777 RVA: 0x0018C435 File Offset: 0x0018A635
			public Symbol regionSequence { get; private set; }

			// Token: 0x17001402 RID: 5122
			// (get) Token: 0x0600706A RID: 28778 RVA: 0x0018C43E File Offset: 0x0018A63E
			// (set) Token: 0x0600706B RID: 28779 RVA: 0x0018C446 File Offset: 0x0018A646
			public Symbol regionStart { get; private set; }

			// Token: 0x17001403 RID: 5123
			// (get) Token: 0x0600706C RID: 28780 RVA: 0x0018C44F File Offset: 0x0018A64F
			// (set) Token: 0x0600706D RID: 28781 RVA: 0x0018C457 File Offset: 0x0018A657
			public Symbol region { get; private set; }

			// Token: 0x17001404 RID: 5124
			// (get) Token: 0x0600706E RID: 28782 RVA: 0x0018C460 File Offset: 0x0018A660
			// (set) Token: 0x0600706F RID: 28783 RVA: 0x0018C468 File Offset: 0x0018A668
			public Symbol mapRegionInSequence { get; private set; }

			// Token: 0x17001405 RID: 5125
			// (get) Token: 0x06007070 RID: 28784 RVA: 0x0018C471 File Offset: 0x0018A671
			// (set) Token: 0x06007071 RID: 28785 RVA: 0x0018C479 File Offset: 0x0018A679
			public Symbol beginNode { get; private set; }

			// Token: 0x17001406 RID: 5126
			// (get) Token: 0x06007072 RID: 28786 RVA: 0x0018C482 File Offset: 0x0018A682
			// (set) Token: 0x06007073 RID: 28787 RVA: 0x0018C48A File Offset: 0x0018A68A
			public Symbol endNode { get; private set; }

			// Token: 0x17001407 RID: 5127
			// (get) Token: 0x06007074 RID: 28788 RVA: 0x0018C493 File Offset: 0x0018A693
			// (set) Token: 0x06007075 RID: 28789 RVA: 0x0018C49B File Offset: 0x0018A69B
			public Symbol selection { get; private set; }

			// Token: 0x17001408 RID: 5128
			// (get) Token: 0x06007076 RID: 28790 RVA: 0x0018C4A4 File Offset: 0x0018A6A4
			// (set) Token: 0x06007077 RID: 28791 RVA: 0x0018C4AC File Offset: 0x0018A6AC
			public Symbol filterSelection { get; private set; }

			// Token: 0x17001409 RID: 5129
			// (get) Token: 0x06007078 RID: 28792 RVA: 0x0018C4B5 File Offset: 0x0018A6B5
			// (set) Token: 0x06007079 RID: 28793 RVA: 0x0018C4BD File Offset: 0x0018A6BD
			public Symbol selectionEnd { get; private set; }

			// Token: 0x1700140A RID: 5130
			// (get) Token: 0x0600707A RID: 28794 RVA: 0x0018C4C6 File Offset: 0x0018A6C6
			// (set) Token: 0x0600707B RID: 28795 RVA: 0x0018C4CE File Offset: 0x0018A6CE
			public Symbol regionStartSiblings { get; private set; }

			// Token: 0x1700140B RID: 5131
			// (get) Token: 0x0600707C RID: 28796 RVA: 0x0018C4D7 File Offset: 0x0018A6D7
			// (set) Token: 0x0600707D RID: 28797 RVA: 0x0018C4DF File Offset: 0x0018A6DF
			public Symbol selection2 { get; private set; }

			// Token: 0x1700140C RID: 5132
			// (get) Token: 0x0600707E RID: 28798 RVA: 0x0018C4E8 File Offset: 0x0018A6E8
			// (set) Token: 0x0600707F RID: 28799 RVA: 0x0018C4F0 File Offset: 0x0018A6F0
			public Symbol selection3 { get; private set; }

			// Token: 0x1700140D RID: 5133
			// (get) Token: 0x06007080 RID: 28800 RVA: 0x0018C4F9 File Offset: 0x0018A6F9
			// (set) Token: 0x06007081 RID: 28801 RVA: 0x0018C501 File Offset: 0x0018A701
			public Symbol filterSelection2 { get; private set; }

			// Token: 0x1700140E RID: 5134
			// (get) Token: 0x06007082 RID: 28802 RVA: 0x0018C50A File Offset: 0x0018A70A
			// (set) Token: 0x06007083 RID: 28803 RVA: 0x0018C512 File Offset: 0x0018A712
			public Symbol selection4 { get; private set; }

			// Token: 0x1700140F RID: 5135
			// (get) Token: 0x06007084 RID: 28804 RVA: 0x0018C51B File Offset: 0x0018A71B
			// (set) Token: 0x06007085 RID: 28805 RVA: 0x0018C523 File Offset: 0x0018A723
			public Symbol selection5 { get; private set; }

			// Token: 0x17001410 RID: 5136
			// (get) Token: 0x06007086 RID: 28806 RVA: 0x0018C52C File Offset: 0x0018A72C
			// (set) Token: 0x06007087 RID: 28807 RVA: 0x0018C534 File Offset: 0x0018A734
			public Symbol filterSelection3 { get; private set; }

			// Token: 0x17001411 RID: 5137
			// (get) Token: 0x06007088 RID: 28808 RVA: 0x0018C53D File Offset: 0x0018A73D
			// (set) Token: 0x06007089 RID: 28809 RVA: 0x0018C545 File Offset: 0x0018A745
			public Symbol selection6 { get; private set; }

			// Token: 0x17001412 RID: 5138
			// (get) Token: 0x0600708A RID: 28810 RVA: 0x0018C54E File Offset: 0x0018A74E
			// (set) Token: 0x0600708B RID: 28811 RVA: 0x0018C556 File Offset: 0x0018A756
			public Symbol selection7 { get; private set; }

			// Token: 0x17001413 RID: 5139
			// (get) Token: 0x0600708C RID: 28812 RVA: 0x0018C55F File Offset: 0x0018A75F
			// (set) Token: 0x0600708D RID: 28813 RVA: 0x0018C567 File Offset: 0x0018A767
			public Symbol filterSelection4 { get; private set; }

			// Token: 0x17001414 RID: 5140
			// (get) Token: 0x0600708E RID: 28814 RVA: 0x0018C570 File Offset: 0x0018A770
			// (set) Token: 0x0600708F RID: 28815 RVA: 0x0018C578 File Offset: 0x0018A778
			public Symbol selection8 { get; private set; }

			// Token: 0x17001415 RID: 5141
			// (get) Token: 0x06007090 RID: 28816 RVA: 0x0018C581 File Offset: 0x0018A781
			// (set) Token: 0x06007091 RID: 28817 RVA: 0x0018C589 File Offset: 0x0018A789
			public Symbol selection9 { get; private set; }

			// Token: 0x17001416 RID: 5142
			// (get) Token: 0x06007092 RID: 28818 RVA: 0x0018C592 File Offset: 0x0018A792
			// (set) Token: 0x06007093 RID: 28819 RVA: 0x0018C59A File Offset: 0x0018A79A
			public Symbol filterSelection5 { get; private set; }

			// Token: 0x17001417 RID: 5143
			// (get) Token: 0x06007094 RID: 28820 RVA: 0x0018C5A3 File Offset: 0x0018A7A3
			// (set) Token: 0x06007095 RID: 28821 RVA: 0x0018C5AB File Offset: 0x0018A7AB
			public Symbol selection10 { get; private set; }

			// Token: 0x17001418 RID: 5144
			// (get) Token: 0x06007096 RID: 28822 RVA: 0x0018C5B4 File Offset: 0x0018A7B4
			// (set) Token: 0x06007097 RID: 28823 RVA: 0x0018C5BC File Offset: 0x0018A7BC
			public Symbol leafFExpr { get; private set; }

			// Token: 0x17001419 RID: 5145
			// (get) Token: 0x06007098 RID: 28824 RVA: 0x0018C5C5 File Offset: 0x0018A7C5
			// (set) Token: 0x06007099 RID: 28825 RVA: 0x0018C5CD File Offset: 0x0018A7CD
			public Symbol leafAtom { get; private set; }

			// Token: 0x1700141A RID: 5146
			// (get) Token: 0x0600709A RID: 28826 RVA: 0x0018C5D6 File Offset: 0x0018A7D6
			// (set) Token: 0x0600709B RID: 28827 RVA: 0x0018C5DE File Offset: 0x0018A7DE
			public Symbol atomExpr { get; private set; }

			// Token: 0x1700141B RID: 5147
			// (get) Token: 0x0600709C RID: 28828 RVA: 0x0018C5E7 File Offset: 0x0018A7E7
			// (set) Token: 0x0600709D RID: 28829 RVA: 0x0018C5EF File Offset: 0x0018A7EF
			public Symbol literalExpr { get; private set; }

			// Token: 0x1700141C RID: 5148
			// (get) Token: 0x0600709E RID: 28830 RVA: 0x0018C5F8 File Offset: 0x0018A7F8
			// (set) Token: 0x0600709F RID: 28831 RVA: 0x0018C600 File Offset: 0x0018A800
			public Symbol fexpr { get; private set; }

			// Token: 0x1700141D RID: 5149
			// (get) Token: 0x060070A0 RID: 28832 RVA: 0x0018C609 File Offset: 0x0018A809
			// (set) Token: 0x060070A1 RID: 28833 RVA: 0x0018C611 File Offset: 0x0018A811
			public Symbol resultFields { get; private set; }

			// Token: 0x1700141E RID: 5150
			// (get) Token: 0x060070A2 RID: 28834 RVA: 0x0018C61A File Offset: 0x0018A81A
			// (set) Token: 0x060070A3 RID: 28835 RVA: 0x0018C622 File Offset: 0x0018A822
			public Symbol singletonField { get; private set; }

			// Token: 0x1700141F RID: 5151
			// (get) Token: 0x060070A4 RID: 28836 RVA: 0x0018C62B File Offset: 0x0018A82B
			// (set) Token: 0x060070A5 RID: 28837 RVA: 0x0018C633 File Offset: 0x0018A833
			public Symbol fieldSubstring { get; private set; }

			// Token: 0x17001420 RID: 5152
			// (get) Token: 0x060070A6 RID: 28838 RVA: 0x0018C63C File Offset: 0x0018A83C
			// (set) Token: 0x060070A7 RID: 28839 RVA: 0x0018C644 File Offset: 0x0018A844
			public Symbol cs { get; private set; }

			// Token: 0x17001421 RID: 5153
			// (get) Token: 0x060070A8 RID: 28840 RVA: 0x0018C64D File Offset: 0x0018A84D
			// (set) Token: 0x060070A9 RID: 28841 RVA: 0x0018C655 File Offset: 0x0018A855
			public Symbol y { get; private set; }

			// Token: 0x17001422 RID: 5154
			// (get) Token: 0x060070AA RID: 28842 RVA: 0x0018C65E File Offset: 0x0018A85E
			// (set) Token: 0x060070AB RID: 28843 RVA: 0x0018C666 File Offset: 0x0018A866
			public Symbol selectSubstring { get; private set; }

			// Token: 0x17001423 RID: 5155
			// (get) Token: 0x060070AC RID: 28844 RVA: 0x0018C66F File Offset: 0x0018A86F
			// (set) Token: 0x060070AD RID: 28845 RVA: 0x0018C677 File Offset: 0x0018A877
			public Symbol substringDisj { get; private set; }

			// Token: 0x17001424 RID: 5156
			// (get) Token: 0x060070AE RID: 28846 RVA: 0x0018C680 File Offset: 0x0018A880
			// (set) Token: 0x060070AF RID: 28847 RVA: 0x0018C688 File Offset: 0x0018A888
			public Symbol substring { get; private set; }

			// Token: 0x17001425 RID: 5157
			// (get) Token: 0x060070B0 RID: 28848 RVA: 0x0018C691 File Offset: 0x0018A891
			// (set) Token: 0x060070B1 RID: 28849 RVA: 0x0018C699 File Offset: 0x0018A899
			public Symbol resultTable { get; private set; }

			// Token: 0x17001426 RID: 5158
			// (get) Token: 0x060070B2 RID: 28850 RVA: 0x0018C6A2 File Offset: 0x0018A8A2
			// (set) Token: 0x060070B3 RID: 28851 RVA: 0x0018C6AA File Offset: 0x0018A8AA
			public Symbol columnSelectors { get; private set; }

			// Token: 0x17001427 RID: 5159
			// (get) Token: 0x060070B4 RID: 28852 RVA: 0x0018C6B3 File Offset: 0x0018A8B3
			// (set) Token: 0x060070B5 RID: 28853 RVA: 0x0018C6BB File Offset: 0x0018A8BB
			public Symbol name { get; private set; }

			// Token: 0x17001428 RID: 5160
			// (get) Token: 0x060070B6 RID: 28854 RVA: 0x0018C6C4 File Offset: 0x0018A8C4
			// (set) Token: 0x060070B7 RID: 28855 RVA: 0x0018C6CC File Offset: 0x0018A8CC
			public Symbol value { get; private set; }

			// Token: 0x17001429 RID: 5161
			// (get) Token: 0x060070B8 RID: 28856 RVA: 0x0018C6D5 File Offset: 0x0018A8D5
			// (set) Token: 0x060070B9 RID: 28857 RVA: 0x0018C6DD File Offset: 0x0018A8DD
			public Symbol cssSelector { get; private set; }

			// Token: 0x1700142A RID: 5162
			// (get) Token: 0x060070BA RID: 28858 RVA: 0x0018C6E6 File Offset: 0x0018A8E6
			// (set) Token: 0x060070BB RID: 28859 RVA: 0x0018C6EE File Offset: 0x0018A8EE
			public Symbol className { get; private set; }

			// Token: 0x1700142B RID: 5163
			// (get) Token: 0x060070BC RID: 28860 RVA: 0x0018C6F7 File Offset: 0x0018A8F7
			// (set) Token: 0x060070BD RID: 28861 RVA: 0x0018C6FF File Offset: 0x0018A8FF
			public Symbol idName { get; private set; }

			// Token: 0x1700142C RID: 5164
			// (get) Token: 0x060070BE RID: 28862 RVA: 0x0018C708 File Offset: 0x0018A908
			// (set) Token: 0x060070BF RID: 28863 RVA: 0x0018C710 File Offset: 0x0018A910
			public Symbol nodeName { get; private set; }

			// Token: 0x1700142D RID: 5165
			// (get) Token: 0x060070C0 RID: 28864 RVA: 0x0018C719 File Offset: 0x0018A919
			// (set) Token: 0x060070C1 RID: 28865 RVA: 0x0018C721 File Offset: 0x0018A921
			public Symbol propName { get; private set; }

			// Token: 0x1700142E RID: 5166
			// (get) Token: 0x060070C2 RID: 28866 RVA: 0x0018C72A File Offset: 0x0018A92A
			// (set) Token: 0x060070C3 RID: 28867 RVA: 0x0018C732 File Offset: 0x0018A932
			public Symbol idx1 { get; private set; }

			// Token: 0x1700142F RID: 5167
			// (get) Token: 0x060070C4 RID: 28868 RVA: 0x0018C73B File Offset: 0x0018A93B
			// (set) Token: 0x060070C5 RID: 28869 RVA: 0x0018C743 File Offset: 0x0018A943
			public Symbol idx2 { get; private set; }

			// Token: 0x17001430 RID: 5168
			// (get) Token: 0x060070C6 RID: 28870 RVA: 0x0018C74C File Offset: 0x0018A94C
			// (set) Token: 0x060070C7 RID: 28871 RVA: 0x0018C754 File Offset: 0x0018A954
			public Symbol names { get; private set; }

			// Token: 0x17001431 RID: 5169
			// (get) Token: 0x060070C8 RID: 28872 RVA: 0x0018C75D File Offset: 0x0018A95D
			// (set) Token: 0x060070C9 RID: 28873 RVA: 0x0018C765 File Offset: 0x0018A965
			public Symbol count { get; private set; }

			// Token: 0x17001432 RID: 5170
			// (get) Token: 0x060070CA RID: 28874 RVA: 0x0018C76E File Offset: 0x0018A96E
			// (set) Token: 0x060070CB RID: 28875 RVA: 0x0018C776 File Offset: 0x0018A976
			public Symbol substringFeatureNames { get; private set; }

			// Token: 0x17001433 RID: 5171
			// (get) Token: 0x060070CC RID: 28876 RVA: 0x0018C77F File Offset: 0x0018A97F
			// (set) Token: 0x060070CD RID: 28877 RVA: 0x0018C787 File Offset: 0x0018A987
			public Symbol substringFeatureValues { get; private set; }

			// Token: 0x17001434 RID: 5172
			// (get) Token: 0x060070CE RID: 28878 RVA: 0x0018C790 File Offset: 0x0018A990
			// (set) Token: 0x060070CF RID: 28879 RVA: 0x0018C798 File Offset: 0x0018A998
			public Symbol k { get; private set; }

			// Token: 0x17001435 RID: 5173
			// (get) Token: 0x060070D0 RID: 28880 RVA: 0x0018C7A1 File Offset: 0x0018A9A1
			// (set) Token: 0x060070D1 RID: 28881 RVA: 0x0018C7A9 File Offset: 0x0018A9A9
			public Symbol entityObjs { get; private set; }

			// Token: 0x17001436 RID: 5174
			// (get) Token: 0x060070D2 RID: 28882 RVA: 0x0018C7B2 File Offset: 0x0018A9B2
			// (set) Token: 0x060070D3 RID: 28883 RVA: 0x0018C7BA File Offset: 0x0018A9BA
			public Symbol direction { get; private set; }

			// Token: 0x17001437 RID: 5175
			// (get) Token: 0x060070D4 RID: 28884 RVA: 0x0018C7C3 File Offset: 0x0018A9C3
			// (set) Token: 0x060070D5 RID: 28885 RVA: 0x0018C7CB File Offset: 0x0018A9CB
			public Symbol allNodes { get; private set; }

			// Token: 0x17001438 RID: 5176
			// (get) Token: 0x060070D6 RID: 28886 RVA: 0x0018C7D4 File Offset: 0x0018A9D4
			// (set) Token: 0x060070D7 RID: 28887 RVA: 0x0018C7DC File Offset: 0x0018A9DC
			public Symbol nodeCollection { get; private set; }

			// Token: 0x17001439 RID: 5177
			// (get) Token: 0x060070D8 RID: 28888 RVA: 0x0018C7E5 File Offset: 0x0018A9E5
			// (set) Token: 0x060070D9 RID: 28889 RVA: 0x0018C7ED File Offset: 0x0018A9ED
			public Symbol gen_NthChild { get; private set; }

			// Token: 0x1700143A RID: 5178
			// (get) Token: 0x060070DA RID: 28890 RVA: 0x0018C7F6 File Offset: 0x0018A9F6
			// (set) Token: 0x060070DB RID: 28891 RVA: 0x0018C7FE File Offset: 0x0018A9FE
			public Symbol gen_NthLastChild { get; private set; }

			// Token: 0x1700143B RID: 5179
			// (get) Token: 0x060070DC RID: 28892 RVA: 0x0018C807 File Offset: 0x0018AA07
			// (set) Token: 0x060070DD RID: 28893 RVA: 0x0018C80F File Offset: 0x0018AA0F
			public Symbol gen_Class { get; private set; }

			// Token: 0x1700143C RID: 5180
			// (get) Token: 0x060070DE RID: 28894 RVA: 0x0018C818 File Offset: 0x0018AA18
			// (set) Token: 0x060070DF RID: 28895 RVA: 0x0018C820 File Offset: 0x0018AA20
			public Symbol gen_ID { get; private set; }

			// Token: 0x1700143D RID: 5181
			// (get) Token: 0x060070E0 RID: 28896 RVA: 0x0018C829 File Offset: 0x0018AA29
			// (set) Token: 0x060070E1 RID: 28897 RVA: 0x0018C831 File Offset: 0x0018AA31
			public Symbol gen_NodeName { get; private set; }

			// Token: 0x1700143E RID: 5182
			// (get) Token: 0x060070E2 RID: 28898 RVA: 0x0018C83A File Offset: 0x0018AA3A
			// (set) Token: 0x060070E3 RID: 28899 RVA: 0x0018C842 File Offset: 0x0018AA42
			public Symbol gen_ItemProp { get; private set; }

			// Token: 0x1700143F RID: 5183
			// (get) Token: 0x060070E4 RID: 28900 RVA: 0x0018C84B File Offset: 0x0018AA4B
			// (set) Token: 0x060070E5 RID: 28901 RVA: 0x0018C853 File Offset: 0x0018AA53
			public Symbol obj { get; private set; }

			// Token: 0x17001440 RID: 5184
			// (get) Token: 0x060070E6 RID: 28902 RVA: 0x0018C85C File Offset: 0x0018AA5C
			// (set) Token: 0x060070E7 RID: 28903 RVA: 0x0018C864 File Offset: 0x0018AA64
			public Symbol _LFun0 { get; private set; }

			// Token: 0x17001441 RID: 5185
			// (get) Token: 0x060070E8 RID: 28904 RVA: 0x0018C86D File Offset: 0x0018AA6D
			// (set) Token: 0x060070E9 RID: 28905 RVA: 0x0018C875 File Offset: 0x0018AA75
			public Symbol _LFun1 { get; private set; }

			// Token: 0x17001442 RID: 5186
			// (get) Token: 0x060070EA RID: 28906 RVA: 0x0018C87E File Offset: 0x0018AA7E
			// (set) Token: 0x060070EB RID: 28907 RVA: 0x0018C886 File Offset: 0x0018AA86
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17001443 RID: 5187
			// (get) Token: 0x060070EC RID: 28908 RVA: 0x0018C88F File Offset: 0x0018AA8F
			// (set) Token: 0x060070ED RID: 28909 RVA: 0x0018C897 File Offset: 0x0018AA97
			public Symbol _LFun2 { get; private set; }

			// Token: 0x17001444 RID: 5188
			// (get) Token: 0x060070EE RID: 28910 RVA: 0x0018C8A0 File Offset: 0x0018AAA0
			// (set) Token: 0x060070EF RID: 28911 RVA: 0x0018C8A8 File Offset: 0x0018AAA8
			public Symbol _LFun3 { get; private set; }

			// Token: 0x17001445 RID: 5189
			// (get) Token: 0x060070F0 RID: 28912 RVA: 0x0018C8B1 File Offset: 0x0018AAB1
			// (set) Token: 0x060070F1 RID: 28913 RVA: 0x0018C8B9 File Offset: 0x0018AAB9
			public Symbol _LFun4 { get; private set; }

			// Token: 0x17001446 RID: 5190
			// (get) Token: 0x060070F2 RID: 28914 RVA: 0x0018C8C2 File Offset: 0x0018AAC2
			// (set) Token: 0x060070F3 RID: 28915 RVA: 0x0018C8CA File Offset: 0x0018AACA
			public Symbol _LFun5 { get; private set; }

			// Token: 0x17001447 RID: 5191
			// (get) Token: 0x060070F4 RID: 28916 RVA: 0x0018C8D3 File Offset: 0x0018AAD3
			// (set) Token: 0x060070F5 RID: 28917 RVA: 0x0018C8DB File Offset: 0x0018AADB
			public Symbol _LFun6 { get; private set; }

			// Token: 0x17001448 RID: 5192
			// (get) Token: 0x060070F6 RID: 28918 RVA: 0x0018C8E4 File Offset: 0x0018AAE4
			// (set) Token: 0x060070F7 RID: 28919 RVA: 0x0018C8EC File Offset: 0x0018AAEC
			public Symbol _LFun7 { get; private set; }

			// Token: 0x17001449 RID: 5193
			// (get) Token: 0x060070F8 RID: 28920 RVA: 0x0018C8F5 File Offset: 0x0018AAF5
			// (set) Token: 0x060070F9 RID: 28921 RVA: 0x0018C8FD File Offset: 0x0018AAFD
			public Symbol _LFun8 { get; private set; }

			// Token: 0x060070FA RID: 28922 RVA: 0x0018C908 File Offset: 0x0018AB08
			public GrammarSymbols(Grammar grammar)
			{
				this.resultSequence = grammar.Symbol("resultSequence");
				this.resultRegion = grammar.Symbol("resultRegion");
				this.subNodeSequence = grammar.Symbol("subNodeSequence");
				this.node = grammar.Symbol("node");
				this.subNode = grammar.Symbol("subNode");
				this.mapNodeInSequence = grammar.Symbol("mapNodeInSequence");
				this.regionSequence = grammar.Symbol("regionSequence");
				this.regionStart = grammar.Symbol("regionStart");
				this.region = grammar.Symbol("region");
				this.mapRegionInSequence = grammar.Symbol("mapRegionInSequence");
				this.beginNode = grammar.Symbol("beginNode");
				this.endNode = grammar.Symbol("endNode");
				this.selection = grammar.Symbol("selection");
				this.filterSelection = grammar.Symbol("filterSelection");
				this.selectionEnd = grammar.Symbol("selectionEnd");
				this.regionStartSiblings = grammar.Symbol("regionStartSiblings");
				this.selection2 = grammar.Symbol("selection2");
				this.selection3 = grammar.Symbol("selection3");
				this.filterSelection2 = grammar.Symbol("filterSelection2");
				this.selection4 = grammar.Symbol("selection4");
				this.selection5 = grammar.Symbol("selection5");
				this.filterSelection3 = grammar.Symbol("filterSelection3");
				this.selection6 = grammar.Symbol("selection6");
				this.selection7 = grammar.Symbol("selection7");
				this.filterSelection4 = grammar.Symbol("filterSelection4");
				this.selection8 = grammar.Symbol("selection8");
				this.selection9 = grammar.Symbol("selection9");
				this.filterSelection5 = grammar.Symbol("filterSelection5");
				this.selection10 = grammar.Symbol("selection10");
				this.leafFExpr = grammar.Symbol("leafFExpr");
				this.leafAtom = grammar.Symbol("leafAtom");
				this.atomExpr = grammar.Symbol("atomExpr");
				this.literalExpr = grammar.Symbol("literalExpr");
				this.fexpr = grammar.Symbol("fexpr");
				this.resultFields = grammar.Symbol("resultFields");
				this.singletonField = grammar.Symbol("singletonField");
				this.fieldSubstring = grammar.Symbol("fieldSubstring");
				this.cs = grammar.Symbol("cs");
				this.y = grammar.Symbol("y");
				this.selectSubstring = grammar.Symbol("selectSubstring");
				this.substringDisj = grammar.Symbol("substringDisj");
				this.substring = grammar.Symbol("substring");
				this.resultTable = grammar.Symbol("resultTable");
				this.columnSelectors = grammar.Symbol("columnSelectors");
				this.name = grammar.Symbol("name");
				this.value = grammar.Symbol("value");
				this.cssSelector = grammar.Symbol("cssSelector");
				this.className = grammar.Symbol("className");
				this.idName = grammar.Symbol("idName");
				this.nodeName = grammar.Symbol("nodeName");
				this.propName = grammar.Symbol("propName");
				this.idx1 = grammar.Symbol("idx1");
				this.idx2 = grammar.Symbol("idx2");
				this.names = grammar.Symbol("names");
				this.count = grammar.Symbol("count");
				this.substringFeatureNames = grammar.Symbol("substringFeatureNames");
				this.substringFeatureValues = grammar.Symbol("substringFeatureValues");
				this.k = grammar.Symbol("k");
				this.entityObjs = grammar.Symbol("entityObjs");
				this.direction = grammar.Symbol("direction");
				this.allNodes = grammar.Symbol("allNodes");
				this.nodeCollection = grammar.Symbol("nodeCollection");
				this.gen_NthChild = grammar.Symbol("gen_NthChild");
				this.gen_NthLastChild = grammar.Symbol("gen_NthLastChild");
				this.gen_Class = grammar.Symbol("gen_Class");
				this.gen_ID = grammar.Symbol("gen_ID");
				this.gen_NodeName = grammar.Symbol("gen_NodeName");
				this.gen_ItemProp = grammar.Symbol("gen_ItemProp");
				this.obj = grammar.Symbol("obj");
				this._LFun0 = grammar.Symbol("_LFun0");
				this._LFun1 = grammar.Symbol("_LFun1");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LFun2 = grammar.Symbol("_LFun2");
				this._LFun3 = grammar.Symbol("_LFun3");
				this._LFun4 = grammar.Symbol("_LFun4");
				this._LFun5 = grammar.Symbol("_LFun5");
				this._LFun6 = grammar.Symbol("_LFun6");
				this._LFun7 = grammar.Symbol("_LFun7");
				this._LFun8 = grammar.Symbol("_LFun8");
			}
		}

		// Token: 0x02000FE2 RID: 4066
		public class GrammarRules
		{
			// Token: 0x1700144A RID: 5194
			// (get) Token: 0x060070FB RID: 28923 RVA: 0x0018CE5A File Offset: 0x0018B05A
			// (set) Token: 0x060070FC RID: 28924 RVA: 0x0018CE62 File Offset: 0x0018B062
			public BlackBoxRule ConvertToWebRegions { get; private set; }

			// Token: 0x1700144B RID: 5195
			// (get) Token: 0x060070FD RID: 28925 RVA: 0x0018CE6B File Offset: 0x0018B06B
			// (set) Token: 0x060070FE RID: 28926 RVA: 0x0018CE73 File Offset: 0x0018B073
			public BlackBoxRule Union { get; private set; }

			// Token: 0x1700144C RID: 5196
			// (get) Token: 0x060070FF RID: 28927 RVA: 0x0018CE7C File Offset: 0x0018B07C
			// (set) Token: 0x06007100 RID: 28928 RVA: 0x0018CE84 File Offset: 0x0018B084
			public BlackBoxRule EmptySequence { get; private set; }

			// Token: 0x1700144D RID: 5197
			// (get) Token: 0x06007101 RID: 28929 RVA: 0x0018CE8D File Offset: 0x0018B08D
			// (set) Token: 0x06007102 RID: 28930 RVA: 0x0018CE95 File Offset: 0x0018B095
			public BlackBoxRule NodeToWebRegion { get; private set; }

			// Token: 0x1700144E RID: 5198
			// (get) Token: 0x06007103 RID: 28931 RVA: 0x0018CE9E File Offset: 0x0018B09E
			// (set) Token: 0x06007104 RID: 28932 RVA: 0x0018CEA6 File Offset: 0x0018B0A6
			public BlackBoxRule NodeToWebRegionInSequence { get; private set; }

			// Token: 0x1700144F RID: 5199
			// (get) Token: 0x06007105 RID: 28933 RVA: 0x0018CEAF File Offset: 0x0018B0AF
			// (set) Token: 0x06007106 RID: 28934 RVA: 0x0018CEB7 File Offset: 0x0018B0B7
			public BlackBoxRule NodeRegionToWebRegion { get; private set; }

			// Token: 0x17001450 RID: 5200
			// (get) Token: 0x06007107 RID: 28935 RVA: 0x0018CEC0 File Offset: 0x0018B0C0
			// (set) Token: 0x06007108 RID: 28936 RVA: 0x0018CEC8 File Offset: 0x0018B0C8
			public BlackBoxRule NodeRegionToWebRegionInSequence { get; private set; }

			// Token: 0x17001451 RID: 5201
			// (get) Token: 0x06007109 RID: 28937 RVA: 0x0018CED1 File Offset: 0x0018B0D1
			// (set) Token: 0x0600710A RID: 28938 RVA: 0x0018CED9 File Offset: 0x0018B0D9
			public BlackBoxRule SingleSelection1 { get; private set; }

			// Token: 0x17001452 RID: 5202
			// (get) Token: 0x0600710B RID: 28939 RVA: 0x0018CEE2 File Offset: 0x0018B0E2
			// (set) Token: 0x0600710C RID: 28940 RVA: 0x0018CEEA File Offset: 0x0018B0EA
			public BlackBoxRule DisjSelection1 { get; private set; }

			// Token: 0x17001453 RID: 5203
			// (get) Token: 0x0600710D RID: 28941 RVA: 0x0018CEF3 File Offset: 0x0018B0F3
			// (set) Token: 0x0600710E RID: 28942 RVA: 0x0018CEFB File Offset: 0x0018B0FB
			public BlackBoxRule CSSSelection { get; private set; }

			// Token: 0x17001454 RID: 5204
			// (get) Token: 0x0600710F RID: 28943 RVA: 0x0018CF04 File Offset: 0x0018B104
			// (set) Token: 0x06007110 RID: 28944 RVA: 0x0018CF0C File Offset: 0x0018B10C
			public BlackBoxRule YoungerSiblingsOf { get; private set; }

			// Token: 0x17001455 RID: 5205
			// (get) Token: 0x06007111 RID: 28945 RVA: 0x0018CF15 File Offset: 0x0018B115
			// (set) Token: 0x06007112 RID: 28946 RVA: 0x0018CF1D File Offset: 0x0018B11D
			public BlackBoxRule LeafChildrenOf1 { get; private set; }

			// Token: 0x17001456 RID: 5206
			// (get) Token: 0x06007113 RID: 28947 RVA: 0x0018CF26 File Offset: 0x0018B126
			// (set) Token: 0x06007114 RID: 28948 RVA: 0x0018CF2E File Offset: 0x0018B12E
			public BlackBoxRule SingleSelection2 { get; private set; }

			// Token: 0x17001457 RID: 5207
			// (get) Token: 0x06007115 RID: 28949 RVA: 0x0018CF37 File Offset: 0x0018B137
			// (set) Token: 0x06007116 RID: 28950 RVA: 0x0018CF3F File Offset: 0x0018B13F
			public BlackBoxRule DisjSelection2 { get; private set; }

			// Token: 0x17001458 RID: 5208
			// (get) Token: 0x06007117 RID: 28951 RVA: 0x0018CF48 File Offset: 0x0018B148
			// (set) Token: 0x06007118 RID: 28952 RVA: 0x0018CF50 File Offset: 0x0018B150
			public BlackBoxRule LeafChildrenOf2 { get; private set; }

			// Token: 0x17001459 RID: 5209
			// (get) Token: 0x06007119 RID: 28953 RVA: 0x0018CF59 File Offset: 0x0018B159
			// (set) Token: 0x0600711A RID: 28954 RVA: 0x0018CF61 File Offset: 0x0018B161
			public BlackBoxRule SingleSelection3 { get; private set; }

			// Token: 0x1700145A RID: 5210
			// (get) Token: 0x0600711B RID: 28955 RVA: 0x0018CF6A File Offset: 0x0018B16A
			// (set) Token: 0x0600711C RID: 28956 RVA: 0x0018CF72 File Offset: 0x0018B172
			public BlackBoxRule DisjSelection3 { get; private set; }

			// Token: 0x1700145B RID: 5211
			// (get) Token: 0x0600711D RID: 28957 RVA: 0x0018CF7B File Offset: 0x0018B17B
			// (set) Token: 0x0600711E RID: 28958 RVA: 0x0018CF83 File Offset: 0x0018B183
			public BlackBoxRule LeafChildrenOf3 { get; private set; }

			// Token: 0x1700145C RID: 5212
			// (get) Token: 0x0600711F RID: 28959 RVA: 0x0018CF8C File Offset: 0x0018B18C
			// (set) Token: 0x06007120 RID: 28960 RVA: 0x0018CF94 File Offset: 0x0018B194
			public BlackBoxRule SingleSelection4 { get; private set; }

			// Token: 0x1700145D RID: 5213
			// (get) Token: 0x06007121 RID: 28961 RVA: 0x0018CF9D File Offset: 0x0018B19D
			// (set) Token: 0x06007122 RID: 28962 RVA: 0x0018CFA5 File Offset: 0x0018B1A5
			public BlackBoxRule DisjSelection4 { get; private set; }

			// Token: 0x1700145E RID: 5214
			// (get) Token: 0x06007123 RID: 28963 RVA: 0x0018CFAE File Offset: 0x0018B1AE
			// (set) Token: 0x06007124 RID: 28964 RVA: 0x0018CFB6 File Offset: 0x0018B1B6
			public BlackBoxRule LeafChildrenOf4 { get; private set; }

			// Token: 0x1700145F RID: 5215
			// (get) Token: 0x06007125 RID: 28965 RVA: 0x0018CFBF File Offset: 0x0018B1BF
			// (set) Token: 0x06007126 RID: 28966 RVA: 0x0018CFC7 File Offset: 0x0018B1C7
			public BlackBoxRule SingleSelection5 { get; private set; }

			// Token: 0x17001460 RID: 5216
			// (get) Token: 0x06007127 RID: 28967 RVA: 0x0018CFD0 File Offset: 0x0018B1D0
			// (set) Token: 0x06007128 RID: 28968 RVA: 0x0018CFD8 File Offset: 0x0018B1D8
			public BlackBoxRule DisjSelection5 { get; private set; }

			// Token: 0x17001461 RID: 5217
			// (get) Token: 0x06007129 RID: 28969 RVA: 0x0018CFE1 File Offset: 0x0018B1E1
			// (set) Token: 0x0600712A RID: 28970 RVA: 0x0018CFE9 File Offset: 0x0018B1E9
			public BlackBoxRule ContainsDate { get; private set; }

			// Token: 0x17001462 RID: 5218
			// (get) Token: 0x0600712B RID: 28971 RVA: 0x0018CFF2 File Offset: 0x0018B1F2
			// (set) Token: 0x0600712C RID: 28972 RVA: 0x0018CFFA File Offset: 0x0018B1FA
			public BlackBoxRule ContainsNum { get; private set; }

			// Token: 0x17001463 RID: 5219
			// (get) Token: 0x0600712D RID: 28973 RVA: 0x0018D003 File Offset: 0x0018B203
			// (set) Token: 0x0600712E RID: 28974 RVA: 0x0018D00B File Offset: 0x0018B20B
			public BlackBoxRule ID_substring { get; private set; }

			// Token: 0x17001464 RID: 5220
			// (get) Token: 0x0600712F RID: 28975 RVA: 0x0018D014 File Offset: 0x0018B214
			// (set) Token: 0x06007130 RID: 28976 RVA: 0x0018D01C File Offset: 0x0018B21C
			public BlackBoxRule Class { get; private set; }

			// Token: 0x17001465 RID: 5221
			// (get) Token: 0x06007131 RID: 28977 RVA: 0x0018D025 File Offset: 0x0018B225
			// (set) Token: 0x06007132 RID: 28978 RVA: 0x0018D02D File Offset: 0x0018B22D
			public BlackBoxRule TitleIs { get; private set; }

			// Token: 0x17001466 RID: 5222
			// (get) Token: 0x06007133 RID: 28979 RVA: 0x0018D036 File Offset: 0x0018B236
			// (set) Token: 0x06007134 RID: 28980 RVA: 0x0018D03E File Offset: 0x0018B23E
			public BlackBoxRule NodeName { get; private set; }

			// Token: 0x17001467 RID: 5223
			// (get) Token: 0x06007135 RID: 28981 RVA: 0x0018D047 File Offset: 0x0018B247
			// (set) Token: 0x06007136 RID: 28982 RVA: 0x0018D04F File Offset: 0x0018B24F
			public BlackBoxRule NodeNames { get; private set; }

			// Token: 0x17001468 RID: 5224
			// (get) Token: 0x06007137 RID: 28983 RVA: 0x0018D058 File Offset: 0x0018B258
			// (set) Token: 0x06007138 RID: 28984 RVA: 0x0018D060 File Offset: 0x0018B260
			public BlackBoxRule NthChild { get; private set; }

			// Token: 0x17001469 RID: 5225
			// (get) Token: 0x06007139 RID: 28985 RVA: 0x0018D069 File Offset: 0x0018B269
			// (set) Token: 0x0600713A RID: 28986 RVA: 0x0018D071 File Offset: 0x0018B271
			public BlackBoxRule NthLastChild { get; private set; }

			// Token: 0x1700146A RID: 5226
			// (get) Token: 0x0600713B RID: 28987 RVA: 0x0018D07A File Offset: 0x0018B27A
			// (set) Token: 0x0600713C RID: 28988 RVA: 0x0018D082 File Offset: 0x0018B282
			public BlackBoxRule ContainsLeafNodes { get; private set; }

			// Token: 0x1700146B RID: 5227
			// (get) Token: 0x0600713D RID: 28989 RVA: 0x0018D08B File Offset: 0x0018B28B
			// (set) Token: 0x0600713E RID: 28990 RVA: 0x0018D093 File Offset: 0x0018B293
			public BlackBoxRule ChildrenCount { get; private set; }

			// Token: 0x1700146C RID: 5228
			// (get) Token: 0x0600713F RID: 28991 RVA: 0x0018D09C File Offset: 0x0018B29C
			// (set) Token: 0x06007140 RID: 28992 RVA: 0x0018D0A4 File Offset: 0x0018B2A4
			public BlackBoxRule HasAttribute { get; private set; }

			// Token: 0x1700146D RID: 5229
			// (get) Token: 0x06007141 RID: 28993 RVA: 0x0018D0AD File Offset: 0x0018B2AD
			// (set) Token: 0x06007142 RID: 28994 RVA: 0x0018D0B5 File Offset: 0x0018B2B5
			public BlackBoxRule HasStyle { get; private set; }

			// Token: 0x1700146E RID: 5230
			// (get) Token: 0x06007143 RID: 28995 RVA: 0x0018D0BE File Offset: 0x0018B2BE
			// (set) Token: 0x06007144 RID: 28996 RVA: 0x0018D0C6 File Offset: 0x0018B2C6
			public BlackBoxRule HasEntityAnchor { get; private set; }

			// Token: 0x1700146F RID: 5231
			// (get) Token: 0x06007145 RID: 28997 RVA: 0x0018D0CF File Offset: 0x0018B2CF
			// (set) Token: 0x06007146 RID: 28998 RVA: 0x0018D0D7 File Offset: 0x0018B2D7
			public BlackBoxRule AppendField { get; private set; }

			// Token: 0x17001470 RID: 5232
			// (get) Token: 0x06007147 RID: 28999 RVA: 0x0018D0E0 File Offset: 0x0018B2E0
			// (set) Token: 0x06007148 RID: 29000 RVA: 0x0018D0E8 File Offset: 0x0018B2E8
			public BlackBoxRule TrimmedTextField { get; private set; }

			// Token: 0x17001471 RID: 5233
			// (get) Token: 0x06007149 RID: 29001 RVA: 0x0018D0F1 File Offset: 0x0018B2F1
			// (set) Token: 0x0600714A RID: 29002 RVA: 0x0018D0F9 File Offset: 0x0018B2F9
			public BlackBoxRule SubstringField { get; private set; }

			// Token: 0x17001472 RID: 5234
			// (get) Token: 0x0600714B RID: 29003 RVA: 0x0018D102 File Offset: 0x0018B302
			// (set) Token: 0x0600714C RID: 29004 RVA: 0x0018D10A File Offset: 0x0018B30A
			public BlackBoxRule GetValueSubstring { get; private set; }

			// Token: 0x17001473 RID: 5235
			// (get) Token: 0x0600714D RID: 29005 RVA: 0x0018D113 File Offset: 0x0018B313
			// (set) Token: 0x0600714E RID: 29006 RVA: 0x0018D11B File Offset: 0x0018B31B
			public BlackBoxRule SelectSubstring { get; private set; }

			// Token: 0x17001474 RID: 5236
			// (get) Token: 0x0600714F RID: 29007 RVA: 0x0018D124 File Offset: 0x0018B324
			// (set) Token: 0x06007150 RID: 29008 RVA: 0x0018D12C File Offset: 0x0018B32C
			public BlackBoxRule SingleSubstring { get; private set; }

			// Token: 0x17001475 RID: 5237
			// (get) Token: 0x06007151 RID: 29009 RVA: 0x0018D135 File Offset: 0x0018B335
			// (set) Token: 0x06007152 RID: 29010 RVA: 0x0018D13D File Offset: 0x0018B33D
			public BlackBoxRule DisjSubstring { get; private set; }

			// Token: 0x17001476 RID: 5238
			// (get) Token: 0x06007153 RID: 29011 RVA: 0x0018D146 File Offset: 0x0018B346
			// (set) Token: 0x06007154 RID: 29012 RVA: 0x0018D14E File Offset: 0x0018B34E
			public BlackBoxRule ExtractTable { get; private set; }

			// Token: 0x17001477 RID: 5239
			// (get) Token: 0x06007155 RID: 29013 RVA: 0x0018D157 File Offset: 0x0018B357
			// (set) Token: 0x06007156 RID: 29014 RVA: 0x0018D15F File Offset: 0x0018B35F
			public BlackBoxRule ExtractRowBasedTable { get; private set; }

			// Token: 0x17001478 RID: 5240
			// (get) Token: 0x06007157 RID: 29015 RVA: 0x0018D168 File Offset: 0x0018B368
			// (set) Token: 0x06007158 RID: 29016 RVA: 0x0018D170 File Offset: 0x0018B370
			public BlackBoxRule SingleColumn { get; private set; }

			// Token: 0x17001479 RID: 5241
			// (get) Token: 0x06007159 RID: 29017 RVA: 0x0018D179 File Offset: 0x0018B379
			// (set) Token: 0x0600715A RID: 29018 RVA: 0x0018D181 File Offset: 0x0018B381
			public BlackBoxRule ColumnSequence { get; private set; }

			// Token: 0x1700147A RID: 5242
			// (get) Token: 0x0600715B RID: 29019 RVA: 0x0018D18A File Offset: 0x0018B38A
			// (set) Token: 0x0600715C RID: 29020 RVA: 0x0018D192 File Offset: 0x0018B392
			public BlackBoxRule AsCollection { get; private set; }

			// Token: 0x1700147B RID: 5243
			// (get) Token: 0x0600715D RID: 29021 RVA: 0x0018D19B File Offset: 0x0018B39B
			// (set) Token: 0x0600715E RID: 29022 RVA: 0x0018D1A3 File Offset: 0x0018B3A3
			public BlackBoxRule DescendantsOf { get; private set; }

			// Token: 0x1700147C RID: 5244
			// (get) Token: 0x0600715F RID: 29023 RVA: 0x0018D1AC File Offset: 0x0018B3AC
			// (set) Token: 0x06007160 RID: 29024 RVA: 0x0018D1B4 File Offset: 0x0018B3B4
			public BlackBoxRule RightSiblingOf { get; private set; }

			// Token: 0x1700147D RID: 5245
			// (get) Token: 0x06007161 RID: 29025 RVA: 0x0018D1BD File Offset: 0x0018B3BD
			// (set) Token: 0x06007162 RID: 29026 RVA: 0x0018D1C5 File Offset: 0x0018B3C5
			public BlackBoxRule ClassFilter { get; private set; }

			// Token: 0x1700147E RID: 5246
			// (get) Token: 0x06007163 RID: 29027 RVA: 0x0018D1CE File Offset: 0x0018B3CE
			// (set) Token: 0x06007164 RID: 29028 RVA: 0x0018D1D6 File Offset: 0x0018B3D6
			public BlackBoxRule IDFilter { get; private set; }

			// Token: 0x1700147F RID: 5247
			// (get) Token: 0x06007165 RID: 29029 RVA: 0x0018D1DF File Offset: 0x0018B3DF
			// (set) Token: 0x06007166 RID: 29030 RVA: 0x0018D1E7 File Offset: 0x0018B3E7
			public BlackBoxRule NodeNameFilter { get; private set; }

			// Token: 0x17001480 RID: 5248
			// (get) Token: 0x06007167 RID: 29031 RVA: 0x0018D1F0 File Offset: 0x0018B3F0
			// (set) Token: 0x06007168 RID: 29032 RVA: 0x0018D1F8 File Offset: 0x0018B3F8
			public BlackBoxRule ItemPropFilter { get; private set; }

			// Token: 0x17001481 RID: 5249
			// (get) Token: 0x06007169 RID: 29033 RVA: 0x0018D201 File Offset: 0x0018B401
			// (set) Token: 0x0600716A RID: 29034 RVA: 0x0018D209 File Offset: 0x0018B409
			public BlackBoxRule NthChildFilter { get; private set; }

			// Token: 0x17001482 RID: 5250
			// (get) Token: 0x0600716B RID: 29035 RVA: 0x0018D212 File Offset: 0x0018B412
			// (set) Token: 0x0600716C RID: 29036 RVA: 0x0018D21A File Offset: 0x0018B41A
			public BlackBoxRule NthLastChildFilter { get; private set; }

			// Token: 0x17001483 RID: 5251
			// (get) Token: 0x0600716D RID: 29037 RVA: 0x0018D223 File Offset: 0x0018B423
			// (set) Token: 0x0600716E RID: 29038 RVA: 0x0018D22B File Offset: 0x0018B42B
			public BlackBoxRule GEN_NthChildFilter { get; private set; }

			// Token: 0x17001484 RID: 5252
			// (get) Token: 0x0600716F RID: 29039 RVA: 0x0018D234 File Offset: 0x0018B434
			// (set) Token: 0x06007170 RID: 29040 RVA: 0x0018D23C File Offset: 0x0018B43C
			public BlackBoxRule GEN_NthLastChildFilter { get; private set; }

			// Token: 0x17001485 RID: 5253
			// (get) Token: 0x06007171 RID: 29041 RVA: 0x0018D245 File Offset: 0x0018B445
			// (set) Token: 0x06007172 RID: 29042 RVA: 0x0018D24D File Offset: 0x0018B44D
			public BlackBoxRule GEN_ClassFilter { get; private set; }

			// Token: 0x17001486 RID: 5254
			// (get) Token: 0x06007173 RID: 29043 RVA: 0x0018D256 File Offset: 0x0018B456
			// (set) Token: 0x06007174 RID: 29044 RVA: 0x0018D25E File Offset: 0x0018B45E
			public BlackBoxRule GEN_IDFilter { get; private set; }

			// Token: 0x17001487 RID: 5255
			// (get) Token: 0x06007175 RID: 29045 RVA: 0x0018D267 File Offset: 0x0018B467
			// (set) Token: 0x06007176 RID: 29046 RVA: 0x0018D26F File Offset: 0x0018B46F
			public BlackBoxRule GEN_NodeNameFilter { get; private set; }

			// Token: 0x17001488 RID: 5256
			// (get) Token: 0x06007177 RID: 29047 RVA: 0x0018D278 File Offset: 0x0018B478
			// (set) Token: 0x06007178 RID: 29048 RVA: 0x0018D280 File Offset: 0x0018B480
			public BlackBoxRule GEN_ItemPropFilter { get; private set; }

			// Token: 0x17001489 RID: 5257
			// (get) Token: 0x06007179 RID: 29049 RVA: 0x0018D289 File Offset: 0x0018B489
			// (set) Token: 0x0600717A RID: 29050 RVA: 0x0018D291 File Offset: 0x0018B491
			public ConceptRule MapToWebRegion { get; private set; }

			// Token: 0x1700148A RID: 5258
			// (get) Token: 0x0600717B RID: 29051 RVA: 0x0018D29A File Offset: 0x0018B49A
			// (set) Token: 0x0600717C RID: 29052 RVA: 0x0018D2A2 File Offset: 0x0018B4A2
			public ConceptRule FindEndNode { get; private set; }

			// Token: 0x1700148B RID: 5259
			// (get) Token: 0x0600717D RID: 29053 RVA: 0x0018D2AB File Offset: 0x0018B4AB
			// (set) Token: 0x0600717E RID: 29054 RVA: 0x0018D2B3 File Offset: 0x0018B4B3
			public ConceptRule KthNodeInSelection { get; private set; }

			// Token: 0x1700148C RID: 5260
			// (get) Token: 0x0600717F RID: 29055 RVA: 0x0018D2BC File Offset: 0x0018B4BC
			// (set) Token: 0x06007180 RID: 29056 RVA: 0x0018D2C4 File Offset: 0x0018B4C4
			public ConceptRule KthNode { get; private set; }

			// Token: 0x1700148D RID: 5261
			// (get) Token: 0x06007181 RID: 29057 RVA: 0x0018D2CD File Offset: 0x0018B4CD
			// (set) Token: 0x06007182 RID: 29058 RVA: 0x0018D2D5 File Offset: 0x0018B4D5
			public ConceptRule LeafFilter1 { get; private set; }

			// Token: 0x1700148E RID: 5262
			// (get) Token: 0x06007183 RID: 29059 RVA: 0x0018D2DE File Offset: 0x0018B4DE
			// (set) Token: 0x06007184 RID: 29060 RVA: 0x0018D2E6 File Offset: 0x0018B4E6
			public ConceptRule FilterNodesEnd { get; private set; }

			// Token: 0x1700148F RID: 5263
			// (get) Token: 0x06007185 RID: 29061 RVA: 0x0018D2EF File Offset: 0x0018B4EF
			// (set) Token: 0x06007186 RID: 29062 RVA: 0x0018D2F7 File Offset: 0x0018B4F7
			public ConceptRule TakeWhileNodesEnd { get; private set; }

			// Token: 0x17001490 RID: 5264
			// (get) Token: 0x06007187 RID: 29063 RVA: 0x0018D300 File Offset: 0x0018B500
			// (set) Token: 0x06007188 RID: 29064 RVA: 0x0018D308 File Offset: 0x0018B508
			public ConceptRule LeafFilter2 { get; private set; }

			// Token: 0x17001491 RID: 5265
			// (get) Token: 0x06007189 RID: 29065 RVA: 0x0018D311 File Offset: 0x0018B511
			// (set) Token: 0x0600718A RID: 29066 RVA: 0x0018D319 File Offset: 0x0018B519
			public ConceptRule LeafFilter3 { get; private set; }

			// Token: 0x17001492 RID: 5266
			// (get) Token: 0x0600718B RID: 29067 RVA: 0x0018D322 File Offset: 0x0018B522
			// (set) Token: 0x0600718C RID: 29068 RVA: 0x0018D32A File Offset: 0x0018B52A
			public ConceptRule LeafFilter4 { get; private set; }

			// Token: 0x17001493 RID: 5267
			// (get) Token: 0x0600718D RID: 29069 RVA: 0x0018D333 File Offset: 0x0018B533
			// (set) Token: 0x0600718E RID: 29070 RVA: 0x0018D33B File Offset: 0x0018B53B
			public ConceptRule LeafFilter5 { get; private set; }

			// Token: 0x17001494 RID: 5268
			// (get) Token: 0x0600718F RID: 29071 RVA: 0x0018D344 File Offset: 0x0018B544
			// (set) Token: 0x06007190 RID: 29072 RVA: 0x0018D34C File Offset: 0x0018B54C
			public ConceptRule LeafAnd { get; private set; }

			// Token: 0x17001495 RID: 5269
			// (get) Token: 0x06007191 RID: 29073 RVA: 0x0018D355 File Offset: 0x0018B555
			// (set) Token: 0x06007192 RID: 29074 RVA: 0x0018D35D File Offset: 0x0018B55D
			public ConceptRule And { get; private set; }

			// Token: 0x17001496 RID: 5270
			// (get) Token: 0x06007193 RID: 29075 RVA: 0x0018D366 File Offset: 0x0018B566
			// (set) Token: 0x06007194 RID: 29076 RVA: 0x0018D36E File Offset: 0x0018B56E
			public ConversionRule Substring { get; private set; }

			// Token: 0x17001497 RID: 5271
			// (get) Token: 0x06007195 RID: 29077 RVA: 0x0018D377 File Offset: 0x0018B577
			// (set) Token: 0x06007196 RID: 29078 RVA: 0x0018D37F File Offset: 0x0018B57F
			public LetRule LetRegion { get; private set; }

			// Token: 0x17001498 RID: 5272
			// (get) Token: 0x06007197 RID: 29079 RVA: 0x0018D388 File Offset: 0x0018B588
			// (set) Token: 0x06007198 RID: 29080 RVA: 0x0018D390 File Offset: 0x0018B590
			public LetRule LetSubstring { get; private set; }

			// Token: 0x06007199 RID: 29081 RVA: 0x0018D39C File Offset: 0x0018B59C
			public GrammarRules(Grammar grammar)
			{
				this.ConvertToWebRegions = (BlackBoxRule)grammar.Rule("ConvertToWebRegions");
				this.Union = (BlackBoxRule)grammar.Rule("Union");
				this.EmptySequence = (BlackBoxRule)grammar.Rule("EmptySequence");
				this.NodeToWebRegion = (BlackBoxRule)grammar.Rule("NodeToWebRegion");
				this.NodeToWebRegionInSequence = (BlackBoxRule)grammar.Rule("NodeToWebRegionInSequence");
				this.NodeRegionToWebRegion = (BlackBoxRule)grammar.Rule("NodeRegionToWebRegion");
				this.NodeRegionToWebRegionInSequence = (BlackBoxRule)grammar.Rule("NodeRegionToWebRegionInSequence");
				this.SingleSelection1 = (BlackBoxRule)grammar.Rule("SingleSelection1");
				this.DisjSelection1 = (BlackBoxRule)grammar.Rule("DisjSelection1");
				this.CSSSelection = (BlackBoxRule)grammar.Rule("CSSSelection");
				this.YoungerSiblingsOf = (BlackBoxRule)grammar.Rule("YoungerSiblingsOf");
				this.LeafChildrenOf1 = (BlackBoxRule)grammar.Rule("LeafChildrenOf1");
				this.SingleSelection2 = (BlackBoxRule)grammar.Rule("SingleSelection2");
				this.DisjSelection2 = (BlackBoxRule)grammar.Rule("DisjSelection2");
				this.LeafChildrenOf2 = (BlackBoxRule)grammar.Rule("LeafChildrenOf2");
				this.SingleSelection3 = (BlackBoxRule)grammar.Rule("SingleSelection3");
				this.DisjSelection3 = (BlackBoxRule)grammar.Rule("DisjSelection3");
				this.LeafChildrenOf3 = (BlackBoxRule)grammar.Rule("LeafChildrenOf3");
				this.SingleSelection4 = (BlackBoxRule)grammar.Rule("SingleSelection4");
				this.DisjSelection4 = (BlackBoxRule)grammar.Rule("DisjSelection4");
				this.LeafChildrenOf4 = (BlackBoxRule)grammar.Rule("LeafChildrenOf4");
				this.SingleSelection5 = (BlackBoxRule)grammar.Rule("SingleSelection5");
				this.DisjSelection5 = (BlackBoxRule)grammar.Rule("DisjSelection5");
				this.ContainsDate = (BlackBoxRule)grammar.Rule("ContainsDate");
				this.ContainsNum = (BlackBoxRule)grammar.Rule("ContainsNum");
				this.ID_substring = (BlackBoxRule)grammar.Rule("ID_substring");
				this.Class = (BlackBoxRule)grammar.Rule("Class");
				this.TitleIs = (BlackBoxRule)grammar.Rule("TitleIs");
				this.NodeName = (BlackBoxRule)grammar.Rule("NodeName");
				this.NodeNames = (BlackBoxRule)grammar.Rule("NodeNames");
				this.NthChild = (BlackBoxRule)grammar.Rule("NthChild");
				this.NthLastChild = (BlackBoxRule)grammar.Rule("NthLastChild");
				this.ContainsLeafNodes = (BlackBoxRule)grammar.Rule("ContainsLeafNodes");
				this.ChildrenCount = (BlackBoxRule)grammar.Rule("ChildrenCount");
				this.HasAttribute = (BlackBoxRule)grammar.Rule("HasAttribute");
				this.HasStyle = (BlackBoxRule)grammar.Rule("HasStyle");
				this.HasEntityAnchor = (BlackBoxRule)grammar.Rule("HasEntityAnchor");
				this.AppendField = (BlackBoxRule)grammar.Rule("AppendField");
				this.TrimmedTextField = (BlackBoxRule)grammar.Rule("TrimmedTextField");
				this.SubstringField = (BlackBoxRule)grammar.Rule("SubstringField");
				this.GetValueSubstring = (BlackBoxRule)grammar.Rule("GetValueSubstring");
				this.SelectSubstring = (BlackBoxRule)grammar.Rule("SelectSubstring");
				this.SingleSubstring = (BlackBoxRule)grammar.Rule("SingleSubstring");
				this.DisjSubstring = (BlackBoxRule)grammar.Rule("DisjSubstring");
				this.ExtractTable = (BlackBoxRule)grammar.Rule("ExtractTable");
				this.ExtractRowBasedTable = (BlackBoxRule)grammar.Rule("ExtractRowBasedTable");
				this.SingleColumn = (BlackBoxRule)grammar.Rule("SingleColumn");
				this.ColumnSequence = (BlackBoxRule)grammar.Rule("ColumnSequence");
				this.AsCollection = (BlackBoxRule)grammar.Rule("AsCollection");
				this.DescendantsOf = (BlackBoxRule)grammar.Rule("DescendantsOf");
				this.RightSiblingOf = (BlackBoxRule)grammar.Rule("RightSiblingOf");
				this.ClassFilter = (BlackBoxRule)grammar.Rule("ClassFilter");
				this.IDFilter = (BlackBoxRule)grammar.Rule("IDFilter");
				this.NodeNameFilter = (BlackBoxRule)grammar.Rule("NodeNameFilter");
				this.ItemPropFilter = (BlackBoxRule)grammar.Rule("ItemPropFilter");
				this.NthChildFilter = (BlackBoxRule)grammar.Rule("NthChildFilter");
				this.NthLastChildFilter = (BlackBoxRule)grammar.Rule("NthLastChildFilter");
				this.GEN_NthChildFilter = (BlackBoxRule)grammar.Rule("GEN_NthChildFilter");
				this.GEN_NthLastChildFilter = (BlackBoxRule)grammar.Rule("GEN_NthLastChildFilter");
				this.GEN_ClassFilter = (BlackBoxRule)grammar.Rule("GEN_ClassFilter");
				this.GEN_IDFilter = (BlackBoxRule)grammar.Rule("GEN_IDFilter");
				this.GEN_NodeNameFilter = (BlackBoxRule)grammar.Rule("GEN_NodeNameFilter");
				this.GEN_ItemPropFilter = (BlackBoxRule)grammar.Rule("GEN_ItemPropFilter");
				this.MapToWebRegion = (ConceptRule)grammar.Rule("MapToWebRegion");
				this.FindEndNode = (ConceptRule)grammar.Rule("FindEndNode");
				this.KthNodeInSelection = (ConceptRule)grammar.Rule("KthNodeInSelection");
				this.KthNode = (ConceptRule)grammar.Rule("KthNode");
				this.LeafFilter1 = (ConceptRule)grammar.Rule("LeafFilter1");
				this.FilterNodesEnd = (ConceptRule)grammar.Rule("FilterNodesEnd");
				this.TakeWhileNodesEnd = (ConceptRule)grammar.Rule("TakeWhileNodesEnd");
				this.LeafFilter2 = (ConceptRule)grammar.Rule("LeafFilter2");
				this.LeafFilter3 = (ConceptRule)grammar.Rule("LeafFilter3");
				this.LeafFilter4 = (ConceptRule)grammar.Rule("LeafFilter4");
				this.LeafFilter5 = (ConceptRule)grammar.Rule("LeafFilter5");
				this.LeafAnd = (ConceptRule)grammar.Rule("LeafAnd");
				this.And = (ConceptRule)grammar.Rule("And");
				this.Substring = (ConversionRule)grammar.Rule("Substring");
				this.LetRegion = (LetRule)grammar.Rule("LetRegion");
				this.LetSubstring = (LetRule)grammar.Rule("LetSubstring");
			}
		}

		// Token: 0x02000FE3 RID: 4067
		public class GrammarUnnamedConversions
		{
			// Token: 0x17001499 RID: 5273
			// (get) Token: 0x0600719A RID: 29082 RVA: 0x0018DA79 File Offset: 0x0018BC79
			// (set) Token: 0x0600719B RID: 29083 RVA: 0x0018DA81 File Offset: 0x0018BC81
			public ConversionRule resultSequence_subNodeSequence { get; private set; }

			// Token: 0x1700149A RID: 5274
			// (get) Token: 0x0600719C RID: 29084 RVA: 0x0018DA8A File Offset: 0x0018BC8A
			// (set) Token: 0x0600719D RID: 29085 RVA: 0x0018DA92 File Offset: 0x0018BC92
			public ConversionRule resultSequence_regionSequence { get; private set; }

			// Token: 0x1700149B RID: 5275
			// (get) Token: 0x0600719E RID: 29086 RVA: 0x0018DA9B File Offset: 0x0018BC9B
			// (set) Token: 0x0600719F RID: 29087 RVA: 0x0018DAA3 File Offset: 0x0018BCA3
			public ConversionRule resultRegion_subNode { get; private set; }

			// Token: 0x1700149C RID: 5276
			// (get) Token: 0x060071A0 RID: 29088 RVA: 0x0018DAAC File Offset: 0x0018BCAC
			// (set) Token: 0x060071A1 RID: 29089 RVA: 0x0018DAB4 File Offset: 0x0018BCB4
			public ConversionRule resultRegion_region { get; private set; }

			// Token: 0x1700149D RID: 5277
			// (get) Token: 0x060071A2 RID: 29090 RVA: 0x0018DABD File Offset: 0x0018BCBD
			// (set) Token: 0x060071A3 RID: 29091 RVA: 0x0018DAC5 File Offset: 0x0018BCC5
			public ConversionRule selectionEnd_regionStartSiblings { get; private set; }

			// Token: 0x1700149E RID: 5278
			// (get) Token: 0x060071A4 RID: 29092 RVA: 0x0018DACE File Offset: 0x0018BCCE
			// (set) Token: 0x060071A5 RID: 29093 RVA: 0x0018DAD6 File Offset: 0x0018BCD6
			public ConversionRule selection2_allNodes { get; private set; }

			// Token: 0x1700149F RID: 5279
			// (get) Token: 0x060071A6 RID: 29094 RVA: 0x0018DADF File Offset: 0x0018BCDF
			// (set) Token: 0x060071A7 RID: 29095 RVA: 0x0018DAE7 File Offset: 0x0018BCE7
			public ConversionRule selection4_allNodes { get; private set; }

			// Token: 0x170014A0 RID: 5280
			// (get) Token: 0x060071A8 RID: 29096 RVA: 0x0018DAF0 File Offset: 0x0018BCF0
			// (set) Token: 0x060071A9 RID: 29097 RVA: 0x0018DAF8 File Offset: 0x0018BCF8
			public ConversionRule selection6_allNodes { get; private set; }

			// Token: 0x170014A1 RID: 5281
			// (get) Token: 0x060071AA RID: 29098 RVA: 0x0018DB01 File Offset: 0x0018BD01
			// (set) Token: 0x060071AB RID: 29099 RVA: 0x0018DB09 File Offset: 0x0018BD09
			public ConversionRule selection8_allNodes { get; private set; }

			// Token: 0x170014A2 RID: 5282
			// (get) Token: 0x060071AC RID: 29100 RVA: 0x0018DB12 File Offset: 0x0018BD12
			// (set) Token: 0x060071AD RID: 29101 RVA: 0x0018DB1A File Offset: 0x0018BD1A
			public ConversionRule selection10_allNodes { get; private set; }

			// Token: 0x170014A3 RID: 5283
			// (get) Token: 0x060071AE RID: 29102 RVA: 0x0018DB23 File Offset: 0x0018BD23
			// (set) Token: 0x060071AF RID: 29103 RVA: 0x0018DB2B File Offset: 0x0018BD2B
			public ConversionRule leafFExpr_leafAtom { get; private set; }

			// Token: 0x170014A4 RID: 5284
			// (get) Token: 0x060071B0 RID: 29104 RVA: 0x0018DB34 File Offset: 0x0018BD34
			// (set) Token: 0x060071B1 RID: 29105 RVA: 0x0018DB3C File Offset: 0x0018BD3C
			public ConversionRule leafAtom_literalExpr { get; private set; }

			// Token: 0x170014A5 RID: 5285
			// (get) Token: 0x060071B2 RID: 29106 RVA: 0x0018DB45 File Offset: 0x0018BD45
			// (set) Token: 0x060071B3 RID: 29107 RVA: 0x0018DB4D File Offset: 0x0018BD4D
			public ConversionRule literalExpr_atomExpr { get; private set; }

			// Token: 0x170014A6 RID: 5286
			// (get) Token: 0x060071B4 RID: 29108 RVA: 0x0018DB56 File Offset: 0x0018BD56
			// (set) Token: 0x060071B5 RID: 29109 RVA: 0x0018DB5E File Offset: 0x0018BD5E
			public ConversionRule fexpr_literalExpr { get; private set; }

			// Token: 0x170014A7 RID: 5287
			// (get) Token: 0x060071B6 RID: 29110 RVA: 0x0018DB67 File Offset: 0x0018BD67
			// (set) Token: 0x060071B7 RID: 29111 RVA: 0x0018DB6F File Offset: 0x0018BD6F
			public ConversionRule resultFields_singletonField { get; private set; }

			// Token: 0x060071B8 RID: 29112 RVA: 0x0018DB78 File Offset: 0x0018BD78
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.resultSequence_subNodeSequence = (ConversionRule)grammar.Rule("~convert_resultSequence_subNodeSequence");
				this.resultSequence_regionSequence = (ConversionRule)grammar.Rule("~convert_resultSequence_regionSequence");
				this.resultRegion_subNode = (ConversionRule)grammar.Rule("~convert_resultRegion_subNode");
				this.resultRegion_region = (ConversionRule)grammar.Rule("~convert_resultRegion_region");
				this.selectionEnd_regionStartSiblings = (ConversionRule)grammar.Rule("~convert_selectionEnd_regionStartSiblings");
				this.selection2_allNodes = (ConversionRule)grammar.Rule("~convert_selection2_allNodes");
				this.selection4_allNodes = (ConversionRule)grammar.Rule("~convert_selection4_allNodes");
				this.selection6_allNodes = (ConversionRule)grammar.Rule("~convert_selection6_allNodes");
				this.selection8_allNodes = (ConversionRule)grammar.Rule("~convert_selection8_allNodes");
				this.selection10_allNodes = (ConversionRule)grammar.Rule("~convert_selection10_allNodes");
				this.leafFExpr_leafAtom = (ConversionRule)grammar.Rule("~convert_leafFExpr_leafAtom");
				this.leafAtom_literalExpr = (ConversionRule)grammar.Rule("~convert_leafAtom_literalExpr");
				this.literalExpr_atomExpr = (ConversionRule)grammar.Rule("~convert_literalExpr_atomExpr");
				this.fexpr_literalExpr = (ConversionRule)grammar.Rule("~convert_fexpr_literalExpr");
				this.resultFields_singletonField = (ConversionRule)grammar.Rule("~convert_resultFields_singletonField");
			}
		}

		// Token: 0x02000FE4 RID: 4068
		public class GrammarHoles
		{
			// Token: 0x170014A8 RID: 5288
			// (get) Token: 0x060071B9 RID: 29113 RVA: 0x0018DCD5 File Offset: 0x0018BED5
			// (set) Token: 0x060071BA RID: 29114 RVA: 0x0018DCDD File Offset: 0x0018BEDD
			public Hole resultSequence { get; private set; }

			// Token: 0x170014A9 RID: 5289
			// (get) Token: 0x060071BB RID: 29115 RVA: 0x0018DCE6 File Offset: 0x0018BEE6
			// (set) Token: 0x060071BC RID: 29116 RVA: 0x0018DCEE File Offset: 0x0018BEEE
			public Hole resultRegion { get; private set; }

			// Token: 0x170014AA RID: 5290
			// (get) Token: 0x060071BD RID: 29117 RVA: 0x0018DCF7 File Offset: 0x0018BEF7
			// (set) Token: 0x060071BE RID: 29118 RVA: 0x0018DCFF File Offset: 0x0018BEFF
			public Hole subNodeSequence { get; private set; }

			// Token: 0x170014AB RID: 5291
			// (get) Token: 0x060071BF RID: 29119 RVA: 0x0018DD08 File Offset: 0x0018BF08
			// (set) Token: 0x060071C0 RID: 29120 RVA: 0x0018DD10 File Offset: 0x0018BF10
			public Hole node { get; private set; }

			// Token: 0x170014AC RID: 5292
			// (get) Token: 0x060071C1 RID: 29121 RVA: 0x0018DD19 File Offset: 0x0018BF19
			// (set) Token: 0x060071C2 RID: 29122 RVA: 0x0018DD21 File Offset: 0x0018BF21
			public Hole subNode { get; private set; }

			// Token: 0x170014AD RID: 5293
			// (get) Token: 0x060071C3 RID: 29123 RVA: 0x0018DD2A File Offset: 0x0018BF2A
			// (set) Token: 0x060071C4 RID: 29124 RVA: 0x0018DD32 File Offset: 0x0018BF32
			public Hole mapNodeInSequence { get; private set; }

			// Token: 0x170014AE RID: 5294
			// (get) Token: 0x060071C5 RID: 29125 RVA: 0x0018DD3B File Offset: 0x0018BF3B
			// (set) Token: 0x060071C6 RID: 29126 RVA: 0x0018DD43 File Offset: 0x0018BF43
			public Hole regionSequence { get; private set; }

			// Token: 0x170014AF RID: 5295
			// (get) Token: 0x060071C7 RID: 29127 RVA: 0x0018DD4C File Offset: 0x0018BF4C
			// (set) Token: 0x060071C8 RID: 29128 RVA: 0x0018DD54 File Offset: 0x0018BF54
			public Hole regionStart { get; private set; }

			// Token: 0x170014B0 RID: 5296
			// (get) Token: 0x060071C9 RID: 29129 RVA: 0x0018DD5D File Offset: 0x0018BF5D
			// (set) Token: 0x060071CA RID: 29130 RVA: 0x0018DD65 File Offset: 0x0018BF65
			public Hole region { get; private set; }

			// Token: 0x170014B1 RID: 5297
			// (get) Token: 0x060071CB RID: 29131 RVA: 0x0018DD6E File Offset: 0x0018BF6E
			// (set) Token: 0x060071CC RID: 29132 RVA: 0x0018DD76 File Offset: 0x0018BF76
			public Hole mapRegionInSequence { get; private set; }

			// Token: 0x170014B2 RID: 5298
			// (get) Token: 0x060071CD RID: 29133 RVA: 0x0018DD7F File Offset: 0x0018BF7F
			// (set) Token: 0x060071CE RID: 29134 RVA: 0x0018DD87 File Offset: 0x0018BF87
			public Hole beginNode { get; private set; }

			// Token: 0x170014B3 RID: 5299
			// (get) Token: 0x060071CF RID: 29135 RVA: 0x0018DD90 File Offset: 0x0018BF90
			// (set) Token: 0x060071D0 RID: 29136 RVA: 0x0018DD98 File Offset: 0x0018BF98
			public Hole endNode { get; private set; }

			// Token: 0x170014B4 RID: 5300
			// (get) Token: 0x060071D1 RID: 29137 RVA: 0x0018DDA1 File Offset: 0x0018BFA1
			// (set) Token: 0x060071D2 RID: 29138 RVA: 0x0018DDA9 File Offset: 0x0018BFA9
			public Hole selection { get; private set; }

			// Token: 0x170014B5 RID: 5301
			// (get) Token: 0x060071D3 RID: 29139 RVA: 0x0018DDB2 File Offset: 0x0018BFB2
			// (set) Token: 0x060071D4 RID: 29140 RVA: 0x0018DDBA File Offset: 0x0018BFBA
			public Hole filterSelection { get; private set; }

			// Token: 0x170014B6 RID: 5302
			// (get) Token: 0x060071D5 RID: 29141 RVA: 0x0018DDC3 File Offset: 0x0018BFC3
			// (set) Token: 0x060071D6 RID: 29142 RVA: 0x0018DDCB File Offset: 0x0018BFCB
			public Hole selectionEnd { get; private set; }

			// Token: 0x170014B7 RID: 5303
			// (get) Token: 0x060071D7 RID: 29143 RVA: 0x0018DDD4 File Offset: 0x0018BFD4
			// (set) Token: 0x060071D8 RID: 29144 RVA: 0x0018DDDC File Offset: 0x0018BFDC
			public Hole regionStartSiblings { get; private set; }

			// Token: 0x170014B8 RID: 5304
			// (get) Token: 0x060071D9 RID: 29145 RVA: 0x0018DDE5 File Offset: 0x0018BFE5
			// (set) Token: 0x060071DA RID: 29146 RVA: 0x0018DDED File Offset: 0x0018BFED
			public Hole selection2 { get; private set; }

			// Token: 0x170014B9 RID: 5305
			// (get) Token: 0x060071DB RID: 29147 RVA: 0x0018DDF6 File Offset: 0x0018BFF6
			// (set) Token: 0x060071DC RID: 29148 RVA: 0x0018DDFE File Offset: 0x0018BFFE
			public Hole selection3 { get; private set; }

			// Token: 0x170014BA RID: 5306
			// (get) Token: 0x060071DD RID: 29149 RVA: 0x0018DE07 File Offset: 0x0018C007
			// (set) Token: 0x060071DE RID: 29150 RVA: 0x0018DE0F File Offset: 0x0018C00F
			public Hole filterSelection2 { get; private set; }

			// Token: 0x170014BB RID: 5307
			// (get) Token: 0x060071DF RID: 29151 RVA: 0x0018DE18 File Offset: 0x0018C018
			// (set) Token: 0x060071E0 RID: 29152 RVA: 0x0018DE20 File Offset: 0x0018C020
			public Hole selection4 { get; private set; }

			// Token: 0x170014BC RID: 5308
			// (get) Token: 0x060071E1 RID: 29153 RVA: 0x0018DE29 File Offset: 0x0018C029
			// (set) Token: 0x060071E2 RID: 29154 RVA: 0x0018DE31 File Offset: 0x0018C031
			public Hole selection5 { get; private set; }

			// Token: 0x170014BD RID: 5309
			// (get) Token: 0x060071E3 RID: 29155 RVA: 0x0018DE3A File Offset: 0x0018C03A
			// (set) Token: 0x060071E4 RID: 29156 RVA: 0x0018DE42 File Offset: 0x0018C042
			public Hole filterSelection3 { get; private set; }

			// Token: 0x170014BE RID: 5310
			// (get) Token: 0x060071E5 RID: 29157 RVA: 0x0018DE4B File Offset: 0x0018C04B
			// (set) Token: 0x060071E6 RID: 29158 RVA: 0x0018DE53 File Offset: 0x0018C053
			public Hole selection6 { get; private set; }

			// Token: 0x170014BF RID: 5311
			// (get) Token: 0x060071E7 RID: 29159 RVA: 0x0018DE5C File Offset: 0x0018C05C
			// (set) Token: 0x060071E8 RID: 29160 RVA: 0x0018DE64 File Offset: 0x0018C064
			public Hole selection7 { get; private set; }

			// Token: 0x170014C0 RID: 5312
			// (get) Token: 0x060071E9 RID: 29161 RVA: 0x0018DE6D File Offset: 0x0018C06D
			// (set) Token: 0x060071EA RID: 29162 RVA: 0x0018DE75 File Offset: 0x0018C075
			public Hole filterSelection4 { get; private set; }

			// Token: 0x170014C1 RID: 5313
			// (get) Token: 0x060071EB RID: 29163 RVA: 0x0018DE7E File Offset: 0x0018C07E
			// (set) Token: 0x060071EC RID: 29164 RVA: 0x0018DE86 File Offset: 0x0018C086
			public Hole selection8 { get; private set; }

			// Token: 0x170014C2 RID: 5314
			// (get) Token: 0x060071ED RID: 29165 RVA: 0x0018DE8F File Offset: 0x0018C08F
			// (set) Token: 0x060071EE RID: 29166 RVA: 0x0018DE97 File Offset: 0x0018C097
			public Hole selection9 { get; private set; }

			// Token: 0x170014C3 RID: 5315
			// (get) Token: 0x060071EF RID: 29167 RVA: 0x0018DEA0 File Offset: 0x0018C0A0
			// (set) Token: 0x060071F0 RID: 29168 RVA: 0x0018DEA8 File Offset: 0x0018C0A8
			public Hole filterSelection5 { get; private set; }

			// Token: 0x170014C4 RID: 5316
			// (get) Token: 0x060071F1 RID: 29169 RVA: 0x0018DEB1 File Offset: 0x0018C0B1
			// (set) Token: 0x060071F2 RID: 29170 RVA: 0x0018DEB9 File Offset: 0x0018C0B9
			public Hole selection10 { get; private set; }

			// Token: 0x170014C5 RID: 5317
			// (get) Token: 0x060071F3 RID: 29171 RVA: 0x0018DEC2 File Offset: 0x0018C0C2
			// (set) Token: 0x060071F4 RID: 29172 RVA: 0x0018DECA File Offset: 0x0018C0CA
			public Hole leafFExpr { get; private set; }

			// Token: 0x170014C6 RID: 5318
			// (get) Token: 0x060071F5 RID: 29173 RVA: 0x0018DED3 File Offset: 0x0018C0D3
			// (set) Token: 0x060071F6 RID: 29174 RVA: 0x0018DEDB File Offset: 0x0018C0DB
			public Hole leafAtom { get; private set; }

			// Token: 0x170014C7 RID: 5319
			// (get) Token: 0x060071F7 RID: 29175 RVA: 0x0018DEE4 File Offset: 0x0018C0E4
			// (set) Token: 0x060071F8 RID: 29176 RVA: 0x0018DEEC File Offset: 0x0018C0EC
			public Hole atomExpr { get; private set; }

			// Token: 0x170014C8 RID: 5320
			// (get) Token: 0x060071F9 RID: 29177 RVA: 0x0018DEF5 File Offset: 0x0018C0F5
			// (set) Token: 0x060071FA RID: 29178 RVA: 0x0018DEFD File Offset: 0x0018C0FD
			public Hole literalExpr { get; private set; }

			// Token: 0x170014C9 RID: 5321
			// (get) Token: 0x060071FB RID: 29179 RVA: 0x0018DF06 File Offset: 0x0018C106
			// (set) Token: 0x060071FC RID: 29180 RVA: 0x0018DF0E File Offset: 0x0018C10E
			public Hole fexpr { get; private set; }

			// Token: 0x170014CA RID: 5322
			// (get) Token: 0x060071FD RID: 29181 RVA: 0x0018DF17 File Offset: 0x0018C117
			// (set) Token: 0x060071FE RID: 29182 RVA: 0x0018DF1F File Offset: 0x0018C11F
			public Hole resultFields { get; private set; }

			// Token: 0x170014CB RID: 5323
			// (get) Token: 0x060071FF RID: 29183 RVA: 0x0018DF28 File Offset: 0x0018C128
			// (set) Token: 0x06007200 RID: 29184 RVA: 0x0018DF30 File Offset: 0x0018C130
			public Hole singletonField { get; private set; }

			// Token: 0x170014CC RID: 5324
			// (get) Token: 0x06007201 RID: 29185 RVA: 0x0018DF39 File Offset: 0x0018C139
			// (set) Token: 0x06007202 RID: 29186 RVA: 0x0018DF41 File Offset: 0x0018C141
			public Hole fieldSubstring { get; private set; }

			// Token: 0x170014CD RID: 5325
			// (get) Token: 0x06007203 RID: 29187 RVA: 0x0018DF4A File Offset: 0x0018C14A
			// (set) Token: 0x06007204 RID: 29188 RVA: 0x0018DF52 File Offset: 0x0018C152
			public Hole cs { get; private set; }

			// Token: 0x170014CE RID: 5326
			// (get) Token: 0x06007205 RID: 29189 RVA: 0x0018DF5B File Offset: 0x0018C15B
			// (set) Token: 0x06007206 RID: 29190 RVA: 0x0018DF63 File Offset: 0x0018C163
			public Hole y { get; private set; }

			// Token: 0x170014CF RID: 5327
			// (get) Token: 0x06007207 RID: 29191 RVA: 0x0018DF6C File Offset: 0x0018C16C
			// (set) Token: 0x06007208 RID: 29192 RVA: 0x0018DF74 File Offset: 0x0018C174
			public Hole selectSubstring { get; private set; }

			// Token: 0x170014D0 RID: 5328
			// (get) Token: 0x06007209 RID: 29193 RVA: 0x0018DF7D File Offset: 0x0018C17D
			// (set) Token: 0x0600720A RID: 29194 RVA: 0x0018DF85 File Offset: 0x0018C185
			public Hole substringDisj { get; private set; }

			// Token: 0x170014D1 RID: 5329
			// (get) Token: 0x0600720B RID: 29195 RVA: 0x0018DF8E File Offset: 0x0018C18E
			// (set) Token: 0x0600720C RID: 29196 RVA: 0x0018DF96 File Offset: 0x0018C196
			public Hole substring { get; private set; }

			// Token: 0x170014D2 RID: 5330
			// (get) Token: 0x0600720D RID: 29197 RVA: 0x0018DF9F File Offset: 0x0018C19F
			// (set) Token: 0x0600720E RID: 29198 RVA: 0x0018DFA7 File Offset: 0x0018C1A7
			public Hole resultTable { get; private set; }

			// Token: 0x170014D3 RID: 5331
			// (get) Token: 0x0600720F RID: 29199 RVA: 0x0018DFB0 File Offset: 0x0018C1B0
			// (set) Token: 0x06007210 RID: 29200 RVA: 0x0018DFB8 File Offset: 0x0018C1B8
			public Hole columnSelectors { get; private set; }

			// Token: 0x170014D4 RID: 5332
			// (get) Token: 0x06007211 RID: 29201 RVA: 0x0018DFC1 File Offset: 0x0018C1C1
			// (set) Token: 0x06007212 RID: 29202 RVA: 0x0018DFC9 File Offset: 0x0018C1C9
			public Hole name { get; private set; }

			// Token: 0x170014D5 RID: 5333
			// (get) Token: 0x06007213 RID: 29203 RVA: 0x0018DFD2 File Offset: 0x0018C1D2
			// (set) Token: 0x06007214 RID: 29204 RVA: 0x0018DFDA File Offset: 0x0018C1DA
			public Hole value { get; private set; }

			// Token: 0x170014D6 RID: 5334
			// (get) Token: 0x06007215 RID: 29205 RVA: 0x0018DFE3 File Offset: 0x0018C1E3
			// (set) Token: 0x06007216 RID: 29206 RVA: 0x0018DFEB File Offset: 0x0018C1EB
			public Hole cssSelector { get; private set; }

			// Token: 0x170014D7 RID: 5335
			// (get) Token: 0x06007217 RID: 29207 RVA: 0x0018DFF4 File Offset: 0x0018C1F4
			// (set) Token: 0x06007218 RID: 29208 RVA: 0x0018DFFC File Offset: 0x0018C1FC
			public Hole className { get; private set; }

			// Token: 0x170014D8 RID: 5336
			// (get) Token: 0x06007219 RID: 29209 RVA: 0x0018E005 File Offset: 0x0018C205
			// (set) Token: 0x0600721A RID: 29210 RVA: 0x0018E00D File Offset: 0x0018C20D
			public Hole idName { get; private set; }

			// Token: 0x170014D9 RID: 5337
			// (get) Token: 0x0600721B RID: 29211 RVA: 0x0018E016 File Offset: 0x0018C216
			// (set) Token: 0x0600721C RID: 29212 RVA: 0x0018E01E File Offset: 0x0018C21E
			public Hole nodeName { get; private set; }

			// Token: 0x170014DA RID: 5338
			// (get) Token: 0x0600721D RID: 29213 RVA: 0x0018E027 File Offset: 0x0018C227
			// (set) Token: 0x0600721E RID: 29214 RVA: 0x0018E02F File Offset: 0x0018C22F
			public Hole propName { get; private set; }

			// Token: 0x170014DB RID: 5339
			// (get) Token: 0x0600721F RID: 29215 RVA: 0x0018E038 File Offset: 0x0018C238
			// (set) Token: 0x06007220 RID: 29216 RVA: 0x0018E040 File Offset: 0x0018C240
			public Hole idx1 { get; private set; }

			// Token: 0x170014DC RID: 5340
			// (get) Token: 0x06007221 RID: 29217 RVA: 0x0018E049 File Offset: 0x0018C249
			// (set) Token: 0x06007222 RID: 29218 RVA: 0x0018E051 File Offset: 0x0018C251
			public Hole idx2 { get; private set; }

			// Token: 0x170014DD RID: 5341
			// (get) Token: 0x06007223 RID: 29219 RVA: 0x0018E05A File Offset: 0x0018C25A
			// (set) Token: 0x06007224 RID: 29220 RVA: 0x0018E062 File Offset: 0x0018C262
			public Hole names { get; private set; }

			// Token: 0x170014DE RID: 5342
			// (get) Token: 0x06007225 RID: 29221 RVA: 0x0018E06B File Offset: 0x0018C26B
			// (set) Token: 0x06007226 RID: 29222 RVA: 0x0018E073 File Offset: 0x0018C273
			public Hole count { get; private set; }

			// Token: 0x170014DF RID: 5343
			// (get) Token: 0x06007227 RID: 29223 RVA: 0x0018E07C File Offset: 0x0018C27C
			// (set) Token: 0x06007228 RID: 29224 RVA: 0x0018E084 File Offset: 0x0018C284
			public Hole substringFeatureNames { get; private set; }

			// Token: 0x170014E0 RID: 5344
			// (get) Token: 0x06007229 RID: 29225 RVA: 0x0018E08D File Offset: 0x0018C28D
			// (set) Token: 0x0600722A RID: 29226 RVA: 0x0018E095 File Offset: 0x0018C295
			public Hole substringFeatureValues { get; private set; }

			// Token: 0x170014E1 RID: 5345
			// (get) Token: 0x0600722B RID: 29227 RVA: 0x0018E09E File Offset: 0x0018C29E
			// (set) Token: 0x0600722C RID: 29228 RVA: 0x0018E0A6 File Offset: 0x0018C2A6
			public Hole k { get; private set; }

			// Token: 0x170014E2 RID: 5346
			// (get) Token: 0x0600722D RID: 29229 RVA: 0x0018E0AF File Offset: 0x0018C2AF
			// (set) Token: 0x0600722E RID: 29230 RVA: 0x0018E0B7 File Offset: 0x0018C2B7
			public Hole entityObjs { get; private set; }

			// Token: 0x170014E3 RID: 5347
			// (get) Token: 0x0600722F RID: 29231 RVA: 0x0018E0C0 File Offset: 0x0018C2C0
			// (set) Token: 0x06007230 RID: 29232 RVA: 0x0018E0C8 File Offset: 0x0018C2C8
			public Hole direction { get; private set; }

			// Token: 0x170014E4 RID: 5348
			// (get) Token: 0x06007231 RID: 29233 RVA: 0x0018E0D1 File Offset: 0x0018C2D1
			// (set) Token: 0x06007232 RID: 29234 RVA: 0x0018E0D9 File Offset: 0x0018C2D9
			public Hole allNodes { get; private set; }

			// Token: 0x170014E5 RID: 5349
			// (get) Token: 0x06007233 RID: 29235 RVA: 0x0018E0E2 File Offset: 0x0018C2E2
			// (set) Token: 0x06007234 RID: 29236 RVA: 0x0018E0EA File Offset: 0x0018C2EA
			public Hole nodeCollection { get; private set; }

			// Token: 0x170014E6 RID: 5350
			// (get) Token: 0x06007235 RID: 29237 RVA: 0x0018E0F3 File Offset: 0x0018C2F3
			// (set) Token: 0x06007236 RID: 29238 RVA: 0x0018E0FB File Offset: 0x0018C2FB
			public Hole gen_NthChild { get; private set; }

			// Token: 0x170014E7 RID: 5351
			// (get) Token: 0x06007237 RID: 29239 RVA: 0x0018E104 File Offset: 0x0018C304
			// (set) Token: 0x06007238 RID: 29240 RVA: 0x0018E10C File Offset: 0x0018C30C
			public Hole gen_NthLastChild { get; private set; }

			// Token: 0x170014E8 RID: 5352
			// (get) Token: 0x06007239 RID: 29241 RVA: 0x0018E115 File Offset: 0x0018C315
			// (set) Token: 0x0600723A RID: 29242 RVA: 0x0018E11D File Offset: 0x0018C31D
			public Hole gen_Class { get; private set; }

			// Token: 0x170014E9 RID: 5353
			// (get) Token: 0x0600723B RID: 29243 RVA: 0x0018E126 File Offset: 0x0018C326
			// (set) Token: 0x0600723C RID: 29244 RVA: 0x0018E12E File Offset: 0x0018C32E
			public Hole gen_ID { get; private set; }

			// Token: 0x170014EA RID: 5354
			// (get) Token: 0x0600723D RID: 29245 RVA: 0x0018E137 File Offset: 0x0018C337
			// (set) Token: 0x0600723E RID: 29246 RVA: 0x0018E13F File Offset: 0x0018C33F
			public Hole gen_NodeName { get; private set; }

			// Token: 0x170014EB RID: 5355
			// (get) Token: 0x0600723F RID: 29247 RVA: 0x0018E148 File Offset: 0x0018C348
			// (set) Token: 0x06007240 RID: 29248 RVA: 0x0018E150 File Offset: 0x0018C350
			public Hole gen_ItemProp { get; private set; }

			// Token: 0x170014EC RID: 5356
			// (get) Token: 0x06007241 RID: 29249 RVA: 0x0018E159 File Offset: 0x0018C359
			// (set) Token: 0x06007242 RID: 29250 RVA: 0x0018E161 File Offset: 0x0018C361
			public Hole obj { get; private set; }

			// Token: 0x170014ED RID: 5357
			// (get) Token: 0x06007243 RID: 29251 RVA: 0x0018E16A File Offset: 0x0018C36A
			// (set) Token: 0x06007244 RID: 29252 RVA: 0x0018E172 File Offset: 0x0018C372
			public Hole _LFun0 { get; private set; }

			// Token: 0x170014EE RID: 5358
			// (get) Token: 0x06007245 RID: 29253 RVA: 0x0018E17B File Offset: 0x0018C37B
			// (set) Token: 0x06007246 RID: 29254 RVA: 0x0018E183 File Offset: 0x0018C383
			public Hole _LFun1 { get; private set; }

			// Token: 0x170014EF RID: 5359
			// (get) Token: 0x06007247 RID: 29255 RVA: 0x0018E18C File Offset: 0x0018C38C
			// (set) Token: 0x06007248 RID: 29256 RVA: 0x0018E194 File Offset: 0x0018C394
			public Hole _LetB0 { get; private set; }

			// Token: 0x170014F0 RID: 5360
			// (get) Token: 0x06007249 RID: 29257 RVA: 0x0018E19D File Offset: 0x0018C39D
			// (set) Token: 0x0600724A RID: 29258 RVA: 0x0018E1A5 File Offset: 0x0018C3A5
			public Hole _LFun2 { get; private set; }

			// Token: 0x170014F1 RID: 5361
			// (get) Token: 0x0600724B RID: 29259 RVA: 0x0018E1AE File Offset: 0x0018C3AE
			// (set) Token: 0x0600724C RID: 29260 RVA: 0x0018E1B6 File Offset: 0x0018C3B6
			public Hole _LFun3 { get; private set; }

			// Token: 0x170014F2 RID: 5362
			// (get) Token: 0x0600724D RID: 29261 RVA: 0x0018E1BF File Offset: 0x0018C3BF
			// (set) Token: 0x0600724E RID: 29262 RVA: 0x0018E1C7 File Offset: 0x0018C3C7
			public Hole _LFun4 { get; private set; }

			// Token: 0x170014F3 RID: 5363
			// (get) Token: 0x0600724F RID: 29263 RVA: 0x0018E1D0 File Offset: 0x0018C3D0
			// (set) Token: 0x06007250 RID: 29264 RVA: 0x0018E1D8 File Offset: 0x0018C3D8
			public Hole _LFun5 { get; private set; }

			// Token: 0x170014F4 RID: 5364
			// (get) Token: 0x06007251 RID: 29265 RVA: 0x0018E1E1 File Offset: 0x0018C3E1
			// (set) Token: 0x06007252 RID: 29266 RVA: 0x0018E1E9 File Offset: 0x0018C3E9
			public Hole _LFun6 { get; private set; }

			// Token: 0x170014F5 RID: 5365
			// (get) Token: 0x06007253 RID: 29267 RVA: 0x0018E1F2 File Offset: 0x0018C3F2
			// (set) Token: 0x06007254 RID: 29268 RVA: 0x0018E1FA File Offset: 0x0018C3FA
			public Hole _LFun7 { get; private set; }

			// Token: 0x170014F6 RID: 5366
			// (get) Token: 0x06007255 RID: 29269 RVA: 0x0018E203 File Offset: 0x0018C403
			// (set) Token: 0x06007256 RID: 29270 RVA: 0x0018E20B File Offset: 0x0018C40B
			public Hole _LFun8 { get; private set; }

			// Token: 0x06007257 RID: 29271 RVA: 0x0018E214 File Offset: 0x0018C414
			public GrammarHoles(GrammarBuilders builders)
			{
				this.resultSequence = new Hole(builders.Symbol.resultSequence, null);
				this.resultRegion = new Hole(builders.Symbol.resultRegion, null);
				this.subNodeSequence = new Hole(builders.Symbol.subNodeSequence, null);
				this.node = new Hole(builders.Symbol.node, null);
				this.subNode = new Hole(builders.Symbol.subNode, null);
				this.mapNodeInSequence = new Hole(builders.Symbol.mapNodeInSequence, null);
				this.regionSequence = new Hole(builders.Symbol.regionSequence, null);
				this.regionStart = new Hole(builders.Symbol.regionStart, null);
				this.region = new Hole(builders.Symbol.region, null);
				this.mapRegionInSequence = new Hole(builders.Symbol.mapRegionInSequence, null);
				this.beginNode = new Hole(builders.Symbol.beginNode, null);
				this.endNode = new Hole(builders.Symbol.endNode, null);
				this.selection = new Hole(builders.Symbol.selection, null);
				this.filterSelection = new Hole(builders.Symbol.filterSelection, null);
				this.selectionEnd = new Hole(builders.Symbol.selectionEnd, null);
				this.regionStartSiblings = new Hole(builders.Symbol.regionStartSiblings, null);
				this.selection2 = new Hole(builders.Symbol.selection2, null);
				this.selection3 = new Hole(builders.Symbol.selection3, null);
				this.filterSelection2 = new Hole(builders.Symbol.filterSelection2, null);
				this.selection4 = new Hole(builders.Symbol.selection4, null);
				this.selection5 = new Hole(builders.Symbol.selection5, null);
				this.filterSelection3 = new Hole(builders.Symbol.filterSelection3, null);
				this.selection6 = new Hole(builders.Symbol.selection6, null);
				this.selection7 = new Hole(builders.Symbol.selection7, null);
				this.filterSelection4 = new Hole(builders.Symbol.filterSelection4, null);
				this.selection8 = new Hole(builders.Symbol.selection8, null);
				this.selection9 = new Hole(builders.Symbol.selection9, null);
				this.filterSelection5 = new Hole(builders.Symbol.filterSelection5, null);
				this.selection10 = new Hole(builders.Symbol.selection10, null);
				this.leafFExpr = new Hole(builders.Symbol.leafFExpr, null);
				this.leafAtom = new Hole(builders.Symbol.leafAtom, null);
				this.atomExpr = new Hole(builders.Symbol.atomExpr, null);
				this.literalExpr = new Hole(builders.Symbol.literalExpr, null);
				this.fexpr = new Hole(builders.Symbol.fexpr, null);
				this.resultFields = new Hole(builders.Symbol.resultFields, null);
				this.singletonField = new Hole(builders.Symbol.singletonField, null);
				this.fieldSubstring = new Hole(builders.Symbol.fieldSubstring, null);
				this.cs = new Hole(builders.Symbol.cs, null);
				this.y = new Hole(builders.Symbol.y, null);
				this.selectSubstring = new Hole(builders.Symbol.selectSubstring, null);
				this.substringDisj = new Hole(builders.Symbol.substringDisj, null);
				this.substring = new Hole(builders.Symbol.substring, null);
				this.resultTable = new Hole(builders.Symbol.resultTable, null);
				this.columnSelectors = new Hole(builders.Symbol.columnSelectors, null);
				this.name = new Hole(builders.Symbol.name, null);
				this.value = new Hole(builders.Symbol.value, null);
				this.cssSelector = new Hole(builders.Symbol.cssSelector, null);
				this.className = new Hole(builders.Symbol.className, null);
				this.idName = new Hole(builders.Symbol.idName, null);
				this.nodeName = new Hole(builders.Symbol.nodeName, null);
				this.propName = new Hole(builders.Symbol.propName, null);
				this.idx1 = new Hole(builders.Symbol.idx1, null);
				this.idx2 = new Hole(builders.Symbol.idx2, null);
				this.names = new Hole(builders.Symbol.names, null);
				this.count = new Hole(builders.Symbol.count, null);
				this.substringFeatureNames = new Hole(builders.Symbol.substringFeatureNames, null);
				this.substringFeatureValues = new Hole(builders.Symbol.substringFeatureValues, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.entityObjs = new Hole(builders.Symbol.entityObjs, null);
				this.direction = new Hole(builders.Symbol.direction, null);
				this.allNodes = new Hole(builders.Symbol.allNodes, null);
				this.nodeCollection = new Hole(builders.Symbol.nodeCollection, null);
				this.gen_NthChild = new Hole(builders.Symbol.gen_NthChild, null);
				this.gen_NthLastChild = new Hole(builders.Symbol.gen_NthLastChild, null);
				this.gen_Class = new Hole(builders.Symbol.gen_Class, null);
				this.gen_ID = new Hole(builders.Symbol.gen_ID, null);
				this.gen_NodeName = new Hole(builders.Symbol.gen_NodeName, null);
				this.gen_ItemProp = new Hole(builders.Symbol.gen_ItemProp, null);
				this.obj = new Hole(builders.Symbol.obj, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
				this._LFun1 = new Hole(builders.Symbol._LFun1, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LFun2 = new Hole(builders.Symbol._LFun2, null);
				this._LFun3 = new Hole(builders.Symbol._LFun3, null);
				this._LFun4 = new Hole(builders.Symbol._LFun4, null);
				this._LFun5 = new Hole(builders.Symbol._LFun5, null);
				this._LFun6 = new Hole(builders.Symbol._LFun6, null);
				this._LFun7 = new Hole(builders.Symbol._LFun7, null);
				this._LFun8 = new Hole(builders.Symbol._LFun8, null);
			}
		}

		// Token: 0x02000FE5 RID: 4069
		public class Nodes
		{
			// Token: 0x06007258 RID: 29272 RVA: 0x0018E940 File Offset: 0x0018CB40
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

			// Token: 0x170014F7 RID: 5367
			// (get) Token: 0x06007259 RID: 29273 RVA: 0x0018EA23 File Offset: 0x0018CC23
			// (set) Token: 0x0600725A RID: 29274 RVA: 0x0018EA2B File Offset: 0x0018CC2B
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x170014F8 RID: 5368
			// (get) Token: 0x0600725B RID: 29275 RVA: 0x0018EA34 File Offset: 0x0018CC34
			// (set) Token: 0x0600725C RID: 29276 RVA: 0x0018EA3C File Offset: 0x0018CC3C
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x170014F9 RID: 5369
			// (get) Token: 0x0600725D RID: 29277 RVA: 0x0018EA45 File Offset: 0x0018CC45
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x170014FA RID: 5370
			// (get) Token: 0x0600725E RID: 29278 RVA: 0x0018EA52 File Offset: 0x0018CC52
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x170014FB RID: 5371
			// (get) Token: 0x0600725F RID: 29279 RVA: 0x0018EA5F File Offset: 0x0018CC5F
			// (set) Token: 0x06007260 RID: 29280 RVA: 0x0018EA67 File Offset: 0x0018CC67
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x170014FC RID: 5372
			// (get) Token: 0x06007261 RID: 29281 RVA: 0x0018EA70 File Offset: 0x0018CC70
			// (set) Token: 0x06007262 RID: 29282 RVA: 0x0018EA78 File Offset: 0x0018CC78
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x170014FD RID: 5373
			// (get) Token: 0x06007263 RID: 29283 RVA: 0x0018EA81 File Offset: 0x0018CC81
			// (set) Token: 0x06007264 RID: 29284 RVA: 0x0018EA89 File Offset: 0x0018CC89
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x170014FE RID: 5374
			// (get) Token: 0x06007265 RID: 29285 RVA: 0x0018EA92 File Offset: 0x0018CC92
			// (set) Token: 0x06007266 RID: 29286 RVA: 0x0018EA9A File Offset: 0x0018CC9A
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x170014FF RID: 5375
			// (get) Token: 0x06007267 RID: 29287 RVA: 0x0018EAA3 File Offset: 0x0018CCA3
			// (set) Token: 0x06007268 RID: 29288 RVA: 0x0018EAAB File Offset: 0x0018CCAB
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17001500 RID: 5376
			// (get) Token: 0x06007269 RID: 29289 RVA: 0x0018EAB4 File Offset: 0x0018CCB4
			// (set) Token: 0x0600726A RID: 29290 RVA: 0x0018EABC File Offset: 0x0018CCBC
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17001501 RID: 5377
			// (get) Token: 0x0600726B RID: 29291 RVA: 0x0018EAC5 File Offset: 0x0018CCC5
			// (set) Token: 0x0600726C RID: 29292 RVA: 0x0018EACD File Offset: 0x0018CCCD
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04003218 RID: 12824
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04003219 RID: 12825
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000FE6 RID: 4070
			public class NodeRules
			{
				// Token: 0x0600726D RID: 29293 RVA: 0x0018EAD6 File Offset: 0x0018CCD6
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600726E RID: 29294 RVA: 0x0018EAE5 File Offset: 0x0018CCE5
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name name(string value)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name(this._builders, value);
				}

				// Token: 0x0600726F RID: 29295 RVA: 0x0018EAF3 File Offset: 0x0018CCF3
				public value value(string value)
				{
					return new value(this._builders, value);
				}

				// Token: 0x06007270 RID: 29296 RVA: 0x0018EB01 File Offset: 0x0018CD01
				public cssSelector cssSelector(string value)
				{
					return new cssSelector(this._builders, value);
				}

				// Token: 0x06007271 RID: 29297 RVA: 0x0018EB0F File Offset: 0x0018CD0F
				public className className(string value)
				{
					return new className(this._builders, value);
				}

				// Token: 0x06007272 RID: 29298 RVA: 0x0018EB1D File Offset: 0x0018CD1D
				public idName idName(string value)
				{
					return new idName(this._builders, value);
				}

				// Token: 0x06007273 RID: 29299 RVA: 0x0018EB2B File Offset: 0x0018CD2B
				public nodeName nodeName(string value)
				{
					return new nodeName(this._builders, value);
				}

				// Token: 0x06007274 RID: 29300 RVA: 0x0018EB39 File Offset: 0x0018CD39
				public propName propName(string value)
				{
					return new propName(this._builders, value);
				}

				// Token: 0x06007275 RID: 29301 RVA: 0x0018EB47 File Offset: 0x0018CD47
				public idx1 idx1(int value)
				{
					return new idx1(this._builders, value);
				}

				// Token: 0x06007276 RID: 29302 RVA: 0x0018EB55 File Offset: 0x0018CD55
				public idx2 idx2(int value)
				{
					return new idx2(this._builders, value);
				}

				// Token: 0x06007277 RID: 29303 RVA: 0x0018EB63 File Offset: 0x0018CD63
				public names names(string[] value)
				{
					return new names(this._builders, value);
				}

				// Token: 0x06007278 RID: 29304 RVA: 0x0018EB71 File Offset: 0x0018CD71
				public count count(int value)
				{
					return new count(this._builders, value);
				}

				// Token: 0x06007279 RID: 29305 RVA: 0x0018EB7F File Offset: 0x0018CD7F
				public substringFeatureNames substringFeatureNames(string[] value)
				{
					return new substringFeatureNames(this._builders, value);
				}

				// Token: 0x0600727A RID: 29306 RVA: 0x0018EB8D File Offset: 0x0018CD8D
				public substringFeatureValues substringFeatureValues(int[] value)
				{
					return new substringFeatureValues(this._builders, value);
				}

				// Token: 0x0600727B RID: 29307 RVA: 0x0018EB9B File Offset: 0x0018CD9B
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k k(int value)
				{
					return new Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k(this._builders, value);
				}

				// Token: 0x0600727C RID: 29308 RVA: 0x0018EBA9 File Offset: 0x0018CDA9
				public entityObjs entityObjs(EntityDetector[] value)
				{
					return new entityObjs(this._builders, value);
				}

				// Token: 0x0600727D RID: 29309 RVA: 0x0018EBB7 File Offset: 0x0018CDB7
				public direction direction(KeyDirections value)
				{
					return new direction(this._builders, value);
				}

				// Token: 0x0600727E RID: 29310 RVA: 0x0018EBC5 File Offset: 0x0018CDC5
				public obj obj(object value)
				{
					return new obj(this._builders, value);
				}

				// Token: 0x0600727F RID: 29311 RVA: 0x0018EBD3 File Offset: 0x0018CDD3
				public resultSequence ConvertToWebRegions(nodeCollection value0)
				{
					return new ConvertToWebRegions(this._builders, value0);
				}

				// Token: 0x06007280 RID: 29312 RVA: 0x0018EBE6 File Offset: 0x0018CDE6
				public resultSequence Union(resultSequence value0, resultSequence value1)
				{
					return new Union(this._builders, value0, value1);
				}

				// Token: 0x06007281 RID: 29313 RVA: 0x0018EBFA File Offset: 0x0018CDFA
				public resultSequence EmptySequence()
				{
					return new EmptySequence(this._builders);
				}

				// Token: 0x06007282 RID: 29314 RVA: 0x0018EC0C File Offset: 0x0018CE0C
				public subNode NodeToWebRegion(beginNode value0)
				{
					return new NodeToWebRegion(this._builders, value0);
				}

				// Token: 0x06007283 RID: 29315 RVA: 0x0018EC1F File Offset: 0x0018CE1F
				public mapNodeInSequence NodeToWebRegionInSequence(node value0)
				{
					return new NodeToWebRegionInSequence(this._builders, value0);
				}

				// Token: 0x06007284 RID: 29316 RVA: 0x0018EC32 File Offset: 0x0018CE32
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 NodeRegionToWebRegion(regionStart value0, endNode value1)
				{
					return new NodeRegionToWebRegion(this._builders, value0, value1);
				}

				// Token: 0x06007285 RID: 29317 RVA: 0x0018EC46 File Offset: 0x0018CE46
				public mapRegionInSequence NodeRegionToWebRegionInSequence(regionStart value0, endNode value1)
				{
					return new NodeRegionToWebRegionInSequence(this._builders, value0, value1);
				}

				// Token: 0x06007286 RID: 29318 RVA: 0x0018EC5A File Offset: 0x0018CE5A
				public selection SingleSelection1(filterSelection value0)
				{
					return new SingleSelection1(this._builders, value0);
				}

				// Token: 0x06007287 RID: 29319 RVA: 0x0018EC6D File Offset: 0x0018CE6D
				public selection DisjSelection1(selection value0, filterSelection value1)
				{
					return new DisjSelection1(this._builders, value0, value1);
				}

				// Token: 0x06007288 RID: 29320 RVA: 0x0018EC81 File Offset: 0x0018CE81
				public selection CSSSelection(cssSelector value0, allNodes value1)
				{
					return new CSSSelection(this._builders, value0, value1);
				}

				// Token: 0x06007289 RID: 29321 RVA: 0x0018EC95 File Offset: 0x0018CE95
				public regionStartSiblings YoungerSiblingsOf(regionStart value0)
				{
					return new YoungerSiblingsOf(this._builders, value0);
				}

				// Token: 0x0600728A RID: 29322 RVA: 0x0018ECA8 File Offset: 0x0018CEA8
				public selection2 LeafChildrenOf1(selection3 value0)
				{
					return new LeafChildrenOf1(this._builders, value0);
				}

				// Token: 0x0600728B RID: 29323 RVA: 0x0018ECBB File Offset: 0x0018CEBB
				public selection3 SingleSelection2(filterSelection2 value0)
				{
					return new SingleSelection2(this._builders, value0);
				}

				// Token: 0x0600728C RID: 29324 RVA: 0x0018ECCE File Offset: 0x0018CECE
				public selection3 DisjSelection2(selection3 value0, filterSelection2 value1)
				{
					return new DisjSelection2(this._builders, value0, value1);
				}

				// Token: 0x0600728D RID: 29325 RVA: 0x0018ECE2 File Offset: 0x0018CEE2
				public selection4 LeafChildrenOf2(selection5 value0)
				{
					return new LeafChildrenOf2(this._builders, value0);
				}

				// Token: 0x0600728E RID: 29326 RVA: 0x0018ECF5 File Offset: 0x0018CEF5
				public selection5 SingleSelection3(filterSelection3 value0)
				{
					return new SingleSelection3(this._builders, value0);
				}

				// Token: 0x0600728F RID: 29327 RVA: 0x0018ED08 File Offset: 0x0018CF08
				public selection5 DisjSelection3(selection5 value0, filterSelection3 value1)
				{
					return new DisjSelection3(this._builders, value0, value1);
				}

				// Token: 0x06007290 RID: 29328 RVA: 0x0018ED1C File Offset: 0x0018CF1C
				public selection6 LeafChildrenOf3(selection7 value0)
				{
					return new LeafChildrenOf3(this._builders, value0);
				}

				// Token: 0x06007291 RID: 29329 RVA: 0x0018ED2F File Offset: 0x0018CF2F
				public selection7 SingleSelection4(filterSelection4 value0)
				{
					return new SingleSelection4(this._builders, value0);
				}

				// Token: 0x06007292 RID: 29330 RVA: 0x0018ED42 File Offset: 0x0018CF42
				public selection7 DisjSelection4(selection7 value0, filterSelection4 value1)
				{
					return new DisjSelection4(this._builders, value0, value1);
				}

				// Token: 0x06007293 RID: 29331 RVA: 0x0018ED56 File Offset: 0x0018CF56
				public selection8 LeafChildrenOf4(selection9 value0)
				{
					return new LeafChildrenOf4(this._builders, value0);
				}

				// Token: 0x06007294 RID: 29332 RVA: 0x0018ED69 File Offset: 0x0018CF69
				public selection9 SingleSelection5(filterSelection5 value0)
				{
					return new SingleSelection5(this._builders, value0);
				}

				// Token: 0x06007295 RID: 29333 RVA: 0x0018ED7C File Offset: 0x0018CF7C
				public selection9 DisjSelection5(selection9 value0, filterSelection5 value1)
				{
					return new DisjSelection5(this._builders, value0, value1);
				}

				// Token: 0x06007296 RID: 29334 RVA: 0x0018ED90 File Offset: 0x0018CF90
				public atomExpr ContainsDate(node value0)
				{
					return new ContainsDate(this._builders, value0);
				}

				// Token: 0x06007297 RID: 29335 RVA: 0x0018EDA3 File Offset: 0x0018CFA3
				public atomExpr ContainsNum(node value0)
				{
					return new ContainsNum(this._builders, value0);
				}

				// Token: 0x06007298 RID: 29336 RVA: 0x0018EDB6 File Offset: 0x0018CFB6
				public atomExpr ID_substring(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, node value1)
				{
					return new ID_substring(this._builders, value0, value1);
				}

				// Token: 0x06007299 RID: 29337 RVA: 0x0018EDCA File Offset: 0x0018CFCA
				public atomExpr Class(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, node value1)
				{
					return new Class(this._builders, value0, value1);
				}

				// Token: 0x0600729A RID: 29338 RVA: 0x0018EDDE File Offset: 0x0018CFDE
				public atomExpr TitleIs(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, node value1)
				{
					return new TitleIs(this._builders, value0, value1);
				}

				// Token: 0x0600729B RID: 29339 RVA: 0x0018EDF2 File Offset: 0x0018CFF2
				public atomExpr NodeName(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, node value1)
				{
					return new NodeName(this._builders, value0, value1);
				}

				// Token: 0x0600729C RID: 29340 RVA: 0x0018EE06 File Offset: 0x0018D006
				public atomExpr NodeNames(names value0, node value1)
				{
					return new NodeNames(this._builders, value0, value1);
				}

				// Token: 0x0600729D RID: 29341 RVA: 0x0018EE1A File Offset: 0x0018D01A
				public atomExpr NthChild(idx1 value0, node value1)
				{
					return new NthChild(this._builders, value0, value1);
				}

				// Token: 0x0600729E RID: 29342 RVA: 0x0018EE2E File Offset: 0x0018D02E
				public atomExpr NthLastChild(idx2 value0, node value1)
				{
					return new NthLastChild(this._builders, value0, value1);
				}

				// Token: 0x0600729F RID: 29343 RVA: 0x0018EE42 File Offset: 0x0018D042
				public atomExpr ContainsLeafNodes(names value0, node value1)
				{
					return new ContainsLeafNodes(this._builders, value0, value1);
				}

				// Token: 0x060072A0 RID: 29344 RVA: 0x0018EE56 File Offset: 0x0018D056
				public atomExpr ChildrenCount(count value0, node value1)
				{
					return new ChildrenCount(this._builders, value0, value1);
				}

				// Token: 0x060072A1 RID: 29345 RVA: 0x0018EE6A File Offset: 0x0018D06A
				public atomExpr HasAttribute(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, value value1, node value2)
				{
					return new HasAttribute(this._builders, value0, value1, value2);
				}

				// Token: 0x060072A2 RID: 29346 RVA: 0x0018EE7F File Offset: 0x0018D07F
				public atomExpr HasStyle(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value0, value value1, node value2)
				{
					return new HasStyle(this._builders, value0, value1, value2);
				}

				// Token: 0x060072A3 RID: 29347 RVA: 0x0018EE94 File Offset: 0x0018D094
				public atomExpr HasEntityAnchor(entityObjs value0, direction value1, node value2)
				{
					return new HasEntityAnchor(this._builders, value0, value1, value2);
				}

				// Token: 0x060072A4 RID: 29348 RVA: 0x0018EEA9 File Offset: 0x0018D0A9
				public resultFields AppendField(resultFields value0, singletonField value1)
				{
					return new AppendField(this._builders, value0, value1);
				}

				// Token: 0x060072A5 RID: 29349 RVA: 0x0018EEBD File Offset: 0x0018D0BD
				public singletonField TrimmedTextField(resultRegion value0)
				{
					return new TrimmedTextField(this._builders, value0);
				}

				// Token: 0x060072A6 RID: 29350 RVA: 0x0018EED0 File Offset: 0x0018D0D0
				public singletonField SubstringField(fieldSubstring value0)
				{
					return new SubstringField(this._builders, value0);
				}

				// Token: 0x060072A7 RID: 29351 RVA: 0x0018EEE3 File Offset: 0x0018D0E3
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y GetValueSubstring(resultRegion value0)
				{
					return new GetValueSubstring(this._builders, value0);
				}

				// Token: 0x060072A8 RID: 29352 RVA: 0x0018EEF6 File Offset: 0x0018D0F6
				public selectSubstring SelectSubstring(substringDisj value0, substringFeatureNames value1, substringFeatureValues value2)
				{
					return new SelectSubstring(this._builders, value0, value1, value2);
				}

				// Token: 0x060072A9 RID: 29353 RVA: 0x0018EF0B File Offset: 0x0018D10B
				public substringDisj SingleSubstring(substring value0)
				{
					return new SingleSubstring(this._builders, value0);
				}

				// Token: 0x060072AA RID: 29354 RVA: 0x0018EF1E File Offset: 0x0018D11E
				public substringDisj DisjSubstring(substringDisj value0, substring value1)
				{
					return new DisjSubstring(this._builders, value0, value1);
				}

				// Token: 0x060072AB RID: 29355 RVA: 0x0018EF32 File Offset: 0x0018D132
				public resultTable ExtractTable(columnSelectors value0)
				{
					return new ExtractTable(this._builders, value0);
				}

				// Token: 0x060072AC RID: 29356 RVA: 0x0018EF45 File Offset: 0x0018D145
				public resultTable ExtractRowBasedTable(columnSelectors value0, resultSequence value1)
				{
					return new ExtractRowBasedTable(this._builders, value0, value1);
				}

				// Token: 0x060072AD RID: 29357 RVA: 0x0018EF59 File Offset: 0x0018D159
				public columnSelectors SingleColumn(resultSequence value0)
				{
					return new SingleColumn(this._builders, value0);
				}

				// Token: 0x060072AE RID: 29358 RVA: 0x0018EF6C File Offset: 0x0018D16C
				public columnSelectors ColumnSequence(columnSelectors value0, resultSequence value1)
				{
					return new ColumnSequence(this._builders, value0, value1);
				}

				// Token: 0x060072AF RID: 29359 RVA: 0x0018EF80 File Offset: 0x0018D180
				public nodeCollection AsCollection(allNodes value0)
				{
					return new AsCollection(this._builders, value0);
				}

				// Token: 0x060072B0 RID: 29360 RVA: 0x0018EF93 File Offset: 0x0018D193
				public nodeCollection DescendantsOf(nodeCollection value0)
				{
					return new DescendantsOf(this._builders, value0);
				}

				// Token: 0x060072B1 RID: 29361 RVA: 0x0018EFA6 File Offset: 0x0018D1A6
				public nodeCollection RightSiblingOf(nodeCollection value0)
				{
					return new RightSiblingOf(this._builders, value0);
				}

				// Token: 0x060072B2 RID: 29362 RVA: 0x0018EFB9 File Offset: 0x0018D1B9
				public nodeCollection ClassFilter(className value0, nodeCollection value1)
				{
					return new ClassFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B3 RID: 29363 RVA: 0x0018EFCD File Offset: 0x0018D1CD
				public nodeCollection IDFilter(idName value0, nodeCollection value1)
				{
					return new IDFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B4 RID: 29364 RVA: 0x0018EFE1 File Offset: 0x0018D1E1
				public nodeCollection NodeNameFilter(nodeName value0, nodeCollection value1)
				{
					return new NodeNameFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B5 RID: 29365 RVA: 0x0018EFF5 File Offset: 0x0018D1F5
				public nodeCollection ItemPropFilter(propName value0, nodeCollection value1)
				{
					return new ItemPropFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B6 RID: 29366 RVA: 0x0018F009 File Offset: 0x0018D209
				public nodeCollection NthChildFilter(idx1 value0, nodeCollection value1)
				{
					return new NthChildFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B7 RID: 29367 RVA: 0x0018F01D File Offset: 0x0018D21D
				public nodeCollection NthLastChildFilter(idx2 value0, nodeCollection value1)
				{
					return new NthLastChildFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B8 RID: 29368 RVA: 0x0018F031 File Offset: 0x0018D231
				public gen_NthChild GEN_NthChildFilter(obj value0, obj value1)
				{
					return new GEN_NthChildFilter(this._builders, value0, value1);
				}

				// Token: 0x060072B9 RID: 29369 RVA: 0x0018F045 File Offset: 0x0018D245
				public gen_NthLastChild GEN_NthLastChildFilter(obj value0, obj value1)
				{
					return new GEN_NthLastChildFilter(this._builders, value0, value1);
				}

				// Token: 0x060072BA RID: 29370 RVA: 0x0018F059 File Offset: 0x0018D259
				public gen_Class GEN_ClassFilter(obj value0, obj value1)
				{
					return new GEN_ClassFilter(this._builders, value0, value1);
				}

				// Token: 0x060072BB RID: 29371 RVA: 0x0018F06D File Offset: 0x0018D26D
				public gen_ID GEN_IDFilter(obj value0, obj value1)
				{
					return new GEN_IDFilter(this._builders, value0, value1);
				}

				// Token: 0x060072BC RID: 29372 RVA: 0x0018F081 File Offset: 0x0018D281
				public gen_NodeName GEN_NodeNameFilter(obj value0, obj value1)
				{
					return new GEN_NodeNameFilter(this._builders, value0, value1);
				}

				// Token: 0x060072BD RID: 29373 RVA: 0x0018F095 File Offset: 0x0018D295
				public gen_ItemProp GEN_ItemPropFilter(obj value0, obj value1)
				{
					return new GEN_ItemPropFilter(this._builders, value0, value1);
				}

				// Token: 0x060072BE RID: 29374 RVA: 0x0018F0A9 File Offset: 0x0018D2A9
				public subNodeSequence MapToWebRegion(mapNodeInSequence value0, selection value1)
				{
					return new MapToWebRegion(this._builders, value0, value1);
				}

				// Token: 0x060072BF RID: 29375 RVA: 0x0018F0BD File Offset: 0x0018D2BD
				public regionSequence FindEndNode(mapRegionInSequence value0, selection value1)
				{
					return new FindEndNode(this._builders, value0, value1);
				}

				// Token: 0x060072C0 RID: 29376 RVA: 0x0018F0D1 File Offset: 0x0018D2D1
				public beginNode KthNodeInSelection(selection value0, Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k value1)
				{
					return new KthNodeInSelection(this._builders, value0, value1);
				}

				// Token: 0x060072C1 RID: 29377 RVA: 0x0018F0E5 File Offset: 0x0018D2E5
				public endNode KthNode(selectionEnd value0, Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k value1)
				{
					return new KthNode(this._builders, value0, value1);
				}

				// Token: 0x060072C2 RID: 29378 RVA: 0x0018F0F9 File Offset: 0x0018D2F9
				public filterSelection LeafFilter1(leafFExpr value0, selection2 value1)
				{
					return new LeafFilter1(this._builders, value0, value1);
				}

				// Token: 0x060072C3 RID: 29379 RVA: 0x0018F10D File Offset: 0x0018D30D
				public selectionEnd FilterNodesEnd(leafFExpr value0, regionStartSiblings value1)
				{
					return new FilterNodesEnd(this._builders, value0, value1);
				}

				// Token: 0x060072C4 RID: 29380 RVA: 0x0018F121 File Offset: 0x0018D321
				public selectionEnd TakeWhileNodesEnd(leafFExpr value0, regionStartSiblings value1)
				{
					return new TakeWhileNodesEnd(this._builders, value0, value1);
				}

				// Token: 0x060072C5 RID: 29381 RVA: 0x0018F135 File Offset: 0x0018D335
				public filterSelection2 LeafFilter2(leafFExpr value0, selection4 value1)
				{
					return new LeafFilter2(this._builders, value0, value1);
				}

				// Token: 0x060072C6 RID: 29382 RVA: 0x0018F149 File Offset: 0x0018D349
				public filterSelection3 LeafFilter3(leafFExpr value0, selection6 value1)
				{
					return new LeafFilter3(this._builders, value0, value1);
				}

				// Token: 0x060072C7 RID: 29383 RVA: 0x0018F15D File Offset: 0x0018D35D
				public filterSelection4 LeafFilter4(leafFExpr value0, selection8 value1)
				{
					return new LeafFilter4(this._builders, value0, value1);
				}

				// Token: 0x060072C8 RID: 29384 RVA: 0x0018F171 File Offset: 0x0018D371
				public filterSelection5 LeafFilter5(leafFExpr value0, selection10 value1)
				{
					return new LeafFilter5(this._builders, value0, value1);
				}

				// Token: 0x060072C9 RID: 29385 RVA: 0x0018F185 File Offset: 0x0018D385
				public leafFExpr LeafAnd(leafFExpr value0, leafAtom value1)
				{
					return new LeafAnd(this._builders, value0, value1);
				}

				// Token: 0x060072CA RID: 29386 RVA: 0x0018F199 File Offset: 0x0018D399
				public fexpr And(fexpr value0, literalExpr value1)
				{
					return new And(this._builders, value0, value1);
				}

				// Token: 0x060072CB RID: 29387 RVA: 0x0018F1AD File Offset: 0x0018D3AD
				public substring Substring(SS value0)
				{
					return new Substring(this._builders, value0);
				}

				// Token: 0x060072CC RID: 29388 RVA: 0x0018F1C0 File Offset: 0x0018D3C0
				public region LetRegion(beginNode value0, Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 value1)
				{
					return new LetRegion(this._builders, value0, value1);
				}

				// Token: 0x060072CD RID: 29389 RVA: 0x0018F1D4 File Offset: 0x0018D3D4
				public fieldSubstring LetSubstring(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y value0, selectSubstring value1)
				{
					return new LetSubstring(this._builders, value0, value1);
				}

				// Token: 0x04003221 RID: 12833
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FE7 RID: 4071
			public class NodeUnnamedConversionRules
			{
				// Token: 0x060072CE RID: 29390 RVA: 0x0018F1E8 File Offset: 0x0018D3E8
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060072CF RID: 29391 RVA: 0x0018F1F7 File Offset: 0x0018D3F7
				public resultSequence resultSequence_subNodeSequence(subNodeSequence value0)
				{
					return new resultSequence_subNodeSequence(this._builders, value0);
				}

				// Token: 0x060072D0 RID: 29392 RVA: 0x0018F20A File Offset: 0x0018D40A
				public resultSequence resultSequence_regionSequence(regionSequence value0)
				{
					return new resultSequence_regionSequence(this._builders, value0);
				}

				// Token: 0x060072D1 RID: 29393 RVA: 0x0018F21D File Offset: 0x0018D41D
				public resultRegion resultRegion_subNode(subNode value0)
				{
					return new resultRegion_subNode(this._builders, value0);
				}

				// Token: 0x060072D2 RID: 29394 RVA: 0x0018F230 File Offset: 0x0018D430
				public resultRegion resultRegion_region(region value0)
				{
					return new resultRegion_region(this._builders, value0);
				}

				// Token: 0x060072D3 RID: 29395 RVA: 0x0018F243 File Offset: 0x0018D443
				public selectionEnd selectionEnd_regionStartSiblings(regionStartSiblings value0)
				{
					return new selectionEnd_regionStartSiblings(this._builders, value0);
				}

				// Token: 0x060072D4 RID: 29396 RVA: 0x0018F256 File Offset: 0x0018D456
				public selection2 selection2_allNodes(allNodes value0)
				{
					return new selection2_allNodes(this._builders, value0);
				}

				// Token: 0x060072D5 RID: 29397 RVA: 0x0018F269 File Offset: 0x0018D469
				public selection4 selection4_allNodes(allNodes value0)
				{
					return new selection4_allNodes(this._builders, value0);
				}

				// Token: 0x060072D6 RID: 29398 RVA: 0x0018F27C File Offset: 0x0018D47C
				public selection6 selection6_allNodes(allNodes value0)
				{
					return new selection6_allNodes(this._builders, value0);
				}

				// Token: 0x060072D7 RID: 29399 RVA: 0x0018F28F File Offset: 0x0018D48F
				public selection8 selection8_allNodes(allNodes value0)
				{
					return new selection8_allNodes(this._builders, value0);
				}

				// Token: 0x060072D8 RID: 29400 RVA: 0x0018F2A2 File Offset: 0x0018D4A2
				public selection10 selection10_allNodes(allNodes value0)
				{
					return new selection10_allNodes(this._builders, value0);
				}

				// Token: 0x060072D9 RID: 29401 RVA: 0x0018F2B5 File Offset: 0x0018D4B5
				public leafFExpr leafFExpr_leafAtom(leafAtom value0)
				{
					return new leafFExpr_leafAtom(this._builders, value0);
				}

				// Token: 0x060072DA RID: 29402 RVA: 0x0018F2C8 File Offset: 0x0018D4C8
				public leafAtom leafAtom_literalExpr(literalExpr value0)
				{
					return new leafAtom_literalExpr(this._builders, value0);
				}

				// Token: 0x060072DB RID: 29403 RVA: 0x0018F2DB File Offset: 0x0018D4DB
				public literalExpr literalExpr_atomExpr(atomExpr value0)
				{
					return new literalExpr_atomExpr(this._builders, value0);
				}

				// Token: 0x060072DC RID: 29404 RVA: 0x0018F2EE File Offset: 0x0018D4EE
				public fexpr fexpr_literalExpr(literalExpr value0)
				{
					return new fexpr_literalExpr(this._builders, value0);
				}

				// Token: 0x060072DD RID: 29405 RVA: 0x0018F301 File Offset: 0x0018D501
				public resultFields resultFields_singletonField(singletonField value0)
				{
					return new resultFields_singletonField(this._builders, value0);
				}

				// Token: 0x04003222 RID: 12834
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FE8 RID: 4072
			public class NodeVariables
			{
				// Token: 0x17001502 RID: 5378
				// (get) Token: 0x060072DE RID: 29406 RVA: 0x0018F314 File Offset: 0x0018D514
				// (set) Token: 0x060072DF RID: 29407 RVA: 0x0018F31C File Offset: 0x0018D51C
				public node node { get; private set; }

				// Token: 0x17001503 RID: 5379
				// (get) Token: 0x060072E0 RID: 29408 RVA: 0x0018F325 File Offset: 0x0018D525
				// (set) Token: 0x060072E1 RID: 29409 RVA: 0x0018F32D File Offset: 0x0018D52D
				public regionStart regionStart { get; private set; }

				// Token: 0x17001504 RID: 5380
				// (get) Token: 0x060072E2 RID: 29410 RVA: 0x0018F336 File Offset: 0x0018D536
				// (set) Token: 0x060072E3 RID: 29411 RVA: 0x0018F33E File Offset: 0x0018D53E
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs cs { get; private set; }

				// Token: 0x17001505 RID: 5381
				// (get) Token: 0x060072E4 RID: 29412 RVA: 0x0018F347 File Offset: 0x0018D547
				// (set) Token: 0x060072E5 RID: 29413 RVA: 0x0018F34F File Offset: 0x0018D54F
				public allNodes allNodes { get; private set; }

				// Token: 0x060072E6 RID: 29414 RVA: 0x0018F358 File Offset: 0x0018D558
				public NodeVariables(GrammarBuilders builders)
				{
					this.node = new node(builders);
					this.regionStart = new regionStart(builders);
					this.cs = new Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs(builders);
					this.allNodes = new allNodes(builders);
				}
			}

			// Token: 0x02000FE9 RID: 4073
			public class NodeHoles
			{
				// Token: 0x17001506 RID: 5382
				// (get) Token: 0x060072E7 RID: 29415 RVA: 0x0018F390 File Offset: 0x0018D590
				// (set) Token: 0x060072E8 RID: 29416 RVA: 0x0018F398 File Offset: 0x0018D598
				public resultSequence resultSequence { get; private set; }

				// Token: 0x17001507 RID: 5383
				// (get) Token: 0x060072E9 RID: 29417 RVA: 0x0018F3A1 File Offset: 0x0018D5A1
				// (set) Token: 0x060072EA RID: 29418 RVA: 0x0018F3A9 File Offset: 0x0018D5A9
				public resultRegion resultRegion { get; private set; }

				// Token: 0x17001508 RID: 5384
				// (get) Token: 0x060072EB RID: 29419 RVA: 0x0018F3B2 File Offset: 0x0018D5B2
				// (set) Token: 0x060072EC RID: 29420 RVA: 0x0018F3BA File Offset: 0x0018D5BA
				public subNodeSequence subNodeSequence { get; private set; }

				// Token: 0x17001509 RID: 5385
				// (get) Token: 0x060072ED RID: 29421 RVA: 0x0018F3C3 File Offset: 0x0018D5C3
				// (set) Token: 0x060072EE RID: 29422 RVA: 0x0018F3CB File Offset: 0x0018D5CB
				public node node { get; private set; }

				// Token: 0x1700150A RID: 5386
				// (get) Token: 0x060072EF RID: 29423 RVA: 0x0018F3D4 File Offset: 0x0018D5D4
				// (set) Token: 0x060072F0 RID: 29424 RVA: 0x0018F3DC File Offset: 0x0018D5DC
				public subNode subNode { get; private set; }

				// Token: 0x1700150B RID: 5387
				// (get) Token: 0x060072F1 RID: 29425 RVA: 0x0018F3E5 File Offset: 0x0018D5E5
				// (set) Token: 0x060072F2 RID: 29426 RVA: 0x0018F3ED File Offset: 0x0018D5ED
				public mapNodeInSequence mapNodeInSequence { get; private set; }

				// Token: 0x1700150C RID: 5388
				// (get) Token: 0x060072F3 RID: 29427 RVA: 0x0018F3F6 File Offset: 0x0018D5F6
				// (set) Token: 0x060072F4 RID: 29428 RVA: 0x0018F3FE File Offset: 0x0018D5FE
				public regionSequence regionSequence { get; private set; }

				// Token: 0x1700150D RID: 5389
				// (get) Token: 0x060072F5 RID: 29429 RVA: 0x0018F407 File Offset: 0x0018D607
				// (set) Token: 0x060072F6 RID: 29430 RVA: 0x0018F40F File Offset: 0x0018D60F
				public regionStart regionStart { get; private set; }

				// Token: 0x1700150E RID: 5390
				// (get) Token: 0x060072F7 RID: 29431 RVA: 0x0018F418 File Offset: 0x0018D618
				// (set) Token: 0x060072F8 RID: 29432 RVA: 0x0018F420 File Offset: 0x0018D620
				public region region { get; private set; }

				// Token: 0x1700150F RID: 5391
				// (get) Token: 0x060072F9 RID: 29433 RVA: 0x0018F429 File Offset: 0x0018D629
				// (set) Token: 0x060072FA RID: 29434 RVA: 0x0018F431 File Offset: 0x0018D631
				public mapRegionInSequence mapRegionInSequence { get; private set; }

				// Token: 0x17001510 RID: 5392
				// (get) Token: 0x060072FB RID: 29435 RVA: 0x0018F43A File Offset: 0x0018D63A
				// (set) Token: 0x060072FC RID: 29436 RVA: 0x0018F442 File Offset: 0x0018D642
				public beginNode beginNode { get; private set; }

				// Token: 0x17001511 RID: 5393
				// (get) Token: 0x060072FD RID: 29437 RVA: 0x0018F44B File Offset: 0x0018D64B
				// (set) Token: 0x060072FE RID: 29438 RVA: 0x0018F453 File Offset: 0x0018D653
				public endNode endNode { get; private set; }

				// Token: 0x17001512 RID: 5394
				// (get) Token: 0x060072FF RID: 29439 RVA: 0x0018F45C File Offset: 0x0018D65C
				// (set) Token: 0x06007300 RID: 29440 RVA: 0x0018F464 File Offset: 0x0018D664
				public selection selection { get; private set; }

				// Token: 0x17001513 RID: 5395
				// (get) Token: 0x06007301 RID: 29441 RVA: 0x0018F46D File Offset: 0x0018D66D
				// (set) Token: 0x06007302 RID: 29442 RVA: 0x0018F475 File Offset: 0x0018D675
				public filterSelection filterSelection { get; private set; }

				// Token: 0x17001514 RID: 5396
				// (get) Token: 0x06007303 RID: 29443 RVA: 0x0018F47E File Offset: 0x0018D67E
				// (set) Token: 0x06007304 RID: 29444 RVA: 0x0018F486 File Offset: 0x0018D686
				public selectionEnd selectionEnd { get; private set; }

				// Token: 0x17001515 RID: 5397
				// (get) Token: 0x06007305 RID: 29445 RVA: 0x0018F48F File Offset: 0x0018D68F
				// (set) Token: 0x06007306 RID: 29446 RVA: 0x0018F497 File Offset: 0x0018D697
				public regionStartSiblings regionStartSiblings { get; private set; }

				// Token: 0x17001516 RID: 5398
				// (get) Token: 0x06007307 RID: 29447 RVA: 0x0018F4A0 File Offset: 0x0018D6A0
				// (set) Token: 0x06007308 RID: 29448 RVA: 0x0018F4A8 File Offset: 0x0018D6A8
				public selection2 selection2 { get; private set; }

				// Token: 0x17001517 RID: 5399
				// (get) Token: 0x06007309 RID: 29449 RVA: 0x0018F4B1 File Offset: 0x0018D6B1
				// (set) Token: 0x0600730A RID: 29450 RVA: 0x0018F4B9 File Offset: 0x0018D6B9
				public selection3 selection3 { get; private set; }

				// Token: 0x17001518 RID: 5400
				// (get) Token: 0x0600730B RID: 29451 RVA: 0x0018F4C2 File Offset: 0x0018D6C2
				// (set) Token: 0x0600730C RID: 29452 RVA: 0x0018F4CA File Offset: 0x0018D6CA
				public filterSelection2 filterSelection2 { get; private set; }

				// Token: 0x17001519 RID: 5401
				// (get) Token: 0x0600730D RID: 29453 RVA: 0x0018F4D3 File Offset: 0x0018D6D3
				// (set) Token: 0x0600730E RID: 29454 RVA: 0x0018F4DB File Offset: 0x0018D6DB
				public selection4 selection4 { get; private set; }

				// Token: 0x1700151A RID: 5402
				// (get) Token: 0x0600730F RID: 29455 RVA: 0x0018F4E4 File Offset: 0x0018D6E4
				// (set) Token: 0x06007310 RID: 29456 RVA: 0x0018F4EC File Offset: 0x0018D6EC
				public selection5 selection5 { get; private set; }

				// Token: 0x1700151B RID: 5403
				// (get) Token: 0x06007311 RID: 29457 RVA: 0x0018F4F5 File Offset: 0x0018D6F5
				// (set) Token: 0x06007312 RID: 29458 RVA: 0x0018F4FD File Offset: 0x0018D6FD
				public filterSelection3 filterSelection3 { get; private set; }

				// Token: 0x1700151C RID: 5404
				// (get) Token: 0x06007313 RID: 29459 RVA: 0x0018F506 File Offset: 0x0018D706
				// (set) Token: 0x06007314 RID: 29460 RVA: 0x0018F50E File Offset: 0x0018D70E
				public selection6 selection6 { get; private set; }

				// Token: 0x1700151D RID: 5405
				// (get) Token: 0x06007315 RID: 29461 RVA: 0x0018F517 File Offset: 0x0018D717
				// (set) Token: 0x06007316 RID: 29462 RVA: 0x0018F51F File Offset: 0x0018D71F
				public selection7 selection7 { get; private set; }

				// Token: 0x1700151E RID: 5406
				// (get) Token: 0x06007317 RID: 29463 RVA: 0x0018F528 File Offset: 0x0018D728
				// (set) Token: 0x06007318 RID: 29464 RVA: 0x0018F530 File Offset: 0x0018D730
				public filterSelection4 filterSelection4 { get; private set; }

				// Token: 0x1700151F RID: 5407
				// (get) Token: 0x06007319 RID: 29465 RVA: 0x0018F539 File Offset: 0x0018D739
				// (set) Token: 0x0600731A RID: 29466 RVA: 0x0018F541 File Offset: 0x0018D741
				public selection8 selection8 { get; private set; }

				// Token: 0x17001520 RID: 5408
				// (get) Token: 0x0600731B RID: 29467 RVA: 0x0018F54A File Offset: 0x0018D74A
				// (set) Token: 0x0600731C RID: 29468 RVA: 0x0018F552 File Offset: 0x0018D752
				public selection9 selection9 { get; private set; }

				// Token: 0x17001521 RID: 5409
				// (get) Token: 0x0600731D RID: 29469 RVA: 0x0018F55B File Offset: 0x0018D75B
				// (set) Token: 0x0600731E RID: 29470 RVA: 0x0018F563 File Offset: 0x0018D763
				public filterSelection5 filterSelection5 { get; private set; }

				// Token: 0x17001522 RID: 5410
				// (get) Token: 0x0600731F RID: 29471 RVA: 0x0018F56C File Offset: 0x0018D76C
				// (set) Token: 0x06007320 RID: 29472 RVA: 0x0018F574 File Offset: 0x0018D774
				public selection10 selection10 { get; private set; }

				// Token: 0x17001523 RID: 5411
				// (get) Token: 0x06007321 RID: 29473 RVA: 0x0018F57D File Offset: 0x0018D77D
				// (set) Token: 0x06007322 RID: 29474 RVA: 0x0018F585 File Offset: 0x0018D785
				public leafFExpr leafFExpr { get; private set; }

				// Token: 0x17001524 RID: 5412
				// (get) Token: 0x06007323 RID: 29475 RVA: 0x0018F58E File Offset: 0x0018D78E
				// (set) Token: 0x06007324 RID: 29476 RVA: 0x0018F596 File Offset: 0x0018D796
				public leafAtom leafAtom { get; private set; }

				// Token: 0x17001525 RID: 5413
				// (get) Token: 0x06007325 RID: 29477 RVA: 0x0018F59F File Offset: 0x0018D79F
				// (set) Token: 0x06007326 RID: 29478 RVA: 0x0018F5A7 File Offset: 0x0018D7A7
				public atomExpr atomExpr { get; private set; }

				// Token: 0x17001526 RID: 5414
				// (get) Token: 0x06007327 RID: 29479 RVA: 0x0018F5B0 File Offset: 0x0018D7B0
				// (set) Token: 0x06007328 RID: 29480 RVA: 0x0018F5B8 File Offset: 0x0018D7B8
				public literalExpr literalExpr { get; private set; }

				// Token: 0x17001527 RID: 5415
				// (get) Token: 0x06007329 RID: 29481 RVA: 0x0018F5C1 File Offset: 0x0018D7C1
				// (set) Token: 0x0600732A RID: 29482 RVA: 0x0018F5C9 File Offset: 0x0018D7C9
				public fexpr fexpr { get; private set; }

				// Token: 0x17001528 RID: 5416
				// (get) Token: 0x0600732B RID: 29483 RVA: 0x0018F5D2 File Offset: 0x0018D7D2
				// (set) Token: 0x0600732C RID: 29484 RVA: 0x0018F5DA File Offset: 0x0018D7DA
				public resultFields resultFields { get; private set; }

				// Token: 0x17001529 RID: 5417
				// (get) Token: 0x0600732D RID: 29485 RVA: 0x0018F5E3 File Offset: 0x0018D7E3
				// (set) Token: 0x0600732E RID: 29486 RVA: 0x0018F5EB File Offset: 0x0018D7EB
				public singletonField singletonField { get; private set; }

				// Token: 0x1700152A RID: 5418
				// (get) Token: 0x0600732F RID: 29487 RVA: 0x0018F5F4 File Offset: 0x0018D7F4
				// (set) Token: 0x06007330 RID: 29488 RVA: 0x0018F5FC File Offset: 0x0018D7FC
				public fieldSubstring fieldSubstring { get; private set; }

				// Token: 0x1700152B RID: 5419
				// (get) Token: 0x06007331 RID: 29489 RVA: 0x0018F605 File Offset: 0x0018D805
				// (set) Token: 0x06007332 RID: 29490 RVA: 0x0018F60D File Offset: 0x0018D80D
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs cs { get; private set; }

				// Token: 0x1700152C RID: 5420
				// (get) Token: 0x06007333 RID: 29491 RVA: 0x0018F616 File Offset: 0x0018D816
				// (set) Token: 0x06007334 RID: 29492 RVA: 0x0018F61E File Offset: 0x0018D81E
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y y { get; private set; }

				// Token: 0x1700152D RID: 5421
				// (get) Token: 0x06007335 RID: 29493 RVA: 0x0018F627 File Offset: 0x0018D827
				// (set) Token: 0x06007336 RID: 29494 RVA: 0x0018F62F File Offset: 0x0018D82F
				public selectSubstring selectSubstring { get; private set; }

				// Token: 0x1700152E RID: 5422
				// (get) Token: 0x06007337 RID: 29495 RVA: 0x0018F638 File Offset: 0x0018D838
				// (set) Token: 0x06007338 RID: 29496 RVA: 0x0018F640 File Offset: 0x0018D840
				public substringDisj substringDisj { get; private set; }

				// Token: 0x1700152F RID: 5423
				// (get) Token: 0x06007339 RID: 29497 RVA: 0x0018F649 File Offset: 0x0018D849
				// (set) Token: 0x0600733A RID: 29498 RVA: 0x0018F651 File Offset: 0x0018D851
				public substring substring { get; private set; }

				// Token: 0x17001530 RID: 5424
				// (get) Token: 0x0600733B RID: 29499 RVA: 0x0018F65A File Offset: 0x0018D85A
				// (set) Token: 0x0600733C RID: 29500 RVA: 0x0018F662 File Offset: 0x0018D862
				public resultTable resultTable { get; private set; }

				// Token: 0x17001531 RID: 5425
				// (get) Token: 0x0600733D RID: 29501 RVA: 0x0018F66B File Offset: 0x0018D86B
				// (set) Token: 0x0600733E RID: 29502 RVA: 0x0018F673 File Offset: 0x0018D873
				public columnSelectors columnSelectors { get; private set; }

				// Token: 0x17001532 RID: 5426
				// (get) Token: 0x0600733F RID: 29503 RVA: 0x0018F67C File Offset: 0x0018D87C
				// (set) Token: 0x06007340 RID: 29504 RVA: 0x0018F684 File Offset: 0x0018D884
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name name { get; private set; }

				// Token: 0x17001533 RID: 5427
				// (get) Token: 0x06007341 RID: 29505 RVA: 0x0018F68D File Offset: 0x0018D88D
				// (set) Token: 0x06007342 RID: 29506 RVA: 0x0018F695 File Offset: 0x0018D895
				public value value { get; private set; }

				// Token: 0x17001534 RID: 5428
				// (get) Token: 0x06007343 RID: 29507 RVA: 0x0018F69E File Offset: 0x0018D89E
				// (set) Token: 0x06007344 RID: 29508 RVA: 0x0018F6A6 File Offset: 0x0018D8A6
				public cssSelector cssSelector { get; private set; }

				// Token: 0x17001535 RID: 5429
				// (get) Token: 0x06007345 RID: 29509 RVA: 0x0018F6AF File Offset: 0x0018D8AF
				// (set) Token: 0x06007346 RID: 29510 RVA: 0x0018F6B7 File Offset: 0x0018D8B7
				public className className { get; private set; }

				// Token: 0x17001536 RID: 5430
				// (get) Token: 0x06007347 RID: 29511 RVA: 0x0018F6C0 File Offset: 0x0018D8C0
				// (set) Token: 0x06007348 RID: 29512 RVA: 0x0018F6C8 File Offset: 0x0018D8C8
				public idName idName { get; private set; }

				// Token: 0x17001537 RID: 5431
				// (get) Token: 0x06007349 RID: 29513 RVA: 0x0018F6D1 File Offset: 0x0018D8D1
				// (set) Token: 0x0600734A RID: 29514 RVA: 0x0018F6D9 File Offset: 0x0018D8D9
				public nodeName nodeName { get; private set; }

				// Token: 0x17001538 RID: 5432
				// (get) Token: 0x0600734B RID: 29515 RVA: 0x0018F6E2 File Offset: 0x0018D8E2
				// (set) Token: 0x0600734C RID: 29516 RVA: 0x0018F6EA File Offset: 0x0018D8EA
				public propName propName { get; private set; }

				// Token: 0x17001539 RID: 5433
				// (get) Token: 0x0600734D RID: 29517 RVA: 0x0018F6F3 File Offset: 0x0018D8F3
				// (set) Token: 0x0600734E RID: 29518 RVA: 0x0018F6FB File Offset: 0x0018D8FB
				public idx1 idx1 { get; private set; }

				// Token: 0x1700153A RID: 5434
				// (get) Token: 0x0600734F RID: 29519 RVA: 0x0018F704 File Offset: 0x0018D904
				// (set) Token: 0x06007350 RID: 29520 RVA: 0x0018F70C File Offset: 0x0018D90C
				public idx2 idx2 { get; private set; }

				// Token: 0x1700153B RID: 5435
				// (get) Token: 0x06007351 RID: 29521 RVA: 0x0018F715 File Offset: 0x0018D915
				// (set) Token: 0x06007352 RID: 29522 RVA: 0x0018F71D File Offset: 0x0018D91D
				public names names { get; private set; }

				// Token: 0x1700153C RID: 5436
				// (get) Token: 0x06007353 RID: 29523 RVA: 0x0018F726 File Offset: 0x0018D926
				// (set) Token: 0x06007354 RID: 29524 RVA: 0x0018F72E File Offset: 0x0018D92E
				public count count { get; private set; }

				// Token: 0x1700153D RID: 5437
				// (get) Token: 0x06007355 RID: 29525 RVA: 0x0018F737 File Offset: 0x0018D937
				// (set) Token: 0x06007356 RID: 29526 RVA: 0x0018F73F File Offset: 0x0018D93F
				public substringFeatureNames substringFeatureNames { get; private set; }

				// Token: 0x1700153E RID: 5438
				// (get) Token: 0x06007357 RID: 29527 RVA: 0x0018F748 File Offset: 0x0018D948
				// (set) Token: 0x06007358 RID: 29528 RVA: 0x0018F750 File Offset: 0x0018D950
				public substringFeatureValues substringFeatureValues { get; private set; }

				// Token: 0x1700153F RID: 5439
				// (get) Token: 0x06007359 RID: 29529 RVA: 0x0018F759 File Offset: 0x0018D959
				// (set) Token: 0x0600735A RID: 29530 RVA: 0x0018F761 File Offset: 0x0018D961
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k k { get; private set; }

				// Token: 0x17001540 RID: 5440
				// (get) Token: 0x0600735B RID: 29531 RVA: 0x0018F76A File Offset: 0x0018D96A
				// (set) Token: 0x0600735C RID: 29532 RVA: 0x0018F772 File Offset: 0x0018D972
				public entityObjs entityObjs { get; private set; }

				// Token: 0x17001541 RID: 5441
				// (get) Token: 0x0600735D RID: 29533 RVA: 0x0018F77B File Offset: 0x0018D97B
				// (set) Token: 0x0600735E RID: 29534 RVA: 0x0018F783 File Offset: 0x0018D983
				public direction direction { get; private set; }

				// Token: 0x17001542 RID: 5442
				// (get) Token: 0x0600735F RID: 29535 RVA: 0x0018F78C File Offset: 0x0018D98C
				// (set) Token: 0x06007360 RID: 29536 RVA: 0x0018F794 File Offset: 0x0018D994
				public nodeCollection nodeCollection { get; private set; }

				// Token: 0x17001543 RID: 5443
				// (get) Token: 0x06007361 RID: 29537 RVA: 0x0018F79D File Offset: 0x0018D99D
				// (set) Token: 0x06007362 RID: 29538 RVA: 0x0018F7A5 File Offset: 0x0018D9A5
				public gen_NthChild gen_NthChild { get; private set; }

				// Token: 0x17001544 RID: 5444
				// (get) Token: 0x06007363 RID: 29539 RVA: 0x0018F7AE File Offset: 0x0018D9AE
				// (set) Token: 0x06007364 RID: 29540 RVA: 0x0018F7B6 File Offset: 0x0018D9B6
				public gen_NthLastChild gen_NthLastChild { get; private set; }

				// Token: 0x17001545 RID: 5445
				// (get) Token: 0x06007365 RID: 29541 RVA: 0x0018F7BF File Offset: 0x0018D9BF
				// (set) Token: 0x06007366 RID: 29542 RVA: 0x0018F7C7 File Offset: 0x0018D9C7
				public gen_Class gen_Class { get; private set; }

				// Token: 0x17001546 RID: 5446
				// (get) Token: 0x06007367 RID: 29543 RVA: 0x0018F7D0 File Offset: 0x0018D9D0
				// (set) Token: 0x06007368 RID: 29544 RVA: 0x0018F7D8 File Offset: 0x0018D9D8
				public gen_ID gen_ID { get; private set; }

				// Token: 0x17001547 RID: 5447
				// (get) Token: 0x06007369 RID: 29545 RVA: 0x0018F7E1 File Offset: 0x0018D9E1
				// (set) Token: 0x0600736A RID: 29546 RVA: 0x0018F7E9 File Offset: 0x0018D9E9
				public gen_NodeName gen_NodeName { get; private set; }

				// Token: 0x17001548 RID: 5448
				// (get) Token: 0x0600736B RID: 29547 RVA: 0x0018F7F2 File Offset: 0x0018D9F2
				// (set) Token: 0x0600736C RID: 29548 RVA: 0x0018F7FA File Offset: 0x0018D9FA
				public gen_ItemProp gen_ItemProp { get; private set; }

				// Token: 0x17001549 RID: 5449
				// (get) Token: 0x0600736D RID: 29549 RVA: 0x0018F803 File Offset: 0x0018DA03
				// (set) Token: 0x0600736E RID: 29550 RVA: 0x0018F80B File Offset: 0x0018DA0B
				public obj obj { get; private set; }

				// Token: 0x1700154A RID: 5450
				// (get) Token: 0x0600736F RID: 29551 RVA: 0x0018F814 File Offset: 0x0018DA14
				// (set) Token: 0x06007370 RID: 29552 RVA: 0x0018F81C File Offset: 0x0018DA1C
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 _LetB0 { get; private set; }

				// Token: 0x06007371 RID: 29553 RVA: 0x0018F828 File Offset: 0x0018DA28
				public NodeHoles(GrammarBuilders builders)
				{
					this.resultSequence = resultSequence.CreateHole(builders, null);
					this.resultRegion = resultRegion.CreateHole(builders, null);
					this.subNodeSequence = subNodeSequence.CreateHole(builders, null);
					this.node = node.CreateHole(builders, null);
					this.subNode = subNode.CreateHole(builders, null);
					this.mapNodeInSequence = mapNodeInSequence.CreateHole(builders, null);
					this.regionSequence = regionSequence.CreateHole(builders, null);
					this.regionStart = regionStart.CreateHole(builders, null);
					this.region = region.CreateHole(builders, null);
					this.mapRegionInSequence = mapRegionInSequence.CreateHole(builders, null);
					this.beginNode = beginNode.CreateHole(builders, null);
					this.endNode = endNode.CreateHole(builders, null);
					this.selection = selection.CreateHole(builders, null);
					this.filterSelection = filterSelection.CreateHole(builders, null);
					this.selectionEnd = selectionEnd.CreateHole(builders, null);
					this.regionStartSiblings = regionStartSiblings.CreateHole(builders, null);
					this.selection2 = selection2.CreateHole(builders, null);
					this.selection3 = selection3.CreateHole(builders, null);
					this.filterSelection2 = filterSelection2.CreateHole(builders, null);
					this.selection4 = selection4.CreateHole(builders, null);
					this.selection5 = selection5.CreateHole(builders, null);
					this.filterSelection3 = filterSelection3.CreateHole(builders, null);
					this.selection6 = selection6.CreateHole(builders, null);
					this.selection7 = selection7.CreateHole(builders, null);
					this.filterSelection4 = filterSelection4.CreateHole(builders, null);
					this.selection8 = selection8.CreateHole(builders, null);
					this.selection9 = selection9.CreateHole(builders, null);
					this.filterSelection5 = filterSelection5.CreateHole(builders, null);
					this.selection10 = selection10.CreateHole(builders, null);
					this.leafFExpr = leafFExpr.CreateHole(builders, null);
					this.leafAtom = leafAtom.CreateHole(builders, null);
					this.atomExpr = atomExpr.CreateHole(builders, null);
					this.literalExpr = literalExpr.CreateHole(builders, null);
					this.fexpr = fexpr.CreateHole(builders, null);
					this.resultFields = resultFields.CreateHole(builders, null);
					this.singletonField = singletonField.CreateHole(builders, null);
					this.fieldSubstring = fieldSubstring.CreateHole(builders, null);
					this.cs = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateHole(builders, null);
					this.y = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateHole(builders, null);
					this.selectSubstring = selectSubstring.CreateHole(builders, null);
					this.substringDisj = substringDisj.CreateHole(builders, null);
					this.substring = substring.CreateHole(builders, null);
					this.resultTable = resultTable.CreateHole(builders, null);
					this.columnSelectors = columnSelectors.CreateHole(builders, null);
					this.name = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateHole(builders, null);
					this.value = value.CreateHole(builders, null);
					this.cssSelector = cssSelector.CreateHole(builders, null);
					this.className = className.CreateHole(builders, null);
					this.idName = idName.CreateHole(builders, null);
					this.nodeName = nodeName.CreateHole(builders, null);
					this.propName = propName.CreateHole(builders, null);
					this.idx1 = idx1.CreateHole(builders, null);
					this.idx2 = idx2.CreateHole(builders, null);
					this.names = names.CreateHole(builders, null);
					this.count = count.CreateHole(builders, null);
					this.substringFeatureNames = substringFeatureNames.CreateHole(builders, null);
					this.substringFeatureValues = substringFeatureValues.CreateHole(builders, null);
					this.k = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateHole(builders, null);
					this.entityObjs = entityObjs.CreateHole(builders, null);
					this.direction = direction.CreateHole(builders, null);
					this.nodeCollection = nodeCollection.CreateHole(builders, null);
					this.gen_NthChild = gen_NthChild.CreateHole(builders, null);
					this.gen_NthLastChild = gen_NthLastChild.CreateHole(builders, null);
					this.gen_Class = gen_Class.CreateHole(builders, null);
					this.gen_ID = gen_ID.CreateHole(builders, null);
					this.gen_NodeName = gen_NodeName.CreateHole(builders, null);
					this.gen_ItemProp = gen_ItemProp.CreateHole(builders, null);
					this.obj = obj.CreateHole(builders, null);
					this._LetB0 = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateHole(builders, null);
				}
			}

			// Token: 0x02000FEA RID: 4074
			public class NodeUnsafe
			{
				// Token: 0x06007372 RID: 29554 RVA: 0x0018FBBC File Offset: 0x0018DDBC
				public resultSequence resultSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence.CreateUnsafe(node);
				}

				// Token: 0x06007373 RID: 29555 RVA: 0x0018FBC4 File Offset: 0x0018DDC4
				public resultRegion resultRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion.CreateUnsafe(node);
				}

				// Token: 0x06007374 RID: 29556 RVA: 0x0018FBCC File Offset: 0x0018DDCC
				public subNodeSequence subNodeSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence.CreateUnsafe(node);
				}

				// Token: 0x06007375 RID: 29557 RVA: 0x0018FBD4 File Offset: 0x0018DDD4
				public node node(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node.CreateUnsafe(node);
				}

				// Token: 0x06007376 RID: 29558 RVA: 0x0018FBDC File Offset: 0x0018DDDC
				public subNode subNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode.CreateUnsafe(node);
				}

				// Token: 0x06007377 RID: 29559 RVA: 0x0018FBE4 File Offset: 0x0018DDE4
				public mapNodeInSequence mapNodeInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence.CreateUnsafe(node);
				}

				// Token: 0x06007378 RID: 29560 RVA: 0x0018FBEC File Offset: 0x0018DDEC
				public regionSequence regionSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence.CreateUnsafe(node);
				}

				// Token: 0x06007379 RID: 29561 RVA: 0x0018FBF4 File Offset: 0x0018DDF4
				public regionStart regionStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart.CreateUnsafe(node);
				}

				// Token: 0x0600737A RID: 29562 RVA: 0x0018FBFC File Offset: 0x0018DDFC
				public region region(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region.CreateUnsafe(node);
				}

				// Token: 0x0600737B RID: 29563 RVA: 0x0018FC04 File Offset: 0x0018DE04
				public mapRegionInSequence mapRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence.CreateUnsafe(node);
				}

				// Token: 0x0600737C RID: 29564 RVA: 0x0018FC0C File Offset: 0x0018DE0C
				public beginNode beginNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode.CreateUnsafe(node);
				}

				// Token: 0x0600737D RID: 29565 RVA: 0x0018FC14 File Offset: 0x0018DE14
				public endNode endNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode.CreateUnsafe(node);
				}

				// Token: 0x0600737E RID: 29566 RVA: 0x0018FC1C File Offset: 0x0018DE1C
				public selection selection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection.CreateUnsafe(node);
				}

				// Token: 0x0600737F RID: 29567 RVA: 0x0018FC24 File Offset: 0x0018DE24
				public filterSelection filterSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection.CreateUnsafe(node);
				}

				// Token: 0x06007380 RID: 29568 RVA: 0x0018FC2C File Offset: 0x0018DE2C
				public selectionEnd selectionEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd.CreateUnsafe(node);
				}

				// Token: 0x06007381 RID: 29569 RVA: 0x0018FC34 File Offset: 0x0018DE34
				public regionStartSiblings regionStartSiblings(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings.CreateUnsafe(node);
				}

				// Token: 0x06007382 RID: 29570 RVA: 0x0018FC3C File Offset: 0x0018DE3C
				public selection2 selection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2.CreateUnsafe(node);
				}

				// Token: 0x06007383 RID: 29571 RVA: 0x0018FC44 File Offset: 0x0018DE44
				public selection3 selection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3.CreateUnsafe(node);
				}

				// Token: 0x06007384 RID: 29572 RVA: 0x0018FC4C File Offset: 0x0018DE4C
				public filterSelection2 filterSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2.CreateUnsafe(node);
				}

				// Token: 0x06007385 RID: 29573 RVA: 0x0018FC54 File Offset: 0x0018DE54
				public selection4 selection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4.CreateUnsafe(node);
				}

				// Token: 0x06007386 RID: 29574 RVA: 0x0018FC5C File Offset: 0x0018DE5C
				public selection5 selection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5.CreateUnsafe(node);
				}

				// Token: 0x06007387 RID: 29575 RVA: 0x0018FC64 File Offset: 0x0018DE64
				public filterSelection3 filterSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3.CreateUnsafe(node);
				}

				// Token: 0x06007388 RID: 29576 RVA: 0x0018FC6C File Offset: 0x0018DE6C
				public selection6 selection6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6.CreateUnsafe(node);
				}

				// Token: 0x06007389 RID: 29577 RVA: 0x0018FC74 File Offset: 0x0018DE74
				public selection7 selection7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7.CreateUnsafe(node);
				}

				// Token: 0x0600738A RID: 29578 RVA: 0x0018FC7C File Offset: 0x0018DE7C
				public filterSelection4 filterSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4.CreateUnsafe(node);
				}

				// Token: 0x0600738B RID: 29579 RVA: 0x0018FC84 File Offset: 0x0018DE84
				public selection8 selection8(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8.CreateUnsafe(node);
				}

				// Token: 0x0600738C RID: 29580 RVA: 0x0018FC8C File Offset: 0x0018DE8C
				public selection9 selection9(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9.CreateUnsafe(node);
				}

				// Token: 0x0600738D RID: 29581 RVA: 0x0018FC94 File Offset: 0x0018DE94
				public filterSelection5 filterSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5.CreateUnsafe(node);
				}

				// Token: 0x0600738E RID: 29582 RVA: 0x0018FC9C File Offset: 0x0018DE9C
				public selection10 selection10(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10.CreateUnsafe(node);
				}

				// Token: 0x0600738F RID: 29583 RVA: 0x0018FCA4 File Offset: 0x0018DEA4
				public leafFExpr leafFExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr.CreateUnsafe(node);
				}

				// Token: 0x06007390 RID: 29584 RVA: 0x0018FCAC File Offset: 0x0018DEAC
				public leafAtom leafAtom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom.CreateUnsafe(node);
				}

				// Token: 0x06007391 RID: 29585 RVA: 0x0018FCB4 File Offset: 0x0018DEB4
				public atomExpr atomExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr.CreateUnsafe(node);
				}

				// Token: 0x06007392 RID: 29586 RVA: 0x0018FCBC File Offset: 0x0018DEBC
				public literalExpr literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr.CreateUnsafe(node);
				}

				// Token: 0x06007393 RID: 29587 RVA: 0x0018FCC4 File Offset: 0x0018DEC4
				public fexpr fexpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr.CreateUnsafe(node);
				}

				// Token: 0x06007394 RID: 29588 RVA: 0x0018FCCC File Offset: 0x0018DECC
				public resultFields resultFields(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields.CreateUnsafe(node);
				}

				// Token: 0x06007395 RID: 29589 RVA: 0x0018FCD4 File Offset: 0x0018DED4
				public singletonField singletonField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField.CreateUnsafe(node);
				}

				// Token: 0x06007396 RID: 29590 RVA: 0x0018FCDC File Offset: 0x0018DEDC
				public fieldSubstring fieldSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring.CreateUnsafe(node);
				}

				// Token: 0x06007397 RID: 29591 RVA: 0x0018FCE4 File Offset: 0x0018DEE4
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateUnsafe(node);
				}

				// Token: 0x06007398 RID: 29592 RVA: 0x0018FCEC File Offset: 0x0018DEEC
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateUnsafe(node);
				}

				// Token: 0x06007399 RID: 29593 RVA: 0x0018FCF4 File Offset: 0x0018DEF4
				public selectSubstring selectSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring.CreateUnsafe(node);
				}

				// Token: 0x0600739A RID: 29594 RVA: 0x0018FCFC File Offset: 0x0018DEFC
				public substringDisj substringDisj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj.CreateUnsafe(node);
				}

				// Token: 0x0600739B RID: 29595 RVA: 0x0018FD04 File Offset: 0x0018DF04
				public substring substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring.CreateUnsafe(node);
				}

				// Token: 0x0600739C RID: 29596 RVA: 0x0018FD0C File Offset: 0x0018DF0C
				public resultTable resultTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable.CreateUnsafe(node);
				}

				// Token: 0x0600739D RID: 29597 RVA: 0x0018FD14 File Offset: 0x0018DF14
				public columnSelectors columnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors.CreateUnsafe(node);
				}

				// Token: 0x0600739E RID: 29598 RVA: 0x0018FD1C File Offset: 0x0018DF1C
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateUnsafe(node);
				}

				// Token: 0x0600739F RID: 29599 RVA: 0x0018FD24 File Offset: 0x0018DF24
				public value value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value.CreateUnsafe(node);
				}

				// Token: 0x060073A0 RID: 29600 RVA: 0x0018FD2C File Offset: 0x0018DF2C
				public cssSelector cssSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector.CreateUnsafe(node);
				}

				// Token: 0x060073A1 RID: 29601 RVA: 0x0018FD34 File Offset: 0x0018DF34
				public className className(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className.CreateUnsafe(node);
				}

				// Token: 0x060073A2 RID: 29602 RVA: 0x0018FD3C File Offset: 0x0018DF3C
				public idName idName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName.CreateUnsafe(node);
				}

				// Token: 0x060073A3 RID: 29603 RVA: 0x0018FD44 File Offset: 0x0018DF44
				public nodeName nodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName.CreateUnsafe(node);
				}

				// Token: 0x060073A4 RID: 29604 RVA: 0x0018FD4C File Offset: 0x0018DF4C
				public propName propName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName.CreateUnsafe(node);
				}

				// Token: 0x060073A5 RID: 29605 RVA: 0x0018FD54 File Offset: 0x0018DF54
				public idx1 idx1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1.CreateUnsafe(node);
				}

				// Token: 0x060073A6 RID: 29606 RVA: 0x0018FD5C File Offset: 0x0018DF5C
				public idx2 idx2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2.CreateUnsafe(node);
				}

				// Token: 0x060073A7 RID: 29607 RVA: 0x0018FD64 File Offset: 0x0018DF64
				public names names(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names.CreateUnsafe(node);
				}

				// Token: 0x060073A8 RID: 29608 RVA: 0x0018FD6C File Offset: 0x0018DF6C
				public count count(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count.CreateUnsafe(node);
				}

				// Token: 0x060073A9 RID: 29609 RVA: 0x0018FD74 File Offset: 0x0018DF74
				public substringFeatureNames substringFeatureNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames.CreateUnsafe(node);
				}

				// Token: 0x060073AA RID: 29610 RVA: 0x0018FD7C File Offset: 0x0018DF7C
				public substringFeatureValues substringFeatureValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues.CreateUnsafe(node);
				}

				// Token: 0x060073AB RID: 29611 RVA: 0x0018FD84 File Offset: 0x0018DF84
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x060073AC RID: 29612 RVA: 0x0018FD8C File Offset: 0x0018DF8C
				public entityObjs entityObjs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs.CreateUnsafe(node);
				}

				// Token: 0x060073AD RID: 29613 RVA: 0x0018FD94 File Offset: 0x0018DF94
				public direction direction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction.CreateUnsafe(node);
				}

				// Token: 0x060073AE RID: 29614 RVA: 0x0018FD9C File Offset: 0x0018DF9C
				public nodeCollection nodeCollection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection.CreateUnsafe(node);
				}

				// Token: 0x060073AF RID: 29615 RVA: 0x0018FDA4 File Offset: 0x0018DFA4
				public gen_NthChild gen_NthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild.CreateUnsafe(node);
				}

				// Token: 0x060073B0 RID: 29616 RVA: 0x0018FDAC File Offset: 0x0018DFAC
				public gen_NthLastChild gen_NthLastChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild.CreateUnsafe(node);
				}

				// Token: 0x060073B1 RID: 29617 RVA: 0x0018FDB4 File Offset: 0x0018DFB4
				public gen_Class gen_Class(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class.CreateUnsafe(node);
				}

				// Token: 0x060073B2 RID: 29618 RVA: 0x0018FDBC File Offset: 0x0018DFBC
				public gen_ID gen_ID(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID.CreateUnsafe(node);
				}

				// Token: 0x060073B3 RID: 29619 RVA: 0x0018FDC4 File Offset: 0x0018DFC4
				public gen_NodeName gen_NodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName.CreateUnsafe(node);
				}

				// Token: 0x060073B4 RID: 29620 RVA: 0x0018FDCC File Offset: 0x0018DFCC
				public gen_ItemProp gen_ItemProp(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp.CreateUnsafe(node);
				}

				// Token: 0x060073B5 RID: 29621 RVA: 0x0018FDD4 File Offset: 0x0018DFD4
				public obj obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj.CreateUnsafe(node);
				}

				// Token: 0x060073B6 RID: 29622 RVA: 0x0018FDDC File Offset: 0x0018DFDC
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}
			}

			// Token: 0x02000FEB RID: 4075
			public class NodeCast
			{
				// Token: 0x060073B8 RID: 29624 RVA: 0x0018FDE4 File Offset: 0x0018DFE4
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060073B9 RID: 29625 RVA: 0x0018FDF4 File Offset: 0x0018DFF4
				public resultSequence resultSequence(ProgramNode node)
				{
					resultSequence? resultSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence.CreateSafe(this._builders, node);
					if (resultSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultSequence.Value;
				}

				// Token: 0x060073BA RID: 29626 RVA: 0x0018FE48 File Offset: 0x0018E048
				public resultRegion resultRegion(ProgramNode node)
				{
					resultRegion? resultRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion.CreateSafe(this._builders, node);
					if (resultRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultRegion.Value;
				}

				// Token: 0x060073BB RID: 29627 RVA: 0x0018FE9C File Offset: 0x0018E09C
				public subNodeSequence subNodeSequence(ProgramNode node)
				{
					subNodeSequence? subNodeSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence.CreateSafe(this._builders, node);
					if (subNodeSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol subNodeSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subNodeSequence.Value;
				}

				// Token: 0x060073BC RID: 29628 RVA: 0x0018FEF0 File Offset: 0x0018E0F0
				public node node(ProgramNode node)
				{
					node? node2 = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node.CreateSafe(this._builders, node);
					if (node2 == null)
					{
						string text = "node";
						string text2 = "expected node for symbol node but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return node2.Value;
				}

				// Token: 0x060073BD RID: 29629 RVA: 0x0018FF44 File Offset: 0x0018E144
				public subNode subNode(ProgramNode node)
				{
					subNode? subNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode.CreateSafe(this._builders, node);
					if (subNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol subNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subNode.Value;
				}

				// Token: 0x060073BE RID: 29630 RVA: 0x0018FF98 File Offset: 0x0018E198
				public mapNodeInSequence mapNodeInSequence(ProgramNode node)
				{
					mapNodeInSequence? mapNodeInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence.CreateSafe(this._builders, node);
					if (mapNodeInSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mapNodeInSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mapNodeInSequence.Value;
				}

				// Token: 0x060073BF RID: 29631 RVA: 0x0018FFEC File Offset: 0x0018E1EC
				public regionSequence regionSequence(ProgramNode node)
				{
					regionSequence? regionSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence.CreateSafe(this._builders, node);
					if (regionSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regionSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regionSequence.Value;
				}

				// Token: 0x060073C0 RID: 29632 RVA: 0x00190040 File Offset: 0x0018E240
				public regionStart regionStart(ProgramNode node)
				{
					regionStart? regionStart = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart.CreateSafe(this._builders, node);
					if (regionStart == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regionStart but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regionStart.Value;
				}

				// Token: 0x060073C1 RID: 29633 RVA: 0x00190094 File Offset: 0x0018E294
				public region region(ProgramNode node)
				{
					region? region = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region.CreateSafe(this._builders, node);
					if (region == null)
					{
						string text = "node";
						string text2 = "expected node for symbol region but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return region.Value;
				}

				// Token: 0x060073C2 RID: 29634 RVA: 0x001900E8 File Offset: 0x0018E2E8
				public mapRegionInSequence mapRegionInSequence(ProgramNode node)
				{
					mapRegionInSequence? mapRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence.CreateSafe(this._builders, node);
					if (mapRegionInSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol mapRegionInSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mapRegionInSequence.Value;
				}

				// Token: 0x060073C3 RID: 29635 RVA: 0x0019013C File Offset: 0x0018E33C
				public beginNode beginNode(ProgramNode node)
				{
					beginNode? beginNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode.CreateSafe(this._builders, node);
					if (beginNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol beginNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return beginNode.Value;
				}

				// Token: 0x060073C4 RID: 29636 RVA: 0x00190190 File Offset: 0x0018E390
				public endNode endNode(ProgramNode node)
				{
					endNode? endNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode.CreateSafe(this._builders, node);
					if (endNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol endNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endNode.Value;
				}

				// Token: 0x060073C5 RID: 29637 RVA: 0x001901E4 File Offset: 0x0018E3E4
				public selection selection(ProgramNode node)
				{
					selection? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073C6 RID: 29638 RVA: 0x00190238 File Offset: 0x0018E438
				public filterSelection filterSelection(ProgramNode node)
				{
					filterSelection? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterSelection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterSelection.Value;
				}

				// Token: 0x060073C7 RID: 29639 RVA: 0x0019028C File Offset: 0x0018E48C
				public selectionEnd selectionEnd(ProgramNode node)
				{
					selectionEnd? selectionEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd.CreateSafe(this._builders, node);
					if (selectionEnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectionEnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectionEnd.Value;
				}

				// Token: 0x060073C8 RID: 29640 RVA: 0x001902E0 File Offset: 0x0018E4E0
				public regionStartSiblings regionStartSiblings(ProgramNode node)
				{
					regionStartSiblings? regionStartSiblings = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings.CreateSafe(this._builders, node);
					if (regionStartSiblings == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regionStartSiblings but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regionStartSiblings.Value;
				}

				// Token: 0x060073C9 RID: 29641 RVA: 0x00190334 File Offset: 0x0018E534
				public selection2 selection2(ProgramNode node)
				{
					selection2? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073CA RID: 29642 RVA: 0x00190388 File Offset: 0x0018E588
				public selection3 selection3(ProgramNode node)
				{
					selection3? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073CB RID: 29643 RVA: 0x001903DC File Offset: 0x0018E5DC
				public filterSelection2 filterSelection2(ProgramNode node)
				{
					filterSelection2? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterSelection2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterSelection.Value;
				}

				// Token: 0x060073CC RID: 29644 RVA: 0x00190430 File Offset: 0x0018E630
				public selection4 selection4(ProgramNode node)
				{
					selection4? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073CD RID: 29645 RVA: 0x00190484 File Offset: 0x0018E684
				public selection5 selection5(ProgramNode node)
				{
					selection5? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073CE RID: 29646 RVA: 0x001904D8 File Offset: 0x0018E6D8
				public filterSelection3 filterSelection3(ProgramNode node)
				{
					filterSelection3? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterSelection3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterSelection.Value;
				}

				// Token: 0x060073CF RID: 29647 RVA: 0x0019052C File Offset: 0x0018E72C
				public selection6 selection6(ProgramNode node)
				{
					selection6? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection6 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073D0 RID: 29648 RVA: 0x00190580 File Offset: 0x0018E780
				public selection7 selection7(ProgramNode node)
				{
					selection7? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection7 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073D1 RID: 29649 RVA: 0x001905D4 File Offset: 0x0018E7D4
				public filterSelection4 filterSelection4(ProgramNode node)
				{
					filterSelection4? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterSelection4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterSelection.Value;
				}

				// Token: 0x060073D2 RID: 29650 RVA: 0x00190628 File Offset: 0x0018E828
				public selection8 selection8(ProgramNode node)
				{
					selection8? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection8 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073D3 RID: 29651 RVA: 0x0019067C File Offset: 0x0018E87C
				public selection9 selection9(ProgramNode node)
				{
					selection9? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection9 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073D4 RID: 29652 RVA: 0x001906D0 File Offset: 0x0018E8D0
				public filterSelection5 filterSelection5(ProgramNode node)
				{
					filterSelection5? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol filterSelection5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterSelection.Value;
				}

				// Token: 0x060073D5 RID: 29653 RVA: 0x00190724 File Offset: 0x0018E924
				public selection10 selection10(ProgramNode node)
				{
					selection10? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10.CreateSafe(this._builders, node);
					if (selection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection10 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection.Value;
				}

				// Token: 0x060073D6 RID: 29654 RVA: 0x00190778 File Offset: 0x0018E978
				public leafFExpr leafFExpr(ProgramNode node)
				{
					leafFExpr? leafFExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr.CreateSafe(this._builders, node);
					if (leafFExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol leafFExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFExpr.Value;
				}

				// Token: 0x060073D7 RID: 29655 RVA: 0x001907CC File Offset: 0x0018E9CC
				public leafAtom leafAtom(ProgramNode node)
				{
					leafAtom? leafAtom = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom.CreateSafe(this._builders, node);
					if (leafAtom == null)
					{
						string text = "node";
						string text2 = "expected node for symbol leafAtom but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafAtom.Value;
				}

				// Token: 0x060073D8 RID: 29656 RVA: 0x00190820 File Offset: 0x0018EA20
				public atomExpr atomExpr(ProgramNode node)
				{
					atomExpr? atomExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr.CreateSafe(this._builders, node);
					if (atomExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol atomExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return atomExpr.Value;
				}

				// Token: 0x060073D9 RID: 29657 RVA: 0x00190874 File Offset: 0x0018EA74
				public literalExpr literalExpr(ProgramNode node)
				{
					literalExpr? literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr.CreateSafe(this._builders, node);
					if (literalExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol literalExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return literalExpr.Value;
				}

				// Token: 0x060073DA RID: 29658 RVA: 0x001908C8 File Offset: 0x0018EAC8
				public fexpr fexpr(ProgramNode node)
				{
					fexpr? fexpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr.CreateSafe(this._builders, node);
					if (fexpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fexpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fexpr.Value;
				}

				// Token: 0x060073DB RID: 29659 RVA: 0x0019091C File Offset: 0x0018EB1C
				public resultFields resultFields(ProgramNode node)
				{
					resultFields? resultFields = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields.CreateSafe(this._builders, node);
					if (resultFields == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultFields but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultFields.Value;
				}

				// Token: 0x060073DC RID: 29660 RVA: 0x00190970 File Offset: 0x0018EB70
				public singletonField singletonField(ProgramNode node)
				{
					singletonField? singletonField = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField.CreateSafe(this._builders, node);
					if (singletonField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol singletonField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singletonField.Value;
				}

				// Token: 0x060073DD RID: 29661 RVA: 0x001909C4 File Offset: 0x0018EBC4
				public fieldSubstring fieldSubstring(ProgramNode node)
				{
					fieldSubstring? fieldSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring.CreateSafe(this._builders, node);
					if (fieldSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fieldSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fieldSubstring.Value;
				}

				// Token: 0x060073DE RID: 29662 RVA: 0x00190A18 File Offset: 0x0018EC18
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs cs(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs? cs = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateSafe(this._builders, node);
					if (cs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cs.Value;
				}

				// Token: 0x060073DF RID: 29663 RVA: 0x00190A6C File Offset: 0x0018EC6C
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y y(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y? y = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateSafe(this._builders, node);
					if (y == null)
					{
						string text = "node";
						string text2 = "expected node for symbol y but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return y.Value;
				}

				// Token: 0x060073E0 RID: 29664 RVA: 0x00190AC0 File Offset: 0x0018ECC0
				public selectSubstring selectSubstring(ProgramNode node)
				{
					selectSubstring? selectSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring.CreateSafe(this._builders, node);
					if (selectSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectSubstring.Value;
				}

				// Token: 0x060073E1 RID: 29665 RVA: 0x00190B14 File Offset: 0x0018ED14
				public substringDisj substringDisj(ProgramNode node)
				{
					substringDisj? substringDisj = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj.CreateSafe(this._builders, node);
					if (substringDisj == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substringDisj but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substringDisj.Value;
				}

				// Token: 0x060073E2 RID: 29666 RVA: 0x00190B68 File Offset: 0x0018ED68
				public substring substring(ProgramNode node)
				{
					substring? substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring.Value;
				}

				// Token: 0x060073E3 RID: 29667 RVA: 0x00190BBC File Offset: 0x0018EDBC
				public resultTable resultTable(ProgramNode node)
				{
					resultTable? resultTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable.CreateSafe(this._builders, node);
					if (resultTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultTable.Value;
				}

				// Token: 0x060073E4 RID: 29668 RVA: 0x00190C10 File Offset: 0x0018EE10
				public columnSelectors columnSelectors(ProgramNode node)
				{
					columnSelectors? columnSelectors = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors.CreateSafe(this._builders, node);
					if (columnSelectors == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnSelectors but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnSelectors.Value;
				}

				// Token: 0x060073E5 RID: 29669 RVA: 0x00190C64 File Offset: 0x0018EE64
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name name(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name? name = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						string text = "node";
						string text2 = "expected node for symbol name but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return name.Value;
				}

				// Token: 0x060073E6 RID: 29670 RVA: 0x00190CB8 File Offset: 0x0018EEB8
				public value value(ProgramNode node)
				{
					value? value = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @value but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value.Value;
				}

				// Token: 0x060073E7 RID: 29671 RVA: 0x00190D0C File Offset: 0x0018EF0C
				public cssSelector cssSelector(ProgramNode node)
				{
					cssSelector? cssSelector = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector.CreateSafe(this._builders, node);
					if (cssSelector == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cssSelector but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cssSelector.Value;
				}

				// Token: 0x060073E8 RID: 29672 RVA: 0x00190D60 File Offset: 0x0018EF60
				public className className(ProgramNode node)
				{
					className? className = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className.CreateSafe(this._builders, node);
					if (className == null)
					{
						string text = "node";
						string text2 = "expected node for symbol className but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return className.Value;
				}

				// Token: 0x060073E9 RID: 29673 RVA: 0x00190DB4 File Offset: 0x0018EFB4
				public idName idName(ProgramNode node)
				{
					idName? idName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName.CreateSafe(this._builders, node);
					if (idName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idName.Value;
				}

				// Token: 0x060073EA RID: 29674 RVA: 0x00190E08 File Offset: 0x0018F008
				public nodeName nodeName(ProgramNode node)
				{
					nodeName? nodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName.CreateSafe(this._builders, node);
					if (nodeName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol nodeName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeName.Value;
				}

				// Token: 0x060073EB RID: 29675 RVA: 0x00190E5C File Offset: 0x0018F05C
				public propName propName(ProgramNode node)
				{
					propName? propName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName.CreateSafe(this._builders, node);
					if (propName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol propName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return propName.Value;
				}

				// Token: 0x060073EC RID: 29676 RVA: 0x00190EB0 File Offset: 0x0018F0B0
				public idx1 idx1(ProgramNode node)
				{
					idx1? idx = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1.CreateSafe(this._builders, node);
					if (idx == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idx1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idx.Value;
				}

				// Token: 0x060073ED RID: 29677 RVA: 0x00190F04 File Offset: 0x0018F104
				public idx2 idx2(ProgramNode node)
				{
					idx2? idx = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2.CreateSafe(this._builders, node);
					if (idx == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idx2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idx.Value;
				}

				// Token: 0x060073EE RID: 29678 RVA: 0x00190F58 File Offset: 0x0018F158
				public names names(ProgramNode node)
				{
					names? names = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names.CreateSafe(this._builders, node);
					if (names == null)
					{
						string text = "node";
						string text2 = "expected node for symbol names but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return names.Value;
				}

				// Token: 0x060073EF RID: 29679 RVA: 0x00190FAC File Offset: 0x0018F1AC
				public count count(ProgramNode node)
				{
					count? count = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count.CreateSafe(this._builders, node);
					if (count == null)
					{
						string text = "node";
						string text2 = "expected node for symbol count but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return count.Value;
				}

				// Token: 0x060073F0 RID: 29680 RVA: 0x00191000 File Offset: 0x0018F200
				public substringFeatureNames substringFeatureNames(ProgramNode node)
				{
					substringFeatureNames? substringFeatureNames = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames.CreateSafe(this._builders, node);
					if (substringFeatureNames == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substringFeatureNames but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substringFeatureNames.Value;
				}

				// Token: 0x060073F1 RID: 29681 RVA: 0x00191054 File Offset: 0x0018F254
				public substringFeatureValues substringFeatureValues(ProgramNode node)
				{
					substringFeatureValues? substringFeatureValues = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues.CreateSafe(this._builders, node);
					if (substringFeatureValues == null)
					{
						string text = "node";
						string text2 = "expected node for symbol substringFeatureValues but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substringFeatureValues.Value;
				}

				// Token: 0x060073F2 RID: 29682 RVA: 0x001910A8 File Offset: 0x0018F2A8
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k k(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k? k = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x060073F3 RID: 29683 RVA: 0x001910FC File Offset: 0x0018F2FC
				public entityObjs entityObjs(ProgramNode node)
				{
					entityObjs? entityObjs = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs.CreateSafe(this._builders, node);
					if (entityObjs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol entityObjs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return entityObjs.Value;
				}

				// Token: 0x060073F4 RID: 29684 RVA: 0x00191150 File Offset: 0x0018F350
				public direction direction(ProgramNode node)
				{
					direction? direction = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction.CreateSafe(this._builders, node);
					if (direction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol direction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return direction.Value;
				}

				// Token: 0x060073F5 RID: 29685 RVA: 0x001911A4 File Offset: 0x0018F3A4
				public nodeCollection nodeCollection(ProgramNode node)
				{
					nodeCollection? nodeCollection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection.CreateSafe(this._builders, node);
					if (nodeCollection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol nodeCollection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeCollection.Value;
				}

				// Token: 0x060073F6 RID: 29686 RVA: 0x001911F8 File Offset: 0x0018F3F8
				public gen_NthChild gen_NthChild(ProgramNode node)
				{
					gen_NthChild? gen_NthChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild.CreateSafe(this._builders, node);
					if (gen_NthChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_NthChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NthChild.Value;
				}

				// Token: 0x060073F7 RID: 29687 RVA: 0x0019124C File Offset: 0x0018F44C
				public gen_NthLastChild gen_NthLastChild(ProgramNode node)
				{
					gen_NthLastChild? gen_NthLastChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild.CreateSafe(this._builders, node);
					if (gen_NthLastChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_NthLastChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NthLastChild.Value;
				}

				// Token: 0x060073F8 RID: 29688 RVA: 0x001912A0 File Offset: 0x0018F4A0
				public gen_Class gen_Class(ProgramNode node)
				{
					gen_Class? gen_Class = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class.CreateSafe(this._builders, node);
					if (gen_Class == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_Class but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_Class.Value;
				}

				// Token: 0x060073F9 RID: 29689 RVA: 0x001912F4 File Offset: 0x0018F4F4
				public gen_ID gen_ID(ProgramNode node)
				{
					gen_ID? gen_ID = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID.CreateSafe(this._builders, node);
					if (gen_ID == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_ID but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_ID.Value;
				}

				// Token: 0x060073FA RID: 29690 RVA: 0x00191348 File Offset: 0x0018F548
				public gen_NodeName gen_NodeName(ProgramNode node)
				{
					gen_NodeName? gen_NodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName.CreateSafe(this._builders, node);
					if (gen_NodeName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_NodeName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NodeName.Value;
				}

				// Token: 0x060073FB RID: 29691 RVA: 0x0019139C File Offset: 0x0018F59C
				public gen_ItemProp gen_ItemProp(ProgramNode node)
				{
					gen_ItemProp? gen_ItemProp = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp.CreateSafe(this._builders, node);
					if (gen_ItemProp == null)
					{
						string text = "node";
						string text2 = "expected node for symbol gen_ItemProp but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_ItemProp.Value;
				}

				// Token: 0x060073FC RID: 29692 RVA: 0x001913F0 File Offset: 0x0018F5F0
				public obj obj(ProgramNode node)
				{
					obj? obj = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj.CreateSafe(this._builders, node);
					if (obj == null)
					{
						string text = "node";
						string text2 = "expected node for symbol obj but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return obj.Value;
				}

				// Token: 0x060073FD RID: 29693 RVA: 0x00191444 File Offset: 0x0018F644
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0400326C RID: 12908
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FEC RID: 4076
			public class RuleCast
			{
				// Token: 0x060073FE RID: 29694 RVA: 0x00191495 File Offset: 0x0018F695
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060073FF RID: 29695 RVA: 0x001914A4 File Offset: 0x0018F6A4
				public resultSequence_subNodeSequence resultSequence_subNodeSequence(ProgramNode node)
				{
					resultSequence_subNodeSequence? resultSequence_subNodeSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_subNodeSequence.CreateSafe(this._builders, node);
					if (resultSequence_subNodeSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultSequence_subNodeSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultSequence_subNodeSequence.Value;
				}

				// Token: 0x06007400 RID: 29696 RVA: 0x001914F8 File Offset: 0x0018F6F8
				public resultSequence_regionSequence resultSequence_regionSequence(ProgramNode node)
				{
					resultSequence_regionSequence? resultSequence_regionSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_regionSequence.CreateSafe(this._builders, node);
					if (resultSequence_regionSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultSequence_regionSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultSequence_regionSequence.Value;
				}

				// Token: 0x06007401 RID: 29697 RVA: 0x0019154C File Offset: 0x0018F74C
				public ConvertToWebRegions ConvertToWebRegions(ProgramNode node)
				{
					ConvertToWebRegions? convertToWebRegions = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ConvertToWebRegions.CreateSafe(this._builders, node);
					if (convertToWebRegions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConvertToWebRegions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return convertToWebRegions.Value;
				}

				// Token: 0x06007402 RID: 29698 RVA: 0x001915A0 File Offset: 0x0018F7A0
				public Union Union(ProgramNode node)
				{
					Union? union = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Union.CreateSafe(this._builders, node);
					if (union == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Union but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return union.Value;
				}

				// Token: 0x06007403 RID: 29699 RVA: 0x001915F4 File Offset: 0x0018F7F4
				public EmptySequence EmptySequence(ProgramNode node)
				{
					EmptySequence? emptySequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.EmptySequence.CreateSafe(this._builders, node);
					if (emptySequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EmptySequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return emptySequence.Value;
				}

				// Token: 0x06007404 RID: 29700 RVA: 0x00191648 File Offset: 0x0018F848
				public resultRegion_subNode resultRegion_subNode(ProgramNode node)
				{
					resultRegion_subNode? resultRegion_subNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_subNode.CreateSafe(this._builders, node);
					if (resultRegion_subNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultRegion_subNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultRegion_subNode.Value;
				}

				// Token: 0x06007405 RID: 29701 RVA: 0x0019169C File Offset: 0x0018F89C
				public resultRegion_region resultRegion_region(ProgramNode node)
				{
					resultRegion_region? resultRegion_region = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_region.CreateSafe(this._builders, node);
					if (resultRegion_region == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultRegion_region but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultRegion_region.Value;
				}

				// Token: 0x06007406 RID: 29702 RVA: 0x001916F0 File Offset: 0x0018F8F0
				public MapToWebRegion MapToWebRegion(ProgramNode node)
				{
					MapToWebRegion? mapToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.MapToWebRegion.CreateSafe(this._builders, node);
					if (mapToWebRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MapToWebRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return mapToWebRegion.Value;
				}

				// Token: 0x06007407 RID: 29703 RVA: 0x00191744 File Offset: 0x0018F944
				public NodeToWebRegion NodeToWebRegion(ProgramNode node)
				{
					NodeToWebRegion? nodeToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegion.CreateSafe(this._builders, node);
					if (nodeToWebRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeToWebRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeToWebRegion.Value;
				}

				// Token: 0x06007408 RID: 29704 RVA: 0x00191798 File Offset: 0x0018F998
				public NodeToWebRegionInSequence NodeToWebRegionInSequence(ProgramNode node)
				{
					NodeToWebRegionInSequence? nodeToWebRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegionInSequence.CreateSafe(this._builders, node);
					if (nodeToWebRegionInSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeToWebRegionInSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeToWebRegionInSequence.Value;
				}

				// Token: 0x06007409 RID: 29705 RVA: 0x001917EC File Offset: 0x0018F9EC
				public FindEndNode FindEndNode(ProgramNode node)
				{
					FindEndNode? findEndNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FindEndNode.CreateSafe(this._builders, node);
					if (findEndNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FindEndNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return findEndNode.Value;
				}

				// Token: 0x0600740A RID: 29706 RVA: 0x00191840 File Offset: 0x0018FA40
				public NodeRegionToWebRegion NodeRegionToWebRegion(ProgramNode node)
				{
					NodeRegionToWebRegion? nodeRegionToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegion.CreateSafe(this._builders, node);
					if (nodeRegionToWebRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeRegionToWebRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeRegionToWebRegion.Value;
				}

				// Token: 0x0600740B RID: 29707 RVA: 0x00191894 File Offset: 0x0018FA94
				public LetRegion LetRegion(ProgramNode node)
				{
					LetRegion? letRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetRegion.CreateSafe(this._builders, node);
					if (letRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letRegion.Value;
				}

				// Token: 0x0600740C RID: 29708 RVA: 0x001918E8 File Offset: 0x0018FAE8
				public NodeRegionToWebRegionInSequence NodeRegionToWebRegionInSequence(ProgramNode node)
				{
					NodeRegionToWebRegionInSequence? nodeRegionToWebRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegionInSequence.CreateSafe(this._builders, node);
					if (nodeRegionToWebRegionInSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeRegionToWebRegionInSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeRegionToWebRegionInSequence.Value;
				}

				// Token: 0x0600740D RID: 29709 RVA: 0x0019193C File Offset: 0x0018FB3C
				public KthNodeInSelection KthNodeInSelection(ProgramNode node)
				{
					KthNodeInSelection? kthNodeInSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNodeInSelection.CreateSafe(this._builders, node);
					if (kthNodeInSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthNodeInSelection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthNodeInSelection.Value;
				}

				// Token: 0x0600740E RID: 29710 RVA: 0x00191990 File Offset: 0x0018FB90
				public KthNode KthNode(ProgramNode node)
				{
					KthNode? kthNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNode.CreateSafe(this._builders, node);
					if (kthNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol KthNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kthNode.Value;
				}

				// Token: 0x0600740F RID: 29711 RVA: 0x001919E4 File Offset: 0x0018FBE4
				public SingleSelection1 SingleSelection1(ProgramNode node)
				{
					SingleSelection1? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection1.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSelection1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSelection.Value;
				}

				// Token: 0x06007410 RID: 29712 RVA: 0x00191A38 File Offset: 0x0018FC38
				public DisjSelection1 DisjSelection1(ProgramNode node)
				{
					DisjSelection1? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection1.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSelection1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSelection.Value;
				}

				// Token: 0x06007411 RID: 29713 RVA: 0x00191A8C File Offset: 0x0018FC8C
				public CSSSelection CSSSelection(ProgramNode node)
				{
					CSSSelection? cssselection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.CSSSelection.CreateSafe(this._builders, node);
					if (cssselection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol CSSSelection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cssselection.Value;
				}

				// Token: 0x06007412 RID: 29714 RVA: 0x00191AE0 File Offset: 0x0018FCE0
				public LeafFilter1 LeafFilter1(ProgramNode node)
				{
					LeafFilter1? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter1.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafFilter1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFilter.Value;
				}

				// Token: 0x06007413 RID: 29715 RVA: 0x00191B34 File Offset: 0x0018FD34
				public FilterNodesEnd FilterNodesEnd(ProgramNode node)
				{
					FilterNodesEnd? filterNodesEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FilterNodesEnd.CreateSafe(this._builders, node);
					if (filterNodesEnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FilterNodesEnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return filterNodesEnd.Value;
				}

				// Token: 0x06007414 RID: 29716 RVA: 0x00191B88 File Offset: 0x0018FD88
				public TakeWhileNodesEnd TakeWhileNodesEnd(ProgramNode node)
				{
					TakeWhileNodesEnd? takeWhileNodesEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TakeWhileNodesEnd.CreateSafe(this._builders, node);
					if (takeWhileNodesEnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TakeWhileNodesEnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return takeWhileNodesEnd.Value;
				}

				// Token: 0x06007415 RID: 29717 RVA: 0x00191BDC File Offset: 0x0018FDDC
				public selectionEnd_regionStartSiblings selectionEnd_regionStartSiblings(ProgramNode node)
				{
					selectionEnd_regionStartSiblings? selectionEnd_regionStartSiblings = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selectionEnd_regionStartSiblings.CreateSafe(this._builders, node);
					if (selectionEnd_regionStartSiblings == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectionEnd_regionStartSiblings but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectionEnd_regionStartSiblings.Value;
				}

				// Token: 0x06007416 RID: 29718 RVA: 0x00191C30 File Offset: 0x0018FE30
				public YoungerSiblingsOf YoungerSiblingsOf(ProgramNode node)
				{
					YoungerSiblingsOf? youngerSiblingsOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.YoungerSiblingsOf.CreateSafe(this._builders, node);
					if (youngerSiblingsOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol YoungerSiblingsOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return youngerSiblingsOf.Value;
				}

				// Token: 0x06007417 RID: 29719 RVA: 0x00191C84 File Offset: 0x0018FE84
				public LeafChildrenOf1 LeafChildrenOf1(ProgramNode node)
				{
					LeafChildrenOf1? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf1.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafChildrenOf1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafChildrenOf.Value;
				}

				// Token: 0x06007418 RID: 29720 RVA: 0x00191CD8 File Offset: 0x0018FED8
				public selection2_allNodes selection2_allNodes(ProgramNode node)
				{
					selection2_allNodes? selection2_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection2_allNodes.CreateSafe(this._builders, node);
					if (selection2_allNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection2_allNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection2_allNodes.Value;
				}

				// Token: 0x06007419 RID: 29721 RVA: 0x00191D2C File Offset: 0x0018FF2C
				public SingleSelection2 SingleSelection2(ProgramNode node)
				{
					SingleSelection2? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection2.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSelection2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSelection.Value;
				}

				// Token: 0x0600741A RID: 29722 RVA: 0x00191D80 File Offset: 0x0018FF80
				public DisjSelection2 DisjSelection2(ProgramNode node)
				{
					DisjSelection2? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection2.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSelection2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSelection.Value;
				}

				// Token: 0x0600741B RID: 29723 RVA: 0x00191DD4 File Offset: 0x0018FFD4
				public LeafFilter2 LeafFilter2(ProgramNode node)
				{
					LeafFilter2? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter2.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafFilter2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFilter.Value;
				}

				// Token: 0x0600741C RID: 29724 RVA: 0x00191E28 File Offset: 0x00190028
				public LeafChildrenOf2 LeafChildrenOf2(ProgramNode node)
				{
					LeafChildrenOf2? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf2.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafChildrenOf2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafChildrenOf.Value;
				}

				// Token: 0x0600741D RID: 29725 RVA: 0x00191E7C File Offset: 0x0019007C
				public selection4_allNodes selection4_allNodes(ProgramNode node)
				{
					selection4_allNodes? selection4_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection4_allNodes.CreateSafe(this._builders, node);
					if (selection4_allNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection4_allNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection4_allNodes.Value;
				}

				// Token: 0x0600741E RID: 29726 RVA: 0x00191ED0 File Offset: 0x001900D0
				public SingleSelection3 SingleSelection3(ProgramNode node)
				{
					SingleSelection3? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection3.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSelection3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSelection.Value;
				}

				// Token: 0x0600741F RID: 29727 RVA: 0x00191F24 File Offset: 0x00190124
				public DisjSelection3 DisjSelection3(ProgramNode node)
				{
					DisjSelection3? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection3.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSelection3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSelection.Value;
				}

				// Token: 0x06007420 RID: 29728 RVA: 0x00191F78 File Offset: 0x00190178
				public LeafFilter3 LeafFilter3(ProgramNode node)
				{
					LeafFilter3? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter3.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafFilter3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFilter.Value;
				}

				// Token: 0x06007421 RID: 29729 RVA: 0x00191FCC File Offset: 0x001901CC
				public LeafChildrenOf3 LeafChildrenOf3(ProgramNode node)
				{
					LeafChildrenOf3? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf3.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafChildrenOf3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafChildrenOf.Value;
				}

				// Token: 0x06007422 RID: 29730 RVA: 0x00192020 File Offset: 0x00190220
				public selection6_allNodes selection6_allNodes(ProgramNode node)
				{
					selection6_allNodes? selection6_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection6_allNodes.CreateSafe(this._builders, node);
					if (selection6_allNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection6_allNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection6_allNodes.Value;
				}

				// Token: 0x06007423 RID: 29731 RVA: 0x00192074 File Offset: 0x00190274
				public SingleSelection4 SingleSelection4(ProgramNode node)
				{
					SingleSelection4? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection4.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSelection4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSelection.Value;
				}

				// Token: 0x06007424 RID: 29732 RVA: 0x001920C8 File Offset: 0x001902C8
				public DisjSelection4 DisjSelection4(ProgramNode node)
				{
					DisjSelection4? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection4.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSelection4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSelection.Value;
				}

				// Token: 0x06007425 RID: 29733 RVA: 0x0019211C File Offset: 0x0019031C
				public LeafFilter4 LeafFilter4(ProgramNode node)
				{
					LeafFilter4? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter4.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafFilter4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFilter.Value;
				}

				// Token: 0x06007426 RID: 29734 RVA: 0x00192170 File Offset: 0x00190370
				public LeafChildrenOf4 LeafChildrenOf4(ProgramNode node)
				{
					LeafChildrenOf4? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf4.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafChildrenOf4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafChildrenOf.Value;
				}

				// Token: 0x06007427 RID: 29735 RVA: 0x001921C4 File Offset: 0x001903C4
				public selection8_allNodes selection8_allNodes(ProgramNode node)
				{
					selection8_allNodes? selection8_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection8_allNodes.CreateSafe(this._builders, node);
					if (selection8_allNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection8_allNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection8_allNodes.Value;
				}

				// Token: 0x06007428 RID: 29736 RVA: 0x00192218 File Offset: 0x00190418
				public SingleSelection5 SingleSelection5(ProgramNode node)
				{
					SingleSelection5? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection5.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSelection5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSelection.Value;
				}

				// Token: 0x06007429 RID: 29737 RVA: 0x0019226C File Offset: 0x0019046C
				public DisjSelection5 DisjSelection5(ProgramNode node)
				{
					DisjSelection5? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection5.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSelection5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSelection.Value;
				}

				// Token: 0x0600742A RID: 29738 RVA: 0x001922C0 File Offset: 0x001904C0
				public LeafFilter5 LeafFilter5(ProgramNode node)
				{
					LeafFilter5? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter5.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafFilter5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFilter.Value;
				}

				// Token: 0x0600742B RID: 29739 RVA: 0x00192314 File Offset: 0x00190514
				public selection10_allNodes selection10_allNodes(ProgramNode node)
				{
					selection10_allNodes? selection10_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection10_allNodes.CreateSafe(this._builders, node);
					if (selection10_allNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selection10_allNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selection10_allNodes.Value;
				}

				// Token: 0x0600742C RID: 29740 RVA: 0x00192368 File Offset: 0x00190568
				public leafFExpr_leafAtom leafFExpr_leafAtom(ProgramNode node)
				{
					leafFExpr_leafAtom? leafFExpr_leafAtom = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafFExpr_leafAtom.CreateSafe(this._builders, node);
					if (leafFExpr_leafAtom == null)
					{
						string text = "node";
						string text2 = "expected node for symbol leafFExpr_leafAtom but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafFExpr_leafAtom.Value;
				}

				// Token: 0x0600742D RID: 29741 RVA: 0x001923BC File Offset: 0x001905BC
				public LeafAnd LeafAnd(ProgramNode node)
				{
					LeafAnd? leafAnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafAnd.CreateSafe(this._builders, node);
					if (leafAnd == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafAnd but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafAnd.Value;
				}

				// Token: 0x0600742E RID: 29742 RVA: 0x00192410 File Offset: 0x00190610
				public leafAtom_literalExpr leafAtom_literalExpr(ProgramNode node)
				{
					leafAtom_literalExpr? leafAtom_literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafAtom_literalExpr.CreateSafe(this._builders, node);
					if (leafAtom_literalExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol leafAtom_literalExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafAtom_literalExpr.Value;
				}

				// Token: 0x0600742F RID: 29743 RVA: 0x00192464 File Offset: 0x00190664
				public ContainsDate ContainsDate(ProgramNode node)
				{
					ContainsDate? containsDate = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsDate.CreateSafe(this._builders, node);
					if (containsDate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ContainsDate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsDate.Value;
				}

				// Token: 0x06007430 RID: 29744 RVA: 0x001924B8 File Offset: 0x001906B8
				public ContainsNum ContainsNum(ProgramNode node)
				{
					ContainsNum? containsNum = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsNum.CreateSafe(this._builders, node);
					if (containsNum == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ContainsNum but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsNum.Value;
				}

				// Token: 0x06007431 RID: 29745 RVA: 0x0019250C File Offset: 0x0019070C
				public ID_substring ID_substring(ProgramNode node)
				{
					ID_substring? id_substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ID_substring.CreateSafe(this._builders, node);
					if (id_substring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ID_substring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return id_substring.Value;
				}

				// Token: 0x06007432 RID: 29746 RVA: 0x00192560 File Offset: 0x00190760
				public Class Class(ProgramNode node)
				{
					Class? @class = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Class.CreateSafe(this._builders, node);
					if (@class == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Class but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @class.Value;
				}

				// Token: 0x06007433 RID: 29747 RVA: 0x001925B4 File Offset: 0x001907B4
				public TitleIs TitleIs(ProgramNode node)
				{
					TitleIs? titleIs = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TitleIs.CreateSafe(this._builders, node);
					if (titleIs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TitleIs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return titleIs.Value;
				}

				// Token: 0x06007434 RID: 29748 RVA: 0x00192608 File Offset: 0x00190808
				public NodeName NodeName(ProgramNode node)
				{
					NodeName? nodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeName.CreateSafe(this._builders, node);
					if (nodeName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeName.Value;
				}

				// Token: 0x06007435 RID: 29749 RVA: 0x0019265C File Offset: 0x0019085C
				public NodeNames NodeNames(ProgramNode node)
				{
					NodeNames? nodeNames = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNames.CreateSafe(this._builders, node);
					if (nodeNames == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeNames but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeNames.Value;
				}

				// Token: 0x06007436 RID: 29750 RVA: 0x001926B0 File Offset: 0x001908B0
				public NthChild NthChild(ProgramNode node)
				{
					NthChild? nthChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChild.CreateSafe(this._builders, node);
					if (nthChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NthChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nthChild.Value;
				}

				// Token: 0x06007437 RID: 29751 RVA: 0x00192704 File Offset: 0x00190904
				public NthLastChild NthLastChild(ProgramNode node)
				{
					NthLastChild? nthLastChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChild.CreateSafe(this._builders, node);
					if (nthLastChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NthLastChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nthLastChild.Value;
				}

				// Token: 0x06007438 RID: 29752 RVA: 0x00192758 File Offset: 0x00190958
				public ContainsLeafNodes ContainsLeafNodes(ProgramNode node)
				{
					ContainsLeafNodes? containsLeafNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsLeafNodes.CreateSafe(this._builders, node);
					if (containsLeafNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ContainsLeafNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsLeafNodes.Value;
				}

				// Token: 0x06007439 RID: 29753 RVA: 0x001927AC File Offset: 0x001909AC
				public ChildrenCount ChildrenCount(ProgramNode node)
				{
					ChildrenCount? childrenCount = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ChildrenCount.CreateSafe(this._builders, node);
					if (childrenCount == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ChildrenCount but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return childrenCount.Value;
				}

				// Token: 0x0600743A RID: 29754 RVA: 0x00192800 File Offset: 0x00190A00
				public HasAttribute HasAttribute(ProgramNode node)
				{
					HasAttribute? hasAttribute = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasAttribute.CreateSafe(this._builders, node);
					if (hasAttribute == null)
					{
						string text = "node";
						string text2 = "expected node for symbol HasAttribute but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasAttribute.Value;
				}

				// Token: 0x0600743B RID: 29755 RVA: 0x00192854 File Offset: 0x00190A54
				public HasStyle HasStyle(ProgramNode node)
				{
					HasStyle? hasStyle = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasStyle.CreateSafe(this._builders, node);
					if (hasStyle == null)
					{
						string text = "node";
						string text2 = "expected node for symbol HasStyle but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasStyle.Value;
				}

				// Token: 0x0600743C RID: 29756 RVA: 0x001928A8 File Offset: 0x00190AA8
				public HasEntityAnchor HasEntityAnchor(ProgramNode node)
				{
					HasEntityAnchor? hasEntityAnchor = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasEntityAnchor.CreateSafe(this._builders, node);
					if (hasEntityAnchor == null)
					{
						string text = "node";
						string text2 = "expected node for symbol HasEntityAnchor but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasEntityAnchor.Value;
				}

				// Token: 0x0600743D RID: 29757 RVA: 0x001928FC File Offset: 0x00190AFC
				public literalExpr_atomExpr literalExpr_atomExpr(ProgramNode node)
				{
					literalExpr_atomExpr? literalExpr_atomExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.literalExpr_atomExpr.CreateSafe(this._builders, node);
					if (literalExpr_atomExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol literalExpr_atomExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return literalExpr_atomExpr.Value;
				}

				// Token: 0x0600743E RID: 29758 RVA: 0x00192950 File Offset: 0x00190B50
				public fexpr_literalExpr fexpr_literalExpr(ProgramNode node)
				{
					fexpr_literalExpr? fexpr_literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.fexpr_literalExpr.CreateSafe(this._builders, node);
					if (fexpr_literalExpr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fexpr_literalExpr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fexpr_literalExpr.Value;
				}

				// Token: 0x0600743F RID: 29759 RVA: 0x001929A4 File Offset: 0x00190BA4
				public And And(ProgramNode node)
				{
					And? and = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.And.CreateSafe(this._builders, node);
					if (and == null)
					{
						string text = "node";
						string text2 = "expected node for symbol And but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return and.Value;
				}

				// Token: 0x06007440 RID: 29760 RVA: 0x001929F8 File Offset: 0x00190BF8
				public resultFields_singletonField resultFields_singletonField(ProgramNode node)
				{
					resultFields_singletonField? resultFields_singletonField = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultFields_singletonField.CreateSafe(this._builders, node);
					if (resultFields_singletonField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol resultFields_singletonField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return resultFields_singletonField.Value;
				}

				// Token: 0x06007441 RID: 29761 RVA: 0x00192A4C File Offset: 0x00190C4C
				public AppendField AppendField(ProgramNode node)
				{
					AppendField? appendField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AppendField.CreateSafe(this._builders, node);
					if (appendField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AppendField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return appendField.Value;
				}

				// Token: 0x06007442 RID: 29762 RVA: 0x00192AA0 File Offset: 0x00190CA0
				public TrimmedTextField TrimmedTextField(ProgramNode node)
				{
					TrimmedTextField? trimmedTextField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TrimmedTextField.CreateSafe(this._builders, node);
					if (trimmedTextField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TrimmedTextField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return trimmedTextField.Value;
				}

				// Token: 0x06007443 RID: 29763 RVA: 0x00192AF4 File Offset: 0x00190CF4
				public SubstringField SubstringField(ProgramNode node)
				{
					SubstringField? substringField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SubstringField.CreateSafe(this._builders, node);
					if (substringField == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SubstringField but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substringField.Value;
				}

				// Token: 0x06007444 RID: 29764 RVA: 0x00192B48 File Offset: 0x00190D48
				public LetSubstring LetSubstring(ProgramNode node)
				{
					LetSubstring? letSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetSubstring.CreateSafe(this._builders, node);
					if (letSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSubstring.Value;
				}

				// Token: 0x06007445 RID: 29765 RVA: 0x00192B9C File Offset: 0x00190D9C
				public GetValueSubstring GetValueSubstring(ProgramNode node)
				{
					GetValueSubstring? getValueSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GetValueSubstring.CreateSafe(this._builders, node);
					if (getValueSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GetValueSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return getValueSubstring.Value;
				}

				// Token: 0x06007446 RID: 29766 RVA: 0x00192BF0 File Offset: 0x00190DF0
				public SelectSubstring SelectSubstring(ProgramNode node)
				{
					SelectSubstring? selectSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SelectSubstring.CreateSafe(this._builders, node);
					if (selectSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectSubstring.Value;
				}

				// Token: 0x06007447 RID: 29767 RVA: 0x00192C44 File Offset: 0x00190E44
				public SingleSubstring SingleSubstring(ProgramNode node)
				{
					SingleSubstring? singleSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSubstring.CreateSafe(this._builders, node);
					if (singleSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleSubstring.Value;
				}

				// Token: 0x06007448 RID: 29768 RVA: 0x00192C98 File Offset: 0x00190E98
				public DisjSubstring DisjSubstring(ProgramNode node)
				{
					DisjSubstring? disjSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSubstring.CreateSafe(this._builders, node);
					if (disjSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DisjSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjSubstring.Value;
				}

				// Token: 0x06007449 RID: 29769 RVA: 0x00192CEC File Offset: 0x00190EEC
				public Substring Substring(ProgramNode node)
				{
					Substring? substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Substring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return substring.Value;
				}

				// Token: 0x0600744A RID: 29770 RVA: 0x00192D40 File Offset: 0x00190F40
				public ExtractTable ExtractTable(ProgramNode node)
				{
					ExtractTable? extractTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractTable.CreateSafe(this._builders, node);
					if (extractTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ExtractTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extractTable.Value;
				}

				// Token: 0x0600744B RID: 29771 RVA: 0x00192D94 File Offset: 0x00190F94
				public ExtractRowBasedTable ExtractRowBasedTable(ProgramNode node)
				{
					ExtractRowBasedTable? extractRowBasedTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractRowBasedTable.CreateSafe(this._builders, node);
					if (extractRowBasedTable == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ExtractRowBasedTable but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return extractRowBasedTable.Value;
				}

				// Token: 0x0600744C RID: 29772 RVA: 0x00192DE8 File Offset: 0x00190FE8
				public SingleColumn SingleColumn(ProgramNode node)
				{
					SingleColumn? singleColumn = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleColumn.CreateSafe(this._builders, node);
					if (singleColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleColumn.Value;
				}

				// Token: 0x0600744D RID: 29773 RVA: 0x00192E3C File Offset: 0x0019103C
				public ColumnSequence ColumnSequence(ProgramNode node)
				{
					ColumnSequence? columnSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ColumnSequence.CreateSafe(this._builders, node);
					if (columnSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ColumnSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnSequence.Value;
				}

				// Token: 0x0600744E RID: 29774 RVA: 0x00192E90 File Offset: 0x00191090
				public AsCollection AsCollection(ProgramNode node)
				{
					AsCollection? asCollection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AsCollection.CreateSafe(this._builders, node);
					if (asCollection == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AsCollection but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return asCollection.Value;
				}

				// Token: 0x0600744F RID: 29775 RVA: 0x00192EE4 File Offset: 0x001910E4
				public DescendantsOf DescendantsOf(ProgramNode node)
				{
					DescendantsOf? descendantsOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DescendantsOf.CreateSafe(this._builders, node);
					if (descendantsOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DescendantsOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return descendantsOf.Value;
				}

				// Token: 0x06007450 RID: 29776 RVA: 0x00192F38 File Offset: 0x00191138
				public RightSiblingOf RightSiblingOf(ProgramNode node)
				{
					RightSiblingOf? rightSiblingOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.RightSiblingOf.CreateSafe(this._builders, node);
					if (rightSiblingOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RightSiblingOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rightSiblingOf.Value;
				}

				// Token: 0x06007451 RID: 29777 RVA: 0x00192F8C File Offset: 0x0019118C
				public ClassFilter ClassFilter(ProgramNode node)
				{
					ClassFilter? classFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ClassFilter.CreateSafe(this._builders, node);
					if (classFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ClassFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return classFilter.Value;
				}

				// Token: 0x06007452 RID: 29778 RVA: 0x00192FE0 File Offset: 0x001911E0
				public IDFilter IDFilter(ProgramNode node)
				{
					IDFilter? idfilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.IDFilter.CreateSafe(this._builders, node);
					if (idfilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IDFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idfilter.Value;
				}

				// Token: 0x06007453 RID: 29779 RVA: 0x00193034 File Offset: 0x00191234
				public NodeNameFilter NodeNameFilter(ProgramNode node)
				{
					NodeNameFilter? nodeNameFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNameFilter.CreateSafe(this._builders, node);
					if (nodeNameFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NodeNameFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nodeNameFilter.Value;
				}

				// Token: 0x06007454 RID: 29780 RVA: 0x00193088 File Offset: 0x00191288
				public ItemPropFilter ItemPropFilter(ProgramNode node)
				{
					ItemPropFilter? itemPropFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ItemPropFilter.CreateSafe(this._builders, node);
					if (itemPropFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ItemPropFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return itemPropFilter.Value;
				}

				// Token: 0x06007455 RID: 29781 RVA: 0x001930DC File Offset: 0x001912DC
				public NthChildFilter NthChildFilter(ProgramNode node)
				{
					NthChildFilter? nthChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChildFilter.CreateSafe(this._builders, node);
					if (nthChildFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NthChildFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nthChildFilter.Value;
				}

				// Token: 0x06007456 RID: 29782 RVA: 0x00193130 File Offset: 0x00191330
				public NthLastChildFilter NthLastChildFilter(ProgramNode node)
				{
					NthLastChildFilter? nthLastChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChildFilter.CreateSafe(this._builders, node);
					if (nthLastChildFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NthLastChildFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nthLastChildFilter.Value;
				}

				// Token: 0x06007457 RID: 29783 RVA: 0x00193184 File Offset: 0x00191384
				public GEN_NthChildFilter GEN_NthChildFilter(ProgramNode node)
				{
					GEN_NthChildFilter? gen_NthChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthChildFilter.CreateSafe(this._builders, node);
					if (gen_NthChildFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_NthChildFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NthChildFilter.Value;
				}

				// Token: 0x06007458 RID: 29784 RVA: 0x001931D8 File Offset: 0x001913D8
				public GEN_NthLastChildFilter GEN_NthLastChildFilter(ProgramNode node)
				{
					GEN_NthLastChildFilter? gen_NthLastChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthLastChildFilter.CreateSafe(this._builders, node);
					if (gen_NthLastChildFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_NthLastChildFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NthLastChildFilter.Value;
				}

				// Token: 0x06007459 RID: 29785 RVA: 0x0019322C File Offset: 0x0019142C
				public GEN_ClassFilter GEN_ClassFilter(ProgramNode node)
				{
					GEN_ClassFilter? gen_ClassFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ClassFilter.CreateSafe(this._builders, node);
					if (gen_ClassFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_ClassFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_ClassFilter.Value;
				}

				// Token: 0x0600745A RID: 29786 RVA: 0x00193280 File Offset: 0x00191480
				public GEN_IDFilter GEN_IDFilter(ProgramNode node)
				{
					GEN_IDFilter? gen_IDFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_IDFilter.CreateSafe(this._builders, node);
					if (gen_IDFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_IDFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_IDFilter.Value;
				}

				// Token: 0x0600745B RID: 29787 RVA: 0x001932D4 File Offset: 0x001914D4
				public GEN_NodeNameFilter GEN_NodeNameFilter(ProgramNode node)
				{
					GEN_NodeNameFilter? gen_NodeNameFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NodeNameFilter.CreateSafe(this._builders, node);
					if (gen_NodeNameFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_NodeNameFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_NodeNameFilter.Value;
				}

				// Token: 0x0600745C RID: 29788 RVA: 0x00193328 File Offset: 0x00191528
				public GEN_ItemPropFilter GEN_ItemPropFilter(ProgramNode node)
				{
					GEN_ItemPropFilter? gen_ItemPropFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ItemPropFilter.CreateSafe(this._builders, node);
					if (gen_ItemPropFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GEN_ItemPropFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return gen_ItemPropFilter.Value;
				}

				// Token: 0x0400326D RID: 12909
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FED RID: 4077
			public class NodeIs
			{
				// Token: 0x0600745D RID: 29789 RVA: 0x00193379 File Offset: 0x00191579
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600745E RID: 29790 RVA: 0x00193388 File Offset: 0x00191588
				public bool resultSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600745F RID: 29791 RVA: 0x001933AC File Offset: 0x001915AC
				public bool resultSequence(ProgramNode node, out resultSequence value)
				{
					resultSequence? resultSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence.CreateSafe(this._builders, node);
					if (resultSequence == null)
					{
						value = default(resultSequence);
						return false;
					}
					value = resultSequence.Value;
					return true;
				}

				// Token: 0x06007460 RID: 29792 RVA: 0x001933E8 File Offset: 0x001915E8
				public bool resultRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007461 RID: 29793 RVA: 0x0019340C File Offset: 0x0019160C
				public bool resultRegion(ProgramNode node, out resultRegion value)
				{
					resultRegion? resultRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion.CreateSafe(this._builders, node);
					if (resultRegion == null)
					{
						value = default(resultRegion);
						return false;
					}
					value = resultRegion.Value;
					return true;
				}

				// Token: 0x06007462 RID: 29794 RVA: 0x00193448 File Offset: 0x00191648
				public bool subNodeSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007463 RID: 29795 RVA: 0x0019346C File Offset: 0x0019166C
				public bool subNodeSequence(ProgramNode node, out subNodeSequence value)
				{
					subNodeSequence? subNodeSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence.CreateSafe(this._builders, node);
					if (subNodeSequence == null)
					{
						value = default(subNodeSequence);
						return false;
					}
					value = subNodeSequence.Value;
					return true;
				}

				// Token: 0x06007464 RID: 29796 RVA: 0x001934A8 File Offset: 0x001916A8
				public bool node(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007465 RID: 29797 RVA: 0x001934CC File Offset: 0x001916CC
				public bool node(ProgramNode node, out node value)
				{
					node? node2 = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node.CreateSafe(this._builders, node);
					if (node2 == null)
					{
						value = default(node);
						return false;
					}
					value = node2.Value;
					return true;
				}

				// Token: 0x06007466 RID: 29798 RVA: 0x00193508 File Offset: 0x00191708
				public bool subNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007467 RID: 29799 RVA: 0x0019352C File Offset: 0x0019172C
				public bool subNode(ProgramNode node, out subNode value)
				{
					subNode? subNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode.CreateSafe(this._builders, node);
					if (subNode == null)
					{
						value = default(subNode);
						return false;
					}
					value = subNode.Value;
					return true;
				}

				// Token: 0x06007468 RID: 29800 RVA: 0x00193568 File Offset: 0x00191768
				public bool mapNodeInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007469 RID: 29801 RVA: 0x0019358C File Offset: 0x0019178C
				public bool mapNodeInSequence(ProgramNode node, out mapNodeInSequence value)
				{
					mapNodeInSequence? mapNodeInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence.CreateSafe(this._builders, node);
					if (mapNodeInSequence == null)
					{
						value = default(mapNodeInSequence);
						return false;
					}
					value = mapNodeInSequence.Value;
					return true;
				}

				// Token: 0x0600746A RID: 29802 RVA: 0x001935C8 File Offset: 0x001917C8
				public bool regionSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600746B RID: 29803 RVA: 0x001935EC File Offset: 0x001917EC
				public bool regionSequence(ProgramNode node, out regionSequence value)
				{
					regionSequence? regionSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence.CreateSafe(this._builders, node);
					if (regionSequence == null)
					{
						value = default(regionSequence);
						return false;
					}
					value = regionSequence.Value;
					return true;
				}

				// Token: 0x0600746C RID: 29804 RVA: 0x00193628 File Offset: 0x00191828
				public bool regionStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600746D RID: 29805 RVA: 0x0019364C File Offset: 0x0019184C
				public bool regionStart(ProgramNode node, out regionStart value)
				{
					regionStart? regionStart = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart.CreateSafe(this._builders, node);
					if (regionStart == null)
					{
						value = default(regionStart);
						return false;
					}
					value = regionStart.Value;
					return true;
				}

				// Token: 0x0600746E RID: 29806 RVA: 0x00193688 File Offset: 0x00191888
				public bool region(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600746F RID: 29807 RVA: 0x001936AC File Offset: 0x001918AC
				public bool region(ProgramNode node, out region value)
				{
					region? region = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region.CreateSafe(this._builders, node);
					if (region == null)
					{
						value = default(region);
						return false;
					}
					value = region.Value;
					return true;
				}

				// Token: 0x06007470 RID: 29808 RVA: 0x001936E8 File Offset: 0x001918E8
				public bool mapRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007471 RID: 29809 RVA: 0x0019370C File Offset: 0x0019190C
				public bool mapRegionInSequence(ProgramNode node, out mapRegionInSequence value)
				{
					mapRegionInSequence? mapRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence.CreateSafe(this._builders, node);
					if (mapRegionInSequence == null)
					{
						value = default(mapRegionInSequence);
						return false;
					}
					value = mapRegionInSequence.Value;
					return true;
				}

				// Token: 0x06007472 RID: 29810 RVA: 0x00193748 File Offset: 0x00191948
				public bool beginNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007473 RID: 29811 RVA: 0x0019376C File Offset: 0x0019196C
				public bool beginNode(ProgramNode node, out beginNode value)
				{
					beginNode? beginNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode.CreateSafe(this._builders, node);
					if (beginNode == null)
					{
						value = default(beginNode);
						return false;
					}
					value = beginNode.Value;
					return true;
				}

				// Token: 0x06007474 RID: 29812 RVA: 0x001937A8 File Offset: 0x001919A8
				public bool endNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007475 RID: 29813 RVA: 0x001937CC File Offset: 0x001919CC
				public bool endNode(ProgramNode node, out endNode value)
				{
					endNode? endNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode.CreateSafe(this._builders, node);
					if (endNode == null)
					{
						value = default(endNode);
						return false;
					}
					value = endNode.Value;
					return true;
				}

				// Token: 0x06007476 RID: 29814 RVA: 0x00193808 File Offset: 0x00191A08
				public bool selection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007477 RID: 29815 RVA: 0x0019382C File Offset: 0x00191A2C
				public bool selection(ProgramNode node, out selection value)
				{
					selection? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007478 RID: 29816 RVA: 0x00193868 File Offset: 0x00191A68
				public bool filterSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007479 RID: 29817 RVA: 0x0019388C File Offset: 0x00191A8C
				public bool filterSelection(ProgramNode node, out filterSelection value)
				{
					filterSelection? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						value = default(filterSelection);
						return false;
					}
					value = filterSelection.Value;
					return true;
				}

				// Token: 0x0600747A RID: 29818 RVA: 0x001938C8 File Offset: 0x00191AC8
				public bool selectionEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600747B RID: 29819 RVA: 0x001938EC File Offset: 0x00191AEC
				public bool selectionEnd(ProgramNode node, out selectionEnd value)
				{
					selectionEnd? selectionEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd.CreateSafe(this._builders, node);
					if (selectionEnd == null)
					{
						value = default(selectionEnd);
						return false;
					}
					value = selectionEnd.Value;
					return true;
				}

				// Token: 0x0600747C RID: 29820 RVA: 0x00193928 File Offset: 0x00191B28
				public bool regionStartSiblings(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600747D RID: 29821 RVA: 0x0019394C File Offset: 0x00191B4C
				public bool regionStartSiblings(ProgramNode node, out regionStartSiblings value)
				{
					regionStartSiblings? regionStartSiblings = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings.CreateSafe(this._builders, node);
					if (regionStartSiblings == null)
					{
						value = default(regionStartSiblings);
						return false;
					}
					value = regionStartSiblings.Value;
					return true;
				}

				// Token: 0x0600747E RID: 29822 RVA: 0x00193988 File Offset: 0x00191B88
				public bool selection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600747F RID: 29823 RVA: 0x001939AC File Offset: 0x00191BAC
				public bool selection2(ProgramNode node, out selection2 value)
				{
					selection2? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection2);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007480 RID: 29824 RVA: 0x001939E8 File Offset: 0x00191BE8
				public bool selection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007481 RID: 29825 RVA: 0x00193A0C File Offset: 0x00191C0C
				public bool selection3(ProgramNode node, out selection3 value)
				{
					selection3? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection3);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007482 RID: 29826 RVA: 0x00193A48 File Offset: 0x00191C48
				public bool filterSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007483 RID: 29827 RVA: 0x00193A6C File Offset: 0x00191C6C
				public bool filterSelection2(ProgramNode node, out filterSelection2 value)
				{
					filterSelection2? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						value = default(filterSelection2);
						return false;
					}
					value = filterSelection.Value;
					return true;
				}

				// Token: 0x06007484 RID: 29828 RVA: 0x00193AA8 File Offset: 0x00191CA8
				public bool selection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007485 RID: 29829 RVA: 0x00193ACC File Offset: 0x00191CCC
				public bool selection4(ProgramNode node, out selection4 value)
				{
					selection4? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection4);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007486 RID: 29830 RVA: 0x00193B08 File Offset: 0x00191D08
				public bool selection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007487 RID: 29831 RVA: 0x00193B2C File Offset: 0x00191D2C
				public bool selection5(ProgramNode node, out selection5 value)
				{
					selection5? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection5);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007488 RID: 29832 RVA: 0x00193B68 File Offset: 0x00191D68
				public bool filterSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007489 RID: 29833 RVA: 0x00193B8C File Offset: 0x00191D8C
				public bool filterSelection3(ProgramNode node, out filterSelection3 value)
				{
					filterSelection3? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						value = default(filterSelection3);
						return false;
					}
					value = filterSelection.Value;
					return true;
				}

				// Token: 0x0600748A RID: 29834 RVA: 0x00193BC8 File Offset: 0x00191DC8
				public bool selection6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600748B RID: 29835 RVA: 0x00193BEC File Offset: 0x00191DEC
				public bool selection6(ProgramNode node, out selection6 value)
				{
					selection6? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection6);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x0600748C RID: 29836 RVA: 0x00193C28 File Offset: 0x00191E28
				public bool selection7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600748D RID: 29837 RVA: 0x00193C4C File Offset: 0x00191E4C
				public bool selection7(ProgramNode node, out selection7 value)
				{
					selection7? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection7);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x0600748E RID: 29838 RVA: 0x00193C88 File Offset: 0x00191E88
				public bool filterSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600748F RID: 29839 RVA: 0x00193CAC File Offset: 0x00191EAC
				public bool filterSelection4(ProgramNode node, out filterSelection4 value)
				{
					filterSelection4? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						value = default(filterSelection4);
						return false;
					}
					value = filterSelection.Value;
					return true;
				}

				// Token: 0x06007490 RID: 29840 RVA: 0x00193CE8 File Offset: 0x00191EE8
				public bool selection8(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007491 RID: 29841 RVA: 0x00193D0C File Offset: 0x00191F0C
				public bool selection8(ProgramNode node, out selection8 value)
				{
					selection8? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection8);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007492 RID: 29842 RVA: 0x00193D48 File Offset: 0x00191F48
				public bool selection9(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007493 RID: 29843 RVA: 0x00193D6C File Offset: 0x00191F6C
				public bool selection9(ProgramNode node, out selection9 value)
				{
					selection9? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection9);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007494 RID: 29844 RVA: 0x00193DA8 File Offset: 0x00191FA8
				public bool filterSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007495 RID: 29845 RVA: 0x00193DCC File Offset: 0x00191FCC
				public bool filterSelection5(ProgramNode node, out filterSelection5 value)
				{
					filterSelection5? filterSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5.CreateSafe(this._builders, node);
					if (filterSelection == null)
					{
						value = default(filterSelection5);
						return false;
					}
					value = filterSelection.Value;
					return true;
				}

				// Token: 0x06007496 RID: 29846 RVA: 0x00193E08 File Offset: 0x00192008
				public bool selection10(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007497 RID: 29847 RVA: 0x00193E2C File Offset: 0x0019202C
				public bool selection10(ProgramNode node, out selection10 value)
				{
					selection10? selection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10.CreateSafe(this._builders, node);
					if (selection == null)
					{
						value = default(selection10);
						return false;
					}
					value = selection.Value;
					return true;
				}

				// Token: 0x06007498 RID: 29848 RVA: 0x00193E68 File Offset: 0x00192068
				public bool leafFExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007499 RID: 29849 RVA: 0x00193E8C File Offset: 0x0019208C
				public bool leafFExpr(ProgramNode node, out leafFExpr value)
				{
					leafFExpr? leafFExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr.CreateSafe(this._builders, node);
					if (leafFExpr == null)
					{
						value = default(leafFExpr);
						return false;
					}
					value = leafFExpr.Value;
					return true;
				}

				// Token: 0x0600749A RID: 29850 RVA: 0x00193EC8 File Offset: 0x001920C8
				public bool leafAtom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600749B RID: 29851 RVA: 0x00193EEC File Offset: 0x001920EC
				public bool leafAtom(ProgramNode node, out leafAtom value)
				{
					leafAtom? leafAtom = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom.CreateSafe(this._builders, node);
					if (leafAtom == null)
					{
						value = default(leafAtom);
						return false;
					}
					value = leafAtom.Value;
					return true;
				}

				// Token: 0x0600749C RID: 29852 RVA: 0x00193F28 File Offset: 0x00192128
				public bool atomExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600749D RID: 29853 RVA: 0x00193F4C File Offset: 0x0019214C
				public bool atomExpr(ProgramNode node, out atomExpr value)
				{
					atomExpr? atomExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr.CreateSafe(this._builders, node);
					if (atomExpr == null)
					{
						value = default(atomExpr);
						return false;
					}
					value = atomExpr.Value;
					return true;
				}

				// Token: 0x0600749E RID: 29854 RVA: 0x00193F88 File Offset: 0x00192188
				public bool literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600749F RID: 29855 RVA: 0x00193FAC File Offset: 0x001921AC
				public bool literalExpr(ProgramNode node, out literalExpr value)
				{
					literalExpr? literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr.CreateSafe(this._builders, node);
					if (literalExpr == null)
					{
						value = default(literalExpr);
						return false;
					}
					value = literalExpr.Value;
					return true;
				}

				// Token: 0x060074A0 RID: 29856 RVA: 0x00193FE8 File Offset: 0x001921E8
				public bool fexpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074A1 RID: 29857 RVA: 0x0019400C File Offset: 0x0019220C
				public bool fexpr(ProgramNode node, out fexpr value)
				{
					fexpr? fexpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr.CreateSafe(this._builders, node);
					if (fexpr == null)
					{
						value = default(fexpr);
						return false;
					}
					value = fexpr.Value;
					return true;
				}

				// Token: 0x060074A2 RID: 29858 RVA: 0x00194048 File Offset: 0x00192248
				public bool resultFields(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074A3 RID: 29859 RVA: 0x0019406C File Offset: 0x0019226C
				public bool resultFields(ProgramNode node, out resultFields value)
				{
					resultFields? resultFields = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields.CreateSafe(this._builders, node);
					if (resultFields == null)
					{
						value = default(resultFields);
						return false;
					}
					value = resultFields.Value;
					return true;
				}

				// Token: 0x060074A4 RID: 29860 RVA: 0x001940A8 File Offset: 0x001922A8
				public bool singletonField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074A5 RID: 29861 RVA: 0x001940CC File Offset: 0x001922CC
				public bool singletonField(ProgramNode node, out singletonField value)
				{
					singletonField? singletonField = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField.CreateSafe(this._builders, node);
					if (singletonField == null)
					{
						value = default(singletonField);
						return false;
					}
					value = singletonField.Value;
					return true;
				}

				// Token: 0x060074A6 RID: 29862 RVA: 0x00194108 File Offset: 0x00192308
				public bool fieldSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074A7 RID: 29863 RVA: 0x0019412C File Offset: 0x0019232C
				public bool fieldSubstring(ProgramNode node, out fieldSubstring value)
				{
					fieldSubstring? fieldSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring.CreateSafe(this._builders, node);
					if (fieldSubstring == null)
					{
						value = default(fieldSubstring);
						return false;
					}
					value = fieldSubstring.Value;
					return true;
				}

				// Token: 0x060074A8 RID: 29864 RVA: 0x00194168 File Offset: 0x00192368
				public bool cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074A9 RID: 29865 RVA: 0x0019418C File Offset: 0x0019238C
				public bool cs(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs value)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs? cs = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateSafe(this._builders, node);
					if (cs == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs);
						return false;
					}
					value = cs.Value;
					return true;
				}

				// Token: 0x060074AA RID: 29866 RVA: 0x001941C8 File Offset: 0x001923C8
				public bool y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074AB RID: 29867 RVA: 0x001941EC File Offset: 0x001923EC
				public bool y(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y value)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y? y = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateSafe(this._builders, node);
					if (y == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y);
						return false;
					}
					value = y.Value;
					return true;
				}

				// Token: 0x060074AC RID: 29868 RVA: 0x00194228 File Offset: 0x00192428
				public bool selectSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074AD RID: 29869 RVA: 0x0019424C File Offset: 0x0019244C
				public bool selectSubstring(ProgramNode node, out selectSubstring value)
				{
					selectSubstring? selectSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring.CreateSafe(this._builders, node);
					if (selectSubstring == null)
					{
						value = default(selectSubstring);
						return false;
					}
					value = selectSubstring.Value;
					return true;
				}

				// Token: 0x060074AE RID: 29870 RVA: 0x00194288 File Offset: 0x00192488
				public bool substringDisj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074AF RID: 29871 RVA: 0x001942AC File Offset: 0x001924AC
				public bool substringDisj(ProgramNode node, out substringDisj value)
				{
					substringDisj? substringDisj = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj.CreateSafe(this._builders, node);
					if (substringDisj == null)
					{
						value = default(substringDisj);
						return false;
					}
					value = substringDisj.Value;
					return true;
				}

				// Token: 0x060074B0 RID: 29872 RVA: 0x001942E8 File Offset: 0x001924E8
				public bool substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074B1 RID: 29873 RVA: 0x0019430C File Offset: 0x0019250C
				public bool substring(ProgramNode node, out substring value)
				{
					substring? substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						value = default(substring);
						return false;
					}
					value = substring.Value;
					return true;
				}

				// Token: 0x060074B2 RID: 29874 RVA: 0x00194348 File Offset: 0x00192548
				public bool resultTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074B3 RID: 29875 RVA: 0x0019436C File Offset: 0x0019256C
				public bool resultTable(ProgramNode node, out resultTable value)
				{
					resultTable? resultTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable.CreateSafe(this._builders, node);
					if (resultTable == null)
					{
						value = default(resultTable);
						return false;
					}
					value = resultTable.Value;
					return true;
				}

				// Token: 0x060074B4 RID: 29876 RVA: 0x001943A8 File Offset: 0x001925A8
				public bool columnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074B5 RID: 29877 RVA: 0x001943CC File Offset: 0x001925CC
				public bool columnSelectors(ProgramNode node, out columnSelectors value)
				{
					columnSelectors? columnSelectors = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors.CreateSafe(this._builders, node);
					if (columnSelectors == null)
					{
						value = default(columnSelectors);
						return false;
					}
					value = columnSelectors.Value;
					return true;
				}

				// Token: 0x060074B6 RID: 29878 RVA: 0x00194408 File Offset: 0x00192608
				public bool name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074B7 RID: 29879 RVA: 0x0019442C File Offset: 0x0019262C
				public bool name(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name value)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name? name = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name);
						return false;
					}
					value = name.Value;
					return true;
				}

				// Token: 0x060074B8 RID: 29880 RVA: 0x00194468 File Offset: 0x00192668
				public bool value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074B9 RID: 29881 RVA: 0x0019448C File Offset: 0x0019268C
				public bool value(ProgramNode node, out value value)
				{
					value? value2 = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value2 == null)
					{
						value = default(value);
						return false;
					}
					value = value2.Value;
					return true;
				}

				// Token: 0x060074BA RID: 29882 RVA: 0x001944C8 File Offset: 0x001926C8
				public bool cssSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074BB RID: 29883 RVA: 0x001944EC File Offset: 0x001926EC
				public bool cssSelector(ProgramNode node, out cssSelector value)
				{
					cssSelector? cssSelector = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector.CreateSafe(this._builders, node);
					if (cssSelector == null)
					{
						value = default(cssSelector);
						return false;
					}
					value = cssSelector.Value;
					return true;
				}

				// Token: 0x060074BC RID: 29884 RVA: 0x00194528 File Offset: 0x00192728
				public bool className(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074BD RID: 29885 RVA: 0x0019454C File Offset: 0x0019274C
				public bool className(ProgramNode node, out className value)
				{
					className? className = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className.CreateSafe(this._builders, node);
					if (className == null)
					{
						value = default(className);
						return false;
					}
					value = className.Value;
					return true;
				}

				// Token: 0x060074BE RID: 29886 RVA: 0x00194588 File Offset: 0x00192788
				public bool idName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074BF RID: 29887 RVA: 0x001945AC File Offset: 0x001927AC
				public bool idName(ProgramNode node, out idName value)
				{
					idName? idName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName.CreateSafe(this._builders, node);
					if (idName == null)
					{
						value = default(idName);
						return false;
					}
					value = idName.Value;
					return true;
				}

				// Token: 0x060074C0 RID: 29888 RVA: 0x001945E8 File Offset: 0x001927E8
				public bool nodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074C1 RID: 29889 RVA: 0x0019460C File Offset: 0x0019280C
				public bool nodeName(ProgramNode node, out nodeName value)
				{
					nodeName? nodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName.CreateSafe(this._builders, node);
					if (nodeName == null)
					{
						value = default(nodeName);
						return false;
					}
					value = nodeName.Value;
					return true;
				}

				// Token: 0x060074C2 RID: 29890 RVA: 0x00194648 File Offset: 0x00192848
				public bool propName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074C3 RID: 29891 RVA: 0x0019466C File Offset: 0x0019286C
				public bool propName(ProgramNode node, out propName value)
				{
					propName? propName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName.CreateSafe(this._builders, node);
					if (propName == null)
					{
						value = default(propName);
						return false;
					}
					value = propName.Value;
					return true;
				}

				// Token: 0x060074C4 RID: 29892 RVA: 0x001946A8 File Offset: 0x001928A8
				public bool idx1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074C5 RID: 29893 RVA: 0x001946CC File Offset: 0x001928CC
				public bool idx1(ProgramNode node, out idx1 value)
				{
					idx1? idx = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1.CreateSafe(this._builders, node);
					if (idx == null)
					{
						value = default(idx1);
						return false;
					}
					value = idx.Value;
					return true;
				}

				// Token: 0x060074C6 RID: 29894 RVA: 0x00194708 File Offset: 0x00192908
				public bool idx2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074C7 RID: 29895 RVA: 0x0019472C File Offset: 0x0019292C
				public bool idx2(ProgramNode node, out idx2 value)
				{
					idx2? idx = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2.CreateSafe(this._builders, node);
					if (idx == null)
					{
						value = default(idx2);
						return false;
					}
					value = idx.Value;
					return true;
				}

				// Token: 0x060074C8 RID: 29896 RVA: 0x00194768 File Offset: 0x00192968
				public bool names(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074C9 RID: 29897 RVA: 0x0019478C File Offset: 0x0019298C
				public bool names(ProgramNode node, out names value)
				{
					names? names = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names.CreateSafe(this._builders, node);
					if (names == null)
					{
						value = default(names);
						return false;
					}
					value = names.Value;
					return true;
				}

				// Token: 0x060074CA RID: 29898 RVA: 0x001947C8 File Offset: 0x001929C8
				public bool count(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074CB RID: 29899 RVA: 0x001947EC File Offset: 0x001929EC
				public bool count(ProgramNode node, out count value)
				{
					count? count = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count.CreateSafe(this._builders, node);
					if (count == null)
					{
						value = default(count);
						return false;
					}
					value = count.Value;
					return true;
				}

				// Token: 0x060074CC RID: 29900 RVA: 0x00194828 File Offset: 0x00192A28
				public bool substringFeatureNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074CD RID: 29901 RVA: 0x0019484C File Offset: 0x00192A4C
				public bool substringFeatureNames(ProgramNode node, out substringFeatureNames value)
				{
					substringFeatureNames? substringFeatureNames = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames.CreateSafe(this._builders, node);
					if (substringFeatureNames == null)
					{
						value = default(substringFeatureNames);
						return false;
					}
					value = substringFeatureNames.Value;
					return true;
				}

				// Token: 0x060074CE RID: 29902 RVA: 0x00194888 File Offset: 0x00192A88
				public bool substringFeatureValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074CF RID: 29903 RVA: 0x001948AC File Offset: 0x00192AAC
				public bool substringFeatureValues(ProgramNode node, out substringFeatureValues value)
				{
					substringFeatureValues? substringFeatureValues = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues.CreateSafe(this._builders, node);
					if (substringFeatureValues == null)
					{
						value = default(substringFeatureValues);
						return false;
					}
					value = substringFeatureValues.Value;
					return true;
				}

				// Token: 0x060074D0 RID: 29904 RVA: 0x001948E8 File Offset: 0x00192AE8
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074D1 RID: 29905 RVA: 0x0019490C File Offset: 0x00192B0C
				public bool k(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k value)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k? k = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x060074D2 RID: 29906 RVA: 0x00194948 File Offset: 0x00192B48
				public bool entityObjs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074D3 RID: 29907 RVA: 0x0019496C File Offset: 0x00192B6C
				public bool entityObjs(ProgramNode node, out entityObjs value)
				{
					entityObjs? entityObjs = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs.CreateSafe(this._builders, node);
					if (entityObjs == null)
					{
						value = default(entityObjs);
						return false;
					}
					value = entityObjs.Value;
					return true;
				}

				// Token: 0x060074D4 RID: 29908 RVA: 0x001949A8 File Offset: 0x00192BA8
				public bool direction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074D5 RID: 29909 RVA: 0x001949CC File Offset: 0x00192BCC
				public bool direction(ProgramNode node, out direction value)
				{
					direction? direction = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction.CreateSafe(this._builders, node);
					if (direction == null)
					{
						value = default(direction);
						return false;
					}
					value = direction.Value;
					return true;
				}

				// Token: 0x060074D6 RID: 29910 RVA: 0x00194A08 File Offset: 0x00192C08
				public bool nodeCollection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074D7 RID: 29911 RVA: 0x00194A2C File Offset: 0x00192C2C
				public bool nodeCollection(ProgramNode node, out nodeCollection value)
				{
					nodeCollection? nodeCollection = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection.CreateSafe(this._builders, node);
					if (nodeCollection == null)
					{
						value = default(nodeCollection);
						return false;
					}
					value = nodeCollection.Value;
					return true;
				}

				// Token: 0x060074D8 RID: 29912 RVA: 0x00194A68 File Offset: 0x00192C68
				public bool gen_NthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074D9 RID: 29913 RVA: 0x00194A8C File Offset: 0x00192C8C
				public bool gen_NthChild(ProgramNode node, out gen_NthChild value)
				{
					gen_NthChild? gen_NthChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild.CreateSafe(this._builders, node);
					if (gen_NthChild == null)
					{
						value = default(gen_NthChild);
						return false;
					}
					value = gen_NthChild.Value;
					return true;
				}

				// Token: 0x060074DA RID: 29914 RVA: 0x00194AC8 File Offset: 0x00192CC8
				public bool gen_NthLastChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074DB RID: 29915 RVA: 0x00194AEC File Offset: 0x00192CEC
				public bool gen_NthLastChild(ProgramNode node, out gen_NthLastChild value)
				{
					gen_NthLastChild? gen_NthLastChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild.CreateSafe(this._builders, node);
					if (gen_NthLastChild == null)
					{
						value = default(gen_NthLastChild);
						return false;
					}
					value = gen_NthLastChild.Value;
					return true;
				}

				// Token: 0x060074DC RID: 29916 RVA: 0x00194B28 File Offset: 0x00192D28
				public bool gen_Class(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074DD RID: 29917 RVA: 0x00194B4C File Offset: 0x00192D4C
				public bool gen_Class(ProgramNode node, out gen_Class value)
				{
					gen_Class? gen_Class = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class.CreateSafe(this._builders, node);
					if (gen_Class == null)
					{
						value = default(gen_Class);
						return false;
					}
					value = gen_Class.Value;
					return true;
				}

				// Token: 0x060074DE RID: 29918 RVA: 0x00194B88 File Offset: 0x00192D88
				public bool gen_ID(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074DF RID: 29919 RVA: 0x00194BAC File Offset: 0x00192DAC
				public bool gen_ID(ProgramNode node, out gen_ID value)
				{
					gen_ID? gen_ID = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID.CreateSafe(this._builders, node);
					if (gen_ID == null)
					{
						value = default(gen_ID);
						return false;
					}
					value = gen_ID.Value;
					return true;
				}

				// Token: 0x060074E0 RID: 29920 RVA: 0x00194BE8 File Offset: 0x00192DE8
				public bool gen_NodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074E1 RID: 29921 RVA: 0x00194C0C File Offset: 0x00192E0C
				public bool gen_NodeName(ProgramNode node, out gen_NodeName value)
				{
					gen_NodeName? gen_NodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName.CreateSafe(this._builders, node);
					if (gen_NodeName == null)
					{
						value = default(gen_NodeName);
						return false;
					}
					value = gen_NodeName.Value;
					return true;
				}

				// Token: 0x060074E2 RID: 29922 RVA: 0x00194C48 File Offset: 0x00192E48
				public bool gen_ItemProp(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074E3 RID: 29923 RVA: 0x00194C6C File Offset: 0x00192E6C
				public bool gen_ItemProp(ProgramNode node, out gen_ItemProp value)
				{
					gen_ItemProp? gen_ItemProp = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp.CreateSafe(this._builders, node);
					if (gen_ItemProp == null)
					{
						value = default(gen_ItemProp);
						return false;
					}
					value = gen_ItemProp.Value;
					return true;
				}

				// Token: 0x060074E4 RID: 29924 RVA: 0x00194CA8 File Offset: 0x00192EA8
				public bool obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074E5 RID: 29925 RVA: 0x00194CCC File Offset: 0x00192ECC
				public bool obj(ProgramNode node, out obj value)
				{
					obj? obj = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj.CreateSafe(this._builders, node);
					if (obj == null)
					{
						value = default(obj);
						return false;
					}
					value = obj.Value;
					return true;
				}

				// Token: 0x060074E6 RID: 29926 RVA: 0x00194D08 File Offset: 0x00192F08
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074E7 RID: 29927 RVA: 0x00194D2C File Offset: 0x00192F2C
				public bool _LetB0(ProgramNode node, out Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0 value)
				{
					Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0400326E RID: 12910
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FEE RID: 4078
			public class RuleIs
			{
				// Token: 0x060074E8 RID: 29928 RVA: 0x00194D66 File Offset: 0x00192F66
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060074E9 RID: 29929 RVA: 0x00194D78 File Offset: 0x00192F78
				public bool resultSequence_subNodeSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_subNodeSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074EA RID: 29930 RVA: 0x00194D9C File Offset: 0x00192F9C
				public bool resultSequence_subNodeSequence(ProgramNode node, out resultSequence_subNodeSequence value)
				{
					resultSequence_subNodeSequence? resultSequence_subNodeSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_subNodeSequence.CreateSafe(this._builders, node);
					if (resultSequence_subNodeSequence == null)
					{
						value = default(resultSequence_subNodeSequence);
						return false;
					}
					value = resultSequence_subNodeSequence.Value;
					return true;
				}

				// Token: 0x060074EB RID: 29931 RVA: 0x00194DD8 File Offset: 0x00192FD8
				public bool resultSequence_regionSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_regionSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074EC RID: 29932 RVA: 0x00194DFC File Offset: 0x00192FFC
				public bool resultSequence_regionSequence(ProgramNode node, out resultSequence_regionSequence value)
				{
					resultSequence_regionSequence? resultSequence_regionSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_regionSequence.CreateSafe(this._builders, node);
					if (resultSequence_regionSequence == null)
					{
						value = default(resultSequence_regionSequence);
						return false;
					}
					value = resultSequence_regionSequence.Value;
					return true;
				}

				// Token: 0x060074ED RID: 29933 RVA: 0x00194E38 File Offset: 0x00193038
				public bool ConvertToWebRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ConvertToWebRegions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074EE RID: 29934 RVA: 0x00194E5C File Offset: 0x0019305C
				public bool ConvertToWebRegions(ProgramNode node, out ConvertToWebRegions value)
				{
					ConvertToWebRegions? convertToWebRegions = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ConvertToWebRegions.CreateSafe(this._builders, node);
					if (convertToWebRegions == null)
					{
						value = default(ConvertToWebRegions);
						return false;
					}
					value = convertToWebRegions.Value;
					return true;
				}

				// Token: 0x060074EF RID: 29935 RVA: 0x00194E98 File Offset: 0x00193098
				public bool Union(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Union.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074F0 RID: 29936 RVA: 0x00194EBC File Offset: 0x001930BC
				public bool Union(ProgramNode node, out Union value)
				{
					Union? union = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Union.CreateSafe(this._builders, node);
					if (union == null)
					{
						value = default(Union);
						return false;
					}
					value = union.Value;
					return true;
				}

				// Token: 0x060074F1 RID: 29937 RVA: 0x00194EF8 File Offset: 0x001930F8
				public bool EmptySequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.EmptySequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074F2 RID: 29938 RVA: 0x00194F1C File Offset: 0x0019311C
				public bool EmptySequence(ProgramNode node, out EmptySequence value)
				{
					EmptySequence? emptySequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.EmptySequence.CreateSafe(this._builders, node);
					if (emptySequence == null)
					{
						value = default(EmptySequence);
						return false;
					}
					value = emptySequence.Value;
					return true;
				}

				// Token: 0x060074F3 RID: 29939 RVA: 0x00194F58 File Offset: 0x00193158
				public bool resultRegion_subNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_subNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074F4 RID: 29940 RVA: 0x00194F7C File Offset: 0x0019317C
				public bool resultRegion_subNode(ProgramNode node, out resultRegion_subNode value)
				{
					resultRegion_subNode? resultRegion_subNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_subNode.CreateSafe(this._builders, node);
					if (resultRegion_subNode == null)
					{
						value = default(resultRegion_subNode);
						return false;
					}
					value = resultRegion_subNode.Value;
					return true;
				}

				// Token: 0x060074F5 RID: 29941 RVA: 0x00194FB8 File Offset: 0x001931B8
				public bool resultRegion_region(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_region.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074F6 RID: 29942 RVA: 0x00194FDC File Offset: 0x001931DC
				public bool resultRegion_region(ProgramNode node, out resultRegion_region value)
				{
					resultRegion_region? resultRegion_region = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_region.CreateSafe(this._builders, node);
					if (resultRegion_region == null)
					{
						value = default(resultRegion_region);
						return false;
					}
					value = resultRegion_region.Value;
					return true;
				}

				// Token: 0x060074F7 RID: 29943 RVA: 0x00195018 File Offset: 0x00193218
				public bool MapToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.MapToWebRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074F8 RID: 29944 RVA: 0x0019503C File Offset: 0x0019323C
				public bool MapToWebRegion(ProgramNode node, out MapToWebRegion value)
				{
					MapToWebRegion? mapToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.MapToWebRegion.CreateSafe(this._builders, node);
					if (mapToWebRegion == null)
					{
						value = default(MapToWebRegion);
						return false;
					}
					value = mapToWebRegion.Value;
					return true;
				}

				// Token: 0x060074F9 RID: 29945 RVA: 0x00195078 File Offset: 0x00193278
				public bool NodeToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074FA RID: 29946 RVA: 0x0019509C File Offset: 0x0019329C
				public bool NodeToWebRegion(ProgramNode node, out NodeToWebRegion value)
				{
					NodeToWebRegion? nodeToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegion.CreateSafe(this._builders, node);
					if (nodeToWebRegion == null)
					{
						value = default(NodeToWebRegion);
						return false;
					}
					value = nodeToWebRegion.Value;
					return true;
				}

				// Token: 0x060074FB RID: 29947 RVA: 0x001950D8 File Offset: 0x001932D8
				public bool NodeToWebRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegionInSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074FC RID: 29948 RVA: 0x001950FC File Offset: 0x001932FC
				public bool NodeToWebRegionInSequence(ProgramNode node, out NodeToWebRegionInSequence value)
				{
					NodeToWebRegionInSequence? nodeToWebRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegionInSequence.CreateSafe(this._builders, node);
					if (nodeToWebRegionInSequence == null)
					{
						value = default(NodeToWebRegionInSequence);
						return false;
					}
					value = nodeToWebRegionInSequence.Value;
					return true;
				}

				// Token: 0x060074FD RID: 29949 RVA: 0x00195138 File Offset: 0x00193338
				public bool FindEndNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FindEndNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060074FE RID: 29950 RVA: 0x0019515C File Offset: 0x0019335C
				public bool FindEndNode(ProgramNode node, out FindEndNode value)
				{
					FindEndNode? findEndNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FindEndNode.CreateSafe(this._builders, node);
					if (findEndNode == null)
					{
						value = default(FindEndNode);
						return false;
					}
					value = findEndNode.Value;
					return true;
				}

				// Token: 0x060074FF RID: 29951 RVA: 0x00195198 File Offset: 0x00193398
				public bool NodeRegionToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007500 RID: 29952 RVA: 0x001951BC File Offset: 0x001933BC
				public bool NodeRegionToWebRegion(ProgramNode node, out NodeRegionToWebRegion value)
				{
					NodeRegionToWebRegion? nodeRegionToWebRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegion.CreateSafe(this._builders, node);
					if (nodeRegionToWebRegion == null)
					{
						value = default(NodeRegionToWebRegion);
						return false;
					}
					value = nodeRegionToWebRegion.Value;
					return true;
				}

				// Token: 0x06007501 RID: 29953 RVA: 0x001951F8 File Offset: 0x001933F8
				public bool LetRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007502 RID: 29954 RVA: 0x0019521C File Offset: 0x0019341C
				public bool LetRegion(ProgramNode node, out LetRegion value)
				{
					LetRegion? letRegion = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetRegion.CreateSafe(this._builders, node);
					if (letRegion == null)
					{
						value = default(LetRegion);
						return false;
					}
					value = letRegion.Value;
					return true;
				}

				// Token: 0x06007503 RID: 29955 RVA: 0x00195258 File Offset: 0x00193458
				public bool NodeRegionToWebRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegionInSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007504 RID: 29956 RVA: 0x0019527C File Offset: 0x0019347C
				public bool NodeRegionToWebRegionInSequence(ProgramNode node, out NodeRegionToWebRegionInSequence value)
				{
					NodeRegionToWebRegionInSequence? nodeRegionToWebRegionInSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegionInSequence.CreateSafe(this._builders, node);
					if (nodeRegionToWebRegionInSequence == null)
					{
						value = default(NodeRegionToWebRegionInSequence);
						return false;
					}
					value = nodeRegionToWebRegionInSequence.Value;
					return true;
				}

				// Token: 0x06007505 RID: 29957 RVA: 0x001952B8 File Offset: 0x001934B8
				public bool KthNodeInSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNodeInSelection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007506 RID: 29958 RVA: 0x001952DC File Offset: 0x001934DC
				public bool KthNodeInSelection(ProgramNode node, out KthNodeInSelection value)
				{
					KthNodeInSelection? kthNodeInSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNodeInSelection.CreateSafe(this._builders, node);
					if (kthNodeInSelection == null)
					{
						value = default(KthNodeInSelection);
						return false;
					}
					value = kthNodeInSelection.Value;
					return true;
				}

				// Token: 0x06007507 RID: 29959 RVA: 0x00195318 File Offset: 0x00193518
				public bool KthNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007508 RID: 29960 RVA: 0x0019533C File Offset: 0x0019353C
				public bool KthNode(ProgramNode node, out KthNode value)
				{
					KthNode? kthNode = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNode.CreateSafe(this._builders, node);
					if (kthNode == null)
					{
						value = default(KthNode);
						return false;
					}
					value = kthNode.Value;
					return true;
				}

				// Token: 0x06007509 RID: 29961 RVA: 0x00195378 File Offset: 0x00193578
				public bool SingleSelection1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600750A RID: 29962 RVA: 0x0019539C File Offset: 0x0019359C
				public bool SingleSelection1(ProgramNode node, out SingleSelection1 value)
				{
					SingleSelection1? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection1.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						value = default(SingleSelection1);
						return false;
					}
					value = singleSelection.Value;
					return true;
				}

				// Token: 0x0600750B RID: 29963 RVA: 0x001953D8 File Offset: 0x001935D8
				public bool DisjSelection1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600750C RID: 29964 RVA: 0x001953FC File Offset: 0x001935FC
				public bool DisjSelection1(ProgramNode node, out DisjSelection1 value)
				{
					DisjSelection1? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection1.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						value = default(DisjSelection1);
						return false;
					}
					value = disjSelection.Value;
					return true;
				}

				// Token: 0x0600750D RID: 29965 RVA: 0x00195438 File Offset: 0x00193638
				public bool CSSSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.CSSSelection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600750E RID: 29966 RVA: 0x0019545C File Offset: 0x0019365C
				public bool CSSSelection(ProgramNode node, out CSSSelection value)
				{
					CSSSelection? cssselection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.CSSSelection.CreateSafe(this._builders, node);
					if (cssselection == null)
					{
						value = default(CSSSelection);
						return false;
					}
					value = cssselection.Value;
					return true;
				}

				// Token: 0x0600750F RID: 29967 RVA: 0x00195498 File Offset: 0x00193698
				public bool LeafFilter1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007510 RID: 29968 RVA: 0x001954BC File Offset: 0x001936BC
				public bool LeafFilter1(ProgramNode node, out LeafFilter1 value)
				{
					LeafFilter1? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter1.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						value = default(LeafFilter1);
						return false;
					}
					value = leafFilter.Value;
					return true;
				}

				// Token: 0x06007511 RID: 29969 RVA: 0x001954F8 File Offset: 0x001936F8
				public bool FilterNodesEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FilterNodesEnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007512 RID: 29970 RVA: 0x0019551C File Offset: 0x0019371C
				public bool FilterNodesEnd(ProgramNode node, out FilterNodesEnd value)
				{
					FilterNodesEnd? filterNodesEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FilterNodesEnd.CreateSafe(this._builders, node);
					if (filterNodesEnd == null)
					{
						value = default(FilterNodesEnd);
						return false;
					}
					value = filterNodesEnd.Value;
					return true;
				}

				// Token: 0x06007513 RID: 29971 RVA: 0x00195558 File Offset: 0x00193758
				public bool TakeWhileNodesEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TakeWhileNodesEnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007514 RID: 29972 RVA: 0x0019557C File Offset: 0x0019377C
				public bool TakeWhileNodesEnd(ProgramNode node, out TakeWhileNodesEnd value)
				{
					TakeWhileNodesEnd? takeWhileNodesEnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TakeWhileNodesEnd.CreateSafe(this._builders, node);
					if (takeWhileNodesEnd == null)
					{
						value = default(TakeWhileNodesEnd);
						return false;
					}
					value = takeWhileNodesEnd.Value;
					return true;
				}

				// Token: 0x06007515 RID: 29973 RVA: 0x001955B8 File Offset: 0x001937B8
				public bool selectionEnd_regionStartSiblings(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selectionEnd_regionStartSiblings.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007516 RID: 29974 RVA: 0x001955DC File Offset: 0x001937DC
				public bool selectionEnd_regionStartSiblings(ProgramNode node, out selectionEnd_regionStartSiblings value)
				{
					selectionEnd_regionStartSiblings? selectionEnd_regionStartSiblings = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selectionEnd_regionStartSiblings.CreateSafe(this._builders, node);
					if (selectionEnd_regionStartSiblings == null)
					{
						value = default(selectionEnd_regionStartSiblings);
						return false;
					}
					value = selectionEnd_regionStartSiblings.Value;
					return true;
				}

				// Token: 0x06007517 RID: 29975 RVA: 0x00195618 File Offset: 0x00193818
				public bool YoungerSiblingsOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.YoungerSiblingsOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007518 RID: 29976 RVA: 0x0019563C File Offset: 0x0019383C
				public bool YoungerSiblingsOf(ProgramNode node, out YoungerSiblingsOf value)
				{
					YoungerSiblingsOf? youngerSiblingsOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.YoungerSiblingsOf.CreateSafe(this._builders, node);
					if (youngerSiblingsOf == null)
					{
						value = default(YoungerSiblingsOf);
						return false;
					}
					value = youngerSiblingsOf.Value;
					return true;
				}

				// Token: 0x06007519 RID: 29977 RVA: 0x00195678 File Offset: 0x00193878
				public bool LeafChildrenOf1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600751A RID: 29978 RVA: 0x0019569C File Offset: 0x0019389C
				public bool LeafChildrenOf1(ProgramNode node, out LeafChildrenOf1 value)
				{
					LeafChildrenOf1? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf1.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						value = default(LeafChildrenOf1);
						return false;
					}
					value = leafChildrenOf.Value;
					return true;
				}

				// Token: 0x0600751B RID: 29979 RVA: 0x001956D8 File Offset: 0x001938D8
				public bool selection2_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection2_allNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600751C RID: 29980 RVA: 0x001956FC File Offset: 0x001938FC
				public bool selection2_allNodes(ProgramNode node, out selection2_allNodes value)
				{
					selection2_allNodes? selection2_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection2_allNodes.CreateSafe(this._builders, node);
					if (selection2_allNodes == null)
					{
						value = default(selection2_allNodes);
						return false;
					}
					value = selection2_allNodes.Value;
					return true;
				}

				// Token: 0x0600751D RID: 29981 RVA: 0x00195738 File Offset: 0x00193938
				public bool SingleSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600751E RID: 29982 RVA: 0x0019575C File Offset: 0x0019395C
				public bool SingleSelection2(ProgramNode node, out SingleSelection2 value)
				{
					SingleSelection2? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection2.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						value = default(SingleSelection2);
						return false;
					}
					value = singleSelection.Value;
					return true;
				}

				// Token: 0x0600751F RID: 29983 RVA: 0x00195798 File Offset: 0x00193998
				public bool DisjSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007520 RID: 29984 RVA: 0x001957BC File Offset: 0x001939BC
				public bool DisjSelection2(ProgramNode node, out DisjSelection2 value)
				{
					DisjSelection2? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection2.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						value = default(DisjSelection2);
						return false;
					}
					value = disjSelection.Value;
					return true;
				}

				// Token: 0x06007521 RID: 29985 RVA: 0x001957F8 File Offset: 0x001939F8
				public bool LeafFilter2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007522 RID: 29986 RVA: 0x0019581C File Offset: 0x00193A1C
				public bool LeafFilter2(ProgramNode node, out LeafFilter2 value)
				{
					LeafFilter2? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter2.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						value = default(LeafFilter2);
						return false;
					}
					value = leafFilter.Value;
					return true;
				}

				// Token: 0x06007523 RID: 29987 RVA: 0x00195858 File Offset: 0x00193A58
				public bool LeafChildrenOf2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007524 RID: 29988 RVA: 0x0019587C File Offset: 0x00193A7C
				public bool LeafChildrenOf2(ProgramNode node, out LeafChildrenOf2 value)
				{
					LeafChildrenOf2? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf2.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						value = default(LeafChildrenOf2);
						return false;
					}
					value = leafChildrenOf.Value;
					return true;
				}

				// Token: 0x06007525 RID: 29989 RVA: 0x001958B8 File Offset: 0x00193AB8
				public bool selection4_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection4_allNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007526 RID: 29990 RVA: 0x001958DC File Offset: 0x00193ADC
				public bool selection4_allNodes(ProgramNode node, out selection4_allNodes value)
				{
					selection4_allNodes? selection4_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection4_allNodes.CreateSafe(this._builders, node);
					if (selection4_allNodes == null)
					{
						value = default(selection4_allNodes);
						return false;
					}
					value = selection4_allNodes.Value;
					return true;
				}

				// Token: 0x06007527 RID: 29991 RVA: 0x00195918 File Offset: 0x00193B18
				public bool SingleSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007528 RID: 29992 RVA: 0x0019593C File Offset: 0x00193B3C
				public bool SingleSelection3(ProgramNode node, out SingleSelection3 value)
				{
					SingleSelection3? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection3.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						value = default(SingleSelection3);
						return false;
					}
					value = singleSelection.Value;
					return true;
				}

				// Token: 0x06007529 RID: 29993 RVA: 0x00195978 File Offset: 0x00193B78
				public bool DisjSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600752A RID: 29994 RVA: 0x0019599C File Offset: 0x00193B9C
				public bool DisjSelection3(ProgramNode node, out DisjSelection3 value)
				{
					DisjSelection3? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection3.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						value = default(DisjSelection3);
						return false;
					}
					value = disjSelection.Value;
					return true;
				}

				// Token: 0x0600752B RID: 29995 RVA: 0x001959D8 File Offset: 0x00193BD8
				public bool LeafFilter3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600752C RID: 29996 RVA: 0x001959FC File Offset: 0x00193BFC
				public bool LeafFilter3(ProgramNode node, out LeafFilter3 value)
				{
					LeafFilter3? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter3.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						value = default(LeafFilter3);
						return false;
					}
					value = leafFilter.Value;
					return true;
				}

				// Token: 0x0600752D RID: 29997 RVA: 0x00195A38 File Offset: 0x00193C38
				public bool LeafChildrenOf3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600752E RID: 29998 RVA: 0x00195A5C File Offset: 0x00193C5C
				public bool LeafChildrenOf3(ProgramNode node, out LeafChildrenOf3 value)
				{
					LeafChildrenOf3? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf3.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						value = default(LeafChildrenOf3);
						return false;
					}
					value = leafChildrenOf.Value;
					return true;
				}

				// Token: 0x0600752F RID: 29999 RVA: 0x00195A98 File Offset: 0x00193C98
				public bool selection6_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection6_allNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007530 RID: 30000 RVA: 0x00195ABC File Offset: 0x00193CBC
				public bool selection6_allNodes(ProgramNode node, out selection6_allNodes value)
				{
					selection6_allNodes? selection6_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection6_allNodes.CreateSafe(this._builders, node);
					if (selection6_allNodes == null)
					{
						value = default(selection6_allNodes);
						return false;
					}
					value = selection6_allNodes.Value;
					return true;
				}

				// Token: 0x06007531 RID: 30001 RVA: 0x00195AF8 File Offset: 0x00193CF8
				public bool SingleSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007532 RID: 30002 RVA: 0x00195B1C File Offset: 0x00193D1C
				public bool SingleSelection4(ProgramNode node, out SingleSelection4 value)
				{
					SingleSelection4? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection4.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						value = default(SingleSelection4);
						return false;
					}
					value = singleSelection.Value;
					return true;
				}

				// Token: 0x06007533 RID: 30003 RVA: 0x00195B58 File Offset: 0x00193D58
				public bool DisjSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007534 RID: 30004 RVA: 0x00195B7C File Offset: 0x00193D7C
				public bool DisjSelection4(ProgramNode node, out DisjSelection4 value)
				{
					DisjSelection4? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection4.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						value = default(DisjSelection4);
						return false;
					}
					value = disjSelection.Value;
					return true;
				}

				// Token: 0x06007535 RID: 30005 RVA: 0x00195BB8 File Offset: 0x00193DB8
				public bool LeafFilter4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007536 RID: 30006 RVA: 0x00195BDC File Offset: 0x00193DDC
				public bool LeafFilter4(ProgramNode node, out LeafFilter4 value)
				{
					LeafFilter4? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter4.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						value = default(LeafFilter4);
						return false;
					}
					value = leafFilter.Value;
					return true;
				}

				// Token: 0x06007537 RID: 30007 RVA: 0x00195C18 File Offset: 0x00193E18
				public bool LeafChildrenOf4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007538 RID: 30008 RVA: 0x00195C3C File Offset: 0x00193E3C
				public bool LeafChildrenOf4(ProgramNode node, out LeafChildrenOf4 value)
				{
					LeafChildrenOf4? leafChildrenOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf4.CreateSafe(this._builders, node);
					if (leafChildrenOf == null)
					{
						value = default(LeafChildrenOf4);
						return false;
					}
					value = leafChildrenOf.Value;
					return true;
				}

				// Token: 0x06007539 RID: 30009 RVA: 0x00195C78 File Offset: 0x00193E78
				public bool selection8_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection8_allNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600753A RID: 30010 RVA: 0x00195C9C File Offset: 0x00193E9C
				public bool selection8_allNodes(ProgramNode node, out selection8_allNodes value)
				{
					selection8_allNodes? selection8_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection8_allNodes.CreateSafe(this._builders, node);
					if (selection8_allNodes == null)
					{
						value = default(selection8_allNodes);
						return false;
					}
					value = selection8_allNodes.Value;
					return true;
				}

				// Token: 0x0600753B RID: 30011 RVA: 0x00195CD8 File Offset: 0x00193ED8
				public bool SingleSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600753C RID: 30012 RVA: 0x00195CFC File Offset: 0x00193EFC
				public bool SingleSelection5(ProgramNode node, out SingleSelection5 value)
				{
					SingleSelection5? singleSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection5.CreateSafe(this._builders, node);
					if (singleSelection == null)
					{
						value = default(SingleSelection5);
						return false;
					}
					value = singleSelection.Value;
					return true;
				}

				// Token: 0x0600753D RID: 30013 RVA: 0x00195D38 File Offset: 0x00193F38
				public bool DisjSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600753E RID: 30014 RVA: 0x00195D5C File Offset: 0x00193F5C
				public bool DisjSelection5(ProgramNode node, out DisjSelection5 value)
				{
					DisjSelection5? disjSelection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection5.CreateSafe(this._builders, node);
					if (disjSelection == null)
					{
						value = default(DisjSelection5);
						return false;
					}
					value = disjSelection.Value;
					return true;
				}

				// Token: 0x0600753F RID: 30015 RVA: 0x00195D98 File Offset: 0x00193F98
				public bool LeafFilter5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007540 RID: 30016 RVA: 0x00195DBC File Offset: 0x00193FBC
				public bool LeafFilter5(ProgramNode node, out LeafFilter5 value)
				{
					LeafFilter5? leafFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter5.CreateSafe(this._builders, node);
					if (leafFilter == null)
					{
						value = default(LeafFilter5);
						return false;
					}
					value = leafFilter.Value;
					return true;
				}

				// Token: 0x06007541 RID: 30017 RVA: 0x00195DF8 File Offset: 0x00193FF8
				public bool selection10_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection10_allNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007542 RID: 30018 RVA: 0x00195E1C File Offset: 0x0019401C
				public bool selection10_allNodes(ProgramNode node, out selection10_allNodes value)
				{
					selection10_allNodes? selection10_allNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection10_allNodes.CreateSafe(this._builders, node);
					if (selection10_allNodes == null)
					{
						value = default(selection10_allNodes);
						return false;
					}
					value = selection10_allNodes.Value;
					return true;
				}

				// Token: 0x06007543 RID: 30019 RVA: 0x00195E58 File Offset: 0x00194058
				public bool leafFExpr_leafAtom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafFExpr_leafAtom.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007544 RID: 30020 RVA: 0x00195E7C File Offset: 0x0019407C
				public bool leafFExpr_leafAtom(ProgramNode node, out leafFExpr_leafAtom value)
				{
					leafFExpr_leafAtom? leafFExpr_leafAtom = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafFExpr_leafAtom.CreateSafe(this._builders, node);
					if (leafFExpr_leafAtom == null)
					{
						value = default(leafFExpr_leafAtom);
						return false;
					}
					value = leafFExpr_leafAtom.Value;
					return true;
				}

				// Token: 0x06007545 RID: 30021 RVA: 0x00195EB8 File Offset: 0x001940B8
				public bool LeafAnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafAnd.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007546 RID: 30022 RVA: 0x00195EDC File Offset: 0x001940DC
				public bool LeafAnd(ProgramNode node, out LeafAnd value)
				{
					LeafAnd? leafAnd = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafAnd.CreateSafe(this._builders, node);
					if (leafAnd == null)
					{
						value = default(LeafAnd);
						return false;
					}
					value = leafAnd.Value;
					return true;
				}

				// Token: 0x06007547 RID: 30023 RVA: 0x00195F18 File Offset: 0x00194118
				public bool leafAtom_literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafAtom_literalExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007548 RID: 30024 RVA: 0x00195F3C File Offset: 0x0019413C
				public bool leafAtom_literalExpr(ProgramNode node, out leafAtom_literalExpr value)
				{
					leafAtom_literalExpr? leafAtom_literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafAtom_literalExpr.CreateSafe(this._builders, node);
					if (leafAtom_literalExpr == null)
					{
						value = default(leafAtom_literalExpr);
						return false;
					}
					value = leafAtom_literalExpr.Value;
					return true;
				}

				// Token: 0x06007549 RID: 30025 RVA: 0x00195F78 File Offset: 0x00194178
				public bool ContainsDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsDate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600754A RID: 30026 RVA: 0x00195F9C File Offset: 0x0019419C
				public bool ContainsDate(ProgramNode node, out ContainsDate value)
				{
					ContainsDate? containsDate = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsDate.CreateSafe(this._builders, node);
					if (containsDate == null)
					{
						value = default(ContainsDate);
						return false;
					}
					value = containsDate.Value;
					return true;
				}

				// Token: 0x0600754B RID: 30027 RVA: 0x00195FD8 File Offset: 0x001941D8
				public bool ContainsNum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsNum.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600754C RID: 30028 RVA: 0x00195FFC File Offset: 0x001941FC
				public bool ContainsNum(ProgramNode node, out ContainsNum value)
				{
					ContainsNum? containsNum = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsNum.CreateSafe(this._builders, node);
					if (containsNum == null)
					{
						value = default(ContainsNum);
						return false;
					}
					value = containsNum.Value;
					return true;
				}

				// Token: 0x0600754D RID: 30029 RVA: 0x00196038 File Offset: 0x00194238
				public bool ID_substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ID_substring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600754E RID: 30030 RVA: 0x0019605C File Offset: 0x0019425C
				public bool ID_substring(ProgramNode node, out ID_substring value)
				{
					ID_substring? id_substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ID_substring.CreateSafe(this._builders, node);
					if (id_substring == null)
					{
						value = default(ID_substring);
						return false;
					}
					value = id_substring.Value;
					return true;
				}

				// Token: 0x0600754F RID: 30031 RVA: 0x00196098 File Offset: 0x00194298
				public bool Class(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Class.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007550 RID: 30032 RVA: 0x001960BC File Offset: 0x001942BC
				public bool Class(ProgramNode node, out Class value)
				{
					Class? @class = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Class.CreateSafe(this._builders, node);
					if (@class == null)
					{
						value = default(Class);
						return false;
					}
					value = @class.Value;
					return true;
				}

				// Token: 0x06007551 RID: 30033 RVA: 0x001960F8 File Offset: 0x001942F8
				public bool TitleIs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TitleIs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007552 RID: 30034 RVA: 0x0019611C File Offset: 0x0019431C
				public bool TitleIs(ProgramNode node, out TitleIs value)
				{
					TitleIs? titleIs = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TitleIs.CreateSafe(this._builders, node);
					if (titleIs == null)
					{
						value = default(TitleIs);
						return false;
					}
					value = titleIs.Value;
					return true;
				}

				// Token: 0x06007553 RID: 30035 RVA: 0x00196158 File Offset: 0x00194358
				public bool NodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007554 RID: 30036 RVA: 0x0019617C File Offset: 0x0019437C
				public bool NodeName(ProgramNode node, out NodeName value)
				{
					NodeName? nodeName = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeName.CreateSafe(this._builders, node);
					if (nodeName == null)
					{
						value = default(NodeName);
						return false;
					}
					value = nodeName.Value;
					return true;
				}

				// Token: 0x06007555 RID: 30037 RVA: 0x001961B8 File Offset: 0x001943B8
				public bool NodeNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNames.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007556 RID: 30038 RVA: 0x001961DC File Offset: 0x001943DC
				public bool NodeNames(ProgramNode node, out NodeNames value)
				{
					NodeNames? nodeNames = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNames.CreateSafe(this._builders, node);
					if (nodeNames == null)
					{
						value = default(NodeNames);
						return false;
					}
					value = nodeNames.Value;
					return true;
				}

				// Token: 0x06007557 RID: 30039 RVA: 0x00196218 File Offset: 0x00194418
				public bool NthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007558 RID: 30040 RVA: 0x0019623C File Offset: 0x0019443C
				public bool NthChild(ProgramNode node, out NthChild value)
				{
					NthChild? nthChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChild.CreateSafe(this._builders, node);
					if (nthChild == null)
					{
						value = default(NthChild);
						return false;
					}
					value = nthChild.Value;
					return true;
				}

				// Token: 0x06007559 RID: 30041 RVA: 0x00196278 File Offset: 0x00194478
				public bool NthLastChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600755A RID: 30042 RVA: 0x0019629C File Offset: 0x0019449C
				public bool NthLastChild(ProgramNode node, out NthLastChild value)
				{
					NthLastChild? nthLastChild = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChild.CreateSafe(this._builders, node);
					if (nthLastChild == null)
					{
						value = default(NthLastChild);
						return false;
					}
					value = nthLastChild.Value;
					return true;
				}

				// Token: 0x0600755B RID: 30043 RVA: 0x001962D8 File Offset: 0x001944D8
				public bool ContainsLeafNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsLeafNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600755C RID: 30044 RVA: 0x001962FC File Offset: 0x001944FC
				public bool ContainsLeafNodes(ProgramNode node, out ContainsLeafNodes value)
				{
					ContainsLeafNodes? containsLeafNodes = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsLeafNodes.CreateSafe(this._builders, node);
					if (containsLeafNodes == null)
					{
						value = default(ContainsLeafNodes);
						return false;
					}
					value = containsLeafNodes.Value;
					return true;
				}

				// Token: 0x0600755D RID: 30045 RVA: 0x00196338 File Offset: 0x00194538
				public bool ChildrenCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ChildrenCount.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600755E RID: 30046 RVA: 0x0019635C File Offset: 0x0019455C
				public bool ChildrenCount(ProgramNode node, out ChildrenCount value)
				{
					ChildrenCount? childrenCount = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ChildrenCount.CreateSafe(this._builders, node);
					if (childrenCount == null)
					{
						value = default(ChildrenCount);
						return false;
					}
					value = childrenCount.Value;
					return true;
				}

				// Token: 0x0600755F RID: 30047 RVA: 0x00196398 File Offset: 0x00194598
				public bool HasAttribute(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasAttribute.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007560 RID: 30048 RVA: 0x001963BC File Offset: 0x001945BC
				public bool HasAttribute(ProgramNode node, out HasAttribute value)
				{
					HasAttribute? hasAttribute = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasAttribute.CreateSafe(this._builders, node);
					if (hasAttribute == null)
					{
						value = default(HasAttribute);
						return false;
					}
					value = hasAttribute.Value;
					return true;
				}

				// Token: 0x06007561 RID: 30049 RVA: 0x001963F8 File Offset: 0x001945F8
				public bool HasStyle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasStyle.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007562 RID: 30050 RVA: 0x0019641C File Offset: 0x0019461C
				public bool HasStyle(ProgramNode node, out HasStyle value)
				{
					HasStyle? hasStyle = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasStyle.CreateSafe(this._builders, node);
					if (hasStyle == null)
					{
						value = default(HasStyle);
						return false;
					}
					value = hasStyle.Value;
					return true;
				}

				// Token: 0x06007563 RID: 30051 RVA: 0x00196458 File Offset: 0x00194658
				public bool HasEntityAnchor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasEntityAnchor.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007564 RID: 30052 RVA: 0x0019647C File Offset: 0x0019467C
				public bool HasEntityAnchor(ProgramNode node, out HasEntityAnchor value)
				{
					HasEntityAnchor? hasEntityAnchor = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasEntityAnchor.CreateSafe(this._builders, node);
					if (hasEntityAnchor == null)
					{
						value = default(HasEntityAnchor);
						return false;
					}
					value = hasEntityAnchor.Value;
					return true;
				}

				// Token: 0x06007565 RID: 30053 RVA: 0x001964B8 File Offset: 0x001946B8
				public bool literalExpr_atomExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.literalExpr_atomExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007566 RID: 30054 RVA: 0x001964DC File Offset: 0x001946DC
				public bool literalExpr_atomExpr(ProgramNode node, out literalExpr_atomExpr value)
				{
					literalExpr_atomExpr? literalExpr_atomExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.literalExpr_atomExpr.CreateSafe(this._builders, node);
					if (literalExpr_atomExpr == null)
					{
						value = default(literalExpr_atomExpr);
						return false;
					}
					value = literalExpr_atomExpr.Value;
					return true;
				}

				// Token: 0x06007567 RID: 30055 RVA: 0x00196518 File Offset: 0x00194718
				public bool fexpr_literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.fexpr_literalExpr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007568 RID: 30056 RVA: 0x0019653C File Offset: 0x0019473C
				public bool fexpr_literalExpr(ProgramNode node, out fexpr_literalExpr value)
				{
					fexpr_literalExpr? fexpr_literalExpr = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.fexpr_literalExpr.CreateSafe(this._builders, node);
					if (fexpr_literalExpr == null)
					{
						value = default(fexpr_literalExpr);
						return false;
					}
					value = fexpr_literalExpr.Value;
					return true;
				}

				// Token: 0x06007569 RID: 30057 RVA: 0x00196578 File Offset: 0x00194778
				public bool And(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.And.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600756A RID: 30058 RVA: 0x0019659C File Offset: 0x0019479C
				public bool And(ProgramNode node, out And value)
				{
					And? and = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.And.CreateSafe(this._builders, node);
					if (and == null)
					{
						value = default(And);
						return false;
					}
					value = and.Value;
					return true;
				}

				// Token: 0x0600756B RID: 30059 RVA: 0x001965D8 File Offset: 0x001947D8
				public bool resultFields_singletonField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultFields_singletonField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600756C RID: 30060 RVA: 0x001965FC File Offset: 0x001947FC
				public bool resultFields_singletonField(ProgramNode node, out resultFields_singletonField value)
				{
					resultFields_singletonField? resultFields_singletonField = Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultFields_singletonField.CreateSafe(this._builders, node);
					if (resultFields_singletonField == null)
					{
						value = default(resultFields_singletonField);
						return false;
					}
					value = resultFields_singletonField.Value;
					return true;
				}

				// Token: 0x0600756D RID: 30061 RVA: 0x00196638 File Offset: 0x00194838
				public bool AppendField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AppendField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600756E RID: 30062 RVA: 0x0019665C File Offset: 0x0019485C
				public bool AppendField(ProgramNode node, out AppendField value)
				{
					AppendField? appendField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AppendField.CreateSafe(this._builders, node);
					if (appendField == null)
					{
						value = default(AppendField);
						return false;
					}
					value = appendField.Value;
					return true;
				}

				// Token: 0x0600756F RID: 30063 RVA: 0x00196698 File Offset: 0x00194898
				public bool TrimmedTextField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TrimmedTextField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007570 RID: 30064 RVA: 0x001966BC File Offset: 0x001948BC
				public bool TrimmedTextField(ProgramNode node, out TrimmedTextField value)
				{
					TrimmedTextField? trimmedTextField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TrimmedTextField.CreateSafe(this._builders, node);
					if (trimmedTextField == null)
					{
						value = default(TrimmedTextField);
						return false;
					}
					value = trimmedTextField.Value;
					return true;
				}

				// Token: 0x06007571 RID: 30065 RVA: 0x001966F8 File Offset: 0x001948F8
				public bool SubstringField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SubstringField.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007572 RID: 30066 RVA: 0x0019671C File Offset: 0x0019491C
				public bool SubstringField(ProgramNode node, out SubstringField value)
				{
					SubstringField? substringField = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SubstringField.CreateSafe(this._builders, node);
					if (substringField == null)
					{
						value = default(SubstringField);
						return false;
					}
					value = substringField.Value;
					return true;
				}

				// Token: 0x06007573 RID: 30067 RVA: 0x00196758 File Offset: 0x00194958
				public bool LetSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007574 RID: 30068 RVA: 0x0019677C File Offset: 0x0019497C
				public bool LetSubstring(ProgramNode node, out LetSubstring value)
				{
					LetSubstring? letSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetSubstring.CreateSafe(this._builders, node);
					if (letSubstring == null)
					{
						value = default(LetSubstring);
						return false;
					}
					value = letSubstring.Value;
					return true;
				}

				// Token: 0x06007575 RID: 30069 RVA: 0x001967B8 File Offset: 0x001949B8
				public bool GetValueSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GetValueSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007576 RID: 30070 RVA: 0x001967DC File Offset: 0x001949DC
				public bool GetValueSubstring(ProgramNode node, out GetValueSubstring value)
				{
					GetValueSubstring? getValueSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GetValueSubstring.CreateSafe(this._builders, node);
					if (getValueSubstring == null)
					{
						value = default(GetValueSubstring);
						return false;
					}
					value = getValueSubstring.Value;
					return true;
				}

				// Token: 0x06007577 RID: 30071 RVA: 0x00196818 File Offset: 0x00194A18
				public bool SelectSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SelectSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007578 RID: 30072 RVA: 0x0019683C File Offset: 0x00194A3C
				public bool SelectSubstring(ProgramNode node, out SelectSubstring value)
				{
					SelectSubstring? selectSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SelectSubstring.CreateSafe(this._builders, node);
					if (selectSubstring == null)
					{
						value = default(SelectSubstring);
						return false;
					}
					value = selectSubstring.Value;
					return true;
				}

				// Token: 0x06007579 RID: 30073 RVA: 0x00196878 File Offset: 0x00194A78
				public bool SingleSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600757A RID: 30074 RVA: 0x0019689C File Offset: 0x00194A9C
				public bool SingleSubstring(ProgramNode node, out SingleSubstring value)
				{
					SingleSubstring? singleSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSubstring.CreateSafe(this._builders, node);
					if (singleSubstring == null)
					{
						value = default(SingleSubstring);
						return false;
					}
					value = singleSubstring.Value;
					return true;
				}

				// Token: 0x0600757B RID: 30075 RVA: 0x001968D8 File Offset: 0x00194AD8
				public bool DisjSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600757C RID: 30076 RVA: 0x001968FC File Offset: 0x00194AFC
				public bool DisjSubstring(ProgramNode node, out DisjSubstring value)
				{
					DisjSubstring? disjSubstring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSubstring.CreateSafe(this._builders, node);
					if (disjSubstring == null)
					{
						value = default(DisjSubstring);
						return false;
					}
					value = disjSubstring.Value;
					return true;
				}

				// Token: 0x0600757D RID: 30077 RVA: 0x00196938 File Offset: 0x00194B38
				public bool Substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600757E RID: 30078 RVA: 0x0019695C File Offset: 0x00194B5C
				public bool Substring(ProgramNode node, out Substring value)
				{
					Substring? substring = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
					if (substring == null)
					{
						value = default(Substring);
						return false;
					}
					value = substring.Value;
					return true;
				}

				// Token: 0x0600757F RID: 30079 RVA: 0x00196998 File Offset: 0x00194B98
				public bool ExtractTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007580 RID: 30080 RVA: 0x001969BC File Offset: 0x00194BBC
				public bool ExtractTable(ProgramNode node, out ExtractTable value)
				{
					ExtractTable? extractTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractTable.CreateSafe(this._builders, node);
					if (extractTable == null)
					{
						value = default(ExtractTable);
						return false;
					}
					value = extractTable.Value;
					return true;
				}

				// Token: 0x06007581 RID: 30081 RVA: 0x001969F8 File Offset: 0x00194BF8
				public bool ExtractRowBasedTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractRowBasedTable.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007582 RID: 30082 RVA: 0x00196A1C File Offset: 0x00194C1C
				public bool ExtractRowBasedTable(ProgramNode node, out ExtractRowBasedTable value)
				{
					ExtractRowBasedTable? extractRowBasedTable = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractRowBasedTable.CreateSafe(this._builders, node);
					if (extractRowBasedTable == null)
					{
						value = default(ExtractRowBasedTable);
						return false;
					}
					value = extractRowBasedTable.Value;
					return true;
				}

				// Token: 0x06007583 RID: 30083 RVA: 0x00196A58 File Offset: 0x00194C58
				public bool SingleColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007584 RID: 30084 RVA: 0x00196A7C File Offset: 0x00194C7C
				public bool SingleColumn(ProgramNode node, out SingleColumn value)
				{
					SingleColumn? singleColumn = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleColumn.CreateSafe(this._builders, node);
					if (singleColumn == null)
					{
						value = default(SingleColumn);
						return false;
					}
					value = singleColumn.Value;
					return true;
				}

				// Token: 0x06007585 RID: 30085 RVA: 0x00196AB8 File Offset: 0x00194CB8
				public bool ColumnSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ColumnSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007586 RID: 30086 RVA: 0x00196ADC File Offset: 0x00194CDC
				public bool ColumnSequence(ProgramNode node, out ColumnSequence value)
				{
					ColumnSequence? columnSequence = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ColumnSequence.CreateSafe(this._builders, node);
					if (columnSequence == null)
					{
						value = default(ColumnSequence);
						return false;
					}
					value = columnSequence.Value;
					return true;
				}

				// Token: 0x06007587 RID: 30087 RVA: 0x00196B18 File Offset: 0x00194D18
				public bool AsCollection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AsCollection.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007588 RID: 30088 RVA: 0x00196B3C File Offset: 0x00194D3C
				public bool AsCollection(ProgramNode node, out AsCollection value)
				{
					AsCollection? asCollection = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AsCollection.CreateSafe(this._builders, node);
					if (asCollection == null)
					{
						value = default(AsCollection);
						return false;
					}
					value = asCollection.Value;
					return true;
				}

				// Token: 0x06007589 RID: 30089 RVA: 0x00196B78 File Offset: 0x00194D78
				public bool DescendantsOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DescendantsOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600758A RID: 30090 RVA: 0x00196B9C File Offset: 0x00194D9C
				public bool DescendantsOf(ProgramNode node, out DescendantsOf value)
				{
					DescendantsOf? descendantsOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DescendantsOf.CreateSafe(this._builders, node);
					if (descendantsOf == null)
					{
						value = default(DescendantsOf);
						return false;
					}
					value = descendantsOf.Value;
					return true;
				}

				// Token: 0x0600758B RID: 30091 RVA: 0x00196BD8 File Offset: 0x00194DD8
				public bool RightSiblingOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.RightSiblingOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600758C RID: 30092 RVA: 0x00196BFC File Offset: 0x00194DFC
				public bool RightSiblingOf(ProgramNode node, out RightSiblingOf value)
				{
					RightSiblingOf? rightSiblingOf = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.RightSiblingOf.CreateSafe(this._builders, node);
					if (rightSiblingOf == null)
					{
						value = default(RightSiblingOf);
						return false;
					}
					value = rightSiblingOf.Value;
					return true;
				}

				// Token: 0x0600758D RID: 30093 RVA: 0x00196C38 File Offset: 0x00194E38
				public bool ClassFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ClassFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600758E RID: 30094 RVA: 0x00196C5C File Offset: 0x00194E5C
				public bool ClassFilter(ProgramNode node, out ClassFilter value)
				{
					ClassFilter? classFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ClassFilter.CreateSafe(this._builders, node);
					if (classFilter == null)
					{
						value = default(ClassFilter);
						return false;
					}
					value = classFilter.Value;
					return true;
				}

				// Token: 0x0600758F RID: 30095 RVA: 0x00196C98 File Offset: 0x00194E98
				public bool IDFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.IDFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007590 RID: 30096 RVA: 0x00196CBC File Offset: 0x00194EBC
				public bool IDFilter(ProgramNode node, out IDFilter value)
				{
					IDFilter? idfilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.IDFilter.CreateSafe(this._builders, node);
					if (idfilter == null)
					{
						value = default(IDFilter);
						return false;
					}
					value = idfilter.Value;
					return true;
				}

				// Token: 0x06007591 RID: 30097 RVA: 0x00196CF8 File Offset: 0x00194EF8
				public bool NodeNameFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNameFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007592 RID: 30098 RVA: 0x00196D1C File Offset: 0x00194F1C
				public bool NodeNameFilter(ProgramNode node, out NodeNameFilter value)
				{
					NodeNameFilter? nodeNameFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNameFilter.CreateSafe(this._builders, node);
					if (nodeNameFilter == null)
					{
						value = default(NodeNameFilter);
						return false;
					}
					value = nodeNameFilter.Value;
					return true;
				}

				// Token: 0x06007593 RID: 30099 RVA: 0x00196D58 File Offset: 0x00194F58
				public bool ItemPropFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ItemPropFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007594 RID: 30100 RVA: 0x00196D7C File Offset: 0x00194F7C
				public bool ItemPropFilter(ProgramNode node, out ItemPropFilter value)
				{
					ItemPropFilter? itemPropFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ItemPropFilter.CreateSafe(this._builders, node);
					if (itemPropFilter == null)
					{
						value = default(ItemPropFilter);
						return false;
					}
					value = itemPropFilter.Value;
					return true;
				}

				// Token: 0x06007595 RID: 30101 RVA: 0x00196DB8 File Offset: 0x00194FB8
				public bool NthChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChildFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007596 RID: 30102 RVA: 0x00196DDC File Offset: 0x00194FDC
				public bool NthChildFilter(ProgramNode node, out NthChildFilter value)
				{
					NthChildFilter? nthChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChildFilter.CreateSafe(this._builders, node);
					if (nthChildFilter == null)
					{
						value = default(NthChildFilter);
						return false;
					}
					value = nthChildFilter.Value;
					return true;
				}

				// Token: 0x06007597 RID: 30103 RVA: 0x00196E18 File Offset: 0x00195018
				public bool NthLastChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChildFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06007598 RID: 30104 RVA: 0x00196E3C File Offset: 0x0019503C
				public bool NthLastChildFilter(ProgramNode node, out NthLastChildFilter value)
				{
					NthLastChildFilter? nthLastChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChildFilter.CreateSafe(this._builders, node);
					if (nthLastChildFilter == null)
					{
						value = default(NthLastChildFilter);
						return false;
					}
					value = nthLastChildFilter.Value;
					return true;
				}

				// Token: 0x06007599 RID: 30105 RVA: 0x00196E78 File Offset: 0x00195078
				public bool GEN_NthChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthChildFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600759A RID: 30106 RVA: 0x00196E9C File Offset: 0x0019509C
				public bool GEN_NthChildFilter(ProgramNode node, out GEN_NthChildFilter value)
				{
					GEN_NthChildFilter? gen_NthChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthChildFilter.CreateSafe(this._builders, node);
					if (gen_NthChildFilter == null)
					{
						value = default(GEN_NthChildFilter);
						return false;
					}
					value = gen_NthChildFilter.Value;
					return true;
				}

				// Token: 0x0600759B RID: 30107 RVA: 0x00196ED8 File Offset: 0x001950D8
				public bool GEN_NthLastChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthLastChildFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600759C RID: 30108 RVA: 0x00196EFC File Offset: 0x001950FC
				public bool GEN_NthLastChildFilter(ProgramNode node, out GEN_NthLastChildFilter value)
				{
					GEN_NthLastChildFilter? gen_NthLastChildFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthLastChildFilter.CreateSafe(this._builders, node);
					if (gen_NthLastChildFilter == null)
					{
						value = default(GEN_NthLastChildFilter);
						return false;
					}
					value = gen_NthLastChildFilter.Value;
					return true;
				}

				// Token: 0x0600759D RID: 30109 RVA: 0x00196F38 File Offset: 0x00195138
				public bool GEN_ClassFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ClassFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600759E RID: 30110 RVA: 0x00196F5C File Offset: 0x0019515C
				public bool GEN_ClassFilter(ProgramNode node, out GEN_ClassFilter value)
				{
					GEN_ClassFilter? gen_ClassFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ClassFilter.CreateSafe(this._builders, node);
					if (gen_ClassFilter == null)
					{
						value = default(GEN_ClassFilter);
						return false;
					}
					value = gen_ClassFilter.Value;
					return true;
				}

				// Token: 0x0600759F RID: 30111 RVA: 0x00196F98 File Offset: 0x00195198
				public bool GEN_IDFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_IDFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060075A0 RID: 30112 RVA: 0x00196FBC File Offset: 0x001951BC
				public bool GEN_IDFilter(ProgramNode node, out GEN_IDFilter value)
				{
					GEN_IDFilter? gen_IDFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_IDFilter.CreateSafe(this._builders, node);
					if (gen_IDFilter == null)
					{
						value = default(GEN_IDFilter);
						return false;
					}
					value = gen_IDFilter.Value;
					return true;
				}

				// Token: 0x060075A1 RID: 30113 RVA: 0x00196FF8 File Offset: 0x001951F8
				public bool GEN_NodeNameFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NodeNameFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060075A2 RID: 30114 RVA: 0x0019701C File Offset: 0x0019521C
				public bool GEN_NodeNameFilter(ProgramNode node, out GEN_NodeNameFilter value)
				{
					GEN_NodeNameFilter? gen_NodeNameFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NodeNameFilter.CreateSafe(this._builders, node);
					if (gen_NodeNameFilter == null)
					{
						value = default(GEN_NodeNameFilter);
						return false;
					}
					value = gen_NodeNameFilter.Value;
					return true;
				}

				// Token: 0x060075A3 RID: 30115 RVA: 0x00197058 File Offset: 0x00195258
				public bool GEN_ItemPropFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ItemPropFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x060075A4 RID: 30116 RVA: 0x0019707C File Offset: 0x0019527C
				public bool GEN_ItemPropFilter(ProgramNode node, out GEN_ItemPropFilter value)
				{
					GEN_ItemPropFilter? gen_ItemPropFilter = Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ItemPropFilter.CreateSafe(this._builders, node);
					if (gen_ItemPropFilter == null)
					{
						value = default(GEN_ItemPropFilter);
						return false;
					}
					value = gen_ItemPropFilter.Value;
					return true;
				}

				// Token: 0x0400326F RID: 12911
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FEF RID: 4079
			public class NodeAs
			{
				// Token: 0x060075A5 RID: 30117 RVA: 0x001970B6 File Offset: 0x001952B6
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060075A6 RID: 30118 RVA: 0x001970C5 File Offset: 0x001952C5
				public resultSequence? resultSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075A7 RID: 30119 RVA: 0x001970D3 File Offset: 0x001952D3
				public resultRegion? resultRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060075A8 RID: 30120 RVA: 0x001970E1 File Offset: 0x001952E1
				public subNodeSequence? subNodeSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075A9 RID: 30121 RVA: 0x001970EF File Offset: 0x001952EF
				public node? node(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AA RID: 30122 RVA: 0x001970FD File Offset: 0x001952FD
				public subNode? subNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AB RID: 30123 RVA: 0x0019710B File Offset: 0x0019530B
				public mapNodeInSequence? mapNodeInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AC RID: 30124 RVA: 0x00197119 File Offset: 0x00195319
				public regionSequence? regionSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AD RID: 30125 RVA: 0x00197127 File Offset: 0x00195327
				public regionStart? regionStart(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AE RID: 30126 RVA: 0x00197135 File Offset: 0x00195335
				public region? region(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region.CreateSafe(this._builders, node);
				}

				// Token: 0x060075AF RID: 30127 RVA: 0x00197143 File Offset: 0x00195343
				public mapRegionInSequence? mapRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B0 RID: 30128 RVA: 0x00197151 File Offset: 0x00195351
				public beginNode? beginNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B1 RID: 30129 RVA: 0x0019715F File Offset: 0x0019535F
				public endNode? endNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B2 RID: 30130 RVA: 0x0019716D File Offset: 0x0019536D
				public selection? selection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B3 RID: 30131 RVA: 0x0019717B File Offset: 0x0019537B
				public filterSelection? filterSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B4 RID: 30132 RVA: 0x00197189 File Offset: 0x00195389
				public selectionEnd? selectionEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B5 RID: 30133 RVA: 0x00197197 File Offset: 0x00195397
				public regionStartSiblings? regionStartSiblings(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B6 RID: 30134 RVA: 0x001971A5 File Offset: 0x001953A5
				public selection2? selection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B7 RID: 30135 RVA: 0x001971B3 File Offset: 0x001953B3
				public selection3? selection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B8 RID: 30136 RVA: 0x001971C1 File Offset: 0x001953C1
				public filterSelection2? filterSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2.CreateSafe(this._builders, node);
				}

				// Token: 0x060075B9 RID: 30137 RVA: 0x001971CF File Offset: 0x001953CF
				public selection4? selection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BA RID: 30138 RVA: 0x001971DD File Offset: 0x001953DD
				public selection5? selection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BB RID: 30139 RVA: 0x001971EB File Offset: 0x001953EB
				public filterSelection3? filterSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BC RID: 30140 RVA: 0x001971F9 File Offset: 0x001953F9
				public selection6? selection6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BD RID: 30141 RVA: 0x00197207 File Offset: 0x00195407
				public selection7? selection7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BE RID: 30142 RVA: 0x00197215 File Offset: 0x00195415
				public filterSelection4? filterSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4.CreateSafe(this._builders, node);
				}

				// Token: 0x060075BF RID: 30143 RVA: 0x00197223 File Offset: 0x00195423
				public selection8? selection8(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C0 RID: 30144 RVA: 0x00197231 File Offset: 0x00195431
				public selection9? selection9(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C1 RID: 30145 RVA: 0x0019723F File Offset: 0x0019543F
				public filterSelection5? filterSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C2 RID: 30146 RVA: 0x0019724D File Offset: 0x0019544D
				public selection10? selection10(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C3 RID: 30147 RVA: 0x0019725B File Offset: 0x0019545B
				public leafFExpr? leafFExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C4 RID: 30148 RVA: 0x00197269 File Offset: 0x00195469
				public leafAtom? leafAtom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C5 RID: 30149 RVA: 0x00197277 File Offset: 0x00195477
				public atomExpr? atomExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C6 RID: 30150 RVA: 0x00197285 File Offset: 0x00195485
				public literalExpr? literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C7 RID: 30151 RVA: 0x00197293 File Offset: 0x00195493
				public fexpr? fexpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C8 RID: 30152 RVA: 0x001972A1 File Offset: 0x001954A1
				public resultFields? resultFields(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields.CreateSafe(this._builders, node);
				}

				// Token: 0x060075C9 RID: 30153 RVA: 0x001972AF File Offset: 0x001954AF
				public singletonField? singletonField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CA RID: 30154 RVA: 0x001972BD File Offset: 0x001954BD
				public fieldSubstring? fieldSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CB RID: 30155 RVA: 0x001972CB File Offset: 0x001954CB
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs? cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CC RID: 30156 RVA: 0x001972D9 File Offset: 0x001954D9
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y? y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CD RID: 30157 RVA: 0x001972E7 File Offset: 0x001954E7
				public selectSubstring? selectSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CE RID: 30158 RVA: 0x001972F5 File Offset: 0x001954F5
				public substringDisj? substringDisj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj.CreateSafe(this._builders, node);
				}

				// Token: 0x060075CF RID: 30159 RVA: 0x00197303 File Offset: 0x00195503
				public substring? substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D0 RID: 30160 RVA: 0x00197311 File Offset: 0x00195511
				public resultTable? resultTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D1 RID: 30161 RVA: 0x0019731F File Offset: 0x0019551F
				public columnSelectors? columnSelectors(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D2 RID: 30162 RVA: 0x0019732D File Offset: 0x0019552D
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name? name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D3 RID: 30163 RVA: 0x0019733B File Offset: 0x0019553B
				public value? value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D4 RID: 30164 RVA: 0x00197349 File Offset: 0x00195549
				public cssSelector? cssSelector(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D5 RID: 30165 RVA: 0x00197357 File Offset: 0x00195557
				public className? className(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D6 RID: 30166 RVA: 0x00197365 File Offset: 0x00195565
				public idName? idName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D7 RID: 30167 RVA: 0x00197373 File Offset: 0x00195573
				public nodeName? nodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D8 RID: 30168 RVA: 0x00197381 File Offset: 0x00195581
				public propName? propName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName.CreateSafe(this._builders, node);
				}

				// Token: 0x060075D9 RID: 30169 RVA: 0x0019738F File Offset: 0x0019558F
				public idx1? idx1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DA RID: 30170 RVA: 0x0019739D File Offset: 0x0019559D
				public idx2? idx2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DB RID: 30171 RVA: 0x001973AB File Offset: 0x001955AB
				public names? names(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DC RID: 30172 RVA: 0x001973B9 File Offset: 0x001955B9
				public count? count(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DD RID: 30173 RVA: 0x001973C7 File Offset: 0x001955C7
				public substringFeatureNames? substringFeatureNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DE RID: 30174 RVA: 0x001973D5 File Offset: 0x001955D5
				public substringFeatureValues? substringFeatureValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues.CreateSafe(this._builders, node);
				}

				// Token: 0x060075DF RID: 30175 RVA: 0x001973E3 File Offset: 0x001955E3
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E0 RID: 30176 RVA: 0x001973F1 File Offset: 0x001955F1
				public entityObjs? entityObjs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E1 RID: 30177 RVA: 0x001973FF File Offset: 0x001955FF
				public direction? direction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E2 RID: 30178 RVA: 0x0019740D File Offset: 0x0019560D
				public nodeCollection? nodeCollection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E3 RID: 30179 RVA: 0x0019741B File Offset: 0x0019561B
				public gen_NthChild? gen_NthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E4 RID: 30180 RVA: 0x00197429 File Offset: 0x00195629
				public gen_NthLastChild? gen_NthLastChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E5 RID: 30181 RVA: 0x00197437 File Offset: 0x00195637
				public gen_Class? gen_Class(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E6 RID: 30182 RVA: 0x00197445 File Offset: 0x00195645
				public gen_ID? gen_ID(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E7 RID: 30183 RVA: 0x00197453 File Offset: 0x00195653
				public gen_NodeName? gen_NodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E8 RID: 30184 RVA: 0x00197461 File Offset: 0x00195661
				public gen_ItemProp? gen_ItemProp(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp.CreateSafe(this._builders, node);
				}

				// Token: 0x060075E9 RID: 30185 RVA: 0x0019746F File Offset: 0x0019566F
				public obj? obj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj.CreateSafe(this._builders, node);
				}

				// Token: 0x060075EA RID: 30186 RVA: 0x0019747D File Offset: 0x0019567D
				public Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x04003270 RID: 12912
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FF0 RID: 4080
			public class RuleAs
			{
				// Token: 0x060075EB RID: 30187 RVA: 0x0019748B File Offset: 0x0019568B
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060075EC RID: 30188 RVA: 0x0019749A File Offset: 0x0019569A
				public resultSequence_subNodeSequence? resultSequence_subNodeSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_subNodeSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075ED RID: 30189 RVA: 0x001974A8 File Offset: 0x001956A8
				public resultSequence_regionSequence? resultSequence_regionSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultSequence_regionSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075EE RID: 30190 RVA: 0x001974B6 File Offset: 0x001956B6
				public ConvertToWebRegions? ConvertToWebRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ConvertToWebRegions.CreateSafe(this._builders, node);
				}

				// Token: 0x060075EF RID: 30191 RVA: 0x001974C4 File Offset: 0x001956C4
				public Union? Union(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Union.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F0 RID: 30192 RVA: 0x001974D2 File Offset: 0x001956D2
				public EmptySequence? EmptySequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.EmptySequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F1 RID: 30193 RVA: 0x001974E0 File Offset: 0x001956E0
				public resultRegion_subNode? resultRegion_subNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_subNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F2 RID: 30194 RVA: 0x001974EE File Offset: 0x001956EE
				public resultRegion_region? resultRegion_region(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultRegion_region.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F3 RID: 30195 RVA: 0x001974FC File Offset: 0x001956FC
				public MapToWebRegion? MapToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.MapToWebRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F4 RID: 30196 RVA: 0x0019750A File Offset: 0x0019570A
				public NodeToWebRegion? NodeToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F5 RID: 30197 RVA: 0x00197518 File Offset: 0x00195718
				public NodeToWebRegionInSequence? NodeToWebRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeToWebRegionInSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F6 RID: 30198 RVA: 0x00197526 File Offset: 0x00195726
				public FindEndNode? FindEndNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FindEndNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F7 RID: 30199 RVA: 0x00197534 File Offset: 0x00195734
				public NodeRegionToWebRegion? NodeRegionToWebRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F8 RID: 30200 RVA: 0x00197542 File Offset: 0x00195742
				public LetRegion? LetRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060075F9 RID: 30201 RVA: 0x00197550 File Offset: 0x00195750
				public NodeRegionToWebRegionInSequence? NodeRegionToWebRegionInSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeRegionToWebRegionInSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FA RID: 30202 RVA: 0x0019755E File Offset: 0x0019575E
				public KthNodeInSelection? KthNodeInSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNodeInSelection.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FB RID: 30203 RVA: 0x0019756C File Offset: 0x0019576C
				public KthNode? KthNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.KthNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FC RID: 30204 RVA: 0x0019757A File Offset: 0x0019577A
				public SingleSelection1? SingleSelection1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection1.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FD RID: 30205 RVA: 0x00197588 File Offset: 0x00195788
				public DisjSelection1? DisjSelection1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection1.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FE RID: 30206 RVA: 0x00197596 File Offset: 0x00195796
				public CSSSelection? CSSSelection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.CSSSelection.CreateSafe(this._builders, node);
				}

				// Token: 0x060075FF RID: 30207 RVA: 0x001975A4 File Offset: 0x001957A4
				public LeafFilter1? LeafFilter1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter1.CreateSafe(this._builders, node);
				}

				// Token: 0x06007600 RID: 30208 RVA: 0x001975B2 File Offset: 0x001957B2
				public FilterNodesEnd? FilterNodesEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.FilterNodesEnd.CreateSafe(this._builders, node);
				}

				// Token: 0x06007601 RID: 30209 RVA: 0x001975C0 File Offset: 0x001957C0
				public TakeWhileNodesEnd? TakeWhileNodesEnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TakeWhileNodesEnd.CreateSafe(this._builders, node);
				}

				// Token: 0x06007602 RID: 30210 RVA: 0x001975CE File Offset: 0x001957CE
				public selectionEnd_regionStartSiblings? selectionEnd_regionStartSiblings(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selectionEnd_regionStartSiblings.CreateSafe(this._builders, node);
				}

				// Token: 0x06007603 RID: 30211 RVA: 0x001975DC File Offset: 0x001957DC
				public YoungerSiblingsOf? YoungerSiblingsOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.YoungerSiblingsOf.CreateSafe(this._builders, node);
				}

				// Token: 0x06007604 RID: 30212 RVA: 0x001975EA File Offset: 0x001957EA
				public LeafChildrenOf1? LeafChildrenOf1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf1.CreateSafe(this._builders, node);
				}

				// Token: 0x06007605 RID: 30213 RVA: 0x001975F8 File Offset: 0x001957F8
				public selection2_allNodes? selection2_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection2_allNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x06007606 RID: 30214 RVA: 0x00197606 File Offset: 0x00195806
				public SingleSelection2? SingleSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection2.CreateSafe(this._builders, node);
				}

				// Token: 0x06007607 RID: 30215 RVA: 0x00197614 File Offset: 0x00195814
				public DisjSelection2? DisjSelection2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection2.CreateSafe(this._builders, node);
				}

				// Token: 0x06007608 RID: 30216 RVA: 0x00197622 File Offset: 0x00195822
				public LeafFilter2? LeafFilter2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter2.CreateSafe(this._builders, node);
				}

				// Token: 0x06007609 RID: 30217 RVA: 0x00197630 File Offset: 0x00195830
				public LeafChildrenOf2? LeafChildrenOf2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760A RID: 30218 RVA: 0x0019763E File Offset: 0x0019583E
				public selection4_allNodes? selection4_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection4_allNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760B RID: 30219 RVA: 0x0019764C File Offset: 0x0019584C
				public SingleSelection3? SingleSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection3.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760C RID: 30220 RVA: 0x0019765A File Offset: 0x0019585A
				public DisjSelection3? DisjSelection3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection3.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760D RID: 30221 RVA: 0x00197668 File Offset: 0x00195868
				public LeafFilter3? LeafFilter3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter3.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760E RID: 30222 RVA: 0x00197676 File Offset: 0x00195876
				public LeafChildrenOf3? LeafChildrenOf3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf3.CreateSafe(this._builders, node);
				}

				// Token: 0x0600760F RID: 30223 RVA: 0x00197684 File Offset: 0x00195884
				public selection6_allNodes? selection6_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection6_allNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x06007610 RID: 30224 RVA: 0x00197692 File Offset: 0x00195892
				public SingleSelection4? SingleSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection4.CreateSafe(this._builders, node);
				}

				// Token: 0x06007611 RID: 30225 RVA: 0x001976A0 File Offset: 0x001958A0
				public DisjSelection4? DisjSelection4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection4.CreateSafe(this._builders, node);
				}

				// Token: 0x06007612 RID: 30226 RVA: 0x001976AE File Offset: 0x001958AE
				public LeafFilter4? LeafFilter4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter4.CreateSafe(this._builders, node);
				}

				// Token: 0x06007613 RID: 30227 RVA: 0x001976BC File Offset: 0x001958BC
				public LeafChildrenOf4? LeafChildrenOf4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafChildrenOf4.CreateSafe(this._builders, node);
				}

				// Token: 0x06007614 RID: 30228 RVA: 0x001976CA File Offset: 0x001958CA
				public selection8_allNodes? selection8_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection8_allNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x06007615 RID: 30229 RVA: 0x001976D8 File Offset: 0x001958D8
				public SingleSelection5? SingleSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSelection5.CreateSafe(this._builders, node);
				}

				// Token: 0x06007616 RID: 30230 RVA: 0x001976E6 File Offset: 0x001958E6
				public DisjSelection5? DisjSelection5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSelection5.CreateSafe(this._builders, node);
				}

				// Token: 0x06007617 RID: 30231 RVA: 0x001976F4 File Offset: 0x001958F4
				public LeafFilter5? LeafFilter5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafFilter5.CreateSafe(this._builders, node);
				}

				// Token: 0x06007618 RID: 30232 RVA: 0x00197702 File Offset: 0x00195902
				public selection10_allNodes? selection10_allNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.selection10_allNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x06007619 RID: 30233 RVA: 0x00197710 File Offset: 0x00195910
				public leafFExpr_leafAtom? leafFExpr_leafAtom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafFExpr_leafAtom.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761A RID: 30234 RVA: 0x0019771E File Offset: 0x0019591E
				public LeafAnd? LeafAnd(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LeafAnd.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761B RID: 30235 RVA: 0x0019772C File Offset: 0x0019592C
				public leafAtom_literalExpr? leafAtom_literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.leafAtom_literalExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761C RID: 30236 RVA: 0x0019773A File Offset: 0x0019593A
				public ContainsDate? ContainsDate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsDate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761D RID: 30237 RVA: 0x00197748 File Offset: 0x00195948
				public ContainsNum? ContainsNum(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsNum.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761E RID: 30238 RVA: 0x00197756 File Offset: 0x00195956
				public ID_substring? ID_substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ID_substring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600761F RID: 30239 RVA: 0x00197764 File Offset: 0x00195964
				public Class? Class(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Class.CreateSafe(this._builders, node);
				}

				// Token: 0x06007620 RID: 30240 RVA: 0x00197772 File Offset: 0x00195972
				public TitleIs? TitleIs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TitleIs.CreateSafe(this._builders, node);
				}

				// Token: 0x06007621 RID: 30241 RVA: 0x00197780 File Offset: 0x00195980
				public NodeName? NodeName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeName.CreateSafe(this._builders, node);
				}

				// Token: 0x06007622 RID: 30242 RVA: 0x0019778E File Offset: 0x0019598E
				public NodeNames? NodeNames(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNames.CreateSafe(this._builders, node);
				}

				// Token: 0x06007623 RID: 30243 RVA: 0x0019779C File Offset: 0x0019599C
				public NthChild? NthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChild.CreateSafe(this._builders, node);
				}

				// Token: 0x06007624 RID: 30244 RVA: 0x001977AA File Offset: 0x001959AA
				public NthLastChild? NthLastChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChild.CreateSafe(this._builders, node);
				}

				// Token: 0x06007625 RID: 30245 RVA: 0x001977B8 File Offset: 0x001959B8
				public ContainsLeafNodes? ContainsLeafNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ContainsLeafNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x06007626 RID: 30246 RVA: 0x001977C6 File Offset: 0x001959C6
				public ChildrenCount? ChildrenCount(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ChildrenCount.CreateSafe(this._builders, node);
				}

				// Token: 0x06007627 RID: 30247 RVA: 0x001977D4 File Offset: 0x001959D4
				public HasAttribute? HasAttribute(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasAttribute.CreateSafe(this._builders, node);
				}

				// Token: 0x06007628 RID: 30248 RVA: 0x001977E2 File Offset: 0x001959E2
				public HasStyle? HasStyle(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasStyle.CreateSafe(this._builders, node);
				}

				// Token: 0x06007629 RID: 30249 RVA: 0x001977F0 File Offset: 0x001959F0
				public HasEntityAnchor? HasEntityAnchor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.HasEntityAnchor.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762A RID: 30250 RVA: 0x001977FE File Offset: 0x001959FE
				public literalExpr_atomExpr? literalExpr_atomExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.literalExpr_atomExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762B RID: 30251 RVA: 0x0019780C File Offset: 0x00195A0C
				public fexpr_literalExpr? fexpr_literalExpr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.fexpr_literalExpr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762C RID: 30252 RVA: 0x0019781A File Offset: 0x00195A1A
				public And? And(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.And.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762D RID: 30253 RVA: 0x00197828 File Offset: 0x00195A28
				public resultFields_singletonField? resultFields_singletonField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes.resultFields_singletonField.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762E RID: 30254 RVA: 0x00197836 File Offset: 0x00195A36
				public AppendField? AppendField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AppendField.CreateSafe(this._builders, node);
				}

				// Token: 0x0600762F RID: 30255 RVA: 0x00197844 File Offset: 0x00195A44
				public TrimmedTextField? TrimmedTextField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.TrimmedTextField.CreateSafe(this._builders, node);
				}

				// Token: 0x06007630 RID: 30256 RVA: 0x00197852 File Offset: 0x00195A52
				public SubstringField? SubstringField(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SubstringField.CreateSafe(this._builders, node);
				}

				// Token: 0x06007631 RID: 30257 RVA: 0x00197860 File Offset: 0x00195A60
				public LetSubstring? LetSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.LetSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007632 RID: 30258 RVA: 0x0019786E File Offset: 0x00195A6E
				public GetValueSubstring? GetValueSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GetValueSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007633 RID: 30259 RVA: 0x0019787C File Offset: 0x00195A7C
				public SelectSubstring? SelectSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SelectSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007634 RID: 30260 RVA: 0x0019788A File Offset: 0x00195A8A
				public SingleSubstring? SingleSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007635 RID: 30261 RVA: 0x00197898 File Offset: 0x00195A98
				public DisjSubstring? DisjSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DisjSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007636 RID: 30262 RVA: 0x001978A6 File Offset: 0x00195AA6
				public Substring? Substring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.Substring.CreateSafe(this._builders, node);
				}

				// Token: 0x06007637 RID: 30263 RVA: 0x001978B4 File Offset: 0x00195AB4
				public ExtractTable? ExtractTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractTable.CreateSafe(this._builders, node);
				}

				// Token: 0x06007638 RID: 30264 RVA: 0x001978C2 File Offset: 0x00195AC2
				public ExtractRowBasedTable? ExtractRowBasedTable(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ExtractRowBasedTable.CreateSafe(this._builders, node);
				}

				// Token: 0x06007639 RID: 30265 RVA: 0x001978D0 File Offset: 0x00195AD0
				public SingleColumn? SingleColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.SingleColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763A RID: 30266 RVA: 0x001978DE File Offset: 0x00195ADE
				public ColumnSequence? ColumnSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ColumnSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763B RID: 30267 RVA: 0x001978EC File Offset: 0x00195AEC
				public AsCollection? AsCollection(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.AsCollection.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763C RID: 30268 RVA: 0x001978FA File Offset: 0x00195AFA
				public DescendantsOf? DescendantsOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.DescendantsOf.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763D RID: 30269 RVA: 0x00197908 File Offset: 0x00195B08
				public RightSiblingOf? RightSiblingOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.RightSiblingOf.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763E RID: 30270 RVA: 0x00197916 File Offset: 0x00195B16
				public ClassFilter? ClassFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ClassFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x0600763F RID: 30271 RVA: 0x00197924 File Offset: 0x00195B24
				public IDFilter? IDFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.IDFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007640 RID: 30272 RVA: 0x00197932 File Offset: 0x00195B32
				public NodeNameFilter? NodeNameFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NodeNameFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007641 RID: 30273 RVA: 0x00197940 File Offset: 0x00195B40
				public ItemPropFilter? ItemPropFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.ItemPropFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007642 RID: 30274 RVA: 0x0019794E File Offset: 0x00195B4E
				public NthChildFilter? NthChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthChildFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007643 RID: 30275 RVA: 0x0019795C File Offset: 0x00195B5C
				public NthLastChildFilter? NthLastChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.NthLastChildFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007644 RID: 30276 RVA: 0x0019796A File Offset: 0x00195B6A
				public GEN_NthChildFilter? GEN_NthChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthChildFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007645 RID: 30277 RVA: 0x00197978 File Offset: 0x00195B78
				public GEN_NthLastChildFilter? GEN_NthLastChildFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NthLastChildFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007646 RID: 30278 RVA: 0x00197986 File Offset: 0x00195B86
				public GEN_ClassFilter? GEN_ClassFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ClassFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007647 RID: 30279 RVA: 0x00197994 File Offset: 0x00195B94
				public GEN_IDFilter? GEN_IDFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_IDFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007648 RID: 30280 RVA: 0x001979A2 File Offset: 0x00195BA2
				public GEN_NodeNameFilter? GEN_NodeNameFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_NodeNameFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x06007649 RID: 30281 RVA: 0x001979B0 File Offset: 0x00195BB0
				public GEN_ItemPropFilter? GEN_ItemPropFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes.GEN_ItemPropFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x04003271 RID: 12913
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000FF2 RID: 4082
		public class Sets
		{
			// Token: 0x0600764D RID: 30285 RVA: 0x001979D8 File Offset: 0x00195BD8
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x1700154B RID: 5451
			// (get) Token: 0x0600764E RID: 30286 RVA: 0x00197A27 File Offset: 0x00195C27
			// (set) Token: 0x0600764F RID: 30287 RVA: 0x00197A2F File Offset: 0x00195C2F
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x1700154C RID: 5452
			// (get) Token: 0x06007650 RID: 30288 RVA: 0x00197A38 File Offset: 0x00195C38
			// (set) Token: 0x06007651 RID: 30289 RVA: 0x00197A40 File Offset: 0x00195C40
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x1700154D RID: 5453
			// (get) Token: 0x06007652 RID: 30290 RVA: 0x00197A49 File Offset: 0x00195C49
			// (set) Token: 0x06007653 RID: 30291 RVA: 0x00197A51 File Offset: 0x00195C51
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x1700154E RID: 5454
			// (get) Token: 0x06007654 RID: 30292 RVA: 0x00197A5A File Offset: 0x00195C5A
			// (set) Token: 0x06007655 RID: 30293 RVA: 0x00197A62 File Offset: 0x00195C62
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x1700154F RID: 5455
			// (get) Token: 0x06007656 RID: 30294 RVA: 0x00197A6B File Offset: 0x00195C6B
			// (set) Token: 0x06007657 RID: 30295 RVA: 0x00197A73 File Offset: 0x00195C73
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000FF3 RID: 4083
			public class Joins
			{
				// Token: 0x06007658 RID: 30296 RVA: 0x00197A7C File Offset: 0x00195C7C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06007659 RID: 30297 RVA: 0x00197A8B File Offset: 0x00195C8B
				public ProgramSetBuilder<resultSequence> ConvertToWebRegions(ProgramSetBuilder<nodeCollection> value0)
				{
					return ProgramSetBuilder<resultSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConvertToWebRegions, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600765A RID: 30298 RVA: 0x00197ABC File Offset: 0x00195CBC
				public ProgramSetBuilder<resultSequence> Union(ProgramSetBuilder<resultSequence> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return ProgramSetBuilder<resultSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Union, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600765B RID: 30299 RVA: 0x00197AFC File Offset: 0x00195CFC
				public ProgramSetBuilder<resultSequence> EmptySequence()
				{
					return ProgramSetBuilder<resultSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EmptySequence, Array.Empty<ProgramSet>()));
				}

				// Token: 0x0600765C RID: 30300 RVA: 0x00197B1D File Offset: 0x00195D1D
				public ProgramSetBuilder<subNode> NodeToWebRegion(ProgramSetBuilder<beginNode> value0)
				{
					return ProgramSetBuilder<subNode>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeToWebRegion, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600765D RID: 30301 RVA: 0x00197B4E File Offset: 0x00195D4E
				public ProgramSetBuilder<mapNodeInSequence> NodeToWebRegionInSequence(ProgramSetBuilder<node> value0)
				{
					return ProgramSetBuilder<mapNodeInSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeToWebRegionInSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600765E RID: 30302 RVA: 0x00197B7F File Offset: 0x00195D7F
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0> NodeRegionToWebRegion(ProgramSetBuilder<regionStart> value0, ProgramSetBuilder<endNode> value1)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeRegionToWebRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600765F RID: 30303 RVA: 0x00197BBF File Offset: 0x00195DBF
				public ProgramSetBuilder<mapRegionInSequence> NodeRegionToWebRegionInSequence(ProgramSetBuilder<regionStart> value0, ProgramSetBuilder<endNode> value1)
				{
					return ProgramSetBuilder<mapRegionInSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeRegionToWebRegionInSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007660 RID: 30304 RVA: 0x00197BFF File Offset: 0x00195DFF
				public ProgramSetBuilder<selection> SingleSelection1(ProgramSetBuilder<filterSelection> value0)
				{
					return ProgramSetBuilder<selection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSelection1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007661 RID: 30305 RVA: 0x00197C30 File Offset: 0x00195E30
				public ProgramSetBuilder<selection> DisjSelection1(ProgramSetBuilder<selection> value0, ProgramSetBuilder<filterSelection> value1)
				{
					return ProgramSetBuilder<selection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSelection1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007662 RID: 30306 RVA: 0x00197C70 File Offset: 0x00195E70
				public ProgramSetBuilder<selection> CSSSelection(ProgramSetBuilder<cssSelector> value0, ProgramSetBuilder<allNodes> value1)
				{
					return ProgramSetBuilder<selection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.CSSSelection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007663 RID: 30307 RVA: 0x00197CB0 File Offset: 0x00195EB0
				public ProgramSetBuilder<regionStartSiblings> YoungerSiblingsOf(ProgramSetBuilder<regionStart> value0)
				{
					return ProgramSetBuilder<regionStartSiblings>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.YoungerSiblingsOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007664 RID: 30308 RVA: 0x00197CE1 File Offset: 0x00195EE1
				public ProgramSetBuilder<selection2> LeafChildrenOf1(ProgramSetBuilder<selection3> value0)
				{
					return ProgramSetBuilder<selection2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafChildrenOf1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007665 RID: 30309 RVA: 0x00197D12 File Offset: 0x00195F12
				public ProgramSetBuilder<selection3> SingleSelection2(ProgramSetBuilder<filterSelection2> value0)
				{
					return ProgramSetBuilder<selection3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSelection2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007666 RID: 30310 RVA: 0x00197D43 File Offset: 0x00195F43
				public ProgramSetBuilder<selection3> DisjSelection2(ProgramSetBuilder<selection3> value0, ProgramSetBuilder<filterSelection2> value1)
				{
					return ProgramSetBuilder<selection3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSelection2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007667 RID: 30311 RVA: 0x00197D83 File Offset: 0x00195F83
				public ProgramSetBuilder<selection4> LeafChildrenOf2(ProgramSetBuilder<selection5> value0)
				{
					return ProgramSetBuilder<selection4>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafChildrenOf2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007668 RID: 30312 RVA: 0x00197DB4 File Offset: 0x00195FB4
				public ProgramSetBuilder<selection5> SingleSelection3(ProgramSetBuilder<filterSelection3> value0)
				{
					return ProgramSetBuilder<selection5>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSelection3, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007669 RID: 30313 RVA: 0x00197DE5 File Offset: 0x00195FE5
				public ProgramSetBuilder<selection5> DisjSelection3(ProgramSetBuilder<selection5> value0, ProgramSetBuilder<filterSelection3> value1)
				{
					return ProgramSetBuilder<selection5>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSelection3, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600766A RID: 30314 RVA: 0x00197E25 File Offset: 0x00196025
				public ProgramSetBuilder<selection6> LeafChildrenOf3(ProgramSetBuilder<selection7> value0)
				{
					return ProgramSetBuilder<selection6>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafChildrenOf3, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600766B RID: 30315 RVA: 0x00197E56 File Offset: 0x00196056
				public ProgramSetBuilder<selection7> SingleSelection4(ProgramSetBuilder<filterSelection4> value0)
				{
					return ProgramSetBuilder<selection7>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSelection4, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600766C RID: 30316 RVA: 0x00197E87 File Offset: 0x00196087
				public ProgramSetBuilder<selection7> DisjSelection4(ProgramSetBuilder<selection7> value0, ProgramSetBuilder<filterSelection4> value1)
				{
					return ProgramSetBuilder<selection7>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSelection4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600766D RID: 30317 RVA: 0x00197EC7 File Offset: 0x001960C7
				public ProgramSetBuilder<selection8> LeafChildrenOf4(ProgramSetBuilder<selection9> value0)
				{
					return ProgramSetBuilder<selection8>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafChildrenOf4, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600766E RID: 30318 RVA: 0x00197EF8 File Offset: 0x001960F8
				public ProgramSetBuilder<selection9> SingleSelection5(ProgramSetBuilder<filterSelection5> value0)
				{
					return ProgramSetBuilder<selection9>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSelection5, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600766F RID: 30319 RVA: 0x00197F29 File Offset: 0x00196129
				public ProgramSetBuilder<selection9> DisjSelection5(ProgramSetBuilder<selection9> value0, ProgramSetBuilder<filterSelection5> value1)
				{
					return ProgramSetBuilder<selection9>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSelection5, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007670 RID: 30320 RVA: 0x00197F69 File Offset: 0x00196169
				public ProgramSetBuilder<atomExpr> ContainsDate(ProgramSetBuilder<node> value0)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ContainsDate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007671 RID: 30321 RVA: 0x00197F9A File Offset: 0x0019619A
				public ProgramSetBuilder<atomExpr> ContainsNum(ProgramSetBuilder<node> value0)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ContainsNum, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007672 RID: 30322 RVA: 0x00197FCB File Offset: 0x001961CB
				public ProgramSetBuilder<atomExpr> ID_substring(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ID_substring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007673 RID: 30323 RVA: 0x0019800B File Offset: 0x0019620B
				public ProgramSetBuilder<atomExpr> Class(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Class, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007674 RID: 30324 RVA: 0x0019804B File Offset: 0x0019624B
				public ProgramSetBuilder<atomExpr> TitleIs(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TitleIs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007675 RID: 30325 RVA: 0x0019808B File Offset: 0x0019628B
				public ProgramSetBuilder<atomExpr> NodeName(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeName, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007676 RID: 30326 RVA: 0x001980CB File Offset: 0x001962CB
				public ProgramSetBuilder<atomExpr> NodeNames(ProgramSetBuilder<names> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeNames, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007677 RID: 30327 RVA: 0x0019810B File Offset: 0x0019630B
				public ProgramSetBuilder<atomExpr> NthChild(ProgramSetBuilder<idx1> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NthChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007678 RID: 30328 RVA: 0x0019814B File Offset: 0x0019634B
				public ProgramSetBuilder<atomExpr> NthLastChild(ProgramSetBuilder<idx2> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NthLastChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007679 RID: 30329 RVA: 0x0019818B File Offset: 0x0019638B
				public ProgramSetBuilder<atomExpr> ContainsLeafNodes(ProgramSetBuilder<names> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ContainsLeafNodes, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600767A RID: 30330 RVA: 0x001981CB File Offset: 0x001963CB
				public ProgramSetBuilder<atomExpr> ChildrenCount(ProgramSetBuilder<count> value0, ProgramSetBuilder<node> value1)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ChildrenCount, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600767B RID: 30331 RVA: 0x0019820C File Offset: 0x0019640C
				public ProgramSetBuilder<atomExpr> HasAttribute(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<value> value1, ProgramSetBuilder<node> value2)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.HasAttribute, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600767C RID: 30332 RVA: 0x00198268 File Offset: 0x00196468
				public ProgramSetBuilder<atomExpr> HasStyle(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<value> value1, ProgramSetBuilder<node> value2)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.HasStyle, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600767D RID: 30333 RVA: 0x001982C4 File Offset: 0x001964C4
				public ProgramSetBuilder<atomExpr> HasEntityAnchor(ProgramSetBuilder<entityObjs> value0, ProgramSetBuilder<direction> value1, ProgramSetBuilder<node> value2)
				{
					return ProgramSetBuilder<atomExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.HasEntityAnchor, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600767E RID: 30334 RVA: 0x0019831E File Offset: 0x0019651E
				public ProgramSetBuilder<resultFields> AppendField(ProgramSetBuilder<resultFields> value0, ProgramSetBuilder<singletonField> value1)
				{
					return ProgramSetBuilder<resultFields>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AppendField, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600767F RID: 30335 RVA: 0x0019835E File Offset: 0x0019655E
				public ProgramSetBuilder<singletonField> TrimmedTextField(ProgramSetBuilder<resultRegion> value0)
				{
					return ProgramSetBuilder<singletonField>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TrimmedTextField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007680 RID: 30336 RVA: 0x0019838F File Offset: 0x0019658F
				public ProgramSetBuilder<singletonField> SubstringField(ProgramSetBuilder<fieldSubstring> value0)
				{
					return ProgramSetBuilder<singletonField>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SubstringField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007681 RID: 30337 RVA: 0x001983C0 File Offset: 0x001965C0
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y> GetValueSubstring(ProgramSetBuilder<resultRegion> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GetValueSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007682 RID: 30338 RVA: 0x001983F4 File Offset: 0x001965F4
				public ProgramSetBuilder<selectSubstring> SelectSubstring(ProgramSetBuilder<substringDisj> value0, ProgramSetBuilder<substringFeatureNames> value1, ProgramSetBuilder<substringFeatureValues> value2)
				{
					return ProgramSetBuilder<selectSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06007683 RID: 30339 RVA: 0x0019844E File Offset: 0x0019664E
				public ProgramSetBuilder<substringDisj> SingleSubstring(ProgramSetBuilder<substring> value0)
				{
					return ProgramSetBuilder<substringDisj>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007684 RID: 30340 RVA: 0x0019847F File Offset: 0x0019667F
				public ProgramSetBuilder<substringDisj> DisjSubstring(ProgramSetBuilder<substringDisj> value0, ProgramSetBuilder<substring> value1)
				{
					return ProgramSetBuilder<substringDisj>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DisjSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007685 RID: 30341 RVA: 0x001984BF File Offset: 0x001966BF
				public ProgramSetBuilder<resultTable> ExtractTable(ProgramSetBuilder<columnSelectors> value0)
				{
					return ProgramSetBuilder<resultTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ExtractTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007686 RID: 30342 RVA: 0x001984F0 File Offset: 0x001966F0
				public ProgramSetBuilder<resultTable> ExtractRowBasedTable(ProgramSetBuilder<columnSelectors> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return ProgramSetBuilder<resultTable>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ExtractRowBasedTable, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007687 RID: 30343 RVA: 0x00198530 File Offset: 0x00196730
				public ProgramSetBuilder<columnSelectors> SingleColumn(ProgramSetBuilder<resultSequence> value0)
				{
					return ProgramSetBuilder<columnSelectors>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007688 RID: 30344 RVA: 0x00198561 File Offset: 0x00196761
				public ProgramSetBuilder<columnSelectors> ColumnSequence(ProgramSetBuilder<columnSelectors> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return ProgramSetBuilder<columnSelectors>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ColumnSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007689 RID: 30345 RVA: 0x001985A1 File Offset: 0x001967A1
				public ProgramSetBuilder<nodeCollection> AsCollection(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AsCollection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600768A RID: 30346 RVA: 0x001985D2 File Offset: 0x001967D2
				public ProgramSetBuilder<nodeCollection> DescendantsOf(ProgramSetBuilder<nodeCollection> value0)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DescendantsOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600768B RID: 30347 RVA: 0x00198603 File Offset: 0x00196803
				public ProgramSetBuilder<nodeCollection> RightSiblingOf(ProgramSetBuilder<nodeCollection> value0)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RightSiblingOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600768C RID: 30348 RVA: 0x00198634 File Offset: 0x00196834
				public ProgramSetBuilder<nodeCollection> ClassFilter(ProgramSetBuilder<className> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ClassFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600768D RID: 30349 RVA: 0x00198674 File Offset: 0x00196874
				public ProgramSetBuilder<nodeCollection> IDFilter(ProgramSetBuilder<idName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IDFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600768E RID: 30350 RVA: 0x001986B4 File Offset: 0x001968B4
				public ProgramSetBuilder<nodeCollection> NodeNameFilter(ProgramSetBuilder<nodeName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NodeNameFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600768F RID: 30351 RVA: 0x001986F4 File Offset: 0x001968F4
				public ProgramSetBuilder<nodeCollection> ItemPropFilter(ProgramSetBuilder<propName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ItemPropFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007690 RID: 30352 RVA: 0x00198734 File Offset: 0x00196934
				public ProgramSetBuilder<nodeCollection> NthChildFilter(ProgramSetBuilder<idx1> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NthChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007691 RID: 30353 RVA: 0x00198774 File Offset: 0x00196974
				public ProgramSetBuilder<nodeCollection> NthLastChildFilter(ProgramSetBuilder<idx2> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return ProgramSetBuilder<nodeCollection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NthLastChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007692 RID: 30354 RVA: 0x001987B4 File Offset: 0x001969B4
				public ProgramSetBuilder<gen_NthChild> GEN_NthChildFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_NthChild>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_NthChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007693 RID: 30355 RVA: 0x001987F4 File Offset: 0x001969F4
				public ProgramSetBuilder<gen_NthLastChild> GEN_NthLastChildFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_NthLastChild>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_NthLastChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007694 RID: 30356 RVA: 0x00198834 File Offset: 0x00196A34
				public ProgramSetBuilder<gen_Class> GEN_ClassFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_Class>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_ClassFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007695 RID: 30357 RVA: 0x00198874 File Offset: 0x00196A74
				public ProgramSetBuilder<gen_ID> GEN_IDFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_ID>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_IDFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007696 RID: 30358 RVA: 0x001988B4 File Offset: 0x00196AB4
				public ProgramSetBuilder<gen_NodeName> GEN_NodeNameFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_NodeName>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_NodeNameFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007697 RID: 30359 RVA: 0x001988F4 File Offset: 0x00196AF4
				public ProgramSetBuilder<gen_ItemProp> GEN_ItemPropFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return ProgramSetBuilder<gen_ItemProp>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GEN_ItemPropFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007698 RID: 30360 RVA: 0x00198934 File Offset: 0x00196B34
				public ProgramSetBuilder<subNodeSequence> MapToWebRegion(ProgramSetBuilder<mapNodeInSequence> value0, ProgramSetBuilder<selection> value1)
				{
					return ProgramSetBuilder<subNodeSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MapToWebRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06007699 RID: 30361 RVA: 0x00198974 File Offset: 0x00196B74
				public ProgramSetBuilder<regionSequence> FindEndNode(ProgramSetBuilder<mapRegionInSequence> value0, ProgramSetBuilder<selection> value1)
				{
					return ProgramSetBuilder<regionSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FindEndNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769A RID: 30362 RVA: 0x001989B4 File Offset: 0x00196BB4
				public ProgramSetBuilder<beginNode> KthNodeInSelection(ProgramSetBuilder<selection> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k> value1)
				{
					return ProgramSetBuilder<beginNode>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthNodeInSelection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769B RID: 30363 RVA: 0x001989F4 File Offset: 0x00196BF4
				public ProgramSetBuilder<endNode> KthNode(ProgramSetBuilder<selectionEnd> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k> value1)
				{
					return ProgramSetBuilder<endNode>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.KthNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769C RID: 30364 RVA: 0x00198A34 File Offset: 0x00196C34
				public ProgramSetBuilder<filterSelection> LeafFilter1(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection2> value1)
				{
					return ProgramSetBuilder<filterSelection>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafFilter1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769D RID: 30365 RVA: 0x00198A74 File Offset: 0x00196C74
				public ProgramSetBuilder<selectionEnd> FilterNodesEnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<regionStartSiblings> value1)
				{
					return ProgramSetBuilder<selectionEnd>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FilterNodesEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769E RID: 30366 RVA: 0x00198AB4 File Offset: 0x00196CB4
				public ProgramSetBuilder<selectionEnd> TakeWhileNodesEnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<regionStartSiblings> value1)
				{
					return ProgramSetBuilder<selectionEnd>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TakeWhileNodesEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600769F RID: 30367 RVA: 0x00198AF4 File Offset: 0x00196CF4
				public ProgramSetBuilder<filterSelection2> LeafFilter2(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection4> value1)
				{
					return ProgramSetBuilder<filterSelection2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafFilter2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A0 RID: 30368 RVA: 0x00198B34 File Offset: 0x00196D34
				public ProgramSetBuilder<filterSelection3> LeafFilter3(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection6> value1)
				{
					return ProgramSetBuilder<filterSelection3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafFilter3, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A1 RID: 30369 RVA: 0x00198B74 File Offset: 0x00196D74
				public ProgramSetBuilder<filterSelection4> LeafFilter4(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection8> value1)
				{
					return ProgramSetBuilder<filterSelection4>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafFilter4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A2 RID: 30370 RVA: 0x00198BB4 File Offset: 0x00196DB4
				public ProgramSetBuilder<filterSelection5> LeafFilter5(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection10> value1)
				{
					return ProgramSetBuilder<filterSelection5>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafFilter5, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A3 RID: 30371 RVA: 0x00198BF4 File Offset: 0x00196DF4
				public ProgramSetBuilder<leafFExpr> LeafAnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<leafAtom> value1)
				{
					return ProgramSetBuilder<leafFExpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafAnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A4 RID: 30372 RVA: 0x00198C34 File Offset: 0x00196E34
				public ProgramSetBuilder<fexpr> And(ProgramSetBuilder<fexpr> value0, ProgramSetBuilder<literalExpr> value1)
				{
					return ProgramSetBuilder<fexpr>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.And, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A5 RID: 30373 RVA: 0x00198C74 File Offset: 0x00196E74
				public ProgramSetBuilder<substring> Substring(ProgramSetBuilder<SS> value0)
				{
					return ProgramSetBuilder<substring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Substring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076A6 RID: 30374 RVA: 0x00198CA5 File Offset: 0x00196EA5
				public ProgramSetBuilder<region> LetRegion(ProgramSetBuilder<beginNode> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0> value1)
				{
					return ProgramSetBuilder<region>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076A7 RID: 30375 RVA: 0x00198CE5 File Offset: 0x00196EE5
				public ProgramSetBuilder<fieldSubstring> LetSubstring(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y> value0, ProgramSetBuilder<selectSubstring> value1)
				{
					return ProgramSetBuilder<fieldSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003278 RID: 12920
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FF4 RID: 4084
			public class ExplicitJoins
			{
				// Token: 0x060076A8 RID: 30376 RVA: 0x00198D25 File Offset: 0x00196F25
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060076A9 RID: 30377 RVA: 0x00198D34 File Offset: 0x00196F34
				public JoinProgramSetBuilder<resultSequence> ConvertToWebRegions(ProgramSetBuilder<nodeCollection> value0)
				{
					return JoinProgramSetBuilder<resultSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConvertToWebRegions, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076AA RID: 30378 RVA: 0x00198D65 File Offset: 0x00196F65
				public JoinProgramSetBuilder<resultSequence> Union(ProgramSetBuilder<resultSequence> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return JoinProgramSetBuilder<resultSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Union, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076AB RID: 30379 RVA: 0x00198DA5 File Offset: 0x00196FA5
				public JoinProgramSetBuilder<resultSequence> EmptySequence()
				{
					return JoinProgramSetBuilder<resultSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EmptySequence, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060076AC RID: 30380 RVA: 0x00198DC6 File Offset: 0x00196FC6
				public JoinProgramSetBuilder<subNode> NodeToWebRegion(ProgramSetBuilder<beginNode> value0)
				{
					return JoinProgramSetBuilder<subNode>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeToWebRegion, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076AD RID: 30381 RVA: 0x00198DF7 File Offset: 0x00196FF7
				public JoinProgramSetBuilder<mapNodeInSequence> NodeToWebRegionInSequence(ProgramSetBuilder<node> value0)
				{
					return JoinProgramSetBuilder<mapNodeInSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeToWebRegionInSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076AE RID: 30382 RVA: 0x00198E28 File Offset: 0x00197028
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0> NodeRegionToWebRegion(ProgramSetBuilder<regionStart> value0, ProgramSetBuilder<endNode> value1)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeRegionToWebRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076AF RID: 30383 RVA: 0x00198E68 File Offset: 0x00197068
				public JoinProgramSetBuilder<mapRegionInSequence> NodeRegionToWebRegionInSequence(ProgramSetBuilder<regionStart> value0, ProgramSetBuilder<endNode> value1)
				{
					return JoinProgramSetBuilder<mapRegionInSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeRegionToWebRegionInSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076B0 RID: 30384 RVA: 0x00198EA8 File Offset: 0x001970A8
				public JoinProgramSetBuilder<selection> SingleSelection1(ProgramSetBuilder<filterSelection> value0)
				{
					return JoinProgramSetBuilder<selection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSelection1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B1 RID: 30385 RVA: 0x00198ED9 File Offset: 0x001970D9
				public JoinProgramSetBuilder<selection> DisjSelection1(ProgramSetBuilder<selection> value0, ProgramSetBuilder<filterSelection> value1)
				{
					return JoinProgramSetBuilder<selection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSelection1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076B2 RID: 30386 RVA: 0x00198F19 File Offset: 0x00197119
				public JoinProgramSetBuilder<selection> CSSSelection(ProgramSetBuilder<cssSelector> value0, ProgramSetBuilder<allNodes> value1)
				{
					return JoinProgramSetBuilder<selection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.CSSSelection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076B3 RID: 30387 RVA: 0x00198F59 File Offset: 0x00197159
				public JoinProgramSetBuilder<regionStartSiblings> YoungerSiblingsOf(ProgramSetBuilder<regionStart> value0)
				{
					return JoinProgramSetBuilder<regionStartSiblings>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.YoungerSiblingsOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B4 RID: 30388 RVA: 0x00198F8A File Offset: 0x0019718A
				public JoinProgramSetBuilder<selection2> LeafChildrenOf1(ProgramSetBuilder<selection3> value0)
				{
					return JoinProgramSetBuilder<selection2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafChildrenOf1, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B5 RID: 30389 RVA: 0x00198FBB File Offset: 0x001971BB
				public JoinProgramSetBuilder<selection3> SingleSelection2(ProgramSetBuilder<filterSelection2> value0)
				{
					return JoinProgramSetBuilder<selection3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSelection2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B6 RID: 30390 RVA: 0x00198FEC File Offset: 0x001971EC
				public JoinProgramSetBuilder<selection3> DisjSelection2(ProgramSetBuilder<selection3> value0, ProgramSetBuilder<filterSelection2> value1)
				{
					return JoinProgramSetBuilder<selection3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSelection2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076B7 RID: 30391 RVA: 0x0019902C File Offset: 0x0019722C
				public JoinProgramSetBuilder<selection4> LeafChildrenOf2(ProgramSetBuilder<selection5> value0)
				{
					return JoinProgramSetBuilder<selection4>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafChildrenOf2, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B8 RID: 30392 RVA: 0x0019905D File Offset: 0x0019725D
				public JoinProgramSetBuilder<selection5> SingleSelection3(ProgramSetBuilder<filterSelection3> value0)
				{
					return JoinProgramSetBuilder<selection5>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSelection3, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076B9 RID: 30393 RVA: 0x0019908E File Offset: 0x0019728E
				public JoinProgramSetBuilder<selection5> DisjSelection3(ProgramSetBuilder<selection5> value0, ProgramSetBuilder<filterSelection3> value1)
				{
					return JoinProgramSetBuilder<selection5>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSelection3, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076BA RID: 30394 RVA: 0x001990CE File Offset: 0x001972CE
				public JoinProgramSetBuilder<selection6> LeafChildrenOf3(ProgramSetBuilder<selection7> value0)
				{
					return JoinProgramSetBuilder<selection6>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafChildrenOf3, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076BB RID: 30395 RVA: 0x001990FF File Offset: 0x001972FF
				public JoinProgramSetBuilder<selection7> SingleSelection4(ProgramSetBuilder<filterSelection4> value0)
				{
					return JoinProgramSetBuilder<selection7>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSelection4, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076BC RID: 30396 RVA: 0x00199130 File Offset: 0x00197330
				public JoinProgramSetBuilder<selection7> DisjSelection4(ProgramSetBuilder<selection7> value0, ProgramSetBuilder<filterSelection4> value1)
				{
					return JoinProgramSetBuilder<selection7>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSelection4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076BD RID: 30397 RVA: 0x00199170 File Offset: 0x00197370
				public JoinProgramSetBuilder<selection8> LeafChildrenOf4(ProgramSetBuilder<selection9> value0)
				{
					return JoinProgramSetBuilder<selection8>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafChildrenOf4, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076BE RID: 30398 RVA: 0x001991A1 File Offset: 0x001973A1
				public JoinProgramSetBuilder<selection9> SingleSelection5(ProgramSetBuilder<filterSelection5> value0)
				{
					return JoinProgramSetBuilder<selection9>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSelection5, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076BF RID: 30399 RVA: 0x001991D2 File Offset: 0x001973D2
				public JoinProgramSetBuilder<selection9> DisjSelection5(ProgramSetBuilder<selection9> value0, ProgramSetBuilder<filterSelection5> value1)
				{
					return JoinProgramSetBuilder<selection9>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSelection5, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C0 RID: 30400 RVA: 0x00199212 File Offset: 0x00197412
				public JoinProgramSetBuilder<atomExpr> ContainsDate(ProgramSetBuilder<node> value0)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ContainsDate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076C1 RID: 30401 RVA: 0x00199243 File Offset: 0x00197443
				public JoinProgramSetBuilder<atomExpr> ContainsNum(ProgramSetBuilder<node> value0)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ContainsNum, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076C2 RID: 30402 RVA: 0x00199274 File Offset: 0x00197474
				public JoinProgramSetBuilder<atomExpr> ID_substring(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ID_substring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C3 RID: 30403 RVA: 0x001992B4 File Offset: 0x001974B4
				public JoinProgramSetBuilder<atomExpr> Class(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Class, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C4 RID: 30404 RVA: 0x001992F4 File Offset: 0x001974F4
				public JoinProgramSetBuilder<atomExpr> TitleIs(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TitleIs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C5 RID: 30405 RVA: 0x00199334 File Offset: 0x00197534
				public JoinProgramSetBuilder<atomExpr> NodeName(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeName, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C6 RID: 30406 RVA: 0x00199374 File Offset: 0x00197574
				public JoinProgramSetBuilder<atomExpr> NodeNames(ProgramSetBuilder<names> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeNames, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C7 RID: 30407 RVA: 0x001993B4 File Offset: 0x001975B4
				public JoinProgramSetBuilder<atomExpr> NthChild(ProgramSetBuilder<idx1> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NthChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C8 RID: 30408 RVA: 0x001993F4 File Offset: 0x001975F4
				public JoinProgramSetBuilder<atomExpr> NthLastChild(ProgramSetBuilder<idx2> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NthLastChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076C9 RID: 30409 RVA: 0x00199434 File Offset: 0x00197634
				public JoinProgramSetBuilder<atomExpr> ContainsLeafNodes(ProgramSetBuilder<names> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ContainsLeafNodes, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076CA RID: 30410 RVA: 0x00199474 File Offset: 0x00197674
				public JoinProgramSetBuilder<atomExpr> ChildrenCount(ProgramSetBuilder<count> value0, ProgramSetBuilder<node> value1)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ChildrenCount, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076CB RID: 30411 RVA: 0x001994B4 File Offset: 0x001976B4
				public JoinProgramSetBuilder<atomExpr> HasAttribute(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<value> value1, ProgramSetBuilder<node> value2)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.HasAttribute, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060076CC RID: 30412 RVA: 0x00199510 File Offset: 0x00197710
				public JoinProgramSetBuilder<atomExpr> HasStyle(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> value0, ProgramSetBuilder<value> value1, ProgramSetBuilder<node> value2)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.HasStyle, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060076CD RID: 30413 RVA: 0x0019956C File Offset: 0x0019776C
				public JoinProgramSetBuilder<atomExpr> HasEntityAnchor(ProgramSetBuilder<entityObjs> value0, ProgramSetBuilder<direction> value1, ProgramSetBuilder<node> value2)
				{
					return JoinProgramSetBuilder<atomExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.HasEntityAnchor, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060076CE RID: 30414 RVA: 0x001995C6 File Offset: 0x001977C6
				public JoinProgramSetBuilder<resultFields> AppendField(ProgramSetBuilder<resultFields> value0, ProgramSetBuilder<singletonField> value1)
				{
					return JoinProgramSetBuilder<resultFields>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AppendField, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076CF RID: 30415 RVA: 0x00199606 File Offset: 0x00197806
				public JoinProgramSetBuilder<singletonField> TrimmedTextField(ProgramSetBuilder<resultRegion> value0)
				{
					return JoinProgramSetBuilder<singletonField>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TrimmedTextField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D0 RID: 30416 RVA: 0x00199637 File Offset: 0x00197837
				public JoinProgramSetBuilder<singletonField> SubstringField(ProgramSetBuilder<fieldSubstring> value0)
				{
					return JoinProgramSetBuilder<singletonField>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SubstringField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D1 RID: 30417 RVA: 0x00199668 File Offset: 0x00197868
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y> GetValueSubstring(ProgramSetBuilder<resultRegion> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GetValueSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D2 RID: 30418 RVA: 0x0019969C File Offset: 0x0019789C
				public JoinProgramSetBuilder<selectSubstring> SelectSubstring(ProgramSetBuilder<substringDisj> value0, ProgramSetBuilder<substringFeatureNames> value1, ProgramSetBuilder<substringFeatureValues> value2)
				{
					return JoinProgramSetBuilder<selectSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060076D3 RID: 30419 RVA: 0x001996F6 File Offset: 0x001978F6
				public JoinProgramSetBuilder<substringDisj> SingleSubstring(ProgramSetBuilder<substring> value0)
				{
					return JoinProgramSetBuilder<substringDisj>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D4 RID: 30420 RVA: 0x00199727 File Offset: 0x00197927
				public JoinProgramSetBuilder<substringDisj> DisjSubstring(ProgramSetBuilder<substringDisj> value0, ProgramSetBuilder<substring> value1)
				{
					return JoinProgramSetBuilder<substringDisj>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DisjSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076D5 RID: 30421 RVA: 0x00199767 File Offset: 0x00197967
				public JoinProgramSetBuilder<resultTable> ExtractTable(ProgramSetBuilder<columnSelectors> value0)
				{
					return JoinProgramSetBuilder<resultTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ExtractTable, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D6 RID: 30422 RVA: 0x00199798 File Offset: 0x00197998
				public JoinProgramSetBuilder<resultTable> ExtractRowBasedTable(ProgramSetBuilder<columnSelectors> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return JoinProgramSetBuilder<resultTable>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ExtractRowBasedTable, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076D7 RID: 30423 RVA: 0x001997D8 File Offset: 0x001979D8
				public JoinProgramSetBuilder<columnSelectors> SingleColumn(ProgramSetBuilder<resultSequence> value0)
				{
					return JoinProgramSetBuilder<columnSelectors>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076D8 RID: 30424 RVA: 0x00199809 File Offset: 0x00197A09
				public JoinProgramSetBuilder<columnSelectors> ColumnSequence(ProgramSetBuilder<columnSelectors> value0, ProgramSetBuilder<resultSequence> value1)
				{
					return JoinProgramSetBuilder<columnSelectors>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ColumnSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076D9 RID: 30425 RVA: 0x00199849 File Offset: 0x00197A49
				public JoinProgramSetBuilder<nodeCollection> AsCollection(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AsCollection, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076DA RID: 30426 RVA: 0x0019987A File Offset: 0x00197A7A
				public JoinProgramSetBuilder<nodeCollection> DescendantsOf(ProgramSetBuilder<nodeCollection> value0)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DescendantsOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076DB RID: 30427 RVA: 0x001998AB File Offset: 0x00197AAB
				public JoinProgramSetBuilder<nodeCollection> RightSiblingOf(ProgramSetBuilder<nodeCollection> value0)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RightSiblingOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076DC RID: 30428 RVA: 0x001998DC File Offset: 0x00197ADC
				public JoinProgramSetBuilder<nodeCollection> ClassFilter(ProgramSetBuilder<className> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ClassFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076DD RID: 30429 RVA: 0x0019991C File Offset: 0x00197B1C
				public JoinProgramSetBuilder<nodeCollection> IDFilter(ProgramSetBuilder<idName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IDFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076DE RID: 30430 RVA: 0x0019995C File Offset: 0x00197B5C
				public JoinProgramSetBuilder<nodeCollection> NodeNameFilter(ProgramSetBuilder<nodeName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NodeNameFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076DF RID: 30431 RVA: 0x0019999C File Offset: 0x00197B9C
				public JoinProgramSetBuilder<nodeCollection> ItemPropFilter(ProgramSetBuilder<propName> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ItemPropFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E0 RID: 30432 RVA: 0x001999DC File Offset: 0x00197BDC
				public JoinProgramSetBuilder<nodeCollection> NthChildFilter(ProgramSetBuilder<idx1> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NthChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E1 RID: 30433 RVA: 0x00199A1C File Offset: 0x00197C1C
				public JoinProgramSetBuilder<nodeCollection> NthLastChildFilter(ProgramSetBuilder<idx2> value0, ProgramSetBuilder<nodeCollection> value1)
				{
					return JoinProgramSetBuilder<nodeCollection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NthLastChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E2 RID: 30434 RVA: 0x00199A5C File Offset: 0x00197C5C
				public JoinProgramSetBuilder<gen_NthChild> GEN_NthChildFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_NthChild>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_NthChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E3 RID: 30435 RVA: 0x00199A9C File Offset: 0x00197C9C
				public JoinProgramSetBuilder<gen_NthLastChild> GEN_NthLastChildFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_NthLastChild>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_NthLastChildFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E4 RID: 30436 RVA: 0x00199ADC File Offset: 0x00197CDC
				public JoinProgramSetBuilder<gen_Class> GEN_ClassFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_Class>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_ClassFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E5 RID: 30437 RVA: 0x00199B1C File Offset: 0x00197D1C
				public JoinProgramSetBuilder<gen_ID> GEN_IDFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_ID>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_IDFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E6 RID: 30438 RVA: 0x00199B5C File Offset: 0x00197D5C
				public JoinProgramSetBuilder<gen_NodeName> GEN_NodeNameFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_NodeName>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_NodeNameFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E7 RID: 30439 RVA: 0x00199B9C File Offset: 0x00197D9C
				public JoinProgramSetBuilder<gen_ItemProp> GEN_ItemPropFilter(ProgramSetBuilder<obj> value0, ProgramSetBuilder<obj> value1)
				{
					return JoinProgramSetBuilder<gen_ItemProp>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GEN_ItemPropFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E8 RID: 30440 RVA: 0x00199BDC File Offset: 0x00197DDC
				public JoinProgramSetBuilder<subNodeSequence> MapToWebRegion(ProgramSetBuilder<mapNodeInSequence> value0, ProgramSetBuilder<selection> value1)
				{
					return JoinProgramSetBuilder<subNodeSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MapToWebRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076E9 RID: 30441 RVA: 0x00199C1C File Offset: 0x00197E1C
				public JoinProgramSetBuilder<regionSequence> FindEndNode(ProgramSetBuilder<mapRegionInSequence> value0, ProgramSetBuilder<selection> value1)
				{
					return JoinProgramSetBuilder<regionSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FindEndNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076EA RID: 30442 RVA: 0x00199C5C File Offset: 0x00197E5C
				public JoinProgramSetBuilder<beginNode> KthNodeInSelection(ProgramSetBuilder<selection> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k> value1)
				{
					return JoinProgramSetBuilder<beginNode>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthNodeInSelection, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076EB RID: 30443 RVA: 0x00199C9C File Offset: 0x00197E9C
				public JoinProgramSetBuilder<endNode> KthNode(ProgramSetBuilder<selectionEnd> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k> value1)
				{
					return JoinProgramSetBuilder<endNode>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.KthNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076EC RID: 30444 RVA: 0x00199CDC File Offset: 0x00197EDC
				public JoinProgramSetBuilder<filterSelection> LeafFilter1(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection2> value1)
				{
					return JoinProgramSetBuilder<filterSelection>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafFilter1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076ED RID: 30445 RVA: 0x00199D1C File Offset: 0x00197F1C
				public JoinProgramSetBuilder<selectionEnd> FilterNodesEnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<regionStartSiblings> value1)
				{
					return JoinProgramSetBuilder<selectionEnd>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FilterNodesEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076EE RID: 30446 RVA: 0x00199D5C File Offset: 0x00197F5C
				public JoinProgramSetBuilder<selectionEnd> TakeWhileNodesEnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<regionStartSiblings> value1)
				{
					return JoinProgramSetBuilder<selectionEnd>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TakeWhileNodesEnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076EF RID: 30447 RVA: 0x00199D9C File Offset: 0x00197F9C
				public JoinProgramSetBuilder<filterSelection2> LeafFilter2(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection4> value1)
				{
					return JoinProgramSetBuilder<filterSelection2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafFilter2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F0 RID: 30448 RVA: 0x00199DDC File Offset: 0x00197FDC
				public JoinProgramSetBuilder<filterSelection3> LeafFilter3(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection6> value1)
				{
					return JoinProgramSetBuilder<filterSelection3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafFilter3, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F1 RID: 30449 RVA: 0x00199E1C File Offset: 0x0019801C
				public JoinProgramSetBuilder<filterSelection4> LeafFilter4(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection8> value1)
				{
					return JoinProgramSetBuilder<filterSelection4>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafFilter4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F2 RID: 30450 RVA: 0x00199E5C File Offset: 0x0019805C
				public JoinProgramSetBuilder<filterSelection5> LeafFilter5(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<selection10> value1)
				{
					return JoinProgramSetBuilder<filterSelection5>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafFilter5, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F3 RID: 30451 RVA: 0x00199E9C File Offset: 0x0019809C
				public JoinProgramSetBuilder<leafFExpr> LeafAnd(ProgramSetBuilder<leafFExpr> value0, ProgramSetBuilder<leafAtom> value1)
				{
					return JoinProgramSetBuilder<leafFExpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafAnd, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F4 RID: 30452 RVA: 0x00199EDC File Offset: 0x001980DC
				public JoinProgramSetBuilder<fexpr> And(ProgramSetBuilder<fexpr> value0, ProgramSetBuilder<literalExpr> value1)
				{
					return JoinProgramSetBuilder<fexpr>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.And, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F5 RID: 30453 RVA: 0x00199F1C File Offset: 0x0019811C
				public JoinProgramSetBuilder<substring> Substring(ProgramSetBuilder<SS> value0)
				{
					return JoinProgramSetBuilder<substring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Substring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076F6 RID: 30454 RVA: 0x00199F4D File Offset: 0x0019814D
				public JoinProgramSetBuilder<region> LetRegion(ProgramSetBuilder<beginNode> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0> value1)
				{
					return JoinProgramSetBuilder<region>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetRegion, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060076F7 RID: 30455 RVA: 0x00199F8D File Offset: 0x0019818D
				public JoinProgramSetBuilder<fieldSubstring> LetSubstring(ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y> value0, ProgramSetBuilder<selectSubstring> value1)
				{
					return JoinProgramSetBuilder<fieldSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSubstring, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003279 RID: 12921
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FF5 RID: 4085
			public class JoinUnnamedConversions
			{
				// Token: 0x060076F8 RID: 30456 RVA: 0x00199FCD File Offset: 0x001981CD
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060076F9 RID: 30457 RVA: 0x00199FDC File Offset: 0x001981DC
				public ProgramSetBuilder<resultSequence> resultSequence_subNodeSequence(ProgramSetBuilder<subNodeSequence> value0)
				{
					return ProgramSetBuilder<resultSequence>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.resultSequence_subNodeSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FA RID: 30458 RVA: 0x0019A00D File Offset: 0x0019820D
				public ProgramSetBuilder<resultSequence> resultSequence_regionSequence(ProgramSetBuilder<regionSequence> value0)
				{
					return ProgramSetBuilder<resultSequence>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.resultSequence_regionSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FB RID: 30459 RVA: 0x0019A03E File Offset: 0x0019823E
				public ProgramSetBuilder<resultRegion> resultRegion_subNode(ProgramSetBuilder<subNode> value0)
				{
					return ProgramSetBuilder<resultRegion>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.resultRegion_subNode, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FC RID: 30460 RVA: 0x0019A06F File Offset: 0x0019826F
				public ProgramSetBuilder<resultRegion> resultRegion_region(ProgramSetBuilder<region> value0)
				{
					return ProgramSetBuilder<resultRegion>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.resultRegion_region, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FD RID: 30461 RVA: 0x0019A0A0 File Offset: 0x001982A0
				public ProgramSetBuilder<selectionEnd> selectionEnd_regionStartSiblings(ProgramSetBuilder<regionStartSiblings> value0)
				{
					return ProgramSetBuilder<selectionEnd>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selectionEnd_regionStartSiblings, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FE RID: 30462 RVA: 0x0019A0D1 File Offset: 0x001982D1
				public ProgramSetBuilder<selection2> selection2_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<selection2>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selection2_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060076FF RID: 30463 RVA: 0x0019A102 File Offset: 0x00198302
				public ProgramSetBuilder<selection4> selection4_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<selection4>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selection4_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007700 RID: 30464 RVA: 0x0019A133 File Offset: 0x00198333
				public ProgramSetBuilder<selection6> selection6_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<selection6>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selection6_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007701 RID: 30465 RVA: 0x0019A164 File Offset: 0x00198364
				public ProgramSetBuilder<selection8> selection8_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<selection8>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selection8_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007702 RID: 30466 RVA: 0x0019A195 File Offset: 0x00198395
				public ProgramSetBuilder<selection10> selection10_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return ProgramSetBuilder<selection10>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selection10_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007703 RID: 30467 RVA: 0x0019A1C6 File Offset: 0x001983C6
				public ProgramSetBuilder<leafFExpr> leafFExpr_leafAtom(ProgramSetBuilder<leafAtom> value0)
				{
					return ProgramSetBuilder<leafFExpr>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.leafFExpr_leafAtom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007704 RID: 30468 RVA: 0x0019A1F7 File Offset: 0x001983F7
				public ProgramSetBuilder<leafAtom> leafAtom_literalExpr(ProgramSetBuilder<literalExpr> value0)
				{
					return ProgramSetBuilder<leafAtom>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.leafAtom_literalExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007705 RID: 30469 RVA: 0x0019A228 File Offset: 0x00198428
				public ProgramSetBuilder<literalExpr> literalExpr_atomExpr(ProgramSetBuilder<atomExpr> value0)
				{
					return ProgramSetBuilder<literalExpr>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.literalExpr_atomExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007706 RID: 30470 RVA: 0x0019A259 File Offset: 0x00198459
				public ProgramSetBuilder<fexpr> fexpr_literalExpr(ProgramSetBuilder<literalExpr> value0)
				{
					return ProgramSetBuilder<fexpr>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.fexpr_literalExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007707 RID: 30471 RVA: 0x0019A28A File Offset: 0x0019848A
				public ProgramSetBuilder<resultFields> resultFields_singletonField(ProgramSetBuilder<singletonField> value0)
				{
					return ProgramSetBuilder<resultFields>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.resultFields_singletonField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0400327A RID: 12922
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FF6 RID: 4086
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06007708 RID: 30472 RVA: 0x0019A2BB File Offset: 0x001984BB
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06007709 RID: 30473 RVA: 0x0019A2CA File Offset: 0x001984CA
				public JoinProgramSetBuilder<resultSequence> resultSequence_subNodeSequence(ProgramSetBuilder<subNodeSequence> value0)
				{
					return JoinProgramSetBuilder<resultSequence>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.resultSequence_subNodeSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770A RID: 30474 RVA: 0x0019A2FB File Offset: 0x001984FB
				public JoinProgramSetBuilder<resultSequence> resultSequence_regionSequence(ProgramSetBuilder<regionSequence> value0)
				{
					return JoinProgramSetBuilder<resultSequence>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.resultSequence_regionSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770B RID: 30475 RVA: 0x0019A32C File Offset: 0x0019852C
				public JoinProgramSetBuilder<resultRegion> resultRegion_subNode(ProgramSetBuilder<subNode> value0)
				{
					return JoinProgramSetBuilder<resultRegion>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.resultRegion_subNode, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770C RID: 30476 RVA: 0x0019A35D File Offset: 0x0019855D
				public JoinProgramSetBuilder<resultRegion> resultRegion_region(ProgramSetBuilder<region> value0)
				{
					return JoinProgramSetBuilder<resultRegion>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.resultRegion_region, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770D RID: 30477 RVA: 0x0019A38E File Offset: 0x0019858E
				public JoinProgramSetBuilder<selectionEnd> selectionEnd_regionStartSiblings(ProgramSetBuilder<regionStartSiblings> value0)
				{
					return JoinProgramSetBuilder<selectionEnd>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selectionEnd_regionStartSiblings, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770E RID: 30478 RVA: 0x0019A3BF File Offset: 0x001985BF
				public JoinProgramSetBuilder<selection2> selection2_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<selection2>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selection2_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600770F RID: 30479 RVA: 0x0019A3F0 File Offset: 0x001985F0
				public JoinProgramSetBuilder<selection4> selection4_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<selection4>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selection4_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007710 RID: 30480 RVA: 0x0019A421 File Offset: 0x00198621
				public JoinProgramSetBuilder<selection6> selection6_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<selection6>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selection6_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007711 RID: 30481 RVA: 0x0019A452 File Offset: 0x00198652
				public JoinProgramSetBuilder<selection8> selection8_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<selection8>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selection8_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007712 RID: 30482 RVA: 0x0019A483 File Offset: 0x00198683
				public JoinProgramSetBuilder<selection10> selection10_allNodes(ProgramSetBuilder<allNodes> value0)
				{
					return JoinProgramSetBuilder<selection10>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selection10_allNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007713 RID: 30483 RVA: 0x0019A4B4 File Offset: 0x001986B4
				public JoinProgramSetBuilder<leafFExpr> leafFExpr_leafAtom(ProgramSetBuilder<leafAtom> value0)
				{
					return JoinProgramSetBuilder<leafFExpr>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.leafFExpr_leafAtom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007714 RID: 30484 RVA: 0x0019A4E5 File Offset: 0x001986E5
				public JoinProgramSetBuilder<leafAtom> leafAtom_literalExpr(ProgramSetBuilder<literalExpr> value0)
				{
					return JoinProgramSetBuilder<leafAtom>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.leafAtom_literalExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007715 RID: 30485 RVA: 0x0019A516 File Offset: 0x00198716
				public JoinProgramSetBuilder<literalExpr> literalExpr_atomExpr(ProgramSetBuilder<atomExpr> value0)
				{
					return JoinProgramSetBuilder<literalExpr>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.literalExpr_atomExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007716 RID: 30486 RVA: 0x0019A547 File Offset: 0x00198747
				public JoinProgramSetBuilder<fexpr> fexpr_literalExpr(ProgramSetBuilder<literalExpr> value0)
				{
					return JoinProgramSetBuilder<fexpr>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.fexpr_literalExpr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06007717 RID: 30487 RVA: 0x0019A578 File Offset: 0x00198778
				public JoinProgramSetBuilder<resultFields> resultFields_singletonField(ProgramSetBuilder<singletonField> value0)
				{
					return JoinProgramSetBuilder<resultFields>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.resultFields_singletonField, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0400327B RID: 12923
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000FF7 RID: 4087
			public class Casts
			{
				// Token: 0x06007718 RID: 30488 RVA: 0x0019A5A9 File Offset: 0x001987A9
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06007719 RID: 30489 RVA: 0x0019A5B8 File Offset: 0x001987B8
				public ProgramSetBuilder<resultSequence> resultSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.resultSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol resultSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultSequence>.CreateUnsafe(set);
				}

				// Token: 0x0600771A RID: 30490 RVA: 0x0019A610 File Offset: 0x00198810
				public ProgramSetBuilder<resultRegion> resultRegion(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.resultRegion)
					{
						string text = "set";
						string text2 = "expected program set for symbol resultRegion but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultRegion>.CreateUnsafe(set);
				}

				// Token: 0x0600771B RID: 30491 RVA: 0x0019A668 File Offset: 0x00198868
				public ProgramSetBuilder<subNodeSequence> subNodeSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.subNodeSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol subNodeSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNodeSequence>.CreateUnsafe(set);
				}

				// Token: 0x0600771C RID: 30492 RVA: 0x0019A6C0 File Offset: 0x001988C0
				public ProgramSetBuilder<node> node(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.node)
					{
						string text = "set";
						string text2 = "expected program set for symbol node but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.node>.CreateUnsafe(set);
				}

				// Token: 0x0600771D RID: 30493 RVA: 0x0019A718 File Offset: 0x00198918
				public ProgramSetBuilder<subNode> subNode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.subNode)
					{
						string text = "set";
						string text2 = "expected program set for symbol subNode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.subNode>.CreateUnsafe(set);
				}

				// Token: 0x0600771E RID: 30494 RVA: 0x0019A770 File Offset: 0x00198970
				public ProgramSetBuilder<mapNodeInSequence> mapNodeInSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mapNodeInSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol mapNodeInSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapNodeInSequence>.CreateUnsafe(set);
				}

				// Token: 0x0600771F RID: 30495 RVA: 0x0019A7C8 File Offset: 0x001989C8
				public ProgramSetBuilder<regionSequence> regionSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regionSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol regionSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionSequence>.CreateUnsafe(set);
				}

				// Token: 0x06007720 RID: 30496 RVA: 0x0019A820 File Offset: 0x00198A20
				public ProgramSetBuilder<regionStart> regionStart(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regionStart)
					{
						string text = "set";
						string text2 = "expected program set for symbol regionStart but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStart>.CreateUnsafe(set);
				}

				// Token: 0x06007721 RID: 30497 RVA: 0x0019A878 File Offset: 0x00198A78
				public ProgramSetBuilder<region> region(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.region)
					{
						string text = "set";
						string text2 = "expected program set for symbol region but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.region>.CreateUnsafe(set);
				}

				// Token: 0x06007722 RID: 30498 RVA: 0x0019A8D0 File Offset: 0x00198AD0
				public ProgramSetBuilder<mapRegionInSequence> mapRegionInSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.mapRegionInSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol mapRegionInSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.mapRegionInSequence>.CreateUnsafe(set);
				}

				// Token: 0x06007723 RID: 30499 RVA: 0x0019A928 File Offset: 0x00198B28
				public ProgramSetBuilder<beginNode> beginNode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.beginNode)
					{
						string text = "set";
						string text2 = "expected program set for symbol beginNode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.beginNode>.CreateUnsafe(set);
				}

				// Token: 0x06007724 RID: 30500 RVA: 0x0019A980 File Offset: 0x00198B80
				public ProgramSetBuilder<endNode> endNode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.endNode)
					{
						string text = "set";
						string text2 = "expected program set for symbol endNode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.endNode>.CreateUnsafe(set);
				}

				// Token: 0x06007725 RID: 30501 RVA: 0x0019A9D8 File Offset: 0x00198BD8
				public ProgramSetBuilder<selection> selection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection>.CreateUnsafe(set);
				}

				// Token: 0x06007726 RID: 30502 RVA: 0x0019AA30 File Offset: 0x00198C30
				public ProgramSetBuilder<filterSelection> filterSelection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterSelection)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterSelection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection>.CreateUnsafe(set);
				}

				// Token: 0x06007727 RID: 30503 RVA: 0x0019AA88 File Offset: 0x00198C88
				public ProgramSetBuilder<selectionEnd> selectionEnd(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectionEnd)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectionEnd but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectionEnd>.CreateUnsafe(set);
				}

				// Token: 0x06007728 RID: 30504 RVA: 0x0019AAE0 File Offset: 0x00198CE0
				public ProgramSetBuilder<regionStartSiblings> regionStartSiblings(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regionStartSiblings)
					{
						string text = "set";
						string text2 = "expected program set for symbol regionStartSiblings but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.regionStartSiblings>.CreateUnsafe(set);
				}

				// Token: 0x06007729 RID: 30505 RVA: 0x0019AB38 File Offset: 0x00198D38
				public ProgramSetBuilder<selection2> selection2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection2)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection2>.CreateUnsafe(set);
				}

				// Token: 0x0600772A RID: 30506 RVA: 0x0019AB90 File Offset: 0x00198D90
				public ProgramSetBuilder<selection3> selection3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection3)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection3>.CreateUnsafe(set);
				}

				// Token: 0x0600772B RID: 30507 RVA: 0x0019ABE8 File Offset: 0x00198DE8
				public ProgramSetBuilder<filterSelection2> filterSelection2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterSelection2)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterSelection2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection2>.CreateUnsafe(set);
				}

				// Token: 0x0600772C RID: 30508 RVA: 0x0019AC40 File Offset: 0x00198E40
				public ProgramSetBuilder<selection4> selection4(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection4)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection4 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection4>.CreateUnsafe(set);
				}

				// Token: 0x0600772D RID: 30509 RVA: 0x0019AC98 File Offset: 0x00198E98
				public ProgramSetBuilder<selection5> selection5(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection5)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection5 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection5>.CreateUnsafe(set);
				}

				// Token: 0x0600772E RID: 30510 RVA: 0x0019ACF0 File Offset: 0x00198EF0
				public ProgramSetBuilder<filterSelection3> filterSelection3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterSelection3)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterSelection3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection3>.CreateUnsafe(set);
				}

				// Token: 0x0600772F RID: 30511 RVA: 0x0019AD48 File Offset: 0x00198F48
				public ProgramSetBuilder<selection6> selection6(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection6)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection6 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection6>.CreateUnsafe(set);
				}

				// Token: 0x06007730 RID: 30512 RVA: 0x0019ADA0 File Offset: 0x00198FA0
				public ProgramSetBuilder<selection7> selection7(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection7)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection7 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection7>.CreateUnsafe(set);
				}

				// Token: 0x06007731 RID: 30513 RVA: 0x0019ADF8 File Offset: 0x00198FF8
				public ProgramSetBuilder<filterSelection4> filterSelection4(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterSelection4)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterSelection4 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection4>.CreateUnsafe(set);
				}

				// Token: 0x06007732 RID: 30514 RVA: 0x0019AE50 File Offset: 0x00199050
				public ProgramSetBuilder<selection8> selection8(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection8)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection8 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection8>.CreateUnsafe(set);
				}

				// Token: 0x06007733 RID: 30515 RVA: 0x0019AEA8 File Offset: 0x001990A8
				public ProgramSetBuilder<selection9> selection9(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection9)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection9 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection9>.CreateUnsafe(set);
				}

				// Token: 0x06007734 RID: 30516 RVA: 0x0019AF00 File Offset: 0x00199100
				public ProgramSetBuilder<filterSelection5> filterSelection5(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.filterSelection5)
					{
						string text = "set";
						string text2 = "expected program set for symbol filterSelection5 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.filterSelection5>.CreateUnsafe(set);
				}

				// Token: 0x06007735 RID: 30517 RVA: 0x0019AF58 File Offset: 0x00199158
				public ProgramSetBuilder<selection10> selection10(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selection10)
					{
						string text = "set";
						string text2 = "expected program set for symbol selection10 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selection10>.CreateUnsafe(set);
				}

				// Token: 0x06007736 RID: 30518 RVA: 0x0019AFB0 File Offset: 0x001991B0
				public ProgramSetBuilder<leafFExpr> leafFExpr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.leafFExpr)
					{
						string text = "set";
						string text2 = "expected program set for symbol leafFExpr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafFExpr>.CreateUnsafe(set);
				}

				// Token: 0x06007737 RID: 30519 RVA: 0x0019B008 File Offset: 0x00199208
				public ProgramSetBuilder<leafAtom> leafAtom(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.leafAtom)
					{
						string text = "set";
						string text2 = "expected program set for symbol leafAtom but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.leafAtom>.CreateUnsafe(set);
				}

				// Token: 0x06007738 RID: 30520 RVA: 0x0019B060 File Offset: 0x00199260
				public ProgramSetBuilder<atomExpr> atomExpr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.atomExpr)
					{
						string text = "set";
						string text2 = "expected program set for symbol atomExpr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.atomExpr>.CreateUnsafe(set);
				}

				// Token: 0x06007739 RID: 30521 RVA: 0x0019B0B8 File Offset: 0x001992B8
				public ProgramSetBuilder<literalExpr> literalExpr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.literalExpr)
					{
						string text = "set";
						string text2 = "expected program set for symbol literalExpr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.literalExpr>.CreateUnsafe(set);
				}

				// Token: 0x0600773A RID: 30522 RVA: 0x0019B110 File Offset: 0x00199310
				public ProgramSetBuilder<fexpr> fexpr(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fexpr)
					{
						string text = "set";
						string text2 = "expected program set for symbol fexpr but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fexpr>.CreateUnsafe(set);
				}

				// Token: 0x0600773B RID: 30523 RVA: 0x0019B168 File Offset: 0x00199368
				public ProgramSetBuilder<resultFields> resultFields(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.resultFields)
					{
						string text = "set";
						string text2 = "expected program set for symbol resultFields but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultFields>.CreateUnsafe(set);
				}

				// Token: 0x0600773C RID: 30524 RVA: 0x0019B1C0 File Offset: 0x001993C0
				public ProgramSetBuilder<singletonField> singletonField(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.singletonField)
					{
						string text = "set";
						string text2 = "expected program set for symbol singletonField but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.singletonField>.CreateUnsafe(set);
				}

				// Token: 0x0600773D RID: 30525 RVA: 0x0019B218 File Offset: 0x00199418
				public ProgramSetBuilder<fieldSubstring> fieldSubstring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fieldSubstring)
					{
						string text = "set";
						string text2 = "expected program set for symbol fieldSubstring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.fieldSubstring>.CreateUnsafe(set);
				}

				// Token: 0x0600773E RID: 30526 RVA: 0x0019B270 File Offset: 0x00199470
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs> cs(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.cs)
					{
						string text = "set";
						string text2 = "expected program set for symbol cs but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cs>.CreateUnsafe(set);
				}

				// Token: 0x0600773F RID: 30527 RVA: 0x0019B2C8 File Offset: 0x001994C8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y> y(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.y)
					{
						string text = "set";
						string text2 = "expected program set for symbol y but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.y>.CreateUnsafe(set);
				}

				// Token: 0x06007740 RID: 30528 RVA: 0x0019B320 File Offset: 0x00199520
				public ProgramSetBuilder<selectSubstring> selectSubstring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectSubstring)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectSubstring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.selectSubstring>.CreateUnsafe(set);
				}

				// Token: 0x06007741 RID: 30529 RVA: 0x0019B378 File Offset: 0x00199578
				public ProgramSetBuilder<substringDisj> substringDisj(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.substringDisj)
					{
						string text = "set";
						string text2 = "expected program set for symbol substringDisj but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringDisj>.CreateUnsafe(set);
				}

				// Token: 0x06007742 RID: 30530 RVA: 0x0019B3D0 File Offset: 0x001995D0
				public ProgramSetBuilder<substring> substring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.substring)
					{
						string text = "set";
						string text2 = "expected program set for symbol substring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substring>.CreateUnsafe(set);
				}

				// Token: 0x06007743 RID: 30531 RVA: 0x0019B428 File Offset: 0x00199628
				public ProgramSetBuilder<resultTable> resultTable(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.resultTable)
					{
						string text = "set";
						string text2 = "expected program set for symbol resultTable but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.resultTable>.CreateUnsafe(set);
				}

				// Token: 0x06007744 RID: 30532 RVA: 0x0019B480 File Offset: 0x00199680
				public ProgramSetBuilder<columnSelectors> columnSelectors(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnSelectors)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnSelectors but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.columnSelectors>.CreateUnsafe(set);
				}

				// Token: 0x06007745 RID: 30533 RVA: 0x0019B4D8 File Offset: 0x001996D8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name> name(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.name)
					{
						string text = "set";
						string text2 = "expected program set for symbol name but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.name>.CreateUnsafe(set);
				}

				// Token: 0x06007746 RID: 30534 RVA: 0x0019B530 File Offset: 0x00199730
				public ProgramSetBuilder<value> value(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.value)
					{
						string text = "set";
						string text2 = "expected program set for symbol @value but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.value>.CreateUnsafe(set);
				}

				// Token: 0x06007747 RID: 30535 RVA: 0x0019B588 File Offset: 0x00199788
				public ProgramSetBuilder<cssSelector> cssSelector(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.cssSelector)
					{
						string text = "set";
						string text2 = "expected program set for symbol cssSelector but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.cssSelector>.CreateUnsafe(set);
				}

				// Token: 0x06007748 RID: 30536 RVA: 0x0019B5E0 File Offset: 0x001997E0
				public ProgramSetBuilder<className> className(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.className)
					{
						string text = "set";
						string text2 = "expected program set for symbol className but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.className>.CreateUnsafe(set);
				}

				// Token: 0x06007749 RID: 30537 RVA: 0x0019B638 File Offset: 0x00199838
				public ProgramSetBuilder<idName> idName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.idName)
					{
						string text = "set";
						string text2 = "expected program set for symbol idName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idName>.CreateUnsafe(set);
				}

				// Token: 0x0600774A RID: 30538 RVA: 0x0019B690 File Offset: 0x00199890
				public ProgramSetBuilder<nodeName> nodeName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.nodeName)
					{
						string text = "set";
						string text2 = "expected program set for symbol nodeName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeName>.CreateUnsafe(set);
				}

				// Token: 0x0600774B RID: 30539 RVA: 0x0019B6E8 File Offset: 0x001998E8
				public ProgramSetBuilder<propName> propName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.propName)
					{
						string text = "set";
						string text2 = "expected program set for symbol propName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.propName>.CreateUnsafe(set);
				}

				// Token: 0x0600774C RID: 30540 RVA: 0x0019B740 File Offset: 0x00199940
				public ProgramSetBuilder<idx1> idx1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.idx1)
					{
						string text = "set";
						string text2 = "expected program set for symbol idx1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx1>.CreateUnsafe(set);
				}

				// Token: 0x0600774D RID: 30541 RVA: 0x0019B798 File Offset: 0x00199998
				public ProgramSetBuilder<idx2> idx2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.idx2)
					{
						string text = "set";
						string text2 = "expected program set for symbol idx2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.idx2>.CreateUnsafe(set);
				}

				// Token: 0x0600774E RID: 30542 RVA: 0x0019B7F0 File Offset: 0x001999F0
				public ProgramSetBuilder<names> names(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.names)
					{
						string text = "set";
						string text2 = "expected program set for symbol names but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.names>.CreateUnsafe(set);
				}

				// Token: 0x0600774F RID: 30543 RVA: 0x0019B848 File Offset: 0x00199A48
				public ProgramSetBuilder<count> count(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.count)
					{
						string text = "set";
						string text2 = "expected program set for symbol count but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.count>.CreateUnsafe(set);
				}

				// Token: 0x06007750 RID: 30544 RVA: 0x0019B8A0 File Offset: 0x00199AA0
				public ProgramSetBuilder<substringFeatureNames> substringFeatureNames(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.substringFeatureNames)
					{
						string text = "set";
						string text2 = "expected program set for symbol substringFeatureNames but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureNames>.CreateUnsafe(set);
				}

				// Token: 0x06007751 RID: 30545 RVA: 0x0019B8F8 File Offset: 0x00199AF8
				public ProgramSetBuilder<substringFeatureValues> substringFeatureValues(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.substringFeatureValues)
					{
						string text = "set";
						string text2 = "expected program set for symbol substringFeatureValues but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.substringFeatureValues>.CreateUnsafe(set);
				}

				// Token: 0x06007752 RID: 30546 RVA: 0x0019B950 File Offset: 0x00199B50
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06007753 RID: 30547 RVA: 0x0019B9A8 File Offset: 0x00199BA8
				public ProgramSetBuilder<entityObjs> entityObjs(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.entityObjs)
					{
						string text = "set";
						string text2 = "expected program set for symbol entityObjs but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.entityObjs>.CreateUnsafe(set);
				}

				// Token: 0x06007754 RID: 30548 RVA: 0x0019BA00 File Offset: 0x00199C00
				public ProgramSetBuilder<direction> direction(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.direction)
					{
						string text = "set";
						string text2 = "expected program set for symbol direction but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.direction>.CreateUnsafe(set);
				}

				// Token: 0x06007755 RID: 30549 RVA: 0x0019BA58 File Offset: 0x00199C58
				public ProgramSetBuilder<nodeCollection> nodeCollection(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.nodeCollection)
					{
						string text = "set";
						string text2 = "expected program set for symbol nodeCollection but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.nodeCollection>.CreateUnsafe(set);
				}

				// Token: 0x06007756 RID: 30550 RVA: 0x0019BAB0 File Offset: 0x00199CB0
				public ProgramSetBuilder<gen_NthChild> gen_NthChild(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_NthChild)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_NthChild but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthChild>.CreateUnsafe(set);
				}

				// Token: 0x06007757 RID: 30551 RVA: 0x0019BB08 File Offset: 0x00199D08
				public ProgramSetBuilder<gen_NthLastChild> gen_NthLastChild(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_NthLastChild)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_NthLastChild but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NthLastChild>.CreateUnsafe(set);
				}

				// Token: 0x06007758 RID: 30552 RVA: 0x0019BB60 File Offset: 0x00199D60
				public ProgramSetBuilder<gen_Class> gen_Class(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_Class)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_Class but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_Class>.CreateUnsafe(set);
				}

				// Token: 0x06007759 RID: 30553 RVA: 0x0019BBB8 File Offset: 0x00199DB8
				public ProgramSetBuilder<gen_ID> gen_ID(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_ID)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_ID but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ID>.CreateUnsafe(set);
				}

				// Token: 0x0600775A RID: 30554 RVA: 0x0019BC10 File Offset: 0x00199E10
				public ProgramSetBuilder<gen_NodeName> gen_NodeName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_NodeName)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_NodeName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_NodeName>.CreateUnsafe(set);
				}

				// Token: 0x0600775B RID: 30555 RVA: 0x0019BC68 File Offset: 0x00199E68
				public ProgramSetBuilder<gen_ItemProp> gen_ItemProp(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.gen_ItemProp)
					{
						string text = "set";
						string text2 = "expected program set for symbol gen_ItemProp but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.gen_ItemProp>.CreateUnsafe(set);
				}

				// Token: 0x0600775C RID: 30556 RVA: 0x0019BCC0 File Offset: 0x00199EC0
				public ProgramSetBuilder<obj> obj(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.obj)
					{
						string text = "set";
						string text2 = "expected program set for symbol obj but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes.obj>.CreateUnsafe(set);
				}

				// Token: 0x0600775D RID: 30557 RVA: 0x0019BD18 File Offset: 0x00199F18
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x0400327C RID: 12924
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
