using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9A RID: 12186
	[ChildElementInfo(typeof(WidowControl))]
	[ChildElementInfo(typeof(OverflowPunctuation))]
	[ChildElementInfo(typeof(ParagraphStyleId))]
	[ChildElementInfo(typeof(KeepNext))]
	[ChildElementInfo(typeof(KeepLines))]
	[ChildElementInfo(typeof(PageBreakBefore))]
	[ChildElementInfo(typeof(FrameProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingProperties))]
	[ChildElementInfo(typeof(SuppressLineNumbers))]
	[ChildElementInfo(typeof(ParagraphBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(Tabs))]
	[ChildElementInfo(typeof(SuppressAutoHyphens))]
	[ChildElementInfo(typeof(Kinsoku))]
	[ChildElementInfo(typeof(WordWrap))]
	[ChildElementInfo(typeof(Indentation))]
	[ChildElementInfo(typeof(OutlineLevel))]
	[ChildElementInfo(typeof(AutoSpaceDE))]
	[ChildElementInfo(typeof(AutoSpaceDN))]
	[ChildElementInfo(typeof(BiDi))]
	[ChildElementInfo(typeof(AdjustRightIndent))]
	[ChildElementInfo(typeof(SnapToGrid))]
	[ChildElementInfo(typeof(SpacingBetweenLines))]
	[ChildElementInfo(typeof(TopLinePunctuation))]
	[ChildElementInfo(typeof(ContextualSpacing))]
	[ChildElementInfo(typeof(MirrorIndents))]
	[ChildElementInfo(typeof(SuppressOverlap))]
	[ChildElementInfo(typeof(Justification))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TextAlignment))]
	[ChildElementInfo(typeof(TextBoxTightWrap))]
	internal class PreviousParagraphProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700920A RID: 37386
		// (get) Token: 0x0601A457 RID: 107607 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x1700920B RID: 37387
		// (get) Token: 0x0601A458 RID: 107608 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700920C RID: 37388
		// (get) Token: 0x0601A459 RID: 107609 RVA: 0x0035FCF8 File Offset: 0x0035DEF8
		internal override int ElementTypeId
		{
			get
			{
				return 11872;
			}
		}

		// Token: 0x0601A45A RID: 107610 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A45B RID: 107611 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousParagraphProperties()
		{
		}

		// Token: 0x0601A45C RID: 107612 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A45D RID: 107613 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A45E RID: 107614 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A45F RID: 107615 RVA: 0x0035FD00 File Offset: 0x0035DF00
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
			return null;
		}

		// Token: 0x1700920D RID: 37389
		// (get) Token: 0x0601A460 RID: 107616 RVA: 0x0035FFF6 File Offset: 0x0035E1F6
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousParagraphProperties.eleTagNames;
			}
		}

		// Token: 0x1700920E RID: 37390
		// (get) Token: 0x0601A461 RID: 107617 RVA: 0x0035FFFD File Offset: 0x0035E1FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousParagraphProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700920F RID: 37391
		// (get) Token: 0x0601A462 RID: 107618 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009210 RID: 37392
		// (get) Token: 0x0601A463 RID: 107619 RVA: 0x00357E9C File Offset: 0x0035609C
		// (set) Token: 0x0601A464 RID: 107620 RVA: 0x00357EA5 File Offset: 0x003560A5
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

		// Token: 0x17009211 RID: 37393
		// (get) Token: 0x0601A465 RID: 107621 RVA: 0x00357EAF File Offset: 0x003560AF
		// (set) Token: 0x0601A466 RID: 107622 RVA: 0x00357EB8 File Offset: 0x003560B8
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

		// Token: 0x17009212 RID: 37394
		// (get) Token: 0x0601A467 RID: 107623 RVA: 0x00357EC2 File Offset: 0x003560C2
		// (set) Token: 0x0601A468 RID: 107624 RVA: 0x00357ECB File Offset: 0x003560CB
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

		// Token: 0x17009213 RID: 37395
		// (get) Token: 0x0601A469 RID: 107625 RVA: 0x00357ED5 File Offset: 0x003560D5
		// (set) Token: 0x0601A46A RID: 107626 RVA: 0x00357EDE File Offset: 0x003560DE
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

		// Token: 0x17009214 RID: 37396
		// (get) Token: 0x0601A46B RID: 107627 RVA: 0x00357EE8 File Offset: 0x003560E8
		// (set) Token: 0x0601A46C RID: 107628 RVA: 0x00357EF1 File Offset: 0x003560F1
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

		// Token: 0x17009215 RID: 37397
		// (get) Token: 0x0601A46D RID: 107629 RVA: 0x00357EFB File Offset: 0x003560FB
		// (set) Token: 0x0601A46E RID: 107630 RVA: 0x00357F04 File Offset: 0x00356104
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

		// Token: 0x17009216 RID: 37398
		// (get) Token: 0x0601A46F RID: 107631 RVA: 0x00357F0E File Offset: 0x0035610E
		// (set) Token: 0x0601A470 RID: 107632 RVA: 0x00357F17 File Offset: 0x00356117
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

		// Token: 0x17009217 RID: 37399
		// (get) Token: 0x0601A471 RID: 107633 RVA: 0x00357F21 File Offset: 0x00356121
		// (set) Token: 0x0601A472 RID: 107634 RVA: 0x00357F2A File Offset: 0x0035612A
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

		// Token: 0x17009218 RID: 37400
		// (get) Token: 0x0601A473 RID: 107635 RVA: 0x00357F34 File Offset: 0x00356134
		// (set) Token: 0x0601A474 RID: 107636 RVA: 0x00357F3D File Offset: 0x0035613D
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

		// Token: 0x17009219 RID: 37401
		// (get) Token: 0x0601A475 RID: 107637 RVA: 0x0035750F File Offset: 0x0035570F
		// (set) Token: 0x0601A476 RID: 107638 RVA: 0x00357519 File Offset: 0x00355719
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

		// Token: 0x1700921A RID: 37402
		// (get) Token: 0x0601A477 RID: 107639 RVA: 0x00357F47 File Offset: 0x00356147
		// (set) Token: 0x0601A478 RID: 107640 RVA: 0x00357F51 File Offset: 0x00356151
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

		// Token: 0x1700921B RID: 37403
		// (get) Token: 0x0601A479 RID: 107641 RVA: 0x00357F5C File Offset: 0x0035615C
		// (set) Token: 0x0601A47A RID: 107642 RVA: 0x00357F66 File Offset: 0x00356166
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

		// Token: 0x1700921C RID: 37404
		// (get) Token: 0x0601A47B RID: 107643 RVA: 0x00357F71 File Offset: 0x00356171
		// (set) Token: 0x0601A47C RID: 107644 RVA: 0x00357F7B File Offset: 0x0035617B
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

		// Token: 0x1700921D RID: 37405
		// (get) Token: 0x0601A47D RID: 107645 RVA: 0x00357F86 File Offset: 0x00356186
		// (set) Token: 0x0601A47E RID: 107646 RVA: 0x00357F90 File Offset: 0x00356190
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

		// Token: 0x1700921E RID: 37406
		// (get) Token: 0x0601A47F RID: 107647 RVA: 0x00357F9B File Offset: 0x0035619B
		// (set) Token: 0x0601A480 RID: 107648 RVA: 0x00357FA5 File Offset: 0x003561A5
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

		// Token: 0x1700921F RID: 37407
		// (get) Token: 0x0601A481 RID: 107649 RVA: 0x00357FB0 File Offset: 0x003561B0
		// (set) Token: 0x0601A482 RID: 107650 RVA: 0x00357FBA File Offset: 0x003561BA
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

		// Token: 0x17009220 RID: 37408
		// (get) Token: 0x0601A483 RID: 107651 RVA: 0x00357FC5 File Offset: 0x003561C5
		// (set) Token: 0x0601A484 RID: 107652 RVA: 0x00357FCF File Offset: 0x003561CF
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

		// Token: 0x17009221 RID: 37409
		// (get) Token: 0x0601A485 RID: 107653 RVA: 0x00357FDA File Offset: 0x003561DA
		// (set) Token: 0x0601A486 RID: 107654 RVA: 0x00357FE4 File Offset: 0x003561E4
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

		// Token: 0x17009222 RID: 37410
		// (get) Token: 0x0601A487 RID: 107655 RVA: 0x00357FEF File Offset: 0x003561EF
		// (set) Token: 0x0601A488 RID: 107656 RVA: 0x00357FF9 File Offset: 0x003561F9
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

		// Token: 0x17009223 RID: 37411
		// (get) Token: 0x0601A489 RID: 107657 RVA: 0x00358004 File Offset: 0x00356204
		// (set) Token: 0x0601A48A RID: 107658 RVA: 0x0035800E File Offset: 0x0035620E
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

		// Token: 0x17009224 RID: 37412
		// (get) Token: 0x0601A48B RID: 107659 RVA: 0x00358019 File Offset: 0x00356219
		// (set) Token: 0x0601A48C RID: 107660 RVA: 0x00358023 File Offset: 0x00356223
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

		// Token: 0x17009225 RID: 37413
		// (get) Token: 0x0601A48D RID: 107661 RVA: 0x0035802E File Offset: 0x0035622E
		// (set) Token: 0x0601A48E RID: 107662 RVA: 0x00358038 File Offset: 0x00356238
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

		// Token: 0x17009226 RID: 37414
		// (get) Token: 0x0601A48F RID: 107663 RVA: 0x00358043 File Offset: 0x00356243
		// (set) Token: 0x0601A490 RID: 107664 RVA: 0x0035804D File Offset: 0x0035624D
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

		// Token: 0x17009227 RID: 37415
		// (get) Token: 0x0601A491 RID: 107665 RVA: 0x00358058 File Offset: 0x00356258
		// (set) Token: 0x0601A492 RID: 107666 RVA: 0x00358062 File Offset: 0x00356262
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

		// Token: 0x17009228 RID: 37416
		// (get) Token: 0x0601A493 RID: 107667 RVA: 0x0035806D File Offset: 0x0035626D
		// (set) Token: 0x0601A494 RID: 107668 RVA: 0x00358077 File Offset: 0x00356277
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

		// Token: 0x17009229 RID: 37417
		// (get) Token: 0x0601A495 RID: 107669 RVA: 0x00358082 File Offset: 0x00356282
		// (set) Token: 0x0601A496 RID: 107670 RVA: 0x0035808C File Offset: 0x0035628C
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

		// Token: 0x1700922A RID: 37418
		// (get) Token: 0x0601A497 RID: 107671 RVA: 0x00358097 File Offset: 0x00356297
		// (set) Token: 0x0601A498 RID: 107672 RVA: 0x003580A1 File Offset: 0x003562A1
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

		// Token: 0x1700922B RID: 37419
		// (get) Token: 0x0601A499 RID: 107673 RVA: 0x003580AC File Offset: 0x003562AC
		// (set) Token: 0x0601A49A RID: 107674 RVA: 0x003580B6 File Offset: 0x003562B6
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

		// Token: 0x1700922C RID: 37420
		// (get) Token: 0x0601A49B RID: 107675 RVA: 0x003580C1 File Offset: 0x003562C1
		// (set) Token: 0x0601A49C RID: 107676 RVA: 0x003580CB File Offset: 0x003562CB
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

		// Token: 0x1700922D RID: 37421
		// (get) Token: 0x0601A49D RID: 107677 RVA: 0x003580D6 File Offset: 0x003562D6
		// (set) Token: 0x0601A49E RID: 107678 RVA: 0x003580E0 File Offset: 0x003562E0
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

		// Token: 0x1700922E RID: 37422
		// (get) Token: 0x0601A49F RID: 107679 RVA: 0x003580EB File Offset: 0x003562EB
		// (set) Token: 0x0601A4A0 RID: 107680 RVA: 0x003580F5 File Offset: 0x003562F5
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

		// Token: 0x0601A4A1 RID: 107681 RVA: 0x00360004 File Offset: 0x0035E204
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousParagraphProperties>(deep);
		}

		// Token: 0x0400AC8B RID: 44171
		private const string tagName = "pPr";

		// Token: 0x0400AC8C RID: 44172
		private const byte tagNsId = 23;

		// Token: 0x0400AC8D RID: 44173
		internal const int ElementTypeIdConst = 11872;

		// Token: 0x0400AC8E RID: 44174
		private static readonly string[] eleTagNames = new string[]
		{
			"pStyle", "keepNext", "keepLines", "pageBreakBefore", "framePr", "widowControl", "numPr", "suppressLineNumbers", "pBdr", "shd",
			"tabs", "suppressAutoHyphens", "kinsoku", "wordWrap", "overflowPunct", "topLinePunct", "autoSpaceDE", "autoSpaceDN", "bidi", "adjustRightInd",
			"snapToGrid", "spacing", "ind", "contextualSpacing", "mirrorIndents", "suppressOverlap", "jc", "textDirection", "textAlignment", "textboxTightWrap",
			"outlineLvl"
		};

		// Token: 0x0400AC8F RID: 44175
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23
		};
	}
}
