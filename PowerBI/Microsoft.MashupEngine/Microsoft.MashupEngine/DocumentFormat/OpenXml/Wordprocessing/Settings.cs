using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.CustomXmlSchemaReferences;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F18 RID: 12056
	[ChildElementInfo(typeof(DocumentType))]
	[ChildElementInfo(typeof(NoLineBreaksBeforeKinsoku))]
	[ChildElementInfo(typeof(PrintTwoOnOne))]
	[ChildElementInfo(typeof(NoPunctuationKerning))]
	[ChildElementInfo(typeof(CharacterSpacingControl))]
	[ChildElementInfo(typeof(DrawingGridHorizontalOrigin))]
	[ChildElementInfo(typeof(WriteProtection))]
	[ChildElementInfo(typeof(View))]
	[ChildElementInfo(typeof(Zoom))]
	[ChildElementInfo(typeof(RemovePersonalInformation))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DoNotDisplayPageBoundaries))]
	[ChildElementInfo(typeof(DisplayBackgroundShape))]
	[ChildElementInfo(typeof(PrintPostScriptOverText))]
	[ChildElementInfo(typeof(PrintFractionalCharacterWidth))]
	[ChildElementInfo(typeof(PrintFormsData))]
	[ChildElementInfo(typeof(EmbedTrueTypeFonts))]
	[ChildElementInfo(typeof(EmbedSystemFonts))]
	[ChildElementInfo(typeof(SaveSubsetFonts))]
	[ChildElementInfo(typeof(SaveFormsData))]
	[ChildElementInfo(typeof(MirrorMargins))]
	[ChildElementInfo(typeof(AlignBorderAndEdges))]
	[ChildElementInfo(typeof(BordersDoNotSurroundHeader))]
	[ChildElementInfo(typeof(BordersDoNotSurroundFooter))]
	[ChildElementInfo(typeof(GutterAtTop))]
	[ChildElementInfo(typeof(HideSpellingErrors))]
	[ChildElementInfo(typeof(HideGrammaticalErrors))]
	[ChildElementInfo(typeof(ActiveWritingStyle))]
	[ChildElementInfo(typeof(ProofState))]
	[ChildElementInfo(typeof(FormsDesign))]
	[ChildElementInfo(typeof(AttachedTemplate))]
	[ChildElementInfo(typeof(LinkStyles))]
	[ChildElementInfo(typeof(StylePaneFormatFilter))]
	[ChildElementInfo(typeof(StylePaneSortMethods))]
	[ChildElementInfo(typeof(MailMerge))]
	[ChildElementInfo(typeof(RevisionView))]
	[ChildElementInfo(typeof(TrackRevisions))]
	[ChildElementInfo(typeof(DoNotTrackMoves))]
	[ChildElementInfo(typeof(DoNotTrackFormatting))]
	[ChildElementInfo(typeof(DocumentProtection))]
	[ChildElementInfo(typeof(AutoFormatOverride))]
	[ChildElementInfo(typeof(StyleLockThemesPart))]
	[ChildElementInfo(typeof(StyleLockStylesPart))]
	[ChildElementInfo(typeof(DefaultTabStop))]
	[ChildElementInfo(typeof(AutoHyphenation))]
	[ChildElementInfo(typeof(ConsecutiveHyphenLimit))]
	[ChildElementInfo(typeof(HyphenationZone))]
	[ChildElementInfo(typeof(DoNotHyphenateCaps))]
	[ChildElementInfo(typeof(ShowEnvelope))]
	[ChildElementInfo(typeof(SummaryLength))]
	[ChildElementInfo(typeof(ClickAndTypeStyle))]
	[ChildElementInfo(typeof(DefaultTableStyle))]
	[ChildElementInfo(typeof(EvenAndOddHeaders))]
	[ChildElementInfo(typeof(BookFoldReversePrinting))]
	[ChildElementInfo(typeof(BookFoldPrinting))]
	[ChildElementInfo(typeof(BookFoldPrintingSheets))]
	[ChildElementInfo(typeof(DrawingGridHorizontalSpacing))]
	[ChildElementInfo(typeof(DrawingGridVerticalSpacing))]
	[ChildElementInfo(typeof(DisplayHorizontalDrawingGrid))]
	[ChildElementInfo(typeof(DisplayVerticalDrawingGrid))]
	[ChildElementInfo(typeof(DoNotUseMarginsForDrawingGridOrigin))]
	[ChildElementInfo(typeof(DrawingGridVerticalOrigin))]
	[ChildElementInfo(typeof(DoNotShadeFormData))]
	[ChildElementInfo(typeof(RemoveDateAndTime))]
	[ChildElementInfo(typeof(StrictFirstAndLastChars))]
	[ChildElementInfo(typeof(NoLineBreaksAfterKinsoku))]
	[ChildElementInfo(typeof(SavePreviewPicture))]
	[ChildElementInfo(typeof(DoNotValidateAgainstSchema))]
	[ChildElementInfo(typeof(SaveInvalidXml))]
	[ChildElementInfo(typeof(IgnoreMixedContent))]
	[ChildElementInfo(typeof(AlwaysShowPlaceholderText))]
	[ChildElementInfo(typeof(DoNotDemarcateInvalidXml))]
	[ChildElementInfo(typeof(SaveXmlDataOnly))]
	[ChildElementInfo(typeof(UseXsltWhenSaving))]
	[ChildElementInfo(typeof(SaveThroughXslt))]
	[ChildElementInfo(typeof(ShowXmlTags))]
	[ChildElementInfo(typeof(AlwaysMergeEmptyNamespace))]
	[ChildElementInfo(typeof(UpdateFieldsOnOpen))]
	[ChildElementInfo(typeof(HeaderShapeDefaults))]
	[ChildElementInfo(typeof(FootnoteDocumentWideProperties))]
	[ChildElementInfo(typeof(EndnoteDocumentWideProperties))]
	[ChildElementInfo(typeof(Compatibility))]
	[ChildElementInfo(typeof(DocumentVariables))]
	[ChildElementInfo(typeof(Rsids))]
	[ChildElementInfo(typeof(MathProperties))]
	[ChildElementInfo(typeof(UICompatibleWith97To2003))]
	[ChildElementInfo(typeof(AttachedSchema))]
	[ChildElementInfo(typeof(ThemeFontLanguages))]
	[ChildElementInfo(typeof(ColorSchemeMapping))]
	[ChildElementInfo(typeof(DoNotIncludeSubdocsInStats))]
	[ChildElementInfo(typeof(DoNotAutoCompressPictures))]
	[ChildElementInfo(typeof(ForceUpgrade))]
	[ChildElementInfo(typeof(Captions))]
	[ChildElementInfo(typeof(ReadModeInkLockDown))]
	[ChildElementInfo(typeof(SmartTagType))]
	[ChildElementInfo(typeof(SchemaLibrary))]
	[ChildElementInfo(typeof(ShapeDefaults))]
	[ChildElementInfo(typeof(DoNotEmbedSmartTags))]
	[ChildElementInfo(typeof(DecimalSymbol))]
	[ChildElementInfo(typeof(ListSeparator))]
	[ChildElementInfo(typeof(DocumentId), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DiscardImageEditingData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DefaultImageDpi), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConflictMode), FileFormatVersions.Office2010)]
	internal class Settings : OpenXmlPartRootElement
	{
		// Token: 0x17008E54 RID: 36436
		// (get) Token: 0x06019C33 RID: 105523 RVA: 0x002A49A3 File Offset: 0x002A2BA3
		public override string LocalName
		{
			get
			{
				return "settings";
			}
		}

		// Token: 0x17008E55 RID: 36437
		// (get) Token: 0x06019C34 RID: 105524 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E56 RID: 36438
		// (get) Token: 0x06019C35 RID: 105525 RVA: 0x00355662 File Offset: 0x00353862
		internal override int ElementTypeId
		{
			get
			{
				return 11697;
			}
		}

		// Token: 0x06019C36 RID: 105526 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019C37 RID: 105527 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Settings(DocumentSettingsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019C38 RID: 105528 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DocumentSettingsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E57 RID: 36439
		// (get) Token: 0x06019C39 RID: 105529 RVA: 0x00355669 File Offset: 0x00353869
		// (set) Token: 0x06019C3A RID: 105530 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DocumentSettingsPart DocumentSettingsPart
		{
			get
			{
				return base.OpenXmlPart as DocumentSettingsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019C3B RID: 105531 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Settings(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C3C RID: 105532 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Settings(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C3D RID: 105533 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Settings(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019C3E RID: 105534 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Settings()
		{
		}

		// Token: 0x06019C3F RID: 105535 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DocumentSettingsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019C40 RID: 105536 RVA: 0x00355678 File Offset: 0x00353878
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "writeProtection" == name)
			{
				return new WriteProtection();
			}
			if (23 == namespaceId && "view" == name)
			{
				return new View();
			}
			if (23 == namespaceId && "zoom" == name)
			{
				return new Zoom();
			}
			if (23 == namespaceId && "removePersonalInformation" == name)
			{
				return new RemovePersonalInformation();
			}
			if (23 == namespaceId && "removeDateAndTime" == name)
			{
				return new RemoveDateAndTime();
			}
			if (23 == namespaceId && "doNotDisplayPageBoundaries" == name)
			{
				return new DoNotDisplayPageBoundaries();
			}
			if (23 == namespaceId && "displayBackgroundShape" == name)
			{
				return new DisplayBackgroundShape();
			}
			if (23 == namespaceId && "printPostScriptOverText" == name)
			{
				return new PrintPostScriptOverText();
			}
			if (23 == namespaceId && "printFractionalCharacterWidth" == name)
			{
				return new PrintFractionalCharacterWidth();
			}
			if (23 == namespaceId && "printFormsData" == name)
			{
				return new PrintFormsData();
			}
			if (23 == namespaceId && "embedTrueTypeFonts" == name)
			{
				return new EmbedTrueTypeFonts();
			}
			if (23 == namespaceId && "embedSystemFonts" == name)
			{
				return new EmbedSystemFonts();
			}
			if (23 == namespaceId && "saveSubsetFonts" == name)
			{
				return new SaveSubsetFonts();
			}
			if (23 == namespaceId && "saveFormsData" == name)
			{
				return new SaveFormsData();
			}
			if (23 == namespaceId && "mirrorMargins" == name)
			{
				return new MirrorMargins();
			}
			if (23 == namespaceId && "alignBordersAndEdges" == name)
			{
				return new AlignBorderAndEdges();
			}
			if (23 == namespaceId && "bordersDoNotSurroundHeader" == name)
			{
				return new BordersDoNotSurroundHeader();
			}
			if (23 == namespaceId && "bordersDoNotSurroundFooter" == name)
			{
				return new BordersDoNotSurroundFooter();
			}
			if (23 == namespaceId && "gutterAtTop" == name)
			{
				return new GutterAtTop();
			}
			if (23 == namespaceId && "hideSpellingErrors" == name)
			{
				return new HideSpellingErrors();
			}
			if (23 == namespaceId && "hideGrammaticalErrors" == name)
			{
				return new HideGrammaticalErrors();
			}
			if (23 == namespaceId && "activeWritingStyle" == name)
			{
				return new ActiveWritingStyle();
			}
			if (23 == namespaceId && "proofState" == name)
			{
				return new ProofState();
			}
			if (23 == namespaceId && "formsDesign" == name)
			{
				return new FormsDesign();
			}
			if (23 == namespaceId && "attachedTemplate" == name)
			{
				return new AttachedTemplate();
			}
			if (23 == namespaceId && "linkStyles" == name)
			{
				return new LinkStyles();
			}
			if (23 == namespaceId && "stylePaneFormatFilter" == name)
			{
				return new StylePaneFormatFilter();
			}
			if (23 == namespaceId && "stylePaneSortMethod" == name)
			{
				return new StylePaneSortMethods();
			}
			if (23 == namespaceId && "documentType" == name)
			{
				return new DocumentType();
			}
			if (23 == namespaceId && "mailMerge" == name)
			{
				return new MailMerge();
			}
			if (23 == namespaceId && "revisionView" == name)
			{
				return new RevisionView();
			}
			if (23 == namespaceId && "trackRevisions" == name)
			{
				return new TrackRevisions();
			}
			if (23 == namespaceId && "doNotTrackMoves" == name)
			{
				return new DoNotTrackMoves();
			}
			if (23 == namespaceId && "doNotTrackFormatting" == name)
			{
				return new DoNotTrackFormatting();
			}
			if (23 == namespaceId && "documentProtection" == name)
			{
				return new DocumentProtection();
			}
			if (23 == namespaceId && "autoFormatOverride" == name)
			{
				return new AutoFormatOverride();
			}
			if (23 == namespaceId && "styleLockTheme" == name)
			{
				return new StyleLockThemesPart();
			}
			if (23 == namespaceId && "styleLockQFSet" == name)
			{
				return new StyleLockStylesPart();
			}
			if (23 == namespaceId && "defaultTabStop" == name)
			{
				return new DefaultTabStop();
			}
			if (23 == namespaceId && "autoHyphenation" == name)
			{
				return new AutoHyphenation();
			}
			if (23 == namespaceId && "consecutiveHyphenLimit" == name)
			{
				return new ConsecutiveHyphenLimit();
			}
			if (23 == namespaceId && "hyphenationZone" == name)
			{
				return new HyphenationZone();
			}
			if (23 == namespaceId && "doNotHyphenateCaps" == name)
			{
				return new DoNotHyphenateCaps();
			}
			if (23 == namespaceId && "showEnvelope" == name)
			{
				return new ShowEnvelope();
			}
			if (23 == namespaceId && "summaryLength" == name)
			{
				return new SummaryLength();
			}
			if (23 == namespaceId && "clickAndTypeStyle" == name)
			{
				return new ClickAndTypeStyle();
			}
			if (23 == namespaceId && "defaultTableStyle" == name)
			{
				return new DefaultTableStyle();
			}
			if (23 == namespaceId && "evenAndOddHeaders" == name)
			{
				return new EvenAndOddHeaders();
			}
			if (23 == namespaceId && "bookFoldRevPrinting" == name)
			{
				return new BookFoldReversePrinting();
			}
			if (23 == namespaceId && "bookFoldPrinting" == name)
			{
				return new BookFoldPrinting();
			}
			if (23 == namespaceId && "bookFoldPrintingSheets" == name)
			{
				return new BookFoldPrintingSheets();
			}
			if (23 == namespaceId && "drawingGridHorizontalSpacing" == name)
			{
				return new DrawingGridHorizontalSpacing();
			}
			if (23 == namespaceId && "drawingGridVerticalSpacing" == name)
			{
				return new DrawingGridVerticalSpacing();
			}
			if (23 == namespaceId && "displayHorizontalDrawingGridEvery" == name)
			{
				return new DisplayHorizontalDrawingGrid();
			}
			if (23 == namespaceId && "displayVerticalDrawingGridEvery" == name)
			{
				return new DisplayVerticalDrawingGrid();
			}
			if (23 == namespaceId && "doNotUseMarginsForDrawingGridOrigin" == name)
			{
				return new DoNotUseMarginsForDrawingGridOrigin();
			}
			if (23 == namespaceId && "drawingGridHorizontalOrigin" == name)
			{
				return new DrawingGridHorizontalOrigin();
			}
			if (23 == namespaceId && "drawingGridVerticalOrigin" == name)
			{
				return new DrawingGridVerticalOrigin();
			}
			if (23 == namespaceId && "doNotShadeFormData" == name)
			{
				return new DoNotShadeFormData();
			}
			if (23 == namespaceId && "noPunctuationKerning" == name)
			{
				return new NoPunctuationKerning();
			}
			if (23 == namespaceId && "characterSpacingControl" == name)
			{
				return new CharacterSpacingControl();
			}
			if (23 == namespaceId && "printTwoOnOne" == name)
			{
				return new PrintTwoOnOne();
			}
			if (23 == namespaceId && "strictFirstAndLastChars" == name)
			{
				return new StrictFirstAndLastChars();
			}
			if (23 == namespaceId && "noLineBreaksAfter" == name)
			{
				return new NoLineBreaksAfterKinsoku();
			}
			if (23 == namespaceId && "noLineBreaksBefore" == name)
			{
				return new NoLineBreaksBeforeKinsoku();
			}
			if (23 == namespaceId && "savePreviewPicture" == name)
			{
				return new SavePreviewPicture();
			}
			if (23 == namespaceId && "doNotValidateAgainstSchema" == name)
			{
				return new DoNotValidateAgainstSchema();
			}
			if (23 == namespaceId && "saveInvalidXml" == name)
			{
				return new SaveInvalidXml();
			}
			if (23 == namespaceId && "ignoreMixedContent" == name)
			{
				return new IgnoreMixedContent();
			}
			if (23 == namespaceId && "alwaysShowPlaceholderText" == name)
			{
				return new AlwaysShowPlaceholderText();
			}
			if (23 == namespaceId && "doNotDemarcateInvalidXml" == name)
			{
				return new DoNotDemarcateInvalidXml();
			}
			if (23 == namespaceId && "saveXmlDataOnly" == name)
			{
				return new SaveXmlDataOnly();
			}
			if (23 == namespaceId && "useXSLTWhenSaving" == name)
			{
				return new UseXsltWhenSaving();
			}
			if (23 == namespaceId && "saveThroughXslt" == name)
			{
				return new SaveThroughXslt();
			}
			if (23 == namespaceId && "showXMLTags" == name)
			{
				return new ShowXmlTags();
			}
			if (23 == namespaceId && "alwaysMergeEmptyNamespace" == name)
			{
				return new AlwaysMergeEmptyNamespace();
			}
			if (23 == namespaceId && "updateFields" == name)
			{
				return new UpdateFieldsOnOpen();
			}
			if (23 == namespaceId && "hdrShapeDefaults" == name)
			{
				return new HeaderShapeDefaults();
			}
			if (23 == namespaceId && "footnotePr" == name)
			{
				return new FootnoteDocumentWideProperties();
			}
			if (23 == namespaceId && "endnotePr" == name)
			{
				return new EndnoteDocumentWideProperties();
			}
			if (23 == namespaceId && "compat" == name)
			{
				return new Compatibility();
			}
			if (23 == namespaceId && "docVars" == name)
			{
				return new DocumentVariables();
			}
			if (23 == namespaceId && "rsids" == name)
			{
				return new Rsids();
			}
			if (21 == namespaceId && "mathPr" == name)
			{
				return new MathProperties();
			}
			if (23 == namespaceId && "uiCompat97To2003" == name)
			{
				return new UICompatibleWith97To2003();
			}
			if (23 == namespaceId && "attachedSchema" == name)
			{
				return new AttachedSchema();
			}
			if (23 == namespaceId && "themeFontLang" == name)
			{
				return new ThemeFontLanguages();
			}
			if (23 == namespaceId && "clrSchemeMapping" == name)
			{
				return new ColorSchemeMapping();
			}
			if (23 == namespaceId && "doNotIncludeSubdocsInStats" == name)
			{
				return new DoNotIncludeSubdocsInStats();
			}
			if (23 == namespaceId && "doNotAutoCompressPictures" == name)
			{
				return new DoNotAutoCompressPictures();
			}
			if (23 == namespaceId && "forceUpgrade" == name)
			{
				return new ForceUpgrade();
			}
			if (23 == namespaceId && "captions" == name)
			{
				return new Captions();
			}
			if (23 == namespaceId && "readModeInkLockDown" == name)
			{
				return new ReadModeInkLockDown();
			}
			if (23 == namespaceId && "smartTagType" == name)
			{
				return new SmartTagType();
			}
			if (25 == namespaceId && "schemaLibrary" == name)
			{
				return new SchemaLibrary();
			}
			if (23 == namespaceId && "shapeDefaults" == name)
			{
				return new ShapeDefaults();
			}
			if (23 == namespaceId && "doNotEmbedSmartTags" == name)
			{
				return new DoNotEmbedSmartTags();
			}
			if (23 == namespaceId && "decimalSymbol" == name)
			{
				return new DecimalSymbol();
			}
			if (23 == namespaceId && "listSeparator" == name)
			{
				return new ListSeparator();
			}
			if (52 == namespaceId && "docId" == name)
			{
				return new DocumentId();
			}
			if (52 == namespaceId && "discardImageEditingData" == name)
			{
				return new DiscardImageEditingData();
			}
			if (52 == namespaceId && "defaultImageDpi" == name)
			{
				return new DefaultImageDpi();
			}
			if (52 == namespaceId && "conflictMode" == name)
			{
				return new ConflictMode();
			}
			return null;
		}

		// Token: 0x17008E58 RID: 36440
		// (get) Token: 0x06019C41 RID: 105537 RVA: 0x0035602E File Offset: 0x0035422E
		internal override string[] ElementTagNames
		{
			get
			{
				return Settings.eleTagNames;
			}
		}

		// Token: 0x17008E59 RID: 36441
		// (get) Token: 0x06019C42 RID: 105538 RVA: 0x00356035 File Offset: 0x00354235
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Settings.eleNamespaceIds;
			}
		}

		// Token: 0x17008E5A RID: 36442
		// (get) Token: 0x06019C43 RID: 105539 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E5B RID: 36443
		// (get) Token: 0x06019C44 RID: 105540 RVA: 0x0035603C File Offset: 0x0035423C
		// (set) Token: 0x06019C45 RID: 105541 RVA: 0x00356045 File Offset: 0x00354245
		public WriteProtection WriteProtection
		{
			get
			{
				return base.GetElement<WriteProtection>(0);
			}
			set
			{
				base.SetElement<WriteProtection>(0, value);
			}
		}

		// Token: 0x17008E5C RID: 36444
		// (get) Token: 0x06019C46 RID: 105542 RVA: 0x0035604F File Offset: 0x0035424F
		// (set) Token: 0x06019C47 RID: 105543 RVA: 0x00356058 File Offset: 0x00354258
		public View View
		{
			get
			{
				return base.GetElement<View>(1);
			}
			set
			{
				base.SetElement<View>(1, value);
			}
		}

		// Token: 0x17008E5D RID: 36445
		// (get) Token: 0x06019C48 RID: 105544 RVA: 0x00356062 File Offset: 0x00354262
		// (set) Token: 0x06019C49 RID: 105545 RVA: 0x0035606B File Offset: 0x0035426B
		public Zoom Zoom
		{
			get
			{
				return base.GetElement<Zoom>(2);
			}
			set
			{
				base.SetElement<Zoom>(2, value);
			}
		}

		// Token: 0x17008E5E RID: 36446
		// (get) Token: 0x06019C4A RID: 105546 RVA: 0x00356075 File Offset: 0x00354275
		// (set) Token: 0x06019C4B RID: 105547 RVA: 0x0035607E File Offset: 0x0035427E
		public RemovePersonalInformation RemovePersonalInformation
		{
			get
			{
				return base.GetElement<RemovePersonalInformation>(3);
			}
			set
			{
				base.SetElement<RemovePersonalInformation>(3, value);
			}
		}

		// Token: 0x17008E5F RID: 36447
		// (get) Token: 0x06019C4C RID: 105548 RVA: 0x00356088 File Offset: 0x00354288
		// (set) Token: 0x06019C4D RID: 105549 RVA: 0x00356091 File Offset: 0x00354291
		public RemoveDateAndTime RemoveDateAndTime
		{
			get
			{
				return base.GetElement<RemoveDateAndTime>(4);
			}
			set
			{
				base.SetElement<RemoveDateAndTime>(4, value);
			}
		}

		// Token: 0x17008E60 RID: 36448
		// (get) Token: 0x06019C4E RID: 105550 RVA: 0x0035609B File Offset: 0x0035429B
		// (set) Token: 0x06019C4F RID: 105551 RVA: 0x003560A4 File Offset: 0x003542A4
		public DoNotDisplayPageBoundaries DoNotDisplayPageBoundaries
		{
			get
			{
				return base.GetElement<DoNotDisplayPageBoundaries>(5);
			}
			set
			{
				base.SetElement<DoNotDisplayPageBoundaries>(5, value);
			}
		}

		// Token: 0x17008E61 RID: 36449
		// (get) Token: 0x06019C50 RID: 105552 RVA: 0x003560AE File Offset: 0x003542AE
		// (set) Token: 0x06019C51 RID: 105553 RVA: 0x003560B7 File Offset: 0x003542B7
		public DisplayBackgroundShape DisplayBackgroundShape
		{
			get
			{
				return base.GetElement<DisplayBackgroundShape>(6);
			}
			set
			{
				base.SetElement<DisplayBackgroundShape>(6, value);
			}
		}

		// Token: 0x17008E62 RID: 36450
		// (get) Token: 0x06019C52 RID: 105554 RVA: 0x003560C1 File Offset: 0x003542C1
		// (set) Token: 0x06019C53 RID: 105555 RVA: 0x003560CA File Offset: 0x003542CA
		public PrintPostScriptOverText PrintPostScriptOverText
		{
			get
			{
				return base.GetElement<PrintPostScriptOverText>(7);
			}
			set
			{
				base.SetElement<PrintPostScriptOverText>(7, value);
			}
		}

		// Token: 0x17008E63 RID: 36451
		// (get) Token: 0x06019C54 RID: 105556 RVA: 0x003560D4 File Offset: 0x003542D4
		// (set) Token: 0x06019C55 RID: 105557 RVA: 0x003560DD File Offset: 0x003542DD
		public PrintFractionalCharacterWidth PrintFractionalCharacterWidth
		{
			get
			{
				return base.GetElement<PrintFractionalCharacterWidth>(8);
			}
			set
			{
				base.SetElement<PrintFractionalCharacterWidth>(8, value);
			}
		}

		// Token: 0x17008E64 RID: 36452
		// (get) Token: 0x06019C56 RID: 105558 RVA: 0x003560E7 File Offset: 0x003542E7
		// (set) Token: 0x06019C57 RID: 105559 RVA: 0x003560F1 File Offset: 0x003542F1
		public PrintFormsData PrintFormsData
		{
			get
			{
				return base.GetElement<PrintFormsData>(9);
			}
			set
			{
				base.SetElement<PrintFormsData>(9, value);
			}
		}

		// Token: 0x17008E65 RID: 36453
		// (get) Token: 0x06019C58 RID: 105560 RVA: 0x003560FC File Offset: 0x003542FC
		// (set) Token: 0x06019C59 RID: 105561 RVA: 0x00356106 File Offset: 0x00354306
		public EmbedTrueTypeFonts EmbedTrueTypeFonts
		{
			get
			{
				return base.GetElement<EmbedTrueTypeFonts>(10);
			}
			set
			{
				base.SetElement<EmbedTrueTypeFonts>(10, value);
			}
		}

		// Token: 0x17008E66 RID: 36454
		// (get) Token: 0x06019C5A RID: 105562 RVA: 0x00356111 File Offset: 0x00354311
		// (set) Token: 0x06019C5B RID: 105563 RVA: 0x0035611B File Offset: 0x0035431B
		public EmbedSystemFonts EmbedSystemFonts
		{
			get
			{
				return base.GetElement<EmbedSystemFonts>(11);
			}
			set
			{
				base.SetElement<EmbedSystemFonts>(11, value);
			}
		}

		// Token: 0x17008E67 RID: 36455
		// (get) Token: 0x06019C5C RID: 105564 RVA: 0x00356126 File Offset: 0x00354326
		// (set) Token: 0x06019C5D RID: 105565 RVA: 0x00356130 File Offset: 0x00354330
		public SaveSubsetFonts SaveSubsetFonts
		{
			get
			{
				return base.GetElement<SaveSubsetFonts>(12);
			}
			set
			{
				base.SetElement<SaveSubsetFonts>(12, value);
			}
		}

		// Token: 0x17008E68 RID: 36456
		// (get) Token: 0x06019C5E RID: 105566 RVA: 0x0035613B File Offset: 0x0035433B
		// (set) Token: 0x06019C5F RID: 105567 RVA: 0x00356145 File Offset: 0x00354345
		public SaveFormsData SaveFormsData
		{
			get
			{
				return base.GetElement<SaveFormsData>(13);
			}
			set
			{
				base.SetElement<SaveFormsData>(13, value);
			}
		}

		// Token: 0x17008E69 RID: 36457
		// (get) Token: 0x06019C60 RID: 105568 RVA: 0x00356150 File Offset: 0x00354350
		// (set) Token: 0x06019C61 RID: 105569 RVA: 0x0035615A File Offset: 0x0035435A
		public MirrorMargins MirrorMargins
		{
			get
			{
				return base.GetElement<MirrorMargins>(14);
			}
			set
			{
				base.SetElement<MirrorMargins>(14, value);
			}
		}

		// Token: 0x17008E6A RID: 36458
		// (get) Token: 0x06019C62 RID: 105570 RVA: 0x00356165 File Offset: 0x00354365
		// (set) Token: 0x06019C63 RID: 105571 RVA: 0x0035616F File Offset: 0x0035436F
		public AlignBorderAndEdges AlignBorderAndEdges
		{
			get
			{
				return base.GetElement<AlignBorderAndEdges>(15);
			}
			set
			{
				base.SetElement<AlignBorderAndEdges>(15, value);
			}
		}

		// Token: 0x17008E6B RID: 36459
		// (get) Token: 0x06019C64 RID: 105572 RVA: 0x0035617A File Offset: 0x0035437A
		// (set) Token: 0x06019C65 RID: 105573 RVA: 0x00356184 File Offset: 0x00354384
		public BordersDoNotSurroundHeader BordersDoNotSurroundHeader
		{
			get
			{
				return base.GetElement<BordersDoNotSurroundHeader>(16);
			}
			set
			{
				base.SetElement<BordersDoNotSurroundHeader>(16, value);
			}
		}

		// Token: 0x17008E6C RID: 36460
		// (get) Token: 0x06019C66 RID: 105574 RVA: 0x0035618F File Offset: 0x0035438F
		// (set) Token: 0x06019C67 RID: 105575 RVA: 0x00356199 File Offset: 0x00354399
		public BordersDoNotSurroundFooter BordersDoNotSurroundFooter
		{
			get
			{
				return base.GetElement<BordersDoNotSurroundFooter>(17);
			}
			set
			{
				base.SetElement<BordersDoNotSurroundFooter>(17, value);
			}
		}

		// Token: 0x17008E6D RID: 36461
		// (get) Token: 0x06019C68 RID: 105576 RVA: 0x003561A4 File Offset: 0x003543A4
		// (set) Token: 0x06019C69 RID: 105577 RVA: 0x003561AE File Offset: 0x003543AE
		public GutterAtTop GutterAtTop
		{
			get
			{
				return base.GetElement<GutterAtTop>(18);
			}
			set
			{
				base.SetElement<GutterAtTop>(18, value);
			}
		}

		// Token: 0x17008E6E RID: 36462
		// (get) Token: 0x06019C6A RID: 105578 RVA: 0x003561B9 File Offset: 0x003543B9
		// (set) Token: 0x06019C6B RID: 105579 RVA: 0x003561C3 File Offset: 0x003543C3
		public HideSpellingErrors HideSpellingErrors
		{
			get
			{
				return base.GetElement<HideSpellingErrors>(19);
			}
			set
			{
				base.SetElement<HideSpellingErrors>(19, value);
			}
		}

		// Token: 0x17008E6F RID: 36463
		// (get) Token: 0x06019C6C RID: 105580 RVA: 0x003561CE File Offset: 0x003543CE
		// (set) Token: 0x06019C6D RID: 105581 RVA: 0x003561D8 File Offset: 0x003543D8
		public HideGrammaticalErrors HideGrammaticalErrors
		{
			get
			{
				return base.GetElement<HideGrammaticalErrors>(20);
			}
			set
			{
				base.SetElement<HideGrammaticalErrors>(20, value);
			}
		}

		// Token: 0x06019C6E RID: 105582 RVA: 0x003561E3 File Offset: 0x003543E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Settings>(deep);
		}

		// Token: 0x0400AA71 RID: 43633
		private const string tagName = "settings";

		// Token: 0x0400AA72 RID: 43634
		private const byte tagNsId = 23;

		// Token: 0x0400AA73 RID: 43635
		internal const int ElementTypeIdConst = 11697;

		// Token: 0x0400AA74 RID: 43636
		private static readonly string[] eleTagNames = new string[]
		{
			"writeProtection", "view", "zoom", "removePersonalInformation", "removeDateAndTime", "doNotDisplayPageBoundaries", "displayBackgroundShape", "printPostScriptOverText", "printFractionalCharacterWidth", "printFormsData",
			"embedTrueTypeFonts", "embedSystemFonts", "saveSubsetFonts", "saveFormsData", "mirrorMargins", "alignBordersAndEdges", "bordersDoNotSurroundHeader", "bordersDoNotSurroundFooter", "gutterAtTop", "hideSpellingErrors",
			"hideGrammaticalErrors", "activeWritingStyle", "proofState", "formsDesign", "attachedTemplate", "linkStyles", "stylePaneFormatFilter", "stylePaneSortMethod", "documentType", "mailMerge",
			"revisionView", "trackRevisions", "doNotTrackMoves", "doNotTrackFormatting", "documentProtection", "autoFormatOverride", "styleLockTheme", "styleLockQFSet", "defaultTabStop", "autoHyphenation",
			"consecutiveHyphenLimit", "hyphenationZone", "doNotHyphenateCaps", "showEnvelope", "summaryLength", "clickAndTypeStyle", "defaultTableStyle", "evenAndOddHeaders", "bookFoldRevPrinting", "bookFoldPrinting",
			"bookFoldPrintingSheets", "drawingGridHorizontalSpacing", "drawingGridVerticalSpacing", "displayHorizontalDrawingGridEvery", "displayVerticalDrawingGridEvery", "doNotUseMarginsForDrawingGridOrigin", "drawingGridHorizontalOrigin", "drawingGridVerticalOrigin", "doNotShadeFormData", "noPunctuationKerning",
			"characterSpacingControl", "printTwoOnOne", "strictFirstAndLastChars", "noLineBreaksAfter", "noLineBreaksBefore", "savePreviewPicture", "doNotValidateAgainstSchema", "saveInvalidXml", "ignoreMixedContent", "alwaysShowPlaceholderText",
			"doNotDemarcateInvalidXml", "saveXmlDataOnly", "useXSLTWhenSaving", "saveThroughXslt", "showXMLTags", "alwaysMergeEmptyNamespace", "updateFields", "hdrShapeDefaults", "footnotePr", "endnotePr",
			"compat", "docVars", "rsids", "mathPr", "uiCompat97To2003", "attachedSchema", "themeFontLang", "clrSchemeMapping", "doNotIncludeSubdocsInStats", "doNotAutoCompressPictures",
			"forceUpgrade", "captions", "readModeInkLockDown", "smartTagType", "schemaLibrary", "shapeDefaults", "doNotEmbedSmartTags", "decimalSymbol", "listSeparator", "docId",
			"discardImageEditingData", "defaultImageDpi", "conflictMode"
		};

		// Token: 0x0400AA75 RID: 43637
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 21, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 25, 23, 23, 23, 23, 52,
			52, 52, 52
		};
	}
}
