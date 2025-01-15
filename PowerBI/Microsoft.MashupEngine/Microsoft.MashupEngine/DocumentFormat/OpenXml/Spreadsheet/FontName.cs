using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C16 RID: 11286
	[GeneratedCode("DomGen", "2.0")]
	internal class FontName : OpenXmlLeafElement
	{
		// Token: 0x1700802A RID: 32810
		// (get) Token: 0x06017CF4 RID: 97524 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x1700802B RID: 32811
		// (get) Token: 0x06017CF5 RID: 97525 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700802C RID: 32812
		// (get) Token: 0x06017CF6 RID: 97526 RVA: 0x0033B79C File Offset: 0x0033999C
		internal override int ElementTypeId
		{
			get
			{
				return 11267;
			}
		}

		// Token: 0x06017CF7 RID: 97527 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700802D RID: 32813
		// (get) Token: 0x06017CF8 RID: 97528 RVA: 0x0033B7A3 File Offset: 0x003399A3
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontName.attributeTagNames;
			}
		}

		// Token: 0x1700802E RID: 32814
		// (get) Token: 0x06017CF9 RID: 97529 RVA: 0x0033B7AA File Offset: 0x003399AA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontName.attributeNamespaceIds;
			}
		}

		// Token: 0x1700802F RID: 32815
		// (get) Token: 0x06017CFA RID: 97530 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017CFB RID: 97531 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017CFD RID: 97533 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017CFE RID: 97534 RVA: 0x0033B7B1 File Offset: 0x003399B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontName>(deep);
		}

		// Token: 0x06017CFF RID: 97535 RVA: 0x0033B7BC File Offset: 0x003399BC
		// Note: this type is marked as 'beforefieldinit'.
		static FontName()
		{
			byte[] array = new byte[1];
			FontName.attributeNamespaceIds = array;
		}

		// Token: 0x04009DA1 RID: 40353
		private const string tagName = "name";

		// Token: 0x04009DA2 RID: 40354
		private const byte tagNsId = 22;

		// Token: 0x04009DA3 RID: 40355
		internal const int ElementTypeIdConst = 11267;

		// Token: 0x04009DA4 RID: 40356
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009DA5 RID: 40357
		private static byte[] attributeNamespaceIds;
	}
}
