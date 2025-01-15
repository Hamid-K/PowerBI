using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A6 RID: 10662
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class UnsignedShortType : OpenXmlLeafElement
	{
		// Token: 0x17006D37 RID: 27959
		// (get) Token: 0x0601532C RID: 86828 RVA: 0x0031CC42 File Offset: 0x0031AE42
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsignedShortType.attributeTagNames;
			}
		}

		// Token: 0x17006D38 RID: 27960
		// (get) Token: 0x0601532D RID: 86829 RVA: 0x0031CC49 File Offset: 0x0031AE49
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsignedShortType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D39 RID: 27961
		// (get) Token: 0x0601532E RID: 86830 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x0601532F RID: 86831 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015330 RID: 86832 RVA: 0x0031CC50 File Offset: 0x0031AE50
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400920E RID: 37390
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400920F RID: 37391
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
