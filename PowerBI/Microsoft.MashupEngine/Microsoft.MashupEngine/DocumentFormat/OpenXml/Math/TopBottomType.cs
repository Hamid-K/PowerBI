using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299A RID: 10650
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TopBottomType : OpenXmlLeafElement
	{
		// Token: 0x17006CEA RID: 27882
		// (get) Token: 0x06015288 RID: 86664 RVA: 0x0031C3F3 File Offset: 0x0031A5F3
		internal override string[] AttributeTagNames
		{
			get
			{
				return TopBottomType.attributeTagNames;
			}
		}

		// Token: 0x17006CEB RID: 27883
		// (get) Token: 0x06015289 RID: 86665 RVA: 0x0031C3FA File Offset: 0x0031A5FA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TopBottomType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006CEC RID: 27884
		// (get) Token: 0x0601528A RID: 86666 RVA: 0x0031C401 File Offset: 0x0031A601
		// (set) Token: 0x0601528B RID: 86667 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<VerticalJustificationValues> Val
		{
			get
			{
				return (EnumValue<VerticalJustificationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601528C RID: 86668 RVA: 0x0031C410 File Offset: 0x0031A610
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<VerticalJustificationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040091E0 RID: 37344
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040091E1 RID: 37345
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
