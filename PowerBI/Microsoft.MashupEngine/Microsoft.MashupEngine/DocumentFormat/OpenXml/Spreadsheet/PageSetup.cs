using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDC RID: 11228
	[GeneratedCode("DomGen", "2.0")]
	internal class PageSetup : OpenXmlLeafElement
	{
		// Token: 0x17007D7C RID: 32124
		// (get) Token: 0x0601773D RID: 96061 RVA: 0x002F67DB File Offset: 0x002F49DB
		public override string LocalName
		{
			get
			{
				return "pageSetup";
			}
		}

		// Token: 0x17007D7D RID: 32125
		// (get) Token: 0x0601773E RID: 96062 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D7E RID: 32126
		// (get) Token: 0x0601773F RID: 96063 RVA: 0x00336EEB File Offset: 0x003350EB
		internal override int ElementTypeId
		{
			get
			{
				return 11200;
			}
		}

		// Token: 0x06017740 RID: 96064 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D7F RID: 32127
		// (get) Token: 0x06017741 RID: 96065 RVA: 0x00336EF2 File Offset: 0x003350F2
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageSetup.attributeTagNames;
			}
		}

		// Token: 0x17007D80 RID: 32128
		// (get) Token: 0x06017742 RID: 96066 RVA: 0x00336EF9 File Offset: 0x003350F9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageSetup.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D81 RID: 32129
		// (get) Token: 0x06017743 RID: 96067 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017744 RID: 96068 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "paperSize")]
		public UInt32Value PaperSize
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

		// Token: 0x17007D82 RID: 32130
		// (get) Token: 0x06017745 RID: 96069 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017746 RID: 96070 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scale")]
		public UInt32Value Scale
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007D83 RID: 32131
		// (get) Token: 0x06017747 RID: 96071 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017748 RID: 96072 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "firstPageNumber")]
		public UInt32Value FirstPageNumber
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

		// Token: 0x17007D84 RID: 32132
		// (get) Token: 0x06017749 RID: 96073 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601774A RID: 96074 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fitToWidth")]
		public UInt32Value FitToWidth
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

		// Token: 0x17007D85 RID: 32133
		// (get) Token: 0x0601774B RID: 96075 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601774C RID: 96076 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fitToHeight")]
		public UInt32Value FitToHeight
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

		// Token: 0x17007D86 RID: 32134
		// (get) Token: 0x0601774D RID: 96077 RVA: 0x00336F00 File Offset: 0x00335100
		// (set) Token: 0x0601774E RID: 96078 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "pageOrder")]
		public EnumValue<PageOrderValues> PageOrder
		{
			get
			{
				return (EnumValue<PageOrderValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007D87 RID: 32135
		// (get) Token: 0x0601774F RID: 96079 RVA: 0x00336F0F File Offset: 0x0033510F
		// (set) Token: 0x06017750 RID: 96080 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "orientation")]
		public EnumValue<OrientationValues> Orientation
		{
			get
			{
				return (EnumValue<OrientationValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007D88 RID: 32136
		// (get) Token: 0x06017751 RID: 96081 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017752 RID: 96082 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "usePrinterDefaults")]
		public BooleanValue UsePrinterDefaults
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007D89 RID: 32137
		// (get) Token: 0x06017753 RID: 96083 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017754 RID: 96084 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "blackAndWhite")]
		public BooleanValue BlackAndWhite
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

		// Token: 0x17007D8A RID: 32138
		// (get) Token: 0x06017755 RID: 96085 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017756 RID: 96086 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "draft")]
		public BooleanValue Draft
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007D8B RID: 32139
		// (get) Token: 0x06017757 RID: 96087 RVA: 0x00336F1E File Offset: 0x0033511E
		// (set) Token: 0x06017758 RID: 96088 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "cellComments")]
		public EnumValue<CellCommentsValues> CellComments
		{
			get
			{
				return (EnumValue<CellCommentsValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007D8C RID: 32140
		// (get) Token: 0x06017759 RID: 96089 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0601775A RID: 96090 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "useFirstPageNumber")]
		public BooleanValue UseFirstPageNumber
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007D8D RID: 32141
		// (get) Token: 0x0601775B RID: 96091 RVA: 0x00336F2E File Offset: 0x0033512E
		// (set) Token: 0x0601775C RID: 96092 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "errors")]
		public EnumValue<PrintErrorValues> Errors
		{
			get
			{
				return (EnumValue<PrintErrorValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007D8E RID: 32142
		// (get) Token: 0x0601775D RID: 96093 RVA: 0x0032C7AF File Offset: 0x0032A9AF
		// (set) Token: 0x0601775E RID: 96094 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "horizontalDpi")]
		public UInt32Value HorizontalDpi
		{
			get
			{
				return (UInt32Value)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007D8F RID: 32143
		// (get) Token: 0x0601775F RID: 96095 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x06017760 RID: 96096 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "verticalDpi")]
		public UInt32Value VerticalDpi
		{
			get
			{
				return (UInt32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007D90 RID: 32144
		// (get) Token: 0x06017761 RID: 96097 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x06017762 RID: 96098 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "copies")]
		public UInt32Value Copies
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007D91 RID: 32145
		// (get) Token: 0x06017763 RID: 96099 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x06017764 RID: 96100 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x06017766 RID: 96102 RVA: 0x00336F40 File Offset: 0x00335140
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "paperSize" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "scale" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstPageNumber" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "fitToWidth" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "fitToHeight" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "pageOrder" == name)
			{
				return new EnumValue<PageOrderValues>();
			}
			if (namespaceId == 0 && "orientation" == name)
			{
				return new EnumValue<OrientationValues>();
			}
			if (namespaceId == 0 && "usePrinterDefaults" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "blackAndWhite" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "draft" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "cellComments" == name)
			{
				return new EnumValue<CellCommentsValues>();
			}
			if (namespaceId == 0 && "useFirstPageNumber" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "errors" == name)
			{
				return new EnumValue<PrintErrorValues>();
			}
			if (namespaceId == 0 && "horizontalDpi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "verticalDpi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "copies" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017767 RID: 96103 RVA: 0x003370CD File Offset: 0x003352CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageSetup>(deep);
		}

		// Token: 0x04009C68 RID: 40040
		private const string tagName = "pageSetup";

		// Token: 0x04009C69 RID: 40041
		private const byte tagNsId = 22;

		// Token: 0x04009C6A RID: 40042
		internal const int ElementTypeIdConst = 11200;

		// Token: 0x04009C6B RID: 40043
		private static string[] attributeTagNames = new string[]
		{
			"paperSize", "scale", "firstPageNumber", "fitToWidth", "fitToHeight", "pageOrder", "orientation", "usePrinterDefaults", "blackAndWhite", "draft",
			"cellComments", "useFirstPageNumber", "errors", "horizontalDpi", "verticalDpi", "copies", "id"
		};

		// Token: 0x04009C6C RID: 40044
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 19
		};
	}
}
