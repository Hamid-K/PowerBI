using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBC RID: 12220
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SignedTwipsMeasureType : OpenXmlLeafElement
	{
		// Token: 0x170093CB RID: 37835
		// (get) Token: 0x0601A7FC RID: 108540 RVA: 0x0036323F File Offset: 0x0036143F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SignedTwipsMeasureType.attributeTagNames;
			}
		}

		// Token: 0x170093CC RID: 37836
		// (get) Token: 0x0601A7FD RID: 108541 RVA: 0x00363246 File Offset: 0x00361446
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SignedTwipsMeasureType.attributeNamespaceIds;
			}
		}

		// Token: 0x170093CD RID: 37837
		// (get) Token: 0x0601A7FE RID: 108542 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A7FF RID: 108543 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601A800 RID: 108544 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AD38 RID: 44344
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD39 RID: 44345
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
