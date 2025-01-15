using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002800 RID: 10240
	[ChildElementInfo(typeof(LeftBorder))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(InsideHorizontalBorder))]
	[ChildElementInfo(typeof(InsideVerticalBorder))]
	[ChildElementInfo(typeof(TopLeftToBottomRightBorder))]
	[ChildElementInfo(typeof(TopRightToBottomLeftBorder))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class TableCellBorders : OpenXmlCompositeElement
	{
		// Token: 0x17006519 RID: 25881
		// (get) Token: 0x0601402B RID: 81963 RVA: 0x0030E460 File Offset: 0x0030C660
		public override string LocalName
		{
			get
			{
				return "tcBdr";
			}
		}

		// Token: 0x1700651A RID: 25882
		// (get) Token: 0x0601402C RID: 81964 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700651B RID: 25883
		// (get) Token: 0x0601402D RID: 81965 RVA: 0x0030E467 File Offset: 0x0030C667
		internal override int ElementTypeId
		{
			get
			{
				return 10276;
			}
		}

		// Token: 0x0601402E RID: 81966 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601402F RID: 81967 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellBorders()
		{
		}

		// Token: 0x06014030 RID: 81968 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellBorders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014031 RID: 81969 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellBorders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014032 RID: 81970 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellBorders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014033 RID: 81971 RVA: 0x0030E470 File Offset: 0x0030C670
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (10 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			if (10 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (10 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (10 == namespaceId && "insideH" == name)
			{
				return new InsideHorizontalBorder();
			}
			if (10 == namespaceId && "insideV" == name)
			{
				return new InsideVerticalBorder();
			}
			if (10 == namespaceId && "tl2br" == name)
			{
				return new TopLeftToBottomRightBorder();
			}
			if (10 == namespaceId && "tr2bl" == name)
			{
				return new TopRightToBottomLeftBorder();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700651C RID: 25884
		// (get) Token: 0x06014034 RID: 81972 RVA: 0x0030E556 File Offset: 0x0030C756
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellBorders.eleTagNames;
			}
		}

		// Token: 0x1700651D RID: 25885
		// (get) Token: 0x06014035 RID: 81973 RVA: 0x0030E55D File Offset: 0x0030C75D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellBorders.eleNamespaceIds;
			}
		}

		// Token: 0x1700651E RID: 25886
		// (get) Token: 0x06014036 RID: 81974 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700651F RID: 25887
		// (get) Token: 0x06014037 RID: 81975 RVA: 0x0030E564 File Offset: 0x0030C764
		// (set) Token: 0x06014038 RID: 81976 RVA: 0x0030E56D File Offset: 0x0030C76D
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(0);
			}
			set
			{
				base.SetElement<LeftBorder>(0, value);
			}
		}

		// Token: 0x17006520 RID: 25888
		// (get) Token: 0x06014039 RID: 81977 RVA: 0x0030E577 File Offset: 0x0030C777
		// (set) Token: 0x0601403A RID: 81978 RVA: 0x0030E580 File Offset: 0x0030C780
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(1);
			}
			set
			{
				base.SetElement<RightBorder>(1, value);
			}
		}

		// Token: 0x17006521 RID: 25889
		// (get) Token: 0x0601403B RID: 81979 RVA: 0x0030E58A File Offset: 0x0030C78A
		// (set) Token: 0x0601403C RID: 81980 RVA: 0x0030E593 File Offset: 0x0030C793
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(2);
			}
			set
			{
				base.SetElement<TopBorder>(2, value);
			}
		}

		// Token: 0x17006522 RID: 25890
		// (get) Token: 0x0601403D RID: 81981 RVA: 0x0030E59D File Offset: 0x0030C79D
		// (set) Token: 0x0601403E RID: 81982 RVA: 0x0030E5A6 File Offset: 0x0030C7A6
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

		// Token: 0x17006523 RID: 25891
		// (get) Token: 0x0601403F RID: 81983 RVA: 0x0030E5B0 File Offset: 0x0030C7B0
		// (set) Token: 0x06014040 RID: 81984 RVA: 0x0030E5B9 File Offset: 0x0030C7B9
		public InsideHorizontalBorder InsideHorizontalBorder
		{
			get
			{
				return base.GetElement<InsideHorizontalBorder>(4);
			}
			set
			{
				base.SetElement<InsideHorizontalBorder>(4, value);
			}
		}

		// Token: 0x17006524 RID: 25892
		// (get) Token: 0x06014041 RID: 81985 RVA: 0x0030E5C3 File Offset: 0x0030C7C3
		// (set) Token: 0x06014042 RID: 81986 RVA: 0x0030E5CC File Offset: 0x0030C7CC
		public InsideVerticalBorder InsideVerticalBorder
		{
			get
			{
				return base.GetElement<InsideVerticalBorder>(5);
			}
			set
			{
				base.SetElement<InsideVerticalBorder>(5, value);
			}
		}

		// Token: 0x17006525 RID: 25893
		// (get) Token: 0x06014043 RID: 81987 RVA: 0x0030E5D6 File Offset: 0x0030C7D6
		// (set) Token: 0x06014044 RID: 81988 RVA: 0x0030E5DF File Offset: 0x0030C7DF
		public TopLeftToBottomRightBorder TopLeftToBottomRightBorder
		{
			get
			{
				return base.GetElement<TopLeftToBottomRightBorder>(6);
			}
			set
			{
				base.SetElement<TopLeftToBottomRightBorder>(6, value);
			}
		}

		// Token: 0x17006526 RID: 25894
		// (get) Token: 0x06014045 RID: 81989 RVA: 0x0030E5E9 File Offset: 0x0030C7E9
		// (set) Token: 0x06014046 RID: 81990 RVA: 0x0030E5F2 File Offset: 0x0030C7F2
		public TopRightToBottomLeftBorder TopRightToBottomLeftBorder
		{
			get
			{
				return base.GetElement<TopRightToBottomLeftBorder>(7);
			}
			set
			{
				base.SetElement<TopRightToBottomLeftBorder>(7, value);
			}
		}

		// Token: 0x17006527 RID: 25895
		// (get) Token: 0x06014047 RID: 81991 RVA: 0x0030E5FC File Offset: 0x0030C7FC
		// (set) Token: 0x06014048 RID: 81992 RVA: 0x0030E605 File Offset: 0x0030C805
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(8);
			}
			set
			{
				base.SetElement<ExtensionList>(8, value);
			}
		}

		// Token: 0x06014049 RID: 81993 RVA: 0x0030E60F File Offset: 0x0030C80F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellBorders>(deep);
		}

		// Token: 0x040088A4 RID: 34980
		private const string tagName = "tcBdr";

		// Token: 0x040088A5 RID: 34981
		private const byte tagNsId = 10;

		// Token: 0x040088A6 RID: 34982
		internal const int ElementTypeIdConst = 10276;

		// Token: 0x040088A7 RID: 34983
		private static readonly string[] eleTagNames = new string[] { "left", "right", "top", "bottom", "insideH", "insideV", "tl2br", "tr2bl", "extLst" };

		// Token: 0x040088A8 RID: 34984
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
	}
}
