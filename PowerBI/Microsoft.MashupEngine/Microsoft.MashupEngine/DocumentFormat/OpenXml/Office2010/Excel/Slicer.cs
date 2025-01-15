using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002436 RID: 9270
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Slicer : OpenXmlCompositeElement
	{
		// Token: 0x17005014 RID: 20500
		// (get) Token: 0x06011110 RID: 69904 RVA: 0x002AED9A File Offset: 0x002ACF9A
		public override string LocalName
		{
			get
			{
				return "slicer";
			}
		}

		// Token: 0x17005015 RID: 20501
		// (get) Token: 0x06011111 RID: 69905 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005016 RID: 20502
		// (get) Token: 0x06011112 RID: 69906 RVA: 0x002EA3E5 File Offset: 0x002E85E5
		internal override int ElementTypeId
		{
			get
			{
				return 12994;
			}
		}

		// Token: 0x06011113 RID: 69907 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005017 RID: 20503
		// (get) Token: 0x06011114 RID: 69908 RVA: 0x002EA3EC File Offset: 0x002E85EC
		internal override string[] AttributeTagNames
		{
			get
			{
				return Slicer.attributeTagNames;
			}
		}

		// Token: 0x17005018 RID: 20504
		// (get) Token: 0x06011115 RID: 69909 RVA: 0x002EA3F3 File Offset: 0x002E85F3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Slicer.attributeNamespaceIds;
			}
		}

		// Token: 0x17005019 RID: 20505
		// (get) Token: 0x06011116 RID: 69910 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011117 RID: 69911 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700501A RID: 20506
		// (get) Token: 0x06011118 RID: 69912 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011119 RID: 69913 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cache")]
		public StringValue Cache
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

		// Token: 0x1700501B RID: 20507
		// (get) Token: 0x0601111A RID: 69914 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601111B RID: 69915 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "caption")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700501C RID: 20508
		// (get) Token: 0x0601111C RID: 69916 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601111D RID: 69917 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "startItem")]
		public UInt32Value StartItem
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700501D RID: 20509
		// (get) Token: 0x0601111E RID: 69918 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601111F RID: 69919 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "columnCount")]
		public UInt32Value ColumnCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700501E RID: 20510
		// (get) Token: 0x06011120 RID: 69920 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06011121 RID: 69921 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "showCaption")]
		public BooleanValue ShowCaption
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700501F RID: 20511
		// (get) Token: 0x06011122 RID: 69922 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06011123 RID: 69923 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "level")]
		public UInt32Value Level
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005020 RID: 20512
		// (get) Token: 0x06011124 RID: 69924 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06011125 RID: 69925 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "style")]
		public StringValue Style
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17005021 RID: 20513
		// (get) Token: 0x06011126 RID: 69926 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06011127 RID: 69927 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "lockedPosition")]
		public BooleanValue LockedPosition
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17005022 RID: 20514
		// (get) Token: 0x06011128 RID: 69928 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06011129 RID: 69929 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "rowHeight")]
		public UInt32Value RowHeight
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x0601112A RID: 69930 RVA: 0x00293ECF File Offset: 0x002920CF
		public Slicer()
		{
		}

		// Token: 0x0601112B RID: 69931 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Slicer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601112C RID: 69932 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Slicer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601112D RID: 69933 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Slicer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601112E RID: 69934 RVA: 0x002E6E04 File Offset: 0x002E5004
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005023 RID: 20515
		// (get) Token: 0x0601112F RID: 69935 RVA: 0x002EA3FA File Offset: 0x002E85FA
		internal override string[] ElementTagNames
		{
			get
			{
				return Slicer.eleTagNames;
			}
		}

		// Token: 0x17005024 RID: 20516
		// (get) Token: 0x06011130 RID: 69936 RVA: 0x002EA401 File Offset: 0x002E8601
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Slicer.eleNamespaceIds;
			}
		}

		// Token: 0x17005025 RID: 20517
		// (get) Token: 0x06011131 RID: 69937 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005026 RID: 20518
		// (get) Token: 0x06011132 RID: 69938 RVA: 0x002E6E2D File Offset: 0x002E502D
		// (set) Token: 0x06011133 RID: 69939 RVA: 0x002E6E36 File Offset: 0x002E5036
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06011134 RID: 69940 RVA: 0x002EA408 File Offset: 0x002E8608
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cache" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "startItem" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "columnCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showCaption" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "level" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lockedPosition" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowHeight" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011135 RID: 69941 RVA: 0x002EA4F9 File Offset: 0x002E86F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Slicer>(deep);
		}

		// Token: 0x06011136 RID: 69942 RVA: 0x002EA504 File Offset: 0x002E8704
		// Note: this type is marked as 'beforefieldinit'.
		static Slicer()
		{
			byte[] array = new byte[10];
			Slicer.attributeNamespaceIds = array;
			Slicer.eleTagNames = new string[] { "extLst" };
			Slicer.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x04007783 RID: 30595
		private const string tagName = "slicer";

		// Token: 0x04007784 RID: 30596
		private const byte tagNsId = 53;

		// Token: 0x04007785 RID: 30597
		internal const int ElementTypeIdConst = 12994;

		// Token: 0x04007786 RID: 30598
		private static string[] attributeTagNames = new string[] { "name", "cache", "caption", "startItem", "columnCount", "showCaption", "level", "style", "lockedPosition", "rowHeight" };

		// Token: 0x04007787 RID: 30599
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007788 RID: 30600
		private static readonly string[] eleTagNames;

		// Token: 0x04007789 RID: 30601
		private static readonly byte[] eleNamespaceIds;
	}
}
