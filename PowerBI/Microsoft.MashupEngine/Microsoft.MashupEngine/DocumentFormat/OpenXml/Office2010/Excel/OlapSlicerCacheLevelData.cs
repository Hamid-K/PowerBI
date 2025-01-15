using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002440 RID: 9280
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OlapSlicerCacheRanges), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapSlicerCacheLevelData : OpenXmlCompositeElement
	{
		// Token: 0x1700506B RID: 20587
		// (get) Token: 0x060111D3 RID: 70099 RVA: 0x002EAC05 File Offset: 0x002E8E05
		public override string LocalName
		{
			get
			{
				return "level";
			}
		}

		// Token: 0x1700506C RID: 20588
		// (get) Token: 0x060111D4 RID: 70100 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700506D RID: 20589
		// (get) Token: 0x060111D5 RID: 70101 RVA: 0x002EAC0C File Offset: 0x002E8E0C
		internal override int ElementTypeId
		{
			get
			{
				return 13004;
			}
		}

		// Token: 0x060111D6 RID: 70102 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700506E RID: 20590
		// (get) Token: 0x060111D7 RID: 70103 RVA: 0x002EAC13 File Offset: 0x002E8E13
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheLevelData.attributeTagNames;
			}
		}

		// Token: 0x1700506F RID: 20591
		// (get) Token: 0x060111D8 RID: 70104 RVA: 0x002EAC1A File Offset: 0x002E8E1A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheLevelData.attributeNamespaceIds;
			}
		}

		// Token: 0x17005070 RID: 20592
		// (get) Token: 0x060111D9 RID: 70105 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060111DA RID: 70106 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005071 RID: 20593
		// (get) Token: 0x060111DB RID: 70107 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060111DC RID: 70108 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceCaption")]
		public StringValue SourceCaption
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005072 RID: 20594
		// (get) Token: 0x060111DD RID: 70109 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x060111DE RID: 70110 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005073 RID: 20595
		// (get) Token: 0x060111DF RID: 70111 RVA: 0x002EAC21 File Offset: 0x002E8E21
		// (set) Token: 0x060111E0 RID: 70112 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sortOrder")]
		public EnumValue<OlapSlicerCacheSortOrderValues> SortOrder
		{
			get
			{
				return (EnumValue<OlapSlicerCacheSortOrderValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005074 RID: 20596
		// (get) Token: 0x060111E1 RID: 70113 RVA: 0x002EA7F9 File Offset: 0x002E89F9
		// (set) Token: 0x060111E2 RID: 70114 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x060111E3 RID: 70115 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheLevelData()
		{
		}

		// Token: 0x060111E4 RID: 70116 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheLevelData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111E5 RID: 70117 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheLevelData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111E6 RID: 70118 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheLevelData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060111E7 RID: 70119 RVA: 0x002EAC30 File Offset: 0x002E8E30
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "ranges" == name)
			{
				return new OlapSlicerCacheRanges();
			}
			return null;
		}

		// Token: 0x17005075 RID: 20597
		// (get) Token: 0x060111E8 RID: 70120 RVA: 0x002EAC4B File Offset: 0x002E8E4B
		internal override string[] ElementTagNames
		{
			get
			{
				return OlapSlicerCacheLevelData.eleTagNames;
			}
		}

		// Token: 0x17005076 RID: 20598
		// (get) Token: 0x060111E9 RID: 70121 RVA: 0x002EAC52 File Offset: 0x002E8E52
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OlapSlicerCacheLevelData.eleNamespaceIds;
			}
		}

		// Token: 0x17005077 RID: 20599
		// (get) Token: 0x060111EA RID: 70122 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005078 RID: 20600
		// (get) Token: 0x060111EB RID: 70123 RVA: 0x002EAC59 File Offset: 0x002E8E59
		// (set) Token: 0x060111EC RID: 70124 RVA: 0x002EAC62 File Offset: 0x002E8E62
		public OlapSlicerCacheRanges OlapSlicerCacheRanges
		{
			get
			{
				return base.GetElement<OlapSlicerCacheRanges>(0);
			}
			set
			{
				base.SetElement<OlapSlicerCacheRanges>(0, value);
			}
		}

		// Token: 0x060111ED RID: 70125 RVA: 0x002EAC6C File Offset: 0x002E8E6C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sortOrder" == name)
			{
				return new EnumValue<OlapSlicerCacheSortOrderValues>();
			}
			if (namespaceId == 0 && "crossFilter" == name)
			{
				return new EnumValue<SlicerCacheCrossFilterValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060111EE RID: 70126 RVA: 0x002EACEF File Offset: 0x002E8EEF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheLevelData>(deep);
		}

		// Token: 0x060111EF RID: 70127 RVA: 0x002EACF8 File Offset: 0x002E8EF8
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheLevelData()
		{
			byte[] array = new byte[5];
			OlapSlicerCacheLevelData.attributeNamespaceIds = array;
			OlapSlicerCacheLevelData.eleTagNames = new string[] { "ranges" };
			OlapSlicerCacheLevelData.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x040077B7 RID: 30647
		private const string tagName = "level";

		// Token: 0x040077B8 RID: 30648
		private const byte tagNsId = 53;

		// Token: 0x040077B9 RID: 30649
		internal const int ElementTypeIdConst = 13004;

		// Token: 0x040077BA RID: 30650
		private static string[] attributeTagNames = new string[] { "uniqueName", "sourceCaption", "count", "sortOrder", "crossFilter" };

		// Token: 0x040077BB RID: 30651
		private static byte[] attributeNamespaceIds;

		// Token: 0x040077BC RID: 30652
		private static readonly string[] eleTagNames;

		// Token: 0x040077BD RID: 30653
		private static readonly byte[] eleNamespaceIds;
	}
}
