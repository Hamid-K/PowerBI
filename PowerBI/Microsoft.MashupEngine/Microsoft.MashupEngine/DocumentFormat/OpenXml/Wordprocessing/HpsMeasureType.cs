using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E93 RID: 11923
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class HpsMeasureType : OpenXmlLeafElement
	{
		// Token: 0x17008B3F RID: 35647
		// (get) Token: 0x0601956E RID: 103790 RVA: 0x00348BA0 File Offset: 0x00346DA0
		internal override string[] AttributeTagNames
		{
			get
			{
				return HpsMeasureType.attributeTagNames;
			}
		}

		// Token: 0x17008B40 RID: 35648
		// (get) Token: 0x0601956F RID: 103791 RVA: 0x00348BA7 File Offset: 0x00346DA7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HpsMeasureType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B41 RID: 35649
		// (get) Token: 0x06019570 RID: 103792 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019571 RID: 103793 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019572 RID: 103794 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A86B RID: 43115
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A86C RID: 43116
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
