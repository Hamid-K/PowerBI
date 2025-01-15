using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build
{
	// Token: 0x02000BD3 RID: 3027
	public class GrammarBuilders
	{
		// Token: 0x06004CB6 RID: 19638 RVA: 0x000F516B File Offset: 0x000F336B
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06004CB7 RID: 19639 RVA: 0x000F5197 File Offset: 0x000F3397
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x000F51A4 File Offset: 0x000F33A4
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x000F51B1 File Offset: 0x000F33B1
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x000F51BE File Offset: 0x000F33BE
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x000F51CB File Offset: 0x000F33CB
		// (set) Token: 0x06004CBC RID: 19644 RVA: 0x000F51D3 File Offset: 0x000F33D3
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x000F51DC File Offset: 0x000F33DC
		// (set) Token: 0x06004CBE RID: 19646 RVA: 0x000F51E4 File Offset: 0x000F33E4
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06004CBF RID: 19647 RVA: 0x000F51F0 File Offset: 0x000F33F0
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

		// Token: 0x04002299 RID: 8857
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x0400229A RID: 8858
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x0400229B RID: 8859
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x0400229C RID: 8860
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x0400229D RID: 8861
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02000BD4 RID: 3028
		public class GrammarSymbols
		{
			// Token: 0x17000DBD RID: 3517
			// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x000F529B File Offset: 0x000F349B
			// (set) Token: 0x06004CC2 RID: 19650 RVA: 0x000F52A3 File Offset: 0x000F34A3
			public Symbol tableIdentifier { get; private set; }

			// Token: 0x17000DBE RID: 3518
			// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x000F52AC File Offset: 0x000F34AC
			// (set) Token: 0x06004CC4 RID: 19652 RVA: 0x000F52B4 File Offset: 0x000F34B4
			public Symbol tableBounds { get; private set; }

			// Token: 0x17000DBF RID: 3519
			// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x000F52BD File Offset: 0x000F34BD
			// (set) Token: 0x06004CC6 RID: 19654 RVA: 0x000F52C5 File Offset: 0x000F34C5
			public Symbol selectedBounds { get; private set; }

			// Token: 0x17000DC0 RID: 3520
			// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x000F52CE File Offset: 0x000F34CE
			// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x000F52D6 File Offset: 0x000F34D6
			public Symbol betweenAxis { get; private set; }

			// Token: 0x17000DC1 RID: 3521
			// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x000F52DF File Offset: 0x000F34DF
			// (set) Token: 0x06004CCA RID: 19658 RVA: 0x000F52E7 File Offset: 0x000F34E7
			public Symbol before { get; private set; }

			// Token: 0x17000DC2 RID: 3522
			// (get) Token: 0x06004CCB RID: 19659 RVA: 0x000F52F0 File Offset: 0x000F34F0
			// (set) Token: 0x06004CCC RID: 19660 RVA: 0x000F52F8 File Offset: 0x000F34F8
			public Symbol expandedBounds { get; private set; }

			// Token: 0x17000DC3 RID: 3523
			// (get) Token: 0x06004CCD RID: 19661 RVA: 0x000F5301 File Offset: 0x000F3501
			// (set) Token: 0x06004CCE RID: 19662 RVA: 0x000F5309 File Offset: 0x000F3509
			public Symbol beforeRelativeBounds { get; private set; }

			// Token: 0x17000DC4 RID: 3524
			// (get) Token: 0x06004CCF RID: 19663 RVA: 0x000F5312 File Offset: 0x000F3512
			// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x000F531A File Offset: 0x000F351A
			public Symbol fixedBounds { get; private set; }

			// Token: 0x17000DC5 RID: 3525
			// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x000F5323 File Offset: 0x000F3523
			// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x000F532B File Offset: 0x000F352B
			public Symbol axis { get; private set; }

			// Token: 0x17000DC6 RID: 3526
			// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x000F5334 File Offset: 0x000F3534
			// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x000F533C File Offset: 0x000F353C
			public Symbol dir { get; private set; }

			// Token: 0x17000DC7 RID: 3527
			// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x000F5345 File Offset: 0x000F3545
			// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x000F534D File Offset: 0x000F354D
			public Symbol k { get; private set; }

			// Token: 0x17000DC8 RID: 3528
			// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x000F5356 File Offset: 0x000F3556
			// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x000F535E File Offset: 0x000F355E
			public Symbol tolerance { get; private set; }

			// Token: 0x17000DC9 RID: 3529
			// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x000F5367 File Offset: 0x000F3567
			// (set) Token: 0x06004CDA RID: 19674 RVA: 0x000F536F File Offset: 0x000F356F
			public Symbol _LetB0 { get; private set; }

			// Token: 0x17000DCA RID: 3530
			// (get) Token: 0x06004CDB RID: 19675 RVA: 0x000F5378 File Offset: 0x000F3578
			// (set) Token: 0x06004CDC RID: 19676 RVA: 0x000F5380 File Offset: 0x000F3580
			public Symbol _LetB1 { get; private set; }

			// Token: 0x06004CDD RID: 19677 RVA: 0x000F538C File Offset: 0x000F358C
			public GrammarSymbols(Grammar grammar)
			{
				this.tableIdentifier = grammar.Symbol("tableIdentifier");
				this.tableBounds = grammar.Symbol("tableBounds");
				this.selectedBounds = grammar.Symbol("selectedBounds");
				this.betweenAxis = grammar.Symbol("betweenAxis");
				this.before = grammar.Symbol("before");
				this.expandedBounds = grammar.Symbol("expandedBounds");
				this.beforeRelativeBounds = grammar.Symbol("beforeRelativeBounds");
				this.fixedBounds = grammar.Symbol("fixedBounds");
				this.axis = grammar.Symbol("axis");
				this.dir = grammar.Symbol("dir");
				this.k = grammar.Symbol("k");
				this.tolerance = grammar.Symbol("tolerance");
				this._LetB0 = grammar.Symbol("_LetB0");
				this._LetB1 = grammar.Symbol("_LetB1");
			}
		}

		// Token: 0x02000BD5 RID: 3029
		public class GrammarRules
		{
			// Token: 0x17000DCB RID: 3531
			// (get) Token: 0x06004CDE RID: 19678 RVA: 0x000F548D File Offset: 0x000F368D
			// (set) Token: 0x06004CDF RID: 19679 RVA: 0x000F5495 File Offset: 0x000F3695
			public BlackBoxRule SnapToGlyphs { get; private set; }

			// Token: 0x17000DCC RID: 3532
			// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x000F549E File Offset: 0x000F369E
			// (set) Token: 0x06004CE1 RID: 19681 RVA: 0x000F54A6 File Offset: 0x000F36A6
			public BlackBoxRule Between { get; private set; }

			// Token: 0x17000DCD RID: 3533
			// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x000F54AF File Offset: 0x000F36AF
			// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x000F54B7 File Offset: 0x000F36B7
			public BlackBoxRule PageBounds { get; private set; }

			// Token: 0x17000DCE RID: 3534
			// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x000F54C0 File Offset: 0x000F36C0
			// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x000F54C8 File Offset: 0x000F36C8
			public BlackBoxRule NextSeparator { get; private set; }

			// Token: 0x17000DCF RID: 3535
			// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x000F54D1 File Offset: 0x000F36D1
			// (set) Token: 0x06004CE7 RID: 19687 RVA: 0x000F54D9 File Offset: 0x000F36D9
			public BlackBoxRule CombineBounds { get; private set; }

			// Token: 0x17000DD0 RID: 3536
			// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x000F54E2 File Offset: 0x000F36E2
			// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x000F54EA File Offset: 0x000F36EA
			public BlackBoxRule NextSeparator_beforeRelative { get; private set; }

			// Token: 0x17000DD1 RID: 3537
			// (get) Token: 0x06004CEA RID: 19690 RVA: 0x000F54F3 File Offset: 0x000F36F3
			// (set) Token: 0x06004CEB RID: 19691 RVA: 0x000F54FB File Offset: 0x000F36FB
			public BlackBoxRule NextSameWidthSeparator { get; private set; }

			// Token: 0x17000DD2 RID: 3538
			// (get) Token: 0x06004CEC RID: 19692 RVA: 0x000F5504 File Offset: 0x000F3704
			// (set) Token: 0x06004CED RID: 19693 RVA: 0x000F550C File Offset: 0x000F370C
			public BlackBoxRule NextFontSizeDecrease { get; private set; }

			// Token: 0x17000DD3 RID: 3539
			// (get) Token: 0x06004CEE RID: 19694 RVA: 0x000F5515 File Offset: 0x000F3715
			// (set) Token: 0x06004CEF RID: 19695 RVA: 0x000F551D File Offset: 0x000F371D
			public LetRule LetBetweenBefore { get; private set; }

			// Token: 0x17000DD4 RID: 3540
			// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x000F5526 File Offset: 0x000F3726
			// (set) Token: 0x06004CF1 RID: 19697 RVA: 0x000F552E File Offset: 0x000F372E
			public LetRule LetBetweenAxis { get; private set; }

			// Token: 0x06004CF2 RID: 19698 RVA: 0x000F5538 File Offset: 0x000F3738
			public GrammarRules(Grammar grammar)
			{
				this.SnapToGlyphs = (BlackBoxRule)grammar.Rule("SnapToGlyphs");
				this.Between = (BlackBoxRule)grammar.Rule("Between");
				this.PageBounds = (BlackBoxRule)grammar.Rule("PageBounds");
				this.NextSeparator = (BlackBoxRule)grammar.Rule("NextSeparator");
				this.CombineBounds = (BlackBoxRule)grammar.Rule("CombineBounds");
				this.NextSeparator_beforeRelative = (BlackBoxRule)grammar.Rule("NextSeparator_beforeRelative");
				this.NextSameWidthSeparator = (BlackBoxRule)grammar.Rule("NextSameWidthSeparator");
				this.NextFontSizeDecrease = (BlackBoxRule)grammar.Rule("NextFontSizeDecrease");
				this.LetBetweenBefore = (LetRule)grammar.Rule("LetBetweenBefore");
				this.LetBetweenAxis = (LetRule)grammar.Rule("LetBetweenAxis");
			}
		}

		// Token: 0x02000BD6 RID: 3030
		public class GrammarUnnamedConversions
		{
			// Token: 0x17000DD5 RID: 3541
			// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x000F5627 File Offset: 0x000F3827
			// (set) Token: 0x06004CF4 RID: 19700 RVA: 0x000F562F File Offset: 0x000F382F
			public ConversionRule tableBounds_expandedBounds { get; private set; }

			// Token: 0x17000DD6 RID: 3542
			// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x000F5638 File Offset: 0x000F3838
			// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x000F5640 File Offset: 0x000F3840
			public ConversionRule selectedBounds_fixedBounds { get; private set; }

			// Token: 0x17000DD7 RID: 3543
			// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x000F5649 File Offset: 0x000F3849
			// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x000F5651 File Offset: 0x000F3851
			public ConversionRule expandedBounds_selectedBounds { get; private set; }

			// Token: 0x17000DD8 RID: 3544
			// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x000F565A File Offset: 0x000F385A
			// (set) Token: 0x06004CFA RID: 19706 RVA: 0x000F5662 File Offset: 0x000F3862
			public ConversionRule beforeRelativeBounds_selectedBounds { get; private set; }

			// Token: 0x17000DD9 RID: 3545
			// (get) Token: 0x06004CFB RID: 19707 RVA: 0x000F566B File Offset: 0x000F386B
			// (set) Token: 0x06004CFC RID: 19708 RVA: 0x000F5673 File Offset: 0x000F3873
			public ConversionRule fixedBounds_tableIdentifier { get; private set; }

			// Token: 0x06004CFD RID: 19709 RVA: 0x000F567C File Offset: 0x000F387C
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.tableBounds_expandedBounds = (ConversionRule)grammar.Rule("~convert_tableBounds_expandedBounds");
				this.selectedBounds_fixedBounds = (ConversionRule)grammar.Rule("~convert_selectedBounds_fixedBounds");
				this.expandedBounds_selectedBounds = (ConversionRule)grammar.Rule("~convert_expandedBounds_selectedBounds");
				this.beforeRelativeBounds_selectedBounds = (ConversionRule)grammar.Rule("~convert_beforeRelativeBounds_selectedBounds");
				this.fixedBounds_tableIdentifier = (ConversionRule)grammar.Rule("~convert_fixedBounds_tableIdentifier");
			}
		}

		// Token: 0x02000BD7 RID: 3031
		public class GrammarHoles
		{
			// Token: 0x17000DDA RID: 3546
			// (get) Token: 0x06004CFE RID: 19710 RVA: 0x000F56FD File Offset: 0x000F38FD
			// (set) Token: 0x06004CFF RID: 19711 RVA: 0x000F5705 File Offset: 0x000F3905
			public Hole tableIdentifier { get; private set; }

			// Token: 0x17000DDB RID: 3547
			// (get) Token: 0x06004D00 RID: 19712 RVA: 0x000F570E File Offset: 0x000F390E
			// (set) Token: 0x06004D01 RID: 19713 RVA: 0x000F5716 File Offset: 0x000F3916
			public Hole tableBounds { get; private set; }

			// Token: 0x17000DDC RID: 3548
			// (get) Token: 0x06004D02 RID: 19714 RVA: 0x000F571F File Offset: 0x000F391F
			// (set) Token: 0x06004D03 RID: 19715 RVA: 0x000F5727 File Offset: 0x000F3927
			public Hole selectedBounds { get; private set; }

			// Token: 0x17000DDD RID: 3549
			// (get) Token: 0x06004D04 RID: 19716 RVA: 0x000F5730 File Offset: 0x000F3930
			// (set) Token: 0x06004D05 RID: 19717 RVA: 0x000F5738 File Offset: 0x000F3938
			public Hole betweenAxis { get; private set; }

			// Token: 0x17000DDE RID: 3550
			// (get) Token: 0x06004D06 RID: 19718 RVA: 0x000F5741 File Offset: 0x000F3941
			// (set) Token: 0x06004D07 RID: 19719 RVA: 0x000F5749 File Offset: 0x000F3949
			public Hole before { get; private set; }

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x06004D08 RID: 19720 RVA: 0x000F5752 File Offset: 0x000F3952
			// (set) Token: 0x06004D09 RID: 19721 RVA: 0x000F575A File Offset: 0x000F395A
			public Hole expandedBounds { get; private set; }

			// Token: 0x17000DE0 RID: 3552
			// (get) Token: 0x06004D0A RID: 19722 RVA: 0x000F5763 File Offset: 0x000F3963
			// (set) Token: 0x06004D0B RID: 19723 RVA: 0x000F576B File Offset: 0x000F396B
			public Hole beforeRelativeBounds { get; private set; }

			// Token: 0x17000DE1 RID: 3553
			// (get) Token: 0x06004D0C RID: 19724 RVA: 0x000F5774 File Offset: 0x000F3974
			// (set) Token: 0x06004D0D RID: 19725 RVA: 0x000F577C File Offset: 0x000F397C
			public Hole fixedBounds { get; private set; }

			// Token: 0x17000DE2 RID: 3554
			// (get) Token: 0x06004D0E RID: 19726 RVA: 0x000F5785 File Offset: 0x000F3985
			// (set) Token: 0x06004D0F RID: 19727 RVA: 0x000F578D File Offset: 0x000F398D
			public Hole axis { get; private set; }

			// Token: 0x17000DE3 RID: 3555
			// (get) Token: 0x06004D10 RID: 19728 RVA: 0x000F5796 File Offset: 0x000F3996
			// (set) Token: 0x06004D11 RID: 19729 RVA: 0x000F579E File Offset: 0x000F399E
			public Hole dir { get; private set; }

			// Token: 0x17000DE4 RID: 3556
			// (get) Token: 0x06004D12 RID: 19730 RVA: 0x000F57A7 File Offset: 0x000F39A7
			// (set) Token: 0x06004D13 RID: 19731 RVA: 0x000F57AF File Offset: 0x000F39AF
			public Hole k { get; private set; }

			// Token: 0x17000DE5 RID: 3557
			// (get) Token: 0x06004D14 RID: 19732 RVA: 0x000F57B8 File Offset: 0x000F39B8
			// (set) Token: 0x06004D15 RID: 19733 RVA: 0x000F57C0 File Offset: 0x000F39C0
			public Hole tolerance { get; private set; }

			// Token: 0x17000DE6 RID: 3558
			// (get) Token: 0x06004D16 RID: 19734 RVA: 0x000F57C9 File Offset: 0x000F39C9
			// (set) Token: 0x06004D17 RID: 19735 RVA: 0x000F57D1 File Offset: 0x000F39D1
			public Hole _LetB0 { get; private set; }

			// Token: 0x17000DE7 RID: 3559
			// (get) Token: 0x06004D18 RID: 19736 RVA: 0x000F57DA File Offset: 0x000F39DA
			// (set) Token: 0x06004D19 RID: 19737 RVA: 0x000F57E2 File Offset: 0x000F39E2
			public Hole _LetB1 { get; private set; }

			// Token: 0x06004D1A RID: 19738 RVA: 0x000F57EC File Offset: 0x000F39EC
			public GrammarHoles(GrammarBuilders builders)
			{
				this.tableIdentifier = new Hole(builders.Symbol.tableIdentifier, null);
				this.tableBounds = new Hole(builders.Symbol.tableBounds, null);
				this.selectedBounds = new Hole(builders.Symbol.selectedBounds, null);
				this.betweenAxis = new Hole(builders.Symbol.betweenAxis, null);
				this.before = new Hole(builders.Symbol.before, null);
				this.expandedBounds = new Hole(builders.Symbol.expandedBounds, null);
				this.beforeRelativeBounds = new Hole(builders.Symbol.beforeRelativeBounds, null);
				this.fixedBounds = new Hole(builders.Symbol.fixedBounds, null);
				this.axis = new Hole(builders.Symbol.axis, null);
				this.dir = new Hole(builders.Symbol.dir, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.tolerance = new Hole(builders.Symbol.tolerance, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
				this._LetB1 = new Hole(builders.Symbol._LetB1, null);
			}
		}

		// Token: 0x02000BD8 RID: 3032
		public class Nodes
		{
			// Token: 0x06004D1B RID: 19739 RVA: 0x000F5944 File Offset: 0x000F3B44
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

			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x06004D1C RID: 19740 RVA: 0x000F5A27 File Offset: 0x000F3C27
			// (set) Token: 0x06004D1D RID: 19741 RVA: 0x000F5A2F File Offset: 0x000F3C2F
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x06004D1E RID: 19742 RVA: 0x000F5A38 File Offset: 0x000F3C38
			// (set) Token: 0x06004D1F RID: 19743 RVA: 0x000F5A40 File Offset: 0x000F3C40
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17000DEA RID: 3562
			// (get) Token: 0x06004D20 RID: 19744 RVA: 0x000F5A49 File Offset: 0x000F3C49
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17000DEB RID: 3563
			// (get) Token: 0x06004D21 RID: 19745 RVA: 0x000F5A56 File Offset: 0x000F3C56
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17000DEC RID: 3564
			// (get) Token: 0x06004D22 RID: 19746 RVA: 0x000F5A63 File Offset: 0x000F3C63
			// (set) Token: 0x06004D23 RID: 19747 RVA: 0x000F5A6B File Offset: 0x000F3C6B
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17000DED RID: 3565
			// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000F5A74 File Offset: 0x000F3C74
			// (set) Token: 0x06004D25 RID: 19749 RVA: 0x000F5A7C File Offset: 0x000F3C7C
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17000DEE RID: 3566
			// (get) Token: 0x06004D26 RID: 19750 RVA: 0x000F5A85 File Offset: 0x000F3C85
			// (set) Token: 0x06004D27 RID: 19751 RVA: 0x000F5A8D File Offset: 0x000F3C8D
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17000DEF RID: 3567
			// (get) Token: 0x06004D28 RID: 19752 RVA: 0x000F5A96 File Offset: 0x000F3C96
			// (set) Token: 0x06004D29 RID: 19753 RVA: 0x000F5A9E File Offset: 0x000F3C9E
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17000DF0 RID: 3568
			// (get) Token: 0x06004D2A RID: 19754 RVA: 0x000F5AA7 File Offset: 0x000F3CA7
			// (set) Token: 0x06004D2B RID: 19755 RVA: 0x000F5AAF File Offset: 0x000F3CAF
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17000DF1 RID: 3569
			// (get) Token: 0x06004D2C RID: 19756 RVA: 0x000F5AB8 File Offset: 0x000F3CB8
			// (set) Token: 0x06004D2D RID: 19757 RVA: 0x000F5AC0 File Offset: 0x000F3CC0
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17000DF2 RID: 3570
			// (get) Token: 0x06004D2E RID: 19758 RVA: 0x000F5AC9 File Offset: 0x000F3CC9
			// (set) Token: 0x06004D2F RID: 19759 RVA: 0x000F5AD1 File Offset: 0x000F3CD1
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x040022CD RID: 8909
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x040022CE RID: 8910
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02000BD9 RID: 3033
			public class NodeRules
			{
				// Token: 0x06004D30 RID: 19760 RVA: 0x000F5ADA File Offset: 0x000F3CDA
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004D31 RID: 19761 RVA: 0x000F5AE9 File Offset: 0x000F3CE9
				public axis axis(Axis value)
				{
					return new axis(this._builders, value);
				}

				// Token: 0x06004D32 RID: 19762 RVA: 0x000F5AF7 File Offset: 0x000F3CF7
				public dir dir(Direction value)
				{
					return new dir(this._builders, value);
				}

				// Token: 0x06004D33 RID: 19763 RVA: 0x000F5B05 File Offset: 0x000F3D05
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x06004D34 RID: 19764 RVA: 0x000F5B13 File Offset: 0x000F3D13
				public tolerance tolerance(int value)
				{
					return new tolerance(this._builders, value);
				}

				// Token: 0x06004D35 RID: 19765 RVA: 0x000F5B21 File Offset: 0x000F3D21
				public tableBounds SnapToGlyphs(expandedBounds value0)
				{
					return new SnapToGlyphs(this._builders, value0);
				}

				// Token: 0x06004D36 RID: 19766 RVA: 0x000F5B34 File Offset: 0x000F3D34
				public _LetB0 Between(betweenAxis value0, before value1, beforeRelativeBounds value2)
				{
					return new Between(this._builders, value0, value1, value2);
				}

				// Token: 0x06004D37 RID: 19767 RVA: 0x000F5B49 File Offset: 0x000F3D49
				public selectedBounds PageBounds(fixedBounds value0)
				{
					return new PageBounds(this._builders, value0);
				}

				// Token: 0x06004D38 RID: 19768 RVA: 0x000F5B5C File Offset: 0x000F3D5C
				public selectedBounds NextSeparator(selectedBounds value0, dir value1, k value2)
				{
					return new NextSeparator(this._builders, value0, value1, value2);
				}

				// Token: 0x06004D39 RID: 19769 RVA: 0x000F5B71 File Offset: 0x000F3D71
				public expandedBounds CombineBounds(selectedBounds value0, selectedBounds value1)
				{
					return new CombineBounds(this._builders, value0, value1);
				}

				// Token: 0x06004D3A RID: 19770 RVA: 0x000F5B85 File Offset: 0x000F3D85
				public beforeRelativeBounds NextSeparator_beforeRelative(before value0, dir value1, k value2)
				{
					return new NextSeparator_beforeRelative(this._builders, value0, value1, value2);
				}

				// Token: 0x06004D3B RID: 19771 RVA: 0x000F5B9A File Offset: 0x000F3D9A
				public beforeRelativeBounds NextSameWidthSeparator(before value0, dir value1, k value2, tolerance value3)
				{
					return new NextSameWidthSeparator(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06004D3C RID: 19772 RVA: 0x000F5BB1 File Offset: 0x000F3DB1
				public beforeRelativeBounds NextFontSizeDecrease(before value0, dir value1)
				{
					return new NextFontSizeDecrease(this._builders, value0, value1);
				}

				// Token: 0x06004D3D RID: 19773 RVA: 0x000F5BC5 File Offset: 0x000F3DC5
				public _LetB1 LetBetweenBefore(selectedBounds value0, _LetB0 value1)
				{
					return new LetBetweenBefore(this._builders, value0, value1);
				}

				// Token: 0x06004D3E RID: 19774 RVA: 0x000F5BD9 File Offset: 0x000F3DD9
				public selectedBounds LetBetweenAxis(axis value0, _LetB1 value1)
				{
					return new LetBetweenAxis(this._builders, value0, value1);
				}

				// Token: 0x040022D6 RID: 8918
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BDA RID: 3034
			public class NodeUnnamedConversionRules
			{
				// Token: 0x06004D3F RID: 19775 RVA: 0x000F5BED File Offset: 0x000F3DED
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004D40 RID: 19776 RVA: 0x000F5BFC File Offset: 0x000F3DFC
				public tableBounds tableBounds_expandedBounds(expandedBounds value0)
				{
					return new tableBounds_expandedBounds(this._builders, value0);
				}

				// Token: 0x06004D41 RID: 19777 RVA: 0x000F5C0F File Offset: 0x000F3E0F
				public selectedBounds selectedBounds_fixedBounds(fixedBounds value0)
				{
					return new selectedBounds_fixedBounds(this._builders, value0);
				}

				// Token: 0x06004D42 RID: 19778 RVA: 0x000F5C22 File Offset: 0x000F3E22
				public expandedBounds expandedBounds_selectedBounds(selectedBounds value0)
				{
					return new expandedBounds_selectedBounds(this._builders, value0);
				}

				// Token: 0x06004D43 RID: 19779 RVA: 0x000F5C35 File Offset: 0x000F3E35
				public beforeRelativeBounds beforeRelativeBounds_selectedBounds(selectedBounds value0)
				{
					return new beforeRelativeBounds_selectedBounds(this._builders, value0);
				}

				// Token: 0x06004D44 RID: 19780 RVA: 0x000F5C48 File Offset: 0x000F3E48
				public fixedBounds fixedBounds_tableIdentifier(tableIdentifier value0)
				{
					return new fixedBounds_tableIdentifier(this._builders, value0);
				}

				// Token: 0x040022D7 RID: 8919
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BDB RID: 3035
			public class NodeVariables
			{
				// Token: 0x17000DF3 RID: 3571
				// (get) Token: 0x06004D45 RID: 19781 RVA: 0x000F5C5B File Offset: 0x000F3E5B
				// (set) Token: 0x06004D46 RID: 19782 RVA: 0x000F5C63 File Offset: 0x000F3E63
				public tableIdentifier tableIdentifier { get; private set; }

				// Token: 0x17000DF4 RID: 3572
				// (get) Token: 0x06004D47 RID: 19783 RVA: 0x000F5C6C File Offset: 0x000F3E6C
				// (set) Token: 0x06004D48 RID: 19784 RVA: 0x000F5C74 File Offset: 0x000F3E74
				public betweenAxis betweenAxis { get; private set; }

				// Token: 0x17000DF5 RID: 3573
				// (get) Token: 0x06004D49 RID: 19785 RVA: 0x000F5C7D File Offset: 0x000F3E7D
				// (set) Token: 0x06004D4A RID: 19786 RVA: 0x000F5C85 File Offset: 0x000F3E85
				public before before { get; private set; }

				// Token: 0x06004D4B RID: 19787 RVA: 0x000F5C8E File Offset: 0x000F3E8E
				public NodeVariables(GrammarBuilders builders)
				{
					this.tableIdentifier = new tableIdentifier(builders);
					this.betweenAxis = new betweenAxis(builders);
					this.before = new before(builders);
				}
			}

			// Token: 0x02000BDC RID: 3036
			public class NodeHoles
			{
				// Token: 0x17000DF6 RID: 3574
				// (get) Token: 0x06004D4C RID: 19788 RVA: 0x000F5CBA File Offset: 0x000F3EBA
				// (set) Token: 0x06004D4D RID: 19789 RVA: 0x000F5CC2 File Offset: 0x000F3EC2
				public tableBounds tableBounds { get; private set; }

				// Token: 0x17000DF7 RID: 3575
				// (get) Token: 0x06004D4E RID: 19790 RVA: 0x000F5CCB File Offset: 0x000F3ECB
				// (set) Token: 0x06004D4F RID: 19791 RVA: 0x000F5CD3 File Offset: 0x000F3ED3
				public selectedBounds selectedBounds { get; private set; }

				// Token: 0x17000DF8 RID: 3576
				// (get) Token: 0x06004D50 RID: 19792 RVA: 0x000F5CDC File Offset: 0x000F3EDC
				// (set) Token: 0x06004D51 RID: 19793 RVA: 0x000F5CE4 File Offset: 0x000F3EE4
				public betweenAxis betweenAxis { get; private set; }

				// Token: 0x17000DF9 RID: 3577
				// (get) Token: 0x06004D52 RID: 19794 RVA: 0x000F5CED File Offset: 0x000F3EED
				// (set) Token: 0x06004D53 RID: 19795 RVA: 0x000F5CF5 File Offset: 0x000F3EF5
				public before before { get; private set; }

				// Token: 0x17000DFA RID: 3578
				// (get) Token: 0x06004D54 RID: 19796 RVA: 0x000F5CFE File Offset: 0x000F3EFE
				// (set) Token: 0x06004D55 RID: 19797 RVA: 0x000F5D06 File Offset: 0x000F3F06
				public expandedBounds expandedBounds { get; private set; }

				// Token: 0x17000DFB RID: 3579
				// (get) Token: 0x06004D56 RID: 19798 RVA: 0x000F5D0F File Offset: 0x000F3F0F
				// (set) Token: 0x06004D57 RID: 19799 RVA: 0x000F5D17 File Offset: 0x000F3F17
				public beforeRelativeBounds beforeRelativeBounds { get; private set; }

				// Token: 0x17000DFC RID: 3580
				// (get) Token: 0x06004D58 RID: 19800 RVA: 0x000F5D20 File Offset: 0x000F3F20
				// (set) Token: 0x06004D59 RID: 19801 RVA: 0x000F5D28 File Offset: 0x000F3F28
				public fixedBounds fixedBounds { get; private set; }

				// Token: 0x17000DFD RID: 3581
				// (get) Token: 0x06004D5A RID: 19802 RVA: 0x000F5D31 File Offset: 0x000F3F31
				// (set) Token: 0x06004D5B RID: 19803 RVA: 0x000F5D39 File Offset: 0x000F3F39
				public axis axis { get; private set; }

				// Token: 0x17000DFE RID: 3582
				// (get) Token: 0x06004D5C RID: 19804 RVA: 0x000F5D42 File Offset: 0x000F3F42
				// (set) Token: 0x06004D5D RID: 19805 RVA: 0x000F5D4A File Offset: 0x000F3F4A
				public dir dir { get; private set; }

				// Token: 0x17000DFF RID: 3583
				// (get) Token: 0x06004D5E RID: 19806 RVA: 0x000F5D53 File Offset: 0x000F3F53
				// (set) Token: 0x06004D5F RID: 19807 RVA: 0x000F5D5B File Offset: 0x000F3F5B
				public k k { get; private set; }

				// Token: 0x17000E00 RID: 3584
				// (get) Token: 0x06004D60 RID: 19808 RVA: 0x000F5D64 File Offset: 0x000F3F64
				// (set) Token: 0x06004D61 RID: 19809 RVA: 0x000F5D6C File Offset: 0x000F3F6C
				public tolerance tolerance { get; private set; }

				// Token: 0x17000E01 RID: 3585
				// (get) Token: 0x06004D62 RID: 19810 RVA: 0x000F5D75 File Offset: 0x000F3F75
				// (set) Token: 0x06004D63 RID: 19811 RVA: 0x000F5D7D File Offset: 0x000F3F7D
				public _LetB0 _LetB0 { get; private set; }

				// Token: 0x17000E02 RID: 3586
				// (get) Token: 0x06004D64 RID: 19812 RVA: 0x000F5D86 File Offset: 0x000F3F86
				// (set) Token: 0x06004D65 RID: 19813 RVA: 0x000F5D8E File Offset: 0x000F3F8E
				public _LetB1 _LetB1 { get; private set; }

				// Token: 0x06004D66 RID: 19814 RVA: 0x000F5D98 File Offset: 0x000F3F98
				public NodeHoles(GrammarBuilders builders)
				{
					this.tableBounds = tableBounds.CreateHole(builders, null);
					this.selectedBounds = selectedBounds.CreateHole(builders, null);
					this.betweenAxis = betweenAxis.CreateHole(builders, null);
					this.before = before.CreateHole(builders, null);
					this.expandedBounds = expandedBounds.CreateHole(builders, null);
					this.beforeRelativeBounds = beforeRelativeBounds.CreateHole(builders, null);
					this.fixedBounds = fixedBounds.CreateHole(builders, null);
					this.axis = axis.CreateHole(builders, null);
					this.dir = dir.CreateHole(builders, null);
					this.k = k.CreateHole(builders, null);
					this.tolerance = tolerance.CreateHole(builders, null);
					this._LetB0 = _LetB0.CreateHole(builders, null);
					this._LetB1 = _LetB1.CreateHole(builders, null);
				}
			}

			// Token: 0x02000BDD RID: 3037
			public class NodeUnsafe
			{
				// Token: 0x06004D67 RID: 19815 RVA: 0x000F5E54 File Offset: 0x000F4054
				public tableBounds tableBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds.CreateUnsafe(node);
				}

				// Token: 0x06004D68 RID: 19816 RVA: 0x000F5E5C File Offset: 0x000F405C
				public selectedBounds selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds.CreateUnsafe(node);
				}

				// Token: 0x06004D69 RID: 19817 RVA: 0x000F5E64 File Offset: 0x000F4064
				public betweenAxis betweenAxis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis.CreateUnsafe(node);
				}

				// Token: 0x06004D6A RID: 19818 RVA: 0x000F5E6C File Offset: 0x000F406C
				public before before(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before.CreateUnsafe(node);
				}

				// Token: 0x06004D6B RID: 19819 RVA: 0x000F5E74 File Offset: 0x000F4074
				public expandedBounds expandedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds.CreateUnsafe(node);
				}

				// Token: 0x06004D6C RID: 19820 RVA: 0x000F5E7C File Offset: 0x000F407C
				public beforeRelativeBounds beforeRelativeBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds.CreateUnsafe(node);
				}

				// Token: 0x06004D6D RID: 19821 RVA: 0x000F5E84 File Offset: 0x000F4084
				public fixedBounds fixedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds.CreateUnsafe(node);
				}

				// Token: 0x06004D6E RID: 19822 RVA: 0x000F5E8C File Offset: 0x000F408C
				public axis axis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis.CreateUnsafe(node);
				}

				// Token: 0x06004D6F RID: 19823 RVA: 0x000F5E94 File Offset: 0x000F4094
				public dir dir(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir.CreateUnsafe(node);
				}

				// Token: 0x06004D70 RID: 19824 RVA: 0x000F5E9C File Offset: 0x000F409C
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x06004D71 RID: 19825 RVA: 0x000F5EA4 File Offset: 0x000F40A4
				public tolerance tolerance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance.CreateUnsafe(node);
				}

				// Token: 0x06004D72 RID: 19826 RVA: 0x000F5EAC File Offset: 0x000F40AC
				public _LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}

				// Token: 0x06004D73 RID: 19827 RVA: 0x000F5EB4 File Offset: 0x000F40B4
				public _LetB1 _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1.CreateUnsafe(node);
				}
			}

			// Token: 0x02000BDE RID: 3038
			public class NodeCast
			{
				// Token: 0x06004D75 RID: 19829 RVA: 0x000F5EBC File Offset: 0x000F40BC
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004D76 RID: 19830 RVA: 0x000F5ECC File Offset: 0x000F40CC
				public tableBounds tableBounds(ProgramNode node)
				{
					tableBounds? tableBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds.CreateSafe(this._builders, node);
					if (tableBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol tableBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tableBounds.Value;
				}

				// Token: 0x06004D77 RID: 19831 RVA: 0x000F5F20 File Offset: 0x000F4120
				public selectedBounds selectedBounds(ProgramNode node)
				{
					selectedBounds? selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds.CreateSafe(this._builders, node);
					if (selectedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectedBounds.Value;
				}

				// Token: 0x06004D78 RID: 19832 RVA: 0x000F5F74 File Offset: 0x000F4174
				public betweenAxis betweenAxis(ProgramNode node)
				{
					betweenAxis? betweenAxis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis.CreateSafe(this._builders, node);
					if (betweenAxis == null)
					{
						string text = "node";
						string text2 = "expected node for symbol betweenAxis but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return betweenAxis.Value;
				}

				// Token: 0x06004D79 RID: 19833 RVA: 0x000F5FC8 File Offset: 0x000F41C8
				public before before(ProgramNode node)
				{
					before? before = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before.CreateSafe(this._builders, node);
					if (before == null)
					{
						string text = "node";
						string text2 = "expected node for symbol before but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return before.Value;
				}

				// Token: 0x06004D7A RID: 19834 RVA: 0x000F601C File Offset: 0x000F421C
				public expandedBounds expandedBounds(ProgramNode node)
				{
					expandedBounds? expandedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds.CreateSafe(this._builders, node);
					if (expandedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol expandedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return expandedBounds.Value;
				}

				// Token: 0x06004D7B RID: 19835 RVA: 0x000F6070 File Offset: 0x000F4270
				public beforeRelativeBounds beforeRelativeBounds(ProgramNode node)
				{
					beforeRelativeBounds? beforeRelativeBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds.CreateSafe(this._builders, node);
					if (beforeRelativeBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol beforeRelativeBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return beforeRelativeBounds.Value;
				}

				// Token: 0x06004D7C RID: 19836 RVA: 0x000F60C4 File Offset: 0x000F42C4
				public fixedBounds fixedBounds(ProgramNode node)
				{
					fixedBounds? fixedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds.CreateSafe(this._builders, node);
					if (fixedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fixedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fixedBounds.Value;
				}

				// Token: 0x06004D7D RID: 19837 RVA: 0x000F6118 File Offset: 0x000F4318
				public axis axis(ProgramNode node)
				{
					axis? axis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis.CreateSafe(this._builders, node);
					if (axis == null)
					{
						string text = "node";
						string text2 = "expected node for symbol axis but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return axis.Value;
				}

				// Token: 0x06004D7E RID: 19838 RVA: 0x000F616C File Offset: 0x000F436C
				public dir dir(ProgramNode node)
				{
					dir? dir = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir.CreateSafe(this._builders, node);
					if (dir == null)
					{
						string text = "node";
						string text2 = "expected node for symbol dir but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return dir.Value;
				}

				// Token: 0x06004D7F RID: 19839 RVA: 0x000F61C0 File Offset: 0x000F43C0
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x06004D80 RID: 19840 RVA: 0x000F6214 File Offset: 0x000F4414
				public tolerance tolerance(ProgramNode node)
				{
					tolerance? tolerance = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance.CreateSafe(this._builders, node);
					if (tolerance == null)
					{
						string text = "node";
						string text2 = "expected node for symbol tolerance but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tolerance.Value;
				}

				// Token: 0x06004D81 RID: 19841 RVA: 0x000F6268 File Offset: 0x000F4468
				public _LetB0 _LetB0(ProgramNode node)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x06004D82 RID: 19842 RVA: 0x000F62BC File Offset: 0x000F44BC
				public _LetB1 _LetB1(ProgramNode node)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB1 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x040022E8 RID: 8936
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BDF RID: 3039
			public class RuleCast
			{
				// Token: 0x06004D83 RID: 19843 RVA: 0x000F630D File Offset: 0x000F450D
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004D84 RID: 19844 RVA: 0x000F631C File Offset: 0x000F451C
				public SnapToGlyphs SnapToGlyphs(ProgramNode node)
				{
					SnapToGlyphs? snapToGlyphs = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.SnapToGlyphs.CreateSafe(this._builders, node);
					if (snapToGlyphs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SnapToGlyphs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return snapToGlyphs.Value;
				}

				// Token: 0x06004D85 RID: 19845 RVA: 0x000F6370 File Offset: 0x000F4570
				public tableBounds_expandedBounds tableBounds_expandedBounds(ProgramNode node)
				{
					tableBounds_expandedBounds? tableBounds_expandedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.tableBounds_expandedBounds.CreateSafe(this._builders, node);
					if (tableBounds_expandedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol tableBounds_expandedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tableBounds_expandedBounds.Value;
				}

				// Token: 0x06004D86 RID: 19846 RVA: 0x000F63C4 File Offset: 0x000F45C4
				public Between Between(ProgramNode node)
				{
					Between? between = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.Between.CreateSafe(this._builders, node);
					if (between == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Between but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return between.Value;
				}

				// Token: 0x06004D87 RID: 19847 RVA: 0x000F6418 File Offset: 0x000F4618
				public LetBetweenBefore LetBetweenBefore(ProgramNode node)
				{
					LetBetweenBefore? letBetweenBefore = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenBefore.CreateSafe(this._builders, node);
					if (letBetweenBefore == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetBetweenBefore but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letBetweenBefore.Value;
				}

				// Token: 0x06004D88 RID: 19848 RVA: 0x000F646C File Offset: 0x000F466C
				public selectedBounds_fixedBounds selectedBounds_fixedBounds(ProgramNode node)
				{
					selectedBounds_fixedBounds? selectedBounds_fixedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.selectedBounds_fixedBounds.CreateSafe(this._builders, node);
					if (selectedBounds_fixedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectedBounds_fixedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectedBounds_fixedBounds.Value;
				}

				// Token: 0x06004D89 RID: 19849 RVA: 0x000F64C0 File Offset: 0x000F46C0
				public PageBounds PageBounds(ProgramNode node)
				{
					PageBounds? pageBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.PageBounds.CreateSafe(this._builders, node);
					if (pageBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol PageBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pageBounds.Value;
				}

				// Token: 0x06004D8A RID: 19850 RVA: 0x000F6514 File Offset: 0x000F4714
				public NextSeparator NextSeparator(ProgramNode node)
				{
					NextSeparator? nextSeparator = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator.CreateSafe(this._builders, node);
					if (nextSeparator == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NextSeparator but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nextSeparator.Value;
				}

				// Token: 0x06004D8B RID: 19851 RVA: 0x000F6568 File Offset: 0x000F4768
				public LetBetweenAxis LetBetweenAxis(ProgramNode node)
				{
					LetBetweenAxis? letBetweenAxis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenAxis.CreateSafe(this._builders, node);
					if (letBetweenAxis == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LetBetweenAxis but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letBetweenAxis.Value;
				}

				// Token: 0x06004D8C RID: 19852 RVA: 0x000F65BC File Offset: 0x000F47BC
				public expandedBounds_selectedBounds expandedBounds_selectedBounds(ProgramNode node)
				{
					expandedBounds_selectedBounds? expandedBounds_selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.expandedBounds_selectedBounds.CreateSafe(this._builders, node);
					if (expandedBounds_selectedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol expandedBounds_selectedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return expandedBounds_selectedBounds.Value;
				}

				// Token: 0x06004D8D RID: 19853 RVA: 0x000F6610 File Offset: 0x000F4810
				public CombineBounds CombineBounds(ProgramNode node)
				{
					CombineBounds? combineBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.CombineBounds.CreateSafe(this._builders, node);
					if (combineBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol CombineBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return combineBounds.Value;
				}

				// Token: 0x06004D8E RID: 19854 RVA: 0x000F6664 File Offset: 0x000F4864
				public beforeRelativeBounds_selectedBounds beforeRelativeBounds_selectedBounds(ProgramNode node)
				{
					beforeRelativeBounds_selectedBounds? beforeRelativeBounds_selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.beforeRelativeBounds_selectedBounds.CreateSafe(this._builders, node);
					if (beforeRelativeBounds_selectedBounds == null)
					{
						string text = "node";
						string text2 = "expected node for symbol beforeRelativeBounds_selectedBounds but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return beforeRelativeBounds_selectedBounds.Value;
				}

				// Token: 0x06004D8F RID: 19855 RVA: 0x000F66B8 File Offset: 0x000F48B8
				public NextSeparator_beforeRelative NextSeparator_beforeRelative(ProgramNode node)
				{
					NextSeparator_beforeRelative? nextSeparator_beforeRelative = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator_beforeRelative.CreateSafe(this._builders, node);
					if (nextSeparator_beforeRelative == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NextSeparator_beforeRelative but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nextSeparator_beforeRelative.Value;
				}

				// Token: 0x06004D90 RID: 19856 RVA: 0x000F670C File Offset: 0x000F490C
				public NextSameWidthSeparator NextSameWidthSeparator(ProgramNode node)
				{
					NextSameWidthSeparator? nextSameWidthSeparator = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSameWidthSeparator.CreateSafe(this._builders, node);
					if (nextSameWidthSeparator == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NextSameWidthSeparator but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nextSameWidthSeparator.Value;
				}

				// Token: 0x06004D91 RID: 19857 RVA: 0x000F6760 File Offset: 0x000F4960
				public NextFontSizeDecrease NextFontSizeDecrease(ProgramNode node)
				{
					NextFontSizeDecrease? nextFontSizeDecrease = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextFontSizeDecrease.CreateSafe(this._builders, node);
					if (nextFontSizeDecrease == null)
					{
						string text = "node";
						string text2 = "expected node for symbol NextFontSizeDecrease but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return nextFontSizeDecrease.Value;
				}

				// Token: 0x06004D92 RID: 19858 RVA: 0x000F67B4 File Offset: 0x000F49B4
				public fixedBounds_tableIdentifier fixedBounds_tableIdentifier(ProgramNode node)
				{
					fixedBounds_tableIdentifier? fixedBounds_tableIdentifier = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.fixedBounds_tableIdentifier.CreateSafe(this._builders, node);
					if (fixedBounds_tableIdentifier == null)
					{
						string text = "node";
						string text2 = "expected node for symbol fixedBounds_tableIdentifier but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return fixedBounds_tableIdentifier.Value;
				}

				// Token: 0x040022E9 RID: 8937
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE0 RID: 3040
			public class NodeIs
			{
				// Token: 0x06004D93 RID: 19859 RVA: 0x000F6805 File Offset: 0x000F4A05
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004D94 RID: 19860 RVA: 0x000F6814 File Offset: 0x000F4A14
				public bool tableBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D95 RID: 19861 RVA: 0x000F6838 File Offset: 0x000F4A38
				public bool tableBounds(ProgramNode node, out tableBounds value)
				{
					tableBounds? tableBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds.CreateSafe(this._builders, node);
					if (tableBounds == null)
					{
						value = default(tableBounds);
						return false;
					}
					value = tableBounds.Value;
					return true;
				}

				// Token: 0x06004D96 RID: 19862 RVA: 0x000F6874 File Offset: 0x000F4A74
				public bool selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D97 RID: 19863 RVA: 0x000F6898 File Offset: 0x000F4A98
				public bool selectedBounds(ProgramNode node, out selectedBounds value)
				{
					selectedBounds? selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds.CreateSafe(this._builders, node);
					if (selectedBounds == null)
					{
						value = default(selectedBounds);
						return false;
					}
					value = selectedBounds.Value;
					return true;
				}

				// Token: 0x06004D98 RID: 19864 RVA: 0x000F68D4 File Offset: 0x000F4AD4
				public bool betweenAxis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D99 RID: 19865 RVA: 0x000F68F8 File Offset: 0x000F4AF8
				public bool betweenAxis(ProgramNode node, out betweenAxis value)
				{
					betweenAxis? betweenAxis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis.CreateSafe(this._builders, node);
					if (betweenAxis == null)
					{
						value = default(betweenAxis);
						return false;
					}
					value = betweenAxis.Value;
					return true;
				}

				// Token: 0x06004D9A RID: 19866 RVA: 0x000F6934 File Offset: 0x000F4B34
				public bool before(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D9B RID: 19867 RVA: 0x000F6958 File Offset: 0x000F4B58
				public bool before(ProgramNode node, out before value)
				{
					before? before = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before.CreateSafe(this._builders, node);
					if (before == null)
					{
						value = default(before);
						return false;
					}
					value = before.Value;
					return true;
				}

				// Token: 0x06004D9C RID: 19868 RVA: 0x000F6994 File Offset: 0x000F4B94
				public bool expandedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D9D RID: 19869 RVA: 0x000F69B8 File Offset: 0x000F4BB8
				public bool expandedBounds(ProgramNode node, out expandedBounds value)
				{
					expandedBounds? expandedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds.CreateSafe(this._builders, node);
					if (expandedBounds == null)
					{
						value = default(expandedBounds);
						return false;
					}
					value = expandedBounds.Value;
					return true;
				}

				// Token: 0x06004D9E RID: 19870 RVA: 0x000F69F4 File Offset: 0x000F4BF4
				public bool beforeRelativeBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004D9F RID: 19871 RVA: 0x000F6A18 File Offset: 0x000F4C18
				public bool beforeRelativeBounds(ProgramNode node, out beforeRelativeBounds value)
				{
					beforeRelativeBounds? beforeRelativeBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds.CreateSafe(this._builders, node);
					if (beforeRelativeBounds == null)
					{
						value = default(beforeRelativeBounds);
						return false;
					}
					value = beforeRelativeBounds.Value;
					return true;
				}

				// Token: 0x06004DA0 RID: 19872 RVA: 0x000F6A54 File Offset: 0x000F4C54
				public bool fixedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DA1 RID: 19873 RVA: 0x000F6A78 File Offset: 0x000F4C78
				public bool fixedBounds(ProgramNode node, out fixedBounds value)
				{
					fixedBounds? fixedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds.CreateSafe(this._builders, node);
					if (fixedBounds == null)
					{
						value = default(fixedBounds);
						return false;
					}
					value = fixedBounds.Value;
					return true;
				}

				// Token: 0x06004DA2 RID: 19874 RVA: 0x000F6AB4 File Offset: 0x000F4CB4
				public bool axis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DA3 RID: 19875 RVA: 0x000F6AD8 File Offset: 0x000F4CD8
				public bool axis(ProgramNode node, out axis value)
				{
					axis? axis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis.CreateSafe(this._builders, node);
					if (axis == null)
					{
						value = default(axis);
						return false;
					}
					value = axis.Value;
					return true;
				}

				// Token: 0x06004DA4 RID: 19876 RVA: 0x000F6B14 File Offset: 0x000F4D14
				public bool dir(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DA5 RID: 19877 RVA: 0x000F6B38 File Offset: 0x000F4D38
				public bool dir(ProgramNode node, out dir value)
				{
					dir? dir = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir.CreateSafe(this._builders, node);
					if (dir == null)
					{
						value = default(dir);
						return false;
					}
					value = dir.Value;
					return true;
				}

				// Token: 0x06004DA6 RID: 19878 RVA: 0x000F6B74 File Offset: 0x000F4D74
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DA7 RID: 19879 RVA: 0x000F6B98 File Offset: 0x000F4D98
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x06004DA8 RID: 19880 RVA: 0x000F6BD4 File Offset: 0x000F4DD4
				public bool tolerance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DA9 RID: 19881 RVA: 0x000F6BF8 File Offset: 0x000F4DF8
				public bool tolerance(ProgramNode node, out tolerance value)
				{
					tolerance? tolerance = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance.CreateSafe(this._builders, node);
					if (tolerance == null)
					{
						value = default(tolerance);
						return false;
					}
					value = tolerance.Value;
					return true;
				}

				// Token: 0x06004DAA RID: 19882 RVA: 0x000F6C34 File Offset: 0x000F4E34
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DAB RID: 19883 RVA: 0x000F6C58 File Offset: 0x000F4E58
				public bool _LetB0(ProgramNode node, out _LetB0 value)
				{
					_LetB0? letB = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x06004DAC RID: 19884 RVA: 0x000F6C94 File Offset: 0x000F4E94
				public bool _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DAD RID: 19885 RVA: 0x000F6CB8 File Offset: 0x000F4EB8
				public bool _LetB1(ProgramNode node, out _LetB1 value)
				{
					_LetB1? letB = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(_LetB1);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x040022EA RID: 8938
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE1 RID: 3041
			public class RuleIs
			{
				// Token: 0x06004DAE RID: 19886 RVA: 0x000F6CF2 File Offset: 0x000F4EF2
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004DAF RID: 19887 RVA: 0x000F6D04 File Offset: 0x000F4F04
				public bool SnapToGlyphs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.SnapToGlyphs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DB0 RID: 19888 RVA: 0x000F6D28 File Offset: 0x000F4F28
				public bool SnapToGlyphs(ProgramNode node, out SnapToGlyphs value)
				{
					SnapToGlyphs? snapToGlyphs = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.SnapToGlyphs.CreateSafe(this._builders, node);
					if (snapToGlyphs == null)
					{
						value = default(SnapToGlyphs);
						return false;
					}
					value = snapToGlyphs.Value;
					return true;
				}

				// Token: 0x06004DB1 RID: 19889 RVA: 0x000F6D64 File Offset: 0x000F4F64
				public bool tableBounds_expandedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.tableBounds_expandedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DB2 RID: 19890 RVA: 0x000F6D88 File Offset: 0x000F4F88
				public bool tableBounds_expandedBounds(ProgramNode node, out tableBounds_expandedBounds value)
				{
					tableBounds_expandedBounds? tableBounds_expandedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.tableBounds_expandedBounds.CreateSafe(this._builders, node);
					if (tableBounds_expandedBounds == null)
					{
						value = default(tableBounds_expandedBounds);
						return false;
					}
					value = tableBounds_expandedBounds.Value;
					return true;
				}

				// Token: 0x06004DB3 RID: 19891 RVA: 0x000F6DC4 File Offset: 0x000F4FC4
				public bool Between(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.Between.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DB4 RID: 19892 RVA: 0x000F6DE8 File Offset: 0x000F4FE8
				public bool Between(ProgramNode node, out Between value)
				{
					Between? between = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.Between.CreateSafe(this._builders, node);
					if (between == null)
					{
						value = default(Between);
						return false;
					}
					value = between.Value;
					return true;
				}

				// Token: 0x06004DB5 RID: 19893 RVA: 0x000F6E24 File Offset: 0x000F5024
				public bool LetBetweenBefore(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenBefore.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DB6 RID: 19894 RVA: 0x000F6E48 File Offset: 0x000F5048
				public bool LetBetweenBefore(ProgramNode node, out LetBetweenBefore value)
				{
					LetBetweenBefore? letBetweenBefore = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenBefore.CreateSafe(this._builders, node);
					if (letBetweenBefore == null)
					{
						value = default(LetBetweenBefore);
						return false;
					}
					value = letBetweenBefore.Value;
					return true;
				}

				// Token: 0x06004DB7 RID: 19895 RVA: 0x000F6E84 File Offset: 0x000F5084
				public bool selectedBounds_fixedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.selectedBounds_fixedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DB8 RID: 19896 RVA: 0x000F6EA8 File Offset: 0x000F50A8
				public bool selectedBounds_fixedBounds(ProgramNode node, out selectedBounds_fixedBounds value)
				{
					selectedBounds_fixedBounds? selectedBounds_fixedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.selectedBounds_fixedBounds.CreateSafe(this._builders, node);
					if (selectedBounds_fixedBounds == null)
					{
						value = default(selectedBounds_fixedBounds);
						return false;
					}
					value = selectedBounds_fixedBounds.Value;
					return true;
				}

				// Token: 0x06004DB9 RID: 19897 RVA: 0x000F6EE4 File Offset: 0x000F50E4
				public bool PageBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.PageBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DBA RID: 19898 RVA: 0x000F6F08 File Offset: 0x000F5108
				public bool PageBounds(ProgramNode node, out PageBounds value)
				{
					PageBounds? pageBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.PageBounds.CreateSafe(this._builders, node);
					if (pageBounds == null)
					{
						value = default(PageBounds);
						return false;
					}
					value = pageBounds.Value;
					return true;
				}

				// Token: 0x06004DBB RID: 19899 RVA: 0x000F6F44 File Offset: 0x000F5144
				public bool NextSeparator(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DBC RID: 19900 RVA: 0x000F6F68 File Offset: 0x000F5168
				public bool NextSeparator(ProgramNode node, out NextSeparator value)
				{
					NextSeparator? nextSeparator = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator.CreateSafe(this._builders, node);
					if (nextSeparator == null)
					{
						value = default(NextSeparator);
						return false;
					}
					value = nextSeparator.Value;
					return true;
				}

				// Token: 0x06004DBD RID: 19901 RVA: 0x000F6FA4 File Offset: 0x000F51A4
				public bool LetBetweenAxis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenAxis.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DBE RID: 19902 RVA: 0x000F6FC8 File Offset: 0x000F51C8
				public bool LetBetweenAxis(ProgramNode node, out LetBetweenAxis value)
				{
					LetBetweenAxis? letBetweenAxis = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenAxis.CreateSafe(this._builders, node);
					if (letBetweenAxis == null)
					{
						value = default(LetBetweenAxis);
						return false;
					}
					value = letBetweenAxis.Value;
					return true;
				}

				// Token: 0x06004DBF RID: 19903 RVA: 0x000F7004 File Offset: 0x000F5204
				public bool expandedBounds_selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.expandedBounds_selectedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DC0 RID: 19904 RVA: 0x000F7028 File Offset: 0x000F5228
				public bool expandedBounds_selectedBounds(ProgramNode node, out expandedBounds_selectedBounds value)
				{
					expandedBounds_selectedBounds? expandedBounds_selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.expandedBounds_selectedBounds.CreateSafe(this._builders, node);
					if (expandedBounds_selectedBounds == null)
					{
						value = default(expandedBounds_selectedBounds);
						return false;
					}
					value = expandedBounds_selectedBounds.Value;
					return true;
				}

				// Token: 0x06004DC1 RID: 19905 RVA: 0x000F7064 File Offset: 0x000F5264
				public bool CombineBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.CombineBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DC2 RID: 19906 RVA: 0x000F7088 File Offset: 0x000F5288
				public bool CombineBounds(ProgramNode node, out CombineBounds value)
				{
					CombineBounds? combineBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.CombineBounds.CreateSafe(this._builders, node);
					if (combineBounds == null)
					{
						value = default(CombineBounds);
						return false;
					}
					value = combineBounds.Value;
					return true;
				}

				// Token: 0x06004DC3 RID: 19907 RVA: 0x000F70C4 File Offset: 0x000F52C4
				public bool beforeRelativeBounds_selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.beforeRelativeBounds_selectedBounds.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DC4 RID: 19908 RVA: 0x000F70E8 File Offset: 0x000F52E8
				public bool beforeRelativeBounds_selectedBounds(ProgramNode node, out beforeRelativeBounds_selectedBounds value)
				{
					beforeRelativeBounds_selectedBounds? beforeRelativeBounds_selectedBounds = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.beforeRelativeBounds_selectedBounds.CreateSafe(this._builders, node);
					if (beforeRelativeBounds_selectedBounds == null)
					{
						value = default(beforeRelativeBounds_selectedBounds);
						return false;
					}
					value = beforeRelativeBounds_selectedBounds.Value;
					return true;
				}

				// Token: 0x06004DC5 RID: 19909 RVA: 0x000F7124 File Offset: 0x000F5324
				public bool NextSeparator_beforeRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator_beforeRelative.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DC6 RID: 19910 RVA: 0x000F7148 File Offset: 0x000F5348
				public bool NextSeparator_beforeRelative(ProgramNode node, out NextSeparator_beforeRelative value)
				{
					NextSeparator_beforeRelative? nextSeparator_beforeRelative = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator_beforeRelative.CreateSafe(this._builders, node);
					if (nextSeparator_beforeRelative == null)
					{
						value = default(NextSeparator_beforeRelative);
						return false;
					}
					value = nextSeparator_beforeRelative.Value;
					return true;
				}

				// Token: 0x06004DC7 RID: 19911 RVA: 0x000F7184 File Offset: 0x000F5384
				public bool NextSameWidthSeparator(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSameWidthSeparator.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DC8 RID: 19912 RVA: 0x000F71A8 File Offset: 0x000F53A8
				public bool NextSameWidthSeparator(ProgramNode node, out NextSameWidthSeparator value)
				{
					NextSameWidthSeparator? nextSameWidthSeparator = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSameWidthSeparator.CreateSafe(this._builders, node);
					if (nextSameWidthSeparator == null)
					{
						value = default(NextSameWidthSeparator);
						return false;
					}
					value = nextSameWidthSeparator.Value;
					return true;
				}

				// Token: 0x06004DC9 RID: 19913 RVA: 0x000F71E4 File Offset: 0x000F53E4
				public bool NextFontSizeDecrease(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextFontSizeDecrease.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DCA RID: 19914 RVA: 0x000F7208 File Offset: 0x000F5408
				public bool NextFontSizeDecrease(ProgramNode node, out NextFontSizeDecrease value)
				{
					NextFontSizeDecrease? nextFontSizeDecrease = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextFontSizeDecrease.CreateSafe(this._builders, node);
					if (nextFontSizeDecrease == null)
					{
						value = default(NextFontSizeDecrease);
						return false;
					}
					value = nextFontSizeDecrease.Value;
					return true;
				}

				// Token: 0x06004DCB RID: 19915 RVA: 0x000F7244 File Offset: 0x000F5444
				public bool fixedBounds_tableIdentifier(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.fixedBounds_tableIdentifier.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06004DCC RID: 19916 RVA: 0x000F7268 File Offset: 0x000F5468
				public bool fixedBounds_tableIdentifier(ProgramNode node, out fixedBounds_tableIdentifier value)
				{
					fixedBounds_tableIdentifier? fixedBounds_tableIdentifier = Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.fixedBounds_tableIdentifier.CreateSafe(this._builders, node);
					if (fixedBounds_tableIdentifier == null)
					{
						value = default(fixedBounds_tableIdentifier);
						return false;
					}
					value = fixedBounds_tableIdentifier.Value;
					return true;
				}

				// Token: 0x040022EB RID: 8939
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE2 RID: 3042
			public class NodeAs
			{
				// Token: 0x06004DCD RID: 19917 RVA: 0x000F72A2 File Offset: 0x000F54A2
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004DCE RID: 19918 RVA: 0x000F72B1 File Offset: 0x000F54B1
				public tableBounds? tableBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DCF RID: 19919 RVA: 0x000F72BF File Offset: 0x000F54BF
				public selectedBounds? selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD0 RID: 19920 RVA: 0x000F72CD File Offset: 0x000F54CD
				public betweenAxis? betweenAxis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD1 RID: 19921 RVA: 0x000F72DB File Offset: 0x000F54DB
				public before? before(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD2 RID: 19922 RVA: 0x000F72E9 File Offset: 0x000F54E9
				public expandedBounds? expandedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD3 RID: 19923 RVA: 0x000F72F7 File Offset: 0x000F54F7
				public beforeRelativeBounds? beforeRelativeBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD4 RID: 19924 RVA: 0x000F7305 File Offset: 0x000F5505
				public fixedBounds? fixedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD5 RID: 19925 RVA: 0x000F7313 File Offset: 0x000F5513
				public axis? axis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD6 RID: 19926 RVA: 0x000F7321 File Offset: 0x000F5521
				public dir? dir(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD7 RID: 19927 RVA: 0x000F732F File Offset: 0x000F552F
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD8 RID: 19928 RVA: 0x000F733D File Offset: 0x000F553D
				public tolerance? tolerance(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DD9 RID: 19929 RVA: 0x000F734B File Offset: 0x000F554B
				public _LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DDA RID: 19930 RVA: 0x000F7359 File Offset: 0x000F5559
				public _LetB1? _LetB1(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1.CreateSafe(this._builders, node);
				}

				// Token: 0x040022EC RID: 8940
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE3 RID: 3043
			public class RuleAs
			{
				// Token: 0x06004DDB RID: 19931 RVA: 0x000F7367 File Offset: 0x000F5567
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004DDC RID: 19932 RVA: 0x000F7376 File Offset: 0x000F5576
				public SnapToGlyphs? SnapToGlyphs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.SnapToGlyphs.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DDD RID: 19933 RVA: 0x000F7384 File Offset: 0x000F5584
				public tableBounds_expandedBounds? tableBounds_expandedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.tableBounds_expandedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DDE RID: 19934 RVA: 0x000F7392 File Offset: 0x000F5592
				public Between? Between(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.Between.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DDF RID: 19935 RVA: 0x000F73A0 File Offset: 0x000F55A0
				public LetBetweenBefore? LetBetweenBefore(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenBefore.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE0 RID: 19936 RVA: 0x000F73AE File Offset: 0x000F55AE
				public selectedBounds_fixedBounds? selectedBounds_fixedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.selectedBounds_fixedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE1 RID: 19937 RVA: 0x000F73BC File Offset: 0x000F55BC
				public PageBounds? PageBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.PageBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE2 RID: 19938 RVA: 0x000F73CA File Offset: 0x000F55CA
				public NextSeparator? NextSeparator(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE3 RID: 19939 RVA: 0x000F73D8 File Offset: 0x000F55D8
				public LetBetweenAxis? LetBetweenAxis(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.LetBetweenAxis.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE4 RID: 19940 RVA: 0x000F73E6 File Offset: 0x000F55E6
				public expandedBounds_selectedBounds? expandedBounds_selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.expandedBounds_selectedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE5 RID: 19941 RVA: 0x000F73F4 File Offset: 0x000F55F4
				public CombineBounds? CombineBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.CombineBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE6 RID: 19942 RVA: 0x000F7402 File Offset: 0x000F5602
				public beforeRelativeBounds_selectedBounds? beforeRelativeBounds_selectedBounds(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.beforeRelativeBounds_selectedBounds.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE7 RID: 19943 RVA: 0x000F7410 File Offset: 0x000F5610
				public NextSeparator_beforeRelative? NextSeparator_beforeRelative(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSeparator_beforeRelative.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE8 RID: 19944 RVA: 0x000F741E File Offset: 0x000F561E
				public NextSameWidthSeparator? NextSameWidthSeparator(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextSameWidthSeparator.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DE9 RID: 19945 RVA: 0x000F742C File Offset: 0x000F562C
				public NextFontSizeDecrease? NextFontSizeDecrease(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes.NextFontSizeDecrease.CreateSafe(this._builders, node);
				}

				// Token: 0x06004DEA RID: 19946 RVA: 0x000F743A File Offset: 0x000F563A
				public fixedBounds_tableIdentifier? fixedBounds_tableIdentifier(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes.fixedBounds_tableIdentifier.CreateSafe(this._builders, node);
				}

				// Token: 0x040022ED RID: 8941
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02000BE5 RID: 3045
		public class Sets
		{
			// Token: 0x06004DEE RID: 19950 RVA: 0x000F7464 File Offset: 0x000F5664
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17000E03 RID: 3587
			// (get) Token: 0x06004DEF RID: 19951 RVA: 0x000F74B3 File Offset: 0x000F56B3
			// (set) Token: 0x06004DF0 RID: 19952 RVA: 0x000F74BB File Offset: 0x000F56BB
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17000E04 RID: 3588
			// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x000F74C4 File Offset: 0x000F56C4
			// (set) Token: 0x06004DF2 RID: 19954 RVA: 0x000F74CC File Offset: 0x000F56CC
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17000E05 RID: 3589
			// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x000F74D5 File Offset: 0x000F56D5
			// (set) Token: 0x06004DF4 RID: 19956 RVA: 0x000F74DD File Offset: 0x000F56DD
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17000E06 RID: 3590
			// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x000F74E6 File Offset: 0x000F56E6
			// (set) Token: 0x06004DF6 RID: 19958 RVA: 0x000F74EE File Offset: 0x000F56EE
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17000E07 RID: 3591
			// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x000F74F7 File Offset: 0x000F56F7
			// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x000F74FF File Offset: 0x000F56FF
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02000BE6 RID: 3046
			public class Joins
			{
				// Token: 0x06004DF9 RID: 19961 RVA: 0x000F7508 File Offset: 0x000F5708
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004DFA RID: 19962 RVA: 0x000F7517 File Offset: 0x000F5717
				public ProgramSetBuilder<tableBounds> SnapToGlyphs(ProgramSetBuilder<expandedBounds> value0)
				{
					return ProgramSetBuilder<tableBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SnapToGlyphs, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004DFB RID: 19963 RVA: 0x000F7548 File Offset: 0x000F5748
				public ProgramSetBuilder<_LetB0> Between(ProgramSetBuilder<betweenAxis> value0, ProgramSetBuilder<before> value1, ProgramSetBuilder<beforeRelativeBounds> value2)
				{
					return ProgramSetBuilder<_LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Between, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004DFC RID: 19964 RVA: 0x000F75A2 File Offset: 0x000F57A2
				public ProgramSetBuilder<selectedBounds> PageBounds(ProgramSetBuilder<fixedBounds> value0)
				{
					return ProgramSetBuilder<selectedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.PageBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004DFD RID: 19965 RVA: 0x000F75D4 File Offset: 0x000F57D4
				public ProgramSetBuilder<selectedBounds> NextSeparator(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<selectedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NextSeparator, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004DFE RID: 19966 RVA: 0x000F762E File Offset: 0x000F582E
				public ProgramSetBuilder<expandedBounds> CombineBounds(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<selectedBounds> value1)
				{
					return ProgramSetBuilder<expandedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.CombineBounds, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004DFF RID: 19967 RVA: 0x000F7670 File Offset: 0x000F5870
				public ProgramSetBuilder<beforeRelativeBounds> NextSeparator_beforeRelative(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NextSeparator_beforeRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004E00 RID: 19968 RVA: 0x000F76CC File Offset: 0x000F58CC
				public ProgramSetBuilder<beforeRelativeBounds> NextSameWidthSeparator(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<tolerance> value3)
				{
					return ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NextSameWidthSeparator, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x06004E01 RID: 19969 RVA: 0x000F7737 File Offset: 0x000F5937
				public ProgramSetBuilder<beforeRelativeBounds> NextFontSizeDecrease(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1)
				{
					return ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.NextFontSizeDecrease, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004E02 RID: 19970 RVA: 0x000F7777 File Offset: 0x000F5977
				public ProgramSetBuilder<_LetB1> LetBetweenBefore(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<_LetB0> value1)
				{
					return ProgramSetBuilder<_LetB1>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetBetweenBefore, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004E03 RID: 19971 RVA: 0x000F77B7 File Offset: 0x000F59B7
				public ProgramSetBuilder<selectedBounds> LetBetweenAxis(ProgramSetBuilder<axis> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return ProgramSetBuilder<selectedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LetBetweenAxis, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040022F4 RID: 8948
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE7 RID: 3047
			public class ExplicitJoins
			{
				// Token: 0x06004E04 RID: 19972 RVA: 0x000F77F7 File Offset: 0x000F59F7
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004E05 RID: 19973 RVA: 0x000F7806 File Offset: 0x000F5A06
				public JoinProgramSetBuilder<tableBounds> SnapToGlyphs(ProgramSetBuilder<expandedBounds> value0)
				{
					return JoinProgramSetBuilder<tableBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SnapToGlyphs, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E06 RID: 19974 RVA: 0x000F7838 File Offset: 0x000F5A38
				public JoinProgramSetBuilder<_LetB0> Between(ProgramSetBuilder<betweenAxis> value0, ProgramSetBuilder<before> value1, ProgramSetBuilder<beforeRelativeBounds> value2)
				{
					return JoinProgramSetBuilder<_LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Between, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004E07 RID: 19975 RVA: 0x000F7892 File Offset: 0x000F5A92
				public JoinProgramSetBuilder<selectedBounds> PageBounds(ProgramSetBuilder<fixedBounds> value0)
				{
					return JoinProgramSetBuilder<selectedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.PageBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E08 RID: 19976 RVA: 0x000F78C4 File Offset: 0x000F5AC4
				public JoinProgramSetBuilder<selectedBounds> NextSeparator(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<selectedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NextSeparator, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004E09 RID: 19977 RVA: 0x000F791E File Offset: 0x000F5B1E
				public JoinProgramSetBuilder<expandedBounds> CombineBounds(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<selectedBounds> value1)
				{
					return JoinProgramSetBuilder<expandedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.CombineBounds, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004E0A RID: 19978 RVA: 0x000F7960 File Offset: 0x000F5B60
				public JoinProgramSetBuilder<beforeRelativeBounds> NextSeparator_beforeRelative(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NextSeparator_beforeRelative, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06004E0B RID: 19979 RVA: 0x000F79BC File Offset: 0x000F5BBC
				public JoinProgramSetBuilder<beforeRelativeBounds> NextSameWidthSeparator(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1, ProgramSetBuilder<k> value2, ProgramSetBuilder<tolerance> value3)
				{
					return JoinProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NextSameWidthSeparator, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x06004E0C RID: 19980 RVA: 0x000F7A27 File Offset: 0x000F5C27
				public JoinProgramSetBuilder<beforeRelativeBounds> NextFontSizeDecrease(ProgramSetBuilder<before> value0, ProgramSetBuilder<dir> value1)
				{
					return JoinProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.NextFontSizeDecrease, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004E0D RID: 19981 RVA: 0x000F7A67 File Offset: 0x000F5C67
				public JoinProgramSetBuilder<_LetB1> LetBetweenBefore(ProgramSetBuilder<selectedBounds> value0, ProgramSetBuilder<_LetB0> value1)
				{
					return JoinProgramSetBuilder<_LetB1>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetBetweenBefore, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06004E0E RID: 19982 RVA: 0x000F7AA7 File Offset: 0x000F5CA7
				public JoinProgramSetBuilder<selectedBounds> LetBetweenAxis(ProgramSetBuilder<axis> value0, ProgramSetBuilder<_LetB1> value1)
				{
					return JoinProgramSetBuilder<selectedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LetBetweenAxis, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040022F5 RID: 8949
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE8 RID: 3048
			public class JoinUnnamedConversions
			{
				// Token: 0x06004E0F RID: 19983 RVA: 0x000F7AE7 File Offset: 0x000F5CE7
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004E10 RID: 19984 RVA: 0x000F7AF6 File Offset: 0x000F5CF6
				public ProgramSetBuilder<tableBounds> tableBounds_expandedBounds(ProgramSetBuilder<expandedBounds> value0)
				{
					return ProgramSetBuilder<tableBounds>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.tableBounds_expandedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E11 RID: 19985 RVA: 0x000F7B27 File Offset: 0x000F5D27
				public ProgramSetBuilder<selectedBounds> selectedBounds_fixedBounds(ProgramSetBuilder<fixedBounds> value0)
				{
					return ProgramSetBuilder<selectedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selectedBounds_fixedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E12 RID: 19986 RVA: 0x000F7B58 File Offset: 0x000F5D58
				public ProgramSetBuilder<expandedBounds> expandedBounds_selectedBounds(ProgramSetBuilder<selectedBounds> value0)
				{
					return ProgramSetBuilder<expandedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.expandedBounds_selectedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E13 RID: 19987 RVA: 0x000F7B89 File Offset: 0x000F5D89
				public ProgramSetBuilder<beforeRelativeBounds> beforeRelativeBounds_selectedBounds(ProgramSetBuilder<selectedBounds> value0)
				{
					return ProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.beforeRelativeBounds_selectedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E14 RID: 19988 RVA: 0x000F7BBA File Offset: 0x000F5DBA
				public ProgramSetBuilder<fixedBounds> fixedBounds_tableIdentifier(ProgramSetBuilder<tableIdentifier> value0)
				{
					return ProgramSetBuilder<fixedBounds>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.fixedBounds_tableIdentifier, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040022F6 RID: 8950
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BE9 RID: 3049
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06004E15 RID: 19989 RVA: 0x000F7BEB File Offset: 0x000F5DEB
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004E16 RID: 19990 RVA: 0x000F7BFA File Offset: 0x000F5DFA
				public JoinProgramSetBuilder<tableBounds> tableBounds_expandedBounds(ProgramSetBuilder<expandedBounds> value0)
				{
					return JoinProgramSetBuilder<tableBounds>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.tableBounds_expandedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E17 RID: 19991 RVA: 0x000F7C2B File Offset: 0x000F5E2B
				public JoinProgramSetBuilder<selectedBounds> selectedBounds_fixedBounds(ProgramSetBuilder<fixedBounds> value0)
				{
					return JoinProgramSetBuilder<selectedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selectedBounds_fixedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E18 RID: 19992 RVA: 0x000F7C5C File Offset: 0x000F5E5C
				public JoinProgramSetBuilder<expandedBounds> expandedBounds_selectedBounds(ProgramSetBuilder<selectedBounds> value0)
				{
					return JoinProgramSetBuilder<expandedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.expandedBounds_selectedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E19 RID: 19993 RVA: 0x000F7C8D File Offset: 0x000F5E8D
				public JoinProgramSetBuilder<beforeRelativeBounds> beforeRelativeBounds_selectedBounds(ProgramSetBuilder<selectedBounds> value0)
				{
					return JoinProgramSetBuilder<beforeRelativeBounds>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.beforeRelativeBounds_selectedBounds, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06004E1A RID: 19994 RVA: 0x000F7CBE File Offset: 0x000F5EBE
				public JoinProgramSetBuilder<fixedBounds> fixedBounds_tableIdentifier(ProgramSetBuilder<tableIdentifier> value0)
				{
					return JoinProgramSetBuilder<fixedBounds>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.fixedBounds_tableIdentifier, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040022F7 RID: 8951
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02000BEA RID: 3050
			public class Casts
			{
				// Token: 0x06004E1B RID: 19995 RVA: 0x000F7CEF File Offset: 0x000F5EEF
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06004E1C RID: 19996 RVA: 0x000F7D00 File Offset: 0x000F5F00
				public ProgramSetBuilder<tableBounds> tableBounds(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.tableBounds)
					{
						string text = "set";
						string text2 = "expected program set for symbol tableBounds but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tableBounds>.CreateUnsafe(set);
				}

				// Token: 0x06004E1D RID: 19997 RVA: 0x000F7D58 File Offset: 0x000F5F58
				public ProgramSetBuilder<selectedBounds> selectedBounds(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectedBounds)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectedBounds but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.selectedBounds>.CreateUnsafe(set);
				}

				// Token: 0x06004E1E RID: 19998 RVA: 0x000F7DB0 File Offset: 0x000F5FB0
				public ProgramSetBuilder<betweenAxis> betweenAxis(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.betweenAxis)
					{
						string text = "set";
						string text2 = "expected program set for symbol betweenAxis but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.betweenAxis>.CreateUnsafe(set);
				}

				// Token: 0x06004E1F RID: 19999 RVA: 0x000F7E08 File Offset: 0x000F6008
				public ProgramSetBuilder<before> before(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.before)
					{
						string text = "set";
						string text2 = "expected program set for symbol before but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.before>.CreateUnsafe(set);
				}

				// Token: 0x06004E20 RID: 20000 RVA: 0x000F7E60 File Offset: 0x000F6060
				public ProgramSetBuilder<expandedBounds> expandedBounds(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.expandedBounds)
					{
						string text = "set";
						string text2 = "expected program set for symbol expandedBounds but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.expandedBounds>.CreateUnsafe(set);
				}

				// Token: 0x06004E21 RID: 20001 RVA: 0x000F7EB8 File Offset: 0x000F60B8
				public ProgramSetBuilder<beforeRelativeBounds> beforeRelativeBounds(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.beforeRelativeBounds)
					{
						string text = "set";
						string text2 = "expected program set for symbol beforeRelativeBounds but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.beforeRelativeBounds>.CreateUnsafe(set);
				}

				// Token: 0x06004E22 RID: 20002 RVA: 0x000F7F10 File Offset: 0x000F6110
				public ProgramSetBuilder<fixedBounds> fixedBounds(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.fixedBounds)
					{
						string text = "set";
						string text2 = "expected program set for symbol fixedBounds but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.fixedBounds>.CreateUnsafe(set);
				}

				// Token: 0x06004E23 RID: 20003 RVA: 0x000F7F68 File Offset: 0x000F6168
				public ProgramSetBuilder<axis> axis(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.axis)
					{
						string text = "set";
						string text2 = "expected program set for symbol axis but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.axis>.CreateUnsafe(set);
				}

				// Token: 0x06004E24 RID: 20004 RVA: 0x000F7FC0 File Offset: 0x000F61C0
				public ProgramSetBuilder<dir> dir(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.dir)
					{
						string text = "set";
						string text2 = "expected program set for symbol dir but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.dir>.CreateUnsafe(set);
				}

				// Token: 0x06004E25 RID: 20005 RVA: 0x000F8018 File Offset: 0x000F6218
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06004E26 RID: 20006 RVA: 0x000F8070 File Offset: 0x000F6270
				public ProgramSetBuilder<tolerance> tolerance(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.tolerance)
					{
						string text = "set";
						string text2 = "expected program set for symbol tolerance but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes.tolerance>.CreateUnsafe(set);
				}

				// Token: 0x06004E27 RID: 20007 RVA: 0x000F80C8 File Offset: 0x000F62C8
				public ProgramSetBuilder<_LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x06004E28 RID: 20008 RVA: 0x000F8120 File Offset: 0x000F6320
				public ProgramSetBuilder<_LetB1> _LetB1(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB1)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB1 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes._LetB1>.CreateUnsafe(set);
				}

				// Token: 0x040022F8 RID: 8952
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
