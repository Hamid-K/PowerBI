using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B4 RID: 9396
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DocumentId : OpenXmlLeafElement
	{
		// Token: 0x17005265 RID: 21093
		// (get) Token: 0x06011654 RID: 71252 RVA: 0x002EE154 File Offset: 0x002EC354
		public override string LocalName
		{
			get
			{
				return "docId";
			}
		}

		// Token: 0x17005266 RID: 21094
		// (get) Token: 0x06011655 RID: 71253 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005267 RID: 21095
		// (get) Token: 0x06011656 RID: 71254 RVA: 0x002EE15B File Offset: 0x002EC35B
		internal override int ElementTypeId
		{
			get
			{
				return 12866;
			}
		}

		// Token: 0x06011657 RID: 71255 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005268 RID: 21096
		// (get) Token: 0x06011658 RID: 71256 RVA: 0x002EE162 File Offset: 0x002EC362
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentId.attributeTagNames;
			}
		}

		// Token: 0x17005269 RID: 21097
		// (get) Token: 0x06011659 RID: 71257 RVA: 0x002EE169 File Offset: 0x002EC369
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentId.attributeNamespaceIds;
			}
		}

		// Token: 0x1700526A RID: 21098
		// (get) Token: 0x0601165A RID: 71258 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601165B RID: 71259 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601165D RID: 71261 RVA: 0x002ECC12 File Offset: 0x002EAE12
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601165E RID: 71262 RVA: 0x002EE170 File Offset: 0x002EC370
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentId>(deep);
		}

		// Token: 0x040079A1 RID: 31137
		private const string tagName = "docId";

		// Token: 0x040079A2 RID: 31138
		private const byte tagNsId = 52;

		// Token: 0x040079A3 RID: 31139
		internal const int ElementTypeIdConst = 12866;

		// Token: 0x040079A4 RID: 31140
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040079A5 RID: 31141
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
