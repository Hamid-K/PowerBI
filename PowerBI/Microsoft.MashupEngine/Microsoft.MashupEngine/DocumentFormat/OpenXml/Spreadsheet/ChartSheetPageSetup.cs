using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE4 RID: 11236
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartSheetPageSetup : OpenXmlLeafElement
	{
		// Token: 0x17007DEA RID: 32234
		// (get) Token: 0x06017823 RID: 96291 RVA: 0x002F67DB File Offset: 0x002F49DB
		public override string LocalName
		{
			get
			{
				return "pageSetup";
			}
		}

		// Token: 0x17007DEB RID: 32235
		// (get) Token: 0x06017824 RID: 96292 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DEC RID: 32236
		// (get) Token: 0x06017825 RID: 96293 RVA: 0x00337B47 File Offset: 0x00335D47
		internal override int ElementTypeId
		{
			get
			{
				return 11208;
			}
		}

		// Token: 0x06017826 RID: 96294 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DED RID: 32237
		// (get) Token: 0x06017827 RID: 96295 RVA: 0x00337B4E File Offset: 0x00335D4E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartSheetPageSetup.attributeTagNames;
			}
		}

		// Token: 0x17007DEE RID: 32238
		// (get) Token: 0x06017828 RID: 96296 RVA: 0x00337B55 File Offset: 0x00335D55
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartSheetPageSetup.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DEF RID: 32239
		// (get) Token: 0x06017829 RID: 96297 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601782A RID: 96298 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007DF0 RID: 32240
		// (get) Token: 0x0601782B RID: 96299 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601782C RID: 96300 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "firstPageNumber")]
		public UInt32Value FirstPageNumber
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

		// Token: 0x17007DF1 RID: 32241
		// (get) Token: 0x0601782D RID: 96301 RVA: 0x00337B5C File Offset: 0x00335D5C
		// (set) Token: 0x0601782E RID: 96302 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "orientation")]
		public EnumValue<OrientationValues> Orientation
		{
			get
			{
				return (EnumValue<OrientationValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007DF2 RID: 32242
		// (get) Token: 0x0601782F RID: 96303 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017830 RID: 96304 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "usePrinterDefaults")]
		public BooleanValue UsePrinterDefaults
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

		// Token: 0x17007DF3 RID: 32243
		// (get) Token: 0x06017831 RID: 96305 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017832 RID: 96306 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "blackAndWhite")]
		public BooleanValue BlackAndWhite
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007DF4 RID: 32244
		// (get) Token: 0x06017833 RID: 96307 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017834 RID: 96308 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "draft")]
		public BooleanValue Draft
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

		// Token: 0x17007DF5 RID: 32245
		// (get) Token: 0x06017835 RID: 96309 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017836 RID: 96310 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "useFirstPageNumber")]
		public BooleanValue UseFirstPageNumber
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007DF6 RID: 32246
		// (get) Token: 0x06017837 RID: 96311 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x06017838 RID: 96312 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "horizontalDpi")]
		public UInt32Value HorizontalDpi
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007DF7 RID: 32247
		// (get) Token: 0x06017839 RID: 96313 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x0601783A RID: 96314 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "verticalDpi")]
		public UInt32Value VerticalDpi
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007DF8 RID: 32248
		// (get) Token: 0x0601783B RID: 96315 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x0601783C RID: 96316 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "copies")]
		public UInt32Value Copies
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

		// Token: 0x17007DF9 RID: 32249
		// (get) Token: 0x0601783D RID: 96317 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0601783E RID: 96318 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x06017840 RID: 96320 RVA: 0x00337B6C File Offset: 0x00335D6C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "paperSize" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstPageNumber" == name)
			{
				return new UInt32Value();
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
			if (namespaceId == 0 && "useFirstPageNumber" == name)
			{
				return new BooleanValue();
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

		// Token: 0x06017841 RID: 96321 RVA: 0x00337C75 File Offset: 0x00335E75
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSheetPageSetup>(deep);
		}

		// Token: 0x04009C94 RID: 40084
		private const string tagName = "pageSetup";

		// Token: 0x04009C95 RID: 40085
		private const byte tagNsId = 22;

		// Token: 0x04009C96 RID: 40086
		internal const int ElementTypeIdConst = 11208;

		// Token: 0x04009C97 RID: 40087
		private static string[] attributeTagNames = new string[]
		{
			"paperSize", "firstPageNumber", "orientation", "usePrinterDefaults", "blackAndWhite", "draft", "useFirstPageNumber", "horizontalDpi", "verticalDpi", "copies",
			"id"
		};

		// Token: 0x04009C98 RID: 40088
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			19
		};
	}
}
