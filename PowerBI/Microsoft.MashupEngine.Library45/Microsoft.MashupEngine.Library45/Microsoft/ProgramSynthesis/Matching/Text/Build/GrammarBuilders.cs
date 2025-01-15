using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build
{
	// Token: 0x020011C1 RID: 4545
	public class GrammarBuilders
	{
		// Token: 0x06008743 RID: 34627 RVA: 0x001CA8C5 File Offset: 0x001C8AC5
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06008744 RID: 34628 RVA: 0x001CA8F1 File Offset: 0x001C8AF1
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06008745 RID: 34629 RVA: 0x001CA8FE File Offset: 0x001C8AFE
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06008746 RID: 34630 RVA: 0x001CA90B File Offset: 0x001C8B0B
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06008747 RID: 34631 RVA: 0x001CA918 File Offset: 0x001C8B18
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06008748 RID: 34632 RVA: 0x001CA925 File Offset: 0x001C8B25
		// (set) Token: 0x06008749 RID: 34633 RVA: 0x001CA92D File Offset: 0x001C8B2D
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600874A RID: 34634 RVA: 0x001CA936 File Offset: 0x001C8B36
		// (set) Token: 0x0600874B RID: 34635 RVA: 0x001CA93E File Offset: 0x001C8B3E
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600874C RID: 34636 RVA: 0x001CA948 File Offset: 0x001C8B48
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

		// Token: 0x040037F4 RID: 14324
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x040037F5 RID: 14325
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x040037F6 RID: 14326
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x040037F7 RID: 14327
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x040037F8 RID: 14328
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x020011C2 RID: 4546
		public class GrammarSymbols
		{
			// Token: 0x17001733 RID: 5939
			// (get) Token: 0x0600874E RID: 34638 RVA: 0x001CA9F3 File Offset: 0x001C8BF3
			// (set) Token: 0x0600874F RID: 34639 RVA: 0x001CA9FB File Offset: 0x001C8BFB
			public Symbol inputSRegion { get; private set; }

			// Token: 0x17001734 RID: 5940
			// (get) Token: 0x06008750 RID: 34640 RVA: 0x001CAA04 File Offset: 0x001C8C04
			// (set) Token: 0x06008751 RID: 34641 RVA: 0x001CAA0C File Offset: 0x001C8C0C
			public Symbol result { get; private set; }

			// Token: 0x17001735 RID: 5941
			// (get) Token: 0x06008752 RID: 34642 RVA: 0x001CAA15 File Offset: 0x001C8C15
			// (set) Token: 0x06008753 RID: 34643 RVA: 0x001CAA1D File Offset: 0x001C8C1D
			public Symbol sRegion { get; private set; }

			// Token: 0x17001736 RID: 5942
			// (get) Token: 0x06008754 RID: 34644 RVA: 0x001CAA26 File Offset: 0x001C8C26
			// (set) Token: 0x06008755 RID: 34645 RVA: 0x001CAA2E File Offset: 0x001C8C2E
			public Symbol disjunctive_match { get; private set; }

			// Token: 0x17001737 RID: 5943
			// (get) Token: 0x06008756 RID: 34646 RVA: 0x001CAA37 File Offset: 0x001C8C37
			// (set) Token: 0x06008757 RID: 34647 RVA: 0x001CAA3F File Offset: 0x001C8C3F
			public Symbol match { get; private set; }

			// Token: 0x17001738 RID: 5944
			// (get) Token: 0x06008758 RID: 34648 RVA: 0x001CAA48 File Offset: 0x001C8C48
			// (set) Token: 0x06008759 RID: 34649 RVA: 0x001CAA50 File Offset: 0x001C8C50
			public Symbol token { get; private set; }

			// Token: 0x17001739 RID: 5945
			// (get) Token: 0x0600875A RID: 34650 RVA: 0x001CAA59 File Offset: 0x001C8C59
			// (set) Token: 0x0600875B RID: 34651 RVA: 0x001CAA61 File Offset: 0x001C8C61
			public Symbol multi_result { get; private set; }

			// Token: 0x1700173A RID: 5946
			// (get) Token: 0x0600875C RID: 34652 RVA: 0x001CAA6A File Offset: 0x001C8C6A
			// (set) Token: 0x0600875D RID: 34653 RVA: 0x001CAA72 File Offset: 0x001C8C72
			public Symbol sRegions { get; private set; }

			// Token: 0x1700173B RID: 5947
			// (get) Token: 0x0600875E RID: 34654 RVA: 0x001CAA7B File Offset: 0x001C8C7B
			// (set) Token: 0x0600875F RID: 34655 RVA: 0x001CAA83 File Offset: 0x001C8C83
			public Symbol multi_result_matches { get; private set; }

			// Token: 0x1700173C RID: 5948
			// (get) Token: 0x06008760 RID: 34656 RVA: 0x001CAA8C File Offset: 0x001C8C8C
			// (set) Token: 0x06008761 RID: 34657 RVA: 0x001CAA94 File Offset: 0x001C8C94
			public Symbol inputSRegions { get; private set; }

			// Token: 0x1700173D RID: 5949
			// (get) Token: 0x06008762 RID: 34658 RVA: 0x001CAA9D File Offset: 0x001C8C9D
			// (set) Token: 0x06008763 RID: 34659 RVA: 0x001CAAA5 File Offset: 0x001C8CA5
			public Symbol labelled_disjunction { get; private set; }

			// Token: 0x1700173E RID: 5950
			// (get) Token: 0x06008764 RID: 34660 RVA: 0x001CAAAE File Offset: 0x001C8CAE
			// (set) Token: 0x06008765 RID: 34661 RVA: 0x001CAAB6 File Offset: 0x001C8CB6
			public Symbol labelled_multi_result { get; private set; }

			// Token: 0x1700173F RID: 5951
			// (get) Token: 0x06008766 RID: 34662 RVA: 0x001CAABF File Offset: 0x001C8CBF
			// (set) Token: 0x06008767 RID: 34663 RVA: 0x001CAAC7 File Offset: 0x001C8CC7
			public Symbol label { get; private set; }

			// Token: 0x17001740 RID: 5952
			// (get) Token: 0x06008768 RID: 34664 RVA: 0x001CAAD0 File Offset: 0x001C8CD0
			// (set) Token: 0x06008769 RID: 34665 RVA: 0x001CAAD8 File Offset: 0x001C8CD8
			public Symbol nil_label { get; private set; }

			// Token: 0x17001741 RID: 5953
			// (get) Token: 0x0600876A RID: 34666 RVA: 0x001CAAE1 File Offset: 0x001C8CE1
			// (set) Token: 0x0600876B RID: 34667 RVA: 0x001CAAE9 File Offset: 0x001C8CE9
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17001742 RID: 5954
			// (get) Token: 0x0600876C RID: 34668 RVA: 0x001CAAF2 File Offset: 0x001C8CF2
			// (set) Token: 0x0600876D RID: 34669 RVA: 0x001CAAFA File Offset: 0x001C8CFA
			public Symbol _LetB1 { get; private set; }

			// Token: 0x17001743 RID: 5955
			// (get) Token: 0x0600876E RID: 34670 RVA: 0x001CAB03 File Offset: 0x001C8D03
			// (set) Token: 0x0600876F RID: 34671 RVA: 0x001CAB0B File Offset: 0x001C8D0B
			public Symbol _LetB2 { get; private set; }

			// Token: 0x17001744 RID: 5956
			// (get) Token: 0x06008770 RID: 34672 RVA: 0x001CAB14 File Offset: 0x001C8D14
			// (set) Token: 0x06008771 RID: 34673 RVA: 0x001CAB1C File Offset: 0x001C8D1C
			public Symbol _LetB3 { get; private set; }

			// Token: 0x17001745 RID: 5957
			// (get) Token: 0x06008772 RID: 34674 RVA: 0x001CAB25 File Offset: 0x001C8D25
			// (set) Token: 0x06008773 RID: 34675 RVA: 0x001CAB2D File Offset: 0x001C8D2D
			public Symbol _LetB4 { get; private set; }

			// Token: 0x06008774 RID: 34676 RVA: 0x001CAB38 File Offset: 0x001C8D38
			public GrammarSymbols(Grammar grammar)
			{
				this.inputSRegion = grammar.Symbol("inputSRegion");
				this.result = grammar.Symbol("result");
				this.sRegion = grammar.Symbol("sRegion");
				this.disjunctive_match = grammar.Symbol("disjunctive_match");
				this.match = grammar.Symbol("match");
				this.token = grammar.Symbol("token");
				this.multi_result = grammar.Symbol("multi_result");
				this.sRegions = grammar.Symbol("sRegions");
				this.multi_result_matches = grammar.Symbol("multi_result_matches");
				this.inputSRegions = grammar.Symbol("inputSRegions");
				this.labelled_disjunction = grammar.Symbol("labelled_disjunction");
				this.labelled_multi_result = grammar.Symbol("labelled_multi_result");
				this.label = grammar.Symbol("label");
				this.nil_label = grammar.Symbol("nil_label");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
				this._LetB2 = grammar.Symbol("_LetB2");
				this._LetB3 = grammar.Symbol("_LetB3");
				this._LetB4 = grammar.Symbol("_LetB4");
			}
		}

		// Token: 0x020011C3 RID: 4547
		public class GrammarRules
		{
			// Token: 0x17001746 RID: 5958
			// (get) Token: 0x06008775 RID: 34677 RVA: 0x001CAC8E File Offset: 0x001C8E8E
			// (set) Token: 0x06008776 RID: 34678 RVA: 0x001CAC96 File Offset: 0x001C8E96
			public BlackBoxRule NoMatch { get; private set; }

			// Token: 0x17001747 RID: 5959
			// (get) Token: 0x06008777 RID: 34679 RVA: 0x001CAC9F File Offset: 0x001C8E9F
			// (set) Token: 0x06008778 RID: 34680 RVA: 0x001CACA7 File Offset: 0x001C8EA7
			public BlackBoxRule Disjunction { get; private set; }

			// Token: 0x17001748 RID: 5960
			// (get) Token: 0x06008779 RID: 34681 RVA: 0x001CACB0 File Offset: 0x001C8EB0
			// (set) Token: 0x0600877A RID: 34682 RVA: 0x001CACB8 File Offset: 0x001C8EB8
			public BlackBoxRule SuffixAfterTokenMatch { get; private set; }

			// Token: 0x17001749 RID: 5961
			// (get) Token: 0x0600877B RID: 34683 RVA: 0x001CACC1 File Offset: 0x001C8EC1
			// (set) Token: 0x0600877C RID: 34684 RVA: 0x001CACC9 File Offset: 0x001C8EC9
			public BlackBoxRule IsNull { get; private set; }

			// Token: 0x1700174A RID: 5962
			// (get) Token: 0x0600877D RID: 34685 RVA: 0x001CACD2 File Offset: 0x001C8ED2
			// (set) Token: 0x0600877E RID: 34686 RVA: 0x001CACDA File Offset: 0x001C8EDA
			public BlackBoxRule EndOf { get; private set; }

			// Token: 0x1700174B RID: 5963
			// (get) Token: 0x0600877F RID: 34687 RVA: 0x001CACE3 File Offset: 0x001C8EE3
			// (set) Token: 0x06008780 RID: 34688 RVA: 0x001CACEB File Offset: 0x001C8EEB
			public BlackBoxRule Tail { get; private set; }

			// Token: 0x1700174C RID: 5964
			// (get) Token: 0x06008781 RID: 34689 RVA: 0x001CACF4 File Offset: 0x001C8EF4
			// (set) Token: 0x06008782 RID: 34690 RVA: 0x001CACFC File Offset: 0x001C8EFC
			public BlackBoxRule MatchColumns { get; private set; }

			// Token: 0x1700174D RID: 5965
			// (get) Token: 0x06008783 RID: 34691 RVA: 0x001CAD05 File Offset: 0x001C8F05
			// (set) Token: 0x06008784 RID: 34692 RVA: 0x001CAD0D File Offset: 0x001C8F0D
			public BlackBoxRule Head { get; private set; }

			// Token: 0x1700174E RID: 5966
			// (get) Token: 0x06008785 RID: 34693 RVA: 0x001CAD16 File Offset: 0x001C8F16
			// (set) Token: 0x06008786 RID: 34694 RVA: 0x001CAD1E File Offset: 0x001C8F1E
			public BlackBoxRule Nil { get; private set; }

			// Token: 0x1700174F RID: 5967
			// (get) Token: 0x06008787 RID: 34695 RVA: 0x001CAD27 File Offset: 0x001C8F27
			// (set) Token: 0x06008788 RID: 34696 RVA: 0x001CAD2F File Offset: 0x001C8F2F
			public BlackBoxRule IfThenElse { get; private set; }

			// Token: 0x17001750 RID: 5968
			// (get) Token: 0x06008789 RID: 34697 RVA: 0x001CAD38 File Offset: 0x001C8F38
			// (set) Token: 0x0600878A RID: 34698 RVA: 0x001CAD40 File Offset: 0x001C8F40
			public BlackBoxRule LabelledMatchColumns { get; private set; }

			// Token: 0x17001751 RID: 5969
			// (get) Token: 0x0600878B RID: 34699 RVA: 0x001CAD49 File Offset: 0x001C8F49
			// (set) Token: 0x0600878C RID: 34700 RVA: 0x001CAD51 File Offset: 0x001C8F51
			public LetRule LetResult { get; private set; }

			// Token: 0x17001752 RID: 5970
			// (get) Token: 0x0600878D RID: 34701 RVA: 0x001CAD5A File Offset: 0x001C8F5A
			// (set) Token: 0x0600878E RID: 34702 RVA: 0x001CAD62 File Offset: 0x001C8F62
			public LetRule LetSplit { get; private set; }

			// Token: 0x17001753 RID: 5971
			// (get) Token: 0x0600878F RID: 34703 RVA: 0x001CAD6B File Offset: 0x001C8F6B
			// (set) Token: 0x06008790 RID: 34704 RVA: 0x001CAD73 File Offset: 0x001C8F73
			public LetRule LetMultiResult { get; private set; }

			// Token: 0x17001754 RID: 5972
			// (get) Token: 0x06008791 RID: 34705 RVA: 0x001CAD7C File Offset: 0x001C8F7C
			// (set) Token: 0x06008792 RID: 34706 RVA: 0x001CAD84 File Offset: 0x001C8F84
			public LetRule LetTail { get; private set; }

			// Token: 0x17001755 RID: 5973
			// (get) Token: 0x06008793 RID: 34707 RVA: 0x001CAD8D File Offset: 0x001C8F8D
			// (set) Token: 0x06008794 RID: 34708 RVA: 0x001CAD95 File Offset: 0x001C8F95
			public LetRule LetHead { get; private set; }

			// Token: 0x06008795 RID: 34709 RVA: 0x001CADA0 File Offset: 0x001C8FA0
			public GrammarRules(Grammar grammar)
			{
				this.NoMatch = (BlackBoxRule)grammar.Rule("NoMatch");
				this.Disjunction = (BlackBoxRule)grammar.Rule("Disjunction");
				this.SuffixAfterTokenMatch = (BlackBoxRule)grammar.Rule("SuffixAfterTokenMatch");
				this.IsNull = (BlackBoxRule)grammar.Rule("IsNull");
				this.EndOf = (BlackBoxRule)grammar.Rule("EndOf");
				this.Tail = (BlackBoxRule)grammar.Rule("Tail");
				this.MatchColumns = (BlackBoxRule)grammar.Rule("MatchColumns");
				this.Head = (BlackBoxRule)grammar.Rule("Head");
				this.Nil = (BlackBoxRule)grammar.Rule("Nil");
				this.IfThenElse = (BlackBoxRule)grammar.Rule("IfThenElse");
				this.LabelledMatchColumns = (BlackBoxRule)grammar.Rule("LabelledMatchColumns");
				this.LetResult = (LetRule)grammar.Rule("LetResult");
				this.LetSplit = (LetRule)grammar.Rule("LetSplit");
				this.LetMultiResult = (LetRule)grammar.Rule("LetMultiResult");
				this.LetTail = (LetRule)grammar.Rule("LetTail");
				this.LetHead = (LetRule)grammar.Rule("LetHead");
			}
		}

		// Token: 0x020011C4 RID: 4548
		public class GrammarUnnamedConversions
		{
			// Token: 0x17001756 RID: 5974
			// (get) Token: 0x06008796 RID: 34710 RVA: 0x001CAF13 File Offset: 0x001C9113
			// (set) Token: 0x06008797 RID: 34711 RVA: 0x001CAF1B File Offset: 0x001C911B
			public ConversionRule labelled_disjunction_label { get; private set; }

			// Token: 0x17001757 RID: 5975
			// (get) Token: 0x06008798 RID: 34712 RVA: 0x001CAF24 File Offset: 0x001C9124
			// (set) Token: 0x06008799 RID: 34713 RVA: 0x001CAF2C File Offset: 0x001C912C
			public ConversionRule labelled_multi_result_nil_label { get; private set; }

			// Token: 0x0600879A RID: 34714 RVA: 0x001CAF35 File Offset: 0x001C9135
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.labelled_disjunction_label = (ConversionRule)grammar.Rule("~convert_labelled_disjunction_label");
				this.labelled_multi_result_nil_label = (ConversionRule)grammar.Rule("~convert_labelled_multi_result_nil_label");
			}
		}

		// Token: 0x020011C5 RID: 4549
		public class GrammarHoles
		{
			// Token: 0x17001758 RID: 5976
			// (get) Token: 0x0600879B RID: 34715 RVA: 0x001CAF69 File Offset: 0x001C9169
			// (set) Token: 0x0600879C RID: 34716 RVA: 0x001CAF71 File Offset: 0x001C9171
			public Hole inputSRegion { get; private set; }

			// Token: 0x17001759 RID: 5977
			// (get) Token: 0x0600879D RID: 34717 RVA: 0x001CAF7A File Offset: 0x001C917A
			// (set) Token: 0x0600879E RID: 34718 RVA: 0x001CAF82 File Offset: 0x001C9182
			public Hole result { get; private set; }

			// Token: 0x1700175A RID: 5978
			// (get) Token: 0x0600879F RID: 34719 RVA: 0x001CAF8B File Offset: 0x001C918B
			// (set) Token: 0x060087A0 RID: 34720 RVA: 0x001CAF93 File Offset: 0x001C9193
			public Hole sRegion { get; private set; }

			// Token: 0x1700175B RID: 5979
			// (get) Token: 0x060087A1 RID: 34721 RVA: 0x001CAF9C File Offset: 0x001C919C
			// (set) Token: 0x060087A2 RID: 34722 RVA: 0x001CAFA4 File Offset: 0x001C91A4
			public Hole disjunctive_match { get; private set; }

			// Token: 0x1700175C RID: 5980
			// (get) Token: 0x060087A3 RID: 34723 RVA: 0x001CAFAD File Offset: 0x001C91AD
			// (set) Token: 0x060087A4 RID: 34724 RVA: 0x001CAFB5 File Offset: 0x001C91B5
			public Hole match { get; private set; }

			// Token: 0x1700175D RID: 5981
			// (get) Token: 0x060087A5 RID: 34725 RVA: 0x001CAFBE File Offset: 0x001C91BE
			// (set) Token: 0x060087A6 RID: 34726 RVA: 0x001CAFC6 File Offset: 0x001C91C6
			public Hole token { get; private set; }

			// Token: 0x1700175E RID: 5982
			// (get) Token: 0x060087A7 RID: 34727 RVA: 0x001CAFCF File Offset: 0x001C91CF
			// (set) Token: 0x060087A8 RID: 34728 RVA: 0x001CAFD7 File Offset: 0x001C91D7
			public Hole multi_result { get; private set; }

			// Token: 0x1700175F RID: 5983
			// (get) Token: 0x060087A9 RID: 34729 RVA: 0x001CAFE0 File Offset: 0x001C91E0
			// (set) Token: 0x060087AA RID: 34730 RVA: 0x001CAFE8 File Offset: 0x001C91E8
			public Hole sRegions { get; private set; }

			// Token: 0x17001760 RID: 5984
			// (get) Token: 0x060087AB RID: 34731 RVA: 0x001CAFF1 File Offset: 0x001C91F1
			// (set) Token: 0x060087AC RID: 34732 RVA: 0x001CAFF9 File Offset: 0x001C91F9
			public Hole multi_result_matches { get; private set; }

			// Token: 0x17001761 RID: 5985
			// (get) Token: 0x060087AD RID: 34733 RVA: 0x001CB002 File Offset: 0x001C9202
			// (set) Token: 0x060087AE RID: 34734 RVA: 0x001CB00A File Offset: 0x001C920A
			public Hole inputSRegions { get; private set; }

			// Token: 0x17001762 RID: 5986
			// (get) Token: 0x060087AF RID: 34735 RVA: 0x001CB013 File Offset: 0x001C9213
			// (set) Token: 0x060087B0 RID: 34736 RVA: 0x001CB01B File Offset: 0x001C921B
			public Hole labelled_disjunction { get; private set; }

			// Token: 0x17001763 RID: 5987
			// (get) Token: 0x060087B1 RID: 34737 RVA: 0x001CB024 File Offset: 0x001C9224
			// (set) Token: 0x060087B2 RID: 34738 RVA: 0x001CB02C File Offset: 0x001C922C
			public Hole labelled_multi_result { get; private set; }

			// Token: 0x17001764 RID: 5988
			// (get) Token: 0x060087B3 RID: 34739 RVA: 0x001CB035 File Offset: 0x001C9235
			// (set) Token: 0x060087B4 RID: 34740 RVA: 0x001CB03D File Offset: 0x001C923D
			public Hole label { get; private set; }

			// Token: 0x17001765 RID: 5989
			// (get) Token: 0x060087B5 RID: 34741 RVA: 0x001CB046 File Offset: 0x001C9246
			// (set) Token: 0x060087B6 RID: 34742 RVA: 0x001CB04E File Offset: 0x001C924E
			public Hole nil_label { get; private set; }

			// Token: 0x17001766 RID: 5990
			// (get) Token: 0x060087B7 RID: 34743 RVA: 0x001CB057 File Offset: 0x001C9257
			// (set) Token: 0x060087B8 RID: 34744 RVA: 0x001CB05F File Offset: 0x001C925F
			public Hole _LetB0 { get; private set; }

			// Token: 0x17001767 RID: 5991
			// (get) Token: 0x060087B9 RID: 34745 RVA: 0x001CB068 File Offset: 0x001C9268
			// (set) Token: 0x060087BA RID: 34746 RVA: 0x001CB070 File Offset: 0x001C9270
			public Hole _LetB1 { get; private set; }

			// Token: 0x17001768 RID: 5992
			// (get) Token: 0x060087BB RID: 34747 RVA: 0x001CB079 File Offset: 0x001C9279
			// (set) Token: 0x060087BC RID: 34748 RVA: 0x001CB081 File Offset: 0x001C9281
			public Hole _LetB2 { get; private set; }

			// Token: 0x17001769 RID: 5993
			// (get) Token: 0x060087BD RID: 34749 RVA: 0x001CB08A File Offset: 0x001C928A
			// (set) Token: 0x060087BE RID: 34750 RVA: 0x001CB092 File Offset: 0x001C9292
			public Hole _LetB3 { get; private set; }

			// Token: 0x1700176A RID: 5994
			// (get) Token: 0x060087BF RID: 34751 RVA: 0x001CB09B File Offset: 0x001C929B
			// (set) Token: 0x060087C0 RID: 34752 RVA: 0x001CB0A3 File Offset: 0x001C92A3
			public Hole _LetB4 { get; private set; }

			// Token: 0x060087C1 RID: 34753 RVA: 0x001CB0AC File Offset: 0x001C92AC
			public GrammarHoles(GrammarBuilders builders)
			{
				this.inputSRegion = new Hole(builders.Symbol.inputSRegion, null);
				this.result = new Hole(builders.Symbol.result, null);
				this.sRegion = new Hole(builders.Symbol.sRegion, null);
				this.disjunctive_match = new Hole(builders.Symbol.disjunctive_match, null);
				this.match = new Hole(builders.Symbol.match, null);
				this.token = new Hole(builders.Symbol.token, null);
				this.multi_result = new Hole(builders.Symbol.multi_result, null);
				this.sRegions = new Hole(builders.Symbol.sRegions, null);
				this.multi_result_matches = new Hole(builders.Symbol.multi_result_matches, null);
				this.inputSRegions = new Hole(builders.Symbol.inputSRegions, null);
				this.labelled_disjunction = new Hole(builders.Symbol.labelled_disjunction, null);
				this.labelled_multi_result = new Hole(builders.Symbol.labelled_multi_result, null);
				this.label = new Hole(builders.Symbol.label, null);
				this.nil_label = new Hole(builders.Symbol.nil_label, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
				this._LetB2 = new Hole(builders.Symbol._LetB2, null);
				this._LetB3 = new Hole(builders.Symbol._LetB3, null);
				this._LetB4 = new Hole(builders.Symbol._LetB4, null);
			}
		}

		// Token: 0x020011C6 RID: 4550
		public class Nodes
		{
			// Token: 0x060087C2 RID: 34754 RVA: 0x001CB274 File Offset: 0x001C9474
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

			// Token: 0x1700176B RID: 5995
			// (get) Token: 0x060087C3 RID: 34755 RVA: 0x001CB357 File Offset: 0x001C9557
			// (set) Token: 0x060087C4 RID: 34756 RVA: 0x001CB35F File Offset: 0x001C955F
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x1700176C RID: 5996
			// (get) Token: 0x060087C5 RID: 34757 RVA: 0x001CB368 File Offset: 0x001C9568
			// (set) Token: 0x060087C6 RID: 34758 RVA: 0x001CB370 File Offset: 0x001C9570
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x1700176D RID: 5997
			// (get) Token: 0x060087C7 RID: 34759 RVA: 0x001CB379 File Offset: 0x001C9579
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x1700176E RID: 5998
			// (get) Token: 0x060087C8 RID: 34760 RVA: 0x001CB386 File Offset: 0x001C9586
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x1700176F RID: 5999
			// (get) Token: 0x060087C9 RID: 34761 RVA: 0x001CB393 File Offset: 0x001C9593
			// (set) Token: 0x060087CA RID: 34762 RVA: 0x001CB39B File Offset: 0x001C959B
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17001770 RID: 6000
			// (get) Token: 0x060087CB RID: 34763 RVA: 0x001CB3A4 File Offset: 0x001C95A4
			// (set) Token: 0x060087CC RID: 34764 RVA: 0x001CB3AC File Offset: 0x001C95AC
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17001771 RID: 6001
			// (get) Token: 0x060087CD RID: 34765 RVA: 0x001CB3B5 File Offset: 0x001C95B5
			// (set) Token: 0x060087CE RID: 34766 RVA: 0x001CB3BD File Offset: 0x001C95BD
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17001772 RID: 6002
			// (get) Token: 0x060087CF RID: 34767 RVA: 0x001CB3C6 File Offset: 0x001C95C6
			// (set) Token: 0x060087D0 RID: 34768 RVA: 0x001CB3CE File Offset: 0x001C95CE
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17001773 RID: 6003
			// (get) Token: 0x060087D1 RID: 34769 RVA: 0x001CB3D7 File Offset: 0x001C95D7
			// (set) Token: 0x060087D2 RID: 34770 RVA: 0x001CB3DF File Offset: 0x001C95DF
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17001774 RID: 6004
			// (get) Token: 0x060087D3 RID: 34771 RVA: 0x001CB3E8 File Offset: 0x001C95E8
			// (set) Token: 0x060087D4 RID: 34772 RVA: 0x001CB3F0 File Offset: 0x001C95F0
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17001775 RID: 6005
			// (get) Token: 0x060087D5 RID: 34773 RVA: 0x001CB3F9 File Offset: 0x001C95F9
			// (set) Token: 0x060087D6 RID: 34774 RVA: 0x001CB401 File Offset: 0x001C9601
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04003835 RID: 14389
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04003836 RID: 14390
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x020011C7 RID: 4551
			public class NodeRules
			{
				// Token: 0x060087D7 RID: 34775 RVA: 0x001CB40A File Offset: 0x001C960A
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060087D8 RID: 34776 RVA: 0x001CB419 File Offset: 0x001C9619
				public token token(IToken value)
				{
					return new token(this._builders, value);
				}

				// Token: 0x060087D9 RID: 34777 RVA: 0x001CB427 File Offset: 0x001C9627
				public inputSRegions inputSRegions(IEnumerable<SuffixRegion> value)
				{
					return new inputSRegions(this._builders, value);
				}

				// Token: 0x060087DA RID: 34778 RVA: 0x001CB435 File Offset: 0x001C9635
				public label label(MatchingLabel value)
				{
					return new label(this._builders, value);
				}

				// Token: 0x060087DB RID: 34779 RVA: 0x001CB443 File Offset: 0x001C9643
				public nil_label nil_label(ImmutableList<MatchingLabel> value)
				{
					return new nil_label(this._builders, value);
				}

				// Token: 0x060087DC RID: 34780 RVA: 0x001CB451 File Offset: 0x001C9651
				public disjunctive_match NoMatch()
				{
					return new NoMatch(this._builders);
				}

				// Token: 0x060087DD RID: 34781 RVA: 0x001CB463 File Offset: 0x001C9663
				public disjunctive_match Disjunction(match value0, disjunctive_match value1)
				{
					return new Disjunction(this._builders, value0, value1);
				}

				// Token: 0x060087DE RID: 34782 RVA: 0x001CB477 File Offset: 0x001C9677
				public _LetB0 SuffixAfterTokenMatch(sRegion value0, token value1)
				{
					return new SuffixAfterTokenMatch(this._builders, value0, value1);
				}

				// Token: 0x060087DF RID: 34783 RVA: 0x001CB48B File Offset: 0x001C968B
				public match IsNull(sRegion value0)
				{
					return new IsNull(this._builders, value0);
				}

				// Token: 0x060087E0 RID: 34784 RVA: 0x001CB49E File Offset: 0x001C969E
				public match EndOf(sRegion value0)
				{
					return new EndOf(this._builders, value0);
				}

				// Token: 0x060087E1 RID: 34785 RVA: 0x001CB4B1 File Offset: 0x001C96B1
				public _LetB1 Tail(sRegions value0)
				{
					return new Tail(this._builders, value0);
				}

				// Token: 0x060087E2 RID: 34786 RVA: 0x001CB4C4 File Offset: 0x001C96C4
				public _LetB2 MatchColumns(disjunctive_match value0, multi_result_matches value1)
				{
					return new MatchColumns(this._builders, value0, value1);
				}

				// Token: 0x060087E3 RID: 34787 RVA: 0x001CB4D8 File Offset: 0x001C96D8
				public _LetB3 Head(sRegions value0)
				{
					return new Head(this._builders, value0);
				}

				// Token: 0x060087E4 RID: 34788 RVA: 0x001CB4EB File Offset: 0x001C96EB
				public multi_result_matches Nil(sRegions value0)
				{
					return new Nil(this._builders, value0);
				}

				// Token: 0x060087E5 RID: 34789 RVA: 0x001CB4FE File Offset: 0x001C96FE
				public labelled_disjunction IfThenElse(match value0, label value1, labelled_disjunction value2)
				{
					return new IfThenElse(this._builders, value0, value1, value2);
				}

				// Token: 0x060087E6 RID: 34790 RVA: 0x001CB513 File Offset: 0x001C9713
				public labelled_multi_result LabelledMatchColumns(labelled_disjunction value0, labelled_multi_result value1)
				{
					return new LabelledMatchColumns(this._builders, value0, value1);
				}

				// Token: 0x060087E7 RID: 34791 RVA: 0x001CB527 File Offset: 0x001C9727
				public result LetResult(inputSRegion value0, disjunctive_match value1)
				{
					return new LetResult(this._builders, value0, value1);
				}

				// Token: 0x060087E8 RID: 34792 RVA: 0x001CB53B File Offset: 0x001C973B
				public match LetSplit(_LetB0 value0, match value1)
				{
					return new LetSplit(this._builders, value0, value1);
				}

				// Token: 0x060087E9 RID: 34793 RVA: 0x001CB54F File Offset: 0x001C974F
				public multi_result LetMultiResult(inputSRegions value0, multi_result_matches value1)
				{
					return new LetMultiResult(this._builders, value0, value1);
				}

				// Token: 0x060087EA RID: 34794 RVA: 0x001CB563 File Offset: 0x001C9763
				public _LetB4 LetTail(_LetB1 value0, _LetB2 value1)
				{
					return new LetTail(this._builders, value0, value1);
				}

				// Token: 0x060087EB RID: 34795 RVA: 0x001CB577 File Offset: 0x001C9777
				public multi_result_matches LetHead(_LetB3 value0, _LetB4 value1)
				{
					return new LetHead(this._builders, value0, value1);
				}

				// Token: 0x0400383E RID: 14398
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011C8 RID: 4552
			public class NodeUnnamedConversionRules
			{
				// Token: 0x060087EC RID: 34796 RVA: 0x001CB58B File Offset: 0x001C978B
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060087ED RID: 34797 RVA: 0x001CB59A File Offset: 0x001C979A
				public labelled_disjunction labelled_disjunction_label(label value0)
				{
					return new labelled_disjunction_label(this._builders, value0);
				}

				// Token: 0x060087EE RID: 34798 RVA: 0x001CB5AD File Offset: 0x001C97AD
				public labelled_multi_result labelled_multi_result_nil_label(nil_label value0)
				{
					return new labelled_multi_result_nil_label(this._builders, value0);
				}

				// Token: 0x0400383F RID: 14399
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011C9 RID: 4553
			public class NodeVariables
			{
				// Token: 0x17001776 RID: 6006
				// (get) Token: 0x060087EF RID: 34799 RVA: 0x001CB5C0 File Offset: 0x001C97C0
				// (set) Token: 0x060087F0 RID: 34800 RVA: 0x001CB5C8 File Offset: 0x001C97C8
				public inputSRegion inputSRegion { get; private set; }

				// Token: 0x17001777 RID: 6007
				// (get) Token: 0x060087F1 RID: 34801 RVA: 0x001CB5D1 File Offset: 0x001C97D1
				// (set) Token: 0x060087F2 RID: 34802 RVA: 0x001CB5D9 File Offset: 0x001C97D9
				public sRegion sRegion { get; private set; }

				// Token: 0x17001778 RID: 6008
				// (get) Token: 0x060087F3 RID: 34803 RVA: 0x001CB5E2 File Offset: 0x001C97E2
				// (set) Token: 0x060087F4 RID: 34804 RVA: 0x001CB5EA File Offset: 0x001C97EA
				public sRegions sRegions { get; private set; }

				// Token: 0x060087F5 RID: 34805 RVA: 0x001CB5F3 File Offset: 0x001C97F3
				public NodeVariables(GrammarBuilders builders)
				{
					this.inputSRegion = new inputSRegion(builders);
					this.sRegion = new sRegion(builders);
					this.sRegions = new sRegions(builders);
				}
			}

			// Token: 0x020011CA RID: 4554
			public class NodeHoles
			{
				// Token: 0x17001779 RID: 6009
				// (get) Token: 0x060087F6 RID: 34806 RVA: 0x001CB61F File Offset: 0x001C981F
				// (set) Token: 0x060087F7 RID: 34807 RVA: 0x001CB627 File Offset: 0x001C9827
				public result result { get; private set; }

				// Token: 0x1700177A RID: 6010
				// (get) Token: 0x060087F8 RID: 34808 RVA: 0x001CB630 File Offset: 0x001C9830
				// (set) Token: 0x060087F9 RID: 34809 RVA: 0x001CB638 File Offset: 0x001C9838
				public sRegion sRegion { get; private set; }

				// Token: 0x1700177B RID: 6011
				// (get) Token: 0x060087FA RID: 34810 RVA: 0x001CB641 File Offset: 0x001C9841
				// (set) Token: 0x060087FB RID: 34811 RVA: 0x001CB649 File Offset: 0x001C9849
				public disjunctive_match disjunctive_match { get; private set; }

				// Token: 0x1700177C RID: 6012
				// (get) Token: 0x060087FC RID: 34812 RVA: 0x001CB652 File Offset: 0x001C9852
				// (set) Token: 0x060087FD RID: 34813 RVA: 0x001CB65A File Offset: 0x001C985A
				public match match { get; private set; }

				// Token: 0x1700177D RID: 6013
				// (get) Token: 0x060087FE RID: 34814 RVA: 0x001CB663 File Offset: 0x001C9863
				// (set) Token: 0x060087FF RID: 34815 RVA: 0x001CB66B File Offset: 0x001C986B
				public token token { get; private set; }

				// Token: 0x1700177E RID: 6014
				// (get) Token: 0x06008800 RID: 34816 RVA: 0x001CB674 File Offset: 0x001C9874
				// (set) Token: 0x06008801 RID: 34817 RVA: 0x001CB67C File Offset: 0x001C987C
				public multi_result multi_result { get; private set; }

				// Token: 0x1700177F RID: 6015
				// (get) Token: 0x06008802 RID: 34818 RVA: 0x001CB685 File Offset: 0x001C9885
				// (set) Token: 0x06008803 RID: 34819 RVA: 0x001CB68D File Offset: 0x001C988D
				public sRegions sRegions { get; private set; }

				// Token: 0x17001780 RID: 6016
				// (get) Token: 0x06008804 RID: 34820 RVA: 0x001CB696 File Offset: 0x001C9896
				// (set) Token: 0x06008805 RID: 34821 RVA: 0x001CB69E File Offset: 0x001C989E
				public multi_result_matches multi_result_matches { get; private set; }

				// Token: 0x17001781 RID: 6017
				// (get) Token: 0x06008806 RID: 34822 RVA: 0x001CB6A7 File Offset: 0x001C98A7
				// (set) Token: 0x06008807 RID: 34823 RVA: 0x001CB6AF File Offset: 0x001C98AF
				public inputSRegions inputSRegions { get; private set; }

				// Token: 0x17001782 RID: 6018
				// (get) Token: 0x06008808 RID: 34824 RVA: 0x001CB6B8 File Offset: 0x001C98B8
				// (set) Token: 0x06008809 RID: 34825 RVA: 0x001CB6C0 File Offset: 0x001C98C0
				public labelled_disjunction labelled_disjunction { get; private set; }

				// Token: 0x17001783 RID: 6019
				// (get) Token: 0x0600880A RID: 34826 RVA: 0x001CB6C9 File Offset: 0x001C98C9
				// (set) Token: 0x0600880B RID: 34827 RVA: 0x001CB6D1 File Offset: 0x001C98D1
				public labelled_multi_result labelled_multi_result { get; private set; }

				// Token: 0x17001784 RID: 6020
				// (get) Token: 0x0600880C RID: 34828 RVA: 0x001CB6DA File Offset: 0x001C98DA
				// (set) Token: 0x0600880D RID: 34829 RVA: 0x001CB6E2 File Offset: 0x001C98E2
				public label label { get; private set; }

				// Token: 0x17001785 RID: 6021
				// (get) Token: 0x0600880E RID: 34830 RVA: 0x001CB6EB File Offset: 0x001C98EB
				// (set) Token: 0x0600880F RID: 34831 RVA: 0x001CB6F3 File Offset: 0x001C98F3
				public nil_label nil_label { get; private set; }

				// Token: 0x17001786 RID: 6022
				// (get) Token: 0x06008810 RID: 34832 RVA: 0x001CB6FC File Offset: 0x001C98FC
				// (set) Token: 0x06008811 RID: 34833 RVA: 0x001CB704 File Offset: 0x001C9904
				public _LetB0 _LetB0 { get; private set; }

				// Token: 0x17001787 RID: 6023
				// (get) Token: 0x06008812 RID: 34834 RVA: 0x001CB70D File Offset: 0x001C990D
				// (set) Token: 0x06008813 RID: 34835 RVA: 0x001CB715 File Offset: 0x001C9915
				public _LetB1 _LetB1 { get; private set; }

				// Token: 0x17001788 RID: 6024
				// (get) Token: 0x06008814 RID: 34836 RVA: 0x001CB71E File Offset: 0x001C991E
				// (set) Token: 0x06008815 RID: 34837 RVA: 0x001CB726 File Offset: 0x001C9926
				public _LetB2 _LetB2 { get; private set; }

				// Token: 0x17001789 RID: 6025
				// (get) Token: 0x06008816 RID: 34838 RVA: 0x001CB72F File Offset: 0x001C992F
				// (set) Token: 0x06008817 RID: 34839 RVA: 0x001CB737 File Offset: 0x001C9937
				public _LetB3 _LetB3 { get; private set; }

				// Token: 0x1700178A RID: 6026
				// (get) Token: 0x06008818 RID: 34840 RVA: 0x001CB740 File Offset: 0x001C9940
				// (set) Token: 0x06008819 RID: 34841 RVA: 0x001CB748 File Offset: 0x001C9948
				public _LetB4 _LetB4 { get; private set; }

				// Token: 0x0600881A RID: 34842 RVA: 0x001CB754 File Offset: 0x001C9954
				public NodeHoles(GrammarBuilders builders)
				{
					this.result = result.CreateHole(builders, null);
					this.sRegion = sRegion.CreateHole(builders, null);
					this.disjunctive_match = disjunctive_match.CreateHole(builders, null);
					this.match = match.CreateHole(builders, null);
					this.token = token.CreateHole(builders, null);
					this.multi_result = multi_result.CreateHole(builders, null);
					this.sRegions = sRegions.CreateHole(builders, null);
					this.multi_result_matches = multi_result_matches.CreateHole(builders, null);
					this.inputSRegions = inputSRegions.CreateHole(builders, null);
					this.labelled_disjunction = labelled_disjunction.CreateHole(builders, null);
					this.labelled_multi_result = labelled_multi_result.CreateHole(builders, null);
					this.label = label.CreateHole(builders, null);
					this.nil_label = nil_label.CreateHole(builders, null);
					this._LetB0 = _LetB0.CreateHole(builders, null);
					this._LetB1 = _LetB1.CreateHole(builders, null);
					this._LetB2 = _LetB2.CreateHole(builders, null);
					this._LetB3 = _LetB3.CreateHole(builders, null);
					this._LetB4 = _LetB4.CreateHole(builders, null);
				}
			}

			// Token: 0x020011CB RID: 4555
			public class NodeUnsafe
			{
				// Token: 0x0600881B RID: 34843 RVA: 0x001CB851 File Offset: 0x001C9A51
				public result result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result.CreateUnsafe(node);
				}

				// Token: 0x0600881C RID: 34844 RVA: 0x001CB859 File Offset: 0x001C9A59
				public sRegion sRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion.CreateUnsafe(node);
				}

				// Token: 0x0600881D RID: 34845 RVA: 0x001CB861 File Offset: 0x001C9A61
				public disjunctive_match disjunctive_match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match.CreateUnsafe(node);
				}

				// Token: 0x0600881E RID: 34846 RVA: 0x001CB869 File Offset: 0x001C9A69
				public match match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match.CreateUnsafe(node);
				}

				// Token: 0x0600881F RID: 34847 RVA: 0x001CB871 File Offset: 0x001C9A71
				public token token(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token.CreateUnsafe(node);
				}

				// Token: 0x06008820 RID: 34848 RVA: 0x001CB879 File Offset: 0x001C9A79
				public multi_result multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result.CreateUnsafe(node);
				}

				// Token: 0x06008821 RID: 34849 RVA: 0x001CB881 File Offset: 0x001C9A81
				public sRegions sRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions.CreateUnsafe(node);
				}

				// Token: 0x06008822 RID: 34850 RVA: 0x001CB889 File Offset: 0x001C9A89
				public multi_result_matches multi_result_matches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches.CreateUnsafe(node);
				}

				// Token: 0x06008823 RID: 34851 RVA: 0x001CB891 File Offset: 0x001C9A91
				public inputSRegions inputSRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions.CreateUnsafe(node);
				}

				// Token: 0x06008824 RID: 34852 RVA: 0x001CB899 File Offset: 0x001C9A99
				public labelled_disjunction labelled_disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction.CreateUnsafe(node);
				}

				// Token: 0x06008825 RID: 34853 RVA: 0x001CB8A1 File Offset: 0x001C9AA1
				public labelled_multi_result labelled_multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result.CreateUnsafe(node);
				}

				// Token: 0x06008826 RID: 34854 RVA: 0x001CB8A9 File Offset: 0x001C9AA9
				public label label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label.CreateUnsafe(node);
				}

				// Token: 0x06008827 RID: 34855 RVA: 0x001CB8B1 File Offset: 0x001C9AB1
				public nil_label nil_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label.CreateUnsafe(node);
				}

				// Token: 0x06008828 RID: 34856 RVA: 0x001CB8B9 File Offset: 0x001C9AB9
				public _LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x06008829 RID: 34857 RVA: 0x001CB8C1 File Offset: 0x001C9AC1
				public _LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}

				// Token: 0x0600882A RID: 34858 RVA: 0x001CB8C9 File Offset: 0x001C9AC9
				public _LetB2 _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2.CreateUnsafe(node);
				}

				// Token: 0x0600882B RID: 34859 RVA: 0x001CB8D1 File Offset: 0x001C9AD1
				public _LetB3 _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3.CreateUnsafe(node);
				}

				// Token: 0x0600882C RID: 34860 RVA: 0x001CB8D9 File Offset: 0x001C9AD9
				public _LetB4 _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4.CreateUnsafe(node);
				}
			}

			// Token: 0x020011CC RID: 4556
			public class NodeCast
			{
				// Token: 0x0600882E RID: 34862 RVA: 0x001CB8E1 File Offset: 0x001C9AE1
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600882F RID: 34863 RVA: 0x001CB8F0 File Offset: 0x001C9AF0
				public result result(ProgramNode node)
				{
					result? result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result.CreateSafe(this._builders, node);
					if (result == null)
					{
						string text = "node";
						string text2 = "expected node for symbol result but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return result.Value;
				}

				// Token: 0x06008830 RID: 34864 RVA: 0x001CB944 File Offset: 0x001C9B44
				public sRegion sRegion(ProgramNode node)
				{
					sRegion? sRegion = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion.CreateSafe(this._builders, node);
					if (sRegion == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sRegion but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sRegion.Value;
				}

				// Token: 0x06008831 RID: 34865 RVA: 0x001CB998 File Offset: 0x001C9B98
				public disjunctive_match disjunctive_match(ProgramNode node)
				{
					disjunctive_match? disjunctive_match = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match.CreateSafe(this._builders, node);
					if (disjunctive_match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol disjunctive_match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjunctive_match.Value;
				}

				// Token: 0x06008832 RID: 34866 RVA: 0x001CB9EC File Offset: 0x001C9BEC
				public match match(ProgramNode node)
				{
					match? match = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return match.Value;
				}

				// Token: 0x06008833 RID: 34867 RVA: 0x001CBA40 File Offset: 0x001C9C40
				public token token(ProgramNode node)
				{
					token? token = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token.CreateSafe(this._builders, node);
					if (token == null)
					{
						string text = "node";
						string text2 = "expected node for symbol token but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return token.Value;
				}

				// Token: 0x06008834 RID: 34868 RVA: 0x001CBA94 File Offset: 0x001C9C94
				public multi_result multi_result(ProgramNode node)
				{
					multi_result? multi_result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result.CreateSafe(this._builders, node);
					if (multi_result == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multi_result but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multi_result.Value;
				}

				// Token: 0x06008835 RID: 34869 RVA: 0x001CBAE8 File Offset: 0x001C9CE8
				public sRegions sRegions(ProgramNode node)
				{
					sRegions? sRegions = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions.CreateSafe(this._builders, node);
					if (sRegions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sRegions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sRegions.Value;
				}

				// Token: 0x06008836 RID: 34870 RVA: 0x001CBB3C File Offset: 0x001C9D3C
				public multi_result_matches multi_result_matches(ProgramNode node)
				{
					multi_result_matches? multi_result_matches = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches.CreateSafe(this._builders, node);
					if (multi_result_matches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol multi_result_matches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return multi_result_matches.Value;
				}

				// Token: 0x06008837 RID: 34871 RVA: 0x001CBB90 File Offset: 0x001C9D90
				public inputSRegions inputSRegions(ProgramNode node)
				{
					inputSRegions? inputSRegions = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions.CreateSafe(this._builders, node);
					if (inputSRegions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputSRegions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputSRegions.Value;
				}

				// Token: 0x06008838 RID: 34872 RVA: 0x001CBBE4 File Offset: 0x001C9DE4
				public labelled_disjunction labelled_disjunction(ProgramNode node)
				{
					labelled_disjunction? labelled_disjunction = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction.CreateSafe(this._builders, node);
					if (labelled_disjunction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol labelled_disjunction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelled_disjunction.Value;
				}

				// Token: 0x06008839 RID: 34873 RVA: 0x001CBC38 File Offset: 0x001C9E38
				public labelled_multi_result labelled_multi_result(ProgramNode node)
				{
					labelled_multi_result? labelled_multi_result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result.CreateSafe(this._builders, node);
					if (labelled_multi_result == null)
					{
						string text = "node";
						string text2 = "expected node for symbol labelled_multi_result but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelled_multi_result.Value;
				}

				// Token: 0x0600883A RID: 34874 RVA: 0x001CBC8C File Offset: 0x001C9E8C
				public label label(ProgramNode node)
				{
					label? label = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label.CreateSafe(this._builders, node);
					if (label == null)
					{
						string text = "node";
						string text2 = "expected node for symbol label but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return label.Value;
				}

				// Token: 0x0600883B RID: 34875 RVA: 0x001CBCE0 File Offset: 0x001C9EE0
				public nil_label nil_label(ProgramNode node)
				{
					nil_label? nil_label = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label.CreateSafe(this._builders, node);
					if (nil_label == null)
					{
						string text = "node";
						string text2 = "expected node for symbol nil_label but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nil_label.Value;
				}

				// Token: 0x0600883C RID: 34876 RVA: 0x001CBD34 File Offset: 0x001C9F34
				public _LetB0 _LetB0(ProgramNode node)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600883D RID: 34877 RVA: 0x001CBD88 File Offset: 0x001C9F88
				public _LetB1 _LetB1(ProgramNode node)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600883E RID: 34878 RVA: 0x001CBDDC File Offset: 0x001C9FDC
				public _LetB2 _LetB2(ProgramNode node)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600883F RID: 34879 RVA: 0x001CBE30 File Offset: 0x001CA030
				public _LetB3 _LetB3(ProgramNode node)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06008840 RID: 34880 RVA: 0x001CBE84 File Offset: 0x001CA084
				public _LetB4 _LetB4(ProgramNode node)
				{
					_LetB4? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x04003855 RID: 14421
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011CD RID: 4557
			public class RuleCast
			{
				// Token: 0x06008841 RID: 34881 RVA: 0x001CBED5 File Offset: 0x001CA0D5
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008842 RID: 34882 RVA: 0x001CBEE4 File Offset: 0x001CA0E4
				public LetResult LetResult(ProgramNode node)
				{
					LetResult? letResult = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetResult.CreateSafe(this._builders, node);
					if (letResult == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetResult but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letResult.Value;
				}

				// Token: 0x06008843 RID: 34883 RVA: 0x001CBF38 File Offset: 0x001CA138
				public NoMatch NoMatch(ProgramNode node)
				{
					NoMatch? noMatch = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.NoMatch.CreateSafe(this._builders, node);
					if (noMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NoMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return noMatch.Value;
				}

				// Token: 0x06008844 RID: 34884 RVA: 0x001CBF8C File Offset: 0x001CA18C
				public Disjunction Disjunction(ProgramNode node)
				{
					Disjunction? disjunction = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
					if (disjunction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Disjunction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjunction.Value;
				}

				// Token: 0x06008845 RID: 34885 RVA: 0x001CBFE0 File Offset: 0x001CA1E0
				public SuffixAfterTokenMatch SuffixAfterTokenMatch(ProgramNode node)
				{
					SuffixAfterTokenMatch? suffixAfterTokenMatch = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.SuffixAfterTokenMatch.CreateSafe(this._builders, node);
					if (suffixAfterTokenMatch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SuffixAfterTokenMatch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return suffixAfterTokenMatch.Value;
				}

				// Token: 0x06008846 RID: 34886 RVA: 0x001CC034 File Offset: 0x001CA234
				public IsNull IsNull(ProgramNode node)
				{
					IsNull? isNull = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
					if (isNull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNull.Value;
				}

				// Token: 0x06008847 RID: 34887 RVA: 0x001CC088 File Offset: 0x001CA288
				public EndOf EndOf(ProgramNode node)
				{
					EndOf? endOf = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.EndOf.CreateSafe(this._builders, node);
					if (endOf == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndOf but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endOf.Value;
				}

				// Token: 0x06008848 RID: 34888 RVA: 0x001CC0DC File Offset: 0x001CA2DC
				public LetSplit LetSplit(ProgramNode node)
				{
					LetSplit? letSplit = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
					if (letSplit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSplit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSplit.Value;
				}

				// Token: 0x06008849 RID: 34889 RVA: 0x001CC130 File Offset: 0x001CA330
				public LetMultiResult LetMultiResult(ProgramNode node)
				{
					LetMultiResult? letMultiResult = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetMultiResult.CreateSafe(this._builders, node);
					if (letMultiResult == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetMultiResult but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letMultiResult.Value;
				}

				// Token: 0x0600884A RID: 34890 RVA: 0x001CC184 File Offset: 0x001CA384
				public Tail Tail(ProgramNode node)
				{
					Tail? tail = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Tail.CreateSafe(this._builders, node);
					if (tail == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Tail but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tail.Value;
				}

				// Token: 0x0600884B RID: 34891 RVA: 0x001CC1D8 File Offset: 0x001CA3D8
				public MatchColumns MatchColumns(ProgramNode node)
				{
					MatchColumns? matchColumns = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.MatchColumns.CreateSafe(this._builders, node);
					if (matchColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol MatchColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matchColumns.Value;
				}

				// Token: 0x0600884C RID: 34892 RVA: 0x001CC22C File Offset: 0x001CA42C
				public Head Head(ProgramNode node)
				{
					Head? head = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Head.CreateSafe(this._builders, node);
					if (head == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Head but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return head.Value;
				}

				// Token: 0x0600884D RID: 34893 RVA: 0x001CC280 File Offset: 0x001CA480
				public LetTail LetTail(ProgramNode node)
				{
					LetTail? letTail = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetTail.CreateSafe(this._builders, node);
					if (letTail == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetTail but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letTail.Value;
				}

				// Token: 0x0600884E RID: 34894 RVA: 0x001CC2D4 File Offset: 0x001CA4D4
				public Nil Nil(ProgramNode node)
				{
					Nil? nil = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Nil.CreateSafe(this._builders, node);
					if (nil == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Nil but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nil.Value;
				}

				// Token: 0x0600884F RID: 34895 RVA: 0x001CC328 File Offset: 0x001CA528
				public LetHead LetHead(ProgramNode node)
				{
					LetHead? letHead = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetHead.CreateSafe(this._builders, node);
					if (letHead == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetHead but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letHead.Value;
				}

				// Token: 0x06008850 RID: 34896 RVA: 0x001CC37C File Offset: 0x001CA57C
				public labelled_disjunction_label labelled_disjunction_label(ProgramNode node)
				{
					labelled_disjunction_label? labelled_disjunction_label = Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_disjunction_label.CreateSafe(this._builders, node);
					if (labelled_disjunction_label == null)
					{
						string text = "node";
						string text2 = "expected node for symbol labelled_disjunction_label but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelled_disjunction_label.Value;
				}

				// Token: 0x06008851 RID: 34897 RVA: 0x001CC3D0 File Offset: 0x001CA5D0
				public IfThenElse IfThenElse(ProgramNode node)
				{
					IfThenElse? ifThenElse = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
					if (ifThenElse == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IfThenElse but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ifThenElse.Value;
				}

				// Token: 0x06008852 RID: 34898 RVA: 0x001CC424 File Offset: 0x001CA624
				public labelled_multi_result_nil_label labelled_multi_result_nil_label(ProgramNode node)
				{
					labelled_multi_result_nil_label? labelled_multi_result_nil_label = Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_multi_result_nil_label.CreateSafe(this._builders, node);
					if (labelled_multi_result_nil_label == null)
					{
						string text = "node";
						string text2 = "expected node for symbol labelled_multi_result_nil_label but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelled_multi_result_nil_label.Value;
				}

				// Token: 0x06008853 RID: 34899 RVA: 0x001CC478 File Offset: 0x001CA678
				public LabelledMatchColumns LabelledMatchColumns(ProgramNode node)
				{
					LabelledMatchColumns? labelledMatchColumns = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LabelledMatchColumns.CreateSafe(this._builders, node);
					if (labelledMatchColumns == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LabelledMatchColumns but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return labelledMatchColumns.Value;
				}

				// Token: 0x04003856 RID: 14422
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011CE RID: 4558
			public class NodeIs
			{
				// Token: 0x06008854 RID: 34900 RVA: 0x001CC4C9 File Offset: 0x001CA6C9
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06008855 RID: 34901 RVA: 0x001CC4D8 File Offset: 0x001CA6D8
				public bool result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008856 RID: 34902 RVA: 0x001CC4FC File Offset: 0x001CA6FC
				public bool result(ProgramNode node, out result value)
				{
					result? result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result.CreateSafe(this._builders, node);
					if (result == null)
					{
						value = default(result);
						return false;
					}
					value = result.Value;
					return true;
				}

				// Token: 0x06008857 RID: 34903 RVA: 0x001CC538 File Offset: 0x001CA738
				public bool sRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008858 RID: 34904 RVA: 0x001CC55C File Offset: 0x001CA75C
				public bool sRegion(ProgramNode node, out sRegion value)
				{
					sRegion? sRegion = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion.CreateSafe(this._builders, node);
					if (sRegion == null)
					{
						value = default(sRegion);
						return false;
					}
					value = sRegion.Value;
					return true;
				}

				// Token: 0x06008859 RID: 34905 RVA: 0x001CC598 File Offset: 0x001CA798
				public bool disjunctive_match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600885A RID: 34906 RVA: 0x001CC5BC File Offset: 0x001CA7BC
				public bool disjunctive_match(ProgramNode node, out disjunctive_match value)
				{
					disjunctive_match? disjunctive_match = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match.CreateSafe(this._builders, node);
					if (disjunctive_match == null)
					{
						value = default(disjunctive_match);
						return false;
					}
					value = disjunctive_match.Value;
					return true;
				}

				// Token: 0x0600885B RID: 34907 RVA: 0x001CC5F8 File Offset: 0x001CA7F8
				public bool match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600885C RID: 34908 RVA: 0x001CC61C File Offset: 0x001CA81C
				public bool match(ProgramNode node, out match value)
				{
					match? match = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						value = default(match);
						return false;
					}
					value = match.Value;
					return true;
				}

				// Token: 0x0600885D RID: 34909 RVA: 0x001CC658 File Offset: 0x001CA858
				public bool token(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600885E RID: 34910 RVA: 0x001CC67C File Offset: 0x001CA87C
				public bool token(ProgramNode node, out token value)
				{
					token? token = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token.CreateSafe(this._builders, node);
					if (token == null)
					{
						value = default(token);
						return false;
					}
					value = token.Value;
					return true;
				}

				// Token: 0x0600885F RID: 34911 RVA: 0x001CC6B8 File Offset: 0x001CA8B8
				public bool multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008860 RID: 34912 RVA: 0x001CC6DC File Offset: 0x001CA8DC
				public bool multi_result(ProgramNode node, out multi_result value)
				{
					multi_result? multi_result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result.CreateSafe(this._builders, node);
					if (multi_result == null)
					{
						value = default(multi_result);
						return false;
					}
					value = multi_result.Value;
					return true;
				}

				// Token: 0x06008861 RID: 34913 RVA: 0x001CC718 File Offset: 0x001CA918
				public bool sRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008862 RID: 34914 RVA: 0x001CC73C File Offset: 0x001CA93C
				public bool sRegions(ProgramNode node, out sRegions value)
				{
					sRegions? sRegions = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions.CreateSafe(this._builders, node);
					if (sRegions == null)
					{
						value = default(sRegions);
						return false;
					}
					value = sRegions.Value;
					return true;
				}

				// Token: 0x06008863 RID: 34915 RVA: 0x001CC778 File Offset: 0x001CA978
				public bool multi_result_matches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008864 RID: 34916 RVA: 0x001CC79C File Offset: 0x001CA99C
				public bool multi_result_matches(ProgramNode node, out multi_result_matches value)
				{
					multi_result_matches? multi_result_matches = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches.CreateSafe(this._builders, node);
					if (multi_result_matches == null)
					{
						value = default(multi_result_matches);
						return false;
					}
					value = multi_result_matches.Value;
					return true;
				}

				// Token: 0x06008865 RID: 34917 RVA: 0x001CC7D8 File Offset: 0x001CA9D8
				public bool inputSRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008866 RID: 34918 RVA: 0x001CC7FC File Offset: 0x001CA9FC
				public bool inputSRegions(ProgramNode node, out inputSRegions value)
				{
					inputSRegions? inputSRegions = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions.CreateSafe(this._builders, node);
					if (inputSRegions == null)
					{
						value = default(inputSRegions);
						return false;
					}
					value = inputSRegions.Value;
					return true;
				}

				// Token: 0x06008867 RID: 34919 RVA: 0x001CC838 File Offset: 0x001CAA38
				public bool labelled_disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008868 RID: 34920 RVA: 0x001CC85C File Offset: 0x001CAA5C
				public bool labelled_disjunction(ProgramNode node, out labelled_disjunction value)
				{
					labelled_disjunction? labelled_disjunction = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction.CreateSafe(this._builders, node);
					if (labelled_disjunction == null)
					{
						value = default(labelled_disjunction);
						return false;
					}
					value = labelled_disjunction.Value;
					return true;
				}

				// Token: 0x06008869 RID: 34921 RVA: 0x001CC898 File Offset: 0x001CAA98
				public bool labelled_multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600886A RID: 34922 RVA: 0x001CC8BC File Offset: 0x001CAABC
				public bool labelled_multi_result(ProgramNode node, out labelled_multi_result value)
				{
					labelled_multi_result? labelled_multi_result = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result.CreateSafe(this._builders, node);
					if (labelled_multi_result == null)
					{
						value = default(labelled_multi_result);
						return false;
					}
					value = labelled_multi_result.Value;
					return true;
				}

				// Token: 0x0600886B RID: 34923 RVA: 0x001CC8F8 File Offset: 0x001CAAF8
				public bool label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600886C RID: 34924 RVA: 0x001CC91C File Offset: 0x001CAB1C
				public bool label(ProgramNode node, out label value)
				{
					label? label = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label.CreateSafe(this._builders, node);
					if (label == null)
					{
						value = default(label);
						return false;
					}
					value = label.Value;
					return true;
				}

				// Token: 0x0600886D RID: 34925 RVA: 0x001CC958 File Offset: 0x001CAB58
				public bool nil_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600886E RID: 34926 RVA: 0x001CC97C File Offset: 0x001CAB7C
				public bool nil_label(ProgramNode node, out nil_label value)
				{
					nil_label? nil_label = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label.CreateSafe(this._builders, node);
					if (nil_label == null)
					{
						value = default(nil_label);
						return false;
					}
					value = nil_label.Value;
					return true;
				}

				// Token: 0x0600886F RID: 34927 RVA: 0x001CC9B8 File Offset: 0x001CABB8
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008870 RID: 34928 RVA: 0x001CC9DC File Offset: 0x001CABDC
				public bool _LetB0(ProgramNode node, out _LetB0 value)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06008871 RID: 34929 RVA: 0x001CCA18 File Offset: 0x001CAC18
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008872 RID: 34930 RVA: 0x001CCA3C File Offset: 0x001CAC3C
				public bool _LetB1(ProgramNode node, out _LetB1 value)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06008873 RID: 34931 RVA: 0x001CCA78 File Offset: 0x001CAC78
				public bool _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008874 RID: 34932 RVA: 0x001CCA9C File Offset: 0x001CAC9C
				public bool _LetB2(ProgramNode node, out _LetB2 value)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB2);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06008875 RID: 34933 RVA: 0x001CCAD8 File Offset: 0x001CACD8
				public bool _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008876 RID: 34934 RVA: 0x001CCAFC File Offset: 0x001CACFC
				public bool _LetB3(ProgramNode node, out _LetB3 value)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB3);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06008877 RID: 34935 RVA: 0x001CCB38 File Offset: 0x001CAD38
				public bool _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008878 RID: 34936 RVA: 0x001CCB5C File Offset: 0x001CAD5C
				public bool _LetB4(ProgramNode node, out _LetB4 value)
				{
					_LetB4? letB = Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB4);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x04003857 RID: 14423
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011CF RID: 4559
			public class RuleIs
			{
				// Token: 0x06008879 RID: 34937 RVA: 0x001CCB96 File Offset: 0x001CAD96
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600887A RID: 34938 RVA: 0x001CCBA8 File Offset: 0x001CADA8
				public bool LetResult(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetResult.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600887B RID: 34939 RVA: 0x001CCBCC File Offset: 0x001CADCC
				public bool LetResult(ProgramNode node, out LetResult value)
				{
					LetResult? letResult = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetResult.CreateSafe(this._builders, node);
					if (letResult == null)
					{
						value = default(LetResult);
						return false;
					}
					value = letResult.Value;
					return true;
				}

				// Token: 0x0600887C RID: 34940 RVA: 0x001CCC08 File Offset: 0x001CAE08
				public bool NoMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.NoMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600887D RID: 34941 RVA: 0x001CCC2C File Offset: 0x001CAE2C
				public bool NoMatch(ProgramNode node, out NoMatch value)
				{
					NoMatch? noMatch = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.NoMatch.CreateSafe(this._builders, node);
					if (noMatch == null)
					{
						value = default(NoMatch);
						return false;
					}
					value = noMatch.Value;
					return true;
				}

				// Token: 0x0600887E RID: 34942 RVA: 0x001CCC68 File Offset: 0x001CAE68
				public bool Disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600887F RID: 34943 RVA: 0x001CCC8C File Offset: 0x001CAE8C
				public bool Disjunction(ProgramNode node, out Disjunction value)
				{
					Disjunction? disjunction = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
					if (disjunction == null)
					{
						value = default(Disjunction);
						return false;
					}
					value = disjunction.Value;
					return true;
				}

				// Token: 0x06008880 RID: 34944 RVA: 0x001CCCC8 File Offset: 0x001CAEC8
				public bool SuffixAfterTokenMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.SuffixAfterTokenMatch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008881 RID: 34945 RVA: 0x001CCCEC File Offset: 0x001CAEEC
				public bool SuffixAfterTokenMatch(ProgramNode node, out SuffixAfterTokenMatch value)
				{
					SuffixAfterTokenMatch? suffixAfterTokenMatch = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.SuffixAfterTokenMatch.CreateSafe(this._builders, node);
					if (suffixAfterTokenMatch == null)
					{
						value = default(SuffixAfterTokenMatch);
						return false;
					}
					value = suffixAfterTokenMatch.Value;
					return true;
				}

				// Token: 0x06008882 RID: 34946 RVA: 0x001CCD28 File Offset: 0x001CAF28
				public bool IsNull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008883 RID: 34947 RVA: 0x001CCD4C File Offset: 0x001CAF4C
				public bool IsNull(ProgramNode node, out IsNull value)
				{
					IsNull? isNull = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
					if (isNull == null)
					{
						value = default(IsNull);
						return false;
					}
					value = isNull.Value;
					return true;
				}

				// Token: 0x06008884 RID: 34948 RVA: 0x001CCD88 File Offset: 0x001CAF88
				public bool EndOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.EndOf.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008885 RID: 34949 RVA: 0x001CCDAC File Offset: 0x001CAFAC
				public bool EndOf(ProgramNode node, out EndOf value)
				{
					EndOf? endOf = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.EndOf.CreateSafe(this._builders, node);
					if (endOf == null)
					{
						value = default(EndOf);
						return false;
					}
					value = endOf.Value;
					return true;
				}

				// Token: 0x06008886 RID: 34950 RVA: 0x001CCDE8 File Offset: 0x001CAFE8
				public bool LetSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008887 RID: 34951 RVA: 0x001CCE0C File Offset: 0x001CB00C
				public bool LetSplit(ProgramNode node, out LetSplit value)
				{
					LetSplit? letSplit = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
					if (letSplit == null)
					{
						value = default(LetSplit);
						return false;
					}
					value = letSplit.Value;
					return true;
				}

				// Token: 0x06008888 RID: 34952 RVA: 0x001CCE48 File Offset: 0x001CB048
				public bool LetMultiResult(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetMultiResult.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008889 RID: 34953 RVA: 0x001CCE6C File Offset: 0x001CB06C
				public bool LetMultiResult(ProgramNode node, out LetMultiResult value)
				{
					LetMultiResult? letMultiResult = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetMultiResult.CreateSafe(this._builders, node);
					if (letMultiResult == null)
					{
						value = default(LetMultiResult);
						return false;
					}
					value = letMultiResult.Value;
					return true;
				}

				// Token: 0x0600888A RID: 34954 RVA: 0x001CCEA8 File Offset: 0x001CB0A8
				public bool Tail(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Tail.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600888B RID: 34955 RVA: 0x001CCECC File Offset: 0x001CB0CC
				public bool Tail(ProgramNode node, out Tail value)
				{
					Tail? tail = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Tail.CreateSafe(this._builders, node);
					if (tail == null)
					{
						value = default(Tail);
						return false;
					}
					value = tail.Value;
					return true;
				}

				// Token: 0x0600888C RID: 34956 RVA: 0x001CCF08 File Offset: 0x001CB108
				public bool MatchColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.MatchColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600888D RID: 34957 RVA: 0x001CCF2C File Offset: 0x001CB12C
				public bool MatchColumns(ProgramNode node, out MatchColumns value)
				{
					MatchColumns? matchColumns = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.MatchColumns.CreateSafe(this._builders, node);
					if (matchColumns == null)
					{
						value = default(MatchColumns);
						return false;
					}
					value = matchColumns.Value;
					return true;
				}

				// Token: 0x0600888E RID: 34958 RVA: 0x001CCF68 File Offset: 0x001CB168
				public bool Head(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Head.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600888F RID: 34959 RVA: 0x001CCF8C File Offset: 0x001CB18C
				public bool Head(ProgramNode node, out Head value)
				{
					Head? head = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Head.CreateSafe(this._builders, node);
					if (head == null)
					{
						value = default(Head);
						return false;
					}
					value = head.Value;
					return true;
				}

				// Token: 0x06008890 RID: 34960 RVA: 0x001CCFC8 File Offset: 0x001CB1C8
				public bool LetTail(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetTail.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008891 RID: 34961 RVA: 0x001CCFEC File Offset: 0x001CB1EC
				public bool LetTail(ProgramNode node, out LetTail value)
				{
					LetTail? letTail = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetTail.CreateSafe(this._builders, node);
					if (letTail == null)
					{
						value = default(LetTail);
						return false;
					}
					value = letTail.Value;
					return true;
				}

				// Token: 0x06008892 RID: 34962 RVA: 0x001CD028 File Offset: 0x001CB228
				public bool Nil(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Nil.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008893 RID: 34963 RVA: 0x001CD04C File Offset: 0x001CB24C
				public bool Nil(ProgramNode node, out Nil value)
				{
					Nil? nil = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Nil.CreateSafe(this._builders, node);
					if (nil == null)
					{
						value = default(Nil);
						return false;
					}
					value = nil.Value;
					return true;
				}

				// Token: 0x06008894 RID: 34964 RVA: 0x001CD088 File Offset: 0x001CB288
				public bool LetHead(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetHead.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008895 RID: 34965 RVA: 0x001CD0AC File Offset: 0x001CB2AC
				public bool LetHead(ProgramNode node, out LetHead value)
				{
					LetHead? letHead = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetHead.CreateSafe(this._builders, node);
					if (letHead == null)
					{
						value = default(LetHead);
						return false;
					}
					value = letHead.Value;
					return true;
				}

				// Token: 0x06008896 RID: 34966 RVA: 0x001CD0E8 File Offset: 0x001CB2E8
				public bool labelled_disjunction_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_disjunction_label.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008897 RID: 34967 RVA: 0x001CD10C File Offset: 0x001CB30C
				public bool labelled_disjunction_label(ProgramNode node, out labelled_disjunction_label value)
				{
					labelled_disjunction_label? labelled_disjunction_label = Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_disjunction_label.CreateSafe(this._builders, node);
					if (labelled_disjunction_label == null)
					{
						value = default(labelled_disjunction_label);
						return false;
					}
					value = labelled_disjunction_label.Value;
					return true;
				}

				// Token: 0x06008898 RID: 34968 RVA: 0x001CD148 File Offset: 0x001CB348
				public bool IfThenElse(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06008899 RID: 34969 RVA: 0x001CD16C File Offset: 0x001CB36C
				public bool IfThenElse(ProgramNode node, out IfThenElse value)
				{
					IfThenElse? ifThenElse = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
					if (ifThenElse == null)
					{
						value = default(IfThenElse);
						return false;
					}
					value = ifThenElse.Value;
					return true;
				}

				// Token: 0x0600889A RID: 34970 RVA: 0x001CD1A8 File Offset: 0x001CB3A8
				public bool labelled_multi_result_nil_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_multi_result_nil_label.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600889B RID: 34971 RVA: 0x001CD1CC File Offset: 0x001CB3CC
				public bool labelled_multi_result_nil_label(ProgramNode node, out labelled_multi_result_nil_label value)
				{
					labelled_multi_result_nil_label? labelled_multi_result_nil_label = Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_multi_result_nil_label.CreateSafe(this._builders, node);
					if (labelled_multi_result_nil_label == null)
					{
						value = default(labelled_multi_result_nil_label);
						return false;
					}
					value = labelled_multi_result_nil_label.Value;
					return true;
				}

				// Token: 0x0600889C RID: 34972 RVA: 0x001CD208 File Offset: 0x001CB408
				public bool LabelledMatchColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LabelledMatchColumns.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600889D RID: 34973 RVA: 0x001CD22C File Offset: 0x001CB42C
				public bool LabelledMatchColumns(ProgramNode node, out LabelledMatchColumns value)
				{
					LabelledMatchColumns? labelledMatchColumns = Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LabelledMatchColumns.CreateSafe(this._builders, node);
					if (labelledMatchColumns == null)
					{
						value = default(LabelledMatchColumns);
						return false;
					}
					value = labelledMatchColumns.Value;
					return true;
				}

				// Token: 0x04003858 RID: 14424
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D0 RID: 4560
			public class NodeAs
			{
				// Token: 0x0600889E RID: 34974 RVA: 0x001CD266 File Offset: 0x001CB466
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600889F RID: 34975 RVA: 0x001CD275 File Offset: 0x001CB475
				public result? result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A0 RID: 34976 RVA: 0x001CD283 File Offset: 0x001CB483
				public sRegion? sRegion(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A1 RID: 34977 RVA: 0x001CD291 File Offset: 0x001CB491
				public disjunctive_match? disjunctive_match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A2 RID: 34978 RVA: 0x001CD29F File Offset: 0x001CB49F
				public match? match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A3 RID: 34979 RVA: 0x001CD2AD File Offset: 0x001CB4AD
				public token? token(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A4 RID: 34980 RVA: 0x001CD2BB File Offset: 0x001CB4BB
				public multi_result? multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A5 RID: 34981 RVA: 0x001CD2C9 File Offset: 0x001CB4C9
				public sRegions? sRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A6 RID: 34982 RVA: 0x001CD2D7 File Offset: 0x001CB4D7
				public multi_result_matches? multi_result_matches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A7 RID: 34983 RVA: 0x001CD2E5 File Offset: 0x001CB4E5
				public inputSRegions? inputSRegions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A8 RID: 34984 RVA: 0x001CD2F3 File Offset: 0x001CB4F3
				public labelled_disjunction? labelled_disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction.CreateSafe(this._builders, node);
				}

				// Token: 0x060088A9 RID: 34985 RVA: 0x001CD301 File Offset: 0x001CB501
				public labelled_multi_result? labelled_multi_result(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AA RID: 34986 RVA: 0x001CD30F File Offset: 0x001CB50F
				public label? label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AB RID: 34987 RVA: 0x001CD31D File Offset: 0x001CB51D
				public nil_label? nil_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AC RID: 34988 RVA: 0x001CD32B File Offset: 0x001CB52B
				public _LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AD RID: 34989 RVA: 0x001CD339 File Offset: 0x001CB539
				public _LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AE RID: 34990 RVA: 0x001CD347 File Offset: 0x001CB547
				public _LetB2? _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
				}

				// Token: 0x060088AF RID: 34991 RVA: 0x001CD355 File Offset: 0x001CB555
				public _LetB3? _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B0 RID: 34992 RVA: 0x001CD363 File Offset: 0x001CB563
				public _LetB4? _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
				}

				// Token: 0x04003859 RID: 14425
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D1 RID: 4561
			public class RuleAs
			{
				// Token: 0x060088B1 RID: 34993 RVA: 0x001CD371 File Offset: 0x001CB571
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088B2 RID: 34994 RVA: 0x001CD380 File Offset: 0x001CB580
				public LetResult? LetResult(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetResult.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B3 RID: 34995 RVA: 0x001CD38E File Offset: 0x001CB58E
				public NoMatch? NoMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.NoMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B4 RID: 34996 RVA: 0x001CD39C File Offset: 0x001CB59C
				public Disjunction? Disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B5 RID: 34997 RVA: 0x001CD3AA File Offset: 0x001CB5AA
				public SuffixAfterTokenMatch? SuffixAfterTokenMatch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.SuffixAfterTokenMatch.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B6 RID: 34998 RVA: 0x001CD3B8 File Offset: 0x001CB5B8
				public IsNull? IsNull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B7 RID: 34999 RVA: 0x001CD3C6 File Offset: 0x001CB5C6
				public EndOf? EndOf(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.EndOf.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B8 RID: 35000 RVA: 0x001CD3D4 File Offset: 0x001CB5D4
				public LetSplit? LetSplit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetSplit.CreateSafe(this._builders, node);
				}

				// Token: 0x060088B9 RID: 35001 RVA: 0x001CD3E2 File Offset: 0x001CB5E2
				public LetMultiResult? LetMultiResult(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetMultiResult.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BA RID: 35002 RVA: 0x001CD3F0 File Offset: 0x001CB5F0
				public Tail? Tail(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Tail.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BB RID: 35003 RVA: 0x001CD3FE File Offset: 0x001CB5FE
				public MatchColumns? MatchColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.MatchColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BC RID: 35004 RVA: 0x001CD40C File Offset: 0x001CB60C
				public Head? Head(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Head.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BD RID: 35005 RVA: 0x001CD41A File Offset: 0x001CB61A
				public LetTail? LetTail(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetTail.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BE RID: 35006 RVA: 0x001CD428 File Offset: 0x001CB628
				public Nil? Nil(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.Nil.CreateSafe(this._builders, node);
				}

				// Token: 0x060088BF RID: 35007 RVA: 0x001CD436 File Offset: 0x001CB636
				public LetHead? LetHead(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LetHead.CreateSafe(this._builders, node);
				}

				// Token: 0x060088C0 RID: 35008 RVA: 0x001CD444 File Offset: 0x001CB644
				public labelled_disjunction_label? labelled_disjunction_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_disjunction_label.CreateSafe(this._builders, node);
				}

				// Token: 0x060088C1 RID: 35009 RVA: 0x001CD452 File Offset: 0x001CB652
				public IfThenElse? IfThenElse(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
				}

				// Token: 0x060088C2 RID: 35010 RVA: 0x001CD460 File Offset: 0x001CB660
				public labelled_multi_result_nil_label? labelled_multi_result_nil_label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.UnnamedConversionNodeTypes.labelled_multi_result_nil_label.CreateSafe(this._builders, node);
				}

				// Token: 0x060088C3 RID: 35011 RVA: 0x001CD46E File Offset: 0x001CB66E
				public LabelledMatchColumns? LabelledMatchColumns(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes.LabelledMatchColumns.CreateSafe(this._builders, node);
				}

				// Token: 0x0400385A RID: 14426
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x020011D3 RID: 4563
		public class Sets
		{
			// Token: 0x060088C7 RID: 35015 RVA: 0x001CD498 File Offset: 0x001CB698
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x1700178B RID: 6027
			// (get) Token: 0x060088C8 RID: 35016 RVA: 0x001CD4E7 File Offset: 0x001CB6E7
			// (set) Token: 0x060088C9 RID: 35017 RVA: 0x001CD4EF File Offset: 0x001CB6EF
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x1700178C RID: 6028
			// (get) Token: 0x060088CA RID: 35018 RVA: 0x001CD4F8 File Offset: 0x001CB6F8
			// (set) Token: 0x060088CB RID: 35019 RVA: 0x001CD500 File Offset: 0x001CB700
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x1700178D RID: 6029
			// (get) Token: 0x060088CC RID: 35020 RVA: 0x001CD509 File Offset: 0x001CB709
			// (set) Token: 0x060088CD RID: 35021 RVA: 0x001CD511 File Offset: 0x001CB711
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x1700178E RID: 6030
			// (get) Token: 0x060088CE RID: 35022 RVA: 0x001CD51A File Offset: 0x001CB71A
			// (set) Token: 0x060088CF RID: 35023 RVA: 0x001CD522 File Offset: 0x001CB722
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x1700178F RID: 6031
			// (get) Token: 0x060088D0 RID: 35024 RVA: 0x001CD52B File Offset: 0x001CB72B
			// (set) Token: 0x060088D1 RID: 35025 RVA: 0x001CD533 File Offset: 0x001CB733
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x020011D4 RID: 4564
			public class Joins
			{
				// Token: 0x060088D2 RID: 35026 RVA: 0x001CD53C File Offset: 0x001CB73C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088D3 RID: 35027 RVA: 0x001CD54B File Offset: 0x001CB74B
				public ProgramSetBuilder<disjunctive_match> NoMatch()
				{
					return ProgramSetBuilder<disjunctive_match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NoMatch, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060088D4 RID: 35028 RVA: 0x001CD56C File Offset: 0x001CB76C
				public ProgramSetBuilder<disjunctive_match> Disjunction(ProgramSetBuilder<match> value0, ProgramSetBuilder<disjunctive_match> value1)
				{
					return ProgramSetBuilder<disjunctive_match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Disjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088D5 RID: 35029 RVA: 0x001CD5AC File Offset: 0x001CB7AC
				public ProgramSetBuilder<_LetB0> SuffixAfterTokenMatch(ProgramSetBuilder<sRegion> value0, ProgramSetBuilder<token> value1)
				{
					return ProgramSetBuilder<_LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SuffixAfterTokenMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088D6 RID: 35030 RVA: 0x001CD5EC File Offset: 0x001CB7EC
				public ProgramSetBuilder<match> IsNull(ProgramSetBuilder<sRegion> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088D7 RID: 35031 RVA: 0x001CD61D File Offset: 0x001CB81D
				public ProgramSetBuilder<match> EndOf(ProgramSetBuilder<sRegion> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088D8 RID: 35032 RVA: 0x001CD64E File Offset: 0x001CB84E
				public ProgramSetBuilder<_LetB1> Tail(ProgramSetBuilder<sRegions> value0)
				{
					return ProgramSetBuilder<_LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Tail, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088D9 RID: 35033 RVA: 0x001CD67F File Offset: 0x001CB87F
				public ProgramSetBuilder<_LetB2> MatchColumns(ProgramSetBuilder<disjunctive_match> value0, ProgramSetBuilder<multi_result_matches> value1)
				{
					return ProgramSetBuilder<_LetB2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.MatchColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088DA RID: 35034 RVA: 0x001CD6BF File Offset: 0x001CB8BF
				public ProgramSetBuilder<_LetB3> Head(ProgramSetBuilder<sRegions> value0)
				{
					return ProgramSetBuilder<_LetB3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Head, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088DB RID: 35035 RVA: 0x001CD6F0 File Offset: 0x001CB8F0
				public ProgramSetBuilder<multi_result_matches> Nil(ProgramSetBuilder<sRegions> value0)
				{
					return ProgramSetBuilder<multi_result_matches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Nil, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088DC RID: 35036 RVA: 0x001CD724 File Offset: 0x001CB924
				public ProgramSetBuilder<labelled_disjunction> IfThenElse(ProgramSetBuilder<match> value0, ProgramSetBuilder<label> value1, ProgramSetBuilder<labelled_disjunction> value2)
				{
					return ProgramSetBuilder<labelled_disjunction>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IfThenElse, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060088DD RID: 35037 RVA: 0x001CD77E File Offset: 0x001CB97E
				public ProgramSetBuilder<labelled_multi_result> LabelledMatchColumns(ProgramSetBuilder<labelled_disjunction> value0, ProgramSetBuilder<labelled_multi_result> value1)
				{
					return ProgramSetBuilder<labelled_multi_result>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LabelledMatchColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088DE RID: 35038 RVA: 0x001CD7BE File Offset: 0x001CB9BE
				public ProgramSetBuilder<result> LetResult(ProgramSetBuilder<inputSRegion> value0, ProgramSetBuilder<disjunctive_match> value1)
				{
					return ProgramSetBuilder<result>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetResult, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088DF RID: 35039 RVA: 0x001CD7FE File Offset: 0x001CB9FE
				public ProgramSetBuilder<match> LetSplit(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<match> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088E0 RID: 35040 RVA: 0x001CD83E File Offset: 0x001CBA3E
				public ProgramSetBuilder<multi_result> LetMultiResult(ProgramSetBuilder<inputSRegions> value0, ProgramSetBuilder<multi_result_matches> value1)
				{
					return ProgramSetBuilder<multi_result>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetMultiResult, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088E1 RID: 35041 RVA: 0x001CD87E File Offset: 0x001CBA7E
				public ProgramSetBuilder<_LetB4> LetTail(ProgramSetBuilder<_LetB1> value0, ProgramSetBuilder<_LetB2> value1)
				{
					return ProgramSetBuilder<_LetB4>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetTail, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088E2 RID: 35042 RVA: 0x001CD8BE File Offset: 0x001CBABE
				public ProgramSetBuilder<multi_result_matches> LetHead(ProgramSetBuilder<_LetB3> value0, ProgramSetBuilder<_LetB4> value1)
				{
					return ProgramSetBuilder<multi_result_matches>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetHead, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003861 RID: 14433
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D5 RID: 4565
			public class ExplicitJoins
			{
				// Token: 0x060088E3 RID: 35043 RVA: 0x001CD8FE File Offset: 0x001CBAFE
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088E4 RID: 35044 RVA: 0x001CD90D File Offset: 0x001CBB0D
				public JoinProgramSetBuilder<disjunctive_match> NoMatch()
				{
					return JoinProgramSetBuilder<disjunctive_match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NoMatch, Array.Empty<ProgramSet>()));
				}

				// Token: 0x060088E5 RID: 35045 RVA: 0x001CD92E File Offset: 0x001CBB2E
				public JoinProgramSetBuilder<disjunctive_match> Disjunction(ProgramSetBuilder<match> value0, ProgramSetBuilder<disjunctive_match> value1)
				{
					return JoinProgramSetBuilder<disjunctive_match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Disjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088E6 RID: 35046 RVA: 0x001CD96E File Offset: 0x001CBB6E
				public JoinProgramSetBuilder<_LetB0> SuffixAfterTokenMatch(ProgramSetBuilder<sRegion> value0, ProgramSetBuilder<token> value1)
				{
					return JoinProgramSetBuilder<_LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SuffixAfterTokenMatch, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088E7 RID: 35047 RVA: 0x001CD9AE File Offset: 0x001CBBAE
				public JoinProgramSetBuilder<match> IsNull(ProgramSetBuilder<sRegion> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088E8 RID: 35048 RVA: 0x001CD9DF File Offset: 0x001CBBDF
				public JoinProgramSetBuilder<match> EndOf(ProgramSetBuilder<sRegion> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndOf, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088E9 RID: 35049 RVA: 0x001CDA10 File Offset: 0x001CBC10
				public JoinProgramSetBuilder<_LetB1> Tail(ProgramSetBuilder<sRegions> value0)
				{
					return JoinProgramSetBuilder<_LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Tail, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088EA RID: 35050 RVA: 0x001CDA41 File Offset: 0x001CBC41
				public JoinProgramSetBuilder<_LetB2> MatchColumns(ProgramSetBuilder<disjunctive_match> value0, ProgramSetBuilder<multi_result_matches> value1)
				{
					return JoinProgramSetBuilder<_LetB2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.MatchColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088EB RID: 35051 RVA: 0x001CDA81 File Offset: 0x001CBC81
				public JoinProgramSetBuilder<_LetB3> Head(ProgramSetBuilder<sRegions> value0)
				{
					return JoinProgramSetBuilder<_LetB3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Head, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088EC RID: 35052 RVA: 0x001CDAB2 File Offset: 0x001CBCB2
				public JoinProgramSetBuilder<multi_result_matches> Nil(ProgramSetBuilder<sRegions> value0)
				{
					return JoinProgramSetBuilder<multi_result_matches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Nil, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088ED RID: 35053 RVA: 0x001CDAE4 File Offset: 0x001CBCE4
				public JoinProgramSetBuilder<labelled_disjunction> IfThenElse(ProgramSetBuilder<match> value0, ProgramSetBuilder<label> value1, ProgramSetBuilder<labelled_disjunction> value2)
				{
					return JoinProgramSetBuilder<labelled_disjunction>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IfThenElse, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060088EE RID: 35054 RVA: 0x001CDB3E File Offset: 0x001CBD3E
				public JoinProgramSetBuilder<labelled_multi_result> LabelledMatchColumns(ProgramSetBuilder<labelled_disjunction> value0, ProgramSetBuilder<labelled_multi_result> value1)
				{
					return JoinProgramSetBuilder<labelled_multi_result>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LabelledMatchColumns, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088EF RID: 35055 RVA: 0x001CDB7E File Offset: 0x001CBD7E
				public JoinProgramSetBuilder<result> LetResult(ProgramSetBuilder<inputSRegion> value0, ProgramSetBuilder<disjunctive_match> value1)
				{
					return JoinProgramSetBuilder<result>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetResult, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088F0 RID: 35056 RVA: 0x001CDBBE File Offset: 0x001CBDBE
				public JoinProgramSetBuilder<match> LetSplit(ProgramSetBuilder<_LetB0> value0, ProgramSetBuilder<match> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSplit, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088F1 RID: 35057 RVA: 0x001CDBFE File Offset: 0x001CBDFE
				public JoinProgramSetBuilder<multi_result> LetMultiResult(ProgramSetBuilder<inputSRegions> value0, ProgramSetBuilder<multi_result_matches> value1)
				{
					return JoinProgramSetBuilder<multi_result>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetMultiResult, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088F2 RID: 35058 RVA: 0x001CDC3E File Offset: 0x001CBE3E
				public JoinProgramSetBuilder<_LetB4> LetTail(ProgramSetBuilder<_LetB1> value0, ProgramSetBuilder<_LetB2> value1)
				{
					return JoinProgramSetBuilder<_LetB4>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetTail, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060088F3 RID: 35059 RVA: 0x001CDC7E File Offset: 0x001CBE7E
				public JoinProgramSetBuilder<multi_result_matches> LetHead(ProgramSetBuilder<_LetB3> value0, ProgramSetBuilder<_LetB4> value1)
				{
					return JoinProgramSetBuilder<multi_result_matches>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetHead, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04003862 RID: 14434
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D6 RID: 4566
			public class JoinUnnamedConversions
			{
				// Token: 0x060088F4 RID: 35060 RVA: 0x001CDCBE File Offset: 0x001CBEBE
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088F5 RID: 35061 RVA: 0x001CDCCD File Offset: 0x001CBECD
				public ProgramSetBuilder<labelled_disjunction> labelled_disjunction_label(ProgramSetBuilder<label> value0)
				{
					return ProgramSetBuilder<labelled_disjunction>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.labelled_disjunction_label, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088F6 RID: 35062 RVA: 0x001CDCFE File Offset: 0x001CBEFE
				public ProgramSetBuilder<labelled_multi_result> labelled_multi_result_nil_label(ProgramSetBuilder<nil_label> value0)
				{
					return ProgramSetBuilder<labelled_multi_result>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.labelled_multi_result_nil_label, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04003863 RID: 14435
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D7 RID: 4567
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x060088F7 RID: 35063 RVA: 0x001CDD2F File Offset: 0x001CBF2F
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088F8 RID: 35064 RVA: 0x001CDD3E File Offset: 0x001CBF3E
				public JoinProgramSetBuilder<labelled_disjunction> labelled_disjunction_label(ProgramSetBuilder<label> value0)
				{
					return JoinProgramSetBuilder<labelled_disjunction>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.labelled_disjunction_label, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060088F9 RID: 35065 RVA: 0x001CDD6F File Offset: 0x001CBF6F
				public JoinProgramSetBuilder<labelled_multi_result> labelled_multi_result_nil_label(ProgramSetBuilder<nil_label> value0)
				{
					return JoinProgramSetBuilder<labelled_multi_result>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.labelled_multi_result_nil_label, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04003864 RID: 14436
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x020011D8 RID: 4568
			public class Casts
			{
				// Token: 0x060088FA RID: 35066 RVA: 0x001CDDA0 File Offset: 0x001CBFA0
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060088FB RID: 35067 RVA: 0x001CDDB0 File Offset: 0x001CBFB0
				public ProgramSetBuilder<result> result(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.result)
					{
						string text = "set";
						string text2 = "expected program set for symbol result but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.result>.CreateUnsafe(set);
				}

				// Token: 0x060088FC RID: 35068 RVA: 0x001CDE08 File Offset: 0x001CC008
				public ProgramSetBuilder<sRegion> sRegion(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sRegion)
					{
						string text = "set";
						string text2 = "expected program set for symbol sRegion but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegion>.CreateUnsafe(set);
				}

				// Token: 0x060088FD RID: 35069 RVA: 0x001CDE60 File Offset: 0x001CC060
				public ProgramSetBuilder<disjunctive_match> disjunctive_match(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.disjunctive_match)
					{
						string text = "set";
						string text2 = "expected program set for symbol disjunctive_match but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.disjunctive_match>.CreateUnsafe(set);
				}

				// Token: 0x060088FE RID: 35070 RVA: 0x001CDEB8 File Offset: 0x001CC0B8
				public ProgramSetBuilder<match> match(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.match)
					{
						string text = "set";
						string text2 = "expected program set for symbol match but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.match>.CreateUnsafe(set);
				}

				// Token: 0x060088FF RID: 35071 RVA: 0x001CDF10 File Offset: 0x001CC110
				public ProgramSetBuilder<token> token(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.token)
					{
						string text = "set";
						string text2 = "expected program set for symbol token but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.token>.CreateUnsafe(set);
				}

				// Token: 0x06008900 RID: 35072 RVA: 0x001CDF68 File Offset: 0x001CC168
				public ProgramSetBuilder<multi_result> multi_result(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.multi_result)
					{
						string text = "set";
						string text2 = "expected program set for symbol multi_result but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result>.CreateUnsafe(set);
				}

				// Token: 0x06008901 RID: 35073 RVA: 0x001CDFC0 File Offset: 0x001CC1C0
				public ProgramSetBuilder<sRegions> sRegions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sRegions)
					{
						string text = "set";
						string text2 = "expected program set for symbol sRegions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.sRegions>.CreateUnsafe(set);
				}

				// Token: 0x06008902 RID: 35074 RVA: 0x001CE018 File Offset: 0x001CC218
				public ProgramSetBuilder<multi_result_matches> multi_result_matches(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.multi_result_matches)
					{
						string text = "set";
						string text2 = "expected program set for symbol multi_result_matches but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.multi_result_matches>.CreateUnsafe(set);
				}

				// Token: 0x06008903 RID: 35075 RVA: 0x001CE070 File Offset: 0x001CC270
				public ProgramSetBuilder<inputSRegions> inputSRegions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inputSRegions)
					{
						string text = "set";
						string text2 = "expected program set for symbol inputSRegions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.inputSRegions>.CreateUnsafe(set);
				}

				// Token: 0x06008904 RID: 35076 RVA: 0x001CE0C8 File Offset: 0x001CC2C8
				public ProgramSetBuilder<labelled_disjunction> labelled_disjunction(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.labelled_disjunction)
					{
						string text = "set";
						string text2 = "expected program set for symbol labelled_disjunction but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_disjunction>.CreateUnsafe(set);
				}

				// Token: 0x06008905 RID: 35077 RVA: 0x001CE120 File Offset: 0x001CC320
				public ProgramSetBuilder<labelled_multi_result> labelled_multi_result(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.labelled_multi_result)
					{
						string text = "set";
						string text2 = "expected program set for symbol labelled_multi_result but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.labelled_multi_result>.CreateUnsafe(set);
				}

				// Token: 0x06008906 RID: 35078 RVA: 0x001CE178 File Offset: 0x001CC378
				public ProgramSetBuilder<label> label(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.label)
					{
						string text = "set";
						string text2 = "expected program set for symbol label but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.label>.CreateUnsafe(set);
				}

				// Token: 0x06008907 RID: 35079 RVA: 0x001CE1D0 File Offset: 0x001CC3D0
				public ProgramSetBuilder<nil_label> nil_label(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.nil_label)
					{
						string text = "set";
						string text2 = "expected program set for symbol nil_label but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes.nil_label>.CreateUnsafe(set);
				}

				// Token: 0x06008908 RID: 35080 RVA: 0x001CE228 File Offset: 0x001CC428
				public ProgramSetBuilder<_LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x06008909 RID: 35081 RVA: 0x001CE280 File Offset: 0x001CC480
				public ProgramSetBuilder<_LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x0600890A RID: 35082 RVA: 0x001CE2D8 File Offset: 0x001CC4D8
				public ProgramSetBuilder<_LetB2> _LetB2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB2)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB2>.CreateUnsafe(set);
				}

				// Token: 0x0600890B RID: 35083 RVA: 0x001CE330 File Offset: 0x001CC530
				public ProgramSetBuilder<_LetB3> _LetB3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB3)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB3>.CreateUnsafe(set);
				}

				// Token: 0x0600890C RID: 35084 RVA: 0x001CE388 File Offset: 0x001CC588
				public ProgramSetBuilder<_LetB4> _LetB4(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB4)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB4 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes._LetB4>.CreateUnsafe(set);
				}

				// Token: 0x04003865 RID: 14437
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
