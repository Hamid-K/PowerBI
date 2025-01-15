using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002148 RID: 8520
	internal static class GlobalPartFactory
	{
		// Token: 0x0600D3CF RID: 54223 RVA: 0x002A0154 File Offset: 0x0029E354
		internal static OpenXmlPart CreateOpenXmlPart(OpenXmlPackage openXmlPackage, string relationshipType)
		{
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			OpenXmlPart openXmlPart = null;
			GlobalPartFactory.CreatePartCore(openXmlPackage, relationshipType, ref openXmlPart);
			if (openXmlPart == null)
			{
				openXmlPart = new ExtendedPart(relationshipType);
			}
			return openXmlPart;
		}

		// Token: 0x0600D3D0 RID: 54224 RVA: 0x002A0188 File Offset: 0x0029E388
		private static void CreatePartCore(OpenXmlPackage openXmlPackage, string relationshipType, ref OpenXmlPart openXmlPart)
		{
			if (openXmlPackage == null)
			{
				throw new ArgumentNullException("openXmlPackage");
			}
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (openXmlPackage is WordprocessingDocument)
			{
				switch (relationshipType)
				{
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument":
					openXmlPart = new MainDocumentPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml":
					openXmlPart = new CustomXmlPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps":
					openXmlPart = new CustomXmlPropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument":
					openXmlPart = new GlossaryDocumentPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments":
					openXmlPart = new WordprocessingCommentsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk":
					openXmlPart = new AlternativeFormatImportPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart":
					openXmlPart = new ChartPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes":
					openXmlPart = new ChartDrawingPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
					openXmlPart = new ImagePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
					openXmlPart = new EmbeddedPackagePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride":
					openXmlPart = new ThemeOverridePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors":
					openXmlPart = new DiagramColorsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData":
					openXmlPart = new DiagramDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
					openXmlPart = new SlidePart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing":
					openXmlPart = new DiagramPersistLayoutPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout":
					openXmlPart = new DiagramLayoutDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle":
					openXmlPart = new DiagramStylePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
					openXmlPart = new EmbeddedObjectPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing":
					openXmlPart = new VmlDrawingPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText":
					openXmlPart = new LegacyDiagramTextPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary":
					openXmlPart = new EmbeddedControlPersistenceBinaryDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide":
					openXmlPart = new NotesSlidePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster":
					openXmlPart = new NotesMasterPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
					openXmlPart = new ThemePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
					openXmlPart = new UserDefinedTagsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout":
					openXmlPart = new SlideLayoutPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster":
					openXmlPart = new SlideMasterPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
					openXmlPart = new EmbeddedControlPersistencePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo":
					openXmlPart = new SlideSyncDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet":
					openXmlPart = new WorksheetPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing":
					openXmlPart = new DrawingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable":
					openXmlPart = new PivotTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition":
					openXmlPart = new PivotTableCacheDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords":
					openXmlPart = new PivotTableCacheRecordsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells":
					openXmlPart = new SingleCellTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table":
					openXmlPart = new TableDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable":
					openXmlPart = new QueryTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp":
					openXmlPart = new ControlPropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty":
					openXmlPart = new CustomPropertyPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/wsSortMap":
					openXmlPart = new WorksheetSortMapPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/slicer":
					openXmlPart = new SlicersPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings":
					openXmlPart = new DocumentSettingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData":
					openXmlPart = new MailMergeRecipientDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes":
					openXmlPart = new EndnotesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable":
					openXmlPart = new FontTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font":
					openXmlPart = new FontPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes":
					openXmlPart = new FootnotesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering":
					openXmlPart = new NumberingDefinitionsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles":
					openXmlPart = new StyleDefinitionsPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/stylesWithEffects":
					openXmlPart = new StylesWithEffectsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings":
					openXmlPart = new WebSettingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer":
					openXmlPart = new FooterPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header":
					openXmlPart = new HeaderPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings":
					openXmlPart = new WordprocessingPrinterSettingsPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations":
					openXmlPart = new CustomizationPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars":
					openXmlPart = new WordAttachedToolbarsPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/vbaProject":
					openXmlPart = new VbaProjectPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/wordVbaData":
					openXmlPart = new VbaDataPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail":
					openXmlPart = new ThumbnailPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties":
					openXmlPart = new CoreFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties":
					openXmlPart = new ExtendedFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties":
					openXmlPart = new CustomFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin":
					openXmlPart = new DigitalSignatureOriginPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature":
					openXmlPart = new XmlSignaturePart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization":
					openXmlPart = new QuickAccessToolbarCustomizationsPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility":
					openXmlPart = new RibbonExtensibilityPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility":
					openXmlPart = new RibbonAndBackstageCustomizationsPart();
					return;

					return;
				}
			}
			else if (openXmlPackage is SpreadsheetDocument)
			{
				switch (relationshipType)
				{
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument":
					openXmlPart = new WorkbookPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml":
					openXmlPart = new CustomXmlPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps":
					openXmlPart = new CustomXmlPropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain":
					openXmlPart = new CalculationChainPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata":
					openXmlPart = new CellMetadataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections":
					openXmlPart = new ConnectionsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps":
					openXmlPart = new CustomXmlMappingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings":
					openXmlPart = new SharedStringTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders":
					openXmlPart = new WorkbookRevisionHeaderPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog":
					openXmlPart = new WorkbookRevisionLogPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames":
					openXmlPart = new WorkbookUserDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles":
					openXmlPart = new WorkbookStylesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
					openXmlPart = new ThemePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
					openXmlPart = new ImagePart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail":
					openXmlPart = new ThumbnailPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies":
					openXmlPart = new VolatileDependenciesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet":
					openXmlPart = new ChartsheetPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings":
					openXmlPart = new SpreadsheetPrinterSettingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing":
					openXmlPart = new DrawingsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart":
					openXmlPart = new ChartPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes":
					openXmlPart = new ChartDrawingPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
					openXmlPart = new EmbeddedPackagePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride":
					openXmlPart = new ThemeOverridePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors":
					openXmlPart = new DiagramColorsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData":
					openXmlPart = new DiagramDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide":
					openXmlPart = new SlidePart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing":
					openXmlPart = new DiagramPersistLayoutPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout":
					openXmlPart = new DiagramLayoutDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle":
					openXmlPart = new DiagramStylePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
					openXmlPart = new EmbeddedObjectPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing":
					openXmlPart = new VmlDrawingPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText":
					openXmlPart = new LegacyDiagramTextPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary":
					openXmlPart = new EmbeddedControlPersistenceBinaryDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide":
					openXmlPart = new NotesSlidePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster":
					openXmlPart = new NotesMasterPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags":
					openXmlPart = new UserDefinedTagsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout":
					openXmlPart = new SlideLayoutPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster":
					openXmlPart = new SlideMasterPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
					openXmlPart = new EmbeddedControlPersistencePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo":
					openXmlPart = new SlideSyncDataPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet":
					openXmlPart = new WorksheetPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments":
					openXmlPart = new WorksheetCommentsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable":
					openXmlPart = new PivotTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition":
					openXmlPart = new PivotTableCacheDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords":
					openXmlPart = new PivotTableCacheRecordsPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells":
					openXmlPart = new SingleCellTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table":
					openXmlPart = new TableDefinitionPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable":
					openXmlPart = new QueryTablePart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp":
					openXmlPart = new ControlPropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty":
					openXmlPart = new CustomPropertyPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/wsSortMap":
					openXmlPart = new WorksheetSortMapPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/slicer":
					openXmlPart = new SlicersPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet":
					openXmlPart = new DialogsheetPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink":
					openXmlPart = new ExternalWorkbookPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars":
					openXmlPart = new ExcelAttachedToolbarsPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/vbaProject":
					openXmlPart = new VbaProjectPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/wordVbaData":
					openXmlPart = new VbaDataPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/xlMacrosheet":
					openXmlPart = new MacroSheetPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/xlIntlMacrosheet":
					openXmlPart = new InternationalMacroSheetPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/customDataProps":
					openXmlPart = new CustomDataPropertiesPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/customData":
					openXmlPart = new CustomDataPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/slicerCache":
					openXmlPart = new SlicerCachePart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties":
					openXmlPart = new CoreFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties":
					openXmlPart = new ExtendedFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties":
					openXmlPart = new CustomFilePropertiesPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin":
					openXmlPart = new DigitalSignatureOriginPart();
					return;
				case "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature":
					openXmlPart = new XmlSignaturePart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization":
					openXmlPart = new QuickAccessToolbarCustomizationsPart();
					return;
				case "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility":
					openXmlPart = new RibbonExtensibilityPart();
					return;
				case "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility":
					openXmlPart = new RibbonAndBackstageCustomizationsPart();
					return;

					return;
				}
			}
			else if (openXmlPackage is PresentationDocument && relationshipType != null)
			{
				if (<PrivateImplementationDetails>{70C8A48F-C76A-471A-9D5B-9406CD7410EA}.$$method0x6000512-3 == null)
				{
					<PrivateImplementationDetails>{70C8A48F-C76A-471A-9D5B-9406CD7410EA}.$$method0x6000512-3 = new Dictionary<string, int>(59)
					{
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", 0 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml", 1 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps", 2 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font", 3 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps", 4 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles", 5 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme", 6 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image", 7 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps", 8 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide", 9 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart", 10 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes", 11 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package", 12 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride", 13 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors", 14 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData", 15 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet", 16 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings", 17 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing", 18 },
						{ "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing", 19 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout", 20 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle", 21 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing", 22 },
						{ "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText", 23 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable", 24 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition", 25 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords", 26 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells", 27 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table", 28 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable", 29 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control", 30 },
						{ "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary", 31 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp", 32 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject", 33 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty", 34 },
						{ "http://schemas.microsoft.com/office/2006/relationships/wsSortMap", 35 },
						{ "http://schemas.microsoft.com/office/2007/relationships/slicer", 36 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments", 37 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide", 38 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster", 39 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags", 40 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout", 41 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster", 42 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo", 43 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors", 44 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster", 45 },
						{ "http://schemas.microsoft.com/office/2006/relationships/legacyDocTextInfo", 46 },
						{ "http://schemas.microsoft.com/office/2006/relationships/smartTags", 47 },
						{ "http://schemas.microsoft.com/office/2006/relationships/vbaProject", 48 },
						{ "http://schemas.microsoft.com/office/2006/relationships/wordVbaData", 49 },
						{ "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties", 50 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties", 51 },
						{ "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties", 52 },
						{ "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail", 53 },
						{ "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin", 54 },
						{ "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature", 55 },
						{ "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization", 56 },
						{ "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility", 57 },
						{ "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility", 58 }
					};
				}
				int num3;
				if (<PrivateImplementationDetails>{70C8A48F-C76A-471A-9D5B-9406CD7410EA}.$$method0x6000512-3.TryGetValue(relationshipType, out num3))
				{
					switch (num3)
					{
					case 0:
						openXmlPart = new PresentationPart();
						return;
					case 1:
						openXmlPart = new CustomXmlPart();
						return;
					case 2:
						openXmlPart = new CustomXmlPropertiesPart();
						return;
					case 3:
						openXmlPart = new FontPart();
						return;
					case 4:
						openXmlPart = new PresentationPropertiesPart();
						return;
					case 5:
						openXmlPart = new TableStylesPart();
						return;
					case 6:
						openXmlPart = new ThemePart();
						return;
					case 7:
						openXmlPart = new ImagePart();
						return;
					case 8:
						openXmlPart = new ViewPropertiesPart();
						return;
					case 9:
						openXmlPart = new SlidePart();
						return;
					case 10:
						openXmlPart = new ChartPart();
						return;
					case 11:
						openXmlPart = new ChartDrawingPart();
						return;
					case 12:
						openXmlPart = new EmbeddedPackagePart();
						return;
					case 13:
						openXmlPart = new ThemeOverridePart();
						return;
					case 14:
						openXmlPart = new DiagramColorsPart();
						return;
					case 15:
						openXmlPart = new DiagramDataPart();
						return;
					case 16:
						openXmlPart = new WorksheetPart();
						return;
					case 17:
						openXmlPart = new SpreadsheetPrinterSettingsPart();
						return;
					case 18:
						openXmlPart = new DrawingsPart();
						return;
					case 19:
						openXmlPart = new DiagramPersistLayoutPart();
						return;
					case 20:
						openXmlPart = new DiagramLayoutDefinitionPart();
						return;
					case 21:
						openXmlPart = new DiagramStylePart();
						return;
					case 22:
						openXmlPart = new VmlDrawingPart();
						return;
					case 23:
						openXmlPart = new LegacyDiagramTextPart();
						return;
					case 24:
						openXmlPart = new PivotTablePart();
						return;
					case 25:
						openXmlPart = new PivotTableCacheDefinitionPart();
						return;
					case 26:
						openXmlPart = new PivotTableCacheRecordsPart();
						return;
					case 27:
						openXmlPart = new SingleCellTablePart();
						return;
					case 28:
						openXmlPart = new TableDefinitionPart();
						return;
					case 29:
						openXmlPart = new QueryTablePart();
						return;
					case 30:
						openXmlPart = new EmbeddedControlPersistencePart();
						return;
					case 31:
						openXmlPart = new EmbeddedControlPersistenceBinaryDataPart();
						return;
					case 32:
						openXmlPart = new ControlPropertiesPart();
						return;
					case 33:
						openXmlPart = new EmbeddedObjectPart();
						return;
					case 34:
						openXmlPart = new CustomPropertyPart();
						return;
					case 35:
						openXmlPart = new WorksheetSortMapPart();
						return;
					case 36:
						openXmlPart = new SlicersPart();
						return;
					case 37:
						openXmlPart = new SlideCommentsPart();
						return;
					case 38:
						openXmlPart = new NotesSlidePart();
						return;
					case 39:
						openXmlPart = new NotesMasterPart();
						return;
					case 40:
						openXmlPart = new UserDefinedTagsPart();
						return;
					case 41:
						openXmlPart = new SlideLayoutPart();
						return;
					case 42:
						openXmlPart = new SlideMasterPart();
						return;
					case 43:
						openXmlPart = new SlideSyncDataPart();
						return;
					case 44:
						openXmlPart = new CommentAuthorsPart();
						return;
					case 45:
						openXmlPart = new HandoutMasterPart();
						return;
					case 46:
						openXmlPart = new LegacyDiagramTextInfoPart();
						return;
					case 47:
						openXmlPart = new SmartTagsPart();
						return;
					case 48:
						openXmlPart = new VbaProjectPart();
						return;
					case 49:
						openXmlPart = new VbaDataPart();
						return;
					case 50:
						openXmlPart = new CoreFilePropertiesPart();
						return;
					case 51:
						openXmlPart = new ExtendedFilePropertiesPart();
						return;
					case 52:
						openXmlPart = new CustomFilePropertiesPart();
						return;
					case 53:
						openXmlPart = new ThumbnailPart();
						return;
					case 54:
						openXmlPart = new DigitalSignatureOriginPart();
						return;
					case 55:
						openXmlPart = new XmlSignaturePart();
						return;
					case 56:
						openXmlPart = new QuickAccessToolbarCustomizationsPart();
						return;
					case 57:
						openXmlPart = new RibbonExtensibilityPart();
						return;
					case 58:
						openXmlPart = new RibbonAndBackstageCustomizationsPart();
						break;
					default:
						return;
					}
				}
			}
		}
	}
}
