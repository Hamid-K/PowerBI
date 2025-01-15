using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE8 RID: 12008
	[ChildElementInfo(typeof(TopLeftToBottomRightCellBorder))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(TopRightToBottomLeftCellBorder))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(StartBorder), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(EndBorder), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InsideHorizontalBorder))]
	[ChildElementInfo(typeof(InsideVerticalBorder))]
	internal class TableCellBorders : OpenXmlCompositeElement
	{
		// Token: 0x17008D5F RID: 36191
		// (get) Token: 0x06019A0B RID: 104971 RVA: 0x003535A8 File Offset: 0x003517A8
		public override string LocalName
		{
			get
			{
				return "tcBorders";
			}
		}

		// Token: 0x17008D60 RID: 36192
		// (get) Token: 0x06019A0C RID: 104972 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D61 RID: 36193
		// (get) Token: 0x06019A0D RID: 104973 RVA: 0x003535AF File Offset: 0x003517AF
		internal override int ElementTypeId
		{
			get
			{
				return 11654;
			}
		}

		// Token: 0x06019A0E RID: 104974 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A0F RID: 104975 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellBorders()
		{
		}

		// Token: 0x06019A10 RID: 104976 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellBorders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019A11 RID: 104977 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellBorders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019A12 RID: 104978 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellBorders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019A13 RID: 104979 RVA: 0x003535B8 File Offset: 0x003517B8
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
			if (23 == namespaceId && "tl2br" == name)
			{
				return new TopLeftToBottomRightCellBorder();
			}
			if (23 == namespaceId && "tr2bl" == name)
			{
				return new TopRightToBottomLeftCellBorder();
			}
			return null;
		}

		// Token: 0x17008D62 RID: 36194
		// (get) Token: 0x06019A14 RID: 104980 RVA: 0x003536B6 File Offset: 0x003518B6
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellBorders.eleTagNames;
			}
		}

		// Token: 0x17008D63 RID: 36195
		// (get) Token: 0x06019A15 RID: 104981 RVA: 0x003536BD File Offset: 0x003518BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellBorders.eleNamespaceIds;
			}
		}

		// Token: 0x17008D64 RID: 36196
		// (get) Token: 0x06019A16 RID: 104982 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008D65 RID: 36197
		// (get) Token: 0x06019A17 RID: 104983 RVA: 0x00345F0C File Offset: 0x0034410C
		// (set) Token: 0x06019A18 RID: 104984 RVA: 0x00345F15 File Offset: 0x00344115
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

		// Token: 0x17008D66 RID: 36198
		// (get) Token: 0x06019A19 RID: 104985 RVA: 0x00345F1F File Offset: 0x0034411F
		// (set) Token: 0x06019A1A RID: 104986 RVA: 0x00345F28 File Offset: 0x00344128
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

		// Token: 0x17008D67 RID: 36199
		// (get) Token: 0x06019A1B RID: 104987 RVA: 0x003536C4 File Offset: 0x003518C4
		// (set) Token: 0x06019A1C RID: 104988 RVA: 0x003536CD File Offset: 0x003518CD
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

		// Token: 0x17008D68 RID: 36200
		// (get) Token: 0x06019A1D RID: 104989 RVA: 0x003536D7 File Offset: 0x003518D7
		// (set) Token: 0x06019A1E RID: 104990 RVA: 0x003536E0 File Offset: 0x003518E0
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

		// Token: 0x17008D69 RID: 36201
		// (get) Token: 0x06019A1F RID: 104991 RVA: 0x003536EA File Offset: 0x003518EA
		// (set) Token: 0x06019A20 RID: 104992 RVA: 0x003536F3 File Offset: 0x003518F3
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

		// Token: 0x17008D6A RID: 36202
		// (get) Token: 0x06019A21 RID: 104993 RVA: 0x003536FD File Offset: 0x003518FD
		// (set) Token: 0x06019A22 RID: 104994 RVA: 0x00353706 File Offset: 0x00351906
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

		// Token: 0x17008D6B RID: 36203
		// (get) Token: 0x06019A23 RID: 104995 RVA: 0x00353710 File Offset: 0x00351910
		// (set) Token: 0x06019A24 RID: 104996 RVA: 0x00353719 File Offset: 0x00351919
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

		// Token: 0x17008D6C RID: 36204
		// (get) Token: 0x06019A25 RID: 104997 RVA: 0x00353723 File Offset: 0x00351923
		// (set) Token: 0x06019A26 RID: 104998 RVA: 0x0035372C File Offset: 0x0035192C
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

		// Token: 0x17008D6D RID: 36205
		// (get) Token: 0x06019A27 RID: 104999 RVA: 0x00353736 File Offset: 0x00351936
		// (set) Token: 0x06019A28 RID: 105000 RVA: 0x0035373F File Offset: 0x0035193F
		public TopLeftToBottomRightCellBorder TopLeftToBottomRightCellBorder
		{
			get
			{
				return base.GetElement<TopLeftToBottomRightCellBorder>(8);
			}
			set
			{
				base.SetElement<TopLeftToBottomRightCellBorder>(8, value);
			}
		}

		// Token: 0x17008D6E RID: 36206
		// (get) Token: 0x06019A29 RID: 105001 RVA: 0x00353749 File Offset: 0x00351949
		// (set) Token: 0x06019A2A RID: 105002 RVA: 0x00353753 File Offset: 0x00351953
		public TopRightToBottomLeftCellBorder TopRightToBottomLeftCellBorder
		{
			get
			{
				return base.GetElement<TopRightToBottomLeftCellBorder>(9);
			}
			set
			{
				base.SetElement<TopRightToBottomLeftCellBorder>(9, value);
			}
		}

		// Token: 0x06019A2B RID: 105003 RVA: 0x0035375E File Offset: 0x0035195E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellBorders>(deep);
		}

		// Token: 0x0400A9C4 RID: 43460
		private const string tagName = "tcBorders";

		// Token: 0x0400A9C5 RID: 43461
		private const byte tagNsId = 23;

		// Token: 0x0400A9C6 RID: 43462
		internal const int ElementTypeIdConst = 11654;

		// Token: 0x0400A9C7 RID: 43463
		private static readonly string[] eleTagNames = new string[] { "top", "left", "start", "bottom", "right", "end", "insideH", "insideV", "tl2br", "tr2bl" };

		// Token: 0x0400A9C8 RID: 43464
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
