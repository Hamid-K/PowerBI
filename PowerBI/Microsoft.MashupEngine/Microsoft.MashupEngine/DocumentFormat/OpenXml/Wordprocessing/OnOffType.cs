using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D69 RID: 11625
	[GeneratedCode("DomGen", "2.0")]
	public abstract class OnOffType : OpenXmlLeafElement
	{
		// Token: 0x170086D7 RID: 34519
		// (get) Token: 0x06018C6D RID: 101485 RVA: 0x00344972 File Offset: 0x00342B72
		internal override string[] AttributeTagNames
		{
			get
			{
				return OnOffType.attributeTagNames;
			}
		}

		// Token: 0x170086D8 RID: 34520
		// (get) Token: 0x06018C6E RID: 101486 RVA: 0x00344979 File Offset: 0x00342B79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OnOffType.attributeNamespaceIds;
			}
		}

		// Token: 0x170086D9 RID: 34521
		// (get) Token: 0x06018C6F RID: 101487 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x06018C70 RID: 101488 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public OnOffValue Val
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018C71 RID: 101489 RVA: 0x00344980 File Offset: 0x00342B80
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A4A7 RID: 42151
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A4A8 RID: 42152
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
