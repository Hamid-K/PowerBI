using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B22 RID: 11042
	[ChildElementInfo(typeof(Drawing))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ChartSheetPageSetup))]
	[ChildElementInfo(typeof(ChartSheetProperties))]
	[ChildElementInfo(typeof(ChartSheetViews))]
	[ChildElementInfo(typeof(ChartSheetProtection))]
	[ChildElementInfo(typeof(CustomChartsheetViews))]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(LegacyDrawingHeaderFooter))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(LegacyDrawing))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(DrawingHeaderFooter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WebPublishItems))]
	internal class Chartsheet : OpenXmlPartRootElement
	{
		// Token: 0x170076AB RID: 30379
		// (get) Token: 0x0601684C RID: 92236 RVA: 0x0032BB90 File Offset: 0x00329D90
		public override string LocalName
		{
			get
			{
				return "chartsheet";
			}
		}

		// Token: 0x170076AC RID: 30380
		// (get) Token: 0x0601684D RID: 92237 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076AD RID: 30381
		// (get) Token: 0x0601684E RID: 92238 RVA: 0x0032BB97 File Offset: 0x00329D97
		internal override int ElementTypeId
		{
			get
			{
				return 11040;
			}
		}

		// Token: 0x0601684F RID: 92239 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016850 RID: 92240 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Chartsheet(ChartsheetPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016851 RID: 92241 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ChartsheetPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076AE RID: 30382
		// (get) Token: 0x06016852 RID: 92242 RVA: 0x0032BB9E File Offset: 0x00329D9E
		// (set) Token: 0x06016853 RID: 92243 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ChartsheetPart ChartsheetPart
		{
			get
			{
				return base.OpenXmlPart as ChartsheetPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016854 RID: 92244 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Chartsheet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016855 RID: 92245 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Chartsheet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016856 RID: 92246 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Chartsheet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016857 RID: 92247 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Chartsheet()
		{
		}

		// Token: 0x06016858 RID: 92248 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ChartsheetPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016859 RID: 92249 RVA: 0x0032BBAC File Offset: 0x00329DAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetPr" == name)
			{
				return new ChartSheetProperties();
			}
			if (22 == namespaceId && "sheetViews" == name)
			{
				return new ChartSheetViews();
			}
			if (22 == namespaceId && "sheetProtection" == name)
			{
				return new ChartSheetProtection();
			}
			if (22 == namespaceId && "customSheetViews" == name)
			{
				return new CustomChartsheetViews();
			}
			if (22 == namespaceId && "pageMargins" == name)
			{
				return new PageMargins();
			}
			if (22 == namespaceId && "pageSetup" == name)
			{
				return new ChartSheetPageSetup();
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
			if (22 == namespaceId && "picture" == name)
			{
				return new Picture();
			}
			if (22 == namespaceId && "webPublishItems" == name)
			{
				return new WebPublishItems();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170076AF RID: 30383
		// (get) Token: 0x0601685A RID: 92250 RVA: 0x0032BD0A File Offset: 0x00329F0A
		internal override string[] ElementTagNames
		{
			get
			{
				return Chartsheet.eleTagNames;
			}
		}

		// Token: 0x170076B0 RID: 30384
		// (get) Token: 0x0601685B RID: 92251 RVA: 0x0032BD11 File Offset: 0x00329F11
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Chartsheet.eleNamespaceIds;
			}
		}

		// Token: 0x170076B1 RID: 30385
		// (get) Token: 0x0601685C RID: 92252 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170076B2 RID: 30386
		// (get) Token: 0x0601685D RID: 92253 RVA: 0x0032BD18 File Offset: 0x00329F18
		// (set) Token: 0x0601685E RID: 92254 RVA: 0x0032BD21 File Offset: 0x00329F21
		public ChartSheetProperties ChartSheetProperties
		{
			get
			{
				return base.GetElement<ChartSheetProperties>(0);
			}
			set
			{
				base.SetElement<ChartSheetProperties>(0, value);
			}
		}

		// Token: 0x170076B3 RID: 30387
		// (get) Token: 0x0601685F RID: 92255 RVA: 0x0032BD2B File Offset: 0x00329F2B
		// (set) Token: 0x06016860 RID: 92256 RVA: 0x0032BD34 File Offset: 0x00329F34
		public ChartSheetViews ChartSheetViews
		{
			get
			{
				return base.GetElement<ChartSheetViews>(1);
			}
			set
			{
				base.SetElement<ChartSheetViews>(1, value);
			}
		}

		// Token: 0x170076B4 RID: 30388
		// (get) Token: 0x06016861 RID: 92257 RVA: 0x0032BD3E File Offset: 0x00329F3E
		// (set) Token: 0x06016862 RID: 92258 RVA: 0x0032BD47 File Offset: 0x00329F47
		public ChartSheetProtection ChartSheetProtection
		{
			get
			{
				return base.GetElement<ChartSheetProtection>(2);
			}
			set
			{
				base.SetElement<ChartSheetProtection>(2, value);
			}
		}

		// Token: 0x170076B5 RID: 30389
		// (get) Token: 0x06016863 RID: 92259 RVA: 0x0032BD51 File Offset: 0x00329F51
		// (set) Token: 0x06016864 RID: 92260 RVA: 0x0032BD5A File Offset: 0x00329F5A
		public CustomChartsheetViews CustomChartsheetViews
		{
			get
			{
				return base.GetElement<CustomChartsheetViews>(3);
			}
			set
			{
				base.SetElement<CustomChartsheetViews>(3, value);
			}
		}

		// Token: 0x170076B6 RID: 30390
		// (get) Token: 0x06016865 RID: 92261 RVA: 0x0032BD64 File Offset: 0x00329F64
		// (set) Token: 0x06016866 RID: 92262 RVA: 0x0032BD6D File Offset: 0x00329F6D
		public PageMargins PageMargins
		{
			get
			{
				return base.GetElement<PageMargins>(4);
			}
			set
			{
				base.SetElement<PageMargins>(4, value);
			}
		}

		// Token: 0x170076B7 RID: 30391
		// (get) Token: 0x06016867 RID: 92263 RVA: 0x0032BD77 File Offset: 0x00329F77
		// (set) Token: 0x06016868 RID: 92264 RVA: 0x0032BD80 File Offset: 0x00329F80
		public ChartSheetPageSetup ChartSheetPageSetup
		{
			get
			{
				return base.GetElement<ChartSheetPageSetup>(5);
			}
			set
			{
				base.SetElement<ChartSheetPageSetup>(5, value);
			}
		}

		// Token: 0x170076B8 RID: 30392
		// (get) Token: 0x06016869 RID: 92265 RVA: 0x0032BD8A File Offset: 0x00329F8A
		// (set) Token: 0x0601686A RID: 92266 RVA: 0x0032BD93 File Offset: 0x00329F93
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(6);
			}
			set
			{
				base.SetElement<HeaderFooter>(6, value);
			}
		}

		// Token: 0x170076B9 RID: 30393
		// (get) Token: 0x0601686B RID: 92267 RVA: 0x0032BD9D File Offset: 0x00329F9D
		// (set) Token: 0x0601686C RID: 92268 RVA: 0x0032BDA6 File Offset: 0x00329FA6
		public Drawing Drawing
		{
			get
			{
				return base.GetElement<Drawing>(7);
			}
			set
			{
				base.SetElement<Drawing>(7, value);
			}
		}

		// Token: 0x170076BA RID: 30394
		// (get) Token: 0x0601686D RID: 92269 RVA: 0x0032BDB0 File Offset: 0x00329FB0
		// (set) Token: 0x0601686E RID: 92270 RVA: 0x0032BDB9 File Offset: 0x00329FB9
		public LegacyDrawing LegacyDrawing
		{
			get
			{
				return base.GetElement<LegacyDrawing>(8);
			}
			set
			{
				base.SetElement<LegacyDrawing>(8, value);
			}
		}

		// Token: 0x170076BB RID: 30395
		// (get) Token: 0x0601686F RID: 92271 RVA: 0x0032BDC3 File Offset: 0x00329FC3
		// (set) Token: 0x06016870 RID: 92272 RVA: 0x0032BDCD File Offset: 0x00329FCD
		public LegacyDrawingHeaderFooter LegacyDrawingHeaderFooter
		{
			get
			{
				return base.GetElement<LegacyDrawingHeaderFooter>(9);
			}
			set
			{
				base.SetElement<LegacyDrawingHeaderFooter>(9, value);
			}
		}

		// Token: 0x170076BC RID: 30396
		// (get) Token: 0x06016871 RID: 92273 RVA: 0x0032BDD8 File Offset: 0x00329FD8
		// (set) Token: 0x06016872 RID: 92274 RVA: 0x0032BDE2 File Offset: 0x00329FE2
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public DrawingHeaderFooter DrawingHeaderFooter
		{
			get
			{
				return base.GetElement<DrawingHeaderFooter>(10);
			}
			set
			{
				base.SetElement<DrawingHeaderFooter>(10, value);
			}
		}

		// Token: 0x170076BD RID: 30397
		// (get) Token: 0x06016873 RID: 92275 RVA: 0x0032BDED File Offset: 0x00329FED
		// (set) Token: 0x06016874 RID: 92276 RVA: 0x0032BDF7 File Offset: 0x00329FF7
		public Picture Picture
		{
			get
			{
				return base.GetElement<Picture>(11);
			}
			set
			{
				base.SetElement<Picture>(11, value);
			}
		}

		// Token: 0x170076BE RID: 30398
		// (get) Token: 0x06016875 RID: 92277 RVA: 0x0032BE02 File Offset: 0x0032A002
		// (set) Token: 0x06016876 RID: 92278 RVA: 0x0032BE0C File Offset: 0x0032A00C
		public WebPublishItems WebPublishItems
		{
			get
			{
				return base.GetElement<WebPublishItems>(12);
			}
			set
			{
				base.SetElement<WebPublishItems>(12, value);
			}
		}

		// Token: 0x170076BF RID: 30399
		// (get) Token: 0x06016877 RID: 92279 RVA: 0x0032BE17 File Offset: 0x0032A017
		// (set) Token: 0x06016878 RID: 92280 RVA: 0x0032BE21 File Offset: 0x0032A021
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(13);
			}
			set
			{
				base.SetElement<ExtensionList>(13, value);
			}
		}

		// Token: 0x06016879 RID: 92281 RVA: 0x0032BE2C File Offset: 0x0032A02C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Chartsheet>(deep);
		}

		// Token: 0x0400990B RID: 39179
		private const string tagName = "chartsheet";

		// Token: 0x0400990C RID: 39180
		private const byte tagNsId = 22;

		// Token: 0x0400990D RID: 39181
		internal const int ElementTypeIdConst = 11040;

		// Token: 0x0400990E RID: 39182
		private static readonly string[] eleTagNames = new string[]
		{
			"sheetPr", "sheetViews", "sheetProtection", "customSheetViews", "pageMargins", "pageSetup", "headerFooter", "drawing", "legacyDrawing", "legacyDrawingHF",
			"drawingHF", "picture", "webPublishItems", "extLst"
		};

		// Token: 0x0400990F RID: 39183
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22
		};
	}
}
