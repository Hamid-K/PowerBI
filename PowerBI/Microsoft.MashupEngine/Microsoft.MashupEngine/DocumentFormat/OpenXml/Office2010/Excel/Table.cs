using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E4 RID: 9188
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Table : OpenXmlLeafElement
	{
		// Token: 0x17004D9D RID: 19869
		// (get) Token: 0x06010B89 RID: 68489 RVA: 0x00049581 File Offset: 0x00047781
		public override string LocalName
		{
			get
			{
				return "table";
			}
		}

		// Token: 0x17004D9E RID: 19870
		// (get) Token: 0x06010B8A RID: 68490 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D9F RID: 19871
		// (get) Token: 0x06010B8B RID: 68491 RVA: 0x002E6686 File Offset: 0x002E4886
		internal override int ElementTypeId
		{
			get
			{
				return 12914;
			}
		}

		// Token: 0x06010B8C RID: 68492 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DA0 RID: 19872
		// (get) Token: 0x06010B8D RID: 68493 RVA: 0x002E668D File Offset: 0x002E488D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Table.attributeTagNames;
			}
		}

		// Token: 0x17004DA1 RID: 19873
		// (get) Token: 0x06010B8E RID: 68494 RVA: 0x002E6694 File Offset: 0x002E4894
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Table.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DA2 RID: 19874
		// (get) Token: 0x06010B8F RID: 68495 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010B90 RID: 68496 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "altText")]
		public StringValue AltText
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

		// Token: 0x17004DA3 RID: 19875
		// (get) Token: 0x06010B91 RID: 68497 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010B92 RID: 68498 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "altTextSummary")]
		public StringValue AltTextSummary
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

		// Token: 0x06010B94 RID: 68500 RVA: 0x002E669B File Offset: 0x002E489B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "altText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "altTextSummary" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B95 RID: 68501 RVA: 0x002E66D1 File Offset: 0x002E48D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Table>(deep);
		}

		// Token: 0x06010B96 RID: 68502 RVA: 0x002E66DC File Offset: 0x002E48DC
		// Note: this type is marked as 'beforefieldinit'.
		static Table()
		{
			byte[] array = new byte[2];
			Table.attributeNamespaceIds = array;
		}

		// Token: 0x0400760D RID: 30221
		private const string tagName = "table";

		// Token: 0x0400760E RID: 30222
		private const byte tagNsId = 53;

		// Token: 0x0400760F RID: 30223
		internal const int ElementTypeIdConst = 12914;

		// Token: 0x04007610 RID: 30224
		private static string[] attributeTagNames = new string[] { "altText", "altTextSummary" };

		// Token: 0x04007611 RID: 30225
		private static byte[] attributeNamespaceIds;
	}
}
