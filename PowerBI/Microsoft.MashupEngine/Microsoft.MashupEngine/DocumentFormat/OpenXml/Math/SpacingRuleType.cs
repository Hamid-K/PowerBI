using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A3 RID: 10659
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SpacingRuleType : OpenXmlLeafElement
	{
		// Token: 0x17006D2E RID: 27950
		// (get) Token: 0x06015319 RID: 86809 RVA: 0x0031CBA8 File Offset: 0x0031ADA8
		internal override string[] AttributeTagNames
		{
			get
			{
				return SpacingRuleType.attributeTagNames;
			}
		}

		// Token: 0x17006D2F RID: 27951
		// (get) Token: 0x0601531A RID: 86810 RVA: 0x0031CBAF File Offset: 0x0031ADAF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SpacingRuleType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D30 RID: 27952
		// (get) Token: 0x0601531B RID: 86811 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601531C RID: 86812 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public IntegerValue Val
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601531D RID: 86813 RVA: 0x0031CBB6 File Offset: 0x0031ADB6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04009206 RID: 37382
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009207 RID: 37383
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
