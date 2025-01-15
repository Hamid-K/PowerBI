using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CD RID: 10701
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OfficeMathJustificationType : OpenXmlLeafElement
	{
		// Token: 0x17006E1A RID: 28186
		// (get) Token: 0x0601551F RID: 87327 RVA: 0x0031DEA8 File Offset: 0x0031C0A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return OfficeMathJustificationType.attributeTagNames;
			}
		}

		// Token: 0x17006E1B RID: 28187
		// (get) Token: 0x06015520 RID: 87328 RVA: 0x0031DEAF File Offset: 0x0031C0AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OfficeMathJustificationType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E1C RID: 28188
		// (get) Token: 0x06015521 RID: 87329 RVA: 0x0031DEB6 File Offset: 0x0031C0B6
		// (set) Token: 0x06015522 RID: 87330 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<JustificationValues> Val
		{
			get
			{
				return (EnumValue<JustificationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015523 RID: 87331 RVA: 0x0031DEC5 File Offset: 0x0031C0C5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<JustificationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040092AA RID: 37546
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040092AB RID: 37547
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
