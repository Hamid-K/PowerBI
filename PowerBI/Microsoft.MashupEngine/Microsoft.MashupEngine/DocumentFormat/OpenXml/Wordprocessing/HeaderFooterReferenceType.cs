using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3D RID: 11837
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class HeaderFooterReferenceType : OpenXmlLeafElement
	{
		// Token: 0x170089AC RID: 35244
		// (get) Token: 0x06019222 RID: 102946 RVA: 0x00346A2F File Offset: 0x00344C2F
		internal override string[] AttributeTagNames
		{
			get
			{
				return HeaderFooterReferenceType.attributeTagNames;
			}
		}

		// Token: 0x170089AD RID: 35245
		// (get) Token: 0x06019223 RID: 102947 RVA: 0x00346A36 File Offset: 0x00344C36
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HeaderFooterReferenceType.attributeNamespaceIds;
			}
		}

		// Token: 0x170089AE RID: 35246
		// (get) Token: 0x06019224 RID: 102948 RVA: 0x00346A3D File Offset: 0x00344C3D
		// (set) Token: 0x06019225 RID: 102949 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<HeaderFooterValues> Type
		{
			get
			{
				return (EnumValue<HeaderFooterValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170089AF RID: 35247
		// (get) Token: 0x06019226 RID: 102950 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019227 RID: 102951 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06019228 RID: 102952 RVA: 0x00346A4C File Offset: 0x00344C4C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<HeaderFooterValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A739 RID: 42809
		private static string[] attributeTagNames = new string[] { "type", "id" };

		// Token: 0x0400A73A RID: 42810
		private static byte[] attributeNamespaceIds = new byte[] { 23, 19 };
	}
}
