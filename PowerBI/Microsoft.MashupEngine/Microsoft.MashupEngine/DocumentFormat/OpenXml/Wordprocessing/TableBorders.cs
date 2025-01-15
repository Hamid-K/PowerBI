using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F09 RID: 12041
	[ChildElementInfo(typeof(InsideHorizontalBorder))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EndBorder), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InsideVerticalBorder))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(StartBorder), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	internal class TableBorders : OpenXmlCompositeElement
	{
		// Token: 0x17008DF8 RID: 36344
		// (get) Token: 0x06019B43 RID: 105283 RVA: 0x0035417D File Offset: 0x0035237D
		public override string LocalName
		{
			get
			{
				return "tblBorders";
			}
		}

		// Token: 0x17008DF9 RID: 36345
		// (get) Token: 0x06019B44 RID: 105284 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DFA RID: 36346
		// (get) Token: 0x06019B45 RID: 105285 RVA: 0x00354184 File Offset: 0x00352384
		internal override int ElementTypeId
		{
			get
			{
				return 11679;
			}
		}

		// Token: 0x06019B46 RID: 105286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019B47 RID: 105287 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableBorders()
		{
		}

		// Token: 0x06019B48 RID: 105288 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableBorders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019B49 RID: 105289 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableBorders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019B4A RID: 105290 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableBorders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019B4B RID: 105291 RVA: 0x0035418C File Offset: 0x0035238C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (23 == namespaceId && "start" == name)
			{
				return new StartBorder();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			if (23 == namespaceId && "end" == name)
			{
				return new EndBorder();
			}
			if (23 == namespaceId && "insideH" == name)
			{
				return new InsideHorizontalBorder();
			}
			if (23 == namespaceId && "insideV" == name)
			{
				return new InsideVerticalBorder();
			}
			return null;
		}

		// Token: 0x17008DFB RID: 36347
		// (get) Token: 0x06019B4C RID: 105292 RVA: 0x0035425A File Offset: 0x0035245A
		internal override string[] ElementTagNames
		{
			get
			{
				return TableBorders.eleTagNames;
			}
		}

		// Token: 0x17008DFC RID: 36348
		// (get) Token: 0x06019B4D RID: 105293 RVA: 0x00354261 File Offset: 0x00352461
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableBorders.eleNamespaceIds;
			}
		}

		// Token: 0x17008DFD RID: 36349
		// (get) Token: 0x06019B4E RID: 105294 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008DFE RID: 36350
		// (get) Token: 0x06019B4F RID: 105295 RVA: 0x00345F0C File Offset: 0x0034410C
		// (set) Token: 0x06019B50 RID: 105296 RVA: 0x00345F15 File Offset: 0x00344115
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(0);
			}
			set
			{
				base.SetElement<TopBorder>(0, value);
			}
		}

		// Token: 0x17008DFF RID: 36351
		// (get) Token: 0x06019B51 RID: 105297 RVA: 0x00345F1F File Offset: 0x0034411F
		// (set) Token: 0x06019B52 RID: 105298 RVA: 0x00345F28 File Offset: 0x00344128
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(1);
			}
			set
			{
				base.SetElement<LeftBorder>(1, value);
			}
		}

		// Token: 0x17008E00 RID: 36352
		// (get) Token: 0x06019B53 RID: 105299 RVA: 0x003536C4 File Offset: 0x003518C4
		// (set) Token: 0x06019B54 RID: 105300 RVA: 0x003536CD File Offset: 0x003518CD
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StartBorder StartBorder
		{
			get
			{
				return base.GetElement<StartBorder>(2);
			}
			set
			{
				base.SetElement<StartBorder>(2, value);
			}
		}

		// Token: 0x17008E01 RID: 36353
		// (get) Token: 0x06019B55 RID: 105301 RVA: 0x003536D7 File Offset: 0x003518D7
		// (set) Token: 0x06019B56 RID: 105302 RVA: 0x003536E0 File Offset: 0x003518E0
		public BottomBorder BottomBorder
		{
			get
			{
				return base.GetElement<BottomBorder>(3);
			}
			set
			{
				base.SetElement<BottomBorder>(3, value);
			}
		}

		// Token: 0x17008E02 RID: 36354
		// (get) Token: 0x06019B57 RID: 105303 RVA: 0x003536EA File Offset: 0x003518EA
		// (set) Token: 0x06019B58 RID: 105304 RVA: 0x003536F3 File Offset: 0x003518F3
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(4);
			}
			set
			{
				base.SetElement<RightBorder>(4, value);
			}
		}

		// Token: 0x17008E03 RID: 36355
		// (get) Token: 0x06019B59 RID: 105305 RVA: 0x003536FD File Offset: 0x003518FD
		// (set) Token: 0x06019B5A RID: 105306 RVA: 0x00353706 File Offset: 0x00351906
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public EndBorder EndBorder
		{
			get
			{
				return base.GetElement<EndBorder>(5);
			}
			set
			{
				base.SetElement<EndBorder>(5, value);
			}
		}

		// Token: 0x17008E04 RID: 36356
		// (get) Token: 0x06019B5B RID: 105307 RVA: 0x00353710 File Offset: 0x00351910
		// (set) Token: 0x06019B5C RID: 105308 RVA: 0x00353719 File Offset: 0x00351919
		public InsideHorizontalBorder InsideHorizontalBorder
		{
			get
			{
				return base.GetElement<InsideHorizontalBorder>(6);
			}
			set
			{
				base.SetElement<InsideHorizontalBorder>(6, value);
			}
		}

		// Token: 0x17008E05 RID: 36357
		// (get) Token: 0x06019B5D RID: 105309 RVA: 0x00353723 File Offset: 0x00351923
		// (set) Token: 0x06019B5E RID: 105310 RVA: 0x0035372C File Offset: 0x0035192C
		public InsideVerticalBorder InsideVerticalBorder
		{
			get
			{
				return base.GetElement<InsideVerticalBorder>(7);
			}
			set
			{
				base.SetElement<InsideVerticalBorder>(7, value);
			}
		}

		// Token: 0x06019B5F RID: 105311 RVA: 0x00354268 File Offset: 0x00352468
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableBorders>(deep);
		}

		// Token: 0x0400AA37 RID: 43575
		private const string tagName = "tblBorders";

		// Token: 0x0400AA38 RID: 43576
		private const byte tagNsId = 23;

		// Token: 0x0400AA39 RID: 43577
		internal const int ElementTypeIdConst = 11679;

		// Token: 0x0400AA3A RID: 43578
		private static readonly string[] eleTagNames = new string[] { "top", "left", "start", "bottom", "right", "end", "insideH", "insideV" };

		// Token: 0x0400AA3B RID: 43579
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
