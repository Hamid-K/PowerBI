using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7D RID: 11133
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class HierarchyUsageType : OpenXmlLeafElement
	{
		// Token: 0x17007A2C RID: 31276
		// (get) Token: 0x06017027 RID: 94247 RVA: 0x00331AAA File Offset: 0x0032FCAA
		internal override string[] AttributeTagNames
		{
			get
			{
				return HierarchyUsageType.attributeTagNames;
			}
		}

		// Token: 0x17007A2D RID: 31277
		// (get) Token: 0x06017028 RID: 94248 RVA: 0x00331AB1 File Offset: 0x0032FCB1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HierarchyUsageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A2E RID: 31278
		// (get) Token: 0x06017029 RID: 94249 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601702A RID: 94250 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "hierarchyUsage")]
		public Int32Value Value
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

		// Token: 0x0601702B RID: 94251 RVA: 0x00331AB8 File Offset: 0x0032FCB8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "hierarchyUsage" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601702D RID: 94253 RVA: 0x00331AD8 File Offset: 0x0032FCD8
		// Note: this type is marked as 'beforefieldinit'.
		static HierarchyUsageType()
		{
			byte[] array = new byte[1];
			HierarchyUsageType.attributeNamespaceIds = array;
		}

		// Token: 0x04009AB9 RID: 39609
		private static string[] attributeTagNames = new string[] { "hierarchyUsage" };

		// Token: 0x04009ABA RID: 39610
		private static byte[] attributeNamespaceIds;
	}
}
