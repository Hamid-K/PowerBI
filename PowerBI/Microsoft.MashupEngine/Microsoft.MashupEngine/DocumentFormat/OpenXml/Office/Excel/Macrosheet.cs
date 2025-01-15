using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x0200237D RID: 9085
	[ChildElementInfo(typeof(DataConsolidate))]
	[ChildElementInfo(typeof(AutoFilter))]
	[ChildElementInfo(typeof(SheetProperties))]
	[ChildElementInfo(typeof(CustomSheetViews))]
	[ChildElementInfo(typeof(PhoneticProperties))]
	[ChildElementInfo(typeof(ConditionalFormatting))]
	[ChildElementInfo(typeof(PrintOptions))]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(PageSetup))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(RowBreaks))]
	[ChildElementInfo(typeof(ColumnBreaks))]
	[ChildElementInfo(typeof(CustomProperties))]
	[ChildElementInfo(typeof(Drawing))]
	[ChildElementInfo(typeof(LegacyDrawing))]
	[ChildElementInfo(typeof(LegacyDrawingHeaderFooter))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(OleObjects))]
	[ChildElementInfo(typeof(DrawingHeaderFooter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SortState))]
	[ChildElementInfo(typeof(SheetDimension))]
	[ChildElementInfo(typeof(SheetViews))]
	[ChildElementInfo(typeof(SheetFormatProperties))]
	[ChildElementInfo(typeof(Columns))]
	[ChildElementInfo(typeof(SheetData))]
	[ChildElementInfo(typeof(SheetProtection))]
	internal class Macrosheet : OpenXmlPartRootElement
	{
		// Token: 0x17004B36 RID: 19254
		// (get) Token: 0x06010631 RID: 67121 RVA: 0x002E2E6E File Offset: 0x002E106E
		public override string LocalName
		{
			get
			{
				return "macrosheet";
			}
		}

		// Token: 0x17004B37 RID: 19255
		// (get) Token: 0x06010632 RID: 67122 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B38 RID: 19256
		// (get) Token: 0x06010633 RID: 67123 RVA: 0x002E2E75 File Offset: 0x002E1075
		internal override int ElementTypeId
		{
			get
			{
				return 12529;
			}
		}

		// Token: 0x06010634 RID: 67124 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06010635 RID: 67125 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Macrosheet(MacroSheetPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010636 RID: 67126 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(MacroSheetPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004B39 RID: 19257
		// (get) Token: 0x06010637 RID: 67127 RVA: 0x002E2E7C File Offset: 0x002E107C
		// (set) Token: 0x06010638 RID: 67128 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public MacroSheetPart MacroSheetPart
		{
			get
			{
				return base.OpenXmlPart as MacroSheetPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010639 RID: 67129 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Macrosheet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601063A RID: 67130 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Macrosheet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601063B RID: 67131 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Macrosheet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601063C RID: 67132 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Macrosheet()
		{
		}

		// Token: 0x0601063D RID: 67133 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(MacroSheetPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601063E RID: 67134 RVA: 0x002E2E8C File Offset: 0x002E108C
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
			if (22 == namespaceId && "sheetProtection" == name)
			{
				return new SheetProtection();
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
			if (22 == namespaceId && "phoneticPr" == name)
			{
				return new PhoneticProperties();
			}
			if (22 == namespaceId && "conditionalFormatting" == name)
			{
				return new ConditionalFormatting();
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
			if (22 == namespaceId && "picture" == name)
			{
				return new Picture();
			}
			if (22 == namespaceId && "oleObjects" == name)
			{
				return new OleObjects();
			}
			if (22 == namespaceId && "drawingHF" == name)
			{
				return new DrawingHeaderFooter();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004B3A RID: 19258
		// (get) Token: 0x0601063F RID: 67135 RVA: 0x002E3122 File Offset: 0x002E1322
		internal override string[] ElementTagNames
		{
			get
			{
				return Macrosheet.eleTagNames;
			}
		}

		// Token: 0x17004B3B RID: 19259
		// (get) Token: 0x06010640 RID: 67136 RVA: 0x002E3129 File Offset: 0x002E1329
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Macrosheet.eleNamespaceIds;
			}
		}

		// Token: 0x17004B3C RID: 19260
		// (get) Token: 0x06010641 RID: 67137 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B3D RID: 19261
		// (get) Token: 0x06010642 RID: 67138 RVA: 0x002E3130 File Offset: 0x002E1330
		// (set) Token: 0x06010643 RID: 67139 RVA: 0x002E3139 File Offset: 0x002E1339
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

		// Token: 0x17004B3E RID: 19262
		// (get) Token: 0x06010644 RID: 67140 RVA: 0x002E3143 File Offset: 0x002E1343
		// (set) Token: 0x06010645 RID: 67141 RVA: 0x002E314C File Offset: 0x002E134C
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

		// Token: 0x17004B3F RID: 19263
		// (get) Token: 0x06010646 RID: 67142 RVA: 0x002E3156 File Offset: 0x002E1356
		// (set) Token: 0x06010647 RID: 67143 RVA: 0x002E315F File Offset: 0x002E135F
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

		// Token: 0x17004B40 RID: 19264
		// (get) Token: 0x06010648 RID: 67144 RVA: 0x002E3169 File Offset: 0x002E1369
		// (set) Token: 0x06010649 RID: 67145 RVA: 0x002E3172 File Offset: 0x002E1372
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

		// Token: 0x0601064A RID: 67146 RVA: 0x002E317C File Offset: 0x002E137C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Macrosheet>(deep);
		}

		// Token: 0x04007464 RID: 29796
		private const string tagName = "macrosheet";

		// Token: 0x04007465 RID: 29797
		private const byte tagNsId = 32;

		// Token: 0x04007466 RID: 29798
		internal const int ElementTypeIdConst = 12529;

		// Token: 0x04007467 RID: 29799
		private static readonly string[] eleTagNames = new string[]
		{
			"sheetPr", "dimension", "sheetViews", "sheetFormatPr", "cols", "sheetData", "sheetProtection", "autoFilter", "sortState", "dataConsolidate",
			"customSheetViews", "phoneticPr", "conditionalFormatting", "printOptions", "pageMargins", "pageSetup", "headerFooter", "rowBreaks", "colBreaks", "customProperties",
			"drawing", "legacyDrawing", "legacyDrawingHF", "picture", "oleObjects", "drawingHF", "extLst"
		};

		// Token: 0x04007468 RID: 29800
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22
		};
	}
}
