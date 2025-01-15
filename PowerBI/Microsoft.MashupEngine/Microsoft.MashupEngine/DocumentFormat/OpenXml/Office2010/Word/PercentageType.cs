using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248F RID: 9359
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PercentageType : OpenXmlLeafElement
	{
		// Token: 0x17005182 RID: 20866
		// (get) Token: 0x0601146E RID: 70766 RVA: 0x002ECA20 File Offset: 0x002EAC20
		internal override string[] AttributeTagNames
		{
			get
			{
				return PercentageType.attributeTagNames;
			}
		}

		// Token: 0x17005183 RID: 20867
		// (get) Token: 0x0601146F RID: 70767 RVA: 0x002ECA27 File Offset: 0x002EAC27
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PercentageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005184 RID: 20868
		// (get) Token: 0x06011470 RID: 70768 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011471 RID: 70769 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
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

		// Token: 0x06011472 RID: 70770 RVA: 0x002EC920 File Offset: 0x002EAB20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04007907 RID: 30983
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007908 RID: 30984
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
