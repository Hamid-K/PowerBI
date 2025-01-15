using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5A RID: 11354
	[GeneratedCode("DomGen", "2.0")]
	internal class OleSize : OpenXmlLeafElement
	{
		// Token: 0x1700825B RID: 33371
		// (get) Token: 0x060181EE RID: 98798 RVA: 0x0033EB85 File Offset: 0x0033CD85
		public override string LocalName
		{
			get
			{
				return "oleSize";
			}
		}

		// Token: 0x1700825C RID: 33372
		// (get) Token: 0x060181EF RID: 98799 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700825D RID: 33373
		// (get) Token: 0x060181F0 RID: 98800 RVA: 0x0033EB8C File Offset: 0x0033CD8C
		internal override int ElementTypeId
		{
			get
			{
				return 11335;
			}
		}

		// Token: 0x060181F1 RID: 98801 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700825E RID: 33374
		// (get) Token: 0x060181F2 RID: 98802 RVA: 0x0033EB93 File Offset: 0x0033CD93
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleSize.attributeTagNames;
			}
		}

		// Token: 0x1700825F RID: 33375
		// (get) Token: 0x060181F3 RID: 98803 RVA: 0x0033EB9A File Offset: 0x0033CD9A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleSize.attributeNamespaceIds;
			}
		}

		// Token: 0x17008260 RID: 33376
		// (get) Token: 0x060181F4 RID: 98804 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060181F5 RID: 98805 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x060181F7 RID: 98807 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060181F8 RID: 98808 RVA: 0x0033EBA1 File Offset: 0x0033CDA1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleSize>(deep);
		}

		// Token: 0x060181F9 RID: 98809 RVA: 0x0033EBAC File Offset: 0x0033CDAC
		// Note: this type is marked as 'beforefieldinit'.
		static OleSize()
		{
			byte[] array = new byte[1];
			OleSize.attributeNamespaceIds = array;
		}

		// Token: 0x04009EEB RID: 40683
		private const string tagName = "oleSize";

		// Token: 0x04009EEC RID: 40684
		private const byte tagNsId = 22;

		// Token: 0x04009EED RID: 40685
		internal const int ElementTypeIdConst = 11335;

		// Token: 0x04009EEE RID: 40686
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x04009EEF RID: 40687
		private static byte[] attributeNamespaceIds;
	}
}
