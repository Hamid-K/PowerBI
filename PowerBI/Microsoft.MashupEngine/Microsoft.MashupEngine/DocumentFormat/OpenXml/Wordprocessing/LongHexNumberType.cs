using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F78 RID: 12152
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LongHexNumberType : OpenXmlLeafElement
	{
		// Token: 0x17009126 RID: 37158
		// (get) Token: 0x0601A26F RID: 107119 RVA: 0x0035E181 File Offset: 0x0035C381
		internal override string[] AttributeTagNames
		{
			get
			{
				return LongHexNumberType.attributeTagNames;
			}
		}

		// Token: 0x17009127 RID: 37159
		// (get) Token: 0x0601A270 RID: 107120 RVA: 0x0035E188 File Offset: 0x0035C388
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LongHexNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x17009128 RID: 37160
		// (get) Token: 0x0601A271 RID: 107121 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601A272 RID: 107122 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A273 RID: 107123 RVA: 0x0035E18F File Offset: 0x0035C38F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AC0D RID: 44045
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC0E RID: 44046
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
