using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AE RID: 9390
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OnOffType : OpenXmlLeafElement
	{
		// Token: 0x17005248 RID: 21064
		// (get) Token: 0x06011618 RID: 71192 RVA: 0x002EDED2 File Offset: 0x002EC0D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return OnOffType.attributeTagNames;
			}
		}

		// Token: 0x17005249 RID: 21065
		// (get) Token: 0x06011619 RID: 71193 RVA: 0x002EDED9 File Offset: 0x002EC0D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OnOffType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700524A RID: 21066
		// (get) Token: 0x0601161A RID: 71194 RVA: 0x002EDEE0 File Offset: 0x002EC0E0
		// (set) Token: 0x0601161B RID: 71195 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public EnumValue<OnOffValues> Val
		{
			get
			{
				return (EnumValue<OnOffValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601161C RID: 71196 RVA: 0x002EDEEF File Offset: 0x002EC0EF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<OnOffValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400798C RID: 31116
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400798D RID: 31117
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
