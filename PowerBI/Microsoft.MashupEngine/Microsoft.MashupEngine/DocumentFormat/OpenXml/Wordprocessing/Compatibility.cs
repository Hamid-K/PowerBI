using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF5 RID: 12277
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AlignTablesRowByRow))]
	[ChildElementInfo(typeof(CompatibilitySetting))]
	[ChildElementInfo(typeof(WordPerfectJustification))]
	[ChildElementInfo(typeof(NoTabHangIndent))]
	[ChildElementInfo(typeof(NoLeading))]
	[ChildElementInfo(typeof(SpaceForUnderline))]
	[ChildElementInfo(typeof(NoColumnBalance))]
	[ChildElementInfo(typeof(BalanceSingleByteDoubleByteWidth))]
	[ChildElementInfo(typeof(NoExtraLineSpacing))]
	[ChildElementInfo(typeof(DoNotLeaveBackslashAlone))]
	[ChildElementInfo(typeof(UnderlineTrailingSpaces))]
	[ChildElementInfo(typeof(DoNotExpandShiftReturn))]
	[ChildElementInfo(typeof(SpacingInWholePoints))]
	[ChildElementInfo(typeof(LineWrapLikeWord6))]
	[ChildElementInfo(typeof(PrintBodyTextBeforeHeader))]
	[ChildElementInfo(typeof(PrintColorBlackWhite))]
	[ChildElementInfo(typeof(WordPerfectSpaceWidth))]
	[ChildElementInfo(typeof(ShowBreaksInFrames))]
	[ChildElementInfo(typeof(SubFontBySize))]
	[ChildElementInfo(typeof(SuppressBottomSpacing))]
	[ChildElementInfo(typeof(SuppressTopSpacing))]
	[ChildElementInfo(typeof(SuppressSpacingAtTopOfPage))]
	[ChildElementInfo(typeof(SuppressTopSpacingWordPerfect))]
	[ChildElementInfo(typeof(SuppressSpacingBeforeAfterPageBreak))]
	[ChildElementInfo(typeof(SwapBordersFacingPages))]
	[ChildElementInfo(typeof(ConvertMailMergeEscape))]
	[ChildElementInfo(typeof(TruncateFontHeightsLikeWordPerfect))]
	[ChildElementInfo(typeof(MacWordSmallCaps))]
	[ChildElementInfo(typeof(UsePrinterMetrics))]
	[ChildElementInfo(typeof(DoNotSuppressParagraphBorders))]
	[ChildElementInfo(typeof(WrapTrailSpaces))]
	[ChildElementInfo(typeof(FootnoteLayoutLikeWord8))]
	[ChildElementInfo(typeof(ShapeLayoutLikeWord8))]
	[ChildElementInfo(typeof(UseSingleBorderForContiguousCells))]
	[ChildElementInfo(typeof(ForgetLastTabAlignment))]
	[ChildElementInfo(typeof(AdjustLineHeightInTable))]
	[ChildElementInfo(typeof(AutoSpaceLikeWord95))]
	[ChildElementInfo(typeof(NoSpaceRaiseLower))]
	[ChildElementInfo(typeof(DoNotUseHTMLParagraphAutoSpacing))]
	[ChildElementInfo(typeof(LayoutRawTableWidth))]
	[ChildElementInfo(typeof(LayoutTableRowsApart))]
	[ChildElementInfo(typeof(UseWord97LineBreakRules))]
	[ChildElementInfo(typeof(DoNotBreakWrappedTables))]
	[ChildElementInfo(typeof(DoNotSnapToGridInCell))]
	[ChildElementInfo(typeof(SelectFieldWithFirstOrLastChar))]
	[ChildElementInfo(typeof(ApplyBreakingRules))]
	[ChildElementInfo(typeof(DoNotWrapTextWithPunctuation))]
	[ChildElementInfo(typeof(DoNotUseEastAsianBreakRules))]
	[ChildElementInfo(typeof(UseWord2002TableStyleRules))]
	[ChildElementInfo(typeof(GrowAutofit))]
	[ChildElementInfo(typeof(UseFarEastLayout))]
	[ChildElementInfo(typeof(UseNormalStyleForList))]
	[ChildElementInfo(typeof(DoNotUseIndentAsNumberingTabStop))]
	[ChildElementInfo(typeof(UseAltKinsokuLineBreakRules))]
	[ChildElementInfo(typeof(AllowSpaceOfSameStyleInTable))]
	[ChildElementInfo(typeof(DoNotSuppressIndentation))]
	[ChildElementInfo(typeof(DoNotAutofitConstrainedTables))]
	[ChildElementInfo(typeof(AutofitToFirstFixedWidthCell))]
	[ChildElementInfo(typeof(UnderlineTabInNumberingList))]
	[ChildElementInfo(typeof(DisplayHangulFixedWidth))]
	[ChildElementInfo(typeof(SplitPageBreakAndParagraphMark))]
	[ChildElementInfo(typeof(DoNotVerticallyAlignCellWithShape))]
	[ChildElementInfo(typeof(DoNotBreakConstrainedForcedTable))]
	[ChildElementInfo(typeof(DoNotVerticallyAlignInTextBox))]
	[ChildElementInfo(typeof(UseAnsiKerningPairs))]
	[ChildElementInfo(typeof(CachedColumnBalance))]
	internal class Compatibility : OpenXmlCompositeElement
	{
		// Token: 0x17009559 RID: 38233
		// (get) Token: 0x0601AB60 RID: 109408 RVA: 0x00366248 File Offset: 0x00364448
		public override string LocalName
		{
			get
			{
				return "compat";
			}
		}

		// Token: 0x1700955A RID: 38234
		// (get) Token: 0x0601AB61 RID: 109409 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700955B RID: 38235
		// (get) Token: 0x0601AB62 RID: 109410 RVA: 0x0036624F File Offset: 0x0036444F
		internal override int ElementTypeId
		{
			get
			{
				return 12038;
			}
		}

		// Token: 0x0601AB63 RID: 109411 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AB64 RID: 109412 RVA: 0x00293ECF File Offset: 0x002920CF
		public Compatibility()
		{
		}

		// Token: 0x0601AB65 RID: 109413 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Compatibility(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB66 RID: 109414 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Compatibility(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB67 RID: 109415 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Compatibility(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AB68 RID: 109416 RVA: 0x00366258 File Offset: 0x00364458
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "useSingleBorderforContiguousCells" == name)
			{
				return new UseSingleBorderForContiguousCells();
			}
			if (23 == namespaceId && "wpJustification" == name)
			{
				return new WordPerfectJustification();
			}
			if (23 == namespaceId && "noTabHangInd" == name)
			{
				return new NoTabHangIndent();
			}
			if (23 == namespaceId && "noLeading" == name)
			{
				return new NoLeading();
			}
			if (23 == namespaceId && "spaceForUL" == name)
			{
				return new SpaceForUnderline();
			}
			if (23 == namespaceId && "noColumnBalance" == name)
			{
				return new NoColumnBalance();
			}
			if (23 == namespaceId && "balanceSingleByteDoubleByteWidth" == name)
			{
				return new BalanceSingleByteDoubleByteWidth();
			}
			if (23 == namespaceId && "noExtraLineSpacing" == name)
			{
				return new NoExtraLineSpacing();
			}
			if (23 == namespaceId && "doNotLeaveBackslashAlone" == name)
			{
				return new DoNotLeaveBackslashAlone();
			}
			if (23 == namespaceId && "ulTrailSpace" == name)
			{
				return new UnderlineTrailingSpaces();
			}
			if (23 == namespaceId && "doNotExpandShiftReturn" == name)
			{
				return new DoNotExpandShiftReturn();
			}
			if (23 == namespaceId && "spacingInWholePoints" == name)
			{
				return new SpacingInWholePoints();
			}
			if (23 == namespaceId && "lineWrapLikeWord6" == name)
			{
				return new LineWrapLikeWord6();
			}
			if (23 == namespaceId && "printBodyTextBeforeHeader" == name)
			{
				return new PrintBodyTextBeforeHeader();
			}
			if (23 == namespaceId && "printColBlack" == name)
			{
				return new PrintColorBlackWhite();
			}
			if (23 == namespaceId && "wpSpaceWidth" == name)
			{
				return new WordPerfectSpaceWidth();
			}
			if (23 == namespaceId && "showBreaksInFrames" == name)
			{
				return new ShowBreaksInFrames();
			}
			if (23 == namespaceId && "subFontBySize" == name)
			{
				return new SubFontBySize();
			}
			if (23 == namespaceId && "suppressBottomSpacing" == name)
			{
				return new SuppressBottomSpacing();
			}
			if (23 == namespaceId && "suppressTopSpacing" == name)
			{
				return new SuppressTopSpacing();
			}
			if (23 == namespaceId && "suppressSpacingAtTopOfPage" == name)
			{
				return new SuppressSpacingAtTopOfPage();
			}
			if (23 == namespaceId && "suppressTopSpacingWP" == name)
			{
				return new SuppressTopSpacingWordPerfect();
			}
			if (23 == namespaceId && "suppressSpBfAfterPgBrk" == name)
			{
				return new SuppressSpacingBeforeAfterPageBreak();
			}
			if (23 == namespaceId && "swapBordersFacingPages" == name)
			{
				return new SwapBordersFacingPages();
			}
			if (23 == namespaceId && "convMailMergeEsc" == name)
			{
				return new ConvertMailMergeEscape();
			}
			if (23 == namespaceId && "truncateFontHeightsLikeWP6" == name)
			{
				return new TruncateFontHeightsLikeWordPerfect();
			}
			if (23 == namespaceId && "mwSmallCaps" == name)
			{
				return new MacWordSmallCaps();
			}
			if (23 == namespaceId && "usePrinterMetrics" == name)
			{
				return new UsePrinterMetrics();
			}
			if (23 == namespaceId && "doNotSuppressParagraphBorders" == name)
			{
				return new DoNotSuppressParagraphBorders();
			}
			if (23 == namespaceId && "wrapTrailSpaces" == name)
			{
				return new WrapTrailSpaces();
			}
			if (23 == namespaceId && "footnoteLayoutLikeWW8" == name)
			{
				return new FootnoteLayoutLikeWord8();
			}
			if (23 == namespaceId && "shapeLayoutLikeWW8" == name)
			{
				return new ShapeLayoutLikeWord8();
			}
			if (23 == namespaceId && "alignTablesRowByRow" == name)
			{
				return new AlignTablesRowByRow();
			}
			if (23 == namespaceId && "forgetLastTabAlignment" == name)
			{
				return new ForgetLastTabAlignment();
			}
			if (23 == namespaceId && "adjustLineHeightInTable" == name)
			{
				return new AdjustLineHeightInTable();
			}
			if (23 == namespaceId && "autoSpaceLikeWord95" == name)
			{
				return new AutoSpaceLikeWord95();
			}
			if (23 == namespaceId && "noSpaceRaiseLower" == name)
			{
				return new NoSpaceRaiseLower();
			}
			if (23 == namespaceId && "doNotUseHTMLParagraphAutoSpacing" == name)
			{
				return new DoNotUseHTMLParagraphAutoSpacing();
			}
			if (23 == namespaceId && "layoutRawTableWidth" == name)
			{
				return new LayoutRawTableWidth();
			}
			if (23 == namespaceId && "layoutTableRowsApart" == name)
			{
				return new LayoutTableRowsApart();
			}
			if (23 == namespaceId && "useWord97LineBreakRules" == name)
			{
				return new UseWord97LineBreakRules();
			}
			if (23 == namespaceId && "doNotBreakWrappedTables" == name)
			{
				return new DoNotBreakWrappedTables();
			}
			if (23 == namespaceId && "doNotSnapToGridInCell" == name)
			{
				return new DoNotSnapToGridInCell();
			}
			if (23 == namespaceId && "selectFldWithFirstOrLastChar" == name)
			{
				return new SelectFieldWithFirstOrLastChar();
			}
			if (23 == namespaceId && "applyBreakingRules" == name)
			{
				return new ApplyBreakingRules();
			}
			if (23 == namespaceId && "doNotWrapTextWithPunct" == name)
			{
				return new DoNotWrapTextWithPunctuation();
			}
			if (23 == namespaceId && "doNotUseEastAsianBreakRules" == name)
			{
				return new DoNotUseEastAsianBreakRules();
			}
			if (23 == namespaceId && "useWord2002TableStyleRules" == name)
			{
				return new UseWord2002TableStyleRules();
			}
			if (23 == namespaceId && "growAutofit" == name)
			{
				return new GrowAutofit();
			}
			if (23 == namespaceId && "useFELayout" == name)
			{
				return new UseFarEastLayout();
			}
			if (23 == namespaceId && "useNormalStyleForList" == name)
			{
				return new UseNormalStyleForList();
			}
			if (23 == namespaceId && "doNotUseIndentAsNumberingTabStop" == name)
			{
				return new DoNotUseIndentAsNumberingTabStop();
			}
			if (23 == namespaceId && "useAltKinsokuLineBreakRules" == name)
			{
				return new UseAltKinsokuLineBreakRules();
			}
			if (23 == namespaceId && "allowSpaceOfSameStyleInTable" == name)
			{
				return new AllowSpaceOfSameStyleInTable();
			}
			if (23 == namespaceId && "doNotSuppressIndentation" == name)
			{
				return new DoNotSuppressIndentation();
			}
			if (23 == namespaceId && "doNotAutofitConstrainedTables" == name)
			{
				return new DoNotAutofitConstrainedTables();
			}
			if (23 == namespaceId && "autofitToFirstFixedWidthCell" == name)
			{
				return new AutofitToFirstFixedWidthCell();
			}
			if (23 == namespaceId && "underlineTabInNumList" == name)
			{
				return new UnderlineTabInNumberingList();
			}
			if (23 == namespaceId && "displayHangulFixedWidth" == name)
			{
				return new DisplayHangulFixedWidth();
			}
			if (23 == namespaceId && "splitPgBreakAndParaMark" == name)
			{
				return new SplitPageBreakAndParagraphMark();
			}
			if (23 == namespaceId && "doNotVertAlignCellWithSp" == name)
			{
				return new DoNotVerticallyAlignCellWithShape();
			}
			if (23 == namespaceId && "doNotBreakConstrainedForcedTable" == name)
			{
				return new DoNotBreakConstrainedForcedTable();
			}
			if (23 == namespaceId && "doNotVertAlignInTxbx" == name)
			{
				return new DoNotVerticallyAlignInTextBox();
			}
			if (23 == namespaceId && "useAnsiKerningPairs" == name)
			{
				return new UseAnsiKerningPairs();
			}
			if (23 == namespaceId && "cachedColBalance" == name)
			{
				return new CachedColumnBalance();
			}
			if (23 == namespaceId && "compatSetting" == name)
			{
				return new CompatibilitySetting();
			}
			return null;
		}

		// Token: 0x1700955C RID: 38236
		// (get) Token: 0x0601AB69 RID: 109417 RVA: 0x00366896 File Offset: 0x00364A96
		internal override string[] ElementTagNames
		{
			get
			{
				return Compatibility.eleTagNames;
			}
		}

		// Token: 0x1700955D RID: 38237
		// (get) Token: 0x0601AB6A RID: 109418 RVA: 0x0036689D File Offset: 0x00364A9D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Compatibility.eleNamespaceIds;
			}
		}

		// Token: 0x1700955E RID: 38238
		// (get) Token: 0x0601AB6B RID: 109419 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700955F RID: 38239
		// (get) Token: 0x0601AB6C RID: 109420 RVA: 0x003668A4 File Offset: 0x00364AA4
		// (set) Token: 0x0601AB6D RID: 109421 RVA: 0x003668AD File Offset: 0x00364AAD
		public UseSingleBorderForContiguousCells UseSingleBorderForContiguousCells
		{
			get
			{
				return base.GetElement<UseSingleBorderForContiguousCells>(0);
			}
			set
			{
				base.SetElement<UseSingleBorderForContiguousCells>(0, value);
			}
		}

		// Token: 0x17009560 RID: 38240
		// (get) Token: 0x0601AB6E RID: 109422 RVA: 0x003668B7 File Offset: 0x00364AB7
		// (set) Token: 0x0601AB6F RID: 109423 RVA: 0x003668C0 File Offset: 0x00364AC0
		public WordPerfectJustification WordPerfectJustification
		{
			get
			{
				return base.GetElement<WordPerfectJustification>(1);
			}
			set
			{
				base.SetElement<WordPerfectJustification>(1, value);
			}
		}

		// Token: 0x17009561 RID: 38241
		// (get) Token: 0x0601AB70 RID: 109424 RVA: 0x003668CA File Offset: 0x00364ACA
		// (set) Token: 0x0601AB71 RID: 109425 RVA: 0x003668D3 File Offset: 0x00364AD3
		public NoTabHangIndent NoTabHangIndent
		{
			get
			{
				return base.GetElement<NoTabHangIndent>(2);
			}
			set
			{
				base.SetElement<NoTabHangIndent>(2, value);
			}
		}

		// Token: 0x17009562 RID: 38242
		// (get) Token: 0x0601AB72 RID: 109426 RVA: 0x003668DD File Offset: 0x00364ADD
		// (set) Token: 0x0601AB73 RID: 109427 RVA: 0x003668E6 File Offset: 0x00364AE6
		public NoLeading NoLeading
		{
			get
			{
				return base.GetElement<NoLeading>(3);
			}
			set
			{
				base.SetElement<NoLeading>(3, value);
			}
		}

		// Token: 0x17009563 RID: 38243
		// (get) Token: 0x0601AB74 RID: 109428 RVA: 0x003668F0 File Offset: 0x00364AF0
		// (set) Token: 0x0601AB75 RID: 109429 RVA: 0x003668F9 File Offset: 0x00364AF9
		public SpaceForUnderline SpaceForUnderline
		{
			get
			{
				return base.GetElement<SpaceForUnderline>(4);
			}
			set
			{
				base.SetElement<SpaceForUnderline>(4, value);
			}
		}

		// Token: 0x17009564 RID: 38244
		// (get) Token: 0x0601AB76 RID: 109430 RVA: 0x00366903 File Offset: 0x00364B03
		// (set) Token: 0x0601AB77 RID: 109431 RVA: 0x0036690C File Offset: 0x00364B0C
		public NoColumnBalance NoColumnBalance
		{
			get
			{
				return base.GetElement<NoColumnBalance>(5);
			}
			set
			{
				base.SetElement<NoColumnBalance>(5, value);
			}
		}

		// Token: 0x17009565 RID: 38245
		// (get) Token: 0x0601AB78 RID: 109432 RVA: 0x00366916 File Offset: 0x00364B16
		// (set) Token: 0x0601AB79 RID: 109433 RVA: 0x0036691F File Offset: 0x00364B1F
		public BalanceSingleByteDoubleByteWidth BalanceSingleByteDoubleByteWidth
		{
			get
			{
				return base.GetElement<BalanceSingleByteDoubleByteWidth>(6);
			}
			set
			{
				base.SetElement<BalanceSingleByteDoubleByteWidth>(6, value);
			}
		}

		// Token: 0x17009566 RID: 38246
		// (get) Token: 0x0601AB7A RID: 109434 RVA: 0x00366929 File Offset: 0x00364B29
		// (set) Token: 0x0601AB7B RID: 109435 RVA: 0x00366932 File Offset: 0x00364B32
		public NoExtraLineSpacing NoExtraLineSpacing
		{
			get
			{
				return base.GetElement<NoExtraLineSpacing>(7);
			}
			set
			{
				base.SetElement<NoExtraLineSpacing>(7, value);
			}
		}

		// Token: 0x17009567 RID: 38247
		// (get) Token: 0x0601AB7C RID: 109436 RVA: 0x0036693C File Offset: 0x00364B3C
		// (set) Token: 0x0601AB7D RID: 109437 RVA: 0x00366945 File Offset: 0x00364B45
		public DoNotLeaveBackslashAlone DoNotLeaveBackslashAlone
		{
			get
			{
				return base.GetElement<DoNotLeaveBackslashAlone>(8);
			}
			set
			{
				base.SetElement<DoNotLeaveBackslashAlone>(8, value);
			}
		}

		// Token: 0x17009568 RID: 38248
		// (get) Token: 0x0601AB7E RID: 109438 RVA: 0x0036694F File Offset: 0x00364B4F
		// (set) Token: 0x0601AB7F RID: 109439 RVA: 0x00366959 File Offset: 0x00364B59
		public UnderlineTrailingSpaces UnderlineTrailingSpaces
		{
			get
			{
				return base.GetElement<UnderlineTrailingSpaces>(9);
			}
			set
			{
				base.SetElement<UnderlineTrailingSpaces>(9, value);
			}
		}

		// Token: 0x17009569 RID: 38249
		// (get) Token: 0x0601AB80 RID: 109440 RVA: 0x00366964 File Offset: 0x00364B64
		// (set) Token: 0x0601AB81 RID: 109441 RVA: 0x0036696E File Offset: 0x00364B6E
		public DoNotExpandShiftReturn DoNotExpandShiftReturn
		{
			get
			{
				return base.GetElement<DoNotExpandShiftReturn>(10);
			}
			set
			{
				base.SetElement<DoNotExpandShiftReturn>(10, value);
			}
		}

		// Token: 0x1700956A RID: 38250
		// (get) Token: 0x0601AB82 RID: 109442 RVA: 0x00366979 File Offset: 0x00364B79
		// (set) Token: 0x0601AB83 RID: 109443 RVA: 0x00366983 File Offset: 0x00364B83
		public SpacingInWholePoints SpacingInWholePoints
		{
			get
			{
				return base.GetElement<SpacingInWholePoints>(11);
			}
			set
			{
				base.SetElement<SpacingInWholePoints>(11, value);
			}
		}

		// Token: 0x1700956B RID: 38251
		// (get) Token: 0x0601AB84 RID: 109444 RVA: 0x0036698E File Offset: 0x00364B8E
		// (set) Token: 0x0601AB85 RID: 109445 RVA: 0x00366998 File Offset: 0x00364B98
		public LineWrapLikeWord6 LineWrapLikeWord6
		{
			get
			{
				return base.GetElement<LineWrapLikeWord6>(12);
			}
			set
			{
				base.SetElement<LineWrapLikeWord6>(12, value);
			}
		}

		// Token: 0x1700956C RID: 38252
		// (get) Token: 0x0601AB86 RID: 109446 RVA: 0x003669A3 File Offset: 0x00364BA3
		// (set) Token: 0x0601AB87 RID: 109447 RVA: 0x003669AD File Offset: 0x00364BAD
		public PrintBodyTextBeforeHeader PrintBodyTextBeforeHeader
		{
			get
			{
				return base.GetElement<PrintBodyTextBeforeHeader>(13);
			}
			set
			{
				base.SetElement<PrintBodyTextBeforeHeader>(13, value);
			}
		}

		// Token: 0x1700956D RID: 38253
		// (get) Token: 0x0601AB88 RID: 109448 RVA: 0x003669B8 File Offset: 0x00364BB8
		// (set) Token: 0x0601AB89 RID: 109449 RVA: 0x003669C2 File Offset: 0x00364BC2
		public PrintColorBlackWhite PrintColorBlackWhite
		{
			get
			{
				return base.GetElement<PrintColorBlackWhite>(14);
			}
			set
			{
				base.SetElement<PrintColorBlackWhite>(14, value);
			}
		}

		// Token: 0x1700956E RID: 38254
		// (get) Token: 0x0601AB8A RID: 109450 RVA: 0x003669CD File Offset: 0x00364BCD
		// (set) Token: 0x0601AB8B RID: 109451 RVA: 0x003669D7 File Offset: 0x00364BD7
		public WordPerfectSpaceWidth WordPerfectSpaceWidth
		{
			get
			{
				return base.GetElement<WordPerfectSpaceWidth>(15);
			}
			set
			{
				base.SetElement<WordPerfectSpaceWidth>(15, value);
			}
		}

		// Token: 0x1700956F RID: 38255
		// (get) Token: 0x0601AB8C RID: 109452 RVA: 0x003669E2 File Offset: 0x00364BE2
		// (set) Token: 0x0601AB8D RID: 109453 RVA: 0x003669EC File Offset: 0x00364BEC
		public ShowBreaksInFrames ShowBreaksInFrames
		{
			get
			{
				return base.GetElement<ShowBreaksInFrames>(16);
			}
			set
			{
				base.SetElement<ShowBreaksInFrames>(16, value);
			}
		}

		// Token: 0x17009570 RID: 38256
		// (get) Token: 0x0601AB8E RID: 109454 RVA: 0x003669F7 File Offset: 0x00364BF7
		// (set) Token: 0x0601AB8F RID: 109455 RVA: 0x00366A01 File Offset: 0x00364C01
		public SubFontBySize SubFontBySize
		{
			get
			{
				return base.GetElement<SubFontBySize>(17);
			}
			set
			{
				base.SetElement<SubFontBySize>(17, value);
			}
		}

		// Token: 0x17009571 RID: 38257
		// (get) Token: 0x0601AB90 RID: 109456 RVA: 0x00366A0C File Offset: 0x00364C0C
		// (set) Token: 0x0601AB91 RID: 109457 RVA: 0x00366A16 File Offset: 0x00364C16
		public SuppressBottomSpacing SuppressBottomSpacing
		{
			get
			{
				return base.GetElement<SuppressBottomSpacing>(18);
			}
			set
			{
				base.SetElement<SuppressBottomSpacing>(18, value);
			}
		}

		// Token: 0x17009572 RID: 38258
		// (get) Token: 0x0601AB92 RID: 109458 RVA: 0x00366A21 File Offset: 0x00364C21
		// (set) Token: 0x0601AB93 RID: 109459 RVA: 0x00366A2B File Offset: 0x00364C2B
		public SuppressTopSpacing SuppressTopSpacing
		{
			get
			{
				return base.GetElement<SuppressTopSpacing>(19);
			}
			set
			{
				base.SetElement<SuppressTopSpacing>(19, value);
			}
		}

		// Token: 0x17009573 RID: 38259
		// (get) Token: 0x0601AB94 RID: 109460 RVA: 0x00366A36 File Offset: 0x00364C36
		// (set) Token: 0x0601AB95 RID: 109461 RVA: 0x00366A40 File Offset: 0x00364C40
		public SuppressSpacingAtTopOfPage SuppressSpacingAtTopOfPage
		{
			get
			{
				return base.GetElement<SuppressSpacingAtTopOfPage>(20);
			}
			set
			{
				base.SetElement<SuppressSpacingAtTopOfPage>(20, value);
			}
		}

		// Token: 0x17009574 RID: 38260
		// (get) Token: 0x0601AB96 RID: 109462 RVA: 0x00366A4B File Offset: 0x00364C4B
		// (set) Token: 0x0601AB97 RID: 109463 RVA: 0x00366A55 File Offset: 0x00364C55
		public SuppressTopSpacingWordPerfect SuppressTopSpacingWordPerfect
		{
			get
			{
				return base.GetElement<SuppressTopSpacingWordPerfect>(21);
			}
			set
			{
				base.SetElement<SuppressTopSpacingWordPerfect>(21, value);
			}
		}

		// Token: 0x17009575 RID: 38261
		// (get) Token: 0x0601AB98 RID: 109464 RVA: 0x00366A60 File Offset: 0x00364C60
		// (set) Token: 0x0601AB99 RID: 109465 RVA: 0x00366A6A File Offset: 0x00364C6A
		public SuppressSpacingBeforeAfterPageBreak SuppressSpacingBeforeAfterPageBreak
		{
			get
			{
				return base.GetElement<SuppressSpacingBeforeAfterPageBreak>(22);
			}
			set
			{
				base.SetElement<SuppressSpacingBeforeAfterPageBreak>(22, value);
			}
		}

		// Token: 0x17009576 RID: 38262
		// (get) Token: 0x0601AB9A RID: 109466 RVA: 0x00366A75 File Offset: 0x00364C75
		// (set) Token: 0x0601AB9B RID: 109467 RVA: 0x00366A7F File Offset: 0x00364C7F
		public SwapBordersFacingPages SwapBordersFacingPages
		{
			get
			{
				return base.GetElement<SwapBordersFacingPages>(23);
			}
			set
			{
				base.SetElement<SwapBordersFacingPages>(23, value);
			}
		}

		// Token: 0x17009577 RID: 38263
		// (get) Token: 0x0601AB9C RID: 109468 RVA: 0x00366A8A File Offset: 0x00364C8A
		// (set) Token: 0x0601AB9D RID: 109469 RVA: 0x00366A94 File Offset: 0x00364C94
		public ConvertMailMergeEscape ConvertMailMergeEscape
		{
			get
			{
				return base.GetElement<ConvertMailMergeEscape>(24);
			}
			set
			{
				base.SetElement<ConvertMailMergeEscape>(24, value);
			}
		}

		// Token: 0x17009578 RID: 38264
		// (get) Token: 0x0601AB9E RID: 109470 RVA: 0x00366A9F File Offset: 0x00364C9F
		// (set) Token: 0x0601AB9F RID: 109471 RVA: 0x00366AA9 File Offset: 0x00364CA9
		public TruncateFontHeightsLikeWordPerfect TruncateFontHeightsLikeWordPerfect
		{
			get
			{
				return base.GetElement<TruncateFontHeightsLikeWordPerfect>(25);
			}
			set
			{
				base.SetElement<TruncateFontHeightsLikeWordPerfect>(25, value);
			}
		}

		// Token: 0x17009579 RID: 38265
		// (get) Token: 0x0601ABA0 RID: 109472 RVA: 0x00366AB4 File Offset: 0x00364CB4
		// (set) Token: 0x0601ABA1 RID: 109473 RVA: 0x00366ABE File Offset: 0x00364CBE
		public MacWordSmallCaps MacWordSmallCaps
		{
			get
			{
				return base.GetElement<MacWordSmallCaps>(26);
			}
			set
			{
				base.SetElement<MacWordSmallCaps>(26, value);
			}
		}

		// Token: 0x1700957A RID: 38266
		// (get) Token: 0x0601ABA2 RID: 109474 RVA: 0x00366AC9 File Offset: 0x00364CC9
		// (set) Token: 0x0601ABA3 RID: 109475 RVA: 0x00366AD3 File Offset: 0x00364CD3
		public UsePrinterMetrics UsePrinterMetrics
		{
			get
			{
				return base.GetElement<UsePrinterMetrics>(27);
			}
			set
			{
				base.SetElement<UsePrinterMetrics>(27, value);
			}
		}

		// Token: 0x1700957B RID: 38267
		// (get) Token: 0x0601ABA4 RID: 109476 RVA: 0x00366ADE File Offset: 0x00364CDE
		// (set) Token: 0x0601ABA5 RID: 109477 RVA: 0x00366AE8 File Offset: 0x00364CE8
		public DoNotSuppressParagraphBorders DoNotSuppressParagraphBorders
		{
			get
			{
				return base.GetElement<DoNotSuppressParagraphBorders>(28);
			}
			set
			{
				base.SetElement<DoNotSuppressParagraphBorders>(28, value);
			}
		}

		// Token: 0x1700957C RID: 38268
		// (get) Token: 0x0601ABA6 RID: 109478 RVA: 0x00366AF3 File Offset: 0x00364CF3
		// (set) Token: 0x0601ABA7 RID: 109479 RVA: 0x00366AFD File Offset: 0x00364CFD
		public WrapTrailSpaces WrapTrailSpaces
		{
			get
			{
				return base.GetElement<WrapTrailSpaces>(29);
			}
			set
			{
				base.SetElement<WrapTrailSpaces>(29, value);
			}
		}

		// Token: 0x1700957D RID: 38269
		// (get) Token: 0x0601ABA8 RID: 109480 RVA: 0x00366B08 File Offset: 0x00364D08
		// (set) Token: 0x0601ABA9 RID: 109481 RVA: 0x00366B12 File Offset: 0x00364D12
		public FootnoteLayoutLikeWord8 FootnoteLayoutLikeWord8
		{
			get
			{
				return base.GetElement<FootnoteLayoutLikeWord8>(30);
			}
			set
			{
				base.SetElement<FootnoteLayoutLikeWord8>(30, value);
			}
		}

		// Token: 0x1700957E RID: 38270
		// (get) Token: 0x0601ABAA RID: 109482 RVA: 0x00366B1D File Offset: 0x00364D1D
		// (set) Token: 0x0601ABAB RID: 109483 RVA: 0x00366B27 File Offset: 0x00364D27
		public ShapeLayoutLikeWord8 ShapeLayoutLikeWord8
		{
			get
			{
				return base.GetElement<ShapeLayoutLikeWord8>(31);
			}
			set
			{
				base.SetElement<ShapeLayoutLikeWord8>(31, value);
			}
		}

		// Token: 0x1700957F RID: 38271
		// (get) Token: 0x0601ABAC RID: 109484 RVA: 0x00366B32 File Offset: 0x00364D32
		// (set) Token: 0x0601ABAD RID: 109485 RVA: 0x00366B3C File Offset: 0x00364D3C
		public AlignTablesRowByRow AlignTablesRowByRow
		{
			get
			{
				return base.GetElement<AlignTablesRowByRow>(32);
			}
			set
			{
				base.SetElement<AlignTablesRowByRow>(32, value);
			}
		}

		// Token: 0x17009580 RID: 38272
		// (get) Token: 0x0601ABAE RID: 109486 RVA: 0x00366B47 File Offset: 0x00364D47
		// (set) Token: 0x0601ABAF RID: 109487 RVA: 0x00366B51 File Offset: 0x00364D51
		public ForgetLastTabAlignment ForgetLastTabAlignment
		{
			get
			{
				return base.GetElement<ForgetLastTabAlignment>(33);
			}
			set
			{
				base.SetElement<ForgetLastTabAlignment>(33, value);
			}
		}

		// Token: 0x17009581 RID: 38273
		// (get) Token: 0x0601ABB0 RID: 109488 RVA: 0x00366B5C File Offset: 0x00364D5C
		// (set) Token: 0x0601ABB1 RID: 109489 RVA: 0x00366B66 File Offset: 0x00364D66
		public AdjustLineHeightInTable AdjustLineHeightInTable
		{
			get
			{
				return base.GetElement<AdjustLineHeightInTable>(34);
			}
			set
			{
				base.SetElement<AdjustLineHeightInTable>(34, value);
			}
		}

		// Token: 0x17009582 RID: 38274
		// (get) Token: 0x0601ABB2 RID: 109490 RVA: 0x00366B71 File Offset: 0x00364D71
		// (set) Token: 0x0601ABB3 RID: 109491 RVA: 0x00366B7B File Offset: 0x00364D7B
		public AutoSpaceLikeWord95 AutoSpaceLikeWord95
		{
			get
			{
				return base.GetElement<AutoSpaceLikeWord95>(35);
			}
			set
			{
				base.SetElement<AutoSpaceLikeWord95>(35, value);
			}
		}

		// Token: 0x17009583 RID: 38275
		// (get) Token: 0x0601ABB4 RID: 109492 RVA: 0x00366B86 File Offset: 0x00364D86
		// (set) Token: 0x0601ABB5 RID: 109493 RVA: 0x00366B90 File Offset: 0x00364D90
		public NoSpaceRaiseLower NoSpaceRaiseLower
		{
			get
			{
				return base.GetElement<NoSpaceRaiseLower>(36);
			}
			set
			{
				base.SetElement<NoSpaceRaiseLower>(36, value);
			}
		}

		// Token: 0x17009584 RID: 38276
		// (get) Token: 0x0601ABB6 RID: 109494 RVA: 0x00366B9B File Offset: 0x00364D9B
		// (set) Token: 0x0601ABB7 RID: 109495 RVA: 0x00366BA5 File Offset: 0x00364DA5
		public DoNotUseHTMLParagraphAutoSpacing DoNotUseHTMLParagraphAutoSpacing
		{
			get
			{
				return base.GetElement<DoNotUseHTMLParagraphAutoSpacing>(37);
			}
			set
			{
				base.SetElement<DoNotUseHTMLParagraphAutoSpacing>(37, value);
			}
		}

		// Token: 0x17009585 RID: 38277
		// (get) Token: 0x0601ABB8 RID: 109496 RVA: 0x00366BB0 File Offset: 0x00364DB0
		// (set) Token: 0x0601ABB9 RID: 109497 RVA: 0x00366BBA File Offset: 0x00364DBA
		public LayoutRawTableWidth LayoutRawTableWidth
		{
			get
			{
				return base.GetElement<LayoutRawTableWidth>(38);
			}
			set
			{
				base.SetElement<LayoutRawTableWidth>(38, value);
			}
		}

		// Token: 0x17009586 RID: 38278
		// (get) Token: 0x0601ABBA RID: 109498 RVA: 0x00366BC5 File Offset: 0x00364DC5
		// (set) Token: 0x0601ABBB RID: 109499 RVA: 0x00366BCF File Offset: 0x00364DCF
		public LayoutTableRowsApart LayoutTableRowsApart
		{
			get
			{
				return base.GetElement<LayoutTableRowsApart>(39);
			}
			set
			{
				base.SetElement<LayoutTableRowsApart>(39, value);
			}
		}

		// Token: 0x17009587 RID: 38279
		// (get) Token: 0x0601ABBC RID: 109500 RVA: 0x00366BDA File Offset: 0x00364DDA
		// (set) Token: 0x0601ABBD RID: 109501 RVA: 0x00366BE4 File Offset: 0x00364DE4
		public UseWord97LineBreakRules UseWord97LineBreakRules
		{
			get
			{
				return base.GetElement<UseWord97LineBreakRules>(40);
			}
			set
			{
				base.SetElement<UseWord97LineBreakRules>(40, value);
			}
		}

		// Token: 0x17009588 RID: 38280
		// (get) Token: 0x0601ABBE RID: 109502 RVA: 0x00366BEF File Offset: 0x00364DEF
		// (set) Token: 0x0601ABBF RID: 109503 RVA: 0x00366BF9 File Offset: 0x00364DF9
		public DoNotBreakWrappedTables DoNotBreakWrappedTables
		{
			get
			{
				return base.GetElement<DoNotBreakWrappedTables>(41);
			}
			set
			{
				base.SetElement<DoNotBreakWrappedTables>(41, value);
			}
		}

		// Token: 0x17009589 RID: 38281
		// (get) Token: 0x0601ABC0 RID: 109504 RVA: 0x00366C04 File Offset: 0x00364E04
		// (set) Token: 0x0601ABC1 RID: 109505 RVA: 0x00366C0E File Offset: 0x00364E0E
		public DoNotSnapToGridInCell DoNotSnapToGridInCell
		{
			get
			{
				return base.GetElement<DoNotSnapToGridInCell>(42);
			}
			set
			{
				base.SetElement<DoNotSnapToGridInCell>(42, value);
			}
		}

		// Token: 0x1700958A RID: 38282
		// (get) Token: 0x0601ABC2 RID: 109506 RVA: 0x00366C19 File Offset: 0x00364E19
		// (set) Token: 0x0601ABC3 RID: 109507 RVA: 0x00366C23 File Offset: 0x00364E23
		public SelectFieldWithFirstOrLastChar SelectFieldWithFirstOrLastChar
		{
			get
			{
				return base.GetElement<SelectFieldWithFirstOrLastChar>(43);
			}
			set
			{
				base.SetElement<SelectFieldWithFirstOrLastChar>(43, value);
			}
		}

		// Token: 0x1700958B RID: 38283
		// (get) Token: 0x0601ABC4 RID: 109508 RVA: 0x00366C2E File Offset: 0x00364E2E
		// (set) Token: 0x0601ABC5 RID: 109509 RVA: 0x00366C38 File Offset: 0x00364E38
		public ApplyBreakingRules ApplyBreakingRules
		{
			get
			{
				return base.GetElement<ApplyBreakingRules>(44);
			}
			set
			{
				base.SetElement<ApplyBreakingRules>(44, value);
			}
		}

		// Token: 0x1700958C RID: 38284
		// (get) Token: 0x0601ABC6 RID: 109510 RVA: 0x00366C43 File Offset: 0x00364E43
		// (set) Token: 0x0601ABC7 RID: 109511 RVA: 0x00366C4D File Offset: 0x00364E4D
		public DoNotWrapTextWithPunctuation DoNotWrapTextWithPunctuation
		{
			get
			{
				return base.GetElement<DoNotWrapTextWithPunctuation>(45);
			}
			set
			{
				base.SetElement<DoNotWrapTextWithPunctuation>(45, value);
			}
		}

		// Token: 0x1700958D RID: 38285
		// (get) Token: 0x0601ABC8 RID: 109512 RVA: 0x00366C58 File Offset: 0x00364E58
		// (set) Token: 0x0601ABC9 RID: 109513 RVA: 0x00366C62 File Offset: 0x00364E62
		public DoNotUseEastAsianBreakRules DoNotUseEastAsianBreakRules
		{
			get
			{
				return base.GetElement<DoNotUseEastAsianBreakRules>(46);
			}
			set
			{
				base.SetElement<DoNotUseEastAsianBreakRules>(46, value);
			}
		}

		// Token: 0x1700958E RID: 38286
		// (get) Token: 0x0601ABCA RID: 109514 RVA: 0x00366C6D File Offset: 0x00364E6D
		// (set) Token: 0x0601ABCB RID: 109515 RVA: 0x00366C77 File Offset: 0x00364E77
		public UseWord2002TableStyleRules UseWord2002TableStyleRules
		{
			get
			{
				return base.GetElement<UseWord2002TableStyleRules>(47);
			}
			set
			{
				base.SetElement<UseWord2002TableStyleRules>(47, value);
			}
		}

		// Token: 0x1700958F RID: 38287
		// (get) Token: 0x0601ABCC RID: 109516 RVA: 0x00366C82 File Offset: 0x00364E82
		// (set) Token: 0x0601ABCD RID: 109517 RVA: 0x00366C8C File Offset: 0x00364E8C
		public GrowAutofit GrowAutofit
		{
			get
			{
				return base.GetElement<GrowAutofit>(48);
			}
			set
			{
				base.SetElement<GrowAutofit>(48, value);
			}
		}

		// Token: 0x17009590 RID: 38288
		// (get) Token: 0x0601ABCE RID: 109518 RVA: 0x00366C97 File Offset: 0x00364E97
		// (set) Token: 0x0601ABCF RID: 109519 RVA: 0x00366CA1 File Offset: 0x00364EA1
		public UseFarEastLayout UseFarEastLayout
		{
			get
			{
				return base.GetElement<UseFarEastLayout>(49);
			}
			set
			{
				base.SetElement<UseFarEastLayout>(49, value);
			}
		}

		// Token: 0x17009591 RID: 38289
		// (get) Token: 0x0601ABD0 RID: 109520 RVA: 0x00366CAC File Offset: 0x00364EAC
		// (set) Token: 0x0601ABD1 RID: 109521 RVA: 0x00366CB6 File Offset: 0x00364EB6
		public UseNormalStyleForList UseNormalStyleForList
		{
			get
			{
				return base.GetElement<UseNormalStyleForList>(50);
			}
			set
			{
				base.SetElement<UseNormalStyleForList>(50, value);
			}
		}

		// Token: 0x17009592 RID: 38290
		// (get) Token: 0x0601ABD2 RID: 109522 RVA: 0x00366CC1 File Offset: 0x00364EC1
		// (set) Token: 0x0601ABD3 RID: 109523 RVA: 0x00366CCB File Offset: 0x00364ECB
		public DoNotUseIndentAsNumberingTabStop DoNotUseIndentAsNumberingTabStop
		{
			get
			{
				return base.GetElement<DoNotUseIndentAsNumberingTabStop>(51);
			}
			set
			{
				base.SetElement<DoNotUseIndentAsNumberingTabStop>(51, value);
			}
		}

		// Token: 0x17009593 RID: 38291
		// (get) Token: 0x0601ABD4 RID: 109524 RVA: 0x00366CD6 File Offset: 0x00364ED6
		// (set) Token: 0x0601ABD5 RID: 109525 RVA: 0x00366CE0 File Offset: 0x00364EE0
		public UseAltKinsokuLineBreakRules UseAltKinsokuLineBreakRules
		{
			get
			{
				return base.GetElement<UseAltKinsokuLineBreakRules>(52);
			}
			set
			{
				base.SetElement<UseAltKinsokuLineBreakRules>(52, value);
			}
		}

		// Token: 0x17009594 RID: 38292
		// (get) Token: 0x0601ABD6 RID: 109526 RVA: 0x00366CEB File Offset: 0x00364EEB
		// (set) Token: 0x0601ABD7 RID: 109527 RVA: 0x00366CF5 File Offset: 0x00364EF5
		public AllowSpaceOfSameStyleInTable AllowSpaceOfSameStyleInTable
		{
			get
			{
				return base.GetElement<AllowSpaceOfSameStyleInTable>(53);
			}
			set
			{
				base.SetElement<AllowSpaceOfSameStyleInTable>(53, value);
			}
		}

		// Token: 0x17009595 RID: 38293
		// (get) Token: 0x0601ABD8 RID: 109528 RVA: 0x00366D00 File Offset: 0x00364F00
		// (set) Token: 0x0601ABD9 RID: 109529 RVA: 0x00366D0A File Offset: 0x00364F0A
		public DoNotSuppressIndentation DoNotSuppressIndentation
		{
			get
			{
				return base.GetElement<DoNotSuppressIndentation>(54);
			}
			set
			{
				base.SetElement<DoNotSuppressIndentation>(54, value);
			}
		}

		// Token: 0x17009596 RID: 38294
		// (get) Token: 0x0601ABDA RID: 109530 RVA: 0x00366D15 File Offset: 0x00364F15
		// (set) Token: 0x0601ABDB RID: 109531 RVA: 0x00366D1F File Offset: 0x00364F1F
		public DoNotAutofitConstrainedTables DoNotAutofitConstrainedTables
		{
			get
			{
				return base.GetElement<DoNotAutofitConstrainedTables>(55);
			}
			set
			{
				base.SetElement<DoNotAutofitConstrainedTables>(55, value);
			}
		}

		// Token: 0x17009597 RID: 38295
		// (get) Token: 0x0601ABDC RID: 109532 RVA: 0x00366D2A File Offset: 0x00364F2A
		// (set) Token: 0x0601ABDD RID: 109533 RVA: 0x00366D34 File Offset: 0x00364F34
		public AutofitToFirstFixedWidthCell AutofitToFirstFixedWidthCell
		{
			get
			{
				return base.GetElement<AutofitToFirstFixedWidthCell>(56);
			}
			set
			{
				base.SetElement<AutofitToFirstFixedWidthCell>(56, value);
			}
		}

		// Token: 0x17009598 RID: 38296
		// (get) Token: 0x0601ABDE RID: 109534 RVA: 0x00366D3F File Offset: 0x00364F3F
		// (set) Token: 0x0601ABDF RID: 109535 RVA: 0x00366D49 File Offset: 0x00364F49
		public UnderlineTabInNumberingList UnderlineTabInNumberingList
		{
			get
			{
				return base.GetElement<UnderlineTabInNumberingList>(57);
			}
			set
			{
				base.SetElement<UnderlineTabInNumberingList>(57, value);
			}
		}

		// Token: 0x17009599 RID: 38297
		// (get) Token: 0x0601ABE0 RID: 109536 RVA: 0x00366D54 File Offset: 0x00364F54
		// (set) Token: 0x0601ABE1 RID: 109537 RVA: 0x00366D5E File Offset: 0x00364F5E
		public DisplayHangulFixedWidth DisplayHangulFixedWidth
		{
			get
			{
				return base.GetElement<DisplayHangulFixedWidth>(58);
			}
			set
			{
				base.SetElement<DisplayHangulFixedWidth>(58, value);
			}
		}

		// Token: 0x1700959A RID: 38298
		// (get) Token: 0x0601ABE2 RID: 109538 RVA: 0x00366D69 File Offset: 0x00364F69
		// (set) Token: 0x0601ABE3 RID: 109539 RVA: 0x00366D73 File Offset: 0x00364F73
		public SplitPageBreakAndParagraphMark SplitPageBreakAndParagraphMark
		{
			get
			{
				return base.GetElement<SplitPageBreakAndParagraphMark>(59);
			}
			set
			{
				base.SetElement<SplitPageBreakAndParagraphMark>(59, value);
			}
		}

		// Token: 0x1700959B RID: 38299
		// (get) Token: 0x0601ABE4 RID: 109540 RVA: 0x00366D7E File Offset: 0x00364F7E
		// (set) Token: 0x0601ABE5 RID: 109541 RVA: 0x00366D88 File Offset: 0x00364F88
		public DoNotVerticallyAlignCellWithShape DoNotVerticallyAlignCellWithShape
		{
			get
			{
				return base.GetElement<DoNotVerticallyAlignCellWithShape>(60);
			}
			set
			{
				base.SetElement<DoNotVerticallyAlignCellWithShape>(60, value);
			}
		}

		// Token: 0x1700959C RID: 38300
		// (get) Token: 0x0601ABE6 RID: 109542 RVA: 0x00366D93 File Offset: 0x00364F93
		// (set) Token: 0x0601ABE7 RID: 109543 RVA: 0x00366D9D File Offset: 0x00364F9D
		public DoNotBreakConstrainedForcedTable DoNotBreakConstrainedForcedTable
		{
			get
			{
				return base.GetElement<DoNotBreakConstrainedForcedTable>(61);
			}
			set
			{
				base.SetElement<DoNotBreakConstrainedForcedTable>(61, value);
			}
		}

		// Token: 0x1700959D RID: 38301
		// (get) Token: 0x0601ABE8 RID: 109544 RVA: 0x00366DA8 File Offset: 0x00364FA8
		// (set) Token: 0x0601ABE9 RID: 109545 RVA: 0x00366DB2 File Offset: 0x00364FB2
		public DoNotVerticallyAlignInTextBox DoNotVerticallyAlignInTextBox
		{
			get
			{
				return base.GetElement<DoNotVerticallyAlignInTextBox>(62);
			}
			set
			{
				base.SetElement<DoNotVerticallyAlignInTextBox>(62, value);
			}
		}

		// Token: 0x1700959E RID: 38302
		// (get) Token: 0x0601ABEA RID: 109546 RVA: 0x00366DBD File Offset: 0x00364FBD
		// (set) Token: 0x0601ABEB RID: 109547 RVA: 0x00366DC7 File Offset: 0x00364FC7
		public UseAnsiKerningPairs UseAnsiKerningPairs
		{
			get
			{
				return base.GetElement<UseAnsiKerningPairs>(63);
			}
			set
			{
				base.SetElement<UseAnsiKerningPairs>(63, value);
			}
		}

		// Token: 0x1700959F RID: 38303
		// (get) Token: 0x0601ABEC RID: 109548 RVA: 0x00366DD2 File Offset: 0x00364FD2
		// (set) Token: 0x0601ABED RID: 109549 RVA: 0x00366DDC File Offset: 0x00364FDC
		public CachedColumnBalance CachedColumnBalance
		{
			get
			{
				return base.GetElement<CachedColumnBalance>(64);
			}
			set
			{
				base.SetElement<CachedColumnBalance>(64, value);
			}
		}

		// Token: 0x0601ABEE RID: 109550 RVA: 0x00366DE7 File Offset: 0x00364FE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Compatibility>(deep);
		}

		// Token: 0x0400AE21 RID: 44577
		private const string tagName = "compat";

		// Token: 0x0400AE22 RID: 44578
		private const byte tagNsId = 23;

		// Token: 0x0400AE23 RID: 44579
		internal const int ElementTypeIdConst = 12038;

		// Token: 0x0400AE24 RID: 44580
		private static readonly string[] eleTagNames = new string[]
		{
			"useSingleBorderforContiguousCells", "wpJustification", "noTabHangInd", "noLeading", "spaceForUL", "noColumnBalance", "balanceSingleByteDoubleByteWidth", "noExtraLineSpacing", "doNotLeaveBackslashAlone", "ulTrailSpace",
			"doNotExpandShiftReturn", "spacingInWholePoints", "lineWrapLikeWord6", "printBodyTextBeforeHeader", "printColBlack", "wpSpaceWidth", "showBreaksInFrames", "subFontBySize", "suppressBottomSpacing", "suppressTopSpacing",
			"suppressSpacingAtTopOfPage", "suppressTopSpacingWP", "suppressSpBfAfterPgBrk", "swapBordersFacingPages", "convMailMergeEsc", "truncateFontHeightsLikeWP6", "mwSmallCaps", "usePrinterMetrics", "doNotSuppressParagraphBorders", "wrapTrailSpaces",
			"footnoteLayoutLikeWW8", "shapeLayoutLikeWW8", "alignTablesRowByRow", "forgetLastTabAlignment", "adjustLineHeightInTable", "autoSpaceLikeWord95", "noSpaceRaiseLower", "doNotUseHTMLParagraphAutoSpacing", "layoutRawTableWidth", "layoutTableRowsApart",
			"useWord97LineBreakRules", "doNotBreakWrappedTables", "doNotSnapToGridInCell", "selectFldWithFirstOrLastChar", "applyBreakingRules", "doNotWrapTextWithPunct", "doNotUseEastAsianBreakRules", "useWord2002TableStyleRules", "growAutofit", "useFELayout",
			"useNormalStyleForList", "doNotUseIndentAsNumberingTabStop", "useAltKinsokuLineBreakRules", "allowSpaceOfSameStyleInTable", "doNotSuppressIndentation", "doNotAutofitConstrainedTables", "autofitToFirstFixedWidthCell", "underlineTabInNumList", "displayHangulFixedWidth", "splitPgBreakAndParaMark",
			"doNotVertAlignCellWithSp", "doNotBreakConstrainedForcedTable", "doNotVertAlignInTxbx", "useAnsiKerningPairs", "cachedColBalance", "compatSetting"
		};

		// Token: 0x0400AE25 RID: 44581
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
