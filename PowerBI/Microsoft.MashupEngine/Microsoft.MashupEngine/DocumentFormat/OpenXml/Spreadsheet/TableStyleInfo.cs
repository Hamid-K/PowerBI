using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C64 RID: 11364
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleInfo : OpenXmlLeafElement
	{
		// Token: 0x17008296 RID: 33430
		// (get) Token: 0x0601827C RID: 98940 RVA: 0x0033F03B File Offset: 0x0033D23B
		public override string LocalName
		{
			get
			{
				return "tableStyleInfo";
			}
		}

		// Token: 0x17008297 RID: 33431
		// (get) Token: 0x0601827D RID: 98941 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008298 RID: 33432
		// (get) Token: 0x0601827E RID: 98942 RVA: 0x0033F042 File Offset: 0x0033D242
		internal override int ElementTypeId
		{
			get
			{
				return 11345;
			}
		}

		// Token: 0x0601827F RID: 98943 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008299 RID: 33433
		// (get) Token: 0x06018280 RID: 98944 RVA: 0x0033F049 File Offset: 0x0033D249
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyleInfo.attributeTagNames;
			}
		}

		// Token: 0x1700829A RID: 33434
		// (get) Token: 0x06018281 RID: 98945 RVA: 0x0033F050 File Offset: 0x0033D250
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyleInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x1700829B RID: 33435
		// (get) Token: 0x06018282 RID: 98946 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018283 RID: 98947 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700829C RID: 33436
		// (get) Token: 0x06018284 RID: 98948 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018285 RID: 98949 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showFirstColumn")]
		public BooleanValue ShowFirstColumn
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

		// Token: 0x1700829D RID: 33437
		// (get) Token: 0x06018286 RID: 98950 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06018287 RID: 98951 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showLastColumn")]
		public BooleanValue ShowLastColumn
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

		// Token: 0x1700829E RID: 33438
		// (get) Token: 0x06018288 RID: 98952 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06018289 RID: 98953 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showRowStripes")]
		public BooleanValue ShowRowStripes
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

		// Token: 0x1700829F RID: 33439
		// (get) Token: 0x0601828A RID: 98954 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601828B RID: 98955 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showColumnStripes")]
		public BooleanValue ShowColumnStripes
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

		// Token: 0x0601828D RID: 98957 RVA: 0x0033F058 File Offset: 0x0033D258
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showFirstColumn" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showLastColumn" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showRowStripes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showColumnStripes" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601828E RID: 98958 RVA: 0x0033F0DB File Offset: 0x0033D2DB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleInfo>(deep);
		}

		// Token: 0x0601828F RID: 98959 RVA: 0x0033F0E4 File Offset: 0x0033D2E4
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyleInfo()
		{
			byte[] array = new byte[5];
			TableStyleInfo.attributeNamespaceIds = array;
		}

		// Token: 0x04009F15 RID: 40725
		private const string tagName = "tableStyleInfo";

		// Token: 0x04009F16 RID: 40726
		private const byte tagNsId = 22;

		// Token: 0x04009F17 RID: 40727
		internal const int ElementTypeIdConst = 11345;

		// Token: 0x04009F18 RID: 40728
		private static string[] attributeTagNames = new string[] { "name", "showFirstColumn", "showLastColumn", "showRowStripes", "showColumnStripes" };

		// Token: 0x04009F19 RID: 40729
		private static byte[] attributeNamespaceIds;
	}
}
