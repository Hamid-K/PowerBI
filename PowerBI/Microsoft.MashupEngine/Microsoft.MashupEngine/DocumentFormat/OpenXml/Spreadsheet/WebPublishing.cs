using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5F RID: 11359
	[GeneratedCode("DomGen", "2.0")]
	internal class WebPublishing : OpenXmlLeafElement
	{
		// Token: 0x17008271 RID: 33393
		// (get) Token: 0x06018226 RID: 98854 RVA: 0x0033ECF1 File Offset: 0x0033CEF1
		public override string LocalName
		{
			get
			{
				return "webPublishing";
			}
		}

		// Token: 0x17008272 RID: 33394
		// (get) Token: 0x06018227 RID: 98855 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008273 RID: 33395
		// (get) Token: 0x06018228 RID: 98856 RVA: 0x0033ECF8 File Offset: 0x0033CEF8
		internal override int ElementTypeId
		{
			get
			{
				return 11340;
			}
		}

		// Token: 0x06018229 RID: 98857 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008274 RID: 33396
		// (get) Token: 0x0601822A RID: 98858 RVA: 0x0033ECFF File Offset: 0x0033CEFF
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebPublishing.attributeTagNames;
			}
		}

		// Token: 0x17008275 RID: 33397
		// (get) Token: 0x0601822B RID: 98859 RVA: 0x0033ED06 File Offset: 0x0033CF06
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebPublishing.attributeNamespaceIds;
			}
		}

		// Token: 0x17008276 RID: 33398
		// (get) Token: 0x0601822C RID: 98860 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601822D RID: 98861 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "css")]
		public BooleanValue UseCss
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008277 RID: 33399
		// (get) Token: 0x0601822E RID: 98862 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601822F RID: 98863 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "thicket")]
		public BooleanValue Thicket
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

		// Token: 0x17008278 RID: 33400
		// (get) Token: 0x06018230 RID: 98864 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06018231 RID: 98865 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "longFileNames")]
		public BooleanValue LongFileNames
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

		// Token: 0x17008279 RID: 33401
		// (get) Token: 0x06018232 RID: 98866 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06018233 RID: 98867 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "vml")]
		public BooleanValue UseVml
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

		// Token: 0x1700827A RID: 33402
		// (get) Token: 0x06018234 RID: 98868 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06018235 RID: 98869 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "allowPng")]
		public BooleanValue AllowPng
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

		// Token: 0x1700827B RID: 33403
		// (get) Token: 0x06018236 RID: 98870 RVA: 0x0033ED0D File Offset: 0x0033CF0D
		// (set) Token: 0x06018237 RID: 98871 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "targetScreenSize")]
		public EnumValue<TargetScreenSizeValues> TargetScreenSize
		{
			get
			{
				return (EnumValue<TargetScreenSizeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700827C RID: 33404
		// (get) Token: 0x06018238 RID: 98872 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06018239 RID: 98873 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "dpi")]
		public UInt32Value Dpi
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

		// Token: 0x1700827D RID: 33405
		// (get) Token: 0x0601823A RID: 98874 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x0601823B RID: 98875 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "codePage")]
		public UInt32Value CodePage
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

		// Token: 0x0601823D RID: 98877 RVA: 0x0033ED1C File Offset: 0x0033CF1C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "css" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "thicket" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "longFileNames" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "vml" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allowPng" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "targetScreenSize" == name)
			{
				return new EnumValue<TargetScreenSizeValues>();
			}
			if (namespaceId == 0 && "dpi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "codePage" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601823E RID: 98878 RVA: 0x0033EDE1 File Offset: 0x0033CFE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPublishing>(deep);
		}

		// Token: 0x0601823F RID: 98879 RVA: 0x0033EDEC File Offset: 0x0033CFEC
		// Note: this type is marked as 'beforefieldinit'.
		static WebPublishing()
		{
			byte[] array = new byte[8];
			WebPublishing.attributeNamespaceIds = array;
		}

		// Token: 0x04009EFE RID: 40702
		private const string tagName = "webPublishing";

		// Token: 0x04009EFF RID: 40703
		private const byte tagNsId = 22;

		// Token: 0x04009F00 RID: 40704
		internal const int ElementTypeIdConst = 11340;

		// Token: 0x04009F01 RID: 40705
		private static string[] attributeTagNames = new string[] { "css", "thicket", "longFileNames", "vml", "allowPng", "targetScreenSize", "dpi", "codePage" };

		// Token: 0x04009F02 RID: 40706
		private static byte[] attributeNamespaceIds;
	}
}
