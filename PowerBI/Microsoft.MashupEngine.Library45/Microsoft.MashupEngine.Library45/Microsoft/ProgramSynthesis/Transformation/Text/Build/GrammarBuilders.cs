using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build
{
	// Token: 0x02001BD2 RID: 7122
	public class GrammarBuilders
	{
		// Token: 0x0600E91E RID: 59678 RVA: 0x0032D4FF File Offset: 0x0032B6FF
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x0600E91F RID: 59679 RVA: 0x0032D52B File Offset: 0x0032B72B
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x0600E920 RID: 59680 RVA: 0x0032D538 File Offset: 0x0032B738
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x0600E921 RID: 59681 RVA: 0x0032D545 File Offset: 0x0032B745
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x0600E922 RID: 59682 RVA: 0x0032D552 File Offset: 0x0032B752
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x0600E923 RID: 59683 RVA: 0x0032D55F File Offset: 0x0032B75F
		// (set) Token: 0x0600E924 RID: 59684 RVA: 0x0032D567 File Offset: 0x0032B767
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x0600E925 RID: 59685 RVA: 0x0032D570 File Offset: 0x0032B770
		// (set) Token: 0x0600E926 RID: 59686 RVA: 0x0032D578 File Offset: 0x0032B778
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600E927 RID: 59687 RVA: 0x0032D584 File Offset: 0x0032B784
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

		// Token: 0x04005901 RID: 22785
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04005902 RID: 22786
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04005903 RID: 22787
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04005904 RID: 22788
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04005905 RID: 22789
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02001BD3 RID: 7123
		public class GrammarSymbols
		{
			// Token: 0x170026C8 RID: 9928
			// (get) Token: 0x0600E929 RID: 59689 RVA: 0x0032D62F File Offset: 0x0032B82F
			// (set) Token: 0x0600E92A RID: 59690 RVA: 0x0032D637 File Offset: 0x0032B837
			public Symbol @switch { get; private set; }

			// Token: 0x170026C9 RID: 9929
			// (get) Token: 0x0600E92B RID: 59691 RVA: 0x0032D640 File Offset: 0x0032B840
			// (set) Token: 0x0600E92C RID: 59692 RVA: 0x0032D648 File Offset: 0x0032B848
			public Symbol ite { get; private set; }

			// Token: 0x170026CA RID: 9930
			// (get) Token: 0x0600E92D RID: 59693 RVA: 0x0032D651 File Offset: 0x0032B851
			// (set) Token: 0x0600E92E RID: 59694 RVA: 0x0032D659 File Offset: 0x0032B859
			public Symbol pred { get; private set; }

			// Token: 0x170026CB RID: 9931
			// (get) Token: 0x0600E92F RID: 59695 RVA: 0x0032D662 File Offset: 0x0032B862
			// (set) Token: 0x0600E930 RID: 59696 RVA: 0x0032D66A File Offset: 0x0032B86A
			public Symbol st { get; private set; }

			// Token: 0x170026CC RID: 9932
			// (get) Token: 0x0600E931 RID: 59697 RVA: 0x0032D673 File Offset: 0x0032B873
			// (set) Token: 0x0600E932 RID: 59698 RVA: 0x0032D67B File Offset: 0x0032B87B
			public Symbol e { get; private set; }

			// Token: 0x170026CD RID: 9933
			// (get) Token: 0x0600E933 RID: 59699 RVA: 0x0032D684 File Offset: 0x0032B884
			// (set) Token: 0x0600E934 RID: 59700 RVA: 0x0032D68C File Offset: 0x0032B88C
			public Symbol f { get; private set; }

			// Token: 0x170026CE RID: 9934
			// (get) Token: 0x0600E935 RID: 59701 RVA: 0x0032D695 File Offset: 0x0032B895
			// (set) Token: 0x0600E936 RID: 59702 RVA: 0x0032D69D File Offset: 0x0032B89D
			public Symbol columnName { get; private set; }

			// Token: 0x170026CF RID: 9935
			// (get) Token: 0x0600E937 RID: 59703 RVA: 0x0032D6A6 File Offset: 0x0032B8A6
			// (set) Token: 0x0600E938 RID: 59704 RVA: 0x0032D6AE File Offset: 0x0032B8AE
			public Symbol letOptions { get; private set; }

			// Token: 0x170026D0 RID: 9936
			// (get) Token: 0x0600E939 RID: 59705 RVA: 0x0032D6B7 File Offset: 0x0032B8B7
			// (set) Token: 0x0600E93A RID: 59706 RVA: 0x0032D6BF File Offset: 0x0032B8BF
			public Symbol cell { get; private set; }

			// Token: 0x170026D1 RID: 9937
			// (get) Token: 0x0600E93B RID: 59707 RVA: 0x0032D6C8 File Offset: 0x0032B8C8
			// (set) Token: 0x0600E93C RID: 59708 RVA: 0x0032D6D0 File Offset: 0x0032B8D0
			public Symbol x { get; private set; }

			// Token: 0x170026D2 RID: 9938
			// (get) Token: 0x0600E93D RID: 59709 RVA: 0x0032D6D9 File Offset: 0x0032B8D9
			// (set) Token: 0x0600E93E RID: 59710 RVA: 0x0032D6E1 File Offset: 0x0032B8E1
			public Symbol v { get; private set; }

			// Token: 0x170026D3 RID: 9939
			// (get) Token: 0x0600E93F RID: 59711 RVA: 0x0032D6EA File Offset: 0x0032B8EA
			// (set) Token: 0x0600E940 RID: 59712 RVA: 0x0032D6F2 File Offset: 0x0032B8F2
			public Symbol indexInputString { get; private set; }

			// Token: 0x170026D4 RID: 9940
			// (get) Token: 0x0600E941 RID: 59713 RVA: 0x0032D6FB File Offset: 0x0032B8FB
			// (set) Token: 0x0600E942 RID: 59714 RVA: 0x0032D703 File Offset: 0x0032B903
			public Symbol lookupInput { get; private set; }

			// Token: 0x170026D5 RID: 9941
			// (get) Token: 0x0600E943 RID: 59715 RVA: 0x0032D70C File Offset: 0x0032B90C
			// (set) Token: 0x0600E944 RID: 59716 RVA: 0x0032D714 File Offset: 0x0032B914
			public Symbol conv { get; private set; }

			// Token: 0x170026D6 RID: 9942
			// (get) Token: 0x0600E945 RID: 59717 RVA: 0x0032D71D File Offset: 0x0032B91D
			// (set) Token: 0x0600E946 RID: 59718 RVA: 0x0032D725 File Offset: 0x0032B925
			public Symbol sharedParsedNumber { get; private set; }

			// Token: 0x170026D7 RID: 9943
			// (get) Token: 0x0600E947 RID: 59719 RVA: 0x0032D72E File Offset: 0x0032B92E
			// (set) Token: 0x0600E948 RID: 59720 RVA: 0x0032D736 File Offset: 0x0032B936
			public Symbol sharedNumberFormat { get; private set; }

			// Token: 0x170026D8 RID: 9944
			// (get) Token: 0x0600E949 RID: 59721 RVA: 0x0032D73F File Offset: 0x0032B93F
			// (set) Token: 0x0600E94A RID: 59722 RVA: 0x0032D747 File Offset: 0x0032B947
			public Symbol sharedParsedDt { get; private set; }

			// Token: 0x170026D9 RID: 9945
			// (get) Token: 0x0600E94B RID: 59723 RVA: 0x0032D750 File Offset: 0x0032B950
			// (set) Token: 0x0600E94C RID: 59724 RVA: 0x0032D758 File Offset: 0x0032B958
			public Symbol sharedDtFormat { get; private set; }

			// Token: 0x170026DA RID: 9946
			// (get) Token: 0x0600E94D RID: 59725 RVA: 0x0032D761 File Offset: 0x0032B961
			// (set) Token: 0x0600E94E RID: 59726 RVA: 0x0032D769 File Offset: 0x0032B969
			public Symbol rangeString { get; private set; }

			// Token: 0x170026DB RID: 9947
			// (get) Token: 0x0600E94F RID: 59727 RVA: 0x0032D772 File Offset: 0x0032B972
			// (set) Token: 0x0600E950 RID: 59728 RVA: 0x0032D77A File Offset: 0x0032B97A
			public Symbol rangeSubstring { get; private set; }

			// Token: 0x170026DC RID: 9948
			// (get) Token: 0x0600E951 RID: 59729 RVA: 0x0032D783 File Offset: 0x0032B983
			// (set) Token: 0x0600E952 RID: 59730 RVA: 0x0032D78B File Offset: 0x0032B98B
			public Symbol rangeNumber { get; private set; }

			// Token: 0x170026DD RID: 9949
			// (get) Token: 0x0600E953 RID: 59731 RVA: 0x0032D794 File Offset: 0x0032B994
			// (set) Token: 0x0600E954 RID: 59732 RVA: 0x0032D79C File Offset: 0x0032B99C
			public Symbol dtRangeString { get; private set; }

			// Token: 0x170026DE RID: 9950
			// (get) Token: 0x0600E955 RID: 59733 RVA: 0x0032D7A5 File Offset: 0x0032B9A5
			// (set) Token: 0x0600E956 RID: 59734 RVA: 0x0032D7AD File Offset: 0x0032B9AD
			public Symbol dtRangeSubstring { get; private set; }

			// Token: 0x170026DF RID: 9951
			// (get) Token: 0x0600E957 RID: 59735 RVA: 0x0032D7B6 File Offset: 0x0032B9B6
			// (set) Token: 0x0600E958 RID: 59736 RVA: 0x0032D7BE File Offset: 0x0032B9BE
			public Symbol rangeDateTime { get; private set; }

			// Token: 0x170026E0 RID: 9952
			// (get) Token: 0x0600E959 RID: 59737 RVA: 0x0032D7C7 File Offset: 0x0032B9C7
			// (set) Token: 0x0600E95A RID: 59738 RVA: 0x0032D7CF File Offset: 0x0032B9CF
			public Symbol datetime { get; private set; }

			// Token: 0x170026E1 RID: 9953
			// (get) Token: 0x0600E95B RID: 59739 RVA: 0x0032D7D8 File Offset: 0x0032B9D8
			// (set) Token: 0x0600E95C RID: 59740 RVA: 0x0032D7E0 File Offset: 0x0032B9E0
			public Symbol inputDateTime { get; private set; }

			// Token: 0x170026E2 RID: 9954
			// (get) Token: 0x0600E95D RID: 59741 RVA: 0x0032D7E9 File Offset: 0x0032B9E9
			// (set) Token: 0x0600E95E RID: 59742 RVA: 0x0032D7F1 File Offset: 0x0032B9F1
			public Symbol parsedDateTime { get; private set; }

			// Token: 0x170026E3 RID: 9955
			// (get) Token: 0x0600E95F RID: 59743 RVA: 0x0032D7FA File Offset: 0x0032B9FA
			// (set) Token: 0x0600E960 RID: 59744 RVA: 0x0032D802 File Offset: 0x0032BA02
			public Symbol SS { get; private set; }

			// Token: 0x170026E4 RID: 9956
			// (get) Token: 0x0600E961 RID: 59745 RVA: 0x0032D80B File Offset: 0x0032BA0B
			// (set) Token: 0x0600E962 RID: 59746 RVA: 0x0032D813 File Offset: 0x0032BA13
			public Symbol PP { get; private set; }

			// Token: 0x170026E5 RID: 9957
			// (get) Token: 0x0600E963 RID: 59747 RVA: 0x0032D81C File Offset: 0x0032BA1C
			// (set) Token: 0x0600E964 RID: 59748 RVA: 0x0032D824 File Offset: 0x0032BA24
			public Symbol pl1 { get; private set; }

			// Token: 0x170026E6 RID: 9958
			// (get) Token: 0x0600E965 RID: 59749 RVA: 0x0032D82D File Offset: 0x0032BA2D
			// (set) Token: 0x0600E966 RID: 59750 RVA: 0x0032D835 File Offset: 0x0032BA35
			public Symbol pl2 { get; private set; }

			// Token: 0x170026E7 RID: 9959
			// (get) Token: 0x0600E967 RID: 59751 RVA: 0x0032D83E File Offset: 0x0032BA3E
			// (set) Token: 0x0600E968 RID: 59752 RVA: 0x0032D846 File Offset: 0x0032BA46
			public Symbol pl2p { get; private set; }

			// Token: 0x170026E8 RID: 9960
			// (get) Token: 0x0600E969 RID: 59753 RVA: 0x0032D84F File Offset: 0x0032BA4F
			// (set) Token: 0x0600E96A RID: 59754 RVA: 0x0032D857 File Offset: 0x0032BA57
			public Symbol pos { get; private set; }

			// Token: 0x170026E9 RID: 9961
			// (get) Token: 0x0600E96B RID: 59755 RVA: 0x0032D860 File Offset: 0x0032BA60
			// (set) Token: 0x0600E96C RID: 59756 RVA: 0x0032D868 File Offset: 0x0032BA68
			public Symbol regexPair { get; private set; }

			// Token: 0x170026EA RID: 9962
			// (get) Token: 0x0600E96D RID: 59757 RVA: 0x0032D871 File Offset: 0x0032BA71
			// (set) Token: 0x0600E96E RID: 59758 RVA: 0x0032D879 File Offset: 0x0032BA79
			public Symbol number { get; private set; }

			// Token: 0x170026EB RID: 9963
			// (get) Token: 0x0600E96F RID: 59759 RVA: 0x0032D882 File Offset: 0x0032BA82
			// (set) Token: 0x0600E970 RID: 59760 RVA: 0x0032D88A File Offset: 0x0032BA8A
			public Symbol castToNumber { get; private set; }

			// Token: 0x170026EC RID: 9964
			// (get) Token: 0x0600E971 RID: 59761 RVA: 0x0032D893 File Offset: 0x0032BA93
			// (set) Token: 0x0600E972 RID: 59762 RVA: 0x0032D89B File Offset: 0x0032BA9B
			public Symbol inputNumber { get; private set; }

			// Token: 0x170026ED RID: 9965
			// (get) Token: 0x0600E973 RID: 59763 RVA: 0x0032D8A4 File Offset: 0x0032BAA4
			// (set) Token: 0x0600E974 RID: 59764 RVA: 0x0032D8AC File Offset: 0x0032BAAC
			public Symbol parsedNumber { get; private set; }

			// Token: 0x170026EE RID: 9966
			// (get) Token: 0x0600E975 RID: 59765 RVA: 0x0032D8B5 File Offset: 0x0032BAB5
			// (set) Token: 0x0600E976 RID: 59766 RVA: 0x0032D8BD File Offset: 0x0032BABD
			public Symbol b { get; private set; }

			// Token: 0x170026EF RID: 9967
			// (get) Token: 0x0600E977 RID: 59767 RVA: 0x0032D8C6 File Offset: 0x0032BAC6
			// (set) Token: 0x0600E978 RID: 59768 RVA: 0x0032D8CE File Offset: 0x0032BACE
			public Symbol cs { get; private set; }

			// Token: 0x170026F0 RID: 9968
			// (get) Token: 0x0600E979 RID: 59769 RVA: 0x0032D8D7 File Offset: 0x0032BAD7
			// (set) Token: 0x0600E97A RID: 59770 RVA: 0x0032D8DF File Offset: 0x0032BADF
			public Symbol y { get; private set; }

			// Token: 0x170026F1 RID: 9969
			// (get) Token: 0x0600E97B RID: 59771 RVA: 0x0032D8E8 File Offset: 0x0032BAE8
			// (set) Token: 0x0600E97C RID: 59772 RVA: 0x0032D8F0 File Offset: 0x0032BAF0
			public Symbol k { get; private set; }

			// Token: 0x170026F2 RID: 9970
			// (get) Token: 0x0600E97D RID: 59773 RVA: 0x0032D8F9 File Offset: 0x0032BAF9
			// (set) Token: 0x0600E97E RID: 59774 RVA: 0x0032D901 File Offset: 0x0032BB01
			public Symbol externalExtractor { get; private set; }

			// Token: 0x170026F3 RID: 9971
			// (get) Token: 0x0600E97F RID: 59775 RVA: 0x0032D90A File Offset: 0x0032BB0A
			// (set) Token: 0x0600E980 RID: 59776 RVA: 0x0032D912 File Offset: 0x0032BB12
			public Symbol r { get; private set; }

			// Token: 0x170026F4 RID: 9972
			// (get) Token: 0x0600E981 RID: 59777 RVA: 0x0032D91B File Offset: 0x0032BB1B
			// (set) Token: 0x0600E982 RID: 59778 RVA: 0x0032D923 File Offset: 0x0032BB23
			public Symbol s { get; private set; }

			// Token: 0x170026F5 RID: 9973
			// (get) Token: 0x0600E983 RID: 59779 RVA: 0x0032D92C File Offset: 0x0032BB2C
			// (set) Token: 0x0600E984 RID: 59780 RVA: 0x0032D934 File Offset: 0x0032BB34
			public Symbol name { get; private set; }

			// Token: 0x170026F6 RID: 9974
			// (get) Token: 0x0600E985 RID: 59781 RVA: 0x0032D93D File Offset: 0x0032BB3D
			// (set) Token: 0x0600E986 RID: 59782 RVA: 0x0032D945 File Offset: 0x0032BB45
			public Symbol roundingSpec { get; private set; }

			// Token: 0x170026F7 RID: 9975
			// (get) Token: 0x0600E987 RID: 59783 RVA: 0x0032D94E File Offset: 0x0032BB4E
			// (set) Token: 0x0600E988 RID: 59784 RVA: 0x0032D956 File Offset: 0x0032BB56
			public Symbol dtRoundingSpec { get; private set; }

			// Token: 0x170026F8 RID: 9976
			// (get) Token: 0x0600E989 RID: 59785 RVA: 0x0032D95F File Offset: 0x0032BB5F
			// (set) Token: 0x0600E98A RID: 59786 RVA: 0x0032D967 File Offset: 0x0032BB67
			public Symbol minTrailingZeros { get; private set; }

			// Token: 0x170026F9 RID: 9977
			// (get) Token: 0x0600E98B RID: 59787 RVA: 0x0032D970 File Offset: 0x0032BB70
			// (set) Token: 0x0600E98C RID: 59788 RVA: 0x0032D978 File Offset: 0x0032BB78
			public Symbol maxTrailingZeros { get; private set; }

			// Token: 0x170026FA RID: 9978
			// (get) Token: 0x0600E98D RID: 59789 RVA: 0x0032D981 File Offset: 0x0032BB81
			// (set) Token: 0x0600E98E RID: 59790 RVA: 0x0032D989 File Offset: 0x0032BB89
			public Symbol minTrailingZerosAndWhitespace { get; private set; }

			// Token: 0x170026FB RID: 9979
			// (get) Token: 0x0600E98F RID: 59791 RVA: 0x0032D992 File Offset: 0x0032BB92
			// (set) Token: 0x0600E990 RID: 59792 RVA: 0x0032D99A File Offset: 0x0032BB9A
			public Symbol minLeadingZeros { get; private set; }

			// Token: 0x170026FC RID: 9980
			// (get) Token: 0x0600E991 RID: 59793 RVA: 0x0032D9A3 File Offset: 0x0032BBA3
			// (set) Token: 0x0600E992 RID: 59794 RVA: 0x0032D9AB File Offset: 0x0032BBAB
			public Symbol minLeadingZerosAndWhitespace { get; private set; }

			// Token: 0x170026FD RID: 9981
			// (get) Token: 0x0600E993 RID: 59795 RVA: 0x0032D9B4 File Offset: 0x0032BBB4
			// (set) Token: 0x0600E994 RID: 59796 RVA: 0x0032D9BC File Offset: 0x0032BBBC
			public Symbol numberFormatSeparatorChar { get; private set; }

			// Token: 0x170026FE RID: 9982
			// (get) Token: 0x0600E995 RID: 59797 RVA: 0x0032D9C5 File Offset: 0x0032BBC5
			// (set) Token: 0x0600E996 RID: 59798 RVA: 0x0032D9CD File Offset: 0x0032BBCD
			public Symbol numberFormatDetails { get; private set; }

			// Token: 0x170026FF RID: 9983
			// (get) Token: 0x0600E997 RID: 59799 RVA: 0x0032D9D6 File Offset: 0x0032BBD6
			// (set) Token: 0x0600E998 RID: 59800 RVA: 0x0032D9DE File Offset: 0x0032BBDE
			public Symbol numberFormat { get; private set; }

			// Token: 0x17002700 RID: 9984
			// (get) Token: 0x0600E999 RID: 59801 RVA: 0x0032D9E7 File Offset: 0x0032BBE7
			// (set) Token: 0x0600E99A RID: 59802 RVA: 0x0032D9EF File Offset: 0x0032BBEF
			public Symbol numberFormatLiteral { get; private set; }

			// Token: 0x17002701 RID: 9985
			// (get) Token: 0x0600E99B RID: 59803 RVA: 0x0032D9F8 File Offset: 0x0032BBF8
			// (set) Token: 0x0600E99C RID: 59804 RVA: 0x0032DA00 File Offset: 0x0032BC00
			public Symbol outputDtFormat { get; private set; }

			// Token: 0x17002702 RID: 9986
			// (get) Token: 0x0600E99D RID: 59805 RVA: 0x0032DA09 File Offset: 0x0032BC09
			// (set) Token: 0x0600E99E RID: 59806 RVA: 0x0032DA11 File Offset: 0x0032BC11
			public Symbol inputDtFormats { get; private set; }

			// Token: 0x17002703 RID: 9987
			// (get) Token: 0x0600E99F RID: 59807 RVA: 0x0032DA1A File Offset: 0x0032BC1A
			// (set) Token: 0x0600E9A0 RID: 59808 RVA: 0x0032DA22 File Offset: 0x0032BC22
			public Symbol lookupDictionary { get; private set; }

			// Token: 0x17002704 RID: 9988
			// (get) Token: 0x0600E9A1 RID: 59809 RVA: 0x0032DA2B File Offset: 0x0032BC2B
			// (set) Token: 0x0600E9A2 RID: 59810 RVA: 0x0032DA33 File Offset: 0x0032BC33
			public Symbol idx { get; private set; }

			// Token: 0x17002705 RID: 9989
			// (get) Token: 0x0600E9A3 RID: 59811 RVA: 0x0032DA3C File Offset: 0x0032BC3C
			// (set) Token: 0x0600E9A4 RID: 59812 RVA: 0x0032DA44 File Offset: 0x0032BC44
			public Symbol columnIdx { get; private set; }

			// Token: 0x17002706 RID: 9990
			// (get) Token: 0x0600E9A5 RID: 59813 RVA: 0x0032DA4D File Offset: 0x0032BC4D
			// (set) Token: 0x0600E9A6 RID: 59814 RVA: 0x0032DA55 File Offset: 0x0032BC55
			public Symbol vs { get; private set; }

			// Token: 0x17002707 RID: 9991
			// (get) Token: 0x0600E9A7 RID: 59815 RVA: 0x0032DA5E File Offset: 0x0032BC5E
			// (set) Token: 0x0600E9A8 RID: 59816 RVA: 0x0032DA66 File Offset: 0x0032BC66
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17002708 RID: 9992
			// (get) Token: 0x0600E9A9 RID: 59817 RVA: 0x0032DA6F File Offset: 0x0032BC6F
			// (set) Token: 0x0600E9AA RID: 59818 RVA: 0x0032DA77 File Offset: 0x0032BC77
			public Symbol _LetB1 { get; private set; }

			// Token: 0x17002709 RID: 9993
			// (get) Token: 0x0600E9AB RID: 59819 RVA: 0x0032DA80 File Offset: 0x0032BC80
			// (set) Token: 0x0600E9AC RID: 59820 RVA: 0x0032DA88 File Offset: 0x0032BC88
			public Symbol _LetB2 { get; private set; }

			// Token: 0x1700270A RID: 9994
			// (get) Token: 0x0600E9AD RID: 59821 RVA: 0x0032DA91 File Offset: 0x0032BC91
			// (set) Token: 0x0600E9AE RID: 59822 RVA: 0x0032DA99 File Offset: 0x0032BC99
			public Symbol _LetB3 { get; private set; }

			// Token: 0x1700270B RID: 9995
			// (get) Token: 0x0600E9AF RID: 59823 RVA: 0x0032DAA2 File Offset: 0x0032BCA2
			// (set) Token: 0x0600E9B0 RID: 59824 RVA: 0x0032DAAA File Offset: 0x0032BCAA
			public Symbol _LetB4 { get; private set; }

			// Token: 0x1700270C RID: 9996
			// (get) Token: 0x0600E9B1 RID: 59825 RVA: 0x0032DAB3 File Offset: 0x0032BCB3
			// (set) Token: 0x0600E9B2 RID: 59826 RVA: 0x0032DABB File Offset: 0x0032BCBB
			public Symbol _LetB5 { get; private set; }

			// Token: 0x1700270D RID: 9997
			// (get) Token: 0x0600E9B3 RID: 59827 RVA: 0x0032DAC4 File Offset: 0x0032BCC4
			// (set) Token: 0x0600E9B4 RID: 59828 RVA: 0x0032DACC File Offset: 0x0032BCCC
			public Symbol _LetB6 { get; private set; }

			// Token: 0x1700270E RID: 9998
			// (get) Token: 0x0600E9B5 RID: 59829 RVA: 0x0032DAD5 File Offset: 0x0032BCD5
			// (set) Token: 0x0600E9B6 RID: 59830 RVA: 0x0032DADD File Offset: 0x0032BCDD
			public Symbol _LetB7 { get; private set; }

			// Token: 0x0600E9B7 RID: 59831 RVA: 0x0032DAE8 File Offset: 0x0032BCE8
			public GrammarSymbols(Grammar grammar)
			{
				this.@switch = grammar.Symbol("switch");
				this.ite = grammar.Symbol("ite");
				this.pred = grammar.Symbol("pred");
				this.st = grammar.Symbol("st");
				this.e = grammar.Symbol("e");
				this.f = grammar.Symbol("f");
				this.columnName = grammar.Symbol("columnName");
				this.letOptions = grammar.Symbol("letOptions");
				this.cell = grammar.Symbol("cell");
				this.x = grammar.Symbol("x");
				this.v = grammar.Symbol("v");
				this.indexInputString = grammar.Symbol("indexInputString");
				this.lookupInput = grammar.Symbol("lookupInput");
				this.conv = grammar.Symbol("conv");
				this.sharedParsedNumber = grammar.Symbol("sharedParsedNumber");
				this.sharedNumberFormat = grammar.Symbol("sharedNumberFormat");
				this.sharedParsedDt = grammar.Symbol("sharedParsedDt");
				this.sharedDtFormat = grammar.Symbol("sharedDtFormat");
				this.rangeString = grammar.Symbol("rangeString");
				this.rangeSubstring = grammar.Symbol("rangeSubstring");
				this.rangeNumber = grammar.Symbol("rangeNumber");
				this.dtRangeString = grammar.Symbol("dtRangeString");
				this.dtRangeSubstring = grammar.Symbol("dtRangeSubstring");
				this.rangeDateTime = grammar.Symbol("rangeDateTime");
				this.datetime = grammar.Symbol("datetime");
				this.inputDateTime = grammar.Symbol("inputDateTime");
				this.parsedDateTime = grammar.Symbol("parsedDateTime");
				this.SS = grammar.Symbol("SS");
				this.PP = grammar.Symbol("PP");
				this.pl1 = grammar.Symbol("pl1");
				this.pl2 = grammar.Symbol("pl2");
				this.pl2p = grammar.Symbol("pl2p");
				this.pos = grammar.Symbol("pos");
				this.regexPair = grammar.Symbol("regexPair");
				this.number = grammar.Symbol("number");
				this.castToNumber = grammar.Symbol("castToNumber");
				this.inputNumber = grammar.Symbol("inputNumber");
				this.parsedNumber = grammar.Symbol("parsedNumber");
				this.b = grammar.Symbol("b");
				this.cs = grammar.Symbol("cs");
				this.y = grammar.Symbol("y");
				this.k = grammar.Symbol("k");
				this.externalExtractor = grammar.Symbol("externalExtractor");
				this.r = grammar.Symbol("r");
				this.s = grammar.Symbol("s");
				this.name = grammar.Symbol("name");
				this.roundingSpec = grammar.Symbol("roundingSpec");
				this.dtRoundingSpec = grammar.Symbol("dtRoundingSpec");
				this.minTrailingZeros = grammar.Symbol("minTrailingZeros");
				this.maxTrailingZeros = grammar.Symbol("maxTrailingZeros");
				this.minTrailingZerosAndWhitespace = grammar.Symbol("minTrailingZerosAndWhitespace");
				this.minLeadingZeros = grammar.Symbol("minLeadingZeros");
				this.minLeadingZerosAndWhitespace = grammar.Symbol("minLeadingZerosAndWhitespace");
				this.numberFormatSeparatorChar = grammar.Symbol("numberFormatSeparatorChar");
				this.numberFormatDetails = grammar.Symbol("numberFormatDetails");
				this.numberFormat = grammar.Symbol("numberFormat");
				this.numberFormatLiteral = grammar.Symbol("numberFormatLiteral");
				this.outputDtFormat = grammar.Symbol("outputDtFormat");
				this.inputDtFormats = grammar.Symbol("inputDtFormats");
				this.lookupDictionary = grammar.Symbol("lookupDictionary");
				this.idx = grammar.Symbol("idx");
				this.columnIdx = grammar.Symbol("columnIdx");
				this.vs = grammar.Symbol("vs");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
				this._LetB2 = grammar.Symbol("_LetB2");
				this._LetB3 = grammar.Symbol("_LetB3");
				this._LetB4 = grammar.Symbol("_LetB4");
				this._LetB5 = grammar.Symbol("_LetB5");
				this._LetB6 = grammar.Symbol("_LetB6");
				this._LetB7 = grammar.Symbol("_LetB7");
			}
		}

		// Token: 0x02001BD4 RID: 7124
		public class GrammarRules
		{
			// Token: 0x1700270F RID: 9999
			// (get) Token: 0x0600E9B8 RID: 59832 RVA: 0x0032DFB2 File Offset: 0x0032C1B2
			// (set) Token: 0x0600E9B9 RID: 59833 RVA: 0x0032DFBA File Offset: 0x0032C1BA
			public BlackBoxRule IfThenElse { get; private set; }

			// Token: 0x17002710 RID: 10000
			// (get) Token: 0x0600E9BA RID: 59834 RVA: 0x0032DFC3 File Offset: 0x0032C1C3
			// (set) Token: 0x0600E9BB RID: 59835 RVA: 0x0032DFCB File Offset: 0x0032C1CB
			public BlackBoxRule Concat { get; private set; }

			// Token: 0x17002711 RID: 10001
			// (get) Token: 0x0600E9BC RID: 59836 RVA: 0x0032DFD4 File Offset: 0x0032C1D4
			// (set) Token: 0x0600E9BD RID: 59837 RVA: 0x0032DFDC File Offset: 0x0032C1DC
			public BlackBoxRule ConstStr { get; private set; }

			// Token: 0x17002712 RID: 10002
			// (get) Token: 0x0600E9BE RID: 59838 RVA: 0x0032DFE5 File Offset: 0x0032C1E5
			// (set) Token: 0x0600E9BF RID: 59839 RVA: 0x0032DFED File Offset: 0x0032C1ED
			public BlackBoxRule ChooseInput { get; private set; }

			// Token: 0x17002713 RID: 10003
			// (get) Token: 0x0600E9C0 RID: 59840 RVA: 0x0032DFF6 File Offset: 0x0032C1F6
			// (set) Token: 0x0600E9C1 RID: 59841 RVA: 0x0032DFFE File Offset: 0x0032C1FE
			public BlackBoxRule IndexInputString { get; private set; }

			// Token: 0x17002714 RID: 10004
			// (get) Token: 0x0600E9C2 RID: 59842 RVA: 0x0032E007 File Offset: 0x0032C207
			// (set) Token: 0x0600E9C3 RID: 59843 RVA: 0x0032E00F File Offset: 0x0032C20F
			public BlackBoxRule LookupInput { get; private set; }

			// Token: 0x17002715 RID: 10005
			// (get) Token: 0x0600E9C4 RID: 59844 RVA: 0x0032E018 File Offset: 0x0032C218
			// (set) Token: 0x0600E9C5 RID: 59845 RVA: 0x0032E020 File Offset: 0x0032C220
			public BlackBoxRule ToLowercase { get; private set; }

			// Token: 0x17002716 RID: 10006
			// (get) Token: 0x0600E9C6 RID: 59846 RVA: 0x0032E029 File Offset: 0x0032C229
			// (set) Token: 0x0600E9C7 RID: 59847 RVA: 0x0032E031 File Offset: 0x0032C231
			public BlackBoxRule ToUppercase { get; private set; }

			// Token: 0x17002717 RID: 10007
			// (get) Token: 0x0600E9C8 RID: 59848 RVA: 0x0032E03A File Offset: 0x0032C23A
			// (set) Token: 0x0600E9C9 RID: 59849 RVA: 0x0032E042 File Offset: 0x0032C242
			public BlackBoxRule ToSimpleTitleCase { get; private set; }

			// Token: 0x17002718 RID: 10008
			// (get) Token: 0x0600E9CA RID: 59850 RVA: 0x0032E04B File Offset: 0x0032C24B
			// (set) Token: 0x0600E9CB RID: 59851 RVA: 0x0032E053 File Offset: 0x0032C253
			public BlackBoxRule FormatPartialDateTime { get; private set; }

			// Token: 0x17002719 RID: 10009
			// (get) Token: 0x0600E9CC RID: 59852 RVA: 0x0032E05C File Offset: 0x0032C25C
			// (set) Token: 0x0600E9CD RID: 59853 RVA: 0x0032E064 File Offset: 0x0032C264
			public BlackBoxRule FormatNumber { get; private set; }

			// Token: 0x1700271A RID: 10010
			// (get) Token: 0x0600E9CE RID: 59854 RVA: 0x0032E06D File Offset: 0x0032C26D
			// (set) Token: 0x0600E9CF RID: 59855 RVA: 0x0032E075 File Offset: 0x0032C275
			public BlackBoxRule Lookup { get; private set; }

			// Token: 0x1700271B RID: 10011
			// (get) Token: 0x0600E9D0 RID: 59856 RVA: 0x0032E07E File Offset: 0x0032C27E
			// (set) Token: 0x0600E9D1 RID: 59857 RVA: 0x0032E086 File Offset: 0x0032C286
			public BlackBoxRule FormatNumericRange { get; private set; }

			// Token: 0x1700271C RID: 10012
			// (get) Token: 0x0600E9D2 RID: 59858 RVA: 0x0032E08F File Offset: 0x0032C28F
			// (set) Token: 0x0600E9D3 RID: 59859 RVA: 0x0032E097 File Offset: 0x0032C297
			public BlackBoxRule FormatDateTimeRange { get; private set; }

			// Token: 0x1700271D RID: 10013
			// (get) Token: 0x0600E9D4 RID: 59860 RVA: 0x0032E0A0 File Offset: 0x0032C2A0
			// (set) Token: 0x0600E9D5 RID: 59861 RVA: 0x0032E0A8 File Offset: 0x0032C2A8
			public BlackBoxRule RangeConcat { get; private set; }

			// Token: 0x1700271E RID: 10014
			// (get) Token: 0x0600E9D6 RID: 59862 RVA: 0x0032E0B1 File Offset: 0x0032C2B1
			// (set) Token: 0x0600E9D7 RID: 59863 RVA: 0x0032E0B9 File Offset: 0x0032C2B9
			public BlackBoxRule RangeConstStr { get; private set; }

			// Token: 0x1700271F RID: 10015
			// (get) Token: 0x0600E9D8 RID: 59864 RVA: 0x0032E0C2 File Offset: 0x0032C2C2
			// (set) Token: 0x0600E9D9 RID: 59865 RVA: 0x0032E0CA File Offset: 0x0032C2CA
			public BlackBoxRule RangeFormatNumber { get; private set; }

			// Token: 0x17002720 RID: 10016
			// (get) Token: 0x0600E9DA RID: 59866 RVA: 0x0032E0D3 File Offset: 0x0032C2D3
			// (set) Token: 0x0600E9DB RID: 59867 RVA: 0x0032E0DB File Offset: 0x0032C2DB
			public BlackBoxRule RangeRoundNumber { get; private set; }

			// Token: 0x17002721 RID: 10017
			// (get) Token: 0x0600E9DC RID: 59868 RVA: 0x0032E0E4 File Offset: 0x0032C2E4
			// (set) Token: 0x0600E9DD RID: 59869 RVA: 0x0032E0EC File Offset: 0x0032C2EC
			public BlackBoxRule DtRangeConcat { get; private set; }

			// Token: 0x17002722 RID: 10018
			// (get) Token: 0x0600E9DE RID: 59870 RVA: 0x0032E0F5 File Offset: 0x0032C2F5
			// (set) Token: 0x0600E9DF RID: 59871 RVA: 0x0032E0FD File Offset: 0x0032C2FD
			public BlackBoxRule DtRangeConstStr { get; private set; }

			// Token: 0x17002723 RID: 10019
			// (get) Token: 0x0600E9E0 RID: 59872 RVA: 0x0032E106 File Offset: 0x0032C306
			// (set) Token: 0x0600E9E1 RID: 59873 RVA: 0x0032E10E File Offset: 0x0032C30E
			public BlackBoxRule RangeFormatDateTime { get; private set; }

			// Token: 0x17002724 RID: 10020
			// (get) Token: 0x0600E9E2 RID: 59874 RVA: 0x0032E117 File Offset: 0x0032C317
			// (set) Token: 0x0600E9E3 RID: 59875 RVA: 0x0032E11F File Offset: 0x0032C31F
			public BlackBoxRule RangeRoundDateTime { get; private set; }

			// Token: 0x17002725 RID: 10021
			// (get) Token: 0x0600E9E4 RID: 59876 RVA: 0x0032E128 File Offset: 0x0032C328
			// (set) Token: 0x0600E9E5 RID: 59877 RVA: 0x0032E130 File Offset: 0x0032C330
			public BlackBoxRule RoundPartialDateTime { get; private set; }

			// Token: 0x17002726 RID: 10022
			// (get) Token: 0x0600E9E6 RID: 59878 RVA: 0x0032E139 File Offset: 0x0032C339
			// (set) Token: 0x0600E9E7 RID: 59879 RVA: 0x0032E141 File Offset: 0x0032C341
			public BlackBoxRule AsPartialDateTime { get; private set; }

			// Token: 0x17002727 RID: 10023
			// (get) Token: 0x0600E9E8 RID: 59880 RVA: 0x0032E14A File Offset: 0x0032C34A
			// (set) Token: 0x0600E9E9 RID: 59881 RVA: 0x0032E152 File Offset: 0x0032C352
			public BlackBoxRule ParsePartialDateTime { get; private set; }

			// Token: 0x17002728 RID: 10024
			// (get) Token: 0x0600E9EA RID: 59882 RVA: 0x0032E15B File Offset: 0x0032C35B
			// (set) Token: 0x0600E9EB RID: 59883 RVA: 0x0032E163 File Offset: 0x0032C363
			public BlackBoxRule SubStr { get; private set; }

			// Token: 0x17002729 RID: 10025
			// (get) Token: 0x0600E9EC RID: 59884 RVA: 0x0032E16C File Offset: 0x0032C36C
			// (set) Token: 0x0600E9ED RID: 59885 RVA: 0x0032E174 File Offset: 0x0032C374
			public BlackBoxRule Add { get; private set; }

			// Token: 0x1700272A RID: 10026
			// (get) Token: 0x0600E9EE RID: 59886 RVA: 0x0032E17D File Offset: 0x0032C37D
			// (set) Token: 0x0600E9EF RID: 59887 RVA: 0x0032E185 File Offset: 0x0032C385
			public BlackBoxRule RSubStr { get; private set; }

			// Token: 0x1700272B RID: 10027
			// (get) Token: 0x0600E9F0 RID: 59888 RVA: 0x0032E18E File Offset: 0x0032C38E
			// (set) Token: 0x0600E9F1 RID: 59889 RVA: 0x0032E196 File Offset: 0x0032C396
			public BlackBoxRule RegexPositionPair { get; private set; }

			// Token: 0x1700272C RID: 10028
			// (get) Token: 0x0600E9F2 RID: 59890 RVA: 0x0032E19F File Offset: 0x0032C39F
			// (set) Token: 0x0600E9F3 RID: 59891 RVA: 0x0032E1A7 File Offset: 0x0032C3A7
			public BlackBoxRule ExternalExtractorPositionPair { get; private set; }

			// Token: 0x1700272D RID: 10029
			// (get) Token: 0x0600E9F4 RID: 59892 RVA: 0x0032E1B0 File Offset: 0x0032C3B0
			// (set) Token: 0x0600E9F5 RID: 59893 RVA: 0x0032E1B8 File Offset: 0x0032C3B8
			public BlackBoxRule RelativePosition { get; private set; }

			// Token: 0x1700272E RID: 10030
			// (get) Token: 0x0600E9F6 RID: 59894 RVA: 0x0032E1C1 File Offset: 0x0032C3C1
			// (set) Token: 0x0600E9F7 RID: 59895 RVA: 0x0032E1C9 File Offset: 0x0032C3C9
			public BlackBoxRule RegexPositionRelative { get; private set; }

			// Token: 0x1700272F RID: 10031
			// (get) Token: 0x0600E9F8 RID: 59896 RVA: 0x0032E1D2 File Offset: 0x0032C3D2
			// (set) Token: 0x0600E9F9 RID: 59897 RVA: 0x0032E1DA File Offset: 0x0032C3DA
			public BlackBoxRule AbsolutePosition { get; private set; }

			// Token: 0x17002730 RID: 10032
			// (get) Token: 0x0600E9FA RID: 59898 RVA: 0x0032E1E3 File Offset: 0x0032C3E3
			// (set) Token: 0x0600E9FB RID: 59899 RVA: 0x0032E1EB File Offset: 0x0032C3EB
			public BlackBoxRule RegexPosition { get; private set; }

			// Token: 0x17002731 RID: 10033
			// (get) Token: 0x0600E9FC RID: 59900 RVA: 0x0032E1F4 File Offset: 0x0032C3F4
			// (set) Token: 0x0600E9FD RID: 59901 RVA: 0x0032E1FC File Offset: 0x0032C3FC
			public BlackBoxRule RoundNumber { get; private set; }

			// Token: 0x17002732 RID: 10034
			// (get) Token: 0x0600E9FE RID: 59902 RVA: 0x0032E205 File Offset: 0x0032C405
			// (set) Token: 0x0600E9FF RID: 59903 RVA: 0x0032E20D File Offset: 0x0032C40D
			public BlackBoxRule AsDecimal { get; private set; }

			// Token: 0x17002733 RID: 10035
			// (get) Token: 0x0600EA00 RID: 59904 RVA: 0x0032E216 File Offset: 0x0032C416
			// (set) Token: 0x0600EA01 RID: 59905 RVA: 0x0032E21E File Offset: 0x0032C41E
			public BlackBoxRule ParseNumber { get; private set; }

			// Token: 0x17002734 RID: 10036
			// (get) Token: 0x0600EA02 RID: 59906 RVA: 0x0032E227 File Offset: 0x0032C427
			// (set) Token: 0x0600EA03 RID: 59907 RVA: 0x0032E22F File Offset: 0x0032C42F
			public BlackBoxRule SelectInput { get; private set; }

			// Token: 0x17002735 RID: 10037
			// (get) Token: 0x0600EA04 RID: 59908 RVA: 0x0032E238 File Offset: 0x0032C438
			// (set) Token: 0x0600EA05 RID: 59909 RVA: 0x0032E240 File Offset: 0x0032C440
			public BlackBoxRule BuildNumberFormat { get; private set; }

			// Token: 0x17002736 RID: 10038
			// (get) Token: 0x0600EA06 RID: 59910 RVA: 0x0032E249 File Offset: 0x0032C449
			// (set) Token: 0x0600EA07 RID: 59911 RVA: 0x0032E251 File Offset: 0x0032C451
			public ConceptRule PosPairRelative { get; private set; }

			// Token: 0x17002737 RID: 10039
			// (get) Token: 0x0600EA08 RID: 59912 RVA: 0x0032E25A File Offset: 0x0032C45A
			// (set) Token: 0x0600EA09 RID: 59913 RVA: 0x0032E262 File Offset: 0x0032C462
			public ConceptRule PosPair { get; private set; }

			// Token: 0x17002738 RID: 10040
			// (get) Token: 0x0600EA0A RID: 59914 RVA: 0x0032E26B File Offset: 0x0032C46B
			// (set) Token: 0x0600EA0B RID: 59915 RVA: 0x0032E273 File Offset: 0x0032C473
			public ConceptRule RegexPair { get; private set; }

			// Token: 0x17002739 RID: 10041
			// (get) Token: 0x0600EA0C RID: 59916 RVA: 0x0032E27C File Offset: 0x0032C47C
			// (set) Token: 0x0600EA0D RID: 59917 RVA: 0x0032E284 File Offset: 0x0032C484
			public ConversionRule SingleBranch { get; private set; }

			// Token: 0x1700273A RID: 10042
			// (get) Token: 0x0600EA0E RID: 59918 RVA: 0x0032E28D File Offset: 0x0032C48D
			// (set) Token: 0x0600EA0F RID: 59919 RVA: 0x0032E295 File Offset: 0x0032C495
			public ConversionRule Predicate { get; private set; }

			// Token: 0x1700273B RID: 10043
			// (get) Token: 0x0600EA10 RID: 59920 RVA: 0x0032E29E File Offset: 0x0032C49E
			// (set) Token: 0x0600EA11 RID: 59921 RVA: 0x0032E2A6 File Offset: 0x0032C4A6
			public ConversionRule Transformation { get; private set; }

			// Token: 0x1700273C RID: 10044
			// (get) Token: 0x0600EA12 RID: 59922 RVA: 0x0032E2AF File Offset: 0x0032C4AF
			// (set) Token: 0x0600EA13 RID: 59923 RVA: 0x0032E2B7 File Offset: 0x0032C4B7
			public ConversionRule Atom { get; private set; }

			// Token: 0x1700273D RID: 10045
			// (get) Token: 0x0600EA14 RID: 59924 RVA: 0x0032E2C0 File Offset: 0x0032C4C0
			// (set) Token: 0x0600EA15 RID: 59925 RVA: 0x0032E2C8 File Offset: 0x0032C4C8
			public ConversionRule SubString { get; private set; }

			// Token: 0x1700273E RID: 10046
			// (get) Token: 0x0600EA16 RID: 59926 RVA: 0x0032E2D1 File Offset: 0x0032C4D1
			// (set) Token: 0x0600EA17 RID: 59927 RVA: 0x0032E2D9 File Offset: 0x0032C4D9
			public ConversionRule WholeColumn { get; private set; }

			// Token: 0x1700273F RID: 10047
			// (get) Token: 0x0600EA18 RID: 59928 RVA: 0x0032E2E2 File Offset: 0x0032C4E2
			// (set) Token: 0x0600EA19 RID: 59929 RVA: 0x0032E2EA File Offset: 0x0032C4EA
			public ConversionRule SelectIndexedInput { get; private set; }

			// Token: 0x17002740 RID: 10048
			// (get) Token: 0x0600EA1A RID: 59930 RVA: 0x0032E2F3 File Offset: 0x0032C4F3
			// (set) Token: 0x0600EA1B RID: 59931 RVA: 0x0032E2FB File Offset: 0x0032C4FB
			public LetRule LetColumnName { get; private set; }

			// Token: 0x17002741 RID: 10049
			// (get) Token: 0x0600EA1C RID: 59932 RVA: 0x0032E304 File Offset: 0x0032C504
			// (set) Token: 0x0600EA1D RID: 59933 RVA: 0x0032E30C File Offset: 0x0032C50C
			public LetRule LetCell { get; private set; }

			// Token: 0x17002742 RID: 10050
			// (get) Token: 0x0600EA1E RID: 59934 RVA: 0x0032E315 File Offset: 0x0032C515
			// (set) Token: 0x0600EA1F RID: 59935 RVA: 0x0032E31D File Offset: 0x0032C51D
			public LetRule LetX { get; private set; }

			// Token: 0x17002743 RID: 10051
			// (get) Token: 0x0600EA20 RID: 59936 RVA: 0x0032E326 File Offset: 0x0032C526
			// (set) Token: 0x0600EA21 RID: 59937 RVA: 0x0032E32E File Offset: 0x0032C52E
			public LetRule LetSharedNumberFormat { get; private set; }

			// Token: 0x17002744 RID: 10052
			// (get) Token: 0x0600EA22 RID: 59938 RVA: 0x0032E337 File Offset: 0x0032C537
			// (set) Token: 0x0600EA23 RID: 59939 RVA: 0x0032E33F File Offset: 0x0032C53F
			public LetRule LetSharedDateTimeFormat { get; private set; }

			// Token: 0x17002745 RID: 10053
			// (get) Token: 0x0600EA24 RID: 59940 RVA: 0x0032E348 File Offset: 0x0032C548
			// (set) Token: 0x0600EA25 RID: 59941 RVA: 0x0032E350 File Offset: 0x0032C550
			public LetRule LetSharedParsedNumber { get; private set; }

			// Token: 0x17002746 RID: 10054
			// (get) Token: 0x0600EA26 RID: 59942 RVA: 0x0032E359 File Offset: 0x0032C559
			// (set) Token: 0x0600EA27 RID: 59943 RVA: 0x0032E361 File Offset: 0x0032C561
			public LetRule LetSharedParsedDateTime { get; private set; }

			// Token: 0x17002747 RID: 10055
			// (get) Token: 0x0600EA28 RID: 59944 RVA: 0x0032E36A File Offset: 0x0032C56A
			// (set) Token: 0x0600EA29 RID: 59945 RVA: 0x0032E372 File Offset: 0x0032C572
			public LetRule _LetB4 { get; private set; }

			// Token: 0x17002748 RID: 10056
			// (get) Token: 0x0600EA2A RID: 59946 RVA: 0x0032E37B File Offset: 0x0032C57B
			// (set) Token: 0x0600EA2B RID: 59947 RVA: 0x0032E383 File Offset: 0x0032C583
			public LetRule LetPL2 { get; private set; }

			// Token: 0x17002749 RID: 10057
			// (get) Token: 0x0600EA2C RID: 59948 RVA: 0x0032E38C File Offset: 0x0032C58C
			// (set) Token: 0x0600EA2D RID: 59949 RVA: 0x0032E394 File Offset: 0x0032C594
			public LetRule _LetB7 { get; private set; }

			// Token: 0x1700274A RID: 10058
			// (get) Token: 0x0600EA2E RID: 59950 RVA: 0x0032E39D File Offset: 0x0032C59D
			// (set) Token: 0x0600EA2F RID: 59951 RVA: 0x0032E3A5 File Offset: 0x0032C5A5
			public LetRule LetPL1 { get; private set; }

			// Token: 0x1700274B RID: 10059
			// (get) Token: 0x0600EA30 RID: 59952 RVA: 0x0032E3AE File Offset: 0x0032C5AE
			// (set) Token: 0x0600EA31 RID: 59953 RVA: 0x0032E3B6 File Offset: 0x0032C5B6
			public LetRule LetPredicate { get; private set; }

			// Token: 0x0600EA32 RID: 59954 RVA: 0x0032E3C0 File Offset: 0x0032C5C0
			public GrammarRules(Grammar grammar)
			{
				this.IfThenElse = (BlackBoxRule)grammar.Rule("IfThenElse");
				this.Concat = (BlackBoxRule)grammar.Rule("Concat");
				this.ConstStr = (BlackBoxRule)grammar.Rule("ConstStr");
				this.ChooseInput = (BlackBoxRule)grammar.Rule("ChooseInput");
				this.IndexInputString = (BlackBoxRule)grammar.Rule("IndexInputString");
				this.LookupInput = (BlackBoxRule)grammar.Rule("LookupInput");
				this.ToLowercase = (BlackBoxRule)grammar.Rule("ToLowercase");
				this.ToUppercase = (BlackBoxRule)grammar.Rule("ToUppercase");
				this.ToSimpleTitleCase = (BlackBoxRule)grammar.Rule("ToSimpleTitleCase");
				this.FormatPartialDateTime = (BlackBoxRule)grammar.Rule("FormatPartialDateTime");
				this.FormatNumber = (BlackBoxRule)grammar.Rule("FormatNumber");
				this.Lookup = (BlackBoxRule)grammar.Rule("Lookup");
				this.FormatNumericRange = (BlackBoxRule)grammar.Rule("FormatNumericRange");
				this.FormatDateTimeRange = (BlackBoxRule)grammar.Rule("FormatDateTimeRange");
				this.RangeConcat = (BlackBoxRule)grammar.Rule("RangeConcat");
				this.RangeConstStr = (BlackBoxRule)grammar.Rule("RangeConstStr");
				this.RangeFormatNumber = (BlackBoxRule)grammar.Rule("RangeFormatNumber");
				this.RangeRoundNumber = (BlackBoxRule)grammar.Rule("RangeRoundNumber");
				this.DtRangeConcat = (BlackBoxRule)grammar.Rule("DtRangeConcat");
				this.DtRangeConstStr = (BlackBoxRule)grammar.Rule("DtRangeConstStr");
				this.RangeFormatDateTime = (BlackBoxRule)grammar.Rule("RangeFormatDateTime");
				this.RangeRoundDateTime = (BlackBoxRule)grammar.Rule("RangeRoundDateTime");
				this.RoundPartialDateTime = (BlackBoxRule)grammar.Rule("RoundPartialDateTime");
				this.AsPartialDateTime = (BlackBoxRule)grammar.Rule("AsPartialDateTime");
				this.ParsePartialDateTime = (BlackBoxRule)grammar.Rule("ParsePartialDateTime");
				this.SubStr = (BlackBoxRule)grammar.Rule("SubStr");
				this.Add = (BlackBoxRule)grammar.Rule("Add");
				this.RSubStr = (BlackBoxRule)grammar.Rule("RSubStr");
				this.RegexPositionPair = (BlackBoxRule)grammar.Rule("RegexPositionPair");
				this.ExternalExtractorPositionPair = (BlackBoxRule)grammar.Rule("ExternalExtractorPositionPair");
				this.RelativePosition = (BlackBoxRule)grammar.Rule("RelativePosition");
				this.RegexPositionRelative = (BlackBoxRule)grammar.Rule("RegexPositionRelative");
				this.AbsolutePosition = (BlackBoxRule)grammar.Rule("AbsolutePosition");
				this.RegexPosition = (BlackBoxRule)grammar.Rule("RegexPosition");
				this.RoundNumber = (BlackBoxRule)grammar.Rule("RoundNumber");
				this.AsDecimal = (BlackBoxRule)grammar.Rule("AsDecimal");
				this.ParseNumber = (BlackBoxRule)grammar.Rule("ParseNumber");
				this.SelectInput = (BlackBoxRule)grammar.Rule("SelectInput");
				this.BuildNumberFormat = (BlackBoxRule)grammar.Rule("BuildNumberFormat");
				this.PosPairRelative = (ConceptRule)grammar.Rule("PosPairRelative");
				this.PosPair = (ConceptRule)grammar.Rule("PosPair");
				this.RegexPair = (ConceptRule)grammar.Rule("RegexPair");
				this.SingleBranch = (ConversionRule)grammar.Rule("SingleBranch");
				this.Predicate = (ConversionRule)grammar.Rule("Predicate");
				this.Transformation = (ConversionRule)grammar.Rule("Transformation");
				this.Atom = (ConversionRule)grammar.Rule("Atom");
				this.SubString = (ConversionRule)grammar.Rule("SubString");
				this.WholeColumn = (ConversionRule)grammar.Rule("WholeColumn");
				this.SelectIndexedInput = (ConversionRule)grammar.Rule("SelectIndexedInput");
				this.LetColumnName = (LetRule)grammar.Rule("LetColumnName");
				this.LetCell = (LetRule)grammar.Rule("LetCell");
				this.LetX = (LetRule)grammar.Rule("LetX");
				this.LetSharedNumberFormat = (LetRule)grammar.Rule("LetSharedNumberFormat");
				this.LetSharedDateTimeFormat = (LetRule)grammar.Rule("LetSharedDateTimeFormat");
				this.LetSharedParsedNumber = (LetRule)grammar.Rule("LetSharedParsedNumber");
				this.LetSharedParsedDateTime = (LetRule)grammar.Rule("LetSharedParsedDateTime");
				this._LetB4 = (LetRule)grammar.Rule("_LetB4");
				this.LetPL2 = (LetRule)grammar.Rule("LetPL2");
				this._LetB7 = (LetRule)grammar.Rule("_LetB7");
				this.LetPL1 = (LetRule)grammar.Rule("LetPL1");
				this.LetPredicate = (LetRule)grammar.Rule("LetPredicate");
			}
		}

		// Token: 0x02001BD5 RID: 7125
		public class GrammarUnnamedConversions
		{
			// Token: 0x1700274C RID: 10060
			// (get) Token: 0x0600EA33 RID: 59955 RVA: 0x0032E911 File Offset: 0x0032CB11
			// (set) Token: 0x0600EA34 RID: 59956 RVA: 0x0032E919 File Offset: 0x0032CB19
			public ConversionRule switch_ite { get; private set; }

			// Token: 0x1700274D RID: 10061
			// (get) Token: 0x0600EA35 RID: 59957 RVA: 0x0032E922 File Offset: 0x0032CB22
			// (set) Token: 0x0600EA36 RID: 59958 RVA: 0x0032E92A File Offset: 0x0032CB2A
			public ConversionRule v_indexInputString { get; private set; }

			// Token: 0x1700274E RID: 10062
			// (get) Token: 0x0600EA37 RID: 59959 RVA: 0x0032E933 File Offset: 0x0032CB33
			// (set) Token: 0x0600EA38 RID: 59960 RVA: 0x0032E93B File Offset: 0x0032CB3B
			public ConversionRule lookupInput_indexInputString { get; private set; }

			// Token: 0x1700274F RID: 10063
			// (get) Token: 0x0600EA39 RID: 59961 RVA: 0x0032E944 File Offset: 0x0032CB44
			// (set) Token: 0x0600EA3A RID: 59962 RVA: 0x0032E94C File Offset: 0x0032CB4C
			public ConversionRule rangeString_rangeSubstring { get; private set; }

			// Token: 0x17002750 RID: 10064
			// (get) Token: 0x0600EA3B RID: 59963 RVA: 0x0032E955 File Offset: 0x0032CB55
			// (set) Token: 0x0600EA3C RID: 59964 RVA: 0x0032E95D File Offset: 0x0032CB5D
			public ConversionRule dtRangeString_dtRangeSubstring { get; private set; }

			// Token: 0x17002751 RID: 10065
			// (get) Token: 0x0600EA3D RID: 59965 RVA: 0x0032E966 File Offset: 0x0032CB66
			// (set) Token: 0x0600EA3E RID: 59966 RVA: 0x0032E96E File Offset: 0x0032CB6E
			public ConversionRule datetime_inputDateTime { get; private set; }

			// Token: 0x17002752 RID: 10066
			// (get) Token: 0x0600EA3F RID: 59967 RVA: 0x0032E977 File Offset: 0x0032CB77
			// (set) Token: 0x0600EA40 RID: 59968 RVA: 0x0032E97F File Offset: 0x0032CB7F
			public ConversionRule inputDateTime_parsedDateTime { get; private set; }

			// Token: 0x17002753 RID: 10067
			// (get) Token: 0x0600EA41 RID: 59969 RVA: 0x0032E988 File Offset: 0x0032CB88
			// (set) Token: 0x0600EA42 RID: 59970 RVA: 0x0032E990 File Offset: 0x0032CB90
			public ConversionRule number_inputNumber { get; private set; }

			// Token: 0x17002754 RID: 10068
			// (get) Token: 0x0600EA43 RID: 59971 RVA: 0x0032E999 File Offset: 0x0032CB99
			// (set) Token: 0x0600EA44 RID: 59972 RVA: 0x0032E9A1 File Offset: 0x0032CBA1
			public ConversionRule inputNumber_castToNumber { get; private set; }

			// Token: 0x17002755 RID: 10069
			// (get) Token: 0x0600EA45 RID: 59973 RVA: 0x0032E9AA File Offset: 0x0032CBAA
			// (set) Token: 0x0600EA46 RID: 59974 RVA: 0x0032E9B2 File Offset: 0x0032CBB2
			public ConversionRule inputNumber_parsedNumber { get; private set; }

			// Token: 0x17002756 RID: 10070
			// (get) Token: 0x0600EA47 RID: 59975 RVA: 0x0032E9BB File Offset: 0x0032CBBB
			// (set) Token: 0x0600EA48 RID: 59976 RVA: 0x0032E9C3 File Offset: 0x0032CBC3
			public ConversionRule numberFormat_numberFormatLiteral { get; private set; }

			// Token: 0x0600EA49 RID: 59977 RVA: 0x0032E9CC File Offset: 0x0032CBCC
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.switch_ite = (ConversionRule)grammar.Rule("~convert_switch_ite");
				this.v_indexInputString = (ConversionRule)grammar.Rule("~convert_v_indexInputString");
				this.lookupInput_indexInputString = (ConversionRule)grammar.Rule("~convert_lookupInput_indexInputString");
				this.rangeString_rangeSubstring = (ConversionRule)grammar.Rule("~convert_rangeString_rangeSubstring");
				this.dtRangeString_dtRangeSubstring = (ConversionRule)grammar.Rule("~convert_dtRangeString_dtRangeSubstring");
				this.datetime_inputDateTime = (ConversionRule)grammar.Rule("~convert_datetime_inputDateTime");
				this.inputDateTime_parsedDateTime = (ConversionRule)grammar.Rule("~convert_inputDateTime_parsedDateTime");
				this.number_inputNumber = (ConversionRule)grammar.Rule("~convert_number_inputNumber");
				this.inputNumber_castToNumber = (ConversionRule)grammar.Rule("~convert_inputNumber_castToNumber");
				this.inputNumber_parsedNumber = (ConversionRule)grammar.Rule("~convert_inputNumber_parsedNumber");
				this.numberFormat_numberFormatLiteral = (ConversionRule)grammar.Rule("~convert_numberFormat_numberFormatLiteral");
			}
		}

		// Token: 0x02001BD6 RID: 7126
		public class GrammarHoles
		{
			// Token: 0x17002757 RID: 10071
			// (get) Token: 0x0600EA4A RID: 59978 RVA: 0x0032EAD1 File Offset: 0x0032CCD1
			// (set) Token: 0x0600EA4B RID: 59979 RVA: 0x0032EAD9 File Offset: 0x0032CCD9
			public Hole @switch { get; private set; }

			// Token: 0x17002758 RID: 10072
			// (get) Token: 0x0600EA4C RID: 59980 RVA: 0x0032EAE2 File Offset: 0x0032CCE2
			// (set) Token: 0x0600EA4D RID: 59981 RVA: 0x0032EAEA File Offset: 0x0032CCEA
			public Hole ite { get; private set; }

			// Token: 0x17002759 RID: 10073
			// (get) Token: 0x0600EA4E RID: 59982 RVA: 0x0032EAF3 File Offset: 0x0032CCF3
			// (set) Token: 0x0600EA4F RID: 59983 RVA: 0x0032EAFB File Offset: 0x0032CCFB
			public Hole pred { get; private set; }

			// Token: 0x1700275A RID: 10074
			// (get) Token: 0x0600EA50 RID: 59984 RVA: 0x0032EB04 File Offset: 0x0032CD04
			// (set) Token: 0x0600EA51 RID: 59985 RVA: 0x0032EB0C File Offset: 0x0032CD0C
			public Hole st { get; private set; }

			// Token: 0x1700275B RID: 10075
			// (get) Token: 0x0600EA52 RID: 59986 RVA: 0x0032EB15 File Offset: 0x0032CD15
			// (set) Token: 0x0600EA53 RID: 59987 RVA: 0x0032EB1D File Offset: 0x0032CD1D
			public Hole e { get; private set; }

			// Token: 0x1700275C RID: 10076
			// (get) Token: 0x0600EA54 RID: 59988 RVA: 0x0032EB26 File Offset: 0x0032CD26
			// (set) Token: 0x0600EA55 RID: 59989 RVA: 0x0032EB2E File Offset: 0x0032CD2E
			public Hole f { get; private set; }

			// Token: 0x1700275D RID: 10077
			// (get) Token: 0x0600EA56 RID: 59990 RVA: 0x0032EB37 File Offset: 0x0032CD37
			// (set) Token: 0x0600EA57 RID: 59991 RVA: 0x0032EB3F File Offset: 0x0032CD3F
			public Hole columnName { get; private set; }

			// Token: 0x1700275E RID: 10078
			// (get) Token: 0x0600EA58 RID: 59992 RVA: 0x0032EB48 File Offset: 0x0032CD48
			// (set) Token: 0x0600EA59 RID: 59993 RVA: 0x0032EB50 File Offset: 0x0032CD50
			public Hole letOptions { get; private set; }

			// Token: 0x1700275F RID: 10079
			// (get) Token: 0x0600EA5A RID: 59994 RVA: 0x0032EB59 File Offset: 0x0032CD59
			// (set) Token: 0x0600EA5B RID: 59995 RVA: 0x0032EB61 File Offset: 0x0032CD61
			public Hole cell { get; private set; }

			// Token: 0x17002760 RID: 10080
			// (get) Token: 0x0600EA5C RID: 59996 RVA: 0x0032EB6A File Offset: 0x0032CD6A
			// (set) Token: 0x0600EA5D RID: 59997 RVA: 0x0032EB72 File Offset: 0x0032CD72
			public Hole x { get; private set; }

			// Token: 0x17002761 RID: 10081
			// (get) Token: 0x0600EA5E RID: 59998 RVA: 0x0032EB7B File Offset: 0x0032CD7B
			// (set) Token: 0x0600EA5F RID: 59999 RVA: 0x0032EB83 File Offset: 0x0032CD83
			public Hole v { get; private set; }

			// Token: 0x17002762 RID: 10082
			// (get) Token: 0x0600EA60 RID: 60000 RVA: 0x0032EB8C File Offset: 0x0032CD8C
			// (set) Token: 0x0600EA61 RID: 60001 RVA: 0x0032EB94 File Offset: 0x0032CD94
			public Hole indexInputString { get; private set; }

			// Token: 0x17002763 RID: 10083
			// (get) Token: 0x0600EA62 RID: 60002 RVA: 0x0032EB9D File Offset: 0x0032CD9D
			// (set) Token: 0x0600EA63 RID: 60003 RVA: 0x0032EBA5 File Offset: 0x0032CDA5
			public Hole lookupInput { get; private set; }

			// Token: 0x17002764 RID: 10084
			// (get) Token: 0x0600EA64 RID: 60004 RVA: 0x0032EBAE File Offset: 0x0032CDAE
			// (set) Token: 0x0600EA65 RID: 60005 RVA: 0x0032EBB6 File Offset: 0x0032CDB6
			public Hole conv { get; private set; }

			// Token: 0x17002765 RID: 10085
			// (get) Token: 0x0600EA66 RID: 60006 RVA: 0x0032EBBF File Offset: 0x0032CDBF
			// (set) Token: 0x0600EA67 RID: 60007 RVA: 0x0032EBC7 File Offset: 0x0032CDC7
			public Hole sharedParsedNumber { get; private set; }

			// Token: 0x17002766 RID: 10086
			// (get) Token: 0x0600EA68 RID: 60008 RVA: 0x0032EBD0 File Offset: 0x0032CDD0
			// (set) Token: 0x0600EA69 RID: 60009 RVA: 0x0032EBD8 File Offset: 0x0032CDD8
			public Hole sharedNumberFormat { get; private set; }

			// Token: 0x17002767 RID: 10087
			// (get) Token: 0x0600EA6A RID: 60010 RVA: 0x0032EBE1 File Offset: 0x0032CDE1
			// (set) Token: 0x0600EA6B RID: 60011 RVA: 0x0032EBE9 File Offset: 0x0032CDE9
			public Hole sharedParsedDt { get; private set; }

			// Token: 0x17002768 RID: 10088
			// (get) Token: 0x0600EA6C RID: 60012 RVA: 0x0032EBF2 File Offset: 0x0032CDF2
			// (set) Token: 0x0600EA6D RID: 60013 RVA: 0x0032EBFA File Offset: 0x0032CDFA
			public Hole sharedDtFormat { get; private set; }

			// Token: 0x17002769 RID: 10089
			// (get) Token: 0x0600EA6E RID: 60014 RVA: 0x0032EC03 File Offset: 0x0032CE03
			// (set) Token: 0x0600EA6F RID: 60015 RVA: 0x0032EC0B File Offset: 0x0032CE0B
			public Hole rangeString { get; private set; }

			// Token: 0x1700276A RID: 10090
			// (get) Token: 0x0600EA70 RID: 60016 RVA: 0x0032EC14 File Offset: 0x0032CE14
			// (set) Token: 0x0600EA71 RID: 60017 RVA: 0x0032EC1C File Offset: 0x0032CE1C
			public Hole rangeSubstring { get; private set; }

			// Token: 0x1700276B RID: 10091
			// (get) Token: 0x0600EA72 RID: 60018 RVA: 0x0032EC25 File Offset: 0x0032CE25
			// (set) Token: 0x0600EA73 RID: 60019 RVA: 0x0032EC2D File Offset: 0x0032CE2D
			public Hole rangeNumber { get; private set; }

			// Token: 0x1700276C RID: 10092
			// (get) Token: 0x0600EA74 RID: 60020 RVA: 0x0032EC36 File Offset: 0x0032CE36
			// (set) Token: 0x0600EA75 RID: 60021 RVA: 0x0032EC3E File Offset: 0x0032CE3E
			public Hole dtRangeString { get; private set; }

			// Token: 0x1700276D RID: 10093
			// (get) Token: 0x0600EA76 RID: 60022 RVA: 0x0032EC47 File Offset: 0x0032CE47
			// (set) Token: 0x0600EA77 RID: 60023 RVA: 0x0032EC4F File Offset: 0x0032CE4F
			public Hole dtRangeSubstring { get; private set; }

			// Token: 0x1700276E RID: 10094
			// (get) Token: 0x0600EA78 RID: 60024 RVA: 0x0032EC58 File Offset: 0x0032CE58
			// (set) Token: 0x0600EA79 RID: 60025 RVA: 0x0032EC60 File Offset: 0x0032CE60
			public Hole rangeDateTime { get; private set; }

			// Token: 0x1700276F RID: 10095
			// (get) Token: 0x0600EA7A RID: 60026 RVA: 0x0032EC69 File Offset: 0x0032CE69
			// (set) Token: 0x0600EA7B RID: 60027 RVA: 0x0032EC71 File Offset: 0x0032CE71
			public Hole datetime { get; private set; }

			// Token: 0x17002770 RID: 10096
			// (get) Token: 0x0600EA7C RID: 60028 RVA: 0x0032EC7A File Offset: 0x0032CE7A
			// (set) Token: 0x0600EA7D RID: 60029 RVA: 0x0032EC82 File Offset: 0x0032CE82
			public Hole inputDateTime { get; private set; }

			// Token: 0x17002771 RID: 10097
			// (get) Token: 0x0600EA7E RID: 60030 RVA: 0x0032EC8B File Offset: 0x0032CE8B
			// (set) Token: 0x0600EA7F RID: 60031 RVA: 0x0032EC93 File Offset: 0x0032CE93
			public Hole parsedDateTime { get; private set; }

			// Token: 0x17002772 RID: 10098
			// (get) Token: 0x0600EA80 RID: 60032 RVA: 0x0032EC9C File Offset: 0x0032CE9C
			// (set) Token: 0x0600EA81 RID: 60033 RVA: 0x0032ECA4 File Offset: 0x0032CEA4
			public Hole SS { get; private set; }

			// Token: 0x17002773 RID: 10099
			// (get) Token: 0x0600EA82 RID: 60034 RVA: 0x0032ECAD File Offset: 0x0032CEAD
			// (set) Token: 0x0600EA83 RID: 60035 RVA: 0x0032ECB5 File Offset: 0x0032CEB5
			public Hole PP { get; private set; }

			// Token: 0x17002774 RID: 10100
			// (get) Token: 0x0600EA84 RID: 60036 RVA: 0x0032ECBE File Offset: 0x0032CEBE
			// (set) Token: 0x0600EA85 RID: 60037 RVA: 0x0032ECC6 File Offset: 0x0032CEC6
			public Hole pl1 { get; private set; }

			// Token: 0x17002775 RID: 10101
			// (get) Token: 0x0600EA86 RID: 60038 RVA: 0x0032ECCF File Offset: 0x0032CECF
			// (set) Token: 0x0600EA87 RID: 60039 RVA: 0x0032ECD7 File Offset: 0x0032CED7
			public Hole pl2 { get; private set; }

			// Token: 0x17002776 RID: 10102
			// (get) Token: 0x0600EA88 RID: 60040 RVA: 0x0032ECE0 File Offset: 0x0032CEE0
			// (set) Token: 0x0600EA89 RID: 60041 RVA: 0x0032ECE8 File Offset: 0x0032CEE8
			public Hole pl2p { get; private set; }

			// Token: 0x17002777 RID: 10103
			// (get) Token: 0x0600EA8A RID: 60042 RVA: 0x0032ECF1 File Offset: 0x0032CEF1
			// (set) Token: 0x0600EA8B RID: 60043 RVA: 0x0032ECF9 File Offset: 0x0032CEF9
			public Hole pos { get; private set; }

			// Token: 0x17002778 RID: 10104
			// (get) Token: 0x0600EA8C RID: 60044 RVA: 0x0032ED02 File Offset: 0x0032CF02
			// (set) Token: 0x0600EA8D RID: 60045 RVA: 0x0032ED0A File Offset: 0x0032CF0A
			public Hole regexPair { get; private set; }

			// Token: 0x17002779 RID: 10105
			// (get) Token: 0x0600EA8E RID: 60046 RVA: 0x0032ED13 File Offset: 0x0032CF13
			// (set) Token: 0x0600EA8F RID: 60047 RVA: 0x0032ED1B File Offset: 0x0032CF1B
			public Hole number { get; private set; }

			// Token: 0x1700277A RID: 10106
			// (get) Token: 0x0600EA90 RID: 60048 RVA: 0x0032ED24 File Offset: 0x0032CF24
			// (set) Token: 0x0600EA91 RID: 60049 RVA: 0x0032ED2C File Offset: 0x0032CF2C
			public Hole castToNumber { get; private set; }

			// Token: 0x1700277B RID: 10107
			// (get) Token: 0x0600EA92 RID: 60050 RVA: 0x0032ED35 File Offset: 0x0032CF35
			// (set) Token: 0x0600EA93 RID: 60051 RVA: 0x0032ED3D File Offset: 0x0032CF3D
			public Hole inputNumber { get; private set; }

			// Token: 0x1700277C RID: 10108
			// (get) Token: 0x0600EA94 RID: 60052 RVA: 0x0032ED46 File Offset: 0x0032CF46
			// (set) Token: 0x0600EA95 RID: 60053 RVA: 0x0032ED4E File Offset: 0x0032CF4E
			public Hole parsedNumber { get; private set; }

			// Token: 0x1700277D RID: 10109
			// (get) Token: 0x0600EA96 RID: 60054 RVA: 0x0032ED57 File Offset: 0x0032CF57
			// (set) Token: 0x0600EA97 RID: 60055 RVA: 0x0032ED5F File Offset: 0x0032CF5F
			public Hole b { get; private set; }

			// Token: 0x1700277E RID: 10110
			// (get) Token: 0x0600EA98 RID: 60056 RVA: 0x0032ED68 File Offset: 0x0032CF68
			// (set) Token: 0x0600EA99 RID: 60057 RVA: 0x0032ED70 File Offset: 0x0032CF70
			public Hole cs { get; private set; }

			// Token: 0x1700277F RID: 10111
			// (get) Token: 0x0600EA9A RID: 60058 RVA: 0x0032ED79 File Offset: 0x0032CF79
			// (set) Token: 0x0600EA9B RID: 60059 RVA: 0x0032ED81 File Offset: 0x0032CF81
			public Hole y { get; private set; }

			// Token: 0x17002780 RID: 10112
			// (get) Token: 0x0600EA9C RID: 60060 RVA: 0x0032ED8A File Offset: 0x0032CF8A
			// (set) Token: 0x0600EA9D RID: 60061 RVA: 0x0032ED92 File Offset: 0x0032CF92
			public Hole k { get; private set; }

			// Token: 0x17002781 RID: 10113
			// (get) Token: 0x0600EA9E RID: 60062 RVA: 0x0032ED9B File Offset: 0x0032CF9B
			// (set) Token: 0x0600EA9F RID: 60063 RVA: 0x0032EDA3 File Offset: 0x0032CFA3
			public Hole externalExtractor { get; private set; }

			// Token: 0x17002782 RID: 10114
			// (get) Token: 0x0600EAA0 RID: 60064 RVA: 0x0032EDAC File Offset: 0x0032CFAC
			// (set) Token: 0x0600EAA1 RID: 60065 RVA: 0x0032EDB4 File Offset: 0x0032CFB4
			public Hole r { get; private set; }

			// Token: 0x17002783 RID: 10115
			// (get) Token: 0x0600EAA2 RID: 60066 RVA: 0x0032EDBD File Offset: 0x0032CFBD
			// (set) Token: 0x0600EAA3 RID: 60067 RVA: 0x0032EDC5 File Offset: 0x0032CFC5
			public Hole s { get; private set; }

			// Token: 0x17002784 RID: 10116
			// (get) Token: 0x0600EAA4 RID: 60068 RVA: 0x0032EDCE File Offset: 0x0032CFCE
			// (set) Token: 0x0600EAA5 RID: 60069 RVA: 0x0032EDD6 File Offset: 0x0032CFD6
			public Hole name { get; private set; }

			// Token: 0x17002785 RID: 10117
			// (get) Token: 0x0600EAA6 RID: 60070 RVA: 0x0032EDDF File Offset: 0x0032CFDF
			// (set) Token: 0x0600EAA7 RID: 60071 RVA: 0x0032EDE7 File Offset: 0x0032CFE7
			public Hole roundingSpec { get; private set; }

			// Token: 0x17002786 RID: 10118
			// (get) Token: 0x0600EAA8 RID: 60072 RVA: 0x0032EDF0 File Offset: 0x0032CFF0
			// (set) Token: 0x0600EAA9 RID: 60073 RVA: 0x0032EDF8 File Offset: 0x0032CFF8
			public Hole dtRoundingSpec { get; private set; }

			// Token: 0x17002787 RID: 10119
			// (get) Token: 0x0600EAAA RID: 60074 RVA: 0x0032EE01 File Offset: 0x0032D001
			// (set) Token: 0x0600EAAB RID: 60075 RVA: 0x0032EE09 File Offset: 0x0032D009
			public Hole minTrailingZeros { get; private set; }

			// Token: 0x17002788 RID: 10120
			// (get) Token: 0x0600EAAC RID: 60076 RVA: 0x0032EE12 File Offset: 0x0032D012
			// (set) Token: 0x0600EAAD RID: 60077 RVA: 0x0032EE1A File Offset: 0x0032D01A
			public Hole maxTrailingZeros { get; private set; }

			// Token: 0x17002789 RID: 10121
			// (get) Token: 0x0600EAAE RID: 60078 RVA: 0x0032EE23 File Offset: 0x0032D023
			// (set) Token: 0x0600EAAF RID: 60079 RVA: 0x0032EE2B File Offset: 0x0032D02B
			public Hole minTrailingZerosAndWhitespace { get; private set; }

			// Token: 0x1700278A RID: 10122
			// (get) Token: 0x0600EAB0 RID: 60080 RVA: 0x0032EE34 File Offset: 0x0032D034
			// (set) Token: 0x0600EAB1 RID: 60081 RVA: 0x0032EE3C File Offset: 0x0032D03C
			public Hole minLeadingZeros { get; private set; }

			// Token: 0x1700278B RID: 10123
			// (get) Token: 0x0600EAB2 RID: 60082 RVA: 0x0032EE45 File Offset: 0x0032D045
			// (set) Token: 0x0600EAB3 RID: 60083 RVA: 0x0032EE4D File Offset: 0x0032D04D
			public Hole minLeadingZerosAndWhitespace { get; private set; }

			// Token: 0x1700278C RID: 10124
			// (get) Token: 0x0600EAB4 RID: 60084 RVA: 0x0032EE56 File Offset: 0x0032D056
			// (set) Token: 0x0600EAB5 RID: 60085 RVA: 0x0032EE5E File Offset: 0x0032D05E
			public Hole numberFormatSeparatorChar { get; private set; }

			// Token: 0x1700278D RID: 10125
			// (get) Token: 0x0600EAB6 RID: 60086 RVA: 0x0032EE67 File Offset: 0x0032D067
			// (set) Token: 0x0600EAB7 RID: 60087 RVA: 0x0032EE6F File Offset: 0x0032D06F
			public Hole numberFormatDetails { get; private set; }

			// Token: 0x1700278E RID: 10126
			// (get) Token: 0x0600EAB8 RID: 60088 RVA: 0x0032EE78 File Offset: 0x0032D078
			// (set) Token: 0x0600EAB9 RID: 60089 RVA: 0x0032EE80 File Offset: 0x0032D080
			public Hole numberFormat { get; private set; }

			// Token: 0x1700278F RID: 10127
			// (get) Token: 0x0600EABA RID: 60090 RVA: 0x0032EE89 File Offset: 0x0032D089
			// (set) Token: 0x0600EABB RID: 60091 RVA: 0x0032EE91 File Offset: 0x0032D091
			public Hole numberFormatLiteral { get; private set; }

			// Token: 0x17002790 RID: 10128
			// (get) Token: 0x0600EABC RID: 60092 RVA: 0x0032EE9A File Offset: 0x0032D09A
			// (set) Token: 0x0600EABD RID: 60093 RVA: 0x0032EEA2 File Offset: 0x0032D0A2
			public Hole outputDtFormat { get; private set; }

			// Token: 0x17002791 RID: 10129
			// (get) Token: 0x0600EABE RID: 60094 RVA: 0x0032EEAB File Offset: 0x0032D0AB
			// (set) Token: 0x0600EABF RID: 60095 RVA: 0x0032EEB3 File Offset: 0x0032D0B3
			public Hole inputDtFormats { get; private set; }

			// Token: 0x17002792 RID: 10130
			// (get) Token: 0x0600EAC0 RID: 60096 RVA: 0x0032EEBC File Offset: 0x0032D0BC
			// (set) Token: 0x0600EAC1 RID: 60097 RVA: 0x0032EEC4 File Offset: 0x0032D0C4
			public Hole lookupDictionary { get; private set; }

			// Token: 0x17002793 RID: 10131
			// (get) Token: 0x0600EAC2 RID: 60098 RVA: 0x0032EECD File Offset: 0x0032D0CD
			// (set) Token: 0x0600EAC3 RID: 60099 RVA: 0x0032EED5 File Offset: 0x0032D0D5
			public Hole idx { get; private set; }

			// Token: 0x17002794 RID: 10132
			// (get) Token: 0x0600EAC4 RID: 60100 RVA: 0x0032EEDE File Offset: 0x0032D0DE
			// (set) Token: 0x0600EAC5 RID: 60101 RVA: 0x0032EEE6 File Offset: 0x0032D0E6
			public Hole columnIdx { get; private set; }

			// Token: 0x17002795 RID: 10133
			// (get) Token: 0x0600EAC6 RID: 60102 RVA: 0x0032EEEF File Offset: 0x0032D0EF
			// (set) Token: 0x0600EAC7 RID: 60103 RVA: 0x0032EEF7 File Offset: 0x0032D0F7
			public Hole vs { get; private set; }

			// Token: 0x17002796 RID: 10134
			// (get) Token: 0x0600EAC8 RID: 60104 RVA: 0x0032EF00 File Offset: 0x0032D100
			// (set) Token: 0x0600EAC9 RID: 60105 RVA: 0x0032EF08 File Offset: 0x0032D108
			public Hole _LetB0 { get; private set; }

			// Token: 0x17002797 RID: 10135
			// (get) Token: 0x0600EACA RID: 60106 RVA: 0x0032EF11 File Offset: 0x0032D111
			// (set) Token: 0x0600EACB RID: 60107 RVA: 0x0032EF19 File Offset: 0x0032D119
			public Hole _LetB1 { get; private set; }

			// Token: 0x17002798 RID: 10136
			// (get) Token: 0x0600EACC RID: 60108 RVA: 0x0032EF22 File Offset: 0x0032D122
			// (set) Token: 0x0600EACD RID: 60109 RVA: 0x0032EF2A File Offset: 0x0032D12A
			public Hole _LetB2 { get; private set; }

			// Token: 0x17002799 RID: 10137
			// (get) Token: 0x0600EACE RID: 60110 RVA: 0x0032EF33 File Offset: 0x0032D133
			// (set) Token: 0x0600EACF RID: 60111 RVA: 0x0032EF3B File Offset: 0x0032D13B
			public Hole _LetB3 { get; private set; }

			// Token: 0x1700279A RID: 10138
			// (get) Token: 0x0600EAD0 RID: 60112 RVA: 0x0032EF44 File Offset: 0x0032D144
			// (set) Token: 0x0600EAD1 RID: 60113 RVA: 0x0032EF4C File Offset: 0x0032D14C
			public Hole _LetB4 { get; private set; }

			// Token: 0x1700279B RID: 10139
			// (get) Token: 0x0600EAD2 RID: 60114 RVA: 0x0032EF55 File Offset: 0x0032D155
			// (set) Token: 0x0600EAD3 RID: 60115 RVA: 0x0032EF5D File Offset: 0x0032D15D
			public Hole _LetB5 { get; private set; }

			// Token: 0x1700279C RID: 10140
			// (get) Token: 0x0600EAD4 RID: 60116 RVA: 0x0032EF66 File Offset: 0x0032D166
			// (set) Token: 0x0600EAD5 RID: 60117 RVA: 0x0032EF6E File Offset: 0x0032D16E
			public Hole _LetB6 { get; private set; }

			// Token: 0x1700279D RID: 10141
			// (get) Token: 0x0600EAD6 RID: 60118 RVA: 0x0032EF77 File Offset: 0x0032D177
			// (set) Token: 0x0600EAD7 RID: 60119 RVA: 0x0032EF7F File Offset: 0x0032D17F
			public Hole _LetB7 { get; private set; }

			// Token: 0x0600EAD8 RID: 60120 RVA: 0x0032EF88 File Offset: 0x0032D188
			public GrammarHoles(GrammarBuilders builders)
			{
				this.@switch = new Hole(builders.Symbol.@switch, null);
				this.ite = new Hole(builders.Symbol.ite, null);
				this.pred = new Hole(builders.Symbol.pred, null);
				this.st = new Hole(builders.Symbol.st, null);
				this.e = new Hole(builders.Symbol.e, null);
				this.f = new Hole(builders.Symbol.f, null);
				this.columnName = new Hole(builders.Symbol.columnName, null);
				this.letOptions = new Hole(builders.Symbol.letOptions, null);
				this.cell = new Hole(builders.Symbol.cell, null);
				this.x = new Hole(builders.Symbol.x, null);
				this.v = new Hole(builders.Symbol.v, null);
				this.indexInputString = new Hole(builders.Symbol.indexInputString, null);
				this.lookupInput = new Hole(builders.Symbol.lookupInput, null);
				this.conv = new Hole(builders.Symbol.conv, null);
				this.sharedParsedNumber = new Hole(builders.Symbol.sharedParsedNumber, null);
				this.sharedNumberFormat = new Hole(builders.Symbol.sharedNumberFormat, null);
				this.sharedParsedDt = new Hole(builders.Symbol.sharedParsedDt, null);
				this.sharedDtFormat = new Hole(builders.Symbol.sharedDtFormat, null);
				this.rangeString = new Hole(builders.Symbol.rangeString, null);
				this.rangeSubstring = new Hole(builders.Symbol.rangeSubstring, null);
				this.rangeNumber = new Hole(builders.Symbol.rangeNumber, null);
				this.dtRangeString = new Hole(builders.Symbol.dtRangeString, null);
				this.dtRangeSubstring = new Hole(builders.Symbol.dtRangeSubstring, null);
				this.rangeDateTime = new Hole(builders.Symbol.rangeDateTime, null);
				this.datetime = new Hole(builders.Symbol.datetime, null);
				this.inputDateTime = new Hole(builders.Symbol.inputDateTime, null);
				this.parsedDateTime = new Hole(builders.Symbol.parsedDateTime, null);
				this.SS = new Hole(builders.Symbol.SS, null);
				this.PP = new Hole(builders.Symbol.PP, null);
				this.pl1 = new Hole(builders.Symbol.pl1, null);
				this.pl2 = new Hole(builders.Symbol.pl2, null);
				this.pl2p = new Hole(builders.Symbol.pl2p, null);
				this.pos = new Hole(builders.Symbol.pos, null);
				this.regexPair = new Hole(builders.Symbol.regexPair, null);
				this.number = new Hole(builders.Symbol.number, null);
				this.castToNumber = new Hole(builders.Symbol.castToNumber, null);
				this.inputNumber = new Hole(builders.Symbol.inputNumber, null);
				this.parsedNumber = new Hole(builders.Symbol.parsedNumber, null);
				this.b = new Hole(builders.Symbol.b, null);
				this.cs = new Hole(builders.Symbol.cs, null);
				this.y = new Hole(builders.Symbol.y, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.externalExtractor = new Hole(builders.Symbol.externalExtractor, null);
				this.r = new Hole(builders.Symbol.r, null);
				this.s = new Hole(builders.Symbol.s, null);
				this.name = new Hole(builders.Symbol.name, null);
				this.roundingSpec = new Hole(builders.Symbol.roundingSpec, null);
				this.dtRoundingSpec = new Hole(builders.Symbol.dtRoundingSpec, null);
				this.minTrailingZeros = new Hole(builders.Symbol.minTrailingZeros, null);
				this.maxTrailingZeros = new Hole(builders.Symbol.maxTrailingZeros, null);
				this.minTrailingZerosAndWhitespace = new Hole(builders.Symbol.minTrailingZerosAndWhitespace, null);
				this.minLeadingZeros = new Hole(builders.Symbol.minLeadingZeros, null);
				this.minLeadingZerosAndWhitespace = new Hole(builders.Symbol.minLeadingZerosAndWhitespace, null);
				this.numberFormatSeparatorChar = new Hole(builders.Symbol.numberFormatSeparatorChar, null);
				this.numberFormatDetails = new Hole(builders.Symbol.numberFormatDetails, null);
				this.numberFormat = new Hole(builders.Symbol.numberFormat, null);
				this.numberFormatLiteral = new Hole(builders.Symbol.numberFormatLiteral, null);
				this.outputDtFormat = new Hole(builders.Symbol.outputDtFormat, null);
				this.inputDtFormats = new Hole(builders.Symbol.inputDtFormats, null);
				this.lookupDictionary = new Hole(builders.Symbol.lookupDictionary, null);
				this.idx = new Hole(builders.Symbol.idx, null);
				this.columnIdx = new Hole(builders.Symbol.columnIdx, null);
				this.vs = new Hole(builders.Symbol.vs, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
				this._LetB2 = new Hole(builders.Symbol._LetB2, null);
				this._LetB3 = new Hole(builders.Symbol._LetB3, null);
				this._LetB4 = new Hole(builders.Symbol._LetB4, null);
				this._LetB5 = new Hole(builders.Symbol._LetB5, null);
				this._LetB6 = new Hole(builders.Symbol._LetB6, null);
				this._LetB7 = new Hole(builders.Symbol._LetB7, null);
			}
		}

		// Token: 0x02001BD7 RID: 7127
		public class Nodes
		{
			// Token: 0x0600EAD9 RID: 60121 RVA: 0x0032F5FC File Offset: 0x0032D7FC
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

			// Token: 0x1700279E RID: 10142
			// (get) Token: 0x0600EADA RID: 60122 RVA: 0x0032F6DF File Offset: 0x0032D8DF
			// (set) Token: 0x0600EADB RID: 60123 RVA: 0x0032F6E7 File Offset: 0x0032D8E7
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x1700279F RID: 10143
			// (get) Token: 0x0600EADC RID: 60124 RVA: 0x0032F6F0 File Offset: 0x0032D8F0
			// (set) Token: 0x0600EADD RID: 60125 RVA: 0x0032F6F8 File Offset: 0x0032D8F8
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x170027A0 RID: 10144
			// (get) Token: 0x0600EADE RID: 60126 RVA: 0x0032F701 File Offset: 0x0032D901
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x170027A1 RID: 10145
			// (get) Token: 0x0600EADF RID: 60127 RVA: 0x0032F70E File Offset: 0x0032D90E
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x170027A2 RID: 10146
			// (get) Token: 0x0600EAE0 RID: 60128 RVA: 0x0032F71B File Offset: 0x0032D91B
			// (set) Token: 0x0600EAE1 RID: 60129 RVA: 0x0032F723 File Offset: 0x0032D923
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x170027A3 RID: 10147
			// (get) Token: 0x0600EAE2 RID: 60130 RVA: 0x0032F72C File Offset: 0x0032D92C
			// (set) Token: 0x0600EAE3 RID: 60131 RVA: 0x0032F734 File Offset: 0x0032D934
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x170027A4 RID: 10148
			// (get) Token: 0x0600EAE4 RID: 60132 RVA: 0x0032F73D File Offset: 0x0032D93D
			// (set) Token: 0x0600EAE5 RID: 60133 RVA: 0x0032F745 File Offset: 0x0032D945
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x170027A5 RID: 10149
			// (get) Token: 0x0600EAE6 RID: 60134 RVA: 0x0032F74E File Offset: 0x0032D94E
			// (set) Token: 0x0600EAE7 RID: 60135 RVA: 0x0032F756 File Offset: 0x0032D956
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x170027A6 RID: 10150
			// (get) Token: 0x0600EAE8 RID: 60136 RVA: 0x0032F75F File Offset: 0x0032D95F
			// (set) Token: 0x0600EAE9 RID: 60137 RVA: 0x0032F767 File Offset: 0x0032D967
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x170027A7 RID: 10151
			// (get) Token: 0x0600EAEA RID: 60138 RVA: 0x0032F770 File Offset: 0x0032D970
			// (set) Token: 0x0600EAEB RID: 60139 RVA: 0x0032F778 File Offset: 0x0032D978
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x170027A8 RID: 10152
			// (get) Token: 0x0600EAEC RID: 60140 RVA: 0x0032F781 File Offset: 0x0032D981
			// (set) Token: 0x0600EAED RID: 60141 RVA: 0x0032F789 File Offset: 0x0032D989
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x040059E0 RID: 23008
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x040059E1 RID: 23009
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001BD8 RID: 7128
			public class NodeRules
			{
				// Token: 0x0600EAEE RID: 60142 RVA: 0x0032F792 File Offset: 0x0032D992
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EAEF RID: 60143 RVA: 0x0032F7A1 File Offset: 0x0032D9A1
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k k(int value)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k(this._builders, value);
				}

				// Token: 0x0600EAF0 RID: 60144 RVA: 0x0032F7AF File Offset: 0x0032D9AF
				public externalExtractor externalExtractor(CustomExtractor value)
				{
					return new externalExtractor(this._builders, value);
				}

				// Token: 0x0600EAF1 RID: 60145 RVA: 0x0032F7BD File Offset: 0x0032D9BD
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r r(RegularExpression value)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r(this._builders, value);
				}

				// Token: 0x0600EAF2 RID: 60146 RVA: 0x0032F7CB File Offset: 0x0032D9CB
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s s(string value)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s(this._builders, value);
				}

				// Token: 0x0600EAF3 RID: 60147 RVA: 0x0032F7D9 File Offset: 0x0032D9D9
				public name name(string value)
				{
					return new name(this._builders, value);
				}

				// Token: 0x0600EAF4 RID: 60148 RVA: 0x0032F7E7 File Offset: 0x0032D9E7
				public roundingSpec roundingSpec(RoundingSpec value)
				{
					return new roundingSpec(this._builders, value);
				}

				// Token: 0x0600EAF5 RID: 60149 RVA: 0x0032F7F5 File Offset: 0x0032D9F5
				public dtRoundingSpec dtRoundingSpec(DateTimeRoundingSpec value)
				{
					return new dtRoundingSpec(this._builders, value);
				}

				// Token: 0x0600EAF6 RID: 60150 RVA: 0x0032F803 File Offset: 0x0032DA03
				public minTrailingZeros minTrailingZeros(uint? value)
				{
					return new minTrailingZeros(this._builders, value);
				}

				// Token: 0x0600EAF7 RID: 60151 RVA: 0x0032F811 File Offset: 0x0032DA11
				public maxTrailingZeros maxTrailingZeros(uint? value)
				{
					return new maxTrailingZeros(this._builders, value);
				}

				// Token: 0x0600EAF8 RID: 60152 RVA: 0x0032F81F File Offset: 0x0032DA1F
				public minTrailingZerosAndWhitespace minTrailingZerosAndWhitespace(uint? value)
				{
					return new minTrailingZerosAndWhitespace(this._builders, value);
				}

				// Token: 0x0600EAF9 RID: 60153 RVA: 0x0032F82D File Offset: 0x0032DA2D
				public minLeadingZeros minLeadingZeros(uint? value)
				{
					return new minLeadingZeros(this._builders, value);
				}

				// Token: 0x0600EAFA RID: 60154 RVA: 0x0032F83B File Offset: 0x0032DA3B
				public minLeadingZerosAndWhitespace minLeadingZerosAndWhitespace(uint? value)
				{
					return new minLeadingZerosAndWhitespace(this._builders, value);
				}

				// Token: 0x0600EAFB RID: 60155 RVA: 0x0032F849 File Offset: 0x0032DA49
				public numberFormatSeparatorChar numberFormatSeparatorChar(char? value)
				{
					return new numberFormatSeparatorChar(this._builders, value);
				}

				// Token: 0x0600EAFC RID: 60156 RVA: 0x0032F857 File Offset: 0x0032DA57
				public numberFormatDetails numberFormatDetails(NumberFormatDetails value)
				{
					return new numberFormatDetails(this._builders, value);
				}

				// Token: 0x0600EAFD RID: 60157 RVA: 0x0032F865 File Offset: 0x0032DA65
				public numberFormatLiteral numberFormatLiteral(NumberFormat value)
				{
					return new numberFormatLiteral(this._builders, value);
				}

				// Token: 0x0600EAFE RID: 60158 RVA: 0x0032F873 File Offset: 0x0032DA73
				public outputDtFormat outputDtFormat(DateTimeFormat value)
				{
					return new outputDtFormat(this._builders, value);
				}

				// Token: 0x0600EAFF RID: 60159 RVA: 0x0032F881 File Offset: 0x0032DA81
				public inputDtFormats inputDtFormats(DateTimeFormat[] value)
				{
					return new inputDtFormats(this._builders, value);
				}

				// Token: 0x0600EB00 RID: 60160 RVA: 0x0032F88F File Offset: 0x0032DA8F
				public lookupDictionary lookupDictionary(IReadOnlyDictionary<Optional<string>, string> value)
				{
					return new lookupDictionary(this._builders, value);
				}

				// Token: 0x0600EB01 RID: 60161 RVA: 0x0032F89D File Offset: 0x0032DA9D
				public idx idx(string value)
				{
					return new idx(this._builders, value);
				}

				// Token: 0x0600EB02 RID: 60162 RVA: 0x0032F8AB File Offset: 0x0032DAAB
				public columnIdx columnIdx(int value)
				{
					return new columnIdx(this._builders, value);
				}

				// Token: 0x0600EB03 RID: 60163 RVA: 0x0032F8B9 File Offset: 0x0032DAB9
				public ite IfThenElse(b value0, st value1, @switch value2)
				{
					return new IfThenElse(this._builders, value0, value1, value2);
				}

				// Token: 0x0600EB04 RID: 60164 RVA: 0x0032F8CE File Offset: 0x0032DACE
				public e Concat(f value0, e value1)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat(this._builders, value0, value1);
				}

				// Token: 0x0600EB05 RID: 60165 RVA: 0x0032F8E2 File Offset: 0x0032DAE2
				public f ConstStr(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value0)
				{
					return new ConstStr(this._builders, value0);
				}

				// Token: 0x0600EB06 RID: 60166 RVA: 0x0032F8F5 File Offset: 0x0032DAF5
				public v ChooseInput(vs value0, columnName value1)
				{
					return new ChooseInput(this._builders, value0, value1);
				}

				// Token: 0x0600EB07 RID: 60167 RVA: 0x0032F909 File Offset: 0x0032DB09
				public indexInputString IndexInputString(vs value0, columnIdx value1)
				{
					return new IndexInputString(this._builders, value0, value1);
				}

				// Token: 0x0600EB08 RID: 60168 RVA: 0x0032F91D File Offset: 0x0032DB1D
				public lookupInput LookupInput(vs value0, columnName value1)
				{
					return new LookupInput(this._builders, value0, value1);
				}

				// Token: 0x0600EB09 RID: 60169 RVA: 0x0032F931 File Offset: 0x0032DB31
				public conv ToLowercase(SS value0)
				{
					return new ToLowercase(this._builders, value0);
				}

				// Token: 0x0600EB0A RID: 60170 RVA: 0x0032F944 File Offset: 0x0032DB44
				public conv ToUppercase(SS value0)
				{
					return new ToUppercase(this._builders, value0);
				}

				// Token: 0x0600EB0B RID: 60171 RVA: 0x0032F957 File Offset: 0x0032DB57
				public conv ToSimpleTitleCase(SS value0)
				{
					return new ToSimpleTitleCase(this._builders, value0);
				}

				// Token: 0x0600EB0C RID: 60172 RVA: 0x0032F96A File Offset: 0x0032DB6A
				public conv FormatPartialDateTime(datetime value0, outputDtFormat value1)
				{
					return new FormatPartialDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB0D RID: 60173 RVA: 0x0032F97E File Offset: 0x0032DB7E
				public conv FormatNumber(number value0, numberFormat value1)
				{
					return new FormatNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB0E RID: 60174 RVA: 0x0032F992 File Offset: 0x0032DB92
				public conv Lookup(x value0, lookupDictionary value1)
				{
					return new Lookup(this._builders, value0, value1);
				}

				// Token: 0x0600EB0F RID: 60175 RVA: 0x0032F9A6 File Offset: 0x0032DBA6
				public conv FormatNumericRange(inputNumber value0, numberFormat value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value2, roundingSpec value3, roundingSpec value4)
				{
					return new FormatNumericRange(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x0600EB10 RID: 60176 RVA: 0x0032F9BF File Offset: 0x0032DBBF
				public conv FormatDateTimeRange(inputDateTime value0, outputDtFormat value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value2, dtRoundingSpec value3, dtRoundingSpec value4)
				{
					return new FormatDateTimeRange(this._builders, value0, value1, value2, value3, value4);
				}

				// Token: 0x0600EB11 RID: 60177 RVA: 0x0032F9D8 File Offset: 0x0032DBD8
				public rangeString RangeConcat(rangeSubstring value0, rangeString value1)
				{
					return new RangeConcat(this._builders, value0, value1);
				}

				// Token: 0x0600EB12 RID: 60178 RVA: 0x0032F9EC File Offset: 0x0032DBEC
				public rangeSubstring RangeConstStr(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value0)
				{
					return new RangeConstStr(this._builders, value0);
				}

				// Token: 0x0600EB13 RID: 60179 RVA: 0x0032F9FF File Offset: 0x0032DBFF
				public rangeSubstring RangeFormatNumber(rangeNumber value0, sharedNumberFormat value1)
				{
					return new RangeFormatNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB14 RID: 60180 RVA: 0x0032FA13 File Offset: 0x0032DC13
				public rangeNumber RangeRoundNumber(sharedParsedNumber value0, roundingSpec value1)
				{
					return new RangeRoundNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB15 RID: 60181 RVA: 0x0032FA27 File Offset: 0x0032DC27
				public dtRangeString DtRangeConcat(dtRangeSubstring value0, dtRangeString value1)
				{
					return new DtRangeConcat(this._builders, value0, value1);
				}

				// Token: 0x0600EB16 RID: 60182 RVA: 0x0032FA3B File Offset: 0x0032DC3B
				public dtRangeSubstring DtRangeConstStr(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value0)
				{
					return new DtRangeConstStr(this._builders, value0);
				}

				// Token: 0x0600EB17 RID: 60183 RVA: 0x0032FA4E File Offset: 0x0032DC4E
				public dtRangeSubstring RangeFormatDateTime(rangeDateTime value0, sharedDtFormat value1)
				{
					return new RangeFormatDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB18 RID: 60184 RVA: 0x0032FA62 File Offset: 0x0032DC62
				public rangeDateTime RangeRoundDateTime(sharedParsedDt value0, dtRoundingSpec value1)
				{
					return new RangeRoundDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB19 RID: 60185 RVA: 0x0032FA76 File Offset: 0x0032DC76
				public datetime RoundPartialDateTime(inputDateTime value0, dtRoundingSpec value1)
				{
					return new RoundPartialDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB1A RID: 60186 RVA: 0x0032FA8A File Offset: 0x0032DC8A
				public inputDateTime AsPartialDateTime(cell value0)
				{
					return new AsPartialDateTime(this._builders, value0);
				}

				// Token: 0x0600EB1B RID: 60187 RVA: 0x0032FA9D File Offset: 0x0032DC9D
				public parsedDateTime ParsePartialDateTime(SS value0, inputDtFormats value1)
				{
					return new ParsePartialDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB1C RID: 60188 RVA: 0x0032FAB1 File Offset: 0x0032DCB1
				public SS SubStr(x value0, PP value1)
				{
					return new SubStr(this._builders, value0, value1);
				}

				// Token: 0x0600EB1D RID: 60189 RVA: 0x0032FAC5 File Offset: 0x0032DCC5
				public _LetB2 Add(pl1 value0, pl2 value1)
				{
					return new Add(this._builders, value0, value1);
				}

				// Token: 0x0600EB1E RID: 60190 RVA: 0x0032FAD9 File Offset: 0x0032DCD9
				public _LetB5 RSubStr(x value0, pl1 value1)
				{
					return new RSubStr(this._builders, value0, value1);
				}

				// Token: 0x0600EB1F RID: 60191 RVA: 0x0032FAED File Offset: 0x0032DCED
				public PP RegexPositionPair(x value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value2)
				{
					return new RegexPositionPair(this._builders, value0, value1, value2);
				}

				// Token: 0x0600EB20 RID: 60192 RVA: 0x0032FB02 File Offset: 0x0032DD02
				public PP ExternalExtractorPositionPair(x value0, externalExtractor value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value2)
				{
					return new ExternalExtractorPositionPair(this._builders, value0, value1, value2);
				}

				// Token: 0x0600EB21 RID: 60193 RVA: 0x0032FB17 File Offset: 0x0032DD17
				public pos RelativePosition(x value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value1)
				{
					return new RelativePosition(this._builders, value0, value1);
				}

				// Token: 0x0600EB22 RID: 60194 RVA: 0x0032FB2B File Offset: 0x0032DD2B
				public pos RegexPositionRelative(x value0, regexPair value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value2)
				{
					return new RegexPositionRelative(this._builders, value0, value1, value2);
				}

				// Token: 0x0600EB23 RID: 60195 RVA: 0x0032FB40 File Offset: 0x0032DD40
				[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
				public pos AbsolutePosition(x value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value1)
				{
					return new AbsolutePosition(this._builders, value0, value1);
				}

				// Token: 0x0600EB24 RID: 60196 RVA: 0x0032FB54 File Offset: 0x0032DD54
				[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
				public pos RegexPosition(x value0, regexPair value1, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value2)
				{
					return new RegexPosition(this._builders, value0, value1, value2);
				}

				// Token: 0x0600EB25 RID: 60197 RVA: 0x0032FB69 File Offset: 0x0032DD69
				public number RoundNumber(inputNumber value0, roundingSpec value1)
				{
					return new RoundNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB26 RID: 60198 RVA: 0x0032FB7D File Offset: 0x0032DD7D
				public castToNumber AsDecimal(cell value0)
				{
					return new AsDecimal(this._builders, value0);
				}

				// Token: 0x0600EB27 RID: 60199 RVA: 0x0032FB90 File Offset: 0x0032DD90
				public parsedNumber ParseNumber(SS value0, numberFormatDetails value1)
				{
					return new ParseNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB28 RID: 60200 RVA: 0x0032FBA4 File Offset: 0x0032DDA4
				public y SelectInput(vs value0, name value1)
				{
					return new SelectInput(this._builders, value0, value1);
				}

				// Token: 0x0600EB29 RID: 60201 RVA: 0x0032FBB8 File Offset: 0x0032DDB8
				public numberFormat BuildNumberFormat(minTrailingZeros value0, maxTrailingZeros value1, minTrailingZerosAndWhitespace value2, minLeadingZeros value3, minLeadingZerosAndWhitespace value4, numberFormatDetails value5)
				{
					return new BuildNumberFormat(this._builders, value0, value1, value2, value3, value4, value5);
				}

				// Token: 0x0600EB2A RID: 60202 RVA: 0x0032FBD3 File Offset: 0x0032DDD3
				public _LetB3 PosPairRelative(pl1 value0, pl2p value1)
				{
					return new PosPairRelative(this._builders, value0, value1);
				}

				// Token: 0x0600EB2B RID: 60203 RVA: 0x0032FBE7 File Offset: 0x0032DDE7
				public PP PosPair(pos value0, pos value1)
				{
					return new PosPair(this._builders, value0, value1);
				}

				// Token: 0x0600EB2C RID: 60204 RVA: 0x0032FBFB File Offset: 0x0032DDFB
				public regexPair RegexPair(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r value1)
				{
					return new RegexPair(this._builders, value0, value1);
				}

				// Token: 0x0600EB2D RID: 60205 RVA: 0x0032FC0F File Offset: 0x0032DE0F
				public @switch SingleBranch(st value0)
				{
					return new SingleBranch(this._builders, value0);
				}

				// Token: 0x0600EB2E RID: 60206 RVA: 0x0032FC22 File Offset: 0x0032DE22
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred Predicate(conjunct value0)
				{
					return new Predicate(this._builders, value0);
				}

				// Token: 0x0600EB2F RID: 60207 RVA: 0x0032FC35 File Offset: 0x0032DE35
				public st Transformation(e value0)
				{
					return new Transformation(this._builders, value0);
				}

				// Token: 0x0600EB30 RID: 60208 RVA: 0x0032FC48 File Offset: 0x0032DE48
				public e Atom(f value0)
				{
					return new Atom(this._builders, value0);
				}

				// Token: 0x0600EB31 RID: 60209 RVA: 0x0032FC5B File Offset: 0x0032DE5B
				public conv SubString(SS value0)
				{
					return new SubString(this._builders, value0);
				}

				// Token: 0x0600EB32 RID: 60210 RVA: 0x0032FC6E File Offset: 0x0032DE6E
				public SS WholeColumn(x value0)
				{
					return new WholeColumn(this._builders, value0);
				}

				// Token: 0x0600EB33 RID: 60211 RVA: 0x0032FC81 File Offset: 0x0032DE81
				public y SelectIndexedInput(v value0)
				{
					return new SelectIndexedInput(this._builders, value0);
				}

				// Token: 0x0600EB34 RID: 60212 RVA: 0x0032FC94 File Offset: 0x0032DE94
				public f LetColumnName(idx value0, letOptions value1)
				{
					return new LetColumnName(this._builders, value0, value1);
				}

				// Token: 0x0600EB35 RID: 60213 RVA: 0x0032FCA8 File Offset: 0x0032DEA8
				public letOptions LetCell(lookupInput value0, conv value1)
				{
					return new LetCell(this._builders, value0, value1);
				}

				// Token: 0x0600EB36 RID: 60214 RVA: 0x0032FCBC File Offset: 0x0032DEBC
				public letOptions LetX(v value0, conv value1)
				{
					return new LetX(this._builders, value0, value1);
				}

				// Token: 0x0600EB37 RID: 60215 RVA: 0x0032FCD0 File Offset: 0x0032DED0
				public _LetB0 LetSharedNumberFormat(numberFormat value0, rangeString value1)
				{
					return new LetSharedNumberFormat(this._builders, value0, value1);
				}

				// Token: 0x0600EB38 RID: 60216 RVA: 0x0032FCE4 File Offset: 0x0032DEE4
				public _LetB1 LetSharedDateTimeFormat(outputDtFormat value0, dtRangeString value1)
				{
					return new LetSharedDateTimeFormat(this._builders, value0, value1);
				}

				// Token: 0x0600EB39 RID: 60217 RVA: 0x0032FCF8 File Offset: 0x0032DEF8
				public conv LetSharedParsedNumber(inputNumber value0, _LetB0 value1)
				{
					return new LetSharedParsedNumber(this._builders, value0, value1);
				}

				// Token: 0x0600EB3A RID: 60218 RVA: 0x0032FD0C File Offset: 0x0032DF0C
				public conv LetSharedParsedDateTime(inputDateTime value0, _LetB1 value1)
				{
					return new LetSharedParsedDateTime(this._builders, value0, value1);
				}

				// Token: 0x0600EB3B RID: 60219 RVA: 0x0032FD20 File Offset: 0x0032DF20
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 _LetB4(_LetB2 value0, _LetB3 value1)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4(this._builders, value0, value1);
				}

				// Token: 0x0600EB3C RID: 60220 RVA: 0x0032FD34 File Offset: 0x0032DF34
				public _LetB6 LetPL2(pos value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 value1)
				{
					return new LetPL2(this._builders, value0, value1);
				}

				// Token: 0x0600EB3D RID: 60221 RVA: 0x0032FD48 File Offset: 0x0032DF48
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 _LetB7(_LetB5 value0, _LetB6 value1)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7(this._builders, value0, value1);
				}

				// Token: 0x0600EB3E RID: 60222 RVA: 0x0032FD5C File Offset: 0x0032DF5C
				public PP LetPL1(pos value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 value1)
				{
					return new LetPL1(this._builders, value0, value1);
				}

				// Token: 0x0600EB3F RID: 60223 RVA: 0x0032FD70 File Offset: 0x0032DF70
				public b LetPredicate(y value0, Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred value1)
				{
					return new LetPredicate(this._builders, value0, value1);
				}

				// Token: 0x040059E9 RID: 23017
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BD9 RID: 7129
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0600EB40 RID: 60224 RVA: 0x0032FD84 File Offset: 0x0032DF84
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EB41 RID: 60225 RVA: 0x0032FD93 File Offset: 0x0032DF93
				public @switch switch_ite(ite value0)
				{
					return new switch_ite(this._builders, value0);
				}

				// Token: 0x0600EB42 RID: 60226 RVA: 0x0032FDA6 File Offset: 0x0032DFA6
				public v v_indexInputString(indexInputString value0)
				{
					return new v_indexInputString(this._builders, value0);
				}

				// Token: 0x0600EB43 RID: 60227 RVA: 0x0032FDB9 File Offset: 0x0032DFB9
				public lookupInput lookupInput_indexInputString(indexInputString value0)
				{
					return new lookupInput_indexInputString(this._builders, value0);
				}

				// Token: 0x0600EB44 RID: 60228 RVA: 0x0032FDCC File Offset: 0x0032DFCC
				public rangeString rangeString_rangeSubstring(rangeSubstring value0)
				{
					return new rangeString_rangeSubstring(this._builders, value0);
				}

				// Token: 0x0600EB45 RID: 60229 RVA: 0x0032FDDF File Offset: 0x0032DFDF
				public dtRangeString dtRangeString_dtRangeSubstring(dtRangeSubstring value0)
				{
					return new dtRangeString_dtRangeSubstring(this._builders, value0);
				}

				// Token: 0x0600EB46 RID: 60230 RVA: 0x0032FDF2 File Offset: 0x0032DFF2
				public datetime datetime_inputDateTime(inputDateTime value0)
				{
					return new datetime_inputDateTime(this._builders, value0);
				}

				// Token: 0x0600EB47 RID: 60231 RVA: 0x0032FE05 File Offset: 0x0032E005
				public inputDateTime inputDateTime_parsedDateTime(parsedDateTime value0)
				{
					return new inputDateTime_parsedDateTime(this._builders, value0);
				}

				// Token: 0x0600EB48 RID: 60232 RVA: 0x0032FE18 File Offset: 0x0032E018
				public number number_inputNumber(inputNumber value0)
				{
					return new number_inputNumber(this._builders, value0);
				}

				// Token: 0x0600EB49 RID: 60233 RVA: 0x0032FE2B File Offset: 0x0032E02B
				public inputNumber inputNumber_castToNumber(castToNumber value0)
				{
					return new inputNumber_castToNumber(this._builders, value0);
				}

				// Token: 0x0600EB4A RID: 60234 RVA: 0x0032FE3E File Offset: 0x0032E03E
				public inputNumber inputNumber_parsedNumber(parsedNumber value0)
				{
					return new inputNumber_parsedNumber(this._builders, value0);
				}

				// Token: 0x0600EB4B RID: 60235 RVA: 0x0032FE51 File Offset: 0x0032E051
				public numberFormat numberFormat_numberFormatLiteral(numberFormatLiteral value0)
				{
					return new numberFormat_numberFormatLiteral(this._builders, value0);
				}

				// Token: 0x040059EA RID: 23018
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BDA RID: 7130
			public class NodeVariables
			{
				// Token: 0x170027A9 RID: 10153
				// (get) Token: 0x0600EB4C RID: 60236 RVA: 0x0032FE64 File Offset: 0x0032E064
				// (set) Token: 0x0600EB4D RID: 60237 RVA: 0x0032FE6C File Offset: 0x0032E06C
				public columnName columnName { get; private set; }

				// Token: 0x170027AA RID: 10154
				// (get) Token: 0x0600EB4E RID: 60238 RVA: 0x0032FE75 File Offset: 0x0032E075
				// (set) Token: 0x0600EB4F RID: 60239 RVA: 0x0032FE7D File Offset: 0x0032E07D
				public cell cell { get; private set; }

				// Token: 0x170027AB RID: 10155
				// (get) Token: 0x0600EB50 RID: 60240 RVA: 0x0032FE86 File Offset: 0x0032E086
				// (set) Token: 0x0600EB51 RID: 60241 RVA: 0x0032FE8E File Offset: 0x0032E08E
				public x x { get; private set; }

				// Token: 0x170027AC RID: 10156
				// (get) Token: 0x0600EB52 RID: 60242 RVA: 0x0032FE97 File Offset: 0x0032E097
				// (set) Token: 0x0600EB53 RID: 60243 RVA: 0x0032FE9F File Offset: 0x0032E09F
				public sharedParsedNumber sharedParsedNumber { get; private set; }

				// Token: 0x170027AD RID: 10157
				// (get) Token: 0x0600EB54 RID: 60244 RVA: 0x0032FEA8 File Offset: 0x0032E0A8
				// (set) Token: 0x0600EB55 RID: 60245 RVA: 0x0032FEB0 File Offset: 0x0032E0B0
				public sharedNumberFormat sharedNumberFormat { get; private set; }

				// Token: 0x170027AE RID: 10158
				// (get) Token: 0x0600EB56 RID: 60246 RVA: 0x0032FEB9 File Offset: 0x0032E0B9
				// (set) Token: 0x0600EB57 RID: 60247 RVA: 0x0032FEC1 File Offset: 0x0032E0C1
				public sharedParsedDt sharedParsedDt { get; private set; }

				// Token: 0x170027AF RID: 10159
				// (get) Token: 0x0600EB58 RID: 60248 RVA: 0x0032FECA File Offset: 0x0032E0CA
				// (set) Token: 0x0600EB59 RID: 60249 RVA: 0x0032FED2 File Offset: 0x0032E0D2
				public sharedDtFormat sharedDtFormat { get; private set; }

				// Token: 0x170027B0 RID: 10160
				// (get) Token: 0x0600EB5A RID: 60250 RVA: 0x0032FEDB File Offset: 0x0032E0DB
				// (set) Token: 0x0600EB5B RID: 60251 RVA: 0x0032FEE3 File Offset: 0x0032E0E3
				public pl1 pl1 { get; private set; }

				// Token: 0x170027B1 RID: 10161
				// (get) Token: 0x0600EB5C RID: 60252 RVA: 0x0032FEEC File Offset: 0x0032E0EC
				// (set) Token: 0x0600EB5D RID: 60253 RVA: 0x0032FEF4 File Offset: 0x0032E0F4
				public pl2 pl2 { get; private set; }

				// Token: 0x170027B2 RID: 10162
				// (get) Token: 0x0600EB5E RID: 60254 RVA: 0x0032FEFD File Offset: 0x0032E0FD
				// (set) Token: 0x0600EB5F RID: 60255 RVA: 0x0032FF05 File Offset: 0x0032E105
				public pl2p pl2p { get; private set; }

				// Token: 0x170027B3 RID: 10163
				// (get) Token: 0x0600EB60 RID: 60256 RVA: 0x0032FF0E File Offset: 0x0032E10E
				// (set) Token: 0x0600EB61 RID: 60257 RVA: 0x0032FF16 File Offset: 0x0032E116
				public cs cs { get; private set; }

				// Token: 0x170027B4 RID: 10164
				// (get) Token: 0x0600EB62 RID: 60258 RVA: 0x0032FF1F File Offset: 0x0032E11F
				// (set) Token: 0x0600EB63 RID: 60259 RVA: 0x0032FF27 File Offset: 0x0032E127
				public vs vs { get; private set; }

				// Token: 0x0600EB64 RID: 60260 RVA: 0x0032FF30 File Offset: 0x0032E130
				public NodeVariables(GrammarBuilders builders)
				{
					this.columnName = new columnName(builders);
					this.cell = new cell(builders);
					this.x = new x(builders);
					this.sharedParsedNumber = new sharedParsedNumber(builders);
					this.sharedNumberFormat = new sharedNumberFormat(builders);
					this.sharedParsedDt = new sharedParsedDt(builders);
					this.sharedDtFormat = new sharedDtFormat(builders);
					this.pl1 = new pl1(builders);
					this.pl2 = new pl2(builders);
					this.pl2p = new pl2p(builders);
					this.cs = new cs(builders);
					this.vs = new vs(builders);
				}
			}

			// Token: 0x02001BDB RID: 7131
			public class NodeHoles
			{
				// Token: 0x170027B5 RID: 10165
				// (get) Token: 0x0600EB65 RID: 60261 RVA: 0x0032FFD3 File Offset: 0x0032E1D3
				// (set) Token: 0x0600EB66 RID: 60262 RVA: 0x0032FFDB File Offset: 0x0032E1DB
				public @switch @switch { get; private set; }

				// Token: 0x170027B6 RID: 10166
				// (get) Token: 0x0600EB67 RID: 60263 RVA: 0x0032FFE4 File Offset: 0x0032E1E4
				// (set) Token: 0x0600EB68 RID: 60264 RVA: 0x0032FFEC File Offset: 0x0032E1EC
				public ite ite { get; private set; }

				// Token: 0x170027B7 RID: 10167
				// (get) Token: 0x0600EB69 RID: 60265 RVA: 0x0032FFF5 File Offset: 0x0032E1F5
				// (set) Token: 0x0600EB6A RID: 60266 RVA: 0x0032FFFD File Offset: 0x0032E1FD
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred pred { get; private set; }

				// Token: 0x170027B8 RID: 10168
				// (get) Token: 0x0600EB6B RID: 60267 RVA: 0x00330006 File Offset: 0x0032E206
				// (set) Token: 0x0600EB6C RID: 60268 RVA: 0x0033000E File Offset: 0x0032E20E
				public st st { get; private set; }

				// Token: 0x170027B9 RID: 10169
				// (get) Token: 0x0600EB6D RID: 60269 RVA: 0x00330017 File Offset: 0x0032E217
				// (set) Token: 0x0600EB6E RID: 60270 RVA: 0x0033001F File Offset: 0x0032E21F
				public e e { get; private set; }

				// Token: 0x170027BA RID: 10170
				// (get) Token: 0x0600EB6F RID: 60271 RVA: 0x00330028 File Offset: 0x0032E228
				// (set) Token: 0x0600EB70 RID: 60272 RVA: 0x00330030 File Offset: 0x0032E230
				public f f { get; private set; }

				// Token: 0x170027BB RID: 10171
				// (get) Token: 0x0600EB71 RID: 60273 RVA: 0x00330039 File Offset: 0x0032E239
				// (set) Token: 0x0600EB72 RID: 60274 RVA: 0x00330041 File Offset: 0x0032E241
				public columnName columnName { get; private set; }

				// Token: 0x170027BC RID: 10172
				// (get) Token: 0x0600EB73 RID: 60275 RVA: 0x0033004A File Offset: 0x0032E24A
				// (set) Token: 0x0600EB74 RID: 60276 RVA: 0x00330052 File Offset: 0x0032E252
				public letOptions letOptions { get; private set; }

				// Token: 0x170027BD RID: 10173
				// (get) Token: 0x0600EB75 RID: 60277 RVA: 0x0033005B File Offset: 0x0032E25B
				// (set) Token: 0x0600EB76 RID: 60278 RVA: 0x00330063 File Offset: 0x0032E263
				public cell cell { get; private set; }

				// Token: 0x170027BE RID: 10174
				// (get) Token: 0x0600EB77 RID: 60279 RVA: 0x0033006C File Offset: 0x0032E26C
				// (set) Token: 0x0600EB78 RID: 60280 RVA: 0x00330074 File Offset: 0x0032E274
				public x x { get; private set; }

				// Token: 0x170027BF RID: 10175
				// (get) Token: 0x0600EB79 RID: 60281 RVA: 0x0033007D File Offset: 0x0032E27D
				// (set) Token: 0x0600EB7A RID: 60282 RVA: 0x00330085 File Offset: 0x0032E285
				public v v { get; private set; }

				// Token: 0x170027C0 RID: 10176
				// (get) Token: 0x0600EB7B RID: 60283 RVA: 0x0033008E File Offset: 0x0032E28E
				// (set) Token: 0x0600EB7C RID: 60284 RVA: 0x00330096 File Offset: 0x0032E296
				public indexInputString indexInputString { get; private set; }

				// Token: 0x170027C1 RID: 10177
				// (get) Token: 0x0600EB7D RID: 60285 RVA: 0x0033009F File Offset: 0x0032E29F
				// (set) Token: 0x0600EB7E RID: 60286 RVA: 0x003300A7 File Offset: 0x0032E2A7
				public lookupInput lookupInput { get; private set; }

				// Token: 0x170027C2 RID: 10178
				// (get) Token: 0x0600EB7F RID: 60287 RVA: 0x003300B0 File Offset: 0x0032E2B0
				// (set) Token: 0x0600EB80 RID: 60288 RVA: 0x003300B8 File Offset: 0x0032E2B8
				public conv conv { get; private set; }

				// Token: 0x170027C3 RID: 10179
				// (get) Token: 0x0600EB81 RID: 60289 RVA: 0x003300C1 File Offset: 0x0032E2C1
				// (set) Token: 0x0600EB82 RID: 60290 RVA: 0x003300C9 File Offset: 0x0032E2C9
				public sharedParsedNumber sharedParsedNumber { get; private set; }

				// Token: 0x170027C4 RID: 10180
				// (get) Token: 0x0600EB83 RID: 60291 RVA: 0x003300D2 File Offset: 0x0032E2D2
				// (set) Token: 0x0600EB84 RID: 60292 RVA: 0x003300DA File Offset: 0x0032E2DA
				public sharedNumberFormat sharedNumberFormat { get; private set; }

				// Token: 0x170027C5 RID: 10181
				// (get) Token: 0x0600EB85 RID: 60293 RVA: 0x003300E3 File Offset: 0x0032E2E3
				// (set) Token: 0x0600EB86 RID: 60294 RVA: 0x003300EB File Offset: 0x0032E2EB
				public sharedParsedDt sharedParsedDt { get; private set; }

				// Token: 0x170027C6 RID: 10182
				// (get) Token: 0x0600EB87 RID: 60295 RVA: 0x003300F4 File Offset: 0x0032E2F4
				// (set) Token: 0x0600EB88 RID: 60296 RVA: 0x003300FC File Offset: 0x0032E2FC
				public sharedDtFormat sharedDtFormat { get; private set; }

				// Token: 0x170027C7 RID: 10183
				// (get) Token: 0x0600EB89 RID: 60297 RVA: 0x00330105 File Offset: 0x0032E305
				// (set) Token: 0x0600EB8A RID: 60298 RVA: 0x0033010D File Offset: 0x0032E30D
				public rangeString rangeString { get; private set; }

				// Token: 0x170027C8 RID: 10184
				// (get) Token: 0x0600EB8B RID: 60299 RVA: 0x00330116 File Offset: 0x0032E316
				// (set) Token: 0x0600EB8C RID: 60300 RVA: 0x0033011E File Offset: 0x0032E31E
				public rangeSubstring rangeSubstring { get; private set; }

				// Token: 0x170027C9 RID: 10185
				// (get) Token: 0x0600EB8D RID: 60301 RVA: 0x00330127 File Offset: 0x0032E327
				// (set) Token: 0x0600EB8E RID: 60302 RVA: 0x0033012F File Offset: 0x0032E32F
				public rangeNumber rangeNumber { get; private set; }

				// Token: 0x170027CA RID: 10186
				// (get) Token: 0x0600EB8F RID: 60303 RVA: 0x00330138 File Offset: 0x0032E338
				// (set) Token: 0x0600EB90 RID: 60304 RVA: 0x00330140 File Offset: 0x0032E340
				public dtRangeString dtRangeString { get; private set; }

				// Token: 0x170027CB RID: 10187
				// (get) Token: 0x0600EB91 RID: 60305 RVA: 0x00330149 File Offset: 0x0032E349
				// (set) Token: 0x0600EB92 RID: 60306 RVA: 0x00330151 File Offset: 0x0032E351
				public dtRangeSubstring dtRangeSubstring { get; private set; }

				// Token: 0x170027CC RID: 10188
				// (get) Token: 0x0600EB93 RID: 60307 RVA: 0x0033015A File Offset: 0x0032E35A
				// (set) Token: 0x0600EB94 RID: 60308 RVA: 0x00330162 File Offset: 0x0032E362
				public rangeDateTime rangeDateTime { get; private set; }

				// Token: 0x170027CD RID: 10189
				// (get) Token: 0x0600EB95 RID: 60309 RVA: 0x0033016B File Offset: 0x0032E36B
				// (set) Token: 0x0600EB96 RID: 60310 RVA: 0x00330173 File Offset: 0x0032E373
				public datetime datetime { get; private set; }

				// Token: 0x170027CE RID: 10190
				// (get) Token: 0x0600EB97 RID: 60311 RVA: 0x0033017C File Offset: 0x0032E37C
				// (set) Token: 0x0600EB98 RID: 60312 RVA: 0x00330184 File Offset: 0x0032E384
				public inputDateTime inputDateTime { get; private set; }

				// Token: 0x170027CF RID: 10191
				// (get) Token: 0x0600EB99 RID: 60313 RVA: 0x0033018D File Offset: 0x0032E38D
				// (set) Token: 0x0600EB9A RID: 60314 RVA: 0x00330195 File Offset: 0x0032E395
				public parsedDateTime parsedDateTime { get; private set; }

				// Token: 0x170027D0 RID: 10192
				// (get) Token: 0x0600EB9B RID: 60315 RVA: 0x0033019E File Offset: 0x0032E39E
				// (set) Token: 0x0600EB9C RID: 60316 RVA: 0x003301A6 File Offset: 0x0032E3A6
				public SS SS { get; private set; }

				// Token: 0x170027D1 RID: 10193
				// (get) Token: 0x0600EB9D RID: 60317 RVA: 0x003301AF File Offset: 0x0032E3AF
				// (set) Token: 0x0600EB9E RID: 60318 RVA: 0x003301B7 File Offset: 0x0032E3B7
				public PP PP { get; private set; }

				// Token: 0x170027D2 RID: 10194
				// (get) Token: 0x0600EB9F RID: 60319 RVA: 0x003301C0 File Offset: 0x0032E3C0
				// (set) Token: 0x0600EBA0 RID: 60320 RVA: 0x003301C8 File Offset: 0x0032E3C8
				public pl1 pl1 { get; private set; }

				// Token: 0x170027D3 RID: 10195
				// (get) Token: 0x0600EBA1 RID: 60321 RVA: 0x003301D1 File Offset: 0x0032E3D1
				// (set) Token: 0x0600EBA2 RID: 60322 RVA: 0x003301D9 File Offset: 0x0032E3D9
				public pl2 pl2 { get; private set; }

				// Token: 0x170027D4 RID: 10196
				// (get) Token: 0x0600EBA3 RID: 60323 RVA: 0x003301E2 File Offset: 0x0032E3E2
				// (set) Token: 0x0600EBA4 RID: 60324 RVA: 0x003301EA File Offset: 0x0032E3EA
				public pl2p pl2p { get; private set; }

				// Token: 0x170027D5 RID: 10197
				// (get) Token: 0x0600EBA5 RID: 60325 RVA: 0x003301F3 File Offset: 0x0032E3F3
				// (set) Token: 0x0600EBA6 RID: 60326 RVA: 0x003301FB File Offset: 0x0032E3FB
				public pos pos { get; private set; }

				// Token: 0x170027D6 RID: 10198
				// (get) Token: 0x0600EBA7 RID: 60327 RVA: 0x00330204 File Offset: 0x0032E404
				// (set) Token: 0x0600EBA8 RID: 60328 RVA: 0x0033020C File Offset: 0x0032E40C
				public regexPair regexPair { get; private set; }

				// Token: 0x170027D7 RID: 10199
				// (get) Token: 0x0600EBA9 RID: 60329 RVA: 0x00330215 File Offset: 0x0032E415
				// (set) Token: 0x0600EBAA RID: 60330 RVA: 0x0033021D File Offset: 0x0032E41D
				public number number { get; private set; }

				// Token: 0x170027D8 RID: 10200
				// (get) Token: 0x0600EBAB RID: 60331 RVA: 0x00330226 File Offset: 0x0032E426
				// (set) Token: 0x0600EBAC RID: 60332 RVA: 0x0033022E File Offset: 0x0032E42E
				public castToNumber castToNumber { get; private set; }

				// Token: 0x170027D9 RID: 10201
				// (get) Token: 0x0600EBAD RID: 60333 RVA: 0x00330237 File Offset: 0x0032E437
				// (set) Token: 0x0600EBAE RID: 60334 RVA: 0x0033023F File Offset: 0x0032E43F
				public inputNumber inputNumber { get; private set; }

				// Token: 0x170027DA RID: 10202
				// (get) Token: 0x0600EBAF RID: 60335 RVA: 0x00330248 File Offset: 0x0032E448
				// (set) Token: 0x0600EBB0 RID: 60336 RVA: 0x00330250 File Offset: 0x0032E450
				public parsedNumber parsedNumber { get; private set; }

				// Token: 0x170027DB RID: 10203
				// (get) Token: 0x0600EBB1 RID: 60337 RVA: 0x00330259 File Offset: 0x0032E459
				// (set) Token: 0x0600EBB2 RID: 60338 RVA: 0x00330261 File Offset: 0x0032E461
				public b b { get; private set; }

				// Token: 0x170027DC RID: 10204
				// (get) Token: 0x0600EBB3 RID: 60339 RVA: 0x0033026A File Offset: 0x0032E46A
				// (set) Token: 0x0600EBB4 RID: 60340 RVA: 0x00330272 File Offset: 0x0032E472
				public cs cs { get; private set; }

				// Token: 0x170027DD RID: 10205
				// (get) Token: 0x0600EBB5 RID: 60341 RVA: 0x0033027B File Offset: 0x0032E47B
				// (set) Token: 0x0600EBB6 RID: 60342 RVA: 0x00330283 File Offset: 0x0032E483
				public y y { get; private set; }

				// Token: 0x170027DE RID: 10206
				// (get) Token: 0x0600EBB7 RID: 60343 RVA: 0x0033028C File Offset: 0x0032E48C
				// (set) Token: 0x0600EBB8 RID: 60344 RVA: 0x00330294 File Offset: 0x0032E494
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k k { get; private set; }

				// Token: 0x170027DF RID: 10207
				// (get) Token: 0x0600EBB9 RID: 60345 RVA: 0x0033029D File Offset: 0x0032E49D
				// (set) Token: 0x0600EBBA RID: 60346 RVA: 0x003302A5 File Offset: 0x0032E4A5
				public externalExtractor externalExtractor { get; private set; }

				// Token: 0x170027E0 RID: 10208
				// (get) Token: 0x0600EBBB RID: 60347 RVA: 0x003302AE File Offset: 0x0032E4AE
				// (set) Token: 0x0600EBBC RID: 60348 RVA: 0x003302B6 File Offset: 0x0032E4B6
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r r { get; private set; }

				// Token: 0x170027E1 RID: 10209
				// (get) Token: 0x0600EBBD RID: 60349 RVA: 0x003302BF File Offset: 0x0032E4BF
				// (set) Token: 0x0600EBBE RID: 60350 RVA: 0x003302C7 File Offset: 0x0032E4C7
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s s { get; private set; }

				// Token: 0x170027E2 RID: 10210
				// (get) Token: 0x0600EBBF RID: 60351 RVA: 0x003302D0 File Offset: 0x0032E4D0
				// (set) Token: 0x0600EBC0 RID: 60352 RVA: 0x003302D8 File Offset: 0x0032E4D8
				public name name { get; private set; }

				// Token: 0x170027E3 RID: 10211
				// (get) Token: 0x0600EBC1 RID: 60353 RVA: 0x003302E1 File Offset: 0x0032E4E1
				// (set) Token: 0x0600EBC2 RID: 60354 RVA: 0x003302E9 File Offset: 0x0032E4E9
				public roundingSpec roundingSpec { get; private set; }

				// Token: 0x170027E4 RID: 10212
				// (get) Token: 0x0600EBC3 RID: 60355 RVA: 0x003302F2 File Offset: 0x0032E4F2
				// (set) Token: 0x0600EBC4 RID: 60356 RVA: 0x003302FA File Offset: 0x0032E4FA
				public dtRoundingSpec dtRoundingSpec { get; private set; }

				// Token: 0x170027E5 RID: 10213
				// (get) Token: 0x0600EBC5 RID: 60357 RVA: 0x00330303 File Offset: 0x0032E503
				// (set) Token: 0x0600EBC6 RID: 60358 RVA: 0x0033030B File Offset: 0x0032E50B
				public minTrailingZeros minTrailingZeros { get; private set; }

				// Token: 0x170027E6 RID: 10214
				// (get) Token: 0x0600EBC7 RID: 60359 RVA: 0x00330314 File Offset: 0x0032E514
				// (set) Token: 0x0600EBC8 RID: 60360 RVA: 0x0033031C File Offset: 0x0032E51C
				public maxTrailingZeros maxTrailingZeros { get; private set; }

				// Token: 0x170027E7 RID: 10215
				// (get) Token: 0x0600EBC9 RID: 60361 RVA: 0x00330325 File Offset: 0x0032E525
				// (set) Token: 0x0600EBCA RID: 60362 RVA: 0x0033032D File Offset: 0x0032E52D
				public minTrailingZerosAndWhitespace minTrailingZerosAndWhitespace { get; private set; }

				// Token: 0x170027E8 RID: 10216
				// (get) Token: 0x0600EBCB RID: 60363 RVA: 0x00330336 File Offset: 0x0032E536
				// (set) Token: 0x0600EBCC RID: 60364 RVA: 0x0033033E File Offset: 0x0032E53E
				public minLeadingZeros minLeadingZeros { get; private set; }

				// Token: 0x170027E9 RID: 10217
				// (get) Token: 0x0600EBCD RID: 60365 RVA: 0x00330347 File Offset: 0x0032E547
				// (set) Token: 0x0600EBCE RID: 60366 RVA: 0x0033034F File Offset: 0x0032E54F
				public minLeadingZerosAndWhitespace minLeadingZerosAndWhitespace { get; private set; }

				// Token: 0x170027EA RID: 10218
				// (get) Token: 0x0600EBCF RID: 60367 RVA: 0x00330358 File Offset: 0x0032E558
				// (set) Token: 0x0600EBD0 RID: 60368 RVA: 0x00330360 File Offset: 0x0032E560
				public numberFormatSeparatorChar numberFormatSeparatorChar { get; private set; }

				// Token: 0x170027EB RID: 10219
				// (get) Token: 0x0600EBD1 RID: 60369 RVA: 0x00330369 File Offset: 0x0032E569
				// (set) Token: 0x0600EBD2 RID: 60370 RVA: 0x00330371 File Offset: 0x0032E571
				public numberFormatDetails numberFormatDetails { get; private set; }

				// Token: 0x170027EC RID: 10220
				// (get) Token: 0x0600EBD3 RID: 60371 RVA: 0x0033037A File Offset: 0x0032E57A
				// (set) Token: 0x0600EBD4 RID: 60372 RVA: 0x00330382 File Offset: 0x0032E582
				public numberFormat numberFormat { get; private set; }

				// Token: 0x170027ED RID: 10221
				// (get) Token: 0x0600EBD5 RID: 60373 RVA: 0x0033038B File Offset: 0x0032E58B
				// (set) Token: 0x0600EBD6 RID: 60374 RVA: 0x00330393 File Offset: 0x0032E593
				public numberFormatLiteral numberFormatLiteral { get; private set; }

				// Token: 0x170027EE RID: 10222
				// (get) Token: 0x0600EBD7 RID: 60375 RVA: 0x0033039C File Offset: 0x0032E59C
				// (set) Token: 0x0600EBD8 RID: 60376 RVA: 0x003303A4 File Offset: 0x0032E5A4
				public outputDtFormat outputDtFormat { get; private set; }

				// Token: 0x170027EF RID: 10223
				// (get) Token: 0x0600EBD9 RID: 60377 RVA: 0x003303AD File Offset: 0x0032E5AD
				// (set) Token: 0x0600EBDA RID: 60378 RVA: 0x003303B5 File Offset: 0x0032E5B5
				public inputDtFormats inputDtFormats { get; private set; }

				// Token: 0x170027F0 RID: 10224
				// (get) Token: 0x0600EBDB RID: 60379 RVA: 0x003303BE File Offset: 0x0032E5BE
				// (set) Token: 0x0600EBDC RID: 60380 RVA: 0x003303C6 File Offset: 0x0032E5C6
				public lookupDictionary lookupDictionary { get; private set; }

				// Token: 0x170027F1 RID: 10225
				// (get) Token: 0x0600EBDD RID: 60381 RVA: 0x003303CF File Offset: 0x0032E5CF
				// (set) Token: 0x0600EBDE RID: 60382 RVA: 0x003303D7 File Offset: 0x0032E5D7
				public idx idx { get; private set; }

				// Token: 0x170027F2 RID: 10226
				// (get) Token: 0x0600EBDF RID: 60383 RVA: 0x003303E0 File Offset: 0x0032E5E0
				// (set) Token: 0x0600EBE0 RID: 60384 RVA: 0x003303E8 File Offset: 0x0032E5E8
				public columnIdx columnIdx { get; private set; }

				// Token: 0x170027F3 RID: 10227
				// (get) Token: 0x0600EBE1 RID: 60385 RVA: 0x003303F1 File Offset: 0x0032E5F1
				// (set) Token: 0x0600EBE2 RID: 60386 RVA: 0x003303F9 File Offset: 0x0032E5F9
				public _LetB0 _LetB0 { get; private set; }

				// Token: 0x170027F4 RID: 10228
				// (get) Token: 0x0600EBE3 RID: 60387 RVA: 0x00330402 File Offset: 0x0032E602
				// (set) Token: 0x0600EBE4 RID: 60388 RVA: 0x0033040A File Offset: 0x0032E60A
				public _LetB1 _LetB1 { get; private set; }

				// Token: 0x170027F5 RID: 10229
				// (get) Token: 0x0600EBE5 RID: 60389 RVA: 0x00330413 File Offset: 0x0032E613
				// (set) Token: 0x0600EBE6 RID: 60390 RVA: 0x0033041B File Offset: 0x0032E61B
				public _LetB2 _LetB2 { get; private set; }

				// Token: 0x170027F6 RID: 10230
				// (get) Token: 0x0600EBE7 RID: 60391 RVA: 0x00330424 File Offset: 0x0032E624
				// (set) Token: 0x0600EBE8 RID: 60392 RVA: 0x0033042C File Offset: 0x0032E62C
				public _LetB3 _LetB3 { get; private set; }

				// Token: 0x170027F7 RID: 10231
				// (get) Token: 0x0600EBE9 RID: 60393 RVA: 0x00330435 File Offset: 0x0032E635
				// (set) Token: 0x0600EBEA RID: 60394 RVA: 0x0033043D File Offset: 0x0032E63D
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 _LetB4 { get; private set; }

				// Token: 0x170027F8 RID: 10232
				// (get) Token: 0x0600EBEB RID: 60395 RVA: 0x00330446 File Offset: 0x0032E646
				// (set) Token: 0x0600EBEC RID: 60396 RVA: 0x0033044E File Offset: 0x0032E64E
				public _LetB5 _LetB5 { get; private set; }

				// Token: 0x170027F9 RID: 10233
				// (get) Token: 0x0600EBED RID: 60397 RVA: 0x00330457 File Offset: 0x0032E657
				// (set) Token: 0x0600EBEE RID: 60398 RVA: 0x0033045F File Offset: 0x0032E65F
				public _LetB6 _LetB6 { get; private set; }

				// Token: 0x170027FA RID: 10234
				// (get) Token: 0x0600EBEF RID: 60399 RVA: 0x00330468 File Offset: 0x0032E668
				// (set) Token: 0x0600EBF0 RID: 60400 RVA: 0x00330470 File Offset: 0x0032E670
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 _LetB7 { get; private set; }

				// Token: 0x0600EBF1 RID: 60401 RVA: 0x0033047C File Offset: 0x0032E67C
				public NodeHoles(GrammarBuilders builders)
				{
					this.@switch = @switch.CreateHole(builders, null);
					this.ite = ite.CreateHole(builders, null);
					this.pred = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateHole(builders, null);
					this.st = st.CreateHole(builders, null);
					this.e = e.CreateHole(builders, null);
					this.f = f.CreateHole(builders, null);
					this.columnName = columnName.CreateHole(builders, null);
					this.letOptions = letOptions.CreateHole(builders, null);
					this.cell = cell.CreateHole(builders, null);
					this.x = x.CreateHole(builders, null);
					this.v = v.CreateHole(builders, null);
					this.indexInputString = indexInputString.CreateHole(builders, null);
					this.lookupInput = lookupInput.CreateHole(builders, null);
					this.conv = conv.CreateHole(builders, null);
					this.sharedParsedNumber = sharedParsedNumber.CreateHole(builders, null);
					this.sharedNumberFormat = sharedNumberFormat.CreateHole(builders, null);
					this.sharedParsedDt = sharedParsedDt.CreateHole(builders, null);
					this.sharedDtFormat = sharedDtFormat.CreateHole(builders, null);
					this.rangeString = rangeString.CreateHole(builders, null);
					this.rangeSubstring = rangeSubstring.CreateHole(builders, null);
					this.rangeNumber = rangeNumber.CreateHole(builders, null);
					this.dtRangeString = dtRangeString.CreateHole(builders, null);
					this.dtRangeSubstring = dtRangeSubstring.CreateHole(builders, null);
					this.rangeDateTime = rangeDateTime.CreateHole(builders, null);
					this.datetime = datetime.CreateHole(builders, null);
					this.inputDateTime = inputDateTime.CreateHole(builders, null);
					this.parsedDateTime = parsedDateTime.CreateHole(builders, null);
					this.SS = SS.CreateHole(builders, null);
					this.PP = PP.CreateHole(builders, null);
					this.pl1 = pl1.CreateHole(builders, null);
					this.pl2 = pl2.CreateHole(builders, null);
					this.pl2p = pl2p.CreateHole(builders, null);
					this.pos = pos.CreateHole(builders, null);
					this.regexPair = regexPair.CreateHole(builders, null);
					this.number = number.CreateHole(builders, null);
					this.castToNumber = castToNumber.CreateHole(builders, null);
					this.inputNumber = inputNumber.CreateHole(builders, null);
					this.parsedNumber = parsedNumber.CreateHole(builders, null);
					this.b = b.CreateHole(builders, null);
					this.cs = cs.CreateHole(builders, null);
					this.y = y.CreateHole(builders, null);
					this.k = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateHole(builders, null);
					this.externalExtractor = externalExtractor.CreateHole(builders, null);
					this.r = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateHole(builders, null);
					this.s = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateHole(builders, null);
					this.name = name.CreateHole(builders, null);
					this.roundingSpec = roundingSpec.CreateHole(builders, null);
					this.dtRoundingSpec = dtRoundingSpec.CreateHole(builders, null);
					this.minTrailingZeros = minTrailingZeros.CreateHole(builders, null);
					this.maxTrailingZeros = maxTrailingZeros.CreateHole(builders, null);
					this.minTrailingZerosAndWhitespace = minTrailingZerosAndWhitespace.CreateHole(builders, null);
					this.minLeadingZeros = minLeadingZeros.CreateHole(builders, null);
					this.minLeadingZerosAndWhitespace = minLeadingZerosAndWhitespace.CreateHole(builders, null);
					this.numberFormatSeparatorChar = numberFormatSeparatorChar.CreateHole(builders, null);
					this.numberFormatDetails = numberFormatDetails.CreateHole(builders, null);
					this.numberFormat = numberFormat.CreateHole(builders, null);
					this.numberFormatLiteral = numberFormatLiteral.CreateHole(builders, null);
					this.outputDtFormat = outputDtFormat.CreateHole(builders, null);
					this.inputDtFormats = inputDtFormats.CreateHole(builders, null);
					this.lookupDictionary = lookupDictionary.CreateHole(builders, null);
					this.idx = idx.CreateHole(builders, null);
					this.columnIdx = columnIdx.CreateHole(builders, null);
					this._LetB0 = _LetB0.CreateHole(builders, null);
					this._LetB1 = _LetB1.CreateHole(builders, null);
					this._LetB2 = _LetB2.CreateHole(builders, null);
					this._LetB3 = _LetB3.CreateHole(builders, null);
					this._LetB4 = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateHole(builders, null);
					this._LetB5 = _LetB5.CreateHole(builders, null);
					this._LetB6 = _LetB6.CreateHole(builders, null);
					this._LetB7 = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateHole(builders, null);
				}
			}

			// Token: 0x02001BDC RID: 7132
			public class NodeUnsafe
			{
				// Token: 0x0600EBF2 RID: 60402 RVA: 0x0033081D File Offset: 0x0032EA1D
				public @switch @switch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch.CreateUnsafe(node);
				}

				// Token: 0x0600EBF3 RID: 60403 RVA: 0x00330825 File Offset: 0x0032EA25
				public ite ite(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite.CreateUnsafe(node);
				}

				// Token: 0x0600EBF4 RID: 60404 RVA: 0x0033082D File Offset: 0x0032EA2D
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateUnsafe(node);
				}

				// Token: 0x0600EBF5 RID: 60405 RVA: 0x00330835 File Offset: 0x0032EA35
				public st st(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st.CreateUnsafe(node);
				}

				// Token: 0x0600EBF6 RID: 60406 RVA: 0x0033083D File Offset: 0x0032EA3D
				public e e(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e.CreateUnsafe(node);
				}

				// Token: 0x0600EBF7 RID: 60407 RVA: 0x00330845 File Offset: 0x0032EA45
				public f f(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f.CreateUnsafe(node);
				}

				// Token: 0x0600EBF8 RID: 60408 RVA: 0x0033084D File Offset: 0x0032EA4D
				public columnName columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName.CreateUnsafe(node);
				}

				// Token: 0x0600EBF9 RID: 60409 RVA: 0x00330855 File Offset: 0x0032EA55
				public letOptions letOptions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions.CreateUnsafe(node);
				}

				// Token: 0x0600EBFA RID: 60410 RVA: 0x0033085D File Offset: 0x0032EA5D
				public cell cell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell.CreateUnsafe(node);
				}

				// Token: 0x0600EBFB RID: 60411 RVA: 0x00330865 File Offset: 0x0032EA65
				public x x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x.CreateUnsafe(node);
				}

				// Token: 0x0600EBFC RID: 60412 RVA: 0x0033086D File Offset: 0x0032EA6D
				public v v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v.CreateUnsafe(node);
				}

				// Token: 0x0600EBFD RID: 60413 RVA: 0x00330875 File Offset: 0x0032EA75
				public indexInputString indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString.CreateUnsafe(node);
				}

				// Token: 0x0600EBFE RID: 60414 RVA: 0x0033087D File Offset: 0x0032EA7D
				public lookupInput lookupInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput.CreateUnsafe(node);
				}

				// Token: 0x0600EBFF RID: 60415 RVA: 0x00330885 File Offset: 0x0032EA85
				public conv conv(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv.CreateUnsafe(node);
				}

				// Token: 0x0600EC00 RID: 60416 RVA: 0x0033088D File Offset: 0x0032EA8D
				public sharedParsedNumber sharedParsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber.CreateUnsafe(node);
				}

				// Token: 0x0600EC01 RID: 60417 RVA: 0x00330895 File Offset: 0x0032EA95
				public sharedNumberFormat sharedNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat.CreateUnsafe(node);
				}

				// Token: 0x0600EC02 RID: 60418 RVA: 0x0033089D File Offset: 0x0032EA9D
				public sharedParsedDt sharedParsedDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt.CreateUnsafe(node);
				}

				// Token: 0x0600EC03 RID: 60419 RVA: 0x003308A5 File Offset: 0x0032EAA5
				public sharedDtFormat sharedDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat.CreateUnsafe(node);
				}

				// Token: 0x0600EC04 RID: 60420 RVA: 0x003308AD File Offset: 0x0032EAAD
				public rangeString rangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString.CreateUnsafe(node);
				}

				// Token: 0x0600EC05 RID: 60421 RVA: 0x003308B5 File Offset: 0x0032EAB5
				public rangeSubstring rangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring.CreateUnsafe(node);
				}

				// Token: 0x0600EC06 RID: 60422 RVA: 0x003308BD File Offset: 0x0032EABD
				public rangeNumber rangeNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber.CreateUnsafe(node);
				}

				// Token: 0x0600EC07 RID: 60423 RVA: 0x003308C5 File Offset: 0x0032EAC5
				public dtRangeString dtRangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString.CreateUnsafe(node);
				}

				// Token: 0x0600EC08 RID: 60424 RVA: 0x003308CD File Offset: 0x0032EACD
				public dtRangeSubstring dtRangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring.CreateUnsafe(node);
				}

				// Token: 0x0600EC09 RID: 60425 RVA: 0x003308D5 File Offset: 0x0032EAD5
				public rangeDateTime rangeDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime.CreateUnsafe(node);
				}

				// Token: 0x0600EC0A RID: 60426 RVA: 0x003308DD File Offset: 0x0032EADD
				public datetime datetime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime.CreateUnsafe(node);
				}

				// Token: 0x0600EC0B RID: 60427 RVA: 0x003308E5 File Offset: 0x0032EAE5
				public inputDateTime inputDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime.CreateUnsafe(node);
				}

				// Token: 0x0600EC0C RID: 60428 RVA: 0x003308ED File Offset: 0x0032EAED
				public parsedDateTime parsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime.CreateUnsafe(node);
				}

				// Token: 0x0600EC0D RID: 60429 RVA: 0x003308F5 File Offset: 0x0032EAF5
				public SS SS(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS.CreateUnsafe(node);
				}

				// Token: 0x0600EC0E RID: 60430 RVA: 0x003308FD File Offset: 0x0032EAFD
				public PP PP(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP.CreateUnsafe(node);
				}

				// Token: 0x0600EC0F RID: 60431 RVA: 0x00330905 File Offset: 0x0032EB05
				public pl1 pl1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1.CreateUnsafe(node);
				}

				// Token: 0x0600EC10 RID: 60432 RVA: 0x0033090D File Offset: 0x0032EB0D
				public pl2 pl2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2.CreateUnsafe(node);
				}

				// Token: 0x0600EC11 RID: 60433 RVA: 0x00330915 File Offset: 0x0032EB15
				public pl2p pl2p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p.CreateUnsafe(node);
				}

				// Token: 0x0600EC12 RID: 60434 RVA: 0x0033091D File Offset: 0x0032EB1D
				public pos pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos.CreateUnsafe(node);
				}

				// Token: 0x0600EC13 RID: 60435 RVA: 0x00330925 File Offset: 0x0032EB25
				public regexPair regexPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair.CreateUnsafe(node);
				}

				// Token: 0x0600EC14 RID: 60436 RVA: 0x0033092D File Offset: 0x0032EB2D
				public number number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number.CreateUnsafe(node);
				}

				// Token: 0x0600EC15 RID: 60437 RVA: 0x00330935 File Offset: 0x0032EB35
				public castToNumber castToNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber.CreateUnsafe(node);
				}

				// Token: 0x0600EC16 RID: 60438 RVA: 0x0033093D File Offset: 0x0032EB3D
				public inputNumber inputNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber.CreateUnsafe(node);
				}

				// Token: 0x0600EC17 RID: 60439 RVA: 0x00330945 File Offset: 0x0032EB45
				public parsedNumber parsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber.CreateUnsafe(node);
				}

				// Token: 0x0600EC18 RID: 60440 RVA: 0x0033094D File Offset: 0x0032EB4D
				public b b(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b.CreateUnsafe(node);
				}

				// Token: 0x0600EC19 RID: 60441 RVA: 0x00330955 File Offset: 0x0032EB55
				public cs cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs.CreateUnsafe(node);
				}

				// Token: 0x0600EC1A RID: 60442 RVA: 0x0033095D File Offset: 0x0032EB5D
				public y y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y.CreateUnsafe(node);
				}

				// Token: 0x0600EC1B RID: 60443 RVA: 0x00330965 File Offset: 0x0032EB65
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x0600EC1C RID: 60444 RVA: 0x0033096D File Offset: 0x0032EB6D
				public externalExtractor externalExtractor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor.CreateUnsafe(node);
				}

				// Token: 0x0600EC1D RID: 60445 RVA: 0x00330975 File Offset: 0x0032EB75
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateUnsafe(node);
				}

				// Token: 0x0600EC1E RID: 60446 RVA: 0x0033097D File Offset: 0x0032EB7D
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateUnsafe(node);
				}

				// Token: 0x0600EC1F RID: 60447 RVA: 0x00330985 File Offset: 0x0032EB85
				public name name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name.CreateUnsafe(node);
				}

				// Token: 0x0600EC20 RID: 60448 RVA: 0x0033098D File Offset: 0x0032EB8D
				public roundingSpec roundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec.CreateUnsafe(node);
				}

				// Token: 0x0600EC21 RID: 60449 RVA: 0x00330995 File Offset: 0x0032EB95
				public dtRoundingSpec dtRoundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec.CreateUnsafe(node);
				}

				// Token: 0x0600EC22 RID: 60450 RVA: 0x0033099D File Offset: 0x0032EB9D
				public minTrailingZeros minTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros.CreateUnsafe(node);
				}

				// Token: 0x0600EC23 RID: 60451 RVA: 0x003309A5 File Offset: 0x0032EBA5
				public maxTrailingZeros maxTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros.CreateUnsafe(node);
				}

				// Token: 0x0600EC24 RID: 60452 RVA: 0x003309AD File Offset: 0x0032EBAD
				public minTrailingZerosAndWhitespace minTrailingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace.CreateUnsafe(node);
				}

				// Token: 0x0600EC25 RID: 60453 RVA: 0x003309B5 File Offset: 0x0032EBB5
				public minLeadingZeros minLeadingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros.CreateUnsafe(node);
				}

				// Token: 0x0600EC26 RID: 60454 RVA: 0x003309BD File Offset: 0x0032EBBD
				public minLeadingZerosAndWhitespace minLeadingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace.CreateUnsafe(node);
				}

				// Token: 0x0600EC27 RID: 60455 RVA: 0x003309C5 File Offset: 0x0032EBC5
				public numberFormatSeparatorChar numberFormatSeparatorChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar.CreateUnsafe(node);
				}

				// Token: 0x0600EC28 RID: 60456 RVA: 0x003309CD File Offset: 0x0032EBCD
				public numberFormatDetails numberFormatDetails(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails.CreateUnsafe(node);
				}

				// Token: 0x0600EC29 RID: 60457 RVA: 0x003309D5 File Offset: 0x0032EBD5
				public numberFormat numberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat.CreateUnsafe(node);
				}

				// Token: 0x0600EC2A RID: 60458 RVA: 0x003309DD File Offset: 0x0032EBDD
				public numberFormatLiteral numberFormatLiteral(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral.CreateUnsafe(node);
				}

				// Token: 0x0600EC2B RID: 60459 RVA: 0x003309E5 File Offset: 0x0032EBE5
				public outputDtFormat outputDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat.CreateUnsafe(node);
				}

				// Token: 0x0600EC2C RID: 60460 RVA: 0x003309ED File Offset: 0x0032EBED
				public inputDtFormats inputDtFormats(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats.CreateUnsafe(node);
				}

				// Token: 0x0600EC2D RID: 60461 RVA: 0x003309F5 File Offset: 0x0032EBF5
				public lookupDictionary lookupDictionary(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary.CreateUnsafe(node);
				}

				// Token: 0x0600EC2E RID: 60462 RVA: 0x003309FD File Offset: 0x0032EBFD
				public idx idx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx.CreateUnsafe(node);
				}

				// Token: 0x0600EC2F RID: 60463 RVA: 0x00330A05 File Offset: 0x0032EC05
				public columnIdx columnIdx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx.CreateUnsafe(node);
				}

				// Token: 0x0600EC30 RID: 60464 RVA: 0x00330A0D File Offset: 0x0032EC0D
				public _LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x0600EC31 RID: 60465 RVA: 0x00330A15 File Offset: 0x0032EC15
				public _LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}

				// Token: 0x0600EC32 RID: 60466 RVA: 0x00330A1D File Offset: 0x0032EC1D
				public _LetB2 _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2.CreateUnsafe(node);
				}

				// Token: 0x0600EC33 RID: 60467 RVA: 0x00330A25 File Offset: 0x0032EC25
				public _LetB3 _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3.CreateUnsafe(node);
				}

				// Token: 0x0600EC34 RID: 60468 RVA: 0x00330A2D File Offset: 0x0032EC2D
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateUnsafe(node);
				}

				// Token: 0x0600EC35 RID: 60469 RVA: 0x00330A35 File Offset: 0x0032EC35
				public _LetB5 _LetB5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5.CreateUnsafe(node);
				}

				// Token: 0x0600EC36 RID: 60470 RVA: 0x00330A3D File Offset: 0x0032EC3D
				public _LetB6 _LetB6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6.CreateUnsafe(node);
				}

				// Token: 0x0600EC37 RID: 60471 RVA: 0x00330A45 File Offset: 0x0032EC45
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 _LetB7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateUnsafe(node);
				}
			}

			// Token: 0x02001BDD RID: 7133
			public class NodeCast
			{
				// Token: 0x0600EC39 RID: 60473 RVA: 0x00330A4D File Offset: 0x0032EC4D
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EC3A RID: 60474 RVA: 0x00330A5C File Offset: 0x0032EC5C
				public @switch @switch(ProgramNode node)
				{
					@switch? @switch = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch.CreateSafe(this._builders, node);
					if (@switch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @switch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @switch.Value;
				}

				// Token: 0x0600EC3B RID: 60475 RVA: 0x00330AB0 File Offset: 0x0032ECB0
				public ite ite(ProgramNode node)
				{
					ite? ite = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite.CreateSafe(this._builders, node);
					if (ite == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ite but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ite.Value;
				}

				// Token: 0x0600EC3C RID: 60476 RVA: 0x00330B04 File Offset: 0x0032ED04
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred pred(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred? pred = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pred.Value;
				}

				// Token: 0x0600EC3D RID: 60477 RVA: 0x00330B58 File Offset: 0x0032ED58
				public st st(ProgramNode node)
				{
					st? st = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st.CreateSafe(this._builders, node);
					if (st == null)
					{
						string text = "node";
						string text2 = "expected node for symbol st but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return st.Value;
				}

				// Token: 0x0600EC3E RID: 60478 RVA: 0x00330BAC File Offset: 0x0032EDAC
				public e e(ProgramNode node)
				{
					e? e = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e.CreateSafe(this._builders, node);
					if (e == null)
					{
						string text = "node";
						string text2 = "expected node for symbol e but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return e.Value;
				}

				// Token: 0x0600EC3F RID: 60479 RVA: 0x00330C00 File Offset: 0x0032EE00
				public f f(ProgramNode node)
				{
					f? f = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f.CreateSafe(this._builders, node);
					if (f == null)
					{
						string text = "node";
						string text2 = "expected node for symbol f but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return f.Value;
				}

				// Token: 0x0600EC40 RID: 60480 RVA: 0x00330C54 File Offset: 0x0032EE54
				public columnName columnName(ProgramNode node)
				{
					columnName? columnName = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
					if (columnName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnName.Value;
				}

				// Token: 0x0600EC41 RID: 60481 RVA: 0x00330CA8 File Offset: 0x0032EEA8
				public letOptions letOptions(ProgramNode node)
				{
					letOptions? letOptions = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions.CreateSafe(this._builders, node);
					if (letOptions == null)
					{
						string text = "node";
						string text2 = "expected node for symbol letOptions but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letOptions.Value;
				}

				// Token: 0x0600EC42 RID: 60482 RVA: 0x00330CFC File Offset: 0x0032EEFC
				public cell cell(ProgramNode node)
				{
					cell? cell = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell.CreateSafe(this._builders, node);
					if (cell == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cell but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cell.Value;
				}

				// Token: 0x0600EC43 RID: 60483 RVA: 0x00330D50 File Offset: 0x0032EF50
				public x x(ProgramNode node)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						string text = "node";
						string text2 = "expected node for symbol x but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return x.Value;
				}

				// Token: 0x0600EC44 RID: 60484 RVA: 0x00330DA4 File Offset: 0x0032EFA4
				public v v(ProgramNode node)
				{
					v? v = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v.CreateSafe(this._builders, node);
					if (v == null)
					{
						string text = "node";
						string text2 = "expected node for symbol v but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return v.Value;
				}

				// Token: 0x0600EC45 RID: 60485 RVA: 0x00330DF8 File Offset: 0x0032EFF8
				public indexInputString indexInputString(ProgramNode node)
				{
					indexInputString? indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString.CreateSafe(this._builders, node);
					if (indexInputString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol indexInputString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return indexInputString.Value;
				}

				// Token: 0x0600EC46 RID: 60486 RVA: 0x00330E4C File Offset: 0x0032F04C
				public lookupInput lookupInput(ProgramNode node)
				{
					lookupInput? lookupInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput.CreateSafe(this._builders, node);
					if (lookupInput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol lookupInput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookupInput.Value;
				}

				// Token: 0x0600EC47 RID: 60487 RVA: 0x00330EA0 File Offset: 0x0032F0A0
				public conv conv(ProgramNode node)
				{
					conv? conv = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv.CreateSafe(this._builders, node);
					if (conv == null)
					{
						string text = "node";
						string text2 = "expected node for symbol conv but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conv.Value;
				}

				// Token: 0x0600EC48 RID: 60488 RVA: 0x00330EF4 File Offset: 0x0032F0F4
				public sharedParsedNumber sharedParsedNumber(ProgramNode node)
				{
					sharedParsedNumber? sharedParsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber.CreateSafe(this._builders, node);
					if (sharedParsedNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sharedParsedNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sharedParsedNumber.Value;
				}

				// Token: 0x0600EC49 RID: 60489 RVA: 0x00330F48 File Offset: 0x0032F148
				public sharedNumberFormat sharedNumberFormat(ProgramNode node)
				{
					sharedNumberFormat? sharedNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat.CreateSafe(this._builders, node);
					if (sharedNumberFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sharedNumberFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sharedNumberFormat.Value;
				}

				// Token: 0x0600EC4A RID: 60490 RVA: 0x00330F9C File Offset: 0x0032F19C
				public sharedParsedDt sharedParsedDt(ProgramNode node)
				{
					sharedParsedDt? sharedParsedDt = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt.CreateSafe(this._builders, node);
					if (sharedParsedDt == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sharedParsedDt but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sharedParsedDt.Value;
				}

				// Token: 0x0600EC4B RID: 60491 RVA: 0x00330FF0 File Offset: 0x0032F1F0
				public sharedDtFormat sharedDtFormat(ProgramNode node)
				{
					sharedDtFormat? sharedDtFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat.CreateSafe(this._builders, node);
					if (sharedDtFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sharedDtFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sharedDtFormat.Value;
				}

				// Token: 0x0600EC4C RID: 60492 RVA: 0x00331044 File Offset: 0x0032F244
				public rangeString rangeString(ProgramNode node)
				{
					rangeString? rangeString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString.CreateSafe(this._builders, node);
					if (rangeString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeString.Value;
				}

				// Token: 0x0600EC4D RID: 60493 RVA: 0x00331098 File Offset: 0x0032F298
				public rangeSubstring rangeSubstring(ProgramNode node)
				{
					rangeSubstring? rangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring.CreateSafe(this._builders, node);
					if (rangeSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeSubstring.Value;
				}

				// Token: 0x0600EC4E RID: 60494 RVA: 0x003310EC File Offset: 0x0032F2EC
				public rangeNumber rangeNumber(ProgramNode node)
				{
					rangeNumber? rangeNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber.CreateSafe(this._builders, node);
					if (rangeNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeNumber.Value;
				}

				// Token: 0x0600EC4F RID: 60495 RVA: 0x00331140 File Offset: 0x0032F340
				public dtRangeString dtRangeString(ProgramNode node)
				{
					dtRangeString? dtRangeString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString.CreateSafe(this._builders, node);
					if (dtRangeString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dtRangeString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRangeString.Value;
				}

				// Token: 0x0600EC50 RID: 60496 RVA: 0x00331194 File Offset: 0x0032F394
				public dtRangeSubstring dtRangeSubstring(ProgramNode node)
				{
					dtRangeSubstring? dtRangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring.CreateSafe(this._builders, node);
					if (dtRangeSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dtRangeSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRangeSubstring.Value;
				}

				// Token: 0x0600EC51 RID: 60497 RVA: 0x003311E8 File Offset: 0x0032F3E8
				public rangeDateTime rangeDateTime(ProgramNode node)
				{
					rangeDateTime? rangeDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime.CreateSafe(this._builders, node);
					if (rangeDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeDateTime.Value;
				}

				// Token: 0x0600EC52 RID: 60498 RVA: 0x0033123C File Offset: 0x0032F43C
				public datetime datetime(ProgramNode node)
				{
					datetime? datetime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime.CreateSafe(this._builders, node);
					if (datetime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol datetime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return datetime.Value;
				}

				// Token: 0x0600EC53 RID: 60499 RVA: 0x00331290 File Offset: 0x0032F490
				public inputDateTime inputDateTime(ProgramNode node)
				{
					inputDateTime? inputDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime.CreateSafe(this._builders, node);
					if (inputDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputDateTime.Value;
				}

				// Token: 0x0600EC54 RID: 60500 RVA: 0x003312E4 File Offset: 0x0032F4E4
				public parsedDateTime parsedDateTime(ProgramNode node)
				{
					parsedDateTime? parsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime.CreateSafe(this._builders, node);
					if (parsedDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parsedDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parsedDateTime.Value;
				}

				// Token: 0x0600EC55 RID: 60501 RVA: 0x00331338 File Offset: 0x0032F538
				public SS SS(ProgramNode node)
				{
					SS? ss = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS.CreateSafe(this._builders, node);
					if (ss == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SS but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ss.Value;
				}

				// Token: 0x0600EC56 RID: 60502 RVA: 0x0033138C File Offset: 0x0032F58C
				public PP PP(ProgramNode node)
				{
					PP? pp = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP.CreateSafe(this._builders, node);
					if (pp == null)
					{
						string text = "node";
						string text2 = "expected node for symbol PP but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pp.Value;
				}

				// Token: 0x0600EC57 RID: 60503 RVA: 0x003313E0 File Offset: 0x0032F5E0
				public pl1 pl1(ProgramNode node)
				{
					pl1? pl = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1.CreateSafe(this._builders, node);
					if (pl == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pl1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pl.Value;
				}

				// Token: 0x0600EC58 RID: 60504 RVA: 0x00331434 File Offset: 0x0032F634
				public pl2 pl2(ProgramNode node)
				{
					pl2? pl = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2.CreateSafe(this._builders, node);
					if (pl == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pl2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pl.Value;
				}

				// Token: 0x0600EC59 RID: 60505 RVA: 0x00331488 File Offset: 0x0032F688
				public pl2p pl2p(ProgramNode node)
				{
					pl2p? pl2p = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p.CreateSafe(this._builders, node);
					if (pl2p == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pl2p but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pl2p.Value;
				}

				// Token: 0x0600EC5A RID: 60506 RVA: 0x003314DC File Offset: 0x0032F6DC
				public pos pos(ProgramNode node)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pos.Value;
				}

				// Token: 0x0600EC5B RID: 60507 RVA: 0x00331530 File Offset: 0x0032F730
				public regexPair regexPair(ProgramNode node)
				{
					regexPair? regexPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair.CreateSafe(this._builders, node);
					if (regexPair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol regexPair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexPair.Value;
				}

				// Token: 0x0600EC5C RID: 60508 RVA: 0x00331584 File Offset: 0x0032F784
				public number number(ProgramNode node)
				{
					number? number = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number.CreateSafe(this._builders, node);
					if (number == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number.Value;
				}

				// Token: 0x0600EC5D RID: 60509 RVA: 0x003315D8 File Offset: 0x0032F7D8
				public castToNumber castToNumber(ProgramNode node)
				{
					castToNumber? castToNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber.CreateSafe(this._builders, node);
					if (castToNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol castToNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return castToNumber.Value;
				}

				// Token: 0x0600EC5E RID: 60510 RVA: 0x0033162C File Offset: 0x0032F82C
				public inputNumber inputNumber(ProgramNode node)
				{
					inputNumber? inputNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber.CreateSafe(this._builders, node);
					if (inputNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputNumber.Value;
				}

				// Token: 0x0600EC5F RID: 60511 RVA: 0x00331680 File Offset: 0x0032F880
				public parsedNumber parsedNumber(ProgramNode node)
				{
					parsedNumber? parsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber.CreateSafe(this._builders, node);
					if (parsedNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parsedNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parsedNumber.Value;
				}

				// Token: 0x0600EC60 RID: 60512 RVA: 0x003316D4 File Offset: 0x0032F8D4
				public b b(ProgramNode node)
				{
					b? b = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b.CreateSafe(this._builders, node);
					if (b == null)
					{
						string text = "node";
						string text2 = "expected node for symbol b but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return b.Value;
				}

				// Token: 0x0600EC61 RID: 60513 RVA: 0x00331728 File Offset: 0x0032F928
				public cs cs(ProgramNode node)
				{
					cs? cs = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs.CreateSafe(this._builders, node);
					if (cs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol cs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return cs.Value;
				}

				// Token: 0x0600EC62 RID: 60514 RVA: 0x0033177C File Offset: 0x0032F97C
				public y y(ProgramNode node)
				{
					y? y = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y.CreateSafe(this._builders, node);
					if (y == null)
					{
						string text = "node";
						string text2 = "expected node for symbol y but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return y.Value;
				}

				// Token: 0x0600EC63 RID: 60515 RVA: 0x003317D0 File Offset: 0x0032F9D0
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k k(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k? k = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x0600EC64 RID: 60516 RVA: 0x00331824 File Offset: 0x0032FA24
				public externalExtractor externalExtractor(ProgramNode node)
				{
					externalExtractor? externalExtractor = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor.CreateSafe(this._builders, node);
					if (externalExtractor == null)
					{
						string text = "node";
						string text2 = "expected node for symbol externalExtractor but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return externalExtractor.Value;
				}

				// Token: 0x0600EC65 RID: 60517 RVA: 0x00331878 File Offset: 0x0032FA78
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r r(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r? r = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						string text = "node";
						string text2 = "expected node for symbol r but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return r.Value;
				}

				// Token: 0x0600EC66 RID: 60518 RVA: 0x003318CC File Offset: 0x0032FACC
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s s(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s? s = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						string text = "node";
						string text2 = "expected node for symbol s but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return s.Value;
				}

				// Token: 0x0600EC67 RID: 60519 RVA: 0x00331920 File Offset: 0x0032FB20
				public name name(ProgramNode node)
				{
					name? name = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						string text = "node";
						string text2 = "expected node for symbol name but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return name.Value;
				}

				// Token: 0x0600EC68 RID: 60520 RVA: 0x00331974 File Offset: 0x0032FB74
				public roundingSpec roundingSpec(ProgramNode node)
				{
					roundingSpec? roundingSpec = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec.CreateSafe(this._builders, node);
					if (roundingSpec == null)
					{
						string text = "node";
						string text2 = "expected node for symbol roundingSpec but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return roundingSpec.Value;
				}

				// Token: 0x0600EC69 RID: 60521 RVA: 0x003319C8 File Offset: 0x0032FBC8
				public dtRoundingSpec dtRoundingSpec(ProgramNode node)
				{
					dtRoundingSpec? dtRoundingSpec = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec.CreateSafe(this._builders, node);
					if (dtRoundingSpec == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dtRoundingSpec but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRoundingSpec.Value;
				}

				// Token: 0x0600EC6A RID: 60522 RVA: 0x00331A1C File Offset: 0x0032FC1C
				public minTrailingZeros minTrailingZeros(ProgramNode node)
				{
					minTrailingZeros? minTrailingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros.CreateSafe(this._builders, node);
					if (minTrailingZeros == null)
					{
						string text = "node";
						string text2 = "expected node for symbol minTrailingZeros but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return minTrailingZeros.Value;
				}

				// Token: 0x0600EC6B RID: 60523 RVA: 0x00331A70 File Offset: 0x0032FC70
				public maxTrailingZeros maxTrailingZeros(ProgramNode node)
				{
					maxTrailingZeros? maxTrailingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros.CreateSafe(this._builders, node);
					if (maxTrailingZeros == null)
					{
						string text = "node";
						string text2 = "expected node for symbol maxTrailingZeros but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return maxTrailingZeros.Value;
				}

				// Token: 0x0600EC6C RID: 60524 RVA: 0x00331AC4 File Offset: 0x0032FCC4
				public minTrailingZerosAndWhitespace minTrailingZerosAndWhitespace(ProgramNode node)
				{
					minTrailingZerosAndWhitespace? minTrailingZerosAndWhitespace = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace.CreateSafe(this._builders, node);
					if (minTrailingZerosAndWhitespace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol minTrailingZerosAndWhitespace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return minTrailingZerosAndWhitespace.Value;
				}

				// Token: 0x0600EC6D RID: 60525 RVA: 0x00331B18 File Offset: 0x0032FD18
				public minLeadingZeros minLeadingZeros(ProgramNode node)
				{
					minLeadingZeros? minLeadingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros.CreateSafe(this._builders, node);
					if (minLeadingZeros == null)
					{
						string text = "node";
						string text2 = "expected node for symbol minLeadingZeros but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return minLeadingZeros.Value;
				}

				// Token: 0x0600EC6E RID: 60526 RVA: 0x00331B6C File Offset: 0x0032FD6C
				public minLeadingZerosAndWhitespace minLeadingZerosAndWhitespace(ProgramNode node)
				{
					minLeadingZerosAndWhitespace? minLeadingZerosAndWhitespace = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace.CreateSafe(this._builders, node);
					if (minLeadingZerosAndWhitespace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol minLeadingZerosAndWhitespace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return minLeadingZerosAndWhitespace.Value;
				}

				// Token: 0x0600EC6F RID: 60527 RVA: 0x00331BC0 File Offset: 0x0032FDC0
				public numberFormatSeparatorChar numberFormatSeparatorChar(ProgramNode node)
				{
					numberFormatSeparatorChar? numberFormatSeparatorChar = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar.CreateSafe(this._builders, node);
					if (numberFormatSeparatorChar == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormatSeparatorChar but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormatSeparatorChar.Value;
				}

				// Token: 0x0600EC70 RID: 60528 RVA: 0x00331C14 File Offset: 0x0032FE14
				public numberFormatDetails numberFormatDetails(ProgramNode node)
				{
					numberFormatDetails? numberFormatDetails = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails.CreateSafe(this._builders, node);
					if (numberFormatDetails == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormatDetails but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormatDetails.Value;
				}

				// Token: 0x0600EC71 RID: 60529 RVA: 0x00331C68 File Offset: 0x0032FE68
				public numberFormat numberFormat(ProgramNode node)
				{
					numberFormat? numberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat.CreateSafe(this._builders, node);
					if (numberFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormat.Value;
				}

				// Token: 0x0600EC72 RID: 60530 RVA: 0x00331CBC File Offset: 0x0032FEBC
				public numberFormatLiteral numberFormatLiteral(ProgramNode node)
				{
					numberFormatLiteral? numberFormatLiteral = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral.CreateSafe(this._builders, node);
					if (numberFormatLiteral == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormatLiteral but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormatLiteral.Value;
				}

				// Token: 0x0600EC73 RID: 60531 RVA: 0x00331D10 File Offset: 0x0032FF10
				public outputDtFormat outputDtFormat(ProgramNode node)
				{
					outputDtFormat? outputDtFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat.CreateSafe(this._builders, node);
					if (outputDtFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol outputDtFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return outputDtFormat.Value;
				}

				// Token: 0x0600EC74 RID: 60532 RVA: 0x00331D64 File Offset: 0x0032FF64
				public inputDtFormats inputDtFormats(ProgramNode node)
				{
					inputDtFormats? inputDtFormats = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats.CreateSafe(this._builders, node);
					if (inputDtFormats == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputDtFormats but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputDtFormats.Value;
				}

				// Token: 0x0600EC75 RID: 60533 RVA: 0x00331DB8 File Offset: 0x0032FFB8
				public lookupDictionary lookupDictionary(ProgramNode node)
				{
					lookupDictionary? lookupDictionary = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary.CreateSafe(this._builders, node);
					if (lookupDictionary == null)
					{
						string text = "node";
						string text2 = "expected node for symbol lookupDictionary but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookupDictionary.Value;
				}

				// Token: 0x0600EC76 RID: 60534 RVA: 0x00331E0C File Offset: 0x0033000C
				public idx idx(ProgramNode node)
				{
					idx? idx = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx.CreateSafe(this._builders, node);
					if (idx == null)
					{
						string text = "node";
						string text2 = "expected node for symbol idx but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return idx.Value;
				}

				// Token: 0x0600EC77 RID: 60535 RVA: 0x00331E60 File Offset: 0x00330060
				public columnIdx columnIdx(ProgramNode node)
				{
					columnIdx? columnIdx = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx.CreateSafe(this._builders, node);
					if (columnIdx == null)
					{
						string text = "node";
						string text2 = "expected node for symbol columnIdx but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return columnIdx.Value;
				}

				// Token: 0x0600EC78 RID: 60536 RVA: 0x00331EB4 File Offset: 0x003300B4
				public _LetB0 _LetB0(ProgramNode node)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC79 RID: 60537 RVA: 0x00331F08 File Offset: 0x00330108
				public _LetB1 _LetB1(ProgramNode node)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7A RID: 60538 RVA: 0x00331F5C File Offset: 0x0033015C
				public _LetB2 _LetB2(ProgramNode node)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7B RID: 60539 RVA: 0x00331FB0 File Offset: 0x003301B0
				public _LetB3 _LetB3(ProgramNode node)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB3 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7C RID: 60540 RVA: 0x00332004 File Offset: 0x00330204
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 _LetB4(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7D RID: 60541 RVA: 0x00332058 File Offset: 0x00330258
				public _LetB5 _LetB5(ProgramNode node)
				{
					_LetB5? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB5 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7E RID: 60542 RVA: 0x003320AC File Offset: 0x003302AC
				public _LetB6 _LetB6(ProgramNode node)
				{
					_LetB6? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB6 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600EC7F RID: 60543 RVA: 0x00332100 File Offset: 0x00330300
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 _LetB7(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB7 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x04005A3D RID: 23101
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BDE RID: 7134
			public class RuleCast
			{
				// Token: 0x0600EC80 RID: 60544 RVA: 0x00332151 File Offset: 0x00330351
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EC81 RID: 60545 RVA: 0x00332160 File Offset: 0x00330360
				public SingleBranch SingleBranch(ProgramNode node)
				{
					SingleBranch? singleBranch = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SingleBranch.CreateSafe(this._builders, node);
					if (singleBranch == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleBranch but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleBranch.Value;
				}

				// Token: 0x0600EC82 RID: 60546 RVA: 0x003321B4 File Offset: 0x003303B4
				public switch_ite switch_ite(ProgramNode node)
				{
					switch_ite? switch_ite = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.switch_ite.CreateSafe(this._builders, node);
					if (switch_ite == null)
					{
						string text = "node";
						string text2 = "expected node for symbol switch_ite but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return switch_ite.Value;
				}

				// Token: 0x0600EC83 RID: 60547 RVA: 0x00332208 File Offset: 0x00330408
				public IfThenElse IfThenElse(ProgramNode node)
				{
					IfThenElse? ifThenElse = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
					if (ifThenElse == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IfThenElse but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ifThenElse.Value;
				}

				// Token: 0x0600EC84 RID: 60548 RVA: 0x0033225C File Offset: 0x0033045C
				public Predicate Predicate(ProgramNode node)
				{
					Predicate? predicate = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Predicate.CreateSafe(this._builders, node);
					if (predicate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Predicate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return predicate.Value;
				}

				// Token: 0x0600EC85 RID: 60549 RVA: 0x003322B0 File Offset: 0x003304B0
				public Transformation Transformation(ProgramNode node)
				{
					Transformation? transformation = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Transformation.CreateSafe(this._builders, node);
					if (transformation == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Transformation but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformation.Value;
				}

				// Token: 0x0600EC86 RID: 60550 RVA: 0x00332304 File Offset: 0x00330504
				public Atom Atom(ProgramNode node)
				{
					Atom? atom = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Atom.CreateSafe(this._builders, node);
					if (atom == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Atom but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return atom.Value;
				}

				// Token: 0x0600EC87 RID: 60551 RVA: 0x00332358 File Offset: 0x00330558
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat Concat(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat? concat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Concat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concat.Value;
				}

				// Token: 0x0600EC88 RID: 60552 RVA: 0x003323AC File Offset: 0x003305AC
				public ConstStr ConstStr(ProgramNode node)
				{
					ConstStr? constStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constStr.Value;
				}

				// Token: 0x0600EC89 RID: 60553 RVA: 0x00332400 File Offset: 0x00330600
				public LetColumnName LetColumnName(ProgramNode node)
				{
					LetColumnName? letColumnName = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetColumnName.CreateSafe(this._builders, node);
					if (letColumnName == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetColumnName but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letColumnName.Value;
				}

				// Token: 0x0600EC8A RID: 60554 RVA: 0x00332454 File Offset: 0x00330654
				public LetCell LetCell(ProgramNode node)
				{
					LetCell? letCell = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetCell.CreateSafe(this._builders, node);
					if (letCell == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetCell but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letCell.Value;
				}

				// Token: 0x0600EC8B RID: 60555 RVA: 0x003324A8 File Offset: 0x003306A8
				public LetX LetX(ProgramNode node)
				{
					LetX? letX = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
					if (letX == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetX but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letX.Value;
				}

				// Token: 0x0600EC8C RID: 60556 RVA: 0x003324FC File Offset: 0x003306FC
				public ChooseInput ChooseInput(ProgramNode node)
				{
					ChooseInput? chooseInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ChooseInput.CreateSafe(this._builders, node);
					if (chooseInput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ChooseInput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return chooseInput.Value;
				}

				// Token: 0x0600EC8D RID: 60557 RVA: 0x00332550 File Offset: 0x00330750
				public v_indexInputString v_indexInputString(ProgramNode node)
				{
					v_indexInputString? v_indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.v_indexInputString.CreateSafe(this._builders, node);
					if (v_indexInputString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol v_indexInputString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return v_indexInputString.Value;
				}

				// Token: 0x0600EC8E RID: 60558 RVA: 0x003325A4 File Offset: 0x003307A4
				public IndexInputString IndexInputString(ProgramNode node)
				{
					IndexInputString? indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IndexInputString.CreateSafe(this._builders, node);
					if (indexInputString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IndexInputString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return indexInputString.Value;
				}

				// Token: 0x0600EC8F RID: 60559 RVA: 0x003325F8 File Offset: 0x003307F8
				public LookupInput LookupInput(ProgramNode node)
				{
					LookupInput? lookupInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LookupInput.CreateSafe(this._builders, node);
					if (lookupInput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LookupInput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookupInput.Value;
				}

				// Token: 0x0600EC90 RID: 60560 RVA: 0x0033264C File Offset: 0x0033084C
				public lookupInput_indexInputString lookupInput_indexInputString(ProgramNode node)
				{
					lookupInput_indexInputString? lookupInput_indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.lookupInput_indexInputString.CreateSafe(this._builders, node);
					if (lookupInput_indexInputString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol lookupInput_indexInputString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookupInput_indexInputString.Value;
				}

				// Token: 0x0600EC91 RID: 60561 RVA: 0x003326A0 File Offset: 0x003308A0
				public LetSharedNumberFormat LetSharedNumberFormat(ProgramNode node)
				{
					LetSharedNumberFormat? letSharedNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedNumberFormat.CreateSafe(this._builders, node);
					if (letSharedNumberFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSharedNumberFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSharedNumberFormat.Value;
				}

				// Token: 0x0600EC92 RID: 60562 RVA: 0x003326F4 File Offset: 0x003308F4
				public LetSharedDateTimeFormat LetSharedDateTimeFormat(ProgramNode node)
				{
					LetSharedDateTimeFormat? letSharedDateTimeFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedDateTimeFormat.CreateSafe(this._builders, node);
					if (letSharedDateTimeFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSharedDateTimeFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSharedDateTimeFormat.Value;
				}

				// Token: 0x0600EC93 RID: 60563 RVA: 0x00332748 File Offset: 0x00330948
				public SubString SubString(ProgramNode node)
				{
					SubString? subString = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubString.CreateSafe(this._builders, node);
					if (subString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SubString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subString.Value;
				}

				// Token: 0x0600EC94 RID: 60564 RVA: 0x0033279C File Offset: 0x0033099C
				public ToLowercase ToLowercase(ProgramNode node)
				{
					ToLowercase? toLowercase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToLowercase.CreateSafe(this._builders, node);
					if (toLowercase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToLowercase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toLowercase.Value;
				}

				// Token: 0x0600EC95 RID: 60565 RVA: 0x003327F0 File Offset: 0x003309F0
				public ToUppercase ToUppercase(ProgramNode node)
				{
					ToUppercase? toUppercase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToUppercase.CreateSafe(this._builders, node);
					if (toUppercase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToUppercase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toUppercase.Value;
				}

				// Token: 0x0600EC96 RID: 60566 RVA: 0x00332844 File Offset: 0x00330A44
				public ToSimpleTitleCase ToSimpleTitleCase(ProgramNode node)
				{
					ToSimpleTitleCase? toSimpleTitleCase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToSimpleTitleCase.CreateSafe(this._builders, node);
					if (toSimpleTitleCase == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToSimpleTitleCase but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toSimpleTitleCase.Value;
				}

				// Token: 0x0600EC97 RID: 60567 RVA: 0x00332898 File Offset: 0x00330A98
				public FormatPartialDateTime FormatPartialDateTime(ProgramNode node)
				{
					FormatPartialDateTime? formatPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatPartialDateTime.CreateSafe(this._builders, node);
					if (formatPartialDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatPartialDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatPartialDateTime.Value;
				}

				// Token: 0x0600EC98 RID: 60568 RVA: 0x003328EC File Offset: 0x00330AEC
				public FormatNumber FormatNumber(ProgramNode node)
				{
					FormatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatNumber.Value;
				}

				// Token: 0x0600EC99 RID: 60569 RVA: 0x00332940 File Offset: 0x00330B40
				public Lookup Lookup(ProgramNode node)
				{
					Lookup? lookup = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Lookup.CreateSafe(this._builders, node);
					if (lookup == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Lookup but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return lookup.Value;
				}

				// Token: 0x0600EC9A RID: 60570 RVA: 0x00332994 File Offset: 0x00330B94
				public FormatNumericRange FormatNumericRange(ProgramNode node)
				{
					FormatNumericRange? formatNumericRange = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumericRange.CreateSafe(this._builders, node);
					if (formatNumericRange == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatNumericRange but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatNumericRange.Value;
				}

				// Token: 0x0600EC9B RID: 60571 RVA: 0x003329E8 File Offset: 0x00330BE8
				public FormatDateTimeRange FormatDateTimeRange(ProgramNode node)
				{
					FormatDateTimeRange? formatDateTimeRange = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange.CreateSafe(this._builders, node);
					if (formatDateTimeRange == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FormatDateTimeRange but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return formatDateTimeRange.Value;
				}

				// Token: 0x0600EC9C RID: 60572 RVA: 0x00332A3C File Offset: 0x00330C3C
				public LetSharedParsedNumber LetSharedParsedNumber(ProgramNode node)
				{
					LetSharedParsedNumber? letSharedParsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedNumber.CreateSafe(this._builders, node);
					if (letSharedParsedNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSharedParsedNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSharedParsedNumber.Value;
				}

				// Token: 0x0600EC9D RID: 60573 RVA: 0x00332A90 File Offset: 0x00330C90
				public LetSharedParsedDateTime LetSharedParsedDateTime(ProgramNode node)
				{
					LetSharedParsedDateTime? letSharedParsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedDateTime.CreateSafe(this._builders, node);
					if (letSharedParsedDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetSharedParsedDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letSharedParsedDateTime.Value;
				}

				// Token: 0x0600EC9E RID: 60574 RVA: 0x00332AE4 File Offset: 0x00330CE4
				public rangeString_rangeSubstring rangeString_rangeSubstring(ProgramNode node)
				{
					rangeString_rangeSubstring? rangeString_rangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.rangeString_rangeSubstring.CreateSafe(this._builders, node);
					if (rangeString_rangeSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol rangeString_rangeSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeString_rangeSubstring.Value;
				}

				// Token: 0x0600EC9F RID: 60575 RVA: 0x00332B38 File Offset: 0x00330D38
				public RangeConcat RangeConcat(ProgramNode node)
				{
					RangeConcat? rangeConcat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConcat.CreateSafe(this._builders, node);
					if (rangeConcat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeConcat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeConcat.Value;
				}

				// Token: 0x0600ECA0 RID: 60576 RVA: 0x00332B8C File Offset: 0x00330D8C
				public RangeConstStr RangeConstStr(ProgramNode node)
				{
					RangeConstStr? rangeConstStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConstStr.CreateSafe(this._builders, node);
					if (rangeConstStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeConstStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeConstStr.Value;
				}

				// Token: 0x0600ECA1 RID: 60577 RVA: 0x00332BE0 File Offset: 0x00330DE0
				public RangeFormatNumber RangeFormatNumber(ProgramNode node)
				{
					RangeFormatNumber? rangeFormatNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatNumber.CreateSafe(this._builders, node);
					if (rangeFormatNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeFormatNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeFormatNumber.Value;
				}

				// Token: 0x0600ECA2 RID: 60578 RVA: 0x00332C34 File Offset: 0x00330E34
				public RangeRoundNumber RangeRoundNumber(ProgramNode node)
				{
					RangeRoundNumber? rangeRoundNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundNumber.CreateSafe(this._builders, node);
					if (rangeRoundNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeRoundNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeRoundNumber.Value;
				}

				// Token: 0x0600ECA3 RID: 60579 RVA: 0x00332C88 File Offset: 0x00330E88
				public dtRangeString_dtRangeSubstring dtRangeString_dtRangeSubstring(ProgramNode node)
				{
					dtRangeString_dtRangeSubstring? dtRangeString_dtRangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.dtRangeString_dtRangeSubstring.CreateSafe(this._builders, node);
					if (dtRangeString_dtRangeSubstring == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dtRangeString_dtRangeSubstring but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRangeString_dtRangeSubstring.Value;
				}

				// Token: 0x0600ECA4 RID: 60580 RVA: 0x00332CDC File Offset: 0x00330EDC
				public DtRangeConcat DtRangeConcat(ProgramNode node)
				{
					DtRangeConcat? dtRangeConcat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConcat.CreateSafe(this._builders, node);
					if (dtRangeConcat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DtRangeConcat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRangeConcat.Value;
				}

				// Token: 0x0600ECA5 RID: 60581 RVA: 0x00332D30 File Offset: 0x00330F30
				public DtRangeConstStr DtRangeConstStr(ProgramNode node)
				{
					DtRangeConstStr? dtRangeConstStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConstStr.CreateSafe(this._builders, node);
					if (dtRangeConstStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DtRangeConstStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dtRangeConstStr.Value;
				}

				// Token: 0x0600ECA6 RID: 60582 RVA: 0x00332D84 File Offset: 0x00330F84
				public RangeFormatDateTime RangeFormatDateTime(ProgramNode node)
				{
					RangeFormatDateTime? rangeFormatDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatDateTime.CreateSafe(this._builders, node);
					if (rangeFormatDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeFormatDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeFormatDateTime.Value;
				}

				// Token: 0x0600ECA7 RID: 60583 RVA: 0x00332DD8 File Offset: 0x00330FD8
				public RangeRoundDateTime RangeRoundDateTime(ProgramNode node)
				{
					RangeRoundDateTime? rangeRoundDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundDateTime.CreateSafe(this._builders, node);
					if (rangeRoundDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RangeRoundDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rangeRoundDateTime.Value;
				}

				// Token: 0x0600ECA8 RID: 60584 RVA: 0x00332E2C File Offset: 0x0033102C
				public datetime_inputDateTime datetime_inputDateTime(ProgramNode node)
				{
					datetime_inputDateTime? datetime_inputDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.datetime_inputDateTime.CreateSafe(this._builders, node);
					if (datetime_inputDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol datetime_inputDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return datetime_inputDateTime.Value;
				}

				// Token: 0x0600ECA9 RID: 60585 RVA: 0x00332E80 File Offset: 0x00331080
				public RoundPartialDateTime RoundPartialDateTime(ProgramNode node)
				{
					RoundPartialDateTime? roundPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundPartialDateTime.CreateSafe(this._builders, node);
					if (roundPartialDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RoundPartialDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return roundPartialDateTime.Value;
				}

				// Token: 0x0600ECAA RID: 60586 RVA: 0x00332ED4 File Offset: 0x003310D4
				public AsPartialDateTime AsPartialDateTime(ProgramNode node)
				{
					AsPartialDateTime? asPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsPartialDateTime.CreateSafe(this._builders, node);
					if (asPartialDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AsPartialDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return asPartialDateTime.Value;
				}

				// Token: 0x0600ECAB RID: 60587 RVA: 0x00332F28 File Offset: 0x00331128
				public inputDateTime_parsedDateTime inputDateTime_parsedDateTime(ProgramNode node)
				{
					inputDateTime_parsedDateTime? inputDateTime_parsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputDateTime_parsedDateTime.CreateSafe(this._builders, node);
					if (inputDateTime_parsedDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputDateTime_parsedDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputDateTime_parsedDateTime.Value;
				}

				// Token: 0x0600ECAC RID: 60588 RVA: 0x00332F7C File Offset: 0x0033117C
				public ParsePartialDateTime ParsePartialDateTime(ProgramNode node)
				{
					ParsePartialDateTime? parsePartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParsePartialDateTime.CreateSafe(this._builders, node);
					if (parsePartialDateTime == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ParsePartialDateTime but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parsePartialDateTime.Value;
				}

				// Token: 0x0600ECAD RID: 60589 RVA: 0x00332FD0 File Offset: 0x003311D0
				public WholeColumn WholeColumn(ProgramNode node)
				{
					WholeColumn? wholeColumn = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.WholeColumn.CreateSafe(this._builders, node);
					if (wholeColumn == null)
					{
						string text = "node";
						string text2 = "expected node for symbol WholeColumn but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return wholeColumn.Value;
				}

				// Token: 0x0600ECAE RID: 60590 RVA: 0x00333024 File Offset: 0x00331224
				public SubStr SubStr(ProgramNode node)
				{
					SubStr? subStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubStr.CreateSafe(this._builders, node);
					if (subStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SubStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return subStr.Value;
				}

				// Token: 0x0600ECAF RID: 60591 RVA: 0x00333078 File Offset: 0x00331278
				public Add Add(ProgramNode node)
				{
					Add? add = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
					if (add == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Add but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return add.Value;
				}

				// Token: 0x0600ECB0 RID: 60592 RVA: 0x003330CC File Offset: 0x003312CC
				public PosPairRelative PosPairRelative(ProgramNode node)
				{
					PosPairRelative? posPairRelative = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPairRelative.CreateSafe(this._builders, node);
					if (posPairRelative == null)
					{
						string text = "node";
						string text2 = "expected node for symbol PosPairRelative but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return posPairRelative.Value;
				}

				// Token: 0x0600ECB1 RID: 60593 RVA: 0x00333120 File Offset: 0x00331320
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4 _LetB4(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB4 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600ECB2 RID: 60594 RVA: 0x00333174 File Offset: 0x00331374
				public RSubStr RSubStr(ProgramNode node)
				{
					RSubStr? rsubStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RSubStr.CreateSafe(this._builders, node);
					if (rsubStr == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RSubStr but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return rsubStr.Value;
				}

				// Token: 0x0600ECB3 RID: 60595 RVA: 0x003331C8 File Offset: 0x003313C8
				public LetPL2 LetPL2(ProgramNode node)
				{
					LetPL2? letPL = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL2.CreateSafe(this._builders, node);
					if (letPL == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetPL2 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letPL.Value;
				}

				// Token: 0x0600ECB4 RID: 60596 RVA: 0x0033321C File Offset: 0x0033141C
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7 _LetB7(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB7 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x0600ECB5 RID: 60597 RVA: 0x00333270 File Offset: 0x00331470
				public PosPair PosPair(ProgramNode node)
				{
					PosPair? posPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPair.CreateSafe(this._builders, node);
					if (posPair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol PosPair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return posPair.Value;
				}

				// Token: 0x0600ECB6 RID: 60598 RVA: 0x003332C4 File Offset: 0x003314C4
				public LetPL1 LetPL1(ProgramNode node)
				{
					LetPL1? letPL = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL1.CreateSafe(this._builders, node);
					if (letPL == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetPL1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letPL.Value;
				}

				// Token: 0x0600ECB7 RID: 60599 RVA: 0x00333318 File Offset: 0x00331518
				public RegexPositionPair RegexPositionPair(ProgramNode node)
				{
					RegexPositionPair? regexPositionPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionPair.CreateSafe(this._builders, node);
					if (regexPositionPair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RegexPositionPair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexPositionPair.Value;
				}

				// Token: 0x0600ECB8 RID: 60600 RVA: 0x0033336C File Offset: 0x0033156C
				public ExternalExtractorPositionPair ExternalExtractorPositionPair(ProgramNode node)
				{
					ExternalExtractorPositionPair? externalExtractorPositionPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ExternalExtractorPositionPair.CreateSafe(this._builders, node);
					if (externalExtractorPositionPair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ExternalExtractorPositionPair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return externalExtractorPositionPair.Value;
				}

				// Token: 0x0600ECB9 RID: 60601 RVA: 0x003333C0 File Offset: 0x003315C0
				public RelativePosition RelativePosition(ProgramNode node)
				{
					RelativePosition? relativePosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RelativePosition.CreateSafe(this._builders, node);
					if (relativePosition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RelativePosition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return relativePosition.Value;
				}

				// Token: 0x0600ECBA RID: 60602 RVA: 0x00333414 File Offset: 0x00331614
				public RegexPositionRelative RegexPositionRelative(ProgramNode node)
				{
					RegexPositionRelative? regexPositionRelative = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionRelative.CreateSafe(this._builders, node);
					if (regexPositionRelative == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RegexPositionRelative but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexPositionRelative.Value;
				}

				// Token: 0x0600ECBB RID: 60603 RVA: 0x00333468 File Offset: 0x00331668
				[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
				public AbsolutePosition AbsolutePosition(ProgramNode node)
				{
					AbsolutePosition? absolutePosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AbsolutePosition.CreateSafe(this._builders, node);
					if (absolutePosition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AbsolutePosition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return absolutePosition.Value;
				}

				// Token: 0x0600ECBC RID: 60604 RVA: 0x003334BC File Offset: 0x003316BC
				[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
				public RegexPosition RegexPosition(ProgramNode node)
				{
					RegexPosition? regexPosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPosition.CreateSafe(this._builders, node);
					if (regexPosition == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RegexPosition but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexPosition.Value;
				}

				// Token: 0x0600ECBD RID: 60605 RVA: 0x00333510 File Offset: 0x00331710
				public RegexPair RegexPair(ProgramNode node)
				{
					RegexPair? regexPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPair.CreateSafe(this._builders, node);
					if (regexPair == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RegexPair but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return regexPair.Value;
				}

				// Token: 0x0600ECBE RID: 60606 RVA: 0x00333564 File Offset: 0x00331764
				public number_inputNumber number_inputNumber(ProgramNode node)
				{
					number_inputNumber? number_inputNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.number_inputNumber.CreateSafe(this._builders, node);
					if (number_inputNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol number_inputNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return number_inputNumber.Value;
				}

				// Token: 0x0600ECBF RID: 60607 RVA: 0x003335B8 File Offset: 0x003317B8
				public RoundNumber RoundNumber(ProgramNode node)
				{
					RoundNumber? roundNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
					if (roundNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RoundNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return roundNumber.Value;
				}

				// Token: 0x0600ECC0 RID: 60608 RVA: 0x0033360C File Offset: 0x0033180C
				public AsDecimal AsDecimal(ProgramNode node)
				{
					AsDecimal? asDecimal = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsDecimal.CreateSafe(this._builders, node);
					if (asDecimal == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AsDecimal but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return asDecimal.Value;
				}

				// Token: 0x0600ECC1 RID: 60609 RVA: 0x00333660 File Offset: 0x00331860
				public inputNumber_castToNumber inputNumber_castToNumber(ProgramNode node)
				{
					inputNumber_castToNumber? inputNumber_castToNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_castToNumber.CreateSafe(this._builders, node);
					if (inputNumber_castToNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputNumber_castToNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputNumber_castToNumber.Value;
				}

				// Token: 0x0600ECC2 RID: 60610 RVA: 0x003336B4 File Offset: 0x003318B4
				public inputNumber_parsedNumber inputNumber_parsedNumber(ProgramNode node)
				{
					inputNumber_parsedNumber? inputNumber_parsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_parsedNumber.CreateSafe(this._builders, node);
					if (inputNumber_parsedNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inputNumber_parsedNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inputNumber_parsedNumber.Value;
				}

				// Token: 0x0600ECC3 RID: 60611 RVA: 0x00333708 File Offset: 0x00331908
				public ParseNumber ParseNumber(ProgramNode node)
				{
					ParseNumber? parseNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
					if (parseNumber == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ParseNumber but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parseNumber.Value;
				}

				// Token: 0x0600ECC4 RID: 60612 RVA: 0x0033375C File Offset: 0x0033195C
				public LetPredicate LetPredicate(ProgramNode node)
				{
					LetPredicate? letPredicate = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPredicate.CreateSafe(this._builders, node);
					if (letPredicate == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetPredicate but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letPredicate.Value;
				}

				// Token: 0x0600ECC5 RID: 60613 RVA: 0x003337B0 File Offset: 0x003319B0
				public SelectInput SelectInput(ProgramNode node)
				{
					SelectInput? selectInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectInput.CreateSafe(this._builders, node);
					if (selectInput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectInput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectInput.Value;
				}

				// Token: 0x0600ECC6 RID: 60614 RVA: 0x00333804 File Offset: 0x00331A04
				public SelectIndexedInput SelectIndexedInput(ProgramNode node)
				{
					SelectIndexedInput? selectIndexedInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectIndexedInput.CreateSafe(this._builders, node);
					if (selectIndexedInput == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectIndexedInput but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectIndexedInput.Value;
				}

				// Token: 0x0600ECC7 RID: 60615 RVA: 0x00333858 File Offset: 0x00331A58
				public BuildNumberFormat BuildNumberFormat(ProgramNode node)
				{
					BuildNumberFormat? buildNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.BuildNumberFormat.CreateSafe(this._builders, node);
					if (buildNumberFormat == null)
					{
						string text = "node";
						string text2 = "expected node for symbol BuildNumberFormat but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return buildNumberFormat.Value;
				}

				// Token: 0x0600ECC8 RID: 60616 RVA: 0x003338AC File Offset: 0x00331AAC
				public numberFormat_numberFormatLiteral numberFormat_numberFormatLiteral(ProgramNode node)
				{
					numberFormat_numberFormatLiteral? numberFormat_numberFormatLiteral = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.numberFormat_numberFormatLiteral.CreateSafe(this._builders, node);
					if (numberFormat_numberFormatLiteral == null)
					{
						string text = "node";
						string text2 = "expected node for symbol numberFormat_numberFormatLiteral but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return numberFormat_numberFormatLiteral.Value;
				}

				// Token: 0x04005A3E RID: 23102
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BDF RID: 7135
			public class NodeIs
			{
				// Token: 0x0600ECC9 RID: 60617 RVA: 0x003338FD File Offset: 0x00331AFD
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ECCA RID: 60618 RVA: 0x0033390C File Offset: 0x00331B0C
				public bool @switch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECCB RID: 60619 RVA: 0x00333930 File Offset: 0x00331B30
				public bool @switch(ProgramNode node, out @switch value)
				{
					@switch? @switch = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch.CreateSafe(this._builders, node);
					if (@switch == null)
					{
						value = default(@switch);
						return false;
					}
					value = @switch.Value;
					return true;
				}

				// Token: 0x0600ECCC RID: 60620 RVA: 0x0033396C File Offset: 0x00331B6C
				public bool ite(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECCD RID: 60621 RVA: 0x00333990 File Offset: 0x00331B90
				public bool ite(ProgramNode node, out ite value)
				{
					ite? ite = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite.CreateSafe(this._builders, node);
					if (ite == null)
					{
						value = default(ite);
						return false;
					}
					value = ite.Value;
					return true;
				}

				// Token: 0x0600ECCE RID: 60622 RVA: 0x003339CC File Offset: 0x00331BCC
				public bool pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECCF RID: 60623 RVA: 0x003339F0 File Offset: 0x00331BF0
				public bool pred(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred? pred = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred);
						return false;
					}
					value = pred.Value;
					return true;
				}

				// Token: 0x0600ECD0 RID: 60624 RVA: 0x00333A2C File Offset: 0x00331C2C
				public bool st(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECD1 RID: 60625 RVA: 0x00333A50 File Offset: 0x00331C50
				public bool st(ProgramNode node, out st value)
				{
					st? st = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st.CreateSafe(this._builders, node);
					if (st == null)
					{
						value = default(st);
						return false;
					}
					value = st.Value;
					return true;
				}

				// Token: 0x0600ECD2 RID: 60626 RVA: 0x00333A8C File Offset: 0x00331C8C
				public bool e(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECD3 RID: 60627 RVA: 0x00333AB0 File Offset: 0x00331CB0
				public bool e(ProgramNode node, out e value)
				{
					e? e = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e.CreateSafe(this._builders, node);
					if (e == null)
					{
						value = default(e);
						return false;
					}
					value = e.Value;
					return true;
				}

				// Token: 0x0600ECD4 RID: 60628 RVA: 0x00333AEC File Offset: 0x00331CEC
				public bool f(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECD5 RID: 60629 RVA: 0x00333B10 File Offset: 0x00331D10
				public bool f(ProgramNode node, out f value)
				{
					f? f = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f.CreateSafe(this._builders, node);
					if (f == null)
					{
						value = default(f);
						return false;
					}
					value = f.Value;
					return true;
				}

				// Token: 0x0600ECD6 RID: 60630 RVA: 0x00333B4C File Offset: 0x00331D4C
				public bool columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECD7 RID: 60631 RVA: 0x00333B70 File Offset: 0x00331D70
				public bool columnName(ProgramNode node, out columnName value)
				{
					columnName? columnName = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
					if (columnName == null)
					{
						value = default(columnName);
						return false;
					}
					value = columnName.Value;
					return true;
				}

				// Token: 0x0600ECD8 RID: 60632 RVA: 0x00333BAC File Offset: 0x00331DAC
				public bool letOptions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECD9 RID: 60633 RVA: 0x00333BD0 File Offset: 0x00331DD0
				public bool letOptions(ProgramNode node, out letOptions value)
				{
					letOptions? letOptions = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions.CreateSafe(this._builders, node);
					if (letOptions == null)
					{
						value = default(letOptions);
						return false;
					}
					value = letOptions.Value;
					return true;
				}

				// Token: 0x0600ECDA RID: 60634 RVA: 0x00333C0C File Offset: 0x00331E0C
				public bool cell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECDB RID: 60635 RVA: 0x00333C30 File Offset: 0x00331E30
				public bool cell(ProgramNode node, out cell value)
				{
					cell? cell = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell.CreateSafe(this._builders, node);
					if (cell == null)
					{
						value = default(cell);
						return false;
					}
					value = cell.Value;
					return true;
				}

				// Token: 0x0600ECDC RID: 60636 RVA: 0x00333C6C File Offset: 0x00331E6C
				public bool x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECDD RID: 60637 RVA: 0x00333C90 File Offset: 0x00331E90
				public bool x(ProgramNode node, out x value)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						value = default(x);
						return false;
					}
					value = x.Value;
					return true;
				}

				// Token: 0x0600ECDE RID: 60638 RVA: 0x00333CCC File Offset: 0x00331ECC
				public bool v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECDF RID: 60639 RVA: 0x00333CF0 File Offset: 0x00331EF0
				public bool v(ProgramNode node, out v value)
				{
					v? v = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v.CreateSafe(this._builders, node);
					if (v == null)
					{
						value = default(v);
						return false;
					}
					value = v.Value;
					return true;
				}

				// Token: 0x0600ECE0 RID: 60640 RVA: 0x00333D2C File Offset: 0x00331F2C
				public bool indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECE1 RID: 60641 RVA: 0x00333D50 File Offset: 0x00331F50
				public bool indexInputString(ProgramNode node, out indexInputString value)
				{
					indexInputString? indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString.CreateSafe(this._builders, node);
					if (indexInputString == null)
					{
						value = default(indexInputString);
						return false;
					}
					value = indexInputString.Value;
					return true;
				}

				// Token: 0x0600ECE2 RID: 60642 RVA: 0x00333D8C File Offset: 0x00331F8C
				public bool lookupInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECE3 RID: 60643 RVA: 0x00333DB0 File Offset: 0x00331FB0
				public bool lookupInput(ProgramNode node, out lookupInput value)
				{
					lookupInput? lookupInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput.CreateSafe(this._builders, node);
					if (lookupInput == null)
					{
						value = default(lookupInput);
						return false;
					}
					value = lookupInput.Value;
					return true;
				}

				// Token: 0x0600ECE4 RID: 60644 RVA: 0x00333DEC File Offset: 0x00331FEC
				public bool conv(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECE5 RID: 60645 RVA: 0x00333E10 File Offset: 0x00332010
				public bool conv(ProgramNode node, out conv value)
				{
					conv? conv = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv.CreateSafe(this._builders, node);
					if (conv == null)
					{
						value = default(conv);
						return false;
					}
					value = conv.Value;
					return true;
				}

				// Token: 0x0600ECE6 RID: 60646 RVA: 0x00333E4C File Offset: 0x0033204C
				public bool sharedParsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECE7 RID: 60647 RVA: 0x00333E70 File Offset: 0x00332070
				public bool sharedParsedNumber(ProgramNode node, out sharedParsedNumber value)
				{
					sharedParsedNumber? sharedParsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber.CreateSafe(this._builders, node);
					if (sharedParsedNumber == null)
					{
						value = default(sharedParsedNumber);
						return false;
					}
					value = sharedParsedNumber.Value;
					return true;
				}

				// Token: 0x0600ECE8 RID: 60648 RVA: 0x00333EAC File Offset: 0x003320AC
				public bool sharedNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECE9 RID: 60649 RVA: 0x00333ED0 File Offset: 0x003320D0
				public bool sharedNumberFormat(ProgramNode node, out sharedNumberFormat value)
				{
					sharedNumberFormat? sharedNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat.CreateSafe(this._builders, node);
					if (sharedNumberFormat == null)
					{
						value = default(sharedNumberFormat);
						return false;
					}
					value = sharedNumberFormat.Value;
					return true;
				}

				// Token: 0x0600ECEA RID: 60650 RVA: 0x00333F0C File Offset: 0x0033210C
				public bool sharedParsedDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECEB RID: 60651 RVA: 0x00333F30 File Offset: 0x00332130
				public bool sharedParsedDt(ProgramNode node, out sharedParsedDt value)
				{
					sharedParsedDt? sharedParsedDt = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt.CreateSafe(this._builders, node);
					if (sharedParsedDt == null)
					{
						value = default(sharedParsedDt);
						return false;
					}
					value = sharedParsedDt.Value;
					return true;
				}

				// Token: 0x0600ECEC RID: 60652 RVA: 0x00333F6C File Offset: 0x0033216C
				public bool sharedDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECED RID: 60653 RVA: 0x00333F90 File Offset: 0x00332190
				public bool sharedDtFormat(ProgramNode node, out sharedDtFormat value)
				{
					sharedDtFormat? sharedDtFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat.CreateSafe(this._builders, node);
					if (sharedDtFormat == null)
					{
						value = default(sharedDtFormat);
						return false;
					}
					value = sharedDtFormat.Value;
					return true;
				}

				// Token: 0x0600ECEE RID: 60654 RVA: 0x00333FCC File Offset: 0x003321CC
				public bool rangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECEF RID: 60655 RVA: 0x00333FF0 File Offset: 0x003321F0
				public bool rangeString(ProgramNode node, out rangeString value)
				{
					rangeString? rangeString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString.CreateSafe(this._builders, node);
					if (rangeString == null)
					{
						value = default(rangeString);
						return false;
					}
					value = rangeString.Value;
					return true;
				}

				// Token: 0x0600ECF0 RID: 60656 RVA: 0x0033402C File Offset: 0x0033222C
				public bool rangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECF1 RID: 60657 RVA: 0x00334050 File Offset: 0x00332250
				public bool rangeSubstring(ProgramNode node, out rangeSubstring value)
				{
					rangeSubstring? rangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring.CreateSafe(this._builders, node);
					if (rangeSubstring == null)
					{
						value = default(rangeSubstring);
						return false;
					}
					value = rangeSubstring.Value;
					return true;
				}

				// Token: 0x0600ECF2 RID: 60658 RVA: 0x0033408C File Offset: 0x0033228C
				public bool rangeNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECF3 RID: 60659 RVA: 0x003340B0 File Offset: 0x003322B0
				public bool rangeNumber(ProgramNode node, out rangeNumber value)
				{
					rangeNumber? rangeNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber.CreateSafe(this._builders, node);
					if (rangeNumber == null)
					{
						value = default(rangeNumber);
						return false;
					}
					value = rangeNumber.Value;
					return true;
				}

				// Token: 0x0600ECF4 RID: 60660 RVA: 0x003340EC File Offset: 0x003322EC
				public bool dtRangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECF5 RID: 60661 RVA: 0x00334110 File Offset: 0x00332310
				public bool dtRangeString(ProgramNode node, out dtRangeString value)
				{
					dtRangeString? dtRangeString = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString.CreateSafe(this._builders, node);
					if (dtRangeString == null)
					{
						value = default(dtRangeString);
						return false;
					}
					value = dtRangeString.Value;
					return true;
				}

				// Token: 0x0600ECF6 RID: 60662 RVA: 0x0033414C File Offset: 0x0033234C
				public bool dtRangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECF7 RID: 60663 RVA: 0x00334170 File Offset: 0x00332370
				public bool dtRangeSubstring(ProgramNode node, out dtRangeSubstring value)
				{
					dtRangeSubstring? dtRangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring.CreateSafe(this._builders, node);
					if (dtRangeSubstring == null)
					{
						value = default(dtRangeSubstring);
						return false;
					}
					value = dtRangeSubstring.Value;
					return true;
				}

				// Token: 0x0600ECF8 RID: 60664 RVA: 0x003341AC File Offset: 0x003323AC
				public bool rangeDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECF9 RID: 60665 RVA: 0x003341D0 File Offset: 0x003323D0
				public bool rangeDateTime(ProgramNode node, out rangeDateTime value)
				{
					rangeDateTime? rangeDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime.CreateSafe(this._builders, node);
					if (rangeDateTime == null)
					{
						value = default(rangeDateTime);
						return false;
					}
					value = rangeDateTime.Value;
					return true;
				}

				// Token: 0x0600ECFA RID: 60666 RVA: 0x0033420C File Offset: 0x0033240C
				public bool datetime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECFB RID: 60667 RVA: 0x00334230 File Offset: 0x00332430
				public bool datetime(ProgramNode node, out datetime value)
				{
					datetime? datetime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime.CreateSafe(this._builders, node);
					if (datetime == null)
					{
						value = default(datetime);
						return false;
					}
					value = datetime.Value;
					return true;
				}

				// Token: 0x0600ECFC RID: 60668 RVA: 0x0033426C File Offset: 0x0033246C
				public bool inputDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECFD RID: 60669 RVA: 0x00334290 File Offset: 0x00332490
				public bool inputDateTime(ProgramNode node, out inputDateTime value)
				{
					inputDateTime? inputDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime.CreateSafe(this._builders, node);
					if (inputDateTime == null)
					{
						value = default(inputDateTime);
						return false;
					}
					value = inputDateTime.Value;
					return true;
				}

				// Token: 0x0600ECFE RID: 60670 RVA: 0x003342CC File Offset: 0x003324CC
				public bool parsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ECFF RID: 60671 RVA: 0x003342F0 File Offset: 0x003324F0
				public bool parsedDateTime(ProgramNode node, out parsedDateTime value)
				{
					parsedDateTime? parsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime.CreateSafe(this._builders, node);
					if (parsedDateTime == null)
					{
						value = default(parsedDateTime);
						return false;
					}
					value = parsedDateTime.Value;
					return true;
				}

				// Token: 0x0600ED00 RID: 60672 RVA: 0x0033432C File Offset: 0x0033252C
				public bool SS(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED01 RID: 60673 RVA: 0x00334350 File Offset: 0x00332550
				public bool SS(ProgramNode node, out SS value)
				{
					SS? ss = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS.CreateSafe(this._builders, node);
					if (ss == null)
					{
						value = default(SS);
						return false;
					}
					value = ss.Value;
					return true;
				}

				// Token: 0x0600ED02 RID: 60674 RVA: 0x0033438C File Offset: 0x0033258C
				public bool PP(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED03 RID: 60675 RVA: 0x003343B0 File Offset: 0x003325B0
				public bool PP(ProgramNode node, out PP value)
				{
					PP? pp = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP.CreateSafe(this._builders, node);
					if (pp == null)
					{
						value = default(PP);
						return false;
					}
					value = pp.Value;
					return true;
				}

				// Token: 0x0600ED04 RID: 60676 RVA: 0x003343EC File Offset: 0x003325EC
				public bool pl1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED05 RID: 60677 RVA: 0x00334410 File Offset: 0x00332610
				public bool pl1(ProgramNode node, out pl1 value)
				{
					pl1? pl = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1.CreateSafe(this._builders, node);
					if (pl == null)
					{
						value = default(pl1);
						return false;
					}
					value = pl.Value;
					return true;
				}

				// Token: 0x0600ED06 RID: 60678 RVA: 0x0033444C File Offset: 0x0033264C
				public bool pl2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED07 RID: 60679 RVA: 0x00334470 File Offset: 0x00332670
				public bool pl2(ProgramNode node, out pl2 value)
				{
					pl2? pl = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2.CreateSafe(this._builders, node);
					if (pl == null)
					{
						value = default(pl2);
						return false;
					}
					value = pl.Value;
					return true;
				}

				// Token: 0x0600ED08 RID: 60680 RVA: 0x003344AC File Offset: 0x003326AC
				public bool pl2p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED09 RID: 60681 RVA: 0x003344D0 File Offset: 0x003326D0
				public bool pl2p(ProgramNode node, out pl2p value)
				{
					pl2p? pl2p = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p.CreateSafe(this._builders, node);
					if (pl2p == null)
					{
						value = default(pl2p);
						return false;
					}
					value = pl2p.Value;
					return true;
				}

				// Token: 0x0600ED0A RID: 60682 RVA: 0x0033450C File Offset: 0x0033270C
				public bool pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED0B RID: 60683 RVA: 0x00334530 File Offset: 0x00332730
				public bool pos(ProgramNode node, out pos value)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						value = default(pos);
						return false;
					}
					value = pos.Value;
					return true;
				}

				// Token: 0x0600ED0C RID: 60684 RVA: 0x0033456C File Offset: 0x0033276C
				public bool regexPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED0D RID: 60685 RVA: 0x00334590 File Offset: 0x00332790
				public bool regexPair(ProgramNode node, out regexPair value)
				{
					regexPair? regexPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair.CreateSafe(this._builders, node);
					if (regexPair == null)
					{
						value = default(regexPair);
						return false;
					}
					value = regexPair.Value;
					return true;
				}

				// Token: 0x0600ED0E RID: 60686 RVA: 0x003345CC File Offset: 0x003327CC
				public bool number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED0F RID: 60687 RVA: 0x003345F0 File Offset: 0x003327F0
				public bool number(ProgramNode node, out number value)
				{
					number? number = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number.CreateSafe(this._builders, node);
					if (number == null)
					{
						value = default(number);
						return false;
					}
					value = number.Value;
					return true;
				}

				// Token: 0x0600ED10 RID: 60688 RVA: 0x0033462C File Offset: 0x0033282C
				public bool castToNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED11 RID: 60689 RVA: 0x00334650 File Offset: 0x00332850
				public bool castToNumber(ProgramNode node, out castToNumber value)
				{
					castToNumber? castToNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber.CreateSafe(this._builders, node);
					if (castToNumber == null)
					{
						value = default(castToNumber);
						return false;
					}
					value = castToNumber.Value;
					return true;
				}

				// Token: 0x0600ED12 RID: 60690 RVA: 0x0033468C File Offset: 0x0033288C
				public bool inputNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED13 RID: 60691 RVA: 0x003346B0 File Offset: 0x003328B0
				public bool inputNumber(ProgramNode node, out inputNumber value)
				{
					inputNumber? inputNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber.CreateSafe(this._builders, node);
					if (inputNumber == null)
					{
						value = default(inputNumber);
						return false;
					}
					value = inputNumber.Value;
					return true;
				}

				// Token: 0x0600ED14 RID: 60692 RVA: 0x003346EC File Offset: 0x003328EC
				public bool parsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED15 RID: 60693 RVA: 0x00334710 File Offset: 0x00332910
				public bool parsedNumber(ProgramNode node, out parsedNumber value)
				{
					parsedNumber? parsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber.CreateSafe(this._builders, node);
					if (parsedNumber == null)
					{
						value = default(parsedNumber);
						return false;
					}
					value = parsedNumber.Value;
					return true;
				}

				// Token: 0x0600ED16 RID: 60694 RVA: 0x0033474C File Offset: 0x0033294C
				public bool b(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED17 RID: 60695 RVA: 0x00334770 File Offset: 0x00332970
				public bool b(ProgramNode node, out b value)
				{
					b? b = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b.CreateSafe(this._builders, node);
					if (b == null)
					{
						value = default(b);
						return false;
					}
					value = b.Value;
					return true;
				}

				// Token: 0x0600ED18 RID: 60696 RVA: 0x003347AC File Offset: 0x003329AC
				public bool cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED19 RID: 60697 RVA: 0x003347D0 File Offset: 0x003329D0
				public bool cs(ProgramNode node, out cs value)
				{
					cs? cs = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs.CreateSafe(this._builders, node);
					if (cs == null)
					{
						value = default(cs);
						return false;
					}
					value = cs.Value;
					return true;
				}

				// Token: 0x0600ED1A RID: 60698 RVA: 0x0033480C File Offset: 0x00332A0C
				public bool y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED1B RID: 60699 RVA: 0x00334830 File Offset: 0x00332A30
				public bool y(ProgramNode node, out y value)
				{
					y? y = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y.CreateSafe(this._builders, node);
					if (y == null)
					{
						value = default(y);
						return false;
					}
					value = y.Value;
					return true;
				}

				// Token: 0x0600ED1C RID: 60700 RVA: 0x0033486C File Offset: 0x00332A6C
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED1D RID: 60701 RVA: 0x00334890 File Offset: 0x00332A90
				public bool k(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k? k = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x0600ED1E RID: 60702 RVA: 0x003348CC File Offset: 0x00332ACC
				public bool externalExtractor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED1F RID: 60703 RVA: 0x003348F0 File Offset: 0x00332AF0
				public bool externalExtractor(ProgramNode node, out externalExtractor value)
				{
					externalExtractor? externalExtractor = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor.CreateSafe(this._builders, node);
					if (externalExtractor == null)
					{
						value = default(externalExtractor);
						return false;
					}
					value = externalExtractor.Value;
					return true;
				}

				// Token: 0x0600ED20 RID: 60704 RVA: 0x0033492C File Offset: 0x00332B2C
				public bool r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED21 RID: 60705 RVA: 0x00334950 File Offset: 0x00332B50
				public bool r(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r? r = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r);
						return false;
					}
					value = r.Value;
					return true;
				}

				// Token: 0x0600ED22 RID: 60706 RVA: 0x0033498C File Offset: 0x00332B8C
				public bool s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED23 RID: 60707 RVA: 0x003349B0 File Offset: 0x00332BB0
				public bool s(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s? s = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
					if (s == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s);
						return false;
					}
					value = s.Value;
					return true;
				}

				// Token: 0x0600ED24 RID: 60708 RVA: 0x003349EC File Offset: 0x00332BEC
				public bool name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED25 RID: 60709 RVA: 0x00334A10 File Offset: 0x00332C10
				public bool name(ProgramNode node, out name value)
				{
					name? name = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						value = default(name);
						return false;
					}
					value = name.Value;
					return true;
				}

				// Token: 0x0600ED26 RID: 60710 RVA: 0x00334A4C File Offset: 0x00332C4C
				public bool roundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED27 RID: 60711 RVA: 0x00334A70 File Offset: 0x00332C70
				public bool roundingSpec(ProgramNode node, out roundingSpec value)
				{
					roundingSpec? roundingSpec = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec.CreateSafe(this._builders, node);
					if (roundingSpec == null)
					{
						value = default(roundingSpec);
						return false;
					}
					value = roundingSpec.Value;
					return true;
				}

				// Token: 0x0600ED28 RID: 60712 RVA: 0x00334AAC File Offset: 0x00332CAC
				public bool dtRoundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED29 RID: 60713 RVA: 0x00334AD0 File Offset: 0x00332CD0
				public bool dtRoundingSpec(ProgramNode node, out dtRoundingSpec value)
				{
					dtRoundingSpec? dtRoundingSpec = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec.CreateSafe(this._builders, node);
					if (dtRoundingSpec == null)
					{
						value = default(dtRoundingSpec);
						return false;
					}
					value = dtRoundingSpec.Value;
					return true;
				}

				// Token: 0x0600ED2A RID: 60714 RVA: 0x00334B0C File Offset: 0x00332D0C
				public bool minTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED2B RID: 60715 RVA: 0x00334B30 File Offset: 0x00332D30
				public bool minTrailingZeros(ProgramNode node, out minTrailingZeros value)
				{
					minTrailingZeros? minTrailingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros.CreateSafe(this._builders, node);
					if (minTrailingZeros == null)
					{
						value = default(minTrailingZeros);
						return false;
					}
					value = minTrailingZeros.Value;
					return true;
				}

				// Token: 0x0600ED2C RID: 60716 RVA: 0x00334B6C File Offset: 0x00332D6C
				public bool maxTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED2D RID: 60717 RVA: 0x00334B90 File Offset: 0x00332D90
				public bool maxTrailingZeros(ProgramNode node, out maxTrailingZeros value)
				{
					maxTrailingZeros? maxTrailingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros.CreateSafe(this._builders, node);
					if (maxTrailingZeros == null)
					{
						value = default(maxTrailingZeros);
						return false;
					}
					value = maxTrailingZeros.Value;
					return true;
				}

				// Token: 0x0600ED2E RID: 60718 RVA: 0x00334BCC File Offset: 0x00332DCC
				public bool minTrailingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED2F RID: 60719 RVA: 0x00334BF0 File Offset: 0x00332DF0
				public bool minTrailingZerosAndWhitespace(ProgramNode node, out minTrailingZerosAndWhitespace value)
				{
					minTrailingZerosAndWhitespace? minTrailingZerosAndWhitespace = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace.CreateSafe(this._builders, node);
					if (minTrailingZerosAndWhitespace == null)
					{
						value = default(minTrailingZerosAndWhitespace);
						return false;
					}
					value = minTrailingZerosAndWhitespace.Value;
					return true;
				}

				// Token: 0x0600ED30 RID: 60720 RVA: 0x00334C2C File Offset: 0x00332E2C
				public bool minLeadingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED31 RID: 60721 RVA: 0x00334C50 File Offset: 0x00332E50
				public bool minLeadingZeros(ProgramNode node, out minLeadingZeros value)
				{
					minLeadingZeros? minLeadingZeros = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros.CreateSafe(this._builders, node);
					if (minLeadingZeros == null)
					{
						value = default(minLeadingZeros);
						return false;
					}
					value = minLeadingZeros.Value;
					return true;
				}

				// Token: 0x0600ED32 RID: 60722 RVA: 0x00334C8C File Offset: 0x00332E8C
				public bool minLeadingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED33 RID: 60723 RVA: 0x00334CB0 File Offset: 0x00332EB0
				public bool minLeadingZerosAndWhitespace(ProgramNode node, out minLeadingZerosAndWhitespace value)
				{
					minLeadingZerosAndWhitespace? minLeadingZerosAndWhitespace = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace.CreateSafe(this._builders, node);
					if (minLeadingZerosAndWhitespace == null)
					{
						value = default(minLeadingZerosAndWhitespace);
						return false;
					}
					value = minLeadingZerosAndWhitespace.Value;
					return true;
				}

				// Token: 0x0600ED34 RID: 60724 RVA: 0x00334CEC File Offset: 0x00332EEC
				public bool numberFormatSeparatorChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED35 RID: 60725 RVA: 0x00334D10 File Offset: 0x00332F10
				public bool numberFormatSeparatorChar(ProgramNode node, out numberFormatSeparatorChar value)
				{
					numberFormatSeparatorChar? numberFormatSeparatorChar = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar.CreateSafe(this._builders, node);
					if (numberFormatSeparatorChar == null)
					{
						value = default(numberFormatSeparatorChar);
						return false;
					}
					value = numberFormatSeparatorChar.Value;
					return true;
				}

				// Token: 0x0600ED36 RID: 60726 RVA: 0x00334D4C File Offset: 0x00332F4C
				public bool numberFormatDetails(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED37 RID: 60727 RVA: 0x00334D70 File Offset: 0x00332F70
				public bool numberFormatDetails(ProgramNode node, out numberFormatDetails value)
				{
					numberFormatDetails? numberFormatDetails = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails.CreateSafe(this._builders, node);
					if (numberFormatDetails == null)
					{
						value = default(numberFormatDetails);
						return false;
					}
					value = numberFormatDetails.Value;
					return true;
				}

				// Token: 0x0600ED38 RID: 60728 RVA: 0x00334DAC File Offset: 0x00332FAC
				public bool numberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED39 RID: 60729 RVA: 0x00334DD0 File Offset: 0x00332FD0
				public bool numberFormat(ProgramNode node, out numberFormat value)
				{
					numberFormat? numberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat.CreateSafe(this._builders, node);
					if (numberFormat == null)
					{
						value = default(numberFormat);
						return false;
					}
					value = numberFormat.Value;
					return true;
				}

				// Token: 0x0600ED3A RID: 60730 RVA: 0x00334E0C File Offset: 0x0033300C
				public bool numberFormatLiteral(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED3B RID: 60731 RVA: 0x00334E30 File Offset: 0x00333030
				public bool numberFormatLiteral(ProgramNode node, out numberFormatLiteral value)
				{
					numberFormatLiteral? numberFormatLiteral = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral.CreateSafe(this._builders, node);
					if (numberFormatLiteral == null)
					{
						value = default(numberFormatLiteral);
						return false;
					}
					value = numberFormatLiteral.Value;
					return true;
				}

				// Token: 0x0600ED3C RID: 60732 RVA: 0x00334E6C File Offset: 0x0033306C
				public bool outputDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED3D RID: 60733 RVA: 0x00334E90 File Offset: 0x00333090
				public bool outputDtFormat(ProgramNode node, out outputDtFormat value)
				{
					outputDtFormat? outputDtFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat.CreateSafe(this._builders, node);
					if (outputDtFormat == null)
					{
						value = default(outputDtFormat);
						return false;
					}
					value = outputDtFormat.Value;
					return true;
				}

				// Token: 0x0600ED3E RID: 60734 RVA: 0x00334ECC File Offset: 0x003330CC
				public bool inputDtFormats(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED3F RID: 60735 RVA: 0x00334EF0 File Offset: 0x003330F0
				public bool inputDtFormats(ProgramNode node, out inputDtFormats value)
				{
					inputDtFormats? inputDtFormats = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats.CreateSafe(this._builders, node);
					if (inputDtFormats == null)
					{
						value = default(inputDtFormats);
						return false;
					}
					value = inputDtFormats.Value;
					return true;
				}

				// Token: 0x0600ED40 RID: 60736 RVA: 0x00334F2C File Offset: 0x0033312C
				public bool lookupDictionary(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED41 RID: 60737 RVA: 0x00334F50 File Offset: 0x00333150
				public bool lookupDictionary(ProgramNode node, out lookupDictionary value)
				{
					lookupDictionary? lookupDictionary = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary.CreateSafe(this._builders, node);
					if (lookupDictionary == null)
					{
						value = default(lookupDictionary);
						return false;
					}
					value = lookupDictionary.Value;
					return true;
				}

				// Token: 0x0600ED42 RID: 60738 RVA: 0x00334F8C File Offset: 0x0033318C
				public bool idx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED43 RID: 60739 RVA: 0x00334FB0 File Offset: 0x003331B0
				public bool idx(ProgramNode node, out idx value)
				{
					idx? idx = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx.CreateSafe(this._builders, node);
					if (idx == null)
					{
						value = default(idx);
						return false;
					}
					value = idx.Value;
					return true;
				}

				// Token: 0x0600ED44 RID: 60740 RVA: 0x00334FEC File Offset: 0x003331EC
				public bool columnIdx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED45 RID: 60741 RVA: 0x00335010 File Offset: 0x00333210
				public bool columnIdx(ProgramNode node, out columnIdx value)
				{
					columnIdx? columnIdx = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx.CreateSafe(this._builders, node);
					if (columnIdx == null)
					{
						value = default(columnIdx);
						return false;
					}
					value = columnIdx.Value;
					return true;
				}

				// Token: 0x0600ED46 RID: 60742 RVA: 0x0033504C File Offset: 0x0033324C
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED47 RID: 60743 RVA: 0x00335070 File Offset: 0x00333270
				public bool _LetB0(ProgramNode node, out _LetB0 value)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED48 RID: 60744 RVA: 0x003350AC File Offset: 0x003332AC
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED49 RID: 60745 RVA: 0x003350D0 File Offset: 0x003332D0
				public bool _LetB1(ProgramNode node, out _LetB1 value)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED4A RID: 60746 RVA: 0x0033510C File Offset: 0x0033330C
				public bool _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED4B RID: 60747 RVA: 0x00335130 File Offset: 0x00333330
				public bool _LetB2(ProgramNode node, out _LetB2 value)
				{
					_LetB2? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB2);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED4C RID: 60748 RVA: 0x0033516C File Offset: 0x0033336C
				public bool _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED4D RID: 60749 RVA: 0x00335190 File Offset: 0x00333390
				public bool _LetB3(ProgramNode node, out _LetB3 value)
				{
					_LetB3? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB3);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED4E RID: 60750 RVA: 0x003351CC File Offset: 0x003333CC
				public bool _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED4F RID: 60751 RVA: 0x003351F0 File Offset: 0x003333F0
				public bool _LetB4(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4 value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED50 RID: 60752 RVA: 0x0033522C File Offset: 0x0033342C
				public bool _LetB5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED51 RID: 60753 RVA: 0x00335250 File Offset: 0x00333450
				public bool _LetB5(ProgramNode node, out _LetB5 value)
				{
					_LetB5? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB5);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED52 RID: 60754 RVA: 0x0033528C File Offset: 0x0033348C
				public bool _LetB6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED53 RID: 60755 RVA: 0x003352B0 File Offset: 0x003334B0
				public bool _LetB6(ProgramNode node, out _LetB6 value)
				{
					_LetB6? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB6);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600ED54 RID: 60756 RVA: 0x003352EC File Offset: 0x003334EC
				public bool _LetB7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED55 RID: 60757 RVA: 0x00335310 File Offset: 0x00333510
				public bool _LetB7(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7 value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x04005A3F RID: 23103
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE0 RID: 7136
			public class RuleIs
			{
				// Token: 0x0600ED56 RID: 60758 RVA: 0x0033534A File Offset: 0x0033354A
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600ED57 RID: 60759 RVA: 0x0033535C File Offset: 0x0033355C
				public bool SingleBranch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SingleBranch.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED58 RID: 60760 RVA: 0x00335380 File Offset: 0x00333580
				public bool SingleBranch(ProgramNode node, out SingleBranch value)
				{
					SingleBranch? singleBranch = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SingleBranch.CreateSafe(this._builders, node);
					if (singleBranch == null)
					{
						value = default(SingleBranch);
						return false;
					}
					value = singleBranch.Value;
					return true;
				}

				// Token: 0x0600ED59 RID: 60761 RVA: 0x003353BC File Offset: 0x003335BC
				public bool switch_ite(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.switch_ite.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED5A RID: 60762 RVA: 0x003353E0 File Offset: 0x003335E0
				public bool switch_ite(ProgramNode node, out switch_ite value)
				{
					switch_ite? switch_ite = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.switch_ite.CreateSafe(this._builders, node);
					if (switch_ite == null)
					{
						value = default(switch_ite);
						return false;
					}
					value = switch_ite.Value;
					return true;
				}

				// Token: 0x0600ED5B RID: 60763 RVA: 0x0033541C File Offset: 0x0033361C
				public bool IfThenElse(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED5C RID: 60764 RVA: 0x00335440 File Offset: 0x00333640
				public bool IfThenElse(ProgramNode node, out IfThenElse value)
				{
					IfThenElse? ifThenElse = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
					if (ifThenElse == null)
					{
						value = default(IfThenElse);
						return false;
					}
					value = ifThenElse.Value;
					return true;
				}

				// Token: 0x0600ED5D RID: 60765 RVA: 0x0033547C File Offset: 0x0033367C
				public bool Predicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Predicate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED5E RID: 60766 RVA: 0x003354A0 File Offset: 0x003336A0
				public bool Predicate(ProgramNode node, out Predicate value)
				{
					Predicate? predicate = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Predicate.CreateSafe(this._builders, node);
					if (predicate == null)
					{
						value = default(Predicate);
						return false;
					}
					value = predicate.Value;
					return true;
				}

				// Token: 0x0600ED5F RID: 60767 RVA: 0x003354DC File Offset: 0x003336DC
				public bool Transformation(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Transformation.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED60 RID: 60768 RVA: 0x00335500 File Offset: 0x00333700
				public bool Transformation(ProgramNode node, out Transformation value)
				{
					Transformation? transformation = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Transformation.CreateSafe(this._builders, node);
					if (transformation == null)
					{
						value = default(Transformation);
						return false;
					}
					value = transformation.Value;
					return true;
				}

				// Token: 0x0600ED61 RID: 60769 RVA: 0x0033553C File Offset: 0x0033373C
				public bool Atom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Atom.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED62 RID: 60770 RVA: 0x00335560 File Offset: 0x00333760
				public bool Atom(ProgramNode node, out Atom value)
				{
					Atom? atom = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Atom.CreateSafe(this._builders, node);
					if (atom == null)
					{
						value = default(Atom);
						return false;
					}
					value = atom.Value;
					return true;
				}

				// Token: 0x0600ED63 RID: 60771 RVA: 0x0033559C File Offset: 0x0033379C
				public bool Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED64 RID: 60772 RVA: 0x003355C0 File Offset: 0x003337C0
				public bool Concat(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat? concat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
					if (concat == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat);
						return false;
					}
					value = concat.Value;
					return true;
				}

				// Token: 0x0600ED65 RID: 60773 RVA: 0x003355FC File Offset: 0x003337FC
				public bool ConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED66 RID: 60774 RVA: 0x00335620 File Offset: 0x00333820
				public bool ConstStr(ProgramNode node, out ConstStr value)
				{
					ConstStr? constStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
					if (constStr == null)
					{
						value = default(ConstStr);
						return false;
					}
					value = constStr.Value;
					return true;
				}

				// Token: 0x0600ED67 RID: 60775 RVA: 0x0033565C File Offset: 0x0033385C
				public bool LetColumnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetColumnName.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED68 RID: 60776 RVA: 0x00335680 File Offset: 0x00333880
				public bool LetColumnName(ProgramNode node, out LetColumnName value)
				{
					LetColumnName? letColumnName = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetColumnName.CreateSafe(this._builders, node);
					if (letColumnName == null)
					{
						value = default(LetColumnName);
						return false;
					}
					value = letColumnName.Value;
					return true;
				}

				// Token: 0x0600ED69 RID: 60777 RVA: 0x003356BC File Offset: 0x003338BC
				public bool LetCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetCell.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED6A RID: 60778 RVA: 0x003356E0 File Offset: 0x003338E0
				public bool LetCell(ProgramNode node, out LetCell value)
				{
					LetCell? letCell = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetCell.CreateSafe(this._builders, node);
					if (letCell == null)
					{
						value = default(LetCell);
						return false;
					}
					value = letCell.Value;
					return true;
				}

				// Token: 0x0600ED6B RID: 60779 RVA: 0x0033571C File Offset: 0x0033391C
				public bool LetX(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED6C RID: 60780 RVA: 0x00335740 File Offset: 0x00333940
				public bool LetX(ProgramNode node, out LetX value)
				{
					LetX? letX = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
					if (letX == null)
					{
						value = default(LetX);
						return false;
					}
					value = letX.Value;
					return true;
				}

				// Token: 0x0600ED6D RID: 60781 RVA: 0x0033577C File Offset: 0x0033397C
				public bool ChooseInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ChooseInput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED6E RID: 60782 RVA: 0x003357A0 File Offset: 0x003339A0
				public bool ChooseInput(ProgramNode node, out ChooseInput value)
				{
					ChooseInput? chooseInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ChooseInput.CreateSafe(this._builders, node);
					if (chooseInput == null)
					{
						value = default(ChooseInput);
						return false;
					}
					value = chooseInput.Value;
					return true;
				}

				// Token: 0x0600ED6F RID: 60783 RVA: 0x003357DC File Offset: 0x003339DC
				public bool v_indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.v_indexInputString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED70 RID: 60784 RVA: 0x00335800 File Offset: 0x00333A00
				public bool v_indexInputString(ProgramNode node, out v_indexInputString value)
				{
					v_indexInputString? v_indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.v_indexInputString.CreateSafe(this._builders, node);
					if (v_indexInputString == null)
					{
						value = default(v_indexInputString);
						return false;
					}
					value = v_indexInputString.Value;
					return true;
				}

				// Token: 0x0600ED71 RID: 60785 RVA: 0x0033583C File Offset: 0x00333A3C
				public bool IndexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IndexInputString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED72 RID: 60786 RVA: 0x00335860 File Offset: 0x00333A60
				public bool IndexInputString(ProgramNode node, out IndexInputString value)
				{
					IndexInputString? indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IndexInputString.CreateSafe(this._builders, node);
					if (indexInputString == null)
					{
						value = default(IndexInputString);
						return false;
					}
					value = indexInputString.Value;
					return true;
				}

				// Token: 0x0600ED73 RID: 60787 RVA: 0x0033589C File Offset: 0x00333A9C
				public bool LookupInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LookupInput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED74 RID: 60788 RVA: 0x003358C0 File Offset: 0x00333AC0
				public bool LookupInput(ProgramNode node, out LookupInput value)
				{
					LookupInput? lookupInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LookupInput.CreateSafe(this._builders, node);
					if (lookupInput == null)
					{
						value = default(LookupInput);
						return false;
					}
					value = lookupInput.Value;
					return true;
				}

				// Token: 0x0600ED75 RID: 60789 RVA: 0x003358FC File Offset: 0x00333AFC
				public bool lookupInput_indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.lookupInput_indexInputString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED76 RID: 60790 RVA: 0x00335920 File Offset: 0x00333B20
				public bool lookupInput_indexInputString(ProgramNode node, out lookupInput_indexInputString value)
				{
					lookupInput_indexInputString? lookupInput_indexInputString = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.lookupInput_indexInputString.CreateSafe(this._builders, node);
					if (lookupInput_indexInputString == null)
					{
						value = default(lookupInput_indexInputString);
						return false;
					}
					value = lookupInput_indexInputString.Value;
					return true;
				}

				// Token: 0x0600ED77 RID: 60791 RVA: 0x0033595C File Offset: 0x00333B5C
				public bool LetSharedNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedNumberFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED78 RID: 60792 RVA: 0x00335980 File Offset: 0x00333B80
				public bool LetSharedNumberFormat(ProgramNode node, out LetSharedNumberFormat value)
				{
					LetSharedNumberFormat? letSharedNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedNumberFormat.CreateSafe(this._builders, node);
					if (letSharedNumberFormat == null)
					{
						value = default(LetSharedNumberFormat);
						return false;
					}
					value = letSharedNumberFormat.Value;
					return true;
				}

				// Token: 0x0600ED79 RID: 60793 RVA: 0x003359BC File Offset: 0x00333BBC
				public bool LetSharedDateTimeFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedDateTimeFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED7A RID: 60794 RVA: 0x003359E0 File Offset: 0x00333BE0
				public bool LetSharedDateTimeFormat(ProgramNode node, out LetSharedDateTimeFormat value)
				{
					LetSharedDateTimeFormat? letSharedDateTimeFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedDateTimeFormat.CreateSafe(this._builders, node);
					if (letSharedDateTimeFormat == null)
					{
						value = default(LetSharedDateTimeFormat);
						return false;
					}
					value = letSharedDateTimeFormat.Value;
					return true;
				}

				// Token: 0x0600ED7B RID: 60795 RVA: 0x00335A1C File Offset: 0x00333C1C
				public bool SubString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED7C RID: 60796 RVA: 0x00335A40 File Offset: 0x00333C40
				public bool SubString(ProgramNode node, out SubString value)
				{
					SubString? subString = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubString.CreateSafe(this._builders, node);
					if (subString == null)
					{
						value = default(SubString);
						return false;
					}
					value = subString.Value;
					return true;
				}

				// Token: 0x0600ED7D RID: 60797 RVA: 0x00335A7C File Offset: 0x00333C7C
				public bool ToLowercase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToLowercase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED7E RID: 60798 RVA: 0x00335AA0 File Offset: 0x00333CA0
				public bool ToLowercase(ProgramNode node, out ToLowercase value)
				{
					ToLowercase? toLowercase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToLowercase.CreateSafe(this._builders, node);
					if (toLowercase == null)
					{
						value = default(ToLowercase);
						return false;
					}
					value = toLowercase.Value;
					return true;
				}

				// Token: 0x0600ED7F RID: 60799 RVA: 0x00335ADC File Offset: 0x00333CDC
				public bool ToUppercase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToUppercase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED80 RID: 60800 RVA: 0x00335B00 File Offset: 0x00333D00
				public bool ToUppercase(ProgramNode node, out ToUppercase value)
				{
					ToUppercase? toUppercase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToUppercase.CreateSafe(this._builders, node);
					if (toUppercase == null)
					{
						value = default(ToUppercase);
						return false;
					}
					value = toUppercase.Value;
					return true;
				}

				// Token: 0x0600ED81 RID: 60801 RVA: 0x00335B3C File Offset: 0x00333D3C
				public bool ToSimpleTitleCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToSimpleTitleCase.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED82 RID: 60802 RVA: 0x00335B60 File Offset: 0x00333D60
				public bool ToSimpleTitleCase(ProgramNode node, out ToSimpleTitleCase value)
				{
					ToSimpleTitleCase? toSimpleTitleCase = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToSimpleTitleCase.CreateSafe(this._builders, node);
					if (toSimpleTitleCase == null)
					{
						value = default(ToSimpleTitleCase);
						return false;
					}
					value = toSimpleTitleCase.Value;
					return true;
				}

				// Token: 0x0600ED83 RID: 60803 RVA: 0x00335B9C File Offset: 0x00333D9C
				public bool FormatPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatPartialDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED84 RID: 60804 RVA: 0x00335BC0 File Offset: 0x00333DC0
				public bool FormatPartialDateTime(ProgramNode node, out FormatPartialDateTime value)
				{
					FormatPartialDateTime? formatPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatPartialDateTime.CreateSafe(this._builders, node);
					if (formatPartialDateTime == null)
					{
						value = default(FormatPartialDateTime);
						return false;
					}
					value = formatPartialDateTime.Value;
					return true;
				}

				// Token: 0x0600ED85 RID: 60805 RVA: 0x00335BFC File Offset: 0x00333DFC
				public bool FormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED86 RID: 60806 RVA: 0x00335C20 File Offset: 0x00333E20
				public bool FormatNumber(ProgramNode node, out FormatNumber value)
				{
					FormatNumber? formatNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
					if (formatNumber == null)
					{
						value = default(FormatNumber);
						return false;
					}
					value = formatNumber.Value;
					return true;
				}

				// Token: 0x0600ED87 RID: 60807 RVA: 0x00335C5C File Offset: 0x00333E5C
				public bool Lookup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Lookup.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED88 RID: 60808 RVA: 0x00335C80 File Offset: 0x00333E80
				public bool Lookup(ProgramNode node, out Lookup value)
				{
					Lookup? lookup = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Lookup.CreateSafe(this._builders, node);
					if (lookup == null)
					{
						value = default(Lookup);
						return false;
					}
					value = lookup.Value;
					return true;
				}

				// Token: 0x0600ED89 RID: 60809 RVA: 0x00335CBC File Offset: 0x00333EBC
				public bool FormatNumericRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumericRange.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED8A RID: 60810 RVA: 0x00335CE0 File Offset: 0x00333EE0
				public bool FormatNumericRange(ProgramNode node, out FormatNumericRange value)
				{
					FormatNumericRange? formatNumericRange = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumericRange.CreateSafe(this._builders, node);
					if (formatNumericRange == null)
					{
						value = default(FormatNumericRange);
						return false;
					}
					value = formatNumericRange.Value;
					return true;
				}

				// Token: 0x0600ED8B RID: 60811 RVA: 0x00335D1C File Offset: 0x00333F1C
				public bool FormatDateTimeRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED8C RID: 60812 RVA: 0x00335D40 File Offset: 0x00333F40
				public bool FormatDateTimeRange(ProgramNode node, out FormatDateTimeRange value)
				{
					FormatDateTimeRange? formatDateTimeRange = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange.CreateSafe(this._builders, node);
					if (formatDateTimeRange == null)
					{
						value = default(FormatDateTimeRange);
						return false;
					}
					value = formatDateTimeRange.Value;
					return true;
				}

				// Token: 0x0600ED8D RID: 60813 RVA: 0x00335D7C File Offset: 0x00333F7C
				public bool LetSharedParsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED8E RID: 60814 RVA: 0x00335DA0 File Offset: 0x00333FA0
				public bool LetSharedParsedNumber(ProgramNode node, out LetSharedParsedNumber value)
				{
					LetSharedParsedNumber? letSharedParsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedNumber.CreateSafe(this._builders, node);
					if (letSharedParsedNumber == null)
					{
						value = default(LetSharedParsedNumber);
						return false;
					}
					value = letSharedParsedNumber.Value;
					return true;
				}

				// Token: 0x0600ED8F RID: 60815 RVA: 0x00335DDC File Offset: 0x00333FDC
				public bool LetSharedParsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED90 RID: 60816 RVA: 0x00335E00 File Offset: 0x00334000
				public bool LetSharedParsedDateTime(ProgramNode node, out LetSharedParsedDateTime value)
				{
					LetSharedParsedDateTime? letSharedParsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedDateTime.CreateSafe(this._builders, node);
					if (letSharedParsedDateTime == null)
					{
						value = default(LetSharedParsedDateTime);
						return false;
					}
					value = letSharedParsedDateTime.Value;
					return true;
				}

				// Token: 0x0600ED91 RID: 60817 RVA: 0x00335E3C File Offset: 0x0033403C
				public bool rangeString_rangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.rangeString_rangeSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED92 RID: 60818 RVA: 0x00335E60 File Offset: 0x00334060
				public bool rangeString_rangeSubstring(ProgramNode node, out rangeString_rangeSubstring value)
				{
					rangeString_rangeSubstring? rangeString_rangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.rangeString_rangeSubstring.CreateSafe(this._builders, node);
					if (rangeString_rangeSubstring == null)
					{
						value = default(rangeString_rangeSubstring);
						return false;
					}
					value = rangeString_rangeSubstring.Value;
					return true;
				}

				// Token: 0x0600ED93 RID: 60819 RVA: 0x00335E9C File Offset: 0x0033409C
				public bool RangeConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConcat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED94 RID: 60820 RVA: 0x00335EC0 File Offset: 0x003340C0
				public bool RangeConcat(ProgramNode node, out RangeConcat value)
				{
					RangeConcat? rangeConcat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConcat.CreateSafe(this._builders, node);
					if (rangeConcat == null)
					{
						value = default(RangeConcat);
						return false;
					}
					value = rangeConcat.Value;
					return true;
				}

				// Token: 0x0600ED95 RID: 60821 RVA: 0x00335EFC File Offset: 0x003340FC
				public bool RangeConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConstStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED96 RID: 60822 RVA: 0x00335F20 File Offset: 0x00334120
				public bool RangeConstStr(ProgramNode node, out RangeConstStr value)
				{
					RangeConstStr? rangeConstStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConstStr.CreateSafe(this._builders, node);
					if (rangeConstStr == null)
					{
						value = default(RangeConstStr);
						return false;
					}
					value = rangeConstStr.Value;
					return true;
				}

				// Token: 0x0600ED97 RID: 60823 RVA: 0x00335F5C File Offset: 0x0033415C
				public bool RangeFormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED98 RID: 60824 RVA: 0x00335F80 File Offset: 0x00334180
				public bool RangeFormatNumber(ProgramNode node, out RangeFormatNumber value)
				{
					RangeFormatNumber? rangeFormatNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatNumber.CreateSafe(this._builders, node);
					if (rangeFormatNumber == null)
					{
						value = default(RangeFormatNumber);
						return false;
					}
					value = rangeFormatNumber.Value;
					return true;
				}

				// Token: 0x0600ED99 RID: 60825 RVA: 0x00335FBC File Offset: 0x003341BC
				public bool RangeRoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED9A RID: 60826 RVA: 0x00335FE0 File Offset: 0x003341E0
				public bool RangeRoundNumber(ProgramNode node, out RangeRoundNumber value)
				{
					RangeRoundNumber? rangeRoundNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundNumber.CreateSafe(this._builders, node);
					if (rangeRoundNumber == null)
					{
						value = default(RangeRoundNumber);
						return false;
					}
					value = rangeRoundNumber.Value;
					return true;
				}

				// Token: 0x0600ED9B RID: 60827 RVA: 0x0033601C File Offset: 0x0033421C
				public bool dtRangeString_dtRangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.dtRangeString_dtRangeSubstring.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED9C RID: 60828 RVA: 0x00336040 File Offset: 0x00334240
				public bool dtRangeString_dtRangeSubstring(ProgramNode node, out dtRangeString_dtRangeSubstring value)
				{
					dtRangeString_dtRangeSubstring? dtRangeString_dtRangeSubstring = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.dtRangeString_dtRangeSubstring.CreateSafe(this._builders, node);
					if (dtRangeString_dtRangeSubstring == null)
					{
						value = default(dtRangeString_dtRangeSubstring);
						return false;
					}
					value = dtRangeString_dtRangeSubstring.Value;
					return true;
				}

				// Token: 0x0600ED9D RID: 60829 RVA: 0x0033607C File Offset: 0x0033427C
				public bool DtRangeConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConcat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600ED9E RID: 60830 RVA: 0x003360A0 File Offset: 0x003342A0
				public bool DtRangeConcat(ProgramNode node, out DtRangeConcat value)
				{
					DtRangeConcat? dtRangeConcat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConcat.CreateSafe(this._builders, node);
					if (dtRangeConcat == null)
					{
						value = default(DtRangeConcat);
						return false;
					}
					value = dtRangeConcat.Value;
					return true;
				}

				// Token: 0x0600ED9F RID: 60831 RVA: 0x003360DC File Offset: 0x003342DC
				public bool DtRangeConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConstStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDA0 RID: 60832 RVA: 0x00336100 File Offset: 0x00334300
				public bool DtRangeConstStr(ProgramNode node, out DtRangeConstStr value)
				{
					DtRangeConstStr? dtRangeConstStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConstStr.CreateSafe(this._builders, node);
					if (dtRangeConstStr == null)
					{
						value = default(DtRangeConstStr);
						return false;
					}
					value = dtRangeConstStr.Value;
					return true;
				}

				// Token: 0x0600EDA1 RID: 60833 RVA: 0x0033613C File Offset: 0x0033433C
				public bool RangeFormatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDA2 RID: 60834 RVA: 0x00336160 File Offset: 0x00334360
				public bool RangeFormatDateTime(ProgramNode node, out RangeFormatDateTime value)
				{
					RangeFormatDateTime? rangeFormatDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatDateTime.CreateSafe(this._builders, node);
					if (rangeFormatDateTime == null)
					{
						value = default(RangeFormatDateTime);
						return false;
					}
					value = rangeFormatDateTime.Value;
					return true;
				}

				// Token: 0x0600EDA3 RID: 60835 RVA: 0x0033619C File Offset: 0x0033439C
				public bool RangeRoundDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDA4 RID: 60836 RVA: 0x003361C0 File Offset: 0x003343C0
				public bool RangeRoundDateTime(ProgramNode node, out RangeRoundDateTime value)
				{
					RangeRoundDateTime? rangeRoundDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundDateTime.CreateSafe(this._builders, node);
					if (rangeRoundDateTime == null)
					{
						value = default(RangeRoundDateTime);
						return false;
					}
					value = rangeRoundDateTime.Value;
					return true;
				}

				// Token: 0x0600EDA5 RID: 60837 RVA: 0x003361FC File Offset: 0x003343FC
				public bool datetime_inputDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.datetime_inputDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDA6 RID: 60838 RVA: 0x00336220 File Offset: 0x00334420
				public bool datetime_inputDateTime(ProgramNode node, out datetime_inputDateTime value)
				{
					datetime_inputDateTime? datetime_inputDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.datetime_inputDateTime.CreateSafe(this._builders, node);
					if (datetime_inputDateTime == null)
					{
						value = default(datetime_inputDateTime);
						return false;
					}
					value = datetime_inputDateTime.Value;
					return true;
				}

				// Token: 0x0600EDA7 RID: 60839 RVA: 0x0033625C File Offset: 0x0033445C
				public bool RoundPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundPartialDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDA8 RID: 60840 RVA: 0x00336280 File Offset: 0x00334480
				public bool RoundPartialDateTime(ProgramNode node, out RoundPartialDateTime value)
				{
					RoundPartialDateTime? roundPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundPartialDateTime.CreateSafe(this._builders, node);
					if (roundPartialDateTime == null)
					{
						value = default(RoundPartialDateTime);
						return false;
					}
					value = roundPartialDateTime.Value;
					return true;
				}

				// Token: 0x0600EDA9 RID: 60841 RVA: 0x003362BC File Offset: 0x003344BC
				public bool AsPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsPartialDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDAA RID: 60842 RVA: 0x003362E0 File Offset: 0x003344E0
				public bool AsPartialDateTime(ProgramNode node, out AsPartialDateTime value)
				{
					AsPartialDateTime? asPartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsPartialDateTime.CreateSafe(this._builders, node);
					if (asPartialDateTime == null)
					{
						value = default(AsPartialDateTime);
						return false;
					}
					value = asPartialDateTime.Value;
					return true;
				}

				// Token: 0x0600EDAB RID: 60843 RVA: 0x0033631C File Offset: 0x0033451C
				public bool inputDateTime_parsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputDateTime_parsedDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDAC RID: 60844 RVA: 0x00336340 File Offset: 0x00334540
				public bool inputDateTime_parsedDateTime(ProgramNode node, out inputDateTime_parsedDateTime value)
				{
					inputDateTime_parsedDateTime? inputDateTime_parsedDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputDateTime_parsedDateTime.CreateSafe(this._builders, node);
					if (inputDateTime_parsedDateTime == null)
					{
						value = default(inputDateTime_parsedDateTime);
						return false;
					}
					value = inputDateTime_parsedDateTime.Value;
					return true;
				}

				// Token: 0x0600EDAD RID: 60845 RVA: 0x0033637C File Offset: 0x0033457C
				public bool ParsePartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParsePartialDateTime.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDAE RID: 60846 RVA: 0x003363A0 File Offset: 0x003345A0
				public bool ParsePartialDateTime(ProgramNode node, out ParsePartialDateTime value)
				{
					ParsePartialDateTime? parsePartialDateTime = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParsePartialDateTime.CreateSafe(this._builders, node);
					if (parsePartialDateTime == null)
					{
						value = default(ParsePartialDateTime);
						return false;
					}
					value = parsePartialDateTime.Value;
					return true;
				}

				// Token: 0x0600EDAF RID: 60847 RVA: 0x003363DC File Offset: 0x003345DC
				public bool WholeColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.WholeColumn.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDB0 RID: 60848 RVA: 0x00336400 File Offset: 0x00334600
				public bool WholeColumn(ProgramNode node, out WholeColumn value)
				{
					WholeColumn? wholeColumn = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.WholeColumn.CreateSafe(this._builders, node);
					if (wholeColumn == null)
					{
						value = default(WholeColumn);
						return false;
					}
					value = wholeColumn.Value;
					return true;
				}

				// Token: 0x0600EDB1 RID: 60849 RVA: 0x0033643C File Offset: 0x0033463C
				public bool SubStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDB2 RID: 60850 RVA: 0x00336460 File Offset: 0x00334660
				public bool SubStr(ProgramNode node, out SubStr value)
				{
					SubStr? subStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubStr.CreateSafe(this._builders, node);
					if (subStr == null)
					{
						value = default(SubStr);
						return false;
					}
					value = subStr.Value;
					return true;
				}

				// Token: 0x0600EDB3 RID: 60851 RVA: 0x0033649C File Offset: 0x0033469C
				public bool Add(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDB4 RID: 60852 RVA: 0x003364C0 File Offset: 0x003346C0
				public bool Add(ProgramNode node, out Add value)
				{
					Add? add = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
					if (add == null)
					{
						value = default(Add);
						return false;
					}
					value = add.Value;
					return true;
				}

				// Token: 0x0600EDB5 RID: 60853 RVA: 0x003364FC File Offset: 0x003346FC
				public bool PosPairRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPairRelative.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDB6 RID: 60854 RVA: 0x00336520 File Offset: 0x00334720
				public bool PosPairRelative(ProgramNode node, out PosPairRelative value)
				{
					PosPairRelative? posPairRelative = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPairRelative.CreateSafe(this._builders, node);
					if (posPairRelative == null)
					{
						value = default(PosPairRelative);
						return false;
					}
					value = posPairRelative.Value;
					return true;
				}

				// Token: 0x0600EDB7 RID: 60855 RVA: 0x0033655C File Offset: 0x0033475C
				public bool _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDB8 RID: 60856 RVA: 0x00336580 File Offset: 0x00334780
				public bool _LetB4(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4 value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600EDB9 RID: 60857 RVA: 0x003365BC File Offset: 0x003347BC
				public bool RSubStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RSubStr.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDBA RID: 60858 RVA: 0x003365E0 File Offset: 0x003347E0
				public bool RSubStr(ProgramNode node, out RSubStr value)
				{
					RSubStr? rsubStr = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RSubStr.CreateSafe(this._builders, node);
					if (rsubStr == null)
					{
						value = default(RSubStr);
						return false;
					}
					value = rsubStr.Value;
					return true;
				}

				// Token: 0x0600EDBB RID: 60859 RVA: 0x0033661C File Offset: 0x0033481C
				public bool LetPL2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL2.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDBC RID: 60860 RVA: 0x00336640 File Offset: 0x00334840
				public bool LetPL2(ProgramNode node, out LetPL2 value)
				{
					LetPL2? letPL = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL2.CreateSafe(this._builders, node);
					if (letPL == null)
					{
						value = default(LetPL2);
						return false;
					}
					value = letPL.Value;
					return true;
				}

				// Token: 0x0600EDBD RID: 60861 RVA: 0x0033667C File Offset: 0x0033487C
				public bool _LetB7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDBE RID: 60862 RVA: 0x003366A0 File Offset: 0x003348A0
				public bool _LetB7(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7 value)
				{
					Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7? letB = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x0600EDBF RID: 60863 RVA: 0x003366DC File Offset: 0x003348DC
				public bool PosPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDC0 RID: 60864 RVA: 0x00336700 File Offset: 0x00334900
				public bool PosPair(ProgramNode node, out PosPair value)
				{
					PosPair? posPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPair.CreateSafe(this._builders, node);
					if (posPair == null)
					{
						value = default(PosPair);
						return false;
					}
					value = posPair.Value;
					return true;
				}

				// Token: 0x0600EDC1 RID: 60865 RVA: 0x0033673C File Offset: 0x0033493C
				public bool LetPL1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDC2 RID: 60866 RVA: 0x00336760 File Offset: 0x00334960
				public bool LetPL1(ProgramNode node, out LetPL1 value)
				{
					LetPL1? letPL = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL1.CreateSafe(this._builders, node);
					if (letPL == null)
					{
						value = default(LetPL1);
						return false;
					}
					value = letPL.Value;
					return true;
				}

				// Token: 0x0600EDC3 RID: 60867 RVA: 0x0033679C File Offset: 0x0033499C
				public bool RegexPositionPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionPair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDC4 RID: 60868 RVA: 0x003367C0 File Offset: 0x003349C0
				public bool RegexPositionPair(ProgramNode node, out RegexPositionPair value)
				{
					RegexPositionPair? regexPositionPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionPair.CreateSafe(this._builders, node);
					if (regexPositionPair == null)
					{
						value = default(RegexPositionPair);
						return false;
					}
					value = regexPositionPair.Value;
					return true;
				}

				// Token: 0x0600EDC5 RID: 60869 RVA: 0x003367FC File Offset: 0x003349FC
				public bool ExternalExtractorPositionPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ExternalExtractorPositionPair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDC6 RID: 60870 RVA: 0x00336820 File Offset: 0x00334A20
				public bool ExternalExtractorPositionPair(ProgramNode node, out ExternalExtractorPositionPair value)
				{
					ExternalExtractorPositionPair? externalExtractorPositionPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ExternalExtractorPositionPair.CreateSafe(this._builders, node);
					if (externalExtractorPositionPair == null)
					{
						value = default(ExternalExtractorPositionPair);
						return false;
					}
					value = externalExtractorPositionPair.Value;
					return true;
				}

				// Token: 0x0600EDC7 RID: 60871 RVA: 0x0033685C File Offset: 0x00334A5C
				public bool RelativePosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RelativePosition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDC8 RID: 60872 RVA: 0x00336880 File Offset: 0x00334A80
				public bool RelativePosition(ProgramNode node, out RelativePosition value)
				{
					RelativePosition? relativePosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RelativePosition.CreateSafe(this._builders, node);
					if (relativePosition == null)
					{
						value = default(RelativePosition);
						return false;
					}
					value = relativePosition.Value;
					return true;
				}

				// Token: 0x0600EDC9 RID: 60873 RVA: 0x003368BC File Offset: 0x00334ABC
				public bool RegexPositionRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionRelative.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDCA RID: 60874 RVA: 0x003368E0 File Offset: 0x00334AE0
				public bool RegexPositionRelative(ProgramNode node, out RegexPositionRelative value)
				{
					RegexPositionRelative? regexPositionRelative = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionRelative.CreateSafe(this._builders, node);
					if (regexPositionRelative == null)
					{
						value = default(RegexPositionRelative);
						return false;
					}
					value = regexPositionRelative.Value;
					return true;
				}

				// Token: 0x0600EDCB RID: 60875 RVA: 0x0033691C File Offset: 0x00334B1C
				public bool AbsolutePosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AbsolutePosition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDCC RID: 60876 RVA: 0x00336940 File Offset: 0x00334B40
				public bool AbsolutePosition(ProgramNode node, out AbsolutePosition value)
				{
					AbsolutePosition? absolutePosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AbsolutePosition.CreateSafe(this._builders, node);
					if (absolutePosition == null)
					{
						value = default(AbsolutePosition);
						return false;
					}
					value = absolutePosition.Value;
					return true;
				}

				// Token: 0x0600EDCD RID: 60877 RVA: 0x0033697C File Offset: 0x00334B7C
				public bool RegexPosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPosition.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDCE RID: 60878 RVA: 0x003369A0 File Offset: 0x00334BA0
				public bool RegexPosition(ProgramNode node, out RegexPosition value)
				{
					RegexPosition? regexPosition = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPosition.CreateSafe(this._builders, node);
					if (regexPosition == null)
					{
						value = default(RegexPosition);
						return false;
					}
					value = regexPosition.Value;
					return true;
				}

				// Token: 0x0600EDCF RID: 60879 RVA: 0x003369DC File Offset: 0x00334BDC
				public bool RegexPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPair.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDD0 RID: 60880 RVA: 0x00336A00 File Offset: 0x00334C00
				public bool RegexPair(ProgramNode node, out RegexPair value)
				{
					RegexPair? regexPair = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPair.CreateSafe(this._builders, node);
					if (regexPair == null)
					{
						value = default(RegexPair);
						return false;
					}
					value = regexPair.Value;
					return true;
				}

				// Token: 0x0600EDD1 RID: 60881 RVA: 0x00336A3C File Offset: 0x00334C3C
				public bool number_inputNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.number_inputNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDD2 RID: 60882 RVA: 0x00336A60 File Offset: 0x00334C60
				public bool number_inputNumber(ProgramNode node, out number_inputNumber value)
				{
					number_inputNumber? number_inputNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.number_inputNumber.CreateSafe(this._builders, node);
					if (number_inputNumber == null)
					{
						value = default(number_inputNumber);
						return false;
					}
					value = number_inputNumber.Value;
					return true;
				}

				// Token: 0x0600EDD3 RID: 60883 RVA: 0x00336A9C File Offset: 0x00334C9C
				public bool RoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDD4 RID: 60884 RVA: 0x00336AC0 File Offset: 0x00334CC0
				public bool RoundNumber(ProgramNode node, out RoundNumber value)
				{
					RoundNumber? roundNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
					if (roundNumber == null)
					{
						value = default(RoundNumber);
						return false;
					}
					value = roundNumber.Value;
					return true;
				}

				// Token: 0x0600EDD5 RID: 60885 RVA: 0x00336AFC File Offset: 0x00334CFC
				public bool AsDecimal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsDecimal.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDD6 RID: 60886 RVA: 0x00336B20 File Offset: 0x00334D20
				public bool AsDecimal(ProgramNode node, out AsDecimal value)
				{
					AsDecimal? asDecimal = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsDecimal.CreateSafe(this._builders, node);
					if (asDecimal == null)
					{
						value = default(AsDecimal);
						return false;
					}
					value = asDecimal.Value;
					return true;
				}

				// Token: 0x0600EDD7 RID: 60887 RVA: 0x00336B5C File Offset: 0x00334D5C
				public bool inputNumber_castToNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_castToNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDD8 RID: 60888 RVA: 0x00336B80 File Offset: 0x00334D80
				public bool inputNumber_castToNumber(ProgramNode node, out inputNumber_castToNumber value)
				{
					inputNumber_castToNumber? inputNumber_castToNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_castToNumber.CreateSafe(this._builders, node);
					if (inputNumber_castToNumber == null)
					{
						value = default(inputNumber_castToNumber);
						return false;
					}
					value = inputNumber_castToNumber.Value;
					return true;
				}

				// Token: 0x0600EDD9 RID: 60889 RVA: 0x00336BBC File Offset: 0x00334DBC
				public bool inputNumber_parsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_parsedNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDDA RID: 60890 RVA: 0x00336BE0 File Offset: 0x00334DE0
				public bool inputNumber_parsedNumber(ProgramNode node, out inputNumber_parsedNumber value)
				{
					inputNumber_parsedNumber? inputNumber_parsedNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_parsedNumber.CreateSafe(this._builders, node);
					if (inputNumber_parsedNumber == null)
					{
						value = default(inputNumber_parsedNumber);
						return false;
					}
					value = inputNumber_parsedNumber.Value;
					return true;
				}

				// Token: 0x0600EDDB RID: 60891 RVA: 0x00336C1C File Offset: 0x00334E1C
				public bool ParseNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDDC RID: 60892 RVA: 0x00336C40 File Offset: 0x00334E40
				public bool ParseNumber(ProgramNode node, out ParseNumber value)
				{
					ParseNumber? parseNumber = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
					if (parseNumber == null)
					{
						value = default(ParseNumber);
						return false;
					}
					value = parseNumber.Value;
					return true;
				}

				// Token: 0x0600EDDD RID: 60893 RVA: 0x00336C7C File Offset: 0x00334E7C
				public bool LetPredicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPredicate.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDDE RID: 60894 RVA: 0x00336CA0 File Offset: 0x00334EA0
				public bool LetPredicate(ProgramNode node, out LetPredicate value)
				{
					LetPredicate? letPredicate = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPredicate.CreateSafe(this._builders, node);
					if (letPredicate == null)
					{
						value = default(LetPredicate);
						return false;
					}
					value = letPredicate.Value;
					return true;
				}

				// Token: 0x0600EDDF RID: 60895 RVA: 0x00336CDC File Offset: 0x00334EDC
				public bool SelectInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectInput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDE0 RID: 60896 RVA: 0x00336D00 File Offset: 0x00334F00
				public bool SelectInput(ProgramNode node, out SelectInput value)
				{
					SelectInput? selectInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectInput.CreateSafe(this._builders, node);
					if (selectInput == null)
					{
						value = default(SelectInput);
						return false;
					}
					value = selectInput.Value;
					return true;
				}

				// Token: 0x0600EDE1 RID: 60897 RVA: 0x00336D3C File Offset: 0x00334F3C
				public bool SelectIndexedInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectIndexedInput.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDE2 RID: 60898 RVA: 0x00336D60 File Offset: 0x00334F60
				public bool SelectIndexedInput(ProgramNode node, out SelectIndexedInput value)
				{
					SelectIndexedInput? selectIndexedInput = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectIndexedInput.CreateSafe(this._builders, node);
					if (selectIndexedInput == null)
					{
						value = default(SelectIndexedInput);
						return false;
					}
					value = selectIndexedInput.Value;
					return true;
				}

				// Token: 0x0600EDE3 RID: 60899 RVA: 0x00336D9C File Offset: 0x00334F9C
				public bool BuildNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.BuildNumberFormat.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDE4 RID: 60900 RVA: 0x00336DC0 File Offset: 0x00334FC0
				public bool BuildNumberFormat(ProgramNode node, out BuildNumberFormat value)
				{
					BuildNumberFormat? buildNumberFormat = Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.BuildNumberFormat.CreateSafe(this._builders, node);
					if (buildNumberFormat == null)
					{
						value = default(BuildNumberFormat);
						return false;
					}
					value = buildNumberFormat.Value;
					return true;
				}

				// Token: 0x0600EDE5 RID: 60901 RVA: 0x00336DFC File Offset: 0x00334FFC
				public bool numberFormat_numberFormatLiteral(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.numberFormat_numberFormatLiteral.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600EDE6 RID: 60902 RVA: 0x00336E20 File Offset: 0x00335020
				public bool numberFormat_numberFormatLiteral(ProgramNode node, out numberFormat_numberFormatLiteral value)
				{
					numberFormat_numberFormatLiteral? numberFormat_numberFormatLiteral = Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.numberFormat_numberFormatLiteral.CreateSafe(this._builders, node);
					if (numberFormat_numberFormatLiteral == null)
					{
						value = default(numberFormat_numberFormatLiteral);
						return false;
					}
					value = numberFormat_numberFormatLiteral.Value;
					return true;
				}

				// Token: 0x04005A40 RID: 23104
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE1 RID: 7137
			public class NodeAs
			{
				// Token: 0x0600EDE7 RID: 60903 RVA: 0x00336E5A File Offset: 0x0033505A
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EDE8 RID: 60904 RVA: 0x00336E69 File Offset: 0x00335069
				public @switch? @switch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDE9 RID: 60905 RVA: 0x00336E77 File Offset: 0x00335077
				public ite? ite(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDEA RID: 60906 RVA: 0x00336E85 File Offset: 0x00335085
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred? pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDEB RID: 60907 RVA: 0x00336E93 File Offset: 0x00335093
				public st? st(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDEC RID: 60908 RVA: 0x00336EA1 File Offset: 0x003350A1
				public e? e(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDED RID: 60909 RVA: 0x00336EAF File Offset: 0x003350AF
				public f? f(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDEE RID: 60910 RVA: 0x00336EBD File Offset: 0x003350BD
				public columnName? columnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDEF RID: 60911 RVA: 0x00336ECB File Offset: 0x003350CB
				public letOptions? letOptions(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF0 RID: 60912 RVA: 0x00336ED9 File Offset: 0x003350D9
				public cell? cell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF1 RID: 60913 RVA: 0x00336EE7 File Offset: 0x003350E7
				public x? x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF2 RID: 60914 RVA: 0x00336EF5 File Offset: 0x003350F5
				public v? v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF3 RID: 60915 RVA: 0x00336F03 File Offset: 0x00335103
				public indexInputString? indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF4 RID: 60916 RVA: 0x00336F11 File Offset: 0x00335111
				public lookupInput? lookupInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF5 RID: 60917 RVA: 0x00336F1F File Offset: 0x0033511F
				public conv? conv(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF6 RID: 60918 RVA: 0x00336F2D File Offset: 0x0033512D
				public sharedParsedNumber? sharedParsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF7 RID: 60919 RVA: 0x00336F3B File Offset: 0x0033513B
				public sharedNumberFormat? sharedNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF8 RID: 60920 RVA: 0x00336F49 File Offset: 0x00335149
				public sharedParsedDt? sharedParsedDt(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDF9 RID: 60921 RVA: 0x00336F57 File Offset: 0x00335157
				public sharedDtFormat? sharedDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFA RID: 60922 RVA: 0x00336F65 File Offset: 0x00335165
				public rangeString? rangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFB RID: 60923 RVA: 0x00336F73 File Offset: 0x00335173
				public rangeSubstring? rangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFC RID: 60924 RVA: 0x00336F81 File Offset: 0x00335181
				public rangeNumber? rangeNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFD RID: 60925 RVA: 0x00336F8F File Offset: 0x0033518F
				public dtRangeString? dtRangeString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFE RID: 60926 RVA: 0x00336F9D File Offset: 0x0033519D
				public dtRangeSubstring? dtRangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EDFF RID: 60927 RVA: 0x00336FAB File Offset: 0x003351AB
				public rangeDateTime? rangeDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE00 RID: 60928 RVA: 0x00336FB9 File Offset: 0x003351B9
				public datetime? datetime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE01 RID: 60929 RVA: 0x00336FC7 File Offset: 0x003351C7
				public inputDateTime? inputDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE02 RID: 60930 RVA: 0x00336FD5 File Offset: 0x003351D5
				public parsedDateTime? parsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE03 RID: 60931 RVA: 0x00336FE3 File Offset: 0x003351E3
				public SS? SS(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE04 RID: 60932 RVA: 0x00336FF1 File Offset: 0x003351F1
				public PP? PP(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE05 RID: 60933 RVA: 0x00336FFF File Offset: 0x003351FF
				public pl1? pl1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE06 RID: 60934 RVA: 0x0033700D File Offset: 0x0033520D
				public pl2? pl2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE07 RID: 60935 RVA: 0x0033701B File Offset: 0x0033521B
				public pl2p? pl2p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE08 RID: 60936 RVA: 0x00337029 File Offset: 0x00335229
				public pos? pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE09 RID: 60937 RVA: 0x00337037 File Offset: 0x00335237
				public regexPair? regexPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0A RID: 60938 RVA: 0x00337045 File Offset: 0x00335245
				public number? number(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0B RID: 60939 RVA: 0x00337053 File Offset: 0x00335253
				public castToNumber? castToNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0C RID: 60940 RVA: 0x00337061 File Offset: 0x00335261
				public inputNumber? inputNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0D RID: 60941 RVA: 0x0033706F File Offset: 0x0033526F
				public parsedNumber? parsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0E RID: 60942 RVA: 0x0033707D File Offset: 0x0033527D
				public b? b(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE0F RID: 60943 RVA: 0x0033708B File Offset: 0x0033528B
				public cs? cs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE10 RID: 60944 RVA: 0x00337099 File Offset: 0x00335299
				public y? y(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE11 RID: 60945 RVA: 0x003370A7 File Offset: 0x003352A7
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE12 RID: 60946 RVA: 0x003370B5 File Offset: 0x003352B5
				public externalExtractor? externalExtractor(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE13 RID: 60947 RVA: 0x003370C3 File Offset: 0x003352C3
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r? r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE14 RID: 60948 RVA: 0x003370D1 File Offset: 0x003352D1
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s? s(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE15 RID: 60949 RVA: 0x003370DF File Offset: 0x003352DF
				public name? name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE16 RID: 60950 RVA: 0x003370ED File Offset: 0x003352ED
				public roundingSpec? roundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE17 RID: 60951 RVA: 0x003370FB File Offset: 0x003352FB
				public dtRoundingSpec? dtRoundingSpec(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE18 RID: 60952 RVA: 0x00337109 File Offset: 0x00335309
				public minTrailingZeros? minTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE19 RID: 60953 RVA: 0x00337117 File Offset: 0x00335317
				public maxTrailingZeros? maxTrailingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1A RID: 60954 RVA: 0x00337125 File Offset: 0x00335325
				public minTrailingZerosAndWhitespace? minTrailingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1B RID: 60955 RVA: 0x00337133 File Offset: 0x00335333
				public minLeadingZeros? minLeadingZeros(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1C RID: 60956 RVA: 0x00337141 File Offset: 0x00335341
				public minLeadingZerosAndWhitespace? minLeadingZerosAndWhitespace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1D RID: 60957 RVA: 0x0033714F File Offset: 0x0033534F
				public numberFormatSeparatorChar? numberFormatSeparatorChar(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1E RID: 60958 RVA: 0x0033715D File Offset: 0x0033535D
				public numberFormatDetails? numberFormatDetails(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE1F RID: 60959 RVA: 0x0033716B File Offset: 0x0033536B
				public numberFormat? numberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE20 RID: 60960 RVA: 0x00337179 File Offset: 0x00335379
				public numberFormatLiteral? numberFormatLiteral(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE21 RID: 60961 RVA: 0x00337187 File Offset: 0x00335387
				public outputDtFormat? outputDtFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE22 RID: 60962 RVA: 0x00337195 File Offset: 0x00335395
				public inputDtFormats? inputDtFormats(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE23 RID: 60963 RVA: 0x003371A3 File Offset: 0x003353A3
				public lookupDictionary? lookupDictionary(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE24 RID: 60964 RVA: 0x003371B1 File Offset: 0x003353B1
				public idx? idx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE25 RID: 60965 RVA: 0x003371BF File Offset: 0x003353BF
				public columnIdx? columnIdx(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE26 RID: 60966 RVA: 0x003371CD File Offset: 0x003353CD
				public _LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE27 RID: 60967 RVA: 0x003371DB File Offset: 0x003353DB
				public _LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE28 RID: 60968 RVA: 0x003371E9 File Offset: 0x003353E9
				public _LetB2? _LetB2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE29 RID: 60969 RVA: 0x003371F7 File Offset: 0x003353F7
				public _LetB3? _LetB3(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE2A RID: 60970 RVA: 0x00337205 File Offset: 0x00335405
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4? _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE2B RID: 60971 RVA: 0x00337213 File Offset: 0x00335413
				public _LetB5? _LetB5(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE2C RID: 60972 RVA: 0x00337221 File Offset: 0x00335421
				public _LetB6? _LetB6(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE2D RID: 60973 RVA: 0x0033722F File Offset: 0x0033542F
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7? _LetB7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7.CreateSafe(this._builders, node);
				}

				// Token: 0x04005A41 RID: 23105
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE2 RID: 7138
			public class RuleAs
			{
				// Token: 0x0600EE2E RID: 60974 RVA: 0x0033723D File Offset: 0x0033543D
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EE2F RID: 60975 RVA: 0x0033724C File Offset: 0x0033544C
				public SingleBranch? SingleBranch(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SingleBranch.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE30 RID: 60976 RVA: 0x0033725A File Offset: 0x0033545A
				public switch_ite? switch_ite(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.switch_ite.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE31 RID: 60977 RVA: 0x00337268 File Offset: 0x00335468
				public IfThenElse? IfThenElse(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IfThenElse.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE32 RID: 60978 RVA: 0x00337276 File Offset: 0x00335476
				public Predicate? Predicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Predicate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE33 RID: 60979 RVA: 0x00337284 File Offset: 0x00335484
				public Transformation? Transformation(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Transformation.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE34 RID: 60980 RVA: 0x00337292 File Offset: 0x00335492
				public Atom? Atom(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Atom.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE35 RID: 60981 RVA: 0x003372A0 File Offset: 0x003354A0
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat? Concat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Concat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE36 RID: 60982 RVA: 0x003372AE File Offset: 0x003354AE
				public ConstStr? ConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ConstStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE37 RID: 60983 RVA: 0x003372BC File Offset: 0x003354BC
				public LetColumnName? LetColumnName(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetColumnName.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE38 RID: 60984 RVA: 0x003372CA File Offset: 0x003354CA
				public LetCell? LetCell(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetCell.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE39 RID: 60985 RVA: 0x003372D8 File Offset: 0x003354D8
				public LetX? LetX(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetX.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3A RID: 60986 RVA: 0x003372E6 File Offset: 0x003354E6
				public ChooseInput? ChooseInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ChooseInput.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3B RID: 60987 RVA: 0x003372F4 File Offset: 0x003354F4
				public v_indexInputString? v_indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.v_indexInputString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3C RID: 60988 RVA: 0x00337302 File Offset: 0x00335502
				public IndexInputString? IndexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.IndexInputString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3D RID: 60989 RVA: 0x00337310 File Offset: 0x00335510
				public LookupInput? LookupInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LookupInput.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3E RID: 60990 RVA: 0x0033731E File Offset: 0x0033551E
				public lookupInput_indexInputString? lookupInput_indexInputString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.lookupInput_indexInputString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE3F RID: 60991 RVA: 0x0033732C File Offset: 0x0033552C
				public LetSharedNumberFormat? LetSharedNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedNumberFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE40 RID: 60992 RVA: 0x0033733A File Offset: 0x0033553A
				public LetSharedDateTimeFormat? LetSharedDateTimeFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedDateTimeFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE41 RID: 60993 RVA: 0x00337348 File Offset: 0x00335548
				public SubString? SubString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE42 RID: 60994 RVA: 0x00337356 File Offset: 0x00335556
				public ToLowercase? ToLowercase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToLowercase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE43 RID: 60995 RVA: 0x00337364 File Offset: 0x00335564
				public ToUppercase? ToUppercase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToUppercase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE44 RID: 60996 RVA: 0x00337372 File Offset: 0x00335572
				public ToSimpleTitleCase? ToSimpleTitleCase(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ToSimpleTitleCase.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE45 RID: 60997 RVA: 0x00337380 File Offset: 0x00335580
				public FormatPartialDateTime? FormatPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatPartialDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE46 RID: 60998 RVA: 0x0033738E File Offset: 0x0033558E
				public FormatNumber? FormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE47 RID: 60999 RVA: 0x0033739C File Offset: 0x0033559C
				public Lookup? Lookup(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Lookup.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE48 RID: 61000 RVA: 0x003373AA File Offset: 0x003355AA
				public FormatNumericRange? FormatNumericRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumericRange.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE49 RID: 61001 RVA: 0x003373B8 File Offset: 0x003355B8
				public FormatDateTimeRange? FormatDateTimeRange(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4A RID: 61002 RVA: 0x003373C6 File Offset: 0x003355C6
				public LetSharedParsedNumber? LetSharedParsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4B RID: 61003 RVA: 0x003373D4 File Offset: 0x003355D4
				public LetSharedParsedDateTime? LetSharedParsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetSharedParsedDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4C RID: 61004 RVA: 0x003373E2 File Offset: 0x003355E2
				public rangeString_rangeSubstring? rangeString_rangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.rangeString_rangeSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4D RID: 61005 RVA: 0x003373F0 File Offset: 0x003355F0
				public RangeConcat? RangeConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConcat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4E RID: 61006 RVA: 0x003373FE File Offset: 0x003355FE
				public RangeConstStr? RangeConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeConstStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE4F RID: 61007 RVA: 0x0033740C File Offset: 0x0033560C
				public RangeFormatNumber? RangeFormatNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE50 RID: 61008 RVA: 0x0033741A File Offset: 0x0033561A
				public RangeRoundNumber? RangeRoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE51 RID: 61009 RVA: 0x00337428 File Offset: 0x00335628
				public dtRangeString_dtRangeSubstring? dtRangeString_dtRangeSubstring(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.dtRangeString_dtRangeSubstring.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE52 RID: 61010 RVA: 0x00337436 File Offset: 0x00335636
				public DtRangeConcat? DtRangeConcat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConcat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE53 RID: 61011 RVA: 0x00337444 File Offset: 0x00335644
				public DtRangeConstStr? DtRangeConstStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.DtRangeConstStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE54 RID: 61012 RVA: 0x00337452 File Offset: 0x00335652
				public RangeFormatDateTime? RangeFormatDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeFormatDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE55 RID: 61013 RVA: 0x00337460 File Offset: 0x00335660
				public RangeRoundDateTime? RangeRoundDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RangeRoundDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE56 RID: 61014 RVA: 0x0033746E File Offset: 0x0033566E
				public datetime_inputDateTime? datetime_inputDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.datetime_inputDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE57 RID: 61015 RVA: 0x0033747C File Offset: 0x0033567C
				public RoundPartialDateTime? RoundPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundPartialDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE58 RID: 61016 RVA: 0x0033748A File Offset: 0x0033568A
				public AsPartialDateTime? AsPartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsPartialDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE59 RID: 61017 RVA: 0x00337498 File Offset: 0x00335698
				public inputDateTime_parsedDateTime? inputDateTime_parsedDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputDateTime_parsedDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5A RID: 61018 RVA: 0x003374A6 File Offset: 0x003356A6
				public ParsePartialDateTime? ParsePartialDateTime(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParsePartialDateTime.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5B RID: 61019 RVA: 0x003374B4 File Offset: 0x003356B4
				public WholeColumn? WholeColumn(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.WholeColumn.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5C RID: 61020 RVA: 0x003374C2 File Offset: 0x003356C2
				public SubStr? SubStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SubStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5D RID: 61021 RVA: 0x003374D0 File Offset: 0x003356D0
				public Add? Add(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Add.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5E RID: 61022 RVA: 0x003374DE File Offset: 0x003356DE
				public PosPairRelative? PosPairRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPairRelative.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE5F RID: 61023 RVA: 0x003374EC File Offset: 0x003356EC
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4? _LetB4(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB4.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE60 RID: 61024 RVA: 0x003374FA File Offset: 0x003356FA
				public RSubStr? RSubStr(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RSubStr.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE61 RID: 61025 RVA: 0x00337508 File Offset: 0x00335708
				public LetPL2? LetPL2(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL2.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE62 RID: 61026 RVA: 0x00337516 File Offset: 0x00335716
				public Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7? _LetB7(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes._LetB7.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE63 RID: 61027 RVA: 0x00337524 File Offset: 0x00335724
				public PosPair? PosPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.PosPair.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE64 RID: 61028 RVA: 0x00337532 File Offset: 0x00335732
				public LetPL1? LetPL1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPL1.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE65 RID: 61029 RVA: 0x00337540 File Offset: 0x00335740
				public RegexPositionPair? RegexPositionPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionPair.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE66 RID: 61030 RVA: 0x0033754E File Offset: 0x0033574E
				public ExternalExtractorPositionPair? ExternalExtractorPositionPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ExternalExtractorPositionPair.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE67 RID: 61031 RVA: 0x0033755C File Offset: 0x0033575C
				public RelativePosition? RelativePosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RelativePosition.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE68 RID: 61032 RVA: 0x0033756A File Offset: 0x0033576A
				public RegexPositionRelative? RegexPositionRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPositionRelative.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE69 RID: 61033 RVA: 0x00337578 File Offset: 0x00335778
				public AbsolutePosition? AbsolutePosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AbsolutePosition.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6A RID: 61034 RVA: 0x00337586 File Offset: 0x00335786
				public RegexPosition? RegexPosition(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPosition.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6B RID: 61035 RVA: 0x00337594 File Offset: 0x00335794
				public RegexPair? RegexPair(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RegexPair.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6C RID: 61036 RVA: 0x003375A2 File Offset: 0x003357A2
				public number_inputNumber? number_inputNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.number_inputNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6D RID: 61037 RVA: 0x003375B0 File Offset: 0x003357B0
				public RoundNumber? RoundNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6E RID: 61038 RVA: 0x003375BE File Offset: 0x003357BE
				public AsDecimal? AsDecimal(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.AsDecimal.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE6F RID: 61039 RVA: 0x003375CC File Offset: 0x003357CC
				public inputNumber_castToNumber? inputNumber_castToNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_castToNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE70 RID: 61040 RVA: 0x003375DA File Offset: 0x003357DA
				public inputNumber_parsedNumber? inputNumber_parsedNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.inputNumber_parsedNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE71 RID: 61041 RVA: 0x003375E8 File Offset: 0x003357E8
				public ParseNumber? ParseNumber(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.ParseNumber.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE72 RID: 61042 RVA: 0x003375F6 File Offset: 0x003357F6
				public LetPredicate? LetPredicate(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.LetPredicate.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE73 RID: 61043 RVA: 0x00337604 File Offset: 0x00335804
				public SelectInput? SelectInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectInput.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE74 RID: 61044 RVA: 0x00337612 File Offset: 0x00335812
				public SelectIndexedInput? SelectIndexedInput(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.SelectIndexedInput.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE75 RID: 61045 RVA: 0x00337620 File Offset: 0x00335820
				public BuildNumberFormat? BuildNumberFormat(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.BuildNumberFormat.CreateSafe(this._builders, node);
				}

				// Token: 0x0600EE76 RID: 61046 RVA: 0x0033762E File Offset: 0x0033582E
				public numberFormat_numberFormatLiteral? numberFormat_numberFormatLiteral(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes.numberFormat_numberFormatLiteral.CreateSafe(this._builders, node);
				}

				// Token: 0x04005A42 RID: 23106
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02001BE4 RID: 7140
		public class Sets
		{
			// Token: 0x0600EE7A RID: 61050 RVA: 0x00337658 File Offset: 0x00335858
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x170027FB RID: 10235
			// (get) Token: 0x0600EE7B RID: 61051 RVA: 0x003376A7 File Offset: 0x003358A7
			// (set) Token: 0x0600EE7C RID: 61052 RVA: 0x003376AF File Offset: 0x003358AF
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x170027FC RID: 10236
			// (get) Token: 0x0600EE7D RID: 61053 RVA: 0x003376B8 File Offset: 0x003358B8
			// (set) Token: 0x0600EE7E RID: 61054 RVA: 0x003376C0 File Offset: 0x003358C0
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x170027FD RID: 10237
			// (get) Token: 0x0600EE7F RID: 61055 RVA: 0x003376C9 File Offset: 0x003358C9
			// (set) Token: 0x0600EE80 RID: 61056 RVA: 0x003376D1 File Offset: 0x003358D1
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x170027FE RID: 10238
			// (get) Token: 0x0600EE81 RID: 61057 RVA: 0x003376DA File Offset: 0x003358DA
			// (set) Token: 0x0600EE82 RID: 61058 RVA: 0x003376E2 File Offset: 0x003358E2
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x170027FF RID: 10239
			// (get) Token: 0x0600EE83 RID: 61059 RVA: 0x003376EB File Offset: 0x003358EB
			// (set) Token: 0x0600EE84 RID: 61060 RVA: 0x003376F3 File Offset: 0x003358F3
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02001BE5 RID: 7141
			public class Joins
			{
				// Token: 0x0600EE85 RID: 61061 RVA: 0x003376FC File Offset: 0x003358FC
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EE86 RID: 61062 RVA: 0x0033770C File Offset: 0x0033590C
				public ProgramSetBuilder<ite> IfThenElse(ProgramSetBuilder<b> value0, ProgramSetBuilder<st> value1, ProgramSetBuilder<@switch> value2)
				{
					return ProgramSetBuilder<ite>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IfThenElse, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EE87 RID: 61063 RVA: 0x00337766 File Offset: 0x00335966
				public ProgramSetBuilder<e> Concat(ProgramSetBuilder<f> value0, ProgramSetBuilder<e> value1)
				{
					return ProgramSetBuilder<e>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE88 RID: 61064 RVA: 0x003377A6 File Offset: 0x003359A6
				public ProgramSetBuilder<f> ConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return ProgramSetBuilder<f>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE89 RID: 61065 RVA: 0x003377D7 File Offset: 0x003359D7
				public ProgramSetBuilder<v> ChooseInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<v>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ChooseInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE8A RID: 61066 RVA: 0x00337817 File Offset: 0x00335A17
				public ProgramSetBuilder<indexInputString> IndexInputString(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnIdx> value1)
				{
					return ProgramSetBuilder<indexInputString>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IndexInputString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE8B RID: 61067 RVA: 0x00337857 File Offset: 0x00335A57
				public ProgramSetBuilder<lookupInput> LookupInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnName> value1)
				{
					return ProgramSetBuilder<lookupInput>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LookupInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE8C RID: 61068 RVA: 0x00337897 File Offset: 0x00335A97
				public ProgramSetBuilder<conv> ToLowercase(ProgramSetBuilder<SS> value0)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToLowercase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE8D RID: 61069 RVA: 0x003378C8 File Offset: 0x00335AC8
				public ProgramSetBuilder<conv> ToUppercase(ProgramSetBuilder<SS> value0)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToUppercase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE8E RID: 61070 RVA: 0x003378F9 File Offset: 0x00335AF9
				public ProgramSetBuilder<conv> ToSimpleTitleCase(ProgramSetBuilder<SS> value0)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToSimpleTitleCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE8F RID: 61071 RVA: 0x0033792A File Offset: 0x00335B2A
				public ProgramSetBuilder<conv> FormatPartialDateTime(ProgramSetBuilder<datetime> value0, ProgramSetBuilder<outputDtFormat> value1)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatPartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE90 RID: 61072 RVA: 0x0033796A File Offset: 0x00335B6A
				public ProgramSetBuilder<conv> FormatNumber(ProgramSetBuilder<number> value0, ProgramSetBuilder<numberFormat> value1)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE91 RID: 61073 RVA: 0x003379AA File Offset: 0x00335BAA
				public ProgramSetBuilder<conv> Lookup(ProgramSetBuilder<x> value0, ProgramSetBuilder<lookupDictionary> value1)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Lookup, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE92 RID: 61074 RVA: 0x003379EC File Offset: 0x00335BEC
				public ProgramSetBuilder<conv> FormatNumericRange(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<numberFormat> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value2, ProgramSetBuilder<roundingSpec> value3, ProgramSetBuilder<roundingSpec> value4)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatNumericRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600EE93 RID: 61075 RVA: 0x00337A68 File Offset: 0x00335C68
				public ProgramSetBuilder<conv> FormatDateTimeRange(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<outputDtFormat> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value2, ProgramSetBuilder<dtRoundingSpec> value3, ProgramSetBuilder<dtRoundingSpec> value4)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FormatDateTimeRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600EE94 RID: 61076 RVA: 0x00337AE4 File Offset: 0x00335CE4
				public ProgramSetBuilder<rangeString> RangeConcat(ProgramSetBuilder<rangeSubstring> value0, ProgramSetBuilder<rangeString> value1)
				{
					return ProgramSetBuilder<rangeString>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeConcat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE95 RID: 61077 RVA: 0x00337B24 File Offset: 0x00335D24
				public ProgramSetBuilder<rangeSubstring> RangeConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return ProgramSetBuilder<rangeSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE96 RID: 61078 RVA: 0x00337B55 File Offset: 0x00335D55
				public ProgramSetBuilder<rangeSubstring> RangeFormatNumber(ProgramSetBuilder<rangeNumber> value0, ProgramSetBuilder<sharedNumberFormat> value1)
				{
					return ProgramSetBuilder<rangeSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeFormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE97 RID: 61079 RVA: 0x00337B95 File Offset: 0x00335D95
				public ProgramSetBuilder<rangeNumber> RangeRoundNumber(ProgramSetBuilder<sharedParsedNumber> value0, ProgramSetBuilder<roundingSpec> value1)
				{
					return ProgramSetBuilder<rangeNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeRoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE98 RID: 61080 RVA: 0x00337BD5 File Offset: 0x00335DD5
				public ProgramSetBuilder<dtRangeString> DtRangeConcat(ProgramSetBuilder<dtRangeSubstring> value0, ProgramSetBuilder<dtRangeString> value1)
				{
					return ProgramSetBuilder<dtRangeString>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DtRangeConcat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE99 RID: 61081 RVA: 0x00337C15 File Offset: 0x00335E15
				public ProgramSetBuilder<dtRangeSubstring> DtRangeConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return ProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DtRangeConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE9A RID: 61082 RVA: 0x00337C46 File Offset: 0x00335E46
				public ProgramSetBuilder<dtRangeSubstring> RangeFormatDateTime(ProgramSetBuilder<rangeDateTime> value0, ProgramSetBuilder<sharedDtFormat> value1)
				{
					return ProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeFormatDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE9B RID: 61083 RVA: 0x00337C86 File Offset: 0x00335E86
				public ProgramSetBuilder<rangeDateTime> RangeRoundDateTime(ProgramSetBuilder<sharedParsedDt> value0, ProgramSetBuilder<dtRoundingSpec> value1)
				{
					return ProgramSetBuilder<rangeDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RangeRoundDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE9C RID: 61084 RVA: 0x00337CC6 File Offset: 0x00335EC6
				public ProgramSetBuilder<datetime> RoundPartialDateTime(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<dtRoundingSpec> value1)
				{
					return ProgramSetBuilder<datetime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RoundPartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE9D RID: 61085 RVA: 0x00337D06 File Offset: 0x00335F06
				public ProgramSetBuilder<inputDateTime> AsPartialDateTime(ProgramSetBuilder<cell> value0)
				{
					return ProgramSetBuilder<inputDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AsPartialDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EE9E RID: 61086 RVA: 0x00337D37 File Offset: 0x00335F37
				public ProgramSetBuilder<parsedDateTime> ParsePartialDateTime(ProgramSetBuilder<SS> value0, ProgramSetBuilder<inputDtFormats> value1)
				{
					return ProgramSetBuilder<parsedDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ParsePartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EE9F RID: 61087 RVA: 0x00337D77 File Offset: 0x00335F77
				public ProgramSetBuilder<SS> SubStr(ProgramSetBuilder<x> value0, ProgramSetBuilder<PP> value1)
				{
					return ProgramSetBuilder<SS>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SubStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA0 RID: 61088 RVA: 0x00337DB7 File Offset: 0x00335FB7
				public ProgramSetBuilder<_LetB2> Add(ProgramSetBuilder<pl1> value0, ProgramSetBuilder<pl2> value1)
				{
					return ProgramSetBuilder<_LetB2>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Add, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA1 RID: 61089 RVA: 0x00337DF7 File Offset: 0x00335FF7
				public ProgramSetBuilder<_LetB5> RSubStr(ProgramSetBuilder<x> value0, ProgramSetBuilder<pl1> value1)
				{
					return ProgramSetBuilder<_LetB5>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RSubStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA2 RID: 61090 RVA: 0x00337E38 File Offset: 0x00336038
				public ProgramSetBuilder<PP> RegexPositionPair(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return ProgramSetBuilder<PP>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RegexPositionPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEA3 RID: 61091 RVA: 0x00337E94 File Offset: 0x00336094
				public ProgramSetBuilder<PP> ExternalExtractorPositionPair(ProgramSetBuilder<x> value0, ProgramSetBuilder<externalExtractor> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return ProgramSetBuilder<PP>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ExternalExtractorPositionPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEA4 RID: 61092 RVA: 0x00337EEE File Offset: 0x003360EE
				public ProgramSetBuilder<pos> RelativePosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value1)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RelativePosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA5 RID: 61093 RVA: 0x00337F30 File Offset: 0x00336130
				public ProgramSetBuilder<pos> RegexPositionRelative(ProgramSetBuilder<x> value0, ProgramSetBuilder<regexPair> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RegexPositionRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEA6 RID: 61094 RVA: 0x00337F8A File Offset: 0x0033618A
				[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
				public ProgramSetBuilder<pos> AbsolutePosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value1)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AbsolutePosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA7 RID: 61095 RVA: 0x00337FCC File Offset: 0x003361CC
				[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
				public ProgramSetBuilder<pos> RegexPosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<regexPair> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RegexPosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEA8 RID: 61096 RVA: 0x00338026 File Offset: 0x00336226
				public ProgramSetBuilder<number> RoundNumber(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<roundingSpec> value1)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEA9 RID: 61097 RVA: 0x00338066 File Offset: 0x00336266
				public ProgramSetBuilder<castToNumber> AsDecimal(ProgramSetBuilder<cell> value0)
				{
					return ProgramSetBuilder<castToNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AsDecimal, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEAA RID: 61098 RVA: 0x00338097 File Offset: 0x00336297
				public ProgramSetBuilder<parsedNumber> ParseNumber(ProgramSetBuilder<SS> value0, ProgramSetBuilder<numberFormatDetails> value1)
				{
					return ProgramSetBuilder<parsedNumber>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ParseNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEAB RID: 61099 RVA: 0x003380D7 File Offset: 0x003362D7
				public ProgramSetBuilder<y> SelectInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<name> value1)
				{
					return ProgramSetBuilder<y>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEAC RID: 61100 RVA: 0x00338118 File Offset: 0x00336318
				public ProgramSetBuilder<numberFormat> BuildNumberFormat(ProgramSetBuilder<minTrailingZeros> value0, ProgramSetBuilder<maxTrailingZeros> value1, ProgramSetBuilder<minTrailingZerosAndWhitespace> value2, ProgramSetBuilder<minLeadingZeros> value3, ProgramSetBuilder<minLeadingZerosAndWhitespace> value4, ProgramSetBuilder<numberFormatDetails> value5)
				{
					return ProgramSetBuilder<numberFormat>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.BuildNumberFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null
					}));
				}

				// Token: 0x0600EEAD RID: 61101 RVA: 0x003381A5 File Offset: 0x003363A5
				public ProgramSetBuilder<_LetB3> PosPairRelative(ProgramSetBuilder<pl1> value0, ProgramSetBuilder<pl2p> value1)
				{
					return ProgramSetBuilder<_LetB3>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.PosPairRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEAE RID: 61102 RVA: 0x003381E5 File Offset: 0x003363E5
				public ProgramSetBuilder<PP> PosPair(ProgramSetBuilder<pos> value0, ProgramSetBuilder<pos> value1)
				{
					return ProgramSetBuilder<PP>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.PosPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEAF RID: 61103 RVA: 0x00338225 File Offset: 0x00336425
				public ProgramSetBuilder<regexPair> RegexPair(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value1)
				{
					return ProgramSetBuilder<regexPair>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RegexPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEB0 RID: 61104 RVA: 0x00338265 File Offset: 0x00336465
				public ProgramSetBuilder<@switch> SingleBranch(ProgramSetBuilder<st> value0)
				{
					return ProgramSetBuilder<@switch>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleBranch, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB1 RID: 61105 RVA: 0x00338296 File Offset: 0x00336496
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred> Predicate(ProgramSetBuilder<conjunct> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Predicate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB2 RID: 61106 RVA: 0x003382C7 File Offset: 0x003364C7
				public ProgramSetBuilder<st> Transformation(ProgramSetBuilder<e> value0)
				{
					return ProgramSetBuilder<st>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Transformation, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB3 RID: 61107 RVA: 0x003382F8 File Offset: 0x003364F8
				public ProgramSetBuilder<e> Atom(ProgramSetBuilder<f> value0)
				{
					return ProgramSetBuilder<e>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Atom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB4 RID: 61108 RVA: 0x00338329 File Offset: 0x00336529
				public ProgramSetBuilder<conv> SubString(ProgramSetBuilder<SS> value0)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SubString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB5 RID: 61109 RVA: 0x0033835A File Offset: 0x0033655A
				public ProgramSetBuilder<SS> WholeColumn(ProgramSetBuilder<x> value0)
				{
					return ProgramSetBuilder<SS>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.WholeColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB6 RID: 61110 RVA: 0x0033838B File Offset: 0x0033658B
				public ProgramSetBuilder<y> SelectIndexedInput(ProgramSetBuilder<v> value0)
				{
					return ProgramSetBuilder<y>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectIndexedInput, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEB7 RID: 61111 RVA: 0x003383BC File Offset: 0x003365BC
				public ProgramSetBuilder<f> LetColumnName(ProgramSetBuilder<idx> value0, ProgramSetBuilder<letOptions> value1)
				{
					return ProgramSetBuilder<f>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetColumnName, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEB8 RID: 61112 RVA: 0x003383FC File Offset: 0x003365FC
				public ProgramSetBuilder<letOptions> LetCell(ProgramSetBuilder<lookupInput> value0, ProgramSetBuilder<conv> value1)
				{
					return ProgramSetBuilder<letOptions>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetCell, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEB9 RID: 61113 RVA: 0x0033843C File Offset: 0x0033663C
				public ProgramSetBuilder<letOptions> LetX(ProgramSetBuilder<v> value0, ProgramSetBuilder<conv> value1)
				{
					return ProgramSetBuilder<letOptions>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetX, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBA RID: 61114 RVA: 0x0033847C File Offset: 0x0033667C
				public ProgramSetBuilder<_LetB0> LetSharedNumberFormat(ProgramSetBuilder<numberFormat> value0, ProgramSetBuilder<rangeString> value1)
				{
					return ProgramSetBuilder<_LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSharedNumberFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBB RID: 61115 RVA: 0x003384BC File Offset: 0x003366BC
				public ProgramSetBuilder<_LetB1> LetSharedDateTimeFormat(ProgramSetBuilder<outputDtFormat> value0, ProgramSetBuilder<dtRangeString> value1)
				{
					return ProgramSetBuilder<_LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSharedDateTimeFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBC RID: 61116 RVA: 0x003384FC File Offset: 0x003366FC
				public ProgramSetBuilder<conv> LetSharedParsedNumber(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<_LetB0> value1)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSharedParsedNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBD RID: 61117 RVA: 0x0033853C File Offset: 0x0033673C
				public ProgramSetBuilder<conv> LetSharedParsedDateTime(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return ProgramSetBuilder<conv>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetSharedParsedDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBE RID: 61118 RVA: 0x0033857C File Offset: 0x0033677C
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4> _LetB4(ProgramSetBuilder<_LetB2> value0, ProgramSetBuilder<_LetB3> value1)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4>.CreateUnsafe(ProgramSet.Join(this._builders.Rule._LetB4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEBF RID: 61119 RVA: 0x003385BC File Offset: 0x003367BC
				public ProgramSetBuilder<_LetB6> LetPL2(ProgramSetBuilder<pos> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4> value1)
				{
					return ProgramSetBuilder<_LetB6>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetPL2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC0 RID: 61120 RVA: 0x003385FC File Offset: 0x003367FC
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7> _LetB7(ProgramSetBuilder<_LetB5> value0, ProgramSetBuilder<_LetB6> value1)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7>.CreateUnsafe(ProgramSet.Join(this._builders.Rule._LetB7, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC1 RID: 61121 RVA: 0x0033863C File Offset: 0x0033683C
				public ProgramSetBuilder<PP> LetPL1(ProgramSetBuilder<pos> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7> value1)
				{
					return ProgramSetBuilder<PP>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetPL1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC2 RID: 61122 RVA: 0x0033867C File Offset: 0x0033687C
				public ProgramSetBuilder<b> LetPredicate(ProgramSetBuilder<y> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred> value1)
				{
					return ProgramSetBuilder<b>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetPredicate, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04005A49 RID: 23113
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE6 RID: 7142
			public class ExplicitJoins
			{
				// Token: 0x0600EEC3 RID: 61123 RVA: 0x003386BC File Offset: 0x003368BC
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EEC4 RID: 61124 RVA: 0x003386CC File Offset: 0x003368CC
				public JoinProgramSetBuilder<ite> IfThenElse(ProgramSetBuilder<b> value0, ProgramSetBuilder<st> value1, ProgramSetBuilder<@switch> value2)
				{
					return JoinProgramSetBuilder<ite>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IfThenElse, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEC5 RID: 61125 RVA: 0x00338726 File Offset: 0x00336926
				public JoinProgramSetBuilder<e> Concat(ProgramSetBuilder<f> value0, ProgramSetBuilder<e> value1)
				{
					return JoinProgramSetBuilder<e>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Concat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC6 RID: 61126 RVA: 0x00338766 File Offset: 0x00336966
				public JoinProgramSetBuilder<f> ConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return JoinProgramSetBuilder<f>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEC7 RID: 61127 RVA: 0x00338797 File Offset: 0x00336997
				public JoinProgramSetBuilder<v> ChooseInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<v>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ChooseInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC8 RID: 61128 RVA: 0x003387D7 File Offset: 0x003369D7
				public JoinProgramSetBuilder<indexInputString> IndexInputString(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnIdx> value1)
				{
					return JoinProgramSetBuilder<indexInputString>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IndexInputString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEC9 RID: 61129 RVA: 0x00338817 File Offset: 0x00336A17
				public JoinProgramSetBuilder<lookupInput> LookupInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<columnName> value1)
				{
					return JoinProgramSetBuilder<lookupInput>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LookupInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EECA RID: 61130 RVA: 0x00338857 File Offset: 0x00336A57
				public JoinProgramSetBuilder<conv> ToLowercase(ProgramSetBuilder<SS> value0)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToLowercase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EECB RID: 61131 RVA: 0x00338888 File Offset: 0x00336A88
				public JoinProgramSetBuilder<conv> ToUppercase(ProgramSetBuilder<SS> value0)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToUppercase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EECC RID: 61132 RVA: 0x003388B9 File Offset: 0x00336AB9
				public JoinProgramSetBuilder<conv> ToSimpleTitleCase(ProgramSetBuilder<SS> value0)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToSimpleTitleCase, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EECD RID: 61133 RVA: 0x003388EA File Offset: 0x00336AEA
				public JoinProgramSetBuilder<conv> FormatPartialDateTime(ProgramSetBuilder<datetime> value0, ProgramSetBuilder<outputDtFormat> value1)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatPartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EECE RID: 61134 RVA: 0x0033892A File Offset: 0x00336B2A
				public JoinProgramSetBuilder<conv> FormatNumber(ProgramSetBuilder<number> value0, ProgramSetBuilder<numberFormat> value1)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EECF RID: 61135 RVA: 0x0033896A File Offset: 0x00336B6A
				public JoinProgramSetBuilder<conv> Lookup(ProgramSetBuilder<x> value0, ProgramSetBuilder<lookupDictionary> value1)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Lookup, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED0 RID: 61136 RVA: 0x003389AC File Offset: 0x00336BAC
				public JoinProgramSetBuilder<conv> FormatNumericRange(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<numberFormat> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value2, ProgramSetBuilder<roundingSpec> value3, ProgramSetBuilder<roundingSpec> value4)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatNumericRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600EED1 RID: 61137 RVA: 0x00338A28 File Offset: 0x00336C28
				public JoinProgramSetBuilder<conv> FormatDateTimeRange(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<outputDtFormat> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value2, ProgramSetBuilder<dtRoundingSpec> value3, ProgramSetBuilder<dtRoundingSpec> value4)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FormatDateTimeRange, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null
					}));
				}

				// Token: 0x0600EED2 RID: 61138 RVA: 0x00338AA4 File Offset: 0x00336CA4
				public JoinProgramSetBuilder<rangeString> RangeConcat(ProgramSetBuilder<rangeSubstring> value0, ProgramSetBuilder<rangeString> value1)
				{
					return JoinProgramSetBuilder<rangeString>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeConcat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED3 RID: 61139 RVA: 0x00338AE4 File Offset: 0x00336CE4
				public JoinProgramSetBuilder<rangeSubstring> RangeConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return JoinProgramSetBuilder<rangeSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EED4 RID: 61140 RVA: 0x00338B15 File Offset: 0x00336D15
				public JoinProgramSetBuilder<rangeSubstring> RangeFormatNumber(ProgramSetBuilder<rangeNumber> value0, ProgramSetBuilder<sharedNumberFormat> value1)
				{
					return JoinProgramSetBuilder<rangeSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeFormatNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED5 RID: 61141 RVA: 0x00338B55 File Offset: 0x00336D55
				public JoinProgramSetBuilder<rangeNumber> RangeRoundNumber(ProgramSetBuilder<sharedParsedNumber> value0, ProgramSetBuilder<roundingSpec> value1)
				{
					return JoinProgramSetBuilder<rangeNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeRoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED6 RID: 61142 RVA: 0x00338B95 File Offset: 0x00336D95
				public JoinProgramSetBuilder<dtRangeString> DtRangeConcat(ProgramSetBuilder<dtRangeSubstring> value0, ProgramSetBuilder<dtRangeString> value1)
				{
					return JoinProgramSetBuilder<dtRangeString>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DtRangeConcat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED7 RID: 61143 RVA: 0x00338BD5 File Offset: 0x00336DD5
				public JoinProgramSetBuilder<dtRangeSubstring> DtRangeConstStr(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> value0)
				{
					return JoinProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DtRangeConstStr, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EED8 RID: 61144 RVA: 0x00338C06 File Offset: 0x00336E06
				public JoinProgramSetBuilder<dtRangeSubstring> RangeFormatDateTime(ProgramSetBuilder<rangeDateTime> value0, ProgramSetBuilder<sharedDtFormat> value1)
				{
					return JoinProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeFormatDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EED9 RID: 61145 RVA: 0x00338C46 File Offset: 0x00336E46
				public JoinProgramSetBuilder<rangeDateTime> RangeRoundDateTime(ProgramSetBuilder<sharedParsedDt> value0, ProgramSetBuilder<dtRoundingSpec> value1)
				{
					return JoinProgramSetBuilder<rangeDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RangeRoundDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEDA RID: 61146 RVA: 0x00338C86 File Offset: 0x00336E86
				public JoinProgramSetBuilder<datetime> RoundPartialDateTime(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<dtRoundingSpec> value1)
				{
					return JoinProgramSetBuilder<datetime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RoundPartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEDB RID: 61147 RVA: 0x00338CC6 File Offset: 0x00336EC6
				public JoinProgramSetBuilder<inputDateTime> AsPartialDateTime(ProgramSetBuilder<cell> value0)
				{
					return JoinProgramSetBuilder<inputDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AsPartialDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEDC RID: 61148 RVA: 0x00338CF7 File Offset: 0x00336EF7
				public JoinProgramSetBuilder<parsedDateTime> ParsePartialDateTime(ProgramSetBuilder<SS> value0, ProgramSetBuilder<inputDtFormats> value1)
				{
					return JoinProgramSetBuilder<parsedDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ParsePartialDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEDD RID: 61149 RVA: 0x00338D37 File Offset: 0x00336F37
				public JoinProgramSetBuilder<SS> SubStr(ProgramSetBuilder<x> value0, ProgramSetBuilder<PP> value1)
				{
					return JoinProgramSetBuilder<SS>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SubStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEDE RID: 61150 RVA: 0x00338D77 File Offset: 0x00336F77
				public JoinProgramSetBuilder<_LetB2> Add(ProgramSetBuilder<pl1> value0, ProgramSetBuilder<pl2> value1)
				{
					return JoinProgramSetBuilder<_LetB2>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Add, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEDF RID: 61151 RVA: 0x00338DB7 File Offset: 0x00336FB7
				public JoinProgramSetBuilder<_LetB5> RSubStr(ProgramSetBuilder<x> value0, ProgramSetBuilder<pl1> value1)
				{
					return JoinProgramSetBuilder<_LetB5>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RSubStr, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEE0 RID: 61152 RVA: 0x00338DF8 File Offset: 0x00336FF8
				public JoinProgramSetBuilder<PP> RegexPositionPair(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return JoinProgramSetBuilder<PP>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RegexPositionPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEE1 RID: 61153 RVA: 0x00338E54 File Offset: 0x00337054
				public JoinProgramSetBuilder<PP> ExternalExtractorPositionPair(ProgramSetBuilder<x> value0, ProgramSetBuilder<externalExtractor> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return JoinProgramSetBuilder<PP>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ExternalExtractorPositionPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEE2 RID: 61154 RVA: 0x00338EAE File Offset: 0x003370AE
				public JoinProgramSetBuilder<pos> RelativePosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value1)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RelativePosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEE3 RID: 61155 RVA: 0x00338EF0 File Offset: 0x003370F0
				public JoinProgramSetBuilder<pos> RegexPositionRelative(ProgramSetBuilder<x> value0, ProgramSetBuilder<regexPair> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RegexPositionRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEE4 RID: 61156 RVA: 0x00338F4A File Offset: 0x0033714A
				[Obsolete("The AbsolutePosition rule is marked as @deprecated in the DSL grammar.")]
				public JoinProgramSetBuilder<pos> AbsolutePosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value1)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AbsolutePosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEE5 RID: 61157 RVA: 0x00338F8C File Offset: 0x0033718C
				[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
				public JoinProgramSetBuilder<pos> RegexPosition(ProgramSetBuilder<x> value0, ProgramSetBuilder<regexPair> value1, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> value2)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RegexPosition, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600EEE6 RID: 61158 RVA: 0x00338FE6 File Offset: 0x003371E6
				public JoinProgramSetBuilder<number> RoundNumber(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<roundingSpec> value1)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RoundNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEE7 RID: 61159 RVA: 0x00339026 File Offset: 0x00337226
				public JoinProgramSetBuilder<castToNumber> AsDecimal(ProgramSetBuilder<cell> value0)
				{
					return JoinProgramSetBuilder<castToNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AsDecimal, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEE8 RID: 61160 RVA: 0x00339057 File Offset: 0x00337257
				public JoinProgramSetBuilder<parsedNumber> ParseNumber(ProgramSetBuilder<SS> value0, ProgramSetBuilder<numberFormatDetails> value1)
				{
					return JoinProgramSetBuilder<parsedNumber>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ParseNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEE9 RID: 61161 RVA: 0x00339097 File Offset: 0x00337297
				public JoinProgramSetBuilder<y> SelectInput(ProgramSetBuilder<vs> value0, ProgramSetBuilder<name> value1)
				{
					return JoinProgramSetBuilder<y>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectInput, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEEA RID: 61162 RVA: 0x003390D8 File Offset: 0x003372D8
				public JoinProgramSetBuilder<numberFormat> BuildNumberFormat(ProgramSetBuilder<minTrailingZeros> value0, ProgramSetBuilder<maxTrailingZeros> value1, ProgramSetBuilder<minTrailingZerosAndWhitespace> value2, ProgramSetBuilder<minLeadingZeros> value3, ProgramSetBuilder<minLeadingZerosAndWhitespace> value4, ProgramSetBuilder<numberFormatDetails> value5)
				{
					return JoinProgramSetBuilder<numberFormat>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.BuildNumberFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null,
						(value4 != null) ? value4.Set : null,
						(value5 != null) ? value5.Set : null
					}));
				}

				// Token: 0x0600EEEB RID: 61163 RVA: 0x00339165 File Offset: 0x00337365
				public JoinProgramSetBuilder<_LetB3> PosPairRelative(ProgramSetBuilder<pl1> value0, ProgramSetBuilder<pl2p> value1)
				{
					return JoinProgramSetBuilder<_LetB3>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.PosPairRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEEC RID: 61164 RVA: 0x003391A5 File Offset: 0x003373A5
				public JoinProgramSetBuilder<PP> PosPair(ProgramSetBuilder<pos> value0, ProgramSetBuilder<pos> value1)
				{
					return JoinProgramSetBuilder<PP>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.PosPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEED RID: 61165 RVA: 0x003391E5 File Offset: 0x003373E5
				public JoinProgramSetBuilder<regexPair> RegexPair(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> value1)
				{
					return JoinProgramSetBuilder<regexPair>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RegexPair, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEEE RID: 61166 RVA: 0x00339225 File Offset: 0x00337425
				public JoinProgramSetBuilder<@switch> SingleBranch(ProgramSetBuilder<st> value0)
				{
					return JoinProgramSetBuilder<@switch>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleBranch, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEEF RID: 61167 RVA: 0x00339256 File Offset: 0x00337456
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred> Predicate(ProgramSetBuilder<conjunct> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Predicate, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF0 RID: 61168 RVA: 0x00339287 File Offset: 0x00337487
				public JoinProgramSetBuilder<st> Transformation(ProgramSetBuilder<e> value0)
				{
					return JoinProgramSetBuilder<st>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Transformation, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF1 RID: 61169 RVA: 0x003392B8 File Offset: 0x003374B8
				public JoinProgramSetBuilder<e> Atom(ProgramSetBuilder<f> value0)
				{
					return JoinProgramSetBuilder<e>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Atom, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF2 RID: 61170 RVA: 0x003392E9 File Offset: 0x003374E9
				public JoinProgramSetBuilder<conv> SubString(ProgramSetBuilder<SS> value0)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SubString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF3 RID: 61171 RVA: 0x0033931A File Offset: 0x0033751A
				public JoinProgramSetBuilder<SS> WholeColumn(ProgramSetBuilder<x> value0)
				{
					return JoinProgramSetBuilder<SS>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.WholeColumn, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF4 RID: 61172 RVA: 0x0033934B File Offset: 0x0033754B
				public JoinProgramSetBuilder<y> SelectIndexedInput(ProgramSetBuilder<v> value0)
				{
					return JoinProgramSetBuilder<y>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectIndexedInput, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EEF5 RID: 61173 RVA: 0x0033937C File Offset: 0x0033757C
				public JoinProgramSetBuilder<f> LetColumnName(ProgramSetBuilder<idx> value0, ProgramSetBuilder<letOptions> value1)
				{
					return JoinProgramSetBuilder<f>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetColumnName, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEF6 RID: 61174 RVA: 0x003393BC File Offset: 0x003375BC
				public JoinProgramSetBuilder<letOptions> LetCell(ProgramSetBuilder<lookupInput> value0, ProgramSetBuilder<conv> value1)
				{
					return JoinProgramSetBuilder<letOptions>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetCell, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEF7 RID: 61175 RVA: 0x003393FC File Offset: 0x003375FC
				public JoinProgramSetBuilder<letOptions> LetX(ProgramSetBuilder<v> value0, ProgramSetBuilder<conv> value1)
				{
					return JoinProgramSetBuilder<letOptions>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetX, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEF8 RID: 61176 RVA: 0x0033943C File Offset: 0x0033763C
				public JoinProgramSetBuilder<_LetB0> LetSharedNumberFormat(ProgramSetBuilder<numberFormat> value0, ProgramSetBuilder<rangeString> value1)
				{
					return JoinProgramSetBuilder<_LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSharedNumberFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEF9 RID: 61177 RVA: 0x0033947C File Offset: 0x0033767C
				public JoinProgramSetBuilder<_LetB1> LetSharedDateTimeFormat(ProgramSetBuilder<outputDtFormat> value0, ProgramSetBuilder<dtRangeString> value1)
				{
					return JoinProgramSetBuilder<_LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSharedDateTimeFormat, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFA RID: 61178 RVA: 0x003394BC File Offset: 0x003376BC
				public JoinProgramSetBuilder<conv> LetSharedParsedNumber(ProgramSetBuilder<inputNumber> value0, ProgramSetBuilder<_LetB0> value1)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSharedParsedNumber, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFB RID: 61179 RVA: 0x003394FC File Offset: 0x003376FC
				public JoinProgramSetBuilder<conv> LetSharedParsedDateTime(ProgramSetBuilder<inputDateTime> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return JoinProgramSetBuilder<conv>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetSharedParsedDateTime, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFC RID: 61180 RVA: 0x0033953C File Offset: 0x0033773C
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4> _LetB4(ProgramSetBuilder<_LetB2> value0, ProgramSetBuilder<_LetB3> value1)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4>.CreateUnsafe(new JoinProgramSet(this._builders.Rule._LetB4, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFD RID: 61181 RVA: 0x0033957C File Offset: 0x0033777C
				public JoinProgramSetBuilder<_LetB6> LetPL2(ProgramSetBuilder<pos> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4> value1)
				{
					return JoinProgramSetBuilder<_LetB6>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetPL2, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFE RID: 61182 RVA: 0x003395BC File Offset: 0x003377BC
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7> _LetB7(ProgramSetBuilder<_LetB5> value0, ProgramSetBuilder<_LetB6> value1)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7>.CreateUnsafe(new JoinProgramSet(this._builders.Rule._LetB7, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EEFF RID: 61183 RVA: 0x003395FC File Offset: 0x003377FC
				public JoinProgramSetBuilder<PP> LetPL1(ProgramSetBuilder<pos> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7> value1)
				{
					return JoinProgramSetBuilder<PP>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetPL1, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600EF00 RID: 61184 RVA: 0x0033963C File Offset: 0x0033783C
				public JoinProgramSetBuilder<b> LetPredicate(ProgramSetBuilder<y> value0, ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred> value1)
				{
					return JoinProgramSetBuilder<b>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetPredicate, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04005A4A RID: 23114
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE7 RID: 7143
			public class JoinUnnamedConversions
			{
				// Token: 0x0600EF01 RID: 61185 RVA: 0x0033967C File Offset: 0x0033787C
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EF02 RID: 61186 RVA: 0x0033968B File Offset: 0x0033788B
				public ProgramSetBuilder<@switch> switch_ite(ProgramSetBuilder<ite> value0)
				{
					return ProgramSetBuilder<@switch>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.switch_ite, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF03 RID: 61187 RVA: 0x003396BC File Offset: 0x003378BC
				public ProgramSetBuilder<v> v_indexInputString(ProgramSetBuilder<indexInputString> value0)
				{
					return ProgramSetBuilder<v>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.v_indexInputString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF04 RID: 61188 RVA: 0x003396ED File Offset: 0x003378ED
				public ProgramSetBuilder<lookupInput> lookupInput_indexInputString(ProgramSetBuilder<indexInputString> value0)
				{
					return ProgramSetBuilder<lookupInput>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.lookupInput_indexInputString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF05 RID: 61189 RVA: 0x0033971E File Offset: 0x0033791E
				public ProgramSetBuilder<rangeString> rangeString_rangeSubstring(ProgramSetBuilder<rangeSubstring> value0)
				{
					return ProgramSetBuilder<rangeString>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.rangeString_rangeSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF06 RID: 61190 RVA: 0x0033974F File Offset: 0x0033794F
				public ProgramSetBuilder<dtRangeString> dtRangeString_dtRangeSubstring(ProgramSetBuilder<dtRangeSubstring> value0)
				{
					return ProgramSetBuilder<dtRangeString>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.dtRangeString_dtRangeSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF07 RID: 61191 RVA: 0x00339780 File Offset: 0x00337980
				public ProgramSetBuilder<datetime> datetime_inputDateTime(ProgramSetBuilder<inputDateTime> value0)
				{
					return ProgramSetBuilder<datetime>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.datetime_inputDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF08 RID: 61192 RVA: 0x003397B1 File Offset: 0x003379B1
				public ProgramSetBuilder<inputDateTime> inputDateTime_parsedDateTime(ProgramSetBuilder<parsedDateTime> value0)
				{
					return ProgramSetBuilder<inputDateTime>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.inputDateTime_parsedDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF09 RID: 61193 RVA: 0x003397E2 File Offset: 0x003379E2
				public ProgramSetBuilder<number> number_inputNumber(ProgramSetBuilder<inputNumber> value0)
				{
					return ProgramSetBuilder<number>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.number_inputNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF0A RID: 61194 RVA: 0x00339813 File Offset: 0x00337A13
				public ProgramSetBuilder<inputNumber> inputNumber_castToNumber(ProgramSetBuilder<castToNumber> value0)
				{
					return ProgramSetBuilder<inputNumber>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.inputNumber_castToNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF0B RID: 61195 RVA: 0x00339844 File Offset: 0x00337A44
				public ProgramSetBuilder<inputNumber> inputNumber_parsedNumber(ProgramSetBuilder<parsedNumber> value0)
				{
					return ProgramSetBuilder<inputNumber>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.inputNumber_parsedNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF0C RID: 61196 RVA: 0x00339875 File Offset: 0x00337A75
				public ProgramSetBuilder<numberFormat> numberFormat_numberFormatLiteral(ProgramSetBuilder<numberFormatLiteral> value0)
				{
					return ProgramSetBuilder<numberFormat>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.numberFormat_numberFormatLiteral, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005A4B RID: 23115
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE8 RID: 7144
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x0600EF0D RID: 61197 RVA: 0x003398A6 File Offset: 0x00337AA6
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EF0E RID: 61198 RVA: 0x003398B5 File Offset: 0x00337AB5
				public JoinProgramSetBuilder<@switch> switch_ite(ProgramSetBuilder<ite> value0)
				{
					return JoinProgramSetBuilder<@switch>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.switch_ite, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF0F RID: 61199 RVA: 0x003398E6 File Offset: 0x00337AE6
				public JoinProgramSetBuilder<v> v_indexInputString(ProgramSetBuilder<indexInputString> value0)
				{
					return JoinProgramSetBuilder<v>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.v_indexInputString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF10 RID: 61200 RVA: 0x00339917 File Offset: 0x00337B17
				public JoinProgramSetBuilder<lookupInput> lookupInput_indexInputString(ProgramSetBuilder<indexInputString> value0)
				{
					return JoinProgramSetBuilder<lookupInput>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.lookupInput_indexInputString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF11 RID: 61201 RVA: 0x00339948 File Offset: 0x00337B48
				public JoinProgramSetBuilder<rangeString> rangeString_rangeSubstring(ProgramSetBuilder<rangeSubstring> value0)
				{
					return JoinProgramSetBuilder<rangeString>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.rangeString_rangeSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF12 RID: 61202 RVA: 0x00339979 File Offset: 0x00337B79
				public JoinProgramSetBuilder<dtRangeString> dtRangeString_dtRangeSubstring(ProgramSetBuilder<dtRangeSubstring> value0)
				{
					return JoinProgramSetBuilder<dtRangeString>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.dtRangeString_dtRangeSubstring, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF13 RID: 61203 RVA: 0x003399AA File Offset: 0x00337BAA
				public JoinProgramSetBuilder<datetime> datetime_inputDateTime(ProgramSetBuilder<inputDateTime> value0)
				{
					return JoinProgramSetBuilder<datetime>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.datetime_inputDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF14 RID: 61204 RVA: 0x003399DB File Offset: 0x00337BDB
				public JoinProgramSetBuilder<inputDateTime> inputDateTime_parsedDateTime(ProgramSetBuilder<parsedDateTime> value0)
				{
					return JoinProgramSetBuilder<inputDateTime>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.inputDateTime_parsedDateTime, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF15 RID: 61205 RVA: 0x00339A0C File Offset: 0x00337C0C
				public JoinProgramSetBuilder<number> number_inputNumber(ProgramSetBuilder<inputNumber> value0)
				{
					return JoinProgramSetBuilder<number>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.number_inputNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF16 RID: 61206 RVA: 0x00339A3D File Offset: 0x00337C3D
				public JoinProgramSetBuilder<inputNumber> inputNumber_castToNumber(ProgramSetBuilder<castToNumber> value0)
				{
					return JoinProgramSetBuilder<inputNumber>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.inputNumber_castToNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF17 RID: 61207 RVA: 0x00339A6E File Offset: 0x00337C6E
				public JoinProgramSetBuilder<inputNumber> inputNumber_parsedNumber(ProgramSetBuilder<parsedNumber> value0)
				{
					return JoinProgramSetBuilder<inputNumber>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.inputNumber_parsedNumber, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600EF18 RID: 61208 RVA: 0x00339A9F File Offset: 0x00337C9F
				public JoinProgramSetBuilder<numberFormat> numberFormat_numberFormatLiteral(ProgramSetBuilder<numberFormatLiteral> value0)
				{
					return JoinProgramSetBuilder<numberFormat>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.numberFormat_numberFormatLiteral, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04005A4C RID: 23116
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001BE9 RID: 7145
			public class Casts
			{
				// Token: 0x0600EF19 RID: 61209 RVA: 0x00339AD0 File Offset: 0x00337CD0
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600EF1A RID: 61210 RVA: 0x00339AE0 File Offset: 0x00337CE0
				public ProgramSetBuilder<@switch> @switch(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.@switch)
					{
						string text = "set";
						string text2 = "expected program set for symbol @switch but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.@switch>.CreateUnsafe(set);
				}

				// Token: 0x0600EF1B RID: 61211 RVA: 0x00339B38 File Offset: 0x00337D38
				public ProgramSetBuilder<ite> ite(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.ite)
					{
						string text = "set";
						string text2 = "expected program set for symbol ite but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.ite>.CreateUnsafe(set);
				}

				// Token: 0x0600EF1C RID: 61212 RVA: 0x00339B90 File Offset: 0x00337D90
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred> pred(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pred)
					{
						string text = "set";
						string text2 = "expected program set for symbol pred but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred>.CreateUnsafe(set);
				}

				// Token: 0x0600EF1D RID: 61213 RVA: 0x00339BE8 File Offset: 0x00337DE8
				public ProgramSetBuilder<st> st(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.st)
					{
						string text = "set";
						string text2 = "expected program set for symbol st but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.st>.CreateUnsafe(set);
				}

				// Token: 0x0600EF1E RID: 61214 RVA: 0x00339C40 File Offset: 0x00337E40
				public ProgramSetBuilder<e> e(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.e)
					{
						string text = "set";
						string text2 = "expected program set for symbol e but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.e>.CreateUnsafe(set);
				}

				// Token: 0x0600EF1F RID: 61215 RVA: 0x00339C98 File Offset: 0x00337E98
				public ProgramSetBuilder<f> f(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.f)
					{
						string text = "set";
						string text2 = "expected program set for symbol f but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.f>.CreateUnsafe(set);
				}

				// Token: 0x0600EF20 RID: 61216 RVA: 0x00339CF0 File Offset: 0x00337EF0
				public ProgramSetBuilder<columnName> columnName(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnName)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnName but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnName>.CreateUnsafe(set);
				}

				// Token: 0x0600EF21 RID: 61217 RVA: 0x00339D48 File Offset: 0x00337F48
				public ProgramSetBuilder<letOptions> letOptions(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.letOptions)
					{
						string text = "set";
						string text2 = "expected program set for symbol letOptions but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.letOptions>.CreateUnsafe(set);
				}

				// Token: 0x0600EF22 RID: 61218 RVA: 0x00339DA0 File Offset: 0x00337FA0
				public ProgramSetBuilder<cell> cell(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.cell)
					{
						string text = "set";
						string text2 = "expected program set for symbol cell but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cell>.CreateUnsafe(set);
				}

				// Token: 0x0600EF23 RID: 61219 RVA: 0x00339DF8 File Offset: 0x00337FF8
				public ProgramSetBuilder<x> x(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.x)
					{
						string text = "set";
						string text2 = "expected program set for symbol x but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.x>.CreateUnsafe(set);
				}

				// Token: 0x0600EF24 RID: 61220 RVA: 0x00339E50 File Offset: 0x00338050
				public ProgramSetBuilder<v> v(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.v)
					{
						string text = "set";
						string text2 = "expected program set for symbol v but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.v>.CreateUnsafe(set);
				}

				// Token: 0x0600EF25 RID: 61221 RVA: 0x00339EA8 File Offset: 0x003380A8
				public ProgramSetBuilder<indexInputString> indexInputString(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.indexInputString)
					{
						string text = "set";
						string text2 = "expected program set for symbol indexInputString but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.indexInputString>.CreateUnsafe(set);
				}

				// Token: 0x0600EF26 RID: 61222 RVA: 0x00339F00 File Offset: 0x00338100
				public ProgramSetBuilder<lookupInput> lookupInput(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.lookupInput)
					{
						string text = "set";
						string text2 = "expected program set for symbol lookupInput but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupInput>.CreateUnsafe(set);
				}

				// Token: 0x0600EF27 RID: 61223 RVA: 0x00339F58 File Offset: 0x00338158
				public ProgramSetBuilder<conv> conv(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.conv)
					{
						string text = "set";
						string text2 = "expected program set for symbol conv but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.conv>.CreateUnsafe(set);
				}

				// Token: 0x0600EF28 RID: 61224 RVA: 0x00339FB0 File Offset: 0x003381B0
				public ProgramSetBuilder<sharedParsedNumber> sharedParsedNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sharedParsedNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol sharedParsedNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600EF29 RID: 61225 RVA: 0x0033A008 File Offset: 0x00338208
				public ProgramSetBuilder<sharedNumberFormat> sharedNumberFormat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sharedNumberFormat)
					{
						string text = "set";
						string text2 = "expected program set for symbol sharedNumberFormat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedNumberFormat>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2A RID: 61226 RVA: 0x0033A060 File Offset: 0x00338260
				public ProgramSetBuilder<sharedParsedDt> sharedParsedDt(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sharedParsedDt)
					{
						string text = "set";
						string text2 = "expected program set for symbol sharedParsedDt but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedParsedDt>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2B RID: 61227 RVA: 0x0033A0B8 File Offset: 0x003382B8
				public ProgramSetBuilder<sharedDtFormat> sharedDtFormat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sharedDtFormat)
					{
						string text = "set";
						string text2 = "expected program set for symbol sharedDtFormat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.sharedDtFormat>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2C RID: 61228 RVA: 0x0033A110 File Offset: 0x00338310
				public ProgramSetBuilder<rangeString> rangeString(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rangeString)
					{
						string text = "set";
						string text2 = "expected program set for symbol rangeString but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeString>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2D RID: 61229 RVA: 0x0033A168 File Offset: 0x00338368
				public ProgramSetBuilder<rangeSubstring> rangeSubstring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rangeSubstring)
					{
						string text = "set";
						string text2 = "expected program set for symbol rangeSubstring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeSubstring>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2E RID: 61230 RVA: 0x0033A1C0 File Offset: 0x003383C0
				public ProgramSetBuilder<rangeNumber> rangeNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rangeNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol rangeNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600EF2F RID: 61231 RVA: 0x0033A218 File Offset: 0x00338418
				public ProgramSetBuilder<dtRangeString> dtRangeString(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dtRangeString)
					{
						string text = "set";
						string text2 = "expected program set for symbol dtRangeString but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeString>.CreateUnsafe(set);
				}

				// Token: 0x0600EF30 RID: 61232 RVA: 0x0033A270 File Offset: 0x00338470
				public ProgramSetBuilder<dtRangeSubstring> dtRangeSubstring(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dtRangeSubstring)
					{
						string text = "set";
						string text2 = "expected program set for symbol dtRangeSubstring but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRangeSubstring>.CreateUnsafe(set);
				}

				// Token: 0x0600EF31 RID: 61233 RVA: 0x0033A2C8 File Offset: 0x003384C8
				public ProgramSetBuilder<rangeDateTime> rangeDateTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.rangeDateTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol rangeDateTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.rangeDateTime>.CreateUnsafe(set);
				}

				// Token: 0x0600EF32 RID: 61234 RVA: 0x0033A320 File Offset: 0x00338520
				public ProgramSetBuilder<datetime> datetime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.datetime)
					{
						string text = "set";
						string text2 = "expected program set for symbol datetime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.datetime>.CreateUnsafe(set);
				}

				// Token: 0x0600EF33 RID: 61235 RVA: 0x0033A378 File Offset: 0x00338578
				public ProgramSetBuilder<inputDateTime> inputDateTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inputDateTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol inputDateTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDateTime>.CreateUnsafe(set);
				}

				// Token: 0x0600EF34 RID: 61236 RVA: 0x0033A3D0 File Offset: 0x003385D0
				public ProgramSetBuilder<parsedDateTime> parsedDateTime(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.parsedDateTime)
					{
						string text = "set";
						string text2 = "expected program set for symbol parsedDateTime but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedDateTime>.CreateUnsafe(set);
				}

				// Token: 0x0600EF35 RID: 61237 RVA: 0x0033A428 File Offset: 0x00338628
				public ProgramSetBuilder<SS> SS(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.SS)
					{
						string text = "set";
						string text2 = "expected program set for symbol SS but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.SS>.CreateUnsafe(set);
				}

				// Token: 0x0600EF36 RID: 61238 RVA: 0x0033A480 File Offset: 0x00338680
				public ProgramSetBuilder<PP> PP(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.PP)
					{
						string text = "set";
						string text2 = "expected program set for symbol PP but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.PP>.CreateUnsafe(set);
				}

				// Token: 0x0600EF37 RID: 61239 RVA: 0x0033A4D8 File Offset: 0x003386D8
				public ProgramSetBuilder<pl1> pl1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pl1)
					{
						string text = "set";
						string text2 = "expected program set for symbol pl1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl1>.CreateUnsafe(set);
				}

				// Token: 0x0600EF38 RID: 61240 RVA: 0x0033A530 File Offset: 0x00338730
				public ProgramSetBuilder<pl2> pl2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pl2)
					{
						string text = "set";
						string text2 = "expected program set for symbol pl2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2>.CreateUnsafe(set);
				}

				// Token: 0x0600EF39 RID: 61241 RVA: 0x0033A588 File Offset: 0x00338788
				public ProgramSetBuilder<pl2p> pl2p(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pl2p)
					{
						string text = "set";
						string text2 = "expected program set for symbol pl2p but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pl2p>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3A RID: 61242 RVA: 0x0033A5E0 File Offset: 0x003387E0
				public ProgramSetBuilder<pos> pos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pos)
					{
						string text = "set";
						string text2 = "expected program set for symbol pos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pos>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3B RID: 61243 RVA: 0x0033A638 File Offset: 0x00338838
				public ProgramSetBuilder<regexPair> regexPair(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.regexPair)
					{
						string text = "set";
						string text2 = "expected program set for symbol regexPair but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.regexPair>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3C RID: 61244 RVA: 0x0033A690 File Offset: 0x00338890
				public ProgramSetBuilder<number> number(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.number)
					{
						string text = "set";
						string text2 = "expected program set for symbol number but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.number>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3D RID: 61245 RVA: 0x0033A6E8 File Offset: 0x003388E8
				public ProgramSetBuilder<castToNumber> castToNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.castToNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol castToNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.castToNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3E RID: 61246 RVA: 0x0033A740 File Offset: 0x00338940
				public ProgramSetBuilder<inputNumber> inputNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inputNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol inputNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600EF3F RID: 61247 RVA: 0x0033A798 File Offset: 0x00338998
				public ProgramSetBuilder<parsedNumber> parsedNumber(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.parsedNumber)
					{
						string text = "set";
						string text2 = "expected program set for symbol parsedNumber but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.parsedNumber>.CreateUnsafe(set);
				}

				// Token: 0x0600EF40 RID: 61248 RVA: 0x0033A7F0 File Offset: 0x003389F0
				public ProgramSetBuilder<b> b(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.b)
					{
						string text = "set";
						string text2 = "expected program set for symbol b but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.b>.CreateUnsafe(set);
				}

				// Token: 0x0600EF41 RID: 61249 RVA: 0x0033A848 File Offset: 0x00338A48
				public ProgramSetBuilder<cs> cs(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.cs)
					{
						string text = "set";
						string text2 = "expected program set for symbol cs but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.cs>.CreateUnsafe(set);
				}

				// Token: 0x0600EF42 RID: 61250 RVA: 0x0033A8A0 File Offset: 0x00338AA0
				public ProgramSetBuilder<y> y(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.y)
					{
						string text = "set";
						string text2 = "expected program set for symbol y but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.y>.CreateUnsafe(set);
				}

				// Token: 0x0600EF43 RID: 61251 RVA: 0x0033A8F8 File Offset: 0x00338AF8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x0600EF44 RID: 61252 RVA: 0x0033A950 File Offset: 0x00338B50
				public ProgramSetBuilder<externalExtractor> externalExtractor(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.externalExtractor)
					{
						string text = "set";
						string text2 = "expected program set for symbol externalExtractor but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.externalExtractor>.CreateUnsafe(set);
				}

				// Token: 0x0600EF45 RID: 61253 RVA: 0x0033A9A8 File Offset: 0x00338BA8
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r> r(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.r)
					{
						string text = "set";
						string text2 = "expected program set for symbol r but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.r>.CreateUnsafe(set);
				}

				// Token: 0x0600EF46 RID: 61254 RVA: 0x0033AA00 File Offset: 0x00338C00
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s> s(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.s)
					{
						string text = "set";
						string text2 = "expected program set for symbol s but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.s>.CreateUnsafe(set);
				}

				// Token: 0x0600EF47 RID: 61255 RVA: 0x0033AA58 File Offset: 0x00338C58
				public ProgramSetBuilder<name> name(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.name)
					{
						string text = "set";
						string text2 = "expected program set for symbol name but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.name>.CreateUnsafe(set);
				}

				// Token: 0x0600EF48 RID: 61256 RVA: 0x0033AAB0 File Offset: 0x00338CB0
				public ProgramSetBuilder<roundingSpec> roundingSpec(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.roundingSpec)
					{
						string text = "set";
						string text2 = "expected program set for symbol roundingSpec but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.roundingSpec>.CreateUnsafe(set);
				}

				// Token: 0x0600EF49 RID: 61257 RVA: 0x0033AB08 File Offset: 0x00338D08
				public ProgramSetBuilder<dtRoundingSpec> dtRoundingSpec(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dtRoundingSpec)
					{
						string text = "set";
						string text2 = "expected program set for symbol dtRoundingSpec but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.dtRoundingSpec>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4A RID: 61258 RVA: 0x0033AB60 File Offset: 0x00338D60
				public ProgramSetBuilder<minTrailingZeros> minTrailingZeros(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.minTrailingZeros)
					{
						string text = "set";
						string text2 = "expected program set for symbol minTrailingZeros but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZeros>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4B RID: 61259 RVA: 0x0033ABB8 File Offset: 0x00338DB8
				public ProgramSetBuilder<maxTrailingZeros> maxTrailingZeros(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.maxTrailingZeros)
					{
						string text = "set";
						string text2 = "expected program set for symbol maxTrailingZeros but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.maxTrailingZeros>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4C RID: 61260 RVA: 0x0033AC10 File Offset: 0x00338E10
				public ProgramSetBuilder<minTrailingZerosAndWhitespace> minTrailingZerosAndWhitespace(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.minTrailingZerosAndWhitespace)
					{
						string text = "set";
						string text2 = "expected program set for symbol minTrailingZerosAndWhitespace but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minTrailingZerosAndWhitespace>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4D RID: 61261 RVA: 0x0033AC68 File Offset: 0x00338E68
				public ProgramSetBuilder<minLeadingZeros> minLeadingZeros(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.minLeadingZeros)
					{
						string text = "set";
						string text2 = "expected program set for symbol minLeadingZeros but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZeros>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4E RID: 61262 RVA: 0x0033ACC0 File Offset: 0x00338EC0
				public ProgramSetBuilder<minLeadingZerosAndWhitespace> minLeadingZerosAndWhitespace(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.minLeadingZerosAndWhitespace)
					{
						string text = "set";
						string text2 = "expected program set for symbol minLeadingZerosAndWhitespace but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.minLeadingZerosAndWhitespace>.CreateUnsafe(set);
				}

				// Token: 0x0600EF4F RID: 61263 RVA: 0x0033AD18 File Offset: 0x00338F18
				public ProgramSetBuilder<numberFormatSeparatorChar> numberFormatSeparatorChar(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberFormatSeparatorChar)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberFormatSeparatorChar but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatSeparatorChar>.CreateUnsafe(set);
				}

				// Token: 0x0600EF50 RID: 61264 RVA: 0x0033AD70 File Offset: 0x00338F70
				public ProgramSetBuilder<numberFormatDetails> numberFormatDetails(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberFormatDetails)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberFormatDetails but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatDetails>.CreateUnsafe(set);
				}

				// Token: 0x0600EF51 RID: 61265 RVA: 0x0033ADC8 File Offset: 0x00338FC8
				public ProgramSetBuilder<numberFormat> numberFormat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberFormat)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberFormat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormat>.CreateUnsafe(set);
				}

				// Token: 0x0600EF52 RID: 61266 RVA: 0x0033AE20 File Offset: 0x00339020
				public ProgramSetBuilder<numberFormatLiteral> numberFormatLiteral(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.numberFormatLiteral)
					{
						string text = "set";
						string text2 = "expected program set for symbol numberFormatLiteral but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.numberFormatLiteral>.CreateUnsafe(set);
				}

				// Token: 0x0600EF53 RID: 61267 RVA: 0x0033AE78 File Offset: 0x00339078
				public ProgramSetBuilder<outputDtFormat> outputDtFormat(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.outputDtFormat)
					{
						string text = "set";
						string text2 = "expected program set for symbol outputDtFormat but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.outputDtFormat>.CreateUnsafe(set);
				}

				// Token: 0x0600EF54 RID: 61268 RVA: 0x0033AED0 File Offset: 0x003390D0
				public ProgramSetBuilder<inputDtFormats> inputDtFormats(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inputDtFormats)
					{
						string text = "set";
						string text2 = "expected program set for symbol inputDtFormats but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.inputDtFormats>.CreateUnsafe(set);
				}

				// Token: 0x0600EF55 RID: 61269 RVA: 0x0033AF28 File Offset: 0x00339128
				public ProgramSetBuilder<lookupDictionary> lookupDictionary(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.lookupDictionary)
					{
						string text = "set";
						string text2 = "expected program set for symbol lookupDictionary but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.lookupDictionary>.CreateUnsafe(set);
				}

				// Token: 0x0600EF56 RID: 61270 RVA: 0x0033AF80 File Offset: 0x00339180
				public ProgramSetBuilder<idx> idx(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.idx)
					{
						string text = "set";
						string text2 = "expected program set for symbol idx but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.idx>.CreateUnsafe(set);
				}

				// Token: 0x0600EF57 RID: 61271 RVA: 0x0033AFD8 File Offset: 0x003391D8
				public ProgramSetBuilder<columnIdx> columnIdx(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.columnIdx)
					{
						string text = "set";
						string text2 = "expected program set for symbol columnIdx but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.columnIdx>.CreateUnsafe(set);
				}

				// Token: 0x0600EF58 RID: 61272 RVA: 0x0033B030 File Offset: 0x00339230
				public ProgramSetBuilder<_LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x0600EF59 RID: 61273 RVA: 0x0033B088 File Offset: 0x00339288
				public ProgramSetBuilder<_LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5A RID: 61274 RVA: 0x0033B0E0 File Offset: 0x003392E0
				public ProgramSetBuilder<_LetB2> _LetB2(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB2)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB2 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB2>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5B RID: 61275 RVA: 0x0033B138 File Offset: 0x00339338
				public ProgramSetBuilder<_LetB3> _LetB3(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB3)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB3 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB3>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5C RID: 61276 RVA: 0x0033B190 File Offset: 0x00339390
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4> _LetB4(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB4)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB4 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB4>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5D RID: 61277 RVA: 0x0033B1E8 File Offset: 0x003393E8
				public ProgramSetBuilder<_LetB5> _LetB5(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB5)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB5 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB5>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5E RID: 61278 RVA: 0x0033B240 File Offset: 0x00339440
				public ProgramSetBuilder<_LetB6> _LetB6(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB6)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB6 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB6>.CreateUnsafe(set);
				}

				// Token: 0x0600EF5F RID: 61279 RVA: 0x0033B298 File Offset: 0x00339498
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7> _LetB7(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB7)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB7 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes._LetB7>.CreateUnsafe(set);
				}

				// Token: 0x04005A4D RID: 23117
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
