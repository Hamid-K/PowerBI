using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F05 RID: 12037
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UnsignedDecimalNumberMax3Type : OpenXmlLeafElement
	{
		// Token: 0x17008DE8 RID: 36328
		// (get) Token: 0x06019B22 RID: 105250 RVA: 0x00354060 File Offset: 0x00352260
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsignedDecimalNumberMax3Type.attributeTagNames;
			}
		}

		// Token: 0x17008DE9 RID: 36329
		// (get) Token: 0x06019B23 RID: 105251 RVA: 0x00354067 File Offset: 0x00352267
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsignedDecimalNumberMax3Type.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DEA RID: 36330
		// (get) Token: 0x06019B24 RID: 105252 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019B25 RID: 105253 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
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

		// Token: 0x06019B26 RID: 105254 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AA2A RID: 43562
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA2B RID: 43563
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
