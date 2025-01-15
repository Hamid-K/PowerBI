using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C38 RID: 11320
	[GeneratedCode("DomGen", "2.0")]
	internal class Sheet : OpenXmlLeafElement
	{
		// Token: 0x17008142 RID: 33090
		// (get) Token: 0x06017F5F RID: 98143 RVA: 0x002A77F6 File Offset: 0x002A59F6
		public override string LocalName
		{
			get
			{
				return "sheet";
			}
		}

		// Token: 0x17008143 RID: 33091
		// (get) Token: 0x06017F60 RID: 98144 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008144 RID: 33092
		// (get) Token: 0x06017F61 RID: 98145 RVA: 0x0033D13F File Offset: 0x0033B33F
		internal override int ElementTypeId
		{
			get
			{
				return 11302;
			}
		}

		// Token: 0x06017F62 RID: 98146 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008145 RID: 33093
		// (get) Token: 0x06017F63 RID: 98147 RVA: 0x0033D146 File Offset: 0x0033B346
		internal override string[] AttributeTagNames
		{
			get
			{
				return Sheet.attributeTagNames;
			}
		}

		// Token: 0x17008146 RID: 33094
		// (get) Token: 0x06017F64 RID: 98148 RVA: 0x0033D14D File Offset: 0x0033B34D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Sheet.attributeNamespaceIds;
			}
		}

		// Token: 0x17008147 RID: 33095
		// (get) Token: 0x06017F65 RID: 98149 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017F66 RID: 98150 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008148 RID: 33096
		// (get) Token: 0x06017F67 RID: 98151 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017F68 RID: 98152 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
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

		// Token: 0x17008149 RID: 33097
		// (get) Token: 0x06017F69 RID: 98153 RVA: 0x0033808E File Offset: 0x0033628E
		// (set) Token: 0x06017F6A RID: 98154 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "state")]
		public EnumValue<SheetStateValues> State
		{
			get
			{
				return (EnumValue<SheetStateValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700814A RID: 33098
		// (get) Token: 0x06017F6B RID: 98155 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06017F6C RID: 98156 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(19, "id")]
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

		// Token: 0x06017F6E RID: 98158 RVA: 0x0033D154 File Offset: 0x0033B354
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "state" == name)
			{
				return new EnumValue<SheetStateValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017F6F RID: 98159 RVA: 0x0033D1C3 File Offset: 0x0033B3C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sheet>(deep);
		}

		// Token: 0x04009E4C RID: 40524
		private const string tagName = "sheet";

		// Token: 0x04009E4D RID: 40525
		private const byte tagNsId = 22;

		// Token: 0x04009E4E RID: 40526
		internal const int ElementTypeIdConst = 11302;

		// Token: 0x04009E4F RID: 40527
		private static string[] attributeTagNames = new string[] { "name", "sheetId", "state", "id" };

		// Token: 0x04009E50 RID: 40528
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 19 };
	}
}
