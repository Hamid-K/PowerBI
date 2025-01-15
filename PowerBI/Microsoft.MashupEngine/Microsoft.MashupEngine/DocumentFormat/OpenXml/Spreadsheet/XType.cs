using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5D RID: 11101
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class XType : OpenXmlLeafElement
	{
		// Token: 0x170078C7 RID: 30919
		// (get) Token: 0x06016D28 RID: 93480 RVA: 0x0032F784 File Offset: 0x0032D984
		internal override string[] AttributeTagNames
		{
			get
			{
				return XType.attributeTagNames;
			}
		}

		// Token: 0x170078C8 RID: 30920
		// (get) Token: 0x06016D29 RID: 93481 RVA: 0x0032F78B File Offset: 0x0032D98B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return XType.attributeNamespaceIds;
			}
		}

		// Token: 0x170078C9 RID: 30921
		// (get) Token: 0x06016D2A RID: 93482 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016D2B RID: 93483 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
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

		// Token: 0x06016D2C RID: 93484 RVA: 0x0032F792 File Offset: 0x0032D992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D2E RID: 93486 RVA: 0x0032F7B4 File Offset: 0x0032D9B4
		// Note: this type is marked as 'beforefieldinit'.
		static XType()
		{
			byte[] array = new byte[1];
			XType.attributeNamespaceIds = array;
		}

		// Token: 0x04009A0A RID: 39434
		private static string[] attributeTagNames = new string[] { "v" };

		// Token: 0x04009A0B RID: 39435
		private static byte[] attributeNamespaceIds;
	}
}
