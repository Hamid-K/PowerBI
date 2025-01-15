using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3C RID: 12092
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class String255Type : OpenXmlLeafElement
	{
		// Token: 0x17008FA8 RID: 36776
		// (get) Token: 0x06019F2B RID: 106283 RVA: 0x0035A308 File Offset: 0x00358508
		internal override string[] AttributeTagNames
		{
			get
			{
				return String255Type.attributeTagNames;
			}
		}

		// Token: 0x17008FA9 RID: 36777
		// (get) Token: 0x06019F2C RID: 106284 RVA: 0x0035A30F File Offset: 0x0035850F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return String255Type.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FAA RID: 36778
		// (get) Token: 0x06019F2D RID: 106285 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019F2E RID: 106286 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019F2F RID: 106287 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AB09 RID: 43785
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB0A RID: 43786
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
