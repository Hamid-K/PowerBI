using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C60 RID: 11360
	[GeneratedCode("DomGen", "2.0")]
	internal class FileRecoveryProperties : OpenXmlLeafElement
	{
		// Token: 0x1700827E RID: 33406
		// (get) Token: 0x06018240 RID: 98880 RVA: 0x0033EE53 File Offset: 0x0033D053
		public override string LocalName
		{
			get
			{
				return "fileRecoveryPr";
			}
		}

		// Token: 0x1700827F RID: 33407
		// (get) Token: 0x06018241 RID: 98881 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008280 RID: 33408
		// (get) Token: 0x06018242 RID: 98882 RVA: 0x0033EE5A File Offset: 0x0033D05A
		internal override int ElementTypeId
		{
			get
			{
				return 11341;
			}
		}

		// Token: 0x06018243 RID: 98883 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008281 RID: 33409
		// (get) Token: 0x06018244 RID: 98884 RVA: 0x0033EE61 File Offset: 0x0033D061
		internal override string[] AttributeTagNames
		{
			get
			{
				return FileRecoveryProperties.attributeTagNames;
			}
		}

		// Token: 0x17008282 RID: 33410
		// (get) Token: 0x06018245 RID: 98885 RVA: 0x0033EE68 File Offset: 0x0033D068
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FileRecoveryProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008283 RID: 33411
		// (get) Token: 0x06018246 RID: 98886 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018247 RID: 98887 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "autoRecover")]
		public BooleanValue AutoRecover
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

		// Token: 0x17008284 RID: 33412
		// (get) Token: 0x06018248 RID: 98888 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018249 RID: 98889 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "crashSave")]
		public BooleanValue CrashSave
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

		// Token: 0x17008285 RID: 33413
		// (get) Token: 0x0601824A RID: 98890 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601824B RID: 98891 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dataExtractLoad")]
		public BooleanValue DataExtractLoad
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

		// Token: 0x17008286 RID: 33414
		// (get) Token: 0x0601824C RID: 98892 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601824D RID: 98893 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "repairLoad")]
		public BooleanValue RepairLoad
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

		// Token: 0x0601824F RID: 98895 RVA: 0x0033EE70 File Offset: 0x0033D070
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "autoRecover" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "crashSave" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dataExtractLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "repairLoad" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018250 RID: 98896 RVA: 0x0033EEDD File Offset: 0x0033D0DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FileRecoveryProperties>(deep);
		}

		// Token: 0x06018251 RID: 98897 RVA: 0x0033EEE8 File Offset: 0x0033D0E8
		// Note: this type is marked as 'beforefieldinit'.
		static FileRecoveryProperties()
		{
			byte[] array = new byte[4];
			FileRecoveryProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009F03 RID: 40707
		private const string tagName = "fileRecoveryPr";

		// Token: 0x04009F04 RID: 40708
		private const byte tagNsId = 22;

		// Token: 0x04009F05 RID: 40709
		internal const int ElementTypeIdConst = 11341;

		// Token: 0x04009F06 RID: 40710
		private static string[] attributeTagNames = new string[] { "autoRecover", "crashSave", "dataExtractLoad", "repairLoad" };

		// Token: 0x04009F07 RID: 40711
		private static byte[] attributeNamespaceIds;
	}
}
