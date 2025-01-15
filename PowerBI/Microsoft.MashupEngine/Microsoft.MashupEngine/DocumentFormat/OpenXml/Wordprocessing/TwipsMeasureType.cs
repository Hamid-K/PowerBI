using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F8B RID: 12171
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TwipsMeasureType : OpenXmlLeafElement
	{
		// Token: 0x170091B9 RID: 37305
		// (get) Token: 0x0601A3AC RID: 107436 RVA: 0x0035F4FC File Offset: 0x0035D6FC
		internal override string[] AttributeTagNames
		{
			get
			{
				return TwipsMeasureType.attributeTagNames;
			}
		}

		// Token: 0x170091BA RID: 37306
		// (get) Token: 0x0601A3AD RID: 107437 RVA: 0x0035F503 File Offset: 0x0035D703
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TwipsMeasureType.attributeNamespaceIds;
			}
		}

		// Token: 0x170091BB RID: 37307
		// (get) Token: 0x0601A3AE RID: 107438 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A3AF RID: 107439 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A3B0 RID: 107440 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AC51 RID: 44113
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC52 RID: 44114
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
