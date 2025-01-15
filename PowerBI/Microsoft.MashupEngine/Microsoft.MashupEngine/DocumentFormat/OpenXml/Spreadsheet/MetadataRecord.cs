using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFC RID: 11260
	[GeneratedCode("DomGen", "2.0")]
	internal class MetadataRecord : OpenXmlLeafElement
	{
		// Token: 0x17007F12 RID: 32530
		// (get) Token: 0x06017A9D RID: 96925 RVA: 0x00339A9A File Offset: 0x00337C9A
		public override string LocalName
		{
			get
			{
				return "rc";
			}
		}

		// Token: 0x17007F13 RID: 32531
		// (get) Token: 0x06017A9E RID: 96926 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F14 RID: 32532
		// (get) Token: 0x06017A9F RID: 96927 RVA: 0x00339AA1 File Offset: 0x00337CA1
		internal override int ElementTypeId
		{
			get
			{
				return 11239;
			}
		}

		// Token: 0x06017AA0 RID: 96928 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F15 RID: 32533
		// (get) Token: 0x06017AA1 RID: 96929 RVA: 0x00339AA8 File Offset: 0x00337CA8
		internal override string[] AttributeTagNames
		{
			get
			{
				return MetadataRecord.attributeTagNames;
			}
		}

		// Token: 0x17007F16 RID: 32534
		// (get) Token: 0x06017AA2 RID: 96930 RVA: 0x00339AAF File Offset: 0x00337CAF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MetadataRecord.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F17 RID: 32535
		// (get) Token: 0x06017AA3 RID: 96931 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017AA4 RID: 96932 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public UInt32Value TypeIndex
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

		// Token: 0x17007F18 RID: 32536
		// (get) Token: 0x06017AA5 RID: 96933 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017AA6 RID: 96934 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "v")]
		public UInt32Value Val
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

		// Token: 0x06017AA8 RID: 96936 RVA: 0x00339AB6 File Offset: 0x00337CB6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "v" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017AA9 RID: 96937 RVA: 0x00339AEC File Offset: 0x00337CEC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MetadataRecord>(deep);
		}

		// Token: 0x06017AAA RID: 96938 RVA: 0x00339AF8 File Offset: 0x00337CF8
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataRecord()
		{
			byte[] array = new byte[2];
			MetadataRecord.attributeNamespaceIds = array;
		}

		// Token: 0x04009D11 RID: 40209
		private const string tagName = "rc";

		// Token: 0x04009D12 RID: 40210
		private const byte tagNsId = 22;

		// Token: 0x04009D13 RID: 40211
		internal const int ElementTypeIdConst = 11239;

		// Token: 0x04009D14 RID: 40212
		private static string[] attributeTagNames = new string[] { "t", "v" };

		// Token: 0x04009D15 RID: 40213
		private static byte[] attributeNamespaceIds;
	}
}
