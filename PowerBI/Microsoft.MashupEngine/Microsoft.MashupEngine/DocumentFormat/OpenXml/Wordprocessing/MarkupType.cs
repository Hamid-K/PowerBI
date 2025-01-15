using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4A RID: 11594
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MarkupType : OpenXmlLeafElement
	{
		// Token: 0x1700867A RID: 34426
		// (get) Token: 0x06018BB1 RID: 101297 RVA: 0x0034464A File Offset: 0x0034284A
		internal override string[] AttributeTagNames
		{
			get
			{
				return MarkupType.attributeTagNames;
			}
		}

		// Token: 0x1700867B RID: 34427
		// (get) Token: 0x06018BB2 RID: 101298 RVA: 0x00344651 File Offset: 0x00342851
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MarkupType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700867C RID: 34428
		// (get) Token: 0x06018BB3 RID: 101299 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018BB4 RID: 101300 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x06018BB5 RID: 101301 RVA: 0x002EE1BE File Offset: 0x002EC3BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A44C RID: 42060
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400A44D RID: 42061
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
