using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D50 RID: 11600
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class StringType : OpenXmlLeafElement
	{
		// Token: 0x1700868C RID: 34444
		// (get) Token: 0x06018BD6 RID: 101334 RVA: 0x00344707 File Offset: 0x00342907
		internal override string[] AttributeTagNames
		{
			get
			{
				return StringType.attributeTagNames;
			}
		}

		// Token: 0x1700868D RID: 34445
		// (get) Token: 0x06018BD7 RID: 101335 RVA: 0x0034470E File Offset: 0x0034290E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StringType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700868E RID: 34446
		// (get) Token: 0x06018BD8 RID: 101336 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018BD9 RID: 101337 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x06018BDA RID: 101338 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A45D RID: 42077
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A45E RID: 42078
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
