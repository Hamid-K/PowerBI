using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2C RID: 12076
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingChange : OpenXmlLeafElement
	{
		// Token: 0x17008F46 RID: 36678
		// (get) Token: 0x06019E52 RID: 106066 RVA: 0x0035903A File Offset: 0x0035723A
		public override string LocalName
		{
			get
			{
				return "numberingChange";
			}
		}

		// Token: 0x17008F47 RID: 36679
		// (get) Token: 0x06019E53 RID: 106067 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F48 RID: 36680
		// (get) Token: 0x06019E54 RID: 106068 RVA: 0x00359041 File Offset: 0x00357241
		internal override int ElementTypeId
		{
			get
			{
				return 11714;
			}
		}

		// Token: 0x06019E55 RID: 106069 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F49 RID: 36681
		// (get) Token: 0x06019E56 RID: 106070 RVA: 0x00359048 File Offset: 0x00357248
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingChange.attributeTagNames;
			}
		}

		// Token: 0x17008F4A RID: 36682
		// (get) Token: 0x06019E57 RID: 106071 RVA: 0x0035904F File Offset: 0x0035724F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F4B RID: 36683
		// (get) Token: 0x06019E58 RID: 106072 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019E59 RID: 106073 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "original")]
		public StringValue Original
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

		// Token: 0x17008F4C RID: 36684
		// (get) Token: 0x06019E5A RID: 106074 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019E5B RID: 106075 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x17008F4D RID: 36685
		// (get) Token: 0x06019E5C RID: 106076 RVA: 0x0031FD86 File Offset: 0x0031DF86
		// (set) Token: 0x06019E5D RID: 106077 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008F4E RID: 36686
		// (get) Token: 0x06019E5E RID: 106078 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06019E5F RID: 106079 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06019E61 RID: 106081 RVA: 0x00359058 File Offset: 0x00357258
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "original" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019E62 RID: 106082 RVA: 0x003590CD File Offset: 0x003572CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingChange>(deep);
		}

		// Token: 0x0400AAC6 RID: 43718
		private const string tagName = "numberingChange";

		// Token: 0x0400AAC7 RID: 43719
		private const byte tagNsId = 23;

		// Token: 0x0400AAC8 RID: 43720
		internal const int ElementTypeIdConst = 11714;

		// Token: 0x0400AAC9 RID: 43721
		private static string[] attributeTagNames = new string[] { "original", "author", "date", "id" };

		// Token: 0x0400AACA RID: 43722
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
