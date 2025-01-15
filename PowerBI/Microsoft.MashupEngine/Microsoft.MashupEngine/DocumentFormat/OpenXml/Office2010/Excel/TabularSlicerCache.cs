using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243A RID: 9274
	[ChildElementInfo(typeof(TabularSlicerCacheItems), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	internal class TabularSlicerCache : OpenXmlCompositeElement
	{
		// Token: 0x1700503E RID: 20542
		// (get) Token: 0x0601116C RID: 69996 RVA: 0x002EA7CE File Offset: 0x002E89CE
		public override string LocalName
		{
			get
			{
				return "tabular";
			}
		}

		// Token: 0x1700503F RID: 20543
		// (get) Token: 0x0601116D RID: 69997 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005040 RID: 20544
		// (get) Token: 0x0601116E RID: 69998 RVA: 0x002EA7D5 File Offset: 0x002E89D5
		internal override int ElementTypeId
		{
			get
			{
				return 12998;
			}
		}

		// Token: 0x0601116F RID: 69999 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005041 RID: 20545
		// (get) Token: 0x06011170 RID: 70000 RVA: 0x002EA7DC File Offset: 0x002E89DC
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabularSlicerCache.attributeTagNames;
			}
		}

		// Token: 0x17005042 RID: 20546
		// (get) Token: 0x06011171 RID: 70001 RVA: 0x002EA7E3 File Offset: 0x002E89E3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabularSlicerCache.attributeNamespaceIds;
			}
		}

		// Token: 0x17005043 RID: 20547
		// (get) Token: 0x06011172 RID: 70002 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011173 RID: 70003 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pivotCacheId")]
		public UInt32Value PivotCacheId
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

		// Token: 0x17005044 RID: 20548
		// (get) Token: 0x06011174 RID: 70004 RVA: 0x002EA7EA File Offset: 0x002E89EA
		// (set) Token: 0x06011175 RID: 70005 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sortOrder")]
		public EnumValue<TabularSlicerCacheSortOrderValues> SortOrder
		{
			get
			{
				return (EnumValue<TabularSlicerCacheSortOrderValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005045 RID: 20549
		// (get) Token: 0x06011176 RID: 70006 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06011177 RID: 70007 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "customListSort")]
		public BooleanValue CustomListSort
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

		// Token: 0x17005046 RID: 20550
		// (get) Token: 0x06011178 RID: 70008 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06011179 RID: 70009 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showMissing")]
		public BooleanValue ShowMissing
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005047 RID: 20551
		// (get) Token: 0x0601117A RID: 70010 RVA: 0x002EA7F9 File Offset: 0x002E89F9
		// (set) Token: 0x0601117B RID: 70011 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "crossFilter")]
		public EnumValue<SlicerCacheCrossFilterValues> CrossFilter
		{
			get
			{
				return (EnumValue<SlicerCacheCrossFilterValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0601117C RID: 70012 RVA: 0x00293ECF File Offset: 0x002920CF
		public TabularSlicerCache()
		{
		}

		// Token: 0x0601117D RID: 70013 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TabularSlicerCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601117E RID: 70014 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TabularSlicerCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601117F RID: 70015 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TabularSlicerCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011180 RID: 70016 RVA: 0x002EA808 File Offset: 0x002E8A08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "items" == name)
			{
				return new TabularSlicerCacheItems();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005048 RID: 20552
		// (get) Token: 0x06011181 RID: 70017 RVA: 0x002EA83B File Offset: 0x002E8A3B
		internal override string[] ElementTagNames
		{
			get
			{
				return TabularSlicerCache.eleTagNames;
			}
		}

		// Token: 0x17005049 RID: 20553
		// (get) Token: 0x06011182 RID: 70018 RVA: 0x002EA842 File Offset: 0x002E8A42
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TabularSlicerCache.eleNamespaceIds;
			}
		}

		// Token: 0x1700504A RID: 20554
		// (get) Token: 0x06011183 RID: 70019 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700504B RID: 20555
		// (get) Token: 0x06011184 RID: 70020 RVA: 0x002EA849 File Offset: 0x002E8A49
		// (set) Token: 0x06011185 RID: 70021 RVA: 0x002EA852 File Offset: 0x002E8A52
		public TabularSlicerCacheItems TabularSlicerCacheItems
		{
			get
			{
				return base.GetElement<TabularSlicerCacheItems>(0);
			}
			set
			{
				base.SetElement<TabularSlicerCacheItems>(0, value);
			}
		}

		// Token: 0x1700504C RID: 20556
		// (get) Token: 0x06011186 RID: 70022 RVA: 0x002E700B File Offset: 0x002E520B
		// (set) Token: 0x06011187 RID: 70023 RVA: 0x002E7014 File Offset: 0x002E5214
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06011188 RID: 70024 RVA: 0x002EA85C File Offset: 0x002E8A5C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pivotCacheId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sortOrder" == name)
			{
				return new EnumValue<TabularSlicerCacheSortOrderValues>();
			}
			if (namespaceId == 0 && "customListSort" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showMissing" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "crossFilter" == name)
			{
				return new EnumValue<SlicerCacheCrossFilterValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011189 RID: 70025 RVA: 0x002EA8DF File Offset: 0x002E8ADF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabularSlicerCache>(deep);
		}

		// Token: 0x0601118A RID: 70026 RVA: 0x002EA8E8 File Offset: 0x002E8AE8
		// Note: this type is marked as 'beforefieldinit'.
		static TabularSlicerCache()
		{
			byte[] array = new byte[5];
			TabularSlicerCache.attributeNamespaceIds = array;
			TabularSlicerCache.eleTagNames = new string[] { "items", "extLst" };
			TabularSlicerCache.eleNamespaceIds = new byte[] { 53, 53 };
		}

		// Token: 0x04007799 RID: 30617
		private const string tagName = "tabular";

		// Token: 0x0400779A RID: 30618
		private const byte tagNsId = 53;

		// Token: 0x0400779B RID: 30619
		internal const int ElementTypeIdConst = 12998;

		// Token: 0x0400779C RID: 30620
		private static string[] attributeTagNames = new string[] { "pivotCacheId", "sortOrder", "customListSort", "showMissing", "crossFilter" };

		// Token: 0x0400779D RID: 30621
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400779E RID: 30622
		private static readonly string[] eleTagNames;

		// Token: 0x0400779F RID: 30623
		private static readonly byte[] eleNamespaceIds;
	}
}
