using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C29 RID: 11305
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetName : OpenXmlLeafElement
	{
		// Token: 0x170080B4 RID: 32948
		// (get) Token: 0x06017E2E RID: 97838 RVA: 0x0033C317 File Offset: 0x0033A517
		public override string LocalName
		{
			get
			{
				return "sheetName";
			}
		}

		// Token: 0x170080B5 RID: 32949
		// (get) Token: 0x06017E2F RID: 97839 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080B6 RID: 32950
		// (get) Token: 0x06017E30 RID: 97840 RVA: 0x0033C31E File Offset: 0x0033A51E
		internal override int ElementTypeId
		{
			get
			{
				return 11286;
			}
		}

		// Token: 0x06017E31 RID: 97841 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080B7 RID: 32951
		// (get) Token: 0x06017E32 RID: 97842 RVA: 0x0033C325 File Offset: 0x0033A525
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetName.attributeTagNames;
			}
		}

		// Token: 0x170080B8 RID: 32952
		// (get) Token: 0x06017E33 RID: 97843 RVA: 0x0033C32C File Offset: 0x0033A52C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetName.attributeNamespaceIds;
			}
		}

		// Token: 0x170080B9 RID: 32953
		// (get) Token: 0x06017E34 RID: 97844 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017E35 RID: 97845 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x06017E37 RID: 97847 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017E38 RID: 97848 RVA: 0x0033C333 File Offset: 0x0033A533
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetName>(deep);
		}

		// Token: 0x06017E39 RID: 97849 RVA: 0x0033C33C File Offset: 0x0033A53C
		// Note: this type is marked as 'beforefieldinit'.
		static SheetName()
		{
			byte[] array = new byte[1];
			SheetName.attributeNamespaceIds = array;
		}

		// Token: 0x04009E02 RID: 40450
		private const string tagName = "sheetName";

		// Token: 0x04009E03 RID: 40451
		private const byte tagNsId = 22;

		// Token: 0x04009E04 RID: 40452
		internal const int ElementTypeIdConst = 11286;

		// Token: 0x04009E05 RID: 40453
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009E06 RID: 40454
		private static byte[] attributeNamespaceIds;
	}
}
