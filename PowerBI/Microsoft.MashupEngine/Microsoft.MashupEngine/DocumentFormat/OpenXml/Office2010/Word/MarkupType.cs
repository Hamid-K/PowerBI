using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B5 RID: 9397
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MarkupType : OpenXmlLeafElement
	{
		// Token: 0x1700526B RID: 21099
		// (get) Token: 0x06011660 RID: 71264 RVA: 0x002EE1B0 File Offset: 0x002EC3B0
		internal override string[] AttributeTagNames
		{
			get
			{
				return MarkupType.attributeTagNames;
			}
		}

		// Token: 0x1700526C RID: 21100
		// (get) Token: 0x06011661 RID: 71265 RVA: 0x002EE1B7 File Offset: 0x002EC3B7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MarkupType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700526D RID: 21101
		// (get) Token: 0x06011662 RID: 71266 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011663 RID: 71267 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011664 RID: 71268 RVA: 0x002EE1BE File Offset: 0x002EC3BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040079A6 RID: 31142
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040079A7 RID: 31143
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
