using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A55 RID: 10837
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class EmbeddedFontDataIdType : OpenXmlLeafElement
	{
		// Token: 0x17007205 RID: 29189
		// (get) Token: 0x06015DC1 RID: 89537 RVA: 0x00323CEB File Offset: 0x00321EEB
		internal override string[] AttributeTagNames
		{
			get
			{
				return EmbeddedFontDataIdType.attributeTagNames;
			}
		}

		// Token: 0x17007206 RID: 29190
		// (get) Token: 0x06015DC2 RID: 89538 RVA: 0x00323CF2 File Offset: 0x00321EF2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EmbeddedFontDataIdType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007207 RID: 29191
		// (get) Token: 0x06015DC3 RID: 89539 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015DC4 RID: 89540 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x06015DC5 RID: 89541 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04009524 RID: 38180
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009525 RID: 38181
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
