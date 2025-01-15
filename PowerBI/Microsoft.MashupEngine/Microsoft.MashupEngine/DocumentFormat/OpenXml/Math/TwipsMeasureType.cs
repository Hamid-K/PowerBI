using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B4 RID: 10676
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TwipsMeasureType : OpenXmlLeafElement
	{
		// Token: 0x17006D94 RID: 28052
		// (get) Token: 0x060153F7 RID: 87031 RVA: 0x0031D420 File Offset: 0x0031B620
		internal override string[] AttributeTagNames
		{
			get
			{
				return TwipsMeasureType.attributeTagNames;
			}
		}

		// Token: 0x17006D95 RID: 28053
		// (get) Token: 0x060153F8 RID: 87032 RVA: 0x0031D427 File Offset: 0x0031B627
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TwipsMeasureType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D96 RID: 28054
		// (get) Token: 0x060153F9 RID: 87033 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060153FA RID: 87034 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
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

		// Token: 0x060153FB RID: 87035 RVA: 0x0031D42E File Offset: 0x0031B62E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400924D RID: 37453
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400924E RID: 37454
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
