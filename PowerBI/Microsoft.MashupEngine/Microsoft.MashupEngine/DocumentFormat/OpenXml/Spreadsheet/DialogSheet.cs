using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B23 RID: 11043
	[ChildElementInfo(typeof(DrawingHeaderFooter), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(SheetProperties))]
	[ChildElementInfo(typeof(SheetViews))]
	[ChildElementInfo(typeof(SheetFormatProperties))]
	[ChildElementInfo(typeof(SheetProtection))]
	[ChildElementInfo(typeof(CustomSheetViews))]
	[ChildElementInfo(typeof(PrintOptions))]
	[ChildElementInfo(typeof(LegacyDrawing))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(Drawing))]
	[ChildElementInfo(typeof(PageSetup))]
	[ChildElementInfo(typeof(LegacyDrawingHeaderFooter))]
	[ChildElementInfo(typeof(OleObjects))]
	[ChildElementInfo(typeof(Controls), FileFormatVersions.Office2010)]
	internal class DialogSheet : OpenXmlPartRootElement
	{
		// Token: 0x170076C0 RID: 30400
		// (get) Token: 0x0601687B RID: 92283 RVA: 0x0032BEDF File Offset: 0x0032A0DF
		public override string LocalName
		{
			get
			{
				return "dialogsheet";
			}
		}

		// Token: 0x170076C1 RID: 30401
		// (get) Token: 0x0601687C RID: 92284 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076C2 RID: 30402
		// (get) Token: 0x0601687D RID: 92285 RVA: 0x0032BEE6 File Offset: 0x0032A0E6
		internal override int ElementTypeId
		{
			get
			{
				return 11041;
			}
		}

		// Token: 0x0601687E RID: 92286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601687F RID: 92287 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal DialogSheet(DialogsheetPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016880 RID: 92288 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DialogsheetPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076C3 RID: 30403
		// (get) Token: 0x06016881 RID: 92289 RVA: 0x0032BEED File Offset: 0x0032A0ED
		// (set) Token: 0x06016882 RID: 92290 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DialogsheetPart DialogsheetPart
		{
			get
			{
				return base.OpenXmlPart as DialogsheetPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016883 RID: 92291 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public DialogSheet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016884 RID: 92292 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public DialogSheet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016885 RID: 92293 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public DialogSheet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016886 RID: 92294 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public DialogSheet()
		{
		}

		// Token: 0x06016887 RID: 92295 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DialogsheetPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016888 RID: 92296 RVA: 0x0032BEFC File Offset: 0x0032A0FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetPr" == name)
			{
				return new SheetProperties();
			}
			if (22 == namespaceId && "sheetViews" == name)
			{
				return new SheetViews();
			}
			if (22 == namespaceId && "sheetFormatPr" == name)
			{
				return new SheetFormatProperties();
			}
			if (22 == namespaceId && "sheetProtection" == name)
			{
				return new SheetProtection();
			}
			if (22 == namespaceId && "customSheetViews" == name)
			{
				return new CustomSheetViews();
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
			if (22 == namespaceId && "oleObjects" == name)
			{
				return new OleObjects();
			}
			if (22 == namespaceId && "controls" == name)
			{
				return new Controls();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170076C4 RID: 30404
		// (get) Token: 0x06016889 RID: 92297 RVA: 0x0032C08A File Offset: 0x0032A28A
		internal override string[] ElementTagNames
		{
			get
			{
				return DialogSheet.eleTagNames;
			}
		}

		// Token: 0x170076C5 RID: 30405
		// (get) Token: 0x0601688A RID: 92298 RVA: 0x0032C091 File Offset: 0x0032A291
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DialogSheet.eleNamespaceIds;
			}
		}

		// Token: 0x170076C6 RID: 30406
		// (get) Token: 0x0601688B RID: 92299 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170076C7 RID: 30407
		// (get) Token: 0x0601688C RID: 92300 RVA: 0x002E3130 File Offset: 0x002E1330
		// (set) Token: 0x0601688D RID: 92301 RVA: 0x002E3139 File Offset: 0x002E1339
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

		// Token: 0x170076C8 RID: 30408
		// (get) Token: 0x0601688E RID: 92302 RVA: 0x0032C098 File Offset: 0x0032A298
		// (set) Token: 0x0601688F RID: 92303 RVA: 0x0032C0A1 File Offset: 0x0032A2A1
		public SheetViews SheetViews
		{
			get
			{
				return base.GetElement<SheetViews>(1);
			}
			set
			{
				base.SetElement<SheetViews>(1, value);
			}
		}

		// Token: 0x170076C9 RID: 30409
		// (get) Token: 0x06016890 RID: 92304 RVA: 0x0032C0AB File Offset: 0x0032A2AB
		// (set) Token: 0x06016891 RID: 92305 RVA: 0x0032C0B4 File Offset: 0x0032A2B4
		public SheetFormatProperties SheetFormatProperties
		{
			get
			{
				return base.GetElement<SheetFormatProperties>(2);
			}
			set
			{
				base.SetElement<SheetFormatProperties>(2, value);
			}
		}

		// Token: 0x170076CA RID: 30410
		// (get) Token: 0x06016892 RID: 92306 RVA: 0x0032C0BE File Offset: 0x0032A2BE
		// (set) Token: 0x06016893 RID: 92307 RVA: 0x0032C0C7 File Offset: 0x0032A2C7
		public SheetProtection SheetProtection
		{
			get
			{
				return base.GetElement<SheetProtection>(3);
			}
			set
			{
				base.SetElement<SheetProtection>(3, value);
			}
		}

		// Token: 0x170076CB RID: 30411
		// (get) Token: 0x06016894 RID: 92308 RVA: 0x0032C0D1 File Offset: 0x0032A2D1
		// (set) Token: 0x06016895 RID: 92309 RVA: 0x0032C0DA File Offset: 0x0032A2DA
		public CustomSheetViews CustomSheetViews
		{
			get
			{
				return base.GetElement<CustomSheetViews>(4);
			}
			set
			{
				base.SetElement<CustomSheetViews>(4, value);
			}
		}

		// Token: 0x170076CC RID: 30412
		// (get) Token: 0x06016896 RID: 92310 RVA: 0x0032C0E4 File Offset: 0x0032A2E4
		// (set) Token: 0x06016897 RID: 92311 RVA: 0x0032C0ED File Offset: 0x0032A2ED
		public PrintOptions PrintOptions
		{
			get
			{
				return base.GetElement<PrintOptions>(5);
			}
			set
			{
				base.SetElement<PrintOptions>(5, value);
			}
		}

		// Token: 0x170076CD RID: 30413
		// (get) Token: 0x06016898 RID: 92312 RVA: 0x0032C0F7 File Offset: 0x0032A2F7
		// (set) Token: 0x06016899 RID: 92313 RVA: 0x0032C100 File Offset: 0x0032A300
		public PageMargins PageMargins
		{
			get
			{
				return base.GetElement<PageMargins>(6);
			}
			set
			{
				base.SetElement<PageMargins>(6, value);
			}
		}

		// Token: 0x170076CE RID: 30414
		// (get) Token: 0x0601689A RID: 92314 RVA: 0x0032C10A File Offset: 0x0032A30A
		// (set) Token: 0x0601689B RID: 92315 RVA: 0x0032C113 File Offset: 0x0032A313
		public PageSetup PageSetup
		{
			get
			{
				return base.GetElement<PageSetup>(7);
			}
			set
			{
				base.SetElement<PageSetup>(7, value);
			}
		}

		// Token: 0x170076CF RID: 30415
		// (get) Token: 0x0601689C RID: 92316 RVA: 0x0032C11D File Offset: 0x0032A31D
		// (set) Token: 0x0601689D RID: 92317 RVA: 0x0032C126 File Offset: 0x0032A326
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(8);
			}
			set
			{
				base.SetElement<HeaderFooter>(8, value);
			}
		}

		// Token: 0x170076D0 RID: 30416
		// (get) Token: 0x0601689E RID: 92318 RVA: 0x0032C130 File Offset: 0x0032A330
		// (set) Token: 0x0601689F RID: 92319 RVA: 0x0032C13A File Offset: 0x0032A33A
		public Drawing Drawing
		{
			get
			{
				return base.GetElement<Drawing>(9);
			}
			set
			{
				base.SetElement<Drawing>(9, value);
			}
		}

		// Token: 0x170076D1 RID: 30417
		// (get) Token: 0x060168A0 RID: 92320 RVA: 0x0032C145 File Offset: 0x0032A345
		// (set) Token: 0x060168A1 RID: 92321 RVA: 0x0032C14F File Offset: 0x0032A34F
		public LegacyDrawing LegacyDrawing
		{
			get
			{
				return base.GetElement<LegacyDrawing>(10);
			}
			set
			{
				base.SetElement<LegacyDrawing>(10, value);
			}
		}

		// Token: 0x170076D2 RID: 30418
		// (get) Token: 0x060168A2 RID: 92322 RVA: 0x0032C15A File Offset: 0x0032A35A
		// (set) Token: 0x060168A3 RID: 92323 RVA: 0x0032C164 File Offset: 0x0032A364
		public LegacyDrawingHeaderFooter LegacyDrawingHeaderFooter
		{
			get
			{
				return base.GetElement<LegacyDrawingHeaderFooter>(11);
			}
			set
			{
				base.SetElement<LegacyDrawingHeaderFooter>(11, value);
			}
		}

		// Token: 0x170076D3 RID: 30419
		// (get) Token: 0x060168A4 RID: 92324 RVA: 0x0032C16F File Offset: 0x0032A36F
		// (set) Token: 0x060168A5 RID: 92325 RVA: 0x0032C179 File Offset: 0x0032A379
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public DrawingHeaderFooter DrawingHeaderFooter
		{
			get
			{
				return base.GetElement<DrawingHeaderFooter>(12);
			}
			set
			{
				base.SetElement<DrawingHeaderFooter>(12, value);
			}
		}

		// Token: 0x170076D4 RID: 30420
		// (get) Token: 0x060168A6 RID: 92326 RVA: 0x0032C184 File Offset: 0x0032A384
		// (set) Token: 0x060168A7 RID: 92327 RVA: 0x0032C18E File Offset: 0x0032A38E
		public OleObjects OleObjects
		{
			get
			{
				return base.GetElement<OleObjects>(13);
			}
			set
			{
				base.SetElement<OleObjects>(13, value);
			}
		}

		// Token: 0x170076D5 RID: 30421
		// (get) Token: 0x060168A8 RID: 92328 RVA: 0x0032C199 File Offset: 0x0032A399
		// (set) Token: 0x060168A9 RID: 92329 RVA: 0x0032C1A3 File Offset: 0x0032A3A3
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Controls Controls
		{
			get
			{
				return base.GetElement<Controls>(14);
			}
			set
			{
				base.SetElement<Controls>(14, value);
			}
		}

		// Token: 0x170076D6 RID: 30422
		// (get) Token: 0x060168AA RID: 92330 RVA: 0x0032C1AE File Offset: 0x0032A3AE
		// (set) Token: 0x060168AB RID: 92331 RVA: 0x0032C1B8 File Offset: 0x0032A3B8
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(15);
			}
			set
			{
				base.SetElement<ExtensionList>(15, value);
			}
		}

		// Token: 0x060168AC RID: 92332 RVA: 0x0032C1C3 File Offset: 0x0032A3C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DialogSheet>(deep);
		}

		// Token: 0x04009910 RID: 39184
		private const string tagName = "dialogsheet";

		// Token: 0x04009911 RID: 39185
		private const byte tagNsId = 22;

		// Token: 0x04009912 RID: 39186
		internal const int ElementTypeIdConst = 11041;

		// Token: 0x04009913 RID: 39187
		private static readonly string[] eleTagNames = new string[]
		{
			"sheetPr", "sheetViews", "sheetFormatPr", "sheetProtection", "customSheetViews", "printOptions", "pageMargins", "pageSetup", "headerFooter", "drawing",
			"legacyDrawing", "legacyDrawingHF", "drawingHF", "oleObjects", "controls", "extLst"
		};

		// Token: 0x04009914 RID: 39188
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22
		};
	}
}
