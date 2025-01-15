using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256A RID: 9578
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RelationshipIdType : OpenXmlLeafElement
	{
		// Token: 0x170055C2 RID: 21954
		// (get) Token: 0x06011DC5 RID: 73157 RVA: 0x002F32D7 File Offset: 0x002F14D7
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelationshipIdType.attributeTagNames;
			}
		}

		// Token: 0x170055C3 RID: 21955
		// (get) Token: 0x06011DC6 RID: 73158 RVA: 0x002F32DE File Offset: 0x002F14DE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelationshipIdType.attributeNamespaceIds;
			}
		}

		// Token: 0x170055C4 RID: 21956
		// (get) Token: 0x06011DC7 RID: 73159 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011DC8 RID: 73160 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011DC9 RID: 73161 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04007CF0 RID: 31984
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04007CF1 RID: 31985
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
