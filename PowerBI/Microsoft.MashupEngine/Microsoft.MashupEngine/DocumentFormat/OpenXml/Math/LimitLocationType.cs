using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C0 RID: 10688
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LimitLocationType : OpenXmlLeafElement
	{
		// Token: 0x17006DC4 RID: 28100
		// (get) Token: 0x06015462 RID: 87138 RVA: 0x0031D7A0 File Offset: 0x0031B9A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return LimitLocationType.attributeTagNames;
			}
		}

		// Token: 0x17006DC5 RID: 28101
		// (get) Token: 0x06015463 RID: 87139 RVA: 0x0031D7A7 File Offset: 0x0031B9A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LimitLocationType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006DC6 RID: 28102
		// (get) Token: 0x06015464 RID: 87140 RVA: 0x0031D7AE File Offset: 0x0031B9AE
		// (set) Token: 0x06015465 RID: 87141 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<LimitLocationValues> Val
		{
			get
			{
				return (EnumValue<LimitLocationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015466 RID: 87142 RVA: 0x0031D7BD File Offset: 0x0031B9BD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<LimitLocationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04009272 RID: 37490
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009273 RID: 37491
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
