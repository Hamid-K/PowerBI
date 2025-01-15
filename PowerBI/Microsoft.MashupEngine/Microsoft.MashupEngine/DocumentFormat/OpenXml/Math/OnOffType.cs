using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002968 RID: 10600
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OnOffType : OpenXmlLeafElement
	{
		// Token: 0x17006C43 RID: 27715
		// (get) Token: 0x06015113 RID: 86291 RVA: 0x0031B359 File Offset: 0x00319559
		internal override string[] AttributeTagNames
		{
			get
			{
				return OnOffType.attributeTagNames;
			}
		}

		// Token: 0x17006C44 RID: 27716
		// (get) Token: 0x06015114 RID: 86292 RVA: 0x0031B360 File Offset: 0x00319560
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OnOffType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006C45 RID: 27717
		// (get) Token: 0x06015115 RID: 86293 RVA: 0x0031B367 File Offset: 0x00319567
		// (set) Token: 0x06015116 RID: 86294 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<BooleanValues> Val
		{
			get
			{
				return (EnumValue<BooleanValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015117 RID: 86295 RVA: 0x0031B376 File Offset: 0x00319576
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<BooleanValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04009145 RID: 37189
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009146 RID: 37190
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
