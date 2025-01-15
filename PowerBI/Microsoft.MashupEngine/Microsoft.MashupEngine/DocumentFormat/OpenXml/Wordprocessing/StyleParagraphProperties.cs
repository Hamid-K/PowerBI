using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA2 RID: 12194
	[ChildElementInfo(typeof(AdjustRightIndent))]
	[ChildElementInfo(typeof(Justification))]
	[ChildElementInfo(typeof(KeepNext))]
	[ChildElementInfo(typeof(KeepLines))]
	[ChildElementInfo(typeof(PageBreakBefore))]
	[ChildElementInfo(typeof(FrameProperties))]
	[ChildElementInfo(typeof(WidowControl))]
	[ChildElementInfo(typeof(NumberingProperties))]
	[ChildElementInfo(typeof(SuppressLineNumbers))]
	[ChildElementInfo(typeof(ParagraphBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(Tabs))]
	[ChildElementInfo(typeof(SuppressAutoHyphens))]
	[ChildElementInfo(typeof(Kinsoku))]
	[ChildElementInfo(typeof(WordWrap))]
	[ChildElementInfo(typeof(OverflowPunctuation))]
	[ChildElementInfo(typeof(TopLinePunctuation))]
	[ChildElementInfo(typeof(AutoSpaceDE))]
	[ChildElementInfo(typeof(AutoSpaceDN))]
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(SpacingBetweenLines))]
	[ChildElementInfo(typeof(Indentation))]
	[ChildElementInfo(typeof(ContextualSpacing))]
	[ChildElementInfo(typeof(MirrorIndents))]
	[ChildElementInfo(typeof(SuppressOverlap))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TextAlignment))]
	[ChildElementInfo(typeof(TextBoxTightWrap))]
	[ChildElementInfo(typeof(OutlineLevel))]
	[ChildElementInfo(typeof(ParagraphPropertiesChange))]
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleParagraphProperties : OpenXmlCompositeElement
	{
		// Token: 0x170092A4 RID: 37540
		// (get) Token: 0x0601A594 RID: 107924 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x170092A5 RID: 37541
		// (get) Token: 0x0601A595 RID: 107925 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092A6 RID: 37542
		// (get) Token: 0x0601A596 RID: 107926 RVA: 0x00360FBC File Offset: 0x0035F1BC
		internal override int ElementTypeId
		{
			get
			{
				return 11888;
			}
		}

		// Token: 0x0601A597 RID: 107927 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A598 RID: 107928 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleParagraphProperties()
		{
		}

		// Token: 0x0601A599 RID: 107929 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A59A RID: 107930 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A59B RID: 107931 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A59C RID: 107932 RVA: 0x00360FC4 File Offset: 0x0035F1C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "keepNext" == name)
			{
				return new KeepNext();
			}
			if (23 == namespaceId && "keepLines" == name)
			{
				return new KeepLines();
			}
			if (23 == namespaceId && "pageBreakBefore" == name)
			{
				return new PageBreakBefore();
			}
			if (23 == namespaceId && "framePr" == name)
			{
				return new FrameProperties();
			}
			if (23 == namespaceId && "widowControl" == name)
			{
				return new WidowControl();
			}
			if (23 == namespaceId && "numPr" == name)
			{
				return new NumberingProperties();
			}
			if (23 == namespaceId && "suppressLineNumbers" == name)
			{
				return new SuppressLineNumbers();
			}
			if (23 == namespaceId && "pBdr" == name)
			{
				return new ParagraphBorders();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "tabs" == name)
			{
				return new Tabs();
			}
			if (23 == namespaceId && "suppressAutoHyphens" == name)
			{
				return new SuppressAutoHyphens();
			}
			if (23 == namespaceId && "kinsoku" == name)
			{
				return new Kinsoku();
			}
			if (23 == namespaceId && "wordWrap" == name)
			{
				return new WordWrap();
			}
			if (23 == namespaceId && "overflowPunct" == name)
			{
				return new OverflowPunctuation();
			}
			if (23 == namespaceId && "topLinePunct" == name)
			{
				return new TopLinePunctuation();
			}
			if (23 == namespaceId && "autoSpaceDE" == name)
			{
				return new AutoSpaceDE();
			}
			if (23 == namespaceId && "autoSpaceDN" == name)
			{
				return new AutoSpaceDN();
			}
			if (23 == namespaceId && "bidi" == name)
			{
				return new BiDi();
			}
			if (23 == namespaceId && "adjustRightInd" == name)
			{
				return new AdjustRightIndent();
			}
			if (23 == namespaceId && "snapToGrid" == name)
			{
				return new SnapToGrid();
			}
			if (23 == namespaceId && "spacing" == name)
			{
				return new SpacingBetweenLines();
			}
			if (23 == namespaceId && "ind" == name)
			{
				return new Indentation();
			}
			if (23 == namespaceId && "contextualSpacing" == name)
			{
				return new ContextualSpacing();
			}
			if (23 == namespaceId && "mirrorIndents" == name)
			{
				return new MirrorIndents();
			}
			if (23 == namespaceId && "suppressOverlap" == name)
			{
				return new SuppressOverlap();
			}
			if (23 == namespaceId && "jc" == name)
			{
				return new Justification();
			}
			if (23 == namespaceId && "textDirection" == name)
			{
				return new TextDirection();
			}
			if (23 == namespaceId && "textAlignment" == name)
			{
				return new TextAlignment();
			}
			if (23 == namespaceId && "textboxTightWrap" == name)
			{
				return new TextBoxTightWrap();
			}
			if (23 == namespaceId && "outlineLvl" == name)
			{
				return new OutlineLevel();
			}
			if (23 == namespaceId && "pPrChange" == name)
			{
				return new ParagraphPropertiesChange();
			}
			return null;
		}

		// Token: 0x170092A7 RID: 37543
		// (get) Token: 0x0601A59D RID: 107933 RVA: 0x003612BA File Offset: 0x0035F4BA
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleParagraphProperties.eleTagNames;
			}
		}

		// Token: 0x170092A8 RID: 37544
		// (get) Token: 0x0601A59E RID: 107934 RVA: 0x003612C1 File Offset: 0x0035F4C1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleParagraphProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170092A9 RID: 37545
		// (get) Token: 0x0601A59F RID: 107935 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170092AA RID: 37546
		// (get) Token: 0x0601A5A0 RID: 107936 RVA: 0x0035ECAC File Offset: 0x0035CEAC
		// (set) Token: 0x0601A5A1 RID: 107937 RVA: 0x0035ECB5 File Offset: 0x0035CEB5
		public KeepNext KeepNext
		{
			get
			{
				return base.GetElement<KeepNext>(0);
			}
			set
			{
				base.SetElement<KeepNext>(0, value);
			}
		}

		// Token: 0x170092AB RID: 37547
		// (get) Token: 0x0601A5A2 RID: 107938 RVA: 0x0035ECBF File Offset: 0x0035CEBF
		// (set) Token: 0x0601A5A3 RID: 107939 RVA: 0x0035ECC8 File Offset: 0x0035CEC8
		public KeepLines KeepLines
		{
			get
			{
				return base.GetElement<KeepLines>(1);
			}
			set
			{
				base.SetElement<KeepLines>(1, value);
			}
		}

		// Token: 0x170092AC RID: 37548
		// (get) Token: 0x0601A5A4 RID: 107940 RVA: 0x0035ECD2 File Offset: 0x0035CED2
		// (set) Token: 0x0601A5A5 RID: 107941 RVA: 0x0035ECDB File Offset: 0x0035CEDB
		public PageBreakBefore PageBreakBefore
		{
			get
			{
				return base.GetElement<PageBreakBefore>(2);
			}
			set
			{
				base.SetElement<PageBreakBefore>(2, value);
			}
		}

		// Token: 0x170092AD RID: 37549
		// (get) Token: 0x0601A5A6 RID: 107942 RVA: 0x0035ECE5 File Offset: 0x0035CEE5
		// (set) Token: 0x0601A5A7 RID: 107943 RVA: 0x0035ECEE File Offset: 0x0035CEEE
		public FrameProperties FrameProperties
		{
			get
			{
				return base.GetElement<FrameProperties>(3);
			}
			set
			{
				base.SetElement<FrameProperties>(3, value);
			}
		}

		// Token: 0x170092AE RID: 37550
		// (get) Token: 0x0601A5A8 RID: 107944 RVA: 0x0035ECF8 File Offset: 0x0035CEF8
		// (set) Token: 0x0601A5A9 RID: 107945 RVA: 0x0035ED01 File Offset: 0x0035CF01
		public WidowControl WidowControl
		{
			get
			{
				return base.GetElement<WidowControl>(4);
			}
			set
			{
				base.SetElement<WidowControl>(4, value);
			}
		}

		// Token: 0x170092AF RID: 37551
		// (get) Token: 0x0601A5AA RID: 107946 RVA: 0x0035ED0B File Offset: 0x0035CF0B
		// (set) Token: 0x0601A5AB RID: 107947 RVA: 0x0035ED14 File Offset: 0x0035CF14
		public NumberingProperties NumberingProperties
		{
			get
			{
				return base.GetElement<NumberingProperties>(5);
			}
			set
			{
				base.SetElement<NumberingProperties>(5, value);
			}
		}

		// Token: 0x170092B0 RID: 37552
		// (get) Token: 0x0601A5AC RID: 107948 RVA: 0x0035ED1E File Offset: 0x0035CF1E
		// (set) Token: 0x0601A5AD RID: 107949 RVA: 0x0035ED27 File Offset: 0x0035CF27
		public SuppressLineNumbers SuppressLineNumbers
		{
			get
			{
				return base.GetElement<SuppressLineNumbers>(6);
			}
			set
			{
				base.SetElement<SuppressLineNumbers>(6, value);
			}
		}

		// Token: 0x170092B1 RID: 37553
		// (get) Token: 0x0601A5AE RID: 107950 RVA: 0x0035ED31 File Offset: 0x0035CF31
		// (set) Token: 0x0601A5AF RID: 107951 RVA: 0x0035ED3A File Offset: 0x0035CF3A
		public ParagraphBorders ParagraphBorders
		{
			get
			{
				return base.GetElement<ParagraphBorders>(7);
			}
			set
			{
				base.SetElement<ParagraphBorders>(7, value);
			}
		}

		// Token: 0x170092B2 RID: 37554
		// (get) Token: 0x0601A5B0 RID: 107952 RVA: 0x0035ED44 File Offset: 0x0035CF44
		// (set) Token: 0x0601A5B1 RID: 107953 RVA: 0x0035ED4D File Offset: 0x0035CF4D
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(8);
			}
			set
			{
				base.SetElement<Shading>(8, value);
			}
		}

		// Token: 0x170092B3 RID: 37555
		// (get) Token: 0x0601A5B2 RID: 107954 RVA: 0x0035ED57 File Offset: 0x0035CF57
		// (set) Token: 0x0601A5B3 RID: 107955 RVA: 0x0035ED61 File Offset: 0x0035CF61
		public Tabs Tabs
		{
			get
			{
				return base.GetElement<Tabs>(9);
			}
			set
			{
				base.SetElement<Tabs>(9, value);
			}
		}

		// Token: 0x170092B4 RID: 37556
		// (get) Token: 0x0601A5B4 RID: 107956 RVA: 0x0035ED6C File Offset: 0x0035CF6C
		// (set) Token: 0x0601A5B5 RID: 107957 RVA: 0x0035ED76 File Offset: 0x0035CF76
		public SuppressAutoHyphens SuppressAutoHyphens
		{
			get
			{
				return base.GetElement<SuppressAutoHyphens>(10);
			}
			set
			{
				base.SetElement<SuppressAutoHyphens>(10, value);
			}
		}

		// Token: 0x170092B5 RID: 37557
		// (get) Token: 0x0601A5B6 RID: 107958 RVA: 0x0035ED81 File Offset: 0x0035CF81
		// (set) Token: 0x0601A5B7 RID: 107959 RVA: 0x0035ED8B File Offset: 0x0035CF8B
		public Kinsoku Kinsoku
		{
			get
			{
				return base.GetElement<Kinsoku>(11);
			}
			set
			{
				base.SetElement<Kinsoku>(11, value);
			}
		}

		// Token: 0x170092B6 RID: 37558
		// (get) Token: 0x0601A5B8 RID: 107960 RVA: 0x0035ED96 File Offset: 0x0035CF96
		// (set) Token: 0x0601A5B9 RID: 107961 RVA: 0x0035EDA0 File Offset: 0x0035CFA0
		public WordWrap WordWrap
		{
			get
			{
				return base.GetElement<WordWrap>(12);
			}
			set
			{
				base.SetElement<WordWrap>(12, value);
			}
		}

		// Token: 0x170092B7 RID: 37559
		// (get) Token: 0x0601A5BA RID: 107962 RVA: 0x0035EDAB File Offset: 0x0035CFAB
		// (set) Token: 0x0601A5BB RID: 107963 RVA: 0x0035EDB5 File Offset: 0x0035CFB5
		public OverflowPunctuation OverflowPunctuation
		{
			get
			{
				return base.GetElement<OverflowPunctuation>(13);
			}
			set
			{
				base.SetElement<OverflowPunctuation>(13, value);
			}
		}

		// Token: 0x170092B8 RID: 37560
		// (get) Token: 0x0601A5BC RID: 107964 RVA: 0x0035EDC0 File Offset: 0x0035CFC0
		// (set) Token: 0x0601A5BD RID: 107965 RVA: 0x0035EDCA File Offset: 0x0035CFCA
		public TopLinePunctuation TopLinePunctuation
		{
			get
			{
				return base.GetElement<TopLinePunctuation>(14);
			}
			set
			{
				base.SetElement<TopLinePunctuation>(14, value);
			}
		}

		// Token: 0x170092B9 RID: 37561
		// (get) Token: 0x0601A5BE RID: 107966 RVA: 0x0035EDD5 File Offset: 0x0035CFD5
		// (set) Token: 0x0601A5BF RID: 107967 RVA: 0x0035EDDF File Offset: 0x0035CFDF
		public AutoSpaceDE AutoSpaceDE
		{
			get
			{
				return base.GetElement<AutoSpaceDE>(15);
			}
			set
			{
				base.SetElement<AutoSpaceDE>(15, value);
			}
		}

		// Token: 0x170092BA RID: 37562
		// (get) Token: 0x0601A5C0 RID: 107968 RVA: 0x0035EDEA File Offset: 0x0035CFEA
		// (set) Token: 0x0601A5C1 RID: 107969 RVA: 0x0035EDF4 File Offset: 0x0035CFF4
		public AutoSpaceDN AutoSpaceDN
		{
			get
			{
				return base.GetElement<AutoSpaceDN>(16);
			}
			set
			{
				base.SetElement<AutoSpaceDN>(16, value);
			}
		}

		// Token: 0x170092BB RID: 37563
		// (get) Token: 0x0601A5C2 RID: 107970 RVA: 0x0035EDFF File Offset: 0x0035CFFF
		// (set) Token: 0x0601A5C3 RID: 107971 RVA: 0x0035EE09 File Offset: 0x0035D009
		public BiDi BiDi
		{
			get
			{
				return base.GetElement<BiDi>(17);
			}
			set
			{
				base.SetElement<BiDi>(17, value);
			}
		}

		// Token: 0x170092BC RID: 37564
		// (get) Token: 0x0601A5C4 RID: 107972 RVA: 0x0035EE14 File Offset: 0x0035D014
		// (set) Token: 0x0601A5C5 RID: 107973 RVA: 0x0035EE1E File Offset: 0x0035D01E
		public AdjustRightIndent AdjustRightIndent
		{
			get
			{
				return base.GetElement<AdjustRightIndent>(18);
			}
			set
			{
				base.SetElement<AdjustRightIndent>(18, value);
			}
		}

		// Token: 0x170092BD RID: 37565
		// (get) Token: 0x0601A5C6 RID: 107974 RVA: 0x0035EE29 File Offset: 0x0035D029
		// (set) Token: 0x0601A5C7 RID: 107975 RVA: 0x0035EE33 File Offset: 0x0035D033
		public SnapToGrid SnapToGrid
		{
			get
			{
				return base.GetElement<SnapToGrid>(19);
			}
			set
			{
				base.SetElement<SnapToGrid>(19, value);
			}
		}

		// Token: 0x170092BE RID: 37566
		// (get) Token: 0x0601A5C8 RID: 107976 RVA: 0x0035EE3E File Offset: 0x0035D03E
		// (set) Token: 0x0601A5C9 RID: 107977 RVA: 0x0035EE48 File Offset: 0x0035D048
		public SpacingBetweenLines SpacingBetweenLines
		{
			get
			{
				return base.GetElement<SpacingBetweenLines>(20);
			}
			set
			{
				base.SetElement<SpacingBetweenLines>(20, value);
			}
		}

		// Token: 0x170092BF RID: 37567
		// (get) Token: 0x0601A5CA RID: 107978 RVA: 0x0035EE53 File Offset: 0x0035D053
		// (set) Token: 0x0601A5CB RID: 107979 RVA: 0x0035EE5D File Offset: 0x0035D05D
		public Indentation Indentation
		{
			get
			{
				return base.GetElement<Indentation>(21);
			}
			set
			{
				base.SetElement<Indentation>(21, value);
			}
		}

		// Token: 0x170092C0 RID: 37568
		// (get) Token: 0x0601A5CC RID: 107980 RVA: 0x0035EE68 File Offset: 0x0035D068
		// (set) Token: 0x0601A5CD RID: 107981 RVA: 0x0035EE72 File Offset: 0x0035D072
		public ContextualSpacing ContextualSpacing
		{
			get
			{
				return base.GetElement<ContextualSpacing>(22);
			}
			set
			{
				base.SetElement<ContextualSpacing>(22, value);
			}
		}

		// Token: 0x170092C1 RID: 37569
		// (get) Token: 0x0601A5CE RID: 107982 RVA: 0x0035EE7D File Offset: 0x0035D07D
		// (set) Token: 0x0601A5CF RID: 107983 RVA: 0x0035EE87 File Offset: 0x0035D087
		public MirrorIndents MirrorIndents
		{
			get
			{
				return base.GetElement<MirrorIndents>(23);
			}
			set
			{
				base.SetElement<MirrorIndents>(23, value);
			}
		}

		// Token: 0x170092C2 RID: 37570
		// (get) Token: 0x0601A5D0 RID: 107984 RVA: 0x0035EE92 File Offset: 0x0035D092
		// (set) Token: 0x0601A5D1 RID: 107985 RVA: 0x0035EE9C File Offset: 0x0035D09C
		public SuppressOverlap SuppressOverlap
		{
			get
			{
				return base.GetElement<SuppressOverlap>(24);
			}
			set
			{
				base.SetElement<SuppressOverlap>(24, value);
			}
		}

		// Token: 0x170092C3 RID: 37571
		// (get) Token: 0x0601A5D2 RID: 107986 RVA: 0x0035EEA7 File Offset: 0x0035D0A7
		// (set) Token: 0x0601A5D3 RID: 107987 RVA: 0x0035EEB1 File Offset: 0x0035D0B1
		public Justification Justification
		{
			get
			{
				return base.GetElement<Justification>(25);
			}
			set
			{
				base.SetElement<Justification>(25, value);
			}
		}

		// Token: 0x170092C4 RID: 37572
		// (get) Token: 0x0601A5D4 RID: 107988 RVA: 0x0035EEBC File Offset: 0x0035D0BC
		// (set) Token: 0x0601A5D5 RID: 107989 RVA: 0x0035EEC6 File Offset: 0x0035D0C6
		public TextDirection TextDirection
		{
			get
			{
				return base.GetElement<TextDirection>(26);
			}
			set
			{
				base.SetElement<TextDirection>(26, value);
			}
		}

		// Token: 0x170092C5 RID: 37573
		// (get) Token: 0x0601A5D6 RID: 107990 RVA: 0x0035EED1 File Offset: 0x0035D0D1
		// (set) Token: 0x0601A5D7 RID: 107991 RVA: 0x0035EEDB File Offset: 0x0035D0DB
		public TextAlignment TextAlignment
		{
			get
			{
				return base.GetElement<TextAlignment>(27);
			}
			set
			{
				base.SetElement<TextAlignment>(27, value);
			}
		}

		// Token: 0x170092C6 RID: 37574
		// (get) Token: 0x0601A5D8 RID: 107992 RVA: 0x0035EEE6 File Offset: 0x0035D0E6
		// (set) Token: 0x0601A5D9 RID: 107993 RVA: 0x0035EEF0 File Offset: 0x0035D0F0
		public TextBoxTightWrap TextBoxTightWrap
		{
			get
			{
				return base.GetElement<TextBoxTightWrap>(28);
			}
			set
			{
				base.SetElement<TextBoxTightWrap>(28, value);
			}
		}

		// Token: 0x170092C7 RID: 37575
		// (get) Token: 0x0601A5DA RID: 107994 RVA: 0x0035EEFB File Offset: 0x0035D0FB
		// (set) Token: 0x0601A5DB RID: 107995 RVA: 0x0035EF05 File Offset: 0x0035D105
		public OutlineLevel OutlineLevel
		{
			get
			{
				return base.GetElement<OutlineLevel>(29);
			}
			set
			{
				base.SetElement<OutlineLevel>(29, value);
			}
		}

		// Token: 0x170092C8 RID: 37576
		// (get) Token: 0x0601A5DC RID: 107996 RVA: 0x003612C8 File Offset: 0x0035F4C8
		// (set) Token: 0x0601A5DD RID: 107997 RVA: 0x003612D2 File Offset: 0x0035F4D2
		public ParagraphPropertiesChange ParagraphPropertiesChange
		{
			get
			{
				return base.GetElement<ParagraphPropertiesChange>(30);
			}
			set
			{
				base.SetElement<ParagraphPropertiesChange>(30, value);
			}
		}

		// Token: 0x0601A5DE RID: 107998 RVA: 0x003612DD File Offset: 0x0035F4DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleParagraphProperties>(deep);
		}

		// Token: 0x0400ACBD RID: 44221
		private const string tagName = "pPr";

		// Token: 0x0400ACBE RID: 44222
		private const byte tagNsId = 23;

		// Token: 0x0400ACBF RID: 44223
		internal const int ElementTypeIdConst = 11888;

		// Token: 0x0400ACC0 RID: 44224
		private static readonly string[] eleTagNames = new string[]
		{
			"keepNext", "keepLines", "pageBreakBefore", "framePr", "widowControl", "numPr", "suppressLineNumbers", "pBdr", "shd", "tabs",
			"suppressAutoHyphens", "kinsoku", "wordWrap", "overflowPunct", "topLinePunct", "autoSpaceDE", "autoSpaceDN", "bidi", "adjustRightInd", "snapToGrid",
			"spacing", "ind", "contextualSpacing", "mirrorIndents", "suppressOverlap", "jc", "textDirection", "textAlignment", "textboxTightWrap", "outlineLvl",
			"pPrChange"
		};

		// Token: 0x0400ACC1 RID: 44225
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23
		};
	}
}
