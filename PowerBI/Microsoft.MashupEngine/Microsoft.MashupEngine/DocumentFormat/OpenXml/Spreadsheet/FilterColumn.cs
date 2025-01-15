using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2B RID: 11051
	[ChildElementInfo(typeof(Filters))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DynamicFilter))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(CustomFilters), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CustomFilters))]
	[ChildElementInfo(typeof(Top10))]
	[ChildElementInfo(typeof(ColorFilter))]
	[ChildElementInfo(typeof(IconFilter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(IconFilter))]
	internal class FilterColumn : OpenXmlCompositeElement
	{
		// Token: 0x1700773A RID: 30522
		// (get) Token: 0x0601699C RID: 92572 RVA: 0x0032D060 File Offset: 0x0032B260
		public override string LocalName
		{
			get
			{
				return "filterColumn";
			}
		}

		// Token: 0x1700773B RID: 30523
		// (get) Token: 0x0601699D RID: 92573 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700773C RID: 30524
		// (get) Token: 0x0601699E RID: 92574 RVA: 0x0032D067 File Offset: 0x0032B267
		internal override int ElementTypeId
		{
			get
			{
				return 11049;
			}
		}

		// Token: 0x0601699F RID: 92575 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700773D RID: 30525
		// (get) Token: 0x060169A0 RID: 92576 RVA: 0x0032D06E File Offset: 0x0032B26E
		internal override string[] AttributeTagNames
		{
			get
			{
				return FilterColumn.attributeTagNames;
			}
		}

		// Token: 0x1700773E RID: 30526
		// (get) Token: 0x060169A1 RID: 92577 RVA: 0x0032D075 File Offset: 0x0032B275
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FilterColumn.attributeNamespaceIds;
			}
		}

		// Token: 0x1700773F RID: 30527
		// (get) Token: 0x060169A2 RID: 92578 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060169A3 RID: 92579 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "colId")]
		public UInt32Value ColumnId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007740 RID: 30528
		// (get) Token: 0x060169A4 RID: 92580 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060169A5 RID: 92581 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hiddenButton")]
		public BooleanValue HiddenButton
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007741 RID: 30529
		// (get) Token: 0x060169A6 RID: 92582 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060169A7 RID: 92583 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showButton")]
		public BooleanValue ShowButton
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060169A8 RID: 92584 RVA: 0x00293ECF File Offset: 0x002920CF
		public FilterColumn()
		{
		}

		// Token: 0x060169A9 RID: 92585 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FilterColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169AA RID: 92586 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FilterColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169AB RID: 92587 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FilterColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060169AC RID: 92588 RVA: 0x0032D07C File Offset: 0x0032B27C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "filters" == name)
			{
				return new Filters();
			}
			if (22 == namespaceId && "top10" == name)
			{
				return new Top10();
			}
			if (53 == namespaceId && "customFilters" == name)
			{
				return new CustomFilters();
			}
			if (22 == namespaceId && "customFilters" == name)
			{
				return new CustomFilters();
			}
			if (22 == namespaceId && "dynamicFilter" == name)
			{
				return new DynamicFilter();
			}
			if (22 == namespaceId && "colorFilter" == name)
			{
				return new ColorFilter();
			}
			if (53 == namespaceId && "iconFilter" == name)
			{
				return new IconFilter();
			}
			if (22 == namespaceId && "iconFilter" == name)
			{
				return new IconFilter();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007742 RID: 30530
		// (get) Token: 0x060169AD RID: 92589 RVA: 0x0032D162 File Offset: 0x0032B362
		internal override string[] ElementTagNames
		{
			get
			{
				return FilterColumn.eleTagNames;
			}
		}

		// Token: 0x17007743 RID: 30531
		// (get) Token: 0x060169AE RID: 92590 RVA: 0x0032D169 File Offset: 0x0032B369
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FilterColumn.eleNamespaceIds;
			}
		}

		// Token: 0x17007744 RID: 30532
		// (get) Token: 0x060169AF RID: 92591 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007745 RID: 30533
		// (get) Token: 0x060169B0 RID: 92592 RVA: 0x0032D170 File Offset: 0x0032B370
		// (set) Token: 0x060169B1 RID: 92593 RVA: 0x0032D179 File Offset: 0x0032B379
		public Filters Filters
		{
			get
			{
				return base.GetElement<Filters>(0);
			}
			set
			{
				base.SetElement<Filters>(0, value);
			}
		}

		// Token: 0x17007746 RID: 30534
		// (get) Token: 0x060169B2 RID: 92594 RVA: 0x0032D183 File Offset: 0x0032B383
		// (set) Token: 0x060169B3 RID: 92595 RVA: 0x0032D18C File Offset: 0x0032B38C
		public Top10 Top10
		{
			get
			{
				return base.GetElement<Top10>(1);
			}
			set
			{
				base.SetElement<Top10>(1, value);
			}
		}

		// Token: 0x17007747 RID: 30535
		// (get) Token: 0x060169B4 RID: 92596 RVA: 0x0032D196 File Offset: 0x0032B396
		// (set) Token: 0x060169B5 RID: 92597 RVA: 0x0032D19F File Offset: 0x0032B39F
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public CustomFilters CustomFilters14
		{
			get
			{
				return base.GetElement<CustomFilters>(2);
			}
			set
			{
				base.SetElement<CustomFilters>(2, value);
			}
		}

		// Token: 0x17007748 RID: 30536
		// (get) Token: 0x060169B6 RID: 92598 RVA: 0x0032D1A9 File Offset: 0x0032B3A9
		// (set) Token: 0x060169B7 RID: 92599 RVA: 0x0032D1B2 File Offset: 0x0032B3B2
		public CustomFilters CustomFilters
		{
			get
			{
				return base.GetElement<CustomFilters>(3);
			}
			set
			{
				base.SetElement<CustomFilters>(3, value);
			}
		}

		// Token: 0x17007749 RID: 30537
		// (get) Token: 0x060169B8 RID: 92600 RVA: 0x0032D1BC File Offset: 0x0032B3BC
		// (set) Token: 0x060169B9 RID: 92601 RVA: 0x0032D1C5 File Offset: 0x0032B3C5
		public DynamicFilter DynamicFilter
		{
			get
			{
				return base.GetElement<DynamicFilter>(4);
			}
			set
			{
				base.SetElement<DynamicFilter>(4, value);
			}
		}

		// Token: 0x1700774A RID: 30538
		// (get) Token: 0x060169BA RID: 92602 RVA: 0x0032D1CF File Offset: 0x0032B3CF
		// (set) Token: 0x060169BB RID: 92603 RVA: 0x0032D1D8 File Offset: 0x0032B3D8
		public ColorFilter ColorFilter
		{
			get
			{
				return base.GetElement<ColorFilter>(5);
			}
			set
			{
				base.SetElement<ColorFilter>(5, value);
			}
		}

		// Token: 0x1700774B RID: 30539
		// (get) Token: 0x060169BC RID: 92604 RVA: 0x0032D1E2 File Offset: 0x0032B3E2
		// (set) Token: 0x060169BD RID: 92605 RVA: 0x0032D1EB File Offset: 0x0032B3EB
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public IconFilter IconFilter14
		{
			get
			{
				return base.GetElement<IconFilter>(6);
			}
			set
			{
				base.SetElement<IconFilter>(6, value);
			}
		}

		// Token: 0x1700774C RID: 30540
		// (get) Token: 0x060169BE RID: 92606 RVA: 0x0032D1F5 File Offset: 0x0032B3F5
		// (set) Token: 0x060169BF RID: 92607 RVA: 0x0032D1FE File Offset: 0x0032B3FE
		public IconFilter IconFilter
		{
			get
			{
				return base.GetElement<IconFilter>(7);
			}
			set
			{
				base.SetElement<IconFilter>(7, value);
			}
		}

		// Token: 0x1700774D RID: 30541
		// (get) Token: 0x060169C0 RID: 92608 RVA: 0x0032D208 File Offset: 0x0032B408
		// (set) Token: 0x060169C1 RID: 92609 RVA: 0x0032D211 File Offset: 0x0032B411
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

		// Token: 0x060169C2 RID: 92610 RVA: 0x0032D21C File Offset: 0x0032B41C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "colId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hiddenButton" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showButton" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060169C3 RID: 92611 RVA: 0x0032D273 File Offset: 0x0032B473
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FilterColumn>(deep);
		}

		// Token: 0x060169C4 RID: 92612 RVA: 0x0032D27C File Offset: 0x0032B47C
		// Note: this type is marked as 'beforefieldinit'.
		static FilterColumn()
		{
			byte[] array = new byte[3];
			FilterColumn.attributeNamespaceIds = array;
			FilterColumn.eleTagNames = new string[] { "filters", "top10", "customFilters", "customFilters", "dynamicFilter", "colorFilter", "iconFilter", "iconFilter", "extLst" };
			FilterColumn.eleNamespaceIds = new byte[] { 22, 22, 53, 22, 22, 22, 53, 22, 22 };
		}

		// Token: 0x04009934 RID: 39220
		private const string tagName = "filterColumn";

		// Token: 0x04009935 RID: 39221
		private const byte tagNsId = 22;

		// Token: 0x04009936 RID: 39222
		internal const int ElementTypeIdConst = 11049;

		// Token: 0x04009937 RID: 39223
		private static string[] attributeTagNames = new string[] { "colId", "hiddenButton", "showButton" };

		// Token: 0x04009938 RID: 39224
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009939 RID: 39225
		private static readonly string[] eleTagNames;

		// Token: 0x0400993A RID: 39226
		private static readonly byte[] eleNamespaceIds;
	}
}
