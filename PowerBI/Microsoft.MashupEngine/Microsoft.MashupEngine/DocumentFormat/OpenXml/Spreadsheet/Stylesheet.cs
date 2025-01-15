using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B26 RID: 11046
	[ChildElementInfo(typeof(Fills))]
	[ChildElementInfo(typeof(NumberingFormats))]
	[ChildElementInfo(typeof(Fonts))]
	[ChildElementInfo(typeof(Colors))]
	[ChildElementInfo(typeof(Borders))]
	[ChildElementInfo(typeof(CellStyleFormats))]
	[ChildElementInfo(typeof(CellFormats))]
	[ChildElementInfo(typeof(CellStyles))]
	[ChildElementInfo(typeof(StylesheetExtensionList))]
	[ChildElementInfo(typeof(DifferentialFormats))]
	[ChildElementInfo(typeof(TableStyles))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Stylesheet : OpenXmlPartRootElement
	{
		// Token: 0x170076E5 RID: 30437
		// (get) Token: 0x060168D6 RID: 92374 RVA: 0x0032C44B File Offset: 0x0032A64B
		public override string LocalName
		{
			get
			{
				return "styleSheet";
			}
		}

		// Token: 0x170076E6 RID: 30438
		// (get) Token: 0x060168D7 RID: 92375 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076E7 RID: 30439
		// (get) Token: 0x060168D8 RID: 92376 RVA: 0x0032C452 File Offset: 0x0032A652
		internal override int ElementTypeId
		{
			get
			{
				return 11044;
			}
		}

		// Token: 0x060168D9 RID: 92377 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060168DA RID: 92378 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Stylesheet(WorkbookStylesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060168DB RID: 92379 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorkbookStylesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076E8 RID: 30440
		// (get) Token: 0x060168DC RID: 92380 RVA: 0x0032C459 File Offset: 0x0032A659
		// (set) Token: 0x060168DD RID: 92381 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorkbookStylesPart WorkbookStylesPart
		{
			get
			{
				return base.OpenXmlPart as WorkbookStylesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060168DE RID: 92382 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Stylesheet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168DF RID: 92383 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Stylesheet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168E0 RID: 92384 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Stylesheet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060168E1 RID: 92385 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Stylesheet()
		{
		}

		// Token: 0x060168E2 RID: 92386 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorkbookStylesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060168E3 RID: 92387 RVA: 0x0032C468 File Offset: 0x0032A668
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "numFmts" == name)
			{
				return new NumberingFormats();
			}
			if (22 == namespaceId && "fonts" == name)
			{
				return new Fonts();
			}
			if (22 == namespaceId && "fills" == name)
			{
				return new Fills();
			}
			if (22 == namespaceId && "borders" == name)
			{
				return new Borders();
			}
			if (22 == namespaceId && "cellStyleXfs" == name)
			{
				return new CellStyleFormats();
			}
			if (22 == namespaceId && "cellXfs" == name)
			{
				return new CellFormats();
			}
			if (22 == namespaceId && "cellStyles" == name)
			{
				return new CellStyles();
			}
			if (22 == namespaceId && "dxfs" == name)
			{
				return new DifferentialFormats();
			}
			if (22 == namespaceId && "tableStyles" == name)
			{
				return new TableStyles();
			}
			if (22 == namespaceId && "colors" == name)
			{
				return new Colors();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new StylesheetExtensionList();
			}
			return null;
		}

		// Token: 0x170076E9 RID: 30441
		// (get) Token: 0x060168E4 RID: 92388 RVA: 0x0032C57E File Offset: 0x0032A77E
		internal override string[] ElementTagNames
		{
			get
			{
				return Stylesheet.eleTagNames;
			}
		}

		// Token: 0x170076EA RID: 30442
		// (get) Token: 0x060168E5 RID: 92389 RVA: 0x0032C585 File Offset: 0x0032A785
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Stylesheet.eleNamespaceIds;
			}
		}

		// Token: 0x170076EB RID: 30443
		// (get) Token: 0x060168E6 RID: 92390 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170076EC RID: 30444
		// (get) Token: 0x060168E7 RID: 92391 RVA: 0x0032C58C File Offset: 0x0032A78C
		// (set) Token: 0x060168E8 RID: 92392 RVA: 0x0032C595 File Offset: 0x0032A795
		public NumberingFormats NumberingFormats
		{
			get
			{
				return base.GetElement<NumberingFormats>(0);
			}
			set
			{
				base.SetElement<NumberingFormats>(0, value);
			}
		}

		// Token: 0x170076ED RID: 30445
		// (get) Token: 0x060168E9 RID: 92393 RVA: 0x0032C59F File Offset: 0x0032A79F
		// (set) Token: 0x060168EA RID: 92394 RVA: 0x0032C5A8 File Offset: 0x0032A7A8
		public Fonts Fonts
		{
			get
			{
				return base.GetElement<Fonts>(1);
			}
			set
			{
				base.SetElement<Fonts>(1, value);
			}
		}

		// Token: 0x170076EE RID: 30446
		// (get) Token: 0x060168EB RID: 92395 RVA: 0x0032C5B2 File Offset: 0x0032A7B2
		// (set) Token: 0x060168EC RID: 92396 RVA: 0x0032C5BB File Offset: 0x0032A7BB
		public Fills Fills
		{
			get
			{
				return base.GetElement<Fills>(2);
			}
			set
			{
				base.SetElement<Fills>(2, value);
			}
		}

		// Token: 0x170076EF RID: 30447
		// (get) Token: 0x060168ED RID: 92397 RVA: 0x0032C5C5 File Offset: 0x0032A7C5
		// (set) Token: 0x060168EE RID: 92398 RVA: 0x0032C5CE File Offset: 0x0032A7CE
		public Borders Borders
		{
			get
			{
				return base.GetElement<Borders>(3);
			}
			set
			{
				base.SetElement<Borders>(3, value);
			}
		}

		// Token: 0x170076F0 RID: 30448
		// (get) Token: 0x060168EF RID: 92399 RVA: 0x0032C5D8 File Offset: 0x0032A7D8
		// (set) Token: 0x060168F0 RID: 92400 RVA: 0x0032C5E1 File Offset: 0x0032A7E1
		public CellStyleFormats CellStyleFormats
		{
			get
			{
				return base.GetElement<CellStyleFormats>(4);
			}
			set
			{
				base.SetElement<CellStyleFormats>(4, value);
			}
		}

		// Token: 0x170076F1 RID: 30449
		// (get) Token: 0x060168F1 RID: 92401 RVA: 0x0032C5EB File Offset: 0x0032A7EB
		// (set) Token: 0x060168F2 RID: 92402 RVA: 0x0032C5F4 File Offset: 0x0032A7F4
		public CellFormats CellFormats
		{
			get
			{
				return base.GetElement<CellFormats>(5);
			}
			set
			{
				base.SetElement<CellFormats>(5, value);
			}
		}

		// Token: 0x170076F2 RID: 30450
		// (get) Token: 0x060168F3 RID: 92403 RVA: 0x0032C5FE File Offset: 0x0032A7FE
		// (set) Token: 0x060168F4 RID: 92404 RVA: 0x0032C607 File Offset: 0x0032A807
		public CellStyles CellStyles
		{
			get
			{
				return base.GetElement<CellStyles>(6);
			}
			set
			{
				base.SetElement<CellStyles>(6, value);
			}
		}

		// Token: 0x170076F3 RID: 30451
		// (get) Token: 0x060168F5 RID: 92405 RVA: 0x0032C611 File Offset: 0x0032A811
		// (set) Token: 0x060168F6 RID: 92406 RVA: 0x0032C61A File Offset: 0x0032A81A
		public DifferentialFormats DifferentialFormats
		{
			get
			{
				return base.GetElement<DifferentialFormats>(7);
			}
			set
			{
				base.SetElement<DifferentialFormats>(7, value);
			}
		}

		// Token: 0x170076F4 RID: 30452
		// (get) Token: 0x060168F7 RID: 92407 RVA: 0x0032C624 File Offset: 0x0032A824
		// (set) Token: 0x060168F8 RID: 92408 RVA: 0x0032C62D File Offset: 0x0032A82D
		public TableStyles TableStyles
		{
			get
			{
				return base.GetElement<TableStyles>(8);
			}
			set
			{
				base.SetElement<TableStyles>(8, value);
			}
		}

		// Token: 0x170076F5 RID: 30453
		// (get) Token: 0x060168F9 RID: 92409 RVA: 0x0032C637 File Offset: 0x0032A837
		// (set) Token: 0x060168FA RID: 92410 RVA: 0x0032C641 File Offset: 0x0032A841
		public Colors Colors
		{
			get
			{
				return base.GetElement<Colors>(9);
			}
			set
			{
				base.SetElement<Colors>(9, value);
			}
		}

		// Token: 0x170076F6 RID: 30454
		// (get) Token: 0x060168FB RID: 92411 RVA: 0x0032C64C File Offset: 0x0032A84C
		// (set) Token: 0x060168FC RID: 92412 RVA: 0x0032C656 File Offset: 0x0032A856
		public StylesheetExtensionList StylesheetExtensionList
		{
			get
			{
				return base.GetElement<StylesheetExtensionList>(10);
			}
			set
			{
				base.SetElement<StylesheetExtensionList>(10, value);
			}
		}

		// Token: 0x060168FD RID: 92413 RVA: 0x0032C661 File Offset: 0x0032A861
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Stylesheet>(deep);
		}

		// Token: 0x0400991D RID: 39197
		private const string tagName = "styleSheet";

		// Token: 0x0400991E RID: 39198
		private const byte tagNsId = 22;

		// Token: 0x0400991F RID: 39199
		internal const int ElementTypeIdConst = 11044;

		// Token: 0x04009920 RID: 39200
		private static readonly string[] eleTagNames = new string[]
		{
			"numFmts", "fonts", "fills", "borders", "cellStyleXfs", "cellXfs", "cellStyles", "dxfs", "tableStyles", "colors",
			"extLst"
		};

		// Token: 0x04009921 RID: 39201
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22
		};
	}
}
