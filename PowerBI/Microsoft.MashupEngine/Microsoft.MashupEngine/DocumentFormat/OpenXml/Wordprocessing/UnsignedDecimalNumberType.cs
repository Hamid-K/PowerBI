using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6B RID: 12139
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UnsignedDecimalNumberType : OpenXmlLeafElement
	{
		// Token: 0x170090D1 RID: 37073
		// (get) Token: 0x0601A1BE RID: 106942 RVA: 0x0035D8F2 File Offset: 0x0035BAF2
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsignedDecimalNumberType.attributeTagNames;
			}
		}

		// Token: 0x170090D2 RID: 37074
		// (get) Token: 0x0601A1BF RID: 106943 RVA: 0x0035D8F9 File Offset: 0x0035BAF9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsignedDecimalNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x170090D3 RID: 37075
		// (get) Token: 0x0601A1C0 RID: 106944 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601A1C1 RID: 106945 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A1C2 RID: 106946 RVA: 0x00348AE4 File Offset: 0x00346CE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400ABD3 RID: 43987
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABD4 RID: 43988
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
