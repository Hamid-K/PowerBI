using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248A RID: 9354
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PositiveFixedPercentageType : OpenXmlLeafElement
	{
		// Token: 0x17005170 RID: 20848
		// (get) Token: 0x06011449 RID: 70729 RVA: 0x002EC912 File Offset: 0x002EAB12
		internal override string[] AttributeTagNames
		{
			get
			{
				return PositiveFixedPercentageType.attributeTagNames;
			}
		}

		// Token: 0x17005171 RID: 20849
		// (get) Token: 0x0601144A RID: 70730 RVA: 0x002EC919 File Offset: 0x002EAB19
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PositiveFixedPercentageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005172 RID: 20850
		// (get) Token: 0x0601144B RID: 70731 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601144C RID: 70732 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601144D RID: 70733 RVA: 0x002EC920 File Offset: 0x002EAB20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040078F7 RID: 30967
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040078F8 RID: 30968
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
