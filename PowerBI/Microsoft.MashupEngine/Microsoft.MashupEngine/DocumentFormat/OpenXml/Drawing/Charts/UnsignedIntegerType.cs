using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200253E RID: 9534
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UnsignedIntegerType : OpenXmlLeafElement
	{
		// Token: 0x170054DF RID: 21727
		// (get) Token: 0x06011BD8 RID: 72664 RVA: 0x002F18F9 File Offset: 0x002EFAF9
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsignedIntegerType.attributeTagNames;
			}
		}

		// Token: 0x170054E0 RID: 21728
		// (get) Token: 0x06011BD9 RID: 72665 RVA: 0x002F1900 File Offset: 0x002EFB00
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsignedIntegerType.attributeNamespaceIds;
			}
		}

		// Token: 0x170054E1 RID: 21729
		// (get) Token: 0x06011BDA RID: 72666 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011BDB RID: 72667 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
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

		// Token: 0x06011BDC RID: 72668 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011BDE RID: 72670 RVA: 0x002F1908 File Offset: 0x002EFB08
		// Note: this type is marked as 'beforefieldinit'.
		static UnsignedIntegerType()
		{
			byte[] array = new byte[1];
			UnsignedIntegerType.attributeNamespaceIds = array;
		}

		// Token: 0x04007C51 RID: 31825
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007C52 RID: 31826
		private static byte[] attributeNamespaceIds;
	}
}
