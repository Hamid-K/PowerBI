using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003003 RID: 12291
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(SpacingBetweenLines))]
	[ChildElementInfo(typeof(MirrorIndents))]
	[ChildElementInfo(typeof(SuppressOverlap))]
	[ChildElementInfo(typeof(ContextualSpacing))]
	[ChildElementInfo(typeof(AdjustRightIndent))]
	[ChildElementInfo(typeof(AutoSpaceDE))]
	[ChildElementInfo(typeof(AutoSpaceDN))]
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
	[ChildElementInfo(typeof(Tabs))]
	[ChildElementInfo(typeof(SuppressAutoHyphens))]
	[ChildElementInfo(typeof(Kinsoku))]
	[ChildElementInfo(typeof(WordWrap))]
	[ChildElementInfo(typeof(OverflowPunctuation))]
	[ChildElementInfo(typeof(TopLinePunctuation))]
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(Indentation))]
	[ChildElementInfo(typeof(Justification))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TextAlignment))]
	[ChildElementInfo(typeof(TextBoxTightWrap))]
	[ChildElementInfo(typeof(OutlineLevel))]
	[ChildElementInfo(typeof(DivId))]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	[ChildElementInfo(typeof(ParagraphMarkRunProperties))]
	[ChildElementInfo(typeof(SectionProperties))]
	[ChildElementInfo(typeof(ParagraphPropertiesChange))]
	internal class ParagraphProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009600 RID: 38400
		// (get) Token: 0x0601ACC2 RID: 109762 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x17009601 RID: 38401
		// (get) Token: 0x0601ACC3 RID: 109763 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009602 RID: 38402
		// (get) Token: 0x0601ACC4 RID: 109764 RVA: 0x00367C2B File Offset: 0x00365E2B
		internal override int ElementTypeId
		{
			get
			{
				return 12138;
			}
		}

		// Token: 0x0601ACC5 RID: 109765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ACC6 RID: 109766 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphProperties()
		{
		}

		// Token: 0x0601ACC7 RID: 109767 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACC8 RID: 109768 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACC9 RID: 109769 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ACCA RID: 109770 RVA: 0x00367C34 File Offset: 0x00365E34
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
			if (23 == namespaceId && "rPr" == name)
			{
				return new ParagraphMarkRunProperties();
			}
			if (23 == namespaceId && "sectPr" == name)
			{
				return new SectionProperties();
			}
			if (23 == namespaceId && "pPrChange" == name)
			{
				return new ParagraphPropertiesChange();
			}
			return null;
		}

		// Token: 0x17009603 RID: 38403
		// (get) Token: 0x0601ACCB RID: 109771 RVA: 0x00367FA2 File Offset: 0x003661A2
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphProperties.eleTagNames;
			}
		}

		// Token: 0x17009604 RID: 38404
		// (get) Token: 0x0601ACCC RID: 109772 RVA: 0x00367FA9 File Offset: 0x003661A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009605 RID: 38405
		// (get) Token: 0x0601ACCD RID: 109773 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009606 RID: 38406
		// (get) Token: 0x0601ACCE RID: 109774 RVA: 0x00357E9C File Offset: 0x0035609C
		// (set) Token: 0x0601ACCF RID: 109775 RVA: 0x00357EA5 File Offset: 0x003560A5
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

		// Token: 0x17009607 RID: 38407
		// (get) Token: 0x0601ACD0 RID: 109776 RVA: 0x00357EAF File Offset: 0x003560AF
		// (set) Token: 0x0601ACD1 RID: 109777 RVA: 0x00357EB8 File Offset: 0x003560B8
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

		// Token: 0x17009608 RID: 38408
		// (get) Token: 0x0601ACD2 RID: 109778 RVA: 0x00357EC2 File Offset: 0x003560C2
		// (set) Token: 0x0601ACD3 RID: 109779 RVA: 0x00357ECB File Offset: 0x003560CB
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

		// Token: 0x17009609 RID: 38409
		// (get) Token: 0x0601ACD4 RID: 109780 RVA: 0x00357ED5 File Offset: 0x003560D5
		// (set) Token: 0x0601ACD5 RID: 109781 RVA: 0x00357EDE File Offset: 0x003560DE
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

		// Token: 0x1700960A RID: 38410
		// (get) Token: 0x0601ACD6 RID: 109782 RVA: 0x00357EE8 File Offset: 0x003560E8
		// (set) Token: 0x0601ACD7 RID: 109783 RVA: 0x00357EF1 File Offset: 0x003560F1
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

		// Token: 0x1700960B RID: 38411
		// (get) Token: 0x0601ACD8 RID: 109784 RVA: 0x00357EFB File Offset: 0x003560FB
		// (set) Token: 0x0601ACD9 RID: 109785 RVA: 0x00357F04 File Offset: 0x00356104
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

		// Token: 0x1700960C RID: 38412
		// (get) Token: 0x0601ACDA RID: 109786 RVA: 0x00357F0E File Offset: 0x0035610E
		// (set) Token: 0x0601ACDB RID: 109787 RVA: 0x00357F17 File Offset: 0x00356117
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

		// Token: 0x1700960D RID: 38413
		// (get) Token: 0x0601ACDC RID: 109788 RVA: 0x00357F21 File Offset: 0x00356121
		// (set) Token: 0x0601ACDD RID: 109789 RVA: 0x00357F2A File Offset: 0x0035612A
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

		// Token: 0x1700960E RID: 38414
		// (get) Token: 0x0601ACDE RID: 109790 RVA: 0x00357F34 File Offset: 0x00356134
		// (set) Token: 0x0601ACDF RID: 109791 RVA: 0x00357F3D File Offset: 0x0035613D
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

		// Token: 0x1700960F RID: 38415
		// (get) Token: 0x0601ACE0 RID: 109792 RVA: 0x0035750F File Offset: 0x0035570F
		// (set) Token: 0x0601ACE1 RID: 109793 RVA: 0x00357519 File Offset: 0x00355719
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

		// Token: 0x17009610 RID: 38416
		// (get) Token: 0x0601ACE2 RID: 109794 RVA: 0x00357F47 File Offset: 0x00356147
		// (set) Token: 0x0601ACE3 RID: 109795 RVA: 0x00357F51 File Offset: 0x00356151
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

		// Token: 0x17009611 RID: 38417
		// (get) Token: 0x0601ACE4 RID: 109796 RVA: 0x00357F5C File Offset: 0x0035615C
		// (set) Token: 0x0601ACE5 RID: 109797 RVA: 0x00357F66 File Offset: 0x00356166
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

		// Token: 0x17009612 RID: 38418
		// (get) Token: 0x0601ACE6 RID: 109798 RVA: 0x00357F71 File Offset: 0x00356171
		// (set) Token: 0x0601ACE7 RID: 109799 RVA: 0x00357F7B File Offset: 0x0035617B
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

		// Token: 0x17009613 RID: 38419
		// (get) Token: 0x0601ACE8 RID: 109800 RVA: 0x00357F86 File Offset: 0x00356186
		// (set) Token: 0x0601ACE9 RID: 109801 RVA: 0x00357F90 File Offset: 0x00356190
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

		// Token: 0x17009614 RID: 38420
		// (get) Token: 0x0601ACEA RID: 109802 RVA: 0x00357F9B File Offset: 0x0035619B
		// (set) Token: 0x0601ACEB RID: 109803 RVA: 0x00357FA5 File Offset: 0x003561A5
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

		// Token: 0x17009615 RID: 38421
		// (get) Token: 0x0601ACEC RID: 109804 RVA: 0x00357FB0 File Offset: 0x003561B0
		// (set) Token: 0x0601ACED RID: 109805 RVA: 0x00357FBA File Offset: 0x003561BA
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

		// Token: 0x17009616 RID: 38422
		// (get) Token: 0x0601ACEE RID: 109806 RVA: 0x00357FC5 File Offset: 0x003561C5
		// (set) Token: 0x0601ACEF RID: 109807 RVA: 0x00357FCF File Offset: 0x003561CF
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

		// Token: 0x17009617 RID: 38423
		// (get) Token: 0x0601ACF0 RID: 109808 RVA: 0x00357FDA File Offset: 0x003561DA
		// (set) Token: 0x0601ACF1 RID: 109809 RVA: 0x00357FE4 File Offset: 0x003561E4
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

		// Token: 0x17009618 RID: 38424
		// (get) Token: 0x0601ACF2 RID: 109810 RVA: 0x00357FEF File Offset: 0x003561EF
		// (set) Token: 0x0601ACF3 RID: 109811 RVA: 0x00357FF9 File Offset: 0x003561F9
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

		// Token: 0x17009619 RID: 38425
		// (get) Token: 0x0601ACF4 RID: 109812 RVA: 0x00358004 File Offset: 0x00356204
		// (set) Token: 0x0601ACF5 RID: 109813 RVA: 0x0035800E File Offset: 0x0035620E
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

		// Token: 0x1700961A RID: 38426
		// (get) Token: 0x0601ACF6 RID: 109814 RVA: 0x00358019 File Offset: 0x00356219
		// (set) Token: 0x0601ACF7 RID: 109815 RVA: 0x00358023 File Offset: 0x00356223
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

		// Token: 0x1700961B RID: 38427
		// (get) Token: 0x0601ACF8 RID: 109816 RVA: 0x0035802E File Offset: 0x0035622E
		// (set) Token: 0x0601ACF9 RID: 109817 RVA: 0x00358038 File Offset: 0x00356238
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

		// Token: 0x1700961C RID: 38428
		// (get) Token: 0x0601ACFA RID: 109818 RVA: 0x00358043 File Offset: 0x00356243
		// (set) Token: 0x0601ACFB RID: 109819 RVA: 0x0035804D File Offset: 0x0035624D
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

		// Token: 0x1700961D RID: 38429
		// (get) Token: 0x0601ACFC RID: 109820 RVA: 0x00358058 File Offset: 0x00356258
		// (set) Token: 0x0601ACFD RID: 109821 RVA: 0x00358062 File Offset: 0x00356262
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

		// Token: 0x1700961E RID: 38430
		// (get) Token: 0x0601ACFE RID: 109822 RVA: 0x0035806D File Offset: 0x0035626D
		// (set) Token: 0x0601ACFF RID: 109823 RVA: 0x00358077 File Offset: 0x00356277
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

		// Token: 0x1700961F RID: 38431
		// (get) Token: 0x0601AD00 RID: 109824 RVA: 0x00358082 File Offset: 0x00356282
		// (set) Token: 0x0601AD01 RID: 109825 RVA: 0x0035808C File Offset: 0x0035628C
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

		// Token: 0x17009620 RID: 38432
		// (get) Token: 0x0601AD02 RID: 109826 RVA: 0x00358097 File Offset: 0x00356297
		// (set) Token: 0x0601AD03 RID: 109827 RVA: 0x003580A1 File Offset: 0x003562A1
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

		// Token: 0x17009621 RID: 38433
		// (get) Token: 0x0601AD04 RID: 109828 RVA: 0x003580AC File Offset: 0x003562AC
		// (set) Token: 0x0601AD05 RID: 109829 RVA: 0x003580B6 File Offset: 0x003562B6
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

		// Token: 0x17009622 RID: 38434
		// (get) Token: 0x0601AD06 RID: 109830 RVA: 0x003580C1 File Offset: 0x003562C1
		// (set) Token: 0x0601AD07 RID: 109831 RVA: 0x003580CB File Offset: 0x003562CB
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

		// Token: 0x17009623 RID: 38435
		// (get) Token: 0x0601AD08 RID: 109832 RVA: 0x003580D6 File Offset: 0x003562D6
		// (set) Token: 0x0601AD09 RID: 109833 RVA: 0x003580E0 File Offset: 0x003562E0
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

		// Token: 0x17009624 RID: 38436
		// (get) Token: 0x0601AD0A RID: 109834 RVA: 0x003580EB File Offset: 0x003562EB
		// (set) Token: 0x0601AD0B RID: 109835 RVA: 0x003580F5 File Offset: 0x003562F5
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

		// Token: 0x17009625 RID: 38437
		// (get) Token: 0x0601AD0C RID: 109836 RVA: 0x00358100 File Offset: 0x00356300
		// (set) Token: 0x0601AD0D RID: 109837 RVA: 0x0035810A File Offset: 0x0035630A
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

		// Token: 0x17009626 RID: 38438
		// (get) Token: 0x0601AD0E RID: 109838 RVA: 0x00358115 File Offset: 0x00356315
		// (set) Token: 0x0601AD0F RID: 109839 RVA: 0x0035811F File Offset: 0x0035631F
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

		// Token: 0x17009627 RID: 38439
		// (get) Token: 0x0601AD10 RID: 109840 RVA: 0x00367FB0 File Offset: 0x003661B0
		// (set) Token: 0x0601AD11 RID: 109841 RVA: 0x00367FBA File Offset: 0x003661BA
		public ParagraphMarkRunProperties ParagraphMarkRunProperties
		{
			get
			{
				return base.GetElement<ParagraphMarkRunProperties>(33);
			}
			set
			{
				base.SetElement<ParagraphMarkRunProperties>(33, value);
			}
		}

		// Token: 0x17009628 RID: 38440
		// (get) Token: 0x0601AD12 RID: 109842 RVA: 0x00367FC5 File Offset: 0x003661C5
		// (set) Token: 0x0601AD13 RID: 109843 RVA: 0x00367FCF File Offset: 0x003661CF
		public SectionProperties SectionProperties
		{
			get
			{
				return base.GetElement<SectionProperties>(34);
			}
			set
			{
				base.SetElement<SectionProperties>(34, value);
			}
		}

		// Token: 0x17009629 RID: 38441
		// (get) Token: 0x0601AD14 RID: 109844 RVA: 0x00367FDA File Offset: 0x003661DA
		// (set) Token: 0x0601AD15 RID: 109845 RVA: 0x00367FE4 File Offset: 0x003661E4
		public ParagraphPropertiesChange ParagraphPropertiesChange
		{
			get
			{
				return base.GetElement<ParagraphPropertiesChange>(35);
			}
			set
			{
				base.SetElement<ParagraphPropertiesChange>(35, value);
			}
		}

		// Token: 0x0601AD16 RID: 109846 RVA: 0x00367FEF File Offset: 0x003661EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphProperties>(deep);
		}

		// Token: 0x0400AE5C RID: 44636
		private const string tagName = "pPr";

		// Token: 0x0400AE5D RID: 44637
		private const byte tagNsId = 23;

		// Token: 0x0400AE5E RID: 44638
		internal const int ElementTypeIdConst = 12138;

		// Token: 0x0400AE5F RID: 44639
		private static readonly string[] eleTagNames = new string[]
		{
			"pStyle", "keepNext", "keepLines", "pageBreakBefore", "framePr", "widowControl", "numPr", "suppressLineNumbers", "pBdr", "shd",
			"tabs", "suppressAutoHyphens", "kinsoku", "wordWrap", "overflowPunct", "topLinePunct", "autoSpaceDE", "autoSpaceDN", "bidi", "adjustRightInd",
			"snapToGrid", "spacing", "ind", "contextualSpacing", "mirrorIndents", "suppressOverlap", "jc", "textDirection", "textAlignment", "textboxTightWrap",
			"outlineLvl", "divId", "cnfStyle", "rPr", "sectPr", "pPrChange"
		};

		// Token: 0x0400AE60 RID: 44640
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
