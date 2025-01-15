using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4B RID: 11083
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldItem : OpenXmlLeafElement
	{
		// Token: 0x17007814 RID: 30740
		// (get) Token: 0x06016B91 RID: 93073 RVA: 0x002F2EEE File Offset: 0x002F10EE
		public override string LocalName
		{
			get
			{
				return "x";
			}
		}

		// Token: 0x17007815 RID: 30741
		// (get) Token: 0x06016B92 RID: 93074 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007816 RID: 30742
		// (get) Token: 0x06016B93 RID: 93075 RVA: 0x0032E5A3 File Offset: 0x0032C7A3
		internal override int ElementTypeId
		{
			get
			{
				return 11066;
			}
		}

		// Token: 0x06016B94 RID: 93076 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007817 RID: 30743
		// (get) Token: 0x06016B95 RID: 93077 RVA: 0x0032E5AA File Offset: 0x0032C7AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return FieldItem.attributeTagNames;
			}
		}

		// Token: 0x17007818 RID: 30744
		// (get) Token: 0x06016B96 RID: 93078 RVA: 0x0032E5B1 File Offset: 0x0032C7B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FieldItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007819 RID: 30745
		// (get) Token: 0x06016B97 RID: 93079 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016B98 RID: 93080 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
		public UInt32Value Val
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

		// Token: 0x06016B9A RID: 93082 RVA: 0x0032E5B8 File Offset: 0x0032C7B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B9B RID: 93083 RVA: 0x0032E5D8 File Offset: 0x0032C7D8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldItem>(deep);
		}

		// Token: 0x06016B9C RID: 93084 RVA: 0x0032E5E4 File Offset: 0x0032C7E4
		// Note: this type is marked as 'beforefieldinit'.
		static FieldItem()
		{
			byte[] array = new byte[1];
			FieldItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099B3 RID: 39347
		private const string tagName = "x";

		// Token: 0x040099B4 RID: 39348
		private const byte tagNsId = 22;

		// Token: 0x040099B5 RID: 39349
		internal const int ElementTypeIdConst = 11066;

		// Token: 0x040099B6 RID: 39350
		private static string[] attributeTagNames = new string[] { "v" };

		// Token: 0x040099B7 RID: 39351
		private static byte[] attributeNamespaceIds;
	}
}
