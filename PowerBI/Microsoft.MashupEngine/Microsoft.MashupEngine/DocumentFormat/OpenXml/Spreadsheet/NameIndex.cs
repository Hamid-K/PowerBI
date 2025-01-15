using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C03 RID: 11267
	[GeneratedCode("DomGen", "2.0")]
	internal class NameIndex : OpenXmlLeafElement
	{
		// Token: 0x17007F54 RID: 32596
		// (get) Token: 0x06017B2C RID: 97068 RVA: 0x0032EEDB File Offset: 0x0032D0DB
		public override string LocalName
		{
			get
			{
				return "n";
			}
		}

		// Token: 0x17007F55 RID: 32597
		// (get) Token: 0x06017B2D RID: 97069 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F56 RID: 32598
		// (get) Token: 0x06017B2E RID: 97070 RVA: 0x0033A0FF File Offset: 0x003382FF
		internal override int ElementTypeId
		{
			get
			{
				return 11246;
			}
		}

		// Token: 0x06017B2F RID: 97071 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F57 RID: 32599
		// (get) Token: 0x06017B30 RID: 97072 RVA: 0x0033A106 File Offset: 0x00338306
		internal override string[] AttributeTagNames
		{
			get
			{
				return NameIndex.attributeTagNames;
			}
		}

		// Token: 0x17007F58 RID: 32600
		// (get) Token: 0x06017B31 RID: 97073 RVA: 0x0033A10D File Offset: 0x0033830D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NameIndex.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F59 RID: 32601
		// (get) Token: 0x06017B32 RID: 97074 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B33 RID: 97075 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public UInt32Value Index
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

		// Token: 0x17007F5A RID: 32602
		// (get) Token: 0x06017B34 RID: 97076 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017B35 RID: 97077 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public BooleanValue IsASet
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

		// Token: 0x06017B37 RID: 97079 RVA: 0x0033A114 File Offset: 0x00338314
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B38 RID: 97080 RVA: 0x0033A14A File Offset: 0x0033834A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NameIndex>(deep);
		}

		// Token: 0x06017B39 RID: 97081 RVA: 0x0033A154 File Offset: 0x00338354
		// Note: this type is marked as 'beforefieldinit'.
		static NameIndex()
		{
			byte[] array = new byte[2];
			NameIndex.attributeNamespaceIds = array;
		}

		// Token: 0x04009D36 RID: 40246
		private const string tagName = "n";

		// Token: 0x04009D37 RID: 40247
		private const byte tagNsId = 22;

		// Token: 0x04009D38 RID: 40248
		internal const int ElementTypeIdConst = 11246;

		// Token: 0x04009D39 RID: 40249
		private static string[] attributeTagNames = new string[] { "x", "s" };

		// Token: 0x04009D3A RID: 40250
		private static byte[] attributeNamespaceIds;
	}
}
