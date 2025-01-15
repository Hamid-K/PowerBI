using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A2 RID: 9378
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class LineJoinMiterProperties : OpenXmlLeafElement
	{
		// Token: 0x170051D5 RID: 20949
		// (get) Token: 0x06011523 RID: 70947 RVA: 0x002ED220 File Offset: 0x002EB420
		public override string LocalName
		{
			get
			{
				return "miter";
			}
		}

		// Token: 0x170051D6 RID: 20950
		// (get) Token: 0x06011524 RID: 70948 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051D7 RID: 20951
		// (get) Token: 0x06011525 RID: 70949 RVA: 0x002ED227 File Offset: 0x002EB427
		internal override int ElementTypeId
		{
			get
			{
				return 12852;
			}
		}

		// Token: 0x06011526 RID: 70950 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051D8 RID: 20952
		// (get) Token: 0x06011527 RID: 70951 RVA: 0x002ED22E File Offset: 0x002EB42E
		internal override string[] AttributeTagNames
		{
			get
			{
				return LineJoinMiterProperties.attributeTagNames;
			}
		}

		// Token: 0x170051D9 RID: 20953
		// (get) Token: 0x06011528 RID: 70952 RVA: 0x002ED235 File Offset: 0x002EB435
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LineJoinMiterProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170051DA RID: 20954
		// (get) Token: 0x06011529 RID: 70953 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601152A RID: 70954 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "lim")]
		public Int32Value Limit
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601152C RID: 70956 RVA: 0x002ED23C File Offset: 0x002EB43C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "lim" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601152D RID: 70957 RVA: 0x002ED25E File Offset: 0x002EB45E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineJoinMiterProperties>(deep);
		}

		// Token: 0x0400794C RID: 31052
		private const string tagName = "miter";

		// Token: 0x0400794D RID: 31053
		private const byte tagNsId = 52;

		// Token: 0x0400794E RID: 31054
		internal const int ElementTypeIdConst = 12852;

		// Token: 0x0400794F RID: 31055
		private static string[] attributeTagNames = new string[] { "lim" };

		// Token: 0x04007950 RID: 31056
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
