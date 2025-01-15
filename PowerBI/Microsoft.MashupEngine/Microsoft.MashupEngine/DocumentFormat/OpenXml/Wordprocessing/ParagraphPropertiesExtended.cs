using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F24 RID: 12068
	[ChildElementInfo(typeof(DivId))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ParagraphStyleId))]
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
	[ChildElementInfo(typeof(AdjustRightIndent))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(SpacingBetweenLines))]
	[ChildElementInfo(typeof(Indentation))]
	[ChildElementInfo(typeof(ContextualSpacing))]
	[ChildElementInfo(typeof(MirrorIndents))]
	[ChildElementInfo(typeof(SuppressOverlap))]
	[ChildElementInfo(typeof(Justification))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TextAlignment))]
	[ChildElementInfo(typeof(TextBoxTightWrap))]
	[ChildElementInfo(typeof(OutlineLevel))]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	internal class ParagraphPropertiesExtended : OpenXmlCompositeElement
	{
		// Token: 0x17008F00 RID: 36608
		// (get) Token: 0x06019DBD RID: 105917 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x17008F01 RID: 36609
		// (get) Token: 0x06019DBE RID: 105918 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F02 RID: 36610
		// (get) Token: 0x06019DBF RID: 105919 RVA: 0x00357B5F File Offset: 0x00355D5F
		internal override int ElementTypeId
		{
			get
			{
				return 11709;
			}
		}

		// Token: 0x06019DC0 RID: 105920 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019DC1 RID: 105921 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphPropertiesExtended()
		{
		}

		// Token: 0x06019DC2 RID: 105922 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphPropertiesExtended(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019DC3 RID: 105923 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphPropertiesExtended(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019DC4 RID: 105924 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphPropertiesExtended(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019DC5 RID: 105925 RVA: 0x00357B68 File Offset: 0x00355D68
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pStyle" == name)
			{
				return new ParagraphStyleId();
			}
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
			if (23 == namespaceId && "divId" == name)
			{
				return new DivId();
			}
			if (23 == namespaceId && "cnfStyle" == name)
			{
				return new ConditionalFormatStyle();
			}
			return null;
		}

		// Token: 0x17008F03 RID: 36611
		// (get) Token: 0x06019DC6 RID: 105926 RVA: 0x00357E8E File Offset: 0x0035608E
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphPropertiesExtended.eleTagNames;
			}
		}

		// Token: 0x17008F04 RID: 36612
		// (get) Token: 0x06019DC7 RID: 105927 RVA: 0x00357E95 File Offset: 0x00356095
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphPropertiesExtended.eleNamespaceIds;
			}
		}

		// Token: 0x17008F05 RID: 36613
		// (get) Token: 0x06019DC8 RID: 105928 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008F06 RID: 36614
		// (get) Token: 0x06019DC9 RID: 105929 RVA: 0x00357E9C File Offset: 0x0035609C
		// (set) Token: 0x06019DCA RID: 105930 RVA: 0x00357EA5 File Offset: 0x003560A5
		public ParagraphStyleId ParagraphStyleId
		{
			get
			{
				return base.GetElement<ParagraphStyleId>(0);
			}
			set
			{
				base.SetElement<ParagraphStyleId>(0, value);
			}
		}

		// Token: 0x17008F07 RID: 36615
		// (get) Token: 0x06019DCB RID: 105931 RVA: 0x00357EAF File Offset: 0x003560AF
		// (set) Token: 0x06019DCC RID: 105932 RVA: 0x00357EB8 File Offset: 0x003560B8
		public KeepNext KeepNext
		{
			get
			{
				return base.GetElement<KeepNext>(1);
			}
			set
			{
				base.SetElement<KeepNext>(1, value);
			}
		}

		// Token: 0x17008F08 RID: 36616
		// (get) Token: 0x06019DCD RID: 105933 RVA: 0x00357EC2 File Offset: 0x003560C2
		// (set) Token: 0x06019DCE RID: 105934 RVA: 0x00357ECB File Offset: 0x003560CB
		public KeepLines KeepLines
		{
			get
			{
				return base.GetElement<KeepLines>(2);
			}
			set
			{
				base.SetElement<KeepLines>(2, value);
			}
		}

		// Token: 0x17008F09 RID: 36617
		// (get) Token: 0x06019DCF RID: 105935 RVA: 0x00357ED5 File Offset: 0x003560D5
		// (set) Token: 0x06019DD0 RID: 105936 RVA: 0x00357EDE File Offset: 0x003560DE
		public PageBreakBefore PageBreakBefore
		{
			get
			{
				return base.GetElement<PageBreakBefore>(3);
			}
			set
			{
				base.SetElement<PageBreakBefore>(3, value);
			}
		}

		// Token: 0x17008F0A RID: 36618
		// (get) Token: 0x06019DD1 RID: 105937 RVA: 0x00357EE8 File Offset: 0x003560E8
		// (set) Token: 0x06019DD2 RID: 105938 RVA: 0x00357EF1 File Offset: 0x003560F1
		public FrameProperties FrameProperties
		{
			get
			{
				return base.GetElement<FrameProperties>(4);
			}
			set
			{
				base.SetElement<FrameProperties>(4, value);
			}
		}

		// Token: 0x17008F0B RID: 36619
		// (get) Token: 0x06019DD3 RID: 105939 RVA: 0x00357EFB File Offset: 0x003560FB
		// (set) Token: 0x06019DD4 RID: 105940 RVA: 0x00357F04 File Offset: 0x00356104
		public WidowControl WidowControl
		{
			get
			{
				return base.GetElement<WidowControl>(5);
			}
			set
			{
				base.SetElement<WidowControl>(5, value);
			}
		}

		// Token: 0x17008F0C RID: 36620
		// (get) Token: 0x06019DD5 RID: 105941 RVA: 0x00357F0E File Offset: 0x0035610E
		// (set) Token: 0x06019DD6 RID: 105942 RVA: 0x00357F17 File Offset: 0x00356117
		public NumberingProperties NumberingProperties
		{
			get
			{
				return base.GetElement<NumberingProperties>(6);
			}
			set
			{
				base.SetElement<NumberingProperties>(6, value);
			}
		}

		// Token: 0x17008F0D RID: 36621
		// (get) Token: 0x06019DD7 RID: 105943 RVA: 0x00357F21 File Offset: 0x00356121
		// (set) Token: 0x06019DD8 RID: 105944 RVA: 0x00357F2A File Offset: 0x0035612A
		public SuppressLineNumbers SuppressLineNumbers
		{
			get
			{
				return base.GetElement<SuppressLineNumbers>(7);
			}
			set
			{
				base.SetElement<SuppressLineNumbers>(7, value);
			}
		}

		// Token: 0x17008F0E RID: 36622
		// (get) Token: 0x06019DD9 RID: 105945 RVA: 0x00357F34 File Offset: 0x00356134
		// (set) Token: 0x06019DDA RID: 105946 RVA: 0x00357F3D File Offset: 0x0035613D
		public ParagraphBorders ParagraphBorders
		{
			get
			{
				return base.GetElement<ParagraphBorders>(8);
			}
			set
			{
				base.SetElement<ParagraphBorders>(8, value);
			}
		}

		// Token: 0x17008F0F RID: 36623
		// (get) Token: 0x06019DDB RID: 105947 RVA: 0x0035750F File Offset: 0x0035570F
		// (set) Token: 0x06019DDC RID: 105948 RVA: 0x00357519 File Offset: 0x00355719
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(9);
			}
			set
			{
				base.SetElement<Shading>(9, value);
			}
		}

		// Token: 0x17008F10 RID: 36624
		// (get) Token: 0x06019DDD RID: 105949 RVA: 0x00357F47 File Offset: 0x00356147
		// (set) Token: 0x06019DDE RID: 105950 RVA: 0x00357F51 File Offset: 0x00356151
		public Tabs Tabs
		{
			get
			{
				return base.GetElement<Tabs>(10);
			}
			set
			{
				base.SetElement<Tabs>(10, value);
			}
		}

		// Token: 0x17008F11 RID: 36625
		// (get) Token: 0x06019DDF RID: 105951 RVA: 0x00357F5C File Offset: 0x0035615C
		// (set) Token: 0x06019DE0 RID: 105952 RVA: 0x00357F66 File Offset: 0x00356166
		public SuppressAutoHyphens SuppressAutoHyphens
		{
			get
			{
				return base.GetElement<SuppressAutoHyphens>(11);
			}
			set
			{
				base.SetElement<SuppressAutoHyphens>(11, value);
			}
		}

		// Token: 0x17008F12 RID: 36626
		// (get) Token: 0x06019DE1 RID: 105953 RVA: 0x00357F71 File Offset: 0x00356171
		// (set) Token: 0x06019DE2 RID: 105954 RVA: 0x00357F7B File Offset: 0x0035617B
		public Kinsoku Kinsoku
		{
			get
			{
				return base.GetElement<Kinsoku>(12);
			}
			set
			{
				base.SetElement<Kinsoku>(12, value);
			}
		}

		// Token: 0x17008F13 RID: 36627
		// (get) Token: 0x06019DE3 RID: 105955 RVA: 0x00357F86 File Offset: 0x00356186
		// (set) Token: 0x06019DE4 RID: 105956 RVA: 0x00357F90 File Offset: 0x00356190
		public WordWrap WordWrap
		{
			get
			{
				return base.GetElement<WordWrap>(13);
			}
			set
			{
				base.SetElement<WordWrap>(13, value);
			}
		}

		// Token: 0x17008F14 RID: 36628
		// (get) Token: 0x06019DE5 RID: 105957 RVA: 0x00357F9B File Offset: 0x0035619B
		// (set) Token: 0x06019DE6 RID: 105958 RVA: 0x00357FA5 File Offset: 0x003561A5
		public OverflowPunctuation OverflowPunctuation
		{
			get
			{
				return base.GetElement<OverflowPunctuation>(14);
			}
			set
			{
				base.SetElement<OverflowPunctuation>(14, value);
			}
		}

		// Token: 0x17008F15 RID: 36629
		// (get) Token: 0x06019DE7 RID: 105959 RVA: 0x00357FB0 File Offset: 0x003561B0
		// (set) Token: 0x06019DE8 RID: 105960 RVA: 0x00357FBA File Offset: 0x003561BA
		public TopLinePunctuation TopLinePunctuation
		{
			get
			{
				return base.GetElement<TopLinePunctuation>(15);
			}
			set
			{
				base.SetElement<TopLinePunctuation>(15, value);
			}
		}

		// Token: 0x17008F16 RID: 36630
		// (get) Token: 0x06019DE9 RID: 105961 RVA: 0x00357FC5 File Offset: 0x003561C5
		// (set) Token: 0x06019DEA RID: 105962 RVA: 0x00357FCF File Offset: 0x003561CF
		public AutoSpaceDE AutoSpaceDE
		{
			get
			{
				return base.GetElement<AutoSpaceDE>(16);
			}
			set
			{
				base.SetElement<AutoSpaceDE>(16, value);
			}
		}

		// Token: 0x17008F17 RID: 36631
		// (get) Token: 0x06019DEB RID: 105963 RVA: 0x00357FDA File Offset: 0x003561DA
		// (set) Token: 0x06019DEC RID: 105964 RVA: 0x00357FE4 File Offset: 0x003561E4
		public AutoSpaceDN AutoSpaceDN
		{
			get
			{
				return base.GetElement<AutoSpaceDN>(17);
			}
			set
			{
				base.SetElement<AutoSpaceDN>(17, value);
			}
		}

		// Token: 0x17008F18 RID: 36632
		// (get) Token: 0x06019DED RID: 105965 RVA: 0x00357FEF File Offset: 0x003561EF
		// (set) Token: 0x06019DEE RID: 105966 RVA: 0x00357FF9 File Offset: 0x003561F9
		public BiDi BiDi
		{
			get
			{
				return base.GetElement<BiDi>(18);
			}
			set
			{
				base.SetElement<BiDi>(18, value);
			}
		}

		// Token: 0x17008F19 RID: 36633
		// (get) Token: 0x06019DEF RID: 105967 RVA: 0x00358004 File Offset: 0x00356204
		// (set) Token: 0x06019DF0 RID: 105968 RVA: 0x0035800E File Offset: 0x0035620E
		public AdjustRightIndent AdjustRightIndent
		{
			get
			{
				return base.GetElement<AdjustRightIndent>(19);
			}
			set
			{
				base.SetElement<AdjustRightIndent>(19, value);
			}
		}

		// Token: 0x17008F1A RID: 36634
		// (get) Token: 0x06019DF1 RID: 105969 RVA: 0x00358019 File Offset: 0x00356219
		// (set) Token: 0x06019DF2 RID: 105970 RVA: 0x00358023 File Offset: 0x00356223
		public SnapToGrid SnapToGrid
		{
			get
			{
				return base.GetElement<SnapToGrid>(20);
			}
			set
			{
				base.SetElement<SnapToGrid>(20, value);
			}
		}

		// Token: 0x17008F1B RID: 36635
		// (get) Token: 0x06019DF3 RID: 105971 RVA: 0x0035802E File Offset: 0x0035622E
		// (set) Token: 0x06019DF4 RID: 105972 RVA: 0x00358038 File Offset: 0x00356238
		public SpacingBetweenLines SpacingBetweenLines
		{
			get
			{
				return base.GetElement<SpacingBetweenLines>(21);
			}
			set
			{
				base.SetElement<SpacingBetweenLines>(21, value);
			}
		}

		// Token: 0x17008F1C RID: 36636
		// (get) Token: 0x06019DF5 RID: 105973 RVA: 0x00358043 File Offset: 0x00356243
		// (set) Token: 0x06019DF6 RID: 105974 RVA: 0x0035804D File Offset: 0x0035624D
		public Indentation Indentation
		{
			get
			{
				return base.GetElement<Indentation>(22);
			}
			set
			{
				base.SetElement<Indentation>(22, value);
			}
		}

		// Token: 0x17008F1D RID: 36637
		// (get) Token: 0x06019DF7 RID: 105975 RVA: 0x00358058 File Offset: 0x00356258
		// (set) Token: 0x06019DF8 RID: 105976 RVA: 0x00358062 File Offset: 0x00356262
		public ContextualSpacing ContextualSpacing
		{
			get
			{
				return base.GetElement<ContextualSpacing>(23);
			}
			set
			{
				base.SetElement<ContextualSpacing>(23, value);
			}
		}

		// Token: 0x17008F1E RID: 36638
		// (get) Token: 0x06019DF9 RID: 105977 RVA: 0x0035806D File Offset: 0x0035626D
		// (set) Token: 0x06019DFA RID: 105978 RVA: 0x00358077 File Offset: 0x00356277
		public MirrorIndents MirrorIndents
		{
			get
			{
				return base.GetElement<MirrorIndents>(24);
			}
			set
			{
				base.SetElement<MirrorIndents>(24, value);
			}
		}

		// Token: 0x17008F1F RID: 36639
		// (get) Token: 0x06019DFB RID: 105979 RVA: 0x00358082 File Offset: 0x00356282
		// (set) Token: 0x06019DFC RID: 105980 RVA: 0x0035808C File Offset: 0x0035628C
		public SuppressOverlap SuppressOverlap
		{
			get
			{
				return base.GetElement<SuppressOverlap>(25);
			}
			set
			{
				base.SetElement<SuppressOverlap>(25, value);
			}
		}

		// Token: 0x17008F20 RID: 36640
		// (get) Token: 0x06019DFD RID: 105981 RVA: 0x00358097 File Offset: 0x00356297
		// (set) Token: 0x06019DFE RID: 105982 RVA: 0x003580A1 File Offset: 0x003562A1
		public Justification Justification
		{
			get
			{
				return base.GetElement<Justification>(26);
			}
			set
			{
				base.SetElement<Justification>(26, value);
			}
		}

		// Token: 0x17008F21 RID: 36641
		// (get) Token: 0x06019DFF RID: 105983 RVA: 0x003580AC File Offset: 0x003562AC
		// (set) Token: 0x06019E00 RID: 105984 RVA: 0x003580B6 File Offset: 0x003562B6
		public TextDirection TextDirection
		{
			get
			{
				return base.GetElement<TextDirection>(27);
			}
			set
			{
				base.SetElement<TextDirection>(27, value);
			}
		}

		// Token: 0x17008F22 RID: 36642
		// (get) Token: 0x06019E01 RID: 105985 RVA: 0x003580C1 File Offset: 0x003562C1
		// (set) Token: 0x06019E02 RID: 105986 RVA: 0x003580CB File Offset: 0x003562CB
		public TextAlignment TextAlignment
		{
			get
			{
				return base.GetElement<TextAlignment>(28);
			}
			set
			{
				base.SetElement<TextAlignment>(28, value);
			}
		}

		// Token: 0x17008F23 RID: 36643
		// (get) Token: 0x06019E03 RID: 105987 RVA: 0x003580D6 File Offset: 0x003562D6
		// (set) Token: 0x06019E04 RID: 105988 RVA: 0x003580E0 File Offset: 0x003562E0
		public TextBoxTightWrap TextBoxTightWrap
		{
			get
			{
				return base.GetElement<TextBoxTightWrap>(29);
			}
			set
			{
				base.SetElement<TextBoxTightWrap>(29, value);
			}
		}

		// Token: 0x17008F24 RID: 36644
		// (get) Token: 0x06019E05 RID: 105989 RVA: 0x003580EB File Offset: 0x003562EB
		// (set) Token: 0x06019E06 RID: 105990 RVA: 0x003580F5 File Offset: 0x003562F5
		public OutlineLevel OutlineLevel
		{
			get
			{
				return base.GetElement<OutlineLevel>(30);
			}
			set
			{
				base.SetElement<OutlineLevel>(30, value);
			}
		}

		// Token: 0x17008F25 RID: 36645
		// (get) Token: 0x06019E07 RID: 105991 RVA: 0x00358100 File Offset: 0x00356300
		// (set) Token: 0x06019E08 RID: 105992 RVA: 0x0035810A File Offset: 0x0035630A
		public DivId DivId
		{
			get
			{
				return base.GetElement<DivId>(31);
			}
			set
			{
				base.SetElement<DivId>(31, value);
			}
		}

		// Token: 0x17008F26 RID: 36646
		// (get) Token: 0x06019E09 RID: 105993 RVA: 0x00358115 File Offset: 0x00356315
		// (set) Token: 0x06019E0A RID: 105994 RVA: 0x0035811F File Offset: 0x0035631F
		public ConditionalFormatStyle ConditionalFormatStyle
		{
			get
			{
				return base.GetElement<ConditionalFormatStyle>(32);
			}
			set
			{
				base.SetElement<ConditionalFormatStyle>(32, value);
			}
		}

		// Token: 0x06019E0B RID: 105995 RVA: 0x0035812A File Offset: 0x0035632A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphPropertiesExtended>(deep);
		}

		// Token: 0x0400AAA9 RID: 43689
		private const string tagName = "pPr";

		// Token: 0x0400AAAA RID: 43690
		private const byte tagNsId = 23;

		// Token: 0x0400AAAB RID: 43691
		internal const int ElementTypeIdConst = 11709;

		// Token: 0x0400AAAC RID: 43692
		private static readonly string[] eleTagNames = new string[]
		{
			"pStyle", "keepNext", "keepLines", "pageBreakBefore", "framePr", "widowControl", "numPr", "suppressLineNumbers", "pBdr", "shd",
			"tabs", "suppressAutoHyphens", "kinsoku", "wordWrap", "overflowPunct", "topLinePunct", "autoSpaceDE", "autoSpaceDN", "bidi", "adjustRightInd",
			"snapToGrid", "spacing", "ind", "contextualSpacing", "mirrorIndents", "suppressOverlap", "jc", "textDirection", "textAlignment", "textboxTightWrap",
			"outlineLvl", "divId", "cnfStyle"
		};

		// Token: 0x0400AAAD RID: 43693
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23
		};
	}
}
