using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC9 RID: 11209
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetId : OpenXmlLeafElement
	{
		// Token: 0x17007CB6 RID: 31926
		// (get) Token: 0x0601759D RID: 95645 RVA: 0x00335B15 File Offset: 0x00333D15
		public override string LocalName
		{
			get
			{
				return "sheetId";
			}
		}

		// Token: 0x17007CB7 RID: 31927
		// (get) Token: 0x0601759E RID: 95646 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CB8 RID: 31928
		// (get) Token: 0x0601759F RID: 95647 RVA: 0x00335B1C File Offset: 0x00333D1C
		internal override int ElementTypeId
		{
			get
			{
				return 11177;
			}
		}

		// Token: 0x060175A0 RID: 95648 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CB9 RID: 31929
		// (get) Token: 0x060175A1 RID: 95649 RVA: 0x00335B23 File Offset: 0x00333D23
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetId.attributeTagNames;
			}
		}

		// Token: 0x17007CBA RID: 31930
		// (get) Token: 0x060175A2 RID: 95650 RVA: 0x00335B2A File Offset: 0x00333D2A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetId.attributeNamespaceIds;
			}
		}

		// Token: 0x17007CBB RID: 31931
		// (get) Token: 0x060175A3 RID: 95651 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060175A4 RID: 95652 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
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

		// Token: 0x060175A6 RID: 95654 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060175A7 RID: 95655 RVA: 0x00335B31 File Offset: 0x00333D31
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetId>(deep);
		}

		// Token: 0x060175A8 RID: 95656 RVA: 0x00335B3C File Offset: 0x00333D3C
		// Note: this type is marked as 'beforefieldinit'.
		static SheetId()
		{
			byte[] array = new byte[1];
			SheetId.attributeNamespaceIds = array;
		}

		// Token: 0x04009C0A RID: 39946
		private const string tagName = "sheetId";

		// Token: 0x04009C0B RID: 39947
		private const byte tagNsId = 22;

		// Token: 0x04009C0C RID: 39948
		internal const int ElementTypeIdConst = 11177;

		// Token: 0x04009C0D RID: 39949
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009C0E RID: 39950
		private static byte[] attributeNamespaceIds;
	}
}
