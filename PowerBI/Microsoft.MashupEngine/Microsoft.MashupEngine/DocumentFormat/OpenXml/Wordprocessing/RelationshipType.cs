using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4C RID: 11852
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RelationshipType : OpenXmlLeafElement
	{
		// Token: 0x17008A24 RID: 35364
		// (get) Token: 0x0601931C RID: 103196 RVA: 0x00347710 File Offset: 0x00345910
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelationshipType.attributeTagNames;
			}
		}

		// Token: 0x17008A25 RID: 35365
		// (get) Token: 0x0601931D RID: 103197 RVA: 0x00347717 File Offset: 0x00345917
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelationshipType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A26 RID: 35366
		// (get) Token: 0x0601931E RID: 103198 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601931F RID: 103199 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019320 RID: 103200 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A77F RID: 42879
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400A780 RID: 42880
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
