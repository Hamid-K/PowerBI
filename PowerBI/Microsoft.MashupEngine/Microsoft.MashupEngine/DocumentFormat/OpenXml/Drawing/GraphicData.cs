using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.CustomXmlSchemaReferences;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing.LegacyCompatibility;
using DocumentFormat.OpenXml.Drawing.LockedCanvas;
using DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office.Drawing;
using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Office2010.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Drawing.Diagram;
using DocumentFormat.OpenXml.Office2010.Drawing.Pictures;
using DocumentFormat.OpenXml.Office2010.Drawing.Slicer;
using DocumentFormat.OpenXml.Office2010.Excel.Drawing;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Office2010.Word.Drawing;
using DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas;
using DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using DocumentFormat.OpenXml.Office2010.Word.DrawingShape;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279C RID: 10140
	[ChildElementInfo(typeof(CameraTool), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BookmarkTarget), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(Blip))]
	[ChildElementInfo(typeof(Theme))]
	[ChildElementInfo(typeof(ThemeOverride))]
	[ChildElementInfo(typeof(ThemeManager))]
	[ChildElementInfo(typeof(Table))]
	[ChildElementInfo(typeof(TableStyleList))]
	[ChildElementInfo(typeof(CompatExtension), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(IsCanvas), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GvmlContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShadowObscured), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenFillProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenLineProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenEffectsProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenScene3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenShape3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(UseLocalDpi), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextMath), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Drawing.Diagram.NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RecolorImages), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office.Drawing.Drawing))]
	[ChildElementInfo(typeof(DataModelExtensionBlock))]
	[ChildElementInfo(typeof(ColorsDefinition))]
	[ChildElementInfo(typeof(ColorsDefinitionHeader))]
	[ChildElementInfo(typeof(ColorsDefinitionHeaderList))]
	[ChildElementInfo(typeof(DataModelRoot))]
	[ChildElementInfo(typeof(LayoutDefinition))]
	[ChildElementInfo(typeof(LayoutDefinitionHeader))]
	[ChildElementInfo(typeof(LayoutDefinitionHeaderList))]
	[ChildElementInfo(typeof(RelationshipIds))]
	[ChildElementInfo(typeof(StyleDefinition))]
	[ChildElementInfo(typeof(StyleDefinitionHeader))]
	[ChildElementInfo(typeof(StyleDefinitionHeaderList))]
	[ChildElementInfo(typeof(ChartSpace))]
	[ChildElementInfo(typeof(UserShapes))]
	[ChildElementInfo(typeof(ChartReference))]
	[ChildElementInfo(typeof(PivotOptions), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SketchOptions), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InvertSolidFillFormat), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Drawing.Charts.Style), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LegacyDrawing))]
	[ChildElementInfo(typeof(LockedCanvas))]
	[ChildElementInfo(typeof(Inline))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Drawing.Wordprocessing.Anchor))]
	[ChildElementInfo(typeof(PercentagePositionHeightOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PercentagePositionVerticalOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RelativeWidth), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RelativeHeight), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Drawing.Pictures.Picture))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Drawing.Pictures.ShapeStyle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Drawing.Pictures.OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WorksheetDrawing))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Drawing.Spreadsheet.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Excel.Drawing.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CommentAuthorList))]
	[ChildElementInfo(typeof(CommentList))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Presentation.OleObject))]
	[ChildElementInfo(typeof(Presentation))]
	[ChildElementInfo(typeof(PresentationProperties))]
	[ChildElementInfo(typeof(Slide))]
	[ChildElementInfo(typeof(SlideLayout))]
	[ChildElementInfo(typeof(SlideMaster))]
	[ChildElementInfo(typeof(HandoutMaster))]
	[ChildElementInfo(typeof(NotesMaster))]
	[ChildElementInfo(typeof(NotesSlide))]
	[ChildElementInfo(typeof(SlideSyncProperties))]
	[ChildElementInfo(typeof(TagList))]
	[ChildElementInfo(typeof(ViewProperties))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Presentation.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.PowerPoint.NonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionListModify), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Media), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VortexTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SwitchTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FlipTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RippleTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HoneycombTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PrismTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DoorsTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WindowTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FerrisTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConveyorTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PanTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GlitterTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WarpTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FlythroughTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FlashTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShredTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RevealTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WheelReverseTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.PowerPoint.SectionProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SectionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BrowseMode), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LaserColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.PowerPoint.DefaultImageDpi), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DiscardImageEditData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShowMediaControls), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LaserTraceList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CreationId), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ModificationId), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShowEventRecordList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SchemaLibrary))]
	[ChildElementInfo(typeof(MathProperties))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Math.Paragraph))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Math.OfficeMath))]
	[ChildElementInfo(typeof(Recipients))]
	[ChildElementInfo(typeof(TextBoxContent))]
	[ChildElementInfo(typeof(Comments))]
	[ChildElementInfo(typeof(Footnotes))]
	[ChildElementInfo(typeof(Endnotes))]
	[ChildElementInfo(typeof(Header))]
	[ChildElementInfo(typeof(Footer))]
	[ChildElementInfo(typeof(Settings))]
	[ChildElementInfo(typeof(WebSettings))]
	[ChildElementInfo(typeof(Fonts))]
	[ChildElementInfo(typeof(Numbering))]
	[ChildElementInfo(typeof(Styles))]
	[ChildElementInfo(typeof(Document))]
	[ChildElementInfo(typeof(GlossaryDocument))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Word.ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentId), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConflictMode), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictInsertionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeStart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomXmlConflictDeletionRangeEnd), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DiscardImageEditingData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Word.DefaultImageDpi), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EntityPickerEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SdtContentCheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Shape))]
	[ChildElementInfo(typeof(Shapetype))]
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Background))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Shadow))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(Arc))]
	[ChildElementInfo(typeof(Curve))]
	[ChildElementInfo(typeof(ImageFile))]
	[ChildElementInfo(typeof(Line))]
	[ChildElementInfo(typeof(Oval))]
	[ChildElementInfo(typeof(PolyLine))]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(RoundRectangle))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Office.ShapeDefaults))]
	[ChildElementInfo(typeof(ShapeLayout))]
	[ChildElementInfo(typeof(SignatureLine))]
	[ChildElementInfo(typeof(Ink))]
	[ChildElementInfo(typeof(Diagram))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Office.Lock))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Office.OleObject))]
	[ChildElementInfo(typeof(Complex))]
	[ChildElementInfo(typeof(LeftStroke))]
	[ChildElementInfo(typeof(TopStroke))]
	[ChildElementInfo(typeof(RightStroke))]
	[ChildElementInfo(typeof(BottomStroke))]
	[ChildElementInfo(typeof(ColumnStroke))]
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(FillExtendedProperties))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Wordprocessing.TopBorder))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Wordprocessing.LeftBorder))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Wordprocessing.RightBorder))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Wordprocessing.BottomBorder))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(AnchorLock))]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Vml.Spreadsheet.ClientData))]
	[ChildElementInfo(typeof(InkAnnotationFlag))]
	[ChildElementInfo(typeof(TextData))]
	[ChildElementInfo(typeof(WordprocessingCanvas), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordprocessingGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordprocessingShape), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Slicer), FileFormatVersions.Office2010)]
	internal class GraphicData : OpenXmlCompositeElement
	{
		// Token: 0x17006254 RID: 25172
		// (get) Token: 0x060139F1 RID: 80369 RVA: 0x00308E64 File Offset: 0x00307064
		public override string LocalName
		{
			get
			{
				return "graphicData";
			}
		}

		// Token: 0x17006255 RID: 25173
		// (get) Token: 0x060139F2 RID: 80370 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006256 RID: 25174
		// (get) Token: 0x060139F3 RID: 80371 RVA: 0x00308E6B File Offset: 0x0030706B
		internal override int ElementTypeId
		{
			get
			{
				return 10173;
			}
		}

		// Token: 0x060139F4 RID: 80372 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006257 RID: 25175
		// (get) Token: 0x060139F5 RID: 80373 RVA: 0x00308E72 File Offset: 0x00307072
		internal override string[] AttributeTagNames
		{
			get
			{
				return GraphicData.attributeTagNames;
			}
		}

		// Token: 0x17006258 RID: 25176
		// (get) Token: 0x060139F6 RID: 80374 RVA: 0x00308E79 File Offset: 0x00307079
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GraphicData.attributeNamespaceIds;
			}
		}

		// Token: 0x17006259 RID: 25177
		// (get) Token: 0x060139F7 RID: 80375 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060139F8 RID: 80376 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060139F9 RID: 80377 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicData()
		{
		}

		// Token: 0x060139FA RID: 80378 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139FB RID: 80379 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139FC RID: 80380 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139FD RID: 80381 RVA: 0x00308E80 File Offset: 0x00307080
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			if (10 == namespaceId && "blip" == name)
			{
				return new Blip();
			}
			if (10 == namespaceId && "theme" == name)
			{
				return new Theme();
			}
			if (10 == namespaceId && "themeOverride" == name)
			{
				return new ThemeOverride();
			}
			if (10 == namespaceId && "themeManager" == name)
			{
				return new ThemeManager();
			}
			if (10 == namespaceId && "tbl" == name)
			{
				return new Table();
			}
			if (10 == namespaceId && "tblStyleLst" == name)
			{
				return new TableStyleList();
			}
			if (48 == namespaceId && "cameraTool" == name)
			{
				return new CameraTool();
			}
			if (48 == namespaceId && "compatExt" == name)
			{
				return new CompatExtension();
			}
			if (48 == namespaceId && "isCanvas" == name)
			{
				return new IsCanvas();
			}
			if (48 == namespaceId && "contentPart" == name)
			{
				return new GvmlContentPart();
			}
			if (48 == namespaceId && "shadowObscured" == name)
			{
				return new ShadowObscured();
			}
			if (48 == namespaceId && "hiddenFill" == name)
			{
				return new HiddenFillProperties();
			}
			if (48 == namespaceId && "hiddenLine" == name)
			{
				return new HiddenLineProperties();
			}
			if (48 == namespaceId && "hiddenEffects" == name)
			{
				return new HiddenEffectsProperties();
			}
			if (48 == namespaceId && "hiddenScene3d" == name)
			{
				return new HiddenScene3D();
			}
			if (48 == namespaceId && "hiddenSp3d" == name)
			{
				return new HiddenShape3D();
			}
			if (48 == namespaceId && "imgProps" == name)
			{
				return new ImageProperties();
			}
			if (48 == namespaceId && "useLocalDpi" == name)
			{
				return new UseLocalDpi();
			}
			if (48 == namespaceId && "m" == name)
			{
				return new TextMath();
			}
			if (58 == namespaceId && "cNvPr" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Drawing.Diagram.NonVisualDrawingProperties();
			}
			if (58 == namespaceId && "recolorImg" == name)
			{
				return new RecolorImages();
			}
			if (56 == namespaceId && "drawing" == name)
			{
				return new DocumentFormat.OpenXml.Office.Drawing.Drawing();
			}
			if (56 == namespaceId && "dataModelExt" == name)
			{
				return new DataModelExtensionBlock();
			}
			if (14 == namespaceId && "colorsDef" == name)
			{
				return new ColorsDefinition();
			}
			if (14 == namespaceId && "colorsDefHdr" == name)
			{
				return new ColorsDefinitionHeader();
			}
			if (14 == namespaceId && "colorsDefHdrLst" == name)
			{
				return new ColorsDefinitionHeaderList();
			}
			if (14 == namespaceId && "dataModel" == name)
			{
				return new DataModelRoot();
			}
			if (14 == namespaceId && "layoutDef" == name)
			{
				return new LayoutDefinition();
			}
			if (14 == namespaceId && "layoutDefHdr" == name)
			{
				return new LayoutDefinitionHeader();
			}
			if (14 == namespaceId && "layoutDefHdrLst" == name)
			{
				return new LayoutDefinitionHeaderList();
			}
			if (14 == namespaceId && "relIds" == name)
			{
				return new RelationshipIds();
			}
			if (14 == namespaceId && "styleDef" == name)
			{
				return new StyleDefinition();
			}
			if (14 == namespaceId && "styleDefHdr" == name)
			{
				return new StyleDefinitionHeader();
			}
			if (14 == namespaceId && "styleDefHdrLst" == name)
			{
				return new StyleDefinitionHeaderList();
			}
			if (11 == namespaceId && "chartSpace" == name)
			{
				return new ChartSpace();
			}
			if (11 == namespaceId && "userShapes" == name)
			{
				return new UserShapes();
			}
			if (11 == namespaceId && "chart" == name)
			{
				return new ChartReference();
			}
			if (46 == namespaceId && "pivotOptions" == name)
			{
				return new PivotOptions();
			}
			if (46 == namespaceId && "sketchOptions" == name)
			{
				return new SketchOptions();
			}
			if (46 == namespaceId && "invertSolidFillFmt" == name)
			{
				return new InvertSolidFillFormat();
			}
			if (46 == namespaceId && "style" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Drawing.Charts.Style();
			}
			if (47 == namespaceId && "contentPart" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing.ContentPart();
			}
			if (13 == namespaceId && "legacyDrawing" == name)
			{
				return new LegacyDrawing();
			}
			if (15 == namespaceId && "lockedCanvas" == name)
			{
				return new LockedCanvas();
			}
			if (16 == namespaceId && "inline" == name)
			{
				return new Inline();
			}
			if (16 == namespaceId && "anchor" == name)
			{
				return new DocumentFormat.OpenXml.Drawing.Wordprocessing.Anchor();
			}
			if (51 == namespaceId && "pctPosHOffset" == name)
			{
				return new PercentagePositionHeightOffset();
			}
			if (51 == namespaceId && "pctPosVOffset" == name)
			{
				return new PercentagePositionVerticalOffset();
			}
			if (51 == namespaceId && "sizeRelH" == name)
			{
				return new RelativeWidth();
			}
			if (51 == namespaceId && "sizeRelV" == name)
			{
				return new RelativeHeight();
			}
			if (17 == namespaceId && "pic" == name)
			{
				return new DocumentFormat.OpenXml.Drawing.Pictures.Picture();
			}
			if (50 == namespaceId && "style" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Drawing.Pictures.ShapeStyle();
			}
			if (50 == namespaceId && "extLst" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Drawing.Pictures.OfficeArtExtensionList();
			}
			if (18 == namespaceId && "wsDr" == name)
			{
				return new WorksheetDrawing();
			}
			if (18 == namespaceId && "contentPart" == name)
			{
				return new DocumentFormat.OpenXml.Drawing.Spreadsheet.ContentPart();
			}
			if (54 == namespaceId && "contentPart" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Excel.Drawing.ContentPart();
			}
			if (24 == namespaceId && "cmAuthorLst" == name)
			{
				return new CommentAuthorList();
			}
			if (24 == namespaceId && "cmLst" == name)
			{
				return new CommentList();
			}
			if (24 == namespaceId && "oleObj" == name)
			{
				return new DocumentFormat.OpenXml.Presentation.OleObject();
			}
			if (24 == namespaceId && "presentation" == name)
			{
				return new Presentation();
			}
			if (24 == namespaceId && "presentationPr" == name)
			{
				return new PresentationProperties();
			}
			if (24 == namespaceId && "sld" == name)
			{
				return new Slide();
			}
			if (24 == namespaceId && "sldLayout" == name)
			{
				return new SlideLayout();
			}
			if (24 == namespaceId && "sldMaster" == name)
			{
				return new SlideMaster();
			}
			if (24 == namespaceId && "handoutMaster" == name)
			{
				return new HandoutMaster();
			}
			if (24 == namespaceId && "notesMaster" == name)
			{
				return new NotesMaster();
			}
			if (24 == namespaceId && "notes" == name)
			{
				return new NotesSlide();
			}
			if (24 == namespaceId && "sldSyncPr" == name)
			{
				return new SlideSyncProperties();
			}
			if (24 == namespaceId && "tagLst" == name)
			{
				return new TagList();
			}
			if (24 == namespaceId && "viewPr" == name)
			{
				return new ViewProperties();
			}
			if (24 == namespaceId && "contentPart" == name)
			{
				return new DocumentFormat.OpenXml.Presentation.ContentPart();
			}
			if (49 == namespaceId && "nvContentPartPr" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.PowerPoint.NonVisualContentPartProperties();
			}
			if (49 == namespaceId && "xfrm" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D();
			}
			if (49 == namespaceId && "extLst" == name)
			{
				return new ExtensionListModify();
			}
			if (49 == namespaceId && "media" == name)
			{
				return new Media();
			}
			if (49 == namespaceId && "vortex" == name)
			{
				return new VortexTransition();
			}
			if (49 == namespaceId && "switch" == name)
			{
				return new SwitchTransition();
			}
			if (49 == namespaceId && "flip" == name)
			{
				return new FlipTransition();
			}
			if (49 == namespaceId && "ripple" == name)
			{
				return new RippleTransition();
			}
			if (49 == namespaceId && "honeycomb" == name)
			{
				return new HoneycombTransition();
			}
			if (49 == namespaceId && "prism" == name)
			{
				return new PrismTransition();
			}
			if (49 == namespaceId && "doors" == name)
			{
				return new DoorsTransition();
			}
			if (49 == namespaceId && "window" == name)
			{
				return new WindowTransition();
			}
			if (49 == namespaceId && "ferris" == name)
			{
				return new FerrisTransition();
			}
			if (49 == namespaceId && "gallery" == name)
			{
				return new GalleryTransition();
			}
			if (49 == namespaceId && "conveyor" == name)
			{
				return new ConveyorTransition();
			}
			if (49 == namespaceId && "pan" == name)
			{
				return new PanTransition();
			}
			if (49 == namespaceId && "glitter" == name)
			{
				return new GlitterTransition();
			}
			if (49 == namespaceId && "warp" == name)
			{
				return new WarpTransition();
			}
			if (49 == namespaceId && "flythrough" == name)
			{
				return new FlythroughTransition();
			}
			if (49 == namespaceId && "flash" == name)
			{
				return new FlashTransition();
			}
			if (49 == namespaceId && "shred" == name)
			{
				return new ShredTransition();
			}
			if (49 == namespaceId && "reveal" == name)
			{
				return new RevealTransition();
			}
			if (49 == namespaceId && "wheelReverse" == name)
			{
				return new WheelReverseTransition();
			}
			if (49 == namespaceId && "bmkTgt" == name)
			{
				return new BookmarkTarget();
			}
			if (49 == namespaceId && "sectionPr" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.PowerPoint.SectionProperties();
			}
			if (49 == namespaceId && "sectionLst" == name)
			{
				return new SectionList();
			}
			if (49 == namespaceId && "browseMode" == name)
			{
				return new BrowseMode();
			}
			if (49 == namespaceId && "laserClr" == name)
			{
				return new LaserColor();
			}
			if (49 == namespaceId && "defaultImageDpi" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.PowerPoint.DefaultImageDpi();
			}
			if (49 == namespaceId && "discardImageEditData" == name)
			{
				return new DiscardImageEditData();
			}
			if (49 == namespaceId && "showMediaCtrls" == name)
			{
				return new ShowMediaControls();
			}
			if (49 == namespaceId && "laserTraceLst" == name)
			{
				return new LaserTraceList();
			}
			if (49 == namespaceId && "creationId" == name)
			{
				return new CreationId();
			}
			if (49 == namespaceId && "modId" == name)
			{
				return new ModificationId();
			}
			if (49 == namespaceId && "showEvtLst" == name)
			{
				return new ShowEventRecordList();
			}
			if (25 == namespaceId && "schemaLibrary" == name)
			{
				return new SchemaLibrary();
			}
			if (21 == namespaceId && "mathPr" == name)
			{
				return new MathProperties();
			}
			if (21 == namespaceId && "oMathPara" == name)
			{
				return new DocumentFormat.OpenXml.Math.Paragraph();
			}
			if (21 == namespaceId && "oMath" == name)
			{
				return new DocumentFormat.OpenXml.Math.OfficeMath();
			}
			if (23 == namespaceId && "recipients" == name)
			{
				return new Recipients();
			}
			if (23 == namespaceId && "txbxContent" == name)
			{
				return new TextBoxContent();
			}
			if (23 == namespaceId && "comments" == name)
			{
				return new Comments();
			}
			if (23 == namespaceId && "footnotes" == name)
			{
				return new Footnotes();
			}
			if (23 == namespaceId && "endnotes" == name)
			{
				return new Endnotes();
			}
			if (23 == namespaceId && "hdr" == name)
			{
				return new Header();
			}
			if (23 == namespaceId && "ftr" == name)
			{
				return new Footer();
			}
			if (23 == namespaceId && "settings" == name)
			{
				return new Settings();
			}
			if (23 == namespaceId && "webSettings" == name)
			{
				return new WebSettings();
			}
			if (23 == namespaceId && "fonts" == name)
			{
				return new Fonts();
			}
			if (23 == namespaceId && "numbering" == name)
			{
				return new Numbering();
			}
			if (23 == namespaceId && "styles" == name)
			{
				return new Styles();
			}
			if (23 == namespaceId && "document" == name)
			{
				return new Document();
			}
			if (23 == namespaceId && "glossaryDocument" == name)
			{
				return new GlossaryDocument();
			}
			if (52 == namespaceId && "contentPart" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Word.ContentPart();
			}
			if (52 == namespaceId && "docId" == name)
			{
				return new DocumentId();
			}
			if (52 == namespaceId && "conflictMode" == name)
			{
				return new ConflictMode();
			}
			if (52 == namespaceId && "customXmlConflictInsRangeStart" == name)
			{
				return new CustomXmlConflictInsertionRangeStart();
			}
			if (52 == namespaceId && "customXmlConflictInsRangeEnd" == name)
			{
				return new CustomXmlConflictInsertionRangeEnd();
			}
			if (52 == namespaceId && "customXmlConflictDelRangeStart" == name)
			{
				return new CustomXmlConflictDeletionRangeStart();
			}
			if (52 == namespaceId && "customXmlConflictDelRangeEnd" == name)
			{
				return new CustomXmlConflictDeletionRangeEnd();
			}
			if (52 == namespaceId && "discardImageEditingData" == name)
			{
				return new DiscardImageEditingData();
			}
			if (52 == namespaceId && "defaultImageDpi" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Word.DefaultImageDpi();
			}
			if (52 == namespaceId && "entityPicker" == name)
			{
				return new EntityPickerEmpty();
			}
			if (52 == namespaceId && "checkbox" == name)
			{
				return new SdtContentCheckBox();
			}
			if (26 == namespaceId && "shape" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Shape();
			}
			if (26 == namespaceId && "shapetype" == name)
			{
				return new Shapetype();
			}
			if (26 == namespaceId && "group" == name)
			{
				return new Group();
			}
			if (26 == namespaceId && "background" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Background();
			}
			if (26 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			if (26 == namespaceId && "formulas" == name)
			{
				return new Formulas();
			}
			if (26 == namespaceId && "handles" == name)
			{
				return new ShapeHandles();
			}
			if (26 == namespaceId && "imagedata" == name)
			{
				return new ImageData();
			}
			if (26 == namespaceId && "path" == name)
			{
				return new Path();
			}
			if (26 == namespaceId && "textbox" == name)
			{
				return new TextBox();
			}
			if (26 == namespaceId && "shadow" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Shadow();
			}
			if (26 == namespaceId && "stroke" == name)
			{
				return new Stroke();
			}
			if (26 == namespaceId && "textpath" == name)
			{
				return new TextPath();
			}
			if (26 == namespaceId && "arc" == name)
			{
				return new Arc();
			}
			if (26 == namespaceId && "curve" == name)
			{
				return new Curve();
			}
			if (26 == namespaceId && "image" == name)
			{
				return new ImageFile();
			}
			if (26 == namespaceId && "line" == name)
			{
				return new Line();
			}
			if (26 == namespaceId && "oval" == name)
			{
				return new Oval();
			}
			if (26 == namespaceId && "polyline" == name)
			{
				return new PolyLine();
			}
			if (26 == namespaceId && "rect" == name)
			{
				return new Rectangle();
			}
			if (26 == namespaceId && "roundrect" == name)
			{
				return new RoundRectangle();
			}
			if (27 == namespaceId && "shapedefaults" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Office.ShapeDefaults();
			}
			if (27 == namespaceId && "shapelayout" == name)
			{
				return new ShapeLayout();
			}
			if (27 == namespaceId && "signatureline" == name)
			{
				return new SignatureLine();
			}
			if (27 == namespaceId && "ink" == name)
			{
				return new Ink();
			}
			if (27 == namespaceId && "diagram" == name)
			{
				return new Diagram();
			}
			if (27 == namespaceId && "skew" == name)
			{
				return new Skew();
			}
			if (27 == namespaceId && "extrusion" == name)
			{
				return new Extrusion();
			}
			if (27 == namespaceId && "callout" == name)
			{
				return new Callout();
			}
			if (27 == namespaceId && "lock" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Office.Lock();
			}
			if (27 == namespaceId && "OLEObject" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Office.OleObject();
			}
			if (27 == namespaceId && "complex" == name)
			{
				return new Complex();
			}
			if (27 == namespaceId && "left" == name)
			{
				return new LeftStroke();
			}
			if (27 == namespaceId && "top" == name)
			{
				return new TopStroke();
			}
			if (27 == namespaceId && "right" == name)
			{
				return new RightStroke();
			}
			if (27 == namespaceId && "bottom" == name)
			{
				return new BottomStroke();
			}
			if (27 == namespaceId && "column" == name)
			{
				return new ColumnStroke();
			}
			if (27 == namespaceId && "clippath" == name)
			{
				return new ClipPath();
			}
			if (27 == namespaceId && "fill" == name)
			{
				return new FillExtendedProperties();
			}
			if (28 == namespaceId && "bordertop" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Wordprocessing.TopBorder();
			}
			if (28 == namespaceId && "borderleft" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Wordprocessing.LeftBorder();
			}
			if (28 == namespaceId && "borderright" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Wordprocessing.RightBorder();
			}
			if (28 == namespaceId && "borderbottom" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Wordprocessing.BottomBorder();
			}
			if (28 == namespaceId && "wrap" == name)
			{
				return new TextWrap();
			}
			if (28 == namespaceId && "anchorlock" == name)
			{
				return new AnchorLock();
			}
			if (29 == namespaceId && "ClientData" == name)
			{
				return new DocumentFormat.OpenXml.Vml.Spreadsheet.ClientData();
			}
			if (30 == namespaceId && "iscomment" == name)
			{
				return new InkAnnotationFlag();
			}
			if (30 == namespaceId && "textdata" == name)
			{
				return new TextData();
			}
			if (59 == namespaceId && "wpc" == name)
			{
				return new WordprocessingCanvas();
			}
			if (60 == namespaceId && "wgp" == name)
			{
				return new WordprocessingGroup();
			}
			if (61 == namespaceId && "wsp" == name)
			{
				return new WordprocessingShape();
			}
			if (62 == namespaceId && "slicer" == name)
			{
				return new Slicer();
			}
			return null;
		}

		// Token: 0x060139FE RID: 80382 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060139FF RID: 80383 RVA: 0x0030A02E File Offset: 0x0030822E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicData>(deep);
		}

		// Token: 0x06013A00 RID: 80384 RVA: 0x0030A038 File Offset: 0x00308238
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicData()
		{
			byte[] array = new byte[1];
			GraphicData.attributeNamespaceIds = array;
		}

		// Token: 0x040086FE RID: 34558
		private const string tagName = "graphicData";

		// Token: 0x040086FF RID: 34559
		private const byte tagNsId = 10;

		// Token: 0x04008700 RID: 34560
		internal const int ElementTypeIdConst = 10173;

		// Token: 0x04008701 RID: 34561
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008702 RID: 34562
		private static byte[] attributeNamespaceIds;
	}
}
