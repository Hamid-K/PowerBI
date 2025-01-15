using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B21 RID: 11041
	[ChildElementInfo(typeof(WorksheetExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Hyperlinks))]
	[ChildElementInfo(typeof(TableParts))]
	[ChildElementInfo(typeof(SheetDimension))]
	[ChildElementInfo(typeof(SheetViews))]
	[ChildElementInfo(typeof(SheetFormatProperties))]
	[ChildElementInfo(typeof(Columns))]
	[ChildElementInfo(typeof(SheetData))]
	[ChildElementInfo(typeof(SheetCalculationProperties))]
	[ChildElementInfo(typeof(SheetProtection))]
	[ChildElementInfo(typeof(ProtectedRanges))]
	[ChildElementInfo(typeof(Scenarios))]
	[ChildElementInfo(typeof(AutoFilter))]
	[ChildElementInfo(typeof(SortState))]
	[ChildElementInfo(typeof(DataConsolidate))]
	[ChildElementInfo(typeof(CustomSheetViews))]
	[ChildElementInfo(typeof(MergeCells))]
	[ChildElementInfo(typeof(PhoneticProperties))]
	[ChildElementInfo(typeof(ConditionalFormatting))]
	[ChildElementInfo(typeof(DataValidations))]
	[ChildElementInfo(typeof(SheetProperties))]
	[ChildElementInfo(typeof(PrintOptions))]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(PageSetup))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(RowBreaks))]
	[ChildElementInfo(typeof(ColumnBreaks))]
	[ChildElementInfo(typeof(CustomProperties))]
	[ChildElementInfo(typeof(CellWatches))]
	[ChildElementInfo(typeof(IgnoredErrors))]
	[ChildElementInfo(typeof(SmartTags))]
	[ChildElementInfo(typeof(Drawing))]
	[ChildElementInfo(typeof(LegacyDrawing))]
	[ChildElementInfo(typeof(LegacyDrawingHeaderFooter))]
	[ChildElementInfo(typeof(DrawingHeaderFooter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(OleObjects))]
	[ChildElementInfo(typeof(Controls))]
	[ChildElementInfo(typeof(WebPublishItems))]
	internal class Worksheet : OpenXmlPartRootElement
	{
		// Token: 0x170076A0 RID: 30368
		// (get) Token: 0x06016831 RID: 92209 RVA: 0x0032B61B File Offset: 0x0032981B
		public override string LocalName
		{
			get
			{
				return "worksheet";
			}
		}

		// Token: 0x170076A1 RID: 30369
		// (get) Token: 0x06016832 RID: 92210 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076A2 RID: 30370
		// (get) Token: 0x06016833 RID: 92211 RVA: 0x0032B622 File Offset: 0x00329822
		internal override int ElementTypeId
		{
			get
			{
				return 11039;
			}
		}

		// Token: 0x06016834 RID: 92212 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016835 RID: 92213 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Worksheet(WorksheetPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016836 RID: 92214 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorksheetPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076A3 RID: 30371
		// (get) Token: 0x06016837 RID: 92215 RVA: 0x0032B629 File Offset: 0x00329829
		// (set) Token: 0x06016838 RID: 92216 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorksheetPart WorksheetPart
		{
			get
			{
				return base.OpenXmlPart as WorksheetPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016839 RID: 92217 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Worksheet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601683A RID: 92218 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Worksheet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601683B RID: 92219 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Worksheet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601683C RID: 92220 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Worksheet()
		{
		}

		// Token: 0x0601683D RID: 92221 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorksheetPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601683E RID: 92222 RVA: 0x0032B638 File Offset: 0x00329838
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetPr" == name)
			{
				return new SheetProperties();
			}
			if (22 == namespaceId && "dimension" == name)
			{
				return new SheetDimension();
			}
			if (22 == namespaceId && "sheetViews" == name)
			{
				return new SheetViews();
			}
			if (22 == namespaceId && "sheetFormatPr" == name)
			{
				return new SheetFormatProperties();
			}
			if (22 == namespaceId && "cols" == name)
			{
				return new Columns();
			}
			if (22 == namespaceId && "sheetData" == name)
			{
				return new SheetData();
			}
			if (22 == namespaceId && "sheetCalcPr" == name)
			{
				return new SheetCalculationProperties();
			}
			if (22 == namespaceId && "sheetProtection" == name)
			{
				return new SheetProtection();
			}
			if (22 == namespaceId && "protectedRanges" == name)
			{
				return new ProtectedRanges();
			}
			if (22 == namespaceId && "scenarios" == name)
			{
				return new Scenarios();
			}
			if (22 == namespaceId && "autoFilter" == name)
			{
				return new AutoFilter();
			}
			if (22 == namespaceId && "sortState" == name)
			{
				return new SortState();
			}
			if (22 == namespaceId && "dataConsolidate" == name)
			{
				return new DataConsolidate();
			}
			if (22 == namespaceId && "customSheetViews" == name)
			{
				return new CustomSheetViews();
			}
			if (22 == namespaceId && "mergeCells" == name)
			{
				return new MergeCells();
			}
			if (22 == namespaceId && "phoneticPr" == name)
			{
				return new PhoneticProperties();
			}
			if (22 == namespaceId && "conditionalFormatting" == name)
			{
				return new ConditionalFormatting();
			}
			if (22 == namespaceId && "dataValidations" == name)
			{
				return new DataValidations();
			}
			if (22 == namespaceId && "hyperlinks" == name)
			{
				return new Hyperlinks();
			}
			if (22 == namespaceId && "printOptions" == name)
			{
				return new PrintOptions();
			}
			if (22 == namespaceId && "pageMargins" == name)
			{
				return new PageMargins();
			}
			if (22 == namespaceId && "pageSetup" == name)
			{
				return new PageSetup();
			}
			if (22 == namespaceId && "headerFooter" == name)
			{
				return new HeaderFooter();
			}
			if (22 == namespaceId && "rowBreaks" == name)
			{
				return new RowBreaks();
			}
			if (22 == namespaceId && "colBreaks" == name)
			{
				return new ColumnBreaks();
			}
			if (22 == namespaceId && "customProperties" == name)
			{
				return new CustomProperties();
			}
			if (22 == namespaceId && "cellWatches" == name)
			{
				return new CellWatches();
			}
			if (22 == namespaceId && "ignoredErrors" == name)
			{
				return new IgnoredErrors();
			}
			if (22 == namespaceId && "smartTags" == name)
			{
				return new SmartTags();
			}
			if (22 == namespaceId && "drawing" == name)
			{
				return new Drawing();
			}
			if (22 == namespaceId && "legacyDrawing" == name)
			{
				return new LegacyDrawing();
			}
			if (22 == namespaceId && "legacyDrawingHF" == name)
			{
				return new LegacyDrawingHeaderFooter();
			}
			if (22 == namespaceId && "drawingHF" == name)
			{
				return new DrawingHeaderFooter();
			}
			if (22 == namespaceId && "picture" == name)
			{
				return new Picture();
			}
			if (22 == namespaceId && "oleObjects" == name)
			{
				return new OleObjects();
			}
			if (22 == namespaceId && "controls" == name)
			{
				return new Controls();
			}
			if (22 == namespaceId && "webPublishItems" == name)
			{
				return new WebPublishItems();
			}
			if (22 == namespaceId && "tableParts" == name)
			{
				return new TableParts();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new WorksheetExtensionList();
			}
			return null;
		}

		// Token: 0x170076A4 RID: 30372
		// (get) Token: 0x0601683F RID: 92223 RVA: 0x0032B9EE File Offset: 0x00329BEE
		internal override string[] ElementTagNames
		{
			get
			{
				return Worksheet.eleTagNames;
			}
		}

		// Token: 0x170076A5 RID: 30373
		// (get) Token: 0x06016840 RID: 92224 RVA: 0x0032B9F5 File Offset: 0x00329BF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Worksheet.eleNamespaceIds;
			}
		}

		// Token: 0x170076A6 RID: 30374
		// (get) Token: 0x06016841 RID: 92225 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170076A7 RID: 30375
		// (get) Token: 0x06016842 RID: 92226 RVA: 0x002E3130 File Offset: 0x002E1330
		// (set) Token: 0x06016843 RID: 92227 RVA: 0x002E3139 File Offset: 0x002E1339
		public SheetProperties SheetProperties
		{
			get
			{
				return base.GetElement<SheetProperties>(0);
			}
			set
			{
				base.SetElement<SheetProperties>(0, value);
			}
		}

		// Token: 0x170076A8 RID: 30376
		// (get) Token: 0x06016844 RID: 92228 RVA: 0x002E3143 File Offset: 0x002E1343
		// (set) Token: 0x06016845 RID: 92229 RVA: 0x002E314C File Offset: 0x002E134C
		public SheetDimension SheetDimension
		{
			get
			{
				return base.GetElement<SheetDimension>(1);
			}
			set
			{
				base.SetElement<SheetDimension>(1, value);
			}
		}

		// Token: 0x170076A9 RID: 30377
		// (get) Token: 0x06016846 RID: 92230 RVA: 0x002E3156 File Offset: 0x002E1356
		// (set) Token: 0x06016847 RID: 92231 RVA: 0x002E315F File Offset: 0x002E135F
		public SheetViews SheetViews
		{
			get
			{
				return base.GetElement<SheetViews>(2);
			}
			set
			{
				base.SetElement<SheetViews>(2, value);
			}
		}

		// Token: 0x170076AA RID: 30378
		// (get) Token: 0x06016848 RID: 92232 RVA: 0x002E3169 File Offset: 0x002E1369
		// (set) Token: 0x06016849 RID: 92233 RVA: 0x002E3172 File Offset: 0x002E1372
		public SheetFormatProperties SheetFormatProperties
		{
			get
			{
				return base.GetElement<SheetFormatProperties>(3);
			}
			set
			{
				base.SetElement<SheetFormatProperties>(3, value);
			}
		}

		// Token: 0x0601684A RID: 92234 RVA: 0x0032B9FC File Offset: 0x00329BFC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Worksheet>(deep);
		}

		// Token: 0x04009906 RID: 39174
		private const string tagName = "worksheet";

		// Token: 0x04009907 RID: 39175
		private const byte tagNsId = 22;

		// Token: 0x04009908 RID: 39176
		internal const int ElementTypeIdConst = 11039;

		// Token: 0x04009909 RID: 39177
		private static readonly string[] eleTagNames = new string[]
		{
			"sheetPr", "dimension", "sheetViews", "sheetFormatPr", "cols", "sheetData", "sheetCalcPr", "sheetProtection", "protectedRanges", "scenarios",
			"autoFilter", "sortState", "dataConsolidate", "customSheetViews", "mergeCells", "phoneticPr", "conditionalFormatting", "dataValidations", "hyperlinks", "printOptions",
			"pageMargins", "pageSetup", "headerFooter", "rowBreaks", "colBreaks", "customProperties", "cellWatches", "ignoredErrors", "smartTags", "drawing",
			"legacyDrawing", "legacyDrawingHF", "drawingHF", "picture", "oleObjects", "controls", "webPublishItems", "tableParts", "extLst"
		};

		// Token: 0x0400990A RID: 39178
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22
		};
	}
}
