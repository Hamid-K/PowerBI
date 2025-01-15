using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE9 RID: 12009
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OnOffOnlyType : OpenXmlLeafElement
	{
		// Token: 0x17008D6F RID: 36207
		// (get) Token: 0x06019A2D RID: 105005 RVA: 0x003537EB File Offset: 0x003519EB
		internal override string[] AttributeTagNames
		{
			get
			{
				return OnOffOnlyType.attributeTagNames;
			}
		}

		// Token: 0x17008D70 RID: 36208
		// (get) Token: 0x06019A2E RID: 105006 RVA: 0x003537F2 File Offset: 0x003519F2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OnOffOnlyType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D71 RID: 36209
		// (get) Token: 0x06019A2F RID: 105007 RVA: 0x003537F9 File Offset: 0x003519F9
		// (set) Token: 0x06019A30 RID: 105008 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<OnOffOnlyValues> Val
		{
			get
			{
				return (EnumValue<OnOffOnlyValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019A31 RID: 105009 RVA: 0x00353808 File Offset: 0x00351A08
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<OnOffOnlyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A9C9 RID: 43465
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A9CA RID: 43466
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
