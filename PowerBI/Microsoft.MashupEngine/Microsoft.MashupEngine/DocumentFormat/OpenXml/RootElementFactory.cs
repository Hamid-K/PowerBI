using System;
using DocumentFormat.OpenXml.AdditionalCharacteristics;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.CustomXmlDataProperties;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office.Drawing;
using DocumentFormat.OpenXml.Office.Excel;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020021B9 RID: 8633
	internal static class RootElementFactory
	{
		// Token: 0x0600DB78 RID: 56184 RVA: 0x002BB850 File Offset: 0x002B9A50
		internal static OpenXmlElement CreateElement(byte namespaceId, string name)
		{
			if (10 == namespaceId && "theme" == name)
			{
				return new Theme();
			}
			if (10 == namespaceId && "themeOverride" == name)
			{
				return new ThemeOverride();
			}
			if (10 == namespaceId && "tblStyleLst" == name)
			{
				return new TableStyleList();
			}
			if (11 == namespaceId && "chartSpace" == name)
			{
				return new ChartSpace();
			}
			if (11 == namespaceId && "userShapes" == name)
			{
				return new UserShapes();
			}
			if (14 == namespaceId && "colorsDef" == name)
			{
				return new ColorsDefinition();
			}
			if (14 == namespaceId && "dataModel" == name)
			{
				return new DataModelRoot();
			}
			if (14 == namespaceId && "layoutDef" == name)
			{
				return new LayoutDefinition();
			}
			if (14 == namespaceId && "styleDef" == name)
			{
				return new StyleDefinition();
			}
			if (18 == namespaceId && "wsDr" == name)
			{
				return new WorksheetDrawing();
			}
			if (8 == namespaceId && "additionalCharacteristics" == name)
			{
				return new AdditionalCharacteristicsInfo();
			}
			if (9 == namespaceId && "Sources" == name)
			{
				return new Sources();
			}
			if (20 == namespaceId && "datastoreItem" == name)
			{
				return new DataStoreItem();
			}
			if (4 == namespaceId && "Properties" == name)
			{
				return new DocumentFormat.OpenXml.CustomProperties.Properties();
			}
			if (3 == namespaceId && "Properties" == name)
			{
				return new DocumentFormat.OpenXml.ExtendedProperties.Properties();
			}
			if (22 == namespaceId && "calcChain" == name)
			{
				return new CalculationChain();
			}
			if (22 == namespaceId && "comments" == name)
			{
				return new DocumentFormat.OpenXml.Spreadsheet.Comments();
			}
			if (22 == namespaceId && "MapInfo" == name)
			{
				return new MapInfo();
			}
			if (22 == namespaceId && "connections" == name)
			{
				return new Connections();
			}
			if (22 == namespaceId && "pivotCacheDefinition" == name)
			{
				return new DocumentFormat.OpenXml.Spreadsheet.PivotCacheDefinition();
			}
			if (22 == namespaceId && "pivotCacheRecords" == name)
			{
				return new PivotCacheRecords();
			}
			if (22 == namespaceId && "pivotTableDefinition" == name)
			{
				return new DocumentFormat.OpenXml.Spreadsheet.PivotTableDefinition();
			}
			if (22 == namespaceId && "queryTable" == name)
			{
				return new QueryTable();
			}
			if (22 == namespaceId && "sst" == name)
			{
				return new SharedStringTable();
			}
			if (22 == namespaceId && "headers" == name)
			{
				return new Headers();
			}
			if (22 == namespaceId && "revisions" == name)
			{
				return new Revisions();
			}
			if (22 == namespaceId && "users" == name)
			{
				return new Users();
			}
			if (22 == namespaceId && "worksheet" == name)
			{
				return new Worksheet();
			}
			if (22 == namespaceId && "chartsheet" == name)
			{
				return new Chartsheet();
			}
			if (22 == namespaceId && "dialogsheet" == name)
			{
				return new DialogSheet();
			}
			if (22 == namespaceId && "metadata" == name)
			{
				return new Metadata();
			}
			if (22 == namespaceId && "singleXmlCells" == name)
			{
				return new SingleXmlCells();
			}
			if (22 == namespaceId && "styleSheet" == name)
			{
				return new Stylesheet();
			}
			if (22 == namespaceId && "externalLink" == name)
			{
				return new ExternalLink();
			}
			if (22 == namespaceId && "table" == name)
			{
				return new DocumentFormat.OpenXml.Spreadsheet.Table();
			}
			if (22 == namespaceId && "volTypes" == name)
			{
				return new VolatileTypes();
			}
			if (22 == namespaceId && "workbook" == name)
			{
				return new Workbook();
			}
			if (23 == namespaceId && "recipients" == name)
			{
				return new Recipients();
			}
			if (23 == namespaceId && "comments" == name)
			{
				return new DocumentFormat.OpenXml.Wordprocessing.Comments();
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
				return new DocumentFormat.OpenXml.Wordprocessing.Header();
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
				return new DocumentFormat.OpenXml.Wordprocessing.Fonts();
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
			if (24 == namespaceId && "cmAuthorLst" == name)
			{
				return new CommentAuthorList();
			}
			if (24 == namespaceId && "cmLst" == name)
			{
				return new DocumentFormat.OpenXml.Presentation.CommentList();
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
			if (32 == namespaceId && "macrosheet" == name)
			{
				return new Macrosheet();
			}
			if (32 == namespaceId && "worksheetSortMap" == name)
			{
				return new WorksheetSortMap();
			}
			if (33 == namespaceId && "tcg" == name)
			{
				return new TemplateCommandGroup();
			}
			if (33 == namespaceId && "vbaSuppData" == name)
			{
				return new VbaSuppData();
			}
			if (33 == namespaceId && "recipients" == name)
			{
				return new MailMergeRecipients();
			}
			if (34 == namespaceId && "customUI" == name)
			{
				return new DocumentFormat.OpenXml.Office.CustomUI.CustomUI();
			}
			if (43 == namespaceId && "ink" == name)
			{
				return new Ink();
			}
			if (53 == namespaceId && "datastoreItem" == name)
			{
				return new DatastoreItem();
			}
			if (53 == namespaceId && "formControlPr" == name)
			{
				return new FormControlProperties();
			}
			if (53 == namespaceId && "slicers" == name)
			{
				return new Slicers();
			}
			if (53 == namespaceId && "slicerCacheDefinition" == name)
			{
				return new SlicerCacheDefinition();
			}
			if (56 == namespaceId && "drawing" == name)
			{
				return new DocumentFormat.OpenXml.Office.Drawing.Drawing();
			}
			if (57 == namespaceId && "customUI" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.CustomUI.CustomUI();
			}
			return null;
		}

		// Token: 0x0600DB79 RID: 56185 RVA: 0x002BBF7C File Offset: 0x002BA17C
		internal static OpenXmlElement CreateElement(string namespaceUri, string name)
		{
			OpenXmlElement openXmlElement = null;
			byte b;
			if (namespaceUri != null && NamespaceIdMap.TryGetNamespaceId(namespaceUri, out b))
			{
				openXmlElement = RootElementFactory.CreateElement(b, name);
			}
			if (openXmlElement == null)
			{
				openXmlElement = new OpenXmlUnknownElement();
			}
			return openXmlElement;
		}
	}
}
