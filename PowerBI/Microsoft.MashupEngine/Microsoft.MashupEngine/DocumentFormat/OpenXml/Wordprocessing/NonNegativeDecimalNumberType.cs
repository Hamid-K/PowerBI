using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F28 RID: 12072
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NonNegativeDecimalNumberType : OpenXmlLeafElement
	{
		// Token: 0x17008F3A RID: 36666
		// (get) Token: 0x06019E39 RID: 106041 RVA: 0x00358FB0 File Offset: 0x003571B0
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonNegativeDecimalNumberType.attributeTagNames;
			}
		}

		// Token: 0x17008F3B RID: 36667
		// (get) Token: 0x06019E3A RID: 106042 RVA: 0x00358FB7 File Offset: 0x003571B7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonNegativeDecimalNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F3C RID: 36668
		// (get) Token: 0x06019E3B RID: 106043 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019E3C RID: 106044 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019E3D RID: 106045 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AABB RID: 43707
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AABC RID: 43708
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
