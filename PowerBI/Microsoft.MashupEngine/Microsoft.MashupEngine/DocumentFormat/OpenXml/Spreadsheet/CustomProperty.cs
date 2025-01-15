using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE5 RID: 11237
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomProperty : OpenXmlLeafElement
	{
		// Token: 0x17007DFA RID: 32250
		// (get) Token: 0x06017843 RID: 96323 RVA: 0x00337D09 File Offset: 0x00335F09
		public override string LocalName
		{
			get
			{
				return "customPr";
			}
		}

		// Token: 0x17007DFB RID: 32251
		// (get) Token: 0x06017844 RID: 96324 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DFC RID: 32252
		// (get) Token: 0x06017845 RID: 96325 RVA: 0x00337D10 File Offset: 0x00335F10
		internal override int ElementTypeId
		{
			get
			{
				return 11209;
			}
		}

		// Token: 0x06017846 RID: 96326 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DFD RID: 32253
		// (get) Token: 0x06017847 RID: 96327 RVA: 0x00337D17 File Offset: 0x00335F17
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomProperty.attributeTagNames;
			}
		}

		// Token: 0x17007DFE RID: 32254
		// (get) Token: 0x06017848 RID: 96328 RVA: 0x00337D1E File Offset: 0x00335F1E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DFF RID: 32255
		// (get) Token: 0x06017849 RID: 96329 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601784A RID: 96330 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007E00 RID: 32256
		// (get) Token: 0x0601784B RID: 96331 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601784C RID: 96332 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0601784E RID: 96334 RVA: 0x00337D25 File Offset: 0x00335F25
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601784F RID: 96335 RVA: 0x00337D5D File Offset: 0x00335F5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomProperty>(deep);
		}

		// Token: 0x04009C99 RID: 40089
		private const string tagName = "customPr";

		// Token: 0x04009C9A RID: 40090
		private const byte tagNsId = 22;

		// Token: 0x04009C9B RID: 40091
		internal const int ElementTypeIdConst = 11209;

		// Token: 0x04009C9C RID: 40092
		private static string[] attributeTagNames = new string[] { "name", "id" };

		// Token: 0x04009C9D RID: 40093
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };
	}
}
