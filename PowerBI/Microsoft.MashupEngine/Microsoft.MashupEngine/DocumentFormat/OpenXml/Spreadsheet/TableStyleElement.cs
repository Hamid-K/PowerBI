using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C19 RID: 11289
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleElement : OpenXmlLeafElement
	{
		// Token: 0x1700803C RID: 32828
		// (get) Token: 0x06017D18 RID: 97560 RVA: 0x0033B88B File Offset: 0x00339A8B
		public override string LocalName
		{
			get
			{
				return "tableStyleElement";
			}
		}

		// Token: 0x1700803D RID: 32829
		// (get) Token: 0x06017D19 RID: 97561 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700803E RID: 32830
		// (get) Token: 0x06017D1A RID: 97562 RVA: 0x0033B892 File Offset: 0x00339A92
		internal override int ElementTypeId
		{
			get
			{
				return 11270;
			}
		}

		// Token: 0x06017D1B RID: 97563 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700803F RID: 32831
		// (get) Token: 0x06017D1C RID: 97564 RVA: 0x0033B899 File Offset: 0x00339A99
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyleElement.attributeTagNames;
			}
		}

		// Token: 0x17008040 RID: 32832
		// (get) Token: 0x06017D1D RID: 97565 RVA: 0x0033B8A0 File Offset: 0x00339AA0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyleElement.attributeNamespaceIds;
			}
		}

		// Token: 0x17008041 RID: 32833
		// (get) Token: 0x06017D1E RID: 97566 RVA: 0x0033B8A7 File Offset: 0x00339AA7
		// (set) Token: 0x06017D1F RID: 97567 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<TableStyleValues> Type
		{
			get
			{
				return (EnumValue<TableStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008042 RID: 32834
		// (get) Token: 0x06017D20 RID: 97568 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017D21 RID: 97569 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "size")]
		public UInt32Value Size
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

		// Token: 0x17008043 RID: 32835
		// (get) Token: 0x06017D22 RID: 97570 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017D23 RID: 97571 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
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

		// Token: 0x06017D25 RID: 97573 RVA: 0x0033B8B8 File Offset: 0x00339AB8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<TableStyleValues>();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D26 RID: 97574 RVA: 0x0033B90F File Offset: 0x00339B0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleElement>(deep);
		}

		// Token: 0x06017D27 RID: 97575 RVA: 0x0033B918 File Offset: 0x00339B18
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyleElement()
		{
			byte[] array = new byte[3];
			TableStyleElement.attributeNamespaceIds = array;
		}

		// Token: 0x04009DB0 RID: 40368
		private const string tagName = "tableStyleElement";

		// Token: 0x04009DB1 RID: 40369
		private const byte tagNsId = 22;

		// Token: 0x04009DB2 RID: 40370
		internal const int ElementTypeIdConst = 11270;

		// Token: 0x04009DB3 RID: 40371
		private static string[] attributeTagNames = new string[] { "type", "size", "dxfId" };

		// Token: 0x04009DB4 RID: 40372
		private static byte[] attributeNamespaceIds;
	}
}
