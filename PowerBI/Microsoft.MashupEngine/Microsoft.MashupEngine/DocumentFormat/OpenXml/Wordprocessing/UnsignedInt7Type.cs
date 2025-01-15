using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE9 RID: 12265
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UnsignedInt7Type : OpenXmlLeafElement
	{
		// Token: 0x1700951B RID: 38171
		// (get) Token: 0x0601AAD4 RID: 109268 RVA: 0x00365D18 File Offset: 0x00363F18
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsignedInt7Type.attributeTagNames;
			}
		}

		// Token: 0x1700951C RID: 38172
		// (get) Token: 0x0601AAD5 RID: 109269 RVA: 0x00365D1F File Offset: 0x00363F1F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsignedInt7Type.attributeNamespaceIds;
			}
		}

		// Token: 0x1700951D RID: 38173
		// (get) Token: 0x0601AAD6 RID: 109270 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601AAD7 RID: 109271 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601AAD8 RID: 109272 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400ADF5 RID: 44533
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADF6 RID: 44534
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
