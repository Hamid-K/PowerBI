using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C83 RID: 11395
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LegacyDrawingType : OpenXmlLeafElement
	{
		// Token: 0x17008362 RID: 33634
		// (get) Token: 0x0601846A RID: 99434 RVA: 0x00340070 File Offset: 0x0033E270
		internal override string[] AttributeTagNames
		{
			get
			{
				return LegacyDrawingType.attributeTagNames;
			}
		}

		// Token: 0x17008363 RID: 33635
		// (get) Token: 0x0601846B RID: 99435 RVA: 0x00340077 File Offset: 0x0033E277
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LegacyDrawingType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008364 RID: 33636
		// (get) Token: 0x0601846C RID: 99436 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601846D RID: 99437 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0601846E RID: 99438 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04009F9B RID: 40859
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009F9C RID: 40860
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
