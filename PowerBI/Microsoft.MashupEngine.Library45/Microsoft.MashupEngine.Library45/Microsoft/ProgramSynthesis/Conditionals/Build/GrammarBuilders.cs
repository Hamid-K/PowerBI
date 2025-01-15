using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Conditionals.Build
{
	// Token: 0x02000A17 RID: 2583
	public class GrammarBuilders
	{
		// Token: 0x06003E3C RID: 15932 RVA: 0x000C6582 File Offset: 0x000C4782
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x000C65AE File Offset: 0x000C47AE
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000C65BB File Offset: 0x000C47BB
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000C65C8 File Offset: 0x000C47C8
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000C65D5 File Offset: 0x000C47D5
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x000C65E2 File Offset: 0x000C47E2
		// (set) Token: 0x06003E42 RID: 15938 RVA: 0x000C65EA File Offset: 0x000C47EA
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000C65F3 File Offset: 0x000C47F3
		// (set) Token: 0x06003E44 RID: 15940 RVA: 0x000C65FB File Offset: 0x000C47FB
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06003E45 RID: 15941 RVA: 0x000C6604 File Offset: 0x000C4804
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

		// Token: 0x04001CFA RID: 7418
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04001CFB RID: 7419
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04001CFC RID: 7420
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04001CFD RID: 7421
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04001CFE RID: 7422
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000A18 RID: 2584
		public class GrammarSymbols
		{
			// Token: 0x17000AE2 RID: 2786
			// (get) Token: 0x06003E47 RID: 15943 RVA: 0x000C66AF File Offset: 0x000C48AF
			// (set) Token: 0x06003E48 RID: 15944 RVA: 0x000C66B7 File Offset: 0x000C48B7
			public Symbol s { get; private set; }

			// Token: 0x17000AE3 RID: 2787
			// (get) Token: 0x06003E49 RID: 15945 RVA: 0x000C66C0 File Offset: 0x000C48C0
			// (set) Token: 0x06003E4A RID: 15946 RVA: 0x000C66C8 File Offset: 0x000C48C8
			public Symbol r { get; private set; }

			// Token: 0x17000AE4 RID: 2788
			// (get) Token: 0x06003E4B RID: 15947 RVA: 0x000C66D1 File Offset: 0x000C48D1
			// (set) Token: 0x06003E4C RID: 15948 RVA: 0x000C66D9 File Offset: 0x000C48D9
			public Symbol k { get; private set; }

			// Token: 0x17000AE5 RID: 2789
			// (get) Token: 0x06003E4D RID: 15949 RVA: 0x000C66E2 File Offset: 0x000C48E2
			// (set) Token: 0x06003E4E RID: 15950 RVA: 0x000C66EA File Offset: 0x000C48EA
			public Symbol str { get; private set; }

			// Token: 0x17000AE6 RID: 2790
			// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000C66F3 File Offset: 0x000C48F3
			// (set) Token: 0x06003E50 RID: 15952 RVA: 0x000C66FB File Offset: 0x000C48FB
			public Symbol output { get; private set; }

			// Token: 0x17000AE7 RID: 2791
			// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000C6704 File Offset: 0x000C4904
			// (set) Token: 0x06003E52 RID: 15954 RVA: 0x000C670C File Offset: 0x000C490C
			public Symbol disjunct { get; private set; }

			// Token: 0x17000AE8 RID: 2792
			// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000C6715 File Offset: 0x000C4915
			// (set) Token: 0x06003E54 RID: 15956 RVA: 0x000C671D File Offset: 0x000C491D
			public Symbol conjunct { get; private set; }

			// Token: 0x17000AE9 RID: 2793
			// (get) Token: 0x06003E55 RID: 15957 RVA: 0x000C6726 File Offset: 0x000C4926
			// (set) Token: 0x06003E56 RID: 15958 RVA: 0x000C672E File Offset: 0x000C492E
			public Symbol baseConjunct { get; private set; }

			// Token: 0x17000AEA RID: 2794
			// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000C6737 File Offset: 0x000C4937
			// (set) Token: 0x06003E58 RID: 15960 RVA: 0x000C673F File Offset: 0x000C493F
			public Symbol pred { get; private set; }

			// Token: 0x17000AEB RID: 2795
			// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000C6748 File Offset: 0x000C4948
			// (set) Token: 0x06003E5A RID: 15962 RVA: 0x000C6750 File Offset: 0x000C4950
			public Symbol match { get; private set; }

			// Token: 0x06003E5B RID: 15963 RVA: 0x000C675C File Offset: 0x000C495C
			public GrammarSymbols(Grammar grammar)
			{
				this.s = grammar.Symbol("s");
				this.r = grammar.Symbol("r");
				this.k = grammar.Symbol("k");
				this.str = grammar.Symbol("str");
				this.output = grammar.Symbol("output");
				this.disjunct = grammar.Symbol("disjunct");
				this.conjunct = grammar.Symbol("conjunct");
				this.baseConjunct = grammar.Symbol("baseConjunct");
				this.pred = grammar.Symbol("pred");
				this.match = grammar.Symbol("match");
			}
		}

		// Token: 0x02000A19 RID: 2585
		public class GrammarRules
		{
			// Token: 0x17000AEC RID: 2796
			// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000C6819 File Offset: 0x000C4A19
			// (set) Token: 0x06003E5D RID: 15965 RVA: 0x000C6821 File Offset: 0x000C4A21
			public BlackBoxRule Disjunction { get; private set; }

			// Token: 0x17000AED RID: 2797
			// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000C682A File Offset: 0x000C4A2A
			// (set) Token: 0x06003E5F RID: 15967 RVA: 0x000C6832 File Offset: 0x000C4A32
			public BlackBoxRule Conjunction { get; private set; }

			// Token: 0x17000AEE RID: 2798
			// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000C683B File Offset: 0x000C4A3B
			// (set) Token: 0x06003E61 RID: 15969 RVA: 0x000C6843 File Offset: 0x000C4A43
			public BlackBoxRule Not { get; private set; }

			// Token: 0x17000AEF RID: 2799
			// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000C684C File Offset: 0x000C4A4C
			// (set) Token: 0x06003E63 RID: 15971 RVA: 0x000C6854 File Offset: 0x000C4A54
			public BlackBoxRule IsNullOrWhiteSpace { get; private set; }

			// Token: 0x17000AF0 RID: 2800
			// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000C685D File Offset: 0x000C4A5D
			// (set) Token: 0x06003E65 RID: 15973 RVA: 0x000C6865 File Offset: 0x000C4A65
			public BlackBoxRule IsNull { get; private set; }

			// Token: 0x17000AF1 RID: 2801
			// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000C686E File Offset: 0x000C4A6E
			// (set) Token: 0x06003E67 RID: 15975 RVA: 0x000C6876 File Offset: 0x000C4A76
			public BlackBoxRule IsWhiteSpace { get; private set; }

			// Token: 0x17000AF2 RID: 2802
			// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000C687F File Offset: 0x000C4A7F
			// (set) Token: 0x06003E69 RID: 15977 RVA: 0x000C6887 File Offset: 0x000C4A87
			public BlackBoxRule True { get; private set; }

			// Token: 0x17000AF3 RID: 2803
			// (get) Token: 0x06003E6A RID: 15978 RVA: 0x000C6890 File Offset: 0x000C4A90
			// (set) Token: 0x06003E6B RID: 15979 RVA: 0x000C6898 File Offset: 0x000C4A98
			public BlackBoxRule StartsWithString { get; private set; }

			// Token: 0x17000AF4 RID: 2804
			// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000C68A1 File Offset: 0x000C4AA1
			// (set) Token: 0x06003E6D RID: 15981 RVA: 0x000C68A9 File Offset: 0x000C4AA9
			public BlackBoxRule StartsWithDigit { get; private set; }

			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000C68B2 File Offset: 0x000C4AB2
			// (set) Token: 0x06003E6F RID: 15983 RVA: 0x000C68BA File Offset: 0x000C4ABA
			public BlackBoxRule StartsWithLetter { get; private set; }

			// Token: 0x17000AF6 RID: 2806
			// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000C68C3 File Offset: 0x000C4AC3
			// (set) Token: 0x06003E71 RID: 15985 RVA: 0x000C68CB File Offset: 0x000C4ACB
			public BlackBoxRule EndsWithString { get; private set; }

			// Token: 0x17000AF7 RID: 2807
			// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000C68D4 File Offset: 0x000C4AD4
			// (set) Token: 0x06003E73 RID: 15987 RVA: 0x000C68DC File Offset: 0x000C4ADC
			public BlackBoxRule EndsWithDigit { get; private set; }

			// Token: 0x17000AF8 RID: 2808
			// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000C68E5 File Offset: 0x000C4AE5
			// (set) Token: 0x06003E75 RID: 15989 RVA: 0x000C68ED File Offset: 0x000C4AED
			public BlackBoxRule EndsWithLetter { get; private set; }

			// Token: 0x17000AF9 RID: 2809
			// (get) Token: 0x06003E76 RID: 15990 RVA: 0x000C68F6 File Offset: 0x000C4AF6
			// (set) Token: 0x06003E77 RID: 15991 RVA: 0x000C68FE File Offset: 0x000C4AFE
			public BlackBoxRule ContainsString { get; private set; }

			// Token: 0x17000AFA RID: 2810
			// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000C6907 File Offset: 0x000C4B07
			// (set) Token: 0x06003E79 RID: 15993 RVA: 0x000C690F File Offset: 0x000C4B0F
			public BlackBoxRule Matches { get; private set; }

			// Token: 0x17000AFB RID: 2811
			// (get) Token: 0x06003E7A RID: 15994 RVA: 0x000C6918 File Offset: 0x000C4B18
			// (set) Token: 0x06003E7B RID: 15995 RVA: 0x000C6920 File Offset: 0x000C4B20
			public BlackBoxRule StartsWith { get; private set; }

			// Token: 0x17000AFC RID: 2812
			// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000C6929 File Offset: 0x000C4B29
			// (set) Token: 0x06003E7D RID: 15997 RVA: 0x000C6931 File Offset: 0x000C4B31
			public BlackBoxRule EndsWith { get; private set; }

			// Token: 0x17000AFD RID: 2813
			// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000C693A File Offset: 0x000C4B3A
			// (set) Token: 0x06003E7F RID: 15999 RVA: 0x000C6942 File Offset: 0x000C4B42
			public BlackBoxRule Contains { get; private set; }

			// Token: 0x17000AFE RID: 2814
			// (get) Token: 0x06003E80 RID: 16000 RVA: 0x000C694B File Offset: 0x000C4B4B
			// (set) Token: 0x06003E81 RID: 16001 RVA: 0x000C6953 File Offset: 0x000C4B53
			public ConversionRule Start { get; private set; }

			// Token: 0x17000AFF RID: 2815
			// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000C695C File Offset: 0x000C4B5C
			// (set) Token: 0x06003E83 RID: 16003 RVA: 0x000C6964 File Offset: 0x000C4B64
			public ConversionRule ConvertDisjunctConjunct { get; private set; }

			// Token: 0x17000B00 RID: 2816
			// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000C696D File Offset: 0x000C4B6D
			// (set) Token: 0x06003E85 RID: 16005 RVA: 0x000C6975 File Offset: 0x000C4B75
			public ConversionRule Conjunct { get; private set; }

			// Token: 0x06003E86 RID: 16006 RVA: 0x000C6980 File Offset: 0x000C4B80
			public GrammarRules(Grammar grammar)
			{
				this.Disjunction = (BlackBoxRule)grammar.Rule("Disjunction");
				this.Conjunction = (BlackBoxRule)grammar.Rule("Conjunction");
				this.Not = (BlackBoxRule)grammar.Rule("Not");
				this.IsNullOrWhiteSpace = (BlackBoxRule)grammar.Rule("IsNullOrWhiteSpace");
				this.IsNull = (BlackBoxRule)grammar.Rule("IsNull");
				this.IsWhiteSpace = (BlackBoxRule)grammar.Rule("IsWhiteSpace");
				this.True = (BlackBoxRule)grammar.Rule("True");
				this.StartsWithString = (BlackBoxRule)grammar.Rule("StartsWithString");
				this.StartsWithDigit = (BlackBoxRule)grammar.Rule("StartsWithDigit");
				this.StartsWithLetter = (BlackBoxRule)grammar.Rule("StartsWithLetter");
				this.EndsWithString = (BlackBoxRule)grammar.Rule("EndsWithString");
				this.EndsWithDigit = (BlackBoxRule)grammar.Rule("EndsWithDigit");
				this.EndsWithLetter = (BlackBoxRule)grammar.Rule("EndsWithLetter");
				this.ContainsString = (BlackBoxRule)grammar.Rule("ContainsString");
				this.Matches = (BlackBoxRule)grammar.Rule("Matches");
				this.StartsWith = (BlackBoxRule)grammar.Rule("StartsWith");
				this.EndsWith = (BlackBoxRule)grammar.Rule("EndsWith");
				this.Contains = (BlackBoxRule)grammar.Rule("Contains");
				this.Start = (ConversionRule)grammar.Rule("Start");
				this.ConvertDisjunctConjunct = (ConversionRule)grammar.Rule("ConvertDisjunctConjunct");
				this.Conjunct = (ConversionRule)grammar.Rule("Conjunct");
			}
		}

		// Token: 0x02000A1A RID: 2586
		public class GrammarUnnamedConversions
		{
			// Token: 0x17000B01 RID: 2817
			// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000C6B61 File Offset: 0x000C4D61
			// (set) Token: 0x06003E88 RID: 16008 RVA: 0x000C6B69 File Offset: 0x000C4D69
			public ConversionRule baseConjunct_pred { get; private set; }

			// Token: 0x17000B02 RID: 2818
			// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000C6B72 File Offset: 0x000C4D72
			// (set) Token: 0x06003E8A RID: 16010 RVA: 0x000C6B7A File Offset: 0x000C4D7A
			public ConversionRule pred_match { get; private set; }

			// Token: 0x06003E8B RID: 16011 RVA: 0x000C6B83 File Offset: 0x000C4D83
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.baseConjunct_pred = (ConversionRule)grammar.Rule("~convert_baseConjunct_pred");
				this.pred_match = (ConversionRule)grammar.Rule("~convert_pred_match");
			}
		}

		// Token: 0x02000A1B RID: 2587
		public class GrammarHoles
		{
			// Token: 0x17000B03 RID: 2819
			// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000C6BB7 File Offset: 0x000C4DB7
			// (set) Token: 0x06003E8D RID: 16013 RVA: 0x000C6BBF File Offset: 0x000C4DBF
			public Hole s { get; private set; }

			// Token: 0x17000B04 RID: 2820
			// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000C6BC8 File Offset: 0x000C4DC8
			// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000C6BD0 File Offset: 0x000C4DD0
			public Hole r { get; private set; }

			// Token: 0x17000B05 RID: 2821
			// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000C6BD9 File Offset: 0x000C4DD9
			// (set) Token: 0x06003E91 RID: 16017 RVA: 0x000C6BE1 File Offset: 0x000C4DE1
			public Hole k { get; private set; }

			// Token: 0x17000B06 RID: 2822
			// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000C6BEA File Offset: 0x000C4DEA
			// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000C6BF2 File Offset: 0x000C4DF2
			public Hole str { get; private set; }

			// Token: 0x17000B07 RID: 2823
			// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000C6BFB File Offset: 0x000C4DFB
			// (set) Token: 0x06003E95 RID: 16021 RVA: 0x000C6C03 File Offset: 0x000C4E03
			public Hole output { get; private set; }

			// Token: 0x17000B08 RID: 2824
			// (get) Token: 0x06003E96 RID: 16022 RVA: 0x000C6C0C File Offset: 0x000C4E0C
			// (set) Token: 0x06003E97 RID: 16023 RVA: 0x000C6C14 File Offset: 0x000C4E14
			public Hole disjunct { get; private set; }

			// Token: 0x17000B09 RID: 2825
			// (get) Token: 0x06003E98 RID: 16024 RVA: 0x000C6C1D File Offset: 0x000C4E1D
			// (set) Token: 0x06003E99 RID: 16025 RVA: 0x000C6C25 File Offset: 0x000C4E25
			public Hole conjunct { get; private set; }

			// Token: 0x17000B0A RID: 2826
			// (get) Token: 0x06003E9A RID: 16026 RVA: 0x000C6C2E File Offset: 0x000C4E2E
			// (set) Token: 0x06003E9B RID: 16027 RVA: 0x000C6C36 File Offset: 0x000C4E36
			public Hole baseConjunct { get; private set; }

			// Token: 0x17000B0B RID: 2827
			// (get) Token: 0x06003E9C RID: 16028 RVA: 0x000C6C3F File Offset: 0x000C4E3F
			// (set) Token: 0x06003E9D RID: 16029 RVA: 0x000C6C47 File Offset: 0x000C4E47
			public Hole pred { get; private set; }

			// Token: 0x17000B0C RID: 2828
			// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000C6C50 File Offset: 0x000C4E50
			// (set) Token: 0x06003E9F RID: 16031 RVA: 0x000C6C58 File Offset: 0x000C4E58
			public Hole match { get; private set; }

			// Token: 0x06003EA0 RID: 16032 RVA: 0x000C6C64 File Offset: 0x000C4E64
			public GrammarHoles(GrammarBuilders builders)
			{
				this.s = new Hole(builders.Symbol.s, null);
				this.r = new Hole(builders.Symbol.r, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.str = new Hole(builders.Symbol.str, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.disjunct = new Hole(builders.Symbol.disjunct, null);
				this.conjunct = new Hole(builders.Symbol.conjunct, null);
				this.baseConjunct = new Hole(builders.Symbol.baseConjunct, null);
				this.pred = new Hole(builders.Symbol.pred, null);
				this.match = new Hole(builders.Symbol.match, null);
			}
		}

		// Token: 0x02000A1C RID: 2588
		public class Nodes
		{
			// Token: 0x06003EA1 RID: 16033 RVA: 0x000C6D60 File Offset: 0x000C4F60
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

			// Token: 0x17000B0D RID: 2829
			// (get) Token: 0x06003EA2 RID: 16034 RVA: 0x000C6E43 File Offset: 0x000C5043
			// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x000C6E4B File Offset: 0x000C504B
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17000B0E RID: 2830
			// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000C6E54 File Offset: 0x000C5054
			// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x000C6E5C File Offset: 0x000C505C
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17000B0F RID: 2831
			// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000C6E65 File Offset: 0x000C5065
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17000B10 RID: 2832
			// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000C6E72 File Offset: 0x000C5072
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17000B11 RID: 2833
			// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000C6E7F File Offset: 0x000C507F
			// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000C6E87 File Offset: 0x000C5087
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17000B12 RID: 2834
			// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000C6E90 File Offset: 0x000C5090
			// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000C6E98 File Offset: 0x000C5098
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17000B13 RID: 2835
			// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000C6EA1 File Offset: 0x000C50A1
			// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000C6EA9 File Offset: 0x000C50A9
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17000B14 RID: 2836
			// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000C6EB2 File Offset: 0x000C50B2
			// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000C6EBA File Offset: 0x000C50BA
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17000B15 RID: 2837
			// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000C6EC3 File Offset: 0x000C50C3
			// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x000C6ECB File Offset: 0x000C50CB
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17000B16 RID: 2838
			// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000C6ED4 File Offset: 0x000C50D4
			// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000C6EDC File Offset: 0x000C50DC
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17000B17 RID: 2839
			// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000C6EE5 File Offset: 0x000C50E5
			// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000C6EED File Offset: 0x000C50ED
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04001D2E RID: 7470
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x04001D2F RID: 7471
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000A1D RID: 2589
			public class NodeRules
			{
				// Token: 0x06003EB6 RID: 16054 RVA: 0x000C6EF6 File Offset: 0x000C50F6
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003EB7 RID: 16055 RVA: 0x000C6F05 File Offset: 0x000C5105
				public r r(RegularExpression value)
				{
					return new r(this._builders, value);
				}

				// Token: 0x06003EB8 RID: 16056 RVA: 0x000C6F13 File Offset: 0x000C5113
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x06003EB9 RID: 16057 RVA: 0x000C6F21 File Offset: 0x000C5121
				public str str(string value)
				{
					return new str(this._builders, value);
				}

				// Token: 0x06003EBA RID: 16058 RVA: 0x000C6F2F File Offset: 0x000C512F
				public disjunct Disjunction(conjunct value0, disjunct value1)
				{
					return new Disjunction(this._builders, value0, value1);
				}

				// Token: 0x06003EBB RID: 16059 RVA: 0x000C6F43 File Offset: 0x000C5143
				public baseConjunct Conjunction(pred value0, baseConjunct value1)
				{
					return new Conjunction(this._builders, value0, value1);
				}

				// Token: 0x06003EBC RID: 16060 RVA: 0x000C6F57 File Offset: 0x000C5157
				public pred Not(match value0)
				{
					return new Not(this._builders, value0);
				}

				// Token: 0x06003EBD RID: 16061 RVA: 0x000C6F6A File Offset: 0x000C516A
				public match IsNullOrWhiteSpace(s value0)
				{
					return new IsNullOrWhiteSpace(this._builders, value0);
				}

				// Token: 0x06003EBE RID: 16062 RVA: 0x000C6F7D File Offset: 0x000C517D
				public match IsNull(s value0)
				{
					return new IsNull(this._builders, value0);
				}

				// Token: 0x06003EBF RID: 16063 RVA: 0x000C6F90 File Offset: 0x000C5190
				public match IsWhiteSpace(s value0)
				{
					return new IsWhiteSpace(this._builders, value0);
				}

				// Token: 0x06003EC0 RID: 16064 RVA: 0x000C6FA3 File Offset: 0x000C51A3
				public match True()
				{
					return new True(this._builders);
				}

				// Token: 0x06003EC1 RID: 16065 RVA: 0x000C6FB5 File Offset: 0x000C51B5
				public match StartsWithString(s value0, str value1)
				{
					return new StartsWithString(this._builders, value0, value1);
				}

				// Token: 0x06003EC2 RID: 16066 RVA: 0x000C6FC9 File Offset: 0x000C51C9
				public match StartsWithDigit(s value0)
				{
					return new StartsWithDigit(this._builders, value0);
				}

				// Token: 0x06003EC3 RID: 16067 RVA: 0x000C6FDC File Offset: 0x000C51DC
				public match StartsWithLetter(s value0)
				{
					return new StartsWithLetter(this._builders, value0);
				}

				// Token: 0x06003EC4 RID: 16068 RVA: 0x000C6FEF File Offset: 0x000C51EF
				public match EndsWithString(s value0, str value1)
				{
					return new EndsWithString(this._builders, value0, value1);
				}

				// Token: 0x06003EC5 RID: 16069 RVA: 0x000C7003 File Offset: 0x000C5203
				public match EndsWithDigit(s value0)
				{
					return new EndsWithDigit(this._builders, value0);
				}

				// Token: 0x06003EC6 RID: 16070 RVA: 0x000C7016 File Offset: 0x000C5216
				public match EndsWithLetter(s value0)
				{
					return new EndsWithLetter(this._builders, value0);
				}

				// Token: 0x06003EC7 RID: 16071 RVA: 0x000C7029 File Offset: 0x000C5229
				public match ContainsString(s value0, str value1, k value2)
				{
					return new ContainsString(this._builders, value0, value1, value2);
				}

				// Token: 0x06003EC8 RID: 16072 RVA: 0x000C703E File Offset: 0x000C523E
				public match Matches(s value0, r value1)
				{
					return new Matches(this._builders, value0, value1);
				}

				// Token: 0x06003EC9 RID: 16073 RVA: 0x000C7052 File Offset: 0x000C5252
				public match StartsWith(s value0, r value1)
				{
					return new StartsWith(this._builders, value0, value1);
				}

				// Token: 0x06003ECA RID: 16074 RVA: 0x000C7066 File Offset: 0x000C5266
				public match EndsWith(s value0, r value1)
				{
					return new EndsWith(this._builders, value0, value1);
				}

				// Token: 0x06003ECB RID: 16075 RVA: 0x000C707A File Offset: 0x000C527A
				public match Contains(s value0, r value1, k value2)
				{
					return new Contains(this._builders, value0, value1, value2);
				}

				// Token: 0x06003ECC RID: 16076 RVA: 0x000C708F File Offset: 0x000C528F
				public output Start(disjunct value0)
				{
					return new Start(this._builders, value0);
				}

				// Token: 0x06003ECD RID: 16077 RVA: 0x000C70A2 File Offset: 0x000C52A2
				public disjunct ConvertDisjunctConjunct(conjunct value0)
				{
					return new ConvertDisjunctConjunct(this._builders, value0);
				}

				// Token: 0x06003ECE RID: 16078 RVA: 0x000C70B5 File Offset: 0x000C52B5
				public conjunct Conjunct(baseConjunct value0)
				{
					return new Conjunct(this._builders, value0);
				}

				// Token: 0x04001D37 RID: 7479
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A1E RID: 2590
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06003ECF RID: 16079 RVA: 0x000C70C8 File Offset: 0x000C52C8
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003ED0 RID: 16080 RVA: 0x000C70D7 File Offset: 0x000C52D7
				public baseConjunct baseConjunct_pred(pred value0)
				{
					return new baseConjunct_pred(this._builders, value0);
				}

				// Token: 0x06003ED1 RID: 16081 RVA: 0x000C70EA File Offset: 0x000C52EA
				public pred pred_match(match value0)
				{
					return new pred_match(this._builders, value0);
				}

				// Token: 0x04001D38 RID: 7480
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A1F RID: 2591
			public class NodeVariables
			{
				// Token: 0x17000B18 RID: 2840
				// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000C70FD File Offset: 0x000C52FD
				// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x000C7105 File Offset: 0x000C5305
				public s s { get; private set; }

				// Token: 0x06003ED4 RID: 16084 RVA: 0x000C710E File Offset: 0x000C530E
				public NodeVariables(GrammarBuilders builders)
				{
					this.s = new s(builders);
				}
			}

			// Token: 0x02000A20 RID: 2592
			public class NodeHoles
			{
				// Token: 0x17000B19 RID: 2841
				// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000C7122 File Offset: 0x000C5322
				// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000C712A File Offset: 0x000C532A
				public r r { get; private set; }

				// Token: 0x17000B1A RID: 2842
				// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000C7133 File Offset: 0x000C5333
				// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000C713B File Offset: 0x000C533B
				public k k { get; private set; }

				// Token: 0x17000B1B RID: 2843
				// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000C7144 File Offset: 0x000C5344
				// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000C714C File Offset: 0x000C534C
				public str str { get; private set; }

				// Token: 0x17000B1C RID: 2844
				// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000C7155 File Offset: 0x000C5355
				// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000C715D File Offset: 0x000C535D
				public output output { get; private set; }

				// Token: 0x17000B1D RID: 2845
				// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000C7166 File Offset: 0x000C5366
				// (set) Token: 0x06003EDE RID: 16094 RVA: 0x000C716E File Offset: 0x000C536E
				public disjunct disjunct { get; private set; }

				// Token: 0x17000B1E RID: 2846
				// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000C7177 File Offset: 0x000C5377
				// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000C717F File Offset: 0x000C537F
				public conjunct conjunct { get; private set; }

				// Token: 0x17000B1F RID: 2847
				// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000C7188 File Offset: 0x000C5388
				// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000C7190 File Offset: 0x000C5390
				public baseConjunct baseConjunct { get; private set; }

				// Token: 0x17000B20 RID: 2848
				// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000C7199 File Offset: 0x000C5399
				// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000C71A1 File Offset: 0x000C53A1
				public pred pred { get; private set; }

				// Token: 0x17000B21 RID: 2849
				// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000C71AA File Offset: 0x000C53AA
				// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000C71B2 File Offset: 0x000C53B2
				public match match { get; private set; }

				// Token: 0x06003EE7 RID: 16103 RVA: 0x000C71BC File Offset: 0x000C53BC
				public NodeHoles(GrammarBuilders builders)
				{
					this.r = r.CreateHole(builders, null);
					this.k = k.CreateHole(builders, null);
					this.str = str.CreateHole(builders, null);
					this.output = output.CreateHole(builders, null);
					this.disjunct = disjunct.CreateHole(builders, null);
					this.conjunct = conjunct.CreateHole(builders, null);
					this.baseConjunct = baseConjunct.CreateHole(builders, null);
					this.pred = pred.CreateHole(builders, null);
					this.match = match.CreateHole(builders, null);
				}
			}

			// Token: 0x02000A21 RID: 2593
			public class NodeUnsafe
			{
				// Token: 0x06003EE8 RID: 16104 RVA: 0x000C7244 File Offset: 0x000C5444
				public r r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r.CreateUnsafe(node);
				}

				// Token: 0x06003EE9 RID: 16105 RVA: 0x000C724C File Offset: 0x000C544C
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x06003EEA RID: 16106 RVA: 0x000C7254 File Offset: 0x000C5454
				public str str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str.CreateUnsafe(node);
				}

				// Token: 0x06003EEB RID: 16107 RVA: 0x000C725C File Offset: 0x000C545C
				public output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x06003EEC RID: 16108 RVA: 0x000C7264 File Offset: 0x000C5464
				public disjunct disjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct.CreateUnsafe(node);
				}

				// Token: 0x06003EED RID: 16109 RVA: 0x000C726C File Offset: 0x000C546C
				public conjunct conjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct.CreateUnsafe(node);
				}

				// Token: 0x06003EEE RID: 16110 RVA: 0x000C7274 File Offset: 0x000C5474
				public baseConjunct baseConjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct.CreateUnsafe(node);
				}

				// Token: 0x06003EEF RID: 16111 RVA: 0x000C727C File Offset: 0x000C547C
				public pred pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred.CreateUnsafe(node);
				}

				// Token: 0x06003EF0 RID: 16112 RVA: 0x000C7284 File Offset: 0x000C5484
				public match match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match.CreateUnsafe(node);
				}
			}

			// Token: 0x02000A22 RID: 2594
			public class NodeCast
			{
				// Token: 0x06003EF2 RID: 16114 RVA: 0x000C728C File Offset: 0x000C548C
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003EF3 RID: 16115 RVA: 0x000C729C File Offset: 0x000C549C
				public r r(ProgramNode node)
				{
					r? r = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						string text = "node";
						string text2 = "expected node for symbol r but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return r.Value;
				}

				// Token: 0x06003EF4 RID: 16116 RVA: 0x000C72F0 File Offset: 0x000C54F0
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x06003EF5 RID: 16117 RVA: 0x000C7344 File Offset: 0x000C5544
				public str str(ProgramNode node)
				{
					str? str = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						string text = "node";
						string text2 = "expected node for symbol str but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return str.Value;
				}

				// Token: 0x06003EF6 RID: 16118 RVA: 0x000C7398 File Offset: 0x000C5598
				public output output(ProgramNode node)
				{
					output? output = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x06003EF7 RID: 16119 RVA: 0x000C73EC File Offset: 0x000C55EC
				public disjunct disjunct(ProgramNode node)
				{
					disjunct? disjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct.CreateSafe(this._builders, node);
					if (disjunct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol disjunct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjunct.Value;
				}

				// Token: 0x06003EF8 RID: 16120 RVA: 0x000C7440 File Offset: 0x000C5640
				public conjunct conjunct(ProgramNode node)
				{
					conjunct? conjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct.CreateSafe(this._builders, node);
					if (conjunct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol conjunct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conjunct.Value;
				}

				// Token: 0x06003EF9 RID: 16121 RVA: 0x000C7494 File Offset: 0x000C5694
				public baseConjunct baseConjunct(ProgramNode node)
				{
					baseConjunct? baseConjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct.CreateSafe(this._builders, node);
					if (baseConjunct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol baseConjunct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return baseConjunct.Value;
				}

				// Token: 0x06003EFA RID: 16122 RVA: 0x000C74E8 File Offset: 0x000C56E8
				public pred pred(ProgramNode node)
				{
					pred? pred = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pred.Value;
				}

				// Token: 0x06003EFB RID: 16123 RVA: 0x000C753C File Offset: 0x000C573C
				public match match(ProgramNode node)
				{
					match? match = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return match.Value;
				}

				// Token: 0x04001D43 RID: 7491
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A23 RID: 2595
			public class RuleCast
			{
				// Token: 0x06003EFC RID: 16124 RVA: 0x000C758D File Offset: 0x000C578D
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003EFD RID: 16125 RVA: 0x000C759C File Offset: 0x000C579C
				public Start Start(ProgramNode node)
				{
					Start? start = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Start.CreateSafe(this._builders, node);
					if (start == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Start but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return start.Value;
				}

				// Token: 0x06003EFE RID: 16126 RVA: 0x000C75F0 File Offset: 0x000C57F0
				public ConvertDisjunctConjunct ConvertDisjunctConjunct(ProgramNode node)
				{
					ConvertDisjunctConjunct? convertDisjunctConjunct = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ConvertDisjunctConjunct.CreateSafe(this._builders, node);
					if (convertDisjunctConjunct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConvertDisjunctConjunct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return convertDisjunctConjunct.Value;
				}

				// Token: 0x06003EFF RID: 16127 RVA: 0x000C7644 File Offset: 0x000C5844
				public Disjunction Disjunction(ProgramNode node)
				{
					Disjunction? disjunction = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
					if (disjunction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Disjunction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return disjunction.Value;
				}

				// Token: 0x06003F00 RID: 16128 RVA: 0x000C7698 File Offset: 0x000C5898
				public Conjunct Conjunct(ProgramNode node)
				{
					Conjunct? conjunct = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunct.CreateSafe(this._builders, node);
					if (conjunct == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Conjunct but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conjunct.Value;
				}

				// Token: 0x06003F01 RID: 16129 RVA: 0x000C76EC File Offset: 0x000C58EC
				public baseConjunct_pred baseConjunct_pred(ProgramNode node)
				{
					baseConjunct_pred? baseConjunct_pred = Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.baseConjunct_pred.CreateSafe(this._builders, node);
					if (baseConjunct_pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol baseConjunct_pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return baseConjunct_pred.Value;
				}

				// Token: 0x06003F02 RID: 16130 RVA: 0x000C7740 File Offset: 0x000C5940
				public Conjunction Conjunction(ProgramNode node)
				{
					Conjunction? conjunction = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunction.CreateSafe(this._builders, node);
					if (conjunction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Conjunction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conjunction.Value;
				}

				// Token: 0x06003F03 RID: 16131 RVA: 0x000C7794 File Offset: 0x000C5994
				public pred_match pred_match(ProgramNode node)
				{
					pred_match? pred_match = Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.pred_match.CreateSafe(this._builders, node);
					if (pred_match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pred_match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pred_match.Value;
				}

				// Token: 0x06003F04 RID: 16132 RVA: 0x000C77E8 File Offset: 0x000C59E8
				public Not Not(ProgramNode node)
				{
					Not? not = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
					if (not == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Not but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return not.Value;
				}

				// Token: 0x06003F05 RID: 16133 RVA: 0x000C783C File Offset: 0x000C5A3C
				public IsNullOrWhiteSpace IsNullOrWhiteSpace(ProgramNode node)
				{
					IsNullOrWhiteSpace? isNullOrWhiteSpace = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNullOrWhiteSpace.CreateSafe(this._builders, node);
					if (isNullOrWhiteSpace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNullOrWhiteSpace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNullOrWhiteSpace.Value;
				}

				// Token: 0x06003F06 RID: 16134 RVA: 0x000C7890 File Offset: 0x000C5A90
				public IsNull IsNull(ProgramNode node)
				{
					IsNull? isNull = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
					if (isNull == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNull but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNull.Value;
				}

				// Token: 0x06003F07 RID: 16135 RVA: 0x000C78E4 File Offset: 0x000C5AE4
				public IsWhiteSpace IsWhiteSpace(ProgramNode node)
				{
					IsWhiteSpace? isWhiteSpace = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsWhiteSpace.CreateSafe(this._builders, node);
					if (isWhiteSpace == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsWhiteSpace but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isWhiteSpace.Value;
				}

				// Token: 0x06003F08 RID: 16136 RVA: 0x000C7938 File Offset: 0x000C5B38
				public True True(ProgramNode node)
				{
					True? @true = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.True.CreateSafe(this._builders, node);
					if (@true == null)
					{
						string text = "node";
						string text2 = "expected node for symbol True but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @true.Value;
				}

				// Token: 0x06003F09 RID: 16137 RVA: 0x000C798C File Offset: 0x000C5B8C
				public StartsWithString StartsWithString(ProgramNode node)
				{
					StartsWithString? startsWithString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithString.CreateSafe(this._builders, node);
					if (startsWithString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWithString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWithString.Value;
				}

				// Token: 0x06003F0A RID: 16138 RVA: 0x000C79E0 File Offset: 0x000C5BE0
				public StartsWithDigit StartsWithDigit(ProgramNode node)
				{
					StartsWithDigit? startsWithDigit = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
					if (startsWithDigit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWithDigit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWithDigit.Value;
				}

				// Token: 0x06003F0B RID: 16139 RVA: 0x000C7A34 File Offset: 0x000C5C34
				public StartsWithLetter StartsWithLetter(ProgramNode node)
				{
					StartsWithLetter? startsWithLetter = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithLetter.CreateSafe(this._builders, node);
					if (startsWithLetter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWithLetter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWithLetter.Value;
				}

				// Token: 0x06003F0C RID: 16140 RVA: 0x000C7A88 File Offset: 0x000C5C88
				public EndsWithString EndsWithString(ProgramNode node)
				{
					EndsWithString? endsWithString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithString.CreateSafe(this._builders, node);
					if (endsWithString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndsWithString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endsWithString.Value;
				}

				// Token: 0x06003F0D RID: 16141 RVA: 0x000C7ADC File Offset: 0x000C5CDC
				public EndsWithDigit EndsWithDigit(ProgramNode node)
				{
					EndsWithDigit? endsWithDigit = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
					if (endsWithDigit == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndsWithDigit but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endsWithDigit.Value;
				}

				// Token: 0x06003F0E RID: 16142 RVA: 0x000C7B30 File Offset: 0x000C5D30
				public EndsWithLetter EndsWithLetter(ProgramNode node)
				{
					EndsWithLetter? endsWithLetter = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithLetter.CreateSafe(this._builders, node);
					if (endsWithLetter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndsWithLetter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endsWithLetter.Value;
				}

				// Token: 0x06003F0F RID: 16143 RVA: 0x000C7B84 File Offset: 0x000C5D84
				public ContainsString ContainsString(ProgramNode node)
				{
					ContainsString? containsString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ContainsString.CreateSafe(this._builders, node);
					if (containsString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ContainsString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return containsString.Value;
				}

				// Token: 0x06003F10 RID: 16144 RVA: 0x000C7BD8 File Offset: 0x000C5DD8
				public Matches Matches(ProgramNode node)
				{
					Matches? matches = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Matches.CreateSafe(this._builders, node);
					if (matches == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Matches but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return matches.Value;
				}

				// Token: 0x06003F11 RID: 16145 RVA: 0x000C7C2C File Offset: 0x000C5E2C
				public StartsWith StartsWith(ProgramNode node)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						string text = "node";
						string text2 = "expected node for symbol StartsWith but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return startsWith.Value;
				}

				// Token: 0x06003F12 RID: 16146 RVA: 0x000C7C80 File Offset: 0x000C5E80
				public EndsWith EndsWith(ProgramNode node)
				{
					EndsWith? endsWith = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWith.CreateSafe(this._builders, node);
					if (endsWith == null)
					{
						string text = "node";
						string text2 = "expected node for symbol EndsWith but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return endsWith.Value;
				}

				// Token: 0x06003F13 RID: 16147 RVA: 0x000C7CD4 File Offset: 0x000C5ED4
				public Contains Contains(ProgramNode node)
				{
					Contains? contains = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
					if (contains == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Contains but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return contains.Value;
				}

				// Token: 0x04001D44 RID: 7492
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A24 RID: 2596
			public class NodeIs
			{
				// Token: 0x06003F14 RID: 16148 RVA: 0x000C7D25 File Offset: 0x000C5F25
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F15 RID: 16149 RVA: 0x000C7D34 File Offset: 0x000C5F34
				public bool r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F16 RID: 16150 RVA: 0x000C7D58 File Offset: 0x000C5F58
				public bool r(ProgramNode node, out r value)
				{
					r? r = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r.CreateSafe(this._builders, node);
					if (r == null)
					{
						value = default(r);
						return false;
					}
					value = r.Value;
					return true;
				}

				// Token: 0x06003F17 RID: 16151 RVA: 0x000C7D94 File Offset: 0x000C5F94
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F18 RID: 16152 RVA: 0x000C7DB8 File Offset: 0x000C5FB8
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x06003F19 RID: 16153 RVA: 0x000C7DF4 File Offset: 0x000C5FF4
				public bool str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F1A RID: 16154 RVA: 0x000C7E18 File Offset: 0x000C6018
				public bool str(ProgramNode node, out str value)
				{
					str? str = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						value = default(str);
						return false;
					}
					value = str.Value;
					return true;
				}

				// Token: 0x06003F1B RID: 16155 RVA: 0x000C7E54 File Offset: 0x000C6054
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F1C RID: 16156 RVA: 0x000C7E78 File Offset: 0x000C6078
				public bool output(ProgramNode node, out output value)
				{
					output? output = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x06003F1D RID: 16157 RVA: 0x000C7EB4 File Offset: 0x000C60B4
				public bool disjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F1E RID: 16158 RVA: 0x000C7ED8 File Offset: 0x000C60D8
				public bool disjunct(ProgramNode node, out disjunct value)
				{
					disjunct? disjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct.CreateSafe(this._builders, node);
					if (disjunct == null)
					{
						value = default(disjunct);
						return false;
					}
					value = disjunct.Value;
					return true;
				}

				// Token: 0x06003F1F RID: 16159 RVA: 0x000C7F14 File Offset: 0x000C6114
				public bool conjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F20 RID: 16160 RVA: 0x000C7F38 File Offset: 0x000C6138
				public bool conjunct(ProgramNode node, out conjunct value)
				{
					conjunct? conjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct.CreateSafe(this._builders, node);
					if (conjunct == null)
					{
						value = default(conjunct);
						return false;
					}
					value = conjunct.Value;
					return true;
				}

				// Token: 0x06003F21 RID: 16161 RVA: 0x000C7F74 File Offset: 0x000C6174
				public bool baseConjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F22 RID: 16162 RVA: 0x000C7F98 File Offset: 0x000C6198
				public bool baseConjunct(ProgramNode node, out baseConjunct value)
				{
					baseConjunct? baseConjunct = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct.CreateSafe(this._builders, node);
					if (baseConjunct == null)
					{
						value = default(baseConjunct);
						return false;
					}
					value = baseConjunct.Value;
					return true;
				}

				// Token: 0x06003F23 RID: 16163 RVA: 0x000C7FD4 File Offset: 0x000C61D4
				public bool pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F24 RID: 16164 RVA: 0x000C7FF8 File Offset: 0x000C61F8
				public bool pred(ProgramNode node, out pred value)
				{
					pred? pred = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						value = default(pred);
						return false;
					}
					value = pred.Value;
					return true;
				}

				// Token: 0x06003F25 RID: 16165 RVA: 0x000C8034 File Offset: 0x000C6234
				public bool match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F26 RID: 16166 RVA: 0x000C8058 File Offset: 0x000C6258
				public bool match(ProgramNode node, out match value)
				{
					match? match = Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						value = default(match);
						return false;
					}
					value = match.Value;
					return true;
				}

				// Token: 0x04001D45 RID: 7493
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A25 RID: 2597
			public class RuleIs
			{
				// Token: 0x06003F27 RID: 16167 RVA: 0x000C8092 File Offset: 0x000C6292
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F28 RID: 16168 RVA: 0x000C80A4 File Offset: 0x000C62A4
				public bool Start(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Start.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F29 RID: 16169 RVA: 0x000C80C8 File Offset: 0x000C62C8
				public bool Start(ProgramNode node, out Start value)
				{
					Start? start = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Start.CreateSafe(this._builders, node);
					if (start == null)
					{
						value = default(Start);
						return false;
					}
					value = start.Value;
					return true;
				}

				// Token: 0x06003F2A RID: 16170 RVA: 0x000C8104 File Offset: 0x000C6304
				public bool ConvertDisjunctConjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ConvertDisjunctConjunct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F2B RID: 16171 RVA: 0x000C8128 File Offset: 0x000C6328
				public bool ConvertDisjunctConjunct(ProgramNode node, out ConvertDisjunctConjunct value)
				{
					ConvertDisjunctConjunct? convertDisjunctConjunct = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ConvertDisjunctConjunct.CreateSafe(this._builders, node);
					if (convertDisjunctConjunct == null)
					{
						value = default(ConvertDisjunctConjunct);
						return false;
					}
					value = convertDisjunctConjunct.Value;
					return true;
				}

				// Token: 0x06003F2C RID: 16172 RVA: 0x000C8164 File Offset: 0x000C6364
				public bool Disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F2D RID: 16173 RVA: 0x000C8188 File Offset: 0x000C6388
				public bool Disjunction(ProgramNode node, out Disjunction value)
				{
					Disjunction? disjunction = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
					if (disjunction == null)
					{
						value = default(Disjunction);
						return false;
					}
					value = disjunction.Value;
					return true;
				}

				// Token: 0x06003F2E RID: 16174 RVA: 0x000C81C4 File Offset: 0x000C63C4
				public bool Conjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunct.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F2F RID: 16175 RVA: 0x000C81E8 File Offset: 0x000C63E8
				public bool Conjunct(ProgramNode node, out Conjunct value)
				{
					Conjunct? conjunct = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunct.CreateSafe(this._builders, node);
					if (conjunct == null)
					{
						value = default(Conjunct);
						return false;
					}
					value = conjunct.Value;
					return true;
				}

				// Token: 0x06003F30 RID: 16176 RVA: 0x000C8224 File Offset: 0x000C6424
				public bool baseConjunct_pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.baseConjunct_pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F31 RID: 16177 RVA: 0x000C8248 File Offset: 0x000C6448
				public bool baseConjunct_pred(ProgramNode node, out baseConjunct_pred value)
				{
					baseConjunct_pred? baseConjunct_pred = Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.baseConjunct_pred.CreateSafe(this._builders, node);
					if (baseConjunct_pred == null)
					{
						value = default(baseConjunct_pred);
						return false;
					}
					value = baseConjunct_pred.Value;
					return true;
				}

				// Token: 0x06003F32 RID: 16178 RVA: 0x000C8284 File Offset: 0x000C6484
				public bool Conjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F33 RID: 16179 RVA: 0x000C82A8 File Offset: 0x000C64A8
				public bool Conjunction(ProgramNode node, out Conjunction value)
				{
					Conjunction? conjunction = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunction.CreateSafe(this._builders, node);
					if (conjunction == null)
					{
						value = default(Conjunction);
						return false;
					}
					value = conjunction.Value;
					return true;
				}

				// Token: 0x06003F34 RID: 16180 RVA: 0x000C82E4 File Offset: 0x000C64E4
				public bool pred_match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.pred_match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F35 RID: 16181 RVA: 0x000C8308 File Offset: 0x000C6508
				public bool pred_match(ProgramNode node, out pred_match value)
				{
					pred_match? pred_match = Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.pred_match.CreateSafe(this._builders, node);
					if (pred_match == null)
					{
						value = default(pred_match);
						return false;
					}
					value = pred_match.Value;
					return true;
				}

				// Token: 0x06003F36 RID: 16182 RVA: 0x000C8344 File Offset: 0x000C6544
				public bool Not(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F37 RID: 16183 RVA: 0x000C8368 File Offset: 0x000C6568
				public bool Not(ProgramNode node, out Not value)
				{
					Not? not = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
					if (not == null)
					{
						value = default(Not);
						return false;
					}
					value = not.Value;
					return true;
				}

				// Token: 0x06003F38 RID: 16184 RVA: 0x000C83A4 File Offset: 0x000C65A4
				public bool IsNullOrWhiteSpace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNullOrWhiteSpace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F39 RID: 16185 RVA: 0x000C83C8 File Offset: 0x000C65C8
				public bool IsNullOrWhiteSpace(ProgramNode node, out IsNullOrWhiteSpace value)
				{
					IsNullOrWhiteSpace? isNullOrWhiteSpace = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNullOrWhiteSpace.CreateSafe(this._builders, node);
					if (isNullOrWhiteSpace == null)
					{
						value = default(IsNullOrWhiteSpace);
						return false;
					}
					value = isNullOrWhiteSpace.Value;
					return true;
				}

				// Token: 0x06003F3A RID: 16186 RVA: 0x000C8404 File Offset: 0x000C6604
				public bool IsNull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F3B RID: 16187 RVA: 0x000C8428 File Offset: 0x000C6628
				public bool IsNull(ProgramNode node, out IsNull value)
				{
					IsNull? isNull = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
					if (isNull == null)
					{
						value = default(IsNull);
						return false;
					}
					value = isNull.Value;
					return true;
				}

				// Token: 0x06003F3C RID: 16188 RVA: 0x000C8464 File Offset: 0x000C6664
				public bool IsWhiteSpace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsWhiteSpace.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F3D RID: 16189 RVA: 0x000C8488 File Offset: 0x000C6688
				public bool IsWhiteSpace(ProgramNode node, out IsWhiteSpace value)
				{
					IsWhiteSpace? isWhiteSpace = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsWhiteSpace.CreateSafe(this._builders, node);
					if (isWhiteSpace == null)
					{
						value = default(IsWhiteSpace);
						return false;
					}
					value = isWhiteSpace.Value;
					return true;
				}

				// Token: 0x06003F3E RID: 16190 RVA: 0x000C84C4 File Offset: 0x000C66C4
				public bool True(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.True.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F3F RID: 16191 RVA: 0x000C84E8 File Offset: 0x000C66E8
				public bool True(ProgramNode node, out True value)
				{
					True? @true = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.True.CreateSafe(this._builders, node);
					if (@true == null)
					{
						value = default(True);
						return false;
					}
					value = @true.Value;
					return true;
				}

				// Token: 0x06003F40 RID: 16192 RVA: 0x000C8524 File Offset: 0x000C6724
				public bool StartsWithString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F41 RID: 16193 RVA: 0x000C8548 File Offset: 0x000C6748
				public bool StartsWithString(ProgramNode node, out StartsWithString value)
				{
					StartsWithString? startsWithString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithString.CreateSafe(this._builders, node);
					if (startsWithString == null)
					{
						value = default(StartsWithString);
						return false;
					}
					value = startsWithString.Value;
					return true;
				}

				// Token: 0x06003F42 RID: 16194 RVA: 0x000C8584 File Offset: 0x000C6784
				public bool StartsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F43 RID: 16195 RVA: 0x000C85A8 File Offset: 0x000C67A8
				public bool StartsWithDigit(ProgramNode node, out StartsWithDigit value)
				{
					StartsWithDigit? startsWithDigit = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
					if (startsWithDigit == null)
					{
						value = default(StartsWithDigit);
						return false;
					}
					value = startsWithDigit.Value;
					return true;
				}

				// Token: 0x06003F44 RID: 16196 RVA: 0x000C85E4 File Offset: 0x000C67E4
				public bool StartsWithLetter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithLetter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F45 RID: 16197 RVA: 0x000C8608 File Offset: 0x000C6808
				public bool StartsWithLetter(ProgramNode node, out StartsWithLetter value)
				{
					StartsWithLetter? startsWithLetter = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithLetter.CreateSafe(this._builders, node);
					if (startsWithLetter == null)
					{
						value = default(StartsWithLetter);
						return false;
					}
					value = startsWithLetter.Value;
					return true;
				}

				// Token: 0x06003F46 RID: 16198 RVA: 0x000C8644 File Offset: 0x000C6844
				public bool EndsWithString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F47 RID: 16199 RVA: 0x000C8668 File Offset: 0x000C6868
				public bool EndsWithString(ProgramNode node, out EndsWithString value)
				{
					EndsWithString? endsWithString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithString.CreateSafe(this._builders, node);
					if (endsWithString == null)
					{
						value = default(EndsWithString);
						return false;
					}
					value = endsWithString.Value;
					return true;
				}

				// Token: 0x06003F48 RID: 16200 RVA: 0x000C86A4 File Offset: 0x000C68A4
				public bool EndsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F49 RID: 16201 RVA: 0x000C86C8 File Offset: 0x000C68C8
				public bool EndsWithDigit(ProgramNode node, out EndsWithDigit value)
				{
					EndsWithDigit? endsWithDigit = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
					if (endsWithDigit == null)
					{
						value = default(EndsWithDigit);
						return false;
					}
					value = endsWithDigit.Value;
					return true;
				}

				// Token: 0x06003F4A RID: 16202 RVA: 0x000C8704 File Offset: 0x000C6904
				public bool EndsWithLetter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithLetter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F4B RID: 16203 RVA: 0x000C8728 File Offset: 0x000C6928
				public bool EndsWithLetter(ProgramNode node, out EndsWithLetter value)
				{
					EndsWithLetter? endsWithLetter = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithLetter.CreateSafe(this._builders, node);
					if (endsWithLetter == null)
					{
						value = default(EndsWithLetter);
						return false;
					}
					value = endsWithLetter.Value;
					return true;
				}

				// Token: 0x06003F4C RID: 16204 RVA: 0x000C8764 File Offset: 0x000C6964
				public bool ContainsString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ContainsString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F4D RID: 16205 RVA: 0x000C8788 File Offset: 0x000C6988
				public bool ContainsString(ProgramNode node, out ContainsString value)
				{
					ContainsString? containsString = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ContainsString.CreateSafe(this._builders, node);
					if (containsString == null)
					{
						value = default(ContainsString);
						return false;
					}
					value = containsString.Value;
					return true;
				}

				// Token: 0x06003F4E RID: 16206 RVA: 0x000C87C4 File Offset: 0x000C69C4
				public bool Matches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Matches.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F4F RID: 16207 RVA: 0x000C87E8 File Offset: 0x000C69E8
				public bool Matches(ProgramNode node, out Matches value)
				{
					Matches? matches = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Matches.CreateSafe(this._builders, node);
					if (matches == null)
					{
						value = default(Matches);
						return false;
					}
					value = matches.Value;
					return true;
				}

				// Token: 0x06003F50 RID: 16208 RVA: 0x000C8824 File Offset: 0x000C6A24
				public bool StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F51 RID: 16209 RVA: 0x000C8848 File Offset: 0x000C6A48
				public bool StartsWith(ProgramNode node, out StartsWith value)
				{
					StartsWith? startsWith = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
					if (startsWith == null)
					{
						value = default(StartsWith);
						return false;
					}
					value = startsWith.Value;
					return true;
				}

				// Token: 0x06003F52 RID: 16210 RVA: 0x000C8884 File Offset: 0x000C6A84
				public bool EndsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWith.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F53 RID: 16211 RVA: 0x000C88A8 File Offset: 0x000C6AA8
				public bool EndsWith(ProgramNode node, out EndsWith value)
				{
					EndsWith? endsWith = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWith.CreateSafe(this._builders, node);
					if (endsWith == null)
					{
						value = default(EndsWith);
						return false;
					}
					value = endsWith.Value;
					return true;
				}

				// Token: 0x06003F54 RID: 16212 RVA: 0x000C88E4 File Offset: 0x000C6AE4
				public bool Contains(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06003F55 RID: 16213 RVA: 0x000C8908 File Offset: 0x000C6B08
				public bool Contains(ProgramNode node, out Contains value)
				{
					Contains? contains = Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
					if (contains == null)
					{
						value = default(Contains);
						return false;
					}
					value = contains.Value;
					return true;
				}

				// Token: 0x04001D46 RID: 7494
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A26 RID: 2598
			public class NodeAs
			{
				// Token: 0x06003F56 RID: 16214 RVA: 0x000C8942 File Offset: 0x000C6B42
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F57 RID: 16215 RVA: 0x000C8951 File Offset: 0x000C6B51
				public r? r(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F58 RID: 16216 RVA: 0x000C895F File Offset: 0x000C6B5F
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F59 RID: 16217 RVA: 0x000C896D File Offset: 0x000C6B6D
				public str? str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5A RID: 16218 RVA: 0x000C897B File Offset: 0x000C6B7B
				public output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5B RID: 16219 RVA: 0x000C8989 File Offset: 0x000C6B89
				public disjunct? disjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5C RID: 16220 RVA: 0x000C8997 File Offset: 0x000C6B97
				public conjunct? conjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5D RID: 16221 RVA: 0x000C89A5 File Offset: 0x000C6BA5
				public baseConjunct? baseConjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5E RID: 16222 RVA: 0x000C89B3 File Offset: 0x000C6BB3
				public pred? pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F5F RID: 16223 RVA: 0x000C89C1 File Offset: 0x000C6BC1
				public match? match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match.CreateSafe(this._builders, node);
				}

				// Token: 0x04001D47 RID: 7495
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A27 RID: 2599
			public class RuleAs
			{
				// Token: 0x06003F60 RID: 16224 RVA: 0x000C89CF File Offset: 0x000C6BCF
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F61 RID: 16225 RVA: 0x000C89DE File Offset: 0x000C6BDE
				public Start? Start(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Start.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F62 RID: 16226 RVA: 0x000C89EC File Offset: 0x000C6BEC
				public ConvertDisjunctConjunct? ConvertDisjunctConjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ConvertDisjunctConjunct.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F63 RID: 16227 RVA: 0x000C89FA File Offset: 0x000C6BFA
				public Disjunction? Disjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Disjunction.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F64 RID: 16228 RVA: 0x000C8A08 File Offset: 0x000C6C08
				public Conjunct? Conjunct(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunct.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F65 RID: 16229 RVA: 0x000C8A16 File Offset: 0x000C6C16
				public baseConjunct_pred? baseConjunct_pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.baseConjunct_pred.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F66 RID: 16230 RVA: 0x000C8A24 File Offset: 0x000C6C24
				public Conjunction? Conjunction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Conjunction.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F67 RID: 16231 RVA: 0x000C8A32 File Offset: 0x000C6C32
				public pred_match? pred_match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes.pred_match.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F68 RID: 16232 RVA: 0x000C8A40 File Offset: 0x000C6C40
				public Not? Not(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F69 RID: 16233 RVA: 0x000C8A4E File Offset: 0x000C6C4E
				public IsNullOrWhiteSpace? IsNullOrWhiteSpace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNullOrWhiteSpace.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6A RID: 16234 RVA: 0x000C8A5C File Offset: 0x000C6C5C
				public IsNull? IsNull(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsNull.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6B RID: 16235 RVA: 0x000C8A6A File Offset: 0x000C6C6A
				public IsWhiteSpace? IsWhiteSpace(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.IsWhiteSpace.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6C RID: 16236 RVA: 0x000C8A78 File Offset: 0x000C6C78
				public True? True(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.True.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6D RID: 16237 RVA: 0x000C8A86 File Offset: 0x000C6C86
				public StartsWithString? StartsWithString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithString.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6E RID: 16238 RVA: 0x000C8A94 File Offset: 0x000C6C94
				public StartsWithDigit? StartsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithDigit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F6F RID: 16239 RVA: 0x000C8AA2 File Offset: 0x000C6CA2
				public StartsWithLetter? StartsWithLetter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWithLetter.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F70 RID: 16240 RVA: 0x000C8AB0 File Offset: 0x000C6CB0
				public EndsWithString? EndsWithString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithString.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F71 RID: 16241 RVA: 0x000C8ABE File Offset: 0x000C6CBE
				public EndsWithDigit? EndsWithDigit(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithDigit.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F72 RID: 16242 RVA: 0x000C8ACC File Offset: 0x000C6CCC
				public EndsWithLetter? EndsWithLetter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWithLetter.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F73 RID: 16243 RVA: 0x000C8ADA File Offset: 0x000C6CDA
				public ContainsString? ContainsString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.ContainsString.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F74 RID: 16244 RVA: 0x000C8AE8 File Offset: 0x000C6CE8
				public Matches? Matches(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Matches.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F75 RID: 16245 RVA: 0x000C8AF6 File Offset: 0x000C6CF6
				public StartsWith? StartsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.StartsWith.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F76 RID: 16246 RVA: 0x000C8B04 File Offset: 0x000C6D04
				public EndsWith? EndsWith(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.EndsWith.CreateSafe(this._builders, node);
				}

				// Token: 0x06003F77 RID: 16247 RVA: 0x000C8B12 File Offset: 0x000C6D12
				public Contains? Contains(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes.Contains.CreateSafe(this._builders, node);
				}

				// Token: 0x04001D48 RID: 7496
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000A29 RID: 2601
		public class Sets
		{
			// Token: 0x06003F7B RID: 16251 RVA: 0x000C8B3C File Offset: 0x000C6D3C
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17000B22 RID: 2850
			// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000C8B8B File Offset: 0x000C6D8B
			// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000C8B93 File Offset: 0x000C6D93
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17000B23 RID: 2851
			// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000C8B9C File Offset: 0x000C6D9C
			// (set) Token: 0x06003F7F RID: 16255 RVA: 0x000C8BA4 File Offset: 0x000C6DA4
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17000B24 RID: 2852
			// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000C8BAD File Offset: 0x000C6DAD
			// (set) Token: 0x06003F81 RID: 16257 RVA: 0x000C8BB5 File Offset: 0x000C6DB5
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17000B25 RID: 2853
			// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000C8BBE File Offset: 0x000C6DBE
			// (set) Token: 0x06003F83 RID: 16259 RVA: 0x000C8BC6 File Offset: 0x000C6DC6
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000C8BCF File Offset: 0x000C6DCF
			// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000C8BD7 File Offset: 0x000C6DD7
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000A2A RID: 2602
			public class Joins
			{
				// Token: 0x06003F86 RID: 16262 RVA: 0x000C8BE0 File Offset: 0x000C6DE0
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F87 RID: 16263 RVA: 0x000C8BEF File Offset: 0x000C6DEF
				public ProgramSetBuilder<disjunct> Disjunction(ProgramSetBuilder<conjunct> value0, ProgramSetBuilder<disjunct> value1)
				{
					return ProgramSetBuilder<disjunct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Disjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F88 RID: 16264 RVA: 0x000C8C2F File Offset: 0x000C6E2F
				public ProgramSetBuilder<baseConjunct> Conjunction(ProgramSetBuilder<pred> value0, ProgramSetBuilder<baseConjunct> value1)
				{
					return ProgramSetBuilder<baseConjunct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Conjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F89 RID: 16265 RVA: 0x000C8C6F File Offset: 0x000C6E6F
				public ProgramSetBuilder<pred> Not(ProgramSetBuilder<match> value0)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Not, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F8A RID: 16266 RVA: 0x000C8CA0 File Offset: 0x000C6EA0
				public ProgramSetBuilder<match> IsNullOrWhiteSpace(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNullOrWhiteSpace, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F8B RID: 16267 RVA: 0x000C8CD1 File Offset: 0x000C6ED1
				public ProgramSetBuilder<match> IsNull(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F8C RID: 16268 RVA: 0x000C8D02 File Offset: 0x000C6F02
				public ProgramSetBuilder<match> IsWhiteSpace(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsWhiteSpace, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F8D RID: 16269 RVA: 0x000C8D33 File Offset: 0x000C6F33
				public ProgramSetBuilder<match> True()
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.True, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06003F8E RID: 16270 RVA: 0x000C8D54 File Offset: 0x000C6F54
				public ProgramSetBuilder<match> StartsWithString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWithString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F8F RID: 16271 RVA: 0x000C8D94 File Offset: 0x000C6F94
				public ProgramSetBuilder<match> StartsWithDigit(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWithDigit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F90 RID: 16272 RVA: 0x000C8DC5 File Offset: 0x000C6FC5
				public ProgramSetBuilder<match> StartsWithLetter(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWithLetter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F91 RID: 16273 RVA: 0x000C8DF6 File Offset: 0x000C6FF6
				public ProgramSetBuilder<match> EndsWithString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndsWithString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F92 RID: 16274 RVA: 0x000C8E36 File Offset: 0x000C7036
				public ProgramSetBuilder<match> EndsWithDigit(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndsWithDigit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F93 RID: 16275 RVA: 0x000C8E67 File Offset: 0x000C7067
				public ProgramSetBuilder<match> EndsWithLetter(ProgramSetBuilder<s> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndsWithLetter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F94 RID: 16276 RVA: 0x000C8E98 File Offset: 0x000C7098
				public ProgramSetBuilder<match> ContainsString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ContainsString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06003F95 RID: 16277 RVA: 0x000C8EF2 File Offset: 0x000C70F2
				public ProgramSetBuilder<match> Matches(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Matches, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F96 RID: 16278 RVA: 0x000C8F32 File Offset: 0x000C7132
				public ProgramSetBuilder<match> StartsWith(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F97 RID: 16279 RVA: 0x000C8F72 File Offset: 0x000C7172
				public ProgramSetBuilder<match> EndsWith(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.EndsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F98 RID: 16280 RVA: 0x000C8FB4 File Offset: 0x000C71B4
				public ProgramSetBuilder<match> Contains(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Contains, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06003F99 RID: 16281 RVA: 0x000C900E File Offset: 0x000C720E
				public ProgramSetBuilder<output> Start(ProgramSetBuilder<disjunct> value0)
				{
					return ProgramSetBuilder<output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Start, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F9A RID: 16282 RVA: 0x000C903F File Offset: 0x000C723F
				public ProgramSetBuilder<disjunct> ConvertDisjunctConjunct(ProgramSetBuilder<conjunct> value0)
				{
					return ProgramSetBuilder<disjunct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConvertDisjunctConjunct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003F9B RID: 16283 RVA: 0x000C9070 File Offset: 0x000C7270
				public ProgramSetBuilder<conjunct> Conjunct(ProgramSetBuilder<baseConjunct> value0)
				{
					return ProgramSetBuilder<conjunct>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Conjunct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04001D4F RID: 7503
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A2B RID: 2603
			public class ExplicitJoins
			{
				// Token: 0x06003F9C RID: 16284 RVA: 0x000C90A1 File Offset: 0x000C72A1
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003F9D RID: 16285 RVA: 0x000C90B0 File Offset: 0x000C72B0
				public JoinProgramSetBuilder<disjunct> Disjunction(ProgramSetBuilder<conjunct> value0, ProgramSetBuilder<disjunct> value1)
				{
					return JoinProgramSetBuilder<disjunct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Disjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F9E RID: 16286 RVA: 0x000C90F0 File Offset: 0x000C72F0
				public JoinProgramSetBuilder<baseConjunct> Conjunction(ProgramSetBuilder<pred> value0, ProgramSetBuilder<baseConjunct> value1)
				{
					return JoinProgramSetBuilder<baseConjunct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Conjunction, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003F9F RID: 16287 RVA: 0x000C9130 File Offset: 0x000C7330
				public JoinProgramSetBuilder<pred> Not(ProgramSetBuilder<match> value0)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Not, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA0 RID: 16288 RVA: 0x000C9161 File Offset: 0x000C7361
				public JoinProgramSetBuilder<match> IsNullOrWhiteSpace(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNullOrWhiteSpace, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA1 RID: 16289 RVA: 0x000C9192 File Offset: 0x000C7392
				public JoinProgramSetBuilder<match> IsNull(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNull, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA2 RID: 16290 RVA: 0x000C91C3 File Offset: 0x000C73C3
				public JoinProgramSetBuilder<match> IsWhiteSpace(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsWhiteSpace, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA3 RID: 16291 RVA: 0x000C91F4 File Offset: 0x000C73F4
				public JoinProgramSetBuilder<match> True()
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.True, Array.Empty<ProgramSet>()));
				}

				// Token: 0x06003FA4 RID: 16292 RVA: 0x000C9215 File Offset: 0x000C7415
				public JoinProgramSetBuilder<match> StartsWithString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWithString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003FA5 RID: 16293 RVA: 0x000C9255 File Offset: 0x000C7455
				public JoinProgramSetBuilder<match> StartsWithDigit(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWithDigit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA6 RID: 16294 RVA: 0x000C9286 File Offset: 0x000C7486
				public JoinProgramSetBuilder<match> StartsWithLetter(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWithLetter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA7 RID: 16295 RVA: 0x000C92B7 File Offset: 0x000C74B7
				public JoinProgramSetBuilder<match> EndsWithString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndsWithString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003FA8 RID: 16296 RVA: 0x000C92F7 File Offset: 0x000C74F7
				public JoinProgramSetBuilder<match> EndsWithDigit(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndsWithDigit, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FA9 RID: 16297 RVA: 0x000C9328 File Offset: 0x000C7528
				public JoinProgramSetBuilder<match> EndsWithLetter(ProgramSetBuilder<s> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndsWithLetter, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FAA RID: 16298 RVA: 0x000C935C File Offset: 0x000C755C
				public JoinProgramSetBuilder<match> ContainsString(ProgramSetBuilder<s> value0, ProgramSetBuilder<str> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ContainsString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06003FAB RID: 16299 RVA: 0x000C93B6 File Offset: 0x000C75B6
				public JoinProgramSetBuilder<match> Matches(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Matches, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003FAC RID: 16300 RVA: 0x000C93F6 File Offset: 0x000C75F6
				public JoinProgramSetBuilder<match> StartsWith(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.StartsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003FAD RID: 16301 RVA: 0x000C9436 File Offset: 0x000C7636
				public JoinProgramSetBuilder<match> EndsWith(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.EndsWith, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06003FAE RID: 16302 RVA: 0x000C9478 File Offset: 0x000C7678
				public JoinProgramSetBuilder<match> Contains(ProgramSetBuilder<s> value0, ProgramSetBuilder<r> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Contains, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06003FAF RID: 16303 RVA: 0x000C94D2 File Offset: 0x000C76D2
				public JoinProgramSetBuilder<output> Start(ProgramSetBuilder<disjunct> value0)
				{
					return JoinProgramSetBuilder<output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Start, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FB0 RID: 16304 RVA: 0x000C9503 File Offset: 0x000C7703
				public JoinProgramSetBuilder<disjunct> ConvertDisjunctConjunct(ProgramSetBuilder<conjunct> value0)
				{
					return JoinProgramSetBuilder<disjunct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConvertDisjunctConjunct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FB1 RID: 16305 RVA: 0x000C9534 File Offset: 0x000C7734
				public JoinProgramSetBuilder<conjunct> Conjunct(ProgramSetBuilder<baseConjunct> value0)
				{
					return JoinProgramSetBuilder<conjunct>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Conjunct, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04001D50 RID: 7504
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A2C RID: 2604
			public class JoinUnnamedConversions
			{
				// Token: 0x06003FB2 RID: 16306 RVA: 0x000C9565 File Offset: 0x000C7765
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003FB3 RID: 16307 RVA: 0x000C9574 File Offset: 0x000C7774
				public ProgramSetBuilder<baseConjunct> baseConjunct_pred(ProgramSetBuilder<pred> value0)
				{
					return ProgramSetBuilder<baseConjunct>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.baseConjunct_pred, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FB4 RID: 16308 RVA: 0x000C95A5 File Offset: 0x000C77A5
				public ProgramSetBuilder<pred> pred_match(ProgramSetBuilder<match> value0)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.pred_match, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04001D51 RID: 7505
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A2D RID: 2605
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06003FB5 RID: 16309 RVA: 0x000C95D6 File Offset: 0x000C77D6
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003FB6 RID: 16310 RVA: 0x000C95E5 File Offset: 0x000C77E5
				public JoinProgramSetBuilder<baseConjunct> baseConjunct_pred(ProgramSetBuilder<pred> value0)
				{
					return JoinProgramSetBuilder<baseConjunct>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.baseConjunct_pred, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06003FB7 RID: 16311 RVA: 0x000C9616 File Offset: 0x000C7816
				public JoinProgramSetBuilder<pred> pred_match(ProgramSetBuilder<match> value0)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.pred_match, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04001D52 RID: 7506
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000A2E RID: 2606
			public class Casts
			{
				// Token: 0x06003FB8 RID: 16312 RVA: 0x000C9647 File Offset: 0x000C7847
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06003FB9 RID: 16313 RVA: 0x000C9658 File Offset: 0x000C7858
				public ProgramSetBuilder<r> r(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.r)
					{
						string text = "set";
						string text2 = "expected program set for symbol r but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.r>.CreateUnsafe(set);
				}

				// Token: 0x06003FBA RID: 16314 RVA: 0x000C96B0 File Offset: 0x000C78B0
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06003FBB RID: 16315 RVA: 0x000C9708 File Offset: 0x000C7908
				public ProgramSetBuilder<str> str(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.str)
					{
						string text = "set";
						string text2 = "expected program set for symbol str but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.str>.CreateUnsafe(set);
				}

				// Token: 0x06003FBC RID: 16316 RVA: 0x000C9760 File Offset: 0x000C7960
				public ProgramSetBuilder<output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x06003FBD RID: 16317 RVA: 0x000C97B8 File Offset: 0x000C79B8
				public ProgramSetBuilder<disjunct> disjunct(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.disjunct)
					{
						string text = "set";
						string text2 = "expected program set for symbol disjunct but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.disjunct>.CreateUnsafe(set);
				}

				// Token: 0x06003FBE RID: 16318 RVA: 0x000C9810 File Offset: 0x000C7A10
				public ProgramSetBuilder<conjunct> conjunct(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.conjunct)
					{
						string text = "set";
						string text2 = "expected program set for symbol conjunct but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.conjunct>.CreateUnsafe(set);
				}

				// Token: 0x06003FBF RID: 16319 RVA: 0x000C9868 File Offset: 0x000C7A68
				public ProgramSetBuilder<baseConjunct> baseConjunct(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.baseConjunct)
					{
						string text = "set";
						string text2 = "expected program set for symbol baseConjunct but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.baseConjunct>.CreateUnsafe(set);
				}

				// Token: 0x06003FC0 RID: 16320 RVA: 0x000C98C0 File Offset: 0x000C7AC0
				public ProgramSetBuilder<pred> pred(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pred)
					{
						string text = "set";
						string text2 = "expected program set for symbol pred but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.pred>.CreateUnsafe(set);
				}

				// Token: 0x06003FC1 RID: 16321 RVA: 0x000C9918 File Offset: 0x000C7B18
				public ProgramSetBuilder<match> match(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.match)
					{
						string text = "set";
						string text2 = "expected program set for symbol match but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes.match>.CreateUnsafe(set);
				}

				// Token: 0x04001D53 RID: 7507
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
